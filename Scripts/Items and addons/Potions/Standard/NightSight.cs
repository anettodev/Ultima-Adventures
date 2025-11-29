using System;
using Server;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.First;

namespace Server.Items
{
	/// <summary>
	/// Night Sight Potion - Provides enhanced vision in darkness.
	/// Allows the drinker to see in low-light conditions.
	/// Fixed 60-second duration with guaranteed light level (100 Magery equivalent).
	/// 50% chance to activate Sense Mode (wealth, trap, danger, aura detection).
	/// 120-second cooldown after use.
	/// </summary>
	public class NightSightPotion : BasePotion
	{
		#region Static Data

		/// <summary>Tracks cooldown end times for each mobile</summary>
		private static System.Collections.Generic.Dictionary<Mobile, DateTime> m_Cooldowns = new System.Collections.Generic.Dictionary<Mobile, DateTime>();

		#endregion

		#region Constants

	/// <summary>Visual effect ID (same as NightSight spell)</summary>
	private const int EFFECT_ID = 0x376A;

	/// <summary>Visual effect speed</summary>
	private const int EFFECT_SPEED = 9;

	/// <summary>Visual effect render mode</summary>
	private const int EFFECT_RENDER = 32;

	/// <summary>Visual effect duration</summary>
	private const int EFFECT_DURATION = 5007;

	/// <summary>Sound effect ID (same as NightSight spell)</summary>
	private const int SOUND_ID = 0x1E3;

	/// <summary>Fixed duration for potion effect (seconds)</summary>
	private const int POTION_DURATION_SECONDS = 60;

	/// <summary>Cooldown duration after effect starts (seconds)</summary>
	private const int POTION_COOLDOWN_SECONDS = 120; // 2 minutes

	/// <summary>Magery value used for light level calculation (always 100)</summary>
	private const double FIXED_MAGERY_FOR_LIGHT = 100.0;

	/// <summary>Chance to activate sensing abilities (50%)</summary>
	private const double SENSING_ABILITIES_CHANCE = 0.50;

	/// <summary>Message color for system messages (cyan)</summary>
	private const int MESSAGE_COLOR = 0x59;

	/// <summary>Message color for errors (red)</summary>
	private const int MESSAGE_COLOR_ERROR = 0x22;

	#endregion

		#region Constructors

		/// <summary>
		/// Creates a new Night Sight Potion
		/// </summary>
		[Constructable]
		public NightSightPotion() : base( 0xF06, PotionEffect.Nightsight )
		{
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public NightSightPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the night sight potion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the night sight potion
		/// </summary>
		/// <param name="reader">Generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds custom properties to the object property list
		/// </summary>
		/// <param name="list">The object property list</param>
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			// Display potion type in custom cyan color (#8be4fc) with brackets
			string potionName = PotionMetadata.GetKegName( this.PotionEffect );
			if ( potionName != null )
			{
				list.Add( 1070722, string.Format( "<BASEFONT COLOR=#8be4fc>[{0}]", potionName ) ); // Custom cyan color #8be4fc
			}
		}

		#endregion

	#region Core Logic

	/// <summary>
	/// Handles drinking the night sight potion
	/// </summary>
	/// <param name="from">The mobile drinking the potion</param>
	public override void Drink( Mobile from )
	{
		// Check if on cooldown
		if ( IsOnCooldown( from ) )
		{
			TimeSpan remaining = GetRemainingCooldown( from );
			from.SendMessage( MESSAGE_COLOR_ERROR, string.Format( "Você não pode beber outra poção de visão noturna por mais {0} segundos.", (int)remaining.TotalSeconds ) );
			return;
		}

		// Check if already has night sight active (cannot stack)
		if ( !from.BeginAction( typeof( LightCycle ) ) )
		{
			from.SendMessage( MESSAGE_COLOR_ERROR, "Você já possui visão noturna ativa. Não é possível acumular o efeito." );
			return;
		}

		// Fixed duration (60 seconds)
		TimeSpan duration = TimeSpan.FromSeconds( POTION_DURATION_SECONDS );

		// Calculate light level using fixed 100 Magery equivalent (guaranteed)
		int level = (int)( LightCycle.DungeonLevel * ( FIXED_MAGERY_FOR_LIGHT / 100 ) );
		if ( level < 0 )
			level = 0;

		from.LightLevel = level;

		// Roll for sensing abilities (50% chance)
		bool senseModeActivated = Utility.RandomDouble() < SENSING_ABILITIES_CHANCE;

		// Start enhanced nightsight timer with danger sense
		new EnhancedNightSightTimer( from, from, duration ).Start();

		// Visual and sound effects
		int hue = Server.Items.CharacterDatabase.GetMySpellHue( from, 0 );
		from.FixedParticles( EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_DURATION, hue, 0, EffectLayer.Waist );
		from.PlaySound( SOUND_ID );

		BasePotion.PlayDrinkEffect( from );

		// Add buff icon
		BuffInfo.AddBuff( from, new BuffInfo( BuffIcon.NightSight, 1075643 ) ); // Night Sight/You ignore lighting effects

		// Activate Sense Mode if successful roll
		if ( senseModeActivated )
		{
			NightSightSpell.ActivateSenseMode( from );
			from.SendMessage( MESSAGE_COLOR, "Seus olhos brilham intensamente e seus sentidos se aguçam! Você pode detectar riquezas, armadilhas, perigos e auras." );
		}
		else
		{
			from.SendMessage( MESSAGE_COLOR, "Seus olhos brilham intensamente! A escuridão não é mais um problema por um tempo..." );
		}

		// Set cooldown (1200 seconds from now)
		SetCooldown( from );

		this.Consume();
	}

	/// <summary>
	/// Checks if a mobile is on cooldown for Night Sight potion
	/// Also performs periodic cleanup of expired cooldowns
	/// </summary>
	private bool IsOnCooldown( Mobile from )
	{
		// Periodic cleanup (1% chance per check to avoid overhead)
		if ( Utility.RandomDouble() < 0.01 )
		{
			CleanupCooldowns();
		}

		if ( m_Cooldowns.ContainsKey( from ) )
		{
			DateTime cooldownEnd = m_Cooldowns[from];
			if ( DateTime.UtcNow < cooldownEnd )
			{
				return true;
			}
			else
			{
				// Cooldown expired, remove from dictionary
				m_Cooldowns.Remove( from );
				return false;
			}
		}
		return false;
	}

	/// <summary>
	/// Gets the remaining cooldown time for a mobile
	/// </summary>
	private TimeSpan GetRemainingCooldown( Mobile from )
	{
		if ( m_Cooldowns.ContainsKey( from ) )
		{
			DateTime cooldownEnd = m_Cooldowns[from];
			TimeSpan remaining = cooldownEnd - DateTime.UtcNow;
			return remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero;
		}
		return TimeSpan.Zero;
	}

	/// <summary>
	/// Sets the cooldown for a mobile
	/// </summary>
	private void SetCooldown( Mobile from )
	{
		DateTime cooldownEnd = DateTime.UtcNow + TimeSpan.FromSeconds( POTION_COOLDOWN_SECONDS );
		m_Cooldowns[from] = cooldownEnd;
	}

	/// <summary>
	/// Cleans up expired cooldowns and deleted mobiles from the dictionary
	/// Called automatically when checking cooldown status
	/// </summary>
	public static void CleanupCooldowns()
	{
		System.Collections.Generic.List<Mobile> toRemove = new System.Collections.Generic.List<Mobile>();
		
		foreach ( System.Collections.Generic.KeyValuePair<Mobile, DateTime> kvp in m_Cooldowns )
		{
			// Remove if mobile is deleted or cooldown expired
			if ( kvp.Key == null || kvp.Key.Deleted || DateTime.UtcNow >= kvp.Value )
			{
				toRemove.Add( kvp.Key );
			}
		}
		
		foreach ( Mobile m in toRemove )
		{
			m_Cooldowns.Remove( m );
		}
	}

	#endregion

	#region Enhanced Night Sight Timer

	/// <summary>
	/// Enhanced timer with danger sense mechanics
	/// Checks for hidden hostiles every 5 seconds
	/// Fixed 60-second duration
	/// </summary>
	private class EnhancedNightSightTimer : Timer
	{
		private Mobile m_Target;
		private Mobile m_Caster;
		private DateTime m_End;
		private DateTime m_NextDangerCheck;

		// Danger Sense Constants
		private const double DANGER_SENSE_CHANCE_PER_MAGERY = 0.2;
		private const double DANGER_SENSE_CHANCE_PER_FORENSICS = 0.1;
		private const int DANGER_SENSE_RANGE = 3;
		private const double DANGER_SENSE_CHECK_INTERVAL_SECONDS = 5.0;

		public EnhancedNightSightTimer( Mobile target, Mobile caster, TimeSpan duration ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
		{
			m_Target = target;
			m_Caster = caster;
			m_End = DateTime.UtcNow + duration;
			m_NextDangerCheck = DateTime.UtcNow + TimeSpan.FromSeconds( DANGER_SENSE_CHECK_INTERVAL_SECONDS );
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick()
		{
			if ( DateTime.UtcNow >= m_End )
			{
				// Timer expired - clean up effect (cooldown continues separately)
				m_Target.EndAction( typeof( LightCycle ) );
				m_Target.LightLevel = 0;
				BuffInfo.RemoveBuff( m_Target, BuffIcon.NightSight );
				
				// Deactivate Sense Mode if active
				NightSightSpell.DeactivateSenseMode( m_Target );
				
				// Notify player effect expired (PT-BR)
				m_Target.SendMessage( MESSAGE_COLOR, "O efeito da poção de visão noturna expirou." );
				
				Stop();
				return;
			}

			// Danger Sense check (for potion drinker with Sense Mode active)
			if ( m_Target == m_Caster && NightSightSpell.HasSenseMode( m_Target ) && DateTime.UtcNow >= m_NextDangerCheck )
			{
				PerformDangerSenseCheck();
				m_NextDangerCheck = DateTime.UtcNow + TimeSpan.FromSeconds( DANGER_SENSE_CHECK_INTERVAL_SECONDS );
			}
		}

		private void PerformDangerSenseCheck()
		{
			if ( m_Target == null || m_Target.Deleted || m_Target.Map == null )
				return;

			// Calculate chance
			double mageryChance = m_Caster.Skills[SkillName.Magery].Value * DANGER_SENSE_CHANCE_PER_MAGERY;
			double forensicsBonus = m_Caster.Skills[SkillName.Forensics].Value * DANGER_SENSE_CHANCE_PER_FORENSICS;
			double totalChance = mageryChance + forensicsBonus;

			if ( Utility.RandomDouble() * 100.0 >= totalChance )
				return;

			// Check for hidden hostiles within range
			IPooledEnumerable eable = m_Target.GetMobilesInRange( DANGER_SENSE_RANGE );
			bool foundDanger = false;

			try
			{
				foreach ( Mobile m in eable )
				{
					if ( m == m_Target || !m.Hidden || !m.Alive )
						continue;

					// Check if mobile is hostile
					if ( m is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)m;
						if ( bc.Controlled && bc.ControlMaster == m_Target )
							continue; // Skip own pets

						foundDanger = true;
						break;
					}
					else if ( m.Player )
					{
						// Hidden player nearby
						foundDanger = true;
						break;
					}
				}
			}
			finally
			{
				eable.Free();
			}

			if ( foundDanger )
			{
				m_Target.SendMessage( Spell.MSG_COLOR_WARNING, "Você sente algo nas sombras ou no ar..." );
			}
		}
	}

	#endregion
}
}
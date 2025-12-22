using System;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Spells;
using Server.Mobiles;
using Server.Items.Helpers;

namespace Server.Items
{
	/// <summary>
	/// Base class for all frostbite potions.
	/// 50% chance to paralyze enemies in the explosion area and creates ice patches.
	/// Regular: 3 second paralyze, Greater: 5 second paralyze.
	/// Ice patches deal cold damage over time.
	/// Throwable with countdown and area effect.
	/// </summary>
	public abstract class BaseFrostbitePotion : BaseThrowablePotion
	{
	#region Abstract Properties

	/// <summary>Gets the minimum damage for ice patches</summary>
	public abstract int MinDamage{ get; }

	/// <summary>Gets the maximum damage for ice patches</summary>
	public abstract int MaxDamage{ get; }

	/// <summary>Gets the paralyze duration in seconds</summary>
	public abstract double ParalyzeDuration{ get; }

	/// <summary>Gets the ice patch duration in seconds (how long ice patches last)</summary>
	public abstract double IcePatchDuration{ get; }

	#endregion

		#region Throwable Potion Configuration

		/// <summary>Flying potion item ID during throw</summary>
		protected override int FlyingPotionItemID { get { return FrostbiteConstants.FLYING_POTION_ITEM_ID; } }

		/// <summary>Potion type name for messages</summary>
		protected override string PotionTypeName { get { return "poção de congelamento"; } }

		/// <summary>Base cooldown between throws (30 seconds, reduced by Chemist)</summary>
		protected override double BaseCooldownSeconds { get { return FrostbiteConstants.BASE_COOLDOWN_SECONDS; } }

		/// <summary>Effect radius (2 tiles)</summary>
		protected override int EffectRadius { get { return FrostbiteConstants.EXPLOSION_RADIUS; } }

		/// <summary>Countdown message color (cyan for ice)</summary>
		protected override int CountdownMessageColor { get { return 0x59; } } // Cyan

		/// <summary>Initial countdown delay (0.5 seconds for faster countdown)</summary>
		protected override double CountdownInitialDelay { get { return 0.5; } }

		/// <summary>Countdown tick interval (0.75 seconds for 3 second total)</summary>
		/// <remarks>Total time: 0.5 + (0.75 * 3) = 2.75 seconds ≈ 3 seconds</remarks>
		protected override double CountdownTickInterval { get { return 0.75; } }

		#endregion

		#region Constructors

		public BaseFrostbitePotion( PotionEffect effect ) : base( 0x180F, effect )
		{
		}

		public BaseFrostbitePotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion

		#region Countdown Visual Effects

		/// <summary>
		/// Displays ice visual effect on each countdown tick
		/// </summary>
		protected override void OnCountdownTick( Point3D loc, Map map, int timer )
		{
			// Ice mist effect during countdown
			Effects.SendLocationEffect( loc, map, 0x376A, 20, 10, 0x481, 0 ); // Ice blue effect
		}

		/// <summary>
		/// Plays sound when potion lands
		/// </summary>
		protected override void OnLanding( Point3D loc, Map map )
		{
			Effects.PlaySound( loc, map, 0x28 ); // Ice crack sound
		}

		#endregion

		#region Area Effect Implementation

		/// <summary>
		/// Plays detonation visual and sound effects
		/// </summary>
		protected override void PlayDetonationEffects( Point3D loc, Map map )
		{
			// Explosion sound (ice blast)
			Effects.PlaySound( loc, map, FrostbiteConstants.EXPLOSION_SOUND_ID );

			// Large ice explosion visual
			Effects.SendLocationEffect( loc, map, 0x3818, 20, 10, 0x481, 0 );
		}

		/// <summary>
		/// Applies area effect: Creates ice patches that continuously attempt to paralyze enemies
		/// No immediate damage or paralyze on explosion
		/// Ice patches roll 50% paralyze chance every tick (every second) for each enemy
		/// </summary>
		protected override void ApplyAreaEffect( Mobile from, bool direct, Point3D loc, Map map )
		{
			// Mark thrower as criminal (throwing frostbite is a hostile act)
			if ( from != null && from.Player )
			{
				from.Criminal = true;
			}

			// Create ice field pattern (5x5 grid)
			// Ice patches will handle paralyze rolls every tick (every second)
			// Each mobile gets independent 50% roll per tick
			CreateIceFieldPattern( from, loc, map );
		}

		#endregion

		#region Frostbite Effect Logic

		/// <summary>
		/// Checks if target can be affected by frostbite (INCLUDING the caster)
		/// </summary>
		private bool CanAffectTarget( Mobile from, Mobile target )
		{
			// Caster CAN be paralyzed by their own frostbite potion
			return SpellHelper.ValidIndirectTarget( from, target ) && 
				   from.CanBeHarmful( target, false );
		}

		/// <summary>
		/// Applies frostbite effect: 50% chance to paralyze (no damage)
		/// Uses same visual/sound effects as ParalyzeField for consistency
		/// </summary>
		private void ApplyFrostbiteEffect( Mobile from, Mobile target )
		{
			if ( from != null )
				from.DoHarmful( target );

			// 50% chance to paralyze
			bool paralyzed = ( Utility.RandomDouble() < 0.50 );

			if ( paralyzed )
			{
				// Apply paralyze effect (no damage, only freeze)
				target.Paralyze( TimeSpan.FromSeconds( ParalyzeDuration ) );

				// Visual and sound effects (EXACT SAME as ParalyzeField line 246-247)
				target.PlaySound( FrostbiteConstants.PARALYZE_SOUND_ID );
				target.FixedEffect( 
					FrostbiteConstants.PARALYZE_EFFECT_ID, 
					FrostbiteConstants.PARALYZE_EFFECT_COUNT, 
					FrostbiteConstants.PARALYZE_EFFECT_DURATION, 
					Server.Items.CharacterDatabase.GetMySpellHue( from, 0 ), 
					0 
				);

				// Send message to victim
				target.SendMessage( 0x59, "Você foi congelado!" ); // Cyan
			}
			else
			{
				// Failed to paralyze - show frost effect but no freeze
				target.FixedParticles( 
					FrostbiteConstants.ICE_MIST_EFFECT_ID, 
					FrostbiteConstants.ICE_MIST_EFFECT_SPEED, 
					FrostbiteConstants.ICE_MIST_EFFECT_DURATION, 
					FrostbiteConstants.ICE_MIST_EFFECT_NUMBER, 
					EffectLayer.Waist 
				);
				target.PlaySound( FrostbiteConstants.ICE_CRACK_SOUND_ID );

				// Send message to victim
				target.SendMessage( 0x59, "Você resistiu ao efeito de congelamento!" ); // Cyan
			}
		}

		#endregion

		#region Ice Field Creation

		/// <summary>
		/// Creates a pattern of ice patches in a grid around the explosion point
		/// Ice patches attempt to paralyze enemies every tick (50% chance)
		/// </summary>
		private void CreateIceFieldPattern( Mobile from, Point3D loc, Map map )
		{
			for ( int i = -FrostbiteConstants.EXPLOSION_RADIUS; i <= FrostbiteConstants.EXPLOSION_RADIUS; i++ )
			{
				for ( int j = -FrostbiteConstants.EXPLOSION_RADIUS; j <= FrostbiteConstants.EXPLOSION_RADIUS; j++ )
				{
					Point3D p = new Point3D( loc.X + i, loc.Y + j, loc.Z );

					if ( map.CanFit( p, FrostbiteConstants.ITEM_HEIGHT_CHECK, true, false ) && from.InLOS( p ) )
						new IcePatch( from, p, map, ParalyzeDuration, IcePatchDuration );
				}
			}
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Ice patch that attempts to paralyze enemies every tick
		/// Lasts 10 seconds, rolls 50% paralyze chance every second for each mobile
		/// </summary>
		public class IcePatch : Item
		{
			#region Fields

			private Mobile m_From;
			private double m_ParalyzeDuration; // Duration in seconds (3.0 or 5.0)
			private DateTime m_End;
			private Timer m_Timer;

			#endregion

			#region Properties

			public Mobile From{ get{ return m_From; } }
			public override bool BlocksFit{ get{ return true; } }

			#endregion

		#region Constructors

		public IcePatch( Mobile from, Point3D loc, Map map, double paralyzeDuration, double icePatchDuration ) : base( FrostbiteConstants.ICE_PATCH_ITEM_ID )
		{
			Movable = false;
			Hue = FrostbiteConstants.ICE_PATCH_HUE;

			MoveToWorld( loc, map );

			m_From = from;
			m_ParalyzeDuration = paralyzeDuration;
			m_End = DateTime.UtcNow + TimeSpan.FromSeconds( icePatchDuration );

			m_Timer = new IcePatchTimer( this, m_End );
			m_Timer.Start();
		}

		public IcePatch( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Visual Effects Application

		/// <summary>
		/// Checks if mobile is in valid range for ice effects
		/// </summary>
		private bool IsInIcePatchRange( Mobile m )
		{
			return (m.Z + FrostbiteConstants.Z_AXIS_HEIGHT_CHECK) > this.Z && 
				   (this.Z + FrostbiteConstants.ITEM_HEIGHT_CHECK) > m.Z;
		}

		/// <summary>
		/// Checks if target can be affected by ice effects (INCLUDING the caster)
		/// </summary>
		private bool CanDamageTarget( Mobile m )
		{
			// Caster CAN be paralyzed by their own ice patches
			return SpellHelper.ValidIndirectTarget( m_From, m ) && 
				   m_From.CanBeHarmful( m, false );
		}

		/// <summary>
		/// Attempts to paralyze a mobile every tick (50% chance per tick)
		/// Skips if already paralyzed
		/// Uses ParalyzeField visual/sound effects
		/// </summary>
		private void ApplyIceDamage( Mobile m )
		{
			// Skip if already paralyzed (don't re-paralyze)
			if ( m.Paralyzed || m.Frozen )
			{
				return;
			}

			// 50% chance to paralyze (rolled EVERY TICK for EACH mobile independently)
			bool paralyzed = ( Utility.RandomDouble() < FrostbiteConstants.PARALYZE_CHANCE );

			if ( paralyzed )
			{
				// Mark harmful action
				if ( m_From != null )
					m_From.DoHarmful( m );

				// Apply paralyze effect
				m.Paralyze( TimeSpan.FromSeconds( GetParalyzeDuration() ) );

				// Visual and sound effects (EXACT SAME as ParalyzeField)
				m.PlaySound( FrostbiteConstants.PARALYZE_SOUND_ID );
				m.FixedEffect( 
					FrostbiteConstants.PARALYZE_EFFECT_ID, 
					FrostbiteConstants.PARALYZE_EFFECT_COUNT, 
					FrostbiteConstants.PARALYZE_EFFECT_DURATION, 
					Server.Items.CharacterDatabase.GetMySpellHue( m_From, 0 ), 
					0 
				);
			}
			else
			{
				// Subtle ice effect for failed paralyze attempt (lighter visual)
				m.FixedParticles( 
					FrostbiteConstants.ICE_HIT_EFFECT_ID, 
					5, // Lighter effect (half particles)
					FrostbiteConstants.ICE_HIT_EFFECT_SPEED, 
					FrostbiteConstants.ICE_HIT_EFFECT_NUMBER, 
					EffectLayer.Waist 
				);
			}
		}

		/// <summary>
		/// Gets paralyze duration for this ice patch
		/// </summary>
		private double GetParalyzeDuration()
		{
			return m_ParalyzeDuration;
		}

			#endregion

			#region Event Handlers

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				if ( m_Timer != null )
					m_Timer.Stop();
			}

			public override bool OnMoveOver( Mobile m )
			{
				if ( Visible && m_From != null && IsInIcePatchRange( m ) && CanDamageTarget( m ) )
				{
					ApplyIceDamage( m );
				}

				return true;
			}

			#endregion

		#region Serialization

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version 1 - changed to paralyze duration

			writer.Write( (Mobile) m_From );
			writer.Write( (DateTime) m_End );
			writer.Write( (double) m_ParalyzeDuration );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			m_From = reader.ReadMobile();
			m_End = reader.ReadDateTime();

			switch ( version )
			{
				case 1:
				{
					m_ParalyzeDuration = reader.ReadDouble();
					break;
				}
				case 0:
				{
					// Old version with damage values - discard and default to 3.0s paralyze
					int minDamage = reader.ReadInt();
					int maxDamage = reader.ReadInt();
					m_ParalyzeDuration = 3.0; // Default to regular frostbite duration
					break;
				}
			}

			m_Timer = new IcePatchTimer( this, m_End );
			m_Timer.Start();
		}

		#endregion

		#region Nested Timer Class

		/// <summary>
		/// Timer that manages ice patch lifecycle (visual/sound effects only, no damage)
		/// </summary>
		private class IcePatchTimer : Timer
			{
				private IcePatch m_Patch;
				private DateTime m_End;

				public IcePatchTimer( IcePatch patch, DateTime end ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( FrostbiteConstants.TIMER_TICK_INTERVAL ) )
				{
					m_Patch = patch;
					m_End = end;
					Priority = TimerPriority.FiftyMS;
				}

				protected override void OnTick()
				{
					if ( m_Patch.Deleted )
						return;

				// Check if patch has expired
				if ( DateTime.UtcNow > m_End )
				{
					m_Patch.Delete();
					Stop();
					return;
				}

				Mobile from = m_Patch.From;

				if ( m_Patch.Map == null || from == null )
					return;

				// Apply visual/sound effects to all mobiles standing on this ice patch
				ProcessMobilesInIce();
				}

			/// <summary>
			/// Finds and applies visual/sound effects to all mobiles standing on the ice patch (no damage)
			/// </summary>
			private void ProcessMobilesInIce()
				{
					List<Mobile> mobiles = new List<Mobile>();

					foreach( Mobile mobile in m_Patch.GetMobilesInRange( FrostbiteConstants.ICE_DAMAGE_RANGE ) )
						mobiles.Add( mobile );

					for( int i = 0; i < mobiles.Count; i++ )
					{
						Mobile m = mobiles[i];
						
						if ( m_Patch.IsInIcePatchRange( m ) && m_Patch.CanDamageTarget( m ) )
						{
							m_Patch.ApplyIceDamage( m );
						}
					}
				}
			}

			#endregion
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;
using Server.Spells;

namespace Server.Spells.Sixth
{
	/// <summary>
	/// Invisibility - 6th Circle Magery Spell
	/// Makes target mobile invisible for a duration
	/// </summary>
	public class InvisibilitySpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Invisibility", "An Lor Xen",
				206,
				9002,
				Reagent.Bloodmoss,
				Reagent.Nightshade
			);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

	#region Constants

	/// <summary>Hiding skill divisor for bonus duration (every 10 points = 2 seconds)</summary>
	private const double HIDING_SKILL_DIVISOR = 10.0;

	/// <summary>Bonus seconds per 10 Hiding skill points</summary>
	private const double HIDING_BONUS_PER_10_POINTS = 2.0;

	/// <summary>Particle effect Z offset</summary>
	private const int PARTICLE_Z_OFFSET = 16;

		/// <summary>Particle effect ID</summary>
		private const int PARTICLE_EFFECT_ID = 0x376A;

		/// <summary>Particle effect count</summary>
		private const int PARTICLE_COUNT = 10;

		/// <summary>Particle effect speed</summary>
		private const int PARTICLE_SPEED = 15;

		/// <summary>Particle effect duration</summary>
		private const int PARTICLE_DURATION = 5045;

		/// <summary>Sound effect ID</summary>
		private const int SOUND_EFFECT = 0x3C4;

	/// <summary>Buff icon localized message ID</summary>
	private const int BUFF_MESSAGE_ID = 1075825;

	#endregion

	#region Data Structures

	/// <summary>Thread-safe storage for active invisibility timers per mobile</summary>
	private static Dictionary<Mobile, Timer> m_Table = new Dictionary<Mobile, Timer>();

	/// <summary>
	/// Whether to hide the target's pets when casting invisibility
	/// Default: false (only hide the target)
	/// </summary>
	public static bool HidePets { get; set; }

	/// <summary>Pet search range when HidePets is enabled</summary>
	private const int PET_SEARCH_RANGE = 12;

	#endregion

		public InvisibilitySpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE );
			}
			else if ( m is Mobiles.BaseVendor || m is Mobiles.PlayerVendor || m is Mobiles.PlayerBarkeeper || m.AccessLevel > Caster.AccessLevel )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, "Este feitiço não funciona neste alvo." );
			}
		else if ( CheckBSequence( m ) )
		{
			SpellHelper.Turn( Caster, m );

			Effects.SendLocationParticles( EffectItem.Create( new Point3D( m.X, m.Y, m.Z + PARTICLE_Z_OFFSET ), Caster.Map, EffectItem.DefaultDuration ), PARTICLE_EFFECT_ID, PARTICLE_COUNT, PARTICLE_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, PARTICLE_DURATION, 0 );
			m.PlaySound( SOUND_EFFECT );

			m.Hidden = true;
			m.Combatant = null;
			m.Warmode = false;
			m.Delta( MobileDelta.Flags );

		// Remove any existing timer WITHOUT forcing reveal (we just hid the player!)
		RemoveTimer( m, forceReveal: false );

		// Optionally hide target's pets (default: false)
		if ( HidePets )
		{
			HideTargetPets( m );
		}

		// Calculate duration using centralized NMS system (uses Inscribe skill)
		TimeSpan baseDuration = SpellHelper.NMSGetDuration( Caster, m, true );

		// Add Hiding skill bonus: +2 seconds per 10 points of Hiding
		double hidingBonus = (Caster.Skills[SkillName.Hiding].Value / HIDING_SKILL_DIVISOR) * HIDING_BONUS_PER_10_POINTS;
		TimeSpan duration = baseDuration + TimeSpan.FromSeconds( hidingBonus );

		// NMSGetDuration already sends a message, but we'll override with our custom message including Hiding bonus
		if ( hidingBonus > 0 )
		{
			Caster.SendMessage( Spell.MSG_COLOR_ERROR, String.Format( "O seu feitiço terá a duração de aproximadamente {0:F1}s ({1:F1}s base + {2:F1}s bônus de Hiding).", 
				duration.TotalSeconds, baseDuration.TotalSeconds, hidingBonus ) );
		}
			Timer t = new InternalTimer( m, duration );

			BuffInfo.RemoveBuff( m, BuffIcon.HidingAndOrStealth );
			BuffInfo.AddBuff( m, new BuffInfo( BuffIcon.Invisibility, BUFF_MESSAGE_ID, duration, m ) );

			lock ( m_Table )
			{
				m_Table[m] = t;
			}

			t.Start();
		}

			FinishSequence();
		}

		public static bool HasTimer( Mobile m )
		{
			lock ( m_Table )
			{
				return m_Table.ContainsKey( m ) && m_Table[m] != null;
			}
		}

	/// <summary>
	/// Removes the invisibility timer for a mobile
	/// </summary>
	/// <param name="m">The mobile whose timer to remove</param>
	/// <param name="forceReveal">If true, forces the mobile to become visible immediately (used for damage/revealing actions)</param>
	public static void RemoveTimer( Mobile m, bool forceReveal = true )
	{
		Timer t = null;
		lock ( m_Table )
		{
			if ( m_Table.TryGetValue( m, out t ) )
			{
				m_Table.Remove( m );
			}
		}

		if ( t != null )
		{
			t.Stop();
		}

		// Force unhide when explicitly requested (damage/revealing action)
		// Do NOT force unhide when called from spell cast (clearing old timer before starting new one)
		if ( forceReveal && m != null && m.Hidden && m.AccessLevel == AccessLevel.Player )
		{
			m.Hidden = false;
			m.Delta( MobileDelta.Flags );
			BuffInfo.RemoveBuff( m, BuffIcon.Invisibility );
		}
	}

		#region Helper Methods

		/// <summary>
		/// Hides all controlled pets of the target mobile
		/// </summary>
		private static void HideTargetPets( Mobile target )
		{
			// Optimize pet finding - use GetMobilesInRange instead of iterating all mobiles
			IPooledEnumerable eable = target.GetMobilesInRange( PET_SEARCH_RANGE );
			foreach ( Mobile pet in eable )
			{
				if ( pet is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)pet;
					if ( bc.Controlled && bc.ControlMaster == target )
						pet.Hidden = true;
				}
			}
			eable.Free();
		}

		#endregion

		#region Internal Classes

	private class InternalTimer : Timer
	{
		private Mobile m_Mobile;

		public InternalTimer( Mobile m, TimeSpan duration ) : base( duration )
		{
			Priority = TimerPriority.OneSecond;
			m_Mobile = m;
		}

	protected override void OnTick()
	{
		// Check if timer is still in the table before revealing
		// If RemoveTimer() was called by damage/revealing action, the timer is already removed
		// and we should NOT call RevealingAction() again (prevents race condition)
		bool shouldReveal = false;
		lock ( m_Table )
		{
			shouldReveal = m_Table.ContainsKey( m_Mobile );
		}

		if ( shouldReveal )
		{
			m_Mobile.RevealingAction();
			RemoveTimer( m_Mobile, forceReveal: true );
		}
	}
	}

		public class InternalTarget : Target
		{
			private InvisibilitySpell m_Owner;

			public InternalTarget( InvisibilitySpell owner ) : base( SpellConstants.GetSpellRange(), false, TargetFlags.Beneficial )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

		#endregion
	}
}

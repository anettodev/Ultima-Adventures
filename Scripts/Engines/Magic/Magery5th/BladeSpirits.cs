using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Spells;

namespace Server.Spells.Fifth
{
	/// <summary>
	/// Blade Spirits - 5th Circle Summon Spell
	/// Summons a WILD and DANGEROUS BladeSpirits at a chosen location
	/// Requires available follower slots
	/// Duration based on Magery and Evaluate Intelligence skills
	/// WARNING: Spirit is UNCONTROLLED and will attack the NEAREST target (including the caster!)
	/// Always evil karma - attacks anything within range
	/// CRIMINAL ACT: Casting this spell flags the caster as criminal (gray)
	/// LIMIT: Only 1 blade spirit per caster (new summon dismisses old one)
	/// </summary>
	public class BladeSpiritsSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Blade Spirits", "In Jux Hur Ylem",
				266,
				9040,
				false,
				Reagent.BlackPearl,
				Reagent.MandrakeRoot,
				Reagent.Nightshade
			);

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }

		#region Single Summon Tracking

		/// <summary>
		/// Tracks the currently active blade spirit for each caster
		/// Only one spirit allowed per caster at a time
		/// </summary>
		private static Dictionary<Mobile, BladeSpirits> m_ActiveSpirits = new Dictionary<Mobile, BladeSpirits>();

		/// <summary>
		/// Registers a newly summoned spirit and dismisses any existing one
		/// </summary>
		private static void RegisterSpirit(Mobile caster, BladeSpirits spirit)
		{
			// Dismiss old spirit if exists
			DismissExistingSpirit(caster);

			// Register new spirit
			m_ActiveSpirits[caster] = spirit;
		}

		/// <summary>
		/// Dismisses the caster's existing blade spirit if present
		/// </summary>
		private static void DismissExistingSpirit(Mobile caster)
		{
			BladeSpirits existingSpirit;
			if (m_ActiveSpirits.TryGetValue(caster, out existingSpirit))
			{
			if (existingSpirit != null && !existingSpirit.Deleted)
			{
				existingSpirit.Delete();
				caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.INFO_PREVIOUS_SUMMON_DISMISSED);
			}
				m_ActiveSpirits.Remove(caster);
			}
		}

		/// <summary>
		/// Unregisters a spirit when it's deleted/expires
		/// Called from blade spirit mobile or cleanup
		/// </summary>
		public static void UnregisterSpirit(Mobile caster, BladeSpirits spirit)
		{
			BladeSpirits registeredSpirit;
			if (m_ActiveSpirits.TryGetValue(caster, out registeredSpirit))
			{
				if (registeredSpirit == spirit)
				{
					m_ActiveSpirits.Remove(caster);
				}
			}
		}

		#endregion

		#region Constants

		// Cast delay multipliers
		/// <summary>Cast delay multiplier for AOS+SE systems</summary>
		private const int CAST_DELAY_MULTIPLIER_SE = 2;

		/// <summary>Cast delay multiplier for AOS (pre-SE) system</summary>
		private const int CAST_DELAY_MULTIPLIER_AOS = 4;

		/// <summary>Additional cast delay for legacy system (seconds)</summary>
		private const double CAST_DELAY_LEGACY_SECONDS = 4.0;

		// Audio constants
		/// <summary>Summon sound effect ID</summary>
		private const int SUMMON_SOUND = 0x212;

		/// <summary>Female "oops" sound when spell fails</summary>
		private const int FEMALE_OOPS_SOUND = 812;

		/// <summary>Male "oops" sound when spell fails</summary>
		private const int MALE_OOPS_SOUND = 1086;

		// Failure messages
		/// <summary>Spoken message when spell fails</summary>
		private const string OOPS_MESSAGE = "*oops*";

		#endregion

		public BladeSpiritsSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		/// <summary>
		/// Override cast delay to make blade spirits slower to summon
		/// SE version: 3x normal delay
		/// AOS version: 5x normal delay
		/// Legacy: base delay + 6 seconds
		/// </summary>
		/// <returns>Modified cast delay timespan</returns>
		public override TimeSpan GetCastDelay()
		{
			if (Core.AOS)
				return TimeSpan.FromTicks(base.GetCastDelay().Ticks * ((Core.SE) ? CAST_DELAY_MULTIPLIER_SE : CAST_DELAY_MULTIPLIER_AOS));

			return base.GetCastDelay() + TimeSpan.FromSeconds(CAST_DELAY_LEGACY_SECONDS);
		}

		/// <summary>
		/// Validates caster can summon blade spirits
		/// Checks: base cast requirements and follower limit
		/// </summary>
		/// <param name="caster">Spell caster</param>
		/// <returns>True if cast can proceed</returns>
		public override bool CheckCast(Mobile caster)
		{
			if (!base.CheckCast(caster))
				return false;

			if (Caster.Followers > Caster.FollowersMax)
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TOO_MANY_FOLLOWERS);
				PlayFailureFeedback();
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
		}

	/// <summary>
	/// Attempts to summon blade spirits at the target location
	/// WARNING: Spirit is WILD and will attack the nearest target, even if it's the caster!
	/// CRIMINAL ACT: You will be flagged as criminal (gray) for casting this spell
	/// Use with extreme caution - summon away from yourself or near enemies
	/// </summary>
	/// <param name="p">Target location</param>
	public void Target(IPoint3D p)
		{
			Map map = Caster.Map;

			SpellHelper.GetSurfaceTop(ref p);

			if (map == null || !map.CanSpawnMobile(p.X, p.Y, p.Z))
			{
				DoFizzle();
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_LOCATION_BLOCKED);
				PlayFailureFeedback();
			}
		else if (SpellHelper.CheckTown(p, Caster) && CheckSequence())
		{
			try
			{
			// Create the blade spirits
			BladeSpirits spirit = new BladeSpirits();
			
			// Calculate duration based on caster skills
			TimeSpan duration = SpellHelper.NMSGetDuration(Caster, Caster, false);

	// Set spirit to evil karma (wild and dangerous)
	spirit.Karma = -2500;

// Summon the blade spirits at the target location
// Remains a proper summon (Summoned=true) but with IsWildSummon=true
// The wild flag bypasses all relationship protections in BaseAI targeting
SpellHelper.Summon(spirit, Caster, SUMMON_SOUND, duration, false, false);
		
		// Move spirit to target location
		spirit.MoveToWorld(new Point3D(p), Caster.Map);

	// Make spirit wild and aggressive - attacks nearest target (INCLUDING CASTER!)
	spirit.FightMode = FightMode.Closest;
	spirit.RangePerception = 10;  // Detects targets within 10 tiles
	spirit.RangeFight = 1;         // Engages in melee range
	
	// Mark as WILD SUMMON - bypasses all relationship protections in BaseAI
	// This allows spirit to attack:
	// - Summoner (caster)
	// - All players
	// - Party members
	// - Guild members
	// - Ignores karma
	// Wild summons are truly uncontrolled and dangerous!
	spirit.IsWildSummon = true;   // CRITICAL: Bypass ALL relationship checks (new flag system)
	spirit.Controlled = false;    // Not under control
	spirit.ControlMaster = null;  // No control master
	spirit.Team = 0;              // No team affiliation
		
		// Flag caster as criminal for summoning an uncontrolled hostile creature
		Caster.Criminal = true;

		// Register spirit for single-summon tracking (dismisses old one)
		RegisterSpirit(Caster, spirit);
			}
			catch (Exception)
			{
				// Silently fail if spirit instantiation fails
			}
		}

			FinishSequence();
		}

	/// <summary>
	/// Plays audio and visual feedback when spell fails
	/// </summary>
	private void PlayFailureFeedback()
	{
		Caster.PlaySound(Caster.Female ? FEMALE_OOPS_SOUND : MALE_OOPS_SOUND);
		Caster.Say(OOPS_MESSAGE);
	}

		private class InternalTarget : Target
		{
			private BladeSpiritsSpell m_Owner;

			public InternalTarget(BladeSpiritsSpell owner) : base(SpellConstants.GetSpellRange(), true, TargetFlags.None)
			{
				m_Owner = owner;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (o is IPoint3D)
					m_Owner.Target((IPoint3D)o);
			}

			/// <summary>
			/// Handles out of line-of-sight targeting
			/// Allows caster to retry the target
			/// </summary>
			/// <param name="from">Caster</param>
			/// <param name="o">Target object</param>
			protected override void OnTargetOutOfLOS(Mobile from, object o)
			{
				from.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
				from.Target = new InternalTarget(m_Owner);
				from.Target.BeginTimeout(from, TimeoutTime - DateTime.UtcNow);
				m_Owner = null;
			}

			protected override void OnTargetFinish(Mobile from)
			{
				if (m_Owner != null)
					m_Owner.FinishSequence();
			}
		}
	}
}

using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Eighth
{
	/// <summary>
	/// Energy Vortex - 8th Circle Magery Spell
	/// Summons a WILD and DANGEROUS Energy Vortex creature at target location.
	/// WARNING: Vortex is UNCONTROLLED and will attack the NEAREST target (including the caster!)
	/// Always evil karma - attacks anything within range
	/// CRIMINAL ACT: Casting this spell flags the caster as criminal (gray)
	/// LIMIT: Only 1 vortex per caster (new vortex dismisses old one)
	/// </summary>
	public class EnergyVortexSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Elemental Vortex", "Vas Corp Por",
				260,
				9032,
				false,
				Reagent.Bloodmoss,
				Reagent.BlackPearl,
				Reagent.MandrakeRoot,
				Reagent.Nightshade
			);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }

		#region Constants

		/// <summary>Multiplier for duration calculation based on Magery skill</summary>
		private const double DURATION_MULTIPLIER = 0.05; // Reduced to half (was 0.1)

		/// <summary>Effect ID for summon visual</summary>
		private const int SUMMON_EFFECT_ID = 0x212;

		/// <summary>Karma value for wild vortex (evil)</summary>
		private const int WILD_VORTEX_KARMA = -2500;

		/// <summary>Perception range for wild vortex (tiles)</summary>
		private const int VORTEX_PERCEPTION_RANGE = 10;

		/// <summary>Fight range for wild vortex (tiles)</summary>
		private const int VORTEX_FIGHT_RANGE = 1;

		#endregion

		#region Single Vortex Tracking

		/// <summary>
		/// Tracks the currently active Energy Vortex for each caster
		/// Only one vortex allowed per caster at a time
		/// </summary>
		private static Dictionary<Mobile, EnergyVortex> m_ActiveVortexes = new Dictionary<Mobile, EnergyVortex>();

		/// <summary>
		/// Registers a newly summoned vortex and dismisses any existing one
		/// </summary>
		private static void RegisterVortex(Mobile caster, EnergyVortex vortex)
		{
			// Dismiss old vortex if exists
			DismissExistingVortex(caster);

			// Register new vortex
			m_ActiveVortexes[caster] = vortex;
		}

		/// <summary>
		/// Dismisses the caster's existing Energy Vortex if present
		/// </summary>
		private static void DismissExistingVortex(Mobile caster)
		{
			EnergyVortex existingVortex;
			if (m_ActiveVortexes.TryGetValue(caster, out existingVortex))
			{
				if (existingVortex != null && !existingVortex.Deleted)
				{
					existingVortex.Delete();
					caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.INFO_PREVIOUS_SUMMON_DISMISSED);
				}
				m_ActiveVortexes.Remove(caster);
			}
		}

		/// <summary>
		/// Unregisters a vortex when it expires or dies
		/// </summary>
		public static void UnregisterVortex(Mobile caster, EnergyVortex vortex)
		{
			EnergyVortex registeredVortex;
			if (m_ActiveVortexes.TryGetValue(caster, out registeredVortex))
			{
				if (registeredVortex == vortex)
					m_ActiveVortexes.Remove(caster);
			}
		}

		#endregion

		public EnergyVortexSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if (!base.CheckCast(caster))
				return false;

			if (Caster.Followers >= Caster.FollowersMax)
			{
				DoFizzle();
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TOO_MANY_FOLLOWERS);
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
		}

		/// <summary>
		/// Handles summoning the Energy Vortex at target location
		/// </summary>
		public void Target(IPoint3D p)
		{
			Map map = Caster.Map;

			SpellHelper.GetSurfaceTop(ref p);

			if (map == null || !map.CanSpawnMobile(p.X, p.Y, p.Z))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_LOCATION_BLOCKED_SUMMON);
				FinishSequence();
				return;
			}

			if (SpellHelper.CheckTown(p, Caster) && CheckSequence())
			{
				TimeSpan duration = CalculateSummonDuration();
				Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, string.Format(Spell.SpellMessages.INFO_SUMMON_DURATION_FORMAT, duration.TotalSeconds));

				// Create the Energy Vortex
				EnergyVortex vortex = new EnergyVortex();
				
				// Store caster's EvalInt for dispel resistance calculation
				vortex.CasterEvalInt = Caster.Skills[SkillName.EvalInt].Value;
				
				// Store caster's Poisoning skill for variant selection
				vortex.CasterPoisoning = Caster.Skills[SkillName.Poisoning].Value;
				
				// Register vortex (dismisses any existing one)
				RegisterVortex(Caster, vortex);
				
				// Summon the vortex
				BaseCreature.Summon(vortex, false, Caster, new Point3D(p), SUMMON_EFFECT_ID, duration);
				
				// Configure as wild summon (uncontrolled and aggressive)
				ConfigureWildVortex(vortex);
				
				// Flag caster as criminal for summoning an uncontrolled hostile creature
				Caster.Criminal = true;
			}

			FinishSequence();
		}

		#region Helper Methods

		/// <summary>
		/// Calculates the duration for the summoned creature
		/// Duration = (Magery.Fixed × 0.1) × EvalInt_Bonus
		/// </summary>
		private TimeSpan CalculateSummonDuration()
		{
			return TimeSpan.FromSeconds((Caster.Skills[SkillName.Magery].Fixed * DURATION_MULTIPLIER) * NMSUtils.getDamageEvalBenefit(Caster));
		}

		/// <summary>
		/// Configures the vortex as a wild, uncontrolled summon
		/// </summary>
		private void ConfigureWildVortex(EnergyVortex vortex)
		{
			if (vortex == null)
				return;

			// Mark as WILD SUMMON - bypasses all relationship protections in BaseAI
			vortex.IsWildSummon = true;
			
			// Set fight mode to attack closest target
			vortex.FightMode = FightMode.Closest;
			
			// Set perception and fight ranges
			vortex.RangePerception = VORTEX_PERCEPTION_RANGE;
			vortex.RangeFight = VORTEX_FIGHT_RANGE;
			
			// Remove control
			vortex.Controlled = false;
			vortex.ControlMaster = null;
			vortex.Team = 0;
			
			// Set evil karma
			vortex.Karma = WILD_VORTEX_KARMA;
		}

		#endregion

		#region Internal Classes

		private class InternalTarget : Target
		{
			private EnergyVortexSpell m_Owner;

			public InternalTarget(EnergyVortexSpell owner) : base(SpellConstants.GetSpellRange(), true, TargetFlags.None)
			{
				m_Owner = owner;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (o is IPoint3D)
					m_Owner.Target((IPoint3D)o);
			}

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

		#endregion
	}
}

using System;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Spells.Eighth
{
	/// <summary>
	/// Base class for all 8th Circle Elemental Summon spells
	/// Centralizes common mechanics: duration calculation, greater elemental chance, followers check
	/// LIMIT: Only 1 elemental per caster (any type - Air, Earth, Fire, Water - dismisses previous)
	/// </summary>
	public abstract class BaseElementalSpell : MagerySpell
	{
		#region Single Elemental Tracking

		/// <summary>
		/// Tracks the currently active elemental for each caster
		/// Only one elemental allowed per caster at a time (any type: Air, Earth, Fire, Water)
		/// </summary>
		private static Dictionary<Mobile, BaseCreature> m_ActiveElementals = new Dictionary<Mobile, BaseCreature>();

		/// <summary>
		/// Registers a newly summoned elemental and dismisses any existing one
		/// Works for all elemental types: Air, Earth, Fire, Water (Regular or Greater)
		/// </summary>
		protected static void RegisterElemental(Mobile caster, BaseCreature elemental)
		{
			// Dismiss old elemental if exists
			DismissExistingElemental(caster);

			// Register new elemental
			m_ActiveElementals[caster] = elemental;
		}

		/// <summary>
		/// Dismisses the caster's existing elemental if present
		/// Works for any elemental type (Air, Earth, Fire, Water - Regular or Greater)
		/// Also cleans up if elemental expired naturally (was already deleted)
		/// </summary>
		protected static void DismissExistingElemental(Mobile caster)
		{
			BaseCreature existingElemental;
			if (m_ActiveElementals.TryGetValue(caster, out existingElemental))
			{
				if (existingElemental != null && !existingElemental.Deleted)
				{
					existingElemental.Delete();
					caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.INFO_PREVIOUS_SUMMON_DISMISSED);
				}
				// Always remove from dictionary (even if already deleted/expired)
				m_ActiveElementals.Remove(caster);
			}
		}

		/// <summary>
		/// Unregisters an elemental when it's deleted/expires
		/// Called from cleanup or when elemental dies naturally
		/// </summary>
		public static void UnregisterElemental(Mobile caster, BaseCreature elemental)
		{
			BaseCreature registeredElemental;
			if (m_ActiveElementals.TryGetValue(caster, out registeredElemental))
			{
				if (registeredElemental == elemental)
				{
					m_ActiveElementals.Remove(caster);
				}
			}
		}

		#endregion

		#region Constants

		/// <summary>Multiplier for duration calculation based on Magery skill</summary>
		protected const double DURATION_MULTIPLIER = 0.1;

		/// <summary>Skill threshold required for Greater Elemental summon chance (Magery and Inscription)</summary>
		protected const double SKILL_THRESHOLD_FOR_GREATER = 100.0;

		/// <summary>Base chance percentage for Greater Elemental (when requirements are met)</summary>
		protected const double GREATER_CHANCE_BASE = 20.0;

		/// <summary>EvalInt bonus multiplier per skill point (0.2% per point)</summary>
		protected const double EVALINT_BONUS_PER_POINT = 0.2;

		/// <summary>Effect ID for summon visual</summary>
		protected const int SUMMON_EFFECT_ID = 0x217;

		#endregion

		protected BaseElementalSpell(Mobile caster, Item scroll, SpellInfo info) : base(caster, scroll, info)
		{
		}

		/// <summary>
		/// Validates caster can summon an elemental
		/// Checks: base cast requirements and follower limit
		/// </summary>
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

		/// <summary>
		/// Calculates the duration for the summoned creature
		/// Formula: (Magery.Fixed * DURATION_MULTIPLIER) * NMSUtils.getBonusIncriptBenefit(Caster)
		/// </summary>
		protected TimeSpan CalculateSummonDuration()
		{
			return TimeSpan.FromSeconds((Caster.Skills[SkillName.Magery].Fixed * DURATION_MULTIPLIER) * NMSUtils.getBonusIncriptBenefit(Caster));
		}

		/// <summary>
		/// Determines if a Greater Elemental should be summoned
		/// Requires: Magery >= 100 AND Inscription >= 100
		/// Base chance: 20% + (EvalInt * 0.2%) per EvalInt point
		/// </summary>
		protected bool ShouldSummonGreaterElemental()
		{
			// Check requirements: Magery >= 100 AND Inscription >= 100
			if (Caster.Skills[SkillName.Magery].Value < SKILL_THRESHOLD_FOR_GREATER ||
			    Caster.Skills[SkillName.Inscribe].Value < SKILL_THRESHOLD_FOR_GREATER)
			{
				return false;
			}

			// Calculate chance: Base 20% + (EvalInt * 0.2%)
			double evalIntValue = Caster.Skills[SkillName.EvalInt].Value;
			double chance = GREATER_CHANCE_BASE + (evalIntValue * EVALINT_BONUS_PER_POINT);

			// Roll random 0-100 and check if it's below the chance percentage
			int roll = Utility.RandomMinMax(0, 100);
			return roll < chance;
		}

		/// <summary>
		/// Sends the duration message to the caster
		/// </summary>
		protected void SendDurationMessage(TimeSpan duration)
		{
			Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, string.Format(Spell.SpellMessages.INFO_SUMMON_DURATION_FORMAT, duration.TotalSeconds));
		}
	}
}


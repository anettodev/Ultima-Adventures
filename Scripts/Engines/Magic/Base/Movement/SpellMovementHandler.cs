using System;
using Server;
using Server.Mobiles;

namespace Server.Spells
{
	/// <summary>
	/// Handles movement and step tracking during spell casting
	/// </summary>
	public static class SpellMovementHandler
	{
		/// <summary>
		/// Calculates the number of steps allowed based on caster's Magery skill
		/// </summary>
		public static int CalculateAllowedStepsByMagery(double mageryValue)
		{
			if (mageryValue >= SpellConstants.MAGERY_SKILL_120)
			{
				return 20;
			}
			if (mageryValue >= SpellConstants.MAGERY_SKILL_110)
			{
				return 18;
			}
			if (mageryValue >= SpellConstants.MAGERY_SKILL_100)
			{
				return 16;
			}
			if (mageryValue >= SpellConstants.MAGERY_SKILL_90)
			{
				return 14;
			}
			if (mageryValue >= SpellConstants.MAGERY_SKILL_80)
			{
				return 12;
			}
			if (mageryValue >= SpellConstants.MAGERY_SKILL_70)
			{
				return 10;
			}
			if (mageryValue >= SpellConstants.MAGERY_SKILL_60)
			{
				return 8;
			}
			if (mageryValue >= SpellConstants.MAGERY_SKILL_50)
			{
				return 6;
			}
			if (mageryValue >= SpellConstants.MAGERY_SKILL_40)
			{
				return 4;
			}
			return SpellConstants.BASE_STEPS_ALLOWED;
		}

		/// <summary>
		/// Configures step tracking for PlayerMobile during casting
		/// </summary>
		public static void ConfigureAllowedSteps(Spell spell, PlayerMobile player)
		{
			int maxSteps = CalculateAllowedStepsByMagery(player.Skills[SkillName.Magery].Value);
			player.StepsAllowedForCastingSpells = maxSteps;

			string message = string.Format(Spell.SpellMessages.INFO_SPELL_UNSTABLE_AFTER_STEPS_FORMAT, maxSteps);
			player.SendMessage(SpellConstants.MSG_COLOR_SYSTEM, message);
		}

		/// <summary>
		/// Processes and deducts remaining steps allowed during casting
		/// </summary>
		public static bool ProcessRemainingSteps(PlayerMobile player, bool isRunning)
		{
			int stepCost = isRunning ? SpellConstants.RUNNING_STEP_COST : SpellConstants.WALKING_STEP_COST;
			player.StepsAllowedForCastingSpells -= stepCost;
			return true;
		}

		/// <summary>
		/// Validates if the spell has been held too long
		/// </summary>
		public static bool ValidateSpellHoldTime(Spell spell, DateTime startCastTime)
		{
			if (!(spell is MagerySpell))
			{
				return true;
			}

			MagerySpell magerySpell = (MagerySpell)spell;
			TimeSpan castDelay = spell.GetCastDelay();
			double maxHoldSeconds = CalculateMaxHoldSeconds(magerySpell.Circle, castDelay);
			double elapsedSeconds = (DateTime.UtcNow - startCastTime).TotalSeconds;

			if (elapsedSeconds > maxHoldSeconds)
			{
				NotifySpellLostConcentration(spell, elapsedSeconds);
				spell.DoFizzle();
				spell.Disturb(DisturbType.UseRequest);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Calculates maximum seconds a spell can be held before losing concentration
		/// </summary>
		private static double CalculateMaxHoldSeconds(SpellCircle circle, TimeSpan castDelay)
		{
			return SpellConstants.SPELL_HOLD_MAX_BASE - ((SpellConstants.SPELL_HOLD_CIRCLE_FACTOR * (int)circle) + castDelay.TotalSeconds);
		}

		/// <summary>
		/// Notifies caster they lost concentration
		/// </summary>
		private static void NotifySpellLostConcentration(Spell spell, double elapsedSeconds)
		{
			string message = string.Format(Spell.SpellMessages.ERROR_LOST_CONCENTRATION_FORMAT, Math.Truncate(elapsedSeconds));
			spell.Caster.SendMessage(message);
		}
	}
}


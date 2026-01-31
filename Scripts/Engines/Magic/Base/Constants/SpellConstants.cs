using System;

namespace Server.Spells
{
	/// <summary>
	/// Centralized constants for the spell system
	/// </summary>
	public static class SpellConstants
	{
		#region Timing Constants
		public const double NEXT_SPELL_DELAY_SECONDS = 0.75;
		public const double ANIMATE_DELAY_SECONDS = 1.5;
		public const int DEFAULT_CAST_RECOVERY_BASE = 6;
		public const int CAST_RECOVERY_PER_SECOND = 4;
		#endregion

		#region Damage Calculation Constants
		public const int BASE_DAMAGE_MULTIPLIER = 100;
		public const int INSCRIBE_DAMAGE_DIVISOR = 200;
		public const int INT_BONUS_DIVISOR = 10;
		public const int EVAL_SCALE_BASE = 30;
		public const int EVAL_SCALE_MULTIPLIER = 9;
		public const int EVAL_SCALE_DIVISOR = 100;
		#endregion

		#region Skill Thresholds for Movement
		public const double MAGERY_SKILL_40 = 40.0;
		public const double MAGERY_SKILL_50 = 50.0;
		public const double MAGERY_SKILL_60 = 60.0;
		public const double MAGERY_SKILL_70 = 70.0;
		public const double MAGERY_SKILL_80 = 80.0;
		public const double MAGERY_SKILL_90 = 90.0;
		public const double MAGERY_SKILL_100 = 100.0;
		public const double MAGERY_SKILL_110 = 110.0;
		public const double MAGERY_SKILL_120 = 120.0;
		#endregion

		#region Steps Allowed Constants
		public const int BASE_STEPS_ALLOWED = 2;
		public const int STEPS_PER_TIER = 2;
		public const int RUNNING_STEP_COST = 2;
		public const int WALKING_STEP_COST = 1;
		#endregion

		#region Drunk Mantra Constants
		public const double DRUNK_MANTRA_CHANCE_THRESHOLD = 0.85;
		public const int BAC_DIVISOR = 200;
		#endregion

		#region Message Colors
		public const int MSG_COLOR_SYSTEM = 95;
		public const int MSG_COLOR_ERROR = 55;
		public const int MSG_COLOR_WARNING = 33;
		public const int MSG_COLOR_HEAL = 68; // Light blue for healing

		#if DEBUG
		public const int MSG_COLOR_DEBUG_1 = 20;
		public const int MSG_COLOR_DEBUG_2 = 21;
		public const int MSG_COLOR_DEBUG_3 = 22;
		#endif
		#endregion

		#region Midland Lucidity Thresholds
		public const double MIDLAND_LUCIDITY_THRESHOLD_LOW = 0.50;
		public const double MIDLAND_LUCIDITY_THRESHOLD_MED = 0.70;
		public const double MIDLAND_LUCIDITY_THRESHOLD_HIGH = 0.90;
		public const double MIDLAND_LUCIDITY_DAMAGE_MULTIPLIER = 1.25;
		#endregion

		#region Time Constants
		/// <summary>Maximum base seconds a spell can be held before losing concentration (movement-based)</summary>
		public const double SPELL_HOLD_MAX_BASE = 6.0;
		
		/// <summary>Circle factor multiplier for spell hold time calculation</summary>
		public const double SPELL_HOLD_CIRCLE_FACTOR = 0.25;
		
		/// <summary>Maximum seconds a spell target can be held before timing out (targeting timeout)</summary>
		public const double SPELL_TARGET_TIMEOUT_SECONDS = 10.0;
		#endregion

		#region Mana/Reagent Caps
		public const int MIN_MANA_SCALAR = 10; // 10% minimum
		public const int MANA_SCALAR_DIVISOR = 100;
		public const double LRC_MANA_INCREASE_DIVISOR = 200.0;
		public const double LMC_MANA_DECREASE_DIVISOR = 100.0;
		#endregion

		#region Stat Mod Constants
		public const int INSCRIBE_MULTIPLIER = 1000;
		public const int PHYLACTERY_MULTIPLIER = 10;
		#endregion

		#region Pre-calculated TimeSpans
		public static readonly TimeSpan NextSpellDelay = TimeSpan.FromSeconds(NEXT_SPELL_DELAY_SECONDS);
		public static readonly TimeSpan AnimateDelay = TimeSpan.FromSeconds(ANIMATE_DELAY_SECONDS);
		#endregion

		#region Healing Constants
		public const double BASE_HEAL_DIVISOR = 3.0;
		public const double GREATER_HEAL_MULTIPLIER = 2.0; // 2x more powerful than Heal
		public const double HEAL_POWER_REDUCTION = 0.7; // 30% reduction (applies to both)
		public const double OTHER_TARGET_BONUS = 1.15; // 15% bonus (same for both)
		public const double RANDOM_VARIANCE_MIN = 0.02; // 2%
		public const double RANDOM_VARIANCE_MAX = 0.05; // 5%
		public const double CONSECUTIVE_CAST_PENALTY = 0.25; // 25%
		public const int MINIMUM_HEAL_AMOUNT = 1;
		
		// Skill bonus constants
		public const double INSCRIPTION_BONUS_PER_10 = 0.3; // 0.3 heal points per 10 Inscription
		public const int HEALING_SKILL_BONUS_PER_10 = 1; // 1 heal point per 10 Healing skill
		public const int SKILL_DIVISOR = 10;
		public const double SELF_HEAL_BONUS_PERCENT = 0.05; // 5% bonus when healing yourself (you know your body better)
		#endregion
		
		#region Cure Constants
		public const int LETHAL_POISON_LEVEL = 4;
		public const int CURE_SUCCESS_THRESHOLD_MULTIPLIER = 2;
		public const double ARCH_CURE_POWER_MULTIPLIER = 2.0; // 2x more powerful than Cure
		public const double HEALING_SKILL_CURE_BONUS_PER_10 = 1.0; // 1% per 10 Healing skill
		#endregion
		
		#region Damage Constants (Circle-Based)
		// 1st Circle
		public const int DAMAGE_1ST_BONUS = 2;
		public const int DAMAGE_1ST_DICE = 1;
		public const int DAMAGE_1ST_SIDES = 3;
		public const int DAMAGE_1ST_CAP = 8;
		
		// 2nd Circle
		public const int DAMAGE_2ND_BONUS = 4;
		public const int DAMAGE_2ND_DICE = 1;
		public const int DAMAGE_2ND_SIDES = 4;
		
		// 3rd Circle
		public const int DAMAGE_3RD_BONUS = 4;
		public const int DAMAGE_3RD_DICE = 1;
		public const int DAMAGE_3RD_SIDES = 6;
		
		// 4th Circle
		public const int DAMAGE_4TH_BONUS = 6; // INCREASED from 4
		public const int DAMAGE_4TH_DICE = 1;
		public const int DAMAGE_4TH_SIDES = 6;
		
		// Minimum Damage Floors (EvalInt-based)
		public const int MIN_DAMAGE_FLOOR_120 = 4;
		public const int MIN_DAMAGE_FLOOR_100 = 3;
		public const int MIN_DAMAGE_FLOOR_80 = 2;
		#endregion

		#region Spell Range Constants
		/// <summary>
		/// Standard spell target range for Mondain's Legacy (ML) mode
		/// </summary>
		public const int SPELL_RANGE_ML = 12;

		/// <summary>
		/// Standard spell target range for Legacy mode
		/// </summary>
		public const int SPELL_RANGE_LEGACY = 14;

		/// <summary>
		/// Gets the standard spell target range based on Core.ML setting
		/// </summary>
		/// <returns>12 if Core.ML is enabled, 14 otherwise</returns>
		public static int GetSpellRange()
		{
			return Core.ML ? SPELL_RANGE_ML : SPELL_RANGE_LEGACY;
		}
		#endregion
	}
}


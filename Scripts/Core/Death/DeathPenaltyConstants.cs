using System;

namespace Server.Misc
{
    /// <summary>
    /// Centralized constants for death penalty calculations and mechanics.
    /// Extracted from DeathPenalty.cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class DeathPenaltyConstants
    {
        #region Base Loss Rates

        /// <summary>Base karma and fame loss rate (5%)</summary>
        public const double BASE_KARMA_LOSS_RATE = 0.05;

        /// <summary>Base balance effect rate (5%)</summary>
        public const double BASE_BALANCE_EFFECT = 0.05;

        /// <summary>Minimum karma loss rate cap (5%)</summary>
        public const double MIN_KARMA_LOSS_RATE = 0.05;

        #endregion

        #region Multipliers

        /// <summary>Multiplier for normal (non-avatar) players (2x)</summary>
        public const int NORMAL_PLAYER_MULTIPLIER = 2;

        /// <summary>Multiplier for avatar players with allPenalty flag (2x)</summary>
        public const int AVATAR_ALL_PENALTY_MULTIPLIER = 2;

        /// <summary>Multiplier for normal players with allPenalty flag (4x)</summary>
        public const int NORMAL_PLAYER_ALL_PENALTY_MULTIPLIER = 4;

        /// <summary>Multiplier for ankh resurrection (2x)</summary>
        public const int ANKH_MULTIPLIER = 2;

        /// <summary>Multiplier for skill loss calculation with allPenalty (3x) - Currently unused</summary>
        public const int SKILL_LOSS_MULTIPLIER = 3;

        /// <summary>Multiplier for balance effect with allPenalty (3x)</summary>
        public const int BALANCE_EFFECT_MULTIPLIER = 3;

        /// <summary>Divisor for balance calculation (1.5)</summary>
        public const double BALANCE_CALCULATION_DIVISOR = 1.5;

        #endregion

        #region Thresholds and Caps

        /// <summary>Minimum karma value for balance effect calculations (1000)</summary>
        public const int MIN_KARMA_FOR_BALANCE = 1000;

        /// <summary>Total stats threshold for exemption from penalties (125)</summary>
        public const int TOTAL_STATS_EXEMPTION_THRESHOLD = 125;

        /// <summary>Maximum balance level value (100000)</summary>
        public const int BALANCE_LEVEL_MAX = 100000;

        /// <summary>Karma divisor for balance calculations (15000)</summary>
        public const int KARMA_DIVISOR = 15000;

        /// <summary>Balance level divisor for skill loss calculations (200000) - Currently unused</summary>
        public const int BALANCE_LEVEL_DIVISOR = 200000;

        /// <summary>Minimum skill base value for loss calculations (35) - Currently unused</summary>
        public const double MIN_SKILL_BASE_FOR_LOSS = 35.0;

        /// <summary>Minimum stat value (10) - Currently unused</summary>
        public const int MIN_STAT_VALUE = 10;

        /// <summary>Percentage base for calculations (100)</summary>
        public const int PERCENTAGE_BASE = 100;

        /// <summary>Percentage base for double calculations (100.0)</summary>
        public const double PERCENTAGE_BASE_DOUBLE = 100.0;

        /// <summary>Maximum skill loss rate cap (0.999 = 99.9%) - Currently unused</summary>
        public const double MAX_SKILL_LOSS_RATE = 0.999;

        /// <summary>No skill loss value (1.0 = 100%)</summary>
        public const double NO_SKILL_LOSS = 1.0;

        #endregion

        #region Random Thresholds (For Reference - Currently Unused)

        /// <summary>Chance threshold for stat loss (33%) - Currently unused</summary>
        public const double STAT_LOSS_CHANCE = 0.33;

        /// <summary>Chance threshold for skill loss with allPenalty (35%) - Currently unused</summary>
        public const double SKILL_LOSS_CHANCE_ALL_PENALTY = 0.35;

        /// <summary>Chance threshold for skill loss without allPenalty (65%) - Currently unused</summary>
        public const double SKILL_LOSS_CHANCE_NORMAL = 0.65;

        #endregion

        #region Message Colors

        /// <summary>Message color for warnings (55 = red/orange)</summary>
        public const int MSG_COLOR_WARNING = 55;

        #endregion
    }
}


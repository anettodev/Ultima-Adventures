using System;

namespace Server.Misc
{
    /// <summary>
    /// Centralized constants for resurrection gump calculations and mechanics.
    /// Extracted from ResurrectNowGump.cs and ResurrectCostGump.cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class ResurrectionGumpConstants
    {
        #region Penalty Calculation (ResurrectNowGump)

        /// <summary>Maximum penalty cap for ResurrectNowGump (0.999 = 99.9%)</summary>
        public const double PENALTY_CAP_RESURRECT_NOW = 0.999;

        /// <summary>Stat loss multiplier for display in ResurrectNowGump message (300%) - Currently unused but shown in message</summary>
        public const int STAT_LOSS_MULTIPLIER_DISPLAY = 300;

        #endregion

        #region Penalty Calculation (ResurrectCostGump)

        /// <summary>Minimum karma value for ResurrectCostGump penalty calculation (5000)</summary>
        public const int MIN_KARMA_FOR_COST_GUMP = 5000;

        /// <summary>Minimum penalty cap for ResurrectCostGump (0.1 = 10%)</summary>
        public const double MIN_PENALTY_CAP_COST_GUMP = 0.1;

        /// <summary>Penalty multiplier when player has no gold (3x)</summary>
        public const int PENALTY_MULTIPLIER_NO_GOLD = 3;

        #endregion

        #region Fame/Karma Loss Percentages

        /// <summary>Base fame/karma loss percentage for avatar players (5%)</summary>
        public const int FAME_KARMA_LOSS_BASE = 5;

        /// <summary>Base fame/karma loss percentage for normal players (10%)</summary>
        public const int FAME_KARMA_LOSS_NORMAL_BASE = 10;

        /// <summary>Fame/karma loss percentage for normal players with strong stats (>125) in ResurrectNowGump (30%)</summary>
        public const int FAME_KARMA_LOSS_NORMAL_STRONG = 30;

        /// <summary>Fame/karma loss percentage for normal players with weak stats (<125) in ResurrectNowGump (15%)</summary>
        public const int FAME_KARMA_LOSS_NORMAL_WEAK = 15;

        /// <summary>Fame/karma loss percentage for normal players with weak stats and no gold (20%)</summary>
        public const int FAME_KARMA_LOSS_NORMAL_NO_GOLD_WEAK = 20;

        /// <summary>Fame/karma loss percentage for normal players with strong stats and no gold (40%)</summary>
        public const int FAME_KARMA_LOSS_NORMAL_NO_GOLD_STRONG = 40;

        /// <summary>Fame/karma loss percentage for avatar players at shrine (20%)</summary>
        public const int FAME_KARMA_LOSS_AVATAR_SHRINE = 20;

        #endregion

        #region Healer Types

        /// <summary>Regular healer type (NPC healer)</summary>
        public const int HEALER_TYPE_REGULAR = 0;

        /// <summary>Shrine healer type (2)</summary>
        public const int HEALER_TYPE_SHRINE = 2;

        /// <summary>Azrael healer type (3)</summary>
        public const int HEALER_TYPE_AZRAEL = 3;

        /// <summary>Reaper healer type (4)</summary>
        public const int HEALER_TYPE_REAPER = 4;

        /// <summary>Goddess of the sea healer type (5)</summary>
        public const int HEALER_TYPE_GODDESS_SEA = 5;

        #endregion

        #region Resurrection Types

        /// <summary>No resurrection type (0)</summary>
        public const int RESURRECT_TYPE_NONE = 0;

        /// <summary>Resurrection without gold payment (1)</summary>
        public const int RESURRECT_TYPE_NO_GOLD = 1;

        /// <summary>Resurrection with gold payment (2)</summary>
        public const int RESURRECT_TYPE_WITH_GOLD = 2;

        /// <summary>Resurrection at shrine (3)</summary>
        public const int RESURRECT_TYPE_SHRINE = 3;

        #endregion

        #region Gump Layout

        /// <summary>Gump X position for ResurrectNowGump (50)</summary>
        public const int GUMP_POS_X_NOW = 50;

        /// <summary>Gump Y position for ResurrectNowGump (20)</summary>
        public const int GUMP_POS_Y_NOW = 20;

        /// <summary>Gump X position for ResurrectCostGump (150)</summary>
        public const int GUMP_POS_X_COST = 150;

        /// <summary>Gump Y position for ResurrectCostGump (50)</summary>
        public const int GUMP_POS_Y_COST = 50;

        /// <summary>First column X position (100)</summary>
        public const int FIRST_COLUMN_X = 100;

        /// <summary>Second column X position (307)</summary>
        public const int SECOND_COLUMN_X = 307;

        /// <summary>Button label X offset (30)</summary>
        public const int BUTTON_LABEL_OFFSET = 30;

        #endregion

        #region Bank Gold Formatting

        /// <summary>Threshold for using 'k' notation (1000)</summary>
        public const int K_NOTATION_THRESHOLD = 1000;

        /// <summary>Threshold for using 'kk' notation (1000000)</summary>
        public const int KK_NOTATION_THRESHOLD = 1000000;

        /// <summary>Divisor for 'k' notation (1000)</summary>
        public const int K_DIVISOR = 1000;

        /// <summary>Divisor for 'kk' notation (1000000)</summary>
        public const int KK_DIVISOR = 1000000;

        /// <summary>Number of decimal places for rounding (2)</summary>
        public const int ROUNDING_DECIMALS = 2;

        #endregion

        #region Auto-Resurrect

        /// <summary>Delay in seconds before showing auto-resurrect gump (60)</summary>
        public const int AUTO_RESURRECT_DELAY_SECONDS = 60;

        #endregion

        #region Effects

        /// <summary>Sound ID for resurrection (0x214)</summary>
        public const int SOUND_RESURRECTION = 0x214;

        /// <summary>Effect ID for resurrection (0x376A)</summary>
        public const int EFFECT_RESURRECTION = 0x376A;

        /// <summary>Effect speed parameter (10)</summary>
        public const int EFFECT_SPEED = 10;

        /// <summary>Effect duration parameter (16)</summary>
        public const int EFFECT_DURATION = 16;

        #endregion

        #region Map Check

        /// <summary>Size parameter for map fit check (16)</summary>
        public const int MAP_FIT_CHECK_SIZE = 16;

        #endregion

        #region Message Colors

        /// <summary>Message color for warnings (55 = red/orange)</summary>
        public const int MSG_COLOR_WARNING = 55;

        #endregion

        #region Random Message Ranges

        /// <summary>Minimum random value for grave messages (0)</summary>
        public const int GRAVE_MESSAGE_MIN = 0;

        /// <summary>Maximum random value for grave messages (3)</summary>
        public const int GRAVE_MESSAGE_MAX = 3;

        #endregion

        #region Resurrection Debit System

        /// <summary>Base delay in seconds per debit count (10 seconds)</summary>
        public const int DEBIT_DELAY_BASE_SECONDS = 10;

        /// <summary>Maximum delay in seconds (300 seconds = 5 minutes)</summary>
        public const int DEBIT_DELAY_MAX_SECONDS = 300;

        /// <summary>Maximum debit count before delay caps (30)</summary>
        public const int DEBIT_COUNT_MAX = 30;

        /// <summary>Gold amount per debt (300 gold pieces)</summary>
        public const int DEBT_GOLD_PER_DEBIT = 300;

        /// <summary>Check interval in seconds for resurrection delay timer (7 seconds)</summary>
        public const int RESURRECTION_DELAY_CHECK_INTERVAL_SECONDS = 7;

        #endregion

        #region Resurrection Delay Particle Effects

        /// <summary>Particle effect ID for resurrection delay (yellow particles)</summary>
        public const int RESURRECTION_DELAY_PARTICLE_EFFECT = 0x375A;

        /// <summary>Particle hue for resurrection delay (yellow - 0x35)</summary>
        public const int RESURRECTION_DELAY_PARTICLE_HUE = 0x35;

        /// <summary>Particle speed for resurrection delay</summary>
        public const int RESURRECTION_DELAY_PARTICLE_SPEED = 10;

        /// <summary>Particle duration for resurrection delay</summary>
        public const int RESURRECTION_DELAY_PARTICLE_DURATION = 20;

        #endregion
    }
}


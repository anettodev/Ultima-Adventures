using System;

namespace Server.Mobiles
{
    /// <summary>
    /// Centralized constants for MineSpirit configuration.
    /// Extracted from MineSpirit.cs to improve maintainability and reduce magic numbers.
    /// </summary>
    public static class MineSpiritConstants
    {
        #region Range Configuration

        /// <summary>Default detection range for MineSpirit</summary>
        public const int DEFAULT_RANGE = 3;

        /// <summary>Minimum detection range</summary>
        public const int RANGE_MIN = 0;

        /// <summary>Maximum detection range</summary>
        public const int RANGE_MAX = 3;

        #endregion

        #region Skill Configuration

        /// <summary>Default required skill for mining</summary>
        public const double DEFAULT_REQ_SKILL = 50.0;

        /// <summary>Default minimum skill for mining</summary>
        public const double DEFAULT_MIN_SKILL = 50.0;

        /// <summary>Default maximum skill for mining</summary>
        public const double DEFAULT_MAX_SKILL = 120.0;

        /// <summary>Minimum skill value allowed</summary>
        public const double SKILL_MIN = 0.0;

        /// <summary>Maximum skill value allowed</summary>
        public const double SKILL_MAX = 120.0;

        #endregion

        #region Vein Configuration

        /// <summary>Chance percentage for custom ore vein</summary>
        public const double CUSTOM_VEIN_CHANCE = 50.0;

        /// <summary>Rarity modifier for custom ore vein</summary>
        public const double CUSTOM_VEIN_RARITY = 0.5;

        /// <summary>Chance percentage for fallback iron vein</summary>
        public const double FALLBACK_VEIN_CHANCE = 50.0;

        /// <summary>Rarity modifier for fallback iron vein</summary>
        public const double FALLBACK_VEIN_RARITY = 0.0;

        #endregion

        #region Fallback Resource Configuration

        /// <summary>Required skill for fallback iron resource</summary>
        public const double FALLBACK_REQ_SKILL = 0.0;

        /// <summary>Minimum skill for fallback iron resource</summary>
        public const double FALLBACK_MIN_SKILL = 0.0;

        /// <summary>Maximum skill for fallback iron resource</summary>
        public const double FALLBACK_MAX_SKILL = 100.0;

        #endregion

        #region String Formatting

        /// <summary>Length of ore type name suffix to remove (e.g., "Ore" from "IronOre")</summary>
        public const int ORE_NAME_SUFFIX_LENGTH = 3;

        #endregion
    }
}


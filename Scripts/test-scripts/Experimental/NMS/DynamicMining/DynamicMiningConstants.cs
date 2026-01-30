using System;

namespace Server.Engines.Harvest
{
    /// <summary>
    /// Centralized constants for DynamicMining system.
    /// Extracted from DynamicMining.cs to improve maintainability and reduce magic numbers.
    /// </summary>
    public static class DynamicMiningConstants
    {
        #region MineSpirit Detection

        /// <summary>Search radius in tiles for finding nearby MineSpirit instances</summary>
        public const int MINE_SPIRIT_SEARCH_RANGE = 5;

        #endregion

        #region Bank Configuration

        /// <summary>Minimum total ore per bank for dynamic mining</summary>
        public const int DYNAMIC_ORE_BANK_MIN_TOTAL = 4;

        /// <summary>Maximum total ore per bank for dynamic mining</summary>
        public const int DYNAMIC_ORE_BANK_MAX_TOTAL = 28;

        /// <summary>Minimum respawn time in minutes for dynamic mining banks</summary>
        public const double DYNAMIC_ORE_RESPAWN_MIN_MINUTES = 10.0;

        /// <summary>Maximum respawn time in minutes for dynamic mining banks</summary>
        public const double DYNAMIC_ORE_RESPAWN_MAX_MINUTES = 30.0;

        #endregion

        #region Effect Configuration

        /// <summary>Effect action ID for digging animation</summary>
        public const int DYNAMIC_EFFECT_ACTION_ID = 11;

        /// <summary>Effect count for digging animation</summary>
        public const int DYNAMIC_EFFECT_COUNT = 1;

        #endregion

        #region Bonus Resource Skill Requirements

        /// <summary>Skill requirement for scroll and map bonuses</summary>
        public const double BONUS_SCROLL_MAP_SKILL_REQ = 60.0;

        /// <summary>Skill requirement for amber bonus</summary>
        public const double BONUS_AMBER_SKILL_REQ = 70.0;

        /// <summary>Skill requirement for amethyst and citrine bonuses</summary>
        public const double BONUS_GEM_SKILL_REQ = 75.0;

        /// <summary>Skill requirement for diamond bonus</summary>
        public const double BONUS_DIAMOND_SKILL_REQ = 80.0;

        /// <summary>Skill requirement for emerald, ruby, and sapphire bonuses</summary>
        public const double BONUS_HIGH_GEM_SKILL_REQ = 85.0;

        /// <summary>Skill requirement for star sapphire and tourmaline bonuses</summary>
        public const double BONUS_RARE_GEM_SKILL_REQ = 90.0;

        /// <summary>Skill requirement for legendary gem bonuses</summary>
        public const double BONUS_LEGENDARY_GEM_SKILL_REQ = 100.0;

        #endregion

        #region Localized Message IDs

        /// <summary>Where do you wish to dig? message</summary>
        public const int MSG_WHERE_TO_DIG = 503033;

        /// <summary>You can't mine while riding message</summary>
        public const int MSG_CANNOT_MINE_RIDING = 501864;

        /// <summary>You can't mine while polymorphed message</summary>
        public const int MSG_CANNOT_MINE_POLYMORPHED = 501865;

        /// <summary>You can't mine there message</summary>
        public const int MSG_CANNOT_MINE_THERE = 501862;

        /// <summary>You can't mine that message</summary>
        public const int MSG_CANNOT_MINE_THAT = 501863;

        #endregion
    }
}


using System;

namespace Server.Engines.Craft
{
    /// <summary>
    /// Centralized constants for cartography crafting system.
    /// Extracted from DefCartography.cs to improve maintainability and reduce magic numbers.
    /// </summary>
    public static class CartographyConstants
    {
        #region Sound Effects

        /// <summary>Sound played when performing cartography crafting</summary>
        public const int SOUND_CRAFTING = 0x249;

        #endregion

        #region Localized Messages

        /// <summary>Gump title for cartography menu</summary>
        public const int GUMP_TITLE = 1044008;

        /// <summary>Message when tool is worn out</summary>
        public const int MSG_TOOL_WORN_OUT = 1044038;

        /// <summary>Message when tool is not accessible</summary>
        public const int MSG_TOOL_NOT_ACCESSIBLE = 1044263;

        /// <summary>Message when crafting fails with material loss</summary>
        public const int MSG_FAILED_MATERIAL_LOSS = 1044043;

        /// <summary>Message when crafting fails without material loss</summary>
        public const int MSG_FAILED_NO_MATERIAL_LOSS = 1044157;

        /// <summary>Message for below average quality</summary>
        public const int MSG_BELOW_AVERAGE = 502785;

        /// <summary>Message for exceptional quality with maker's mark</summary>
        public const int MSG_EXCEPTIONAL_WITH_MARK = 1044156;

        /// <summary>Message for exceptional quality</summary>
        public const int MSG_EXCEPTIONAL = 1044155;

        /// <summary>Message for normal quality</summary>
        public const int MSG_NORMAL = 1044154;

        #endregion

        #region Craft Categories

        /// <summary>Blank scrolls category name</summary>
        public const int CATEGORY_BLANK_SCROLLS = 1044294;

        /// <summary>Maps category name</summary>
        public const int CATEGORY_MAPS = 1044448;

        #endregion

        #region Craft Item Names (Cliloc IDs)

        /// <summary>Blank scroll item name</summary>
        public const int ITEM_BLANK_SCROLL = 1044377;

        #endregion

        #region Skill Ranges

        /// <summary>Minimum skill for blank scrolls</summary>
        public const double SKILL_MIN_BLANK_SCROLL = 40.0;

        /// <summary>Maximum skill for blank scrolls</summary>
        public const double SKILL_MAX_BLANK_SCROLL = 70.0;

        /// <summary>Minimum skill for small maps</summary>
        public const double SKILL_MIN_SMALL_MAP = 10.0;

        /// <summary>Maximum skill for small maps</summary>
        public const double SKILL_MAX_SMALL_MAP = 70.0;

        /// <summary>Minimum skill for large maps</summary>
        public const double SKILL_MIN_LARGE_MAP = 25.0;

        /// <summary>Maximum skill for large maps</summary>
        public const double SKILL_MAX_LARGE_MAP = 85.0;

        /// <summary>Minimum skill for sea charts</summary>
        public const double SKILL_MIN_SEA_CHART = 35.0;

        /// <summary>Maximum skill for sea charts</summary>
        public const double SKILL_MAX_SEA_CHART = 95.0;

        /// <summary>Minimum skill for huge maps</summary>
        public const double SKILL_MIN_HUGE_MAP = 39.5;

        /// <summary>Maximum skill for huge maps</summary>
        public const double SKILL_MAX_HUGE_MAP = 99.5;

        /// <summary>Minimum skill for world maps</summary>
        public const double SKILL_MIN_WORLD_MAP = 89.5;

        /// <summary>Maximum skill for world maps</summary>
        public const double SKILL_MAX_WORLD_MAP = 110.5;

        #endregion

        #region Resources

        /// <summary>Bark fragment resource name</summary>
        public const int RESOURCE_BARK_FRAGMENT = 1073477;

        /// <summary>Bark fragment not found message</summary>
        public const int RESOURCE_BARK_FRAGMENT_NOT_FOUND = 1073478;

        /// <summary>Blank map resource name</summary>
        public const int RESOURCE_BLANK_MAP = 1044449;

        /// <summary>Blank map not found message</summary>
        public const int RESOURCE_BLANK_MAP_NOT_FOUND = 1044450;

        #endregion

        #region Resource Amounts

        /// <summary>Amount of bark fragment required for blank scrolls</summary>
        public const int BARK_FRAGMENT_AMOUNT = 1;

        /// <summary>Amount of blank map required for maps</summary>
        public const int BLANK_MAP_AMOUNT = 1;

        #endregion

        #region System Parameters

        /// <summary>Craft system minimum skill parameter</summary>
        public const int CRAFT_SYSTEM_MIN_SKILL = 1;

        /// <summary>Craft system maximum skill parameter</summary>
        public const int CRAFT_SYSTEM_MAX_SKILL = 1;

        /// <summary>Craft system delay multiplier</summary>
        public const double CRAFT_SYSTEM_DELAY = 1.25;

        #endregion
    }
}

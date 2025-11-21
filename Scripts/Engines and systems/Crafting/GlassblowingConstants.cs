using System;

namespace Server.Engines.Craft
{
    /// <summary>
    /// Centralized constants for glassblowing crafting system.
    /// Extracted from DefGlassblowing.cs to improve maintainability and reduce magic numbers.
    /// </summary>
    public static class GlassblowingConstants
    {
        #region Skill Requirements

        /// <summary>Minimum Alchemy skill required to use glassblowing</summary>
        public const double REQUIRED_ALCHEMY_SKILL = 100.0;

        #endregion

        #region Sound Effects

        /// <summary>Sound played when using bellows during glassblowing</summary>
        public const int SOUND_BELLOWS = 0x2B;

        /// <summary>Sound played when hitting the anvil</summary>
        public const int SOUND_ANVIL_HIT = 0x2A;

        /// <summary>Sound played when glass item is successfully crafted</summary>
        public const int SOUND_GLASS_BREAKING = 0x41;

        #endregion

        #region Localized Messages

        /// <summary>Gump title for glassblowing menu</summary>
        public const int GUMP_TITLE = 1044622;

        /// <summary>Message when tool is worn out</summary>
        public const int MSG_TOOL_WORN_OUT = 1044038;

        /// <summary>Message when wrong tool is equipped</summary>
        public const int MSG_WRONG_TOOL_EQUIPPED = 1048146;

        /// <summary>Message when player hasn't learned glassblowing</summary>
        public const int MSG_NOT_LEARNED_GLASSBLOWING = 1044634;

        /// <summary>Message when tool is not accessible</summary>
        public const int MSG_TOOL_NOT_ACCESSIBLE = 1044263;

        /// <summary>Message when not near a forge</summary>
        public const int MSG_MUST_BE_NEAR_FORGE = 1044628;

        /// <summary>Message when crafting fails with material loss</summary>
        public const int MSG_FAILED_MATERIAL_LOSS = 1044043;

        /// <summary>Message when crafting fails without material loss</summary>
        public const int MSG_FAILED_NO_MATERIAL_LOSS = 1044157;

        /// <summary>Message for exceptional quality with maker's mark</summary>
        public const int MSG_EXCEPTIONAL_WITH_MARK = 1044156;

        /// <summary>Message for exceptional quality</summary>
        public const int MSG_EXCEPTIONAL = 1044155;

        /// <summary>Message for normal quality</summary>
        public const int MSG_NORMAL = 1044154;

        /// <summary>Message for below average quality</summary>
        public const int MSG_BELOW_AVERAGE = 502785;

        #endregion

        #region Craft Item Names (Cliloc IDs)

        /// <summary>Bottle item name</summary>
        public const int ITEM_BOTTLE = 1023854;

        /// <summary>Small flask item name</summary>
        public const int ITEM_SMALL_FLASK = 1044610;

        /// <summary>Medium flask item name</summary>
        public const int ITEM_MEDIUM_FLASK = 1044611;

        /// <summary>Curved flask item name</summary>
        public const int ITEM_CURVED_FLASK = 1044612;

        /// <summary>Long flask item name</summary>
        public const int ITEM_LONG_FLASK = 1044613;

        /// <summary>Large flask item name</summary>
        public const int ITEM_LARGE_FLASK = 1044623;

        /// <summary>Animated small blue flask item name</summary>
        public const int ITEM_ANI_SMALL_BLUE_FLASK = 1044614;

        /// <summary>Animated large violet flask item name</summary>
        public const int ITEM_ANI_LARGE_VIOLET_FLASK = 1044615;

        /// <summary>Animated red ribbed flask item name</summary>
        public const int ITEM_ANI_RED_RIBBED_FLASK = 1044624;

        /// <summary>Empty vials with rack item name</summary>
        public const int ITEM_EMPTY_VIALS_RACK = 1044616;

        /// <summary>Full vials with rack item name</summary>
        public const int ITEM_FULL_VIALS_RACK = 1044617;

        /// <summary>Spinning hourglass item name</summary>
        public const int ITEM_SPINNING_HOURGLASS = 1044618;

        #endregion

        #region Skill Ranges

        /// <summary>Minimum skill for basic glass items (bottle, jar)</summary>
        public const double SKILL_MIN_BASIC = 52.5;

        /// <summary>Maximum skill for basic glass items (bottle, jar)</summary>
        public const double SKILL_MAX_BASIC = 102.5;

        /// <summary>Minimum skill for monocle</summary>
        public const double SKILL_MIN_MONOCLE = 5.0;

        /// <summary>Maximum skill for monocle</summary>
        public const double SKILL_MAX_MONOCLE = 55.0;

        /// <summary>Minimum skill for small/medium flasks</summary>
        public const double SKILL_MIN_SMALL_MEDIUM_FLASK = 52.5;

        /// <summary>Maximum skill for small/medium flasks</summary>
        public const double SKILL_MAX_SMALL_MEDIUM_FLASK = 102.5;

        /// <summary>Minimum skill for curved flask</summary>
        public const double SKILL_MIN_CURVED_FLASK = 55.0;

        /// <summary>Maximum skill for curved flask</summary>
        public const double SKILL_MAX_CURVED_FLASK = 105.0;

        /// <summary>Minimum skill for long flask</summary>
        public const double SKILL_MIN_LONG_FLASK = 57.5;

        /// <summary>Maximum skill for long flask</summary>
        public const double SKILL_MAX_LONG_FLASK = 107.5;

        /// <summary>Minimum skill for advanced flasks</summary>
        public const double SKILL_MIN_ADVANCED_FLASK = 60.0;

        /// <summary>Maximum skill for advanced flasks</summary>
        public const double SKILL_MAX_ADVANCED_FLASK = 110.0;

        /// <summary>Minimum skill for vials and hourglass</summary>
        public const double SKILL_MIN_VIALS_HOURGLASS = 65.0;

        /// <summary>Maximum skill for vials and hourglass</summary>
        public const double SKILL_MAX_VIALS_HOURGLASS = 115.0;

        /// <summary>Minimum skill for spinning hourglass</summary>
        public const double SKILL_MIN_SPINNING_HOURGLASS = 75.0;

        /// <summary>Maximum skill for spinning hourglass</summary>
        public const double SKILL_MAX_SPINNING_HOURGLASS = 125.0;

        #endregion

        #region Resource Amounts

        /// <summary>Sand amount for basic items (bottle, jar)</summary>
        public const int SAND_AMOUNT_BASIC = 1;

        /// <summary>Sand amount for small flask</summary>
        public const int SAND_AMOUNT_SMALL_FLASK = 2;

        /// <summary>Sand amount for medium flask</summary>
        public const int SAND_AMOUNT_MEDIUM_FLASK = 3;

        /// <summary>Sand amount for curved flask</summary>
        public const int SAND_AMOUNT_CURVED_FLASK = 2;

        /// <summary>Sand amount for long flask</summary>
        public const int SAND_AMOUNT_LONG_FLASK = 4;

        /// <summary>Sand amount for large/advanced flasks</summary>
        public const int SAND_AMOUNT_LARGE_FLASK = 5;

        /// <summary>Sand amount for animated red ribbed flask</summary>
        public const int SAND_AMOUNT_RED_RIBBED_FLASK = 7;

        /// <summary>Sand amount for empty vials rack</summary>
        public const int SAND_AMOUNT_EMPTY_VIALS = 8;

        /// <summary>Sand amount for full vials rack</summary>
        public const int SAND_AMOUNT_FULL_VIALS = 9;

        /// <summary>Sand amount for spinning hourglass</summary>
        public const int SAND_AMOUNT_SPINNING_HOURGLASS = 10;

        #endregion

        #region Resources

        /// <summary>Sand resource name</summary>
        public const int RESOURCE_SAND = 1044625;

        /// <summary>Sand resource not found message</summary>
        public const int RESOURCE_SAND_NOT_FOUND = 1044627;

        #endregion

        #region System Parameters

        /// <summary>Craft system minimum skill parameter</summary>
        public const int CRAFT_SYSTEM_MIN_SKILL = 1;

        /// <summary>Craft system maximum skill parameter</summary>
        public const int CRAFT_SYSTEM_MAX_SKILL = 1;

        /// <summary>Craft system delay multiplier</summary>
        public const double CRAFT_SYSTEM_DELAY = 1.25;

        /// <summary>Timer delay for sound synchronization</summary>
        public const double TIMER_DELAY_SECONDS = 0.7;

        /// <summary>Distance for blacksmithy forge check</summary>
        public const int FORGE_CHECK_DISTANCE = 2;

        #endregion
    }
}

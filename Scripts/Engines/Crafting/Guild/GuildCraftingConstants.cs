using System;

namespace Server.Items
{
    /// <summary>
    /// Centralized constants for Guild Crafting system.
    /// Extracted from multiple files to improve maintainability and eliminate magic numbers.
    /// </summary>
    public static class GuildCraftingConstants
    {
        #region Proximity and Range

        /// <summary>Range in tiles to check for guildmaster or shoppe</summary>
        public const int GUILDMASTER_PROXIMITY_RANGE = 20;

        #endregion

        #region Skill Requirements

        /// <summary>Minimum skill level required to use guild tools</summary>
        public const double MIN_SKILL_REQUIRED = 90.0;

        /// <summary>Elder skill level that bypasses guild membership requirement for some guilds</summary>
        public const double ELDER_SKILL_BYPASS = 110.0;

        /// <summary>Minimum fletching skill for archers guild (slightly different requirement)</summary>
        public const double MIN_FLETCHING_SKILL = 100.0;

        #endregion

        #region Tool Properties

        /// <summary>Weight of heavy guild tools (hammers, carpentry tools, fletching tools, tinkering tools)</summary>
        public const double TOOL_WEIGHT_HEAVY = 5.0;

        /// <summary>Weight of light guild tools (sewing kits)</summary>
        public const double TOOL_WEIGHT_LIGHT = 2.0;

        /// <summary>Weight of very light guild tools (scribe pens)</summary>
        public const double TOOL_WEIGHT_VERY_LIGHT = 1.0;

        /// <summary>Hue color for all extraordinary guild tools</summary>
        public const int EXTRAORDINARY_TOOL_HUE = 0x430;

        #endregion

        #region Tool Item IDs

        /// <summary>Item ID for Guild Carpentry tools</summary>
        public const int ITEMID_CARPENTRY_TOOLS = 0x1EBA;

        /// <summary>Item ID for Guild Hammer</summary>
        public const int ITEMID_SMITHING_HAMMER = 0xFB5;

        /// <summary>Item ID for Guild Tinkering tools</summary>
        public const int ITEMID_TINKERING_TOOLS = 0x1EBB;

        /// <summary>Item ID for Guild Fletching tools</summary>
        public const int ITEMID_FLETCHING_TOOLS = 0x1EB8;

        /// <summary>Item ID for Guild Sewing kit (variant 1)</summary>
        public const int ITEMID_SEWING_KIT_1 = 0x4C80;

        /// <summary>Item ID for Guild Sewing kit (variant 2)</summary>
        public const int ITEMID_SEWING_KIT_2 = 0x4C81;

        /// <summary>Item ID for Guild Scribe pen</summary>
        public const int ITEMID_SCRIBE_PEN = 0x2051;

        #endregion

        #region Ter Mur Guild District Coordinates

        /// <summary>Minimum X coordinate for Ter Mur guild district</summary>
        public const int TER_MUR_GUILD_MIN_X = 1054;

        /// <summary>Maximum X coordinate for Ter Mur guild district</summary>
        public const int TER_MUR_GUILD_MAX_X = 1126;

        /// <summary>Minimum Y coordinate for Ter Mur guild district</summary>
        public const int TER_MUR_GUILD_MIN_Y = 1907;

        /// <summary>Maximum Y coordinate for Ter Mur guild district</summary>
        public const int TER_MUR_GUILD_MAX_Y = 1983;

        #endregion

        #region Enhancement Limits and Costs

        /// <summary>Base gold cost for enhancement before multipliers</summary>
        public const int BASE_ENHANCEMENT_COST = 100;

        /// <summary>Maximum number of attributes allowed on enhanced item</summary>
        public const int MAX_ATTRIBUTES_ALLOWED = 15;

        /// <summary>Discount divisor when item was crafted by the enhancer (50% discount)</summary>
        public const int CRAFTER_DISCOUNT_DIVISOR = 2;

        #endregion

        #region Attribute Handler Defaults

        /// <summary>Default maximum value for attributes</summary>
        public const int DEFAULT_MAX_VALUE = 15;

        /// <summary>Default increment value for attributes</summary>
        public const int DEFAULT_INCREMENT = 1;

        /// <summary>Default cost multiplier for attributes</summary>
        public const int DEFAULT_COST = 1;

        #endregion

        #region UI Layout - Enhancement Gump

        /// <summary>Width of enhancement gump window</summary>
        public const int GUMP_WIDTH = 620;

        /// <summary>Height of enhancement gump window</summary>
        public const int GUMP_HEIGHT = 390;

        /// <summary>Background graphic ID for enhancement gump</summary>
        public const int GUMP_BACKGROUND_ID = 9200;

        /// <summary>Tiled graphic ID for gump sections</summary>
        public const int GUMP_TILE_ID = 2624;

        /// <summary>Left padding for gump elements</summary>
        public const int GUMP_PADDING_LEFT = 8;

        /// <summary>Top padding for gump elements</summary>
        public const int GUMP_PADDING_TOP = 10;

        /// <summary>Width of gump title bar</summary>
        public const int GUMP_TITLE_WIDTH = 604;

        /// <summary>Height of gump title bar</summary>
        public const int GUMP_TITLE_HEIGHT = 24;

        /// <summary>Width of gump left column</summary>
        public const int GUMP_LEFT_COLUMN_WIDTH = 300;

        /// <summary>Width of gump right column</summary>
        public const int GUMP_RIGHT_COLUMN_WIDTH = 300;

        /// <summary>Height of gump columns</summary>
        public const int GUMP_COLUMN_HEIGHT = 345;

        /// <summary>Left position for gump columns</summary>
        public const int GUMP_LEFT_COLUMN_X = 8;

        /// <summary>Left position for gump right column</summary>
        public const int GUMP_RIGHT_COLUMN_X = 312;

        /// <summary>Top position for gump columns</summary>
        public const int GUMP_COLUMN_Y = 38;

        /// <summary>Color for gump labels (golden/orange)</summary>
        public const int GUMP_LABEL_COLOR = 0x481;

        /// <summary>Maximum rows per column before splitting to second column</summary>
        public const int MAX_ROWS_PER_COLUMN = 11;

        /// <summary>Horizontal offset between columns</summary>
        public const int COLUMN_OFFSET = 304;

        /// <summary>Vertical height of each row</summary>
        public const int ROW_HEIGHT = 25;

        /// <summary>Button ID offset for attribute selection (buttons start at 1000+)</summary>
        public const int BUTTON_ID_OFFSET = 1000;

        /// <summary>X position for labels in first column</summary>
        public const int LABEL_X_BASE = 15;

        /// <summary>X position for gold cost labels</summary>
        public const int GOLD_LABEL_X_BASE = 184;

        /// <summary>X position for use buttons</summary>
        public const int BUTTON_X_BASE = 270;

        /// <summary>Y offset for labels and buttons from row start</summary>
        public const int LABEL_Y_OFFSET = 65;

        /// <summary>Y offset adjustment for buttons</summary>
        public const int BUTTON_Y_OFFSET = 62;

        /// <summary>Y position for title label</summary>
        public const int TITLE_LABEL_Y = 12;

        /// <summary>X position for title label</summary>
        public const int TITLE_LABEL_X = 224;

        /// <summary>Y position for column header labels</summary>
        public const int HEADER_LABEL_Y = 40;

        /// <summary>X position for "Attributes" header in left column</summary>
        public const int HEADER_ATTRIBUTES_X_LEFT = 15;

        /// <summary>X position for "Gold" header in left column</summary>
        public const int HEADER_GOLD_X_LEFT = 184;

        /// <summary>X position for "Use" header in left column</summary>
        public const int HEADER_USE_X_LEFT = 273;

        /// <summary>X position for "Attributes" header in right column</summary>
        public const int HEADER_ATTRIBUTES_X_RIGHT = 319;

        /// <summary>X position for "Gold" header in right column</summary>
        public const int HEADER_GOLD_X_RIGHT = 488;

        /// <summary>X position for "Use" header in right column</summary>
        public const int HEADER_USE_X_RIGHT = 577;

        /// <summary>Button graphic ID (normal state)</summary>
        public const int BUTTON_GRAPHIC_NORMAL = 4023;

        /// <summary>Button graphic ID (pressed state)</summary>
        public const int BUTTON_GRAPHIC_PRESSED = 4024;

        #endregion

        #region Context Menu

        /// <summary>Context menu entry ID for speech gump</summary>
        public const int CONTEXT_MENU_ENTRY_ID = 6121;

        /// <summary>Context menu priority level</summary>
        public const int CONTEXT_MENU_PRIORITY = 3;

        #endregion

        #region Localized Messages (Client-side)

        /// <summary>Localized message ID: "That must be in your pack for you to use it."</summary>
        public const int MSG_MUST_BE_IN_PACK = 1042001;

        /// <summary>Localized message ID: "You need more gold..."</summary>
        public const int MSG_NEED_MORE_GOLD = 500192;

        #endregion

        #region Sound Effects

        /// <summary>Sound effect for tailor guild enhancement</summary>
        public const int SOUND_TAILORING = 0x248;

        /// <summary>Sound effect for carpentry guild enhancement</summary>
        public const int SOUND_CARPENTRY = 0x23D;

        /// <summary>Sound effect for archery/fletching guild enhancement</summary>
        public const int SOUND_FLETCHING = 0x55;

        /// <summary>Sound effect for tinkering guild enhancement</summary>
        public const int SOUND_TINKERING = 0x542;

        /// <summary>Sound effect for blacksmithing guild enhancement</summary>
        public const int SOUND_BLACKSMITHING = 0x541;

        #endregion

        #region Targeting

        /// <summary>Targeting distance (-1 = unlimited)</summary>
        public const int TARGET_DISTANCE_UNLIMITED = -1;

        #endregion

        #region Serialization

        /// <summary>Current version number for serialization</summary>
        public const int SERIALIZATION_VERSION = 0;

        #endregion
    }
}

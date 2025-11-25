namespace Server.Items
{
    /// <summary>
    /// Centralized constants for harvest tool calculations and mechanics.
    /// Extracted from BaseHarvestTool.cs and Shovel.cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class HarvestToolConstants
    {
        #region Default Values
        
        /// <summary>Default uses remaining for new tools</summary>
        public const int DEFAULT_USES_REMAINING = 50;
        
        #endregion
        
        #region Quality Scalars
        
        /// <summary>Scalar for regular quality tools (100%)</summary>
        public const int QUALITY_SCALAR_REGULAR = 100;
        
        /// <summary>Scalar for exceptional quality tools (200%)</summary>
        public const int QUALITY_SCALAR_EXCEPTIONAL = 200;
        
        /// <summary>Divisor used in scaling calculations</summary>
        public const int SCALING_DIVISOR = 100;
        
        #endregion
        
        #region Skill Thresholds
        
        /// <summary>Minimum skill required for stone mining</summary>
        public const double STONE_MINING_MIN_SKILL = 100.0;
        
        #endregion
        
        #region Item IDs
        
        /// <summary>Shovel item ID</summary>
        public const int ITEM_ID_SHOVEL = 0xF39;
        
        #endregion
        
        #region Weights
        
        /// <summary>Default weight for shovel</summary>
        public const double WEIGHT_SHOVEL = 2.0;
        
        #endregion
        
        #region Message IDs (Localization)
        
        /// <summary>"crafted by ~1_NAME~"</summary>
        public const int MSG_ID_CRAFTED_BY = 1050043;
        
        /// <summary>Property label format ID</summary>
        public const int MSG_ID_PROPERTY_LABEL_FORMAT = 1053099;
        
        /// <summary>"exceptional" (commented, kept for reference)</summary>
        public const int MSG_ID_EXCEPTIONAL = 1060636;
        
        /// <summary>"uses remaining: ~1_val~"</summary>
        public const int MSG_ID_USES_REMAINING = 1060584;
        
        /// <summary>"Durability"</summary>
        public const int MSG_ID_DURABILITY = 1017323;
        
        /// <summary>"That must be in your pack for you to use it."</summary>
        public const int MSG_ID_MUST_BE_IN_PACK = 1042001;
        
        /// <summary>Mining context menu: both ore and stone</summary>
        public const int MSG_ID_MINING_BOTH = 6179;
        
        /// <summary>Mining context menu: ore only</summary>
        public const int MSG_ID_MINING_ORE_ONLY = 6178;
        
        /// <summary>Toggle mining stone: false</summary>
        public const int MSG_ID_TOGGLE_STONE_FALSE = 6176;
        
        /// <summary>Toggle mining stone: true</summary>
        public const int MSG_ID_TOGGLE_STONE_TRUE = 6177;
        
        /// <summary>"You are already set to mine both ore and stone!"</summary>
        public const int MSG_ID_ALREADY_MINING_BOTH = 1054023;
        
        /// <summary>"You have not learned how to mine stone or you do not have enough skill!"</summary>
        public const int MSG_ID_CANNOT_MINE_STONE = 1054024;
        
        /// <summary>"You are now set to mine both ore and stone."</summary>
        public const int MSG_ID_NOW_MINING_BOTH = 1054022;
        
        /// <summary>"You are now set to mine only ore."</summary>
        public const int MSG_ID_NOW_MINING_ORE_ONLY = 1054020;
        
        /// <summary>"You are already set to mine only ore!"</summary>
        public const int MSG_ID_ALREADY_MINING_ORE_ONLY = 1054021;
        
        #endregion
        
        #region Color Codes
        
        /// <summary>Cyan color for crafter name and resource name</summary>
        public const string COLOR_CYAN = "#8be4fc";
        
        /// <summary>Yellow color for exceptional quality</summary>
        public const string COLOR_YELLOW = "#ffe066";
        
        #endregion
        
        #region Context Menu Colors
        
        /// <summary>Color for mining context menu entry</summary>
        public const int CONTEXT_MENU_COLOR_MINING = 0x421F;
        
        #endregion
        
        #region Serialization
        
        /// <summary>Current serialization version</summary>
        public const int SERIALIZATION_VERSION_CURRENT = 1;
        
        /// <summary>Legacy serialization version</summary>
        public const int SERIALIZATION_VERSION_LEGACY = 0;
        
        #endregion
        
        #region SaveFlag Values
        
        /// <summary>SaveFlag: None</summary>
        public const int SAVE_FLAG_NONE = 0x00000000;
        
        /// <summary>SaveFlag: Quality</summary>
        public const int SAVE_FLAG_QUALITY = 0x00000008;
        
        /// <summary>SaveFlag: Crafter</summary>
        public const int SAVE_FLAG_CRAFTER = 0x00000200;
        
        /// <summary>SaveFlag: Resource</summary>
        public const int SAVE_FLAG_RESOURCE = 0x00800000;
        
        #endregion
    }
}


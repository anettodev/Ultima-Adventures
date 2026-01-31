namespace Server.Items
{
    /// <summary>
    /// Centralized constants for ingot calculations and mechanics.
    /// Extracted from Ingots.cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class IngotConstants
    {
        #region Item IDs
        
        /// <summary>Standard ingot item ID</summary>
        public const int ITEM_ID_INGOT = 0x1BF2;
        
        /// <summary>Flipped ingot item ID</summary>
        public const int ITEM_ID_INGOT_FLIPPED = 0x1BEF;
        
        #endregion
        
        #region Weight Constants
        
        /// <summary>Default weight for ingots</summary>
        public const double DEFAULT_WEIGHT = 0.1;
        
        #endregion
        
        #region Label Numbers (Localization IDs)
        
        /// <summary>Base label number for DullCopper-Valorite range</summary>
        public const int LABEL_BASE_DULL_COPPER_TO_VALORITE = 1042684;
        
        /// <summary>Default/Iron ingot label number</summary>
        public const int LABEL_DEFAULT_IRON = 1042692;
        
        /// <summary>Titanium ingot label number</summary>
        public const int LABEL_TITANIUM = 6661001;
        
        /// <summary>Rosenium ingot label number</summary>
        public const int LABEL_ROSENIUM = 6662001;
        
        /// <summary>Platinum ingot label number</summary>
        public const int LABEL_PLATINUM = 6663001;
        
        /// <summary>Steel ingot label number</summary>
        public const int LABEL_STEEL = 1036159;
        
        /// <summary>Brass ingot label number</summary>
        public const int LABEL_BRASS = 1036160;
        
        /// <summary>Mithril ingot label number</summary>
        public const int LABEL_MITHRIL = 1036158;
        
        /// <summary>Obsidian ingot label number</summary>
        public const int LABEL_OBSIDIAN = 1036168;
        
        /// <summary>Nepturite ingot label number</summary>
        public const int LABEL_NEPTURITE = 1036176;
        
        /// <summary>Xormite ingot label number</summary>
        public const int LABEL_XORMITE = 1034443;
        
        /// <summary>Dwarven ingot label number</summary>
        public const int LABEL_DWARVEN = 1036187;
        
        #endregion
        
        #region Serialization
        
        /// <summary>Current serialization version</summary>
        public const int SERIALIZATION_VERSION_CURRENT = 1;
        
        /// <summary>Legacy serialization version (OreInfo-based)</summary>
        public const int SERIALIZATION_VERSION_LEGACY = 0;
        
        #endregion
        
        #region Property Display
        
        /// <summary>Property label format ID for resource type display</summary>
        public const int PROPERTY_LABEL_FORMAT_ID = 1053099;
        
        #endregion
        
        #region OreInfo Mapping (Version 0 Deserialization)
        
        /// <summary>OreInfo index for Iron</summary>
        public const int ORE_INFO_IRON = 0;
        
        /// <summary>OreInfo index for DullCopper</summary>
        public const int ORE_INFO_DULL_COPPER = 1;
        
        /// <summary>OreInfo index for Copper</summary>
        public const int ORE_INFO_COPPER = 2;
        
        /// <summary>OreInfo index for Bronze</summary>
        public const int ORE_INFO_BRONZE = 3;
        
        /// <summary>OreInfo index for ShadowIron</summary>
        public const int ORE_INFO_SHADOW_IRON = 4;
        
        /// <summary>OreInfo index for Platinum</summary>
        public const int ORE_INFO_PLATINUM = 5;
        
        /// <summary>OreInfo index for Gold</summary>
        public const int ORE_INFO_GOLD = 6;
        
        /// <summary>OreInfo index for Agapite</summary>
        public const int ORE_INFO_AGAPITE = 7;
        
        /// <summary>OreInfo index for Verite</summary>
        public const int ORE_INFO_VERITE = 8;
        
        /// <summary>OreInfo index for Valorite</summary>
        public const int ORE_INFO_VALORITE = 9;
        
        /// <summary>OreInfo index for Titanium</summary>
        public const int ORE_INFO_TITANIUM = 10;
        
        /// <summary>OreInfo index for Rosenium</summary>
        public const int ORE_INFO_ROSENIUM = 11;
        
        /// <summary>OreInfo index for Nepturite</summary>
        public const int ORE_INFO_NEPTURITE = 12;
        
        /// <summary>OreInfo index for Obsidian</summary>
        public const int ORE_INFO_OBSIDIAN = 13;
        
        /// <summary>OreInfo index for Mithril</summary>
        public const int ORE_INFO_MITHRIL = 14;
        
        /// <summary>OreInfo index for Xormite</summary>
        public const int ORE_INFO_XORMITE = 15;
        
        /// <summary>OreInfo index for Dwarven</summary>
        public const int ORE_INFO_DWARVEN = 16;
        
        #endregion
    }
}


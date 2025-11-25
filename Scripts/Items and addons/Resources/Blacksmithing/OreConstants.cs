namespace Server.Items
{
    /// <summary>
    /// Centralized constants for ore system calculations and mechanics.
    /// Extracted from Ore.cs to improve maintainability and reduce magic numbers.
    /// </summary>
    public static class OreConstants
    {
        #region Item IDs

        /// <summary>Item ID for standard ore pile</summary>
        public const int ITEM_ID_STANDARD_PILE = 0x19B9;

        /// <summary>Item ID for small ore pile</summary>
        public const int ITEM_ID_SMALL_PILE = 0x19B7;

        /// <summary>Item ID for medium ore pile</summary>
        public const int ITEM_ID_MEDIUM_PILE = 0x19B8;

        /// <summary>Item ID for large ore pile</summary>
        public const int ITEM_ID_LARGE_PILE = 0x19BA;

        #endregion

        #region Ore Worth Multipliers

        /// <summary>Worth multiplier for standard ore pile (0x19B9)</summary>
        public const int WORTH_MULTIPLIER_STANDARD_PILE = 8;

        /// <summary>Worth multiplier for small ore pile (0x19B7)</summary>
        public const int WORTH_MULTIPLIER_SMALL_PILE = 2;

        /// <summary>Worth multiplier for medium/large ore piles (0x19B8, 0x19BA)</summary>
        public const int WORTH_MULTIPLIER_MEDIUM_LARGE_PILE = 4;

        #endregion

        #region Weight Limits

        /// <summary>Maximum amount of ore that can be smelted in one operation</summary>
        public const int MAX_SMELT_AMOUNT = 30000;

        /// <summary>Maximum worth for standard ore pile combination</summary>
        public const int MAX_WORTH_STANDARD_PILE = 120000;

        /// <summary>Maximum worth for medium/large ore pile combination</summary>
        public const int MAX_WORTH_MEDIUM_LARGE_PILE = 60000;

        /// <summary>Maximum worth for small ore pile combination</summary>
        public const int MAX_WORTH_SMALL_PILE = 30000;

        #endregion

        #region Smelting Difficulty Values

        /// <summary>Default smelting difficulty</summary>
        public const double SMELT_DIFFICULTY_DEFAULT = 50.0;

        /// <summary>Smelting difficulty for DullCopper</summary>
        public const double SMELT_DIFFICULTY_DULL_COPPER = 65.0;

        /// <summary>Smelting difficulty for Copper</summary>
        public const double SMELT_DIFFICULTY_COPPER = 70.0;

        /// <summary>Smelting difficulty for Bronze</summary>
        public const double SMELT_DIFFICULTY_BRONZE = 75.0;

        /// <summary>Smelting difficulty for ShadowIron</summary>
        public const double SMELT_DIFFICULTY_SHADOW_IRON = 80.0;

        /// <summary>Smelting difficulty for Platinum</summary>
        public const double SMELT_DIFFICULTY_PLATINUM = 85.0;

        /// <summary>Smelting difficulty for Gold</summary>
        public const double SMELT_DIFFICULTY_GOLD = 85.0;

        /// <summary>Smelting difficulty for Agapite</summary>
        public const double SMELT_DIFFICULTY_AGAPITE = 90.0;

        /// <summary>Smelting difficulty for Verite</summary>
        public const double SMELT_DIFFICULTY_VERITE = 95.0;

        /// <summary>Smelting difficulty for Valorite</summary>
        public const double SMELT_DIFFICULTY_VALORITE = 95.0;

        /// <summary>Smelting difficulty for Titanium</summary>
        public const double SMELT_DIFFICULTY_TITANIUM = 100.0;

        /// <summary>Smelting difficulty for Rosenium</summary>
        public const double SMELT_DIFFICULTY_ROSENIUM = 100.0;

        /// <summary>Smelting difficulty for Nepturite</summary>
        public const double SMELT_DIFFICULTY_NEPTURITE = 105.0;

        /// <summary>Smelting difficulty for Obsidian</summary>
        public const double SMELT_DIFFICULTY_OBSIDIAN = 105.0;

        /// <summary>Smelting difficulty for Mithril</summary>
        public const double SMELT_DIFFICULTY_MITHRIL = 110.0;

        /// <summary>Smelting difficulty for Xormite</summary>
        public const double SMELT_DIFFICULTY_XORMITE = 110.0;

        /// <summary>Smelting difficulty for Dwarven</summary>
        public const double SMELT_DIFFICULTY_DWARVEN = 120.0;

        #endregion

        #region Skill Configuration

        /// <summary>Skill variance for smelting (min/max skill range)</summary>
        public const double SMELT_SKILL_VARIANCE = 10.0;

        /// <summary>Minimum skill threshold for non-standard ores</summary>
        public const double SMELT_MIN_SKILL_THRESHOLD = 50.0;

        #endregion

        #region Ore Density Values (DefaultWeight)

        /// <summary>Density for Iron, Agapite, Verite, Valorite</summary>
        public const double DENSITY_STANDARD = 4.0;

        /// <summary>Density for DullCopper, Copper, Bronze</summary>
        public const double DENSITY_COPPER_BASED = 4.5;

        /// <summary>Density for Gold</summary>
        public const double DENSITY_GOLD = 9.5;

        /// <summary>Density for Titanium, Rosenium</summary>
        public const double DENSITY_TITANIUM_ROSENIUM = 2.25;

        /// <summary>Density for Platinum</summary>
        public const double DENSITY_PLATINUM = 10.5;

        /// <summary>Density for Obsidian</summary>
        public const double DENSITY_OBSIDIAN = 1.5;

        /// <summary>Density for Mithril (lowest possible)</summary>
        public const double DENSITY_MITHRIL = 1.0;

        /// <summary>Density for Dwarven (medium)</summary>
        public const double DENSITY_DWARVEN = 5.0;

        /// <summary>Density for Xormite (high)</summary>
        public const double DENSITY_XORMITE = 8.0;

        /// <summary>Density for Nepturite (base/low)</summary>
        public const double DENSITY_NEPTURITE = 2.0;

        #endregion

        #region Localized Message IDs

        /// <summary>Format string for multiple items</summary>
        public const int MSG_ID_MULTIPLE_ITEMS_FORMAT = 1050039;

        /// <summary>"ore" label</summary>
        public const int MSG_ID_ORE_LABEL = 1026583;

        /// <summary>"That is not accessible"</summary>
        public const int MSG_ID_NOT_ACCESSIBLE = 500447;

        /// <summary>"Select the forge on which to smelt the ore..."</summary>
        public const int MSG_ID_SELECT_FORGE = 501971;

        /// <summary>"The ore is too far away"</summary>
        public const int MSG_ID_ORE_TOO_FAR = 501976;

        /// <summary>"Select another pile of ore with which to combine this"</summary>
        public const int MSG_ID_SELECT_ANOTHER_PILE = 501972;

        /// <summary>"You cannot combine ores of different metals"</summary>
        public const int MSG_ID_CANNOT_COMBINE_DIFFERENT = 501979;

        /// <summary>"There is too much ore to combine"</summary>
        public const int MSG_ID_TOO_MUCH_ORE = 1062844;

        /// <summary>"The weight is too great to combine in a container"</summary>
        public const int MSG_ID_WEIGHT_TOO_GREAT = 501978;

        /// <summary>"You burn away the impurities but are left with less useable metal"</summary>
        public const int MSG_ID_BURN_IMPURITIES = 501990;

        #endregion

        #region Sound IDs

        /// <summary>Sound ID for smelting operation</summary>
        public const int SOUND_ID_SMELT = 0x208;

        #endregion

        #region Range Configuration

        /// <summary>Range for double-click interaction with ore</summary>
        public const int INTERACTION_RANGE = 2;

        /// <summary>Range for targeting forge or other ore</summary>
        public const int TARGET_RANGE = 2;

        #endregion
    }
}


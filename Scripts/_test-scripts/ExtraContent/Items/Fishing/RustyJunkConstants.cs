namespace Server.Items
{
    /// <summary>
    /// Centralized constants for RustyJunk calculations and mechanics.
    /// Extracted from RustyJunk.cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class RustyJunkConstants
    {
        #region Base Item IDs

        /// <summary>Base shield item ID</summary>
        public const int ITEM_ID_BASE_SHIELD = 0x1B72;

        #endregion

        #region Shield Item IDs

        /// <summary>Shield item ID variant 1</summary>
        public const int ITEM_ID_SHIELD_1 = 0x1B72;

        /// <summary>Shield item ID variant 2</summary>
        public const int ITEM_ID_SHIELD_2 = 0x1B73;

        /// <summary>Shield item ID variant 3 (option 1)</summary>
        public const int ITEM_ID_SHIELD_3A = 0x1B74;

        /// <summary>Shield item ID variant 3 (option 2)</summary>
        public const int ITEM_ID_SHIELD_3B = 0x1B75;

        /// <summary>Shield item ID variant 4 (option 1)</summary>
        public const int ITEM_ID_SHIELD_4A = 0x1B76;

        /// <summary>Shield item ID variant 4 (option 2)</summary>
        public const int ITEM_ID_SHIELD_4B = 0x1B77;

        /// <summary>Shield item ID variant 5</summary>
        public const int ITEM_ID_SHIELD_5 = 0x1B7B;

        /// <summary>Shield item ID variant 6</summary>
        public const int ITEM_ID_SHIELD_6 = 0x1BC3;

        /// <summary>Shield item ID variant 7 (option 1)</summary>
        public const int ITEM_ID_SHIELD_7A = 0x1BC4;

        /// <summary>Shield item ID variant 7 (option 2)</summary>
        public const int ITEM_ID_SHIELD_7B = 0x1BC5;

        /// <summary>Shield item ID variant 8</summary>
        public const int ITEM_ID_SHIELD_8 = 0x1BC5;

        #endregion

        #region Armor Item IDs

        /// <summary>Arms item ID (option 1)</summary>
        public const int ITEM_ID_ARMS_1 = 0x1410;

        /// <summary>Arms item ID (option 2)</summary>
        public const int ITEM_ID_ARMS_2 = 0x1417;

        /// <summary>Leggings item ID (option 1)</summary>
        public const int ITEM_ID_LEGGINGS_1 = 0x1411;

        /// <summary>Leggings item ID (option 2)</summary>
        public const int ITEM_ID_LEGGINGS_2 = 0x141A;

        /// <summary>Helm item ID</summary>
        public const int ITEM_ID_HELM = 0x1412;

        /// <summary>Gorget item ID</summary>
        public const int ITEM_ID_GORGET = 0x1413;

        /// <summary>Gloves item ID (option 1)</summary>
        public const int ITEM_ID_GLOVES_1 = 0x1414;

        /// <summary>Gloves item ID (option 2)</summary>
        public const int ITEM_ID_GLOVES_2 = 0x1418;

        /// <summary>Armor item ID (option 1)</summary>
        public const int ITEM_ID_ARMOR_1 = 0x1415;

        /// <summary>Armor item ID (option 2)</summary>
        public const int ITEM_ID_ARMOR_2 = 0x1416;

        /// <summary>Coif item ID (option 1)</summary>
        public const int ITEM_ID_COIF_1 = 0x13BB;

        /// <summary>Coif item ID (option 2)</summary>
        public const int ITEM_ID_COIF_2 = 0x13C0;

        /// <summary>Leggings item ID variant 2 (option 1)</summary>
        public const int ITEM_ID_LEGGINGS_2A = 0x13BE;

        /// <summary>Leggings item ID variant 2 (option 2)</summary>
        public const int ITEM_ID_LEGGINGS_2B = 0x13C3;

        /// <summary>Tunic item ID (option 1)</summary>
        public const int ITEM_ID_TUNIC_1 = 0x13BF;

        /// <summary>Tunic item ID (option 2)</summary>
        public const int ITEM_ID_TUNIC_2 = 0x13C4;

        /// <summary>Gloves item ID variant 2 (option 1)</summary>
        public const int ITEM_ID_GLOVES_2A = 0x13EB;

        /// <summary>Gloves item ID variant 2 (option 2)</summary>
        public const int ITEM_ID_GLOVES_2B = 0x13F2;

        /// <summary>Leggings item ID variant 3 (option 1)</summary>
        public const int ITEM_ID_LEGGINGS_3A = 0x13F0;

        /// <summary>Leggings item ID variant 3 (option 2)</summary>
        public const int ITEM_ID_LEGGINGS_3B = 0x13F1;

        /// <summary>Tunic item ID variant 2 (option 1)</summary>
        public const int ITEM_ID_TUNIC_2A = 0x13EC;

        /// <summary>Tunic item ID variant 2 (option 2)</summary>
        public const int ITEM_ID_TUNIC_2B = 0x13ED;

        /// <summary>Sleeves item ID (option 1)</summary>
        public const int ITEM_ID_SLEEVES_1 = 0x13EE;

        /// <summary>Sleeves item ID (option 2)</summary>
        public const int ITEM_ID_SLEEVES_2 = 0x13EF;

        #endregion

        #region Weapon Item IDs

        /// <summary>Hatchet item ID (option 1)</summary>
        public const int ITEM_ID_HATCHET_1 = 0xF43;

        /// <summary>Hatchet item ID (option 2)</summary>
        public const int ITEM_ID_HATCHET_2 = 0xF44;

        /// <summary>Axe item ID (option 1)</summary>
        public const int ITEM_ID_AXE_1 = 0xF45;

        /// <summary>Axe item ID (option 2)</summary>
        public const int ITEM_ID_AXE_2 = 0xF46;

        /// <summary>Battle axe item ID (option 1)</summary>
        public const int ITEM_ID_BATTLE_AXE_1 = 0xF47;

        /// <summary>Battle axe item ID (option 2)</summary>
        public const int ITEM_ID_BATTLE_AXE_2 = 0xF48;

        /// <summary>Axe item ID variant 2 (option 1)</summary>
        public const int ITEM_ID_AXE_2A = 0xF49;

        /// <summary>Axe item ID variant 2 (option 2)</summary>
        public const int ITEM_ID_AXE_2B = 0xF4A;

        /// <summary>Double axe item ID (option 1)</summary>
        public const int ITEM_ID_DOUBLE_AXE_1 = 0xF4B;

        /// <summary>Double axe item ID (option 2)</summary>
        public const int ITEM_ID_DOUBLE_AXE_2 = 0xF4C;

        /// <summary>Bardiche item ID (option 1)</summary>
        public const int ITEM_ID_BARDICHE_1 = 0xF4D;

        /// <summary>Bardiche item ID (option 2)</summary>
        public const int ITEM_ID_BARDICHE_2 = 0xF4E;

        /// <summary>Dagger item ID (option 1)</summary>
        public const int ITEM_ID_DAGGER_1 = 0xF51;

        /// <summary>Dagger item ID (option 2)</summary>
        public const int ITEM_ID_DAGGER_2 = 0xF52;

        /// <summary>Mace item ID (option 1)</summary>
        public const int ITEM_ID_MACE_1 = 0xF5C;

        /// <summary>Mace item ID (option 2)</summary>
        public const int ITEM_ID_MACE_2 = 0xF5D;

        /// <summary>Broadsword item ID (option 1)</summary>
        public const int ITEM_ID_BROADSWORD_1 = 0xF5E;

        /// <summary>Broadsword item ID (option 2)</summary>
        public const int ITEM_ID_BROADSWORD_2 = 0xF5F;

        /// <summary>Longsword item ID (option 1)</summary>
        public const int ITEM_ID_LONGSWORD_1 = 0xF60;

        /// <summary>Longsword item ID (option 2)</summary>
        public const int ITEM_ID_LONGSWORD_2 = 0xF61;

        /// <summary>Spear item ID (option 1)</summary>
        public const int ITEM_ID_SPEAR_1 = 0xF62;

        /// <summary>Spear item ID (option 2)</summary>
        public const int ITEM_ID_SPEAR_2 = 0xF63;

        /// <summary>War hammer item ID (option 1)</summary>
        public const int ITEM_ID_WAR_HAMMER_1 = 0x1438;

        /// <summary>War hammer item ID (option 2)</summary>
        public const int ITEM_ID_WAR_HAMMER_2 = 0x1439;

        /// <summary>Maul item ID (option 1)</summary>
        public const int ITEM_ID_MAUL_1 = 0x143A;

        /// <summary>Maul item ID (option 2)</summary>
        public const int ITEM_ID_MAUL_2 = 0x143B;

        /// <summary>Hammer pick item ID (option 1)</summary>
        public const int ITEM_ID_HAMMER_PICK_1 = 0x143C;

        /// <summary>Hammer pick item ID (option 2)</summary>
        public const int ITEM_ID_HAMMER_PICK_2 = 0x143D;

        /// <summary>Halberd item ID (option 1)</summary>
        public const int ITEM_ID_HALBERD_1 = 0x143E;

        /// <summary>Halberd item ID (option 2)</summary>
        public const int ITEM_ID_HALBERD_2 = 0x143F;

        /// <summary>Cutlass item ID (option 1)</summary>
        public const int ITEM_ID_CUTLASS_1 = 0x1440;

        /// <summary>Cutlass item ID (option 2)</summary>
        public const int ITEM_ID_CUTLASS_2 = 0x1441;

        /// <summary>Great axe item ID (option 1)</summary>
        public const int ITEM_ID_GREAT_AXE_1 = 0x1442;

        /// <summary>Great axe item ID (option 2)</summary>
        public const int ITEM_ID_GREAT_AXE_2 = 0x1443;

        /// <summary>War axe item ID (option 1)</summary>
        public const int ITEM_ID_WAR_AXE_1 = 0x13AF;

        /// <summary>War axe item ID (option 2)</summary>
        public const int ITEM_ID_WAR_AXE_2 = 0x13B0;

        /// <summary>Scimitar item ID (option 1)</summary>
        public const int ITEM_ID_SCIMITAR_1 = 0x13B5;

        /// <summary>Scimitar item ID (option 2)</summary>
        public const int ITEM_ID_SCIMITAR_2 = 0x13B6;

        /// <summary>Long sword item ID (option 1)</summary>
        public const int ITEM_ID_LONG_SWORD_1 = 0x13B7;

        /// <summary>Long sword item ID (option 2)</summary>
        public const int ITEM_ID_LONG_SWORD_2 = 0x13B8;

        /// <summary>Barbarian sword item ID (option 1)</summary>
        public const int ITEM_ID_BARBARIAN_SWORD_1 = 0x13B9;

        /// <summary>Barbarian sword item ID (option 2)</summary>
        public const int ITEM_ID_BARBARIAN_SWORD_2 = 0x13BA;

        /// <summary>Scythe item ID (option 1)</summary>
        public const int ITEM_ID_SCYTHE_1 = 0x26BA;

        /// <summary>Scythe item ID (option 2)</summary>
        public const int ITEM_ID_SCYTHE_2 = 0x26C4;

        /// <summary>Pike item ID (option 1)</summary>
        public const int ITEM_ID_PIKE_1 = 0x26BE;

        /// <summary>Pike item ID (option 2)</summary>
        public const int ITEM_ID_PIKE_2 = 0x26C8;

        #endregion

        #region Weight Constants

        /// <summary>Default initial weight</summary>
        public const double WEIGHT_DEFAULT = 3.0;

        /// <summary>Minimum weight for smelting</summary>
        public const int WEIGHT_MIN = 1;

        /// <summary>Light weight (1.0)</summary>
        public const double WEIGHT_LIGHT_1 = 1.0;

        /// <summary>Light weight (2.0)</summary>
        public const double WEIGHT_LIGHT_2 = 2.0;

        /// <summary>Medium weight (4.0)</summary>
        public const double WEIGHT_MEDIUM_4 = 4.0;

        /// <summary>Medium weight (5.0)</summary>
        public const double WEIGHT_MEDIUM_5 = 5.0;

        /// <summary>Medium weight (7.0)</summary>
        public const double WEIGHT_MEDIUM_7 = 7.0;

        /// <summary>Heavy weight (8.0)</summary>
        public const double WEIGHT_HEAVY_8 = 8.0;

        /// <summary>Heavy weight (10.0)</summary>
        public const double WEIGHT_HEAVY_10 = 10.0;

        /// <summary>Very heavy weight (15.0)</summary>
        public const double WEIGHT_VERY_HEAVY = 15.0;

        #endregion

        #region Weight Ranges

        /// <summary>Light weapon weight minimum</summary>
        public const int WEIGHT_RANGE_LIGHT_MIN = 4;

        /// <summary>Light weapon weight maximum</summary>
        public const int WEIGHT_RANGE_LIGHT_MAX = 8;

        /// <summary>Heavy weapon weight minimum</summary>
        public const int WEIGHT_RANGE_HEAVY_MIN = 8;

        /// <summary>Heavy weapon weight maximum</summary>
        public const int WEIGHT_RANGE_HEAVY_MAX = 16;

        /// <summary>Dagger weight minimum</summary>
        public const int WEIGHT_RANGE_DAGGER_MIN = 1;

        /// <summary>Dagger weight maximum</summary>
        public const int WEIGHT_RANGE_DAGGER_MAX = 2;

        #endregion

        #region Smelting Constants

        /// <summary>Base smelting difficulty</summary>
        public const double SMELTING_DIFFICULTY = 50.0;

        /// <summary>Skill offset for smelting check</summary>
        public const double SMELTING_SKILL_OFFSET = 25.0;

        /// <summary>Minimum skill for smelting</summary>
        public const double SMELTING_MIN_SKILL = 25.0;

        /// <summary>Maximum skill for smelting</summary>
        public const double SMELTING_MAX_SKILL = 75.0;

        /// <summary>Base ingot amount</summary>
        public const int INGOT_AMOUNT_BASE = 1;

        #endregion

        #region Random Ranges

        /// <summary>Armor/weapon choice random minimum</summary>
        public const int RANDOM_ARMOR_WEAPON_MIN = 0;

        /// <summary>Armor/weapon choice random maximum</summary>
        public const int RANDOM_ARMOR_WEAPON_MAX = 1;

        /// <summary>Armor/shield selection random minimum</summary>
        public const int RANDOM_ARMOR_MIN = 0;

        /// <summary>Armor/shield selection random maximum</summary>
        public const int RANDOM_ARMOR_MAX = 20;

        /// <summary>Weapon selection random minimum</summary>
        public const int RANDOM_WEAPON_MIN = 0;

        /// <summary>Weapon selection random maximum</summary>
        public const int RANDOM_WEAPON_MAX = 22;

        #endregion

        #region Hue Values

        /// <summary>Rusty hue option 1</summary>
        public const int HUE_RUSTY_1 = 0xB97;

        /// <summary>Rusty hue option 2</summary>
        public const int HUE_RUSTY_2 = 0xB98;

        /// <summary>Rusty hue option 3</summary>
        public const int HUE_RUSTY_3 = 0xB99;

        /// <summary>Rusty hue option 4</summary>
        public const int HUE_RUSTY_4 = 0xB9A;

        /// <summary>Rusty hue option 5</summary>
        public const int HUE_RUSTY_5 = 0xB88;

        #endregion

        #region Other Constants

        /// <summary>Target range for smelting</summary>
        public const int TARGET_RANGE = 2;

        /// <summary>Sound ID for smelting</summary>
        public const int SOUND_ID_SMELT = 0x208;

        /// <summary>Serialization version</summary>
        public const int SERIALIZATION_VERSION = 0;

        #endregion
    }
}


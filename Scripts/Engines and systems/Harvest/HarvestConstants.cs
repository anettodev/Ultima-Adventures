using System;

namespace Server.Engines.Harvest
{
    /// <summary>
    /// Centralized constants for all harvest systems (Mining, Lumberjacking, Fishing).
    /// Extracted from Mining.cs, Lumberjacking.cs, and Fishing.cs to improve maintainability and reduce magic numbers.
    /// </summary>
    public static class HarvestConstants
    {
        #region Common Harvest System Settings

        /// <summary>Default bank width for harvest systems</summary>
        public const int DEFAULT_BANK_WIDTH = 3;

        /// <summary>Default bank height for harvest systems</summary>
        public const int DEFAULT_BANK_HEIGHT = 3;

        /// <summary>Default minimum respawn time in minutes</summary>
        public const double DEFAULT_RESPAWN_MIN_MINUTES = 15.0;

        /// <summary>Default maximum respawn time in minutes</summary>
        public const double DEFAULT_RESPAWN_MAX_MINUTES = 30.0;

        /// <summary>Default maximum harvest range in tiles</summary>
        public const int DEFAULT_MAX_RANGE = 2;

        /// <summary>Default resources consumed per harvest</summary>
        public const int DEFAULT_CONSUMED_PER_HARVEST = 1;

        /// <summary>Default effect sound delay in seconds</summary>
        public const double DEFAULT_EFFECT_SOUND_DELAY = 0.7;

        #endregion

        #region Mining System Constants

        #region Ore and Stone Mining

        /// <summary>Bank width for ore and stone deposits</summary>
        public const int ORE_BANK_WIDTH = 3;

        /// <summary>Bank height for ore and stone deposits</summary>
        public const int ORE_BANK_HEIGHT = 3;

        /// <summary>Minimum total ore per bank</summary>
        public const int ORE_BANK_MIN_TOTAL = 5;

        /// <summary>Maximum total ore per bank</summary>
        public const int ORE_BANK_MAX_TOTAL = 30;

        /// <summary>Minimum ore respawn time in minutes</summary>
        public const double ORE_RESPAWN_MIN_MINUTES = 15.0;

        /// <summary>Maximum ore respawn time in minutes</summary>
        public const double ORE_RESPAWN_MAX_MINUTES = 30.0;

        /// <summary>Maximum range for ore mining</summary>
        public const int ORE_MAX_RANGE = 2;

        /// <summary>Ore consumed per harvest action</summary>
        public const int ORE_CONSUMED_PER_HARVEST = 1;

        /// <summary>Digging effect delay in seconds</summary>
        public const double ORE_EFFECT_DELAY = 1.5;

        /// <summary>Digging effect sound delay in seconds</summary>
        public const double ORE_EFFECT_SOUND_DELAY = 0.7;

        #endregion

        #region Sand Mining

        /// <summary>Bank width for sand deposits</summary>
        public const int SAND_BANK_WIDTH = 3;

        /// <summary>Bank height for sand deposits</summary>
        public const int SAND_BANK_HEIGHT = 3;

        /// <summary>Minimum total sand per bank</summary>
        public const int SAND_BANK_MIN_TOTAL = 9;

        /// <summary>Maximum total sand per bank</summary>
        public const int SAND_BANK_MAX_TOTAL = 18;

        /// <summary>Minimum sand respawn time in minutes</summary>
        public const double SAND_RESPAWN_MIN_MINUTES = 10.0;

        /// <summary>Maximum sand respawn time in minutes</summary>
        public const double SAND_RESPAWN_MAX_MINUTES = 20.0;

        /// <summary>Maximum range for sand mining</summary>
        public const int SAND_MAX_RANGE = 2;

        /// <summary>Sand consumed per harvest action</summary>
        public const int SAND_CONSUMED_PER_HARVEST = 1;

        /// <summary>Sand digging effect delay in seconds</summary>
        public const double SAND_EFFECT_DELAY = 1.5;

        /// <summary>Sand digging effect sound delay in seconds</summary>
        public const double SAND_EFFECT_SOUND_DELAY = 0.7;

        #endregion

        #region Mining Skill Thresholds

        /// <summary>Minimum skill for iron ore</summary>
        public const double IRON_SKILL_MIN = 00.0;

        /// <summary>Maximum skill for iron ore</summary>
        public const double IRON_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for dull copper ore</summary>
        public const double DULL_COPPER_SKILL_MIN = 65.0;

        /// <summary>Maximum skill for dull copper ore</summary>
        public const double DULL_COPPER_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for copper ore</summary>
        public const double COPPER_SKILL_MIN = 70.0;

        /// <summary>Maximum skill for copper ore</summary>
        public const double COPPER_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for bronze ore</summary>
        public const double BRONZE_SKILL_MIN = 75.0;

        /// <summary>Maximum skill for bronze ore</summary>
        public const double BRONZE_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for shadow iron ore</summary>
        public const double SHADOW_IRON_SKILL_MIN = 80.0;

        /// <summary>Maximum skill for shadow iron ore</summary>
        public const double SHADOW_IRON_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for platinum ore</summary>
        public const double PLATINUM_SKILL_MIN = 85.0;

        /// <summary>Maximum skill for platinum ore</summary>
        public const double PLATINUM_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for gold ore</summary>
        public const double GOLD_SKILL_MIN = 85.0;

        /// <summary>Maximum skill for gold ore</summary>
        public const double GOLD_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for agapite ore</summary>
        public const double AGAPITE_SKILL_MIN = 90.0;

        /// <summary>Maximum skill for agapite ore</summary>
        public const double AGAPITE_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for verite ore</summary>
        public const double VERITE_SKILL_MIN = 95.0;

        /// <summary>Maximum skill for verite ore</summary>
        public const double VERITE_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for valorite ore</summary>
        public const double VALORITE_SKILL_MIN = 95.0;

        /// <summary>Maximum skill for valorite ore</summary>
        public const double VALORITE_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for titanium ore</summary>
        public const double TITANIUM_SKILL_MIN = 100.0;

        /// <summary>Maximum skill for titanium ore</summary>
        public const double TITANIUM_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for rosenium ore</summary>
        public const double ROSENIUM_SKILL_MIN = 100.0;

        /// <summary>Maximum skill for rosenium ore</summary>
        public const double ROSENIUM_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for sand</summary>
        public const double SAND_SKILL_MIN = 50.0;

        /// <summary>Maximum skill for sand</summary>
        public const double SAND_SKILL_MAX = 120.0;

        #endregion

        #region Mining Vein Chances

        /// <summary>Vein chance for iron ore</summary>
        public const double IRON_VEIN_CHANCE = 25.0;

        /// <summary>Vein chance for dull copper ore</summary>
        public const double DULL_COPPER_VEIN_CHANCE = 15.0;

        /// <summary>Vein chance for copper ore</summary>
        public const double COPPER_VEIN_CHANCE = 13.0;

        /// <summary>Vein chance for bronze ore</summary>
        public const double BRONZE_VEIN_CHANCE = 10.0;

        /// <summary>Vein chance for shadow iron ore</summary>
        public const double SHADOW_IRON_VEIN_CHANCE = 9.0;

        /// <summary>Vein chance for platinum ore</summary>
        public const double PLATINUM_VEIN_CHANCE = 6.25;

        /// <summary>Vein chance for gold ore</summary>
        public const double GOLD_VEIN_CHANCE = 6.25;

        /// <summary>Vein chance for agapite ore</summary>
        public const double AGAPITE_VEIN_CHANCE = 4.5;

        /// <summary>Vein chance for verite ore</summary>
        public const double VERITE_VEIN_CHANCE = 3.5;

        /// <summary>Vein chance for valorite ore</summary>
        public const double VALORITE_VEIN_CHANCE = 3.5;

        /// <summary>Vein chance for titanium ore</summary>
        public const double TITANIUM_VEIN_CHANCE = 2.0;

        /// <summary>Vein chance for rosenium ore</summary>
        public const double ROSENIUM_VEIN_CHANCE = 2.0;

        /// <summary>Vein chance for sand</summary>
        public const double SAND_VEIN_CHANCE = 80.0;

        #endregion

        #region Mining Vein Rarity Modifiers

        /// <summary>Rarity modifier for dull copper</summary>
        public const double DULL_COPPER_RARITY = 0.5;

        /// <summary>Rarity modifier for copper</summary>
        public const double COPPER_RARITY = 0.5;

        /// <summary>Rarity modifier for bronze</summary>
        public const double BRONZE_RARITY = 0.5;

        /// <summary>Rarity modifier for shadow iron</summary>
        public const double SHADOW_IRON_RARITY = 0.5;

        /// <summary>Rarity modifier for platinum</summary>
        public const double PLATINUM_RARITY = 0.5;

        /// <summary>Rarity modifier for gold</summary>
        public const double GOLD_RARITY = 0.5;

        /// <summary>Rarity modifier for agapite</summary>
        public const double AGAPITE_RARITY = 0.3;

        /// <summary>Rarity modifier for verite</summary>
        public const double VERITE_RARITY = 0.2;

        /// <summary>Rarity modifier for valorite</summary>
        public const double VALORITE_RARITY = 0.2;

        /// <summary>Rarity modifier for titanium</summary>
        public const double TITANIUM_RARITY = 0.1;

        /// <summary>Rarity modifier for rosenium</summary>
        public const double ROSENIUM_RARITY = 0.1;

        #endregion

        #region Mining Sound Effects

        /// <summary>Sound for digging action</summary>
        public const int SOUND_DIGGING_1 = 0x125;

        /// <summary>Sound for digging action (alternative)</summary>
        public const int SOUND_DIGGING_2 = 0x126;

        /// <summary>Sound for successful item creation</summary>
        public const int SOUND_GLASS_BREAKING = 0x41;

        #endregion

        #region Mining Effect Arrays

        /// <summary>Digging effect actions</summary>
        public static readonly int[] ORE_EFFECT_ACTIONS = new int[] { 11 };

        /// <summary>Digging effect sounds</summary>
        public static readonly int[] ORE_EFFECT_SOUNDS = new int[] { SOUND_DIGGING_1, SOUND_DIGGING_2 };

        /// <summary>Digging effect counts</summary>
        public static readonly int[] ORE_EFFECT_COUNTS = new int[] { 1 };

        /// <summary>Sand digging effect counts</summary>
        public static readonly int[] SAND_EFFECT_COUNTS = new int[] { 6 };

        #endregion

        #region Mining Localized Messages

        /// <summary>No ore resources message</summary>
        public const int MSG_NO_METAL_HERE = 503040;

        /// <summary>Someone got to metal first message</summary>
        public const int MSG_METAL_TAKEN = 503042;

        /// <summary>Moved too far away message</summary>
        public const int MSG_TOO_FAR_AWAY = 503041;

        /// <summary>Failed to find ore message</summary>
        public const int MSG_FAILED_FIND_ORE = 503043;

        /// <summary>Backpack full message</summary>
        public const int MSG_BACKPACK_FULL = 1010481;

        /// <summary>Tool worn out message</summary>
        public const int MSG_TOOL_WORN_OUT = 1044038;

        /// <summary>No sand here message</summary>
        public const int MSG_NO_SAND_HERE = 1044629;

        /// <summary>Failed to find quality sand message</summary>
        public const int MSG_FAILED_FIND_SAND = 1044630;

        /// <summary>Sand backpack full message</summary>
        public const int MSG_SAND_BACKPACK_FULL = 1044632;

        #endregion

        #region Mining Bonus Resources

        /// <summary>Chance for nothing bonus</summary>
        public const double BONUS_NOTHING_CHANCE = 89.75;

        /// <summary>Chance for blank scroll bonus</summary>
        public const double BONUS_SCROLL_CHANCE = 5;

        /// <summary>Chance for local map bonus</summary>
        public const double BONUS_LOCAL_MAP_CHANCE = 1;

        /// <summary>Chance for indecipherable map bonus</summary>
        public const double BONUS_INDECIPHERABLE_MAP_CHANCE = 1;

        /// <summary>Chance for blank map bonus</summary>
        public const double BONUS_BLANK_MAP_CHANCE = 1;

        /// <summary>Chance for amber bonus</summary>
        public const double BONUS_AMBER_CHANCE = 0.5;

        /// <summary>Chance for amethyst bonus</summary>
        public const double BONUS_AMETHYST_CHANCE = 0.5;

        /// <summary>Chance for citrine bonus</summary>
        public const double BONUS_CITRINE_CHANCE = 0.5;

        /// <summary>Chance for diamond bonus</summary>
        public const double BONUS_DIAMOND_CHANCE = 0.1;

        /// <summary>Chance for emerald bonus</summary>
        public const double BONUS_EMERALD_CHANCE = 0.1;

        /// <summary>Chance for ruby bonus</summary>
        public const double BONUS_RUBY_CHANCE = 0.1;

        /// <summary>Chance for sapphire bonus</summary>
        public const double BONUS_SAPPHIRE_CHANCE = 0.1;

        /// <summary>Chance for star sapphire bonus</summary>
        public const double BONUS_STAR_SAPPHIRE_CHANCE = 0.05;

        /// <summary>Chance for tourmaline bonus</summary>
        public const double BONUS_TOURMALINE_CHANCE = 0.05;

        /// <summary>Chance for blue diamond bonus</summary>
        public const double BONUS_BLUE_DIAMOND_CHANCE = 0.05;

        /// <summary>Chance for dark sapphire bonus</summary>
        public const double BONUS_DARK_SAPPHIRE_CHANCE = 0.05;

        /// <summary>Chance for ecru citrine bonus</summary>
        public const double BONUS_ECRU_CITRINE_CHANCE = 0.05;

        /// <summary>Chance for fire ruby bonus</summary>
        public const double BONUS_FIRE_RUBY_CHANCE = 0.05;

        /// <summary>Chance for perfect emerald bonus</summary>
        public const double BONUS_PERFECT_EMERALD_CHANCE = 0.05;

        #endregion

        #region Mining Special Mechanics

        /// <summary>Minimum skill required for stone mining</summary>
        public const double STONE_MINING_SKILL_REQUIRED = 100.0;

        /// <summary>Probability of getting stone instead of ore when stone mining</summary>
        public const double STONE_MINING_PROBABILITY = 0.5;

        /// <summary>Chance of spawning elemental when using gargoyle pickaxe</summary>
        public const double ELEMENTAL_SPAWN_CHANCE = 0.1;

        /// <summary>Strength of spawned elementals</summary>
        public const int ELEMENTAL_STRENGTH = 25;

        /// <summary>Number of spawn offset positions to try</summary>
        public const int SPAWN_OFFSET_COUNT = 8;

        #endregion

        #endregion

        #region Lumberjacking System Constants

        #region Lumberjacking Bank Settings

        /// <summary>Bank width for lumber deposits</summary>
        public const int LUMBER_BANK_WIDTH = 3;

        /// <summary>Bank height for lumber deposits</summary>
        public const int LUMBER_BANK_HEIGHT = 3;

        /// <summary>Minimum total logs per bank</summary>
        public const int LUMBER_BANK_MIN_TOTAL = 6;

        /// <summary>Maximum total logs per bank</summary>
        public const int LUMBER_BANK_MAX_TOTAL = 36;

        /// <summary>Minimum lumber respawn time in minutes</summary>
        public const double LUMBER_RESPAWN_MIN_MINUTES = 15.0;

        /// <summary>Maximum lumber respawn time in minutes</summary>
        public const double LUMBER_RESPAWN_MAX_MINUTES = 30.0;

        /// <summary>Maximum range for lumberjacking</summary>
        public const int LUMBER_MAX_RANGE = 2;

        /// <summary>Logs consumed per harvest action</summary>
        public const int LUMBER_CONSUMED_PER_HARVEST = 2;

        /// <summary>Lumberjacking effect delay in seconds</summary>
        public const double LUMBER_EFFECT_DELAY = 1.4;

        /// <summary>Lumberjacking effect sound delay in seconds</summary>
        public const double LUMBER_EFFECT_SOUND_DELAY = 0.7;

        #endregion

        #region Lumberjacking Skill Thresholds

        /// <summary>Minimum skill for regular logs</summary>
        public const double LOG_SKILL_MIN = 00.0;

        /// <summary>Maximum skill for regular logs</summary>
        public const double LOG_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for ash logs</summary>
        public const double ASH_LOG_SKILL_MIN = 60.0;

        /// <summary>Maximum skill for ash logs</summary>
        public const double ASH_LOG_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for ebony logs</summary>
        public const double EBONY_LOG_SKILL_MIN = 70.0;

        /// <summary>Maximum skill for ebony logs</summary>
        public const double EBONY_LOG_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for golden oak logs</summary>
        public const double GOLDEN_OAK_LOG_SKILL_MIN = 80.0;

        /// <summary>Maximum skill for golden oak logs</summary>
        public const double GOLDEN_OAK_LOG_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for cherry logs</summary>
        public const double CHERRY_LOG_SKILL_MIN = 90.0;

        /// <summary>Maximum skill for cherry logs</summary>
        public const double CHERRY_LOG_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for rosewood logs</summary>
        public const double ROSEWOOD_LOG_SKILL_MIN = 95.0;

        /// <summary>Maximum skill for rosewood logs</summary>
        public const double ROSEWOOD_LOG_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for elven logs</summary>
        public const double ELVEN_LOG_SKILL_MIN = 100.0;

        /// <summary>Maximum skill for elven logs</summary>
        public const double ELVEN_LOG_SKILL_MAX = 120.0;

        /// <summary>Minimum skill for hickory logs</summary>
        public const double HICKORY_LOG_SKILL_MIN = 100.0;

        /// <summary>Maximum skill for hickory logs</summary>
        public const double HICKORY_LOG_SKILL_MAX = 120.0;

        #endregion

        #region Lumberjacking Vein Chances

        /// <summary>Vein chance for regular logs</summary>
        public const double LOG_VEIN_CHANCE = 27.0;

        /// <summary>Vein chance for ash logs</summary>
        public const double ASH_LOG_VEIN_CHANCE = 18.0;

        /// <summary>Vein chance for ebony logs</summary>
        public const double EBONY_LOG_VEIN_CHANCE = 15.0;

        /// <summary>Vein chance for golden oak logs</summary>
        public const double GOLDEN_OAK_VEIN_CHANCE = 12.0;

        /// <summary>Vein chance for cherry logs</summary>
        public const double CHERRY_LOG_VEIN_CHANCE = 10.0;

        /// <summary>Vein chance for rosewood logs</summary>
        public const double ROSEWOOD_LOG_VEIN_CHANCE = 8.0;

        /// <summary>Vein chance for elven logs</summary>
        public const double ELVEN_LOG_VEIN_CHANCE = 5.0;

        /// <summary>Vein chance for hickory logs</summary>
        public const double HICKORY_LOG_VEIN_CHANCE = 5.0;

        #endregion

        #region Lumberjacking Vein Rarity Modifiers

        /// <summary>Rarity modifier for ash logs</summary>
        public const double ASH_LOG_RARITY = 0.5;

        /// <summary>Rarity modifier for ebony logs</summary>
        public const double EBONY_LOG_RARITY = 0.4;

        /// <summary>Rarity modifier for golden oak logs</summary>
        public const double GOLDEN_OAK_RARITY = 0.4;

        /// <summary>Rarity modifier for cherry logs</summary>
        public const double CHERRY_LOG_RARITY = 0.3;

        /// <summary>Rarity modifier for rosewood logs</summary>
        public const double ROSEWOOD_LOG_RARITY = 0.2;

        /// <summary>Rarity modifier for elven logs</summary>
        public const double ELVEN_LOG_RARITY = 0.1;

        /// <summary>Rarity modifier for hickory logs</summary>
        public const double HICKORY_LOG_RARITY = 0.1;

        #endregion

        #region Lumberjacking Sound Effects

        /// <summary>Sound for chopping action</summary>
        public const int SOUND_CHOPPING = 0x13E;

        #endregion

        #region Lumberjacking Effect Arrays

        /// <summary>Chopping effect actions</summary>
        public static readonly int[] LUMBER_EFFECT_ACTIONS = new int[] { 13 };

        /// <summary>Chopping effect sounds</summary>
        public static readonly int[] LUMBER_EFFECT_SOUNDS = new int[] { SOUND_CHOPPING };

        /// <summary>Chopping effect counts</summary>
        public static readonly int[] LUMBER_EFFECT_COUNTS = new int[] { 1 };

        #endregion

        #region Lumberjacking Localized Messages

        /// <summary>No wood here message</summary>
        public const int MSG_NO_WOOD_HERE = 500493;

        /// <summary>Failed to produce wood message</summary>
        public const int MSG_FAILED_PRODUCE_WOOD = 500495;

        /// <summary>Backpack full message</summary>
        public const int MSG_BACKPACK_FULL_WOOD = 500497;

        /// <summary>Axe broken message</summary>
        public const int MSG_AXE_BROKEN = 500499;

        /// <summary>Axe must be equipped message</summary>
        public const int MSG_AXE_MUST_EQUIPPED = 500487;

        /// <summary>Cannot use axe on that message</summary>
        public const int MSG_CANNOT_USE_AXE = 500489;

        /// <summary>Cannot mine while riding message</summary>
        public const int MSG_CANNOT_MINE_RIDING = 501864;

        /// <summary>Cannot mine while polymorphed message</summary>
        public const int MSG_CANNOT_MINE_POLYMORPHED = 501865;

        /// <summary>Cannot mine there message</summary>
        public const int MSG_CANNOT_MINE_THERE = 501862;

        /// <summary>Cannot mine that message</summary>
        public const int MSG_CANNOT_MINE_THAT = 501863;

        #endregion

        #region Lumberjacking Bonus Resources

        /// <summary>Chance for nothing bonus</summary>
        public const double LUMBER_BONUS_NOTHING_CHANCE = 94.0;

        /// <summary>Chance for mushroom bonus</summary>
        public const double LUMBER_BONUS_MUSHROOM_CHANCE = 2.0;

        /// <summary>Chance for reaper oil bonus</summary>
        public const double LUMBER_BONUS_REAPER_OIL_CHANCE = 2.0;

        /// <summary>Chance for mystical tree sap bonus</summary>
        public const double LUMBER_BONUS_TREE_SAP_CHANCE = 1.0;

        /// <summary>Chance for oil wood bonus</summary>
        public const double LUMBER_BONUS_OIL_WOOD_CHANCE = 1.0;

        #endregion

        #endregion

        #region Fishing System Constants

        #region Fishing Bank Settings

        /// <summary>Bank width for fishing</summary>
        public const int FISHING_BANK_WIDTH = 4;

        /// <summary>Bank height for fishing</summary>
        public const int FISHING_BANK_HEIGHT = 4;

        /// <summary>Minimum total fish per bank</summary>
        public const int FISHING_BANK_MIN_TOTAL = 1;

        /// <summary>Maximum total fish per bank</summary>
        public const int FISHING_BANK_MAX_TOTAL = 9;

        /// <summary>Minimum fishing respawn time in minutes</summary>
        public const double FISHING_RESPAWN_MIN_MINUTES = 15.0;

        /// <summary>Maximum fishing respawn time in minutes</summary>
        public const double FISHING_RESPAWN_MAX_MINUTES = 30.0;

        /// <summary>Maximum range for fishing</summary>
        public const int FISHING_MAX_RANGE = 4;

        /// <summary>Fish consumed per harvest action</summary>
        public const int FISHING_CONSUMED_PER_HARVEST = 1;

        /// <summary>Fishing effect delay in seconds</summary>
        public const double FISHING_EFFECT_DELAY = 4.0;

        /// <summary>Fishing effect sound delay in seconds</summary>
        public const double FISHING_EFFECT_SOUND_DELAY = 2.0;

        #endregion

        #region Fishing Skill Thresholds

        /// <summary>Minimum skill for common fish</summary>
        public const double FISH_SKILL_MIN = 00.0;

        /// <summary>Maximum skill for common fish</summary>
        public const double FISH_SKILL_MAX = 100.0;

        #endregion

        #region Fishing Vein Chances

        /// <summary>Vein chance for common fish</summary>
        public const double FISH_VEIN_CHANCE = 100.0;

        #endregion

        #region Fishing Effect Arrays

        /// <summary>Fishing effect actions</summary>
        public static readonly int[] FISHING_EFFECT_ACTIONS = new int[] { 12 };

        /// <summary>Fishing effect sounds (empty array)</summary>
        public static readonly int[] FISHING_EFFECT_SOUNDS = new int[0];

        /// <summary>Fishing effect counts</summary>
        public static readonly int[] FISHING_EFFECT_COUNTS = new int[] { 1 };

        #endregion

        #region Fishing Localized Messages

        /// <summary>Fish don't seem to bite message</summary>
        public const int MSG_FISH_NOT_BITING = 503172;

        /// <summary>Failed to catch anything message</summary>
        public const int MSG_FAILED_CATCH = 503171;

        /// <summary>Need to be closer to water message</summary>
        public const int MSG_TOO_FAR_FROM_WATER = 500976;

        /// <summary>Backpack full message</summary>
        public const int MSG_BACKPACK_FULL_FISH = 503204;

        /// <summary>Fishing pole broken message</summary>
        public const int MSG_POLE_BROKEN = 503174;

        #endregion

        #endregion
    }
}

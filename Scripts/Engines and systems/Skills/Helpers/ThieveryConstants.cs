namespace Server.SkillHandlers
{
	/// <summary>
	/// Centralized constants for thievery skills (Stealing and Snooping) calculations and mechanics.
	/// Extracted from Stealing.cs and Snooping.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class ThieveryConstants
	{
		#region Skill IDs

		/// <summary>Skill ID for Stealing skill</summary>
		public const int STEALING_SKILL_ID = 33;

		#endregion

		#region Range and Distance

		/// <summary>Range for container snooping/stealing</summary>
		public const int CONTAINER_RANGE = 1;

		/// <summary>Range for broadcasting messages to nearby players</summary>
		public const int MESSAGE_BROADCAST_RANGE = 8;

		/// <summary>Range for spotter detection when stealing fails</summary>
		public const int SPOTTER_DETECTION_RANGE = 15;

		#endregion

		#region Skill Check Values

		/// <summary>Maximum skill check value for snooping</summary>
		public const double SNOOPING_SKILL_CHECK_MAX = 100.0;

		/// <summary>Minimum skill check value</summary>
		public const int SKILL_CHECK_MIN = 0;

		/// <summary>Medium skill check value</summary>
		public const int SKILL_CHECK_MEDIUM = 60;

		/// <summary>High skill check value</summary>
		public const int SKILL_CHECK_HIGH = 85;

		/// <summary>Very high skill check value</summary>
		public const int SKILL_CHECK_VERY_HIGH = 125;

		/// <summary>Maximum skill check value for stealing</summary>
		public const int STEALING_SKILL_CHECK_MAX = 150;

		/// <summary>Skill check range for snooping</summary>
		public const int SNOOPING_SKILL_CHECK_RANGE_MIN = 0;

		/// <summary>Skill check range for snooping</summary>
		public const int SNOOPING_SKILL_CHECK_RANGE_MAX = 60;

		/// <summary>Skill check range for stealing (low)</summary>
		public const int STEALING_SKILL_CHECK_RANGE_LOW_MIN = 0;

		/// <summary>Skill check range for stealing (low)</summary>
		public const int STEALING_SKILL_CHECK_RANGE_LOW_MAX = 60;

		/// <summary>Skill check range for stealing (medium)</summary>
		public const int STEALING_SKILL_CHECK_RANGE_MEDIUM_MIN = 0;

		/// <summary>Skill check range for stealing (medium)</summary>
		public const int STEALING_SKILL_CHECK_RANGE_MEDIUM_MAX = 70;

		/// <summary>Skill check range for stealing (high)</summary>
		public const int STEALING_SKILL_CHECK_RANGE_HIGH_MIN = 85;

		/// <summary>Skill check range for stealing (high)</summary>
		public const int STEALING_SKILL_CHECK_RANGE_HIGH_MAX = 125;

		/// <summary>Minimum skill value required for stealable artifacts</summary>
		public const double STEALABLE_ARTIFACT_MIN_SKILL = 100.0;

		#endregion

		#region Weight and Capacity

		/// <summary>Maximum weight that can be stolen</summary>
		public const double MAX_STEALABLE_WEIGHT = 25.0;

		/// <summary>Divisor for weight-based agility calculations</summary>
		public const double WEIGHT_DIVISOR = 30.0;

		/// <summary>Multiplier for weight in skill check calculations</summary>
		public const int WEIGHT_MULTIPLIER = 10;

		/// <summary>Minimum offset for weight-based skill checks</summary>
		public const double WEIGHT_SKILL_OFFSET_MIN = 22.5;

		/// <summary>Maximum offset for weight-based skill checks</summary>
		public const double WEIGHT_SKILL_OFFSET_MAX = 27.5;

		#endregion

		#region Probabilities

		/// <summary>High probability threshold (80%)</summary>
		public const double PROBABILITY_HIGH = 0.80;

		/// <summary>Very high probability threshold (90%)</summary>
		public const double PROBABILITY_VERY_HIGH = 0.90;

		/// <summary>Near certain probability threshold (95%)</summary>
		public const double PROBABILITY_NEAR_CERTAIN = 0.95;

		/// <summary>Low probability threshold (1%)</summary>
		public const double PROBABILITY_LOW = 0.01;

		/// <summary>Very low probability threshold (5%)</summary>
		public const double PROBABILITY_VERY_LOW = 0.05;

		/// <summary>Probability for target to become guarded after failed snoop</summary>
		public const double SNOOP_GUARDED_CHANCE = 0.05;

		#endregion

		#region Fame Thresholds

		/// <summary>Fame threshold for easy difficulty</summary>
		public const int FAME_THRESHOLD_EASY = 1000;

		/// <summary>Fame threshold for medium difficulty</summary>
		public const int FAME_THRESHOLD_MEDIUM = 5000;

		/// <summary>Fame threshold for hard difficulty</summary>
		public const int FAME_THRESHOLD_HARD = 10000;

		/// <summary>Fame threshold for very hard difficulty</summary>
		public const int FAME_THRESHOLD_VERY_HARD = 18000;

		/// <summary>Fame threshold for extreme difficulty</summary>
		public const int FAME_THRESHOLD_EXTREME = 26000;

		#endregion

		#region Fame Reward Ranges

		/// <summary>Easy reward range minimum</summary>
		public const int REWARD_EASY_MIN = 0;

		/// <summary>Easy reward range maximum</summary>
		public const int REWARD_EASY_MAX = 250;

		/// <summary>Medium reward range minimum</summary>
		public const int REWARD_MEDIUM_MIN = 100;

		/// <summary>Medium reward range maximum</summary>
		public const int REWARD_MEDIUM_MAX = 350;

		/// <summary>Hard reward range minimum</summary>
		public const int REWARD_HARD_MIN = 200;

		/// <summary>Hard reward range maximum</summary>
		public const int REWARD_HARD_MAX = 400;

		/// <summary>Very hard reward range minimum</summary>
		public const int REWARD_VERY_HARD_MIN = 300;

		/// <summary>Very hard reward range maximum</summary>
		public const int REWARD_VERY_HARD_MAX = 400;

		/// <summary>Extreme reward range minimum</summary>
		public const int REWARD_EXTREME_MIN = 400;

		/// <summary>Extreme reward range maximum</summary>
		public const int REWARD_EXTREME_MAX = 500;

		/// <summary>Impossible reward range minimum</summary>
		public const int REWARD_IMPOSSIBLE_MIN = 450;

		/// <summary>Impossible reward range maximum</summary>
		public const int REWARD_IMPOSSIBLE_MAX = 500;

		/// <summary>Midland reward reduction minimum</summary>
		public const int MIDLAND_REWARD_REDUCTION_MIN = 50;

		/// <summary>Midland reward reduction maximum</summary>
		public const int MIDLAND_REWARD_REDUCTION_MAX = 149;

		/// <summary>Midland reward threshold</summary>
		public const int MIDLAND_REWARD_THRESHOLD = 150;

		#endregion

		#region Karma and Fame Calculations

		/// <summary>Karma penalty for snooping</summary>
		public const int SNOOPING_KARMA_PENALTY = -4;

		/// <summary>Karma penalty for stealing (base)</summary>
		public const int STEALING_KARMA_PENALTY_BASE = -50;

		/// <summary>Karma divisor for target karma calculations</summary>
		public const int KARMA_DIVISOR = 150;

		/// <summary>Fame divisor for fame calculations</summary>
		public const int FAME_DIVISOR = 150;

		/// <summary>Coffer gold divisor for fame calculation</summary>
		public const int COFFER_GOLD_FAME_DIVISOR = 50;

		/// <summary>Coffer gold divisor for karma calculation</summary>
		public const int COFFER_GOLD_KARMA_DIVISOR = 25;

		#endregion

		#region Gump IDs

		/// <summary>Default container gump ID</summary>
		public const int GUMP_ID_DEFAULT = 60;

		/// <summary>Special character database gump ID</summary>
		public const int GUMP_ID_SPECIAL_CHARACTER = 10913;

		/// <summary>Character database hue for special gump</summary>
		public const int CHARACTER_DB_HUE_SPECIAL = 3;

		#endregion

		#region Hiding Skill

		/// <summary>Divisor for hiding skill in reveal calculations</summary>
		public const int HIDING_SKILL_DIVISOR = 2;

		#endregion

		#region Time Values

		/// <summary>Delay for skill use in seconds</summary>
		public const double SKILL_USE_DELAY_SECONDS = 5.0;

		/// <summary>Stolen item expiration time in minutes</summary>
		public const double STOLEN_ITEM_EXPIRATION_MINUTES = 2.0;

		#endregion

		#region Dungeon Chest Item IDs

		/// <summary>Dungeon chest item ID variant 1</summary>
		public const int DUNGEON_CHEST_ID_1 = 0x3582;

		/// <summary>Dungeon chest item ID variant 2</summary>
		public const int DUNGEON_CHEST_ID_2 = 0x3583;

		/// <summary>Dungeon chest item ID variant 3</summary>
		public const int DUNGEON_CHEST_ID_3 = 0x35AD;

		/// <summary>Dungeon chest item ID variant 4</summary>
		public const int DUNGEON_CHEST_ID_4 = 0x3868;

		/// <summary>Dungeon chest item ID range start</summary>
		public const int DUNGEON_CHEST_ID_RANGE_START = 0x4B5A;

		/// <summary>Dungeon chest item ID range end</summary>
		public const int DUNGEON_CHEST_ID_RANGE_END = 0x4BAB;

		/// <summary>Dungeon chest item ID range start (second range)</summary>
		public const int DUNGEON_CHEST_ID_RANGE_START_2 = 0xECA;

		/// <summary>Dungeon chest item ID range end (second range)</summary>
		public const int DUNGEON_CHEST_ID_RANGE_END_2 = 0xED2;

		/// <summary>Broken golem item ID variant 1</summary>
		public const int BROKEN_GOLEM_ID_1 = 0x3564;

		/// <summary>Broken golem item ID variant 2</summary>
		public const int BROKEN_GOLEM_ID_2 = 0x3565;

		#endregion

		#region Dungeon Chest Calculations

		/// <summary>Multiplier for dungeon chest value calculation</summary>
		public const int DUNGEON_CHEST_VALUE_MULTIPLIER = 50;

		#endregion

		#region Item Amount Ranges

		/// <summary>Random item amount range minimum (small)</summary>
		public const int ITEM_AMOUNT_SMALL_MIN = 1;

		/// <summary>Random item amount range maximum (small)</summary>
		public const int ITEM_AMOUNT_SMALL_MAX = 15;

		/// <summary>Random item amount range minimum (medium)</summary>
		public const int ITEM_AMOUNT_MEDIUM_MIN = 1;

		/// <summary>Random item amount range maximum (medium)</summary>
		public const int ITEM_AMOUNT_MEDIUM_MAX = 20;

		/// <summary>Random item amount range minimum (large)</summary>
		public const int ITEM_AMOUNT_LARGE_MIN = 1;

		/// <summary>Random item amount range maximum (large)</summary>
		public const int ITEM_AMOUNT_LARGE_MAX = 50;

		#endregion

		#region Attribute Mutation Levels

		/// <summary>Attribute mutation level low minimum</summary>
		public const int MUTATION_LEVEL_LOW_MIN = 1;

		/// <summary>Attribute mutation level low maximum</summary>
		public const int MUTATION_LEVEL_LOW_MAX = 2;

		/// <summary>Attribute mutation level medium</summary>
		public const int MUTATION_LEVEL_MEDIUM = 4;

		/// <summary>Attribute mutation level high minimum</summary>
		public const int MUTATION_LEVEL_HIGH_MIN = 4;

		/// <summary>Attribute mutation level high maximum</summary>
		public const int MUTATION_LEVEL_HIGH_MAX = 8;

		/// <summary>Attribute mutation level very high minimum</summary>
		public const int MUTATION_LEVEL_VERY_HIGH_MIN = 8;

		/// <summary>Attribute mutation level very high maximum</summary>
		public const int MUTATION_LEVEL_VERY_HIGH_MAX = 15;

		/// <summary>Attribute mutation level extreme</summary>
		public const int MUTATION_LEVEL_EXTREME = 9;

		#endregion

		#region Switch Case Counts

		/// <summary>Number of cases in easy reward switch</summary>
		public const int SWITCH_CASE_COUNT_EASY = 24;

		/// <summary>Number of cases in medium reward switch</summary>
		public const int SWITCH_CASE_COUNT_MEDIUM = 18;

		/// <summary>Number of cases in rare reward switch</summary>
		public const int SWITCH_CASE_COUNT_RARE = 20;

		/// <summary>Number of cases in impossible reward switch</summary>
		public const int SWITCH_CASE_COUNT_IMPOSSIBLE = 10;

		#endregion

		#region Material Use Values

		/// <summary>Material use value for Ash</summary>
		public const int MATERIAL_USE_ASH = 20;

		/// <summary>Material use value for Cherry</summary>
		public const int MATERIAL_USE_CHERRY = 40;

		/// <summary>Material use value for Ebony</summary>
		public const int MATERIAL_USE_EBONY = 60;

		/// <summary>Material use value for Golden Oak</summary>
		public const int MATERIAL_USE_GOLDEN_OAK = 80;

		/// <summary>Material use value for Hickory</summary>
		public const int MATERIAL_USE_HICKORY = 100;

		/// <summary>Material use value for Rosewood</summary>
		public const int MATERIAL_USE_ROSEWOOD = 120;

		#endregion

		#region Random Ranges

		/// <summary>Random range minimum for quality checks</summary>
		public const int RANDOM_RANGE_QUALITY_MIN = 1;

		/// <summary>Random range maximum for quality checks</summary>
		public const int RANDOM_RANGE_QUALITY_MAX = 4;

		/// <summary>Random range minimum for slayer checks</summary>
		public const int RANDOM_RANGE_SLAYER_MIN = 1;

		/// <summary>Random range maximum for slayer checks</summary>
		public const int RANDOM_RANGE_SLAYER_MAX = 4;

		/// <summary>Random range minimum for damage level</summary>
		public const int RANDOM_RANGE_DAMAGE_LEVEL_MIN = 0;

		/// <summary>Random range maximum for damage level</summary>
		public const int RANDOM_RANGE_DAMAGE_LEVEL_MAX = 4;

		/// <summary>Random range minimum for durability level</summary>
		public const int RANDOM_RANGE_DURABILITY_LEVEL_MIN = 0;

		/// <summary>Random range maximum for durability level</summary>
		public const int RANDOM_RANGE_DURABILITY_LEVEL_MAX = 4;

		/// <summary>Random range minimum for instrument type</summary>
		public const int RANDOM_RANGE_INSTRUMENT_TYPE_MIN = 0;

		/// <summary>Random range maximum for instrument type</summary>
		public const int RANDOM_RANGE_INSTRUMENT_TYPE_MAX = 6;

		/// <summary>Random range minimum for instrument variant</summary>
		public const int RANDOM_RANGE_INSTRUMENT_VARIANT_MIN = 1;

		/// <summary>Random range maximum for instrument variant</summary>
		public const int RANDOM_RANGE_INSTRUMENT_VARIANT_MAX = 4;

		/// <summary>Random range minimum for dungeon chest spawner</summary>
		public const int RANDOM_RANGE_DUNGEON_CHEST_MIN = 45;

		/// <summary>Random range maximum for dungeon chest spawner</summary>
		public const int RANDOM_RANGE_DUNGEON_CHEST_MAX = 105;

		/// <summary>Random range minimum for thief note skill check</summary>
		public const int RANDOM_RANGE_THIEF_NOTE_MIN = 0;

		/// <summary>Random range maximum for thief note skill check</summary>
		public const int RANDOM_RANGE_THIEF_NOTE_MAX = 60;

		/// <summary>Skill check value for thief note</summary>
		public const int SKILL_CHECK_THIEF_NOTE = 100;

		/// <summary>Random range minimum for reputation adjustment</summary>
		public const int RANDOM_RANGE_REPUTATION_MIN = 500;

		/// <summary>Random range maximum for reputation adjustment</summary>
		public const int RANDOM_RANGE_REPUTATION_MAX = 2500;

		/// <summary>Minimum hits required for certain checks</summary>
		public const int MIN_HITS_REQUIRED = 10;

		/// <summary>Fame divisor for stealing odds calculation</summary>
		public const int FAME_DIVISOR = 30000;

		/// <summary>Minimum karma threshold for certain checks</summary>
		public const int MIN_KARMA_THRESHOLD = 20;

		/// <summary>Random range minimum for reagent amounts</summary>
		public const int RANDOM_RANGE_REAGENT_MIN = 1;

		/// <summary>Random range maximum for reagent amounts</summary>
		public const int RANDOM_RANGE_REAGENT_MAX = 15;

		/// <summary>Weight divisor for stealing calculation</summary>
		public const double WEIGHT_CALCULATION_DIVISOR = 10.0;

		/// <summary>Message color for emote messages</summary>
		public const int MESSAGE_COLOR_EMOTE = 1150;

		#endregion
	}
}


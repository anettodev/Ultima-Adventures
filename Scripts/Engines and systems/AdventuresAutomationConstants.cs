namespace Server.Items
{
	/// <summary>
	/// Centralized constants for AdventuresAutomation system calculations and mechanics.
	/// Extracted from AdventuresAutomation.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class AdventuresAutomationConstants
	{
		#region Automation Delays (Seconds)

		/// <summary>Delay between fishing automation actions in seconds</summary>
		public const int DELAY_FISHING_SECONDS = 3;

		/// <summary>Delay between mining automation actions in seconds</summary>
		public const int DELAY_MINING_SECONDS = 2;

		/// <summary>Delay between lumberjacking automation actions in seconds</summary>
		public const int DELAY_LUMBERJACKING_SECONDS = 2;

		/// <summary>Delay between skinning automation actions in seconds</summary>
		public const int DELAY_SKINNING_SECONDS = 3;

		/// <summary>Delay between milling automation actions in seconds</summary>
		public const int DELAY_MILLING_SECONDS = 3;

		/// <summary>Delay between crafting automation actions in seconds</summary>
		public const int DELAY_CRAFTING_SECONDS = 3;

		#endregion

		#region Timer System

		/// <summary>Initial value for global automation timer</summary>
		public const int TIMER_INITIAL_VALUE = 0;

		/// <summary>Maximum value for automation timer before reset</summary>
		public const int TIMER_MAX_VALUE = 30;

		/// <summary>OneTime system type identifier for automation</summary>
		public const int ONETIME_TYPE_VALUE = 3;

		#endregion

		#region Harvest Target Ranges

		/// <summary>Default harvest target search range in tiles</summary>
		public const int HARVEST_RANGE_DEFAULT = 4;

		/// <summary>Harvest target search range for mining and lumberjacking in tiles</summary>
		public const int HARVEST_RANGE_MINING_LUMBERJACKING = 2;

		#endregion

		#region Search Ranges

		/// <summary>Search range for flour mill in tiles</summary>
		public const int MILL_SEARCH_RANGE = 3;

		/// <summary>Search range for water sources (items) in tiles</summary>
		public const int WATER_SEARCH_RANGE_ITEMS = 2;

		/// <summary>Search range for water sources (tiles) - X/Y offset</summary>
		public const int WATER_SEARCH_RANGE_TILES = 1;

		/// <summary>Maximum distance for water tile search</summary>
		public const double WATER_SEARCH_DISTANCE_MAX = 1.0;

		/// <summary>Search range for skinning targets in tiles</summary>
		public const int SKINNING_SEARCH_RANGE = 3;

		#endregion

		#region Weight and Overload

		/// <summary>Weight overload multiplier threshold (110% of max weight)</summary>
		public const double WEIGHT_OVERLOAD_MULTIPLIER = 1.1;

		/// <summary>Minimum item loss when overloaded</summary>
		public const int WEIGHT_LOSS_MINIMUM = 1;

		/// <summary>Divisor for calculating weight loss amount</summary>
		public const int WEIGHT_LOSS_DIVISOR = 10;

		#endregion

		#region Food and Thirst Thresholds

		/// <summary>Hunger threshold for automatic food consumption</summary>
		public const int FOOD_CHECK_THRESHOLD = 15;

		/// <summary>Thirst threshold for automatic water consumption</summary>
		public const int THIRST_CHECK_THRESHOLD = 15;

		/// <summary>Hunger threshold below which automation stops</summary>
		public const int FOOD_STOP_THRESHOLD = 3;

		/// <summary>Thirst threshold below which automation stops</summary>
		public const int THIRST_STOP_THRESHOLD = 3;

		#endregion

		#region Skill Checks

		/// <summary>Minimum skill value for skill checks</summary>
		public const int SKILL_CHECK_MIN = 0;

		/// <summary>Maximum skill value for skill checks</summary>
		public const int SKILL_CHECK_MAX = 50;

		#endregion

		#region Crafting Skills

		/// <summary>Minimum skill required for making dough</summary>
		public const int DOUGH_MIN_SKILL = 0;

		/// <summary>Maximum skill for making dough</summary>
		public const int DOUGH_MAX_SKILL = 60;

		/// <summary>Minimum skill required for making bread</summary>
		public const int BREAD_MIN_SKILL = 30;

		/// <summary>Maximum skill for making bread</summary>
		public const int BREAD_MAX_SKILL = 80;

		#endregion

		#region Item IDs

		/// <summary>Item ID for AdventuresAutomation system item</summary>
		public const int AUTOMATION_ITEM_ID = 0x0EDE;

		/// <summary>First waterskin item ID</summary>
		public const int WATERSKIN_ITEMID_1 = 0xA21;

		/// <summary>Second waterskin item ID</summary>
		public const int WATERSKIN_ITEMID_2 = 0x4971;

		#endregion

		#region Sound IDs

		/// <summary>Sound ID for overweight (female)</summary>
		public const int SOUND_OVERWEIGHT_FEMALE = 0x31C;

		/// <summary>Sound ID for overweight (male)</summary>
		public const int SOUND_OVERWEIGHT_MALE = 0x42C;

		#endregion

		#region Tile Offsets

		/// <summary>Offset added to static tile IDs</summary>
		public const int STATIC_TILE_ID_OFFSET = 0x4000;

		#endregion

		#region Message Colors

		/// <summary>Red color for error messages</summary>
		public const int MSG_COLOR_ERROR = 55;

		/// <summary>Yellow color for warning messages</summary>
		public const int MSG_COLOR_WARNING = 33;

		/// <summary>Debug message color</summary>
		public const int MSG_COLOR_DEBUG = 66;

		/// <summary>White color for normal/system messages</summary>
		public const int MSG_COLOR_NORMAL = 0;

		#endregion
	}
}


namespace Server.Mobiles
{
	/// <summary>
	/// Centralized constants for Animal Broker (AnimalTrainerLord) calculations and mechanics.
	/// Extracted from AnimalBroker.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class AnimalBrokerConstants
	{
		#region Character Setup Constants

		/// <summary>Strength stat for AnimalTrainerLord initialization</summary>
		public const int STAT_STR = 85;

		/// <summary>Dexterity stat for AnimalTrainerLord initialization</summary>
		public const int STAT_DEX = 75;

		/// <summary>Intelligence stat for AnimalTrainerLord initialization</summary>
		public const int STAT_INT = 65;

		/// <summary>Body type for AnimalTrainerLord (human male)</summary>
		public const int BODY_TYPE = 0x191;

		/// <summary>Hair item ID for AnimalTrainerLord</summary>
		public const int HAIR_ITEM_ID = 0x203C;

		/// <summary>Hair hue for AnimalTrainerLord</summary>
		public const int HAIR_HUE = 1175;

		#endregion

		#region Range Constants

		/// <summary>Range for movement-based greeting messages</summary>
		public const int MOVEMENT_GREETING_RANGE = 4;

		/// <summary>Range for pet sale targeting</summary>
		public const int PET_SALE_TARGET_RANGE = 12;

		/// <summary>Range for speech command recognition</summary>
		public const int SPEECH_RANGE = 4;

		/// <summary>Range for checking if pet is in combat</summary>
		public const int PET_COMBAT_CHECK_RANGE = 12;

		/// <summary>Range for context menu entry</summary>
		public const int CONTEXT_MENU_RANGE = 3;

		#endregion

		#region Pet Valuation Constants

		/// <summary>Divisor for calculating chance from MinTameSkill (unused but kept for reference)</summary>
		public const int TAME_SKILL_DIVISOR = 150;

		/// <summary>Maximum tame skill value before division by zero protection</summary>
		public const int MAX_TAME_SKILL = 125;

		/// <summary>Safe maximum tame skill value (prevents division by zero)</summary>
		public const int SAFE_MAX_TAME_SKILL = 124;

		/// <summary>Divisor for easier tames (non-angering pets worth less)</summary>
		public const double EASY_TAME_VALUE_DIVISOR = 1.15;

		/// <summary>Step value for iterative price calculation</summary>
		public const double CALCULATION_STEP = 10.0;

		/// <summary>Base value for factorial calculation</summary>
		public const int BASE_CALC_VALUE = 125;

		/// <summary>Multiplier for factorial calculation</summary>
		public const int FACTORIAL_MULTIPLIER = 15;

		/// <summary>Divisor for gold cut rate calculation (percentage conversion)</summary>
		public const int GOLD_CUT_RATE_DIVISOR = 100;

		/// <summary>Exponent for level-based price adjustment</summary>
		public const double LEVEL_PRICE_EXPONENT = 0.25;

		/// <summary>Minimum price for any pet sale</summary>
		public const int MIN_PET_PRICE = 10;

		#endregion

		#region Gold Constants

		/// <summary>Threshold for using BankCheck instead of Gold (in gold pieces)</summary>
		public const int GOLD_TO_BANKCHECK_THRESHOLD = 60000;

		#endregion

		#region Price Threshold Constants

		/// <summary>Price threshold for low-value pets (400 gold or less)</summary>
		public const int PRICE_THRESHOLD_LOW = 400;

		/// <summary>Price threshold for medium-value pets (401-1000 gold)</summary>
		public const int PRICE_THRESHOLD_MEDIUM = 1000;

		/// <summary>Price threshold for high-value pets (1001-5000 gold)</summary>
		public const int PRICE_THRESHOLD_HIGH = 5000;

		/// <summary>Price threshold for very high-value pets (5001-10001 gold)</summary>
		public const int PRICE_THRESHOLD_VERY_HIGH = 10001;

		/// <summary>Price threshold for ultra high-value pets (40001+ gold)</summary>
		public const int PRICE_THRESHOLD_ULTRA_HIGH = 40001;

		#endregion

		#region Fame Constants

		/// <summary>Divisor for calculating fame award from pet fame</summary>
		public const int FAME_DIVISOR = 100;

		#endregion

		#region Greeting Constants

		/// <summary>Number of greeting messages available</summary>
		public const int GREETING_MESSAGE_COUNT = 10;

		#endregion

		#region Context Menu Constants

		/// <summary>Context menu entry ID for TamingBOD dealer (Contract)</summary>
		public const int CONTEXT_MENU_ID = 3006095;

		/// <summary>Context menu entry ID for appraise command</summary>
		public const int CONTEXT_MENU_APPRAISE_ID = 3006096;

		#endregion

		#region Time Constants

		/// <summary>Delay between contract requests in hours</summary>
		public const int CONTRACT_DELAY_HOURS = 6;

		#endregion
	}
}


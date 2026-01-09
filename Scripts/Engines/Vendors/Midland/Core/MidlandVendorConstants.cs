namespace Server.Mobiles
{
	/// <summary>
	/// Centralized constants for Midland vendor system calculations and mechanics.
	/// Extracted from MidlandVendor.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class MidlandVendorConstants
	{
		#region Stat and Attribute Ranges
		
		/// <summary>Minimum karma value for Midland vendors</summary>
		public const int KARMA_MIN = 13;
		
		/// <summary>Maximum karma value for Midland vendors</summary>
		public const int KARMA_MAX = -45;
		
		/// <summary>Minimum stat value (Str/Dex/Int)</summary>
		public const int STAT_MIN = 100;
		
		/// <summary>Maximum stat value (Str/Dex/Int)</summary>
		public const int STAT_MAX = 300;
		
		/// <summary>Minimum hit points</summary>
		public const int HITS_MIN = 100;
		
		/// <summary>Maximum hit points</summary>
		public const int HITS_MAX = 300;
		
		/// <summary>Minimum damage</summary>
		public const int DAMAGE_MIN = 15;
		
		/// <summary>Maximum damage</summary>
		public const int DAMAGE_MAX = 70;
		
		/// <summary>Virtual armor value</summary>
		public const int VIRTUAL_ARMOR = 70;
		
		#endregion
		
		#region Damage Type Percentages
		
		/// <summary>Physical damage percentage</summary>
		public const int DAMAGE_PHYSICAL_PERCENT = 40;
		
		/// <summary>Cold damage percentage</summary>
		public const int DAMAGE_COLD_PERCENT = 60;
		
		/// <summary>Energy damage percentage</summary>
		public const int DAMAGE_ENERGY_PERCENT = 60;
		
		#endregion
		
		#region Resistance Values
		
		/// <summary>Physical resistance range (min-max)</summary>
		public const int RESISTANCE_PHYSICAL_MIN = 65;
		public const int RESISTANCE_PHYSICAL_MAX = 75;
		
		/// <summary>Fire resistance range (min-max)</summary>
		public const int RESISTANCE_FIRE_MIN = 35;
		public const int RESISTANCE_FIRE_MAX = 40;
		
		/// <summary>Cold resistance range (min-max)</summary>
		public const int RESISTANCE_COLD_MIN = 60;
		public const int RESISTANCE_COLD_MAX = 70;
		
		/// <summary>Poison resistance range (min-max)</summary>
		public const int RESISTANCE_POISON_MIN = 60;
		public const int RESISTANCE_POISON_MAX = 70;
		
		/// <summary>Energy resistance range (min-max)</summary>
		public const int RESISTANCE_ENERGY_MIN = 35;
		public const int RESISTANCE_ENERGY_MAX = 40;
		
		#endregion
		
		#region Skill Ranges
		
		/// <summary>Minimum skill value</summary>
		public const double SKILL_MIN = 90.0;
		
		/// <summary>Maximum skill value</summary>
		public const double SKILL_MAX = 120.0;
		
		/// <summary>OmniAI minimum skill value</summary>
		public const double OMNIAI_SKILL_MIN = 70.0;
		
		/// <summary>OmniAI maximum skill value</summary>
		public const double OMNIAI_SKILL_MAX = 110.0;
		
		#endregion
		
		#region Interaction Ranges
		
		/// <summary>Speech recognition range (tiles)</summary>
		public const int SPEECH_RANGE = 4;
		
		/// <summary>General interaction range (tiles)</summary>
		public const int INTERACTION_RANGE = 5;
		
		/// <summary>Movement detection range (tiles)</summary>
		public const int MOVEMENT_RANGE = 5;
		
		/// <summary>Banker speech range (tiles)</summary>
		public const int BANKER_SPEECH_RANGE = 12;
		
		/// <summary>Banker interaction range (tiles)</summary>
		public const int BANKER_INTERACTION_RANGE = 6;
		
		#endregion
		
		#region Pricing Constants
		
		/// <summary>Service fee percentage (1%)</summary>
		public const double SERVICE_FEE_PERCENT = 0.01;
		
		/// <summary>Buy price multiplier (1.01 = 1% markup)</summary>
		public const double BUY_PRICE_MULTIPLIER = 1.01;
		
		/// <summary>Sell price multiplier (0.99 = 1% discount)</summary>
		public const double SELL_PRICE_MULTIPLIER = 0.99;
		
		/// <summary>Reputation calculation divisor</summary>
		public const int REPUTATION_DIVISOR = 50;
		
		/// <summary>Price calculation base multiplier</summary>
		public const int PRICE_BASE_MULTIPLIER = 100;
		
		/// <summary>Price calculation inventory divisor</summary>
		public const int PRICE_INVENTORY_DIVISOR = 5000;
		
		/// <summary>Minimum price value</summary>
		public const int MIN_PRICE = 1;
		
		#endregion
		
		#region Bulk Purchase Constants
		
		/// <summary>Bulk amount threshold (processes in chunks above this)</summary>
		public const int BULK_AMOUNT_THRESHOLD = 20;
		
		/// <summary>Bulk processing chunk size</summary>
		public const int BULK_CHUNK_SIZE = 10;
		
		#endregion
		
		#region Bank Constants
		
		/// <summary>Bank deposit threshold (amounts above this go to account)</summary>
		public const int BANK_DEPOSIT_THRESHOLD = 5000;
		
		/// <summary>Withdraw limit for classic clients</summary>
		public const int WITHDRAW_LIMIT_CLASSIC = 5000;
		
		/// <summary>Withdraw limit for ML clients</summary>
		public const int WITHDRAW_LIMIT_ML = 60000;
		
		#endregion
		
		#region Timing Constants
		
		/// <summary>Talk delay in seconds</summary>
		public const int TALK_DELAY_SECONDS = 30;
		
		/// <summary>Sale tick timeout (OneTime ticks)</summary>
		public const int SALE_TICK_TIMEOUT = 20;
		
		#endregion
		
		#region Inventory Decay Constants
		
		/// <summary>Inventory decay random chance minimum</summary>
		public const int INVENTORY_DECAY_CHANCE_MIN = 1;
		
		/// <summary>Inventory decay random chance maximum</summary>
		public const int INVENTORY_DECAY_CHANCE_MAX = 500;
		
		/// <summary>Inventory decay trigger value</summary>
		public const int INVENTORY_DECAY_TRIGGER = 69;
		
		/// <summary>Minimum stock for inventory decay</summary>
		public const int INVENTORY_DECAY_MIN_STOCK = 3;
		
		/// <summary>Inventory decay minimum amount</summary>
		public const int INVENTORY_DECAY_MIN = 1;
		
		/// <summary>Inventory decay maximum amount</summary>
		public const int INVENTORY_DECAY_MAX = 3;
		
		#endregion
		
		#region Appearance Constants
		
		/// <summary>Chance for special shoe hue (10%)</summary>
		public const double SHOE_HUE_CHANCE = 0.1;
		
		#endregion
		
		#region Race Constants
		
		/// <summary>Human race ID</summary>
		public const int RACE_HUMAN = 1;
		
		/// <summary>Gargoyle race ID</summary>
		public const int RACE_GARGOYLE = 2;
		
		/// <summary>Lizard race ID</summary>
		public const int RACE_LIZARD = 3;
		
		/// <summary>Pirate race ID</summary>
		public const int RACE_PIRATE = 4;
		
		/// <summary>Orc race ID</summary>
		public const int RACE_ORC = 5;
		
		#endregion
	}
}


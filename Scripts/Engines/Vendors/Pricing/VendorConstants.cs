namespace Server.Mobiles.Vendors
{
	/// <summary>
	/// Centralized constants for vendor pricing system calculations and mechanics.
	/// Extracted from GenericBuy.cs and GenericSell.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class VendorConstants
	{
		#region Order/Chaos Balance System Control
		
		/// <summary>
		/// Enable/disable Order/Chaos balance influence on vendor pricing.
		/// When false: Prices are not affected by GetGoldCutRate() or VendorCurse.
		/// When true: Prices are affected by world balance (Order/Chaos system).
		/// Set to false for simplified pricing system.
		/// </summary>
		public const bool ENABLE_ORDER_CHAOS_PRICING = false;
		
		#endregion
		
		#region Price Calculation Constants
		
		/// <summary>Random price calculation: Low percentage for buy prices</summary>
		public const int RANDOM_BUY_PRICE_LOW_PERCENT = 75;
		
		/// <summary>Random price calculation: High percentage for buy prices</summary>
		public const int RANDOM_BUY_PRICE_HIGH_PERCENT = 125;
		
		/// <summary>Random price calculation: Low percentage for sell prices</summary>
		public const int RANDOM_SELL_PRICE_LOW_PERCENT = 90;
		
		/// <summary>Random price calculation: High percentage for sell prices</summary>
		public const int RANDOM_SELL_PRICE_HIGH_PERCENT = 110;
		
		/// <summary>Number of random iterations for price averaging</summary>
		public const int RANDOM_ITERATIONS = 3;
		
		/// <summary>Percentage divisor for price calculations (100 = 100%)</summary>
		public const int PERCENTAGE_DIVISOR = 100;
		
		/// <summary>Minimum price allowed (prevents 0 or negative prices)</summary>
		public const int MIN_PRICE = 1;
		
		#endregion
		
		#region Quality Modifiers
		
		/// <summary>Price multiplier for Low quality items (armor/weapons)</summary>
		public const double QUALITY_LOW_MULTIPLIER = 0.60;
		
		/// <summary>Price multiplier for Exceptional quality items (armor/weapons)</summary>
		public const double QUALITY_EXCEPTIONAL_MULTIPLIER = 1.25;
		
		#endregion
		
		#region Durability and Protection Bonuses
		
		/// <summary>Gold bonus per durability level (armor/weapons)</summary>
		public const int DURABILITY_BONUS_PER_LEVEL = 100;
		
		/// <summary>Gold bonus per protection level (armor)</summary>
		public const int PROTECTION_BONUS_PER_LEVEL = 100;
		
		/// <summary>Gold bonus per damage level (weapons)</summary>
		public const int DAMAGE_BONUS_PER_LEVEL = 100;
		
		#endregion
		
		#region Final Price Calculation
		
		/// <summary>Final price divisor (WIZARD: price / 2)</summary>
		public const double FINAL_PRICE_DIVISOR = 2.0;
		
		/// <summary>Buy-back price multiplier (vendor charges 1.90x sell price)</summary>
		public const double BUY_BACK_MULTIPLIER = 1.90;
		
		#endregion
		
		#region Barter System
		
		/// <summary>Maximum barter percentage (100 = 100%)</summary>
		public const int MAX_BARTER_PERCENT = 100;
		
		/// <summary>Barter multiplier per percentage point (0.03 = 3% per barter point)</summary>
		public const double BARTER_MULTIPLIER = 0.03;
		
		#endregion
		
		#region Restock Constants
		
		/// <summary>Restock multiplier when amount reaches 0</summary>
		public const int RESTOCK_MULTIPLIER = 2;
		
		/// <summary>Maximum stock amount</summary>
		public const int MAX_STOCK_AMOUNT = 999;
		
		/// <summary>Large stock half amount (when max >= 999)</summary>
		public const int LARGE_STOCK_HALF_AMOUNT = 640;
		
		/// <summary>Small stock threshold (below this, halving is different)</summary>
		public const int SMALL_STOCK_THRESHOLD = 20;
		
		#endregion
		
		#region Display Cache
		
		/// <summary>Display cache item ID (0 = no visual item)</summary>
		public const int DISPLAY_CACHE_ITEMID = 0;
		
		#endregion
	}
}


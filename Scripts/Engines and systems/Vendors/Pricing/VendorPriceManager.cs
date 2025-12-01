using System;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles.Vendors
{
	/// <summary>
	/// Centralized price management system for all vendors.
	/// Simplified version with optional Order/Chaos balance effects (disabled by default).
	/// All price calculations go through this manager to ensure consistency.
	/// </summary>
	public static class VendorPriceManager
	{
		#region Buy Price Calculation
		
		/// <summary>
		/// Calculates the final buy price (what player pays vendor).
		/// Applies: Inflation → (Optional: GetGoldCutRate) → (Optional: VendorCurse) → Minimum price
		/// </summary>
		/// <param name="basePrice">Base price from shop definition</param>
		/// <param name="vendor">The vendor (optional, for VendorCurse if enabled)</param>
		/// <param name="buyer">The buyer (optional, for location-based pricing if enabled)</param>
		/// <param name="item">The item (optional, for location-based pricing if enabled)</param>
		/// <returns>Final price with all applicable modifiers</returns>
		public static int CalculateBuyPrice(int basePrice, BaseVendor vendor = null, Mobile buyer = null, Item item = null)
		{
			// Step 1: Apply inflation (always active)
			int price = VendorInflation.ApplyInflation(basePrice);
			
			// Step 2: Apply GetGoldCutRate (only if Order/Chaos system enabled)
			if (VendorConstants.ENABLE_ORDER_CHAOS_PRICING)
			{
				int goldCutRate = MyServerSettings.GetGoldCutRate(buyer, item);
				double goldCutMultiplier = goldCutRate / 100.0;
				price = (int)(price * goldCutMultiplier);
			}
			
			// Step 3: Apply VendorCurse (only if Order/Chaos system enabled)
			if (VendorConstants.ENABLE_ORDER_CHAOS_PRICING && vendor != null)
			{
				bool isGoodVendor = vendor.Karma > 0 || (vendor.Karma == 0 && buyer != null && buyer.Karma >= 0);
				price = ApplyVendorCurse(price, isGoodVendor);
			}
			
			return Math.Max(VendorConstants.MIN_PRICE, price);
		}
		
		#endregion
		
		#region Sell Price Calculation
		
		/// <summary>
		/// Calculates the final sell price (what vendor pays player).
		/// Applies: Quality/Resource modifiers → Durability bonuses → Final divisor → Barter → Inflation → (Optional: GetGoldCutRate) → (Optional: VendorCurse)
		/// </summary>
		/// <param name="basePrice">Base sell price</param>
		/// <param name="item">The item being sold</param>
		/// <param name="vendor">The vendor (optional, for VendorCurse if enabled)</param>
		/// <param name="seller">The seller (optional, for location-based pricing if enabled)</param>
		/// <param name="barter">Barter skill value (0-100)</param>
		/// <returns>Final sell price with all modifiers</returns>
		public static int CalculateSellPrice(int basePrice, Item item, BaseVendor vendor = null, Mobile seller = null, int barter = 0)
		{
			// Step 1: Apply quality modifiers
			int price = VendorPriceHelper.ApplyQualityModifiers(basePrice, item);
			
			// Step 2: Apply resource multipliers
			price = VendorPriceHelper.ApplyResourceMultipliers(price, item);
			
			// Step 3: Apply durability/protection bonuses
			price = VendorPriceHelper.ApplyDurabilityBonuses(price, item);
			
			// Step 4: Apply final price divisor (WIZARD: price / 2)
			price = (int)(price / VendorConstants.FINAL_PRICE_DIVISOR);
			
			// Step 5: Apply barter bonus
			if (barter > 0)
			{
				price = VendorPriceHelper.ApplyBarterBonus(price, barter);
			}
			
			// Step 6: Apply inflation (always active)
			price = VendorInflation.ApplyInflation(price);
			
			// Step 7: Apply GetGoldCutRate (only if Order/Chaos system enabled)
			if (VendorConstants.ENABLE_ORDER_CHAOS_PRICING)
			{
				int goldCutRate = MyServerSettings.GetGoldCutRate(seller, item);
				double goldCutMultiplier = goldCutRate / 100.0;
				price = (int)(price * goldCutMultiplier);
			}
			
			// Step 8: Apply VendorCurse (only if Order/Chaos system enabled)
			if (VendorConstants.ENABLE_ORDER_CHAOS_PRICING && vendor != null)
			{
				bool isGoodVendor = vendor.Karma > 0 || (vendor.Karma == 0 && seller != null && seller.Karma >= 0);
				price = ApplyVendorCurse(price, isGoodVendor);
			}
			
			return Math.Max(VendorConstants.MIN_PRICE, price);
		}
		
		/// <summary>
		/// Calculates buy-back price (what vendor charges to buy back).
		/// Typically higher than sell price.
		/// </summary>
		/// <param name="sellPrice">The sell price</param>
		/// <returns>Buy-back price (typically 1.90x sell price)</returns>
		public static int CalculateBuyBackPrice(int sellPrice)
		{
			double buyBackPrice = (double)sellPrice * VendorConstants.BUY_BACK_MULTIPLIER;
			return VendorInflation.ApplyInflation((int)buyBackPrice);
		}
		
		#endregion
		
		#region Random Price Calculation
		
		/// <summary>
		/// Calculates random buy price variation (for restocking).
		/// </summary>
		/// <param name="itemPrice">Base item price</param>
		/// <returns>Randomized price within configured range</returns>
		public static int GetRandomBuyPrice(int itemPrice)
		{
			int randomPrice = VendorPriceHelper.CalculateRandomPrice(
				itemPrice,
				VendorConstants.RANDOM_BUY_PRICE_LOW_PERCENT,
				VendorConstants.RANDOM_BUY_PRICE_HIGH_PERCENT,
				VendorConstants.RANDOM_ITERATIONS
			);
			
			return VendorInflation.ApplyInflation(randomPrice);
		}
		
		/// <summary>
		/// Calculates random sell price variation.
		/// </summary>
		/// <param name="itemPrice">Base item price</param>
		/// <returns>Randomized price within configured range</returns>
		public static int GetRandomSellPrice(int itemPrice)
		{
			int randomPrice = VendorPriceHelper.CalculateRandomPrice(
				itemPrice,
				VendorConstants.RANDOM_SELL_PRICE_LOW_PERCENT,
				VendorConstants.RANDOM_SELL_PRICE_HIGH_PERCENT,
				VendorConstants.RANDOM_ITERATIONS
			);
			
			return VendorInflation.ApplyInflation(randomPrice);
		}
		
		#endregion
		
		#region VendorCurse Integration (Optional)
		
		/// <summary>
		/// Applies VendorCurse adjustment to price.
		/// Only used when ENABLE_ORDER_CHAOS_PRICING is true.
		/// Extracted from BaseVendor.AdjustPrices() for centralized use.
		/// </summary>
		private static int ApplyVendorCurse(int price, bool isGoodVendor)
		{
			int curse = Server.Items.AetherGlobe.VendorCurse;
			
			// Invert for bad vendors
			if (!isGoodVendor)
				curse = 100000 - curse;
			
			double multiplier = GetVendorCurseMultiplier(curse);
			int adjustedPrice = (int)(price * multiplier);
			
			return Math.Max(VendorConstants.MIN_PRICE, adjustedPrice);
		}
		
		/// <summary>
		/// Gets the price multiplier based on VendorCurse level.
		/// </summary>
		private static double GetVendorCurseMultiplier(int curse)
		{
			if (curse < 10000)
				return 0.55;
			else if (curse < 20000)
				return 0.60;
			else if (curse < 30000)
				return 0.65;
			else if (curse < 40000)
				return 0.70;
			else if (curse < 50000)
				return 0.75;
			else if (curse < 60000)
				return 0.80;
			else if (curse < 70000)
				return 0.85;
			else if (curse < 80000)
				return 0.90;
			else if (curse < 90000)
				return 0.95;
			else // curse < 100000
				return 1.00;
		}
		
		/// <summary>
		/// Applies VendorCurse adjustment to stock amount.
		/// Only used when ENABLE_ORDER_CHAOS_PRICING is true.
		/// Extracted from BaseVendor.AdjustAmount() for centralized use.
		/// </summary>
		public static int CalculateStockAmount(int baseAmount, bool isGoodVendor)
		{
			if (!VendorConstants.ENABLE_ORDER_CHAOS_PRICING)
				return baseAmount; // No adjustment when disabled
			
			int curse = Server.Items.AetherGlobe.VendorCurse;
			
			// Invert for bad vendors
			if (!isGoodVendor)
				curse = 100000 - curse;
			
			double multiplier = GetVendorCurseAmountMultiplier(curse);
			int adjustedAmount = (int)(baseAmount * multiplier);
			
			return Math.Max(VendorConstants.MIN_PRICE, adjustedAmount);
		}
		
		/// <summary>
		/// Gets the amount multiplier based on VendorCurse level.
		/// </summary>
		private static double GetVendorCurseAmountMultiplier(int curse)
		{
			if (curse < 10000)
				return 1.45;
			else if (curse < 20000)
				return 1.35;
			else if (curse < 30000)
				return 1.25;
			else if (curse < 40000)
				return 1.15;
			else if (curse < 50000)
				return 1.05;
			else if (curse < 60000)
				return 0.95;
			else if (curse < 70000)
				return 0.85;
			else if (curse < 80000)
				return 0.75;
			else if (curse < 90000)
				return 0.65;
			else // curse < 100000
				return 0.55;
		}
		
		#endregion
	}
}


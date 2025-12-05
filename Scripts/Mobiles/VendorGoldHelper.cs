using Server.Misc;

namespace Server.Mobiles
{
	/// <summary>
	/// Helper class for vendor gold calculation logic.
	/// Extracted from BaseVendor.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class VendorGoldHelper
	{
		/// <summary>
		/// Calculates initial gold amount for a vendor based on gold cut rate.
		/// </summary>
		/// <param name="vendor">The vendor to calculate gold for</param>
		/// <param name="minAmount">Minimum gold amount</param>
		/// <param name="maxAmount">Maximum gold amount</param>
		/// <returns>Tuple containing calculated min and max gold amounts</returns>
		public static System.Tuple<int, int> CalculateInitialGold(BaseVendor vendor, int minAmount, int maxAmount)
		{
			double goldCutRate = MyServerSettings.GetGoldCutRate(vendor, null) * BaseVendorConstants.PERCENTAGE_MULTIPLIER;
			
			double calculatedMin = minAmount * goldCutRate;
			double calculatedMax = maxAmount * goldCutRate;
			
			return new System.Tuple<int, int>((int)calculatedMin, (int)calculatedMax);
		}

		/// <summary>
		/// Calculates initial gold amount for a vendor using default values.
		/// </summary>
		/// <param name="vendor">The vendor to calculate gold for</param>
		/// <returns>Tuple containing calculated min and max gold amounts</returns>
		public static System.Tuple<int, int> CalculateInitialGold(BaseVendor vendor)
		{
			return CalculateInitialGold(
				vendor,
				BaseVendorConstants.INITIAL_GOLD_MIN,
				BaseVendorConstants.INITIAL_GOLD_MAX
			);
		}

		/// <summary>
		/// Packs gold into vendor's backpack using calculated amounts.
		/// </summary>
		/// <param name="vendor">The vendor to pack gold for</param>
		/// <param name="minAmount">Minimum gold amount</param>
		/// <param name="maxAmount">Maximum gold amount</param>
		public static void PackGoldForVendor(BaseVendor vendor, int minAmount, int maxAmount)
		{
			System.Tuple<int, int> goldAmounts = CalculateInitialGold(vendor, minAmount, maxAmount);
			vendor.PackGold(goldAmounts.Item1, goldAmounts.Item2);
		}

		/// <summary>
		/// Packs gold into vendor's backpack using default values.
		/// </summary>
		/// <param name="vendor">The vendor to pack gold for</param>
		public static void PackGoldForVendor(BaseVendor vendor)
		{
			PackGoldForVendor(
				vendor,
				BaseVendorConstants.INITIAL_GOLD_MIN,
				BaseVendorConstants.INITIAL_GOLD_MAX
			);
		}
	}
}


using System;

namespace Server.Mobiles.Vendors
{
	/// <summary>
	/// Centralized inflation system for vendor prices.
	/// Allows easy adjustment of all vendor prices globally.
	/// Default: 0% inflation (multiplier = 1.0)
	/// </summary>
	public static class VendorInflation
	{
		#region Fields
		
		/// <summary>Global inflation multiplier for all vendor prices (1.0 = no inflation, 0% change)</summary>
		private static double s_InflationMultiplier = 1.0;
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets or sets the global inflation multiplier.
		/// Value of 1.0 = no inflation (0%), 1.5 = 50% increase, 0.8 = 20% decrease.
		/// Easy to access and modify for future changes.
		/// </summary>
		public static double InflationMultiplier
		{
			get { return s_InflationMultiplier; }
			set
			{
				if (value < 0.1)
					value = 0.1; // Minimum 10% of original price
				if (value > 10.0)
					value = 10.0; // Maximum 1000% of original price
				s_InflationMultiplier = value;
			}
		}
		
		/// <summary>
		/// Gets the inflation percentage (for display purposes).
		/// Returns 0.0 for 1.0 multiplier (no inflation).
		/// </summary>
		public static double InflationPercentage
		{
			get { return (s_InflationMultiplier - 1.0) * 100.0; }
		}
		
		#endregion
		
		#region Price Calculation
		
		/// <summary>
		/// Applies inflation to a base price.
		/// </summary>
		/// <param name="basePrice">The base price before inflation</param>
		/// <returns>The price after applying inflation multiplier</returns>
		public static int ApplyInflation(int basePrice)
		{
			if (basePrice <= 0)
				return basePrice;
			
			double inflatedPrice = (double)basePrice * s_InflationMultiplier;
			return (int)Math.Round(inflatedPrice);
		}
		
		/// <summary>
		/// Applies inflation to a base price (double precision).
		/// </summary>
		/// <param name="basePrice">The base price before inflation</param>
		/// <returns>The price after applying inflation multiplier</returns>
		public static double ApplyInflation(double basePrice)
		{
			if (basePrice <= 0)
				return basePrice;
			
			return basePrice * s_InflationMultiplier;
		}
		
		#endregion
	}
}


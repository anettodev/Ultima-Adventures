using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles.Vendors
{
	/// <summary>
	/// Centralized resource multipliers for vendor pricing.
	/// Uses Dictionary for O(1) lookup performance.
	/// Extracted from GenericSell.cs to eliminate code duplication.
	/// </summary>
	public static class VendorResourceMultipliers
	{
		#region Fields
		
		/// <summary>Dictionary mapping CraftResource to price multiplier</summary>
		private static readonly Dictionary<CraftResource, double> s_Multipliers;
		
		#endregion
		
		#region Constructor
		
		static VendorResourceMultipliers()
		{
			s_Multipliers = new Dictionary<CraftResource, double>
			{
				// Metal resources
				{ CraftResource.DullCopper, 1.25 },
				{ CraftResource.ShadowIron, 1.5 },
				{ CraftResource.Copper, 1.75 },
				{ CraftResource.Bronze, 2.0 },
				{ CraftResource.Gold, 2.25 },
				{ CraftResource.Agapite, 2.50 },
				{ CraftResource.Verite, 2.75 },
				{ CraftResource.Valorite, 3.0 },
				{ CraftResource.Nepturite, 3.10 },
				{ CraftResource.Obsidian, 3.10 },
				{ CraftResource.Steel, 3.25 },
				{ CraftResource.Brass, 3.5 },
				{ CraftResource.Mithril, 3.75 },
				{ CraftResource.Xormite, 3.75 },
				{ CraftResource.Dwarven, 7.50 },
				
				// Leather resources
				{ CraftResource.SpinedLeather, 1.5 },
				{ CraftResource.HornedLeather, 1.75 },
				{ CraftResource.BarbedLeather, 2.0 },
				{ CraftResource.NecroticLeather, 2.25 },
				{ CraftResource.VolcanicLeather, 2.5 },
				{ CraftResource.FrozenLeather, 2.75 },
				{ CraftResource.GoliathLeather, 3.0 },
				{ CraftResource.DraconicLeather, 3.25 },
				{ CraftResource.HellishLeather, 3.5 },
				{ CraftResource.DinosaurLeather, 3.75 },
				{ CraftResource.AlienLeather, 3.75 },
				
				// Scale resources
				{ CraftResource.RedScales, 1.25 },
				{ CraftResource.YellowScales, 1.25 },
				{ CraftResource.BlackScales, 1.5 },
				{ CraftResource.GreenScales, 1.5 },
				{ CraftResource.WhiteScales, 1.5 },
				{ CraftResource.BlueScales, 1.5 },
				
				// Wood resources
				{ CraftResource.AshTree, 1.25 },
				{ CraftResource.CherryTree, 1.45 },
				{ CraftResource.EbonyTree, 1.65 },
				{ CraftResource.GoldenOakTree, 1.85 },
				{ CraftResource.HickoryTree, 2.05 },
				{ CraftResource.RosewoodTree, 2.85 },
				{ CraftResource.ElvenTree, 6.0 }
			};
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Gets the price multiplier for a given resource.
		/// </summary>
		/// <param name="resource">The craft resource</param>
		/// <returns>Multiplier (1.0 if resource not found)</returns>
		public static double GetMultiplier(CraftResource resource)
		{
			double multiplier;
			if (s_Multipliers.TryGetValue(resource, out multiplier))
				return multiplier;
			
			return 1.0; // Default multiplier
		}
		
		/// <summary>
		/// Applies resource multiplier to a price.
		/// </summary>
		/// <param name="price">Base price</param>
		/// <param name="resource">The craft resource</param>
		/// <returns>Price with multiplier applied</returns>
		public static int ApplyMultiplier(int price, CraftResource resource)
		{
			double multiplier = GetMultiplier(resource);
			return (int)(price * multiplier);
		}
		
		#endregion
	}
}


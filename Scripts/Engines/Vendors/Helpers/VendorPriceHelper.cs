using System;
using Server.Items;

namespace Server.Mobiles.Vendors
{
	/// <summary>
	/// Helper methods for vendor price calculations.
	/// Extracted from GenericBuy.cs and GenericSell.cs to improve code organization and reusability.
	/// </summary>
	public static class VendorPriceHelper
	{
		#region Random Price Calculation
		
		/// <summary>
		/// Calculates random price variation using averaging method.
		/// </summary>
		/// <param name="basePrice">Base price</param>
		/// <param name="lowPercent">Low percentage (e.g., 75)</param>
		/// <param name="highPercent">High percentage (e.g., 125)</param>
		/// <param name="iterations">Number of random iterations for averaging</param>
		/// <returns>Calculated random price</returns>
		public static int CalculateRandomPrice(int basePrice, int lowPercent, int highPercent, int iterations)
		{
			int total = 0;
			for (int i = 1; i <= iterations; i++)
			{
				total += Utility.RandomMinMax(lowPercent, highPercent);
			}
			
			int average = total / iterations;
			if (average < VendorConstants.MIN_PRICE)
				average = VendorConstants.MIN_PRICE;
			
			double finalPrice = ((double)average / VendorConstants.PERCENTAGE_DIVISOR) * (double)basePrice;
			return Convert.ToInt32(finalPrice);
		}
		
		#endregion
		
		#region Quality Modifiers
		
		/// <summary>
		/// Applies quality modifiers to price for armor items.
		/// </summary>
		/// <param name="price">Base price</param>
		/// <param name="item">The item being priced</param>
		/// <returns>Price with quality modifiers applied</returns>
		public static int ApplyQualityModifiers(int price, Item item)
		{
			if (item is BaseArmor && !Loot.IsArtefact(item))
			{
				BaseArmor armor = (BaseArmor)item;
				
				if (armor.Quality == ArmorQuality.Low)
					price = (int)(price * VendorConstants.QUALITY_LOW_MULTIPLIER);
				else if (armor.Quality == ArmorQuality.Exceptional)
					price = (int)(price * VendorConstants.QUALITY_EXCEPTIONAL_MULTIPLIER);
			}
			else if (item is BaseWeapon && !Loot.IsArtefact(item))
			{
				BaseWeapon weapon = (BaseWeapon)item;
				
				if (weapon.Quality == WeaponQuality.Low)
					price = (int)(price * VendorConstants.QUALITY_LOW_MULTIPLIER);
				else if (weapon.Quality == WeaponQuality.Exceptional)
					price = (int)(price * VendorConstants.QUALITY_EXCEPTIONAL_MULTIPLIER);
			}
			
			return price;
		}
		
		#endregion
		
		#region Resource Multipliers
		
		/// <summary>
		/// Applies resource multipliers to price for armor items.
		/// </summary>
		/// <param name="price">Base price</param>
		/// <param name="item">The item being priced</param>
		/// <returns>Price with resource multipliers applied</returns>
		public static int ApplyResourceMultipliers(int price, Item item)
		{
			if (item is BaseArmor && !Loot.IsArtefact(item))
			{
				BaseArmor armor = (BaseArmor)item;
				price = VendorResourceMultipliers.ApplyMultiplier(price, armor.Resource);
			}
			else if (item is BaseWeapon && !Loot.IsArtefact(item))
			{
				BaseWeapon weapon = (BaseWeapon)item;
				price = VendorResourceMultipliers.ApplyMultiplier(price, weapon.Resource);
			}
			
			return price;
		}
		
		#endregion
		
		#region Durability and Protection Bonuses
		
		/// <summary>
		/// Applies durability and protection bonuses to price.
		/// </summary>
		/// <param name="price">Base price</param>
		/// <param name="item">The item being priced</param>
		/// <returns>Price with durability/protection bonuses applied</returns>
		public static int ApplyDurabilityBonuses(int price, Item item)
		{
			if (item is BaseArmor && !Loot.IsArtefact(item))
			{
				BaseArmor armor = (BaseArmor)item;
				price += VendorConstants.DURABILITY_BONUS_PER_LEVEL * (int)armor.Durability;
				price += VendorConstants.PROTECTION_BONUS_PER_LEVEL * (int)armor.ProtectionLevel;
			}
			else if (item is BaseWeapon && !Loot.IsArtefact(item))
			{
				BaseWeapon weapon = (BaseWeapon)item;
				price += VendorConstants.DURABILITY_BONUS_PER_LEVEL * (int)weapon.DurabilityLevel;
				price += VendorConstants.DAMAGE_BONUS_PER_LEVEL * (int)weapon.DamageLevel;
			}
			
			if (price < VendorConstants.MIN_PRICE)
				price = VendorConstants.MIN_PRICE;
			
			return price;
		}
		
		#endregion
		
		#region Barter Bonus
		
		/// <summary>
		/// Applies barter skill bonus to price.
		/// </summary>
		/// <param name="price">Base price</param>
		/// <param name="barter">Barter skill value (0-100)</param>
		/// <returns>Price with barter bonus applied</returns>
		public static int ApplyBarterBonus(int price, int barter)
		{
			if (barter > VendorConstants.MAX_BARTER_PERCENT)
				barter = VendorConstants.MAX_BARTER_PERCENT;
			
			double multiplier = 1.0 + (barter * VendorConstants.BARTER_MULTIPLIER);
			return (int)(price * multiplier);
		}
		
		#endregion
	}
}


using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{
	/// <summary>
	/// Handles damage absorption and item hit detection for weapons.
	/// Extracted from BaseWeapon.AbsorbDamageAOS to reduce complexity and improve maintainability.
	/// </summary>
	public static class WeaponDamageHandler
	{
		#region Damage Absorption
		
		/// <summary>
		/// Handles parry damage reduction logic.
		/// </summary>
		/// <param name="defender">The defending mobile</param>
		/// <param name="damage">The incoming damage</param>
		/// <returns>The damage after parry reduction</returns>
		public static int HandleParryDamage(Mobile defender, int damage)
		{
			// Parry reduces damage instead of completely blocking
			// 70% chance of taking at least 2 damage (30% chance of zero)
			if (Utility.RandomDouble() < 0.70)
			{
				// Reduce damage by shield AR/2, but ensure minimum of 2 damage
				BaseShield shield = defender.FindItemOnLayer(Layer.TwoHanded) as BaseShield;
				if (shield != null)
				{
					double ar = shield.ArmorRating;
					damage -= (int)(ar / 2.0);
					if (damage < 2)
						damage = 2; // Ensure minimum damage of 2
				}
				else
				{
					// Weapon parry - reduce by 50% but ensure minimum of 2
					damage = (int)(damage * 0.5);
					if (damage < 2)
						damage = 2;
				}
			}
			else
			{
				// 30% chance of complete block (zero damage)
				damage = 0;
			}
			
			return damage;
		}
		
		/// <summary>
		/// Ensures minimum damage based on shield presence.
		/// </summary>
		/// <param name="defender">The defending mobile</param>
		/// <param name="damage">The current damage</param>
		/// <param name="blocked">Whether the attack was parried</param>
		/// <returns>The damage with minimum enforced</returns>
		public static int EnsureMinimumDamage(Mobile defender, int damage, bool blocked)
		{
			// Ensure minimum damage (never zero, except for parry 30% chance which is handled above)
			// Without shield: minimum 4 damage
			// With shield: minimum 2 damage (after parry reduction)
			BaseShield defenderShield = defender.FindItemOnLayer(Layer.TwoHanded) as BaseShield;
			if (damage > 0 && !blocked)
			{
				if (defenderShield == null)
				{
					// No shield: minimum 4 damage
					if (damage < 4)
						damage = 4;
				}
				else
				{
					// With shield: minimum 2 damage (parry already handled above)
					if (damage < 2)
						damage = 2;
				}
			}
			
			return damage;
		}
		
		#endregion
		
		#region Item Hit Detection
		
		/// <summary>
		/// Determines which item on the defender gets hit based on random chance.
		/// Hit order: Weapons > Outer Clothes > Armor > Inner Clothes > Jewelry
		/// </summary>
		/// <param name="defender">The defending mobile</param>
		/// <returns>The item that was hit, or null if none</returns>
		public static Item DetermineHitItem(Mobile defender)
		{
			if (defender == null)
				return null;
			
			double positionChance = Utility.RandomDouble();
			Item hitItem = null;
			int tries = 10;
			
			while (hitItem == null && tries > 0)
			{
				// 18% odds it's a weapon that gets hit
				hitItem = TryGetWeaponHit(defender, positionChance);
				
				// 64% odds it's armor/outer clothes that gets hit
				if (hitItem == null)
					hitItem = TryGetArmorHit(defender, positionChance);
				
				// 15% odds it's inner clothes that gets hit
				if (hitItem == null)
					hitItem = TryGetInnerClothesHit(defender, positionChance);
				
				// 4% it's jewelry
				if (hitItem == null)
					hitItem = TryGetJewelryHit(defender, positionChance);
				
				// Sanity check - can't reduce durability on items that don't support it
				if (hitItem != null && !(hitItem is IWearableDurability) && !(hitItem is BaseJewel) && !(hitItem is IDurability))
					hitItem = null;
				
				tries--;
			}
			
			return hitItem;
		}
		
		/// <summary>
		/// Tries to get a weapon hit (18% chance total).
		/// </summary>
		private static Item TryGetWeaponHit(Mobile defender, double positionChance)
		{
			if (positionChance < 0.06 && defender.FindItemOnLayer(Layer.OneHanded) != null && defender.FindItemOnLayer(Layer.OneHanded) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.OneHanded));
			else if (positionChance < 0.12 && defender.FindItemOnLayer(Layer.TwoHanded) != null && defender.FindItemOnLayer(Layer.TwoHanded) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.TwoHanded));
			else if (positionChance < 0.18 && defender.FindItemOnLayer(Layer.FirstValid) != null && defender.FindItemOnLayer(Layer.FirstValid) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.FirstValid));
			
			return null;
		}
		
		/// <summary>
		/// Tries to get an armor/outer clothes hit (64% chance total).
		/// </summary>
		private static Item TryGetArmorHit(Mobile defender, double positionChance)
		{
			// Outer Torso: 0.18-0.26 (8%)
			if (positionChance < 0.26 && defender.FindItemOnLayer(Layer.OuterTorso) != null && defender.FindItemOnLayer(Layer.OuterTorso) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.OuterTorso));
			// Outer Legs: 0.26-0.33 (7%)
			else if (positionChance < 0.33 && defender.FindItemOnLayer(Layer.OuterLegs) != null && defender.FindItemOnLayer(Layer.OuterLegs) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.OuterLegs));
			// Waist: 0.33-0.40 (7%)
			else if (positionChance < 0.40 && defender.FindItemOnLayer(Layer.Waist) != null && defender.FindItemOnLayer(Layer.Waist) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.Waist));
			// Helm: 0.40-0.47 (7%)
			else if (positionChance < 0.47 && defender.FindItemOnLayer(Layer.Helm) != null && defender.FindItemOnLayer(Layer.Helm) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.Helm));
			// Arms: 0.47-0.54 (7%)
			else if (positionChance < 0.54 && defender.FindItemOnLayer(Layer.Arms) != null && defender.FindItemOnLayer(Layer.Arms) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.Arms));
			// Neck: 0.54-0.61 (7%)
			else if (positionChance < 0.61 && defender.FindItemOnLayer(Layer.Neck) != null && defender.FindItemOnLayer(Layer.Neck) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.Neck));
			// Gloves: 0.61-0.68 (7%)
			else if (positionChance < 0.68 && defender.FindItemOnLayer(Layer.Gloves) != null && defender.FindItemOnLayer(Layer.Gloves) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.Gloves));
			// Shoes: 0.68-0.75 (7%)
			else if (positionChance < 0.75 && defender.FindItemOnLayer(Layer.Shoes) != null && defender.FindItemOnLayer(Layer.Shoes) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.Shoes));
			// Cloak: 0.75-0.82 (7%)
			else if (positionChance < 0.82 && defender.FindItemOnLayer(Layer.Cloak) != null && defender.FindItemOnLayer(Layer.Cloak) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.Cloak));
			
			return null;
		}
		
		/// <summary>
		/// Tries to get an inner clothes hit (15% chance total).
		/// </summary>
		private static Item TryGetInnerClothesHit(Mobile defender, double positionChance)
		{
			// Inner Legs: 0.82-0.84 (2%)
			if (positionChance < 0.84 && defender.FindItemOnLayer(Layer.InnerLegs) != null && defender.FindItemOnLayer(Layer.InnerLegs) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.InnerLegs));
			// Inner Torso: 0.84-0.96 (12% - multiple checks for same layer)
			else if (positionChance < 0.96 && defender.FindItemOnLayer(Layer.InnerTorso) != null && defender.FindItemOnLayer(Layer.InnerTorso) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.InnerTorso));
			// Pants: 0.88-0.90 (2%)
			else if (positionChance < 0.90 && defender.FindItemOnLayer(Layer.Pants) != null && defender.FindItemOnLayer(Layer.Pants) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.Pants));
			// Shirt: 0.90-0.92 (2%)
			else if (positionChance < 0.92 && defender.FindItemOnLayer(Layer.Shirt) != null && defender.FindItemOnLayer(Layer.Shirt) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.Shirt));
			
			return null;
		}
		
		/// <summary>
		/// Tries to get a jewelry hit (4% chance total).
		/// </summary>
		private static Item TryGetJewelryHit(Mobile defender, double positionChance)
		{
			// Bracelet: 0.96-0.98 (2%)
			if (positionChance < 0.98 && defender.FindItemOnLayer(Layer.Bracelet) != null && defender.FindItemOnLayer(Layer.Bracelet) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.Bracelet));
			// Ring: 0.98-0.99 (1%)
			else if (positionChance < 0.99 && defender.FindItemOnLayer(Layer.Ring) != null && defender.FindItemOnLayer(Layer.Ring) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.Ring));
			// Earrings: 0.99-1.0 (1%)
			else if (positionChance <= 1.0 && defender.FindItemOnLayer(Layer.Earrings) != null && defender.FindItemOnLayer(Layer.Earrings) is Item)
				return (Item)(defender.FindItemOnLayer(Layer.Earrings));
			
			return null;
		}
		
		/// <summary>
		/// Applies damage to a hit item and triggers repair checks.
		/// </summary>
		/// <param name="hitItem">The item that was hit</param>
		/// <param name="weapon">The weapon that hit</param>
		/// <param name="damage">The damage amount</param>
		/// <param name="defender">The defending mobile</param>
		public static void ApplyItemDamage(Item hitItem, BaseWeapon weapon, int damage, Mobile defender)
		{
			if (hitItem == null || weapon == null || defender == null)
				return;
			
			if (hitItem is IWearableDurability)
			{
				IWearableDurability armor = hitItem as IWearableDurability;
				if (armor != null)
				{
					armor.OnHit(weapon, damage);
					LevelItemManager.RepairItems(defender);
				}
			}
			else if (hitItem is BaseJewel)
			{
				BaseJewel jewel = hitItem as BaseJewel;
				if (jewel != null)
				{
					jewel.OnHit(weapon, damage);
					LevelItemManager.RepairItems(defender);
				}
			}
		}
		
		#endregion
	}

}


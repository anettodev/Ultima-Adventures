using System;
using Server.Items;
using Server.Misc;
using Server.Mobiles;

namespace Server.SkillHandlers
{
	/// <summary>
	/// Helper methods for poison application logic.
	/// Extracted to reduce complexity and improve maintainability.
	/// </summary>
	public static class PoisoningHelpers
	{
		/// <summary>
		/// Checks if weapon has InfectiousStrike or ShadowInfectiousStrike ability.
		/// </summary>
		public static bool HasInfectiousStrikeAbility( BaseWeapon weapon )
		{
			if ( weapon == null )
				return false;
				
			return weapon.PrimaryAbility == WeaponAbility.InfectiousStrike ||
				   weapon.SecondaryAbility == WeaponAbility.InfectiousStrike ||
				   weapon.ThirdAbility == WeaponAbility.InfectiousStrike ||
				   weapon.FourthAbility == WeaponAbility.InfectiousStrike ||
				   weapon.FifthAbility == WeaponAbility.InfectiousStrike ||
				   weapon.PrimaryAbility == WeaponAbility.ShadowInfectiousStrike ||
				   weapon.SecondaryAbility == WeaponAbility.ShadowInfectiousStrike ||
				   weapon.ThirdAbility == WeaponAbility.ShadowInfectiousStrike ||
				   weapon.FourthAbility == WeaponAbility.ShadowInfectiousStrike ||
				   weapon.FifthAbility == WeaponAbility.ShadowInfectiousStrike;
		}
		
		/// <summary>
		/// Checks if target can be poisoned in classic mode.
		/// In classic mode, metal weapons and ranged weapons can be poisoned.
		/// Also checks base weapon types to allow default weapons (Resource = None).
		/// BaseBashing weapons (mace fighting) and all Pickaxe variants CANNOT be poisoned.
		/// </summary>
		public static bool CanPoisonInClassicMode( BaseWeapon weapon )
		{
			if ( weapon == null )
				return false;
			
			// BaseBashing weapons (mace fighting) cannot be poisoned
			if ( weapon is BaseBashing )
				return false;
			
			// Pickaxe and all pickaxe variants cannot be poisoned
			if ( weapon is Pickaxe || 
				 weapon is SturdyPickaxe || 
				 weapon is GargoylesPickaxe || 
				 weapon is RubyPickaxe || 
				 weapon is LevelPickaxe || 
				 weapon is GiftPickaxe )
				return false;
			
			// Check if it's a ranged weapon (bows/crossbows)
			if ( weapon is BaseRanged )
				return true;
			
			// Check if it's a metal weapon by resource
			if ( MaterialInfo.IsMetalItem( weapon ) )
				return true;
			
			// Check base weapon types that can be poisoned (even if Resource = None)
			// These types match the delivery check in BaseWeapon.cs line 2065
			return weapon is BaseKnife || 
				   weapon is BaseSword || 
				   weapon is BaseSpear || 
				   weapon is BaseAxe || 
				   weapon is BasePoleArm;
		}
		
		/// <summary>
		/// Checks if item is a food type that can be poisoned.
		/// </summary>
		public static bool IsPoisonableFood( Item item )
		{
			return item is Food || 
				   item is FoodStaleBread || 
				   item is FoodDriedBeef;
		}
		
		/// <summary>
		/// Checks if item is a drink type that can be poisoned.
		/// </summary>
		public static bool IsPoisonableDrink( Item item )
		{
			return item is BaseBeverage || 
				   item is Waterskin || 
				   item is DirtyWaterskin;
		}
		
		/// <summary>
		/// Checks if item is a ninja weapon that can be poisoned.
		/// </summary>
		public static bool IsPoisonableNinjaWeapon( Item item )
		{
			return item is FukiyaDarts || item is Shuriken;
		}
		
		/// <summary>
		/// Converts poison level to food/drink poison level.
		/// </summary>
		public static int ConvertPoisonToFoodLevel( Poison poison )
		{
			if ( poison == null )
				return 1;
				
			switch ( poison.Level )
			{
				case 4: return 5; // Lethal
				case 3: return 4; // Deadly
				case 2: return 3; // Greater
				case 1: return 2; // Regular
				default: return 1; // Lesser
			}
		}
		
		/// <summary>
		/// Calculates modern mode charges based on poison level.
		/// </summary>
		public static int CalculateModernCharges( int poisonLevel )
		{
			return PoisoningConstants.MODERN_MODE_BASE_CHARGES - 
				   ( poisonLevel * PoisoningConstants.CHARGES_PER_LEVEL );
		}
		
		/// <summary>
		/// Handles item consumption (decrement amount or delete).
		/// </summary>
		public static void ConsumeItem( Item item )
		{
			if ( item == null )
				return;
				
			if ( item.Amount > 1 )
				item.Amount = item.Amount - 1;
			else
				item.Delete();
		}
		
		/// <summary>
		/// Sends failure message based on weapon type.
		/// </summary>
		public static void SendFailureMessage( Mobile from, BaseWeapon weapon )
		{
			if ( weapon == null )
			{
				from.SendLocalizedMessage( 1010518 ); // Generic failure
				return;
			}
			
			if ( weapon.Type == WeaponType.Slashing )
				from.SendLocalizedMessage( 1010516 ); // Blade failure
			else
				from.SendLocalizedMessage( 1010518 ); // Generic failure
		}
		
		/// <summary>
		/// Checks if weapon is already poisoned.
		/// </summary>
		public static bool IsWeaponAlreadyPoisoned( BaseWeapon weapon )
		{
			return weapon != null && weapon.Poison != null;
		}
		
		/// <summary>
		/// Gets poison name for display purposes.
		/// </summary>
		public static string GetPoisonName( Poison poison )
		{
			if ( poison == null )
				return "nenhum";
				
			switch ( poison.Level )
			{
				case 0: return "Lesser";
				case 1: return "Regular";
				case 2: return "Greater";
				case 3: return "Deadly";
				case 4: return "Lethal";
				default: return "desconhecido";
			}
		}
	}
}


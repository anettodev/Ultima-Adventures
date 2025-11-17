using System;
using Server.Items;

namespace Server.Items
{
	/// <summary>
	/// Extension methods for BaseWeapon to centralize weapon type checking.
	/// Eliminates 15+ repeated weapon type checks throughout BaseWeapon.
	/// </summary>
	public static class WeaponTypeExtensions
	{
		/// <summary>
		/// Checks if the weapon is a ranged weapon.
		/// </summary>
		public static bool IsRanged(this BaseWeapon weapon)
		{
			return weapon is BaseRanged;
		}
		
		/// <summary>
		/// Checks if the weapon is a harpoon type (Harpoon, GiftHarpoon, or LevelHarpoon).
		/// </summary>
		public static bool IsHarpoon(this BaseWeapon weapon)
		{
			return weapon is Harpoon || weapon is GiftHarpoon || weapon is LevelHarpoon;
		}
		
		/// <summary>
		/// Checks if the weapon is a wizard staff type.
		/// </summary>
		public static bool IsWizardStaff(this BaseWeapon weapon)
		{
			return weapon is WizardWand || 
			       weapon is BaseWizardStaff || 
			       weapon is BaseLevelStave || 
			       weapon is BaseGiftStave || 
			       weapon is GiftScepter || 
			       weapon is LevelScepter || 
			       weapon is Scepter;
		}
		
		/// <summary>
		/// Checks if the weapon is a wood-based ranged weapon.
		/// </summary>
		public static bool IsWoodRanged(this BaseWeapon weapon)
		{
			return weapon is BaseRanged && Server.Misc.MaterialInfo.IsAnyKindOfWoodItem(weapon);
		}
		
		/// <summary>
		/// Checks if the weapon supports Bushido skill bonus (Axe, Slashing, or Polearm types).
		/// </summary>
		public static bool SupportsBushido(this BaseWeapon weapon)
		{
			return weapon.Type == WeaponType.Axe || 
			       weapon.Type == WeaponType.Slashing || 
			       weapon.Type == WeaponType.Polearm;
		}
		
		/// <summary>
		/// Checks if the weapon supports Lumberjacking skill bonus (Axe type only).
		/// </summary>
		public static bool SupportsLumberjacking(this BaseWeapon weapon)
		{
			return weapon.Type == WeaponType.Axe;
		}
		
		/// <summary>
		/// Checks if the weapon supports Mining skill bonus (Bashing type only).
		/// </summary>
		public static bool SupportsMining(this BaseWeapon weapon)
		{
			return weapon.Type == WeaponType.Bashing;
		}
	}
}


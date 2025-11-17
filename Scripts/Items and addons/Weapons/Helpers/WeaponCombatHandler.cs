using System;
using Server.Mobiles;
using Server.Spells.Ninjitsu;
using Server.Spells.Bushido;

namespace Server.Items
{
	/// <summary>
	/// Centralized combat mechanics for weapons.
	/// Extracted from BaseWeapon.cs to improve maintainability and reduce cyclomatic complexity.
	/// Handles parry and dodge calculations.
	/// </summary>
	public static class WeaponCombatHandler
	{
		#region Parry Calculation
		
		/// <summary>
		/// Checks if the defender successfully parries an attack.
		/// Extracted from BaseWeapon.CheckParry to reduce complexity.
		/// </summary>
		/// <param name="defender">The mobile attempting to parry</param>
		/// <returns>True if the parry succeeds, false otherwise</returns>
		public static bool CheckParry(Mobile defender)
		{
			if (defender == null)
				return false;
			
			BaseShield shield = defender.FindItemOnLayer(Layer.TwoHanded) as BaseShield;
			
			if (shield != null)
			{
				return CheckShieldParry(defender, shield);
			}
			else if (!(defender.Weapon is Fists) && !(defender.Weapon is BaseRanged))
			{
				return CheckWeaponParry(defender);
			}
			
			return false;
		}
		
		/// <summary>
		/// Calculates shield parry chance and checks if parry succeeds.
		/// </summary>
		private static bool CheckShieldParry(Mobile defender, BaseShield shield)
		{
			double parry = defender.Skills[SkillName.Parry].Value;
			double bushidoNonRacial = defender.Skills[SkillName.Bushido].NonRacialValue;
			double bushido = defender.Skills[SkillName.Bushido].Value;
			
			// As per OSI, no negative effect from the Racial stuffs, ie, 120 parry and '0' bushido with humans
			double chance = (parry - bushidoNonRacial) / 400.0;
			
			if (chance < 0) // chance shouldn't go below 0
				chance = 0;
			
			// Parry/Bushido over 100 grants a 5% bonus
			if (parry >= 100.0 || bushido >= 100.0)
				chance += 0.05;
			
			// Evasion grants a variable bonus post ML. 50% prior
			if (Evasion.IsEvading(defender))
				chance *= Evasion.GetParryScalar(defender);
			
			// Low dexterity lowers the chance
			if (defender.Dex < WeaponConstants.PARRY_DEX_THRESHOLD)
				chance = chance * (WeaponConstants.PARRY_DEX_MULTIPLIER + defender.Dex) / 100;
			
			if (defender.IsInMidland() && defender.IsPlayerMobile())
				chance *= defender.GetAgility();
			
			// Cap parry chance at 58%
			if (chance > WeaponConstants.PARRY_CHANCE_CAP)
				chance = WeaponConstants.PARRY_CHANCE_CAP;
			
			return defender.CheckSkill(SkillName.Parry, chance);
		}
		
		/// <summary>
		/// Calculates weapon parry chance and checks if parry succeeds.
		/// </summary>
		private static bool CheckWeaponParry(Mobile defender)
		{
			BaseWeapon weapon = defender.Weapon as BaseWeapon;
			if (weapon == null)
				return false;
			
			double parry = defender.Skills[SkillName.Parry].Value;
			double bushido = defender.Skills[SkillName.Bushido].Value;
			
			double divisor = (weapon.Layer == Layer.OneHanded) ? 48000.0 : 41140.0;
			double chance = (parry * bushido) / divisor;
			double aosChance = parry / 800.0;
			
			// Parry or Bushido over 100 grant a 5% bonus
			if (parry >= WeaponConstants.PARRY_SKILL_THRESHOLD)
			{
				chance += WeaponConstants.PARRY_SKILL_BONUS;
				aosChance += WeaponConstants.PARRY_SKILL_BONUS;
			}
			else if (bushido >= WeaponConstants.PARRY_SKILL_THRESHOLD)
			{
				chance += WeaponConstants.PARRY_SKILL_BONUS;
			}
			
			// Evasion grants a variable bonus post ML. 50% prior
			if (Evasion.IsEvading(defender))
				chance *= Evasion.GetParryScalar(defender);
			
			// Low dexterity lowers the chance
			if (defender.Dex < 80)
				chance = chance * (20 + defender.Dex) / 100;
			
			if (defender.IsInMidland() && defender.IsPlayerMobile())
				chance *= defender.GetAgility();
			
			// Cap parry chance at 58%
			if (chance > WeaponConstants.PARRY_CHANCE_CAP)
				chance = WeaponConstants.PARRY_CHANCE_CAP;
			if (aosChance > WeaponConstants.PARRY_CHANCE_CAP)
				aosChance = WeaponConstants.PARRY_CHANCE_CAP;
			
			if (chance > aosChance)
				return defender.CheckSkill(SkillName.Parry, chance);
			else
				return (aosChance > Utility.RandomDouble()); // Only skillcheck if wielding a shield & there's no effect from Bushido
		}
		
		#endregion
		
		#region Dodge Calculation
		
		/// <summary>
		/// Checks if the defender successfully dodges an attack.
		/// Extracted from BaseWeapon.CheckDodge to reduce complexity.
		/// </summary>
		/// <param name="defender">The mobile attempting to dodge</param>
		/// <param name="attacker">The mobile performing the attack</param>
		/// <returns>True if the dodge succeeds, false otherwise</returns>
		public static bool CheckDodge(Mobile defender, Mobile attacker)
		{
			if (defender == null || (!(defender.IsPlayerMobile() || defender.IsBaseCreature()) || !(attacker.IsPlayerMobile() || attacker.IsBaseCreature())))
				return false; // Only basecreatures and playermobiles can do this
			
			double chance = CalculateDodgeChance(defender, attacker);
			
			return chance > Utility.RandomDouble();
		}
		
		/// <summary>
		/// Calculates the dodge chance based on agility, evasion, dexterity, and other factors.
		/// </summary>
		private static double CalculateDodgeChance(Mobile defender, Mobile attacker)
		{
			double defag = defender.GetAgility();
			double attag = attacker.GetAgility();
			
			double chance = defag / WeaponConstants.DODGE_AGILITY_DIVISOR; // max 0.8%
			
			// Evasion grants a variable bonus post ML. 50% prior
			if (Evasion.IsEvading(defender))
				chance *= (Evasion.GetParryScalar(defender) / WeaponConstants.DODGE_EVASION_DIVISOR);
			
			// Low dexterity lowers the chance
			if (defender.Dex < WeaponConstants.DODGE_DEX_THRESHOLD)
				chance = chance * (WeaponConstants.DODGE_DEX_MULTIPLIER + defender.Dex) / WeaponConstants.DURABILITY_SCALE_DIVISOR;
			
			// Midrace bonus
			BaseCreature bc = defender.AsBaseCreature();
			PlayerMobile pm = defender.AsPlayerMobile();
			if ((bc != null && bc.midrace == 3) || (pm != null && pm.midrace == 3))
				chance += WeaponConstants.DODGE_MIDRACE_BONUS;
			
			// Compare enemy agility. Attacker with higher agility can reduce chance of defender dodge
			chance += (defag - attag) / WeaponConstants.DODGE_AGILITY_COMPARISON_DIVISOR; // MAX 0.25 bonus if opponent is NOT agile
			
			// Mounted penalty
			if (defender.IsMounted())
				chance /= WeaponConstants.DODGE_MOUNTED_DIVISOR;
			
			return chance;
		}
		
		#endregion
	}

}


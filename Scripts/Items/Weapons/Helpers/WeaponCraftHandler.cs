using System;
using Server.Mobiles;
using Server.Engines.Craft;

namespace Server.Items
{
	/// <summary>
	/// Handles weapon crafting logic.
	/// Extracted from BaseWeapon.OnCraft to improve maintainability.
	/// </summary>
	public static class WeaponCraftHandler
	{
		/// <summary>
		/// Applies exceptional quality bonuses to a weapon.
		/// </summary>
		/// <param name="weapon">The weapon being crafted</param>
		/// <param name="from">The crafter</param>
		public static void ApplyExceptionalQuality(BaseWeapon weapon, Mobile from)
		{
			if (weapon == null || from == null)
				return;
			
			if (weapon.Quality != WeaponQuality.Exceptional)
				return;
			
			// Adjust weapon damage attribute
			if (weapon.Attributes.WeaponDamage > 35)
				weapon.Attributes.WeaponDamage -= 20;
			else
				weapon.Attributes.WeaponDamage = 15;
			
			// ML: Add ArmsLore bonus
			if (Core.ML)
			{
				weapon.Attributes.WeaponDamage += (int)(from.Skills.ArmsLore.Value / 20);
				
				if (weapon.Attributes.WeaponDamage > BaseWeapon.MaxWeaponDamage())
					weapon.Attributes.WeaponDamage = BaseWeapon.MaxWeaponDamage();
				
				from.CheckSkill(SkillName.ArmsLore, 0, 100);
			}
		}
		
		/// <summary>
		/// Sets the weapon hue based on resource, respecting DoNotColor context.
		/// </summary>
		/// <param name="weapon">The weapon being crafted</param>
		/// <param name="resource">The craft resource</param>
		/// <param name="doNotColor">Whether to skip coloring</param>
		public static void SetWeaponHue(BaseWeapon weapon, CraftResource resource, bool doNotColor)
		{
			if (weapon == null)
				return;
			
			if (doNotColor)
			{
				weapon.Hue = 0;
			}
			else
			{
				// Set hue based on material type using WeaponResourceMapper
				int materialHue = WeaponResourceMapper.GetMaterialHue(resource);
				if (materialHue > 0)
					weapon.Hue = materialHue;
			}
		}
		
		/// <summary>
		/// Applies resource-based craft attributes to a weapon (for non-AOS crafting).
		/// </summary>
		/// <param name="weapon">The weapon being crafted</param>
		/// <param name="resource">The craft resource</param>
		public static void ApplyResourceAttributes(BaseWeapon weapon, CraftResource resource)
		{
			if (weapon == null)
				return;
			
			WeaponDurabilityLevel? durabilityLevel;
			WeaponDamageLevel? damageLevel;
			WeaponAccuracyLevel? accuracyLevel;
			
			if (WeaponResourceMapper.GetCraftAttributes(resource, out durabilityLevel, out damageLevel, out accuracyLevel))
			{
				weapon.Identified = true;
				
				if (durabilityLevel.HasValue)
					weapon.DurabilityLevel = durabilityLevel.Value;
				
				if (damageLevel.HasValue)
					weapon.DamageLevel = damageLevel.Value;
				
				if (accuracyLevel.HasValue)
					weapon.AccuracyLevel = accuracyLevel.Value;
			}
		}
	}

}


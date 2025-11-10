using System;
using Server;
using Server.Mobiles;

namespace Server.Spells
{
	/// <summary>
	/// Centralized cure calculation helper for Magery cure spells
	/// Ensures consistent calculations across all cure spells
	/// </summary>
	public static class SpellCureCalculator
	{
		#region Constants
		private const int LETHAL_POISON_LEVEL = 4;
		private const int CURE_SUCCESS_THRESHOLD_MULTIPLIER = 2;
		private const double ARCH_CURE_POWER_MULTIPLIER = 2.0; // 2x more powerful than Cure
		private const int SKILL_DIVISOR = 10;
		private const double HEALING_SKILL_BONUS_PER_10 = 1.0; // 1% per 10 Healing skill
		#endregion
		
		/// <summary>
		/// Calculates cure chance for standard Cure spell (2nd circle)
		/// CANNOT cure lethal poison (Level 4+)
		/// </summary>
		public static int CalculateCureChance(Mobile caster, Poison poison)
		{
			if (poison == null)
				return 0;
				
			// Cure cannot cure lethal poison
			if (poison.Level >= LETHAL_POISON_LEVEL)
				return 0;
				
			int baseChance = (int)NMSUtils.getBeneficialMageryInscribePercentage(caster);
			int penalty = poison.Level; // Standard penalty (lethal already filtered)
			int skillBonus = CalculateHealingSkillBonus(caster);
			
			return Math.Max(0, baseChance + skillBonus - penalty);
		}
		
		/// <summary>
		/// Calculates cure chance for Arch Cure spell (4th circle)
		/// 2x more powerful than Cure AND has area effect
		/// CANNOT cure lethal poison (Level 4+)
		/// </summary>
		public static int CalculateArchCureChance(Mobile caster, Poison poison)
		{
			if (poison == null)
				return 0;
				
			// Arch Cure cannot cure lethal poison
			if (poison.Level >= LETHAL_POISON_LEVEL)
				return 0;
				
			int baseChance = (int)NMSUtils.getBeneficialMageryInscribePercentage(caster);
			
			// Apply 2x multiplier for Arch Cure power
			baseChance = (int)(baseChance * ARCH_CURE_POWER_MULTIPLIER);
			
			int penalty = poison.Level; // Standard penalty (lethal already filtered)
			int skillBonus = CalculateHealingSkillBonus(caster);
			
			return Math.Max(0, baseChance + skillBonus - penalty);
		}
		
		/// <summary>
		/// Calculates Healing skill bonus percentage
		/// 1% per 10 skill points
		/// </summary>
		private static int CalculateHealingSkillBonus(Mobile caster)
		{
			double healingSkill = caster.Skills[SkillName.Healing].Value;
			return (int)((healingSkill / SKILL_DIVISOR) * HEALING_SKILL_BONUS_PER_10);
		}
		
		/// <summary>
		/// Checks if cure attempt succeeds
		/// </summary>
		public static bool CheckCureSuccess(int cureChance, Poison poison)
		{
			if (poison == null)
				return false;
				
			int threshold = poison.Level * CURE_SUCCESS_THRESHOLD_MULTIPLIER;
			int roll = Utility.RandomMinMax(threshold, 100);
			return cureChance >= roll;
		}
		
		/// <summary>
		/// Checks if Arch Cure can attempt to cure this poison
		/// </summary>
		public static bool CanArchCurePoison(Poison poison)
		{
			if (poison == null)
				return false;
				
			// Arch Cure cannot cure lethal poison
			return poison.Level < LETHAL_POISON_LEVEL;
		}
		
		/// <summary>
		/// Checks if Cure can attempt to cure this poison
		/// </summary>
		public static bool CanCurePoison(Poison poison)
		{
			if (poison == null)
				return false;
				
			// Cure cannot cure lethal poison
			return poison.Level < LETHAL_POISON_LEVEL;
		}
	}
}


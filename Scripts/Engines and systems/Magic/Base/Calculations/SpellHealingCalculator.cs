using System;
using Server;
using Server.Mobiles;

namespace Server.Spells
{
	/// <summary>
	/// Centralized healing calculation helper for Magery healing spells
	/// Ensures consistent calculations across all healing spells
	/// </summary>
	public static class SpellHealingCalculator
	{
		#region Constants
		private const double BASE_HEAL_DIVISOR = 3.0;
		private const double GREATER_HEAL_MULTIPLIER = 2.0; // 2x more powerful than Heal
		private const double HEAL_POWER_REDUCTION = 0.7; // 30% reduction
		private const double OTHER_TARGET_BONUS = 1.15; // 15% bonus (same for both)
		private const double RANDOM_VARIANCE_MIN = 0.02; // 2%
		private const double RANDOM_VARIANCE_MAX = 0.05; // 5%
		private const double CONSECUTIVE_CAST_PENALTY = 0.25; // 25%
		private const int MINIMUM_HEAL_AMOUNT = 1;
		
		// Skill bonus constants
		private const double INSCRIPTION_BONUS_PER_10 = 0.3; // 0.3 heal points per 10 Inscription
		private const int HEALING_SKILL_BONUS_PER_10 = 1; // 1 heal point per 10 Healing skill
		private const int SKILL_DIVISOR = 10;
		
		// Self-heal bonus (you know your body better)
		private const double SELF_HEAL_BONUS_PERCENT = 0.05; // 5% bonus when healing yourself
		#endregion
		
		/// <summary>
		/// Calculates heal amount for standard Heal spell (1st circle)
		/// </summary>
		public static int CalculateHeal(Mobile caster, Mobile target, bool isConsecutiveCast)
		{
			int baseHeal = CalculateBaseHeal(caster, BASE_HEAL_DIVISOR);
			baseHeal = ApplySkillBonuses(baseHeal, caster);
			baseHeal = ApplyOtherTargetBonus(baseHeal, caster, target);
			baseHeal = ApplyPowerReduction(baseHeal);
			baseHeal = ApplyRandomVariance(baseHeal);
			
			if (isConsecutiveCast)
				baseHeal = ApplyConsecutiveCastPenalty(baseHeal);
			
			// Apply self-heal bonus (you know your body better)
			baseHeal = ApplySelfHealBonus(baseHeal, caster, target);
				
			return Math.Max(baseHeal, MINIMUM_HEAL_AMOUNT);
		}
		
		/// <summary>
		/// Calculates heal amount for Greater Heal spell (4th circle)
		/// Uses same formula as Heal but with 2.0x multiplier
		/// </summary>
		public static int CalculateGreaterHeal(Mobile caster, Mobile target, bool isConsecutiveCast)
		{
			// Use same base calculation as Heal
			int baseHeal = CalculateBaseHeal(caster, BASE_HEAL_DIVISOR);
			baseHeal = ApplySkillBonuses(baseHeal, caster);
			
			// Apply 2.0x multiplier for Greater Heal
			baseHeal = (int)(baseHeal * GREATER_HEAL_MULTIPLIER);
			
			// Apply same bonuses and reductions as Heal
			baseHeal = ApplyOtherTargetBonus(baseHeal, caster, target);
			baseHeal = ApplyPowerReduction(baseHeal);
			baseHeal = ApplyRandomVariance(baseHeal);
			
			if (isConsecutiveCast)
				baseHeal = ApplyConsecutiveCastPenalty(baseHeal);
			
			// Apply self-heal bonus (you know your body better)
			baseHeal = ApplySelfHealBonus(baseHeal, caster, target);
				
			return Math.Max(baseHeal, MINIMUM_HEAL_AMOUNT);
		}
		
		/// <summary>
		/// Calculates base heal amount from beneficial magery/inscribe percentage
		/// </summary>
		private static int CalculateBaseHeal(Mobile caster, double divisor)
		{
			return (int)(NMSUtils.getBeneficialMageryInscribePercentage(caster) / divisor);
		}
		
		/// <summary>
		/// Applies skill bonuses: Inscription and Healing
		/// </summary>
		private static int ApplySkillBonuses(int heal, Mobile caster)
		{
			// Inscription bonus: 0.3 heal points per 10 skill points (use Ceiling)
			double inscribeSkill = caster.Skills[SkillName.Inscribe].Value;
			int inscribeBonus = (int)Math.Ceiling((inscribeSkill / SKILL_DIVISOR) * INSCRIPTION_BONUS_PER_10);
			
			// Healing skill bonus: 1 heal point per 10 skill points
			double healingSkill = caster.Skills[SkillName.Healing].Value;
			int healingBonus = (int)(healingSkill / SKILL_DIVISOR) * HEALING_SKILL_BONUS_PER_10;
			
			return heal + inscribeBonus + healingBonus;
		}
		
		/// <summary>
		/// Applies bonus when healing others (15% bonus)
		/// </summary>
		private static int ApplyOtherTargetBonus(int heal, Mobile caster, Mobile target)
		{
			return (caster == target) ? heal : (int)(heal * OTHER_TARGET_BONUS);
		}
		
		/// <summary>
		/// Applies 30% power reduction (70% of original)
		/// </summary>
		private static int ApplyPowerReduction(int heal)
		{
			return (int)(heal * HEAL_POWER_REDUCTION);
		}
		
		/// <summary>
		/// Applies random variance (2-5% reduction)
		/// </summary>
		private static int ApplyRandomVariance(int heal)
		{
			double variance = RANDOM_VARIANCE_MIN + 
				(Utility.RandomDouble() * (RANDOM_VARIANCE_MAX - RANDOM_VARIANCE_MIN));
			int reduction = Math.Max(1, (int)(heal * variance));
			return heal - reduction;
		}
		
		/// <summary>
		/// Applies consecutive cast penalty (25% reduction)
		/// </summary>
		private static int ApplyConsecutiveCastPenalty(int heal)
		{
			int penalty = Math.Max(1, (int)(heal * CONSECUTIVE_CAST_PENALTY));
			return heal - penalty;
		}
		
		/// <summary>
		/// Applies self-heal bonus (5% bonus when healing yourself)
		/// You know your body better than others
		/// </summary>
		private static int ApplySelfHealBonus(int heal, Mobile caster, Mobile target)
		{
			if (caster == target)
			{
				int bonus = (int)Math.Ceiling(heal * SELF_HEAL_BONUS_PERCENT);
				return heal + bonus;
			}
			return heal;
		}
	}
}


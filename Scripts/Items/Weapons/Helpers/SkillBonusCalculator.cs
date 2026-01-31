using System;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Configuration for skill bonus calculations.
	/// </summary>
	public class SkillBonusConfig
	{
		public SkillName Skill { get; set; }
		public double Scalar { get; set; }
		public double Threshold { get; set; }
		public double Offset { get; set; }
		public Func<BaseWeapon, bool> WeaponTypeValidator { get; set; }
	}
	
	/// <summary>
	/// Centralizes all skill bonus calculations using configuration-driven approach.
	/// Replaces 12+ separate GetBonus() calls with a single configuration system.
	/// </summary>
	public static class SkillBonusCalculator
	{
		#region Constants
		
		/// <summary>
		/// Strength bonus scalar (0.1% per point).
		/// </summary>
		private const double STRENGTH_SCALAR = 0.100;
		
		/// <summary>
		/// Strength bonus threshold (100+ gets additional bonus).
		/// </summary>
		private const double STRENGTH_THRESHOLD = 100.0;
		
		/// <summary>
		/// Strength bonus offset at threshold (+3% at 100+).
		/// </summary>
		private const double STRENGTH_OFFSET = 3.00;
		
		#endregion
		
		#region Skill Configuration Table
		
		private static readonly Dictionary<SkillName, SkillBonusConfig> SkillConfigs = 
			new Dictionary<SkillName, SkillBonusConfig>
		{
			{ SkillName.Anatomy, new SkillBonusConfig 
				{ Skill = SkillName.Anatomy, Scalar = 0.200, Threshold = 100.0, Offset = 3.00, WeaponTypeValidator = null } },
			{ SkillName.Tactics, new SkillBonusConfig 
				{ Skill = SkillName.Tactics, Scalar = 0.200, Threshold = 100.0, Offset = 3.00, WeaponTypeValidator = null } },
			{ SkillName.Lumberjacking, new SkillBonusConfig 
				{ Skill = SkillName.Lumberjacking, Scalar = 0.200, Threshold = 100.0, Offset = 10.00, 
				  WeaponTypeValidator = w => w.SupportsLumberjacking() } },
			{ SkillName.Mining, new SkillBonusConfig 
				{ Skill = SkillName.Mining, Scalar = 0.200, Threshold = 100.0, Offset = 10.00,
				  WeaponTypeValidator = w => w.SupportsMining() } },
			{ SkillName.Fishing, new SkillBonusConfig 
				{ Skill = SkillName.Fishing, Scalar = 0.200, Threshold = 100.0, Offset = 10.00,
				  WeaponTypeValidator = w => w.IsHarpoon() } },
			{ SkillName.Bushido, new SkillBonusConfig 
				{ Skill = SkillName.Bushido, Scalar = 0.300, Threshold = 100.0, Offset = 6.25,
				  WeaponTypeValidator = w => w.SupportsBushido() } },
			{ SkillName.Ninjitsu, new SkillBonusConfig 
				{ Skill = SkillName.Ninjitsu, Scalar = 0.300, Threshold = 100.0, Offset = 6.25, WeaponTypeValidator = null } },
			{ SkillName.Necromancy, new SkillBonusConfig 
				{ Skill = SkillName.Necromancy, Scalar = 0.300, Threshold = 100.0, Offset = 6.25,
				  WeaponTypeValidator = w => w.IsWizardStaff() } },
			{ SkillName.Magery, new SkillBonusConfig 
				{ Skill = SkillName.Magery, Scalar = 0.300, Threshold = 100.0, Offset = 6.25,
				  WeaponTypeValidator = w => w.IsWizardStaff() } },
			{ SkillName.Fletching, new SkillBonusConfig 
				{ Skill = SkillName.Fletching, Scalar = 0.300, Threshold = 100.0, Offset = 6.25,
				  WeaponTypeValidator = w => w.IsWoodRanged() } }
		};
		
		#endregion
		
		#region Public Methods
		
		/// <summary>
		/// Calculates the strength bonus for damage.
		/// </summary>
		public static double CalculateStrengthBonus(int strength)
		{
			return CalculateBonus(strength, STRENGTH_SCALAR, STRENGTH_THRESHOLD, STRENGTH_OFFSET);
		}
		
		/// <summary>
		/// Calculates the skill bonus for a specific skill and weapon combination.
		/// </summary>
		public static double CalculateSkillBonus(Mobile attacker, SkillName skill, BaseWeapon weapon)
		{
			if (!SkillConfigs.ContainsKey(skill))
				return 0.0;
				
			SkillBonusConfig config = SkillConfigs[skill];
			
			// Check weapon type validator
			if (config.WeaponTypeValidator != null && !config.WeaponTypeValidator(weapon))
				return 0.0;
			
			double skillValue = attacker.Skills[skill].Value;
			return CalculateBonus(skillValue, config.Scalar, config.Threshold, config.Offset);
		}
		
		/// <summary>
		/// Calculates all skill bonuses for an attacker and weapon combination.
		/// Returns a dictionary with SkillName as key and bonus value as value.
		/// Note: Strength is not a skill, but is included for convenience.
		/// </summary>
		public static Dictionary<string, double> CalculateAllSkillBonuses(Mobile attacker, BaseWeapon weapon)
		{
			Dictionary<string, double> bonuses = new Dictionary<string, double>();
			
			// Strength bonus (special case, not a skill)
			bonuses["Strength"] = CalculateStrengthBonus(attacker.Str);
			
			// All skill bonuses
			foreach (SkillName skill in SkillConfigs.Keys)
			{
				bonuses[skill.ToString()] = CalculateSkillBonus(attacker, skill, weapon);
			}
			
			return bonuses;
		}
		
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Core bonus calculation formula: (value * scalar + offset if >= threshold) / 100
		/// </summary>
		private static double CalculateBonus(double value, double scalar, double threshold, double offset)
		{
			double bonus = value * scalar;
			if (value >= threshold)
				bonus += offset;
			return bonus / 100.0;
		}
		
		#endregion
	}
}


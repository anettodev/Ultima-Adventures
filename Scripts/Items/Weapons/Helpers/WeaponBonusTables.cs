using System.Collections.Generic;

namespace Server.Items
{
	/// <summary>
	/// Provides dictionary-based lookups for weapon bonus values.
	/// Replaces switch statements with O(1) dictionary lookups for better performance.
	/// </summary>
	public static class WeaponBonusTables
	{
		#region Quality Bonuses
		
		private static readonly Dictionary<WeaponQuality, int> QualityBonuses = 
			new Dictionary<WeaponQuality, int>
		{
			{ WeaponQuality.Low, -10 },
			{ WeaponQuality.Regular, 0 },
			{ WeaponQuality.Exceptional, 5 }
		};
		
		/// <summary>
		/// Gets the damage bonus for a weapon quality level.
		/// </summary>
		public static int GetQualityBonus(WeaponQuality quality)
		{
			int bonus;
			if (QualityBonuses.TryGetValue(quality, out bonus))
				return bonus;
			return 0;
		}
		
		#endregion
		
		#region Damage Level Bonuses
		
		private static readonly Dictionary<WeaponDamageLevel, int> DamageLevelBonuses = 
			new Dictionary<WeaponDamageLevel, int>
		{
			{ WeaponDamageLevel.Regular, 0 },
			{ WeaponDamageLevel.Ruin, 5 },
			{ WeaponDamageLevel.Might, 10 },
			{ WeaponDamageLevel.Force, 12 },
			{ WeaponDamageLevel.Power, 15 },
			{ WeaponDamageLevel.Vanq, 18 }
		};
		
		/// <summary>
		/// Gets the damage bonus for a weapon damage level.
		/// </summary>
		public static int GetDamageLevelBonus(WeaponDamageLevel level)
		{
			int bonus;
			if (DamageLevelBonuses.TryGetValue(level, out bonus))
				return bonus;
			return 0;
		}
		
		#endregion
		
		#region Accuracy Level Bonuses
		
		private static readonly Dictionary<WeaponAccuracyLevel, int> AccuracyLevelBonuses = 
			new Dictionary<WeaponAccuracyLevel, int>
		{
			{ WeaponAccuracyLevel.Regular, 0 },
			{ WeaponAccuracyLevel.Accurate, 2 },
			{ WeaponAccuracyLevel.Surpassingly, 4 },
			{ WeaponAccuracyLevel.Eminently, 6 },
			{ WeaponAccuracyLevel.Exceedingly, 8 },
			{ WeaponAccuracyLevel.Supremely, 10 }
		};
		
		/// <summary>
		/// Gets the hit chance bonus for a weapon accuracy level.
		/// </summary>
		public static int GetAccuracyLevelBonus(WeaponAccuracyLevel level)
		{
			int bonus;
			if (AccuracyLevelBonuses.TryGetValue(level, out bonus))
				return bonus;
			return 0;
		}
		
		#endregion
	}
}


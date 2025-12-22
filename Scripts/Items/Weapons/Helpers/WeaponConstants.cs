namespace Server.Items
{
	/// <summary>
	/// Centralized constants for weapon calculations and combat mechanics.
	/// Extracted from BaseWeapon.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class WeaponConstants
	{
		#region Combat Bonuses
		
		/// <summary>Bonus damage when mounted in Midland</summary>
		public const int MIDLAND_MOUNTED_BONUS = 15;
		
		/// <summary>Agility multiplier for Midland combat calculations</summary>
		public const int MIDLAND_AGILITY_MULTIPLIER = 15;
		
		/// <summary>Damage bonus from Divine Fury spell</summary>
		public const int DIVINE_FURY_BONUS = 10;
		
		/// <summary>Defense malus when under Divine Fury effect</summary>
		public const int DIVINE_FURY_DEFENSE_MALUS = 20;
		
		/// <summary>Damage bonus from Horrific Beast transformation</summary>
		public const int HORRIFIC_BEAST_BONUS = 25;
		
		/// <summary>Attack malus from Hit Lower Attack effect</summary>
		public const int HIT_LOWER_ATTACK_MALUS = 25;
		
		/// <summary>Damage bonus when slayer weapon matches target</summary>
		public const int SLAYER_BONUS = 75;
		
		/// <summary>Damage bonus from Enemy of One ability</summary>
		public const int ENEMY_OF_ONE_BONUS = 50;
		
		/// <summary>Damage bonus when Honor is active</summary>
		public const int HONOR_ACTIVE_BONUS = 25;
		
		/// <summary>Damage bonus from transformations</summary>
		public const int TRANSFORMATION_DAMAGE_BONUS = 25;
		
		#endregion
		
		#region Skill Check Values
		
		/// <summary>Minimum value for hiding skill check</summary>
		public const int HIDING_SKILL_CHECK_MIN = 1;
		
		/// <summary>Maximum value for hiding skill check</summary>
		public const int HIDING_SKILL_CHECK_MAX = 125;
		
		/// <summary>Minimum value for stealth skill check</summary>
		public const int STEALTH_SKILL_CHECK_MIN = 1;
		
		/// <summary>Maximum value for stealth skill check</summary>
		public const int STEALTH_SKILL_CHECK_MAX = 125;
		
		#endregion
		
		#region Dodge Calculation
		
		/// <summary>Divisor for agility in dodge chance calculation</summary>
		public const double DODGE_AGILITY_DIVISOR = 1.25;
		
		/// <summary>Divisor for agility comparison in dodge calculation</summary>
		public const double DODGE_AGILITY_COMPARISON_DIVISOR = 4.0;
		
		/// <summary>Divisor for evasion bonus in dodge calculation</summary>
		public const double DODGE_EVASION_DIVISOR = 2.0;
		
		/// <summary>Dexterity threshold for dodge calculation</summary>
		public const int DODGE_DEX_THRESHOLD = 50;
		
		/// <summary>Multiplier for dexterity in dodge calculation</summary>
		public const int DODGE_DEX_MULTIPLIER = 20;
		
		/// <summary>Divisor for mounted dodge chance</summary>
		public const int DODGE_MOUNTED_DIVISOR = 2;
		
		/// <summary>Bonus dodge chance for Midrace characters</summary>
		public const double DODGE_MIDRACE_BONUS = 0.1;
		
		#endregion
		
		#region Hit Chance Calculation
		
		/// <summary>Attack value offset for hit chance calculation</summary>
		public const double HIT_CHANCE_ATK_VALUE_OFFSET = 20.0;
		
		/// <summary>Minimum value for hit chance calculation</summary>
		public const double HIT_CHANCE_MIN_VALUE = -19.9;
		
		/// <summary>Maximum value for hit chance calculation</summary>
		public const double HIT_CHANCE_MAX_VALUE = -20.0;
		
		#endregion
		
		#region Damage Caps
		
		/// <summary>Maximum damage percentage bonus cap</summary>
		public const int MAX_DAMAGE_PERCENTAGE_BONUS = 300;
		
		#endregion
		
		#region Blood Effects
		
		/// <summary>Minimum extra blood for SE</summary>
		public const int EXTRA_BLOOD_SE_MIN = 3;
		
		/// <summary>Maximum extra blood for SE</summary>
		public const int EXTRA_BLOOD_SE_MAX = 4;
		
		/// <summary>Minimum extra blood for Classic</summary>
		public const int EXTRA_BLOOD_CLASSIC_MIN = 0;
		
		/// <summary>Maximum extra blood for Classic</summary>
		public const int EXTRA_BLOOD_CLASSIC_MAX = 1;
		
		/// <summary>Minimum blood spread distance</summary>
		public const int BLOOD_SPREAD_MIN = -1;
		
		/// <summary>Maximum blood spread distance</summary>
		public const int BLOOD_SPREAD_MAX = 1;
		
		#endregion
		
		#region Durability Scaling
		
		/// <summary>Base scale for durability calculations</summary>
		public const int DURABILITY_BASE_SCALE = 50;
		
		/// <summary>Divisor for durability scaling</summary>
		public const int DURABILITY_SCALE_DIVISOR = 100;
		
		/// <summary>Rounding offset for durability calculations</summary>
		public const int DURABILITY_ROUNDING_OFFSET = 99;
		
		#endregion
		
		#region Parry Calculation
		
		/// <summary>Dexterity threshold for parry calculation</summary>
		public const int PARRY_DEX_THRESHOLD = 80;
		
		/// <summary>Multiplier for dexterity in parry calculation</summary>
		public const int PARRY_DEX_MULTIPLIER = 20;
		
		/// <summary>Bonus parry chance when skill >= threshold</summary>
		public const double PARRY_SKILL_BONUS = 0.05;
		
		/// <summary>Skill threshold for parry bonus</summary>
		public const double PARRY_SKILL_THRESHOLD = 100.0;
		
		/// <summary>Maximum parry chance cap (58%)</summary>
		public const double PARRY_CHANCE_CAP = 0.58;
		
		#endregion
		
		#region Wraith Form Leech
		
		/// <summary>Base mana leech for Wraith Form</summary>
		public const int WRAITH_LEECH_BASE = 5;
		
		/// <summary>Multiplier for Spirit Speak skill in Wraith Form leech</summary>
		public const int WRAITH_LEECH_MULTIPLIER = 15;
		
		#endregion
		
		#region Accuracy Level
		
		/// <summary>Multiplier for accuracy level skill modification</summary>
		public const int ACCURACY_LEVEL_SKILL_MOD_MULTIPLIER = 5;
		
		#endregion
		
		#region Weapon Type Multipliers
		
		/// <summary>Damage multiplier for metal weapons (25% more than wood)</summary>
		public const double METAL_WEAPON_DAMAGE_MULTIPLIER = 1.25;
		
		/// <summary>Base damage reduction factor (10% reduction, rounded up)</summary>
		public const double BASE_DAMAGE_REDUCTION = 0.9;
		
		#endregion
	}
}


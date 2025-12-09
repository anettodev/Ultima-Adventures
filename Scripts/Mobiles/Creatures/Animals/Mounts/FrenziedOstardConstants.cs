namespace Server.Mobiles
{
	/// <summary>
	/// Centralized constants for FrenziedOstard calculations and mechanics.
	/// Extracted from FrenziedOstard.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class FrenziedOstardConstants
	{
		#region Body and Item IDs
		/// <summary>Body ID for FrenziedOstard</summary>
		public const int BODY_ID = 0xDB;
		
		/// <summary>Item ID for FrenziedOstard mount item</summary>
		public const int ITEM_ID = 0xDB;
		
		/// <summary>Base sound ID for FrenziedOstard</summary>
		public const int BASE_SOUND_ID = 0x275;
		#endregion

		#region Spawn Probability
		/// <summary>Probability threshold for greater variant spawn (20% chance)</summary>
		public const double GREATER_VARIANT_CHANCE = 0.80;
		#endregion

		#region Hue Values
		/// <summary>Special hue for greater variant</summary>
		public const int HUE_GREATER = 1154;
		
		/// <summary>Default hue for regular variant</summary>
		public const int HUE_REGULAR = 0;
		#endregion

		#region Speed Values
		/// <summary>Active speed for regular variant</summary>
		public const int REGULAR_ACTIVE_SPEED = 10;
		
		/// <summary>Passive speed for regular variant</summary>
		public const int REGULAR_PASSIVE_SPEED = 1;
		
		/// <summary>Active speed for greater variant</summary>
		public const int GREATER_ACTIVE_SPEED = 11;
		
		/// <summary>Passive speed for greater variant</summary>
		public const int GREATER_PASSIVE_SPEED = 2;
		
		/// <summary>Active speed passive multiplier</summary>
		public const double ACTIVE_SPEED_PASSIVE = 0.2;
		
		/// <summary>Passive speed passive multiplier</summary>
		public const double PASSIVE_SPEED_PASSIVE = 0.4;
		#endregion

		#region Greater Variant Stats
		/// <summary>Minimum strength for greater variant</summary>
		public const int GREATER_STR_MIN = 185;
		
		/// <summary>Maximum strength for greater variant</summary>
		public const int GREATER_STR_MAX = 305;
		
		/// <summary>Minimum dexterity for greater variant</summary>
		public const int GREATER_DEX_MIN = 105;
		
		/// <summary>Maximum dexterity for greater variant</summary>
		public const int GREATER_DEX_MAX = 445;
		
		/// <summary>Minimum intelligence for greater variant</summary>
		public const int GREATER_INT_MIN = 6;
		
		/// <summary>Maximum intelligence for greater variant</summary>
		public const int GREATER_INT_MAX = 100;
		
		/// <summary>Minimum hits for greater variant</summary>
		public const int GREATER_HITS_MIN = 200;
		
		/// <summary>Maximum hits for greater variant</summary>
		public const int GREATER_HITS_MAX = 350;
		
		/// <summary>Minimum damage for greater variant</summary>
		public const int GREATER_DAMAGE_MIN = 23;
		
		/// <summary>Maximum damage for greater variant</summary>
		public const int GREATER_DAMAGE_MAX = 30;
		
		/// <summary>Minimum physical resistance for greater variant</summary>
		public const int GREATER_PHYSICAL_RESIST_MIN = 25;
		
		/// <summary>Maximum physical resistance for greater variant</summary>
		public const int GREATER_PHYSICAL_RESIST_MAX = 80;
		
		/// <summary>Minimum taming skill required for greater variant</summary>
		public const double GREATER_MIN_TAME_SKILL = 90.1;
		#endregion

		#region Regular Variant Stats
		/// <summary>Minimum strength for regular variant</summary>
		public const int REGULAR_STR_MIN = 105;
		
		/// <summary>Maximum strength for regular variant</summary>
		public const int REGULAR_STR_MAX = 255;
		
		/// <summary>Minimum dexterity for regular variant</summary>
		public const int REGULAR_DEX_MIN = 105;
		
		/// <summary>Maximum dexterity for regular variant</summary>
		public const int REGULAR_DEX_MAX = 345;
		
		/// <summary>Minimum intelligence for regular variant</summary>
		public const int REGULAR_INT_MIN = 6;
		
		/// <summary>Maximum intelligence for regular variant</summary>
		public const int REGULAR_INT_MAX = 100;
		
		/// <summary>Minimum hits for regular variant</summary>
		public const int REGULAR_HITS_MIN = 100;
		
		/// <summary>Maximum hits for regular variant</summary>
		public const int REGULAR_HITS_MAX = 250;
		
		/// <summary>Minimum damage for regular variant</summary>
		public const int REGULAR_DAMAGE_MIN = 20;
		
		/// <summary>Maximum damage for regular variant</summary>
		public const int REGULAR_DAMAGE_MAX = 25;
		
		/// <summary>Minimum physical resistance for regular variant</summary>
		public const int REGULAR_PHYSICAL_RESIST_MIN = 25;
		
		/// <summary>Maximum physical resistance for regular variant</summary>
		public const int REGULAR_PHYSICAL_RESIST_MAX = 70;
		
		/// <summary>Minimum taming skill required for regular variant</summary>
		public const double REGULAR_MIN_TAME_SKILL = 85.1;
		#endregion

		#region Common Stats
		/// <summary>Mana value (always 0 for FrenziedOstard)</summary>
		public const int MANA = 0;
		
		/// <summary>Damage type percentage (100% physical)</summary>
		public const int DAMAGE_TYPE_PHYSICAL_PERCENT = 100;
		
		/// <summary>Minimum fire resistance</summary>
		public const int FIRE_RESIST_MIN = 10;
		
		/// <summary>Maximum fire resistance</summary>
		public const int FIRE_RESIST_MAX = 55;
		
		/// <summary>Minimum poison resistance</summary>
		public const int POISON_RESIST_MIN = 20;
		
		/// <summary>Maximum poison resistance</summary>
		public const int POISON_RESIST_MAX = 55;
		
		/// <summary>Minimum energy resistance</summary>
		public const int ENERGY_RESIST_MIN = 20;
		
		/// <summary>Maximum energy resistance</summary>
		public const int ENERGY_RESIST_MAX = 55;
		
		/// <summary>Minimum magic resist skill</summary>
		public const double MAGIC_RESIST_SKILL_MIN = 75.1;
		
		/// <summary>Maximum magic resist skill</summary>
		public const double MAGIC_RESIST_SKILL_MAX = 80.0;
		
		/// <summary>Minimum tactics skill</summary>
		public const double TACTICS_SKILL_MIN = 79.3;
		
		/// <summary>Maximum tactics skill</summary>
		public const double TACTICS_SKILL_MAX = 94.0;
		
		/// <summary>Minimum wrestling skill</summary>
		public const double WRESTLING_SKILL_MIN = 79.3;
		
		/// <summary>Maximum wrestling skill</summary>
		public const double WRESTLING_SKILL_MAX = 94.0;
		
		/// <summary>Fame value</summary>
		public const int FAME = 1500;
		
		/// <summary>Karma value</summary>
		public const int KARMA = -1500;
		
		/// <summary>Control slots required</summary>
		public const int CONTROL_SLOTS = 1;
		#endregion

		#region Base Constructor Parameters
		/// <summary>AI type for FrenziedOstard</summary>
		public const AIType AI_TYPE = AIType.AI_Melee;
		
		/// <summary>Fight mode for FrenziedOstard</summary>
		public const FightMode FIGHT_MODE = FightMode.Closest;
		
		/// <summary>Range perception</summary>
		public const int RANGE_PERCEPTION = 10;
		
		/// <summary>Range fight</summary>
		public const int RANGE_FIGHT = 1;
		#endregion
	}
}


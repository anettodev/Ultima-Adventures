using System;

namespace Server.Items
{
	/// <summary>
	/// Centralized constants for potion calculations and mechanics.
	/// Extracted from BasePotion.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class PotionConstants
	{
		#region Potion Physical Properties

		/// <summary>Default weight for all potions (in stones)</summary>
		public const double DEFAULT_POTION_WEIGHT = 0.65;

		/// <summary>Maximum distance from which a potion can be used</summary>
		public const int USE_DISTANCE = 1;

		#endregion

	#region Effects and Animations

	/// <summary>Sound ID played when drinking a potion</summary>
	public const int DRINK_SOUND_ID = 0x2D6;

	/// <summary>Animation ID for drinking animation</summary>
	public const int DRINK_ANIMATION_ID = 34;

	/// <summary>Number of frames in the drink animation</summary>
	public const int DRINK_ANIMATION_FRAMES = 5;

	/// <summary>Number of times the drink animation repeats</summary>
	public const int DRINK_ANIMATION_REPEAT = 1;

	#endregion

	#region Cooldown System

	/// <summary>Global cooldown between drinking any potions (in seconds)</summary>
	/// <remarks>Prevents rapid combo-chugging of multiple potions (e.g., heal + mana)</remarks>
	public const double DRINK_COOLDOWN_SECONDS = 1.5;

	/// <summary>Message color for cooldown warnings (red)</summary>
	public const int MSG_COLOR_COOLDOWN = 0x22;

	#endregion

		#region Alchemy Skill System

		/// <summary>Grandmaster alchemy skill level (99.0 fixed points)</summary>
		public const int ALCHEMY_GRANDMASTER = 99;

		/// <summary>Expert alchemy skill level (66.0 fixed points)</summary>
		public const int ALCHEMY_EXPERT = 66;

		/// <summary>Journeyman alchemy skill level (33.0 fixed points)</summary>
		public const int ALCHEMY_JOURNEYMAN = 33;

		/// <summary>Enhancement bonus for grandmaster alchemists</summary>
		public const int ALCHEMY_BONUS_GM = 30;

		/// <summary>Enhancement bonus for expert alchemists</summary>
		public const int ALCHEMY_BONUS_EXPERT = 20;

		/// <summary>Enhancement bonus for journeyman alchemists</summary>
		public const int ALCHEMY_BONUS_JOURNEYMAN = 10;

		#endregion

		#region Enhancement Scaling

		/// <summary>Base scalar for potion enhancement calculations</summary>
		public const double ENHANCE_POTION_SCALAR_BASE = 1.0;

		/// <summary>Multiplier for enhance potions percentage conversion (1% = 0.01)</summary>
		public const double ENHANCE_POTION_SCALAR_MULTIPLIER = 0.01;

		#endregion

		#region Sci-Fi Theme Item IDs

		/// <summary>Item ID for fuel canister (fire-based potions)</summary>
		public const int ITEMID_FUEL_CANISTER = 0x34D7;

		/// <summary>Item ID for fire extinguisher (cold-based potions)</summary>
		public const int ITEMID_EXTINGUISHER = 0x3563;

		/// <summary>Item ID for liquid bottle (explosion potions)</summary>
		public const int ITEMID_LIQUID_BOTTLE = 0x1FDD;

		/// <summary>Item ID for pill bottle (standard pills)</summary>
		public const int ITEMID_PILL_BOTTLE = 0x27FE;

		/// <summary>Item ID for syringe (injectable serums)</summary>
		public const int ITEMID_SYRINGE = 0x27FF;

		/// <summary>Hue for halon fire extinguisher</summary>
		public const int HUE_HALON = 0xB50;

		/// <summary>Default hue (no color)</summary>
		public const int HUE_DEFAULT = 0;

		#endregion

		#region Keg Integration

		/// <summary>Maximum capacity of a potion keg</summary>
		public const int KEG_MAX_CAPACITY = 100;

		/// <summary>Minimum capacity of a potion keg (empty)</summary>
		public const int KEG_MIN_CAPACITY = 0;

		#endregion

		#region Serialization

		/// <summary>Current serialization version for BasePotion</summary>
		public const int SERIALIZATION_VERSION = 1;

		/// <summary>Legacy serialization version</summary>
		public const int LEGACY_VERSION = 0;

		#endregion
	}
}


using System;

namespace Server.Items
{
	/// <summary>
	/// Centralized constants for CrystallineJar item mechanics.
	/// Extracted from CrystallineJar.cs to improve maintainability.
	/// </summary>
	public static class CrystallineJarConstants
	{
		#region Item Properties

		/// <summary>Item ID for crystalline jar graphic (UO client item 0x2828)</summary>
		public const int ITEM_ID_CRYSTALLINE_JAR = 0x2828;

		/// <summary>Weight of empty crystalline jar in stones</summary>
		public const double WEIGHT_EMPTY = 1.0;

		/// <summary>Weight of jar with holy water</summary>
		public const double WEIGHT_HOLY_WATER = 2.0;

		/// <summary>Weight threshold for glow effect when throwing</summary>
		public const double WEIGHT_GLOW_THRESHOLD = 2.0;

		/// <summary>Hue for empty crystalline jar (colorless)</summary>
		public const int HUE_EMPTY = 0;

		/// <summary>Hue for holy water in jar</summary>
		public const int HUE_HOLY_WATER = 0x539;

		#endregion

		#region Range and Distance

		/// <summary>Maximum range for scooping substances (tiles)</summary>
		public const int SCOOP_RANGE = 1;

		/// <summary>Maximum distance to scoop substance (squared distance)</summary>
		public const int MAX_SCOOP_DISTANCE = 2;

		/// <summary>Maximum range for throwing jar contents (tiles)</summary>
		public const int THROW_RANGE = 12;

		/// <summary>Maximum distance to throw jar contents (squared distance)</summary>
		public const int MAX_THROW_DISTANCE = 8;

		#endregion

		#region Sound Effects

		/// <summary>Sound ID for scooping substance into jar</summary>
		public const int SOUND_ID_SCOOP = 0x23F;

		#endregion

		#region Throw Results

		/// <summary>Throw success indicator</summary>
		public const int THROW_SUCCESS = 1;

		/// <summary>Throw failure indicator</summary>
		public const int THROW_FAIL = 0;

		#endregion

		#region Property Display

		/// <summary>Cliloc number for "Holds Odd Substances" property</summary>
		public const int CLILOC_HOLDS_SUBSTANCES = 1070722;

		#endregion

		#region Serialization

		/// <summary>Current serialization version for backwards compatibility</summary>
		public const int SERIALIZATION_VERSION = 0;

		#endregion
	}
}


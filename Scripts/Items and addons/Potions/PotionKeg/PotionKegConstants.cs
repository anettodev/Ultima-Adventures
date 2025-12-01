using System;

namespace Server.Items
{
	/// <summary>
	/// Centralized constants for PotionKeg mechanics and display.
	/// Extracted from PotionKeg.cs to improve maintainability.
	/// </summary>
	public static class PotionKegConstants
	{
		#region Capacity and Weight
		
		/// <summary>Maximum number of potions a keg can hold</summary>
		public const int MAX_CAPACITY = 100;
		
		/// <summary>Base weight of empty keg</summary>
		public const int BASE_WEIGHT = 20;
		
		/// <summary>Weight contributed by full keg (100 potions)</summary>
		public const int FULL_WEIGHT_ADDITION = 80;
		
		#endregion
		
		#region Appearance
		
		/// <summary>Hue of empty keg</summary>
		public const int EMPTY_KEG_HUE = 0x96D;
		
		/// <summary>Item ID for keg</summary>
		public const int KEG_ITEM_ID = 0x1940;
		
		/// <summary>Flipped keg item ID</summary>
		public const int KEG_FLIPPED_ID = 0x1AD7;
		
		/// <summary>Keg tile height for pathfinding</summary>
		public const int KEG_TILE_HEIGHT = 4;
		
		#endregion
		
		#region Interaction
		
		/// <summary>Maximum range for keg interaction</summary>
		public const int INTERACTION_RANGE = 2;
		
		/// <summary>Sound played when pouring from keg</summary>
		public const int POUR_SOUND_ID = 0x240;
		
		#endregion
		
		#region Fill Level Thresholds
		
		/// <summary>Threshold for "nearly empty" (< 5)</summary>
		public const int FILL_NEARLY_EMPTY = 5;
		
		/// <summary>Threshold for "not very full" (< 20)</summary>
		public const int FILL_NOT_VERY_FULL = 20;
		
		/// <summary>Threshold for "one quarter full" (< 30)</summary>
		public const int FILL_QUARTER_FULL = 30;
		
		/// <summary>Threshold for "one third full" (< 40)</summary>
		public const int FILL_THIRD_FULL = 40;
		
		/// <summary>Threshold for "almost half full" (< 47)</summary>
		public const int FILL_ALMOST_HALF = 47;
		
		/// <summary>Threshold for "approximately half full" (< 54)</summary>
		public const int FILL_HALF_FULL = 54;
		
		/// <summary>Threshold for "more than half full" (< 70)</summary>
		public const int FILL_MORE_THAN_HALF = 70;
		
		/// <summary>Threshold for "three quarters full" (< 80)</summary>
		public const int FILL_THREE_QUARTERS = 80;
		
		/// <summary>Threshold for "very full" (< 96)</summary>
		public const int FILL_VERY_FULL = 96;
		
		/// <summary>Threshold for "almost to the top" (< 100)</summary>
		public const int FILL_ALMOST_TOP = 100;
		
		#endregion
	}
}


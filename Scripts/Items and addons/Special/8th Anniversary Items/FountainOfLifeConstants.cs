namespace Server.Items
{
	/// <summary>
	/// Centralized constants for Fountain of Life mechanics.
	/// Extracted from FountainOfLife.cs to improve maintainability.
	/// </summary>
	public static class FountainOfLifeConstants
	{
		#region Enhanced Bandage Constants

		/// <summary>Healing bonus provided by enhanced bandages</summary>
		public const int ENHANCED_BANDAGE_HEALING_BONUS = 10;

		#endregion

		#region Fountain Configuration

		/// <summary>Maximum charges the fountain can hold</summary>
		public const int FOUNTAIN_MAX_CHARGES = 15;

		/// <summary>Default charges when fountain is created</summary>
		public const int FOUNTAIN_DEFAULT_CHARGES = 15;

		/// <summary>Number of days for fountain recharge cycle</summary>
		public const int FOUNTAIN_RECHARGE_DAYS = 1;

		#endregion

		#region Item IDs

		/// <summary>Fountain of Life item ID</summary>
		public const int FOUNTAIN_ITEM_ID = 0x2AC0;

		/// <summary>Fountain of Life flipable item ID</summary>
		public const int FOUNTAIN_FLIPABLE_ITEM_ID = 0x2AC3;

		#endregion

		#region Display Constants

		/// <summary>Default gump ID for fountain</summary>
		public const int FOUNTAIN_DEFAULT_GUMP_ID = 0x11A;

		/// <summary>Default drop sound for fountain</summary>
		public const int FOUNTAIN_DEFAULT_DROP_SOUND = 66;

		/// <summary>Default maximum items fountain can hold</summary>
		public const int FOUNTAIN_DEFAULT_MAX_ITEMS = 125;

		#endregion

		#region Cliloc Message IDs

		/// <summary>"Fountain of Life" label</summary>
		public const int CLILOC_FOUNTAIN_OF_LIFE = 1075197;

		/// <summary>"Only bandages may be dropped into the fountain."</summary>
		public const int CLILOC_ONLY_BANDAGES = 1075209;

		/// <summary>"~1_val~ charges remaining"</summary>
		public const int CLILOC_CHARGES_REMAINING = 1075217;

		/// <summary>"these bandages have been enhanced" (unused, kept for reference)</summary>
		public const int CLILOC_ENHANCED_BANDAGE = 1075216;

		#endregion
	}
}


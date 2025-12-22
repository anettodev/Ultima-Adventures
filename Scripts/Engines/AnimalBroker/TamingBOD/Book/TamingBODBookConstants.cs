namespace Server.Items
{
	/// <summary>
	/// Centralized constants for Taming BOD Book calculations and mechanics.
	/// Extracted from TamingBODBook.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class TamingBODBookConstants
	{
		#region Item Constants

		/// <summary>Item ID for Taming BOD Book</summary>
		public const int ITEM_ID = 0x2259;

		/// <summary>Weight of the book</summary>
		public const double WEIGHT = 1.0;

		/// <summary>Hue color for the book</summary>
		public const int HUE = 1204;

		/// <summary>Range check distance for double-click</summary>
		public const int RANGE_CHECK_DISTANCE = 2;

		/// <summary>Message color for range check</summary>
		public const int MESSAGE_COLOR = 0x3B2;

		#endregion

		#region Capacity Constants

		/// <summary>Maximum number of entries that can be stored in the book</summary>
		public const int MAX_ENTRIES = 10;

		#endregion

		#region Serialization Constants

		/// <summary>Current serialization version</summary>
		public const int SERIALIZATION_VERSION = 0;

		#endregion

		#region Localized Message IDs

		/// <summary>Message ID: "I can't reach that."</summary>
		public const int MSG_CANT_REACH = 1019045;

		/// <summary>Message ID: "The book is empty."</summary>
		public const int MSG_BOOK_EMPTY = 1062381;

		/// <summary>Message ID: "You must have the book in your backpack to add deeds to it."</summary>
		public const int MSG_MUST_BE_IN_BACKPACK = 1062385;

		/// <summary>Message ID: "Deed added to book."</summary>
		public const int MSG_DEED_ADDED = 1062386;

		/// <summary>Message ID: "The book is full of deeds."</summary>
		public const int MSG_BOOK_FULL = 1062387;

		/// <summary>Message ID: "Deeds in book: ~1_val~"</summary>
		public const int MSG_DEEDS_IN_BOOK = 1062344;

		#endregion
	}
}


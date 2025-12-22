using System;

namespace Server.Items
{
	/// <summary>
	/// Centralized constants for Bottle item.
	/// Extracted from Bottle.cs to improve maintainability.
	/// </summary>
	public static class BottleConstants
	{
		#region Item Properties

		/// <summary>Item ID for empty bottle graphic (UO client item 0xF0E)</summary>
		public const int ITEM_ID_BOTTLE = 0xF0E;

		/// <summary>Weight of a single bottle in stones (0.45 stones)</summary>
		public const double WEIGHT_BOTTLE = 0.45;

		/// <summary>Default amount when creating a new bottle stack</summary>
		public const int DEFAULT_AMOUNT = 1;

		#endregion

		#region Serialization

		/// <summary>Current serialization version for backwards compatibility</summary>
		public const int SERIALIZATION_VERSION = 0;

		#endregion
	}
}


namespace Server.Custom
{
	/// <summary>
	/// Centralized constants for Hitching Post calculations and mechanics.
	/// Extracted from HitchingPost.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class HitchingPostConstants
	{
		#region Item Constants

		/// <summary>Item ID for Hitching Post</summary>
		public const int ITEM_ID = 0x14E7;

		/// <summary>Hue color for Hitching Post</summary>
		public const int HUE = 0x33;

		/// <summary>Target range for selecting pets</summary>
		public const int TARGET_RANGE = 2;

		/// <summary>Range check distance for pet when hitching</summary>
		public const int PET_RANGE_CHECK = 10;

		#endregion

		#region Serialization Constants

		/// <summary>Current serialization version</summary>
		public const int SERIALIZATION_VERSION = 2;

		#endregion
	}
}


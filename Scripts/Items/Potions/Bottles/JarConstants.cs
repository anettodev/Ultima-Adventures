using System;

namespace Server.Items
{
	/// <summary>
	/// Centralized constants for Jar item.
	/// Extracted from Jar.cs to improve maintainability.
	/// </summary>
	public static class JarConstants
	{
		#region Item Properties

		/// <summary>Item ID for jar graphic (UO client item 0x10B4)</summary>
		public const int ITEM_ID_JAR = 0x10B4;

		/// <summary>Weight of a single jar in stones</summary>
		public const double WEIGHT_JAR = 1.0;

		/// <summary>Default amount when creating a new jar stack</summary>
		public const int DEFAULT_AMOUNT = 1;

		#endregion

		#region Serialization

		/// <summary>Current serialization version for backwards compatibility</summary>
		public const int SERIALIZATION_VERSION = 0;

		#endregion
	}
}


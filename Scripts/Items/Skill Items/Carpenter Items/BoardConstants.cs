using System.Collections.Generic;
using Server.Engines.Craft;

namespace Server.Items
{
	/// <summary>
	/// Centralized constants for board properties and mechanics.
	/// Extracted from Board.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class BoardConstants
	{
		#region Item IDs

		/// <summary>Base board item ID</summary>
		public const int ITEM_ID_BOARD = 0x1BD7;

		#endregion

		#region Weight Constants

		/// <summary>Board weight in constructor</summary>
		public const double WEIGHT_CONSTRUCTOR = 1.0;

		/// <summary>Board weight after deserialization</summary>
		public const double WEIGHT_DESERIALIZED = 0.5;

		#endregion

		#region Serialization Constants

		/// <summary>Current serialization version</summary>
		public const int SERIALIZATION_VERSION_CURRENT = 3;

		/// <summary>Default serialization version for derived classes</summary>
		public const int SERIALIZATION_VERSION_DEFAULT = 0;

		#endregion

		#region Localization IDs

		/// <summary>Localization ID for Regular Wood boards</summary>
		public const int LOCALIZATION_ID_REGULAR_WOOD = 1015101;

		/// <summary>Localization ID for Ash Tree boards</summary>
		public const int LOCALIZATION_ID_ASH = 1095389;

		/// <summary>Localization ID for Cherry Tree boards</summary>
		public const int LOCALIZATION_ID_CHERRY = 1095390;

		/// <summary>Localization ID for Ebony Tree boards</summary>
		public const int LOCALIZATION_ID_EBONY = 1095391;

		/// <summary>Localization ID for Golden Oak Tree boards</summary>
		public const int LOCALIZATION_ID_GOLDEN_OAK = 1095392;

		/// <summary>Localization ID for Hickory Tree boards</summary>
		public const int LOCALIZATION_ID_HICKORY = 1095393;

		/// <summary>Localization ID for Rosewood Tree boards</summary>
		public const int LOCALIZATION_ID_ROSEWOOD = 1095397;

		/// <summary>Localization ID for Elven Tree boards</summary>
		public const int LOCALIZATION_ID_ELVEN = 1095536;

		#endregion

		#region Localization ID Dictionary

		/// <summary>
		/// Dictionary mapping CraftResource to localization ID.
		/// Used for ICommodity.DescriptionNumber property.
		/// </summary>
		public static readonly Dictionary<CraftResource, int> LOCALIZATION_IDS = new Dictionary<CraftResource, int>
		{
			{ CraftResource.RegularWood, LOCALIZATION_ID_REGULAR_WOOD },
			{ CraftResource.AshTree, LOCALIZATION_ID_ASH },
			{ CraftResource.CherryTree, LOCALIZATION_ID_CHERRY },
			{ CraftResource.EbonyTree, LOCALIZATION_ID_EBONY },
			{ CraftResource.GoldenOakTree, LOCALIZATION_ID_GOLDEN_OAK },
			{ CraftResource.HickoryTree, LOCALIZATION_ID_HICKORY },
			{ CraftResource.RosewoodTree, LOCALIZATION_ID_ROSEWOOD },
			{ CraftResource.ElvenTree, LOCALIZATION_ID_ELVEN }
		};

		#endregion
	}
}


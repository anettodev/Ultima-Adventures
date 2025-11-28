namespace Server.Items
{
	/// <summary>
	/// Centralized display name constants for board types in PT-BR.
	/// Boards use the same wood types as logs, so we reuse LogNameConstants values.
	/// Extracted from Board.cs to enable custom names/aliases for boards.
	/// </summary>
	public static class BoardNameConstants
	{
		#region Board Display Names (Portuguese)

		/// <summary>Display name for Regular Wood boards (reuses LogNameConstants)</summary>
		public const string REGULAR_WOOD_DISPLAY_NAME = LogNameConstants.REGULAR_WOOD_DISPLAY_NAME;

		/// <summary>Display name for Ash Tree boards (reuses LogNameConstants)</summary>
		public const string ASH_TREE_DISPLAY_NAME = LogNameConstants.ASH_TREE_DISPLAY_NAME;

		/// <summary>Display name for Ebony Tree boards (reuses LogNameConstants)</summary>
		public const string EBONY_TREE_DISPLAY_NAME = LogNameConstants.EBONY_TREE_DISPLAY_NAME;

		/// <summary>Display name for Elven Tree boards (reuses LogNameConstants)</summary>
		public const string ELVEN_TREE_DISPLAY_NAME = LogNameConstants.ELVEN_TREE_DISPLAY_NAME;

		/// <summary>Display name for Golden Oak Tree boards (reuses LogNameConstants)</summary>
		public const string GOLDEN_OAK_TREE_DISPLAY_NAME = LogNameConstants.GOLDEN_OAK_TREE_DISPLAY_NAME;

		/// <summary>Display name for Cherry Tree boards (reuses LogNameConstants)</summary>
		public const string CHERRY_TREE_DISPLAY_NAME = LogNameConstants.CHERRY_TREE_DISPLAY_NAME;

		/// <summary>Display name for Rosewood Tree boards (reuses LogNameConstants)</summary>
		public const string ROSEWOOD_TREE_DISPLAY_NAME = LogNameConstants.ROSEWOOD_TREE_DISPLAY_NAME;

		/// <summary>Display name for Hickory Tree boards (reuses LogNameConstants)</summary>
		public const string HICKORY_TREE_DISPLAY_NAME = LogNameConstants.HICKORY_TREE_DISPLAY_NAME;

		#endregion
	}
}


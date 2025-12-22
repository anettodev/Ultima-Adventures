namespace Server.Items
{
	/// <summary>
	/// Centralized constants for RecipeScroll mechanics.
	/// Extracted from RecipeScroll.cs to improve maintainability.
	/// </summary>
	public static class RecipeScrollConstants
	{
		#region Item Properties

		/// <summary>Label number for recipe scroll</summary>
		public const int LABEL_NUMBER_RECIPE_SCROLL = 1074560;

		/// <summary>Cliloc for recipe name display</summary>
		public const int CLILOC_RECIPE_NAME = 1049644;

		/// <summary>Item ID for recipe scroll</summary>
		public const int ITEM_ID_RECIPE_SCROLL = 0x2831;

		#endregion

		#region Range and Distance

		/// <summary>Maximum range to interact with recipe scroll</summary>
		public const int INTERACT_RANGE = 2;

		#endregion

		#region Message Colors

		/// <summary>Message color for "can't reach that"</summary>
		public const int MSG_COLOR_CANT_REACH = 0x3B2;

		#endregion

		#region Localized Messages (Clilocs)

		/// <summary>Cliloc: "I can't reach that."</summary>
		public const int CLILOC_CANT_REACH = 1019045;

		/// <summary>Cliloc: "You have learned a new recipe: ~1_RECIPE~"</summary>
		public const int CLILOC_RECIPE_LEARNED = 1073451;

		/// <summary>Cliloc: "You don't have the required skills to attempt this item."</summary>
		public const int CLILOC_INSUFFICIENT_SKILLS = 1044153;

		/// <summary>Cliloc: "You already know this recipe."</summary>
		public const int CLILOC_ALREADY_KNOW_RECIPE = 1073427;

		#endregion

		#region Serialization

		/// <summary>Current serialization version</summary>
		public const int SERIALIZATION_VERSION = 0;

		#endregion
	}
}


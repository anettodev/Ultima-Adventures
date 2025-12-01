namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for Alchemy Recipe Book system.
	/// Contains item IDs, hues, gump positions, and recipe ID ranges.
	/// </summary>
	public static class AlchemyRecipeConstants
	{
		#region Item IDs

		/// <summary>Item ID for Alchemy Recipe Book (Necromancer book style)</summary>
		public const int BOOK_ITEM_ID = 0x2253;

		/// <summary>Item ID for Alchemy Recipe Scroll</summary>
		public const int SCROLL_ITEM_ID = 0x14ED;

		/// <summary>Item ID for potion bottle icon in gumps</summary>
		public const int POTION_ICON_ITEM_ID = 0x0F0B;

		#endregion

		#region Hue Values

		/// <summary>Hue for Alchemy Recipe Book and scrolls</summary>
		public const int BOOK_HUE = 2003; // 0x7D3 - greenish/teal

		#endregion

		#region Recipe ID Ranges

		/// <summary>Starting recipe ID for alchemy potions</summary>
		public const int RECIPE_ID_START = 500;

		/// <summary>Ending recipe ID for alchemy potions</summary>
		public const int RECIPE_ID_END = 549;

		#endregion

		#region Recipe Categories

		/// <summary>Category 0: Basic potions (All Lesser and regular potions)</summary>
		public const int CATEGORY_BASIC = 0;

		/// <summary>Category 1: Strong and Advanced potions (All Greater potions)</summary>
		public const int CATEGORY_ADVANCED = 1;

		/// <summary>Category 2: Special potions (Frostbite, Confusion, Conflagration)</summary>
		public const int CATEGORY_SPECIAL = 2;

		/// <summary>Category 3: Cosmetic potions (Hair cut and tint)</summary>
		public const int CATEGORY_COSMETIC = 3;

		/// <summary>Total number of categories</summary>
		public const int CATEGORY_COUNT = 4;

		#endregion

		#region Gump Layout

		/// <summary>Gump width (matches book image)</summary>
		public const int GUMP_WIDTH = 520;

		/// <summary>Gump height (matches book image)</summary>
		public const int GUMP_HEIGHT = 370;

		/// <summary>Gump X position</summary>
		public const int GUMP_X = 50;

		/// <summary>Gump Y position</summary>
		public const int GUMP_Y = 50;

		/// <summary>Gump background ID (Book-style image)</summary>
		public const int GUMP_BACKGROUND_ID = 1054; // Runebook-style book image

		/// <summary>Maximum recipes displayed per page</summary>
		public const int RECIPES_PER_PAGE = 8;

		/// <summary>Category panel X offset (left page)</summary>
		public const int CATEGORY_PANEL_X = 70; // Moved 20px right

		/// <summary>Category panel Y offset</summary>
		public const int CATEGORY_PANEL_Y = 80;

		/// <summary>Recipe panel X offset (right page)</summary>
		public const int RECIPE_PANEL_X = 380; // Moved 20px right (was 360)

		/// <summary>Recipe panel Y offset</summary>
		public const int RECIPE_PANEL_Y = 80;

		/// <summary>Spacing between category entries</summary>
		public const int CATEGORY_SPACING = 22;

		/// <summary>Spacing between recipe entries</summary>
		public const int RECIPE_SPACING = 28;

		#endregion

		#region Button IDs

		/// <summary>Button ID base for category selection (100-107)</summary>
		public const int BUTTON_CATEGORY_BASE = 100;

		/// <summary>Button ID base for recipe selection (200+)</summary>
		public const int BUTTON_RECIPE_BASE = 200;

		/// <summary>Button ID for back/return button</summary>
		public const int BUTTON_BACK = 1;

		/// <summary>Button ID for next page</summary>
		public const int BUTTON_NEXT = 10;

		/// <summary>Button ID for previous page</summary>
		public const int BUTTON_PREV = 11;

		#endregion

		#region Gump Image IDs

		/// <summary>Selected category indicator (down-pointing arrow)</summary>
		public const int IMAGE_CATEGORY_SELECTED = 0x26AD; // Down arrow (0x26AC is up arrow, 0x26AD is down arrow)

		/// <summary>Unselected category indicator</summary>
		public const int IMAGE_CATEGORY_UNSELECTED = 0x26B0;

		/// <summary>Recipe button normal</summary>
		public const int IMAGE_RECIPE_BUTTON = 0x845;

		/// <summary>Recipe button pressed</summary>
		public const int IMAGE_RECIPE_BUTTON_PRESSED = 0x846;

		/// <summary>Next page button (normal)</summary>
		public const int IMAGE_NEXT_BUTTON = 0x15E1;

		/// <summary>Next page button (pressed)</summary>
		public const int IMAGE_NEXT_BUTTON_PRESSED = 0x15E5;

		/// <summary>Previous page button (normal)</summary>
		public const int IMAGE_PREV_BUTTON = 0x15E3;

		/// <summary>Previous page button (pressed)</summary>
		public const int IMAGE_PREV_BUTTON_PRESSED = 0x15E7;

		#endregion

		#region Item Properties

		/// <summary>Weight of Alchemy Recipe Book</summary>
		public const double BOOK_WEIGHT = 3.0;

		/// <summary>Weight of Alchemy Recipe Scroll</summary>
		public const double SCROLL_WEIGHT = 0.1;

		#endregion

		#region Sound IDs

		/// <summary>Sound played when learning a recipe</summary>
		public const int SOUND_LEARN_RECIPE = 0x249;

		#endregion

		#region Message Colors

		/// <summary>Error message color (red)</summary>
		public const int MSG_COLOR_ERROR = 55;

		/// <summary>Success message color (green)</summary>
		public const int MSG_COLOR_SUCCESS = 68;

		/// <summary>Info message color (cyan)</summary>
		public const int MSG_COLOR_INFO = 88;

		#endregion
	}
}


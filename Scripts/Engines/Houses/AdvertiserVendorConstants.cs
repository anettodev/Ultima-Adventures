namespace Server.Items
{
	/// <summary>
	/// Centralized constants for AdvertiserVendor item and gump mechanics.
	/// Extracted from AdvertiserVendor.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class AdvertiserVendorConstants
	{
		#region Item IDs and Graphics

		/// <summary>AdvertiserVendor item ID (flipable variant 1)</summary>
		public const int ITEM_ID_1 = 0x1E5E;

		/// <summary>AdvertiserVendor item ID (flipable variant 2)</summary>
		public const int ITEM_ID_2 = 0x1E5F;

		/// <summary>Item hue color</summary>
		public const int ITEM_HUE = 0xB9A;

		/// <summary>Black alpha background graphic ID</summary>
		public const int GRAPHIC_BLACK_ALPHA = 2624;

		/// <summary>Gump background graphic ID</summary>
		public const int GRAPHIC_GUMP_BACKGROUND = 3500;

		/// <summary>Previous page button graphic (active)</summary>
		public const int GRAPHIC_BUTTON_PREV_ACTIVE = 0x15E3;

		/// <summary>Previous page button graphic (pressed)</summary>
		public const int GRAPHIC_BUTTON_PREV_PRESSED = 0x15E7;

		/// <summary>Next page button graphic (active)</summary>
		public const int GRAPHIC_BUTTON_NEXT_ACTIVE = 0x15E1;

		/// <summary>Next page button graphic (pressed)</summary>
		public const int GRAPHIC_BUTTON_NEXT_PRESSED = 0x15E5;

		/// <summary>Disabled previous page button graphic</summary>
		public const int GRAPHIC_BUTTON_PREV_DISABLED = 0x25EA;

		/// <summary>Disabled next page button graphic</summary>
		public const int GRAPHIC_BUTTON_NEXT_DISABLED = 0x25E6;

		#endregion

		#region Weight

		/// <summary>Weight of AdvertiserVendor item</summary>
		public const double WEIGHT = 1.0;

		#endregion

		#region Gump Dimensions and Positions

		/// <summary>Gump base X position</summary>
		public const int GUMP_X = 50;

		/// <summary>Gump base Y position</summary>
		public const int GUMP_Y = 40;

		/// <summary>Gump width</summary>
		public const int GUMP_WIDTH = 645;

		/// <summary>Gump height</summary>
		public const int GUMP_HEIGHT = 325;

		/// <summary>Black alpha region X position</summary>
		public const int ALPHA_REGION_X = 20;

		/// <summary>Black alpha region Y position</summary>
		public const int ALPHA_REGION_Y = 20;

		/// <summary>Black alpha region width</summary>
		public const int ALPHA_REGION_WIDTH = 604;

		/// <summary>Black alpha region height</summary>
		public const int ALPHA_REGION_HEIGHT = 277;

		#endregion

		#region Label Positions and Sizes

		/// <summary>Shop name label X position</summary>
		public const int LABEL_SHOP_NAME_X = 32;

		/// <summary>Shop name label Y position</summary>
		public const int LABEL_SHOP_NAME_Y = 20;

		/// <summary>Shop name label width</summary>
		public const int LABEL_SHOP_NAME_WIDTH = 100;

		/// <summary>Shop name label height</summary>
		public const int LABEL_SHOP_NAME_HEIGHT = 20;

		/// <summary>Owner label X position</summary>
		public const int LABEL_OWNER_X = 250;

		/// <summary>Owner label Y position</summary>
		public const int LABEL_OWNER_Y = 20;

		/// <summary>Owner label width</summary>
		public const int LABEL_OWNER_WIDTH = 120;

		/// <summary>Owner label height</summary>
		public const int LABEL_OWNER_HEIGHT = 20;

		/// <summary>Location label X position</summary>
		public const int LABEL_LOCATION_X = 415;

		/// <summary>Location label Y position</summary>
		public const int LABEL_LOCATION_Y = 20;

		/// <summary>Location label width</summary>
		public const int LABEL_LOCATION_WIDTH = 120;

		/// <summary>Location label height</summary>
		public const int LABEL_LOCATION_HEIGHT = 20;

		/// <summary>Footer label X position</summary>
		public const int LABEL_FOOTER_X = 27;

		/// <summary>Footer label Y position</summary>
		public const int LABEL_FOOTER_Y = 298;

		/// <summary>Empty list message X position</summary>
		public const int LABEL_EMPTY_X = 180;

		/// <summary>Empty list message Y position</summary>
		public const int LABEL_EMPTY_Y = 115;

		/// <summary>Row start X position (shop name)</summary>
		public const int ROW_START_X_SHOP = 32;

		/// <summary>Row start X position (owner)</summary>
		public const int ROW_START_X_OWNER = 250;

		/// <summary>Row start X position (location)</summary>
		public const int ROW_START_X_LOCATION = 415;

		/// <summary>Row start Y position</summary>
		public const int ROW_START_Y = 46;

		/// <summary>Row height multiplier</summary>
		public const int ROW_HEIGHT = 20;

		#endregion

		#region Button Positions

		/// <summary>Previous page button X position</summary>
		public const int BUTTON_PREV_X = 573;

		/// <summary>Previous page button Y position</summary>
		public const int BUTTON_PREV_Y = 22;

		/// <summary>Next page button X position</summary>
		public const int BUTTON_NEXT_X = 590;

		/// <summary>Next page button Y position</summary>
		public const int BUTTON_NEXT_Y = 22;

		#endregion

		#region Color/Hue Values

		/// <summary>Green hue for gump elements</summary>
		public const int HUE_GREEN = 0x40;

		/// <summary>Red hue for gump elements</summary>
		public const int HUE_RED = 0x20;

		/// <summary>Label text hue</summary>
		public const int HUE_LABEL_TEXT = 1152;

		/// <summary>Footer text hue</summary>
		public const int HUE_FOOTER_TEXT = 32;

		#endregion

		#region Pagination Constants

		/// <summary>Number of vendors displayed per page</summary>
		public const int VENDORS_PER_PAGE = 12;

		/// <summary>First page number</summary>
		public const int FIRST_PAGE = 1;

		/// <summary>Button ID offset for vendor selection buttons (buttons start at this value)</summary>
		public const int BUTTON_ID_OFFSET = 101;

		/// <summary>Next page button ID</summary>
		public const int BUTTON_ID_NEXT = 2;

		/// <summary>Previous page button ID</summary>
		public const int BUTTON_ID_PREV = 1;

		#endregion

		#region Default Values

		/// <summary>Default X coordinate</summary>
		public const int DEFAULT_X = 1;

		/// <summary>Default Y coordinate</summary>
		public const int DEFAULT_Y = 1;

		/// <summary>Default Z coordinate for location formatting</summary>
		public const int DEFAULT_Z = 0;

		#endregion
	}
}


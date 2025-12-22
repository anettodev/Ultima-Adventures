namespace Server.Gumps
{
	/// <summary>
	/// Centralized constants for Taming BOD Gump layout and mechanics.
	/// Extracted from TamingBODGump.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class TamingBODGumpConstants
	{
		#region Gump Layout

		/// <summary>Gump page ID</summary>
		public const int GUMP_PAGE_ID = 0;

		/// <summary>Gump X position</summary>
		public const int GUMP_X = 0;

		/// <summary>Gump Y position</summary>
		public const int GUMP_Y = 0;

		/// <summary>Gump width</summary>
		public const int GUMP_WIDTH = 300;

		/// <summary>Gump height</summary>
		public const int GUMP_HEIGHT = 170;

		/// <summary>Gump background image ID</summary>
		public const int GUMP_BACKGROUND_ID = 5170;

		#endregion

		#region Label Layout

		/// <summary>Label X position</summary>
		public const int LABEL_X = 40;

		/// <summary>Contract label Y position</summary>
		public const int LABEL_CONTRACT_Y = 40;

		/// <summary>Quantity label Y position</summary>
		public const int LABEL_QUANTITY_Y = 60;

		/// <summary>Reward label Y position</summary>
		public const int LABEL_REWARD_Y = 80;

		/// <summary>Label text color</summary>
		public const int LABEL_COLOR = 0;
		public const int LABEL_WHITE_COLOR = 1153;

		#endregion

		#region Button Layout

		/// <summary>Button X position</summary>
		public const int BUTTON_X = 90;

		/// <summary>Button Y position</summary>
		public const int BUTTON_Y = 110;

		/// <summary>Button normal image ID</summary>
		public const int BUTTON_NORMAL_ID = 10800;

		/// <summary>Button pressed image ID</summary>
		public const int BUTTON_PRESSED_ID = 10840;

		/// <summary>Button pressed image ID</summary>
		public const int BUTTON_REWARD_PRESSED_ID = 10820;

		/// <summary>Add creature button ID</summary>
		public const int BUTTON_ADD_ID = 1;

		/// <summary>Reward button ID</summary>
		public const int BUTTON_REWARD_ID = 2;

		/// <summary>Button label X position</summary>
		public const int BUTTON_LABEL_X = 124;

		/// <summary>Button label X position</summary>
		public const int BUTTON_LABEL_X_REWARD = 114;

		/// <summary>Button label Y position</summary>
		public const int BUTTON_LABEL_Y = 108;

		/// <summary>Button label Y position</summary>
		public const int BUTTON_LABEL_Y_REWARD = 114;

		/// <summary>Button background width</summary>
		public const int BUTTON_BACKGROUND_WIDTH = 100;

		/// <summary>Button background height</summary>
		public const int BUTTON_BACKGROUND_HEIGHT = 30;

		/// <summary>Green color for button background (RGB: 0, 128, 0)</summary>
		public const int BUTTON_BACKGROUND_COLOR_GREEN = 0x008000;

		/// <summary>White color for button text (RGB: 255, 255, 255)</summary>
		public const int BUTTON_TEXT_COLOR_WHITE = 0xFFFFFF;

		#endregion

		#region Target Constants

		/// <summary>Unlimited target range</summary>
		public const int TARGET_RANGE_UNLIMITED = -1;

		/// <summary>Pet combat range check distance</summary>
		public const int PET_COMBAT_RANGE = 12;

		#endregion

		#region Calculation Constants

		/// <summary>Reward multiplier (75% of pet value)</summary>
		public const double REWARD_MULTIPLIER = 0.75;

		#endregion
	}
}


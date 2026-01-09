namespace Server.Gumps
{
	/// <summary>
	/// Centralized constants for Taming BOD Book Gump layout and mechanics.
	/// Extracted from TamingBODBookGump.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class TamingBODBookGumpConstants
	{
		#region Gump Layout

		/// <summary>Gump page ID</summary>
		public const int GUMP_PAGE_ID = 0;

		/// <summary>Gump background X offset</summary>
		public const int GUMP_X_OFFSET = 8;

		/// <summary>Gump background Y offset</summary>
		public const int GUMP_Y_OFFSET = 10;

		/// <summary>Gump background width (reduced by 80px from 700 to 620)</summary>
		public const int GUMP_WIDTH = 600;

		/// <summary>Gump header section height</summary>
		public const int GUMP_HEADER_HEIGHT = 80;

		/// <summary>Height per entry row</summary>
		public const int ENTRY_ROW_HEIGHT = 30;

		/// <summary>Gump background image ID</summary>
		public const int GUMP_BACKGROUND_ID = 9200;

		#endregion

		#region Title Region

		/// <summary>Title alpha region X position (re-centered for 620px width)</summary>
		public const int TITLE_REGION_X = 100;

		/// <summary>Title alpha region Y position</summary>
		public const int TITLE_REGION_Y = 21;

		/// <summary>Title alpha region width</summary>
		public const int TITLE_REGION_WIDTH = 400;

		/// <summary>Title alpha region height</summary>
		public const int TITLE_REGION_HEIGHT = 24;

		/// <summary>Title label X position (re-centered, calculated dynamically in code)</summary>
		public const int TITLE_LABEL_X = 230;

		/// <summary>Title label Y position</summary>
		public const int TITLE_LABEL_Y = 22;

		#endregion

		#region Column Layout

		/// <summary>First column X position (re-centered for 620px width, shifted 40px left)</summary>
		public const int COLUMN_1_X = 71;

		/// <summary>Second column X position (re-centered, shifted 40px left)</summary>
		public const int COLUMN_2_X = 221;

		/// <summary>Third column X position (re-centered, shifted 40px left)</summary>
		public const int COLUMN_3_X = 311;

		/// <summary>Fourth column X position (re-centered, shifted 40px left)</summary>
		public const int COLUMN_4_X = 421;

		/// <summary>First column width</summary>
		public const int COLUMN_1_WIDTH = 150;

		/// <summary>Second column width (LABEL_TAMED - increased by 20px from 70 to 90)</summary>
		public const int COLUMN_2_WIDTH = 90;

		/// <summary>Third column width (LABEL_TO_TAME - increased by 40px from 70 to 110)</summary>
		public const int COLUMN_3_WIDTH = 110;

		/// <summary>Fourth column width</summary>
		public const int COLUMN_4_WIDTH = 100;

		#endregion

		#region Row Layout

		/// <summary>Header row Y position</summary>
		public const int HEADER_ROW_Y = 52;

		/// <summary>First entry row Y position</summary>
		public const int FIRST_ENTRY_ROW_Y = 71;

		/// <summary>Row height</summary>
		public const int ROW_HEIGHT = 15;

		/// <summary>Entry label Y offset</summary>
		public const int ENTRY_LABEL_Y_OFFSET = 70;

		#endregion

		#region Button Layout

		/// <summary>Add button X position (re-centered, shifted 40px left)</summary>
		public const int BUTTON_ADD_X = 526;

		/// <summary>Remove button X position (re-centered, shifted 40px left)</summary>
		public const int BUTTON_REMOVE_X = 541;

		/// <summary>Button Y offset</summary>
		public const int BUTTON_Y_OFFSET = 73;

		/// <summary>Add button normal image ID</summary>
		public const int BUTTON_ADD_NORMAL_ID = 2362;

		/// <summary>Add button pressed image ID</summary>
		public const int BUTTON_ADD_PRESSED_ID = 2362;

		/// <summary>Remove button normal image ID</summary>
		public const int BUTTON_REMOVE_NORMAL_ID = 2360;

		/// <summary>Remove button pressed image ID</summary>
		public const int BUTTON_REMOVE_PRESSED_ID = 2360;

		/// <summary>Add button base ID</summary>
		public const int BUTTON_ADD_BASE_ID = 200;

		/// <summary>Remove button base ID</summary>
		public const int BUTTON_REMOVE_BASE_ID = 100;

		#endregion

		#region Label Colors

		/// <summary>Label text color (gray)</summary>
		public const int LABEL_COLOR = 50;
		public const int LABEL_WHITE_COLOR = 1153;
		public const int LABEL_GREEN_COLOR = 58;

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


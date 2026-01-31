namespace Server.Items
{
	/// <summary>
	/// Centralized constants for name change system calculations and mechanics.
	/// Extracted from CensusRecords.cs, ChangeName.cs, NameAlterGump.cs, and NameChangeGump.cs
	/// to improve maintainability and reduce code duplication.
	/// </summary>
	public static class NameChangeConstants
	{
		#region Item IDs

		/// <summary>Item ID for Name Change Contract (first variant)</summary>
		public const int ITEM_ID_CHANGE_NAME_1 = 0x14EF;

		/// <summary>Item ID for Name Change Contract (second variant)</summary>
		public const int ITEM_ID_CHANGE_NAME_2 = 0x14F0;

		/// <summary>Item ID for Census Records (first variant)</summary>
		public const int ITEM_ID_CENSUS_RECORDS_1 = 0xFBD;

		/// <summary>Item ID for Census Records (second variant)</summary>
		public const int ITEM_ID_CENSUS_RECORDS_2 = 0xFBE;

		#endregion

		#region Message Colors

		/// <summary>Color code for info/error messages (34 decimal, 0x22 hex)</summary>
		public const int MSG_COLOR_INFO = 0x22;

		#endregion

		#region Name Validation

		/// <summary>Minimum length for character names</summary>
		public const int NAME_MIN_LENGTH = 2;

		/// <summary>Maximum length for character names</summary>
		public const int NAME_MAX_LENGTH = 16;

		/// <summary>NameVerification parameter for validation</summary>
		public const int NAME_VERIFICATION_PARAM = 1;

		#endregion

		#region Gold Cost

		/// <summary>Gold cost for name change via Census Records</summary>
		public const int CENSUS_NAME_CHANGE_COST = 2000;

		#endregion

		#region Gump Layout - Base Positions

		/// <summary>Base X position for legal name change gumps (CensusGump, NameAlterGump)</summary>
		public const int GUMP_BASE_X_LEGAL = 25;

		/// <summary>Base Y position for legal name change gumps (CensusGump, NameAlterGump)</summary>
		public const int GUMP_BASE_Y_LEGAL = 25;

		/// <summary>Base X position for forced name change gump (NameChangeGump)</summary>
		public const int GUMP_BASE_X_FORCED = 100;

		/// <summary>Base Y position for forced name change gump (NameChangeGump)</summary>
		public const int GUMP_BASE_Y_FORCED = 100;

		#endregion

		#region Gump Layout - CensusGump and NameAlterGump

		/// <summary>Text entry ID for name input</summary>
		public const int TEXT_ENTRY_ID = 1;

		/// <summary>Maximum character length for text entry</summary>
		public const int TEXT_ENTRY_MAX_LENGTH = 16;

		/// <summary>Text entry X position</summary>
		public const int TEXT_ENTRY_X = 126;

		/// <summary>Text entry Y position</summary>
		public const int TEXT_ENTRY_Y = 238;

		/// <summary>Text entry width</summary>
		public const int TEXT_ENTRY_WIDTH = 267;

		/// <summary>Text entry height</summary>
		public const int TEXT_ENTRY_HEIGHT = 20;

		/// <summary>Text entry hue (1511)</summary>
		public const int TEXT_ENTRY_HUE = 1511;

		/// <summary>Button X position</summary>
		public const int BUTTON_X = 90;

		/// <summary>Button Y position</summary>
		public const int BUTTON_Y = 238;

		/// <summary>Button normal ID</summary>
		public const int BUTTON_NORMAL_ID = 4005;

		/// <summary>Button pressed ID</summary>
		public const int BUTTON_PRESSED_ID = 4005;

		/// <summary>Button reply ID</summary>
		public const int BUTTON_REPLY_ID = 1;

		/// <summary>HTML text X position</summary>
		public const int HTML_TEXT_X = 175;

		/// <summary>HTML text Y position</summary>
		public const int HTML_TEXT_Y = 37;

		/// <summary>HTML text width</summary>
		public const int HTML_TEXT_WIDTH = 349;

		/// <summary>HTML text height</summary>
		public const int HTML_TEXT_HEIGHT = 180;

		#endregion

		#region Gump Layout - NameChangeGump

		/// <summary>NameChangeGump HTML text X position</summary>
		public const int FORCED_HTML_TEXT_X = 153;

		/// <summary>NameChangeGump HTML text Y position</summary>
		public const int FORCED_HTML_TEXT_Y = 135;

		/// <summary>NameChangeGump HTML text width</summary>
		public const int FORCED_HTML_TEXT_WIDTH = 304;

		/// <summary>NameChangeGump HTML text height</summary>
		public const int FORCED_HTML_TEXT_HEIGHT = 113;

		/// <summary>NameChangeGump background X position</summary>
		public const int FORCED_BACKGROUND_X = 137;

		/// <summary>NameChangeGump background Y position</summary>
		public const int FORCED_BACKGROUND_Y = 119;

		/// <summary>NameChangeGump background width</summary>
		public const int FORCED_BACKGROUND_WIDTH = 334;

		/// <summary>NameChangeGump background height</summary>
		public const int FORCED_BACKGROUND_HEIGHT = 195;

		/// <summary>NameChangeGump background ID</summary>
		public const int FORCED_BACKGROUND_ID = 9250;

		/// <summary>NameChangeGump text entry background X position</summary>
		public const int FORCED_TEXT_BACKGROUND_X = 221;

		/// <summary>NameChangeGump text entry background Y position</summary>
		public const int FORCED_TEXT_BACKGROUND_Y = 264;

		/// <summary>NameChangeGump text entry background width</summary>
		public const int FORCED_TEXT_BACKGROUND_WIDTH = 171;

		/// <summary>NameChangeGump text entry background height</summary>
		public const int FORCED_TEXT_BACKGROUND_HEIGHT = 29;

		/// <summary>NameChangeGump text entry background ID</summary>
		public const int FORCED_TEXT_BACKGROUND_ID = 3000;

		/// <summary>NameChangeGump label X position</summary>
		public const int FORCED_LABEL_X = 153;

		/// <summary>NameChangeGump label Y position</summary>
		public const int FORCED_LABEL_Y = 270;

		/// <summary>NameChangeGump label hue</summary>
		public const int FORCED_LABEL_HUE = 0;

		/// <summary>NameChangeGump text entry X position</summary>
		public const int FORCED_TEXT_ENTRY_X = 224;

		/// <summary>NameChangeGump text entry Y position</summary>
		public const int FORCED_TEXT_ENTRY_Y = 268;

		/// <summary>NameChangeGump text entry width</summary>
		public const int FORCED_TEXT_ENTRY_WIDTH = 163;

		/// <summary>NameChangeGump text entry height</summary>
		public const int FORCED_TEXT_ENTRY_HEIGHT = 21;

		/// <summary>NameChangeGump text entry hue</summary>
		public const int FORCED_TEXT_ENTRY_HUE = 0;

		/// <summary>NameChangeGump button X position</summary>
		public const int FORCED_BUTTON_X = 395;

		/// <summary>NameChangeGump button Y position</summary>
		public const int FORCED_BUTTON_Y = 267;

		/// <summary>NameChangeGump button normal ID</summary>
		public const int FORCED_BUTTON_NORMAL_ID = 4023;

		/// <summary>NameChangeGump button pressed ID</summary>
		public const int FORCED_BUTTON_PRESSED_ID = 4024;

		/// <summary>NameChangeGump button reply ID</summary>
		public const int FORCED_BUTTON_REPLY_ID = 1;

		#endregion
	}
}


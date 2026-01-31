namespace Server.Gumps
{
	/// <summary>
	/// Centralized constants for Young/Iniciante Gump layout and mechanics.
	/// Extracted from YoungGumps.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class YoungGumpConstants
	{
		#region YoungDungeonWarning Layout

		/// <summary>Gump X position</summary>
		public const int DUNGEON_WARNING_X = 150;

		/// <summary>Gump Y position</summary>
		public const int DUNGEON_WARNING_Y = 200;

		/// <summary>Background X position</summary>
		public const int DUNGEON_WARNING_BG_X = 0;

		/// <summary>Background Y position</summary>
		public const int DUNGEON_WARNING_BG_Y = 0;

		/// <summary>Background width</summary>
		public const int DUNGEON_WARNING_BG_WIDTH = 250;

		/// <summary>Background height</summary>
		public const int DUNGEON_WARNING_BG_HEIGHT = 170;

		/// <summary>HTML content X position</summary>
		public const int DUNGEON_WARNING_HTML_X = 20;

		/// <summary>HTML content Y position</summary>
		public const int DUNGEON_WARNING_HTML_Y = 43;

		/// <summary>HTML content width</summary>
		public const int DUNGEON_WARNING_HTML_WIDTH = 215;

		/// <summary>HTML content height</summary>
		public const int DUNGEON_WARNING_HTML_HEIGHT = 70;

		/// <summary>Button X position</summary>
		public const int DUNGEON_WARNING_BUTTON_X = 70;

		/// <summary>Button Y position</summary>
		public const int DUNGEON_WARNING_BUTTON_Y = 123;

		/// <summary>Button label X position</summary>
		public const int DUNGEON_WARNING_BUTTON_LABEL_X = 105;

		/// <summary>Button label Y position</summary>
		public const int DUNGEON_WARNING_BUTTON_LABEL_Y = 125;

		/// <summary>Button label width</summary>
		public const int DUNGEON_WARNING_BUTTON_LABEL_WIDTH = 100;

		/// <summary>Button label height</summary>
		public const int DUNGEON_WARNING_BUTTON_LABEL_HEIGHT = 35;

		#endregion

		#region YoungDeathNotice Layout

		/// <summary>Gump X position</summary>
		public const int DEATH_NOTICE_X = 100;

		/// <summary>Gump Y position</summary>
		public const int DEATH_NOTICE_Y = 15;

		/// <summary>Background X position</summary>
		public const int DEATH_NOTICE_BG_X = 25;

		/// <summary>Background Y position</summary>
		public const int DEATH_NOTICE_BG_Y = 10;

		/// <summary>Background width</summary>
		public const int DEATH_NOTICE_BG_WIDTH = 425;

		/// <summary>Background height</summary>
		public const int DEATH_NOTICE_BG_HEIGHT = 444;

		/// <summary>Tiled/Alpha region X position</summary>
		public const int DEATH_NOTICE_TILE_X = 33;

		/// <summary>Tiled/Alpha region Y position</summary>
		public const int DEATH_NOTICE_TILE_Y = 20;

		/// <summary>Tiled/Alpha region width</summary>
		public const int DEATH_NOTICE_TILE_WIDTH = 407;

		/// <summary>Tiled/Alpha region height</summary>
		public const int DEATH_NOTICE_TILE_HEIGHT = 425;

		/// <summary>Title X position</summary>
		public const int DEATH_NOTICE_TITLE_X = 190;

		/// <summary>Title Y position</summary>
		public const int DEATH_NOTICE_TITLE_Y = 24;

		/// <summary>Title width</summary>
		public const int DEATH_NOTICE_TITLE_WIDTH = 120;

		/// <summary>Title height</summary>
		public const int DEATH_NOTICE_TITLE_HEIGHT = 20;

		/// <summary>Content X position</summary>
		public const int DEATH_NOTICE_CONTENT_X = 50;

		/// <summary>Content width</summary>
		public const int DEATH_NOTICE_CONTENT_WIDTH = 380;

		/// <summary>Content Y position - Line 1</summary>
		public const int DEATH_NOTICE_CONTENT_Y_1 = 50;

		/// <summary>Content height - Line 1</summary>
		public const int DEATH_NOTICE_CONTENT_HEIGHT_1 = 40;

		/// <summary>Content Y position - Line 2</summary>
		public const int DEATH_NOTICE_CONTENT_Y_2 = 100;

		/// <summary>Content height - Line 2</summary>
		public const int DEATH_NOTICE_CONTENT_HEIGHT_2 = 45;

		/// <summary>Content Y position - Line 3</summary>
		public const int DEATH_NOTICE_CONTENT_Y_3 = 140;

		/// <summary>Content height - Line 3</summary>
		public const int DEATH_NOTICE_CONTENT_HEIGHT_3 = 60;

		/// <summary>Content Y position - Line 4</summary>
		public const int DEATH_NOTICE_CONTENT_Y_4 = 204;

		/// <summary>Content height - Line 4</summary>
		public const int DEATH_NOTICE_CONTENT_HEIGHT_4 = 65;

		/// <summary>Content Y position - Line 5</summary>
		public const int DEATH_NOTICE_CONTENT_Y_5 = 269;

		/// <summary>Content height - Line 5</summary>
		public const int DEATH_NOTICE_CONTENT_HEIGHT_5 = 65;

		/// <summary>Content Y position - Line 6</summary>
		public const int DEATH_NOTICE_CONTENT_Y_6 = 334;

		/// <summary>Content height - Line 6</summary>
		public const int DEATH_NOTICE_CONTENT_HEIGHT_6 = 70;

		/// <summary>Button X position</summary>
		public const int DEATH_NOTICE_BUTTON_X = 195;

		/// <summary>Button Y position</summary>
		public const int DEATH_NOTICE_BUTTON_Y = 410;

		#endregion

		#region RenounceYoungGump Layout

		/// <summary>Gump X position</summary>
		public const int RENOUNCE_X = 150;

		/// <summary>Gump Y position</summary>
		public const int RENOUNCE_Y = 50;

		/// <summary>Background X position</summary>
		public const int RENOUNCE_BG_X = 0;

		/// <summary>Background Y position</summary>
		public const int RENOUNCE_BG_Y = 0;

		/// <summary>Background width</summary>
		public const int RENOUNCE_BG_WIDTH = 450;

		/// <summary>Background height</summary>
		public const int RENOUNCE_BG_HEIGHT = 400;

		/// <summary>Title X position</summary>
		public const int RENOUNCE_TITLE_X = 0;

		/// <summary>Title Y position</summary>
		public const int RENOUNCE_TITLE_Y = 30;

		/// <summary>Title width</summary>
		public const int RENOUNCE_TITLE_WIDTH = 450;

		/// <summary>Title height</summary>
		public const int RENOUNCE_TITLE_HEIGHT = 35;

		/// <summary>Body X position</summary>
		public const int RENOUNCE_BODY_X = 30;

		/// <summary>Body Y position</summary>
		public const int RENOUNCE_BODY_Y = 70;

		/// <summary>Body width</summary>
		public const int RENOUNCE_BODY_WIDTH = 390;

		/// <summary>Body height</summary>
		public const int RENOUNCE_BODY_HEIGHT = 210;

		/// <summary>OK Button X position</summary>
		public const int RENOUNCE_OK_BUTTON_X = 45;

		/// <summary>OK Button Y position</summary>
		public const int RENOUNCE_OK_BUTTON_Y = 298;

		/// <summary>OK Button label X position</summary>
		public const int RENOUNCE_OK_LABEL_X = 78;

		/// <summary>OK Button label Y position</summary>
		public const int RENOUNCE_OK_LABEL_Y = 300;

		/// <summary>OK Button label width</summary>
		public const int RENOUNCE_OK_LABEL_WIDTH = 100;

		/// <summary>OK Button label height</summary>
		public const int RENOUNCE_OK_LABEL_HEIGHT = 35;

		/// <summary>Cancel Button X position</summary>
		public const int RENOUNCE_CANCEL_BUTTON_X = 178;

		/// <summary>Cancel Button Y position</summary>
		public const int RENOUNCE_CANCEL_BUTTON_Y = 298;

		/// <summary>Cancel Button label X position</summary>
		public const int RENOUNCE_CANCEL_LABEL_X = 211;

		/// <summary>Cancel Button label Y position</summary>
		public const int RENOUNCE_CANCEL_LABEL_Y = 300;

		/// <summary>Cancel Button label width</summary>
		public const int RENOUNCE_CANCEL_LABEL_WIDTH = 100;

		/// <summary>Cancel Button label height</summary>
		public const int RENOUNCE_CANCEL_LABEL_HEIGHT = 35;

		/// <summary>Button ID for OK/Confirm</summary>
		public const int RENOUNCE_BUTTON_ID_CONFIRM = 1;

		/// <summary>Button ID for Cancel</summary>
		public const int RENOUNCE_BUTTON_ID_CANCEL = 0;

		#endregion

		#region YoungStatusLostGump Layout

		/// <summary>Gump X position</summary>
		public const int STATUS_LOST_X = 150;

		/// <summary>Gump Y position</summary>
		public const int STATUS_LOST_Y = 100;

		/// <summary>Background X position</summary>
		public const int STATUS_LOST_BG_X = 0;

		/// <summary>Background Y position</summary>
		public const int STATUS_LOST_BG_Y = 0;

		/// <summary>Background width</summary>
		public const int STATUS_LOST_BG_WIDTH = 500;

		/// <summary>Background height</summary>
		public const int STATUS_LOST_BG_HEIGHT = 400;

		/// <summary>Tiled/Alpha region X position</summary>
		public const int STATUS_LOST_TILE_X = 10;

		/// <summary>Tiled/Alpha region Y position</summary>
		public const int STATUS_LOST_TILE_Y = 10;

		/// <summary>Tiled/Alpha region width</summary>
		public const int STATUS_LOST_TILE_WIDTH = 480;

		/// <summary>Tiled/Alpha region height</summary>
		public const int STATUS_LOST_TILE_HEIGHT = 380;

		/// <summary>Title X position</summary>
		public const int STATUS_LOST_TITLE_X = 20;

		/// <summary>Title Y position</summary>
		public const int STATUS_LOST_TITLE_Y = 20;

		/// <summary>Title width</summary>
		public const int STATUS_LOST_TITLE_WIDTH = 460;

		/// <summary>Title height</summary>
		public const int STATUS_LOST_TITLE_HEIGHT = 30;

		/// <summary>Body X position</summary>
		public const int STATUS_LOST_BODY_X = 30;

		/// <summary>Body Y position</summary>
		public const int STATUS_LOST_BODY_Y = 60;

		/// <summary>Body width</summary>
		public const int STATUS_LOST_BODY_WIDTH = 440;

		/// <summary>Body height</summary>
		public const int STATUS_LOST_BODY_HEIGHT = 280;

		/// <summary>Button X position</summary>
		public const int STATUS_LOST_BUTTON_X = 210;

		/// <summary>Button Y position</summary>
		public const int STATUS_LOST_BUTTON_Y = 350;

		/// <summary>Button label X position</summary>
		public const int STATUS_LOST_BUTTON_LABEL_X = 233;

		/// <summary>Button label Y position</summary>
		public const int STATUS_LOST_BUTTON_LABEL_Y = 352;

		/// <summary>Button label width</summary>
		public const int STATUS_LOST_BUTTON_LABEL_WIDTH = 100;

		/// <summary>Button label height</summary>
		public const int STATUS_LOST_BUTTON_LABEL_HEIGHT = 20;

		/// <summary>Button ID for OK</summary>
		public const int STATUS_LOST_BUTTON_ID_OK = 0;

		#endregion

		#region YoungLoginInfoGump Layout

		/// <summary>Gump X position</summary>
		public const int LOGIN_INFO_X = 100;

		/// <summary>Gump Y position</summary>
		public const int LOGIN_INFO_Y = 15;

		/// <summary>Background X position</summary>
		public const int LOGIN_INFO_BG_X = 25;

		/// <summary>Background Y position</summary>
		public const int LOGIN_INFO_BG_Y = 10;

		/// <summary>Background width</summary>
		public const int LOGIN_INFO_BG_WIDTH = 425;

		/// <summary>Background height</summary>
		public const int LOGIN_INFO_BG_HEIGHT = 500;

		/// <summary>Tiled/Alpha region X position</summary>
		public const int LOGIN_INFO_TILE_X = 33;

		/// <summary>Tiled/Alpha region Y position</summary>
		public const int LOGIN_INFO_TILE_Y = 20;

		/// <summary>Tiled/Alpha region width</summary>
		public const int LOGIN_INFO_TILE_WIDTH = 407;

		/// <summary>Tiled/Alpha region height</summary>
		public const int LOGIN_INFO_TILE_HEIGHT = 470;

		/// <summary>Title X position</summary>
		public const int LOGIN_INFO_TITLE_X = 140;

		/// <summary>Title Y position</summary>
		public const int LOGIN_INFO_TITLE_Y = 24;

		/// <summary>Title width</summary>
		public const int LOGIN_INFO_TITLE_WIDTH = 200;

		/// <summary>Title height</summary>
		public const int LOGIN_INFO_TITLE_HEIGHT = 30;

		/// <summary>Content X position</summary>
		public const int LOGIN_INFO_CONTENT_X = 43;

		/// <summary>Content Y position</summary>
		public const int LOGIN_INFO_CONTENT_Y = 60;

		/// <summary>Content width</summary>
		public const int LOGIN_INFO_CONTENT_WIDTH = 387;

		/// <summary>Content height</summary>
		public const int LOGIN_INFO_CONTENT_HEIGHT = 400;

		/// <summary>Button X position</summary>
		public const int LOGIN_INFO_BUTTON_X = 200;

		/// <summary>Button Y position</summary>
		public const int LOGIN_INFO_BUTTON_Y = 460;

		/// <summary>Button ID for OK</summary>
		public const int LOGIN_INFO_BUTTON_ID_OK = 0;

		#endregion

		#region Image IDs

		/// <summary>Standard background image ID</summary>
		public const int IMAGE_BACKGROUND_STANDARD = 0xA28;

		/// <summary>Death notice background image ID</summary>
		public const int IMAGE_BACKGROUND_DEATH_NOTICE = 0x13BE;

		/// <summary>Standard tiled image ID</summary>
		public const int IMAGE_TILED_STANDARD = 0xA40;

		/// <summary>Button normal state image ID</summary>
		public const int IMAGE_BUTTON_NORMAL = 0xFA5;

		/// <summary>Button pressed state image ID</summary>
		public const int IMAGE_BUTTON_PRESSED = 0xFA7;

		/// <summary>OK button normal state image ID</summary>
		public const int IMAGE_BUTTON_OK_NORMAL = 0xF8;

		/// <summary>OK button pressed state image ID</summary>
		public const int IMAGE_BUTTON_OK_PRESSED = 0xF9;

		#endregion

		#region Colors

		/// <summary>White text color</summary>
		public const int COLOR_WHITE = 0xFFFFFF;

		/// <summary>Red text color</summary>
		public const int COLOR_RED = 0x7D00;

		#endregion
	}
}

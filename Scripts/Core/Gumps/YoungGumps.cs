using System;
using System.Text;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Gumps
{
	#region YoungDungeonWarning

	/// <summary>
	/// Warning gump displayed to Young/Iniciante players when entering dungeons.
	/// Informs players that monsters may attack them in dungeon areas.
	/// </summary>
	public class YoungDungeonWarning : Gump
	{
		/// <summary>
		/// Initializes a new instance of the YoungDungeonWarning gump.
		/// </summary>
		public YoungDungeonWarning() : base( YoungGumpConstants.DUNGEON_WARNING_X, YoungGumpConstants.DUNGEON_WARNING_Y )
		{
			AddBackground(
				YoungGumpConstants.DUNGEON_WARNING_BG_X,
				YoungGumpConstants.DUNGEON_WARNING_BG_Y,
				YoungGumpConstants.DUNGEON_WARNING_BG_WIDTH,
				YoungGumpConstants.DUNGEON_WARNING_BG_HEIGHT,
				YoungGumpConstants.IMAGE_BACKGROUND_STANDARD
			);

			AddHtml(
				YoungGumpConstants.DUNGEON_WARNING_HTML_X,
				YoungGumpConstants.DUNGEON_WARNING_HTML_Y,
				YoungGumpConstants.DUNGEON_WARNING_HTML_WIDTH,
				YoungGumpConstants.DUNGEON_WARNING_HTML_HEIGHT,
				YoungGumpStringConstants.MSG_DUNGEON_WARNING,
				true,
				true
			);

			AddButton(
				YoungGumpConstants.DUNGEON_WARNING_BUTTON_X,
				YoungGumpConstants.DUNGEON_WARNING_BUTTON_Y,
				YoungGumpConstants.IMAGE_BUTTON_NORMAL,
				YoungGumpConstants.IMAGE_BUTTON_PRESSED,
				YoungGumpConstants.STATUS_LOST_BUTTON_ID_OK,
				GumpButtonType.Reply,
				0
			);

			AddHtml(
				YoungGumpConstants.DUNGEON_WARNING_BUTTON_LABEL_X,
				YoungGumpConstants.DUNGEON_WARNING_BUTTON_LABEL_Y,
				YoungGumpConstants.DUNGEON_WARNING_BUTTON_LABEL_WIDTH,
				YoungGumpConstants.DUNGEON_WARNING_BUTTON_LABEL_HEIGHT,
				YoungGumpStringConstants.BUTTON_OKAY,
				false,
				false
			);
		}
	}

	#endregion

	#region YoungDeathNotice

	/// <summary>
	/// Informational gump displayed to Young/Iniciante players upon death.
	/// Explains death mechanics, resurrection process, and Young player benefits.
	/// </summary>
	public class YoungDeathNotice : Gump
	{
		/// <summary>
		/// Initializes a new instance of the YoungDeathNotice gump.
		/// </summary>
		public YoungDeathNotice() : base( YoungGumpConstants.DEATH_NOTICE_X, YoungGumpConstants.DEATH_NOTICE_Y )
		{
			Closable = false;

			AddBackground(
				YoungGumpConstants.DEATH_NOTICE_BG_X,
				YoungGumpConstants.DEATH_NOTICE_BG_Y,
				YoungGumpConstants.DEATH_NOTICE_BG_WIDTH,
				YoungGumpConstants.DEATH_NOTICE_BG_HEIGHT,
				YoungGumpConstants.IMAGE_BACKGROUND_DEATH_NOTICE
			);

			AddImageTiled(
				YoungGumpConstants.DEATH_NOTICE_TILE_X,
				YoungGumpConstants.DEATH_NOTICE_TILE_Y,
				YoungGumpConstants.DEATH_NOTICE_TILE_WIDTH,
				YoungGumpConstants.DEATH_NOTICE_TILE_HEIGHT,
				YoungGumpConstants.IMAGE_TILED_STANDARD
			);

			AddAlphaRegion(
				YoungGumpConstants.DEATH_NOTICE_TILE_X,
				YoungGumpConstants.DEATH_NOTICE_TILE_Y,
				YoungGumpConstants.DEATH_NOTICE_TILE_WIDTH,
				YoungGumpConstants.DEATH_NOTICE_TILE_HEIGHT
			);

			AddHtml(
				YoungGumpConstants.DEATH_NOTICE_TITLE_X,
				YoungGumpConstants.DEATH_NOTICE_TITLE_Y,
				YoungGumpConstants.DEATH_NOTICE_TITLE_WIDTH,
				YoungGumpConstants.DEATH_NOTICE_TITLE_HEIGHT,
				YoungGumpStringConstants.MSG_DEATH_TITLE,
				false,
				false
			);

			AddHtml(
				YoungGumpConstants.DEATH_NOTICE_CONTENT_X,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_Y_1,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_WIDTH,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_HEIGHT_1,
				YoungGumpStringConstants.MSG_DEATH_1,
				false,
				false
			);

			AddHtml(
				YoungGumpConstants.DEATH_NOTICE_CONTENT_X,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_Y_2,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_WIDTH,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_HEIGHT_2,
				YoungGumpStringConstants.MSG_DEATH_2,
				false,
				false
			);

			AddHtml(
				YoungGumpConstants.DEATH_NOTICE_CONTENT_X,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_Y_3,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_WIDTH,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_HEIGHT_3,
				YoungGumpStringConstants.MSG_DEATH_3,
				false,
				false
			);

			AddHtml(
				YoungGumpConstants.DEATH_NOTICE_CONTENT_X,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_Y_4,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_WIDTH,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_HEIGHT_4,
				YoungGumpStringConstants.MSG_DEATH_4,
				false,
				false
			);

			AddHtml(
				YoungGumpConstants.DEATH_NOTICE_CONTENT_X,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_Y_5,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_WIDTH,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_HEIGHT_5,
				YoungGumpStringConstants.MSG_DEATH_5,
				false,
				false
			);

			AddHtml(
				YoungGumpConstants.DEATH_NOTICE_CONTENT_X,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_Y_6,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_WIDTH,
				YoungGumpConstants.DEATH_NOTICE_CONTENT_HEIGHT_6,
				YoungGumpStringConstants.MSG_DEATH_6,
				false,
				false
			);

			AddButton(
				YoungGumpConstants.DEATH_NOTICE_BUTTON_X,
				YoungGumpConstants.DEATH_NOTICE_BUTTON_Y,
				YoungGumpConstants.IMAGE_BUTTON_OK_NORMAL,
				YoungGumpConstants.IMAGE_BUTTON_OK_PRESSED,
				YoungGumpConstants.STATUS_LOST_BUTTON_ID_OK,
				GumpButtonType.Reply,
				0
			);
		}
	}

	#endregion

	#region RenounceYoungGump

	/// <summary>
	/// Confirmation gump for players who wish to renounce their Young/Iniciante status.
	/// Allows players to voluntarily give up their protected status early.
	/// </summary>
	public class RenounceYoungGump : Gump
	{
		/// <summary>
		/// Initializes a new instance of the RenounceYoungGump gump.
		/// </summary>
		public RenounceYoungGump() : base( YoungGumpConstants.RENOUNCE_X, YoungGumpConstants.RENOUNCE_Y )
		{
			AddBackground(
				YoungGumpConstants.RENOUNCE_BG_X,
				YoungGumpConstants.RENOUNCE_BG_Y,
				YoungGumpConstants.RENOUNCE_BG_WIDTH,
				YoungGumpConstants.RENOUNCE_BG_HEIGHT,
				YoungGumpConstants.IMAGE_BACKGROUND_STANDARD
			);

			AddHtml(
				YoungGumpConstants.RENOUNCE_TITLE_X,
				YoungGumpConstants.RENOUNCE_TITLE_Y,
				YoungGumpConstants.RENOUNCE_TITLE_WIDTH,
				YoungGumpConstants.RENOUNCE_TITLE_HEIGHT,
				YoungGumpStringConstants.MSG_RENOUNCE_TITLE,
				false,
				false
			);

			AddHtml(
				YoungGumpConstants.RENOUNCE_BODY_X,
				YoungGumpConstants.RENOUNCE_BODY_Y,
				YoungGumpConstants.RENOUNCE_BODY_WIDTH,
				YoungGumpConstants.RENOUNCE_BODY_HEIGHT,
				YoungGumpStringConstants.MSG_RENOUNCE_BODY,
				true,
				true
			);

			AddButton(
				YoungGumpConstants.RENOUNCE_OK_BUTTON_X,
				YoungGumpConstants.RENOUNCE_OK_BUTTON_Y,
				YoungGumpConstants.IMAGE_BUTTON_NORMAL,
				YoungGumpConstants.IMAGE_BUTTON_PRESSED,
				YoungGumpConstants.RENOUNCE_BUTTON_ID_CONFIRM,
				GumpButtonType.Reply,
				0
			);

			AddHtml(
				YoungGumpConstants.RENOUNCE_OK_LABEL_X,
				YoungGumpConstants.RENOUNCE_OK_LABEL_Y,
				YoungGumpConstants.RENOUNCE_OK_LABEL_WIDTH,
				YoungGumpConstants.RENOUNCE_OK_LABEL_HEIGHT,
				YoungGumpStringConstants.BUTTON_OKAY,
				false,
				false
			);

			AddButton(
				YoungGumpConstants.RENOUNCE_CANCEL_BUTTON_X,
				YoungGumpConstants.RENOUNCE_CANCEL_BUTTON_Y,
				YoungGumpConstants.IMAGE_BUTTON_NORMAL,
				YoungGumpConstants.IMAGE_BUTTON_PRESSED,
				YoungGumpConstants.RENOUNCE_BUTTON_ID_CANCEL,
				GumpButtonType.Reply,
				0
			);

			AddHtml(
				YoungGumpConstants.RENOUNCE_CANCEL_LABEL_X,
				YoungGumpConstants.RENOUNCE_CANCEL_LABEL_Y,
				YoungGumpConstants.RENOUNCE_CANCEL_LABEL_WIDTH,
				YoungGumpConstants.RENOUNCE_CANCEL_LABEL_HEIGHT,
				YoungGumpStringConstants.BUTTON_CANCEL,
				false,
				false
			);
		}

		/// <summary>
		/// Handles button clicks in the RenounceYoungGump.
		/// </summary>
		/// <param name="sender">The network state of the player</param>
		/// <param name="info">The relay information containing button ID</param>
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( info.ButtonID == YoungGumpConstants.RENOUNCE_BUTTON_ID_CONFIRM )
			{
				Account acc = from.Account as Account;

				if ( acc != null )
				{
					acc.RemoveYoungStatus( 0 );
					from.SendMessage( YoungGumpStringConstants.MSG_RENOUNCED_STATUS );
				}
			}
			else
			{
				from.SendMessage( YoungGumpStringConstants.MSG_NOT_RENOUNCED );
			}
		}
	}

	#endregion

	#region YoungStatusLostGump

	/// <summary>
	/// Informational gump shown when a player loses their Iniciante (Young) status.
	/// Congratulates the player and explains the consequences of no longer being protected.
	/// </summary>
	public class YoungStatusLostGump : Gump
	{
		/// <summary>
		/// Initializes a new instance of the YoungStatusLostGump gump.
		/// </summary>
		public YoungStatusLostGump() : base( YoungGumpConstants.STATUS_LOST_X, YoungGumpConstants.STATUS_LOST_Y )
		{
			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddBackground(
				YoungGumpConstants.STATUS_LOST_BG_X,
				YoungGumpConstants.STATUS_LOST_BG_Y,
				YoungGumpConstants.STATUS_LOST_BG_WIDTH,
				YoungGumpConstants.STATUS_LOST_BG_HEIGHT,
				YoungGumpConstants.IMAGE_BACKGROUND_STANDARD
			);

			AddImageTiled(
				YoungGumpConstants.STATUS_LOST_TILE_X,
				YoungGumpConstants.STATUS_LOST_TILE_Y,
				YoungGumpConstants.STATUS_LOST_TILE_WIDTH,
				YoungGumpConstants.STATUS_LOST_TILE_HEIGHT,
				YoungGumpConstants.IMAGE_TILED_STANDARD
			);

			AddAlphaRegion(
				YoungGumpConstants.STATUS_LOST_TILE_X,
				YoungGumpConstants.STATUS_LOST_TILE_Y,
				YoungGumpConstants.STATUS_LOST_TILE_WIDTH,
				YoungGumpConstants.STATUS_LOST_TILE_HEIGHT
			);

			AddHtml(
				YoungGumpConstants.STATUS_LOST_TITLE_X,
				YoungGumpConstants.STATUS_LOST_TITLE_Y,
				YoungGumpConstants.STATUS_LOST_TITLE_WIDTH,
				YoungGumpConstants.STATUS_LOST_TITLE_HEIGHT,
				YoungGumpStringConstants.TITLE_STATUS_LOST,
				false,
				false
			);

			// Build body text using StringBuilder for better performance
			StringBuilder bodyText = new StringBuilder();
			bodyText.Append( YoungGumpStringConstants.BODY_GREETING );
			bodyText.Append( YoungGumpStringConstants.BODY_INTRO );
			bodyText.Append( YoungGumpStringConstants.BODY_QUESTION );
			bodyText.Append( YoungGumpStringConstants.BODY_EXPLANATION );
			bodyText.Append( YoungGumpStringConstants.BODY_BULLET_1 );
			bodyText.Append( YoungGumpStringConstants.BODY_BULLET_2 );
			bodyText.Append( YoungGumpStringConstants.BODY_BULLET_3 );
			bodyText.Append( YoungGumpStringConstants.BODY_BULLET_4 );
			bodyText.Append( YoungGumpStringConstants.BODY_CLOSING );

			AddHtml(
				YoungGumpConstants.STATUS_LOST_BODY_X,
				YoungGumpConstants.STATUS_LOST_BODY_Y,
				YoungGumpConstants.STATUS_LOST_BODY_WIDTH,
				YoungGumpConstants.STATUS_LOST_BODY_HEIGHT,
				bodyText.ToString(),
				true,
				true
			);

			AddButton(
				YoungGumpConstants.STATUS_LOST_BUTTON_X,
				YoungGumpConstants.STATUS_LOST_BUTTON_Y,
				YoungGumpConstants.IMAGE_BUTTON_OK_NORMAL,
				YoungGumpConstants.IMAGE_BUTTON_OK_PRESSED,
				YoungGumpConstants.STATUS_LOST_BUTTON_ID_OK,
				GumpButtonType.Reply,
				0
			);
		}

		/// <summary>
		/// Handles button clicks in the YoungStatusLostGump.
		/// This gump is informational only, so it simply closes when OK is clicked.
		/// </summary>
		/// <param name="sender">The network state of the player</param>
		/// <param name="info">The relay information containing button ID</param>
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			// Just close the gump - it's informational only
		}
	}

	#endregion

	#region YoungLoginInfoGump

	/// <summary>
	/// Informational gump displayed to Young/Iniciante players on login.
	/// Explains how the Young status works, its benefits, and how it can expire or be removed.
	/// </summary>
	public class YoungLoginInfoGump : Gump
	{
		/// <summary>
		/// Initializes a new instance of the YoungLoginInfoGump gump.
		/// </summary>
		public YoungLoginInfoGump() : base( YoungGumpConstants.LOGIN_INFO_X, YoungGumpConstants.LOGIN_INFO_Y )
		{
			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddBackground(
				YoungGumpConstants.LOGIN_INFO_BG_X,
				YoungGumpConstants.LOGIN_INFO_BG_Y,
				YoungGumpConstants.LOGIN_INFO_BG_WIDTH,
				YoungGumpConstants.LOGIN_INFO_BG_HEIGHT,
				YoungGumpConstants.IMAGE_BACKGROUND_DEATH_NOTICE
			);

			AddImageTiled(
				YoungGumpConstants.LOGIN_INFO_TILE_X,
				YoungGumpConstants.LOGIN_INFO_TILE_Y,
				YoungGumpConstants.LOGIN_INFO_TILE_WIDTH,
				YoungGumpConstants.LOGIN_INFO_TILE_HEIGHT,
				YoungGumpConstants.IMAGE_TILED_STANDARD
			);

			AddAlphaRegion(
				YoungGumpConstants.LOGIN_INFO_TILE_X,
				YoungGumpConstants.LOGIN_INFO_TILE_Y,
				YoungGumpConstants.LOGIN_INFO_TILE_WIDTH,
				YoungGumpConstants.LOGIN_INFO_TILE_HEIGHT
			);

			AddHtml(
				YoungGumpConstants.LOGIN_INFO_TITLE_X,
				YoungGumpConstants.LOGIN_INFO_TITLE_Y,
				YoungGumpConstants.LOGIN_INFO_TITLE_WIDTH,
				YoungGumpConstants.LOGIN_INFO_TITLE_HEIGHT,
				YoungGumpStringConstants.MSG_LOGIN_INFO_TITLE,
				false,
				false
			);

			AddHtml(
				YoungGumpConstants.LOGIN_INFO_CONTENT_X,
				YoungGumpConstants.LOGIN_INFO_CONTENT_Y,
				YoungGumpConstants.LOGIN_INFO_CONTENT_WIDTH,
				YoungGumpConstants.LOGIN_INFO_CONTENT_HEIGHT,
				YoungGumpStringConstants.MSG_LOGIN_INFO_BODY,
				true,
				true
			);

			AddButton(
				YoungGumpConstants.LOGIN_INFO_BUTTON_X,
				YoungGumpConstants.LOGIN_INFO_BUTTON_Y,
				YoungGumpConstants.IMAGE_BUTTON_OK_NORMAL,
				YoungGumpConstants.IMAGE_BUTTON_OK_PRESSED,
				YoungGumpConstants.LOGIN_INFO_BUTTON_ID_OK,
				GumpButtonType.Reply,
				0
			);
		}

		/// <summary>
		/// Handles button clicks in the YoungLoginInfoGump.
		/// This gump is informational only, so it simply closes when OK is clicked.
		/// </summary>
		/// <param name="sender">The network state of the player</param>
		/// <param name="info">The relay information containing button ID</param>
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			// Just close the gump - it's informational only
		}
	}

	#endregion
}

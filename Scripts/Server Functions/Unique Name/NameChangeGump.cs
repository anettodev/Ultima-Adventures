using System;
using System.Collections;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Misc;
using Server.Items;

namespace Server.Gumps
{
	/// <summary>
	/// Gump for forced name change when chosen name is already in use.
	/// Non-closable gump that requires player to choose a different name.
	/// </summary>
	public class NameChangeGump : Gump
	{
		#region Gump Construction

		/// <summary>
		/// Creates a new forced Name Change gump.
		/// </summary>
		/// <param name="from">Mobile viewing the gump</param>
		public NameChangeGump(Mobile from) : base(NameChangeConstants.GUMP_BASE_X_FORCED, NameChangeConstants.GUMP_BASE_Y_FORCED)
		{
			Closable = false;
			Disposable = false;
			Dragable = true;
			Resizable = false;

			AddPage(0);

			AddHtml(
				NameChangeConstants.FORCED_HTML_TEXT_X,
				NameChangeConstants.FORCED_HTML_TEXT_Y,
				NameChangeConstants.FORCED_HTML_TEXT_WIDTH,
				NameChangeConstants.FORCED_HTML_TEXT_HEIGHT,
				NameChangeStringConstants.GUMP_TEXT_NAME_CHANGE,
				(bool)true,
				(bool)false
			);
			AddBackground(
				NameChangeConstants.FORCED_BACKGROUND_X,
				NameChangeConstants.FORCED_BACKGROUND_Y,
				NameChangeConstants.FORCED_BACKGROUND_WIDTH,
				NameChangeConstants.FORCED_BACKGROUND_HEIGHT,
				NameChangeConstants.FORCED_BACKGROUND_ID
			);
			AddBackground(
				NameChangeConstants.FORCED_TEXT_BACKGROUND_X,
				NameChangeConstants.FORCED_TEXT_BACKGROUND_Y,
				NameChangeConstants.FORCED_TEXT_BACKGROUND_WIDTH,
				NameChangeConstants.FORCED_TEXT_BACKGROUND_HEIGHT,
				NameChangeConstants.FORCED_TEXT_BACKGROUND_ID
			);

			AddLabel(
				NameChangeConstants.FORCED_LABEL_X,
				NameChangeConstants.FORCED_LABEL_Y,
				NameChangeConstants.FORCED_LABEL_HUE,
				NameChangeStringConstants.LABEL_NEW_NAME
			);
			AddTextEntry(
				NameChangeConstants.FORCED_TEXT_ENTRY_X,
				NameChangeConstants.FORCED_TEXT_ENTRY_Y,
				NameChangeConstants.FORCED_TEXT_ENTRY_WIDTH,
				NameChangeConstants.FORCED_TEXT_ENTRY_HEIGHT,
				NameChangeConstants.FORCED_TEXT_ENTRY_HUE,
				NameChangeConstants.TEXT_ENTRY_ID,
				NameChangeStringConstants.PLACEHOLDER_TYPE_HERE,
				NameChangeConstants.TEXT_ENTRY_MAX_LENGTH
			);
			AddButton(
				NameChangeConstants.FORCED_BUTTON_X,
				NameChangeConstants.FORCED_BUTTON_Y,
				NameChangeConstants.FORCED_BUTTON_NORMAL_ID,
				NameChangeConstants.FORCED_BUTTON_PRESSED_ID,
				NameChangeConstants.FORCED_BUTTON_REPLY_ID,
				GumpButtonType.Reply,
				0
			);
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Handles gump response and processes name change request.
		/// </summary>
		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;
			if (from == null)
			{
				return;
			}

			string name = NameChangeHelper.GetString(info, NameChangeConstants.TEXT_ENTRY_ID);
			if (name == null || string.IsNullOrEmpty(name))
			{
				from.SendMessage(NameChangeConstants.MSG_COLOR_INFO, NameChangeStringConstants.MSG_NAME_REQUIRED);
				from.SendGump(new NameChangeGump(from));
				return;
			}

			// Validate and apply name change without gold requirement
			if (!NameChangeHelper.ValidateAndApplyNameChange(from, name, requireGold: false))
			{
				// Error messages are sent by helper method, reopen gump (required - non-closable)
				from.SendGump(new NameChangeGump(from));
				return;
			}
		}

		#endregion
	}
}
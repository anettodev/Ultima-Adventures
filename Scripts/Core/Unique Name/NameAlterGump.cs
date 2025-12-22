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
	/// Gump for Name Change Contract that allows players to change their character name.
	/// Free name change option (no gold cost).
	/// </summary>
	public class NameAlterGump : Gump
	{
		#region Gump Construction

		/// <summary>
		/// Creates a new Name Alter gump.
		/// </summary>
		/// <param name="from">Mobile viewing the gump</param>
		public NameAlterGump(Mobile from) : base(NameChangeConstants.GUMP_BASE_X_LEGAL, NameChangeConstants.GUMP_BASE_Y_LEGAL)
		{
			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			this.Resizable = false;

			string htmlText = string.Format(NameChangeStringConstants.HTML_FORMAT, NameChangeStringConstants.GUMP_TEXT_NAME_ALTER);

			AddPage(0);
			AddImage(0, 0, 151);
			AddImage(300, 0, 151);
			AddImage(0, 120, 151);
			AddImage(300, 120, 151);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(2, 118, 129);
			AddImage(298, 118, 129);
			AddImage(8, 7, 145);
			AddImage(32, 353, 130);
			AddImage(188, 182, 136);
			AddImage(168, 16, 130);
			AddImage(257, 16, 130);
			AddImage(552, 11, 143);
			AddImage(16, 348, 159);
			AddTextEntry(
				NameChangeConstants.TEXT_ENTRY_X,
				NameChangeConstants.TEXT_ENTRY_Y,
				NameChangeConstants.TEXT_ENTRY_WIDTH,
				NameChangeConstants.TEXT_ENTRY_HEIGHT,
				NameChangeConstants.TEXT_ENTRY_HUE,
				NameChangeConstants.TEXT_ENTRY_ID,
				NameChangeStringConstants.PLACEHOLDER_TYPE_HERE,
				NameChangeConstants.TEXT_ENTRY_MAX_LENGTH
			);
			AddHtml(
				NameChangeConstants.HTML_TEXT_X,
				NameChangeConstants.HTML_TEXT_Y,
				NameChangeConstants.HTML_TEXT_WIDTH,
				NameChangeConstants.HTML_TEXT_HEIGHT,
				htmlText,
				(bool)false,
				(bool)false
			);
			AddButton(
				NameChangeConstants.BUTTON_X,
				NameChangeConstants.BUTTON_Y,
				NameChangeConstants.BUTTON_NORMAL_ID,
				NameChangeConstants.BUTTON_PRESSED_ID,
				NameChangeConstants.BUTTON_REPLY_ID,
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
			if (name == null)
			{
				from.SendMessage(NameChangeConstants.MSG_COLOR_INFO, NameChangeStringConstants.MSG_NAME_OPTIONAL);
				from.SendGump(new NameAlterGump(from));
				return;
			}

			if (string.IsNullOrEmpty(name))
			{
				from.SendMessage(NameChangeConstants.MSG_COLOR_INFO, NameChangeStringConstants.MSG_NAME_REQUIRED);
				from.SendGump(new NameAlterGump(from));
				return;
			}

			// Validate and apply name change without gold requirement
			if (!NameChangeHelper.ValidateAndApplyNameChange(from, name, requireGold: false))
			{
				// Error messages are sent by helper method, reopen gump
				from.SendGump(new NameAlterGump(from));
				return;
			}
		}

		#endregion
	}
}
using System;
using Server;
using Server.Misc;
using Server.Network;
using System.Text;
using System.IO;
using System.Threading;
using Server.Gumps;
using System.Collections;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{
	/// <summary>
	/// Census Records item that allows players to change their character name for a gold fee.
	/// Can be legal (sages) or forged (thieves guild) version.
	/// </summary>
	[Flipable(NameChangeConstants.ITEM_ID_CENSUS_RECORDS_1, NameChangeConstants.ITEM_ID_CENSUS_RECORDS_2)]
	public class CensusRecords : Item
	{
		#region Constructors

		/// <summary>
		/// Creates a new Census Records item.
		/// </summary>
		[Constructable]
		public CensusRecords() : base(NameChangeConstants.ITEM_ID_CENSUS_RECORDS_1)
		{
			Weight = 1.0;
			Name = NameChangeStringConstants.ITEM_NAME_CENSUS_RECORDS;
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		public CensusRecords(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Opens the census gump when double-clicked.
		/// Determines if records are legal or forged based on item name.
		/// </summary>
		public override void OnDoubleClick(Mobile e)
		{
			bool isLegal = (Name == NameChangeStringConstants.ITEM_NAME_CENSUS_RECORDS);
			e.SendGump(new CensusGump(e, isLegal));
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the item.
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		/// <summary>
		/// Deserializes the item.
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}

namespace Server.Gumps
{
	/// <summary>
	/// Gump for Census Records name change interface.
	/// Supports both legal (sages) and forged (thieves guild) versions.
	/// </summary>
	public class CensusGump : Gump
	{
		#region Gump Construction

		/// <summary>
		/// Creates a new Census Records gump.
		/// </summary>
		/// <param name="from">Mobile viewing the gump</param>
		/// <param name="isLegal">True if legal census records, false if forged</param>
		public CensusGump(Mobile from, bool isLegal) : base(NameChangeConstants.GUMP_BASE_X_LEGAL, NameChangeConstants.GUMP_BASE_Y_LEGAL)
		{
			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			this.Resizable = false;

			string instructionText = isLegal
				? NameChangeStringConstants.GUMP_TEXT_CENSUS_LEGAL
				: NameChangeStringConstants.GUMP_TEXT_CENSUS_FORGED;

			string fullText = instructionText + NameChangeStringConstants.GUMP_TEXT_CENSUS_ADDITIONAL;
			string htmlText = string.Format(NameChangeStringConstants.HTML_FORMAT, fullText);

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
			if (string.IsNullOrEmpty(name))
			{
				from.SendMessage(NameChangeConstants.MSG_COLOR_INFO, NameChangeStringConstants.MSG_NAME_REQUIRED);
				return;
			}

			// Validate and apply name change with gold requirement
			if (!NameChangeHelper.ValidateAndApplyNameChange(from, name, requireGold: true))
			{
				// Error messages are sent by helper method
				return;
			}
		}

		#endregion
	}
}
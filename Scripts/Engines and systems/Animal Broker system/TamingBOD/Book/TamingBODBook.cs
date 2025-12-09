using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	/// <summary>
	/// Taming BOD Book - Item that stores Taming Bulk Order Deeds.
	/// Can hold up to 20 deeds and displays them in a gump interface.
	/// Entries are automatically sorted alphabetically when added.
	/// </summary>
	public class TamingBODBook : Item
	{
		#region Fields

		private List<TamingBODEntry> m_Entries;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the list of entries in the book.
		/// </summary>
		public List<TamingBODEntry> Entries
		{
			get { return m_Entries; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TamingBODBook class.
		/// </summary>
		[Constructable]
		public TamingBODBook() : base(TamingBODBookConstants.ITEM_ID)
		{
			Weight = TamingBODBookConstants.WEIGHT;
			Hue = TamingBODBookConstants.HUE;
			Name = TamingBODBookStringConstants.ITEM_NAME;
			m_Entries = new List<TamingBODEntry>();
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		public TamingBODBook(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the TamingBODBook.
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)TamingBODBookConstants.SERIALIZATION_VERSION);

			writer.WriteEncodedInt((int)m_Entries.Count);

			for (int i = 0; i < m_Entries.Count; ++i)
			{
				TamingBODEntry entry = m_Entries[i];
				if (entry != null)
				{
					entry.Serialize(writer);
				}
			}
		}

		/// <summary>
		/// Deserializes the TamingBODBook.
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					int count = reader.ReadEncodedInt();

					m_Entries = new List<TamingBODEntry>(count);

					for (int i = 0; i < count; ++i)
					{
						m_Entries.Add(new TamingBODEntry(reader));
					}
					break;
				}
			}
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to display the book's gump interface.
		/// </summary>
		/// <param name="from">The player using the book</param>
		public override void OnDoubleClick(Mobile from)
		{
			if (!from.InRange(GetWorldLocation(), TamingBODBookConstants.RANGE_CHECK_DISTANCE))
			{
				from.LocalOverheadMessage(MessageType.Regular, TamingBODBookConstants.MESSAGE_COLOR, TamingBODBookConstants.MSG_CANT_REACH);
				return;
			}

			if (m_Entries.Count == 0)
			{
				from.SendLocalizedMessage(TamingBODBookConstants.MSG_BOOK_EMPTY);
				return;
			}

			DisplayGump(from);
		}

		/// <summary>
		/// Handles drag and drop of TamingBOD items into the book.
		/// </summary>
		/// <param name="from">The player dropping the item</param>
		/// <param name="dropped">The item being dropped</param>
		/// <returns>True if the item was successfully added, false otherwise</returns>
		public override bool OnDragDrop(Mobile from, Item dropped)
		{
			TamingBOD bod = dropped as TamingBOD;
			if (bod == null)
			{
				from.SendMessage(TamingBODBookStringConstants.MSG_INVALID_CONTRACT);
				return false;
			}

			if (!ValidateBookInBackpack(from))
				return false;

			if (!CanAddEntry())
			{
				from.SendLocalizedMessage(TamingBODBookConstants.MSG_BOOK_FULL);
				return false;
			}

			AddEntryToBook(bod, from);
			return true;
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Validates that the book is in the player's backpack.
		/// </summary>
		/// <param name="from">The player to validate</param>
		/// <returns>True if valid, false otherwise</returns>
		private bool ValidateBookInBackpack(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(TamingBODBookConstants.MSG_MUST_BE_IN_BACKPACK);
				return false;
			}
			return true;
		}

		/// <summary>
		/// Checks if a new entry can be added to the book.
		/// </summary>
		/// <returns>True if the book has space, false if it's full</returns>
		private bool CanAddEntry()
		{
			return m_Entries.Count < TamingBODBookConstants.MAX_ENTRIES;
		}

		/// <summary>
		/// Adds a TamingBOD entry to the book and updates the display.
		/// </summary>
		/// <param name="bod">The TamingBOD being added</param>
		/// <param name="from">The player adding the entry</param>
		private void AddEntryToBook(TamingBOD bod, Mobile from)
		{
			// Use the new constructor that accepts TamingBOD to preserve tier and creature type
			m_Entries.Add(new TamingBODEntry(bod));
			m_Entries.Sort(); // Sort alphabetically
			InvalidateProperties();

			from.SendLocalizedMessage(TamingBODBookConstants.MSG_DEED_ADDED);
			DisplayGump(from);
			bod.Delete();
		}

		/// <summary>
		/// Displays the book's gump interface to the player.
		/// </summary>
		/// <param name="from">The player to display the gump to</param>
		private void DisplayGump(Mobile from)
		{
			PlayerMobile player = from as PlayerMobile;
			if (player != null)
			{
				player.SendGump(new TamingBODBookGump(player, this));
			}
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds properties to the object property list.
		/// </summary>
		/// <param name="list">The property list to add to</param>
		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			list.Add(TamingBODBookStringConstants.CONTRACT_TYPE);
			list.Add(TamingBODBookConstants.MSG_DEEDS_IN_BOOK, m_Entries.Count.ToString());
		}

		#endregion
	}
}

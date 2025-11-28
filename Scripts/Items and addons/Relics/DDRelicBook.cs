using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	/// <summary>
	/// Book relic item with random book appearance and title.
	/// </summary>
	public class DDRelicBook : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0xFBD;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new book relic with random appearance and title
		/// </summary>
		[Constructable]
		public DDRelicBook() : base(BASE_ITEM_ID)
		{
			Weight = RelicConstants.WEIGHT_LIGHT;
			ItemID = RandomThings.GetRandomBookItemID();
			Hue = RandomThings.GetRandomColor(0);
			Name = Server.Misc.RandomThings.GetBookTitle();
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicBook(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to display book message
		/// </summary>
		public override void OnDoubleClick(Mobile from)
		{
			from.SendMessage(RelicConstants.MSG_COLOR_ERROR, RelicStringConstants.MSG_BOOK_STORY);
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the book relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the book relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}

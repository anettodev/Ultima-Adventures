using System;
using Server;
using Server.Misc;
using Server.Network;
using System.Text;
using System.IO;
using System.Threading;
using Server.Gumps;

namespace Server.Items
{
	/// <summary>
	/// Name Change Contract item that allows players to change their character name.
	/// </summary>
	[Flipable(NameChangeConstants.ITEM_ID_CHANGE_NAME_1, NameChangeConstants.ITEM_ID_CHANGE_NAME_2)]
	public class ChangeName : Item
	{
		#region Constructors

		/// <summary>
		/// Creates a new Name Change Contract item.
		/// </summary>
		[Constructable]
		public ChangeName() : base(NameChangeConstants.ITEM_ID_CHANGE_NAME_1)
		{
			Weight = 1.0;
			Name = NameChangeStringConstants.ITEM_NAME_CHANGE_CONTRACT;
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		public ChangeName(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Opens the name alteration gump when double-clicked.
		/// </summary>
		public override void OnDoubleClick(Mobile e)
		{
			e.SendGump(new NameAlterGump(e));
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
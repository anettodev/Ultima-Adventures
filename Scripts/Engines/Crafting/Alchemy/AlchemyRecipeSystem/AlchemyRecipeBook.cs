using System;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;

namespace Server.Items
{
	/// <summary>
	/// Blessed alchemy recipe book that displays learned recipes organized by category.
	/// Players can view recipe details but cannot craft directly from the book.
	/// Uses Necromancer book style (ItemID 0x2253) with custom hue (2003).
	/// </summary>
	public class AlchemyRecipeBook : Item
	{
		[Constructable]
		public AlchemyRecipeBook() : base(Server.Engines.Craft.AlchemyRecipeConstants.BOOK_ITEM_ID)
		{
			Name = Server.Engines.Craft.AlchemyRecipeStringConstants.BOOK_NAME;
			Hue = Server.Engines.Craft.AlchemyRecipeConstants.BOOK_HUE;
			Weight = Server.Engines.Craft.AlchemyRecipeConstants.BOOK_WEIGHT;
			Layer = Layer.OneHanded;
			LootType = LootType.Blessed; // Never lost on death
		}

		public AlchemyRecipeBook(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			// Must be in backpack or held in hand
			if (!IsChildOf(from.Backpack) && from.FindItemOnLayer(Layer.OneHanded) != this)
			{
				from.SendMessage(Server.Engines.Craft.AlchemyRecipeConstants.MSG_COLOR_ERROR,
					Server.Engines.Craft.AlchemyRecipeStringConstants.MSG_BOOK_NOT_ACCESSIBLE);
				return;
			}

			if (from is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)from;

				// Close any existing gump
				from.CloseGump(typeof(Server.Engines.Craft.AlchemyRecipeBookGump));

				// Show recipe list gump starting with category 0
				from.SendGump(new Server.Engines.Craft.AlchemyRecipeBookGump(pm, this, 0));
			}
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			// Show that book is blessed
			list.Add(1038021); // blessed
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			// Ensure book remains blessed after server restart
			if (LootType != LootType.Blessed)
				LootType = LootType.Blessed;
		}
	}
}


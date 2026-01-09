using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Jewel relic item with random type (necklace, amulet, ring, earrings) and quality.
	/// </summary>
	public class DDRelicJewels : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x4210;
		private const int RANDOM_JEWEL_TYPE_MIN = 0;
		private const int RANDOM_JEWEL_TYPE_MAX = RelicConstants.RANDOM_JEWEL_TYPE_MAX;

		#endregion

		#region Fields

		/// <summary>
		/// Structure for jewel variant data
		/// </summary>
		private struct JewelVariant
		{
			public int ItemID;
			public string TypeName;

			public JewelVariant(int itemID, string typeName)
			{
				this.ItemID = itemID;
				this.TypeName = typeName;
			}
		}

		/// <summary>
		/// Array of jewel variants
		/// </summary>
		private static readonly JewelVariant[] JewelVariants = new JewelVariant[]
		{
			new JewelVariant(0x4210, RelicStringConstants.ITEM_TYPE_NECKLACE),
			new JewelVariant(0x4210, RelicStringConstants.ITEM_TYPE_AMULET),
			new JewelVariant(0x4210, RelicStringConstants.ITEM_TYPE_MEDALLION),
			new JewelVariant(0x4212, RelicStringConstants.ITEM_TYPE_RING),
			new JewelVariant(0x4213, RelicStringConstants.ITEM_TYPE_EARRINGS_SET),
			new JewelVariant(0x4212, RelicStringConstants.ITEM_TYPE_RING),
			new JewelVariant(0x4213, RelicStringConstants.ITEM_TYPE_EARRINGS_SET),
			new JewelVariant(0x4212, RelicStringConstants.ITEM_TYPE_RING),
			new JewelVariant(0x4213, RelicStringConstants.ITEM_TYPE_EARRINGS_PAIR)
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new jewel relic with random type and quality
		/// </summary>
		[Constructable]
		public DDRelicJewels() : base(BASE_ITEM_ID)
		{
			Weight = RelicConstants.WEIGHT_LIGHT;
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			int variant = Utility.RandomMinMax(RANDOM_JEWEL_TYPE_MIN, RANDOM_JEWEL_TYPE_MAX);
			JewelVariant jewel = JewelVariants[variant];

			ItemID = jewel.ItemID;

			string quality = RelicHelper.GetRandomQualityDescriptor();
			Name = quality + jewel.TypeName;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicJewels(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the jewels relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the jewels relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}

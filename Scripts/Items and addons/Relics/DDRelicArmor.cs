using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Armor relic item that can be flipped between two ItemID states.
	/// Supports shields and suits of armor with quality descriptors.
	/// </summary>
	public class DDRelicArmor : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x156C;
		private const int RANDOM_ARMOR_TYPE_MIN = 0;
		private const int RANDOM_ARMOR_TYPE_MAX = 14;
		private const int WEIGHT_ARMOR_SUIT = 60;

		#endregion

		#region Fields

		/// <summary>First ItemID for flipping</summary>
		public int RelicFlipID1;

		/// <summary>Second ItemID for flipping</summary>
		public int RelicFlipID2;

		/// <summary>
		/// Structure for armor variant data
		/// </summary>
		private struct ArmorVariant
		{
			public int ItemID;
			public int FlipID1;
			public int FlipID2;
			public string TypeName;
			public int Weight;

			public ArmorVariant(int itemID, int flipID1, int flipID2, string typeName, int weight)
			{
				this.ItemID = itemID;
				this.FlipID1 = flipID1;
				this.FlipID2 = flipID2;
				this.TypeName = typeName;
				this.Weight = weight;
			}
		}

		/// <summary>
		/// Array of armor variants
		/// </summary>
		private static readonly ArmorVariant[] ArmorVariants = new ArmorVariant[]
		{
			new ArmorVariant(0x156C, 0x156C, 0x156D, "escudo", RelicConstants.WEIGHT_FUR),
			new ArmorVariant(0x156E, 0x156E, 0x156F, "escudo", RelicConstants.WEIGHT_FUR),
			new ArmorVariant(0x1570, 0x1570, 0x1571, "escudo", RelicConstants.WEIGHT_FUR),
			new ArmorVariant(0x1572, 0x1572, 0x1573, "escudo", RelicConstants.WEIGHT_FUR),
			new ArmorVariant(0x1574, 0x1574, 0x1575, "escudo", RelicConstants.WEIGHT_FUR),
			new ArmorVariant(0x1576, 0x1576, 0x1577, "escudo", RelicConstants.WEIGHT_FUR),
			new ArmorVariant(0x1578, 0x1578, 0x1579, "escudo", RelicConstants.WEIGHT_FUR),
			new ArmorVariant(0x157A, 0x157A, 0x157B, "escudo", RelicConstants.WEIGHT_FUR),
			new ArmorVariant(0x157C, 0x157C, 0x157D, "escudo", RelicConstants.WEIGHT_FUR),
			new ArmorVariant(0x157E, 0x157E, 0x157F, "escudo", RelicConstants.WEIGHT_FUR),
			new ArmorVariant(0x1580, 0x1580, 0x1581, "escudo", RelicConstants.WEIGHT_FUR),
			new ArmorVariant(0x4228, 0x4228, 0x4229, "escudo", RelicConstants.WEIGHT_FUR),
			new ArmorVariant(0x422A, 0x422A, 0x422C, "escudo", RelicConstants.WEIGHT_FUR),
			new ArmorVariant(0x1508, 0x1508, 0x151C, "armadura completa", WEIGHT_ARMOR_SUIT),
			new ArmorVariant(0x1512, 0x1512, 0x151A, "armadura completa", WEIGHT_ARMOR_SUIT)
		};

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the first flip ItemID
		/// </summary>
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_FlipID1
		{
			get { return RelicFlipID1; }
			set { RelicFlipID1 = value; InvalidateProperties(); }
		}

		/// <summary>
		/// Gets or sets the second flip ItemID
		/// </summary>
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_FlipID2
		{
			get { return RelicFlipID2; }
			set { RelicFlipID2 = value; InvalidateProperties(); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new armor relic with random type and quality
		/// </summary>
		[Constructable]
		public DDRelicArmor() : base(BASE_ITEM_ID)
		{
			Weight = RelicConstants.WEIGHT_FUR;
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			int variant = Utility.RandomMinMax(RANDOM_ARMOR_TYPE_MIN, RANDOM_ARMOR_TYPE_MAX);
			ArmorVariant armor = ArmorVariants[variant];

			ItemID = armor.ItemID;
			RelicFlipID1 = armor.FlipID1;
			RelicFlipID2 = armor.FlipID2;
			Weight = armor.Weight;

			string quality = RelicHelper.GetRandomQualityDescriptor();
			string decorative = RelicHelper.GetRandomDecorativeTerm(true);

			Name = quality + decorative + " " + armor.TypeName;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicArmor(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to flip armor or show identification message
		/// </summary>
		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendMessage(RelicStringConstants.MSG_IDENTIFY_VALUE);
				from.SendMessage(RelicStringConstants.MSG_MUST_BE_IN_PACK);
			}
			else
			{
				if (ItemID == RelicFlipID1)
				{
					ItemID = RelicFlipID2;
				}
				else
				{
					ItemID = RelicFlipID1;
				}
			}
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the armor relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(RelicConstants.SERIALIZATION_VERSION);
			writer.Write(RelicFlipID1);
			writer.Write(RelicFlipID2);
		}

		/// <summary>
		/// Deserializes the armor relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			RelicFlipID1 = reader.ReadInt();
			RelicFlipID2 = reader.ReadInt();
		}

		#endregion
	}
}

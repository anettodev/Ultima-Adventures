using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Vase relic item with random visual variations and special value variants.
	/// </summary>
	public class DDRelicVase : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x44F1;
		private const int RANDOM_VASE_MIN = 0;
		private const int RANDOM_VASE_MAX = RelicConstants.RANDOM_VASE_MAX;

		#endregion

		#region Fields

		/// <summary>
		/// Structure for vase variant data
		/// </summary>
		private struct VaseVariant
		{
			public int ItemID;
			public int Weight;
			public bool HasSpecialValue;

			public VaseVariant(int itemID, int weight, bool hasSpecialValue)
			{
				this.ItemID = itemID;
				this.Weight = weight;
				this.HasSpecialValue = hasSpecialValue;
			}
		}

		/// <summary>
		/// Array of vase variants
		/// </summary>
		private static readonly VaseVariant[] VaseVariants = new VaseVariant[]
		{
			new VaseVariant(0x44F1, RelicConstants.WEIGHT_HEAVY, false),
			new VaseVariant(0xB46, RelicConstants.WEIGHT_HEAVY, false),
			new VaseVariant(0x44EF, RelicConstants.WEIGHT_HEAVY, false),
			new VaseVariant(0xB48, RelicConstants.WEIGHT_HEAVY, false),
			new VaseVariant(0xB45, RelicConstants.VASE_SPECIAL_WEIGHT, true),
			new VaseVariant(0xB47, RelicConstants.VASE_SPECIAL_WEIGHT, true),
			new VaseVariant(0x42B2, RelicConstants.VASE_SPECIAL_WEIGHT, true),
			new VaseVariant(0x42B3, RelicConstants.VASE_SPECIAL_WEIGHT, true),
			new VaseVariant(0x44F0, RelicConstants.VASE_SPECIAL_WEIGHT, true)
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new vase relic with random type and quality
		/// </summary>
		[Constructable]
		public DDRelicVase() : base(BASE_ITEM_ID)
		{
			Weight = RelicConstants.WEIGHT_HEAVY;
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			int variant = Utility.RandomMinMax(RANDOM_VASE_MIN, RANDOM_VASE_MAX);
			VaseVariant vase = VaseVariants[variant];

			ItemID = vase.ItemID;
			Weight = vase.Weight;

			if (vase.HasSpecialValue)
			{
				RelicGoldValue = Utility.RandomMinMax(RelicConstants.VASE_SPECIAL_VALUE_MIN, RelicConstants.VASE_SPECIAL_VALUE_MAX);
			}

			string quality = RelicHelper.GetRandomQualityDescriptor();
			Name = quality + RelicStringConstants.ITEM_TYPE_VASE;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicVase(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the vase relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the vase relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}

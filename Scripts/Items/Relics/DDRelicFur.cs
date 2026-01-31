using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Fur relic item with random animal type and quality.
	/// Some fur types have special hues (white or black).
	/// </summary>
	public class DDRelicFur : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x11F4;
		private const int RANDOM_FUR_TYPE_MIN = 0;
		private const int RANDOM_FUR_TYPE_MAX = RelicConstants.RANDOM_FUR_TYPE_MAX;
		private const int HUE_NONE = 0;

		#endregion

		#region Fields

		/// <summary>Item IDs for fur variants</summary>
		private static readonly int[] FUR_ITEM_IDS = new[]
		{
			0x11F4, 0x11F5, 0x11F6, 0x11F7, 0x11F8, 0x11F9, 0x11FA, 0x11FB
		};

		/// <summary>
		/// Structure for fur variant data
		/// </summary>
		private struct FurVariant
		{
			public string TypeName;
			public int Hue;

			public FurVariant(string typeName, int hue)
			{
				this.TypeName = typeName;
				this.Hue = hue;
			}
		}

		/// <summary>
		/// Array of fur variants with their special hues
		/// </summary>
		private static readonly FurVariant[] FurVariants = new FurVariant[]
		{
			new FurVariant(RelicStringConstants.FUR_TYPE_BEAVER, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_ERMINE, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_FOX, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_MARTEN, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_MINK, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_MUSKRAT, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_SABLE, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_BEAR, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_DEER, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_RABBIT, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_YETI, RelicStringConstants.HUE_WHITE_FUR),
			new FurVariant(RelicStringConstants.FUR_TYPE_DIRE_BEAR, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_POLAR_BEAR, RelicStringConstants.HUE_WHITE_FUR),
			new FurVariant(RelicStringConstants.FUR_TYPE_BLACK_WOLF, RelicStringConstants.HUE_BLACK_FUR),
			new FurVariant(RelicStringConstants.FUR_TYPE_BADGER, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_MAMMOTH, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_MASTODON, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_BUFFALO, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_CAMEL, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_CHEETAH, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_LEOPARD, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_LION, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_PANTHER, RelicStringConstants.HUE_BLACK_FUR),
			new FurVariant(RelicStringConstants.FUR_TYPE_LYNX, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_COUGAR, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_SABRETOOTH_TIGER, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_TIGER, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_GOAT, RelicStringConstants.HUE_WHITE_FUR),
			new FurVariant(RelicStringConstants.FUR_TYPE_GRIFFIN, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_HIPPOGRIFF, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_HYENA, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_JACKAL, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_WOLF, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_OTTER, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_KODIAK_BEAR, RelicStringConstants.HUE_BLACK_FUR),
			new FurVariant(RelicStringConstants.FUR_TYPE_UNICORN, RelicStringConstants.HUE_WHITE_FUR),
			new FurVariant(RelicStringConstants.FUR_TYPE_PEGASUS, RelicStringConstants.HUE_WHITE_FUR),
			new FurVariant(RelicStringConstants.FUR_TYPE_WEASEL, HUE_NONE),
			new FurVariant(RelicStringConstants.FUR_TYPE_WOLVERINE, HUE_NONE)
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new fur relic with random type and quality
		/// </summary>
		[Constructable]
		public DDRelicFur() : base(BASE_ITEM_ID)
		{
			Weight = RelicConstants.WEIGHT_FUR;
			ItemID = Utility.RandomList(FUR_ITEM_IDS);

			int variant = Utility.RandomMinMax(RANDOM_FUR_TYPE_MIN, RANDOM_FUR_TYPE_MAX);
			FurVariant fur = FurVariants[variant];

			if (fur.Hue == HUE_NONE)
			{
				Hue = Utility.RandomNeutralHue();
			}
			else
			{
				Hue = fur.Hue;
			}

			string quality = RelicHelper.GetRandomQualityDescriptor();
			Name = quality + " pacote de pele de " + fur.TypeName;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicFur(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the fur relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the fur relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}

using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Leather relic item with random animal type and quality.
	/// Can be either a bundle or stretched hide depending on ItemID.
	/// </summary>
	public class DDRelicLeather : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x106B;
		private const int RANDOM_LEATHER_TYPE_MIN = 0;
		private const int RANDOM_LEATHER_TYPE_MAX = 20;
		private const int HUE_MIN = 2401;
		private const int HUE_MAX = 2430;
		private const int BUNDLE_ITEM_ID_1 = 0x1079;
		private const int BUNDLE_ITEM_ID_2 = 0x1078;

		#endregion

		#region Fields

		/// <summary>Item IDs for leather variants</summary>
		private static readonly int[] LEATHER_ITEM_IDS = new[]
		{
			0x106B, 0x106A, 0x1069, 0x107C, 0x107B, 0x107A, 0x1079, 0x1078
		};

		/// <summary>Leather type names</summary>
		private static readonly string[] LEATHER_TYPES = new[]
		{
			"veado",
			"lobo",
			"dinossauro",
			"dragão",
			"crocodilo",
			"lagarto",
			"serpente",
			"urso",
			"leão",
			"mamute",
			"manticora",
			"rinoceronte",
			"dente-de-sabre",
			"basilisco",
			"gárgula",
			"unicórnio",
			"pégaso",
			"demônio",
			"grifo",
			"jacaré",
			"cobra"
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new leather relic with random type and quality
		/// </summary>
		[Constructable]
		public DDRelicLeather() : base(BASE_ITEM_ID)
		{
			Weight = RelicConstants.WEIGHT_FUR;
			ItemID = Utility.RandomList(LEATHER_ITEM_IDS);
			Hue = Utility.RandomMinMax(HUE_MIN, HUE_MAX);

			int leatherType = Utility.RandomMinMax(RANDOM_LEATHER_TYPE_MIN, RANDOM_LEATHER_TYPE_MAX);
			string leatherTypeName = LEATHER_TYPES[leatherType];

			string quality = RelicHelper.GetRandomQualityDescriptor();

			if (ItemID == BUNDLE_ITEM_ID_1 || ItemID == BUNDLE_ITEM_ID_2)
			{
				Name = quality + " pacote de couro de " + leatherTypeName;
			}
			else
			{
				Name = quality + " pele esticada de couro de " + leatherTypeName;
			}
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicLeather(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to display identification message
		/// </summary>
		public override void OnDoubleClick(Mobile from)
		{
			from.SendMessage(RelicConstants.MSG_COLOR_ERROR, RelicStringConstants.MSG_IDENTIFY_VALUE);
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the leather relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the leather relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}

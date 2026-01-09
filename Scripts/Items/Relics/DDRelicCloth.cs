using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Cloth relic item with random cloth type and quality.
	/// </summary>
	public class DDRelicCloth : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x175D;
		private const int RANDOM_CLOTH_TYPE_MIN = 0;
		private const int RANDOM_CLOTH_TYPE_MAX = RelicConstants.RANDOM_CLOTH_TYPE_MAX;

		#endregion

		#region Fields

		/// <summary>Item IDs for cloth variants</summary>
		private static readonly int[] CLOTH_ITEM_IDS = new[]
		{
			0x175D, 0x175E, 0x175F, 0x1760, 0x1761, 0x1762, 0x1763, 0x1764, 0x1765
		};

		/// <summary>Cloth type names</summary>
		private static readonly string[] CLOTH_TYPES = new[]
		{
			RelicStringConstants.CLOTH_TYPE_SILK,
			RelicStringConstants.CLOTH_TYPE_COTTON,
			RelicStringConstants.CLOTH_TYPE_HEMP,
			RelicStringConstants.CLOTH_TYPE_WOOL,
			RelicStringConstants.CLOTH_TYPE_NONE,
			RelicStringConstants.CLOTH_TYPE_NONE
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new cloth relic with random type and quality
		/// </summary>
		[Constructable]
		public DDRelicCloth() : base(BASE_ITEM_ID)
		{
			Weight = RelicConstants.WEIGHT_CLOTH;
			ItemID = Utility.RandomList(CLOTH_ITEM_IDS);
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			int clothType = Utility.RandomMinMax(RANDOM_CLOTH_TYPE_MIN, RANDOM_CLOTH_TYPE_MAX);
			string clothTypeName = CLOTH_TYPES[clothType];

			string quality = RelicHelper.GetRandomQualityDescriptor();
			Name = quality + " pacote de " + clothTypeName + "tecido";
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicCloth(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the cloth relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the cloth relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}

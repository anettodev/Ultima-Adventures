using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Decorative arts relic item with random visual variations.
	/// Can spawn as goblets, bowls, or scepters with various quality descriptors.
	/// </summary>
	public class DDRelicArts : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x4210;
		private const int RANDOM_TYPE_MIN = 0;
		private const int RANDOM_TYPE_MAX = 2;

		#endregion

		#region Fields

		/// <summary>Item IDs for goblet variants</summary>
		private static readonly int[] GOBLET_ITEM_IDS = new[] { 0x9CB, 0x9B3, 0x9BF, 0x9CB };

		/// <summary>Item IDs for bowl variants</summary>
		private static readonly int[] BOWL_ITEM_IDS = new[] { 0x42BE, 0x15F8, 0x15FD, 0x1603, 0x1604 };

		/// <summary>Item IDs for scepter variants</summary>
		private static readonly int[] SCEPTER_ITEM_IDS = new[] { 0xDF2, 0xDF3, 0xDF4, 0xDF5 };

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new decorative arts relic with random type and quality
		/// </summary>
		[Constructable]
		public DDRelicArts() : base(BASE_ITEM_ID)
		{
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			int type = Utility.RandomMinMax(RANDOM_TYPE_MIN, RANDOM_TYPE_MAX);
			string itemType;
			int weight;

			switch (type)
			{
				case 0:
					ItemID = Utility.RandomList(GOBLET_ITEM_IDS);
					itemType = RelicStringConstants.ITEM_TYPE_GOBLET;
					weight = RelicConstants.WEIGHT_LIGHT;
					break;
				case 1:
					ItemID = Utility.RandomList(BOWL_ITEM_IDS);
					itemType = RelicStringConstants.ITEM_TYPE_BOWL;
					weight = RelicConstants.WEIGHT_HEAVY;
					break;
				default:
					ItemID = Utility.RandomList(SCEPTER_ITEM_IDS);
					itemType = RelicStringConstants.ITEM_TYPE_SCEPTER;
					weight = RelicConstants.WEIGHT_MEDIUM;
					break;
			}

			Weight = weight;

			string quality = RelicHelper.GetRandomQualityDescriptor();
			string decorative = RelicHelper.GetRandomDecorativeTerm();
			Name = quality + decorative + itemType;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicArts(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the arts relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the arts relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}
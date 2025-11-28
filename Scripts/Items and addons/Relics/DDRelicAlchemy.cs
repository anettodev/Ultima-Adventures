using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Alchemy flask relic item with random visual appearance.
	/// </summary>
	public class DDRelicAlchemy : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x182A;

		#endregion

		#region Fields

		/// <summary>Item IDs for alchemy flask variants</summary>
		private static readonly int[] FLASK_ITEM_IDS = new[]
		{
			0x182A, 0x182B, 0x182C, 0x182D, 0x182E, 0x182F, 0x1830, 0x1831, 0x1832, 0x1833,
			0x1834, 0x1835, 0x1836, 0x1837, 0x1838, 0x1839, 0x183A, 0x183B, 0x183C, 0x183D,
			0x183E, 0x183F, 0x1840, 0x1841, 0x1842, 0x1843, 0x1844, 0x1845, 0x1846, 0x1847, 0x1848
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new alchemy flask relic with random appearance
		/// </summary>
		[Constructable]
		public DDRelicAlchemy() : base(BASE_ITEM_ID)
		{
			Weight = RelicConstants.WEIGHT_VERY_LIGHT;
			ItemID = Utility.RandomList(FLASK_ITEM_IDS);
			Name = RelicStringConstants.ITEM_TYPE_ALCHEMY_FLASK;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicAlchemy(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the alchemy flask relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the alchemy flask relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}

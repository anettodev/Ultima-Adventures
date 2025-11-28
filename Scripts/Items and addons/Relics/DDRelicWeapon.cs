using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Weapon relic item that can be flipped between two ItemID states.
	/// Supports various weapon types with quality descriptors.
	/// </summary>
	public class DDRelicWeapon : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x48B0;
		private const int RANDOM_WEAPON_MIN = 0;
		private const int RANDOM_WEAPON_MAX = 9;

		#endregion

		#region Fields

		/// <summary>First ItemID for flipping</summary>
		public int RelicFlipID1;

		/// <summary>Second ItemID for flipping</summary>
		public int RelicFlipID2;

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

		#region Fields

		/// <summary>
		/// Structure for weapon variant data
		/// </summary>
		private struct WeaponVariant
		{
			public int ItemID;
			public int FlipID1;
			public int FlipID2;
			public string TypeName;

			public WeaponVariant(int itemID, int flipID1, int flipID2, string typeName)
			{
				this.ItemID = itemID;
				this.FlipID1 = flipID1;
				this.FlipID2 = flipID2;
				this.TypeName = typeName;
			}
		}

		/// <summary>
		/// Array of weapon variants
		/// </summary>
		private static readonly WeaponVariant[] WeaponVariants = new WeaponVariant[]
		{
			new WeaponVariant(0x48B0, 0x48B0, 0x48B1, RelicStringConstants.ITEM_TYPE_AXE),
			new WeaponVariant(0x48B2, 0x48B2, 0x48B3, RelicStringConstants.ITEM_TYPE_AXE),
			new WeaponVariant(0x48BA, 0x48BA, 0x48BB, RelicStringConstants.ITEM_TYPE_SWORD),
			new WeaponVariant(0x48BC, 0x48BC, 0x48BD, RelicStringConstants.ITEM_TYPE_DAGGER),
			new WeaponVariant(0x48BE, 0x48BE, 0x48BF, RelicStringConstants.ITEM_TYPE_TRIDENT),
			new WeaponVariant(0x48C0, 0x48C0, 0x48C1, RelicStringConstants.ITEM_TYPE_WAR_HAMMER),
			new WeaponVariant(0x48C6, 0x48C6, 0x48C7, RelicStringConstants.ITEM_TYPE_SCYTHE),
			new WeaponVariant(0x48C8, 0x48C8, 0x48C9, RelicStringConstants.ITEM_TYPE_PIKE),
			new WeaponVariant(0x48CA, 0x48CA, 0x48CB, RelicStringConstants.ITEM_TYPE_LANCE),
			new WeaponVariant(0x48D0, 0x48D0, 0x48D1, RelicStringConstants.ITEM_TYPE_SWORDS)
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new weapon relic with random type and quality
		/// </summary>
		[Constructable]
		public DDRelicWeapon() : base(BASE_ITEM_ID)
		{
			Weight = RelicConstants.WEIGHT_MEDIUM;
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			int variant = Utility.RandomMinMax(RANDOM_WEAPON_MIN, RANDOM_WEAPON_MAX);
			WeaponVariant weapon = WeaponVariants[variant];

			ItemID = weapon.ItemID;
			RelicFlipID1 = weapon.FlipID1;
			RelicFlipID2 = weapon.FlipID2;

			string quality = RelicHelper.GetRandomQualityDescriptor();
			string decorative = RelicHelper.GetRandomDecorativeTerm(true);
			Name = quality + decorative + weapon.TypeName;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicWeapon(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to flip weapon or show identification message
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

		#region Helper Methods

		/// <summary>
		/// Converts a weapon relic to oriental style with special naming
		/// </summary>
		/// <param name="item">The weapon item to convert</param>
		public static void MakeOriental(Item item)
		{
			DDRelicWeapon relic = item as DDRelicWeapon;
			if (relic == null)
			{
				return;
			}

			string sLook = "wakizashi";
			switch (Utility.RandomMinMax(0, 13))
			{
				case 0: sLook = "wakizashi"; break;
				case 1: sLook = "sword"; break;
				case 2: sLook = "katana"; break;
				case 3: sLook = "tanto"; break;
				case 4: sLook = "chokuto"; break;
				case 5: sLook = "tsurugi"; break;
				case 6: sLook = "tachi"; break;
				case 7: sLook = "odachi"; break;
				case 8: sLook = "jokoto"; break;
				case 9: sLook = "koto"; break;
				case 10: sLook = "shinto"; break;
				case 11: sLook = "shinshinto"; break;
				case 12: sLook = "gendaito"; break;
				case 13: sLook = "shinsakuto"; break;
			}

			string OwnerName = Server.Misc.RandomThings.GetRandomOrientalName();
			string OwnerTitle = Server.LootPackEntry.MagicItemAdj("end", true, false, item.ItemID);

			relic.Name = sLook + " of " + OwnerName + " " + OwnerTitle;

			switch (Utility.RandomMinMax(0, 9))
			{
				case 0: relic.ItemID = 0x2851; relic.RelicFlipID1 = 0x2851; relic.RelicFlipID2 = 0x2852; break;
				case 1: relic.ItemID = 0x2853; relic.RelicFlipID1 = 0x2853; relic.RelicFlipID2 = 0x2854; break;
				case 2: relic.ItemID = 0x2855; relic.RelicFlipID1 = 0x2855; relic.RelicFlipID2 = 0x2856; break;
				case 3: relic.ItemID = 0x291C; relic.RelicFlipID1 = 0x291C; relic.RelicFlipID2 = 0x291D; break;
				case 4: relic.ItemID = 0x291E; relic.RelicFlipID1 = 0x291E; relic.RelicFlipID2 = 0x291F; break;
				case 5: relic.ItemID = 0x2A2A; relic.RelicFlipID1 = 0x2A2A; relic.RelicFlipID2 = 0x2A2A; break;
				case 6: relic.ItemID = 0x2A45; relic.RelicFlipID1 = 0x2A45; relic.RelicFlipID2 = 0x2A46; break;
				case 7: relic.ItemID = 0x2A47; relic.RelicFlipID1 = 0x2A47; relic.RelicFlipID2 = 0x2A48; break;
				case 8: relic.ItemID = 0x2A49; relic.RelicFlipID1 = 0x2A49; relic.RelicFlipID2 = 0x2A4A; break;
				case 9: relic.ItemID = 0x2A4B; relic.RelicFlipID1 = 0x2A4B; relic.RelicFlipID2 = 0x2A4C; break;
			}
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the weapon relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(RelicConstants.SERIALIZATION_VERSION);
			writer.Write(RelicFlipID1);
			writer.Write(RelicFlipID2);
		}

		/// <summary>
		/// Deserializes the weapon relic
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

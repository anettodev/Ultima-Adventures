using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Banner/tapestry relic item that can be flipped between two ItemID states.
	/// Supports special rare painting variants and oriental transformations.
	/// </summary>
	public class DDRelicBanner : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x2D6F;
		private const int RANDOM_BANNER_TYPE_MIN = 0;
		private const int RANDOM_BANNER_TYPE_MAX = 9;
		private const int RANDOM_QUALITY_MIN = 0;
		private const int RANDOM_QUALITY_MAX = 19;
		private const int SUPER_RARE_INDEX = 19;
		private const int SUPER_RARE_ITEM_ID = 0x2886;
		private const int SUPER_RARE_WEIGHT = 100;
		private const int SUPER_RARE_VALUE_MIN = 1000;
		private const int SUPER_RARE_VALUE_MAX = 5000;
		private const int WEIGHT_STANDARD = 30;

		#endregion

		#region Fields

		/// <summary>First ItemID for flipping</summary>
		public int RelicFlipID1;

		/// <summary>Second ItemID for flipping</summary>
		public int RelicFlipID2;

		/// <summary>
		/// Structure for banner variant data
		/// </summary>
		private struct BannerVariant
		{
			public int ItemID;
			public int FlipID1;
			public int FlipID2;

			public BannerVariant(int itemID, int flipID1, int flipID2)
			{
				this.ItemID = itemID;
				this.FlipID1 = flipID1;
				this.FlipID2 = flipID2;
			}
		}

		/// <summary>
		/// Array of banner variants
		/// </summary>
		private static readonly BannerVariant[] BannerVariants = new BannerVariant[]
		{
			new BannerVariant(0x2D6F, 0x2D6F, 0x2D70),
			new BannerVariant(0x2D71, 0x2D71, 0x2D72),
			new BannerVariant(0x42C4, 0x42C4, 0x42C4),
			new BannerVariant(0x42C9, 0x42C9, 0x42CA),
			new BannerVariant(0x42CB, 0x42CB, 0x42CC),
			new BannerVariant(0x42CD, 0x42CD, 0x42CE),
			new BannerVariant(0x2D6F, 0x2D6F, 0x2D70),
			new BannerVariant(0x2D71, 0x2D71, 0x2D72),
			new BannerVariant(0x42C4, 0x42C4, 0x42C4),
			new BannerVariant(0x49A1, 0x49A1, 0x49B9)
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
		/// Creates a new banner relic with random type and quality
		/// </summary>
		[Constructable]
		public DDRelicBanner() : base(BASE_ITEM_ID)
		{
			Weight = WEIGHT_STANDARD;
			Hue = Server.Misc.RandomThings.GetRandomColor(0);
			int superRare = 0;

			int qualityIndex = Utility.RandomMinMax(RANDOM_QUALITY_MIN, RANDOM_QUALITY_MAX);
			string quality;

			if (qualityIndex == SUPER_RARE_INDEX)
			{
				superRare = 1;
				quality = ""; // Will be set below
			}
			else
			{
				quality = RelicHelper.GetRandomQualityDescriptor();
			}

			int bannerType = Utility.RandomMinMax(RANDOM_BANNER_TYPE_MIN, RANDOM_BANNER_TYPE_MAX);
			BannerVariant banner = BannerVariants[bannerType];

			ItemID = banner.ItemID;
			RelicFlipID1 = banner.FlipID1;
			RelicFlipID2 = banner.FlipID2;

			Name = quality + " tapeçaria";

			if (superRare > 0)
			{
				ItemID = SUPER_RARE_ITEM_ID;
				RelicFlipID1 = SUPER_RARE_ITEM_ID;
				RelicFlipID2 = 0x2887;
				Name = "uma pintura de " + Server.Misc.RandomThings.GetRandomScenePainting();
				Hue = 0;
				Weight = SUPER_RARE_WEIGHT;
				RelicGoldValue = Utility.RandomMinMax(SUPER_RARE_VALUE_MIN, SUPER_RARE_VALUE_MAX);
			}
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicBanner(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to flip banner or show identification message
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
		/// Converts a banner relic to oriental style with special naming
		/// </summary>
		/// <param name="item">The banner item to convert</param>
		public static void MakeOriental(Item item)
		{
			DDRelicBanner relic = item as DDRelicBanner;
			if (relic == null)
			{
				return;
			}

			string quality = RelicHelper.GetRandomQualityDescriptor();
			string ownerName = Server.Misc.RandomThings.GetRandomOrientalName();
			string ownerTitle = Server.LootPackEntry.MagicItemAdj("end", true, false, item.ItemID);
			string ownerNation = Server.Misc.RandomThings.GetRandomOrientalNation();

			if (Utility.RandomMinMax(1, 3) > 1)
			{
				relic.Name = quality + " pintura de " + ownerName + " " + ownerTitle;
			}
			else
			{
				relic.Hue = 0;
				string decorative = "decorativa";
				switch (Utility.RandomMinMax(0, 5))
				{
					case 0: decorative = "decorativa"; break;
					case 1: decorative = "cerimonial"; break;
					case 2: decorative = "ornamental"; break;
					case 3:
					case 4:
					case 5: decorative = Server.Misc.RandomThings.GetRandomOrientalNation(); break;
				}

				string world = "terra";
				switch (Utility.RandomMinMax(0, 6))
				{
					case 0: world = "terra"; break;
					case 1: world = "mundo"; break;
					case 2: world = "ilha"; break;
					case 3: world = "península"; break;
					case 4: world = "continente"; break;
					case 5: world = "mar"; break;
					case 6: world = "oceano"; break;
				}

				string scene = "uma batalha " + ownerNation;
				switch (Utility.RandomMinMax(0, 10))
				{
					case 0: scene = "uma batalha " + ownerNation; break;
					case 1: scene = "um castelo " + ownerNation; break;
					case 2: scene = "um mosteiro " + ownerNation; break;
					case 3: scene = "um jardim " + ownerNation; break;
					case 4: scene = "a paisagem de " + ownerName; break;
					case 5: scene = "o mar de " + ownerName; break;
					case 6: scene = "uma cidade " + ownerNation; break;
					case 7: scene = "uma vila " + ownerNation; break;
					case 8: scene = "uma casa " + ownerNation; break;
					case 9: scene = "o palácio de " + ownerName; break;
					case 10: scene = "uma pagode " + ownerNation; break;
				}

				string saying = Server.Misc.RandomThings.GetRandomJobTitle(0);
				switch (Utility.RandomMinMax(0, 3))
				{
					case 0: saying = Server.Misc.RandomThings.GetRandomJobTitle(0); break;
					case 1: saying = Server.Misc.RandomThings.GetRandomColorName(0) + " " + Server.Misc.RandomThings.GetRandomJobTitle(0); break;
					case 2: saying = Server.Misc.RandomThings.GetRandomThing(0); break;
					case 3: saying = Server.Misc.RandomThings.GetRandomColorName(0) + " " + Server.Misc.RandomThings.GetRandomThing(0); break;
				}

				switch (Utility.RandomMinMax(0, 10))
				{
					case 0: relic.Weight = 20.0; relic.ItemID = 0x2D73; relic.RelicFlipID1 = 0x2D73; relic.RelicFlipID2 = 0x2D74; relic.Name = quality + " mapa da " + ownerNation + " " + world + " de " + ownerName; break;
					case 1: relic.Weight = 20.0; relic.ItemID = 0x2D73; relic.RelicFlipID1 = 0x2D73; relic.RelicFlipID2 = 0x2D74; relic.Name = quality + " mapa de uma " + world + " durante a Dinastia " + Server.Misc.RandomThings.GetRandomColorName(0); break;
					case 2: relic.Weight = 5.0; relic.ItemID = 0x2409; relic.RelicFlipID1 = 0x2409; relic.RelicFlipID2 = 0x240A; relic.Name = quality + ", " + decorative + " leque"; break;
					case 3: relic.Weight = 5.0; relic.ItemID = 0x240B; relic.RelicFlipID1 = 0x240B; relic.RelicFlipID2 = 0x240C; relic.Name = quality + " conjunto de leques " + decorative; break;
					case 4: relic.Weight = 10.0; relic.ItemID = 0x240D; relic.RelicFlipID1 = 0x240D; relic.RelicFlipID2 = 0x240E; relic.Name = quality + " pintura de " + scene; break;
					case 5: relic.Weight = 10.0; relic.ItemID = 0x240F; relic.RelicFlipID1 = 0x240F; relic.RelicFlipID2 = 0x2410; relic.Name = quality + " pintura de " + scene; break;
					case 6: relic.Weight = 10.0; relic.ItemID = 0x2411; relic.RelicFlipID1 = 0x2411; relic.RelicFlipID2 = 0x2412; relic.Name = quality + " pintura de " + scene; break;
					case 7: relic.Weight = 10.0; relic.ItemID = 0x2413; relic.RelicFlipID1 = 0x2413; relic.RelicFlipID2 = 0x2414; relic.Name = quality + " pintura de " + scene; break;
					case 8: relic.Weight = 50.0; relic.ItemID = 0x2886; relic.RelicFlipID1 = 0x2886; relic.RelicFlipID2 = 0x2887; relic.Name = quality + " pintura de " + scene; break;
					case 9: relic.Weight = 15.0; relic.ItemID = 0x2415; relic.RelicFlipID1 = 0x2415; relic.RelicFlipID2 = 0x2416; relic.Name = "uma pintura de símbolos " + ownerNation + " para o " + saying; break;
					case 10: relic.Weight = 15.0; relic.ItemID = 0x2417; relic.RelicFlipID1 = 0x2417; relic.RelicFlipID2 = 0x2418; relic.Name = "uma pintura de símbolos " + ownerNation + " para o " + saying; break;
				}
			}
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the banner relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(RelicConstants.SERIALIZATION_VERSION);
			writer.Write(RelicFlipID1);
			writer.Write(RelicFlipID2);
		}

		/// <summary>
		/// Deserializes the banner relic
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

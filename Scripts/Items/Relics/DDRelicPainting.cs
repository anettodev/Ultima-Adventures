using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Painting relic item that can be flipped between two ItemID states.
	/// Supports 81 different painting variants with random scene generation.
	/// </summary>
	public class DDRelicPainting : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x3E20;
		private const int RANDOM_PAINTING_TYPE_MIN = 0;
		private const int RANDOM_PAINTING_TYPE_MAX = 80;
		private const int RANDOM_PAINTING_SCENE_MIN = 0;
		private const int RANDOM_PAINTING_SCENE_MAX = 7;
		private const int RANDOM_FLIP_CHANCE_MIN = 1;
		private const int RANDOM_FLIP_CHANCE_MAX = 2;
		private const int WEIGHT_STANDARD = 10;
		private const int HUE_RANGE_MIN_1 = 0x5377;
		private const int HUE_RANGE_MAX_1 = 0x5390;
		private const string MUCK_SUFFIX = " (covered in muck)";
		private const string MSG_MUST_BE_IN_PACK = "Isto deve estar em sua mochila para virar.";
		private const string MSG_CLEAR_MUCK = "Você limpa a lama da pintura.";

		#endregion

		#region Fields

		/// <summary>
		/// Structure for painting variant data with flip pairs
		/// </summary>
		private struct PaintingVariant
		{
			public int ItemID1;
			public int ItemID2;

			public PaintingVariant(int itemID1, int itemID2)
			{
				this.ItemID1 = itemID1;
				this.ItemID2 = itemID2;
			}
		}

		/// <summary>
		/// Array of painting variants (81 total)
		/// Each variant has two ItemIDs that can be flipped between
		/// </summary>
		private static readonly PaintingVariant[] PaintingVariants = new PaintingVariant[]
		{
			new PaintingVariant(0x3E20, 0x3E21),
			new PaintingVariant(0x3E7, 0xC2C),
			new PaintingVariant(0x3E8, 0xEA0),
			new PaintingVariant(0x2A5D, 0x2A61),
			new PaintingVariant(0x3EA, 0xEA7),
			new PaintingVariant(0x3EB, 0xEA6),
			new PaintingVariant(0x3EC, 0xEA5),
			new PaintingVariant(0x3ED, 0xE55),
			new PaintingVariant(0xDDF, 0xE9F),
			new PaintingVariant(0xEA3, 0xEA4),
			new PaintingVariant(0xEA1, 0xEA2),
			new PaintingVariant(0x2A65, 0x2A68),
			new PaintingVariant(0x3308, 0x3E0C),
			new PaintingVariant(0x3309, 0x3E0D),
			new PaintingVariant(0x330A, 0x3E0E),
			new PaintingVariant(0x330B, 0x3E0F),
			new PaintingVariant(0x330C, 0x3E10),
			new PaintingVariant(0x330D, 0x3E11),
			new PaintingVariant(0x330E, 0x3E12),
			new PaintingVariant(0x330F, 0x3E13),
			new PaintingVariant(0x3310, 0x3E14),
			new PaintingVariant(0x3311, 0x3E15),
			new PaintingVariant(0x3312, 0x3E16),
			new PaintingVariant(0x3313, 0x3E17),
			new PaintingVariant(0x3314, 0x3E18),
			new PaintingVariant(0x3315, 0x3E19),
			new PaintingVariant(0x3316, 0x3E1A),
			new PaintingVariant(0x3317, 0x3E1B),
			new PaintingVariant(0x3318, 0x3E1C),
			new PaintingVariant(0x3319, 0x3E1D),
			new PaintingVariant(0x331A, 0x3E1E),
			new PaintingVariant(0x331B, 0x3E1F),
			new PaintingVariant(0x331C, 0x3E22),
			new PaintingVariant(0x331D, 0x3E23),
			new PaintingVariant(0x331E, 0x3E24),
			new PaintingVariant(0x331F, 0x3E25),
			new PaintingVariant(0x3320, 0x3E26),
			new PaintingVariant(0x3321, 0x3DEC),
			new PaintingVariant(0x3322, 0x3DED),
			new PaintingVariant(0x3323, 0x3DEE),
			new PaintingVariant(0x3324, 0x3DEF),
			new PaintingVariant(0x3325, 0x3DF0),
			new PaintingVariant(0x3326, 0x3DF1),
			new PaintingVariant(0x3327, 0x3DF2),
			new PaintingVariant(0x3328, 0x3DF3),
			new PaintingVariant(0x3329, 0x3DF4),
			new PaintingVariant(0x332A, 0x3DF5),
			new PaintingVariant(0x332B, 0x3DF6),
			new PaintingVariant(0x332C, 0x3DF7),
			new PaintingVariant(0x332D, 0x3DF8),
			new PaintingVariant(0x332E, 0x3DF9),
			new PaintingVariant(0x332F, 0x3DFA),
			new PaintingVariant(21291, 21292),
			new PaintingVariant(21293, 21294),
			new PaintingVariant(21295, 21296),
			new PaintingVariant(21297, 21298),
			new PaintingVariant(21299, 21300),
			new PaintingVariant(21301, 21302),
			new PaintingVariant(21303, 21304),
			new PaintingVariant(21305, 21306),
			new PaintingVariant(21307, 21308),
			new PaintingVariant(21309, 21310),
			new PaintingVariant(21311, 21312),
			new PaintingVariant(21313, 21314),
			new PaintingVariant(21315, 21316),
			new PaintingVariant(21317, 21318),
			new PaintingVariant(21319, 21320),
			new PaintingVariant(21321, 21322),
			new PaintingVariant(21367, 21368),
			new PaintingVariant(21369, 21370),
			new PaintingVariant(21371, 21372),
			new PaintingVariant(21373, 21374),
			new PaintingVariant(21375, 21376),
			new PaintingVariant(21377, 21378),
			new PaintingVariant(21379, 21380),
			new PaintingVariant(21381, 21382),
			new PaintingVariant(21383, 21384),
			new PaintingVariant(21385, 21386),
			new PaintingVariant(21387, 21388),
			new PaintingVariant(21389, 21390),
			new PaintingVariant(21391, 21392)
		};

		/// <summary>Special hue values for certain painting ranges</summary>
		private static readonly int[] SPECIAL_HUES = new[]
		{
			0xABE, 0x4A7, 0x747, 0x96C, 0x7DA, 0x415, 0x908, 0x712, 0x1CD, 0x9C2, 0x843, 0x750, 0xA94, 0x973, 0xA3A
		};

		/// <summary>Additional painting flip pairs (for special cases in OnDoubleClick)</summary>
		private static readonly PaintingVariant[] AdditionalFlipPairs = new PaintingVariant[]
		{
			new PaintingVariant(0x52FE, 0x52FF),
			new PaintingVariant(0x52B3, 0x52B4),
			new PaintingVariant(0x53A6, 0x53A7),
			new PaintingVariant(0x49A0, 0x49B4),
			new PaintingVariant(0x49A2, 0x49BF),
			new PaintingVariant(0x49A3, 0x49C0),
			new PaintingVariant(0x49A7, 0x49BA),
			new PaintingVariant(0x49A8, 0x49B5),
			new PaintingVariant(0x49B2, 0x49BE),
			new PaintingVariant(0x49B3, 0x49BB),
			new PaintingVariant(0x49A1, 0x49B9)
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new painting relic with random type and scene
		/// </summary>
		[Constructable]
		public DDRelicPainting() : base(BASE_ITEM_ID)
		{
			Weight = WEIGHT_STANDARD;

			if (RelicGoldValue < 1)
			{
				RelicGoldValue = Server.Misc.RelicItems.RelicValue();

				string quality = RelicHelper.GetRandomQualityDescriptor();

				int paintingType = Utility.RandomMinMax(RANDOM_PAINTING_TYPE_MIN, RANDOM_PAINTING_TYPE_MAX);
				PaintingVariant painting = PaintingVariants[paintingType];

				// 50% chance to use second ItemID
				if (Utility.RandomMinMax(RANDOM_FLIP_CHANCE_MIN, RANDOM_FLIP_CHANCE_MAX) == 1)
				{
					ItemID = painting.ItemID2;
				}
				else
				{
					ItemID = painting.ItemID1;
				}

				// Apply special hues for certain ItemID ranges
				if (ItemID >= HUE_RANGE_MIN_1 && ItemID <= HUE_RANGE_MAX_1)
				{
					Hue = Utility.RandomList(SPECIAL_HUES);
				}

				string paintingScene = GetRandomPaintingScene();

				Name = quality + " pintura de " + paintingScene;
			}
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicPainting(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to flip painting, clear muck, or show message
		/// </summary>
		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendMessage(MSG_MUST_BE_IN_PACK);
			}
			else if (Name.Contains(MUCK_SUFFIX))
			{
				from.SendMessage(MSG_CLEAR_MUCK);
				Hue = 0;
				if (ItemID >= HUE_RANGE_MIN_1 && ItemID <= HUE_RANGE_MAX_1)
				{
					Hue = Utility.RandomList(SPECIAL_HUES);
				}
				Name = Name.Replace(MUCK_SUFFIX, "");
			}
			else
			{
				FlipPainting();
			}
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Gets a random painting scene description
		/// </summary>
		/// <returns>Random scene description</returns>
		private string GetRandomPaintingScene()
		{
			int sceneType = Utility.RandomMinMax(RANDOM_PAINTING_SCENE_MIN, RANDOM_PAINTING_SCENE_MAX);

			switch (sceneType)
			{
				case 0:
					return Server.Misc.RandomThings.GetRandomScenePainting();
				case 1:
					return Server.Misc.RandomThings.GetRandomCity();
				case 2:
					return "o " + Server.Misc.RandomThings.RandomMagicalItem();
				case 3:
					return Server.Misc.RandomThings.GetRandomShipName("", 0);
				case 4:
					return Server.Misc.RandomThings.GetRandomGirlName() + " a " + Server.Misc.RandomThings.GetBoyGirlJob(1);
				case 5:
					return Server.Misc.RandomThings.GetRandomBoyName() + " o " + Server.Misc.RandomThings.GetBoyGirlJob(0);
				case 6:
					return Server.Misc.RandomThings.GetRandomGirlName() + " a " + Server.Misc.RandomThings.GetRandomGirlNoble();
				case 7:
					return Server.Misc.RandomThings.GetRandomBoyName() + " o " + Server.Misc.RandomThings.GetRandomBoyNoble();
				default:
					return Server.Misc.RandomThings.GetRandomScenePainting();
			}
		}

		/// <summary>
		/// Flips the painting between its two ItemID states
		/// </summary>
		private void FlipPainting()
		{
			// Check main painting variants
			for (int i = 0; i < PaintingVariants.Length; i++)
			{
				PaintingVariant variant = PaintingVariants[i];
				if (ItemID == variant.ItemID1)
				{
					ItemID = variant.ItemID2;
					return;
				}
				else if (ItemID == variant.ItemID2)
				{
					ItemID = variant.ItemID1;
					return;
				}
			}

			// Check additional flip pairs
			for (int i = 0; i < AdditionalFlipPairs.Length; i++)
			{
				PaintingVariant variant = AdditionalFlipPairs[i];
				if (ItemID == variant.ItemID1)
				{
					ItemID = variant.ItemID2;
					return;
				}
				else if (ItemID == variant.ItemID2)
				{
					ItemID = variant.ItemID1;
					return;
				}
			}
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the painting relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the painting relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}

using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Statue relic item that can be flipped between two ItemID states.
	/// Supports 38 different statue types with material variations and special weights/values.
	/// </summary>
	public class DDRelicStatue : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x1224;
		private const int RANDOM_MATERIAL_TYPE_MIN = 0;
		private const int RANDOM_MATERIAL_TYPE_MAX = 7;
		private const int RANDOM_MATERIAL_SPECIAL_MIN = 0;
		private const int RANDOM_MATERIAL_SPECIAL_MAX = 50;
		private const int RANDOM_STATUE_TYPE_MIN = 0;
		private const int RANDOM_STATUE_TYPE_MAX = 37;
		private const int WEIGHT_STANDARD = 60;
		private const int WEIGHT_HEAVY = 100;
		private const int WEIGHT_VERY_HEAVY = 150;
		private const int VALUE_MULTIPLIER = 2;
		private const int VALUE_RANGE_MIN_1 = 100;
		private const int VALUE_RANGE_MAX_1 = 400;
		private const int VALUE_RANGE_MIN_2 = 150;
		private const int VALUE_RANGE_MAX_2 = 500;

		#endregion

		#region Fields

		/// <summary>First ItemID for flipping</summary>
		public int RelicFlipID1;

		/// <summary>Second ItemID for flipping</summary>
		public int RelicFlipID2;

		/// <summary>Description text for the statue material</summary>
		public string RelicDescription;

		/// <summary>
		/// Structure for statue variant data
		/// </summary>
		private struct StatueVariant
		{
			public int ItemID;
			public int FlipID1;
			public int FlipID2;
			public string NameSuffix;
			public int Weight;
			public int ValueMin;
			public int ValueMax;
			public bool HasSpecialMaterial;

			public StatueVariant(int itemID, int flipID1, int flipID2, string nameSuffix, int weight, int valueMin, int valueMax, bool hasSpecialMaterial)
			{
				this.ItemID = itemID;
				this.FlipID1 = flipID1;
				this.FlipID2 = flipID2;
				this.NameSuffix = nameSuffix;
				this.Weight = weight;
				this.ValueMin = valueMin;
				this.ValueMax = valueMax;
				this.HasSpecialMaterial = hasSpecialMaterial;
			}
		}

		/// <summary>
		/// Array of statue variants (38 total)
		/// </summary>
		private static readonly StatueVariant[] StatueVariants = new StatueVariant[]
		{
			new StatueVariant(0x1224, 0x1224, 0x139A, "estátua de uma mulher", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x1225, 0x1225, 0x1225, "estátua de um homem", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x1226, 0x1226, 0x139B, "estátua de um anjo", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x1226, 0x1226, 0x139B, "estátua de um demônio", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x1227, 0x1227, 0x139C, "estátua de um homem", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x1228, 0x1228, 0x139D, "estátua de um pássaro", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x1228, 0x1228, 0x139D, "estátua de um pégaso", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x12CA, 0x12CA, 0x12CB, "busto de um homem", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x207C, 0x207C, 0x207C, "estátua de um anjo", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x42BB, 0x42BB, 0x42BB, "estátua de uma gárgula", WEIGHT_HEAVY, VALUE_RANGE_MIN_1, VALUE_RANGE_MAX_1, false),
			new StatueVariant(0x42BB, 0x42BB, 0x42BB, "estátua de um demônio", WEIGHT_HEAVY, VALUE_RANGE_MIN_1, VALUE_RANGE_MAX_1, false),
			new StatueVariant(0x42C2, 0x42C2, 0x42C2, "estátua de uma criatura estranha", WEIGHT_VERY_HEAVY, VALUE_RANGE_MIN_2, VALUE_RANGE_MAX_2, false),
			new StatueVariant(0x40BC, 0x40BC, 0x40BC, "estátua de uma medusa", WEIGHT_VERY_HEAVY, VALUE_RANGE_MIN_2, VALUE_RANGE_MAX_2, false),
			new StatueVariant(0x42C5, 0x42C5, 0x42C5, "estátua de um demônio", WEIGHT_VERY_HEAVY, VALUE_RANGE_MIN_2, VALUE_RANGE_MAX_2, false),
			new StatueVariant(0x42BC, 0x42BC, 0x42BC, "busto de um demônio", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x48A8, 0x48A8, 0x48A9, "estátua de cabeça de dragão", WEIGHT_VERY_HEAVY, VALUE_RANGE_MIN_2, VALUE_RANGE_MAX_2, false),
			new StatueVariant(0x4578, 0x4578, 0x4579, "estátua de um cavalo-marinho", WEIGHT_VERY_HEAVY, VALUE_RANGE_MIN_2, VALUE_RANGE_MAX_2, false),
			new StatueVariant(0x457A, 0x457A, 0x457B, "estátua de uma sereia", WEIGHT_VERY_HEAVY, VALUE_RANGE_MIN_2, VALUE_RANGE_MAX_2, false),
			new StatueVariant(0x457C, 0x457C, 0x457D, "estátua de um grifo", WEIGHT_VERY_HEAVY, VALUE_RANGE_MIN_2, VALUE_RANGE_MAX_2, false),
			new StatueVariant(0x42C0, 0x42C0, 0x42C1, "estátua de um demônio", WEIGHT_HEAVY, VALUE_RANGE_MIN_1, VALUE_RANGE_MAX_1, false),
			new StatueVariant(0x3F19, 0x3F19, 0x3F1A, "estátua de um deus", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x3F1B, 0x3F1B, 0x3F1C, "estátua de um cavaleiro", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x4688, 0x4688, 0x4689, "estátua de um gato", WEIGHT_STANDARD, 0, 0, true),
			new StatueVariant(0x3142, 0x3143, 0x3142, "estátua de um leão", WEIGHT_HEAVY, VALUE_RANGE_MIN_1, VALUE_RANGE_MAX_1, false),
			new StatueVariant(0x3182, 0x3182, 0x3182, "estátua de um leão", WEIGHT_HEAVY, VALUE_RANGE_MIN_1, VALUE_RANGE_MAX_1, false),
			new StatueVariant(0x31C1, 0x31C1, 0x31C2, "estátua de um pégaso", WEIGHT_VERY_HEAVY, VALUE_RANGE_MIN_2, VALUE_RANGE_MAX_2, false),
			new StatueVariant(0x31C7, 0x31C8, 0x31C7, "estátua de um cavaleiro", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x31CB, 0x31CB, 0x31CC, "estátua de um explorador", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x31CD, 0x31CD, 0x31CE, "estátua de um mago", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x31CF, 0x31CF, 0x31D0, "estátua de um lanceiro", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x31D1, 0x31D1, 0x31D2, "estátua de um sacerdote", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x31D3, 0x31D3, 0x31D4, "estátua de um rei", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x31FC, 0x31FC, 0x31FD, "estátua de um deus", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x31FE, 0x31FE, 0x31FF, "estátua de um guarda", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x320B, 0x320B, 0x3219, "estátua de um elfo", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x320C, 0x320C, 0x3212, "estátua de um elfo", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x321F, 0x321F, 0x3225, "estátua de um elfo", WEIGHT_STANDARD, 0, 0, false),
			new StatueVariant(0x322B, 0x322B, 0x3235, "estátua de um elfo", WEIGHT_STANDARD, 0, 0, false)
		};

		/// <summary>Material descriptions</summary>
		private static readonly string[] MATERIAL_DESCRIPTIONS = new[]
		{
			"Feito de pedra colorida",
			"Feito de pedra colorida",
			"Feito de pedra colorida",
			"Feito de pedra colorida",
			"Feito de pedra colorida",
			"Feito de pedra colorida",
			"Feito de pedra",
			"Feito de pedra"
		};

		/// <summary>Special material descriptions with hues</summary>
		private struct MaterialData
		{
			public string Description;
			public int Hue;
			public bool MultiplyValue;
			public bool MultiplyWeight;

			public MaterialData(string description, int hue, bool multiplyValue, bool multiplyWeight)
			{
				this.Description = description;
				this.Hue = hue;
				this.MultiplyValue = multiplyValue;
				this.MultiplyWeight = multiplyWeight;
			}
		}

		/// <summary>Special material data</summary>
		private static readonly MaterialData[] SPECIAL_MATERIALS = new MaterialData[]
		{
			new MaterialData("Feito de bronze", 0xB9A, false, false),
			new MaterialData("Feito de jade", 0xB93, false, false),
			new MaterialData("Feito de granito", 0xB8E, false, false),
			new MaterialData("Feito de mármore", 0xB8B, false, false),
			new MaterialData("Feito de cobre", 0x972, false, false),
			new MaterialData("Feito de gelo", 0x480, false, false),
			new MaterialData("Feito de prata", 0x835, false, false),
			new MaterialData("Feito de ametista", 0x492, false, false),
			new MaterialData("Feito de esmeralda", 0x5B4, false, false),
			new MaterialData("", 0, false, false), // Case 9 - skip
			new MaterialData("Feito de granada", 0x48F, false, false),
			new MaterialData("Feito de ônix", 0x497, false, false),
			new MaterialData("Feito de quartzo", 0x4AC, false, false),
			new MaterialData("Feito de rubi", 0x5B5, false, false),
			new MaterialData("Feito de safira", 0x5B6, false, false),
			new MaterialData("Feito de espinélio", 0x48B, false, false),
			new MaterialData("Feito de rubi estrela", 0x48E, false, false),
			new MaterialData("Feito de topázio", 0x488, false, false),
			new MaterialData("Feito de marfim", 0x47E, false, false),
			new MaterialData("Feito de ouro maciço", 0x4AC, true, true)
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

		/// <summary>
		/// Gets or sets the statue description
		/// </summary>
		[CommandProperty(AccessLevel.Owner)]
		public string Relic_Describe
		{
			get { return RelicDescription; }
			set { RelicDescription = value; InvalidateProperties(); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new statue relic with random type and material
		/// </summary>
		[Constructable]
		public DDRelicStatue() : base(BASE_ITEM_ID)
		{
			Weight = WEIGHT_STANDARD;

			string materialDescription = GetMaterialDescription();

			string quality = RelicHelper.GetRandomQualityDescriptor();
			string decorative = RelicHelper.GetRandomDecorativeTerm();

			int statueType = Utility.RandomMinMax(RANDOM_STATUE_TYPE_MIN, RANDOM_STATUE_TYPE_MAX);
			StatueVariant statue = StatueVariants[statueType];

			ItemID = statue.ItemID;
			RelicFlipID1 = statue.FlipID1;
			RelicFlipID2 = statue.FlipID2;
			Weight = statue.Weight;

			// Apply special material for cat statue (case 22)
			if (statue.HasSpecialMaterial)
			{
				materialDescription = "Feito de ônix";
				Hue = 0;
			}

			RelicDescription = materialDescription;

			// Apply special value/weight for certain statues
			if (statue.ValueMin > 0 && statue.ValueMax > 0)
			{
				RelicGoldValue = Utility.RandomMinMax(statue.ValueMin, statue.ValueMax);
			}

			Name = quality + ", " + decorative + " " + statue.NameSuffix;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicStatue(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds description information to the item properties
		/// </summary>
		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
			list.Add(1049644, RelicDescription);
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to flip statue or show identification message
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
		/// Gets a random material description and applies hue
		/// </summary>
		/// <returns>Material description string</returns>
		private string GetMaterialDescription()
		{
			int materialType = Utility.RandomMinMax(RANDOM_MATERIAL_TYPE_MIN, RANDOM_MATERIAL_TYPE_MAX);
			string material = MATERIAL_DESCRIPTIONS[materialType];

			if (materialType < 6)
			{
				Hue = Server.Misc.RandomThings.GetRandomColor(0);
			}
			else
			{
				Hue = 0;
			}

			// Check for special materials (0-50 range)
			int specialMaterial = Utility.RandomMinMax(RANDOM_MATERIAL_SPECIAL_MIN, RANDOM_MATERIAL_SPECIAL_MAX);
			if (specialMaterial < SPECIAL_MATERIALS.Length && !string.IsNullOrEmpty(SPECIAL_MATERIALS[specialMaterial].Description))
			{
				MaterialData materialData = SPECIAL_MATERIALS[specialMaterial];
				material = materialData.Description;
				Hue = materialData.Hue;

				if (materialData.MultiplyValue)
				{
					RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER;
				}

				if (materialData.MultiplyWeight)
				{
					Weight = Weight * VALUE_MULTIPLIER;
				}
			}

			return material;
		}

		/// <summary>
		/// Converts a statue relic to oriental style with special naming
		/// </summary>
		/// <param name="item">The statue item to convert</param>
		public static void MakeOriental(Item item)
		{
			DDRelicStatue relic = item as DDRelicStatue;
			if (relic == null)
			{
				return;
			}

			string quality = RelicHelper.GetRandomQualityDescriptor();
			string decorative = RelicHelper.GetRandomDecorativeTerm(true);
			string ownerName = Server.Misc.RandomThings.GetRandomOrientalName();
			string ownerTitle = Server.LootPackEntry.MagicItemAdj("end", true, false, item.ItemID);

			int orientalType = Utility.RandomMinMax(0, 4);

			switch (orientalType)
			{
				case 0:
					relic.ItemID = 0x1947;
					relic.RelicFlipID1 = 0x1947;
					relic.RelicFlipID2 = 0x1948;
					relic.Name = quality + ", " + decorative + " estátua de Buda";
					break;
				case 1:
					relic.ItemID = 0x2419;
					relic.RelicFlipID1 = 0x2419;
					relic.RelicFlipID2 = 0x2419;
					relic.Name = quality + ", " + decorative + " escultura";
					break;
				case 2:
					relic.ItemID = 0x241A;
					relic.RelicFlipID1 = 0x241A;
					relic.RelicFlipID2 = 0x241A;
					relic.Name = quality + ", " + decorative + " escultura";
					break;
				case 3:
					relic.ItemID = 0x241B;
					relic.RelicFlipID1 = 0x241B;
					relic.RelicFlipID2 = 0x241B;
					relic.Name = quality + ", " + decorative + " escultura";
					break;
				case 4:
					relic.ItemID = 0x2848;
					relic.RelicFlipID1 = 0x2848;
					relic.RelicFlipID2 = 0x2849;
					relic.Name = quality + " escultura de " + ownerName + " " + ownerTitle;
					break;
			}
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the statue relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(RelicConstants.SERIALIZATION_VERSION);
			writer.Write(RelicFlipID1);
			writer.Write(RelicFlipID2);
			writer.Write(RelicDescription);
		}

		/// <summary>
		/// Deserializes the statue relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			RelicFlipID1 = reader.ReadInt();
			RelicFlipID2 = reader.ReadInt();
			RelicDescription = reader.ReadString();
		}

		#endregion
	}
}

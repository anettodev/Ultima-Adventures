using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	/// <summary>
	/// Grave relic item that can be flipped between two ItemID states.
	/// Supports gravestones, tombstones, and various grave markers with material variations.
	/// </summary>
	public class DDRelicGrave : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0xED4;
		private const int RANDOM_GRAVE_TYPE_MIN = 0;
		private const int RANDOM_GRAVE_TYPE_MAX = 30;
		private const int RANDOM_BODY_TYPE_MIN = 0;
		private const int RANDOM_BODY_TYPE_MAX = 2;
		private const int RANDOM_CHAIN_TYPE_MIN = 0;
		private const int RANDOM_CHAIN_TYPE_MAX = 3;
		private const int RANDOM_GRAVE_STONE_MIN = 0;
		private const int RANDOM_GRAVE_STONE_MAX = 1;
		private const int RANDOM_CARVING_MIN = 0;
		private const int RANDOM_CARVING_MAX = 4;
		private const int RANDOM_MATERIAL_MIN = 0;
		private const int RANDOM_MATERIAL_MAX = 50;
		private const int WEIGHT_STANDARD = 30;
		private const int VALUE_MULTIPLIER = 2;
		private const int VALUE_MULTIPLIER_GOLD = 4;
		private const int WEIGHT_MULTIPLIER_GOLD = 2;
		private const string DEFAULT_NAME = "gravestone";

		#endregion

		#region Fields

		/// <summary>First ItemID for flipping</summary>
		public int RelicFlipID1;

		/// <summary>Second ItemID for flipping</summary>
		public int RelicFlipID2;

		/// <summary>Description text for the grave</summary>
		public string RelicDescription;

		/// <summary>
		/// Structure for grave variant data
		/// </summary>
		private struct GraveVariant
		{
			public int ItemID;
			public int FlipID1;
			public int FlipID2;
			public string Name;
			public string DescriptionPrefix;

			public GraveVariant(int itemID, int flipID1, int flipID2, string name, string descriptionPrefix)
			{
				this.ItemID = itemID;
				this.FlipID1 = flipID1;
				this.FlipID2 = flipID2;
				this.Name = name;
				this.DescriptionPrefix = descriptionPrefix;
			}
		}

		/// <summary>
		/// Array of standard grave variants (cases 0-19)
		/// </summary>
		private static readonly GraveVariant[] StandardGraveVariants = new GraveVariant[]
		{
			new GraveVariant(0xED4, 0xED5, 0xED4, DEFAULT_NAME, ""),
			new GraveVariant(0xED7, 0xED8, 0xED7, DEFAULT_NAME, ""),
			new GraveVariant(0xEDB, 0xEDC, 0xEDB, DEFAULT_NAME, ""),
			new GraveVariant(0xEDD, 0xEDF, 0xEDD, DEFAULT_NAME, ""),
			new GraveVariant(0x1165, 0x1166, 0x1165, DEFAULT_NAME, ""),
			new GraveVariant(0x1167, 0x1168, 0x1167, DEFAULT_NAME, ""),
			new GraveVariant(0x1169, 0x116A, 0x1169, DEFAULT_NAME, ""),
			new GraveVariant(0x116B, 0x116C, 0x116B, DEFAULT_NAME, ""),
			new GraveVariant(0x116D, 0x116E, 0x116D, DEFAULT_NAME, ""),
			new GraveVariant(0x116F, 0x1170, 0x116F, DEFAULT_NAME, ""),
			new GraveVariant(0x1171, 0x1172, 0x1171, DEFAULT_NAME, ""),
			new GraveVariant(0x1173, 0x1174, 0x1173, DEFAULT_NAME, ""),
			new GraveVariant(0x1175, 0x1176, 0x1175, DEFAULT_NAME, ""),
			new GraveVariant(0x1177, 0x1178, 0x1177, DEFAULT_NAME, ""),
			new GraveVariant(0x1179, 0x117A, 0x1179, DEFAULT_NAME, ""),
			new GraveVariant(0x117B, 0x117C, 0x117B, DEFAULT_NAME, ""),
			new GraveVariant(0x117D, 0x117E, 0x117D, DEFAULT_NAME, ""),
			new GraveVariant(0x117F, 0x1180, 0x117F, DEFAULT_NAME, ""),
			new GraveVariant(0x1181, 0x1182, 0x1181, DEFAULT_NAME, ""),
			new GraveVariant(0x1183, 0x1184, 0x1183, DEFAULT_NAME, "")
		};

		/// <summary>Body type names</summary>
		private static readonly string[] BODY_TYPES = new[]
		{
			"cadáver", "corpo", "esqueleto"
		};

		/// <summary>Chain/binding type names</summary>
		private static readonly string[] CHAIN_TYPES = new[]
		{
			"um acorrentado", "um algemado", "um amarrado", "um manietado"
		};

		/// <summary>Grave stone type names</summary>
		private static readonly string[] GRAVE_STONE_TYPES = new[]
		{
			"lápide", "túmulo"
		};

		/// <summary>Carving text options</summary>
		private static readonly string[] CARVING_TEXTS = new[]
		{
			"Aqui Jaz", "Descanse em Paz", "Nós Lembraremos", "Aqui Descansa", "Enterrado Aqui Está"
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
		/// Gets or sets the grave description
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
		/// Creates a new grave relic with random type and material
		/// </summary>
		[Constructable]
		public DDRelicGrave() : base(BASE_ITEM_ID)
		{
			Weight = WEIGHT_STANDARD;
			Name = DEFAULT_NAME;

			int bodyType = Utility.RandomMinMax(RANDOM_BODY_TYPE_MIN, RANDOM_BODY_TYPE_MAX);
			string body = BODY_TYPES[bodyType];

			int chainType = Utility.RandomMinMax(RANDOM_CHAIN_TYPE_MIN, RANDOM_CHAIN_TYPE_MAX);
			string chain = CHAIN_TYPES[chainType];

			int graveType = Utility.RandomMinMax(RANDOM_GRAVE_TYPE_MIN, RANDOM_GRAVE_TYPE_MAX);

			if (graveType <= 19)
			{
				GraveVariant grave = StandardGraveVariants[graveType];
				ItemID = grave.ItemID;
				RelicFlipID1 = grave.FlipID1;
				RelicFlipID2 = grave.FlipID2;
			}
			else if (graveType == 20)
			{
				ItemID = 0x124B;
				RelicFlipID1 = 0x1249;
				RelicFlipID2 = 0x124B;
				Name = "uma donzela de ferro";
				RelicDescription = "Que Uma Vez Conteve " + ContainerFunctions.GetOwner("property");
			}
			else if (graveType == 21)
			{
				ItemID = 0x1C20;
				RelicFlipID1 = 0x1C21;
				RelicFlipID2 = 0x1C20;
				Name = "um corpo embrulhado";
				RelicDescription = ContainerFunctions.GetOwner("Body");
			}
			else if (graveType == 22)
			{
				ItemID = 0x1D9E;
				RelicFlipID1 = 0x1D9D;
				RelicFlipID2 = 0x1D9E;
				Name = "um espinho sangrento";
				RelicDescription = "Que Matou " + ContainerFunctions.GetOwner("property");
			}
			else
			{
				// Cases 23-30: Chained bodies
				int[] chainedBodyItemIDs = new[] { 0x1A01, 0x1A03, 0x1A05, 0x1A09, 0x1A0B, 0x1A0D, 0x1B7C, 0x1B1D };
				int[] chainedBodyFlipID1s = new[] { 0x1A02, 0x1A04, 0x1A06, 0x1A0A, 0x1A0C, 0x1A0E, 0x1B7F, 0x1B1E };
				int[] chainedBodyFlipID2s = new[] { 0x1A01, 0x1A03, 0x1A05, 0x1A09, 0x1A0B, 0x1A0D, 0x1B7C, 0x1B1D };

				int index = graveType - 23;
				ItemID = chainedBodyItemIDs[index];
				RelicFlipID1 = chainedBodyFlipID1s[index];
				RelicFlipID2 = chainedBodyFlipID2s[index];
				Name = chain + " " + body;
				RelicDescription = ContainerFunctions.GetOwner("Body");
			}

			if (Name == DEFAULT_NAME)
			{
				int graveStoneType = Utility.RandomMinMax(RANDOM_GRAVE_STONE_MIN, RANDOM_GRAVE_STONE_MAX);
				string graveStone = GRAVE_STONE_TYPES[graveStoneType];

				int carvingIndex = Utility.RandomMinMax(RANDOM_CARVING_MIN, RANDOM_CARVING_MAX);
				string carving = CARVING_TEXTS[carvingIndex];

				RelicDescription = carving + " " + ContainerFunctions.GetOwner("property");

				string material = "uma " + graveStone;
				int materialType = Utility.RandomMinMax(RANDOM_MATERIAL_MIN, RANDOM_MATERIAL_MAX);

				switch (materialType)
				{
					case 0: material = "uma lápide de bronze"; Hue = 0xB9A; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 1: material = "uma lápide de jade"; Hue = 0xB93; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 2: material = "uma lápide de granito"; Hue = 0xB8E; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 3: material = "uma lápide de mármore"; Hue = 0xB8B; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 4: material = "uma lápide de cobre"; Hue = 0x972; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 5: material = "uma lápide de prata"; Hue = 0x835; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 7: material = "uma lápide de ametista"; Hue = 0x492; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 8: material = "uma lápide de esmeralda"; Hue = 0x5B4; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 10: material = "uma lápide de granada"; Hue = 0x48F; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 11: material = "uma lápide de ônix"; Hue = 0x497; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 12: material = "uma lápide de quartzo"; Hue = 0x4AC; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 13: material = "uma lápide de rubi"; Hue = 0x5B5; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 14: material = "uma lápide de safira"; Hue = 0x5B6; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 15: material = "uma lápide de espinélio"; Hue = 0x48B; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 16: material = "uma lápide de rubi estrela"; Hue = 0x48E; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 17: material = "uma lápide de topázio"; Hue = 0x488; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 18: material = "uma lápide de marfim"; Hue = 0x47E; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER; break;
					case 19: material = "uma lápide de ouro maciço"; Hue = 0x4AC; RelicGoldValue = RelicGoldValue * VALUE_MULTIPLIER_GOLD; Weight = Weight * WEIGHT_MULTIPLIER_GOLD; break;
					default: material = "uma " + graveStone; break;
				}

				Name = material;
			}
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicGrave(Serial serial) : base(serial)
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
		/// Handles double-click to flip grave or show identification message
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

		#region Serialization

		/// <summary>
		/// Serializes the grave relic
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
		/// Deserializes the grave relic
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

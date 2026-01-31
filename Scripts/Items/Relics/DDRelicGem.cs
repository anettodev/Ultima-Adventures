using System;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	/// <summary>
	/// Gem relic item with random appearance, extended quality descriptors, and provenance information.
	/// </summary>
	public class DDRelicGem : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x3192;
		private const int RANDOM_QUALITY_MIN = 0;
		private const int RANDOM_QUALITY_MAX = 24;
		private const int RANDOM_GEM_TYPE_MIN = 0;
		private const int RANDOM_GEM_TYPE_MAX = 6;
		private const int RANDOM_GIFT_TYPE_MIN = 0;
		private const int RANDOM_GIFT_TYPE_MAX = 7;
		private const int RANDOM_GIFT2_TYPE_MIN = 0;
		private const int RANDOM_GIFT2_TYPE_MAX = 4;
		private const int RANDOM_EYE_TYPE_MIN = 0;
		private const int RANDOM_EYE_TYPE_MAX = 4;
		private const int RANDOM_PROVENANCE_MIN = 1;
		private const int RANDOM_PROVENANCE_MAX = 8;

		#endregion

		#region Fields

		/// <summary>Provenance information for the gem</summary>
		public string RelicCameFrom;

		/// <summary>Item IDs for gem variants</summary>
		private static readonly int[] GEM_ITEM_IDS = new[]
		{
			0xF21, 0xF10, 0xF19, 0xF13, 0xF15, 0xF16, 0xF2D, 0xF25, 0xF26
		};

		/// <summary>Extended quality descriptors for gems (includes gem-specific terms)</summary>
		private static readonly string[] GEM_QUALITY_DESCRIPTORS = new[]
		{
			"um raro", "um bonito", "um belo", "um excelente", "um delicioso",
			"um elegante", "um requintado", "um fino", "um magnífico", "um adorável",
			"um majestoso", "um maravilhoso", "um esplêndido", "um maravilhoso", "um extraordinário",
			"estranho", "estranho", "um único", "incomum", "um brilhante",
			"um claro", "um lustroso", "um radiante", "um brilhante", "um precioso"
		};

		/// <summary>Gem type names</summary>
		private static readonly string[] GEM_TYPES = new[]
		{
			"gema", "joia", "pedra", "gema preciosa", "pedra natal", "cristal", "fragmento"
		};

		/// <summary>Provenance prefix strings (first type)</summary>
		private static readonly string[] PROVENANCE_PREFIXES = new[]
		{
			"Pertenceu a", "Presenteado para", "Roubado de", "Encontrado em", "Perdido em", "Tomado de", "Presenteado de", "Desaparecido de"
		};

		/// <summary>Provenance prefix strings (second type)</summary>
		private static readonly string[] PROVENANCE_PREFIXES2 = new[]
		{
			"Roubado de", "Pertenceu a", "Perdido por", "Tomado de", "Desaparecido de"
		};

		/// <summary>Eye/Crystal type names</summary>
		private static readonly string[] EYE_TYPES = new[]
		{
			"Olho", "Cristal", "Pedra", "Fragmento", "Gema"
		};

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the gem's provenance information
		/// </summary>
		[CommandProperty(AccessLevel.Owner)]
		public string Relic_CameFrom
		{
			get { return RelicCameFrom; }
			set { RelicCameFrom = value; InvalidateProperties(); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new gem relic with random appearance and provenance
		/// </summary>
		[Constructable]
		public DDRelicGem() : base(BASE_ITEM_ID)
		{
			switch (Utility.RandomMinMax(0, 2))
			{
				case 0:
					Hue = Utility.RandomList(0x47E, 0x47F, 0x480, 0x481, 0x482, 0xB93, 0xB94, 0xB95, 0xB96, 0xB83, 0x48D, 0x48E, 0x48F, 0x490, 0x491, 0x492, 0x489, 0x495, 0x496, 0x499);
					break;
				case 1:
					Hue = Utility.RandomMinMax(0x9C5, 0xA54);
					break;
				case 2:
					Hue = Utility.RandomMinMax(0xA5B, 0xA66);
					break;
			}

			ItemID = Utility.RandomList(GEM_ITEM_IDS);
			Weight = RelicConstants.WEIGHT_VERY_LIGHT;
			Light = LightType.Circle150;

			int qualityIndex = Utility.RandomMinMax(RANDOM_QUALITY_MIN, RANDOM_QUALITY_MAX);
			string quality = GEM_QUALITY_DESCRIPTORS[qualityIndex];

			int gemTypeIndex = Utility.RandomMinMax(RANDOM_GEM_TYPE_MIN, RANDOM_GEM_TYPE_MAX);
			string gemType = GEM_TYPES[gemTypeIndex];

			Name = quality + " " + gemType;

			int provenanceType = Utility.RandomMinMax(RANDOM_PROVENANCE_MIN, RANDOM_PROVENANCE_MAX);
			string provenancePrefix;
			string provenancePrefix2;

			switch (provenanceType)
			{
				case 1:
					provenancePrefix = PROVENANCE_PREFIXES[Utility.RandomMinMax(0, PROVENANCE_PREFIXES.Length - 1)];
					RelicCameFrom = provenancePrefix + " o " + Server.Misc.RandomThings.GetRandomKingdomName() + " " + Server.Misc.RandomThings.GetRandomKingdom();
					break;
				case 2:
					provenancePrefix = PROVENANCE_PREFIXES[Utility.RandomMinMax(0, PROVENANCE_PREFIXES.Length - 1)];
					RelicCameFrom = provenancePrefix + " o " + Server.Misc.RandomThings.GetRandomKingdom() + " de " + Server.Misc.RandomThings.GetRandomKingdomName();
					break;
				case 3:
					provenancePrefix2 = PROVENANCE_PREFIXES2[Utility.RandomMinMax(0, PROVENANCE_PREFIXES2.Length - 1)];
					RelicCameFrom = provenancePrefix2 + " o " + Server.Misc.RandomThings.GetRandomNoble() + " de " + Server.Misc.RandomThings.GetRandomKingdomName();
					break;
				case 4:
					provenancePrefix2 = PROVENANCE_PREFIXES2[Utility.RandomMinMax(0, PROVENANCE_PREFIXES2.Length - 1)];
					RelicCameFrom = provenancePrefix2 + " o " + Server.Misc.RandomThings.GetRandomNoble() + " do " + Server.Misc.RandomThings.GetRandomKingdomName() + " " + Server.Misc.RandomThings.GetRandomKingdom();
					break;
				case 5:
					provenancePrefix2 = PROVENANCE_PREFIXES2[Utility.RandomMinMax(0, PROVENANCE_PREFIXES2.Length - 1)];
					RelicCameFrom = provenancePrefix2 + " " + Server.Misc.RandomThings.GetRandomSociety();
					break;
				case 6:
					provenancePrefix2 = PROVENANCE_PREFIXES2[Utility.RandomMinMax(0, PROVENANCE_PREFIXES2.Length - 1)];
					RelicCameFrom = provenancePrefix2 + " o Navio chamado " + Server.Misc.RandomThings.GetRandomShipName("", 0);
					break;
				case 7:
					provenancePrefix2 = PROVENANCE_PREFIXES2[Utility.RandomMinMax(0, PROVENANCE_PREFIXES2.Length - 1)];
					RelicCameFrom = provenancePrefix2 + " " + Server.Misc.RandomThings.GetRandomScenePainting();
					break;
				case 8:
					string eyeType = EYE_TYPES[Utility.RandomMinMax(RANDOM_EYE_TYPE_MIN, RANDOM_EYE_TYPE_MAX)];
					RelicCameFrom = eyeType + " da " + Server.Misc.RandomThings.GetRandomCreature();
					break;
			}
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicGem(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds provenance information to the item properties
		/// </summary>
		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
			list.Add(1049644, RelicCameFrom);
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the gem relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(RelicConstants.SERIALIZATION_VERSION);
			writer.Write(RelicCameFrom);
		}

		/// <summary>
		/// Deserializes the gem relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			RelicCameFrom = reader.ReadString();
		}

		#endregion
	}
}

using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Reagent relic item with random bottle appearance and complex name generation.
	/// </summary>
	public class DDRelicReagent : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x44F1;
		private const int RANDOM_NAME_TYPE_MIN = 1;
		private const int RANDOM_NAME_TYPE_MAX = 4;

		#endregion

		#region Fields

		/// <summary>Item IDs for reagent bottle variants</summary>
		private static readonly int[] REAGENT_ITEM_IDS = new[]
		{
			0xE25, 0xE26, 0xE29, 0xE2A, 0xE2B, 0xE2C
		};

		/// <summary>Creature names for reagent naming (Type 1)</summary>
		private static readonly string[] CREATURE_NAMES = new[]
		{
			"formiga", "animal", "morcego", "urso", "besouro", "javali", "duende", "bugbear", "basilisco", "touro",
			"froglok", "gato", "centauro", "quimera", "vaca", "crocodilo", "ciclope", "elfo negro", "demônio", "diabo",
			"doppelganger", "dragão", "drake", "dríade", "anão", "elfo", "ettin", "sapo", "gárgula", "ghoul",
			"gigante", "gnoll", "gnomo", "goblin", "gorila", "gremlin", "grifo", "bruxa", "hobbit", "harpia",
			"hipogrifo", "hobgoblin", "cavalo", "hidra", "imp", "kobold", "kraken", "leprechaun", "lagarto", "homem-lagarto",
			"medusa", "humano", "minotauro", "rato", "naga", "pesadelo", "nixie", "ogro", "orc", "pixie",
			"pégaso", "fênix", "lagarto gigante", "rato", "cobra gigante", "sátiro", "escorpião", "serpente", "tubarão", "cobra",
			"esfinge", "aranha gigante", "aranha", "silvano", "sprite", "súcubo", "silvano", "titã", "sapo", "troglodita",
			"troll", "unicórnio", "vampiro", "doninha", "urso-lobisomem", "rato-lobisomem", "lobisomem", "gato-lobisomem", "lobo", "verme",
			"wyrm", "wyvern", "iéti", "zumbi"
		};

		/// <summary>Substance names for reagent naming (Type 1)</summary>
		private static readonly string[] SUBSTANCE_NAMES = new[]
		{
			"bile", "sangue", "pó de osso", "essência", "extrato", "olhos", "cabelo/pele", "ervas", "suco", "óleo",
			"pó", "sal", "molho", "aroma", "soro", "tempero", "cuspir", "lágrimas", "dentes", "urina"
		};

		/// <summary>Special reagent names (Type 2)</summary>
		private static readonly string[] SPECIAL_REAGENT_NAMES = new[]
		{
			"formigas", "lama", "bigodes de morcego", "abelhas", "cabelo de gato preto", "sal negro", "sanguessugas", "bigodes de gato", "centopeias", "lascas de caixão",
			"raios de lua cristalinos", "cílios de ciclope", "escamas de dragão", "poeira de efreet", "poeira elemental", "olho de salamandra", "poeira de fada", "asas de fada", "cinzas de gigante de fogo", "gosma gelatinosa",
			"fumaça de gênio", "flocos de pele de ghoul", "terra de cemitério", "lama", "cinzas de cão do inferno", "sanguessugas", "terra de lich", "mel do amor", "mosquitos", "tempero de múmia",
			"poeira mística", "geleia ocre", "cinzas de fênix", "poeira de pixie", "asas de pixie", "pó ritual", "sal de serpente do mar", "escamas de serpente", "escamas de cobra", "areia de feiticeiro",
			"asas de sprite", "folhas de árvore", "raiz de ceifador", "seiva de ent", "cinzas de vampiro", "essência de víbora", "vespas", "poeira de wisp", "hamamélis", "vermes",
			"carne de zumbi"
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new reagent relic with random appearance and name
		/// </summary>
		[Constructable]
		public DDRelicReagent() : base(BASE_ITEM_ID)
		{
			Weight = RelicConstants.WEIGHT_LIGHT;
			ItemID = Utility.RandomList(REAGENT_ITEM_IDS);
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			if (Utility.RandomMinMax(RANDOM_NAME_TYPE_MIN, RANDOM_NAME_TYPE_MAX) > 1)
			{
				string creatureName = CREATURE_NAMES[Utility.RandomMinMax(0, CREATURE_NAMES.Length - 1)];
				string substanceName = SUBSTANCE_NAMES[Utility.RandomMinMax(0, SUBSTANCE_NAMES.Length - 1)];
				Name = "frasco de " + creatureName + " " + substanceName;
			}
			else
			{
				string specialName = SPECIAL_REAGENT_NAMES[Utility.RandomMinMax(0, SPECIAL_REAGENT_NAMES.Length - 1)];
				Name = "frasco de " + specialName;
			}
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicReagent(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the reagent relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the reagent relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}

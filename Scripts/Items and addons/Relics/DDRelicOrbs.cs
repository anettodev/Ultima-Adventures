using System;
using Server;
using System.Collections;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	/// <summary>
	/// Crystal ball relic item with random appearance and scrying functionality.
	/// Displays random visions when double-clicked.
	/// </summary>
	public class DDRelicOrbs : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0xE2F;
		private const int RANDOM_ORB_TYPE_MIN = 0;
		private const int RANDOM_ORB_TYPE_MAX = 5;
		private const int RANDOM_VISION_MIN = 0;
		private const int RANDOM_VISION_MAX = 51;
		private const int MESSAGE_COLOR = 0x14C;

		#endregion

		#region Fields

		/// <summary>
		/// Structure for orb variant data
		/// </summary>
		private struct OrbVariant
		{
			public int ItemID;
			public int Weight;

			public OrbVariant(int itemID, int weight)
			{
				this.ItemID = itemID;
				this.Weight = weight;
			}
		}

		/// <summary>
		/// Array of orb variants
		/// </summary>
		private static readonly OrbVariant[] OrbVariants = new OrbVariant[]
		{
			new OrbVariant(0xE2F, RelicConstants.WEIGHT_HEAVY),
			new OrbVariant(0x4FD6, RelicConstants.WEIGHT_HEAVY),
			new OrbVariant(0xE2D, RelicConstants.WEIGHT_HEAVY),
			new OrbVariant(0x468A, RelicConstants.WEIGHT_HEAVY),
			new OrbVariant(0x468B, RelicConstants.WEIGHT_VERY_HEAVY_40),
			new OrbVariant(0x573E, RelicConstants.WEIGHT_VERY_HEAVY_30)
		};

		/// <summary>Spell caster names for crystal ball naming</summary>
		private static readonly string[] SPELL_CASTER_NAMES = new[]
		{
			"Clyz", "Achug", "Theram", "Quale", "Lutin", "Gad", "Croeq", "Achund", "Therrisi", "Qualorm",
			"Lyeit", "Garaso", "Crul", "Ackhine", "Thritai", "Quaso", "Lyetonu", "Garck", "Cuina", "Ackult",
			"Tig", "Quealt", "Moin", "Garund", "Daror", "Aeny", "Tinalt", "Rador", "Moragh", "Ghagha",
			"Deet", "Aeru", "Tinkima", "Rakeld", "Morir", "Ghatas", "Deldrad", "Ageick", "Tinut", "Rancwor",
			"Morosy", "Gosul", "Deldrae", "Agemor", "Tonk", "Ranildu", "Mosat", "Hatalt", "Delz", "Aghai",
			"Tonolde", "Ranot", "Mosd", "Hatash", "Denad", "Ahiny", "Tonper", "Ranper", "Mosrt", "Hatque",
			"Denold", "Aldkely", "Torint", "Ransayi", "Mosyl", "Hatskel", "Denyl", "Aleler", "Trooph", "Ranzmor",
			"Moszight", "Hattia", "Drahono", "Anagh", "Turbelm", "Raydan", "Naldely", "Hiert", "Draold", "Anclor",
			"Uighta", "Rayxwor", "Nalusk", "Hinalde", "Dynal", "Anl", "Uinga", "Rhit", "Nalwar", "Hinall",
			"Dyndray", "Antack", "Umnt", "Risormy", "Nag", "Hindend", "Eacki", "Ardburo", "Undaughe", "Risshy",
			"Nat", "Iade", "Earda", "Ardmose", "Untdran", "Rodiz", "Nator", "Iaper", "Echal", "Ardurne",
			"Untld", "Rodkali", "Nayth", "Iass", "Echind", "Ardyn", "Uoso", "Rodrado", "Neil", "Iawy",
			"Echwaro", "Ashaugha", "Urnroth", "Roort", "Nenal", "Iechi", "Eeni", "Ashdend", "Urode", "Ruina",
			"Newl", "Ightult", "Einea", "Ashye", "Uskdar", "Rynm", "Nia", "Ildaw", "Eldsera", "Asim",
			"Uskmdan", "Rynryna", "Nikim", "Ildoq", "Eldwen", "Athdra", "Usksough", "Ryns", "Nof", "Inabel",
			"Eldyril", "Athskel", "Usktoro", "Rynut", "Nook", "Inaony", "Elmkach", "Atkin", "Ustagee", "Samgha",
			"Nybage", "Inease", "Elmll", "Aughint", "Ustld", "Samnche", "Nyiy", "Ineegh", "Emath", "Aughthere",
			"Ustton", "Samssam", "Nyseld", "Ineiti", "Emengi", "Avery", "Verporm", "Sawor", "Nysklye", "Ineun",
			"Emild", "Awch", "Vesrade", "Sayimo", "Nyw", "Ingr", "Emmend", "Banend", "Voraughe", "Sayn",
			"Oasho", "Isbaugh", "Emnden", "Beac", "Vorril", "Sayskelu", "Oendy", "Islyei", "Endvelm", "Belan",
			"Vorunt", "Scheach", "Oenthi", "Issy", "Endych", "Beloz", "Whedan", "Scheyer", "Ohato", "Istin",
			"Engeh", "Beltiai", "Whisam", "Serat", "Oldack", "Iumo", "Engen", "Bliorm", "Whok", "Sernd",
			"Oldar", "Jyhin", "Engh", "Burold", "Worath", "Skell", "Oldr", "Jyon", "Engraki", "Buror",
			"Worav", "Skelser", "Oldtar", "Kalov", "Engroth", "Byt", "Worina", "Slim", "Omdser", "Kelol",
			"Engum", "Cakal", "Worryno", "Snaest", "Ond", "Kinser", "Enhech", "Carr", "Worunty", "Sniund",
			"Oron", "Koor", "Enina", "Cayld", "Worwaw", "Sosam", "Orrbel", "Lear", "Enk", "Cerar",
			"Yary", "Stayl", "Osnt", "Leert", "Enlald", "Cerl", "Yawi", "Stol", "Peright", "Legar",
			"Enskele", "Cerv", "Yena", "Strever", "Perpban", "Lerev", "Eoru", "Chaur", "Yero", "Swaih",
			"Phiunt", "Lerzshy", "Ernysi", "Chayn", "Yerrves", "Tagar", "Poll", "Llash", "Erque", "Cheimo",
			"Yhone", "Taienn", "Polrad", "Llotor", "Errusk", "Chekim", "Yradi", "Taiyild", "Polsera", "Loem",
			"Ervory", "Chreusk", "Zhugar", "Tanen", "Puon", "Loing", "Essisi", "Chrir", "Zirt", "Tasaf",
			"Quaev", "Lorelmo", "Essnd", "Chroelt", "Zoine", "Tasrr", "Quahang", "Lorud", "Estech", "Cloran",
			"Zotin", "Thaeng", "Qual", "Lour", "Estkunt", "Etoth", "Esule", "Estnight"
		};

		/// <summary>Adjective descriptors for crystal ball naming</summary>
		private static readonly string[] ADJECTIVE_DESCRIPTORS = new[]
		{
			"Exotic", "Mysterious", "Enchanted", "Marvelous", "Amazing", "Astonishing", "Mystical", "Astounding",
			"Magical", "Divine", "Excellent", "Magnificent", "Phenomenal", "Fantastic", "Incredible", "Extraordinary",
			"Fabulous", "Wondrous", "Glorious", "Lost", "Fabled", "Legendary", "Mythical", "Missing",
			"Ancestral", "Ornate", "Ultimate", "Rare", "Wonderful", "Sacred", "Almighty", "Supreme",
			"Mighty", "Unspeakable", "Unknown", "Forgotten"
		};

		/// <summary>Vision descriptions for scrying</summary>
		private static readonly string[] VISION_DESCRIPTIONS = new[]
		{
			"um príncipe", "um rei", "uma coroa", "uma espada", "um machado", "um leão", "um urso", "um morcego",
			"uma rainha", "uma princesa", "uma donzela", "um mendigo", "um demônio", "um diabo", "um anjo", "um dragão",
			"uma sombra", "uma águia", "um falcão", "um bardo", "um cavalo", "um lobo", "um pégaso", "um carneiro",
			"uma caveira", "uma aranha", "um unicórnio", "um escorpião", "uma pilha de tesouro", "um cadáver",
			"um olho olhando de volta para você", "uma cruz", "uma mulher", "um homem", "uma floresta",
			"uma terra coberta de neve", "um oceano", "um deserto", "uma selva", "uma fortaleza",
			"uma casa", "algumas ruínas", "um castelo", "uma cidade", "uma vila", "uma aldeia",
			"um forte", "uma masmorra", "uma caverna", "um cemitério", "uma tumba", "uma cripta"
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new crystal ball relic with random appearance and name
		/// </summary>
		[Constructable]
		public DDRelicOrbs() : base(BASE_ITEM_ID)
		{
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			int variant = Utility.RandomMinMax(RANDOM_ORB_TYPE_MIN, RANDOM_ORB_TYPE_MAX);
			OrbVariant orb = OrbVariants[variant];

			ItemID = orb.ItemID;
			Weight = orb.Weight;

			string casterName = SPELL_CASTER_NAMES[Utility.RandomMinMax(0, SPELL_CASTER_NAMES.Length - 1)];
			string adjective = ADJECTIVE_DESCRIPTORS[Utility.RandomMinMax(0, ADJECTIVE_DESCRIPTORS.Length - 1)];

			Name = casterName + "'s " + adjective + " Crystal Ball";
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicOrbs(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to display random vision
		/// </summary>
		public override void OnDoubleClick(Mobile from)
		{
			int visionIndex = Utility.RandomMinMax(RANDOM_VISION_MIN, RANDOM_VISION_MAX);
			string vision = VISION_DESCRIPTIONS[visionIndex];
			from.PrivateOverheadMessage(MessageType.Regular, MESSAGE_COLOR, false, "Dentro da bola você pode ver: " + vision + ".", from.NetState);
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the orbs relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the orbs relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}

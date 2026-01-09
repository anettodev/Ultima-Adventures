using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Scroll relic item with random spell name generation.
	/// </summary>
	public class DDRelicScrolls : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x227B;

		#endregion

		#region Fields

		/// <summary>Item IDs for scroll variants</summary>
		private static readonly int[] SCROLL_ITEM_IDS = new[]
		{
			0x227B, 0x227A, 0x2C94, 0x2272, 0x2278, 0x2273, 0x2279, 0x46AE, 0x46AF
		};

		/// <summary>Spell caster names (first part)</summary>
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
			"Ingr", "Emmend", "Banend", "Voraughe", "Sayn", "Oasho", "Isbaugh", "Emnden", "Beac", "Vorril",
			"Sayskelu", "Oendy", "Islyei", "Endvelm", "Belan", "Vorunt", "Scheach", "Oenthi", "Issy", "Endych",
			"Beloz", "Whedan", "Scheyer", "Ohato", "Istin", "Engeh", "Beltiai", "Whisam", "Serat", "Oldack",
			"Iumo", "Engen", "Bliorm", "Whok", "Sernd", "Oldar", "Jyhin", "Engh", "Burold", "Worath",
			"Skell", "Oldr", "Jyon", "Engraki", "Buror", "Worav", "Skelser", "Oldtar", "Kalov", "Engroth",
			"Byt", "Worina", "Slim", "Omdser", "Kelol", "Engum", "Cakal", "Worryno", "Snaest", "Ond",
			"Kinser", "Enhech", "Carr", "Worunty", "Sniund", "Oron", "Koor", "Enina", "Cayld", "Worwaw",
			"Sosam", "Orrbel", "Lear", "Enk", "Cerar", "Yary", "Stayl", "Osnt", "Leert", "Enlald",
			"Cerl", "Yawi", "Stol", "Peright", "Legar", "Enskele", "Cerv", "Yena", "Strever", "Perpban",
			"Lerev", "Eoru", "Chaur", "Yero", "Swaih", "Phiunt", "Lerzshy", "Ernysi", "Chayn", "Yerrves",
			"Tagar", "Poll", "Llash", "Erque", "Cheimo", "Yhone", "Taienn", "Polrad", "Llotor", "Errusk",
			"Chekim", "Yradi", "Taiyild", "Polsera", "Loem", "Ervory", "Chreusk", "Zhugar", "Tanen", "Puon",
			"Loing", "Essisi", "Chrir", "Zirt", "Tasaf", "Quaev", "Lorelmo", "Essnd", "Chroelt", "Zoine",
			"Tasrr", "Quahang", "Lorud", "Estech", "Cloran", "Zotin", "Thaeng", "Qual", "Lour", "Estkunt",
			"Etoth", "Esule", "Estnight"
		};

		/// <summary>Spell adjectives (second part)</summary>
		private static readonly string[] SPELL_ADJECTIVES = new[]
		{
			"Acidic", "Summoning", "Scrying", "Obscure", "Iron", "Ghoulish", "Enfeebling", "Altered", "Secret", "Obscuring",
			"Irresistible", "Gibbering", "Enlarged", "Confusing", "Analyzing", "Sympathetic", "Secure", "Permanent", "Keen", "Glittering",
			"Ethereal", "Contacting", "Animal", "Telekinetic", "Seeming", "Persistent", "Lawful", "Evil", "Continual", "Animated",
			"Telepathic", "Shadow", "Phantasmal", "Legendary", "Good", "Expeditious", "Control", "Antimagic", "Teleporting", "Shattering",
			"Phantom", "Lesser", "Grasping", "Explosive", "Crushing", "Arcane", "Temporal", "Shocking", "Phasing", "Levitating",
			"Greater", "Fabricated", "Cursed", "Articulated", "Tiny", "Shouting", "Planar", "Limited", "Guarding", "Faithful",
			"Dancing", "Binding", "Transmuting", "Shrinking", "Poisonous", "Lucubrating", "Fearful", "Dazzling", "Black", "Undead",
			"Silent", "Polymorphing", "Magical", "Hallucinatory", "Delayed", "Blinding", "Undetectable", "Slow", "Prismatic", "Magnificent",
			"Holding", "Fire", "Demanding", "Blinking", "Unseen", "Solid", "Programmed", "Major", "Horrid", "Discern",
			"Burning", "Vanishing", "Spectral", "Mending", "Hypnotic", "Floating", "Disintegrating", "Cat", "Protective", "Mind",
			"Ice", "Flying", "Disruptive", "Chain", "Spidery", "Prying", "Minor", "Illusionary", "Force", "Dominating",
			"Dreaming", "Chaotic", "Water", "Stone", "Rainbow", "Misdirected", "Incendiary", "Freezing", "Elemental", "Charming",
			"Watery", "Misleading", "Instant", "Gaseous", "Emotional", "Chilling", "Weird", "Storming", "Resilient", "Mnemonic",
			"Interposing", "Gentle", "Enduring", "Whispering", "Suggestive", "Reverse", "Moving", "Invisible", "Ghostly", "Energy",
			"Clenched", "Climbing", "Comprehending", "Colorful", "True", "False"
		};

		/// <summary>Spell nouns (third part)</summary>
		private static readonly string[] SPELL_NOUNS = new[]
		{
			"Acid", "Tentacles", "Sigil", "Plane", "Legend", "Gravity", "Emotion", "Chest", "Alarm", "Terrain",
			"Simulacrum", "Poison", "Lightning", "Grease", "Endurance", "Circle", "Anchor", "Thoughts", "Skin", "Polymorph",
			"Lights", "Growth", "Enervation", "Clairvoyance", "Animal", "Time", "Sleep", "Prestidigitation", "Location", "Guards",
			"Enfeeblement", "Clone", "Antipathy", "Tongues", "Soul", "Projection", "Lock", "Hand", "Enhancer", "Cloud",
			"Arcana", "Touch", "Sound", "Pyrotechnics", "Lore", "Haste", "Etherealness", "Cold", "Armor", "Transformation",
			"Spells", "Refuge", "Lucubration", "Hat", "Evil", "Color", "Arrows", "Trap", "Sphere", "Repulsion",
			"Magic", "Hound", "Evocation", "Confusion", "Aura", "Trick", "Spider", "Resistance", "Mansion", "Hypnotism",
			"Eye", "Conjuration", "Banishment", "Turning", "Spray", "Retreat", "Mask", "Ice", "Fall", "Contagion",
			"Banshee", "Undead", "Stasis", "Rope", "Maze", "Image", "Fear", "Creation", "Bear", "Vanish",
			"Statue", "Runes", "Message", "Imprisonment", "Feather", "Curse", "Binding", "Veil", "Steed", "Scare",
			"Meteor", "Insanity", "Field", "Dance", "Vision", "Stone", "Screen", "Mind", "Invisibility", "Fireball",
			"Darkness", "Blindness", "Vocation", "Storm", "Script", "Mirage", "Invulnerability", "Flame", "Daylight", "Blink",
			"Wail", "Strength", "Scrying", "Misdirection", "Iron", "Flesh", "Dead", "Blur", "Walk", "Strike",
			"Seeing", "Missile", "Item", "Fog", "Deafness", "Body", "Wall", "Stun", "Self", "Mist",
			"Jar", "Force", "Death", "Bolt", "Wards", "Suggestion", "Sending", "Monster", "Jaunt", "Foresight",
			"Demand", "Bond", "Water", "Summons", "Servant", "Mouth", "Jump", "Form", "Disjunction", "Breathing",
			"Weapon", "Sunburst", "Shadow", "Mud", "Kill", "Freedom", "Disk", "Burning", "Weather", "Swarm",
			"Shape", "Nightmare", "Killer", "Frost", "Dismissal", "Cage", "Web", "Symbol", "Shelter", "Object",
			"Knock", "Gate", "Displacement", "Chain", "Wilting", "Sympathy", "Shield", "Page", "Languages", "Good",
			"Door", "Chaos", "Wind", "Telekinesis", "Shift", "Pattern", "Laughter", "Grace", "Drain", "Charm",
			"Wish", "Teleport", "Shout", "Person", "Law", "Grasp", "Dream", "Elements", "Edge", "Earth",
			"Dust"
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new scroll relic with random spell name
		/// </summary>
		[Constructable]
		public DDRelicScrolls() : base(BASE_ITEM_ID)
		{
			Weight = RelicConstants.WEIGHT_VERY_LIGHT;
			ItemID = Utility.RandomList(SCROLL_ITEM_IDS);
			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			string casterName = SPELL_CASTER_NAMES[Utility.RandomMinMax(0, SPELL_CASTER_NAMES.Length - 1)];
			string adjective = SPELL_ADJECTIVES[Utility.RandomMinMax(0, SPELL_ADJECTIVES.Length - 1)];
			string noun = SPELL_NOUNS[Utility.RandomMinMax(0, SPELL_NOUNS.Length - 1)];

			Name = casterName + "'s Spell of " + adjective + " " + noun;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicScrolls(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the scrolls relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the scrolls relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}

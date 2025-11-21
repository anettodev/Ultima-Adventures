using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized string constants for Inscription crafting system.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Replaces hardcoded cliloc IDs with meaningful translations.
	/// </summary>
	public static class InscriptionStringConstants
	{
		#region Category Names

		/// <summary>Cliloc 1044369: "1st & 2nd Circle"</summary>
		public const string CATEGORY_CIRCLE_1_AND_2 = "1º e 2º Círculo";

		/// <summary>Cliloc 1044371: "3rd & 4th Circle"</summary>
		public const string CATEGORY_CIRCLE_3_AND_4 = "3º e 4º Círculo";

		/// <summary>Cliloc 1044373: "5th & 6th Circle"</summary>
		public const string CATEGORY_CIRCLE_5_AND_6 = "5º e 6º Círculo";

		/// <summary>Cliloc 1044375: "7th & 8th Circle"</summary>
		public const string CATEGORY_CIRCLE_7_AND_8 = "7º e 8º Círculo";

		/// <summary>Cliloc 1044294: "Other Items"</summary>
		public const string CATEGORY_OTHER_ITEMS = "Outros Itens";

		/// <summary>Cliloc 1061677: "Necromancy"</summary>
		public const string CATEGORY_NECROMANCY = "Necromancia";

		/// <summary>Custom category: "Expert Study Books"</summary>
		public const string CATEGORY_EXPERT_STUDY_BOOKS = "Livros de Estudo Avançado";

		#endregion

		#region Item Names - Crafted Items

		/// <summary>Cliloc 1044377: "Blank Scroll"</summary>
		public const string ITEM_BLANK_SCROLL = "Pergaminho em Branco";

		/// <summary>Cliloc 1041267: "Runebook"</summary>
		public const string ITEM_RUNEBOOK = "Livro de Runas";

		/// <summary>Cliloc 1028793: "Bulk Order Book"</summary>
		public const string ITEM_BULK_ORDER_BOOK = "Livro de Encomendas";

		/// <summary>Cliloc 1023834: "Spellbook"</summary>
		public const string ITEM_SPELLBOOK = "Livro de Magias";

		/// <summary>Cliloc 1028787: "Necromancer Spellbook"</summary>
		public const string ITEM_NECROMANCER_SPELLBOOK = "Livro de Necromancia";

		/// <summary>Cliloc 1095590: "Song Book"</summary>
		public const string ITEM_SONG_BOOK = "Livro de Canções";

		#endregion

		#region Resource Names

		/// <summary>Cliloc 1044353: "Black Pearl"</summary>
		public const int RESOURCE_BLACK_PEARL = 1044353;

		/// <summary>Cliloc 1044354: "Bloodmoss"</summary>
		public const int RESOURCE_BLOODMOSS = 1044354;

		/// <summary>Cliloc 1044355: "Garlic"</summary>
		public const int RESOURCE_GARLIC = 1044355;

		/// <summary>Cliloc 1044356: "Ginseng"</summary>
		public const int RESOURCE_GINSENG = 1044356;

		/// <summary>Cliloc 1044357: "Mandrake Root"</summary>
		public const int RESOURCE_MANDRAKE_ROOT = 1044357;

		/// <summary>Cliloc 1044358: "Nightshade"</summary>
		public const int RESOURCE_NIGHTSHADE = 1044358;

		/// <summary>Cliloc 1044359: "Sulfurous Ash"</summary>
		public const int RESOURCE_SULFUROUS_ASH = 1044359;

		/// <summary>Cliloc 1044360: "Spider's Silk"</summary>
		public const int RESOURCE_SPIDERS_SILK = 1044360;

		/// <summary>Cliloc 1073477: "Bark Fragment"</summary>
		public const string RESOURCE_BARK_FRAGMENT = "Fragmento de Casca";

		/// <summary>Cliloc 1044462: "Leather"</summary>
		public const string RESOURCE_LEATHER = "Couro";

		/// <summary>Cliloc 1025154: "Beeswax"</summary>
		public const string RESOURCE_BEESWAX = "Cera de Abelha";

		/// <summary>Cliloc 1044445: "Recall Scroll"</summary>
		public const string RESOURCE_RECALL_SCROLL = "Pergaminho de Recall";

		/// <summary>Cliloc 1044446: "Gate Travel Scroll"</summary>
		public const string RESOURCE_GATE_TRAVEL_SCROLL = "Pergaminho de Gate Travel";

		/// <summary>Cliloc 1027958: "Recall Rune"</summary>
		public const string RESOURCE_RECALL_RUNE = "Runa de Recall";

		public const string RESOURCE_LUTE = "Alaúde";

		#endregion

		#region Spell Scroll Names - Circle 1

		/// <summary>Cliloc 1044381: "Reactive Armor"</summary>
		public const int SCROLL_REACTIVE_ARMOR = 1044381;

		/// <summary>Cliloc 1044382: "Clumsy"</summary>
		public const int SCROLL_CLUMSY = 1044382;

		/// <summary>Cliloc 1044383: "Create Food"</summary>
		public const int SCROLL_CREATE_FOOD = 1044383;

		/// <summary>Cliloc 1044384: "Feeblemind"</summary>
		public const int SCROLL_FEEBLEMIND = 1044384;

		/// <summary>Cliloc 1044385: "Heal"</summary>
		public const int SCROLL_HEAL = 1044385;

		/// <summary>Cliloc 1044386: "Magic Arrow"</summary>
		public const int SCROLL_MAGIC_ARROW = 1044386;

		/// <summary>Cliloc 1044387: "Night Sight"</summary>
		public const int SCROLL_NIGHT_SIGHT = 1044387;

		/// <summary>Cliloc 1044388: "Weaken"</summary>
		public const int SCROLL_WEAKEN = 1044388;

		#endregion

		#region Spell Scroll Names - Circle 2

		/// <summary>Cliloc 1044389: "Agility"</summary>
		public const int SCROLL_AGILITY = 1044389;

		/// <summary>Cliloc 1044390: "Cunning"</summary>
		public const int SCROLL_CUNNING = 1044390;

		/// <summary>Cliloc 1044391: "Cure"</summary>
		public const int SCROLL_CURE = 1044391;

		/// <summary>Cliloc 1044392: "Harm"</summary>
		public const int SCROLL_HARM = 1044392;

		/// <summary>Cliloc 1044393: "Magic Trap"</summary>
		public const int SCROLL_MAGIC_TRAP = 1044393;

		/// <summary>Cliloc 1044394: "Magic Untrap"</summary>
		public const int SCROLL_MAGIC_UNTRAP = 1044394;

		/// <summary>Cliloc 1044395: "Protection"</summary>
		public const int SCROLL_PROTECTION = 1044395;

		/// <summary>Cliloc 1044396: "Strength"</summary>
		public const int SCROLL_STRENGTH = 1044396;

		#endregion

		#region Spell Scroll Names - Circle 3

		/// <summary>Cliloc 1044397: "Bless"</summary>
		public const int SCROLL_BLESS = 1044397;

		/// <summary>Cliloc 1044398: "Fireball"</summary>
		public const int SCROLL_FIREBALL = 1044398;

		/// <summary>Cliloc 1044399: "Magic Lock"</summary>
		public const int SCROLL_MAGIC_LOCK = 1044399;

		/// <summary>Cliloc 1044400: "Poison"</summary>
		public const int SCROLL_POISON = 1044400;

		/// <summary>Cliloc 1044401: "Telekinesis"</summary>
		public const int SCROLL_TELEKINESIS = 1044401;

		/// <summary>Cliloc 1044402: "Teleport"</summary>
		public const int SCROLL_TELEPORT = 1044402;

		/// <summary>Cliloc 1044403: "Unlock"</summary>
		public const int SCROLL_UNLOCK = 1044403;

		/// <summary>Cliloc 1044404: "Wall of Stone"</summary>
		public const int SCROLL_WALL_OF_STONE = 1044404;

		#endregion

		#region Spell Scroll Names - Circle 4

		/// <summary>Cliloc 1044405: "Arch Cure"</summary>
		public const int SCROLL_ARCH_CURE = 1044405;

		/// <summary>Cliloc 1044406: "Arch Protection"</summary>
		public const int SCROLL_ARCH_PROTECTION = 1044406;

		/// <summary>Cliloc 1044407: "Curse"</summary>
		public const int SCROLL_CURSE = 1044407;

		/// <summary>Cliloc 1044408: "Fire Field"</summary>
		public const int SCROLL_FIRE_FIELD = 1044408;

		/// <summary>Cliloc 1044409: "Greater Heal"</summary>
		public const int SCROLL_GREATER_HEAL = 1044409;

		/// <summary>Cliloc 1044410: "Lightning"</summary>
		public const int SCROLL_LIGHTNING = 1044410;

		/// <summary>Cliloc 1044411: "Mana Drain"</summary>
		public const int SCROLL_MANA_DRAIN = 1044411;

		/// <summary>Cliloc 1044412: "Recall"</summary>
		public const int SCROLL_RECALL = 1044412;

		#endregion

		#region Spell Scroll Names - Circle 5

		/// <summary>Cliloc 1044413: "Blade Spirits"</summary>
		public const int SCROLL_BLADE_SPIRITS = 1044413;

		/// <summary>Cliloc 1044414: "Dispel Field"</summary>
		public const int SCROLL_DISPEL_FIELD = 1044414;

		/// <summary>Cliloc 1044415: "Incognito"</summary>
		public const int SCROLL_INCOGNITO = 1044415;

		/// <summary>Cliloc 1044416: "Magic Reflection"</summary>
		public const int SCROLL_MAGIC_REFLECTION = 1044416;

		/// <summary>Cliloc 1044417: "Mind Blast"</summary>
		public const int SCROLL_MIND_BLAST = 1044417;

		/// <summary>Cliloc 1044418: "Paralyze"</summary>
		public const int SCROLL_PARALYZE = 1044418;

		/// <summary>Cliloc 1044419: "Poison Field"</summary>
		public const int SCROLL_POISON_FIELD = 1044419;

		/// <summary>Cliloc 1044420: "Summon Creature"</summary>
		public const int SCROLL_SUMMON_CREATURE = 1044420;

		#endregion

		#region Spell Scroll Names - Circle 6

		/// <summary>Cliloc 1044421: "Dispel"</summary>
		public const int SCROLL_DISPEL = 1044421;

		/// <summary>Cliloc 1044422: "Energy Bolt"</summary>
		public const int SCROLL_ENERGY_BOLT = 1044422;

		/// <summary>Cliloc 1044423: "Explosion"</summary>
		public const int SCROLL_EXPLOSION = 1044423;

		/// <summary>Cliloc 1044424: "Invisibility"</summary>
		public const int SCROLL_INVISIBILITY = 1044424;

		/// <summary>Cliloc 1044425: "Mark"</summary>
		public const int SCROLL_MARK = 1044425;

		/// <summary>Cliloc 1044426: "Mass Curse"</summary>
		public const int SCROLL_MASS_CURSE = 1044426;

		/// <summary>Cliloc 1044427: "Paralyze Field"</summary>
		public const int SCROLL_PARALYZE_FIELD = 1044427;

		/// <summary>Cliloc 1044428: "Reveal"</summary>
		public const int SCROLL_REVEAL = 1044428;

		#endregion

		#region Spell Scroll Names - Circle 7

		/// <summary>Cliloc 1044429: "Chain Lightning"</summary>
		public const int SCROLL_CHAIN_LIGHTNING = 1044429;

		/// <summary>Cliloc 1044430: "Energy Field"</summary>
		public const int SCROLL_ENERGY_FIELD = 1044430;

		/// <summary>Cliloc 1044431: "Flamestrike"</summary>
		public const int SCROLL_FLAMESTRIKE = 1044431;

		/// <summary>Cliloc 1044432: "Gate Travel"</summary>
		public const int SCROLL_GATE_TRAVEL = 1044432;

		/// <summary>Cliloc 1044433: "Mana Vampire"</summary>
		public const int SCROLL_MANA_VAMPIRE = 1044433;

		/// <summary>Cliloc 1044434: "Mass Dispel"</summary>
		public const int SCROLL_MASS_DISPEL = 1044434;

		/// <summary>Cliloc 1044435: "Meteor Swarm"</summary>
		public const int SCROLL_METEOR_SWARM = 1044435;

		/// <summary>Cliloc 1044436: "Polymorph"</summary>
		public const int SCROLL_POLYMORPH = 1044436;

		#endregion

		#region Spell Scroll Names - Circle 8

		/// <summary>Cliloc 1044437: "Earthquake"</summary>
		public const int SCROLL_EARTHQUAKE = 1044437;

		/// <summary>Cliloc 1044438: "Energy Vortex"</summary>
		public const int SCROLL_ENERGY_VORTEX = 1044438;

		/// <summary>Cliloc 1044439: "Resurrection"</summary>
		public const int SCROLL_RESURRECTION = 1044439;

		/// <summary>Cliloc 1044440: "Summon Air Elemental"</summary>
		public const int SCROLL_SUMMON_AIR_ELEMENTAL = 1044440;

		/// <summary>Cliloc 1044441: "Summon Daemon"</summary>
		public const int SCROLL_SUMMON_DAEMON = 1044441;

		/// <summary>Cliloc 1044442: "Summon Earth Elemental"</summary>
		public const int SCROLL_SUMMON_EARTH_ELEMENTAL = 1044442;

		/// <summary>Cliloc 1044443: "Summon Fire Elemental"</summary>
		public const int SCROLL_SUMMON_FIRE_ELEMENTAL = 1044443;

		/// <summary>Cliloc 1044444: "Summon Water Elemental"</summary>
		public const int SCROLL_SUMMON_WATER_ELEMENTAL = 1044444;

		#endregion

		#region Necromancy Spell Names

		/// <summary>Cliloc 1060509: "Animate Dead"</summary>
		public const string NECRO_ANIMATE_DEAD = "Animar Mortos";

		/// <summary>Cliloc 1060510: "Blood Oath"</summary>
		public const string NECRO_BLOOD_OATH = "Juramento de Sangue";

		/// <summary>Cliloc 1060511: "Corpse Skin"</summary>
		public const string NECRO_CORPSE_SKIN = "Pele de Cadáver";

		/// <summary>Cliloc 1060512: "Curse Weapon"</summary>
		public const string NECRO_CURSE_WEAPON = "Amaldiçoar Arma";

		/// <summary>Cliloc 1060513: "Evil Omen"</summary>
		public const string NECRO_EVIL_OMEN = "Presságio Maligno";

		/// <summary>Cliloc 1060514: "Horrific Beast"</summary>
		public const string NECRO_HORRIFIC_BEAST = "Besta Horrível";

		/// <summary>Cliloc 1060515: "Lich Form"</summary>
		public const string NECRO_LICH_FORM = "Forma de Lich";

		/// <summary>Cliloc 1060516: "Mind Rot"</summary>
		public const string NECRO_MIND_ROT = "Apodrecer a Mente";

		/// <summary>Cliloc 1060517: "Pain Spike"</summary>
		public const string NECRO_PAIN_SPIKE = "Espinho de Dor";

		/// <summary>Cliloc 1060518: "Poison Strike"</summary>
		public const string NECRO_POISON_STRIKE = "Golpe Venenoso";

		/// <summary>Cliloc 1060519: "Strangle"</summary>
		public const string NECRO_STRANGLE = "Estrangular";

		/// <summary>Cliloc 1060520: "Summon Familiar"</summary>
		public const string NECRO_SUMMON_FAMILIAR = "Invocar Familiar";

		/// <summary>Cliloc 1060521: "Vampiric Embrace"</summary>
		public const string NECRO_VAMPIRIC_EMBRACE = "Abraço Vampírico";

		/// <summary>Cliloc 1060522: "Vengeful Spirit"</summary>
		public const string NECRO_VENGEFUL_SPIRIT = "Espírito Vingativo";

		/// <summary>Cliloc 1060523: "Wither"</summary>
		public const string NECRO_WITHER = "Murchar";

		/// <summary>Cliloc 1060524: "Wraith Form"</summary>
		public const string NECRO_WRAITH_FORM = "Forma Espectral";

		/// <summary>Cliloc 1060525: "Exorcism"</summary>
		public const string NECRO_EXORCISM = "Exorcismo";

		#endregion

		#region Bard Scroll Names (Commented out in original)

		public const string BARD_ARMYS_PAEON = "Peã do Exército";
		public const string BARD_ENCHANTING_ETUDE = "Estudo Encantador";
		public const string BARD_ENERGY_CAROL = "Canto de Energia";
		public const string BARD_ENERGY_THRENODY = "Lamento de Energia";
		public const string BARD_FIRE_CAROL = "Canto de Fogo";
		public const string BARD_FIRE_THRENODY = "Lamento de Fogo";
		public const string BARD_FOE_REQUIEM = "Réquiem do Inimigo";
		public const string BARD_ICE_CAROL = "Canto de Gelo";
		public const string BARD_ICE_THRENODY = "Lamento de Gelo";
		public const string BARD_KNIGHTS_MINNE = "Minne do Cavaleiro";
		public const string BARD_MAGES_BALLAD = "Balada do Mago";
		public const string BARD_MAGIC_FINALE = "Final Mágico";
		public const string BARD_POISON_CAROL = "Canto de Veneno";
		public const string BARD_POISON_THRENODY = "Lamento de Veneno";
		public const string BARD_SHEEPFOE_MAMBO = "Mambo Anti-Ovelha";
		public const string BARD_SINEWY_ETUDE = "Estudo Musculoso";

		#endregion

		#region Error Messages

		/// <summary>Cliloc 1044038: "You have worn out your tool!"</summary>
		public const string MSG_TOOL_WORN_OUT = "Você desgastou sua ferramenta!";

		/// <summary>Cliloc 1044263: "The tool must be on your person to use."</summary>
		public const string MSG_TOOL_MUST_BE_ON_PERSON = "A ferramenta deve estar com você para usar.";

		/// <summary>Cliloc 1042404: "You don't have that spell!"</summary>
		public const string MSG_DONT_HAVE_SPELL = "Você não possui essa magia!";

		/// <summary>Cliloc 1044253: Generic resource lack message</summary>
		public const string MSG_RESOURCE_LACK = "Você não tem recursos suficientes.";

		/// <summary>Cliloc 1044361+: "You don't have [reagent]" base message</summary>
		public const string MSG_REAGENT_LACK_FORMAT = "Você não tem {0} suficiente.";

		/// <summary>Cliloc 1044378: "You don't have enough blank scrolls"</summary>
		public const string MSG_BLANK_SCROLL_LACK = "Você não tem pergaminhos em branco suficientes.";

		/// <summary>Cliloc 1073478: "You don't have enough bark fragments"</summary>
		public const string MSG_BARK_FRAGMENT_LACK = "Você não tem fragmentos de casca suficientes.";

		/// <summary>Cliloc 501627: "You don't have the required reagents" (Necro)</summary>
		public const string MSG_NECRO_REAGENT_LACK = "Você não tem os reagentes necessários.";

		#endregion

		#region Success/Failure Messages

		/// <summary>Cliloc 1044043: "You failed to create the item, and some of your materials are lost."</summary>
		public const string MSG_FAILED_MATERIAL_LOST = "Você falhou em criar o item e alguns materiais foram perdidos.";

		/// <summary>Cliloc 1044157: "You failed to create the item, but no materials were lost."</summary>
		public const string MSG_FAILED_NO_MATERIAL_LOST = "Você falhou em criar o item, mas nenhum material foi perdido.";

		/// <summary>Cliloc 502785: "You were barely able to make this item. It's quality is below average."</summary>
		public const string MSG_BARELY_MADE_ITEM = "Você mal conseguiu fazer este item. A qualidade está abaixo da média.";

		/// <summary>Cliloc 1044156: "You create an exceptional quality item and affix your maker's mark."</summary>
		public const string MSG_EXCEPTIONAL_WITH_MARK = "Você cria um item de qualidade excepcional e coloca sua marca de criador.";

		/// <summary>Cliloc 1044155: "You create an exceptional quality item."</summary>
		public const string MSG_EXCEPTIONAL_QUALITY = "Você cria um item de qualidade excepcional.";

		/// <summary>Cliloc 1044154: "You create the item."</summary>
		public const string MSG_ITEM_CREATED = "Você cria o item.";

		/// <summary>Cliloc 501630: "You fail to inscribe the scroll, and the scroll is ruined."</summary>
		public const string MSG_SCROLL_FAILED = "Você falha ao inscrever o pergaminho, e o pergaminho é arruinado.";

		/// <summary>Cliloc 501629: "You inscribe the spell and put the scroll in your backpack."</summary>
		public const string MSG_SCROLL_SUCCESS = "Você inscreve a magia e coloca o pergaminho na mochila.";

		#endregion
	}
}

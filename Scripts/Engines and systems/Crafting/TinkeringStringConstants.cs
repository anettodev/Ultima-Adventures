namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized string constants for Tinkering messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from DefTinkering.cs to improve maintainability and enable localization.
	/// </summary>
	public static class TinkeringStringConstants
	{
		#region Craft Group Names

		/// <summary>Group name: "Itens de Madeira"</summary>
		public const string GROUP_WOODEN_ITEMS = "Itens de Madeira";

		/// <summary>Group name: "Ferramentas"</summary>
		public const string GROUP_TOOLS = "Ferramentas";

		/// <summary>Group name: "Peças"</summary>
		public const string GROUP_PARTS = "Peças";

		/// <summary>Group name: "Utensílios"</summary>
		public const string GROUP_UTENSILS = "Utensílios";

		/// <summary>Group name: "Joias"</summary>
		public const string GROUP_JEWELRY = "Joias";

		/// <summary>Group name: "Variados"</summary>
		public const string GROUP_MISC = "Variados";

		/// <summary>Group name: "Montagens"</summary>
		public const string GROUP_MULTI_COMPONENT = "Montagens";

		/// <summary>Group name: "Hospitalidade"</summary>
		public const string GROUP_HOSPITALITY = "Hospitalidade";

		#endregion

		#region Menu and System Messages

		/// <summary>Menu title: "MENU DE INVENTARIA"</summary>
		public const string MSG_GUMP_TITLE = "MENU DE INVENTARIA";

		/// <summary>Error: "Você quebrou sua ferramenta!"</summary>
		public const string MSG_TOOL_WORN_OUT = "Você quebrou sua ferramenta!";

		/// <summary>Error: "A ferramenta deve estar com você para usar."</summary>
		public const string MSG_TOOL_MUST_BE_ON_PERSON = "A ferramenta deve estar com você para usar.";

		/// <summary>Error: "Você falhou ao criar o item, e alguns de seus materiais foram perdidos."</summary>
		public const string MSG_FAILED_LOST_MATERIALS = "Você falhou ao criar o item, e alguns de seus materiais foram perdidos.";

		/// <summary>Error: "Você falhou ao criar o item, mas nenhum material foi perdido."</summary>
		public const string MSG_FAILED_NO_MATERIALS_LOST = "Você falhou ao criar o item, mas nenhum material foi perdido.";

		/// <summary>Warning: "Você mal conseguiu fazer este item. Sua qualidade está abaixo da média."</summary>
		public const string MSG_BARELY_MADE_ITEM = "Você mal conseguiu fazer este item. Sua qualidade está abaixo da média.";

		/// <summary>Success: "Você cria um item de qualidade excepcional e assina a sua marca nele."</summary>
		public const string MSG_EXCEPTIONAL_WITH_MARK = "Você cria um item de qualidade excepcional e assina a sua marca nele.";

		/// <summary>Success: "Você cria um item de qualidade excepcional."</summary>
		public const string MSG_EXCEPTIONAL_QUALITY = "Você cria um item de qualidade excepcional.";

		/// <summary>Success: "Você cria o item."</summary>
		public const string MSG_ITEM_CREATED = "Você cria o item.";

		#endregion

		#region Resource Names

		/// <summary>Resource name: "Lingotes de Ferro"</summary>
		public const string RESOURCE_IRON_INGOTS = "Lingotes de Ferro";

		/// <summary>Error: "Você não tem lingotes suficientes."</summary>
		public const string MSG_INSUFFICIENT_INGOTS = "Você não tem lingotes suficientes.";

		/// <summary>Resource name: "Toras"</summary>
		public const string RESOURCE_LOGS = "Toras";

		/// <summary>Error: "Você não tem toras suficientes."</summary>
		public const string MSG_INSUFFICIENT_LOGS = "Você não tem toras suficientes.";

		/// <summary>Error: "Você não tem os itens necessários."</summary>
		public const string MSG_INSUFFICIENT_RESOURCES = "Você não tem os itens necessários.";

		/// <summary>Resource name: "Granito"</summary>
		public const string RESOURCE_GRANITE = "Granito";

		/// <summary>Error: "Você não tem granito suficiente."</summary>
		public const string MSG_INSUFFICIENT_GRANITE = "Você não tem granito suficiente.";

		/// <summary>Resource name: "Couro"</summary>
		public const string RESOURCE_LEATHER = "Couro";

		/// <summary>Error: "Você não tem couro suficiente."</summary>
		public const string MSG_INSUFFICIENT_LEATHER = "Você não tem couro suficiente.";

		/// <summary>Resource name: "Cera de Abelha"</summary>
		public const string RESOURCE_BEESWAX = "Cera de Abelha";

		/// <summary>Error: "Você não tem cera de abelha suficiente."</summary>
		public const string MSG_INSUFFICIENT_BEESWAX = "Você não tem cera de abelha suficiente.";

		/// <summary>Resource name: "Gema Arcana"</summary>
		public const string RESOURCE_ARCANE_GEM = "Gema Arcana";

		/// <summary>Resource name: "Garrafa"</summary>
		public const string RESOURCE_BOTTLE = "Garrafa";

		/// <summary>Resource name: "Tampa de Barril"</summary>
		public const string RESOURCE_BARREL_LID = "Tampa de Barril";

		/// <summary>Resource name: "Torneira de Barril"</summary>
		public const string RESOURCE_BARREL_TAP = "Torneira de Barril";

		/// <summary>Resource name: "Tocha"</summary>
		public const string RESOURCE_TORCH = "Tocha";

		#endregion

		#region Item Names - Wooden Items

		/// <summary>Item name: "plaina de junção"</summary>
		public const string ITEM_JOINTING_PLANE = "plaina de junção";

		/// <summary>Item name: "plaina de moldura"</summary>
		public const string ITEM_MOULDING_PLANE = "plaina de moldura";

		/// <summary>Item name: "plaina de alisamento"</summary>
		public const string ITEM_SMOOTHING_PLANE = "plaina de alisamento";

		/// <summary>Item name: "estrutura de relógio"</summary>
		public const string ITEM_CLOCK_FRAME = "estrutura de relógio";

		/// <summary>Item name: "eixo"</summary>
		public const string ITEM_AXLE = "eixo";

		/// <summary>Item name: "rolo de massa"</summary>
		public const string ITEM_ROLLING_PIN = "rolo de massa";

		/// <summary>Item name: "nunchaku"</summary>
		public const string ITEM_NUNCHAKU = "nunchaku";

		/// <summary>Item name: "serraria (sul)"</summary>
		public const string ITEM_SAW_MILL_SOUTH = "serraria (sul)";

		/// <summary>Item name: "serraria (leste)"</summary>
		public const string ITEM_SAW_MILL_EAST = "serraria (leste)";

		#endregion

		#region Item Names - Tools

		/// <summary>Item name: "tesoura"</summary>
		public const string ITEM_SCISSORS = "tesoura";

		/// <summary>Item name: "almofariz e pilão"</summary>
		public const string ITEM_MORTAR_PESTLE = "almofariz e pilão";

		/// <summary>Item name: "escorp"</summary>
		public const string ITEM_SCORP = "escorp";

		/// <summary>Item name: "ferramentas de inventor"</summary>
		public const string ITEM_TINKER_TOOLS = "ferramentas de inventor";

		/// <summary>Item name: "machadinha"</summary>
		public const string ITEM_HATCHET = "machadinha";

		/// <summary>Item name: "faca de desenho"</summary>
		public const string ITEM_DRAW_KNIFE = "faca de desenho";

		/// <summary>Item name: "kit de costura"</summary>
		public const string ITEM_SEWING_KIT = "kit de costura";

		/// <summary>Item name: "serra"</summary>
		public const string ITEM_SAW = "serra";

		/// <summary>Item name: "serra de cauda de andorinha"</summary>
		public const string ITEM_DOVETAIL_SAW = "serra de cauda de andorinha";

		/// <summary>Item name: "froe"</summary>
		public const string ITEM_FROE = "froe";

		/// <summary>Item name: "pá"</summary>
		public const string ITEM_SHOVEL = "pá";

		/// <summary>Item name: "martelo"</summary>
		public const string ITEM_HAMMER = "martelo";

		/// <summary>Item name: "tenazes"</summary>
		public const string ITEM_TONGS = "tenazes";

		/// <summary>Item name: "martelo de ferreiro"</summary>
		public const string ITEM_SMITH_HAMMER = "martelo de ferreiro";

		/// <summary>Item name: "marreta"</summary>
		public const string ITEM_SLEDGE_HAMMER = "marreta";

		/// <summary>Item name: "inshave"</summary>
		public const string ITEM_INSHAVE = "inshave";

		/// <summary>Item name: "picareta"</summary>
		public const string ITEM_PICKAXE = "picareta";

		/// <summary>Item name: "gazua"</summary>
		public const string ITEM_LOCKPICK = "gazua";

		/// <summary>Item name: "frigideira"</summary>
		public const string ITEM_SKILLET = "frigideira";

		/// <summary>Item name: "peneira de farinha"</summary>
		public const string ITEM_FLOUR_SIFTER = "peneira de farinha";

		/// <summary>Item name: "ferramentas de flecheiro"</summary>
		public const string ITEM_FLETCHER_TOOLS = "ferramentas de flecheiro";

		/// <summary>Item name: "caneta de cartógrafo"</summary>
		public const string ITEM_MAPMAKERS_PEN = "caneta de cartógrafo";

		/// <summary>Item name: "caneta de escriba"</summary>
		public const string ITEM_SCRIBES_PEN = "caneta de escriba";

		/// <summary>Item name: "tesoura de jardinagem"</summary>
		public const string ITEM_GARDEN_TOOL = "tesoura de jardinagem";

		/// <summary>Item name: "caldeirão de herbalista"</summary>
		public const string ITEM_HERBALIST_CAULDRON = "caldeirão de herbalista";

		/// <summary>Item name: "pá de minério"</summary>
		public const string ITEM_ORE_SHOVEL = "pá de minério";

		/// <summary>Item name: "pá de sepultura"</summary>
		public const string ITEM_GRAVE_SHOVEL = "pá de sepultura";

		/// <summary>Item name: "faca de esfolar"</summary>
		public const string ITEM_SKINNING_KNIFE = "faca de esfolar";

		/// <summary>Item name: "bisturi"</summary>
		public const string ITEM_SURGEONS_KNIFE = "bisturi";

		/// <summary>Item name: "caldeirão de mistura"</summary>
		public const string ITEM_MIXING_CAULDRON = "caldeirão de mistura";

		/// <summary>Item name: "panela de cera"</summary>
		public const string ITEM_WAXING_POT = "panela de cera";

		/// <summary>Item name: "ferramentas de carpintaria"</summary>
		public const string ITEM_WOODWORKING_TOOLS = "ferramentas de carpintaria";

		/// <summary>Item name: "ferramentas de armadilhas"</summary>
		public const string ITEM_TRAP_KIT = "ferramentas de armadilhas";

		#endregion

		#region Item Names - Parts

		/// <summary>Item name: "engrenagens"</summary>
		public const string ITEM_GEARS = "engrenagens";

		/// <summary>Item name: "peças de relógio"</summary>
		public const string ITEM_CLOCK_PARTS = "peças de relógio";

		/// <summary>Item name: "torneira de barril"</summary>
		public const string ITEM_BARREL_TAP_ITEM = "torneira de barril";

		/// <summary>Item name: "molas"</summary>
		public const string ITEM_SPRINGS = "molas";

		/// <summary>Item name: "peças de sextante"</summary>
		public const string ITEM_SEXTANT_PARTS = "peças de sextante";

		/// <summary>Item name: "aros de barril"</summary>
		public const string ITEM_BARREL_HOOPS = "aros de barril";

		/// <summary>Item name: "dobradiça"</summary>
		public const string ITEM_HINGE = "dobradiça";

		/// <summary>Item name: "bola de bola"</summary>
		public const string ITEM_BOLA_BALL = "bola de bola";

		/// <summary>Item name: "eixo (recurso)"</summary>
		public const string ITEM_AXLE_RESOURCE = "eixo";

		/// <summary>Item name: "engrenagens de eixo"</summary>
		public const string ITEM_AXLE_GEARS = "engrenagens de eixo";

		/// <summary>Item name: "molas (recurso)"</summary>
		public const string ITEM_SPRINGS_RESOURCE = "molas";

		/// <summary>Item name: "dobradiça (recurso)"</summary>
		public const string ITEM_HINGE_RESOURCE = "dobradiça";

		/// <summary>Item name: "peças de relógio (recurso)"</summary>
		public const string ITEM_CLOCK_PARTS_RESOURCE = "peças de relógio";

		/// <summary>Item name: "estrutura de relógio (recurso)"</summary>
		public const string ITEM_CLOCK_FRAME_RESOURCE = "estrutura de relógio";

		/// <summary>Item name: "peças de sextante (recurso)"</summary>
		public const string ITEM_SEXTANT_PARTS_RESOURCE = "peças de sextante";

		/// <summary>Item name: "engrenagens (recurso)"</summary>
		public const string ITEM_GEARS_RESOURCE = "engrenagens";

		#endregion

		#region Item Names - Utensils

		/// <summary>Item name: "faca de açougueiro"</summary>
		public const string ITEM_BUTCHER_KNIFE = "faca de açougueiro";

		/// <summary>Item name: "colher (esquerda)"</summary>
		public const string ITEM_SPOON_LEFT = "colher (esquerda)";

		/// <summary>Item name: "colher (direita)"</summary>
		public const string ITEM_SPOON_RIGHT = "colher (direita)";

		/// <summary>Item name: "prato"</summary>
		public const string ITEM_PLATE = "prato";

		/// <summary>Item name: "garfo (esquerda)"</summary>
		public const string ITEM_FORK_LEFT = "garfo (esquerda)";

		/// <summary>Item name: "garfo (direita)"</summary>
		public const string ITEM_FORK_RIGHT = "garfo (direita)";

		/// <summary>Item name: "cutelo"</summary>
		public const string ITEM_CLEAVER = "cutelo";

		/// <summary>Item name: "faca (esquerda)"</summary>
		public const string ITEM_KNIFE_LEFT = "faca (esquerda)";

		/// <summary>Item name: "faca (direita)"</summary>
		public const string ITEM_KNIFE_RIGHT = "faca (direita)";

		/// <summary>Item name: "cálice"</summary>
		public const string ITEM_GOBLET = "cálice";

		/// <summary>Item name: "caneca de estanho"</summary>
		public const string ITEM_PEWTER_MUG = "caneca de estanho";

		/// <summary>Item name: "faca de esfolar (utensílios)"</summary>
		public const string ITEM_SKINNING_KNIFE_UTENSILS = "faca de esfolar";

		#endregion

		#region Item Names - Misc

		/// <summary>Item name: "vela (grande)"</summary>
		public const string ITEM_CANDLE_LARGE = "vela (grande)";

		/// <summary>Item name: "candelabro"</summary>
		public const string ITEM_CANDELABRA = "candelabro";

		/// <summary>Item name: "balança"</summary>
		public const string ITEM_SCALES = "balança";

		/// <summary>Item name: "chave"</summary>
		public const string ITEM_KEY = "chave";

		/// <summary>Item name: "chaveiro"</summary>
		public const string ITEM_KEY_RING = "chaveiro";

		/// <summary>Item name: "globo"</summary>
		public const string ITEM_GLOBE = "globo";

		/// <summary>Item name: "lanterna"</summary>
		public const string ITEM_LANTERN = "lanterna";

		/// <summary>Item name: "suporte de aquecimento"</summary>
		public const string ITEM_HEATING_STAND = "suporte de aquecimento";

		/// <summary>Item name: "lanterna shoji"</summary>
		public const string ITEM_SHOJI_LANTERN = "lanterna shoji";

		/// <summary>Item name: "lanterna de papel"</summary>
		public const string ITEM_PAPER_LANTERN = "lanterna de papel";

		/// <summary>Item name: "lanterna de papel redonda"</summary>
		public const string ITEM_ROUND_PAPER_LANTERN = "lanterna de papel redonda";

		/// <summary>Item name: "sino de vento"</summary>
		public const string ITEM_WIND_CHIMES = "sino de vento";

		/// <summary>Item name: "sino de vento elegante"</summary>
		public const string ITEM_FANCY_WIND_CHIMES = "sino de vento elegante";

		/// <summary>Item name: "telescópio"</summary>
		public const string ITEM_SPYGLASS = "telescópio";

		/// <summary>Item name: "tocha de parede"</summary>
		public const string ITEM_WALL_TORCH = "tocha de parede";

		/// <summary>Item name: "tocha de parede colorida"</summary>
		public const string ITEM_COLORED_WALL_TORCH = "tocha de parede colorida";

		#endregion

		#region Item Names - Multi-Component

		/// <summary>Item name: "engrenagens de eixo (item)"</summary>
		public const string ITEM_AXLE_GEARS_ITEM = "engrenagens de eixo";

		/// <summary>Item name: "relógio (direita)"</summary>
		public const string ITEM_CLOCK_RIGHT = "relógio (direita)";

		/// <summary>Item name: "relógio (esquerda)"</summary>
		public const string ITEM_CLOCK_LEFT = "relógio (esquerda)";

		/// <summary>Item name: "sextante"</summary>
		public const string ITEM_SEXTANT = "sextante";

		/// <summary>Item name: "bola"</summary>
		public const string ITEM_BOLA = "bola";

		/// <summary>Item name: "bola de bola (recurso)"</summary>
		public const string ITEM_BOLA_BALL_RESOURCE = "bola de bola";

		/// <summary>Item name: "barril de poção"</summary>
		public const string ITEM_POTION_KEG = "barril de poção";

		/// <summary>Error: "Falhou ao criar o item"</summary>
		public const string MSG_FAILED_CREATE = "Falhou ao criar o item";

		#endregion

		#region Item Names - Jewelry (Metal Types)

		/// <summary>Jewelry type: "amuleto"</summary>
		public const string JEWELRY_AMULET = "amuleto";

		/// <summary>Jewelry type: "bracelete"</summary>
		public const string JEWELRY_BRACELET = "bracelete";

		/// <summary>Jewelry type: "anel"</summary>
		public const string JEWELRY_RING = "anel";

		/// <summary>Jewelry type: "brincos"</summary>
		public const string JEWELRY_EARRINGS = "brincos";

		#endregion

		#region Item Names - Lockpicking Chests

		/// <summary>Lockpicking chest format: "baú de lockpicking {0}"</summary>
		public const string ITEM_LOCKPICKING_CHEST_FORMAT = "baú de lockpicking {0}";

		/// <summary>Item name: "baú de lockpicking 10"</summary>
		public const string ITEM_LOCKPICKING_CHEST_10 = "baú de lockpicking 10";

		/// <summary>Item name: "baú de lockpicking 20"</summary>
		public const string ITEM_LOCKPICKING_CHEST_20 = "baú de lockpicking 20";

		/// <summary>Item name: "baú de lockpicking 30"</summary>
		public const string ITEM_LOCKPICKING_CHEST_30 = "baú de lockpicking 30";

		/// <summary>Item name: "baú de lockpicking 40"</summary>
		public const string ITEM_LOCKPICKING_CHEST_40 = "baú de lockpicking 40";

		/// <summary>Item name: "baú de lockpicking 50"</summary>
		public const string ITEM_LOCKPICKING_CHEST_50 = "baú de lockpicking 50";

		/// <summary>Item name: "baú de lockpicking 60"</summary>
		public const string ITEM_LOCKPICKING_CHEST_60 = "baú de lockpicking 60";

		/// <summary>Item name: "baú de lockpicking 70"</summary>
		public const string ITEM_LOCKPICKING_CHEST_70 = "baú de lockpicking 70";

		/// <summary>Item name: "baú de lockpicking 80"</summary>
		public const string ITEM_LOCKPICKING_CHEST_80 = "baú de lockpicking 80";

		/// <summary>Item name: "baú de lockpicking 90"</summary>
		public const string ITEM_LOCKPICKING_CHEST_90 = "baú de lockpicking 90";

		#endregion

		#region Item Names - Hospitality

		/// <summary>Item name: "barril de cerveja"</summary>
		public const string ITEM_ALE_BARREL = "barril de cerveja";

		/// <summary>Item name: "prensa de queijo"</summary>
		public const string ITEM_CHEESE_PRESS = "prensa de queijo";

		/// <summary>Item name: "barril de cidra"</summary>
		public const string ITEM_CIDER_BARREL = "barril de cidra";

		/// <summary>Item name: "barril de licor"</summary>
		public const string ITEM_LIQUOR_BARREL = "barril de licor";

		/// <summary>Item name: "barril de vinho"</summary>
		public const string ITEM_WINE_BARREL = "barril de vinho";

		#endregion

		#region Resource Names

		/// <summary>Resource name: "Barril"</summary>
		public const string RESOURCE_KEG = "Barril";

		/// <summary>Resource name: "Caixa de Madeira"</summary>
		public const string RESOURCE_WOODEN_BOX = "Caixa de Madeira";

		#endregion

		#region Metal Type Names

		/// <summary>Metal type: "agapite"</summary>
		public const string METAL_AGAPITE = "agapite";

		/// <summary>Metal type: "ametista"</summary>
		public const string METAL_AMETHYST = "ametista";

		/// <summary>Metal type: "latão"</summary>
		public const string METAL_BRASS = "latão";

		/// <summary>Metal type: "bronze"</summary>
		public const string METAL_BRONZE = "bronze";

		/// <summary>Metal type: "caddellite"</summary>
		public const string METAL_CADDELLITE = "caddellite";

		/// <summary>Metal type: "cobre"</summary>
		public const string METAL_COPPER = "cobre";

		/// <summary>Metal type: "cobre opaco"</summary>
		public const string METAL_DULL_COPPER = "cobre opaco";

		/// <summary>Metal type: "anão"</summary>
		public const string METAL_DWARVEN = "anão";

		/// <summary>Metal type: "granada"</summary>
		public const string METAL_GARNET = "granada";

		/// <summary>Metal type: "ouro"</summary>
		public const string METAL_GOLD = "ouro";

		/// <summary>Metal type: "dourado"</summary>
		public const string METAL_GOLDEN = "dourado";

		/// <summary>Metal type: "jade"</summary>
		public const string METAL_JADE = "jade";

		/// <summary>Metal type: "mithril"</summary>
		public const string METAL_MITHRIL = "mithril";

		/// <summary>Metal type: "nepturite"</summary>
		public const string METAL_NEPTURITE = "nepturite";

		/// <summary>Metal type: "obsidiana"</summary>
		public const string METAL_OBSIDIAN = "obsidiana";

		/// <summary>Metal type: "ônix"</summary>
		public const string METAL_ONYX = "ônix";

		/// <summary>Metal type: "quartzo"</summary>
		public const string METAL_QUARTZ = "quartzo";

		/// <summary>Metal type: "ferro sombrio"</summary>
		public const string METAL_SHADOW_IRON = "ferro sombrio";

		/// <summary>Metal type: "prata"</summary>
		public const string METAL_SILVER = "prata";

		/// <summary>Metal type: "prateado"</summary>
		public const string METAL_SILVERY = "prateado";

		/// <summary>Metal type: "espinélio"</summary>
		public const string METAL_SPINEL = "espinélio";

		/// <summary>Metal type: "rubi estelar"</summary>
		public const string METAL_STAR_RUBY = "rubi estelar";

		/// <summary>Metal type: "aço"</summary>
		public const string METAL_STEEL = "aço";

		/// <summary>Metal type: "topázio"</summary>
		public const string METAL_TOPAZ = "topázio";

		/// <summary>Metal type: "valorite"</summary>
		public const string METAL_VALORITE = "valorite";

		/// <summary>Metal type: "verite"</summary>
		public const string METAL_VERITE = "verite";

		/// <summary>Metal type: "ferro"</summary>
		public const string METAL_IRON = "ferro";

		/// <summary>Metal type: "titânio"</summary>
		public const string METAL_TITANIUM = "titânio";

		/// <summary>Metal type: "rosenium"</summary>
		public const string METAL_ROSENIUM = "rosenium";

		/// <summary>Metal type: "platina"</summary>
		public const string METAL_PLATINUM = "platina";

		/// <summary>Metal type: "xormite"</summary>
		public const string METAL_XORMITE = "xormite";

		#endregion

		#region Gem Type Names

		/// <summary>Gem type: "âmbar"</summary>
		public const string GEM_AMBER = "âmbar";

		/// <summary>Gem type: "diamante"</summary>
		public const string GEM_DIAMOND = "diamante";

		/// <summary>Gem type: "esmeralda"</summary>
		public const string GEM_EMERALD = "esmeralda";

		/// <summary>Gem type: "pérola"</summary>
		public const string GEM_PEARL = "pérola";

		/// <summary>Gem type: "rubi"</summary>
		public const string GEM_RUBY = "rubi";

		/// <summary>Gem type: "safira"</summary>
		public const string GEM_SAPPHIRE = "safira";

		/// <summary>Gem type: "safira estelar"</summary>
		public const string GEM_STAR_SAPPHIRE = "safira estelar";

		/// <summary>Gem type: "turmalina"</summary>
		public const string GEM_TOURMALINE = "turmalina";

		#endregion

		#region Ingot Type Names

		/// <summary>Ingot name: "lingote de agapite"</summary>
		public const string INGOT_AGAPITE = "lingote de agapite";

		/// <summary>Ingot name: "lingote de ametista"</summary>
		public const string INGOT_AMETHYST = "lingote de ametista";

		/// <summary>Ingot name: "lingote de latão"</summary>
		public const string INGOT_BRASS = "lingote de latão";

		/// <summary>Ingot name: "lingote de bronze"</summary>
		public const string INGOT_BRONZE = "lingote de bronze";

		/// <summary>Ingot name: "lingote de caddellite"</summary>
		public const string INGOT_CADDELLITE = "lingote de caddellite";

		/// <summary>Ingot name: "lingote de cobre"</summary>
		public const string INGOT_COPPER = "lingote de cobre";

		/// <summary>Ingot name: "lingote de cobre opaco"</summary>
		public const string INGOT_DULL_COPPER = "lingote de cobre opaco";

		/// <summary>Ingot name: "lingote de anão"</summary>
		public const string INGOT_DWARVEN = "lingote de anão";

		/// <summary>Ingot name: "lingote de granada"</summary>
		public const string INGOT_GARNET = "lingote de granada";

		/// <summary>Ingot name: "lingote de ouro"</summary>
		public const string INGOT_GOLD = "lingote de ouro";

		/// <summary>Ingot name: "lingote de ferro"</summary>
		public const string INGOT_IRON = "lingote de ferro";

		/// <summary>Ingot name: "lingote de jade"</summary>
		public const string INGOT_JADE = "lingote de jade";

		/// <summary>Ingot name: "lingote de mithril"</summary>
		public const string INGOT_MITHRIL = "lingote de mithril";

		/// <summary>Ingot name: "lingote de nepturite"</summary>
		public const string INGOT_NEPTURITE = "lingote de nepturite";

		/// <summary>Ingot name: "lingote de obsidiana"</summary>
		public const string INGOT_OBSIDIAN = "lingote de obsidiana";

		/// <summary>Ingot name: "lingote de ônix"</summary>
		public const string INGOT_ONYX = "lingote de ônix";

		/// <summary>Ingot name: "lingote de quartzo"</summary>
		public const string INGOT_QUARTZ = "lingote de quartzo";

		/// <summary>Ingot name: "lingote de ferro sombrio"</summary>
		public const string INGOT_SHADOW_IRON = "lingote de ferro sombrio";

		/// <summary>Ingot name: "lingote de prata"</summary>
		public const string INGOT_SILVER = "lingote de prata";

		/// <summary>Ingot name: "lingote de espinélio"</summary>
		public const string INGOT_SPINEL = "lingote de espinélio";

		/// <summary>Ingot name: "lingote de rubi estelar"</summary>
		public const string INGOT_STAR_RUBY = "lingote de rubi estelar";

		/// <summary>Ingot name: "lingote de aço"</summary>
		public const string INGOT_STEEL = "lingote de aço";

		/// <summary>Ingot name: "lingote de topázio"</summary>
		public const string INGOT_TOPAZ = "lingote de topázio";

		/// <summary>Ingot name: "lingote de valorite"</summary>
		public const string INGOT_VALORITE = "lingote de valorite";

		/// <summary>Ingot name: "lingote de verite"</summary>
		public const string INGOT_VERITE = "lingote de verite";

		/// <summary>Ingot name: "lingote de titânio"</summary>
		public const string INGOT_TITANIUM = "lingote de titânio";

		/// <summary>Ingot name: "lingote de rosenium"</summary>
		public const string INGOT_ROSENIUM = "lingote de rosenium";

		/// <summary>Ingot name: "lingote de platina"</summary>
		public const string INGOT_PLATINUM = "lingote de platina";

		#endregion

		#region Error Messages

		/// <summary>Error: "Você não tem material suficiente."</summary>
		public const string MSG_NOT_ENOUGH_MATERIAL = "Você não tem material suficiente.";

		/// <summary>Message: "Selecione o tipo de material"</summary>
		public const string MSG_MATERIAL_SELECTION = "Selecione o tipo de material";

		#endregion

		#region Helper Methods

		/// <summary>
		/// Gets the jewelry name by combining metal/gem type with jewelry type
		/// </summary>
		/// <param name="materialName">The material name (e.g., "agapite", "âmbar")</param>
		/// <param name="jewelryType">The jewelry type ("amulet", "bracelet", "ring", "earrings")</param>
		/// <returns>Full jewelry name in PT-BR</returns>
		public static string GetJewelryName(string materialName, string jewelryType)
		{
			string jewelryTypePTBR;
			switch (jewelryType)
			{
				case "amulet":
					jewelryTypePTBR = JEWELRY_AMULET;
					break;
				case "bracelet":
					jewelryTypePTBR = JEWELRY_BRACELET;
					break;
				case "ring":
					jewelryTypePTBR = JEWELRY_RING;
					break;
				case "earrings":
					jewelryTypePTBR = JEWELRY_EARRINGS;
					break;
				default:
					jewelryTypePTBR = jewelryType;
					break;
			}

			return string.Format("{0} de {1}", jewelryTypePTBR, materialName);
		}

		/// <summary>
		/// Gets the ingot name for a metal type
		/// </summary>
		/// <param name="metalName">The metal name (e.g., "agapite", "bronze")</param>
		/// <returns>Ingot name in PT-BR</returns>
		public static string GetIngotName(string metalName)
		{
			// Map metal names to ingot names
			switch (metalName)
			{
				case "agapite": return INGOT_AGAPITE;
				case "amethyst": return INGOT_AMETHYST;
				case "brass": return INGOT_BRASS;
				case "bronze": return INGOT_BRONZE;
				case "caddellite": return INGOT_CADDELLITE;
				case "copper": return INGOT_COPPER;
				case "dull copper": return INGOT_DULL_COPPER;
				case "dwarven": return INGOT_DWARVEN;
				case "garnet": return INGOT_GARNET;
				case "gold": return INGOT_GOLD;
				case "golden": return INGOT_GOLD; // golden uses gold ingot
				case "iron": return INGOT_IRON;
				case "jade": return INGOT_JADE;
				case "mithril": return INGOT_MITHRIL;
				case "nepturite": return INGOT_NEPTURITE;
				case "obsidian": return INGOT_OBSIDIAN;
				case "onyx": return INGOT_ONYX;
				case "quartz": return INGOT_QUARTZ;
				case "shadow iron": return INGOT_SHADOW_IRON;
				case "silver": return INGOT_SILVER;
				case "silvery": return INGOT_SILVER; // silvery uses silver ingot
				case "spinel": return INGOT_SPINEL;
				case "star ruby": return INGOT_STAR_RUBY;
				case "steel": return INGOT_STEEL;
				case "topaz": return INGOT_TOPAZ;
				case "valorite": return INGOT_VALORITE;
				case "verite": return INGOT_VERITE;
				default: return "lingote de " + metalName;
			}
		}

		/// <summary>
		/// Gets the metal name in PT-BR for jewelry
		/// </summary>
		/// <param name="metalType">The metal type in English</param>
		/// <returns>Metal name in PT-BR</returns>
		public static string GetMetalNamePTBR(string metalType)
		{
			switch (metalType)
			{
				case "agapite": return METAL_AGAPITE;
				case "amethyst": return METAL_AMETHYST;
				case "brass": return METAL_BRASS;
				case "bronze": return METAL_BRONZE;
				case "caddellite": return METAL_CADDELLITE;
				case "copper": return METAL_COPPER;
				case "dull copper": return METAL_DULL_COPPER;
				case "dwarven": return METAL_DWARVEN;
				case "garnet": return METAL_GARNET;
				case "gold": return METAL_GOLD;
				case "golden": return METAL_GOLDEN;
				case "jade": return METAL_JADE;
				case "mithril": return METAL_MITHRIL;
				case "nepturite": return METAL_NEPTURITE;
				case "obsidian": return METAL_OBSIDIAN;
				case "onyx": return METAL_ONYX;
				case "quartz": return METAL_QUARTZ;
				case "shadow iron": return METAL_SHADOW_IRON;
				case "silver": return METAL_SILVER;
				case "silvery": return METAL_SILVERY;
				case "spinel": return METAL_SPINEL;
				case "star ruby": return METAL_STAR_RUBY;
				case "steel": return METAL_STEEL;
				case "topaz": return METAL_TOPAZ;
				case "valorite": return METAL_VALORITE;
				case "verite": return METAL_VERITE;
				default: return metalType;
			}
		}

		/// <summary>
		/// Gets the gem name in PT-BR
		/// </summary>
		/// <param name="gemType">The gem type in English</param>
		/// <returns>Gem name in PT-BR</returns>
		public static string GetGemNamePTBR(string gemType)
		{
			switch (gemType)
			{
				case "amber": return GEM_AMBER;
				case "diamond": return GEM_DIAMOND;
				case "emerald": return GEM_EMERALD;
				case "pearl": return GEM_PEARL;
				case "ruby": return GEM_RUBY;
				case "sapphire": return GEM_SAPPHIRE;
				case "star sapphire": return GEM_STAR_SAPPHIRE;
				case "tourmaline": return GEM_TOURMALINE;
				default: return gemType;
			}
		}

		#endregion
	}
}

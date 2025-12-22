namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for Tinkering crafting system calculations and mechanics.
	/// Extracted from DefTinkering.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class TinkeringConstants
	{
		#region Craft System Configuration

		/// <summary>Minimum craft effect value</summary>
		public const int MIN_CRAFT_EFFECT = 1;

		/// <summary>Maximum craft effect value</summary>
		public const int MAX_CRAFT_EFFECT = 1;

		/// <summary>Craft delay multiplier</summary>
		public const double CRAFT_DELAY_MULTIPLIER = 1.25;

		/// <summary>Sound ID for tinkering craft effect (0x542)</summary>
		public const int SOUND_TINKERING_CRAFT = 0x542;

		/// <summary>Chance at minimum skill for special items (potion keg, faction trap kit)</summary>
		public const double CHANCE_AT_MIN_SPECIAL = 0.5;

		/// <summary>Chance at minimum skill for standard items</summary>
		public const double CHANCE_AT_MIN_STANDARD = 0.0;

		#endregion

		#region Message Numbers (Cliloc)

		/// <summary>Gump title: "MENU DE INVENTARIA" (cliloc 1044007)</summary>
		public const int MSG_GUMP_TITLE = 1044007;

		/// <summary>Message: "Você quebrou sua ferramenta!"</summary>
		public const string MSG_TOOL_WORN_OUT = TinkeringStringConstants.MSG_TOOL_WORN_OUT;

		/// <summary>Message: "A ferramenta deve estar com você para usar."</summary>
		public const string MSG_TOOL_MUST_BE_ON_PERSON = TinkeringStringConstants.MSG_TOOL_MUST_BE_ON_PERSON;

		/// <summary>Message: "Você falhou ao criar o item, e alguns de seus materiais foram perdidos."</summary>
		public const string MSG_FAILED_LOST_MATERIALS = TinkeringStringConstants.MSG_FAILED_LOST_MATERIALS;

		/// <summary>Message: "Você falhou ao criar o item, mas nenhum material foi perdido."</summary>
		public const string MSG_FAILED_NO_MATERIALS_LOST = TinkeringStringConstants.MSG_FAILED_NO_MATERIALS_LOST;

		/// <summary>Message: "Você mal conseguiu fazer este item. Sua qualidade está abaixo da média."</summary>
		public const string MSG_BARELY_MADE_ITEM = TinkeringStringConstants.MSG_BARELY_MADE_ITEM;

		/// <summary>Message: "Você cria um item de qualidade excepcional e assina a sua marca nele."</summary>
		public const string MSG_EXCEPTIONAL_WITH_MARK = TinkeringStringConstants.MSG_EXCEPTIONAL_WITH_MARK;

		/// <summary>Message: "Você cria um item de qualidade excepcional."</summary>
		public const string MSG_EXCEPTIONAL_QUALITY = TinkeringStringConstants.MSG_EXCEPTIONAL_QUALITY;

		/// <summary>Message: "Você cria o item."</summary>
		public const string MSG_ITEM_CREATED = TinkeringStringConstants.MSG_ITEM_CREATED;

		/// <summary>Resource name: "Lingotes de Ferro"</summary>
		public const string MSG_IRON_INGOTS = TinkeringStringConstants.RESOURCE_IRON_INGOTS;

		/// <summary>Error: "Você não tem lingotes suficientes."</summary>
		public const string MSG_INSUFFICIENT_INGOTS = TinkeringStringConstants.MSG_INSUFFICIENT_INGOTS;

		/// <summary>Resource name: "Toras"</summary>
		public const string MSG_LOGS = TinkeringStringConstants.RESOURCE_LOGS;

		/// <summary>Error: "Você não tem toras suficientes."</summary>
		public const string MSG_INSUFFICIENT_LOGS = TinkeringStringConstants.MSG_INSUFFICIENT_LOGS;

		/// <summary>Error: "Você não tem os itens necessários."</summary>
		public const string MSG_INSUFFICIENT_RESOURCES = TinkeringStringConstants.MSG_INSUFFICIENT_RESOURCES;

		/// <summary>Resource name: "Granito"</summary>
		public const string MSG_GRANITE = TinkeringStringConstants.RESOURCE_GRANITE;

		/// <summary>Error: "Você não tem granito suficiente."</summary>
		public const string MSG_INSUFFICIENT_GRANITE = TinkeringStringConstants.MSG_INSUFFICIENT_GRANITE;

		/// <summary>Resource name: "Couro"</summary>
		public const string MSG_LEATHER = TinkeringStringConstants.RESOURCE_LEATHER;

		/// <summary>Error: "Você não tem couro suficiente."</summary>
		public const string MSG_INSUFFICIENT_LEATHER = TinkeringStringConstants.MSG_INSUFFICIENT_LEATHER;

		/// <summary>Resource name: "Cera de Abelha"</summary>
		public const string MSG_BEESWAX = TinkeringStringConstants.RESOURCE_BEESWAX;

		/// <summary>Error: "Você não tem cera de abelha suficiente."</summary>
		public const string MSG_INSUFFICIENT_BEESWAX = TinkeringStringConstants.MSG_INSUFFICIENT_BEESWAX;

		/// <summary>Resource name: "Gema Arcana"</summary>
		public const string MSG_ARCANE_GEM = TinkeringStringConstants.RESOURCE_ARCANE_GEM;

		/// <summary>Resource name: "Garrafa"</summary>
		public const string MSG_BOTTLE = TinkeringStringConstants.RESOURCE_BOTTLE;

		/// <summary>Resource name: "Tampa de Barril"</summary>
		public const string MSG_BARREL_LID = TinkeringStringConstants.RESOURCE_BARREL_LID;

		/// <summary>Resource name: "Torneira de Barril"</summary>
		public const string MSG_BARREL_TAP = TinkeringStringConstants.RESOURCE_BARREL_TAP;

		/// <summary>Resource name: "Tocha"</summary>
		public const string MSG_TORCH = TinkeringStringConstants.RESOURCE_TORCH;

		#endregion

		#region Craft Group Names (PT-BR)

		/// <summary>Group: "Itens de Madeira"</summary>
		public const string MSG_GROUP_WOODEN_ITEMS = TinkeringStringConstants.GROUP_WOODEN_ITEMS;

		/// <summary>Group: "Ferramentas"</summary>
		public const string MSG_GROUP_TOOLS = TinkeringStringConstants.GROUP_TOOLS;

		/// <summary>Group: "Peças"</summary>
		public const string MSG_GROUP_PARTS = TinkeringStringConstants.GROUP_PARTS;

		/// <summary>Group: "Utensílios"</summary>
		public const string MSG_GROUP_UTENSILS = TinkeringStringConstants.GROUP_UTENSILS;

		/// <summary>Group: "Joias"</summary>
		public const string MSG_GROUP_JEWELRY = TinkeringStringConstants.GROUP_JEWELRY;

		/// <summary>Group: "Variados"</summary>
		public const string MSG_GROUP_MISC = TinkeringStringConstants.GROUP_MISC;

		/// <summary>Group: "Montagens"</summary>
		public const string MSG_GROUP_MULTI_COMPONENT = TinkeringStringConstants.GROUP_MULTI_COMPONENT;

		#endregion

		#region Item Names (PT-BR) - Wooden Items

		/// <summary>Jointing Plane</summary>
		public const string MSG_JOINTING_PLANE = TinkeringStringConstants.ITEM_JOINTING_PLANE;

		/// <summary>Moulding Plane</summary>
		public const string MSG_MOULDING_PLANE = TinkeringStringConstants.ITEM_MOULDING_PLANE;

		/// <summary>Smoothing Plane</summary>
		public const string MSG_SMOOTHING_PLANE = TinkeringStringConstants.ITEM_SMOOTHING_PLANE;

		/// <summary>Clock Frame</summary>
		public const string MSG_CLOCK_FRAME = TinkeringStringConstants.ITEM_CLOCK_FRAME;

		/// <summary>Axle</summary>
		public const string MSG_AXLE = TinkeringStringConstants.ITEM_AXLE;

		/// <summary>Rolling Pin</summary>
		public const string MSG_ROLLING_PIN = TinkeringStringConstants.ITEM_ROLLING_PIN;

		/// <summary>Nunchaku</summary>
		public const string MSG_NUNCHAKU = TinkeringStringConstants.ITEM_NUNCHAKU;

		#endregion

		#region Item Names (PT-BR) - Tools

		/// <summary>Scissors</summary>
		public const string MSG_SCISSORS = TinkeringStringConstants.ITEM_SCISSORS;

		/// <summary>Mortar and Pestle</summary>
		public const string MSG_MORTAR_PESTLE = TinkeringStringConstants.ITEM_MORTAR_PESTLE;

		/// <summary>Scorp</summary>
		public const string MSG_SCORP = TinkeringStringConstants.ITEM_SCORP;

		/// <summary>Tinker Tools</summary>
		public const string MSG_TINKER_TOOLS = TinkeringStringConstants.ITEM_TINKER_TOOLS;

		/// <summary>Hatchet</summary>
		public const string MSG_HATCHET = TinkeringStringConstants.ITEM_HATCHET;

		/// <summary>Draw Knife</summary>
		public const string MSG_DRAW_KNIFE = TinkeringStringConstants.ITEM_DRAW_KNIFE;

		/// <summary>Sewing Kit</summary>
		public const string MSG_SEWING_KIT = TinkeringStringConstants.ITEM_SEWING_KIT;

		/// <summary>Saw</summary>
		public const string MSG_SAW = TinkeringStringConstants.ITEM_SAW;

		/// <summary>Dovetail Saw</summary>
		public const string MSG_DOVETAIL_SAW = TinkeringStringConstants.ITEM_DOVETAIL_SAW;

		/// <summary>Froe</summary>
		public const string MSG_FROE = TinkeringStringConstants.ITEM_FROE;

		/// <summary>Shovel</summary>
		public const string MSG_SHOVEL = TinkeringStringConstants.ITEM_SHOVEL;

		/// <summary>Hammer</summary>
		public const string MSG_HAMMER = TinkeringStringConstants.ITEM_HAMMER;

		/// <summary>Tongs</summary>
		public const string MSG_TONGS = TinkeringStringConstants.ITEM_TONGS;

		/// <summary>Smith Hammer</summary>
		public const string MSG_SMITH_HAMMER = TinkeringStringConstants.ITEM_SMITH_HAMMER;

		/// <summary>Sledge Hammer</summary>
		public const string MSG_SLEDGE_HAMMER = TinkeringStringConstants.ITEM_SLEDGE_HAMMER;

		/// <summary>Inshave</summary>
		public const string MSG_INSHAVE = TinkeringStringConstants.ITEM_INSHAVE;

		/// <summary>Pickaxe</summary>
		public const string MSG_PICKAXE = TinkeringStringConstants.ITEM_PICKAXE;

		/// <summary>Lockpick</summary>
		public const string MSG_LOCKPICK = TinkeringStringConstants.ITEM_LOCKPICK;

		/// <summary>Skillet</summary>
		public const string MSG_SKILLET = TinkeringStringConstants.ITEM_SKILLET;

		/// <summary>Flour Sifter</summary>
		public const string MSG_FLOUR_SIFTER = TinkeringStringConstants.ITEM_FLOUR_SIFTER;

		/// <summary>Fletcher Tools</summary>
		public const string MSG_FLETCHER_TOOLS = TinkeringStringConstants.ITEM_FLETCHER_TOOLS;

		/// <summary>Mapmaker's Pen</summary>
		public const string MSG_MAPMAKERS_PEN = TinkeringStringConstants.ITEM_MAPMAKERS_PEN;

		/// <summary>Scribe's Pen</summary>
		public const string MSG_SCRIBES_PEN = TinkeringStringConstants.ITEM_SCRIBES_PEN;

		#endregion

		#region Item Names (PT-BR) - Parts

		/// <summary>Gears</summary>
		public const string MSG_GEARS = TinkeringStringConstants.ITEM_GEARS;

		/// <summary>Clock Parts</summary>
		public const string MSG_CLOCK_PARTS = TinkeringStringConstants.ITEM_CLOCK_PARTS;

		/// <summary>Barrel Tap</summary>
		public const string MSG_BARREL_TAP_ITEM = TinkeringStringConstants.ITEM_BARREL_TAP_ITEM;

		/// <summary>Springs</summary>
		public const string MSG_SPRINGS = TinkeringStringConstants.ITEM_SPRINGS;

		/// <summary>Sextant Parts</summary>
		public const string MSG_SEXTANT_PARTS = TinkeringStringConstants.ITEM_SEXTANT_PARTS;

		/// <summary>Barrel Hoops</summary>
		public const string MSG_BARREL_HOOPS = TinkeringStringConstants.ITEM_BARREL_HOOPS;

		/// <summary>Hinge</summary>
		public const string MSG_HINGE = TinkeringStringConstants.ITEM_HINGE;

		/// <summary>Bola Ball</summary>
		public const string MSG_BOLA_BALL = TinkeringStringConstants.ITEM_BOLA_BALL;

		/// <summary>Axle (resource)</summary>
		public const string MSG_AXLE_RESOURCE = TinkeringStringConstants.ITEM_AXLE_RESOURCE;

		/// <summary>Axle Gears</summary>
		public const string MSG_AXLE_GEARS = TinkeringStringConstants.ITEM_AXLE_GEARS;

		/// <summary>Springs (resource)</summary>
		public const string MSG_SPRINGS_RESOURCE = TinkeringStringConstants.ITEM_SPRINGS_RESOURCE;

		/// <summary>Hinge (resource)</summary>
		public const string MSG_HINGE_RESOURCE = TinkeringStringConstants.ITEM_HINGE_RESOURCE;

		/// <summary>Clock Parts (resource)</summary>
		public const string MSG_CLOCK_PARTS_RESOURCE = TinkeringStringConstants.ITEM_CLOCK_PARTS_RESOURCE;

		/// <summary>Clock Frame (resource)</summary>
		public const string MSG_CLOCK_FRAME_RESOURCE = TinkeringStringConstants.ITEM_CLOCK_FRAME_RESOURCE;

		/// <summary>Sextant Parts (resource)</summary>
		public const string MSG_SEXTANT_PARTS_RESOURCE = TinkeringStringConstants.ITEM_SEXTANT_PARTS_RESOURCE;

		/// <summary>Gears (resource)</summary>
		public const string MSG_GEARS_RESOURCE = TinkeringStringConstants.ITEM_GEARS_RESOURCE;

		#endregion

		#region Item Names (PT-BR) - Utensils

		/// <summary>Butcher Knife</summary>
		public const string MSG_BUTCHER_KNIFE = TinkeringStringConstants.ITEM_BUTCHER_KNIFE;

		/// <summary>Spoon (Left)</summary>
		public const string MSG_SPOON_LEFT = TinkeringStringConstants.ITEM_SPOON_LEFT;

		/// <summary>Spoon (Right)</summary>
		public const string MSG_SPOON_RIGHT = TinkeringStringConstants.ITEM_SPOON_RIGHT;

		/// <summary>Plate</summary>
		public const string MSG_PLATE = TinkeringStringConstants.ITEM_PLATE;

		/// <summary>Fork (Left)</summary>
		public const string MSG_FORK_LEFT = TinkeringStringConstants.ITEM_FORK_LEFT;

		/// <summary>Fork (Right)</summary>
		public const string MSG_FORK_RIGHT = TinkeringStringConstants.ITEM_FORK_RIGHT;

		/// <summary>Cleaver</summary>
		public const string MSG_CLEAVER = TinkeringStringConstants.ITEM_CLEAVER;

		/// <summary>Knife (Left)</summary>
		public const string MSG_KNIFE_LEFT = TinkeringStringConstants.ITEM_KNIFE_LEFT;

		/// <summary>Knife (Right)</summary>
		public const string MSG_KNIFE_RIGHT = TinkeringStringConstants.ITEM_KNIFE_RIGHT;

		/// <summary>Goblet</summary>
		public const string MSG_GOBLET = TinkeringStringConstants.ITEM_GOBLET;

		/// <summary>Pewter Mug</summary>
		public const string MSG_PEWTER_MUG = TinkeringStringConstants.ITEM_PEWTER_MUG;

		/// <summary>Skinning Knife</summary>
		public const string MSG_SKINNING_KNIFE = TinkeringStringConstants.ITEM_SKINNING_KNIFE_UTENSILS;

		#endregion

		#region Item Names (PT-BR) - Misc

		/// <summary>Candle (Large)</summary>
		public const string MSG_CANDLE_LARGE = TinkeringStringConstants.ITEM_CANDLE_LARGE;

		/// <summary>Candelabra</summary>
		public const string MSG_CANDELABRA = TinkeringStringConstants.ITEM_CANDELABRA;

		/// <summary>Scales</summary>
		public const string MSG_SCALES = TinkeringStringConstants.ITEM_SCALES;

		/// <summary>Key</summary>
		public const string MSG_KEY = TinkeringStringConstants.ITEM_KEY;

		/// <summary>Key Ring</summary>
		public const string MSG_KEY_RING = TinkeringStringConstants.ITEM_KEY_RING;

		/// <summary>Globe</summary>
		public const string MSG_GLOBE = TinkeringStringConstants.ITEM_GLOBE;

		/// <summary>Lantern</summary>
		public const string MSG_LANTERN = TinkeringStringConstants.ITEM_LANTERN;

		/// <summary>Heating Stand</summary>
		public const string MSG_HEATING_STAND = TinkeringStringConstants.ITEM_HEATING_STAND;

		/// <summary>Shoji Lantern</summary>
		public const string MSG_SHOJI_LANTERN = TinkeringStringConstants.ITEM_SHOJI_LANTERN;

		/// <summary>Paper Lantern</summary>
		public const string MSG_PAPER_LANTERN = TinkeringStringConstants.ITEM_PAPER_LANTERN;

		/// <summary>Round Paper Lantern</summary>
		public const string MSG_ROUND_PAPER_LANTERN = TinkeringStringConstants.ITEM_ROUND_PAPER_LANTERN;

		/// <summary>Wind Chimes</summary>
		public const string MSG_WIND_CHIMES = TinkeringStringConstants.ITEM_WIND_CHIMES;

		/// <summary>Fancy Wind Chimes</summary>
		public const string MSG_FANCY_WIND_CHIMES = TinkeringStringConstants.ITEM_FANCY_WIND_CHIMES;

		#endregion

		#region Item Names (PT-BR) - Multi-Component

		/// <summary>Axle Gears</summary>
		public const string MSG_AXLE_GEARS_ITEM = TinkeringStringConstants.ITEM_AXLE_GEARS_ITEM;

		/// <summary>Clock (Right)</summary>
		public const string MSG_CLOCK_RIGHT = TinkeringStringConstants.ITEM_CLOCK_RIGHT;

		/// <summary>Clock (Left)</summary>
		public const string MSG_CLOCK_LEFT = TinkeringStringConstants.ITEM_CLOCK_LEFT;

		/// <summary>Sextant</summary>
		public const string MSG_SEXTANT = TinkeringStringConstants.ITEM_SEXTANT;

		/// <summary>Bola</summary>
		public const string MSG_BOLA = TinkeringStringConstants.ITEM_BOLA;

		/// <summary>Bola Ball (resource)</summary>
		public const string MSG_BOLA_BALL_RESOURCE = TinkeringStringConstants.ITEM_BOLA_BALL_RESOURCE;

		/// <summary>Potion Keg</summary>
		public const string MSG_POTION_KEG = TinkeringStringConstants.ITEM_POTION_KEG;

		/// <summary>Failed to create item</summary>
		public const string MSG_FAILED_CREATE = TinkeringStringConstants.MSG_FAILED_CREATE;

		#endregion

		#region Metal Type Names (PT-BR)

		/// <summary>Iron</summary>
		public const string MSG_METAL_IRON = TinkeringStringConstants.METAL_IRON;

		/// <summary>Dull Copper</summary>
		public const string MSG_METAL_DULL_COPPER = TinkeringStringConstants.METAL_DULL_COPPER;

		/// <summary>Shadow Iron</summary>
		public const string MSG_METAL_SHADOW_IRON = TinkeringStringConstants.METAL_SHADOW_IRON;

		/// <summary>Copper</summary>
		public const string MSG_METAL_COPPER = TinkeringStringConstants.METAL_COPPER;

		/// <summary>Bronze</summary>
		public const string MSG_METAL_BRONZE = TinkeringStringConstants.METAL_BRONZE;

		/// <summary>Platinum</summary>
		public const string MSG_METAL_PLATINUM = TinkeringStringConstants.METAL_PLATINUM;

		/// <summary>Gold</summary>
		public const string MSG_METAL_GOLD = TinkeringStringConstants.METAL_GOLD;

		/// <summary>Agapite</summary>
		public const string MSG_METAL_AGAPITE = TinkeringStringConstants.METAL_AGAPITE;

		/// <summary>Verite</summary>
		public const string MSG_METAL_VERITE = TinkeringStringConstants.METAL_VERITE;

		/// <summary>Valorite</summary>
		public const string MSG_METAL_VALORITE = TinkeringStringConstants.METAL_VALORITE;

		/// <summary>Titanium</summary>
		public const string MSG_METAL_TITANIUM = TinkeringStringConstants.METAL_TITANIUM;

		/// <summary>Rosenium</summary>
		public const string MSG_METAL_ROSENIUM = TinkeringStringConstants.METAL_ROSENIUM;
	

		/// <summary>Nepturite</summary>
		public const string MSG_METAL_NEPTURITE = TinkeringStringConstants.METAL_NEPTURITE;

		/// <summary>Obsidian</summary>
		public const string MSG_METAL_OBSIDIAN = TinkeringStringConstants.METAL_OBSIDIAN;

		/// <summary>Steel</summary>
		public const string MSG_METAL_STEEL = TinkeringStringConstants.METAL_STEEL;

		/// <summary>Brass</summary>
		public const string MSG_METAL_BRASS = TinkeringStringConstants.METAL_BRASS;

		/// <summary>Mithril</summary>
		public const string MSG_METAL_MITHRIL = TinkeringStringConstants.METAL_MITHRIL;

		/// <summary>Xormite</summary>
		public const string MSG_METAL_XORMITE = TinkeringStringConstants.METAL_XORMITE;

		/// <summary>Dwarven</summary>
		public const string MSG_METAL_DWARVEN = TinkeringStringConstants.METAL_DWARVEN;

		/// <summary>Not enough material</summary>
		public const string MSG_NOT_ENOUGH_MATERIAL = TinkeringStringConstants.MSG_NOT_ENOUGH_MATERIAL;

		/// <summary>Material type selection</summary>
		public const string MSG_MATERIAL_SELECTION = TinkeringStringConstants.MSG_MATERIAL_SELECTION;

		/// <summary>Dull Copper Ingot</summary>
		public const string MSG_DULL_COPPER_INGOT = TinkeringStringConstants.INGOT_DULL_COPPER;

		/// <summary>Shadow Iron Ingot</summary>
		public const string MSG_SHADOW_IRON_INGOT = TinkeringStringConstants.INGOT_SHADOW_IRON;

		/// <summary>Bronze Ingot</summary>
		public const string MSG_BRONZE_INGOT = TinkeringStringConstants.INGOT_BRONZE;

		/// <summary>Gold Ingot</summary>
		public const string MSG_GOLD_INGOT = TinkeringStringConstants.INGOT_GOLD;

		/// <summary>Agapite Ingot</summary>
		public const string MSG_AGAPITE_INGOT = TinkeringStringConstants.INGOT_AGAPITE;

		/// <summary>Verite Ingot</summary>
		public const string MSG_VERITE_INGOT = TinkeringStringConstants.INGOT_VERITE;

		/// <summary>Valorite Ingot</summary>
		public const string MSG_VALORITE_INGOT = TinkeringStringConstants.INGOT_VALORITE;

		#endregion

		#region Quality Levels

		/// <summary>Quality level: Below average (0)</summary>
		public const int QUALITY_BELOW_AVERAGE = 0;

		/// <summary>Quality level: Exceptional (2)</summary>
		public const int QUALITY_EXCEPTIONAL = 2;

		#endregion

		#region Skill Requirements - Wooden Items

		/// <summary>Minimum skill for planes</summary>
		public const double SKILL_MIN_PLANES = 0.0;

		/// <summary>Maximum skill for planes</summary>
		public const double SKILL_MAX_PLANES = 50.0;

		/// <summary>Minimum skill for clock frame</summary>
		public const double SKILL_MIN_CLOCK_FRAME = 0.0;

		/// <summary>Maximum skill for clock frame</summary>
		public const double SKILL_MAX_CLOCK_FRAME = 50.0;

		/// <summary>Minimum skill for axle</summary>
		public const double SKILL_MIN_AXLE = -25.0;

		/// <summary>Maximum skill for axle</summary>
		public const double SKILL_MAX_AXLE = 25.0;

		/// <summary>Minimum skill for rolling pin</summary>
		public const double SKILL_MIN_ROLLING_PIN = 0.0;

		/// <summary>Maximum skill for rolling pin</summary>
		public const double SKILL_MAX_ROLLING_PIN = 50.0;

		/// <summary>Minimum skill for saw mill</summary>
		public const double SKILL_MIN_SAW_MILL = 60.0;

		/// <summary>Maximum skill for saw mill</summary>
		public const double SKILL_MAX_SAW_MILL = 120.0;

		/// <summary>Minimum lumberjacking skill for saw mill</summary>
		public const double SKILL_MIN_LUMBERJACKING_SAW_MILL = 75.0;

		/// <summary>Maximum lumberjacking skill for saw mill</summary>
		public const double SKILL_MAX_LUMBERJACKING_SAW_MILL = 80.0;

		/// <summary>Minimum skill for nunchaku</summary>
		public const double SKILL_MIN_NUNCHAKU = 70.0;

		/// <summary>Maximum skill for nunchaku</summary>
		public const double SKILL_MAX_NUNCHAKU = 120.0;

		#endregion

		#region Skill Requirements - Tools

		/// <summary>Minimum skill for scissors</summary>
		public const double SKILL_MIN_SCISSORS = 5.0;

		/// <summary>Maximum skill for scissors</summary>
		public const double SKILL_MAX_SCISSORS = 55.0;

		/// <summary>Minimum skill for mortar and pestle</summary>
		public const double SKILL_MIN_MORTAR_PESTLE = 20.0;

		/// <summary>Maximum skill for mortar and pestle</summary>
		public const double SKILL_MAX_MORTAR_PESTLE = 70.0;

		/// <summary>Minimum skill for scorp</summary>
		public const double SKILL_MIN_SCORP = 30.0;

		/// <summary>Maximum skill for scorp</summary>
		public const double SKILL_MAX_SCORP = 80.0;

		/// <summary>Minimum skill for tinker tools</summary>
		public const double SKILL_MIN_TINKER_TOOLS = 10.0;

		/// <summary>Maximum skill for tinker tools</summary>
		public const double SKILL_MAX_TINKER_TOOLS = 60.0;

		/// <summary>Minimum skill for hatchet</summary>
		public const double SKILL_MIN_HATCHET = 30.0;

		/// <summary>Maximum skill for hatchet</summary>
		public const double SKILL_MAX_HATCHET = 80.0;

		/// <summary>Minimum skill for draw knife</summary>
		public const double SKILL_MIN_DRAW_KNIFE = 30.0;

		/// <summary>Maximum skill for draw knife</summary>
		public const double SKILL_MAX_DRAW_KNIFE = 80.0;

		/// <summary>Minimum skill for sewing kit</summary>
		public const double SKILL_MIN_SEWING_KIT = 10.0;

		/// <summary>Maximum skill for sewing kit</summary>
		public const double SKILL_MAX_SEWING_KIT = 70.0;

		/// <summary>Minimum skill for garden tool</summary>
		public const double SKILL_MIN_GARDEN_TOOL = 5.0;

		/// <summary>Maximum skill for garden tool</summary>
		public const double SKILL_MAX_GARDEN_TOOL = 55.0;

		/// <summary>Minimum skill for herbalist cauldron</summary>
		public const double SKILL_MIN_HERBALIST_CAULDRON = 30.0;

		/// <summary>Maximum skill for herbalist cauldron</summary>
		public const double SKILL_MAX_HERBALIST_CAULDRON = 60.0;

		/// <summary>Minimum skill for saw</summary>
		public const double SKILL_MIN_SAW = 30.0;

		/// <summary>Maximum skill for saw</summary>
		public const double SKILL_MAX_SAW = 80.0;

		/// <summary>Minimum skill for dovetail saw</summary>
		public const double SKILL_MIN_DOVETAIL_SAW = 30.0;

		/// <summary>Maximum skill for dovetail saw</summary>
		public const double SKILL_MAX_DOVETAIL_SAW = 80.0;

		/// <summary>Minimum skill for froe</summary>
		public const double SKILL_MIN_FROE = 30.0;

		/// <summary>Maximum skill for froe</summary>
		public const double SKILL_MAX_FROE = 80.0;

		/// <summary>Minimum skill for shovel</summary>
		public const double SKILL_MIN_SHOVEL = 40.0;

		/// <summary>Maximum skill for shovel</summary>
		public const double SKILL_MAX_SHOVEL = 90.0;

		/// <summary>Minimum skill for ore shovel</summary>
		public const double SKILL_MIN_ORE_SHOVEL = 35.0;

		/// <summary>Maximum skill for ore shovel</summary>
		public const double SKILL_MAX_ORE_SHOVEL = 85.0;

		/// <summary>Minimum skill for grave shovel</summary>
		public const double SKILL_MIN_GRAVE_SHOVEL = 35.0;

		/// <summary>Maximum skill for grave shovel</summary>
		public const double SKILL_MAX_GRAVE_SHOVEL = 85.0;

		/// <summary>Minimum skill for hammer</summary>
		public const double SKILL_MIN_HAMMER = 30.0;

		/// <summary>Maximum skill for hammer</summary>
		public const double SKILL_MAX_HAMMER = 80.0;

		/// <summary>Minimum skill for tongs</summary>
		public const double SKILL_MIN_TONGS = 35.0;

		/// <summary>Maximum skill for tongs</summary>
		public const double SKILL_MAX_TONGS = 85.0;

		/// <summary>Minimum skill for smith hammer</summary>
		public const double SKILL_MIN_SMITH_HAMMER = 40.0;

		/// <summary>Maximum skill for smith hammer</summary>
		public const double SKILL_MAX_SMITH_HAMMER = 90.0;

		/// <summary>Minimum skill for sledge hammer</summary>
		public const double SKILL_MIN_SLEDGE_HAMMER = 40.0;

		/// <summary>Maximum skill for sledge hammer</summary>
		public const double SKILL_MAX_SLEDGE_HAMMER = 90.0;

		/// <summary>Minimum skill for inshave</summary>
		public const double SKILL_MIN_INSHAVE = 30.0;

		/// <summary>Maximum skill for inshave</summary>
		public const double SKILL_MAX_INSHAVE = 80.0;

		/// <summary>Minimum skill for pickaxe</summary>
		public const double SKILL_MIN_PICKAXE = 40.0;

		/// <summary>Maximum skill for pickaxe</summary>
		public const double SKILL_MAX_PICKAXE = 90.0;

		/// <summary>Minimum skill for lockpick</summary>
		public const double SKILL_MIN_LOCKPICK = 45.0;

		/// <summary>Maximum skill for lockpick</summary>
		public const double SKILL_MAX_LOCKPICK = 95.0;

		/// <summary>Minimum skill for skillet</summary>
		public const double SKILL_MIN_SKILLET = 30.0;

		/// <summary>Maximum skill for skillet</summary>
		public const double SKILL_MAX_SKILLET = 80.0;

		/// <summary>Minimum skill for flour sifter</summary>
		public const double SKILL_MIN_FLOUR_SIFTER = 50.0;

		/// <summary>Maximum skill for flour sifter</summary>
		public const double SKILL_MAX_FLOUR_SIFTER = 100.0;

		/// <summary>Minimum skill for fletcher tools</summary>
		public const double SKILL_MIN_FLETCHER_TOOLS = 35.0;

		/// <summary>Maximum skill for fletcher tools</summary>
		public const double SKILL_MAX_FLETCHER_TOOLS = 85.0;

		/// <summary>Minimum skill for mapmaker's pen</summary>
		public const double SKILL_MIN_MAPMAKERS_PEN = 25.0;

		/// <summary>Maximum skill for mapmaker's pen</summary>
		public const double SKILL_MAX_MAPMAKERS_PEN = 75.0;

		/// <summary>Minimum skill for scribe's pen</summary>
		public const double SKILL_MIN_SCRIBES_PEN = 25.0;

		/// <summary>Maximum skill for scribe's pen</summary>
		public const double SKILL_MAX_SCRIBES_PEN = 75.0;

		/// <summary>Minimum skill for skinning knife</summary>
		public const double SKILL_MIN_SKINNING_KNIFE = 15.0;

		/// <summary>Maximum skill for skinning knife</summary>
		public const double SKILL_MAX_SKINNING_KNIFE = 55.0;

		/// <summary>Minimum skill for surgeon's knife</summary>
		public const double SKILL_MIN_SURGEONS_KNIFE = 15.0;

		/// <summary>Maximum skill for surgeon's knife</summary>
		public const double SKILL_MAX_SURGEONS_KNIFE = 55.0;

		/// <summary>Minimum skill for mixing cauldron</summary>
		public const double SKILL_MIN_MIXING_CAULDRON = 30.0;

		/// <summary>Maximum skill for mixing cauldron</summary>
		public const double SKILL_MAX_MIXING_CAULDRON = 60.0;

		/// <summary>Minimum skill for waxing pot</summary>
		public const double SKILL_MIN_WAXING_POT = 20.0;

		/// <summary>Maximum skill for waxing pot</summary>
		public const double SKILL_MAX_WAXING_POT = 60.0;

		/// <summary>Minimum skill for woodworking tools</summary>
		public const double SKILL_MIN_WOODWORKING_TOOLS = 30.0;

		/// <summary>Maximum skill for woodworking tools</summary>
		public const double SKILL_MAX_WOODWORKING_TOOLS = 80.0;

		/// <summary>Minimum skill for trap kit</summary>
		public const double SKILL_MIN_TRAP_KIT = 75.0;

		/// <summary>Maximum skill for trap kit</summary>
		public const double SKILL_MAX_TRAP_KIT = 110.0;

		#endregion

		#region Skill Requirements - Parts

		/// <summary>Minimum skill for gears</summary>
		public const double SKILL_MIN_GEARS = 5.0;

		/// <summary>Maximum skill for gears</summary>
		public const double SKILL_MAX_GEARS = 55.0;

		/// <summary>Minimum skill for clock parts</summary>
		public const double SKILL_MIN_CLOCK_PARTS = 25.0;

		/// <summary>Maximum skill for clock parts</summary>
		public const double SKILL_MAX_CLOCK_PARTS = 75.0;

		/// <summary>Minimum skill for barrel tap</summary>
		public const double SKILL_MIN_BARREL_TAP = 35.0;

		/// <summary>Maximum skill for barrel tap</summary>
		public const double SKILL_MAX_BARREL_TAP = 85.0;

		/// <summary>Minimum skill for springs</summary>
		public const double SKILL_MIN_SPRINGS = 5.0;

		/// <summary>Maximum skill for springs</summary>
		public const double SKILL_MAX_SPRINGS = 55.0;

		/// <summary>Minimum skill for sextant parts</summary>
		public const double SKILL_MIN_SEXTANT_PARTS = 30.0;

		/// <summary>Maximum skill for sextant parts</summary>
		public const double SKILL_MAX_SEXTANT_PARTS = 80.0;

		/// <summary>Minimum skill for barrel hoops</summary>
		public const double SKILL_MIN_BARREL_HOOPS = -15.0;

		/// <summary>Maximum skill for barrel hoops</summary>
		public const double SKILL_MAX_BARREL_HOOPS = 35.0;

		/// <summary>Minimum skill for hinge</summary>
		public const double SKILL_MIN_HINGE = 5.0;

		/// <summary>Maximum skill for hinge</summary>
		public const double SKILL_MAX_HINGE = 55.0;

		/// <summary>Minimum skill for bola ball</summary>
		public const double SKILL_MIN_BOLA_BALL = 45.0;

		/// <summary>Maximum skill for bola ball</summary>
		public const double SKILL_MAX_BOLA_BALL = 95.0;

		#endregion

		#region Skill Requirements - Utensils

		/// <summary>Minimum skill for butcher knife</summary>
		public const double SKILL_MIN_BUTCHER_KNIFE = 25.0;

		/// <summary>Maximum skill for butcher knife</summary>
		public const double SKILL_MAX_BUTCHER_KNIFE = 75.0;

		/// <summary>Minimum skill for spoon</summary>
		public const double SKILL_MIN_SPOON = 0.0;

		/// <summary>Maximum skill for spoon</summary>
		public const double SKILL_MAX_SPOON = 50.0;

		/// <summary>Minimum skill for plate</summary>
		public const double SKILL_MIN_PLATE = 0.0;

		/// <summary>Maximum skill for plate</summary>
		public const double SKILL_MAX_PLATE = 50.0;

		/// <summary>Minimum skill for fork</summary>
		public const double SKILL_MIN_FORK = 0.0;

		/// <summary>Maximum skill for fork</summary>
		public const double SKILL_MAX_FORK = 50.0;

		/// <summary>Minimum skill for cleaver</summary>
		public const double SKILL_MIN_CLEAVER = 20.0;

		/// <summary>Maximum skill for cleaver</summary>
		public const double SKILL_MAX_CLEAVER = 70.0;

		/// <summary>Minimum skill for knife</summary>
		public const double SKILL_MIN_KNIFE = 0.0;

		/// <summary>Maximum skill for knife</summary>
		public const double SKILL_MAX_KNIFE = 50.0;

		/// <summary>Minimum skill for goblet</summary>
		public const double SKILL_MIN_GOBLET = 10.0;

		/// <summary>Maximum skill for goblet</summary>
		public const double SKILL_MAX_GOBLET = 60.0;

		/// <summary>Minimum skill for pewter mug</summary>
		public const double SKILL_MIN_PEWTER_MUG = 10.0;

		/// <summary>Maximum skill for pewter mug</summary>
		public const double SKILL_MAX_PEWTER_MUG = 60.0;

		/// <summary>Minimum skill for skinning knife (utensils)</summary>
		public const double SKILL_MIN_SKINNING_KNIFE_UTENSILS = 25.0;

		/// <summary>Maximum skill for skinning knife (utensils)</summary>
		public const double SKILL_MAX_SKINNING_KNIFE_UTENSILS = 75.0;

		#endregion

		#region Skill Requirements - Misc

		/// <summary>Minimum skill for candle large</summary>
		public const double SKILL_MIN_CANDLE_LARGE = 45.0;

		/// <summary>Maximum skill for candle large</summary>
		public const double SKILL_MAX_CANDLE_LARGE = 85.0;

		/// <summary>Minimum skill for candelabra</summary>
		public const double SKILL_MIN_CANDELABRA = 55.0;

		/// <summary>Maximum skill for candelabra</summary>
		public const double SKILL_MAX_CANDELABRA = 195.0;

		/// <summary>Minimum skill for candelabra stand</summary>
		public const double SKILL_MIN_CANDELABRA_STAND = 65.0;

		/// <summary>Maximum skill for candelabra stand</summary>
		public const double SKILL_MAX_CANDELABRA_STAND = 105.0;

		/// <summary>Minimum skill for scales</summary>
		public const double SKILL_MIN_SCALES = 60.0;

		/// <summary>Maximum skill for scales</summary>
		public const double SKILL_MAX_SCALES = 110.0;

		/// <summary>Minimum skill for key</summary>
		public const double SKILL_MIN_KEY = 20.0;

		/// <summary>Maximum skill for key</summary>
		public const double SKILL_MAX_KEY = 70.0;

		/// <summary>Minimum skill for key ring</summary>
		public const double SKILL_MIN_KEY_RING = 10.0;

		/// <summary>Maximum skill for key ring</summary>
		public const double SKILL_MAX_KEY_RING = 60.0;

		/// <summary>Minimum skill for globe</summary>
		public const double SKILL_MIN_GLOBE = 55.0;

		/// <summary>Maximum skill for globe</summary>
		public const double SKILL_MAX_GLOBE = 105.0;

		/// <summary>Minimum skill for spyglass</summary>
		public const double SKILL_MIN_SPYGLASS = 60.0;

		/// <summary>Maximum skill for spyglass</summary>
		public const double SKILL_MAX_SPYGLASS = 110.0;

		/// <summary>Minimum skill for lantern</summary>
		public const double SKILL_MIN_LANTERN = 30.0;

		/// <summary>Maximum skill for lantern</summary>
		public const double SKILL_MAX_LANTERN = 80.0;

		/// <summary>Minimum skill for heating stand</summary>
		public const double SKILL_MIN_HEATING_STAND = 60.0;

		/// <summary>Maximum skill for heating stand</summary>
		public const double SKILL_MAX_HEATING_STAND = 110.0;

		/// <summary>Minimum skill for wall torch</summary>
		public const double SKILL_MIN_WALL_TORCH = 55.0;

		/// <summary>Maximum skill for wall torch</summary>
		public const double SKILL_MAX_WALL_TORCH = 105.0;

		/// <summary>Minimum skill for colored wall torch</summary>
		public const double SKILL_MIN_COLORED_WALL_TORCH = 85.0;

		/// <summary>Maximum skill for colored wall torch</summary>
		public const double SKILL_MAX_COLORED_WALL_TORCH = 125.0;

		/// <summary>Minimum skill for shoji lantern</summary>
		public const double SKILL_MIN_SHOJI_LANTERN = 65.0;

		/// <summary>Maximum skill for shoji lantern</summary>
		public const double SKILL_MAX_SHOJI_LANTERN = 115.0;

		/// <summary>Minimum skill for paper lantern</summary>
		public const double SKILL_MIN_PAPER_LANTERN = 65.0;

		/// <summary>Maximum skill for paper lantern</summary>
		public const double SKILL_MAX_PAPER_LANTERN = 115.0;

		/// <summary>Minimum skill for round paper lantern</summary>
		public const double SKILL_MIN_ROUND_PAPER_LANTERN = 65.0;

		/// <summary>Maximum skill for round paper lantern</summary>
		public const double SKILL_MAX_ROUND_PAPER_LANTERN = 115.0;

		/// <summary>Minimum skill for wind chimes</summary>
		public const double SKILL_MIN_WIND_CHIMES = 80.0;

		/// <summary>Maximum skill for wind chimes</summary>
		public const double SKILL_MAX_WIND_CHIMES = 130.0;

		/// <summary>Minimum skill for fancy wind chimes</summary>
		public const double SKILL_MIN_FANCY_WIND_CHIMES = 80.0;

		/// <summary>Maximum skill for fancy wind chimes</summary>
		public const double SKILL_MAX_FANCY_WIND_CHIMES = 130.0;

		#endregion

		#region Skill Requirements - Jewelry

		/// <summary>Minimum skill for standard jewelry</summary>
		public const double SKILL_MIN_JEWELRY = 40.0;

		/// <summary>Maximum skill for standard jewelry</summary>
		public const double SKILL_MAX_JEWELRY = 90.0;

		/// <summary>Minimum skill for dwarven jewelry</summary>
		public const double SKILL_MIN_DWARVEN_JEWELRY = 99.0;

		/// <summary>Maximum skill for dwarven jewelry</summary>
		public const double SKILL_MAX_DWARVEN_JEWELRY = 125.0;

		#endregion

		#region Skill Requirements - Multi-Component Items

		/// <summary>Minimum skill for axle gears (assembly)</summary>
		public const double SKILL_MIN_AXLE_GEARS = 0.0;

		/// <summary>Maximum skill for axle gears (assembly)</summary>
		public const double SKILL_MAX_AXLE_GEARS = 0.0;

		/// <summary>Minimum skill for clock parts (assembly)</summary>
		public const double SKILL_MIN_CLOCK_PARTS_ASSEMBLY = 0.0;

		/// <summary>Maximum skill for clock parts (assembly)</summary>
		public const double SKILL_MAX_CLOCK_PARTS_ASSEMBLY = 0.0;

		/// <summary>Minimum skill for sextant parts (assembly)</summary>
		public const double SKILL_MIN_SEXTANT_PARTS_ASSEMBLY = 0.0;

		/// <summary>Maximum skill for sextant parts (assembly)</summary>
		public const double SKILL_MAX_SEXTANT_PARTS_ASSEMBLY = 0.0;

		/// <summary>Minimum skill for clock assembly</summary>
		public const double SKILL_MIN_CLOCK = 0.0;

		/// <summary>Maximum skill for clock assembly</summary>
		public const double SKILL_MAX_CLOCK = 0.0;

		/// <summary>Minimum skill for sextant assembly</summary>
		public const double SKILL_MIN_SEXTANT = 0.0;

		/// <summary>Maximum skill for sextant assembly</summary>
		public const double SKILL_MAX_SEXTANT = 0.0;

		/// <summary>Minimum skill for bola</summary>
		public const double SKILL_MIN_BOLA = 60.0;

		/// <summary>Maximum skill for bola</summary>
		public const double SKILL_MAX_BOLA = 80.0;

		/// <summary>Minimum skill for potion keg</summary>
		public const double SKILL_MIN_POTION_KEG = 75.0;

		/// <summary>Maximum skill for potion keg</summary>
		public const double SKILL_MAX_POTION_KEG = 100.0;

		#endregion

		#region Skill Requirements - Lockpicking Chests

		/// <summary>Maximum skill for all lockpicking chests</summary>
		public const double SKILL_MAX_LOCKPICKING_CHEST = 100.0;

		/// <summary>Minimum skill for lockpicking chest 10</summary>
		public const double SKILL_MIN_LOCKPICKING_CHEST_10 = 75.0;

		/// <summary>Minimum skill for lockpicking chest 20</summary>
		public const double SKILL_MIN_LOCKPICKING_CHEST_20 = 80.0;

		/// <summary>Minimum skill for lockpicking chest 30</summary>
		public const double SKILL_MIN_LOCKPICKING_CHEST_30 = 82.5;

		/// <summary>Minimum skill for lockpicking chest 40</summary>
		public const double SKILL_MIN_LOCKPICKING_CHEST_40 = 85.0;

		/// <summary>Minimum skill for lockpicking chest 50</summary>
		public const double SKILL_MIN_LOCKPICKING_CHEST_50 = 87.5;

		/// <summary>Minimum skill for lockpicking chest 60</summary>
		public const double SKILL_MIN_LOCKPICKING_CHEST_60 = 90.0;

		/// <summary>Minimum skill for lockpicking chest 70</summary>
		public const double SKILL_MIN_LOCKPICKING_CHEST_70 = 92.5;

		/// <summary>Minimum skill for lockpicking chest 80</summary>
		public const double SKILL_MIN_LOCKPICKING_CHEST_80 = 95.0;

		/// <summary>Minimum skill for lockpicking chest 90</summary>
		public const double SKILL_MIN_LOCKPICKING_CHEST_90 = 97.5;

		#endregion

		#region Skill Requirements - Hospitality Items

		/// <summary>Maximum skill for hospitality items</summary>
		public const double SKILL_MAX_HOSPITALITY = 100.0;

		/// <summary>Minimum skill for ale barrel</summary>
		public const double SKILL_MIN_ALE_BARREL = 60.0;

		/// <summary>Minimum skill for cheese press</summary>
		public const double SKILL_MIN_CHEESE_PRESS = 60.0;

		/// <summary>Minimum skill for cider barrel</summary>
		public const double SKILL_MIN_CIDER_BARREL = 60.0;

		/// <summary>Minimum skill for liquor barrel</summary>
		public const double SKILL_MIN_LIQUOR_BARREL = 95.0;

		/// <summary>Maximum skill for liquor barrel</summary>
		public const double SKILL_MAX_LIQUOR_BARREL = 120.0;

		/// <summary>Minimum skill for wine barrel</summary>
		public const double SKILL_MIN_WINE_BARREL = 80.0;

		#endregion

		#region Skill Requirements - Metal Type Sub-Resources

		/// <summary>Skill required for iron</summary>
		public const double SKILL_REQ_METAL_IRON = 00.0;

		/// <summary>Skill required for dull copper</summary>
		public const double SKILL_REQ_METAL_DULL_COPPER = 65.0;

		/// <summary>Skill required for shadow iron</summary>
		public const double SKILL_REQ_METAL_SHADOW_IRON = 70.0;

		/// <summary>Skill required for copper</summary>
		public const double SKILL_REQ_METAL_COPPER = 75.0;

		/// <summary>Skill required for bronze</summary>
		public const double SKILL_REQ_METAL_BRONZE = 80.0;

		/// <summary>Skill required for gold</summary>
		public const double SKILL_REQ_METAL_GOLD = 85.0;

		/// <summary>Skill required for agapite</summary>
		public const double SKILL_REQ_METAL_AGAPITE = 90.0;

		/// <summary>Skill required for verite</summary>
		public const double SKILL_REQ_METAL_VERITE = 95.0;

		/// <summary>Skill required for valorite</summary>
		public const double SKILL_REQ_METAL_VALORITE = 99.0;

		/// <summary>Skill required for titanium</summary>
		public const double SKILL_REQ_METAL_TITANIUM = 99.0;

		/// <summary>Skill required for rosenium</summary>
		public const double SKILL_REQ_METAL_ROSENIUM = 99.0;

		/// <summary>Skill required for platinum</summary>
		public const double SKILL_REQ_METAL_PLATINUM = 99.0;

		/// <summary>Skill required for nepturite</summary>
		public const double SKILL_REQ_METAL_NEPTURITE = 99.0;

		/// <summary>Skill required for obsidian</summary>
		public const double SKILL_REQ_METAL_OBSIDIAN = 99.0;

		/// <summary>Skill required for steel</summary>
		public const double SKILL_REQ_METAL_STEEL = 99.0;

		/// <summary>Skill required for brass</summary>
		public const double SKILL_REQ_METAL_BRASS = 105.0;

		/// <summary>Skill required for mithril</summary>
		public const double SKILL_REQ_METAL_MITHRIL = 110.0;

		/// <summary>Skill required for xormite</summary>
		public const double SKILL_REQ_METAL_XORMITE = 115.0;

		/// <summary>Skill required for dwarven</summary>
		public const double SKILL_REQ_METAL_DWARVEN = 120.0;

		#endregion

		#region Resource Amounts - Wooden Items

		/// <summary>Logs required for planes</summary>
		public const int RESOURCE_LOGS_PLANES = 4;

		/// <summary>Logs required for clock frame</summary>
		public const int RESOURCE_LOGS_CLOCK_FRAME = 6;

		/// <summary>Logs required for axle</summary>
		public const int RESOURCE_LOGS_AXLE = 2;

		/// <summary>Logs required for rolling pin</summary>
		public const int RESOURCE_LOGS_ROLLING_PIN = 5;

		/// <summary>Granite required for saw mill</summary>
		public const int RESOURCE_GRANITE_SAW_MILL = 80;

		/// <summary>Iron ingots required for saw mill</summary>
		public const int RESOURCE_IRON_SAW_MILL = 10;

		/// <summary>Iron ingots required for nunchaku</summary>
		public const int RESOURCE_IRON_NUNCHAKU = 3;

		/// <summary>Logs required for nunchaku</summary>
		public const int RESOURCE_LOGS_NUNCHAKU = 8;

		#endregion

		#region Resource Amounts - Tools

		/// <summary>Iron ingots required for scissors</summary>
		public const int RESOURCE_IRON_SCISSORS = 2;

		/// <summary>Iron ingots required for mortar and pestle</summary>
		public const int RESOURCE_IRON_MORTAR_PESTLE = 3;

		/// <summary>Iron ingots required for scorp</summary>
		public const int RESOURCE_IRON_SCORP = 2;

		/// <summary>Iron ingots required for tinker tools</summary>
		public const int RESOURCE_IRON_TINKER_TOOLS = 2;

		/// <summary>Iron ingots required for hatchet</summary>
		public const int RESOURCE_IRON_HATCHET = 4;

		/// <summary>Iron ingots required for draw knife</summary>
		public const int RESOURCE_IRON_DRAW_KNIFE = 2;

		/// <summary>Iron ingots required for sewing kit</summary>
		public const int RESOURCE_IRON_SEWING_KIT = 2;

		/// <summary>Iron ingots required for garden tool</summary>
		public const int RESOURCE_IRON_GARDEN_TOOL = 2;

		/// <summary>Iron ingots required for herbalist cauldron</summary>
		public const int RESOURCE_IRON_HERBALIST_CAULDRON = 20;

		/// <summary>Iron ingots required for saw</summary>
		public const int RESOURCE_IRON_SAW = 4;

		/// <summary>Iron ingots required for dovetail saw</summary>
		public const int RESOURCE_IRON_DOVETAIL_SAW = 4;

		/// <summary>Iron ingots required for froe</summary>
		public const int RESOURCE_IRON_FROE = 2;

		/// <summary>Iron ingots required for shovel</summary>
		public const int RESOURCE_IRON_SHOVEL = 4;

		/// <summary>Iron ingots required for ore shovel</summary>
		public const int RESOURCE_IRON_ORE_SHOVEL = 4;

		/// <summary>Iron ingots required for grave shovel</summary>
		public const int RESOURCE_IRON_GRAVE_SHOVEL = 4;

		/// <summary>Iron ingots required for hammer</summary>
		public const int RESOURCE_IRON_HAMMER = 1;

		/// <summary>Iron ingots required for tongs</summary>
		public const int RESOURCE_IRON_TONGS = 1;

		/// <summary>Iron ingots required for smith hammer</summary>
		public const int RESOURCE_IRON_SMITH_HAMMER = 4;

		/// <summary>Iron ingots required for sledge hammer</summary>
		public const int RESOURCE_IRON_SLEDGE_HAMMER = 4;

		/// <summary>Iron ingots required for inshave</summary>
		public const int RESOURCE_IRON_INSHAVE = 2;

		/// <summary>Iron ingots required for pickaxe</summary>
		public const int RESOURCE_IRON_PICKAXE = 4;

		/// <summary>Iron ingots required for lockpick</summary>
		public const int RESOURCE_IRON_LOCKPICK = 1;

		/// <summary>Iron ingots required for skillet</summary>
		public const int RESOURCE_IRON_SKILLET = 4;

		/// <summary>Iron ingots required for flour sifter</summary>
		public const int RESOURCE_IRON_FLOUR_SIFTER = 3;

		/// <summary>Iron ingots required for fletcher tools</summary>
		public const int RESOURCE_IRON_FLETCHER_TOOLS = 3;

		/// <summary>Iron ingots required for mapmaker's pen</summary>
		public const int RESOURCE_IRON_MAPMAKERS_PEN = 1;

		/// <summary>Iron ingots required for scribe's pen</summary>
		public const int RESOURCE_IRON_SCRIBES_PEN = 1;

		/// <summary>Iron ingots required for skinning knife</summary>
		public const int RESOURCE_IRON_SKINNING_KNIFE = 2;

		/// <summary>Iron ingots required for surgeon's knife</summary>
		public const int RESOURCE_IRON_SURGEONS_KNIFE = 2;

		/// <summary>Iron ingots required for mixing cauldron</summary>
		public const int RESOURCE_IRON_MIXING_CAULDRON = 20;

		/// <summary>Iron ingots required for waxing pot</summary>
		public const int RESOURCE_IRON_WAXING_POT = 10;

		/// <summary>Iron ingots required for woodworking tools</summary>
		public const int RESOURCE_IRON_WOODWORKING_TOOLS = 2;

		/// <summary>Iron ingots required for trap kit</summary>
		public const int RESOURCE_IRON_TRAP_KIT = 32;

		#endregion

		#region Resource Amounts - Parts

		/// <summary>Iron ingots required for gears</summary>
		public const int RESOURCE_IRON_GEARS = 2;

		/// <summary>Iron ingots required for clock parts</summary>
		public const int RESOURCE_IRON_CLOCK_PARTS = 1;

		/// <summary>Iron ingots required for barrel tap</summary>
		public const int RESOURCE_IRON_BARREL_TAP = 2;

		/// <summary>Iron ingots required for springs</summary>
		public const int RESOURCE_IRON_SPRINGS = 2;

		/// <summary>Iron ingots required for sextant parts</summary>
		public const int RESOURCE_IRON_SEXTANT_PARTS = 4;

		/// <summary>Iron ingots required for barrel hoops</summary>
		public const int RESOURCE_IRON_BARREL_HOOPS = 5;

		/// <summary>Iron ingots required for hinge</summary>
		public const int RESOURCE_IRON_HINGE = 2;

		/// <summary>Iron ingots required for bola ball</summary>
		public const int RESOURCE_IRON_BOLA_BALL = 10;

		#endregion

		#region Resource Amounts - Utensils

		/// <summary>Iron ingots required for butcher knife</summary>
		public const int RESOURCE_IRON_BUTCHER_KNIFE = 2;

		/// <summary>Iron ingots required for spoon</summary>
		public const int RESOURCE_IRON_SPOON = 1;

		/// <summary>Iron ingots required for plate</summary>
		public const int RESOURCE_IRON_PLATE = 2;

		/// <summary>Iron ingots required for fork</summary>
		public const int RESOURCE_IRON_FORK = 1;

		/// <summary>Iron ingots required for cleaver</summary>
		public const int RESOURCE_IRON_CLEAVER = 3;

		/// <summary>Iron ingots required for knife</summary>
		public const int RESOURCE_IRON_KNIFE = 1;

		/// <summary>Iron ingots required for goblet</summary>
		public const int RESOURCE_IRON_GOBLET = 2;

		/// <summary>Iron ingots required for pewter mug</summary>
		public const int RESOURCE_IRON_PEWTER_MUG = 2;

		/// <summary>Iron ingots required for skinning knife (utensils)</summary>
		public const int RESOURCE_IRON_SKINNING_KNIFE_UTENSILS = 2;

		#endregion

		#region Resource Amounts - Misc

		/// <summary>Iron ingots required for candle large</summary>
		public const int RESOURCE_IRON_CANDLE_LARGE = 2;

		/// <summary>Beeswax required for candle large</summary>
		public const int RESOURCE_BEESWAX_CANDLE_LARGE = 1;

		/// <summary>Iron ingots required for candelabra</summary>
		public const int RESOURCE_IRON_CANDELABRA = 4;

		/// <summary>Beeswax required for candelabra</summary>
		public const int RESOURCE_BEESWAX_CANDELABRA = 3;

		/// <summary>Iron ingots required for candelabra stand</summary>
		public const int RESOURCE_IRON_CANDELABRA_STAND = 8;

		/// <summary>Beeswax required for candelabra stand</summary>
		public const int RESOURCE_BEESWAX_CANDELABRA_STAND = 3;

		/// <summary>Iron ingots required for scales</summary>
		public const int RESOURCE_IRON_SCALES = 4;

		/// <summary>Iron ingots required for key</summary>
		public const int RESOURCE_IRON_KEY = 3;

		/// <summary>Iron ingots required for key ring</summary>
		public const int RESOURCE_IRON_KEY_RING = 2;

		/// <summary>Iron ingots required for globe</summary>
		public const int RESOURCE_IRON_GLOBE = 4;

		/// <summary>Iron ingots required for spyglass</summary>
		public const int RESOURCE_IRON_SPYGLASS = 4;

		/// <summary>Iron ingots required for lantern</summary>
		public const int RESOURCE_IRON_LANTERN = 2;

		/// <summary>Iron ingots required for heating stand</summary>
		public const int RESOURCE_IRON_HEATING_STAND = 4;

		/// <summary>Iron ingots required for wall torch</summary>
		public const int RESOURCE_IRON_WALL_TORCH = 5;

		/// <summary>Torch required for wall torch</summary>
		public const int RESOURCE_TORCH_WALL_TORCH = 1;

		/// <summary>Iron ingots required for colored wall torch</summary>
		public const int RESOURCE_IRON_COLORED_WALL_TORCH = 5;

		/// <summary>Torch required for colored wall torch</summary>
		public const int RESOURCE_TORCH_COLORED_WALL_TORCH = 1;

		/// <summary>Iron ingots required for shoji lantern</summary>
		public const int RESOURCE_IRON_SHOJI_LANTERN = 10;

		/// <summary>Logs required for shoji lantern</summary>
		public const int RESOURCE_LOGS_SHOJI_LANTERN = 5;

		/// <summary>Iron ingots required for paper lantern</summary>
		public const int RESOURCE_IRON_PAPER_LANTERN = 10;

		/// <summary>Logs required for paper lantern</summary>
		public const int RESOURCE_LOGS_PAPER_LANTERN = 5;

		/// <summary>Iron ingots required for round paper lantern</summary>
		public const int RESOURCE_IRON_ROUND_PAPER_LANTERN = 10;

		/// <summary>Logs required for round paper lantern</summary>
		public const int RESOURCE_LOGS_ROUND_PAPER_LANTERN = 5;

		/// <summary>Iron ingots required for wind chimes</summary>
		public const int RESOURCE_IRON_WIND_CHIMES = 15;

		/// <summary>Iron ingots required for fancy wind chimes</summary>
		public const int RESOURCE_IRON_FANCY_WIND_CHIMES = 15;

		#endregion

		#region Resource Amounts - Jewelry

		/// <summary>Metal ingots required for metal-based jewelry</summary>
		public const int RESOURCE_INGOTS_JEWELRY = 2;

		/// <summary>Iron ingots required for gem-based jewelry</summary>
		public const int RESOURCE_IRON_GEM_JEWELRY = 1;

		/// <summary>Gems required for gem-based jewelry</summary>
		public const int RESOURCE_GEMS_JEWELRY = 1;

		#endregion

		#region Resource Amounts - Multi-Component Items

		/// <summary>Axle required for axle gears</summary>
		public const int RESOURCE_AXLE_AXLE_GEARS = 1;

		/// <summary>Gears required for axle gears</summary>
		public const int RESOURCE_GEARS_AXLE_GEARS = 1;

		/// <summary>Axle gears required for clock parts assembly</summary>
		public const int RESOURCE_AXLE_GEARS_CLOCK_PARTS = 1;

		/// <summary>Springs required for clock parts assembly</summary>
		public const int RESOURCE_SPRINGS_CLOCK_PARTS = 1;

		/// <summary>Axle gears required for sextant parts assembly</summary>
		public const int RESOURCE_AXLE_GEARS_SEXTANT_PARTS = 1;

		/// <summary>Hinge required for sextant parts assembly</summary>
		public const int RESOURCE_HINGE_SEXTANT_PARTS = 1;

		/// <summary>Clock frame required for clock</summary>
		public const int RESOURCE_CLOCK_FRAME_CLOCK = 1;

		/// <summary>Clock parts required for clock</summary>
		public const int RESOURCE_CLOCK_PARTS_CLOCK = 1;

		/// <summary>Sextant parts required for sextant</summary>
		public const int RESOURCE_SEXTANT_PARTS_SEXTANT = 1;

		/// <summary>Bola balls required for bola</summary>
		public const int RESOURCE_BOLA_BALL_BOLA = 4;

		/// <summary>Leather required for bola</summary>
		public const int RESOURCE_LEATHER_BOLA = 3;

		/// <summary>Keg required for potion keg</summary>
		public const int RESOURCE_KEG_POTION_KEG = 1;

		/// <summary>Bottles required for potion keg</summary>
		public const int RESOURCE_BOTTLES_POTION_KEG = 10;

		/// <summary>Barrel lid required for potion keg</summary>
		public const int RESOURCE_BARREL_LID_POTION_KEG = 1;

		/// <summary>Barrel tap required for potion keg</summary>
		public const int RESOURCE_BARREL_TAP_POTION_KEG = 1;

		#endregion

		#region Resource Amounts - Lockpicking Chests

		/// <summary>Iron ingots for lockpicking chest 10</summary>
		public const int RESOURCE_IRON_LOCKPICKING_CHEST_10 = 10;

		/// <summary>Arcane gems for lockpicking chest 10</summary>
		public const int RESOURCE_GEMS_LOCKPICKING_CHEST_10 = 1;

		/// <summary>Iron ingots for lockpicking chest 20</summary>
		public const int RESOURCE_IRON_LOCKPICKING_CHEST_20 = 15;

		/// <summary>Arcane gems for lockpicking chest 20</summary>
		public const int RESOURCE_GEMS_LOCKPICKING_CHEST_20 = 3;

		/// <summary>Dull copper ingots for lockpicking chest 30</summary>
		public const int RESOURCE_DULL_COPPER_LOCKPICKING_CHEST_30 = 10;

		/// <summary>Arcane gems for lockpicking chest 30</summary>
		public const int RESOURCE_GEMS_LOCKPICKING_CHEST_30 = 4;

		/// <summary>Shadow iron ingots for lockpicking chest 40</summary>
		public const int RESOURCE_SHADOW_IRON_LOCKPICKING_CHEST_40 = 25;

		/// <summary>Arcane gems for lockpicking chest 40</summary>
		public const int RESOURCE_GEMS_LOCKPICKING_CHEST_40 = 5;

		/// <summary>Bronze ingots for lockpicking chest 50</summary>
		public const int RESOURCE_BRONZE_LOCKPICKING_CHEST_50 = 50;

		/// <summary>Arcane gems for lockpicking chest 50</summary>
		public const int RESOURCE_GEMS_LOCKPICKING_CHEST_50 = 8;

		/// <summary>Gold ingots for lockpicking chest 60</summary>
		public const int RESOURCE_GOLD_LOCKPICKING_CHEST_60 = 100;

		/// <summary>Arcane gems for lockpicking chest 60</summary>
		public const int RESOURCE_GEMS_LOCKPICKING_CHEST_60 = 10;

		/// <summary>Agapite ingots for lockpicking chest 70</summary>
		public const int RESOURCE_AGAPITE_LOCKPICKING_CHEST_70 = 250;

		/// <summary>Arcane gems for lockpicking chest 70</summary>
		public const int RESOURCE_GEMS_LOCKPICKING_CHEST_70 = 15;

		/// <summary>Verite ingots for lockpicking chest 80</summary>
		public const int RESOURCE_VERITE_LOCKPICKING_CHEST_80 = 500;

		/// <summary>Arcane gems for lockpicking chest 80</summary>
		public const int RESOURCE_GEMS_LOCKPICKING_CHEST_80 = 30;

		/// <summary>Valorite ingots for lockpicking chest 90</summary>
		public const int RESOURCE_VALORITE_LOCKPICKING_CHEST_90 = 1000;

		/// <summary>Arcane gems for lockpicking chest 90</summary>
		public const int RESOURCE_GEMS_LOCKPICKING_CHEST_90 = 45;

		/// <summary>Wooden box required for all lockpicking chests</summary>
		public const int RESOURCE_WOODEN_BOX_LOCKPICKING_CHEST = 1;

		#endregion

		#region Resource Amounts - Hospitality Items

		/// <summary>Arcane gems for ale barrel</summary>
		public const int RESOURCE_GEMS_ALE_BARREL = 1;

		/// <summary>Kegs for ale barrel</summary>
		public const int RESOURCE_KEGS_ALE_BARREL = 2;

		/// <summary>Axle gears for ale barrel</summary>
		public const int RESOURCE_AXLE_GEARS_ALE_BARREL = 4;

		/// <summary>Arcane gems for cheese press</summary>
		public const int RESOURCE_GEMS_CHEESE_PRESS = 2;

		/// <summary>Kegs for cheese press</summary>
		public const int RESOURCE_KEGS_CHEESE_PRESS = 2;

		/// <summary>Axle gears for cheese press</summary>
		public const int RESOURCE_AXLE_GEARS_CHEESE_PRESS = 4;

		/// <summary>Arcane gems for cider barrel</summary>
		public const int RESOURCE_GEMS_CIDER_BARREL = 1;

		/// <summary>Kegs for cider barrel</summary>
		public const int RESOURCE_KEGS_CIDER_BARREL = 2;

		/// <summary>Axle gears for cider barrel</summary>
		public const int RESOURCE_AXLE_GEARS_CIDER_BARREL = 4;

		/// <summary>Arcane gems for liquor barrel</summary>
		public const int RESOURCE_GEMS_LIQUOR_BARREL = 4;

		/// <summary>Kegs for liquor barrel</summary>
		public const int RESOURCE_KEGS_LIQUOR_BARREL = 2;

		/// <summary>Axle gears for liquor barrel</summary>
		public const int RESOURCE_AXLE_GEARS_LIQUOR_BARREL = 4;

		/// <summary>Arcane gems for wine barrel</summary>
		public const int RESOURCE_GEMS_WINE_BARREL = 2;

		/// <summary>Kegs for wine barrel</summary>
		public const int RESOURCE_KEGS_WINE_BARREL = 2;

		/// <summary>Axle gears for wine barrel</summary>
		public const int RESOURCE_AXLE_GEARS_WINE_BARREL = 4;

		#endregion

		#region Trap Mechanics

		/// <summary>Trap level divisor (skill / 10)</summary>
		public const int TRAP_LEVEL_DIVISOR = 10;

		/// <summary>Trap power multiplier (trapLevel * 9)</summary>
		public const int TRAP_POWER_MULTIPLIER = 9;

		#endregion

		#region Trap Validation Messages

		/// <summary>You can only trap lockable chests (cliloc 1005638)</summary>
		public const int MSG_ONLY_LOCKABLE_CHESTS = 1005638;

		/// <summary>That is too far away (cliloc 500446)</summary>
		public const int MSG_TOO_FAR_AWAY = 500446;

		/// <summary>You cannot trap this item because it is locked down (cliloc 502944)</summary>
		public const int MSG_CANNOT_TRAP_LOCKED_DOWN = 502944;

		/// <summary>That belongs to someone else (cliloc 502946)</summary>
		public const int MSG_BELONGS_TO_SOMEONE_ELSE = 502946;

		/// <summary>You can only trap an unlocked object (cliloc 502943)</summary>
		public const int MSG_ONLY_TRAP_UNLOCKED = 502943;

		/// <summary>You can only place one trap on an object at a time (cliloc 502945)</summary>
		public const int MSG_ONE_TRAP_AT_TIME = 502945;

		/// <summary>Trap is disabled until you lock the chest (cliloc 1005639)</summary>
		public const int MSG_TRAP_DISABLED = 1005639;

		/// <summary>What would you like to set a trap on? (cliloc 502921)</summary>
		public const int MSG_SET_TRAP_ON = 502921;

		#endregion
	}
}

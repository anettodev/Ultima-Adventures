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

		/// <summary>Gump title: "TINKERING MENU" (cliloc 1044007)</summary>
		public const int MSG_GUMP_TITLE = 1044007;

		/// <summary>Message: "You have worn out your tool!" (cliloc 1044038)</summary>
		public const int MSG_TOOL_WORN_OUT = 1044038;

		/// <summary>Message: "The tool must be on your person to use." (cliloc 1044263)</summary>
		public const int MSG_TOOL_MUST_BE_ON_PERSON = 1044263;

		/// <summary>Message: "You failed to create the item, and some of your materials are lost." (cliloc 1044043)</summary>
		public const int MSG_FAILED_LOST_MATERIALS = 1044043;

		/// <summary>Message: "You failed to create the item, but no materials were lost." (cliloc 1044157)</summary>
		public const int MSG_FAILED_NO_MATERIALS_LOST = 1044157;

		/// <summary>Message: "You were barely able to make this item. It's quality is below average." (cliloc 502785)</summary>
		public const int MSG_BARELY_MADE_ITEM = 502785;

		/// <summary>Message: "You create an exceptional quality item and affix your maker's mark." (cliloc 1044156)</summary>
		public const int MSG_EXCEPTIONAL_WITH_MARK = 1044156;

		/// <summary>Message: "You create an exceptional quality item." (cliloc 1044155)</summary>
		public const int MSG_EXCEPTIONAL_QUALITY = 1044155;

		/// <summary>Message: "You create the item." (cliloc 1044154)</summary>
		public const int MSG_ITEM_CREATED = 1044154;

		/// <summary>Resource name: "Iron Ingots" (cliloc 1044036)</summary>
		public const int MSG_IRON_INGOTS = 1044036;

		/// <summary>Error: "You don't have enough ingots." (cliloc 1044037)</summary>
		public const int MSG_INSUFFICIENT_INGOTS = 1044037;

		/// <summary>Resource name: "Logs" (cliloc 1015101)</summary>
		public const int MSG_LOGS = 1015101;

		/// <summary>Error: "You don't have enough logs." (cliloc 1044351)</summary>
		public const int MSG_INSUFFICIENT_LOGS = 1044351;

		/// <summary>Error: "You don't have the required items." (cliloc 1044253)</summary>
		public const int MSG_INSUFFICIENT_RESOURCES = 1044253;

		/// <summary>Resource name: "Granite" (cliloc 1044514)</summary>
		public const int MSG_GRANITE = 1044514;

		/// <summary>Error: "You don't have enough granite." (cliloc 1044513)</summary>
		public const int MSG_INSUFFICIENT_GRANITE = 1044513;

		/// <summary>Resource name: "Leather" (cliloc 1044462)</summary>
		public const int MSG_LEATHER = 1044462;

		/// <summary>Error: "You don't have enough leather." (cliloc 1044463)</summary>
		public const int MSG_INSUFFICIENT_LEATHER = 1044463;

		/// <summary>Resource name: "Beeswax" (cliloc 1025154)</summary>
		public const int MSG_BEESWAX = 1025154;

		/// <summary>Error: "You don't have enough beeswax." (cliloc 1053098)</summary>
		public const int MSG_INSUFFICIENT_BEESWAX = 1053098;

		/// <summary>Resource name: "Arcane Gem" (cliloc 1114115)</summary>
		public const int MSG_ARCANE_GEM = 1114115;

		/// <summary>Resource name: "Bottle" (cliloc 1044250)</summary>
		public const int MSG_BOTTLE = 1044250;

		/// <summary>Resource name: "Barrel Lid" (cliloc 1044251)</summary>
		public const int MSG_BARREL_LID = 1044251;

		/// <summary>Resource name: "Barrel Tap" (cliloc 1044252)</summary>
		public const int MSG_BARREL_TAP = 1044252;

		/// <summary>Resource name: "Torch" (cliloc 1011410)</summary>
		public const int MSG_TORCH = 1011410;

		#endregion

		#region Craft Group Names (Cliloc)

		/// <summary>Group: "Wooden Items" (cliloc 1044042)</summary>
		public const int MSG_GROUP_WOODEN_ITEMS = 1044042;

		/// <summary>Group: "Tools" (cliloc 1044046)</summary>
		public const int MSG_GROUP_TOOLS = 1044046;

		/// <summary>Group: "Parts" (cliloc 1044047)</summary>
		public const int MSG_GROUP_PARTS = 1044047;

		/// <summary>Group: "Utensils" (cliloc 1044048)</summary>
		public const int MSG_GROUP_UTENSILS = 1044048;

		/// <summary>Group: "Jewelry" (cliloc 1044049)</summary>
		public const int MSG_GROUP_JEWELRY = 1044049;

		/// <summary>Group: "Misc" (cliloc 1044050)</summary>
		public const int MSG_GROUP_MISC = 1044050;

		/// <summary>Group: "Assemblies" (cliloc 1044051)</summary>
		public const int MSG_GROUP_MULTI_COMPONENT = 1044051;

		#endregion

		#region Item Names (Cliloc) - Wooden Items

		/// <summary>Jointing Plane (cliloc 1024144)</summary>
		public const int MSG_JOINTING_PLANE = 1024144;

		/// <summary>Moulding Plane (cliloc 1024140)</summary>
		public const int MSG_MOULDING_PLANE = 1024140;

		/// <summary>Smoothing Plane (cliloc 1024146)</summary>
		public const int MSG_SMOOTHING_PLANE = 1024146;

		/// <summary>Clock Frame (cliloc 1024173)</summary>
		public const int MSG_CLOCK_FRAME = 1024173;

		/// <summary>Axle (cliloc 1024187)</summary>
		public const int MSG_AXLE = 1024187;

		/// <summary>Rolling Pin (cliloc 1024163)</summary>
		public const int MSG_ROLLING_PIN = 1024163;

		/// <summary>Nunchaku (cliloc 1030158)</summary>
		public const int MSG_NUNCHAKU = 1030158;

		#endregion

		#region Item Names (Cliloc) - Tools

		/// <summary>Scissors (cliloc 1023998)</summary>
		public const int MSG_SCISSORS = 1023998;

		/// <summary>Mortar and Pestle (cliloc 1023739)</summary>
		public const int MSG_MORTAR_PESTLE = 1023739;

		/// <summary>Scorp (cliloc 1024327)</summary>
		public const int MSG_SCORP = 1024327;

		/// <summary>Tinker Tools (cliloc 1044164)</summary>
		public const int MSG_TINKER_TOOLS = 1044164;

		/// <summary>Hatchet (cliloc 1023907)</summary>
		public const int MSG_HATCHET = 1023907;

		/// <summary>Draw Knife (cliloc 1024324)</summary>
		public const int MSG_DRAW_KNIFE = 1024324;

		/// <summary>Sewing Kit (cliloc 1023997)</summary>
		public const int MSG_SEWING_KIT = 1023997;

		/// <summary>Saw (cliloc 1024148)</summary>
		public const int MSG_SAW = 1024148;

		/// <summary>Dovetail Saw (cliloc 1024136)</summary>
		public const int MSG_DOVETAIL_SAW = 1024136;

		/// <summary>Froe (cliloc 1024325)</summary>
		public const int MSG_FROE = 1024325;

		/// <summary>Shovel (cliloc 1023898)</summary>
		public const int MSG_SHOVEL = 1023898;

		/// <summary>Hammer (cliloc 1024138)</summary>
		public const int MSG_HAMMER = 1024138;

		/// <summary>Tongs (cliloc 1024028)</summary>
		public const int MSG_TONGS = 1024028;

		/// <summary>Smith Hammer (cliloc 1025091)</summary>
		public const int MSG_SMITH_HAMMER = 1025091;

		/// <summary>Sledge Hammer (cliloc 1024021)</summary>
		public const int MSG_SLEDGE_HAMMER = 1024021;

		/// <summary>Inshave (cliloc 1024326)</summary>
		public const int MSG_INSHAVE = 1024326;

		/// <summary>Pickaxe (cliloc 1023718)</summary>
		public const int MSG_PICKAXE = 1023718;

		/// <summary>Lockpick (cliloc 1025371)</summary>
		public const int MSG_LOCKPICK = 1025371;

		/// <summary>Skillet (cliloc 1044567)</summary>
		public const int MSG_SKILLET = 1044567;

		/// <summary>Flour Sifter (cliloc 1024158)</summary>
		public const int MSG_FLOUR_SIFTER = 1024158;

		/// <summary>Fletcher Tools (cliloc 1044166)</summary>
		public const int MSG_FLETCHER_TOOLS = 1044166;

		/// <summary>Mapmaker's Pen (cliloc 1044167)</summary>
		public const int MSG_MAPMAKERS_PEN = 1044167;

		/// <summary>Scribe's Pen (cliloc 1044168)</summary>
		public const int MSG_SCRIBES_PEN = 1044168;

		#endregion

		#region Item Names (Cliloc) - Parts

		/// <summary>Gears (cliloc 1024179)</summary>
		public const int MSG_GEARS = 1024179;

		/// <summary>Clock Parts (cliloc 1024175)</summary>
		public const int MSG_CLOCK_PARTS = 1024175;

		/// <summary>Barrel Tap (cliloc 1024100)</summary>
		public const int MSG_BARREL_TAP_ITEM = 1024100;

		/// <summary>Springs (cliloc 1024189)</summary>
		public const int MSG_SPRINGS = 1024189;

		/// <summary>Sextant Parts (cliloc 1024185)</summary>
		public const int MSG_SEXTANT_PARTS = 1024185;

		/// <summary>Barrel Hoops (cliloc 1024321)</summary>
		public const int MSG_BARREL_HOOPS = 1024321;

		/// <summary>Hinge (cliloc 1024181)</summary>
		public const int MSG_HINGE = 1024181;

		/// <summary>Bola Ball (cliloc 1023699)</summary>
		public const int MSG_BOLA_BALL = 1023699;

		/// <summary>Axle (cliloc 1044169)</summary>
		public const int MSG_AXLE_RESOURCE = 1044169;

		/// <summary>Axle Gears (cliloc 1044170)</summary>
		public const int MSG_AXLE_GEARS = 1044170;

		/// <summary>Springs (cliloc 1044171)</summary>
		public const int MSG_SPRINGS_RESOURCE = 1044171;

		/// <summary>Hinge (cliloc 1044172)</summary>
		public const int MSG_HINGE_RESOURCE = 1044172;

		/// <summary>Clock Parts (cliloc 1044173)</summary>
		public const int MSG_CLOCK_PARTS_RESOURCE = 1044173;

		/// <summary>Clock Frame (cliloc 1044174)</summary>
		public const int MSG_CLOCK_FRAME_RESOURCE = 1044174;

		/// <summary>Sextant Parts (cliloc 1044175)</summary>
		public const int MSG_SEXTANT_PARTS_RESOURCE = 1044175;

		/// <summary>Gears (cliloc 1044254)</summary>
		public const int MSG_GEARS_RESOURCE = 1044254;

		#endregion

		#region Item Names (Cliloc) - Utensils

		/// <summary>Butcher Knife (cliloc 1025110)</summary>
		public const int MSG_BUTCHER_KNIFE = 1025110;

		/// <summary>Spoon (Left) (cliloc 1044158)</summary>
		public const int MSG_SPOON_LEFT = 1044158;

		/// <summary>Spoon (Right) (cliloc 1044159)</summary>
		public const int MSG_SPOON_RIGHT = 1044159;

		/// <summary>Plate (cliloc 1022519)</summary>
		public const int MSG_PLATE = 1022519;

		/// <summary>Fork (Left) (cliloc 1044160)</summary>
		public const int MSG_FORK_LEFT = 1044160;

		/// <summary>Fork (Right) (cliloc 1044161)</summary>
		public const int MSG_FORK_RIGHT = 1044161;

		/// <summary>Cleaver (cliloc 1023778)</summary>
		public const int MSG_CLEAVER = 1023778;

		/// <summary>Knife (Left) (cliloc 1044162)</summary>
		public const int MSG_KNIFE_LEFT = 1044162;

		/// <summary>Knife (Right) (cliloc 1044163)</summary>
		public const int MSG_KNIFE_RIGHT = 1044163;

		/// <summary>Goblet (cliloc 1022458)</summary>
		public const int MSG_GOBLET = 1022458;

		/// <summary>Pewter Mug (cliloc 1024097)</summary>
		public const int MSG_PEWTER_MUG = 1024097;

		/// <summary>Skinning Knife (cliloc 1023781)</summary>
		public const int MSG_SKINNING_KNIFE = 1023781;

		#endregion

		#region Item Names (Cliloc) - Misc

		/// <summary>Candle (Large) (cliloc 1022598)</summary>
		public const int MSG_CANDLE_LARGE = 1022598;

		/// <summary>Candelabra (cliloc 1022599)</summary>
		public const int MSG_CANDELABRA = 1022599;

		/// <summary>Scales (cliloc 1026225)</summary>
		public const int MSG_SCALES = 1026225;

		/// <summary>Key (cliloc 1024112)</summary>
		public const int MSG_KEY = 1024112;

		/// <summary>Key Ring (cliloc 1024113)</summary>
		public const int MSG_KEY_RING = 1024113;

		/// <summary>Globe (cliloc 1024167)</summary>
		public const int MSG_GLOBE = 1024167;

		/// <summary>Lantern (cliloc 1022597)</summary>
		public const int MSG_LANTERN = 1022597;

		/// <summary>Heating Stand (cliloc 1026217)</summary>
		public const int MSG_HEATING_STAND = 1026217;

		/// <summary>Shoji Lantern (cliloc 1029404)</summary>
		public const int MSG_SHOJI_LANTERN = 1029404;

		/// <summary>Paper Lantern (cliloc 1029406)</summary>
		public const int MSG_PAPER_LANTERN = 1029406;

		/// <summary>Round Paper Lantern (cliloc 1029418)</summary>
		public const int MSG_ROUND_PAPER_LANTERN = 1029418;

		/// <summary>Wind Chimes (cliloc 1030290)</summary>
		public const int MSG_WIND_CHIMES = 1030290;

		/// <summary>Fancy Wind Chimes (cliloc 1030291)</summary>
		public const int MSG_FANCY_WIND_CHIMES = 1030291;

		#endregion

		#region Item Names (Cliloc) - Multi-Component

		/// <summary>Axle Gears (cliloc 1024177)</summary>
		public const int MSG_AXLE_GEARS_ITEM = 1024177;

		/// <summary>Clock (Right) (cliloc 1044257)</summary>
		public const int MSG_CLOCK_RIGHT = 1044257;

		/// <summary>Clock (Left) (cliloc 1044256)</summary>
		public const int MSG_CLOCK_LEFT = 1044256;

		/// <summary>Sextant (cliloc 1024183)</summary>
		public const int MSG_SEXTANT = 1024183;

		/// <summary>Bola (cliloc 1046441)</summary>
		public const int MSG_BOLA = 1046441;

		/// <summary>Bola Ball (cliloc 1046440)</summary>
		public const int MSG_BOLA_BALL_RESOURCE = 1046440;

		/// <summary>Potion Keg (cliloc 1044258)</summary>
		public const int MSG_POTION_KEG = 1044258;

		/// <summary>Failed to create item (cliloc 1042613)</summary>
		public const int MSG_FAILED_CREATE = 1042613;

		#endregion

		#region Metal Type Names (Cliloc)

		/// <summary>Iron (cliloc 1044022)</summary>
		public const int MSG_METAL_IRON = 1044022;

		/// <summary>Dull Copper (cliloc 1044023)</summary>
		public const int MSG_METAL_DULL_COPPER = 1044023;

		/// <summary>Shadow Iron (cliloc 1044024)</summary>
		public const int MSG_METAL_SHADOW_IRON = 1044024;

		/// <summary>Copper (cliloc 1044025)</summary>
		public const int MSG_METAL_COPPER = 1044025;

		/// <summary>Bronze (cliloc 1044026)</summary>
		public const int MSG_METAL_BRONZE = 1044026;

		/// <summary>Gold (cliloc 1044027)</summary>
		public const int MSG_METAL_GOLD = 1044027;

		/// <summary>Agapite (cliloc 1044028)</summary>
		public const int MSG_METAL_AGAPITE = 1044028;

		/// <summary>Verite (cliloc 1044029)</summary>
		public const int MSG_METAL_VERITE = 1044029;

		/// <summary>Valorite (cliloc 1044030)</summary>
		public const int MSG_METAL_VALORITE = 1044030;

		/// <summary>Titanium (cliloc 6661000)</summary>
		public const int MSG_METAL_TITANIUM = 6661000;

		/// <summary>Rosenium (cliloc 6662000)</summary>
		public const int MSG_METAL_ROSENIUM = 6662000;

		/// <summary>Platinum (cliloc 6663000)</summary>
		public const int MSG_METAL_PLATINUM = 6663000;

		/// <summary>Nepturite (cliloc 1036173)</summary>
		public const int MSG_METAL_NEPTURITE = 1036173;

		/// <summary>Obsidian (cliloc 1036162)</summary>
		public const int MSG_METAL_OBSIDIAN = 1036162;

		/// <summary>Steel (cliloc 1036144)</summary>
		public const int MSG_METAL_STEEL = 1036144;

		/// <summary>Brass (cliloc 1036152)</summary>
		public const int MSG_METAL_BRASS = 1036152;

		/// <summary>Mithril (cliloc 1036137)</summary>
		public const int MSG_METAL_MITHRIL = 1036137;

		/// <summary>Xormite (cliloc 1034437)</summary>
		public const int MSG_METAL_XORMITE = 1034437;

		/// <summary>Dwarven (cliloc 1036181)</summary>
		public const int MSG_METAL_DWARVEN = 1036181;

		/// <summary>Not enough material (cliloc 1044267)</summary>
		public const int MSG_NOT_ENOUGH_MATERIAL = 1044267;

		/// <summary>Material type selection (cliloc 1044268)</summary>
		public const int MSG_MATERIAL_SELECTION = 1044268;

		/// <summary>Dull Copper Ingot (cliloc 1074916)</summary>
		public const int MSG_DULL_COPPER_INGOT = 1074916;

		/// <summary>Shadow Iron Ingot (cliloc 1074917)</summary>
		public const int MSG_SHADOW_IRON_INGOT = 1074917;

		/// <summary>Bronze Ingot (cliloc 1074919)</summary>
		public const int MSG_BRONZE_INGOT = 1074919;

		/// <summary>Gold Ingot (cliloc 1074920)</summary>
		public const int MSG_GOLD_INGOT = 1074920;

		/// <summary>Agapite Ingot (cliloc 1074921)</summary>
		public const int MSG_AGAPITE_INGOT = 1074921;

		/// <summary>Verite Ingot (cliloc 1074922)</summary>
		public const int MSG_VERITE_INGOT = 1074922;

		/// <summary>Valorite Ingot (cliloc 1074923)</summary>
		public const int MSG_VALORITE_INGOT = 1074923;

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

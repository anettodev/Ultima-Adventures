namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for Carpentry crafting system calculations and mechanics.
	/// Extracted from DefCarpentry.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class CarpentryConstants
	{
		#region Craft System Configuration

		/// <summary>Minimum chance at minimum skill (50%)</summary>
		public const double CHANCE_AT_MIN = 0.5;

		/// <summary>Minimum craft effect value</summary>
		public const int MIN_CRAFT_EFFECT = 1;

		/// <summary>Craft delay multiplier</summary>
		public const double CRAFT_DELAY = 1.25;

		/// <summary>Sound ID for crafting effect</summary>
		public const int SOUND_CRAFT_EFFECT = 0x23D;

		#endregion

		#region Message Numbers (Cliloc)

		/// <summary>Gump title: "MENU DE CARPINTARIA" (cliloc 1044004)</summary>
		public const int MSG_GUMP_TITLE = 1044004;

		/// <summary>Message: "Você quebrou sua ferramenta!" (cliloc 1044038)</summary>
		public const int MSG_TOOL_WORN_OUT = 1044038;

		/// <summary>Message: "A ferramenta deve estar com você para usar." (cliloc 1044263)</summary>
		public const int MSG_TOOL_MUST_BE_ON_PERSON = 1044263;

		/// <summary>Message: "Você falhou ao criar o item, e alguns de seus materiais foram perdidos." (cliloc 1044043)</summary>
		public const int MSG_FAILED_LOST_MATERIALS = 1044043;

		/// <summary>Message: "Você não conseguiu criar o item, mas nenhum material foi perdido." (cliloc 1044157)</summary>
		public const int MSG_FAILED_NO_MATERIALS_LOST = 1044157;

		/// <summary>Message: "You were barely able to make this item. It's quality is below average." (cliloc 502785)</summary>
		public const int MSG_BARELY_MADE_ITEM = 502785;

		/// <summary>Message: "Você cria um item de qualidade excepcional e assina a sua marca nele." (cliloc 1044156)</summary>
		public const int MSG_EXCEPTIONAL_WITH_MARK = 1044156;

		/// <summary>Message: "Você cria um item de qualidade excepcional." (cliloc 1044155)</summary>
		public const int MSG_EXCEPTIONAL_QUALITY = 1044155;

		/// <summary>Message: "Você cria o item." (cliloc 1044154)</summary>
		public const int MSG_ITEM_CREATED = 1044154;

		/// <summary>Resource name: "Tábuas" (cliloc 1015101)</summary>
		public const int MSG_BOARDS = 1015101;

		/// <summary>Error: "Você não possui madeira suficiente para fazer isso." (cliloc 1044351)</summary>
		public const int MSG_INSUFFICIENT_WOOD = 1044351;

		/// <summary>Error: "Você não pode trabalhar esta madeira estranha e incomum." (cliloc 1072652)</summary>
		public const int MSG_CANNOT_WORK_WOOD = 1072652;

		/// <summary>Error: "Você não possui toras de madeira." (cliloc 1044465)</summary>
		public const int MSG_INSUFFICIENT_LOGS = 1044465;

		/// <summary>Resource name: "Tábuas Élficas" (cliloc 1044041)</summary>
		public const int MSG_ELVEN_BOARDS = 1044041;

		/// <summary>Resource name: "Tecido" (cliloc 1044286)</summary>
		public const int MSG_CLOTH = 1044286;

		/// <summary>Error: "Você não tem tecido suficiente para fazer isso." (cliloc 1044287)</summary>
		public const int MSG_INSUFFICIENT_CLOTH = 1044287;

		/// <summary>Resource name: "Couro" (cliloc 1044462)</summary>
		public const int MSG_LEATHER = 1044462;

		/// <summary>Error: "Você não tem couro suficiente para fazer isso." (cliloc 1044463)</summary>
		public const int MSG_INSUFFICIENT_LEATHER = 1044463;

		/// <summary>Resource name: "Lingotes de Ferro" (cliloc 1044036)</summary>
		public const int MSG_IRON_INGOTS = 1044036;

		/// <summary>Error: "Você não tem metal suficiente para fazer isso." (cliloc 1044037)</summary>
		public const int MSG_INSUFFICIENT_METAL = 1044037;

		/// <summary>Resource name: "Pergaminhos em Branco" (cliloc 1044377)</summary>
		public const int MSG_BLANK_SCROLLS = 1044377;

		/// <summary>Error: "Você não tem pergaminhos em branco suficientes para fazer isso." (cliloc 1044378)</summary>
		public const int MSG_INSUFFICIENT_BLANK_SCROLLS = 1044378;

		/// <summary>Resource name: "Penas" (cliloc 1044562)</summary>
		public const int MSG_FEATHERS = 1044562;

		/// <summary>Error: "Você não tem penas suficientes para fazer isso." (cliloc 1044563)</summary>
		public const int MSG_INSUFFICIENT_FEATHERS = 1044563;

		/// <summary>Resource name: "Hastes" (cliloc 1027125)</summary>
		public const int MSG_SHAFTS = 1027125;

		/// <summary>Error: "Você não tem hastes suficientes para fazer isso." (cliloc 1044561)</summary>
		public const int MSG_INSUFFICIENT_SHAFTS = 1044561;

		/// <summary>Resource name: "Aduelas de Barril" (cliloc 1044288)</summary>
		public const int MSG_BARREL_STAVES = 1044288;

		/// <summary>Resource name: "Aros de Barril" (cliloc 1044289)</summary>
		public const int MSG_BARREL_HOOPS = 1044289;

		/// <summary>Resource name: "Tampa de Barril" (cliloc 1044251)</summary>
		public const int MSG_BARREL_LID = 1044251;

		/// <summary>Resource name: "Torneira de Barril" (cliloc 1044252)</summary>
		public const int MSG_BARREL_TAP = 1044252;

		/// <summary>Error: "Você não tem os componentes necessários para fazer isso." (cliloc 1044253)</summary>
		public const int MSG_INSUFFICIENT_COMPONENTS = 1044253;

		/// <summary>Item name: "Aduelas de Barril" (cliloc 1027857)</summary>
		public const int MSG_BARREL_STAVES_ITEM = 1027857;

		/// <summary>Item name: "Tampa de Barril" (cliloc 1027608)</summary>
		public const int MSG_BARREL_LID_ITEM = 1027608;

		/// <summary>Item name: "Barril" (cliloc 1023711)</summary>
		public const int MSG_KEG = 1023711;

		/// <summary>Item name: "Vara de Pescar" (cliloc 1023519)</summary>
		public const int MSG_FISHING_POLE = 1023519;

		/// <summary>Item name: "Tela Shoji" (cliloc 1029423)</summary>
		public const int MSG_SHOJI_SCREEN = 1029423;

		/// <summary>Item name: "Tela de Bambu" (cliloc 1029428)</summary>
		public const int MSG_BAMBOO_SCREEN = 1029428;

		/// <summary>Item name: "Cavalete" (cliloc 1044317)</summary>
		public const int MSG_EASLE = 1044317;

		/// <summary>Item name: "Lanterna Pendurada Branca" (cliloc 1029416)</summary>
		public const int MSG_WHITE_HANGING_LANTERN = 1029416;

		/// <summary>Item name: "Lanterna Pendurada Vermelha" (cliloc 1029412)</summary>
		public const int MSG_RED_HANGING_LANTERN = 1029412;

		/// <summary>Error: "Você não tem recursos suficientes para fazer esse item." (cliloc 1042081)</summary>
		public const int MSG_INSUFFICIENT_RESOURCES = 1042081;

		/// <summary>Group name: "Armas & Escudos" (cliloc 1044295)</summary>
		public const int MSG_WEAPONS_AND_SHIELDS = 1044295;

		/// <summary>Item name: "Bokuto" (cliloc 1030227)</summary>
		public const int MSG_BOKUTO = 1030227;

		/// <summary>Item name: "Fukiya" (cliloc 1030229)</summary>
		public const int MSG_FUKIYA = 1030229;

		/// <summary>Item name: "Tetsubo" (cliloc 1030225)</summary>
		public const int MSG_TETSUBO = 1030225;

		#endregion

		#region Addon Message Numbers

		/// <summary>Addon: "Small Forge" (cliloc 1044330)</summary>
		public const int MSG_ADDON_SMALL_FORGE = 1044330;

		/// <summary>Addon: "Large Forge East" (cliloc 1044331)</summary>
		public const int MSG_ADDON_LARGE_FORGE_EAST = 1044331;

		/// <summary>Addon: "Large Forge South" (cliloc 1044332)</summary>
		public const int MSG_ADDON_LARGE_FORGE_SOUTH = 1044332;

		/// <summary>Addon: "Anvil East" (cliloc 1044333)</summary>
		public const int MSG_ADDON_ANVIL_EAST = 1044333;

		/// <summary>Addon: "Anvil South" (cliloc 1044334)</summary>
		public const int MSG_ADDON_ANVIL_SOUTH = 1044334;

		/// <summary>Addon: "Training Dummy East" (cliloc 1044335)</summary>
		public const int MSG_ADDON_TRAINING_DUMMY_EAST = 1044335;

		/// <summary>Addon: "Training Dummy South" (cliloc 1044336)</summary>
		public const int MSG_ADDON_TRAINING_DUMMY_SOUTH = 1044336;

		/// <summary>Addon: "Pickpocket Dip East" (cliloc 1044337)</summary>
		public const int MSG_ADDON_PICKPOCKET_DIP_EAST = 1044337;

		/// <summary>Addon: "Pickpocket Dip South" (cliloc 1044338)</summary>
		public const int MSG_ADDON_PICKPOCKET_DIP_SOUTH = 1044338;

		/// <summary>Addon: "Dressform" (cliloc 1044339)</summary>
		public const int MSG_ADDON_DRESSFORM = 1044339;

		/// <summary>Addon: "Spinningwheel East" (cliloc 1044341)</summary>
		public const int MSG_ADDON_SPINNINGWHEEL_EAST = 1044341;

		/// <summary>Addon: "Spinningwheel South" (cliloc 1044342)</summary>
		public const int MSG_ADDON_SPINNINGWHEEL_SOUTH = 1044342;

		/// <summary>Addon: "Loom East" (cliloc 1044343)</summary>
		public const int MSG_ADDON_LOOM_EAST = 1044343;

		/// <summary>Addon: "Loom South" (cliloc 1044344)</summary>
		public const int MSG_ADDON_LOOM_SOUTH = 1044344;

		/// <summary>Addon: "Stone Oven East" (cliloc 1044345)</summary>
		public const int MSG_ADDON_STONE_OVEN_EAST = 1044345;

		/// <summary>Addon: "Stone Oven South" (cliloc 1044346)</summary>
		public const int MSG_ADDON_STONE_OVEN_SOUTH = 1044346;

		/// <summary>Addon: "Flour Mill East" (cliloc 1044347)</summary>
		public const int MSG_ADDON_FLOUR_MILL_EAST = 1044347;

		/// <summary>Addon: "Flour Mill South" (cliloc 1044348)</summary>
		public const int MSG_ADDON_FLOUR_MILL_SOUTH = 1044348;

		/// <summary>Addon: "Water Trough East" (cliloc 1044349)</summary>
		public const int MSG_ADDON_WATER_TROUGH_EAST = 1044349;

		/// <summary>Addon: "Water Trough South" (cliloc 1044350)</summary>
		public const int MSG_ADDON_WATER_TROUGH_SOUTH = 1044350;

		/// <summary>Addon: "Dart Board South" (cliloc 1044325)</summary>
		public const int MSG_ADDON_DART_BOARD_SOUTH = 1044325;

		/// <summary>Addon: "Dart Board East" (cliloc 1044326)</summary>
		public const int MSG_ADDON_DART_BOARD_EAST = 1044326;

		#endregion

		#region Skill Requirements

		// Armories
		/// <summary>Minimum skill required for armories</summary>
		public const double SKILL_REQ_ARMOIRES_MIN = 51.5;

		/// <summary>Maximum skill required for armories</summary>
		public const double SKILL_REQ_ARMOIRES_MAX = 76.5;

		// Crates and Boxes
		/// <summary>Minimum skill required for small crate</summary>
		public const double SKILL_REQ_CRATE_SMALL_MIN = 10.0;

		/// <summary>Maximum skill required for small crate</summary>
		public const double SKILL_REQ_CRATE_SMALL_MAX = 35.0;

		/// <summary>Minimum skill required for medium crate</summary>
		public const double SKILL_REQ_CRATE_MEDIUM_MIN = 31.0;

		/// <summary>Maximum skill required for medium crate</summary>
		public const double SKILL_REQ_CRATE_MEDIUM_MAX = 56.0;

		/// <summary>Minimum skill required for large crate</summary>
		public const double SKILL_REQ_CRATE_LARGE_MIN = 47.3;

		/// <summary>Maximum skill required for large crate</summary>
		public const double SKILL_REQ_CRATE_LARGE_MAX = 72.3;

		/// <summary>Minimum skill required for wooden box</summary>
		public const double SKILL_REQ_WOODEN_BOX_MIN = 21.0;

		/// <summary>Maximum skill required for wooden box</summary>
		public const double SKILL_REQ_WOODEN_BOX_MAX = 46.0;

		/// <summary>Minimum skill required for wooden chest</summary>
		public const double SKILL_REQ_WOODEN_CHEST_MIN = 73.6;

		/// <summary>Maximum skill required for wooden chest</summary>
		public const double SKILL_REQ_WOODEN_CHEST_MAX = 98.6;

		/// <summary>Minimum skill required for finished/gilded chest</summary>
		public const double SKILL_REQ_FINISHED_CHEST_MIN = 90.0;

		/// <summary>Maximum skill required for finished/gilded chest</summary>
		public const double SKILL_REQ_FINISHED_CHEST_MAX = 115.0;

		/// <summary>Minimum skill required for simple coffin</summary>
		public const double SKILL_REQ_COFFIN_SIMPLE_MIN = 85.0;

		/// <summary>Maximum skill required for simple coffin</summary>
		public const double SKILL_REQ_COFFIN_SIMPLE_MAX = 90.0;

		/// <summary>Minimum skill required for elegant coffin</summary>
		public const double SKILL_REQ_COFFIN_ELEGANT_MIN = 90.0;

		/// <summary>Maximum skill required for elegant coffin</summary>
		public const double SKILL_REQ_COFFIN_ELEGANT_MAX = 95.0;

		// Dressers and Drawers
		/// <summary>Minimum skill required for dressers and drawers</summary>
		public const double SKILL_REQ_DRESSERS_MIN = 90.0;

		/// <summary>Maximum skill required for dressers and drawers</summary>
		public const double SKILL_REQ_DRESSERS_MAX = 115.0;

		// Shelves
		/// <summary>Minimum skill required for shelves</summary>
		public const double SKILL_REQ_SHELVES_MIN = 41.5;

		/// <summary>Maximum skill required for shelves</summary>
		public const double SKILL_REQ_SHELVES_MAX = 66.5;

		// Furniture - Seats
		/// <summary>Minimum skill required for foot stool</summary>
		public const double SKILL_REQ_FOOT_STOOL_MIN = 11.0;

		/// <summary>Maximum skill required for foot stool</summary>
		public const double SKILL_REQ_FOOT_STOOL_MAX = 28.0;

		/// <summary>Minimum skill required for stool</summary>
		public const double SKILL_REQ_STOOL_MIN = 27.0;

		/// <summary>Maximum skill required for stool</summary>
		public const double SKILL_REQ_STOOL_MAX = 36.0;

		/// <summary>Minimum skill required for wooden bench</summary>
		public const double SKILL_REQ_BENCH_MIN = 35.6;

		/// <summary>Maximum skill required for wooden bench</summary>
		public const double SKILL_REQ_BENCH_MAX = 47.6;

		/// <summary>Minimum skill required for bamboo chair</summary>
		public const double SKILL_REQ_BAMBOO_CHAIR_MIN = 46.0;

		/// <summary>Maximum skill required for bamboo chair</summary>
		public const double SKILL_REQ_BAMBOO_CHAIR_MAX = 50.0;

		/// <summary>Minimum skill required for wooden chair</summary>
		public const double SKILL_REQ_WOODEN_CHAIR_MIN = 49.0;

		/// <summary>Maximum skill required for wooden chair</summary>
		public const double SKILL_REQ_WOODEN_CHAIR_MAX = 56.0;

		/// <summary>Minimum skill required for cushioned chair</summary>
		public const double SKILL_REQ_CUSHIONED_CHAIR_MIN = 52.1;

		/// <summary>Maximum skill required for cushioned chair</summary>
		public const double SKILL_REQ_CUSHIONED_CHAIR_MAX = 59.1;

		/// <summary>Minimum skill required for fancy chair</summary>
		public const double SKILL_REQ_FANCY_CHAIR_MIN = 58.1;

		/// <summary>Maximum skill required for fancy chair</summary>
		public const double SKILL_REQ_FANCY_CHAIR_MAX = 67.1;

		/// <summary>Minimum skill required for simple throne</summary>
		public const double SKILL_REQ_THRONE_SIMPLE_MIN = 66.6;

		/// <summary>Maximum skill required for simple throne</summary>
		public const double SKILL_REQ_THRONE_SIMPLE_MAX = 71.6;

		/// <summary>Minimum skill required for elegant throne</summary>
		public const double SKILL_REQ_THRONE_ELEGANT_MIN = 70.6;

		/// <summary>Maximum skill required for elegant throne</summary>
		public const double SKILL_REQ_THRONE_ELEGANT_MAX = 77.6;

		/// <summary>Minimum skill required for elven throne</summary>
		public const double SKILL_REQ_THRONE_ELVEN_MIN = 77.0;

		/// <summary>Maximum skill required for elven throne</summary>
		public const double SKILL_REQ_THRONE_ELVEN_MAX = 82.0;

		// Furniture - Tables
		/// <summary>Minimum skill required for nightstand</summary>
		public const double SKILL_REQ_NIGHTSTAND_MIN = 42.1;

		/// <summary>Maximum skill required for nightstand</summary>
		public const double SKILL_REQ_NIGHTSTAND_MAX = 57.1;

		/// <summary>Minimum skill required for plain large table</summary>
		public const double SKILL_REQ_PLAIN_TABLE_MIN = 55.1;

		/// <summary>Maximum skill required for plain large table</summary>
		public const double SKILL_REQ_PLAIN_TABLE_MAX = 67.1;

		/// <summary>Minimum skill required for writing table</summary>
		public const double SKILL_REQ_WRITING_TABLE_MIN = 63.1;

		/// <summary>Maximum skill required for writing table</summary>
		public const double SKILL_REQ_WRITING_TABLE_MAX = 68.1;

		/// <summary>Minimum skill required for yew wood table</summary>
		public const double SKILL_REQ_YEW_TABLE_MIN = 64.1;

		/// <summary>Maximum skill required for yew wood table</summary>
		public const double SKILL_REQ_YEW_TABLE_MAX = 73.1;

		/// <summary>Minimum skill required for large table</summary>
		public const double SKILL_REQ_LARGE_TABLE_MIN = 69.2;

		/// <summary>Maximum skill required for large table</summary>
		public const double SKILL_REQ_LARGE_TABLE_MAX = 79.2;

		/// <summary>Minimum skill required for simple low table</summary>
		public const double SKILL_REQ_LOW_TABLE_SIMPLE_MIN = 79.0;

		/// <summary>Maximum skill required for simple low table</summary>
		public const double SKILL_REQ_LOW_TABLE_SIMPLE_MAX = 85.0;

		/// <summary>Minimum skill required for elegant low table</summary>
		public const double SKILL_REQ_LOW_TABLE_ELEGANT_MIN = 80.0;

		/// <summary>Maximum skill required for elegant low table</summary>
		public const double SKILL_REQ_LOW_TABLE_ELEGANT_MAX = 88.0;

		// Musical Instruments
		/// <summary>Minimum skill required for short music stand</summary>
		public const double SKILL_REQ_MUSIC_STAND_SHORT_MIN = 30.0;

		/// <summary>Maximum skill required for short music stand</summary>
		public const double SKILL_REQ_MUSIC_STAND_SHORT_MAX = 35.0;

		/// <summary>Minimum skill required for tall music stand</summary>
		public const double SKILL_REQ_MUSIC_STAND_TALL_MIN = 35.0;

		/// <summary>Maximum skill required for tall music stand</summary>
		public const double SKILL_REQ_MUSIC_STAND_TALL_MAX = 40.0;

		/// <summary>Minimum skill required for bamboo flute</summary>
		public const double SKILL_REQ_FLUTE_MIN = 70.0;

		/// <summary>Maximum skill required for bamboo flute</summary>
		public const double SKILL_REQ_FLUTE_MAX = 75.0;

		/// <summary>Minimum skill required for drums</summary>
		public const double SKILL_REQ_DRUMS_MIN = 75.0;

		/// <summary>Maximum skill required for drums</summary>
		public const double SKILL_REQ_DRUMS_MAX = 80.0;

		/// <summary>Minimum skill required for tambourine</summary>
		public const double SKILL_REQ_TAMBOURINE_MIN = 78.0;

		/// <summary>Maximum skill required for tambourine</summary>
		public const double SKILL_REQ_TAMBOURINE_MAX = 83.0;

		/// <summary>Minimum skill required for tambourine tassel</summary>
		public const double SKILL_REQ_TAMBOURINE_TASSEL_MIN = 80.0;

		/// <summary>Maximum skill required for tambourine tassel</summary>
		public const double SKILL_REQ_TAMBOURINE_TASSEL_MAX = 85.0;

		/// <summary>Minimum skill required for lap harp</summary>
		public const double SKILL_REQ_LAP_HARP_MIN = 84.0;

		/// <summary>Maximum skill required for lap harp</summary>
		public const double SKILL_REQ_LAP_HARP_MAX = 89.1;

		/// <summary>Minimum skill required for harp</summary>
		public const double SKILL_REQ_HARP_MIN = 88.9;

		/// <summary>Maximum skill required for harp</summary>
		public const double SKILL_REQ_HARP_MAX = 93.6;

		/// <summary>Minimum skill required for lute</summary>
		public const double SKILL_REQ_LUTE_MIN = 92.4;

		/// <summary>Maximum skill required for lute</summary>
		public const double SKILL_REQ_LUTE_MAX = 96.3;

		/// <summary>Minimum skill required for pipes</summary>
		public const double SKILL_REQ_PIPES_MIN = 95.8;

		/// <summary>Maximum skill required for pipes</summary>
		public const double SKILL_REQ_PIPES_MAX = 98.0;

		/// <summary>Minimum skill required for fiddle</summary>
		public const double SKILL_REQ_FIDDLE_MIN = 97.7;

		/// <summary>Maximum skill required for fiddle</summary>
		public const double SKILL_REQ_FIDDLE_MAX = 101.1;

		// Weapons and Shields
		/// <summary>Minimum skill required for shepherd's crook</summary>
		public const double SKILL_REQ_SHEPHERDS_CROOK_MIN = 50.0;

		/// <summary>Maximum skill required for shepherd's crook</summary>
		public const double SKILL_REQ_SHEPHERDS_CROOK_MAX = 57.9;

		/// <summary>Minimum skill required for wild staff</summary>
		public const double SKILL_REQ_WILD_STAFF_MIN = 56.9;

		/// <summary>Maximum skill required for wild staff</summary>
		public const double SKILL_REQ_WILD_STAFF_MAX = 60.9;

		/// <summary>Minimum skill required for quarter staff</summary>
		public const double SKILL_REQ_QUARTER_STAFF_MIN = 60.6;

		/// <summary>Maximum skill required for quarter staff</summary>
		public const double SKILL_REQ_QUARTER_STAFF_MAX = 65.6;

		/// <summary>Minimum skill required for gnarled staff</summary>
		public const double SKILL_REQ_GNARLED_STAFF_MIN = 64.9;

		/// <summary>Maximum skill required for gnarled staff</summary>
		public const double SKILL_REQ_GNARLED_STAFF_MAX = 70.9;

		/// <summary>Minimum skill required for wooden shield</summary>
		public const double SKILL_REQ_WOODEN_SHIELD_MIN = 70.6;

		/// <summary>Maximum skill required for wooden shield</summary>
		public const double SKILL_REQ_WOODEN_SHIELD_MAX = 75.6;

		/// <summary>Minimum skill required for bokuto</summary>
		public const double SKILL_REQ_BOKUTO_MIN = 73.0;

		/// <summary>Maximum skill required for bokuto</summary>
		public const double SKILL_REQ_BOKUTO_MAX = 78.5;

		/// <summary>Minimum skill required for fukiya</summary>
		public const double SKILL_REQ_FUKIYA_MIN = 78.0;

		/// <summary>Maximum skill required for fukiya</summary>
		public const double SKILL_REQ_FUKIYA_MAX = 81.0;

		/// <summary>Minimum skill required for tetsubo</summary>
		public const double SKILL_REQ_TETSUBO_MIN = 80.5;

		/// <summary>Maximum skill required for tetsubo</summary>
		public const double SKILL_REQ_TETSUBO_MAX = 85.3;

		// Wooden Armor
		/// <summary>Minimum skill required for wooden plate gorget</summary>
		public const double SKILL_REQ_WOODEN_ARMOR_GORGET_MIN = 56.4;

		/// <summary>Maximum skill required for wooden plate gorget</summary>
		public const double SKILL_REQ_WOODEN_ARMOR_GORGET_MAX = 106.4;

		/// <summary>Minimum skill required for wooden plate gloves</summary>
		public const double SKILL_REQ_WOODEN_ARMOR_GLOVES_MIN = 58.9;

		/// <summary>Maximum skill required for wooden plate gloves</summary>
		public const double SKILL_REQ_WOODEN_ARMOR_GLOVES_MAX = 108.9;

		/// <summary>Minimum skill required for wooden plate helm</summary>
		public const double SKILL_REQ_WOODEN_ARMOR_HELM_MIN = 62.6;

		/// <summary>Maximum skill required for wooden plate helm</summary>
		public const double SKILL_REQ_WOODEN_ARMOR_HELM_MAX = 112.6;

		/// <summary>Minimum skill required for wooden plate arms</summary>
		public const double SKILL_REQ_WOODEN_ARMOR_ARMS_MIN = 66.3;

		/// <summary>Maximum skill required for wooden plate arms</summary>
		public const double SKILL_REQ_WOODEN_ARMOR_ARMS_MAX = 116.3;

		/// <summary>Minimum skill required for wooden plate legs</summary>
		public const double SKILL_REQ_WOODEN_ARMOR_LEGS_MIN = 68.8;

		/// <summary>Maximum skill required for wooden plate legs</summary>
		public const double SKILL_REQ_WOODEN_ARMOR_LEGS_MAX = 118.8;

		/// <summary>Minimum skill required for wooden plate chest</summary>
		public const double SKILL_REQ_WOODEN_ARMOR_CHEST_MIN = 75.0;

		/// <summary>Maximum skill required for wooden plate chest</summary>
		public const double SKILL_REQ_WOODEN_ARMOR_CHEST_MAX = 125.0;

		// Addons
		/// <summary>Minimum skill required for dart board</summary>
		public const double SKILL_REQ_DART_BOARD_MIN = 15.7;

		/// <summary>Maximum skill required for dart board</summary>
		public const double SKILL_REQ_DART_BOARD_MAX = 40.7;

		/// <summary>Minimum skill required for training dummy</summary>
		public const double SKILL_REQ_TRAINING_DUMMY_MIN = 68.4;

		/// <summary>Maximum skill required for training dummy</summary>
		public const double SKILL_REQ_TRAINING_DUMMY_MAX = 93.4;

		/// <summary>Minimum skill required for pickpocket dip</summary>
		public const double SKILL_REQ_PICKPOCKET_DIP_MIN = 73.6;

		/// <summary>Maximum skill required for pickpocket dip</summary>
		public const double SKILL_REQ_PICKPOCKET_DIP_MAX = 98.6;

		/// <summary>Minimum skill required for dressform</summary>
		public const double SKILL_REQ_DRESSFORM_MIN = 63.1;

		/// <summary>Maximum skill required for dressform</summary>
		public const double SKILL_REQ_DRESSFORM_MAX = 88.1;

		/// <summary>Minimum skill required for small forge</summary>
		public const double SKILL_REQ_SMALL_FORGE_MIN = 73.6;

		/// <summary>Maximum skill required for small forge</summary>
		public const double SKILL_REQ_SMALL_FORGE_MAX = 98.6;

		/// <summary>Minimum skill required for large forge</summary>
		public const double SKILL_REQ_LARGE_FORGE_MIN = 78.9;

		/// <summary>Maximum skill required for large forge</summary>
		public const double SKILL_REQ_LARGE_FORGE_MAX = 103.9;

		/// <summary>Minimum skill required for anvil</summary>
		public const double SKILL_REQ_ANVIL_MIN = 73.6;

		/// <summary>Maximum skill required for anvil</summary>
		public const double SKILL_REQ_ANVIL_MAX = 98.6;

		/// <summary>Minimum skill required for spinningwheel/loom</summary>
		public const double SKILL_REQ_SPINNINGWHEEL_MIN = 73.6;

		/// <summary>Maximum skill required for spinningwheel/loom</summary>
		public const double SKILL_REQ_SPINNINGWHEEL_MAX = 98.6;

		/// <summary>Minimum skill required for loom</summary>
		public const double SKILL_REQ_LOOM_MIN = 84.2;

		/// <summary>Maximum skill required for loom</summary>
		public const double SKILL_REQ_LOOM_MAX = 109.2;

		/// <summary>Minimum skill required for stone oven</summary>
		public const double SKILL_REQ_STONE_OVEN_MIN = 68.4;

		/// <summary>Maximum skill required for stone oven</summary>
		public const double SKILL_REQ_STONE_OVEN_MAX = 93.4;

		/// <summary>Minimum skill required for flour mill</summary>
		public const double SKILL_REQ_FLOUR_MILL_MIN = 94.7;

		/// <summary>Maximum skill required for flour mill</summary>
		public const double SKILL_REQ_FLOUR_MILL_MAX = 119.7;

		// Misc Items
		/// <summary>Minimum skill required for kindling</summary>
		public const double SKILL_REQ_KINDLING_MIN = 0.0;

		/// <summary>Maximum skill required for kindling</summary>
		public const double SKILL_REQ_KINDLING_MAX = 10.0;

		/// <summary>Minimum skill required for kindling batch</summary>
		public const double SKILL_REQ_KINDLING_BATCH_MIN = 9.0;

		/// <summary>Maximum skill required for kindling batch</summary>
		public const double SKILL_REQ_KINDLING_BATCH_MAX = 15.0;

		/// <summary>Minimum skill required for bark fragment</summary>
		public const double SKILL_REQ_BARK_FRAGMENT_MIN = 14.0;

		/// <summary>Maximum skill required for bark fragment</summary>
		public const double SKILL_REQ_BARK_FRAGMENT_MAX = 20.0;

		/// <summary>Minimum skill required for barrel staves</summary>
		public const double SKILL_REQ_BARREL_STAVES_MIN = 18.0;

		/// <summary>Maximum skill required for barrel staves</summary>
		public const double SKILL_REQ_BARREL_STAVES_MAX = 25.0;

		/// <summary>Minimum skill required for barrel lid</summary>
		public const double SKILL_REQ_BARREL_LID_MIN = 23.0;

		/// <summary>Maximum skill required for barrel lid</summary>
		public const double SKILL_REQ_BARREL_LID_MAX = 30.2;

		/// <summary>Minimum skill required for mixing spoon</summary>
		public const double SKILL_REQ_MIXING_SPOON_MIN = 30.0;

		/// <summary>Maximum skill required for mixing spoon</summary>
		public const double SKILL_REQ_MIXING_SPOON_MAX = 40.0;

		/// <summary>Minimum skill required for keg</summary>
		public const double SKILL_REQ_KEG_MIN = 75.0;

		/// <summary>Maximum skill required for keg</summary>
		public const double SKILL_REQ_KEG_MAX = 80.8;

		/// <summary>Minimum skill required for alchemy tub</summary>
		public const double SKILL_REQ_ALCHEMY_TUB_MIN = 87.8;

		/// <summary>Maximum skill required for alchemy tub</summary>
		public const double SKILL_REQ_ALCHEMY_TUB_MAX = 102.8;

		/// <summary>Minimum skill required for fishing pole</summary>
		public const double SKILL_REQ_FISHING_POLE_MIN = 50.0;

		/// <summary>Maximum skill required for fishing pole</summary>
		public const double SKILL_REQ_FISHING_POLE_MAX = 60.4;

		/// <summary>Minimum skill required for shoji screen</summary>
		public const double SKILL_REQ_SHOJI_SCREEN_MIN = 60.0;

		/// <summary>Maximum skill required for shoji screen</summary>
		public const double SKILL_REQ_SHOJI_SCREEN_MAX = 65.0;

		/// <summary>Minimum skill required for bamboo screen</summary>
		public const double SKILL_REQ_BAMBOO_SCREEN_MIN = 65.0;

		/// <summary>Maximum skill required for bamboo screen</summary>
		public const double SKILL_REQ_BAMBOO_SCREEN_MAX = 70.0;

		/// <summary>Minimum skill required for easle</summary>
		public const double SKILL_REQ_EASLE_MIN = 70.0;

		/// <summary>Maximum skill required for easle</summary>
		public const double SKILL_REQ_EASLE_MAX = 75.0;

		/// <summary>Minimum skill required for white hanging lantern</summary>
		public const double SKILL_REQ_WHITE_LANTERN_MIN = 60.0;

		/// <summary>Maximum skill required for white hanging lantern</summary>
		public const double SKILL_REQ_WHITE_LANTERN_MAX = 65.0;

		/// <summary>Minimum skill required for red hanging lantern</summary>
		public const double SKILL_REQ_RED_LANTERN_MIN = 65.0;

		/// <summary>Maximum skill required for red hanging lantern</summary>
		public const double SKILL_REQ_RED_LANTERN_MAX = 70.0;

		// Wood Type Requirements
		/// <summary>Skill required for common wood</summary>
		public const double SKILL_REQ_WOOD_COMMON = 0.0;

		/// <summary>Skill required for ash wood</summary>
		public const double SKILL_REQ_WOOD_ASH = 60.0;

		/// <summary>Skill required for ebony wood</summary>
		public const double SKILL_REQ_WOOD_EBONY = 70.0;

		/// <summary>Skill required for elven wood</summary>
		public const double SKILL_REQ_WOOD_ELVEN = 80.0;

		/// <summary>Skill required for golden oak wood</summary>
		public const double SKILL_REQ_WOOD_GOLDEN_OAK = 85.0;

		/// <summary>Skill required for cherry wood</summary>
		public const double SKILL_REQ_WOOD_CHERRY = 90.0;

		/// <summary>Skill required for rosewood</summary>
		public const double SKILL_REQ_WOOD_ROSEWOOD = 95.0;

		/// <summary>Skill required for hickory wood</summary>
		public const double SKILL_REQ_WOOD_HICKORY = 100.0;

		// Secondary Skill Requirements
		/// <summary>Minimum forensics skill for simple coffin</summary>
		public const double SKILL_REQ_FORENSICS_COFFIN_SIMPLE_MIN = 60.0;

		/// <summary>Maximum forensics skill for simple coffin</summary>
		public const double SKILL_REQ_FORENSICS_COFFIN_SIMPLE_MAX = 70.0;

		/// <summary>Minimum forensics skill for elegant coffin</summary>
		public const double SKILL_REQ_FORENSICS_COFFIN_ELEGANT_MIN = 70.0;

		/// <summary>Maximum forensics skill for elegant coffin</summary>
		public const double SKILL_REQ_FORENSICS_COFFIN_ELEGANT_MAX = 80.0;

		/// <summary>Minimum musicianship skill for bamboo flute</summary>
		public const double SKILL_REQ_MUSICIANSHIP_FLUTE_MIN = 30.0;

		/// <summary>Maximum musicianship skill for bamboo flute</summary>
		public const double SKILL_REQ_MUSICIANSHIP_FLUTE_MAX = 45.0;

		/// <summary>Minimum musicianship skill for drums</summary>
		public const double SKILL_REQ_MUSICIANSHIP_DRUMS_MIN = 45.0;

		/// <summary>Maximum musicianship skill for drums</summary>
		public const double SKILL_REQ_MUSICIANSHIP_DRUMS_MAX = 50.0;

		/// <summary>Minimum musicianship skill for tambourine</summary>
		public const double SKILL_REQ_MUSICIANSHIP_TAMBOURINE_MIN = 50.0;

		/// <summary>Maximum musicianship skill for tambourine</summary>
		public const double SKILL_REQ_MUSICIANSHIP_TAMBOURINE_MAX = 55.0;

		/// <summary>Minimum musicianship skill for tambourine tassel</summary>
		public const double SKILL_REQ_MUSICIANSHIP_TAMBOURINE_TASSEL_MIN = 55.0;

		/// <summary>Maximum musicianship skill for tambourine tassel</summary>
		public const double SKILL_REQ_MUSICIANSHIP_TAMBOURINE_TASSEL_MAX = 60.0;

		/// <summary>Minimum musicianship skill for lap harp</summary>
		public const double SKILL_REQ_MUSICIANSHIP_LAP_HARP_MIN = 60.0;

		/// <summary>Maximum musicianship skill for lap harp</summary>
		public const double SKILL_REQ_MUSICIANSHIP_LAP_HARP_MAX = 65.0;

		/// <summary>Minimum musicianship skill for harp</summary>
		public const double SKILL_REQ_MUSICIANSHIP_HARP_MIN = 65.0;

		/// <summary>Maximum musicianship skill for harp</summary>
		public const double SKILL_REQ_MUSICIANSHIP_HARP_MAX = 70.0;

		/// <summary>Minimum musicianship skill for lute</summary>
		public const double SKILL_REQ_MUSICIANSHIP_LUTE_MIN = 75.0;

		/// <summary>Maximum musicianship skill for lute</summary>
		public const double SKILL_REQ_MUSICIANSHIP_LUTE_MAX = 80.0;

		/// <summary>Minimum musicianship skill for pipes</summary>
		public const double SKILL_REQ_MUSICIANSHIP_PIPES_MIN = 80.0;

		/// <summary>Maximum musicianship skill for pipes</summary>
		public const double SKILL_REQ_MUSICIANSHIP_PIPES_MAX = 90.0;

		/// <summary>Minimum musicianship skill for fiddle</summary>
		public const double SKILL_REQ_MUSICIANSHIP_FIDDLE_MIN = 90.0;

		/// <summary>Maximum musicianship skill for fiddle</summary>
		public const double SKILL_REQ_MUSICIANSHIP_FIDDLE_MAX = 100.0;

		/// <summary>Minimum blacksmith skill for forge addons</summary>
		public const double SKILL_REQ_BLACKSMITH_FORGE_SMALL_MIN = 75.0;

		/// <summary>Maximum blacksmith skill for forge addons</summary>
		public const double SKILL_REQ_BLACKSMITH_FORGE_SMALL_MAX = 80.0;

		/// <summary>Minimum blacksmith skill for large forge</summary>
		public const double SKILL_REQ_BLACKSMITH_FORGE_LARGE_MIN = 80.0;

		/// <summary>Maximum blacksmith skill for large forge</summary>
		public const double SKILL_REQ_BLACKSMITH_FORGE_LARGE_MAX = 85.0;

		/// <summary>Minimum blacksmith skill for anvil</summary>
		public const double SKILL_REQ_BLACKSMITH_ANVIL_MIN = 75.0;

		/// <summary>Maximum blacksmith skill for anvil</summary>
		public const double SKILL_REQ_BLACKSMITH_ANVIL_MAX = 80.0;

		/// <summary>Minimum tailoring skill for training dummy</summary>
		public const double SKILL_REQ_TAILORING_TRAINING_DUMMY_MIN = 50.0;

		/// <summary>Maximum tailoring skill for training dummy</summary>
		public const double SKILL_REQ_TAILORING_TRAINING_DUMMY_MAX = 55.0;

		/// <summary>Minimum tailoring skill for pickpocket dip</summary>
		public const double SKILL_REQ_TAILORING_PICKPOCKET_DIP_MIN = 50.0;

		/// <summary>Maximum tailoring skill for pickpocket dip</summary>
		public const double SKILL_REQ_TAILORING_PICKPOCKET_DIP_MAX = 55.0;

		/// <summary>Minimum tailoring skill for dressform</summary>
		public const double SKILL_REQ_TAILORING_DRESSFORM_MIN = 65.0;

		/// <summary>Maximum tailoring skill for dressform</summary>
		public const double SKILL_REQ_TAILORING_DRESSFORM_MAX = 70.0;

		/// <summary>Minimum tailoring skill for spinningwheel/loom</summary>
		public const double SKILL_REQ_TAILORING_SPINNINGWHEEL_MIN = 65.0;

		/// <summary>Maximum tailoring skill for spinningwheel/loom</summary>
		public const double SKILL_REQ_TAILORING_SPINNINGWHEEL_MAX = 70.0;

		/// <summary>Minimum tailoring skill for fishing pole</summary>
		public const double SKILL_REQ_TAILORING_FISHING_POLE_MIN = 40.0;

		/// <summary>Maximum tailoring skill for fishing pole</summary>
		public const double SKILL_REQ_TAILORING_FISHING_POLE_MAX = 45.0;

		/// <summary>Minimum tailoring skill for screens</summary>
		public const double SKILL_REQ_TAILORING_SCREENS_MIN = 50.0;

		/// <summary>Maximum tailoring skill for screens</summary>
		public const double SKILL_REQ_TAILORING_SCREENS_MAX = 55.0;

		/// <summary>Minimum tinkering skill for cooking addons</summary>
		public const double SKILL_REQ_TINKERING_COOKING_MIN = 50.0;

		/// <summary>Maximum tinkering skill for cooking addons</summary>
		public const double SKILL_REQ_TINKERING_COOKING_MAX = 55.0;

		#endregion

		#region Resource Amounts

		// Armories
		/// <summary>Board amount for small armories</summary>
		public const int RESOURCE_BOARDS_ARMOIRE_SMALL = 25;

		/// <summary>Board amount for medium armories</summary>
		public const int RESOURCE_BOARDS_ARMOIRE_MEDIUM = 30;

		/// <summary>Board amount for large armories</summary>
		public const int RESOURCE_BOARDS_ARMOIRE_LARGE = 35;

		// Crates and Boxes
		/// <summary>Board amount for small crate</summary>
		public const int RESOURCE_BOARDS_CRATE_SMALL = 8;

		/// <summary>Board amount for medium crate</summary>
		public const int RESOURCE_BOARDS_CRATE_MEDIUM = 15;

		/// <summary>Board amount for large crate</summary>
		public const int RESOURCE_BOARDS_CRATE_LARGE = 18;

		/// <summary>Board amount for wooden box</summary>
		public const int RESOURCE_BOARDS_WOODEN_BOX = 10;

		/// <summary>Board amount for wooden chest</summary>
		public const int RESOURCE_BOARDS_WOODEN_CHEST = 20;

		/// <summary>Board amount for finished/gilded chest</summary>
		public const int RESOURCE_BOARDS_FINISHED_CHEST = 30;

		/// <summary>Board amount for coffins</summary>
		public const int RESOURCE_BOARDS_COFFIN = 40;

		// Dressers and Drawers
		/// <summary>Board amount for dressers (small)</summary>
		public const int RESOURCE_BOARDS_DRESSER_SMALL = 30;

		/// <summary>Board amount for dressers (medium)</summary>
		public const int RESOURCE_BOARDS_DRESSER_MEDIUM = 35;

		/// <summary>Board amount for dressers (large)</summary>
		public const int RESOURCE_BOARDS_DRESSER_LARGE = 40;

		// Shelves
		/// <summary>Board amount for small shelves</summary>
		public const int RESOURCE_BOARDS_SHELF_SMALL = 25;

		/// <summary>Board amount for large shelves</summary>
		public const int RESOURCE_BOARDS_SHELF_LARGE = 35;

		// Furniture - Seats
		/// <summary>Board amount for simple seats</summary>
		public const int RESOURCE_BOARDS_SEAT_SIMPLE = 9;

		/// <summary>Board amount for bench</summary>
		public const int RESOURCE_BOARDS_BENCH = 17;

		/// <summary>Board amount for chairs</summary>
		public const int RESOURCE_BOARDS_CHAIR = 13;

		/// <summary>Board amount for fancy chair</summary>
		public const int RESOURCE_BOARDS_CHAIR_FANCY = 15;

		/// <summary>Board amount for simple throne</summary>
		public const int RESOURCE_BOARDS_THRONE_SIMPLE = 19;

		/// <summary>Board amount for elegant throne</summary>
		public const int RESOURCE_BOARDS_THRONE_ELEGANT = 17;

		/// <summary>Board amount for elven throne</summary>
		public const int RESOURCE_BOARDS_THRONE_ELVEN = 30;

		// Furniture - Tables
		/// <summary>Board amount for nightstand</summary>
		public const int RESOURCE_BOARDS_NIGHTSTAND = 17;

		/// <summary>Board amount for plain large table</summary>
		public const int RESOURCE_BOARDS_TABLE_PLAIN = 23;

		/// <summary>Board amount for writing table</summary>
		public const int RESOURCE_BOARDS_TABLE_WRITING = 20;

		/// <summary>Board amount for large table</summary>
		public const int RESOURCE_BOARDS_TABLE_LARGE = 27;

		/// <summary>Board amount for simple low table</summary>
		public const int RESOURCE_BOARDS_TABLE_LOW_SIMPLE = 35;

		/// <summary>Board amount for elegant low table</summary>
		public const int RESOURCE_BOARDS_TABLE_LOW_ELEGANT = 40;

		// Musical Instruments
		/// <summary>Board amount for short music stand</summary>
		public const int RESOURCE_BOARDS_MUSIC_STAND_SHORT = 15;

		/// <summary>Board amount for tall music stand</summary>
		public const int RESOURCE_BOARDS_MUSIC_STAND_TALL = 22;

		/// <summary>Board amount for bamboo flute</summary>
		public const int RESOURCE_BOARDS_FLUTE = 15;

		/// <summary>Board amount for drums</summary>
		public const int RESOURCE_BOARDS_DRUMS = 23;

		/// <summary>Board amount for tambourine</summary>
		public const int RESOURCE_BOARDS_TAMBOURINE = 16;

		/// <summary>Board amount for lap harp</summary>
		public const int RESOURCE_BOARDS_LAP_HARP = 22;

		/// <summary>Board amount for harp</summary>
		public const int RESOURCE_BOARDS_HARP = 30;

		/// <summary>Board amount for lute</summary>
		public const int RESOURCE_BOARDS_LUTE = 24;

		/// <summary>Board amount for pipes</summary>
		public const int RESOURCE_BOARDS_PIPES = 28;

		/// <summary>Board amount for fiddle</summary>
		public const int RESOURCE_BOARDS_FIDDLE = 31;

		// Weapons and Shields
		/// <summary>Board amount for shepherd's crook</summary>
		public const int RESOURCE_BOARDS_SHEPHERDS_CROOK = 7;

		/// <summary>Board amount for wild staff</summary>
		public const int RESOURCE_BOARDS_WILD_STAFF = 8;

		/// <summary>Board amount for quarter staff</summary>
		public const int RESOURCE_BOARDS_QUARTER_STAFF = 8;

		/// <summary>Board amount for gnarled staff</summary>
		public const int RESOURCE_BOARDS_GNARLED_STAFF = 7;

		/// <summary>Board amount for wooden shield</summary>
		public const int RESOURCE_BOARDS_WOODEN_SHIELD = 12;

		/// <summary>Board amount for bokuto</summary>
		public const int RESOURCE_BOARDS_BOKUTO = 9;

		/// <summary>Board amount for fukiya</summary>
		public const int RESOURCE_BOARDS_FUKIYA = 9;

		/// <summary>Board amount for tetsubo</summary>
		public const int RESOURCE_BOARDS_TETSUBO = 13;

		// Wooden Armor
		/// <summary>Reaper oil amount for wooden plate gorget</summary>
		public const int RESOURCE_REAPER_OIL_ARMOR_GORGET = 1;

		/// <summary>Reaper oil amount for wooden plate gloves</summary>
		public const int RESOURCE_REAPER_OIL_ARMOR_GLOVES = 1;

		/// <summary>Reaper oil amount for wooden plate helm</summary>
		public const int RESOURCE_REAPER_OIL_ARMOR_HELM = 1;

		/// <summary>Reaper oil amount for wooden plate arms</summary>
		public const int RESOURCE_REAPER_OIL_ARMOR_ARMS = 2;

		/// <summary>Reaper oil amount for wooden plate legs</summary>
		public const int RESOURCE_REAPER_OIL_ARMOR_LEGS = 3;

		/// <summary>Reaper oil amount for wooden plate chest</summary>
		public const int RESOURCE_REAPER_OIL_ARMOR_CHEST = 3;

		/// <summary>Mystical tree sap amount for wooden plate gorget</summary>
		public const int RESOURCE_SAP_ARMOR_GORGET = 1;

		/// <summary>Mystical tree sap amount for wooden plate gloves</summary>
		public const int RESOURCE_SAP_ARMOR_GLOVES = 1;

		/// <summary>Mystical tree sap amount for wooden plate helm</summary>
		public const int RESOURCE_SAP_ARMOR_HELM = 1;

		/// <summary>Mystical tree sap amount for wooden plate arms</summary>
		public const int RESOURCE_SAP_ARMOR_ARMS = 2;

		/// <summary>Mystical tree sap amount for wooden plate legs</summary>
		public const int RESOURCE_SAP_ARMOR_LEGS = 3;

		/// <summary>Mystical tree sap amount for wooden plate chest</summary>
		public const int RESOURCE_SAP_ARMOR_CHEST = 3;

		/// <summary>Board amount for wooden plate gorget</summary>
		public const int RESOURCE_BOARDS_ARMOR_GORGET = 10;

		/// <summary>Board amount for wooden plate gloves</summary>
		public const int RESOURCE_BOARDS_ARMOR_GLOVES = 12;

		/// <summary>Board amount for wooden plate helm</summary>
		public const int RESOURCE_BOARDS_ARMOR_HELM = 15;

		/// <summary>Board amount for wooden plate arms</summary>
		public const int RESOURCE_BOARDS_ARMOR_ARMS = 18;

		/// <summary>Board amount for wooden plate legs</summary>
		public const int RESOURCE_BOARDS_ARMOR_LEGS = 20;

		/// <summary>Board amount for wooden plate chest</summary>
		public const int RESOURCE_BOARDS_ARMOR_CHEST = 25;

		// Addons
		/// <summary>Board amount for addons (small)</summary>
		public const int RESOURCE_BOARDS_ADDON_SMALL = 5;

		/// <summary>Board amount for dressform</summary>
		public const int RESOURCE_BOARDS_ADDON_DRESSFORM = 25;

		/// <summary>Board amount for training dummy</summary>
		public const int RESOURCE_BOARDS_ADDON_TRAINING_DUMMY = 55;

		/// <summary>Board amount for pickpocket dip</summary>
		public const int RESOURCE_BOARDS_ADDON_PICKPOCKET_DIP = 65;

		/// <summary>Board amount for spinningwheel</summary>
		public const int RESOURCE_BOARDS_ADDON_SPINNINGWHEEL = 75;

		/// <summary>Board amount for loom</summary>
		public const int RESOURCE_BOARDS_ADDON_LOOM = 85;

		/// <summary>Board amount for stone oven</summary>
		public const int RESOURCE_BOARDS_ADDON_STONE_OVEN = 85;

		/// <summary>Board amount for flour mill</summary>
		public const int RESOURCE_BOARDS_ADDON_FLOUR_MILL = 100;

		/// <summary>Board amount for water trough</summary>
		public const int RESOURCE_BOARDS_ADDON_WATER_TROUGH = 150;

		/// <summary>Iron ingot amount for small forge</summary>
		public const int RESOURCE_IRON_INGOTS_ADDON_SMALL_FORGE = 75;

		/// <summary>Iron ingot amount for large forge</summary>
		public const int RESOURCE_IRON_INGOTS_ADDON_LARGE_FORGE = 100;

		/// <summary>Iron ingot amount for anvil</summary>
		public const int RESOURCE_IRON_INGOTS_ADDON_ANVIL = 150;

		/// <summary>Cloth amount for training dummy</summary>
		public const int RESOURCE_CLOTH_ADDON_TRAINING_DUMMY = 60;

		/// <summary>Cloth amount for pickpocket dip</summary>
		public const int RESOURCE_CLOTH_ADDON_PICKPOCKET_DIP = 60;

		/// <summary>Cloth amount for dressform</summary>
		public const int RESOURCE_CLOTH_ADDON_DRESSFORM = 10;

		/// <summary>Cloth amount for spinningwheel/loom</summary>
		public const int RESOURCE_CLOTH_ADDON_SPINNINGWHEEL = 25;

		/// <summary>Iron ingot amount for stone oven</summary>
		public const int RESOURCE_IRON_INGOTS_ADDON_STONE_OVEN = 125;

		/// <summary>Iron ingot amount for flour mill</summary>
		public const int RESOURCE_IRON_INGOTS_ADDON_FLOUR_MILL = 50;

		// Misc Items
		/// <summary>Log amount for kindling</summary>
		public const int RESOURCE_LOGS_KINDLING = 1;

		/// <summary>Board amount for bark fragment</summary>
		public const int RESOURCE_BOARDS_BARK_FRAGMENT = 2;

		/// <summary>Board amount for barrel staves</summary>
		public const int RESOURCE_BOARDS_BARREL_STAVES = 5;

		/// <summary>Board amount for barrel lid</summary>
		public const int RESOURCE_BOARDS_BARREL_LID = 4;

		/// <summary>Board amount for mixing spoon</summary>
		public const int RESOURCE_BOARDS_MIXING_SPOON = 5;

		/// <summary>Barrel staves amount for keg</summary>
		public const int RESOURCE_BARREL_STAVES_KEG = 4;

		/// <summary>Barrel hoops amount for keg</summary>
		public const int RESOURCE_BARREL_HOOPS_KEG = 1;

		/// <summary>Barrel lid amount for keg</summary>
		public const int RESOURCE_BARREL_LID_KEG = 2;

		/// <summary>Barrel tap amount for keg</summary>
		public const int RESOURCE_BARREL_TAP_KEG = 1;

		/// <summary>Barrel staves amount for alchemy tub</summary>
		public const int RESOURCE_BARREL_STAVES_ALCHEMY_TUB = 4;

		/// <summary>Barrel hoops amount for alchemy tub</summary>
		public const int RESOURCE_BARREL_HOOPS_ALCHEMY_TUB = 1;

		/// <summary>Barrel lid amount for alchemy tub</summary>
		public const int RESOURCE_BARREL_LID_ALCHEMY_TUB = 1;

		/// <summary>Board amount for fishing pole</summary>
		public const int RESOURCE_BOARDS_FISHING_POLE = 5;

		/// <summary>Cloth amount for fishing pole</summary>
		public const int RESOURCE_CLOTH_FISHING_POLE = 5;

		/// <summary>Board amount for shoji screen</summary>
		public const int RESOURCE_BOARDS_SHOJI_SCREEN = 75;

		/// <summary>Cloth amount for screens</summary>
		public const int RESOURCE_CLOTH_SCREENS = 60;

		/// <summary>Board amount for bamboo screen</summary>
		public const int RESOURCE_BOARDS_BAMBOO_SCREEN = 75;

		/// <summary>Board amount for easle</summary>
		public const int RESOURCE_BOARDS_EASLE = 20;

		/// <summary>Board amount for hanging lanterns</summary>
		public const int RESOURCE_BOARDS_LANTERN = 6;

		/// <summary>Blank scroll amount for hanging lanterns</summary>
		public const int RESOURCE_BLANK_SCROLLS_LANTERN = 10;

		// Musical Instrument Resources
		/// <summary>Cloth amount for drums</summary>
		public const int RESOURCE_CLOTH_DRUMS = 10;

		/// <summary>Leather amount for tambourine</summary>
		public const int RESOURCE_LEATHER_TAMBOURINE = 5;

		/// <summary>Iron ingot amount for tambourine</summary>
		public const int RESOURCE_IRON_INGOTS_TAMBOURINE = 4;

		/// <summary>Cloth amount for tambourine tassel</summary>
		public const int RESOURCE_CLOTH_TAMBOURINE_TASSEL = 4;

		/// <summary>Iron ingot amount for lap harp</summary>
		public const int RESOURCE_IRON_INGOTS_LAP_HARP = 5;

		/// <summary>Iron ingot amount for harp</summary>
		public const int RESOURCE_IRON_INGOTS_HARP = 10;

		/// <summary>Iron ingot amount for lute</summary>
		public const int RESOURCE_IRON_INGOTS_LUTE = 7;

		/// <summary>Iron ingot amount for pipes</summary>
		public const int RESOURCE_IRON_INGOTS_PIPES = 6;

		/// <summary>Iron ingot amount for fiddle</summary>
		public const int RESOURCE_IRON_INGOTS_FIDDLE = 8;

		/// <summary>Shaft amount for fiddle</summary>
		public const int RESOURCE_SHAFTS_FIDDLE = 2;

		// Weapon Resources
		/// <summary>Feather amount for wild staff</summary>
		public const int RESOURCE_FEATHERS_WILD_STAFF = 4;

		/// <summary>Cloth amount for bokuto</summary>
		public const int RESOURCE_CLOTH_BOKUTO = 4;

		/// <summary>Iron ingot amount for fukiya</summary>
		public const int RESOURCE_IRON_INGOTS_FUKIYA = 2;

		/// <summary>Iron ingot amount for tetsubo</summary>
		public const int RESOURCE_IRON_INGOTS_TETSUBO = 5;

		#endregion
	}
}


namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for Bow Fletching crafting system calculations and mechanics.
	/// Extracted from DefBowFletching.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class BowFletchingConstants
	{
		#region Craft System Configuration

		/// <summary>Minimum chance at minimum skill (50%)</summary>
		public const double CHANCE_AT_MIN_SKILL = 0.5;

		/// <summary>Minimum craft effect value</summary>
		public const int MIN_CRAFT_EFFECT = 1;

		/// <summary>Maximum craft effect value</summary>
		public const int MAX_CRAFT_EFFECT = 1;

		/// <summary>Craft delay multiplier</summary>
		public const double CRAFT_DELAY_MULTIPLIER = 1.25;

		/// <summary>Sound ID for fletching craft effect (0x55)</summary>
		public const int SOUND_FLETCHING_CRAFT = 0x55;

		#endregion

		#region Message Numbers (Cliloc)

		/// <summary>Gump title: "MENU DE ARQUEIRIA E FLECHARIA" (cliloc 1044010)</summary>
		public const int MSG_GUMP_TITLE = 1044006;

		/// <summary>Message: "Você quebrou sua ferramenta!"</summary>
		public const string MSG_TOOL_WORN_OUT = BowFletchingStringConstants.MSG_TOOL_WORN_OUT;

		/// <summary>Message: "A ferramenta deve estar com você para usar."</summary>
		public const string MSG_TOOL_MUST_BE_ON_PERSON = BowFletchingStringConstants.MSG_TOOL_MUST_BE_ON_PERSON;

		/// <summary>Message: "Você falhou ao criar o item, e alguns de seus materiais foram perdidos."</summary>
		public const string MSG_FAILED_LOST_MATERIALS = BowFletchingStringConstants.MSG_FAILED_LOST_MATERIALS;

		/// <summary>Message: "Você falhou ao criar o item, mas nenhum material foi perdido."</summary>
		public const string MSG_FAILED_NO_MATERIALS_LOST = BowFletchingStringConstants.MSG_FAILED_NO_MATERIALS_LOST;

		/// <summary>Message: "Você mal conseguiu fazer este item. Sua qualidade está abaixo da média."</summary>
		public const string MSG_BARELY_MADE_ITEM = BowFletchingStringConstants.MSG_BARELY_MADE_ITEM;

		/// <summary>Message: "Você cria um item de qualidade excepcional e assina a sua marca nele."</summary>
		public const string MSG_EXCEPTIONAL_WITH_MARK = BowFletchingStringConstants.MSG_EXCEPTIONAL_WITH_MARK;

		/// <summary>Message: "Você cria um item de qualidade excepcional."</summary>
		public const string MSG_EXCEPTIONAL_QUALITY = BowFletchingStringConstants.MSG_EXCEPTIONAL_QUALITY;

		/// <summary>Message: "Você cria o item."</summary>
		public const string MSG_ITEM_CREATED = BowFletchingStringConstants.MSG_ITEM_CREATED;

		/// <summary>Group name: "Materiais"</summary>
		public const string MSG_GROUP_MATERIALS = BowFletchingStringConstants.GROUP_MATERIALS;

		/// <summary>Group name: "Munição"</summary>
		public const string MSG_GROUP_AMMUNITION = BowFletchingStringConstants.GROUP_AMMUNITION;

		/// <summary>Group name: "Armas"</summary>
		public const string MSG_GROUP_WEAPONS = BowFletchingStringConstants.GROUP_WEAPONS;

		/// <summary>Resource name: "Tora"</summary>
		public const string MSG_LOG = BowFletchingStringConstants.RESOURCE_LOG;

		/// <summary>Resource name: "Tábuas"</summary>
		public const string MSG_BOARD = BowFletchingStringConstants.RESOURCE_BOARDS;

		/// <summary>Error: "Você não tem madeira suficiente para fazer isso."</summary>
		public const string MSG_INSUFFICIENT_WOOD = BowFletchingStringConstants.MSG_INSUFFICIENT_WOOD;

		/// <summary>Item name: "haste"</summary>
		public const string MSG_SHAFT_ITEM = BowFletchingStringConstants.ITEM_SHAFT;

		/// <summary>Resource name: "Haste"</summary>
		public const string MSG_SHAFT_RESOURCE = BowFletchingStringConstants.RESOURCE_SHAFT;

		/// <summary>Error: "Você não tem hastes suficientes."</summary>
		public const string MSG_INSUFFICIENT_SHAFTS = BowFletchingStringConstants.MSG_INSUFFICIENT_SHAFTS;

		/// <summary>Resource name: "Pena"</summary>
		public const string MSG_FEATHER = BowFletchingStringConstants.RESOURCE_FEATHER;

		/// <summary>Error: "Você não tem penas suficientes."</summary>
		public const string MSG_INSUFFICIENT_FEATHERS = BowFletchingStringConstants.MSG_INSUFFICIENT_FEATHERS;

		/// <summary>Item name: "flecha"</summary>
		public const string MSG_ARROW = BowFletchingStringConstants.ITEM_ARROW;

		/// <summary>Item name: "virote"</summary>
		public const string MSG_BOLT = BowFletchingStringConstants.ITEM_BOLT;

		/// <summary>Item name: "dardos fukiya"</summary>
		public const string MSG_FUKIYA_DARTS = BowFletchingStringConstants.ITEM_FUKIYA_DARTS;

		/// <summary>Item name: "arma de arremesso"</summary>
		public const string MSG_THROWING_WEAPON = BowFletchingStringConstants.ITEM_THROWING_WEAPON;

		/// <summary>Resource name: "Lingote de Ferro"</summary>
		public const string MSG_IRON_INGOT = BowFletchingStringConstants.RESOURCE_IRON_INGOT;

		/// <summary>Error: "Você não tem metal suficiente."</summary>
		public const string MSG_INSUFFICIENT_METAL = BowFletchingStringConstants.MSG_INSUFFICIENT_METAL;

		/// <summary>Item name: "arco"</summary>
		public const string MSG_BOW = BowFletchingStringConstants.ITEM_BOW;

		/// <summary>Item name: "besta"</summary>
		public const string MSG_CROSSBOW = BowFletchingStringConstants.ITEM_CROSSBOW;

		/// <summary>Item name: "besta pesada"</summary>
		public const string MSG_HEAVY_CROSSBOW = BowFletchingStringConstants.ITEM_HEAVY_CROSSBOW;

		/// <summary>Item name: "arco composto"</summary>
		public const string MSG_COMPOSITE_BOW = BowFletchingStringConstants.ITEM_COMPOSITE_BOW;

		/// <summary>Item name: "besta repetidora"</summary>
		public const string MSG_REPEATING_CROSSBOW = BowFletchingStringConstants.ITEM_REPEATING_CROSSBOW;

		/// <summary>Item name: "yumi"</summary>
		public const string MSG_YUMI = BowFletchingStringConstants.ITEM_YUMI;

		/// <summary>Message: "Material de Tábuas"</summary>
		public const string MSG_BOARD_MATERIAL = BowFletchingStringConstants.MSG_BOARD_MATERIAL;

		/// <summary>Error: "Você não pode trabalhar esta madeira estranha e incomum."</summary>
		public const string MSG_CANNOT_WORK_WOOD = BowFletchingStringConstants.MSG_CANNOT_WORK_WOOD;

		/// <summary>Resource name: "Tábuas de Carvalho Cinza"</summary>
		public const string MSG_ASH_BOARD = BowFletchingStringConstants.RESOURCE_ASH_BOARD;

		/// <summary>Resource name: "Tábuas de Ébano"</summary>
		public const string MSG_EBONY_BOARD = BowFletchingStringConstants.RESOURCE_EBONY_BOARD;

		/// <summary>Resource name: "Tábuas Élficas"</summary>
		public const string MSG_ELVEN_BOARD = BowFletchingStringConstants.RESOURCE_ELVEN_BOARD;

		/// <summary>Resource name: "Tábuas de Ipê-Amarelo"</summary>
		public const string MSG_GOLDEN_OAK_BOARD = BowFletchingStringConstants.RESOURCE_GOLDEN_OAK_BOARD;

		/// <summary>Resource name: "Tábuas de Cerejeira"</summary>
		public const string MSG_CHERRY_BOARD = BowFletchingStringConstants.RESOURCE_CHERRY_BOARD;

		/// <summary>Resource name: "Tábuas de Pau-Brasil"</summary>
		public const string MSG_ROSEWOOD_BOARD = BowFletchingStringConstants.RESOURCE_ROSEWOOD_BOARD;

		/// <summary>Resource name: "Tábuas de Nogueira Branca"</summary>
		public const string MSG_HICKORY_BOARD = BowFletchingStringConstants.RESOURCE_HICKORY_BOARD;

		#endregion

		#region Skill Requirements - Materials

		/// <summary>Minimum skill required for kindling</summary>
		public const double SKILL_MIN_KINDLING = 0.0;

		/// <summary>Maximum skill required for kindling</summary>
		public const double SKILL_MAX_KINDLING = 30.0;

		/// <summary>Minimum skill required for kindling batch</summary>
		public const double SKILL_MIN_KINDLING_BATCH = 15.0;

		/// <summary>Maximum skill required for kindling batch</summary>
		public const double SKILL_MAX_KINDLING_BATCH = 40.0;

		/// <summary>Minimum skill required for shaft</summary>
		public const double SKILL_MIN_SHAFT = 0.0;

		/// <summary>Maximum skill required for shaft</summary>
		public const double SKILL_MAX_SHAFT = 30.0;

		#endregion

		#region Skill Requirements - Ammunition

		/// <summary>Minimum skill required for arrow</summary>
		public const double SKILL_MIN_ARROW = 30.0;

		/// <summary>Maximum skill required for arrow</summary>
		public const double SKILL_MAX_ARROW = 60.0;

		/// <summary>Minimum skill required for bolt</summary>
		public const double SKILL_MIN_BOLT = 50.0;

		/// <summary>Maximum skill required for bolt</summary>
		public const double SKILL_MAX_BOLT = 70.0;

		/// <summary>Minimum skill required for fukiya darts</summary>
		public const double SKILL_MIN_FUKIYA_DARTS = 60.0;

		/// <summary>Maximum skill required for fukiya darts</summary>
		public const double SKILL_MAX_FUKIYA_DARTS = 80.0;

		/// <summary>Minimum skill required for throwing weapon</summary>
		public const double SKILL_MIN_THROWING_WEAPON = 70.0;

		/// <summary>Maximum skill required for throwing weapon</summary>
		public const double SKILL_MAX_THROWING_WEAPON = 90.0;

		#endregion

		#region Skill Requirements - Weapons

		/// <summary>Minimum skill required for bow</summary>
		public const double SKILL_MIN_BOW = 40.0;

		/// <summary>Maximum skill required for bow</summary>
		public const double SKILL_MAX_BOW = 75.0;

		/// <summary>Minimum skill required for crossbow</summary>
		public const double SKILL_MIN_CROSSBOW = 60.0;

		/// <summary>Maximum skill required for crossbow</summary>
		public const double SKILL_MAX_CROSSBOW = 90.0;

		/// <summary>Minimum skill required for heavy crossbow</summary>
		public const double SKILL_MIN_HEAVY_CROSSBOW = 80.0;

		/// <summary>Maximum skill required for heavy crossbow</summary>
		public const double SKILL_MAX_HEAVY_CROSSBOW = 110.0;

		/// <summary>Minimum skill required for composite bow</summary>
		public const double SKILL_MIN_COMPOSITE_BOW = 70.0;

		/// <summary>Maximum skill required for composite bow</summary>
		public const double SKILL_MAX_COMPOSITE_BOW = 100.0;

		/// <summary>Minimum skill required for repeating crossbow</summary>
		public const double SKILL_MIN_REPEATING_CROSSBOW = 90.0;

		/// <summary>Maximum skill required for repeating crossbow</summary>
		public const double SKILL_MAX_REPEATING_CROSSBOW = 110.0;

		/// <summary>Minimum skill required for yumi</summary>
		public const double SKILL_MIN_YUMI = 90.0;

		/// <summary>Maximum skill required for yumi</summary>
		public const double SKILL_MAX_YUMI = 120.0;

		/// <summary>Minimum skill required for magical shortbow</summary>
		public const double SKILL_MIN_MAGICAL_SHORTBOW = 55.0;

		/// <summary>Maximum skill required for magical shortbow</summary>
		public const double SKILL_MAX_MAGICAL_SHORTBOW = 85.0;

		/// <summary>Minimum skill required for elven composite longbow</summary>
		public const double SKILL_MIN_ELVEN_COMPOSITE_LONGBOW = 70.0;

		/// <summary>Maximum skill required for elven composite longbow</summary>
		public const double SKILL_MAX_ELVEN_COMPOSITE_LONGBOW = 95.0;

		#endregion

		#region Resource Amounts

		/// <summary>Log amount for kindling</summary>
		public const int RESOURCE_LOGS_KINDLING = 1;

		/// <summary>Log amount for shaft</summary>
		public const int RESOURCE_LOGS_SHAFT = 1;

		/// <summary>Shaft amount for arrow</summary>
		public const int RESOURCE_SHAFTS_ARROW = 1;

		/// <summary>Feather amount for arrow</summary>
		public const int RESOURCE_FEATHERS_ARROW = 1;

		/// <summary>Shaft amount for bolt</summary>
		public const int RESOURCE_SHAFTS_BOLT = 1;

		/// <summary>Feather amount for bolt</summary>
		public const int RESOURCE_FEATHERS_BOLT = 1;

		/// <summary>Iron ingot amount for bolt</summary>
		public const int RESOURCE_IRON_INGOTS_BOLT = 1;

		/// <summary>Log amount for fukiya darts</summary>
		public const int RESOURCE_LOGS_FUKIYA_DARTS = 1;

		/// <summary>Iron ingot amount for throwing weapon</summary>
		public const int RESOURCE_IRON_INGOTS_THROWING_WEAPON = 1;

		/// <summary>Board amount for bow</summary>
		public const int RESOURCE_BOARDS_BOW = 8;

		/// <summary>Board amount for crossbow</summary>
		public const int RESOURCE_BOARDS_CROSSBOW = 12;

		/// <summary>Board amount for heavy crossbow</summary>
		public const int RESOURCE_BOARDS_HEAVY_CROSSBOW = 15;

		/// <summary>Board amount for composite bow</summary>
		public const int RESOURCE_BOARDS_COMPOSITE_BOW = 11;

		/// <summary>Board amount for repeating crossbow</summary>
		public const int RESOURCE_BOARDS_REPEATING_CROSSBOW = 14;

		/// <summary>Board amount for yumi</summary>
		public const int RESOURCE_BOARDS_YUMI = 13;

		/// <summary>Board amount for magical shortbow</summary>
		public const int RESOURCE_BOARDS_MAGICAL_SHORTBOW = 9;

		/// <summary>Board amount for elven composite longbow</summary>
		public const int RESOURCE_BOARDS_ELVEN_COMPOSITE_LONGBOW = 16;

		#endregion

		#region Wood Type Skill Requirements

		/// <summary>Skill required for common wood (Board)</summary>
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

		#endregion

		#region Quality Levels

		/// <summary>Quality level: Below average (0)</summary>
		public const int QUALITY_BELOW_AVERAGE = 0;

		/// <summary>Quality level: Exceptional (2)</summary>
		public const int QUALITY_EXCEPTIONAL = 2;

		#endregion
	}
}

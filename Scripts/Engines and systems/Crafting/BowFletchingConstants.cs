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

		/// <summary>Gump title: "BOWCRAFT AND FLETCHING MENU" (cliloc 1044006)</summary>
		public const int MSG_GUMP_TITLE = 1044006;

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

		/// <summary>Group name: "Materials" (cliloc 1044457)</summary>
		public const int MSG_GROUP_MATERIALS = 1044457;

		/// <summary>Group name: "Ammunition" (cliloc 1044565)</summary>
		public const int MSG_GROUP_AMMUNITION = 1044565;

		/// <summary>Group name: "Weapons" (cliloc 1044566)</summary>
		public const int MSG_GROUP_WEAPONS = 1044566;

		/// <summary>Resource name: "Log" (cliloc 1015101)</summary>
		public const int MSG_LOG = 1015101;

		/// <summary>Resource name: "Board" (cliloc 1015101)</summary>
		public const int MSG_BOARD = 1015101;

		/// <summary>Error: "You don't have enough wood to make that." (cliloc 1044351)</summary>
		public const int MSG_INSUFFICIENT_WOOD = 1044351;

		/// <summary>Item name: "Shaft" (cliloc 1027124)</summary>
		public const int MSG_SHAFT_ITEM = 1027124;

		/// <summary>Resource name: "Shaft" (cliloc 1044560)</summary>
		public const int MSG_SHAFT_RESOURCE = 1044560;

		/// <summary>Error: "You don't have enough shafts." (cliloc 1044561)</summary>
		public const int MSG_INSUFFICIENT_SHAFTS = 1044561;

		/// <summary>Resource name: "Feather" (cliloc 1044562)</summary>
		public const int MSG_FEATHER = 1044562;

		/// <summary>Error: "You don't have enough feathers." (cliloc 1044563)</summary>
		public const int MSG_INSUFFICIENT_FEATHERS = 1044563;

		/// <summary>Item name: "Arrow" (cliloc 1023903)</summary>
		public const int MSG_ARROW = 1023903;

		/// <summary>Item name: "Bolt" (cliloc 1027163)</summary>
		public const int MSG_BOLT = 1027163;

		/// <summary>Item name: "Fukiya Darts" (cliloc 1030246)</summary>
		public const int MSG_FUKIYA_DARTS = 1030246;

		/// <summary>Item name: "Throwing Weapon" (cliloc 1044117)</summary>
		public const int MSG_THROWING_WEAPON = 1044117;

		/// <summary>Resource name: "Iron Ingot" (cliloc 1074904)</summary>
		public const int MSG_IRON_INGOT = 1074904;

		/// <summary>Error: "You don't have enough metal." (cliloc 1044037)</summary>
		public const int MSG_INSUFFICIENT_METAL = 1044037;

		/// <summary>Item name: "Bow" (cliloc 1025042)</summary>
		public const int MSG_BOW = 1025042;

		/// <summary>Item name: "Crossbow" (cliloc 1023919)</summary>
		public const int MSG_CROSSBOW = 1023919;

		/// <summary>Item name: "Heavy Crossbow" (cliloc 1025117)</summary>
		public const int MSG_HEAVY_CROSSBOW = 1025117;

		/// <summary>Item name: "Composite Bow" (cliloc 1029922)</summary>
		public const int MSG_COMPOSITE_BOW = 1029922;

		/// <summary>Item name: "Repeating Crossbow" (cliloc 1029923)</summary>
		public const int MSG_REPEATING_CROSSBOW = 1029923;

		/// <summary>Item name: "Yumi" (cliloc 1030224)</summary>
		public const int MSG_YUMI = 1030224;

		/// <summary>Message: "Board Material" (cliloc 1072643)</summary>
		public const int MSG_BOARD_MATERIAL = 1072643;

		/// <summary>Error: "You cannot work this strange and unusual wood." (cliloc 1072652)</summary>
		public const int MSG_CANNOT_WORK_WOOD = 1072652;

		/// <summary>Resource name: "Ash Board" (cliloc 1095379)</summary>
		public const int MSG_ASH_BOARD = 1095379;

		/// <summary>Resource name: "Ebony Board" (cliloc 1095381)</summary>
		public const int MSG_EBONY_BOARD = 1095381;

		/// <summary>Resource name: "Elven Board" (cliloc 1095535)</summary>
		public const int MSG_ELVEN_BOARD = 1095535;

		/// <summary>Resource name: "Golden Oak Board" (cliloc 1095382)</summary>
		public const int MSG_GOLDEN_OAK_BOARD = 1095382;

		/// <summary>Resource name: "Cherry Board" (cliloc 1095380)</summary>
		public const int MSG_CHERRY_BOARD = 1095380;

		/// <summary>Resource name: "Rosewood Board" (cliloc 1095387)</summary>
		public const int MSG_ROSEWOOD_BOARD = 1095387;

		/// <summary>Resource name: "Hickory Board" (cliloc 1095383)</summary>
		public const int MSG_HICKORY_BOARD = 1095383;

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

namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for Blacksmithy system calculations and mechanics.
	/// Extracted from DefBlacksmithy.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class BlacksmithyConstants
	{
		#region Item IDs - Anvil

		/// <summary>Standard anvil item ID 1</summary>
		public const int ANVIL_ITEM_ID_1 = 4015;

		/// <summary>Standard anvil item ID 2</summary>
		public const int ANVIL_ITEM_ID_2 = 4016;

		/// <summary>Anvil item ID (hex 0x2DD5)</summary>
		public const int ANVIL_ITEM_ID_3 = 0x2DD5;

		/// <summary>Anvil item ID (hex 0x2DD6)</summary>
		public const int ANVIL_ITEM_ID_4 = 0x2DD6;

		/// <summary>Anvil item ID (hex 0x2B55)</summary>
		public const int ANVIL_ITEM_ID_5 = 0x2B55;

		/// <summary>Anvil item ID (hex 0x2B57)</summary>
		public const int ANVIL_ITEM_ID_6 = 0x2B57;

		/// <summary>Anvil item ID (hex 0xFAF)</summary>
		public const int ANVIL_ITEM_ID_7 = 0xFAF;

		#endregion

		#region Item IDs - Forge

		/// <summary>Standard forge item ID</summary>
		public const int FORGE_ITEM_ID = 4017;

		/// <summary>Fire Giant Forge minimum item ID</summary>
		public const int FIRE_GIANT_FORGE_MIN = 6896;

		/// <summary>Fire Giant Forge maximum item ID</summary>
		public const int FIRE_GIANT_FORGE_MAX = 6898;

		/// <summary>Forge item ID range start (hex 0x10DE)</summary>
		public const int FORGE_RANGE_1_START = 0x10DE;

		/// <summary>Forge item ID range end (hex 0x10E0)</summary>
		public const int FORGE_RANGE_1_END = 0x10E0;

		/// <summary>Forge item ID range start (6522)</summary>
		public const int FORGE_RANGE_2_START = 6522;

		/// <summary>Forge item ID range end (6569)</summary>
		public const int FORGE_RANGE_2_END = 6569;

		/// <summary>Forge item ID range start (hex 0x544B)</summary>
		public const int FORGE_RANGE_3_START = 0x544B;

		/// <summary>Forge item ID range end (hex 0x544E)</summary>
		public const int FORGE_RANGE_3_END = 0x544E;

		/// <summary>Forge item ID (hex 0x2DD8)</summary>
		public const int FORGE_ITEM_ID_2 = 0x2DD8;

		/// <summary>Forge item ID range start (hex 0x197A)</summary>
		public const int FORGE_RANGE_4_START = 0x197A;

		/// <summary>Forge item ID range end (hex 0x1984)</summary>
		public const int FORGE_RANGE_4_END = 0x1984;

		#endregion

		#region Coordinates - Special Locations

		/// <summary>Skara Brae Area 1 - X coordinate minimum</summary>
		public const int SKARA_BRAE_AREA1_X_MIN = 6896;

		/// <summary>Skara Brae Area 1 - X coordinate maximum</summary>
		public const int SKARA_BRAE_AREA1_X_MAX = 6912;

		/// <summary>Skara Brae Area 1 - Y coordinate minimum</summary>
		public const int SKARA_BRAE_AREA1_Y_MIN = 145;

		/// <summary>Skara Brae Area 1 - Y coordinate maximum</summary>
		public const int SKARA_BRAE_AREA1_Y_MAX = 163;

		/// <summary>Skara Brae Area 2 - X coordinate minimum</summary>
		public const int SKARA_BRAE_AREA2_X_MIN = 6911;

		/// <summary>Skara Brae Area 2 - X coordinate maximum</summary>
		public const int SKARA_BRAE_AREA2_X_MAX = 6920;

		/// <summary>Skara Brae Area 2 - Y coordinate minimum</summary>
		public const int SKARA_BRAE_AREA2_Y_MIN = 179;

		/// <summary>Skara Brae Area 2 - Y coordinate maximum</summary>
		public const int SKARA_BRAE_AREA2_Y_MAX = 186;

		#endregion

		#region Range and Offsets

		/// <summary>Default range for checking anvil and forge proximity</summary>
		public const int CHECK_RANGE = 2;

		/// <summary>Z coordinate offset for line of sight checks</summary>
		public const int Z_OFFSET = 16;

		/// <summary>Tile height offset for line of sight calculations</summary>
		public const int TILE_HEIGHT_OFFSET = 1;

		#endregion

		#region Craft System Parameters

		/// <summary>Minimum number of craft effect animations</summary>
		public const int MIN_CRAFT_EFFECT = 1;

		/// <summary>Maximum number of craft effect animations</summary>
		public const int MAX_CRAFT_EFFECT = 1;

		/// <summary>Delay between craft effect animations (seconds)</summary>
		public const double CRAFT_DELAY = 1.25;

		#endregion

		#region Sound IDs

		/// <summary>Blacksmith crafting sound effect</summary>
		public const int SOUND_BLACKSMITH = 0x541;

		#endregion

		#region Timer Delays

		/// <summary>Timer delay for synchronized sound effect (seconds)</summary>
		public const double TIMER_DELAY_SECONDS = 0.7;

		#endregion

		#region Message IDs

		/// <summary>Gump title: "BLACKSMITHY MENU"</summary>
		public const int MSG_GUMP_TITLE = 1044002;

		/// <summary>Message: "You have worn out your tool!"</summary>
		public const int MSG_TOOL_WORN_OUT = 1044038;

		/// <summary>Message: "If you have a tool equipped, you must use that tool."</summary>
		public const int MSG_MUST_USE_EQUIPPED_TOOL = 1048146;

		/// <summary>Message: "The tool must be on your person to use."</summary>
		public const int MSG_TOOL_MUST_BE_ON_PERSON = 1044263;

		/// <summary>Message: "You must be near an anvil and a forge to smith items."</summary>
		public const int MSG_MUST_BE_NEAR_ANVIL_AND_FORGE = 1044267;

		/// <summary>Message: "You failed to create the item, and some of your materials are lost."</summary>
		public const int MSG_FAILED_WITH_MATERIAL_LOSS = 1044043;

		/// <summary>Message: "You failed to create the item, but no materials were lost."</summary>
		public const int MSG_FAILED_WITHOUT_MATERIAL_LOSS = 1044157;

		/// <summary>Message: "You were barely able to make this item. It's quality is below average."</summary>
		public const int MSG_BELOW_AVERAGE_QUALITY = 502785;

		/// <summary>Message: "You create an exceptional quality item and affix your maker's mark."</summary>
		public const int MSG_EXCEPTIONAL_WITH_MARK = 1044156;

		/// <summary>Message: "You create an exceptional quality item."</summary>
		public const int MSG_EXCEPTIONAL_QUALITY = 1044155;

		/// <summary>Message: "You create the item."</summary>
		public const int MSG_ITEM_CREATED = 1044154;

		/// <summary>Message ID for iron ingot resource</summary>
		public const int MSG_IRON_INGOT = 1044036;

		/// <summary>Message ID for missing resource</summary>
		public const int MSG_MISSING_RESOURCE = 1044037;

		/// <summary>Message ID for dragon scales resource</summary>
		public const int MSG_DRAGON_SCALES = 1042081;

		/// <summary>Message ID for iron ingot name</summary>
		public const int MSG_IRON_INGOT_NAME = 1044022;

		/// <summary>Message ID for sub-resource error</summary>
		public const int MSG_SUB_RESOURCE_ERROR = 1044267;

		/// <summary>Message ID for sub-resource skill error</summary>
		public const int MSG_SUB_RESOURCE_SKILL_ERROR = 1044268;

		/// <summary>Message ID for scale skill error</summary>
		public const int MSG_SCALE_SKILL_ERROR = 1054018;

		/// <summary>Message ID for harpoon resource error</summary>
		public const int MSG_HARPOON_RESOURCE_ERROR = 1044351;

		#endregion

		#region Ingot Message IDs

		/// <summary>Message ID for dull copper ingot name</summary>
		public const int MSG_DULL_COPPER_INGOT = 1044023;

		/// <summary>Message ID for shadow iron ingot name</summary>
		public const int MSG_SHADOW_IRON_INGOT = 1044024;

		/// <summary>Message ID for copper ingot name</summary>
		public const int MSG_COPPER_INGOT = 1044025;

		/// <summary>Message ID for bronze ingot name</summary>
		public const int MSG_BRONZE_INGOT = 1044026;

		/// <summary>Message ID for gold ingot name</summary>
		public const int MSG_GOLD_INGOT = 1044027;

		/// <summary>Message ID for agapite ingot name</summary>
		public const int MSG_AGAPITE_INGOT = 1044028;

		/// <summary>Message ID for verite ingot name</summary>
		public const int MSG_VERITE_INGOT = 1044029;

		/// <summary>Message ID for valorite ingot name</summary>
		public const int MSG_VALORITE_INGOT = 1044030;

		/// <summary>Message ID for titanium ingot name (custom)</summary>
		public const int MSG_TITANIUM_INGOT = 6661000;

		/// <summary>Message ID for rosenium ingot name (custom)</summary>
		public const int MSG_ROSENIUM_INGOT = 6662000;

		/// <summary>Message ID for platinum ingot name (custom)</summary>
		public const int MSG_PLATINUM_INGOT = 6663000;

	#endregion
	}
}


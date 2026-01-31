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

		#region Message Numbers (Cliloc)

		/// <summary>Gump title: "MENU DE FERRARIA" (cliloc 1044002)</summary>
		public const int MSG_GUMP_TITLE = 1044002;

		/// <summary>Message: "Você quebrou sua ferramenta!"</summary>
		public const string MSG_TOOL_WORN_OUT = BlacksmithyStringConstants.MSG_TOOL_WORN_OUT;

		/// <summary>Message: "Se você tiver uma ferramenta equipada, deve usar essa ferramenta."</summary>
		public const string MSG_MUST_USE_EQUIPPED_TOOL = BlacksmithyStringConstants.MSG_MUST_USE_EQUIPPED_TOOL;

		/// <summary>Message: "A ferramenta deve estar com você para usar."</summary>
		public const string MSG_TOOL_MUST_BE_ON_PERSON = BlacksmithyStringConstants.MSG_TOOL_MUST_BE_ON_PERSON;

		/// <summary>Message: "Você deve estar perto de uma bigorna e uma forja para fazer isso."</summary>
		public const string MSG_MUST_BE_NEAR_ANVIL_AND_FORGE = BlacksmithyStringConstants.MSG_MUST_BE_NEAR_ANVIL_AND_FORGE;

		/// <summary>Message: "Você falhou ao criar o item, e alguns de seus materiais foram perdidos."</summary>
		public const string MSG_FAILED_WITH_MATERIAL_LOSS = BlacksmithyStringConstants.MSG_FAILED_LOST_MATERIALS;

		/// <summary>Message: "Você falhou ao criar o item, mas nenhum material foi perdido."</summary>
		public const string MSG_FAILED_WITHOUT_MATERIAL_LOSS = BlacksmithyStringConstants.MSG_FAILED_NO_MATERIALS_LOST;

		/// <summary>Message: "Você mal conseguiu fazer este item. Sua qualidade está abaixo da média."</summary>
		public const string MSG_BELOW_AVERAGE_QUALITY = BlacksmithyStringConstants.MSG_BARELY_MADE_ITEM;

		/// <summary>Message: "Você cria um item de qualidade excepcional e assina a sua marca nele."</summary>
		public const string MSG_EXCEPTIONAL_WITH_MARK = BlacksmithyStringConstants.MSG_EXCEPTIONAL_WITH_MARK;

		/// <summary>Message: "Você cria um item de qualidade excepcional."</summary>
		public const string MSG_EXCEPTIONAL_QUALITY = BlacksmithyStringConstants.MSG_EXCEPTIONAL_QUALITY;

		/// <summary>Message: "Você cria o item."</summary>
		public const string MSG_ITEM_CREATED = BlacksmithyStringConstants.MSG_ITEM_CREATED;

		/// <summary>Resource name: "Lingote de Ferro"</summary>
		public const string MSG_IRON_INGOT = BlacksmithyStringConstants.RESOURCE_IRON_INGOT;

		/// <summary>Error: "Você não tem o recurso necessário."</summary>
		public const string MSG_MISSING_RESOURCE = BlacksmithyStringConstants.MSG_MISSING_RESOURCE;

		/// <summary>Resource name: "Escamas Reptilianas"</summary>
		public const string MSG_DRAGON_SCALES = BlacksmithyStringConstants.RESOURCE_DRAGON_SCALES;

		/// <summary>Resource name: "Lingote de Ferro"</summary>
		public const string MSG_IRON_INGOT_NAME = BlacksmithyStringConstants.INGOT_IRON;

		/// <summary>Error: "Você não tem o sub-recurso necessário."</summary>
		public const string MSG_SUB_RESOURCE_ERROR = BlacksmithyStringConstants.MSG_SUB_RESOURCE_ERROR;

		/// <summary>Error: "Você não tem habilidade suficiente para trabalhar com este sub-recurso."</summary>
		public const string MSG_SUB_RESOURCE_SKILL_ERROR = BlacksmithyStringConstants.MSG_SUB_RESOURCE_SKILL_ERROR;

		/// <summary>Error: "Você não tem escamas reptilianas suficientes."</summary>
		public const string MSG_SCALE_SKILL_ERROR = BlacksmithyStringConstants.MSG_SCALE_SKILL_ERROR;

		/// <summary>Error: "Você não tem madeira suficiente para fazer isso."</summary>
		public const string MSG_HARPOON_RESOURCE_ERROR = BlacksmithyStringConstants.MSG_HARPOON_RESOURCE_ERROR;

		#endregion

		#region Ingot Names (PT-BR)

		/// <summary>Dull copper ingot name</summary>
		public const string MSG_DULL_COPPER_INGOT = BlacksmithyStringConstants.INGOT_DULL_COPPER;

		/// <summary>Shadow iron ingot name</summary>
		public const string MSG_SHADOW_IRON_INGOT = BlacksmithyStringConstants.INGOT_SHADOW_IRON;

		/// <summary>Copper ingot name</summary>
		public const string MSG_COPPER_INGOT = BlacksmithyStringConstants.INGOT_COPPER;

		/// <summary>Bronze ingot name</summary>
		public const string MSG_BRONZE_INGOT = BlacksmithyStringConstants.INGOT_BRONZE;

		/// <summary>Gold ingot name</summary>
		public const string MSG_GOLD_INGOT = BlacksmithyStringConstants.INGOT_GOLD;

		/// <summary>Agapite ingot name</summary>
		public const string MSG_AGAPITE_INGOT = BlacksmithyStringConstants.INGOT_AGAPITE;

		/// <summary>Verite ingot name</summary>
		public const string MSG_VERITE_INGOT = BlacksmithyStringConstants.INGOT_VERITE;

		/// <summary>Valorite ingot name</summary>
		public const string MSG_VALORITE_INGOT = BlacksmithyStringConstants.INGOT_VALORITE;

		/// <summary>Titanium ingot name</summary>
		public const string MSG_TITANIUM_INGOT = BlacksmithyStringConstants.INGOT_TITANIUM;

		/// <summary>Rosenium ingot name</summary>
		public const string MSG_ROSENIUM_INGOT = BlacksmithyStringConstants.INGOT_ROSENIUM;

		/// <summary>Platinum ingot name</summary>
		public const string MSG_PLATINUM_INGOT = BlacksmithyStringConstants.INGOT_PLATINUM;

	#endregion
	}
}


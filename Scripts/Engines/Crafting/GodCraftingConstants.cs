using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for God Crafting systems (Smithing, Sewing, Brewing).
	/// Extracted from DefGodCrafting.cs to improve maintainability.
	/// </summary>
	public static class GodCraftingConstants
	{
		#region Crafting System Parameters

		/// <summary>Minimum chance multiplier for crafting success</summary>
		public const int MIN_CHANCE_MULTIPLIER = 1;

		/// <summary>Maximum chance multiplier for crafting success</summary>
		public const int MAX_CHANCE_MULTIPLIER = 1;

		/// <summary>Delay multiplier for crafting actions</summary>
		public const double DELAY_MULTIPLIER = 1.25;

		/// <summary>Minimum success chance at minimum skill (50%)</summary>
		public const double MIN_SUCCESS_CHANCE = 0.5;

		#endregion

		#region Sound Effects

		/// <summary>Sound played when smithing (hammering)</summary>
		public const int SOUND_SMITHING = 0x541;

		/// <summary>Sound played when sewing (scissors)</summary>
		public const int SOUND_SEWING = 0x248;

		/// <summary>Sound played when brewing (alchemy)</summary>
		public const int SOUND_BREWING = 0x242;

		#endregion

		#region Location Coordinates (TerMur Map)

		// Smithing Area (Dragon Forge)
		/// <summary>Minimum X coordinate for smithing area</summary>
		public const int SMITHING_AREA_MIN_X = 1104;

		/// <summary>Maximum X coordinate for smithing area</summary>
		public const int SMITHING_AREA_MAX_X = 1125;

		/// <summary>Minimum Y coordinate for smithing area</summary>
		public const int SMITHING_AREA_MIN_Y = 1960;

		/// <summary>Maximum Y coordinate for smithing area</summary>
		public const int SMITHING_AREA_MAX_Y = 1978;

		// Sewing Area (Enchanted Spinning Wheel)
		/// <summary>Minimum X coordinate for sewing area</summary>
		public const int SEWING_AREA_MIN_X = 1087;

		/// <summary>Maximum X coordinate for sewing area</summary>
		public const int SEWING_AREA_MAX_X = 1105;

		/// <summary>Minimum Y coordinate for sewing area</summary>
		public const int SEWING_AREA_MIN_Y = 1968;

		/// <summary>Maximum Y coordinate for sewing area</summary>
		public const int SEWING_AREA_MAX_Y = 1982;

		// Brewing Area (Alchemy Sanctuary)
		/// <summary>Minimum X coordinate for brewing area</summary>
		public const int BREWING_AREA_MIN_X = 1098;

		/// <summary>Maximum X coordinate for brewing area</summary>
		public const int BREWING_AREA_MAX_X = 1121;

		/// <summary>Minimum Y coordinate for brewing area</summary>
		public const int BREWING_AREA_MIN_Y = 1908;

		/// <summary>Maximum Y coordinate for brewing area</summary>
		public const int BREWING_AREA_MAX_Y = 1931;

		#endregion

		#region Skill Requirements - Smithing & Sewing

		/// <summary>Minimum skill for all smithing and sewing recipes</summary>
		public const double STANDARD_MIN_SKILL = 90.0;

		/// <summary>Maximum skill for all smithing and sewing recipes</summary>
		public const double STANDARD_MAX_SKILL = 125.0;

		#endregion

		#region Skill Requirements - Brewing

		/// <summary>Minimum skill for lesser potions</summary>
		public const double LESSER_POTION_MIN_SKILL = 70.0;

		/// <summary>Maximum skill for lesser potions</summary>
		public const double LESSER_POTION_MAX_SKILL = 105.0;

		/// <summary>Minimum skill for regular potions</summary>
		public const double REGULAR_POTION_MIN_SKILL = 80.0;

		/// <summary>Maximum skill for regular potions</summary>
		public const double REGULAR_POTION_MAX_SKILL = 115.0;

		/// <summary>Minimum skill for greater/advanced potions</summary>
		public const double GREATER_POTION_MIN_SKILL = 90.0;

		/// <summary>Maximum skill for greater/advanced potions</summary>
		public const double GREATER_POTION_MAX_SKILL = 125.0;

		#endregion

		#region Smithing Armor Resource Quantities

		/// <summary>Ingots required for plate arms</summary>
		public const int PLATE_ARMS_INGOT_QUANTITY = 18;

		/// <summary>Ingots required for plate gloves</summary>
		public const int PLATE_GLOVES_INGOT_QUANTITY = 12;

		/// <summary>Ingots required for plate gorget</summary>
		public const int PLATE_GORGET_INGOT_QUANTITY = 10;

		/// <summary>Ingots required for plate legs</summary>
		public const int PLATE_LEGS_INGOT_QUANTITY = 20;

		/// <summary>Ingots required for plate chest</summary>
		public const int PLATE_CHEST_INGOT_QUANTITY = 25;

		/// <summary>Ingots required for female plate chest</summary>
		public const int FEMALE_PLATE_CHEST_INGOT_QUANTITY = 20;

		/// <summary>Ingots required for plate helm</summary>
		public const int PLATE_HELM_INGOT_QUANTITY = 15;

		/// <summary>Ingots required for shield</summary>
		public const int SHIELD_INGOT_QUANTITY = 18;

		/// <summary>Ingots required for oil (in addition to bottle)</summary>
		public const int OIL_INGOT_QUANTITY = 30;

		/// <summary>Bottles required for oil</summary>
		public const int OIL_BOTTLE_QUANTITY = 1;

		#endregion

		#region Sewing Armor Resource Quantities

		/// <summary>Skins required for skin arms</summary>
		public const int SKIN_ARMS_QUANTITY = 4;

		/// <summary>Skins required for skin cap/helm</summary>
		public const int SKIN_CAP_QUANTITY = 2;

		/// <summary>Skins required for skin gloves</summary>
		public const int SKIN_GLOVES_QUANTITY = 3;

		/// <summary>Skins required for skin gorget</summary>
		public const int SKIN_GORGET_QUANTITY = 4;

		/// <summary>Skins required for skin legs</summary>
		public const int SKIN_LEGS_QUANTITY = 10;

		/// <summary>Skins required for skin chest</summary>
		public const int SKIN_CHEST_QUANTITY = 12;

		#endregion

		#region Brewing Potion Resource Quantities

		// Bottles (universal)
		/// <summary>Bottles required for all potions</summary>
		public const int POTION_BOTTLE_QUANTITY = 1;

		// Invisibility Potions
		/// <summary>Silver Serpent Venom for lesser invisibility potion</summary>
		public const int LESSER_INVISIBILITY_VENOM = 1;

		/// <summary>Dragon Blood for lesser invisibility potion</summary>
		public const int LESSER_INVISIBILITY_BLOOD = 2;

		/// <summary>Silver Serpent Venom for invisibility potion</summary>
		public const int INVISIBILITY_VENOM = 2;

		/// <summary>Dragon Blood for invisibility potion</summary>
		public const int INVISIBILITY_BLOOD = 4;

		/// <summary>Silver Serpent Venom for greater invisibility potion</summary>
		public const int GREATER_INVISIBILITY_VENOM = 3;

		/// <summary>Dragon Blood for greater invisibility potion</summary>
		public const int GREATER_INVISIBILITY_BLOOD = 6;

		// Invulnerability Potion
		/// <summary>Enchanted Seaweed for invulnerability potion</summary>
		public const int INVULNERABILITY_SEAWEED = 3;

		/// <summary>Dragon Tooth for invulnerability potion</summary>
		public const int INVULNERABILITY_TOOTH = 2;

		// Mana Potions
		/// <summary>Golden Serpent Venom for lesser mana potion</summary>
		public const int LESSER_MANA_VENOM = 1;

		/// <summary>Lich Dust for lesser mana potion</summary>
		public const int LESSER_MANA_DUST = 2;

		/// <summary>Golden Serpent Venom for mana potion</summary>
		public const int MANA_VENOM = 2;

		/// <summary>Lich Dust for mana potion</summary>
		public const int MANA_DUST = 4;

		/// <summary>Golden Serpent Venom for greater mana potion</summary>
		public const int GREATER_MANA_VENOM = 3;

		/// <summary>Lich Dust for greater mana potion</summary>
		public const int GREATER_MANA_DUST = 6;

		// Rejuvenate Potions
		/// <summary>Demon Claw for lesser rejuvenate potion</summary>
		public const int LESSER_REJUVENATE_CLAW = 1;

		/// <summary>Unicorn Horn for lesser rejuvenate potion</summary>
		public const int LESSER_REJUVENATE_HORN = 1;

		/// <summary>Demon Claw for rejuvenate potion</summary>
		public const int REJUVENATE_CLAW = 2;

		/// <summary>Unicorn Horn for rejuvenate potion</summary>
		public const int REJUVENATE_HORN = 2;

		/// <summary>Demon Claw for greater rejuvenate/super potion</summary>
		public const int GREATER_REJUVENATE_CLAW = 3;

		/// <summary>Unicorn Horn for greater rejuvenate/super potion</summary>
		public const int GREATER_REJUVENATE_HORN = 3;

		// Resurrection Potions
		/// <summary>Demigod Blood for resurrection potions</summary>
		public const int RESURRECTION_DEMIGOD_BLOOD = 3;

		/// <summary>Ghostly Dust for resurrection potions</summary>
		public const int RESURRECTION_GHOSTLY_DUST = 2;

		// Repair Potion
		/// <summary>Unicorn Horn for repair potion</summary>
		public const int REPAIR_UNICORN_HORN = 3;

		/// <summary>Silver Serpent Venom for repair potion</summary>
		public const int REPAIR_SERPENT_VENOM = 2;

		#endregion

		#region Cliloc IDs

		/// <summary>Cliloc 1044038: "You have worn out your tool!"</summary>
		public const int CLILOC_TOOL_WORN_OUT = 1044038;

		/// <summary>Cliloc 1044263: "The tool must be on your person to use."</summary>
		public const int CLILOC_TOOL_MUST_BE_ON_PERSON = 1044263;

		/// <summary>Cliloc 1044043: "You failed to create the item, and some of your materials are lost."</summary>
		public const int CLILOC_FAILED_MATERIAL_LOST = 1044043;

		/// <summary>Cliloc 1044157: "You failed to create the item, but no materials were lost."</summary>
		public const int CLILOC_FAILED_NO_MATERIAL_LOST = 1044157;

		/// <summary>Cliloc 1044154: "You create the item."</summary>
		public const int CLILOC_ITEM_CREATED = 1044154;

		/// <summary>Cliloc 501816: Location error message</summary>
		public const int CLILOC_LOCATION_ERROR = 501816;

		/// <summary>Cliloc 1044529: "Bottle"</summary>
		public const int CLILOC_BOTTLE = 1044529;

		/// <summary>Cliloc 500315: Generic error/lack message</summary>
		public const int CLILOC_GENERIC_ERROR = 500315;

		/// <summary>Cliloc 1042081: Generic resource lack message</summary>
		public const int CLILOC_RESOURCE_LACK = 1042081;

		#endregion
	}
}

using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for Masonry crafting system.
	/// Extracted from DefMasonry.cs to improve maintainability.
	/// </summary>
	public static class MasonryConstants
	{
		#region Crafting System Parameters

		/// <summary>Minimum chance multiplier for crafting success</summary>
		public const int MIN_CHANCE_MULTIPLIER = 1;

		/// <summary>Maximum chance multiplier for crafting success</summary>
		public const int MAX_CHANCE_MULTIPLIER = 1;

		/// <summary>Delay multiplier for crafting actions</summary>
		public const double DELAY_MULTIPLIER = 1.25;

		/// <summary>Minimum success chance at minimum skill (0%)</summary>
		public const double MIN_SUCCESS_CHANCE = 0.0;

		#endregion

		#region Skill Requirements - General

		/// <summary>Minimum Carpentry skill required to access masonry crafting</summary>
		public const double MASONRY_SKILL_REQUIREMENT = 100.0;

		/// <summary>Forensics skill requirement for sarcophagus (minimum)</summary>
		public const double FORENSICS_SARCOPHAGUS_MIN = 75.0;

		/// <summary>Forensics skill requirement for sarcophagus (maximum)</summary>
		public const double FORENSICS_SARCOPHAGUS_MAX = 80.0;

		#endregion

		#region Skill Requirements - Containers

		public const double SARCOPHAGUS_MIN_SKILL = 90.0;
		public const double SARCOPHAGUS_MAX_SKILL = 115.0;

		public const double CONTAINER_BASIC_MIN_SKILL = 80.0;
		public const double CONTAINER_BASIC_MAX_SKILL = 105.0;

		public const double CONTAINER_ORNATE_MIN_SKILL = 90.0;
		public const double CONTAINER_ORNATE_MAX_SKILL = 110.0;

		public const double CONTAINER_TALL_VASE_MIN_SKILL = 95.0;
		public const double CONTAINER_TALL_VASE_MAX_SKILL = 120.0;

		#endregion

		#region Skill Requirements - Decorations

		public const double DECORATION_BASIC_MIN_SKILL = 42.5;
		public const double DECORATION_BASIC_MAX_SKILL = 92.5;

		public const double DECORATION_LARGE_MIN_SKILL = 52.5;
		public const double DECORATION_LARGE_MAX_SKILL = 102.5;

		public const double DECORATION_GARGOYLE_MIN_SKILL = 62.5;
		public const double DECORATION_GARGOYLE_MAX_SKILL = 112.5;

		public const double SCULPTURE_BUDDHIST_MIN_SKILL = 62.5;
		public const double SCULPTURE_BUDDHIST_MAX_SKILL = 122.5;

		public const double SCULPTURE_ORIENTAL_MIN_SKILL = 52.5;
		public const double SCULPTURE_ORIENTAL_MAX_SKILL = 122.5;

		#endregion

		#region Skill Requirements - Furniture

		public const double FURNITURE_BASIC_MIN_SKILL = 55.0;
		public const double FURNITURE_BASIC_MAX_SKILL = 105.0;

		public const double FURNITURE_TABLE_MIN_SKILL = 65.0;
		public const double FURNITURE_TABLE_MAX_SKILL = 115.0;

		public const double FURNITURE_WIZARD_TABLE_MIN_SKILL = 95.0;
		public const double FURNITURE_WIZARD_TABLE_MAX_SKILL = 125.0;

		public const double FURNITURE_SARCOPHAGUS_MIN_SKILL = 65.0;
		public const double FURNITURE_SARCOPHAGUS_MAX_SKILL = 125.0;

		public const double FURNITURE_COLUMN_MIN_SKILL = 65.0;
		public const double FURNITURE_COLUMN_MAX_SKILL = 125.0;

		public const double FURNITURE_FANCY_PEDESTAL_MIN_SKILL = 70.0;
		public const double FURNITURE_FANCY_PEDESTAL_MAX_SKILL = 130.0;

		public const double FURNITURE_GOTHIC_MIN_SKILL = 85.0;
		public const double FURNITURE_GOTHIC_MAX_SKILL = 135.0;

		#endregion

		#region Skill Requirements - Statues

		public const double STATUE_SMALL_MIN_SKILL = 55.0;
		public const double STATUE_SMALL_MAX_SKILL = 105.0;

		public const double STATUE_BUST_MIN_SKILL = 60.0;
		public const double STATUE_BUST_MAX_SKILL = 110.0;

		public const double STATUE_MEDIUM_MIN_SKILL = 65.0;
		public const double STATUE_MEDIUM_MAX_SKILL = 115.0;

		public const double STATUE_LARGE_MIN_SKILL = 75.0;
		public const double STATUE_LARGE_MAX_SKILL = 125.0;

		public const double STATUE_HUGE_MIN_SKILL = 85.0;
		public const double STATUE_HUGE_MAX_SKILL = 125.0;

		public const double STATUE_GIANT_MIN_SKILL = 95.0;
		public const double STATUE_GIANT_MAX_SKILL = 135.0;

		#endregion

		#region Skill Requirements - Tombstones

		public const double TOMBSTONE_MIN_SKILL = 45.0;
		public const double TOMBSTONE_MAX_SKILL = 95.0;

		#endregion

		#region Resource Quantities - Containers

		public const int GRANITE_QUANTITY_SARCOPHAGUS = 10;
		public const int GRANITE_QUANTITY_URN = 5;
		public const int GRANITE_QUANTITY_VASE_BASIC = 5;
		public const int GRANITE_QUANTITY_URN_ORNATE = 6;
		public const int GRANITE_QUANTITY_TALL_VASE_ORNATE = 8;

		#endregion

		#region Resource Quantities - Decorations

		public const int GRANITE_QUANTITY_SMALL_DECORATION = 2;
		public const int GRANITE_QUANTITY_URN_SMALL = 3;
		public const int GRANITE_QUANTITY_LARGE_DECORATION = 4;
		public const int GRANITE_QUANTITY_LARGE_VASE_DECORATION = 6;
		public const int GRANITE_QUANTITY_SCULPTURE = 6;
		public const int GRANITE_QUANTITY_SCULPTURE_BUDDHIST = 8;

		#endregion

		#region Resource Quantities - Furniture

		public const int GRANITE_QUANTITY_CHAIRS = 4;
		public const int GRANITE_QUANTITY_BENCH_SHORT = 5;
		public const int GRANITE_QUANTITY_STEPS = 5;
		public const int GRANITE_QUANTITY_BLOCK = 5;
		public const int GRANITE_QUANTITY_PEDESTAL = 5;
		public const int GRANITE_QUANTITY_PEDESTAL_FANCY = 7;
		public const int GRANITE_QUANTITY_BENCH_LONG = 8;
		public const int GRANITE_QUANTITY_FURNITURE_SARCOPHAGUS = 10;
		public const int GRANITE_QUANTITY_TABLE_SHORT = 10;
		public const int GRANITE_QUANTITY_COLUMN = 10;
		public const int GRANITE_QUANTITY_TABLE_LONG = 12;
		public const int GRANITE_QUANTITY_PILLAR = 15;
		public const int GRANITE_QUANTITY_WIZARD_TABLE = 15;
		public const int GRANITE_QUANTITY_GOTHIC_COLUMN = 20;

		#endregion

		#region Resource Quantities - Statues

		public const int GRANITE_QUANTITY_SMALL_STATUE = 4;
		public const int GRANITE_QUANTITY_BUST = 6;
		public const int GRANITE_QUANTITY_MEDIUM_STATUE = 8;
		public const int GRANITE_QUANTITY_MEDIUM_STATUE_LARGE = 10;
		public const int GRANITE_QUANTITY_LARGE_STATUE = 16;
		public const int GRANITE_QUANTITY_HUGE_STATUE = 24;
		public const int GRANITE_QUANTITY_GIANT_STATUE = 32;

		#endregion

		#region Resource Quantities - Tombstones

		public const int GRANITE_QUANTITY_TOMBSTONE = 3;

		#endregion

		#region Cliloc IDs - Gump

		/// <summary>Cliloc 1044500: "MASONRY MENU"</summary>
		public const int CLILOC_GUMP_TITLE = 1044500;

		#endregion

		#region Cliloc IDs - Tool Messages

		/// <summary>Cliloc 1044038: "You have worn out your tool!"</summary>
		public const int CLILOC_TOOL_WORN_OUT = 1044038;

		/// <summary>Cliloc 1048146: "If you have a tool equipped, you must use that tool."</summary>
		public const int CLILOC_TOOL_EQUIPPED = 1048146;

		/// <summary>Cliloc 1044633: "You havent learned stonecraft."</summary>
		public const int CLILOC_NOT_LEARNED_STONECRAFT = 1044633;

		/// <summary>Cliloc 1044263: "The tool must be on your person to use."</summary>
		public const int CLILOC_TOOL_MUST_BE_ON_PERSON = 1044263;

		#endregion

		#region Cliloc IDs - Crafting Results

		/// <summary>Cliloc 1044043: "You failed to create the item, and some of your materials are lost."</summary>
		public const int CLILOC_FAILED_MATERIAL_LOST = 1044043;

		/// <summary>Cliloc 1044157: "You failed to create the item, but no materials were lost."</summary>
		public const int CLILOC_FAILED_NO_MATERIAL_LOST = 1044157;

		/// <summary>Cliloc 502785: "You were barely able to make this item. It's quality is below average."</summary>
		public const int CLILOC_BARELY_MADE = 502785;

		/// <summary>Cliloc 1044156: "You create an exceptional quality item and affix your maker's mark."</summary>
		public const int CLILOC_EXCEPTIONAL_WITH_MARK = 1044156;

		/// <summary>Cliloc 1044155: "You create an exceptional quality item."</summary>
		public const int CLILOC_EXCEPTIONAL = 1044155;

		/// <summary>Cliloc 1044154: "You create the item."</summary>
		public const int CLILOC_ITEM_CREATED = 1044154;

		#endregion

		#region Cliloc IDs - Resources

		/// <summary>Cliloc 1044514: Granite resource name</summary>
		public const int CLILOC_GRANITE_RESOURCE = 1044514;

		/// <summary>Cliloc 1044513: Resource lack message</summary>
		public const int CLILOC_RESOURCE_LACK = 1044513;

		/// <summary>Cliloc 1044525: Granite subresource name (normal)</summary>
		public const int CLILOC_GRANITE_SUBRES = 1044525;

		/// <summary>Cliloc 1044526: Granite subresource message (normal)</summary>
		public const int CLILOC_GRANITE_SUBRES_MSG = 1044526;

		/// <summary>Cliloc 1044527: Colored granite subresource message</summary>
		public const int CLILOC_COLORED_GRANITE_MSG = 1044527;

		/// <summary>Cliloc 1024635: Stone Chairs</summary>
		public const int CLILOC_STONE_CHAIRS = 1024635;

		#endregion

		#region Cliloc IDs - Colored Granite Types

		public const int CLILOC_DULL_COPPER_GRANITE = 1044023;
		public const int CLILOC_SHADOW_IRON_GRANITE = 1044024;
		public const int CLILOC_COPPER_GRANITE = 1044025;
		public const int CLILOC_BRONZE_GRANITE = 1044026;
		public const int CLILOC_GOLD_GRANITE = 1044027;
		public const int CLILOC_AGAPITE_GRANITE = 1044028;
		public const int CLILOC_VERITE_GRANITE = 1044029;
		public const int CLILOC_VALORITE_GRANITE = 1044030;
		public const int CLILOC_NEPTURITE_GRANITE = 1036173;
		public const int CLILOC_OBSIDIAN_GRANITE = 1036162;
		public const int CLILOC_MITHRIL_GRANITE = 1036137;
		public const int CLILOC_XORMITE_GRANITE = 1034437;
		public const int CLILOC_DWARVEN_GRANITE = 1036181;

		// Custom granite types (non-standard clilocs)
		public const int CLILOC_PLATINUM_GRANITE = 6663000;
		public const int CLILOC_TITANIUM_GRANITE = 6661000;
		public const int CLILOC_ROSENIUM_GRANITE = 6662000;

		#endregion

		#region Granite Type Skill Requirements

		public const double GRANITE_NORMAL_SKILL = 0.0;
		public const double GRANITE_DULL_COPPER_SKILL = 65.0;
		public const double GRANITE_SHADOW_IRON_SKILL = 70.0;
		public const double GRANITE_COPPER_SKILL = 75.0;
		public const double GRANITE_BRONZE_SKILL = 80.0;
		public const double GRANITE_GOLD_SKILL = 85.0;
		public const double GRANITE_AGAPITE_SKILL = 90.0;
		public const double GRANITE_VERITE_SKILL = 95.0;
		public const double GRANITE_VALORITE_SKILL = 99.0;
		public const double GRANITE_PLATINUM_SKILL = 99.0;
		public const double GRANITE_TITANIUM_SKILL = 99.0;
		public const double GRANITE_ROSENIUM_SKILL = 99.0;
		public const double GRANITE_NEPTURITE_SKILL = 99.0;
		public const double GRANITE_OBSIDIAN_SKILL = 99.0;
		public const double GRANITE_MITHRIL_SKILL = 109.0;
		public const double GRANITE_XORMITE_SKILL = 109.0;
		public const double GRANITE_DWARVEN_SKILL = 110.0;

		#endregion

		#region Sound Effects

		/// <summary>Sound ID for stoneworking (0x65A)</summary>
		public const int SOUND_STONEWORKING = 0x65A;

		/// <summary>Sound ID for anvil hit (0x23D)</summary>
		public const int SOUND_ANVIL_HIT = 0x23D;

		/// <summary>Delay in seconds before anvil sound plays</summary>
		public const double SOUND_DELAY_SECONDS = 0.7;

		#endregion

		#region Quality Indicators

		/// <summary>Quality level for below average items</summary>
		public const int QUALITY_BELOW_AVERAGE = 0;

		/// <summary>Quality level for exceptional items</summary>
		public const int QUALITY_EXCEPTIONAL = 2;

		#endregion
	}
}

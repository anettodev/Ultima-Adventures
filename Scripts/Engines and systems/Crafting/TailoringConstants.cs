namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for Tailoring crafting system calculations and mechanics.
	/// Extracted from DefTailoring.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class TailoringConstants
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

		/// <summary>Sound ID for tailoring craft effect (0x248)</summary>
		public const int SOUND_TAILORING_CRAFT = 0x248;

		#endregion

		#region Message Numbers (Cliloc)

		/// <summary>Gump title: "TAILORING MENU" (cliloc 1044005)</summary>
		public const int MSG_GUMP_TITLE = 1044005;

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

		/// <summary>Resource name: "Cotton Cloth" (cliloc 1044286)</summary>
		public const int MSG_COTTON_CLOTH = 1044286;

		/// <summary>Error: "You don't have enough cloth." (cliloc 1044287)</summary>
		public const int MSG_INSUFFICIENT_CLOTH = 1044287;

		/// <summary>Resource name: "Leather" (cliloc 1044462)</summary>
		public const int MSG_LEATHER = 1044462;

		/// <summary>Error: "You don't have enough leather." (cliloc 1044463)</summary>
		public const int MSG_INSUFFICIENT_LEATHER = 1044463;

		/// <summary>Error: "You don't have the required items." (cliloc 1042081)</summary>
		public const int MSG_INSUFFICIENT_RESOURCES = 1042081;

		/// <summary>Error: "You don't have the required skill." (cliloc 1049063)</summary>
		public const int MSG_INSUFFICIENT_SKILL = 1049063;

		/// <summary>Message: "You do not have enough cloth material." (cliloc 1044458)</summary>
		public const int MSG_NO_CLOTH_MATERIAL = 1044458;

		/// <summary>Message: "You cannot work this cloth." (cliloc 1054019)</summary>
		public const int MSG_CANNOT_WORK_CLOTH = 1054019;

		/// <summary>Message: "Goliath Leather" (cliloc 1061740)</summary>
		public const int MSG_GOLIATH_LEATHER = 1061740;

		/// <summary>Error: "You don't have enough goliath leather." (cliloc 1049311)</summary>
		public const int MSG_INSUFFICIENT_GOLIATH_LEATHER = 1049311;

		#endregion

		#region Quality Levels

		/// <summary>Quality level: Below average (0)</summary>
		public const int QUALITY_BELOW_AVERAGE = 0;

		/// <summary>Quality level: Exceptional (2)</summary>
		public const int QUALITY_EXCEPTIONAL = 2;

		#endregion

		#region Cloth Types (Cliloc)

		/// <summary>Cotton Cloth (cliloc 1067440)</summary>
		public const int MSG_CLOTH_COTTON = 1067440;

		/// <summary>Wool Cloth (cliloc 1067443)</summary>
		public const int MSG_CLOTH_WOOL = 1067443;

		/// <summary>Flax Cloth (cliloc 1067441)</summary>
		public const int MSG_CLOTH_FLAX = 1067441;

		/// <summary>Silk Cloth (cliloc 1067442)</summary>
		public const int MSG_CLOTH_SILK = 1067442;

		/// <summary>Poliester Cloth (cliloc 1067444)</summary>
		public const int MSG_CLOTH_POLIESTER = 1067444;

		#endregion

		#region Leather Types (Cliloc)

		/// <summary>Leather (cliloc 1049150)</summary>
		public const int MSG_LEATHER_REGULAR = 1049150;

		/// <summary>Spined Leather (cliloc 1049151)</summary>
		public const int MSG_LEATHER_SPINED = 1049151;

		/// <summary>Horned Leather (cliloc 1049152)</summary>
		public const int MSG_LEATHER_HORNED = 1049152;

		/// <summary>Barbed Leather (cliloc 1049153)</summary>
		public const int MSG_LEATHER_BARBED = 1049153;

		#endregion

		#region Item Names (Cliloc) - Hats

		/// <summary>Skull Cap (cliloc 1025444)</summary>
		public const int MSG_SKULL_CAP = 1025444;

		/// <summary>Bandana (cliloc 1025440)</summary>
		public const int MSG_BANDANA = 1025440;

		/// <summary>Floppy Hat (cliloc 1025907)</summary>
		public const int MSG_FLOPPY_HAT = 1025907;

		/// <summary>Cap (cliloc 1025909)</summary>
		public const int MSG_CAP = 1025909;

		/// <summary>Wide Brim Hat (cliloc 1025908)</summary>
		public const int MSG_WIDE_BRIM_HAT = 1025908;

		/// <summary>Straw Hat (cliloc 1025911)</summary>
		public const int MSG_STRAW_HAT = 1025911;

		/// <summary>Tall Straw Hat (cliloc 1025910)</summary>
		public const int MSG_TALL_STRAW_HAT = 1025910;

		/// <summary>Bonnet (cliloc 1025913)</summary>
		public const int MSG_BONNET = 1025913;

		/// <summary>Feathered Hat (cliloc 1025914)</summary>
		public const int MSG_FEATHERED_HAT = 1025914;

		/// <summary>Tricorne Hat (cliloc 1025915)</summary>
		public const int MSG_TRICORNE_HAT = 1025915;

		/// <summary>Jester Hat (cliloc 1025916)</summary>
		public const int MSG_JESTER_HAT = 1025916;

		/// <summary>Wizard's Hat (cliloc 1025912)</summary>
		public const int MSG_WIZARDS_HAT = 1025912;

		#endregion

		#region Item Names (Cliloc) - Shirts/Robes

		/// <summary>Doublet (cliloc 1028059)</summary>
		public const int MSG_DOUBLET = 1028059;

		/// <summary>Shirt (cliloc 1025399)</summary>
		public const int MSG_SHIRT = 1025399;

		/// <summary>Tunic (cliloc 1028097)</summary>
		public const int MSG_TUNIC = 1028097;

		/// <summary>Surcoat (cliloc 1028189)</summary>
		public const int MSG_SURCOAT = 1028189;

		/// <summary>Plain Dress (cliloc 1027937)</summary>
		public const int MSG_PLAIN_DRESS = 1027937;

		/// <summary>Fancy Dress (cliloc 1027935)</summary>
		public const int MSG_FANCY_DRESS = 1027935;

		/// <summary>Cloak (cliloc 1025397)</summary>
		public const int MSG_CLOAK = 1025397;

		/// <summary>Robe (cliloc 1027939)</summary>
		public const int MSG_ROBE = 1027939;

		/// <summary>Fancy Shirt (cliloc 1027933)</summary>
		public const int MSG_FANCY_SHIRT = 1027933;

		#endregion

		#region Item Names (Cliloc) - Pants

		/// <summary>Short Pants (cliloc 1025422)</summary>
		public const int MSG_SHORT_PANTS = 1025422;

		/// <summary>Long Pants (cliloc 1025433)</summary>
		public const int MSG_LONG_PANTS = 1025433;

		/// <summary>Kilt (cliloc 1025431)</summary>
		public const int MSG_KILT = 1025431;

		/// <summary>Skirt (cliloc 1025398)</summary>
		public const int MSG_SKIRT = 1025398;

		#endregion

		#region Item Names (Cliloc) - Footwear

		/// <summary>Sandals (cliloc 1025901)</summary>
		public const int MSG_SANDALS = 1025901;

		/// <summary>Shoes (cliloc 1025904)</summary>
		public const int MSG_SHOES = 1025904;

		/// <summary>Boots (cliloc 1025899)</summary>
		public const int MSG_BOOTS = 1025899;

		#endregion

		#region Item Names (Cliloc) - Misc

		/// <summary>Body Sash (cliloc 1025441)</summary>
		public const int MSG_BODY_SASH = 1025441;

		/// <summary>Half Apron (cliloc 1025435)</summary>
		public const int MSG_HALF_APRON = 1025435;

		/// <summary>Full Apron (cliloc 1025437)</summary>
		public const int MSG_FULL_APRON = 1025437;

		#endregion

		#region Item Names (Cliloc) - Leather Armor

		/// <summary>Leather Cap (cliloc 1027609)</summary>
		public const int MSG_LEATHER_CAP = 1027609;

		/// <summary>Leather Gorget (cliloc 1025063)</summary>
		public const int MSG_LEATHER_GORGET = 1025063;

		/// <summary>Leather Gloves (cliloc 1025062)</summary>
		public const int MSG_LEATHER_GLOVES = 1025062;

		/// <summary>Leather Arms (cliloc 1025061)</summary>
		public const int MSG_LEATHER_ARMS = 1025061;

		/// <summary>Leather Legs (cliloc 1025067)</summary>
		public const int MSG_LEATHER_LEGS = 1025067;

		/// <summary>Leather Chest (cliloc 1025068)</summary>
		public const int MSG_LEATHER_CHEST = 1025068;

		/// <summary>Leather Skirt (cliloc 1027176)</summary>
		public const int MSG_LEATHER_SKIRT = 1027176;

		/// <summary>Leather Shorts (cliloc 1027168)</summary>
		public const int MSG_LEATHER_SHORTS = 1027168;

		/// <summary>Leather Bustier Arms (cliloc 1027178)</summary>
		public const int MSG_LEATHER_BUSTIER_ARMS = 1027178;

		/// <summary>Female Leather Chest (cliloc 1027174)</summary>
		public const int MSG_FEMALE_LEATHER_CHEST = 1027174;

		/// <summary>Studded Gorget (cliloc 1025078)</summary>
		public const int MSG_STUDDED_GORGET = 1025078;

		/// <summary>Studded Gloves (cliloc 1025077)</summary>
		public const int MSG_STUDDED_GLOVES = 1025077;

		/// <summary>Studded Arms (cliloc 1025076)</summary>
		public const int MSG_STUDDED_ARMS = 1025076;

		/// <summary>Studded Legs (cliloc 1025082)</summary>
		public const int MSG_STUDDED_LEGS = 1025082;

		/// <summary>Studded Chest (cliloc 1025083)</summary>
		public const int MSG_STUDDED_CHEST = 1025083;

		/// <summary>Studded Bustier Arms (cliloc 1027180)</summary>
		public const int MSG_STUDDED_BUSTIER_ARMS = 1027180;

		/// <summary>Female Studded Chest (cliloc 1027170)</summary>
		public const int MSG_FEMALE_STUDDED_CHEST = 1027170;

		#endregion

		#region Item Names (Cliloc) - Bone Armor

		/// <summary>Bone Helm (cliloc 1025206)</summary>
		public const int MSG_BONE_HELM = 1025206;

		/// <summary>Bone Gloves (cliloc 1025205)</summary>
		public const int MSG_BONE_GLOVES = 1025205;

		/// <summary>Bone Arms (cliloc 1025203)</summary>
		public const int MSG_BONE_ARMS = 1025203;

		/// <summary>Bone Legs (cliloc 1025202)</summary>
		public const int MSG_BONE_LEGS = 1025202;

		/// <summary>Bone Chest (cliloc 1025199)</summary>
		public const int MSG_BONE_CHEST = 1025199;

		#endregion

		#region Skill Requirements - Cloth Type Sub-Resources

		/// <summary>Skill required for cotton cloth</summary>
		public const double SKILL_REQ_CLOTH_COTTON = 0.0;

		/// <summary>Skill required for wool cloth</summary>
		public const double SKILL_REQ_CLOTH_WOOL = 60.0;

		/// <summary>Skill required for flax cloth</summary>
		public const double SKILL_REQ_CLOTH_FLAX = 70.0;

		/// <summary>Skill required for silk cloth</summary>
		public const double SKILL_REQ_CLOTH_SILK = 80.0;

		/// <summary>Skill required for poliester cloth</summary>
		public const double SKILL_REQ_CLOTH_POLIESTER = 80.0;

		#endregion

		#region Skill Requirements - Leather Type Sub-Resources

		/// <summary>Skill required for regular leather</summary>
		public const double SKILL_REQ_LEATHER_REGULAR = 0.0;

		/// <summary>Skill required for spined leather</summary>
		public const double SKILL_REQ_LEATHER_SPINED = 70.0;

		/// <summary>Skill required for horned leather</summary>
		public const double SKILL_REQ_LEATHER_HORNED = 85.0;

		/// <summary>Skill required for barbed leather</summary>
		public const double SKILL_REQ_LEATHER_BARBED = 80.0;

		#endregion

		#region Magery Skill Requirements

		/// <summary>Magery skill minimum for wizard's hat</summary>
		public const double MAGERY_SKILL_MIN_WIZARDS_HAT = 90.0;

		/// <summary>Magery skill maximum for wizard's hat</summary>
		public const double MAGERY_SKILL_MAX_WIZARDS_HAT = 100.0;

		/// <summary>Magery skill minimum for witch hat</summary>
		public const double MAGERY_SKILL_MIN_WITCH_HAT = 90.0;

		/// <summary>Magery skill maximum for witch hat</summary>
		public const double MAGERY_SKILL_MAX_WITCH_HAT = 100.0;

		/// <summary>Magery skill minimum for miners pouch</summary>
		public const double MAGERY_SKILL_MIN_MINERS_POUCH = 90.0;

		/// <summary>Magery skill maximum for miners pouch</summary>
		public const double MAGERY_SKILL_MAX_MINERS_POUCH = 100.0;

		/// <summary>Magery skill minimum for lumberjack pouch</summary>
		public const double MAGERY_SKILL_MIN_LUMBERJACK_POUCH = 90.0;

		/// <summary>Magery skill maximum for lumberjack pouch</summary>
		public const double MAGERY_SKILL_MAX_LUMBERJACK_POUCH = 100.0;

		#endregion
	}
}

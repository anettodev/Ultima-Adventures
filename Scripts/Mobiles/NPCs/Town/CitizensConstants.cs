namespace Server.Mobiles
{
	/// <summary>
	/// Centralized constants for Citizens NPC calculations and mechanics.
	/// Extracted from Citizens.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class CitizensConstants
	{
		#region Body and Appearance

		/// <summary>Female body type ID</summary>
		public const int BODY_FEMALE = 401;

		/// <summary>Male body type ID</summary>
		public const int BODY_MALE = 400;

		/// <summary>Facial hair item IDs for randomization</summary>
		public static readonly int[] FACIAL_HAIR_IDS = new int[] { 0, 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 };

		#endregion

		#region Weapon Item IDs

		/// <summary>Weapon item IDs that should be colored with metal color</summary>
		public static readonly int[] METAL_WEAPON_IDS = new int[] { 0x26BC, 0x26C6, 0x269D, 0x269E, 0xDF2, 0xDF3, 0xDF4, 0xDF5 };

		#endregion

		#region Stats

		/// <summary>Minimum strength value</summary>
		public const int STAT_STR_MIN = 386;

		/// <summary>Maximum strength value</summary>
		public const int STAT_STR_MAX = 400;

		/// <summary>Minimum dexterity value</summary>
		public const int STAT_DEX_MIN = 151;

		/// <summary>Maximum dexterity value</summary>
		public const int STAT_DEX_MAX = 165;

		/// <summary>Minimum intelligence value</summary>
		public const int STAT_INT_MIN = 161;

		/// <summary>Maximum intelligence value</summary>
		public const int STAT_INT_MAX = 175;

		/// <summary>Minimum hits value</summary>
		public const int HITS_MIN = 300;

		/// <summary>Maximum hits value</summary>
		public const int HITS_MAX = 400;

		/// <summary>Minimum damage value</summary>
		public const int DAMAGE_MIN = 8;

		/// <summary>Maximum damage value</summary>
		public const int DAMAGE_MAX = 10;

		/// <summary>Physical damage type percentage</summary>
		public const int DAMAGE_TYPE_PHYSICAL = 100;

		#endregion

		#region Resistances

		/// <summary>Minimum physical resistance</summary>
		public const int RESIST_PHYSICAL_MIN = 35;

		/// <summary>Maximum physical resistance</summary>
		public const int RESIST_PHYSICAL_MAX = 45;

		/// <summary>Minimum fire resistance</summary>
		public const int RESIST_FIRE_MIN = 25;

		/// <summary>Maximum fire resistance</summary>
		public const int RESIST_FIRE_MAX = 30;

		/// <summary>Minimum cold resistance</summary>
		public const int RESIST_COLD_MIN = 25;

		/// <summary>Maximum cold resistance</summary>
		public const int RESIST_COLD_MAX = 30;

		/// <summary>Minimum poison resistance</summary>
		public const int RESIST_POISON_MIN = 10;

		/// <summary>Maximum poison resistance</summary>
		public const int RESIST_POISON_MAX = 20;

		/// <summary>Minimum energy resistance</summary>
		public const int RESIST_ENERGY_MIN = 10;

		/// <summary>Maximum energy resistance</summary>
		public const int RESIST_ENERGY_MAX = 20;

		#endregion

		#region Skills

		/// <summary>Minimum skill value for all citizen skills</summary>
		public const double SKILL_MIN = 60.0;

		/// <summary>Maximum skill value for all citizen skills</summary>
		public const double SKILL_MAX = 82.5;

		#endregion

		#region Armor and Equipment

		/// <summary>Virtual armor value</summary>
		public const int VIRTUAL_ARMOR = 30;

		#endregion

		#region Citizen Types

		/// <summary>Citizen type: Wizard</summary>
		public const int CITIZEN_TYPE_WIZARD = 1;

		/// <summary>Citizen type: Fighter</summary>
		public const int CITIZEN_TYPE_FIGHTER = 2;

		/// <summary>Citizen type: Rogue</summary>
		public const int CITIZEN_TYPE_ROGUE = 3;

		/// <summary>Citizen type: Smith</summary>
		public const int CITIZEN_TYPE_SMITH = 4;

		/// <summary>Citizen type: Lumberjack</summary>
		public const int CITIZEN_TYPE_LUMBERJACK = 5;

		/// <summary>Citizen type: Leather Worker</summary>
		public const int CITIZEN_TYPE_LEATHER = 6;

		/// <summary>Citizen type: Miner</summary>
		public const int CITIZEN_TYPE_MINER = 7;

		/// <summary>Citizen type: Smelter</summary>
		public const int CITIZEN_TYPE_SMELTER = 8;

		/// <summary>Citizen type: Alchemist</summary>
		public const int CITIZEN_TYPE_ALCHEMIST = 9;

		/// <summary>Citizen type: Cook</summary>
		public const int CITIZEN_TYPE_COOK = 10;

		/// <summary>Citizen type: Metal Vendor</summary>
		public const int CITIZEN_TYPE_METAL_VENDOR = 20;

		/// <summary>Citizen type: Wood Vendor</summary>
		public const int CITIZEN_TYPE_WOOD_VENDOR = 21;

		/// <summary>Citizen type: Leather Vendor</summary>
		public const int CITIZEN_TYPE_LEATHER_VENDOR = 22;

		/// <summary>Citizen type: Ore Vendor</summary>
		public const int CITIZEN_TYPE_ORE_VENDOR = 23;

		/// <summary>Citizen type: Reagent Vendor</summary>
		public const int CITIZEN_TYPE_REAGENT_VENDOR = 24;

		/// <summary>Citizen type: Potion Vendor</summary>
		public const int CITIZEN_TYPE_POTION_VENDOR = 25;

		/// <summary>Citizen type: Food Vendor</summary>
		public const int CITIZEN_TYPE_FOOD_VENDOR = 26;

		#endregion

		#region Citizen Services

		/// <summary>Service: Repair/Unlock/Wand Recharge</summary>
		public const int CITIZEN_SERVICE_REPAIR = 1;

		/// <summary>Service: Weapon/Armor Repair</summary>
		public const int CITIZEN_SERVICE_REPAIR_2 = 2;

		/// <summary>Service: Wood Weapon Repair</summary>
		public const int CITIZEN_SERVICE_REPAIR_WOOD_WEAPON = 3;

		/// <summary>Service: Wood Armor Repair</summary>
		public const int CITIZEN_SERVICE_REPAIR_WOOD_ARMOR = 4;

		/// <summary>Service: Magic Item Sale</summary>
		public const int CITIZEN_SERVICE_MAGIC_ITEM = 5;

		/// <summary>Service: Spellbook/Runebook Sale</summary>
		public const int CITIZEN_SERVICE_BOOK = 6;

		/// <summary>Service: Scroll Sale</summary>
		public const int CITIZEN_SERVICE_SCROLL = 7;

		/// <summary>Service: Wand Sale</summary>
		public const int CITIZEN_SERVICE_WAND = 8;

		/// <summary>Service: Wizard Reagent Jar 1</summary>
		public const int CITIZEN_SERVICE_REAGENT_JAR_1 = 2;

		/// <summary>Service: Wizard Reagent Jar 2</summary>
		public const int CITIZEN_SERVICE_REAGENT_JAR_2 = 3;

		/// <summary>Service: Wizard Reagent Jar 3</summary>
		public const int CITIZEN_SERVICE_REAGENT_JAR_3 = 4;

		#endregion

		#region Random Ranges

		/// <summary>Minimum value for dungeon/city random replacement</summary>
		public const int RANDOM_DUNGEON_MIN = 1;

		/// <summary>Maximum value for dungeon/city random replacement</summary>
		public const int RANDOM_DUNGEON_MAX = 3;

		/// <summary>Random replacement threshold (if roll == 1, use random)</summary>
		public const int RANDOM_DUNGEON_THRESHOLD = 1;

		/// <summary>Minimum relic value</summary>
		public const int RELIC_MIN = 1;

		/// <summary>Maximum relic value</summary>
		public const int RELIC_MAX = 59;

		/// <summary>Minimum topic value for rumors</summary>
		public const int TOPIC_MIN = 0;

		/// <summary>Maximum topic value for rumors</summary>
		public const int TOPIC_MAX = 40;

		/// <summary>Topic value for HouseVisitor (disables rumors)</summary>
		public const int TOPIC_HOUSE_VISITOR = 100;

		/// <summary>Minimum preface index</summary>
		public const int PREFACE_MIN = 0;

		/// <summary>Maximum preface index</summary>
		public const int PREFACE_MAX = 13;

		/// <summary>Minimum initial phrase index</summary>
		public const int INIT_PHRASE_MIN = 0;

		/// <summary>Maximum initial phrase index</summary>
		public const int INIT_PHRASE_MAX = 6;

		/// <summary>Maximum initial phrase index for tavern patrons</summary>
		public const int INIT_PHRASE_MAX_TAVERN = 4;

		/// <summary>Wizard service chance denominator</summary>
		public const int WIZARD_SERVICE_CHANCE = 10;

		/// <summary>Wizard service chance threshold</summary>
		public const int WIZARD_SERVICE_THRESHOLD = 1;

		/// <summary>Special type random range maximum</summary>
		public const int SPECIAL_TYPE_RANDOM_MAX = 50;

		/// <summary>Special type random range minimum</summary>
		public const int SPECIAL_TYPE_RANDOM_MIN = 1;

		#endregion

		#region Material Random Ranges

		/// <summary>Maximum value for material randomization (65536 = 2^16)</summary>
		public const int MATERIAL_RANDOM_MAX = 65536;

		/// <summary>Minimum value for material randomization</summary>
		public const int MATERIAL_RANDOM_MIN = 1;

		/// <summary>Iron threshold (50% chance)</summary>
		public const int MATERIAL_THRESHOLD_IRON = 32768;

		/// <summary>Dull Copper threshold (25% chance)</summary>
		public const int MATERIAL_THRESHOLD_DULL_COPPER = 16384;

		/// <summary>Shadow Iron threshold (12.5% chance)</summary>
		public const int MATERIAL_THRESHOLD_SHADOW_IRON = 8192;

		/// <summary>Copper threshold (6.25% chance)</summary>
		public const int MATERIAL_THRESHOLD_COPPER = 4096;

		/// <summary>Bronze threshold (3.125% chance)</summary>
		public const int MATERIAL_THRESHOLD_BRONZE = 2048;

		/// <summary>Gold threshold (1.5625% chance)</summary>
		public const int MATERIAL_THRESHOLD_GOLD = 1024;

		/// <summary>Agapite threshold (0.78125% chance)</summary>
		public const int MATERIAL_THRESHOLD_AGAPITE = 512;

		/// <summary>Verite threshold (0.390625% chance)</summary>
		public const int MATERIAL_THRESHOLD_VERITE = 256;

		/// <summary>Valorite threshold (0.1953125% chance)</summary>
		public const int MATERIAL_THRESHOLD_VALORITE = 128;

		/// <summary>Nepturite threshold (0.09765625% chance)</summary>
		public const int MATERIAL_THRESHOLD_NEPTURITE = 64;

		/// <summary>Obsidian threshold (0.048828125% chance)</summary>
		public const int MATERIAL_THRESHOLD_OBSIDIAN = 32;

		/// <summary>Steel threshold (0.0244140625% chance)</summary>
		public const int MATERIAL_THRESHOLD_STEEL = 16;

		/// <summary>Brass threshold (0.01220703125% chance)</summary>
		public const int MATERIAL_THRESHOLD_BRASS = 8;

		/// <summary>Mithril threshold (0.006103515625% chance)</summary>
		public const int MATERIAL_THRESHOLD_MITHRIL = 4;

		/// <summary>Xormite threshold (0.0030517578125% chance)</summary>
		public const int MATERIAL_THRESHOLD_XORMITE = 2;

		#endregion

		#region Item IDs - Metal Ingots

		/// <summary>Iron ingot item ID</summary>
		public const int ITEM_ID_IRON_INGOT = 0x5094;

		/// <summary>Metal ingot item ID (non-iron)</summary>
		public const int ITEM_ID_METAL_INGOT = 0x5095;

		#endregion

		#region Item IDs - Wood

		/// <summary>Regular wood board item ID</summary>
		public const int ITEM_ID_WOOD_BOARD = 0x5088;

		/// <summary>Special wood board item ID</summary>
		public const int ITEM_ID_WOOD_BOARD_SPECIAL = 0x5085;

		/// <summary>Regular log item ID</summary>
		public const int ITEM_ID_LOG_REGULAR = 0x5097;

		/// <summary>Special log item ID</summary>
		public const int ITEM_ID_LOG_SPECIAL = 0x5096;

		#endregion

		#region Item IDs - Leather

		/// <summary>Regular leather item ID</summary>
		public const int ITEM_ID_LEATHER_REGULAR = 0x5092;

		/// <summary>Special leather item ID</summary>
		public const int ITEM_ID_LEATHER_SPECIAL = 0x5093;

		#endregion

		#region Item IDs - Ore

		/// <summary>Iron ore item ID</summary>
		public const int ITEM_ID_ORE_IRON = 0x5084;

		/// <summary>Metal ore item ID (non-iron)</summary>
		public const int ITEM_ID_ORE_METAL = 0x50B5;

		#endregion

		#region Item IDs - Reagents

		/// <summary>Bloodmoss reagent item ID</summary>
		public const int ITEM_ID_REAGENT_BLOODMOSS = 0x508E;

		/// <summary>Black Pearl reagent item ID</summary>
		public const int ITEM_ID_REAGENT_BLACK_PEARL = 0x508F;

		/// <summary>Garlic reagent item ID</summary>
		public const int ITEM_ID_REAGENT_GARLIC = 0x5098;

		/// <summary>Ginseng reagent item ID</summary>
		public const int ITEM_ID_REAGENT_GINSENG = 0x5099;

		/// <summary>Mandrake Root reagent item ID</summary>
		public const int ITEM_ID_REAGENT_MANDRAKE_ROOT = 0x509A;

		/// <summary>Nightshade reagent item ID</summary>
		public const int ITEM_ID_REAGENT_NIGHTSHADE = 0x509B;

		/// <summary>Sulfurous Ash reagent item ID</summary>
		public const int ITEM_ID_REAGENT_SULFUROUS_ASH = 0x509C;

		/// <summary>Spider Silk reagent item ID</summary>
		public const int ITEM_ID_REAGENT_SPIDER_SILK = 0x509D;

		/// <summary>Swamp Berry reagent item ID</summary>
		public const int ITEM_ID_REAGENT_SWAMP_BERRY = 0x568A;

		/// <summary>Bat Wing reagent item ID</summary>
		public const int ITEM_ID_REAGENT_BAT_WING = 0x55E0;

		/// <summary>Beetle Shell reagent item ID</summary>
		public const int ITEM_ID_REAGENT_BEETLE_SHELL = 0x55E1;

		/// <summary>Brimstone reagent item ID</summary>
		public const int ITEM_ID_REAGENT_BRIMSTONE = 0x55E2;

		/// <summary>Butterfly reagent item ID</summary>
		public const int ITEM_ID_REAGENT_BUTTERFLY = 0x55E3;

		/// <summary>Daemon Blood reagent item ID</summary>
		public const int ITEM_ID_REAGENT_DAEMON_BLOOD = 0x55E4;

		/// <summary>Toad Eyes reagent item ID</summary>
		public const int ITEM_ID_REAGENT_TOAD_EYES = 0x55E5;

		/// <summary>Fairy Eggs reagent item ID</summary>
		public const int ITEM_ID_REAGENT_FAIRY_EGGS = 0x55E6;

		/// <summary>Gargoyle Ears reagent item ID</summary>
		public const int ITEM_ID_REAGENT_GARGOYLE_EARS = 0x55E7;

		/// <summary>Grave Dust reagent item ID</summary>
		public const int ITEM_ID_REAGENT_GRAVE_DUST = 0x55E8;

		/// <summary>Moon Crystals reagent item ID</summary>
		public const int ITEM_ID_REAGENT_MOON_CRYSTALS = 0x55E9;

		/// <summary>Nox Crystal reagent item ID</summary>
		public const int ITEM_ID_REAGENT_NOX_CRYSTAL = 0x55EA;

		/// <summary>Silver Widow reagent item ID</summary>
		public const int ITEM_ID_REAGENT_SILVER_WIDOW = 0x55EB;

		/// <summary>Pig Iron reagent item ID</summary>
		public const int ITEM_ID_REAGENT_PIG_IRON = 0x55EC;

		/// <summary>Pixie Skull reagent item ID</summary>
		public const int ITEM_ID_REAGENT_PIXIE_SKULL = 0x55ED;

		/// <summary>Red Lotus reagent item ID</summary>
		public const int ITEM_ID_REAGENT_RED_LOTUS = 0x55EE;

		/// <summary>Sea Salt reagent item ID</summary>
		public const int ITEM_ID_REAGENT_SEA_SALT = 0x55EF;

		/// <summary>Maximum reagent index for randomization</summary>
		public const int REAGENT_MAX_INDEX = 24;

		#endregion

		#region Item IDs - Potions

		/// <summary>Nightsight potion jug item ID</summary>
		public const int ITEM_ID_POTION_NIGHTSIGHT = 1109;

		/// <summary>Cure potion jug item ID</summary>
		public const int ITEM_ID_POTION_CURE = 45;

		/// <summary>Agility potion jug item ID</summary>
		public const int ITEM_ID_POTION_AGILITY = 396;

		/// <summary>Strength potion jug item ID</summary>
		public const int ITEM_ID_POTION_STRENGTH = 1001;

		/// <summary>Poison potion jug item ID</summary>
		public const int ITEM_ID_POTION_POISON = 73;

		/// <summary>Refresh potion jug item ID</summary>
		public const int ITEM_ID_POTION_REFRESH = 140;

		/// <summary>Heal potion jug item ID</summary>
		public const int ITEM_ID_POTION_HEAL = 50;

		/// <summary>Explosion potion jug item ID</summary>
		public const int ITEM_ID_POTION_EXPLOSION = 425;

		/// <summary>Invisibility potion jug item ID</summary>
		public const int ITEM_ID_POTION_INVISIBILITY = 0x490;

		/// <summary>Rejuvenate potion jug item ID</summary>
		public const int ITEM_ID_POTION_REJUVENATE = 0x48E;

		/// <summary>Mana potion jug item ID</summary>
		public const int ITEM_ID_POTION_MANA = 0x48D;

		/// <summary>Conflagration potion jug item ID</summary>
		public const int ITEM_ID_POTION_CONFLAGRATION = 0xAD8;

		/// <summary>Confusion Blast potion jug item ID</summary>
		public const int ITEM_ID_POTION_CONFUSION_BLAST = 0x495;

		/// <summary>Frostbite potion jug item ID</summary>
		public const int ITEM_ID_POTION_FROSTBITE = 0xAF3;

		/// <summary>Acid bottle item ID</summary>
		public const int ITEM_ID_POTION_ACID = 1167;

		/// <summary>Potion crate item ID</summary>
		public const int ITEM_ID_POTION_CRATE = 0x55DF;

		/// <summary>Maximum potion index for randomization</summary>
		public const int POTION_MAX_INDEX = 36;

		#endregion

		#region Item IDs - Food

		/// <summary>Cooked fish steak item ID</summary>
		public const int ITEM_ID_FOOD_FISH_STEAK = 0x508B;

		/// <summary>Cooked lamb leg item ID</summary>
		public const int ITEM_ID_FOOD_LAMB_LEG = 0x508C;

		/// <summary>Cooked ribs item ID</summary>
		public const int ITEM_ID_FOOD_RIBS = 0x508D;

		/// <summary>Baked bread item ID</summary>
		public const int ITEM_ID_FOOD_BREAD = 0x50BA;

		/// <summary>Maximum food index for randomization</summary>
		public const int FOOD_MAX_INDEX = 3;

		#endregion

		#region Costs

		/// <summary>Repair cost in gold</summary>
		public const int COST_REPAIR = 7500;

		/// <summary>Reagent jar 1 cost minimum</summary>
		public const int COST_REAGENT_JAR_1_MIN = 70;

		/// <summary>Reagent jar 1 cost maximum</summary>
		public const int COST_REAGENT_JAR_1_MAX = 150;

		/// <summary>Reagent jar 2 cost minimum</summary>
		public const int COST_REAGENT_JAR_2_MIN = 50;

		/// <summary>Reagent jar 2 cost maximum</summary>
		public const int COST_REAGENT_JAR_2_MAX = 90;

		/// <summary>Reagent jar 3 cost minimum</summary>
		public const int COST_REAGENT_JAR_3_MIN = 180;

		/// <summary>Reagent jar 3 cost maximum</summary>
		public const int COST_REAGENT_JAR_3_MAX = 300;

		/// <summary>Spellbook cost multiplier minimum</summary>
		public const int COST_SPELLBOOK_MULT_MIN = 500;

		/// <summary>Spellbook cost multiplier maximum</summary>
		public const int COST_SPELLBOOK_MULT_MAX = 800;

		/// <summary>Necromancer spellbook cost multiplier minimum</summary>
		public const int COST_NECRO_SPELLBOOK_MULT_MIN = 800;

		/// <summary>Necromancer spellbook cost multiplier maximum</summary>
		public const int COST_NECRO_SPELLBOOK_MULT_MAX = 1000;

		/// <summary>Runebook cost minimum</summary>
		public const int COST_RUNEBOOK_MIN = 150;

		/// <summary>Runebook cost maximum</summary>
		public const int COST_RUNEBOOK_MAX = 230;

		/// <summary>Scroll cost base minimum</summary>
		public const int COST_SCROLL_BASE_MIN = 8;

		/// <summary>Scroll cost base maximum</summary>
		public const int COST_SCROLL_BASE_MAX = 12;

		/// <summary>Wand cost base minimum</summary>
		public const int COST_WAND_BASE_MIN = 20;

		/// <summary>Wand cost base maximum</summary>
		public const int COST_WAND_BASE_MAX = 60;

		/// <summary>Wand cost multiplier for IntRequirement 10</summary>
		public const int COST_WAND_MULT_10 = 5;

		/// <summary>Wand cost multiplier for IntRequirement 15</summary>
		public const int COST_WAND_MULT_15 = 10;

		/// <summary>Wand cost multiplier for IntRequirement 20</summary>
		public const int COST_WAND_MULT_20 = 15;

		/// <summary>Wand cost multiplier for IntRequirement 25</summary>
		public const int COST_WAND_MULT_25 = 20;

		/// <summary>Wand cost multiplier for IntRequirement 30</summary>
		public const int COST_WAND_MULT_30 = 25;

		/// <summary>Wand cost multiplier for IntRequirement 35</summary>
		public const int COST_WAND_MULT_35 = 30;

		/// <summary>Wand cost multiplier for IntRequirement 40</summary>
		public const int COST_WAND_MULT_40 = 35;

		/// <summary>Wand cost multiplier for IntRequirement 45</summary>
		public const int COST_WAND_MULT_45 = 40;

		/// <summary>Item value minimum</summary>
		public const int ITEM_VALUE_MIN = 25;

		/// <summary>Item value maximum</summary>
		public const int ITEM_VALUE_MAX = 100;

		/// <summary>Item properties minimum</summary>
		public const int ITEM_PROPERTIES_MIN = 5;

		/// <summary>Item properties maximum</summary>
		public const int ITEM_PROPERTIES_MAX = 10;

		/// <summary>Item luck minimum</summary>
		public const int ITEM_LUCK_MIN = 0;

		/// <summary>Item luck maximum</summary>
		public const int ITEM_LUCK_MAX = 200;

		/// <summary>Item chance minimum</summary>
		public const int ITEM_CHANCE_MIN = 1;

		/// <summary>Item chance maximum</summary>
		public const int ITEM_CHANCE_MAX = 100;

		/// <summary>Item chance threshold for regular items (80%)</summary>
		public const int ITEM_CHANCE_THRESHOLD_REGULAR = 80;

		/// <summary>Item chance threshold for clothing (90%)</summary>
		public const int ITEM_CHANCE_THRESHOLD_CLOTHING = 90;

		/// <summary>Item chance threshold for instruments (95%)</summary>
		public const int ITEM_CHANCE_THRESHOLD_INSTRUMENT = 95;

		/// <summary>Item cost multiplier</summary>
		public const int ITEM_COST_MULTIPLIER = 20;

		/// <summary>Arty cost minimum</summary>
		public const int COST_ARTY_MIN = 250;

		/// <summary>Arty cost maximum</summary>
		public const int COST_ARTY_MAX = 750;

		/// <summary>Reagent cost per unit</summary>
		public const int COST_REAGENT_PER_UNIT = 5;

		/// <summary>Food cost: Fish steak</summary>
		public const int COST_FOOD_FISH_STEAK = 6;

		/// <summary>Food cost: Lamb leg</summary>
		public const int COST_FOOD_LAMB_LEG = 8;

		/// <summary>Food cost: Ribs</summary>
		public const int COST_FOOD_RIBS = 7;

		/// <summary>Food cost: Bread</summary>
		public const int COST_FOOD_BREAD = 6;

		#endregion

		#region Material Costs

		/// <summary>Iron material cost per unit</summary>
		public const int MATERIAL_COST_IRON = 2;

		/// <summary>Dull Copper material cost per unit</summary>
		public const int MATERIAL_COST_DULL_COPPER = 4;

		/// <summary>Shadow Iron material cost per unit</summary>
		public const int MATERIAL_COST_SHADOW_IRON = 6;

		/// <summary>Copper material cost per unit</summary>
		public const int MATERIAL_COST_COPPER = 8;

		/// <summary>Bronze material cost per unit</summary>
		public const int MATERIAL_COST_BRONZE = 10;

		/// <summary>Gold material cost per unit</summary>
		public const int MATERIAL_COST_GOLD = 12;

		/// <summary>Agapite material cost per unit</summary>
		public const int MATERIAL_COST_AGAPITE = 14;

		/// <summary>Verite material cost per unit</summary>
		public const int MATERIAL_COST_VERITE = 16;

		/// <summary>Valorite material cost per unit</summary>
		public const int MATERIAL_COST_VALORITE = 18;

		/// <summary>Nepturite material cost per unit</summary>
		public const int MATERIAL_COST_NEPTURITE = 20;

		/// <summary>Obsidian material cost per unit</summary>
		public const int MATERIAL_COST_OBSIDIAN = 22;

		/// <summary>Steel material cost per unit</summary>
		public const int MATERIAL_COST_STEEL = 24;

		/// <summary>Brass material cost per unit</summary>
		public const int MATERIAL_COST_BRASS = 26;

		/// <summary>Mithril material cost per unit</summary>
		public const int MATERIAL_COST_MITHRIL = 28;

		/// <summary>Xormite material cost per unit</summary>
		public const int MATERIAL_COST_XORMITE = 30;

		/// <summary>Dwarven material cost per unit</summary>
		public const int MATERIAL_COST_DWARVEN = 32;

		#endregion

		#region Material Quantities

		/// <summary>Iron material base quantity</summary>
		public const int MATERIAL_QTY_IRON = 80;

		/// <summary>Dull Copper material base quantity</summary>
		public const int MATERIAL_QTY_DULL_COPPER = 75;

		/// <summary>Shadow Iron material base quantity</summary>
		public const int MATERIAL_QTY_SHADOW_IRON = 70;

		/// <summary>Copper material base quantity</summary>
		public const int MATERIAL_QTY_COPPER = 65;

		/// <summary>Bronze material base quantity</summary>
		public const int MATERIAL_QTY_BRONZE = 60;

		/// <summary>Gold material base quantity</summary>
		public const int MATERIAL_QTY_GOLD = 55;

		/// <summary>Agapite material base quantity</summary>
		public const int MATERIAL_QTY_AGAPITE = 50;

		/// <summary>Verite material base quantity</summary>
		public const int MATERIAL_QTY_VERITE = 45;

		/// <summary>Valorite material base quantity</summary>
		public const int MATERIAL_QTY_VALORITE = 40;

		/// <summary>Nepturite material base quantity</summary>
		public const int MATERIAL_QTY_NEPTURITE = 35;

		/// <summary>Obsidian material base quantity</summary>
		public const int MATERIAL_QTY_OBSIDIAN = 30;

		/// <summary>Steel material base quantity</summary>
		public const int MATERIAL_QTY_STEEL = 25;

		/// <summary>Brass material base quantity</summary>
		public const int MATERIAL_QTY_BRASS = 20;

		/// <summary>Mithril material base quantity</summary>
		public const int MATERIAL_QTY_MITHRIL = 15;

		/// <summary>Xormite material base quantity</summary>
		public const int MATERIAL_QTY_XORMITE = 10;

		/// <summary>Dwarven material base quantity</summary>
		public const int MATERIAL_QTY_DWARVEN = 5;

		/// <summary>Crate quantity multiplier minimum</summary>
		public const int CRATE_QTY_MULT_MIN = 5;

		/// <summary>Crate quantity multiplier maximum</summary>
		public const int CRATE_QTY_MULT_MAX = 15;

		/// <summary>Reagent quantity minimum</summary>
		public const int REAGENT_QTY_MIN = 400;

		/// <summary>Reagent quantity maximum</summary>
		public const int REAGENT_QTY_MAX = 1200;

		/// <summary>Potion quantity minimum</summary>
		public const int POTION_QTY_MIN = 30;

		/// <summary>Potion quantity maximum</summary>
		public const int POTION_QTY_MAX = 100;

		/// <summary>Food quantity minimum</summary>
		public const int FOOD_QTY_MIN = 50;

		/// <summary>Food quantity maximum</summary>
		public const int FOOD_QTY_MAX = 150;

		/// <summary>Weight multiplier for crates</summary>
		public const double CRATE_WEIGHT_MULTIPLIER = 0.1;

		#endregion

		#region Wand Charges

		/// <summary>Wand charges for IntRequirement 10</summary>
		public const int WAND_CHARGES_10 = 30;

		/// <summary>Wand charges for IntRequirement 15</summary>
		public const int WAND_CHARGES_15 = 23;

		/// <summary>Wand charges for IntRequirement 20</summary>
		public const int WAND_CHARGES_20 = 18;

		/// <summary>Wand charges for IntRequirement 25</summary>
		public const int WAND_CHARGES_25 = 15;

		/// <summary>Wand charges for IntRequirement 30</summary>
		public const int WAND_CHARGES_30 = 12;

		/// <summary>Wand charges for IntRequirement 35</summary>
		public const int WAND_CHARGES_35 = 9;

		/// <summary>Wand charges for IntRequirement 40</summary>
		public const int WAND_CHARGES_40 = 6;

		/// <summary>Wand charges for IntRequirement 45</summary>
		public const int WAND_CHARGES_45 = 3;

		#endregion

		#region Scroll Multipliers

		/// <summary>Scroll cost multiplier tier 1 (low level)</summary>
		public const int SCROLL_MULT_TIER_1 = 10;

		/// <summary>Scroll cost multiplier tier 2 (mid level)</summary>
		public const int SCROLL_MULT_TIER_2 = 20;

		/// <summary>Scroll cost multiplier tier 3 (high level)</summary>
		public const int SCROLL_MULT_TIER_3 = 40;

		/// <summary>Scroll cost multiplier tier 4 (very high level)</summary>
		public const int SCROLL_MULT_TIER_4 = 60;

		/// <summary>Scroll cost multiplier tier 5 (highest level)</summary>
		public const int SCROLL_MULT_TIER_5 = 80;

		#endregion

		#region Repair

		/// <summary>Maximum hit points reduction minimum</summary>
		public const int REPAIR_HP_REDUCTION_MIN = 5;

		/// <summary>Maximum hit points reduction maximum</summary>
		public const int REPAIR_HP_REDUCTION_MAX = 10;

		/// <summary>Maximum hit points threshold for reduction</summary>
		public const int REPAIR_HP_THRESHOLD = 10;

		/// <summary>Maximum hit points minimum reduction</summary>
		public const int REPAIR_HP_MIN_REDUCTION = 1;

		#endregion

		#region Movement and Talk

		/// <summary>Talk range in tiles</summary>
		public const int TALK_RANGE = 30;

		/// <summary>Talk delay minimum in seconds</summary>
		public const int TALK_DELAY_MIN = 15;

		/// <summary>Talk delay maximum in seconds</summary>
		public const int TALK_DELAY_MAX = 45;

		/// <summary>Citizen position modifier (no mount)</summary>
		public const int CITIZEN_POS_MOD_NO_MOUNT = 2;

		/// <summary>Citizen position modifier (with mount)</summary>
		public const int CITIZEN_POS_MOD_MOUNT = 3;

		#endregion

		#region Gump

		/// <summary>Gump X position</summary>
		public const int GUMP_X = 25;

		/// <summary>Gump Y position</summary>
		public const int GUMP_Y = 25;

		/// <summary>Gump image ID 1</summary>
		public const int GUMP_IMAGE_1 = 153;

		/// <summary>Gump image ID 2</summary>
		public const int GUMP_IMAGE_2 = 163;

		/// <summary>Gump image ID 3</summary>
		public const int GUMP_IMAGE_3 = 145;

		/// <summary>Gump image ID 4</summary>
		public const int GUMP_IMAGE_4 = 140;

		/// <summary>Gump image ID 5</summary>
		public const int GUMP_IMAGE_5 = 143;

		/// <summary>Gump HTML X position</summary>
		public const int GUMP_HTML_X = 177;

		/// <summary>Gump HTML Y position</summary>
		public const int GUMP_HTML_Y = 45;

		/// <summary>Gump HTML width</summary>
		public const int GUMP_HTML_WIDTH = 371;

		/// <summary>Gump HTML height</summary>
		public const int GUMP_HTML_HEIGHT = 204;

		/// <summary>Gump HTML color</summary>
		public const int GUMP_HTML_COLOR = 0xFFA200;

		/// <summary>Context menu ID for speech</summary>
		public const int CONTEXT_MENU_ID = 6146;

		/// <summary>Context menu range</summary>
		public const int CONTEXT_MENU_RANGE = 3;

		#endregion

		#region Sounds

		/// <summary>Sound ID: Trade success</summary>
		public const int SOUND_TRADE_SUCCESS = 0x2E6;

		/// <summary>Sound ID: Repair</summary>
		public const int SOUND_REPAIR = 0x541;

		/// <summary>Sound ID: Unlock</summary>
		public const int SOUND_UNLOCK = 0x241;

		/// <summary>Sound ID: Leather repair</summary>
		public const int SOUND_LEATHER_REPAIR = 0x248;

		/// <summary>Sound ID: Wood repair</summary>
		public const int SOUND_WOOD_REPAIR = 0x23D;

		/// <summary>Sound ID: Wand charge</summary>
		public const int SOUND_WAND_CHARGE = 0x5C1;

		/// <summary>Sound ID: Speech</summary>
		public const int SOUND_SPEECH = 0x5B6;

		/// <summary>Sound ID: Death</summary>
		public const int SOUND_DEATH = 0x202;

		/// <summary>Sound ID: Spawn</summary>
		public const int SOUND_SPAWN = 0x1FE;

		#endregion

		#region Effects

		/// <summary>Particle effect ID</summary>
		public const int EFFECT_PARTICLE = 0x376A;

		/// <summary>Particle effect speed</summary>
		public const int EFFECT_SPEED = 9;

		/// <summary>Particle effect duration</summary>
		public const int EFFECT_DURATION = 32;

		/// <summary>Particle effect item ID</summary>
		public const int EFFECT_ITEM_ID = 5030;

		#endregion

		#region Mount Selection

		/// <summary>Mount random range maximum</summary>
		public const int MOUNT_RANDOM_MAX = 30;

		/// <summary>Mount bear selection range maximum</summary>
		public const int MOUNT_BEAR_MAX = 10;

		/// <summary>Mount reptile selection range maximum</summary>
		public const int MOUNT_REPTILE_MAX = 4;

		/// <summary>Mount wolf selection range maximum</summary>
		public const int MOUNT_WOLF_MAX = 4;

		/// <summary>Mount cat selection range maximum</summary>
		public const int MOUNT_CAT_MAX = 6;

		/// <summary>Mount ostard selection range maximum</summary>
		public const int MOUNT_OSTARD_MAX = 4;

		/// <summary>Mount bird selection range maximum</summary>
		public const int MOUNT_BIRD_MAX = 5;

		/// <summary>Mount drake selection range maximum</summary>
		public const int MOUNT_DRAKE_MAX = 4;

		/// <summary>Mount beetle selection range maximum</summary>
		public const int MOUNT_BEETLE_MAX = 6;

		/// <summary>Mount raptor selection range maximum</summary>
		public const int MOUNT_RAPTOR_MAX = 5;

		/// <summary>Mount steed selection range maximum</summary>
		public const int MOUNT_STEED_MAX = 8;

		/// <summary>Mount exotic selection range maximum</summary>
		public const int MOUNT_EXOTIC_MAX = 6;

		/// <summary>Special horse chance denominator</summary>
		public const int MOUNT_SPECIAL_HORSE_CHANCE = 50;

		/// <summary>Special horse chance threshold</summary>
		public const int MOUNT_SPECIAL_HORSE_THRESHOLD = 1;

		/// <summary>Special horse body ID</summary>
		public const int MOUNT_SPECIAL_HORSE_BODY = 587;

		/// <summary>Special horse hue selection range maximum</summary>
		public const int MOUNT_SPECIAL_HORSE_HUE_MAX = 16;

		/// <summary>Dragon body ID option 1</summary>
		public const int MOUNT_DRAGON_BODY_1 = 59;

		/// <summary>Dragon body ID option 2</summary>
		public const int MOUNT_DRAGON_BODY_2 = 61;

		/// <summary>Gem dragon item ID minimum</summary>
		public const int MOUNT_GEM_DRAGON_ITEM_MIN = 595;

		/// <summary>Gem dragon item ID maximum</summary>
		public const int MOUNT_GEM_DRAGON_ITEM_MAX = 596;

		/// <summary>Raptor body variant 1</summary>
		public const int MOUNT_RAPTOR_BODY_1 = 116;

		/// <summary>Raptor body variant 2</summary>
		public const int MOUNT_RAPTOR_BODY_2 = 117;

		/// <summary>Raptor body variant 3</summary>
		public const int MOUNT_RAPTOR_BODY_3 = 219;

		/// <summary>Instrument quality chance denominator</summary>
		public const int INSTRUMENT_QUALITY_CHANCE = 4;

		/// <summary>Instrument quality chance threshold</summary>
		public const int INSTRUMENT_QUALITY_THRESHOLD = 1;

		/// <summary>Instrument slayer chance denominator</summary>
		public const int INSTRUMENT_SLAYER_CHANCE = 4;

		/// <summary>Instrument slayer chance threshold</summary>
		public const int INSTRUMENT_SLAYER_THRESHOLD = 1;

		/// <summary>Instrument random hue chance denominator</summary>
		public const int INSTRUMENT_HUE_CHANCE = 4;

		/// <summary>Instrument random hue chance threshold</summary>
		public const int INSTRUMENT_HUE_THRESHOLD = 1;

		#endregion

		#region Instrument Resource Uses

		/// <summary>Instrument uses: Ash</summary>
		public const int INSTRUMENT_USES_ASH = 20;

		/// <summary>Instrument uses: Ebony</summary>
		public const int INSTRUMENT_USES_EBONY = 60;

		/// <summary>Instrument uses: Elven</summary>
		public const int INSTRUMENT_USES_ELVEN = 80;

		/// <summary>Instrument uses: Cherry</summary>
		public const int INSTRUMENT_USES_CHERRY = 100;

		/// <summary>Instrument uses: Rosewood</summary>
		public const int INSTRUMENT_USES_ROSEWOOD = 120;

		/// <summary>Instrument uses: Golden Oak</summary>
		public const int INSTRUMENT_USES_GOLDEN_OAK = 140;

		/// <summary>Instrument uses: Hickory</summary>
		public const int INSTRUMENT_USES_HICKORY = 180;

		#endregion

		#region Potion Costs

		/// <summary>Potion cost: Nightsight</summary>
		public const int POTION_COST_NIGHTSIGHT = 15;

		/// <summary>Potion cost: Lesser Cure</summary>
		public const int POTION_COST_LESSER_CURE = 15;

		/// <summary>Potion cost: Cure</summary>
		public const int POTION_COST_CURE = 30;

		/// <summary>Potion cost: Greater Cure</summary>
		public const int POTION_COST_GREATER_CURE = 60;

		/// <summary>Potion cost: Agility</summary>
		public const int POTION_COST_AGILITY = 15;

		/// <summary>Potion cost: Greater Agility</summary>
		public const int POTION_COST_GREATER_AGILITY = 60;

		/// <summary>Potion cost: Strength</summary>
		public const int POTION_COST_STRENGTH = 15;

		/// <summary>Potion cost: Greater Strength</summary>
		public const int POTION_COST_GREATER_STRENGTH = 60;

		/// <summary>Potion cost: Lesser Poison</summary>
		public const int POTION_COST_LESSER_POISON = 15;

		/// <summary>Potion cost: Poison</summary>
		public const int POTION_COST_POISON = 30;

		/// <summary>Potion cost: Greater Poison</summary>
		public const int POTION_COST_GREATER_POISON = 60;

		/// <summary>Potion cost: Deadly Poison</summary>
		public const int POTION_COST_DEADLY_POISON = 90;

		/// <summary>Potion cost: Lethal Poison</summary>
		public const int POTION_COST_LETHAL_POISON = 120;

		/// <summary>Potion cost: Refresh</summary>
		public const int POTION_COST_REFRESH = 15;

		/// <summary>Potion cost: Total Refresh</summary>
		public const int POTION_COST_TOTAL_REFRESH = 30;

		/// <summary>Potion cost: Lesser Heal</summary>
		public const int POTION_COST_LESSER_HEAL = 15;

		/// <summary>Potion cost: Heal</summary>
		public const int POTION_COST_HEAL = 30;

		/// <summary>Potion cost: Greater Heal</summary>
		public const int POTION_COST_GREATER_HEAL = 60;

		/// <summary>Potion cost: Lesser Explosion</summary>
		public const int POTION_COST_LESSER_EXPLOSION = 15;

		/// <summary>Potion cost: Explosion</summary>
		public const int POTION_COST_EXPLOSION = 30;

		/// <summary>Potion cost: Greater Explosion</summary>
		public const int POTION_COST_GREATER_EXPLOSION = 60;

		/// <summary>Potion cost: Lesser Invisibility</summary>
		public const int POTION_COST_LESSER_INVISIBILITY = 15;

		/// <summary>Potion cost: Invisibility</summary>
		public const int POTION_COST_INVISIBILITY = 30;

		/// <summary>Potion cost: Greater Invisibility</summary>
		public const int POTION_COST_GREATER_INVISIBILITY = 60;

		/// <summary>Potion cost: Lesser Rejuvenate</summary>
		public const int POTION_COST_LESSER_REJUVENATE = 15;

		/// <summary>Potion cost: Rejuvenate</summary>
		public const int POTION_COST_REJUVENATE = 30;

		/// <summary>Potion cost: Greater Rejuvenate</summary>
		public const int POTION_COST_GREATER_REJUVENATE = 60;

		/// <summary>Potion cost: Lesser Mana</summary>
		public const int POTION_COST_LESSER_MANA = 15;

		/// <summary>Potion cost: Mana</summary>
		public const int POTION_COST_MANA = 30;

		/// <summary>Potion cost: Greater Mana</summary>
		public const int POTION_COST_GREATER_MANA = 60;

		/// <summary>Potion cost: Conflagration</summary>
		public const int POTION_COST_CONFLAGRATION = 30;

		/// <summary>Potion cost: Greater Conflagration</summary>
		public const int POTION_COST_GREATER_CONFLAGRATION = 60;

		/// <summary>Potion cost: Confusion Blast</summary>
		public const int POTION_COST_CONFUSION_BLAST = 30;

		/// <summary>Potion cost: Greater Confusion Blast</summary>
		public const int POTION_COST_GREATER_CONFUSION_BLAST = 60;

		/// <summary>Potion cost: Frostbite</summary>
		public const int POTION_COST_FROSTBITE = 30;

		/// <summary>Potion cost: Greater Frostbite</summary>
		public const int POTION_COST_GREATER_FROSTBITE = 60;

		/// <summary>Potion cost: Acid</summary>
		public const int POTION_COST_ACID = 60;

		#endregion

		#region Location Checks

		/// <summary>Range for infected region check</summary>
		public const int INFECTED_REGION_CHECK_RANGE = 20;

		/// <summary>Home position tolerance X/Y</summary>
		public const int HOME_POSITION_TOLERANCE = 2;

		#endregion

		#region Mount Constants

		/// <summary>Dragon body ID option 1</summary>
		public const int DRAGON_BODY_ID_1 = 59;

		/// <summary>Dragon body ID option 2</summary>
		public const int DRAGON_BODY_ID_2 = 61;

		/// <summary>Special horse chance denominator</summary>
		public const int SPECIAL_HORSE_CHANCE = 50;

		/// <summary>Special horse body ID</summary>
		public const int SPECIAL_HORSE_BODY_ID = 587;

		/// <summary>Special horse item ID</summary>
		public const int SPECIAL_HORSE_ITEM_ID = 587;

		/// <summary>Special horse hue count</summary>
		public const int SPECIAL_HORSE_HUE_COUNT = 16;

		/// <summary>Gem dragon item ID min</summary>
		public const int GEM_DRAGON_ITEM_ID_1 = 595;

		/// <summary>Gem dragon item ID max</summary>
		public const int GEM_DRAGON_ITEM_ID_2 = 596;

		/// <summary>Raptor body ID option 1</summary>
		public const int RAPTOR_BODY_ID_1 = 116;

		/// <summary>Raptor body ID option 2</summary>
		public const int RAPTOR_BODY_ID_2 = 117;

		/// <summary>Raptor body ID option 3</summary>
		public const int RAPTOR_BODY_ID_3 = 219;

		#endregion

		#region Citizen Creation Constants

		/// <summary>Control slots for first citizen</summary>
		public const int CONTROL_SLOTS_CITIZEN_1 = 2;

		/// <summary>Control slots for second citizen</summary>
		public const int CONTROL_SLOTS_CITIZEN_2 = 3;

		/// <summary>Control slots for third citizen</summary>
		public const int CONTROL_SLOTS_CITIZEN_3 = 4;

		/// <summary>Control slots for fourth citizen</summary>
		public const int CONTROL_SLOTS_CITIZEN_4 = 5;

		#endregion
	}
}


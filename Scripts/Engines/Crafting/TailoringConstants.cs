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

		/// <summary>Gump title: "MENU DE ALFAIATARIA" (cliloc 1044005)</summary>
		public const int MSG_GUMP_TITLE = 1044005;

		/// <summary>Message: "Você quebrou sua ferramenta!"</summary>
		public const string MSG_TOOL_WORN_OUT = TailoringStringConstants.MSG_TOOL_WORN_OUT;

		/// <summary>Message: "A ferramenta deve estar com você para usar."</summary>
		public const string MSG_TOOL_MUST_BE_ON_PERSON = TailoringStringConstants.MSG_TOOL_MUST_BE_ON_PERSON;

		/// <summary>Message: "Você falhou ao criar o item, e alguns de seus materiais foram perdidos."</summary>
		public const string MSG_FAILED_LOST_MATERIALS = TailoringStringConstants.MSG_FAILED_LOST_MATERIALS;

		/// <summary>Message: "Você falhou ao criar o item, mas nenhum material foi perdido."</summary>
		public const string MSG_FAILED_NO_MATERIALS_LOST = TailoringStringConstants.MSG_FAILED_NO_MATERIALS_LOST;

		/// <summary>Message: "Você mal conseguiu fazer este item. Sua qualidade está abaixo da média."</summary>
		public const string MSG_BARELY_MADE_ITEM = TailoringStringConstants.MSG_BARELY_MADE_ITEM;

		/// <summary>Message: "Você cria um item de qualidade excepcional e assina a sua marca nele."</summary>
		public const string MSG_EXCEPTIONAL_WITH_MARK = TailoringStringConstants.MSG_EXCEPTIONAL_WITH_MARK;

		/// <summary>Message: "Você cria um item de qualidade excepcional."</summary>
		public const string MSG_EXCEPTIONAL_QUALITY = TailoringStringConstants.MSG_EXCEPTIONAL_QUALITY;

		/// <summary>Message: "Você cria o item."</summary>
		public const string MSG_ITEM_CREATED = TailoringStringConstants.MSG_ITEM_CREATED;

		/// <summary>Resource name: "Tecido de Algodão"</summary>
		public const string MSG_COTTON_CLOTH = TailoringStringConstants.RESOURCE_COTTON_CLOTH;

		/// <summary>Error: "Você não tem tecido suficiente."</summary>
		public const string MSG_INSUFFICIENT_CLOTH = TailoringStringConstants.MSG_INSUFFICIENT_CLOTH;

		/// <summary>Resource name: "Couro"</summary>
		public const string MSG_LEATHER = TailoringStringConstants.RESOURCE_LEATHER;

		/// <summary>Error: "Você não tem couro suficiente."</summary>
		public const string MSG_INSUFFICIENT_LEATHER = TailoringStringConstants.MSG_INSUFFICIENT_LEATHER;

		/// <summary>Error: "Você não tem os itens necessários."</summary>
		public const string MSG_INSUFFICIENT_RESOURCES = TailoringStringConstants.MSG_INSUFFICIENT_RESOURCES;

		/// <summary>Error: "Você não tem a habilidade necessária."</summary>
		public const string MSG_INSUFFICIENT_SKILL = TailoringStringConstants.MSG_INSUFFICIENT_SKILL;

		/// <summary>Message: "Você não tem material de tecido suficiente."</summary>
		public const string MSG_NO_CLOTH_MATERIAL = TailoringStringConstants.MSG_NO_CLOTH_MATERIAL;

		/// <summary>Message: "Você não pode trabalhar este tecido."</summary>
		public const string MSG_CANNOT_WORK_CLOTH = TailoringStringConstants.MSG_CANNOT_WORK_CLOTH;

		/// <summary>Message: "Couro Golias"</summary>
		public const string MSG_GOLIATH_LEATHER = TailoringStringConstants.RESOURCE_GOLIATH_LEATHER;

		/// <summary>Error: "Você não tem couro golias suficiente."</summary>
		public const string MSG_INSUFFICIENT_GOLIATH_LEATHER = TailoringStringConstants.MSG_INSUFFICIENT_GOLIATH_LEATHER;

		#endregion

		#region Quality Levels

		/// <summary>Quality level: Below average (0)</summary>
		public const int QUALITY_BELOW_AVERAGE = 0;

		/// <summary>Quality level: Exceptional (2)</summary>
		public const int QUALITY_EXCEPTIONAL = 2;

		#endregion

		#region Cloth Types (PT-BR)

		/// <summary>Cotton Cloth</summary>
		public const string MSG_CLOTH_COTTON = TailoringStringConstants.CLOTH_COTTON;

		/// <summary>Wool Cloth</summary>
		public const string MSG_CLOTH_WOOL = TailoringStringConstants.CLOTH_WOOL;

		/// <summary>Flax Cloth</summary>
		public const string MSG_CLOTH_FLAX = TailoringStringConstants.CLOTH_FLAX;

		/// <summary>Silk Cloth</summary>
		public const string MSG_CLOTH_SILK = TailoringStringConstants.CLOTH_SILK;

		/// <summary>Poliester Cloth</summary>
		public const string MSG_CLOTH_POLIESTER = TailoringStringConstants.CLOTH_POLIESTER;

		#endregion

		#region Leather Types (PT-BR)

		/// <summary>Leather</summary>
		public const string MSG_LEATHER_REGULAR = TailoringStringConstants.LEATHER_REGULAR;

		/// <summary>Spined Leather</summary>
		public const string MSG_LEATHER_SPINED = TailoringStringConstants.LEATHER_SPINED;

		/// <summary>Horned Leather</summary>
		public const string MSG_LEATHER_HORNED = TailoringStringConstants.LEATHER_HORNED;

		/// <summary>Barbed Leather</summary>
		public const string MSG_LEATHER_BARBED = TailoringStringConstants.LEATHER_BARBED;

		/// <summary>Volcanic Leather</summary>
		public const string MSG_LEATHER_VOLCANIC = TailoringStringConstants.LEATHER_VOLCANIC;

		#endregion

		#region Item Names (PT-BR) - Hats

		/// <summary>Skull Cap</summary>
		public const string MSG_SKULL_CAP = TailoringStringConstants.ITEM_SKULL_CAP;

		/// <summary>Bandana</summary>
		public const string MSG_BANDANA = TailoringStringConstants.ITEM_BANDANA;

		/// <summary>Floppy Hat</summary>
		public const string MSG_FLOPPY_HAT = TailoringStringConstants.ITEM_FLOPPY_HAT;

		/// <summary>Cap</summary>
		public const string MSG_CAP = TailoringStringConstants.ITEM_CAP;

		/// <summary>Wide Brim Hat</summary>
		public const string MSG_WIDE_BRIM_HAT = TailoringStringConstants.ITEM_WIDE_BRIM_HAT;

		/// <summary>Straw Hat</summary>
		public const string MSG_STRAW_HAT = TailoringStringConstants.ITEM_STRAW_HAT;

		/// <summary>Tall Straw Hat</summary>
		public const string MSG_TALL_STRAW_HAT = TailoringStringConstants.ITEM_TALL_STRAW_HAT;

		/// <summary>Bonnet</summary>
		public const string MSG_BONNET = TailoringStringConstants.ITEM_BONNET;

		/// <summary>Feathered Hat</summary>
		public const string MSG_FEATHERED_HAT = TailoringStringConstants.ITEM_FEATHERED_HAT;

		/// <summary>Tricorne Hat</summary>
		public const string MSG_TRICORNE_HAT = TailoringStringConstants.ITEM_TRICORNE_HAT;

		/// <summary>Jester Hat</summary>
		public const string MSG_JESTER_HAT = TailoringStringConstants.ITEM_JESTER_HAT;

		/// <summary>Wizard's Hat</summary>
		public const string MSG_WIZARDS_HAT = TailoringStringConstants.ITEM_WIZARDS_HAT;

		#endregion

		#region Item Names (PT-BR) - Shirts/Robes

		/// <summary>Doublet</summary>
		public const string MSG_DOUBLET = TailoringStringConstants.ITEM_DOUBLET;

		/// <summary>Shirt</summary>
		public const string MSG_SHIRT = TailoringStringConstants.ITEM_SHIRT;

		/// <summary>Tunic</summary>
		public const string MSG_TUNIC = TailoringStringConstants.ITEM_TUNIC;

		/// <summary>Surcoat</summary>
		public const string MSG_SURCOAT = TailoringStringConstants.ITEM_SURCOAT;

		/// <summary>Plain Dress</summary>
		public const string MSG_PLAIN_DRESS = TailoringStringConstants.ITEM_PLAIN_DRESS;

		/// <summary>Fancy Dress</summary>
		public const string MSG_FANCY_DRESS = TailoringStringConstants.ITEM_FANCY_DRESS;

		/// <summary>Cloak</summary>
		public const string MSG_CLOAK = TailoringStringConstants.ITEM_CLOAK;

		/// <summary>Robe</summary>
		public const string MSG_ROBE = TailoringStringConstants.ITEM_ROBE;

		/// <summary>Fancy Shirt</summary>
		public const string MSG_FANCY_SHIRT = TailoringStringConstants.ITEM_FANCY_SHIRT;

		#endregion

		#region Item Names (PT-BR) - Pants

		/// <summary>Short Pants</summary>
		public const string MSG_SHORT_PANTS = TailoringStringConstants.ITEM_SHORT_PANTS;

		/// <summary>Long Pants</summary>
		public const string MSG_LONG_PANTS = TailoringStringConstants.ITEM_LONG_PANTS;

		/// <summary>Kilt</summary>
		public const string MSG_KILT = TailoringStringConstants.ITEM_KILT;

		/// <summary>Skirt</summary>
		public const string MSG_SKIRT = TailoringStringConstants.ITEM_SKIRT;

		#endregion

		#region Item Names (PT-BR) - Footwear

		/// <summary>Sandals</summary>
		public const string MSG_SANDALS = TailoringStringConstants.ITEM_SANDALS;

		/// <summary>Shoes</summary>
		public const string MSG_SHOES = TailoringStringConstants.ITEM_SHOES;

		/// <summary>Boots</summary>
		public const string MSG_BOOTS = TailoringStringConstants.ITEM_BOOTS;

		#endregion

		#region Item Names (PT-BR) - Misc

		/// <summary>Body Sash</summary>
		public const string MSG_BODY_SASH = TailoringStringConstants.ITEM_BODY_SASH;

		/// <summary>Half Apron</summary>
		public const string MSG_HALF_APRON = TailoringStringConstants.ITEM_HALF_APRON;

		/// <summary>Full Apron</summary>
		public const string MSG_FULL_APRON = TailoringStringConstants.ITEM_FULL_APRON;

		#endregion

		#region Item Names (PT-BR) - Leather Armor

		/// <summary>Leather Cap</summary>
		public const string MSG_LEATHER_CAP = TailoringStringConstants.ITEM_LEATHER_CAP;

		/// <summary>Leather Gorget</summary>
		public const string MSG_LEATHER_GORGET = TailoringStringConstants.ITEM_LEATHER_GORGET;

		/// <summary>Leather Gloves</summary>
		public const string MSG_LEATHER_GLOVES = TailoringStringConstants.ITEM_LEATHER_GLOVES;

		/// <summary>Leather Arms</summary>
		public const string MSG_LEATHER_ARMS = TailoringStringConstants.ITEM_LEATHER_ARMS;

		/// <summary>Leather Legs</summary>
		public const string MSG_LEATHER_LEGS = TailoringStringConstants.ITEM_LEATHER_LEGS;

		/// <summary>Leather Chest</summary>
		public const string MSG_LEATHER_CHEST = TailoringStringConstants.ITEM_LEATHER_CHEST;

		/// <summary>Leather Skirt</summary>
		public const string MSG_LEATHER_SKIRT = TailoringStringConstants.ITEM_LEATHER_SKIRT;

		/// <summary>Leather Shorts</summary>
		public const string MSG_LEATHER_SHORTS = TailoringStringConstants.ITEM_LEATHER_SHORTS;

		/// <summary>Leather Bustier Arms</summary>
		public const string MSG_LEATHER_BUSTIER_ARMS = TailoringStringConstants.ITEM_LEATHER_BUSTIER_ARMS;

		/// <summary>Female Leather Chest</summary>
		public const string MSG_FEMALE_LEATHER_CHEST = TailoringStringConstants.ITEM_FEMALE_LEATHER_CHEST;

		/// <summary>Studded Gorget</summary>
		public const string MSG_STUDDED_GORGET = TailoringStringConstants.ITEM_STUDDED_GORGET;

		/// <summary>Studded Gloves</summary>
		public const string MSG_STUDDED_GLOVES = TailoringStringConstants.ITEM_STUDDED_GLOVES;

		/// <summary>Studded Arms</summary>
		public const string MSG_STUDDED_ARMS = TailoringStringConstants.ITEM_STUDDED_ARMS;

		/// <summary>Studded Legs</summary>
		public const string MSG_STUDDED_LEGS = TailoringStringConstants.ITEM_STUDDED_LEGS;

		/// <summary>Studded Chest</summary>
		public const string MSG_STUDDED_CHEST = TailoringStringConstants.ITEM_STUDDED_CHEST;

		/// <summary>Studded Bustier Arms</summary>
		public const string MSG_STUDDED_BUSTIER_ARMS = TailoringStringConstants.ITEM_STUDDED_BUSTIER_ARMS;

		/// <summary>Female Studded Chest</summary>
		public const string MSG_FEMALE_STUDDED_CHEST = TailoringStringConstants.ITEM_FEMALE_STUDDED_CHEST;

		#endregion

		#region Item Names (PT-BR) - Bone Armor

		/// <summary>Bone Helm</summary>
		public const string MSG_BONE_HELM = TailoringStringConstants.ITEM_BONE_HELM;

		/// <summary>Bone Gloves</summary>
		public const string MSG_BONE_GLOVES = TailoringStringConstants.ITEM_BONE_GLOVES;

		/// <summary>Bone Arms</summary>
		public const string MSG_BONE_ARMS = TailoringStringConstants.ITEM_BONE_ARMS;

		/// <summary>Bone Legs</summary>
		public const string MSG_BONE_LEGS = TailoringStringConstants.ITEM_BONE_LEGS;

		/// <summary>Bone Chest</summary>
		public const string MSG_BONE_CHEST = TailoringStringConstants.ITEM_BONE_CHEST;

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

		/// <summary>Skill required for volcanic leather</summary>
		public const double SKILL_REQ_LEATHER_VOLCANIC = 82.0;

		/// <summary>Skill required for goliath leather</summary>
		public const double SKILL_REQ_LEATHER_GOLIATH = 85.0;

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
		public const double MAGERY_SKILL_MIN_MINERS_POUCH = 100.0;

		/// <summary>Magery skill maximum for miners pouch</summary>
		public const double MAGERY_SKILL_MAX_MINERS_POUCH = 100.0;

		/// <summary>Magery skill minimum for lumberjack pouch</summary>
		public const double MAGERY_SKILL_MIN_LUMBERJACK_POUCH = 100.0;

		/// <summary>Magery skill maximum for lumberjack pouch</summary>
		public const double MAGERY_SKILL_MAX_LUMBERJACK_POUCH = 100.0;

		/// <summary>Magery skill minimum for alchemy pouch</summary>
		public const double MAGERY_SKILL_MIN_ALCHEMY_POUCH = 100.0;

		/// <summary>Magery skill maximum for alchemy pouch</summary>
		public const double MAGERY_SKILL_MAX_ALCHEMY_POUCH = 100.0;

		/// <summary>Magery skill minimum for tailoring pouch</summary>
		public const double MAGERY_SKILL_MIN_TAILORING_POUCH = 100.0;

		/// <summary>Magery skill maximum for tailoring pouch</summary>
		public const double MAGERY_SKILL_MAX_TAILORING_POUCH = 100.0;

		#endregion

		#region Rucksack Crafting Success Chances

		/// <summary>Minimum success chance for rucksacks at 100 tailoring (30%)</summary>
		public const double RUCKSACK_CHANCE_AT_MIN = 0.3;

		/// <summary>Maximum success chance for rucksacks at 120 tailoring (50%)</summary>
		public const double RUCKSACK_CHANCE_AT_MAX = 0.5;

		/// <summary>Tailoring skill minimum for rucksacks</summary>
		public const double RUCKSACK_TAILORING_MIN = 100.0;

		/// <summary>Tailoring skill maximum for rucksacks</summary>
		public const double RUCKSACK_TAILORING_MAX = 120.0;

		/// <summary>Goliath leather amount required for rucksacks</summary>
		public const int RUCKSACK_GOLIATH_LEATHER_AMOUNT = 50;

		#endregion
	}
}

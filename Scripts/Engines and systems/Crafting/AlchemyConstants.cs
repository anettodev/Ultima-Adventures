namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for Alchemy crafting system calculations and mechanics.
	/// Extracted from DefAlchemy.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class AlchemyConstants
	{
		#region Craft System Configuration

		/// <summary>Minimum chance at minimum skill (0%)</summary>
		public const double CHANCE_AT_MIN_SKILL = 0.0;

		/// <summary>Minimum craft effect value</summary>
		public const int MIN_CRAFT_EFFECT = 1;

		/// <summary>Maximum craft effect value</summary>
		public const int MAX_CRAFT_EFFECT = 1;

		/// <summary>Craft delay multiplier</summary>
		public const double CRAFT_DELAY_MULTIPLIER = 1.25;

		/// <summary>Sound ID for alchemy craft effect (0x242)</summary>
		public const int SOUND_ALCHEMY_CRAFT = 0x242;

		/// <summary>Sound ID for bottle filling (0x240)</summary>
		public const int SOUND_BOTTLE_FILLING = 0x240;

		#endregion

		#region Message Numbers (Cliloc)

		/// <summary>Gump title: "MENU DE ALQUIMIA" (cliloc 1044006)</summary>
		public const int MSG_GUMP_TITLE = 1044001;

		/// <summary>Message: "Você quebrou sua ferramenta!"</summary>
		public const string MSG_TOOL_WORN_OUT = AlchemyStringConstants.MSG_TOOL_WORN_OUT;

		/// <summary>Message: "A ferramenta deve estar com você para usar."</summary>
		public const string MSG_TOOL_MUST_BE_ON_PERSON = AlchemyStringConstants.MSG_TOOL_MUST_BE_ON_PERSON;

		/// <summary>Message: "Você falhou ao criar o item, e alguns de seus materiais foram perdidos."</summary>
		public const string MSG_FAILED_LOST_MATERIALS = AlchemyStringConstants.MSG_FAILED_LOST_MATERIALS;

		/// <summary>Message: "Você falhou ao criar uma poção útil."</summary>
		public const string MSG_FAILED_POTION = AlchemyStringConstants.MSG_FAILED_POTION;

		/// <summary>Message: "Você cria o item."</summary>
		public const string MSG_ITEM_CREATED = AlchemyStringConstants.MSG_ITEM_CREATED;

		/// <summary>Message: "Você cria a poção e a despeja em um barril."</summary>
		public const string MSG_POTION_TO_KEG = AlchemyStringConstants.MSG_POTION_TO_KEG;

		/// <summary>Message: "Você despeja a poção em uma garrafa..."</summary>
		public const string MSG_POTION_TO_BOTTLE = AlchemyStringConstants.MSG_POTION_TO_BOTTLE;

		/// <summary>Resource name: "Garrafa"</summary>
		public const string MSG_BOTTLE = AlchemyStringConstants.RESOURCE_BOTTLE;

		/// <summary>Error: "Você não tem garrafas suficientes."</summary>
		public const string MSG_INSUFFICIENT_BOTTLES = AlchemyStringConstants.MSG_INSUFFICIENT_BOTTLES;

		/// <summary>Resource name: "Musgo Sanguíneo"</summary>
		public const string MSG_BLOODMOSS = AlchemyStringConstants.RESOURCE_BLOODMOSS;

		/// <summary>Error: "Você não tem musgo sanguíneo suficiente."</summary>
		public const string MSG_INSUFFICIENT_BLOODMOSS = AlchemyStringConstants.MSG_INSUFFICIENT_BLOODMOSS;

		/// <summary>Resource name: "Alho"</summary>
		public const string MSG_GARLIC = AlchemyStringConstants.RESOURCE_GARLIC;

		/// <summary>Error: "Você não tem alho suficiente."</summary>
		public const string MSG_INSUFFICIENT_GARLIC = AlchemyStringConstants.MSG_INSUFFICIENT_GARLIC;

		/// <summary>Resource name: "Ginseng"</summary>
		public const string MSG_GINSENG = AlchemyStringConstants.RESOURCE_GINSENG;

		/// <summary>Error: "Você não tem ginseng suficiente."</summary>
		public const string MSG_INSUFFICIENT_GINSENG = AlchemyStringConstants.MSG_INSUFFICIENT_GINSENG;

		/// <summary>Resource name: "Raiz de Mandrágora"</summary>
		public const string MSG_MANDRAKE_ROOT = AlchemyStringConstants.RESOURCE_MANDRAKE_ROOT;

		/// <summary>Error: "Você não tem raiz de mandrágora suficiente."</summary>
		public const string MSG_INSUFFICIENT_MANDRAKE_ROOT = AlchemyStringConstants.MSG_INSUFFICIENT_MANDRAKE_ROOT;

		/// <summary>Resource name: "Meimendro"</summary>
		public const string MSG_NIGHTSHADE = AlchemyStringConstants.RESOURCE_NIGHTSHADE;

		/// <summary>Error: "Você não tem meimendro suficiente."</summary>
		public const string MSG_INSUFFICIENT_NIGHTSHADE = AlchemyStringConstants.MSG_INSUFFICIENT_NIGHTSHADE;

		/// <summary>Resource name: "Cinza Sulfurosa"</summary>
		public const string MSG_SULFUROUS_ASH = AlchemyStringConstants.RESOURCE_SULFUROUS_ASH;

		/// <summary>Error: "Você não tem cinza sulfurosa suficiente."</summary>
		public const string MSG_INSUFFICIENT_SULFUROUS_ASH = AlchemyStringConstants.MSG_INSUFFICIENT_SULFUROUS_ASH;

		/// <summary>Resource name: "Seda de Aranha"</summary>
		public const string MSG_SPIDERS_SILK = AlchemyStringConstants.RESOURCE_SPIDERS_SILK;

		/// <summary>Error: "Você não tem seda de aranha suficiente."</summary>
		public const string MSG_INSUFFICIENT_SPIDERS_SILK = AlchemyStringConstants.MSG_INSUFFICIENT_SPIDERS_SILK;

		/// <summary>Resource name: "Pérola Negra"</summary>
		public const string MSG_BLACK_PEARL = AlchemyStringConstants.RESOURCE_BLACK_PEARL;

		/// <summary>Error: "Você não tem pérola negra suficiente."</summary>
		public const string MSG_INSUFFICIENT_BLACK_PEARL = AlchemyStringConstants.MSG_INSUFFICIENT_BLACK_PEARL;

		/// <summary>Resource name: "Cristal Nox"</summary>
		public const string MSG_NOX_CRYSTAL = AlchemyStringConstants.RESOURCE_NOX_CRYSTAL;

		/// <summary>Error: "Você não tem cristal nox suficiente."</summary>
		public const string MSG_INSUFFICIENT_NOX_CRYSTAL = AlchemyStringConstants.MSG_INSUFFICIENT_NOX_CRYSTAL;

		/// <summary>Error: "Você não tem os itens necessários."</summary>
		public const string MSG_INSUFFICIENT_RESOURCES = AlchemyStringConstants.MSG_INSUFFICIENT_RESOURCES;

		#endregion

		#region Quality Levels

		/// <summary>Quality level: Keg (-1)</summary>
		public const int QUALITY_KEG = -1;

		#endregion

		#region Poisoning Skill Training

		/// <summary>Minimum poisoning skill for training</summary>
		public const double POISONING_SKILL_MIN = 0.0;

		/// <summary>Maximum poisoning skill for lesser poison potion</summary>
		public const double POISONING_SKILL_LESSER = 40.0;

		/// <summary>Maximum poisoning skill for regular poison potion</summary>
		public const double POISONING_SKILL_REGULAR = 60.0;

		/// <summary>Maximum poisoning skill for greater poison potion</summary>
		public const double POISONING_SKILL_GREATER = 80.0;

		/// <summary>Maximum poisoning skill for deadly poison potion</summary>
		public const double POISONING_SKILL_DEADLY = 100.0;

		/// <summary>Maximum poisoning skill for lethal poison potion</summary>
		public const double POISONING_SKILL_LETHAL = 110.0;

		#endregion

		#region Resource Amounts

		/// <summary>Bottle amount for potions</summary>
		public const int RESOURCE_BOTTLES_POTION = 1;

		#endregion

		#region Skill Requirements - Agility Potions

		/// <summary>Minimum skill required for agility potion</summary>
		public const double SKILL_MIN_AGILITY = 35.0;

		/// <summary>Maximum skill required for agility potion</summary>
		public const double SKILL_MAX_AGILITY = 65.0;

		/// <summary>Reagent amount for agility potion</summary>
		public const int RESOURCE_BLOODMOSS_AGILITY = 1;

		/// <summary>Minimum skill required for greater agility potion</summary>
		public const double SKILL_MIN_GREATER_AGILITY = 55.0;

		/// <summary>Maximum skill required for greater agility potion</summary>
		public const double SKILL_MAX_GREATER_AGILITY = 85.0;

		/// <summary>Reagent amount for greater agility potion</summary>
		public const int RESOURCE_BLOODMOSS_GREATER_AGILITY = 3;

		#endregion

		#region Skill Requirements - Cure Potions

		/// <summary>Minimum skill required for lesser cure potion</summary>
		public const double SKILL_MIN_LESSER_CURE = 10.0;

		/// <summary>Maximum skill required for lesser cure potion</summary>
		public const double SKILL_MAX_LESSER_CURE = 45.0;

		/// <summary>Reagent amount for lesser cure potion</summary>
		public const int RESOURCE_GARLIC_LESSER_CURE = 2;

		/// <summary>Minimum skill required for cure potion</summary>
		public const double SKILL_MIN_CURE = 30.0;

		/// <summary>Maximum skill required for cure potion</summary>
		public const double SKILL_MAX_CURE = 75.0;

		/// <summary>Reagent amount for cure potion</summary>
		public const int RESOURCE_GARLIC_CURE = 3;

		/// <summary>Minimum skill required for greater cure potion</summary>
		public const double SKILL_MIN_GREATER_CURE = 60.0;

		/// <summary>Maximum skill required for greater cure potion</summary>
		public const double SKILL_MAX_GREATER_CURE = 90.0;

		/// <summary>Reagent amount for greater cure potion</summary>
		public const int RESOURCE_GARLIC_GREATER_CURE = 6;

		#endregion

		#region Skill Requirements - Explosion Potions

		/// <summary>Minimum skill required for lesser explosion potion</summary>
		public const double SKILL_MIN_LESSER_EXPLOSION = 15.0;

		/// <summary>Maximum skill required for lesser explosion potion</summary>
		public const double SKILL_MAX_LESSER_EXPLOSION = 55.0;

		/// <summary>Reagent amount for lesser explosion potion</summary>
		public const int RESOURCE_SULFUROUS_ASH_LESSER_EXPLOSION = 3;

		/// <summary>Minimum skill required for explosion potion</summary>
		public const double SKILL_MIN_EXPLOSION = 40.0;

		/// <summary>Maximum skill required for explosion potion</summary>
		public const double SKILL_MAX_EXPLOSION = 85.0;

		/// <summary>Reagent amount for explosion potion</summary>
		public const int RESOURCE_SULFUROUS_ASH_EXPLOSION = 5;

		/// <summary>Minimum skill required for greater explosion potion</summary>
		public const double SKILL_MIN_GREATER_EXPLOSION = 65.0;

		/// <summary>Maximum skill required for greater explosion potion</summary>
		public const double SKILL_MAX_GREATER_EXPLOSION = 95.0;

		/// <summary>Reagent amount for greater explosion potion</summary>
		public const int RESOURCE_SULFUROUS_ASH_GREATER_EXPLOSION = 10;

		#endregion

		#region Skill Requirements - Heal Potions

		/// <summary>Minimum skill required for lesser heal potion</summary>
		public const double SKILL_MIN_LESSER_HEAL = 15.0;

		/// <summary>Maximum skill required for lesser heal potion</summary>
		public const double SKILL_MAX_LESSER_HEAL = 45.0;

		/// <summary>Reagent amount for lesser heal potion</summary>
		public const int RESOURCE_GINSENG_LESSER_HEAL = 2;

		/// <summary>Minimum skill required for heal potion</summary>
		public const double SKILL_MIN_HEAL = 35.0;

		/// <summary>Maximum skill required for heal potion</summary>
		public const double SKILL_MAX_HEAL = 70.0;

		/// <summary>Reagent amount for heal potion</summary>
		public const int RESOURCE_GINSENG_HEAL = 4;

		/// <summary>Minimum skill required for greater heal potion</summary>
		public const double SKILL_MIN_GREATER_HEAL = 55.0;

		/// <summary>Maximum skill required for greater heal potion</summary>
		public const double SKILL_MAX_GREATER_HEAL = 95.0;

		/// <summary>Reagent amount for greater heal potion</summary>
		public const int RESOURCE_GINSENG_GREATER_HEAL = 7;

		#endregion

		#region Skill Requirements - Nightsight Potion

		/// <summary>Minimum skill required for nightsight potion</summary>
		public const double SKILL_MIN_NIGHTSIGHT = 5.0;

		/// <summary>Maximum skill required for nightsight potion</summary>
		public const double SKILL_MAX_NIGHTSIGHT = 30.0;

		/// <summary>Reagent amount for nightsight potion</summary>
		public const int RESOURCE_SPIDERS_SILK_NIGHTSIGHT = 1;

		#endregion

		#region Skill Requirements - Refresh Potions

		/// <summary>Minimum skill required for refresh potion</summary>
		public const double SKILL_MIN_REFRESH = 10.0;

		/// <summary>Maximum skill required for refresh potion</summary>
		public const double SKILL_MAX_REFRESH = 45.0;

		/// <summary>Reagent amount for refresh potion</summary>
		public const int RESOURCE_BLACK_PEARL_REFRESH = 2;

		/// <summary>Minimum skill required for total refresh potion</summary>
		public const double SKILL_MIN_TOTAL_REFRESH = 45.0;

		/// <summary>Maximum skill required for total refresh potion</summary>
		public const double SKILL_MAX_TOTAL_REFRESH = 80.0;

		/// <summary>Reagent amount for total refresh potion</summary>
		public const int RESOURCE_BLACK_PEARL_TOTAL_REFRESH = 5;

		#endregion

		#region Skill Requirements - Strength Potions

		/// <summary>Minimum skill required for strength potion</summary>
		public const double SKILL_MIN_STRENGTH = 25.0;

		/// <summary>Maximum skill required for strength potion</summary>
		public const double SKILL_MAX_STRENGTH = 65.0;

		/// <summary>Reagent amount for strength potion</summary>
		public const int RESOURCE_MANDRAKE_ROOT_STRENGTH = 2;

		/// <summary>Minimum skill required for greater strength potion</summary>
		public const double SKILL_MIN_GREATER_STRENGTH = 45.0;

		/// <summary>Maximum skill required for greater strength potion</summary>
		public const double SKILL_MAX_GREATER_STRENGTH = 90.0;

		/// <summary>Reagent amount for greater strength potion</summary>
		public const int RESOURCE_MANDRAKE_ROOT_GREATER_STRENGTH = 5;

		#endregion

		#region Skill Requirements - Poison Potions

		/// <summary>Minimum skill required for lesser poison potion</summary>
		public const double SKILL_MIN_LESSER_POISON = 15.0;

		/// <summary>Maximum skill required for lesser poison potion</summary>
		public const double SKILL_MAX_LESSER_POISON = 50.0;

		/// <summary>Reagent amount for lesser poison potion</summary>
		public const int RESOURCE_NIGHTSHADE_LESSER_POISON = 1;

		/// <summary>Minimum skill required for poison potion</summary>
		public const double SKILL_MIN_POISON = 25.0;

		/// <summary>Maximum skill required for poison potion</summary>
		public const double SKILL_MAX_POISON = 70.0;

		/// <summary>Reagent amount for poison potion</summary>
		public const int RESOURCE_NIGHTSHADE_POISON = 2;

		/// <summary>Minimum skill required for greater poison potion</summary>
		public const double SKILL_MIN_GREATER_POISON = 40.0;

		/// <summary>Maximum skill required for greater poison potion</summary>
		public const double SKILL_MAX_GREATER_POISON = 80.0;

		/// <summary>Reagent amount for greater poison potion</summary>
		public const int RESOURCE_NIGHTSHADE_GREATER_POISON = 4;

		/// <summary>Nox crystal amount for greater poison potion</summary>
		public const int RESOURCE_NOX_CRYSTAL_GREATER_POISON = 1;

		/// <summary>Minimum skill required for deadly poison potion</summary>
		public const double SKILL_MIN_DEADLY_POISON = 65.0;

		/// <summary>Maximum skill required for deadly poison potion</summary>
		public const double SKILL_MAX_DEADLY_POISON = 95.0;

		/// <summary>Reagent amount for deadly poison potion</summary>
		public const int RESOURCE_NIGHTSHADE_DEADLY_POISON = 8;

		/// <summary>Nox crystal amount for deadly poison potion</summary>
		public const int RESOURCE_NOX_CRYSTAL_DEADLY_POISON = 2;

		/// <summary>Minimum skill required for lethal poison potion</summary>
		public const double SKILL_MIN_LETHAL_POISON = 80.0;

		/// <summary>Maximum skill required for lethal poison potion</summary>
		public const double SKILL_MAX_LETHAL_POISON = 110.0;

		/// <summary>Reagent amount for lethal poison potion</summary>
		public const int RESOURCE_NIGHTSHADE_LETHAL_POISON = 12;

		/// <summary>Nox crystal amount for lethal poison potion</summary>
		public const int RESOURCE_NOX_CRYSTAL_LETHAL_POISON = 3;

		#endregion

		#region Skill Requirements - Cosmetic Potions

		/// <summary>Minimum skill required for hair oil potion</summary>
		public const double SKILL_MIN_HAIR_OIL = 80.0;

		/// <summary>Maximum skill required for hair oil potion</summary>
		public const double SKILL_MAX_HAIR_OIL = 110.0;

		/// <summary>Pixie skull amount for hair oil potion</summary>
		public const int RESOURCE_PIXIE_SKULL_HAIR_OIL = 2;

		/// <summary>Minimum skill required for hair dye potion</summary>
		public const double SKILL_MIN_HAIR_DYE = 80.0;

		/// <summary>Maximum skill required for hair dye potion</summary>
		public const double SKILL_MAX_HAIR_DYE = 110.0;

		/// <summary>Fairy egg amount for hair dye potion</summary>
		public const int RESOURCE_FAIRY_EGG_HAIR_DYE = 3;

		#endregion
	}
}

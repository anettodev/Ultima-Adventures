namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized PT-BR string constants for Alchemy Recipe Book system.
	/// All user-facing messages and labels in Portuguese-Brazilian.
	/// </summary>
	public static class AlchemyRecipeStringConstants
	{
		#region Item Names

		/// <summary>Name for Alchemy Recipe Book</summary>
		public const string BOOK_NAME = "Tomo Alquímico";

		/// <summary>Name for Alchemy Recipe Scroll</summary>
		public const string SCROLL_NAME = "Pergaminho de Receita de Alquimia";

		#endregion

		#region Category Names

		/// <summary>Category 0: Basic Potions</summary>
		public const string CATEGORY_BASIC = "Poções Básicas";

		/// <summary>Category 1: Strong and Advanced Potions</summary>
		public const string CATEGORY_ADVANCED = "Poções Fortes e Avançadas";

		/// <summary>Category 2: Special Potions</summary>
		public const string CATEGORY_SPECIAL = "Poções Especiais";

		/// <summary>Category 3: Cosmetic Potions</summary>
		public const string CATEGORY_COSMETIC = "Cosméticos";

		#endregion

		#region Gump Labels

		/// <summary>Main gump title</summary>
		public const string GUMP_TITLE = "TOMO ALQUÍMICO";

		/// <summary>Recipes known counter label</summary>
		public const string LABEL_RECIPES_KNOWN = "Receitas Conhecidas:";

		/// <summary>Categories section header</summary>
		public const string LABEL_CATEGORIES = "CATEGORIAS";

		/// <summary>Recipes section header</summary>
		public const string LABEL_RECIPES = "RECEITAS";

		/// <summary>Skill required label</summary>
		public const string LABEL_SKILL_REQUIRED = "Habilidade Necessária:";

		/// <summary>Reagents required label</summary>
		public const string LABEL_REAGENTS_REQUIRED = "Reagentes Necessários:";

		/// <summary>Category label</summary>
		public const string LABEL_CATEGORY = "Categoria:";

		/// <summary>Back button label</summary>
		public const string LABEL_BACK = "Voltar";

		/// <summary>No recipes learned message</summary>
		public const string MSG_NO_RECIPES_IN_CATEGORY = "Nenhuma receita aprendida.";

		/// <summary>Additional recipes indicator</summary>
		public const string MSG_MORE_RECIPES = "... e mais {0} receitas";

		#endregion

		#region Messages - Success

		/// <summary>Recipe learned successfully</summary>
		public const string MSG_RECIPE_LEARNED = "Você aprendeu a receita: {0}!";

		#endregion

		#region Messages - Errors

		/// <summary>Book must be in backpack or hand</summary>
		public const string MSG_BOOK_NOT_ACCESSIBLE = "O tomo deve estar em sua mochila ou mão.";

		/// <summary>Scroll must be in backpack</summary>
		public const string MSG_SCROLL_NOT_IN_BACKPACK = "O pergaminho deve estar em sua mochila.";

		/// <summary>Recipe is invalid</summary>
		public const string MSG_INVALID_RECIPE = "Esta receita é inválida.";

		/// <summary>Already know this recipe</summary>
		public const string MSG_ALREADY_KNOW_RECIPE = "Você já conhece esta receita.";

		/// <summary>Insufficient alchemy skill to learn recipe</summary>
		public const string MSG_INSUFFICIENT_SKILL = "Você precisa de pelo menos {0:F1} de Alchemy para aprender esta receita.";

		/// <summary>Don't know this recipe (craft attempt)</summary>
		public const string MSG_DONT_KNOW_RECIPE = "Você não conhece a receita para criar este item!";

		#endregion

		#region Object Property List

		/// <summary>Recipe name property on scroll (brackets format)</summary>
		public const string PROP_RECIPE = "[{0}]";

		#endregion

		#region HTML Colors

		/// <summary>Title color (light green)</summary>
		public const string COLOR_TITLE = "#000000";

		/// <summary>Info color (light cyan)</summary>
		public const string COLOR_INFO = "#AAFFAA";

		/// <summary>Header color (orange)</summary>
		public const string COLOR_HEADER = "#FFCC99";

		/// <summary>Selected category color (yellow)</summary>
		public const string COLOR_CATEGORY_SELECTED = "#FFFF00";

		/// <summary>Unselected category color (light gray)</summary>
		public const string COLOR_CATEGORY_UNSELECTED = "#CCCCCC";

		/// <summary>Recipe name color (light cyan)</summary>
		public const string COLOR_RECIPE_NAME = "#CCFFFF";

		/// <summary>Disabled/empty text color (dark gray, almost black)</summary>
		public const string COLOR_DISABLED = "#555555";

		#endregion

		#region Category Colors

		/// <summary>Category 0: Poções Básicas - Light blue</summary>
		public const string COLOR_CATEGORY_BASIC = "#99CCFF";

		/// <summary>Category 1: Poções Fortes e Avançadas - Gold</summary>
		public const string COLOR_CATEGORY_ADVANCED = "#FFCC66";

		/// <summary>Category 2: Poções Especiais - Light orange</summary>
		public const string COLOR_CATEGORY_SPECIAL = "#FFCC99";

		/// <summary>Category 3: Cosméticos - Pink</summary>
		public const string COLOR_CATEGORY_COSMETIC = "#FF99CC";

		#endregion
	}
}


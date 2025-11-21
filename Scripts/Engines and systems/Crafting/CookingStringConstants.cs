using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized string constants for Cooking crafting system.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Replaces hardcoded cliloc IDs with meaningful translations.
	/// </summary>
	public static class CookingStringConstants
	{
		#region Category Names

		/// <summary>Cliloc 1044495: "Ingredients"</summary>
		public const string CATEGORY_INGREDIENTS = "Ingredientes";

		/// <summary>Cliloc 1044496: "Preparations"</summary>
		public const string CATEGORY_PREPARATIONS = "Preparações";

		/// <summary>Cliloc 1044497: "Baking"</summary>
		public const string CATEGORY_BAKING = "Assados";

		/// <summary>Cliloc 1044498: "Barbecue"</summary>
		public const string CATEGORY_BARBECUE = "Churrasco";

		/// <summary>Custom category: "Rations"</summary>
		public const string CATEGORY_RATIONS = "Rações";

		#endregion

		#region Item Names

		// Ingredients
		public const string ITEM_SACK_FLOUR = "Saco de Farinha";
		public const string ITEM_DOUGH = "Massa";
		public const string ITEM_SWEET_DOUGH = "Massa Doce";
		public const string ITEM_CAKE_MIX = "Mistura para Bolo";
		public const string ITEM_COOKIE_MIX = "Mistura para Biscoitos";
		public const string ITEM_FRUIT_BASKET = "Cesta de Frutas";
		public const string ITEM_UNBAKED_QUICHE = "Quiche Não Assado";
		public const string ITEM_UNBAKED_MEAT_PIE = "Torta de Carne Não Assada";
		public const string ITEM_UNCOOKED_SAUSAGE_PIZZA = "Pizza de Linguiça Crua";
		public const string ITEM_UNCOOKED_CHEESE_PIZZA = "Pizza de Queijo Crua";
		public const string ITEM_UNBAKED_FRUIT_PIE = "Torta de Frutas Não Assada";
		public const string ITEM_UNBAKED_PEACH_COBBLER = "Torta de Pêssego Não Assada";
		public const string ITEM_UNBAKED_APPLE_PIE = "Torta de Maçã Não Assada";
		public const string ITEM_UNBAKED_PUMPKIN_PIE = "Torta de Abóbora Não Assada";
		public const string ITEM_GREEN_TEA = "Chá Verde";
		public const string ITEM_WASABI_CLUMPS = "Pedaços de Wasabi";
		public const string ITEM_SUSHI_ROLLS = "Rolinhos de Sushi";
		public const string ITEM_SUSHI_PLATTER = "Prato de Sushi";
		public const string ITEM_TRIBAL_PAINT = "Pintura Tribal";
		public const string ITEM_EGG_BOMB = "Bomba de Ovo";

		// Baking
		public const string ITEM_BREAD_LOAF = "Pão";
		public const string ITEM_CHEESE_BREAD = "Pão de Queijo";
		public const string ITEM_COOKIES = "Biscoitos";
		public const string ITEM_CAKE = "Bolo";
		public const string ITEM_MUFFINS = "Muffins";
		public const string ITEM_QUICHE = "Quiche";
		public const string ITEM_MEAT_PIE = "Torta de Carne";
		public const string ITEM_SAUSAGE_PIZZA = "Pizza de Linguiça";
		public const string ITEM_CHEESE_PIZZA = "Pizza de Queijo";
		public const string ITEM_FRUIT_PIE = "Torta de Frutas";
		public const string ITEM_PEACH_COBBLER = "Torta de Pêssego";
		public const string ITEM_APPLE_PIE = "Torta de Maçã";
		public const string ITEM_PUMPKIN_PIE = "Torta de Abóbora";
		public const string ITEM_MISO_SOUP = "Sopa de Missô";
		public const string ITEM_WHITE_MISO_SOUP = "Sopa de Missô Branco";
		public const string ITEM_RED_MISO_SOUP = "Sopa de Missô Vermelho";
		public const string ITEM_AWASE_MISO_SOUP = "Sopa de Missô Awase";

		// Barbecue
		public const string ITEM_COOKED_BIRD = "Pássaro Cozido";
		public const string ITEM_CHICKEN_LEG = "Coxa de Frango";
		public const string ITEM_FISH_STEAK = "Filé de Peixe";
		public const string ITEM_FRIED_EGGS = "Ovos Fritos";
		public const string ITEM_LAMB_LEG = "Pernil de Carneiro";
		public const string ITEM_RIBS = "Costelas";

		// Rations
		public const string ITEM_RATION_FISH = "Ração de Peixe";
		public const string ITEM_RATION_LAMB = "Ração de Carneiro";
		public const string ITEM_RATION_PORK = "Ração de Porco";
		public const string ITEM_RATION_CHICKEN = "Ração de Galinha";
		public const string ITEM_RATION_BIRD = "Ração de Pássaro";

		#endregion

		#region Resource Names

		/// <summary>Cliloc 1044489: "Wheat Sheaf"</summary>
		public const string RESOURCE_WHEAT_SHEAF = "Feixe de Trigo";

		/// <summary>Cliloc 1044468: "Sack of Flour"</summary>
		public const string RESOURCE_SACK_FLOUR = "Saco de Farinha";

		/// <summary>Cliloc 1046458: "Water"</summary>
		public const string RESOURCE_WATER = "Água";

		/// <summary>Cliloc 1044469: "Dough"</summary>
		public const string RESOURCE_DOUGH = "Massa";

		/// <summary>Cliloc 1044472: "Jar of Honey"</summary>
		public const string RESOURCE_JAR_HONEY = "Pote de Mel";

		/// <summary>Cliloc 1044475: "Sweet Dough"</summary>
		public const string RESOURCE_SWEET_DOUGH = "Massa Doce";

		/// <summary>Cliloc 1044474: "Cookie Mix"</summary>
		public const string RESOURCE_COOKIE_MIX = "Mistura para Biscoitos";

		/// <summary>Cliloc 1044471: "Cake Mix"</summary>
		public const string RESOURCE_CAKE_MIX = "Mistura para Bolo";

		public const string RESOURCE_BASKET = "Cesta";
		public const string RESOURCE_PEAR = "Pêra";
		public const string RESOURCE_PEACH = "Pêssego";
		public const string RESOURCE_APPLE = "Maçã";
		public const string RESOURCE_CARROT = "Cenoura";
		public const string RESOURCE_POTATO = "Batata";
		public const string RESOURCE_TOMATO = "Tomate";

		/// <summary>Cliloc 1044477: "Eggs"</summary>
		public const string RESOURCE_EGGS = "Ovos";

		/// <summary>Cliloc 1044482: "Raw Ribs"</summary>
		public const string RESOURCE_RAW_RIBS = "Costelas Cruas";

		/// <summary>Cliloc 1044483: "Sausage"</summary>
		public const string RESOURCE_SAUSAGE = "Linguiça";

		/// <summary>Cliloc 1044486: "Cheese Wheel"</summary>
		public const string RESOURCE_CHEESE_WHEEL = "Roda de Queijo";

		/// <summary>Cliloc 1044481: "Pear"</summary>
		public const string RESOURCE_PEAR_SINGLE = "Pêra";

		/// <summary>Cliloc 1044480: "Peach"</summary>
		public const string RESOURCE_PEACH_SINGLE = "Pêssego";

		/// <summary>Cliloc 1044479: "Apple"</summary>
		public const string RESOURCE_APPLE_SINGLE = "Maçã";

		/// <summary>Cliloc 1044484: "Pumpkin"</summary>
		public const string RESOURCE_PUMPKIN = "Abóbora";

		/// <summary>Cliloc 1030316: "Green Tea Basket"</summary>
		public const string RESOURCE_GREEN_TEA_BASKET = "Cesta de Chá Verde";

		/// <summary>Cliloc 1025633: "Wooden Bowl of Peas"</summary>
		public const string RESOURCE_WOODEN_BOWL_PEAS = "Tigela de Madeira com Ervilhas";

		/// <summary>Cliloc 1044476: "Raw Fish Steak"</summary>
		public const string RESOURCE_RAW_FISH_STEAK = "Filé de Peixe Cru";

		/// <summary>Cliloc 1046460: "Tribal Berry"</summary>
		public const string RESOURCE_TRIBAL_BERRY = "Fruta Tribal";

		/// <summary>Cliloc 1044518: "Unbaked Quiche"</summary>
		public const string RESOURCE_UNBAKED_QUICHE = "Quiche Não Assado";

		/// <summary>Cliloc 1044519: "Unbaked Meat Pie"</summary>
		public const string RESOURCE_UNBAKED_MEAT_PIE = "Torta de Carne Não Assada";

		/// <summary>Cliloc 1044520: "Uncooked Sausage Pizza"</summary>
		public const string RESOURCE_UNCOOKED_SAUSAGE_PIZZA = "Pizza de Linguiça Crua";

		/// <summary>Cliloc 1044521: "Uncooked Cheese Pizza"</summary>
		public const string RESOURCE_UNCOOKED_CHEESE_PIZZA = "Pizza de Queijo Crua";

		/// <summary>Cliloc 1044522: "Unbaked Fruit Pie"</summary>
		public const string RESOURCE_UNBAKED_FRUIT_PIE = "Torta de Frutas Não Assada";

		/// <summary>Cliloc 1044523: "Unbaked Peach Cobbler"</summary>
		public const string RESOURCE_UNBAKED_PEACH_COBBLER = "Torta de Pêssego Não Assada";

		/// <summary>Cliloc 1044524: "Unbaked Apple Pie"</summary>
		public const string RESOURCE_UNBAKED_APPLE_PIE = "Torta de Maçã Não Assada";

		/// <summary>Cliloc 1046461: "Unbaked Pumpkin Pie"</summary>
		public const string RESOURCE_UNBAKED_PUMPKIN_PIE = "Torta de Abóbora Não Assada";

		/// <summary>Cliloc 1044470: "Raw Bird"</summary>
		public const string RESOURCE_RAW_BIRD = "Pássaro Cru";

		/// <summary>Cliloc 1044473: "Raw Chicken Leg"</summary>
		public const string RESOURCE_RAW_CHICKEN_LEG = "Coxa de Frango Crua";

		/// <summary>Cliloc 1044478: "Raw Lamb Leg"</summary>
		public const string RESOURCE_RAW_LAMB_LEG = "Pernil de Carneiro Cru";

		/// <summary>Cliloc 1044485: "Raw Ribs"</summary>
		public const string RESOURCE_RAW_RIBS_ALT = "Costelas Cruas";

		/// <summary>Cliloc 1024156: "Bread Loaf"</summary>
		public const string RESOURCE_BREAD_LOAF = "Pão";

		#endregion

		#region Error Messages

		/// <summary>Cliloc 1044038: "You have worn out your tool!"</summary>
		public const string MSG_TOOL_WORN_OUT = "Você desgastou sua ferramenta!";

		/// <summary>Cliloc 1044263: "The tool must be on your person to use."</summary>
		public const string MSG_TOOL_MUST_BE_ON_PERSON = "A ferramenta deve estar com você para usar.";

		/// <summary>Cliloc 1044253: Generic failure message</summary>
		public const string MSG_RESOURCE_LOST = "Você não tem recursos suficientes.";

		/// <summary>Cliloc 1044490: Wheat sheaf lost message</summary>
		public const string MSG_WHEAT_SHEAF_LOST = "Você não tem feixes de trigo suficientes.";

		#endregion

		#region Success/Failure Messages

		/// <summary>Cliloc 1044043: "You failed to create the item, and some of your materials are lost."</summary>
		public const string MSG_FAILED_MATERIAL_LOST = "Você falhou em criar o item e alguns materiais foram perdidos.";

		/// <summary>Cliloc 1044157: "You failed to create the item, but no materials were lost."</summary>
		public const string MSG_FAILED_NO_MATERIAL_LOST = "Você falhou em criar o item, mas nenhum material foi perdido.";

		/// <summary>Cliloc 502785: "You were barely able to make this item. It's quality is below average."</summary>
		public const string MSG_BARELY_MADE_ITEM = "Você mal conseguiu fazer este item. A qualidade está abaixo da média.";

		/// <summary>Cliloc 1044156: "You create an exceptional quality item and affix your maker's mark."</summary>
		public const string MSG_EXCEPTIONAL_WITH_MARK = "Você cria um item de qualidade excepcional e coloca sua marca de criador.";

		/// <summary>Cliloc 1044155: "You create an exceptional quality item."</summary>
		public const string MSG_EXCEPTIONAL_QUALITY = "Você cria um item de qualidade excepcional.";

		/// <summary>Cliloc 1044154: "You create the item."</summary>
		public const string MSG_ITEM_CREATED = "Você cria o item.";

		#endregion
	}
}

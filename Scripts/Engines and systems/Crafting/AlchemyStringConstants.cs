namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized string constants for Alchemy messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from DefAlchemy.cs to improve maintainability and enable localization.
	/// </summary>
	public static class AlchemyStringConstants
	{
		#region Craft Group Names

		/// <summary>Group name: "Poções"</summary>
		public const string GROUP_POTIONS = "Poções";

		/// <summary>Group name: "Cosmético"</summary>
		public const string GROUP_COSMETIC = "Cosmético";

		#endregion

		#region Item Names - Agility Potions

		/// <summary>Item name: "agilidade"</summary>
		public const string ITEM_AGILITY_POTION = "agilidade";

		/// <summary>Item name: "agilidade maior"</summary>
		public const string ITEM_GREATER_AGILITY_POTION = "agilidade maior";

		#endregion

		#region Item Names - Cure Potions

		/// <summary>Item name: "cura menor"</summary>
		public const string ITEM_LESSER_CURE_POTION = "cura menor";

		/// <summary>Item name: "cura"</summary>
		public const string ITEM_CURE_POTION = "cura";

		/// <summary>Item name: "cura maior"</summary>
		public const string ITEM_GREATER_CURE_POTION = "cura maior";

		#endregion

		#region Item Names - Explosion Potions

		/// <summary>Item name: "explosão menor"</summary>
		public const string ITEM_LESSER_EXPLOSION_POTION = "explosão menor";

		/// <summary>Item name: "explosão"</summary>
		public const string ITEM_EXPLOSION_POTION = "explosão";

		/// <summary>Item name: "explosão maior"</summary>
		public const string ITEM_GREATER_EXPLOSION_POTION = "explosão maior";

		#endregion

		#region Item Names - Heal Potions

		/// <summary>Item name: "vida menor"</summary>
		public const string ITEM_LESSER_HEAL_POTION = "vida menor";

		/// <summary>Item name: "vida"</summary>
		public const string ITEM_HEAL_POTION = "vida";

		/// <summary>Item name: "vida maior"</summary>
		public const string ITEM_GREATER_HEAL_POTION = "vida maior";

		#endregion

		#region Item Names - Nightsight Potion

		/// <summary>Item name: "visão noturna"</summary>
		public const string ITEM_NIGHTSIGHT_POTION = "visão noturna";

		#endregion

		#region Item Names - Refresh Potions

		/// <summary>Item name: "refresh"</summary>
		public const string ITEM_REFRESH_POTION = "refresh";

		/// <summary>Item name: "refresh, total"</summary>
		public const string ITEM_TOTAL_REFRESH_POTION = "refresh, total";

		#endregion

		#region Item Names - Strength Potions

		/// <summary>Item name: "força"</summary>
		public const string ITEM_STRENGTH_POTION = "força";

		/// <summary>Item name: "força maior"</summary>
		public const string ITEM_GREATER_STRENGTH_POTION = "força maior";

		#endregion

		#region Item Names - Poison Potions

		/// <summary>Item name: "veneno menor"</summary>
		public const string ITEM_LESSER_POISON_POTION = "veneno menor";

		/// <summary>Item name: "veneno"</summary>
		public const string ITEM_POISON_POTION = "veneno";

		/// <summary>Item name: "veneno maior"</summary>
		public const string ITEM_GREATER_POISON_POTION = "veneno maior";

		/// <summary>Item name: "veneno mortal"</summary>
		public const string ITEM_DEADLY_POISON_POTION = "veneno mortal";

		/// <summary>Item name: "veneno letal"</summary>
		public const string ITEM_LETHAL_POISON_POTION = "veneno letal";

		#endregion

		#region Item Names - Cosmetic Potions

		/// <summary>Item name: "poção de corte de cabelo"</summary>
		public const string ITEM_HAIR_OIL_POTION = "poção de corte de cabelo";

		/// <summary>Item name: "tinta de cabelo"</summary>
		public const string ITEM_HAIR_DYE_POTION = "tinta de cabelo";

		#endregion

		#region Menu and System Messages

		/// <summary>Menu title: "MENU DE ALQUIMIA"</summary>
		public const string MSG_GUMP_TITLE = "MENU DE ALQUIMIA";

		/// <summary>Error: "Você quebrou sua ferramenta!"</summary>
		public const string MSG_TOOL_WORN_OUT = "Você quebrou sua ferramenta!";

		/// <summary>Error: "A ferramenta deve estar com você para usar."</summary>
		public const string MSG_TOOL_MUST_BE_ON_PERSON = "A ferramenta deve estar com você para usar.";

		/// <summary>Error: "Você falhou ao criar o item, e alguns de seus materiais foram perdidos."</summary>
		public const string MSG_FAILED_LOST_MATERIALS = "Você falhou ao criar o item, e alguns de seus materiais foram perdidos.";

		/// <summary>Error: "Você falhou ao criar uma poção útil."</summary>
		public const string MSG_FAILED_POTION = "Você falhou ao criar uma poção útil.";

		/// <summary>Success: "Você cria o item."</summary>
		public const string MSG_ITEM_CREATED = "Você cria o item.";

		/// <summary>Success: "Você cria a poção e a despeja em um barril."</summary>
		public const string MSG_POTION_TO_KEG = "Você cria a poção e a despeja em um barril.";

		/// <summary>Success: "Você despeja a poção em uma garrafa..."</summary>
		public const string MSG_POTION_TO_BOTTLE = "Você despeja a poção em uma garrafa...";

		#endregion

		#region Resource Names

		/// <summary>Resource name: "Garrafa"</summary>
		public const string RESOURCE_BOTTLE = "Garrafa";

		/// <summary>Error: "Você não tem garrafas suficientes."</summary>
		public const string MSG_INSUFFICIENT_BOTTLES = "Você não tem garrafas suficientes.";

		/// <summary>Resource name: "Musgo Sanguíneo"</summary>
		public const string RESOURCE_BLOODMOSS = "Musgo Sanguíneo";

		/// <summary>Error: "Você não tem musgo sanguíneo suficiente."</summary>
		public const string MSG_INSUFFICIENT_BLOODMOSS = "Você não tem musgo sanguíneo suficiente.";

		/// <summary>Resource name: "Alho"</summary>
		public const string RESOURCE_GARLIC = "Alho";

		/// <summary>Error: "Você não tem alho suficiente."</summary>
		public const string MSG_INSUFFICIENT_GARLIC = "Você não tem alho suficiente.";

		/// <summary>Resource name: "Ginseng"</summary>
		public const string RESOURCE_GINSENG = "Ginseng";

		/// <summary>Error: "Você não tem ginseng suficiente."</summary>
		public const string MSG_INSUFFICIENT_GINSENG = "Você não tem ginseng suficiente.";

		/// <summary>Resource name: "Raiz de Mandrágora"</summary>
		public const string RESOURCE_MANDRAKE_ROOT = "Raiz de Mandrágora";

		/// <summary>Error: "Você não tem raiz de mandrágora suficiente."</summary>
		public const string MSG_INSUFFICIENT_MANDRAKE_ROOT = "Você não tem raiz de mandrágora suficiente.";

		/// <summary>Resource name: "Meimendro"</summary>
		public const string RESOURCE_NIGHTSHADE = "Meimendro";

		/// <summary>Error: "Você não tem meimendro suficiente."</summary>
		public const string MSG_INSUFFICIENT_NIGHTSHADE = "Você não tem meimendro suficiente.";

		/// <summary>Resource name: "Cinza Sulfurosa"</summary>
		public const string RESOURCE_SULFUROUS_ASH = "Cinza Sulfurosa";

		/// <summary>Error: "Você não tem cinza sulfurosa suficiente."</summary>
		public const string MSG_INSUFFICIENT_SULFUROUS_ASH = "Você não tem cinza sulfurosa suficiente.";

		/// <summary>Resource name: "Seda de Aranha"</summary>
		public const string RESOURCE_SPIDERS_SILK = "Seda de Aranha";

		/// <summary>Error: "Você não tem seda de aranha suficiente."</summary>
		public const string MSG_INSUFFICIENT_SPIDERS_SILK = "Você não tem seda de aranha suficiente.";

		/// <summary>Resource name: "Pérola Negra"</summary>
		public const string RESOURCE_BLACK_PEARL = "Pérola Negra";

		/// <summary>Error: "Você não tem pérola negra suficiente."</summary>
		public const string MSG_INSUFFICIENT_BLACK_PEARL = "Você não tem pérola negra suficiente.";

		/// <summary>Resource name: "Cristal Nox"</summary>
		public const string RESOURCE_NOX_CRYSTAL = "Cristal Nox";

		/// <summary>Error: "Você não tem cristal nox suficiente."</summary>
		public const string MSG_INSUFFICIENT_NOX_CRYSTAL = "Você não tem cristal nox suficiente.";

		/// <summary>Error: "Você não tem os itens necessários."</summary>
		public const string MSG_INSUFFICIENT_RESOURCES = "Você não tem os itens necessários.";

		#endregion
	}
}

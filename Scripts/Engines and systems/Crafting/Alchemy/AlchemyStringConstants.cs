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

	/// <summary>Group name: "Poções Básicas" - All Lesser and regular potions</summary>
	public const string GROUP_BASIC = "Poções Básicas";

	/// <summary>Group name: "Poções Fortes e Avançadas" - All Greater potions</summary>
	public const string GROUP_ADVANCED = "Poções Fortes e Avançadas";

	/// <summary>Group name: "Poções Especiais" - Frostbite, Confusion, Conflagration</summary>
	public const string GROUP_SPECIAL = "Poções Especiais";

	/// <summary>Group name: "Cosméticos" - Hair cut and tint</summary>
	public const string GROUP_COSMETIC = "Cosméticos";

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
		public const string ITEM_REFRESH_POTION = "vigor";

	/// <summary>Item name: "refresh, total"</summary>
	public const string ITEM_TOTAL_REFRESH_POTION = "vigor total";

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

	#region Item Names - Mana Potions

	/// <summary>Item name: "mana menor"</summary>
	public const string ITEM_LESSER_MANA_POTION = "mana menor";

	/// <summary>Item name: "mana"</summary>
	public const string ITEM_MANA_POTION = "mana";

	/// <summary>Item name: "mana maior"</summary>
	public const string ITEM_GREATER_MANA_POTION = "mana maior";

	#endregion

	#region Item Names - Invisibility Potions

	/// <summary>Item name: "invisibilidade menor"</summary>
	public const string ITEM_LESSER_INVISIBILITY_POTION = "invisibilidade menor";

	/// <summary>Item name: "invisibilidade"</summary>
	public const string ITEM_INVISIBILITY_POTION = "invisibilidade";

	/// <summary>Item name: "invisibilidade maior"</summary>
	public const string ITEM_GREATER_INVISIBILITY_POTION = "invisibilidade maior";

	#endregion

	#region Item Names - Frostbite Potions

	/// <summary>Item name: "congelamento"</summary>
	public const string ITEM_FROSTBITE_POTION = "congelamento";

	/// <summary>Item name: "congelamento maior"</summary>
	public const string ITEM_GREATER_FROSTBITE_POTION = "congelamento maior";

	#endregion

	#region Item Names - Confusion Blast Potions

	/// <summary>Item name: "confusão"</summary>
	public const string ITEM_CONFUSION_BLAST_POTION = "confusão";

	/// <summary>Item name: "confusão maior"</summary>
	public const string ITEM_GREATER_CONFUSION_BLAST_POTION = "confusão maior";

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

		/// <summary>Resource name: "Bottle" (English for consistency)</summary>
		public const string RESOURCE_BOTTLE = "Garrafa Vazia";

		/// <summary>Error: "Você não tem garrafas suficientes."</summary>
		public const string MSG_INSUFFICIENT_BOTTLES = "Você não tem garrafas vazias o suficientes.";

		/// <summary>Resource name: "Bloodmoss" (English for consistency)</summary>
		public const string RESOURCE_BLOODMOSS = "Bloodmoss";

		/// <summary>Error: "Você não tem musgo sanguíneo suficiente."</summary>
		public const string MSG_INSUFFICIENT_BLOODMOSS = "Você não tem Bloodmoss suficiente.";

		/// <summary>Resource name: "Garlic" (English for consistency)</summary>
		public const string RESOURCE_GARLIC = "Garlic";

		/// <summary>Error: "Você não tem alho suficiente."</summary>
		public const string MSG_INSUFFICIENT_GARLIC = "Você não tem Garlic suficiente.";

		/// <summary>Resource name: "Ginseng" (English for consistency)</summary>
		public const string RESOURCE_GINSENG = "Ginseng";

		/// <summary>Error: "Você não tem ginseng suficiente."</summary>
		public const string MSG_INSUFFICIENT_GINSENG = "Você não tem ginseng suficiente.";

		/// <summary>Resource name: "Mandrake Root" (English for consistency)</summary>
		public const string RESOURCE_MANDRAKE_ROOT = "Mandrake Root";

		/// <summary>Error: "Você não tem raiz de mandrágora suficiente."</summary>
		public const string MSG_INSUFFICIENT_MANDRAKE_ROOT = "Você não tem Mandrake Root suficiente.";

		/// <summary>Resource name: "Nightshade" (English for consistency)</summary>
		public const string RESOURCE_NIGHTSHADE = "Nightshade";

		/// <summary>Error: "Você não tem meimendro suficiente."</summary>
		public const string MSG_INSUFFICIENT_NIGHTSHADE = "Você não tem Nightshade suficiente.";

		/// <summary>Resource name: "Sulfurous Ash" (English for consistency)</summary>
		public const string RESOURCE_SULFUROUS_ASH = "Sulfurous Ash";

		/// <summary>Error: "Você não tem cinza sulfurosa suficiente."</summary>
		public const string MSG_INSUFFICIENT_SULFUROUS_ASH = "Você não tem Sulfurous Ash suficiente.";

		/// <summary>Resource name: "Spider's Silk" (English for consistency)</summary>
		public const string RESOURCE_SPIDERS_SILK = "Spider's Silk";

		/// <summary>Error: "Você não tem seda de aranha suficiente."</summary>
		public const string MSG_INSUFFICIENT_SPIDERS_SILK = "Você não tem Spider's Silk suficiente.";

		/// <summary>Resource name: "Black Pearl" (English for consistency)</summary>
		public const string RESOURCE_BLACK_PEARL = "Black Pearl";

		/// <summary>Error: "Você não tem pérola negra suficiente."</summary>
		public const string MSG_INSUFFICIENT_BLACK_PEARL = "Você não tem pérola negra(Black Pearl) suficiente.";

		/// <summary>Resource name: "Nox Crystal" (English for consistency)</summary>
		public const string RESOURCE_NOX_CRYSTAL = "Nox Crystal";

		/// <summary>Error: "Você não tem cristal nox suficiente."</summary>
		public const string MSG_INSUFFICIENT_NOX_CRYSTAL = "Você não tem cristal nox suficiente.";

		/// <summary>Error: "Você não tem os itens necessários."</summary>
		public const string MSG_INSUFFICIENT_RESOURCES = "Você não tem os itens necessários.";

		#endregion
	}
}

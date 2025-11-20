namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized string constants for Bow Fletching messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from DefBowFletching.cs to improve maintainability and enable localization.
	/// </summary>
	public static class BowFletchingStringConstants
	{
		#region Craft Group Names

		/// <summary>Group name: "Materiais"</summary>
		public const string GROUP_MATERIALS = "Materiais";

		/// <summary>Group name: "Munição"</summary>
		public const string GROUP_AMMUNITION = "Munição";

		/// <summary>Group name: "Armas"</summary>
		public const string GROUP_WEAPONS = "Armas";

		#endregion

		#region Item Names - Materials

		/// <summary>Item name: "gravetos"</summary>
		public const string ITEM_KINDLING = "gravetos";

		/// <summary>Item name: "lote de gravetos"</summary>
		public const string ITEM_KINDLING_BATCH = "lote de gravetos";

		/// <summary>Item name: "haste"</summary>
		public const string ITEM_SHAFT = "haste";

		#endregion

		#region Item Names - Ammunition

		/// <summary>Item name: "flecha"</summary>
		public const string ITEM_ARROW = "flecha";

		/// <summary>Item name: "virote"</summary>
		public const string ITEM_BOLT = "virote";

		/// <summary>Item name: "dardos fukiya"</summary>
		public const string ITEM_FUKIYA_DARTS = "dardos fukiya";

		/// <summary>Item name: "arma de arremesso"</summary>
		public const string ITEM_THROWING_WEAPON = "arma de arremesso";

		#endregion

		#region Item Names - Weapons

		/// <summary>Item name: "arco"</summary>
		public const string ITEM_BOW = "arco";

		/// <summary>Item name: "besta"</summary>
		public const string ITEM_CROSSBOW = "besta";

		/// <summary>Item name: "besta pesada"</summary>
		public const string ITEM_HEAVY_CROSSBOW = "besta pesada";

		/// <summary>Item name: "arco composto"</summary>
		public const string ITEM_COMPOSITE_BOW = "arco composto";

		/// <summary>Item name: "besta repetidora"</summary>
		public const string ITEM_REPEATING_CROSSBOW = "besta repetidora";

		/// <summary>Item name: "yumi"</summary>
		public const string ITEM_YUMI = "yumi";

		/// <summary>Item name: "Arco curto da floresta"</summary>
		public const string ITEM_MAGICAL_SHORTBOW = "Arco curto da floresta";

		/// <summary>Item name: "Arco longo da floresta"</summary>
		public const string ITEM_ELVEN_COMPOSITE_LONGBOW = "Arco longo da floresta";

		#endregion

		#region Resource Names

		/// <summary>Resource name: "Tora"</summary>
		public const string RESOURCE_LOG = "Tora";

		/// <summary>Resource name: "Tábuas"</summary>
		public const string RESOURCE_BOARDS = "Tábuas";

		/// <summary>Error: "Você não tem madeira suficiente para fazer isso."</summary>
		public const string MSG_INSUFFICIENT_WOOD = "Você não tem madeira suficiente para fazer isso.";

		/// <summary>Resource name: "Haste"</summary>
		public const string RESOURCE_SHAFT = "Haste";

		/// <summary>Error: "Você não tem hastes suficientes."</summary>
		public const string MSG_INSUFFICIENT_SHAFTS = "Você não tem hastes suficientes.";

		/// <summary>Resource name: "Pena"</summary>
		public const string RESOURCE_FEATHER = "Pena";

		/// <summary>Error: "Você não tem penas suficientes."</summary>
		public const string MSG_INSUFFICIENT_FEATHERS = "Você não tem penas suficientes.";

		/// <summary>Resource name: "Lingote de Ferro"</summary>
		public const string RESOURCE_IRON_INGOT = "Lingote de Ferro";

		/// <summary>Error: "Você não tem metal suficiente."</summary>
		public const string MSG_INSUFFICIENT_METAL = "Você não tem metal suficiente.";

		/// <summary>Message: "Material de Tábuas"</summary>
		public const string MSG_BOARD_MATERIAL = "Tábuas de Madeira Comum";

		/// <summary>Error: "Você não pode trabalhar esta madeira estranha e incomum."</summary>
		public const string MSG_CANNOT_WORK_WOOD = "Você não pode trabalhar esta madeira estranha e incomum.";

		/// <summary>Resource name: "Tábuas de Carvalho Cinza"</summary>
		public const string RESOURCE_ASH_BOARD = "Tábuas de Carvalho Cinza";

		/// <summary>Resource name: "Tábuas de Ébano"</summary>
		public const string RESOURCE_EBONY_BOARD = "Tábuas de Ébano";

		/// <summary>Resource name: "Tábuas Élficas"</summary>
		public const string RESOURCE_ELVEN_BOARD = "Tábuas Élficas";

		/// <summary>Resource name: "Tábuas de Ipê-Amarelo"</summary>
		public const string RESOURCE_GOLDEN_OAK_BOARD = "Tábuas de Ipê-Amarelo";

		/// <summary>Resource name: "Tábuas de Cerejeira"</summary>
		public const string RESOURCE_CHERRY_BOARD = "Tábuas de Cerejeira";

		/// <summary>Resource name: "Tábuas de Pau-Brasil"</summary>
		public const string RESOURCE_ROSEWOOD_BOARD = "Tábuas de Pau-Brasil";

		/// <summary>Resource name: "Tábuas de Nogueira Branca"</summary>
		public const string RESOURCE_HICKORY_BOARD = "Tábuas de Nogueira Branca";

		/// <summary>Error: "Você não tem os itens necessários."</summary>
		public const string MSG_INSUFFICIENT_RESOURCES = "Você não tem os itens necessários.";

		#endregion

		#region Menu and System Messages

		/// <summary>Menu title: "MENU DE ARQUEIRIA E FLECHARIA"</summary>
		public const string MSG_GUMP_TITLE = "MENU DE ARQUEIRIA E FLECHARIA";

		/// <summary>Error: "Você quebrou sua ferramenta!"</summary>
		public const string MSG_TOOL_WORN_OUT = "Você quebrou sua ferramenta!";

		/// <summary>Error: "A ferramenta deve estar com você para usar."</summary>
		public const string MSG_TOOL_MUST_BE_ON_PERSON = "A ferramenta deve estar com você para usar.";

		/// <summary>Error: "Você falhou ao criar o item, e alguns de seus materiais foram perdidos."</summary>
		public const string MSG_FAILED_LOST_MATERIALS = "Você falhou ao criar o item, e alguns de seus materiais foram perdidos.";

		/// <summary>Error: "Você falhou ao criar o item, mas nenhum material foi perdido."</summary>
		public const string MSG_FAILED_NO_MATERIALS_LOST = "Você falhou ao criar o item, mas nenhum material foi perdido.";

		/// <summary>Warning: "Você mal conseguiu fazer este item. Sua qualidade está abaixo da média."</summary>
		public const string MSG_BARELY_MADE_ITEM = "Você mal conseguiu fazer este item. Sua qualidade está abaixo da média.";

		/// <summary>Success: "Você cria um item de qualidade excepcional e assina a sua marca nele."</summary>
		public const string MSG_EXCEPTIONAL_WITH_MARK = "Você cria um item de qualidade excepcional e assina a sua marca nele.";

		/// <summary>Success: "Você cria um item de qualidade excepcional."</summary>
		public const string MSG_EXCEPTIONAL_QUALITY = "Você cria um item de qualidade excepcional.";

		/// <summary>Success: "Você cria o item."</summary>
		public const string MSG_ITEM_CREATED = "Você cria o item.";

		#endregion
	}
}

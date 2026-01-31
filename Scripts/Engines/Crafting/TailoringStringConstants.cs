namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized string constants for Tailoring messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from DefTailoring.cs to improve maintainability and enable localization.
	/// </summary>
	public static class TailoringStringConstants
	{
		#region Craft Group Names

		/// <summary>Group name: "Máscara/Chapéu/Boné"</summary>
		public const string GROUP_HATS = "Máscara/Chapéu/Boné";

		/// <summary>Group name: "Robes/Camisas/Capas"</summary>
		public const string GROUP_CLOTHING = "Robes/Camisas/Capas";

		/// <summary>Group name: "Calças/Shorts"</summary>
		public const string GROUP_PANTS = "Calças/Shorts";

		/// <summary>Group name: "Calçados"</summary>
		public const string GROUP_FOOTWEAR = "Calçados";

		/// <summary>Group name: "Variados"</summary>
		public const string GROUP_MISC = "Variados";

		/// <summary>Group name: "Armadura de Couro"</summary>
		public const string GROUP_LEATHER_ARMOR = "Armadura de Couro";

		/// <summary>Group name: "Armadura de Ossos"</summary>
		public const string GROUP_BONE_ARMOR = "Armadura de Ossos";

		/// <summary>Group name: "Bolsas/Sacolas/Mochilas"</summary>
		public const string GROUP_BAGS = "Bolsas/Sacolas/Mochilas";

		#endregion

		#region Item Names - Hats

		/// <summary>Item name: "chapéu de pirata"</summary>
		public const string ITEM_PIRATE_HAT = "chapéu de pirata";

		/// <summary>Item name: "chapéu de bruxa"</summary>
		public const string ITEM_WITCH_HAT = "chapéu de bruxa";

		#endregion

		#region Item Names - Clothing

		/// <summary>Item name: "camisa de palhaço"</summary>
		public const string ITEM_FOOLS_COAT = "camisa de palhaço";

		#endregion

		#region Item Names - Pants

		/// <summary>Item name: "mini-saia"</summary>
		public const string ITEM_ROYAL_SKIRT = "mini-saia";

		#endregion

		#region Item Names - Footwear

		/// <summary>Item name: "sandálias de couro"</summary>
		public const string ITEM_LEATHER_SANDALS = "sandálias de couro";

		/// <summary>Item name: "sapatos de couro"</summary>
		public const string ITEM_LEATHER_SHOES = "sapatos de couro";

		/// <summary>Item name: "botas de couro"</summary>
		public const string ITEM_LEATHER_BOOTS = "botas de couro";

		/// <summary>Item name: "botas de cano alto"</summary>
		public const string ITEM_LEATHER_THIGH_BOOTS = "botas de cano alto";

		#endregion

		#region Item Names - Misc

		/// <summary>Item name: "pano para óleos"</summary>
		public const string ITEM_OIL_CLOTH = "pano para óleos";

		#endregion

		#region Item Names - Leather Armor

		/// <summary>Item name: "saia de couro-reforçado"</summary>
		public const string ITEM_STUDDED_SKIRT = "saia de couro-reforçado";

		#endregion

		#region Item Names - Bone Armor

		/// <summary>Item name: "elmo com chifres"</summary>
		public const string ITEM_ORC_HELM = "elmo com chifres";

		#endregion

		#region Item Names - Bags

		/// <summary>Item name: "sacola"</summary>
		public const string ITEM_BAG = "sacola";

		/// <summary>Item name: "sacola grande"</summary>
		public const string ITEM_LARGE_BAG = "sacola grande";

		/// <summary>Item name: "sacola gigante"</summary>
		public const string ITEM_GIANT_BAG = "sacola gigante";

		/// <summary>Item name: "mochila"</summary>
		public const string ITEM_BACKPACK = "mochila";

		/// <summary>Item name: "mochila reforçada"</summary>
		public const string ITEM_RUGGED_BACKPACK = "mochila reforçada";

		/// <summary>Item name: "bolsa mágica de minérios"</summary>
		public const string ITEM_MINERS_POUCH = "Bolsa Mágica de Minérios";

		/// <summary>Item name: "bolsa mágica de madeiras"</summary>
		public const string ITEM_LUMBERJACK_POUCH = "Bolsa Mágica de Madeiras";

		/// <summary>Item name: "bolsa mágica de alquimia"</summary>
		public const string ITEM_ALCHEMY_POUCH = "Bolsa Mágica de Alquimia";

		/// <summary>Item name: "Bolsa Mágica de Custura"</summary>
		public const string ITEM_TAILORING_POUCH = "Bolsa Mágica de Custura";

		#endregion

		#region Resource Names

		/// <summary>Resource name: "Tecido de Algodão"</summary>
		public const string RESOURCE_COTTON_CLOTH = "Tecido de Algodão";

		/// <summary>Error: "Você não tem tecido suficiente."</summary>
		public const string MSG_INSUFFICIENT_CLOTH = "Você não tem tecido suficiente.";

		/// <summary>Resource name: "Couro"</summary>
		public const string RESOURCE_LEATHER = "Couro";

		/// <summary>Error: "Você não tem couro suficiente."</summary>
		public const string MSG_INSUFFICIENT_LEATHER = "Você não tem couro suficiente.";

		/// <summary>Error: "Você não tem os itens necessários."</summary>
		public const string MSG_INSUFFICIENT_RESOURCES = "Você não tem os itens necessários.";

		/// <summary>Error: "Você não tem a habilidade necessária."</summary>
		public const string MSG_INSUFFICIENT_SKILL = "Você não tem a habilidade necessária.";

		/// <summary>Error: "Você não tem material de tecido suficiente."</summary>
		public const string MSG_NO_CLOTH_MATERIAL = "Você não tem material de tecido suficiente.";

		/// <summary>Error: "Você não pode trabalhar este tecido."</summary>
		public const string MSG_CANNOT_WORK_CLOTH = "Você não pode trabalhar este tecido.";

		/// <summary>Resource name: "Couro Golias"</summary>
		public const string RESOURCE_GOLIATH_LEATHER = "Couro Goliático";

		/// <summary>Error: "Você não tem couro golias suficiente."</summary>
		public const string MSG_INSUFFICIENT_GOLIATH_LEATHER = "Você não tem couro goliático suficiente.";

		/// <summary>Resource name: "Caveira Polida"</summary>
		public const string RESOURCE_POLISHED_SKULL = "Caveira Polida";

		/// <summary>Resource name: "Osso Polido"</summary>
		public const string RESOURCE_POLISHED_BONE = "Osso Polido";

		/// <summary>Resource name: "Couro ou Peles"</summary>
		public const string RESOURCE_LEATHER_OR_FURS = "Couro ou Peles";

		/// <summary>Resource name: "Lingotes de Platina"</summary>
		public const string RESOURCE_PLATINUM_INGOTS = "Lingotes de Platina";

		/// <summary>Resource name: "Tábuas de Pau-Brasil"</summary>
		public const string RESOURCE_ROSEWOOD_BOARDS = "Tábuas de Pau-Brasil";

		#endregion

		#region Menu and System Messages

		/// <summary>Menu title: "MENU DE ALFAIATARIA"</summary>
		public const string MSG_GUMP_TITLE = "MENU DE ALFAIATARIA";

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

		#region Item Names - Hats (PT-BR)

		/// <summary>Item name: "gorro"</summary>
		public const string ITEM_SKULL_CAP = "gorro";

		/// <summary>Item name: "bandana"</summary>
		public const string ITEM_BANDANA = "bandana";

		/// <summary>Item name: "chapéu flexível"</summary>
		public const string ITEM_FLOPPY_HAT = "chapéu flexível";

		/// <summary>Item name: "boné"</summary>
		public const string ITEM_CAP = "boné";

		/// <summary>Item name: "chapéu de aba larga"</summary>
		public const string ITEM_WIDE_BRIM_HAT = "chapéu de aba larga";

		/// <summary>Item name: "chapéu de palha"</summary>
		public const string ITEM_STRAW_HAT = "chapéu de palha";

		/// <summary>Item name: "chapéu de palha alto"</summary>
		public const string ITEM_TALL_STRAW_HAT = "chapéu de palha alto";

		/// <summary>Item name: "touca"</summary>
		public const string ITEM_BONNET = "touca";

		/// <summary>Item name: "chapéu com penas"</summary>
		public const string ITEM_FEATHERED_HAT = "chapéu com penas";

		/// <summary>Item name: "tricorne"</summary>
		public const string ITEM_TRICORNE_HAT = "tricorne";

		/// <summary>Item name: "chapéu de bobo"</summary>
		public const string ITEM_JESTER_HAT = "chapéu de bobo";

		/// <summary>Item name: "chapéu de mago"</summary>
		public const string ITEM_WIZARDS_HAT = "chapéu de mago";

		#endregion

		#region Item Names - Clothing (PT-BR)

		/// <summary>Item name: "gibão"</summary>
		public const string ITEM_DOUBLET = "gibão";

		/// <summary>Item name: "camisa"</summary>
		public const string ITEM_SHIRT = "camisa";

		/// <summary>Item name: "túnica"</summary>
		public const string ITEM_TUNIC = "túnica";

		/// <summary>Item name: "sobreveste"</summary>
		public const string ITEM_SURCOAT = "sobreveste";

		/// <summary>Item name: "vestido simples"</summary>
		public const string ITEM_PLAIN_DRESS = "vestido simples";

		/// <summary>Item name: "vestido elegante"</summary>
		public const string ITEM_FANCY_DRESS = "vestido elegante";

		/// <summary>Item name: "capa"</summary>
		public const string ITEM_CLOAK = "capa";

		/// <summary>Item name: "robe"</summary>
		public const string ITEM_ROBE = "robe";

		/// <summary>Item name: "camisa elegante"</summary>
		public const string ITEM_FANCY_SHIRT = "camisa elegante";

		#endregion

		#region Item Names - Pants (PT-BR)

		/// <summary>Item name: "calças curtas"</summary>
		public const string ITEM_SHORT_PANTS = "calças curtas";

		/// <summary>Item name: "calças longas"</summary>
		public const string ITEM_LONG_PANTS = "calças longas";

		/// <summary>Item name: "kilt"</summary>
		public const string ITEM_KILT = "kilt";

		/// <summary>Item name: "saia"</summary>
		public const string ITEM_SKIRT = "saia";

		#endregion

		#region Item Names - Footwear (PT-BR)

		/// <summary>Item name: "sandálias"</summary>
		public const string ITEM_SANDALS = "sandálias";

		/// <summary>Item name: "sapatos"</summary>
		public const string ITEM_SHOES = "sapatos";

		/// <summary>Item name: "botas"</summary>
		public const string ITEM_BOOTS = "botas";

		#endregion

		#region Item Names - Misc (PT-BR)

		/// <summary>Item name: "faixa corporal"</summary>
		public const string ITEM_BODY_SASH = "faixa corporal";

		/// <summary>Item name: "avental (meio)"</summary>
		public const string ITEM_HALF_APRON = "avental (meio)";

		/// <summary>Item name: "avental (completo)"</summary>
		public const string ITEM_FULL_APRON = "avental (completo)";

		#endregion

		#region Item Names - Leather Armor (PT-BR)

		/// <summary>Item name: "gorro de couro"</summary>
		public const string ITEM_LEATHER_CAP = "gorro de couro";

		/// <summary>Item name: "gorgel de couro"</summary>
		public const string ITEM_LEATHER_GORGET = "gorgel de couro";

		/// <summary>Item name: "luvas de couro"</summary>
		public const string ITEM_LEATHER_GLOVES = "luvas de couro";

		/// <summary>Item name: "ombreiras de couro"</summary>
		public const string ITEM_LEATHER_ARMS = "ombreiras de couro";

		/// <summary>Item name: "calças de couro"</summary>
		public const string ITEM_LEATHER_LEGS = "calças de couro";

		/// <summary>Item name: "peitoral de couro"</summary>
		public const string ITEM_LEATHER_CHEST = "peitoral de couro";

		/// <summary>Item name: "saia de couro"</summary>
		public const string ITEM_LEATHER_SKIRT = "saia de couro";

		/// <summary>Item name: "shorts de couro"</summary>
		public const string ITEM_LEATHER_SHORTS = "shorts de couro";

		/// <summary>Item name: "ombreiras de bustiê de couro"</summary>
		public const string ITEM_LEATHER_BUSTIER_ARMS = "ombreiras de bustiê de couro";

		/// <summary>Item name: "peitoral feminino de couro"</summary>
		public const string ITEM_FEMALE_LEATHER_CHEST = "peitoral feminino de couro";

		/// <summary>Item name: "gorgel de couro-reforçado"</summary>
		public const string ITEM_STUDDED_GORGET = "gorgel de couro-reforçado";

		/// <summary>Item name: "luvas de couro-reforçado"</summary>
		public const string ITEM_STUDDED_GLOVES = "luvas de couro-reforçado";

		/// <summary>Item name: "ombreiras de couro-reforçado"</summary>
		public const string ITEM_STUDDED_ARMS = "ombreiras de couro-reforçado";

		/// <summary>Item name: "calças de couro-reforçado"</summary>
		public const string ITEM_STUDDED_LEGS = "calças de couro-reforçado";

		/// <summary>Item name: "peitoral de couro-reforçado"</summary>
		public const string ITEM_STUDDED_CHEST = "peitoral de couro-reforçado";

		/// <summary>Item name: "ombreiras de bustiê de couro-reforçado"</summary>
		public const string ITEM_STUDDED_BUSTIER_ARMS = "ombreiras de bustiê de couro-reforçado";

		/// <summary>Item name: "peitoral feminino de couro-reforçado"</summary>
		public const string ITEM_FEMALE_STUDDED_CHEST = "peitoral feminino de couro-reforçado";

		#endregion

		#region Item Names - Bone Armor (PT-BR)

		/// <summary>Item name: "elmo de osso"</summary>
		public const string ITEM_BONE_HELM = "elmo de osso";

		/// <summary>Item name: "luvas de osso"</summary>
		public const string ITEM_BONE_GLOVES = "luvas de osso";

		/// <summary>Item name: "ombreiras de osso"</summary>
		public const string ITEM_BONE_ARMS = "ombreiras de osso";

		/// <summary>Item name: "calças de osso"</summary>
		public const string ITEM_BONE_LEGS = "calças de osso";

		/// <summary>Item name: "peitoral de osso"</summary>
		public const string ITEM_BONE_CHEST = "peitoral de osso";

		#endregion

		#region Cloth Type Names (PT-BR)

		/// <summary>Cloth type: "Tecido de Algodão"</summary>
		public const string CLOTH_COTTON = "Tecido de Algodão";

		/// <summary>Cloth type: "Tecido de Lã"</summary>
		public const string CLOTH_WOOL = "Tecido de Lã";

		/// <summary>Cloth type: "Tecido de Linho"</summary>
		public const string CLOTH_FLAX = "Tecido de Linho";

		/// <summary>Cloth type: "Tecido de Seda"</summary>
		public const string CLOTH_SILK = "Tecido de Seda";

		/// <summary>Cloth type: "Tecido de Poliéster"</summary>
		public const string CLOTH_POLIESTER = "Tecido de Poliéster";

		#endregion

		#region Leather Type Names (PT-BR)

		/// <summary>Leather type: "Couro"</summary>
		public const string LEATHER_REGULAR = "Couro Comum";

		/// <summary>Leather type: "Couro Espinhoso"</summary>
		public const string LEATHER_SPINED = "Couro Spined";

		/// <summary>Leather type: "Couro com Chifres"</summary>
		public const string LEATHER_HORNED = "Couro Horned";

		/// <summary>Leather type: "Couro com Espinhos"</summary>
		public const string LEATHER_BARBED = "Couro Barbed";

		/// <summary>Leather type: "Couro Vulcanico"</summary>
		public const string LEATHER_VOLCANIC = "Couro Vulcanico";

		#endregion
	}
}

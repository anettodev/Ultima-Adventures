namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized string constants for Blacksmithy-related messages and labels.
	/// Extracted from DefBlacksmithy.cs to improve maintainability and enable localization.
	/// </summary>
	public static class BlacksmithyStringConstants
	{
		#region Craft Group Titles

		/// <summary>Title for Chainmail and Ringmail group</summary>
		public const string GROUP_CHAIN_RING = "Loriga & Malhas";

		/// <summary>Title for Platemail group</summary>
		public const string GROUP_PLATEMAIL = "Armadura de Metal";

		/// <summary>Title for Royal armor group</summary>
		public const string GROUP_ROYAL = "Armadura Real";

		/// <summary>Title for Dragon Scale armor group</summary>
		public const string GROUP_SCALEMAIL = "Armadura de Escamas";

		/// <summary>Title for Helmets group</summary>
		public const string GROUP_HELMETS = "Elmos & Capacetes";

		/// <summary>Title for Shields group</summary>
		public const string GROUP_SHIELDS = "Escudos";

		/// <summary>Title for Blades group</summary>
		public const string GROUP_BLADES = "Lâminas e Espadas";

		/// <summary>Title for Axes group</summary>
		public const string GROUP_AXES = "Machados";

		/// <summary>Title for Pole Arms group</summary>
		public const string GROUP_POLE_ARMS = "Lanças & Hastes";

		/// <summary>Title for Bashing weapons group</summary>
		public const string GROUP_BASHING = "Macas e Martelos";

		#endregion

		#region Item Names - Ringmail

		/// <summary>Ringmail gloves name</summary>
		public const string ITEM_RINGMAIL_GLOVES = "Luvas de Loriga";

		/// <summary>Ringmail legs name</summary>
		public const string ITEM_RINGMAIL_LEGS = "Calça de Loriga";

		/// <summary>Ringmail arms name</summary>
		public const string ITEM_RINGMAIL_ARMS = "Ombreiras de Loriga";

		/// <summary>Ringmail chest name</summary>
		public const string ITEM_RINGMAIL_CHEST = "Peitoral de Loriga";

		#endregion

		#region Item Names - Chainmail

		/// <summary>Chainmail coif name</summary>
		public const string ITEM_CHAIN_COIF = "Coifa de Malha";

		/// <summary>Chainmail legs name</summary>
		public const string ITEM_CHAIN_LEGS = "Calça de Malha";

		/// <summary>Chainmail chest name</summary>
		public const string ITEM_CHAIN_CHEST = "Tunica de Malha";

		#endregion

		#region Item Names - Platemail

		/// <summary>Plate arms name</summary>
		public const string ITEM_PLATE_ARMS = "Ombreiras de Metal";

		/// <summary>Plate gloves name</summary>
		public const string ITEM_PLATE_GLOVES = "Luvas de Metal";

		/// <summary>Plate gorget name</summary>
		public const string ITEM_PLATE_GORGET = "Gorgel de Metal";

		/// <summary>Plate legs name</summary>
		public const string ITEM_PLATE_LEGS = "Calças de Metal";

		/// <summary>Plate chest name</summary>
		public const string ITEM_PLATE_CHEST = "Peitoral de Metal";

		/// <summary>Female plate chest name</summary>
		public const string ITEM_FEMALE_PLATE_CHEST = "Peitoral Feminino de Metal";

		/// <summary>Horse armor name</summary>
		public const string ITEM_HORSE_ARMOR = "Armadura para Cavalos";

		#endregion

		#region Item Names - Royal

		/// <summary>Royal gloves name</summary>
		public const string ITEM_ROYAL_GLOVES = "Braçadeiras Real";

		/// <summary>Royal gorget name</summary>
		public const string ITEM_ROYAL_GORGET = "Gorgel Real";

		/// <summary>Royal helm name</summary>
		public const string ITEM_ROYAL_HELM = "Elmo Real";

		/// <summary>Royal legs name</summary>
		public const string ITEM_ROYAL_LEGS = "Calças Real";

		/// <summary>Royal boots name</summary>
		public const string ITEM_ROYAL_BOOTS = "Botas Real";

		/// <summary>Royal arms name</summary>
		public const string ITEM_ROYAL_ARMS = "Ombreira Real";

		/// <summary>Royal chest name</summary>
		public const string ITEM_ROYAL_CHEST = "Tunica Real";

		#endregion

		#region Item Names - Dragon Scale

		/// <summary>Dragon gloves name</summary>
		public const string ITEM_DRAGON_GLOVES = "Luvas de Escamas";

		/// <summary>Dragon helm name</summary>
		public const string ITEM_DRAGON_HELM = "Elmo de Escamas";

		/// <summary>Dragon legs name</summary>
		public const string ITEM_DRAGON_LEGS = "Calças de Escamas";

		/// <summary>Dragon arms name</summary>
		public const string ITEM_DRAGON_ARMS = "Ombreiras de Escamas";

		/// <summary>Dragon chest name</summary>
		public const string ITEM_DRAGON_CHEST = "Tunica de Escamas";

		#endregion

		#region Item Names - Helmets

		/// <summary>Bascinet name</summary>
		public const string ITEM_BASCINET = "Bacinete ";

		/// <summary>Close helm name</summary>
		public const string ITEM_CLOSE_HELM = "Elmo Fechado";

		/// <summary>Helmet name</summary>
		public const string ITEM_HELMET = "Elmo Comum";

		/// <summary>Norse helm name</summary>
		public const string ITEM_NORSE_HELM = "Elmo Nórdico";

		/// <summary>Plate helm name</summary>
		public const string ITEM_PLATE_HELM = "Elmo Completo";

		/// <summary>Dread helm name</summary>
		public const string ITEM_DREAD_HELM = "Elmo de Chifres";

		#endregion

		#region Item Names - Shields

		/// <summary>Buckler name</summary>
		public const string ITEM_BUCKLER = "Buckler";

		/// <summary>Metal shield name</summary>
		public const string ITEM_METAL_SHIELD = "Escudo Redondo";

		/// <summary>Wooden kite shield name</summary>
		public const string ITEM_WOODEN_KITE_SHIELD = "Escudo Kite";

		/// <summary>Bronze shield name</summary>
		public const string ITEM_BRONZE_SHIELD = "Escudo Bizantino";

		/// <summary>Metal kite shield name</summary>
		public const string ITEM_METAL_KITE_SHIELD = "Escudo Heater";

		/// <summary>Heater shield name</summary>
		public const string ITEM_HEATER_SHIELD = "Escudo Corporal";

		/// <summary>Chaos shield name</summary>
		public const string ITEM_CHAOS_SHIELD = "Escudo do Caos";

		/// <summary>Order shield name</summary>
		public const string ITEM_ORDER_SHIELD = "Escudo da Ordem";

		#endregion

		#region Item Names - Blades

		/// <summary>Dagger name</summary>
		public const string ITEM_DAGGER = "Adaga";

		/// <summary>Cutlass name</summary>
		public const string ITEM_CUTLASS = "Cutelo";

		/// <summary>Scimitar name</summary>
		public const string ITEM_SCIMITAR = "Cimitarra";

		/// <summary>Kryss name</summary>
		public const string ITEM_KRYSS = "Kopesh";

		/// <summary>Katana name</summary>
		public const string ITEM_KATANA = "Katana";

		/// <summary>Viking sword name</summary>
		public const string ITEM_VIKING_SWORD = "Espada Bastarda";

		/// <summary>Broadsword name</summary>
		public const string ITEM_BROADSWORD = "Espada Larga";

		/// <summary>Longsword name</summary>
		public const string ITEM_LONGSWORD = "Espada Longa";

		#endregion

		#region Item Names - Axes

		/// <summary>Hatchet name</summary>
		public const string ITEM_HATCHET = "Machadinha";

		/// <summary>Lumber axe name</summary>
		public const string ITEM_LUMBER_AXE = "Machado de Lenhador";

		/// <summary>Battle axe name</summary>
		public const string ITEM_BATTLE_AXE = "Machado de Batalha";

		/// <summary>Axe name</summary>
		public const string ITEM_AXE = "Machado Comum";

		/// <summary>Two-handed axe name</summary>
		public const string ITEM_TWO_HANDED_AXE = "Machado de Duas Mãos";

		/// <summary>War axe name</summary>
		public const string ITEM_WAR_AXE = "Machado de Guerra";

		/// <summary>Double axe name</summary>
		public const string ITEM_DOUBLE_AXE = "Machado Duplo";

		/// <summary>Executioner's axe name</summary>
		public const string ITEM_EXECUTIONERS_AXE = "Machado de Carrasco";

		/// <summary>Large battle axe name</summary>
		public const string ITEM_LARGE_BATTLE_AXE = "Machado Grande de Batalha";

		#endregion

		#region Item Names - Pole Arms

		/// <summary>Harpoon name</summary>
		public const string ITEM_HARPOON = "Arpão";

		/// <summary>Short spear name</summary>
		public const string ITEM_SHORT_SPEAR = "Lança Pequena";

		/// <summary>Spear name</summary>
		public const string ITEM_SPEAR = "Lança";

		/// <summary>Pike name</summary>
		public const string ITEM_PIKE = "Pique";

		/// <summary>War fork name</summary>
		public const string ITEM_WAR_FORK = "Garfo de Guerra";

		/// <summary>Bardiche name</summary>
		public const string ITEM_BARDICHE = "Bardiche";

		/// <summary>Halberd name</summary>
		public const string ITEM_HALBERD = "Alabarda";

		#endregion

		#region Item Names - Bashing

		/// <summary>Hammer pick name</summary>
		public const string ITEM_HAMMER_PICK = "Martelo Picareta";

		/// <summary>Mace name</summary>
		public const string ITEM_MACE = "Maca";

		/// <summary>Maul name</summary>
		public const string ITEM_MAUL = "Maul";

		/// <summary>War mace name</summary>
		public const string ITEM_WAR_MACE = "Maca de Guerra";

		/// <summary>War hammer name</summary>
		public const string ITEM_WAR_HAMMER = "Martelo de Guerra";

		#endregion

		#region Resource Names

		/// <summary>Dragon scales resource name</summary>
		public const string RESOURCE_DRAGON_SCALES = "Escamas Reptilianas";

		/// <summary>Platinum ingots resource name</summary>
		public const string RESOURCE_PLATINUM_INGOTS = "Lingotes de Platina";

		/// <summary>Resource name: "Lingotes de Ferro"</summary>
		public const string RESOURCE_IRON_INGOTS = "Lingotes de Ferro";

		/// <summary>Error: "Você não tem lingotes suficientes."</summary>
		public const string MSG_INSUFFICIENT_INGOTS = "Você não tem lingotes suficientes.";

		/// <summary>Error: "Você não tem os itens necessários."</summary>
		public const string MSG_INSUFFICIENT_RESOURCES = "Você não tem os itens necessários.";

		/// <summary>Error: "Você não tem escamas reptilianas suficientes."</summary>
		public const string MSG_INSUFFICIENT_DRAGON_SCALES = "Você não tem escamas reptilianas suficientes.";

		#endregion

		#region Menu and System Messages

		/// <summary>Menu title: "MENU DE FERRARIA"</summary>
		public const string MSG_GUMP_TITLE = "MENU DE FERRARIA";

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

		#region Ingot Names

		/// <summary>Iron ingot name</summary>
		public const string INGOT_IRON = "Ferro";

		/// <summary>Dull copper ingot name</summary>
		public const string INGOT_DULL_COPPER = "Cobre Rústico";

		/// <summary>Shadow iron ingot name</summary>
		public const string INGOT_SHADOW_IRON = "Ferro Sombrio";

		/// <summary>Copper ingot name</summary>
		public const string INGOT_COPPER = "Cobre";

		/// <summary>Bronze ingot name</summary>
		public const string INGOT_BRONZE = "Bronze";

		/// <summary>Platinum ingot name</summary>
		public const string INGOT_PLATINUM = "Platina";

		/// <summary>Gold ingot name</summary>
		public const string INGOT_GOLD = "Dourado";

		/// <summary>Agapite ingot name</summary>
		public const string INGOT_AGAPITE = "Agapite";

		/// <summary>Verite ingot name</summary>
		public const string INGOT_VERITE = "Verite";

		/// <summary>Valorite ingot name</summary>
		public const string INGOT_VALORITE = "Valorite";

		/// <summary>Titanium ingot name</summary>
		public const string INGOT_TITANIUM = "Titânio";

		/// <summary>Rosenium ingot name</summary>
		public const string INGOT_ROSENIUM = "Rosenium";

		#endregion

		#region Error Messages

		/// <summary>Error: "Você deve usar a ferramenta equipada."</summary>
		public const string MSG_MUST_USE_EQUIPPED_TOOL = "Você deve usar a ferramenta equipada.";

		/// <summary>Error: "Você deve estar perto de uma bigorna e uma forja para fazer isso."</summary>
		public const string MSG_MUST_BE_NEAR_ANVIL_AND_FORGE = "Você deve estar perto de uma bigorna e uma forja para fazer isso.";

		/// <summary>Error: "Você não tem o recurso necessário."</summary>
		public const string MSG_MISSING_RESOURCE = "Você não tem o recurso necessário.";

		/// <summary>Error: "Você não tem escamas reptilianas suficientes."</summary>
		public const string MSG_SCALE_SKILL_ERROR = "Você não tem escamas reptilianas suficientes.";

		/// <summary>Error: "Você não tem madeira suficiente para fazer isso."</summary>
		public const string MSG_HARPOON_RESOURCE_ERROR = "Você não tem madeira suficiente para fazer isso.";

		/// <summary>Error: "Você não tem o sub-recurso necessário."</summary>
		public const string MSG_SUB_RESOURCE_ERROR = "Você não tem o sub-recurso necessário.";

		/// <summary>Error: "Você não tem habilidade suficiente para trabalhar com este sub-recurso."</summary>
		public const string MSG_SUB_RESOURCE_SKILL_ERROR = "Você não tem habilidade suficiente para trabalhar com este sub-recurso.";

		/// <summary>Resource name: "Lingote de Ferro"</summary>
		public const string RESOURCE_IRON_INGOT = "Lingote de Ferro";

		#endregion
	}
}


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

		#endregion

		#region Ingot Names

		/// <summary>Iron ingot name</summary>
		public const string INGOT_IRON = "Lingotes de Ferro";

		/// <summary>Dull copper ingot name</summary>
		public const string INGOT_DULL_COPPER = "Lingotes de Cobre Fosco";

		/// <summary>Shadow iron ingot name</summary>
		public const string INGOT_SHADOW_IRON = "Lingotes de Ferro Sombrio";

		/// <summary>Copper ingot name</summary>
		public const string INGOT_COPPER = "Lingotes de Cobre";

		/// <summary>Bronze ingot name</summary>
		public const string INGOT_BRONZE = "Lingotes de Bronze";

		/// <summary>Platinum ingot name</summary>
		public const string INGOT_PLATINUM = "Lingotes de Platina";

		/// <summary>Gold ingot name</summary>
		public const string INGOT_GOLD = "Lingotes de Ouro";

		/// <summary>Agapite ingot name</summary>
		public const string INGOT_AGAPITE = "Lingotes de Agapite";

		/// <summary>Verite ingot name</summary>
		public const string INGOT_VERITE = "Lingotes de Verite";

		/// <summary>Valorite ingot name</summary>
		public const string INGOT_VALORITE = "Lingotes de Valorite";

		/// <summary>Titanium ingot name</summary>
		public const string INGOT_TITANIUM = "Lingotes de Titânio";

		/// <summary>Rosenium ingot name</summary>
		public const string INGOT_ROSENIUM = "Lingotes de Rosenium";

		#endregion
	}
}


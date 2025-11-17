using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Commands;
using Server.Targeting;
using Server.Items;
using Server.Multis;

namespace Server.Gumps
{
	/// <summary>
	/// Gump de menu para testar todos os gumps disponíveis
	/// </summary>
	public class GumpTestMenu : Gump
	{
		/// <summary>
		/// Representa uma entrada de gump para teste
		/// </summary>
		private class GumpEntry
		{
			public string Name { get; set; }
			public int ButtonID { get; set; }

			public GumpEntry(string name, int buttonID)
			{
				Name = name;
				ButtonID = buttonID;
			}
		}

		/// <summary>
		/// Representa uma categoria de gumps
		/// </summary>
		private class GumpCategory
		{
			public string Name { get; set; }
			public GumpEntry[] Entries { get; set; }

			public GumpCategory(string name, params GumpEntry[] entries)
			{
				Name = name;
				Entries = entries;
			}
		}

		/// <summary>
		/// Cores para cada categoria
		/// </summary>
		private static string[] CategoryColors = new string[]
		{
			"#FF6B6B", // Ressurreição - Vermelho claro
			"#4ECDC4", // Outros / Comuns - Turquesa
			"#45B7D1", // Utilitários - Azul
			"#FFA07A", // Jogadores Novos - Salmão
			"#98D8C8", // Casas - Verde água
			"#F7DC6F", // Itens e Vendors - Amarelo
			"#9B59B6", // UOE Editor - Roxo
			"#FF6B9D"  // Tests - Rosa
		};

		/// <summary>
		/// Categorias de gumps disponíveis
		/// </summary>
		private static GumpCategory[] Categories = new GumpCategory[]
		{
			new GumpCategory("Ressurreição",
				new GumpEntry("ResurrectGump (Player - Sem Preço)", 1),
				new GumpEntry("ResurrectGump (Player - Com Preço: 5000)", 2),
				new GumpEntry("PetResurrectGump (Pet Resurrection)", 3)
			),
			new GumpCategory("Outros / Comuns",
				new GumpEntry("AddGump (Add Menu)", 4),
				new GumpEntry("SkillsGump (Skills Menu)", 5),
				new GumpEntry("WhoGump (Who List)", 6),
				new GumpEntry("TithingGump (Tithing Menu)", 7),
				new GumpEntry("PolymorphGump (Polymorph Menu)", 8),
				new GumpEntry("RunebookGump (Runebook Menu)", 9)
			),
			new GumpCategory("Utilitários",
				new GumpEntry("WarningGump (Warning Dialog)", 10),
				new GumpEntry("NoticeGump (Notice Dialog)", 11),
				new GumpEntry("BaseConfirmGump (Confirm Dialog)", 12)
			),
			new GumpCategory("Jogadores Novos",
				new GumpEntry("YoungDungeonWarning (Dungeon Warning)", 13),
				new GumpEntry("YoungDeathNotice (Death Notice)", 14),
				new GumpEntry("RenounceYoungGump (Renounce Young)", 15)
			),
			new GumpCategory("Casas",
				new GumpEntry("HouseGump (House Menu)", 16),
				new GumpEntry("HouseGumpAOS (House Menu AOS)", 17),
				new GumpEntry("HouseDemolishGump (Demolish House)", 18),
				new GumpEntry("HouseTransferGump (Transfer House)", 19),
				new GumpEntry("ViewHousesGump (View Houses)", 20)
			),
			new GumpCategory("Itens e Vendors",
				new GumpEntry("HeritageTokenGump (Heritage Token)", 21),
				new GumpEntry("HonorSelf (Honor Self)", 22),
				new GumpEntry("ConfirmBreakCrystalGump (Break Crystal)", 23),
				new GumpEntry("PlayerVendorBuyGump (Vendor Buy)", 24),
				new GumpEntry("PlayerVendorOwnerGump (Vendor Owner)", 25),
				new GumpEntry("NewPlayerVendorOwnerGump (New Vendor Owner)", 26),
				new GumpEntry("PlayerVendorCustomizeGump (Vendor Customize)", 27),
				new GumpEntry("NewPlayerVendorCustomizeGump (New Vendor Customize)", 28),
				new GumpEntry("ReclaimVendorGump (Reclaim Vendor)", 29),
				new GumpEntry("VendorInventoryGump (Vendor Inventory)", 30)
			),
			new GumpCategory("UOE Editor",
				new GumpEntry("MainUOE (Main Menu)", 31),
				new GumpEntry("SubUOE (Sub Menu)", 32),
				new GumpEntry("AddUOE (Add Tile)", 33),
				new GumpEntry("AltUOE (Altitude)", 34),
				new GumpEntry("CirUOE (Circle)", 35),
				new GumpEntry("DelUOE (Delete)", 36),
				new GumpEntry("GridUOE (Grid)", 37),
				new GumpEntry("GumpSelUOE (Gump Selection)", 38),
				new GumpEntry("HelpUOE (Help Menu)", 39),
				new GumpEntry("HueUOE (Hue)", 40),
				new GumpEntry("InfoUOE (Info)", 41),
				new GumpEntry("ListUOE (ID List)", 42),
				new GumpEntry("MoveUOE (Move)", 43),
				new GumpEntry("MultiUOE (Multi)", 44),
				new GumpEntry("PickUOE (Pick Background)", 45),
				new GumpEntry("PosUOE (Position)", 46),
				new GumpEntry("ResetUOE (Reset)", 47),
				new GumpEntry("RndUOE (Random)", 48),
				new GumpEntry("SetIdUOE (Set ID)", 49),
				new GumpEntry("SetLocUOE (Set Location)", 50),
				new GumpEntry("SettingUOE (Settings)", 51)
			),
			new GumpCategory("Tests",
				new GumpEntry("MageryItemsGump (Magery Items)", 52)
			)
		};

		private Mobile m_From;
		private int m_SelectedCategory;

		/// <summary>
		/// Registra o comando GM para abrir o menu de gumps
		/// </summary>
		public static void Initialize()
		{
			CommandSystem.Register("TestGumps", AccessLevel.GameMaster, new CommandEventHandler(TestGumps_OnCommand));
			CommandSystem.Register("Gumps", AccessLevel.GameMaster, new CommandEventHandler(TestGumps_OnCommand));
		}

		[Usage("TestGumps ou [Gumps")]
		[Description("Abre um menu com todos os gumps disponíveis para teste")]
		private static void TestGumps_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;
			from.CloseGump(typeof(GumpTestMenu));
			from.SendGump(new GumpTestMenu(from, 0));
		}

		public GumpTestMenu(Mobile from) : this(from, 0)
		{
		}

		public GumpTestMenu(Mobile from, int selectedCategory) : base(50, 50)
		{
			m_From = from;
			m_SelectedCategory = selectedCategory;

			from.CloseGump(typeof(GumpTestMenu));

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage(0);

			// Background - aumentado para acomodar mais categorias
			AddBackground(0, 0, 585, 393, 5054);
			AddBackground(195, 36, 387, 275, 3000);

			// Title - centralizado no container (585 de largura, título centralizado)
			AddHtml(0, 0, 585, 18, @"<BODY><BASEFONT Color=#000000><CENTER>MENU DE TESTE DE GUMPS</CENTER></BASEFONT></BODY>", false, false);

			// Close button (X) no topo direito, movido -10px no eixo X (de 565 para 555)
			AddButton(555, 0, 4017, 4019, 0, GumpButtonType.Reply, 0); // 4017/4019 são os IDs do botão X

			// Lista de categorias na esquerda - altura aumentada para não truncar labels
			int y = 35;
			int maxY = 400; // Altura máxima aumentada para não truncar categorias
			for (int i = 0; i < Categories.Length; i++)
			{
				GumpCategory cat = Categories[i];
				// Usar cor específica da categoria, destacar a selecionada com cor mais forte
				string color = CategoryColors[i];
				if (i == m_SelectedCategory)
				{
					// Categoria selecionada: usar cor mais brilhante (adicionar brilho)
					color = CategoryColors[i];
				}
				AddHtml(5, y, 150, 30, String.Format(@"<BODY><BASEFONT Color={0}>{1}</BASEFONT></BODY>", color, cat.Name), false, false);
				// Primeira página da categoria (pode ter múltiplas páginas)
				int firstPageOfCategory = (i * 100) + 1;
				AddButton(155, y, 4005, 4007, 0, GumpButtonType.Page, firstPageOfCategory);
				y += 30; // Aumentado de 25 para 30 para dar mais espaço
				
				// Se passar do limite, parar
				if (y > maxY)
					break;
			}

			// Páginas para cada categoria com paginação
			for (int i = 0; i < Categories.Length; i++)
			{
				GumpCategory cat = Categories[i];
				int entriesPerPage = 8; // Número de gumps por página
				int totalPages = (cat.Entries.Length + entriesPerPage - 1) / entriesPerPage;

				// Criar páginas para esta categoria
				for (int page = 0; page < totalPages; page++)
				{
					int pageNumber = (i * 100) + page + 1; // Usar números únicos para cada página
					AddPage(pageNumber);

					// Título da categoria - usar cor da categoria (visível em todas as páginas)
					string categoryColor = CategoryColors[i];
					AddHtml(200, 40, 380, 20, String.Format(@"<BODY><BASEFONT Color={0}><BIG>{1}</BIG></BASEFONT></BODY>", categoryColor, cat.Name), false, false);

					// Lista de gumps da categoria para esta página
					int startIndex = page * entriesPerPage;
					int endIndex = Math.Min(startIndex + entriesPerPage, cat.Entries.Length);
					
					y = 70;
					for (int c = startIndex; c < endIndex; c++)
					{
						GumpEntry entry = cat.Entries[c];
						
						AddButton(200, y, 4005, 4007, entry.ButtonID, GumpButtonType.Reply, pageNumber);
						AddHtml(235, y + 2, 340, 20, String.Format(@"<BODY><BASEFONT Color=#000000>{0}</BASEFONT></BODY>", entry.Name), false, false);
						
						y += 30;
					}

					// Botões de paginação - movidos 50px mais no eixo Y (de 300 para 350)
					if (totalPages > 1)
					{
						// Botão Back
						if (page > 0)
						{
							AddButton(200, 350, 4014, 4016, 0, GumpButtonType.Page, (i * 100) + page);
							AddHtml(235, 352, 100, 20, @"<BODY><BASEFONT Color=#FFFFFF>Anterior</BASEFONT></BODY>", false, false);
						}

						// Indicador de página
						AddHtml(300, 352, 100, 20, String.Format(@"<BODY><BASEFONT Color=#FFFFFF>Página {0}/{1}</BASEFONT></BODY>", page + 1, totalPages), false, false);

						// Botão Next
						if (page < totalPages - 1)
						{
							AddButton(450, 350, 4005, 4007, 0, GumpButtonType.Page, (i * 100) + page + 2);
							AddHtml(485, 352, 100, 20, @"<BODY><BASEFONT Color=#FFFFFF>Próxima</BASEFONT></BODY>", false, false);
						}
					}
				}
			}
		}

		public override void OnResponse(NetState state, RelayInfo info)
		{
			Mobile from = state.Mobile;

			if (info.ButtonID == 0) // Close
				return;

			switch (info.ButtonID)
			{
				case 1: // ResurrectGump (sem preço)
					from.SendGump(new ResurrectGump(from, from, ResurrectMessage.Generic, false, 0.0));
					from.SendMessage("Gump de ressurreição (sem preço) aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 2: // ResurrectGump (com preço)
					from.SendGump(new ResurrectGump(from, from, 5000));
					from.SendMessage("Gump de ressurreição (com preço: 5000) aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 3: // PetResurrectGump
					from.SendMessage("Selecione um pet ou pressione ESC para criar um pet de teste.");
					from.Target = new TestPetTargetForMenu(from, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 4: // AddGump
					from.SendGump(new AddGump(from, "", 0, new Type[0], false));
					from.SendMessage("AddGump aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 5: // SkillsGump
					from.SendGump(new SkillsGump(from, from));
					from.SendMessage("SkillsGump aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 6: // WhoGump
					from.SendGump(new WhoGump(from, ""));
					from.SendMessage("WhoGump aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 7: // TithingGump
					from.SendGump(new TithingGump(from, 0));
					from.SendMessage("TithingGump aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 8: // PolymorphGump
					// PolymorphGump precisa de um Item (scroll) como parâmetro
					// Criamos um SpellScroll temporário para teste (usando Polymorph spell ID e item ID padrão)
					Server.Items.SpellScroll testScroll = new Server.Items.SpellScroll(0x1F, 0x1F2D); // Polymorph spell
					testScroll.MoveToWorld(from.Location, from.Map);
					from.SendGump(new PolymorphGump(from, testScroll));
					from.SendMessage("PolymorphGump aberto. O scroll será deletado quando você fechar o gump.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 9: // RunebookGump
					// Cria um Runebook temporário para teste
					Server.Items.Runebook testBook = new Server.Items.Runebook();
					testBook.MoveToWorld(from.Location, from.Map);
					from.SendGump(new RunebookGump(from, testBook));
					from.SendMessage("Runebook de teste criado e gump aberto. O Runebook será deletado quando você fechar o gump.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 10: // WarningGump
					from.SendGump(new WarningGump(1060635, 30720, "Este é um exemplo de WarningGump. Use este gump para avisos importantes que requerem confirmação do jogador.", 0xFFC000, 420, 280, new WarningGumpCallback(OnWarningGumpResponse), null));
					from.SendMessage("WarningGump aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 11: // NoticeGump
					from.SendGump(new NoticeGump(1060636, 30720, "Este é um exemplo de NoticeGump. Use este gump para notificações simples que apenas precisam ser lidas.", 0xFFC000, 420, 280, new NoticeGumpCallback(OnNoticeGumpResponse), null));
					from.SendMessage("NoticeGump aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 12: // BaseConfirmGump
					from.SendGump(new TestConfirmGump());
					from.SendMessage("BaseConfirmGump aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 13: // YoungDungeonWarning
					from.SendGump(new YoungDungeonWarning());
					from.SendMessage("YoungDungeonWarning aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 14: // YoungDeathNotice
					from.SendGump(new YoungDeathNotice());
					from.SendMessage("YoungDeathNotice aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 15: // RenounceYoungGump
					from.SendGump(new RenounceYoungGump());
					from.SendMessage("RenounceYoungGump aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 16: // HouseGump
					from.SendMessage("Selecione uma casa para abrir o HouseGump.");
					from.Target = new HouseTargetForMenu(from, 16, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 17: // HouseGumpAOS
					from.SendMessage("Selecione uma casa para abrir o HouseGumpAOS.");
					from.Target = new HouseTargetForMenu(from, 17, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 18: // HouseDemolishGump
					from.SendMessage("Selecione uma casa para abrir o HouseDemolishGump.");
					from.Target = new HouseTargetForMenu(from, 18, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 19: // HouseTransferGump
					from.SendMessage("Selecione uma casa para abrir o HouseTransferGump.");
					from.Target = new HouseTransferTargetForMenu(from, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 20: // ViewHousesGump
					from.SendMessage("Selecione um jogador para ver suas casas.");
					from.Target = new ViewHousesTargetForMenu(from, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 21: // HeritageTokenGump
					Server.Items.HeritageToken testToken = new Server.Items.HeritageToken();
					testToken.MoveToWorld(from.Location, from.Map);
					from.SendGump(new HeritageTokenGump(testToken));
					from.SendMessage("HeritageTokenGump aberto. O token de teste será deletado quando você fechar o gump.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 22: // HonorSelf
					if (from is Server.Mobiles.PlayerMobile)
					{
						from.SendGump(new HonorSelf((Server.Mobiles.PlayerMobile)from));
						from.SendMessage("HonorSelf aberto.");
					}
					else
					{
						from.SendMessage("Este gump requer um PlayerMobile.");
					}
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 23: // ConfirmBreakCrystalGump
					from.SendMessage("Selecione um BaseImprisonedMobile (crystal) para abrir o ConfirmBreakCrystalGump.");
					from.Target = new CrystalTargetForMenu(from, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 24: // PlayerVendorBuyGump
					from.SendMessage("Selecione um PlayerVendor para abrir o PlayerVendorBuyGump.");
					from.Target = new VendorTargetForMenu(from, 24, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 25: // PlayerVendorOwnerGump
					from.SendMessage("Selecione um PlayerVendor para abrir o PlayerVendorOwnerGump.");
					from.Target = new VendorTargetForMenu(from, 25, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 26: // NewPlayerVendorOwnerGump
					from.SendMessage("Selecione um PlayerVendor para abrir o NewPlayerVendorOwnerGump.");
					from.Target = new VendorTargetForMenu(from, 26, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 27: // PlayerVendorCustomizeGump
					from.SendMessage("Selecione um PlayerVendor para abrir o PlayerVendorCustomizeGump.");
					from.Target = new VendorTargetForMenu(from, 27, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 28: // NewPlayerVendorCustomizeGump
					from.SendMessage("Selecione um PlayerVendor para abrir o NewPlayerVendorCustomizeGump.");
					from.Target = new VendorTargetForMenu(from, 28, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 29: // ReclaimVendorGump
					from.SendMessage("Selecione uma casa para abrir o ReclaimVendorGump.");
					from.Target = new HouseTargetForMenu(from, 29, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 30: // VendorInventoryGump
					from.SendMessage("Selecione uma casa para abrir o VendorInventoryGump.");
					from.Target = new HouseTargetForMenu(from, 30, m_SelectedCategory);
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					break;

				case 31: // MainUOE
					EnsureUOETool(from);
					from.SendGump(new MainUOE(from, 0));
					from.SendMessage("MainUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 32: // SubUOE
					EnsureUOETool(from);
					from.SendGump(new SubUOE(from, 0));
					from.SendMessage("SubUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 33: // AddUOE
					EnsureUOETool(from);
					from.SendGump(new AddUOE(from, 0));
					from.SendMessage("AddUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 34: // AltUOE
					EnsureUOETool(from);
					from.SendGump(new AltUOE(from, 0));
					from.SendMessage("AltUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 35: // CirUOE
					EnsureUOETool(from);
					from.SendGump(new CirUOE(from, 0));
					from.SendMessage("CirUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 36: // DelUOE
					EnsureUOETool(from);
					from.SendGump(new DelUOE(from, 0));
					from.SendMessage("DelUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 37: // GridUOE
					EnsureUOETool(from);
					from.SendGump(new GridUOE(from, 0));
					from.SendMessage("GridUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 38: // GumpSelUOE
					EnsureUOETool(from);
					from.SendGump(new GumpSelUOE(from, 0));
					from.SendMessage("GumpSelUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 39: // HelpUOE
					EnsureUOETool(from);
					from.SendGump(new HelpUOE(from, 0));
					from.SendMessage("HelpUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 40: // HueUOE
					EnsureUOETool(from);
					from.SendGump(new HueUOE(from, 0));
					from.SendMessage("HueUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 41: // InfoUOE
					EnsureUOETool(from);
					from.SendGump(new InfoUOE(from, 0));
					from.SendMessage("InfoUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 42: // ListUOE
					EnsureUOETool(from);
					from.SendGump(new ListUOE(from, 0));
					from.SendMessage("ListUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 43: // MoveUOE
					EnsureUOETool(from);
					from.SendGump(new MoveUOE(from, 0));
					from.SendMessage("MoveUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 44: // MultiUOE
					EnsureUOETool(from);
					from.SendGump(new MultiUOE(from, 0));
					from.SendMessage("MultiUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 45: // PickUOE
					EnsureUOETool(from);
					from.SendGump(new PickUOE(from, 0));
					from.SendMessage("PickUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 46: // PosUOE
					EnsureUOETool(from);
					from.SendGump(new PosUOE(from, 0));
					from.SendMessage("PosUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 47: // ResetUOE
					EnsureUOETool(from);
					from.SendGump(new ResetUOE(from, 0));
					from.SendMessage("ResetUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 48: // RndUOE
					EnsureUOETool(from);
					from.SendGump(new RndUOE(from, 0));
					from.SendMessage("RndUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 49: // SetIdUOE
					EnsureUOETool(from);
					from.SendGump(new SetIdUOE(from, 0));
					from.SendMessage("SetIdUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 50: // SetLocUOE
					EnsureUOETool(from);
					from.SendGump(new SetLocUOE(from, 0));
					from.SendMessage("SetLocUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 51: // SettingUOE
					EnsureUOETool(from);
					from.SendGump(new SettingUOE(from, 0));
					from.SendMessage("SettingUOE aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;

				case 52: // MageryItemsGump
					Server.Items.MageryTestToken mageryToken = new Server.Items.MageryTestToken();
					mageryToken.MoveToWorld(from.Location, from.Map);
					from.SendGump(new MageryItemsGump(mageryToken));
					from.SendMessage("MageryItemsGump aberto. O token de teste será deletado quando você fechar o gump.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory));
					break;
			}
		}

		/// <summary>
		/// Garante que o jogador tenha um UOETool no backpack para testar os gumps UOE
		/// </summary>
		private static void EnsureUOETool(Mobile from)
		{
			if (from == null || !(from is PlayerMobile))
				return;

			PlayerMobile pm = from as PlayerMobile;
			if (pm.Backpack == null)
				return;

			Item existingTool = pm.Backpack.FindItemByType(typeof(Server.Items.UOETool));
			if (existingTool == null)
			{
				Server.Items.UOETool tool = new Server.Items.UOETool();
				tool.MoveToWorld(pm.Location, pm.Map);
				pm.PlaceInBackpack(tool);
				pm.SendMessage("UOETool de teste criado e adicionado ao seu backpack.");
			}
		}

		/// <summary>
		/// Callback para WarningGump
		/// </summary>
		private static void OnWarningGumpResponse(Mobile from, bool okay, object state)
		{
			if (okay)
				from.SendMessage("Você clicou em OK no WarningGump.");
			else
				from.SendMessage("Você cancelou o WarningGump.");
		}

		/// <summary>
		/// Callback para NoticeGump
		/// </summary>
		private static void OnNoticeGumpResponse(Mobile from, object state)
		{
			from.SendMessage("Você leu o NoticeGump.");
		}

		/// <summary>
		/// Gump de teste baseado em BaseConfirmGump
		/// </summary>
		private class TestConfirmGump : BaseConfirmGump
		{
			public override int TitleNumber { get { return 1075083; } } // Warning!
			public override int LabelNumber { get { return 1074975; } } // Are you sure you wish to select this?

			public override void Confirm(Mobile from)
			{
				from.SendMessage("Você confirmou a ação no BaseConfirmGump.");
			}

			public override void Refuse(Mobile from)
			{
				from.SendMessage("Você recusou a ação no BaseConfirmGump.");
			}
		}

		/// <summary>
		/// Target para selecionar pet para teste do PetResurrectGump
		/// </summary>
		private class TestPetTargetForMenu : Target
		{
			private Mobile m_From;
			private int m_SelectedCategory;

			public TestPetTargetForMenu(Mobile from, int selectedCategory) : base(10, false, TargetFlags.None)
			{
				m_From = from;
				m_SelectedCategory = selectedCategory;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				BaseCreature pet = targeted as BaseCreature;
				if (pet != null)
				{
					from.SendGump(new PetResurrectGump(from, pet, 0.10));
					from.SendMessage(String.Format("Gump de ressurreição de pet aberto para teste (pet: {0}).", pet.Name));
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
				}
				else
				{
					from.SendMessage("O alvo não é um pet válido.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
				}
			}

			protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
			{
				// Cria um pet de teste temporário
				BaseCreature testPet = new Server.Mobiles.Horse();
				testPet.Name = "Pet de Teste";
				testPet.MoveToWorld(from.Location, from.Map);
				testPet.ControlMaster = from;
				testPet.Controlled = true;
				testPet.IsBonded = true;
				testPet.SetControlMaster(from);
				testPet.IsDeadPet = true;
				testPet.Hits = 0;

				from.SendGump(new PetResurrectGump(from, testPet, 0.10));
				from.SendMessage("Pet de teste criado e gump aberto. O pet será deletado quando você fechar o gump ou cancelar.");
				from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
			}
		}

		/// <summary>
		/// Target para selecionar casa para teste de gumps de casas
		/// </summary>
		private class HouseTargetForMenu : Target
		{
			private Mobile m_From;
			private int m_GumpType; // 16 = HouseGump, 17 = HouseGumpAOS, 18 = HouseDemolishGump
			private int m_SelectedCategory;

			public HouseTargetForMenu(Mobile from, int gumpType, int selectedCategory) : base(15, false, TargetFlags.None)
			{
				m_From = from;
				m_GumpType = gumpType;
				m_SelectedCategory = selectedCategory;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				BaseHouse house = null;

				// Tenta obter a casa de diferentes formas
				if (targeted is BaseHouse)
				{
					house = (BaseHouse)targeted;
				}
				else if (targeted is HouseSign)
				{
					house = ((HouseSign)targeted).Owner;
				}
				else if (targeted is Item)
				{
					house = BaseHouse.FindHouseAt((Item)targeted);
				}
				else if (targeted is Mobile)
				{
					house = BaseHouse.FindHouseAt((Mobile)targeted);
				}

				if (house != null && !house.Deleted)
				{
					switch (m_GumpType)
					{
						case 16: // HouseGump
							from.SendGump(new HouseGump(from, house));
							from.SendMessage(String.Format("HouseGump aberto para a casa: {0}.", house.Sign != null ? house.Sign.GetName() : "Sem nome"));
							break;

						case 17: // HouseGumpAOS
							from.SendGump(new HouseGumpAOS(HouseGumpPageAOS.Information, from, house));
							from.SendMessage(String.Format("HouseGumpAOS aberto para a casa: {0}.", house.Sign != null ? house.Sign.GetName() : "Sem nome"));
							break;

						case 18: // HouseDemolishGump
							from.SendGump(new HouseDemolishGump(from, house));
							from.SendMessage(String.Format("HouseDemolishGump aberto para a casa: {0}.", house.Sign != null ? house.Sign.GetName() : "Sem nome"));
							break;

						case 29: // ReclaimVendorGump
							from.SendGump(new ReclaimVendorGump(house));
							from.SendMessage(String.Format("ReclaimVendorGump aberto para a casa: {0}.", house.Sign != null ? house.Sign.GetName() : "Sem nome"));
							break;

						case 30: // VendorInventoryGump
							from.SendGump(new VendorInventoryGump(house, from));
							from.SendMessage(String.Format("VendorInventoryGump aberto para a casa: {0}.", house.Sign != null ? house.Sign.GetName() : "Sem nome"));
							break;
					}
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
				}
				else
				{
					from.SendMessage("O alvo não é uma casa válida. Tente selecionar um HouseSign, um item dentro de uma casa, ou um jogador dentro de uma casa.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
				}
			}

			protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
			{
				from.SendMessage("Seleção de casa cancelada.");
				from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
			}
		}

		/// <summary>
		/// Target para selecionar casa e jogador para HouseTransferGump
		/// </summary>
		private class HouseTransferTargetForMenu : Target
		{
			private Mobile m_From;
			private BaseHouse m_House;
			private int m_SelectedCategory;

			public HouseTransferTargetForMenu(Mobile from, int selectedCategory) : this(from, null, selectedCategory)
			{
			}

			public HouseTransferTargetForMenu(Mobile from, BaseHouse house, int selectedCategory) : base(15, false, TargetFlags.None)
			{
				m_From = from;
				m_House = house;
				m_SelectedCategory = selectedCategory;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (m_House == null)
				{
					// Primeiro target: casa
					BaseHouse house = null;

					if (targeted is BaseHouse)
					{
						house = (BaseHouse)targeted;
					}
					else if (targeted is HouseSign)
					{
						house = ((HouseSign)targeted).Owner;
					}
					else if (targeted is Item)
					{
						house = BaseHouse.FindHouseAt((Item)targeted);
					}
					else if (targeted is Mobile)
					{
						house = BaseHouse.FindHouseAt((Mobile)targeted);
					}

					if (house != null && !house.Deleted)
					{
						from.SendMessage("Agora selecione o jogador para transferir a casa.");
						from.Target = new HouseTransferTargetForMenu(from, house, m_SelectedCategory);
					}
					else
					{
						from.SendMessage("O alvo não é uma casa válida. Tente selecionar um HouseSign, um item dentro de uma casa, ou um jogador dentro de uma casa.");
						from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					}
				}
				else
				{
					// Segundo target: jogador
					Mobile targetPlayer = targeted as Mobile;
					if (targetPlayer != null && targetPlayer.Player)
					{
						from.SendGump(new HouseTransferGump(from, targetPlayer, m_House));
						from.SendMessage(String.Format("HouseTransferGump aberto para transferir a casa {0} para {1}.", 
							m_House.Sign != null ? m_House.Sign.GetName() : "Sem nome", targetPlayer.Name));
						from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
					}
					else
					{
						from.SendMessage("O alvo deve ser um jogador. Tente novamente.");
						from.Target = new HouseTransferTargetForMenu(from, m_House, m_SelectedCategory);
					}
				}
			}

			protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
			{
				from.SendMessage("Seleção cancelada.");
				from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
			}
		}

		/// <summary>
		/// Target para selecionar jogador para ViewHousesGump
		/// </summary>
		private class ViewHousesTargetForMenu : Target
		{
			private Mobile m_From;
			private int m_SelectedCategory;

			public ViewHousesTargetForMenu(Mobile from, int selectedCategory) : base(15, false, TargetFlags.None)
			{
				m_From = from;
				m_SelectedCategory = selectedCategory;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				Mobile targetPlayer = targeted as Mobile;
				if (targetPlayer != null && targetPlayer.Player)
				{
					System.Collections.Generic.List<BaseHouse> houses = ViewHousesGump.GetHouses(targetPlayer);
					from.SendGump(new ViewHousesGump(from, houses, null));
					from.SendMessage(String.Format("ViewHousesGump aberto mostrando as casas de {0}.", targetPlayer.Name));
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
				}
				else
				{
					from.SendMessage("O alvo deve ser um jogador.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
				}
			}

			protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
			{
				from.SendMessage("Seleção cancelada.");
				from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
			}
		}

		/// <summary>
		/// Target para selecionar BaseImprisonedMobile (crystal)
		/// </summary>
		private class CrystalTargetForMenu : Target
		{
			private Mobile m_From;
			private int m_SelectedCategory;

			public CrystalTargetForMenu(Mobile from, int selectedCategory) : base(15, false, TargetFlags.None)
			{
				m_From = from;
				m_SelectedCategory = selectedCategory;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				Server.Items.BaseImprisonedMobile crystal = targeted as Server.Items.BaseImprisonedMobile;
				if (crystal != null && !crystal.Deleted)
				{
					from.SendGump(new ConfirmBreakCrystalGump(crystal));
					from.SendMessage("ConfirmBreakCrystalGump aberto.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
				}
				else
				{
					from.SendMessage("O alvo deve ser um BaseImprisonedMobile (crystal).");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
				}
			}

			protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
			{
				from.SendMessage("Seleção cancelada.");
				from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
			}
		}

		/// <summary>
		/// Target para selecionar PlayerVendor para vários gumps
		/// </summary>
		private class VendorTargetForMenu : Target
		{
			private Mobile m_From;
			private int m_GumpType; // 24 = Buy, 25 = Owner, 26 = NewOwner, 27 = Customize, 28 = NewCustomize
			private int m_SelectedCategory;

			public VendorTargetForMenu(Mobile from, int gumpType, int selectedCategory) : base(15, false, TargetFlags.None)
			{
				m_From = from;
				m_GumpType = gumpType;
				m_SelectedCategory = selectedCategory;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				Server.Mobiles.PlayerVendor vendor = targeted as Server.Mobiles.PlayerVendor;
				if (vendor != null && !vendor.Deleted)
				{
					switch (m_GumpType)
					{
						case 24: // PlayerVendorBuyGump
							// Precisa de VendorItem, vamos criar um temporário ou usar o primeiro do vendor
							if (vendor.Backpack != null && vendor.Backpack.Items.Count > 0)
							{
								Item testItem = vendor.Backpack.Items[0];
								// VendorItem constructor: (Item item, int price, string description, DateTime created)
								Server.Mobiles.VendorItem vi = new Server.Mobiles.VendorItem(testItem, 100, "Item de Teste", DateTime.UtcNow);
								from.SendGump(new PlayerVendorBuyGump(vendor, vi));
								from.SendMessage("PlayerVendorBuyGump aberto (usando item de teste do vendor).");
							}
							else
							{
								from.SendMessage("O vendor precisa ter pelo menos um item no backpack para testar este gump.");
							}
							break;

						case 25: // PlayerVendorOwnerGump
							from.SendGump(new PlayerVendorOwnerGump(vendor));
							from.SendMessage("PlayerVendorOwnerGump aberto.");
							break;

						case 26: // NewPlayerVendorOwnerGump
							from.SendGump(new NewPlayerVendorOwnerGump(vendor));
							from.SendMessage("NewPlayerVendorOwnerGump aberto.");
							break;

						case 27: // PlayerVendorCustomizeGump
							from.SendGump(new PlayerVendorCustomizeGump(vendor, from));
							from.SendMessage("PlayerVendorCustomizeGump aberto.");
							break;

						case 28: // NewPlayerVendorCustomizeGump
							from.SendGump(new NewPlayerVendorCustomizeGump(vendor));
							from.SendMessage("NewPlayerVendorCustomizeGump aberto.");
							break;
					}
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
				}
				else
				{
					from.SendMessage("O alvo deve ser um PlayerVendor.");
					from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
				}
			}

			protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
			{
				from.SendMessage("Seleção cancelada.");
				from.SendGump(new GumpTestMenu(from, m_SelectedCategory)); // Reabre o menu mantendo a categoria
			}
		}
	}
}

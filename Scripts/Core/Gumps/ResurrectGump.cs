using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{
	public enum ResurrectMessage
	{
		ChaosShrine = 0,
		VirtueShrine = 1,
		Healer = 2,
		Generic = 3,
	}

	public class ResurrectGump : Gump
	{
		private Mobile m_Healer;
		private int m_Price;
		private bool m_FromSacrifice;
		private double m_HitsScalar;

		/// <summary>
		/// Registra comandos GM para testar os gumps
		/// </summary>
		public static void Initialize()
		{
			CommandSystem.Register("TestResurrectGump", AccessLevel.GameMaster, new CommandEventHandler(TestResurrectGump_OnCommand));
			CommandSystem.Register("TestResurrectGumpPrice", AccessLevel.GameMaster, new CommandEventHandler(TestResurrectGumpPrice_OnCommand));
		}

		[Usage("TestResurrectGump")]
		[Description("Abre o gump de ressurreição para teste (sem preço)")]
		private static void TestResurrectGump_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;
			from.SendGump(new ResurrectGump(from, from, ResurrectMessage.Generic, false, 0.0));
			from.SendMessage("Gump de ressurreição aberto para teste.");
		}

		[Usage("TestResurrectGumpPrice [preço]")]
		[Description("Abre o gump de ressurreição com preço para teste")]
		private static void TestResurrectGumpPrice_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;
			int price = 1000; // Preço padrão

			if (e.Arguments.Length > 0)
			{
				if (!int.TryParse(e.Arguments[0], out price))
				{
					from.SendMessage("Uso: TestResurrectGumpPrice [preço]");
					return;
				}
			}

			from.SendGump(new ResurrectGump(from, from, price));
			from.SendMessage(String.Format("Gump de ressurreição com preço {0} moedas de ouro aberto para teste.", price));
		}

		public ResurrectGump( Mobile owner ): this( owner, owner, ResurrectMessage.Generic, false )
		{
		}

		public ResurrectGump( Mobile owner, double hitsScalar ): this( owner, owner, ResurrectMessage.Generic, false, hitsScalar )
		{
		}

		public ResurrectGump( Mobile owner, bool fromSacrifice ): this( owner, owner, ResurrectMessage.Generic, fromSacrifice )
		{
		}

		public ResurrectGump( Mobile owner, Mobile healer ): this( owner, healer, ResurrectMessage.Generic, false )
		{
		}

		public ResurrectGump( Mobile owner, ResurrectMessage msg ): this( owner, owner, msg, false )
		{
		}

		public ResurrectGump( Mobile owner, Mobile healer, ResurrectMessage msg ): this( owner, healer, msg, false )
		{
		}

		public ResurrectGump( Mobile owner, Mobile healer, ResurrectMessage msg, bool fromSacrifice ): this( owner, healer, msg, fromSacrifice, 0.0 )
		{
		}

		public ResurrectGump( Mobile owner, Mobile healer, ResurrectMessage msg, bool fromSacrifice, double hitsScalar ): base( 50, 50 )
		{
			// Verifica se é um Avatar e requer sacrifício do curador
			if (healer is PlayerMobile && owner is PlayerMobile && ((PlayerMobile)owner).Avatar)
			{
                bool proceed = false;
                PlayerMobile healed = (PlayerMobile)owner;
                PlayerMobile hEaler = (PlayerMobile)healer;

                if (hEaler.BalanceEffect <= -5)
                {
                    proceed = true;
                    hEaler.BalanceEffect += 5;
                    hEaler.SendMessage(55, "Você sacrificou 5 pontos de sua influência no equilíbrio da força para trazer esta alma de volta.");
                }
                else if (hEaler.BalanceEffect >= 5)
                {
                    proceed = true;
                    hEaler.BalanceEffect -= 5;
                    hEaler.SendMessage(55, "Você sacrificou 5 pontos de sua influência no equilíbrio da força para trazer esta alma de volta.");
                }
                else
                {
                    proceed = true;
                    hEaler.Hits = (hEaler.Hits / 2) >= 1 ? hEaler.Hits / 2 : 1;
                    hEaler.SendMessage(55, "Você sacrificou metade de sua vitalidade para trazer esta alma de volta.");
                }

                if (proceed)
                {
                    healed.SendMessage(55, "Um sacrifício foi exigido para que o outro aventureiro lhe trouxesse de volta a vida!");
                }
                else
                {
                    hEaler.SendMessage(55, "Para trazer o alvo de volta a vida, um sacrifício é necessário e você não possui o suficiente!");
                    healed.SendMessage(55, "Para lhe trazer de volta a vida é necessário um sacrifício e o curandeiro não possui o suficiente! Procure outro que possa lhe ajudar.");
                    healed.CloseGump(typeof(ResurrectGump));
                    return;
                }
            }

			m_Healer = healer;
			m_FromSacrifice = fromSacrifice;
			m_HitsScalar = hitsScalar;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

            int firstColumn = 100;
            int secondColumn = 350;
            int buttonLabelOffset = 30;

            AddPage(0);
			
			// Background images (corners and borders)
			AddImage(0, 0, 154);
			AddImage(300, 0, 154);
			AddImage(0, 99, 154);
			AddImage(300, 99, 154);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(2, 97, 129);
			AddImage(298, 97, 129);
			
			// Decorative elements
			AddImage(7, 6, 145);
			AddImage(5, 143, 142);
			AddImage(255, 171, 144);
			AddImage(171, 47, 132);
			AddImage(379, 8, 134);
			AddImage(167, 7, 156);
			AddImage(189, 10, 156);
			AddImage(209, 11, 156);
			AddImage(170, 44, 159);
			
			// Items
			AddItem(156, 67, 2);
			AddItem(178, 67, 3);
			AddItem(185, 118, 3244);
			
			// Decorative flames
			AddImage(36, 124, 162);
			AddImage(33, 131, 162);
			AddImage(45, 138, 162);
			AddImage(17, 135, 162);
			
			// Title
			AddHtml( 267, 95, 224, 22, @"<BODY><BASEFONT Color=#fff700><BIG>RESSURREIÇÃO</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			
			// Main message
			AddHtml( 100, 163, 400, 96, @"<BODY><BASEFONT Color=#FFFFFF><BIG>É possível que você seja ressuscitado aqui por este curandeiro.<br/>Você quer voltar para a terra dos vivos? Caso contrário, você poderá permanecer no reino espiritual.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			
			// Buttons (Yes and No)
			AddButton(100, 260, 4023, 4023, 1, GumpButtonType.Reply, 0); // Yes
            AddHtml(firstColumn + buttonLabelOffset, 260 + 2, 477, 22, @"<BODY><BASEFONT Color=#5eff00><BIG>Ressuscite-me</BIG></BASEFONT></BODY>", false, false);
            AddButton(350, 260, 4020, 4020, 0, GumpButtonType.Reply, 0); // No
            AddHtml(secondColumn + buttonLabelOffset, 260 + 2, 477, 22, @"<BODY><BASEFONT Color=#FF0000><BIG>Agora não</BIG></BASEFONT></BODY>", false, false);
		}

		public ResurrectGump( Mobile owner, Mobile healer, int price ): base( 50, 50 )
		{
			// Verifica se é um Avatar e requer sacrifício do curador
			if (healer is PlayerMobile && owner is PlayerMobile && ((PlayerMobile)owner).Avatar)
			{
				bool proceed = false;
				PlayerMobile healed = (PlayerMobile)owner;
				PlayerMobile hEaler = (PlayerMobile)healer;

				if (hEaler.BalanceEffect <= -5)
				{
					proceed = true;
					hEaler.BalanceEffect += 5;
                    hEaler.SendMessage(55, "Você sacrificou 5 pontos de sua influência no equilíbrio da força para trazer esta alma de volta.");
                }
				else if (hEaler.BalanceEffect >= 5)
				{
					proceed = true;
					hEaler.BalanceEffect -= 5;
                    hEaler.SendMessage(55, "Você sacrificou 5 pontos de sua influência no equilíbrio da força para trazer esta alma de volta.");
                }
				else 
				{
                    proceed = true;
                    hEaler.Hits = (hEaler.Hits / 2) >= 1 ? hEaler.Hits / 2 : 1;
                    hEaler.SendMessage(55, "Você sacrificou metade de sua vitalidade para trazer esta alma de volta.");
                }

				if (proceed)
				{
					healed.SendMessage(55, "Um sacrifício foi exigido para que o outro aventureiro lhe trouxesse de volta a vida!");
                }
				else
				{
					hEaler.SendMessage(55, "Para trazer o alvo de volta a vida, um sacrifício é necessário e você não possui o suficiente!");
                    healed.SendMessage(55, "Para lhe trazer de volta a vida é necessário um sacrifício e o curandeiro não possui o suficiente! Procure outro que possa lhe ajudar.");
                    healed.CloseGump( typeof( ResurrectGump ) );
					return;
				}
			}

			m_Healer = healer;
			m_Price = price;

			Closable = false;

			AddPage( 0 );

			// Background frame
			AddImage( 0, 0, 3600 );
			AddImage( 0, 201, 3606 );
			AddImage( 380, 0, 3602 );
			AddImage( 380, 201, 3608 );

			// Side borders
			AddImageTiled( 0, 14, 15, 200, 3603 );
			AddImageTiled( 380, 14, 14, 200, 3605 );

			// Top and bottom borders
			AddImageTiled( 15, 201, 370, 16, 3607 );
			AddImageTiled( 15, 0, 370, 16, 3601 );

			// Main background
			AddImageTiled( 15, 15, 365, 190, 2624 );

			// Main message
			AddHtml( 30, 20, 360, 35, @"<BODY><BASEFONT Color=#FFFFFF>Deseja se juntar aos vivos novamente? Posso restaurar seu corpo... por um preço, é claro...</BASEFONT></BODY>", false, false );

			// Gold coin image and lines
			AddImage( 65, 72, 5605 );
			AddImageTiled( 80, 90, 200, 1, 9107 );
			AddImageTiled( 95, 92, 200, 1, 9157 );

			// Price label (yellow)
			AddHtml( 90, 70, 100, 25, @"<BODY><BASEFONT Color=#FFFF00>" + price.ToString() + "</BASEFONT></BODY>", false, false );
			AddHtml( 140, 70, 100, 25, @"<BODY><BASEFONT Color=#FFFFFF>moedas de ouro</BASEFONT></BODY>", false, false );

			// Question (white)
			AddHtml( 30, 105, 345, 40, @"<BODY><BASEFONT Color=#FFFFFF>Você aceita a taxa, que será retirada do seu banco?</BASEFONT></BODY>", false, false );

			// Radio buttons
			AddRadio( 30, 140, 9727, 9730, true, 1 );
			AddHtml( 65, 145, 300, 25, @"<BODY><BASEFONT Color=#FFFF00>Pagar o dinheiro, mesmo assim?</BASEFONT></BODY>", false, false );

			AddRadio( 30, 175, 9727, 9730, false, 0 );
			AddHtml( 65, 178, 300, 25, @"<BODY><BASEFONT Color=#FF0000>Prefiro ficar morto, seu ganancioso!!!</BASEFONT></BODY>", false, false );

			// Accept button
			AddButton( 290, 175, 247, 248, 2, GumpButtonType.Reply, 0 );

			// Border decorations
			AddImageTiled( 15, 14, 365, 1, 9107 );
			AddImageTiled( 380, 14, 1, 190, 9105 );
			AddImageTiled( 15, 205, 365, 1, 9107 );
			AddImageTiled( 15, 14, 1, 190, 9105 );
			AddImageTiled( 0, 0, 395, 1, 9157 );
			AddImageTiled( 394, 0, 1, 217, 9155 );
			AddImageTiled( 0, 216, 395, 1, 9157 );
			AddImageTiled( 0, 0, 1, 217, 9155 );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			from.CloseGump( typeof( ResurrectGump ) );

			if (from is PlayerMobile && ((PlayerMobile)from).SoulBound)
			{
				((PlayerMobile)from).ResetPlayer();
				return;
			}

			if( info.ButtonID == 1 || info.ButtonID == 2 ) // Yes button or Accept button
			{
				if( from.Map == null || !from.Map.CanFit( from.Location, 16, false, false ) )
				{
					from.SendMessage("Você não pode ser ressuscitado neste local!");
					return;
				}

				// Verifica se há preço a pagar (gump com healer pago)
				if( m_Price > 0 )
				{
					if( info.IsSwitched( 1 ) ) // Radio button "pay money" selecionado
					{
						if( Banker.Withdraw( from, m_Price ) )
						{
							from.SendMessage(String.Format("{0} moedas de ouro foram retiradas do seu banco.", m_Price.ToString()));
							from.SendMessage(String.Format("Você tem {0} moedas de ouro restantes no seu banco.", Banker.GetBalance( from ).ToString()));
							Server.Misc.Death.Penalty( from, false );
						}
						else
						{
							from.SendMessage("Infelizmente, você não tem dinheiro suficiente no banco para cobrir o custo da cura.");
							return;
						}
					}
					else
					{
						from.SendMessage("Você decidiu não pagar o curandeiro e assim permanece morto.");
						return;
					}
				}

				from.PlaySound( 0x214 );
				from.FixedEffect( 0x376A, 10, 16 );

				if (from.Criminal)
					from.Criminal = false;
					
				from.Resurrect();

				if( m_Healer != null && from != m_Healer )
				{
					VirtueLevel level = VirtueHelper.GetLevel( m_Healer, VirtueName.Compassion );

					switch( level )
					{
						case VirtueLevel.Seeker: from.Hits = AOS.Scale( from.HitsMax, 20 ); break;
						case VirtueLevel.Follower: from.Hits = AOS.Scale( from.HitsMax, 40 ); break;
						case VirtueLevel.Knight: from.Hits = AOS.Scale( from.HitsMax, 80 ); break;
					}
				}

				if( m_FromSacrifice && from is PlayerMobile )
				{
					((PlayerMobile)from).AvailableResurrects -= 1;

					Container pack = from.Backpack;
					Container corpse = from.Corpse;

					if( pack != null && corpse != null )
					{
						List<Item> items = new List<Item>( corpse.Items );

						for( int i = 0; i < items.Count; ++i )
						{
							Item item = items[i];

							if( item.Layer != Layer.Hair && item.Layer != Layer.FacialHair && item.Movable )
								pack.DropItem( item );
						}
					}
				}

				if( from.Fame > 0 )
				{
					int amount = from.Fame / 10;

					Misc.Titles.AwardFame( from, -amount, true );
				}

				if( !Core.AOS && from.ShortTermMurders >= 5 )
				{
					double loss = (100.0 - (4.0 + (from.ShortTermMurders / 5.0))) / 100.0; // 5 to 15% loss

					if( loss < 0.85 )
						loss = 0.85;
					else if( loss > 0.95 )
						loss = 0.95;

					if( from.RawStr * loss > 10 )
						from.RawStr = (int)(from.RawStr * loss);
					if( from.RawInt * loss > 10 )
						from.RawInt = (int)(from.RawInt * loss);
					if( from.RawDex * loss > 10 )
						from.RawDex = (int)(from.RawDex * loss);

					for( int s = 0; s < from.Skills.Length; s++ )
					{
						if( from.Skills[s].Base * loss > 35 )
							from.Skills[s].Base *= loss;
					}
				}

				if( from.Alive && m_HitsScalar > 0 )
					from.Hits = (int)(from.HitsMax * m_HitsScalar);
			}
		}
	}
}

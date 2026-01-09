using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Commands;
using Server.Targeting;

namespace Server.Gumps
{
	public class PetResurrectGump : Gump
	{
		private BaseCreature m_Pet;
		private double m_HitsScalar;

		/// <summary>
		/// Registra comandos GM para testar o gump
		/// </summary>
		public static void Initialize()
		{
			CommandSystem.Register("TestPetResurrectGump", AccessLevel.GameMaster, new CommandEventHandler(TestPetResurrectGump_OnCommand));
		}

		[Usage("TestPetResurrectGump")]
		[Description("Abre o gump de ressurreição de pet para teste. Selecione um pet ou use sem target para criar um pet de teste.")]
		private static void TestPetResurrectGump_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;
			from.SendMessage("Selecione um pet ou pressione ESC para criar um pet de teste.");
			from.Target = new TestPetTarget();
		}

		private class TestPetTarget : Target
		{
			public TestPetTarget() : base(10, false, TargetFlags.None)
			{
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				BaseCreature pet = targeted as BaseCreature;
				if (pet != null)
				{
					from.SendGump(new PetResurrectGump(from, pet, 0.10));
					from.SendMessage(String.Format("Gump de ressurreição de pet aberto para teste (pet: {0}).", pet.Name));
				}
				else
				{
					from.SendMessage("O alvo não é um pet válido.");
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
			}
		}

		public PetResurrectGump( Mobile from, BaseCreature pet ) : this( from, pet, 0.0 )
		{
		}

		public PetResurrectGump( Mobile from, BaseCreature pet, double hitsScalar ) : base( 50, 50 )
		{
			from.CloseGump( typeof( PetResurrectGump ) );

			m_Pet = pet;
			m_HitsScalar = hitsScalar;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

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
			AddHtml( 267, 95, 224, 22, @"<BODY><BASEFONT Color=#FBFBFB><BIG>RESSURREIÇÃO</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			
			// Main message
			AddHtml( 93, 163, 400, 76, @"<BODY><BASEFONT Color=#FCFF00><BIG>Deseja santificar a ressurreição de " + pet.Name + "?</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			
			// Buttons (Yes and No)
			AddButton(146, 260, 4023, 4023, 0x1, GumpButtonType.Reply, 0); // Yes
			AddButton(374, 260, 4020, 4020, 0x2, GumpButtonType.Reply, 0); // No
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( m_Pet.Deleted || !m_Pet.IsBonded || !m_Pet.IsDeadPet )
				return;

			Mobile from = state.Mobile;

			if ( info.ButtonID == 1 ) // Yes button
			{
				if ( m_Pet.Map == null || !m_Pet.Map.CanFit( m_Pet.Location, 16, false, false ) )
				{
					from.SendMessage("Você falhou ao ressuscitar a criatura. O local está bloqueado.");
					return;
				}
				else if( m_Pet.Region != null && m_Pet.Region.IsPartOf( "Khaldun" ) )
				{
					from.SendMessage("O véu da morte nesta área é muito forte e resiste aos seus esforços para restaurar a vida.");
					return;
				}

				m_Pet.PlaySound( 0x214 );
				m_Pet.FixedEffect( 0x376A, 10, 16 );
				m_Pet.ResurrectPet();

				double decreaseAmount;

				// Reduz habilidades do pet (10% se for o dono, 20% se for outro)
				if( from == m_Pet.ControlMaster )
					decreaseAmount = 0.1;
				else
					decreaseAmount = 0.2;

				for ( int i = 0; i < m_Pet.Skills.Length; ++i )
					m_Pet.Skills[i].Base -= decreaseAmount;

				// Aplica porcentagem de stats se especificado (10% para ressurreição por spell)
				if( !m_Pet.IsDeadPet && m_HitsScalar > 0 )
				{
					m_Pet.Hits = (int)(m_Pet.HitsMax * m_HitsScalar);
					m_Pet.Stam = (int)(m_Pet.StamMax * m_HitsScalar);
					m_Pet.Mana = (int)(m_Pet.ManaMax * m_HitsScalar);
				}
			}

		}
	}
}
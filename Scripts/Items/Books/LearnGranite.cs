using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

namespace Server.Items
{
	public class LearnGraniteBook : Item
	{
		[Constructable]
		public LearnGraniteBook( ) : base( 0x4C5C )
		{
			Weight = 1.0;
			Name = "Guia de Granitos";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Guia sobre Vidros, Granitos & Mármore" );
		}

		public class LearnGraniteGump : Gump
		{
			public LearnGraniteGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);

				AddImage(0, 430, 155);
				AddImage(300, 430, 155);
				AddImage(600, 430, 155);
				AddImage(0, 0, 155);
				AddImage(300, 0, 155);
				AddImage(0, 300, 155);
				AddImage(300, 300, 155);
				AddImage(600, 0, 155);
				AddImage(600, 300, 155);

				AddImage(2, 2, 129);
				AddImage(300, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(302, 298, 129);
				AddImage(598, 298, 129);

				AddImage(6, 7, 133);
				AddImage(230, 46, 132);
				AddImage(530, 46, 132);
				AddImage(680, 7, 134);
				AddImage(598, 428, 129);
				AddImage(298, 428, 129);
				AddImage(2, 428, 129);
				AddImage(5, 484, 142);
				AddImage(329, 693, 140);
				AddImage(573, 693, 140);
				AddImage(856, 695, 143);

				AddItem(575, 122, 2859);
				AddItem(346, 120, 3898);
				AddItem(663, 131, 3854);
				AddItem(834, 101, 4632);
				AddItem(308, 122, 6009);
				AddItem(752, 130, 4787);
				AddItem(486, 131, 3718);

				AddHtml( 170, 70, 600, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>GUIA SOBRE VIDRO, GRANITOS & M�RMORE</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				int i = 135;
				int o = 36;

				AddItem(100, i, 6011, 0);
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Regular</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;

				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Cobre R�stico</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;

                AddItem(100, i, 6011, MaterialInfo.GetMaterialColor("copper", "classic", 0));
                AddHtml(150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Cobre</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i = i + o;

                AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Ferro Negro</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Bronze</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;

				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "gold", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Dourado</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;

				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Agapite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;

				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "verite", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Verite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;

				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Valorite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;

                AddItem(100, i, 6011, MaterialInfo.GetMaterialColor("rosenium", "classic", 0));
                AddHtml(150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Rosenium</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i = i + o;

                AddItem(100, i, 6011, MaterialInfo.GetMaterialColor("titanium", "classic", 0));
                AddHtml(150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Titanium</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i = i + o;


                /*AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Nepturite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;

				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Obsidian</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Mithril</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "xormite", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Xormite</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;
				AddItem(100, i, 6011, MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ));
				AddHtml( 150, i, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Dwarven</BIG></BASEFONT></BODY>", (bool)false, (bool)false); i=i+o;*/

				AddHtml( 303, 198, 573, 466, @"<BODY><BASEFONT Color=#FBFBFB><BIG>A minera��o � a habilidade necess�ria para encontrar granito e m�rmore dentro de cavernas e montanhas. Com estes materias, o artes�o pode criar mobilia e est�tuas.<br/><br/>Voc� simplesmente precisa uma picareta, marreta ou uma p�, clicar duas vezes nela e em seguida mirar em um lado da montanha ou no ch�o de cavernas. Embora normalmente se obtenha granito regular, voc� acabar� ficando habilidoso o suficiente para desenterrar outros tipos de granitos mais raros.<br/><br/>Os muitos tipos de granitos est�o listados aqui, come�ando do mais simples para o de maior qualidade.<br/><br/>Para criar objetos com granitos, primeiro voc� precisa aprender a habilidade de cavar, identificar e extrair corretamente estas pedras. Lendas dizem que os g�rgulas s�o os criadores deste conhecimento secreto e que eles passaram partes destes segredos ao mestres do conhecimento. Por isso, al�m de deter o conhecimento de minera��o, voc� precisa adquirir uma forma de obter este saber atrav�s do estudo desta arte e/ou ci�ncia.<br/>Somente ap�s isso, � poss�vel combinar este conhecimento com as habilidades avan�adas de capintaria atrav�s do uso de ferramentas como martelo e chisel.<br/><br/>Tamb�m � poss�vel atrav�s da minera��o em desertos e praias encontrar areia para aa cria��o de vidro. Com este material em m�os � poss�vel utilizar uma ferramenta de sopro para fundir a areia em vidro e assim, criar itens como garrafas e jarras. Voc� precisar� de uma marreta ou p� e selecionar uma �rea com areia de qualidade pr�xima a voc� que poder� ser utilizada para a cria��o dos itens de vidro.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( !IsChildOf( e.Backpack ) ) 
			{
				e.SendMessage("Para ler, isso precisa estar na sua mochila.");
			}
			else
			{
				e.CloseGump( typeof( LearnGraniteGump ) );
				e.SendGump( new LearnGraniteGump( e ) );
				e.PlaySound( 0x249 );
			}
		}

		public LearnGraniteBook(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Name = "Pergaminho do Conhecimento";
            ItemID = 0x4C5C;
		}
	}
}
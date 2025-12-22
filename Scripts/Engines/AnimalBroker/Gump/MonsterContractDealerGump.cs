using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class TamingBODDealerGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "TamingBODDealerGump", AccessLevel.GameMaster, new CommandEventHandler( TamingBODDealerGump_OnCommand ) ); 
      } 

      private static void TamingBODDealerGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new TamingBODDealerGump( e.Mobile ) ); 
      } 

      public TamingBODDealerGump( Mobile owner ) : base( 50,50 ) 
      { 
//----------------------------------------------------------------------------------------------------

				AddPage( 0 );
			AddImageTiled(  54, 33, 369, 400, 2624 );
			AddAlphaRegion( 54, 33, 369, 400 );

			AddImageTiled( 416, 39, 44, 389, 203 );
//--------------------------------------Window size bar--------------------------------------------
			
			AddImage( 97, 49, 9005 );
			AddImageTiled( 58, 39, 29, 390, 10460 );
			AddImageTiled( 412, 37, 31, 389, 10460 );
			AddLabel( 140, 60, 0x34, "Contratos de Animais & Bestas" );
			
			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW>Olá. Tenho um favor a pedir e gostaria de saber se você poderia me ajudar.<BR><BR>Tenho em minha posse contratos de compradores para certos amimais e bestas. Ficarei feliz em dar-lhe os contratos e ficarei apenas com uma pequena comissão se você cumprir estes contratos.<BR>" +
"<BASEFONT COLOR=YELLOW><BR>Você me ajudará a fechar estes contratos?<BR><BR>Obrigado meu amigo. " + "</BODY>", false, true);

			AddImage( 430, 9, 10441);
			AddImageTiled( 40, 38, 17, 391, 9263 );
			AddImage( 6, 25, 10421 );
			AddImage( 34, 12, 10420 );
			AddImageTiled( 94, 25, 342, 15, 10304 );
			AddImageTiled( 40, 427, 415, 16, 10304 );
			AddImage( -10, 314, 10402 );
			AddImage( 56, 150, 10411 );
			AddImage( 155, 120, 2103 );
			AddImage( 136, 84, 96 );

			AddButton( 225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 ); 

//--------------------------------------------------------------------------------------------------------------
      } 

      public override void OnResponse( NetState state, RelayInfo info ) //Function for GumpButtonType.Reply Buttons 
      { 
         Mobile from = state.Mobile; 

         switch ( info.ButtonID ) 
         { 
            case 0: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
            { 
               // Create contract only after player clicks the button
               if ( from != null && from.Backpack != null )
               {
                  TamingBOD contract = new TamingBOD();
                  from.Backpack.DropItem( contract );
                  from.SendMessage( "O contrato foi colocado em sua mochila. Boa caça!" );
               }
               break; 
            } 

         }
      }
   }
}

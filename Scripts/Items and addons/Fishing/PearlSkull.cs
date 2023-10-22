using System;using Server;using Server.Network;using System.Text;using Server.Items;using Server.Mobiles;namespace Server.Items{	public class PearlSkull : Item	{		[Constructable]		public PearlSkull() : base( 0x1AE0 )		{			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3, 0x1AE4 );			string sLiquid = "estranho";			switch( Utility.RandomMinMax( 0, 6 ) )			{				case 0: sLiquid = "estranho"; break;				case 1: sLiquid = "incomum"; break;				case 2: sLiquid = "bizarro"; break;				case 3: sLiquid = "curioso"; break;				case 4: sLiquid = "peculiar"; break;				case 5: sLiquid = "estranho"; break;				case 6: sLiquid = "anormal"; break;			}			Name = "cr�nio " + sLiquid;			Weight = 1.0;		}		public PearlSkull( Serial serial ) : base( serial )		{		}		public override void OnDoubleClick( Mobile from )		{			if ( !IsChildOf( from.Backpack ) ) 			{				from.SendMessage(55, "Isso deve estar na sua mochila para usar.");				return;			}			else			{				from.AddToBackpack( new MysticalPearl() );				from.SendMessage(55, "Voc� abre a boca da caveira e encontra uma p�rola m�stica.");				this.Delete();			}		}		public override void Serialize( GenericWriter writer )		{			base.Serialize( writer );			writer.Write( (int) 0 ); // version		}		public override void Deserialize( GenericReader reader )		{			base.Deserialize( reader );			int version = reader.ReadInt();		}	}}
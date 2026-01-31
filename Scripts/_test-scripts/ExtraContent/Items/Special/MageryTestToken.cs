using System;
using Server;
using Server.Gumps;

namespace Server.Items
{
	public class MageryTestToken : Item
	{
		public override int LabelNumber { get { return 0; } }

		[Constructable]
		public MageryTestToken() : base( 0x1F14 )
		{
			Name = "Magery Test Token";
			LootType = LootType.Blessed;
			Weight = 1.0;
			Hue = 0x48;
		}

		public MageryTestToken( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.CloseGump( typeof( MageryItemsGump ) );
				from.SendGump( new MageryItemsGump( this ) );
			}
			else
				from.SendLocalizedMessage( 1062334 ); // This item must be in your backpack to be used.
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Use this to test the Magery Items Gump" );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}


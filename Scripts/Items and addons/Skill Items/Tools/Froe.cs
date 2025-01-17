using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class Froe : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefCarpentry.CraftSystem; } }

		[Constructable]
		public Froe() : base( 0x10E5 )
		{
			Weight = 1.0;
            Name = "froe";
        }

		[Constructable]
		public Froe( int uses ) : base( uses, 0x10E5 )
		{
			Weight = 1.0;
            Name = "froe";
        }

		public Froe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
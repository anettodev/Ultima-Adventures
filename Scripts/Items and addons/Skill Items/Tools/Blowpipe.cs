using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	//[FlipableAttribute( 0xE8A, 0xE89 )]
	public class Blowpipe : BaseTool
	{
		public override CraftSystem CraftSystem { get { return DefGlassblowing.CraftSystem; } }

		public override int LabelNumber{ get{ return 1044608; } } // blow pipe

		[Constructable]
		public Blowpipe() : base(0x2805)
		{
			Name = "Tubo de sopro";
			Weight = 3.0;
			Hue = 0x4EA;
		}

		[Constructable]
		public Blowpipe( int uses ) : base( uses, 0x2805)
		{
            Name = "Tubo de sopro";
            Weight = 3.0;
			Hue = 0x4EA;
		}

		public Blowpipe( Serial serial ) : base( serial )
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

			if ( Weight == 2.0 )
				Weight = 4.0;
		}
	}
}
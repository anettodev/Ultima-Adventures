using System;
using Server;

namespace Server.Items
{
	public class GreaterManaPotion : BaseManaRefreshPotion
	{
		public override int MinMana { get{ return 20; } }
		public override int MaxMana { get{ return 28; } }
		public override double Delay { get{ return 6.0; } }

	[Constructable]
	public GreaterManaPotion( ) : base( PotionEffect.ManaGreater )
	{
		Name = "Poção de Alquimia";
		ItemID = 0x2406;
		Hue = Server.Items.PotionKeg.GetPotionColor( this );
	}

		public GreaterManaPotion( Serial serial ) : base( serial )
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
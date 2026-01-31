using System;
using Server;

namespace Server.Items
{
	public class GreaterStrengthPotion : BaseStrengthPotion
	{
		public override int MinStrength{ get{ return 6; } }
		public override int MaxStrength{ get{ return 8; } }
		public override TimeSpan Duration{ get{ return TimeSpan.FromSeconds( 90 ); } } // 90 seconds max

	[Constructable]
	public GreaterStrengthPotion() : base( PotionEffect.StrengthGreater )
	{
		Name = "Poção de Alquimia";
		ItemID = 0x25F7;
	}

		public GreaterStrengthPotion( Serial serial ) : base( serial )
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
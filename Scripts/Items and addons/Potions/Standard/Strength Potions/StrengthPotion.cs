using System;
using Server;

namespace Server.Items
{
	public class StrengthPotion : BaseStrengthPotion
	{
		public override int MinStrength{ get{ return 3; } }
		public override int MaxStrength{ get{ return 5; } }
		public override TimeSpan Duration{ get{ return TimeSpan.FromSeconds( 60 ); } } // 60 seconds

		[Constructable]
		public StrengthPotion() : base( PotionEffect.Strength )
		{
		}

		public StrengthPotion( Serial serial ) : base( serial )
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
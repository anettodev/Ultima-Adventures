using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Eternal Key (Chave Eterna) - A special key that doesn't drop on death
	/// Can be crafted by tinkers with 90+ Tinkering and Magery skills
	/// </summary>
	public class EternalKey : Key
	{
		[Constructable]
		public EternalKey() : base( KeyType.Gold, 0 )
		{
			Name = "Chave Eterna";
			LootType = LootType.Newbied;
			Hue = 0x430; // Special hue to distinguish from regular keys
		}

		[Constructable]
		public EternalKey( uint keyValue ) : base( KeyType.Gold, keyValue )
		{
			Name = "Chave Eterna";
			LootType = LootType.Newbied;
			Hue = 0x430;
		}

		public EternalKey( Serial serial ) : base( serial )
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


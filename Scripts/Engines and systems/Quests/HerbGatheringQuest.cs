using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class HerbGatheringQuest : Item
	{
		[Constructable]
		public HerbGatheringQuest() : base( 0x0E77 )
		{
			Movable = false;
			Name = "Ancient Druid Stone";
			Hue = 0x8A4; // Nature green
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from is PlayerMobile )
			{
				from.SendMessage( "You find an ancient stone etched with druidic runes..." );
				from.SendMessage( "The stone speaks of gathering rare herbs and learning the ways of nature." );

				// Give some herbs as reward
				from.AddToBackpack( new PlantHerbalism_Leaf() );
				from.AddToBackpack( new PlantHerbalism_Leaf() );

				// Small gold reward
				from.AddToBackpack( new Gold( Utility.RandomMinMax( 50, 150 ) ) );

				from.SendMessage( "You receive some herbs and a small reward for your discovery!" );

				this.Delete();
			}
		}

		public HerbGatheringQuest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}

using System;
using Server;
using Server.Items;

namespace Server.Items
{
	/// <summary>
	/// DEPRECATED: This addon has been consolidated into the generic TreasurePileAddon system.
	/// Use: new TreasurePileAddon(TreasurePileAddonConstants.TreasurePileVariation.Compact01)
	/// This file is maintained for backward compatibility but should not be used for new content.
	/// </summary>
	[Obsolete("Use TreasurePileAddon with TreasurePileVariation.Compact01 instead")]
	public class TreasurePile01Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new TreasurePile01AddonDeed();
			}
		}

		[ Constructable ]
		public TreasurePile01Addon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 6975 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 6976 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 6977 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 6979 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 6978 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 6980 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 6982 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 6983 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 6984 );
			AddComponent( ac, 1, -1, 0 );

		}

		public TreasurePile01Addon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class TreasurePile01AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new TreasurePile01Addon();
			}
		}

		[Constructable]
		public TreasurePile01AddonDeed()
		{
			ItemID = 0x0E41;
			Weight = 50.0;
			Hue = 2989;
			Name = string.Format(TreasurePileAddonStringConstants.DEED_NAME_FORMAT, 2); // Compact01 - #2
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add(1070722, string.Format("<BASEFONT COLOR=#8be4fc>{0}", TreasurePileAddonStringConstants.TYPE_COMPACT)); // [Compacto]
            list.Add(1070722, string.Format("<BASEFONT COLOR=#8be4fc>[{0}]", "Para adicionar, clique 2x no baú. Use um machado para remover."));

        }

		public TreasurePile01AddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
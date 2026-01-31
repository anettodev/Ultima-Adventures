using System;
using Server;
using Server.Items;

namespace Server.Items
{
	/// <summary>
	/// DEPRECATED: This addon has been consolidated into the generic TreasurePileAddon system.
	/// Use: new TreasurePileAddon(TreasurePileAddonConstants.TreasurePileVariation.Minimal03)
	/// This file is maintained for backward compatibility but should not be used for new content.
	/// </summary>
	[Obsolete("Use TreasurePileAddon with TreasurePileVariation.Minimal03 instead")]
	public class TreasurePile02Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new TreasurePile02AddonDeed();
			}
		}

		[ Constructable ]
		public TreasurePile02Addon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 6986 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 6987 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 6988 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 6991 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 6990 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 6993 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 6989 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 6992 );
			AddComponent( ac, 1, -1, 0 );

		}

		public TreasurePile02Addon( Serial serial ) : base( serial )
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

	public class TreasurePile02AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new TreasurePile02Addon();
			}
		}

		[Constructable]
		public TreasurePile02AddonDeed()
		{
			ItemID = 0x0E41;
			Weight = 50.0;
			Hue = 2989;
			Name = string.Format(TreasurePileAddonStringConstants.DEED_NAME_FORMAT, 1); // Minimal03 - #1 (smallest)
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add(1070722, string.Format("<BASEFONT COLOR=#8be4fc>{0}", TreasurePileAddonStringConstants.TYPE_MINIMAL)); // [Menor]
            list.Add(1070722, string.Format("<BASEFONT COLOR=#8be4fc>[{0}]", "Para adicionar, clique 2x no baú. Use um machado para remover."));
        }

		public TreasurePile02AddonDeed( Serial serial ) : base( serial )
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
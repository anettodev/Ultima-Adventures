using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	/// <summary>
	/// Minter NPC - A specialized banker who can mint coins and handle banking operations.
	/// Inherits from Banker to provide full banking functionality with minter-specific job title.
	/// </summary>
	public class Minter : Banker
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.MerchantsGuild; } }

		[Constructable]
		public Minter()
		{
			Job = JobFragment.minter;
			Karma = Utility.RandomMinMax(VendorConstants.KARMA_MIN, VendorConstants.KARMA_MAX);
			Title = "o Faria Limer";
		}

		public Minter( Serial serial ) : base( serial )
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
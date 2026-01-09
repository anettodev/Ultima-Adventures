using System;
using Server;
using Server.Items;

namespace Server.Items
{
	/// <summary>
	/// DEPRECATED: This addon has been consolidated into the generic TreasurePileAddon system.
	/// Use: new TreasurePileAddon(TreasurePileAddonConstants.TreasurePileVariation.Extended02)
	/// This file is maintained for backward compatibility but should not be used for new content.
	/// </summary>
	[Obsolete("Use TreasurePileAddon with TreasurePileVariation.Extended02 instead")]
	public class TreasurePile2Addon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {6981, 3, -2, 0}, {6988, -2, -1, 0}, {6989, -2, -2, 0}// 1	2	3	
			, {6995, 1, 0, 0}, {6996, 0, 2, 0}, {6998, -1, 2, 0}// 4	5	6	
			, {7003, 1, 1, 0}, {7000, -1, 0, 0}, {7005, -1, 1, 0}// 7	8	9	
			, {7001, 0, -1, 0}, {6984, 2, -2, 0}, {6980, -1, -1, 0}// 10	11	12	
			, {6999, -2, 0, 0}, {7015, 0, -2, 0}, {6979, 0, 1, 0}// 13	14	15	
			, {7002, 1, -1, 0}, {6993, -1, -2, 0}, {6998, 0, 0, 0}// 16	17	18	
			, {7017, 1, -2, 0}, {7010, -1, 3, 0}, {7004, 2, -1, 2}// 19	20	21	
			, {7011, -2, 3, 0}, {6992, -2, 2, 0}, {7012, -2, 1, 0}// 22	23	24	
			, {7011, -2, 1, 0}, {6977, -3, 3, 0}, {6996, -2, 3, 0}// 25	26	27	
					};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new TreasurePile2AddonDeed();
			}
		}

		[ Constructable ]
		public TreasurePile2Addon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


		}

		public TreasurePile2Addon( Serial serial ) : base( serial )
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

	public class TreasurePile2AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new TreasurePile2Addon();
			}
		}

		[Constructable]
		public TreasurePile2AddonDeed()
		{
			ItemID = 0x0E41;
			Weight = 50.0;
			Hue = 2989;
			Name = string.Format(TreasurePileAddonStringConstants.DEED_NAME_FORMAT, 7); // Extended02 - #7
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add(1070722, string.Format("<BASEFONT COLOR=#8be4fc>{0}", TreasurePileAddonStringConstants.TYPE_EXTENDED)); // [Estendido]
            list.Add(1070722, string.Format("<BASEFONT COLOR=#8be4fc>[{0}]", "Para adicionar, clique 2x no baú. Use um machado para remover."));

        }

		public TreasurePile2AddonDeed( Serial serial ) : base( serial )
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
using System;
using Server;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// PearlSkull - A mysterious skull that can be opened to reveal a mystical pearl.
	/// When double-clicked, opens to reveal a MysticalPearl item.
	/// </summary>
	public class PearlSkull : Item
	{
		#region Constructors

		[Constructable]
		public PearlSkull() : base( PearlSkullConstants.ITEM_ID_BASE )
		{
			ItemID = Utility.RandomList( 
				PearlSkullConstants.ITEM_ID_BASE, 
				PearlSkullConstants.ITEM_ID_SKULL_1, 
				PearlSkullConstants.ITEM_ID_SKULL_2, 
				PearlSkullConstants.ITEM_ID_SKULL_3, 
				PearlSkullConstants.ITEM_ID_SKULL_4 
			);
			
			string liquid = GetRandomLiquidDescriptor();
			Name = FishingStringConstants.SKULL_NAME_PREFIX + liquid;
			Weight = PearlSkullConstants.WEIGHT_SKULL;
		}

		public PearlSkull( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Core Methods

		/// <summary>
		/// Handles double-click to open skull and reveal pearl
		/// </summary>
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_MUST_BE_IN_PACK_USE );
				return;
			}

			from.AddToBackpack( new MysticalPearl() );
			from.SendMessage( FishingStringConstants.COLOR_ERROR, FishingStringConstants.MSG_FOUND_PEARL );
			Delete();
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Gets a random liquid descriptor for the skull name
		/// </summary>
		/// <returns>Random liquid descriptor string</returns>
		private string GetRandomLiquidDescriptor()
		{
			switch( Utility.RandomMinMax( PearlSkullConstants.RANDOM_LIQUID_MIN, PearlSkullConstants.RANDOM_LIQUID_MAX ) )
			{
				case 0:
				case 5:
					return FishingStringConstants.SKULL_LIQUID_STRANGE;
				case 1:
					return FishingStringConstants.SKULL_LIQUID_UNCOMMON;
				case 2:
					return FishingStringConstants.SKULL_LIQUID_BIZARRE;
				case 3:
					return FishingStringConstants.SKULL_LIQUID_CURIOUS;
				case 4:
					return FishingStringConstants.SKULL_LIQUID_PECULIAR;
				case 6:
					return FishingStringConstants.SKULL_LIQUID_ABNORMAL;
				default:
					return FishingStringConstants.SKULL_LIQUID_STRANGE;
			}
		}

		#endregion

		#region Serialization

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

		#endregion
	}
}

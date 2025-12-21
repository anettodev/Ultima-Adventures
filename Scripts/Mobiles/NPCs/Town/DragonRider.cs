using System;
using Server;
using Server.ContextMenus;
using System.Collections;
using System.Collections.Generic;

namespace Server.Mobiles
{
	/// <summary>
	/// Dragon Rider NPC - A specialized Citizen that rides a dragon mount.
	/// Overrides base Citizen behavior to disable player range sensitivity and interaction.
	/// </summary>
	public class DragonRider : Citizens
	{
		/// <summary>
		/// Gets a value indicating whether this NPC is sensitive to player range.
		/// Dragon Riders are not range sensitive, allowing them to persist regardless of player proximity.
		/// </summary>
		public override bool PlayerRangeSensitive { get { return false; } }

		/// <summary>
		/// Initializes a new instance of the DragonRider class.
		/// Clears backpack and resets all citizen properties to default values.
		/// </summary>
		[Constructable]
		public DragonRider()
		{
			if ( Backpack != null ){ Backpack.Delete(); }
			CitizenRumor = "";
			CitizenType = 0;
			CitizenCost = 0;
			CitizenService = 0;
			CitizenPhrase = "";
		}

		/// <summary>
		/// Called when a mobile moves within range.
		/// Dragon Riders do not respond to movement.
		/// </summary>
		/// <param name="m">The mobile that moved</param>
		/// <param name="oldLocation">The previous location of the mobile</param>
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
		}

		/// <summary>
		/// Gets the context menu entries for this NPC.
		/// Dragon Riders do not provide context menu options.
		/// </summary>
		/// <param name="from">The mobile requesting the context menu</param>
		/// <param name="list">The list to add context menu entries to</param>
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">The serialization reader</param>
		public DragonRider( Serial serial ) : base( serial )
		{
		}

		/// <summary>
		/// Serializes the DragonRider data
		/// </summary>
		/// <param name="writer">The writer to serialize to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the DragonRider data
		/// </summary>
		/// <param name="reader">The reader to deserialize from</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		/// <summary>
		/// Called when an item is dragged and dropped on this NPC.
		/// Dragon Riders do not accept any items.
		/// </summary>
		/// <param name="from">The mobile attempting to drop the item</param>
		/// <param name="dropped">The item being dropped</param>
		/// <returns>Always returns false to reject all items</returns>
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			return false;
		}
	}
}
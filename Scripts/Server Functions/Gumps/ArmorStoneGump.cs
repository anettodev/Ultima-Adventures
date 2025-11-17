using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Gumps;

namespace Server.Gumps
{
	/// <summary>
	/// Gump for selecting armor pieces from ArmorStone, filtered by material type.
	/// </summary>
	public class ArmorStoneGump : BaseItemSelectionGump<ArmorStone>
	{
		public ArmorStoneGump( ArmorStone stone ) : base( stone )
		{
		}

		protected override List<ItemEntry> GetItemEntries()
		{
			if ( m_Token != null && !m_Token.Deleted )
			{
				return InitializeItems( m_Token.Material );
			}
			return new List<ItemEntry>();
		}

		/// <summary>
		/// Initializes the list of armor items based on the material type.
		/// </summary>
		private static List<ItemEntry> InitializeItems( CraftResource material )
		{
			List<ItemEntry> items = new List<ItemEntry>();
			CraftResourceType resourceType = CraftResources.GetType( material );

			if ( resourceType == CraftResourceType.Metal )
			{
				// Metal materials can be used for: Ringmail, Chainmail, Plate armor
				AddMetalArmorItems( items, material );
			}
			else if ( resourceType == CraftResourceType.Leather )
			{
				// Leather materials can be used for: Leather, Studded armor
				AddLeatherArmorItems( items, material );
			}

			return items;
		}

		/// <summary>
		/// Adds metal armor items (Ringmail, Chainmail, Plate) to the list.
		/// </summary>
		private static void AddMetalArmorItems( List<ItemEntry> items, CraftResource material )
		{
			int buttonID = 0x100;

			// Ringmail Armor
			AddItem( items, ref buttonID, typeof( RingmailChest ), 0x13EC );
			AddItem( items, ref buttonID, typeof( RingmailArms ), 0x13EE );
			AddItem( items, ref buttonID, typeof( RingmailLegs ), 0x13F0 );
			AddItem( items, ref buttonID, typeof( RingmailGloves ), 0x13EB );

			// Chainmail Armor
			AddItem( items, ref buttonID, typeof( ChainChest ), 0x13BF );
			AddItem( items, ref buttonID, typeof( ChainLegs ), 0x13BE );
			AddItem( items, ref buttonID, typeof( ChainCoif ), 0x13BB );

			// Plate Armor
			AddItem( items, ref buttonID, typeof( PlateChest ), 0x1415 );
			AddItem( items, ref buttonID, typeof( PlateArms ), 0x1410 );
			AddItem( items, ref buttonID, typeof( PlateLegs ), 0x1411 );
			AddItem( items, ref buttonID, typeof( PlateGloves ), 0x1414 );
			AddItem( items, ref buttonID, typeof( PlateGorget ), 0x1413 );
			AddItem( items, ref buttonID, typeof( PlateHelm ), 0x1412 );
			AddItem( items, ref buttonID, typeof( FemalePlateChest ), 0x1C04 );

			// Helmets (can be made with metal)
			AddItem( items, ref buttonID, typeof( Bascinet ), 0x140C );
			AddItem( items, ref buttonID, typeof( CloseHelm ), 0x1408 );
			AddItem( items, ref buttonID, typeof( Helmet ), 0x140A );
			AddItem( items, ref buttonID, typeof( NorseHelm ), 0x140E );

			// Shields (can be made with metal)
			AddItem( items, ref buttonID, typeof( Buckler ), 0x1B73 );
		}

		/// <summary>
		/// Adds leather armor items (Leather, Studded) to the list.
		/// </summary>
		private static void AddLeatherArmorItems( List<ItemEntry> items, CraftResource material )
		{
			int buttonID = 0x100;

			// Leather Armor
			AddItem( items, ref buttonID, typeof( LeatherChest ), 0x13CC );
			AddItem( items, ref buttonID, typeof( LeatherArms ), 0x13CD );
			AddItem( items, ref buttonID, typeof( LeatherLegs ), 0x13CB );
			AddItem( items, ref buttonID, typeof( LeatherGloves ), 0x13C6 );
			AddItem( items, ref buttonID, typeof( LeatherGorget ), 0x13C7 );
			AddItem( items, ref buttonID, typeof( LeatherCap ), 0x1DB9 );
			AddItem( items, ref buttonID, typeof( FemaleLeatherChest ), 0x1C06 );

			// Studded Armor
			AddItem( items, ref buttonID, typeof( StuddedChest ), 0x13DB );
			AddItem( items, ref buttonID, typeof( StuddedArms ), 0x13DC );
			AddItem( items, ref buttonID, typeof( StuddedLegs ), 0x13DA );
			AddItem( items, ref buttonID, typeof( StuddedGloves ), 0x13D5 );
			AddItem( items, ref buttonID, typeof( StuddedGorget ), 0x13D6 );
			AddItem( items, ref buttonID, typeof( FemaleStuddedChest ), 0x1C02 );
		}

		/// <summary>
		/// Helper method to add an armor item to the list.
		/// </summary>
		private static void AddItem( List<ItemEntry> items, ref int buttonID, Type armorType, int itemID )
		{
			ItemEntry entry = new ItemEntry( buttonID, armorType, 0, 0, itemID, 0x0, 18, 8 );
			items.Add( entry );
			buttonID++;
		}

		protected override void OnItemSelected( Mobile from, ArmorStone stone, ItemEntry entry )
		{
			if ( stone == null || stone.Deleted || from == null )
				return;

			if ( entry.ItemTypes == null || entry.ItemTypes.Length == 0 )
				return;

			Container backpack = from.Backpack;
			if ( backpack == null )
			{
				from.SendMessage( "You need a backpack to receive armor." );
				return;
			}

			// Create and give the armor item
			Item item = null;
			foreach ( Type type in entry.ItemTypes )
			{
				try
				{
					item = Activator.CreateInstance( type ) as Item;
					if ( item != null )
					{
						// Set the resource for armor items
						if ( item is BaseArmor )
						{
							BaseArmor armor = (BaseArmor)item;
							armor.Resource = stone.Material;
							// Mark as made by Developer
							armor.Crafter = Server.Mobiles.DeveloperMobile.Instance;
							armor.PlayerConstructed = true;
							
							// Buckler shield should have SpellChanneling to prevent unequipping when casting spells
							if ( armor is Buckler )
							{
								armor.Attributes.SpellChanneling = 1;
							}
						}

						from.AddToBackpack( item );
						string itemName = item.Name;
						if ( String.IsNullOrEmpty( itemName ) && item.LabelNumber > 0 )
							itemName = String.Format( "#{0}", item.LabelNumber );
						
						from.SendMessage( "You received: {0}", itemName );
					}
				}
				catch ( Exception ex )
				{
					Console.WriteLine( ex.Message );
					Console.WriteLine( ex.StackTrace );
				}
			}
		}

		protected override void AddItemEntry( ItemEntry entry, int x, int y )
		{
			AddImageTiledButton( x, y, ButtonNormalID, ButtonPressedID, entry.ButtonID, GumpButtonType.Reply, 0, entry.ImageID, entry.ImageHue, entry.ImageOffsetX, entry.ImageOffsetY );
			
			// Get item name for display
			string itemName = "Unknown Item";
			if ( entry.ItemTypes != null && entry.ItemTypes.Length > 0 )
			{
				try
				{
					Item tempItem = Activator.CreateInstance( entry.ItemTypes[0] ) as Item;
					if ( tempItem != null )
					{
						itemName = tempItem.Name;
						if ( String.IsNullOrEmpty( itemName ) && tempItem.LabelNumber > 0 )
							itemName = String.Format( "#{0}", tempItem.LabelNumber );
						tempItem.Delete();
					}
				}
				catch
				{
					// Use type name as fallback
					itemName = entry.ItemTypes[0].Name;
				}
			}

			AddHtml( x + 84, y, ItemWidth, ItemHeight, String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", TextColor, itemName ), false, false );
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Token == null || m_Token.Deleted || info.ButtonID == 0 )
				return;

			ItemEntry entry;
			if ( m_ItemLookup.TryGetValue( info.ButtonID, out entry ) )
			{
				if ( entry.ItemTypes != null && entry.ItemTypes.Length > 0 )
				{
					OnItemSelected( sender.Mobile, m_Token, entry );
				}
				else
				{
					sender.Mobile.SendLocalizedMessage( 501311 ); // This option is currently disabled, while we evaluate it for game balance.
				}
			}
		}
	}
}


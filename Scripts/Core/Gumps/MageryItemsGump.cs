using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Gumps
{
	public class MageryItemsGump : BaseItemSelectionGump<MageryTestToken>
	{
		private static List<ItemEntry> m_AllItems;

		static MageryItemsGump()
		{
			InitializeItems();
		}

		public MageryItemsGump( MageryTestToken token ) : base( token )
		{
		}

		protected override List<ItemEntry> GetItemEntries()
		{
			return m_AllItems;
		}

		protected override void OnItemSelected( Mobile from, MageryTestToken token, ItemEntry entry )
		{
			if ( token == null || token.Deleted || from == null )
				return;

			// Create and give the item directly (for testing purposes)
			if ( entry.ItemTypes != null && entry.ItemTypes.Length > 0 )
			{
				Item item = null;
				foreach ( Type type in entry.ItemTypes )
				{
					try
					{
						item = Activator.CreateInstance( type ) as Item;
						if ( item != null )
						{
							from.AddToBackpack( item );
							string itemName = item.Name;
							if ( String.IsNullOrEmpty( itemName ) && item.LabelNumber > 0 )
								itemName = String.Format( "#{0}", item.LabelNumber );
							from.SendMessage( "You receive: {0}", itemName );
						}
					}
					catch ( Exception ex )
					{
						Console.WriteLine( ex.Message );
						Console.WriteLine( ex.StackTrace );
					}
				}
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Token == null || m_Token.Deleted || info.ButtonID == 0 )
				return;

			ItemEntry entry;
			if ( m_ItemLookup.TryGetValue( info.ButtonID, out entry ) )
			{
				// For test gump, we don't require NameCliloc > 0
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

		private static void InitializeItems()
		{
			m_AllItems = new List<ItemEntry>();

			// Bag of Reagents - ItemID: 0xE76
			AddItem( 0x100, typeof( BagOfReagents ), 0, 0, 0xE76, 0x0, 18, 8 );

			// Full Magery Spellbook - ItemID: 0xEFA
			AddItem( 0x101, typeof( FullMagerySpellbook ), 0, 0, 0xEFA, 0x0, 18, 8 );

			// Magical Wizards Hat - ItemID: 0x1540
			AddItem( 0x102, typeof( MagicalWizardsHat ), 0, 0, 0x1540, 0x0, 18, 8 );

			// Wizard Staff - ItemID: 0x0908
			AddItem( 0x103, typeof( WizardStaff ), 0, 0, 0x0908, 0xB3A, 18, 8 );
		}

		private static void AddItem( int buttonID, Type itemType, int nameCliloc, int tooltipCliloc, int imageID, int imageHue, int imageOffsetX, int imageOffsetY )
		{
			ItemEntry entry = new ItemEntry( buttonID, itemType, nameCliloc, tooltipCliloc, imageID, imageHue, imageOffsetX, imageOffsetY );
			m_AllItems.Add( entry );
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
	}
}


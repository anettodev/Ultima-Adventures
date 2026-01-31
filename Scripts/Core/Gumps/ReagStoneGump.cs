using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Gumps;

namespace Server.Gumps
{
	/// <summary>
	/// Gump for selecting magery items from ReagStone with cooldown and guard rail validation.
	/// </summary>
	public class ReagStoneGump : BaseItemSelectionGump<ReagStone>
	{
		private static List<ItemEntry> m_AllItems;

		static ReagStoneGump()
		{
			InitializeItems();
		}

		public ReagStoneGump( ReagStone stone ) : base( stone )
		{
		}

		protected override List<ItemEntry> GetItemEntries()
		{
			return m_AllItems;
		}

		protected override void OnItemSelected( Mobile from, ReagStone stone, ItemEntry entry )
		{
			if ( stone == null || stone.Deleted || from == null )
				return;

			if ( entry.ItemTypes == null || entry.ItemTypes.Length == 0 )
				return;

			// Handle BagOfReagents specially - always check cooldown + guard rails (players accumulate these)
			if ( entry.ItemTypes[0] == typeof( BagOfReagents ) )
			{
				HandleBagOfReagents( from, stone );
				return;
			}

			// Handle other items (Spellbook, Hat, Staff) - check if player already has it
			Container backpack = from.Backpack;
			if ( backpack == null )
				return;

			// Check if player already has this item type
			Item existingItem = backpack.FindItemByType( entry.ItemTypes[0], true );
			bool playerAlreadyHasItem = ( existingItem != null );

			// If player already has the item, check cooldown to prevent accumulation
			if ( playerAlreadyHasItem )
			{
				if ( stone.CheckCooldownForGump( from ) )
					return;
			}
			// If player doesn't have it, give it without cooldown (first-time setup)

			// Create and give the item
			Item item = null;
			foreach ( Type type in entry.ItemTypes )
			{
				try
				{
					item = Activator.CreateInstance( type ) as Item;
					if ( item != null )
					{
						// For FullMagerySpellbook, ensure it's full
						if ( item is FullMagerySpellbook )
						{
							FullMagerySpellbook spellbook = (FullMagerySpellbook)item;
							spellbook.Content = ulong.MaxValue;
						}
						// For regular Spellbook, fill it
						else if ( item is Spellbook )
						{
							Spellbook spellbook = (Spellbook)item;
							if ( spellbook.BookCount == 64 )
							{
								spellbook.Content = ulong.MaxValue;
							}
							else
							{
								spellbook.Content = (1ul << spellbook.BookCount) - 1;
							}
						}
						// For stackable items, give a reasonable amount
						else if ( item is BlankScroll )
						{
							BlankScroll scroll = (BlankScroll)item;
							scroll.Amount = 100; // Give 100 blank scrolls for inscription training
						}
						else if ( item is MageEye )
						{
							MageEye eye = (MageEye)item;
							eye.Amount = 50; // Give 50 mage eyes for spell testing
						}
						else if ( item is Bandage )
						{
							Bandage bandage = (Bandage)item;
							bandage.Amount = 100; // Give 100 bandages for healing
						}
						else if ( item is Arrow )
						{
							Arrow arrow = (Arrow)item;
							arrow.Amount = 100; // Give 100 arrows for archery
						}

						from.AddToBackpack( item );
						string itemName = item.Name;
						if ( String.IsNullOrEmpty( itemName ) && item.LabelNumber > 0 )
							itemName = String.Format( "#{0}", item.LabelNumber );
						
						if ( playerAlreadyHasItem )
						{
							from.SendMessage( "Você recebeu: {0} (cooldown aplicado para evitar acúmulo)", itemName );
							// Apply cooldown only if player already had the item (prevent accumulation)
							stone.ApplyCooldown( from );
						}
						else
						{
							from.SendMessage( "Você recebeu: {0} (primeira vez - sem cooldown)", itemName );
							// No cooldown for first-time item acquisition
						}
					}
				}
				catch ( Exception ex )
				{
					Console.WriteLine( ex.Message );
					Console.WriteLine( ex.StackTrace );
				}
			}
		}

		/// <summary>
		/// Handles BagOfReagents selection using ReagStone's existing logic.
		/// </summary>
		private void HandleBagOfReagents( Mobile from, ReagStone stone )
		{
			Container backpack = from.Backpack;
			if ( backpack == null )
				return;

			// Check guard rails for reagents specifically
			if ( stone.CheckGuardRailsForGump( from ) )
				return;

			// Use ReagStone's existing method to find or create bag
			BagOfReagents reagentBag = stone.FindOrCreateReagentBagForGump( backpack );
			if ( reagentBag == null )
			{
				from.SendMessage( "Não foi possível criar ou encontrar uma bolsa de reagentes." );
				return;
			}

			// Replenish reagents using ReagStone's existing method
			bool replenished = stone.ReplenishReagentsForGump( backpack, reagentBag );

			if ( replenished )
			{
				// Apply cooldown after successful reagent replenishment
				stone.ApplyCooldown( from );
				from.SendMessage( "Você recebeu reagentes de magia." );
			}
			else
			{
				from.SendMessage( "Você já possui reagentes suficientes (55 de cada tipo)." );
			}
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

		private static void InitializeItems()
		{
			m_AllItems = new List<ItemEntry>();

			// Bag of Reagents - ItemID: 0xE76
			AddItem( 0x100, typeof( BagOfReagents ), 0, 0, 0xE76, 0x0, 18, 8 );

			// Full Magery Spellbook - ItemID: 0xEFA
			AddItem( 0x101, typeof( FullMagerySpellbook ), 0, 0, 0xEFA, 0x0, 18, 8 );

			// Magical Wizards Hat - ItemID: 0x1718
			//AddItem( 0x102, typeof( MagicWizardsHat ), 0, 0, 0x1718, 0x0, 18, 8 );

			// Wizard Staff - ItemID: 0x0908
			AddItem( 0x103, typeof( WizardStaff ), 0, 0, 0x0908, 0xB3A, 18, 8 );

			// Blank Scroll - ItemID: 0xEF3 (for inscription training)
			AddItem( 0x104, typeof( BlankScroll ), 0, 0, 0xEF3, 0x0, 18, 8 );

			// Recall Rune - ItemID: 0x1F14 (for testing Recall spell)
			AddItem( 0x105, typeof( RecallRune ), 0, 0, 0x1F14, 0x0, 18, 8 );

			// Mage Eye - ItemID: 0xF19 (for spell target testing)
			AddItem( 0x106, typeof( MageEye ), 0, 0, 0xF19, 0xB78, 18, 8 );

			// Bandage - ItemID: 0xE21 (for healing)
			AddItem( 0x107, typeof( Bandage ), 0, 0, 0xE21, 0x0, 18, 8 );

			// Arrow - ItemID: 0xF3F (for archery)
			AddItem( 0x108, typeof( Arrow ), 0, 0, 0xF3F, 0x0, 18, 8 );
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


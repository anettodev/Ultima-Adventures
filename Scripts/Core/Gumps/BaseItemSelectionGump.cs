using System;
using System.Collections.Generic;
using Server;
using Server.Network;

namespace Server.Gumps
{
	public abstract class BaseItemSelectionGump<T> : Gump where T : Item
	{
		// Default layout constants (can be overridden)
		protected virtual int GumpX { get { return 60; } }
		protected virtual int GumpY { get { return 36; } }
		protected virtual int GumpWidth { get { return 520; } }
		protected virtual int GumpHeight { get { return 404; } }
		protected virtual int BorderSize { get { return 10; } }
		protected virtual int HeaderHeight { get { return 20; } }
		protected virtual int FooterHeight { get { return 20; } }
		protected virtual int ContentStartY { get { return 40; } }
		protected virtual int ContentHeight { get { return 324; } }
		protected virtual int ItemStartX { get { return 14; } }
		protected virtual int ItemStartY { get { return 44; } }
		protected virtual int ItemWidth { get { return 250; } }
		protected virtual int ItemHeight { get { return 60; } }
		protected virtual int ItemSpacingX { get { return 250; } }
		protected virtual int ItemSpacingY { get { return 64; } }
		protected virtual int ItemsPerPage { get { return 8; } }
		protected virtual int ItemsPerRow { get { return 2; } }

		// Default Gump IDs (can be overridden)
		protected virtual int BackgroundID { get { return 0x13BE; } }
		protected virtual int TileID { get { return 0xA40; } }
		protected virtual int ButtonNormalID { get { return 0x918; } }
		protected virtual int ButtonPressedID { get { return 0x919; } }
		protected virtual int CancelButtonNormalID { get { return 0xFB1; } }
		protected virtual int CancelButtonPressedID { get { return 0xFB2; } }
		protected virtual int NextButtonNormalID { get { return 0xFA5; } }
		protected virtual int NextButtonPressedID { get { return 0xFA7; } }
		protected virtual int BackButtonNormalID { get { return 0xFAE; } }
		protected virtual int BackButtonPressedID { get { return 0xFB0; } }

		// Default text colors
		protected virtual int TextColor { get { return 0x7FFF; } }

		protected T m_Token;
		protected Dictionary<int, ItemEntry> m_ItemLookup;
		protected List<ItemEntry> m_AllItems;

		public BaseItemSelectionGump( T token ) : base( 60, 36 )
		{
			m_Token = token;
			m_AllItems = GetItemEntries();
			m_ItemLookup = new Dictionary<int, ItemEntry>();

			foreach ( ItemEntry entry in m_AllItems )
			{
				m_ItemLookup[entry.ButtonID] = entry;
			}

			BuildGump();
		}

		// Abstract methods that must be implemented by derived classes
		protected abstract List<ItemEntry> GetItemEntries();
		protected abstract void OnItemSelected( Mobile from, T token, ItemEntry entry );

		// Virtual methods that can be overridden for customization
		protected virtual int GetTitleCliloc() { return 1075576; } // Choose your item from the following pages
		protected virtual int GetCancelCliloc() { return 1060051; } // CANCEL
		protected virtual int GetNextCliloc() { return 1043353; } // Next
		protected virtual int GetBackCliloc() { return 1011393; } // Back

		private void BuildGump()
		{
			AddPage( 0 );
			AddGumpHeader();
			BuildAllPages();
		}

		private void AddGumpHeader()
		{
			AddBackground( 0, 0, GumpWidth, GumpHeight, BackgroundID );
			AddImageTiled( BorderSize, BorderSize, GumpWidth - (BorderSize * 2), HeaderHeight, TileID );
			AddImageTiled( BorderSize, ContentStartY, GumpWidth - (BorderSize * 2), ContentHeight, TileID );
			AddImageTiled( BorderSize, GumpHeight - BorderSize - FooterHeight, GumpWidth - (BorderSize * 2), FooterHeight, TileID );
			AddAlphaRegion( BorderSize, BorderSize, GumpWidth - (BorderSize * 2), GumpHeight - (BorderSize * 2) );
			AddCancelButton();
			AddHtmlLocalized( BorderSize + 4, BorderSize + 2, GumpWidth - (BorderSize * 2) - 8, HeaderHeight, GetTitleCliloc(), TextColor, false, false );
		}

		private void AddCancelButton()
		{
			AddButton( BorderSize, GumpHeight - BorderSize - FooterHeight, CancelButtonNormalID, CancelButtonPressedID, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( BorderSize + 35, GumpHeight - BorderSize - FooterHeight + 2, 450, FooterHeight, GetCancelCliloc(), TextColor, false, false );
		}

		private void BuildAllPages()
		{
			int totalPages = (int)Math.Ceiling( (double)m_AllItems.Count / ItemsPerPage );

			for ( int page = 1; page <= totalPages; page++ )
			{
				BuildPage( page, totalPages );
			}
		}

		private void BuildPage( int pageNumber, int totalPages )
		{
			AddPage( pageNumber );

			int startIndex = (pageNumber - 1) * ItemsPerPage;
			int endIndex = Math.Min( startIndex + ItemsPerPage, m_AllItems.Count );

			for ( int i = startIndex; i < endIndex; i++ )
			{
				ItemEntry entry = m_AllItems[i];
				int itemIndex = i - startIndex;
				int row = itemIndex / ItemsPerRow;
				int col = itemIndex % ItemsPerRow;

				int x = ItemStartX + (col * ItemSpacingX);
				int y = ItemStartY + (row * ItemSpacingY);

				AddItemEntry( entry, x, y );
			}

			AddPageNavigation( pageNumber, totalPages );
		}

		protected virtual void AddItemEntry( ItemEntry entry, int x, int y )
		{
			AddImageTiledButton( x, y, ButtonNormalID, ButtonPressedID, entry.ButtonID, GumpButtonType.Reply, 0, entry.ImageID, entry.ImageHue, entry.ImageOffsetX, entry.ImageOffsetY );
			AddTooltip( entry.TooltipCliloc );
			AddHtmlLocalized( x + 84, y, ItemWidth, ItemHeight, entry.NameCliloc, TextColor, false, false );
		}

		private void AddPageNavigation( int currentPage, int totalPages )
		{
			int footerY = GumpHeight - BorderSize - FooterHeight;

			if ( currentPage > 1 )
			{
				AddButton( 300, footerY, BackButtonNormalID, BackButtonPressedID, 0, GumpButtonType.Page, currentPage - 1 );
				AddHtmlLocalized( 340, footerY + 2, 60, FooterHeight, GetBackCliloc(), TextColor, false, false );
			}

			if ( currentPage < totalPages )
			{
				AddButton( 400, footerY, NextButtonNormalID, NextButtonPressedID, 0, GumpButtonType.Page, currentPage + 1 );
				AddHtmlLocalized( 440, footerY + 2, 60, FooterHeight, GetNextCliloc(), TextColor, false, false );
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Token == null || m_Token.Deleted || info.ButtonID == 0 )
				return;

			ItemEntry entry;
			if ( m_ItemLookup.TryGetValue( info.ButtonID, out entry ) )
			{
				if ( entry.ItemTypes != null && entry.ItemTypes.Length > 0 && entry.NameCliloc > 0 )
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


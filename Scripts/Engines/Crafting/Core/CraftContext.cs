using System;
using System.Collections.Generic;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Options for how maker's mark should be handled when crafting
	/// </summary>
	public enum CraftMarkOption
	{
		/// <summary>Always mark crafted items</summary>
		MarkItem,
		/// <summary>Never mark crafted items</summary>
		DoNotMark,
		/// <summary>Prompt the player each time</summary>
		PromptForMark
	}

	/// <summary>
	/// Options for quantity selection when crafting multiple items
	/// </summary>
	public enum CraftQtdOption
	{
		/// <summary>Create one item</summary>
		One,
		/// <summary>Create five items</summary>
		Five,
		/// <summary>Create ten items</summary>
		Ten,
		/// <summary>Create twenty-five items</summary>
		TwentyFive,
		/// <summary>Create fifty items</summary>
		Fifty
	}

	/// <summary>
	/// Tracks the crafting context for a player, including recently made items and preferences
	/// </summary>
	public class CraftContext
	{
		#region Constants

		/// <summary>Maximum number of recently made items to track</summary>
		private const int MAX_RECENT_ITEMS = 10;

		/// <summary>Index of the last item when list is at maximum capacity</summary>
		private const int LAST_ITEM_INDEX = 9;

		/// <summary>Default index value indicating no selection</summary>
		private const int NO_INDEX = -1;

		#endregion

		#region Fields

		private List<CraftItem> m_Items;
		private int m_LastResourceIndex;
		private int m_LastResourceIndex2;
		private int m_LastGroupIndex;
		private bool m_DoNotColor;
		private CraftMarkOption m_MarkOption;
		private CraftQtdOption m_QtdOption;

		#endregion

		#region Properties

		/// <summary>
		/// List of recently made craft items (most recent first, max 10 items)
		/// </summary>
		public List<CraftItem> Items { get { return m_Items; } }

		/// <summary>
		/// Index of the last selected primary resource
		/// </summary>
		public int LastResourceIndex{ get{ return m_LastResourceIndex; } set{ m_LastResourceIndex = value; } }

		/// <summary>
		/// Index of the last selected secondary resource
		/// </summary>
		public int LastResourceIndex2{ get{ return m_LastResourceIndex2; } set{ m_LastResourceIndex2 = value; } }

		/// <summary>
		/// Index of the last selected craft group
		/// </summary>
		public int LastGroupIndex{ get{ return m_LastGroupIndex; } set{ m_LastGroupIndex = value; } }

		/// <summary>
		/// Whether to disable color retention from resources
		/// </summary>
		public bool DoNotColor{ get{ return m_DoNotColor; } set{ m_DoNotColor = value; } }

		/// <summary>
		/// Maker's mark preference for this context
		/// </summary>
		public CraftMarkOption MarkOption{ get{ return m_MarkOption; } set{ m_MarkOption = value; } }

		/// <summary>
		/// Quantity selection preference for this context
		/// </summary>
		public CraftQtdOption QtdOption { get { return m_QtdOption; } set { m_QtdOption = value; } }

		/// <summary>
		/// The most recently made craft item
		/// </summary>
		public CraftItem LastMade
		{
			get
			{
				if ( m_Items.Count > 0 )
					return m_Items[0];

				return null;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CraftContext class
		/// </summary>
		public CraftContext()
		{
			m_Items = new List<CraftItem>();
			m_LastResourceIndex = NO_INDEX;
			m_LastResourceIndex2 = NO_INDEX;
			m_LastGroupIndex = NO_INDEX;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Records that a craft item was made, adding it to the recent items list
		/// </summary>
		/// <param name="item">The craft item that was made</param>
		public void OnMade( CraftItem item )
		{
			m_Items.Remove( item );

			// Remove oldest item if list is at maximum capacity
			if ( m_Items.Count == MAX_RECENT_ITEMS )
				m_Items.RemoveAt( LAST_ITEM_INDEX );

			// Add new item at the beginning (most recent first)
			m_Items.Insert( 0, item );
		}

		#endregion
	}
}
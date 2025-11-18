using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Represents a group (category) of craftable items within a crafting system
	/// </summary>
	public class CraftGroup
	{
		#region Fields

		private CraftItemCol m_arCraftItem;
		private string m_NameString;
		private int m_NameNumber;

		#endregion

		#region Properties

		/// <summary>
		/// Collection of craft items in this group
		/// </summary>
		public CraftItemCol CraftItems
		{
			get { return m_arCraftItem; }
		}

		/// <summary>
		/// The string name of this group
		/// </summary>
		public string NameString
		{
			get { return m_NameString; }
		}

		/// <summary>
		/// The localized name number of this group
		/// </summary>
		public int NameNumber
		{
			get { return m_NameNumber; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CraftGroup class
		/// </summary>
		/// <param name="groupName">The name of the group (TextDefinition)</param>
		public CraftGroup( TextDefinition groupName )
		{
			m_NameNumber = groupName;
			m_NameString = groupName;
			m_arCraftItem = new CraftItemCol();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a craft item to this group
		/// </summary>
		/// <param name="craftItem">The craft item to add</param>
		public void AddCraftItem( CraftItem craftItem )
		{
			m_arCraftItem.Add( craftItem );
		}

		#endregion
	}
}
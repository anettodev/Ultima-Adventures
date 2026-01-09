using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Collection of sub-resources (e.g., metal types) for a crafting system
	/// </summary>
	public class CraftSubResCol : System.Collections.CollectionBase
	{
		#region Fields

		private Type m_Type;
		private string m_NameString;
		private int m_NameNumber;
		private bool m_Init;

		#endregion

		#region Properties

		/// <summary>
		/// Whether this sub-resource collection has been initialized
		/// </summary>
		public bool Init
		{
			get { return m_Init; }
			set { m_Init = value; }
		}

		/// <summary>
		/// The base resource type for this collection
		/// </summary>
		public Type ResType
		{
			get { return m_Type; }
			set { m_Type = value; }
		}

		/// <summary>
		/// The string name of this sub-resource collection
		/// </summary>
		public string NameString
		{
			get { return m_NameString; }
			set { m_NameString = value; }
		}

		/// <summary>
		/// The localized name number of this sub-resource collection
		/// </summary>
		public int NameNumber
		{
			get { return m_NameNumber; }
			set { m_NameNumber = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CraftSubResCol class
		/// </summary>
		public CraftSubResCol()
		{
			m_Init = false;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a sub-resource to the collection
		/// </summary>
		/// <param name="craftSubRes">The sub-resource to add</param>
		public void Add( CraftSubRes craftSubRes )
		{
			List.Add( craftSubRes );
		}

		/// <summary>
		/// Removes a sub-resource from the collection at the specified index
		/// </summary>
		/// <param name="index">The index of the sub-resource to remove</param>
		public void Remove( int index )
		{
			if ( index > Count - 1 || index < 0 )
			{
				// Invalid index, do nothing
			}
			else
			{
				List.RemoveAt( index );
			}
		}

		/// <summary>
		/// Gets the sub-resource at the specified index
		/// </summary>
		/// <param name="index">The index of the sub-resource to retrieve</param>
		/// <returns>The sub-resource at the specified index</returns>
		public CraftSubRes GetAt( int index )
		{
			return ( CraftSubRes ) List[index];
		}

		/// <summary>
		/// Searches for a sub-resource by type
		/// </summary>
		/// <param name="type">The type to search for</param>
		/// <returns>The sub-resource if found, otherwise null</returns>
		public CraftSubRes SearchFor( Type type )
		{
			for ( int i = 0; i < List.Count; i++ )
			{
				CraftSubRes craftSubRes = ( CraftSubRes )List[i];
				if ( craftSubRes.ItemType == type )
				{
					return craftSubRes;
				}
			}
			return null;
		}

		#endregion
	}
}
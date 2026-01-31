using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Collection of craftable items for a crafting system
	/// </summary>
	public class CraftItemCol : System.Collections.CollectionBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CraftItemCol class
		/// </summary>
		public CraftItemCol()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a craft item to the collection
		/// </summary>
		/// <param name="craftItem">The craft item to add</param>
		/// <returns>The index at which the item was added</returns>
		public int Add( CraftItem craftItem )
		{
			return List.Add( craftItem );
		}

		/// <summary>
		/// Removes a craft item from the collection at the specified index
		/// </summary>
		/// <param name="index">The index of the item to remove</param>
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
		/// Gets the craft item at the specified index
		/// </summary>
		/// <param name="index">The index of the item to retrieve</param>
		/// <returns>The craft item at the specified index</returns>
		public CraftItem GetAt( int index )
		{
			return ( CraftItem ) List[index];
		}

		/// <summary>
		/// Searches for a craft item by exact type match
		/// </summary>
		/// <param name="type">The type to search for</param>
		/// <returns>The craft item if found, otherwise null</returns>
		public CraftItem SearchFor( Type type )
		{
			for ( int i = 0; i < List.Count; i++ )
			{
				CraftItem craftItem = ( CraftItem )List[i];
				if ( craftItem.ItemType == type )
				{
					return craftItem;
				}
			}
			return null;
		}

		/// <summary>
		/// Searches for a craft item by type, including subclass matches
		/// </summary>
		/// <param name="type">The type to search for</param>
		/// <returns>The craft item if found (exact match or subclass), otherwise null</returns>
		public CraftItem SearchForSubclass( Type type )
		{
			for ( int i = 0; i < List.Count; i++ )
			{
				CraftItem craftItem = ( CraftItem )List[i];

				if ( craftItem.ItemType == type || type.IsSubclassOf( craftItem.ItemType ) )
					return craftItem;
			}

			return null;
		}

		#endregion
	}
}
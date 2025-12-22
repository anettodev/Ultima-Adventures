using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Collection of craft groups (categories) for a crafting system
	/// </summary>
	public class CraftGroupCol : System.Collections.CollectionBase
	{
		#region Constants

		/// <summary>Return value indicating item not found in collection</summary>
		private const int NOT_FOUND = -1;

		/// <summary>Value indicating no localized name number</summary>
		private const int NO_NAME_NUMBER = 0;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CraftGroupCol class
		/// </summary>
		public CraftGroupCol()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a craft group to the collection
		/// </summary>
		/// <param name="craftGroup">The craft group to add</param>
		/// <returns>The index at which the group was added</returns>
		public int Add( CraftGroup craftGroup )
		{
			return List.Add( craftGroup );
		}

		/// <summary>
		/// Removes a craft group from the collection at the specified index
		/// </summary>
		/// <param name="index">The index of the group to remove</param>
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
		/// Gets the craft group at the specified index
		/// </summary>
		/// <param name="index">The index of the group to retrieve</param>
		/// <returns>The craft group at the specified index</returns>
		public CraftGroup GetAt( int index )
		{
			return ( CraftGroup ) List[index];
		}

		/// <summary>
		/// Searches for a craft group by name
		/// </summary>
		/// <param name="groupName">The name to search for (TextDefinition)</param>
		/// <returns>The index of the group if found, otherwise -1</returns>
		public int SearchFor( TextDefinition groupName )
		{
			for ( int i = 0; i < List.Count; i++ )
			{
				CraftGroup craftGroup = (CraftGroup)List[i];

				int nameNumber = craftGroup.NameNumber;
				string nameString = craftGroup.NameString;

				if ( ( nameNumber != NO_NAME_NUMBER && nameNumber == groupName.Number ) || ( nameString != null && nameString == groupName.String ) )
					return i;
			}

			return NOT_FOUND;
		}

		#endregion
	}
}
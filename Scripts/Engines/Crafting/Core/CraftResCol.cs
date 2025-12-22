using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Collection of resource requirements for a craftable item
	/// </summary>
	public class CraftResCol : System.Collections.CollectionBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CraftResCol class
		/// </summary>
		public CraftResCol()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a resource requirement to the collection
		/// </summary>
		/// <param name="craftRes">The resource requirement to add</param>
		public void Add( CraftRes craftRes )
		{
			List.Add( craftRes );
		}

		/// <summary>
		/// Removes a resource requirement from the collection at the specified index
		/// </summary>
		/// <param name="index">The index of the resource to remove</param>
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
		/// Gets the resource requirement at the specified index
		/// </summary>
		/// <param name="index">The index of the resource to retrieve</param>
		/// <returns>The resource requirement at the specified index</returns>
		public CraftRes GetAt( int index )
		{
			return ( CraftRes ) List[index];
		}

		#endregion
	}
}
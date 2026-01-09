using System;
using Server;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Attribute used to specify the item ID for a craftable item class
	/// </summary>
	[AttributeUsage( AttributeTargets.Class )]
	public class CraftItemIDAttribute : Attribute
	{
		#region Fields

		private int m_ItemID;

		#endregion

		#region Properties

		/// <summary>
		/// The item ID associated with this craftable item
		/// </summary>
		public int ItemID{ get{ return m_ItemID; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CraftItemIDAttribute class
		/// </summary>
		/// <param name="itemID">The item ID to associate with the craftable item</param>
		public CraftItemIDAttribute( int itemID )
		{
			m_ItemID = itemID;
		}

		#endregion
	}
}
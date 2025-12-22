using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Collection of skill requirements for a craftable item
	/// </summary>
	public class CraftSkillCol : System.Collections.CollectionBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CraftSkillCol class
		/// </summary>
		public CraftSkillCol()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a skill requirement to the collection
		/// </summary>
		/// <param name="craftSkill">The skill requirement to add</param>
		public void Add( CraftSkill craftSkill )
		{
			List.Add( craftSkill );
		}

		/// <summary>
		/// Removes a skill requirement from the collection at the specified index
		/// </summary>
		/// <param name="index">The index of the skill to remove</param>
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
		/// Gets the skill requirement at the specified index
		/// </summary>
		/// <param name="index">The index of the skill to retrieve</param>
		/// <returns>The skill requirement at the specified index</returns>
		public CraftSkill GetAt( int index )
		{
			return ( CraftSkill ) List[index];
		}

		#endregion
	}
}
using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Represents a skill requirement for crafting an item
	/// </summary>
	public class CraftSkill
	{
		#region Fields

		private SkillName m_SkillToMake;
		private double m_MinSkill;
		private double m_MaxSkill;

		#endregion

		#region Properties

		/// <summary>
		/// The skill required to craft the item
		/// </summary>
		public SkillName SkillToMake
		{
			get { return m_SkillToMake; }
		}

		/// <summary>
		/// The minimum skill value required
		/// </summary>
		public double MinSkill
		{
			get { return m_MinSkill; }
		}

		/// <summary>
		/// The maximum skill value that affects success chance
		/// </summary>
		public double MaxSkill
		{
			get { return m_MaxSkill; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CraftSkill class
		/// </summary>
		/// <param name="skillToMake">The skill required to craft</param>
		/// <param name="minSkill">The minimum skill value required</param>
		/// <param name="maxSkill">The maximum skill value that affects success chance</param>
		public CraftSkill( SkillName skillToMake, double minSkill, double maxSkill )
		{
			m_SkillToMake = skillToMake;
			m_MinSkill = minSkill;
			m_MaxSkill = maxSkill;
		}

		#endregion
	}
}
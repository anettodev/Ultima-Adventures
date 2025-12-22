using System;
using Server.Mobiles;

namespace Server.Spells
{
	/// <summary>
	/// Centralized helper for poison-related spell calculations
	/// Used by: Poison spell, Poison Field, Poison Strike, and other poison-based spells
	/// </summary>
	public static class PoisonHelper
	{
		#region Poison Level Thresholds

		/// <summary>
		/// Minimum combined Magery + Poisoning skill for Lethal poison (Level 4)
		/// </summary>
		public const int LETHAL_THRESHOLD = 240;

		/// <summary>
		/// Minimum Magery skill required for Deadly poison (Level 3)
		/// </summary>
		public const double MAGERY_FOR_DEADLY = 120.0;

		/// <summary>
		/// Minimum Poisoning skill required for Deadly poison (Level 3)
		/// </summary>
		public const double POISONING_FOR_DEADLY = 100.0;

		/// <summary>
		/// Minimum Magery skill required for Greater poison (Level 2)
		/// </summary>
		public const double MAGERY_FOR_GREATER = 100.0;

		/// <summary>
		/// Minimum Poisoning skill required for Greater poison (Level 2) - alternative to EvalInt
		/// </summary>
		public const double POISONING_FOR_GREATER = 80.0;

		/// <summary>
		/// Minimum EvalInt skill required for Greater poison (Level 2) - alternative to Poisoning
		/// </summary>
		public const double EVAL_FOR_GREATER = 100.0;

		/// <summary>
		/// Minimum Magery skill required for Regular poison (Level 1)
		/// </summary>
		public const double MAGERY_FOR_REGULAR = 80.0;

		/// <summary>
		/// Minimum Poisoning skill required for Regular poison (Level 1) - alternative to EvalInt
		/// </summary>
		public const double POISONING_FOR_REGULAR = 60.0;

		/// <summary>
		/// Minimum EvalInt skill required for Regular poison (Level 1) - alternative to Poisoning
		/// </summary>
		public const double EVAL_FOR_REGULAR = 80.0;

		#endregion

		#region Poison Level Calculation

		/// <summary>
		/// Calculates poison level based on caster's Magery, Poisoning, and EvalInt skills
		/// This is the standard algorithm used across all poison spells for consistency
		/// </summary>
		/// <param name="caster">The spell caster</param>
		/// <returns>Poison object with appropriate level (0-4)</returns>
		public static Poison CalculatePoisonLevel(Mobile caster)
		{
			double magery = caster.Skills[SkillName.Magery].Value;
			double poisoning = caster.Skills[SkillName.Poisoning].Value;
			double evalInt = caster.Skills[SkillName.EvalInt].Value;
			int total = (int)(magery + poisoning);

			// Level 4: Lethal - Requires very high combined skills
			if (total >= LETHAL_THRESHOLD)
				return Poison.Lethal;

			// Level 3: Deadly - Requires high Magery AND Poisoning
			else if (magery >= MAGERY_FOR_DEADLY && poisoning >= POISONING_FOR_DEADLY)
				return Poison.Deadly;

			// Level 2: Greater - Requires good Magery AND (Poisoning OR EvalInt)
			else if (magery >= MAGERY_FOR_GREATER && 
					(poisoning >= POISONING_FOR_GREATER || evalInt >= EVAL_FOR_GREATER))
				return Poison.Greater;

			// Level 1: Regular - Requires moderate Magery AND (Poisoning OR EvalInt)
			else if (magery >= MAGERY_FOR_REGULAR && 
					(poisoning >= POISONING_FOR_REGULAR || evalInt >= EVAL_FOR_REGULAR))
				return Poison.Regular;

			// Level 0: Lesser - Default for low skill casters
			else
				return Poison.Lesser;
		}

		/// <summary>
		/// Gets the appropriate poison level for friendly fire (reduced by 1 level)
		/// Used when a field affects allies/party members/pets
		/// </summary>
		/// <param name="normalPoison">The normal poison that would be applied</param>
		/// <returns>Reduced poison level</returns>
		public static Poison GetReducedPoisonLevel(Poison normalPoison)
		{
			int level = normalPoison.Level;
			
			// Reduce by 1 level, minimum 0 (Lesser)
			if (level > 0)
				level--;
			
			return Poison.GetPoison(level);
		}

		/// <summary>
		/// Gets poison level as integer (0-4) for display or comparison
		/// </summary>
		/// <param name="caster">The spell caster</param>
		/// <returns>Poison level (0=Lesser, 1=Regular, 2=Greater, 3=Deadly, 4=Lethal)</returns>
		public static int GetPoisonLevelAsInt(Mobile caster)
		{
			return CalculatePoisonLevel(caster).Level;
		}

		#endregion
	}
}


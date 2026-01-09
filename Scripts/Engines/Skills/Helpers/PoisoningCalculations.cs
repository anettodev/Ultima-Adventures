namespace Server.SkillHandlers
{
	/// <summary>
	/// Calculation methods for poison success chance and charges.
	/// Extracted to improve testability and maintainability.
	/// </summary>
	public static class PoisoningCalculations
	{
		/// <summary>
		/// Gets required skill for 90% success chance with poison level.
		/// </summary>
		public static double GetRequiredSkillForPoison( int poisonLevel )
		{
			switch ( poisonLevel )
			{
				case 0: return PoisoningConstants.SKILL_REQUIRED_LESSER;
				case 1: return PoisoningConstants.SKILL_REQUIRED_REGULAR;
				case 2: return PoisoningConstants.SKILL_REQUIRED_GREATER;
				case 3: return PoisoningConstants.SKILL_REQUIRED_DEADLY;
				case 4: return PoisoningConstants.SKILL_REQUIRED_LETHAL;
				default: return PoisoningConstants.SKILL_REQUIRED_LESSER;
			}
		}
		
		/// <summary>
		/// Gets base success chance cap for poison level.
		/// </summary>
		public static double GetBaseSuccessChance( int poisonLevel )
		{
			return ( poisonLevel == 4 ) 
				? PoisoningConstants.LETHAL_SUCCESS_CHANCE 
				: PoisoningConstants.BASE_SUCCESS_CHANCE;
		}
		
		/// <summary>
		/// Calculates success chance for applying poison based on skill.
		/// Success chance scales from 0% to the base chance (90% or 80% for Lethal).
		/// </summary>
		public static double CalculateSuccessChance( double currentSkill, int poisonLevel )
		{
			if ( currentSkill <= 0.0 )
				return 0.0;
			
			double requiredSkill = GetRequiredSkillForPoison( poisonLevel );
			double baseChance = GetBaseSuccessChance( poisonLevel );
			
			double chance = ( currentSkill / requiredSkill ) * baseChance;
			
			// Clamp between 0 and baseChance
			if ( chance > baseChance )
				chance = baseChance;
			if ( chance < 0.0 )
				chance = 0.0;
			
			return chance;
		}
		
		/// <summary>
		/// Calculates maximum poison charges based on skill.
		/// Each 10 skill points = 1 charge, capped at 12.
		/// </summary>
		public static int CalculateMaxCharges( double skill )
		{
			int charges = (int)( skill / PoisoningConstants.SKILL_POINTS_PER_CHARGE );
			
			if ( charges > PoisoningConstants.MAX_POISON_CHARGES )
				charges = PoisoningConstants.MAX_POISON_CHARGES;
			if ( charges < 0 )
				charges = 0;
			
			return charges;
		}
	}
}


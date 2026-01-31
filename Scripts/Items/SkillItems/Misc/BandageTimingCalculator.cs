using System;
using Server.Mobiles;
using Server.Misc;

namespace Server.Items
{
	/// <summary>
	/// Calculates bandage healing timing based on dexterity, player state, and target type.
	/// Extracted from Bandage.cs to improve maintainability and reduce complexity.
	/// </summary>
	public static class BandageTimingCalculator
	{
		/// <summary>
		/// Calculates the total time in milliseconds for a bandage heal to complete.
		/// </summary>
		/// <param name="healer">The mobile performing the healing</param>
		/// <param name="patient">The mobile being healed</param>
		/// <param name="primarySkill">The primary skill (Healing or Veterinary)</param>
		/// <param name="isEnhancedBandage">Whether enhanced bandage is being used (reduces time by 0.3-1.0s)</param>
		/// <returns>Time in milliseconds</returns>
		public static double CalculateBandageDelay(Mobile healer, Mobile patient, SkillName primarySkill, bool isEnhancedBandage = false)
		{
			if (healer == null || patient == null)
				return 0;

			double bandageSpeedMin = MyServerSettings.BandageSpeedMin();
			bool onSelf = (healer == patient);
			int dex = GetCappedDexterity(healer.Dex);

			double resDelay = patient.Alive ? 0.0 : bandageSpeedMin;
			double dexseconds = CalculateDexteritySeconds(healer, dex);

			double seconds;

			if (onSelf)
			{
				// Calculate self-healing based on other-healing time, then multiply
				double otherHealTime = CalculateOtherHealDelay(dex, dexseconds, resDelay, primarySkill);
				seconds = CalculateSelfHealDelay(otherHealTime);
			}
			else
			{
				seconds = CalculateOtherHealDelay(dex, dexseconds, resDelay, primarySkill);
			}

			// Apply 30% speed penalty (slower healing)
			seconds *= BandageConstants.BANDAGE_SPEED_MULTIPLIER;
			
			// Apply enhanced bandage speed bonus (random 0.3s to 1.0s reduction)
			if (isEnhancedBandage)
			{
				double speedBonus = Utility.RandomDouble() * 0.7 + 0.3; // Random between 0.3 and 1.0
				seconds -= speedBonus;
				if (seconds < 0)
					seconds = 0;
			}
			
			// Final bounds check for all scenarios (min 1.3s, max 7.2s)
			if (seconds < BandageConstants.HEALING_MIN_SECONDS)
				seconds = BandageConstants.HEALING_MIN_SECONDS;
			if (seconds > BandageConstants.HEALING_MAX_SECONDS)
				seconds = BandageConstants.HEALING_MAX_SECONDS;

			return seconds * BandageConstants.MILLISECONDS_PER_SECOND;
		}

		/// <summary>
		/// Caps dexterity at the maximum value for bandage calculations.
		/// </summary>
		private static int GetCappedDexterity(int dex)
		{
			return dex > BandageConstants.DEX_CAP_FOR_BANDAGE_SPEED
				? BandageConstants.DEX_CAP_FOR_BANDAGE_SPEED
				: dex;
		}

		/// <summary>
		/// Calculates the dexterity-based timing component with special handling for Midland and Soulbound.
		/// DEX bonus is 30% stronger (1.3x multiplier) for faster healing.
		/// </summary>
		private static double CalculateDexteritySeconds(Mobile healer, int dex)
		{
			double dexseconds;

			// Check for Midland special timing
			if (AdventuresFunctions.IsInMidland((object)healer) && healer is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)healer;
				double baseDexSeconds = ((BandageConstants.MIDLAND_DEX_BASE - (double)dex) / BandageConstants.MIDLAND_DEX_DIVISOR)
					* (1 - pm.Agility());
				// Apply 30% stronger DEX bonus
				dexseconds = baseDexSeconds * (1.0 - ((BandageConstants.DEX_BONUS_MULTIPLIER - 1.0) * (1.0 - ((double)dex / BandageConstants.DEX_CAP_FOR_BANDAGE_SPEED))));
			}
			// Check for Soulbound special timing
			else if (healer is PlayerMobile && ((PlayerMobile)healer).SoulBound)
			{
				double baseDexSeconds = BandageConstants.BANDAGE_BASE_SECONDS * (1 - (dex / (double)BandageConstants.SOULBOUND_DEX_DIVISOR));
				// Apply 30% stronger DEX bonus
				dexseconds = baseDexSeconds * (1.0 - ((BandageConstants.DEX_BONUS_MULTIPLIER - 1.0) * (1.0 - ((double)dex / BandageConstants.DEX_CAP_FOR_BANDAGE_SPEED))));
			}
			// Standard dexterity calculation with 30% stronger bonus
			else
			{
				// Base calculation with enhanced DEX effect (30% stronger)
				double dexFactor = ((double)dex / BandageConstants.DEX_CAP_FOR_BANDAGE_SPEED) * BandageConstants.DEX_BONUS_MULTIPLIER;
				if (dexFactor > 1.0)
					dexFactor = 1.0;
				dexseconds = BandageConstants.BANDAGE_BASE_SECONDS * (1.0 - dexFactor);
			}

			// Ensure dexseconds is never negative (possible with very high dex)
			return dexseconds < 0 ? 0 : dexseconds;
		}

		/// <summary>
		/// Calculates healing delay when healing yourself.
		/// Self-healing is 2.5x slower than other-healing.
		/// </summary>
		/// <param name="otherHealTime">The calculated time for healing others (before speed penalty)</param>
		/// <returns>Self-healing time (before speed penalty)</returns>
		private static double CalculateSelfHealDelay(double otherHealTime)
		{
			// Apply self-healing multiplier (2.5x slower than other-healing)
			double selfHealTime = otherHealTime * BandageConstants.SELF_HEALING_MULTIPLIER;
			
			// Ensure within bounds (min 1.0s, max 2.88s before speed penalty)
			// After 1.3x speed penalty: min = 1.0 * 1.3 = 1.3s, max = 2.88 * 1.3 = 3.744s
			// But we want max 7.2s, so: 7.2 / 1.3 = 5.54s before penalty
			// Actually, let's use: max = 7.2 / 1.3 = 5.54s before penalty
			double maxBeforePenalty = BandageConstants.HEALING_MAX_SECONDS / BandageConstants.BANDAGE_SPEED_MULTIPLIER;
			double minBeforePenalty = BandageConstants.HEALING_MIN_SECONDS / BandageConstants.BANDAGE_SPEED_MULTIPLIER;
			
			if (selfHealTime < minBeforePenalty)
				selfHealTime = minBeforePenalty;
			if (selfHealTime > maxBeforePenalty)
				selfHealTime = maxBeforePenalty;
			
			return selfHealTime;
		}

		/// <summary>
		/// Calculates healing delay when healing another mobile.
		/// </summary>
		private static double CalculateOtherHealDelay(int dex, double dexseconds, double resDelay, SkillName primarySkill)
		{
			// Veterinary healing has special timing
			if (Core.AOS && primarySkill == SkillName.Veterinary)
			{
				return CalculateVeterinaryDelay(dexseconds);
			}

			// AOS (Age of Shadows) timing
			if (Core.AOS)
			{
				return CalculateAOSDelay(dex, resDelay);
			}

			// Classic (pre-AOS) timing
			return CalculateClassicDelay(dex, resDelay);
		}

		/// <summary>
		/// Calculates veterinary-specific healing delay.
		/// Enhanced with 30% stronger DEX bonus and bounded to min 1.0s, max 2.4s.
		/// </summary>
		private static double CalculateVeterinaryDelay(double dexseconds)
		{
			// Enhanced dexseconds already has 30% bonus applied
			double baseTime;
			
			if (dexseconds >= BandageConstants.VET_DEXSECONDS_THRESHOLD)
			{
				baseTime = dexseconds + BandageConstants.VET_ADDITIONAL_SECONDS;
			}
			else
			{
				baseTime = BandageConstants.VET_DEFAULT_SECONDS;
			}
			
			// Ensure within bounds (min 1.0s, max 2.4s for other-healing)
			if (baseTime < BandageConstants.OTHER_HEALING_MIN_SECONDS)
				baseTime = BandageConstants.OTHER_HEALING_MIN_SECONDS;
			if (baseTime > BandageConstants.OTHER_HEALING_MAX_SECONDS)
				baseTime = BandageConstants.OTHER_HEALING_MAX_SECONDS;
			
			return baseTime;
		}

		/// <summary>
		/// Calculates AOS (Age of Shadows) healing delay.
		/// Enhanced with 30% stronger DEX bonus and bounded to min 1.0s, max 2.4s.
		/// </summary>
		private static double CalculateAOSDelay(int dex, double resDelay)
		{
			double baseTime;
			
			if (dex < BandageConstants.AOS_DEX_THRESHOLD)
			{
				// Enhanced formula with 30% stronger DEX bonus
				// Original: 3.2 - sin(dex/130) * 2.5
				// Enhanced: Apply DEX bonus multiplier for faster healing
				double dexRatio = (double)dex / BandageConstants.DEX_CAP_FOR_BANDAGE_SPEED;
				double enhancedDexFactor = dexRatio * BandageConstants.DEX_BONUS_MULTIPLIER;
				if (enhancedDexFactor > 1.0)
					enhancedDexFactor = 1.0;
				
				// Scale from max to min based on enhanced DEX
				double timeRange = BandageConstants.OTHER_HEALING_MAX_SECONDS - BandageConstants.OTHER_HEALING_MIN_SECONDS;
				baseTime = BandageConstants.OTHER_HEALING_MAX_SECONDS - (timeRange * enhancedDexFactor);
			}
			else
			{
				// High dex gets minimum delay
				baseTime = BandageConstants.OTHER_HEALING_MIN_SECONDS;
			}

			// Add resurrection delay if applicable
			double finalTime = baseTime + resDelay;
			
			// Ensure within bounds (min 1.0s, max 2.4s for other-healing)
			if (finalTime < BandageConstants.OTHER_HEALING_MIN_SECONDS)
				finalTime = BandageConstants.OTHER_HEALING_MIN_SECONDS;
			if (finalTime > BandageConstants.OTHER_HEALING_MAX_SECONDS)
				finalTime = BandageConstants.OTHER_HEALING_MAX_SECONDS;
			
			return finalTime;
		}

		/// <summary>
		/// Calculates classic (pre-AOS) healing delay.
		/// Enhanced with 30% stronger DEX bonus and bounded to min 1.0s, max 2.4s.
		/// </summary>
		private static double CalculateClassicDelay(int dex, double resDelay)
		{
			double baseTime;
			
			// Enhanced DEX calculation with 30% stronger bonus
			double dexRatio = (double)dex / BandageConstants.DEX_CAP_FOR_BANDAGE_SPEED;
			double enhancedDexFactor = dexRatio * BandageConstants.DEX_BONUS_MULTIPLIER;
			if (enhancedDexFactor > 1.0)
				enhancedDexFactor = 1.0;
			
			// Scale from max to min based on enhanced DEX
			double timeRange = BandageConstants.OTHER_HEALING_MAX_SECONDS - BandageConstants.OTHER_HEALING_MIN_SECONDS;
			baseTime = BandageConstants.OTHER_HEALING_MAX_SECONDS - (timeRange * enhancedDexFactor);
			
			// Add resurrection delay if applicable
			double finalTime = baseTime + resDelay;
			
			// Ensure within bounds (min 1.0s, max 2.4s for other-healing)
			if (finalTime < BandageConstants.OTHER_HEALING_MIN_SECONDS)
				finalTime = BandageConstants.OTHER_HEALING_MIN_SECONDS;
			if (finalTime > BandageConstants.OTHER_HEALING_MAX_SECONDS)
				finalTime = BandageConstants.OTHER_HEALING_MAX_SECONDS;
			
			return finalTime;
		}
	}
}

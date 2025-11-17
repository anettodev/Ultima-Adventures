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
		/// <returns>Time in milliseconds</returns>
		public static double CalculateBandageDelay(Mobile healer, Mobile patient, SkillName primarySkill)
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
				seconds = CalculateSelfHealDelay(bandageSpeedMin, dexseconds);
			}
			else
			{
				seconds = CalculateOtherHealDelay(dex, dexseconds, resDelay, primarySkill);
			}

			// Apply 30% speed penalty (slower healing)
			seconds *= BandageConstants.BANDAGE_SPEED_MULTIPLIER;

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
		/// </summary>
		private static double CalculateDexteritySeconds(Mobile healer, int dex)
		{
			double dexseconds;

			// Check for Midland special timing
			if (AdventuresFunctions.IsInMidland((object)healer) && healer is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)healer;
				dexseconds = ((BandageConstants.MIDLAND_DEX_BASE - (double)dex) / BandageConstants.MIDLAND_DEX_DIVISOR)
					* (1 - pm.Agility());
			}
			// Check for Soulbound special timing
			else if (healer is PlayerMobile && ((PlayerMobile)healer).SoulBound)
			{
				dexseconds = BandageConstants.BANDAGE_BASE_SECONDS * (1 - (dex / (double)BandageConstants.SOULBOUND_DEX_DIVISOR));
			}
			// Standard dexterity calculation
			else
			{
				dexseconds = BandageConstants.BANDAGE_BASE_SECONDS
					* (1 - ((double)dex / BandageConstants.DEX_CAP_FOR_BANDAGE_SPEED));
			}

			// Ensure dexseconds is never negative (possible with very high dex)
			return dexseconds < 0 ? 0 : dexseconds;
		}

		/// <summary>
		/// Calculates healing delay when healing yourself.
		/// </summary>
		private static double CalculateSelfHealDelay(double bandageSpeedMin, double dexseconds)
		{
			return bandageSpeedMin + dexseconds;
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
		/// </summary>
		private static double CalculateVeterinaryDelay(double dexseconds)
		{
			if (dexseconds >= BandageConstants.VET_DEXSECONDS_THRESHOLD)
			{
				return dexseconds + BandageConstants.VET_ADDITIONAL_SECONDS;
			}

			return BandageConstants.VET_DEFAULT_SECONDS;
		}

		/// <summary>
		/// Calculates AOS (Age of Shadows) healing delay.
		/// </summary>
		private static double CalculateAOSDelay(int dex, double resDelay)
		{
			if (dex < BandageConstants.AOS_DEX_THRESHOLD)
			{
				// Complex formula for lower dex: 3.2 - sin(dex/130) * 2.5
				double sinComponent = Math.Sin((double)dex / BandageConstants.AOS_SIN_DIVISOR)
					* BandageConstants.AOS_SIN_MULTIPLIER;
				return BandageConstants.AOS_BASE_SECONDS - sinComponent + resDelay;
			}

			// High dex gets minimum delay
			return BandageConstants.AOS_MIN_SECONDS + resDelay;
		}

		/// <summary>
		/// Calculates classic (pre-AOS) healing delay.
		/// </summary>
		private static double CalculateClassicDelay(int dex, double resDelay)
		{
			if (dex >= BandageConstants.CLASSIC_FAST_DEX)
			{
				return BandageConstants.CLASSIC_FAST_SECONDS + resDelay;
			}

			if (dex >= BandageConstants.CLASSIC_MEDIUM_DEX)
			{
				return BandageConstants.CLASSIC_MEDIUM_SECONDS + resDelay;
			}

			// Very low dex uses server-configured minimum speed
			return MyServerSettings.BandageSpeedMin() + resDelay;
		}
	}
}

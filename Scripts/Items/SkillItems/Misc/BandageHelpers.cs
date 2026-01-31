using System;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	/// <summary>
	/// Helper methods for bandage healing system.
	/// Extracted from Bandage.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class BandageHelpers
	{
		#region Validation Helpers

		/// <summary>
		/// Checks if the mobile is currently performing a bandage healing action.
		/// </summary>
		/// <param name="from">The mobile to check</param>
		/// <returns>True if currently bandaging, false otherwise</returns>
		public static bool IsCurrentlyBandaging(Mobile from)
		{
			if (from == null)
				return false;

			return !from.CanBeginAction(typeof(BandageContext));
		}

		/// <summary>
		/// Checks if the mobile is wearing the One Ring which prevents bandage usage.
		/// </summary>
		/// <param name="from">The mobile to check</param>
		/// <returns>True if wearing One Ring, false otherwise</returns>
		public static bool IsWearingOneRing(Mobile from)
		{
			if (from == null)
				return false;

			return from is PlayerMobile
				&& from.FindItemOnLayer(Layer.Ring) != null
				&& from.FindItemOnLayer(Layer.Ring) is OneRing;
		}

		/// <summary>
		/// Checks if the mobile is in a blessed state and cannot use items.
		/// </summary>
		/// <param name="from">The mobile to check</param>
		/// <returns>True if blessed and cannot use items, false otherwise</returns>
		public static bool IsBlessedAndCannotUseItems(Mobile from)
		{
			if (from == null)
				return false;

			return from.Blessed;
		}

		/// <summary>
		/// Checks if a mobile has the minimum resurrection skills required.
		/// </summary>
		/// <param name="from">The mobile to check</param>
		/// <returns>True if has 80+ Healing and 80+ Anatomy, false otherwise</returns>
		public static bool HasResurrectionSkills(Mobile from)
		{
			if (from == null)
				return false;

			return from.Skills[SkillName.Anatomy].Value >= BandageConstants.SKILL_HENCHMAN_RESURRECT_MIN
				&& from.Skills[SkillName.Healing].Value >= BandageConstants.SKILL_HENCHMAN_RESURRECT_MIN;
		}

		#endregion

		#region Messaging Helpers

		/// <summary>
		/// Sends a localized message to the mobile and displays it overhead.
		/// </summary>
		/// <param name="mobile">The mobile to send the message to</param>
		/// <param name="clilocId">The cliloc message ID</param>
		public static void SendMessageWithOverhead(Mobile mobile, int clilocId)
		{
			if (mobile == null)
				return;

			mobile.SendLocalizedMessage(clilocId);
			mobile.LocalOverheadMessage(MessageType.Regular, BandageConstants.MESSAGE_COLOR_OVERHEAD, clilocId);
		}

		#endregion

		#region Skill Check Helpers

		/// <summary>
		/// Performs skill checks for both healing-related skills.
		/// </summary>
		/// <param name="healer">The mobile performing the healing</param>
		/// <param name="primarySkill">Primary skill (Healing or Veterinary)</param>
		/// <param name="secondarySkill">Secondary skill (Anatomy or AnimalLore)</param>
		public static void CheckHealingSkills(Mobile healer, SkillName primarySkill, SkillName secondarySkill)
		{
			if (healer == null)
				return;

			healer.CheckSkill(secondarySkill, 0.0, 120.0);
			healer.CheckSkill(primarySkill, 0.0, 120.0);
		}

		/// <summary>
		/// Calculates the average of two skill values divided by a divisor.
		/// </summary>
		/// <param name="skill1">First skill value</param>
		/// <param name="skill2">Second skill value</param>
		/// <param name="divisor">Divisor to apply to each skill</param>
		/// <returns>The sum of divided skills</returns>
		public static double CalculateSkillAverage(double skill1, double skill2, int divisor)
		{
			return (skill1 / divisor) + (skill2 / divisor);
		}

		#endregion

		#region Skill Gain Helpers

		/// <summary>
		/// Determines if a healer should receive skill gains based on their skill level and action performed.
		/// 0.0-60.0: Gain on successful HP heals
		/// 60.1-90.0: Gain on successful poison cures (levels 0-3)
		/// 80.1-110.0: Gain on successful resurrections
		/// 110.1-120.0: Gain on successful Lethal poison cures (level 4)
		/// </summary>
		/// <param name="healerSkill">The healer's primary skill value (Healing or Veterinary)</param>
		/// <param name="actionType">Type of action: "heal", "cure", "resurrect", "cure_lethal"</param>
		/// <param name="wasSuccessful">Whether the action was successful</param>
		/// <param name="poisonLevel">Level of poison if curing (optional)</param>
		/// <returns>True if skill gains should be granted</returns>
		public static bool ShouldGrantSkillGains(double healerSkill, string actionType, bool wasSuccessful, int poisonLevel = 0)
		{
			// Only grant gains on successful actions
			if (!wasSuccessful)
				return false;

			// 0.0 - 60.0: Gain on successful HP heals only
			if (healerSkill <= 60.0)
			{
				return actionType == "heal";
			}

			// 60.1 - 90.0: Gain on successful poison cures (levels 0-3)
			if (healerSkill >= BandageConstants.SKILL_GAIN_POISON_CURE_MIN && healerSkill <= BandageConstants.SKILL_GAIN_POISON_CURE_MAX)
			{
				return actionType == "cure" && poisonLevel <= 3;
			}

			// 80.1 - 110.0: Gain on successful resurrections
			if (healerSkill >= BandageConstants.SKILL_GAIN_RESURRECTION_MIN && healerSkill <= BandageConstants.SKILL_GAIN_RESURRECTION_MAX)
			{
				return actionType == "resurrect";
			}

			// 110.1 - 120.0: Gain on successful Lethal poison cures (level 4)
			if (healerSkill >= BandageConstants.SKILL_GAIN_LETHAL_POISON_MIN && healerSkill <= BandageConstants.SKILL_GAIN_LETHAL_POISON_MAX)
			{
				return actionType == "cure_lethal" || (actionType == "cure" && poisonLevel == 4);
			}

			// No gains outside defined ranges
			return false;
		}

		#endregion

		#region Poison Cure Helpers

		/// <summary>
		/// Gets the base cure chance for a given poison level.
		/// </summary>
		/// <param name="poisonLevel">The level of the poison (0-4)</param>
		/// <returns>Base cure chance as a decimal (0.0 to 1.0)</returns>
		public static double GetPoisonCureBaseChance(int poisonLevel)
		{
			switch (poisonLevel)
			{
				case 0: return BandageConstants.POISON_CURE_BASE_LESSER;   // 50%
				case 1: return BandageConstants.POISON_CURE_BASE_REGULAR;  // 40%
				case 2: return BandageConstants.POISON_CURE_BASE_GREATER;  // 30%
				case 3: return BandageConstants.POISON_CURE_BASE_DEADLY;   // 12%
				case 4: return BandageConstants.POISON_CURE_BASE_LETHAL;   // 3%
				default: return BandageConstants.POISON_CURE_BASE_LESSER;  // Fallback to 50%
			}
		}

		/// <summary>
		/// Calculates the total poison cure chance based on skills, poison level, slips, and enhanced bandage.
		/// Formula: BaseChance + (Healing/10 * 1%) + (Anatomy/10 * 1%) - (Slips * 2%) + (EnhancedBandage ? 5% : 0%)
		/// </summary>
		/// <param name="healing">Healing or Veterinary skill value</param>
		/// <param name="anatomy">Anatomy or AnimalLore skill value</param>
		/// <param name="poisonLevel">Level of the poison (0-4)</param>
		/// <param name="slips">Number of finger slips</param>
		/// <param name="isEnhancedBandage">Whether enhanced bandage is being used</param>
		/// <returns>Total cure chance as a decimal (0.0 to 1.0+)</returns>
		public static double CalculatePoisonCureChance(double healing, double anatomy, int poisonLevel, int slips, bool isEnhancedBandage = false)
		{
			double baseChance = GetPoisonCureBaseChance(poisonLevel);
			double healingBonus = (healing / 10.0) * BandageConstants.POISON_CURE_SKILL_BONUS_PER_10;
			double anatomyBonus = (anatomy / 10.0) * BandageConstants.POISON_CURE_SKILL_BONUS_PER_10;
			double slipPenalty = slips * BandageConstants.SLIP_PENALTY_PER_SLIP;
			double enhancedBonus = isEnhancedBandage ? 0.05 : 0.0; // 5% bonus for enhanced bandage (all poison levels)

			return baseChance + healingBonus + anatomyBonus - slipPenalty + enhancedBonus;
		}

		#endregion

		#region Henchman Resurrection Helpers

		/// <summary>
		/// Attempts to resurrect a henchman item if it's dead and the healer has sufficient skills.
		/// </summary>
		/// <typeparam name="T">The type of henchman item</typeparam>
		/// <param name="henchman">The henchman item to resurrect</param>
		/// <param name="from">The mobile attempting the resurrection</param>
		/// <param name="bandage">The bandage being consumed</param>
		/// <param name="henchmanName">The name to set for the henchman</param>
		/// <returns>True if resurrection was successful or attempted, false if prerequisites not met</returns>
		public static bool TryResurrectHenchman<T>(T henchman, Mobile from, Bandage bandage, string henchmanName) where T : HenchmanItem
		{
			if (henchman == null || from == null || bandage == null)
				return false;

			if (!HasResurrectionSkills(from))
				return false;

			if (henchman.HenchDead <= 0)
			{
				from.SendMessage(BandageStringConstants.MSG_HENCHMAN_NOT_DEAD);
				return true; // Return true because we handled the message
			}

			henchman.Name = henchmanName;
			henchman.HenchDead = 0;
			henchman.InvalidateProperties();
			bandage.Consume();

			return true;
		}

		#endregion
	}
}

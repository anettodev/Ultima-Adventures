using System;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	/// <summary>
	/// Helper class for validating pets in contract systems.
	/// Extracted from TamingBODGump.cs and TamingBODBookGump.cs to eliminate code duplication.
	/// </summary>
	public static class PetValidationHelper
	{
		#region Validation Methods

		/// <summary>
		/// Validates if a pet can be added to a contract.
		/// </summary>
		/// <param name="pet">The pet to validate</param>
		/// <param name="from">The player attempting to add the pet</param>
		/// <param name="combatRange">The combat range check distance</param>
		/// <returns>Error message if validation fails, null if valid</returns>
		public static string ValidatePetForContract(BaseCreature pet, Mobile from, int combatRange)
		{
			return ValidatePetForContract(pet, from, combatRange, null, null);
		}

		/// <summary>
		/// Validates if a pet can be added to a contract with creature type checking.
		/// </summary>
		/// <param name="pet">The pet to validate</param>
		/// <param name="from">The player attempting to add the pet</param>
		/// <param name="combatRange">The combat range check distance</param>
		/// <param name="contractTier">The contract tier (1-5), or null to skip type checking</param>
		/// <param name="contractCreatureType">The required creature type, or null for generic contracts</param>
		/// <returns>Error message if validation fails, null if valid</returns>
		public static string ValidatePetForContract(BaseCreature pet, Mobile from, int combatRange, int? contractTier, Type contractCreatureType)
		{
			if (!pet.Controlled || pet.ControlMaster != from)
			{
				from.SendLocalizedMessage(1042562); // You do not own that pet!
				return ""; // Return empty string to indicate error was sent via localized message
			}

			if (pet.IsDeadPet)
			{
				from.SendLocalizedMessage(1049668); // Living pets only, please.
				return "";
			}

			if (pet.Summoned)
			{
				return TamingBODGumpStringConstants.MSG_CREATURE_SUMMONED;
			}

			if (pet is Squire)
			{
				return TamingBODGumpStringConstants.MSG_CREATURE_TOO_BIG;
			}

			if (pet.Body.IsHuman)
			{
				return TamingBODGumpStringConstants.MSG_WONT_WORK_ON_HUMANS;
			}

			if ((pet is PackLlama || pet is PackHorse || pet is Beetle) && (pet.Backpack != null && pet.Backpack.Items.Count > 0))
			{
				from.SendLocalizedMessage(1042563); // You need to unload your pet.
				return "";
			}

			if (pet.Combatant != null && pet.InRange(pet.Combatant, combatRange) && pet.Map == pet.Combatant.Map)
			{
				from.SendLocalizedMessage(1042564); // I'm sorry. Your pet seems to be busy.
				return "";
			}

			// Check creature type if contract has specific requirements
			if (contractTier.HasValue && contractTier.Value > 1 && contractCreatureType != null)
			{
				Type petType = pet.GetType();
				if (!TamingBODCreatureTypes.IsValidCreatureType(petType, contractTier.Value) || petType != contractCreatureType)
				{
					string creatureTypeName = TamingBODCreatureTypes.GetCreatureTypeName(contractCreatureType, contractTier.Value);
					if (creatureTypeName != null)
					{
						return string.Format(TamingBODGumpStringConstants.MSG_WRONG_CREATURE_TYPE, creatureTypeName);
					}
					else
					{
						return TamingBODGumpStringConstants.MSG_WRONG_CREATURE_TYPE_GENERIC;
					}
				}
			}

			return null; // Valid
		}

		/// <summary>
		/// Processes a pet for a contract (removes control, deletes pet).
		/// </summary>
		/// <param name="pet">The pet to process</param>
		public static void ProcessPetForContract(BaseCreature pet)
		{
			pet.ControlTarget = null;
			pet.ControlOrder = OrderType.None;
			pet.Internalize();
			pet.SetControlMaster(null);
			pet.SummonMaster = null;
			pet.Delete();
		}

		/// <summary>
		/// Calculates the contract reward based on pet value.
		/// Uses legacy multiplier system.
		/// </summary>
		/// <param name="pet">The pet being added</param>
		/// <param name="from">The player who owns the pet</param>
		/// <returns>The calculated reward amount</returns>
		public static int CalculateContractReward(BaseCreature pet, Mobile from)
		{
			int petValue = Server.Mobiles.AnimalTrainerLord.ValuatePet(pet, from);
			return (int)((double)petValue * TamingBODGumpConstants.REWARD_MULTIPLIER);
		}

		/// <summary>
		/// Calculates the contract reward based on dynamic pet valuation with bonus.
		/// Uses AnimalTrainerLord.ValuatePet() with 15-35% premium bonus for contracts.
		/// This provides better rewards for higher-value pets and incentivizes contracts over direct sales.
		/// </summary>
		/// <param name="pet">The pet being added to the contract</param>
		/// <param name="from">The player who owns the pet</param>
		/// <returns>The calculated reward amount with bonus</returns>
		public static int CalculateDynamicContractReward(BaseCreature pet, Mobile from)
		{
			if (pet == null || from == null)
				return 0;

			// Calculate base pet value using the same system as Animal Broker
			int baseValue = Server.Mobiles.AnimalTrainerLord.ValuatePet(pet, from);
			
			// Apply 15-35% premium bonus for contracts (incentivizes contracts over direct sales)
			int bonusPercent = Utility.RandomMinMax(15, 35);
			int reward = (int)((double)baseValue * (1.0 + ((double)bonusPercent / 100.0)));
			
			return reward;
		}

		/// <summary>
		/// Calculates the contract reward based on tier system.
		/// DEPRECATED: Use CalculateDynamicContractReward() instead for better rewards.
		/// Kept for backward compatibility if needed.
		/// </summary>
		/// <param name="tier">The contract tier (1-5)</param>
		/// <returns>The calculated reward amount per creature</returns>
		public static int CalculateTierBasedReward(int tier)
		{
			switch (tier)
			{
				case 1:
					return TamingBODConstants.TIER_1_COINS_PER_CREATURE;
				case 2:
					return Utility.RandomMinMax(TamingBODConstants.TIER_2_COINS_MIN, TamingBODConstants.TIER_2_COINS_MAX);
				case 3:
					return Utility.RandomMinMax(TamingBODConstants.TIER_3_COINS_MIN, TamingBODConstants.TIER_3_COINS_MAX);
				case 4:
					return Utility.RandomMinMax(TamingBODConstants.TIER_4_COINS_MIN, TamingBODConstants.TIER_4_COINS_MAX);
				case 5:
					return Utility.RandomMinMax(TamingBODConstants.TIER_5_COINS_MIN, TamingBODConstants.TIER_5_COINS_MAX);
				default:
					return TamingBODConstants.TIER_1_COINS_PER_CREATURE; // Default to tier 1
			}
		}

		#endregion
	}
}


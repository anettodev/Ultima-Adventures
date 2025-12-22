using System;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	/// <summary>
	/// Pet Trainer - Item that allows training of bonded pets.
	/// Randomly modifies pet skills with chance of success or failure.
	/// Requires Animal Taming >= 100 for better results (higher skill gains, lower losses).
	/// Has a 10% chance to break on use.
	/// </summary>
	public class PetTrainer : Item
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PetTrainer class.
		/// </summary>
		[Constructable]
		public PetTrainer() : base(PetTrainerConstants.ITEM_ID)
		{
			Name = PetTrainerStringConstants.ITEM_NAME;
			//LootType = LootType.Blessed;
			Hue = PetTrainerConstants.HUE_VALUES[Utility.Random(PetTrainerConstants.HUE_VALUES.Length)];
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		public PetTrainer(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the PetTrainer.
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the PetTrainer.
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			//LootType = LootType.Blessed;

			int version = reader.ReadInt();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets whether to display loot type.
		/// </summary>
		public override bool DisplayLootType { get { return false; } }

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to initiate pet training targeting.
		/// Has a 10% chance to break the tool.
		/// </summary>
		/// <param name="from">The player using the trainer</param>
		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
				return;
			}

			if (Utility.RandomDouble() <= PetTrainerConstants.BREAK_CHANCE)
			{
				this.Delete();
				from.SendMessage(PetTrainerStringConstants.MSG_TOOL_BROKE);
			}
			else
			{
				from.SendMessage(PetTrainerStringConstants.MSG_CHOOSE_PET);
				from.Target = new TrainTarget(this);
			}
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Target class for selecting a pet to train.
		/// </summary>
		private class TrainTarget : Target
		{
			private PetTrainer m_Trainer;

			/// <summary>
			/// Initializes a new instance of the TrainTarget class.
			/// </summary>
			public TrainTarget(PetTrainer trainer) : base(PetTrainerConstants.TARGET_RANGE, false, TargetFlags.None)
			{
				m_Trainer = trainer;
			}

			/// <summary>
			/// Handles the target selection and training process.
			/// </summary>
			/// <param name="from">The player who selected the target</param>
			/// <param name="targeted">The object that was targeted</param>
			protected override void OnTarget(Mobile from, object targeted)
			{
				BaseCreature pet = targeted as BaseCreature;
				if (pet == null)
				{
					from.SendMessage(PetTrainerStringConstants.MSG_INVALID_TARGET);
					return;
				}

				string errorMessage = ValidatePetForTraining(from, pet);
				if (errorMessage != null)
				{
					from.SendMessage(errorMessage);
					return;
				}

				bool isHighSkill = from.Skills[SkillName.AnimalTaming].Base >= PetTrainerConstants.ANIMAL_TAMING_THRESHOLD;
				double skillGain = isHighSkill ? PetTrainerConstants.SKILL_GAIN_HIGH : PetTrainerConstants.SKILL_GAIN_LOW;
				double skillLoss = isHighSkill ? PetTrainerConstants.SKILL_LOSS_HIGH : PetTrainerConstants.SKILL_LOSS_LOW;
				int combatChance = isHighSkill ? PetTrainerConstants.COMBAT_CHANCE_DENOMINATOR_HIGH : PetTrainerConstants.COMBAT_CHANCE_DENOMINATOR_LOW;

				PerformTraining(from, pet, skillGain, skillLoss, combatChance);
			}

			/// <summary>
			/// Validates if a pet can be trained.
			/// </summary>
			/// <param name="from">The player attempting to train</param>
			/// <param name="pet">The pet to validate</param>
			/// <returns>Error message if validation fails, null if valid</returns>
			private string ValidatePetForTraining(Mobile from, BaseCreature pet)
			{
				if (pet.IsDeadPet)
					return PetTrainerStringConstants.MSG_PET_MUST_BE_ALIVE;

				if (pet.ControlMaster != from)
					return PetTrainerStringConstants.MSG_NOT_YOUR_PET;

				if (!pet.IsBonded)
					return PetTrainerStringConstants.MSG_MUST_BE_BONDED;

				if (pet.SkillsTotal >= pet.SkillsCap)
					return PetTrainerStringConstants.MSG_MAX_SKILL_LEVEL;

				return null; // Valid
			}

			/// <summary>
			/// Performs the training on the pet.
			/// </summary>
			/// <param name="from">The player training the pet</param>
			/// <param name="pet">The pet being trained</param>
			/// <param name="skillGain">Amount of skill to gain on success</param>
			/// <param name="skillLoss">Amount of skill to lose on failure</param>
			/// <param name="combatChance">Denominator for combat chance (1 in X)</param>
			private void PerformTraining(Mobile from, BaseCreature pet, double skillGain, double skillLoss, int combatChance)
			{
				int random = Utility.Random(PetTrainerConstants.SWITCH_CASE_COUNT);

				// Cases 0-7: Skill gain (8 skills)
				if (random < PetTrainerConstants.SKILL_GAIN_COUNT)
				{
					SkillName skill = PetTrainerConstants.TRAINABLE_SKILLS[random];
					pet.Skills[skill].Base += skillGain;
					from.SendMessage(PetTrainerStringConstants.MSG_PET_BECOMES_STRONGER);
				}
				// Cases 8-15: Skill loss - anger beast (8 skills)
				else if (random < PetTrainerConstants.SKILL_GAIN_COUNT + PetTrainerConstants.SKILL_LOSS_COUNT)
				{
					int skillIndex = random - PetTrainerConstants.SKILL_GAIN_COUNT;
					SkillName skill = PetTrainerConstants.TRAINABLE_SKILLS[skillIndex];
					pet.Skills[skill].Base -= skillLoss;
					HandleAngeredBeast(from, pet);
				}
				// Cases 16-18: No effect (3 cases)
				else
				{
					from.SendMessage(PetTrainerStringConstants.MSG_PET_LOOKS_SHEEPISHLY);
				}

				// Chance to really anger the beast (combat)
				if (Utility.Random(combatChance) == 0)
				{
					pet.Combatant = from;
					from.SendMessage(PetTrainerStringConstants.MSG_REALLY_ANGER_BEAST);
				}
			}

			/// <summary>
			/// Handles the "anger beast" effect when training fails.
			/// </summary>
			/// <param name="from">The player who angered the beast</param>
			/// <param name="pet">The pet that got angry</param>
			private void HandleAngeredBeast(Mobile from, BaseCreature pet)
			{
				from.SendMessage(PetTrainerStringConstants.MSG_ANGER_THE_BEAST);
				pet.PlaySound(pet.GetAngerSound());
				pet.Direction = pet.GetDirectionTo(from);
			}
		}

		#endregion
	}
}

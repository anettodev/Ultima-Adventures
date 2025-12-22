using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using System.Collections;
using Server.Gumps;
using Server.Targeting;
using Server.Misc;
using Server.Accounting;
using System.Xml;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Pet Bond Deed - Item that allows instant bonding of tamed pets.
	/// Players can use this deed to immediately bond with their pets instead of waiting for the natural 7-day bonding process.
	/// Requires Animal Taming and Animal Lore skills >= pet's MinTameSkill.
	/// </summary>
	public class PetBondDeed : Item
	{
		#region Constants

		/// <summary>Item ID for Pet Bond Deed</summary>
		private const int ITEM_ID = 0x14F0;

		/// <summary>Hue color for Pet Bond Deed</summary>
		private const int HUE_BOND_DEED = 1759;

		/// <summary>Target range for selecting pets</summary>
		private const int TARGET_RANGE = 3;

		/// <summary>Vendor price when sold by AnimalTrainer</summary>
		public const int VENDOR_PRICE = 2000;

		/// <summary>Maximum quantity available from vendor</summary>
		public const int VENDOR_MAX_QUANTITY = 8;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PetBondDeed class.
		/// </summary>
		[Constructable]
		public PetBondDeed() : base(ITEM_ID)
		{
			Weight = 0;
			//LootType = LootType.Blessed;
			Name = PetBondDeedStringConstants.ITEM_NAME;
			Hue = HUE_BOND_DEED;
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		public PetBondDeed(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the PetBondDeed.
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the PetBondDeed.
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to initiate pet bonding targeting.
		/// </summary>
		/// <param name="from">The player using the deed</param>
		public override void OnDoubleClick(Mobile from)
		{
			if (IsChildOf(from.Backpack))
			{
				from.Target = new InternalTarget(from, this);
			}
			else
			{
				from.SendMessage(PetBondDeedStringConstants.MSG_NOT_IN_BACKPACK);
			}
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Target class for selecting a pet to bond with.
		/// </summary>
		private class InternalTarget : Target
		{
			private Mobile m_From;
			private PetBondDeed m_Deed;

			/// <summary>
			/// Initializes a new instance of the InternalTarget class.
			/// </summary>
			public InternalTarget(Mobile from, PetBondDeed deed) : base(TARGET_RANGE, false, TargetFlags.None)
			{
				m_Deed = deed;
				m_From = from;
				from.SendMessage(PetBondDeedStringConstants.MSG_TARGET_PROMPT);
			}

			/// <summary>
			/// Handles the target selection and bonding process.
			/// </summary>
			/// <param name="from">The player who selected the target</param>
			/// <param name="targeted">The object that was targeted</param>
			protected override void OnTarget(Mobile from, object targeted)
			{
				if (!m_Deed.IsChildOf(m_From.Backpack))
				{
					from.SendMessage(PetBondDeedStringConstants.MSG_NOT_IN_BACKPACK);
					return;
				}

				BaseCreature creature = targeted as BaseCreature;
				if (creature == null)
				{
					from.SendMessage(PetBondDeedStringConstants.MSG_TARGET_NOT_ANIMAL);
					return;
				}

				string errorMessage = ValidatePetForBonding(from, creature);
				if (errorMessage != null)
				{
					from.SendMessage(errorMessage);
					return;
				}

				if (creature.IsBonded)
				{
					string ludicMessage = PetBondDeedStringConstants.MSG_ALREADY_BONDED_LUDIC[Utility.Random(PetBondDeedStringConstants.MSG_ALREADY_BONDED_LUDIC.Length)];
					from.SendMessage(0x3F, ludicMessage);
					return;
				}

				PerformBonding(from, creature);
			}

			/// <summary>
			/// Validates if a pet can be bonded using the deed.
			/// </summary>
			/// <param name="from">The player attempting to bond</param>
			/// <param name="creature">The pet to validate</param>
			/// <returns>Error message if validation fails, null if valid</returns>
			private string ValidatePetForBonding(Mobile from, BaseCreature creature)
			{
				if (!creature.Tamable)
					return PetBondDeedStringConstants.MSG_NOT_TAMABLE;

				if (!creature.Controlled || creature.ControlMaster != from)
					return PetBondDeedStringConstants.MSG_NOT_YOUR_PET;

				if (creature.IsDeadPet)
					return PetBondDeedStringConstants.MSG_PET_DEAD;

				if (creature.Summoned)
					return PetBondDeedStringConstants.MSG_PET_SUMMONED;

				if (creature.Body.IsHuman)
					return PetBondDeedStringConstants.MSG_HUMANOID_ERROR;

				if (!ValidateSkills(from, creature))
					return PetBondDeedStringConstants.MSG_SKILL_TOO_LOW;

				return null; // Valid
			}

			/// <summary>
			/// Validates if the player has sufficient skills to bond with the pet.
			/// </summary>
			/// <param name="from">The player to check</param>
			/// <param name="creature">The pet to check against</param>
			/// <returns>True if skills are sufficient, false otherwise</returns>
			private bool ValidateSkills(Mobile from, BaseCreature creature)
			{
				double minTameSkill = creature.MinTameSkill;
				return from.Skills[SkillName.AnimalTaming].Base >= minTameSkill &&
					   from.Skills[SkillName.AnimalLore].Base >= minTameSkill;
			}

			/// <summary>
			/// Performs the actual bonding process.
			/// </summary>
			/// <param name="from">The player bonding the pet</param>
			/// <param name="creature">The pet being bonded</param>
			private void PerformBonding(Mobile from, BaseCreature creature)
			{
				try
				{
					creature.IsBonded = true;
					from.SendMessage(string.Format(PetBondDeedStringConstants.MSG_BOND_SUCCESS_FORMAT, creature.Name));
					m_Deed.Delete();
				}
				catch
				{
					from.SendMessage(PetBondDeedStringConstants.MSG_BOND_ERROR);
				}
			}
		}

		#endregion
	}
}

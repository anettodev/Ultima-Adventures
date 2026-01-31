using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Engines.Craft;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Rusty junk item that can be smelted into iron ingots at a forge.
	/// Can randomly spawn as various types of rusty armor, shields, or weapons.
	/// </summary>
	public class RustyJunk : Item
	{
		#region Constants

		private const int RANDOM_ARMOR_WEAPON_MIN = RustyJunkConstants.RANDOM_ARMOR_WEAPON_MIN;
		private const int RANDOM_ARMOR_WEAPON_MAX = RustyJunkConstants.RANDOM_ARMOR_WEAPON_MAX;
		private const int RANDOM_ARMOR_MIN = RustyJunkConstants.RANDOM_ARMOR_MIN;
		private const int RANDOM_ARMOR_MAX = RustyJunkConstants.RANDOM_ARMOR_MAX;
		private const int RANDOM_WEAPON_MIN = RustyJunkConstants.RANDOM_WEAPON_MIN;
		private const int RANDOM_WEAPON_MAX = RustyJunkConstants.RANDOM_WEAPON_MAX;

		#endregion

		#region Fields

		/// <summary>
		/// Structure for rusty item variant data
		/// </summary>
		private struct RustyItemVariant
		{
			public string Name;
			public int[] ItemIDs;
			public double Weight;
			public int WeightMin;
			public int WeightMax;
			public bool UseRandomWeight;

			public RustyItemVariant(string name, int[] itemIDs, double weight)
			{
				this.Name = name;
				this.ItemIDs = itemIDs;
				this.Weight = weight;
				this.WeightMin = 0;
				this.WeightMax = 0;
				this.UseRandomWeight = false;
			}

			public RustyItemVariant(string name, int[] itemIDs, int weightMin, int weightMax)
			{
				this.Name = name;
				this.ItemIDs = itemIDs;
				this.Weight = -1.0;
				this.WeightMin = weightMin;
				this.WeightMax = weightMax;
				this.UseRandomWeight = true;
			}
		}

		/// <summary>
		/// Array of armor and shield variants
		/// </summary>
		private static readonly RustyItemVariant[] ArmorShieldVariants = new RustyItemVariant[]
		{
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_SHIELD, new[] { RustyJunkConstants.ITEM_ID_SHIELD_1 }, RustyJunkConstants.WEIGHT_MEDIUM_5),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_SHIELD, new[] { RustyJunkConstants.ITEM_ID_SHIELD_2 }, RustyJunkConstants.WEIGHT_MEDIUM_4),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_SHIELD, new[] { RustyJunkConstants.ITEM_ID_SHIELD_3A, RustyJunkConstants.ITEM_ID_SHIELD_3B }, RustyJunkConstants.WEIGHT_MEDIUM_5),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_SHIELD, new[] { RustyJunkConstants.ITEM_ID_SHIELD_4A, RustyJunkConstants.ITEM_ID_SHIELD_4B }, RustyJunkConstants.WEIGHT_HEAVY_8),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_SHIELD, new[] { RustyJunkConstants.ITEM_ID_SHIELD_5 }, RustyJunkConstants.WEIGHT_MEDIUM_5),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_SHIELD, new[] { RustyJunkConstants.ITEM_ID_SHIELD_6 }, RustyJunkConstants.WEIGHT_MEDIUM_5),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_SHIELD, new[] { RustyJunkConstants.ITEM_ID_SHIELD_7A, RustyJunkConstants.ITEM_ID_SHIELD_7B }, RustyJunkConstants.WEIGHT_MEDIUM_7),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_SHIELD, new[] { RustyJunkConstants.ITEM_ID_SHIELD_8 }, RustyJunkConstants.WEIGHT_MEDIUM_5),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_ARMS, new[] { RustyJunkConstants.ITEM_ID_ARMS_1, RustyJunkConstants.ITEM_ID_ARMS_2 }, RustyJunkConstants.WEIGHT_MEDIUM_5),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_LEGGINGS, new[] { RustyJunkConstants.ITEM_ID_LEGGINGS_1, RustyJunkConstants.ITEM_ID_LEGGINGS_2 }, RustyJunkConstants.WEIGHT_MEDIUM_7),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_HELM, new[] { RustyJunkConstants.ITEM_ID_HELM }, RustyJunkConstants.WEIGHT_MEDIUM_5),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_GORGET, new[] { RustyJunkConstants.ITEM_ID_GORGET }, RustyJunkConstants.WEIGHT_LIGHT_2),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_GLOVES, new[] { RustyJunkConstants.ITEM_ID_GLOVES_1, RustyJunkConstants.ITEM_ID_GLOVES_2 }, RustyJunkConstants.WEIGHT_LIGHT_2),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_ARMOR, new[] { RustyJunkConstants.ITEM_ID_ARMOR_1, RustyJunkConstants.ITEM_ID_ARMOR_2 }, RustyJunkConstants.WEIGHT_HEAVY_10),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_COIF, new[] { RustyJunkConstants.ITEM_ID_COIF_1, RustyJunkConstants.ITEM_ID_COIF_2 }, RustyJunkConstants.WEIGHT_LIGHT_1),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_LEGGINGS, new[] { RustyJunkConstants.ITEM_ID_LEGGINGS_2A, RustyJunkConstants.ITEM_ID_LEGGINGS_2B }, RustyJunkConstants.WEIGHT_MEDIUM_7),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_TUNIC, new[] { RustyJunkConstants.ITEM_ID_TUNIC_1, RustyJunkConstants.ITEM_ID_TUNIC_2 }, RustyJunkConstants.WEIGHT_MEDIUM_7),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_GLOVES, new[] { RustyJunkConstants.ITEM_ID_GLOVES_2A, RustyJunkConstants.ITEM_ID_GLOVES_2B }, RustyJunkConstants.WEIGHT_LIGHT_2),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_LEGGINGS, new[] { RustyJunkConstants.ITEM_ID_LEGGINGS_3A, RustyJunkConstants.ITEM_ID_LEGGINGS_3B }, RustyJunkConstants.WEIGHT_VERY_HEAVY),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_TUNIC, new[] { RustyJunkConstants.ITEM_ID_TUNIC_2A, RustyJunkConstants.ITEM_ID_TUNIC_2B }, RustyJunkConstants.WEIGHT_VERY_HEAVY),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_SLEEVES, new[] { RustyJunkConstants.ITEM_ID_SLEEVES_1, RustyJunkConstants.ITEM_ID_SLEEVES_2 }, RustyJunkConstants.WEIGHT_VERY_HEAVY)
		};

		/// <summary>
		/// Array of weapon variants
		/// </summary>
		private static readonly RustyItemVariant[] WeaponVariants = new RustyItemVariant[]
		{
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_HATCHET, new[] { RustyJunkConstants.ITEM_ID_HATCHET_1, RustyJunkConstants.ITEM_ID_HATCHET_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_AXE, new[] { RustyJunkConstants.ITEM_ID_AXE_1, RustyJunkConstants.ITEM_ID_AXE_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_BATTLE_AXE, new[] { RustyJunkConstants.ITEM_ID_BATTLE_AXE_1, RustyJunkConstants.ITEM_ID_BATTLE_AXE_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_AXE, new[] { RustyJunkConstants.ITEM_ID_AXE_2A, RustyJunkConstants.ITEM_ID_AXE_2B }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_DOUBLE_AXE, new[] { RustyJunkConstants.ITEM_ID_DOUBLE_AXE_1, RustyJunkConstants.ITEM_ID_DOUBLE_AXE_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_BARDICHE, new[] { RustyJunkConstants.ITEM_ID_BARDICHE_1, RustyJunkConstants.ITEM_ID_BARDICHE_2 }, RustyJunkConstants.WEIGHT_RANGE_HEAVY_MIN, RustyJunkConstants.WEIGHT_RANGE_HEAVY_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_DAGGER, new[] { RustyJunkConstants.ITEM_ID_DAGGER_1, RustyJunkConstants.ITEM_ID_DAGGER_2 }, RustyJunkConstants.WEIGHT_RANGE_DAGGER_MIN, RustyJunkConstants.WEIGHT_RANGE_DAGGER_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_MACE, new[] { RustyJunkConstants.ITEM_ID_MACE_1, RustyJunkConstants.ITEM_ID_MACE_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_BROADSWORD, new[] { RustyJunkConstants.ITEM_ID_BROADSWORD_1, RustyJunkConstants.ITEM_ID_BROADSWORD_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_LONGSWORD, new[] { RustyJunkConstants.ITEM_ID_LONGSWORD_1, RustyJunkConstants.ITEM_ID_LONGSWORD_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_SPEAR, new[] { RustyJunkConstants.ITEM_ID_SPEAR_1, RustyJunkConstants.ITEM_ID_SPEAR_2 }, RustyJunkConstants.WEIGHT_RANGE_HEAVY_MIN, RustyJunkConstants.WEIGHT_RANGE_HEAVY_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_WAR_HAMMER, new[] { RustyJunkConstants.ITEM_ID_WAR_HAMMER_1, RustyJunkConstants.ITEM_ID_WAR_HAMMER_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_MAUL, new[] { RustyJunkConstants.ITEM_ID_MAUL_1, RustyJunkConstants.ITEM_ID_MAUL_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_HAMMER_PICK, new[] { RustyJunkConstants.ITEM_ID_HAMMER_PICK_1, RustyJunkConstants.ITEM_ID_HAMMER_PICK_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_HALBERD, new[] { RustyJunkConstants.ITEM_ID_HALBERD_1, RustyJunkConstants.ITEM_ID_HALBERD_2 }, RustyJunkConstants.WEIGHT_RANGE_HEAVY_MIN, RustyJunkConstants.WEIGHT_RANGE_HEAVY_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_CUTLASS, new[] { RustyJunkConstants.ITEM_ID_CUTLASS_1, RustyJunkConstants.ITEM_ID_CUTLASS_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_GREAT_AXE, new[] { RustyJunkConstants.ITEM_ID_GREAT_AXE_1, RustyJunkConstants.ITEM_ID_GREAT_AXE_2 }, RustyJunkConstants.WEIGHT_RANGE_HEAVY_MIN, RustyJunkConstants.WEIGHT_RANGE_HEAVY_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_WAR_AXE, new[] { RustyJunkConstants.ITEM_ID_WAR_AXE_1, RustyJunkConstants.ITEM_ID_WAR_AXE_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_SCIMITAR, new[] { RustyJunkConstants.ITEM_ID_SCIMITAR_1, RustyJunkConstants.ITEM_ID_SCIMITAR_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_LONG_SWORD, new[] { RustyJunkConstants.ITEM_ID_LONG_SWORD_1, RustyJunkConstants.ITEM_ID_LONG_SWORD_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_BARBARIAN_SWORD, new[] { RustyJunkConstants.ITEM_ID_BARBARIAN_SWORD_1, RustyJunkConstants.ITEM_ID_BARBARIAN_SWORD_2 }, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MIN, RustyJunkConstants.WEIGHT_RANGE_LIGHT_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_SCYTHE, new[] { RustyJunkConstants.ITEM_ID_SCYTHE_1, RustyJunkConstants.ITEM_ID_SCYTHE_2 }, RustyJunkConstants.WEIGHT_RANGE_HEAVY_MIN, RustyJunkConstants.WEIGHT_RANGE_HEAVY_MAX),
			new RustyItemVariant(RustyJunkStringConstants.NAME_RUSTY_PIKE, new[] { RustyJunkConstants.ITEM_ID_PIKE_1, RustyJunkConstants.ITEM_ID_PIKE_2 }, RustyJunkConstants.WEIGHT_RANGE_HEAVY_MIN, RustyJunkConstants.WEIGHT_RANGE_HEAVY_MAX)
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new rusty junk item with random variant (armor/shield or weapon)
		/// </summary>
		[Constructable]
		public RustyJunk() : base(RustyJunkConstants.ITEM_ID_BASE_SHIELD)
		{
			Name = RustyJunkStringConstants.NAME_RUSTY_SHIELD;
			ItemID = RustyJunkConstants.ITEM_ID_BASE_SHIELD;
			Weight = RustyJunkConstants.WEIGHT_DEFAULT;

			if (Utility.RandomMinMax(RANDOM_ARMOR_WEAPON_MIN, RANDOM_ARMOR_WEAPON_MAX) == 1)
			{
				InitializeArmorShield();
			}
			else
			{
				InitializeWeapon();
			}

			Hue = GetRandomHue();
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public RustyJunk(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to initiate smelting at a forge
		/// </summary>
		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_MUST_BE_IN_PACK_USE);
				return;
			}

			from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.MSG_SELECT_FORGE);
			from.Target = new InternalTarget(this);
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Initializes the item as a random armor or shield variant
		/// </summary>
		private void InitializeArmorShield()
		{
			int variant = Utility.RandomMinMax(RANDOM_ARMOR_MIN, RANDOM_ARMOR_MAX);
			RustyItemVariant item = ArmorShieldVariants[variant];

			Name = item.Name;
			ItemID = GetRandomItemID(item.ItemIDs);
			Weight = GetItemWeight(item);
		}

		/// <summary>
		/// Initializes the item as a random weapon variant
		/// </summary>
		private void InitializeWeapon()
		{
			int variant = Utility.RandomMinMax(RANDOM_WEAPON_MIN, RANDOM_WEAPON_MAX);
			RustyItemVariant item = WeaponVariants[variant];

			Name = item.Name;
			ItemID = GetRandomItemID(item.ItemIDs);
			Weight = GetItemWeight(item);
		}

		/// <summary>
		/// Gets a random item ID from the array (or single ID if array has one element)
		/// </summary>
		/// <param name="itemIDs">Array of item IDs</param>
		/// <returns>Selected item ID</returns>
		private int GetRandomItemID(int[] itemIDs)
		{
			if (itemIDs.Length == 1)
			{
				return itemIDs[0];
			}
			return Utility.RandomList(itemIDs);
		}

		/// <summary>
		/// Gets the weight for an item variant (fixed or random range)
		/// </summary>
		/// <param name="item">The item variant</param>
		/// <returns>Weight value</returns>
		private double GetItemWeight(RustyItemVariant item)
		{
			if (item.UseRandomWeight)
			{
				return Utility.RandomMinMax(item.WeightMin, item.WeightMax);
			}
			return item.Weight;
		}

		/// <summary>
		/// Gets a random rusty hue
		/// </summary>
		/// <returns>Random hue value</returns>
		private int GetRandomHue()
		{
			return Utility.RandomList(
				RustyJunkConstants.HUE_RUSTY_1,
				RustyJunkConstants.HUE_RUSTY_2,
				RustyJunkConstants.HUE_RUSTY_3,
				RustyJunkConstants.HUE_RUSTY_4,
				RustyJunkConstants.HUE_RUSTY_5
			);
		}

		/// <summary>
		/// Calculates the smelting weight (ensures minimum of 1)
		/// </summary>
		/// <param name="weight">Current item weight</param>
		/// <returns>Adjusted weight for smelting</returns>
		private int CalculateSmeltingWeight(double weight)
		{
			int smeltingWeight = (int)weight;
			if (smeltingWeight < RustyJunkConstants.WEIGHT_MIN)
			{
				smeltingWeight = RustyJunkConstants.WEIGHT_MIN;
			}
			return smeltingWeight;
		}

		/// <summary>
		/// Performs the smelting operation at a forge
		/// </summary>
		/// <param name="from">The mobile performing the smelting</param>
		/// <param name="targeted">The forge target</param>
		private void PerformSmelting(Mobile from, object targeted)
		{
			int weight = CalculateSmeltingWeight(Weight);
			double difficulty = RustyJunkConstants.SMELTING_DIFFICULTY;
			double minSkill = RustyJunkConstants.SMELTING_MIN_SKILL;
			double maxSkill = RustyJunkConstants.SMELTING_MAX_SKILL;

			if (difficulty > from.Skills[SkillName.Mining].Value)
			{
				from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_NO_IDEA_SMELT);
				return;
			}

			if (from.CheckTargetSkill(SkillName.Mining, targeted, minSkill, maxSkill))
			{
				IronIngot ingot = new IronIngot(RustyJunkConstants.INGOT_AMOUNT_BASE);
				ingot.Amount = weight;
				from.AddToBackpack(ingot);
				from.PlaySound(RustyJunkConstants.SOUND_ID_SMELT);
				
				if (weight == RustyJunkConstants.WEIGHT_MIN)
				{
					from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.MSG_SMELTED_INGOT);
				}
				else
				{
					from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.MSG_SMELTED_INGOTS);
				}
				
				Delete();
			}
			else
			{
				from.PlaySound(RustyJunkConstants.SOUND_ID_SMELT);
				from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_FAILED_SMELT);
				Delete();
			}
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds name properties to the property list
		/// </summary>
		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
			list.Add(1070722, FishingStringConstants.FormatProperty(FishingStringConstants.PROP_SCRAP_IRON));
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the rusty junk item
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(RustyJunkConstants.SERIALIZATION_VERSION);
		}

		/// <summary>
		/// Deserializes the rusty junk item
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Internal target for selecting a forge to smelt the rusty junk
		/// </summary>
		private class InternalTarget : Target
		{
			private RustyJunk m_Rusted;

			/// <summary>
			/// Creates a new internal target for smelting
			/// </summary>
			/// <param name="ore">The rusty junk item to smelt</param>
			public InternalTarget(RustyJunk ore) : base(RustyJunkConstants.TARGET_RANGE, false, TargetFlags.None)
			{
				m_Rusted = ore;
			}

			/// <summary>
			/// Handles the target selection
			/// </summary>
			protected override void OnTarget(Mobile from, object targeted)
			{
				if (m_Rusted.Deleted)
				{
					return;
				}

				if (DefBlacksmithy.IsForge(targeted))
				{
					m_Rusted.PerformSmelting(from, targeted);
				}
			}
		}

		#endregion
	}
}

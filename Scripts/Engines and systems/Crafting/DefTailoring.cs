using System;
using System.Collections.Generic;
using System.Linq;
using Server.Items;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Defines the Tailoring craft system for creating clothing, leather armor, and bags.
	/// </summary>
	public class DefTailoring : CraftSystem
	{
		#region Properties

		/// <summary>
		/// Gets the main skill for this craft system (Tailoring).
		/// </summary>
		public override SkillName MainSkill
		{
			get { return SkillName.Tailoring; }
		}

		/// <summary>
		/// Gets the gump title number for the crafting menu.
		/// </summary>
		public override int GumpTitleNumber
		{
			get { return TailoringConstants.MSG_GUMP_TITLE; }
		}

		/// <summary>
		/// Gets the Exceptional Chance Adjustment type for this craft system.
		/// </summary>
		public override CraftECA ECA
		{
			get { return CraftECA.ChanceMinusSixtyToFourtyFive; }
		}

		#endregion

		#region Singleton

		private static CraftSystem m_CraftSystem;

		/// <summary>
		/// Gets the singleton instance of the Tailoring craft system.
		/// </summary>
		public static CraftSystem CraftSystem
		{
			get
			{
				if (m_CraftSystem == null)
					m_CraftSystem = new DefTailoring();

				return m_CraftSystem;
			}
		}

		#endregion

		#region Type Collections

		/// <summary>
		/// Set of non-colorable tailoring items.
		/// </summary>
		private static readonly HashSet<Type> m_TailorNonColorables = new HashSet<Type>
		{
			typeof(MinersPouch), typeof(LumberjackPouch),
			typeof(OrcHelm), typeof(BoneHelm),
			typeof(BoneGloves), typeof(BoneArms),
			typeof(BoneLegs), typeof(BoneChest)
		};

		/// <summary>
		/// Array of colorable tailoring items.
		/// </summary>
		private static Type[] m_TailorColorables = new Type[]
		{
			typeof(GozaMatEastDeed), typeof(GozaMatSouthDeed),
			typeof(SquareGozaMatEastDeed), typeof(SquareGozaMatSouthDeed),
			typeof(BrocadeGozaMatEastDeed), typeof(BrocadeGozaMatSouthDeed),
			typeof(BrocadeSquareGozaMatEastDeed), typeof(BrocadeSquareGozaMatSouthDeed)
		};

		/// <summary>
		/// Checks if a type is non-colorable.
		/// </summary>
		public static bool IsNonColorable(Type type)
		{
			return m_TailorNonColorables.Contains(type);
		}

		/// <summary>
		/// Determines if an item retains color from the specified material type.
		/// </summary>
		public override bool RetainsColorFrom(CraftItem item, Type type)
		{
			if (type != typeof(CottonCloth) && type != typeof(PoliesterCloth))
				return false;

			return m_TailorColorables.Contains(item.ItemType);
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the DefTailoring class.
		/// </summary>
		private DefTailoring() : base(
			TailoringConstants.MIN_CRAFT_EFFECT,
			TailoringConstants.MAX_CRAFT_EFFECT,
			TailoringConstants.CRAFT_DELAY_MULTIPLIER)
		{
		}

		#endregion

		#region Craft System Overrides

		/// <summary>
		/// Gets the minimum chance of success at minimum skill level.
		/// </summary>
		public override double GetChanceAtMin(CraftItem item)
		{
			return TailoringConstants.CHANCE_AT_MIN_SKILL;
		}

		/// <summary>
		/// Checks if the player can craft with the given tool.
		/// </summary>
		public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
		{
			if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
				return TailoringConstants.MSG_TOOL_WORN_OUT;

			if (!BaseTool.CheckAccessible(tool, from))
				return TailoringConstants.MSG_TOOL_MUST_BE_ON_PERSON;

			return 0;
		}

		/// <summary>
		/// Plays the crafting sound effect.
		/// </summary>
		public override void PlayCraftEffect(Mobile from)
		{
			from.PlaySound(TailoringConstants.SOUND_TAILORING_CRAFT);
		}

		/// <summary>
		/// Displays the appropriate message when crafting ends.
		/// </summary>
		public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
		{
			if (toolBroken)
				from.SendLocalizedMessage(TailoringConstants.MSG_TOOL_WORN_OUT);

			if (failed)
			{
				if (lostMaterial)
					return TailoringConstants.MSG_FAILED_LOST_MATERIALS;
				else
					return TailoringConstants.MSG_FAILED_NO_MATERIALS_LOST;
			}

			if (quality == TailoringConstants.QUALITY_BELOW_AVERAGE)
				return TailoringConstants.MSG_BARELY_MADE_ITEM;

			if (quality == TailoringConstants.QUALITY_EXCEPTIONAL)
			{
				if (makersMark)
					return TailoringConstants.MSG_EXCEPTIONAL_WITH_MARK;
				else
					return TailoringConstants.MSG_EXCEPTIONAL_QUALITY;
			}

			return TailoringConstants.MSG_ITEM_CREATED;
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Adds a cloth craft with automatic cotton cloth requirement.
		/// </summary>
		private int AddClothCraft(Type itemType, string groupName, int nameCliloc, double minSkill, double maxSkill, int clothAmount)
		{
			return AddCraft(itemType, groupName, nameCliloc, minSkill, maxSkill,
				typeof(CottonCloth), TailoringConstants.MSG_COTTON_CLOTH,
				clothAmount, TailoringConstants.MSG_INSUFFICIENT_CLOTH);
		}

		/// <summary>
		/// Adds a cloth craft using a custom name string.
		/// </summary>
		private int AddClothCraft(Type itemType, string groupName, string customName, double minSkill, double maxSkill, int clothAmount)
		{
			return AddCraft(itemType, groupName, customName, minSkill, maxSkill,
				typeof(CottonCloth), TailoringConstants.MSG_COTTON_CLOTH,
				clothAmount, TailoringConstants.MSG_INSUFFICIENT_CLOTH);
		}

		/// <summary>
		/// Adds a leather craft with automatic leather requirement and SubRes2 setting.
		/// </summary>
		private int AddLeatherCraft(Type itemType, string groupName, int nameCliloc, double minSkill, double maxSkill, int leatherAmount)
		{
			int index = AddCraft(itemType, groupName, nameCliloc, minSkill, maxSkill,
				typeof(Leather), TailoringConstants.MSG_LEATHER,
				leatherAmount, TailoringConstants.MSG_INSUFFICIENT_LEATHER);
			SetUseSubRes2(index, true);
			return index;
		}

		/// <summary>
		/// Adds a leather craft using a custom name string.
		/// </summary>
		private int AddLeatherCraft(Type itemType, string groupName, string customName, double minSkill, double maxSkill, int leatherAmount)
		{
			int index = AddCraft(itemType, groupName, customName, minSkill, maxSkill,
				typeof(Leather), TailoringConstants.MSG_LEATHER,
				leatherAmount, TailoringConstants.MSG_INSUFFICIENT_LEATHER);
			SetUseSubRes2(index, true);
			return index;
		}

		#endregion

		#region Craft List Initialization

		/// <summary>
		/// Initializes the list of craftable items for this craft system.
		/// </summary>
		public override void InitCraftList()
		{
			AddHats();
			AddClothing();
			AddPants();
			AddFootwear();
			AddMisc();
			AddLeatherArmor();
			AddBoneArmor();
			AddBags();

			// Configure material sub-resources
			InitializeClothTypes();
			InitializeLeatherTypes();

			// Configuration
			MarkOption = true;
			Repair = Core.AOS;
			CanEnhance = Core.AOS;
		}

		/// <summary>
		/// Adds hat crafts.
		/// </summary>
		private void AddHats()
		{
			AddClothCraft(typeof(SkullCap), TailoringStringConstants.GROUP_HATS, TailoringConstants.MSG_SKULL_CAP, 0.0, 25.0, 3);
			AddClothCraft(typeof(Bandana), TailoringStringConstants.GROUP_HATS, TailoringConstants.MSG_BANDANA, 0.0, 18.0, 2);
			AddClothCraft(typeof(FloppyHat), TailoringStringConstants.GROUP_HATS, TailoringConstants.MSG_FLOPPY_HAT, 16.2, 31.2, 9);
			AddClothCraft(typeof(Cap), TailoringStringConstants.GROUP_HATS, TailoringConstants.MSG_CAP, 16.2, 31.2, 9);

			int index = AddCraft(typeof(WideBrimHat), TailoringStringConstants.GROUP_HATS, TailoringConstants.MSG_WIDE_BRIM_HAT, 16.2, 31.2, typeof(CottonCloth), TailoringConstants.MSG_COTTON_CLOTH, 10, TailoringConstants.MSG_INSUFFICIENT_CLOTH);
			AddRes(index, typeof(Leather), TailoringStringConstants.RESOURCE_LEATHER_OR_FURS, 3, TailoringConstants.MSG_INSUFFICIENT_RESOURCES);

			AddClothCraft(typeof(StrawHat), TailoringStringConstants.GROUP_HATS, TailoringConstants.MSG_STRAW_HAT, 16.2, 31.2, 10);
			AddClothCraft(typeof(TallStrawHat), TailoringStringConstants.GROUP_HATS, TailoringConstants.MSG_TALL_STRAW_HAT, 16.7, 31.7, 12);
			AddClothCraft(typeof(Bonnet), TailoringStringConstants.GROUP_HATS, TailoringConstants.MSG_BONNET, 16.2, 31.2, 9);
			AddClothCraft(typeof(FeatheredHat), TailoringStringConstants.GROUP_HATS, TailoringConstants.MSG_FEATHERED_HAT, 16.2, 31.2, 12);
			AddClothCraft(typeof(TricorneHat), TailoringStringConstants.GROUP_HATS, TailoringConstants.MSG_TRICORNE_HAT, 16.2, 31.2, 12);
			AddClothCraft(typeof(PirateHat), TailoringStringConstants.GROUP_HATS, TailoringStringConstants.ITEM_PIRATE_HAT, 16.2, 31.2, 12);
			AddClothCraft(typeof(JesterHat), TailoringStringConstants.GROUP_HATS, TailoringConstants.MSG_JESTER_HAT, 27.2, 42.2, 15);

			index = AddClothCraft(typeof(WizardsHat), TailoringStringConstants.GROUP_HATS, TailoringConstants.MSG_WIZARDS_HAT, 77.2, 92.2, 21);
			AddSkill(index, SkillName.Magery, TailoringConstants.MAGERY_SKILL_MIN_WIZARDS_HAT, TailoringConstants.MAGERY_SKILL_MAX_WIZARDS_HAT);

			index = AddClothCraft(typeof(WitchHat), TailoringStringConstants.GROUP_HATS, TailoringStringConstants.ITEM_WITCH_HAT, 77.2, 92.2, 21);
			AddSkill(index, SkillName.Magery, TailoringConstants.MAGERY_SKILL_MIN_WITCH_HAT, TailoringConstants.MAGERY_SKILL_MAX_WITCH_HAT);
		}

		/// <summary>
		/// Adds clothing crafts (shirts, robes, cloaks).
		/// </summary>
		private void AddClothing()
		{
			AddClothCraft(typeof(Doublet), TailoringStringConstants.GROUP_CLOTHING, TailoringConstants.MSG_DOUBLET, 0, 25.0, 7);
			AddClothCraft(typeof(Shirt), TailoringStringConstants.GROUP_CLOTHING, TailoringConstants.MSG_SHIRT, 20.7, 45.7, 8);
			AddClothCraft(typeof(Tunic), TailoringStringConstants.GROUP_CLOTHING, TailoringConstants.MSG_TUNIC, 0.0, 25.0, 12);
			AddClothCraft(typeof(Surcoat), TailoringStringConstants.GROUP_CLOTHING, TailoringConstants.MSG_SURCOAT, 8.2, 33.2, 14);
			AddClothCraft(typeof(PlainDress), TailoringStringConstants.GROUP_CLOTHING, TailoringConstants.MSG_PLAIN_DRESS, 12.4, 37.4, 16);
			AddClothCraft(typeof(FancyDress), TailoringStringConstants.GROUP_CLOTHING, TailoringConstants.MSG_FANCY_DRESS, 33.1, 58.1, 18);
			AddClothCraft(typeof(Cloak), TailoringStringConstants.GROUP_CLOTHING, TailoringConstants.MSG_CLOAK, 41.4, 66.4, 15);
			AddClothCraft(typeof(Robe), TailoringStringConstants.GROUP_CLOTHING, TailoringConstants.MSG_ROBE, 53.9, 78.9, 18);
			AddClothCraft(typeof(FoolsCoat), TailoringStringConstants.GROUP_CLOTHING, TailoringStringConstants.ITEM_FOOLS_COAT, 70.0, 95.0, 13);
			AddClothCraft(typeof(FancyShirt), TailoringStringConstants.GROUP_CLOTHING, TailoringConstants.MSG_FANCY_SHIRT, 24.8, 49.8, 10);
		}

		/// <summary>
		/// Adds pants and skirt crafts.
		/// </summary>
		private void AddPants()
		{
			AddClothCraft(typeof(ShortPants), TailoringStringConstants.GROUP_PANTS, TailoringConstants.MSG_SHORT_PANTS, 24.8, 49.8, 8);
			AddClothCraft(typeof(LongPants), TailoringStringConstants.GROUP_PANTS, TailoringConstants.MSG_LONG_PANTS, 24.8, 49.8, 10);
			AddClothCraft(typeof(Kilt), TailoringStringConstants.GROUP_PANTS, TailoringConstants.MSG_KILT, 20.7, 45.7, 8);
			AddClothCraft(typeof(Skirt), TailoringStringConstants.GROUP_PANTS, TailoringConstants.MSG_SKIRT, 29.0, 54.0, 10);
			AddClothCraft(typeof(RoyalSkirt), TailoringStringConstants.GROUP_PANTS, TailoringStringConstants.ITEM_ROYAL_SKIRT, 20.7, 45.7, 6);
		}

		/// <summary>
		/// Adds footwear crafts.
		/// </summary>
		private void AddFootwear()
		{
			AddLeatherCraft(typeof(Sandals), TailoringStringConstants.GROUP_FOOTWEAR, TailoringConstants.MSG_SANDALS, 22.4, 37.4, 6);
			AddLeatherCraft(typeof(Shoes), TailoringStringConstants.GROUP_FOOTWEAR, TailoringConstants.MSG_SHOES, 26.5, 41.5, 8);
			AddLeatherCraft(typeof(Boots), TailoringStringConstants.GROUP_FOOTWEAR, TailoringConstants.MSG_BOOTS, 33.1, 58.1, 10);

			AddLeatherCraft(typeof(LeatherSandals), TailoringStringConstants.GROUP_FOOTWEAR, TailoringStringConstants.ITEM_LEATHER_SANDALS, 42.4, 67.4, 5);
			AddLeatherCraft(typeof(LeatherShoes), TailoringStringConstants.GROUP_FOOTWEAR, TailoringStringConstants.ITEM_LEATHER_SHOES, 56.5, 71.5, 7);
			AddLeatherCraft(typeof(LeatherBoots), TailoringStringConstants.GROUP_FOOTWEAR, TailoringStringConstants.ITEM_LEATHER_BOOTS, 63.1, 88.1, 9);
			AddLeatherCraft(typeof(LeatherThighBoots), TailoringStringConstants.GROUP_FOOTWEAR, TailoringStringConstants.ITEM_LEATHER_THIGH_BOOTS, 71.4, 96.4, 12);
		}

		/// <summary>
		/// Adds miscellaneous crafts (sashes, aprons).
		/// </summary>
		private void AddMisc()
		{
			AddClothCraft(typeof(BodySash), TailoringStringConstants.GROUP_MISC, TailoringConstants.MSG_BODY_SASH, 20.0, 29.1, 4);
			AddClothCraft(typeof(HalfApron), TailoringStringConstants.GROUP_MISC, TailoringConstants.MSG_HALF_APRON, 25.7, 45.7, 6);
			AddClothCraft(typeof(FullApron), TailoringStringConstants.GROUP_MISC, TailoringConstants.MSG_FULL_APRON, 29.0, 54.0, 12);
			AddClothCraft(typeof(OilCloth), TailoringStringConstants.GROUP_MISC, TailoringStringConstants.ITEM_OIL_CLOTH, 74.6, 99.6, 1);
		}

		/// <summary>
		/// Adds leather armor crafts.
		/// </summary>
		private void AddLeatherArmor()
		{
			// Leather armor
			AddLeatherCraft(typeof(LeatherCap), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_LEATHER_CAP, 50.0, 55.2, 4);
			AddLeatherCraft(typeof(LeatherGorget), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_LEATHER_GORGET, 53.9, 58.9, 4);
			AddLeatherCraft(typeof(LeatherGloves), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_LEATHER_GLOVES, 61.8, 66.8, 6);
			AddLeatherCraft(typeof(LeatherArms), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_LEATHER_ARMS, 63.9, 68.9, 10);
			AddLeatherCraft(typeof(LeatherLegs), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_LEATHER_LEGS, 66.3, 71.3, 12);
			AddLeatherCraft(typeof(LeatherChest), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_LEATHER_CHEST, 70.5, 75.5, 16);
			AddLeatherCraft(typeof(LeatherSkirt), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_LEATHER_SKIRT, 74.0, 78.0, 7);
			AddLeatherCraft(typeof(LeatherShorts), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_LEATHER_SHORTS, 72.2, 77.2, 8);
			AddLeatherCraft(typeof(LeatherBustierArms), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_LEATHER_BUSTIER_ARMS, 78.0, 83.0, 9);
			AddLeatherCraft(typeof(FemaleLeatherChest), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_FEMALE_LEATHER_CHEST, 82.2, 87.2, 11);

			// Studded armor
			AddLeatherCraft(typeof(StuddedGorget), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_STUDDED_GORGET, 68.8, 73.8, 6);
			AddLeatherCraft(typeof(StuddedGloves), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_STUDDED_GLOVES, 72.9, 77.9, 8);
			AddLeatherCraft(typeof(StuddedArms), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_STUDDED_ARMS, 77.1, 82.1, 12);
			AddLeatherCraft(typeof(StuddedLegs), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_STUDDED_LEGS, 81.2, 86.2, 14);
			AddLeatherCraft(typeof(StuddedChest), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_STUDDED_CHEST, 84.0, 89.0, 18);
			AddLeatherCraft(typeof(StuddedSkirt), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringStringConstants.ITEM_STUDDED_SKIRT, 71.2, 76.2, 9);
			AddLeatherCraft(typeof(StuddedBustierArms), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_STUDDED_BUSTIER_ARMS, 82.9, 87.9, 11);
			AddLeatherCraft(typeof(FemaleStuddedChest), TailoringStringConstants.GROUP_LEATHER_ARMOR, TailoringConstants.MSG_FEMALE_STUDDED_CHEST, 87.1, 92.1, 13);
		}

		/// <summary>
		/// Adds bone armor crafts.
		/// </summary>
		private void AddBoneArmor()
		{
			int index = AddLeatherCraft(typeof(BoneHelm), TailoringStringConstants.GROUP_BONE_ARMOR, TailoringConstants.MSG_BONE_HELM, 35.0, 40.1, 4);
			AddRes(index, typeof(PolishedSkull), TailoringStringConstants.RESOURCE_POLISHED_SKULL, 1, TailoringConstants.MSG_INSUFFICIENT_SKILL);
			AddRes(index, typeof(PolishedBone), TailoringStringConstants.RESOURCE_POLISHED_BONE, 1, TailoringConstants.MSG_INSUFFICIENT_SKILL);

			index = AddLeatherCraft(typeof(OrcHelm), TailoringStringConstants.GROUP_BONE_ARMOR, TailoringStringConstants.ITEM_ORC_HELM, 40.0, 45.1, 5);
			AddRes(index, typeof(PolishedSkull), TailoringStringConstants.RESOURCE_POLISHED_SKULL, 1, TailoringConstants.MSG_INSUFFICIENT_SKILL);
			AddRes(index, typeof(PolishedBone), TailoringStringConstants.RESOURCE_POLISHED_BONE, 3, TailoringConstants.MSG_INSUFFICIENT_SKILL);

			index = AddLeatherCraft(typeof(BoneGloves), TailoringStringConstants.GROUP_BONE_ARMOR, TailoringConstants.MSG_BONE_GLOVES, 39.0, 44.1, 6);
			AddRes(index, typeof(PolishedBone), TailoringStringConstants.RESOURCE_POLISHED_BONE, 4, TailoringConstants.MSG_INSUFFICIENT_SKILL);

			index = AddLeatherCraft(typeof(BoneArms), TailoringStringConstants.GROUP_BONE_ARMOR, TailoringConstants.MSG_BONE_ARMS, 52.0, 57.1, 10);
			AddRes(index, typeof(PolishedBone), TailoringStringConstants.RESOURCE_POLISHED_BONE, 8, TailoringConstants.MSG_INSUFFICIENT_SKILL);

			index = AddLeatherCraft(typeof(BoneLegs), TailoringStringConstants.GROUP_BONE_ARMOR, TailoringConstants.MSG_BONE_LEGS, 45.0, 50.1, 10);
			AddRes(index, typeof(PolishedBone), TailoringStringConstants.RESOURCE_POLISHED_BONE, 12, TailoringConstants.MSG_INSUFFICIENT_SKILL);

			index = AddLeatherCraft(typeof(BoneChest), TailoringStringConstants.GROUP_BONE_ARMOR, TailoringConstants.MSG_BONE_CHEST, 56.0, 61.0, 12);
			AddRes(index, typeof(PolishedBone), TailoringStringConstants.RESOURCE_POLISHED_BONE, 16, TailoringConstants.MSG_INSUFFICIENT_SKILL);
		}

		/// <summary>
		/// Adds bag and pouch crafts.
		/// </summary>
		private void AddBags()
		{
			AddLeatherCraft(typeof(Bag), TailoringStringConstants.GROUP_BAGS, TailoringStringConstants.ITEM_BAG, 30.0, 40.1, 11);
			AddLeatherCraft(typeof(LargeBag), TailoringStringConstants.GROUP_BAGS, TailoringStringConstants.ITEM_LARGE_BAG, 50.0, 60.1, 21);

			int index = AddCraft(typeof(GiantBag), TailoringStringConstants.GROUP_BAGS, TailoringStringConstants.ITEM_GIANT_BAG, 70.0, 80.1, typeof(GoliathLeather), TailoringConstants.MSG_GOLIATH_LEATHER, 20, TailoringConstants.MSG_INSUFFICIENT_LEATHER);
			SetUseSubRes2(index, true);

			AddLeatherCraft(typeof(Backpack), TailoringStringConstants.GROUP_BAGS, TailoringStringConstants.ITEM_BACKPACK, 50.0, 60.1, 15);
			AddLeatherCraft(typeof(RuggedBackpack), TailoringStringConstants.GROUP_BAGS, TailoringStringConstants.ITEM_RUGGED_BACKPACK, 60.0, 70.1, 25);

			index = AddCraft(typeof(MinersPouch), TailoringStringConstants.GROUP_BAGS, TailoringStringConstants.ITEM_MINERS_POUCH, 90.0, 100.1, typeof(GoliathLeather), TailoringConstants.MSG_GOLIATH_LEATHER, 50, TailoringConstants.MSG_INSUFFICIENT_GOLIATH_LEATHER);
			AddSkill(index, SkillName.Magery, TailoringConstants.MAGERY_SKILL_MIN_MINERS_POUCH, TailoringConstants.MAGERY_SKILL_MAX_MINERS_POUCH);
			AddRes(index, typeof(PlatinumIngot), TailoringStringConstants.RESOURCE_PLATINUM_INGOTS, 8, TailoringConstants.MSG_INSUFFICIENT_RESOURCES);
			SetUseSubRes2(index, true);

			index = AddCraft(typeof(LumberjackPouch), TailoringStringConstants.GROUP_BAGS, TailoringStringConstants.ITEM_LUMBERJACK_POUCH, 90.0, 100.1, typeof(GoliathLeather), TailoringConstants.MSG_GOLIATH_LEATHER, 50, TailoringConstants.MSG_INSUFFICIENT_GOLIATH_LEATHER);
			AddSkill(index, SkillName.Magery, TailoringConstants.MAGERY_SKILL_MIN_LUMBERJACK_POUCH, TailoringConstants.MAGERY_SKILL_MAX_LUMBERJACK_POUCH);
			AddRes(index, typeof(RosewoodBoard), TailoringStringConstants.RESOURCE_ROSEWOOD_BOARDS, 8, TailoringConstants.MSG_INSUFFICIENT_RESOURCES);
			SetUseSubRes2(index, true);
		}

		/// <summary>
		/// Initializes cloth type sub-resources.
		/// </summary>
		private void InitializeClothTypes()
		{
			SetSubRes(typeof(CottonCloth), TailoringConstants.MSG_CLOTH_COTTON);

			AddSubRes(typeof(CottonCloth), TailoringConstants.MSG_CLOTH_COTTON, TailoringConstants.SKILL_REQ_CLOTH_COTTON, TailoringConstants.MSG_NO_CLOTH_MATERIAL, TailoringConstants.MSG_CANNOT_WORK_CLOTH);
			AddSubRes(typeof(WoolCloth), TailoringConstants.MSG_CLOTH_WOOL, TailoringConstants.SKILL_REQ_CLOTH_WOOL, TailoringConstants.MSG_NO_CLOTH_MATERIAL, TailoringConstants.MSG_CANNOT_WORK_CLOTH);
			AddSubRes(typeof(FlaxCloth), TailoringConstants.MSG_CLOTH_FLAX, TailoringConstants.SKILL_REQ_CLOTH_FLAX, TailoringConstants.MSG_NO_CLOTH_MATERIAL, TailoringConstants.MSG_CANNOT_WORK_CLOTH);
			AddSubRes(typeof(SilkCloth), TailoringConstants.MSG_CLOTH_SILK, TailoringConstants.SKILL_REQ_CLOTH_SILK, TailoringConstants.MSG_NO_CLOTH_MATERIAL, TailoringConstants.MSG_CANNOT_WORK_CLOTH);
			AddSubRes(typeof(PoliesterCloth), TailoringConstants.MSG_CLOTH_POLIESTER, TailoringConstants.SKILL_REQ_CLOTH_POLIESTER, TailoringConstants.MSG_NO_CLOTH_MATERIAL, TailoringConstants.MSG_CANNOT_WORK_CLOTH);
		}

		/// <summary>
		/// Initializes leather type sub-resources.
		/// </summary>
		private void InitializeLeatherTypes()
		{
			SetSubRes2(typeof(Leather), TailoringConstants.MSG_LEATHER_REGULAR);

			AddSubRes2(typeof(Leather), TailoringConstants.MSG_LEATHER_REGULAR, TailoringConstants.SKILL_REQ_LEATHER_REGULAR, TailoringConstants.MSG_LEATHER, TailoringConstants.MSG_INSUFFICIENT_GOLIATH_LEATHER);
			AddSubRes2(typeof(SpinedLeather), TailoringConstants.MSG_LEATHER_SPINED, TailoringConstants.SKILL_REQ_LEATHER_SPINED, TailoringConstants.MSG_LEATHER, TailoringConstants.MSG_INSUFFICIENT_GOLIATH_LEATHER);
			AddSubRes2(typeof(HornedLeather), TailoringConstants.MSG_LEATHER_HORNED, TailoringConstants.SKILL_REQ_LEATHER_HORNED, TailoringConstants.MSG_LEATHER, TailoringConstants.MSG_INSUFFICIENT_GOLIATH_LEATHER);
			AddSubRes2(typeof(BarbedLeather), TailoringConstants.MSG_LEATHER_BARBED, TailoringConstants.SKILL_REQ_LEATHER_BARBED, TailoringConstants.MSG_LEATHER, TailoringConstants.MSG_INSUFFICIENT_GOLIATH_LEATHER);
		}

		#endregion
	}
}

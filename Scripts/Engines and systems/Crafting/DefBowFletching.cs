using System;
using Server.Items;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Defines the Bow Fletching craft system for creating bows, crossbows, arrows, and related items.
	/// </summary>
	public class DefBowFletching : CraftSystem
	{
		#region Properties

		/// <summary>
		/// Gets the main skill for this craft system (Fletching).
		/// </summary>
		public override SkillName MainSkill
		{
			get { return SkillName.Fletching; }
		}

		/// <summary>
		/// Gets the gump title number for the crafting menu.
		/// </summary>
		public override int GumpTitleNumber
		{
			get { return BowFletchingConstants.MSG_GUMP_TITLE; }
		}

		/// <summary>
		/// Gets the Exceptional Chance Adjustment type for this craft system.
		/// </summary>
		public override CraftECA ECA
		{
			get { return CraftECA.FiftyPercentChanceMinusTenPercent; }
		}

		#endregion

		#region Singleton

		private static CraftSystem m_CraftSystem;

		/// <summary>
		/// Gets the singleton instance of the Bow Fletching craft system.
		/// </summary>
		public static CraftSystem CraftSystem
		{
			get
			{
				if (m_CraftSystem == null)
					m_CraftSystem = new DefBowFletching();

				return m_CraftSystem;
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the DefBowFletching class.
		/// </summary>
		private DefBowFletching() : base(
			BowFletchingConstants.MIN_CRAFT_EFFECT,
			BowFletchingConstants.MAX_CRAFT_EFFECT,
			BowFletchingConstants.CRAFT_DELAY_MULTIPLIER)
		{
		}

		#endregion

		#region Craft System Overrides

		/// <summary>
		/// Gets the minimum chance of success at minimum skill level.
		/// </summary>
		public override double GetChanceAtMin(CraftItem item)
		{
			return BowFletchingConstants.CHANCE_AT_MIN_SKILL;
		}

		/// <summary>
		/// Checks if the player can craft with the given tool.
		/// </summary>
		public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
		{
			if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
				return BowFletchingConstants.MSG_TOOL_WORN_OUT;

			if (!BaseTool.CheckAccessible(tool, from))
				return BowFletchingConstants.MSG_TOOL_MUST_BE_ON_PERSON;

			return 0;
		}

		/// <summary>
		/// Plays the crafting sound effect.
		/// </summary>
		public override void PlayCraftEffect(Mobile from)
		{
			from.PlaySound(BowFletchingConstants.SOUND_FLETCHING_CRAFT);
		}

		/// <summary>
		/// Displays the appropriate message when crafting ends.
		/// </summary>
		public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
		{
			if (toolBroken)
				from.SendLocalizedMessage(BowFletchingConstants.MSG_TOOL_WORN_OUT);

			if (failed)
			{
				if (lostMaterial)
					return BowFletchingConstants.MSG_FAILED_LOST_MATERIALS;
				else
					return BowFletchingConstants.MSG_FAILED_NO_MATERIALS_LOST;
			}

			if (quality == BowFletchingConstants.QUALITY_BELOW_AVERAGE)
				return BowFletchingConstants.MSG_BARELY_MADE_ITEM;

			if (quality == BowFletchingConstants.QUALITY_EXCEPTIONAL)
			{
				if (makersMark)
					return BowFletchingConstants.MSG_EXCEPTIONAL_WITH_MARK;
				else
					return BowFletchingConstants.MSG_EXCEPTIONAL_QUALITY;
			}

			return BowFletchingConstants.MSG_ITEM_CREATED;
		}

		#endregion

		#region Craft List Initialization

		/// <summary>
		/// Initializes the list of craftable items for this craft system.
		/// </summary>
		public override void InitCraftList()
		{
			AddMaterialCrafts();
			AddAmmunitionCrafts();
			AddWeaponCrafts();

			// Configuration
			Repair = true;
			MarkOption = true;
			CanEnhance = Core.AOS;

			// Initialize wood type sub-resources
			InitializeWoodTypes();
		}

		/// <summary>
		/// Adds basic material crafts (kindling, shafts).
		/// </summary>
		private void AddMaterialCrafts()
		{
			// Kindling
			AddCraft(
				typeof(Kindling),
				BowFletchingConstants.MSG_GROUP_MATERIALS,
				BowFletchingStringConstants.ITEM_KINDLING,
				BowFletchingConstants.SKILL_MIN_KINDLING,
				BowFletchingConstants.SKILL_MAX_KINDLING,
				typeof(Log),
				BowFletchingConstants.MSG_LOG,
				BowFletchingConstants.RESOURCE_LOGS_KINDLING,
				BowFletchingConstants.MSG_INSUFFICIENT_WOOD);

			// Kindling batch (uses all resources)
			int index = AddCraft(
				typeof(Kindling),
				BowFletchingConstants.MSG_GROUP_MATERIALS,
				BowFletchingStringConstants.ITEM_KINDLING_BATCH,
				BowFletchingConstants.SKILL_MIN_KINDLING_BATCH,
				BowFletchingConstants.SKILL_MAX_KINDLING_BATCH,
				typeof(Log),
				BowFletchingConstants.MSG_LOG,
				BowFletchingConstants.RESOURCE_LOGS_KINDLING,
				BowFletchingConstants.MSG_INSUFFICIENT_WOOD);
			SetUseAllRes(index, true);

			// Shaft
			AddCraft(
				typeof(Shaft),
				BowFletchingConstants.MSG_GROUP_MATERIALS,
				BowFletchingConstants.MSG_SHAFT_ITEM,
				BowFletchingConstants.SKILL_MIN_SHAFT,
				BowFletchingConstants.SKILL_MAX_SHAFT,
				typeof(Log),
				BowFletchingConstants.MSG_LOG,
				BowFletchingConstants.RESOURCE_LOGS_SHAFT,
				BowFletchingConstants.MSG_INSUFFICIENT_WOOD);
		}

		/// <summary>
		/// Adds ammunition crafts (arrows, bolts, darts).
		/// </summary>
		private void AddAmmunitionCrafts()
		{
			// Arrow
			int index = AddCraft(
				typeof(Arrow),
				BowFletchingConstants.MSG_GROUP_AMMUNITION,
				BowFletchingConstants.MSG_ARROW,
				BowFletchingConstants.SKILL_MIN_ARROW,
				BowFletchingConstants.SKILL_MAX_ARROW,
				typeof(Shaft),
				BowFletchingConstants.MSG_SHAFT_RESOURCE,
				BowFletchingConstants.RESOURCE_SHAFTS_ARROW,
				BowFletchingConstants.MSG_INSUFFICIENT_SHAFTS);
			AddRes(index, typeof(Feather), BowFletchingConstants.MSG_FEATHER, BowFletchingConstants.RESOURCE_FEATHERS_ARROW, BowFletchingConstants.MSG_INSUFFICIENT_FEATHERS);

			// Bolt
			index = AddCraft(
				typeof(Bolt),
				BowFletchingConstants.MSG_GROUP_AMMUNITION,
				BowFletchingConstants.MSG_BOLT,
				BowFletchingConstants.SKILL_MIN_BOLT,
				BowFletchingConstants.SKILL_MAX_BOLT,
				typeof(Shaft),
				BowFletchingConstants.MSG_SHAFT_RESOURCE,
				BowFletchingConstants.RESOURCE_SHAFTS_BOLT,
				BowFletchingConstants.MSG_INSUFFICIENT_SHAFTS);
			AddRes(index, typeof(Feather), BowFletchingConstants.MSG_FEATHER, BowFletchingConstants.RESOURCE_FEATHERS_BOLT, BowFletchingConstants.MSG_INSUFFICIENT_FEATHERS);
			AddRes(index, typeof(IronIngot), BowFletchingConstants.MSG_IRON_INGOT, BowFletchingConstants.RESOURCE_IRON_INGOTS_BOLT, BowFletchingConstants.MSG_INSUFFICIENT_METAL);

			// SE-specific ammunition
			if (Core.SE)
			{
				AddCraft(
					typeof(FukiyaDarts),
					BowFletchingConstants.MSG_GROUP_AMMUNITION,
					BowFletchingConstants.MSG_FUKIYA_DARTS,
					BowFletchingConstants.SKILL_MIN_FUKIYA_DARTS,
					BowFletchingConstants.SKILL_MAX_FUKIYA_DARTS,
					typeof(Log),
					BowFletchingConstants.MSG_LOG,
					BowFletchingConstants.RESOURCE_LOGS_FUKIYA_DARTS,
					BowFletchingConstants.MSG_INSUFFICIENT_WOOD);

				AddCraft(
					typeof(ThrowingWeapon),
					BowFletchingConstants.MSG_GROUP_AMMUNITION,
					BowFletchingConstants.MSG_THROWING_WEAPON,
					BowFletchingConstants.SKILL_MIN_THROWING_WEAPON,
					BowFletchingConstants.SKILL_MAX_THROWING_WEAPON,
					typeof(IronIngot),
					BowFletchingConstants.MSG_IRON_INGOT,
					BowFletchingConstants.RESOURCE_IRON_INGOTS_THROWING_WEAPON,
					BowFletchingConstants.MSG_INSUFFICIENT_METAL);
			}
		}

		/// <summary>
		/// Adds weapon crafts (bows, crossbows).
		/// </summary>
		private void AddWeaponCrafts()
		{
			// Standard weapons
			AddCraft(
				typeof(Bow),
				BowFletchingConstants.MSG_GROUP_WEAPONS,
				BowFletchingConstants.MSG_BOW,
				BowFletchingConstants.SKILL_MIN_BOW,
				BowFletchingConstants.SKILL_MAX_BOW,
				typeof(Board),
				BowFletchingConstants.MSG_BOARD,
				BowFletchingConstants.RESOURCE_BOARDS_BOW,
				BowFletchingConstants.MSG_INSUFFICIENT_WOOD);

			AddCraft(
				typeof(Crossbow),
				BowFletchingConstants.MSG_GROUP_WEAPONS,
				BowFletchingConstants.MSG_CROSSBOW,
				BowFletchingConstants.SKILL_MIN_CROSSBOW,
				BowFletchingConstants.SKILL_MAX_CROSSBOW,
				typeof(Board),
				BowFletchingConstants.MSG_BOARD,
				BowFletchingConstants.RESOURCE_BOARDS_CROSSBOW,
				BowFletchingConstants.MSG_INSUFFICIENT_WOOD);

			AddCraft(
				typeof(HeavyCrossbow),
				BowFletchingConstants.MSG_GROUP_WEAPONS,
				BowFletchingConstants.MSG_HEAVY_CROSSBOW,
				BowFletchingConstants.SKILL_MIN_HEAVY_CROSSBOW,
				BowFletchingConstants.SKILL_MAX_HEAVY_CROSSBOW,
				typeof(Board),
				BowFletchingConstants.MSG_BOARD,
				BowFletchingConstants.RESOURCE_BOARDS_HEAVY_CROSSBOW,
				BowFletchingConstants.MSG_INSUFFICIENT_WOOD);

			// AOS weapons
			if (Core.AOS)
			{
				AddCraft(
					typeof(CompositeBow),
					BowFletchingConstants.MSG_GROUP_WEAPONS,
					BowFletchingConstants.MSG_COMPOSITE_BOW,
					BowFletchingConstants.SKILL_MIN_COMPOSITE_BOW,
					BowFletchingConstants.SKILL_MAX_COMPOSITE_BOW,
					typeof(Board),
					BowFletchingConstants.MSG_BOARD,
					BowFletchingConstants.RESOURCE_BOARDS_COMPOSITE_BOW,
					BowFletchingConstants.MSG_INSUFFICIENT_WOOD);

				AddCraft(
					typeof(RepeatingCrossbow),
					BowFletchingConstants.MSG_GROUP_WEAPONS,
					BowFletchingConstants.MSG_REPEATING_CROSSBOW,
					BowFletchingConstants.SKILL_MIN_REPEATING_CROSSBOW,
					BowFletchingConstants.SKILL_MAX_REPEATING_CROSSBOW,
					typeof(Board),
					BowFletchingConstants.MSG_BOARD,
					BowFletchingConstants.RESOURCE_BOARDS_REPEATING_CROSSBOW,
					BowFletchingConstants.MSG_INSUFFICIENT_WOOD);
			}

			// SE weapons
			if (Core.SE)
			{
				AddCraft(
					typeof(Yumi),
					BowFletchingConstants.MSG_GROUP_WEAPONS,
					BowFletchingConstants.MSG_YUMI,
					BowFletchingConstants.SKILL_MIN_YUMI,
					BowFletchingConstants.SKILL_MAX_YUMI,
					typeof(Board),
					BowFletchingConstants.MSG_BOARD,
					BowFletchingConstants.RESOURCE_BOARDS_YUMI,
					BowFletchingConstants.MSG_INSUFFICIENT_WOOD);
			}

			// Custom weapons
			AddCraft(
				typeof(MagicalShortbow),
				BowFletchingConstants.MSG_GROUP_WEAPONS,
				BowFletchingStringConstants.ITEM_MAGICAL_SHORTBOW,
				BowFletchingConstants.SKILL_MIN_MAGICAL_SHORTBOW,
				BowFletchingConstants.SKILL_MAX_MAGICAL_SHORTBOW,
				typeof(Board),
				BowFletchingConstants.MSG_BOARD,
				BowFletchingConstants.RESOURCE_BOARDS_MAGICAL_SHORTBOW,
				BowFletchingConstants.MSG_INSUFFICIENT_WOOD);

			AddCraft(
				typeof(ElvenCompositeLongbow),
				BowFletchingConstants.MSG_GROUP_WEAPONS,
				BowFletchingStringConstants.ITEM_ELVEN_COMPOSITE_LONGBOW,
				BowFletchingConstants.SKILL_MIN_ELVEN_COMPOSITE_LONGBOW,
				BowFletchingConstants.SKILL_MAX_ELVEN_COMPOSITE_LONGBOW,
				typeof(Board),
				BowFletchingConstants.MSG_BOARD,
				BowFletchingConstants.RESOURCE_BOARDS_ELVEN_COMPOSITE_LONGBOW,
				BowFletchingConstants.MSG_INSUFFICIENT_WOOD);
		}

		/// <summary>
		/// Initializes wood type sub-resources for material selection.
		/// </summary>
		private void InitializeWoodTypes()
		{
			SetSubRes(typeof(Board), BowFletchingConstants.MSG_BOARD_MATERIAL);

			// Add each wood type with skill requirements
			AddSubRes(
				typeof(Board),
				BowFletchingConstants.MSG_BOARD_MATERIAL,
				BowFletchingConstants.SKILL_REQ_WOOD_COMMON,
				BowFletchingConstants.MSG_LOG,
				BowFletchingConstants.MSG_CANNOT_WORK_WOOD);

			AddSubRes(
				typeof(AshBoard),
				BowFletchingConstants.MSG_ASH_BOARD,
				BowFletchingConstants.SKILL_REQ_WOOD_ASH,
				BowFletchingConstants.MSG_LOG,
				BowFletchingConstants.MSG_CANNOT_WORK_WOOD);

			AddSubRes(
				typeof(EbonyBoard),
				BowFletchingConstants.MSG_EBONY_BOARD,
				BowFletchingConstants.SKILL_REQ_WOOD_EBONY,
				BowFletchingConstants.MSG_LOG,
				BowFletchingConstants.MSG_CANNOT_WORK_WOOD);

			AddSubRes(
				typeof(ElvenBoard),
				BowFletchingConstants.MSG_ELVEN_BOARD,
				BowFletchingConstants.SKILL_REQ_WOOD_ELVEN,
				BowFletchingConstants.MSG_LOG,
				BowFletchingConstants.MSG_CANNOT_WORK_WOOD);

			AddSubRes(
				typeof(GoldenOakBoard),
				BowFletchingConstants.MSG_GOLDEN_OAK_BOARD,
				BowFletchingConstants.SKILL_REQ_WOOD_GOLDEN_OAK,
				BowFletchingConstants.MSG_LOG,
				BowFletchingConstants.MSG_CANNOT_WORK_WOOD);

			AddSubRes(
				typeof(CherryBoard),
				BowFletchingConstants.MSG_CHERRY_BOARD,
				BowFletchingConstants.SKILL_REQ_WOOD_CHERRY,
				BowFletchingConstants.MSG_LOG,
				BowFletchingConstants.MSG_CANNOT_WORK_WOOD);

			AddSubRes(
				typeof(RosewoodBoard),
				BowFletchingConstants.MSG_ROSEWOOD_BOARD,
				BowFletchingConstants.SKILL_REQ_WOOD_ROSEWOOD,
				BowFletchingConstants.MSG_LOG,
				BowFletchingConstants.MSG_CANNOT_WORK_WOOD);

			AddSubRes(
				typeof(HickoryBoard),
				BowFletchingConstants.MSG_HICKORY_BOARD,
				BowFletchingConstants.SKILL_REQ_WOOD_HICKORY,
				BowFletchingConstants.MSG_LOG,
				BowFletchingConstants.MSG_CANNOT_WORK_WOOD);
		}

		#endregion
	}
}

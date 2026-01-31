using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Defines the Alchemy craft system for creating potions, elixirs, and alchemical mixtures.
	/// </summary>
	public class DefAlchemy : CraftSystem
	{
		#region Properties

		/// <summary>
		/// Gets the main skill for this craft system (Alchemy).
		/// </summary>
		public override SkillName MainSkill
		{
			get { return SkillName.Alchemy; }
		}

		/// <summary>
		/// Gets the gump title number for the crafting menu.
		/// </summary>
		public override int GumpTitleNumber
		{
			get { return AlchemyConstants.MSG_GUMP_TITLE; }
		}

		#endregion

		#region Singleton

		private static CraftSystem m_CraftSystem;

		/// <summary>
		/// Gets the singleton instance of the Alchemy craft system.
		/// </summary>
		public static CraftSystem CraftSystem
		{
			get
			{
				if (m_CraftSystem == null)
					m_CraftSystem = new DefAlchemy();

				return m_CraftSystem;
			}
		}

		#endregion

		#region Type Checking Helpers

		private static Type typeofPotion = typeof(BasePotion);
		private static Type typeofLiquid = typeof(BaseLiquid);
		private static Type typeofMixture = typeof(BaseMixture);

		/// <summary>
		/// Checks if a type is a potion.
		/// </summary>
		public static bool IsPotion(Type type)
		{
			return typeofPotion.IsAssignableFrom(type);
		}

		/// <summary>
		/// Checks if a type is a liquid.
		/// </summary>
		public static bool IsLiquid(Type type)
		{
			return typeofLiquid.IsAssignableFrom(type);
		}

		/// <summary>
		/// Checks if a type is a mixture.
		/// </summary>
		public static bool IsMixture(Type type)
		{
			return typeofMixture.IsAssignableFrom(type);
		}

		/// <summary>
		/// Checks if a type is a liquid or mixture.
		/// </summary>
		private static bool IsLiquidOrMixture(Type type)
		{
			return IsLiquid(type) || IsMixture(type);
		}

		#endregion

		#region Poisoning Skill Training

		/// <summary>
		/// Maps poison potion types to their maximum skill training values.
		/// </summary>
		private static readonly Dictionary<Type, double> PoisonPotionSkillMap = new Dictionary<Type, double>
		{
			{ typeof(LesserPoisonPotion), AlchemyConstants.POISONING_SKILL_LESSER },
			{ typeof(PoisonPotion), AlchemyConstants.POISONING_SKILL_REGULAR },
			{ typeof(GreaterPoisonPotion), AlchemyConstants.POISONING_SKILL_GREATER },
			{ typeof(DeadlyPoisonPotion), AlchemyConstants.POISONING_SKILL_DEADLY },
			{ typeof(LethalPoisonPotion), AlchemyConstants.POISONING_SKILL_LETHAL }
		};

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the DefAlchemy class.
		/// </summary>
		private DefAlchemy() : base(
			AlchemyConstants.MIN_CRAFT_EFFECT,
			AlchemyConstants.MAX_CRAFT_EFFECT,
			AlchemyConstants.CRAFT_DELAY_MULTIPLIER)
		{
		}

		#endregion

		#region Craft System Overrides

		/// <summary>
		/// Gets the minimum chance of success at minimum skill level.
		/// </summary>
		public override double GetChanceAtMin(CraftItem item)
		{
			return AlchemyConstants.CHANCE_AT_MIN_SKILL;
		}

		/// <summary>
		/// Checks if the player can craft with the given tool.
		/// </summary>
		public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
		{
			if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
			{
				from.SendMessage( 55, AlchemyConstants.MSG_TOOL_WORN_OUT );
				return 500295; // Generic error number
			}

			if (!BaseTool.CheckAccessible(tool, from))
			{
				from.SendMessage( 55, AlchemyConstants.MSG_TOOL_MUST_BE_ON_PERSON );
				return 500295; // Generic error number
			}

			return 0;
		}

		/// <summary>
		/// Plays the crafting sound effect.
		/// </summary>
		public override void PlayCraftEffect(Mobile from)
		{
			from.PlaySound(AlchemyConstants.SOUND_ALCHEMY_CRAFT);
		}

		/// <summary>
		/// Displays the appropriate message when crafting ends.
		/// </summary>
		public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
		{
			Server.Gumps.MReagentGump.XReagentGump(from);

			if (toolBroken)
				from.SendMessage( 55, AlchemyConstants.MSG_TOOL_WORN_OUT );

			if (failed)
				return HandleCraftFailure(from, item);

			return HandleCraftSuccess(from, item, quality);
		}

		#endregion

		#region Craft Ending Effect Helpers

		/// <summary>
		/// Handles the craft failure scenario.
		/// </summary>
		private int HandleCraftFailure(Mobile from, CraftItem item)
		{
			if (IsLiquidOrMixture(item.ItemType))
			{
				AddEmptyBottle(from);
				AddEmptyJar(from);
				from.SendMessage( 55, AlchemyConstants.MSG_FAILED_POTION );
			}
			else if (IsPotion(item.ItemType))
			{
				AddEmptyBottle(from);
				from.SendMessage( 55, AlchemyConstants.MSG_FAILED_POTION );
			}
			else
			{
				from.SendMessage( 55, AlchemyConstants.MSG_FAILED_LOST_MATERIALS );
			}

			return 0;
		}

		/// <summary>
		/// Handles the craft success scenario.
		/// </summary>
		private int HandleCraftSuccess(Mobile from, CraftItem item, int quality)
		{
			if (IsLiquidOrMixture(item.ItemType))
				AddEmptyBottle(from);

			from.PlaySound(AlchemyConstants.SOUND_BOTTLE_FILLING);

			CheckPoisoningSkillGain(from, item.ItemType);

			GetSuccessMessage(from, item.ItemType, quality);

			return 0;
		}

		/// <summary>
		/// Adds an empty bottle to the player's backpack.
		/// </summary>
		private void AddEmptyBottle(Mobile from)
		{
			from.AddToBackpack(new Bottle());
		}

		/// <summary>
		/// Adds an empty jar to the player's backpack.
		/// </summary>
		private void AddEmptyJar(Mobile from)
		{
			from.AddToBackpack(new Jar());
		}

		/// <summary>
		/// Checks if the player should gain poisoning skill when crafting poison potions.
		/// </summary>
		private void CheckPoisoningSkillGain(Mobile from, Type itemType)
		{
			if (!(from is PlayerMobile) || !Utility.RandomBool())
				return;

			double maxSkill;
			if (PoisonPotionSkillMap.TryGetValue(itemType, out maxSkill))
				from.CheckSkill(SkillName.Poisoning, AlchemyConstants.POISONING_SKILL_MIN, maxSkill);
		}

		/// <summary>
		/// Gets the appropriate success message based on item type and quality.
		/// </summary>
		private void GetSuccessMessage(Mobile from, Type itemType, int quality)
		{
			if (IsPotion(itemType))
			{
				if (quality == AlchemyConstants.QUALITY_KEG)
					from.SendMessage( 95, AlchemyConstants.MSG_POTION_TO_KEG );
				else
					from.SendMessage( 95, AlchemyConstants.MSG_POTION_TO_BOTTLE );
			}
			else
			{
				from.SendMessage( 95, AlchemyConstants.MSG_ITEM_CREATED );
			}
		}

		#endregion

		#region Craft List Initialization

		/// <summary>
		/// Initializes the list of craftable items for this craft system.
		/// </summary>
	public override void InitCraftList()
	{
		AddAgilityPotions();
		AddCurePotions();
		AddExplosionPotions();
		AddHealPotions();
		AddNightsightPotion();
		AddRefreshPotions();
		AddStrengthPotions();
		AddPoisonPotions();
		AddManaPotions();
		AddInvisibilityPotions();
		AddFrostbitePotions();
		AddConfusionBlastPotions();
		AddCosmeticPotions();
	}

		/// <summary>
		/// Gets the craft group name based on recipe ID.
		/// </summary>
		private string GetGroupForRecipe(int recipeID)
		{
			// Category 0 (BASIC): All Lesser and regular potions
			if (recipeID == 500 || recipeID == 501 || recipeID == 502 || recipeID == 503 || recipeID == 504 ||
				recipeID == 505 || recipeID == 506 || recipeID == 507 || recipeID == 509 || recipeID == 512 ||
				recipeID == 514 || recipeID == 515 || recipeID == 521 || recipeID == 522 || recipeID == 524 ||
				recipeID == 525)
			{
				return AlchemyStringConstants.GROUP_BASIC;
			}
			// Category 1 (ADVANCED): All Greater potions
			else if (recipeID == 508 || recipeID == 510 || recipeID == 511 || recipeID == 513 || recipeID == 516 ||
				recipeID == 517 || recipeID == 518 || recipeID == 519 || recipeID == 520 || recipeID == 523 ||
				recipeID == 526)
			{
				return AlchemyStringConstants.GROUP_ADVANCED;
			}
			// Category 2 (SPECIAL): Frostbite, Confusion, Conflagration
			else if (recipeID == 527 || recipeID == 528 || recipeID == 529 || recipeID == 530 || recipeID == 531 ||
				recipeID == 532)
			{
				return AlchemyStringConstants.GROUP_SPECIAL;
			}
			// Category 3 (COSMETIC): Hair cut and tint
			else if (recipeID == 533 || recipeID == 534)
			{
				return AlchemyStringConstants.GROUP_COSMETIC;
			}
			// Default fallback
			return AlchemyStringConstants.GROUP_BASIC;
		}

		/// <summary>
		/// Adds a potion craft with automatic bottle requirement.
		/// </summary>
		private void AddPotionCraft(Type potionType, string name, double minSkill, double maxSkill,
			Type reagent, TextDefinition reagentNameId, int reagentAmount, TextDefinition errorId, int recipeID)
		{
			string groupName = GetGroupForRecipe(recipeID);
			int index = AddCraft(potionType, groupName, name,
				minSkill, maxSkill, reagent, reagentNameId, reagentAmount, errorId);
			AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
				AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		}

	/// <summary>
	/// Adds agility potion crafts.
	/// </summary>
	private void AddAgilityPotions()
	{
		int index = AddCraft(
			typeof(AgilityPotion),
			AlchemyStringConstants.GROUP_BASIC,
			AlchemyStringConstants.ITEM_AGILITY_POTION,
			AlchemyConstants.SKILL_MIN_AGILITY,
			AlchemyConstants.SKILL_MAX_AGILITY,
			typeof(Bloodmoss),
			AlchemyConstants.MSG_BLOODMOSS,
			AlchemyConstants.RESOURCE_BLOODMOSS_AGILITY,
			AlchemyConstants.MSG_INSUFFICIENT_BLOODMOSS);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 500); // Recipe ID 500

		index = AddCraft(
			typeof(GreaterAgilityPotion),
			AlchemyStringConstants.GROUP_ADVANCED,
			AlchemyStringConstants.ITEM_GREATER_AGILITY_POTION,
			AlchemyConstants.SKILL_MIN_GREATER_AGILITY,
			AlchemyConstants.SKILL_MAX_GREATER_AGILITY,
			typeof(Bloodmoss),
			AlchemyConstants.MSG_BLOODMOSS,
			AlchemyConstants.RESOURCE_BLOODMOSS_GREATER_AGILITY,
			AlchemyConstants.MSG_INSUFFICIENT_BLOODMOSS);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 519); // Recipe ID 519
	}

	/// <summary>
	/// Adds cure potion crafts.
	/// </summary>
	private void AddCurePotions()
	{
		int index = AddCraft(
			typeof(LesserCurePotion),
			AlchemyStringConstants.GROUP_BASIC,
			AlchemyStringConstants.ITEM_LESSER_CURE_POTION,
			AlchemyConstants.SKILL_MIN_LESSER_CURE,
			AlchemyConstants.SKILL_MAX_LESSER_CURE,
			typeof(Garlic),
			AlchemyConstants.MSG_GARLIC,
			AlchemyConstants.RESOURCE_GARLIC_LESSER_CURE,
			AlchemyConstants.MSG_INSUFFICIENT_GARLIC);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 502); // Recipe ID 502

		index = AddCraft(
			typeof(CurePotion),
			AlchemyStringConstants.GROUP_BASIC,
			AlchemyStringConstants.ITEM_CURE_POTION,
			AlchemyConstants.SKILL_MIN_CURE,
			AlchemyConstants.SKILL_MAX_CURE,
			typeof(Garlic),
			AlchemyConstants.MSG_GARLIC,
			AlchemyConstants.RESOURCE_GARLIC_CURE,
			AlchemyConstants.MSG_INSUFFICIENT_GARLIC);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 507); // Recipe ID 507

		index = AddCraft(
			typeof(GreaterCurePotion),
			AlchemyStringConstants.GROUP_ADVANCED,
			AlchemyStringConstants.ITEM_GREATER_CURE_POTION,
			AlchemyConstants.SKILL_MIN_GREATER_CURE,
			AlchemyConstants.SKILL_MAX_GREATER_CURE,
			typeof(Garlic),
			AlchemyConstants.MSG_GARLIC,
			AlchemyConstants.RESOURCE_GARLIC_GREATER_CURE,
			AlchemyConstants.MSG_INSUFFICIENT_GARLIC);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 508); // Recipe ID 508
	}

	/// <summary>
	/// Adds explosion potion crafts.
	/// </summary>
	private void AddExplosionPotions()
	{
		int index = AddCraft(
			typeof(LesserExplosionPotion),
			AlchemyStringConstants.GROUP_BASIC,
			AlchemyStringConstants.ITEM_LESSER_EXPLOSION_POTION,
			AlchemyConstants.SKILL_MIN_LESSER_EXPLOSION,
			AlchemyConstants.SKILL_MAX_LESSER_EXPLOSION,
			typeof(SulfurousAsh),
			AlchemyConstants.MSG_SULFUROUS_ASH,
			AlchemyConstants.RESOURCE_SULFUROUS_ASH_LESSER_EXPLOSION,
			AlchemyConstants.MSG_INSUFFICIENT_SULFUROUS_ASH);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 504); // Recipe ID 504

		index = AddCraft(
			typeof(ExplosionPotion),
			AlchemyStringConstants.GROUP_BASIC,
			AlchemyStringConstants.ITEM_EXPLOSION_POTION,
			AlchemyConstants.SKILL_MIN_EXPLOSION,
			AlchemyConstants.SKILL_MAX_EXPLOSION,
			typeof(SulfurousAsh),
			AlchemyConstants.MSG_SULFUROUS_ASH,
			AlchemyConstants.RESOURCE_SULFUROUS_ASH_EXPLOSION,
			AlchemyConstants.MSG_INSUFFICIENT_SULFUROUS_ASH);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 512); // Recipe ID 512

		index = AddCraft(
			typeof(GreaterExplosionPotion),
			AlchemyStringConstants.GROUP_ADVANCED,
			AlchemyStringConstants.ITEM_GREATER_EXPLOSION_POTION,
			AlchemyConstants.SKILL_MIN_GREATER_EXPLOSION,
			AlchemyConstants.SKILL_MAX_GREATER_EXPLOSION,
			typeof(SulfurousAsh),
			AlchemyConstants.MSG_SULFUROUS_ASH,
			AlchemyConstants.RESOURCE_SULFUROUS_ASH_GREATER_EXPLOSION,
			AlchemyConstants.MSG_INSUFFICIENT_SULFUROUS_ASH);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 513); // Recipe ID 513
	}

	/// <summary>
	/// Adds heal potion crafts.
	/// </summary>
	private void AddHealPotions()
	{
		int index = AddCraft(
			typeof(LesserHealPotion),
			AlchemyStringConstants.GROUP_BASIC,
			AlchemyStringConstants.ITEM_LESSER_HEAL_POTION,
			AlchemyConstants.SKILL_MIN_LESSER_HEAL,
			AlchemyConstants.SKILL_MAX_LESSER_HEAL,
			typeof(Ginseng),
			AlchemyConstants.MSG_GINSENG,
			AlchemyConstants.RESOURCE_GINSENG_LESSER_HEAL,
			AlchemyConstants.MSG_INSUFFICIENT_GINSENG);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 503); // Recipe ID 503

		index = AddCraft(
			typeof(HealPotion),
			AlchemyStringConstants.GROUP_BASIC,
			AlchemyStringConstants.ITEM_HEAL_POTION,
			AlchemyConstants.SKILL_MIN_HEAL,
			AlchemyConstants.SKILL_MAX_HEAL,
			typeof(Ginseng),
			AlchemyConstants.MSG_GINSENG,
			AlchemyConstants.RESOURCE_GINSENG_HEAL,
			AlchemyConstants.MSG_INSUFFICIENT_GINSENG);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 509); // Recipe ID 509

		index = AddCraft(
			typeof(GreaterHealPotion),
			AlchemyStringConstants.GROUP_ADVANCED,
			AlchemyStringConstants.ITEM_GREATER_HEAL_POTION,
			AlchemyConstants.SKILL_MIN_GREATER_HEAL,
			AlchemyConstants.SKILL_MAX_GREATER_HEAL,
			typeof(Ginseng),
			AlchemyConstants.MSG_GINSENG,
			AlchemyConstants.RESOURCE_GINSENG_GREATER_HEAL,
			AlchemyConstants.MSG_INSUFFICIENT_GINSENG);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 510); // Recipe ID 510
	}

	/// <summary>
	/// Adds nightsight potion craft.
	/// </summary>
	private void AddNightsightPotion()
	{
		int index = AddCraft(
			typeof(NightSightPotion),
			AlchemyStringConstants.GROUP_BASIC,
			AlchemyStringConstants.ITEM_NIGHTSIGHT_POTION,
			AlchemyConstants.SKILL_MIN_NIGHTSIGHT,
			AlchemyConstants.SKILL_MAX_NIGHTSIGHT,
			typeof(SpidersSilk),
			AlchemyConstants.MSG_SPIDERS_SILK,
			AlchemyConstants.RESOURCE_SPIDERS_SILK_NIGHTSIGHT,
			AlchemyConstants.MSG_INSUFFICIENT_SPIDERS_SILK);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 501); // Recipe ID 501
	}

	/// <summary>
	/// Adds refresh potion crafts.
	/// </summary>
	private void AddRefreshPotions()
	{
		int index = AddCraft(
			typeof(RefreshPotion),
			AlchemyStringConstants.GROUP_BASIC,
			AlchemyStringConstants.ITEM_REFRESH_POTION,
			AlchemyConstants.SKILL_MIN_REFRESH,
			AlchemyConstants.SKILL_MAX_REFRESH,
			typeof(BlackPearl),
			AlchemyConstants.MSG_BLACK_PEARL,
			AlchemyConstants.RESOURCE_BLACK_PEARL_REFRESH,
			AlchemyConstants.MSG_INSUFFICIENT_BLACK_PEARL);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 505); // Recipe ID 505

		index = AddCraft(
			typeof(TotalRefreshPotion),
			AlchemyStringConstants.GROUP_ADVANCED,
			AlchemyStringConstants.ITEM_TOTAL_REFRESH_POTION,
			AlchemyConstants.SKILL_MIN_TOTAL_REFRESH,
			AlchemyConstants.SKILL_MAX_TOTAL_REFRESH,
			typeof(BlackPearl),
			AlchemyConstants.MSG_BLACK_PEARL,
			AlchemyConstants.RESOURCE_BLACK_PEARL_TOTAL_REFRESH,
			AlchemyConstants.MSG_INSUFFICIENT_BLACK_PEARL);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 511); // Recipe ID 511
	}

	/// <summary>
	/// Adds strength potion crafts.
	/// </summary>
	private void AddStrengthPotions()
	{
		int index = AddCraft(
			typeof(StrengthPotion),
			AlchemyStringConstants.GROUP_BASIC,
			AlchemyStringConstants.ITEM_STRENGTH_POTION,
			AlchemyConstants.SKILL_MIN_STRENGTH,
			AlchemyConstants.SKILL_MAX_STRENGTH,
			typeof(MandrakeRoot),
			AlchemyConstants.MSG_MANDRAKE_ROOT,
			AlchemyConstants.RESOURCE_MANDRAKE_ROOT_STRENGTH,
			AlchemyConstants.MSG_INSUFFICIENT_MANDRAKE_ROOT);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 506); // Recipe ID 506

		index = AddCraft(
			typeof(GreaterStrengthPotion),
			AlchemyStringConstants.GROUP_ADVANCED,
			AlchemyStringConstants.ITEM_GREATER_STRENGTH_POTION,
			AlchemyConstants.SKILL_MIN_GREATER_STRENGTH,
			AlchemyConstants.SKILL_MAX_GREATER_STRENGTH,
			typeof(MandrakeRoot),
			AlchemyConstants.MSG_MANDRAKE_ROOT,
			AlchemyConstants.RESOURCE_MANDRAKE_ROOT_GREATER_STRENGTH,
			AlchemyConstants.MSG_INSUFFICIENT_MANDRAKE_ROOT);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 520); // Recipe ID 520
	}

	/// <summary>
	/// Adds poison potion crafts.
	/// </summary>
	private void AddPoisonPotions()
	{
		// Lesser Poison Potion
		int index = AddCraft(
			typeof(LesserPoisonPotion),
			AlchemyStringConstants.GROUP_BASIC,
			AlchemyStringConstants.ITEM_LESSER_POISON_POTION,
			AlchemyConstants.SKILL_MIN_LESSER_POISON,
			AlchemyConstants.SKILL_MAX_LESSER_POISON,
			typeof(Nightshade),
			AlchemyConstants.MSG_NIGHTSHADE,
			AlchemyConstants.RESOURCE_NIGHTSHADE_LESSER_POISON,
			AlchemyConstants.MSG_INSUFFICIENT_NIGHTSHADE);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 514); // Recipe ID 514

		// Poison Potion
		index = AddCraft(
			typeof(PoisonPotion),
			AlchemyStringConstants.GROUP_BASIC,
			AlchemyStringConstants.ITEM_POISON_POTION,
			AlchemyConstants.SKILL_MIN_POISON,
			AlchemyConstants.SKILL_MAX_POISON,
			typeof(Nightshade),
			AlchemyConstants.MSG_NIGHTSHADE,
			AlchemyConstants.RESOURCE_NIGHTSHADE_POISON,
			AlchemyConstants.MSG_INSUFFICIENT_NIGHTSHADE);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 515); // Recipe ID 515

		// Greater Poison Potion (requires Nox Crystal)
		index = AddCraft(
			typeof(GreaterPoisonPotion),
			AlchemyStringConstants.GROUP_ADVANCED,
			AlchemyStringConstants.ITEM_GREATER_POISON_POTION,
			AlchemyConstants.SKILL_MIN_GREATER_POISON,
			AlchemyConstants.SKILL_MAX_GREATER_POISON,
			typeof(Nightshade),
			AlchemyConstants.MSG_NIGHTSHADE,
			AlchemyConstants.RESOURCE_NIGHTSHADE_GREATER_POISON,
			AlchemyConstants.MSG_INSUFFICIENT_NIGHTSHADE);
		AddRes(index, typeof(NoxCrystal), AlchemyConstants.MSG_NOX_CRYSTAL,
			AlchemyConstants.RESOURCE_NOX_CRYSTAL_GREATER_POISON, AlchemyConstants.MSG_INSUFFICIENT_NOX_CRYSTAL);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 516); // Recipe ID 516

		// Deadly Poison Potion (requires Nox Crystal)
		index = AddCraft(
			typeof(DeadlyPoisonPotion),
			AlchemyStringConstants.GROUP_ADVANCED,
			AlchemyStringConstants.ITEM_DEADLY_POISON_POTION,
			AlchemyConstants.SKILL_MIN_DEADLY_POISON,
			AlchemyConstants.SKILL_MAX_DEADLY_POISON,
			typeof(Nightshade),
			AlchemyConstants.MSG_NIGHTSHADE,
			AlchemyConstants.RESOURCE_NIGHTSHADE_DEADLY_POISON,
			AlchemyConstants.MSG_INSUFFICIENT_NIGHTSHADE);
		AddRes(index, typeof(NoxCrystal), AlchemyConstants.MSG_NOX_CRYSTAL,
			AlchemyConstants.RESOURCE_NOX_CRYSTAL_DEADLY_POISON, AlchemyConstants.MSG_INSUFFICIENT_NOX_CRYSTAL);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 517); // Recipe ID 517

		// Lethal Poison Potion (requires Nox Crystal)
		index = AddCraft(
			typeof(LethalPoisonPotion),
			AlchemyStringConstants.GROUP_ADVANCED,
			AlchemyStringConstants.ITEM_LETHAL_POISON_POTION,
			AlchemyConstants.SKILL_MIN_LETHAL_POISON,
			AlchemyConstants.SKILL_MAX_LETHAL_POISON,
			typeof(Nightshade),
			AlchemyConstants.MSG_NIGHTSHADE,
			AlchemyConstants.RESOURCE_NIGHTSHADE_LETHAL_POISON,
			AlchemyConstants.MSG_INSUFFICIENT_NIGHTSHADE);
		AddRes(index, typeof(NoxCrystal), AlchemyConstants.MSG_NOX_CRYSTAL,
			AlchemyConstants.RESOURCE_NOX_CRYSTAL_LETHAL_POISON, AlchemyConstants.MSG_INSUFFICIENT_NOX_CRYSTAL);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
			AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 518); // Recipe ID 518
	}

/// <summary>
/// Adds mana potion crafts.
/// </summary>
private void AddManaPotions()
{
	int index = AddCraft(
		typeof(LesserManaPotion),
		AlchemyStringConstants.GROUP_BASIC,
		AlchemyStringConstants.ITEM_LESSER_MANA_POTION,
		AlchemyConstants.SKILL_MIN_LESSER_MANA,
		AlchemyConstants.SKILL_MAX_LESSER_MANA,
		typeof(BlackPearl),
		AlchemyConstants.MSG_BLACK_PEARL,
		AlchemyConstants.RESOURCE_BLACK_PEARL_LESSER_MANA,
		AlchemyConstants.MSG_INSUFFICIENT_BLACK_PEARL);
	AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
		AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
	AddRecipe(index, 521); // Recipe ID 521

	index = AddCraft(
		typeof(ManaPotion),
		AlchemyStringConstants.GROUP_BASIC,
		AlchemyStringConstants.ITEM_MANA_POTION,
		AlchemyConstants.SKILL_MIN_MANA,
		AlchemyConstants.SKILL_MAX_MANA,
		typeof(BlackPearl),
		AlchemyConstants.MSG_BLACK_PEARL,
		AlchemyConstants.RESOURCE_BLACK_PEARL_MANA,
		AlchemyConstants.MSG_INSUFFICIENT_BLACK_PEARL);
	AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
		AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
	AddRecipe(index, 522); // Recipe ID 522

	index = AddCraft(
		typeof(GreaterManaPotion),
		AlchemyStringConstants.GROUP_ADVANCED,
		AlchemyStringConstants.ITEM_GREATER_MANA_POTION,
		AlchemyConstants.SKILL_MIN_GREATER_MANA,
		AlchemyConstants.SKILL_MAX_GREATER_MANA,
		typeof(BlackPearl),
		AlchemyConstants.MSG_BLACK_PEARL,
		AlchemyConstants.RESOURCE_BLACK_PEARL_GREATER_MANA,
		AlchemyConstants.MSG_INSUFFICIENT_BLACK_PEARL);
	AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
		AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
	AddRecipe(index, 523); // Recipe ID 523
}

/// <summary>
/// Adds invisibility potion crafts.
/// </summary>
private void AddInvisibilityPotions()
{
	int index = AddCraft(
		typeof(LesserInvisibilityPotion),
		AlchemyStringConstants.GROUP_BASIC,
		AlchemyStringConstants.ITEM_LESSER_INVISIBILITY_POTION,
		AlchemyConstants.SKILL_MIN_LESSER_INVISIBILITY,
		AlchemyConstants.SKILL_MAX_LESSER_INVISIBILITY,
		typeof(Bloodmoss),
		AlchemyConstants.MSG_BLOODMOSS,
		AlchemyConstants.RESOURCE_BLOODMOSS_LESSER_INVISIBILITY,
		AlchemyConstants.MSG_INSUFFICIENT_BLOODMOSS);
	AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
		AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
	AddRecipe(index, 524); // Recipe ID 524

	index = AddCraft(
		typeof(InvisibilityPotion),
		AlchemyStringConstants.GROUP_BASIC,
		AlchemyStringConstants.ITEM_INVISIBILITY_POTION,
		AlchemyConstants.SKILL_MIN_INVISIBILITY,
		AlchemyConstants.SKILL_MAX_INVISIBILITY,
		typeof(Bloodmoss),
		AlchemyConstants.MSG_BLOODMOSS,
		AlchemyConstants.RESOURCE_BLOODMOSS_INVISIBILITY,
		AlchemyConstants.MSG_INSUFFICIENT_BLOODMOSS);
	AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
		AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
	AddRecipe(index, 525); // Recipe ID 525

	index = AddCraft(
		typeof(GreaterInvisibilityPotion),
		AlchemyStringConstants.GROUP_ADVANCED,
		AlchemyStringConstants.ITEM_GREATER_INVISIBILITY_POTION,
		AlchemyConstants.SKILL_MIN_GREATER_INVISIBILITY,
		AlchemyConstants.SKILL_MAX_GREATER_INVISIBILITY,
		typeof(Bloodmoss),
		AlchemyConstants.MSG_BLOODMOSS,
		AlchemyConstants.RESOURCE_BLOODMOSS_GREATER_INVISIBILITY,
		AlchemyConstants.MSG_INSUFFICIENT_BLOODMOSS);
	AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
		AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
	AddRecipe(index, 526); // Recipe ID 526
}

/// <summary>
/// Adds frostbite potion crafts.
/// </summary>
private void AddFrostbitePotions()
{
	// Frostbite Potion (requires Spider's Silk + Ginseng)
	int index = AddCraft(
		typeof(FrostbitePotion),
		AlchemyStringConstants.GROUP_SPECIAL,
		AlchemyStringConstants.ITEM_FROSTBITE_POTION,
		AlchemyConstants.SKILL_MIN_FROSTBITE,
		AlchemyConstants.SKILL_MAX_FROSTBITE,
		typeof(SpidersSilk),
		AlchemyConstants.MSG_SPIDERS_SILK,
		AlchemyConstants.RESOURCE_SPIDERS_SILK_FROSTBITE,
		AlchemyConstants.MSG_INSUFFICIENT_SPIDERS_SILK);
	AddRes(index, typeof(Ginseng), AlchemyConstants.MSG_GINSENG,
		AlchemyConstants.RESOURCE_GINSENG_FROSTBITE, AlchemyConstants.MSG_INSUFFICIENT_GINSENG);
	AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
		AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
	AddRecipe(index, 527); // Recipe ID 527

	// Greater Frostbite Potion (requires Spider's Silk + Ginseng)
	index = AddCraft(
		typeof(GreaterFrostbitePotion),
		AlchemyStringConstants.GROUP_SPECIAL,
		AlchemyStringConstants.ITEM_GREATER_FROSTBITE_POTION,
		AlchemyConstants.SKILL_MIN_GREATER_FROSTBITE,
		AlchemyConstants.SKILL_MAX_GREATER_FROSTBITE,
		typeof(SpidersSilk),
		AlchemyConstants.MSG_SPIDERS_SILK,
		AlchemyConstants.RESOURCE_SPIDERS_SILK_GREATER_FROSTBITE,
		AlchemyConstants.MSG_INSUFFICIENT_SPIDERS_SILK);
	AddRes(index, typeof(Ginseng), AlchemyConstants.MSG_GINSENG,
		AlchemyConstants.RESOURCE_GINSENG_GREATER_FROSTBITE, AlchemyConstants.MSG_INSUFFICIENT_GINSENG);
	AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
		AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
	AddRecipe(index, 528); // Recipe ID 528
}

/// <summary>
/// Adds confusion blast potion crafts.
/// </summary>
private void AddConfusionBlastPotions()
{
	// Confusion Blast Potion (requires Nightshade + Black Pearl)
	int index = AddCraft(
		typeof(ConfusionBlastPotion),
		AlchemyStringConstants.GROUP_SPECIAL,
		AlchemyStringConstants.ITEM_CONFUSION_BLAST_POTION,
		AlchemyConstants.SKILL_MIN_CONFUSION_BLAST,
		AlchemyConstants.SKILL_MAX_CONFUSION_BLAST,
		typeof(Nightshade),
		AlchemyConstants.MSG_NIGHTSHADE,
		AlchemyConstants.RESOURCE_NIGHTSHADE_CONFUSION_BLAST,
		AlchemyConstants.MSG_INSUFFICIENT_NIGHTSHADE);
	AddRes(index, typeof(BlackPearl), AlchemyConstants.MSG_BLACK_PEARL,
		AlchemyConstants.RESOURCE_BLACK_PEARL_CONFUSION_BLAST, AlchemyConstants.MSG_INSUFFICIENT_BLACK_PEARL);
	AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
		AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
	AddRecipe(index, 531); // Recipe ID 531

	// Greater Confusion Blast Potion (requires Nightshade + Black Pearl)
	index = AddCraft(
		typeof(GreaterConfusionBlastPotion),
		AlchemyStringConstants.GROUP_SPECIAL,
		AlchemyStringConstants.ITEM_GREATER_CONFUSION_BLAST_POTION,
		AlchemyConstants.SKILL_MIN_GREATER_CONFUSION_BLAST,
		AlchemyConstants.SKILL_MAX_GREATER_CONFUSION_BLAST,
		typeof(Nightshade),
		AlchemyConstants.MSG_NIGHTSHADE,
		AlchemyConstants.RESOURCE_NIGHTSHADE_GREATER_CONFUSION_BLAST,
		AlchemyConstants.MSG_INSUFFICIENT_NIGHTSHADE);
	AddRes(index, typeof(BlackPearl), AlchemyConstants.MSG_BLACK_PEARL,
		AlchemyConstants.RESOURCE_BLACK_PEARL_GREATER_CONFUSION_BLAST, AlchemyConstants.MSG_INSUFFICIENT_BLACK_PEARL);
	AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
		AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
	AddRecipe(index, 532); // Recipe ID 532
}

/// <summary>
/// Adds cosmetic potion crafts.
/// </summary>
private void AddCosmeticPotions()
{
		int index = AddCraft(
			typeof(HairOilPotion),
			AlchemyStringConstants.GROUP_COSMETIC,
			AlchemyStringConstants.ITEM_HAIR_OIL_POTION,
			AlchemyConstants.SKILL_MIN_HAIR_OIL,
			AlchemyConstants.SKILL_MAX_HAIR_OIL,
			typeof(PixieSkull),
			"pixie skull",
			AlchemyConstants.RESOURCE_PIXIE_SKULL_HAIR_OIL,
			AlchemyConstants.MSG_INSUFFICIENT_RESOURCES);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE, AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 533); // Recipe ID 533

		index = AddCraft(
			typeof(HairDyePotion),
			AlchemyStringConstants.GROUP_COSMETIC,
			AlchemyStringConstants.ITEM_HAIR_DYE_POTION,
			AlchemyConstants.SKILL_MIN_HAIR_DYE,
			AlchemyConstants.SKILL_MAX_HAIR_DYE,
			typeof(FairyEgg),
			"fairy egg",
			AlchemyConstants.RESOURCE_FAIRY_EGG_HAIR_DYE,
			AlchemyConstants.MSG_INSUFFICIENT_RESOURCES);
		AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE, AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		AddRecipe(index, 534); // Recipe ID 534
	}

		#endregion
	}
}

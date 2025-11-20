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
			AddCosmeticPotions();
		}

		/// <summary>
		/// Adds a potion craft with automatic bottle requirement.
		/// </summary>
		private void AddPotionCraft(Type potionType, string name, double minSkill, double maxSkill,
			Type reagent, TextDefinition reagentNameId, int reagentAmount, TextDefinition errorId)
		{
			int index = AddCraft(potionType, AlchemyStringConstants.GROUP_POTIONS, name,
				minSkill, maxSkill, reagent, reagentNameId, reagentAmount, errorId);
			AddRes(index, typeof(Bottle), AlchemyConstants.MSG_BOTTLE,
				AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		}

		/// <summary>
		/// Adds agility potion crafts.
		/// </summary>
		private void AddAgilityPotions()
		{
			AddPotionCraft(
				typeof(AgilityPotion),
				AlchemyStringConstants.ITEM_AGILITY_POTION,
				AlchemyConstants.SKILL_MIN_AGILITY,
				AlchemyConstants.SKILL_MAX_AGILITY,
				typeof(Bloodmoss),
				AlchemyConstants.MSG_BLOODMOSS,
				AlchemyConstants.RESOURCE_BLOODMOSS_AGILITY,
				AlchemyConstants.MSG_INSUFFICIENT_BLOODMOSS);

			AddPotionCraft(
				typeof(GreaterAgilityPotion),
				AlchemyStringConstants.ITEM_GREATER_AGILITY_POTION,
				AlchemyConstants.SKILL_MIN_GREATER_AGILITY,
				AlchemyConstants.SKILL_MAX_GREATER_AGILITY,
				typeof(Bloodmoss),
				AlchemyConstants.MSG_BLOODMOSS,
				AlchemyConstants.RESOURCE_BLOODMOSS_GREATER_AGILITY,
				AlchemyConstants.MSG_INSUFFICIENT_BLOODMOSS);
		}

		/// <summary>
		/// Adds cure potion crafts.
		/// </summary>
		private void AddCurePotions()
		{
			AddPotionCraft(
				typeof(LesserCurePotion),
				AlchemyStringConstants.ITEM_LESSER_CURE_POTION,
				AlchemyConstants.SKILL_MIN_LESSER_CURE,
				AlchemyConstants.SKILL_MAX_LESSER_CURE,
				typeof(Garlic),
				AlchemyConstants.MSG_GARLIC,
				AlchemyConstants.RESOURCE_GARLIC_LESSER_CURE,
				AlchemyConstants.MSG_INSUFFICIENT_GARLIC);

			AddPotionCraft(
				typeof(CurePotion),
				AlchemyStringConstants.ITEM_CURE_POTION,
				AlchemyConstants.SKILL_MIN_CURE,
				AlchemyConstants.SKILL_MAX_CURE,
				typeof(Garlic),
				AlchemyConstants.MSG_GARLIC,
				AlchemyConstants.RESOURCE_GARLIC_CURE,
				AlchemyConstants.MSG_INSUFFICIENT_GARLIC);

			AddPotionCraft(
				typeof(GreaterCurePotion),
				AlchemyStringConstants.ITEM_GREATER_CURE_POTION,
				AlchemyConstants.SKILL_MIN_GREATER_CURE,
				AlchemyConstants.SKILL_MAX_GREATER_CURE,
				typeof(Garlic),
				AlchemyConstants.MSG_GARLIC,
				AlchemyConstants.RESOURCE_GARLIC_GREATER_CURE,
				AlchemyConstants.MSG_INSUFFICIENT_GARLIC);
		}

		/// <summary>
		/// Adds explosion potion crafts.
		/// </summary>
		private void AddExplosionPotions()
		{
			AddPotionCraft(
				typeof(LesserExplosionPotion),
				AlchemyStringConstants.ITEM_LESSER_EXPLOSION_POTION,
				AlchemyConstants.SKILL_MIN_LESSER_EXPLOSION,
				AlchemyConstants.SKILL_MAX_LESSER_EXPLOSION,
				typeof(SulfurousAsh),
				AlchemyConstants.MSG_SULFUROUS_ASH,
				AlchemyConstants.RESOURCE_SULFUROUS_ASH_LESSER_EXPLOSION,
				AlchemyConstants.MSG_INSUFFICIENT_SULFUROUS_ASH);

			AddPotionCraft(
				typeof(ExplosionPotion),
				AlchemyStringConstants.ITEM_EXPLOSION_POTION,
				AlchemyConstants.SKILL_MIN_EXPLOSION,
				AlchemyConstants.SKILL_MAX_EXPLOSION,
				typeof(SulfurousAsh),
				AlchemyConstants.MSG_SULFUROUS_ASH,
				AlchemyConstants.RESOURCE_SULFUROUS_ASH_EXPLOSION,
				AlchemyConstants.MSG_INSUFFICIENT_SULFUROUS_ASH);

			AddPotionCraft(
				typeof(GreaterExplosionPotion),
				AlchemyStringConstants.ITEM_GREATER_EXPLOSION_POTION,
				AlchemyConstants.SKILL_MIN_GREATER_EXPLOSION,
				AlchemyConstants.SKILL_MAX_GREATER_EXPLOSION,
				typeof(SulfurousAsh),
				AlchemyConstants.MSG_SULFUROUS_ASH,
				AlchemyConstants.RESOURCE_SULFUROUS_ASH_GREATER_EXPLOSION,
				AlchemyConstants.MSG_INSUFFICIENT_SULFUROUS_ASH);
		}

		/// <summary>
		/// Adds heal potion crafts.
		/// </summary>
		private void AddHealPotions()
		{
			AddPotionCraft(
				typeof(LesserHealPotion),
				AlchemyStringConstants.ITEM_LESSER_HEAL_POTION,
				AlchemyConstants.SKILL_MIN_LESSER_HEAL,
				AlchemyConstants.SKILL_MAX_LESSER_HEAL,
				typeof(Ginseng),
				AlchemyConstants.MSG_GINSENG,
				AlchemyConstants.RESOURCE_GINSENG_LESSER_HEAL,
				AlchemyConstants.MSG_INSUFFICIENT_GINSENG);

			AddPotionCraft(
				typeof(HealPotion),
				AlchemyStringConstants.ITEM_HEAL_POTION,
				AlchemyConstants.SKILL_MIN_HEAL,
				AlchemyConstants.SKILL_MAX_HEAL,
				typeof(Ginseng),
				AlchemyConstants.MSG_GINSENG,
				AlchemyConstants.RESOURCE_GINSENG_HEAL,
				AlchemyConstants.MSG_INSUFFICIENT_GINSENG);

			AddPotionCraft(
				typeof(GreaterHealPotion),
				AlchemyStringConstants.ITEM_GREATER_HEAL_POTION,
				AlchemyConstants.SKILL_MIN_GREATER_HEAL,
				AlchemyConstants.SKILL_MAX_GREATER_HEAL,
				typeof(Ginseng),
				AlchemyConstants.MSG_GINSENG,
				AlchemyConstants.RESOURCE_GINSENG_GREATER_HEAL,
				AlchemyConstants.MSG_INSUFFICIENT_GINSENG);
		}

		/// <summary>
		/// Adds nightsight potion craft.
		/// </summary>
		private void AddNightsightPotion()
		{
			AddPotionCraft(
				typeof(NightSightPotion),
				AlchemyStringConstants.ITEM_NIGHTSIGHT_POTION,
				AlchemyConstants.SKILL_MIN_NIGHTSIGHT,
				AlchemyConstants.SKILL_MAX_NIGHTSIGHT,
				typeof(SpidersSilk),
				AlchemyConstants.MSG_SPIDERS_SILK,
				AlchemyConstants.RESOURCE_SPIDERS_SILK_NIGHTSIGHT,
				AlchemyConstants.MSG_INSUFFICIENT_SPIDERS_SILK);
		}

		/// <summary>
		/// Adds refresh potion crafts.
		/// </summary>
		private void AddRefreshPotions()
		{
			AddPotionCraft(
				typeof(RefreshPotion),
				AlchemyStringConstants.ITEM_REFRESH_POTION,
				AlchemyConstants.SKILL_MIN_REFRESH,
				AlchemyConstants.SKILL_MAX_REFRESH,
				typeof(BlackPearl),
				AlchemyConstants.MSG_BLACK_PEARL,
				AlchemyConstants.RESOURCE_BLACK_PEARL_REFRESH,
				AlchemyConstants.MSG_INSUFFICIENT_BLACK_PEARL);

			AddPotionCraft(
				typeof(TotalRefreshPotion),
				AlchemyStringConstants.ITEM_TOTAL_REFRESH_POTION,
				AlchemyConstants.SKILL_MIN_TOTAL_REFRESH,
				AlchemyConstants.SKILL_MAX_TOTAL_REFRESH,
				typeof(BlackPearl),
				AlchemyConstants.MSG_BLACK_PEARL,
				AlchemyConstants.RESOURCE_BLACK_PEARL_TOTAL_REFRESH,
				AlchemyConstants.MSG_INSUFFICIENT_BLACK_PEARL);
		}

		/// <summary>
		/// Adds strength potion crafts.
		/// </summary>
		private void AddStrengthPotions()
		{
			AddPotionCraft(
				typeof(StrengthPotion),
				AlchemyStringConstants.ITEM_STRENGTH_POTION,
				AlchemyConstants.SKILL_MIN_STRENGTH,
				AlchemyConstants.SKILL_MAX_STRENGTH,
				typeof(MandrakeRoot),
				AlchemyConstants.MSG_MANDRAKE_ROOT,
				AlchemyConstants.RESOURCE_MANDRAKE_ROOT_STRENGTH,
				AlchemyConstants.MSG_INSUFFICIENT_MANDRAKE_ROOT);

			AddPotionCraft(
				typeof(GreaterStrengthPotion),
				AlchemyStringConstants.ITEM_GREATER_STRENGTH_POTION,
				AlchemyConstants.SKILL_MIN_GREATER_STRENGTH,
				AlchemyConstants.SKILL_MAX_GREATER_STRENGTH,
				typeof(MandrakeRoot),
				AlchemyConstants.MSG_MANDRAKE_ROOT,
				AlchemyConstants.RESOURCE_MANDRAKE_ROOT_GREATER_STRENGTH,
				AlchemyConstants.MSG_INSUFFICIENT_MANDRAKE_ROOT);
		}

		/// <summary>
		/// Adds poison potion crafts.
		/// </summary>
		private void AddPoisonPotions()
		{
			// Lesser Poison Potion
			AddPotionCraft(
				typeof(LesserPoisonPotion),
				AlchemyStringConstants.ITEM_LESSER_POISON_POTION,
				AlchemyConstants.SKILL_MIN_LESSER_POISON,
				AlchemyConstants.SKILL_MAX_LESSER_POISON,
				typeof(Nightshade),
				AlchemyConstants.MSG_NIGHTSHADE,
				AlchemyConstants.RESOURCE_NIGHTSHADE_LESSER_POISON,
				AlchemyConstants.MSG_INSUFFICIENT_NIGHTSHADE);

			// Poison Potion
			AddPotionCraft(
				typeof(PoisonPotion),
				AlchemyStringConstants.ITEM_POISON_POTION,
				AlchemyConstants.SKILL_MIN_POISON,
				AlchemyConstants.SKILL_MAX_POISON,
				typeof(Nightshade),
				AlchemyConstants.MSG_NIGHTSHADE,
				AlchemyConstants.RESOURCE_NIGHTSHADE_POISON,
				AlchemyConstants.MSG_INSUFFICIENT_NIGHTSHADE);

			// Greater Poison Potion (requires Nox Crystal)
			int index = AddCraft(
				typeof(GreaterPoisonPotion),
				AlchemyStringConstants.GROUP_POTIONS,
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

			// Deadly Poison Potion (requires Nox Crystal)
			index = AddCraft(
				typeof(DeadlyPoisonPotion),
				AlchemyStringConstants.GROUP_POTIONS,
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

			// Lethal Poison Potion (requires Nox Crystal)
			index = AddCraft(
				typeof(LethalPoisonPotion),
				AlchemyStringConstants.GROUP_POTIONS,
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
			AddRes(index, typeof(Bottle), "empty bottle", AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);

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
			AddRes(index, typeof(Bottle), "empty bottle", AlchemyConstants.RESOURCE_BOTTLES_POTION, AlchemyConstants.MSG_INSUFFICIENT_BOTTLES);
		}

		#endregion
	}
}

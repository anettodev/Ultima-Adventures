using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Glassblowing crafting system for creating glass items like bottles, flasks, and vials.
	/// Requires Alchemy skill and proximity to a forge.
	/// </summary>
	public class DefGlassblowing : CraftSystem
	{
		/// <summary>
		/// Gets the main skill required for glassblowing
		/// </summary>
		public override SkillName MainSkill
		{
			get{ return SkillName.Alchemy; }
		}

		public override int GumpTitleNumber
		{
			get{ return GlassblowingConstants.GUMP_TITLE; } // <CENTER>Glassblowing MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefGlassblowing();

				return m_CraftSystem;
			}
		}

		/// <summary>
		/// Gets the crafting chance at minimum skill level
		/// </summary>
		/// <param name="item">The craft item</param>
		/// <returns>Always returns 0.0 for glassblowing</returns>
		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefGlassblowing() : base( GlassblowingConstants.CRAFT_SYSTEM_MIN_SKILL, GlassblowingConstants.CRAFT_SYSTEM_MAX_SKILL, GlassblowingConstants.CRAFT_SYSTEM_DELAY )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			int toolCheck = ValidateTool(tool, from);
			if (toolCheck != 0)
				return toolCheck;

			int skillCheck = ValidateGlassblowingSkill(from);
			if (skillCheck != 0)
				return skillCheck;

			int forgeCheck = ValidateForgeProximity(from);
			if (forgeCheck != 0)
				return forgeCheck;

			return 0;
		}

		/// <summary>
		/// Validates that the tool is usable for crafting
		/// </summary>
		/// <param name="tool">The tool to validate</param>
		/// <param name="from">The mobile using the tool</param>
		/// <returns>0 if valid, otherwise the error message cliloc</returns>
		private int ValidateTool(BaseTool tool, Mobile from)
		{
			if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
				return GlassblowingConstants.MSG_TOOL_WORN_OUT;

			if (!BaseTool.CheckTool(tool, from))
				return GlassblowingConstants.MSG_WRONG_TOOL_EQUIPPED;

			if (!BaseTool.CheckAccessible(tool, from))
				return GlassblowingConstants.MSG_TOOL_NOT_ACCESSIBLE;

			return 0;
		}

		/// <summary>
		/// Validates that the mobile has learned glassblowing
		/// </summary>
		/// <param name="from">The mobile to validate</param>
		/// <returns>0 if valid, otherwise the error message cliloc</returns>
		private int ValidateGlassblowingSkill(Mobile from)
		{
			if (!(from is PlayerMobile && ((PlayerMobile)from).Glassblowing && from.Skills[SkillName.Alchemy].Base >= GlassblowingConstants.REQUIRED_ALCHEMY_SKILL))
				return GlassblowingConstants.MSG_NOT_LEARNED_GLASSBLOWING;

			return 0;
		}

		/// <summary>
		/// Validates that the mobile is near a forge
		/// </summary>
		/// <param name="from">The mobile to validate</param>
		/// <returns>0 if valid, otherwise the error message cliloc</returns>
		private int ValidateForgeProximity(Mobile from)
		{
			bool anvil, forge;
			DefBlacksmithy.CheckAnvilAndForge(from, GlassblowingConstants.FORGE_CHECK_DISTANCE, out anvil, out forge);

			if (forge)
				return 0;

			return GlassblowingConstants.MSG_MUST_BE_NEAR_FORGE;
		}

		/// <summary>
		/// Plays the crafting effect sounds for glassblowing
		/// </summary>
		/// <param name="from">The mobile performing the craft</param>
		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( GlassblowingConstants.SOUND_BELLOWS ); // bellows

			// Animation code removed - not needed for glassblowing
			// Timer code removed - not needed for glassblowing
		}

		/// <summary>
		/// Timer to synchronize the anvil hit sound with the animation
		/// </summary>
		private class InternalTimer : Timer
		{
			private Mobile m_From;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( GlassblowingConstants.TIMER_DELAY_SECONDS ) )
			{
				m_From = from;
			}

			protected override void OnTick()
			{
				m_From.PlaySound( GlassblowingConstants.SOUND_ANVIL_HIT );
			}
		}

		/// <summary>
		/// Plays ending effects and returns appropriate messages based on craft result
		/// </summary>
		/// <param name="from">The mobile performing the craft</param>
		/// <param name="failed">Whether the craft failed</param>
		/// <param name="lostMaterial">Whether materials were lost on failure</param>
		/// <param name="toolBroken">Whether the tool broke</param>
		/// <param name="quality">The quality level of the crafted item</param>
		/// <param name="makersMark">Whether maker's mark was applied</param>
		/// <param name="item">The crafted item</param>
		/// <returns>The cliloc ID of the message to display</returns>
		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( GlassblowingConstants.MSG_TOOL_WORN_OUT );

			if ( failed )
			{
				if ( lostMaterial )
					return GlassblowingConstants.MSG_FAILED_MATERIAL_LOSS;
				else
					return GlassblowingConstants.MSG_FAILED_NO_MATERIAL_LOSS;
			}
			else
			{
				from.PlaySound( GlassblowingConstants.SOUND_GLASS_BREAKING );

				if ( quality == 0 )
					return GlassblowingConstants.MSG_BELOW_AVERAGE;
				else if ( makersMark && quality == 2 )
					return GlassblowingConstants.MSG_EXCEPTIONAL_WITH_MARK;
				else if ( quality == 2 )
					return GlassblowingConstants.MSG_EXCEPTIONAL;
				else
					return GlassblowingConstants.MSG_NORMAL;
			}
		}

		/// <summary>
		/// Initializes the craft list with all available glassblowing recipes
		/// </summary>
		public override void InitCraftList()
		{
			AddBasicGlassItems();
			AddFlasks();
			AddAdvancedItems();
		}

		/// <summary>
		/// Adds basic glass items (bottle, jar)
		/// </summary>
		private void AddBasicGlassItems()
		{
			int index = AddCraft( typeof( Bottle ), 1044050, GlassblowingConstants.ITEM_BOTTLE,
				GlassblowingConstants.SKILL_MIN_BASIC, GlassblowingConstants.SKILL_MAX_BASIC,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_BASIC, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( Jar ), 1044050, GlassblowingStringConstants.ITEM_JAR,
				GlassblowingConstants.SKILL_MIN_BASIC, GlassblowingConstants.SKILL_MAX_BASIC,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_BASIC, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );
			SetUseAllRes( index, true );
		}

		/// <summary>
		/// Adds various flask types
		/// </summary>
		private void AddFlasks()
		{
			AddCraft( typeof( Monocle ), 1044050, GlassblowingStringConstants.ITEM_MONOCLE,
				GlassblowingConstants.SKILL_MIN_MONOCLE, GlassblowingConstants.SKILL_MAX_MONOCLE,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_BASIC, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );

			AddCraft( typeof( SmallFlask ), 1044050, GlassblowingConstants.ITEM_SMALL_FLASK,
				GlassblowingConstants.SKILL_MIN_SMALL_MEDIUM_FLASK, GlassblowingConstants.SKILL_MAX_SMALL_MEDIUM_FLASK,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_SMALL_FLASK, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );

			AddCraft( typeof( MediumFlask ), 1044050, GlassblowingConstants.ITEM_MEDIUM_FLASK,
				GlassblowingConstants.SKILL_MIN_SMALL_MEDIUM_FLASK, GlassblowingConstants.SKILL_MAX_SMALL_MEDIUM_FLASK,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_MEDIUM_FLASK, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );

			AddCraft( typeof( CurvedFlask ), 1044050, GlassblowingConstants.ITEM_CURVED_FLASK,
				GlassblowingConstants.SKILL_MIN_CURVED_FLASK, GlassblowingConstants.SKILL_MAX_CURVED_FLASK,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_CURVED_FLASK, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );

			AddCraft( typeof( LongFlask ), 1044050, GlassblowingConstants.ITEM_LONG_FLASK,
				GlassblowingConstants.SKILL_MIN_LONG_FLASK, GlassblowingConstants.SKILL_MAX_LONG_FLASK,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_LONG_FLASK, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );

			AddCraft( typeof( LargeFlask ), 1044050, GlassblowingConstants.ITEM_LARGE_FLASK,
				GlassblowingConstants.SKILL_MIN_ADVANCED_FLASK, GlassblowingConstants.SKILL_MAX_ADVANCED_FLASK,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_LARGE_FLASK, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );

			AddCraft( typeof( AniSmallBlueFlask ), 1044050, GlassblowingConstants.ITEM_ANI_SMALL_BLUE_FLASK,
				GlassblowingConstants.SKILL_MIN_ADVANCED_FLASK, GlassblowingConstants.SKILL_MAX_ADVANCED_FLASK,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_LARGE_FLASK, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );

			AddCraft( typeof( AniLargeVioletFlask ), 1044050, GlassblowingConstants.ITEM_ANI_LARGE_VIOLET_FLASK,
				GlassblowingConstants.SKILL_MIN_ADVANCED_FLASK, GlassblowingConstants.SKILL_MAX_ADVANCED_FLASK,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_LARGE_FLASK, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );

			AddCraft( typeof( Jug ), 1044050, GlassblowingStringConstants.ITEM_JUG,
				GlassblowingConstants.SKILL_MIN_ADVANCED_FLASK, GlassblowingConstants.SKILL_MAX_ADVANCED_FLASK,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_LARGE_FLASK, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );

			AddCraft( typeof( BeverageBottle ), 1044050, GlassblowingStringConstants.ITEM_BEVERAGE_BOTTLE,
				GlassblowingConstants.SKILL_MIN_ADVANCED_FLASK, GlassblowingConstants.SKILL_MAX_ADVANCED_FLASK,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_LARGE_FLASK, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );

			AddCraft( typeof( AniRedRibbedFlask ), 1044050, GlassblowingConstants.ITEM_ANI_RED_RIBBED_FLASK,
				GlassblowingConstants.SKILL_MIN_ADVANCED_FLASK, GlassblowingConstants.SKILL_MAX_ADVANCED_FLASK,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_RED_RIBBED_FLASK, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );
		}

		/// <summary>
		/// Adds advanced glass items (vials, hourglass)
		/// </summary>
		private void AddAdvancedItems()
		{
			AddCraft( typeof( EmptyVialsWRack ), 1044050, GlassblowingConstants.ITEM_EMPTY_VIALS_RACK,
				GlassblowingConstants.SKILL_MIN_VIALS_HOURGLASS, GlassblowingConstants.SKILL_MAX_VIALS_HOURGLASS,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_EMPTY_VIALS, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );

			AddCraft( typeof( FullVialsWRack ), 1044050, GlassblowingConstants.ITEM_FULL_VIALS_RACK,
				GlassblowingConstants.SKILL_MIN_VIALS_HOURGLASS, GlassblowingConstants.SKILL_MAX_VIALS_HOURGLASS,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_FULL_VIALS, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );

			AddCraft( typeof( SpinningHourglass ), 1044050, GlassblowingConstants.ITEM_SPINNING_HOURGLASS,
				GlassblowingConstants.SKILL_MIN_SPINNING_HOURGLASS, GlassblowingConstants.SKILL_MAX_SPINNING_HOURGLASS,
				typeof( Sand ), GlassblowingConstants.RESOURCE_SAND,
				GlassblowingConstants.SAND_AMOUNT_SPINNING_HOURGLASS, GlassblowingConstants.RESOURCE_SAND_NOT_FOUND );
		}
	}
}

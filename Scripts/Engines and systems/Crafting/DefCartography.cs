using System;
using Server.Items;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Cartography crafting system for creating maps, charts, and scrolls.
	/// Requires Cartography skill and blank maps/bark fragments as resources.
	/// </summary>
	public class DefCartography : CraftSystem
	{
		/// <summary>
		/// Gets the main skill required for cartography
		/// </summary>
		public override SkillName MainSkill
		{
			get	{ return SkillName.Cartography; }
		}

		public override int GumpTitleNumber
		{
			get { return CartographyConstants.GUMP_TITLE; } // <CENTER>CARTOGRAPHY MENU</CENTER>
		}

		/// <summary>
		/// Gets the crafting chance at minimum skill level
		/// </summary>
		/// <param name="item">The craft item</param>
		/// <returns>Always returns 0.0 for cartography</returns>
		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefCartography();

				return m_CraftSystem;
			}
		}

		private DefCartography() : base( CartographyConstants.CRAFT_SYSTEM_MIN_SKILL, CartographyConstants.CRAFT_SYSTEM_MAX_SKILL, CartographyConstants.CRAFT_SYSTEM_DELAY )
		{
		}

		/// <summary>
		/// Validates if the mobile can perform cartography crafting
		/// </summary>
		/// <param name="from">The mobile attempting to craft</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="itemType">The type of item being crafted</param>
		/// <returns>0 if crafting is allowed, otherwise the error message cliloc</returns>
		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			int toolCheck = ValidateTool(tool, from);
			if (toolCheck != 0)
				return toolCheck;

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
				return CartographyConstants.MSG_TOOL_WORN_OUT;

			if (!BaseTool.CheckAccessible(tool, from))
				return CartographyConstants.MSG_TOOL_NOT_ACCESSIBLE;

			return 0;
		}

		/// <summary>
		/// Plays the crafting effect sounds for cartography
		/// </summary>
		/// <param name="from">The mobile performing the craft</param>
		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( CartographyConstants.SOUND_CRAFTING );
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
				from.SendLocalizedMessage( CartographyConstants.MSG_TOOL_WORN_OUT );

			if ( failed )
			{
				if ( lostMaterial )
					return CartographyConstants.MSG_FAILED_MATERIAL_LOSS;
				else
					return CartographyConstants.MSG_FAILED_NO_MATERIAL_LOSS;
			}
			else
			{
				if ( quality == 0 )
					return CartographyConstants.MSG_BELOW_AVERAGE;
				else if ( makersMark && quality == 2 )
					return CartographyConstants.MSG_EXCEPTIONAL_WITH_MARK;
				else if ( quality == 2 )
					return CartographyConstants.MSG_EXCEPTIONAL;
				else
					return CartographyConstants.MSG_NORMAL;
			}
		}

		/// <summary>
		/// Initializes the craft list with all available cartography recipes
		/// </summary>
		public override void InitCraftList()
		{
			AddBlankScrolls();
			AddMaps();
		}

		/// <summary>
		/// Adds blank scroll crafting recipes
		/// </summary>
		private void AddBlankScrolls()
		{
			int index = AddCraft( typeof( BlankScroll ), CartographyConstants.CATEGORY_BLANK_SCROLLS,
				CartographyConstants.ITEM_BLANK_SCROLL,
				CartographyConstants.SKILL_MIN_BLANK_SCROLL, CartographyConstants.SKILL_MAX_BLANK_SCROLL,
				typeof( BarkFragment ), CartographyConstants.RESOURCE_BARK_FRAGMENT,
				CartographyConstants.BARK_FRAGMENT_AMOUNT, CartographyConstants.RESOURCE_BARK_FRAGMENT_NOT_FOUND );
			SetUseAllRes( index, true );
		}

		/// <summary>
		/// Adds map crafting recipes
		/// </summary>
		private void AddMaps()
		{
			// Maps use blank maps as their resource
			AddCraft( typeof( LocalMap ), CartographyConstants.CATEGORY_MAPS, CartographyStringConstants.ITEM_SMALL_MAP,
				CartographyConstants.SKILL_MIN_SMALL_MAP, CartographyConstants.SKILL_MAX_SMALL_MAP,
				typeof( BlankMap ), CartographyConstants.RESOURCE_BLANK_MAP,
				CartographyConstants.BLANK_MAP_AMOUNT, CartographyConstants.RESOURCE_BLANK_MAP_NOT_FOUND );

			AddCraft( typeof( CityMap ), CartographyConstants.CATEGORY_MAPS, CartographyStringConstants.ITEM_LARGE_MAP,
				CartographyConstants.SKILL_MIN_LARGE_MAP, CartographyConstants.SKILL_MAX_LARGE_MAP,
				typeof( BlankMap ), CartographyConstants.RESOURCE_BLANK_MAP,
				CartographyConstants.BLANK_MAP_AMOUNT, CartographyConstants.RESOURCE_BLANK_MAP_NOT_FOUND );

			AddCraft( typeof( SeaChart ), CartographyConstants.CATEGORY_MAPS, CartographyStringConstants.ITEM_SEA_CHART,
				CartographyConstants.SKILL_MIN_SEA_CHART, CartographyConstants.SKILL_MAX_SEA_CHART,
				typeof( BlankMap ), CartographyConstants.RESOURCE_BLANK_MAP,
				CartographyConstants.BLANK_MAP_AMOUNT, CartographyConstants.RESOURCE_BLANK_MAP_NOT_FOUND );

			AddCraft( typeof( WorldMap ), CartographyConstants.CATEGORY_MAPS, CartographyStringConstants.ITEM_HUGE_MAP,
				CartographyConstants.SKILL_MIN_HUGE_MAP, CartographyConstants.SKILL_MAX_HUGE_MAP,
				typeof( BlankMap ), CartographyConstants.RESOURCE_BLANK_MAP,
				CartographyConstants.BLANK_MAP_AMOUNT, CartographyConstants.RESOURCE_BLANK_MAP_NOT_FOUND );

			AddCraft( typeof( MapWorld ), CartographyConstants.CATEGORY_MAPS, CartographyStringConstants.ITEM_WORLD_MAP,
				CartographyConstants.SKILL_MIN_WORLD_MAP, CartographyConstants.SKILL_MAX_WORLD_MAP,
				typeof( BlankMap ), CartographyConstants.RESOURCE_BLANK_MAP,
				CartographyConstants.BLANK_MAP_AMOUNT, CartographyConstants.RESOURCE_BLANK_MAP_NOT_FOUND );
		}
	}
}
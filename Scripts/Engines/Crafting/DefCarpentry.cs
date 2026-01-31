using System;
using Server.Items;
using Server.Mobiles;
namespace Server.Engines.Craft
{
	/// <summary>
	/// Carpentry crafting system for creating furniture, containers, and wooden items from boards
	/// </summary>
	public class DefCarpentry : CraftSystem
	{
		#region Fields

		private static CraftSystem m_CraftSystem;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the main skill required for carpentry
		/// </summary>
		public override SkillName MainSkill
		{
			get	{ return SkillName.Carpentry;	}
		}

		/// <summary>
		/// Gets the gump title number for the carpentry menu
		/// </summary>
		public override int GumpTitleNumber
		{
			get { return CarpentryConstants.MSG_GUMP_TITLE; }
		}

		/// <summary>
		/// Gets the singleton instance of the carpentry craft system
		/// </summary>
		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefCarpentry();

				return m_CraftSystem;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DefCarpentry class
		/// </summary>
		private DefCarpentry() : base( CarpentryConstants.MIN_CRAFT_EFFECT, CarpentryConstants.MIN_CRAFT_EFFECT, CarpentryConstants.CRAFT_DELAY )
		{
		}

		#endregion

		#region Craft System Overrides

		/// <summary>
		/// Gets the chance of success at minimum skill level
		/// </summary>
		/// <param name="item">The craft item</param>
		/// <returns>50% chance at minimum skill</returns>
		public override double GetChanceAtMin( CraftItem item )
		{
			return CarpentryConstants.CHANCE_AT_MIN;
		}

		/// <summary>
		/// Checks if the player can craft the specified item with the given tool
		/// </summary>
		/// <param name="from">The player attempting to craft</param>
		/// <param name="tool">The crafting tool</param>
		/// <param name="itemType">The type of item to craft</param>
		/// <returns>0 if successful, error message number otherwise</returns>
		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return CarpentryConstants.MSG_TOOL_WORN_OUT;
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return CarpentryConstants.MSG_TOOL_MUST_BE_ON_PERSON;

			return 0;
		}

		/// <summary>
		/// Plays the crafting effect (sound)
		/// </summary>
		/// <param name="from">The player crafting</param>
		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( CarpentryConstants.SOUND_CRAFT_EFFECT );
		}

		/// <summary>
		/// Plays the ending effect and returns the appropriate message
		/// </summary>
		/// <param name="from">The player crafting</param>
		/// <param name="failed">Whether the craft failed</param>
		/// <param name="lostMaterial">Whether materials were lost</param>
		/// <param name="toolBroken">Whether the tool broke</param>
		/// <param name="quality">Quality level (0=low, 1=normal, 2=exceptional)</param>
		/// <param name="makersMark">Whether maker's mark was applied</param>
		/// <param name="item">The craft item</param>
		/// <returns>Message number to display</returns>
		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( CarpentryConstants.MSG_TOOL_WORN_OUT );

			if ( failed )
			{
				return lostMaterial ? CarpentryConstants.MSG_FAILED_LOST_MATERIALS : CarpentryConstants.MSG_FAILED_NO_MATERIALS_LOST;
			}

			if ( quality == 0 )
				return CarpentryConstants.MSG_BARELY_MADE_ITEM;

			if ( makersMark && quality == 2 )
				return CarpentryConstants.MSG_EXCEPTIONAL_WITH_MARK;

			if ( quality == 2 )
				return CarpentryConstants.MSG_EXCEPTIONAL_QUALITY;

			return CarpentryConstants.MSG_ITEM_CREATED;
		}

		#endregion

		#region Craft Item Initialization

		/// <summary>
		/// Initializes the list of craftable items
		/// </summary>
		public override void InitCraftList()
		{
			int index = -1;

            //index =	AddCraft( typeof( Board ),			1044294, 1027127,	0.0,   0.0,	typeof( BaseLog ), 1044466,  1, CarpentryStringConstants.ERROR_INSUFFICIENT_LOGS );
            //SetUseAllRes( index, true );

            #region Armários (Armoires)

            AddCraft(typeof(NewArmoireG), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_ARMOIRE_SLATS, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredArmoireB), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_ARMOIRE_NICE, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredArmoireA), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_DISPLAY_CABINET, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_FINISHED_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(FancyArmoire), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_WARDROBE, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(Armoire), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_CLOSET, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredCabinetB), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_KITCHEN_CABINET, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_FINISHED_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredCabinetF), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_FANCY_CABINET_SMALL, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CHAIR_FANCY, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredCabinetM), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_FANCY_CABINET_MEDIUM, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredCabinetE), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_SIMPLE_CABINET_SMALL, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CHAIR_FANCY, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredCabinetC), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_SIMPLE_CABINET_MEDIUM, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_FINISHED_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredCabinetG), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_SHORT_CABINET, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CHAIR_FANCY, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredCabinetD), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_BOOK_CABINET_NARROW, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_FINISHED_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredCabinetA), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_BOOK_CABINET_LARGE, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_FINISHED_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredCabinetN), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_STORAGE_CABINET_SMALL, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredCabinetH), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_STORAGE_CABINET_LARGE, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredCabinetK), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_TALL_CABINET_SIMPLE, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ColoredCabinetL), CarpentryStringConstants.GROUP_ARMOIRES, CarpentryStringConstants.ITEM_TALL_CABINET_FANCY, CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            /*AddCraft( typeof( NewArmoireA ), 		"Arm�rios", "bamboo armoire", CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof( Board ), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ARMOIRE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD );
			AddCraft( typeof( NewArmoireB ), 		"Arm�rios", "bamboo armoire", CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof( Board ), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ARMOIRE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD );
			AddCraft( typeof( NewArmoireC ), 		"Arm�rios", "bamboo armoire", CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof( Board ), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ARMOIRE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD );*/
            /*AddCraft( typeof( NewArmoireD ), 		"Arm�rios", "armoire", CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof( Board ), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ARMOIRE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD );*/
            /*AddCraft( typeof( NewArmoireE ),		"Arm�rios", "empty armoire", CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof( Board ), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ARMOIRE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD );
			AddCraft( typeof( NewArmoireF ), 		"Arm�rios", "open armoire", CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof( Board ), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ARMOIRE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD );*/
            /*AddCraft( typeof( NewArmoireH ), 		"Arm�rios", "empty armoire", CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof( Board ), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ARMOIRE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD );
			AddCraft( typeof( NewArmoireI ), 		"Arm�rios", "open armoire", CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof( Board ), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ARMOIRE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD );
			AddCraft( typeof( NewArmoireJ ), 		"Arm�rios", "open armoire", CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof( Board ), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ARMOIRE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD );*/
            /*AddCraft( typeof( ColoredCabinetI ), "Gabinetes", "tall fancy cabinet*", CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof( Board ), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ARMOIRE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD );
			AddCraft( typeof( ColoredCabinetJ ), "Gabinetes", "tall medium cabinet*", CarpentryConstants.SKILL_REQ_ARMOIRES_MIN, CarpentryConstants.SKILL_REQ_ARMOIRES_MAX, typeof( Board ), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ARMOIRE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD );*/

            #endregion

            #region Baús & Caixas (Chests and Boxes)

            AddCraft(typeof(SmallCrate), CarpentryStringConstants.GROUP_CHESTS_AND_BOXES, CarpentryStringConstants.ITEM_SIMPLE_CRATE_SMALL, CarpentryConstants.SKILL_REQ_CRATE_SMALL_MIN, CarpentryConstants.SKILL_REQ_CRATE_SMALL_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CRATE_SMALL, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(MediumCrate), CarpentryStringConstants.GROUP_CHESTS_AND_BOXES, CarpentryStringConstants.ITEM_SIMPLE_CRATE_MEDIUM, CarpentryConstants.SKILL_REQ_CRATE_MEDIUM_MIN, CarpentryConstants.SKILL_REQ_CRATE_MEDIUM_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CRATE_MEDIUM, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(LargeCrate), CarpentryStringConstants.GROUP_CHESTS_AND_BOXES, CarpentryStringConstants.ITEM_SIMPLE_CRATE_LARGE, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CRATE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(WoodenBox), CarpentryStringConstants.GROUP_CHESTS_AND_BOXES, CarpentryStringConstants.ITEM_WOODEN_CHEST_SMALL, CarpentryConstants.SKILL_REQ_WOODEN_BOX_MIN, CarpentryConstants.SKILL_REQ_WOODEN_BOX_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_BOX, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(WoodenChest), CarpentryStringConstants.GROUP_CHESTS_AND_BOXES, CarpentryStringConstants.ITEM_WOODEN_CHEST_MEDIUM, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MIN, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(FinishedWoodenChest), CarpentryStringConstants.GROUP_CHESTS_AND_BOXES, CarpentryStringConstants.ITEM_WOODEN_CHEST_LARGE, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MIN, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_FINISHED_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(GildedWoodenChest), CarpentryStringConstants.GROUP_CHESTS_AND_BOXES, CarpentryStringConstants.ITEM_WOODEN_CHEST_ROYAL, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MIN, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_FINISHED_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(WoodenFootLocker), CarpentryStringConstants.GROUP_CHESTS_AND_BOXES, CarpentryStringConstants.ITEM_WOODEN_FOOT_LOCKER, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MIN, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_FINISHED_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            index = AddCraft(typeof(WoodenCoffin), CarpentryStringConstants.GROUP_CHESTS_AND_BOXES, CarpentryStringConstants.ITEM_SIMPLE_COFFIN, CarpentryConstants.SKILL_REQ_COFFIN_SIMPLE_MIN, CarpentryConstants.SKILL_REQ_COFFIN_SIMPLE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_COFFIN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Forensics, 60.0, 70.0);
            index = AddCraft(typeof(WoodenCasket), CarpentryStringConstants.GROUP_CHESTS_AND_BOXES, CarpentryStringConstants.ITEM_ELEGANT_COFFIN, CarpentryConstants.SKILL_REQ_COFFIN_ELEGANT_MIN, CarpentryConstants.SKILL_REQ_COFFIN_ELEGANT_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_COFFIN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Forensics, 70.0, 80.0);

            #endregion

            #region Caixas Especiais (Specialty Crates - Commented)

            /*AddCraft(typeof(AdventurerCrate), "Caixas", "Caixa de Aventureiro", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(AlchemyCrate), "Caixas", "Caixa de Alquimista", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ArmsCrate), "Caixas", "Caixa de Armeiro", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(BakerCrate), "Caixas", "Caixa de Padeiro", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(BeekeeperCrate), "Caixas", "Caixa de Apicultor", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(BlacksmithCrate), "Caixas", "Caixa de Ferreiro", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(BowyerCrate), "Caixas", "Caixa de Arqueiro", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ButcherCrate), "Caixas", CarpentryStringConstants.ITEM_BUTCHER_CRATE, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(CarpenterCrate), "Caixas", "Caixa de Carpinteiro", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(FletcherCrate), "Caixas", "Caixa de Flecheiro", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(HealerCrate), "Caixas", "Caixa de Curandeiro", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(JewelerCrate), "Caixas", "Caixa de Joalheiro", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(LibrarianCrate), "Caixas", CarpentryStringConstants.ITEM_LIBRARIAN_CRATE, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(MusicianCrate), "Caixas", CarpentryStringConstants.ITEM_MUSICIAN_CRATE, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(SailorCrate), "Caixas", "Caixa de Marinheiro", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(TailorCrate), "Caixas", "Caixa de Alfaiate", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(TinkerCrate), "Caixas", "Caixa de Inventor", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(WizardryCrate), "Caixas", "Caixa de Mago", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);*/

            /*AddCraft(typeof(HugeCrate), "Caixas", "huge Caixa de ", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);*/
            /*AddCraft(typeof(NecromancerCrate), "Caixas", "necromancer Caixa de ", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);*/
            /*AddCraft(typeof(ProvisionerCrate), "Caixas", "provisioner Caixa de ", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);*/
            /*AddCraft(typeof(StableCrate), "Caixas", "stable Caixa de ", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(SupplyCrate), "Caixas", "supply Caixa de ", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);*/
            /*AddCraft(typeof(TavernCrate), "Caixas", "tavern Caixa de ", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);*/
            /*AddCraft(typeof(TreasureCrate), "Caixas", "treasure Caixa de ", CarpentryConstants.SKILL_REQ_CRATE_LARGE_MIN, CarpentryConstants.SKILL_REQ_CRATE_LARGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);*/

            #endregion

            #region Cômodas & Gaveteiros (Dressers and Drawers)

            AddCraft(typeof(TallCabinet), CarpentryStringConstants.GROUP_DRESSERS_AND_DRAWERS, CarpentryStringConstants.ITEM_TALL_CABINET, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MIN, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ShortCabinet), CarpentryStringConstants.GROUP_DRESSERS_AND_DRAWERS, CarpentryStringConstants.ITEM_SHORT_CABINET_DRESSER, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MIN, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(MapleArmoire), CarpentryStringConstants.GROUP_DRESSERS_AND_DRAWERS, CarpentryStringConstants.ITEM_SIMPLE_DRESSER, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MIN, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_COFFIN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(CherryArmoire), CarpentryStringConstants.GROUP_DRESSERS_AND_DRAWERS, CarpentryStringConstants.ITEM_LARGE_DRESSER, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MIN, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_COFFIN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(OrnateWoodenChest), CarpentryStringConstants.GROUP_DRESSERS_AND_DRAWERS, CarpentryStringConstants.ITEM_ROYAL_DRESSER, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MIN, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_FINISHED_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(RedArmoire), CarpentryStringConstants.GROUP_DRESSERS_AND_DRAWERS, CarpentryStringConstants.ITEM_SIMPLE_DRAWER, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MIN, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_COFFIN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(ElegantArmoire), CarpentryStringConstants.GROUP_DRESSERS_AND_DRAWERS, CarpentryStringConstants.ITEM_ELEGANT_DRAWER, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MIN, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_COFFIN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(PlainWoodenChest), CarpentryStringConstants.GROUP_DRESSERS_AND_DRAWERS, CarpentryStringConstants.ITEM_ROYAL_DRAWER, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MIN, CarpentryConstants.SKILL_REQ_FINISHED_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_FINISHED_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            #endregion

            #region Estantes (Shelves)

            AddCraft(typeof(NewShelfA), CarpentryStringConstants.GROUP_SHELVES, CarpentryStringConstants.ITEM_BAMBOO_SHELF_SMALL, CarpentryConstants.SKILL_REQ_SHELVES_MIN, CarpentryConstants.SKILL_REQ_SHELVES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(NewShelfB), CarpentryStringConstants.GROUP_SHELVES, CarpentryStringConstants.ITEM_BAMBOO_SHELF_LARGE, CarpentryConstants.SKILL_REQ_SHELVES_MIN, CarpentryConstants.SKILL_REQ_SHELVES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(NewShelfE), CarpentryStringConstants.GROUP_SHELVES, CarpentryStringConstants.ITEM_RUSTIC_SHELF_SMALL, CarpentryConstants.SKILL_REQ_SHELVES_MIN, CarpentryConstants.SKILL_REQ_SHELVES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(NewShelfF), CarpentryStringConstants.GROUP_SHELVES, CarpentryStringConstants.ITEM_RUSTIC_SHELF_LARGE, CarpentryConstants.SKILL_REQ_SHELVES_MIN, CarpentryConstants.SKILL_REQ_SHELVES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(NewShelfD), CarpentryStringConstants.GROUP_SHELVES, CarpentryStringConstants.ITEM_SOLID_SHELF_SMALL, CarpentryConstants.SKILL_REQ_SHELVES_MIN, CarpentryConstants.SKILL_REQ_SHELVES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHELF_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(EmptyBookcase), CarpentryStringConstants.GROUP_SHELVES, CarpentryStringConstants.ITEM_SOLID_SHELF_LARGE, CarpentryConstants.SKILL_REQ_SHELVES_MIN, CarpentryConstants.SKILL_REQ_SHELVES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CHAIR_FANCY, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            #endregion

            #region Mesas & Assentos (Tables and Seats)

            AddCraft(typeof(FootStool), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_FOOT_STOOL, CarpentryConstants.SKILL_REQ_FOOT_STOOL_MIN, CarpentryConstants.SKILL_REQ_FOOT_STOOL_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SEAT_SIMPLE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(Stool), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_STOOL, CarpentryConstants.SKILL_REQ_STOOL_MIN, CarpentryConstants.SKILL_REQ_STOOL_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SEAT_SIMPLE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(WoodenBench), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_RUSTIC_BENCH, CarpentryConstants.SKILL_REQ_BENCH_MIN, CarpentryConstants.SKILL_REQ_BENCH_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BENCH, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(BambooChair), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_BAMBOO_CHAIR, CarpentryConstants.SKILL_REQ_BAMBOO_CHAIR_MIN, CarpentryConstants.SKILL_REQ_BAMBOO_CHAIR_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CHAIR, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(WoodenChair), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_SIMPLE_CHAIR, CarpentryConstants.SKILL_REQ_WOODEN_CHAIR_MIN, CarpentryConstants.SKILL_REQ_WOODEN_CHAIR_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CHAIR, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(WoodenChairCushion), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_NORMAL_CHAIR, CarpentryConstants.SKILL_REQ_CUSHIONED_CHAIR_MIN, CarpentryConstants.SKILL_REQ_CUSHIONED_CHAIR_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CHAIR, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(FancyWoodenChairCushion), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_FANCY_CHAIR, CarpentryConstants.SKILL_REQ_FANCY_CHAIR_MIN, CarpentryConstants.SKILL_REQ_FANCY_CHAIR_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CRATE_MEDIUM, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(Throne), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_SIMPLE_THRONE, CarpentryConstants.SKILL_REQ_THRONE_SIMPLE_MIN, CarpentryConstants.SKILL_REQ_THRONE_SIMPLE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_THRONE_SIMPLE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(WoodenThrone), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_ELEGANT_THRONE, CarpentryConstants.SKILL_REQ_THRONE_ELEGANT_MIN, CarpentryConstants.SKILL_REQ_THRONE_ELEGANT_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BENCH, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            index = AddCraft(typeof(OrnateElvenChair), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_ELVEN_THRONE, CarpentryConstants.SKILL_REQ_THRONE_ELVEN_MIN, CarpentryConstants.SKILL_REQ_THRONE_ELVEN_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_ELVEN_BOARDS, CarpentryConstants.RESOURCE_BOARDS_THRONE_ELVEN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            AddCraft(typeof(Nightstand), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_NIGHTSTAND, CarpentryConstants.SKILL_REQ_NIGHTSTAND_MIN, CarpentryConstants.SKILL_REQ_NIGHTSTAND_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BENCH, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(PlainLargeTable), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_LARGE_TABLE, CarpentryConstants.SKILL_REQ_PLAIN_TABLE_MIN, CarpentryConstants.SKILL_REQ_PLAIN_TABLE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_TABLE_PLAIN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(WritingTable), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_WRITING_TABLE, CarpentryConstants.SKILL_REQ_WRITING_TABLE_MIN, CarpentryConstants.SKILL_REQ_WRITING_TABLE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(YewWoodTable), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_SIMPLE_TABLE, CarpentryConstants.SKILL_REQ_YEW_TABLE_MIN, CarpentryConstants.SKILL_REQ_YEW_TABLE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_TABLE_PLAIN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(LargeTable), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_GRAND_TABLE, CarpentryConstants.SKILL_REQ_LARGE_TABLE_MIN, CarpentryConstants.SKILL_REQ_LARGE_TABLE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_TABLE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            index = AddCraft(typeof(PlainLowTable), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_SIMPLE_LOW_TABLE, CarpentryConstants.SKILL_REQ_LOW_TABLE_SIMPLE_MIN, CarpentryConstants.SKILL_REQ_LOW_TABLE_SIMPLE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_ELVEN_BOARDS, CarpentryConstants.RESOURCE_BOARDS_TABLE_LOW_SIMPLE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            index = AddCraft(typeof(ElegantLowTable), CarpentryStringConstants.GROUP_TABLES_AND_SEATS, CarpentryStringConstants.ITEM_ELEGANT_LOW_TABLE, CarpentryConstants.SKILL_REQ_LOW_TABLE_ELEGANT_MIN, CarpentryConstants.SKILL_REQ_LOW_TABLE_ELEGANT_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_ELVEN_BOARDS, CarpentryConstants.RESOURCE_BOARDS_TABLE_LOW_ELEGANT, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            #endregion

            #region Instrumentos Musicais (Musical Instruments)

            AddCraft(typeof(ShortMusicStand), CarpentryStringConstants.GROUP_MUSICAL_INSTRUMENTS, CarpentryStringConstants.ITEM_SHORT_MUSIC_STAND, CarpentryConstants.SKILL_REQ_MUSIC_STAND_SHORT_MIN, CarpentryConstants.SKILL_REQ_MUSIC_STAND_SHORT_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CRATE_MEDIUM, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(TallMusicStand), CarpentryStringConstants.GROUP_MUSICAL_INSTRUMENTS, CarpentryStringConstants.ITEM_TALL_MUSIC_STAND, CarpentryConstants.SKILL_REQ_MUSIC_STAND_TALL_MIN, CarpentryConstants.SKILL_REQ_MUSIC_STAND_TALL_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_MUSIC_STAND_TALL, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            index = AddCraft(typeof(BambooFlute), CarpentryStringConstants.GROUP_MUSICAL_INSTRUMENTS, CarpentryStringConstants.ITEM_BAMBOO_FLUTE, CarpentryConstants.SKILL_REQ_FLUTE_MIN, CarpentryConstants.SKILL_REQ_FLUTE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CRATE_MEDIUM, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Musicianship, 30.0, 45.0);

            index = AddCraft(typeof(Drums), CarpentryStringConstants.GROUP_MUSICAL_INSTRUMENTS, CarpentryStringConstants.ITEM_DRUMS, CarpentryConstants.SKILL_REQ_DRUMS_MIN, CarpentryConstants.SKILL_REQ_DRUMS_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_TABLE_PLAIN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Musicianship, 45.0, 50.0);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, 10, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            index = AddCraft(typeof(Tambourine), CarpentryStringConstants.GROUP_MUSICAL_INSTRUMENTS, CarpentryStringConstants.ITEM_TAMBOURINE, CarpentryConstants.SKILL_REQ_TAMBOURINE_MIN, CarpentryConstants.SKILL_REQ_TAMBOURINE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_TAMBOURINE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Musicianship, 50.0, 55.0);
            AddRes(index, typeof(Leather), CarpentryStringConstants.RESOURCE_LEATHER, 5, CarpentryStringConstants.ERROR_INSUFFICIENT_LEATHER);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 4, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            index = AddCraft(typeof(TambourineTassel), CarpentryStringConstants.GROUP_MUSICAL_INSTRUMENTS, CarpentryStringConstants.ITEM_TAMBOURINE_TASSEL, CarpentryConstants.SKILL_REQ_TAMBOURINE_TASSEL_MIN, CarpentryConstants.SKILL_REQ_TAMBOURINE_TASSEL_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_TAMBOURINE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Musicianship, 55.0, 60.0);
            AddRes(index, typeof(Leather), CarpentryStringConstants.RESOURCE_LEATHER, 5, CarpentryStringConstants.ERROR_INSUFFICIENT_LEATHER);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 4, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, 4, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            index = AddCraft(typeof(LapHarp), CarpentryStringConstants.GROUP_MUSICAL_INSTRUMENTS, CarpentryStringConstants.ITEM_LAP_HARP, CarpentryConstants.SKILL_REQ_LAP_HARP_MIN, CarpentryConstants.SKILL_REQ_LAP_HARP_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_MUSIC_STAND_TALL, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Musicianship, 60.0, 65.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 5, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            index = AddCraft(typeof(Harp), CarpentryStringConstants.GROUP_MUSICAL_INSTRUMENTS, CarpentryStringConstants.ITEM_HARP, CarpentryConstants.SKILL_REQ_HARP_MIN, CarpentryConstants.SKILL_REQ_HARP_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_FINISHED_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Musicianship, 65.0, 70.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 10, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            index = AddCraft(typeof(Lute), CarpentryStringConstants.GROUP_MUSICAL_INSTRUMENTS, CarpentryStringConstants.ITEM_LUTE, CarpentryConstants.SKILL_REQ_LUTE_MIN, CarpentryConstants.SKILL_REQ_LUTE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_LUTE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Musicianship, 75.0, 80.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 7, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            index = AddCraft(typeof(Pipes), CarpentryStringConstants.GROUP_MUSICAL_INSTRUMENTS, CarpentryStringConstants.ITEM_PIPES, CarpentryConstants.SKILL_REQ_PIPES_MIN, CarpentryConstants.SKILL_REQ_PIPES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_PIPES, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Musicianship, 80.0, 90.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 6, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            index = AddCraft(typeof(Fiddle), CarpentryStringConstants.GROUP_MUSICAL_INSTRUMENTS, CarpentryStringConstants.ITEM_FIDDLE, CarpentryConstants.SKILL_REQ_FIDDLE_MIN, CarpentryConstants.SKILL_REQ_FIDDLE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_FIDDLE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Musicianship, 90.0, 100.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 8, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);
            AddRes(index, typeof(Shaft), CarpentryStringConstants.RESOURCE_SHAFTS, 2, CarpentryStringConstants.ERROR_INSUFFICIENT_SHAFTS);

            #endregion

            #region Armas & Escudos (Weapons and Shields)

            AddCraft(typeof(ShepherdsCrook), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryStringConstants.ITEM_SHEPHERDS_CROOK, CarpentryConstants.SKILL_REQ_SHEPHERDS_CROOK_MIN, CarpentryConstants.SKILL_REQ_SHEPHERDS_CROOK_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHEPHERDS_CROOK, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            index = AddCraft(typeof(WildStaff), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryStringConstants.ITEM_WILD_STAFF, CarpentryConstants.SKILL_REQ_WILD_STAFF_MIN, CarpentryConstants.SKILL_REQ_WILD_STAFF_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CRATE_SMALL, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddRes(index, typeof(Feather), CarpentryStringConstants.RESOURCE_FEATHERS, 4, CarpentryStringConstants.ERROR_INSUFFICIENT_FEATHERS);

            AddCraft(typeof(QuarterStaff), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryStringConstants.ITEM_QUARTER_STAFF, CarpentryConstants.SKILL_REQ_QUARTER_STAFF_MIN, CarpentryConstants.SKILL_REQ_QUARTER_STAFF_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CRATE_SMALL, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(GnarledStaff), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryStringConstants.ITEM_GNARLED_STAFF, CarpentryConstants.SKILL_REQ_GNARLED_STAFF_MIN, CarpentryConstants.SKILL_REQ_GNARLED_STAFF_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHEPHERDS_CROOK, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(WoodenShield), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryStringConstants.ITEM_WOODEN_SHIELD, CarpentryConstants.SKILL_REQ_WOODEN_SHIELD_MIN, CarpentryConstants.SKILL_REQ_WOODEN_SHIELD_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_SHIELD, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            index = AddCraft(typeof(Bokuto), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryConstants.MSG_BOKUTO, CarpentryConstants.SKILL_REQ_BOKUTO_MIN, CarpentryConstants.SKILL_REQ_BOKUTO_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SEAT_SIMPLE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, 4, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);
            index = AddCraft(typeof(Fukiya), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryConstants.MSG_FUKIYA, CarpentryConstants.SKILL_REQ_FUKIYA_MIN, CarpentryConstants.SKILL_REQ_FUKIYA_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SEAT_SIMPLE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 2, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);
            index = AddCraft(typeof(Tetsubo), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryConstants.MSG_TETSUBO, CarpentryConstants.SKILL_REQ_TETSUBO_MIN, CarpentryConstants.SKILL_REQ_TETSUBO_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CHAIR, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 5, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            #endregion

            #region Armaduras (Armor)

            // Armor
            index = AddCraft(typeof(WoodenPlateArms), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryStringConstants.ITEM_WOODEN_PLATE_ARMS, CarpentryConstants.SKILL_REQ_WOODEN_ARMOR_ARMS_MIN, CarpentryConstants.SKILL_REQ_WOODEN_ARMOR_ARMS_MAX, typeof(ReaperOil), CarpentryStringConstants.RESOURCE_REAPER_OIL, 2, CarpentryStringConstants.ERROR_INSUFFICIENT_RESOURCES);
            AddRes(index, typeof(MysticalTreeSap), CarpentryStringConstants.RESOURCE_MYSTICAL_TREE_SAP, 2, CarpentryStringConstants.ERROR_INSUFFICIENT_RESOURCES);
            AddRes(index, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CRATE_LARGE, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            index = AddCraft(typeof(WoodenPlateHelm), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryStringConstants.ITEM_WOODEN_PLATE_HELM, CarpentryConstants.SKILL_REQ_WOODEN_ARMOR_HELM_MIN, CarpentryConstants.SKILL_REQ_WOODEN_ARMOR_HELM_MAX, typeof(ReaperOil), CarpentryStringConstants.RESOURCE_REAPER_OIL, 1, CarpentryStringConstants.ERROR_INSUFFICIENT_RESOURCES);
            AddRes(index, typeof(MysticalTreeSap), CarpentryStringConstants.RESOURCE_MYSTICAL_TREE_SAP, 1, CarpentryStringConstants.ERROR_INSUFFICIENT_RESOURCES);
            AddRes(index, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CRATE_MEDIUM, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            index = AddCraft(typeof(WoodenPlateGloves), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryStringConstants.ITEM_WOODEN_PLATE_GLOVES, CarpentryConstants.SKILL_REQ_WOODEN_ARMOR_GLOVES_MIN, CarpentryConstants.SKILL_REQ_WOODEN_ARMOR_GLOVES_MAX, typeof(ReaperOil), CarpentryStringConstants.RESOURCE_REAPER_OIL, 1, CarpentryStringConstants.ERROR_INSUFFICIENT_RESOURCES);
            AddRes(index, typeof(MysticalTreeSap), CarpentryStringConstants.RESOURCE_MYSTICAL_TREE_SAP, 1, CarpentryStringConstants.ERROR_INSUFFICIENT_RESOURCES);
            AddRes(index, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_SHIELD, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            index = AddCraft(typeof(WoodenPlateGorget), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryStringConstants.ITEM_WOODEN_PLATE_GORGET, CarpentryConstants.SKILL_REQ_WOODEN_ARMOR_GORGET_MIN, CarpentryConstants.SKILL_REQ_WOODEN_ARMOR_GORGET_MAX, typeof(ReaperOil), CarpentryStringConstants.RESOURCE_REAPER_OIL, 1, CarpentryStringConstants.ERROR_INSUFFICIENT_RESOURCES);
            AddRes(index, typeof(MysticalTreeSap), CarpentryStringConstants.RESOURCE_MYSTICAL_TREE_SAP, 1, CarpentryStringConstants.ERROR_INSUFFICIENT_RESOURCES);
            AddRes(index, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_BOX, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            index = AddCraft(typeof(WoodenPlateLegs), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryStringConstants.ITEM_WOODEN_PLATE_LEGS, CarpentryConstants.SKILL_REQ_WOODEN_ARMOR_LEGS_MIN, CarpentryConstants.SKILL_REQ_WOODEN_ARMOR_LEGS_MAX, typeof(ReaperOil), CarpentryStringConstants.RESOURCE_REAPER_OIL, 3, CarpentryStringConstants.ERROR_INSUFFICIENT_RESOURCES);
            AddRes(index, typeof(MysticalTreeSap), CarpentryStringConstants.RESOURCE_MYSTICAL_TREE_SAP, 3, CarpentryStringConstants.ERROR_INSUFFICIENT_RESOURCES);
            AddRes(index, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            index = AddCraft(typeof(WoodenPlateChest), CarpentryConstants.MSG_WEAPONS_AND_SHIELDS, CarpentryStringConstants.ITEM_WOODEN_PLATE_CHEST, CarpentryConstants.SKILL_REQ_WOODEN_ARMOR_CHEST_MIN, CarpentryConstants.SKILL_REQ_WOODEN_ARMOR_CHEST_MAX, typeof(ReaperOil), CarpentryStringConstants.RESOURCE_REAPER_OIL, 3, CarpentryStringConstants.ERROR_INSUFFICIENT_RESOURCES);
            AddRes(index, typeof(MysticalTreeSap), CarpentryStringConstants.RESOURCE_MYSTICAL_TREE_SAP, 3, CarpentryStringConstants.ERROR_INSUFFICIENT_RESOURCES);
            AddRes(index, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CHAIR_FANCY, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            #endregion

            #region Addons - Ferraria (Blacksmithy)

            // Blacksmithy
            index = AddCraft(typeof(SmallForgeDeed), 1044290, CarpentryConstants.MSG_ADDON_SMALL_FORGE, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MIN, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BARREL_STAVES, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Blacksmith, 75.0, 80.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 75, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            index = AddCraft(typeof(LargeForgeEastDeed), 1044290, CarpentryConstants.MSG_ADDON_LARGE_FORGE_EAST, CarpentryConstants.SKILL_REQ_LARGE_FORGE_MIN, CarpentryConstants.SKILL_REQ_LARGE_FORGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BARREL_STAVES, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Blacksmith, 80.0, 85.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 100, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            index = AddCraft(typeof(LargeForgeSouthDeed), 1044290, CarpentryConstants.MSG_ADDON_LARGE_FORGE_SOUTH, CarpentryConstants.SKILL_REQ_LARGE_FORGE_MIN, CarpentryConstants.SKILL_REQ_LARGE_FORGE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BARREL_STAVES, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Blacksmith, 80.0, 85.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 100, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            index = AddCraft(typeof(AnvilEastDeed), 1044290, CarpentryConstants.MSG_ADDON_ANVIL_EAST, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MIN, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BARREL_STAVES, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Blacksmith, 75.0, 80.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 150, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            index = AddCraft(typeof(AnvilSouthDeed), 1044290, CarpentryConstants.MSG_ADDON_ANVIL_SOUTH, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MIN, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BARREL_STAVES, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Blacksmith, 75.0, 80.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 150, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            #endregion

            #region Addons - Treinamento (Training)

            // Training
            index = AddCraft(typeof(TrainingDummyEastDeed), 1044290/*1044297*/, CarpentryConstants.MSG_ADDON_TRAINING_DUMMY_EAST, CarpentryConstants.SKILL_REQ_TRAINING_DUMMY_MIN, CarpentryConstants.SKILL_REQ_TRAINING_DUMMY_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_TRAINING_DUMMY, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tailoring, 50.0, 55.0);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, 60, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            index = AddCraft(typeof(TrainingDummySouthDeed), 1044290, CarpentryConstants.MSG_ADDON_TRAINING_DUMMY_SOUTH, CarpentryConstants.SKILL_REQ_TRAINING_DUMMY_MIN, CarpentryConstants.SKILL_REQ_TRAINING_DUMMY_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_TRAINING_DUMMY, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tailoring, 50.0, 55.0);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, 60, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            index = AddCraft(typeof(PickpocketDipEastDeed), 1044290, CarpentryConstants.MSG_ADDON_PICKPOCKET_DIP_EAST, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MIN, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_PICKPOCKET_DIP, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tailoring, 50.0, 55.0);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, 60, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            index = AddCraft(typeof(PickpocketDipSouthDeed), 1044290, CarpentryConstants.MSG_ADDON_PICKPOCKET_DIP_SOUTH, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MIN, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_PICKPOCKET_DIP, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tailoring, 50.0, 55.0);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, 60, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            #endregion

            #region Addons - Alfaiataria (Tailoring)

            // Tailoring
            index = AddCraft(typeof(Dressform), 1044290, CarpentryConstants.MSG_ADDON_DRESSFORM, CarpentryConstants.SKILL_REQ_DRESSFORM_MIN, CarpentryConstants.SKILL_REQ_DRESSFORM_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_CHAIR_FANCY, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tailoring, 65.0, 70.0);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, 10, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            index = AddCraft(typeof(SpinningwheelEastDeed), 1044290, CarpentryConstants.MSG_ADDON_SPINNINGWHEEL_EAST, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MIN, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_SPINNINGWHEEL, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tailoring, 65.0, 70.0);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, CarpentryConstants.RESOURCE_CLOTH_ADDON_SPINNINGWHEEL, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            index = AddCraft(typeof(SpinningwheelSouthDeed), 1044290, CarpentryConstants.MSG_ADDON_SPINNINGWHEEL_SOUTH, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MIN, CarpentryConstants.SKILL_REQ_WOODEN_CHEST_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_SPINNINGWHEEL, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tailoring, 65.0, 70.0);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, CarpentryConstants.RESOURCE_CLOTH_ADDON_SPINNINGWHEEL, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            index = AddCraft(typeof(LoomEastDeed), 1044290, CarpentryConstants.MSG_ADDON_LOOM_EAST, CarpentryConstants.SKILL_REQ_LOOM_MIN, CarpentryConstants.SKILL_REQ_LOOM_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_LOOM, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tailoring, 65.0, 70.0);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, CarpentryConstants.RESOURCE_CLOTH_ADDON_SPINNINGWHEEL, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            index = AddCraft(typeof(LoomSouthDeed), 1044290, CarpentryConstants.MSG_ADDON_LOOM_SOUTH, CarpentryConstants.SKILL_REQ_LOOM_MIN, CarpentryConstants.SKILL_REQ_LOOM_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_LOOM, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tailoring, 65.0, 70.0);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, CarpentryConstants.RESOURCE_CLOTH_ADDON_SPINNINGWHEEL, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            #endregion

            #region Addons - Culinária (Cooking)

            // Cooking
            index = AddCraft(typeof(StoneOvenEastDeed), 1044290, CarpentryConstants.MSG_ADDON_STONE_OVEN_EAST, CarpentryConstants.SKILL_REQ_TRAINING_DUMMY_MIN, CarpentryConstants.SKILL_REQ_TRAINING_DUMMY_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_STONE_OVEN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tinkering, 50.0, 55.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 125, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            index = AddCraft(typeof(StoneOvenSouthDeed), 1044290, CarpentryConstants.MSG_ADDON_STONE_OVEN_SOUTH, CarpentryConstants.SKILL_REQ_TRAINING_DUMMY_MIN, CarpentryConstants.SKILL_REQ_TRAINING_DUMMY_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_STONE_OVEN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tinkering, 50.0, 55.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 125, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            index = AddCraft(typeof(FlourMillEastDeed), 1044290, CarpentryConstants.MSG_ADDON_FLOUR_MILL_EAST, CarpentryConstants.SKILL_REQ_FLOUR_MILL_MIN, CarpentryConstants.SKILL_REQ_FLOUR_MILL_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_FLOUR_MILL, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tinkering, 50.0, 55.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 50, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            index = AddCraft(typeof(FlourMillSouthDeed), 1044290, CarpentryConstants.MSG_ADDON_FLOUR_MILL_SOUTH, CarpentryConstants.SKILL_REQ_FLOUR_MILL_MIN, CarpentryConstants.SKILL_REQ_FLOUR_MILL_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_FLOUR_MILL, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tinkering, 50.0, 55.0);
            AddRes(index, typeof(IronIngot), CarpentryStringConstants.RESOURCE_IRON_INGOTS, 50, CarpentryStringConstants.ERROR_INSUFFICIENT_METAL);

            AddCraft(typeof(WaterTroughEastDeed), 1044290, CarpentryConstants.MSG_ADDON_WATER_TROUGH_EAST, CarpentryConstants.SKILL_REQ_FLOUR_MILL_MIN, CarpentryConstants.SKILL_REQ_FLOUR_MILL_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_WATER_TROUGH, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(WaterTroughSouthDeed), 1044290, CarpentryConstants.MSG_ADDON_WATER_TROUGH_SOUTH, CarpentryConstants.SKILL_REQ_FLOUR_MILL_MIN, CarpentryConstants.SKILL_REQ_FLOUR_MILL_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_ADDON_WATER_TROUGH, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            #endregion

            #region Addons - Diversos (Miscellaneous)

            // GENERAL MISC
            AddCraft(typeof(DartBoardSouthDeed), 1044290, CarpentryConstants.MSG_ADDON_DART_BOARD_SOUTH, CarpentryConstants.SKILL_REQ_DART_BOARD_MIN, CarpentryConstants.SKILL_REQ_DART_BOARD_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BARREL_STAVES, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(DartBoardEastDeed), 1044290, CarpentryConstants.MSG_ADDON_DART_BOARD_EAST, CarpentryConstants.SKILL_REQ_DART_BOARD_MIN, CarpentryConstants.SKILL_REQ_DART_BOARD_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BARREL_STAVES, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            #endregion

            #region Outros & Variados (Other & Miscellaneous)

            // Outros & Variados
            AddCraft(typeof(Kindling), CarpentryStringConstants.GROUP_MISC, CarpentryStringConstants.ITEM_KINDLING, CarpentryConstants.SKILL_REQ_KINDLING_MIN, CarpentryConstants.SKILL_REQ_KINDLING_MAX, typeof(Log), CarpentryStringConstants.RESOURCE_BOARDS, 1, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            index = AddCraft(typeof(Kindling), CarpentryStringConstants.GROUP_MISC, CarpentryStringConstants.ITEM_KINDLING_BATCH, CarpentryConstants.SKILL_REQ_KINDLING_BATCH_MIN, CarpentryConstants.SKILL_REQ_KINDLING_BATCH_MAX, typeof(Log), CarpentryStringConstants.RESOURCE_BOARDS, 1, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            SetUseAllRes(index, true);

            index = AddCraft(typeof(BarkFragment), CarpentryStringConstants.GROUP_MISC, CarpentryStringConstants.ITEM_BARK_FRAGMENT, CarpentryConstants.SKILL_REQ_BARK_FRAGMENT_MIN, CarpentryConstants.SKILL_REQ_BARK_FRAGMENT_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BARK_FRAGMENT, CarpentryStringConstants.ERROR_INSUFFICIENT_LOGS);
            SetUseAllRes(index, true);

            AddCraft(typeof(BarrelStaves), CarpentryStringConstants.GROUP_MISC, CarpentryConstants.MSG_BARREL_STAVES_ITEM, CarpentryConstants.SKILL_REQ_BARREL_STAVES_MIN, CarpentryConstants.SKILL_REQ_BARREL_STAVES_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BARREL_STAVES, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddCraft(typeof(BarrelLid), CarpentryStringConstants.GROUP_MISC, CarpentryConstants.MSG_BARREL_LID_ITEM, CarpentryConstants.SKILL_REQ_BARREL_LID_MIN, CarpentryConstants.SKILL_REQ_BARREL_LID_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BARREL_LID, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            AddCraft(typeof(MixingSpoon), CarpentryStringConstants.GROUP_MISC, CarpentryStringConstants.ITEM_MIXING_SPOON, CarpentryConstants.SKILL_REQ_MIXING_SPOON_MIN, CarpentryConstants.SKILL_REQ_MIXING_SPOON_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BARREL_STAVES, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            index = AddCraft(typeof(Keg), CarpentryStringConstants.GROUP_MISC, CarpentryConstants.MSG_KEG, CarpentryConstants.SKILL_REQ_KEG_MIN, CarpentryConstants.SKILL_REQ_KEG_MAX, typeof(BarrelStaves), CarpentryStringConstants.RESOURCE_BARREL_STAVES, 4, CarpentryStringConstants.ERROR_INSUFFICIENT_COMPONENTS);
            AddRes(index, typeof(BarrelHoops), CarpentryStringConstants.RESOURCE_BARREL_HOOPS, 1, CarpentryStringConstants.ERROR_INSUFFICIENT_COMPONENTS);
            AddRes(index, typeof(BarrelLid), CarpentryStringConstants.RESOURCE_BARREL_LID, 2, CarpentryStringConstants.ERROR_INSUFFICIENT_COMPONENTS);
            AddRes(index, typeof(BarrelTap), CarpentryStringConstants.RESOURCE_BARREL_TAP, 1, CarpentryStringConstants.ERROR_INSUFFICIENT_COMPONENTS);

            index = AddCraft(typeof(AlchemyTub), CarpentryStringConstants.GROUP_MISC, CarpentryStringConstants.ITEM_ALCHEMY_TUB, CarpentryConstants.SKILL_REQ_ALCHEMY_TUB_MIN, CarpentryConstants.SKILL_REQ_ALCHEMY_TUB_MAX, typeof(BarrelStaves), CarpentryStringConstants.RESOURCE_BARREL_STAVES, 4, CarpentryStringConstants.ERROR_INSUFFICIENT_COMPONENTS);
            AddRes(index, typeof(BarrelHoops), CarpentryStringConstants.RESOURCE_BARREL_HOOPS, 1, CarpentryStringConstants.ERROR_INSUFFICIENT_COMPONENTS);
            AddRes(index, typeof(BarrelLid), CarpentryStringConstants.RESOURCE_BARREL_LID, 1, CarpentryStringConstants.ERROR_INSUFFICIENT_COMPONENTS);

            index = AddCraft(typeof(FishingPole), CarpentryStringConstants.GROUP_MISC, CarpentryConstants.MSG_FISHING_POLE, CarpentryConstants.SKILL_REQ_FISHING_POLE_MIN, CarpentryConstants.SKILL_REQ_FISHING_POLE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BARREL_STAVES, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tailoring, 40.0, 45.0);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, 5, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            index = AddCraft(typeof(ShojiScreen), CarpentryStringConstants.GROUP_MISC, CarpentryConstants.MSG_SHOJI_SCREEN, CarpentryConstants.SKILL_REQ_SHOJI_SCREEN_MIN, CarpentryConstants.SKILL_REQ_SHOJI_SCREEN_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_SHOJI_SCREEN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tailoring, 50.0, 55.0);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, 60, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            index = AddCraft(typeof(BambooScreen), CarpentryStringConstants.GROUP_MISC, CarpentryConstants.MSG_BAMBOO_SCREEN, CarpentryConstants.SKILL_REQ_BAMBOO_SCREEN_MIN, CarpentryConstants.SKILL_REQ_BAMBOO_SCREEN_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_BAMBOO_SCREEN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddSkill(index, SkillName.Tailoring, 50.0, 55.0);
            AddRes(index, typeof(Cloth), CarpentryStringConstants.RESOURCE_CLOTH, 60, CarpentryStringConstants.ERROR_INSUFFICIENT_CLOTH);

            AddCraft(typeof(Easle), CarpentryStringConstants.GROUP_MISC, CarpentryConstants.MSG_EASLE, CarpentryConstants.SKILL_REQ_FLUTE_MIN, CarpentryConstants.SKILL_REQ_FLUTE_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_WOODEN_CHEST, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);

            index = AddCraft(typeof(WhiteHangingLantern), CarpentryStringConstants.GROUP_MISC, CarpentryConstants.MSG_WHITE_HANGING_LANTERN, CarpentryConstants.SKILL_REQ_SHOJI_SCREEN_MIN, CarpentryConstants.SKILL_REQ_SHOJI_SCREEN_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_LANTERN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddRes(index, typeof(BlankScroll), CarpentryStringConstants.RESOURCE_BLANK_SCROLLS, 10, CarpentryStringConstants.ERROR_INSUFFICIENT_BLANK_SCROLLS);

            index = AddCraft(typeof(RedHangingLantern), CarpentryStringConstants.GROUP_MISC, CarpentryConstants.MSG_RED_HANGING_LANTERN, CarpentryConstants.SKILL_REQ_BAMBOO_SCREEN_MIN, CarpentryConstants.SKILL_REQ_BAMBOO_SCREEN_MAX, typeof(Board), CarpentryStringConstants.RESOURCE_BOARDS, CarpentryConstants.RESOURCE_BOARDS_LANTERN, CarpentryStringConstants.ERROR_INSUFFICIENT_WOOD);
            AddRes(index, typeof(BlankScroll), CarpentryStringConstants.RESOURCE_BLANK_SCROLLS, 10, CarpentryStringConstants.ERROR_INSUFFICIENT_BLANK_SCROLLS);

            #endregion

            #region Configuração do Sistema (System Configuration)

            Repair = true;
			MarkOption = true;
			CanEnhance = Core.AOS;

		SetSubRes( typeof( Board ), "Madeira Comum" );

			// Add every material you want the player to be able to choose from
			// This will override the overridable material	TODO: Verify the required skill amount
		AddSubRes( typeof( Board ), "Madeira Comum", 00.0, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD );
		AddSubRes( typeof( AshBoard ), "Carvalho Cinza", 60.0, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD );
		AddSubRes( typeof( EbonyBoard ), "Ébano", 70.0, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD );
        AddSubRes(typeof(ElvenBoard), "Élfica", 80.0, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD);
        AddSubRes(typeof(GoldenOakBoard), "Ipê-Amarelo", 85.0, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD);
        AddSubRes(typeof(CherryBoard), "Cerejeira", 90.0, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD);
        AddSubRes(typeof(RosewoodBoard), "Pau-Brasil", 95.0, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD);
        AddSubRes(typeof(HickoryBoard), "Nogueira Branca", 100.0, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD);

            /*AddSubRes( typeof( MahoganyBoard ), 1095384, 90.0, CarpentryStringConstants.RESOURCE_BOARDS, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD );
			AddSubRes( typeof( OakBoard ), 1095385, 95.0, CarpentryStringConstants.RESOURCE_BOARDS, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD );
			AddSubRes( typeof( PineBoard ), 1095386, 100.0, CarpentryStringConstants.RESOURCE_BOARDS, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD );*/

            /*AddSubRes( typeof( WalnutBoard ), 1095388, 100.0, CarpentryStringConstants.RESOURCE_BOARDS, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD );
			AddSubRes( typeof( DriftwoodBoard ), 1095409, 105.0, CarpentryStringConstants.RESOURCE_BOARDS, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD );
			AddSubRes( typeof( GhostBoard ), 1095511, 110.0, CarpentryStringConstants.RESOURCE_BOARDS, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD );
			AddSubRes( typeof( PetrifiedBoard ), 1095532, 115.0, CarpentryStringConstants.RESOURCE_BOARDS, CarpentryStringConstants.ERROR_CANNOT_WORK_WOOD );*/

            #endregion

            #endregion

        }
	}
}

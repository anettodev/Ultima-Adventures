using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Targeting;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Tinkering crafting system definition
	/// </summary>
	public class DefTinkering : CraftSystem
	{
		#region Properties and Singleton

		public override SkillName MainSkill
		{
			get	{ return SkillName.Tinkering; }
		}

		public override int GumpTitleNumber
		{
			get { return TinkeringConstants.MSG_GUMP_TITLE; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTinkering();

				return m_CraftSystem;
			}
		}

		#endregion

		#region Constructor

		private DefTinkering() : base(
			TinkeringConstants.MIN_CRAFT_EFFECT,
			TinkeringConstants.MAX_CRAFT_EFFECT,
			TinkeringConstants.CRAFT_DELAY_MULTIPLIER )
		{
		}

		#endregion

		#region Core CraftSystem Methods

		/// <summary>
		/// Gets the chance at minimum skill for the specified item
		/// </summary>
		public override double GetChanceAtMin( CraftItem item )
		{
			string itemName = item.NameString;
			if ( itemName == TinkeringConstants.MSG_POTION_KEG ||
			     itemName == "kit de remoção de armadilha de facção" ) // faction trap removal kit
				return TinkeringConstants.CHANCE_AT_MIN_SPECIAL;

			// Eternal Key (Chave Eterna) has 20% chance at minimum skill (90.0)
			if ( item.ItemType == typeof( Server.Items.EternalKey ) )
				return 0.20;

			return TinkeringConstants.CHANCE_AT_MIN_STANDARD;
		}

		/// <summary>
		/// Validates if the mobile can craft with the given tool
		/// </summary>
		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
			{
				from.SendMessage( 55, TinkeringConstants.MSG_TOOL_WORN_OUT );
				return 500295; // Generic error number
			}
			else if ( !BaseTool.CheckAccessible( tool, from ) )
			{
				from.SendMessage( 55, TinkeringConstants.MSG_TOOL_MUST_BE_ON_PERSON );
				return 500295; // Generic error number
			}

			return 0;
		}

		/// <summary>
		/// Plays the crafting sound effect
		/// </summary>
		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( TinkeringConstants.SOUND_TINKERING_CRAFT );
		}

		/// <summary>
		/// Plays the ending effect and returns the appropriate message
		/// </summary>
		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken,
			int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendMessage( 55, TinkeringConstants.MSG_TOOL_WORN_OUT );

			if ( failed )
				return HandleCraftFailure( from, lostMaterial );

			return HandleCraftSuccess( from, quality, makersMark );
		}

		/// <summary>
		/// Determines if resources should be consumed on failure
		/// </summary>
		public override bool ConsumeOnFailure( Mobile from, Type resourceType, CraftItem craftItem )
		{
			return base.ConsumeOnFailure( from, resourceType, craftItem );
		}

		private static readonly HashSet<Type> m_TinkerColorables = new HashSet<Type>
		{
			typeof( ForkLeft ), typeof( ForkRight ),
			typeof( SpoonLeft ), typeof( SpoonRight ),
			typeof( KnifeLeft ), typeof( KnifeRight ),
			typeof( Plate ),
			typeof( Goblet ), typeof( PewterMug ),
			typeof( KeyRing ),
			typeof( Candelabra ), typeof( Scales ),
			typeof( Key ), typeof( Globe ),
			typeof( Spyglass ), typeof( Lantern ),
			typeof( HeatingStand )
		};

		/// <summary>
		/// Determines if the crafted item retains color from the resource type
		/// </summary>
		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			if ( !type.IsSubclassOf( typeof( BaseIngot ) ) )
				return false;

			return m_TinkerColorables.Contains( item.ItemType );
		}

		#endregion

		#region Helper Methods - PlayEndingEffect

		/// <summary>
		/// Handles craft failure message selection
		/// </summary>
		private int HandleCraftFailure( Mobile from, bool lostMaterial )
		{
			if ( lostMaterial )
				from.SendMessage( 55, TinkeringConstants.MSG_FAILED_LOST_MATERIALS );
			else
				from.SendMessage( 55, TinkeringConstants.MSG_FAILED_NO_MATERIALS_LOST );

			return 0;
		}

		/// <summary>
		/// Handles craft success message selection
		/// </summary>
		private int HandleCraftSuccess( Mobile from, int quality, bool makersMark )
		{
			if ( quality == TinkeringConstants.QUALITY_BELOW_AVERAGE )
			{
				from.SendMessage( 33, TinkeringConstants.MSG_BARELY_MADE_ITEM );
			}
			else if ( quality == TinkeringConstants.QUALITY_EXCEPTIONAL )
			{
				if ( makersMark )
					from.SendMessage( 95, TinkeringConstants.MSG_EXCEPTIONAL_WITH_MARK );
				else
					from.SendMessage( 95, TinkeringConstants.MSG_EXCEPTIONAL_QUALITY );
			}
			else
			{
				from.SendMessage( 95, TinkeringConstants.MSG_ITEM_CREATED );
			}

			return 0;
		}

		#endregion

		#region Helper Methods - Jewelry

		/// <summary>
		/// Adds a complete set of metal-based jewelry (amulet, bracelet, ring, earrings)
		/// </summary>
		private void AddMetalJewelrySet( Type ingotType, string metalName, double minSkill, double maxSkill,
			Type amuletType, Type braceletType, Type ringType, Type earringsType )
		{
			string metalNamePTBR = TinkeringStringConstants.GetMetalNamePTBR( metalName );
			string ingotName = TinkeringStringConstants.GetIngotName( metalName );

			string amuletName = TinkeringStringConstants.GetJewelryName( metalNamePTBR, "amulet" );
			string braceletName = TinkeringStringConstants.GetJewelryName( metalNamePTBR, "bracelet" );
			string ringName = TinkeringStringConstants.GetJewelryName( metalNamePTBR, "ring" );
			string earringsName = TinkeringStringConstants.GetJewelryName( metalNamePTBR, "earrings" );

			AddCraft( amuletType, TinkeringConstants.MSG_GROUP_JEWELRY, amuletName,
				minSkill, maxSkill, ingotType, ingotName, TinkeringConstants.RESOURCE_INGOTS_JEWELRY,
				TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );
			AddCraft( braceletType, TinkeringConstants.MSG_GROUP_JEWELRY, braceletName,
				minSkill, maxSkill, ingotType, ingotName, TinkeringConstants.RESOURCE_INGOTS_JEWELRY,
				TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );
			AddCraft( ringType, TinkeringConstants.MSG_GROUP_JEWELRY, ringName,
				minSkill, maxSkill, ingotType, ingotName, TinkeringConstants.RESOURCE_INGOTS_JEWELRY,
				TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );
			AddCraft( earringsType, TinkeringConstants.MSG_GROUP_JEWELRY, earringsName,
				minSkill, maxSkill, ingotType, ingotName, TinkeringConstants.RESOURCE_INGOTS_JEWELRY,
				TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );
		}

		/// <summary>
		/// Adds a complete set of gem-based jewelry (amulet, bracelet, ring, earrings)
		/// </summary>
		private void AddGemJewelrySet( Type gemType, string gemName, double minSkill, double maxSkill,
			Type amuletType, Type braceletType, Type ringType, Type earringsType )
		{
			string gemNamePTBR = TinkeringStringConstants.GetGemNamePTBR( gemName );

			string amuletName = TinkeringStringConstants.GetJewelryName( gemNamePTBR, "amulet" );
			string braceletName = TinkeringStringConstants.GetJewelryName( gemNamePTBR, "bracelet" );
			string ringName = TinkeringStringConstants.GetJewelryName( gemNamePTBR, "ring" );
			string earringsName = TinkeringStringConstants.GetJewelryName( gemNamePTBR, "earrings" );

			int index;

			index = AddCraft( amuletType, TinkeringConstants.MSG_GROUP_JEWELRY, amuletName,
				minSkill, maxSkill, typeof( IronIngot ), TinkeringStringConstants.INGOT_IRON,
				TinkeringConstants.RESOURCE_IRON_GEM_JEWELRY, TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );
			AddRes( index, gemType, gemNamePTBR, TinkeringConstants.RESOURCE_GEMS_JEWELRY,
				TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );

			index = AddCraft( braceletType, TinkeringConstants.MSG_GROUP_JEWELRY, braceletName,
				minSkill, maxSkill, typeof( IronIngot ), TinkeringStringConstants.INGOT_IRON,
				TinkeringConstants.RESOURCE_IRON_GEM_JEWELRY, TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );
			AddRes( index, gemType, gemNamePTBR, TinkeringConstants.RESOURCE_GEMS_JEWELRY,
				TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );

			index = AddCraft( ringType, TinkeringConstants.MSG_GROUP_JEWELRY, ringName,
				minSkill, maxSkill, typeof( IronIngot ), TinkeringStringConstants.INGOT_IRON,
				TinkeringConstants.RESOURCE_IRON_GEM_JEWELRY, TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );
			AddRes( index, gemType, gemNamePTBR, TinkeringConstants.RESOURCE_GEMS_JEWELRY,
				TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );

			index = AddCraft( earringsType, TinkeringConstants.MSG_GROUP_JEWELRY, earringsName,
				minSkill, maxSkill, typeof( IronIngot ), TinkeringStringConstants.INGOT_IRON,
				TinkeringConstants.RESOURCE_IRON_GEM_JEWELRY, TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );
			AddRes( index, gemType, gemNamePTBR, TinkeringConstants.RESOURCE_GEMS_JEWELRY,
				TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );
		}

		#endregion

		#region Helper Methods - Lockpicking Chests

		/// <summary>
		/// Adds a lockpicking training chest
		/// </summary>
		private void AddLockpickingChest( Type chestType, string chestName, double minSkill,
			Type ingotType, TextDefinition ingotName, int ingotAmount, int gemAmount )
		{
			int index = AddCraft( chestType, TinkeringConstants.MSG_GROUP_MULTI_COMPONENT, chestName,
				minSkill, TinkeringConstants.SKILL_MAX_LOCKPICKING_CHEST, ingotType, ingotName,
				ingotAmount, TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
			AddRes( index, typeof( ArcaneGem ), TinkeringConstants.MSG_ARCANE_GEM, gemAmount,
				TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );
			AddRes( index, typeof( WoodenBox ), TinkeringStringConstants.RESOURCE_WOODEN_BOX,
				TinkeringConstants.RESOURCE_WOODEN_BOX_LOCKPICKING_CHEST,
				TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );
		}

		#endregion

		#region Helper Methods - Hospitality Items

		/// <summary>
		/// Adds a hospitality item (barrels and presses)
		/// </summary>
		private void AddHospitalityItem( Type itemType, string itemName, double minSkill, double maxSkill, int gemAmount )
		{
			int index = AddCraft( itemType, TinkeringStringConstants.GROUP_HOSPITALITY, itemName,
				minSkill, maxSkill, typeof( ArcaneGem ), TinkeringConstants.MSG_ARCANE_GEM,
				gemAmount, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );
			AddRes( index, typeof( Keg ), TinkeringStringConstants.RESOURCE_KEG,
				TinkeringConstants.RESOURCE_KEGS_ALE_BARREL, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );
			AddRes( index, typeof( AxleGears ), TinkeringConstants.MSG_AXLE_GEARS,
				TinkeringConstants.RESOURCE_AXLE_GEARS_ALE_BARREL, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );
		}

		#endregion

		#region Craft List Initialization

		/// <summary>
		/// Initializes the complete tinkering craft list
		/// </summary>
		public override void InitCraftList()
		{
			AddWoodenItems();
			AddTools();
			AddParts();
			AddUtensils();
			AddMisc();
			AddJewelry();
			AddMultiComponentItems();
			AddHospitalityItems();
			InitializeMetalTypes();

			MarkOption = true;
			Repair = true;
			CanEnhance = Core.AOS;
		}

		/// <summary>
		/// Adds all wooden item crafts
		/// </summary>
		private void AddWoodenItems()
		{
			int index = -1;

			AddCraft( typeof( JointingPlane ), TinkeringConstants.MSG_GROUP_WOODEN_ITEMS,
				TinkeringConstants.MSG_JOINTING_PLANE, TinkeringConstants.SKILL_MIN_PLANES,
				TinkeringConstants.SKILL_MAX_PLANES, typeof( Log ), TinkeringConstants.MSG_LOGS,
				TinkeringConstants.RESOURCE_LOGS_PLANES, TinkeringConstants.MSG_INSUFFICIENT_LOGS );

			AddCraft( typeof( MouldingPlane ), TinkeringConstants.MSG_GROUP_WOODEN_ITEMS,
				TinkeringConstants.MSG_MOULDING_PLANE, TinkeringConstants.SKILL_MIN_PLANES,
				TinkeringConstants.SKILL_MAX_PLANES, typeof( Log ), TinkeringConstants.MSG_LOGS,
				TinkeringConstants.RESOURCE_LOGS_PLANES, TinkeringConstants.MSG_INSUFFICIENT_LOGS );

			AddCraft( typeof( SmoothingPlane ), TinkeringConstants.MSG_GROUP_WOODEN_ITEMS,
				TinkeringConstants.MSG_SMOOTHING_PLANE, TinkeringConstants.SKILL_MIN_PLANES,
				TinkeringConstants.SKILL_MAX_PLANES, typeof( Log ), TinkeringConstants.MSG_LOGS,
				TinkeringConstants.RESOURCE_LOGS_PLANES, TinkeringConstants.MSG_INSUFFICIENT_LOGS );

			AddCraft( typeof( ClockFrame ), TinkeringConstants.MSG_GROUP_WOODEN_ITEMS,
				TinkeringConstants.MSG_CLOCK_FRAME, TinkeringConstants.SKILL_MIN_CLOCK_FRAME,
				TinkeringConstants.SKILL_MAX_CLOCK_FRAME, typeof( Log ), TinkeringConstants.MSG_LOGS,
				TinkeringConstants.RESOURCE_LOGS_CLOCK_FRAME, TinkeringConstants.MSG_INSUFFICIENT_LOGS );

			AddCraft( typeof( Axle ), TinkeringConstants.MSG_GROUP_WOODEN_ITEMS,
				TinkeringConstants.MSG_AXLE, TinkeringConstants.SKILL_MIN_AXLE,
				TinkeringConstants.SKILL_MAX_AXLE, typeof( Log ), TinkeringConstants.MSG_LOGS,
				TinkeringConstants.RESOURCE_LOGS_AXLE, TinkeringConstants.MSG_INSUFFICIENT_LOGS );

			AddCraft( typeof( RollingPin ), TinkeringConstants.MSG_GROUP_WOODEN_ITEMS,
				TinkeringConstants.MSG_ROLLING_PIN, TinkeringConstants.SKILL_MIN_ROLLING_PIN,
				TinkeringConstants.SKILL_MAX_ROLLING_PIN, typeof( Log ), TinkeringConstants.MSG_LOGS,
				TinkeringConstants.RESOURCE_LOGS_ROLLING_PIN, TinkeringConstants.MSG_INSUFFICIENT_LOGS );

			index = AddCraft( typeof( SawMillSouthAddonDeed ), TinkeringConstants.MSG_GROUP_WOODEN_ITEMS,
				TinkeringStringConstants.ITEM_SAW_MILL_SOUTH, TinkeringConstants.SKILL_MIN_SAW_MILL,
				TinkeringConstants.SKILL_MAX_SAW_MILL, typeof( Granite ), TinkeringConstants.MSG_GRANITE,
				TinkeringConstants.RESOURCE_GRANITE_SAW_MILL, TinkeringConstants.MSG_INSUFFICIENT_GRANITE );
			AddSkill( index, SkillName.Lumberjacking, TinkeringConstants.SKILL_MIN_LUMBERJACKING_SAW_MILL,
				TinkeringConstants.SKILL_MAX_LUMBERJACKING_SAW_MILL );
			AddRes( index, typeof( IronIngot ), TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.RESOURCE_IRON_SAW_MILL, TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			index = AddCraft( typeof( SawMillEastAddonDeed ), TinkeringConstants.MSG_GROUP_WOODEN_ITEMS,
				TinkeringStringConstants.ITEM_SAW_MILL_EAST, TinkeringConstants.SKILL_MIN_SAW_MILL,
				TinkeringConstants.SKILL_MAX_SAW_MILL, typeof( Granite ), TinkeringConstants.MSG_GRANITE,
				TinkeringConstants.RESOURCE_GRANITE_SAW_MILL, TinkeringConstants.MSG_INSUFFICIENT_GRANITE );
			AddSkill( index, SkillName.Lumberjacking, TinkeringConstants.SKILL_MIN_LUMBERJACKING_SAW_MILL,
				TinkeringConstants.SKILL_MAX_LUMBERJACKING_SAW_MILL );
			AddRes( index, typeof( IronIngot ), TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.RESOURCE_IRON_SAW_MILL, TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			if( Core.SE )
			{
				index = AddCraft( typeof( Nunchaku ), TinkeringConstants.MSG_GROUP_WOODEN_ITEMS,
					TinkeringConstants.MSG_NUNCHAKU, TinkeringConstants.SKILL_MIN_NUNCHAKU,
					TinkeringConstants.SKILL_MAX_NUNCHAKU, typeof( IronIngot ), TinkeringConstants.MSG_IRON_INGOTS,
					TinkeringConstants.RESOURCE_IRON_NUNCHAKU, TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
				AddRes( index, typeof( Log ), TinkeringConstants.MSG_LOGS,
					TinkeringConstants.RESOURCE_LOGS_NUNCHAKU, TinkeringConstants.MSG_INSUFFICIENT_LOGS );
			}
		}

		/// <summary>
		/// Adds all tool crafts
		/// </summary>
		private void AddTools()
		{
			AddCraft( typeof( Scissors ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_SCISSORS,
				TinkeringConstants.SKILL_MIN_SCISSORS, TinkeringConstants.SKILL_MAX_SCISSORS, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SCISSORS,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( MortarPestle ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_MORTAR_PESTLE,
				TinkeringConstants.SKILL_MIN_MORTAR_PESTLE, TinkeringConstants.SKILL_MAX_MORTAR_PESTLE, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_MORTAR_PESTLE,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Scorp ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_SCORP,
				TinkeringConstants.SKILL_MIN_SCORP, TinkeringConstants.SKILL_MAX_SCORP, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SCORP,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( TinkerTools ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_TINKER_TOOLS,
				TinkeringConstants.SKILL_MIN_TINKER_TOOLS, TinkeringConstants.SKILL_MAX_TINKER_TOOLS, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_TINKER_TOOLS,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Hatchet ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_HATCHET,
				TinkeringConstants.SKILL_MIN_HATCHET, TinkeringConstants.SKILL_MAX_HATCHET, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_HATCHET,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( DrawKnife ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_DRAW_KNIFE,
				TinkeringConstants.SKILL_MIN_DRAW_KNIFE, TinkeringConstants.SKILL_MAX_DRAW_KNIFE, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_DRAW_KNIFE,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( SewingKit ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_SEWING_KIT,
				TinkeringConstants.SKILL_MIN_SEWING_KIT, TinkeringConstants.SKILL_MAX_SEWING_KIT, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SEWING_KIT,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( GardenTool ), TinkeringConstants.MSG_GROUP_TOOLS,
				TinkeringStringConstants.ITEM_GARDEN_TOOL, TinkeringConstants.SKILL_MIN_GARDEN_TOOL,
				TinkeringConstants.SKILL_MAX_GARDEN_TOOL, typeof( IronIngot ), TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.RESOURCE_IRON_GARDEN_TOOL, TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( HerbalistCauldron ), TinkeringConstants.MSG_GROUP_TOOLS,
				TinkeringStringConstants.ITEM_HERBALIST_CAULDRON, TinkeringConstants.SKILL_MIN_HERBALIST_CAULDRON,
				TinkeringConstants.SKILL_MAX_HERBALIST_CAULDRON, typeof( IronIngot ), TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.RESOURCE_IRON_HERBALIST_CAULDRON, TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Saw ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_SAW,
				TinkeringConstants.SKILL_MIN_SAW, TinkeringConstants.SKILL_MAX_SAW, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SAW,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( DovetailSaw ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_DOVETAIL_SAW,
				TinkeringConstants.SKILL_MIN_DOVETAIL_SAW, TinkeringConstants.SKILL_MAX_DOVETAIL_SAW, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_DOVETAIL_SAW,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Froe ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_FROE,
				TinkeringConstants.SKILL_MIN_FROE, TinkeringConstants.SKILL_MAX_FROE, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_FROE,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Shovel ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_SHOVEL,
				TinkeringConstants.SKILL_MIN_SHOVEL, TinkeringConstants.SKILL_MAX_SHOVEL, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SHOVEL,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( OreShovel ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringStringConstants.ITEM_ORE_SHOVEL,
				TinkeringConstants.SKILL_MIN_ORE_SHOVEL, TinkeringConstants.SKILL_MAX_ORE_SHOVEL, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_ORE_SHOVEL,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( GraveShovel ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringStringConstants.ITEM_GRAVE_SHOVEL,
				TinkeringConstants.SKILL_MIN_GRAVE_SHOVEL, TinkeringConstants.SKILL_MAX_GRAVE_SHOVEL, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_GRAVE_SHOVEL,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Hammer ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_HAMMER,
				TinkeringConstants.SKILL_MIN_HAMMER, TinkeringConstants.SKILL_MAX_HAMMER, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_HAMMER,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Tongs ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_TONGS,
				TinkeringConstants.SKILL_MIN_TONGS, TinkeringConstants.SKILL_MAX_TONGS, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_TONGS,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( SmithHammer ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_SMITH_HAMMER,
				TinkeringConstants.SKILL_MIN_SMITH_HAMMER, TinkeringConstants.SKILL_MAX_SMITH_HAMMER, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SMITH_HAMMER,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( SledgeHammer ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_SLEDGE_HAMMER,
				TinkeringConstants.SKILL_MIN_SLEDGE_HAMMER, TinkeringConstants.SKILL_MAX_SLEDGE_HAMMER, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SLEDGE_HAMMER,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Inshave ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_INSHAVE,
				TinkeringConstants.SKILL_MIN_INSHAVE, TinkeringConstants.SKILL_MAX_INSHAVE, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_INSHAVE,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Pickaxe ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_PICKAXE,
				TinkeringConstants.SKILL_MIN_PICKAXE, TinkeringConstants.SKILL_MAX_PICKAXE, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_PICKAXE,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Lockpick ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_LOCKPICK,
				TinkeringConstants.SKILL_MIN_LOCKPICK, TinkeringConstants.SKILL_MAX_LOCKPICK, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_LOCKPICK,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Skillet ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_SKILLET,
				TinkeringConstants.SKILL_MIN_SKILLET, TinkeringConstants.SKILL_MAX_SKILLET, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SKILLET,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( FlourSifter ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_FLOUR_SIFTER,
				TinkeringConstants.SKILL_MIN_FLOUR_SIFTER, TinkeringConstants.SKILL_MAX_FLOUR_SIFTER, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_FLOUR_SIFTER,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( FletcherTools ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_FLETCHER_TOOLS,
				TinkeringConstants.SKILL_MIN_FLETCHER_TOOLS, TinkeringConstants.SKILL_MAX_FLETCHER_TOOLS, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_FLETCHER_TOOLS,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( MapmakersPen ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_MAPMAKERS_PEN,
				TinkeringConstants.SKILL_MIN_MAPMAKERS_PEN, TinkeringConstants.SKILL_MAX_MAPMAKERS_PEN, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_MAPMAKERS_PEN,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( ScribesPen ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringConstants.MSG_SCRIBES_PEN,
				TinkeringConstants.SKILL_MIN_SCRIBES_PEN, TinkeringConstants.SKILL_MAX_SCRIBES_PEN, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SCRIBES_PEN,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( SkinningKnife ), TinkeringConstants.MSG_GROUP_TOOLS,
				TinkeringStringConstants.ITEM_SKINNING_KNIFE, TinkeringConstants.SKILL_MIN_SKINNING_KNIFE,
				TinkeringConstants.SKILL_MAX_SKINNING_KNIFE, typeof( IronIngot ), TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.RESOURCE_IRON_SKINNING_KNIFE, TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( SurgeonsKnife ), TinkeringConstants.MSG_GROUP_TOOLS,
				TinkeringStringConstants.ITEM_SURGEONS_KNIFE, TinkeringConstants.SKILL_MIN_SURGEONS_KNIFE,
				TinkeringConstants.SKILL_MAX_SURGEONS_KNIFE, typeof( IronIngot ), TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.RESOURCE_IRON_SURGEONS_KNIFE, TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( MixingCauldron ), TinkeringConstants.MSG_GROUP_TOOLS,
				TinkeringStringConstants.ITEM_MIXING_CAULDRON, TinkeringConstants.SKILL_MIN_MIXING_CAULDRON,
				TinkeringConstants.SKILL_MAX_MIXING_CAULDRON, typeof( IronIngot ), TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.RESOURCE_IRON_MIXING_CAULDRON, TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( WaxingPot ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringStringConstants.ITEM_WAXING_POT,
				TinkeringConstants.SKILL_MIN_WAXING_POT, TinkeringConstants.SKILL_MAX_WAXING_POT, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_WAXING_POT,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( WoodworkingTools ), TinkeringConstants.MSG_GROUP_TOOLS,
				TinkeringStringConstants.ITEM_WOODWORKING_TOOLS, TinkeringConstants.SKILL_MIN_WOODWORKING_TOOLS,
				TinkeringConstants.SKILL_MAX_WOODWORKING_TOOLS, typeof( IronIngot ), TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.RESOURCE_IRON_WOODWORKING_TOOLS, TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( TrapKit ), TinkeringConstants.MSG_GROUP_TOOLS, TinkeringStringConstants.ITEM_TRAP_KIT,
				TinkeringConstants.SKILL_MIN_TRAP_KIT, TinkeringConstants.SKILL_MAX_TRAP_KIT, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_TRAP_KIT,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
		}

		/// <summary>
		/// Adds all part crafts
		/// </summary>
		private void AddParts()
		{
			AddCraft( typeof( Gears ), TinkeringConstants.MSG_GROUP_PARTS, TinkeringConstants.MSG_GEARS,
				TinkeringConstants.SKILL_MIN_GEARS, TinkeringConstants.SKILL_MAX_GEARS, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_GEARS,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( ClockParts ), TinkeringConstants.MSG_GROUP_PARTS, TinkeringConstants.MSG_CLOCK_PARTS,
				TinkeringConstants.SKILL_MIN_CLOCK_PARTS, TinkeringConstants.SKILL_MAX_CLOCK_PARTS, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_CLOCK_PARTS,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( BarrelTap ), TinkeringConstants.MSG_GROUP_PARTS, TinkeringConstants.MSG_BARREL_TAP_ITEM,
				TinkeringConstants.SKILL_MIN_BARREL_TAP, TinkeringConstants.SKILL_MAX_BARREL_TAP, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_BARREL_TAP,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Springs ), TinkeringConstants.MSG_GROUP_PARTS, TinkeringConstants.MSG_SPRINGS,
				TinkeringConstants.SKILL_MIN_SPRINGS, TinkeringConstants.SKILL_MAX_SPRINGS, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SPRINGS,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( SextantParts ), TinkeringConstants.MSG_GROUP_PARTS, TinkeringConstants.MSG_SEXTANT_PARTS,
				TinkeringConstants.SKILL_MIN_SEXTANT_PARTS, TinkeringConstants.SKILL_MAX_SEXTANT_PARTS, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SEXTANT_PARTS,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( BarrelHoops ), TinkeringConstants.MSG_GROUP_PARTS, TinkeringConstants.MSG_BARREL_HOOPS,
				TinkeringConstants.SKILL_MIN_BARREL_HOOPS, TinkeringConstants.SKILL_MAX_BARREL_HOOPS, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_BARREL_HOOPS,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Hinge ), TinkeringConstants.MSG_GROUP_PARTS, TinkeringConstants.MSG_HINGE,
				TinkeringConstants.SKILL_MIN_HINGE, TinkeringConstants.SKILL_MAX_HINGE, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_HINGE,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( BolaBall ), TinkeringConstants.MSG_GROUP_PARTS, TinkeringConstants.MSG_BOLA_BALL,
				TinkeringConstants.SKILL_MIN_BOLA_BALL, TinkeringConstants.SKILL_MAX_BOLA_BALL, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_BOLA_BALL,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
		}

		/// <summary>
		/// Adds all utensil crafts
		/// </summary>
		private void AddUtensils()
		{
			AddCraft( typeof( ButcherKnife ), TinkeringConstants.MSG_GROUP_UTENSILS, TinkeringConstants.MSG_BUTCHER_KNIFE,
				TinkeringConstants.SKILL_MIN_BUTCHER_KNIFE, TinkeringConstants.SKILL_MAX_BUTCHER_KNIFE, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_BUTCHER_KNIFE,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( SpoonLeft ), TinkeringConstants.MSG_GROUP_UTENSILS, TinkeringConstants.MSG_SPOON_LEFT,
				TinkeringConstants.SKILL_MIN_SPOON, TinkeringConstants.SKILL_MAX_SPOON, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SPOON,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( SpoonRight ), TinkeringConstants.MSG_GROUP_UTENSILS, TinkeringConstants.MSG_SPOON_RIGHT,
				TinkeringConstants.SKILL_MIN_SPOON, TinkeringConstants.SKILL_MAX_SPOON, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SPOON,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Plate ), TinkeringConstants.MSG_GROUP_UTENSILS, TinkeringConstants.MSG_PLATE,
				TinkeringConstants.SKILL_MIN_PLATE, TinkeringConstants.SKILL_MAX_PLATE, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_PLATE,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( ForkLeft ), TinkeringConstants.MSG_GROUP_UTENSILS, TinkeringConstants.MSG_FORK_LEFT,
				TinkeringConstants.SKILL_MIN_FORK, TinkeringConstants.SKILL_MAX_FORK, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_FORK,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( ForkRight ), TinkeringConstants.MSG_GROUP_UTENSILS, TinkeringConstants.MSG_FORK_RIGHT,
				TinkeringConstants.SKILL_MIN_FORK, TinkeringConstants.SKILL_MAX_FORK, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_FORK,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Cleaver ), TinkeringConstants.MSG_GROUP_UTENSILS, TinkeringConstants.MSG_CLEAVER,
				TinkeringConstants.SKILL_MIN_CLEAVER, TinkeringConstants.SKILL_MAX_CLEAVER, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_CLEAVER,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( KnifeLeft ), TinkeringConstants.MSG_GROUP_UTENSILS, TinkeringConstants.MSG_KNIFE_LEFT,
				TinkeringConstants.SKILL_MIN_KNIFE, TinkeringConstants.SKILL_MAX_KNIFE, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_KNIFE,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( KnifeRight ), TinkeringConstants.MSG_GROUP_UTENSILS, TinkeringConstants.MSG_KNIFE_RIGHT,
				TinkeringConstants.SKILL_MIN_KNIFE, TinkeringConstants.SKILL_MAX_KNIFE, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_KNIFE,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Goblet ), TinkeringConstants.MSG_GROUP_UTENSILS, TinkeringConstants.MSG_GOBLET,
				TinkeringConstants.SKILL_MIN_GOBLET, TinkeringConstants.SKILL_MAX_GOBLET, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_GOBLET,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( PewterMug ), TinkeringConstants.MSG_GROUP_UTENSILS, TinkeringConstants.MSG_PEWTER_MUG,
				TinkeringConstants.SKILL_MIN_PEWTER_MUG, TinkeringConstants.SKILL_MAX_PEWTER_MUG, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_PEWTER_MUG,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( SkinningKnife ), TinkeringConstants.MSG_GROUP_UTENSILS, TinkeringConstants.MSG_SKINNING_KNIFE,
				TinkeringConstants.SKILL_MIN_SKINNING_KNIFE_UTENSILS, TinkeringConstants.SKILL_MAX_SKINNING_KNIFE_UTENSILS,
				typeof( IronIngot ), TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SKINNING_KNIFE_UTENSILS,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
		}

		/// <summary>
		/// Adds all miscellaneous item crafts
		/// </summary>
		private void AddMisc()
		{
			int index;

			index = AddCraft( typeof( CandleLarge ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_CANDLE_LARGE,
				TinkeringConstants.SKILL_MIN_CANDLE_LARGE, TinkeringConstants.SKILL_MAX_CANDLE_LARGE, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_CANDLE_LARGE,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
			AddRes( index, typeof( Beeswax ), TinkeringConstants.MSG_BEESWAX, TinkeringConstants.RESOURCE_BEESWAX_CANDLE_LARGE,
				TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );

			index = AddCraft( typeof( Candelabra ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_CANDELABRA,
				TinkeringConstants.SKILL_MIN_CANDELABRA, TinkeringConstants.SKILL_MAX_CANDELABRA, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_CANDELABRA,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
			AddRes( index, typeof( Beeswax ), TinkeringConstants.MSG_BEESWAX, TinkeringConstants.RESOURCE_BEESWAX_CANDELABRA,
				TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );

			index = AddCraft( typeof( CandelabraStand ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_CANDELABRA,
				TinkeringConstants.SKILL_MIN_CANDELABRA_STAND, TinkeringConstants.SKILL_MAX_CANDELABRA_STAND, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_CANDELABRA_STAND,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
			AddRes( index, typeof( Beeswax ), TinkeringConstants.MSG_BEESWAX, TinkeringConstants.RESOURCE_BEESWAX_CANDELABRA_STAND,
				TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );

			AddCraft( typeof( Scales ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_SCALES,
				TinkeringConstants.SKILL_MIN_SCALES, TinkeringConstants.SKILL_MAX_SCALES, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SCALES,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Key ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_KEY,
				TinkeringConstants.SKILL_MIN_KEY, TinkeringConstants.SKILL_MAX_KEY, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_KEY,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( KeyRing ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_KEY_RING,
				TinkeringConstants.SKILL_MIN_KEY_RING, TinkeringConstants.SKILL_MAX_KEY_RING, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_KEY_RING,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			// Eternal Key (Chave Eterna) - Requires 90+ Tinkering and Magery, uses special resources
			// Success chance: 20% at 90.0 skill, +0.5% per skill point, capped at 35% at 120.0 skill
			int eternalKeyIndex = AddCraft( typeof( EternalKey ), TinkeringConstants.MSG_GROUP_MISC, "Chave Eterna",
				90.0, 120.0, typeof( PlatinumIngot ), TinkeringConstants.MSG_METAL_PLATINUM, 10, TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
			AddSkill( eternalKeyIndex, SkillName.Magery, 90.0, 120.0 );
			AddRes( eternalKeyIndex, typeof( GoldIngot ), TinkeringConstants.MSG_METAL_GOLD, 4, TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
			AddRes( eternalKeyIndex, typeof( ArcaneGem ), TinkeringConstants.MSG_ARCANE_GEM, 1, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );

			AddCraft( typeof( Globe ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_GLOBE,
				TinkeringConstants.SKILL_MIN_GLOBE, TinkeringConstants.SKILL_MAX_GLOBE, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_GLOBE,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Spyglass ), TinkeringConstants.MSG_GROUP_MISC, TinkeringStringConstants.ITEM_SPYGLASS,
				TinkeringConstants.SKILL_MIN_SPYGLASS, TinkeringConstants.SKILL_MAX_SPYGLASS, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SPYGLASS,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( Lantern ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_LANTERN,
				TinkeringConstants.SKILL_MIN_LANTERN, TinkeringConstants.SKILL_MAX_LANTERN, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_LANTERN,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( HeatingStand ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_HEATING_STAND,
				TinkeringConstants.SKILL_MIN_HEATING_STAND, TinkeringConstants.SKILL_MAX_HEATING_STAND, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_HEATING_STAND,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			index = AddCraft( typeof( WallTorch ), TinkeringConstants.MSG_GROUP_MISC, TinkeringStringConstants.ITEM_WALL_TORCH,
				TinkeringConstants.SKILL_MIN_WALL_TORCH, TinkeringConstants.SKILL_MAX_WALL_TORCH, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_WALL_TORCH,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
			AddRes( index, typeof( Torch ), TinkeringConstants.MSG_TORCH, TinkeringConstants.RESOURCE_TORCH_WALL_TORCH,
				TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );

			index = AddCraft( typeof( ColoredWallTorch ), TinkeringConstants.MSG_GROUP_MISC,
				TinkeringStringConstants.ITEM_COLORED_WALL_TORCH, TinkeringConstants.SKILL_MIN_COLORED_WALL_TORCH,
				TinkeringConstants.SKILL_MAX_COLORED_WALL_TORCH, typeof( IronIngot ), TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.RESOURCE_IRON_COLORED_WALL_TORCH, TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
			AddRes( index, typeof( Torch ), TinkeringConstants.MSG_TORCH, TinkeringConstants.RESOURCE_TORCH_COLORED_WALL_TORCH,
				TinkeringConstants.MSG_INSUFFICIENT_BEESWAX );

			index = AddCraft( typeof( ShojiLantern ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_SHOJI_LANTERN,
				TinkeringConstants.SKILL_MIN_SHOJI_LANTERN, TinkeringConstants.SKILL_MAX_SHOJI_LANTERN, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_SHOJI_LANTERN,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
			AddRes( index, typeof( Log ), TinkeringConstants.MSG_LOGS, TinkeringConstants.RESOURCE_LOGS_SHOJI_LANTERN,
				TinkeringConstants.MSG_INSUFFICIENT_LOGS );

			index = AddCraft( typeof( PaperLantern ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_PAPER_LANTERN,
				TinkeringConstants.SKILL_MIN_PAPER_LANTERN, TinkeringConstants.SKILL_MAX_PAPER_LANTERN, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_PAPER_LANTERN,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
			AddRes( index, typeof( Log ), TinkeringConstants.MSG_LOGS, TinkeringConstants.RESOURCE_LOGS_PAPER_LANTERN,
				TinkeringConstants.MSG_INSUFFICIENT_LOGS );

			index = AddCraft( typeof( RoundPaperLantern ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_ROUND_PAPER_LANTERN,
				TinkeringConstants.SKILL_MIN_ROUND_PAPER_LANTERN, TinkeringConstants.SKILL_MAX_ROUND_PAPER_LANTERN, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_ROUND_PAPER_LANTERN,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
			AddRes( index, typeof( Log ), TinkeringConstants.MSG_LOGS, TinkeringConstants.RESOURCE_LOGS_ROUND_PAPER_LANTERN,
				TinkeringConstants.MSG_INSUFFICIENT_LOGS );

			AddCraft( typeof( WindChimes ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_WIND_CHIMES,
				TinkeringConstants.SKILL_MIN_WIND_CHIMES, TinkeringConstants.SKILL_MAX_WIND_CHIMES, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_WIND_CHIMES,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );

			AddCraft( typeof( FancyWindChimes ), TinkeringConstants.MSG_GROUP_MISC, TinkeringConstants.MSG_FANCY_WIND_CHIMES,
				TinkeringConstants.SKILL_MIN_FANCY_WIND_CHIMES, TinkeringConstants.SKILL_MAX_FANCY_WIND_CHIMES, typeof( IronIngot ),
				TinkeringConstants.MSG_IRON_INGOTS, TinkeringConstants.RESOURCE_IRON_FANCY_WIND_CHIMES,
				TinkeringConstants.MSG_INSUFFICIENT_INGOTS );
		}

		/// <summary>
		/// Adds all jewelry crafts
		/// </summary>
		private void AddJewelry()
		{
			// Metal-based jewelry
			AddMetalJewelrySet( typeof( AgapiteIngot ), "agapite", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( AgapiteAmulet ), typeof( AgapiteBracelet ),
				typeof( AgapiteRing ), typeof( AgapiteEarrings ) );

			AddMetalJewelrySet( typeof( AmethystIngot ), "amethyst", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( AmethystAmulet ), typeof( AmethystBracelet ),
				typeof( AmethystRing ), typeof( AmethystEarrings ) );

			// GATED RESOURCE: Brass ingots are not yet available to players
			//AddMetalJewelrySet( typeof( BrassIngot ), "brass", TinkeringConstants.SKILL_MIN_JEWELRY,
			//	TinkeringConstants.SKILL_MAX_JEWELRY, typeof( BrassAmulet ), typeof( BrassBracelet ),
			//	typeof( BrassRing ), typeof( BrassEarrings ) );

			AddMetalJewelrySet( typeof( BronzeIngot ), "bronze", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( BronzeAmulet ), typeof( BronzeBracelet ),
				typeof( BronzeRing ), typeof( BronzeEarrings ) );

			AddMetalJewelrySet( typeof( CaddelliteIngot ), "caddellite", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( CaddelliteAmulet ), typeof( CaddelliteBracelet ),
				typeof( CaddelliteRing ), typeof( CaddelliteEarrings ) );

			AddMetalJewelrySet( typeof( CopperIngot ), "copper", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( CopperAmulet ), typeof( CopperBracelet ),
				typeof( CopperRing ), typeof( CopperEarrings ) );

			AddMetalJewelrySet( typeof( DullCopperIngot ), "dull copper", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( DullCopperAmulet ), typeof( DullCopperBracelet ),
				typeof( DullCopperRing ), typeof( DullCopperEarrings ) );

			AddMetalJewelrySet( typeof( GarnetIngot ), "garnet", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( GarnetAmulet ), typeof( GarnetBracelet ),
				typeof( GarnetRing ), typeof( GarnetEarrings ) );

			AddMetalJewelrySet( typeof( GoldIngot ), "golden", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( GoldenAmulet ), typeof( GoldenBracelet ),
				typeof( GoldenRing ), typeof( GoldenEarrings ) );

			AddMetalJewelrySet( typeof( JadeIngot ), "jade", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( JadeAmulet ), typeof( JadeBracelet ),
				typeof( JadeRing ), typeof( JadeEarrings ) );

			// GATED RESOURCE: Mithril ingots are not yet available to players
			//AddMetalJewelrySet( typeof( MithrilIngot ), "mithril", TinkeringConstants.SKILL_MIN_JEWELRY,
			//	TinkeringConstants.SKILL_MAX_JEWELRY, typeof( MithrilAmulet ), typeof( MithrilBracelet ),
			//	typeof( MithrilRing ), typeof( MithrilEarrings ) );

			// GATED RESOURCE: Nepturite ingots are not yet available to players
			//AddMetalJewelrySet( typeof( NepturiteIngot ), "nepturite", TinkeringConstants.SKILL_MIN_JEWELRY,
			//	TinkeringConstants.SKILL_MAX_JEWELRY, typeof( NepturiteAmulet ), typeof( NepturiteBracelet ),
			//	typeof( NepturiteRing ), typeof( NepturiteEarrings ) );

			// GATED RESOURCE: Obsidian ingots are not yet available to players
			//AddMetalJewelrySet( typeof( ObsidianIngot ), "obsidian", TinkeringConstants.SKILL_MIN_JEWELRY,
			//	TinkeringConstants.SKILL_MAX_JEWELRY, typeof( ObsidianAmulet ), typeof( ObsidianBracelet ),
			//	typeof( ObsidianRing ), typeof( ObsidianEarrings ) );

			AddMetalJewelrySet( typeof( OnyxIngot ), "onyx", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( OnyxAmulet ), typeof( OnyxBracelet ),
				typeof( OnyxRing ), typeof( OnyxEarrings ) );

			AddMetalJewelrySet( typeof( QuartzIngot ), "quartz", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( QuartzAmulet ), typeof( QuartzBracelet ),
				typeof( QuartzRing ), typeof( QuartzEarrings ) );

			AddMetalJewelrySet( typeof( ShadowIronIngot ), "shadow iron", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( ShadowIronAmulet ), typeof( ShadowIronBracelet ),
				typeof( ShadowIronRing ), typeof( ShadowIronEarrings ) );

			AddMetalJewelrySet( typeof( ShinySilverIngot ), "silvery", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( SilveryAmulet ), typeof( SilveryBracelet ),
				typeof( SilveryRing ), typeof( SilveryEarrings ) );

			AddMetalJewelrySet( typeof( SpinelIngot ), "spinel", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( SpinelAmulet ), typeof( SpinelBracelet ),
				typeof( SpinelRing ), typeof( SpinelEarrings ) );

			AddMetalJewelrySet( typeof( StarRubyIngot ), "star ruby", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( StarRubyAmulet ), typeof( StarRubyBracelet ),
				typeof( StarRubyRing ), typeof( StarRubyEarrings ) );

			// GATED RESOURCE: Steel ingots are not yet available to players
			//AddMetalJewelrySet( typeof( SteelIngot ), "steel", TinkeringConstants.SKILL_MIN_JEWELRY,
			//	TinkeringConstants.SKILL_MAX_JEWELRY, typeof( SteelAmulet ), typeof( SteelBracelet ),
			//	typeof( SteelRing ), typeof( SteelEarrings ) );

			AddMetalJewelrySet( typeof( TopazIngot ), "topaz", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( TopazAmulet ), typeof( TopazBracelet ),
				typeof( TopazRing ), typeof( TopazEarrings ) );

			AddMetalJewelrySet( typeof( ValoriteIngot ), "valorite", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( ValoriteAmulet ), typeof( ValoriteBracelet ),
				typeof( ValoriteRing ), typeof( ValoriteEarrings ) );

			AddMetalJewelrySet( typeof( VeriteIngot ), "verite", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( VeriteAmulet ), typeof( VeriteBracelet ),
				typeof( VeriteRing ), typeof( VeriteEarrings ) );

			// Dwarven jewelry (higher skill requirement)
			AddMetalJewelrySet( typeof( MithrilIngot ), "dwarven", TinkeringConstants.SKILL_MIN_DWARVEN_JEWELRY,
				TinkeringConstants.SKILL_MAX_DWARVEN_JEWELRY, typeof( DwarvenAmulet ), typeof( DwarvenBracelet ),
				typeof( DwarvenRing ), typeof( DwarvenEarrings ) );

			// Gem-based jewelry
			AddGemJewelrySet( typeof( Amber ), "amber", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( AmberAmulet ), typeof( AmberBracelet ),
				typeof( AmberRing ), typeof( AmberEarrings ) );

			AddGemJewelrySet( typeof( Diamond ), "diamond", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( DiamondAmulet ), typeof( DiamondBracelet ),
				typeof( DiamondRing ), typeof( DiamondEarrings ) );

			AddGemJewelrySet( typeof( Emerald ), "emerald", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( EmeraldAmulet ), typeof( EmeraldBracelet ),
				typeof( EmeraldRing ), typeof( EmeraldEarrings ) );

			AddGemJewelrySet( typeof( MysticalPearl ), "pearl", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( PearlAmulet ), typeof( PearlBracelet ),
				typeof( PearlRing ), typeof( PearlEarrings ) );

			AddGemJewelrySet( typeof( Ruby ), "ruby", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( RubyAmulet ), typeof( RubyBracelet ),
				typeof( RubyRing ), typeof( RubyEarrings ) );

			AddGemJewelrySet( typeof( Sapphire ), "sapphire", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( SapphireAmulet ), typeof( SapphireBracelet ),
				typeof( SapphireRing ), typeof( SapphireEarrings ) );

			AddGemJewelrySet( typeof( StarSapphire ), "star sapphire", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( StarSapphireAmulet ), typeof( StarSapphireBracelet ),
				typeof( StarSapphireRing ), typeof( StarSapphireEarrings ) );

			AddGemJewelrySet( typeof( Tourmaline ), "tourmaline", TinkeringConstants.SKILL_MIN_JEWELRY,
				TinkeringConstants.SKILL_MAX_JEWELRY, typeof( TourmalineAmulet ), typeof( TourmalineBracelet ),
				typeof( TourmalineRing ), typeof( TourmalineEarrings ) );
		}

		/// <summary>
		/// Adds all multi-component and assembly item crafts
		/// </summary>
		private void AddMultiComponentItems()
		{
			int index;

			// Assemblies
			index = AddCraft( typeof( AxleGears ), TinkeringConstants.MSG_GROUP_MULTI_COMPONENT,
				TinkeringConstants.MSG_AXLE_GEARS_ITEM, TinkeringConstants.SKILL_MIN_AXLE_GEARS,
				TinkeringConstants.SKILL_MAX_AXLE_GEARS, typeof( Axle ), TinkeringConstants.MSG_AXLE_RESOURCE,
				TinkeringConstants.RESOURCE_AXLE_AXLE_GEARS, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );
			AddRes( index, typeof( Gears ), TinkeringConstants.MSG_GEARS_RESOURCE,
				TinkeringConstants.RESOURCE_GEARS_AXLE_GEARS, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );

			index = AddCraft( typeof( ClockParts ), TinkeringConstants.MSG_GROUP_MULTI_COMPONENT,
				TinkeringConstants.MSG_CLOCK_PARTS, TinkeringConstants.SKILL_MIN_CLOCK_PARTS_ASSEMBLY,
				TinkeringConstants.SKILL_MAX_CLOCK_PARTS_ASSEMBLY, typeof( AxleGears ), TinkeringConstants.MSG_AXLE_GEARS,
				TinkeringConstants.RESOURCE_AXLE_GEARS_CLOCK_PARTS, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );
			AddRes( index, typeof( Springs ), TinkeringConstants.MSG_SPRINGS_RESOURCE,
				TinkeringConstants.RESOURCE_SPRINGS_CLOCK_PARTS, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );

			index = AddCraft( typeof( SextantParts ), TinkeringConstants.MSG_GROUP_MULTI_COMPONENT,
				TinkeringConstants.MSG_SEXTANT_PARTS, TinkeringConstants.SKILL_MIN_SEXTANT_PARTS_ASSEMBLY,
				TinkeringConstants.SKILL_MAX_SEXTANT_PARTS_ASSEMBLY, typeof( AxleGears ), TinkeringConstants.MSG_AXLE_GEARS,
				TinkeringConstants.RESOURCE_AXLE_GEARS_SEXTANT_PARTS, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );
			AddRes( index, typeof( Hinge ), TinkeringConstants.MSG_HINGE_RESOURCE,
				TinkeringConstants.RESOURCE_HINGE_SEXTANT_PARTS, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );

			index = AddCraft( typeof( ClockRight ), TinkeringConstants.MSG_GROUP_MULTI_COMPONENT,
				TinkeringConstants.MSG_CLOCK_RIGHT, TinkeringConstants.SKILL_MIN_CLOCK,
				TinkeringConstants.SKILL_MAX_CLOCK, typeof( ClockFrame ), TinkeringConstants.MSG_CLOCK_FRAME_RESOURCE,
				TinkeringConstants.RESOURCE_CLOCK_FRAME_CLOCK, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );
			AddRes( index, typeof( ClockParts ), TinkeringConstants.MSG_CLOCK_PARTS_RESOURCE,
				TinkeringConstants.RESOURCE_CLOCK_PARTS_CLOCK, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );

			index = AddCraft( typeof( ClockLeft ), TinkeringConstants.MSG_GROUP_MULTI_COMPONENT,
				TinkeringConstants.MSG_CLOCK_LEFT, TinkeringConstants.SKILL_MIN_CLOCK,
				TinkeringConstants.SKILL_MAX_CLOCK, typeof( ClockFrame ), TinkeringConstants.MSG_CLOCK_FRAME_RESOURCE,
				TinkeringConstants.RESOURCE_CLOCK_FRAME_CLOCK, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );
			AddRes( index, typeof( ClockParts ), TinkeringConstants.MSG_CLOCK_PARTS_RESOURCE,
				TinkeringConstants.RESOURCE_CLOCK_PARTS_CLOCK, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );

			AddCraft( typeof( Sextant ), TinkeringConstants.MSG_GROUP_MULTI_COMPONENT, TinkeringConstants.MSG_SEXTANT,
				TinkeringConstants.SKILL_MIN_SEXTANT, TinkeringConstants.SKILL_MAX_SEXTANT, typeof( SextantParts ),
				TinkeringConstants.MSG_SEXTANT_PARTS_RESOURCE, TinkeringConstants.RESOURCE_SEXTANT_PARTS_SEXTANT,
				TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );

			index = AddCraft( typeof( Bola ), TinkeringConstants.MSG_GROUP_MULTI_COMPONENT, TinkeringConstants.MSG_BOLA,
				TinkeringConstants.SKILL_MIN_BOLA, TinkeringConstants.SKILL_MAX_BOLA, typeof( BolaBall ),
				TinkeringConstants.MSG_BOLA_BALL_RESOURCE, TinkeringConstants.RESOURCE_BOLA_BALL_BOLA,
				TinkeringConstants.MSG_FAILED_CREATE );
			AddRes( index, typeof( Leather ), TinkeringConstants.MSG_LEATHER, TinkeringConstants.RESOURCE_LEATHER_BOLA,
				TinkeringConstants.MSG_INSUFFICIENT_LEATHER );

			index = AddCraft( typeof( PotionKeg ), TinkeringConstants.MSG_GROUP_MULTI_COMPONENT,
				TinkeringConstants.MSG_POTION_KEG, TinkeringConstants.SKILL_MIN_POTION_KEG,
				TinkeringConstants.SKILL_MAX_POTION_KEG, typeof( Keg ), TinkeringStringConstants.RESOURCE_KEG,
				TinkeringConstants.RESOURCE_KEG_POTION_KEG, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );
			AddRes( index, typeof( Bottle ), TinkeringConstants.MSG_BOTTLE, TinkeringConstants.RESOURCE_BOTTLES_POTION_KEG,
				TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );
			AddRes( index, typeof( BarrelLid ), TinkeringConstants.MSG_BARREL_LID,
				TinkeringConstants.RESOURCE_BARREL_LID_POTION_KEG, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );
			AddRes( index, typeof( BarrelTap ), TinkeringConstants.MSG_BARREL_TAP,
				TinkeringConstants.RESOURCE_BARREL_TAP_POTION_KEG, TinkeringConstants.MSG_INSUFFICIENT_RESOURCES );

			// Lockpicking training chests
			AddLockpickingChest( typeof( LockpickingChest10 ), TinkeringStringConstants.ITEM_LOCKPICKING_CHEST_10,
				TinkeringConstants.SKILL_MIN_LOCKPICKING_CHEST_10, typeof( IronIngot ), TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.RESOURCE_IRON_LOCKPICKING_CHEST_10, TinkeringConstants.RESOURCE_GEMS_LOCKPICKING_CHEST_10 );

			AddLockpickingChest( typeof( LockpickingChest20 ), TinkeringStringConstants.ITEM_LOCKPICKING_CHEST_20,
				TinkeringConstants.SKILL_MIN_LOCKPICKING_CHEST_20, typeof( IronIngot ), TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.RESOURCE_IRON_LOCKPICKING_CHEST_20, TinkeringConstants.RESOURCE_GEMS_LOCKPICKING_CHEST_20 );

			AddLockpickingChest( typeof( LockpickingChest30 ), TinkeringStringConstants.ITEM_LOCKPICKING_CHEST_30,
				TinkeringConstants.SKILL_MIN_LOCKPICKING_CHEST_30, typeof( DullCopperIngot ), TinkeringConstants.MSG_DULL_COPPER_INGOT,
				TinkeringConstants.RESOURCE_DULL_COPPER_LOCKPICKING_CHEST_30, TinkeringConstants.RESOURCE_GEMS_LOCKPICKING_CHEST_30 );

			AddLockpickingChest( typeof( LockpickingChest40 ), TinkeringStringConstants.ITEM_LOCKPICKING_CHEST_40,
				TinkeringConstants.SKILL_MIN_LOCKPICKING_CHEST_40, typeof( ShadowIronIngot ), TinkeringConstants.MSG_SHADOW_IRON_INGOT,
				TinkeringConstants.RESOURCE_SHADOW_IRON_LOCKPICKING_CHEST_40, TinkeringConstants.RESOURCE_GEMS_LOCKPICKING_CHEST_40 );

			AddLockpickingChest( typeof( LockpickingChest50 ), TinkeringStringConstants.ITEM_LOCKPICKING_CHEST_50,
				TinkeringConstants.SKILL_MIN_LOCKPICKING_CHEST_50, typeof( BronzeIngot ), TinkeringConstants.MSG_BRONZE_INGOT,
				TinkeringConstants.RESOURCE_BRONZE_LOCKPICKING_CHEST_50, TinkeringConstants.RESOURCE_GEMS_LOCKPICKING_CHEST_50 );

			AddLockpickingChest( typeof( LockpickingChest60 ), TinkeringStringConstants.ITEM_LOCKPICKING_CHEST_60,
				TinkeringConstants.SKILL_MIN_LOCKPICKING_CHEST_60, typeof( GoldIngot ), TinkeringConstants.MSG_GOLD_INGOT,
				TinkeringConstants.RESOURCE_GOLD_LOCKPICKING_CHEST_60, TinkeringConstants.RESOURCE_GEMS_LOCKPICKING_CHEST_60 );

			AddLockpickingChest( typeof( LockpickingChest70 ), TinkeringStringConstants.ITEM_LOCKPICKING_CHEST_70,
				TinkeringConstants.SKILL_MIN_LOCKPICKING_CHEST_70, typeof( AgapiteIngot ), TinkeringConstants.MSG_AGAPITE_INGOT,
				TinkeringConstants.RESOURCE_AGAPITE_LOCKPICKING_CHEST_70, TinkeringConstants.RESOURCE_GEMS_LOCKPICKING_CHEST_70 );

			AddLockpickingChest( typeof( LockpickingChest80 ), TinkeringStringConstants.ITEM_LOCKPICKING_CHEST_80,
				TinkeringConstants.SKILL_MIN_LOCKPICKING_CHEST_80, typeof( VeriteIngot ), TinkeringConstants.MSG_VERITE_INGOT,
				TinkeringConstants.RESOURCE_VERITE_LOCKPICKING_CHEST_80, TinkeringConstants.RESOURCE_GEMS_LOCKPICKING_CHEST_80 );

			AddLockpickingChest( typeof( LockpickingChest90 ), TinkeringStringConstants.ITEM_LOCKPICKING_CHEST_90,
				TinkeringConstants.SKILL_MIN_LOCKPICKING_CHEST_90, typeof( ValoriteIngot ), TinkeringConstants.MSG_VALORITE_INGOT,
				TinkeringConstants.RESOURCE_VALORITE_LOCKPICKING_CHEST_90, TinkeringConstants.RESOURCE_GEMS_LOCKPICKING_CHEST_90 );
		}

		/// <summary>
		/// Adds all hospitality items (barrels and presses)
		/// </summary>
		private void AddHospitalityItems()
		{
			AddHospitalityItem( typeof( AleBarrel ), TinkeringStringConstants.ITEM_ALE_BARREL,
				TinkeringConstants.SKILL_MIN_ALE_BARREL, TinkeringConstants.SKILL_MAX_HOSPITALITY,
				TinkeringConstants.RESOURCE_GEMS_ALE_BARREL );

			AddHospitalityItem( typeof( CheesePress ), TinkeringStringConstants.ITEM_CHEESE_PRESS,
				TinkeringConstants.SKILL_MIN_CHEESE_PRESS, TinkeringConstants.SKILL_MAX_HOSPITALITY,
				TinkeringConstants.RESOURCE_GEMS_CHEESE_PRESS );

			AddHospitalityItem( typeof( CiderBarrel ), TinkeringStringConstants.ITEM_CIDER_BARREL,
				TinkeringConstants.SKILL_MIN_CIDER_BARREL, TinkeringConstants.SKILL_MAX_HOSPITALITY,
				TinkeringConstants.RESOURCE_GEMS_CIDER_BARREL );

			AddHospitalityItem( typeof( LiquorBarrel ), TinkeringStringConstants.ITEM_LIQUOR_BARREL,
				TinkeringConstants.SKILL_MIN_LIQUOR_BARREL, TinkeringConstants.SKILL_MAX_LIQUOR_BARREL,
				TinkeringConstants.RESOURCE_GEMS_LIQUOR_BARREL );

			AddHospitalityItem( typeof( WineBarrel ), TinkeringStringConstants.ITEM_WINE_BARREL,
				TinkeringConstants.SKILL_MIN_WINE_BARREL, TinkeringConstants.SKILL_MAX_HOSPITALITY,
				TinkeringConstants.RESOURCE_GEMS_WINE_BARREL );
		}

		/// <summary>
		/// Initializes metal type sub-resources for material selection
		/// </summary>
		private void InitializeMetalTypes()
		{
			SetSubRes( typeof( IronIngot ), TinkeringConstants.MSG_METAL_IRON );

			AddSubRes( typeof( IronIngot ), TinkeringConstants.MSG_METAL_IRON,
				TinkeringConstants.SKILL_REQ_METAL_IRON, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_NOT_ENOUGH_MATERIAL );

			AddSubRes( typeof( DullCopperIngot ), TinkeringConstants.MSG_METAL_DULL_COPPER,
				TinkeringConstants.SKILL_REQ_METAL_DULL_COPPER, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( ShadowIronIngot ), TinkeringConstants.MSG_METAL_SHADOW_IRON,
				TinkeringConstants.SKILL_REQ_METAL_SHADOW_IRON, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( CopperIngot ), TinkeringConstants.MSG_METAL_COPPER,
				TinkeringConstants.SKILL_REQ_METAL_COPPER, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( BronzeIngot ), TinkeringConstants.MSG_METAL_BRONZE,
				TinkeringConstants.SKILL_REQ_METAL_BRONZE, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( PlatinumIngot ), TinkeringConstants.MSG_METAL_PLATINUM,
				TinkeringConstants.SKILL_REQ_METAL_PLATINUM, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( GoldIngot ), TinkeringConstants.MSG_METAL_GOLD,
				TinkeringConstants.SKILL_REQ_METAL_GOLD, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( AgapiteIngot ), TinkeringConstants.MSG_METAL_AGAPITE,
				TinkeringConstants.SKILL_REQ_METAL_AGAPITE, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( VeriteIngot ), TinkeringConstants.MSG_METAL_VERITE,
				TinkeringConstants.SKILL_REQ_METAL_VERITE, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( ValoriteIngot ), TinkeringConstants.MSG_METAL_VALORITE,
				TinkeringConstants.SKILL_REQ_METAL_VALORITE, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( TitaniumIngot ), TinkeringConstants.MSG_METAL_TITANIUM,
				TinkeringConstants.SKILL_REQ_METAL_TITANIUM, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( RoseniumIngot ), TinkeringConstants.MSG_METAL_ROSENIUM,
				TinkeringConstants.SKILL_REQ_METAL_ROSENIUM, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			// GATED RESOURCES: These metals are not yet available to players
			/*
			AddSubRes( typeof( NepturiteIngot ), TinkeringConstants.MSG_METAL_NEPTURITE,
				TinkeringConstants.SKILL_REQ_METAL_NEPTURITE, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( ObsidianIngot ), TinkeringConstants.MSG_METAL_OBSIDIAN,
				TinkeringConstants.SKILL_REQ_METAL_OBSIDIAN, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( SteelIngot ), TinkeringConstants.MSG_METAL_STEEL,
				TinkeringConstants.SKILL_REQ_METAL_STEEL, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( BrassIngot ), TinkeringConstants.MSG_METAL_BRASS,
				TinkeringConstants.SKILL_REQ_METAL_BRASS, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( MithrilIngot ), TinkeringConstants.MSG_METAL_MITHRIL,
				TinkeringConstants.SKILL_REQ_METAL_MITHRIL, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( XormiteIngot ), TinkeringConstants.MSG_METAL_XORMITE,
				TinkeringConstants.SKILL_REQ_METAL_XORMITE, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );

			AddSubRes( typeof( DwarvenIngot ), TinkeringConstants.MSG_METAL_DWARVEN,
				TinkeringConstants.SKILL_REQ_METAL_DWARVEN, TinkeringConstants.MSG_IRON_INGOTS,
				TinkeringConstants.MSG_MATERIAL_SELECTION );
			*/
		}

		#endregion
	}

	#region TrapCraft Classes

	/// <summary>
	/// Base class for trap crafting
	/// </summary>
	public abstract class TrapCraft : CustomCraft
	{
		private LockableContainer m_Container;

		public LockableContainer Container{ get{ return m_Container; } }

		public abstract TrapType TrapType{ get; }

		public TrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality )
			: base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}

		/// <summary>
		/// Verifies that the container is valid for trapping
		/// </summary>
		private int Verify( LockableContainer container )
		{
			if ( container == null || container.KeyValue == 0 )
				return TinkeringConstants.MSG_ONLY_LOCKABLE_CHESTS;
			if ( From.Map != container.Map || !From.InRange( container.GetWorldLocation(), 2 ) )
				return TinkeringConstants.MSG_TOO_FAR_AWAY;
			if ( !container.Movable )
				return TinkeringConstants.MSG_CANNOT_TRAP_LOCKED_DOWN;
			if ( !container.IsAccessibleTo( From ) )
				return TinkeringConstants.MSG_BELONGS_TO_SOMEONE_ELSE;
			if ( container.Locked )
				return TinkeringConstants.MSG_ONLY_TRAP_UNLOCKED;
			if ( container.TrapType != TrapType.None )
				return TinkeringConstants.MSG_ONE_TRAP_AT_TIME;

			return 0;
		}

		/// <summary>
		/// Acquires and validates the target container
		/// </summary>
		private bool Acquire( object target, out int message )
		{
			LockableContainer container = target as LockableContainer;

			message = Verify( container );

			if ( message > 0 )
			{
				return false;
			}
			else
			{
				m_Container = container;
				return true;
			}
		}

		/// <summary>
		/// Prompts player to select a container to trap
		/// </summary>
		public override void EndCraftAction()
		{
			From.SendLocalizedMessage( TinkeringConstants.MSG_SET_TRAP_ON );
			From.Target = new ContainerTarget( this );
		}

		private class ContainerTarget : Target
		{
			private TrapCraft m_TrapCraft;

			public ContainerTarget( TrapCraft trapCraft ) : base( -1, false, TargetFlags.None )
			{
				m_TrapCraft = trapCraft;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				int message;

				if ( m_TrapCraft.Acquire( targeted, out message ) )
					m_TrapCraft.CraftItem.CompleteCraft( m_TrapCraft.Quality, false, m_TrapCraft.From,
						m_TrapCraft.CraftSystem, m_TrapCraft.TypeRes, m_TrapCraft.Tool, m_TrapCraft );
				else
					Failure( message );
			}

			protected override void OnTargetCancel( Mobile from, TargetCancelType cancelType )
			{
				if ( cancelType == TargetCancelType.Canceled )
					Failure( 0 );
			}

			private void Failure( int message )
			{
				Mobile from = m_TrapCraft.From;
				BaseTool tool = m_TrapCraft.Tool;

				if ( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
					from.SendGump( new CraftGump( from, m_TrapCraft.CraftSystem, tool, message ) );
				else if ( message > 0 )
					from.SendLocalizedMessage( message );
			}
		}

		/// <summary>
		/// Completes the trap craft by applying the trap to the container
		/// </summary>
		public override Item CompleteCraft( out int message )
		{
			message = Verify( this.Container );

			if ( message == 0 )
			{
				int trapLevel = (int)(From.Skills.Tinkering.Value / TinkeringConstants.TRAP_LEVEL_DIVISOR);

				Container.TrapType = this.TrapType;
				Container.TrapPower = trapLevel * TinkeringConstants.TRAP_POWER_MULTIPLIER;
				Container.TrapLevel = trapLevel;
				Container.TrapOnLockpick = true;

				message = TinkeringConstants.MSG_TRAP_DISABLED;
			}

			return null;
		}
	}

	/// <summary>
	/// Dart trap craft
	/// </summary>
	[CraftItemID( 0x1BFC )]
	public class DartTrapCraft : TrapCraft
	{
		public override TrapType TrapType{ get{ return TrapType.DartTrap; } }

		public DartTrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality )
			: base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}
	}

	/// <summary>
	/// Poison trap craft
	/// </summary>
	[CraftItemID( 0x113E )]
	public class PoisonTrapCraft : TrapCraft
	{
		public override TrapType TrapType{ get{ return TrapType.PoisonTrap; } }

		public PoisonTrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality )
			: base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}
	}

	/// <summary>
	/// Explosion trap craft
	/// </summary>
	[CraftItemID( 0x370C )]
	public class ExplosionTrapCraft : TrapCraft
	{
		public override TrapType TrapType{ get{ return TrapType.ExplosionTrap; } }

		public ExplosionTrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality )
			: base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}
	}

	#endregion
}

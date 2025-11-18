using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Engines.Craft;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Blacksmithy crafting system for creating weapons, armor, and tools from metal ingots
	/// </summary>
	public class DefBlacksmithy : CraftSystem
	{
		#region Constants

		/// <summary>Minimum chance at minimum skill (0%)</summary>
		private const double CHANCE_AT_MIN = 0.0;

		#endregion

		#region Fields

		private static CraftSystem m_CraftSystem;
		private static Type typeofAnvil = typeof( AnvilAttribute );
		private static Type typeofForge = typeof( ForgeAttribute );

		#endregion

		#region Properties

		/// <summary>
		/// Gets the main skill required for blacksmithy
		/// </summary>
		public override SkillName MainSkill
		{
			get { return SkillName.Blacksmith; }
		}

		/// <summary>
		/// Gets the gump title number for the blacksmithy menu
		/// </summary>
		public override int GumpTitleNumber
		{
			get { return BlacksmithyConstants.MSG_GUMP_TITLE; }
		}

		/// <summary>
		/// Gets the singleton instance of the blacksmithy craft system
		/// </summary>
		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefBlacksmithy();

				return m_CraftSystem;
			}
		}

		/// <summary>
		/// Gets the exception chance adjustment type for blacksmithy
		/// </summary>
		public override CraftECA ECA
		{
			get { return CraftECA.ChanceMinusSixtyToFourtyFive; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DefBlacksmithy class
		/// </summary>
		private DefBlacksmithy() : base( BlacksmithyConstants.MIN_CRAFT_EFFECT, BlacksmithyConstants.MAX_CRAFT_EFFECT, BlacksmithyConstants.CRAFT_DELAY )
		{
		}

		#endregion

		#region Craft System Overrides

		/// <summary>
		/// Gets the chance of success at minimum skill level
		/// </summary>
		/// <param name="item">The craft item</param>
		/// <returns>0.0 (0% chance at minimum skill)</returns>
		public override double GetChanceAtMin( CraftItem item )
		{
			return CHANCE_AT_MIN;
		}

		#endregion

		#region Anvil and Forge Detection

		/// <summary>
		/// Checks if an item ID represents an anvil
		/// </summary>
		/// <param name="itemID">The item ID to check</param>
		/// <returns>True if the item ID is an anvil</returns>
		private static bool IsAnvilItemID( int itemID )
		{
			return itemID == BlacksmithyConstants.ANVIL_ITEM_ID_1
				|| itemID == BlacksmithyConstants.ANVIL_ITEM_ID_2
				|| itemID == BlacksmithyConstants.ANVIL_ITEM_ID_3
				|| itemID == BlacksmithyConstants.ANVIL_ITEM_ID_4
				|| itemID == BlacksmithyConstants.ANVIL_ITEM_ID_5
				|| itemID == BlacksmithyConstants.ANVIL_ITEM_ID_6
				|| itemID == BlacksmithyConstants.ANVIL_ITEM_ID_7;
		}

		/// <summary>
		/// Checks if an item ID represents a forge
		/// </summary>
		/// <param name="itemID">The item ID to check</param>
		/// <returns>True if the item ID is a forge</returns>
		private static bool IsForgeItemID( int itemID )
		{
			if ( itemID == BlacksmithyConstants.FORGE_ITEM_ID )
				return true;

			if ( itemID >= BlacksmithyConstants.FORGE_RANGE_1_START && itemID <= BlacksmithyConstants.FORGE_RANGE_1_END )
				return true;

			if ( itemID >= BlacksmithyConstants.FORGE_RANGE_2_START && itemID <= BlacksmithyConstants.FORGE_RANGE_2_END )
				return true;

			if ( itemID >= BlacksmithyConstants.FORGE_RANGE_3_START && itemID <= BlacksmithyConstants.FORGE_RANGE_3_END )
				return true;

			if ( itemID == BlacksmithyConstants.FORGE_ITEM_ID_2 )
				return true;

			if ( itemID >= BlacksmithyConstants.FORGE_RANGE_4_START && itemID <= BlacksmithyConstants.FORGE_RANGE_4_END )
				return true;

			return false;
		}

		/// <summary>
		/// Checks if an item ID is in the Fire Giant Forge range
		/// </summary>
		/// <param name="itemID">The item ID to check</param>
		/// <returns>True if the item ID is in the Fire Giant Forge range</returns>
		private static bool IsFireGiantForgeRange( int itemID )
		{
			return itemID >= BlacksmithyConstants.FIRE_GIANT_FORGE_MIN && itemID <= BlacksmithyConstants.FIRE_GIANT_FORGE_MAX;
		}

		/// <summary>
		/// Checks if an object is a Fire Giant Forge and consumes its charge
		/// </summary>
		/// <param name="obj">The object to check</param>
		/// <returns>True if the object is a Fire Giant Forge</returns>
		private static bool CheckFireGiantForge( object obj )
		{
			if ( obj is FireGiantForge )
			{
				FireGiantForge kettle = (FireGiantForge)obj;
				Server.Items.FireGiantForge.ConsumeCharge( kettle );
				return true;
			}

			return false;
		}

		/// <summary>
		/// Determines if an object is a forge
		/// </summary>
		/// <param name="obj">The object to check</param>
		/// <returns>True if the object is a forge</returns>
		public static bool IsForge( object obj )
		{
			if ( Core.ML && obj is Mobile && ((Mobile)obj).IsDeadBondedPet )
				return false;

			if ( obj.GetType().IsDefined( typeofForge, false ) )
				return true;

			int itemID = 0;

			if ( obj is Item )
				itemID = ((Item)obj).ItemID;
			else if ( obj is StaticTarget )
				itemID = ((StaticTarget)obj).ItemID;

			if ( IsFireGiantForgeRange( itemID ) )
			{
				return CheckFireGiantForge( obj );
			}

			return IsForgeItemID( itemID );
		}

		/// <summary>
		/// Checks items in range for anvil and forge
		/// </summary>
		/// <param name="from">The mobile to check from</param>
		/// <param name="range">The range to check</param>
		/// <param name="anvil">Output parameter indicating if anvil was found</param>
		/// <param name="forge">Output parameter indicating if forge was found</param>
		private static void CheckItemsInRange( Mobile from, int range, ref bool anvil, ref bool forge )
		{
			Map map = from.Map;
			if ( map == null )
				return;

			IPooledEnumerable eable = map.GetItemsInRange( from.Location, range );

			foreach ( Item item in eable )
			{
				Type type = item.GetType();

				bool isAnvil = type.IsDefined( typeofAnvil, false ) || IsAnvilItemID( item.ItemID );
				bool isForge = type.IsDefined( typeofForge, false ) || IsForgeItemID( item.ItemID );

				if ( isAnvil || isForge )
				{
					if ( (from.Z + BlacksmithyConstants.Z_OFFSET) < item.Z || (item.Z + BlacksmithyConstants.Z_OFFSET) < from.Z || !from.InLOS( item ) )
						continue;

					anvil = anvil || isAnvil;
					forge = forge || isForge;

					if ( anvil && forge )
						break;
				}
			}

			eable.Free();
		}

		/// <summary>
		/// Checks static tiles for anvil and forge
		/// </summary>
		/// <param name="from">The mobile to check from</param>
		/// <param name="map">The map to check</param>
		/// <param name="range">The range to check</param>
		/// <param name="anvil">Output parameter indicating if anvil was found</param>
		/// <param name="forge">Output parameter indicating if forge was found</param>
		private static void CheckStaticTiles( Mobile from, Map map, int range, ref bool anvil, ref bool forge )
		{
			for ( int x = -range; (!anvil || !forge) && x <= range; ++x )
			{
				for ( int y = -range; (!anvil || !forge) && y <= range; ++y )
				{
					StaticTile[] tiles = map.Tiles.GetStaticTiles( from.X + x, from.Y + y, true );

					for ( int i = 0; (!anvil || !forge) && i < tiles.Length; ++i )
					{
						int id = tiles[i].ID;

						bool isAnvil = IsAnvilItemID( id );
						bool isForge = IsForgeItemID( id );

						if ( isAnvil || isForge )
						{
							int tileZ = tiles[i].Z;
							Point3D tileLocation = new Point3D( from.X + x, from.Y + y, tileZ + (tiles[i].Height / BlacksmithyConstants.TILE_HEIGHT_OFFSET) + BlacksmithyConstants.TILE_HEIGHT_OFFSET );

							if ( (from.Z + BlacksmithyConstants.Z_OFFSET) < tileZ || (tileZ + BlacksmithyConstants.Z_OFFSET) < from.Z || !from.InLOS( tileLocation ) )
								continue;

							anvil = anvil || isAnvil;
							forge = forge || isForge;
						}
					}
				}
			}
		}

		/// <summary>
		/// Checks special locations (Skara Brae) for anvil and forge
		/// </summary>
		/// <param name="from">The mobile to check</param>
		/// <param name="anvil">Output parameter indicating if anvil was found</param>
		/// <param name="forge">Output parameter indicating if forge was found</param>
		private static void CheckSpecialLocations( Mobile from, ref bool anvil, ref bool forge )
		{
			if ( from.Map == Map.Felucca )
			{
				// Skara Brae Area 1
				if ( from.X >= BlacksmithyConstants.SKARA_BRAE_AREA1_X_MIN && from.X <= BlacksmithyConstants.SKARA_BRAE_AREA1_X_MAX
					&& from.Y >= BlacksmithyConstants.SKARA_BRAE_AREA1_Y_MIN && from.Y <= BlacksmithyConstants.SKARA_BRAE_AREA1_Y_MAX )
				{
					anvil = true;
					forge = true;
					return;
				}

				// Skara Brae Area 2
				if ( from.X >= BlacksmithyConstants.SKARA_BRAE_AREA2_X_MIN && from.X <= BlacksmithyConstants.SKARA_BRAE_AREA2_X_MAX
					&& from.Y >= BlacksmithyConstants.SKARA_BRAE_AREA2_Y_MIN && from.Y <= BlacksmithyConstants.SKARA_BRAE_AREA2_Y_MAX )
				{
					anvil = true;
					forge = true;
				}
			}
		}

		/// <summary>
		/// Checks if the mobile is near both an anvil and a forge
		/// </summary>
		/// <param name="from">The mobile to check</param>
		/// <param name="range">The range to check</param>
		/// <param name="anvil">Output parameter indicating if anvil was found</param>
		/// <param name="forge">Output parameter indicating if forge was found</param>
		public static void CheckAnvilAndForge( Mobile from, int range, out bool anvil, out bool forge )
		{
			anvil = false;
			forge = false;

			Map map = from.Map;
			if ( map == null )
				return;

			CheckItemsInRange( from, range, ref anvil, ref forge );

			if ( !anvil || !forge )
			{
				CheckStaticTiles( from, map, range, ref anvil, ref forge );
			}

			if ( !anvil || !forge )
			{
				CheckSpecialLocations( from, ref anvil, ref forge );
			}
		}

		#endregion

		/// <summary>
		/// Checks if the mobile can craft with the given tool
		/// </summary>
		/// <param name="from">The mobile attempting to craft</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="itemType">The type of item being crafted</param>
		/// <returns>0 if crafting is allowed, otherwise a localized message number</returns>
		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return BlacksmithyConstants.MSG_TOOL_WORN_OUT;
			else if ( !BaseTool.CheckTool( tool, from ) )
				return BlacksmithyConstants.MSG_MUST_USE_EQUIPPED_TOOL;
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return BlacksmithyConstants.MSG_TOOL_MUST_BE_ON_PERSON;

			bool anvil, forge;

			CheckAnvilAndForge( from, BlacksmithyConstants.CHECK_RANGE, out anvil, out forge );

			if ( anvil && forge )
				return 0;

			return BlacksmithyConstants.MSG_MUST_BE_NEAR_ANVIL_AND_FORGE;
		}

		/// <summary>
		/// Plays the craft effect animation and sound
		/// </summary>
		/// <param name="from">The mobile crafting</param>
		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( BlacksmithyConstants.SOUND_BLACKSMITH );
		}

		/// <summary>
		/// Plays the ending effect message based on crafting result
		/// </summary>
		/// <param name="from">The mobile who crafted</param>
		/// <param name="failed">Whether the craft failed</param>
		/// <param name="lostMaterial">Whether materials were lost</param>
		/// <param name="toolBroken">Whether the tool broke</param>
		/// <param name="quality">The quality level (0=below average, 1=average, 2=exceptional)</param>
		/// <param name="makersMark">Whether a maker's mark was applied</param>
		/// <param name="item">The craft item</param>
		/// <returns>The localized message number for the result</returns>
		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( BlacksmithyConstants.MSG_TOOL_WORN_OUT );

			if ( failed )
			{
				if ( lostMaterial )
					return BlacksmithyConstants.MSG_FAILED_WITH_MATERIAL_LOSS;
				else
					return BlacksmithyConstants.MSG_FAILED_WITHOUT_MATERIAL_LOSS;
			}
			else
			{
				if ( quality == 0 )
					return BlacksmithyConstants.MSG_BELOW_AVERAGE_QUALITY;
				else if ( makersMark && quality == 2 )
					return BlacksmithyConstants.MSG_EXCEPTIONAL_WITH_MARK;
				else if ( quality == 2 )
					return BlacksmithyConstants.MSG_EXCEPTIONAL_QUALITY;
				else
					return BlacksmithyConstants.MSG_ITEM_CREATED;
			}
		}

		#region Craft List Initialization

		/// <summary>
		/// Adds Royal armor skills (ArmsLore and Magery) to a craft item
		/// </summary>
		/// <param name="index">The craft item index</param>
		private void AddRoyalArmorSkills( int index )
		{
			AddSkill( index, SkillName.ArmsLore, 100.0, 120.0 );
			AddSkill( index, SkillName.Magery, 70.0, 110.0 );
		}

		/// <summary>
		/// Adds Dragon Scale armor skills (ArmsLore and Magery) to a craft item
		/// </summary>
		/// <param name="index">The craft item index</param>
		private void AddDragonScaleArmorSkills( int index )
		{
			AddSkill( index, SkillName.ArmsLore, 100.0, 120.0 );
			AddSkill( index, SkillName.Magery, 80.0, 110.0 );
		}

		/// <summary>
		/// Initializes the list of craftable items for blacksmithy
		/// </summary>
		public override void InitCraftList()
		{
			string chainRingTitle = BlacksmithyStringConstants.GROUP_CHAIN_RING;

            #region Ringmail
            AddCraft(typeof(RingmailGloves), chainRingTitle, BlacksmithyStringConstants.ITEM_RINGMAIL_GLOVES, 32.0, 62.0, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 8, BlacksmithyConstants.MSG_MISSING_RESOURCE);
            AddCraft(typeof(RingmailLegs), chainRingTitle, BlacksmithyStringConstants.ITEM_RINGMAIL_LEGS, 40.4, 69.4, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 12, BlacksmithyConstants.MSG_MISSING_RESOURCE);
            AddCraft(typeof(RingmailArms), chainRingTitle, BlacksmithyStringConstants.ITEM_RINGMAIL_ARMS, 36.9, 66.9, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 10, BlacksmithyConstants.MSG_MISSING_RESOURCE);
            AddCraft(typeof(RingmailChest), chainRingTitle, BlacksmithyStringConstants.ITEM_RINGMAIL_CHEST, 44.9, 74.9, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 14, BlacksmithyConstants.MSG_MISSING_RESOURCE);
            #endregion

            #region Chainmail
            AddCraft( typeof( ChainCoif ), chainRingTitle, BlacksmithyStringConstants.ITEM_CHAIN_COIF, 34.5, 64.5, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 10, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( ChainLegs ), chainRingTitle, BlacksmithyStringConstants.ITEM_CHAIN_LEGS, 46.7, 86.7, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 14, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( ChainChest ), chainRingTitle, BlacksmithyStringConstants.ITEM_CHAIN_CHEST, 49.1, 89.1, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 16, BlacksmithyConstants.MSG_MISSING_RESOURCE );
            #endregion


			int index = -1;

            #region Platemail
            string platemailTitle = BlacksmithyStringConstants.GROUP_PLATEMAIL;
            AddCraft( typeof( PlateArms ), platemailTitle, BlacksmithyStringConstants.ITEM_PLATE_ARMS, 66.3, 116.3, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 18, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( PlateGloves ), platemailTitle, BlacksmithyStringConstants.ITEM_PLATE_GLOVES, 58.9, 108.9, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 12, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( PlateGorget ), platemailTitle, BlacksmithyStringConstants.ITEM_PLATE_GORGET, 56.4, 106.4, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 10, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( PlateLegs ), platemailTitle, BlacksmithyStringConstants.ITEM_PLATE_LEGS, 70.8, 118.8, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 20, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( PlateChest ), platemailTitle, BlacksmithyStringConstants.ITEM_PLATE_CHEST, 75.0, 125.0, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 25, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( FemalePlateChest ), platemailTitle, BlacksmithyStringConstants.ITEM_FEMALE_PLATE_CHEST, 64.1, 104.1, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 22, BlacksmithyConstants.MSG_MISSING_RESOURCE );

			index = AddCraft( typeof( HorseArmor ), platemailTitle, BlacksmithyStringConstants.ITEM_HORSE_ARMOR, 90.0, 100.1, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 250, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddSkill( index, SkillName.ArmsLore, 100.0, 120.0 );

            #endregion

            #region Royal
            string royalTitle = BlacksmithyStringConstants.GROUP_ROYAL;

            index = AddCraft( typeof( RoyalGloves ), royalTitle, BlacksmithyStringConstants.ITEM_ROYAL_GLOVES, 70.0, 140.1, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 12, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddRoyalArmorSkills( index );
            index = AddCraft(typeof(RoyalGorget), royalTitle, BlacksmithyStringConstants.ITEM_ROYAL_GORGET, 72.4, 140.1, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 8, BlacksmithyConstants.MSG_MISSING_RESOURCE);
			AddRoyalArmorSkills( index );
            index = AddCraft( typeof( RoyalHelm ), royalTitle, BlacksmithyStringConstants.ITEM_ROYAL_HELM, 75.0, 140.1, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 15, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddRoyalArmorSkills( index );
            index = AddCraft( typeof( RoyalsLegs ), royalTitle, BlacksmithyStringConstants.ITEM_ROYAL_LEGS, 80.0, 140.1, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 20, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddRoyalArmorSkills( index );
            index = AddCraft(typeof(RoyalBoots), royalTitle, BlacksmithyStringConstants.ITEM_ROYAL_BOOTS, 82.9, 140.1, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 9, BlacksmithyConstants.MSG_MISSING_RESOURCE);
			AddRoyalArmorSkills( index );
            index = AddCraft( typeof( RoyalArms ), royalTitle, BlacksmithyStringConstants.ITEM_ROYAL_ARMS, 85.0, 140.1, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 18, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddRoyalArmorSkills( index );
            index = AddCraft( typeof( RoyalChest ), royalTitle, BlacksmithyStringConstants.ITEM_ROYAL_CHEST, 90.0, 140.1, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 24, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddRoyalArmorSkills( index );
            #endregion

            #region Dragon Scale Armor

            string scalemailTitle = BlacksmithyStringConstants.GROUP_SCALEMAIL;

			index = AddCraft( typeof( DragonGloves ), scalemailTitle, BlacksmithyStringConstants.ITEM_DRAGON_GLOVES, 70.0, 150.1, typeof( RedScales ), BlacksmithyStringConstants.RESOURCE_DRAGON_SCALES, 12, BlacksmithyConstants.MSG_DRAGON_SCALES );
            AddRes(index, typeof(PlatinumIngot), BlacksmithyStringConstants.RESOURCE_PLATINUM_INGOTS, 6, BlacksmithyConstants.MSG_DRAGON_SCALES);
			AddDragonScaleArmorSkills( index );
            SetUseSubRes2( index, true );

			index = AddCraft( typeof( DragonHelm ), scalemailTitle, BlacksmithyStringConstants.ITEM_DRAGON_HELM, 75.0, 150.1, typeof( RedScales ), BlacksmithyStringConstants.RESOURCE_DRAGON_SCALES, 16, BlacksmithyConstants.MSG_DRAGON_SCALES );
            AddRes(index, typeof(PlatinumIngot), BlacksmithyStringConstants.RESOURCE_PLATINUM_INGOTS, 8, BlacksmithyConstants.MSG_DRAGON_SCALES);
			AddDragonScaleArmorSkills( index );
            SetUseSubRes2( index, true );

			index = AddCraft( typeof( DragonLegs ), scalemailTitle, BlacksmithyStringConstants.ITEM_DRAGON_LEGS, 80.0, 150.1, typeof( RedScales ), BlacksmithyStringConstants.RESOURCE_DRAGON_SCALES, 20, BlacksmithyConstants.MSG_DRAGON_SCALES );
            AddRes(index, typeof(PlatinumIngot), BlacksmithyStringConstants.RESOURCE_PLATINUM_INGOTS, 12, BlacksmithyConstants.MSG_DRAGON_SCALES);
			AddDragonScaleArmorSkills( index );
            SetUseSubRes2( index, true );

			index = AddCraft( typeof( DragonArms ), scalemailTitle, BlacksmithyStringConstants.ITEM_DRAGON_ARMS, 85.0, 150.1, typeof( RedScales ), BlacksmithyStringConstants.RESOURCE_DRAGON_SCALES, 18, BlacksmithyConstants.MSG_DRAGON_SCALES );
            AddRes(index, typeof(PlatinumIngot), BlacksmithyStringConstants.RESOURCE_PLATINUM_INGOTS, 10, BlacksmithyConstants.MSG_DRAGON_SCALES);
			AddDragonScaleArmorSkills( index );
            SetUseSubRes2( index, true );

			index = AddCraft( typeof( DragonChest ), scalemailTitle, BlacksmithyStringConstants.ITEM_DRAGON_CHEST, 90.0, 150.1, typeof( RedScales ), BlacksmithyStringConstants.RESOURCE_DRAGON_SCALES, 24, BlacksmithyConstants.MSG_DRAGON_SCALES );
            AddRes(index, typeof(PlatinumIngot), BlacksmithyStringConstants.RESOURCE_PLATINUM_INGOTS, 14, BlacksmithyConstants.MSG_DRAGON_SCALES);
			AddDragonScaleArmorSkills( index );
            SetUseSubRes2( index, true );
            #endregion

            #region Helmets
            string helmetsTitle = BlacksmithyStringConstants.GROUP_HELMETS;

            AddCraft( typeof( Bascinet ), helmetsTitle, BlacksmithyStringConstants.ITEM_BASCINET, 28.3, 58.3, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 11, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( CloseHelm ), helmetsTitle, BlacksmithyStringConstants.ITEM_CLOSE_HELM, 57.9, 87.9, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 14, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( Helmet ), helmetsTitle, BlacksmithyStringConstants.ITEM_HELMET, 37.9, 87.9, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 12, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( NorseHelm ), helmetsTitle, BlacksmithyStringConstants.ITEM_NORSE_HELM, 47.9, 87.9, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 14, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( PlateHelm ), helmetsTitle, BlacksmithyStringConstants.ITEM_PLATE_HELM, 62.6, 112.6, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 16, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( DreadHelm ), helmetsTitle, BlacksmithyStringConstants.ITEM_DREAD_HELM, 62.6, 112.6, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 15, BlacksmithyConstants.MSG_MISSING_RESOURCE );

            #endregion

            #region Shields

            string shieldsTitle = BlacksmithyStringConstants.GROUP_SHIELDS;

            AddCraft( typeof( Buckler ), shieldsTitle, BlacksmithyStringConstants.ITEM_BUCKLER, 0.0, 45.0, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 10, BlacksmithyConstants.MSG_MISSING_RESOURCE );
            AddCraft(typeof( MetalShield ), shieldsTitle, BlacksmithyStringConstants.ITEM_METAL_SHIELD, 15.2, 49.8, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 12, BlacksmithyConstants.MSG_MISSING_RESOURCE);
            AddCraft(typeof( WoodenKiteShield), shieldsTitle, BlacksmithyStringConstants.ITEM_WOODEN_KITE_SHIELD, 20.2, 64.8, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 9, BlacksmithyConstants.MSG_MISSING_RESOURCE);
            AddCraft( typeof( BronzeShield ), shieldsTitle, BlacksmithyStringConstants.ITEM_BRONZE_SHIELD, 35.2, 69.8, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 14, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( MetalKiteShield ), shieldsTitle, BlacksmithyStringConstants.ITEM_METAL_KITE_SHIELD, 54.6, 85.6, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 16, BlacksmithyConstants.MSG_MISSING_RESOURCE );
            AddCraft(typeof(HeaterShield), shieldsTitle, BlacksmithyStringConstants.ITEM_HEATER_SHIELD, 65.3, 89.3, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 18, BlacksmithyConstants.MSG_MISSING_RESOURCE);

            if (Core.AOS)
            {
                AddCraft(typeof(ChaosShield), shieldsTitle, BlacksmithyStringConstants.ITEM_CHAOS_SHIELD, 85.0, 125.0, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 25, BlacksmithyConstants.MSG_MISSING_RESOURCE);
                AddCraft(typeof(OrderShield), shieldsTitle, BlacksmithyStringConstants.ITEM_ORDER_SHIELD, 85.0, 125.0, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 25, BlacksmithyConstants.MSG_MISSING_RESOURCE);
            }


            #endregion

            #region Bladed
            string bladesTitle = BlacksmithyStringConstants.GROUP_BLADES;

            AddCraft(typeof(Dagger), bladesTitle, BlacksmithyStringConstants.ITEM_DAGGER, 0, 49.6, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 3, BlacksmithyConstants.MSG_MISSING_RESOURCE);
            AddCraft(typeof(Cutlass), bladesTitle, BlacksmithyStringConstants.ITEM_CUTLASS, 24.3, 64.3, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 8, BlacksmithyConstants.MSG_MISSING_RESOURCE);
            AddCraft(typeof(Scimitar), bladesTitle, BlacksmithyStringConstants.ITEM_SCIMITAR, 31.7, 71.7, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 10, BlacksmithyConstants.MSG_MISSING_RESOURCE);

            AddCraft(typeof(Kryss), bladesTitle, BlacksmithyStringConstants.ITEM_KRYSS, 36.7, 88.7, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 8, BlacksmithyConstants.MSG_MISSING_RESOURCE);
            AddCraft(typeof(Katana), bladesTitle, BlacksmithyStringConstants.ITEM_KATANA, 42.1, 90.1, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 9, BlacksmithyConstants.MSG_MISSING_RESOURCE);

            AddCraft( typeof( VikingSword ), bladesTitle, BlacksmithyStringConstants.ITEM_VIKING_SWORD, 48.3, 84.3, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 14, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( Broadsword ), bladesTitle, BlacksmithyStringConstants.ITEM_BROADSWORD, 55.4, 95.4, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 11, BlacksmithyConstants.MSG_MISSING_RESOURCE );

			AddCraft( typeof( Longsword ), bladesTitle, BlacksmithyStringConstants.ITEM_LONGSWORD, 59.0, 88.0, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 14, BlacksmithyConstants.MSG_MISSING_RESOURCE );

            #endregion

            #region Axes
            string axeTitle = BlacksmithyStringConstants.GROUP_AXES;
            
            AddCraft( typeof( Hatchet ), axeTitle, BlacksmithyStringConstants.ITEM_HATCHET, 24.2, 64.2, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 10, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( LumberAxe ), axeTitle, BlacksmithyStringConstants.ITEM_LUMBER_AXE, 24.2, 64.2, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 10, BlacksmithyConstants.MSG_MISSING_RESOURCE );
            
            AddCraft( typeof( BattleAxe ), axeTitle, BlacksmithyStringConstants.ITEM_BATTLE_AXE, 30.5, 70.5, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 11, BlacksmithyConstants.MSG_MISSING_RESOURCE );
            AddCraft(typeof(Axe), axeTitle, BlacksmithyStringConstants.ITEM_AXE, 35.2, 74.2, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 12, BlacksmithyConstants.MSG_MISSING_RESOURCE);

            AddCraft(typeof(TwoHandedAxe), axeTitle, BlacksmithyStringConstants.ITEM_TWO_HANDED_AXE, 63.0, 83.0, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 16, BlacksmithyConstants.MSG_MISSING_RESOURCE);

            AddCraft(typeof(WarAxe), axeTitle, BlacksmithyStringConstants.ITEM_WAR_AXE, 69.1, 89.1, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 13, BlacksmithyConstants.MSG_MISSING_RESOURCE);
            AddCraft( typeof( DoubleAxe ), axeTitle, BlacksmithyStringConstants.ITEM_DOUBLE_AXE, 39.3, 79.3, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 14, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( ExecutionersAxe ), axeTitle, BlacksmithyStringConstants.ITEM_EXECUTIONERS_AXE, 44.2, 84.2, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 14, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( LargeBattleAxe ), axeTitle, BlacksmithyStringConstants.ITEM_LARGE_BATTLE_AXE, 58.0, 88.0, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 14, BlacksmithyConstants.MSG_MISSING_RESOURCE );


            #endregion

            #region Pole Arms
            string poleArmsTitle = BlacksmithyStringConstants.GROUP_POLE_ARMS;

            AddCraft(typeof(Harpoon), poleArmsTitle, BlacksmithyStringConstants.ITEM_HARPOON, 30.0, 70.0, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 10, BlacksmithyConstants.MSG_HARPOON_RESOURCE_ERROR);

            AddCraft(typeof(ShortSpear), poleArmsTitle, BlacksmithyStringConstants.ITEM_SHORT_SPEAR, 45.3, 85.3, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 8, BlacksmithyConstants.MSG_MISSING_RESOURCE);
            AddCraft(typeof(Spear), poleArmsTitle, BlacksmithyStringConstants.ITEM_SPEAR, 55.0, 90.0, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 12, BlacksmithyConstants.MSG_MISSING_RESOURCE);
            if (Core.AOS)
                AddCraft(typeof(Pike), poleArmsTitle, BlacksmithyStringConstants.ITEM_PIKE, 47.0, 87.0, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 10, BlacksmithyConstants.MSG_MISSING_RESOURCE);

            AddCraft(typeof(WarFork), poleArmsTitle, BlacksmithyStringConstants.ITEM_WAR_FORK, 52.9, 92.9, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 12, BlacksmithyConstants.MSG_MISSING_RESOURCE);

            AddCraft( typeof( Bardiche ), poleArmsTitle, BlacksmithyStringConstants.ITEM_BARDICHE, 31.7, 85.7, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 16, BlacksmithyConstants.MSG_MISSING_RESOURCE );
            AddCraft(typeof(Halberd), poleArmsTitle, BlacksmithyStringConstants.ITEM_HALBERD, 39.1, 89.1, typeof(IronIngot), BlacksmithyConstants.MSG_IRON_INGOT, 16, BlacksmithyConstants.MSG_MISSING_RESOURCE);


            // Not craftable (is this an AOS change ??)

            #endregion

            #region Bashing
            string bashingTitle = BlacksmithyStringConstants.GROUP_BASHING;

            AddCraft( typeof( HammerPick ), bashingTitle, BlacksmithyStringConstants.ITEM_HAMMER_PICK, 34.2, 84.2, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 12, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( Mace ), bashingTitle, BlacksmithyStringConstants.ITEM_MACE, 14.5, 64.5, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 8, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( Maul ), bashingTitle, BlacksmithyStringConstants.ITEM_MAUL, 19.4, 69.4, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 10, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( WarMace ), bashingTitle, BlacksmithyStringConstants.ITEM_WAR_MACE, 28.0, 78.0, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 14, BlacksmithyConstants.MSG_MISSING_RESOURCE );
			AddCraft( typeof( WarHammer ), bashingTitle, BlacksmithyStringConstants.ITEM_WAR_HAMMER, 34.2, 84.2, typeof( IronIngot ), BlacksmithyConstants.MSG_IRON_INGOT, 13, BlacksmithyConstants.MSG_MISSING_RESOURCE );


            #endregion

			// Set the overridable material
			SetSubRes( typeof( IronIngot ), BlacksmithyStringConstants.INGOT_IRON );

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
			AddSubRes( typeof( IronIngot ), BlacksmithyStringConstants.INGOT_IRON, 00.0, BlacksmithyConstants.MSG_SUB_RESOURCE_ERROR );
			AddSubRes( typeof( DullCopperIngot ), BlacksmithyStringConstants.INGOT_DULL_COPPER, 65.0, BlacksmithyConstants.MSG_SUB_RESOURCE_SKILL_ERROR );
			AddSubRes( typeof( CopperIngot ), BlacksmithyStringConstants.INGOT_COPPER, 70.0, BlacksmithyConstants.MSG_SUB_RESOURCE_SKILL_ERROR );
			AddSubRes( typeof( BronzeIngot ), BlacksmithyStringConstants.INGOT_BRONZE, 75.0, BlacksmithyConstants.MSG_SUB_RESOURCE_SKILL_ERROR );
			AddSubRes( typeof( ShadowIronIngot ), BlacksmithyStringConstants.INGOT_SHADOW_IRON, 80.0, BlacksmithyConstants.MSG_SUB_RESOURCE_SKILL_ERROR );
			AddSubRes( typeof( PlatinumIngot ), BlacksmithyStringConstants.INGOT_PLATINUM, 85.0, BlacksmithyConstants.MSG_SUB_RESOURCE_SKILL_ERROR );
			AddSubRes( typeof( GoldIngot ), BlacksmithyStringConstants.INGOT_GOLD, 85.0, BlacksmithyConstants.MSG_SUB_RESOURCE_SKILL_ERROR );
			AddSubRes( typeof( AgapiteIngot ), BlacksmithyStringConstants.INGOT_AGAPITE, 90.0, BlacksmithyConstants.MSG_SUB_RESOURCE_SKILL_ERROR );
			AddSubRes( typeof( VeriteIngot ), BlacksmithyStringConstants.INGOT_VERITE, 95.0, BlacksmithyConstants.MSG_SUB_RESOURCE_SKILL_ERROR );
			AddSubRes( typeof( ValoriteIngot ), BlacksmithyStringConstants.INGOT_VALORITE, 95.0, BlacksmithyConstants.MSG_SUB_RESOURCE_SKILL_ERROR );
			AddSubRes( typeof( TitaniumIngot ), BlacksmithyStringConstants.INGOT_TITANIUM, 100.0, BlacksmithyConstants.MSG_SUB_RESOURCE_SKILL_ERROR );
			AddSubRes( typeof( RoseniumIngot ), BlacksmithyStringConstants.INGOT_ROSENIUM, 100.0, BlacksmithyConstants.MSG_SUB_RESOURCE_SKILL_ERROR );

			SetSubRes2( typeof( RedScales ), BlacksmithyConstants.MSG_RED_SCALES_NAME );

			AddSubRes2( typeof( RedScales ),		BlacksmithyConstants.MSG_RED_SCALES_NAME, 0.0, BlacksmithyConstants.MSG_SCALE_RESOURCE_ERROR, BlacksmithyConstants.MSG_SCALE_SKILL_ERROR );
			AddSubRes2( typeof(YellowScales),		BlacksmithyConstants.MSG_YELLOW_SCALES, 0.0, BlacksmithyConstants.MSG_SCALE_RESOURCE_ERROR, BlacksmithyConstants.MSG_SCALE_SKILL_ERROR );
			AddSubRes2( typeof( BlackScales ),		BlacksmithyConstants.MSG_BLACK_SCALES, 0.0, BlacksmithyConstants.MSG_SCALE_RESOURCE_ERROR, BlacksmithyConstants.MSG_SCALE_SKILL_ERROR );
			AddSubRes2( typeof( GreenScales ),		BlacksmithyConstants.MSG_GREEN_SCALES, 0.0, BlacksmithyConstants.MSG_SCALE_RESOURCE_ERROR, BlacksmithyConstants.MSG_SCALE_SKILL_ERROR );
			AddSubRes2( typeof( WhiteScales ),		BlacksmithyConstants.MSG_WHITE_SCALES, 0.0, BlacksmithyConstants.MSG_SCALE_RESOURCE_ERROR, BlacksmithyConstants.MSG_SCALE_SKILL_ERROR );
			AddSubRes2( typeof( BlueScales ),		BlacksmithyConstants.MSG_BLUE_SCALES, 0.0, BlacksmithyConstants.MSG_SCALE_RESOURCE_ERROR, BlacksmithyConstants.MSG_SCALE_SKILL_ERROR );
			AddSubRes2( typeof( DinosaurScales ),	BlacksmithyConstants.MSG_DINOSAUR_SCALES_NAME, 0.0, BlacksmithyConstants.MSG_SCALE_RESOURCE_ERROR, BlacksmithyConstants.MSG_SCALE_SKILL_ERROR );

			Resmelt = true;
			Repair = true;
			MarkOption = true;
			CanEnhance = Core.AOS;
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Timer to synchronize sound effect with anvil hit (currently unused)
		/// </summary>
		private class InternalTimer : Timer
		{
			private Mobile m_From;

			/// <summary>
			/// Initializes a new instance of the InternalTimer class
			/// </summary>
			/// <param name="from">The mobile to play sound for</param>
			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( BlacksmithyConstants.TIMER_DELAY_SECONDS ) )
			{
				m_From = from;
			}

			/// <summary>
			/// Called when the timer expires
			/// </summary>
			protected override void OnTick()
			{
				m_From.PlaySound( BlacksmithyConstants.SOUND_BLACKSMITH );
			}
		}

		#endregion
	}

	/// <summary>
	/// Attribute used to mark items as forges for blacksmithy crafting
	/// </summary>
	public class ForgeAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the ForgeAttribute class
		/// </summary>
		public ForgeAttribute()
		{
		}
	}

	/// <summary>
	/// Attribute used to mark items as anvils for blacksmithy crafting
	/// </summary>
	public class AnvilAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the AnvilAttribute class
		/// </summary>
		public AnvilAttribute()
		{
		}
	}
}
using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefGodSmithing : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Blacksmith; }
		}

        public override int GumpTitleNumber
        {
            get { return 0; }
        }
 
        public override string GumpTitleString
        {
            get { return GodCraftingStringConstants.GUMP_TITLE_SMITHING; }
        }

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefGodSmithing();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return GodCraftingConstants.MIN_SUCCESS_CHANCE; // 50%
		}

		private DefGodSmithing() : base( GodCraftingConstants.MIN_CHANCE_MULTIPLIER, GodCraftingConstants.MAX_CHANCE_MULTIPLIER, GodCraftingConstants.DELAY_MULTIPLIER )// base( 1, 1, 4.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return GodCraftingConstants.CLILOC_TOOL_WORN_OUT; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return GodCraftingConstants.CLILOC_TOOL_MUST_BE_ON_PERSON; // The tool must be on your person to use.

			if ( from.Map == Map.TerMur && from.X > GodCraftingConstants.SMITHING_AREA_MIN_X && from.X < GodCraftingConstants.SMITHING_AREA_MAX_X && from.Y > GodCraftingConstants.SMITHING_AREA_MIN_Y && from.Y < GodCraftingConstants.SMITHING_AREA_MAX_Y )
				return 0;

			return GodCraftingConstants.CLILOC_LOCATION_ERROR;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( GodCraftingConstants.SOUND_SMITHING );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( GodCraftingConstants.CLILOC_TOOL_WORN_OUT ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return GodCraftingConstants.CLILOC_FAILED_MATERIAL_LOST; // You failed to create the item, and some of your materials are lost.
				else
					return GodCraftingConstants.CLILOC_FAILED_NO_MATERIAL_LOST; // You failed to create the item, but no materials were lost.
			}
			else
			{
				return GodCraftingConstants.CLILOC_ITEM_CREATED; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			#region Armor

			AddCraft( typeof( AmethystPlateArms ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Amethyst Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( AmethystIngot ), "Block of Amethyst", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( AmethystPlateGloves ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Amethyst Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( AmethystIngot ), "Block of Amethyst", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( AmethystPlateGorget ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Amethyst Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( AmethystIngot ), "Block of Amethyst", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( AmethystPlateLegs ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Amethyst Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( AmethystIngot ), "Block of Amethyst", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( AmethystPlateChest ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Amethyst Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( AmethystIngot ), "Block of Amethyst", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( AmethystFemalePlateChest ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Amethyst Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( AmethystIngot ), "Block of Amethyst", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( AmethystPlateHelm ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Amethyst Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( AmethystIngot ), "Block of Amethyst", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( AmethystShield ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Amethyst Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( AmethystIngot ), "Block of Amethyst", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilAmethyst ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Amethyst Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( AmethystIngot ), "Block of Amethyst", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( EmeraldPlateArms ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Emerald Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( EmeraldIngot ), "Block of Emerald", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( EmeraldPlateGloves ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Emerald Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( EmeraldIngot ), "Block of Emerald", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( EmeraldPlateGorget ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Emerald Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( EmeraldIngot ), "Block of Emerald", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( EmeraldPlateLegs ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Emerald Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( EmeraldIngot ), "Block of Emerald", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( EmeraldPlateChest ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Emerald Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( EmeraldIngot ), "Block of Emerald", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( EmeraldFemalePlateChest ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Emerald Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( EmeraldIngot ), "Block of Emerald", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( EmeraldPlateHelm ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Emerald Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( EmeraldIngot ), "Block of Emerald", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( EmeraldShield ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Emerald Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( EmeraldIngot ), "Block of Emerald", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilEmerald ), GodCraftingStringConstants.CATEGORY_AMETHYST_EMERALD, "Emerald Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( EmeraldIngot ), "Block of Emerald", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( GarnetPlateArms ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Garnet Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( GarnetIngot ), "Block of Garnet", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( GarnetPlateGloves ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Garnet Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( GarnetIngot ), "Block of Garnet", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( GarnetPlateGorget ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Garnet Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( GarnetIngot ), "Block of Garnet", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( GarnetPlateLegs ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Garnet Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( GarnetIngot ), "Block of Garnet", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( GarnetPlateChest ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Garnet Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( GarnetIngot ), "Block of Garnet", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( GarnetFemalePlateChest ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Garnet Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( GarnetIngot ), "Block of Garnet", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( GarnetPlateHelm ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Garnet Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( GarnetIngot ), "Block of Garnet", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( GarnetShield ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Garnet Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( GarnetIngot ), "Block of Garnet", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilGarnet ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Garnet Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( GarnetIngot ), "Block of Garnet", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( IcePlateArms ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Ice Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( IceIngot ), "Block of Ice", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( IcePlateGloves ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Ice Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( IceIngot ), "Block of Ice", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( IcePlateGorget ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Ice Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( IceIngot ), "Block of Ice", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( IcePlateLegs ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Ice Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( IceIngot ), "Block of Ice", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( IcePlateChest ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Ice Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( IceIngot ), "Block of Ice", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( IceFemalePlateChest ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Ice Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( IceIngot ), "Block of Ice", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( IcePlateHelm ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Ice Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( IceIngot ), "Block of Ice", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( IceShield ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Ice Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( IceIngot ), "Block of Ice", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilIce ), GodCraftingStringConstants.CATEGORY_GARNET_ICE, "Ice Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( IceIngot ), "Block of Ice", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( JadePlateArms ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Jade Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( JadeIngot ), "Block of Jade", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( JadePlateGloves ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Jade Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( JadeIngot ), "Block of Jade", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( JadePlateGorget ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Jade Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( JadeIngot ), "Block of Jade", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( JadePlateLegs ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Jade Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( JadeIngot ), "Block of Jade", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( JadePlateChest ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Jade Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( JadeIngot ), "Block of Jade", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( JadeFemalePlateChest ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Jade Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( JadeIngot ), "Block of Jade", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( JadePlateHelm ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Jade Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( JadeIngot ), "Block of Jade", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( JadeShield ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Jade Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( JadeIngot ), "Block of Jade", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilJade ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Jade Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( JadeIngot ), "Block of Jade", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( MarblePlateArms ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Marble Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( MarbleIngot ), "Block of Marble", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( MarblePlateGloves ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Marble Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( MarbleIngot ), "Block of Marble", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( MarblePlateGorget ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Marble Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( MarbleIngot ), "Block of Marble", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( MarblePlateLegs ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Marble Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( MarbleIngot ), "Block of Marble", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( MarblePlateChest ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Marble Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( MarbleIngot ), "Block of Marble", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( MarbleFemalePlateChest ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Marble Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( MarbleIngot ), "Block of Marble", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( MarblePlateHelm ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Marble Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( MarbleIngot ), "Block of Marble", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( MarbleShields ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Marble Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( MarbleIngot ), "Block of Marble", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilMarble ), GodCraftingStringConstants.CATEGORY_JADE_MARBLE, "Marble Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( MarbleIngot ), "Block of Marble", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( OnyxPlateArms ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Onyx Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( OnyxIngot ), "Block of Onyx", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( OnyxPlateGloves ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Onyx Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( OnyxIngot ), "Block of Onyx", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( OnyxPlateGorget ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Onyx Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( OnyxIngot ), "Block of Onyx", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( OnyxPlateLegs ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Onyx Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( OnyxIngot ), "Block of Onyx", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( OnyxPlateChest ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Onyx Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( OnyxIngot ), "Block of Onyx", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( OnyxFemalePlateChest ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Onyx Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( OnyxIngot ), "Block of Onyx", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( OnyxPlateHelm ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Onyx Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( OnyxIngot ), "Block of Onyx", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( OnyxShield ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Onyx Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( OnyxIngot ), "Block of Onyx", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilOnyx ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Onyx Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( OnyxIngot ), "Block of Onyx", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( QuartzPlateArms ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Quartz Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( QuartzIngot ), "Block of Quartz", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( QuartzPlateGloves ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Quartz Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( QuartzIngot ), "Block of Quartz", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( QuartzPlateGorget ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Quartz Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( QuartzIngot ), "Block of Quartz", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( QuartzPlateLegs ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Quartz Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( QuartzIngot ), "Block of Quartz", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( QuartzPlateChest ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Quartz Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( QuartzIngot ), "Block of Quartz", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( QuartzFemalePlateChest ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Quartz Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( QuartzIngot ), "Block of Quartz", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( QuartzPlateHelm ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Quartz Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( QuartzIngot ), "Block of Quartz", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( QuartzShield ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Quartz Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( QuartzIngot ), "Block of Quartz", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilQuartz ), GodCraftingStringConstants.CATEGORY_ONYX_QUARTZ, "Quartz Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( QuartzIngot ), "Block of Quartz", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( RubyPlateArms ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Ruby Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( RubyIngot ), "Block of Ruby", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( RubyPlateGloves ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Ruby Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( RubyIngot ), "Block of Ruby", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( RubyPlateGorget ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Ruby Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( RubyIngot ), "Block of Ruby", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( RubyPlateLegs ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Ruby Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( RubyIngot ), "Block of Ruby", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( RubyPlateChest ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Ruby Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( RubyIngot ), "Block of Ruby", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( RubyFemalePlateChest ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Ruby Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( RubyIngot ), "Block of Ruby", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( RubyPlateHelm ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Ruby Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( RubyIngot ), "Block of Ruby", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( RubyShield ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Ruby Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( RubyIngot ), "Block of Ruby", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilRuby ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Ruby Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( RubyIngot ), "Block of Ruby", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( SapphirePlateArms ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Sapphire Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SapphireIngot ), "Block of Sapphire", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SapphirePlateGloves ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Sapphire Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SapphireIngot ), "Block of Sapphire", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SapphirePlateGorget ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Sapphire Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SapphireIngot ), "Block of Sapphire", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SapphirePlateLegs ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Sapphire Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SapphireIngot ), "Block of Sapphire", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SapphirePlateChest ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Sapphire Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SapphireIngot ), "Block of Sapphire", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SapphireFemalePlateChest ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Sapphire Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SapphireIngot ), "Block of Sapphire", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SapphirePlateHelm ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Sapphire Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SapphireIngot ), "Block of Sapphire", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SapphireShield ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Sapphire Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SapphireIngot ), "Block of Sapphire", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilSapphire ), GodCraftingStringConstants.CATEGORY_RUBY_SAPPHIRE, "Sapphire Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( SapphireIngot ), "Block of Sapphire", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( SilverPlateArms ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Silver Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( ShinySilverIngot ), "Block of Silver", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SilverPlateGloves ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Silver Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( ShinySilverIngot ), "Block of Silver", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SilverPlateGorget ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Silver Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( ShinySilverIngot ), "Block of Silver", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SilverPlateLegs ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Silver Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( ShinySilverIngot ), "Block of Silver", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SilverPlateChest ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Silver Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( ShinySilverIngot ), "Block of Silver", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SilverFemalePlateChest ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Silver Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( ShinySilverIngot ), "Block of Silver", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SilverPlateHelm ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Silver Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( ShinySilverIngot ), "Block of Silver", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SilverShield ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Silver Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( ShinySilverIngot ), "Block of Silver", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilSilver ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Silver Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( ShinySilverIngot ), "Block of Silver", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( SpinelPlateArms ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Spinel Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SpinelIngot ), "Block of Spinel", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SpinelPlateGloves ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Spinel Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SpinelIngot ), "Block of Spinel", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SpinelPlateGorget ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Spinel Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SpinelIngot ), "Block of Spinel", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SpinelPlateLegs ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Spinel Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SpinelIngot ), "Block of Spinel", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SpinelPlateChest ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Spinel Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SpinelIngot ), "Block of Spinel", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SpinelFemalePlateChest ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Spinel Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SpinelIngot ), "Block of Spinel", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SpinelPlateHelm ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Spinel Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SpinelIngot ), "Block of Spinel", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SpinelShield ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Spinel Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SpinelIngot ), "Block of Spinel", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilSpinel ), GodCraftingStringConstants.CATEGORY_SILVER_SPINEL, "Spinel Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( SpinelIngot ), "Block of Spinel", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( StarRubyPlateArms ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Star Ruby Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( StarRubyIngot ), "Block of Star Ruby", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StarRubyPlateGloves ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Star Ruby Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( StarRubyIngot ), "Block of Star Ruby", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StarRubyPlateGorget ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Star Ruby Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( StarRubyIngot ), "Block of Star Ruby", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StarRubyPlateLegs ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Star Ruby Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( StarRubyIngot ), "Block of Star Ruby", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StarRubyPlateChest ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Star Ruby Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( StarRubyIngot ), "Block of Star Ruby", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StarRubyFemalePlateChest ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Star Ruby Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( StarRubyIngot ), "Block of Star Ruby", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StarRubyPlateHelm ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Star Ruby Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( StarRubyIngot ), "Block of Star Ruby", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StarRubyShield ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Star Ruby Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( StarRubyIngot ), "Block of Star Ruby", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilStarRuby ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Star Ruby Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( StarRubyIngot ), "Block of Star Ruby", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( TopazPlateArms ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Topaz Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TopazIngot ), "Block of Topaz", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( TopazPlateGloves ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Topaz Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TopazIngot ), "Block of Topaz", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( TopazPlateGorget ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Topaz Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TopazIngot ), "Block of Topaz", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( TopazPlateLegs ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Topaz Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TopazIngot ), "Block of Topaz", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( TopazPlateChest ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Topaz Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TopazIngot ), "Block of Topaz", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( TopazFemalePlateChest ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Topaz Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TopazIngot ), "Block of Topaz", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( TopazPlateHelm ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Topaz Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TopazIngot ), "Block of Topaz", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( TopazShield ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Topaz Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TopazIngot ), "Block of Topaz", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilTopaz ), GodCraftingStringConstants.CATEGORY_STAR_RUBY_TOPAZ, "Topaz Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( TopazIngot ), "Block of Topaz", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( CaddellitePlateArms ), GodCraftingStringConstants.CATEGORY_CADDELLITE, "Caddellite Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( CaddelliteIngot ), "Block of Caddellite", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( CaddellitePlateGloves ), GodCraftingStringConstants.CATEGORY_CADDELLITE, "Caddellite Gauntlets", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( CaddelliteIngot ), "Block of Caddellite", 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( CaddellitePlateGorget ), GodCraftingStringConstants.CATEGORY_CADDELLITE, "Caddellite Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( CaddelliteIngot ), "Block of Caddellite", 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( CaddellitePlateLegs ), GodCraftingStringConstants.CATEGORY_CADDELLITE, "Caddellite Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( CaddelliteIngot ), "Block of Caddellite", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( CaddellitePlateChest ), GodCraftingStringConstants.CATEGORY_CADDELLITE, "Caddellite Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( CaddelliteIngot ), "Block of Caddellite", 25, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( CaddelliteFemalePlateChest ), GodCraftingStringConstants.CATEGORY_CADDELLITE, "Caddellite Female Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( CaddelliteIngot ), "Block of Caddellite", 20, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( CaddellitePlateHelm ), GodCraftingStringConstants.CATEGORY_CADDELLITE, "Caddellite Helm", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( CaddelliteIngot ), "Block of Caddellite", 15, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( CaddelliteShield ), GodCraftingStringConstants.CATEGORY_CADDELLITE, "Caddellite Shield", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( CaddelliteIngot ), "Block of Caddellite", 18, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( OilCaddellite ), GodCraftingStringConstants.CATEGORY_CADDELLITE, "Caddellite Oil", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( CaddelliteIngot ), "Block of Caddellite", 30, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			#endregion
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class DefGodSewing : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tailoring; }
		}

        public override int GumpTitleNumber
        {
            get { return 0; }
        }
 
        public override string GumpTitleString
        {
            get { return GodCraftingStringConstants.GUMP_TITLE_SEWING; }
        }

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefGodSewing();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return GodCraftingConstants.MIN_SUCCESS_CHANCE; // 50%
		}

		private DefGodSewing() : base( GodCraftingConstants.MIN_CHANCE_MULTIPLIER, GodCraftingConstants.MAX_CHANCE_MULTIPLIER, GodCraftingConstants.DELAY_MULTIPLIER )// base( 1, 1, 4.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return GodCraftingConstants.CLILOC_TOOL_WORN_OUT; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return GodCraftingConstants.CLILOC_TOOL_MUST_BE_ON_PERSON; // The tool must be on your person to use.

			if ( from.Map == Map.TerMur && from.X > GodCraftingConstants.SEWING_AREA_MIN_X && from.X < GodCraftingConstants.SEWING_AREA_MAX_X && from.Y > GodCraftingConstants.SEWING_AREA_MIN_Y && from.Y < GodCraftingConstants.SEWING_AREA_MAX_Y )
				return 0;

			return GodCraftingConstants.CLILOC_LOCATION_ERROR;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( GodCraftingConstants.SOUND_SEWING );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( GodCraftingConstants.CLILOC_TOOL_WORN_OUT ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return GodCraftingConstants.CLILOC_FAILED_MATERIAL_LOST; // You failed to create the item, and some of your materials are lost.
				else
					return GodCraftingConstants.CLILOC_FAILED_NO_MATERIAL_LOST; // You failed to create the item, but no materials were lost.
			}
			else
			{
				return GodCraftingConstants.CLILOC_ITEM_CREATED; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			#region Armor

			AddCraft( typeof( SkinDemonArms ), GodCraftingStringConstants.CATEGORY_DEMON_SKIN, "Demon Skin Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( DemonSkin ), GodCraftingStringConstants.CATEGORY_DEMON_SKIN, 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinDemonHelm ), GodCraftingStringConstants.CATEGORY_DEMON_SKIN, "Demon Skin Cap", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( DemonSkin ), GodCraftingStringConstants.CATEGORY_DEMON_SKIN, 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinDemonGloves ), GodCraftingStringConstants.CATEGORY_DEMON_SKIN, "Demon Skin Gloves", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( DemonSkin ), GodCraftingStringConstants.CATEGORY_DEMON_SKIN, 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinDemonGorget ), GodCraftingStringConstants.CATEGORY_DEMON_SKIN, "Demon Skin Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( DemonSkin ), GodCraftingStringConstants.CATEGORY_DEMON_SKIN, 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinDemonLegs ), GodCraftingStringConstants.CATEGORY_DEMON_SKIN, "Demon Skin Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( DemonSkin ), GodCraftingStringConstants.CATEGORY_DEMON_SKIN, 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinDemonChest ), GodCraftingStringConstants.CATEGORY_DEMON_SKIN, "Demon Skin Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( DemonSkin ), GodCraftingStringConstants.CATEGORY_DEMON_SKIN, 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( SkinDragonArms ), GodCraftingStringConstants.CATEGORY_DRAGON_SKIN, "Dragon Skin Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( DragonSkin ), GodCraftingStringConstants.CATEGORY_DRAGON_SKIN, 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinDragonHelm ), GodCraftingStringConstants.CATEGORY_DRAGON_SKIN, "Dragon Skin Cap", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( DragonSkin ), GodCraftingStringConstants.CATEGORY_DRAGON_SKIN, 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinDragonGloves ), GodCraftingStringConstants.CATEGORY_DRAGON_SKIN, "Dragon Skin Gloves", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( DragonSkin ), GodCraftingStringConstants.CATEGORY_DRAGON_SKIN, 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinDragonGorget ), GodCraftingStringConstants.CATEGORY_DRAGON_SKIN, "Dragon Skin Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( DragonSkin ), GodCraftingStringConstants.CATEGORY_DRAGON_SKIN, 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinDragonLegs ), GodCraftingStringConstants.CATEGORY_DRAGON_SKIN, "Dragon Skin Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( DragonSkin ), GodCraftingStringConstants.CATEGORY_DRAGON_SKIN, 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinDragonChest ), GodCraftingStringConstants.CATEGORY_DRAGON_SKIN, "Dragon Skin Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( DragonSkin ), GodCraftingStringConstants.CATEGORY_DRAGON_SKIN, 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( SkinNightmareArms ), GodCraftingStringConstants.CATEGORY_NIGHTMARE_SKIN, "Nightmare Skin Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( NightmareSkin ), GodCraftingStringConstants.CATEGORY_NIGHTMARE_SKIN, 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinNightmareHelm ), GodCraftingStringConstants.CATEGORY_NIGHTMARE_SKIN, "Nightmare Skin Cap", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( NightmareSkin ), GodCraftingStringConstants.CATEGORY_NIGHTMARE_SKIN, 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinNightmareGloves ), GodCraftingStringConstants.CATEGORY_NIGHTMARE_SKIN, "Nightmare Skin Gloves", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( NightmareSkin ), GodCraftingStringConstants.CATEGORY_NIGHTMARE_SKIN, 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinNightmareGorget ), GodCraftingStringConstants.CATEGORY_NIGHTMARE_SKIN, "Nightmare Skin Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( NightmareSkin ), GodCraftingStringConstants.CATEGORY_NIGHTMARE_SKIN, 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinNightmareLegs ), GodCraftingStringConstants.CATEGORY_NIGHTMARE_SKIN, "Nightmare Skin Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( NightmareSkin ), GodCraftingStringConstants.CATEGORY_NIGHTMARE_SKIN, 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinNightmareChest ), GodCraftingStringConstants.CATEGORY_NIGHTMARE_SKIN, "Nightmare Skin Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( NightmareSkin ), GodCraftingStringConstants.CATEGORY_NIGHTMARE_SKIN, 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( SkinSerpentArms ), GodCraftingStringConstants.CATEGORY_SERPENT_SKIN, "Serpent Skin Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SerpentSkin ), GodCraftingStringConstants.CATEGORY_SERPENT_SKIN, 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinSerpentHelm ), GodCraftingStringConstants.CATEGORY_SERPENT_SKIN, "Serpent Skin Cap", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SerpentSkin ), GodCraftingStringConstants.CATEGORY_SERPENT_SKIN, 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinSerpentGloves ), GodCraftingStringConstants.CATEGORY_SERPENT_SKIN, "Serpent Skin Gloves", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SerpentSkin ), GodCraftingStringConstants.CATEGORY_SERPENT_SKIN, 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinSerpentGorget ), GodCraftingStringConstants.CATEGORY_SERPENT_SKIN, "Serpent Skin Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SerpentSkin ), GodCraftingStringConstants.CATEGORY_SERPENT_SKIN, 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinSerpentLegs ), GodCraftingStringConstants.CATEGORY_SERPENT_SKIN, "Serpent Skin Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SerpentSkin ), GodCraftingStringConstants.CATEGORY_SERPENT_SKIN, 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinSerpentChest ), GodCraftingStringConstants.CATEGORY_SERPENT_SKIN, "Serpent Skin Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( SerpentSkin ), GodCraftingStringConstants.CATEGORY_SERPENT_SKIN, 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( SkinTrollArms ), GodCraftingStringConstants.CATEGORY_TROLL_SKIN, "Troll Skin Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TrollSkin ), GodCraftingStringConstants.CATEGORY_TROLL_SKIN, 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinTrollHelm ), GodCraftingStringConstants.CATEGORY_TROLL_SKIN, "Troll Skin Cap", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TrollSkin ), GodCraftingStringConstants.CATEGORY_TROLL_SKIN, 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinTrollGloves ), GodCraftingStringConstants.CATEGORY_TROLL_SKIN, "Troll Skin Gloves", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TrollSkin ), GodCraftingStringConstants.CATEGORY_TROLL_SKIN, 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinTrollGorget ), GodCraftingStringConstants.CATEGORY_TROLL_SKIN, "Troll Skin Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TrollSkin ), GodCraftingStringConstants.CATEGORY_TROLL_SKIN, 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinTrollLegs ), GodCraftingStringConstants.CATEGORY_TROLL_SKIN, "Troll Skin Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TrollSkin ), GodCraftingStringConstants.CATEGORY_TROLL_SKIN, 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinTrollChest ), GodCraftingStringConstants.CATEGORY_TROLL_SKIN, "Troll Skin Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( TrollSkin ), GodCraftingStringConstants.CATEGORY_TROLL_SKIN, 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( SkinUnicornArms ), GodCraftingStringConstants.CATEGORY_UNICORN_SKIN, "Unicorn Skin Arms", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( UnicornSkin ), GodCraftingStringConstants.CATEGORY_UNICORN_SKIN, 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinUnicornHelm ), GodCraftingStringConstants.CATEGORY_UNICORN_SKIN, "Unicorn Skin Cap", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( UnicornSkin ), GodCraftingStringConstants.CATEGORY_UNICORN_SKIN, 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinUnicornGloves ), GodCraftingStringConstants.CATEGORY_UNICORN_SKIN, "Unicorn Skin Gloves", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( UnicornSkin ), GodCraftingStringConstants.CATEGORY_UNICORN_SKIN, 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinUnicornGorget ), GodCraftingStringConstants.CATEGORY_UNICORN_SKIN, "Unicorn Skin Gorget", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( UnicornSkin ), GodCraftingStringConstants.CATEGORY_UNICORN_SKIN, 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinUnicornLegs ), GodCraftingStringConstants.CATEGORY_UNICORN_SKIN, "Unicorn Skin Leggings", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( UnicornSkin ), GodCraftingStringConstants.CATEGORY_UNICORN_SKIN, 10, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SkinUnicornChest ), GodCraftingStringConstants.CATEGORY_UNICORN_SKIN, "Unicorn Skin Tunic", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( UnicornSkin ), GodCraftingStringConstants.CATEGORY_UNICORN_SKIN, 12, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			#endregion
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class DefGodBrewing : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Alchemy; }
		}

        public override int GumpTitleNumber
        {
            get { return 0; }
        }
 
        public override string GumpTitleString
        {
            get { return GodCraftingStringConstants.GUMP_TITLE_BREWING; }
        }

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefGodBrewing();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return GodCraftingConstants.MIN_SUCCESS_CHANCE; // 50%
		}

		private DefGodBrewing() : base( GodCraftingConstants.MIN_CHANCE_MULTIPLIER, GodCraftingConstants.MAX_CHANCE_MULTIPLIER, GodCraftingConstants.DELAY_MULTIPLIER )// base( 1, 1, 4.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return GodCraftingConstants.CLILOC_TOOL_WORN_OUT; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return GodCraftingConstants.CLILOC_TOOL_MUST_BE_ON_PERSON; // The tool must be on your person to use.

			if ( from.Map == Map.TerMur && from.X > GodCraftingConstants.BREWING_AREA_MIN_X && from.X < GodCraftingConstants.BREWING_AREA_MAX_X && from.Y > GodCraftingConstants.BREWING_AREA_MIN_Y && from.Y < GodCraftingConstants.BREWING_AREA_MAX_Y )
				return 0;

			return GodCraftingConstants.CLILOC_LOCATION_ERROR;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( GodCraftingConstants.SOUND_BREWING );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			Server.Gumps.MReagentGump.XReagentGump( from );

			if ( toolBroken )
				from.SendLocalizedMessage( GodCraftingConstants.CLILOC_TOOL_WORN_OUT ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return GodCraftingConstants.CLILOC_FAILED_MATERIAL_LOST; // You failed to create the item, and some of your materials are lost.
				else
					return GodCraftingConstants.CLILOC_FAILED_NO_MATERIAL_LOST; // You failed to create the item, but no materials were lost.
			}
			else
			{
				return GodCraftingConstants.CLILOC_ITEM_CREATED; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			#region Potions

			index = AddCraft( typeof( LesserInvisibilityPotion ), GodCraftingStringConstants.CATEGORY_INVISIBILITY, "Invisibility Potion, Lesser", GodCraftingConstants.LESSER_POTION_MIN_SKILL, GodCraftingConstants.LESSER_POTION_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( SilverSerpentVenom ), "Silver Serpent Venom", 1, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( DragonBlood ), "Dragon Blood", 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			index = AddCraft( typeof( InvisibilityPotion ), GodCraftingStringConstants.CATEGORY_INVISIBILITY, "Invisibility Potion", GodCraftingConstants.REGULAR_POTION_MIN_SKILL, GodCraftingConstants.REGULAR_POTION_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( SilverSerpentVenom ), "Silver Serpent Venom", 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( DragonBlood ), "Dragon Blood", 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			index = AddCraft( typeof( GreaterInvisibilityPotion ), GodCraftingStringConstants.CATEGORY_INVISIBILITY, "Invisibility Potion, Greater", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( SilverSerpentVenom ), "Silver Serpent Venom", 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( DragonBlood ), "Dragon Blood", 6, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			index = AddCraft( typeof( InvulnerabilityPotion ), GodCraftingStringConstants.CATEGORY_INVULNERABILITY, "Invulnerability Potion", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( EnchantedSeaweed ), "Enchanted Seaweed", 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( DragonTooth ), "Dragon Tooth", 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			index = AddCraft( typeof( LesserManaPotion ), GodCraftingStringConstants.CATEGORY_MANA, "Mana Potion, Lesser", GodCraftingConstants.LESSER_POTION_MIN_SKILL, GodCraftingConstants.LESSER_POTION_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( GoldenSerpentVenom ), "Golden Serpent Venom", 1, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( LichDust ), "Lich Dust", 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			index = AddCraft( typeof( ManaPotion ), GodCraftingStringConstants.CATEGORY_MANA, "Mana Potion", GodCraftingConstants.REGULAR_POTION_MIN_SKILL, GodCraftingConstants.REGULAR_POTION_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( GoldenSerpentVenom ), "Golden Serpent Venom", 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( LichDust ), "Lich Dust", 4, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			index = AddCraft( typeof( GreaterManaPotion ), GodCraftingStringConstants.CATEGORY_MANA, "Mana Potion, Greater", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( GoldenSerpentVenom ), "Golden Serpent Venom", 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( LichDust ), "Lich Dust", 6, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			index = AddCraft( typeof( LesserRejuvenatePotion ), GodCraftingStringConstants.CATEGORY_REJUVENATE, "Rejuvenation Potion, Lesser", GodCraftingConstants.LESSER_POTION_MIN_SKILL, GodCraftingConstants.LESSER_POTION_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( DemonClaw ), "Demon Claw", 1, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( UnicornHorn ), "Unicorn Horn", 1, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			index = AddCraft( typeof( RejuvenatePotion ), GodCraftingStringConstants.CATEGORY_REJUVENATE, "Rejuvenation Potion", GodCraftingConstants.REGULAR_POTION_MIN_SKILL, GodCraftingConstants.REGULAR_POTION_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( DemonClaw ), "Demon Claw", 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( UnicornHorn ), "Unicorn Horn", 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			index = AddCraft( typeof( GreaterRejuvenatePotion ), GodCraftingStringConstants.CATEGORY_REJUVENATE, "Rejuvenation Potion, Greater", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( DemonClaw ), "Demon Claw", 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( UnicornHorn ), "Unicorn Horn", 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			index = AddCraft( typeof( SuperPotion ), GodCraftingStringConstants.CATEGORY_REJUVENATE, "Superior Potion", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( DemonClaw ), "Demon Claw", 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( UnicornHorn ), "Unicorn Horn", 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			index = AddCraft( typeof( AutoResPotion ), GodCraftingStringConstants.CATEGORY_RESURRECTION, "Resurrect Self Potion", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( DemigodBlood ), "Demigod Blood", 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( GhostlyDust ), "Ghostly Dust", 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			index = AddCraft( typeof( ResurrectPotion ), GodCraftingStringConstants.CATEGORY_RESURRECTION, "Resurrect Others Potion", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( DemigodBlood ), "Demigod Blood", 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( GhostlyDust ), "Ghostly Dust", 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			index = AddCraft( typeof( RepairPotion ), GodCraftingStringConstants.CATEGORY_REPAIR, "Repairing Potion", GodCraftingConstants.STANDARD_MIN_SKILL, GodCraftingConstants.STANDARD_MAX_SKILL, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( UnicornHorn ), "Unicorn Horn", 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( SilverSerpentVenom ), "Silver Serpent Venom", 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );

			/*index = AddCraft( typeof( DurabilityPotion ), GodCraftingStringConstants.CATEGORY_REPAIR, "Durability Potion", 110.0, 125.0, typeof( Bottle ), GodCraftingConstants.CLILOC_BOTTLE, 1, GodCraftingConstants.CLILOC_GENERIC_ERROR );
			AddRes( index, typeof( GoldenSerpentVenom ), "Golden Serpent Venom", 3, GodCraftingConstants.CLILOC_RESOURCE_LACK );
			AddRes( index, typeof( DragonBlood ), "Dragon Blood", 2, GodCraftingConstants.CLILOC_RESOURCE_LACK );*/

			#endregion
		}
	}
}
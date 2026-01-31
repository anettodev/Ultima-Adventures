using System; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Engines.Craft 
{ 
	public class DefMasonry : CraftSystem 
	{ 
		public override SkillName MainSkill 
		{ 
			get{ return SkillName.Carpentry; } 
		} 

		public override int GumpTitleNumber 
		{ 
			get{ return MasonryConstants.CLILOC_GUMP_TITLE; } // <CENTER>MASONRY MENU</CENTER> 
		} 

		private static CraftSystem m_CraftSystem; 

		public static CraftSystem CraftSystem 
		{ 
			get 
			{ 
				if ( m_CraftSystem == null ) 
					m_CraftSystem = new DefMasonry(); 

				return m_CraftSystem; 
			} 
		} 

		public override double GetChanceAtMin( CraftItem item ) 
		{ 
			return MasonryConstants.MIN_SUCCESS_CHANCE; // 0% 
		} 

		private DefMasonry() : base( MasonryConstants.MIN_CHANCE_MULTIPLIER, MasonryConstants.MAX_CHANCE_MULTIPLIER, MasonryConstants.DELAY_MULTIPLIER )// base( 1, 2, 1.7 ) 
		{ 
		} 

		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			return true;
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return MasonryConstants.CLILOC_TOOL_WORN_OUT; // You have worn out your tool!
			else if ( !BaseTool.CheckTool( tool, from ) )
				return MasonryConstants.CLILOC_TOOL_EQUIPPED; // If you have a tool equipped, you must use that tool.
			else if ( !(from is PlayerMobile && ((PlayerMobile)from).Masonry && from.Skills[SkillName.Carpentry].Base >= MasonryConstants.MASONRY_SKILL_REQUIREMENT) )
				return MasonryConstants.CLILOC_NOT_LEARNED_STONECRAFT; // You havent learned stonecraft.
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return MasonryConstants.CLILOC_TOOL_MUST_BE_ON_PERSON; // The tool must be on your person to use.

			return 0;
		} 

		public override void PlayCraftEffect( Mobile from ) 
		{ 
			// no effects
			//if ( from.Body.Type == BodyType.Human && !from.Mounted ) 
			//	from.Animate( 9, 5, 1, true, false, 0 ); 
			//new InternalTimer( from ).Start();

			from.PlaySound( MasonryConstants.SOUND_STONEWORKING );
		} 

		// Delay to synchronize the sound with the hit on the anvil 
		private class InternalTimer : Timer 
		{ 
			private Mobile m_From; 

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( MasonryConstants.SOUND_DELAY_SECONDS ) ) 
			{ 
				m_From = from; 
			} 

			protected override void OnTick() 
			{ 
				m_From.PlaySound( MasonryConstants.SOUND_ANVIL_HIT ); 
			} 
		} 

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item ) 
		{ 
			if ( toolBroken ) 
				from.SendLocalizedMessage( MasonryConstants.CLILOC_TOOL_WORN_OUT ); // You have worn out your tool 

			if ( failed ) 
			{ 
				if ( lostMaterial ) 
					return MasonryConstants.CLILOC_FAILED_MATERIAL_LOST; // You failed to create the item, and some of your materials are lost. 
				else 
					return MasonryConstants.CLILOC_FAILED_NO_MATERIAL_LOST; // You failed to create the item, but no materials were lost. 
			} 
			else 
			{ 
				if ( quality == MasonryConstants.QUALITY_BELOW_AVERAGE ) 
					return MasonryConstants.CLILOC_BARELY_MADE; // You were barely able to make this item.  It's quality is below average. 
				else if ( makersMark && quality == MasonryConstants.QUALITY_EXCEPTIONAL ) 
					return MasonryConstants.CLILOC_EXCEPTIONAL_WITH_MARK; // You create an exceptional quality item and affix your maker's mark. 
				else if ( quality == MasonryConstants.QUALITY_EXCEPTIONAL ) 
					return MasonryConstants.CLILOC_EXCEPTIONAL; // You create an exceptional quality item. 
				else             
					return MasonryConstants.CLILOC_ITEM_CREATED; // You create the item. 
			} 
		} 

		public override void InitCraftList() 
		{
			int index = -1;

			// Containers

			index = AddCraft( typeof( StoneCoffin ), MasonryStringConstants.CATEGORY_CONTAINERS, "sarcophagus, woman", 90.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 10, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddSkill( index, SkillName.Forensics, MasonryConstants.FORENSICS_SARCOPHAGUS_MIN, MasonryConstants.FORENSICS_SARCOPHAGUS_MAX );
			index = AddCraft( typeof( StoneCasket ), MasonryStringConstants.CATEGORY_CONTAINERS, "sarcophagus, man", 90.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 10, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddSkill( index, SkillName.Forensics, MasonryConstants.FORENSICS_SARCOPHAGUS_MIN, MasonryConstants.FORENSICS_SARCOPHAGUS_MAX );
			index = AddCraft( typeof( RockUrn ), MasonryStringConstants.CATEGORY_CONTAINERS, "urn", 80.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 5, MasonryConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( RockVase ), MasonryStringConstants.CATEGORY_CONTAINERS, "vase", 80.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 5, MasonryConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( StoneOrnateUrn ), MasonryStringConstants.CATEGORY_CONTAINERS, "urn", 90.0, 110.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 6, MasonryConstants.CLILOC_RESOURCE_LACK );
			index = AddCraft( typeof( StoneOrnateTallVase ), MasonryStringConstants.CATEGORY_CONTAINERS, "vase", 95.0, 120.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );

			// Decorations

			AddCraft( typeof( StoneVase ), MasonryStringConstants.CATEGORY_DECORATIONS, "vase", 42.5, 92.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 2, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneLargeVase ), MasonryStringConstants.CATEGORY_DECORATIONS, "vase, large", 52.5, 102.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneAmphora ), MasonryStringConstants.CATEGORY_DECORATIONS, "amphora", 42.5, 92.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 2, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneLargeAmphora ), MasonryStringConstants.CATEGORY_DECORATIONS, "amphora, large", 52.5, 102.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneOrnateVase ), MasonryStringConstants.CATEGORY_DECORATIONS, "vase, ornate", 52.5, 102.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneOrnateAmphora ), MasonryStringConstants.CATEGORY_DECORATIONS, "amphora, ornate", 52.5, 102.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneGargoyleVase ), MasonryStringConstants.CATEGORY_DECORATIONS, "vase, gargoyle", 62.5, 112.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 6, MasonryConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( StoneBuddhistSculpture ), MasonryStringConstants.CATEGORY_DECORATIONS, "sculpture, Buddhist", 62.5, 122.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneMingSculpture ), MasonryStringConstants.CATEGORY_DECORATIONS, "sculpture, Ming", 52.5, 122.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 6, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneYuanSculpture ), MasonryStringConstants.CATEGORY_DECORATIONS, "sculpture, Yuan", 52.5, 122.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 6, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneQinSculpture ), MasonryStringConstants.CATEGORY_DECORATIONS, "sculpture, Qin", 52.5, 122.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 6, MasonryConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( StoneMingUrn ), MasonryStringConstants.CATEGORY_DECORATIONS, "urn, Ming", 42.5, 92.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneQinUrn ), MasonryStringConstants.CATEGORY_DECORATIONS, "urn, Qin", 42.5, 92.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneYuanUrn ), MasonryStringConstants.CATEGORY_DECORATIONS, "urn, Yuan", 42.5, 92.5, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( StoneChairs ), MasonryStringConstants.CATEGORY_FURNITURE, MasonryConstants.CLILOC_STONE_CHAIRS, 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneBenchLong ), MasonryStringConstants.CATEGORY_FURNITURE, "bench, long", 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneBenchShort ), MasonryStringConstants.CATEGORY_FURNITURE, "bench, short", 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 5, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTableLong ), MasonryStringConstants.CATEGORY_FURNITURE, "table, long", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 12, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTableShort ), MasonryStringConstants.CATEGORY_FURNITURE, "table, short", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 10, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneWizardTable ), MasonryStringConstants.CATEGORY_FURNITURE, "table, wizard", 95.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 15, MasonryConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( StoneSteps ), MasonryStringConstants.CATEGORY_FURNITURE, "steps", 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 5, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneBlock ), MasonryStringConstants.CATEGORY_FURNITURE, "block", 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 5, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneSarcophagus ), MasonryStringConstants.CATEGORY_FURNITURE, "sarcophagus", 65.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 10, MasonryConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( StoneColumn ), MasonryStringConstants.CATEGORY_FURNITURE, "column", 65.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 10, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneGothicColumn ), MasonryStringConstants.CATEGORY_FURNITURE, "column, gothic", 85.0, 135.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 20, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StonePedestal ), MasonryStringConstants.CATEGORY_FURNITURE, "pedestal", 65.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 5, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneFancyPedestal ), MasonryStringConstants.CATEGORY_FURNITURE, "pedestal, fancy", 70.0, 130.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 7, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneRoughPillar ), MasonryStringConstants.CATEGORY_FURNITURE, "pillar", 85.0, 135.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 15, MasonryConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( SmallStatueAngel ), MasonryStringConstants.CATEGORY_SMALL_STATUES, "angel statue", 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SmallStatueDragon ), MasonryStringConstants.CATEGORY_SMALL_STATUES, "dragon statue", 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueGargoyleBust ), MasonryStringConstants.CATEGORY_SMALL_STATUES, "gargoyle bust", 60.0, 110.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 6, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( GargoyleStatue ), MasonryStringConstants.CATEGORY_SMALL_STATUES, "gargoyle statue", 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueBust ), MasonryStringConstants.CATEGORY_SMALL_STATUES, "man bust", 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SmallStatueMan ), MasonryStringConstants.CATEGORY_SMALL_STATUES, "man statue", 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SmallStatueNoble ), MasonryStringConstants.CATEGORY_SMALL_STATUES, "noble statue", 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SmallStatuePegasus ), MasonryStringConstants.CATEGORY_SMALL_STATUES, "pegasus statue", 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SmallStatueSkull ), MasonryStringConstants.CATEGORY_SMALL_STATUES, "skull idol", 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SmallStatueWoman ), MasonryStringConstants.CATEGORY_SMALL_STATUES, "woman statue", 55.0, 105.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 4, MasonryConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( StatueAdventurer ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "adventurer statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueAmazon ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "amazon statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueDemonicFace ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "demonic face", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueDruid ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "druid statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueElvenKnight ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "elf knight statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueElvenPriestess ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "elf priestess statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueElvenSorceress ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "elf sorceress statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueElvenWarrior ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "elf warrior statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueFighter ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "fighter statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueGargoyleTall ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "gargoyle statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( GargoyleFlightStatue ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "gargoyle statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueGryphon ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "gryphon statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 10, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SmallStatueLion ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "lion statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( MedusaStatue ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "medusa statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueMermaid ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "mermaid statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 10, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueNoble ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "noble statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatuePriest ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "priest statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueSeaHorse ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "sea horse statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 10, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( SphinxStatue ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "sphinx statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueSwordsman ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "swordsman statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueWolfWinged ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "winged wolf statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueWizard ), MasonryStringConstants.CATEGORY_MEDIUM_STATUES, "wizard statue", 65.0, 115.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 8, MasonryConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( StatueDwarf ), MasonryStringConstants.CATEGORY_LARGE_STATUES, "dwarf statue", 75.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 16, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueDesertGod ), MasonryStringConstants.CATEGORY_LARGE_STATUES, "god statue", 75.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 16, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueHorseRider ), MasonryStringConstants.CATEGORY_LARGE_STATUES, "horse rider", 75.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 16, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( MediumStatueLion ), MasonryStringConstants.CATEGORY_LARGE_STATUES, "lion statue", 75.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 16, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueMinotaurDefend ), MasonryStringConstants.CATEGORY_LARGE_STATUES, "minotaur statue, defend", 75.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 16, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueMinotaurAttack ), MasonryStringConstants.CATEGORY_LARGE_STATUES, "minotaur statue, attack", 75.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 16, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( LargePegasusStatue ), MasonryStringConstants.CATEGORY_LARGE_STATUES, "pegasus statue", 75.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 16, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueWomanWarriorPillar ), MasonryStringConstants.CATEGORY_LARGE_STATUES, "woman warrior statue", 75.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 16, MasonryConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( StatueAngelTall ), MasonryStringConstants.CATEGORY_HUGE_STATUES, "angel statue", 85.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 24, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueDaemon ), MasonryStringConstants.CATEGORY_HUGE_STATUES, "daemon statue, tall", 85.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 24, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( LargeStatueLion ), MasonryStringConstants.CATEGORY_HUGE_STATUES, "lion statue", 85.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 24, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( TallStatueLion ), MasonryStringConstants.CATEGORY_HUGE_STATUES, "lion statue, tall", 85.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 24, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueCapeWarrior ), MasonryStringConstants.CATEGORY_HUGE_STATUES, "warrior statue", 85.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 24, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueWiseManTall ), MasonryStringConstants.CATEGORY_HUGE_STATUES, "wise man statue", 85.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 24, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( LargeStatueWolf ), MasonryStringConstants.CATEGORY_HUGE_STATUES, "wolf statue", 85.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 24, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueWomanTall ), MasonryStringConstants.CATEGORY_HUGE_STATUES, "woman statue", 85.0, 125.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 24, MasonryConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( StatueGateGuardian ), MasonryStringConstants.CATEGORY_GIANT_STATUES, "gate guardian statue", 95.0, 135.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 32, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueGuardian ), MasonryStringConstants.CATEGORY_GIANT_STATUES, "guardian statue", 95.0, 135.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 32, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StatueGiantWarrior ), MasonryStringConstants.CATEGORY_GIANT_STATUES, "warrior statue", 95.0, 135.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 32, MasonryConstants.CLILOC_RESOURCE_LACK );

			AddCraft( typeof( StoneTombStoneA ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneB ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneC ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneD ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneE ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneF ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneG ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneH ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneI ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneJ ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneK ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneL ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneM ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneN ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneO ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneP ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneQ ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneR ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneS ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );
			AddCraft( typeof( StoneTombStoneT ), MasonryStringConstants.CATEGORY_TOMBSTONES, "tombstone", 45.0, 95.0, typeof( Granite ), MasonryConstants.CLILOC_GRANITE_RESOURCE, 3, MasonryConstants.CLILOC_RESOURCE_LACK );

			SetSubRes( typeof( Granite ), MasonryConstants.CLILOC_GRANITE_SUBRES );
            // The NameNumber is in CILOC file!!!!
            AddSubRes( typeof( Granite ),			MasonryConstants.CLILOC_GRANITE_SUBRES, 00.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_GRANITE_SUBRES_MSG );
			AddSubRes( typeof( DullCopperGranite ),	1044023, 65.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG );
			AddSubRes( typeof( ShadowIronGranite ),	1044024, 70.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG );
			AddSubRes( typeof( CopperGranite ),		1044025, 75.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG );
			AddSubRes( typeof( BronzeGranite ),		1044026, 80.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG );
            AddSubRes(typeof(PlatinumGranite), 6663000, 99.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG);
            AddSubRes( typeof( GoldGranite ),		1044027, 85.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG );
			AddSubRes( typeof( AgapiteGranite ),	1044028, 90.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG );
			AddSubRes( typeof( VeriteGranite ),		1044029, 95.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG );
			AddSubRes( typeof( ValoriteGranite ),	1044030, 99.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG );
            AddSubRes( typeof( TitaniumGranite),	6661000, 99.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG);
            AddSubRes(typeof(RoseniumGranite), 6662000, 99.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG);
            AddSubRes( typeof( NepturiteGranite ),	1036173, 99.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG );
			AddSubRes( typeof( ObsidianGranite ),	1036162, 99.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG );
			AddSubRes( typeof( MithrilGranite ),	1036137, 109.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG );
			AddSubRes( typeof( XormiteGranite ),	1034437, 109.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG );
			AddSubRes( typeof( DwarvenGranite ),	1036181, 110.0, MasonryConstants.CLILOC_GRANITE_RESOURCE, MasonryConstants.CLILOC_COLORED_GRANITE_MSG );
		}
	}
}
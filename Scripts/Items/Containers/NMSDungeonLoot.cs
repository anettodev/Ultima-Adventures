using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Spells.Third;

namespace Server.Items
{
	/// <summary>
	/// NMS (New Magic System) Dungeon Loot class for harvest resources
	/// Provides specialized loot focused on crafting materials and reagents
	/// </summary>
	public class NMSDungeonLoot
	{
		#region Harvest Resource Types

		/// <summary>
		/// Ingot types for smithing resources
		/// </summary>
		private static Type[] m_IngotTypes = new Type[]
		{
			typeof( IronIngot ),				typeof( IronIngot ),				typeof( IronIngot ),				typeof( IronIngot ),
			typeof( DullCopperIngot ),			typeof( DullCopperIngot ),			typeof( DullCopperIngot ),
			typeof( ShadowIronIngot ),			typeof( ShadowIronIngot ),			typeof( ShadowIronIngot ),
			typeof( CopperIngot ),				typeof( CopperIngot ),				typeof( CopperIngot ),
			typeof( BronzeIngot ),				typeof( BronzeIngot ),				typeof( BronzeIngot ),
			typeof( GoldIngot ),				typeof( GoldIngot ),
			typeof( AgapiteIngot ),			typeof( AgapiteIngot ),
			typeof( VeriteIngot ),
			typeof( ValoriteIngot ),
			typeof( TitaniumIngot ),
			typeof( RoseniumIngot ),
			typeof( PlatinumIngot )
		};

		/// <summary>
		/// Leather types for tailoring resources
		/// </summary>
		private static Type[] m_LeatherTypes = new Type[]
		{
			typeof( HornedLeather ),			typeof( HornedLeather ),			typeof( HornedLeather ),
			typeof( BarbedLeather ),			typeof( BarbedLeather ),			typeof( BarbedLeather ),
			typeof( SpinedLeather ),			typeof( SpinedLeather ),
			typeof( VolcanicLeather ),			typeof( VolcanicLeather ),
			typeof( GoliathLeather )
		};

		/// <summary>
		/// Board types for carpentry resources
		/// </summary>
		private static Type[] m_BoardTypes = new Type[]
		{
			typeof( RosewoodBoard ),			typeof( RosewoodBoard ),			typeof( RosewoodBoard ),
			typeof( CherryBoard ),				typeof( CherryBoard ),				typeof( CherryBoard ),
			typeof( GoldenOakBoard ),			typeof( GoldenOakBoard ),			typeof( GoldenOakBoard ),
			typeof( ElvenBoard ),				typeof( ElvenBoard ),				typeof( ElvenBoard ),
			typeof( HickoryBoard ),				typeof( HickoryBoard ),				typeof( HickoryBoard )
		};

		/// <summary>
		/// Magery reagent types (standard spell reagents)
		/// </summary>
		private static Type[] m_MageryReagentTypes = new Type[]
		{
			typeof( BlackPearl ),				typeof( BlackPearl ),				typeof( BlackPearl ),
			typeof( Bloodmoss ),				typeof( Bloodmoss ),				typeof( Bloodmoss ),
			typeof( Garlic ),					typeof( Garlic ),					typeof( Garlic ),
			typeof( Ginseng ),					typeof( Ginseng ),					typeof( Ginseng ),
			typeof( MandrakeRoot ),				typeof( MandrakeRoot ),			typeof( MandrakeRoot ),
			typeof( Nightshade ),				typeof( Nightshade ),				typeof( Nightshade ),
			typeof( SulfurousAsh ),				typeof( SulfurousAsh ),			typeof( SulfurousAsh ),
			typeof( SpidersSilk ),				typeof( SpidersSilk ),				typeof( SpidersSilk )
		};

		/// <summary>
		/// Alchemy reagent types (for alchemy crafting)
		/// </summary>
		private static Type[] m_AlchemyReagentTypes = new Type[]
		{
			typeof( Brimstone ),				typeof( Brimstone ),				typeof( Brimstone ),
			typeof( ButterflyWings ),			typeof( ButterflyWings ),			typeof( ButterflyWings ),
			typeof( EyeOfToad ),				typeof( EyeOfToad ),				typeof( EyeOfToad ),
			typeof( FairyEgg ),					typeof( FairyEgg ),					typeof( FairyEgg ),
			typeof( GargoyleEar ),				typeof( GargoyleEar ),				typeof( GargoyleEar ),
			typeof( BeetleShell ),				typeof( BeetleShell ),				typeof( BeetleShell ),
			typeof( MoonCrystal ),				typeof( MoonCrystal ),				typeof( MoonCrystal ),
			typeof( PixieSkull ),				typeof( PixieSkull ),				typeof( PixieSkull )
		};

		/// <summary>
		/// Dragon scale types (for armor crafting)
		/// </summary>
		private static Type[] m_DragonScaleTypes = new Type[]
		{
			typeof( BlackScales ),				typeof( BlackScales ),				typeof( BlackScales ),
			typeof( BlueScales ),				typeof( BlueScales ),				typeof( BlueScales ),
			typeof( GreenScales ),				typeof( GreenScales ),				typeof( GreenScales ),
			typeof( RedScales ),				typeof( RedScales ),				typeof( RedScales ),
			typeof( WhiteScales ),				typeof( WhiteScales ),				typeof( WhiteScales ),
			typeof( YellowScales ),			typeof( YellowScales ),				typeof( YellowScales )
		};

		/// <summary>
		/// Basic material types (leather and cloth)
		/// </summary>
		private static Type[] m_BasicMaterialTypes = new Type[]
		{
			typeof( Leather ),					typeof( Leather ),					typeof( Leather ),					typeof( Leather ),
			typeof( Cloth ),					typeof( Cloth ),					typeof( Cloth ),
			typeof( BoltOfCloth ),				typeof( BoltOfCloth ),
			typeof( UncutCloth )
		};

		/// <summary>
		/// Fletching material types
		/// </summary>
		private static Type[] m_FletchingMaterialTypes = new Type[]
		{
			typeof( Feather ),					typeof( Feather ),					typeof( Feather ),					typeof( Feather ),
			typeof( Shaft ),					typeof( Shaft ),					typeof( Shaft ),					typeof( Shaft )
		};

		/// <summary>
		/// Necromancy reagent types
		/// </summary>
		private static Type[] m_NecromancyReagentTypes = new Type[]
		{
			typeof( BatWing ),					typeof( BatWing ),					typeof( BatWing ),
			typeof( GraveDust ),				typeof( GraveDust ),				typeof( GraveDust ),
			typeof( DaemonBlood ),				typeof( DaemonBlood ),				typeof( DaemonBlood ),
			typeof( NoxCrystal ),				typeof( NoxCrystal ),				typeof( NoxCrystal ),
			typeof( PigIron ),					typeof( PigIron ),					typeof( PigIron )
		};

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the ingot types array
		/// </summary>
		public static Type[] IngotTypes{ get{ return m_IngotTypes; } }

		/// <summary>
		/// Gets the leather types array
		/// </summary>
		public static Type[] LeatherTypes{ get{ return m_LeatherTypes; } }

		/// <summary>
		/// Gets the board types array
		/// </summary>
		public static Type[] BoardTypes{ get{ return m_BoardTypes; } }

		/// <summary>
		/// Gets the magery reagent types array
		/// </summary>
		public static Type[] MageryReagentTypes{ get{ return m_MageryReagentTypes; } }

		/// <summary>
		/// Gets the alchemy reagent types array
		/// </summary>
		public static Type[] AlchemyReagentTypes{ get{ return m_AlchemyReagentTypes; } }

		/// <summary>
		/// Gets the dragon scale types array
		/// </summary>
		public static Type[] DragonScaleTypes{ get{ return m_DragonScaleTypes; } }

		/// <summary>
		/// Gets the basic material types array
		/// </summary>
		public static Type[] BasicMaterialTypes{ get{ return m_BasicMaterialTypes; } }

		/// <summary>
		/// Gets the fletching material types array
		/// </summary>
		public static Type[] FletchingMaterialTypes{ get{ return m_FletchingMaterialTypes; } }

		/// <summary>
		/// Gets the necromancy reagent types array
		/// </summary>
		public static Type[] NecromancyReagentTypes{ get{ return m_NecromancyReagentTypes; } }

		/// <summary>
		/// Metal weapon types (swords, axes, maces, daggers, polearms)
		/// </summary>
		private static Type[] m_MetalWeaponTypes = new Type[]
		{
			typeof( Longsword ),				typeof( Longsword ),				typeof( Longsword ),
			typeof( Broadsword ),				typeof( Broadsword ),
			typeof( WarHammer ),				typeof( WarHammer ),				typeof( WarHammer ),
			typeof( Mace ),						typeof( Mace ),						typeof( Mace ),
			typeof( Axe ),						typeof( Axe ),						typeof( Axe ),
			typeof( BattleAxe ),				typeof( BattleAxe ),
			typeof( Dagger ),					typeof( Dagger ),					typeof( Dagger ),
			typeof( Spear ),					typeof( Spear ),
			typeof( Halberd )
		};

		/// <summary>
		/// Wood weapon types (bows, crossbows, staves)
		/// </summary>
		private static Type[] m_WoodWeaponTypes = new Type[]
		{
			typeof( Bow ),						typeof( Bow ),						typeof( Bow ),
			typeof( CompositeBow ),				typeof( CompositeBow ),
			typeof( Crossbow ),					typeof( Crossbow ),
			typeof( HeavyCrossbow ),
			typeof( QuarterStaff ),			typeof( QuarterStaff ),			typeof( QuarterStaff ),
			typeof( GnarledStaff ),				typeof( GnarledStaff ),
			typeof( BlackStaff )
		};

		/// <summary>
		/// Leather weapon types (very limited - mostly whips if available)
		/// </summary>
		private static Type[] m_LeatherWeaponTypes = new Type[]
		{
			// Note: Leather weapons are rare. Add specific leather weapon types here if they exist in your shard.
			// For now, this array is empty as most weapons use metal or wood.
		};

		/// <summary>
		/// Metal armor types (plate, chain, ringmail)
		/// </summary>
		private static Type[] m_MetalArmorTypes = new Type[]
		{
			typeof( PlateChest ),				typeof( PlateChest ),				typeof( PlateChest ),
			typeof( PlateArms ),				typeof( PlateArms ),
			typeof( PlateLegs ),				typeof( PlateLegs ),				typeof( PlateLegs ),
			typeof( PlateGloves ),				typeof( PlateGloves ),				typeof( PlateGloves ),
			typeof( PlateGorget ),				typeof( PlateGorget ),
			typeof( ChainChest ),				typeof( ChainChest ),				typeof( ChainChest ),
			typeof( ChainLegs ),				typeof( ChainLegs ),				typeof( ChainLegs ),
			typeof( ChainCoif ),
			typeof( RingmailArms ),			typeof( RingmailArms ),
			typeof( RingmailChest ),			typeof( RingmailChest ),
			typeof( RingmailLegs ),			typeof( RingmailLegs ),
			typeof( RingmailGloves )
		};

		/// <summary>
		/// Leather armor types (leather, studded leather)
		/// </summary>
		private static Type[] m_LeatherArmorTypes = new Type[]
		{
			typeof( LeatherChest ),			typeof( LeatherChest ),			typeof( LeatherChest ),
			typeof( LeatherArms ),				typeof( LeatherArms ),				typeof( LeatherArms ),
			typeof( LeatherLegs ),				typeof( LeatherLegs ),
			typeof( LeatherGloves ),			typeof( LeatherGloves ),			typeof( LeatherGloves ),
			typeof( LeatherGorget ),			typeof( LeatherGorget ),
			typeof( StuddedChest ),			typeof( StuddedChest ),			typeof( StuddedChest ),
			typeof( StuddedArms ),				typeof( StuddedArms ),
			typeof( StuddedLegs ),				typeof( StuddedLegs ),
			typeof( StuddedGloves ),			typeof( StuddedGloves ),
			typeof( StuddedGorget )
		};

		/// <summary>
		/// Wood armor types (very limited - mostly shields)
		/// </summary>
		private static Type[] m_WoodArmorTypes = new Type[]
		{
			typeof( WoodenShield ),			typeof( WoodenShield ),
			typeof( WoodenKiteShield )
		};

		/// <summary>
		/// Basic tool types for random generation (lumber, mining, blacksmith, tailor, carpentry, fishing)
		/// </summary>
		private static Type[] m_ToolTypes = new Type[]
		{
			typeof( LumberAxe ),				typeof( LumberAxe ),				typeof( LumberAxe ),
			typeof( Pickaxe ),					typeof( Pickaxe ),					typeof( Pickaxe ),
			typeof( SmithHammer ),			typeof( SmithHammer ),			typeof( SmithHammer ),
			typeof( SewingKit ),				typeof( SewingKit ),				typeof( SewingKit ),
			typeof( Saw ),						typeof( Saw ),						typeof( Saw ),
			typeof( FishingPole ),			typeof( FishingPole ),			typeof( FishingPole )
		};

		/// <summary>
		/// High-level spell scroll types (Necromancy, Magery 4th-8th circle)
		/// </summary>
		private static Type[] m_HighLevelScrolls = new Type[]
		{
			// Necromancy scrolls - commented out
			//typeof( AnimateDeadScroll ),		typeof( ExorcismScroll ),
			//typeof( EvilOmenScroll ),			typeof( HorrificBeastScroll ),			typeof( LichFormScroll ),				typeof( MindRotScroll ),
			//typeof( PainSpikeScroll ),			typeof( PoisonStrikeScroll ),			typeof( StrangleScroll ),				typeof( SummonFamiliarScroll ),
			//typeof( VampiricEmbraceScroll ),	typeof( VengefulSpiritScroll ),			typeof( WitherScroll ),					typeof( WraithFormScroll ),
			// Magery scrolls (4th-8th circle)
			typeof( ArchCureScroll ),			typeof( ArchProtectionScroll ),			typeof( CurseScroll ),					typeof( FireFieldScroll ),
			typeof( GreaterHealScroll ),		typeof( LightningScroll ),				typeof( ManaDrainScroll ),				typeof( RecallScroll ),
			typeof( BladeSpiritsScroll ),		typeof( DispelFieldScroll ),			typeof( IncognitoScroll ),				typeof( MagicReflectScroll ),
			typeof( MindBlastScroll ),			typeof( ParalyzeScroll ),				typeof( PoisonFieldScroll ),			typeof( SummonCreatureScroll ),
			typeof( DispelScroll ),				typeof( EnergyBoltScroll ),				typeof( ExplosionScroll ),				typeof( InvisibilityScroll ),
			typeof( MarkScroll ),				typeof( MassCurseScroll ),				typeof( ParalyzeFieldScroll ),			typeof( RevealScroll ),
			typeof( ChainLightningScroll ),		typeof( EnergyFieldScroll ),			typeof( FlamestrikeScroll ),			typeof( GateTravelScroll ),
			typeof( ManaVampireScroll ),		typeof( MassDispelScroll ),				typeof( MeteorSwarmScroll ),			typeof( PolymorphScroll ),
			typeof( EarthquakeScroll ),			typeof( EnergyVortexScroll ),			typeof( ResurrectionScroll ),			typeof( SummonAirElementalScroll ),
			typeof( SummonDaemonScroll ),		typeof( SummonEarthElementalScroll ),	typeof( SummonFireElementalScroll ),	typeof( SummonWaterElementalScroll )
		};

		/// <summary>
		/// Lore book types (spellbooks, lore books, maps, learning books)
		/// </summary>
		private static Type[] m_LoreBooks = new Type[]
		{
			typeof( Spellbook ),				//typeof( NecromancerSpellbook ),			// Necromancy - commented out
			typeof( SongBook ),
			// Simple books - keeping only 3
			typeof( SomeRandomNote ),			typeof( LoreBook ),							typeof( BlueBook ),
			// Maps - keeping only WorldMapBottle, commenting out others
			//typeof( WorldMapLodor ),			typeof( WorldMapSosaria ),					
			typeof( WorldMapBottle ),	
			//typeof( WorldMapSerpent ),			typeof( WorldMapUmber ),					
			//typeof( WorldMapAmbrosia ),			typeof( WorldMapIslesOfDread ),				typeof( WorldMapSavage ),
			typeof( RuneMagicBook ),			typeof( MapRanger ),						typeof( GoldenRangers ),
			typeof( CBookDruidicHerbalism ),	//typeof( CBookNecroticAlchemy ),			// Necromancy - commented out
			typeof( LearnWoodBook ),
			typeof( LearnTraps ),				typeof( LearnTitles ),						typeof( LearnTailorBook ),
			typeof( LearnStealingBook ),		typeof( LearnScalesBook ),					typeof( LearnReagentsBook ),
			typeof( LearnMiscBook ),			typeof( LearnMetalBook ),					typeof( LearnLeatherBook ),
			typeof( LearnGraniteBook ),			typeof( AlchemicalMixtures ),				typeof( BookOfPoisons ),
			typeof( WorkShoppes ),				typeof( SwordsAndShackles ),				typeof( QuestTake ),
			typeof( EternalWar ), 				typeof( BloodLichCult ), typeof( BeASorcerer ), typeof( BeATroubadour )
		};

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the metal weapon types array
		/// </summary>
		public static Type[] MetalWeaponTypes{ get{ return m_MetalWeaponTypes; } }

		/// <summary>
		/// Gets the wood weapon types array
		/// </summary>
		public static Type[] WoodWeaponTypes{ get{ return m_WoodWeaponTypes; } }

		/// <summary>
		/// Gets the leather weapon types array
		/// </summary>
		public static Type[] LeatherWeaponTypes{ get{ return m_LeatherWeaponTypes; } }

		/// <summary>
		/// Gets the metal armor types array
		/// </summary>
		public static Type[] MetalArmorTypes{ get{ return m_MetalArmorTypes; } }

		/// <summary>
		/// Gets the leather armor types array
		/// </summary>
		public static Type[] LeatherArmorTypes{ get{ return m_LeatherArmorTypes; } }

		/// <summary>
		/// Gets the wood armor types array
		/// </summary>
		public static Type[] WoodArmorTypes{ get{ return m_WoodArmorTypes; } }

		/// <summary>
		/// Gets the tool types array
		/// </summary>
		public static Type[] ToolTypes{ get{ return m_ToolTypes; } }

		/// <summary>
		/// Gets the high-level scroll types array
		/// </summary>
		public static Type[] HighLevelScrolls{ get{ return m_HighLevelScrolls; } }

		/// <summary>
		/// Gets the lore book types array
		/// </summary>
		public static Type[] LoreBooks{ get{ return m_LoreBooks; } }

		#endregion

		#region Random Harvest Resource Method

		/// <summary>
		/// Returns a random harvest resource item (ingots, leather, boards, or reagents)
		/// If a reagent is selected, returns a pouch containing 2-6 different reagent types (mix of Magery and Alchemy)
		/// </summary>
		/// <returns>A random harvest resource item, or a pouch with reagents if reagent was selected</returns>
		public static Item RandomHarvestResource()
		{
			// Combine all resource types into a single array for random selection
			Type[] allResources = new Type[]
			{
				// Ingots (weighted - common ingots appear more often)
				typeof( IronIngot ),				typeof( IronIngot ),				typeof( IronIngot ),				typeof( IronIngot ),
				typeof( DullCopperIngot ),		typeof( DullCopperIngot ),			typeof( DullCopperIngot ),
				typeof( ShadowIronIngot ),		typeof( ShadowIronIngot ),			typeof( ShadowIronIngot ),
				typeof( CopperIngot ),			typeof( CopperIngot ),				typeof( CopperIngot ),
				typeof( BronzeIngot ),			typeof( BronzeIngot ),				typeof( BronzeIngot ),
				typeof( GoldIngot ),			typeof( GoldIngot ),
				typeof( AgapiteIngot ),		typeof( AgapiteIngot ),
				typeof( VeriteIngot ),
				typeof( ValoriteIngot ),
				typeof( TitaniumIngot ),
				typeof( RoseniumIngot ),
				typeof( PlatinumIngot ),

				// Ores (weighted - common ores appear more often, same types as ingots)
				typeof( IronOre ),				typeof( IronOre ),					typeof( IronOre ),					typeof( IronOre ),
				typeof( DullCopperOre ),		typeof( DullCopperOre ),			typeof( DullCopperOre ),
				typeof( ShadowIronOre ),		typeof( ShadowIronOre ),			typeof( ShadowIronOre ),
				typeof( CopperOre ),			typeof( CopperOre ),				typeof( CopperOre ),
				typeof( BronzeOre ),			typeof( BronzeOre ),				typeof( BronzeOre ),
				typeof( GoldOre ),				typeof( GoldOre ),
				typeof( AgapiteOre ),			typeof( AgapiteOre ),
				typeof( VeriteOre ),
				typeof( ValoriteOre ),
				typeof( TitaniumOre ),
				typeof( RoseniumOre ),
				typeof( PlatinumOre ),

				// Leather (weighted - common leather appears more often)
				typeof( HornedLeather ),		typeof( HornedLeather ),			typeof( HornedLeather ),
				typeof( BarbedLeather ),		typeof( BarbedLeather ),			typeof( BarbedLeather ),
				typeof( SpinedLeather ),		typeof( SpinedLeather ),
				typeof( VolcanicLeather ),		typeof( VolcanicLeather ),
				typeof( GoliathLeather ),

				// Boards (weighted - common boards appear more often)
				typeof( RosewoodBoard ),		typeof( RosewoodBoard ),			typeof( RosewoodBoard ),
				typeof( CherryBoard ),			typeof( CherryBoard ),				typeof( CherryBoard ),
				typeof( GoldenOakBoard ),		typeof( GoldenOakBoard ),			typeof( GoldenOakBoard ),
				typeof( ElvenBoard ),			typeof( ElvenBoard ),				typeof( ElvenBoard ),
				typeof( HickoryBoard ),			typeof( HickoryBoard ),			typeof( HickoryBoard ),

				// Logs (weighted - common logs appear more often, same types as boards)
				typeof( RosewoodLog ),			typeof( RosewoodLog ),			typeof( RosewoodLog ),
				typeof( CherryLog ),			typeof( CherryLog ),				typeof( CherryLog ),
				typeof( GoldenOakLog ),			typeof( GoldenOakLog ),			typeof( GoldenOakLog ),
				typeof( ElvenLog ),				typeof( ElvenLog ),				typeof( ElvenLog ),
				typeof( HickoryLog ),			typeof( HickoryLog ),				typeof( HickoryLog ),

				// Magery Reagents (weighted - all appear equally)
				// Note: When selected, will create a pouch with 2-6 different reagent types
				typeof( BlackPearl ),			typeof( BlackPearl ),				typeof( BlackPearl ),
				typeof( Bloodmoss ),			typeof( Bloodmoss ),				typeof( Bloodmoss ),
				typeof( Garlic ),				typeof( Garlic ),					typeof( Garlic ),
				typeof( Ginseng ),				typeof( Ginseng ),					typeof( Ginseng ),
				typeof( MandrakeRoot ),			typeof( MandrakeRoot ),			typeof( MandrakeRoot ),
				typeof( Nightshade ),			typeof( Nightshade ),				typeof( Nightshade ),
				typeof( SulfurousAsh ),			typeof( SulfurousAsh ),			typeof( SulfurousAsh ),
				typeof( SpidersSilk ),			typeof( SpidersSilk ),				typeof( SpidersSilk ),

				// Alchemy Reagents (weighted - all appear equally)
				// Note: When selected, will create a pouch with 2-6 different reagent types
				typeof( Brimstone ),			typeof( Brimstone ),				typeof( Brimstone ),
				typeof( ButterflyWings ),		typeof( ButterflyWings ),			typeof( ButterflyWings ),
				typeof( EyeOfToad ),			typeof( EyeOfToad ),				typeof( EyeOfToad ),
				typeof( FairyEgg ),				typeof( FairyEgg ),					typeof( FairyEgg ),
				typeof( GargoyleEar ),			typeof( GargoyleEar ),				typeof( GargoyleEar ),
				typeof( BeetleShell ),			typeof( BeetleShell ),				typeof( BeetleShell ),
				typeof( MoonCrystal ),			typeof( MoonCrystal ),				typeof( MoonCrystal ),
				typeof( PixieSkull ),			typeof( PixieSkull ),				typeof( PixieSkull ),

				// Dragon Scales (weighted - all appear equally)
				typeof( BlackScales ),			typeof( BlackScales ),				typeof( BlackScales ),
				typeof( BlueScales ),			typeof( BlueScales ),				typeof( BlueScales ),
				typeof( GreenScales ),			typeof( GreenScales ),				typeof( GreenScales ),
				typeof( RedScales ),			typeof( RedScales ),				typeof( RedScales ),
				typeof( WhiteScales ),			typeof( WhiteScales ),				typeof( WhiteScales ),
				typeof( YellowScales ),		typeof( YellowScales ),				typeof( YellowScales ),

				// Basic Materials (weighted - Leather more common)
				typeof( Leather ),				typeof( Leather ),					typeof( Leather ),					typeof( Leather ),
				typeof( UncutCloth ),

				// Fletching Materials (weighted - both appear equally)
				typeof( Feather ),				typeof( Feather ),					typeof( Feather ),					typeof( Feather ),
				typeof( Shaft ),				typeof( Shaft ),					typeof( Shaft ),					typeof( Shaft ),

				// Necromancy Reagents (weighted - all appear equally)
				typeof( BatWing ),				typeof( BatWing ),					typeof( BatWing ),
				typeof( GraveDust ),			typeof( GraveDust ),				typeof( GraveDust ),
				typeof( DaemonBlood ),			typeof( DaemonBlood ),				typeof( DaemonBlood ),
				typeof( NoxCrystal ),			typeof( NoxCrystal ),				typeof( NoxCrystal ),
				typeof( PigIron ),				typeof( PigIron ),					typeof( PigIron ),

				// Cloth items (weighted - Cotton most common, others equally)
				typeof( CottonCloth ),			typeof( CottonCloth ),			typeof( CottonCloth ),			typeof( CottonCloth ),
				typeof( FlaxCloth ),			typeof( FlaxCloth ),
				typeof( SilkCloth ),			typeof( SilkCloth ),
				typeof( WoolCloth ),			typeof( WoolCloth ),
				typeof( OilCloth ),			typeof( OilCloth ),
				typeof( BoltOfCloth ),			typeof( BoltOfCloth ),				typeof( BoltOfCloth )
			};

			Type selectedType = allResources[Utility.Random(allResources.Length)];
			Item selectedItem = Construct(selectedType);

			// Handle BoltOfCloth - set appropriate fabric resource (Cotton, Flax, Silk, Wool)
			if ( selectedItem is BoltOfCloth )
			{
				BoltOfCloth bolt = (BoltOfCloth)selectedItem;
				// Random fabric resource: Cotton (40%), Flax (20%), Silk (20%), Wool (20%)
				int fabricRoll = Utility.Random( 100 );
				if ( fabricRoll < 40 )
				{
					bolt.Resource = CraftResource.Cotton;
				}
				else if ( fabricRoll < 60 )
				{
					bolt.Resource = CraftResource.Flax;
				}
				else if ( fabricRoll < 80 )
				{
					bolt.Resource = CraftResource.Silk;
				}
				else
				{
					bolt.Resource = CraftResource.Wool;
				}
			}

			// Check if the selected item is a reagent (from Magery or Alchemy reagent types)
			bool isMageryReagent = Array.IndexOf(m_MageryReagentTypes, selectedType) >= 0;
			bool isAlchemyReagent = Array.IndexOf(m_AlchemyReagentTypes, selectedType) >= 0;

			if ( isMageryReagent || isAlchemyReagent )
			{
				// Create a pouch container with 2-6 different reagent types
				Container reagentPouch = new Pouch();
				reagentPouch.Name = "bolsa de reagentes";

				// Combine all reagent types into a single list
				List<Type> allReagentTypes = new List<Type>();
				allReagentTypes.AddRange(m_MageryReagentTypes);
				allReagentTypes.AddRange(m_AlchemyReagentTypes);

				// Remove duplicates (since arrays may have multiple entries of same type)
				List<Type> uniqueReagentTypes = new List<Type>();
				foreach ( Type reagentType in allReagentTypes )
				{
					if ( !uniqueReagentTypes.Contains( reagentType ) )
					{
						uniqueReagentTypes.Add( reagentType );
					}
				}

				// Generate 2-6 different reagent types
				int reagentCount = Utility.RandomMinMax( 2, 6 );
				List<Type> selectedReagentTypes = new List<Type>();

				// Randomly select unique reagent types
				for ( int i = 0; i < reagentCount && uniqueReagentTypes.Count > 0; i++ )
				{
					int randomIndex = Utility.Random( uniqueReagentTypes.Count );
					Type reagentType = uniqueReagentTypes[randomIndex];
					selectedReagentTypes.Add( reagentType );
					uniqueReagentTypes.RemoveAt( randomIndex ); // Remove to ensure uniqueness
				}

				// Create and add reagents to the pouch
				foreach ( Type reagentType in selectedReagentTypes )
				{
					Item reagent = Construct( reagentType );
					if ( reagent != null )
					{
						// Set random amount for each reagent (1-10)
						if ( reagent.Stackable )
						{
							reagent.Amount = Utility.RandomMinMax( 1, 10 );
						}
						reagentPouch.DropItem( reagent );
					}
				}

				// Delete the original selected reagent item since we're returning the pouch
				if ( selectedItem != null )
				{
					selectedItem.Delete();
				}

				return reagentPouch;
			}

			// Return the normal item for non-reagent resources
			return selectedItem;
		}

		#endregion

		#region Random Weapon Method

		/// <summary>
		/// Returns a random weapon with appropriate resource type based on weapon category
		/// Metal weapons can only be metal, wood weapons can only be wood, etc.
		/// </summary>
		/// <returns>A random weapon item with appropriate resource</returns>
		public static Item RandomWeapon()
		{
			BaseWeapon weapon = null;
			
			// Randomly select resource category: Metal (50%), Wood (40%), Leather (10%)
			// Leather weapons are rare, so they have lower chance
			int resourceCategory = Utility.Random( 100 );
			
			if ( resourceCategory < 50 )
			{
				// Metal weapons (50% chance)
				if ( m_MetalWeaponTypes.Length > 0 )
				{
					Type weaponType = m_MetalWeaponTypes[Utility.Random(m_MetalWeaponTypes.Length)];
					weapon = Construct(weaponType) as BaseWeapon;
					
					if ( weapon != null )
					{
						// Random metal resource (Agapite, Verite, Valorite, Platinum, Rosenium, Titanium)
						CraftResource[] metalResources = new CraftResource[]
						{
							CraftResource.Agapite,		CraftResource.Agapite,
							CraftResource.Verite,		CraftResource.Verite,
							CraftResource.Valorite,		CraftResource.Valorite,
							CraftResource.Platinum,		CraftResource.Platinum,
							CraftResource.Rosenium,		CraftResource.Rosenium,
							CraftResource.Titanium,		CraftResource.Titanium
						};
						weapon.Resource = metalResources[Utility.Random(metalResources.Length)];
						// Hue is automatically set when Resource is assigned (BaseWeapon.cs line 284-288)
					}
				}
			}
			else if ( resourceCategory < 90 )
			{
				// Wood weapons (40% chance)
				if ( m_WoodWeaponTypes.Length > 0 )
				{
					Type weaponType = m_WoodWeaponTypes[Utility.Random(m_WoodWeaponTypes.Length)];
					weapon = Construct(weaponType) as BaseWeapon;
					
					if ( weapon != null )
					{
						// Random wood resource (Elven, Rosewood, GoldenOak, Ash, Ebony)
						CraftResource[] woodResources = new CraftResource[]
						{
							CraftResource.ElvenTree,		CraftResource.ElvenTree,
							CraftResource.RosewoodTree,	CraftResource.RosewoodTree,
							CraftResource.GoldenOakTree,	CraftResource.GoldenOakTree,
							CraftResource.AshTree,		CraftResource.AshTree,
							CraftResource.EbonyTree,		CraftResource.EbonyTree
						};
						weapon.Resource = woodResources[Utility.Random(woodResources.Length)];
						// Hue is automatically set when Resource is assigned (BaseWeapon.cs line 284-288)
					}
				}
			}
			else
			{
				// Leather weapons (10% chance) - fallback to metal if no leather weapons available
				if ( m_LeatherWeaponTypes.Length > 0 )
				{
					Type weaponType = m_LeatherWeaponTypes[Utility.Random(m_LeatherWeaponTypes.Length)];
					weapon = Construct(weaponType) as BaseWeapon;
					
					if ( weapon != null )
					{
						// Random leather resource (Spined, Horned, Barbed, Volcanic)
						CraftResource[] leatherResources = new CraftResource[]
						{
							CraftResource.SpinedLeather,	CraftResource.SpinedLeather,
							CraftResource.HornedLeather,	CraftResource.HornedLeather,
							CraftResource.BarbedLeather,	CraftResource.BarbedLeather,
							CraftResource.VolcanicLeather,	CraftResource.VolcanicLeather
						};
						weapon.Resource = leatherResources[Utility.Random(leatherResources.Length)];
						// Hue is automatically set when Resource is assigned (BaseWeapon.cs line 284-288)
					}
				}
				else
				{
					// Fallback to metal weapon if no leather weapons available
					if ( m_MetalWeaponTypes.Length > 0 )
					{
						Type weaponType = m_MetalWeaponTypes[Utility.Random(m_MetalWeaponTypes.Length)];
						weapon = Construct(weaponType) as BaseWeapon;
						
						if ( weapon != null )
						{
							CraftResource[] metalResources = new CraftResource[]
							{
								CraftResource.Agapite,		CraftResource.Agapite,
								CraftResource.Verite,		CraftResource.Verite,
								CraftResource.Valorite,		CraftResource.Valorite,
								CraftResource.Platinum,		CraftResource.Platinum,
								CraftResource.Rosenium,		CraftResource.Rosenium,
								CraftResource.Titanium,		CraftResource.Titanium
							};
							weapon.Resource = metalResources[Utility.Random(metalResources.Length)];
							// Hue is automatically set when Resource is assigned (BaseWeapon.cs line 284-288)
						}
					}
				}
			}

			if ( weapon != null )
			{
				// Mark as player constructed for proper display
				weapon.PlayerConstructed = true;
			}

			return weapon;
		}

		#endregion

		#region Random Armor Method

		/// <summary>
		/// Returns a random armor piece with appropriate resource type based on armor category
		/// Metal armor can only be metal, leather armor can only be leather, etc.
		/// </summary>
		/// <returns>A random armor item with appropriate resource</returns>
		public static Item RandomArmor()
		{
			BaseArmor armor = null;
			
			// Randomly select resource category: Metal (50%), Leather (45%), Wood (5%)
			// Wood armor (shields) are rare, so they have lower chance
			int resourceCategory = Utility.Random( 100 );
			
			if ( resourceCategory < 50 )
			{
				// Metal armor (50% chance) - Plate, Chain, Ringmail
				if ( m_MetalArmorTypes.Length > 0 )
				{
					Type armorType = m_MetalArmorTypes[Utility.Random(m_MetalArmorTypes.Length)];
					armor = Construct(armorType) as BaseArmor;
					
					if ( armor != null )
					{
						// Random metal resource (Agapite, Verite, Valorite, Platinum, Rosenium, Titanium)
						CraftResource[] metalResources = new CraftResource[]
						{
							CraftResource.Agapite,		CraftResource.Agapite,
							CraftResource.Verite,		CraftResource.Verite,
							CraftResource.Valorite,		CraftResource.Valorite,
							CraftResource.Platinum,		CraftResource.Platinum,
							CraftResource.Rosenium,		CraftResource.Rosenium,
							CraftResource.Titanium,		CraftResource.Titanium
						};
						armor.Resource = metalResources[Utility.Random(metalResources.Length)];
						// Hue is automatically set when Resource is assigned (BaseArmor.cs line 224)
					}
				}
			}
			else if ( resourceCategory < 95 )
			{
				// Leather armor (45% chance) - Leather, Studded Leather
				if ( m_LeatherArmorTypes.Length > 0 )
				{
					Type armorType = m_LeatherArmorTypes[Utility.Random(m_LeatherArmorTypes.Length)];
					armor = Construct(armorType) as BaseArmor;
					
					if ( armor != null )
					{
						// Random leather resource (Spined, Horned, Barbed, Volcanic)
						CraftResource[] leatherResources = new CraftResource[]
						{
							CraftResource.SpinedLeather,	CraftResource.SpinedLeather,
							CraftResource.HornedLeather,	CraftResource.HornedLeather,
							CraftResource.BarbedLeather,	CraftResource.BarbedLeather,
							CraftResource.VolcanicLeather,	CraftResource.VolcanicLeather
						};
						armor.Resource = leatherResources[Utility.Random(leatherResources.Length)];
						// Hue is automatically set when Resource is assigned (BaseArmor.cs line 224)
					}
				}
			}
			else
			{
				// Wood armor (5% chance) - Wooden Shields
				if ( m_WoodArmorTypes.Length > 0 )
				{
					Type armorType = m_WoodArmorTypes[Utility.Random(m_WoodArmorTypes.Length)];
					armor = Construct(armorType) as BaseArmor;
					
					if ( armor != null )
					{
						// Random wood resource (Elven, Rosewood, GoldenOak, Ash, Ebony)
						CraftResource[] woodResources = new CraftResource[]
						{
							CraftResource.ElvenTree,		CraftResource.ElvenTree,
							CraftResource.RosewoodTree,	CraftResource.RosewoodTree,
							CraftResource.GoldenOakTree,	CraftResource.GoldenOakTree,
							CraftResource.AshTree,		CraftResource.AshTree,
							CraftResource.EbonyTree,		CraftResource.EbonyTree
						};
						armor.Resource = woodResources[Utility.Random(woodResources.Length)];
						// Hue is automatically set when Resource is assigned (BaseArmor.cs line 224)
					}
				}
				else
				{
					// Fallback to leather armor if no wood armor available
					if ( m_LeatherArmorTypes.Length > 0 )
					{
						Type armorType = m_LeatherArmorTypes[Utility.Random(m_LeatherArmorTypes.Length)];
						armor = Construct(armorType) as BaseArmor;
						
						if ( armor != null )
						{
							CraftResource[] leatherResources = new CraftResource[]
							{
								CraftResource.SpinedLeather,	CraftResource.SpinedLeather,
								CraftResource.HornedLeather,	CraftResource.HornedLeather,
								CraftResource.BarbedLeather,	CraftResource.BarbedLeather,
								CraftResource.VolcanicLeather,	CraftResource.VolcanicLeather
							};
							armor.Resource = leatherResources[Utility.Random(leatherResources.Length)];
							// Hue is automatically set when Resource is assigned (BaseArmor.cs line 224)
						}
					}
				}
			}

			if ( armor != null )
			{
				// Mark as player constructed for proper display
				armor.PlayerConstructed = true;
			}

			return armor;
		}

		#endregion

		#region Random Tools Method

		/// <summary>
		/// Returns a random tool for basic skills (lumber, mining, blacksmith, tailor, carpentry, fishing)
		/// </summary>
		/// <returns>A random tool item</returns>
		public static Item RandomTools()
		{
			return Construct(m_ToolTypes);
		}

		#endregion

		#region Random High-Level Scrolls Method

		/// <summary>
		/// Returns a random high-level spell scroll (Necromancy, Magery 4th-8th circle)
		/// </summary>
		/// <returns>A random high-level spell scroll</returns>
		public static Item RandomHighLevelScroll()
		{
			return Construct(m_HighLevelScrolls);
		}

		#endregion

		#region Random Lore Books Method

		/// <summary>
		/// Returns a random lore book (spellbooks, lore books, maps, learning books)
		/// </summary>
		/// <returns>A random lore book item</returns>
		public static Item RandomLoreBook()
		{
			return Construct(m_LoreBooks);
		}

		#endregion

		#region DDRelics Types

		/// <summary>
		/// DDRelic types with rarity weighting
		/// Normal: DDRelicPainting, DDRelicArts (most common - ~67%)
		/// Hard: DDRelicBook (less common - ~17%)
		/// Rare: DDRelicStatue (least common - ~17%)
		/// Total: 12 entries (8 normal, 2 hard, 2 rare)
		/// </summary>
		private static Type[] m_DDRelicTypes = new Type[]
		{
			// Normal rarity (most common - 8 entries = 66.67%)
			typeof( DDRelicPainting ),		typeof( DDRelicPainting ),		typeof( DDRelicPainting ),		typeof( DDRelicPainting ),
			typeof( DDRelicArts ),			typeof( DDRelicArts ),			typeof( DDRelicArts ),			typeof( DDRelicArts ),
			// Hard rarity (less common - 2 entries = 16.67%)
			typeof( DDRelicBook ),			typeof( DDRelicBook ),
			// Rare rarity (least common - 2 entries = 16.67%)
			typeof( DDRelicStatue ),		typeof( DDRelicStatue )
		};

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the DDRelic types array
		/// </summary>
		public static Type[] DDRelicTypes{ get{ return m_DDRelicTypes; } }

		#endregion

		#region Random DDRelics Method

		/// <summary>
		/// Returns a random DDRelic item with appropriate rarity weighting
		/// Normal: DDRelicPainting, DDRelicArts (most common)
		/// Hard: DDRelicBook (less common)
		/// Rare: DDRelicStatue (least common)
		/// </summary>
		/// <returns>A random DDRelic item</returns>
		public static Item RandomDDRelic()
		{
			return Construct(m_DDRelicTypes);
		}

		#endregion

		#region Alchemy Recipe Types

		/// <summary>
		/// Alchemy potion types for recipe generation
		/// Hard: GreaterExplosionPotion, GreaterCurePotion, GreaterHealPotion, GreaterStrengthPotion, GreaterPoisonPotion (2x weighted)
		/// Rare: TotalRefreshPotion, DeadlyPoisonPotion, LethalPoisonPotion (1x weighted)
		/// Normal: All other potions (4x weighted)
		/// </summary>
		private static Type[] m_AlchemyRecipePotionTypes = new Type[]
		{
			// Normal recipes (4x weighted - most common)
			typeof( AgilityPotion ),				typeof( AgilityPotion ),				typeof( AgilityPotion ),				typeof( AgilityPotion ),
			typeof( GreaterAgilityPotion ),			typeof( GreaterAgilityPotion ),		typeof( GreaterAgilityPotion ),		typeof( GreaterAgilityPotion ),
			typeof( LesserCurePotion ),				typeof( LesserCurePotion ),			typeof( LesserCurePotion ),			typeof( LesserCurePotion ),
			typeof( CurePotion ),					typeof( CurePotion ),				typeof( CurePotion ),				typeof( CurePotion ),
			typeof( LesserExplosionPotion ),		typeof( LesserExplosionPotion ),	typeof( LesserExplosionPotion ),	typeof( LesserExplosionPotion ),
			typeof( ExplosionPotion ),				typeof( ExplosionPotion ),			typeof( ExplosionPotion ),			typeof( ExplosionPotion ),
			typeof( LesserHealPotion ),				typeof( LesserHealPotion ),			typeof( LesserHealPotion ),			typeof( LesserHealPotion ),
			typeof( HealPotion ),					typeof( HealPotion ),				typeof( HealPotion ),				typeof( HealPotion ),
			typeof( NightSightPotion ),				typeof( NightSightPotion ),			typeof( NightSightPotion ),			typeof( NightSightPotion ),
			typeof( RefreshPotion ),					typeof( RefreshPotion ),				typeof( RefreshPotion ),				typeof( RefreshPotion ),
			typeof( StrengthPotion ),				typeof( StrengthPotion ),			typeof( StrengthPotion ),			typeof( StrengthPotion ),
			typeof( LesserPoisonPotion ),			typeof( LesserPoisonPotion ),		typeof( LesserPoisonPotion ),		typeof( LesserPoisonPotion ),
			typeof( PoisonPotion ),					typeof( PoisonPotion ),				typeof( PoisonPotion ),				typeof( PoisonPotion ),
			typeof( HairOilPotion ),					typeof( HairOilPotion ),			typeof( HairOilPotion ),			typeof( HairOilPotion ),
			typeof( HairDyePotion ),					typeof( HairDyePotion ),			typeof( HairDyePotion ),			typeof( HairDyePotion ),

			// Hard recipes (2x weighted - less common)
			typeof( GreaterExplosionPotion ),		typeof( GreaterExplosionPotion ),
			typeof( GreaterCurePotion ),				typeof( GreaterCurePotion ),
			typeof( GreaterHealPotion ),			typeof( GreaterHealPotion ),
			typeof( GreaterStrengthPotion ),		typeof( GreaterStrengthPotion ),
			typeof( GreaterPoisonPotion ),			typeof( GreaterPoisonPotion ),

			// Rare recipes (1x weighted - least common)
			typeof( TotalRefreshPotion ),
			typeof( DeadlyPoisonPotion ),
			typeof( LethalPoisonPotion )
		};

		/// <summary>
		/// Gets the alchemy recipe potion types array
		/// </summary>
		public static Type[] AlchemyRecipePotionTypes{ get{ return m_AlchemyRecipePotionTypes; } }

		#endregion

		#region Random Alchemy Recipe Methods

		/// <summary>
		/// Returns a random alchemy recipe scroll from a specific category
		/// </summary>
		/// <param name="category">Category index (0=Basic, 1=Advanced, 2=Special, 3=Cosmetic)</param>
		/// <returns>A random AlchemyRecipeScroll from the specified category, or null if category is empty</returns>
		public static Item RandomAlchemyRecipeByCategory( int category )
		{
			// Get all recipes in the specified category
			System.Collections.Generic.List<Server.Engines.Craft.AlchemyRecipeInfo> recipes = 
				Server.Engines.Craft.AlchemyRecipeData.GetRecipesByCategory( category );
			
			if ( recipes == null || recipes.Count == 0 )
			{
				return null;
			}
			
			// Get a random recipe from the category
			Server.Engines.Craft.AlchemyRecipeInfo randomRecipe = recipes[Utility.Random( recipes.Count )];
			
			// Create and return AlchemyRecipeScroll with the recipe ID
			return new AlchemyRecipeScroll( randomRecipe.RecipeID );
		}

		/// <summary>
		/// Returns a random alchemy recipe scroll with appropriate rarity weighting
		/// Hard: GreaterExplosionPotion, GreaterCurePotion, GreaterHealPotion, GreaterStrengthPotion, GreaterPoisonPotion (2x weighted)
		/// Rare: TotalRefreshPotion, DeadlyPoisonPotion, LethalPoisonPotion (1x weighted)
		/// Normal: All other potions (4x weighted)
		/// </summary>
		/// <returns>A random AlchemyRecipeScroll for an alchemy recipe</returns>
		public static Item RandomAlchemyRecipe()
		{
			// Get a random potion type from the weighted array
			Type potionType = m_AlchemyRecipePotionTypes[Utility.Random(m_AlchemyRecipePotionTypes.Length)];

			// Find the craft item for this potion type in the Alchemy craft system
			Server.Engines.Craft.CraftSystem alchemySystem = Server.Engines.Craft.DefAlchemy.CraftSystem;
			
			if ( alchemySystem == null )
			{
				return null;
			}

			// Ensure craft system is initialized
			if ( alchemySystem.CraftItems == null || alchemySystem.CraftItems.Count == 0 )
			{
				// Craft system not initialized yet, try to initialize it
				alchemySystem.InitCraftList();
			}
			
			// Search through craft items to find one that matches this potion type
			Server.Engines.Craft.CraftItem craftItem = alchemySystem.CraftItems.SearchFor( potionType );
			
			if ( craftItem == null )
			{
				return null;
			}

			// All alchemy recipes should already be assigned (recipe IDs 500-534)
			// If craft item doesn't have a recipe, return null (shouldn't happen)
			if ( craftItem.Recipe == null )
			{
				return null;
			}
			
			// Get the recipe ID and create AlchemyRecipeScroll
			int recipeID = craftItem.Recipe.ID;
			return new AlchemyRecipeScroll( recipeID );
		}

		#endregion

		#region Random PotionKeg Method

		/// <summary>
		/// Returns a random PotionKeg with 5-15 potions
		/// Types: Lesser Mana, Lesser Invisibility, Lesser Poison, Lesser Heal, Lesser Cure, Lesser Explosion,
		///        Cure, Invisibility, Mana, Agility, Strength, Poison, Heal, Explosion
		/// </summary>
		/// <returns>A random PotionKeg item</returns>
		public static Item RandomPotionKeg()
		{
			PotionKeg keg = new PotionKeg();
			
			// Random potion effect from available types
			PotionEffect[] potionEffects = new PotionEffect[]
			{
				// Lesser potions
				PotionEffect.ManaLesser,
				PotionEffect.InvisibilityLesser,
				PotionEffect.PoisonLesser,
				PotionEffect.HealLesser,
				PotionEffect.CureLesser,
				PotionEffect.ExplosionLesser,
				
				// Regular potions
				PotionEffect.Cure,
				PotionEffect.Invisibility,
				PotionEffect.Mana,
				PotionEffect.Agility,
				PotionEffect.Strength,
				PotionEffect.Poison,
				PotionEffect.Heal,
				PotionEffect.Explosion
			};
			
			// Randomly select a potion effect
			PotionEffect selectedEffect = potionEffects[Utility.Random(potionEffects.Length)];
			
			// Set keg properties (hue is automatically set when Type is assigned)
			keg.Held = Utility.RandomMinMax( 5, 15 );
			keg.Type = selectedEffect;
			
			return keg;
		}

		#endregion

		#region Special NMS Items Types

		/// <summary>
		/// Special NMS item types (rare special items)
		/// </summary>
		private static Type[] m_SpecialNMSItems = new Type[]
		{
			typeof( ElectrumFlask )
		};

		/// <summary>
		/// Gets the special NMS item types array
		/// </summary>
		public static Type[] SpecialNMSItems{ get{ return m_SpecialNMSItems; } }

		#endregion

		#region Random Special NMS Items Method

		/// <summary>
		/// Returns a random special NMS item
		/// </summary>
		/// <returns>A random special NMS item</returns>
		public static Item RandomSpecialNMSItem()
		{
			return Construct(m_SpecialNMSItems);
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Constructs an item instance from the given type
		/// </summary>
		/// <param name="type">The type of item to construct</param>
		/// <returns>The constructed item, or null if construction failed</returns>
		public static Item Construct( Type type )
		{
			try
			{
				return Activator.CreateInstance( type ) as Item;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Constructs a random item from the given types array
		/// </summary>
		/// <param name="types">Array of types to choose from</param>
		/// <returns>The constructed item, or null if construction failed</returns>
		public static Item Construct( Type[] types )
		{
			if ( types.Length > 0 )
				return Construct( types, Utility.Random( types.Length ) );

			return null;
		}

		/// <summary>
		/// Constructs an item from the given types array at the specified index
		/// </summary>
		/// <param name="types">Array of types to choose from</param>
		/// <param name="index">Index of the type to construct</param>
		/// <returns>The constructed item, or null if construction failed</returns>
		public static Item Construct( Type[] types, int index )
		{
			if ( index >= 0 && index < types.Length )
				return Construct( types[index] );

			return null;
		}

		#endregion
	}
}


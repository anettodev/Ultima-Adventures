using System;
using System.Collections.Generic;
using Server.Network;
using Server.Targeting;
using Server.Misc;

namespace Server.Items
{
	/// <summary>
	/// NewFish - Exotic fish that can be caught while fishing.
	/// Fish have random names, colors, and gold values based on type.
	/// </summary>
	public class NewFish : Item, ICarvable
	{
		#region Static Data

		/// <summary>
		/// Structure to hold special fish data
		/// </summary>
		private struct SpecialFishData
		{
			public string Name;
			public int GoldMin;
			public int GoldMax;
			public double Weight;

			public SpecialFishData( string name, int goldMin, int goldMax, double weight )
			{
				Name = name;
				GoldMin = goldMin;
				GoldMax = goldMax;
				Weight = weight;
			}
		}

		/// <summary>
		/// Dictionary mapping special ItemIDs to their fish data
		/// </summary>
		private static readonly Dictionary<int, SpecialFishData> SpecialFish = new Dictionary<int, SpecialFishData>
		{
			{ NewFishConstants.ITEM_ID_MARLIN_1, new SpecialFishData( FishingStringConstants.FISH_NAME_MARLIN, NewFishConstants.GOLD_VALUE_MIN_MARLIN, NewFishConstants.GOLD_VALUE_MAX_MARLIN, NewFishConstants.WEIGHT_HEAVY ) },
			{ NewFishConstants.ITEM_ID_MARLIN_2, new SpecialFishData( FishingStringConstants.FISH_NAME_MARLIN, NewFishConstants.GOLD_VALUE_MIN_MARLIN, NewFishConstants.GOLD_VALUE_MAX_MARLIN, NewFishConstants.WEIGHT_HEAVY ) },
			{ NewFishConstants.ITEM_ID_SHARK, new SpecialFishData( FishingStringConstants.FISH_NAME_SHARK, NewFishConstants.GOLD_VALUE_MIN_SHARK, NewFishConstants.GOLD_VALUE_MAX_SHARK, NewFishConstants.WEIGHT_HEAVY ) },
			{ NewFishConstants.ITEM_ID_SEAHORSE, new SpecialFishData( FishingStringConstants.FISH_NAME_SEAHORSE, NewFishConstants.GOLD_VALUE_MIN_SEAHORSE, NewFishConstants.GOLD_VALUE_MAX_SEAHORSE, NewFishConstants.WEIGHT_STANDARD ) },
			{ NewFishConstants.ITEM_ID_STINGRAY_1, new SpecialFishData( FishingStringConstants.FISH_NAME_STINGRAY, NewFishConstants.GOLD_VALUE_MIN_STINGRAY, NewFishConstants.GOLD_VALUE_MAX_STINGRAY, NewFishConstants.WEIGHT_MEDIUM ) },
			{ NewFishConstants.ITEM_ID_STINGRAY_2, new SpecialFishData( FishingStringConstants.FISH_NAME_STINGRAY, NewFishConstants.GOLD_VALUE_MIN_STINGRAY, NewFishConstants.GOLD_VALUE_MAX_STINGRAY, NewFishConstants.WEIGHT_MEDIUM ) },
			{ NewFishConstants.ITEM_ID_SQUID, new SpecialFishData( FishingStringConstants.FISH_NAME_SQUID, NewFishConstants.GOLD_VALUE_MIN_SQUID, NewFishConstants.GOLD_VALUE_MAX_SQUID, NewFishConstants.WEIGHT_STANDARD ) },
			{ NewFishConstants.ITEM_ID_OCTOPUS, new SpecialFishData( FishingStringConstants.FISH_NAME_OCTOPUS, NewFishConstants.GOLD_VALUE_MIN_OCTOPUS, NewFishConstants.GOLD_VALUE_MAX_OCTOPUS, NewFishConstants.WEIGHT_STANDARD ) },
			{ NewFishConstants.ITEM_ID_CRAB, new SpecialFishData( FishingStringConstants.FISH_NAME_CRAB, NewFishConstants.GOLD_VALUE_MIN_CRAB, NewFishConstants.GOLD_VALUE_MAX_CRAB, NewFishConstants.WEIGHT_STANDARD ) }
		};

		/// <summary>
		/// Array of standard fish names
		/// </summary>
		private static readonly string[] StandardFishNames = new string[]
		{
			FishingStringConstants.FISH_NAME_FLYING,
			FishingStringConstants.FISH_NAME_ANGLER,
			FishingStringConstants.FISH_NAME_BARB,
			FishingStringConstants.FISH_NAME_BARRACUDA,
			FishingStringConstants.FISH_NAME_CARP,
			FishingStringConstants.FISH_NAME_CATALUFA,
			FishingStringConstants.FISH_NAME_COD,
			FishingStringConstants.FISH_NAME_SARDINE,
			FishingStringConstants.FISH_NAME_TILAPIA,
			FishingStringConstants.FISH_NAME_FLY,
			FishingStringConstants.FISH_NAME_FLOUNDER,
			FishingStringConstants.FISH_NAME_GROUPER,
			FishingStringConstants.FISH_NAME_GULPER,
			FishingStringConstants.FISH_NAME_GUNNEL,
			FishingStringConstants.FISH_NAME_HAKE,
			FishingStringConstants.FISH_NAME_WHITING,
			FishingStringConstants.FISH_NAME_SALMON,
			FishingStringConstants.FISH_NAME_SHARK,
			FishingStringConstants.FISH_NAME_TROUT,
			FishingStringConstants.FISH_NAME_TUNA
		};

		/// <summary>
		/// Array of all possible fish ItemIDs
		/// </summary>
		private static readonly int[] FishItemIDs = new int[]
		{
			0x52DA, 0x52DB, 0x52DC, 0x52DD, 0x52DE, 0x52DF, 0x52E0, 0x52E1, 0x52C9, 0x531E, 0x531F, 0x534F, 0x5350,
			0x22AF, 0x22AE, 0x22AD, 0x22AC, 0x22AB, 0x22AA, 0x22A7, 0x22A8, 0x22B2, 0x22B1, 0x22B0,
			0x44C3, 0x44C4, 0x44C5, 0x44C6, 0x4302, 0x4303, 0x4304, 0x4305, 0x4306, 0x4307,
			0x9CC, 0x9CD, 0x9CE, 0x9CF
		};

		/// <summary>
		/// Dictionary mapping material names to their color suffix strings
		/// </summary>
		private static readonly Dictionary<string, string> MaterialSuffixes = new Dictionary<string, string>
		{
			{ "copper", FishingStringConstants.FISH_MATERIAL_COPPER },
			{ "verite", FishingStringConstants.FISH_MATERIAL_VERITE },
			{ "valorite", FishingStringConstants.FISH_MATERIAL_VALORITE },
			{ "agapite", FishingStringConstants.FISH_MATERIAL_AGAPITE },
			{ "bronze", FishingStringConstants.FISH_MATERIAL_BRONZE },
			{ "dull copper", FishingStringConstants.FISH_MATERIAL_COPPERY },
			{ "gold", FishingStringConstants.FISH_MATERIAL_GOLDEN },
			{ "shadow iron", FishingStringConstants.FISH_MATERIAL_BLACK },
			{ "topaz", FishingStringConstants.FISH_MATERIAL_TOPAZ },
			{ "amethyst", FishingStringConstants.FISH_MATERIAL_AMETHYST },
			{ "marble", FishingStringConstants.FISH_MATERIAL_MARBLE },
			{ "onyx", FishingStringConstants.FISH_MATERIAL_ONYX },
			{ "ruby", FishingStringConstants.FISH_MATERIAL_RUBY },
			{ "sapphire", FishingStringConstants.FISH_MATERIAL_SAPPHIRE },
			{ "silver", FishingStringConstants.FISH_MATERIAL_SILVER }
		};

		/// <summary>
		/// Array of material names in order
		/// </summary>
		private static readonly string[] MaterialNames = new string[]
		{
			"copper", "verite", "valorite", "agapite", "bronze", "dull copper",
			"gold", "shadow iron", "topaz", "amethyst", "marble", "onyx", "ruby", "sapphire", "silver"
		};

		#endregion

		#region Fields

		public int FishGoldValue;

		#endregion

		#region Properties

		[CommandProperty(AccessLevel.Owner)]
		public int FishGold_Value 
		{ 
			get { return FishGoldValue; } 
			set { FishGoldValue = value; InvalidateProperties(); } 
		}

		#endregion

		#region Constructors

		[Constructable]
		public NewFish() : base( NewFishConstants.ITEM_ID_BASE )
		{
			ItemID = Utility.RandomList( FishItemIDs );
			Weight = NewFishConstants.WEIGHT_STANDARD;
			FishGoldValue = Utility.RandomMinMax( NewFishConstants.GOLD_VALUE_MIN_STANDARD, NewFishConstants.GOLD_VALUE_MAX_STANDARD );

			string fishName = GetFishName();
			ApplySpecialFishData( ref fishName );

			string fishType = GetRandomFishType();
			ApplyFishColor( fishName, fishType );
		}

		public NewFish( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Core Methods

		/// <summary>
		/// Carves the fish into fish steaks
		/// </summary>
		/// <param name="from">The mobile carving the fish</param>
		/// <param name="item">The item being carved</param>
		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), NewFishConstants.CARVE_STEAK_COUNT );
		}

		/// <summary>
		/// Adds name properties to the fish
		/// </summary>
		/// <param name="list">The property list</param>
		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
			list.Add( 1070722, ItemNameHue.UnifiedItemProps.SetColor( FishingStringConstants.PROP_EXOTIC_FISH, FishingStringConstants.COLOR_CYAN ) );
			list.Add( 1049644, FishingStringConstants.FormatGoldValue( FishGoldValue ) );
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Gets a random standard fish name
		/// </summary>
		/// <returns>Random fish name</returns>
		private string GetFishName()
		{
			return StandardFishNames[Utility.RandomMinMax( 0, StandardFishNames.Length - 1 )];
		}

		/// <summary>
		/// Gets a random fish type
		/// </summary>
		/// <returns>Fish type string</returns>
		private string GetRandomFishType()
		{
			string[] types = new string[] { FishingStringConstants.FISH_TYPE_EYED, FishingStringConstants.FISH_TYPE_COLORS };
			return types[Utility.RandomMinMax( 0, types.Length - 1 )];
		}

		/// <summary>
		/// Applies special fish data if this ItemID matches a special fish
		/// </summary>
		/// <param name="fishName">Reference to fish name (may be modified)</param>
		private void ApplySpecialFishData( ref string fishName )
		{
			SpecialFishData specialData;
			if ( SpecialFish.TryGetValue( ItemID, out specialData ) )
			{
				fishName = specialData.Name;
				FishGoldValue = Utility.RandomMinMax( specialData.GoldMin, specialData.GoldMax );
				Weight = specialData.Weight;
			}
		}

		/// <summary>
		/// Applies color/material to the fish name and hue
		/// </summary>
		/// <param name="fishName">Base fish name</param>
		/// <param name="fishType">Fish type</param>
		private void ApplyFishColor( string fishName, string fishType )
		{
			if ( fishType == FishingStringConstants.FISH_TYPE_COLORS )
			{
				ApplyColorVariant( fishName );
			}
			else
			{
				Name = fishName;
			}
		}

		/// <summary>
		/// Applies a color variant to the fish
		/// </summary>
		/// <param name="fishName">Base fish name</param>
		private void ApplyColorVariant( string fishName )
		{
			switch( Utility.Random( NewFishConstants.COLOR_TYPE_COUNT ) )
			{
				case 0:
					Name = fishName + FishingStringConstants.FISH_COLOR_RED;
					Hue = GetHue( 1 );
					break;
				case 1:
					Name = fishName + FishingStringConstants.FISH_COLOR_BLUE;
					Hue = GetHue( 2 );
					break;
				case 2:
					Name = fishName + FishingStringConstants.FISH_COLOR_GREEN;
					Hue = GetHue( 3 );
					break;
				case 3:
					Name = fishName + FishingStringConstants.FISH_COLOR_YELLOW;
					Hue = GetHue( 4 );
					break;
				case 4:
					Name = fishName + FishingStringConstants.FISH_COLOR_ORANGE;
					Hue = GetHue( 9 );
					break;
				case 5:
					Name = fishName + FishingStringConstants.FISH_COLOR_PINK;
					Hue = GetHue( 10 );
					break;
				case 6:
					Name = fishName + FishingStringConstants.FISH_COLOR_EMERALD;
					Hue = Utility.RandomList( NewFishConstants.HUE_EMERALD );
					break;
				case 7:
					Name = fishName + FishingStringConstants.FISH_COLOR_FIRE;
					Hue = Utility.RandomList( NewFishConstants.HUE_FIRE );
					break;
				case 8:
					Name = fishName + FishingStringConstants.FISH_COLOR_COLDWATER;
					Hue = Utility.RandomList( NewFishConstants.HUE_COLDWATER );
					break;
				case 9:
					Name = fishName + FishingStringConstants.FISH_COLOR_POISONOUS;
					Hue = Utility.RandomList( NewFishConstants.HUE_POISON );
					break;
				case 10:
					ApplyMaterialVariant( fishName );
					break;
			}
		}

		/// <summary>
		/// Applies a material variant to the fish
		/// </summary>
		/// <param name="fishName">Base fish name</param>
		private void ApplyMaterialVariant( string fishName )
		{
			int materialIndex = Utility.Random( NewFishConstants.MATERIAL_TYPE_COUNT );
			string materialName = MaterialNames[materialIndex];
			string suffix = MaterialSuffixes[materialName];

			Name = fishName + suffix;
			Hue = MaterialInfo.GetMaterialColor( materialName, "classic", 0 );
		}

		/// <summary>
		/// Gets a random hue based on color index
		/// </summary>
		/// <param name="color">Color index (0-11)</param>
		/// <returns>Random hue value</returns>
		public static int GetHue( int color )
		{
			if ( color < 0 )
			{
				color = Utility.Random( NewFishConstants.HUE_COLOR_MAX );
			}

			int hue = 0;
			switch( color )
			{
				case 0: hue = Utility.RandomNeutralHue(); break;
				case 1: hue = Utility.RandomRedHue(); break;
				case 2: hue = Utility.RandomBlueHue(); break;
				case 3: hue = Utility.RandomGreenHue(); break;
				case 4: hue = Utility.RandomYellowHue(); break;
				case 5: hue = Utility.RandomSnakeHue(); break;
				case 6: hue = Utility.RandomMetalHue(); break;
				case 7: hue = Utility.RandomAnimalHue(); break;
				case 8: hue = Utility.RandomSlimeHue(); break;
				case 9: hue = Utility.RandomOrangeHue(); break;
				case 10: hue = Utility.RandomPinkHue(); break;
				case 11: hue = Utility.RandomDyedHue(); break;
			}
			return hue;
		}

		#endregion

		#region Serialization

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( FishGoldValue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			FishGoldValue = reader.ReadInt();
		}

		#endregion
	}
}

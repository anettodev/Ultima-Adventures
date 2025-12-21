using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using System.Text;
using Server.Commands;
using Server.Commands.Generic;
using System.IO;
using Server.Mobiles;
using System.Threading;
using Server.Gumps;
using Server.Accounting;
using Server.Regions;
using System.Globalization;

namespace Server.Mobiles
{
	/// <summary>
	/// Citizens NPC class - Represents various types of citizen NPCs that provide services,
	/// sell items, and interact with players in towns and cities.
	/// </summary>
	public class Citizens : BaseCreature
	{
		#region Properties and Fields

		public override bool PlayerRangeSensitive { get { return true; } }

		public int CitizenService;
		[CommandProperty(AccessLevel.Owner)]
		public int Citizen_Service { get { return CitizenService; } set { CitizenService = value; InvalidateProperties(); } }

		public int CitizenType;
		[CommandProperty(AccessLevel.Owner)]
		public int Citizen_Type { get { return CitizenType; } set { CitizenType = value; InvalidateProperties(); } }

		public int CitizenCost;
		[CommandProperty(AccessLevel.Owner)]
		public int Citizen_Cost { get { return CitizenCost; } set { CitizenCost = value; InvalidateProperties(); } }

		public string CitizenPhrase;
		[CommandProperty(AccessLevel.Owner)]
		public string Citizen_Phrase { get { return CitizenPhrase; } set { CitizenPhrase = value; InvalidateProperties(); } }

		public string CitizenRumor;
		[CommandProperty(AccessLevel.Owner)]
		public string Citizen_Rumor { get { return CitizenRumor; } set { CitizenRumor = value; InvalidateProperties(); } }

		public override bool InitialInnocent{ get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		public DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the Citizens class.
		/// Sets up appearance, stats, skills, and calls SetupCitizen to configure services.
		/// </summary>
		[Constructable]
		public Citizens() : base( AIType.AI_Citizen, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			if ( Female = Utility.RandomBool() ) 
			{ 
				Body = CitizensConstants.BODY_FEMALE; 
				Name = NameList.RandomName( "female" );
			}
			else 
			{ 
				Body = CitizensConstants.BODY_MALE; 			
				Name = NameList.RandomName( "male" ); 
				FacialHairItemID = Utility.RandomList( CitizensConstants.FACIAL_HAIR_IDS );
			}

			switch ( Utility.Random( 3 ) )
			{
				case 0: Server.Misc.IntelligentAction.DressUpWizards( this ); 				CitizenType = CitizensConstants.CITIZEN_TYPE_WIZARD;	break;
				case 1: Server.Misc.IntelligentAction.DressUpFighters( this, "", false, 0 );	CitizenType = CitizensConstants.CITIZEN_TYPE_FIGHTER;	break;
				case 2: Server.Misc.IntelligentAction.DressUpRogues( this, "", false, 0, "" );		CitizenType = CitizensConstants.CITIZEN_TYPE_ROGUE;	break;
			}

			CitizenCost = 0;
			CitizenService = 0;

			SetupCitizen();

			CantWalk = true;
			Title = TavernPatrons.GetTitle();
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();
			Utility.AssignRandomHair( this );
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			NameHue = Utility.RandomOrangeHue();
			AI = AIType.AI_Citizen;
			FightMode = FightMode.None;

			SetStr( CitizensConstants.STAT_STR_MIN, CitizensConstants.STAT_STR_MAX );
			SetDex( CitizensConstants.STAT_DEX_MIN, CitizensConstants.STAT_DEX_MAX );
			SetInt( CitizensConstants.STAT_INT_MIN, CitizensConstants.STAT_INT_MAX );

			SetHits( CitizensConstants.HITS_MIN, CitizensConstants.HITS_MAX );

			SetDamage( CitizensConstants.DAMAGE_MIN, CitizensConstants.DAMAGE_MAX );

			SetDamageType( ResistanceType.Physical, CitizensConstants.DAMAGE_TYPE_PHYSICAL );

			SetResistance( ResistanceType.Physical, CitizensConstants.RESIST_PHYSICAL_MIN, CitizensConstants.RESIST_PHYSICAL_MAX );
			SetResistance( ResistanceType.Fire, CitizensConstants.RESIST_FIRE_MIN, CitizensConstants.RESIST_FIRE_MAX );
			SetResistance( ResistanceType.Cold, CitizensConstants.RESIST_COLD_MIN, CitizensConstants.RESIST_COLD_MAX );
			SetResistance( ResistanceType.Poison, CitizensConstants.RESIST_POISON_MIN, CitizensConstants.RESIST_POISON_MAX );
			SetResistance( ResistanceType.Energy, CitizensConstants.RESIST_ENERGY_MIN, CitizensConstants.RESIST_ENERGY_MAX );

			SetSkill( SkillName.DetectHidden, CitizensConstants.SKILL_MIN, CitizensConstants.SKILL_MAX );
			SetSkill( SkillName.Anatomy, CitizensConstants.SKILL_MIN, CitizensConstants.SKILL_MAX );
			SetSkill( SkillName.Poisoning, CitizensConstants.SKILL_MIN, CitizensConstants.SKILL_MAX );
			SetSkill( SkillName.MagicResist, CitizensConstants.SKILL_MIN, CitizensConstants.SKILL_MAX );
			SetSkill( SkillName.Tactics, CitizensConstants.SKILL_MIN, CitizensConstants.SKILL_MAX );
			SetSkill( SkillName.Wrestling, CitizensConstants.SKILL_MIN, CitizensConstants.SKILL_MAX );
			SetSkill( SkillName.Swords, CitizensConstants.SKILL_MIN, CitizensConstants.SKILL_MAX );
			SetSkill( SkillName.Fencing, CitizensConstants.SKILL_MIN, CitizensConstants.SKILL_MAX );
			SetSkill( SkillName.Macing, CitizensConstants.SKILL_MIN, CitizensConstants.SKILL_MAX );

			Fame = 0;
			Karma = 0;
			VirtualArmor = CitizensConstants.VIRTUAL_ARMOR;

			int HairColor = Utility.RandomHairHue();
			HairHue = HairColor;
			FacialHairHue = HairColor;

			if ( this is HouseVisitor && Backpack != null ){ Backpack.Delete(); }
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Checks if an item ID is a metal weapon that should be colored
		/// </summary>
		/// <param name="itemID">The item ID to check</param>
		/// <returns>True if the item is a metal weapon</returns>
		private bool IsMetalWeapon(int itemID)
		{
			foreach (int metalWeaponID in CitizensConstants.METAL_WEAPON_IDS)
			{
				if (itemID == metalWeaponID)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Gets wand charges based on IntRequirement
		/// </summary>
		/// <param name="intRequirement">The intelligence requirement of the wand</param>
		/// <returns>The number of charges the wand should have</returns>
		private int GetWandCharges(int intRequirement)
		{
			switch (intRequirement)
			{
				case 10: return CitizensConstants.WAND_CHARGES_10;
				case 15: return CitizensConstants.WAND_CHARGES_15;
				case 20: return CitizensConstants.WAND_CHARGES_20;
				case 25: return CitizensConstants.WAND_CHARGES_25;
				case 30: return CitizensConstants.WAND_CHARGES_30;
				case 35: return CitizensConstants.WAND_CHARGES_35;
				case 40: return CitizensConstants.WAND_CHARGES_40;
				case 45: return CitizensConstants.WAND_CHARGES_45;
				default: return 0;
			}
		}

		#endregion

		#region Service Setup Helpers

		/// <summary>
		/// Sets up service for a wizard citizen.
		/// </summary>
		private void SetupWizardService()
		{
			if ( Utility.RandomMinMax( CitizensConstants.SPECIAL_TYPE_RANDOM_MIN, CitizensConstants.WIZARD_SERVICE_CHANCE ) == CitizensConstants.WIZARD_SERVICE_THRESHOLD )
			{
				CitizenService = Utility.RandomMinMax( CitizensConstants.CITIZEN_SERVICE_REPAIR, CitizensConstants.CITIZEN_SERVICE_WAND );
			}
		}

		/// <summary>
		/// Sets up service for a smith citizen.
		/// </summary>
		private void SetupSmithService()
		{
			CitizenService = 0;
			CitizenType = 0;
			switch ( Utility.RandomMinMax( CitizensConstants.SPECIAL_TYPE_RANDOM_MIN, CitizensConstants.SPECIAL_TYPE_RANDOM_MAX ) )
			{
				case 1: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR;		CitizenType = CitizensConstants.CITIZEN_TYPE_FIGHTER;	break;
				case 2: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR_2;		CitizenType = CitizensConstants.CITIZEN_TYPE_FIGHTER;	break;
				case 3: CitizenService = CitizensConstants.CITIZEN_TYPE_METAL_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_METAL_VENDOR;	break;
				case 4: CitizenService = CitizensConstants.CITIZEN_TYPE_METAL_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_METAL_VENDOR;	break;
				case 5: CitizenService = CitizensConstants.CITIZEN_TYPE_METAL_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_METAL_VENDOR;	break;
			}
		}

		/// <summary>
		/// Sets up service for a lumberjack citizen.
		/// </summary>
		private void SetupLumberjackService()
		{
			CitizenService = 0;
			CitizenType = 0;
			switch ( Utility.RandomMinMax( CitizensConstants.SPECIAL_TYPE_RANDOM_MIN, CitizensConstants.SPECIAL_TYPE_RANDOM_MAX ) )
			{
				case 1: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR_WOOD_WEAPON;		CitizenType = CitizensConstants.CITIZEN_TYPE_FIGHTER;	break;
				case 2: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR_WOOD_ARMOR;		CitizenType = CitizensConstants.CITIZEN_TYPE_FIGHTER;	break;
				case 3: CitizenService = CitizensConstants.CITIZEN_TYPE_WOOD_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_WOOD_VENDOR;	break;
				case 4: CitizenService = CitizensConstants.CITIZEN_TYPE_WOOD_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_WOOD_VENDOR;	break;
				case 5: CitizenService = CitizensConstants.CITIZEN_TYPE_WOOD_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_WOOD_VENDOR;	break;
			}
		}

		/// <summary>
		/// Sets up service for a leather worker citizen.
		/// </summary>
		private void SetupLeatherService()
		{
			CitizenService = 0;
			CitizenType = 0;
			switch ( Utility.RandomMinMax( CitizensConstants.SPECIAL_TYPE_RANDOM_MIN, CitizensConstants.SPECIAL_TYPE_RANDOM_MAX ) )
			{
				case 1: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR_2;		CitizenType = CitizensConstants.CITIZEN_TYPE_LEATHER_VENDOR;	break;
				case 2: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR_2;		CitizenType = CitizensConstants.CITIZEN_TYPE_LEATHER_VENDOR;	break;
				case 3: CitizenService = CitizensConstants.CITIZEN_TYPE_LEATHER_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_LEATHER_VENDOR;	break;
				case 4: CitizenService = CitizensConstants.CITIZEN_TYPE_LEATHER_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_LEATHER_VENDOR;	break;
				case 5: CitizenService = CitizensConstants.CITIZEN_TYPE_LEATHER_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_LEATHER_VENDOR;	break;
			}
		}

		/// <summary>
		/// Sets up service for a miner citizen.
		/// </summary>
		private void SetupMinerService()
		{
			CitizenService = 0;
			CitizenType = 0;
			switch ( Utility.RandomMinMax( CitizensConstants.SPECIAL_TYPE_RANDOM_MIN, CitizensConstants.SPECIAL_TYPE_RANDOM_MAX ) )
			{
				case 1: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR;		CitizenType = CitizensConstants.CITIZEN_TYPE_FIGHTER;	break;
				case 2: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR_2;		CitizenType = CitizensConstants.CITIZEN_TYPE_FIGHTER;	break;
				case 3: CitizenService = CitizensConstants.CITIZEN_TYPE_ORE_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_ORE_VENDOR;	break;
				case 4: CitizenService = CitizensConstants.CITIZEN_TYPE_ORE_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_ORE_VENDOR;	break;
				case 5: CitizenService = CitizensConstants.CITIZEN_TYPE_ORE_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_ORE_VENDOR;	break;
			}
		}

		/// <summary>
		/// Sets up service for a smelter citizen.
		/// </summary>
		private void SetupSmelterService()
		{
			CitizenService = 0;
			CitizenType = 0;
			switch ( Utility.RandomMinMax( CitizensConstants.SPECIAL_TYPE_RANDOM_MIN, CitizensConstants.SPECIAL_TYPE_RANDOM_MAX ) )
			{
				case 1: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR;		CitizenType = CitizensConstants.CITIZEN_TYPE_FIGHTER;	break;
				case 2: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR_2;		CitizenType = CitizensConstants.CITIZEN_TYPE_FIGHTER;	break;
				case 3: CitizenService = CitizensConstants.CITIZEN_TYPE_METAL_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_METAL_VENDOR;	break;
				case 4: CitizenService = CitizensConstants.CITIZEN_TYPE_METAL_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_METAL_VENDOR;	break;
				case 5: CitizenService = CitizensConstants.CITIZEN_TYPE_ORE_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_ORE_VENDOR;	break;
				case 6: CitizenService = CitizensConstants.CITIZEN_TYPE_ORE_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_ORE_VENDOR;	break;
			}
		}

		/// <summary>
		/// Sets up service for an alchemist citizen.
		/// </summary>
		private void SetupAlchemistService()
		{
			CitizenService = 0;
			CitizenType = 0;
			switch ( Utility.RandomMinMax( CitizensConstants.SPECIAL_TYPE_RANDOM_MIN, CitizensConstants.SPECIAL_TYPE_RANDOM_MAX ) )
			{
				case 1: CitizenService = CitizensConstants.CITIZEN_TYPE_REAGENT_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_REAGENT_VENDOR;	break;
				case 2: CitizenService = CitizensConstants.CITIZEN_TYPE_REAGENT_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_REAGENT_VENDOR;	break;
				case 3: CitizenService = CitizensConstants.CITIZEN_TYPE_POTION_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_POTION_VENDOR;	break;
				case 4: CitizenService = CitizensConstants.CITIZEN_TYPE_POTION_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_POTION_VENDOR;	break;
			}
		}

		/// <summary>
		/// Sets up service for a cook citizen.
		/// </summary>
		private void SetupCookService()
		{
			CitizenService = 0;
			CitizenType = 0;
			switch ( Utility.RandomMinMax( CitizensConstants.SPECIAL_TYPE_RANDOM_MIN, CitizensConstants.SPECIAL_TYPE_RANDOM_MAX ) )
			{
				case 1: CitizenService = CitizensConstants.CITIZEN_TYPE_FOOD_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_FOOD_VENDOR;	break;
				case 2: CitizenService = CitizensConstants.CITIZEN_TYPE_FOOD_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_FOOD_VENDOR;	break;
				case 3: CitizenService = CitizensConstants.CITIZEN_TYPE_FOOD_VENDOR;	CitizenType = CitizensConstants.CITIZEN_TYPE_FOOD_VENDOR;	break;
			}
		}

		/// <summary>
		/// Sets up service for a default citizen (fighter or rogue).
		/// </summary>
		private void SetupDefaultService()
		{
			switch ( Utility.RandomMinMax( CitizensConstants.SPECIAL_TYPE_RANDOM_MIN, CitizensConstants.SPECIAL_TYPE_RANDOM_MAX ) )
			{
				case 1: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR;		break;
				case 2: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR_2;		break;
				case 3: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR_WOOD_WEAPON;		break;
				case 4: CitizenService = CitizensConstants.CITIZEN_SERVICE_REPAIR_WOOD_ARMOR;		break;
				case 5: CitizenService = CitizensConstants.CITIZEN_SERVICE_MAGIC_ITEM;		break;
			}
		}

		#endregion

		#region Crate Creation Helpers

		/// <summary>
		/// Generates a material vendor phrase based on the crate details and location.
		/// </summary>
		/// <param name="phrase">The initial greeting phrase</param>
		/// <param name="quantity">The quantity of items in the crate</param>
		/// <param name="materialName">The name of the material</param>
		/// <param name="itemType">The type of item (e.g., "ingots", "boards", "leather")</param>
		/// <param name="actionVerb">The action verb (e.g., "mined", "chopped", "skinned")</param>
		/// <param name="locationType">The location type (e.g., "cave", "forest")</param>
		/// <param name="dungeon">The dungeon name</param>
		/// <param name="city">The city name</param>
		/// <returns>The complete material vendor phrase</returns>
		private string GenerateMaterialVendorPhrase(string phrase, int quantity, string materialName, string itemType, string actionVerb, string locationType, string dungeon, string city)
		{
			string sell = CitizensStringConstants.WILLING_PART_WITH;
			if (Utility.RandomBool())
			{
				sell = CitizensStringConstants.WILLING_TRADE;
			}
			else if (Utility.RandomBool())
			{
				sell = CitizensStringConstants.WILLING_SELL;
			}

			string location = dungeon;
			string locationPreposition = CitizensStringConstants.LOCATION_NEAR;
			switch (Utility.RandomMinMax(0, 5))
			{
				case 0: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_NEAR; break;
				case 1: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_OUTSIDE; break;
				case 2: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_BY; break;
				case 3: location = city; locationPreposition = CitizensStringConstants.LOCATION_NEAR; break;
				case 4: location = city; locationPreposition = CitizensStringConstants.LOCATION_BY; break;
				case 5: location = city; locationPreposition = CitizensStringConstants.LOCATION_OUTSIDE; break;
			}

			string fullPhrase = phrase + " " + string.Format(CitizensStringConstants.MATERIAL_VENDOR_PHRASE_FORMAT, quantity, materialName, itemType, actionVerb, locationType, locationPreposition, location, sell);
			fullPhrase = fullPhrase + " " + string.Format(CitizensStringConstants.MATERIAL_VENDOR_CLOSING_FORMAT, itemType);
			return fullPhrase;
		}

		/// <summary>
		/// Configures a material crate with quantity, cost, and properties.
		/// </summary>
		/// <param name="crate">The crate to configure</param>
		/// <param name="materialName">The name of the material</param>
		/// <param name="itemID">The item ID for the crate</param>
		/// <param name="hue">The hue for the crate</param>
		/// <param name="baseQty">The base quantity</param>
		/// <param name="cost">The cost per item</param>
		/// <param name="itemType">The type of item (e.g., "ingots", "boards")</param>
		private void ConfigureMaterialCrate(Item crate, string materialName, int itemID, int hue, int baseQty, int cost, string itemType)
		{
			if (crate is CrateOfMetal)
			{
				CrateOfMetal metalCrate = (CrateOfMetal)crate;
				metalCrate.CrateQty = Utility.RandomMinMax(baseQty * 5, baseQty * 15);
				metalCrate.CrateItem = materialName;
				metalCrate.Hue = hue;
				metalCrate.ItemID = itemID;
				metalCrate.Name = "crate of " + materialName + " " + itemType;
				metalCrate.Weight = metalCrate.CrateQty * CitizensConstants.CRATE_WEIGHT_MULTIPLIER;
				CitizenCost = metalCrate.CrateQty * cost;
			}
			else if (crate is CrateOfWood)
			{
				CrateOfWood woodCrate = (CrateOfWood)crate;
				woodCrate.CrateQty = Utility.RandomMinMax(baseQty * 5, baseQty * 15);
				woodCrate.CrateItem = materialName;
				woodCrate.Hue = hue;
				woodCrate.ItemID = itemID;
				woodCrate.Name = "crate of " + materialName + " " + itemType;
				woodCrate.Weight = woodCrate.CrateQty * CitizensConstants.CRATE_WEIGHT_MULTIPLIER;
				CitizenCost = woodCrate.CrateQty * cost;
			}
			else if (crate is CrateOfLeather)
			{
				CrateOfLeather leatherCrate = (CrateOfLeather)crate;
				leatherCrate.CrateQty = Utility.RandomMinMax(baseQty * 5, baseQty * 15);
				leatherCrate.CrateItem = materialName;
				leatherCrate.Hue = hue;
				leatherCrate.ItemID = itemID;
				leatherCrate.Name = "crate of " + materialName + " " + itemType;
				leatherCrate.Weight = leatherCrate.CrateQty * CitizensConstants.CRATE_WEIGHT_MULTIPLIER;
				CitizenCost = leatherCrate.CrateQty * cost;
			}
			else if (crate is CrateOfOre)
			{
				CrateOfOre oreCrate = (CrateOfOre)crate;
				oreCrate.CrateQty = Utility.RandomMinMax(baseQty * 5, baseQty * 15);
				oreCrate.CrateItem = materialName;
				oreCrate.Hue = hue;
				oreCrate.ItemID = itemID;
				oreCrate.Name = "crate of " + materialName + " " + itemType;
				oreCrate.Weight = oreCrate.CrateQty * CitizensConstants.CRATE_WEIGHT_MULTIPLIER;
				CitizenCost = oreCrate.CrateQty * cost;
			}
		}

		#endregion

		#region Setup Methods

		/// <summary>
		/// Sets up the citizen's equipment, services, phrases, and inventory.
		/// This is the main configuration method called during construction.
		/// </summary>
		public void SetupCitizen()
		{
			TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;

			if ( Backpack != null ){ Backpack.Delete(); }
			Container pack = new Backpack();
			pack.Movable = false;
			AddItem( pack );

			if ( this.FindItemOnLayer( Layer.OneHanded ) != null )
			{
				Item myOneHand = this.FindItemOnLayer( Layer.OneHanded );

				if ( IsMetalWeapon( myOneHand.ItemID ) )
				{
					Server.Misc.MaterialInfo.ColorMetal( myOneHand, 0 );
				}
				else
				{
					Server.Misc.MorphingTime.ChangeMaterialType( myOneHand, this );
				}
			}

			if ( this.FindItemOnLayer( Layer.TwoHanded ) != null )
			{
				Item myTwoHand = this.FindItemOnLayer( Layer.TwoHanded );

				if ( IsMetalWeapon( myTwoHand.ItemID ) )
				{
					Server.Misc.MaterialInfo.ColorMetal( myTwoHand, 0 );
				}
				else
				{
					Server.Misc.MorphingTime.ChangeMaterialType( myTwoHand, this );
				}
			}

			string dungeon = QuestCharacters.SomePlace( "tavern" );
				if ( Utility.RandomMinMax( CitizensConstants.RANDOM_DUNGEON_MIN, CitizensConstants.RANDOM_DUNGEON_MAX ) == CitizensConstants.RANDOM_DUNGEON_THRESHOLD ){ dungeon = RandomThings.MadeUpDungeon(); }

			string Clues = QuestCharacters.SomePlace( "tavern" );
				if ( Utility.RandomMinMax( CitizensConstants.RANDOM_DUNGEON_MIN, CitizensConstants.RANDOM_DUNGEON_MAX ) == CitizensConstants.RANDOM_DUNGEON_THRESHOLD ){ Clues = RandomThings.MadeUpDungeon(); }

			string city = RandomThings.GetRandomCity();
				if ( Utility.RandomMinMax( CitizensConstants.RANDOM_DUNGEON_MIN, CitizensConstants.RANDOM_DUNGEON_MAX ) == CitizensConstants.RANDOM_DUNGEON_THRESHOLD ){ city = RandomThings.MadeUpCity(); }

			string adventurer = Server.Misc.TavernPatrons.Adventurer();

			int relic = Utility.RandomMinMax( CitizensConstants.RELIC_MIN, CitizensConstants.RELIC_MAX );
			string item = Server.Items.SomeRandomNote.GetSpecialItem( relic, 1 );
				item = "the '" + cultInfo.ToTitleCase(item) + "'";

			string locale = Server.Items.SomeRandomNote.GetSpecialItem( relic, 0 );

			if ( Utility.RandomMinMax( CitizensConstants.RANDOM_DUNGEON_MIN, CitizensConstants.RANDOM_DUNGEON_MAX ) > CitizensConstants.RANDOM_DUNGEON_THRESHOLD )
			{
				item = QuestCharacters.QuestItems( true );
				locale = dungeon;
			}

			string preface = CitizensStringConstants.PREFACE_FOUND;

			int topic = Utility.RandomMinMax( CitizensConstants.TOPIC_MIN, CitizensConstants.TOPIC_MAX );
				if ( this is HouseVisitor ){ topic = CitizensConstants.TOPIC_HOUSE_VISITOR; }

			switch ( topic )
			{
				case 0:	CitizenRumor = string.Format( CitizensStringConstants.RUMOR_TEMPLATE_1_FORMAT, item, locale ); break;
				case 1:	CitizenRumor = string.Format( CitizensStringConstants.RUMOR_TEMPLATE_2_FORMAT, item, locale ); break;
				case 2:	CitizenRumor = string.Format( CitizensStringConstants.RUMOR_TEMPLATE_3_FORMAT, locale, item ); break;
				case 3:	CitizenRumor = string.Format( CitizensStringConstants.RUMOR_TEMPLATE_4_FORMAT, locale, item ); break;
				case 4:	CitizenRumor = string.Format( CitizensStringConstants.RUMOR_TEMPLATE_5_FORMAT, QuestCharacters.RandomWords(), item, locale ); break;
				case 5:	CitizenRumor = string.Format( CitizensStringConstants.RUMOR_TEMPLATE_6_FORMAT, RandomThings.GetRandomJob(), item, locale ); break;
				case 6:	CitizenRumor = string.Format( CitizensStringConstants.RUMOR_TEMPLATE_7_FORMAT, QuestCharacters.RandomWords(), item, locale ); break;
				case 7:	CitizenRumor = string.Format( CitizensStringConstants.RUMOR_TEMPLATE_8_FORMAT, item, locale ); break;
				case 8:	CitizenRumor = string.Format( CitizensStringConstants.RUMOR_TEMPLATE_9_FORMAT, RandomThings.GetRandomCity(), locale, item ); break;
				case 9:	CitizenRumor = Server.Misc.TavernPatrons.GetRareLocation( this, true, false );		break;
			}

			switch( Utility.RandomMinMax( CitizensConstants.PREFACE_MIN, CitizensConstants.PREFACE_MAX ) )
			{
				case 0: preface = CitizensStringConstants.PREFACE_FOUND; 											break;
				case 1: preface = CitizensStringConstants.PREFACE_HEARD_RUMOURS; 								break;
				case 2: preface = CitizensStringConstants.PREFACE_HEARD_STORY; 								break;
				case 3: preface = CitizensStringConstants.PREFACE_OVERHEARD; 						break;
				case 4: preface = string.Format( CitizensStringConstants.PREFACE_ADVENTURER_FOUND_FORMAT, adventurer ); 						break;
				case 5: preface = string.Format( CitizensStringConstants.PREFACE_ADVENTURER_HEARD_RUMOURS_FORMAT, adventurer ); 		break;
				case 6: preface = string.Format( CitizensStringConstants.PREFACE_ADVENTURER_HEARD_STORY_FORMAT, adventurer ); 		break;
				case 7: preface = string.Format( CitizensStringConstants.PREFACE_ADVENTURER_OVERHEARD_FORMAT, adventurer ); 	break;
				case 8: preface = string.Format( CitizensStringConstants.PREFACE_ADVENTURER_SPREADING_FORMAT, adventurer ); 	break;
				case 9: preface = string.Format( CitizensStringConstants.PREFACE_ADVENTURER_TELLING_TALES_FORMAT, adventurer ); 	break;
				case 10: preface = CitizensStringConstants.PREFACE_WE_FOUND; 											break;
				case 11: preface = CitizensStringConstants.PREFACE_WE_HEARD_RUMOURS; 							break;
				case 12: preface = CitizensStringConstants.PREFACE_WE_HEARD_STORY; 							break;
				case 13: preface = CitizensStringConstants.PREFACE_WE_OVERHEARD; 						break;
			}

			if ( CitizenRumor == null ){ CitizenRumor = preface + " " + Server.Misc.TavernPatrons.CommonTalk( "", city, dungeon, this, adventurer, true ) + "."; }

			if ( this is HouseVisitor )
			{
				CitizenService = 0;
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_WIZARD )
			{
				SetupWizardService();
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_SMITH )
			{
				SetupSmithService();
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_LUMBERJACK )
			{
				SetupLumberjackService();
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_LEATHER )
			{
				SetupLeatherService();
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_MINER )
			{
				SetupMinerService();
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_SMELTER )
			{
				SetupSmelterService();
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_ALCHEMIST )
			{
				SetupAlchemistService();
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_COOK )
			{
				SetupCookService();
			}
			else
			{
				SetupDefaultService();
			}

			string phrase = "";

			int initPhrase = Utility.RandomMinMax( CitizensConstants.INIT_PHRASE_MIN, CitizensConstants.INIT_PHRASE_MAX );
				if ( this is TavernPatronNorth || this is TavernPatronSouth || this is TavernPatronEast || this is TavernPatronWest ){ initPhrase = Utility.RandomMinMax( CitizensConstants.INIT_PHRASE_MIN, CitizensConstants.INIT_PHRASE_MAX_TAVERN ); }

			switch ( initPhrase )
			{
				case 0:	phrase = CitizensStringConstants.GREETING_1; break;
				case 1:	phrase = CitizensStringConstants.GREETING_2; break;
				case 2:	phrase = CitizensStringConstants.GREETING_3; break;
				case 3:	phrase = CitizensStringConstants.GREETING_4; break;
				case 4:	phrase = string.Format( CitizensStringConstants.GREETING_5_FORMAT, dungeon ); break;
				case 5:	phrase = CitizensStringConstants.GREETING_6_FORMAT; break;
				case 6:	phrase = CitizensStringConstants.GREETING_7; break;
			}

			if ( CitizenService == CitizensConstants.CITIZEN_SERVICE_REPAIR )
			{
				if ( CitizenType == CitizensConstants.CITIZEN_TYPE_WIZARD ){ CitizenPhrase = phrase + " " + CitizensStringConstants.SERVICE_WAND_RECHARGE; }
				else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_FIGHTER ){ CitizenPhrase = phrase + " " + CitizensStringConstants.SERVICE_BLACKSMITH_ARMOR; }
				else { CitizenPhrase = phrase + " " + CitizensStringConstants.SERVICE_UNLOCK; }
			}
			else if ( CitizenService == CitizensConstants.CITIZEN_SERVICE_REPAIR_2 )
			{
				if ( CitizenType == CitizensConstants.CITIZEN_TYPE_FIGHTER ){ CitizenPhrase = phrase + " " + CitizensStringConstants.SERVICE_BLACKSMITH_WEAPON; }
				else { CitizenPhrase = phrase + " " + CitizensStringConstants.SERVICE_LEATHER_WORKER; }
			}
			else if ( CitizenService == CitizensConstants.CITIZEN_SERVICE_REPAIR_WOOD_WEAPON )
			{
				if ( CitizenType == CitizensConstants.CITIZEN_TYPE_FIGHTER ){ CitizenPhrase = phrase + " " + CitizensStringConstants.SERVICE_WOOD_WORKER_WEAPON; }
				else { CitizenPhrase = phrase + " " + CitizensStringConstants.SERVICE_WOOD_WORKER_WEAPON; }
			}
			else if ( CitizenService == CitizensConstants.CITIZEN_SERVICE_REPAIR_WOOD_ARMOR )
			{
				if ( CitizenType == CitizensConstants.CITIZEN_TYPE_FIGHTER ){ CitizenPhrase = phrase + " " + CitizensStringConstants.SERVICE_WOOD_WORKER_ARMOR; }
				else { CitizenPhrase = phrase + " " + CitizensStringConstants.SERVICE_WOOD_WORKER_ARMOR; }
			}
			else if ( CitizenService == CitizensConstants.CITIZEN_SERVICE_MAGIC_ITEM )
			{
				string aty1 = CitizensStringConstants.ITEM_DESC_MAGIC; 
				if (Utility.RandomBool() ){ aty1 = CitizensStringConstants.ITEM_DESC_ENCHANTED; } 
				else if (Utility.RandomBool() ){ aty1 = CitizensStringConstants.ITEM_DESC_SPECIAL; }
				
				string aty2 = CitizensStringConstants.ACTION_FOUND; 
				if (Utility.RandomBool() ){ aty2 = CitizensStringConstants.ACTION_DISCOVERED; }
				
				string aty3 = CitizensStringConstants.WILLING_PART_WITH; 
				if (Utility.RandomBool() ){ aty3 = CitizensStringConstants.WILLING_TRADE; } 
				else if (Utility.RandomBool() ){ aty3 = CitizensStringConstants.WILLING_SELL; }

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.MAGIC_ITEM_SALE_1_FORMAT, aty1, aty2, Clues, aty3 ); break;
					case 1:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.MAGIC_ITEM_SALE_2_FORMAT, aty1, city, aty3 ); break;
					case 2:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.MAGIC_ITEM_SALE_3_FORMAT, aty1, aty2, adventurer, aty3 ); break;
					case 3:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.MAGIC_ITEM_SALE_4_FORMAT, aty1, aty2, Clues, aty3 ); break;
					case 4:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.MAGIC_ITEM_SALE_5_FORMAT, aty1, aty2, Clues, aty3 ); break;
					case 5:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.MAGIC_ITEM_SALE_6_FORMAT, aty1, aty2, adventurer, Clues, aty3 ); break;
				}
				CitizenPhrase = CitizenPhrase + " " + CitizensStringConstants.MAGIC_ITEM_SALE_CLOSING;
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_METAL_VENDOR && CitizenService == CitizensConstants.CITIZEN_TYPE_METAL_VENDOR )
			{
				dungeon = RandomThings.MadeUpDungeon();
				city = RandomThings.MadeUpCity();

				CrateOfMetal crate = new CrateOfMetal();

				int ingot = Utility.RandomMinMax( 1, 65536 );

				string metal;
				int steel;
				int hue;
				int qty;
				int cost;

				if ( ingot >= 32768 ){ metal = "iron"; steel = 0x5094; hue = 0; qty = 80; cost = 2; }
				else if ( ingot >= 16384 ){ metal = "dull copper"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "dull copper", "", 0 ); qty = 75; cost = 4; }
				else if ( ingot >= 8192 ){ metal = "shadow iron"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ); qty = 70; cost = 6; }
				else if ( ingot >= 4096 ){ metal = "copper"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "copper", "", 0 ); qty = 65; cost = 8; }
				else if ( ingot >= 2048 ){ metal = "bronze"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "bronze", "", 0 ); qty = 60; cost = 10; }
				else if ( ingot >= 1024 ){ metal = "gold"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "gold", "", 0 ); qty = 55; cost = 12; }
				else if ( ingot >= 512 ){ metal = "agapite"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "agapite", "", 0 ); qty = 50; cost = 14; }
				else if ( ingot >= 256 ){ metal = "verite"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "verite", "", 0 ); qty = 45; cost = 16; }
				else if ( ingot >= 128 ){ metal = "valorite"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "valorite", "", 0 ); qty = 40; cost = 18; }
				else if ( ingot >= 64 ){ metal = "nepturite"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "nepturite", "", 0 ); qty = 35; cost = 20; }
				else if ( ingot >= 32 ){ metal = "obsidian"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "obsidian", "", 0 ); qty = 30; cost = 22; }
				else if ( ingot >= 16 ){ metal = "steel"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "steel", "", 0 ); qty = 25; cost = 24; }
				else if ( ingot >= 8 ){ metal = "brass"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "brass", "", 0 ); qty = 20; cost = 26; }
				else if ( ingot >= 4 ){ metal = "mithril"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "mithril", "", 0 ); qty = 15; cost = 28; }
				else if ( ingot >= 2 ){ metal = "xormite"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "xormite", "", 0 ); qty = 10; cost = 30; }
				else { metal = "dwarven"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "dwarven", "", 0 ); qty = 5; cost = 32; }

				crate.CrateQty = Utility.RandomMinMax( qty*5, qty*15 );
				crate.CrateItem = metal;
				crate.Hue = hue;
				crate.ItemID = steel;
				crate.Name = "crate of " + metal + " ingots";
				crate.Weight = crate.CrateQty * 0.1;
				CitizenCost = crate.CrateQty * cost;

				string dug = CitizensStringConstants.ACTION_SMELTED;
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	dug = CitizensStringConstants.ACTION_MINED; break;
					case 1:	dug = CitizensStringConstants.ACTION_SMELTED; break;
					case 2:	dug = CitizensStringConstants.ACTION_FORGED; break;
					case 3:	dug = CitizensStringConstants.ACTION_DUG_UP; break;
					case 4:	dug = CitizensStringConstants.ACTION_EXCAVATED; break;
					case 5:	dug = CitizensStringConstants.ACTION_FORMED; break;
				}

				string sell = CitizensStringConstants.WILLING_PART_WITH; 
				if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_TRADE; } 
				else if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_SELL; }
				string cave = CitizensStringConstants.LOCATION_CAVE; 
				if (Utility.RandomBool() ){ cave = CitizensStringConstants.LOCATION_MINE; }

				string location = dungeon;
				string locationPreposition = CitizensStringConstants.LOCATION_NEAR;
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_NEAR; break;
					case 1: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_OUTSIDE; break;
					case 2: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_BY; break;
					case 3: location = city; locationPreposition = CitizensStringConstants.LOCATION_NEAR; break;
					case 4: location = city; locationPreposition = CitizensStringConstants.LOCATION_BY; break;
					case 5: location = city; locationPreposition = CitizensStringConstants.LOCATION_OUTSIDE; break;
				}
				CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.MATERIAL_VENDOR_PHRASE_FORMAT, crate.CrateQty, metal, "ingots", dug, cave, locationPreposition, location, sell );
				CitizenPhrase = CitizenPhrase + " " + string.Format( CitizensStringConstants.MATERIAL_VENDOR_CLOSING_FORMAT, "ingots" );

				PackItem( crate );
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_WOOD_VENDOR && CitizenService == CitizensConstants.CITIZEN_TYPE_WOOD_VENDOR )
			{
				bool isLogs = Utility.RandomBool();

				string contents = "boards";
					if ( isLogs ){ contents = "logs"; }

				dungeon = RandomThings.MadeUpDungeon();
				city = RandomThings.MadeUpCity();

				int boards = Utility.RandomMinMax( 1, 65536 );

				string wood;
				int tree;
				int hue;
				int qty;
				int cost;

				if ( boards >= 32768 ){ wood = "regular"; tree = 0x5088; hue = 0; qty = 80; cost = 2; }
				else if ( boards >= 16384 ){ wood = "ash"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "ash", "", 0 ); qty = 75; cost = 4; }
				else if ( boards >= 8192 ){ wood = "cherry"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "cherry", "", 0 ); qty = 70; cost = 6; }
				else if ( boards >= 4096 ){ wood = "ebony"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "ebony", "", 0 ); qty = 65; cost = 8; }
				else if ( boards >= 2048 ){ wood = "golden oak"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "golden oak", "", 0 ); qty = 60; cost = 10; }
				else if ( boards >= 1024 ){ wood = "hickory"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "hickory", "", 0 ); qty = 55; cost = 12; }
				else if ( boards >= 512 ){ wood = "mahogany"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 ); qty = 50; cost = 14; }
				else if ( boards >= 256 ){ wood = "oak"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "oak", "", 0 ); qty = 45; cost = 16; }
				else if ( boards >= 128 ){ wood = "pine"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "pine", "", 0 ); qty = 40; cost = 18; }
				else if ( boards >= 64 ){ wood = "ghostwood"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "ghostwood", "", 0 ); qty = 35; cost = 20; }
				else if ( boards >= 32 ){ wood = "rosewood"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 ); qty = 30; cost = 22; }
				else if ( boards >= 16 ){ wood = "walnut"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "walnut", "", 0 ); qty = 25; cost = 24; }
				else if ( boards >= 8 ){ wood = "petrified"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "petrified", "", 0 ); qty = 20; cost = 26; }
				else if ( boards >= 4 ){ wood = "driftwood"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "driftwood", "", 0 ); qty = 15; cost = 28; }
				else { wood = "elven"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "elven", "", 0 ); qty = 10; cost = 30; }

				if ( isLogs )
				{
					if ( tree == 0x5088 ){ tree = 0x5097; }
					else { tree = 0x5096; }
				}

				Item box = null;
				int amount = 0;
				if ( isLogs )
				{
					box = new CrateOfLogs();
					CrateOfLogs crate = (CrateOfLogs)box;
					crate.CrateQty = Utility.RandomMinMax( qty*5, qty*15 );
					amount = crate.CrateQty;
					crate.CrateItem = wood;
					crate.Hue = hue;
					crate.ItemID = tree;
					crate.Name = "crate of " + wood + " " + contents + "";
					crate.Weight = crate.CrateQty * 0.1;
					CitizenCost = crate.CrateQty * cost;
				}
				else
				{
					box = new CrateOfWood();
					CrateOfWood crate = (CrateOfWood)box;
					crate.CrateQty = Utility.RandomMinMax( qty*5, qty*15 );
					amount = crate.CrateQty;
					crate.CrateItem = wood;
					crate.Hue = hue;
					crate.ItemID = tree;
					crate.Name = "crate of " + wood + " " + contents + "";
					crate.Weight = crate.CrateQty * 0.1;
					CitizenCost = crate.CrateQty * cost;
				}

				string chop = CitizensStringConstants.ACTION_CHOPPED;
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	chop = CitizensStringConstants.ACTION_CHOPPED; break;
					case 1:	chop = CitizensStringConstants.ACTION_CUT; break;
					case 2:	chop = CitizensStringConstants.ACTION_LOGGED; break;
				}

				string sell = CitizensStringConstants.WILLING_PART_WITH; 
				if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_TRADE; } 
				else if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_SELL; }
				string forest = CitizensStringConstants.LOCATION_WOODS; 
				if (Utility.RandomBool() ){ forest = CitizensStringConstants.LOCATION_FOREST; }

				string location = dungeon;
				string locationPreposition = CitizensStringConstants.LOCATION_NEAR;
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_NEAR; break;
					case 1: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_OUTSIDE; break;
					case 2: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_BY; break;
					case 3: location = city; locationPreposition = CitizensStringConstants.LOCATION_NEAR; break;
					case 4: location = city; locationPreposition = CitizensStringConstants.LOCATION_BY; break;
					case 5: location = city; locationPreposition = CitizensStringConstants.LOCATION_OUTSIDE; break;
				}
				CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.MATERIAL_VENDOR_PHRASE_FORMAT, amount, wood, contents, chop, forest, locationPreposition, location, sell );
				CitizenPhrase = CitizenPhrase + " " + string.Format( CitizensStringConstants.MATERIAL_VENDOR_CLOSING_FORMAT, contents );

				PackItem( box );
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_LEATHER_VENDOR && CitizenService == CitizensConstants.CITIZEN_TYPE_LEATHER_VENDOR )
			{
				dungeon = RandomThings.MadeUpDungeon();
				city = RandomThings.MadeUpCity();

				CrateOfLeather crate = new CrateOfLeather();

				int skin = Utility.RandomMinMax( 1, 65536 );

				string flesh;
				int hide;
				int hue;
				int qty;
				int cost;

				if ( skin >= 32768 ){ flesh = "regular"; hide = 0x5092; hue = 0; qty = 80; cost = 2; }
				else if ( skin >= 16384 ){ flesh = "deep sea"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "deep sea", "", 0 ); qty = 75; cost = 4; }
				else if ( skin >= 8192 ){ flesh = "lizard"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "lizard", "", 0 ); qty = 70; cost = 6; }
				else if ( skin >= 4096 ){ flesh = "serpent"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "serpent", "", 0 ); qty = 65; cost = 8; }
				// TODO: Future implementation - Special hide types disabled
				//else if ( skin >= 2048 ){ flesh = "necrotic"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "necrotic", "", 0 ); qty = 60; cost = 10; }
				//else if ( skin >= 1024 ){ flesh = "volcanic"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "volcanic", "", 0 ); qty = 55; cost = 12; }
				//else if ( skin >= 512 ){ flesh = "frozen"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "frozen", "", 0 ); qty = 50; cost = 14; }
				else if ( skin >= 256 ){ flesh = "goliath"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "goliath", "", 0 ); qty = 45; cost = 16; }
				//else if ( skin >= 128 ){ flesh = "draconic"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "draconic", "", 0 ); qty = 40; cost = 18; }
				//else if ( skin >= 64 ){ flesh = "hellish"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "hellish", "", 0 ); qty = 35; cost = 20; }
				//else if ( skin >= 32 ){ flesh = "dinosaur"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "dinosaur", "", 0 ); qty = 30; cost = 22; }
				//else { flesh = "alien"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "alien", "", 0 ); qty = 10; cost = 30; }
				else { flesh = "serpent"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "serpent", "", 0 ); qty = 65; cost = 8; } // Fallback to serpent for disabled types

				crate.CrateQty = Utility.RandomMinMax( qty*5, qty*15 );
				crate.CrateItem = flesh;
				crate.Hue = hue;
				crate.ItemID = hide;
				crate.Name = "crate of " + flesh + " leather";
				crate.Weight = crate.CrateQty * 0.1;
				CitizenCost = crate.CrateQty * cost;

				string carve = CitizensStringConstants.ACTION_SKINNED;
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	carve = CitizensStringConstants.ACTION_SKINNED; break;
					case 1:	carve = CitizensStringConstants.ACTION_TANNED; break;
					case 2:	carve = CitizensStringConstants.ACTION_GATHERED; break;
				}

				string sell = CitizensStringConstants.WILLING_PART_WITH; 
				if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_TRADE; } 
				else if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_SELL; }

				string location = dungeon;
				string locationPreposition = CitizensStringConstants.LOCATION_NEAR;
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_NEAR; break;
					case 1: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_OUTSIDE; break;
					case 2: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_BY; break;
					case 3: location = city; locationPreposition = CitizensStringConstants.LOCATION_NEAR; break;
					case 4: location = city; locationPreposition = CitizensStringConstants.LOCATION_BY; break;
					case 5: location = city; locationPreposition = CitizensStringConstants.LOCATION_OUTSIDE; break;
				}
				CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.MATERIAL_VENDOR_PHRASE_FORMAT, crate.CrateQty, flesh, "leather", carve, "", locationPreposition, location, sell );
				CitizenPhrase = CitizenPhrase + " " + string.Format( CitizensStringConstants.MATERIAL_VENDOR_CLOSING_FORMAT, "leather" );

				PackItem( crate );
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_ORE_VENDOR && CitizenService == CitizensConstants.CITIZEN_TYPE_ORE_VENDOR )
			{
				dungeon = RandomThings.MadeUpDungeon();
				city = RandomThings.MadeUpCity();

				CrateOfOre crate = new CrateOfOre();

				int ingot = Utility.RandomMinMax( 1, 65536 );

				string metal;
				int steel;
				int hue;
				int qty;
				int cost;

				if ( ingot >= 32768 ){ metal = "iron"; steel = 0x5084; hue = 0; qty = 80; cost = 2; }
				else if ( ingot >= 16384 ){ metal = "dull copper"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "dull copper", "", 0 ); qty = 75; cost = 4; }
				else if ( ingot >= 8192 ){ metal = "shadow iron"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ); qty = 70; cost = 6; }
				else if ( ingot >= 4096 ){ metal = "copper"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "copper", "", 0 ); qty = 65; cost = 8; }
				else if ( ingot >= 2048 ){ metal = "bronze"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "bronze", "", 0 ); qty = 60; cost = 10; }
				else if ( ingot >= 1024 ){ metal = "gold"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "gold", "", 0 ); qty = 55; cost = 12; }
				else if ( ingot >= 512 ){ metal = "agapite"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "agapite", "", 0 ); qty = 50; cost = 14; }
				else if ( ingot >= 256 ){ metal = "verite"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "verite", "", 0 ); qty = 45; cost = 16; }
				else if ( ingot >= 128 ){ metal = "valorite"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "valorite", "", 0 ); qty = 40; cost = 18; }
				else if ( ingot >= 64 ){ metal = "nepturite"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "nepturite", "", 0 ); qty = 35; cost = 20; }
				else if ( ingot >= 32 ){ metal = "obsidian"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "obsidian", "", 0 ); qty = 30; cost = 22; }
				else if ( ingot >= 16 ){ metal = "steel"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "steel", "", 0 ); qty = 25; cost = 24; }
				else if ( ingot >= 8 ){ metal = "brass"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "brass", "", 0 ); qty = 20; cost = 26; }
				else if ( ingot >= 4 ){ metal = "mithril"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "mithril", "", 0 ); qty = 15; cost = 28; }
				else if ( ingot >= 2 ){ metal = "xormite"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "xormite", "", 0 ); qty = 10; cost = 30; }
				else { metal = "dwarven"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "dwarven", "", 0 ); qty = 5; cost = 32; }

				crate.CrateQty = Utility.RandomMinMax( qty*5, qty*15 );
				crate.CrateItem = metal;
				crate.Hue = hue;
				crate.ItemID = steel;
				crate.Name = "crate of " + metal + " ore";
				crate.Weight = crate.CrateQty * 0.1;
				CitizenCost = crate.CrateQty * cost;

				string dug = CitizensStringConstants.ACTION_MINED;
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	dug = CitizensStringConstants.ACTION_MINED; break;
					case 1:	dug = CitizensStringConstants.ACTION_DUG_UP; break;
					case 2:	dug = CitizensStringConstants.ACTION_EXCAVATED; break;
				}

				string sell = CitizensStringConstants.WILLING_PART_WITH; 
				if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_TRADE; } 
				else if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_SELL; }
				string cave = CitizensStringConstants.LOCATION_CAVE; 
				if (Utility.RandomBool() ){ cave = CitizensStringConstants.LOCATION_MINE; }

				string location = dungeon;
				string locationPreposition = CitizensStringConstants.LOCATION_NEAR;
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_NEAR; break;
					case 1: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_OUTSIDE; break;
					case 2: location = dungeon; locationPreposition = CitizensStringConstants.LOCATION_BY; break;
					case 3: location = city; locationPreposition = CitizensStringConstants.LOCATION_NEAR; break;
					case 4: location = city; locationPreposition = CitizensStringConstants.LOCATION_BY; break;
					case 5: location = city; locationPreposition = CitizensStringConstants.LOCATION_OUTSIDE; break;
				}
				CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.MATERIAL_VENDOR_PHRASE_FORMAT, crate.CrateQty, metal, "ore", dug, cave, locationPreposition, location, sell );
				CitizenPhrase = CitizenPhrase + " " + string.Format( CitizensStringConstants.MATERIAL_VENDOR_CLOSING_FORMAT, "ore" );

				PackItem( crate );
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_REAGENT_VENDOR && CitizenService == CitizensConstants.CITIZEN_TYPE_REAGENT_VENDOR )
			{
				dungeon = RandomThings.MadeUpDungeon();
				city = RandomThings.MadeUpCity();

				CrateOfReagents crate = new CrateOfReagents();

				string reagent = "bloodmoss";
				int bottle = 0x508E;

				switch ( Utility.RandomMinMax( 0, 24 ) )
				{
					case 0:	bottle = 0x508E; reagent = "bloodmoss"; break;
					case 1:	bottle = 0x508F; reagent = "black pearl"; break;
					case 2:	bottle = 0x5098; reagent = "garlic"; break;
					case 3:	bottle = 0x5099; reagent = "ginseng"; break;
					case 4:	bottle = 0x509A; reagent = "mandrake root"; break;
					case 5:	bottle = 0x509B; reagent = "nightshade"; break;
					case 6:	bottle = 0x509C; reagent = "sulfurous ash"; break;
					case 7:	bottle = 0x509D; reagent = "spider silk"; break;
					case 8:		bottle = 0x568A; reagent = "swamp berry"; break;
					case 9:		bottle = 0x55E0; reagent = "bat wing"; break;
					case 10:	bottle = 0x55E1; reagent = "beetle shell"; break;
					case 11:	bottle = 0x55E2; reagent = "brimstone"; break;
					case 12:	bottle = 0x55E3; reagent = "butterfly"; break;
					case 13:	bottle = 0x55E4; reagent = "daemon blood"; break;
					case 14:	bottle = 0x55E5; reagent = "toad eyes"; break;
					case 15:	bottle = 0x55E6; reagent = "fairy eggs"; break;
					case 16:	bottle = 0x55E7; reagent = "gargoyle ears"; break;
					case 17:	bottle = 0x55E8; reagent = "grave dust"; break;
					case 18:	bottle = 0x55E9; reagent = "moon crystals"; break;
					case 19:	bottle = 0x55EA; reagent = "nox crystal"; break;
					case 20:	bottle = 0x55EB; reagent = "silver widow"; break;
					case 21:	bottle = 0x55EC; reagent = "pig iron"; break;
					case 22:	bottle = 0x55ED; reagent = "pixie skull"; break;
					case 23:	bottle = 0x55EE; reagent = "red lotus"; break;
					case 24:	bottle = 0x55EF; reagent = "sea salt"; break;
				}

				crate.CrateQty = Utility.RandomMinMax( 400, 1200 );
				crate.CrateItem = reagent;
				crate.ItemID = bottle;
				crate.Name = "crate of " + reagent + "";
				crate.Weight = crate.CrateQty * 0.1;
				CitizenCost = crate.CrateQty * 5;

				string bought = CitizensStringConstants.ACTION_BOUGHT;
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	bought = CitizensStringConstants.ACTION_ACQUIRED; break;
					case 1:	bought = CitizensStringConstants.ACTION_PURCHASED; break;
					case 2:	bought = CitizensStringConstants.ACTION_BOUGHT; break;
				}
				string found = CitizensStringConstants.ACTION_FOUND_REAGENT;
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	found = CitizensStringConstants.ACTION_FOUND_REAGENT; break;
					case 1:	found = CitizensStringConstants.ACTION_DISCOVERED_REAGENT; break;
					case 2:	found = CitizensStringConstants.ACTION_CAME_UPON; break;
				}

				string sell = CitizensStringConstants.WILLING_PART_WITH; 
				if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_TRADE; } 
				else if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_SELL; }

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.REAGENT_VENDOR_FOUND_FORMAT, crate.CrateQty, reagent, found, dungeon, sell ); break;
					case 1:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.REAGENT_VENDOR_FOUND_DEEP_FORMAT, crate.CrateQty, reagent, found, dungeon, sell ); break;
					case 2:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.REAGENT_VENDOR_FOUND_SOMEWHERE_FORMAT, crate.CrateQty, reagent, found, dungeon, sell ); break;
					case 3:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.REAGENT_VENDOR_BOUGHT_CITY_FORMAT, crate.CrateQty, reagent, bought, city, sell ); break;
					case 4:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.REAGENT_VENDOR_BOUGHT_NEAR_FORMAT, crate.CrateQty, reagent, bought, city, sell ); break;
					case 5:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.REAGENT_VENDOR_BOUGHT_SOMEWHERE_FORMAT, crate.CrateQty, reagent, bought, city, sell ); break;
				}
				CitizenPhrase = CitizenPhrase + " " + CitizensStringConstants.REAGENT_VENDOR_CLOSING;

				PackItem( crate );
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_POTION_VENDOR && CitizenService == CitizensConstants.CITIZEN_TYPE_POTION_VENDOR )
			{
				dungeon = RandomThings.MadeUpDungeon();
				city = RandomThings.MadeUpCity();

				CrateOfPotions crate = new CrateOfPotions();

				string potion = "crate of nightsight potions";
				int jug = 1109;
				int coins = 15;

				switch ( Utility.RandomMinMax( 0, 36 ) )
				{
					case 0: coins = 15; potion = "nightsight potions"; jug = 1109; break;
					case 1: coins = 15; potion = "lesser cure potions"; jug = 45; break;
					case 2: coins = 30; potion = "cure potions"; jug = 45; break;
					case 3: coins = 60; potion = "greater cure potions"; jug = 45; break;
					case 4: coins = 15; potion = "agility potions"; jug = 396; break;
					case 5: coins = 60; potion = "greater agility potions"; jug = 396; break;
					case 6: coins = 15; potion = "strength potions"; jug = 1001; break;
					case 7: coins = 60; potion = "greater strength potions"; jug = 1001; break;
					case 8: coins = 15; potion = "lesser poison potions"; jug = 73; break;
					case 9: coins = 30; potion = "poison potions"; jug = 73; break;
					case 10: coins = 60; potion = "greater poison potions"; jug = 73; break;
					case 11: coins = 90; potion = "deadly poison potions"; jug = 73; break;
					case 12: coins = 120; potion = "lethal poison potions"; jug = 73; break;
					case 13: coins = 15; potion = "refresh potions"; jug = 140; break;
					case 14: coins = 30; potion = "total refresh potions"; jug = 140; break;
					case 15: coins = 15; potion = "lesser heal potions"; jug = 50; break;
					case 16: coins = 30; potion = "heal potions"; jug = 50; break;
					case 17: coins = 60; potion = "greater heal potions"; jug = 50; break;
					case 18: coins = 15; potion = "lesser explosion potions"; jug = 425; break;
					case 19: coins = 30; potion = "explosion potions"; jug = 425; break;
					case 20: coins = 60; potion = "greater explosion potions"; jug = 425; break;
					case 21: coins = 15; potion = "lesser invisibility potions"; jug = 0x490; break;
					case 22: coins = 30; potion = "invisibility potions"; jug = 0x490; break;
					case 23: coins = 60; potion = "greater invisibility potions"; jug = 0x490; break;
					case 24: coins = 15; potion = "lesser rejuvenate potions"; jug = 0x48E; break;
					case 25: coins = 30; potion = "rejuvenate potions"; jug = 0x48E; break;
					case 26: coins = 60; potion = "greater rejuvenate potions"; jug = 0x48E; break;
					case 27: coins = 15; potion = "lesser mana potions"; jug = 0x48D; break;
					case 28: coins = 30; potion = "mana potions"; jug = 0x48D; break;
					case 29: coins = 60; potion = "greater mana potions"; jug = 0x48D; break;
					case 30: coins = 30; potion = "conflagration potions"; jug = 0xAD8; break;
					case 31: coins = 60; potion = "greater conflagration potions"; jug = 0xAD8; break;
					case 32: coins = 30; potion = "confusion blast potions"; jug = 0x495; break;
					case 33: coins = 60; potion = "greater confusion blast potions"; jug = 0x495; break;
					case 34: coins = 30; potion = "frostbite potions"; jug = 0xAF3; break;
					case 35: coins = 60; potion = "greater frostbite potions"; jug = 0xAF3; break;
					case 36: coins = 60; potion = "acid bottles"; jug = 1167; break;
				}

				crate.CrateQty = Utility.RandomMinMax( 30, 100 );
				crate.CrateItem = potion;
				crate.ItemID = 0x55DF;
				crate.Hue = jug;
				crate.Name = "crate of " + potion + "";
				crate.Weight = crate.CrateQty * 0.1;
				CitizenCost = crate.CrateQty * coins;

				string bought = CitizensStringConstants.ACTION_BOUGHT;
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	bought = CitizensStringConstants.ACTION_ACQUIRED; break;
					case 1:	bought = CitizensStringConstants.ACTION_PURCHASED; break;
					case 2:	bought = CitizensStringConstants.ACTION_BOUGHT; break;
					case 3:	bought = CitizensStringConstants.ACTION_BREWED; break;
					case 4:	bought = CitizensStringConstants.ACTION_CONCOCTED; break;
					case 5:	bought = CitizensStringConstants.ACTION_PREPARED; break;
				}
				string found = CitizensStringConstants.ACTION_FOUND;
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	found = CitizensStringConstants.ACTION_FOUND; break;
					case 1:	found = CitizensStringConstants.ACTION_DISCOVERED; break;
					case 2:	found = CitizensStringConstants.ACTION_CAME_UPON; break;
				}

				string sell = CitizensStringConstants.WILLING_PART_WITH; 
				if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_TRADE; } 
				else if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_SELL; }

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.POTION_VENDOR_FOUND_FORMAT, crate.CrateQty, potion, found, dungeon, sell ); break;
					case 1:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.POTION_VENDOR_FOUND_DEEP_FORMAT, crate.CrateQty, potion, found, dungeon, sell ); break;
					case 2:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.POTION_VENDOR_FOUND_SOMEWHERE_FORMAT, crate.CrateQty, potion, found, dungeon, sell ); break;
					case 3:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.POTION_VENDOR_BOUGHT_CITY_FORMAT, crate.CrateQty, potion, bought, city, sell ); break;
					case 4:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.POTION_VENDOR_BOUGHT_NEAR_FORMAT, crate.CrateQty, potion, bought, city, sell ); break;
					case 5:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.POTION_VENDOR_BOUGHT_SOMEWHERE_FORMAT, crate.CrateQty, potion, bought, city, sell ); break;
				}
				CitizenPhrase = CitizenPhrase + " " + CitizensStringConstants.POTION_VENDOR_CLOSING;

				PackItem( crate );
			}

			if ( CitizenType == CitizensConstants.CITIZEN_TYPE_WIZARD && CitizenService == CitizensConstants.CITIZEN_SERVICE_REAGENT_JAR_1 ){ PackItem( new reagents_magic_jar1() ); CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_REAGENT_JAR_1_MIN, CitizensConstants.COST_REAGENT_JAR_1_MAX )*10; }
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_WIZARD && CitizenService == CitizensConstants.CITIZEN_SERVICE_REAGENT_JAR_2 ){ PackItem( new reagents_magic_jar2() ); CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_REAGENT_JAR_2_MIN, CitizensConstants.COST_REAGENT_JAR_2_MAX )*10; }
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_WIZARD && CitizenService == CitizensConstants.CITIZEN_SERVICE_REAGENT_JAR_3 ){ PackItem( new reagents_magic_jar3() ); CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_REAGENT_JAR_3_MIN, CitizensConstants.COST_REAGENT_JAR_3_MAX )*10; }
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_WIZARD && CitizenService == CitizensConstants.CITIZEN_SERVICE_BOOK )
			{
				if ( Utility.RandomBool() )
				{
					if ( Utility.RandomBool() )
					{
						Spellbook tome = new MySpellbook();
						CitizenCost = Utility.RandomMinMax( ((tome.SpellCount+1)*CitizensConstants.COST_SPELLBOOK_MULT_MIN), ((tome.SpellCount+1)*CitizensConstants.COST_SPELLBOOK_MULT_MAX) );
						PackItem( tome ); 
					}
					else
					{
						Spellbook tome = new MyNecromancerSpellbook();
						CitizenCost = Utility.RandomMinMax( ((tome.SpellCount+1)*CitizensConstants.COST_NECRO_SPELLBOOK_MULT_MIN), ((tome.SpellCount+1)*CitizensConstants.COST_NECRO_SPELLBOOK_MULT_MAX) );
						PackItem( tome ); 
					}
				}
				else
				{
					PackItem( new Runebook() ); CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_RUNEBOOK_MIN, CitizensConstants.COST_RUNEBOOK_MAX )*10;
				}
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_WIZARD && CitizenService == CitizensConstants.CITIZEN_SERVICE_SCROLL )
			{
				Item scroll = Server.Items.DungeonLoot.RandomHighLevelScroll();

				int mult = 1;

				if ( scroll is PainSpikeScroll || scroll is EvilOmenScroll || scroll is WraithFormScroll || scroll is ArchCureScroll || scroll is ArchProtectionScroll || scroll is CurseScroll || scroll is FireFieldScroll || scroll is GreaterHealScroll || scroll is LightningScroll || scroll is ManaDrainScroll || scroll is RecallScroll ){ mult = CitizensConstants.SCROLL_MULT_TIER_1; }

				else if ( scroll is MindRotScroll || scroll is SummonFamiliarScroll || scroll is HorrificBeastScroll || scroll is AnimateDeadScroll || scroll is BladeSpiritsScroll || scroll is DispelFieldScroll || scroll is IncognitoScroll || scroll is MagicReflectScroll || scroll is MindBlastScroll || scroll is ParalyzeScroll || scroll is PoisonFieldScroll || scroll is SummonCreatureScroll ){ mult = CitizensConstants.SCROLL_MULT_TIER_2; }

				else if ( scroll is DispelScroll || scroll is EnergyBoltScroll || scroll is ExplosionScroll || scroll is InvisibilityScroll || scroll is MarkScroll || scroll is MassCurseScroll || scroll is ParalyzeFieldScroll || scroll is RevealScroll ){ mult = CitizensConstants.SCROLL_MULT_TIER_3; }

				else if ( scroll is PoisonStrikeScroll || scroll is WitherScroll || scroll is StrangleScroll || scroll is LichFormScroll || scroll is ChainLightningScroll || scroll is EnergyFieldScroll || scroll is FlamestrikeScroll || scroll is GateTravelScroll || scroll is ManaVampireScroll || scroll is MassDispelScroll || scroll is MeteorSwarmScroll || scroll is PolymorphScroll ){ mult = CitizensConstants.SCROLL_MULT_TIER_4; }

				else if ( scroll is ExorcismScroll || scroll is VampiricEmbraceScroll || scroll is VengefulSpiritScroll || scroll is EarthquakeScroll || scroll is EnergyVortexScroll || scroll is ResurrectionScroll || scroll is SummonAirElementalScroll || scroll is SummonDaemonScroll || scroll is SummonEarthElementalScroll || scroll is SummonFireElementalScroll || scroll is  SummonWaterElementalScroll ){ mult = CitizensConstants.SCROLL_MULT_TIER_5; }

				PackItem( scroll );
				CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_SCROLL_BASE_MIN, CitizensConstants.COST_SCROLL_BASE_MAX )*mult;
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_WIZARD && CitizenService == CitizensConstants.CITIZEN_SERVICE_WAND )
			{
				Item wand = Loot.RandomWand();
				Server.Misc.MaterialInfo.ColorMetal( wand, 0 );
				string wandOwner = Server.LootPackEntry.MagicWandOwner() + " ";
				wand.Name = wandOwner + wand.Name;
				BaseWeapon bw = (BaseWeapon)wand;
				if ( bw.IntRequirement == 10 ) { CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_WAND_BASE_MIN, CitizensConstants.COST_WAND_BASE_MAX )*CitizensConstants.COST_WAND_MULT_10; }
				else if ( bw.IntRequirement == 15 ) { CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_WAND_BASE_MIN, CitizensConstants.COST_WAND_BASE_MAX )*CitizensConstants.COST_WAND_MULT_15; }
				else if ( bw.IntRequirement == 20 ) { CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_WAND_BASE_MIN, CitizensConstants.COST_WAND_BASE_MAX )*CitizensConstants.COST_WAND_MULT_20; }
				else if ( bw.IntRequirement == 25 ) { CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_WAND_BASE_MIN, CitizensConstants.COST_WAND_BASE_MAX )*CitizensConstants.COST_WAND_MULT_25; }
				else if ( bw.IntRequirement == 30 ) { CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_WAND_BASE_MIN, CitizensConstants.COST_WAND_BASE_MAX )*CitizensConstants.COST_WAND_MULT_30; }
				else if ( bw.IntRequirement == 35 ) { CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_WAND_BASE_MIN, CitizensConstants.COST_WAND_BASE_MAX )*CitizensConstants.COST_WAND_MULT_35; }
				else if ( bw.IntRequirement == 40 ) { CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_WAND_BASE_MIN, CitizensConstants.COST_WAND_BASE_MAX )*CitizensConstants.COST_WAND_MULT_40; }
				else if ( bw.IntRequirement == 45 ) { CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_WAND_BASE_MIN, CitizensConstants.COST_WAND_BASE_MAX )*CitizensConstants.COST_WAND_MULT_45; }
				PackItem( wand );
			}
			else if ( CitizenService == CitizensConstants.CITIZEN_SERVICE_MAGIC_ITEM )
			{
				int val = Utility.RandomMinMax( CitizensConstants.ITEM_VALUE_MIN, CitizensConstants.ITEM_VALUE_MAX );
				int props = CitizensConstants.ITEM_PROPERTIES_MIN + Utility.RandomMinMax( 0, CitizensConstants.ITEM_PROPERTIES_MAX - CitizensConstants.ITEM_PROPERTIES_MIN );
				int luck = Utility.RandomMinMax( CitizensConstants.ITEM_LUCK_MIN, CitizensConstants.ITEM_LUCK_MAX );
				int chance = Utility.RandomMinMax( CitizensConstants.ITEM_CHANCE_MIN, CitizensConstants.ITEM_CHANCE_MAX );

				if ( chance < CitizensConstants.ITEM_CHANCE_THRESHOLD_REGULAR )
				{
					Item arty = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing();
					if ( arty is BaseWeapon ){ BaseRunicTool.ApplyAttributesTo( (BaseWeapon)arty, false, luck, props, val, val ); }
					else if ( arty is BaseArmor ){ BaseRunicTool.ApplyAttributesTo( (BaseArmor)arty, false, luck, props, val, val ); }
					else if ( arty is BaseJewel ){ BaseRunicTool.ApplyAttributesTo( (BaseJewel)arty, false, luck, props, val, val ); }
					Server.Misc.MorphingTime.ChangeMaterialType( arty, this );
					arty.Movable = false;
					arty.Name = LootPackEntry.MagicItemName( arty, this, Region.Find( this.Location, this.Map ) );
					arty.Name = cultInfo.ToTitleCase(arty.Name);
					PackItem( arty );
					CitizenCost = (val+props+luck)*CitizensConstants.ITEM_COST_MULTIPLIER;
				}
				else if ( chance < CitizensConstants.ITEM_CHANCE_THRESHOLD_CLOTHING )
				{
					Item arty = Loot.RandomClothing();
					Server.Misc.MorphingTime.ChangeMaterialType( arty, this );
					BaseRunicTool.ApplyAttributesTo( (BaseClothing)arty, false, luck, props, val, val );
					arty.Movable = false;
					arty.Name = LootPackEntry.MagicItemName( arty, this, Region.Find( this.Location, this.Map ) );
					arty.Name = cultInfo.ToTitleCase(arty.Name);
					PackItem( arty );
					CitizenCost = (val+props+luck)*CitizensConstants.ITEM_COST_MULTIPLIER;
				}
				else if ( chance < CitizensConstants.ITEM_CHANCE_THRESHOLD_INSTRUMENT )
				{
					Item arty = Loot.RandomInstrument();
					Server.Misc.MorphingTime.ChangeMaterialType( arty, this );
					SlayerName slayer = BaseRunicTool.GetRandomSlayer();
					BaseInstrument instr = (BaseInstrument)arty;

					int cHue = 0;
					int cUse = 0;

					switch ( instr.Resource )
					{
						case CraftResource.AshTree: cHue = MaterialInfo.GetMaterialColor( "ash", "", 0 ); cUse = CitizensConstants.INSTRUMENT_USES_ASH; break;
						case CraftResource.EbonyTree: cHue = MaterialInfo.GetMaterialColor( "ebony", "", 0 ); cUse = CitizensConstants.INSTRUMENT_USES_EBONY; break;
                        case CraftResource.ElvenTree: cHue = MaterialInfo.GetMaterialColor("elven", "", 0); cUse = CitizensConstants.INSTRUMENT_USES_ELVEN; break;
                        case CraftResource.CherryTree: cHue = MaterialInfo.GetMaterialColor("cherry", "", 0); cUse = CitizensConstants.INSTRUMENT_USES_CHERRY; break;
                        case CraftResource.RosewoodTree: cHue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 ); cUse = CitizensConstants.INSTRUMENT_USES_ROSEWOOD; break;
                        case CraftResource.GoldenOakTree: cHue = MaterialInfo.GetMaterialColor("golden oak", "", 0); cUse = CitizensConstants.INSTRUMENT_USES_GOLDEN_OAK; break;
                        case CraftResource.HickoryTree: cHue = MaterialInfo.GetMaterialColor("hickory", "", 0); cUse = CitizensConstants.INSTRUMENT_USES_HICKORY; break;
                    }

					instr.UsesRemaining = instr.UsesRemaining + cUse;
					if ( cHue > 0 ){ arty.Hue = cHue; }
					else if ( Utility.RandomMinMax( CitizensConstants.INSTRUMENT_HUE_THRESHOLD, CitizensConstants.INSTRUMENT_HUE_CHANCE ) == CitizensConstants.INSTRUMENT_HUE_THRESHOLD ){ arty.Hue = Server.Misc.RandomThings.GetRandomColor(0); }
					instr.Quality = InstrumentQuality.Regular;
					if ( Utility.RandomMinMax( CitizensConstants.INSTRUMENT_QUALITY_THRESHOLD, CitizensConstants.INSTRUMENT_QUALITY_CHANCE ) == CitizensConstants.INSTRUMENT_QUALITY_THRESHOLD ){ instr.Quality = InstrumentQuality.Exceptional; }
					if ( Utility.RandomMinMax( CitizensConstants.INSTRUMENT_SLAYER_THRESHOLD, CitizensConstants.INSTRUMENT_SLAYER_CHANCE ) == CitizensConstants.INSTRUMENT_SLAYER_THRESHOLD ){ instr.Slayer = slayer; }

					BaseRunicTool.ApplyAttributesTo( (BaseInstrument)arty, false, luck, props, val, val );
					arty.Movable = false;
					arty.Name = LootPackEntry.MagicItemName( arty, this, Region.Find( this.Location, this.Map ) );
					arty.Name = cultInfo.ToTitleCase(arty.Name);
					PackItem( arty );
					CitizenCost = (val+props+luck)*CitizensConstants.ITEM_COST_MULTIPLIER;
				}
				else
				{
					Item arty = Loot.RandomArty();
					arty.Movable = false;
					PackItem( arty );
					CitizenCost = Utility.RandomMinMax( CitizensConstants.COST_ARTY_MIN, CitizensConstants.COST_ARTY_MAX )*10;
				}
			}
			else if ( CitizenType == CitizensConstants.CITIZEN_TYPE_FOOD_VENDOR && CitizenService == CitizensConstants.CITIZEN_TYPE_FOOD_VENDOR )
			{
				city = RandomThings.MadeUpCity();

				CrateOfFood crate = new CrateOfFood();

				string food = "meat";
				int eat = CitizensConstants.ITEM_ID_FOOD_LAMB_LEG;
				int cost = 0;

				switch ( Utility.RandomMinMax( 0, CitizensConstants.FOOD_MAX_INDEX ) )
				{
					case 0:	cost = CitizensConstants.COST_FOOD_FISH_STEAK;	eat = CitizensConstants.ITEM_ID_FOOD_FISH_STEAK; food = "cooked fish steaks"; break;
					case 1:	cost = CitizensConstants.COST_FOOD_LAMB_LEG;	eat = CitizensConstants.ITEM_ID_FOOD_LAMB_LEG; food = "cooked lamb legs"; break;
					case 2:	cost = CitizensConstants.COST_FOOD_RIBS;	eat = CitizensConstants.ITEM_ID_FOOD_RIBS; food = "cooked ribs"; break;
					case 3:	cost = CitizensConstants.COST_FOOD_BREAD;	eat = CitizensConstants.ITEM_ID_FOOD_BREAD; food = "baked bread"; break;
				}

				crate.CrateQty = Utility.RandomMinMax( CitizensConstants.FOOD_QTY_MIN, CitizensConstants.FOOD_QTY_MAX );
				crate.CrateItem = food;
				crate.ItemID = eat;
				crate.Name = "crate of " + food + "";
				crate.Weight = crate.CrateQty * CitizensConstants.CRATE_WEIGHT_MULTIPLIER;
				CitizenCost = crate.CrateQty * cost;

				string bought = CitizensStringConstants.ACTION_BOUGHT;
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	bought = CitizensStringConstants.ACTION_ACQUIRED; break;
					case 1:	bought = CitizensStringConstants.ACTION_PURCHASED; break;
					case 2:	bought = CitizensStringConstants.ACTION_BOUGHT; break;
					case 3:	bought = CitizensStringConstants.ACTION_COOKED; break;
					case 4:	bought = CitizensStringConstants.ACTION_BAKED; break;
					case 5:	bought = CitizensStringConstants.ACTION_PREPARED; break;
				}

				string sell = CitizensStringConstants.WILLING_PART_WITH; 
				if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_TRADE; } 
				else if (Utility.RandomBool() ){ sell = CitizensStringConstants.WILLING_SELL; }

				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.FOOD_VENDOR_PHRASE_FORMAT, crate.CrateQty, food, bought, city, sell ); break;
					case 1:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.FOOD_VENDOR_PHRASE_NEAR_FORMAT, crate.CrateQty, food, bought, city, sell ); break;
					case 2:	CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.FOOD_VENDOR_PHRASE_SOMEWHERE_FORMAT, crate.CrateQty, food, bought, city, sell ); break;
				}
				CitizenPhrase = CitizenPhrase + " " + string.Format( CitizensStringConstants.FOOD_VENDOR_CLOSING_FORMAT, food );

				PackItem( crate );
			}

			if ( CitizenType == CitizensConstants.CITIZEN_TYPE_WIZARD && ( CitizenService == CitizensConstants.CITIZEN_SERVICE_REAGENT_JAR_1 || CitizenService == CitizensConstants.CITIZEN_SERVICE_REAGENT_JAR_2 || CitizenService == CitizensConstants.CITIZEN_SERVICE_REAGENT_JAR_3 || CitizenService == CitizensConstants.CITIZEN_SERVICE_BOOK || CitizenService == CitizensConstants.CITIZEN_SERVICE_SCROLL || CitizenService == CitizensConstants.CITIZEN_SERVICE_WAND ) )
			{
				string aty1 = CitizensStringConstants.WIZARD_ITEM_JAR_WIZARD;
					if ( CitizenService == CitizensConstants.CITIZEN_SERVICE_REAGENT_JAR_2 ){ aty1 = CitizensStringConstants.WIZARD_ITEM_JAR_NECRO; }
					else if ( CitizenService == CitizensConstants.CITIZEN_SERVICE_REAGENT_JAR_3 ){ aty1 = CitizensStringConstants.WIZARD_ITEM_JAR_ALCHEMICAL; }
					else if ( CitizenService == CitizensConstants.CITIZEN_SERVICE_BOOK ){ aty1 = CitizensStringConstants.WIZARD_ITEM_BOOK; }
					else if ( CitizenService == CitizensConstants.CITIZEN_SERVICE_SCROLL ){ aty1 = CitizensStringConstants.WIZARD_ITEM_SCROLL; }
					else if ( CitizenService == CitizensConstants.CITIZEN_SERVICE_WAND ){ aty1 = CitizensStringConstants.WIZARD_ITEM_WAND; }

				string aty3 = CitizensStringConstants.WILLING_PART_WITH; 
				if (Utility.RandomBool() ){ aty3 = CitizensStringConstants.WILLING_TRADE; } 
				else if (Utility.RandomBool() ){ aty3 = CitizensStringConstants.WILLING_SELL; }

				CitizenPhrase = phrase + " " + string.Format( CitizensStringConstants.WIZARD_ITEM_SALE_FORMAT, aty1, aty3 );
				CitizenPhrase = CitizenPhrase + " " + CitizensStringConstants.WIZARD_ITEM_SALE_CLOSING;
			}

			string holding = "";
			List<Item> belongings = new List<Item>();
			foreach( Item i in this.Backpack.Items )
			{
				i.Movable = false;
				holding = i.Name;
				if ( i.Name != null && i.Name != "" ){} else { holding = MorphingItem.AddSpacesToSentence( (i.GetType()).Name ); }
				if ( Server.Misc.MaterialInfo.GetMaterialName( i ) != "" ){ holding = Server.Misc.MaterialInfo.GetMaterialName( i ) + " " + i.Name; }
				holding = cultInfo.ToTitleCase(holding);
			}

			if ( holding != "" ){ CitizenPhrase = CitizenPhrase + "<br><br>" + holding; } 
			else if ( CitizenService == CitizensConstants.CITIZEN_SERVICE_MAGIC_ITEM ){ CitizenPhrase = null; }
			else if ( ( CitizenService >= CitizensConstants.CITIZEN_SERVICE_REAGENT_JAR_1 && CitizenService <= CitizensConstants.CITIZEN_SERVICE_WAND ) && CitizenType == CitizensConstants.CITIZEN_TYPE_WIZARD ){ CitizenPhrase = null; }
		}

		#endregion

		#region Override Methods

		/// <summary>
		/// Called when a mobile moves within range of this citizen.
		/// Triggers random chatter if conditions are met.
		/// </summary>
		/// <param name="m">The mobile that moved</param>
		/// <param name="oldLocation">The previous location of the mobile</param>
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( !(this is HouseVisitor) )
			{
			Region reg = Region.Find( this.Location, this.Map );
			if ( DateTime.UtcNow >= m_NextTalk && InRange( m, CitizensConstants.TALK_RANGE ) )
			{
				if ( Utility.RandomBool() ){ TavernPatrons.GetChatter( this ); }
				Server.Misc.MaterialInfo.IsNoHairHat( 0, this );
				m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( CitizensConstants.TALK_DELAY_MIN, CitizensConstants.TALK_DELAY_MAX ) ));
			}
		}
		}

		/// <summary>
		/// Gets the context menu entries for this citizen.
		/// Adds a speech gump entry to allow players to interact.
		/// </summary>
		/// <param name="from">The mobile requesting the context menu</param>
		/// <param name="list">The list to add context menu entries to</param>
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		}

		/// <summary>
		/// Called before the citizen dies. Prevents death and plays a spell effect.
		/// </summary>
		/// <returns>Always returns false to prevent death</returns>
		public override bool OnBeforeDeath()
		{
			Say(CitizensStringConstants.DEATH_SPELL);
			this.Hits = this.HitsMax;
			this.FixedParticles( CitizensConstants.EFFECT_PARTICLE, CitizensConstants.EFFECT_SPEED, CitizensConstants.EFFECT_DURATION, CitizensConstants.EFFECT_ITEM_ID, EffectLayer.Waist );
			this.PlaySound( CitizensConstants.SOUND_DEATH );
			return false;
		}

		/// <summary>
		/// Determines if a mobile is an enemy of this citizen.
		/// Citizens are not enemies of any mobile.
		/// </summary>
		/// <param name="m">The mobile to check</param>
		/// <returns>Always returns false - citizens have no enemies</returns>
		public override bool IsEnemy( Mobile m )
		{
			return false;
		}

		/// <summary>
		/// Handles item drag and drop interactions with the citizen.
		/// Supports wand recharging, item repairs, unlocking containers, and item purchases.
		/// </summary>
		/// <param name="from">The mobile dropping the item</param>
		/// <param name="dropped">The item being dropped</param>
		/// <returns>Always returns false to prevent default handling</returns>
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			ArrayList wanderers = new ArrayList();
			foreach ( Mobile wanderer in World.Mobiles.Values )
			{
				if ( wanderer is Citizens && !( wanderer is HouseVisitor || wanderer is AdventurerWest || wanderer is AdventurerSouth || wanderer is AdventurerNorth || wanderer is AdventurerEast || wanderer is TavernPatronWest || wanderer is TavernPatronSouth || wanderer is TavernPatronNorth || wanderer is TavernPatronEast ) )
				{
					wanderers.Add( wanderer );
				}
			}
			for ( int i = 0; i < wanderers.Count; ++i )
			{
				Mobile person = ( Mobile )wanderers[ i ];
				//Effects.SendLocationParticles( EffectItem.Create( person.Location, person.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
				//person.PlaySound( 0x1FE );
				person.Delete();
			}

			ArrayList meetingSpots = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is MeetingSpots )
				{
					bool infected = false;
					String reg = Worlds.GetMyWorld( item.Map, item.Location, item.X, item.Y );
					if (reg != null && AdventuresFunctions.RegionIsInfected( reg ) )
					{
						foreach ( Mobile m in item.GetMobilesInRange( CitizensConstants.INFECTED_REGION_CHECK_RANGE ) )
						{
							if (m is BaseCreature)
							{
								if ( ((BaseCreature)m).CanInfect && !infected)
									infected = true;
							}
						}
					}
					if (!infected)
						meetingSpots.Add( item );
				}
			}
			for ( int i = 0; i < meetingSpots.Count; ++i )
			{
				Item spot = ( Item )meetingSpots[ i ];
				if ( PeopleMeetingHere() ){ CreateCitizenss( spot ); }
			}
			CreateDragonRiders();
		}

		/// <summary>
		/// Checks if a region allows mounts for citizens.
		/// </summary>
		/// <param name="reg">The region to check</param>
		/// <returns>True if mounts are allowed</returns>
		private static bool RegionAllowsMounts(Region reg)
		{
			return !(reg.IsPartOf("Anchor Rock Docks") || reg.IsPartOf("Kraken Reef Docks") || 
				reg.IsPartOf("Savage Sea Docks") || reg.IsPartOf("Serpent Sail Docks") || 
				reg.IsPartOf("the Forgotten Lighthouse"));
		}

		/// <summary>
		/// Creates a single citizen at the specified position.
		/// </summary>
		/// <param name="location">The location to spawn the citizen</param>
		/// <param name="map">The map to spawn on</param>
		/// <param name="direction">The direction the citizen should face</param>
		/// <param name="mount">Whether to mount the citizen</param>
		/// <param name="controlSlots">The number of control slots for the citizen</param>
		/// <returns>The created citizen, or null if creation failed</returns>
		private static Citizens CreateCitizenAtPosition(Point3D location, Map map, Direction direction, bool mount, int controlSlots)
		{
			Citizens citizen = new Citizens();
			if (citizen != null)
			{
				citizen.AddItem(new LightCitizen(false));
				citizen.MoveToWorld(location, map);
				if (mount)
				{
					MountCitizens(citizen, true);
				}
				citizen.Direction = direction;
				((BaseCreature)citizen).ControlSlots = controlSlots;
				//Effects.SendLocationParticles( EffectItem.Create( citizen.Location, citizen.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
				//citizen.PlaySound( 0x1FE );
				Server.Items.EssenceBase.ColorCitizen(citizen);
			}
			return citizen;
		}

		/// <summary>
		/// Creates citizens at a specific meeting spot.
		/// </summary>
		/// <param name="spot">The meeting spot item where citizens should spawn</param>
		public static void CreateCitizenss ( Item spot )
		{
			Region reg = Region.Find( spot.Location, spot.Map );

			int mod = CitizensConstants.CITIZEN_POS_MOD_NO_MOUNT;
			bool mount = false;

			if (RegionAllowsMounts(reg))
			{
				if (Utility.RandomBool())
				{
					mount = true;
					mod = CitizensConstants.CITIZEN_POS_MOD_MOUNT;
				}
			}

			Point3D cit1 = new Point3D((spot.X - mod), (spot.Y), spot.Z);
			Point3D cit2 = new Point3D((spot.X), (spot.Y - mod), spot.Z);
			Point3D cit3 = new Point3D((spot.X + mod), (spot.Y), spot.Z);
			Point3D cit4 = new Point3D((spot.X), (spot.Y + mod), spot.Z);

			int total = 0;

			// Create first citizen (East)
			if (Utility.RandomBool())
			{
				Citizens citizen = CreateCitizenAtPosition(cit1, spot.Map, Direction.East, mount, CitizensConstants.CONTROL_SLOTS_CITIZEN_1);
				if (citizen != null)
				{
					total++;
				}
			}

			// Create second citizen (South)
			if (Utility.RandomMinMax(1, 3) == 1)
			{
				Citizens citizen = CreateCitizenAtPosition(cit2, spot.Map, Direction.South, mount, CitizensConstants.CONTROL_SLOTS_CITIZEN_2);
				if (citizen != null)
				{
					total++;
				}
			}

			// Create third citizen (West) - ensure at least one
			if (Utility.RandomMinMax(1, 4) == 1 || total == 0)
			{
				Citizens citizen = CreateCitizenAtPosition(cit3, spot.Map, Direction.West, mount, CitizensConstants.CONTROL_SLOTS_CITIZEN_3);
				if (citizen != null)
				{
					total++;
				}
			}

			// Create fourth citizen (North) - ensure at least two
			if (Utility.RandomMinMax(1, 4) == 1 || total < 2)
			{
				Citizens citizen = CreateCitizenAtPosition(cit4, spot.Map, Direction.North, mount, CitizensConstants.CONTROL_SLOTS_CITIZEN_4);
				if (citizen != null)
				{
					total++;
				}
			}
		}

		/// <summary>
		/// Creates dragon riders at various locations.
		/// </summary>
		public static void CreateDragonRiders()
		{

				Point3D loc; Map map; Direction direction;

				if ( Utility.RandomBool() ){ loc = new Point3D( 3022, 969, 70 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Britain
				if ( Utility.RandomBool() ){ loc = new Point3D( 2985, 1042, 45 ); map = Map.Trammel; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Britain
				if ( Utility.RandomBool() ){ loc = new Point3D( 6728, 1797, 30 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Kuldara
				if ( Utility.RandomBool() ){ loc = new Point3D( 6752, 1665, 80 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Kuldara
				if ( Utility.RandomBool() ){ loc = new Point3D( 355, 1071, 65 ); map = Map.Tokuno; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Cimmeran Hold
				if ( Utility.RandomBool() ){ loc = new Point3D( 385, 1044, 99 ); map = Map.Tokuno; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Cimmeran Hold
				if ( Utility.RandomBool() ){ loc = new Point3D( 392, 1096, 59 ); map = Map.Tokuno; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Cimmeran Hold
				if ( Utility.RandomBool() ){ loc = new Point3D( 734, 367, 40 ); map = Map.Ilshenar; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Fort of Tenebrae
				if ( Utility.RandomBool() ){ loc = new Point3D( 1441, 3779, 30 ); map = Map.Trammel; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Town of Renika
				if ( Utility.RandomBool() ){ loc = new Point3D( 1395, 3668, 115 ); map = Map.Trammel; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // the Island of Umber Veil
				if ( Utility.RandomBool() ){ loc = new Point3D( 2278, 1667, 30 ); map = Map.Malas; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Furnace
				if ( Utility.RandomBool() ){ loc = new Point3D( 2176, 1680, 75 ); map = Map.Malas; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Furnace
				if ( Utility.RandomBool() ){ loc = new Point3D( 291, 1736, 60 ); map = Map.TerMur; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Village of Barako
				if ( Utility.RandomBool() ){ loc = new Point3D( 282, 1631, 110 ); map = Map.TerMur; direction = Direction.North; CreateDragonRider ( loc, map, direction ); } // the Savaged Empire
				if ( Utility.RandomBool() ){ loc = new Point3D( 786, 875, 55 ); map = Map.TerMur; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Kurak
				if ( Utility.RandomBool() ){ loc = new Point3D( 821, 982, 80 ); map = Map.TerMur; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Kurak
				if ( Utility.RandomBool() ){ loc = new Point3D( 2687, 3165, 60 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Port of Dusk
				if ( Utility.RandomBool() ){ loc = new Point3D( 2956, 1248, 70 ); map = Map.Felucca; direction = Direction.North; CreateDragonRider ( loc, map, direction ); } // the City of Elidor
				if ( Utility.RandomBool() ){ loc = new Point3D( 2970, 1319, 45 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Elidor
				if ( Utility.RandomBool() ){ loc = new Point3D( 2902, 1399, 55 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Elidor
				if ( Utility.RandomBool() ){ loc = new Point3D( 3737, 397, 44 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Town of Glacial Hills
				if ( Utility.RandomBool() ){ loc = new Point3D( 3660, 470, 44 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Town of Glacial Hills
				if ( Utility.RandomBool() ){ loc = new Point3D( 4215, 2993, 60 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // Greensky Village
				if ( Utility.RandomBool() ){ loc = new Point3D( 2827, 2258, 35 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Village of Islegem
				if ( Utility.RandomBool() ){ loc = new Point3D( 4842, 3266, 50 ); map = Map.Felucca; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // Kraken Reef Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 4815, 3112, 73 ); map = Map.Felucca; direction = Direction.Up; CreateDragonRider ( loc, map, direction ); } // Kraken Reef Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 4712, 3194, 84 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // Kraken Reef Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 1809, 2224, 70 ); map = Map.Felucca; direction = Direction.Right; CreateDragonRider ( loc, map, direction ); } // the City of Feluccaia
				if ( Utility.RandomBool() ){ loc = new Point3D( 1942, 2185, 57 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Feluccaia
				if ( Utility.RandomBool() ){ loc = new Point3D( 2084, 2195, 32 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Feluccaia
				if ( Utility.RandomBool() ){ loc = new Point3D( 841, 2019, 55 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Village of Portshine
				if ( Utility.RandomBool() ){ loc = new Point3D( 6763, 3649, 122 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Ravendark
				if ( Utility.RandomBool() ){ loc = new Point3D( 6759, 3756, 76 ); map = Map.Felucca; direction = Direction.Right; CreateDragonRider ( loc, map, direction ); } // the Village of Ravendark
				if ( Utility.RandomBool() ){ loc = new Point3D( 4232, 1454, 48 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Springvale
				if ( Utility.RandomBool() ){ loc = new Point3D( 4293, 1492, 45 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Village of Springvale
				if ( Utility.RandomBool() ){ loc = new Point3D( 4172, 1489, 45 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Springvale
				if ( Utility.RandomBool() ){ loc = new Point3D( 2381, 3155, 28 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Port of Starguide
				if ( Utility.RandomBool() ){ loc = new Point3D( 2302, 3154, 52 ); map = Map.Felucca; direction = Direction.West; CreateDragonRider ( loc, map, direction ); } // the Port of Starguide
				if ( Utility.RandomBool() ){ loc = new Point3D( 876, 904, 30 ); map = Map.Felucca; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // the Village of Whisper
				if ( Utility.RandomBool() ){ loc = new Point3D( 1101, 321, 66 ); map = Map.TerMur; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // Savage Sea Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 952, 1801, 50 ); map = Map.Malas; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // Serpent Sail Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 315, 1407, 17 ); map = Map.Trammel; direction = Direction.Left; CreateDragonRider ( loc, map, direction ); } // Anchor Rock Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 415, 1292, 67 ); map = Map.Trammel; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // Anchor Rock Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 5932, 2868, 45 ); map = Map.Trammel; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Lunar City of Dawn
				if ( Utility.RandomBool() ){ loc = new Point3D( 3705, 1486, 55 ); map = Map.Trammel; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // Death Gulch
				if ( Utility.RandomBool() ){ loc = new Point3D( 1608, 1507, 48 ); map = Map.Trammel; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // The Town of Devil Guard
				if ( Utility.RandomBool() ){ loc = new Point3D( 2084, 258, 60 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Fawn
				if ( Utility.RandomBool() ){ loc = new Point3D( 2168, 305, 60 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Fawn
				if ( Utility.RandomBool() ){ loc = new Point3D( 4781, 1185, 50 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // Glacial Coast Village
				if ( Utility.RandomBool() ){ loc = new Point3D( 869, 2068, 40 ); map = Map.Trammel; direction = Direction.North; CreateDragonRider ( loc, map, direction ); } // the Village of Grey
				if ( Utility.RandomBool() ){ loc = new Point3D( 3070, 2615, 60 ); map = Map.Trammel; direction = Direction.Up; CreateDragonRider ( loc, map, direction ); } // the City of Montor
				if ( Utility.RandomBool() ){ loc = new Point3D( 3180, 2613, 66 ); map = Map.Trammel; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Montor
				if ( Utility.RandomBool() ){ loc = new Point3D( 3322, 2638, 70 ); map = Map.Trammel; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Montor
				if ( Utility.RandomBool() ){ loc = new Point3D( 838, 692, 70 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Town of Moon
				if ( Utility.RandomBool() ){ loc = new Point3D( 4565, 1253, 82 ); map = Map.Trammel; direction = Direction.Left; CreateDragonRider ( loc, map, direction ); } // the Town of Mountain Crest
				if ( Utility.RandomBool() ){ loc = new Point3D( 1823, 758, 70 ); map = Map.Trammel; direction = Direction.Up; CreateDragonRider ( loc, map, direction ); } // the Land of Trammel
				if ( Utility.RandomBool() ){ loc = new Point3D( 7089, 610, 100 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Port
				if ( Utility.RandomBool() ){ loc = new Point3D( 7025, 680, 120 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Port

		}

		/// <summary>
		/// Creates a single dragon rider at the specified location.
		/// </summary>
		/// <param name="loc">The location to spawn the dragon rider</param>
		/// <param name="map">The map to spawn on</param>
		/// <param name="direction">The direction the dragon rider should face</param>
		public static void CreateDragonRider ( Point3D loc, Map map, Direction direction )
		{
			DragonRider citizen = new DragonRider();
			citizen.MoveToWorld( loc, map );
			MountCitizens ( citizen, true );
			citizen.Direction = direction;
			((BaseCreature)citizen).ControlSlots = 2;
			//Effects.SendLocationParticles( EffectItem.Create( citizen.Location, citizen.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
			//citizen.PlaySound( 0x1FE );
		}

		/// <summary>
		/// Checks if a citizen should be mounted based on location and type restrictions.
		/// </summary>
		/// <param name="m">The citizen mobile to check</param>
		/// <returns>True if the citizen should be mounted</returns>
		private static bool ShouldMountCitizen(Mobile m)
		{
			if (m is HouseVisitor)
				return false;

			// Check restricted map coordinates
			if (m.Map == Map.Trammel && m.X >= 2954 && m.Y >= 893 && m.X <= 3026 && m.Y <= 967)
				return false; // Castle British
			if (m.Map == Map.Felucca && m.X >= 1759 && m.Y >= 2195 && m.X <= 1821 && m.Y <= 2241)
				return false; // Castle of Knowledge
			if (m.Map == Map.TerMur && m.X >= 309 && m.Y >= 1738 && m.X <= 323 && m.Y <= 1751)
				return false; // Savaged Empire spot
			if (m.Map == Map.TerMur && m.X >= 284 && m.Y >= 1642 && m.X <= 298 && m.Y <= 1655)
				return false; // Savaged Empire spot
			if (m.Map == Map.TerMur && m.X >= 785 && m.Y >= 896 && m.X <= 805 && m.Y <= 879)
				return false; // Savaged Empire spot
			if (m.Map == Map.TerMur && m.X >= 706 && m.Y >= 953 && m.X <= 726 && m.Y <= 963)
				return false; // Savaged Empire spot
			if (m.Map == Map.Tokuno && m.X >= 364 && m.Y >= 1027 && m.X <= 415 && m.Y <= 1057)
				return false; // Cimmerian Castle

			// Check restricted regions
			if (m.Region.IsPartOf("Kraken Reef Docks") || m.Region.IsPartOf("Anchor Rock Docks") || 
				m.Region.IsPartOf("Serpent Sail Docks") || m.Region.IsPartOf("Savage Sea Docks") || 
				m.Region.IsPartOf("the Forgotten Lighthouse"))
				return false; // Ports

			// Check no-mount regions
			if (Server.Mobiles.AnimalTrainer.IsNoMountRegion(m.Region) && Server.Misc.MyServerSettings.NoMountsInCertainRegions())
				return false;

			return true;
		}

		/// <summary>
		/// Creates and configures a dragon mount for a DragonRider.
		/// </summary>
		/// <param name="m">The DragonRider mobile</param>
		private static void MountDragonRider(Mobile m)
		{
			BaseMount dragon = new RidingDragon();
			dragon.Body = Utility.RandomList(CitizensConstants.DRAGON_BODY_ID_1, CitizensConstants.DRAGON_BODY_ID_2);
			dragon.Blessed = true;
			dragon.Location = m.Location;
			dragon.OnAfterSpawn();
			Server.Mobiles.BaseMount.Ride(dragon, m);
		}

		/// <summary>
		/// Selects a random mount type for a citizen.
		/// </summary>
		/// <param name="m">The citizen mobile</param>
		/// <returns>The selected mount</returns>
		private static BaseMount SelectMountType(Mobile m)
		{
			BaseMount mount = new Horse();
			int roll = 0;

			switch (Utility.Random(30))
			{
				case 0: mount = SelectBearMount(); break;
				case 1: mount = SelectReptileMount(); break;
				case 2: mount = SelectWolfMount(m); break;
				case 3: mount = SelectCatMount(); break;
				case 4: mount = SelectOstardMount(); break;
				case 5: mount = SelectBirdMount(); break;
				case 6: mount = SelectDrakeMount(); break;
				case 7: mount = SelectBeetleMount(); break;
				case 8: mount = SelectRaptorMount(); break;
				case 9: mount = SelectHorseMount(m); break;
				case 10: mount = SelectExoticMount(); break;
				default: mount = new Horse(); break;
			}

			// Apply special horse colors
			if (mount is Horse && Utility.RandomMinMax(1, CitizensConstants.SPECIAL_HORSE_CHANCE) == 1)
			{
				ApplySpecialHorseColor(mount);
			}

			return mount;
		}

		/// <summary>
		/// Selects a bear-type mount.
		/// </summary>
		private static BaseMount SelectBearMount()
		{
			switch (Utility.RandomMinMax(1, 10))
			{
				case 1: return new CaveBearRiding();
				case 2: return new DireBear();
				case 3: return new ElderBlackBearRiding();
				case 4: return new ElderBrownBearRiding();
				case 5: return new ElderPolarBearRiding();
				case 6: return new GreatBear();
				case 7: return new GrizzlyBearRiding();
				case 8: return new KodiakBear();
				case 9: return new SabretoothBearRiding();
				case 10: return new PandaRiding();
				default: return new Horse();
			}
		}

		/// <summary>
		/// Selects a reptile-type mount.
		/// </summary>
		private static BaseMount SelectReptileMount()
		{
			switch (Utility.RandomMinMax(1, 4))
			{
				case 1: return new BullradonRiding();
				case 2: return new GorceratopsRiding();
				case 3: return new GorgonRiding();
				case 4: return new BasiliskRiding();
				default: return new Horse();
			}
		}

		/// <summary>
		/// Selects a wolf-type mount.
		/// </summary>
		private static BaseMount SelectWolfMount(Mobile m)
		{
			int roll = Utility.RandomMinMax(1, 4);
			if (Server.Misc.MorphingTime.CheckNecro(m))
			{
				roll = Utility.RandomMinMax(3, 4);
			}

			switch (roll)
			{
				case 1: return new WhiteWolf();
				case 2: return new WinterWolf();
				case 3: return new BlackWolf();
				case 4:
					Server.Misc.MorphingTime.TurnToNecromancer(m);
					return new DemonDog();
				default: return new Horse();
			}
		}

		/// <summary>
		/// Selects a cat-type mount.
		/// </summary>
		private static BaseMount SelectCatMount()
		{
			switch (Utility.RandomMinMax(1, 6))
			{
				case 1: return new LionRiding();
				case 2: return new SnowLion();
				case 3: return new TigerRiding();
				case 4: return new WhiteTigerRiding();
				case 5: return new PredatorHellCatRiding();
				case 6: return new SabretoothTigerRiding();
				default: return new Horse();
			}
		}

		/// <summary>
		/// Selects an ostard-type mount.
		/// </summary>
		private static BaseMount SelectOstardMount()
		{
			switch (Utility.RandomMinMax(1, 4))
			{
				case 1: return new DesertOstard();
				case 2: return new ForestOstard();
				case 3: return new FrenziedOstard();
				case 4: return new SnowOstard();
				default: return new Horse();
			}
		}

		/// <summary>
		/// Selects a bird-type mount.
		/// </summary>
		private static BaseMount SelectBirdMount()
		{
			switch (Utility.RandomMinMax(1, 5))
			{
				case 1: return new GiantHawk();
				case 2: return new GiantRaven();
				case 3: return new Roc();
				case 4: return new Phoenix();
				case 5: return new AxeBeakRiding();
				default: return new Horse();
			}
		}

		/// <summary>
		/// Selects a drake-type mount.
		/// </summary>
		private static BaseMount SelectDrakeMount()
		{
			switch (Utility.RandomMinMax(1, 4))
			{
				case 1: return new SwampDrakeRiding();
				case 2: return new Wyverns();
				case 3: return new Teradactyl();
				case 4:
					BaseMount gemDragon = new GemDragon();
					gemDragon.Hue = 0;
					gemDragon.ItemID = Utility.RandomMinMax(CitizensConstants.GEM_DRAGON_ITEM_ID_1, CitizensConstants.GEM_DRAGON_ITEM_ID_2);
					return gemDragon;
				default: return new Horse();
			}
		}

		/// <summary>
		/// Selects a beetle-type mount.
		/// </summary>
		private static BaseMount SelectBeetleMount()
		{
			switch (Utility.RandomMinMax(1, 6))
			{
				case 1: return new Beetle();
				case 2: return new FireBeetle();
				case 3: return new GlowBeetleRiding();
				case 4: return new PoisonBeetleRiding();
				case 5: return new TigerBeetleRiding();
				case 6: return new WaterBeetleRiding();
				default: return new Horse();
			}
		}

		/// <summary>
		/// Selects a raptor-type mount.
		/// </summary>
		private static BaseMount SelectRaptorMount()
		{
			switch (Utility.RandomMinMax(1, 5))
			{
				case 1: return new RaptorRiding();
				case 2: return new RavenousRiding();
				case 3:
					BaseMount raptor = new RaptorRiding();
					raptor.Body = CitizensConstants.RAPTOR_BODY_ID_1;
					raptor.ItemID = CitizensConstants.RAPTOR_BODY_ID_1;
					return raptor;
				case 4:
					BaseMount raptor2 = new RaptorRiding();
					raptor2.Body = CitizensConstants.RAPTOR_BODY_ID_2;
					raptor2.ItemID = CitizensConstants.RAPTOR_BODY_ID_2;
					return raptor2;
				case 5:
					BaseMount raptor3 = new RaptorRiding();
					raptor3.Body = CitizensConstants.RAPTOR_BODY_ID_3;
					raptor3.ItemID = CitizensConstants.RAPTOR_BODY_ID_3;
					return raptor3;
				default: return new Horse();
			}
		}

		/// <summary>
		/// Selects a horse-type mount (including special horses).
		/// </summary>
		private static BaseMount SelectHorseMount(Mobile m)
		{
			int roll = Utility.RandomMinMax(0, 8);
			if (Server.Misc.MorphingTime.CheckNecro(m))
			{
				roll = Utility.RandomMinMax(3, 8);
			}

			switch (roll)
			{
				case 0: return new ZebraRiding();
				case 1: return new Unicorn();
				case 2: return new IceSteed();
				case 3: return new FireSteed();
				case 4: return new Nightmare();
				case 5: return new AncientNightmareRiding();
				case 6:
					Server.Misc.MorphingTime.TurnToNecromancer(m);
					return new DarkUnicornRiding();
				case 7:
					Server.Misc.MorphingTime.TurnToNecromancer(m);
					return new HellSteed();
				case 8: return new Dreadhorn();
				default: return new Horse();
			}
		}

		/// <summary>
		/// Selects an exotic-type mount.
		/// </summary>
		private static BaseMount SelectExoticMount()
		{
			switch (Utility.RandomMinMax(1, 6))
			{
				case 1: return new Ramadon();
				case 2: return new RidableLlama();
				case 3: return new GriffonRiding();
				case 4: return new HippogriffRiding();
				case 5: return new Kirin();
				case 6: return new ManticoreRiding();
				default: return new Horse();
			}
		}

		/// <summary>
		/// Applies special material color to a horse mount.
		/// </summary>
		/// <param name="mount">The horse mount to color</param>
		private static void ApplySpecialHorseColor(BaseMount mount)
		{
			mount.Body = CitizensConstants.SPECIAL_HORSE_BODY_ID;
			mount.ItemID = CitizensConstants.SPECIAL_HORSE_ITEM_ID;

			switch (Utility.RandomMinMax(1, CitizensConstants.SPECIAL_HORSE_HUE_COUNT))
			{
				case 1: mount.Hue = MaterialInfo.GetMaterialColor("dull copper", "classic", 0); break;
				case 2: mount.Hue = MaterialInfo.GetMaterialColor("shadow iron", "classic", 0); break;
				case 3: mount.Hue = MaterialInfo.GetMaterialColor("copper", "classic", 0); break;
				case 4: mount.Hue = MaterialInfo.GetMaterialColor("bronze", "classic", 0); break;
				case 5: mount.Hue = MaterialInfo.GetMaterialColor("gold", "classic", 0); break;
				case 6: mount.Hue = MaterialInfo.GetMaterialColor("agapite", "classic", 0); break;
				case 7: mount.Hue = MaterialInfo.GetMaterialColor("verite", "classic", 0); break;
				case 8: mount.Hue = MaterialInfo.GetMaterialColor("valorite", "classic", 0); break;
				case 9: mount.Hue = MaterialInfo.GetMaterialColor("nepturite", "classic", 0); break;
				case 10: mount.Hue = MaterialInfo.GetMaterialColor("obsidian", "classic", 0); break;
				case 11: mount.Hue = MaterialInfo.GetMaterialColor("steel", "classic", 0); break;
				case 12: mount.Hue = MaterialInfo.GetMaterialColor("brass", "classic", 0); break;
				case 13: mount.Hue = MaterialInfo.GetMaterialColor("mithril", "classic", 0); break;
				case 14: mount.Hue = MaterialInfo.GetMaterialColor("xormite", "classic", 0); break;
				case 15: mount.Hue = MaterialInfo.GetMaterialColor("dwarven", "classic", 0); break;
				case 16: mount.Hue = MaterialInfo.GetMaterialColor("silver", "classic", 0); break;
			}
		}

		/// <summary>
		/// Mounts citizens on various mounts based on their type and location.
		/// </summary>
		/// <param name="m">The citizen mobile to mount</param>
		/// <param name="includeDragyns">Whether to include dragon mounts</param>
		public static void MountCitizens ( Mobile m, bool includeDragyns )
		{
			if (m is DragonRider)
			{
				MountDragonRider(m);
				return;
			}

			if (!ShouldMountCitizen(m))
			{
				return;
			}

			BaseMount mount = SelectMountType(m);
			Server.Mobiles.BaseMount.Ride(mount, m);
		}

		/// <summary>
		/// Determines if people should be meeting at a location.
		/// </summary>
		/// <returns>Randomly returns true or false</returns>
		public static bool PeopleMeetingHere()
		{
			if ( Utility.RandomBool() )
				return true;

			return false;
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">The serialization reader</param>
		public Citizens( Serial serial ) : base( serial )
		{
		}

		/// <summary>
		/// Serializes the citizen data
		/// </summary>
		/// <param name="writer">The writer to serialize to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( CitizenService );
			writer.Write( CitizenType );
			writer.Write( CitizenCost );
			writer.Write( CitizenPhrase );
			writer.Write( CitizenRumor );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			CitizenService = reader.ReadInt();
			CitizenType = reader.ReadInt();
			CitizenCost = reader.ReadInt();
			CitizenPhrase = reader.ReadString();
			CitizenRumor = reader.ReadString();
		}

		#endregion

		#region Override Methods (Continued)

		/// <summary>
		/// Called after the citizen spawns. Applies transformations and color effects.
		/// Also resets position to home if citizen has wandered too far.
		/// </summary>
		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Server.Misc.MorphingTime.CheckNecromancer( this );

			if ( this.Home.X > 0 && this.Home.Y > 0 && ( Math.Abs( this.X-this.Home.X ) > CitizensConstants.HOME_POSITION_TOLERANCE || Math.Abs( this.Y-this.Home.Y ) > CitizensConstants.HOME_POSITION_TOLERANCE || Math.Abs( this.Z-this.Home.Z ) > CitizensConstants.HOME_POSITION_TOLERANCE ) )
			{
				this.Location = this.Home;
				//Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				//Effects.PlaySound( this, this.Map, 0x201 );
			}
		}

		/// <summary>
		/// Called when the citizen's map changes. Applies transformations.
		/// </summary>
		/// <param name="oldMap">The previous map</param>
		protected override void OnMapChange( Map oldMap )
		{
			base.OnMapChange( oldMap );
			Server.Misc.MorphingTime.CheckNecromancer( this );
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Context menu entry for opening the citizen's speech gump.
		/// </summary>
		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( CitizensConstants.CONTEXT_MENU_ID, CitizensConstants.CONTEXT_MENU_RANGE )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
				if (!(m_Mobile is PlayerMobile))
					return;

				Citizens citizen = (Citizens)m_Giver;
				PlayerMobile mobile = (PlayerMobile)m_Mobile;
				string speak = "";

				if (m_Giver.Fame == 0 && m_Mobile.Backpack.FindItemByType(typeof(MuseumBook)) != null && !(m_Giver is HouseVisitor))
				{
					speak = MuseumBook.TellRumor(m_Mobile, m_Giver);
				}
				if (speak == "" && m_Giver.Fame == 0 && m_Mobile.Backpack.FindItemByType(typeof(QuestTome)) != null && !(m_Giver is HouseVisitor))
				{
					speak = QuestTome.TellRumor(m_Mobile, m_Giver);
				}

				if (speak != "")
				{
					m_Mobile.PlaySound(CitizensConstants.SOUND_SPEECH);
					m_Giver.Say(speak);
				}
				else if (citizen.CitizenService == 0)
				{
					speak = citizen.CitizenRumor;
					if (speak.Contains(CitizensStringConstants.PLACEHOLDER_PLAYER_NAME))
					{
						speak = speak.Replace(CitizensStringConstants.PLACEHOLDER_PLAYER_NAME, m_Mobile.Name);
					}
					if (speak.Contains(CitizensStringConstants.PLACEHOLDER_REGION_NAME))
					{
						speak = speak.Replace(CitizensStringConstants.PLACEHOLDER_REGION_NAME, m_Mobile.Region.Name);
					}
					m_Giver.Say(speak);
				}
				else
				{
					mobile.CloseGump(typeof(CitizenGump));
					mobile.SendGump(new CitizenGump(m_Giver, m_Mobile));
				}
			}
		}

		/// <summary>
		/// Gump displayed when interacting with a citizen.
		/// Shows the citizen's phrase with placeholders replaced.
		/// </summary>
		public class CitizenGump : Gump
		{
			private Mobile c_Citizen;
			private Mobile c_Player;

			public CitizenGump( Mobile citizen, Mobile player ) : base( CitizensConstants.GUMP_X, CitizensConstants.GUMP_Y )
			{
				c_Citizen = citizen;
				Citizens b_Citizen = (Citizens)citizen;
				c_Player = player;

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				string speak = b_Citizen.CitizenPhrase;
				if ( speak.Contains(CitizensStringConstants.PLACEHOLDER_PLAYER_NAME) ){ speak = speak.Replace(CitizensStringConstants.PLACEHOLDER_PLAYER_NAME, c_Player.Name); }
				if ( speak.Contains(CitizensStringConstants.PLACEHOLDER_REGION_NAME) ){ speak = speak.Replace(CitizensStringConstants.PLACEHOLDER_REGION_NAME, c_Player.Region.Name); }
				if ( speak.Contains(CitizensStringConstants.PLACEHOLDER_GOLD_AMOUNT) ){ speak = speak.Replace(CitizensStringConstants.PLACEHOLDER_GOLD_AMOUNT, (b_Citizen.CitizenCost).ToString()); }

				AddPage(0);
				AddImage(0, 0, CitizensConstants.GUMP_IMAGE_1);
				AddImage(269, 0, CitizensConstants.GUMP_IMAGE_1);
				AddImage(2, 2, CitizensConstants.GUMP_IMAGE_2);
				AddImage(271, 2, CitizensConstants.GUMP_IMAGE_2);
				AddImage(6, 6, CitizensConstants.GUMP_IMAGE_3);
				AddImage(167, 7, CitizensConstants.GUMP_IMAGE_4);
				AddImage(244, 7, CitizensConstants.GUMP_IMAGE_4);
				AddImage(530, 9, CitizensConstants.GUMP_IMAGE_5);

				AddHtml( CitizensConstants.GUMP_HTML_X, CitizensConstants.GUMP_HTML_Y, CitizensConstants.GUMP_HTML_WIDTH, CitizensConstants.GUMP_HTML_HEIGHT, @"<BODY><BASEFONT Color=#" + CitizensConstants.GUMP_HTML_COLOR.ToString("X") + "><BIG>" + speak + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			from.CloseGump( typeof( CitizenGump ) );
			int sound = 0;
			string say = "";

			// Handle special item types
			if ( dropped is Cargo )
			{
				Server.Items.Cargo.GiveCargo( (Cargo)dropped, this, from );
				return false;
			}

			// Handle gold payment for item purchase
			if ( dropped is Gold )
			{
				HandleItemPurchase( from, (Gold)dropped, out say, out sound );
				SayTo(from, say);
				if ( sound > 0 ){ from.PlaySound( sound ); }
				return false;
			}

			// Handle wand recharging
			if ( CitizenType == CitizensConstants.CITIZEN_TYPE_WIZARD && dropped is BaseMagicStaff )
			{
				HandleWandRecharge( (BaseMagicStaff)dropped, out say, out sound );
				SayTo(from, say);
				if ( sound > 0 ){ from.PlaySound( sound ); }
				return false;
			}

			// Handle container unlocking
			if ( CitizenService == CitizensConstants.CITIZEN_SERVICE_REPAIR && CitizenType == CitizensConstants.CITIZEN_TYPE_ROGUE && dropped is LockableContainer )
			{
				HandleContainerUnlock( (LockableContainer)dropped, out say, out sound );
				SayTo(from, say);
				if ( sound > 0 ){ from.PlaySound( sound ); }
				return false;
			}

			// Determine item types
			bool isArmor = dropped is BaseArmor;
			bool isWeapon = dropped is BaseWeapon;
			bool isMetal = Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( dropped );
			bool isWood = Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( dropped );
			bool isLeather = Server.Misc.MaterialInfo.IsAnyKindOfClothItem( dropped );

			// Determine repair type
			bool fixArmor = false;
			bool fixWeapon = false;
			DetermineRepairType( dropped, isArmor, isWeapon, isMetal, isWood, isLeather, out fixArmor, out fixWeapon, out sound );

			// Handle item repairs
			Container bank = from.FindBankNoCreate();
			if ( fixArmor && dropped is BaseArmor )
			{
				if ( ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( Gold ), CitizensConstants.COST_REPAIR ) ) || ( bank != null && bank.ConsumeTotal( typeof( Gold ), CitizensConstants.COST_REPAIR ) ) )
				{
					say = RepairArmor( (BaseArmor)dropped );
				}
				else
				{
					say = CitizensStringConstants.ERROR_NOT_ENOUGH_GOLD;
					sound = 0;
				}
			}
			else if ( fixWeapon && dropped is BaseWeapon )
			{
				if ( ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( Gold ), CitizensConstants.COST_REPAIR ) ) || ( bank != null && bank.ConsumeTotal( typeof( Gold ), CitizensConstants.COST_REPAIR ) ) )
				{
					say = RepairWeapon( (BaseWeapon)dropped );
				}
				else
				{
					say = CitizensStringConstants.ERROR_NOT_ENOUGH_GOLD;
					sound = 0;
				}
			}

			SayTo(from, say);
			if ( sound > 0 ){ from.PlaySound( sound ); }

			return false;
		}

		#endregion
	}
}

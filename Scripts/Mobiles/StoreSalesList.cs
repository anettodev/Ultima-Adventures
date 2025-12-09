using System;
using System.Collections.Generic;
using System.Collections;
using Server;
using Server.Engines.Apiculture;
using Server.Items;
using Server.Misc;
using Server.Custom;
using Server.Multis;
using Server.Guilds;
using Server.Engines.Mahjong;
using Server.Gumps;
using Server.Mobiles;
using Server.Mobiles.Vendors;
using Server.Mobiles.Vendors.ShopDefinitions;

namespace Server.Mobiles
{
	public abstract class SBInfo
	{
		public SBInfo()
		{
		}

		public abstract IShopSellInfo SellInfo { get; }

        public abstract List<GenericBuyInfo> BuyInfo { get; }
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	/// <summary>
	/// Shop info for Doom Variety Dealer vendor.
	/// Sells a variety of common items including potions, reagents, scrolls, and rare items.
	/// </summary>
	public class SBDoomVarietyDealer : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBDoomVarietyDealer class.
		/// </summary>
		public SBDoomVarietyDealer()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Basic supplies
				StoreSalesListHelper.AddBuyItem(this, typeof(Bandage), StoreSalesListConstants.PRICE_BANDAGE, StoreSalesListConstants.QTY_UNLIMITED, StoreSalesListConstants.ITEMID_BANDAGE);
				StoreSalesListHelper.AddBuyItem(this, typeof(BlankScroll), StoreSalesListConstants.PRICE_BLANK_SCROLL, StoreSalesListConstants.QTY_UNLIMITED, StoreSalesListConstants.ITEMID_BLANK_SCROLL);

				// Rare items
				StoreSalesListHelper.AddBuyItem(this, typeof(HoodedShroudOfShadows), StoreSalesListConstants.PRICE_HOODED_SHROUD, StoreSalesListConstants.QTY_SINGLE, StoreSalesListConstants.ITEMID_HOODED_SHROUD);
				StoreSalesListHelper.AddBuyItem(this, typeof(ChargerOfTheFallen), StoreSalesListConstants.PRICE_CHARGER_FALLEN, StoreSalesListConstants.QTY_SINGLE, StoreSalesListConstants.ITEMID_CHARGER_FALLEN);

				// Potions
				StoreSalesListHelper.AddBuyItemRandomMinMax(this, typeof(NightSightPotion), StoreSalesListConstants.PRICE_LESSER_POTION, StoreSalesListConstants.QTY_POTION_MIN, StoreSalesListConstants.QTY_POTION_MAX, StoreSalesListConstants.ITEMID_POTION_NIGHTSIGHT);
				StoreSalesListHelper.AddBuyItemRandomMinMax(this, typeof(AgilityPotion), StoreSalesListConstants.PRICE_LESSER_POTION, StoreSalesListConstants.QTY_POTION_MIN, StoreSalesListConstants.QTY_POTION_MAX, StoreSalesListConstants.ITEMID_POTION_AGILITY);
				StoreSalesListHelper.AddBuyItemRandomMinMax(this, typeof(StrengthPotion), StoreSalesListConstants.PRICE_LESSER_POTION, StoreSalesListConstants.QTY_POTION_MIN, StoreSalesListConstants.QTY_POTION_MAX, StoreSalesListConstants.ITEMID_POTION_STRENGTH);
				StoreSalesListHelper.AddBuyItemRandomMinMax(this, typeof(RefreshPotion), StoreSalesListConstants.PRICE_LESSER_POTION, StoreSalesListConstants.QTY_POTION_MIN, StoreSalesListConstants.QTY_POTION_MAX, StoreSalesListConstants.ITEMID_POTION_REFRESH);
				StoreSalesListHelper.AddBuyItemRandomMinMax(this, typeof(LesserCurePotion), StoreSalesListConstants.PRICE_LESSER_POTION, StoreSalesListConstants.QTY_POTION_MIN, StoreSalesListConstants.QTY_POTION_MAX, StoreSalesListConstants.ITEMID_POTION_LESSER_CURE);
				StoreSalesListHelper.AddBuyItemRandomMinMax(this, typeof(LesserHealPotion), StoreSalesListConstants.PRICE_LESSER_POTION, StoreSalesListConstants.QTY_POTION_MIN, StoreSalesListConstants.QTY_POTION_MAX, StoreSalesListConstants.ITEMID_POTION_LESSER_HEAL);
				StoreSalesListHelper.AddBuyItemRandomMinMax(this, typeof(LesserPoisonPotion), StoreSalesListConstants.PRICE_LESSER_POTION, StoreSalesListConstants.QTY_POTION_MIN, StoreSalesListConstants.QTY_POTION_MAX, StoreSalesListConstants.ITEMID_POTION_LESSER_POISON);
				StoreSalesListHelper.AddBuyItemRandomMinMax(this, typeof(LesserExplosionPotion), StoreSalesListConstants.PRICE_LESSER_POTION, StoreSalesListConstants.QTY_POTION_MIN, StoreSalesListConstants.QTY_POTION_MAX, StoreSalesListConstants.ITEMID_POTION_LESSER_EXPLOSION);
				StoreSalesListHelper.AddBuyItemRandomMinMax(this, typeof(GreaterExplosionPotion), StoreSalesListConstants.PRICE_GREATER_POTION, StoreSalesListConstants.QTY_POTION_MIN, StoreSalesListConstants.QTY_POTION_MAX, StoreSalesListConstants.ITEMID_POTION_LESSER_EXPLOSION);

				// Ammunition
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Bolt), StoreSalesListConstants.PRICE_BOLT, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MIN, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MAX, StoreSalesListConstants.ITEMID_BOLT);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Arrow), StoreSalesListConstants.PRICE_ARROW, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MIN, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MAX, StoreSalesListConstants.ITEMID_ARROW);

				// Reagents
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(BlackPearl), StoreSalesListConstants.PRICE_REAGENT, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MIN, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MAX, StoreSalesListConstants.ITEMID_REAGENT_BLACK_PEARL);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Bloodmoss), StoreSalesListConstants.PRICE_REAGENT, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MIN, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MAX, StoreSalesListConstants.ITEMID_REAGENT_BLOODMOSS);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(MandrakeRoot), StoreSalesListConstants.PRICE_REAGENT, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MIN, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MAX, StoreSalesListConstants.ITEMID_REAGENT_MANDRAKE);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Garlic), StoreSalesListConstants.PRICE_REAGENT, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MIN, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MAX, StoreSalesListConstants.ITEMID_REAGENT_GARLIC);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Ginseng), StoreSalesListConstants.PRICE_REAGENT, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MIN, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MAX, StoreSalesListConstants.ITEMID_REAGENT_GINSENG);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Nightshade), StoreSalesListConstants.PRICE_REAGENT, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MIN, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MAX, StoreSalesListConstants.ITEMID_REAGENT_NIGHTSHADE);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(SpidersSilk), StoreSalesListConstants.PRICE_REAGENT, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MIN, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MAX, StoreSalesListConstants.ITEMID_REAGENT_SPIDERS_SILK);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(SulfurousAsh), StoreSalesListConstants.PRICE_REAGENT, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MIN, StoreSalesListConstants.QTY_RANDOM_VERY_LARGE_MAX, StoreSalesListConstants.ITEMID_REAGENT_SULFUROUS_ASH);

				// Food
				StoreSalesListHelper.AddBuyItemRandomMinMax(this, typeof(BreadLoaf), StoreSalesListConstants.PRICE_BREAD_LOAF, StoreSalesListConstants.QTY_POTION_MIN, StoreSalesListConstants.QTY_POTION_MAX, StoreSalesListConstants.ITEMID_BREAD_PIE);
				StoreSalesListHelper.AddBuyItemRandomMinMax(this, typeof(MeatPie), StoreSalesListConstants.PRICE_MEAT_PIE, StoreSalesListConstants.QTY_POTION_MIN, StoreSalesListConstants.QTY_POTION_MAX, StoreSalesListConstants.ITEMID_BREAD_PIE);
				StoreSalesListHelper.AddBuyItem(this, typeof(Backpack), StoreSalesListConstants.PRICE_BACKPACK, StoreSalesListConstants.QTY_FIXED_20, StoreSalesListConstants.ITEMID_BACKPACK);

				// Scrolls
				Type[] types = Loot.RegularScrollTypes;
				int circles = 3;

				for (int i = 0; i < circles * 8 && i < types.Length; ++i)
				{
					int itemID = StoreSalesListConstants.ITEMID_SCROLL_START + i;

					if (i == 6)
						itemID = StoreSalesListConstants.ITEMID_SCROLL_SPECIAL;
					else if (i > 6)
						--itemID;

					int price = StoreSalesListConstants.PRICE_SCROLL_BASE + ((i / 8) * StoreSalesListConstants.PRICE_SCROLL_INCREMENT);
					StoreSalesListHelper.AddBuyItem(this, types[i], price, StoreSalesListConstants.QTY_FIXED_20, itemID);
				}

				// Necromancy reagents
				StoreSalesListHelper.AddBuyItem(this, typeof(BatWing), StoreSalesListConstants.PRICE_NECRO_REAGENT, StoreSalesListConstants.QTY_UNLIMITED, StoreSalesListConstants.ITEMID_NECRO_BAT_WING);
				StoreSalesListHelper.AddBuyItem(this, typeof(GraveDust), StoreSalesListConstants.PRICE_NECRO_REAGENT, StoreSalesListConstants.QTY_UNLIMITED, StoreSalesListConstants.ITEMID_NECRO_GRAVE_DUST);
				StoreSalesListHelper.AddBuyItem(this, typeof(DaemonBlood), StoreSalesListConstants.PRICE_NECRO_REAGENT_RARE, StoreSalesListConstants.QTY_UNLIMITED, StoreSalesListConstants.ITEMID_NECRO_DAEMON_BLOOD);
				StoreSalesListHelper.AddBuyItem(this, typeof(NoxCrystal), StoreSalesListConstants.PRICE_NECRO_REAGENT_RARE, StoreSalesListConstants.QTY_UNLIMITED, StoreSalesListConstants.ITEMID_NECRO_NOX_CRYSTAL);
				StoreSalesListHelper.AddBuyItem(this, typeof(PigIron), StoreSalesListConstants.PRICE_PIG_IRON, StoreSalesListConstants.QTY_UNLIMITED, StoreSalesListConstants.ITEMID_NECRO_PIG_IRON);

				// Special items
				StoreSalesListHelper.AddBuyItem(this, typeof(RecallRune), StoreSalesListConstants.PRICE_RECALL_RUNE, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_RECALL_RUNE);
				StoreSalesListHelper.AddBuyItem(this, typeof(FullMagerySpellbook), StoreSalesListConstants.PRICE_FULL_MAGERY_SPELLBOOK, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_SPELLBOOK);
				StoreSalesListHelper.AddBuyItem(this, typeof(FullNecroSpellbook), StoreSalesListConstants.PRICE_FULL_NECRO_SPELLBOOK, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_SPELLBOOK);

				StoreSalesListHelper.AddBuyItemWithName(this, "1041072", typeof(MagicWizardsHat), StoreSalesListConstants.PRICE_MAGIC_WIZARDS_HAT, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_MAGIC_WIZARDS_HAT);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Basic supplies
				StoreSalesListHelper.AddSellItem(this, typeof(Bandage), StoreSalesListConstants.SELL_PRICE_BANDAGE);
				StoreSalesListHelper.AddSellItem(this, typeof(BlankScroll), StoreSalesListConstants.SELL_PRICE_BLANK_SCROLL);

				// Potions
				StoreSalesListHelper.AddSellItem(this, typeof(NightSightPotion), StoreSalesListConstants.SELL_PRICE_LESSER_POTION);
				StoreSalesListHelper.AddSellItem(this, typeof(AgilityPotion), StoreSalesListConstants.SELL_PRICE_LESSER_POTION);
				StoreSalesListHelper.AddSellItem(this, typeof(StrengthPotion), StoreSalesListConstants.SELL_PRICE_LESSER_POTION);
				StoreSalesListHelper.AddSellItem(this, typeof(RefreshPotion), StoreSalesListConstants.SELL_PRICE_LESSER_POTION);
				StoreSalesListHelper.AddSellItem(this, typeof(LesserCurePotion), StoreSalesListConstants.SELL_PRICE_LESSER_POTION);
				StoreSalesListHelper.AddSellItem(this, typeof(LesserHealPotion), StoreSalesListConstants.SELL_PRICE_LESSER_POTION);
				StoreSalesListHelper.AddSellItem(this, typeof(LesserPoisonPotion), StoreSalesListConstants.SELL_PRICE_LESSER_POTION);
				StoreSalesListHelper.AddSellItem(this, typeof(LesserExplosionPotion), StoreSalesListConstants.SELL_PRICE_LESSER_EXPLOSION_POTION);

				// Ammunition
				StoreSalesListHelper.AddSellItem(this, typeof(Bolt), StoreSalesListConstants.SELL_PRICE_BOLT);
				StoreSalesListHelper.AddSellItem(this, typeof(Arrow), StoreSalesListConstants.SELL_PRICE_ARROW);

				// Reagents
				StoreSalesListHelper.AddSellItem(this, typeof(BlackPearl), StoreSalesListConstants.SELL_PRICE_REAGENT);
				StoreSalesListHelper.AddSellItem(this, typeof(Bloodmoss), StoreSalesListConstants.SELL_PRICE_REAGENT);
				StoreSalesListHelper.AddSellItem(this, typeof(MandrakeRoot), StoreSalesListConstants.SELL_PRICE_REAGENT);
				StoreSalesListHelper.AddSellItem(this, typeof(Garlic), StoreSalesListConstants.SELL_PRICE_REAGENT);
				StoreSalesListHelper.AddSellItem(this, typeof(Ginseng), StoreSalesListConstants.SELL_PRICE_REAGENT);
				StoreSalesListHelper.AddSellItem(this, typeof(Nightshade), StoreSalesListConstants.SELL_PRICE_REAGENT);
				StoreSalesListHelper.AddSellItem(this, typeof(SpidersSilk), StoreSalesListConstants.SELL_PRICE_REAGENT);
				StoreSalesListHelper.AddSellItem(this, typeof(SulfurousAsh), StoreSalesListConstants.SELL_PRICE_REAGENT);

				// Food and containers
				StoreSalesListHelper.AddSellItem(this, typeof(BreadLoaf), StoreSalesListConstants.SELL_PRICE_BREAD_LOAF);
				StoreSalesListHelper.AddSellItem(this, typeof(Backpack), StoreSalesListConstants.SELL_PRICE_BACKPACK);
				StoreSalesListHelper.AddSellItem(this, typeof(RecallRune), StoreSalesListConstants.SELL_PRICE_RECALL_RUNE);
				StoreSalesListHelper.AddSellItem(this, typeof(Spellbook), StoreSalesListConstants.SELL_PRICE_SPELLBOOK);
				StoreSalesListHelper.AddSellItem(this, typeof(BlankScroll), StoreSalesListConstants.SELL_PRICE_BLANK_SCROLL);

				// Necromancy reagents (AOS only)
				if (Core.AOS)
				{
					StoreSalesListHelper.AddSellItem(this, typeof(BatWing), StoreSalesListConstants.SELL_PRICE_NECRO_REAGENT);
					StoreSalesListHelper.AddSellItem(this, typeof(GraveDust), StoreSalesListConstants.SELL_PRICE_NECRO_REAGENT);
					StoreSalesListHelper.AddSellItem(this, typeof(DaemonBlood), StoreSalesListConstants.SELL_PRICE_NECRO_REAGENT);
					StoreSalesListHelper.AddSellItem(this, typeof(NoxCrystal), StoreSalesListConstants.SELL_PRICE_NECRO_REAGENT);
					StoreSalesListHelper.AddSellItem(this, typeof(PigIron), StoreSalesListConstants.SELL_PRICE_NECRO_REAGENT);
				}

				// Scrolls
				Type[] types = Loot.RegularScrollTypes;
				for (int i = 0; i < types.Length; ++i)
				{
					int price = (StoreSalesListConstants.PRICE_SCROLL_SELL_BASE + (i / 8)) * StoreSalesListConstants.PRICE_SCROLL_SELL_MULTIPLIER;
					StoreSalesListHelper.AddSellItem(this, types[i], price);
				}
			}
		}

		#endregion
	}	
	
	/// <summary>
	/// Shop info for Elf Rares vendor.
	/// Sells rare decorative items, furniture, and artifacts for elven-themed homes.
	/// </summary>
	public class SBElfRares: SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBElfRares class.
		/// </summary>
		public SBElfRares()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Furniture and decorative items (all very rare with random prices)
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(Futon), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_FUTON);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(ArtifactVase), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_ARTIFACT_VASE);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(ArtifactLargeVase), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_ARTIFACT_LARGE_VASE);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BrokenBookcaseDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_BROKEN_BOOKCASE_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BrokenBedDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_BROKEN_BED_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BrokenArmoireDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_BROKEN_ARMOIRE_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(StandingBrokenChairDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_STANDING_BROKEN_CHAIR_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BrokenVanityDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_BROKEN_VANITY_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BrokenFallenChairDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_BROKEN_FALLEN_CHAIR_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BrokenCoveredChairDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_BROKEN_COVERED_CHAIR_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BrokenChestOfDrawersDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_BROKEN_CHEST_OF_DRAWERS_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(TapestryOfSosaria), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_TAPESTRY_OF_SOSARIA);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(RoseOfTrinsic), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_ROSE_OF_TRINSIC);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(HearthOfHomeFireDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_HEARTH_OF_HOME_FIRE_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(VanityDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_VANITY_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BlueDecorativeRugDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_BLUE_DECORATIVE_RUG_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BlueFancyRugDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_BLUE_FANCY_RUG_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BluePlainRugDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_BLUE_PLAIN_RUG_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BoilingCauldronDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_BOILING_CAULDRON_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(CinnamonFancyRugDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_CINNAMON_FANCY_RUG_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(CurtainsDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_CURTAINS_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(FountainDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_FOUNTAIN_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(GoldenDecorativeRugDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_GOLDEN_DECORATIVE_RUG_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(HangingAxesDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_HANGING_AXES_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(HangingSwordsDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_HANGING_SWORDS_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(HouseLadderDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_HOUSE_LADDER_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(LargeFishingNetDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_LARGE_FISHING_NET_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(PinkFancyRugDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_PINK_FANCY_RUG_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(RedPlainRugDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_RED_PLAIN_RUG_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(ScarecrowDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_SCARECROW_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(SmallFishingNetDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_SMALL_FISHING_NET_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(TableWithBlueClothDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_TABLE_WITH_BLUE_CLOTH_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(TableWithOrangeClothDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_TABLE_WITH_ORANGE_CLOTH_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(TableWithPurpleClothDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_TABLE_WITH_PURPLE_CLOTH_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(TableWithRedClothDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_TABLE_WITH_RED_CLOTH_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(UnmadeBedDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_UNMADE_BED_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(WallBannerDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_WALL_BANNER_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(TreeStumpDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_TREE_STUMP_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(DecorativeShieldDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_DECORATIVE_SHIELD_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(MiningCartDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_MINING_CART_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(PottedCactusDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_POTTED_CACTUS_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(StoneAnkhDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_STONE_ANKH_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BannerDeed), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_BANNER_DEED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(Tub), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_TUB);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(WaterBarrel), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_WATER_BARREL);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(ClosedBarrel), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_CLOSED_BARREL);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(Bucket), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_BUCKET);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(DecoTray), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_DECO_TRAY);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(DecoTray2), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_DECO_TRAY2);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(DecoBottlesOfLiquor), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_DECO_BOTTLES_OF_LIQUOR);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(Checkers), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_CHECKERS);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(Chessmen3), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_CHESSMEN3);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(Chessmen2), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_CHESSMEN2);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(Chessmen), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_CHESSMEN);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(Checkers2), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_CHECKERS2);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(DecoHay2), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_DECO_HAY2);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(DecoBridle2), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_DECO_BRIDLE2);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(DecoBridle), StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_ELF_RARE_ITEM_MAX, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.QTY_ELF_RARE_ITEM, StoreSalesListConstants.ITEMID_DECO_BRIDLE);

				// Treasure pile deeds (decorative items with fixed pricing)
				StoreSalesListHelper.AddBuyItem(this, typeof(TreasurePile02AddonDeed), 240, StoreSalesListConstants.QTY_UNLIMITED, 0x0E41); // Minimal03 - 9 components (smallest) - 200 + 20%
				StoreSalesListHelper.AddBuyItem(this, typeof(TreasurePile01AddonDeed), 270, StoreSalesListConstants.QTY_UNLIMITED, 0x0E41); // Compact01 - 10 components - 225 + 20%
				StoreSalesListHelper.AddBuyItem(this, typeof(TreasurePile03AddonDeed), 300, StoreSalesListConstants.QTY_UNLIMITED, 0x0E41); // Standard05 - 11 components - 250 + 20%
				StoreSalesListHelper.AddBuyItem(this, typeof(TreasurePile04AddonDeed), 360, StoreSalesListConstants.QTY_UNLIMITED, 0x0E41); // Medium06 - 13 components - 300 + 20%
				StoreSalesListHelper.AddBuyItem(this, typeof(TreasurePile05AddonDeed), 420, StoreSalesListConstants.QTY_UNLIMITED, 0x0E41); // Balanced07 - 15 components - 350 + 20%
				StoreSalesListHelper.AddBuyItem(this, typeof(TreasurePileAddonDeed), 750, StoreSalesListConstants.QTY_UNLIMITED, 0x0E41); // Original - 26 components - 625 + 20%
				StoreSalesListHelper.AddBuyItem(this, typeof(TreasurePile2AddonDeed), 780, StoreSalesListConstants.QTY_UNLIMITED, 0x0E41); // Extended02 - 27 components - 650 + 20%
				StoreSalesListHelper.AddBuyItem(this, typeof(TreasurePile3AddonDeed), 960, StoreSalesListConstants.QTY_UNLIMITED, 0x0E41); // Large04 - 33 components (largest) - 800 + 20%
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Elf rare items are not purchased back by vendors
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBChainmailArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBChainmailArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChainCoif ), 17, Utility.Random( 1,100 ), 0x13BB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChainChest ), 143, Utility.Random( 1,100 ), 0x13BF, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( ChainLegs ), 149, Utility.Random( 1,100 ), 0x13BE, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( ChainCoif ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ChainChest ), 71 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ChainLegs ), 74 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ChainSkirt ), 74 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBHelmetArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHelmetArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHelm ), 21, Utility.Random( 1,100 ), 0x1412, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CloseHelm ), 18, Utility.Random( 1,100 ), 0x1408, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CloseHelm ), 18, Utility.Random( 1,100 ), 0x1409, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Helmet ), 31, Utility.Random( 1,100 ), 0x140A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Helmet ), 18, Utility.Random( 1,100 ), 0x140B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NorseHelm ), 18, Utility.Random( 1,100 ), 0x140E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NorseHelm ), 18, Utility.Random( 1,100 ), 0x140F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bascinet ), 18, Utility.Random( 1,100 ), 0x140C, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( PlateHelm ), 21, Utility.Random( 1,100 ), 0x1419, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DreadHelm ), 21, Utility.Random( 1,100 ), 0x2FBB, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bascinet ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CloseHelm ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Helmet ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NorseHelm ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateHelm ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DreadHelm ), 10 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBLeatherArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBLeatherArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherArms ), 80, Utility.Random( 1,100 ), 0x13CD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherChest ), 101, Utility.Random( 1,100 ), 0x13CC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherGloves ), 60, Utility.Random( 1,100 ), 0x13C6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherGorget ), 74, Utility.Random( 1,100 ), 0x13C7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherLegs ), 80, Utility.Random( 1,100 ), 0x13cb, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherCap ), 10, Utility.Random( 1,100 ), 0x1DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FemaleLeatherChest ), 116, Utility.Random( 1,100 ), 0x1C06, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherBustierArms ), 97, Utility.Random( 1,100 ), 0x1C0A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherShorts ), 86, Utility.Random( 1,100 ), 0x1C00, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LeatherSkirt ), 87, Utility.Random( 1,100 ), 0x1C08, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherCloak ), 120, Utility.Random( 1,100 ), 0x1515, 0x83F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherRobe ), 160, Utility.Random( 1,100 ), 0x1F03, 0x83F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PugilistMits ), 18, Utility.Random( 1,100 ), 0x13C6, 0x966 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ThrowingGloves ), 26, Utility.Random( 1,100 ), 0x13C6, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDragonArms ), 43200, 1, 0x13CD, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDragonChest ), 44000, 1, 0x13CC, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDragonGloves ), 42900, 1, 0x13C6, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDragonGorget ), 42700, 1, 0x13C7, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDragonLegs ), 43200, 1, 0x13cb, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDragonHelm ), 42800, 1, 0x1DB9, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinNightmareArms ), 43200, 1, 0x13CD, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinNightmareChest ), 44000, 1, 0x13CC, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinNightmareGloves ), 42900, 1, 0x13C6, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinNightmareGorget ), 42700, 1, 0x13C7, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinNightmareLegs ), 43200, 1, 0x13cb, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinNightmareHelm ), 42800, 1, 0x1DB9, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinSerpentArms ), 43200, 1, 0x13CD, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinSerpentChest ), 44000, 1, 0x13CC, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinSerpentGloves ), 42900, 1, 0x13C6, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinSerpentGorget ), 42700, 1, 0x13C7, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinSerpentLegs ), 43200, 1, 0x13cb, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinSerpentHelm ), 42800, 1, 0x1DB9, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinTrollArms ), 43200, 1, 0x13CD, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinTrollChest ), 44000, 1, 0x13CC, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinTrollGloves ), 42900, 1, 0x13C6, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinTrollGorget ), 42700, 1, 0x13C7, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinTrollLegs ), 43200, 1, 0x13cb, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinTrollHelm ), 42800, 1, 0x1DB9, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinUnicornArms ), 43200, 1, 0x13CD, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinUnicornChest ), 44000, 1, 0x13CC, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinUnicornGloves ), 42900, 1, 0x13C6, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinUnicornGorget ), 42700, 1, 0x13C7, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinUnicornLegs ), 43200, 1, 0x13cb, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinUnicornHelm ), 42800, 1, 0x1DB9, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDemonArms ), 43200, 1, 0x13CD, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDemonChest ), 44000, 1, 0x13CC, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDemonGloves ), 42900, 1, 0x13C6, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDemonGorget ), 42700, 1, 0x13C7, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDemonLegs ), 43200, 1, 0x13cb, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDemonHelm ), 42800, 1, 0x1DB9, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherArms ), 40 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherChest ), 52 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherGloves ), 30 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherGorget ), 37 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherLegs ), 40 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherCap ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FemaleLeatherChest ), 18 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FemaleStuddedChest ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherShorts ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherSkirt ), 11 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherBustierArms ), 11 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherCloak ), 60 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherRobe ), 80 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PugilistMits ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedBustierArms ), 27 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDragonArms ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDragonChest ), 500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDragonGloves ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDragonGorget ), 370 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDragonLegs ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDragonHelm ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinNightmareArms ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinNightmareChest ), 500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinNightmareGloves ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinNightmareGorget ), 370 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinNightmareLegs ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinNightmareHelm ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinSerpentArms ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinSerpentChest ), 500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinSerpentGloves ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinSerpentGorget ), 370 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinSerpentLegs ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinSerpentHelm ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinTrollArms ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinTrollChest ), 500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinTrollGloves ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinTrollGorget ), 370 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinTrollLegs ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinTrollHelm ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinUnicornArms ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinUnicornChest ), 500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinUnicornGloves ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinUnicornGorget ), 370 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinUnicornLegs ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinUnicornHelm ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDemonArms ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDemonChest ), 500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDemonGloves ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDemonGorget ), 370 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDemonLegs ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDemonHelm ), 100 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMetalShields : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMetalShields()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BronzeShield ), 66, Utility.Random( 1,100 ), 0x1B72, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Buckler ), 50, Utility.Random( 1,100 ), 0x1B73, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MetalKiteShield ), 123, Utility.Random( 1,100 ), 0x1B74, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HeaterShield ), 231, Utility.Random( 1,100 ), 0x1B76, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenKiteShield ), 70, Utility.Random( 1,100 ), 0x1B78, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( MetalShield ), 121, Utility.Random( 1,100 ), 0x1B7B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GuardsmanShield ), 231, Utility.Random( 1,100 ), 0x2FCB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ElvenShield ), 231, Utility.Random( 1,100 ), 0x2FCA, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DarkShield ), 231, Utility.Random( 1,100 ), 0x2FC8, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( CrestedShield ), 231, Utility.Random( 1,100 ), 0x2FC9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ChampionShield ), 231, Utility.Random( 1,100 ), 0x2B74, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JeweledShield ), 231, Utility.Random( 1,100 ), 0x2B75, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Buckler ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BronzeShield ), 33 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MetalShield ), 60 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MetalKiteShield ), 62 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HeaterShield ), 115 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenKiteShield ), 35 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GuardsmanShield ), 115 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ElvenShield ), 115 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DarkShield ), 115 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CrestedShield ), 115 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ChampionShield ), 115 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JeweledShield ), 115 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBPlateArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBPlateArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateGorget ), 104, Utility.Random( 1,100 ), 0x1413, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateChest ), 243, Utility.Random( 1,100 ), 0x1415, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateLegs ), 218, Utility.Random( 1,100 ), 0x1411, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateArms ), 188, Utility.Random( 1,100 ), 0x1410, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( PlateGloves ), 155, Utility.Random( 1,100 ), 0x1414, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FemalePlateChest ), 207, Utility.Random( 1,100 ), 0x1C04, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateArms ), 94 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateChest ), 121 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateGloves ), 72 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateGorget ), 52 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateLegs ), 109 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateSkirt ), 109 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FemalePlateChest ), 113 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBLotsOfArrows: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBLotsOfArrows()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( ManyArrows100 ), 3000, Utility.RandomMinMax(10,50), 0xF41, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyBolts100 ), 3000, Utility.RandomMinMax(10,50), 0x1BFD, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyArrows1000 ), 30000, Utility.RandomMinMax(10,50), 0xF41, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyBolts1000 ), 30000, Utility.RandomMinMax(10,50), 0x1BFD, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBRingmailArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRingmailArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RingmailChest ), 121, Utility.Random( 1,100 ), 0x13ec, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RingmailLegs ), 90, Utility.Random( 1,100 ), 0x13F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RingmailArms ), 85, Utility.Random( 1,100 ), 0x13EE, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( RingmailGloves ), 93, Utility.Random( 1,100 ), 0x13eb, 0 ) ); }

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( RingmailArms ), 42 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RingmailChest ), 60 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RingmailGloves ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RingmailLegs ), 45 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RingmailSkirt ), 45 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBStuddedArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBStuddedArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedArms ), 87, Utility.Random( 1,100 ), 0x13DC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedChest ), 128, Utility.Random( 1,100 ), 0x13DB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedGloves ), 79, Utility.Random( 1,100 ), 0x13D5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedGorget ), 73, Utility.Random( 1,100 ), 0x13D6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedLegs ), 103, Utility.Random( 1,100 ), 0x13DA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FemaleStuddedChest ), 142, Utility.Random( 1,100 ), 0x1C02, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( StuddedBustierArms ), 120, Utility.Random( 1,100 ), 0x1c0c, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedArms ), 43 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedChest ), 64 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedGloves ), 39 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedGorget ), 36 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedLegs ), 51 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedSkirt ), 51 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FemaleStuddedChest ), 71 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedBustierArms ), 60 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBWoodenShields: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBWoodenShields()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( WoodenShield ), 150, Utility.Random( 1,10 ), 0x1B7A, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenShield ), 80 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSEArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSEArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHatsuburi ), 76, Utility.Random( 1,100 ), 0x2775, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HeavyPlateJingasa ), 76, Utility.Random( 1,100 ), 0x2777, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DecorativePlateKabuto ), 95, Utility.Random( 1,100 ), 0x2778, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateDo ), 310, Utility.Random( 1,100 ), 0x277D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHiroSode ), 222, Utility.Random( 1,100 ), 0x2780, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateSuneate ), 224, Utility.Random( 1,100 ), 0x2788, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHaidate ), 235, Utility.Random( 1,100 ), 0x278D, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( ChainHatsuburi ), 76, Utility.Random( 1,100 ), 0x2774, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateHatsuburi ), 38 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HeavyPlateJingasa ), 38 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DecorativePlateKabuto ), 47 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateDo ), 155 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateHiroSode ), 111 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateSuneate ), 112 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateHaidate), 117 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ChainHatsuburi ), 38 ); } 

			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSEBowyer: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSEBowyer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Yumi ), 53, Utility.Random( 1,100 ), 0x27A5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Fukiya ), 20, Utility.Random( 1,100 ), 0x27AA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Nunchaku ), 35, Utility.Random( 1,100 ), 0x27AE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FukiyaDarts ), 3, Utility.Random( 1,100 ), 0x2806, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bokuto ), 21, Utility.Random( 1,100 ), 0x27A8, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Yumi ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Fukiya ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Nunchaku ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FukiyaDarts ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bokuto ), 10 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSECarpenter: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSECarpenter()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bokuto ), 21, Utility.Random( 1,100 ), 0x27A8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tetsubo ), 43, Utility.Random( 1,100 ), 0x27A6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Fukiya ), 20, Utility.Random( 1,100 ), 0x27AA, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tetsubo ), 21 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Fukiya ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bokuto ), 10 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSEFood: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSEFood()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Wasabi ), 200, Utility.Random( 1,100 ), 0x24E8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Wasabi ), 200, Utility.Random( 1,100 ), 0x24E9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BentoBox ), 600, Utility.Random( 1,100 ), 0x2836, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BentoBox ), 600, Utility.Random( 1,100 ), 0x2837, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( GreenTeaBasket ), 200, Utility.Random( 1,100 ), 0x284B, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Wasabi ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BentoBox ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreenTeaBasket ), 1 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSELeatherArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSELeatherArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherJingasa ), 11, Utility.Random( 1,100 ), 0x2776, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherDo ), 87, Utility.Random( 1,100 ), 0x277B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherHiroSode ), 49, Utility.Random( 1,100 ), 0x277E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherSuneate ), 55, Utility.Random( 1,100 ), 0x2786, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherHaidate), 54, Utility.Random( 1,100 ), 0x278A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherNinjaPants ), 49, Utility.Random( 1,100 ), 0x2791, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherNinjaJacket ), 51, Utility.Random( 1,100 ), 0x2793, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedMempo ), 61, Utility.Random( 1,100 ), 0x279D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedDo ), 130, Utility.Random( 1,100 ), 0x277C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedHiroSode ), 73, Utility.Random( 1,100 ), 0x277F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedSuneate ), 78, Utility.Random( 1,100 ), 0x2787, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( StuddedHaidate ), 76, Utility.Random( 1,100 ), 0x278B, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherJingasa ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherDo ), 42 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherHiroSode ), 23 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherSuneate ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherHaidate), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherNinjaPants ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherNinjaJacket ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedMempo ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedDo ), 66 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedHiroSode ), 32 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedSuneate ), 40 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedHaidate ), 37 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSEWeapons: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSEWeapons()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NoDachi ), 82, Utility.Random( 1,100 ), 0x27A2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tessen ), 83, Utility.Random( 1,100 ), 0x27A3, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Wakizashi ), 38, Utility.Random( 1,100 ), 0x27A4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tetsubo ), 43, Utility.Random( 1,100 ), 0x27A6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lajatang ), 108, Utility.Random( 1,100 ), 0x27A7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Daisho ), 66, Utility.Random( 1,100 ), 0x27A9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tekagi ), 55, Utility.Random( 1,100 ), 0x27AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Shuriken ), 18, Utility.Random( 1,100 ), 0x27AC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Kama ), 61, Utility.Random( 1,100 ), 0x27AD, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Sai ), 56, Utility.Random( 1,100 ), 0x27AF, 0 ) ); }		
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( NoDachi ), 41 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tessen ), 41 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Wakizashi ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tetsubo ), 21 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lajatang ), 54 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Daisho ), 33 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tekagi ), 22 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Shuriken), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Kama ), 30 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sai ), 28 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBAxeWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBAxeWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ExecutionersAxe ), 30, Utility.Random( 1,100 ), 0xF45, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BattleAxe ), 26, Utility.Random( 1,100 ), 0xF47, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TwoHandedAxe ), 32, Utility.Random( 1,100 ), 0x1443, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Axe ), 40, Utility.Random( 1,100 ), 0xF49, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DoubleAxe ), 52, Utility.Random( 1,100 ), 0xF4B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pickaxe ), 22, Utility.Random( 1,100 ), 0xE86, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LargeBattleAxe ), 33, Utility.Random( 1,100 ), 0x13FB, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( WarAxe ), 29, Utility.Random( 1,100 ), 0x13B0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( OrnateAxe ), 42, Utility.Random( 1,100 ), 0x2D28, 0 ) ); }

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( OrnateAxe ),21 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BattleAxe ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DoubleAxe ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ExecutionersAxe ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LargeBattleAxe ),16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pickaxe ), 11 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TwoHandedAxe ), 16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WarAxe ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Axe ), 20 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBKnifeWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBKnifeWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ButcherKnife ), 14, Utility.Random( 1,100 ), 0x13F6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dagger ), 21, Utility.Random( 1,100 ), 0xF52, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cleaver ), 100, Utility.Random( 1,100 ), 0xEC3, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( SkinningKnife ), 14, Utility.Random( 1,100 ), 0xEC4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AssassinSpike ), 21, Utility.Random( 1,100 ), 0x2D21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Leafblade ), 21, Utility.Random( 1,100 ), 0x2D22, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WarCleaver ), 25, Utility.Random( 1,100 ), 0x2D2F, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( ButcherKnife ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cleaver ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dagger ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinningKnife ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AssassinSpike ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Leafblade ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WarCleaver ), 12 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMaceWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMaceWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DiamondMace ), 31, Utility.Random( 1,100 ), 0x2D24, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HammerPick ), 26, Utility.Random( 1,100 ), 0x143D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Club ), 16, Utility.Random( 1,100 ), 0x13B4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Mace ), 28, Utility.Random( 1,100 ), 0xF5C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Maul ), 21, Utility.Random( 1,100 ), 0x143B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WarHammer ), 25, Utility.Random( 1,100 ), 0x1439, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( WarMace ), 31, Utility.Random( 1,100 ), 0x1407, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Club ), 8 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HammerPick ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Mace ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Maul ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WarHammer ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WarMace ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DiamondMace ), 100 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBPoleArmWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBPoleArmWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bardiche ), 60, Utility.Random( 1,100 ), 0xF4D, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Halberd ), 42, Utility.Random( 1,100 ), 0x143E, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bardiche ), 30 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Halberd ), 21 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBRangedWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRangedWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bow ), 40, Utility.Random( 1,100 ), 0x13B2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Crossbow ), 55, Utility.Random( 1,100 ), 0xF50, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HeavyCrossbow ), 55, Utility.Random( 1,100 ), 0x13FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RepeatingCrossbow ), 46, Utility.Random( 1,100 ), 0x26C3, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CompositeBow ), 45, Utility.Random( 1,100 ), 0x26C2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MagicalShortbow ), 42, Utility.Random( 1,100 ), 0x2D2B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ElvenCompositeLongbow ), 42, Utility.Random( 1,100 ), 0x2D1E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bolt ), 4, Utility.Random( 200, 500 ), 0x1BFB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Arrow ), 4, Utility.Random( 200, 500 ), 0xF3F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Feather ), 2, Utility.Random( 200, 1000 ), 0x4CCD, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Shaft ), 1, Utility.Random( 200, 1000 ), 0x1BD4, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bolt ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Arrow ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Shaft ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Feather ), 1 ); } 			
				if ( MyServerSettings.BuyChance() ){Add( typeof( HeavyCrossbow ), 27 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bow ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Crossbow ), 25 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( CompositeBow ), 23 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RepeatingCrossbow ), 22 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicalShortbow ), 18 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ElvenCompositeLongbow ), 18 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSpearForkWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSpearForkWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pitchfork ), 19, Utility.Random( 1,100 ), 0xE87, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ShortSpear ), 23, Utility.Random( 1,100 ), 0x1403, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Spear ), 31, Utility.Random( 1,100 ), 0xF62, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Spear ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pitchfork ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShortSpear ), 11 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBStavesWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBStavesWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlackStaff ), 22, Utility.Random( 1,100 ), 0xDF1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WildStaff ), 20, Utility.Random( 1,100 ), 0x2D25, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GnarledStaff ), 16, Utility.Random( 1,100 ), 0x13F8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( QuarterStaff ), 19, Utility.Random( 1,100 ), 0xE89, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( ShepherdsCrook ), 20, Utility.Random( 1,100 ), 0xE81, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlackStaff ), 11 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GnarledStaff ), 8 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuarterStaff ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShepherdsCrook ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WildStaff ), 10 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSwordWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSwordWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cutlass ), 24, Utility.Random( 1,100 ), 0x1441, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Katana ), 33, Utility.Random( 1,100 ), 0x13FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Kryss ), 32, Utility.Random( 1,100 ), 0x1401, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Broadsword ), 35, Utility.Random( 1,100 ), 0xF5E, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Longsword ), 55, Utility.Random( 1,100 ), 0xF61, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ThinLongsword ), 27, Utility.Random( 1,100 ), 0x13B8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( VikingSword ), 55, Utility.Random( 1,100 ), 0x13B9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Scimitar ), 36, Utility.Random( 1,100 ), 0x13B6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoneHarvester ), 35, Utility.Random( 1,100 ), 0x26BB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CrescentBlade ), 37, Utility.Random( 1,100 ), 0x26C1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DoubleBladedStaff ), 35, Utility.Random( 1,100 ), 0x26BF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lance ), 34, Utility.Random( 1,100 ), 0x26C0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pike ), 39, Utility.Random( 1,100 ), 0x26BE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Scythe ), 39, Utility.Random( 1,100 ), 0x26BA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RuneBlade ), 55, Utility.Random( 1,100 ), 0x2D32, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RadiantScimitar ), 35, Utility.Random( 1,100 ), 0x2D33, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ElvenSpellblade ), 33, Utility.Random( 1,100 ), 0x2D20, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ElvenMachete ), 35, Utility.Random( 1,100 ), 0x2D35, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Broadsword ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cutlass ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Katana ), 16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Kryss ), 16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Longsword ), 27 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Scimitar ), 18 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ThinLongsword ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( VikingSword ), 27 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Scythe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoneHarvester ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Scepter ), 18 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BladedStaff ), 16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pike ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DoubleBladedStaff ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lance ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CrescentBlade ), 18 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RuneBlade ), 27 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RadiantScimitar ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ElvenSpellblade ), 16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ElvenMachete ), 17 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBElfWizard : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBElfWizard()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{  
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BagOfSending ), 10000, Utility.Random( 1,10 ), 0xE76, 0x8AD ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BallOfSummoning ), 3000, Utility.Random( 1,10 ), 0xE2E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BraceletOfBinding ), 3500, Utility.Random( 1,10 ), 0x4CF1, 0x489 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PowderOfTranslocation ), 1000, Utility.Random( 5,20 ), 0x26B8, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBElfHealer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBElfHealer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{  
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FountainOfLifeDeed ), 100000, 1, 0x14F0, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBUndertaker: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBUndertaker()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( PowerCoil ), Utility.Random( 10000,20000 ), Utility.Random( 1,5 ), 0x8A7, 0 ) );
				Add( new GenericBuyInfo( typeof( EmbalmingFluid ), Utility.Random( 100,200 ), Utility.Random( 15,55 ), 0xE0F, 0xBA1 ) );
            }
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( FrankenArmRight ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenHead ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenLegLeft ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenLegRight ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenTorso ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenArmLeft ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenBrain ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenJournal ), Utility.Random( 300,750 ) );
				Add( typeof( PowerCoil ), Utility.Random( 3500,4500 ) );
				Add( typeof( CorpseSailor ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( CorpseChest ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BuriedBody ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BoneContainer ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( LeftLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( TastyHeart ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( BodyPart ), Utility.RandomMinMax( 30, 90 ) );
				Add( typeof( Head ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( LeftArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Torso ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bone ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RibCage ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( BonePile ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bones ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( GraveChest ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( EmbalmingFluid ), Utility.RandomMinMax( 25, 45 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( DracolichSkull ), Utility.Random( 500,1000 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Alchemist vendor.
	/// Sells alchemy tools, reagents, potions, and recipe scrolls.
	/// </summary>
	public class SBAlchemist : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBAlchemist class.
		/// </summary>
		public SBAlchemist()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{  
				// Alchemy tools
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(MortarPestle), StoreSalesListConstants.PRICE_MORTAR_PESTLE, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x4CE9);
				/*
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(MixingCauldron), StoreSalesListConstants.PRICE_MIXING_CAULDRON, StoreSalesListConstants.QTY_SINGLE, 0x269C);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(MixingSpoon), StoreSalesListConstants.PRICE_MIXING_SPOON, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1E27, StoreSalesListConstants.HUE_MIXING_SPOON);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CBookNecroticAlchemy), StoreSalesListConstants.PRICE_ALCHEMY_BOOK, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2253, StoreSalesListConstants.HUE_NECROTIC_ALCHEMY);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(AlchemicalElixirs), StoreSalesListConstants.PRICE_ALCHEMY_BOOK, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2219);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(AlchemicalMixtures), StoreSalesListConstants.PRICE_ALCHEMY_BOOK, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2223);
				*/
				
				// Containers (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Bottle), StoreSalesListConstants.PRICE_BOTTLE, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.QTY_RANDOM_MAX, 0xF0E);
				//StoreSalesListHelper.AddBuyItemRandom(this, typeof(Jar), StoreSalesListConstants.PRICE_JAR, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x10B4);
				//StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(HeatingStand), StoreSalesListConstants.PRICE_HEATING_STAND, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1849);

				// Alchemy tub
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(AlchemyTub), StoreSalesListConstants.PRICE_ALCHEMY_TUB, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_SMALL_MAX, 0x126A);

				// Alchemy Recipe Book and Book of Poisons
				if (MyServerSettings.SellChance())
				{
					StoreSalesListHelper.AddBuyItem(this, typeof(AlchemyRecipeBook), StoreSalesListConstants.PRICE_ALCHEMY_RECIPE_BOOK, StoreSalesListConstants.QTY_ALCHEMY_RECIPE_BOOK, 0x2253, StoreSalesListConstants.HUE_ALCHEMY_RECIPE);
				}
				// Book of Poisons
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(BookOfPoisons), StoreSalesListConstants.PRICE_ALCHEMY_BOOK, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2253, StoreSalesListConstants.HUE_BOOK_OF_POISONS);

				// Basic reagents
				/*StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(BlackPearl), StoreSalesListConstants.PRICE_REAGENT_BASIC, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x266F);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Bloodmoss), StoreSalesListConstants.PRICE_REAGENT_BASIC, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_REAGENT_BLOODMOSS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Garlic), StoreSalesListConstants.PRICE_REAGENT_COMMON, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_REAGENT_GARLIC);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Ginseng), StoreSalesListConstants.PRICE_REAGENT_COMMON, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_REAGENT_GINSENG);
				*/
				//StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(MandrakeRoot), StoreSalesListConstants.PRICE_REAGENT_COMMON, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_REAGENT_MANDRAKE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Nightshade), StoreSalesListConstants.PRICE_REAGENT_COMMON, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_REAGENT_NIGHTSHADE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SpidersSilk), StoreSalesListConstants.PRICE_REAGENT_COMMON, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_REAGENT_SPIDERS_SILK);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SulfurousAsh), StoreSalesListConstants.PRICE_REAGENT_COMMON, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_REAGENT_SULFUROUS_ASH);

				// Special reagents
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Brimstone), StoreSalesListConstants.PRICE_REAGENT_SPECIAL, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2FD3);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ButterflyWings), StoreSalesListConstants.PRICE_REAGENT_SPECIAL, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x3002);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(EyeOfToad), StoreSalesListConstants.PRICE_REAGENT_SPECIAL, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2FDA);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(FairyEgg), StoreSalesListConstants.PRICE_REAGENT_SPECIAL, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2FDB);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(GargoyleEar), StoreSalesListConstants.PRICE_REAGENT_SPECIAL, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2FD9);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(BeetleShell), StoreSalesListConstants.PRICE_REAGENT_SPECIAL, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2FF8);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(MoonCrystal), StoreSalesListConstants.PRICE_REAGENT_SPECIAL, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x3003);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PixieSkull), StoreSalesListConstants.PRICE_REAGENT_SPECIAL, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2FE1);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RedLotus), StoreSalesListConstants.PRICE_REAGENT_SPECIAL, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2FE8);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SeaSalt), StoreSalesListConstants.PRICE_REAGENT_SPECIAL, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2FE9);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SilverWidow), StoreSalesListConstants.PRICE_REAGENT_SPECIAL, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2FF7);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SwampBerries), StoreSalesListConstants.PRICE_REAGENT_SPECIAL, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2FE0);

				// Special items
				//StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(BottleOfAcid), StoreSalesListConstants.PRICE_BOTTLE_OF_ACID, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x180F, StoreSalesListConstants.HUE_BOTTLE_OF_ACID);

				// Basic potions
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RefreshPotion), StoreSalesListConstants.PRICE_POTION_BASIC, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_POTION_REFRESH);
				//StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(AgilityPotion), StoreSalesListConstants.PRICE_POTION_BASIC, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_POTION_AGILITY);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(NightSightPotion), StoreSalesListConstants.PRICE_POTION_BASIC, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_POTION_NIGHTSIGHT);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LesserHealPotion), StoreSalesListConstants.PRICE_POTION_BASIC, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x25FD);
				//StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(StrengthPotion), StoreSalesListConstants.PRICE_POTION_BASIC, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_POTION_STRENGTH);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LesserPoisonPotion), StoreSalesListConstants.PRICE_POTION_BASIC, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2600);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LesserCurePotion), StoreSalesListConstants.PRICE_POTION_BASIC, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x233B);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LesserExplosionPotion), StoreSalesListConstants.PRICE_POTION_LESSER_EXPLOSION, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2407);

				// Standard potions (rare)
				/*StoreSalesListHelper.AddRareItemRandom(this, typeof(HealPotion), StoreSalesListConstants.PRICE_POTION_STANDARD, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_POTION_LESSER_HEAL);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(PoisonPotion), StoreSalesListConstants.PRICE_POTION_STANDARD, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_POTION_LESSER_POISON);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(CurePotion), StoreSalesListConstants.PRICE_POTION_STANDARD, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_POTION_LESSER_CURE);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(ExplosionPotion), StoreSalesListConstants.PRICE_POTION_EXPLOSION, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_POTION_LESSER_EXPLOSION);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(ConflagrationPotion), StoreSalesListConstants.PRICE_POTION_STANDARD, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x180F, StoreSalesListConstants.HUE_CONFLAGRATION);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(ConfusionBlastPotion), StoreSalesListConstants.PRICE_POTION_STANDARD, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x180F, StoreSalesListConstants.HUE_MANA);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(FrostbitePotion), StoreSalesListConstants.PRICE_POTION_STANDARD, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x180F, StoreSalesListConstants.HUE_FROSTBITE);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(TotalRefreshPotion), StoreSalesListConstants.PRICE_POTION_STANDARD, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x25FF);

				// Greater potions (rare)
				StoreSalesListHelper.AddRareItemRandom(this, typeof(GreaterAgilityPotion), StoreSalesListConstants.PRICE_POTION_GREATER, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x256A);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(GreaterConflagrationPotion), StoreSalesListConstants.PRICE_POTION_GREATER, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2406, StoreSalesListConstants.HUE_CONFLAGRATION);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(GreaterConfusionBlastPotion), StoreSalesListConstants.PRICE_POTION_GREATER, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2406, StoreSalesListConstants.HUE_MANA);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(GreaterCurePotion), StoreSalesListConstants.PRICE_POTION_GREATER, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x24EA);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(GreaterExplosionPotion), StoreSalesListConstants.PRICE_POTION_GREATER, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2408);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(GreaterFrostbitePotion), StoreSalesListConstants.PRICE_POTION_GREATER, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2406, StoreSalesListConstants.HUE_FROSTBITE);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(GreaterHealPotion), StoreSalesListConstants.PRICE_POTION_GREATER, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x25FE);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(GreaterPoisonPotion), StoreSalesListConstants.PRICE_POTION_GREATER, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2601);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(GreaterStrengthPotion), StoreSalesListConstants.PRICE_POTION_GREATER, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x25F7);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(DeadlyPoisonPotion), StoreSalesListConstants.PRICE_POTION_GREATER, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2669);
				*/
				// Special potions (very rare)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LesserInvisibilityPotion), StoreSalesListConstants.PRICE_POTION_LESSER_SPECIAL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_SMALL_MAX, 0x23BD, StoreSalesListConstants.HUE_INVISIBILITY);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LesserManaPotion), StoreSalesListConstants.PRICE_POTION_LESSER_SPECIAL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_SMALL_MAX, 0x23BD, StoreSalesListConstants.HUE_MANA);
				/*
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LesserRejuvenatePotion), StoreSalesListConstants.PRICE_POTION_LESSER_SPECIAL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_SMALL_MAX, 0x23BD, StoreSalesListConstants.HUE_REJUVENATE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(InvisibilityPotion), StoreSalesListConstants.PRICE_POTION_SPECIAL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_SMALL_MAX, 0x180F, StoreSalesListConstants.HUE_INVISIBILITY);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ManaPotion), StoreSalesListConstants.PRICE_POTION_SPECIAL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_SMALL_MAX, 0x180F, StoreSalesListConstants.HUE_MANA);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RejuvenatePotion), StoreSalesListConstants.PRICE_POTION_SPECIAL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_SMALL_MAX, 0x180F, StoreSalesListConstants.HUE_REJUVENATE);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(GreaterInvisibilityPotion), StoreSalesListConstants.PRICE_POTION_GREATER_SPECIAL, StoreSalesListConstants.QTY_SINGLE, 0x2406, StoreSalesListConstants.HUE_INVISIBILITY);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(GreaterManaPotion), StoreSalesListConstants.PRICE_POTION_GREATER_SPECIAL, StoreSalesListConstants.QTY_SINGLE, 0x2406, StoreSalesListConstants.HUE_MANA);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(GreaterRejuvenatePotion), StoreSalesListConstants.PRICE_POTION_GREATER_SPECIAL, StoreSalesListConstants.QTY_SINGLE, 0x2406, StoreSalesListConstants.HUE_REJUVENATE);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(InvulnerabilityPotion), StoreSalesListConstants.PRICE_POTION_INVULNERABILITY, StoreSalesListConstants.QTY_SINGLE, 0x180F, StoreSalesListConstants.HUE_INVULNERABILITY);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(AutoResPotion), StoreSalesListConstants.PRICE_POTION_AUTORES, StoreSalesListConstants.QTY_SINGLE, 0x0E0F, StoreSalesListConstants.HUE_AUTORES);
				*/
				
				// Alchemy recipe scrolls (Category 0 and 3 only) - 4-8 quantity per type
				// Category 0 (Basic) recipes: 500-506, 507, 509, 512, 514-515, 521-522, 524-525
				// Category 3 (Cosmetic) recipes: 533-534
				StoreSalesListHelper.AddBuyItemWithChanceArgs(this, typeof(AlchemyRecipeScroll), 120, StoreSalesListConstants.QTY_RECIPE_SCROLL_MIN, StoreSalesListConstants.QTY_RECIPE_SCROLL_MAX, StoreSalesListConstants.ITEMID_ALCHEMY_RECIPE_SCROLL, StoreSalesListConstants.HUE_ALCHEMY_RECIPE, new object[] { 500 }); // Agility
				StoreSalesListHelper.AddBuyItemWithChanceArgs(this, typeof(AlchemyRecipeScroll), 88, StoreSalesListConstants.QTY_RECIPE_SCROLL_MIN, StoreSalesListConstants.QTY_RECIPE_SCROLL_MAX, StoreSalesListConstants.ITEMID_ALCHEMY_RECIPE_SCROLL, StoreSalesListConstants.HUE_ALCHEMY_RECIPE, new object[] { 501 }); // Nightsight
				StoreSalesListHelper.AddBuyItemWithChanceArgs(this, typeof(AlchemyRecipeScroll), 56, StoreSalesListConstants.QTY_RECIPE_SCROLL_MIN, StoreSalesListConstants.QTY_RECIPE_SCROLL_MAX, StoreSalesListConstants.ITEMID_ALCHEMY_RECIPE_SCROLL, StoreSalesListConstants.HUE_ALCHEMY_RECIPE, new object[] { 502 }); // Lesser Cure
				StoreSalesListHelper.AddBuyItemWithChanceArgs(this, typeof(AlchemyRecipeScroll), 63, StoreSalesListConstants.QTY_RECIPE_SCROLL_MIN, StoreSalesListConstants.QTY_RECIPE_SCROLL_MAX, StoreSalesListConstants.ITEMID_ALCHEMY_RECIPE_SCROLL, StoreSalesListConstants.HUE_ALCHEMY_RECIPE, new object[] { 503 }); // Lesser Heal
				StoreSalesListHelper.AddBuyItemWithChanceArgs(this, typeof(AlchemyRecipeScroll), 110, StoreSalesListConstants.QTY_RECIPE_SCROLL_MIN, StoreSalesListConstants.QTY_RECIPE_SCROLL_MAX, StoreSalesListConstants.ITEMID_ALCHEMY_RECIPE_SCROLL, StoreSalesListConstants.HUE_ALCHEMY_RECIPE, new object[] { 505 }); // Refresh
				StoreSalesListHelper.AddBuyItemWithChanceArgs(this, typeof(AlchemyRecipeScroll), 120, StoreSalesListConstants.QTY_RECIPE_SCROLL_MIN, StoreSalesListConstants.QTY_RECIPE_SCROLL_MAX, StoreSalesListConstants.ITEMID_ALCHEMY_RECIPE_SCROLL, StoreSalesListConstants.HUE_ALCHEMY_RECIPE, new object[] { 506 }); // Strength
				StoreSalesListHelper.AddBuyItemWithChanceArgs(this, typeof(AlchemyRecipeScroll), 49, StoreSalesListConstants.QTY_RECIPE_SCROLL_MIN, StoreSalesListConstants.QTY_RECIPE_SCROLL_MAX, StoreSalesListConstants.ITEMID_ALCHEMY_RECIPE_SCROLL, StoreSalesListConstants.HUE_ALCHEMY_RECIPE, new object[] { 533 }); // Hair Oil
				StoreSalesListHelper.AddBuyItemWithChanceArgs(this, typeof(AlchemyRecipeScroll), 44, StoreSalesListConstants.QTY_RECIPE_SCROLL_MIN, StoreSalesListConstants.QTY_RECIPE_SCROLL_MAX, StoreSalesListConstants.ITEMID_ALCHEMY_RECIPE_SCROLL, StoreSalesListConstants.HUE_ALCHEMY_RECIPE, new object[] { 534 }); // Hair Dye
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				/*
				// Skulls and special items
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(SkullMinotaur), 50, 150);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(SkullWyrm), 200, 400);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(SkullGreatDragon), 300, 600);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(SkullDragon), 100, 300);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(SkullDemon), 100, 300);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(SkullGiant), 100, 300);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(CanopicJar), 50, 300);

				// Alchemy tools
				//StoreSalesListHelper.AddSellItemWithChance(this, typeof(MixingCauldron), StoreSalesListConstants.SELL_PRICE_MIXING_CAULDRON);
				//StoreSalesListHelper.AddSellItemWithChance(this, typeof(MixingSpoon), StoreSalesListConstants.SELL_PRICE_MIXING_SPOON);
				/*
				// Special ingredients
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(DragonTooth), StoreSalesListConstants.SELL_PRICE_SPECIAL_INGREDIENT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(EnchantedSeaweed), StoreSalesListConstants.SELL_PRICE_SPECIAL_INGREDIENT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GhostlyDust), StoreSalesListConstants.SELL_PRICE_SPECIAL_INGREDIENT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GoldenSerpentVenom), StoreSalesListConstants.SELL_PRICE_SPECIAL_INGREDIENT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LichDust), StoreSalesListConstants.SELL_PRICE_SPECIAL_INGREDIENT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SilverSerpentVenom), StoreSalesListConstants.SELL_PRICE_SPECIAL_INGREDIENT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(UnicornHorn), StoreSalesListConstants.SELL_PRICE_SPECIAL_INGREDIENT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(DemigodBlood), StoreSalesListConstants.SELL_PRICE_SPECIAL_INGREDIENT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(DemonClaw), StoreSalesListConstants.SELL_PRICE_SPECIAL_INGREDIENT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(DragonBlood), StoreSalesListConstants.SELL_PRICE_SPECIAL_INGREDIENT);
				
				// Basic reagents
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BlackPearl), StoreSalesListConstants.SELL_PRICE_REAGENT_BASIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Bloodmoss), StoreSalesListConstants.SELL_PRICE_REAGENT_BASIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MandrakeRoot), StoreSalesListConstants.SELL_PRICE_REAGENT_COMMON);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Garlic), StoreSalesListConstants.SELL_PRICE_REAGENT_COMMON);
				*/
				//StoreSalesListHelper.AddSellItemWithChance(this, typeof(Ginseng), StoreSalesListConstants.SELL_PRICE_REAGENT_COMMON);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Nightshade), StoreSalesListConstants.SELL_PRICE_REAGENT_COMMON);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SpidersSilk), StoreSalesListConstants.SELL_PRICE_REAGENT_COMMON);
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(SulfurousAsh), StoreSalesListConstants.SELL_PRICE_SULFUROUS_ASH);

				// Special reagents
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Brimstone), StoreSalesListConstants.SELL_PRICE_REAGENT_SPECIAL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ButterflyWings), StoreSalesListConstants.SELL_PRICE_REAGENT_SPECIAL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(EyeOfToad), StoreSalesListConstants.SELL_PRICE_REAGENT_SPECIAL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FairyEgg), StoreSalesListConstants.SELL_PRICE_REAGENT_SPECIAL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GargoyleEar), StoreSalesListConstants.SELL_PRICE_REAGENT_SPECIAL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BeetleShell), StoreSalesListConstants.SELL_PRICE_REAGENT_SPECIAL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MoonCrystal), StoreSalesListConstants.SELL_PRICE_REAGENT_SPECIAL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PixieSkull), StoreSalesListConstants.SELL_PRICE_REAGENT_SPECIAL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RedLotus), StoreSalesListConstants.SELL_PRICE_REAGENT_SPECIAL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SeaSalt), StoreSalesListConstants.SELL_PRICE_REAGENT_SPECIAL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SilverWidow), StoreSalesListConstants.SELL_PRICE_REAGENT_SPECIAL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SwampBerries), StoreSalesListConstants.SELL_PRICE_REAGENT_SPECIAL);

				// Containers and tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Bottle), StoreSalesListConstants.SELL_PRICE_BOTTLE_JAR);
				//StoreSalesListHelper.AddSellItemWithChance(this, typeof(Jar), StoreSalesListConstants.SELL_PRICE_BOTTLE_JAR);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MortarPestle), StoreSalesListConstants.SELL_PRICE_MORTAR_PESTLE);
				/*
				// Potions
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(AgilityPotion), StoreSalesListConstants.SELL_PRICE_POTION_BASIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(AutoResPotion), StoreSalesListConstants.SELL_PRICE_POTION_AUTORES);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BottleOfAcid), StoreSalesListConstants.SELL_PRICE_BOTTLE_OF_ACID);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ConflagrationPotion), StoreSalesListConstants.SELL_PRICE_POTION_BASIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FrostbitePotion), StoreSalesListConstants.SELL_PRICE_POTION_BASIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ConfusionBlastPotion), StoreSalesListConstants.SELL_PRICE_POTION_BASIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CurePotion), StoreSalesListConstants.SELL_PRICE_POTION_STANDARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(DeadlyPoisonPotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ExplosionPotion), StoreSalesListConstants.SELL_PRICE_POTION_STANDARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GreaterAgilityPotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GreaterConflagrationPotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GreaterFrostbitePotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GreaterConfusionBlastPotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GreaterCurePotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GreaterExplosionPotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GreaterHealPotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GreaterInvisibilityPotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GreaterManaPotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GreaterPoisonPotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GreaterRejuvenatePotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GreaterStrengthPotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(HealPotion), StoreSalesListConstants.SELL_PRICE_POTION_STANDARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(InvisibilityPotion), StoreSalesListConstants.SELL_PRICE_POTION_STANDARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(InvulnerabilityPotion), StoreSalesListConstants.SELL_PRICE_POTION_INVULNERABILITY);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(PotionOfWisdom), 250, 500);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(PotionOfDexterity), 250, 500);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(PotionOfMight), 250, 500);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LesserCurePotion), StoreSalesListConstants.SELL_PRICE_POTION_BASIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LesserExplosionPotion), StoreSalesListConstants.SELL_PRICE_POTION_BASIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LesserHealPotion), StoreSalesListConstants.SELL_PRICE_POTION_BASIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LesserInvisibilityPotion), StoreSalesListConstants.SELL_PRICE_POTION_BASIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LesserManaPotion), StoreSalesListConstants.SELL_PRICE_POTION_BASIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LesserPoisonPotion), StoreSalesListConstants.SELL_PRICE_POTION_BASIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LesserRejuvenatePotion), StoreSalesListConstants.SELL_PRICE_POTION_BASIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ManaPotion), StoreSalesListConstants.SELL_PRICE_POTION_STANDARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(NightSightPotion), StoreSalesListConstants.SELL_PRICE_POTION_STANDARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PoisonPotion), StoreSalesListConstants.SELL_PRICE_POTION_STANDARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RefreshPotion), StoreSalesListConstants.SELL_PRICE_POTION_STANDARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RejuvenatePotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(StrengthPotion), StoreSalesListConstants.SELL_PRICE_POTION_STANDARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(TotalRefreshPotion), StoreSalesListConstants.SELL_PRICE_POTION_GREATER);
				*/
				// Always available items
				StoreSalesListHelper.AddSellItemRandom(this, typeof(BottleOfParts), 10, 30);
				StoreSalesListHelper.AddSellItemRandom(this, typeof(SpecialSeaweed), 15, 35);
				StoreSalesListHelper.AddSellItemRandom(this, typeof(AlchemyTub), 200, 500);
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Mixologist vendor.
	/// Buys various elixirs and mixtures for skill training and effects.
	/// </summary>
	public class SBMixologist : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBMixologist class.
		/// </summary>
		public SBMixologist()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Mixologist only buys items, does not sell them
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Skill elixirs (random prices)
				Add(typeof(ElixirAlchemy), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirAnatomy), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirAnimalLore), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirAnimalTaming), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirArchery), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirArmsLore), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirBegging), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirBlacksmith), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirCamping), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirCarpentry), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirCartography), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirCooking), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirDetectHidden), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirDiscordance), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirEvalInt), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirFencing), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirFishing), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirFletching), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirFocus), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirForensics), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirHealing), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirHerding), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirHiding), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirInscribe), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirItemID), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirLockpicking), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirLumberjacking), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirMacing), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirMagicResist), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirMeditation), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirMining), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirMusicianship), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirParry), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirPeacemaking), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirPoisoning), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirProvocation), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirRemoveTrap), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirSnooping), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirSpiritSpeak), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirStealing), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirStealth), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirSwords), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirTactics), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirTailoring), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirTasteID), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirTinkering), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirTracking), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirVeterinary), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(ElixirWrestling), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));

				// Mixtures (random prices)
				Add(typeof(MixtureSlime), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(MixtureIceSlime), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(MixtureFireSlime), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(MixtureDiseasedSlime), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(MixtureRadiatedSlime), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));

				// Liquids (random prices)
				Add(typeof(LiquidFire), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(LiquidGoo), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(LiquidIce), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(LiquidRot), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
				Add(typeof(LiquidPain), Utility.Random(StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MIN, StoreSalesListConstants.SELL_PRICE_ELIXIR_MIXTURE_MAX));
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Animal Trainer vendor.
	/// Sells various animals for taming and pet training supplies.
	/// </summary>
	public class SBAnimalTrainer : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBAnimalTrainer class.
		/// </summary>
		public SBAnimalTrainer()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Animals (always available)
				// Horse (random 3 types - Horse class randomizes appearance)
				Add(new AnimalBuyInfo(1, "Equinos", typeof(Horse), 550, StoreSalesListConstants.QTY_ANIMALS, 204, StoreSalesListConstants.HUE_DEFAULT));
				Add(new AnimalBuyInfo(1, typeof(PackHorse), StoreSalesListConstants.PRICE_PACK_HORSE, StoreSalesListConstants.QTY_ANIMALS, StoreSalesListConstants.ITEMID_PACK_HORSE, StoreSalesListConstants.HUE_DEFAULT));
				Add(new AnimalBuyInfo(1, typeof(Rat), 30, StoreSalesListConstants.QTY_ANIMALS, StoreSalesListConstants.ITEMID_RAT, StoreSalesListConstants.HUE_DEFAULT));
				Add(new AnimalBuyInfo(1, typeof(Dog), 55, StoreSalesListConstants.QTY_ANIMALS, StoreSalesListConstants.ITEMID_DOG, StoreSalesListConstants.HUE_DEFAULT));
				Add(new AnimalBuyInfo(1, typeof(Cat), 47, StoreSalesListConstants.QTY_ANIMALS, StoreSalesListConstants.ITEMID_CAT, StoreSalesListConstants.HUE_DEFAULT));
				Add(new AnimalBuyInfo(1, typeof(Rabbit), 38, StoreSalesListConstants.QTY_ANIMALS, StoreSalesListConstants.ITEMID_RABBIT, StoreSalesListConstants.HUE_DEFAULT));
				
				// Equipment and supplies
				Add(new GenericBuyInfo(typeof(HitchingPost), 9000, Utility.Random(2, 6), StoreSalesListConstants.ITEMID_HITCHING_POST, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(TamingBODBook), 600, Utility.Random(3, 7), StoreSalesListConstants.ITEMID_TAMING_BOD_BOOK, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(PetTrainer), StoreSalesListConstants.PRICE_PET_TRAINER, Utility.Random(3, 7), StoreSalesListConstants.ITEMID_PET_TRAINER, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(PetBondDeed), 1800, 8, 0x14F0, 1759));
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Eggs (random prices, chance-based)
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(AlienEgg), Utility.Random(StoreSalesListConstants.SELL_PRICE_ALIEN_EGG_MIN, StoreSalesListConstants.SELL_PRICE_ALIEN_EGG_MAX));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(DragonEgg), Utility.Random(StoreSalesListConstants.SELL_PRICE_DRAGON_EGG_MIN, StoreSalesListConstants.SELL_PRICE_DRAGON_EGG_MAX));
				}
			}
		}

		#endregion
	}
	public class SBHumanAnimalTrainer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHumanAnimalTrainer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new AnimalBuyInfo( 1, typeof( PackMule ), 5000, Utility.Random( 1, 4 ), 291, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( PackLlama ), 700, 5, 292, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	public class SBGargoyleAnimalTrainer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGargoyleAnimalTrainer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new AnimalBuyInfo( 1, typeof( PackTurtle ), 10000, Utility.Random( 1, 4 ), 134, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	public class SBElfAnimalTrainer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBElfAnimalTrainer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				// Empty - no items for SBElfAnimalTrainer
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	public class SBBarbarianAnimalTrainer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBarbarianAnimalTrainer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new AnimalBuyInfo( 1, typeof( PackBear ), 10000, Utility.Random( 1, 3 ), 213, 0x908 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	public class SBOrkAnimalTrainer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBOrkAnimalTrainer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new AnimalBuyInfo( 1, typeof( PackStegosaurus ), 15500, 1, 134, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBArchitect : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBArchitect()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( InteriorDecorator ), 1000, Utility.Random( 1,100 ), 0x1EBA, 0 ) );
				Add( new GenericBuyInfo( typeof( HousePlacementTool ), 500, Utility.Random( 1,100 ), 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "house teleporter", typeof( PlayersHouseTeleporter ), 25000, Utility.Random( 1,10 ), 0x181D, 0 ) );
				Add( new GenericBuyInfo( "house high teleporter", typeof( PlayersZTeleporter ), 15000, Utility.Random( 1,10 ), 0x181D, 0 ) );
				if ( Server.Items.MovingBox.IsEnabled() ){ Add( new GenericBuyInfo( typeof( MovingBox ), 1500, Utility.Random( 1,100 ), 0xE3D, 0xAC0 ) ); }
				//if ( Server.Items.BasementDoor.IsEnabled() ){ Add( new GenericBuyInfo( typeof( BasementDoor ), 2500, Utility.Random( 1,100 ), 0x02C1, 0 ) ); }
				Add( new GenericBuyInfo( typeof( house_sign_sign_post_a ), 500, Utility.Random( 1,100 ), 2967, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_post_b ), 500, Utility.Random( 1,100 ), 2970, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_merc ), 1000, Utility.Random( 1,100 ), 3082, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_armor ), 1000, Utility.Random( 1,100 ), 3008, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bake ), 1000, Utility.Random( 1,100 ), 2980, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bank ), 1000, Utility.Random( 1,100 ), 3084, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bard ), 1000, Utility.Random( 1,100 ), 3004, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_smith ), 1000, Utility.Random( 1,100 ), 3016, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bow ), 1000, Utility.Random( 1,100 ), 3022, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_ship ), 1000, Utility.Random( 1,100 ), 2998, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_fletch ), 1000, Utility.Random( 1,100 ), 3006, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_heal ), 1000, Utility.Random( 1,100 ), 2988, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_inn ), 1000, Utility.Random( 1,100 ), 2996, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_gem ), 1000, Utility.Random( 1,100 ), 3010, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_book ), 1000, Utility.Random( 1,100 ), 2966, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_mage ), 1000, Utility.Random( 1,100 ), 2990, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_necro ), 1000, Utility.Random( 1,100 ), 2811, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_supply ), 1000, Utility.Random( 1,100 ), 3020, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_herb ), 1000, Utility.Random( 1,100 ), 3014, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_pen ), 1000, Utility.Random( 1,100 ), 3000, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_sew ), 1000, Utility.Random( 1,100 ), 2982, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_tavern ), 1000, Utility.Random( 1,100 ), 3012, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_tinker ), 1000, Utility.Random( 1,100 ), 2984, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_wood ), 1000, Utility.Random( 1,100 ), 2992, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StoneWellDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RedWellDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MarbleWellDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BrownWellDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlackWellDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodWellDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MedCaseSouthAddonDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MedCaseEastAddonDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmallCaseAddonDeed ), 22500, 1, 0xF3A, 0xB97 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( InteriorDecorator ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HousePlacementTool ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlayersHouseTeleporter ), 2500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlayersZTeleporter ), 1000 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_post_a ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_post_b ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_merc ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_armor ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_bake ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_bank ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_bard ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_smith ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_bow ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_ship ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_fletch ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_heal ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_inn ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_gem ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_book ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_mage ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_necro ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_supply ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_herb ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_pen ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_sew ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_tavern ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_tinker ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_wood ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StoneWellDeed ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RedWellDeed ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarbleWellDeed ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BrownWellDeed ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlackWellDeed ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodWellDeed ), 250 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSailor : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSailor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Harpoon ), 40, Utility.Random( 3,31 ), 0xF63, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( HarpoonRope ), 2, Utility.Random( 50,250 ), 0x52B1, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( FishingPole ), 100, Utility.Random( 3,31 ), 0xDC0, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( SwordsAndShackles ), 50, Utility.Random( 1,100 ), 0x529D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoatStain ), 26, Utility.Random( 1,100 ), 0x14E0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Sextant ), 13, Utility.Random( 1,100 ), 0x1057, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GrapplingHook ), 58, Utility.Random( 1,100 ), 0x4F40, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DockingLantern ), 580, Utility.Random( 3,31 ), 0x40FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041205", typeof( SmallBoatDeed ), 19500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Harpoon ), 20 ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HarpoonRope ), 1 ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SeaShell ), 58 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DockingLantern ), 29 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RawFishSteak ), 3 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Fish ), 5 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FishingPole ), 7 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Sextant ), 6 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GrapplingHook ), 29 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PirateChest ), Utility.RandomMinMax( 200, 800 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SunkenChest ), Utility.RandomMinMax( 200, 800 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FishingNet ), Utility.RandomMinMax( 20, 40 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SpecialFishingNet ), Utility.RandomMinMax( 60, 80 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FabledFishingNet ), Utility.RandomMinMax( 100, 120 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( NeptunesFishingNet ), Utility.RandomMinMax( 140, 160 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PrizedFish ), Utility.RandomMinMax( 60, 120 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WondrousFish ), Utility.RandomMinMax( 60, 120 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TrulyRareFish ), Utility.RandomMinMax( 60, 120 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PeculiarFish ), Utility.RandomMinMax( 60, 120 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SpecialSeaweed ), Utility.RandomMinMax( 40, 160 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SunkenBag ), Utility.RandomMinMax( 100, 500 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShipwreckedItem ), Utility.RandomMinMax( 20, 60 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HighSeasRelic ), Utility.RandomMinMax( 20, 60 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BoatStain ), 13 );}
				if ( 1 > 0 ){Add( typeof( SwordsAndShackles ), 25 ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MegalodonTooth ), Utility.RandomMinMax( 500, 2000 ) );}
			} 
		} 
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBKungFu: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBKungFu()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BookOfBushido), 750, 20, 0x238C, 0 ) );
				Add( new GenericBuyInfo( typeof( BookOfNinjitsu ), 750, 20, 0x23A0, 0 ) );
				Add( new GenericBuyInfo( typeof( MysticSpellbook ), 750, 20, 0x1A97, 0xB61 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHatsuburi ), 76, 20, 0x2775, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HeavyPlateJingasa ), 76, 20, 0x2777, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DecorativePlateKabuto ), 95, 20, 0x2778, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateDo ), 310, 20, 0x277D, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHiroSode ), 222, 20, 0x2780, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateSuneate ), 224, 20, 0x2788, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHaidate ), 235, 20, 0x278D, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChainHatsuburi ), 76, 20, 0x2774, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Yumi ), 53, 20, 0x27A5, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Fukiya ), 20, 20, 0x27AA, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Nunchaku ), 35, 20, 0x27AE, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FukiyaDarts ), 3, 20, 0x2806, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bokuto ), 21, 20, 0x27A8, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bokuto ), 21, 20, 0x27A8, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tetsubo ), 43, 20, 0x27A6, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BambooFlute ), 21, 20, 0x2805, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BambooFlute ), 21, 20, 0x2805, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherJingasa ), 11, 20, 0x2776, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherDo ), 87, 20, 0x277B, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherHiroSode ), 49, 20, 0x277E, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherSuneate ), 55, 20, 0x2786, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherHaidate), 54, 20, 0x278A, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherNinjaPants ), 49, 20, 0x2791, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherNinjaJacket ), 51, 20, 0x2793, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedMempo ), 61, 20, 0x279D, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedDo ), 130, 20, 0x277C, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedHiroSode ), 73, 20, 0x277F, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedSuneate ), 78, 20, 0x2787, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedHaidate ), 76, 20, 0x278B, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NoDachi ), 82, 20, 0x27A2, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tessen ), 83, 20, 0x27A3, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Wakizashi ), 38, 20, 0x27A4, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lajatang ), 108, 20, 0x27A7, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Daisho ), 66, 20, 0x27A9, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tekagi ), 55, 20, 0x27AB, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Shuriken ), 18, 20, 0x27AC, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Kama ), 61, 20, 0x27AD, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Sai ), 56, 20, 0x27AF, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Kasa ), 31, 20, 0x2798, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ThrowingWeapon ), 2, Utility.Random( 20, 120 ), 0x52B2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherJingasa ), 11, 20, 0x2776, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ClothNinjaHood ), 33, 20, 0x278F, 0 ) );}
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BookOfBushido ), 70 );
				Add( typeof( BookOfNinjitsu ), 70 );
				Add( typeof( MysticSpellbook ), 70 );
				if ( MyServerSettings.BuyChance() ){Add( typeof( MySamuraibook ), Utility.Random( 50, 200 ) );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( MyNinjabook ), Utility.Random( 50, 200 ) );}
				Add( typeof( PlateHatsuburi ), 38 );
				Add( typeof( HeavyPlateJingasa ), 38 );
				Add( typeof( DecorativePlateKabuto ), 47 );
				Add( typeof( PlateDo ), 155 );
				Add( typeof( PlateHiroSode ), 111 );
				Add( typeof( PlateSuneate ), 112 );
				Add( typeof( PlateHaidate), 117 );
				Add( typeof( ChainHatsuburi ), 38 );
				Add( typeof( Yumi ), 26 );
				Add( typeof( Fukiya ), 10 );
				Add( typeof( Nunchaku ), 17 );
				Add( typeof( FukiyaDarts ), 1 );
				Add( typeof( Bokuto ), 10 );
				Add( typeof( Tetsubo ), 21 );
				Add( typeof( Fukiya ), 10 );
				Add( typeof( BambooFlute ), 10 );
				Add( typeof( Bokuto ), 10 );
				Add( typeof( LeatherJingasa ), 5 );
				Add( typeof( LeatherDo ), 42 );
				Add( typeof( LeatherHiroSode ), 23 );
				Add( typeof( LeatherSuneate ), 26 );
				Add( typeof( LeatherHaidate), 28 );
				Add( typeof( LeatherNinjaPants ), 25 );
				Add( typeof( LeatherNinjaJacket ), 26 );
				Add( typeof( StuddedMempo ), 28 );
				Add( typeof( StuddedDo ), 66 );
				Add( typeof( StuddedHiroSode ), 32 );
				Add( typeof( StuddedSuneate ), 40 );
				Add( typeof( StuddedHaidate ), 37 );
				Add( typeof( NoDachi ), 41 );
				Add( typeof( Tessen ), 41 );
				Add( typeof( Wakizashi ), 19 );
				Add( typeof( Tetsubo ), 21 );
				Add( typeof( Lajatang ), 54 );
				Add( typeof( Daisho ), 33 );
				Add( typeof( Tekagi ), 22 );
				Add( typeof( Shuriken), 9 );
				Add( typeof( Kama ), 30 );
				Add( typeof( Sai ), 28 );
				Add( typeof( Kasa ), 100 );
				Add( typeof( ThrowingWeapon ), 1 );
				Add( typeof( LeatherJingasa ), 5 );
				Add( typeof( ClothNinjaHood ), 16 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Baker vendor.
	/// Sells baked goods, bread, pastries, and baking ingredients.
	/// </summary>
	public class SBBaker : SBInfo 
	{ 
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBBaker class.
		/// </summary>
		public SBBaker() 
		{ 
		} 

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo() 
			{ 
				// Bread (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(BreadLoaf), 60, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_BREAD_PIE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(BreadLoaf), 50, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x103C);

				// Baked goods
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ApplePie), 170, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1041);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Cake), 1530, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9E9);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Muffins), 800, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9EA);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Cookies), 930, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x160b);

				// Ingredients (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(SackFlour), 50, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1039);

				// Other bread types
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(FrenchBread), 50, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x98C);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(CheesePizza), 80, StoreSalesListConstants.QTY_FIXED_10, 0x1040);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(BowlFlour), 70, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xA1E);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo 
		{ 
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo() 
			{ 
				// Baked goods
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BreadLoaf), 15);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FrenchBread), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Cake), 700);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Cookies), 350);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Muffins), 300);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CheesePizza), 20);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ApplePie), 25);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PeachCobbler), 25);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Quiche), 30);

				// Ingredients
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Dough), 20);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Pitcher), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SackFlour), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Eggs), 5);
			}
		}

		#endregion
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Banker vendor.
	/// Sells banking services, contracts, safes, and related items.
	/// </summary>
	public class SBBanker : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBBanker class.
		/// </summary>
		public SBBanker()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Contracts
				//StoreSalesListHelper.AddBuyItemWithNameRandom(this, "1041243", typeof(ContractOfEmployment), 1252, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_DEED);
				//StoreSalesListHelper.AddBuyItemWithNameRandom(this, "1062332", typeof(VendorRentalContract), 1252, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_DEED, 0x672);
				//StoreSalesListHelper.AddBuyItemWithNameRandom(this, "1047016", typeof(CommodityDeed), 5, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_DEED, 0x47);

				// Interest bags
				//StoreSalesListHelper.AddBuyItemWithName(this, "Interest Bag (Good)", typeof(InterestBag), 1500, StoreSalesListConstants.QTY_SINGLE, 0xE76, 0xABE);
				//StoreSalesListHelper.AddBuyItemWithName(this, "Interest Bag (Evil)", typeof(EvilInterestBag), 1500, StoreSalesListConstants.QTY_SINGLE, 0xE76, 0xABE);

				// Safes and vaults
				//StoreSalesListHelper.AddBuyItemRandom(this, typeof(MetalVault), 15000, StoreSalesListConstants.QTY_VERY_RARE_MIN, StoreSalesListConstants.QTY_VERY_RARE_MAX, 0x4FE3);
				//StoreSalesListHelper.AddBuyItemRandom(this, typeof(MetalSafe), 15000, StoreSalesListConstants.QTY_VERY_RARE_MIN, StoreSalesListConstants.QTY_VERY_RARE_MAX, 0x436);
				//StoreSalesListHelper.AddBuyItemRandom(this, typeof(IronSafe), 15000, StoreSalesListConstants.QTY_VERY_RARE_MIN, StoreSalesListConstants.QTY_VERY_RARE_MAX, 0x5329);
				//StoreSalesListHelper.AddBuyItemRandom(this, typeof(Safe), 500000, StoreSalesListConstants.QTY_VERY_RARE_MIN, StoreSalesListConstants.QTY_VERY_RARE_MAX, 0x436);

				// Currency deeds
				//StoreSalesListHelper.AddBuyItemRandom(this, typeof(DDCopper), 1, 70000, 200000, 0xEF0);
				//StoreSalesListHelper.AddBuyItemRandom(this, typeof(DDSilver), 2, 70000, 200000, 0xEF0);
				//StoreSalesListHelper.AddBuyItemRandom(this, typeof(SilverDeed), 2000, 200, 500, StoreSalesListConstants.ITEMID_DEED);

				// Treasure pile deeds (decorative items with fixed pricing)
				StoreSalesListHelper.AddBuyItem(this, typeof(TreasurePile02AddonDeed), StoreSalesListConstants.PRICE_TREASURE_PILE_02_ADDON_DEED, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_TREASURE_PILE_ADDON_DEED); // Minimal03 - 9 components (smallest) - 200 + 20%
				
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Treasure pile deeds
				StoreSalesListHelper.AddSellItem(this, typeof(TreasurePile02AddonDeed), StoreSalesListConstants.SELL_PRICE_TREASURE_PILE_02_ADDON_DEED); // Minimal03 - 9 components (smallest)
				StoreSalesListHelper.AddSellItem(this, typeof(TreasurePile01AddonDeed), StoreSalesListConstants.SELL_PRICE_TREASURE_PILE_01_ADDON_DEED); // Compact01 - 10 components
				StoreSalesListHelper.AddSellItem(this, typeof(TreasurePile03AddonDeed), StoreSalesListConstants.SELL_PRICE_TREASURE_PILE_03_ADDON_DEED); // Standard05 - 11 components
				StoreSalesListHelper.AddSellItem(this, typeof(TreasurePile04AddonDeed), StoreSalesListConstants.SELL_PRICE_TREASURE_PILE_04_ADDON_DEED); // Medium06 - 13 components
				StoreSalesListHelper.AddSellItem(this, typeof(TreasurePile05AddonDeed), StoreSalesListConstants.SELL_PRICE_TREASURE_PILE_05_ADDON_DEED); // Balanced07 - 15 components
				StoreSalesListHelper.AddSellItem(this, typeof(TreasurePileAddonDeed), StoreSalesListConstants.SELL_PRICE_TREASURE_PILE_ADDON_DEED); // Original - 26 components
				StoreSalesListHelper.AddSellItem(this, typeof(TreasurePile2AddonDeed), StoreSalesListConstants.SELL_PRICE_TREASURE_PILE_2_ADDON_DEED); // Extended02 - 27 components
				StoreSalesListHelper.AddSellItem(this, typeof(TreasurePile3AddonDeed), StoreSalesListConstants.SELL_PRICE_TREASURE_PILE_3_ADDON_DEED); // Large04 - 33 components (largest)

				// Safes and vaults
				//StoreSalesListHelper.AddSellItemRandom(this, typeof(MetalVault), 1000, 2000);
				//StoreSalesListHelper.AddSellItemRandom(this, typeof(MetalSafe), 1000, 2000);
				//StoreSalesListHelper.AddSellItemRandom(this, typeof(IronSafe), 1000, 2000);
				//StoreSalesListHelper.AddSellItemRandom(this, typeof(Safe), 100000, 200000);
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Bard vendor.
	/// Sells musical instruments, song books, and bard scrolls.
	/// </summary>
	public class SBBard : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBBard class.
		/// </summary>
		public SBBard() 
		{ 
		} 

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo() 
			{
				// Musical instruments
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Drums), 210, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x0E9C);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Tambourine), 210, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x0E9E);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LapHarp), 210, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x0EB2);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Lute), 210, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x0EB3);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(BambooFlute), 210, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2805);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SongBook), 240, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_SMALL_MAX, 0x225A);

				// Rare bard scrolls
				StoreSalesListHelper.AddRareItem(this, typeof(EnergyCarolScroll), 320, StoreSalesListConstants.QTY_SINGLE, 0x1F48, 0x96);
				StoreSalesListHelper.AddRareItem(this, typeof(FireCarolScroll), 320, StoreSalesListConstants.QTY_SINGLE, 0x1F49, 0x96);
				StoreSalesListHelper.AddRareItem(this, typeof(IceCarolScroll), 320, StoreSalesListConstants.QTY_SINGLE, 0x1F34, 0x96);
				StoreSalesListHelper.AddRareItem(this, typeof(KnightsMinneScroll), 320, StoreSalesListConstants.QTY_SINGLE, 0x1F31, 0x96);
				StoreSalesListHelper.AddRareItem(this, typeof(PoisonCarolScroll), 320, StoreSalesListConstants.QTY_SINGLE, 0x1F33, 0x96);
				StoreSalesListHelper.AddRareItemRandom(this, typeof(JarsOfWaxInstrument), 1600, StoreSalesListConstants.QTY_VERY_RARE_MIN, StoreSalesListConstants.QTY_VERY_RARE_MAX, 0x1005, 0x845);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo 
		{ 
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo() 
			{
				// Musical instruments
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(JarsOfWaxInstrument), 80);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LapHarp), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Lute), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Drums), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Harp), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Tambourine), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BambooFlute), 10);

				// Song books
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(MySongbook), 50, 200);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SongBook), 12);

				// Bard scrolls
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(EnergyCarolScroll), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FireCarolScroll), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(IceCarolScroll), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(KnightsMinneScroll), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PoisonCarolScroll), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ArmysPaeonScroll), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MagesBalladScroll), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(EnchantingEtudeScroll), 7);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SheepfoeMamboScroll), 7);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SinewyEtudeScroll), 7);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(EnergyThrenodyScroll), 8);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FireThrenodyScroll), 8);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(IceThrenodyScroll), 8);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PoisonThrenodyScroll), 8);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FoeRequiemScroll), 9);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MagicFinaleScroll), 10);
			}
		}

		#endregion
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Barkeeper vendor.
	/// Sells beverages, food, games, and contracts.
	/// </summary>
	public class SBBarkeeper : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBBarkeeper class.
		/// </summary>
		public SBBarkeeper()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Beverages (BeverageBuyInfo - must use direct calls)
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Ale, StoreSalesListConstants.PRICE_BEVERAGE_BOTTLE, Utility.Random(StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX), StoreSalesListConstants.ITEMID_BEVERAGE_BOTTLE_ALE, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Wine, StoreSalesListConstants.PRICE_BEVERAGE_BOTTLE, Utility.Random(StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX), StoreSalesListConstants.ITEMID_BEVERAGE_BOTTLE_WINE, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Liquor, StoreSalesListConstants.PRICE_BEVERAGE_BOTTLE, Utility.Random(StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX), StoreSalesListConstants.ITEMID_BEVERAGE_BOTTLE_LIQUOR, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(Jug), BeverageType.Cider, StoreSalesListConstants.PRICE_JUG_CIDER, Utility.Random(StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX), StoreSalesListConstants.ITEMID_JUG_CIDER, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Milk, StoreSalesListConstants.PRICE_PITCHER_MILK, Utility.Random(StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX), StoreSalesListConstants.ITEMID_PITCHER_MILK, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Ale, StoreSalesListConstants.PRICE_PITCHER_BEVERAGE, Utility.Random(StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX), StoreSalesListConstants.ITEMID_PITCHER_ALE, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Cider, StoreSalesListConstants.PRICE_PITCHER_BEVERAGE, Utility.Random(StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX), StoreSalesListConstants.ITEMID_PITCHER_CIDER, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Liquor, StoreSalesListConstants.PRICE_PITCHER_BEVERAGE, Utility.Random(StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX), StoreSalesListConstants.ITEMID_PITCHER_LIQUOR, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Wine, StoreSalesListConstants.PRICE_PITCHER_BEVERAGE, Utility.Random(StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX), StoreSalesListConstants.ITEMID_PITCHER_WINE, StoreSalesListConstants.HUE_DEFAULT));
				}
				// Water (always available)
				Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Water, StoreSalesListConstants.PRICE_PITCHER_WATER, Utility.Random(StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX), StoreSalesListConstants.ITEMID_PITCHER_WATER, StoreSalesListConstants.HUE_DEFAULT));

				// Food (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(BreadLoaf), StoreSalesListConstants.PRICE_BREAD_LOAF_WAITER, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_BREAD_PIE);

				// Food (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CheeseWheel), StoreSalesListConstants.PRICE_CHEESE_WHEEL, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_CHEESE_WHEEL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CookedBird), StoreSalesListConstants.PRICE_COOKED_BIRD, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_COOKED_BIRD);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LambLeg), StoreSalesListConstants.PRICE_LAMB_LEG, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_LAMB_LEG);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfCarrots), StoreSalesListConstants.PRICE_WOODEN_BOWL_CARROTS, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_WOODEN_BOWL_CARROTS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfCorn), StoreSalesListConstants.PRICE_WOODEN_BOWL_CORN, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_WOODEN_BOWL_CORN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfLettuce), StoreSalesListConstants.PRICE_WOODEN_BOWL_LETTUCE, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_WOODEN_BOWL_LETTUCE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfPeas), StoreSalesListConstants.PRICE_WOODEN_BOWL_PEAS, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_WOODEN_BOWL_PEAS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(EmptyPewterBowl), StoreSalesListConstants.PRICE_EMPTY_PEWTER_BOWL, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_EMPTY_PEWTER_BOWL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfCorn), StoreSalesListConstants.PRICE_PEWTER_BOWL_CORN, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_PEWTER_BOWL_CORN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfLettuce), StoreSalesListConstants.PRICE_PEWTER_BOWL_LETTUCE, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_PEWTER_BOWL_LETTUCE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfPeas), StoreSalesListConstants.PRICE_PEWTER_BOWL_PEAS, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_PEWTER_BOWL_PEAS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfFoodPotatos), StoreSalesListConstants.PRICE_PEWTER_BOWL_POTATOS, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_PEWTER_BOWL_POTATOS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfStew), StoreSalesListConstants.PRICE_WOODEN_BOWL_STEW, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_WOODEN_BOWL_STEW);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfTomatoSoup), StoreSalesListConstants.PRICE_WOODEN_BOWL_TOMATO_SOUP, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_WOODEN_BOWL_TOMATO_SOUP);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ApplePie), StoreSalesListConstants.PRICE_APPLE_PIE_BARKEEPER, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_APPLE_PIE);

				// Games
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(tarotpoker), StoreSalesListConstants.PRICE_TAROT_POKER, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_TAROT_POKER);
				if (MyServerSettings.SellChance())
				{
					StoreSalesListHelper.AddBuyItemWithNameRandom(this, "1016450", typeof(Chessboard), StoreSalesListConstants.PRICE_CHESSBOARD, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_CHESSBOARD);
				}
				if (MyServerSettings.SellChance())
				{
					StoreSalesListHelper.AddBuyItemWithNameRandom(this, "1016449", typeof(CheckerBoard), StoreSalesListConstants.PRICE_CHECKER_BOARD, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_CHECKER_BOARD);
				}
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Engines.Mahjong.MahjongGame), StoreSalesListConstants.PRICE_MAHJONG_GAME, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_MAHJONG_GAME);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Backgammon), StoreSalesListConstants.PRICE_BACKGAMMON, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_BACKGAMMON);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Dices), StoreSalesListConstants.PRICE_DICES, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_DICES);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Waterskin), StoreSalesListConstants.PRICE_WATERSKIN, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_WATERSKIN);

				// Henchman items
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(HenchmanFighterItem), StoreSalesListConstants.PRICE_HENCHMAN_FIGHTER, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_HENCHMAN_FIGHTER, StoreSalesListConstants.HUE_HENCHMAN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(HenchmanArcherItem), StoreSalesListConstants.PRICE_HENCHMAN_ARCHER, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_HENCHMAN_ARCHER, StoreSalesListConstants.HUE_HENCHMAN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(HenchmanWizardItem), StoreSalesListConstants.PRICE_HENCHMAN_WIZARD, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_HENCHMAN_WIZARD, StoreSalesListConstants.HUE_HENCHMAN);

				// Contracts
				if (MyServerSettings.SellChance())
				{
					StoreSalesListHelper.AddBuyItemWithNameRandom(this, "1041243", typeof(ContractOfEmployment), StoreSalesListConstants.PRICE_CONTRACT_EMPLOYMENT, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_CONTRACT_EMPLOYMENT);
				}
				// Barkeep contract (always available)
				StoreSalesListHelper.AddBuyItemWithNameRandom(this, "a barkeep contract", typeof(BarkeepContract), StoreSalesListConstants.PRICE_BARKEEP_CONTRACT, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_BARKEEP_CONTRACT);
				if (Multis.BaseHouse.NewVendorSystem)
				{
					if (MyServerSettings.SellChance())
					{
						StoreSalesListHelper.AddBuyItemWithNameRandom(this, "1062332", typeof(VendorRentalContract), StoreSalesListConstants.PRICE_VENDOR_RENTAL_CONTRACT, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_VENDOR_RENTAL_CONTRACT, StoreSalesListConstants.HUE_VENDOR_RENTAL_CONTRACT);
					}
				}
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Bowls
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfCarrots), StoreSalesListConstants.SELL_PRICE_WOODEN_BOWL_CARROTS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfCorn), StoreSalesListConstants.SELL_PRICE_WOODEN_BOWL_CORN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfLettuce), StoreSalesListConstants.SELL_PRICE_WOODEN_BOWL_LETTUCE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfPeas), StoreSalesListConstants.SELL_PRICE_WOODEN_BOWL_PEAS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(EmptyPewterBowl), StoreSalesListConstants.SELL_PRICE_EMPTY_PEWTER_BOWL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PewterBowlOfCorn), StoreSalesListConstants.SELL_PRICE_PEWTER_BOWL_CORN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PewterBowlOfLettuce), StoreSalesListConstants.SELL_PRICE_PEWTER_BOWL_LETTUCE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PewterBowlOfPeas), StoreSalesListConstants.SELL_PRICE_PEWTER_BOWL_PEAS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PewterBowlOfFoodPotatos), StoreSalesListConstants.SELL_PRICE_PEWTER_BOWL_POTATOS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfStew), StoreSalesListConstants.SELL_PRICE_WOODEN_BOWL_STEW);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfTomatoSoup), StoreSalesListConstants.SELL_PRICE_WOODEN_BOWL_TOMATO_SOUP);

				// Beverages
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BeverageBottle), StoreSalesListConstants.SELL_PRICE_BEVERAGE_BOTTLE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Jug), StoreSalesListConstants.SELL_PRICE_JUG);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Pitcher), StoreSalesListConstants.SELL_PRICE_PITCHER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GlassMug), StoreSalesListConstants.SELL_PRICE_GLASS_MUG);

				// Food
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BreadLoaf), StoreSalesListConstants.SELL_PRICE_BREAD_LOAF);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CheeseWheel), StoreSalesListConstants.SELL_PRICE_CHEESE_WHEEL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Ribs), StoreSalesListConstants.SELL_PRICE_RIBS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Peach), StoreSalesListConstants.SELL_PRICE_PEACH);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Pear), StoreSalesListConstants.SELL_PRICE_PEAR);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Grapes), StoreSalesListConstants.SELL_PRICE_GRAPES);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Apple), StoreSalesListConstants.SELL_PRICE_APPLE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Banana), StoreSalesListConstants.SELL_PRICE_BANANA);

				// Misc
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Candle), StoreSalesListConstants.SELL_PRICE_CANDLE);

				// Games
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Chessboard), StoreSalesListConstants.SELL_PRICE_CHESSBOARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CheckerBoard), StoreSalesListConstants.SELL_PRICE_CHECKER_BOARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(tarotpoker), StoreSalesListConstants.SELL_PRICE_TAROT_POKER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MahjongGame), StoreSalesListConstants.SELL_PRICE_MAHJONG_GAME);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Backgammon), StoreSalesListConstants.SELL_PRICE_BACKGAMMON);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Dices), StoreSalesListConstants.SELL_PRICE_DICES);

				// Contracts and misc
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ContractOfEmployment), StoreSalesListConstants.SELL_PRICE_CONTRACT_EMPLOYMENT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Waterskin), StoreSalesListConstants.SELL_PRICE_WATERSKIN);
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(RomulanAle), Utility.Random(StoreSalesListConstants.SELL_PRICE_ROMULAN_ALE_MIN, StoreSalesListConstants.SELL_PRICE_ROMULAN_ALE_MAX));
				}
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Beekeeper vendor.
	/// Sells honey, beeswax, beekeeping tools, and candle-related items.
	/// </summary>
	public class SBBeekeeper : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBBeekeeper class.
		/// </summary>
		public SBBeekeeper()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Honey (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(JarHoney), 600, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9EC);

				// Rare items
				StoreSalesListHelper.AddRareItemRandom(this, typeof(Beeswax), 1000, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1422);

				// Beekeeping tools and equipment
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(apiBeeHiveDeed), 2000, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_SMALL_MAX, 2330);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(HiveTool), 100, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 2549);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(apiSmallWaxPot), 250, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 2532);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(apiLargeWaxPot), 400, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 2541);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WaxingPot), 50, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x142B);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Honey and beeswax
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(JarHoney), 300);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Beeswax), 50);

				// Beekeeping tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(apiBeeHiveDeed), 1000);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(HiveTool), 50);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(apiSmallWaxPot), 125);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(apiLargeWaxPot), 200);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WaxingPot), 25);

				// Candles and lighting
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ColorCandleShort), 85);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ColorCandleLong), 90);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Candle), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CandleLarge), 70);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Candelabra), 140);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CandelabraStand), 210);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CandleLong), 80);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CandleShort), 75);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CandleSkull), 95);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CandleReligious), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WallSconce), 60);

				// Wax jars
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(JarsOfWaxMetal), 80);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(JarsOfWaxLeather), 80);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(JarsOfWaxInstrument), 80);
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Blacksmith vendor.
	/// Sells smithing tools, ingots, and related items.
	/// </summary>
	public class SBBlacksmith : SBInfo 
	{ 
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBBlacksmith class.
		/// </summary>
		public SBBlacksmith() 
		{ 
		} 

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo() 
			{ 	
				// Basic materials
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(IronIngot), 5, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1BF2);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Tongs), 13, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xFBB);

				// Special items (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(RandomLetter), 250, 25, StoreSalesListConstants.QTY_RANDOM_MAX, 0x55BF);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(SmithHammer), 21, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x0FB4);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo 
		{ 
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo() 
			{
				// Gem ingots
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(TopazIngot), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ShinySilverIngot), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(AmethystIngot), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(EmeraldIngot), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GarnetIngot), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(IceIngot), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(JadeIngot), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MarbleIngot), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(OnyxIngot), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(QuartzIngot), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RubyIngot), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SapphireIngot), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SpinelIngot), 120);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(StarRubyIngot), 120);

				// Tools and basic materials
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Tongs), 7);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(IronIngot), 4);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SmithHammer), 10);

				// Special items (always available)
				StoreSalesListHelper.AddSellItemRandom(this, typeof(MagicHammer), 300, 400);
			}
		}

		#endregion
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Bowyer vendor.
	/// Sells fletching tools and archery equipment.
	/// </summary>
	public class SBBowyer : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBBowyer class.
		/// </summary>
		public SBBowyer()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(FletcherTools), 2, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1F2C);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(ArcherQuiver), 32, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_SMALL_MAX, 0x2B02);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(ArcherPoonBag), 32, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_SMALL_MAX, 0x2B02);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FletcherTools), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ArcherQuiver), 16);
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Butcher vendor.
	/// Sells raw meat, processed meat products, and butchering tools.
	/// </summary>
	public class SBButcher : SBInfo 
	{ 
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBButcher class.
		/// </summary>
		public SBButcher() 
		{ 
		} 

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo() 
			{
				// Processed meat products
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Bacon), 70, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x979);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Ham), 260, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9C9);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Sausage), 180, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9C0);

				// Raw meat (always available)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RawChickenLeg), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1607);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RawBird), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9B9);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RawLambLeg), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1609);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(RawRibs), 20, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9F1);

				// Butchering tools
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ButcherKnife), 130, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x13F6);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Cleaver), 130, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xEC3);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SkinningKnife), 130, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xEC4);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo 
		{ 
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo() 
			{ 
				// Raw meat
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RawRibs), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RawLambLeg), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RawChickenLeg), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RawBird), 7);

				// Processed meat products
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Bacon), 15);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Sausage), 45);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Ham), 65);

				// Butchering tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ButcherKnife), 35);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Cleaver), 35);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SkinningKnife), 35);
			}
		}

		#endregion
	}

	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Carpenter vendor.
	/// Sells carpentry tools, materials, furniture, and rare decorative items.
	/// </summary>
	public class SBCarpenter : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBCarpenter class.
		/// </summary>
		public SBCarpenter()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Basic tools (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Hatchet), StoreSalesListConstants.PRICE_HATCHET, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_HATCHET);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LumberAxe), StoreSalesListConstants.PRICE_LUMBER_AXE, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_LUMBER_AXE, StoreSalesListConstants.HUE_LUMBER_AXE);

				// Basic materials (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Nails), StoreSalesListConstants.PRICE_NAILS, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_NAILS);

				// Tools and materials (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Axle), StoreSalesListConstants.PRICE_AXLE, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_AXLE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Board), StoreSalesListConstants.PRICE_BOARD, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_BOARD);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(DrawKnife), StoreSalesListConstants.PRICE_DRAW_KNIFE, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_DRAW_KNIFE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Froe), StoreSalesListConstants.PRICE_FROE, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_FROE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Scorp), StoreSalesListConstants.PRICE_SCORP, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_SCORP);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Inshave), StoreSalesListConstants.PRICE_INSHAVE, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_INSHAVE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(DovetailSaw), StoreSalesListConstants.PRICE_DOVETAIL_SAW, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_DOVETAIL_SAW);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Saw), StoreSalesListConstants.PRICE_SAW, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_SAW);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Hammer), StoreSalesListConstants.PRICE_HAMMER, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_HAMMER);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(MouldingPlane), StoreSalesListConstants.PRICE_MOULDING_PLANE, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_MOULDING_PLANE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SmoothingPlane), StoreSalesListConstants.PRICE_SMOOTHING_PLANE, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_SMOOTHING_PLANE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(JointingPlane), StoreSalesListConstants.PRICE_JOINTING_PLANE, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_JOINTING_PLANE);

				// WoodworkingTools (always available, different quantity range)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(WoodworkingTools), StoreSalesListConstants.PRICE_WOODWORKING_TOOLS, StoreSalesListConstants.QTY_WOODWORKING_TOOLS_MIN, StoreSalesListConstants.QTY_WOODWORKING_TOOLS_MAX, StoreSalesListConstants.ITEMID_WOODWORKING_TOOLS);

				// Addon deeds (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SawMillEastAddonDeed), StoreSalesListConstants.PRICE_SAWMILL_ADDON_DEED, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_SAWMILL_ADDON_DEED);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SawMillSouthAddonDeed), StoreSalesListConstants.PRICE_SAWMILL_SOUTH_ADDON_DEED, StoreSalesListConstants.QTY_BARKEEPER_MIN, StoreSalesListConstants.QTY_BARKEEPER_MAX, StoreSalesListConstants.ITEMID_SAWMILL_ADDON_DEED);

				// Rare crates and decorative items (very rare chance, random price and quantity)
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(AdventurerCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F9B);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(AlchemyCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F91);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(ArmsCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F9E);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BakerCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F92);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BeekeeperCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F95);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BlacksmithCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F8D);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(BowyerCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F97);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(ButcherCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F89);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(CarpenterCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F8A);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(FletcherCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F88);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(HealerCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F98);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(HugeCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F86);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(JewelerCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F8B);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(LibrarianCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F96);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(MusicianCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F94);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NecromancerCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F9A);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(ProvisionerCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F8E);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(SailorCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F9C);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(StableCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F87);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(SupplyCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F9D);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(TailorCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F8F);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(TavernCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F99);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(TinkerCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F90);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(TreasureCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F93);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(WizardryCrate), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x4F8C);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmoireA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C43);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmoireB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C45);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmoireC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C47);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmoireD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C89);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmoireE), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x38B);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmoireF), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x38D);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmoireG), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CC9);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmoireH), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CCB);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmoireI), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CCD);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmoireJ), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3D26);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmorShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3BF1);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmorShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C31);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmorShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C63);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmorShelfD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CAD);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewArmorShelfE), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CEF);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBakerShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C3B);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBakerShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C65);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBakerShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C67);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBakerShelfD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CBF);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBakerShelfE), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CC1);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBakerShelfF), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CF1);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBakerShelfG), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CF3);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBlacksmithShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C41);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBlacksmithShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C4B);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBlacksmithShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C6B);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBlacksmithShelfD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CC5);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBlacksmithShelfE), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CF7);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBookShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C15);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBookShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C2B);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBookShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C2D);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBookShelfD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C33);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBookShelfE), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C5F);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBookShelfF), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C61);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBookShelfG), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C79);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBookShelfH), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CA5);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBookShelfI), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CA7);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBookShelfJ), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CAF);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBookShelfK), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CEB);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBookShelfL), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CED);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBookShelfM), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3D05);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBowyerShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C29);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBowyerShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C5D);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBowyerShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CA3);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewBowyerShelfD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CE9);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewCarpenterShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C6F);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewCarpenterShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CD7);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewCarpenterShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CFB);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewClothShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C51);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewClothShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C53);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewClothShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C75);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewClothShelfD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C77);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewClothShelfE), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CDD);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewClothShelfF), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CDF);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewClothShelfG), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CFF);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewClothShelfH), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3D01);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDarkBookShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3BF9);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDarkBookShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3BFB);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDarkShelf), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3BFD);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C7F);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C81);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C83);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C85);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersE), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C87);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersF), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CB5);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersG), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CB7);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersH), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CB9);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersI), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CBB);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersJ), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CBD);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersK), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3D0B);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersL), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3D20);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersM), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3D22);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrawersN), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3D24);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrinkShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C27);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrinkShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C5B);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrinkShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CA1);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrinkShelfD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CE7);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewDrinkShelfE), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C1B);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewHelmShelf), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3BFF);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewHunterShelf), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C4D);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewKitchenShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C19);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewKitchenShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C39);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewOldBookShelf), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x19FF);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewPotionShelf), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3BF3);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewRuinedBookShelf), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0xC14);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C35);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C3D);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C69);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewShelfD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C7B);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewShelfE), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CB1);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewShelfF), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CC3);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewShelfG), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CF5);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewShelfH), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3D07);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewShoeShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C37);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewShoeShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C7D);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewShoeShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CB3);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewShoeShelfD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3D09);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewSorcererShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C4F);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewSorcererShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C73);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewSorcererShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CDB);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewSorcererShelfD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CFD);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewSupplyShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C57);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewSupplyShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C9D);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewSupplyShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CE3);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTailorShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C3F);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTailorShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C6D);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTailorShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CC7);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTailorShelfD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CF9);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTannerShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C23);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTannerShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C49);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTavernShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C25);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTavernShelfD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C59);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTavernShelfE), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C9F);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTavernShelfF), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CE5);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTinkerShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C71);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTinkerShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CD9);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTinkerShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3D03);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewTortureShelf), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C2F);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewWizardShelfA), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C17);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewWizardShelfB), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C1D);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewWizardShelfC), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C21);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewWizardShelfD), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C55);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewWizardShelfE), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3C9B);
				StoreSalesListHelper.AddVeryRareItemRandom(this, typeof(NewWizardShelfF), StoreSalesListConstants.PRICE_RARE_ITEM_MIN, StoreSalesListConstants.PRICE_RARE_ITEM_MAX, StoreSalesListConstants.QTY_RARE_ITEM_MIN, StoreSalesListConstants.QTY_RARE_ITEM_MAX, 0x3CE1);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Hatchet), StoreSalesListConstants.SELL_PRICE_HATCHET);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LumberAxe), StoreSalesListConstants.SELL_PRICE_LUMBER_AXE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Saw), StoreSalesListConstants.SELL_PRICE_SAW);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Scorp), StoreSalesListConstants.SELL_PRICE_SCORP);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SmoothingPlane), StoreSalesListConstants.SELL_PRICE_SMOOTHING_PLANE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(DrawKnife), StoreSalesListConstants.SELL_PRICE_DRAW_KNIFE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Froe), StoreSalesListConstants.SELL_PRICE_FROE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Hammer), StoreSalesListConstants.SELL_PRICE_HAMMER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Inshave), StoreSalesListConstants.SELL_PRICE_INSHAVE);

				// Containers and furniture
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBox), StoreSalesListConstants.SELL_PRICE_WOODEN_BOX);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SmallCrate), StoreSalesListConstants.SELL_PRICE_SMALL_CRATE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MediumCrate), StoreSalesListConstants.SELL_PRICE_MEDIUM_CRATE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LargeCrate), StoreSalesListConstants.SELL_PRICE_LARGE_CRATE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenChest), StoreSalesListConstants.SELL_PRICE_WOODEN_CHEST);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LargeTable), StoreSalesListConstants.SELL_PRICE_LARGE_TABLE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Nightstand), StoreSalesListConstants.SELL_PRICE_NIGHTSTAND);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(YewWoodTable), StoreSalesListConstants.SELL_PRICE_YEW_WOOD_TABLE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Throne), StoreSalesListConstants.SELL_PRICE_THRONE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenThrone), StoreSalesListConstants.SELL_PRICE_WOODEN_THRONE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Stool), StoreSalesListConstants.SELL_PRICE_STOOL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FootStool), StoreSalesListConstants.SELL_PRICE_FOOT_STOOL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FancyWoodenChairCushion), StoreSalesListConstants.SELL_PRICE_FANCY_WOODEN_CHAIR_CUSHION);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenChairCushion), StoreSalesListConstants.SELL_PRICE_WOODEN_CHAIR_CUSHION);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenChair), StoreSalesListConstants.SELL_PRICE_WOODEN_CHAIR);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BambooChair), StoreSalesListConstants.SELL_PRICE_BAMBOO_CHAIR);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBench), StoreSalesListConstants.SELL_PRICE_WOODEN_BENCH);

				// Additional tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodworkingTools), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(JointingPlane), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MouldingPlane), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(DovetailSaw), 7);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Axle), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Club), 13);

				// Wooden plate armor
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenPlateArms), 90);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenPlateChest), 119);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenPlateGloves), 70);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenPlateGorget), 50);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenPlateLegs), 106);

				// Logs
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Log), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(AshLog), 2);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CherryLog), 2);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(EbonyLog), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GoldenOakLog), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(HickoryLog), 4);
				// Commented out logs: MahoganyLog, DriftwoodLog, OakLog, PineLog, GhostLog, WalnutLog
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RosewoodLog), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ElvenLog), 12);
				// Commented out: PetrifiedLog

				// Materials
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Board), 2);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(AshBoard), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CherryBoard), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(EbonyBoard), 4);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GoldenOakBoard), 4);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(HickoryBoard), 5);
				// Commented out boards: MahoganyBoard, DriftwoodBoard, OakBoard, PineBoard, GhostBoard, WalnutBoard, PetrifiedBoard
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RosewoodBoard), 7);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ElvenBoard), 14);
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Cobbler vendor.
	/// Sells footwear including shoes, boots, sandals, and leather variants.
	/// </summary>
	public class SBCobbler : SBInfo 
	{ 
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBCobbler class.
		/// </summary>
		public SBCobbler() 
		{ 
		} 

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo() 
			{ 	
				// Standard footwear (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Shoes), 8, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x170f, Utility.RandomNeutralHue());

				// Standard footwear (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ThighBoots), 100, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1711, Utility.RandomNeutralHue());
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Boots), 10, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x170b, Utility.RandomNeutralHue());
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Sandals), 5, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x170d, Utility.RandomNeutralHue());

				// Leather footwear
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LeatherSandals), 60, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x170d, 0x83E);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LeatherShoes), 75, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x170f, 0x83E);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LeatherBoots), 90, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x170b, 0x83E);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LeatherThighBoots), 105, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1711, 0x83E);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo 
		{ 
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo() 
			{ 
				// Standard footwear
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MagicBoots), 70);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Shoes), 4);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Boots), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ThighBoots), 7);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Sandals), 2);

				// Rare magic boots
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(MagicBoots), 25);

				// Leather footwear
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LeatherSandals), 30);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LeatherShoes), 37);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LeatherBoots), 45);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LeatherThighBoots), 52);

				// Rare leather boots
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(LeatherSoftBoots), 60);
			}
		}

		#endregion
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Cook vendor.
	/// Sells food items, cooking ingredients, and cooking tools.
	/// </summary>
	public class SBCook : SBInfo 
	{ 
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBCook class.
		/// </summary>
		public SBCook() 
		{ 
		} 

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		#endregion 

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo() 
			{ 
				// Bread (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(BreadLoaf), 50, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_BREAD_PIE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(BreadLoaf), 50, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x103C);

				// Baked goods
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ApplePie), 170, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1041);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Cake), 1530, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9E9);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Muffins), 1300, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9EA);

				// Dairy and meat
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CheeseWheel), 310, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x97E);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CookedBird), 170, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9B7);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LambLeg), 80, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x160A);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ChickenLeg), 50, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1608);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RoastPig), 1060, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9BB);

				// Vegetable bowls (wooden)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfCarrots), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x15F9);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfCorn), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x15FA);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfLettuce), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x15FB);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfPeas), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x15FC);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfStew), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1604);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfTomatoSoup), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1606);

				// Vegetable bowls (pewter)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(EmptyPewterBowl), 5, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x15FD);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfCorn), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x15FE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfLettuce), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x15FF);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfPeas), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1600);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfFoodPotatos), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1601);

				// Ingredients and tools
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SackFlour), 15, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1039);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RollingPin), 5, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1043);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(FlourSifter), 5, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x103E);
				StoreSalesListHelper.AddBuyItemWithNameRandom(this, "1044567", typeof(Skillet), 5, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x97F);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(GardenTool), 12, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xDFD, 0x84F);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(HerbalistCauldron), 247, StoreSalesListConstants.QTY_SINGLE, 0x2676);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(MixingSpoon), 34, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1E27, StoreSalesListConstants.HUE_MIXING_SPOON);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Jar), 5, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x10B4);

				// Rare items
				StoreSalesListHelper.AddRareItemRandom(this, typeof(CBookDruidicHerbalism), 50, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x2D50);
			}
		}

		#endregion 

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo 
		{ 
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo() 
			{ 
				// Food items
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CheeseWheel), 60);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CookedBird), 40);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RoastPig), 250);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Cake), 250);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BreadLoaf), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ChickenLeg), 15);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LambLeg), 20);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Muffins), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ApplePie), 15);

				// Ingredients
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SackFlour), 5);

				// Cooking tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Skillet), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FlourSifter), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RollingPin), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GardenTool), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(HerbalistCauldron), 123);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MixingSpoon), 17);

				// Vegetable bowls (wooden)
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfCarrots), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfCorn), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfLettuce), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfPeas), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfStew), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfTomatoSoup), 5);

				// Vegetable bowls (pewter)
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(EmptyPewterBowl), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PewterBowlOfCorn), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PewterBowlOfLettuce), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PewterBowlOfPeas), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PewterBowlOfFoodPotatos), 5);

				// Herbalism items
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(PlantHerbalism_Leaf), 3, 7);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(PlantHerbalism_Flower), 3, 7);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(PlantHerbalism_Mushroom), 3, 7);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(PlantHerbalism_Lilly), 3, 7);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(PlantHerbalism_Cactus), 3, 7);
				StoreSalesListHelper.AddSellItemWithChanceRandom(this, typeof(PlantHerbalism_Grass), 3, 7);
			}
		}

		#endregion
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Farmer vendor.
	/// Sells vegetables, fruits, crops, patch deeds, and farming-related items.
	/// </summary>
	public class SBFarmer : SBInfo 
	{ 
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBFarmer class.
		/// </summary>
		public SBFarmer() 
		{ 
		} 

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo() 
			{ 
				// Vegetables
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Cabbage), 5, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xC7B);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Cantaloupe), 6, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xC79);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Carrot), 3, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xC78);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(HoneydewMelon), 7, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xC74);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Squash), 3, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xC72);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Lettuce), 5, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xC70);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Onion), 3, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xC6D);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Pumpkin), 11, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xC6A);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(GreenGourd), 3, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xC66);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(YellowGourd), 3, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xC64);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Watermelon), 7, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xC5C);

				// Dairy and eggs
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Eggs), 3, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9B5);
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Milk, 120, Utility.Random(StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX), 0x9AD, 0));
				}

				// Fruits
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Peach), 3, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9D2);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Pear), 3, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x994);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Lemon), 3, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x1728);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Lime), 3, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x172A);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Grapes), 8, 1, 1000, 0x9D1);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Apple), 5, 10, 1000, 0x9D0);

				// Crops
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WheatSheaf), 5, 50, 1000, 0xF36);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Hops), 5, 10, 1000, 0x1727);

				// Patch deeds
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(PumpkinPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(CabbagePatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(MelonPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(TurnipPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(GourdPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(OnionPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(LettucePatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(SquashPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(HoneydewPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(CarrotPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(CantaloupePatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(CornPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(PotatoPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(BananaPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(CoconutPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(DatePatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(GarlicPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(NightshadePatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(GinsengPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(MandrakePatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(FlaxPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(TomatoPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(GreenTeaPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(PeaPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(HayPatchDeed), 2500, StoreSalesListConstants.QTY_FIXED_10, StoreSalesListConstants.ITEMID_DEED);

				// Special items
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CattleHaybale), 17000, StoreSalesListConstants.QTY_VERY_RARE_MIN, StoreSalesListConstants.QTY_VERY_RARE_MAX, 0x50AE);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo 
		{ 
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo() 
			{ 
				// Dairy and eggs
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Pitcher), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Eggs), 1);

				// Fruits
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Apple), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Grapes), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Lemon), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Lime), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Peach), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Pear), 1);

				// Vegetables
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Watermelon), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(YellowGourd), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GreenGourd), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Pumpkin), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Onion), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Lettuce), 2);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Squash), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Carrot), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(HoneydewMelon), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Cantaloupe), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Cabbage), 2);

				// Crops
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WheatSheaf), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Hops), 1);
			}
		}

		#endregion
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Fisherman vendor.
	/// Sells fish, fishing equipment, and related items.
	/// </summary>
	public class SBFisherman : SBInfo 
	{ 
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBFisherman class.
		/// </summary>
		public SBFisherman() 
		{ 
		} 

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo() 
			{ 
				// Fish and fish steaks
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RawFishSteak), 10, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x97A);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Fish), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9CC);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Fish), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9CD);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Fish), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9CE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Fish), 30, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x9CF);

				// Fishing equipment (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(FishingPole), 100, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0xDC0);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(GrapplingHook), 58, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x4F40);

				// Special fishing items (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(MovableTrashChest), 5000, StoreSalesListConstants.QTY_VERY_RARE_MIN, StoreSalesListConstants.QTY_VERY_RARE_MAX, 0x2811);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(ArcherPoonBag), 32, StoreSalesListConstants.QTY_VERY_RARE_MIN, StoreSalesListConstants.QTY_VERY_RARE_MAX, 0x2B02);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo 
		{ 
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo() 
			{ 
				// Fish (rare)
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(RawFishSteak), 5);
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(Fish), 5);

				// Fishing equipment
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FishingPole), 7);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GrapplingHook), 29);

				// Rare items
				StoreSalesListHelper.AddSellRareItemWithChanceRandom(this, typeof(MegalodonTooth), 500, 2000);
			}
		}

		#endregion
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Fortune Teller vendor.
	/// Sells basic healing items, potions, and reagents.
	/// </summary>
	public class SBFortuneTeller : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBFortuneTeller class.
		/// </summary>
		public SBFortuneTeller()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Basic healing items (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Bandage), StoreSalesListConstants.PRICE_BANDAGE / 5, 10, 60, StoreSalesListConstants.ITEMID_BANDAGE);

				// Potions
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LesserHealPotion), StoreSalesListConstants.PRICE_LESSER_POTION / 5, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, 0x25FD);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RefreshPotion), StoreSalesListConstants.PRICE_LESSER_POTION / 5, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_POTION_REFRESH);

				// Reagents
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Ginseng), 3, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_REAGENT_GINSENG);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Garlic), 3, StoreSalesListConstants.QTY_RANDOM_MIN, StoreSalesListConstants.QTY_RANDOM_MAX, StoreSalesListConstants.ITEMID_REAGENT_GARLIC);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Potions
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LesserHealPotion), 7);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RefreshPotion), 7);

				// Reagents
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Garlic), 2);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Ginseng), 2);
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBFurtrader : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBFurtrader() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Hides ), 4, Utility.Random( 1,100 ), 0x1078, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Leather ), 4, Utility.Random( 1,100 ), 0x1081, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Furs ), 5, Utility.Random( 1,25 ), 0x11F4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FursWhite ), 8, Utility.Random( 1,25 ), 0x11F4, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurCape ), 16, Utility.Random( 1,5 ), 0x230A, 0x907 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurRobe ), 20, Utility.Random( 1,5 ), 0x1F03, 0x907 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurBoots ), 10, Utility.Random( 1,5 ), 0x2307, 0x907 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurCap ), 8, Utility.Random( 1,5 ), 0x1DB9, 0x907 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurSarong ), 14, Utility.Random( 1,5 ), 0x230C, 0x907 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurArms ), 100, Utility.Random( 1,100 ), 0x2B77, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurTunic ), 121, Utility.Random( 1,100 ), 0x2B79, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurLegs ), 100, Utility.Random( 1,100 ), 0x2B78, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurCape ), 18, Utility.Random( 1,5 ), 0x230A, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurRobe ), 24, Utility.Random( 1,5 ), 0x1F03, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurBoots ), 12, Utility.Random( 1,5 ), 0x2307, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurCap ), 16, Utility.Random( 1,5 ), 0x1DB9, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurSarong ), 16, Utility.Random( 1,5 ), 0x230C, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurArms ), 100, Utility.Random( 1,100 ), 0x2B77, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurTunic ), 121, Utility.Random( 1,100 ), 0x2B79, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurLegs ), 100, Utility.Random( 1,100 ), 0x2B78, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BearMask ), Utility.Random( 28,50 ), Utility.Random( 1,5 ), 0x1545, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DeerMask ), Utility.Random( 28,50 ), Utility.Random( 1,5 ), 0x1547, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WolfMask ), Utility.Random( 28,50 ), Utility.Random( 1,5 ), 0x2B6D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DemonSkin ), 1235, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DragonSkin ), 1235, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NightmareSkin ), 1228, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SerpentSkin ), 1214, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TrollSkin ), 1221, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( UnicornSkin ), 1228, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyChance() ){Add( typeof( UnicornSkin ), 30 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Furs ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FursWhite ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DemonSkin ), 40 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DragonSkin ), 50 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( NightmareSkin ), 30 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SerpentSkin ), 10 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( TrollSkin ), 20 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurCape ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurCape ), 9 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurRobe ), 10 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurRobe ), 12 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurBoots ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurBoots ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurSarong ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurSarong ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurCap ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurCap ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurArms ), 50 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurTunic ), 60 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurLegs ), 50 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurArms ), 50 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurTunic ), 60 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurLegs ), 50 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( BearMask ), 14 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DeerMask ), 14 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WolfMask ), 14 ); }  

				if ( MyServerSettings.BuyChance() ){Add( typeof( Hides ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinedHides ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( HornedHides ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( BarbedHides ), 4 ); }  
				// TODO: Future implementation - Special hide types disabled
				//if ( MyServerSettings.BuyChance() ){Add( typeof( NecroticHides ), 4 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( VolcanicHides ), 5 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( FrozenHides ), 5 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( DraconicHides ), 6 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( HellishHides ), 7 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( DinosaurHides ), 7 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( AlienHides ), 7 ); }  

			if ( MyServerSettings.BuyChance() ){Add( typeof( Leather ), 3 ); }  
			// TODO: Future implementation - Special leather types disabled from NPC sales
			//if ( MyServerSettings.BuyChance() ){Add( typeof( SpinedLeather ), 4 ); }  
			//if ( MyServerSettings.BuyChance() ){Add( typeof( HornedLeather ), 4 ); }  
			//if ( MyServerSettings.BuyChance() ){Add( typeof( BarbedLeather ), 5 ); }  
			// TODO: Future implementation - Special leather types disabled
			//if ( MyServerSettings.BuyChance() ){Add( typeof( NecroticLeather ), 5 ); }
				//if ( MyServerSettings.BuyChance() ){Add( typeof( VolcanicLeather ), 6 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( FrozenLeather ), 6 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( DraconicLeather ), 7 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( HellishLeather ), 8 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( DinosaurLeather ), 8 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( AlienLeather ), 8 ); }  
			} 
		} 
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Glassblower vendor.
	/// Sells glass containers, glassblowing books, and glassblowing tools.
	/// </summary>
	public class SBGlassblower : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBGlassblower class.
		/// </summary>
		public SBGlassblower()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Containers (chance-based)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Bottle), StoreSalesListConstants.PRICE_BOTTLE_GLASSBLOWER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BOTTLE);

				// Containers and books (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Jar), StoreSalesListConstants.PRICE_JAR_GLASSBLOWER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_JAR);
				StoreSalesListHelper.AddBuyItemWithNameRandom(this, "Crafting Glass With Glassblowing", typeof(GlassblowingBook), StoreSalesListConstants.PRICE_GLASSBLOWING_BOOK, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BOOK);
				StoreSalesListHelper.AddBuyItemWithNameRandom(this, "Finding Glass-Quality Sand", typeof(SandMiningBook), StoreSalesListConstants.PRICE_SAND_MINING_BOOK, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BOOK);

				// Tools (chance-based)
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo("1044608", typeof(Blowpipe), StoreSalesListConstants.PRICE_BLOWPIPE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_BLOWPIPE, StoreSalesListConstants.HUE_BLOWPIPE)); }
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Monocle), StoreSalesListConstants.PRICE_MONOCLE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_LARGE_MAX, StoreSalesListConstants.ITEMID_MONOCLE);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Bottle), StoreSalesListConstants.SELL_PRICE_BOTTLE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Jar), StoreSalesListConstants.SELL_PRICE_JAR);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GlassblowingBook), StoreSalesListConstants.SELL_PRICE_GLASSBLOWING_BOOK);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SandMiningBook), StoreSalesListConstants.SELL_PRICE_SAND_MINING_BOOK);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Blowpipe), StoreSalesListConstants.SELL_PRICE_BLOWPIPE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Monocle), StoreSalesListConstants.SELL_PRICE_MONOCLE);
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for HairStylist vendor.
	/// Sells hair dyes, special dyes, and disguise kits.
	/// </summary>
	public class SBHairStylist : SBInfo 
	{ 
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBHairStylist class.
		/// </summary>
		public SBHairStylist() 
		{ 
		} 

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo() 
			{ 
				// Special dyes (always available)
				StoreSalesListHelper.AddBuyItemWithNameRandom(this, "special beard dye", typeof(SpecialBeardDye), StoreSalesListConstants.PRICE_SPECIAL_BEARD_DYE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SPECIAL_DYE);
				StoreSalesListHelper.AddBuyItemWithNameRandom(this, "special hair dye", typeof(SpecialHairDye), StoreSalesListConstants.PRICE_SPECIAL_HAIR_DYE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SPECIAL_DYE);
				StoreSalesListHelper.AddBuyItemWithNameRandom(this, "1041060", typeof(HairDye), StoreSalesListConstants.PRICE_HAIR_DYE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_HAIR_DYE);
				StoreSalesListHelper.AddBuyItemWithNameRandom(this, "hair dye bottle", typeof(HairDyeBottle), StoreSalesListConstants.PRICE_HAIR_DYE_BOTTLE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_HAIR_DYE_BOTTLE);

				// Disguise kit (chance-based)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(DisguiseKit), StoreSalesListConstants.PRICE_DISGUISE_KIT, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_SMALL_MAX, StoreSalesListConstants.ITEMID_DISGUISE_KIT);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo 
		{ 
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo() 
			{ 
				// Dyes (always available)
				Add(typeof(HairDye), StoreSalesListConstants.SELL_PRICE_HAIR_DYE);
				Add(typeof(SpecialBeardDye), StoreSalesListConstants.SELL_PRICE_SPECIAL_BEARD_DYE);
				Add(typeof(SpecialHairDye), StoreSalesListConstants.SELL_PRICE_SPECIAL_HAIR_DYE);
				Add(typeof(HairDyeBottle), StoreSalesListConstants.SELL_PRICE_HAIR_DYE_BOTTLE);
			}
		}

		#endregion
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Healer vendor.
	/// Sells bandages, healing potions, reagents, and medical tools.
	/// </summary>
	public class SBHealer : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBHealer class.
		/// </summary>
		public SBHealer()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Basic medical supplies (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Bandage), StoreSalesListConstants.PRICE_BANDAGE, StoreSalesListConstants.QTY_BANDAGE_MIN, StoreSalesListConstants.QTY_BANDAGE_MAX, StoreSalesListConstants.ITEMID_BANDAGE);

				// Potions and reagents (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LesserHealPotion), StoreSalesListConstants.PRICE_LESSER_HEAL_POTION, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LESSER_HEAL_POTION);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Ginseng), StoreSalesListConstants.PRICE_GINSENG, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_GINSENG);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Garlic), StoreSalesListConstants.PRICE_GARLIC, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_GARLIC);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RefreshPotion), StoreSalesListConstants.PRICE_REFRESH_POTION, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_REFRESH_POTION);

				// Rare medical tools (rare chance)
				if (MyServerSettings.SellRareChance())
				{
					Add(new GenericBuyInfo(typeof(GraveShovel), StoreSalesListConstants.PRICE_GRAVE_SHOVEL, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_GRAVE_SHOVEL, StoreSalesListConstants.HUE_GRAVE_SHOVEL));
				}
				if (MyServerSettings.SellRareChance())
				{
					Add(new GenericBuyInfo(typeof(SurgeonsKnife), StoreSalesListConstants.PRICE_SURGEONS_KNIFE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_SURGEONS_KNIFE, StoreSalesListConstants.HUE_SURGEONS_KNIFE));
				}
				if (MyServerSettings.SellRareChance())
				{
					Add(new GenericBuyInfo(typeof(HealingDragonJar), StoreSalesListConstants.PRICE_HEALING_DRAGON_JAR, Utility.Random(StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX), StoreSalesListConstants.ITEMID_HEALING_DRAGON_JAR, StoreSalesListConstants.HUE_HEALING_DRAGON_JAR));
				}
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Potions and reagents
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LesserHealPotion), StoreSalesListConstants.SELL_PRICE_LESSER_HEAL_POTION);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RefreshPotion), StoreSalesListConstants.SELL_PRICE_REFRESH_POTION);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Garlic), StoreSalesListConstants.SELL_PRICE_GARLIC);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Ginseng), StoreSalesListConstants.SELL_PRICE_GINSENG);

				// Medical tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SurgeonsKnife), StoreSalesListConstants.SELL_PRICE_SURGEONS_KNIFE);

				// First aid kit (random price)
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(FirstAidKit), Utility.Random(StoreSalesListConstants.SELL_PRICE_FIRST_AID_KIT_MIN, StoreSalesListConstants.SELL_PRICE_FIRST_AID_KIT_MAX));
				}
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBDruid : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBDruid()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bandage ), 3, Utility.Random( 100,250 ), 0xE21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 1,100 ), 0x25FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 1,100 ), 0xF0B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GardenTool ), 12, Utility.Random( 1,100 ), 0xDFD, 0x84F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HerbalistCauldron ), 247, 1, 0x2676, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 1,100 ), 0x1E27, 0x979 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( CBookDruidicHerbalism ), 50, Utility.Random( 1,100 ), 0x2D50, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AppleTreeDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CherryBlossomTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DarkBrownTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GreyTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LightBrownTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PeachTreeDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PearTreeDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( VinePatchAddonDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HopsPatchDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HealingDragonJar ), 500, Utility.Random( 1,5 ), 0xF39, 0x966 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserHealPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RefreshPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Garlic ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ginseng ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GardenTool ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HerbalistCauldron ), 123 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MixingSpoon ), 17 ); } 
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBDruidTree : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBDruidTree()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bandage ), 2, Utility.Random( 10,150 ), 0xE21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 1,100 ), 0x25FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 1,100 ), 0xF0B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GardenTool ), 12, Utility.Random( 1,100 ), 0xDFD, 0x84F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HerbalistCauldron ), 247, 1, 0x2676, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 1,100 ), 0x1E27, 0x979 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( CBookDruidicHerbalism ), 50, Utility.Random( 1,100 ), 0x2D50, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AppleTreeDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CherryBlossomTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DarkBrownTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GreyTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LightBrownTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PeachTreeDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PearTreeDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( VinePatchAddonDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HopsPatchDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ShieldOfEarthPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x300 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( WoodlandProtectionPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x7E2 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ProtectiveFairyPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x9FF ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HerbalHealingPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x279 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GraspingRootsPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x83F ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( BlendWithForestPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x59C ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( SwarmOfInsectsPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xA70 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( VolcanicEruptionPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x54E ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( TreefellowPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x223 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( StoneCirclePotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x396 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( DruidicRunePotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x487 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( LureStonePotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x967 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NaturesPassagePotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x48B ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MushroomGatewayPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x3B7 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( RestorativeSoilPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x479 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( FireflyPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x491 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserHealPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RefreshPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Garlic ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ginseng ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GardenTool ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HerbalistCauldron ), 123 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MixingSpoon ), 17 ); } 
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Herbalist vendor.
	/// Sells reagents, herbalism tools, cauldrons, and plants for alchemy and herbalism.
	/// </summary>
	public class SBHerbalist : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBHerbalist class.
		/// </summary>
		public SBHerbalist()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Reagents (with chance, except Garlic which is always available)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Ginseng), StoreSalesListConstants.PRICE_GINSENG_HERBALIST, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_GINSENG_HERBALIST);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Garlic), StoreSalesListConstants.PRICE_GARLIC_HERBALIST, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_GARLIC_HERBALIST);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(MandrakeRoot), StoreSalesListConstants.PRICE_MANDRAKE_ROOT, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_MANDRAKE_ROOT);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Nightshade), StoreSalesListConstants.PRICE_NIGHTSHADE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_NIGHTSHADE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Bloodmoss), StoreSalesListConstants.PRICE_BLOODMOSS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BLOODMOSS);

				// Tools and equipment (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(MortarPestle), StoreSalesListConstants.PRICE_MORTAR_PESTLE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_MORTAR_PESTLE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Jar), StoreSalesListConstants.PRICE_JAR, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_JAR);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(GardenTool), StoreSalesListConstants.PRICE_GARDEN_TOOL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_GARDEN_TOOL, StoreSalesListConstants.HUE_GARDEN_TOOL);
				StoreSalesListHelper.AddBuyItemWithChance(this, typeof(HerbalistCauldron), StoreSalesListConstants.PRICE_HERBALIST_CAULDRON, 1, StoreSalesListConstants.ITEMID_HERBALIST_CAULDRON);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(MixingSpoon), StoreSalesListConstants.PRICE_MIXING_SPOON, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_MIXING_SPOON, StoreSalesListConstants.HUE_MIXING_SPOON);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(AlchemyTub), StoreSalesListConstants.PRICE_ALCHEMY_TUB, StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX, StoreSalesListConstants.ITEMID_ALCHEMY_TUB);

				// Books (rare chance)
				if (MyServerSettings.SellRareChance())
				{
					Add(new GenericBuyInfo(typeof(CBookDruidicHerbalism), StoreSalesListConstants.PRICE_CBOOK_DRUIDIC_HERBALISM, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_CBOOK_DRUIDIC_HERBALISM, StoreSalesListConstants.HUE_DEFAULT));
				}

				// Hanging plants (very rare chance, random prices)
				if (MyServerSettings.SellVeryRareChance())
				{
					Add(new GenericBuyInfo(typeof(HangingPlantA), Utility.Random(StoreSalesListConstants.PRICE_HANGING_PLANT_MIN, StoreSalesListConstants.PRICE_HANGING_PLANT_MAX), 1, StoreSalesListConstants.ITEMID_HANGING_PLANT_A, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellVeryRareChance())
				{
					Add(new GenericBuyInfo(typeof(HangingPlantB), Utility.Random(StoreSalesListConstants.PRICE_HANGING_PLANT_MIN, StoreSalesListConstants.PRICE_HANGING_PLANT_MAX), 1, StoreSalesListConstants.ITEMID_HANGING_PLANT_B, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellVeryRareChance())
				{
					Add(new GenericBuyInfo(typeof(HangingPlantC), Utility.Random(StoreSalesListConstants.PRICE_HANGING_PLANT_MIN, StoreSalesListConstants.PRICE_HANGING_PLANT_MAX), 1, StoreSalesListConstants.ITEMID_HANGING_PLANT_C, StoreSalesListConstants.HUE_DEFAULT));
				}
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Reagents
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Bloodmoss), StoreSalesListConstants.SELL_PRICE_BLOODMOSS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MandrakeRoot), StoreSalesListConstants.SELL_PRICE_MANDRAKE_ROOT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Garlic), StoreSalesListConstants.SELL_PRICE_GARLIC_HERBALIST);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Ginseng), StoreSalesListConstants.SELL_PRICE_GINSENG_HERBALIST);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Nightshade), StoreSalesListConstants.SELL_PRICE_NIGHTSHADE);

				// Tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Jar), StoreSalesListConstants.SELL_PRICE_JAR_HERBALIST);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MortarPestle), StoreSalesListConstants.SELL_PRICE_MORTAR_PESTLE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GardenTool), StoreSalesListConstants.SELL_PRICE_GARDEN_TOOL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(HerbalistCauldron), StoreSalesListConstants.SELL_PRICE_HERBALIST_CAULDRON);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MixingSpoon), StoreSalesListConstants.SELL_PRICE_MIXING_SPOON);

				// Plants and herbs (random prices)
				Add(typeof(PlantHerbalism_Flower), Utility.Random(StoreSalesListConstants.SELL_PRICE_PLANT_HERBALISM_MIN, StoreSalesListConstants.SELL_PRICE_PLANT_HERBALISM_MAX));
				Add(typeof(PlantHerbalism_Leaf), Utility.Random(StoreSalesListConstants.SELL_PRICE_PLANT_HERBALISM_MIN, StoreSalesListConstants.SELL_PRICE_PLANT_HERBALISM_MAX));
				Add(typeof(PlantHerbalism_Mushroom), Utility.Random(StoreSalesListConstants.SELL_PRICE_PLANT_HERBALISM_MIN, StoreSalesListConstants.SELL_PRICE_PLANT_HERBALISM_MAX));
				Add(typeof(PlantHerbalism_Lilly), Utility.Random(StoreSalesListConstants.SELL_PRICE_PLANT_HERBALISM_MIN, StoreSalesListConstants.SELL_PRICE_PLANT_HERBALISM_MAX));
				Add(typeof(PlantHerbalism_Cactus), Utility.Random(StoreSalesListConstants.SELL_PRICE_PLANT_HERBALISM_MIN, StoreSalesListConstants.SELL_PRICE_PLANT_HERBALISM_MAX));

				// Home plants (random prices)
				Add(typeof(HomePlants_Flower), Utility.Random(StoreSalesListConstants.SELL_PRICE_HOME_PLANTS_MIN, StoreSalesListConstants.SELL_PRICE_HOME_PLANTS_MAX));
				Add(typeof(HomePlants_Leaf), Utility.Random(StoreSalesListConstants.SELL_PRICE_HOME_PLANTS_MIN, StoreSalesListConstants.SELL_PRICE_HOME_PLANTS_MAX));
				Add(typeof(HomePlants_Mushroom), Utility.Random(StoreSalesListConstants.SELL_PRICE_HOME_PLANTS_MIN, StoreSalesListConstants.SELL_PRICE_HOME_PLANTS_MAX));
				Add(typeof(HomePlants_Cactus), Utility.Random(StoreSalesListConstants.SELL_PRICE_HOME_PLANTS_MIN, StoreSalesListConstants.SELL_PRICE_HOME_PLANTS_MAX));
				Add(typeof(HomePlants_Lilly), Utility.Random(StoreSalesListConstants.SELL_PRICE_HOME_PLANTS_MIN, StoreSalesListConstants.SELL_PRICE_HOME_PLANTS_MAX));
				Add(typeof(HomePlants_Grass), Utility.Random(StoreSalesListConstants.SELL_PRICE_HOME_PLANTS_MIN, StoreSalesListConstants.SELL_PRICE_HOME_PLANTS_MAX));

				// Special seaweed (random price)
				Add(typeof(SpecialSeaweed), Utility.Random(StoreSalesListConstants.SELL_PRICE_SPECIAL_SEAWEED_MIN, StoreSalesListConstants.SELL_PRICE_SPECIAL_SEAWEED_MAX));

				// Hanging plants (with chance, random prices)
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(HangingPlantA), Utility.Random(StoreSalesListConstants.SELL_PRICE_HANGING_PLANT_MIN, StoreSalesListConstants.SELL_PRICE_HANGING_PLANT_MAX));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(HangingPlantB), Utility.Random(StoreSalesListConstants.SELL_PRICE_HANGING_PLANT_MIN, StoreSalesListConstants.SELL_PRICE_HANGING_PLANT_MAX));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(HangingPlantC), Utility.Random(StoreSalesListConstants.SELL_PRICE_HANGING_PLANT_MIN, StoreSalesListConstants.SELL_PRICE_HANGING_PLANT_MAX));
				}

				// Alchemy tub (random price)
				Add(typeof(AlchemyTub), Utility.Random(StoreSalesListConstants.SELL_PRICE_ALCHEMY_TUB_MIN, StoreSalesListConstants.SELL_PRICE_ALCHEMY_TUB_MAX));
			}
		}

		#endregion
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Holy Mage vendor.
	/// Sells spellbooks, reagents, scrolls, magic staves, and arcane supplies.
	/// </summary>
	public class SBHolyMage : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBHolyMage class.
		/// </summary>
		public SBHolyMage()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Basic magical supplies (always available or with chance)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Spellbook), StoreSalesListConstants.PRICE_SPELLBOOK_HOLY_MAGE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SPELLBOOK_HOLY_MAGE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ScribesPen), StoreSalesListConstants.PRICE_SCRIBES_PEN, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SCRIBES_PEN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(BlankScroll), StoreSalesListConstants.PRICE_BLANK_SCROLL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BLANK_SCROLL);

				// Hats (with random dyed hues)
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo("1041072", typeof(MagicWizardsHat), StoreSalesListConstants.PRICE_MAGIC_WIZARDS_HAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_MAGIC_WIZARDS_HAT, Utility.RandomDyedHue()));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo(typeof(WitchHat), StoreSalesListConstants.PRICE_WITCH_HAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_WITCH_HAT, Utility.RandomDyedHue()));
				}

				// Magical tools and runes
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RecallRune), StoreSalesListConstants.PRICE_RECALL_RUNE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_RECALL_RUNE);

				// Reagents (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(BlackPearl), StoreSalesListConstants.PRICE_BLACK_PEARL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BLACK_PEARL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Bloodmoss), StoreSalesListConstants.PRICE_BLOODMOSS_HOLY_MAGE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BLOODMOSS_HOLY_MAGE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Garlic), StoreSalesListConstants.PRICE_GARLIC_HOLY_MAGE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_GARLIC_HOLY_MAGE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Ginseng), StoreSalesListConstants.PRICE_GINSENG_HOLY_MAGE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_GINSENG_HOLY_MAGE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(MandrakeRoot), StoreSalesListConstants.PRICE_MANDRAKE_ROOT_HOLY_MAGE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_MANDRAKE_ROOT_HOLY_MAGE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Nightshade), StoreSalesListConstants.PRICE_NIGHTSHADE_HOLY_MAGE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_NIGHTSHADE_HOLY_MAGE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SpidersSilk), StoreSalesListConstants.PRICE_SPIDERS_SILK, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SPIDERS_SILK);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SulfurousAsh), StoreSalesListConstants.PRICE_SULFUROUS_ASH, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SULFUROUS_ASH);

				// Rare magical items
				if (MyServerSettings.SellRareChance())
				{
					Add(new GenericBuyInfo(typeof(reagents_magic_jar1), StoreSalesListConstants.PRICE_REAGENTS_MAGIC_JAR1, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_REAGENTS_MAGIC_JAR1, StoreSalesListConstants.HUE_DEFAULT));
				}

				// Magical staves and tools
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WizardStaff), StoreSalesListConstants.PRICE_WIZARD_STAFF, StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX, StoreSalesListConstants.ITEMID_WIZARD_STAFF, StoreSalesListConstants.HUE_WIZARD_STAFF);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WizardStick), StoreSalesListConstants.PRICE_WIZARD_STICK, StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX, StoreSalesListConstants.ITEMID_WIZARD_STICK, StoreSalesListConstants.HUE_WIZARD_STICK);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(MageEye), StoreSalesListConstants.PRICE_MAGE_EYE, StoreSalesListConstants.QTY_MEDIUM_MIN, StoreSalesListConstants.QTY_MEDIUM_MAX, StoreSalesListConstants.ITEMID_MAGE_EYE, StoreSalesListConstants.HUE_MAGE_EYE);

				// Regular scrolls (dynamic pricing and item IDs)
				AddRegularScrolls();
			}

			/// <summary>
			/// Adds regular magic scrolls to the buy list with dynamic pricing.
			/// </summary>
			private void AddRegularScrolls()
			{
				Type[] types = Loot.RegularScrollTypes;

				for (int i = 0; i < types.Length && i < 8; ++i)
				{
					int itemID = StoreSalesListConstants.ITEMID_SCROLL_BASE + i;

					if (i == 6)
						itemID = 0x1F2D;
					else if (i > 6)
						--itemID;

					int price = StoreSalesListConstants.PRICE_SCROLL_BASE + ((i / 8) * StoreSalesListConstants.PRICE_SCROLL_INCREMENT);

					if (MyServerSettings.SellChance())
					{
						Add(new GenericBuyInfo(types[i], price, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), itemID, StoreSalesListConstants.HUE_DEFAULT));
					}
				}
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Magical items
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(MagicTalisman), Utility.Random(StoreSalesListConstants.SELL_PRICE_MAGIC_TALISMAN_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_TALISMAN_MAX));
				}

				// Reagents
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BlackPearl), StoreSalesListConstants.SELL_PRICE_BLACK_PEARL_HOLY_MAGE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Bloodmoss), StoreSalesListConstants.SELL_PRICE_BLOODMOSS_HOLY_MAGE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MandrakeRoot), StoreSalesListConstants.SELL_PRICE_MANDRAKE_ROOT_HOLY_MAGE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Garlic), StoreSalesListConstants.SELL_PRICE_GARLIC_HOLY_MAGE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Ginseng), StoreSalesListConstants.SELL_PRICE_GINSENG_HOLY_MAGE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Nightshade), StoreSalesListConstants.SELL_PRICE_NIGHTSHADE_HOLY_MAGE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SpidersSilk), StoreSalesListConstants.SELL_PRICE_SPIDERS_SILK);

				if (MyServerSettings.BuyRareChance())
				{
					Add(typeof(SulfurousAsh), StoreSalesListConstants.SELL_PRICE_SULFUROUS_ASH);
				}

				// Magical tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RecallRune), StoreSalesListConstants.SELL_PRICE_RECALL_RUNE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Spellbook), StoreSalesListConstants.SELL_PRICE_SPELLBOOK_HOLY_MAGE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BlankScroll), StoreSalesListConstants.SELL_PRICE_BLANK_SCROLL);

				if (MyServerSettings.BuyCommonChance())
				{
					Add(typeof(MysticalPearl), StoreSalesListConstants.SELL_PRICE_MYSTICAL_PEARL);
				}

				// Regular scrolls (dynamic pricing)
				AddRegularScrolls();

				// Magic staves by spell circle
				AddMagicStaves();

				// Special magical items
				if (MyServerSettings.BuyRareChance())
				{
					Add(typeof(MySpellbook), Utility.Random(StoreSalesListConstants.SELL_PRICE_MY_SPELLBOOK_MIN, StoreSalesListConstants.SELL_PRICE_MY_SPELLBOOK_MAX));
				}

				Add(typeof(TomeOfWands), Utility.Random(StoreSalesListConstants.SELL_PRICE_TOME_OF_WANDS_MIN, StoreSalesListConstants.SELL_PRICE_TOME_OF_WANDS_MAX));
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WizardStaff), StoreSalesListConstants.SELL_PRICE_WIZARD_STAFF);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WizardStick), StoreSalesListConstants.SELL_PRICE_WIZARD_STICK);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MageEye), StoreSalesListConstants.SELL_PRICE_MAGE_EYE);
			}

			/// <summary>
			/// Adds regular magic scrolls to the sell list with dynamic pricing.
			/// </summary>
			private void AddRegularScrolls()
			{
				Type[] types = Loot.RegularScrollTypes;

				for (int i = 0; i < types.Length; ++i)
				{
					if (MyServerSettings.BuyChance())
					{
						int price = (i / 8 + StoreSalesListConstants.SELL_PRICE_SCROLL_BASE) * StoreSalesListConstants.SELL_PRICE_SCROLL_MULTIPLIER;
						Add(types[i], price);
					}
				}
			}

			/// <summary>
			/// Adds magic staves to the sell list organized by spell circle.
			/// </summary>
			private void AddMagicStaves()
			{
				// 1st Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(ClumsyMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(CreateFoodMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(FeebleMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(HealMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(MagicArrowMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(NightSightMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(ReactiveArmorMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(WeaknessMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);

				// 2nd Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(AgilityMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(CunningMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(CureMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(HarmMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(MagicTrapMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(MagicUntrapMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(ProtectionMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(StrengthMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);

				// 3rd Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(BlessMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(FireballMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(MagicLockMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(MagicUnlockMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(PoisonMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(TelekinesisMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(TeleportMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(WallofStoneMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);

				// 4th Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(ArchCureMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(ArchProtectionMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(CurseMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(FireFieldMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(GreaterHealMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(LightningMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(ManaDrainMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(RecallMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);

				// 5th Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(BladeSpiritsMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(DispelFieldMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(IncognitoMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(MagicReflectionMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(MindBlastMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(ParalyzeMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(PoisonFieldMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(SummonCreatureMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);

				// 6th Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(DispelMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(EnergyBoltMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(ExplosionMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(InvisibilityMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(MarkMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(MassCurseMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(ParalyzeFieldMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(RevealMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);

				// 7th Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(ChainLightningMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(EnergyFieldMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(FlameStrikeMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(GateTravelMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(ManaVampireMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(MassDispelMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(MeteorSwarmMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(PolymorphMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);

				// 8th Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(AirElementalMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(EarthElementalMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(EarthquakeMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(EnergyVortexMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(FireElementalMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(ResurrectionMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(SummonDaemonMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(WaterElementalMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
			}

			/// <summary>
			/// Helper method to add a magic staff with rare chance and random pricing.
			/// </summary>
			private void AddMagicStaffWithRareChance(Type staffType, int minPrice, int maxPrice)
			{
				if (MyServerSettings.BuyRareChance())
				{
					Add(staffType, Utility.Random(minPrice, maxPrice));
				}
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBRuneCasting: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRuneCasting()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( RuneMagicBook ), 500 );
				Add( typeof( RuneBag ), 200 );
				Add( typeof( An ), 50 );
				Add( typeof( Bet ), 50 );
				Add( typeof( Corp ), 50 );
				Add( typeof( Des ), 50 );
				Add( typeof( Ex ), 50 );
				Add( typeof( Flam ), 50 );
				Add( typeof( Grav ), 50 );
				Add( typeof( Hur ), 50 );
				Add( typeof( In ), 50 );
				Add( typeof( Jux ), 50 );
				Add( typeof( Kal ), 50 );
				Add( typeof( Lor ), 50 );
				Add( typeof( Mani ), 50 );
				Add( typeof( Nox ), 50 );
				Add( typeof( Ort ), 50 );
				Add( typeof( Por ), 50 );
				Add( typeof( Quas ), 50 );
				Add( typeof( Rel ), 50 );
				Add( typeof( Sanct ), 50 );
				Add( typeof( Tym ), 50 );
				Add( typeof( Uus ), 50 );
				Add( typeof( Vas ), 50 );
				Add( typeof( Wis ), 50 );
				Add( typeof( Xen ), 50 );
				Add( typeof( Ylem ), 50 );
				Add( typeof( Zu ), 50 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBEnchanter : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBEnchanter()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ClumsyMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CreateFoodMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( FeebleMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HealMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( MagicArrowMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NightSightMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ReactiveArmorMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( WeaknessMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( AgilityMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CunningMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CureMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HarmMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( MagicTrapMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( MagicUntrapMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ProtectionMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( StrengthMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( BlessMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( FireballMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( MagicLockMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( MagicUnlockMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( PoisonMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( TelekinesisMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( TeleportMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( WallofStoneMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( ArchCureMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( ArchProtectionMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( CurseMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( FireFieldMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( GreaterHealMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( LightningMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( ManaDrainMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( RecallMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( BladeSpiritsMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( DispelFieldMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( IncognitoMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( MagicReflectionMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( MindBlastMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( ParalyzeMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( PoisonFieldMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( SummonCreatureMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( DispelMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( EnergyBoltMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( ExplosionMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( InvisibilityMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( MarkMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( MassCurseMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( ParalyzeFieldMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( RevealMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( ChainLightningMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( EnergyFieldMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( FlameStrikeMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( GateTravelMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( ManaVampireMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( MassDispelMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( MeteorSwarmMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( PolymorphMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( AirElementalMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( EarthElementalMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( EarthquakeMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( EnergyVortexMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( FireElementalMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( ResurrectionMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( SummonDaemonMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( WaterElementalMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for House Deed vendor.
	/// Currently empty - no items sold or bought.
	/// </summary>
	public class SBHouseDeed: SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBHouseDeed class.
		/// </summary>
		public SBHouseDeed()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// Currently empty - no items for sale.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// Currently empty - no items for sale.
			/// </summary>
			public InternalBuyInfo()
			{
				// Currently no items for sale
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// Currently empty - no items purchased.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// Currently empty - no items purchased.
			/// </summary>
			public InternalSellInfo()
			{
				// Currently no items purchased
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Inn Keeper vendor.
	/// Sells beverages, food, games, contracts, and various inn/household items.
	/// </summary>
	public class SBInnKeeper : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBInnKeeper class.
		/// </summary>
		public SBInnKeeper()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				AddBeverages();
				AddFood();
				AddBowlFood();
				AddFruit();
				AddHouseholdItems();
				AddGames();
				AddContracts();
			}

			/// <summary>
			/// Adds beverage items to the buy list.
			/// </summary>
			private void AddBeverages()
			{
				// Beverage bottles
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Ale, StoreSalesListConstants.PRICE_BEVERAGE_BOTTLE_ALE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_BEVERAGE_BOTTLE_ALE, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Wine, StoreSalesListConstants.PRICE_BEVERAGE_BOTTLE_WINE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_BEVERAGE_BOTTLE_WINE, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Liquor, StoreSalesListConstants.PRICE_BEVERAGE_BOTTLE_LIQUOR, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_BEVERAGE_BOTTLE_LIQUOR, StoreSalesListConstants.HUE_DEFAULT));
				}

				// Jugs and pitchers
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(Jug), BeverageType.Cider, StoreSalesListConstants.PRICE_JUG_CIDER, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_JUG_CIDER, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Milk, StoreSalesListConstants.PRICE_PITCHER_MILK, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PITCHER_MILK, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Ale, StoreSalesListConstants.PRICE_PITCHER_ALE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PITCHER_ALE, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Cider, StoreSalesListConstants.PRICE_PITCHER_CIDER, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PITCHER_CIDER, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Liquor, StoreSalesListConstants.PRICE_PITCHER_LIQUOR, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PITCHER_LIQUOR, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Wine, StoreSalesListConstants.PRICE_PITCHER_WINE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PITCHER_WINE, StoreSalesListConstants.HUE_DEFAULT));
				}

				// Water (always available)
				StoreSalesListHelper.AddBeverageBuyItem(this, typeof(Pitcher), BeverageType.Water, StoreSalesListConstants.PRICE_PITCHER_WATER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PITCHER_WATER);
			}

			/// <summary>
			/// Adds food items to the buy list.
			/// </summary>
			private void AddFood()
			{
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(BreadLoaf), StoreSalesListConstants.PRICE_BREAD_LOAF, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BREAD_LOAF);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CheeseWheel), StoreSalesListConstants.PRICE_CHEESE_WHEEL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_CHEESE_WHEEL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CookedBird), StoreSalesListConstants.PRICE_COOKED_BIRD, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_COOKED_BIRD);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LambLeg), StoreSalesListConstants.PRICE_LAMB_LEG, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LAMB_LEG);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ChickenLeg), StoreSalesListConstants.PRICE_CHICKEN_LEG, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_CHICKEN_LEG);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Ribs), StoreSalesListConstants.PRICE_RIBS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_RIBS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ApplePie), StoreSalesListConstants.PRICE_APPLE_PIE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_APPLE_PIE);
			}

			/// <summary>
			/// Adds bowl food items to the buy list.
			/// </summary>
			private void AddBowlFood()
			{
				// Wooden bowls
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfCarrots), StoreSalesListConstants.PRICE_WOODEN_BOWL_CARROTS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WOODEN_BOWL_CARROTS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfCorn), StoreSalesListConstants.PRICE_WOODEN_BOWL_CORN, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WOODEN_BOWL_CORN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfLettuce), StoreSalesListConstants.PRICE_WOODEN_BOWL_LETTUCE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WOODEN_BOWL_LETTUCE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfPeas), StoreSalesListConstants.PRICE_WOODEN_BOWL_PEAS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WOODEN_BOWL_PEAS);

				// Pewter bowls
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(EmptyPewterBowl), StoreSalesListConstants.PRICE_EMPTY_PEWTER_BOWL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_EMPTY_PEWTER_BOWL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfCorn), StoreSalesListConstants.PRICE_PEWTER_BOWL_CORN, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PEWTER_BOWL_CORN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfLettuce), StoreSalesListConstants.PRICE_PEWTER_BOWL_LETTUCE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PEWTER_BOWL_LETTUCE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfPeas), StoreSalesListConstants.PRICE_PEWTER_BOWL_PEAS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PEWTER_BOWL_PEAS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfFoodPotatos), StoreSalesListConstants.PRICE_PEWTER_BOWL_POTATOS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PEWTER_BOWL_POTATOS);

				// Soups
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfStew), StoreSalesListConstants.PRICE_WOODEN_BOWL_STEW, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WOODEN_BOWL_STEW);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfTomatoSoup), StoreSalesListConstants.PRICE_WOODEN_BOWL_TOMATO_SOUP, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WOODEN_BOWL_TOMATO_SOUP);
			}

			/// <summary>
			/// Adds fruit items to the buy list.
			/// </summary>
			private void AddFruit()
			{
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Peach), StoreSalesListConstants.PRICE_PEACH, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PEACH);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Pear), StoreSalesListConstants.PRICE_PEAR, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PEAR);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Grapes), StoreSalesListConstants.PRICE_GRAPES, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_GRAPES);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Apple), StoreSalesListConstants.PRICE_APPLE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_APPLE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Banana), StoreSalesListConstants.PRICE_BANANA, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BANANA);
			}

			/// <summary>
			/// Adds household items to the buy list.
			/// </summary>
			private void AddHouseholdItems()
			{
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Torch), StoreSalesListConstants.PRICE_TORCH, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_TORCH);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Candle), StoreSalesListConstants.PRICE_CANDLE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_CANDLE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Backpack), StoreSalesListConstants.PRICE_BACKPACK, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BACKPACK);
			}

			/// <summary>
			/// Adds game items to the buy list.
			/// </summary>
			private void AddGames()
			{
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(tarotpoker), StoreSalesListConstants.PRICE_TAROT_POKER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_TAROT_POKER);
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo("1016450", typeof(Chessboard), StoreSalesListConstants.PRICE_CHESSBOARD, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_CHESSBOARD, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo("1016449", typeof(CheckerBoard), StoreSalesListConstants.PRICE_CHECKER_BOARD, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_CHESSBOARD, StoreSalesListConstants.HUE_DEFAULT));
				}
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Backgammon), StoreSalesListConstants.PRICE_BACKGAMMON, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BACKGAMMON);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Dices), StoreSalesListConstants.PRICE_DICES, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_DICES);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Engines.Mahjong.MahjongGame), StoreSalesListConstants.PRICE_MAHJONG_GAME, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_MAHJONG_GAME);
			}

			/// <summary>
			/// Adds contract and henchman items to the buy list.
			/// </summary>
			private void AddContracts()
			{
				// Henchmen
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(HenchmanFighterItem), StoreSalesListConstants.PRICE_HENCHMAN_FIGHTER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_HENCHMAN_FIGHTER, StoreSalesListConstants.HUE_HENCHMAN_FIGHTER);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(HenchmanArcherItem), StoreSalesListConstants.PRICE_HENCHMAN_ARCHER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_HENCHMAN_ARCHER, StoreSalesListConstants.HUE_HENCHMAN_ARCHER);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(HenchmanWizardItem), StoreSalesListConstants.PRICE_HENCHMAN_WIZARD, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_HENCHMAN_WIZARD, StoreSalesListConstants.HUE_HENCHMAN_WIZARD);

				// Contracts
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo("1041243", typeof(ContractOfEmployment), StoreSalesListConstants.PRICE_CONTRACT_OF_EMPLOYMENT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_CONTRACT_OF_EMPLOYMENT, StoreSalesListConstants.HUE_DEFAULT));
				}
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo("a barkeep contract", typeof(BarkeepContract), StoreSalesListConstants.PRICE_BARKEEP_CONTRACT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_BARKEEP_CONTRACT, StoreSalesListConstants.HUE_DEFAULT));
				}

				// Vendor rental contract (conditional on NewVendorSystem)
				if (Multis.BaseHouse.NewVendorSystem && MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo("1062332", typeof(VendorRentalContract), StoreSalesListConstants.PRICE_VENDOR_RENTAL_CONTRACT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_VENDOR_RENTAL_CONTRACT, StoreSalesListConstants.HUE_VENDOR_RENTAL_CONTRACT));
				}
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Beverages
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BeverageBottle), StoreSalesListConstants.SELL_PRICE_BEVERAGE_BOTTLE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Jug), StoreSalesListConstants.SELL_PRICE_JUG);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Pitcher), StoreSalesListConstants.SELL_PRICE_PITCHER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GlassMug), StoreSalesListConstants.SELL_PRICE_GLASS_MUG);

				// Food
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BreadLoaf), StoreSalesListConstants.SELL_PRICE_BREAD_LOAF);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CheeseWheel), StoreSalesListConstants.SELL_PRICE_CHEESE_WHEEL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Ribs), StoreSalesListConstants.SELL_PRICE_RIBS);

				// Fruit
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Peach), StoreSalesListConstants.SELL_PRICE_PEACH);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Pear), StoreSalesListConstants.SELL_PRICE_PEAR);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Grapes), StoreSalesListConstants.SELL_PRICE_GRAPES);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Apple), StoreSalesListConstants.SELL_PRICE_APPLE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Banana), StoreSalesListConstants.SELL_PRICE_BANANA);

				// Household items
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Torch), StoreSalesListConstants.SELL_PRICE_TORCH);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Candle), StoreSalesListConstants.SELL_PRICE_CANDLE);

				// Games
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(tarotpoker), StoreSalesListConstants.SELL_PRICE_TAROT_POKER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MahjongGame), StoreSalesListConstants.SELL_PRICE_MAHJONG_GAME);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Chessboard), StoreSalesListConstants.SELL_PRICE_CHESSBOARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CheckerBoard), StoreSalesListConstants.SELL_PRICE_CHECKER_BOARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Backgammon), StoreSalesListConstants.SELL_PRICE_BACKGAMMON);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Dices), StoreSalesListConstants.SELL_PRICE_DICES);

				// Contracts
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ContractOfEmployment), StoreSalesListConstants.SELL_PRICE_CONTRACT_OF_EMPLOYMENT);

				// Bowl food
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfCarrots), StoreSalesListConstants.SELL_PRICE_WOODEN_BOWL_CARROTS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfCorn), StoreSalesListConstants.SELL_PRICE_WOODEN_BOWL_CORN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfLettuce), StoreSalesListConstants.SELL_PRICE_WOODEN_BOWL_LETTUCE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfPeas), StoreSalesListConstants.SELL_PRICE_WOODEN_BOWL_PEAS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(EmptyPewterBowl), StoreSalesListConstants.SELL_PRICE_EMPTY_PEWTER_BOWL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PewterBowlOfCorn), StoreSalesListConstants.SELL_PRICE_PEWTER_BOWL_CORN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PewterBowlOfLettuce), StoreSalesListConstants.SELL_PRICE_PEWTER_BOWL_LETTUCE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PewterBowlOfPeas), StoreSalesListConstants.SELL_PRICE_PEWTER_BOWL_PEAS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PewterBowlOfFoodPotatos), StoreSalesListConstants.SELL_PRICE_PEWTER_BOWL_POTATOS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfStew), StoreSalesListConstants.SELL_PRICE_WOODEN_BOWL_STEW);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBowlOfTomatoSoup), StoreSalesListConstants.SELL_PRICE_WOODEN_BOWL_TOMATO_SOUP);
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBJewel: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBJewel()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldRing ), 27, Utility.Random( 1,100 ), 0x4CFA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Necklace ), 26, Utility.Random( 1,100 ), 0x4CFE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldNecklace ), 27, Utility.Random( 1,100 ), 0x4CFF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldBeadNecklace ), 27, Utility.Random( 1,100 ), 0x4CFD, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Beads ), 27, Utility.Random( 1,100 ), 0x4CFE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldBracelet ), 27, Utility.Random( 1,100 ), 0x4CF1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldEarrings ), 27, Utility.Random( 1,100 ), 0x4CFB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1060740", typeof( BroadcastCrystal ),  68, Utility.Random( 1,100 ), 0x1ED0, 0, new object[] {  500 } ) ); } // 500 charges
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1060740", typeof( BroadcastCrystal ), 131, Utility.Random( 1,100 ), 0x1ED0, 0, new object[] { 1000 } ) ); } // 1000 charges
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1060740", typeof( BroadcastCrystal ), 256, Utility.Random( 1,100 ), 0x1ED0, 0, new object[] { 2000 } ) ); } // 2000 charges
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1060740", typeof( ReceiverCrystal ), 6, Utility.Random( 1,100 ), 0x1ED0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StarSapphire ), 125, Utility.Random( 1,100 ), 0xF21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Emerald ), 100, Utility.Random( 1,100 ), 0xF10, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Sapphire ), 100, Utility.Random( 1,100 ), 0xF19, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ruby ), 75, Utility.Random( 1,100 ), 0xF13, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Citrine ), 50, Utility.Random( 1,100 ), 0xF15, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Amethyst ), 100, Utility.Random( 1,100 ), 0xF16, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tourmaline ), 75, Utility.Random( 1,100 ), 0xF2D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Amber ), 50, Utility.Random( 1,100 ), 0xF25, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Diamond ), 200, Utility.Random( 1,100 ), 0xF26, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Crystals ), 5 );
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amber ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amethyst ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Citrine ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Diamond ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Emerald ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ruby ), 37 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sapphire ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphire ), 62 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tourmaline ), 47 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Krystal ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MageEye ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldRing ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverRing ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Necklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBeadNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBeadNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Beads ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBracelet ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBracelet ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldEarrings ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverEarrings ), 10 ); } 
				if ( MyServerSettings.SellChance() ){Add( typeof( MysticalPearl ), 500 ); } 
				if ( MyServerSettings.SellChance() ){Add( typeof( LargeCrystal ), Utility.Random( 500,1000 ) ); } 
				Add( typeof( MagicJewelryRing ), Utility.Random( 50,300 ) );
				Add( typeof( MagicJewelryCirclet ), Utility.Random( 50,300 ) );
				Add( typeof( MagicJewelryNecklace ), Utility.Random( 50,300 ) );
				Add( typeof( MagicJewelryEarrings ), Utility.Random( 50,300 ) );
				Add( typeof( MagicJewelryBracelet ), Utility.Random( 50,300 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( AgapiteAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AgapiteBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AgapiteRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AgapiteEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmberAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmberBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmberRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmberEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BrassAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BrassBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BrassRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BrassEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BronzeAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BronzeBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BronzeRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BronzeEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CaddelliteAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CaddelliteBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CaddelliteRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CaddelliteEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CopperAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CopperBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CopperRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CopperEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DiamondAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DiamondBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DiamondRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DiamondEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DullCopperAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DullCopperBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DullCopperRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DullCopperEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldenAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldenBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldenRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldenEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadeAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadeBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadeRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadeEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MithrilAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MithrilBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MithrilRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MithrilEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NepturiteAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NepturiteBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NepturiteRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NepturiteEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ObsidianAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ObsidianBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ObsidianRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ObsidianEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PearlAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PearlBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PearlRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PearlEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphireAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphireBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphireRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphireEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShadowIronAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShadowIronBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShadowIronRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShadowIronEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilveryAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilveryBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilveryRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilveryEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphireAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphireBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphireRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphireEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SteelAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SteelBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SteelRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SteelEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TourmalineAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TourmalineBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TourmalineRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TourmalineEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ValoriteAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ValoriteBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ValoriteRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ValoriteEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( VeriteAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( VeriteBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( VeriteRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( VeriteEarrings ), Utility.Random( 11,16 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBWarriorGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBWarriorGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "warhorse", typeof( Warhorse ), 250000, Utility.Random( 1,10 ), 0x55DC, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBKeeperOfChivalry : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBKeeperOfChivalry()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BookOfChivalry ), 140, Utility.Random( 1,100 ), 0x2252, 0 ) );
				Add( new GenericBuyInfo( "silver griffon", typeof( PaladinWarhorse ), 500000, Utility.Random( 1,10 ), 0x4C59, 0x99B ) );
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GwennoGraveAddonDeed ), Utility.Random( 5000,10000 ), 1, 0x14F0, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( MyPaladinbook ), Utility.Random( 50,200 ) ); } 
				Add( typeof( BookOfChivalry ), 70 );
				Add( typeof( HolyManSpellbook ), Utility.Random( 50,200 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for LeatherWorker vendor.
	/// Sells hides, leather, waterskins, and special rare skins.
	/// </summary>
	public class SBLeatherWorker : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBLeatherWorker class.
		/// </summary>
		public SBLeatherWorker()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Basic materials (chance-based)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Hides), StoreSalesListConstants.PRICE_HIDES_LEATHERWORKER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_HIDES);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Leather), StoreSalesListConstants.PRICE_LEATHER_LEATHERWORKER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LEATHER);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Waterskin), StoreSalesListConstants.PRICE_WATERSKIN_LEATHERWORKER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WATERSKIN);

				// Very rare special skins (chance-based, with MaterialInfo.GetMaterialColor)
				if (MyServerSettings.SellVeryRareChance()) { Add(new GenericBuyInfo(typeof(DemonSkin), StoreSalesListConstants.PRICE_DEMON_SKIN, Utility.Random(StoreSalesListConstants.QTY_VERY_RARE_SKIN_MIN, StoreSalesListConstants.QTY_VERY_RARE_SKIN_MAX), StoreSalesListConstants.ITEMID_LEATHER, Server.Misc.MaterialInfo.GetMaterialColor("demon skin", "", 0))); }
				if (MyServerSettings.SellVeryRareChance()) { Add(new GenericBuyInfo(typeof(DragonSkin), StoreSalesListConstants.PRICE_DRAGON_SKIN, Utility.Random(StoreSalesListConstants.QTY_VERY_RARE_SKIN_MIN, StoreSalesListConstants.QTY_VERY_RARE_SKIN_MAX), StoreSalesListConstants.ITEMID_LEATHER, Server.Misc.MaterialInfo.GetMaterialColor("dragon skin", "", 0))); }
				if (MyServerSettings.SellVeryRareChance()) { Add(new GenericBuyInfo(typeof(NightmareSkin), StoreSalesListConstants.PRICE_NIGHTMARE_SKIN, Utility.Random(StoreSalesListConstants.QTY_VERY_RARE_SKIN_MIN, StoreSalesListConstants.QTY_VERY_RARE_SKIN_MAX), StoreSalesListConstants.ITEMID_LEATHER, Server.Misc.MaterialInfo.GetMaterialColor("nightmare skin", "", 0))); }
				if (MyServerSettings.SellVeryRareChance()) { Add(new GenericBuyInfo(typeof(SerpentSkin), StoreSalesListConstants.PRICE_SERPENT_SKIN, Utility.Random(StoreSalesListConstants.QTY_VERY_RARE_SKIN_MIN, StoreSalesListConstants.QTY_VERY_RARE_SKIN_MAX), StoreSalesListConstants.ITEMID_LEATHER, Server.Misc.MaterialInfo.GetMaterialColor("serpent skin", "", 0))); }
				if (MyServerSettings.SellVeryRareChance()) { Add(new GenericBuyInfo(typeof(TrollSkin), StoreSalesListConstants.PRICE_TROLL_SKIN, Utility.Random(StoreSalesListConstants.QTY_VERY_RARE_SKIN_MIN, StoreSalesListConstants.QTY_VERY_RARE_SKIN_MAX), StoreSalesListConstants.ITEMID_LEATHER, Server.Misc.MaterialInfo.GetMaterialColor("troll skin", "", 0))); }
				if (MyServerSettings.SellVeryRareChance()) { Add(new GenericBuyInfo(typeof(UnicornSkin), StoreSalesListConstants.PRICE_UNICORN_SKIN, Utility.Random(StoreSalesListConstants.QTY_VERY_RARE_SKIN_MIN, StoreSalesListConstants.QTY_VERY_RARE_SKIN_MAX), StoreSalesListConstants.ITEMID_LEATHER, Server.Misc.MaterialInfo.GetMaterialColor("unicorn skin", "", 0))); }
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Equipment (chance-based)
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ThighBoots), StoreSalesListConstants.SELL_PRICE_THIGH_BOOTS);
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(MagicBoots), StoreSalesListConstants.SELL_PRICE_MAGIC_BOOTS);
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(MagicBelt), StoreSalesListConstants.SELL_PRICE_MAGIC_BELT);
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(MagicSash), StoreSalesListConstants.SELL_PRICE_MAGIC_SASH);
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(ThrowingGloves), StoreSalesListConstants.SELL_PRICE_THROWING_GLOVES);
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(PugilistGlove), StoreSalesListConstants.SELL_PRICE_PUGILIST_GLOVE);
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(PugilistGloves), StoreSalesListConstants.SELL_PRICE_PUGILIST_GLOVES);

				// Special skins (chance-based)
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(UnicornSkin), StoreSalesListConstants.SELL_PRICE_UNICORN_SKIN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(DemonSkin), StoreSalesListConstants.SELL_PRICE_DEMON_SKIN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(DragonSkin), StoreSalesListConstants.SELL_PRICE_DRAGON_SKIN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(NightmareSkin), StoreSalesListConstants.SELL_PRICE_NIGHTMARE_SKIN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SerpentSkin), StoreSalesListConstants.SELL_PRICE_SERPENT_SKIN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(TrollSkin), StoreSalesListConstants.SELL_PRICE_TROLL_SKIN);

				// Hides and leather (chance-based)
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Hides), StoreSalesListConstants.SELL_PRICE_HIDES);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SpinedHides), StoreSalesListConstants.SELL_PRICE_SPINED_HIDES);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(HornedHides), StoreSalesListConstants.SELL_PRICE_HORNED_HIDES);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BarbedHides), StoreSalesListConstants.SELL_PRICE_BARBED_HIDES);
				// TODO: Future implementation - Special hide types disabled
				//if ( MyServerSettings.BuyChance() ){Add( typeof( NecroticHides ), 4 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( VolcanicHides ), 5 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( FrozenHides ), 5 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( DraconicHides ), 6 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( HellishHides ), 7 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( DinosaurHides ), 7 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( AlienHides ), 7 ); }  

				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Leather), StoreSalesListConstants.SELL_PRICE_LEATHER);
				// TODO: Future implementation - Special leather types disabled from NPC sales
				//if ( MyServerSettings.BuyChance() ){Add( typeof( SpinedLeather ), 4 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( HornedLeather ), 4 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( BarbedLeather ), 5 ); }  
				// TODO: Future implementation - Special leather types disabled
				//if ( MyServerSettings.BuyChance() ){Add( typeof( NecroticLeather ), 5 ); }
				//if ( MyServerSettings.BuyChance() ){Add( typeof( VolcanicLeather ), 6 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( FrozenLeather ), 6 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( DraconicLeather ), 7 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( HellishLeather ), 8 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( DinosaurLeather ), 8 ); }  
				//if ( MyServerSettings.BuyChance() ){Add( typeof( AlienLeather ), 8 ); }  

				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Waterskin), StoreSalesListConstants.SELL_PRICE_WATERSKIN);
			}
		}

		#endregion
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Mapmaker vendor.
	/// Sells maps, mapmaking tools, and scrolls.
	/// </summary>
	public class SBMapmaker : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBMapmaker class.
		/// </summary>
		public SBMapmaker()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Maps and tools (chance-based and always available)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(BlankMap), StoreSalesListConstants.PRICE_BLANK_MAP, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BLANK_MAP);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(MapmakersPen), StoreSalesListConstants.PRICE_MAPMAKERS_PEN, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_MAPMAKERS_PEN);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(BlankScroll), StoreSalesListConstants.PRICE_BLANK_SCROLL_MAPMAKER, StoreSalesListConstants.QTY_BLANK_SCROLL_MAPMAKER_MIN, StoreSalesListConstants.QTY_BLANK_SCROLL_MAPMAKER_MAX, StoreSalesListConstants.ITEMID_BLANK_SCROLL_MAPMAKER);

				// Rare key (common chance)
				if (MyServerSettings.SellCommonChance()) { Add(new GenericBuyInfo(typeof(MasterSkeletonsKey), Utility.Random(StoreSalesListConstants.PRICE_MASTER_SKELETONS_KEY_MIN, StoreSalesListConstants.PRICE_MASTER_SKELETONS_KEY_MAX), StoreSalesListConstants.QTY_MASTER_SKELETONS_KEY, StoreSalesListConstants.ITEMID_MASTER_SKELETONS_KEY, 0)); }
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Basic maps and tools (chance-based, fixed prices)
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BlankScroll), StoreSalesListConstants.SELL_PRICE_BLANK_SCROLL_MAPMAKER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MapmakersPen), StoreSalesListConstants.SELL_PRICE_MAPMAKERS_PEN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BlankMap), StoreSalesListConstants.SELL_PRICE_BLANK_MAP);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CityMap), StoreSalesListConstants.SELL_PRICE_CITY_MAP);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LocalMap), StoreSalesListConstants.SELL_PRICE_LOCAL_MAP);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WorldMap), StoreSalesListConstants.SELL_PRICE_WORLD_MAP);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PresetMapEntry), StoreSalesListConstants.SELL_PRICE_PRESET_MAP_ENTRY);

				// World maps (chance-based, random prices)
				if (MyServerSettings.BuyChance()) { Add(typeof(WorldMapLodor), Utility.Random(StoreSalesListConstants.SELL_PRICE_WORLD_MAP_LODOR_MIN, StoreSalesListConstants.SELL_PRICE_WORLD_MAP_LODOR_MAX)); }
				if (MyServerSettings.BuyChance()) { Add(typeof(WorldMapSosaria), Utility.Random(StoreSalesListConstants.SELL_PRICE_WORLD_MAP_SOSARIA_MIN, StoreSalesListConstants.SELL_PRICE_WORLD_MAP_SOSARIA_MAX)); }
				if (MyServerSettings.BuyChance()) { Add(typeof(WorldMapBottle), Utility.Random(StoreSalesListConstants.SELL_PRICE_WORLD_MAP_BOTTLE_MIN, StoreSalesListConstants.SELL_PRICE_WORLD_MAP_BOTTLE_MAX)); }
				if (MyServerSettings.BuyChance()) { Add(typeof(WorldMapSerpent), Utility.Random(StoreSalesListConstants.SELL_PRICE_WORLD_MAP_SERPENT_MIN, StoreSalesListConstants.SELL_PRICE_WORLD_MAP_SERPENT_MAX)); }
				if (MyServerSettings.BuyChance()) { Add(typeof(WorldMapUmber), Utility.Random(StoreSalesListConstants.SELL_PRICE_WORLD_MAP_UMBER_MIN, StoreSalesListConstants.SELL_PRICE_WORLD_MAP_UMBER_MAX)); }
				if (MyServerSettings.BuyChance()) { Add(typeof(WorldMapAmbrosia), Utility.Random(StoreSalesListConstants.SELL_PRICE_WORLD_MAP_AMBROSIA_MIN, StoreSalesListConstants.SELL_PRICE_WORLD_MAP_AMBROSIA_MAX)); }
				if (MyServerSettings.BuyChance()) { Add(typeof(WorldMapIslesOfDread), Utility.Random(StoreSalesListConstants.SELL_PRICE_WORLD_MAP_ISLES_OF_DREAD_MIN, StoreSalesListConstants.SELL_PRICE_WORLD_MAP_ISLES_OF_DREAD_MAX)); }
				if (MyServerSettings.BuyChance()) { Add(typeof(WorldMapSavage), Utility.Random(StoreSalesListConstants.SELL_PRICE_WORLD_MAP_SAVAGE_MIN, StoreSalesListConstants.SELL_PRICE_WORLD_MAP_SAVAGE_MAX)); }
				if (MyServerSettings.BuyChance()) { Add(typeof(WorldMapUnderworld), Utility.Random(StoreSalesListConstants.SELL_PRICE_WORLD_MAP_UNDERWORLD_MIN, StoreSalesListConstants.SELL_PRICE_WORLD_MAP_UNDERWORLD_MAX)); }

				// Alternate reality map (always available, random price)
				Add(typeof(AlternateRealityMap), Utility.Random(StoreSalesListConstants.SELL_PRICE_ALTERNATE_REALITY_MAP_MIN, StoreSalesListConstants.SELL_PRICE_ALTERNATE_REALITY_MAP_MAX));
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Miller vendor.
	/// Sells flour and wheat products.
	/// </summary>
	public class SBMiller : SBInfo 
	{ 
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBMiller class.
		/// </summary>
		public SBMiller() 
		{ 
		} 

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo() 
			{ 
				// Miller products (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(SackFlour), StoreSalesListConstants.PRICE_SACK_FLOUR, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SACK_FLOUR);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(WheatSheaf), StoreSalesListConstants.PRICE_WHEAT_SHEAF, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WHEAT_SHEAF);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo 
		{ 
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo() 
			{ 
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SackFlour), StoreSalesListConstants.SELL_PRICE_SACK_FLOUR);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WheatSheaf), StoreSalesListConstants.SELL_PRICE_WHEAT_SHEAF);
			} 
		} 

		#endregion
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Miner vendor.
	/// Sells mining tools and equipment.
	/// </summary>
	public class SBMiner : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBMiner class.
		/// </summary>
		public SBMiner()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Equipment (chance-based)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Bag), StoreSalesListConstants.PRICE_BAG_MINER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BAG, StoreSalesListConstants.HUE_BAG);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Candle), StoreSalesListConstants.PRICE_CANDLE_MINER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_CANDLE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Torch), StoreSalesListConstants.PRICE_TORCH_MINER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_TORCH);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Lantern), StoreSalesListConstants.PRICE_LANTERN_MINER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LANTERN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Pickaxe), StoreSalesListConstants.PRICE_PICKAXE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PICKAXE);

				// Tools (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Shovel), StoreSalesListConstants.PRICE_SHOVEL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SHOVEL);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(OreShovel), StoreSalesListConstants.PRICE_ORE_SHOVEL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_ORE_SHOVEL, StoreSalesListConstants.HUE_ORE_SHOVEL);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Pickaxe), StoreSalesListConstants.SELL_PRICE_PICKAXE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Shovel), StoreSalesListConstants.SELL_PRICE_SHOVEL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(OreShovel), StoreSalesListConstants.SELL_PRICE_ORE_SHOVEL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Lantern), StoreSalesListConstants.SELL_PRICE_LANTERN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Torch), StoreSalesListConstants.SELL_PRICE_TORCH);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Bag), StoreSalesListConstants.SELL_PRICE_BAG);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Candle), StoreSalesListConstants.SELL_PRICE_CANDLE);
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Monk vendor.
	/// Sells monk robes and equipment.
	/// </summary>
	public class SBMonk : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBMonk class.
		/// </summary>
		public SBMonk()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Monk equipment (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(MonkRobe), StoreSalesListConstants.PRICE_MONK_ROBE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_MONK_ROBE, StoreSalesListConstants.HUE_MONK_ROBE);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// No items to buy from players
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBPlayerBarkeeper : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBPlayerBarkeeper()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Ale, 7, Utility.Random( 1,100 ), 0x99F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Wine, 7, Utility.Random( 1,100 ), 0x9C7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Liquor, 7, Utility.Random( 1,100 ), 0x99B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Jug ), BeverageType.Cider, 13, Utility.Random( 1,100 ), 0x9C8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Milk, 180, Utility.Random( 1,100 ), 0x9F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Ale, 11, Utility.Random( 1,100 ), 0x1F95, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Cider, 11, Utility.Random( 1,100 ), 0x1F97, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Liquor, 11, Utility.Random( 1,100 ), 0x1F99, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Wine, 11, Utility.Random( 1,100 ), 0x1F9B, 0 ) ); }
				if ( 1 > 0 ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Water, 11, Utility.Random( 1,100 ), 0x1F9D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( tarotpoker ), 5, Utility.Random( 1,100 ), 0x12AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016450", typeof( Chessboard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016449", typeof( CheckerBoard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backgammon ), 2, Utility.Random( 1,100 ), 0xE1C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dices ), 2, Utility.Random( 1,100 ), 0xFA7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Engines.Mahjong.MahjongGame ), 6, Utility.Random( 1,100 ), 0xFAA, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBProvisioner : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBProvisioner()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1060834", typeof( Engines.Plants.PlantBowl ), 2, Utility.Random( 1,100 ), 0x15FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Arrow ), 6, Utility.Random( 1,100 ), 0xF3F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bolt ), 7, Utility.Random( 1,100 ), 0x1BFB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backpack ), 100, Utility.Random( 1,100 ), 0x53D5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pouch ), 6, Utility.Random( 1,100 ), 0xE79, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bag ), 6, Utility.Random( 1,100 ), 0xE76, 0xABE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Candle ), 6, Utility.Random( 1,100 ), 0xA28, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Torch ), 8, Utility.Random( 1,100 ), 0xF6B, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){Add( new GenericBuyInfo( typeof( TenFootPole ), Utility.Random( 500,1000 ), Utility.Random( 1,100 ), 0xE8A, 0x972 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lantern ), 2, Utility.Random( 1,100 ), 0xA25, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lockpick ), 12, Utility.Random( 1,100 ), 0x14FC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FloppyHat ), 7, Utility.Random( 1,100 ), 0x1713, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WideBrimHat ), 8, Utility.Random( 1,100 ), 0x1714, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cap ), 10, Utility.Random( 1,100 ), 0x1715, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TallStrawHat ), 8, Utility.Random( 1,100 ), 0x1716, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StrawHat ), 7, Utility.Random( 1,100 ), 0x1717, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardsHat ), 11, Utility.Random( 1,100 ), 0x1718, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WitchHat ), 11, Utility.Random( 1,100 ), 0x2FC3, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherCap ), 10, Utility.Random( 1,100 ), 0x1DB9, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FeatheredHat ), 10, Utility.Random( 1,100 ), 0x171A, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TricorneHat ), 8, Utility.Random( 1,100 ), 0x171B, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PirateHat ), 8, Utility.Random( 1,100 ), 0x2FBC, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bandana ), 6, Utility.Random( 1,100 ), 0x1540, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SkullCap ), 7, Utility.Random( 1,100 ), 0x1544, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ThrowingWeapon ), 2, Utility.Random( 20, 120 ), 0x52B2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BreadLoaf ), 60, Utility.Random( 1,100 ), 0x103B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LambLeg ), 80, Utility.Random( 1,100 ), 0x160A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChickenLeg ), 100, Utility.Random( 1,100 ), 0x1608, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CookedBird ), 170, Utility.Random( 1,100 ), 0x9B7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Ale, 70, Utility.Random( 1,100 ), 0x99F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Wine, 70, Utility.Random( 1,100 ), 0x9C7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Liquor, 70, Utility.Random( 1,100 ), 0x99B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Jug ), BeverageType.Cider, 130, Utility.Random( 1,100 ), 0x9C8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pear ), 30, Utility.Random( 1,100 ), 0x994, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Apple ), 30, Utility.Random( 1,100 ), 0x9D0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bottle ), 5, Utility.Random( 1,100 ), 0xF0E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Waterskin ), 5, Utility.Random( 1,100 ), 0xA21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RedBook ), 100, Utility.Random( 1,100 ), 0xFF1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlueBook ), 100, Utility.Random( 1,100 ), 0xFF2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TanBook ), 100, Utility.Random( 1,100 ), 0xFF0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBox ), 14, Utility.Random( 1,100 ), 0xE7D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Key ), 2, Utility.Random( 1,100 ), 0x100E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MerchantCrate ), 500, 1, 0xE3D, 0x83F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bedroll ), 5, Utility.Random( 1,100 ), 0xA59, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmallTent ), 200, Utility.Random( 1,5 ), 0x1914, Utility.RandomList( 0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E ) ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CampersTent ), 500, Utility.Random( 1,5 ), 0x0A59, Utility.RandomList( 0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E ) ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Kindling ), 2, Utility.Random( 1,100 ), 0xDE1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( tarotpoker ), 5, Utility.Random( 1,100 ), 0x12AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016450", typeof( Chessboard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016449", typeof( CheckerBoard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backgammon ), 2, Utility.Random( 1,100 ), 0xE1C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Engines.Mahjong.MahjongGame ), 6, Utility.Random( 1,100 ), 0xFAA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dices ), 2, Utility.Random( 1,100 ), 0xFA7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmallBagBall ), 3, Utility.Random( 1,100 ), 0x2256, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LargeBagBall ), 3, Utility.Random( 1,100 ), 0x2257, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BagOfNecroReagents ), 2000, 25, 0xE76, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BagOfReagents ), 2000, 25, 0xE76, 0 ) ); }
			

				if( !Guild.NewGuildSystem )
					if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041055", typeof( GuildDeed ), 12450, Utility.Random( 1,100 ), 0x14F0, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( IvoryTusk ), Utility.Random( 50,250 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MerchantCrate ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmallTent ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CampersTent ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Arrow ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bolt ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backpack ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pouch ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bag ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Candle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Torch ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lantern ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lockpick ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FloppyHat ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WideBrimHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cap ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TallStrawHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StrawHat ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WizardsHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WitchHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherCap ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FeatheredHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TricorneHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PirateHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bandana ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullCap ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ThrowingWeapon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Waterskin ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bottle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Jar ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RedBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlueBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TanBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBox ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Kindling ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( tarotpoker ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MahjongGame ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Chessboard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheckerBoard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backgammon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dices ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amber ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amethyst ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Citrine ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Diamond ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Emerald ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ruby ), 37 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sapphire ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphire ), 62 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tourmaline ), 47 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldRing ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverRing ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Necklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBeadNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBeadNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Beads ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBracelet ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBracelet ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldEarrings ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverEarrings ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryRing ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryCirclet ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryNecklace ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryEarrings ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryBracelet ), Utility.Random( 50,300 ) ); } 

				if( !Guild.NewGuildSystem )
					if ( MyServerSettings.BuyChance() ){Add( typeof( GuildDeed ), 6225 ); } 
			}
		}
	}
///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBasement : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBasement()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1060834", typeof( Engines.Plants.PlantBowl ), 2, Utility.Random( 1,100 ), 0x15FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Arrow ), 3, Utility.Random( 50,100 ), 0xF3F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bolt ), 3, Utility.Random( 50,100 ), 0x1BFB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backpack ), 100, Utility.Random( 1,100 ), 0x53D5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pouch ), 6, Utility.Random( 1,100 ), 0xE79, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bag ), 6, Utility.Random( 1,100 ), 0xE76, 0xABE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Candle ), 6, Utility.Random( 1,100 ), 0xA28, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Torch ), 8, Utility.Random( 1,100 ), 0xF6B, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){Add( new GenericBuyInfo( typeof( TenFootPole ), Utility.Random( 500,1000 ), Utility.Random( 1,100 ), 0xE8A, 0x972 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lantern ), 2, Utility.Random( 1,100 ), 0xA25, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lockpick ), 12, Utility.Random( 1,100 ), 0x14FC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FloppyHat ), 7, Utility.Random( 1,100 ), 0x1713, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WideBrimHat ), 8, Utility.Random( 1,100 ), 0x1714, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cap ), 10, Utility.Random( 1,100 ), 0x1715, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TallStrawHat ), 8, Utility.Random( 1,100 ), 0x1716, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StrawHat ), 7, Utility.Random( 1,100 ), 0x1717, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardsHat ), 11, Utility.Random( 1,100 ), 0x1718, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WitchHat ), 11, Utility.Random( 1,100 ), 0x2FC3, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherCap ), 10, Utility.Random( 1,100 ), 0x1DB9, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FeatheredHat ), 10, Utility.Random( 1,100 ), 0x171A, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TricorneHat ), 8, Utility.Random( 1,100 ), 0x171B, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PirateHat ), 8, Utility.Random( 1,100 ), 0x2FBC, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bandana ), 6, Utility.Random( 1,100 ), 0x1540, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SkullCap ), 7, Utility.Random( 1,100 ), 0x1544, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ThrowingWeapon ), 2, Utility.Random( 20, 120 ), 0x52B2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BreadLoaf ), 60, Utility.Random( 1,100 ), 0x103B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LambLeg ), 80, Utility.Random( 1,100 ), 0x160A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChickenLeg ), 50, Utility.Random( 1,100 ), 0x1608, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CookedBird ), 170, Utility.Random( 1,100 ), 0x9B7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Ale, 70, Utility.Random( 1,100 ), 0x99F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Wine, 70, Utility.Random( 1,100 ), 0x9C7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Liquor, 70, Utility.Random( 1,100 ), 0x99B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Jug ), BeverageType.Cider, 130, Utility.Random( 1,100 ), 0x9C8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pear ), 30, Utility.Random( 1,100 ), 0x994, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Apple ), 30, Utility.Random( 1,100 ), 0x9D0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bottle ), 5, Utility.Random( 100,1000 ), 0xF0E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Waterskin ), 5, Utility.Random( 1,100 ), 0xA21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RedBook ), 100, Utility.Random( 1,100 ), 0xFF1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlueBook ), 100, Utility.Random( 1,100 ), 0xFF2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TanBook ), 100, Utility.Random( 1,100 ), 0xFF0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBox ), 14, Utility.Random( 1,100 ), 0xE7D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Key ), 2, Utility.Random( 1,100 ), 0x100E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MerchantCrate ), 500, 1, 0xE3D, 0x83F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bedroll ), 5, Utility.Random( 1,100 ), 0xA59, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmallTent ), 200, Utility.Random( 1,5 ), 0x1914, Utility.RandomList( 0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E ) ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CampersTent ), 500, Utility.Random( 1,5 ), 0x0A59, Utility.RandomList( 0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E ) ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Kindling ), 2, Utility.Random( 1,100 ), 0xDE1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( tarotpoker ), 5, Utility.Random( 1,100 ), 0x12AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016450", typeof( Chessboard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016449", typeof( CheckerBoard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backgammon ), 2, Utility.Random( 1,100 ), 0xE1C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Engines.Mahjong.MahjongGame ), 6, Utility.Random( 1,100 ), 0xFAA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dices ), 2, Utility.Random( 1,100 ), 0xFA7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmallBagBall ), 3, Utility.Random( 1,100 ), 0x2256, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LargeBagBall ), 3, Utility.Random( 1,100 ), 0x2257, 0 ) ); }
				Add( new GenericBuyInfo( typeof( BagOfNecroReagents ), 500, 10, 0xE76, 0 ) ); 
				Add( new GenericBuyInfo( typeof( BagOfReagents ), 500, 10, 0xE76, 0 ) ); 
				Add( new GenericBuyInfo( typeof( SBArmorDeed ), 800, 100, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Lute ), 21, Utility.Random( 10,20 ), 0x0EB3, 0 ) ); 
				
			
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( IvoryTusk ), Utility.Random( 50,250 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MerchantCrate ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmallTent ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CampersTent ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Arrow ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bolt ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backpack ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pouch ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bag ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Candle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Torch ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lantern ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lockpick ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FloppyHat ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WideBrimHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cap ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TallStrawHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StrawHat ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WizardsHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WitchHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherCap ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FeatheredHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TricorneHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PirateHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bandana ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullCap ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ThrowingWeapon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Waterskin ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bottle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Jar ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RedBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlueBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TanBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBox ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Kindling ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( tarotpoker ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MahjongGame ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Chessboard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheckerBoard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backgammon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dices ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amber ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amethyst ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Citrine ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Diamond ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Emerald ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ruby ), 37 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sapphire ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphire ), 62 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tourmaline ), 47 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldRing ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverRing ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Necklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBeadNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBeadNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Beads ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBracelet ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBracelet ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldEarrings ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverEarrings ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryRing ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryCirclet ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryNecklace ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryEarrings ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryBracelet ), Utility.Random( 50,300 ) ); } 

			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Rancher vendor.
	/// Sells pack animals.
	/// </summary>
	public class SBRancher : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBRancher class.
		/// </summary>
		public SBRancher()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Pack animals (always available)
				Add(new AnimalBuyInfo(1, typeof(PackHorse), StoreSalesListConstants.PRICE_PACK_HORSE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PACK_HORSE, 0));
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// No items to buy from players
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Ranger vendor.
	/// Sells animals, ranged weapons, ammunition, ranger armor, tents, and trap kits.
	/// </summary>
	public class SBRanger : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBRanger class.
		/// </summary>
		public SBRanger()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Animals (with chance)
				if (MyServerSettings.SellChance())
				{
					Add(new AnimalBuyInfo(1, typeof(Cat), StoreSalesListConstants.PRICE_CAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_CAT, StoreSalesListConstants.HUE_DEFAULT));
				}

				// Dog (always available)
				Add(new AnimalBuyInfo(1, typeof(Dog), StoreSalesListConstants.PRICE_DOG, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_DOG, StoreSalesListConstants.HUE_DEFAULT));

				if (MyServerSettings.SellChance())
				{
					Add(new AnimalBuyInfo(1, typeof(PackLlama), StoreSalesListConstants.PRICE_PACK_LLAMA, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PACK_LLAMA, StoreSalesListConstants.HUE_DEFAULT));
				}

				if (MyServerSettings.SellChance())
				{
					Add(new AnimalBuyInfo(1, typeof(PackHorse), StoreSalesListConstants.PRICE_PACK_HORSE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PACK_HORSE, StoreSalesListConstants.HUE_DEFAULT));
				}

				// PackMule (very rare chance)
				if (MyServerSettings.SellVeryRareChance())
				{
					Add(new AnimalBuyInfo(5, typeof(PackMule), StoreSalesListConstants.PRICE_PACK_MULE, 1, StoreSalesListConstants.ITEMID_PACK_MULE, StoreSalesListConstants.HUE_DEFAULT));
				}

				// Medical supplies (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Bandage), StoreSalesListConstants.PRICE_BANDAGE, StoreSalesListConstants.QTY_BANDAGE_MIN, StoreSalesListConstants.QTY_BANDAGE_MAX, StoreSalesListConstants.ITEMID_BANDAGE);

				// Ranged weapons (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Crossbow), StoreSalesListConstants.PRICE_CROSSBOW, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_CROSSBOW);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(HeavyCrossbow), StoreSalesListConstants.PRICE_HEAVY_CROSSBOW, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_HEAVY_CROSSBOW);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RepeatingCrossbow), StoreSalesListConstants.PRICE_REPEATING_CROSSBOW, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_REPEATING_CROSSBOW);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CompositeBow), StoreSalesListConstants.PRICE_COMPOSITE_BOW, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_COMPOSITE_BOW);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Bow), StoreSalesListConstants.PRICE_BOW, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BOW);

				// Ammunition (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Bolt), StoreSalesListConstants.PRICE_BOLT, StoreSalesListConstants.QTY_AMMUNITION_MIN, StoreSalesListConstants.QTY_AMMUNITION_MAX, StoreSalesListConstants.ITEMID_BOLT);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Arrow), StoreSalesListConstants.PRICE_ARROW, StoreSalesListConstants.QTY_AMMUNITION_MIN, StoreSalesListConstants.QTY_AMMUNITION_MAX, StoreSalesListConstants.ITEMID_ARROW);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Feather), StoreSalesListConstants.PRICE_FEATHER, StoreSalesListConstants.QTY_AMMUNITION_MIN, StoreSalesListConstants.QTY_AMMUNITION_MAX, StoreSalesListConstants.ITEMID_FEATHER);

				// Shaft (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Shaft), StoreSalesListConstants.PRICE_SHAFT, StoreSalesListConstants.QTY_AMMUNITION_MIN, StoreSalesListConstants.QTY_AMMUNITION_MAX, StoreSalesListConstants.ITEMID_SHAFT);

				// Quivers (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ArcherQuiver), StoreSalesListConstants.PRICE_ARCHER_QUIVER, StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX, StoreSalesListConstants.ITEMID_ARCHER_QUIVER);

				// Ranger armor (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RangerArms), StoreSalesListConstants.PRICE_RANGER_ARMS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_RANGER_ARMS, StoreSalesListConstants.HUE_RANGER_ARMOR);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RangerChest), StoreSalesListConstants.PRICE_RANGER_CHEST, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_RANGER_CHEST, StoreSalesListConstants.HUE_RANGER_ARMOR);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RangerGloves), StoreSalesListConstants.PRICE_RANGER_GLOVES, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_RANGER_GLOVES, StoreSalesListConstants.HUE_RANGER_ARMOR);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RangerGorget), StoreSalesListConstants.PRICE_RANGER_GORGET, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_RANGER_GORGET, StoreSalesListConstants.HUE_RANGER_ARMOR);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RangerLegs), StoreSalesListConstants.PRICE_RANGER_LEGS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_RANGER_LEGS, StoreSalesListConstants.HUE_RANGER_ARMOR);

				// Tents (with chance, random hue)
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo(typeof(SmallTent), StoreSalesListConstants.PRICE_SMALL_TENT, Utility.Random(StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX), StoreSalesListConstants.ITEMID_SMALL_TENT, Utility.RandomList(0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E)));
				}

				if (MyServerSettings.SellCommonChance())
				{
					Add(new GenericBuyInfo(typeof(CampersTent), StoreSalesListConstants.PRICE_CAMPERS_TENT, Utility.Random(StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX), StoreSalesListConstants.ITEMID_CAMPERS_TENT, Utility.RandomList(0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E)));
				}

				// Tent addon deeds (rare chance, random dyed hue)
				if (MyServerSettings.SellRareChance())
				{
					Add(new GenericBuyInfo(typeof(MyTentEastAddonDeed), StoreSalesListConstants.PRICE_MY_TENT_EAST_ADDON_DEED, 1, StoreSalesListConstants.ITEMID_MY_TENT_EAST_ADDON_DEED, Utility.RandomDyedHue()));
				}

				if (MyServerSettings.SellRareChance())
				{
					Add(new GenericBuyInfo(typeof(MyTentSouthAddonDeed), StoreSalesListConstants.PRICE_MY_TENT_SOUTH_ADDON_DEED, 1, StoreSalesListConstants.ITEMID_MY_TENT_SOUTH_ADDON_DEED, Utility.RandomDyedHue()));
				}

				// Trap kits (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(TrapKit), StoreSalesListConstants.PRICE_TRAP_KIT, StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX, StoreSalesListConstants.ITEMID_TRAP_KIT);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Tents and addon deeds
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MyTentEastAddonDeed), StoreSalesListConstants.SELL_PRICE_MY_TENT_EAST_ADDON_DEED);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MyTentSouthAddonDeed), StoreSalesListConstants.SELL_PRICE_MY_TENT_SOUTH_ADDON_DEED);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SmallTent), StoreSalesListConstants.SELL_PRICE_SMALL_TENT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CampersTent), StoreSalesListConstants.SELL_PRICE_CAMPERS_TENT);

				// Ranged weapons
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Crossbow), StoreSalesListConstants.SELL_PRICE_CROSSBOW);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(HeavyCrossbow), StoreSalesListConstants.SELL_PRICE_HEAVY_CROSSBOW);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RepeatingCrossbow), StoreSalesListConstants.SELL_PRICE_REPEATING_CROSSBOW);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CompositeBow), StoreSalesListConstants.SELL_PRICE_COMPOSITE_BOW);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Bow), StoreSalesListConstants.SELL_PRICE_BOW);

				// Ammunition
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Bolt), StoreSalesListConstants.SELL_PRICE_BOLT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Arrow), StoreSalesListConstants.SELL_PRICE_ARROW);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Feather), StoreSalesListConstants.SELL_PRICE_FEATHER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Shaft), StoreSalesListConstants.SELL_PRICE_SHAFT);

				// Quivers
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ArcherQuiver), StoreSalesListConstants.SELL_PRICE_ARCHER_QUIVER);

				// Ranger armor
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RangerArms), StoreSalesListConstants.SELL_PRICE_RANGER_ARMS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RangerChest), StoreSalesListConstants.SELL_PRICE_RANGER_CHEST);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RangerGloves), StoreSalesListConstants.SELL_PRICE_RANGER_GLOVES);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RangerLegs), StoreSalesListConstants.SELL_PRICE_RANGER_LEGS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RangerGorget), StoreSalesListConstants.SELL_PRICE_RANGER_GORGET);

				// Trap kits
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(TrapKit), StoreSalesListConstants.SELL_PRICE_TRAP_KIT);
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBRealEstateBroker : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRealEstateBroker()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( InteriorDecorator ), 1000, Utility.Random( 1,100 ), 0x1EBA, 0 ) );
				Add( new GenericBuyInfo( typeof( HousePlacementTool ), 500, Utility.Random( 1,100 ), 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "house teleporter", typeof( PlayersHouseTeleporter ), 25000, Utility.Random( 1,10 ), 0x181D, 0 ) );
				Add( new GenericBuyInfo( "house high teleporter", typeof( PlayersZTeleporter ), 15000, Utility.Random( 1,10 ), 0x181D, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlankScroll ), 5, Utility.Random( 1,100 ), 0x0E34, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ScribesPen ), 8,  Utility.Random( 1,100 ), 0x2051, 0 ) ); }
				Add( new GenericBuyInfo( typeof( house_sign_sign_post_a ), 500, Utility.Random( 1,100 ), 2967, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_post_b ), 500, Utility.Random( 1,100 ), 2970, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_merc ), 1000, Utility.Random( 1,100 ), 3082, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_armor ), 1000, Utility.Random( 1,100 ), 3008, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bake ), 1000, Utility.Random( 1,100 ), 2980, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bank ), 1000, Utility.Random( 1,100 ), 3084, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bard ), 1000, Utility.Random( 1,100 ), 3004, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_smith ), 1000, Utility.Random( 1,100 ), 3016, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bow ), 1000, Utility.Random( 1,100 ), 3022, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_ship ), 1000, Utility.Random( 1,100 ), 2998, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_fletch ), 1000, Utility.Random( 1,100 ), 3006, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_heal ), 1000, Utility.Random( 1,100 ), 2988, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_inn ), 1000, Utility.Random( 1,100 ), 2996, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_gem ), 1000, Utility.Random( 1,100 ), 3010, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_book ), 1000, Utility.Random( 1,100 ), 2966, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_mage ), 1000, Utility.Random( 1,100 ), 2990, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_necro ), 1000, Utility.Random( 1,100 ), 2811, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_supply ), 1000, Utility.Random( 1,100 ), 3020, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_herb ), 1000, Utility.Random( 1,100 ), 3014, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_pen ), 1000, Utility.Random( 1,100 ), 3000, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_sew ), 1000, Utility.Random( 1,100 ), 2982, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_tavern ), 1000, Utility.Random( 1,100 ), 3012, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_tinker ), 1000, Utility.Random( 1,100 ), 2984, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_wood ), 1000, Utility.Random( 1,100 ), 2992, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( ScribesPen ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlankScroll ), 2 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Scribe vendor.
	/// Sells writing supplies, books, magical tomes, and related scholarly items.
	/// </summary>
	public class SBScribe: SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBScribe class.
		/// </summary>
		public SBScribe()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Writing supplies (with chance)
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo(typeof(ScribesPen), StoreSalesListConstants.PRICE_SCRIBES_PEN, Utility.Random(StoreSalesListConstants.QTY_BOOKS_MIN, StoreSalesListConstants.QTY_BOOKS_MAX), StoreSalesListConstants.ITEMID_SCRIBES_PEN, StoreSalesListConstants.HUE_DEFAULT));
					Add(new GenericBuyInfo(typeof(BlankScroll), StoreSalesListConstants.PRICE_BLANK_SCROLL_SCRIBE, Utility.Random(StoreSalesListConstants.QTY_BOOKS_MIN, StoreSalesListConstants.QTY_BOOKS_MAX), StoreSalesListConstants.ITEMID_BLANK_SCROLL_SCRIBE, StoreSalesListConstants.HUE_DEFAULT));
					Add(new GenericBuyInfo(typeof(Monocle), StoreSalesListConstants.PRICE_MONOCLE, Utility.Random(StoreSalesListConstants.QTY_MONOCLE_MIN, StoreSalesListConstants.QTY_MONOCLE_MAX), StoreSalesListConstants.ITEMID_MONOCLE, StoreSalesListConstants.HUE_DEFAULT));
				}

				// Books (with chance)
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo(typeof(BrownBook), StoreSalesListConstants.PRICE_BROWN_BOOK, Utility.Random(StoreSalesListConstants.QTY_BOOKS_MIN, StoreSalesListConstants.QTY_BOOKS_MAX), StoreSalesListConstants.ITEMID_BROWN_BOOK, StoreSalesListConstants.HUE_DEFAULT));
					Add(new GenericBuyInfo(typeof(TanBook), StoreSalesListConstants.PRICE_TAN_BOOK, Utility.Random(StoreSalesListConstants.QTY_BOOKS_MIN, StoreSalesListConstants.QTY_BOOKS_MAX), StoreSalesListConstants.ITEMID_TAN_BOOK, StoreSalesListConstants.HUE_DEFAULT));
					Add(new GenericBuyInfo(typeof(BlueBook), StoreSalesListConstants.PRICE_BLUE_BOOK, Utility.Random(StoreSalesListConstants.QTY_BOOKS_MIN, StoreSalesListConstants.QTY_BOOKS_MAX), StoreSalesListConstants.ITEMID_BLUE_BOOK, StoreSalesListConstants.HUE_DEFAULT));
				}

				// Rare items (with rare chance)
				if (MyServerSettings.SellRareChance())
				{
					Add(new GenericBuyInfo("1041267", typeof(Runebook), StoreSalesListConstants.PRICE_RUNEBOOK, Utility.Random(StoreSalesListConstants.QTY_RUNEBOOK_MIN, StoreSalesListConstants.QTY_RUNEBOOK_MAX), StoreSalesListConstants.ITEMID_RUNEBOOK, StoreSalesListConstants.HUE_DEFAULT));
				}

				// Mailbox (with chance)
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo(typeof(Mailbox), StoreSalesListConstants.PRICE_MAILBOX, Utility.Random(StoreSalesListConstants.QTY_MAILBOX_MIN, StoreSalesListConstants.QTY_MAILBOX_MAX), StoreSalesListConstants.ITEMID_MAILBOX, StoreSalesListConstants.HUE_DEFAULT));
				}
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Writing supplies
				if (MyServerSettings.BuyChance()) { Add(typeof(ScribesPen), StoreSalesListConstants.SELL_PRICE_SCRIBES_PEN); }
				if (MyServerSettings.BuyChance()) { Add(typeof(Monocle), StoreSalesListConstants.SELL_PRICE_MONOCLE); }
				if (MyServerSettings.BuyChance()) { Add(typeof(BrownBook), StoreSalesListConstants.SELL_PRICE_BROWN_BOOK); }
				if (MyServerSettings.BuyChance()) { Add(typeof(TanBook), StoreSalesListConstants.SELL_PRICE_TAN_BOOK); }
				if (MyServerSettings.BuyChance()) { Add(typeof(BlueBook), StoreSalesListConstants.SELL_PRICE_BLUE_BOOK); }
				if (MyServerSettings.BuyChance()) { Add(typeof(BlankScroll), StoreSalesListConstants.SELL_PRICE_BLANK_SCROLL_SCRIBE_SELL); }

				// Books and magical tomes
				Add(typeof(JokeBook), Utility.Random(StoreSalesListConstants.SELL_PRICE_JOKE_BOOK_MIN, StoreSalesListConstants.SELL_PRICE_JOKE_BOOK_MAX));
				if (MyServerSettings.BuyChance()) { Add(typeof(DynamicBook), Utility.Random(StoreSalesListConstants.SELL_PRICE_DYNAMIC_BOOK_MIN, StoreSalesListConstants.SELL_PRICE_DYNAMIC_BOOK_MAX)); }
				if (MyServerSettings.BuyChance()) { Add(typeof(DataPad), Utility.Random(StoreSalesListConstants.SELL_PRICE_DATA_PAD_MIN, StoreSalesListConstants.SELL_PRICE_DATA_PAD_MAX)); }

				// Spellbooks
				if (MyServerSettings.BuyChance()) { Add(typeof(NecromancerSpellbook), StoreSalesListConstants.SELL_PRICE_NECROMANCER_SPELLBOOK_SCRIBE); }
				if (MyServerSettings.BuyChance()) { Add(typeof(BookOfBushido), StoreSalesListConstants.SELL_PRICE_BOOK_OF_BUSHIDO); }
				if (MyServerSettings.BuyChance()) { Add(typeof(BookOfNinjitsu), StoreSalesListConstants.SELL_PRICE_BOOK_OF_NINJITSU); }
				if (MyServerSettings.BuyChance()) { Add(typeof(MysticSpellbook), StoreSalesListConstants.SELL_PRICE_MYSTIC_SPELLBOOK); }
				if (MyServerSettings.BuyChance()) { Add(typeof(DeathKnightSpellbook), Utility.Random(StoreSalesListConstants.SELL_PRICE_DEATH_KNIGHT_SPELLBOOK_SCRIBE_MIN, StoreSalesListConstants.SELL_PRICE_DEATH_KNIGHT_SPELLBOOK_SCRIBE_MAX)); }
				if (MyServerSettings.BuyChance()) { Add(typeof(Runebook), Utility.Random(StoreSalesListConstants.SELL_PRICE_RUNEBOOK_SCRIBE_MIN, StoreSalesListConstants.SELL_PRICE_RUNEBOOK_SCRIBE_MAX)); }
				if (MyServerSettings.BuyChance()) { Add(typeof(BookOfChivalry), StoreSalesListConstants.SELL_PRICE_BOOK_OF_CHIVALRY); }
				if (MyServerSettings.BuyChance()) { Add(typeof(BookOfChivalry), StoreSalesListConstants.SELL_PRICE_BOOK_OF_CHIVALRY); }
				if (MyServerSettings.BuyChance()) { Add(typeof(HolyManSpellbook), Utility.Random(StoreSalesListConstants.SELL_PRICE_HOLY_MAN_SPELLBOOK_SCRIBE_MIN, StoreSalesListConstants.SELL_PRICE_HOLY_MAN_SPELLBOOK_SCRIBE_MAX)); }
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class NMS_SBMuseumGuide : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public NMS_SBMuseumGuide()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add(new GenericBuyInfo(typeof(LoreGuidetoAdventure), 5, Utility.Random(5, 100), 0x1C11, 0));
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
    public class NMS_SBMinerBSMuseumGuide : SBInfo
    {
        private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public NMS_SBMinerBSMuseumGuide()
        {
        }

        public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                // Books (use item's own name)
                Add(new GenericBuyInfo(typeof(LoreGuidetoAdventure), 5, Utility.Random(5, 100), 0x1C11, 0));
                Add(new GenericBuyInfo(typeof(LearnMetalBook), 5, Utility.Random(1, 100), 0x4C5B, 0));
                Add(new GenericBuyInfo(typeof(LearnGraniteBook), 5, Utility.Random(1, 100), 0x4C5C, 0));

                // Ingots (with random availability for higher tiers) - names built from resource type
                Add(new GenericBuyInfo(typeof(IronIngot), 15, 150, 0x1BF2, 0));
            /*
				d(new GenericBuyInfo(typeof(DullCopperIngot), 25, 100, 0x1BF2, 2741));
                Add(new GenericBuyInfo(typeof(CopperIngot), 35, 70, 0x1BF2, 2840));
                Add(new GenericBuyInfo(typeof(ShadowIronIngot), 30, 80, 0x1BF2, 2739));
                
                if (Utility.RandomBool())
                    Add(new GenericBuyInfo(typeof(BronzeIngot), 40, 50, 0x1BF2, 2236));
                if (Utility.RandomDouble() > 0.60)
                    Add(new GenericBuyInfo(typeof(GoldIngot), 55, 30, 0x1BF2, 2843));
                if (Utility.RandomDouble() > 0.70)
                    Add(new GenericBuyInfo(typeof(AgapiteIngot), 85, 25, 0x1BF2, 2794));
                if (Utility.RandomDouble() > 0.80)
                    Add(new GenericBuyInfo(typeof(VeriteIngot), 125, 20, 0x1BF2, 2141));
                if (Utility.RandomDouble() > 0.90)
                    Add(new GenericBuyInfo(typeof(ValoriteIngot), 250, 15, 0x1BF2, 2397));
                if (Utility.RandomDouble() > 0.95)
                    Add(new GenericBuyInfo(typeof(RoseniumIngot), 500, 5, 0x1BF2, 236));
                if (Utility.RandomDouble() > 0.95)
                    Add(new GenericBuyInfo(typeof(TitaniumIngot), 500, 5, 0x1BF2, 1381));
           */
		    }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                // Buys ingots from players
                Add(typeof(IronIngot), 5);
                /*
                Add(typeof(DullCopperIngot), 8);
                Add(typeof(ShadowIronIngot), 10);
                Add(typeof(CopperIngot), 12);
                Add(typeof(BronzeIngot), 14);
                Add(typeof(GoldIngot), 16);
                Add(typeof(AgapiteIngot), 19);
                Add(typeof(VeriteIngot), 25);
                Add(typeof(ValoriteIngot), 45);
                Add(typeof(RoseniumIngot), 100);
                Add(typeof(TitaniumIngot), 100);
				*/
            }
        }
    }
    public class NMS_SBLumberMuseumGuide : SBInfo
    {
        private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public NMS_SBLumberMuseumGuide()
        {
        }

        public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                Add(new GenericBuyInfo(typeof(LoreGuidetoAdventure), 5, Utility.Random(5, 100), 0x1C11, 0));
                Add(new GenericBuyInfo(typeof(LearnWoodBook), 5, Utility.Random(1, 100), 0x4C5E, 0));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
            }
        }
    }
    public class NMS_SBTailorLeatherMuseumGuide : SBInfo
    {
        private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public NMS_SBTailorLeatherMuseumGuide()
        {
        }

        public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                Add(new GenericBuyInfo(typeof(LoreGuidetoAdventure), 5, Utility.Random(5, 100), 0x1C11, 0));
                Add(new GenericBuyInfo(typeof(LearnTailorBook), 5, Utility.Random(1, 100), 0x4C5E, 0));
                Add(new GenericBuyInfo(typeof(LearnLeatherBook), 5, Utility.Random(1, 100), 0x4C60, 0));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class SBSage: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSage()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LoreGuidetoAdventure ), 5, Utility.Random( 5,100 ), 0x1C11, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( WeaponAbilityBook ), 50, Utility.Random( 1,100 ), 0x2254, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnLeatherBook ), 50, Utility.Random( 1,100 ), 0x4C60, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnMiscBook ), 50, Utility.Random( 1,100 ), 0x4C5D, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnMetalBook ), 50, Utility.Random( 1,100 ), 0x4C5B, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnWoodBook ), 50, Utility.Random( 1,100 ), 0x4C5E, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnReagentsBook ), 50, Utility.Random( 1,100 ), 0x4C5E, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnGraniteBook ), 50, Utility.Random( 1,100 ), 0x4C5C, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnScalesBook ), 50, Utility.Random( 1,100 ), 0x4C60, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnTailorBook ), 50, Utility.Random( 1,100 ), 0x4C5E, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnTraps ), 50, Utility.Random( 1,100 ), 0xFF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BeATroubadour ), 500, Utility.Random( 1,2 ), 0x4C5C, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BeASorcerer ), 500, Utility.Random( 1,2 ), 0x4C5C, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( SwordsAndShackles ), 500, Utility.Random( 1,100 ), 0x529D, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( CBookDruidicHerbalism ), 500, Utility.Random( 1,100 ), 0x2D50, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 500, Utility.Random( 1,100 ), 0x2253, 0x4AA ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( AlchemicalElixirs ), 500, Utility.Random( 1,100 ), 0x2219, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( AlchemicalMixtures ), 500, Utility.Random( 1,100 ), 0x2223, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( BookOfPoisons ), 500, Utility.Random( 1,100 ), 0x2253, 0x4F8 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( WorkShoppes ), 500, Utility.Random( 1,100 ), 0x2259, 0xB50 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnTitles ), 50, Utility.Random( 1,100 ), 0xFF2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ScribesPen ), 8, Utility.Random( 1,100 ), 0x2051, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlankScroll ), 7, Utility.Random( 50,500 ), 0x0E34, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Monocle ), 24, Utility.Random( 1,25 ), 0x2C84, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( "1041267", typeof( Runebook ), 3500, Utility.Random( 1,3 ), 0x0F3D, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( ScribesPen ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlankScroll ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Monocle ), 12 ); } 
				Add( typeof( TomeOfWands ), Utility.Random( 100,400 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( NecromancerSpellbook ), 55 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BookOfBushido ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BookOfNinjitsu ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MysticSpellbook ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DeathKnightSpellbook ), Utility.Random( 100,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Runebook ), Utility.Random( 100,350 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BookOfChivalry ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BookOfChivalry ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HolyManSpellbook ), Utility.Random( 50,200 ) ); } 
				if ( 1 > 0 ){Add( typeof( SwordsAndShackles ), 25 ); }
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSECook: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSECook()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Wasabi ), 20, Utility.Random( 1,100 ), 0x24E8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SushiRolls ), 30, Utility.Random( 1,100 ), 0x283E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SushiPlatter ), 30, Utility.Random( 1,100 ), 0x2840, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GreenTea ), 30, Utility.Random( 1,100 ), 0x284C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MisoSoup ), 30, Utility.Random( 1,100 ), 0x284D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteMisoSoup ), 30, Utility.Random( 1,100 ), 0x284E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RedMisoSoup ), 30, Utility.Random( 1,100 ), 0x284F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AwaseMisoSoup ), 30, Utility.Random( 1,100 ), 0x2850, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BentoBox ), 60, Utility.Random( 1,100 ), 0x2836, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( BentoBox ), 6, Utility.Random( 1,100 ), 0x2837, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Wasabi ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BentoBox ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreenTea ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SushiRolls ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SushiPlatter ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MisoSoup ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RedMisoSoup ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteMisoSoup ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AwaseMisoSoup ), 5 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSEHats: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSEHats()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Kasa ), 31, Utility.Random( 1,100 ), 0x2798, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherJingasa ), 11, Utility.Random( 1,100 ), 0x2776, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ClothNinjaHood ), 33, Utility.Random( 1,100 ), 0x278F, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Kasa ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherJingasa ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ClothNinjaHood ), 16 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBShipwright : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBShipwright()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041205", typeof( SmallBoatDeed ), 10000, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041206", typeof( SmallDragonBoatDeed ), 13000, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041207", typeof( MediumBoatDeed ), 18000, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041208", typeof( MediumDragonBoatDeed ), 20000, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041209", typeof( LargeBoatDeed ), 30000, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041210", typeof( LargeDragonBoatDeed ), 40000, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( DockingLantern ), 580, Utility.Random( 1,100 ), 0x40FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Sextant ), 13, Utility.Random( 1,100 ), 0x1057, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GrapplingHook ), 58, Utility.Random( 1,100 ), 0x4F40, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoatStain ), 26, Utility.Random( 1,100 ), 0x14E0, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( SeaShell ), 58 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DockingLantern ), 29 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sextant ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GrapplingHook ), 29 ); } 
				Add( typeof( PirateChest ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( SunkenChest ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( FishingNet ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( SpecialFishingNet ), Utility.RandomMinMax( 30, 40 ) );
				Add( typeof( FabledFishingNet ), Utility.RandomMinMax( 50, 60 ) );
				Add( typeof( NeptunesFishingNet ), Utility.RandomMinMax( 70, 80 ) );
				Add( typeof( PrizedFish ), Utility.RandomMinMax( 30, 60 ) );
				Add( typeof( WondrousFish ), Utility.RandomMinMax( 30, 60 ) );
				Add( typeof( TrulyRareFish ), Utility.RandomMinMax( 30, 60 ) );
				Add( typeof( PeculiarFish ), Utility.RandomMinMax( 30, 60 ) );
				Add( typeof( SpecialSeaweed ), Utility.RandomMinMax( 20, 80 ) );
				Add( typeof( SunkenBag ), Utility.RandomMinMax( 50, 250 ) );
				Add( typeof( ShipwreckedItem ), Utility.RandomMinMax( 10, 50 ) );
				Add( typeof( HighSeasRelic ), Utility.RandomMinMax( 10, 50 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoatStain ), 13 ); } 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MegalodonTooth ), Utility.RandomMinMax( 500, 2000 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBDevon : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBDevon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( MagicSextant ), Utility.Random( 500,1000 ), Utility.Random( 5,100 ), 0x26A0, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSmithTools: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBSmithTools() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( IronIngot ), 5, Utility.Random( 1,100 ), 0x1BF2, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Tongs ), 13, Utility.Random( 1,100 ), 0xFBB, 0 ) ); } 

			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tongs ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( IronIngot ), 4 ); }  
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBStoneCrafter : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBStoneCrafter()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Nails ), 3, Utility.Random( 1,100 ), 0x102E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Axle ), 2, Utility.Random( 1,100 ), 0x105B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Board ), 3, Utility.Random( 1,100 ), 0x1BD7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DrawKnife ), 10, Utility.Random( 1,100 ), 0x10E4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Froe ), 10, Utility.Random( 1,100 ), 0x10E5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Scorp ), 10, Utility.Random( 1,100 ), 0x10E7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Inshave ), 10, Utility.Random( 1,100 ), 0x10E6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DovetailSaw ), 12, Utility.Random( 1,100 ), 0x1028, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Saw ), 100, Utility.Random( 1,100 ), 0x1034, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Hammer ), 17, Utility.Random( 1,100 ), 0x102A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MouldingPlane ), 11, Utility.Random( 1,100 ), 0x102C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmoothingPlane ), 10, Utility.Random( 1,100 ), 0x1032, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( JointingPlane ), 11, Utility.Random( 1,100 ), 0x1030, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( WoodworkingTools ), 10, Utility.Random( 10,30 ), 0x4F52, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( "Making Valuables With Stonecrafting", typeof( MasonryBook ), 50625, Utility.Random( 1,100 ), 0xFBE, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( "Mining For Quality Stone", typeof( StoneMiningBook ), 25625, Utility.Random( 1,100 ), 0xFBE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1044515", typeof( MalletAndChisel ), 3, Utility.Random( 1,100 ), 0x12B3, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "Jade Statue Maker", typeof( JadeStatueMaker ), 50000, 1, 0x32F2, 0xB93 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "Marble Statue Maker", typeof( MarbleStatueMaker ), 50000, 1, 0x32F2, 0xB8F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "Bronze Statue Maker", typeof( BronzeStatueMaker ), 50000, 1, 0x32F2, 0xB97 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( MasonryBook ), 5000 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StoneMiningBook ), 5000 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MalletAndChisel ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBox ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmallCrate ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MediumCrate ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LargeCrate ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenChest ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LargeTable ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Nightstand ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( YewWoodTable ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Throne ), 24 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenThrone ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Stool ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FootStool ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FancyWoodenChairCushion ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenChairCushion ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenChair ), 8 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BambooChair ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBench ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Saw ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Scorp ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmoothingPlane ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DrawKnife ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Froe ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Hammer ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Inshave ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodworkingTools ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JointingPlane ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MouldingPlane ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DovetailSaw ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Board ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Axle ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenShield ), 31 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlackStaff ), 24 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GnarledStaff ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuarterStaff ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShepherdsCrook ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Club ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Log ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RockUrn ), 30 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RockVase ), 30 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Tailor vendor.
	/// Sells tailoring tools, clothing, hats, and accessories.
	/// </summary>
	public class SBTailor : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBTailor class.
		/// </summary>
		public SBTailor()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Tailoring tools (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Scissors), StoreSalesListConstants.PRICE_SCISSORS, 5, 15, StoreSalesListConstants.ITEMID_SCISSORS);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(SewingKit), StoreSalesListConstants.PRICE_SEWING_KIT, 5, 15, StoreSalesListConstants.ITEMID_SEWING_KIT);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Dyes), StoreSalesListConstants.PRICE_DYES, 5, 30, StoreSalesListConstants.ITEMID_DYES);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(DyeTub), StoreSalesListConstants.PRICE_DYE_TUB, 3, 15, StoreSalesListConstants.ITEMID_DYE_TUB);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(CottonSpoolOfThread), StoreSalesListConstants.PRICE_COTTON_SPOOL_THREAD, 15, 35, StoreSalesListConstants.ITEMID_COTTON_SPOOL_THREAD);

				// Basic headwear (always available, with random dyed hues)
				Add(new GenericBuyInfo(typeof(Bandana), StoreSalesListConstants.PRICE_BANDANA, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_BANDANA, Utility.RandomDyedHue()));
				Add(new GenericBuyInfo(typeof(SkullCap), StoreSalesListConstants.PRICE_SKULL_CAP, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_SKULL_CAP, Utility.RandomDyedHue()));
				Add(new GenericBuyInfo(typeof(Cap), StoreSalesListConstants.PRICE_CAP, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_CAP, Utility.RandomDyedHue()));

				// Thread and clothing (chance-based, with random dyed hues where applicable)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(FlaxSpoolOfThread), StoreSalesListConstants.PRICE_FLAX_SPOOL_THREAD, 2, 15, StoreSalesListConstants.ITEMID_FLAX_SPOOL_THREAD);
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(Shirt), StoreSalesListConstants.PRICE_SHIRT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_SHIRT, Utility.RandomDyedHue())); }
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ShortPants), StoreSalesListConstants.PRICE_SHORT_PANTS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15, StoreSalesListConstants.ITEMID_SHORT_PANTS);
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(FancyShirt), StoreSalesListConstants.PRICE_FANCY_SHIRT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_FANCY_SHIRT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(RoyalCoat), StoreSalesListConstants.PRICE_ROYAL_COAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_ROYAL_COAT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(RoyalShirt), StoreSalesListConstants.PRICE_ROYAL_SHIRT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_ROYAL_SHIRT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(RusticShirt), StoreSalesListConstants.PRICE_RUSTIC_SHIRT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_RUSTIC_SHIRT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(SquireShirt), StoreSalesListConstants.PRICE_SQUIRE_SHIRT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_SQUIRE_SHIRT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(FormalCoat), StoreSalesListConstants.PRICE_FORMAL_COAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_FORMAL_COAT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(WizardShirt), StoreSalesListConstants.PRICE_WIZARD_SHIRT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_WIZARD_SHIRT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(BeggarVest), StoreSalesListConstants.PRICE_BEGGAR_VEST, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_BEGGAR_VEST, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(RoyalVest), StoreSalesListConstants.PRICE_ROYAL_VEST, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_ROYAL_VEST, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(RusticVest), StoreSalesListConstants.PRICE_RUSTIC_VEST, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_RUSTIC_VEST, Utility.RandomDyedHue())); }
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SailorPants), StoreSalesListConstants.PRICE_SAILOR_PANTS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15, StoreSalesListConstants.ITEMID_SAILOR_PANTS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PiratePants), StoreSalesListConstants.PRICE_PIRATE_PANTS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15, StoreSalesListConstants.ITEMID_PIRATE_PANTS);
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(RoyalSkirt), StoreSalesListConstants.PRICE_ROYAL_SKIRT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_ROYAL_SKIRT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(Skirt), StoreSalesListConstants.PRICE_SKIRT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_SKIRT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(RoyalLongSkirt), StoreSalesListConstants.PRICE_ROYAL_LONG_SKIRT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_ROYAL_LONG_SKIRT, Utility.RandomDyedHue())); }
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LongPants), StoreSalesListConstants.PRICE_LONG_PANTS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15, StoreSalesListConstants.ITEMID_LONG_PANTS);
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(FancyDress), StoreSalesListConstants.PRICE_FANCY_DRESS, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_FANCY_DRESS, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(PlainDress), StoreSalesListConstants.PRICE_PLAIN_DRESS, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_PLAIN_DRESS, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(Kilt), StoreSalesListConstants.PRICE_KILT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_KILT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(HalfApron), StoreSalesListConstants.PRICE_HALF_APRON, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_HALF_APRON, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(LoinCloth), StoreSalesListConstants.PRICE_LOIN_CLOTH, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_LOIN_CLOTH, StoreSalesListConstants.HUE_LOIN_CLOTH)); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(RoyalLoinCloth), StoreSalesListConstants.PRICE_ROYAL_LOIN_CLOTH, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_ROYAL_LOIN_CLOTH, StoreSalesListConstants.HUE_LOIN_CLOTH)); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(Robe), StoreSalesListConstants.PRICE_ROBE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_ROBE, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(Cloak), StoreSalesListConstants.PRICE_CLOAK, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_CLOAK, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(Doublet), StoreSalesListConstants.PRICE_DOUBLET, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_DOUBLET, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(Tunic), StoreSalesListConstants.PRICE_TUNIC, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_TUNIC, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(JesterSuit), StoreSalesListConstants.PRICE_JESTER_SUIT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_JESTER_SUIT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(JesterHat), StoreSalesListConstants.PRICE_JESTER_HAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_JESTER_HAT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(FloppyHat), StoreSalesListConstants.PRICE_FLOPPY_HAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_FLOPPY_HAT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(WideBrimHat), StoreSalesListConstants.PRICE_WIDE_BRIM_HAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_WIDE_BRIM_HAT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(TallStrawHat), StoreSalesListConstants.PRICE_TALL_STRAW_HAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_TALL_STRAW_HAT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(StrawHat), StoreSalesListConstants.PRICE_STRAW_HAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_STRAW_HAT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(WizardsHat), StoreSalesListConstants.PRICE_WIZARDS_HAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_WIZARDS_HAT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(WitchHat), StoreSalesListConstants.PRICE_WITCH_HAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_WITCH_HAT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(LeatherCap), StoreSalesListConstants.PRICE_LEATHER_CAP, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_LEATHER_CAP, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(FeatheredHat), StoreSalesListConstants.PRICE_FEATHERED_HAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_FEATHERED_HAT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(TricorneHat), StoreSalesListConstants.PRICE_TRICORNE_HAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_TRICORNE_HAT, Utility.RandomDyedHue())); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(PirateHat), StoreSalesListConstants.PRICE_PIRATE_HAT, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 15), StoreSalesListConstants.ITEMID_PIRATE_HAT, Utility.RandomDyedHue())); }
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Special robes
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(JokerRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(AssassinRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FancyRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GildedRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(OrnateRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MagistrateRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RoyalRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SorcererRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(AssassinShroud), 29);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(NecromancerRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SpiderRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(VagabondRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PirateCoat), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ExquisiteRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ProphetRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ElegantRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FormalRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ArchmageRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PriestRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(CultistRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GildedDarkRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(GildedLightRobe), 19);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SageRobe), 19);

				// Tools and materials
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Scissors), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SewingKit), 1);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Dyes), 4);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(DyeTub), 4);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BoltOfCloth), 35);

				// Clothing
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FancyShirt), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Shirt), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ShortPants), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LongPants), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Cloak), 4);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FancyDress), 12);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Robe), 9);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PlainDress), 7);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Skirt), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RoyalCoat), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RoyalShirt), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RusticShirt), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SquireShirt), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FormalCoat), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WizardShirt), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BeggarVest), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RoyalVest), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RusticVest), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SailorPants), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PiratePants), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RoyalSkirt), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RoyalLongSkirt), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Kilt), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Doublet), 7);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Tunic), 9);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(JesterSuit), 13);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FullApron), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(HalfApron), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LoinCloth), 5);

				// Hats
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(JesterHat), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FloppyHat), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WideBrimHat), 4);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Cap), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SkullCap), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ClothCowl), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WizardHood), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ClothHood), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FancyHood), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Bandana), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(TallStrawHat), 4);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(StrawHat), 4);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WizardsHat), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WitchHat), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Bonnet), 4);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FeatheredHat), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(TricorneHat), 4);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PirateHat), 4);

				// Materials
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SpoolOfThread), 9);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Flax), 51);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Cotton), 51);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Wool), 31);

				// Rare magic items
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(MagicRobe), 30);
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(MagicHat), 20);
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(MagicCloak), 30);
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(MagicBelt), 20);
				StoreSalesListHelper.AddSellItemWithRareChance(this, typeof(MagicSash), 20);
				StoreSalesListHelper.AddSellItemRandom(this, typeof(MagicScissors), 300, 400);
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBJester: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBJester()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BagOfTricks ), 200, Utility.Random( 1,100 ), 0x1E3F, 0 ) );
				Add( new GenericBuyInfo( typeof( JesterHat ), 12, Utility.Random( 1,100 ), 0x171C, 0 ) );
				Add( new GenericBuyInfo( typeof( JokerHat ), 12, Utility.Random( 1,100 ), 0x171C, 0 ) );
				Add( new GenericBuyInfo( typeof( JesterSuit ), 26, Utility.Random( 1,100 ), 0x1F9F, 0 ) );
				Add( new GenericBuyInfo( typeof( JesterGarb ), 26, Utility.Random( 1,100 ), 0x1F9F, 0 ) );
				Add( new GenericBuyInfo( typeof( FoolsCoat ), 26, Utility.Random( 1,100 ), 0x1F9F, 0 ) );
				Add( new GenericBuyInfo( typeof( JokerRobe ), 26, Utility.Random( 1,100 ), 0x1F9F, 0 ) );
				Add( new GenericBuyInfo( typeof( JesterShoes ), 26, Utility.Random( 1,100 ), 0x170f, 0 ) );
				Add( new GenericBuyInfo( typeof( ThrowingGloves ), 26, Utility.Random( 1,100 ), 0x13C6, 0 ) );
				Add( new GenericBuyInfo( typeof( ThrowingWeapon ), 2, Utility.Random( 20, 200 ), 0x52B2, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MyCircusTentEastAddonDeed ), 1000, 1, 0xA58, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MyCircusTentSouthAddonDeed ), 1000, 1, 0xA59, Utility.RandomDyedHue() ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BagOfTricks ), 100 );
				Add( typeof( JesterHat ), 6 );
				Add( typeof( JokerHat ), 6 );
				Add( typeof( JesterSuit ), 13 );
				Add( typeof( JesterGarb ), 13 );
				Add( typeof( FoolsCoat ), 13 );
				Add( typeof( JokerRobe ), 13 );
				Add( typeof( JesterShoes ), 13 );
				Add( typeof( ThrowingGloves ), 13 );
				Add( typeof( ThrowingWeapon ), 1 );
				Add( typeof( MyCircusTentEastAddonDeed ), 200 );
				Add( typeof( MyCircusTentSouthAddonDeed ), 200 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Tanner vendor.
	/// Sells leather armor, bags, pouches, and tanning tools.
	/// </summary>
	public class SBTanner : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBTanner class.
		/// </summary>
		public SBTanner()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Leather armor (chance-based)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(FemaleStuddedChest), StoreSalesListConstants.PRICE_FEMALE_STUDDED_CHEST, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_FEMALE_STUDDED_CHEST);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(FemalePlateChest), StoreSalesListConstants.PRICE_FEMALE_PLATE_CHEST, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_FEMALE_PLATE_CHEST);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(FemaleLeatherChest), StoreSalesListConstants.PRICE_FEMALE_LEATHER_CHEST, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_FEMALE_LEATHER_CHEST);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LeatherShorts), StoreSalesListConstants.PRICE_LEATHER_SHORTS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LEATHER_SHORTS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LeatherSkirt), StoreSalesListConstants.PRICE_LEATHER_SKIRT, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LEATHER_SKIRT);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LeatherBustierArms), StoreSalesListConstants.PRICE_LEATHER_BUSTIER_ARMS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LEATHER_BUSTIER_ARMS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(StuddedBustierArms), StoreSalesListConstants.PRICE_STUDDED_BUSTIER_ARMS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_STUDDED_BUSTIER_ARMS);

				// Bags and containers (chance-based)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Bag), StoreSalesListConstants.PRICE_BAG, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BAG, StoreSalesListConstants.HUE_BAG);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Pouch), StoreSalesListConstants.PRICE_POUCH, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_POUCH);

				// Backpack (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Backpack), StoreSalesListConstants.PRICE_BACKPACK, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BACKPACK_TANNER);

				// Tools (chance-based)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SkinningKnife), StoreSalesListConstants.PRICE_SKINNING_KNIFE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SKINNING_KNIFE);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Containers
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Bag), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Pouch), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Backpack), 7);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SkinningKnife), 7);

				// Leather armor
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FemaleStuddedChest), 31);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(StuddedBustierArms), 23);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FemalePlateChest), 103);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(FemaleLeatherChest), 18);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LeatherBustierArms), 12);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LeatherShorts), 14);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LeatherSkirt), 12);
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBTavernKeeper : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBTavernKeeper()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Ale, 70, Utility.Random( 1,100 ), 0x99F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Wine, 70, Utility.Random( 1,100 ), 0x9C7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Liquor, 70, Utility.Random( 1,100 ), 0x99B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Jug ), BeverageType.Cider, 130, Utility.Random( 1,100 ), 0x9C8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Milk, 180, Utility.Random( 1,100 ), 0x9F0, 0 ) ); }
				if ( 1 > 0 ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Ale, 110, Utility.Random( 1,100 ), 0x1F95, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Cider, 110, Utility.Random( 1,100 ), 0x1F97, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Liquor, 110, Utility.Random( 1,100 ), 0x1F99, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Wine, 110, Utility.Random( 1,100 ), 0x1F9B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Water, 110, Utility.Random( 1,100 ), 0x1F9D, 0 ) ); }
				Add( new GenericBuyInfo( typeof( Pitcher ), 3, Utility.Random( 50,100 ), 0x1F97, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BreadLoaf ), 60, Utility.Random( 1,100 ), 0x103B, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( CheeseWheel ), 410, Utility.Random( 1,100 ), 0x97E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CookedBird ), 170, Utility.Random( 1,100 ), 0x9B7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LambLeg ), 80, Utility.Random( 1,100 ), 0x160A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChickenLeg ), 80, Utility.Random( 1,100 ), 0x1608, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ribs ), 90, Utility.Random( 1,100 ), 0x9F2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfCarrots ), 30, Utility.Random( 1,100 ), 0x15F9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfCorn ), 30, Utility.Random( 1,100 ), 0x15FA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfLettuce ), 30, Utility.Random( 1,100 ), 0x15FB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfPeas ), 30, Utility.Random( 1,100 ), 0x15FC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( EmptyPewterBowl ), 20, Utility.Random( 1,100 ), 0x15FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfCorn ), 30, Utility.Random( 1,100 ), 0x15FE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfLettuce ), 30, Utility.Random( 1,100 ), 0x15FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfPeas ), 30, Utility.Random( 1,100 ), 0x1600, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfFoodPotatos ), 30, Utility.Random( 1,100 ), 0x1601, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfStew ), 30, Utility.Random( 1,100 ), 0x1604, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfTomatoSoup ), 30, Utility.Random( 1,100 ), 0x1606, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ApplePie ), 70, Utility.Random( 1,100 ), 0x1041, 0 ) ); } //OSI just has Pie, not Apple/Fruit/Meat
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( tarotpoker ), 50, Utility.Random( 1,100 ), 0x12AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016450", typeof( Chessboard ), 20, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016449", typeof( CheckerBoard ), 20, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backgammon ), 20, Utility.Random( 1,100 ), 0xE1C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dices ), 20, Utility.Random( 1,100 ), 0xFA7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Engines.Mahjong.MahjongGame ), 600, Utility.Random( 1,100 ), 0xFAA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Waterskin ), 5, Utility.Random( 1,100 ), 0xA21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HenchmanFighterItem ), 5000, Utility.Random( 1,100 ), 0x1419, 0xB96 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HenchmanArcherItem ), 6000, Utility.Random( 1,100 ), 0xF50, 0xB96 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HenchmanWizardItem ), 7000, Utility.Random( 1,100 ), 0xE30, 0xB96 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041243", typeof( ContractOfEmployment ), 1252, Utility.Random( 1,100 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "a barkeep contract", typeof( BarkeepContract ), 1252, Utility.Random( 1,100 ), 0x14F0, 0 ) ); }

				if ( Multis.BaseHouse.NewVendorSystem )
					if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1062332", typeof( VendorRentalContract ), 1252, Utility.Random( 1,100 ), 0x14F0, 0x672 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfCarrots ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfCorn ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfLettuce ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfPeas ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmptyPewterBowl ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfCorn ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfLettuce ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfPeas ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfFoodPotatos ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfStew ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfTomatoSoup ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BeverageBottle ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Waterskin ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Jug ), 30 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pitcher ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GlassMug ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BreadLoaf ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheeseWheel ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ribs ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Peach ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pear ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Grapes ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Apple ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Banana ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Candle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( tarotpoker ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MahjongGame ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Chessboard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheckerBoard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backgammon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dices ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ContractOfEmployment ), 626 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RomulanAle ), Utility.Random( 20,100 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Thief vendor.
	/// Sells thieving tools, lockpicks, keys, disguise kits, and related items.
	/// </summary>
	public class SBThief : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBThief class.
		/// </summary>
		public SBThief()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Containers (chance-based)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Backpack), StoreSalesListConstants.PRICE_BACKPACK, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BACKPACK);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Pouch), StoreSalesListConstants.PRICE_POUCH, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_POUCH);

				// Light sources (chance-based)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Torch), StoreSalesListConstants.PRICE_TORCH, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_TORCH);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Lantern), StoreSalesListConstants.PRICE_LANTERN, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LANTERN);

				// Training books (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(LearnStealingBook), StoreSalesListConstants.PRICE_LEARN_STEALING_BOOK, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LEARN_STEALING_BOOK);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(LearnTraps), StoreSalesListConstants.PRICE_LEARN_TRAPS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LEARN_TRAPS);

				// Lockpicking tools (chance-based)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Lockpick), StoreSalesListConstants.PRICE_LOCKPICK, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LOCKPICK);
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo(typeof(SkeletonsKey), Utility.Random(StoreSalesListConstants.PRICE_SKELETONS_KEY_MIN, StoreSalesListConstants.PRICE_SKELETONS_KEY_MAX), StoreSalesListConstants.QTY_SKELETONS_KEY, StoreSalesListConstants.ITEMID_SKELETONS_KEY, 0));
				}

				// Containers and keys (chance-based)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBox), StoreSalesListConstants.PRICE_WOODEN_BOX, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WOODEN_BOX);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Key), StoreSalesListConstants.PRICE_KEY, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_KEY);

				// Disguise items (chance-based)
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo("1041060", typeof(HairDye), StoreSalesListConstants.PRICE_HAIR_DYE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_HAIR_DYE, 0)); }
				if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo("hair dye bottle", typeof(HairDyeBottle), StoreSalesListConstants.PRICE_HAIR_DYE_BOTTLE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_HAIR_DYE_BOTTLE, 0)); }
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(DisguiseKit), StoreSalesListConstants.PRICE_DISGUISE_KIT, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, StoreSalesListConstants.QTY_RANDOM_SMALL_MAX, StoreSalesListConstants.ITEMID_DISGUISE_KIT);

				// Special items (chance-based)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PackGrenade), StoreSalesListConstants.PRICE_PACK_GRENADE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PACK_GRENADE);

				// Rare item (always available)
				StoreSalesListHelper.AddBuyItem(this, typeof(CorruptedMoonStone), StoreSalesListConstants.PRICE_CORRUPTED_MOON_STONE, StoreSalesListConstants.QTY_CORRUPTED_MOON_STONE, StoreSalesListConstants.ITEMID_CORRUPTED_MOON_STONE);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Containers
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Backpack), 7);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Pouch), 3);

				// Light sources
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Torch), 3);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Lantern), 1);

				// Tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Lockpick), 6);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodenBox), 7);

				// Disguise items
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(HairDye), 50);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(HairDyeBottle), 300);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SkeletonsKey), 10);
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Tinker vendor.
	/// Sells tools, mechanical parts, keys, lockpicks, harvesters, barrels, and various tinkering supplies.
	/// </summary>
	public class SBTinker: SBInfo 
	{ 
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBTinker class.
		/// </summary>
		public SBTinker() 
		{ 
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion 

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo() 
			{
				// Clocks and mechanical parts (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Clock), StoreSalesListConstants.PRICE_CLOCK, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_CLOCK);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Nails), StoreSalesListConstants.PRICE_NAILS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_NAILS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ClockParts), StoreSalesListConstants.PRICE_CLOCK_PARTS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_CLOCK_PARTS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(AxleGears), StoreSalesListConstants.PRICE_AXLE_GEARS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_AXLE_GEARS);

				// Gears (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Gears), StoreSalesListConstants.PRICE_GEARS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_GEARS);

				// More mechanical parts (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Hinge), StoreSalesListConstants.PRICE_HINGE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_HINGE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Sextant), StoreSalesListConstants.PRICE_SEXTANT, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SEXTANT);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SextantParts), StoreSalesListConstants.PRICE_SEXTANT_PARTS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SEXTANT_PARTS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Axle), StoreSalesListConstants.PRICE_AXLE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_AXLE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Springs), StoreSalesListConstants.PRICE_SPRINGS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SPRINGS);

				// Keys (with chance, multiple variants)
				StoreSalesListHelper.AddBuyItemWithNameWithChanceRandom(this, "1024111", typeof(Key), StoreSalesListConstants.PRICE_KEY, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_KEY_1);
				StoreSalesListHelper.AddBuyItemWithNameWithChanceRandom(this, "1024112", typeof(Key), StoreSalesListConstants.PRICE_KEY, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_KEY_2);
				StoreSalesListHelper.AddBuyItemWithNameWithChanceRandom(this, "1024115", typeof(Key), StoreSalesListConstants.PRICE_KEY, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_KEY_3);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(KeyRing), StoreSalesListConstants.PRICE_KEY_RING, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_KEY_RING);

				// Lockpicking tools (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Lockpick), StoreSalesListConstants.PRICE_LOCKPICK, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LOCKPICK);
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo(typeof(SkeletonsKey), Utility.Random(StoreSalesListConstants.PRICE_SKELETONS_KEY_MIN, StoreSalesListConstants.PRICE_SKELETONS_KEY_MAX), StoreSalesListConstants.QTY_SKELETONS_KEY, StoreSalesListConstants.ITEMID_SKELETONS_KEY, StoreSalesListConstants.HUE_DEFAULT));
				}

				// TinkersTools (always available)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(TinkersTools), StoreSalesListConstants.PRICE_TINKERS_TOOLS, StoreSalesListConstants.QTY_TINKERS_TOOLS_MIN, StoreSalesListConstants.QTY_TINKERS_TOOLS_MAX, StoreSalesListConstants.ITEMID_TINKERS_TOOLS); 
				// Materials and tools (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Board), StoreSalesListConstants.PRICE_BOARD, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BOARD);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(IronIngot), StoreSalesListConstants.PRICE_IRON_INGOT, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_IRON_INGOT);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SewingKit), StoreSalesListConstants.PRICE_SEWING_KIT_TINKER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SEWING_KIT);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(DrawKnife), StoreSalesListConstants.PRICE_DRAW_KNIFE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_DRAW_KNIFE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Froe), StoreSalesListConstants.PRICE_FROE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_FROE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Scorp), StoreSalesListConstants.PRICE_SCORP, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SCORP);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Inshave), StoreSalesListConstants.PRICE_INSHAVE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_INSHAVE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ButcherKnife), StoreSalesListConstants.PRICE_BUTCHER_KNIFE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BUTCHER_KNIFE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Scissors), StoreSalesListConstants.PRICE_SCISSORS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SCISSORS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Tongs), StoreSalesListConstants.PRICE_TONGS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_TONGS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(DovetailSaw), StoreSalesListConstants.PRICE_DOVETAIL_SAW, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_DOVETAIL_SAW);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Saw), StoreSalesListConstants.PRICE_SAW, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SAW);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Hammer), StoreSalesListConstants.PRICE_HAMMER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_HAMMER);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SmithHammer), StoreSalesListConstants.PRICE_SMITH_HAMMER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SMITH_HAMMER);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Shovel), StoreSalesListConstants.PRICE_SHOVEL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SHOVEL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(OreShovel), StoreSalesListConstants.PRICE_ORE_SHOVEL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_ORE_SHOVEL, StoreSalesListConstants.HUE_ORE_SHOVEL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(MouldingPlane), StoreSalesListConstants.PRICE_MOULDING_PLANE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_MOULDING_PLANE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(JointingPlane), StoreSalesListConstants.PRICE_JOINTING_PLANE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_JOINTING_PLANE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SmoothingPlane), StoreSalesListConstants.PRICE_SMOOTHING_PLANE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SMOOTHING_PLANE);

				// WoodworkingTools (always available, different quantity range)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(WoodworkingTools), StoreSalesListConstants.PRICE_WOODWORKING_TOOLS, StoreSalesListConstants.QTY_WOODWORKING_TOOLS_MIN, StoreSalesListConstants.QTY_WOODWORKING_TOOLS_MAX, StoreSalesListConstants.ITEMID_WOODWORKING_TOOLS);

				// More tools (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Pickaxe), StoreSalesListConstants.PRICE_PICKAXE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PICKAXE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ThrowingWeapon), StoreSalesListConstants.PRICE_THROWING_WEAPON, StoreSalesListConstants.QTY_THROWING_WEAPON_MIN, StoreSalesListConstants.QTY_THROWING_WEAPON_MAX, StoreSalesListConstants.ITEMID_THROWING_WEAPON);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WallTorch), StoreSalesListConstants.PRICE_WALL_TORCH, StoreSalesListConstants.QTY_WALL_TORCH_MIN, StoreSalesListConstants.QTY_WALL_TORCH_MAX, StoreSalesListConstants.ITEMID_WALL_TORCH);

				// ColoredWallTorch (very rare chance)
				if (MyServerSettings.SellVeryRareChance())
				{
					Add(new GenericBuyInfo(typeof(ColoredWallTorch), StoreSalesListConstants.PRICE_COLORED_WALL_TORCH, Utility.Random(StoreSalesListConstants.QTY_WALL_TORCH_MIN, StoreSalesListConstants.QTY_WALL_TORCH_MAX), StoreSalesListConstants.ITEMID_COLORED_WALL_TORCH, StoreSalesListConstants.HUE_DEFAULT));
				}

				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(light_dragon_brazier), StoreSalesListConstants.PRICE_LIGHT_DRAGON_BRAZIER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LIGHT_DRAGON_BRAZIER);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(TrapKit), StoreSalesListConstants.PRICE_TRAP_KIT, StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX, StoreSalesListConstants.ITEMID_TRAP_KIT);
				// PianoAddonDeed (rare chance, random price)
				if (MyServerSettings.SellRareChance())
				{
					Add(new GenericBuyInfo(typeof(PianoAddonDeed), Utility.Random(StoreSalesListConstants.PRICE_PIANO_ADDON_DEED_MIN, StoreSalesListConstants.PRICE_PIANO_ADDON_DEED_MAX), 1, StoreSalesListConstants.ITEMID_PIANO_ADDON_DEED, StoreSalesListConstants.HUE_DEFAULT));
				}

				// Harvesters (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PoorMiningHarvester), StoreSalesListConstants.PRICE_POOR_MINING_HARVESTER, StoreSalesListConstants.QTY_DEVICE_KIT_MIN, StoreSalesListConstants.QTY_DEVICE_KIT_MAX, StoreSalesListConstants.ITEMID_MINING_HARVESTER);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(StandardMiningHarvester), StoreSalesListConstants.PRICE_STANDARD_MINING_HARVESTER, StoreSalesListConstants.QTY_DEVICE_KIT_MIN, StoreSalesListConstants.QTY_DEVICE_KIT_MAX, StoreSalesListConstants.ITEMID_MINING_HARVESTER);
				if (MyServerSettings.SellRareChance())
				{
					Add(new GenericBuyInfo(typeof(GoodMiningHarvester), StoreSalesListConstants.PRICE_GOOD_MINING_HARVESTER, Utility.Random(StoreSalesListConstants.QTY_DEVICE_KIT_MIN, StoreSalesListConstants.QTY_DEVICE_KIT_MAX), StoreSalesListConstants.ITEMID_MINING_HARVESTER, StoreSalesListConstants.HUE_DEFAULT));
				}
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PoorLumberHarvester), StoreSalesListConstants.PRICE_POOR_LUMBER_HARVESTER, StoreSalesListConstants.QTY_DEVICE_KIT_MIN, StoreSalesListConstants.QTY_DEVICE_KIT_MAX, StoreSalesListConstants.ITEMID_LUMBER_HARVESTER);
				if (MyServerSettings.SellRareChance())
				{
					Add(new GenericBuyInfo(typeof(StandardLumberHarvester), StoreSalesListConstants.PRICE_STANDARD_LUMBER_HARVESTER, Utility.Random(StoreSalesListConstants.QTY_DEVICE_KIT_MIN, StoreSalesListConstants.QTY_DEVICE_KIT_MAX), StoreSalesListConstants.ITEMID_LUMBER_HARVESTER, StoreSalesListConstants.HUE_DEFAULT));
				}
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PoorHideHarvester), StoreSalesListConstants.PRICE_POOR_HIDE_HARVESTER, StoreSalesListConstants.QTY_DEVICE_KIT_MIN, StoreSalesListConstants.QTY_DEVICE_KIT_MAX, StoreSalesListConstants.ITEMID_HIDE_HARVESTER);
				if (MyServerSettings.SellRareChance())
				{
					Add(new GenericBuyInfo(typeof(StandardHideHarvester), StoreSalesListConstants.PRICE_STANDARD_HIDE_HARVESTER, Utility.Random(StoreSalesListConstants.QTY_DEVICE_KIT_MIN, StoreSalesListConstants.QTY_DEVICE_KIT_MAX), StoreSalesListConstants.ITEMID_HIDE_HARVESTER, StoreSalesListConstants.HUE_DEFAULT));
				}
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(HarvesterRepairKit), StoreSalesListConstants.PRICE_HARVESTER_REPAIR_KIT, StoreSalesListConstants.QTY_DEVICE_KIT_MIN, StoreSalesListConstants.QTY_DEVICE_KIT_MAX, StoreSalesListConstants.ITEMID_HARVESTER_REPAIR_KIT);

				// Barrels and presses (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CiderBarrel), StoreSalesListConstants.PRICE_CIDER_BARREL, StoreSalesListConstants.QTY_BARREL_MIN, StoreSalesListConstants.QTY_BARREL_MAX, StoreSalesListConstants.ITEMID_BARREL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(AleBarrel), StoreSalesListConstants.PRICE_ALE_BARREL, StoreSalesListConstants.QTY_BARREL_MIN, StoreSalesListConstants.QTY_BARREL_MAX, StoreSalesListConstants.ITEMID_BARREL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LiquorBarrel), StoreSalesListConstants.PRICE_LIQUOR_BARREL, StoreSalesListConstants.QTY_BARREL_MIN, StoreSalesListConstants.QTY_BARREL_MAX, StoreSalesListConstants.ITEMID_BARREL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CheesePress), StoreSalesListConstants.PRICE_CHEESE_PRESS, StoreSalesListConstants.QTY_BARREL_MIN, StoreSalesListConstants.QTY_BARREL_MAX, StoreSalesListConstants.ITEMID_BARREL);

				// DeviceKit and GogglesofScience (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(DeviceKit), StoreSalesListConstants.PRICE_DEVICE_KIT, StoreSalesListConstants.QTY_DEVICE_KIT_MIN, StoreSalesListConstants.QTY_DEVICE_KIT_MAX, StoreSalesListConstants.ITEMID_DEVICE_KIT);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(GogglesofScience), StoreSalesListConstants.PRICE_GOGGLES_OF_SCIENCE, StoreSalesListConstants.QTY_DEVICE_KIT_MIN, StoreSalesListConstants.QTY_DEVICE_KIT_MAX, StoreSalesListConstants.ITEMID_GOGGLES_OF_SCIENCE);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo 
		{ 
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo() 
			{
				// Containers
				if (MyServerSettings.BuyCommonChance())
				{
					Add(typeof(LootChest), 600);
				}

				// Tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Shovel), StoreSalesListConstants.SELL_PRICE_SHOVEL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(OreShovel), StoreSalesListConstants.SELL_PRICE_ORE_SHOVEL);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SewingKit), StoreSalesListConstants.SELL_PRICE_SEWING_KIT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Scissors), StoreSalesListConstants.SELL_PRICE_SCISSORS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Tongs), StoreSalesListConstants.SELL_PRICE_TONGS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Key), StoreSalesListConstants.SELL_PRICE_KEY);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(DovetailSaw), StoreSalesListConstants.SELL_PRICE_DOVETAIL_SAW);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(MouldingPlane), StoreSalesListConstants.SELL_PRICE_MOULDING_PLANE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Nails), StoreSalesListConstants.SELL_PRICE_NAILS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(JointingPlane), StoreSalesListConstants.SELL_PRICE_JOINTING_PLANE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SmoothingPlane), StoreSalesListConstants.SELL_PRICE_SMOOTHING_PLANE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Saw), StoreSalesListConstants.SELL_PRICE_SAW);

				// Clock and mechanical parts
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Clock), StoreSalesListConstants.SELL_PRICE_CLOCK);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ClockParts), StoreSalesListConstants.SELL_PRICE_CLOCK_PARTS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(AxleGears), StoreSalesListConstants.SELL_PRICE_AXLE_GEARS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Gears), StoreSalesListConstants.SELL_PRICE_GEARS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Hinge), StoreSalesListConstants.SELL_PRICE_HINGE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Sextant), StoreSalesListConstants.SELL_PRICE_SEXTANT);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SextantParts), StoreSalesListConstants.SELL_PRICE_SEXTANT_PARTS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Axle), StoreSalesListConstants.SELL_PRICE_AXLE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Springs), StoreSalesListConstants.SELL_PRICE_SPRINGS);

				// Woodworking tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(DrawKnife), StoreSalesListConstants.SELL_PRICE_DRAW_KNIFE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Froe), StoreSalesListConstants.SELL_PRICE_FROE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Inshave), StoreSalesListConstants.SELL_PRICE_INSHAVE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(WoodworkingTools), StoreSalesListConstants.SELL_PRICE_WOODWORKING_TOOLS);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Scorp), StoreSalesListConstants.SELL_PRICE_SCORP);

				// Lockpicking tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Lockpick), StoreSalesListConstants.SELL_PRICE_LOCKPICK);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SkeletonsKey), StoreSalesListConstants.SELL_PRICE_SKELETONS_KEY);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(TinkerTools), StoreSalesListConstants.SELL_PRICE_TINKER_TOOLS);

				// Materials
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Board), StoreSalesListConstants.SELL_PRICE_BOARD);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Log), StoreSalesListConstants.SELL_PRICE_LOG);

				// More tools
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Pickaxe), StoreSalesListConstants.SELL_PRICE_PICKAXE);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Hammer), StoreSalesListConstants.SELL_PRICE_HAMMER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(SmithHammer), StoreSalesListConstants.SELL_PRICE_SMITH_HAMMER);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ButcherKnife), StoreSalesListConstants.SELL_PRICE_BUTCHER_KNIFE); 
				// Special items (with chance, random prices)
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(CrystalScales), Utility.Random(250, 500));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(GolemManual), Utility.Random(500, 750));
				}
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(PowerCrystal), 100);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ArcaneGem), 10);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ClockworkAssembly), 100);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(BottleOil), 5);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(ThrowingWeapon), StoreSalesListConstants.SELL_PRICE_THROWING_WEAPON);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(TrapKit), StoreSalesListConstants.SELL_PRICE_TRAP_KIT);

				// Space junk items (with chance, random prices)
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkA), Utility.Random(5, 10));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkB), Utility.Random(10, 20));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkC), Utility.Random(15, 30));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkD), Utility.Random(20, 40));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkE), Utility.Random(25, 50));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkF), Utility.Random(30, 60));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkG), Utility.Random(35, 70));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkH), Utility.Random(40, 80));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkI), Utility.Random(45, 90));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkJ), Utility.Random(50, 100));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkK), Utility.Random(55, 110));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkL), Utility.Random(60, 120));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkM), Utility.Random(65, 130));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkN), Utility.Random(70, 140));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkO), Utility.Random(75, 150));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkP), Utility.Random(80, 160));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkQ), Utility.Random(85, 170));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkR), Utility.Random(90, 180));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkS), Utility.Random(95, 190));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkT), Utility.Random(100, 200));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkU), Utility.Random(105, 210));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkV), Utility.Random(110, 220));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkW), Utility.Random(115, 230));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkX), Utility.Random(120, 240));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkY), Utility.Random(125, 250));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SpaceJunkZ), Utility.Random(130, 260));
				}

				// Robot and advanced items (with chance, random prices)
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(LandmineSetup), Utility.Random(100, 300));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(PlasmaGrenade), Utility.Random(28, 38));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(ThermalDetonator), Utility.Random(28, 38));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(PuzzleCube), Utility.Random(45, 90));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(PlasmaTorch), Utility.Random(45, 90));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(DuctTape), Utility.Random(45, 90));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(RobotBatteries), Utility.Random(5, 100));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(RobotSheetMetal), Utility.Random(5, 100));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(RobotOil), Utility.Random(5, 100));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(RobotGears), Utility.Random(5, 100));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(RobotEngineParts), Utility.Random(5, 100));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(RobotCircuitBoard), Utility.Random(5, 100));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(RobotBolt), Utility.Random(5, 100));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(RobotTransistor), Utility.Random(5, 100));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(RobotSchematics), Utility.Random(500, 750));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(DataPad), Utility.Random(5, 150));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(MaterialLiquifier), Utility.Random(100, 300));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(Chainsaw), Utility.Random(130, 260));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(PortableSmelter), Utility.Random(130, 260));
				}
			}
		}

		#endregion
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBVagabond : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBVagabond()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldRing ), 27, Utility.Random( 1,100 ), 0x4CFA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Necklace ), 26, Utility.Random( 1,100 ), 0x4CFE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldNecklace ), 27, Utility.Random( 1,100 ), 0x4CFF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldBeadNecklace ), 27, Utility.Random( 1,100 ), 0x4CFD, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Beads ), 27, Utility.Random( 1,100 ), 0x4CFE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldBracelet ), 27, Utility.Random( 1,100 ), 0x4CF1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldEarrings ), 27, Utility.Random( 1,100 ), 0x4CFB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Board ), 3, Utility.Random( 1,100 ), 0x1BD7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( IronIngot ), 6, Utility.Random( 1,100 ), 0x1BF2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StarSapphire ), 125, Utility.Random( 1,100 ), 0xF21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Emerald ), 100, Utility.Random( 1,100 ), 0xF10, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Sapphire ), 100, Utility.Random( 1,100 ), 0xF19, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ruby ), 75, Utility.Random( 1,100 ), 0xF13, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Citrine ), 50, Utility.Random( 1,100 ), 0xF15, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Amethyst ), 100, Utility.Random( 1,100 ), 0xF16, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tourmaline ), 75, Utility.Random( 1,100 ), 0xF2D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Amber ), 50, Utility.Random( 1,100 ), 0xF25, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Diamond ), 200, Utility.Random( 1,100 ), 0xF26, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Board ), 3, Utility.Random( 1,100 ), 0x1BD7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( IronIngot ), 6, Utility.Random( 1,100 ), 0x1BF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new AnimalBuyInfo( 1, typeof( TrainingElemental ), 50000, 14, 214, 0 ) ); }				
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Board ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IronIngot ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amber ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amethyst ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Citrine ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Diamond ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Emerald ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ruby ), 37 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sapphire ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphire ), 62 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tourmaline ), 47 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MageEye ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldRing ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverRing ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Necklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBeadNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBeadNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Beads ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBracelet ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBracelet ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldEarrings ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverEarrings ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryRing ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryCirclet ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryNecklace ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryEarrings ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryBracelet ), Utility.Random( 50,300 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBVarietyDealer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBVarietyDealer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( ArtifactVase ), Utility.Random( 50000,150000 ), 1, 0x0B48, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ArtifactLargeVase ), Utility.Random( 50000,150000 ), 1, 0x0B47, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TapestryOfSosaria ), Utility.Random( 50000,150000 ), 1, 0x234E, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BlueDecorativeRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BlueFancyRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BluePlainRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( CinnamonFancyRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( CurtainsDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( FountainDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GoldenDecorativeRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HangingAxesDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HangingSwordsDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( PinkFancyRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( RedPlainRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( WallBannerDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DecorativeShieldDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StoneAnkhDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BannerDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DecoTray ), Utility.Random( 50000,150000 ), 1, 0x992, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DecoTray2 ), Utility.Random( 50000,150000 ), 1, 0x991, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile01AddonDeed ), Utility.Random( 80000,120000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile02AddonDeed ), Utility.Random( 80000,120000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile03AddonDeed ), Utility.Random( 80000,120000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile04AddonDeed ), Utility.Random( 80000,120000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile05AddonDeed ), Utility.Random( 80000,120000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePileAddonDeed ), Utility.Random( 120000,200000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile2AddonDeed ), Utility.Random( 120000,200000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile3AddonDeed ), Utility.Random( 120000,200000 ), 1, 0x0E41, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( LootChest ), 600 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPainting ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingA ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingB ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingC ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingD ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingE ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingF ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingG ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxSculptors ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxSculptorsA ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxSculptorsB ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxSculptorsC ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxSculptorsD ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxSculptorsE ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( DragonLamp ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( DragonPedStatue ), Utility.Random( 50,500 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Veterinarian vendor.
	/// Sells medical supplies, potions, and hitching posts for animal care.
	/// </summary>
	public class SBVeterinarian : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBVeterinarian class.
		/// </summary>
		public SBVeterinarian()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Medical supplies (always available or with chance)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(Bandage), StoreSalesListConstants.PRICE_BANDAGE_VETERINARIAN, StoreSalesListConstants.QTY_BANDAGE_VETERINARIAN_MIN, StoreSalesListConstants.QTY_BANDAGE_VETERINARIAN_MAX, StoreSalesListConstants.ITEMID_BANDAGE_VETERINARIAN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LesserHealPotion), StoreSalesListConstants.PRICE_LESSER_HEAL_POTION_VETERINARIAN, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LESSER_HEAL_POTION_VETERINARIAN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Ginseng), StoreSalesListConstants.PRICE_GINSENG_VETERINARIAN, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_GINSENG_VETERINARIAN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Garlic), StoreSalesListConstants.PRICE_GARLIC_VETERINARIAN, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_GARLIC_VETERINARIAN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(RefreshPotion), StoreSalesListConstants.PRICE_REFRESH_POTION_VETERINARIAN, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_REFRESH_POTION_VETERINARIAN);

				// Animal care equipment (common chance)
				if (MyServerSettings.SellCommonChance())
				{
					Add(new GenericBuyInfo(typeof(HitchingPost), StoreSalesListConstants.PRICE_HITCHING_POST, Utility.Random(StoreSalesListConstants.QTY_HITCHING_POST_MIN, StoreSalesListConstants.QTY_HITCHING_POST_MAX), StoreSalesListConstants.ITEMID_HITCHING_POST, StoreSalesListConstants.HUE_DEFAULT));
				}
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// Medical supplies
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(LesserHealPotion), StoreSalesListConstants.SELL_PRICE_LESSER_HEAL_POTION_VETERINARIAN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(RefreshPotion), StoreSalesListConstants.SELL_PRICE_REFRESH_POTION_VETERINARIAN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Garlic), StoreSalesListConstants.SELL_PRICE_GARLIC_VETERINARIAN);
				StoreSalesListHelper.AddSellItemWithChance(this, typeof(Ginseng), StoreSalesListConstants.SELL_PRICE_GINSENG_VETERINARIAN);

				// Animal care equipment
				if (MyServerSettings.BuyCommonChance())
				{
					Add(typeof(HitchingPost), StoreSalesListConstants.SELL_PRICE_HITCHING_POST);
				}

				// Animal-related items (random prices)
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(AlienEgg), Utility.Random(StoreSalesListConstants.SELL_PRICE_ALIEN_EGG_MIN, StoreSalesListConstants.SELL_PRICE_ALIEN_EGG_MAX));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(DragonEgg), Utility.Random(StoreSalesListConstants.SELL_PRICE_DRAGON_EGG_MIN, StoreSalesListConstants.SELL_PRICE_DRAGON_EGG_MAX));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(FirstAidKit), Utility.Random(StoreSalesListConstants.SELL_PRICE_FIRST_AID_KIT_VETERINARIAN_MIN, StoreSalesListConstants.SELL_PRICE_FIRST_AID_KIT_VETERINARIAN_MAX));
				}
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Waiter vendor.
	/// Sells beverages, food, and meal items.
	/// </summary>
	public class SBWaiter : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBWaiter class.
		/// </summary>
		public SBWaiter()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				// Beverages (chance-based)
				if (MyServerSettings.SellChance()) { Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Ale, StoreSalesListConstants.PRICE_BEVERAGE_BOTTLE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_BEVERAGE_BOTTLE_ALE, 0)); }
				if (MyServerSettings.SellChance()) { Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Wine, StoreSalesListConstants.PRICE_BEVERAGE_BOTTLE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_BEVERAGE_BOTTLE_WINE, 0)); }
				if (MyServerSettings.SellChance()) { Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Liquor, StoreSalesListConstants.PRICE_BEVERAGE_BOTTLE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_BEVERAGE_BOTTLE_LIQUOR, 0)); }
				if (MyServerSettings.SellChance()) { Add(new BeverageBuyInfo(typeof(Jug), BeverageType.Cider, StoreSalesListConstants.PRICE_JUG_CIDER, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_JUG_CIDER, 0)); }
				if (MyServerSettings.SellChance()) { Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Milk, StoreSalesListConstants.PRICE_PITCHER_MILK, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PITCHER_MILK, 0)); }

				// Pitchers (always available and chance-based)
				Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Ale, StoreSalesListConstants.PRICE_PITCHER_BEVERAGE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PITCHER_ALE, 0));
				Add(new GenericBuyInfo(typeof(Pitcher), StoreSalesListConstants.PRICE_PITCHER_EMPTY, Utility.Random(StoreSalesListConstants.QTY_PITCHER_EMPTY_MIN, StoreSalesListConstants.QTY_PITCHER_EMPTY_MAX), StoreSalesListConstants.ITEMID_PITCHER_CIDER, 0));
				if (MyServerSettings.SellChance()) { Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Cider, StoreSalesListConstants.PRICE_PITCHER_BEVERAGE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PITCHER_CIDER, 0)); }
				if (MyServerSettings.SellChance()) { Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Liquor, StoreSalesListConstants.PRICE_PITCHER_BEVERAGE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PITCHER_LIQUOR, 0)); }
				if (MyServerSettings.SellChance()) { Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Wine, StoreSalesListConstants.PRICE_PITCHER_BEVERAGE, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PITCHER_WINE, 0)); }
				if (MyServerSettings.SellChance()) { Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Water, StoreSalesListConstants.PRICE_PITCHER_WATER, Utility.Random(StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100), StoreSalesListConstants.ITEMID_PITCHER_WATER, 0)); }

				// Food (always available and chance-based)
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(BreadLoaf), StoreSalesListConstants.PRICE_BREAD_LOAF_WAITER, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_BREAD_PIE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CheeseWheel), StoreSalesListConstants.PRICE_CHEESE_WHEEL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_CHEESE_WHEEL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CookedBird), StoreSalesListConstants.PRICE_COOKED_BIRD, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_COOKED_BIRD);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(LambLeg), StoreSalesListConstants.PRICE_LAMB_LEG, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_LAMB_LEG);

				// Bowls and meals (chance-based)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfCarrots), StoreSalesListConstants.PRICE_WOODEN_BOWL_CARROTS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WOODEN_BOWL_CARROTS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfCorn), StoreSalesListConstants.PRICE_WOODEN_BOWL_CORN, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WOODEN_BOWL_CORN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfLettuce), StoreSalesListConstants.PRICE_WOODEN_BOWL_LETTUCE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WOODEN_BOWL_LETTUCE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfPeas), StoreSalesListConstants.PRICE_WOODEN_BOWL_PEAS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WOODEN_BOWL_PEAS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(EmptyPewterBowl), StoreSalesListConstants.PRICE_EMPTY_PEWTER_BOWL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_EMPTY_PEWTER_BOWL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfCorn), StoreSalesListConstants.PRICE_PEWTER_BOWL_CORN, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PEWTER_BOWL_CORN);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfLettuce), StoreSalesListConstants.PRICE_PEWTER_BOWL_LETTUCE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PEWTER_BOWL_LETTUCE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfPeas), StoreSalesListConstants.PRICE_PEWTER_BOWL_PEAS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PEWTER_BOWL_PEAS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(PewterBowlOfFoodPotatos), StoreSalesListConstants.PRICE_PEWTER_BOWL_POTATOS, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_PEWTER_BOWL_POTATOS);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfStew), StoreSalesListConstants.PRICE_WOODEN_BOWL_STEW, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WOODEN_BOWL_STEW);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WoodenBowlOfTomatoSoup), StoreSalesListConstants.PRICE_WOODEN_BOWL_TOMATO_SOUP, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_WOODEN_BOWL_TOMATO_SOUP);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(ApplePie), StoreSalesListConstants.PRICE_APPLE_PIE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_APPLE_PIE);
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				// No items to buy from players
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBWeaponSmith: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBWeaponSmith() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WeaponAbilityBook ), 5, Utility.Random( 1,100 ), 0x2254, 0 ) ); }
			}
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( RareAnvil ), Utility.Random( 200,1000 ) ); } 
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBWeaver: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBWeaver() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{
                //Always
                Add(new GenericBuyInfo(typeof(Scissors), 9, 9, 0xF9F, 0));
                Add(new GenericBuyInfo(typeof(Dyes), 5, Utility.Random(5, 30), 0xFA9, 0));
                Add(new GenericBuyInfo(typeof(DyeTub), 8, Utility.Random(3, 15), 0xFAB, 0));
                Add(new GenericBuyInfo(typeof(Cotton), 5, Utility.Random(10, 20), 0xDF9, 0));
                Add(new GenericBuyInfo(typeof(Wool), 4, Utility.Random(10, 20), 0xDF8, 0));
                Add(new GenericBuyInfo(typeof(Flax), 6, Utility.Random(10, 20), 0x1A9C, 0));

				// 
                if (MyServerSettings.SellRareChance()) { Add(new GenericBuyInfo(typeof(Silk), 21, Utility.Random(5, 15), 0xE1F, 2173)); }
                if (MyServerSettings.SellCommonChance()) { Add(new GenericBuyInfo(typeof(DarkYarn), 36, Utility.Random(20, 50), 0xE1D, 0)); }
                if (MyServerSettings.SellCommonChance()) { Add(new GenericBuyInfo(typeof(LightYarn), 36, Utility.Random(20, 50), 0xE1E, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(BoltOfCloth), 100, Utility.Random(5, 15), 0xf95, 0)); }
                if (MyServerSettings.SellVeryRareChance()) { Add(new GenericBuyInfo(typeof(PaintCanvas), 500, Utility.Random(1, 5), 0xA6C, 0x47E)); }

                /*                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( UncutCloth ), 3, Utility.Random( 1,100 ), 0x1761, 0 ) ); } 
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( UncutCloth ), 3, Utility.Random( 1,100 ), 0x1762, 0 ) ); } 
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( UncutCloth ), 3, Utility.Random( 1,100 ), 0x1763, 0 ) ); } 
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( UncutCloth ), 3, Utility.Random( 1,100 ), 0x1764, 0 ) ); } 
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoltOfCloth ), 100, Utility.Random( 1,100 ), 0xf9B, 0 ) ); } 
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoltOfCloth ), 100, Utility.Random( 1,100 ), 0xf9C, 0 ) ); } 
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoltOfCloth ), 100, Utility.Random( 1,100 ), 0xf96, 0 ) ); } 
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoltOfCloth ), 100, Utility.Random( 1,100 ), 0xf97, 0 ) ); } */

                /*if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LightYarnUnraveled ), 18, Utility.Random( 1,100 ), 0xE1F, 0 ) ); }*/
                /*if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(Cloth), 2, Utility.Random(1, 15), 0x1766, Utility.RandomDyedHue())); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(UncutCloth), 2, Utility.Random(1, 15), 0x1767, Utility.RandomDyedHue())); }*/
            } 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Scissors ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dyes ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DyeTub ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( UncutCloth ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoltOfCloth ), 70 ); }  
				/*if ( MyServerSettings.BuyChance() ){Add( typeof( LightYarnUnraveled ), 9 ); } */
				if ( MyServerSettings.BuyChance() ){Add( typeof( LightYarn ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DarkYarn ), 9 ); } 
				if ( MyServerSettings.BuyVeryRareChance() ){Add( typeof( PaintCanvas ), 250 ); } 
			} 
		} 
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Necro Mage vendor.
	/// Sells necrotic reagents, alchemy tools, necrotic scrolls, and dark magic supplies.
	/// </summary>
	public class SBNecroMage : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBNecroMage class.
		/// </summary>
		public SBNecroMage()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				AddReagents();
				AddTools();
				AddSpecialItems();
			}

			/// <summary>
			/// Adds necrotic reagents to the buy list.
			/// </summary>
			private void AddReagents()
			{
				Add(new GenericBuyInfo(typeof(BatWing), StoreSalesListConstants.PRICE_BAT_WING_NECRO, StoreSalesListConstants.QTY_REAGENTS_NECRO_MIN, StoreSalesListConstants.ITEMID_BAT_WING, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(DaemonBlood), StoreSalesListConstants.PRICE_DAEMON_BLOOD, StoreSalesListConstants.QTY_REAGENTS_NECRO_MIN, StoreSalesListConstants.ITEMID_DAEMON_BLOOD, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(PigIron), StoreSalesListConstants.PRICE_PIG_IRON, StoreSalesListConstants.QTY_REAGENTS_NECRO_MIN, StoreSalesListConstants.ITEMID_PIG_IRON, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(NoxCrystal), StoreSalesListConstants.PRICE_NOX_CRYSTAL, StoreSalesListConstants.QTY_REAGENTS_NECRO_MIN, StoreSalesListConstants.ITEMID_NOX_CRYSTAL, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(GraveDust), StoreSalesListConstants.PRICE_GRAVE_DUST, StoreSalesListConstants.QTY_REAGENTS_NECRO_MIN, StoreSalesListConstants.ITEMID_GRAVE_DUST, StoreSalesListConstants.HUE_DEFAULT));
			}

			/// <summary>
			/// Adds tools and scrolls to the buy list.
			/// </summary>
			private void AddTools()
			{
				// Scrolls
				Add(new GenericBuyInfo(typeof(BloodOathScroll), StoreSalesListConstants.PRICE_BLOOD_OATH_SCROLL, 5, StoreSalesListConstants.ITEMID_BLOOD_OATH_SCROLL, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(CorpseSkinScroll), StoreSalesListConstants.PRICE_CORPSE_SKIN_SCROLL, 5, StoreSalesListConstants.ITEMID_CORPSE_SKIN_SCROLL, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(CurseWeaponScroll), StoreSalesListConstants.PRICE_CURSE_WEAPON_SCROLL, 5, StoreSalesListConstants.ITEMID_CURSE_WEAPON_SCROLL, StoreSalesListConstants.HUE_DEFAULT));

				// Tools
				Add(new GenericBuyInfo(typeof(PolishBoneBrush), StoreSalesListConstants.PRICE_POLISH_BONE_BRUSH, 10, StoreSalesListConstants.ITEMID_POLISH_BONE_BRUSH, StoreSalesListConstants.HUE_DEFAULT));
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(GraveShovel), StoreSalesListConstants.PRICE_GRAVE_SHOVEL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_GRAVE_SHOVEL, StoreSalesListConstants.HUE_GRAVE_SHOVEL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SurgeonsKnife), StoreSalesListConstants.PRICE_SURGEONS_KNIFE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SURGEONS_KNIFE, StoreSalesListConstants.HUE_SURGEONS_KNIFE);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Jar), StoreSalesListConstants.PRICE_JAR, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_JAR);
				Add(new GenericBuyInfo(typeof(MixingCauldron), StoreSalesListConstants.PRICE_MIXING_CAULDRON, 1, StoreSalesListConstants.ITEMID_MIXING_CAULDRON, StoreSalesListConstants.HUE_DEFAULT));
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(MixingSpoon), StoreSalesListConstants.PRICE_MIXING_SPOON_NECRO, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_MIXING_SPOON_NECRO, StoreSalesListConstants.HUE_MIXING_SPOON_NECRO);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CBookNecroticAlchemy), StoreSalesListConstants.PRICE_CBOOK_NECROTIC_ALCHEMY, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_CBOOK_NECROTIC_ALCHEMY, StoreSalesListConstants.HUE_CBOOK_NECROTIC_ALCHEMY);
			}

			/// <summary>
			/// Adds special and magical items to the buy list.
			/// </summary>
			private void AddSpecialItems()
			{
				// Alchemy tub (with chance)
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo(typeof(AlchemyTub), StoreSalesListConstants.PRICE_ALCHEMY_TUB, Utility.Random(StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX), StoreSalesListConstants.ITEMID_ALCHEMY_TUB, StoreSalesListConstants.HUE_DEFAULT));
				}

				// Magical staves and tools (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WizardStaff), StoreSalesListConstants.PRICE_WIZARD_STAFF, StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX, StoreSalesListConstants.ITEMID_WIZARD_STAFF, StoreSalesListConstants.HUE_WIZARD_STAFF);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WizardStick), StoreSalesListConstants.PRICE_WIZARD_STICK, StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX, StoreSalesListConstants.ITEMID_WIZARD_STICK, StoreSalesListConstants.HUE_WIZARD_STICK);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(MageEye), StoreSalesListConstants.PRICE_MAGE_EYE, StoreSalesListConstants.QTY_MEDIUM_MIN, StoreSalesListConstants.QTY_MEDIUM_MAX, StoreSalesListConstants.ITEMID_MAGE_EYE, StoreSalesListConstants.HUE_MAGE_EYE);

				// Special items (always available)
				Add(new GenericBuyInfo(typeof(CorruptedMoonStone), StoreSalesListConstants.PRICE_CORRUPTED_MOON_STONE, StoreSalesListConstants.QTY_CORRUPTED_MOON_STONE, StoreSalesListConstants.ITEMID_CORRUPTED_MOON_STONE, StoreSalesListConstants.HUE_DEFAULT));
			}
		}

		#endregion

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				AddReagents();
				AddScrolls();
				AddTools();
				AddBodyParts();
				AddContainers();
			}

			/// <summary>
			/// Adds necrotic reagents to the sell list.
			/// </summary>
			private void AddReagents()
			{
				Add(typeof(BatWing), StoreSalesListConstants.SELL_PRICE_BAT_WING);
				Add(typeof(DaemonBlood), StoreSalesListConstants.SELL_PRICE_DAEMON_BLOOD);
				Add(typeof(PigIron), StoreSalesListConstants.SELL_PRICE_PIG_IRON);
				Add(typeof(NoxCrystal), StoreSalesListConstants.SELL_PRICE_NOX_CRYSTAL);
				Add(typeof(GraveDust), StoreSalesListConstants.SELL_PRICE_GRAVE_DUST);
			}

			/// <summary>
			/// Adds necrotic scrolls to the sell list.
			/// </summary>
			private void AddScrolls()
			{
				Add(typeof(ExorcismScroll), StoreSalesListConstants.SELL_PRICE_EXORCISM_SCROLL);
				Add(typeof(AnimateDeadScroll), StoreSalesListConstants.SELL_PRICE_ANIMATE_DEAD_SCROLL);
				Add(typeof(BloodOathScroll), StoreSalesListConstants.SELL_PRICE_BLOOD_OATH_SCROLL);
				Add(typeof(CorpseSkinScroll), StoreSalesListConstants.SELL_PRICE_CORPSE_SKIN_SCROLL);
				Add(typeof(CurseWeaponScroll), StoreSalesListConstants.SELL_PRICE_CURSE_WEAPON_SCROLL);
				Add(typeof(EvilOmenScroll), StoreSalesListConstants.SELL_PRICE_EVIL_OMEN_SCROLL);
				Add(typeof(PainSpikeScroll), StoreSalesListConstants.SELL_PRICE_PAIN_SPIKE_SCROLL);
				Add(typeof(SummonFamiliarScroll), StoreSalesListConstants.SELL_PRICE_SUMMON_FAMILIAR_SCROLL);
				Add(typeof(HorrificBeastScroll), StoreSalesListConstants.SELL_PRICE_HORRIFIC_BEAST_SCROLL);
				Add(typeof(MindRotScroll), StoreSalesListConstants.SELL_PRICE_MIND_ROT_SCROLL);
				Add(typeof(PoisonStrikeScroll), StoreSalesListConstants.SELL_PRICE_POISON_STRIKE_SCROLL);
				Add(typeof(WraithFormScroll), StoreSalesListConstants.SELL_PRICE_WRAITH_FORM_SCROLL);
				Add(typeof(LichFormScroll), StoreSalesListConstants.SELL_PRICE_LICH_FORM_SCROLL);
				Add(typeof(StrangleScroll), StoreSalesListConstants.SELL_PRICE_STRANGLE_SCROLL);
				Add(typeof(WitherScroll), StoreSalesListConstants.SELL_PRICE_WITHER_SCROLL);
				Add(typeof(VampiricEmbraceScroll), StoreSalesListConstants.SELL_PRICE_VAMPIRIC_EMBRACE_SCROLL);
				Add(typeof(VengefulSpiritScroll), StoreSalesListConstants.SELL_PRICE_VENGEFUL_SPIRIT_SCROLL);
			}

			/// <summary>
			/// Adds tools and special items to the sell list.
			/// </summary>
			private void AddTools()
			{
				Add(typeof(PolishBoneBrush), StoreSalesListConstants.SELL_PRICE_POLISH_BONE_BRUSH);
				Add(typeof(PolishedSkull), StoreSalesListConstants.SELL_PRICE_POLISHED_SKULL);
				Add(typeof(PolishedBone), StoreSalesListConstants.SELL_PRICE_POLISHED_BONE);

				if (MyServerSettings.BuyChance())
				{
					Add(typeof(MixingCauldron), StoreSalesListConstants.SELL_PRICE_MIXING_CAULDRON_NECRO);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(MixingSpoon), StoreSalesListConstants.SELL_PRICE_MIXING_SPOON_NECRO);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SurgeonsKnife), StoreSalesListConstants.SELL_PRICE_SURGEONS_KNIFE_NECRO);
				}

				if (MyServerSettings.BuyChance())
				{
					Add(typeof(WoodenCoffin), StoreSalesListConstants.SELL_PRICE_WOODEN_COFFIN);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(WoodenCasket), StoreSalesListConstants.SELL_PRICE_WOODEN_CASKET);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(StoneCoffin), StoreSalesListConstants.SELL_PRICE_STONE_COFFIN);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(StoneCasket), StoreSalesListConstants.SELL_PRICE_STONE_CASKET);
				}

				if (MyServerSettings.BuyChance())
				{
					Add(typeof(DracolichSkull), Utility.Random(StoreSalesListConstants.SELL_PRICE_DRACOLICH_SKULL_MIN, StoreSalesListConstants.SELL_PRICE_DRACOLICH_SKULL_MAX));
				}

				if (MyServerSettings.BuyChance())
				{
					Add(typeof(WizardStaff), StoreSalesListConstants.SELL_PRICE_WIZARD_STAFF_NECRO);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(WizardStick), StoreSalesListConstants.SELL_PRICE_WIZARD_STICK_NECRO);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(MageEye), StoreSalesListConstants.SELL_PRICE_MAGE_EYE_NECRO);
				}
			}

			/// <summary>
			/// Adds body parts and skulls to the sell list.
			/// </summary>
			private void AddBodyParts()
			{
				// Corpses and bodies
				Add(typeof(CorpseSailor), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_CORPSE_SAILOR_MIN, StoreSalesListConstants.SELL_PRICE_CORPSE_SAILOR_MAX));
				Add(typeof(CorpseChest), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_CORPSE_CHEST_MIN, StoreSalesListConstants.SELL_PRICE_CORPSE_CHEST_MAX));
				Add(typeof(BuriedBody), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_BURIED_BODY_MIN, StoreSalesListConstants.SELL_PRICE_BURIED_BODY_MAX));
				Add(typeof(BoneContainer), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_BONE_CONTAINER_MIN, StoreSalesListConstants.SELL_PRICE_BONE_CONTAINER_MAX));

				// Body parts
				Add(typeof(LeftLeg), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_LEFT_LEG_MIN, StoreSalesListConstants.SELL_PRICE_LEFT_LEG_MAX));
				Add(typeof(RightLeg), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_RIGHT_LEG_MIN, StoreSalesListConstants.SELL_PRICE_RIGHT_LEG_MAX));
				Add(typeof(TastyHeart), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_TASTY_HEART_MIN, StoreSalesListConstants.SELL_PRICE_TASTY_HEART_MAX));
				Add(typeof(BodyPart), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_BODY_PART_MIN, StoreSalesListConstants.SELL_PRICE_BODY_PART_MAX));
				Add(typeof(Head), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_HEAD_MIN, StoreSalesListConstants.SELL_PRICE_HEAD_MAX));
				Add(typeof(LeftArm), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_LEFT_ARM_MIN, StoreSalesListConstants.SELL_PRICE_LEFT_ARM_MAX));
				Add(typeof(RightArm), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_RIGHT_ARM_MIN, StoreSalesListConstants.SELL_PRICE_RIGHT_ARM_MAX));
				Add(typeof(Torso), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_TORSO_MIN, StoreSalesListConstants.SELL_PRICE_TORSO_MAX));
				Add(typeof(Bone), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_BONE_MIN, StoreSalesListConstants.SELL_PRICE_BONE_MAX));
				Add(typeof(RibCage), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_RIB_CAGE_MIN, StoreSalesListConstants.SELL_PRICE_RIB_CAGE_MAX));
				Add(typeof(BonePile), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_BONE_PILE_MIN, StoreSalesListConstants.SELL_PRICE_BONE_PILE_MAX));
				Add(typeof(Bones), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_BONES_MIN, StoreSalesListConstants.SELL_PRICE_BONES_MAX));

				// Monster skulls
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SkullMinotaur), Utility.Random(StoreSalesListConstants.SELL_PRICE_SKULL_MINOTAUR_MIN, StoreSalesListConstants.SELL_PRICE_SKULL_MINOTAUR_MAX));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SkullWyrm), Utility.Random(StoreSalesListConstants.SELL_PRICE_SKULL_WYRM_MIN, StoreSalesListConstants.SELL_PRICE_SKULL_WYRM_MAX));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SkullGreatDragon), Utility.Random(StoreSalesListConstants.SELL_PRICE_SKULL_GREAT_DRAGON_MIN, StoreSalesListConstants.SELL_PRICE_SKULL_GREAT_DRAGON_MAX));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SkullDragon), Utility.Random(StoreSalesListConstants.SELL_PRICE_SKULL_DRAGON_MIN, StoreSalesListConstants.SELL_PRICE_SKULL_DRAGON_MAX));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SkullDemon), Utility.Random(StoreSalesListConstants.SELL_PRICE_SKULL_DEMON_MIN, StoreSalesListConstants.SELL_PRICE_SKULL_DEMON_MAX));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SkullGiant), Utility.Random(StoreSalesListConstants.SELL_PRICE_SKULL_GIANT_MIN, StoreSalesListConstants.SELL_PRICE_SKULL_GIANT_MAX));
				}
			}

			/// <summary>
			/// Adds containers and special items to the sell list.
			/// </summary>
			private void AddContainers()
			{
				Add(typeof(GraveChest), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_GRAVE_CHEST_MIN, StoreSalesListConstants.SELL_PRICE_GRAVE_CHEST_MAX));
				Add(typeof(AlchemyTub), Utility.Random(StoreSalesListConstants.SELL_PRICE_ALCHEMY_TUB_NECRO_MIN, StoreSalesListConstants.SELL_PRICE_ALCHEMY_TUB_NECRO_MAX));
			}
		}

		#endregion
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Necromancer vendor.
	/// Sells advanced necromantic supplies, spellbooks, mounts, and dark artifacts.
	/// </summary>
	public class SBNecromancer : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBNecromancer class.
		/// </summary>
		public SBNecromancer()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		/// <summary>
		/// Internal class for buy item definitions.
		/// </summary>
		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			/// <summary>
			/// Initializes the buy item list.
			/// </summary>
			public InternalBuyInfo()
			{
				AddSpellbooks();
				AddPotions();
				AddReagents();
				AddTools();
				AddMounts();
				AddSpecialItems();
			}

			/// <summary>
			/// Adds spellbooks to the buy list.
			/// </summary>
			private void AddSpellbooks()
			{
				Add(new GenericBuyInfo(typeof(NecromancerSpellbook), StoreSalesListConstants.PRICE_NECROMANCER_SPELLBOOK, 1, StoreSalesListConstants.ITEMID_NECROMANCER_SPELLBOOK, StoreSalesListConstants.HUE_DEFAULT));
			}

			/// <summary>
			/// Adds potions to the buy list.
			/// </summary>
			private void AddPotions()
			{
				Add(new GenericBuyInfo(typeof(NecroSkinPotion), StoreSalesListConstants.PRICE_NECRO_SKIN_POTION, 1, StoreSalesListConstants.ITEMID_NECRO_SKIN_POTION, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(BookofDead), StoreSalesListConstants.PRICE_BOOK_OF_DEAD, 1, StoreSalesListConstants.ITEMID_BOOK_OF_DEAD, StoreSalesListConstants.HUE_BOOK_OF_DEAD));
				Add(new GenericBuyInfo(typeof(DarkHeart), StoreSalesListConstants.PRICE_DARK_HEART, 5, StoreSalesListConstants.ITEMID_DARK_HEART, StoreSalesListConstants.HUE_DARK_HEART));
			}

			/// <summary>
			/// Adds necrotic reagents to the buy list.
			/// </summary>
			private void AddReagents()
			{
				Add(new GenericBuyInfo(typeof(BatWing), StoreSalesListConstants.PRICE_BAT_WING_NECRO, StoreSalesListConstants.QTY_REAGENTS_NECRO_MIN, StoreSalesListConstants.ITEMID_BAT_WING, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(DaemonBlood), StoreSalesListConstants.PRICE_DAEMON_BLOOD, StoreSalesListConstants.QTY_REAGENTS_NECRO_MIN, StoreSalesListConstants.ITEMID_DAEMON_BLOOD, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(PigIron), StoreSalesListConstants.PRICE_PIG_IRON, StoreSalesListConstants.QTY_REAGENTS_NECRO_MIN, StoreSalesListConstants.ITEMID_PIG_IRON, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(NoxCrystal), StoreSalesListConstants.PRICE_NOX_CRYSTAL, StoreSalesListConstants.QTY_REAGENTS_NECRO_MIN, StoreSalesListConstants.ITEMID_NOX_CRYSTAL, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(GraveDust), StoreSalesListConstants.PRICE_GRAVE_DUST, StoreSalesListConstants.QTY_REAGENTS_NECRO_MIN, StoreSalesListConstants.ITEMID_GRAVE_DUST, StoreSalesListConstants.HUE_DEFAULT));
			}

			/// <summary>
			/// Adds tools and scrolls to the buy list.
			/// </summary>
			private void AddTools()
			{
				// Scrolls
				Add(new GenericBuyInfo(typeof(BloodOathScroll), StoreSalesListConstants.PRICE_BLOOD_OATH_SCROLL, 5, StoreSalesListConstants.ITEMID_BLOOD_OATH_SCROLL, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(CorpseSkinScroll), StoreSalesListConstants.PRICE_CORPSE_SKIN_SCROLL, 5, StoreSalesListConstants.ITEMID_CORPSE_SKIN_SCROLL, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(CurseWeaponScroll), StoreSalesListConstants.PRICE_CURSE_WEAPON_SCROLL, 5, StoreSalesListConstants.ITEMID_CURSE_WEAPON_SCROLL, StoreSalesListConstants.HUE_DEFAULT));

				// Tools
				Add(new GenericBuyInfo(typeof(PolishBoneBrush), StoreSalesListConstants.PRICE_POLISH_BONE_BRUSH, 10, StoreSalesListConstants.ITEMID_POLISH_BONE_BRUSH, StoreSalesListConstants.HUE_DEFAULT));
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(GraveShovel), StoreSalesListConstants.PRICE_GRAVE_SHOVEL, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_GRAVE_SHOVEL, StoreSalesListConstants.HUE_GRAVE_SHOVEL);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(SurgeonsKnife), StoreSalesListConstants.PRICE_SURGEONS_KNIFE, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_SURGEONS_KNIFE, StoreSalesListConstants.HUE_SURGEONS_KNIFE);
				Add(new GenericBuyInfo(typeof(MixingCauldron), StoreSalesListConstants.PRICE_MIXING_CAULDRON, 1, StoreSalesListConstants.ITEMID_MIXING_CAULDRON, StoreSalesListConstants.HUE_DEFAULT));
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(MixingSpoon), StoreSalesListConstants.PRICE_MIXING_SPOON_NECRO, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_MIXING_SPOON_NECRO, StoreSalesListConstants.HUE_MIXING_SPOON_NECRO);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(Jar), StoreSalesListConstants.PRICE_JAR, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_JAR);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(CBookNecroticAlchemy), StoreSalesListConstants.PRICE_CBOOK_NECROTIC_ALCHEMY, StoreSalesListConstants.QTY_RANDOM_SMALL_MIN, 100, StoreSalesListConstants.ITEMID_CBOOK_NECROTIC_ALCHEMY, StoreSalesListConstants.HUE_CBOOK_NECROTIC_ALCHEMY);
			}

			/// <summary>
			/// Adds mounts to the buy list.
			/// </summary>
			private void AddMounts()
			{
				Add(new GenericBuyInfo("undead horse", typeof(NecroHorse), StoreSalesListConstants.PRICE_NECRO_HORSE, 5, StoreSalesListConstants.ITEMID_NECRO_HORSE, StoreSalesListConstants.HUE_NECRO_HORSE));
				Add(new GenericBuyInfo("daemon servant", typeof(DaemonMount), StoreSalesListConstants.PRICE_DAEMON_MOUNT, 5, StoreSalesListConstants.ITEMID_DAEMON_MOUNT, StoreSalesListConstants.HUE_DAEMON_MOUNT));
			}

			/// <summary>
			/// Adds special and magical items to the buy list.
			/// </summary>
			private void AddSpecialItems()
			{
				// Alchemy tub (with chance)
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo(typeof(AlchemyTub), StoreSalesListConstants.PRICE_ALCHEMY_TUB, Utility.Random(StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX), StoreSalesListConstants.ITEMID_ALCHEMY_TUB, StoreSalesListConstants.HUE_DEFAULT));
				}

				// Magical staves and tools (with chance)
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WizardStaff), StoreSalesListConstants.PRICE_WIZARD_STAFF, StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX, StoreSalesListConstants.ITEMID_WIZARD_STAFF, StoreSalesListConstants.HUE_WIZARD_STAFF);
				StoreSalesListHelper.AddBuyItemWithChanceRandom(this, typeof(WizardStick), StoreSalesListConstants.PRICE_WIZARD_STICK, StoreSalesListConstants.QTY_TENT_MIN, StoreSalesListConstants.QTY_TENT_MAX, StoreSalesListConstants.ITEMID_WIZARD_STICK, StoreSalesListConstants.HUE_WIZARD_STICK);
				StoreSalesListHelper.AddBuyItemRandom(this, typeof(MageEye), StoreSalesListConstants.PRICE_MAGE_EYE, StoreSalesListConstants.QTY_MEDIUM_MIN, StoreSalesListConstants.QTY_MEDIUM_MAX, StoreSalesListConstants.ITEMID_MAGE_EYE, StoreSalesListConstants.HUE_MAGE_EYE);

				// Special items (always available)
				Add(new GenericBuyInfo(typeof(CorruptedMoonStone), StoreSalesListConstants.PRICE_CORRUPTED_MOON_STONE, StoreSalesListConstants.QTY_CORRUPTED_MOON_STONE, StoreSalesListConstants.ITEMID_CORRUPTED_MOON_STONE, StoreSalesListConstants.HUE_DEFAULT));
				Add(new GenericBuyInfo(typeof(BagOfNecroReagents), StoreSalesListConstants.PRICE_BAG_OF_NECRO_REAGENTS, StoreSalesListConstants.QTY_BAG_OF_NECRO_REAGENTS, StoreSalesListConstants.ITEMID_BAG_OF_NECRO_REAGENTS, StoreSalesListConstants.HUE_DEFAULT));

				// Bone grinder (with chance)
				if (MyServerSettings.SellChance())
				{
					Add(new GenericBuyInfo(typeof(BoneGrinder), StoreSalesListConstants.PRICE_BONE_GRINDER, Utility.Random(StoreSalesListConstants.QTY_BONE_GRINDER_MIN, StoreSalesListConstants.QTY_BONE_GRINDER_MAX), StoreSalesListConstants.ITEMID_BONE_GRINDER, StoreSalesListConstants.HUE_DEFAULT));
				}
			}
		}

		#endregion

				
			}
		}

		#region InternalSellInfo

		/// <summary>
		/// Internal class for sell item definitions.
		/// </summary>
		public class InternalSellInfo : GenericSellInfo
		{
			/// <summary>
			/// Initializes the sell item list.
			/// </summary>
			public InternalSellInfo()
			{
				AddReagents();
				AddScrolls();
				AddSpellbooks();
				AddTools();
				AddBodyParts();
				AddContainers();
				AddMagicStaves();
			}

			/// <summary>
			/// Adds necrotic reagents to the sell list.
			/// </summary>
			private void AddReagents()
			{
				Add(typeof(BatWing), StoreSalesListConstants.SELL_PRICE_BAT_WING);
				Add(typeof(DaemonBlood), StoreSalesListConstants.SELL_PRICE_DAEMON_BLOOD);
				Add(typeof(PigIron), StoreSalesListConstants.SELL_PRICE_PIG_IRON);
				Add(typeof(NoxCrystal), StoreSalesListConstants.SELL_PRICE_NOX_CRYSTAL);
				Add(typeof(GraveDust), StoreSalesListConstants.SELL_PRICE_GRAVE_DUST);
			}

			/// <summary>
			/// Adds necrotic scrolls to the sell list.
			/// </summary>
			private void AddScrolls()
			{
				Add(typeof(ExorcismScroll), StoreSalesListConstants.SELL_PRICE_EXORCISM_SCROLL);
				Add(typeof(AnimateDeadScroll), StoreSalesListConstants.SELL_PRICE_ANIMATE_DEAD_SCROLL);
				Add(typeof(BloodOathScroll), StoreSalesListConstants.SELL_PRICE_BLOOD_OATH_SCROLL);
				Add(typeof(CorpseSkinScroll), StoreSalesListConstants.SELL_PRICE_CORPSE_SKIN_SCROLL);
				Add(typeof(CurseWeaponScroll), StoreSalesListConstants.SELL_PRICE_CURSE_WEAPON_SCROLL);
				Add(typeof(EvilOmenScroll), StoreSalesListConstants.SELL_PRICE_EVIL_OMEN_SCROLL);
				Add(typeof(PainSpikeScroll), StoreSalesListConstants.SELL_PRICE_PAIN_SPIKE_SCROLL);
				Add(typeof(SummonFamiliarScroll), StoreSalesListConstants.SELL_PRICE_SUMMON_FAMILIAR_SCROLL);
				Add(typeof(HorrificBeastScroll), StoreSalesListConstants.SELL_PRICE_HORRIFIC_BEAST_SCROLL);
				Add(typeof(MindRotScroll), StoreSalesListConstants.SELL_PRICE_MIND_ROT_SCROLL);
				Add(typeof(PoisonStrikeScroll), StoreSalesListConstants.SELL_PRICE_POISON_STRIKE_SCROLL);
				Add(typeof(WraithFormScroll), StoreSalesListConstants.SELL_PRICE_WRAITH_FORM_SCROLL);
				Add(typeof(LichFormScroll), StoreSalesListConstants.SELL_PRICE_LICH_FORM_SCROLL);
				Add(typeof(StrangleScroll), StoreSalesListConstants.SELL_PRICE_STRANGLE_SCROLL);
				Add(typeof(WitherScroll), StoreSalesListConstants.SELL_PRICE_WITHER_SCROLL);
				Add(typeof(VampiricEmbraceScroll), StoreSalesListConstants.SELL_PRICE_VAMPIRIC_EMBRACE_SCROLL);
				Add(typeof(VengefulSpiritScroll), StoreSalesListConstants.SELL_PRICE_VENGEFUL_SPIRIT_SCROLL);
			}

			/// <summary>
			/// Adds spellbooks to the sell list.
			/// </summary>
			private void AddSpellbooks()
			{
				Add(typeof(NecromancerSpellbook), StoreSalesListConstants.SELL_PRICE_NECROMANCER_SPELLBOOK);
				Add(typeof(DeathKnightSpellbook), Utility.Random(StoreSalesListConstants.SELL_PRICE_DEATH_KNIGHT_SPELLBOOK_MIN, StoreSalesListConstants.SELL_PRICE_DEATH_KNIGHT_SPELLBOOK_MAX));
			}

			/// <summary>
			/// Adds tools and special items to the sell list.
			/// </summary>
			private void AddTools()
			{
				Add(typeof(PolishBoneBrush), StoreSalesListConstants.SELL_PRICE_POLISH_BONE_BRUSH);
				Add(typeof(PolishedSkull), StoreSalesListConstants.SELL_PRICE_POLISHED_SKULL);
				Add(typeof(PolishedBone), StoreSalesListConstants.SELL_PRICE_POLISHED_BONE);

				if (MyServerSettings.BuyChance())
				{
					Add(typeof(BoneContainer), StoreSalesListConstants.SELL_PRICE_BONE_CONTAINER_NECRO);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(MixingCauldron), StoreSalesListConstants.SELL_PRICE_MIXING_CAULDRON_NECROMANCER);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(MixingSpoon), StoreSalesListConstants.SELL_PRICE_MIXING_SPOON_NECROMANCER);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SurgeonsKnife), StoreSalesListConstants.SELL_PRICE_SURGEONS_KNIFE_NECROMANCER);
				}

				if (MyServerSettings.BuyChance())
				{
					Add(typeof(WoodenCoffin), StoreSalesListConstants.SELL_PRICE_WOODEN_COFFIN_NECROMANCER);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(WoodenCasket), StoreSalesListConstants.SELL_PRICE_WOODEN_CASKET_NECROMANCER);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(StoneCoffin), StoreSalesListConstants.SELL_PRICE_STONE_COFFIN_NECROMANCER);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(StoneCasket), StoreSalesListConstants.SELL_PRICE_STONE_CASKET_NECROMANCER);
				}

				if (MyServerSettings.BuyChance())
				{
					Add(typeof(DemonPrison), Utility.Random(StoreSalesListConstants.SELL_PRICE_DEMON_PRISON_MIN, StoreSalesListConstants.SELL_PRICE_DEMON_PRISON_MAX));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(DracolichSkull), Utility.Random(StoreSalesListConstants.SELL_PRICE_DRACOLICH_SKULL_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_DRACOLICH_SKULL_NECROMANCER_MAX));
				}

				if (MyServerSettings.BuyChance())
				{
					Add(typeof(WizardStaff), StoreSalesListConstants.SELL_PRICE_WIZARD_STAFF_NECROMANCER);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(WizardStick), StoreSalesListConstants.SELL_PRICE_WIZARD_STICK_NECROMANCER);
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(MageEye), StoreSalesListConstants.SELL_PRICE_MAGE_EYE_NECROMANCER);
				}

				if (MyServerSettings.BuyRareChance())
				{
					Add(typeof(MyNecromancerSpellbook), Utility.Random(StoreSalesListConstants.SELL_PRICE_MY_NECROMANCER_SPELLBOOK_MIN, StoreSalesListConstants.SELL_PRICE_MY_NECROMANCER_SPELLBOOK_MAX));
				}
			}

			/// <summary>
			/// Adds body parts and skulls to the sell list.
			/// </summary>
			private void AddBodyParts()
			{
				// Corpses and bodies
				Add(typeof(CorpseSailor), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_CORPSE_SAILOR_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_CORPSE_SAILOR_NECROMANCER_MAX));
				Add(typeof(CorpseChest), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_CORPSE_CHEST_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_CORPSE_CHEST_NECROMANCER_MAX));
				Add(typeof(BodyPart), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_BODY_PART_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_BODY_PART_NECROMANCER_MAX));
				Add(typeof(BuriedBody), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_BURIED_BODY_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_BURIED_BODY_NECROMANCER_MAX));
				Add(typeof(BoneContainer), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_BONE_CONTAINER_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_BONE_CONTAINER_NECROMANCER_MAX));

				// Body parts
				Add(typeof(LeftLeg), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_LEFT_LEG_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_LEFT_LEG_NECROMANCER_MAX));
				Add(typeof(RightLeg), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_RIGHT_LEG_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_RIGHT_LEG_NECROMANCER_MAX));
				Add(typeof(TastyHeart), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_TASTY_HEART_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_TASTY_HEART_NECROMANCER_MAX));
				Add(typeof(Head), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_HEAD_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_HEAD_NECROMANCER_MAX));
				Add(typeof(LeftArm), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_LEFT_ARM_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_LEFT_ARM_NECROMANCER_MAX));
				Add(typeof(RightArm), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_RIGHT_ARM_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_RIGHT_ARM_NECROMANCER_MAX));
				Add(typeof(Torso), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_TORSO_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_TORSO_NECROMANCER_MAX));
				Add(typeof(Bone), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_BONE_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_BONE_NECROMANCER_MAX));
				Add(typeof(RibCage), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_RIB_CAGE_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_RIB_CAGE_NECROMANCER_MAX));
				Add(typeof(BonePile), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_BONE_PILE_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_BONE_PILE_NECROMANCER_MAX));
				Add(typeof(Bones), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_BONES_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_BONES_NECROMANCER_MAX));

				// Monster skulls
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SkullDragon), Utility.Random(StoreSalesListConstants.SELL_PRICE_SKULL_DRAGON_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_SKULL_DRAGON_NECROMANCER_MAX));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SkullDemon), Utility.Random(StoreSalesListConstants.SELL_PRICE_SKULL_DEMON_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_SKULL_DEMON_NECROMANCER_MAX));
				}
				if (MyServerSettings.BuyChance())
				{
					Add(typeof(SkullGiant), Utility.Random(StoreSalesListConstants.SELL_PRICE_SKULL_GIANT_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_SKULL_GIANT_NECROMANCER_MAX));
				}
			}

			/// <summary>
			/// Adds containers and special items to the sell list.
			/// </summary>
			private void AddContainers()
			{
				Add(typeof(GraveChest), Utility.RandomMinMax(StoreSalesListConstants.SELL_PRICE_GRAVE_CHEST_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_GRAVE_CHEST_NECROMANCER_MAX));
				Add(typeof(AlchemyTub), Utility.Random(StoreSalesListConstants.SELL_PRICE_ALCHEMY_TUB_NECROMANCER_MIN, StoreSalesListConstants.SELL_PRICE_ALCHEMY_TUB_NECROMANCER_MAX));
			}

			/// <summary>
			/// Adds magic staves to the sell list organized by spell circle.
			/// </summary>
			private void AddMagicStaves()
			{
				// 1st Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(ClumsyMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(CreateFoodMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(FeebleMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(HealMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(MagicArrowMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(NightSightMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(ReactiveArmorMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);
				AddMagicStaffWithRareChance(typeof(WeaknessMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_1ST_MAX);

				// 2nd Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(AgilityMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(CunningMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(CureMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(HarmMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(MagicTrapMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(MagicUntrapMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(ProtectionMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);
				AddMagicStaffWithRareChance(typeof(StrengthMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_2ND_MAX);

				// 3rd Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(BlessMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(FireballMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(MagicLockMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(MagicUnlockMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(PoisonMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(TelekinesisMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(TeleportMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);
				AddMagicStaffWithRareChance(typeof(WallofStoneMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_3RD_MAX);

				// 4th Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(ArchCureMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(ArchProtectionMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(CurseMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(FireFieldMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(GreaterHealMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(LightningMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(ManaDrainMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);
				AddMagicStaffWithRareChance(typeof(RecallMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_4TH_MAX);

				// 5th Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(BladeSpiritsMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(DispelFieldMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(IncognitoMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(MagicReflectionMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(MindBlastMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(ParalyzeMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(PoisonFieldMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);
				AddMagicStaffWithRareChance(typeof(SummonCreatureMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_5TH_MAX);

				// 6th Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(DispelMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(EnergyBoltMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(ExplosionMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(InvisibilityMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(MarkMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(MassCurseMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(ParalyzeFieldMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);
				AddMagicStaffWithRareChance(typeof(RevealMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_6TH_MAX);

				// 7th Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(ChainLightningMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(EnergyFieldMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(FlameStrikeMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(GateTravelMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(ManaVampireMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(MassDispelMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(MeteorSwarmMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);
				AddMagicStaffWithRareChance(typeof(PolymorphMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_7TH_MAX);

				// 8th Circle Magic Staves
				AddMagicStaffWithRareChance(typeof(AirElementalMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(EarthElementalMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(EarthquakeMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(EnergyVortexMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(FireElementalMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(ResurrectionMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(SummonDaemonMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
				AddMagicStaffWithRareChance(typeof(WaterElementalMagicStaff), StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MIN, StoreSalesListConstants.SELL_PRICE_MAGIC_STAFF_8TH_MAX);
			}

			/// <summary>
			/// Helper method to add a magic staff with rare chance and random pricing.
			/// </summary>
			private void AddMagicStaffWithRareChance(Type staffType, int minPrice, int maxPrice)
			{
				if (MyServerSettings.BuyRareChance())
				{
					Add(staffType, Utility.Random(minPrice, maxPrice));
				}
			}
		}

		#endregion
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBWitches : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBWitches()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 115, 1, 0x2253, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BatWing ), 3, Utility.Random( 10,100 ), 0xF78, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DaemonBlood ), 6, Utility.Random( 10,100 ), 0xF7D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PigIron ), 5, Utility.Random( 10,100 ), 0xF8A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NoxCrystal ), 6, Utility.Random( 10,100 ), 0xF8E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GraveDust ), 3, Utility.Random( 10,100 ), 0xF8F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BloodOathScroll ), 25, 5, 0x2263, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CorpseSkinScroll ), 28, 5, 0x2263, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CurseWeaponScroll ), 12, 5, 0x2263, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TarotDeck ), 5, Utility.Random( 1,100 ), 0x12AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PolishBoneBrush ), 12, 10, 0x1371, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GraveShovel ), 12, Utility.Random( 1,100 ), 0xF39, 0x966 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( SurgeonsKnife ), 14, Utility.Random( 1,100 ), 0xEC4, 0x1B0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingCauldron ), 247, 1, 0x269C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 1,100 ), 0x1E27, 0x979 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 50, Utility.Random( 1,100 ), 0x2253, 0x4AA ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BlackDyeTub ), 5000, 1, 0xFAB, 0x1 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( reagents_magic_jar2 ), 1500, Utility.Random( 1,100 ), 0x1007, 0xB97 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HellsGateScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x54F ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ManaLeechScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xB87 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NecroCurePoisonScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x8A2 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NecroPoisonScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x4F8 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NecroUnlockScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x493 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PhantasmScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x6DE ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( RetchedAirScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xA97 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( SpectreShadowScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x17E ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( UndeadEyesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x491 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( VampireGiftScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xB85 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( WallOfSpikesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xB8F ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( BloodPactScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x5B5 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GhostlyImagesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xBF ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GhostPhaseScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x47E ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GraveyardGatewayScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x2EA ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HellsBrandScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x54C ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStaff ), 40, Utility.Random( 1,5 ), 0x0908, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStick ), 38, Utility.Random( 1,5 ), 0xDF2, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BatWing ), 1 );
				Add( typeof( DaemonBlood ), 3 );
				Add( typeof( PigIron ), 2 );
				Add( typeof( NoxCrystal ), 3 );
				Add( typeof( GraveDust ), 1 );
				Add( typeof( ExorcismScroll ), 1 );
				Add( typeof( AnimateDeadScroll ), 26 );
				Add( typeof( BloodOathScroll ), 26 );
				Add( typeof( CorpseSkinScroll ), 26 );
				Add( typeof( CurseWeaponScroll ), 26 );
				Add( typeof( EvilOmenScroll ), 26 );
				Add( typeof( PainSpikeScroll ), 26 );
				Add( typeof( SummonFamiliarScroll ), 26 );
				Add( typeof( HorrificBeastScroll ), 27 );
				Add( typeof( MindRotScroll ), 39 );
				Add( typeof( PoisonStrikeScroll ), 39 );
				Add( typeof( WraithFormScroll ), 51 );
				Add( typeof( LichFormScroll ), 64 );
				Add( typeof( StrangleScroll ), 64 );
				Add( typeof( WitherScroll ), 64 );
				Add( typeof( VampiricEmbraceScroll ), 101 );
				Add( typeof( VengefulSpiritScroll ), 114 );
				Add( typeof( PolishBoneBrush ), 6 );
				Add( typeof( PolishedSkull ), 3 );
				Add( typeof( PolishedBone ), 3 );
				Add( typeof( NecromancerSpellbook ), 55 );
				Add( typeof( DeathKnightSpellbook ), Utility.Random( 100,300 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullMinotaur ), Utility.Random( 50,150 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullWyrm ), Utility.Random( 200,400 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullGreatDragon ), Utility.Random( 300,600 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDragon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDemon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullGiant ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){ Add( typeof( MixingCauldron ), 123 ); }  
				if ( MyServerSettings.BuyChance() ){ Add( typeof( MixingSpoon ), 17 ); }  
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MyNecromancerSpellbook ), Utility.Random( 250,1000 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BlackDyeTub ), 2500 ); }  
				Add( typeof( CorpseSailor ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( CorpseChest ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BuriedBody ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BodyPart ), Utility.RandomMinMax( 30, 90 ) );
				Add( typeof( BoneContainer ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( LeftLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( TastyHeart ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( Head ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( LeftArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Torso ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bone ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RibCage ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( BonePile ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bones ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( GraveChest ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( DemonPrison ), Utility.Random( 500,1000 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DracolichSkull ), Utility.Random( 500,1000 ) ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( WizardStaff ), 20 ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( WizardStick ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( MageEye ), 1 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMortician : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMortician()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GraveShovel ), 12, Utility.Random( 1,100 ), 0xF39, 0x966 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( SurgeonsKnife ), 14, Utility.Random( 1,100 ), 0xEC4, 0x1B0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingCauldron ), 247, 1, 0x269C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 1,100 ), 0x1E27, 0x979 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PolishBoneBrush ), 12, 10, 0x1371, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 50, Utility.Random( 1,100 ), 0x2253, 0x4AA ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HellsGateScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x54F ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ManaLeechScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xB87 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NecroCurePoisonScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x8A2 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NecroPoisonScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x4F8 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NecroUnlockScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x493 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PhantasmScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x6DE ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( RetchedAirScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xA97 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( SpectreShadowScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x17E ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( UndeadEyesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x491 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( VampireGiftScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xB85 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( WallOfSpikesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xB8F ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( BloodPactScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x5B5 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GhostlyImagesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xBF ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GhostPhaseScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x47E ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GraveyardGatewayScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x2EA ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HellsBrandScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x54C ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) ); } 
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( WoodenCoffin ), 100, 1, 0x2800, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( WoodenCasket ), 100, 1, 0x27E9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StoneCoffin ), 180, 1, 0x27E0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StoneCasket ), 180, 1, 0x2802, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullMinotaur ), Utility.Random( 50,150 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullWyrm ), Utility.Random( 200,400 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullGreatDragon ), Utility.Random( 300,600 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDragon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDemon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullGiant ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){ Add( typeof( MixingCauldron ), 123 ); }  
				if ( MyServerSettings.BuyChance() ){ Add( typeof( MixingSpoon ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoneArms ), 94 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoneChest ), 121 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoneGloves ), 72 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoneLegs ), 109 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoneSkirt ), 109 ); }				
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MyNecromancerSpellbook ), Utility.Random( 250,1000 ) ); } 
				Add( typeof( CorpseSailor ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( CorpseChest ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BuriedBody ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BodyPart ), Utility.RandomMinMax( 30, 90 ) );
				Add( typeof( BoneContainer ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( LeftLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( TastyHeart ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( Head ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( LeftArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Torso ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bone ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RibCage ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( BonePile ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bones ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( GraveChest ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
				Add( typeof( PolishBoneBrush ), 6 );
				Add( typeof( PolishedSkull ), 3 );
				Add( typeof( PolishedBone ), 3 );
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenCoffin ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenCasket ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StoneCoffin ), 45 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StoneCasket ), 45 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DracolichSkull ), Utility.Random( 500,1000 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Mage vendor.
	/// Sells spellbooks, reagents, magical hats, scrolls, and arcane supplies.
	/// </summary>
	/// <summary>
	/// Shop info for Mage vendor.
	/// Sells spellbooks, reagents, magical hats, scrolls, and arcane supplies.
	/// </summary>
	public class SBMage : SBInfo
	{
		#region Fields

		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SBMage class.
		/// </summary>
		public SBMage()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sell information for this vendor.
		/// </summary>
		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }

		/// <summary>
		/// Gets the buy information for this vendor.
		/// </summary>
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		#endregion

		#region InternalBuyInfo

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{

                //if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 115, Utility.Random( 1,100 ), 0x2253, 0 ) ); }
                //if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ScribesPen ), 8, Utility.Random( 1,100 ), 0x2051, 0 ) ); }
                //if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlankScroll ), 5, Utility.Random( 1,100 ), 0x0E34, 0 ) ); }
                //if ( MyServerSettings.SellChance() ){ }

                
                Add(new GenericBuyInfo(typeof(Spellbook), 66, 8, 0xEFA, 0));
                //Add(new GenericBuyInfo(typeof(WizardStaff), 120, Utility.Random(1, 5), 0x0908, 0xB3A));
                Add(new GenericBuyInfo(typeof(WizardStick), 118, Utility.Random(1, 5), 0xDF2, 0xB3A));
                Add(new GenericBuyInfo(typeof(WitchHat), 79, Utility.Random(1, 10), 0x2FC3, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo("1041072", typeof(MagicWizardsHat), 78, Utility.Random(2, 10), 0x1718, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(BagOfReagents), 1088, 10, 0xE76, 0)); // pay 34 group reags and win 1 group for free
                Add( new GenericBuyInfo( typeof( RecallRune ), 8, 50, 0x1F14, 0 ) );
				Add( new GenericBuyInfo( typeof( BlackPearl ), 6, 50, 0x266F, 0 ) );
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, 50, 0xF7B, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, 50, 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 4, 50, 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 4, 50, 0xF86, 0 ) );
				Add( new GenericBuyInfo( typeof( Nightshade ), 3, 50, 0xF88, 0 ) );
				Add( new GenericBuyInfo( typeof( SpidersSilk ), 3, 50, 0xF8D, 0 ) );
				Add( new GenericBuyInfo( typeof( SulfurousAsh ), 4, 50, 0xF8C, 0 ) );
                Add(new GenericBuyInfo(typeof(MageEye), 5, Utility.Random(10, 50), 0xF19, 0xB78));
                /*				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BatWing ), 3, Utility.Random( 1,100 ), 0xF78, 0 ) ); }
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DaemonBlood ), 6, Utility.Random( 1,100 ), 0xF7D, 0 ) ); }
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PigIron ), 5, Utility.Random( 1,100 ), 0xF8A, 0 ) ); }
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NoxCrystal ), 6, Utility.Random( 1,100 ), 0xF8E, 0 ) ); }
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GraveDust ), 3, Utility.Random( 1,100 ), 0xF8F, 0 ) ); }
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BloodOathScroll ), 25, Utility.Random( 1,100 ), 0x2263, 0 ) ); }
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CorpseSkinScroll ), 28, Utility.Random( 1,100 ), 0x2263, 0 ) ); }
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CurseWeaponScroll ), 12, Utility.Random( 1,100 ), 0x2263, 0 ) ); }
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ElectrumFlask ), 2500, Utility.Random( 1,5 ), 0x282E, 0 ) ); }
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStaff ), 40, Utility.Random( 1,5 ), 0x0908, 0xB3A ) ); }
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStick ), 38, Utility.Random( 1,5 ), 0xDF2, 0xB3A ) ); }
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) ); }
                                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "Stable Gates", typeof( LinkedGateBag ), 300000, 1, 0xE76, 0xABE ) ); }*/


                Type[] types = Loot.RegularScrollTypes;

				int circles = 3;

				for ( int i = 0; i < circles*8 && i < types.Length; ++i )
				{
					int itemID = 0x1F2E + i;

					if ( i == 6 )
						itemID = 0x1F2D;
					else if ( i > 6 )
						--itemID;

					if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( types[i], 12 + ((i / 8) * 10), Utility.Random(1, 12), itemID, 0 ) ); }
				}
			}
		}

		#endregion

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{

                //if (MyServerSettings.BuyChance()) { Add(typeof(WizardStaff), 60); }
                if (MyServerSettings.SellChance()) { Add(typeof(WizardStick), 49); }
                if (MyServerSettings.BuyChance()) { Add(typeof(WizardsHat), 50); }
                if (MyServerSettings.BuyChance()) { Add(typeof(WitchHat), 49); }
                if (MyServerSettings.BuyChance()) { Add(typeof(BlackPearl), 5); }
                if (MyServerSettings.BuyChance()) { Add(typeof(Bloodmoss), 4); }
                if (MyServerSettings.BuyChance()) { Add(typeof(MandrakeRoot), 3); }
                if (MyServerSettings.BuyChance()) { Add(typeof(Garlic), 2); }
                if (MyServerSettings.BuyChance()) { Add(typeof(Ginseng), 3); }
                if (MyServerSettings.BuyChance()) { Add(typeof(Nightshade), 2); }
                if (MyServerSettings.BuyChance()) { Add(typeof(SpidersSilk), 2); }
                if (MyServerSettings.BuyChance()) { Add(typeof(SulfurousAsh), 3); }
                if (MyServerSettings.BuyChance()) { Add(typeof(RecallRune), 7); }
                if (MyServerSettings.BuyChance()) { Add(typeof(Spellbook), 60); }
                if (MyServerSettings.BuyChance()) { Add(typeof(MageEye), 3); }
                //if (MyServerSettings.BuyRareChance()) { Add(typeof(MySpellbook), Utility.Random(4500, 9000)); }

                /*                if (MyServerSettings.BuyChance()) { Add(typeof(MagicTalisman), Utility.Random(50, 100)); }
                                if (MyServerSettings.BuyChance()) { Add(typeof(BatWing), 1); }
                                if (MyServerSettings.BuyChance()) { Add(typeof(DaemonBlood), 3); }
                                if (MyServerSettings.BuyChance()) { Add(typeof(PigIron), 2); }
                                if (MyServerSettings.BuyChance()) { Add(typeof(NoxCrystal), 3); }
                                if (MyServerSettings.BuyChance()) { Add(typeof(GraveDust), 1); }

                                if (MyServerSettings.BuyChance()) { Add(typeof(NecromancerSpellbook), 55); }
                                if (MyServerSettings.SellCommonChance()) { Add(typeof(MysticalPearl), 250); }*/

                Type[] types = Loot.RegularScrollTypes;

				for ( int i = 0; i < types.Length; ++i )
					if ( MyServerSettings.BuyChance() ){ Add(types[i], i + 3 + (i / 4)); }

                /*				if ( MyServerSettings.BuyChance() ){Add( typeof( ExorcismScroll ), 1 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( AnimateDeadScroll ), 26 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( BloodOathScroll ), 26 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( CorpseSkinScroll ), 26 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( CurseWeaponScroll ), 26 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( EvilOmenScroll ), 26 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( PainSpikeScroll ), 26 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( SummonFamiliarScroll ), 26 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( HorrificBeastScroll ), 27 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( MindRotScroll ), 39 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( PoisonStrikeScroll ), 39 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( WraithFormScroll ), 51 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( LichFormScroll ), 64 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( StrangleScroll ), 64 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( WitherScroll ), 64 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( VampiricEmbraceScroll ), 101 ); } 
                                if ( MyServerSettings.BuyChance() ){Add( typeof( VengefulSpiritScroll ), 114 ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ClumsyMagicStaff ), Utility.Random( 10,20 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CreateFoodMagicStaff ), Utility.Random( 10,20 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FeebleMagicStaff ), Utility.Random( 10,20 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( HealMagicStaff ), Utility.Random( 10,20 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicArrowMagicStaff ), Utility.Random( 10,20 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( NightSightMagicStaff ), Utility.Random( 10,20 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ReactiveArmorMagicStaff ), Utility.Random( 10,20 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WeaknessMagicStaff ), Utility.Random( 10,20 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( AgilityMagicStaff ), Utility.Random( 20,40 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CunningMagicStaff ), Utility.Random( 20,40 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CureMagicStaff ), Utility.Random( 20,40 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( HarmMagicStaff ), Utility.Random( 20,40 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicTrapMagicStaff ), Utility.Random( 20,40 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicUntrapMagicStaff ), Utility.Random( 20,40 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ProtectionMagicStaff ), Utility.Random( 20,40 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( StrengthMagicStaff ), Utility.Random( 20,40 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BlessMagicStaff ), Utility.Random( 30,60 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireballMagicStaff ), Utility.Random( 30,60 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicLockMagicStaff ), Utility.Random( 30,60 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicUnlockMagicStaff ), Utility.Random( 30,60 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PoisonMagicStaff ), Utility.Random( 30,60 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( TelekinesisMagicStaff ), Utility.Random( 30,60 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( TeleportMagicStaff ), Utility.Random( 30,60 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WallofStoneMagicStaff ), Utility.Random( 30,60 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ArchCureMagicStaff ), Utility.Random( 40,80 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ArchProtectionMagicStaff ), Utility.Random( 40,80 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CurseMagicStaff ), Utility.Random( 40,80 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireFieldMagicStaff ), Utility.Random( 40,80 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GreaterHealMagicStaff ), Utility.Random( 40,80 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( LightningMagicStaff ), Utility.Random( 40,80 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaDrainMagicStaff ), Utility.Random( 40,80 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( RecallMagicStaff ), Utility.Random( 40,80 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BladeSpiritsMagicStaff ), Utility.Random( 50,100 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( DispelFieldMagicStaff ), Utility.Random( 50,100 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( IncognitoMagicStaff ), Utility.Random( 50,100 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicReflectionMagicStaff ), Utility.Random( 50,100 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MindBlastMagicStaff ), Utility.Random( 50,100 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ParalyzeMagicStaff ), Utility.Random( 50,100 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PoisonFieldMagicStaff ), Utility.Random( 50,100 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( SummonCreatureMagicStaff ), Utility.Random( 50,100 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( DispelMagicStaff ), Utility.Random( 60,120 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyBoltMagicStaff ), Utility.Random( 60,120 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ExplosionMagicStaff ), Utility.Random( 60,120 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( InvisibilityMagicStaff ), Utility.Random( 60,120 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MarkMagicStaff ), Utility.Random( 60,120 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MassCurseMagicStaff ), Utility.Random( 60,120 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ParalyzeFieldMagicStaff ), Utility.Random( 60,120 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( RevealMagicStaff ), Utility.Random( 60,120 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ChainLightningMagicStaff ), Utility.Random( 70,140 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyFieldMagicStaff ), Utility.Random( 70,140 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FlameStrikeMagicStaff ), Utility.Random( 70,140 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GateTravelMagicStaff ), Utility.Random( 70,140 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaVampireMagicStaff ), Utility.Random( 70,140 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MassDispelMagicStaff ), Utility.Random( 70,140 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MeteorSwarmMagicStaff ), Utility.Random( 70,140 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PolymorphMagicStaff ), Utility.Random( 70,140 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( AirElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EarthElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EarthquakeMagicStaff ), Utility.Random( 80,160 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyVortexMagicStaff ), Utility.Random( 80,160 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ResurrectionMagicStaff ), Utility.Random( 80,160 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( SummonDaemonMagicStaff ), Utility.Random( 80,160 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WaterElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
                                if ( MyServerSettings.BuyRareChance() ){Add( typeof( MyNecromancerSpellbook ), Utility.Random( 250,1000 ) ); } */

                
/*				Add( typeof( TomeOfWands ), Utility.Random( 100,400 ) );
				if ( MyServerSettings.SellChance() ){Add( typeof( DemonPrison ), Utility.Random( 500,1000 ) ); } */
				
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBGodlySewing: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGodlySewing()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GodSewing ), 1000, 20, 0x0F9F, 0x501 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBGodlySmithing: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGodlySmithing()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GodSmithing ), 1000, 20, 0x0FB5, 0x501 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBGodlyBrewing: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGodlyBrewing()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GodBrewing ), 1000, 20, 0x0E28, 0x501 ) );
				Add( new GenericBuyInfo( typeof( reagents_magic_jar1 ), 2000, Utility.Random( 1,100 ), 0x1007, 0 ) );
				Add( new GenericBuyInfo( typeof( reagents_magic_jar2 ), 1500, Utility.Random( 1,100 ), 0x1007, 0xB97 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMazeStore: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMazeStore()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( reagents_magic_jar1 ), 2000, 20, 0x1007, 0 ) );
				Add( new GenericBuyInfo( typeof( reagents_magic_jar2 ), 1500, 20, 0x1007, 0xB97 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBuyArtifacts: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBuyArtifacts()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Antiquity ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Annihilation ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneCap ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneShield ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcanicRobe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcticBeacon ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcticDeathDealer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmorOfFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmorOfInsight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmorOfNobility ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfAegis ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfInsight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfNobility ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfTheFallenKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfTheHarrower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfToxicity ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AuraOfShadows ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AxeoftheMinotaur ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BagOfHolding ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BalancingDeed ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BeltofHercules ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BlazeOfDeath ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BookOfKnowledge ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BootsofHermes ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BowOfTheJukaKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BowofthePhoenix ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BraceletOfHealth ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BraceletOfTheElements ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BraceletOfTheVile ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BreathOfTheDead ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BurglarsBandana ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Calm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandelabraOfSouls ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandleCold ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandleEnergy ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandleFire ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandleNecromancer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandlePoison ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandleWizard ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CapOfFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CapOfTheFallenKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CavortingClub ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CircletOfTheSorceress ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CoifOfBane ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CoifOfFire ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ColdBlood ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ColoringBook ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ConansHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ConansSword ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CrimsonCincture ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CrownOfTalKeesh ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DaggerOfVenom ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DarkGuardiansChest ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DarkLordsPitchfork ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DarkNeck ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DeathsMask ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DivineArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DivineCountenance ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DivineGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DivineGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DivineLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DivineTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DjinnisRing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DreadPirateHat ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DupresCollar ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DupresShield ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EarringBoxSet ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EarringsOfHealth ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EarringsOfProtection ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EarringsOfTheElements ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EarringsOfTheMagician ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EarringsOfTheVile ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ElvenQuiver ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EmbroideredOakLeafCloak ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EnchantedTitanLegBone ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EternalFlame ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EverlastingBottle ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EverlastingLoaf ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EvilMageGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Excalibur ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FalseGodsScepter ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FangOfRactus ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FesteringWound ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Fortifiedarms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FortunateBlades ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Frostbringer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FurCapeOfTheSorceress ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Fury ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GandalfsHat ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GandalfsRobe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GandalfsStaff ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GauntletsOfNobility ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GeishasObi ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GemOfSeeing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GiantBlackjack ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GladiatorsCollar ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlassSword ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfAegis ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfCorruption ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfDexterity ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfInsight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfRegeneration ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfTheFallenKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfTheHarrower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfThePugilist ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GorgetOfAegis ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GorgetOfFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GorgetOfInsight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GuantletsOfAnger ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GwennosHarp ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HammerofThor ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HatOfTheMagi ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HeartOfTheLion ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HellForgedArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HelmOfAegis ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HelmOfBrilliance ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HelmOfInsight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolyKnightsArmPlates ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolyKnightsBreastplate ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolyKnightsGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolyKnightsGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolyKnightsLegging ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolyKnightsPlateHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolySword ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HuntersArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HuntersGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HuntersGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HuntersHeaddress ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HuntersLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HuntersTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Indecency ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( InquisitorsArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( InquisitorsGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( InquisitorsHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( InquisitorsLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( InquisitorsResolution ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( InquisitorsTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( IolosLute ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JackalsArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JackalsCollar ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JackalsGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JackalsHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JackalsLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JackalsTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JadeScimitar ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JesterHatofChuckles ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JinBaoriOfGoodFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( KamiNarisIndestructableDoubleAxe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( KodiakBearMask ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LargeBagofHolding ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LegacyOfTheDreadLord ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LeggingsOfAegis ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LeggingsOfBane ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LeggingsOfDeceit ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LeggingsOfEnlightenment ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LeggingsOfFire ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LegsOfFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LegsOfInsight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LegsOfNobility ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LegsOfTheFallenKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LegsOfTheHarrower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LieutenantOfTheBritannianRoyalGuard ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LongShot ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LuckyEarrings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LuckyNecklace ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LunaLance ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MadmansHatchet ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagesBand ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagiciansIllusion ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagiciansMempo ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MarbleShield ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MauloftheBeast ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MaulOfTheTitans ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MediumBagofHolding ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MidnightBracers ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MidnightGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MidnightHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MidnightLegs ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MidnightTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MinersPickaxe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( NightsKiss ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( NordicVikingSword ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( NoxBow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( NoxNightlight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( NoxRangersHeavyCrossbow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( OblivionsNeedle ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( OrcChieftainHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( OrcishVisage ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( OrnamentOfTheMagician ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( OrnateCrownOfTheHarrower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Pestilence ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PixieSwatter ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PolarBearBoots ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PolarBearCape ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PolarBearMask ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PowerSurge ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Quell ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfBlight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfElements ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfFire ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfIce ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfInfinity ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfLightning ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfRage ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RamusNecromanticScalpel ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Retort ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RighteousAnger ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RingOfHealth ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RingOfTheElements ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RingOfTheMagician ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RingOfTheVile ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RobeOfTeleportation ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RobeOfTheEclipse ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RobeOfTheEquinox ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RobeOfTreason ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RobinHoodsBow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RobinHoodsFeatheredHat ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RodOfResurrection ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RoyalArchersBow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RoyalGuardsChestplate ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RoyalGuardsGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RoyalGuardSurvivalKnife ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SamaritanRobe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SamuraiHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SerpentsFang ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowBlade ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowDancerArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowDancerCap ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowDancerGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowDancerGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowDancerLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowDancerTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShaminoCrossbow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShieldOfInvulnerability ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShimmeringTalisman ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShroudOfDeciet ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SinbadsSword ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SmallBagofHolding ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SoulSeeker ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SpiritOfTheTotem ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SprintersSandals ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( StaffOfPower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( StaffofSnakes ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( StaffOfTheMagi ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Stormbringer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Subdue ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SwiftStrike ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TalonBite ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TheBeserkersMaul ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TheDragonSlayer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TheDryadBow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TheRobeOfBritanniaAri ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TheTaskmaster ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TitansHammer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TorchOfTrapFinding ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TotemArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TotemGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TotemGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TotemLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TotemOfVoid ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TotemTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TownGuardsHalberd ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TunicOfAegis ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TunicOfBane ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TunicOfFire ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TunicOfTheFallenKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TunicOfTheHarrower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( VampiricDaisho ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( VioletCourage ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( VoiceOfTheFallenKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WarriorsClasp ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WeaponRenamingTool ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WildfireBow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Windsong ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WizardsPants ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WrathOfTheDryad ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( YashimotosHatsuburi ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ZyronicClaw ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AegisOfGrace ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AlchemistsBauble ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AxeOfTheHeavens ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BeggarsRobe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BladeDance ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BladeOfInsanity ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BladeOfTheRighteous ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BlightGrippedLongbow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BloodwoodSpirit ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BoneCrusher ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Bonesmasher ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Boomstick ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BottomlessBargainBucket ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CaptainQuacklebushsCutlass ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ColdForgedBlade ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ConansLoinCloth ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DoubletOfPower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EssenceOfBattle ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FeyLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FleshRipper ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GrayMouserCloak ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GrimReapersMask ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GrimReapersRobe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GrimReapersScythe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HelmOfSwiftness ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LuminousRuneBlade ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagusShirt ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MelisandesCorrodedHatchet ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( OverseerSunderedBlade ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Pacify ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PandorasBox ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PendantOfTheMagi ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PhantomStaff ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PrincessIllusion ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RaedsGlory ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ResilientBracer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RuneCarvingKnife ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShardThrasher ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SilvanisFeywoodBow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( StreetFightersVest ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TenguHakama ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TheNightReaper ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ThinkingMansKilt ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( VampiresRobe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( VeryFancyShirt ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WindSpirit ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiGonsLightSword ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( sotedeathstarrelic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( soter2d2relic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BloodTrail ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BowOfHarps ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ChildOfDeath ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Erotica ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FeverFall ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfTheHardWorker ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HandsofTabulature ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Kamadon ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MIBHunter ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PurposeOfPain ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Revenge ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SatanicHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShieldOfIce ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( StandStill ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SwordOfIce ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TacticalMask ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ThickNeck ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ValasCompromise ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Valicious ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WizardsStrongArm ), Utility.Random( 20000,30000 ) ); }
				
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBGemArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGemArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystShield ), 25425, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldPlateChest ),25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldShield ), 25425, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetShield ), 25425, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IceFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IcePlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IcePlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IcePlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IcePlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IcePlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IcePlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IceShield ), 25425, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadeFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadePlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadePlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadePlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadePlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadePlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadePlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadeShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarbleFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarblePlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarblePlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarblePlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarblePlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarblePlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarblePlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarbleShields ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphireFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphirePlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphirePlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphirePlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphirePlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphirePlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphirePlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphireShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IceFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IcePlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IcePlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IcePlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IcePlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IcePlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IcePlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IceShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadeFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadePlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadePlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadePlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadePlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadePlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadePlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadeShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarbleFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarblePlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarblePlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarblePlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarblePlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarblePlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarblePlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarbleShields ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzPlateGloves  ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphirePlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphirePlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphirePlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphirePlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphirePlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphireFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphirePlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphireShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RareAnvil ), Utility.Random( 200,1000 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBRoscoe: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRoscoe()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LesserManaPotion ), 20, Utility.Random( 1,100 ), 0x23BD, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ManaPotion ), 40, Utility.Random( 1,100 ), 0x180F, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GreaterManaPotion ), 80, Utility.Random( 1,100 ), 0x2406, 0x48D ) ); }

				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ClumsyMagicStaff ), 500, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( CreateFoodMagicStaff ), 500, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( FeebleMagicStaff ), 500, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HealMagicStaff ), 500, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MagicArrowMagicStaff ), 500, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NightSightMagicStaff ), 500, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ReactiveArmorMagicStaff ), 500, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( WeaknessMagicStaff ), 500, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( AgilityMagicStaff ), 1000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( CunningMagicStaff ), 1000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( CureMagicStaff ), 1000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HarmMagicStaff ), 1000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MagicTrapMagicStaff ), 1000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MagicUntrapMagicStaff ), 1000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ProtectionMagicStaff ), 1000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( StrengthMagicStaff ), 1000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( BlessMagicStaff ), 2000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( FireballMagicStaff ), 2000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MagicLockMagicStaff ), 2000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MagicUnlockMagicStaff ), 2000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PoisonMagicStaff ), 2000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( TelekinesisMagicStaff ), 2000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( TeleportMagicStaff ), 2000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( WallofStoneMagicStaff ), 2000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ArchCureMagicStaff ), 4000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ArchProtectionMagicStaff ), 4000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( CurseMagicStaff ), 4000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( FireFieldMagicStaff ), 4000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GreaterHealMagicStaff ), 4000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( LightningMagicStaff ), 4000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ManaDrainMagicStaff ), 4000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RecallMagicStaff ), 4000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BladeSpiritsMagicStaff ), 8000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DispelFieldMagicStaff ), 8000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IncognitoMagicStaff ), 8000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MagicReflectionMagicStaff ), 8000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MindBlastMagicStaff ), 8000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ParalyzeMagicStaff ), 8000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( PoisonFieldMagicStaff ), 8000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SummonCreatureMagicStaff ), 8000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DispelMagicStaff ), 16000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EnergyBoltMagicStaff ), 16000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ExplosionMagicStaff ), 16000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( InvisibilityMagicStaff ), 16000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarkMagicStaff ), 16000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MassCurseMagicStaff ), 16000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ParalyzeFieldMagicStaff ), 16000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RevealMagicStaff ), 16000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ChainLightningMagicStaff ), 24000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EnergyFieldMagicStaff ), 24000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( FlameStrikeMagicStaff ), 24000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GateTravelMagicStaff ), 24000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ManaVampireMagicStaff ), 24000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MassDispelMagicStaff ), 24000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MeteorSwarmMagicStaff ), 24000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( PolymorphMagicStaff ), 24000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AirElementalMagicStaff ), 32000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EarthElementalMagicStaff ), 32000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EarthquakeMagicStaff ), 32000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EnergyVortexMagicStaff ), 32000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( FireElementalMagicStaff ), 32000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ResurrectionMagicStaff ), 32000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SummonDaemonMagicStaff ), 32000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( WaterElementalMagicStaff ), 32000, 1, 0xDF5, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( LesserManaPotion ), 10 ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaPotion ), 20 ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GreaterManaPotion ), 40 ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ClumsyMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CreateFoodMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FeebleMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( HealMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicArrowMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( NightSightMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ReactiveArmorMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WeaknessMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( AgilityMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CunningMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CureMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( HarmMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicTrapMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicUntrapMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ProtectionMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( StrengthMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BlessMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireballMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicLockMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicUnlockMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PoisonMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( TelekinesisMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( TeleportMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WallofStoneMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ArchCureMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ArchProtectionMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CurseMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireFieldMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GreaterHealMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( LightningMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaDrainMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( RecallMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BladeSpiritsMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( DispelFieldMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( IncognitoMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicReflectionMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MindBlastMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ParalyzeMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PoisonFieldMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( SummonCreatureMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( DispelMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyBoltMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ExplosionMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( InvisibilityMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MarkMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MassCurseMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ParalyzeFieldMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( RevealMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ChainLightningMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyFieldMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FlameStrikeMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GateTravelMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaVampireMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MassDispelMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MeteorSwarmMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PolymorphMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( AirElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EarthElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EarthquakeMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyVortexMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ResurrectionMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( SummonDaemonMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WaterElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
			}
		}
	}

/////----------------------------------------------------------------------------------------------------------------------------------------------------/////

	public class SBTinkerGuild: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBTinkerGuild() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{
				Add( new GenericBuyInfo( typeof( GuildTinkering ), 500, Utility.Random( 1,5 ), 0x1EBB, 0x430 ) );
				Add( new GenericBuyInfo( typeof( Clock ), 22, Utility.Random( 50,200 ), 0x104B, 0 ) );
				Add( new GenericBuyInfo( typeof( Nails ), 3, Utility.Random( 50,200 ), 0x102E, 0 ) );
				Add( new GenericBuyInfo( typeof( ClockParts ), 3, Utility.Random( 50,200 ), 0x104F, 0 ) );
				Add( new GenericBuyInfo( typeof( AxleGears ), 3, Utility.Random( 50,200 ), 0x1051, 0 ) );
				Add( new GenericBuyInfo( typeof( Gears ), 2, Utility.Random( 50,200 ), 0x1053, 0 ) );
				Add( new GenericBuyInfo( typeof( Hinge ), 2, Utility.Random( 50,200 ), 0x1055, 0 ) );
				Add( new GenericBuyInfo( typeof( Sextant ), 13, Utility.Random( 50,200 ), 0x1057, 0 ) );
				Add( new GenericBuyInfo( typeof( SextantParts ), 5, Utility.Random( 50,200 ), 0x1059, 0 ) );
				Add( new GenericBuyInfo( typeof( Axle ), 2, Utility.Random( 50,200 ), 0x105B, 0 ) );
				Add( new GenericBuyInfo( typeof( Springs ), 3, Utility.Random( 50,200 ), 0x105D, 0 ) );
				Add( new GenericBuyInfo( "1024111", typeof( Key ), 8, Utility.Random( 50,200 ), 0x100F, 0 ) );
				Add( new GenericBuyInfo( "1024112", typeof( Key ), 8, Utility.Random( 50,200 ), 0x1010, 0 ) );
				Add( new GenericBuyInfo( "1024115", typeof( Key ), 8, Utility.Random( 50,200 ), 0x1013, 0 ) );
				Add( new GenericBuyInfo( typeof( KeyRing ), 8, Utility.Random( 50,200 ), 0x1010, 0 ) );
				Add( new GenericBuyInfo( typeof( Lockpick ), 12, Utility.Random( 50,200 ), 0x14FC, 0 ) );
				Add( new GenericBuyInfo( typeof( SkeletonsKey ), Utility.Random( 150,250 ), 20, 0x410A, 0 ) );
				Add( new GenericBuyInfo( typeof( TinkersTools ), 7, Utility.Random( 50,200 ), 0x1EBC, 0 ) );
				Add( new GenericBuyInfo( typeof( Board ), 3, Utility.Random( 50,200 ), 0x1BD7, 0 ) );
				Add( new GenericBuyInfo( typeof( IronIngot ), 5, Utility.Random( 50,200 ), 0x1BF2, 0 ) );
				Add( new GenericBuyInfo( typeof( SewingKit ), 3, Utility.Random( 50,200 ), 0x4C81, 0 ) );
				Add( new GenericBuyInfo( typeof( DrawKnife ), 10, Utility.Random( 50,200 ), 0x10E4, 0 ) );
				Add( new GenericBuyInfo( typeof( Froe ), 10, Utility.Random( 50,200 ), 0x10E5, 0 ) );
				Add( new GenericBuyInfo( typeof( Scorp ), 10, Utility.Random( 50,200 ), 0x10E7, 0 ) );
				Add( new GenericBuyInfo( typeof( Inshave ), 10, Utility.Random( 50,200 ), 0x10E6, 0 ) );
				Add( new GenericBuyInfo( typeof( ButcherKnife ), 13, Utility.Random( 50,200 ), 0x13F6, 0 ) );
				Add( new GenericBuyInfo( typeof( Scissors ), 11, Utility.Random( 50,200 ), 0xF9F, 0 ) );
				Add( new GenericBuyInfo( typeof( Tongs ), 13, Utility.Random( 50,200 ), 0xFBB, 0 ) );
				Add( new GenericBuyInfo( typeof( DovetailSaw ), 12, Utility.Random( 50,200 ), 0x1028, 0 ) );
				Add( new GenericBuyInfo( typeof( Saw ), 100, Utility.Random( 50,200 ), 0x1034, 0 ) );
				Add( new GenericBuyInfo( typeof( Hammer ), 17, Utility.Random( 50,200 ), 0x102A, 0 ) );
				Add( new GenericBuyInfo( typeof( SmithHammer ), 23, Utility.Random( 50,200 ), 0x0FB4, 0 ) );
				Add( new GenericBuyInfo( typeof( Shovel ), 12, Utility.Random( 50,200 ), 0xF39, 0 ) );
				Add( new GenericBuyInfo( typeof( OreShovel ), 10, Utility.Random( 50,200 ), 0xF39, 0x96D ) );
				Add( new GenericBuyInfo( typeof( MouldingPlane ), 11, Utility.Random( 50,200 ), 0x102C, 0 ) );
				Add( new GenericBuyInfo( typeof( JointingPlane ), 10, Utility.Random( 50,200 ), 0x1030, 0 ) );
				Add( new GenericBuyInfo( typeof( SmoothingPlane ), 11, Utility.Random( 50,200 ), 0x1032, 0 ) );
				Add( new GenericBuyInfo( typeof( WoodworkingTools ), 10, Utility.Random( 10,50 ), 0x4F52, 0 ) );
				Add( new GenericBuyInfo( typeof( Pickaxe ), 25, Utility.Random( 50,200 ), 0xE86, 0 ) );
				Add( new GenericBuyInfo( typeof( ThrowingWeapon ), 2, Utility.Random( 20, 120 ), 0x52B2, 0 ) );
				Add( new GenericBuyInfo( typeof( light_wall_torch ), 50, Utility.Random( 50,200 ), 0xA07, 0 ) );
				Add( new GenericBuyInfo( typeof( light_dragon_brazier ), 750, Utility.Random( 50,200 ), 0x194E, 0 ) );
				Add( new GenericBuyInfo( typeof( TrapKit ), 420, Utility.Random( 1,3 ), 0x1EBB, 0 ) );
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PianoAddonDeed ), Utility.Random( 95000,190000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PoorMiningHarvester ),12000, Utility.Random( 1,2 ), 0x5484, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StandardMiningHarvester ), 70000, Utility.Random( 1,2 ), 0x5484, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GoodMiningHarvester ), 200000, Utility.Random( 1,2 ), 0x5484, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PoorLumberHarvester ), 12000, Utility.Random( 1,2 ), 0x5486, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( StandardLumberHarvester ), 70000, Utility.Random( 1,2 ), 0x5486, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PoorHideHarvester ), 12000, Utility.Random( 1,2 ), 0x5487, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( StandardHideHarvester ), 70000, Utility.Random( 1,2 ), 0x5487, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HarvesterRepairKit ), 6500, Utility.Random( 1,2 ), 0x4C2C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CiderBarrel ), 17000, Utility.Random( 1,4 ), 0x3DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AleBarrel ), 17000, Utility.Random( 1,4 ), 0x3DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LiquorBarrel ), 17000, Utility.Random( 1,4 ), 0x3DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CheesePress ), 17000, Utility.Random( 1,4 ), 0x3DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DeviceKit), 900, Utility.Random( 1,2 ), 0x4F86, 0 ) ); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				Add( typeof( LootChest ), 600 );
				Add( typeof( Shovel ), 6 );
				Add( typeof( OreShovel ), 5 );
				Add( typeof( SewingKit ), 1 );
				Add( typeof( Scissors ), 6 );
				Add( typeof( Tongs ), 7 );
				Add( typeof( Key ), 1 );
				Add( typeof( DovetailSaw ), 6 );
				Add( typeof( MouldingPlane ), 6 );
				Add( typeof( Nails ), 1 );
				Add( typeof( JointingPlane ), 6 );
				Add( typeof( SmoothingPlane ), 6 );
				Add( typeof( Saw ), 7 );
				Add( typeof( Clock ), 11 );
				Add( typeof( ClockParts ), 1 );
				Add( typeof( AxleGears ), 1 );
				Add( typeof( Gears ), 1 );
				Add( typeof( Hinge ), 1 );
				Add( typeof( Sextant ), 6 );
				Add( typeof( SextantParts ), 2 );
				Add( typeof( Axle ), 1 );
				Add( typeof( Springs ), 1 );
				Add( typeof( DrawKnife ), 5 );
				Add( typeof( Froe ), 5 );
				Add( typeof( Inshave ), 5 );
				Add( typeof( WoodworkingTools ), 5 );
				Add( typeof( Scorp ), 5 );
				Add( typeof( Lockpick ), 6 );
				Add( typeof( SkeletonsKey ), 10 );
				Add( typeof( TinkerTools ), 3 );
				Add( typeof( Board ), 1 );
				Add( typeof( Log ), 1 );
				Add( typeof( Pickaxe ), 16 );
				Add( typeof( Hammer ), 3 );
				Add( typeof( SmithHammer ), 11 );
				Add( typeof( ButcherKnife ), 6 );
				Add( typeof( CrystalScales ), Utility.Random( 300,600 ) );
				Add( typeof( GolemManual ), Utility.Random( 500,750 ) );
				Add( typeof( PowerCrystal ), 100 );
				Add( typeof( ArcaneGem ), 10 );
				Add( typeof( ClockworkAssembly ), 100 );
				Add( typeof( BottleOil ), 5 );
				Add( typeof( ThrowingWeapon ), 1 );
				Add( typeof( TrapKit ), 210 );
				Add( typeof( SpaceJunkA ), Utility.Random( 5, 10 ) );
				Add( typeof( SpaceJunkB ), Utility.Random( 10, 20 ) );
				Add( typeof( SpaceJunkC ), Utility.Random( 15, 30 ) );
				Add( typeof( SpaceJunkD ), Utility.Random( 20, 40 ) );
				Add( typeof( SpaceJunkE ), Utility.Random( 25, 50 ) );
				Add( typeof( SpaceJunkF ), Utility.Random( 30, 60 ) );
				Add( typeof( SpaceJunkG ), Utility.Random( 35, 70 ) );
				Add( typeof( SpaceJunkH ), Utility.Random( 40, 80 ) );
				Add( typeof( SpaceJunkI ), Utility.Random( 45, 90 ) );
				Add( typeof( SpaceJunkJ ), Utility.Random( 50, 100 ) );
				Add( typeof( SpaceJunkK ), Utility.Random( 55, 110 ) );
				Add( typeof( SpaceJunkL ), Utility.Random( 60, 120 ) );
				Add( typeof( SpaceJunkM ), Utility.Random( 65, 130 ) );
				Add( typeof( SpaceJunkN ), Utility.Random( 70, 140 ) );
				Add( typeof( SpaceJunkO ), Utility.Random( 75, 150 ) );
				Add( typeof( SpaceJunkP ), Utility.Random( 80, 160 ) );
				Add( typeof( SpaceJunkQ ), Utility.Random( 85, 170 ) );
				Add( typeof( SpaceJunkR ), Utility.Random( 90, 180 ) );
				Add( typeof( SpaceJunkS ), Utility.Random( 95, 190 ) );
				Add( typeof( SpaceJunkT ), Utility.Random( 100, 200 ) );
				Add( typeof( SpaceJunkU ), Utility.Random( 105, 210 ) );
				Add( typeof( SpaceJunkV ), Utility.Random( 110, 220 ) );
				Add( typeof( SpaceJunkW ), Utility.Random( 115, 230 ) );
				Add( typeof( SpaceJunkX ), Utility.Random( 120, 240 ) );
				Add( typeof( SpaceJunkY ), Utility.Random( 125, 250 ) );
				Add( typeof( SpaceJunkZ ), Utility.Random( 130, 260 ) );
				Add( typeof( LandmineSetup ), Utility.Random( 100, 300 ) );
				Add( typeof( PlasmaGrenade ), Utility.Random( 28, 38 ) );
				Add( typeof( ThermalDetonator ), Utility.Random( 28, 38 ) );
				Add( typeof( PuzzleCube ), Utility.Random( 45, 90 ) );
				Add( typeof( PlasmaTorch ), Utility.Random( 45, 90 ) );
				Add( typeof( DuctTape ), Utility.Random( 45, 90 ) );
				Add( typeof( RobotBatteries ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotSheetMetal ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotOil ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotGears ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotEngineParts ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotCircuitBoard ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotBolt ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotTransistor ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotSchematics ), Utility.Random( 500,750 ) );
				Add( typeof( DataPad ), Utility.Random( 5, 150 ) );
				Add( typeof( MaterialLiquifier ), Utility.Random( 100, 300 ) );
				Add( typeof( Chainsaw ), Utility.Random( 130, 260 ) );
				Add( typeof( PortableSmelter ), Utility.Random( 130, 260 ) );
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBThiefGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBThiefGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Backpack ), 100, Utility.Random( 50,200 ), 0x53D5, 0 ) );
				Add( new GenericBuyInfo( typeof( Pouch ), 6, Utility.Random( 50,200 ), 0xE79, 0 ) );
				Add( new GenericBuyInfo( typeof( Torch ), 8, Utility.Random( 50,200 ), 0xF6B, 0 ) );
				Add( new GenericBuyInfo( typeof( Lantern ), 2, Utility.Random( 50,200 ), 0xA25, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnStealingBook ), 5, Utility.Random( 50,200 ), 0x4C5C, 0 ) );
				Add( new GenericBuyInfo( typeof( Lockpick ), 12, Utility.Random( 50,500 ), 0x14FC, 0 ) );
				Add( new GenericBuyInfo( typeof( SkeletonsKey ), Utility.Random( 50,200 ), 25, 0x410A, 0 ) );
				Add( new GenericBuyInfo( typeof( MasterSkeletonsKey ), Utility.Random( 500,2000 ), 10, 0x410A, 0 ) );
				Add( new GenericBuyInfo( typeof( WoodenBox ), 14, Utility.Random( 50,200 ), 0x9AA, 0 ) );
				Add( new GenericBuyInfo( typeof( Key ), 2, Utility.Random( 50,200 ), 0x100E, 0 ) );
				Add( new GenericBuyInfo( "1041060", typeof( HairDye ), 100, Utility.Random( 1,100 ), 0xEFF, 0 ) );
				Add( new GenericBuyInfo( "hair dye bottle", typeof( HairDyeBottle ), 1000, Utility.Random( 1,100 ), 0xE0F, 0 ) );
				Add( new GenericBuyInfo( typeof( DisguiseKit ), 700, Utility.Random( 1,5 ), 0xE05, 0 ) );
				Add( new GenericBuyInfo( typeof( CorruptedMoonStone ), 2500, 5, 0xF8B, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Backpack ), 7 );
				Add( typeof( Pouch ), 3 );
				Add( typeof( Torch ), 3 );
				Add( typeof( Lantern ), 1 );
				Add( typeof( Lockpick ), 6 );
				Add( typeof( WoodenBox ), 7 );
				Add( typeof( HairDye ), 50 );
				Add( typeof( HairDyeBottle ), 300 );
				Add( typeof( SkeletonsKey ), 10 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBTailorGuild: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBTailorGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GuildSewing ), 500, Utility.Random( 1,5 ), 0x4C81, 0x430 ) );
				Add( new GenericBuyInfo( typeof( SewingKit ), 3, Utility.Random( 3,31 ), 0x4C81, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Scissors ), 11, Utility.Random( 3,31 ), 0xF9F, 0 ) );
				Add( new GenericBuyInfo( typeof( DyeTub ), 8, Utility.Random( 3,31 ), 0xFAB, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Dyes ), 8, Utility.Random( 3,31 ), 0xFA9, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Shirt ), 12, Utility.Random( 3,31 ), 0x1517, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( ShortPants ), 7, Utility.Random( 3,31 ), 0x152E, 0 ) );
				Add( new GenericBuyInfo( typeof( FancyShirt ), 21, Utility.Random( 3,31 ), 0x1EFD, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( LongPants ), 10, Utility.Random( 3,31 ), 0x1539, 0 ) );
				Add( new GenericBuyInfo( typeof( FancyDress ), 26, Utility.Random( 3,31 ), 0x1EFF, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( PlainDress ), 13, Utility.Random( 3,31 ), 0x1F01, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Kilt ), 11, Utility.Random( 3,31 ), 0x1537, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( HalfApron ), 10, Utility.Random( 3,31 ), 0x153b, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( LoinCloth ), 10, Utility.Random( 3,31 ), 0x2B68, 637 ) );
				Add( new GenericBuyInfo( typeof( RoyalLoinCloth ), 10, Utility.Random( 3,31 ), 0x55DB, 637 ) );
				Add( new GenericBuyInfo( typeof( Robe ), 18, Utility.Random( 3,31 ), 0x1F03, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( JokerRobe ), 40, Utility.Random( 1,5 ), 0x2B6B, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( AssassinRobe ), 40, Utility.Random( 1,5 ), 0x2B69, 0 ) );
				Add( new GenericBuyInfo( typeof( FancyRobe ), 40, Utility.Random( 1,5 ), 0x2B6A, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( GildedRobe ), 40, Utility.Random( 1,5 ), 0x2B6C, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( OrnateRobe ), 40, Utility.Random( 1,5 ), 0x2B6E, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( MagistrateRobe ), 40, Utility.Random( 1,5 ), 0x2B70, 0 ) );
				Add( new GenericBuyInfo( typeof( RoyalRobe ), 40, Utility.Random( 1,5 ), 0x2B73, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( SorcererRobe ), 40, Utility.Random( 1,5 ), 0x3175, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( AssassinShroud ), 40, Utility.Random( 1,5 ), 0x2FB9, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( NecromancerRobe ), 40, Utility.Random( 1,5 ), 0x2FBA, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( SpiderRobe ), 40, Utility.Random( 1,5 ), 0x2FC6, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( VagabondRobe ), 40, Utility.Random( 1,5 ), 0x567D, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( PirateCoat ), 40, Utility.Random( 1,5 ), 0x567E, 0 ) );
				Add( new GenericBuyInfo( typeof( ExquisiteRobe ), 40, Utility.Random( 1,5 ), 0x283, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( ProphetRobe ), 40, Utility.Random( 1,5 ), 0x284, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( ElegantRobe ), 40, Utility.Random( 1,5 ), 0x285, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( FormalRobe ), 40, Utility.Random( 1,5 ), 0x286, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( ArchmageRobe ), 40, Utility.Random( 1,5 ), 0x287, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( PriestRobe ), 40, Utility.Random( 1,5 ), 0x288, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( CultistRobe ), 40, Utility.Random( 1,5 ), 0x289, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( GildedDarkRobe ), 40, Utility.Random( 1,5 ), 0x28A, 0 ) );
				Add( new GenericBuyInfo( typeof( GildedLightRobe ), 40, Utility.Random( 1,5 ), 0x301, 0 ) );
				Add( new GenericBuyInfo( typeof( SageRobe ), 40, Utility.Random( 1,5 ), 0x302, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Cloak ), 8, Utility.Random( 3,31 ), 0x1515, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Doublet ), 13, Utility.Random( 3,31 ), 0x1F7B, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Tunic ), 18, Utility.Random( 3,31 ), 0x1FA1, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( JesterSuit ), 26, Utility.Random( 3,31 ), 0x1F9F, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( JesterHat ), 12, Utility.Random( 3,31 ), 0x171C, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( FloppyHat ), 7, Utility.Random( 3,31 ), 0x1713, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( WideBrimHat ), 8, Utility.Random( 3,31 ), 0x1714, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Cap ), 10, Utility.Random( 3,31 ), 0x1715, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( TallStrawHat ), 8, Utility.Random( 3,31 ), 0x1716, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( StrawHat ), 7, Utility.Random( 3,31 ), 0x1717, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( WizardsHat ), 11, Utility.Random( 3,31 ), 0x1718, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( WitchHat ), 11, Utility.Random( 1,15 ), 0x2FC3, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( LeatherCap ), 10, Utility.Random( 3,31 ), 0x1DB9, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( FeatheredHat ), 10, Utility.Random( 3,31 ), 0x171A, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( TricorneHat ), 8, Utility.Random( 3,31 ), 0x171B, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( PirateHat ), 8, Utility.Random( 3,31 ), 0x2FBC, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Bandana ), 6, Utility.Random( 3,31 ), 0x1540, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( SkullCap ), 7, Utility.Random( 3,31 ), 0x1544, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( ClothHood ), 12, Utility.Random( 1,15 ), 0x2B71, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( ClothCowl ), 12, Utility.Random( 1,15 ), 0x3176, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( WizardHood ), 12, Utility.Random( 1,15 ), 0x310, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( FancyHood ), 12, Utility.Random( 1,15 ), 0x4D09, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( BoltOfCloth ), 100, Utility.Random( 3,31 ), 0xf95, Utility.RandomDyedHue() ) ); 
				Add( new GenericBuyInfo( typeof( Cloth ), 2, Utility.Random( 3,31 ), 0x1766, Utility.RandomDyedHue() ) ); 
				Add( new GenericBuyInfo( typeof( UncutCloth ), 2, Utility.Random( 3,31 ), 0x1767, Utility.RandomDyedHue() ) ); 
				Add( new GenericBuyInfo( typeof( Cotton ), 102, Utility.Random( 3,31 ), 0xDF9, 0 ) );
				Add( new GenericBuyInfo( typeof( Wool ), 62, Utility.Random( 3,31 ), 0xDF8, 0 ) );
				Add( new GenericBuyInfo( typeof( Flax ), 102, Utility.Random( 3,31 ), 0x1A9C, 0 ) );
				Add( new GenericBuyInfo( typeof( SpoolOfThread ), 18, Utility.Random( 3,31 ), 0xFA0, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FemaleMannequinDeed ), Utility.Random( 5000,10000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MaleMannequinDeed ), Utility.Random( 5000,10000 ), 1, 0x14F0, 0 ) ); }
				
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Scissors ), 6 );
				Add( typeof( SewingKit ), 1 );
				Add( typeof( Dyes ), 4 );
				Add( typeof( DyeTub ), 4 );
				Add( typeof( BoltOfCloth ), 35 );
				Add( typeof( FancyShirt ), 10 );
				Add( typeof( Shirt ), 6 );
				Add( typeof( ShortPants ), 3 );
				Add( typeof( LongPants ), 5 );
				Add( typeof( Cloak ), 4 );
				Add( typeof( FancyDress ), 12 );
				Add( typeof( Robe ), 9 );
				Add( typeof( PlainDress ), 7 );
				Add( typeof( Skirt ), 5 );
				Add( typeof( Kilt ), 5 );
				Add( typeof( Doublet ), 7 );
				Add( typeof( Tunic ), 9 );
				Add( typeof( JesterSuit ), 13 );
				Add( typeof( FullApron ), 5 );
				Add( typeof( HalfApron ), 5 );
				Add( typeof( LoinCloth ), 5 );
				Add( typeof( JesterHat ), 6 );
				Add( typeof( FloppyHat ), 3 );
				Add( typeof( WideBrimHat ), 4 );
				Add( typeof( Cap ), 5 );
				Add( typeof( SkullCap ), 3 );
				Add( typeof( ClothCowl ), 6 );
				Add( typeof( ClothHood ), 6 );
				Add( typeof( WizardHood ), 6 );
				Add( typeof( FancyHood ), 6 );
				Add( typeof( Bandana ), 3 );
				Add( typeof( TallStrawHat ), 4 );
				Add( typeof( StrawHat ), 4 );
				Add( typeof( WizardsHat ), 5 );
				Add( typeof( WitchHat ), 5 );
				Add( typeof( Bonnet ), 4 );
				Add( typeof( FeatheredHat ), 5 );
				Add( typeof( TricorneHat ), 4 );
				Add( typeof( PirateHat ), 4 );
				Add( typeof( SpoolOfThread ), 9 );
				Add( typeof( Flax ), 51 );
				Add( typeof( Cotton ), 51 );
				Add( typeof( Wool ), 31 );
				Add( typeof( MagicRobe ), 30 ); 
				Add( typeof( MagicHat ), 20 ); 
				Add( typeof( MagicCloak ), 30 ); 
				Add( typeof( MagicBelt ), 20 ); 
				Add( typeof( MagicSash ), 20 ); 
				Add( typeof( JokerRobe ), 19 );
				Add( typeof( AssassinRobe ), 19 );
				Add( typeof( FancyRobe ), 19 );
				Add( typeof( GildedRobe ), 19 );
				Add( typeof( OrnateRobe ), 19 );
				Add( typeof( MagistrateRobe ), 19 );
				Add( typeof( VagabondRobe ), 19 );
				Add( typeof( PirateCoat ), 19 );
				Add( typeof( RoyalRobe ), 19 );
				Add( typeof( SorcererRobe ), 19 );
				Add( typeof( AssassinShroud ), 29 );
				Add( typeof( NecromancerRobe ), 19 );
				Add( typeof( SpiderRobe ), 19 );
				Add( typeof( ExquisiteRobe ), 19 );
				Add( typeof( ProphetRobe ), 19 );
				Add( typeof( ElegantRobe ), 19 );
				Add( typeof( FormalRobe ), 19 );
				Add( typeof( ArchmageRobe ), 19 );
				Add( typeof( PriestRobe ), 19 );
				Add( typeof( CultistRobe ), 19 );
				Add( typeof( GildedDarkRobe ), 19 );
				Add( typeof( GildedLightRobe ), 19 );
				Add( typeof( SageRobe ), 19 );
				Add( typeof( MagicScissors ), Utility.Random( 300,400 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMinerGuild: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMinerGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Bag ), 6, Utility.Random( 50,200 ), 0xE76, 0xABE ) );
				Add( new GenericBuyInfo( typeof( Candle ), 6, Utility.Random( 50,200 ), 0xA28, 0 ) );
				Add( new GenericBuyInfo( typeof( Torch ), 8, Utility.Random( 50,200 ), 0xF6B, 0 ) );
				Add( new GenericBuyInfo( typeof( Lantern ), 2, Utility.Random( 50,200 ), 0xA25, 0 ) );
				Add( new GenericBuyInfo( typeof( Pickaxe ), 25, Utility.Random( 50,200 ), 0xE86, 0 ) );
				Add( new GenericBuyInfo( typeof( Shovel ), 12, Utility.Random( 50,200 ), 0xF39, 0 ) );
				Add( new GenericBuyInfo( typeof( OreShovel ), 10, Utility.Random( 50,200 ), 0xF39, 0x96D ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Pickaxe ), 12 );
				Add( typeof( Shovel ), 6 );
				Add( typeof( OreShovel ), 5 );
				Add( typeof( Lantern ), 1 );
				Add( typeof( Torch ), 3 );
				Add( typeof( Bag ), 3 );
				Add( typeof( Candle ), 3 );
				Add( typeof( IronIngot ), 4 ); 
				Add( typeof( DullCopperIngot ), 8 ); 
				Add( typeof( ShadowIronIngot ), 12 ); 
				Add( typeof( CopperIngot ), 16 ); 
				Add( typeof( BronzeIngot ), 20 ); 
				Add( typeof( GoldIngot ), 24 ); 
				Add( typeof( AgapiteIngot ), 28 ); 
				Add( typeof( VeriteIngot ), 32 ); 
				Add( typeof( ValoriteIngot ), 36 ); 
				Add( typeof( NepturiteIngot ), 36 ); 
				Add( typeof( ObsidianIngot ), 36 ); 
				Add( typeof( SteelIngot ), 40 ); 
				Add( typeof( BrassIngot ), 44 ); 
				Add( typeof( MithrilIngot ), 48 ); 
				Add( typeof( XormiteIngot ), 48 ); 
				Add( typeof( DwarvenIngot ), 96 ); 
				Add( typeof( Amber ), 25 );
				Add( typeof( Amethyst ), 50 );
				Add( typeof( Citrine ), 25 );
				Add( typeof( Diamond ), 100 );
				Add( typeof( Emerald ), 50 );
				Add( typeof( Ruby ), 37 );
				Add( typeof( Sapphire ), 50 );
				Add( typeof( StarSapphire ), 62 );
				Add( typeof( Tourmaline ), 47 );
				if ( MyServerSettings.BuyChance() ){Add( typeof( RareAnvil ), Utility.Random( 200,1000 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Mage Guild vendor.
	/// Sells spellbooks, reagents, scrolls, magical hats, tools, and arcane supplies for guild members.
	/// </summary>
	public class SBMageGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMageGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Spellbook ), 18, Utility.Random( 50,200 ), 0xEFA, 0 ) );
				Add( new GenericBuyInfo( typeof( ScribesPen ), 8, Utility.Random( 50,200 ), 0x2051, 0 ) );
				Add( new GenericBuyInfo( typeof( BlankScroll ), 5, Utility.Random( 50,200 ), 0x0E34, 0 ) );
				Add( new GenericBuyInfo( "1041072", typeof( MagicWizardsHat ), 11, Utility.Random( 50,200 ), 0x1718, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( WitchHat ), 11, Utility.Random( 1,100 ), 0x2FC3, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( RecallRune ), 100, Utility.Random( 50,200 ), 0x1F14, 0 ) );
				Add( new GenericBuyInfo( typeof( BlackPearl ), 5, Utility.Random( 250,1000 ), 0x266F, 0 ) );
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, Utility.Random( 250,1000 ), 0xF7B, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 250,1000 ), 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 250,1000 ), 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, Utility.Random( 250,1000 ), 0xF86, 0 ) );
				Add( new GenericBuyInfo( typeof( Nightshade ), 3, Utility.Random( 250,1000 ), 0xF88, 0 ) );
				Add( new GenericBuyInfo( typeof( SpidersSilk ), 3, Utility.Random( 250,1000 ), 0xF8D, 0 ) );
				Add( new GenericBuyInfo( typeof( SulfurousAsh ), 3, Utility.Random( 250,1000 ), 0xF8C, 0 ) );
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( reagents_magic_jar1 ), 2000, Utility.Random( 50,200 ), 0x1007, 0 ) ); }
				Add( new GenericBuyInfo( typeof( WizardStaff ), 40, Utility.Random( 1,5 ), 0x0908, 0xB3A ) );
				Add( new GenericBuyInfo( typeof( WizardStick ), 38, Utility.Random( 1,5 ), 0xDF2, 0xB3A ) );
				Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "Stable Gates", typeof( LinkedGateBag ), 250000, 1, 0xE76, 0xABE ) ); }


				Type[] types = Loot.RegularScrollTypes;

				int circles = 4;

				for ( int i = 0; i < circles*8 && i < types.Length; ++i )
				{
					int itemID = 0x1F2E + i;

					if ( i == 6 )
						itemID = 0x1F2D;
					else if ( i > 6 )
						--itemID;

					Add( new GenericBuyInfo( types[i], 12 + ((i / 8) * 10), 20, itemID, 0 ) );
				}
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( MagicTalisman ), Utility.Random( 50,100 ) ); 
				Add( typeof( WizardsHat ), 100 );
				Add( typeof( WitchHat ), 5 );
				Add( typeof( BlackPearl ), 3 ); 
				Add( typeof( Bloodmoss ),4 ); 
				Add( typeof( MandrakeRoot ), 2 ); 
				Add( typeof( Garlic ), 2 ); 
				Add( typeof( Ginseng ), 2 ); 
				Add( typeof( Nightshade ), 2 ); 
				Add( typeof( SpidersSilk ), 2 ); 
				Add( typeof( SulfurousAsh ), 1 ); 
				Add( typeof( RecallRune ), 13 );
				Add( typeof( Spellbook ), 25 );
				Add( typeof( MysticalPearl ), 250 );

				Type[] types = Loot.RegularScrollTypes;

				for ( int i = 0; i < types.Length; ++i )
					Add(types[i], i + 3 + (i / 4));
			
				Add( typeof( ClumsyMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( CreateFoodMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( FeebleMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( HealMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( MagicArrowMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( NightSightMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( ReactiveArmorMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( WeaknessMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( AgilityMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( CunningMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( CureMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( HarmMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( MagicTrapMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( MagicUntrapMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( ProtectionMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( StrengthMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( BlessMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( FireballMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( MagicLockMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( MagicUnlockMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( PoisonMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( TelekinesisMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( TeleportMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( WallofStoneMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( ArchCureMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( ArchProtectionMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( CurseMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( FireFieldMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( GreaterHealMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( LightningMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( ManaDrainMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( RecallMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( BladeSpiritsMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( DispelFieldMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( IncognitoMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( MagicReflectionMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( MindBlastMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( ParalyzeMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( PoisonFieldMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( SummonCreatureMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( DispelMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( EnergyBoltMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( ExplosionMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( InvisibilityMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( MarkMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( MassCurseMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( ParalyzeFieldMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( RevealMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( ChainLightningMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( EnergyFieldMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( FlameStrikeMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( GateTravelMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( ManaVampireMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( MassDispelMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( MeteorSwarmMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( PolymorphMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( AirElementalMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( EarthElementalMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( EarthquakeMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( EnergyVortexMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( FireElementalMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( ResurrectionMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( SummonDaemonMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( WaterElementalMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( MySpellbook ), 500 );
				Add( typeof( TomeOfWands ), Utility.Random( 100,400 ) );
				Add( typeof( DemonPrison ), Utility.Random( 500,1000 ) );
				Add( typeof( WizardStaff ), 20 );
				Add( typeof( WizardStick ), 19 );
				Add( typeof( MageEye ), 1 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBHealerGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHealerGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Bandage ), 2, Utility.Random( 250,1000 ), 0xE21, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 50,200 ), 0x25FD, 0 ) );
				Add( new GenericBuyInfo( typeof( HealPotion ), 30, Utility.Random( 50,200 ), 0xF0C, 0 ) );
				Add( new GenericBuyInfo( typeof( GreaterHealPotion ), 60, Utility.Random( 50,200 ), 0x25FE, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 50,200 ), 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 50,200 ), 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 50,200 ), 0xF0B, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( LesserHealPotion ), 7 );
				Add( typeof( HealPotion ), 14 );
				Add( typeof( GreaterHealPotion ), 28 );
				Add( typeof( RefreshPotion ), 7 );
				Add( typeof( Garlic ), 2 );
				Add( typeof( Ginseng ), 2 );
				Add( typeof( FirstAidKit ), Utility.Random( 100,250 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSailorGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSailorGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "1041205", typeof( SmallBoatDeed ), 9500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) );
				Add( new GenericBuyInfo( "1041206", typeof( SmallDragonBoatDeed ), 10500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) );
				Add( new GenericBuyInfo( "1041207", typeof( MediumBoatDeed ), 11500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) );
				Add( new GenericBuyInfo( "1041208", typeof( MediumDragonBoatDeed ), 12500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) );
				Add( new GenericBuyInfo( "1041209", typeof( LargeBoatDeed ), 13500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) );
				Add( new GenericBuyInfo( "1041210", typeof( LargeDragonBoatDeed ), 14500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) );
				Add( new GenericBuyInfo( typeof( DockingLantern ), 58, Utility.Random( 50,200 ), 0x40FF, 0 ) );
				Add( new GenericBuyInfo( typeof( RawFishSteak ), 10, Utility.Random( 50,200 ), 0x97A, 0 ) );
				Add( new GenericBuyInfo( typeof( Fish ), 30, Utility.Random( 50,200 ), 0x9CC, 0 ) );
				Add( new GenericBuyInfo( typeof( Fish ), 30, Utility.Random( 50,200 ), 0x9CD, 0 ) );
				Add( new GenericBuyInfo( typeof( Fish ), 30, Utility.Random( 50,200 ), 0x9CE, 0 ) );
				Add( new GenericBuyInfo( typeof( Fish ), 30, Utility.Random( 50,200 ), 0x9CF, 0 ) );
				Add( new GenericBuyInfo( typeof( FishingPole ), 100, Utility.Random( 50,200 ), 0xDC0, 0 ) );
				Add( new GenericBuyInfo( typeof( BoatStain ), 26, Utility.Random( 1,100 ), 0x14E0, 0 ) );
				Add( new GenericBuyInfo( typeof( Sextant ), 13, Utility.Random( 1,100 ), 0x1057, 0 ) );
				Add( new GenericBuyInfo( typeof( GrapplingHook ), 58, Utility.Random( 1,100 ), 0x4F40, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( SeaShell ), 58 );
				Add( typeof( DockingLantern ), 29 );
				Add( typeof( RawFishSteak ), 5 );
				Add( typeof( Fish ), 3 );
				Add( typeof( FishingPole ), 7 );
				Add( typeof( Sextant ), 6 );
				Add( typeof( GrapplingHook ), 29 );
				Add( typeof( PirateChest ), Utility.RandomMinMax( 200, 800 ) );
				Add( typeof( SunkenChest ), Utility.RandomMinMax( 200, 800 ) );
				Add( typeof( FishingNet ), Utility.RandomMinMax( 20, 40 ) );
				Add( typeof( SpecialFishingNet ), Utility.RandomMinMax( 60, 80 ) );
				Add( typeof( FabledFishingNet ), Utility.RandomMinMax( 100, 120 ) );
				Add( typeof( NeptunesFishingNet ), Utility.RandomMinMax( 140, 160 ) );
				Add( typeof( PrizedFish ), Utility.RandomMinMax( 60, 120 ) );
				Add( typeof( WondrousFish ), Utility.RandomMinMax( 60, 120 ) );
				Add( typeof( TrulyRareFish ), Utility.RandomMinMax( 60, 120 ) );
				Add( typeof( PeculiarFish ), Utility.RandomMinMax( 60, 120 ) );
				Add( typeof( SpecialSeaweed ), Utility.RandomMinMax( 40, 160 ) );
				Add( typeof( SunkenBag ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( ShipwreckedItem ), Utility.RandomMinMax( 20, 60 ) );
				Add( typeof( HighSeasRelic ), Utility.RandomMinMax( 20, 60 ) );
				Add( typeof( BoatStain ), 13 );
				Add( typeof( MegalodonTooth ), Utility.RandomMinMax( 1000, 2000 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBlacksmithGuild : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBBlacksmithGuild() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{
				Add( new GenericBuyInfo( typeof( GuildHammer ), 500, Utility.Random( 1,5 ), 0xFB5, 0x430 ) );
				Add( new GenericBuyInfo( typeof( IronIngot ), 5, Utility.Random( 10,1000 ), 0x1BF2, 0 ) );
				Add( new GenericBuyInfo( typeof( Tongs ), 13, Utility.Random( 50,200 ), 0xFBB, 0 ) ); 
				Add( new GenericBuyInfo( typeof( BronzeShield ), 66, Utility.Random( 50,200 ), 0x1B72, 0 ) );
				Add( new GenericBuyInfo( typeof( Buckler ), 50, Utility.Random( 50,200 ), 0x1B73, 0 ) );
				Add( new GenericBuyInfo( typeof( MetalKiteShield ), 123, Utility.Random( 50,200 ), 0x1B74, 0 ) );
				Add( new GenericBuyInfo( typeof( HeaterShield ), 231, Utility.Random( 50,200 ), 0x1B76, 0 ) );
				Add( new GenericBuyInfo( typeof( MetalShield ), 121, Utility.Random( 50,200 ), 0x1B7B, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateGorget ), 104, Utility.Random( 50,200 ), 0x1413, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateChest ), 243, Utility.Random( 50,200 ), 0x1415, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateLegs ), 218, Utility.Random( 50,200 ), 0x1411, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateArms ), 188, Utility.Random( 50,200 ), 0x1410, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateGloves ), 155, Utility.Random( 50,200 ), 0x1414, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateHelm ), 21, Utility.Random( 50,200 ), 0x1412, 0 ) );
				Add( new GenericBuyInfo( typeof( CloseHelm ), 18, Utility.Random( 50,200 ), 0x1408, 0 ) );
				Add( new GenericBuyInfo( typeof( CloseHelm ), 18, Utility.Random( 50,200 ), 0x1409, 0 ) );
				Add( new GenericBuyInfo( typeof( Helmet ), 31, Utility.Random( 50,200 ), 0x140A, 0 ) );
				Add( new GenericBuyInfo( typeof( Helmet ), 18, Utility.Random( 50,200 ), 0x140B, 0 ) );
				Add( new GenericBuyInfo( typeof( NorseHelm ), 18, Utility.Random( 50,200 ), 0x140E, 0 ) );
				Add( new GenericBuyInfo( typeof( NorseHelm ), 18, Utility.Random( 50,200 ), 0x140F, 0 ) );
				Add( new GenericBuyInfo( typeof( Bascinet ), 18, Utility.Random( 50,200 ), 0x140C, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateHelm ), 21, Utility.Random( 50,200 ), 0x1419, 0 ) );
				Add( new GenericBuyInfo( typeof( DreadHelm ), 21, Utility.Random( 50,200 ), 0x2FBB, 0 ) );
				Add( new GenericBuyInfo( typeof( ChainCoif ), 17, Utility.Random( 50,200 ), 0x13BB, 0 ) );
				Add( new GenericBuyInfo( typeof( ChainChest ), 143, Utility.Random( 50,200 ), 0x13BF, 0 ) );
				Add( new GenericBuyInfo( typeof( ChainLegs ), 149, Utility.Random( 50,200 ), 0x13BE, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailChest ), 121, Utility.Random( 50,200 ), 0x13ec, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailLegs ), 90, Utility.Random( 50,200 ), 0x13F0, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailArms ), 85, Utility.Random( 50,200 ), 0x13EE, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailGloves ), 93, Utility.Random( 50,200 ), 0x13eb, 0 ) );
				Add( new GenericBuyInfo( typeof( ExecutionersAxe ), 30, Utility.Random( 50,200 ), 0xF45, 0 ) );
				Add( new GenericBuyInfo( typeof( Bardiche ), 60, Utility.Random( 50,200 ), 0xF4D, 0 ) );
				Add( new GenericBuyInfo( typeof( BattleAxe ), 26, Utility.Random( 50,200 ), 0xF47, 0 ) );
				Add( new GenericBuyInfo( typeof( TwoHandedAxe ), 32, Utility.Random( 50,200 ), 0x1443, 0 ) );
				Add( new GenericBuyInfo( typeof( ButcherKnife ), 14, Utility.Random( 50,200 ), 0x13F6, 0 ) );
				Add( new GenericBuyInfo( typeof( Cutlass ), 24, Utility.Random( 50,200 ), 0x1441, 0 ) );
				Add( new GenericBuyInfo( typeof( Dagger ), 21, Utility.Random( 50,200 ), 0xF52, 0 ) );
				Add( new GenericBuyInfo( typeof( Halberd ), 42, Utility.Random( 50,200 ), 0x143E, 0 ) );
				Add( new GenericBuyInfo( typeof( HammerPick ), 26, Utility.Random( 50,200 ), 0x143D, 0 ) );
				Add( new GenericBuyInfo( typeof( Katana ), 33, Utility.Random( 50,200 ), 0x13FF, 0 ) );
				Add( new GenericBuyInfo( typeof( Kryss ), 32, Utility.Random( 50,200 ), 0x1401, 0 ) );
				Add( new GenericBuyInfo( typeof( Broadsword ), 35, Utility.Random( 50,200 ), 0xF5E, 0 ) );
				Add( new GenericBuyInfo( typeof( Longsword ), 55, Utility.Random( 50,200 ), 0xF61, 0 ) );
				Add( new GenericBuyInfo( typeof( ThinLongsword ), 27, Utility.Random( 50,200 ), 0x13B8, 0 ) );
				Add( new GenericBuyInfo( typeof( VikingSword ), 55, Utility.Random( 50,200 ), 0x13B9, 0 ) );
				Add( new GenericBuyInfo( typeof( Cleaver ), 100, Utility.Random( 50,200 ), 0xEC3, 0 ) );
				Add( new GenericBuyInfo( typeof( Axe ), 40, Utility.Random( 50,200 ), 0xF49, 0 ) );
				Add( new GenericBuyInfo( typeof( DoubleAxe ), 52, Utility.Random( 50,200 ), 0xF4B, 0 ) );
				Add( new GenericBuyInfo( typeof( Pickaxe ), 22, Utility.Random( 50,200 ), 0xE86, 0 ) );
				Add( new GenericBuyInfo( typeof( Pitchfork ), 19, Utility.Random( 50,200 ), 0xE87, 0xB3A ) );
				Add( new GenericBuyInfo( typeof( Scimitar ), 36, Utility.Random( 50,200 ), 0x13B6, 0 ) );
				Add( new GenericBuyInfo( typeof( SkinningKnife ), 14, Utility.Random( 50,200 ), 0xEC4, 0 ) );
				Add( new GenericBuyInfo( typeof( LargeBattleAxe ), 33, Utility.Random( 50,200 ), 0x13FB, 0 ) );
				Add( new GenericBuyInfo( typeof( WarAxe ), 29, Utility.Random( 50,200 ), 0x13B0, 0 ) );
				Add( new GenericBuyInfo( typeof( BoneHarvester ), 35, Utility.Random( 50,200 ), 0x26BB, 0 ) );
				Add( new GenericBuyInfo( typeof( CrescentBlade ), 37, Utility.Random( 50,200 ), 0x26C1, 0 ) );
				Add( new GenericBuyInfo( typeof( DoubleBladedStaff ), 35, Utility.Random( 50,200 ), 0x26BF, 0 ) );
				Add( new GenericBuyInfo( typeof( Lance ), 34, Utility.Random( 50,200 ), 0x26C0, 0 ) );
				Add( new GenericBuyInfo( typeof( Pike ), 39, Utility.Random( 50,200 ), 0x26BE, 0 ) );
				Add( new GenericBuyInfo( typeof( Scythe ), 39, Utility.Random( 50,200 ), 0x26BA, 0 ) );
				Add( new GenericBuyInfo( typeof( Mace ), 28, Utility.Random( 50,200 ), 0xF5C, 0 ) );
				Add( new GenericBuyInfo( typeof( Maul ), 21, Utility.Random( 50,200 ), 0x143B, 0 ) );
				Add( new GenericBuyInfo( typeof( SmithHammer ), 21, Utility.Random( 50,200 ), 0x0FB4, 0 ) );
				Add( new GenericBuyInfo( typeof( ShortSpear ), 23, Utility.Random( 50,200 ), 0x1403, 0 ) );
				Add( new GenericBuyInfo( typeof( Spear ), 31, Utility.Random( 50,200 ), 0xF62, 0 ) );
				Add( new GenericBuyInfo( typeof( WarHammer ), 25, Utility.Random( 50,200 ), 0x1439, 0 ) );
				Add( new GenericBuyInfo( typeof( WarMace ), 31, Utility.Random( 50,200 ), 0x1407, 0 ) );
				Add( new GenericBuyInfo( typeof( Scepter ), 39, Utility.Random( 50,200 ), 0x26BC, 0 ) );
				Add( new GenericBuyInfo( typeof( BladedStaff ), 40, Utility.Random( 50,200 ), 0x26BD, 0 ) );
				Add( new GenericBuyInfo( typeof( GuardsmanShield ), 231, Utility.Random( 1,100 ), 0x2FCB, 0 ) );
				Add( new GenericBuyInfo( typeof( ElvenShield ), 231, Utility.Random( 1,100 ), 0x2FCA, 0 ) );
				Add( new GenericBuyInfo( typeof( DarkShield ), 231, Utility.Random( 1,100 ), 0x2FC8, 0 ) );
				Add( new GenericBuyInfo( typeof( CrestedShield ), 231, Utility.Random( 1,100 ), 0x2FC9, 0 ) );
				Add( new GenericBuyInfo( typeof( ChampionShield ), 231, Utility.Random( 1,100 ), 0x2B74, 0 ) );
				Add( new GenericBuyInfo( typeof( JeweledShield ), 231, Utility.Random( 1,100 ), 0x2B75, 0 ) );
				Add( new GenericBuyInfo( typeof( AssassinSpike ), 21, Utility.Random( 1,100 ), 0x2D21, 0 ) );
				Add( new GenericBuyInfo( typeof( Leafblade ), 21, Utility.Random( 1,100 ), 0x2D22, 0 ) );
				Add( new GenericBuyInfo( typeof( WarCleaver ), 25, Utility.Random( 1,100 ), 0x2D2F, 0 ) );
				Add( new GenericBuyInfo( typeof( DiamondMace ), 31, Utility.Random( 1,100 ), 0x2D24, 0 ) );
				Add( new GenericBuyInfo( typeof( OrnateAxe ), 42, Utility.Random( 1,100 ), 0x2D28, 0 ) );
				Add( new GenericBuyInfo( typeof( RuneBlade ), 55, Utility.Random( 1,100 ), 0x2D32, 0 ) );
				Add( new GenericBuyInfo( typeof( RadiantScimitar ), 35, Utility.Random( 1,100 ), 0x2D33, 0 ) );
				Add( new GenericBuyInfo( typeof( ElvenSpellblade ), 33, Utility.Random( 1,100 ), 0x2D20, 0 ) );
				Add( new GenericBuyInfo( typeof( ElvenMachete ), 35, Utility.Random( 1,100 ), 0x2D35, 0 ) );
				Add( new GenericBuyInfo( typeof( RandomLetter ), 150, Utility.Random( 25,100 ), 0x55BF, 0 ) ); 

			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				Add( typeof( GuardsmanShield ), 115 );
				Add( typeof( ElvenShield ), 115 );
				Add( typeof( DarkShield ), 115 );
				Add( typeof( CrestedShield ), 115 );
				Add( typeof( ChampionShield ), 115 );
				Add( typeof( JeweledShield ), 115 );
				Add( typeof( TopazIngot ), 120 ); 
				Add( typeof( ShinySilverIngot ), 120 ); 
				Add( typeof( AmethystIngot ), 120 ); 
				Add( typeof( EmeraldIngot ), 120 ); 
				Add( typeof( GarnetIngot ), 120 ); 
				Add( typeof( IceIngot ), 120 ); 
				Add( typeof( JadeIngot ), 120 ); 
				Add( typeof( MarbleIngot ), 120 ); 
				Add( typeof( OnyxIngot ), 120 ); 
				Add( typeof( QuartzIngot ), 120 ); 
				Add( typeof( RubyIngot ), 120 ); 
				Add( typeof( SapphireIngot ), 120 ); 
				Add( typeof( SpinelIngot ), 120 ); 
				Add( typeof( StarRubyIngot ), 120 ); 
				Add( typeof( Tongs ), 7 ); 
				Add( typeof( IronIngot ), 4 ); 
				Add( typeof( DullCopperIngot ), 8 ); 
				Add( typeof( ShadowIronIngot ), 12 ); 
				Add( typeof( CopperIngot ), 16 ); 
				Add( typeof( BronzeIngot ), 20 ); 
				Add( typeof( GoldIngot ), 24 ); 
				Add( typeof( AgapiteIngot ), 28 ); 
				Add( typeof( VeriteIngot ), 32 ); 
				Add( typeof( ValoriteIngot ), 36 ); 
				Add( typeof( NepturiteIngot ), 36 ); 
				Add( typeof( SteelIngot ), 40 ); 
				Add( typeof( BrassIngot ), 44 ); 
				Add( typeof( MithrilIngot ), 48 ); 
				Add( typeof( XormiteIngot ), 48 ); 
				Add( typeof( DwarvenIngot ), 96 ); 
				Add( typeof( ObsidianIngot ), 36 ); 
				Add( typeof( Buckler ), 25 );
				Add( typeof( BronzeShield ), 33 );
				Add( typeof( MetalShield ), 60 );
				Add( typeof( MetalKiteShield ), 62 );
				Add( typeof( HeaterShield ), 115 );
				Add( typeof( PlateArms ), 94 );
				Add( typeof( PlateChest ), 121 );
				Add( typeof( PlateGloves ), 72 );
				Add( typeof( PlateGorget ), 52 );
				Add( typeof( PlateLegs ), 109 );
				Add( typeof( PlateSkirt ), 109 );
				Add( typeof( FemalePlateChest ), 113 );
				Add( typeof( Bascinet ), 9 );
				Add( typeof( CloseHelm ), 9 );
				Add( typeof( Helmet ), 9 );
				Add( typeof( NorseHelm ), 9 );
				Add( typeof( PlateHelm ), 10 );
				Add( typeof( DreadHelm ), 10 );
				Add( typeof( ChainCoif ), 6 );
				Add( typeof( ChainChest ), 71 );
				Add( typeof( ChainLegs ), 74 );
				Add( typeof( ChainSkirt ), 74 );
				Add( typeof( RingmailArms ), 42 );
				Add( typeof( RingmailChest ), 60 );
				Add( typeof( RingmailGloves ), 26 );
				Add( typeof( RingmailLegs ), 45 );
				Add( typeof( RingmailSkirt ), 45 );
				Add( typeof( BattleAxe ), 13 );
				Add( typeof( DoubleAxe ), 26 );
				Add( typeof( ExecutionersAxe ), 10 );
				Add( typeof( LargeBattleAxe ),16 );
				Add( typeof( Pickaxe ), 11 );
				Add( typeof( TwoHandedAxe ), 16 );
				Add( typeof( WarAxe ), 14 );
				Add( typeof( Axe ), 20 );
				Add( typeof( Bardiche ), 30 );
				Add( typeof( Halberd ), 21 );
				Add( typeof( ButcherKnife ), 7 );
				Add( typeof( Cleaver ), 7 );
				Add( typeof( Dagger ), 10 );
				Add( typeof( SkinningKnife ), 7 );
				Add( typeof( HammerPick ), 13 );
				Add( typeof( Mace ), 14 );
				Add( typeof( Maul ), 10 );
				Add( typeof( WarHammer ), 12 );
				Add( typeof( WarMace ), 10 );
				Add( typeof( Scepter ), 20 );
				Add( typeof( BladedStaff ), 20 );
				Add( typeof( Scythe ), 19 );
				Add( typeof( BoneHarvester ), 17 );
				Add( typeof( Scepter ), 18 );
				Add( typeof( BladedStaff ), 16 );
				Add( typeof( Pike ), 19 );
				Add( typeof( DoubleBladedStaff ), 17 );
				Add( typeof( Lance ), 17 );
				Add( typeof( CrescentBlade ), 18 );
				Add( typeof( Spear ), 10 );
				Add( typeof( Pitchfork ), 9 );
				Add( typeof( ShortSpear ), 11 );
				Add( typeof( SmithHammer ), 10 );
				Add( typeof( Broadsword ), 17 );
				Add( typeof( Cutlass ), 12 );
				Add( typeof( Katana ), 16 );
				Add( typeof( Kryss ), 16 );
				Add( typeof( Longsword ), 27 );
				Add( typeof( Scimitar ), 18 );
				Add( typeof( ThinLongsword ), 13 );
				Add( typeof( VikingSword ), 27 );
				Add( typeof( AssassinSpike ), 10 );
				Add( typeof( Leafblade ), 10 );
				Add( typeof( WarCleaver ), 12 );
				Add( typeof( DiamondMace ), 10 );
				Add( typeof( OrnateAxe ),21 );
				Add( typeof( RuneBlade ), 27 );
				Add( typeof( RadiantScimitar ), 17 );
				Add( typeof( ElvenSpellblade ), 16 );
				Add( typeof( ElvenMachete ), 17 );
				Add( typeof( RareAnvil ), Utility.Random( 400,1500 ) );
				Add( typeof( MagicHammer ), Utility.Random( 300,400 ) );
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBardGuild: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBBardGuild() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{
				Add( new GenericBuyInfo( typeof( Drums ), 21, Utility.Random( 50,200 ), 0x0E9C, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Tambourine ), 21, Utility.Random( 50,200 ), 0x0E9E, 0 ) ); 
				Add( new GenericBuyInfo( typeof( LapHarp ), 21, Utility.Random( 50,200 ), 0x0EB2, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Lute ), 21, Utility.Random( 50,200 ), 0x0EB3, 0 ) ); 
				Add( new GenericBuyInfo( typeof( BambooFlute ), 21, Utility.Random( 1,100 ), 0x2805, 0 ) );
				Add( new GenericBuyInfo( typeof( SongBook ), 24, Utility.Random( 1,5 ), 0x225A, 0 ) ); 
				Add( new GenericBuyInfo( typeof( EnergyCarolScroll ), 32, Utility.Random( 1,5 ), 0x1F48, 0x96 ) ); 
				Add( new GenericBuyInfo( typeof( FireCarolScroll ), 32, Utility.Random( 1,5 ), 0x1F49, 0x96 ) ); 
				Add( new GenericBuyInfo( typeof( IceCarolScroll ), 32, Utility.Random( 1,5 ), 0x1F34, 0x96 ) ); 
				Add( new GenericBuyInfo( typeof( KnightsMinneScroll ), 32, Utility.Random( 1,5 ), 0x1F31, 0x96 ) ); 
				Add( new GenericBuyInfo( typeof( PoisonCarolScroll ), 32, Utility.Random( 1,5 ), 0x1F33, 0x96 ) ); 
				Add( new GenericBuyInfo( typeof( JarsOfWaxInstrument ), 160, Utility.Random( 1,5 ), 0x1005, 0x845 ) );
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				Add( typeof( JarsOfWaxInstrument ), 80 );
				Add( typeof( BambooFlute ), 10 );
				Add( typeof( LapHarp ), 10 ); 
				Add( typeof( Lute ), 10 ); 
				Add( typeof( Drums ), 10 ); 
				Add( typeof( Harp ), 10 ); 
				Add( typeof( Tambourine ), 10 );
				Add( typeof( MySongbook ), 200 );
				Add( typeof( SongBook ), 12 ); 
				Add( typeof( EnergyCarolScroll ), 5 ); 
				Add( typeof( FireCarolScroll ), 5 ); 
				Add( typeof( IceCarolScroll ), 5 ); 
				Add( typeof( KnightsMinneScroll ), 5 ); 
				Add( typeof( PoisonCarolScroll ), 5 ); 
				Add( typeof( ArmysPaeonScroll ), 6 ); 
				Add( typeof( MagesBalladScroll ), 6 ); 
				Add( typeof( EnchantingEtudeScroll ), 7 ); 
				Add( typeof( SheepfoeMamboScroll ), 7 ); 
				Add( typeof( SinewyEtudeScroll ), 7 ); 
				Add( typeof( EnergyThrenodyScroll ), 8 ); 
				Add( typeof( FireThrenodyScroll ), 8 ); 
				Add( typeof( IceThrenodyScroll ), 8 ); 
				Add( typeof( PoisonThrenodyScroll ), 8 ); 
				Add( typeof( FoeRequiemScroll ), 9 ); 
				Add( typeof( MagicFinaleScroll ), 10 ); 
			} 
		} 
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBHolidayXmas : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHolidayXmas()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( WreathDeed ), 30000, Utility.Random( 1,3 ), 0x14EF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreenStocking ), 11000, Utility.Random( 1,3 ), 0x2bd9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( RedStocking ), 11000, Utility.Random( 1,3 ), 0x2bdb, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( SnowPileDeco ), 8000, Utility.Random( 1,3 ), 0x8E2, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( Snowman ), 23000, Utility.Random( 1,3 ), 0x2328, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BlueSnowflake ), 10000, Utility.Random( 1,3 ), 0x232E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( WhiteSnowflake ), 10000, Utility.Random( 1,3 ), 0x232F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( RedPoinsettia ), 12000, Utility.Random( 1,3 ), 0x2330, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( WhitePoinsettia ), 12000, Utility.Random( 1,3 ), 0x2331, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcyPatch ), 6000, Utility.Random( 1,3 ), 0x122F, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcicleLargeSouth ), 8000, Utility.Random( 1,3 ), 0x4572, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcicleMedSouth ), 7000, Utility.Random( 1,3 ), 0x4573, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcicleSmallSouth ), 6000, Utility.Random( 1,3 ), 0x4574, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcicleLargeEast ), 8000, Utility.Random( 1,3 ), 0x4575, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcicleMedEast ), 7000, Utility.Random( 1,3 ), 0x4576, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcicleSmallEast ), 6000, Utility.Random( 1,3 ), 0x4577, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GingerBreadHouseDeed ), 45000, Utility.Random( 1,3 ), 0x14EF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CandyCane ), 2000, Utility.Random( 1,3 ), 0x2bdd, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GingerBreadCookie ), 2000, Utility.Random( 1,3 ), 0x2be1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HolidayBell ), 28000, Utility.Random( 1,3 ), 0x1C12, 0xA ) ); }
			if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HolidayBells ), 15000, Utility.Random( 1,3 ), 0x544F, 0 ) ); }

				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBoxRectangle ), 14000, Utility.Random( 1,3 ), 0x46A6, Utility.RandomList( 0x672, 0x454, 0x507, 0x4ac, 0x504, 0x84b, 0x495, 0x97c, 0x493, 0x4a8, 0x494, 0x4aa, 0xb8b, 0x84f, 0x491, 0x851, 0x503, 0xb8c, 0x4ab, 0x84B ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBoxCube ), 14000, Utility.Random( 1,3 ), 0x46A2, Utility.RandomList( 0x672, 0x454, 0x507, 0x4ac, 0x504, 0x84b, 0x495, 0x97c, 0x493, 0x4a8, 0x494, 0x4aa, 0xb8b, 0x84f, 0x491, 0x851, 0x503, 0xb8c, 0x4ab, 0x84B ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBoxCylinder ), 14000, Utility.Random( 1,3 ), 0x46A3, Utility.RandomList( 0x672, 0x454, 0x507, 0x4ac, 0x504, 0x84b, 0x495, 0x97c, 0x493, 0x4a8, 0x494, 0x4aa, 0xb8b, 0x84f, 0x491, 0x851, 0x503, 0xb8c, 0x4ab, 0x84B ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBoxOctogon ), 14000, Utility.Random( 1,3 ), 0x46A4, Utility.RandomList( 0x672, 0x454, 0x507, 0x4ac, 0x504, 0x84b, 0x495, 0x97c, 0x493, 0x4a8, 0x494, 0x4aa, 0xb8b, 0x84f, 0x491, 0x851, 0x503, 0xb8c, 0x4ab, 0x84B ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBoxAngel ), 14000, Utility.Random( 1,3 ), 0x46A7, Utility.RandomList( 0x672, 0x454, 0x507, 0x4ac, 0x504, 0x84b, 0x495, 0x97c, 0x493, 0x4a8, 0x494, 0x4aa, 0xb8b, 0x84f, 0x491, 0x851, 0x503, 0xb8c, 0x4ab, 0x84B ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBoxNeon ), 14000, Utility.Random( 1,3 ), 0x232A, Utility.RandomList( 0x438, 0x424, 0x433, 0x445, 0x42b, 0x448 ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBox ), 14000, Utility.Random( 1,3 ), 0x232A, Utility.RandomDyedHue() ) ); }
				Add( new GenericBuyInfo( typeof( HolidayTreeDeed ), 8600, Utility.Random( 1,3 ), 0x0CC8, 0 ) );
				Add( new GenericBuyInfo( typeof( HolidayTreeFlatDeed ), 8600, Utility.Random( 1,3 ), 0x0CC8, 0 ) );
				Add( new GenericBuyInfo( typeof( NewHolidayTree ), 19800, Utility.Random( 1,3 ), 0x4C7D, 0 ) );
				Add( new GenericBuyInfo( typeof( RibbonTreeSmall ), 7000, Utility.Random( 1,3 ), 0x5448, 0 ) );
				Add( new GenericBuyInfo( typeof( RibbonTree ), 18000, Utility.Random( 1,3 ), 0x5447, 0 ) );
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ChristmasRobe ), 500, Utility.Random( 1,3 ), 0x2684, 1153 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBHolidayHalloween : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHolidayHalloween()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BloodyTableAddonDeed ), 17500, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BloodPentagramDeed ), 75000, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( MongbatDartBoardEastDeed ), 1200, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( MongbatDartBoardSouthDeed ), 1200, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTree6 ), 10800, Utility.Random( 1,3 ), 0xCCD, 0x2C1 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTree5 ), 10800, Utility.Random( 1,3 ), 0x224D, 0x2C1 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTree4 ), 10800, Utility.Random( 1,3 ), 0xCD3, 0x2C1 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTree3 ), 10800, Utility.Random( 1,3 ), 0x224A, 0x2C5 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTree2 ), 10800, Utility.Random( 1,3 ), 0xD94, 0x2C5 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTree1 ), 10800, Utility.Random( 1,3 ), 0xCE3, 0x2C5 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTortSkel ), 7450, Utility.Random( 1,3 ), 0x1A03, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenStoneSpike2 ), 7600, Utility.Random( 1,3 ), 0x2202, 0x322 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenStoneSpike ), 10600, Utility.Random( 1,3 ), 0x2201, 0x322 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenStoneColumn ), 10500, Utility.Random( 1,3 ), 0x77, 0x322 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenSkullPole ), 10540, Utility.Random( 1,3 ), 0x2204, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenShrineChaosDeed ), 200380, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenPylonFire ), 22100, Utility.Random( 1,3 ), 0x19AF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenPylon ), 11800, Utility.Random( 1,3 ), 0x1ECB, 0x322 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenMaiden ), 22780, Utility.Random( 1,3 ), 0x124B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenGrave3 ), 3350, Utility.Random( 1,3 ), 0xEDE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenGrave2 ), 3350, Utility.Random( 1,3 ), 0x116E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenGrave1 ), 3350, Utility.Random( 1,3 ), 0x1168, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenColumn ), 8100, Utility.Random( 1,3 ), 0x196, 0x322 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenChopper ), 8760, Utility.Random( 1,3 ), 0x1245, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenBonePileDeed ), 5680, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenBlood ), 990, Utility.Random( 1,3 ), 0x122A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( AppleBobbingBarrel ), 570, Utility.Random( 1,3 ), 0x0F33, 0 ) ); }
				Add( new GenericBuyInfo( typeof( CarvedPumpkin ), 580, Utility.Random( 1,3 ), 0x4694, 0 ) );
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin2 ), 580, Utility.Random( 1,3 ), 0x4698, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin3 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5457, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin4 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x545B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin5 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x545F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin6 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5464, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin7 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5468, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin8 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x546C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin9 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5470, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin10 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5474, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin11 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5478, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin12 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x547C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin13 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5480, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin14 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5544, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin15 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5547, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin16 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5549, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin17 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x554D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin18 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5551, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin19 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5555, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin20 ), Utility.Random( 15000,50000 ), Utility.Random( 1,3 ), 0x5451, 0 ) ); }				
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( DeadBodyEWDeed ), 7345, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( DeadBodyNSDeed ), 7345, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( EvilFireplaceSouthFaceAddonDeed ), 66800, Utility.Random( 1,3 ), 0x14EF, 1175 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( EvilFireplaceEastFaceAddonDeed ), 66800, Utility.Random( 1,3 ), 0x14EF, 1175 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_coffin_eastAddonDeed ), 9470, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_coffin_southAddonDeed ), 9470, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_block_southAddonDeed ), 9430, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_block_eastAddonDeed ), 9430, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( LargeDyingPlant ), 225, Utility.Random( 1,3 ), 0x42B9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( DyingPlant ), 175, Utility.Random( 1,3 ), 0x42BA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( PumpkinScarecrow ), 2240, Utility.Random( 1,3 ), 0x469B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GrimWarning ), 3120, Utility.Random( 1,3 ), 0x42BD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( SkullsOnPike ), 3120, Utility.Random( 1,3 ), 0x42B5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BlackCatStatue ), 3100, Utility.Random( 1,3 ), 0x4688, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( RuinedTapestry ), 15135, Utility.Random( 1,3 ), 0x4699, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenWeb ), 5185, Utility.Random( 1,3 ), 0xEE3, Utility.RandomList( 43, 1175, 1151 ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_shackles ), 125, Utility.Random( 1,3 ), 5696, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_ruined_bookcase ), 5340, Utility.Random( 1,3 ), 0x0C14, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_covered_chair ), 520, Utility.Random( 1,3 ), 3095, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_HauntedMirror1 ), 3270, Utility.Random( 1,3 ), 10875, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_HauntedMirror2 ), 3270, Utility.Random( 1,3 ), 10876, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_devil_face ), 350150, Utility.Random( 1,3 ), 4348, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){ Add( new GenericBuyInfo( typeof( PackedCostume ), 2530, Utility.Random( 1,3 ), 0x46A3, 0x5E0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){ Add( new GenericBuyInfo( typeof( WrappedCandy ), 120, Utility.Random( 1,3 ), 0x469E, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){ Add( new GenericBuyInfo( typeof( HalloweenPack ), 2530, Utility.Random( 1,3 ), 0x46A3, 0x5E0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NecromancerTable ), 15520, Utility.Random( 1,3 ), 0x149D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NecromancerBanner ), 15350, Utility.Random( 1,3 ), 0x149B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BurningScarecrowA ), 5290, Utility.Random( 1,3 ), 0x23A9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GothicCandelabraA ), 5280, Utility.Random( 1,3 ), 0x052D, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Shop info for Necro Guild vendor.
	/// Sells necrotic reagents, spellbooks, scrolls, tools, body parts, and dark artifacts for guild members.
	/// </summary>
	public class SBNecroGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBNecroGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BatWing ), 3, Utility.Random( 250,1000 ), 0xF78, 0 ) );
				Add( new GenericBuyInfo( typeof( DaemonBlood ), 6, Utility.Random( 250,1000 ), 0xF7D, 0 ) );
				Add( new GenericBuyInfo( typeof( PigIron ), 5, Utility.Random( 250,1000 ), 0xF8A, 0 ) );
				Add( new GenericBuyInfo( typeof( NoxCrystal ), 6, Utility.Random( 250,1000 ), 0xF8E, 0 ) );
				Add( new GenericBuyInfo( typeof( GraveDust ), 3, Utility.Random( 250,1000 ), 0xF8F, 0 ) );
				Add( new GenericBuyInfo( typeof( BloodOathScroll ), 25, Utility.Random( 50,200 ), 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( CorpseSkinScroll ), 28, Utility.Random( 50,200 ), 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( CurseWeaponScroll ), 12, Utility.Random( 50,200 ), 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( PolishBoneBrush ), 12, 10, 0x1371, 0 ) );
				Add( new GenericBuyInfo( typeof( GraveShovel ), 12, Utility.Random( 50,200 ), 0xF39, 0x966 ) );
				Add( new GenericBuyInfo( typeof( SurgeonsKnife ), 14, Utility.Random( 50,200 ), 0xEC4, 0x1B0 ) );
				Add( new GenericBuyInfo( typeof( MixingCauldron ), 247, Utility.Random( 50,200 ), 0x269C, 0 ) );
				Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 50,200 ), 0x1E27, 0x979 ) );
				Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 15,30 ), 0x10B4, 0 ) );
				Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 50, Utility.Random( 50,200 ), 0x2253, 0x4AA ) );
				Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 115, Utility.Random( 50,200 ), 0x2253, 0 ) );
				Add( new GenericBuyInfo( typeof( TarotDeck ), 5, Utility.Random( 50,200 ), 0x12AB, 0 ) ); 
				Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) );
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BlackDyeTub ), 5000, Utility.Random( 1,1 ), 0xFAB, 0x1 ) ); }
				Add( new GenericBuyInfo( typeof( reagents_magic_jar2 ), 1500, Utility.Random( 250,1000 ), 0x1007, 0xB97 ) );
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HellsGateScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x54F ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ManaLeechScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0xB87 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NecroCurePoisonScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x8A2 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NecroPoisonScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x4F8 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NecroUnlockScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x493 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( PhantasmScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x6DE ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( RetchedAirScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0xA97 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( SpectreShadowScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x17E ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( UndeadEyesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x491 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( VampireGiftScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0xB85 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( WallOfSpikesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0xB8F ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BloodPactScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x5B5 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GhostlyImagesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0xBF ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GhostPhaseScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x47E ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GraveyardGatewayScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x2EA ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HellsBrandScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x54C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ReaperHood ), 28, Utility.Random( 1,10 ), 0x4CDB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ReaperCowl ), 28, Utility.Random( 1,10 ), 0x4CDD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( DeadMask ), 28, Utility.Random( 1,10 ), 0x405, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NecromancerRobe ), 50, Utility.Random( 1,10 ), 0x2FBA, 0 ) ); }
				Add( new GenericBuyInfo( typeof( WizardStaff ), 40, Utility.Random( 1,5 ), 0x0908, 0xB3A ) );
				Add( new GenericBuyInfo( typeof( WizardStick ), 38, Utility.Random( 1,5 ), 0xDF2, 0xB3A ) );
				Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) );
				Add( new GenericBuyInfo( typeof( CorruptedMoonStone ), 1000, 5, 0xF8B, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BatWing ), 1 );
				Add( typeof( DaemonBlood ), 3 );
				Add( typeof( PigIron ), 2 );
				Add( typeof( NoxCrystal ), 3 );
				Add( typeof( GraveDust ), 1 );
				Add( typeof( NecromancerSpellbook ), 55 );
				Add( typeof( DeathKnightSpellbook ), Utility.Random( 100,300 ) );
				Add( typeof( ExorcismScroll ), 1 );
				Add( typeof( AnimateDeadScroll ), 26 );
				Add( typeof( BloodOathScroll ), 26 );
				Add( typeof( CorpseSkinScroll ), 26 );
				Add( typeof( CurseWeaponScroll ), 26 );
				Add( typeof( EvilOmenScroll ), 26 );
				Add( typeof( PainSpikeScroll ), 26 );
				Add( typeof( SummonFamiliarScroll ), 26 );
				Add( typeof( HorrificBeastScroll ), 27 );
				Add( typeof( MindRotScroll ), 39 );
				Add( typeof( PoisonStrikeScroll ), 39 );
				Add( typeof( WraithFormScroll ), 51 );
				Add( typeof( LichFormScroll ), 64 );
				Add( typeof( StrangleScroll ), 64 );
				Add( typeof( WitherScroll ), 64 );
				Add( typeof( VampiricEmbraceScroll ), 101 );
				Add( typeof( VengefulSpiritScroll ), 114 );
				Add( typeof( MixingCauldron ), 123 ); 
				Add( typeof( MixingSpoon ), 17 ); 
				Add( typeof( MyNecromancerSpellbook ), 500 );
				Add( typeof( BlackDyeTub ), 2500 ); 
				Add( typeof( PolishBoneBrush ), 6 );
				Add( typeof( PolishedSkull ), 3 );
				Add( typeof( PolishedBone ), 3 );
				Add( typeof( BoneContainer ), Utility.RandomMinMax( 100, 400 ) );
				Add( typeof( CorpseSailor ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BodyPart ), Utility.RandomMinMax( 30, 90 ) );
				Add( typeof( CorpseChest ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BuriedBody ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( LeftLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( TastyHeart ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( Head ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( LeftArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Torso ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bone ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RibCage ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( BonePile ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bones ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( GraveChest ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( SkullMinotaur ), Utility.Random( 50,150 ) );
				Add( typeof( SkullWyrm ), Utility.Random( 200,400 ) );
				Add( typeof( SkullGreatDragon ), Utility.Random( 300,600 ) );
				Add( typeof( SkullDragon ), Utility.Random( 100,300 ) );
				Add( typeof( SkullDemon ), Utility.Random( 100,300 ) );
				Add( typeof( SkullGiant ), Utility.Random( 100,300 ) );
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
				Add( typeof( WoodenCoffin ), 25 );
				Add( typeof( WoodenCasket ), 25 );
				Add( typeof( StoneCoffin ), 45 );
				Add( typeof( StoneCasket ), 45 );
				Add( typeof( DemonPrison ), Utility.Random( 500,1000 ) );
				Add( typeof( ReaperHood ), 11 );
				Add( typeof( ReaperCowl ), 11 );
				Add( typeof( DeadMask ), 11 );
				Add( typeof( NecromancerRobe ), 19 );
				if ( MyServerSettings.BuyChance() ){Add( typeof( DracolichSkull ), Utility.Random( 500,1000 ) ); } 
				Add( typeof( WizardStaff ), 20 );
				Add( typeof( WizardStick ), 19 );
				Add( typeof( MageEye ), 1 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBArcherGuild: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBArcherGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GuildFletching ), 500, Utility.Random( 1,5 ), 0x1EB8, 0x430 ) );
				Add( new GenericBuyInfo( typeof( ArcherQuiver ), 32, Utility.Random( 1,5 ), 0x2B02, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyArrows100 ), 300, Utility.Random( 1,10 ), 0xF41, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyBolts100 ), 300, Utility.Random( 1,10 ), 0x1BFD, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyArrows1000 ), 3000, Utility.Random( 1,10 ), 0xF41, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyBolts1000 ), 3000, Utility.Random( 1,10 ), 0x1BFD, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( ArcherQuiver ), 16 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBAlchemistGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBAlchemistGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{  
				Add( new GenericBuyInfo( typeof( MortarPestle ), 8, Utility.Random( 50,200 ), 0x4CE9, 0 ) );
				Add( new GenericBuyInfo( typeof( MixingCauldron ), 247, Utility.Random( 50,200 ), 0x269C, 0 ) );
				Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 50,200 ), 0x1E27, 0x979 ) );
				Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 50, Utility.Random( 50,200 ), 0x2253, 0x4AA ) );
				Add( new GenericBuyInfo( typeof( AlchemicalElixirs ), 50, Utility.Random( 1,100 ), 0x2219, 0 ) );
				Add( new GenericBuyInfo( typeof( AlchemicalMixtures ), 50, Utility.Random( 1,100 ), 0x2223, 0 ) );
				Add( new GenericBuyInfo( typeof( BookOfPoisons ), 50, Utility.Random( 1,100 ), 0x2253, 0x4F8 ) );

				Add( new GenericBuyInfo( typeof( Bottle ), 5, Utility.Random( 50,200 ), 0xF0E, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 15,30 ), 0x10B4, 0 ) );
				Add( new GenericBuyInfo( typeof( HeatingStand ), 2, Utility.Random( 1,100 ), 0x1849, 0 ) ); 

				Add( new GenericBuyInfo( typeof( BlackPearl ), 5, Utility.Random( 250,1000 ), 0x266F, 0 ) );
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, Utility.Random( 250,1000 ), 0xF7B, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 250,1000 ), 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 250,1000 ), 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, Utility.Random( 250,1000 ), 0xF86, 0 ) );
				Add( new GenericBuyInfo( typeof( Nightshade ), 3, Utility.Random( 250,1000 ), 0xF88, 0 ) );
				Add( new GenericBuyInfo( typeof( SpidersSilk ), 3, Utility.Random( 250,1000 ), 0xF8D, 0 ) );
				Add( new GenericBuyInfo( typeof( SulfurousAsh ), 3, Utility.Random( 250,1000 ), 0xF8C, 0 ) );

				Add( new GenericBuyInfo( typeof( Brimstone ), 6, Utility.Random( 250,1000 ), 0x2FD3, 0 ) );
				Add( new GenericBuyInfo( typeof( ButterflyWings ), 6, Utility.Random( 250,1000 ), 0x3002, 0 ) );
				Add( new GenericBuyInfo( typeof( EyeOfToad ), 6, Utility.Random( 250,1000 ), 0x2FDA, 0 ) );
				Add( new GenericBuyInfo( typeof( FairyEgg ), 6, Utility.Random( 250,1000 ), 0x2FDB, 0 ) );
				Add( new GenericBuyInfo( typeof( GargoyleEar ), 6, Utility.Random( 250,1000 ), 0x2FD9, 0 ) );
				Add( new GenericBuyInfo( typeof( BeetleShell ), 6, Utility.Random( 250,1000 ), 0x2FF8, 0 ) );
				Add( new GenericBuyInfo( typeof( MoonCrystal ), 6, Utility.Random( 250,1000 ), 0x3003, 0 ) );
				Add( new GenericBuyInfo( typeof( PixieSkull ), 6, Utility.Random( 250,1000 ), 0x2FE1, 0 ) );
				Add( new GenericBuyInfo( typeof( RedLotus ), 6, Utility.Random( 250,1000 ), 0x2FE8, 0 ) );
				Add( new GenericBuyInfo( typeof( SeaSalt ), 6, Utility.Random( 250,1000 ), 0x2FE9, 0 ) );
				Add( new GenericBuyInfo( typeof( SilverWidow ), 6, Utility.Random( 250,1000 ), 0x2FF7, 0 ) );
				Add( new GenericBuyInfo( typeof( SwampBerries ), 6, Utility.Random( 250,1000 ), 0x2FE0, 0 ) );

				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( reagents_magic_jar1 ), 2000, Utility.Random( 50,200 ), 0x1007, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( reagents_magic_jar2 ), 1500, Utility.Random( 50,200 ), 0x1007, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( reagents_magic_jar3 ), 5000, Utility.Random( 250,1000 ), 0x1007, 0x488 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BottleOfAcid ), 600, Utility.Random( 3,21 ), 0x180F, 1167 ) ); }


				Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 10,25 ), 0xF0B, 0 ) );
				Add( new GenericBuyInfo( typeof( AgilityPotion ), 100, Utility.Random( 10,25 ), 0xF08, 0 ) );
				Add( new GenericBuyInfo( typeof( NightSightPotion ), 100, Utility.Random( 10,25 ), 0xF06, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 10,25 ), 0x25FD, 0 ) );
				Add( new GenericBuyInfo( typeof( StrengthPotion ), 100, Utility.Random( 10,25 ), 0xF09, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserPoisonPotion ), 100, Utility.Random( 10,25 ), 0x2600, 0 ) );
 				Add( new GenericBuyInfo( typeof( LesserCurePotion ), 100, Utility.Random( 10,25 ), 0x233B, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserExplosionPotion ), 21, Utility.Random( 10,25 ), 0x2407, 0 ) );

				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HealPotion ), 30, Utility.Random( 1,100 ), 0xF0C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( PoisonPotion ), 30, Utility.Random( 1,100 ), 0xF0A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CurePotion ), 30, Utility.Random( 1,100 ), 0xF07, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ExplosionPotion ), 42, Utility.Random( 1,100 ), 0xF0D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ConflagrationPotion ), 30, Utility.Random( 1,100 ), 0x180F, 0xAD8 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ConfusionBlastPotion ), 30, Utility.Random( 1,100 ), 0x180F, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( FrostbitePotion ), 30, Utility.Random( 1,100 ), 0x180F, 0xAF3 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( TotalRefreshPotion ), 30, Utility.Random( 1,100 ), 0x25FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterAgilityPotion ), 60, Utility.Random( 1,100 ), 0x256A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterConflagrationPotion ), 60, Utility.Random( 1,100 ), 0x2406, 0xAD8 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterConfusionBlastPotion ), 60, Utility.Random( 1,100 ), 0x2406, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterCurePotion ), 60, Utility.Random( 1,100 ), 0x24EA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterExplosionPotion ), 60, Utility.Random( 1,100 ), 0x2408, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterFrostbitePotion ), 60, Utility.Random( 1,100 ), 0x2406, 0xAF3 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterHealPotion ), 60, Utility.Random( 1,100 ), 0x25FE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterPoisonPotion ), 60, Utility.Random( 1,100 ), 0x2601, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterStrengthPotion ), 60, Utility.Random( 1,100 ), 0x25F7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( DeadlyPoisonPotion ), 60, Utility.Random( 1,100 ), 0x2669, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( LesserInvisibilityPotion ), 860, Utility.Random( 1,3 ), 0x23BD, 0x490 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( LesserManaPotion ), 860, Utility.Random( 1,3 ), 0x23BD, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( LesserRejuvenatePotion ), 860, Utility.Random( 1,3 ), 0x23BD, 0x48E ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( InvisibilityPotion ), 890, Utility.Random( 1,3 ), 0x180F, 0x490 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ManaPotion ), 890, Utility.Random( 1,3 ), 0x180F, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( RejuvenatePotion ), 890, Utility.Random( 1,3 ), 0x180F, 0x48E ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterInvisibilityPotion ), 8120, 1, 0x2406, 0x490 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterManaPotion ), 8120, 1, 0x2406, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterRejuvenatePotion ), 8120, 1, 0x2406, 0x48E ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( InvulnerabilityPotion ), 8300, 1, 0x180F, 0x48F ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( AutoResPotion ), 8600, 1, 0x0E0F, 0x494 ) ); }
				Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( SkullMinotaur ), Utility.Random( 50,150 ) );
				Add( typeof( SkullWyrm ), Utility.Random( 200,400 ) );
				Add( typeof( SkullGreatDragon ), Utility.Random( 300,600 ) );
				Add( typeof( SkullDragon ), Utility.Random( 100,300 ) );
				Add( typeof( SkullDemon ), Utility.Random( 100,300 ) );
				Add( typeof( SkullGiant ), Utility.Random( 100,300 ) );
				Add( typeof( CanopicJar ), Utility.Random( 50,300 ) );
				Add( typeof( MixingCauldron ), 123 );
				Add( typeof( MixingSpoon ), 17 );
				Add( typeof( DragonTooth ), 120 );
				Add( typeof( EnchantedSeaweed ), 120 );
				Add( typeof( GhostlyDust ), 120 );
				Add( typeof( GoldenSerpentVenom ), 120 );
				Add( typeof( LichDust ), 120 );
				Add( typeof( SilverSerpentVenom ), 120 );
				Add( typeof( UnicornHorn ), 120 );
				Add( typeof( DemigodBlood ), 120 );
				Add( typeof( DemonClaw ), 120 );
				Add( typeof( DragonBlood ), 120 );
				Add( typeof( BlackPearl ), 3 );
				Add( typeof( Bloodmoss ), 3 );
				Add( typeof( MandrakeRoot ), 2 );
				Add( typeof( Garlic ), 2 );
				Add( typeof( Ginseng ), 2 );
				Add( typeof( Nightshade ), 2 );
				Add( typeof( SpidersSilk ), 2 );
				Add( typeof( SulfurousAsh ), 1 );
				Add( typeof( Brimstone ), 3 );
				Add( typeof( ButterflyWings ), 3 );
				Add( typeof( EyeOfToad ), 3 );
				Add( typeof( FairyEgg ), 3 );
				Add( typeof( GargoyleEar ), 3 );
				Add( typeof( BeetleShell ), 3 );
				Add( typeof( MoonCrystal ), 3 );
				Add( typeof( PixieSkull ), 3 );
				Add( typeof( RedLotus ), 3 );
				Add( typeof( SeaSalt ), 3 );
				Add( typeof( SilverWidow ), 3 );
				Add( typeof( SwampBerries ), 3 );
				Add( typeof( Bottle ), 3 );
				Add( typeof( Jar ), 3 );
				Add( typeof( MortarPestle ), 4 );
				Add( typeof( AgilityPotion ), 7 );
				Add( typeof( AutoResPotion ), 94 );
				Add( typeof( BottleOfAcid ), 32 );
				Add( typeof( ConflagrationPotion ), 7 );
				Add( typeof( FrostbitePotion ), 7 );
				Add( typeof( ConfusionBlastPotion ), 7 );
				Add( typeof( CurePotion ), 14 );
				Add( typeof( DeadlyPoisonPotion ), 28 );
				Add( typeof( ExplosionPotion ), 14 );
				Add( typeof( GreaterAgilityPotion ), 28 );
				Add( typeof( GreaterConflagrationPotion ), 28 );
				Add( typeof( GreaterFrostbitePotion ), 28 );
				Add( typeof( GreaterConfusionBlastPotion ), 28 );
				Add( typeof( GreaterCurePotion ), 28 );
				Add( typeof( GreaterExplosionPotion ), 28 );
				Add( typeof( GreaterHealPotion ), 28 );
				Add( typeof( GreaterInvisibilityPotion ), 28 );
				Add( typeof( GreaterManaPotion ), 28 );
				Add( typeof( GreaterPoisonPotion ), 28 );
				Add( typeof( GreaterRejuvenatePotion ), 28 );
				Add( typeof( GreaterStrengthPotion ), 28 );
				Add( typeof( HealPotion ), 14 );
				Add( typeof( InvisibilityPotion ), 14 );
				Add( typeof( InvulnerabilityPotion ), 53 );
				Add( typeof( PotionOfWisdom ), Utility.Random( 250,500 ) );
				Add( typeof( PotionOfDexterity ), Utility.Random( 250,500 ) );
				Add( typeof( PotionOfMight ), Utility.Random( 250,500 ) );
				Add( typeof( LesserCurePotion ), 7 );
				Add( typeof( LesserExplosionPotion ), 7 );
				Add( typeof( LesserHealPotion ), 7 );
				Add( typeof( LesserInvisibilityPotion ), 7 );
				Add( typeof( LesserManaPotion ), 7 );
				Add( typeof( LesserPoisonPotion ), 7 );
				Add( typeof( LesserRejuvenatePotion ), 7 );
				Add( typeof( ManaPotion ), 14 );
				Add( typeof( NightSightPotion ), 14 );
				Add( typeof( PoisonPotion ), 14 );
				Add( typeof( RefreshPotion ), 14 );
				Add( typeof( RejuvenatePotion ), 28 );
				Add( typeof( StrengthPotion ), 14 );
				Add( typeof( TotalRefreshPotion ), 28 );
				Add( typeof( BottleOfParts ), Utility.Random( 10, 30 ) );
				Add( typeof( SpecialSeaweed ), Utility.Random( 20, 40 ) );
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBLibraryGuild: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBLibraryGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GuildScribe ), 500, Utility.Random( 1,5 ), 0x2051, 0x430 ) );
				Add( new GenericBuyInfo( typeof( LoreGuidetoAdventure ), 5, Utility.Random( 5,100 ), 0x1C11, 0 ) );
				Add( new GenericBuyInfo( typeof( WeaponAbilityBook ), 5, Utility.Random( 1,100 ), 0x2254, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnLeatherBook ), 5, Utility.Random( 1,100 ), 0x4C60, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnMiscBook ), 5, Utility.Random( 1,100 ), 0x4C5D, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnMetalBook ), 5, Utility.Random( 1,100 ), 0x4C5B, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnWoodBook ), 5, Utility.Random( 1,100 ), 0x4C5E, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnReagentsBook ), 5, Utility.Random( 1,100 ), 0x4C5E, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnTailorBook ), 5, Utility.Random( 1,100 ), 0x4C5E, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnGraniteBook ), 5, Utility.Random( 1,100 ), 0x4C5C, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnScalesBook ), 5, Utility.Random( 1,100 ), 0x4C60, 0 ) );
				Add( new GenericBuyInfo( typeof( CBookDruidicHerbalism ), 50, Utility.Random( 1,100 ), 0x2D50, 0 ) );
				Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 50, Utility.Random( 1,100 ), 0x2253, 0x4AA ) );
				Add( new GenericBuyInfo( typeof( AlchemicalElixirs ), 50, Utility.Random( 1,100 ), 0x2219, 0 ) );
				Add( new GenericBuyInfo( typeof( AlchemicalMixtures ), 50, Utility.Random( 1,100 ), 0x2223, 0 ) );
				Add( new GenericBuyInfo( typeof( BookOfPoisons ), 50, Utility.Random( 1,100 ), 0x2253, 0x4F8 ) );
				Add( new GenericBuyInfo( typeof( WorkShoppes ), 50, Utility.Random( 1,100 ), 0x2259, 0xB50 ) );
				Add( new GenericBuyInfo( typeof( LearnTitles ), 5, Utility.Random( 1,100 ), 0xFF2, 0 ) );
				Add( new GenericBuyInfo( typeof( ScribesPen ), 8, Utility.Random( 50,200 ), 0x2051, 0 ) );
				Add( new GenericBuyInfo( typeof( BlankScroll ), 5, Utility.Random( 250,1000 ), 0x0E34, 0 ) );
				Add( new GenericBuyInfo( typeof( Monocle ), 24, Utility.Random( 3,30 ), 0x2C84, 0 ) );
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( "1041267", typeof( Runebook ), 3500, Utility.Random( 1,3 ), 0x0F3D, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( ScribesPen ), 4 );
				Add( typeof( BlankScroll ), 3 );
				Add( typeof( Monocle ), 12 );
				Add( typeof( DynamicBook ), Utility.Random( 10,150 ) );
				Add( typeof( TomeOfWands ), Utility.Random( 100,400 ) );
				Add( typeof( JokeBook ), Utility.Random( 750,1500 ) );
				Add( typeof( DataPad ), Utility.Random( 5, 150 ) );
				Add( typeof( NecromancerSpellbook ), 55 );
				Add( typeof( BookOfBushido ), 70 );
				Add( typeof( BookOfNinjitsu ), 70 );
				Add( typeof( MysticSpellbook ), 70 );
				Add( typeof( DeathKnightSpellbook ), Utility.Random( 100,300 ) );
				Add( typeof( Runebook ), Utility.Random( 100,350 ) );
				Add( typeof( BookOfChivalry ), 70 );
				Add( typeof( BookOfChivalry ), 70 );
				Add( typeof( HolyManSpellbook ), Utility.Random( 50,200 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBDruidGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBDruidGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Bandage ), 2, Utility.Random( 250,1000 ), 0xE21, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 50,200 ), 0x25FD, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 50,200 ), 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 50,200 ), 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 50,200 ), 0xF0B, 0 ) );
				Add( new GenericBuyInfo( typeof( GardenTool ), 12, Utility.Random( 50,200 ), 0xDFD, 0x84F ) );
				Add( new GenericBuyInfo( typeof( HerbalistCauldron ), 247, Utility.Random( 50,200 ), 0x2676, 0 ) );
				Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 50,200 ), 0x1E27, 0x979 ) );
				Add( new GenericBuyInfo( typeof( CBookDruidicHerbalism ), 50, Utility.Random( 1,100 ), 0x2D50, 0 ) );
				Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) );
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( AppleTreeDeed ), 10640, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CherryBlossomTreeDeed ), 10540, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( DarkBrownTreeDeed ), 10540, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreyTreeDeed ), 10540, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( LightBrownTreeDeed ), 10540, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( PeachTreeDeed ), 10640, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( PearTreeDeed ), 10640, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( VinePatchAddonDeed ), 10400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HopsPatchDeed ), 10400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( LesserHealPotion ), 7 );
				Add( typeof( RefreshPotion ), 7 );
				Add( typeof( Garlic ), 2 );
				Add( typeof( Ginseng ), 2 );
				Add( typeof( GardenTool ), 6 );
				Add( typeof( HerbalistCauldron ), 123 );
				Add( typeof( MixingSpoon ), 17 );
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
				Add( typeof( FirstAidKit ), Utility.Random( 100,250 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBCarpenterGuild: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBCarpenterGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Hatchet ), 20, Utility.Random( 1,100 ), 0xF44, 0 ) );
				Add( new GenericBuyInfo( typeof( LumberAxe ), 22, Utility.Random( 1,100 ), 0xF43, 0x96D ) );
				Add( new GenericBuyInfo( typeof( GuildCarpentry ), 500, Utility.Random( 1,5 ), 0x1EBA, 0x430 ) );
				Add( new GenericBuyInfo( typeof( Nails ), 3, Utility.Random( 50,200 ), 0x102E, 0 ) );
				Add( new GenericBuyInfo( typeof( Axle ), 2, Utility.Random( 50,200 ), 0x105B, 0 ) );
				Add( new GenericBuyInfo( typeof( Board ), 3, Utility.Random( 50,200 ), 0x1BD7, 0 ) );
				Add( new GenericBuyInfo( typeof( DrawKnife ), 10, Utility.Random( 50,200 ), 0x10E4, 0 ) );
				Add( new GenericBuyInfo( typeof( Froe ), 10, Utility.Random( 50,200 ), 0x10E5, 0 ) );
				Add( new GenericBuyInfo( typeof( Scorp ), 10, Utility.Random( 50,200 ), 0x10E7, 0 ) );
				Add( new GenericBuyInfo( typeof( Inshave ), 10, Utility.Random( 50,200 ), 0x10E6, 0 ) );
				Add( new GenericBuyInfo( typeof( DovetailSaw ), 12, Utility.Random( 50,200 ), 0x1028, 0 ) );
				Add( new GenericBuyInfo( typeof( Saw ), 100, Utility.Random( 50,200 ), 0x1034, 0 ) );
				Add( new GenericBuyInfo( typeof( Hammer ), 17, Utility.Random( 50,200 ), 0x102A, 0 ) );
				Add( new GenericBuyInfo( typeof( MouldingPlane ), 11, Utility.Random( 50,200 ), 0x102C, 0 ) );
				Add( new GenericBuyInfo( typeof( SmoothingPlane ), 10, Utility.Random( 50,200 ), 0x1032, 0 ) );
				Add( new GenericBuyInfo( typeof( JointingPlane ), 11, Utility.Random( 50,200 ), 0x1030, 0 ) );
				Add( new GenericBuyInfo( typeof( WoodworkingTools ), 10, Utility.Random( 10,50 ), 0x4F52, 0 ) );
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AdventurerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AlchemyCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F91, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ArmsCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9E, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BakerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F92, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BeekeeperCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F95, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BlacksmithCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BowyerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F97, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ButcherCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F89, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( CarpenterCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8A, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( FletcherCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F88, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HealerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F98, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HugeCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F86, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JewelerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( LibrarianCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F96, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MusicianCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F94, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NecromancerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9A, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ProvisionerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8E, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SailorCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9C, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StableCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F87, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SupplyCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TailorCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TavernCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F99, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TinkerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F90, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasureCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F93, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( WizardryCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8C, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C43, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C45, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C47, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C89, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x38B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x38D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CCB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireI ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CCD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireJ ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D26, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BF1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C31, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C63, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CAD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CEF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C3B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C65, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C67, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CBF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C4B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C6B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C15, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C2B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C2D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C33, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C5F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C61, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C79, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CA5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfI ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CA7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfJ ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CAF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfK ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CEB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfL ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CED, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfM ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D05, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBowyerShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C29, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBowyerShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C5D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBowyerShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CA3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBowyerShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewCarpenterShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C6F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewCarpenterShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CD7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewCarpenterShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CFB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C51, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C53, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C75, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C77, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CDD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CDF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CFF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D01, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDarkBookShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BF9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDarkBookShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BFB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDarkShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BFD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C7F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C81, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C83, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C85, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C87, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersI ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CBB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersJ ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CBD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersK ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D0B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersL ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D20, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersM ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D22, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersN ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D24, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C27, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C5B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CA1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C1B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewHelmShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BFF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewHunterShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C4D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewKitchenShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C19, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewKitchenShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C39, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewOldBookShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x19FF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewPotionShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewRuinedBookShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0xC14, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C35, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C3D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C69, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C7B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D07, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShoeShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C37, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShoeShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C7D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShoeShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShoeShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D09, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSorcererShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C4F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSorcererShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C73, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSorcererShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CDB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSorcererShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CFD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSupplyShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C57, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSupplyShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C9D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSupplyShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTailorShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C3F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTailorShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C6D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTailorShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTailorShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTannerShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C23, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTannerShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C49, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTavernShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C25, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTavernShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C59, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTavernShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C9F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTavernShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTinkerShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C71, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTinkerShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CD9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTinkerShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D03, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTortureShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C2F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C17, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C1D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C21, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C55, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C9B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE1, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Hatchet ), 100 );
				Add( typeof( LumberAxe ), 16 );
				Add( typeof( WoodenBox ), 7 );
				Add( typeof( SmallCrate ), 5 );
				Add( typeof( MediumCrate ), 6 );
				Add( typeof( LargeCrate ), 7 );
				Add( typeof( WoodenChest ), 100 );
				Add( typeof( LargeTable ), 10 );
				Add( typeof( Nightstand ), 7 );
				Add( typeof( YewWoodTable ), 10 );
				Add( typeof( Throne ), 24 );
				Add( typeof( WoodenThrone ), 6 );
				Add( typeof( Stool ), 6 );
				Add( typeof( FootStool ), 6 );
				Add( typeof( FancyWoodenChairCushion ), 12 );
				Add( typeof( WoodenChairCushion ), 10 );
				Add( typeof( WoodenChair ), 8 );
				Add( typeof( BambooChair ), 6 );
				Add( typeof( WoodenBench ), 6 );
				Add( typeof( Saw ), 9 );
				Add( typeof( Scorp ), 6 );
				Add( typeof( SmoothingPlane ), 6 );
				Add( typeof( DrawKnife ), 6 );
				Add( typeof( Froe ), 6 );
				Add( typeof( Hammer ), 14 );
				Add( typeof( Inshave ), 6 );
				Add( typeof( WoodworkingTools ), 6 );
				Add( typeof( JointingPlane ), 6 );
				Add( typeof( MouldingPlane ), 6 );
				Add( typeof( DovetailSaw ), 7 );
				Add( typeof( Axle ), 1 );
				Add( typeof( Club ), 13 );

				Add( typeof( Log ), 1 );
				Add( typeof( AshLog ), 2 );
				Add( typeof( CherryLog ), 2 );
				Add( typeof( EbonyLog ), 3 );
				Add( typeof( GoldenOakLog ), 3 );
				Add( typeof( HickoryLog ), 4 );
				/*Add( typeof( MahoganyLog ), 4 );
				Add( typeof( DriftwoodLog ), 4 );
				Add( typeof( OakLog ), 5 );
				Add( typeof( PineLog ), 5 );
				Add( typeof( GhostLog ), 5 );*/
				Add( typeof( RosewoodLog ), 6 );
				/*Add( typeof( WalnutLog ), 6 );*/
				Add( typeof( ElvenLog ), 12 );
				/*Add( typeof( PetrifiedLog ), 7 );*/

				Add( typeof( Board ), 2 );
				Add( typeof( AshBoard ), 3 );
				Add( typeof( CherryBoard ), 3 );
				Add( typeof( EbonyBoard ), 4 );
				Add( typeof( GoldenOakBoard ), 4 );
				Add( typeof( HickoryBoard ), 5 );
				/*Add( typeof( MahoganyBoard ), 5 );
				Add( typeof( DriftwoodBoard ), 5 );
				Add( typeof( OakBoard ), 6 );
				Add( typeof( PineBoard ), 6 );
				Add( typeof( GhostBoard ), 6 );*/
				Add( typeof( RosewoodBoard ), 7 );
				/*Add( typeof( WalnutBoard ), 7 );*/
				Add( typeof( ElvenBoard ), 14 );
				/*Add( typeof( PetrifiedBoard ), 8 );*/
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBAssassin : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBAssassin()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( LesserPoisonPotion ), 100, Utility.Random( 10,50 ), 0x2600, 0 ) );
				Add( new GenericBuyInfo( typeof( PoisonPotion ), 30, Utility.Random( 10,50 ), 0xF0A, 0 ) );
				Add( new GenericBuyInfo( typeof( GreaterPoisonPotion ), 60, Utility.Random( 10,50 ), 0x2601, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DeadlyPoisonPotion ), 120, Utility.Random( 1,100 ), 0x2669, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( LethalPoisonPotion ), 320, Utility.Random( 1,100 ), 0x266A, 0 ) ); }
				Add( new GenericBuyInfo( typeof( Nightshade ), 4, Utility.Random( 250,1000 ), 0xF88, 0 ) );
				Add( new GenericBuyInfo( typeof( Dagger ), 21, Utility.Random( 10,50 ), 0xF52, 0 ) );
				Add( new GenericBuyInfo( typeof( AssassinSpike ), 21, Utility.Random( 1,100 ), 0x2D21, 0 ) );
				Add( new GenericBuyInfo( "1041060", typeof( HairDye ), 100, Utility.Random( 1,100 ), 0xEFF, 0 ) );
				Add( new GenericBuyInfo( "hair dye bottle", typeof( HairDyeBottle ), 1000, Utility.Random( 1,100 ), 0xE0F, 0 ) );
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( DisguiseKit ), 700, Utility.Random( 1,5 ), 0xE05, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( AssassinRobe ), 38, Utility.Random( 1,10 ), 0x2B69, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( AssassinShroud ), 50, Utility.Random( 1,10 ), 0x2FB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ReaperHood ), 28, Utility.Random( 1,10 ), 0x4CDB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ReaperCowl ), 28, Utility.Random( 1,10 ), 0x4CDD, 0 ) ); }
				Add( new GenericBuyInfo( typeof( BookOfPoisons ), 50, Utility.Random( 1,100 ), 0x2253, 0x4F8 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( LesserPoisonPotion ), 7 );
				Add( typeof( PoisonPotion ), 14 );
				Add( typeof( GreaterPoisonPotion ), 28 );
				Add( typeof( DeadlyPoisonPotion ), 56 );
				Add( typeof( LethalPoisonPotion ), 128 );
				Add( typeof( Nightshade ), 2 );
				Add( typeof( Dagger ), 10 );
				Add( typeof( HairDye ), 50 );
				Add( typeof( HairDyeBottle ), 300 );
				Add( typeof( SilverSerpentVenom ), 140 );
				Add( typeof( GoldenSerpentVenom ), 210 );
				Add( typeof( AssassinSpike ), 10 );
				Add( typeof( AssassinRobe ), 19 );
				Add( typeof( AssassinShroud ), 29 );
				Add( typeof( ReaperHood ), 11 );
				Add( typeof( ReaperCowl ), 11 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBCartographer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBCartographer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BlankMap ), 5, Utility.Random( 3,31 ), 0x14EC, 0 ) );
				Add( new GenericBuyInfo( typeof( MapmakersPen ), 8, Utility.Random( 3,31 ), 0x2052, 0 ) );
				Add( new GenericBuyInfo( typeof( BlankScroll ), 12, Utility.Random( 30,310 ), 0xEF3, 0 ) );
				Add( new GenericBuyInfo( typeof( MasterSkeletonsKey ), Utility.Random( 250,1000 ), Utility.Random( 3,5 ), 0x410B, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BlankScroll ), 6 );
				Add( typeof( MapmakersPen ), 4 );
				Add( typeof( BlankMap ), 2 );
				Add( typeof( CityMap ), 3 );
				Add( typeof( LocalMap ), 3 );
				Add( typeof( WorldMap ), 3 );
				Add( typeof( PresetMapEntry ), 3 );
				Add( typeof( WorldMapLodor ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapSosaria ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapBottle ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapSerpent ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapUmber ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapAmbrosia ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapIslesOfDread ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapSavage ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapUnderworld ), Utility.Random( 20,300 ) );
				Add( typeof( AlternateRealityMap ), Utility.Random( 500,1000 ) );
			}
		}
	}

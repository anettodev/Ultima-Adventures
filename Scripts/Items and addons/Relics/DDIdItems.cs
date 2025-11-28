using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Mobiles;

namespace Server.Misc
{
	/// <summary>
	/// Utility class for identifying and valuing relic items.
	/// Provides methods to determine gold value and buyer type for various relic items.
	/// </summary>
	class RelicItems
	{
		#region Constants

		private const int RELIC_VALUE_MIN = 80;
		private const int RELIC_VALUE_MAX = 500;
		private const int DYNAMIC_BOOK_VALUE_MIN = 20;
		private const int DYNAMIC_BOOK_VALUE_MAX = 100;
		private const double INSCRIBE_SKILL_MULTIPLIER = 0.01;

		#endregion

		#region Public Methods

		/// <summary>
		/// Identifies the value of a relic item and returns a message describing it
		/// </summary>
		/// <param name="examiner">The mobile examining the item</param>
		/// <param name="customer">The mobile who owns or is viewing the item</param>
		/// <param name="item">The relic item to identify</param>
		/// <returns>Message describing the item's value and potential buyer</returns>
		public static string IdentifyRelicValue(Mobile examiner, Mobile customer, Item item)
		{
			int gold = 0;
			string buyer = "";
			string phrase = "";

			// Determine gold value and buyer type based on item type
			GetRelicValueAndBuyer(item, out gold, out buyer);

			// Apply Inscribe skill bonus for books/tablets/scrolls
			if (item is DDRelicBook || item is DDRelicTablet || item is DDRelicScrolls)
			{
				gold = (int)(gold * (1 + (customer.Skills[SkillName.Inscribe].Value * INSCRIBE_SKILL_MULTIPLIER)));
			}

			// Build phrase based on examiner and customer relationship
			if (buyer != "" && examiner == customer)
			{
				phrase = "m is worth " + gold.ToString() + " gold to " + buyer + ".";
			}
			else if (buyer != "")
			{
				phrase = "You could give this to " + buyer + " for " + gold.ToString() + " gold.";
			}

			return phrase;
		}

		/// <summary>
		/// Identifies the value of an arms relic (armor/weapon) and returns a message describing it
		/// </summary>
		/// <param name="examiner">The mobile examining the item</param>
		/// <param name="customer">The mobile who owns or is viewing the item</param>
		/// <param name="item">The arms relic item to identify</param>
		/// <returns>Message describing the item's value and potential buyer</returns>
		public static string IdentifyArmsRelicValue(Mobile examiner, Mobile customer, Item item)
		{
			int gold = 0;
			string buyer = "";
			string phrase = "";

			if (item is DDRelicArmor)
			{
				gold = ((DDRelicArmor)item).RelicGoldValue;
				buyer = "an armorer";
			}
			else if (item is DDRelicWeapon)
			{
				gold = ((DDRelicWeapon)item).RelicGoldValue;
				buyer = "a weaponsmith";
			}

			// Build phrase based on examiner and customer relationship
			if (buyer != "" && examiner == customer)
			{
				phrase = "m is worth " + gold.ToString() + " gold to " + buyer + ".";
			}
			else if (buyer != "")
			{
				phrase = "You could give this to " + buyer + " for " + gold.ToString() + " gold.";
			}

			return phrase;
		}

		/// <summary>
		/// Checks if an item is a relic item type
		/// </summary>
		/// <param name="item">The item to check</param>
		/// <returns>True if the item is a relic type, false otherwise</returns>
		public static bool IsRelicItem(Item item)
		{
			return item is DDRelicAlchemy
				|| item is DDRelicArmor
				|| item is DDRelicArts
				|| item is DDRelicBanner
				|| item is DDRelicBearRugsAddon
				|| item is DDRelicBook
				|| item is DDRelicClock1
				|| item is DDRelicClock2
				|| item is DDRelicClock3
				|| item is DDRelicCloth
				|| item is DDRelicCoins
				|| item is DDRelicDrink
				|| item is DDRelicFur
				|| item is DDRelicGem
				|| item is DDRelicInstrument
				|| item is DDRelicJewels
				|| item is DDRelicLight1
				|| item is DDRelicLight2
				|| item is DDRelicLight3
				|| item is DDRelicOrbs
				|| item is DDRelicPainting
				|| item is DDRelicReagent
				|| item is DDRelicRugAddonDeed
				|| item is DDRelicScrolls
				|| item is DDRelicStatue
				|| item is DDRelicTablet
				|| item is DDRelicVase
				|| item is DDRelicWeapon
				|| item is DDRelicLeather
				|| item is DDRelicGrave
				|| item is HighSeasRelic
				|| item is EmptyCanopicJar
				|| item is StatueGygaxAddonDeed;
		}

		/// <summary>
		/// Generates a random relic value
		/// </summary>
		/// <returns>Random gold value between 80-500</returns>
		public static int RelicValue()
		{
			return Utility.RandomMinMax(RELIC_VALUE_MIN, RELIC_VALUE_MAX);
		}

		/// <summary>
		/// Gets the relic value for a specific item when sold to a specific mobile
		/// Takes into account buyer type, henchman status, and special bonuses
		/// </summary>
		/// <param name="relics">The relic item</param>
		/// <param name="m">The mobile buying the relic</param>
		/// <returns>Gold value the mobile will pay for the relic</returns>
		public static int RelicValue(Item relics, Mobile m)
		{
			int relicValue = 0;
			bool isHenchman = IsHenchman(m);

			// Check each relic type and buyer combination
			relicValue = GetRelicValueForBuyer(relics, m, isHenchman);

			// Apply Inscribe skill bonus for books/tablets/scrolls
			if (relics is DDRelicBook || relics is DDRelicTablet || relics is DDRelicScrolls)
			{
				relicValue = (int)(relicValue * (1 + (m.Skills[SkillName.Inscribe].Value * INSCRIBE_SKILL_MULTIPLIER)));
			}

			return relicValue;
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Gets the relic value and buyer type for a given item
		/// </summary>
		/// <param name="item">The relic item</param>
		/// <param name="gold">Output parameter for gold value</param>
		/// <param name="buyer">Output parameter for buyer type</param>
		private static void GetRelicValueAndBuyer(Item item, out int gold, out string buyer)
		{
			gold = 0;
			buyer = "";

			if (item is DDRelicInstrument)
			{
				gold = ((DDRelicInstrument)item).RelicGoldValue;
				buyer = "a bard";
			}
			else if (item is DDRelicAlchemy)
			{
				gold = ((DDRelicAlchemy)item).RelicGoldValue;
				buyer = "an alchemist";
			}
			else if (item is DDRelicArts)
			{
				gold = ((DDRelicArts)item).RelicGoldValue;
				buyer = "an art collector";
			}
			else if (item is DDRelicBanner)
			{
				gold = ((DDRelicBanner)item).RelicGoldValue;
				buyer = "an art collector";
			}
			else if (item is DDRelicBearRugsAddon)
			{
				gold = ((DDRelicBearRugsAddon)item).RelicGoldValue;
				buyer = "a fur trader";
			}
			else if (item is DDRelicBook)
			{
				gold = ((DDRelicBook)item).RelicGoldValue;
				buyer = "a scribe";
			}
			else if (item is DDRelicClock1)
			{
				gold = ((DDRelicClock1)item).RelicGoldValue;
				buyer = "a tinker";
			}
			else if (item is DDRelicClock2)
			{
				gold = ((DDRelicClock2)item).RelicGoldValue;
				buyer = "a tinker";
			}
			else if (item is DDRelicClock3)
			{
				gold = ((DDRelicClock3)item).RelicGoldValue;
				buyer = "a tinker";
			}
			else if (item is DDRelicCloth)
			{
				gold = ((DDRelicCloth)item).RelicGoldValue;
				buyer = "a tailor";
			}
			else if (item is DDRelicCoins)
			{
				gold = ((DDRelicCoins)item).RelicGoldValue;
				buyer = "a minter";
			}
			else if (item is DDRelicDrink)
			{
				gold = ((DDRelicDrink)item).RelicGoldValue;
				buyer = "a tavern keeper";
			}
			else if (item is DDRelicFur)
			{
				gold = ((DDRelicFur)item).RelicGoldValue;
				buyer = "a fur trader";
			}
			else if (item is DDRelicGem)
			{
				gold = ((DDRelicGem)item).RelicGoldValue;
				buyer = "a jeweler";
			}
			else if (item is DDRelicJewels)
			{
				gold = ((DDRelicJewels)item).RelicGoldValue;
				buyer = "a jeweler";
			}
			else if (item is DDRelicLight1)
			{
				gold = ((DDRelicLight1)item).RelicGoldValue;
				buyer = "an art collector";
			}
			else if (item is DDRelicLight2)
			{
				gold = ((DDRelicLight2)item).RelicGoldValue;
				buyer = "an art collector";
			}
			else if (item is DDRelicLight3)
			{
				gold = ((DDRelicLight3)item).RelicGoldValue;
				buyer = "an art collector";
			}
			else if (item is DDRelicOrbs)
			{
				gold = ((DDRelicOrbs)item).RelicGoldValue;
				buyer = "a mage";
			}
			else if (item is DDRelicPainting)
			{
				gold = ((DDRelicPainting)item).RelicGoldValue;
				buyer = "an art collector";
			}
			else if (item is DDRelicReagent)
			{
				gold = ((DDRelicReagent)item).RelicGoldValue;
				buyer = "an alchemist";
			}
			else if (item is DDRelicRugAddonDeed)
			{
				gold = ((DDRelicRugAddonDeed)item).RelicGoldValue;
				buyer = "an art collector";
			}
			else if (item is DDRelicScrolls)
			{
				gold = ((DDRelicScrolls)item).RelicGoldValue;
				buyer = "a mage";
			}
			else if (item is DDRelicStatue)
			{
				gold = ((DDRelicStatue)item).RelicGoldValue;
				buyer = "an art collector";
			}
			else if (item is DDRelicTablet)
			{
				gold = ((DDRelicTablet)item).RelicGoldValue;
				buyer = "a sage";
			}
			else if (item is DDRelicVase)
			{
				gold = ((DDRelicVase)item).RelicGoldValue;
				buyer = "an art collector";
			}
			else if (item is DDRelicLeather)
			{
				gold = ((DDRelicLeather)item).RelicGoldValue;
				buyer = "a leather worker";
			}
			else if (item is DDRelicGrave)
			{
				gold = ((DDRelicGrave)item).RelicGoldValue;
				buyer = "a necromancer";
			}
			else if (item is EmptyCanopicJar)
			{
				gold = ((EmptyCanopicJar)item).RelicGoldValue;
				buyer = "an art collector";
			}
			else if (item is HighSeasRelic)
			{
				gold = ((HighSeasRelic)item).RelicGoldValue;
				buyer = "a shipwright";
			}
			else if (item is StatueGygaxAddonDeed)
			{
				gold = ((StatueGygaxAddonDeed)item).RelicGoldValue;
				buyer = "an art collector";
			}
		}

		/// <summary>
		/// Gets the relic value for a specific buyer type
		/// </summary>
		/// <param name="relics">The relic item</param>
		/// <param name="m">The mobile buying the relic</param>
		/// <param name="isHenchman">Whether the buyer is a henchman</param>
		/// <returns>Gold value the buyer will pay</returns>
		private static int GetRelicValueForBuyer(Item relics, Mobile m, bool isHenchman)
		{
			// Art collectors (VarietyDealer)
			if (relics is DDRelicVase && (isHenchman || m is VarietyDealer))
			{
				return ((DDRelicVase)relics).RelicGoldValue;
			}
			else if (relics is DDRelicPainting && (isHenchman || m is VarietyDealer))
			{
				return ((DDRelicPainting)relics).RelicGoldValue;
			}
			else if (relics is EmptyCanopicJar && (isHenchman || m is VarietyDealer))
			{
				return ((EmptyCanopicJar)relics).RelicGoldValue;
			}
			else if (relics is DDRelicBanner && (isHenchman || m is VarietyDealer))
			{
				return ((DDRelicBanner)relics).RelicGoldValue;
			}
			else if (relics is DDRelicLight1 && (isHenchman || m is VarietyDealer))
			{
				return ((DDRelicLight1)relics).RelicGoldValue;
			}
			else if (relics is DDRelicLight2 && (isHenchman || m is VarietyDealer))
			{
				return ((DDRelicLight2)relics).RelicGoldValue;
			}
			else if (relics is DDRelicLight3 && (isHenchman || m is VarietyDealer))
			{
				return ((DDRelicLight3)relics).RelicGoldValue;
			}
			else if (relics is DDRelicArts && (isHenchman || m is VarietyDealer))
			{
				return ((DDRelicArts)relics).RelicGoldValue;
			}
			else if (relics is DDRelicStatue && (isHenchman || m is VarietyDealer))
			{
				return ((DDRelicStatue)relics).RelicGoldValue;
			}
			else if (relics is DDRelicRugAddonDeed && (isHenchman || m is VarietyDealer))
			{
				return ((DDRelicRugAddonDeed)relics).RelicGoldValue;
			}
			else if (relics is StatueGygaxAddonDeed && (isHenchman || m is VarietyDealer))
			{
				return ((StatueGygaxAddonDeed)relics).RelicGoldValue;
			}

			// Necromancers
			else if (relics is DDRelicGrave && (isHenchman || m is Necromancer || m is NecromancerGuildmaster || m is NecroMage || m is Witches))
			{
				return ((DDRelicGrave)relics).RelicGoldValue;
			}

			// Artists (pay double for paintings)
			else if (relics is DDRelicPainting && (isHenchman || m is Artist))
			{
				return ((DDRelicPainting)relics).RelicGoldValue * 2;
			}
			else if (relics is DDRelicBanner && relics.Name.Contains("painting") && (isHenchman || m is Artist))
			{
				return ((DDRelicBanner)relics).RelicGoldValue * 2;
			}

			// Specialized buyers
			else if (relics is DDRelicWeapon && (isHenchman || m is Weaponsmith))
			{
				return ((DDRelicWeapon)relics).RelicGoldValue;
			}
			else if (relics is DDRelicArmor && (isHenchman || m is Armorer))
			{
				return ((DDRelicArmor)relics).RelicGoldValue;
			}
			else if (relics is DDRelicJewels && (isHenchman || m is Jeweler))
			{
				return ((DDRelicJewels)relics).RelicGoldValue;
			}
			else if (relics is DDRelicGem && (isHenchman || m is Jeweler))
			{
				return ((DDRelicGem)relics).RelicGoldValue;
			}
			else if (relics is DDRelicInstrument && (isHenchman || m is Bard))
			{
				return ((DDRelicInstrument)relics).RelicGoldValue;
			}
			else if (relics is DDRelicScrolls && (isHenchman || m is Mage))
			{
				return ((DDRelicScrolls)relics).RelicGoldValue;
			}
			else if (relics is DDRelicClock1 && (isHenchman || m is Tinker))
			{
				return ((DDRelicClock1)relics).RelicGoldValue;
			}
			else if (relics is DDRelicClock2 && (isHenchman || m is Tinker))
			{
				return ((DDRelicClock2)relics).RelicGoldValue;
			}
			else if (relics is DDRelicClock3 && (isHenchman || m is Tinker))
			{
				return ((DDRelicClock3)relics).RelicGoldValue;
			}
			else if (relics is DDRelicCloth && (isHenchman || m is Tailor))
			{
				return ((DDRelicCloth)relics).RelicGoldValue;
			}
			else if (relics is DDRelicFur && (isHenchman || m is Furtrader))
			{
				return ((DDRelicFur)relics).RelicGoldValue;
			}
			else if (relics is DDRelicLeather && (isHenchman || m is LeatherWorker))
			{
				return ((DDRelicLeather)relics).RelicGoldValue;
			}
			else if (relics is DDRelicDrink && (isHenchman || m is TavernKeeper))
			{
				return ((DDRelicDrink)relics).RelicGoldValue;
			}
			else if (relics is DDRelicReagent && (isHenchman || m is Alchemist))
			{
				return ((DDRelicReagent)relics).RelicGoldValue;
			}
			else if (relics is DDRelicAlchemy && (isHenchman || m is Alchemist))
			{
				return ((DDRelicAlchemy)relics).RelicGoldValue;
			}
			else if (relics is DDRelicReagent && (isHenchman || m is Witches))
			{
				return ((DDRelicReagent)relics).RelicGoldValue;
			}
			else if (relics is DDRelicAlchemy && (isHenchman || m is Witches))
			{
				return ((DDRelicAlchemy)relics).RelicGoldValue;
			}
			else if (relics is ObsidianStone && (isHenchman || m is StoneCrafter))
			{
				return ((ObsidianStone)relics).RelicGoldValue;
			}
			else if (relics is DDRelicCoins && (isHenchman || m is Minter))
			{
				return ((DDRelicCoins)relics).RelicGoldValue;
			}
			else if (relics is DDRelicOrbs && (isHenchman || m is Mage))
			{
				return ((DDRelicOrbs)relics).RelicGoldValue;
			}
			else if (relics is HighSeasRelic && (isHenchman || m is Shipwright))
			{
				return ((HighSeasRelic)relics).RelicGoldValue;
			}
			else if (relics is DDRelicBook && (isHenchman || m is Scribe))
			{
				return ((DDRelicBook)relics).RelicGoldValue;
			}
			else if (relics is DDRelicTablet && (isHenchman || m is Sage))
			{
				return ((DDRelicTablet)relics).RelicGoldValue;
			}
			else if (relics is DDRelicBearRugsAddonDeed && (isHenchman || m is Furtrader))
			{
				return ((DDRelicBearRugsAddonDeed)relics).RelicGoldValue;
			}
			else if (relics is DynamicBook && (isHenchman || m is Scribe))
			{
				return Utility.RandomMinMax(DYNAMIC_BOOK_VALUE_MIN, DYNAMIC_BOOK_VALUE_MAX);
			}
			else if (relics is DynamicBook && (isHenchman || m is Sage))
			{
				return Utility.RandomMinMax(DYNAMIC_BOOK_VALUE_MIN, DYNAMIC_BOOK_VALUE_MAX);
			}

			return 0;
		}

		/// <summary>
		/// Checks if a mobile is a henchman type
		/// </summary>
		/// <param name="m">The mobile to check</param>
		/// <returns>True if the mobile is a henchman, false otherwise</returns>
		private static bool IsHenchman(Mobile m)
		{
			return m is HenchmanMonster || m is HenchmanFighter || m is HenchmanArcher || m is HenchmanWizard;
		}

		#endregion
	}
}

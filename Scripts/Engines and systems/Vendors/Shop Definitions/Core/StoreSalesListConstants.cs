namespace Server.Mobiles.Vendors.ShopDefinitions
{
	/// <summary>
	/// Centralized constants for vendor shop definitions.
	/// Extracted from StoreSalesList.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class StoreSalesListConstants
	{
		#region Common Prices

		/// <summary>Price for Bandage</summary>
		public const int PRICE_BANDAGE = 10;

		/// <summary>Price for BlankScroll</summary>
		public const int PRICE_BLANK_SCROLL = 5;

		/// <summary>Price for Bolt</summary>
		public const int PRICE_BOLT = 50;

		/// <summary>Price for Arrow</summary>
		public const int PRICE_ARROW = 50;

		/// <summary>Price for Reagent (BlackPearl, Bloodmoss, etc.)</summary>
		public const int PRICE_REAGENT = 150;

		/// <summary>Price for Lesser Potion</summary>
		public const int PRICE_LESSER_POTION = 500;

		/// <summary>Price for Greater Potion</summary>
		public const int PRICE_GREATER_POTION = 1500;

		/// <summary>Price for RecallRune</summary>
		public const int PRICE_RECALL_RUNE = 750;

		/// <summary>Price for Backpack</summary>
		public const int PRICE_BACKPACK = 150;

		/// <summary>Price for BreadLoaf</summary>
		public const int PRICE_BREAD_LOAF = 1000;

		/// <summary>Price for MeatPie</summary>
		public const int PRICE_MEAT_PIE = 1500;

		/// <summary>Price for HoodedShroudOfShadows</summary>
		public const int PRICE_HOODED_SHROUD = 50000;

		/// <summary>Price for ChargerOfTheFallen</summary>
		public const int PRICE_CHARGER_FALLEN = 2000000;

		/// <summary>Price for FullMagerySpellbook</summary>
		public const int PRICE_FULL_MAGERY_SPELLBOOK = 250000;

		/// <summary>Price for FullNecroSpellbook</summary>
		public const int PRICE_FULL_NECRO_SPELLBOOK = 350000;

		/// <summary>Price for MagicWizardsHat</summary>
		public const int PRICE_MAGIC_WIZARDS_HAT = 110;

		/// <summary>Price for Necromancy reagent (BatWing, GraveDust, etc.)</summary>
		public const int PRICE_NECRO_REAGENT = 130;

		/// <summary>Price for DaemonBlood and NoxCrystal</summary>
		public const int PRICE_NECRO_REAGENT_RARE = 160;

		/// <summary>Price for PigIron</summary>
		public const int PRICE_PIG_IRON = 150;

		#endregion

		#region Common Quantities

		/// <summary>Unlimited stock quantity</summary>
		public const int QTY_UNLIMITED = 999;

		/// <summary>Common fixed quantity (20 items)</summary>
		public const int QTY_FIXED_20 = 20;

		/// <summary>Common fixed quantity (10 items)</summary>
		public const int QTY_FIXED_10 = 10;

		/// <summary>Common random quantity range (1-100)</summary>
		public const int QTY_RANDOM_MIN = 3;
		public const int QTY_RANDOM_MAX = 36;

		/// <summary>Small random quantity range (1-5)</summary>
		public const int QTY_RANDOM_SMALL_MIN = 1;
		public const int QTY_RANDOM_SMALL_MAX = 5;

		/// <summary>Medium random quantity range (1-10)</summary>
		public const int QTY_RANDOM_MEDIUM_MIN = 1;
		public const int QTY_RANDOM_MEDIUM_MAX = 10;

		/// <summary>Large random quantity range (1-25)</summary>
		public const int QTY_RANDOM_LARGE_MIN = 1;
		public const int QTY_RANDOM_LARGE_MAX = 25;

		/// <summary>Very large random quantity range (2000-5000)</summary>
		public const int QTY_RANDOM_VERY_LARGE_MIN = 120;
		public const int QTY_RANDOM_VERY_LARGE_MAX = 250;

		/// <summary>Recipe scroll quantity range (4-8)</summary>
		public const int QTY_RECIPE_SCROLL_MIN = 3;
		public const int QTY_RECIPE_SCROLL_MAX = 9;

		/// <summary>Potion quantity range (50-200)</summary>
		public const int QTY_POTION_MIN = 10;
		public const int QTY_POTION_MAX = 40;
		
		/// <summary>Single item quantity</summary>
		public const int QTY_SINGLE = 1;
		
		/// <summary>Very rare item quantity range (1-5)</summary>
		public const int QTY_VERY_RARE_MIN = 1;
		public const int QTY_VERY_RARE_MAX = 3;
		
		/// <summary>Very rare item price range (200-400)</summary>
		public const int PRICE_VERY_RARE_ITEM_MIN = 300;
		public const int PRICE_VERY_RARE_ITEM_MAX = 600;
		
		#endregion

		#region Common Item IDs

		/// <summary>Item ID for Bandage</summary>
		public const int ITEMID_BANDAGE = 0xE21;

		/// <summary>Item ID for BlankScroll</summary>
		public const int ITEMID_BLANK_SCROLL = 0x0E34;

		/// <summary>Item ID for Bolt</summary>
		public const int ITEMID_BOLT = 0x1BFB;

		/// <summary>Item ID for Arrow</summary>
		public const int ITEMID_ARROW = 0xF3F;

		/// <summary>Item ID for Backpack</summary>
		public const int ITEMID_BACKPACK = 0x9B2;

		/// <summary>Item ID for RecallRune</summary>
		public const int ITEMID_RECALL_RUNE = 0x1f14;

		/// <summary>Item ID for Deed items (common)</summary>
		public const int ITEMID_DEED = 0x14F0;

		/// <summary>Item ID for MagicWizardsHat</summary>
		public const int ITEMID_MAGIC_WIZARDS_HAT = 0x1718;

		/// <summary>Item ID for HoodedShroudOfShadows</summary>
		public const int ITEMID_HOODED_SHROUD = 0x455;

		/// <summary>Item ID for ChargerOfTheFallen</summary>
		public const int ITEMID_CHARGER_FALLEN = 0x2D9C;

		/// <summary>Item ID for Spellbook</summary>
		public const int ITEMID_SPELLBOOK = 0xEFA;

		/// <summary>Item ID for AlchemyRecipeScroll</summary>
		public const int ITEMID_ALCHEMY_RECIPE_SCROLL = 0x14ED;

		/// <summary>Item ID for Bread/MeatPie</summary>
		public const int ITEMID_BREAD_PIE = 0x103B;

		#endregion

		#region Potion Item IDs

		/// <summary>Item ID for NightSightPotion</summary>
		public const int ITEMID_POTION_NIGHTSIGHT = 0xF06;

		/// <summary>Item ID for AgilityPotion</summary>
		public const int ITEMID_POTION_AGILITY = 0xF08;

		/// <summary>Item ID for StrengthPotion</summary>
		public const int ITEMID_POTION_STRENGTH = 0xF09;

		/// <summary>Item ID for RefreshPotion</summary>
		public const int ITEMID_POTION_REFRESH = 0xF0B;

		/// <summary>Item ID for LesserCurePotion</summary>
		public const int ITEMID_POTION_LESSER_CURE = 0xF07;

		/// <summary>Item ID for LesserHealPotion</summary>
		public const int ITEMID_POTION_LESSER_HEAL = 0xF0C;

		/// <summary>Item ID for LesserPoisonPotion</summary>
		public const int ITEMID_POTION_LESSER_POISON = 0xF0A;

		/// <summary>Item ID for LesserExplosionPotion</summary>
		public const int ITEMID_POTION_LESSER_EXPLOSION = 0xF0D;

		#endregion

		#region Reagent Item IDs

		/// <summary>Item ID for BlackPearl</summary>
		public const int ITEMID_REAGENT_BLACK_PEARL = 0xF7A;

		/// <summary>Item ID for Bloodmoss</summary>
		public const int ITEMID_REAGENT_BLOODMOSS = 0xF7B;

		/// <summary>Item ID for MandrakeRoot</summary>
		public const int ITEMID_REAGENT_MANDRAKE = 0xF86;

		/// <summary>Item ID for Garlic</summary>
		public const int ITEMID_REAGENT_GARLIC = 0xF84;

		/// <summary>Item ID for Ginseng</summary>
		public const int ITEMID_REAGENT_GINSENG = 0xF85;

		/// <summary>Item ID for Nightshade</summary>
		public const int ITEMID_REAGENT_NIGHTSHADE = 0xF88;

		/// <summary>Item ID for SpidersSilk</summary>
		public const int ITEMID_REAGENT_SPIDERS_SILK = 0xF8D;

		/// <summary>Item ID for SulfurousAsh</summary>
		public const int ITEMID_REAGENT_SULFUROUS_ASH = 0xF8C;

		/// <summary>Item ID for BatWing</summary>
		public const int ITEMID_NECRO_BAT_WING = 0xF78;

		/// <summary>Item ID for GraveDust</summary>
		public const int ITEMID_NECRO_GRAVE_DUST = 0xF8F;

		/// <summary>Item ID for DaemonBlood</summary>
		public const int ITEMID_NECRO_DAEMON_BLOOD = 0xF7D;

		/// <summary>Item ID for NoxCrystal</summary>
		public const int ITEMID_NECRO_NOX_CRYSTAL = 0xF8E;

		/// <summary>Item ID for PigIron</summary>
		public const int ITEMID_NECRO_PIG_IRON = 0xF8A;

		#endregion

		#region Common Hues

		/// <summary>Default hue (no color)</summary>
		public const int HUE_DEFAULT = 0;

		/// <summary>Hue for Alchemy recipe scrolls</summary>
		public const int HUE_ALCHEMY_RECIPE = 2003;

		/// <summary>Hue for Mana potions</summary>
		public const int HUE_MANA_POTION = 0x48D;

		/// <summary>Hue for Guild items</summary>
		public const int HUE_GUILD = 0x430;

		#endregion

		#region Price Ranges

		/// <summary>Very rare item price range (min)</summary>
		public const int PRICE_VERY_RARE_MIN = 10000;

		/// <summary>Very rare item price range (max)</summary>
		public const int PRICE_VERY_RARE_MAX = 100000;

		#endregion

		#region Scroll Pricing

		/// <summary>Base price for scrolls (first circle)</summary>
		public const int PRICE_SCROLL_BASE = 60;

		/// <summary>Price increment per circle</summary>
		public const int PRICE_SCROLL_INCREMENT = 10;

		/// <summary>Base sell price for scrolls</summary>
		public const int PRICE_SCROLL_SELL_BASE = 2;

		/// <summary>Sell price multiplier per circle</summary>
		public const int PRICE_SCROLL_SELL_MULTIPLIER = 5;

		/// <summary>Starting item ID for scrolls</summary>
		public const int ITEMID_SCROLL_START = 0x1F2E;

		/// <summary>Special item ID for scroll index 6</summary>
		public const int ITEMID_SCROLL_SPECIAL = 0x1F2D;

		#endregion

		#region Sell Prices

		/// <summary>Sell price for Bandage</summary>
		public const int SELL_PRICE_BANDAGE = 3;

		/// <summary>Sell price for BlankScroll</summary>
		public const int SELL_PRICE_BLANK_SCROLL = 3;

		/// <summary>Sell price for Lesser Potion</summary>
		public const int SELL_PRICE_LESSER_POTION = 50;

		/// <summary>Sell price for Lesser Explosion Potion</summary>
		public const int SELL_PRICE_LESSER_EXPLOSION_POTION = 33;

		/// <summary>Sell price for Bolt</summary>
		public const int SELL_PRICE_BOLT = 5;

		/// <summary>Sell price for Arrow</summary>
		public const int SELL_PRICE_ARROW = 4;

		/// <summary>Sell price for Reagent</summary>
		public const int SELL_PRICE_REAGENT = 6;

		/// <summary>Sell price for BreadLoaf</summary>
		public const int SELL_PRICE_BREAD_LOAF = 18;

		/// <summary>Sell price for Backpack</summary>
		public const int SELL_PRICE_BACKPACK = 35;

		/// <summary>Sell price for RecallRune</summary>
		public const int SELL_PRICE_RECALL_RUNE = 15;

		/// <summary>Sell price for Spellbook</summary>
		public const int SELL_PRICE_SPELLBOOK = 88;

		/// <summary>Sell price for Necromancy reagent</summary>
		public const int SELL_PRICE_NECRO_REAGENT = 9;

		#endregion

		#region Alchemist-Specific Constants

		/// <summary>Price for MortarPestle</summary>
		public const int PRICE_MORTAR_PESTLE = 5;

		/// <summary>Price for MixingCauldron</summary>
		public const int PRICE_MIXING_CAULDRON = 347;

		/// <summary>Price for MixingSpoon</summary>
		public const int PRICE_MIXING_SPOON = 34;

		/// <summary>Price for Alchemy books</summary>
		public const int PRICE_ALCHEMY_BOOK = 55;

		/// <summary>Price for Bottle</summary>
		public const int PRICE_BOTTLE = 5;

		/// <summary>Price for Jar</summary>
		public const int PRICE_JAR = 6;

		/// <summary>Price for HeatingStand</summary>
		public const int PRICE_HEATING_STAND = 2;

		/// <summary>Price for basic reagent (alchemist)</summary>
		public const int PRICE_REAGENT_BASIC = 5;

		/// <summary>Price for common reagent (alchemist)</summary>
		public const int PRICE_REAGENT_COMMON = 3;

		/// <summary>Price for special reagent (alchemist)</summary>
		public const int PRICE_REAGENT_SPECIAL = 6;

		/// <summary>Price for BottleOfAcid</summary>
		public const int PRICE_BOTTLE_OF_ACID = 320;

		/// <summary>Price for basic potion (alchemist)</summary>
		public const int PRICE_POTION_BASIC = 36;

		/// <summary>Price for LesserExplosionPotion (alchemist)</summary>
		public const int PRICE_POTION_LESSER_EXPLOSION = 39;

		/// <summary>Price for standard potion (alchemist)</summary>
		public const int PRICE_POTION_STANDARD = 41;

		/// <summary>Price for ExplosionPotion (alchemist)</summary>
		public const int PRICE_POTION_EXPLOSION = 44;

		/// <summary>Price for Greater potion (alchemist)</summary>
		public const int PRICE_POTION_GREATER = 54;

		/// <summary>Price for Lesser special potion (alchemist)</summary>
		public const int PRICE_POTION_LESSER_SPECIAL = 39;

		/// <summary>Price for special potion (alchemist)</summary>
		public const int PRICE_POTION_SPECIAL = 61;

		/// <summary>Price for Greater special potion (alchemist)</summary>
		public const int PRICE_POTION_GREATER_SPECIAL = 68;

		/// <summary>Price for InvulnerabilityPotion</summary>
		public const int PRICE_POTION_INVULNERABILITY = 900;

		/// <summary>Price for AutoResPotion</summary>
		public const int PRICE_POTION_AUTORES = 900;

		/// <summary>Price for AlchemyTub</summary>
		public const int PRICE_ALCHEMY_TUB = 580;

		/// <summary>Price for AlchemyRecipeBook</summary>
		public const int PRICE_ALCHEMY_RECIPE_BOOK = 96;

		/// <summary>Quantity for AlchemyRecipeBook</summary>
		public const int QTY_ALCHEMY_RECIPE_BOOK = 15;

		/// <summary>Hue for MixingSpoon</summary>
		public const int HUE_MIXING_SPOON = 0x979;

		/// <summary>Hue for CBookNecroticAlchemy</summary>
		public const int HUE_NECROTIC_ALCHEMY = 0x4AA;

		/// <summary>Hue for BookOfPoisons</summary>
		public const int HUE_BOOK_OF_POISONS = 0x4F8;

		/// <summary>Hue for BottleOfAcid</summary>
		public const int HUE_BOTTLE_OF_ACID = 1167;

		/// <summary>Hue for ConflagrationPotion</summary>
		public const int HUE_CONFLAGRATION = 0xAD8;

		/// <summary>Hue for ManaPotion</summary>
		public const int HUE_MANA = 0x48D;

		/// <summary>Hue for FrostbitePotion</summary>
		public const int HUE_FROSTBITE = 0xAF3;

		/// <summary>Hue for InvisibilityPotion</summary>
		public const int HUE_INVISIBILITY = 0x490;

		/// <summary>Hue for RejuvenatePotion</summary>
		public const int HUE_REJUVENATE = 0x48E;

		/// <summary>Hue for InvulnerabilityPotion</summary>
		public const int HUE_INVULNERABILITY = 0x48F;

		/// <summary>Hue for AutoResPotion</summary>
		public const int HUE_AUTORES = 0x494;

		/// <summary>Sell price for MixingCauldron</summary>
		public const int SELL_PRICE_MIXING_CAULDRON = 153;

		/// <summary>Sell price for MixingSpoon</summary>
		public const int SELL_PRICE_MIXING_SPOON = 17;

		/// <summary>Sell price for special ingredient</summary>
		public const int SELL_PRICE_SPECIAL_INGREDIENT = 120;

		/// <summary>Sell price for basic reagent (alchemist)</summary>
		public const int SELL_PRICE_REAGENT_BASIC = 3;

		/// <summary>Sell price for common reagent (alchemist)</summary>
		public const int SELL_PRICE_REAGENT_COMMON = 2;

		/// <summary>Sell price for SulfurousAsh (rare)</summary>
		public const int SELL_PRICE_SULFUROUS_ASH = 2;

		/// <summary>Sell price for special reagent (alchemist)</summary>
		public const int SELL_PRICE_REAGENT_SPECIAL = 3;

		/// <summary>Sell price for Bottle/Jar</summary>
		public const int SELL_PRICE_BOTTLE_JAR = 3;

		/// <summary>Sell price for MortarPestle</summary>
		public const int SELL_PRICE_MORTAR_PESTLE = 4;

		/// <summary>Sell price for basic potion (alchemist)</summary>
		public const int SELL_PRICE_POTION_BASIC = 7;

		/// <summary>Sell price for standard potion (alchemist)</summary>
		public const int SELL_PRICE_POTION_STANDARD = 14;

		/// <summary>Sell price for Greater potion (alchemist)</summary>
		public const int SELL_PRICE_POTION_GREATER = 28;

		/// <summary>Sell price for InvulnerabilityPotion</summary>
		public const int SELL_PRICE_POTION_INVULNERABILITY = 530;

		/// <summary>Sell price for AutoResPotion</summary>
		public const int SELL_PRICE_POTION_AUTORES = 600;

		/// <summary>Sell price for BottleOfAcid</summary>
		public const int SELL_PRICE_BOTTLE_OF_ACID = 150;

		#endregion
	}
}


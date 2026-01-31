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

		/// <summary>Medium quantity range (1-10)</summary>
		public const int QTY_MEDIUM_MIN = 1;
		public const int QTY_MEDIUM_MAX = 10;

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

		#region Tailor Constants

		/// <summary>Price for Scissors</summary>
		public const int PRICE_SCISSORS = 11;

		/// <summary>Price for SewingKit</summary>
		public const int PRICE_SEWING_KIT = 8;

		/// <summary>Price for Dyes</summary>
		public const int PRICE_DYES = 5;

		/// <summary>Price for DyeTub</summary>
		public const int PRICE_DYE_TUB = 8;

		/// <summary>Price for CottonSpoolOfThread</summary>
		public const int PRICE_COTTON_SPOOL_THREAD = 18;

		/// <summary>Price for FlaxSpoolOfThread</summary>
		public const int PRICE_FLAX_SPOOL_THREAD = 18;

		/// <summary>Price for Bandana</summary>
		public const int PRICE_BANDANA = 6;

		/// <summary>Price for SkullCap</summary>
		public const int PRICE_SKULL_CAP = 7;

		/// <summary>Price for Cap</summary>
		public const int PRICE_CAP = 10;

		/// <summary>Price for Shirt</summary>
		public const int PRICE_SHIRT = 12;

		/// <summary>Price for ShortPants</summary>
		public const int PRICE_SHORT_PANTS = 7;

		/// <summary>Price for FancyShirt</summary>
		public const int PRICE_FANCY_SHIRT = 21;

		/// <summary>Price for RoyalCoat</summary>
		public const int PRICE_ROYAL_COAT = 21;

		/// <summary>Price for RoyalShirt</summary>
		public const int PRICE_ROYAL_SHIRT = 21;

		/// <summary>Price for RusticShirt</summary>
		public const int PRICE_RUSTIC_SHIRT = 21;

		/// <summary>Price for SquireShirt</summary>
		public const int PRICE_SQUIRE_SHIRT = 21;

		/// <summary>Price for FormalCoat</summary>
		public const int PRICE_FORMAL_COAT = 21;

		/// <summary>Price for WizardShirt</summary>
		public const int PRICE_WIZARD_SHIRT = 21;

		/// <summary>Price for BeggarVest</summary>
		public const int PRICE_BEGGAR_VEST = 12;

		/// <summary>Price for RoyalVest</summary>
		public const int PRICE_ROYAL_VEST = 12;

		/// <summary>Price for RusticVest</summary>
		public const int PRICE_RUSTIC_VEST = 12;

		/// <summary>Price for SailorPants</summary>
		public const int PRICE_SAILOR_PANTS = 7;

		/// <summary>Price for PiratePants</summary>
		public const int PRICE_PIRATE_PANTS = 10;

		/// <summary>Price for RoyalSkirt</summary>
		public const int PRICE_ROYAL_SKIRT = 11;

		/// <summary>Price for Skirt</summary>
		public const int PRICE_SKIRT = 12;

		/// <summary>Price for RoyalLongSkirt</summary>
		public const int PRICE_ROYAL_LONG_SKIRT = 12;

		/// <summary>Price for LongPants</summary>
		public const int PRICE_LONG_PANTS = 10;

		/// <summary>Price for FancyDress</summary>
		public const int PRICE_FANCY_DRESS = 26;

		/// <summary>Price for PlainDress</summary>
		public const int PRICE_PLAIN_DRESS = 13;

		/// <summary>Price for Kilt</summary>
		public const int PRICE_KILT = 11;

		/// <summary>Price for HalfApron</summary>
		public const int PRICE_HALF_APRON = 10;

		/// <summary>Price for LoinCloth</summary>
		public const int PRICE_LOIN_CLOTH = 10;

		/// <summary>Price for RoyalLoinCloth</summary>
		public const int PRICE_ROYAL_LOIN_CLOTH = 10;

		/// <summary>Price for Robe</summary>
		public const int PRICE_ROBE = 18;

		/// <summary>Price for Cloak</summary>
		public const int PRICE_CLOAK = 8;

		/// <summary>Price for Doublet</summary>
		public const int PRICE_DOUBLET = 13;

		/// <summary>Price for Tunic</summary>
		public const int PRICE_TUNIC = 18;

		/// <summary>Price for JesterSuit</summary>
		public const int PRICE_JESTER_SUIT = 26;

		/// <summary>Price for JesterHat</summary>
		public const int PRICE_JESTER_HAT = 12;

		/// <summary>Price for FloppyHat</summary>
		public const int PRICE_FLOPPY_HAT = 7;

		/// <summary>Price for WideBrimHat</summary>
		public const int PRICE_WIDE_BRIM_HAT = 8;

		/// <summary>Price for TallStrawHat</summary>
		public const int PRICE_TALL_STRAW_HAT = 8;

		/// <summary>Price for StrawHat</summary>
		public const int PRICE_STRAW_HAT = 7;

		/// <summary>Price for WizardsHat</summary>
		public const int PRICE_WIZARDS_HAT = 11;

		/// <summary>Price for WitchHat</summary>
		public const int PRICE_WITCH_HAT = 11;

		/// <summary>Price for LeatherCap</summary>
		public const int PRICE_LEATHER_CAP = 10;

		/// <summary>Price for FeatheredHat</summary>
		public const int PRICE_FEATHERED_HAT = 10;

		/// <summary>Price for TricorneHat</summary>
		public const int PRICE_TRICORNE_HAT = 8;

		/// <summary>Price for PirateHat</summary>
		public const int PRICE_PIRATE_HAT = 8;

		/// <summary>Item ID for Scissors</summary>
		public const int ITEMID_SCISSORS = 0xF9F;

		/// <summary>Item ID for SewingKit</summary>
		public const int ITEMID_SEWING_KIT = 0x4C81;

		/// <summary>Item ID for Dyes</summary>
		public const int ITEMID_DYES = 0xFA9;

		/// <summary>Item ID for DyeTub</summary>
		public const int ITEMID_DYE_TUB = 0xFAB;

		/// <summary>Item ID for CottonSpoolOfThread</summary>
		public const int ITEMID_COTTON_SPOOL_THREAD = 0x543A;

		/// <summary>Item ID for FlaxSpoolOfThread</summary>
		public const int ITEMID_FLAX_SPOOL_THREAD = 0xFA0;

		/// <summary>Item ID for Bandana</summary>
		public const int ITEMID_BANDANA = 0x1540;

		/// <summary>Item ID for SkullCap</summary>
		public const int ITEMID_SKULL_CAP = 0x1544;

		/// <summary>Item ID for Cap</summary>
		public const int ITEMID_CAP = 0x1715;

		/// <summary>Item ID for Shirt</summary>
		public const int ITEMID_SHIRT = 0x1517;

		/// <summary>Item ID for ShortPants</summary>
		public const int ITEMID_SHORT_PANTS = 0x152E;

		/// <summary>Item ID for FancyShirt</summary>
		public const int ITEMID_FANCY_SHIRT = 0x1EFD;

		/// <summary>Item ID for RoyalCoat</summary>
		public const int ITEMID_ROYAL_COAT = 0x307;

		/// <summary>Item ID for RoyalShirt</summary>
		public const int ITEMID_ROYAL_SHIRT = 0x30B;

		/// <summary>Item ID for RusticShirt</summary>
		public const int ITEMID_RUSTIC_SHIRT = 0x30D;

		/// <summary>Item ID for SquireShirt</summary>
		public const int ITEMID_SQUIRE_SHIRT = 0x311;

		/// <summary>Item ID for FormalCoat</summary>
		public const int ITEMID_FORMAL_COAT = 0x403;

		/// <summary>Item ID for WizardShirt</summary>
		public const int ITEMID_WIZARD_SHIRT = 0x407;

		/// <summary>Item ID for BeggarVest</summary>
		public const int ITEMID_BEGGAR_VEST = 0x308;

		/// <summary>Item ID for RoyalVest</summary>
		public const int ITEMID_ROYAL_VEST = 0x30C;

		/// <summary>Item ID for RusticVest</summary>
		public const int ITEMID_RUSTIC_VEST = 0x30E;

		/// <summary>Item ID for SailorPants</summary>
		public const int ITEMID_SAILOR_PANTS = 0x309;

		/// <summary>Item ID for PiratePants</summary>
		public const int ITEMID_PIRATE_PANTS = 0x404;

		/// <summary>Item ID for RoyalSkirt</summary>
		public const int ITEMID_ROYAL_SKIRT = 0x30A;

		/// <summary>Item ID for Skirt</summary>
		public const int ITEMID_SKIRT = 0x1516;

		/// <summary>Item ID for RoyalLongSkirt</summary>
		public const int ITEMID_ROYAL_LONG_SKIRT = 0x408;

		/// <summary>Item ID for LongPants</summary>
		public const int ITEMID_LONG_PANTS = 0x1539;

		/// <summary>Item ID for FancyDress</summary>
		public const int ITEMID_FANCY_DRESS = 0x1EFF;

		/// <summary>Item ID for PlainDress</summary>
		public const int ITEMID_PLAIN_DRESS = 0x1F01;

		/// <summary>Item ID for Kilt</summary>
		public const int ITEMID_KILT = 0x1537;

		/// <summary>Item ID for HalfApron</summary>
		public const int ITEMID_HALF_APRON = 0x153b;

		/// <summary>Item ID for LoinCloth</summary>
		public const int ITEMID_LOIN_CLOTH = 0x2B68;

		/// <summary>Item ID for RoyalLoinCloth</summary>
		public const int ITEMID_ROYAL_LOIN_CLOTH = 0x55DB;

		/// <summary>Item ID for Robe</summary>
		public const int ITEMID_ROBE = 0x1F03;

		/// <summary>Item ID for Cloak</summary>
		public const int ITEMID_CLOAK = 0x1515;

		/// <summary>Item ID for Doublet</summary>
		public const int ITEMID_DOUBLET = 0x1F7B;

		/// <summary>Item ID for Tunic</summary>
		public const int ITEMID_TUNIC = 0x1FA1;

		/// <summary>Item ID for JesterSuit</summary>
		public const int ITEMID_JESTER_SUIT = 0x1F9F;

		/// <summary>Item ID for JesterHat</summary>
		public const int ITEMID_JESTER_HAT = 0x171C;

		/// <summary>Item ID for FloppyHat</summary>
		public const int ITEMID_FLOPPY_HAT = 0x1713;

		/// <summary>Item ID for WideBrimHat</summary>
		public const int ITEMID_WIDE_BRIM_HAT = 0x1714;

		/// <summary>Item ID for TallStrawHat</summary>
		public const int ITEMID_TALL_STRAW_HAT = 0x1716;

		/// <summary>Item ID for StrawHat</summary>
		public const int ITEMID_STRAW_HAT = 0x1717;

		/// <summary>Item ID for WizardsHat</summary>
		public const int ITEMID_WIZARDS_HAT = 0x1718;

		/// <summary>Item ID for WitchHat</summary>
		public const int ITEMID_WITCH_HAT = 0x2FC3;

		/// <summary>Item ID for LeatherCap</summary>
		public const int ITEMID_LEATHER_CAP = 0x1DB9;

		/// <summary>Item ID for FeatheredHat</summary>
		public const int ITEMID_FEATHERED_HAT = 0x171A;

		/// <summary>Item ID for TricorneHat</summary>
		public const int ITEMID_TRICORNE_HAT = 0x171B;

		/// <summary>Item ID for PirateHat</summary>
		public const int ITEMID_PIRATE_HAT = 0x2FBC;

		/// <summary>Hue for LoinCloth</summary>
		public const int HUE_LOIN_CLOTH = 637;

		#endregion

		#region Tanner Constants

		/// <summary>Price for FemaleStuddedChest</summary>
		public const int PRICE_FEMALE_STUDDED_CHEST = 62;

		/// <summary>Price for FemalePlateChest</summary>
		public const int PRICE_FEMALE_PLATE_CHEST = 207;

		/// <summary>Price for FemaleLeatherChest</summary>
		public const int PRICE_FEMALE_LEATHER_CHEST = 36;

		/// <summary>Price for LeatherShorts</summary>
		public const int PRICE_LEATHER_SHORTS = 28;

		/// <summary>Price for LeatherSkirt</summary>
		public const int PRICE_LEATHER_SKIRT = 25;

		/// <summary>Price for LeatherBustierArms</summary>
		public const int PRICE_LEATHER_BUSTIER_ARMS = 30;

		/// <summary>Price for StuddedBustierArms</summary>
		public const int PRICE_STUDDED_BUSTIER_ARMS = 50;

		/// <summary>Price for Bag</summary>
		public const int PRICE_BAG = 6;

		/// <summary>Price for Pouch</summary>
		public const int PRICE_POUCH = 6;

		/// <summary>Price for SkinningKnife</summary>
		public const int PRICE_SKINNING_KNIFE = 100;

		/// <summary>Item ID for FemaleStuddedChest</summary>
		public const int ITEMID_FEMALE_STUDDED_CHEST = 0x1C02;

		/// <summary>Item ID for FemalePlateChest</summary>
		public const int ITEMID_FEMALE_PLATE_CHEST = 0x1C04;

		/// <summary>Item ID for FemaleLeatherChest</summary>
		public const int ITEMID_FEMALE_LEATHER_CHEST = 0x1C06;

		/// <summary>Item ID for LeatherShorts</summary>
		public const int ITEMID_LEATHER_SHORTS = 0x1C00;

		/// <summary>Item ID for LeatherSkirt</summary>
		public const int ITEMID_LEATHER_SKIRT = 0x1C08;

		/// <summary>Item ID for LeatherBustierArms</summary>
		public const int ITEMID_LEATHER_BUSTIER_ARMS = 0x1C0B;

		/// <summary>Item ID for StuddedBustierArms</summary>
		public const int ITEMID_STUDDED_BUSTIER_ARMS = 0x1C0C;

		/// <summary>Item ID for Bag</summary>
		public const int ITEMID_BAG = 0xE76;

		/// <summary>Item ID for Pouch</summary>
		public const int ITEMID_POUCH = 0xE79;

		/// <summary>Item ID for SkinningKnife</summary>
		public const int ITEMID_SKINNING_KNIFE = 0xEC4;

		/// <summary>Hue for Bag</summary>
		public const int HUE_BAG = 0xABE;

		/// <summary>Item ID for Backpack</summary>
		public const int ITEMID_BACKPACK_TANNER = 0x53D5;

		#endregion

		#region Thief Constants

		/// <summary>Price for Torch</summary>
		public const int PRICE_TORCH = 8;

		/// <summary>Price for Lantern</summary>
		public const int PRICE_LANTERN = 2;

		/// <summary>Price for LearnStealingBook</summary>
		public const int PRICE_LEARN_STEALING_BOOK = 5;

		/// <summary>Price for LearnTraps</summary>
		public const int PRICE_LEARN_TRAPS = 5;

		/// <summary>Price for Lockpick</summary>
		public const int PRICE_LOCKPICK = 15;

		/// <summary>Price for WoodenBox</summary>
		public const int PRICE_WOODEN_BOX = 14;

		/// <summary>Price for Key</summary>
		public const int PRICE_KEY = 2;

		/// <summary>Price for HairDye</summary>
		public const int PRICE_HAIR_DYE = 100;

		/// <summary>Price for HairDyeBottle</summary>
		public const int PRICE_HAIR_DYE_BOTTLE = 1000;

		/// <summary>Price for DisguiseKit</summary>
		public const int PRICE_DISGUISE_KIT = 700;

		/// <summary>Price for PackGrenade</summary>
		public const int PRICE_PACK_GRENADE = 100;

		/// <summary>Price for CorruptedMoonStone</summary>
		public const int PRICE_CORRUPTED_MOON_STONE = 5000;

		/// <summary>Item ID for Torch</summary>
		public const int ITEMID_TORCH = 0xF6B;

		/// <summary>Item ID for Lantern</summary>
		public const int ITEMID_LANTERN = 0xA25;

		/// <summary>Item ID for LearnStealingBook</summary>
		public const int ITEMID_LEARN_STEALING_BOOK = 0x4C5C;

		/// <summary>Item ID for LearnTraps</summary>
		public const int ITEMID_LEARN_TRAPS = 0xFF2;

		/// <summary>Item ID for Lockpick</summary>
		public const int ITEMID_LOCKPICK = 0x14FC;

		/// <summary>Item ID for SkeletonsKey</summary>
		public const int ITEMID_SKELETONS_KEY = 0x410A;

		/// <summary>Item ID for WoodenBox</summary>
		public const int ITEMID_WOODEN_BOX = 0x9AA;

		/// <summary>Item ID for Key</summary>
		public const int ITEMID_KEY = 0x100E;

		/// <summary>Item ID for HairDye</summary>
		public const int ITEMID_HAIR_DYE = 0xEFF;

		/// <summary>Item ID for HairDyeBottle</summary>
		public const int ITEMID_HAIR_DYE_BOTTLE = 0xE0F;

		/// <summary>Item ID for DisguiseKit</summary>
		public const int ITEMID_DISGUISE_KIT = 0xE05;

		/// <summary>Item ID for PackGrenade</summary>
		public const int ITEMID_PACK_GRENADE = 0x25FD;

		/// <summary>Item ID for CorruptedMoonStone</summary>
		public const int ITEMID_CORRUPTED_MOON_STONE = 0xF8B;

		/// <summary>Price range for SkeletonsKey (min)</summary>
		public const int PRICE_SKELETONS_KEY_MIN = 100;

		/// <summary>Price range for SkeletonsKey (max)</summary>
		public const int PRICE_SKELETONS_KEY_MAX = 500;

		/// <summary>Quantity for SkeletonsKey</summary>
		public const int QTY_SKELETONS_KEY = 25;

		/// <summary>Quantity for CorruptedMoonStone</summary>
		public const int QTY_CORRUPTED_MOON_STONE = 5;

		#endregion

		#region HairStylist Constants

		/// <summary>Price for SpecialBeardDye</summary>
		public const int PRICE_SPECIAL_BEARD_DYE = 1500;

		/// <summary>Price for SpecialHairDye</summary>
		public const int PRICE_SPECIAL_HAIR_DYE = 1500;

		/// <summary>Item ID for SpecialBeardDye</summary>
		public const int ITEMID_SPECIAL_DYE = 0xE26;

		/// <summary>Sell price for SpecialBeardDye</summary>
		public const int SELL_PRICE_SPECIAL_BEARD_DYE = 250;

		/// <summary>Sell price for SpecialHairDye</summary>
		public const int SELL_PRICE_SPECIAL_HAIR_DYE = 250;

		/// <summary>Sell price for HairDye</summary>
		public const int SELL_PRICE_HAIR_DYE = 50;

		/// <summary>Sell price for HairDyeBottle</summary>
		public const int SELL_PRICE_HAIR_DYE_BOTTLE = 300;

		#endregion

		#region Glassblower Constants

		/// <summary>Price for Bottle</summary>
		public const int PRICE_BOTTLE_GLASSBLOWER = 5;

		/// <summary>Price for Jar</summary>
		public const int PRICE_JAR_GLASSBLOWER = 5;

		/// <summary>Price for GlassblowingBook</summary>
		public const int PRICE_GLASSBLOWING_BOOK = 20637;

		/// <summary>Price for SandMiningBook</summary>
		public const int PRICE_SAND_MINING_BOOK = 20637;

		/// <summary>Price for Blowpipe</summary>
		public const int PRICE_BLOWPIPE = 21;

		/// <summary>Price for Monocle</summary>
		public const int PRICE_MONOCLE = 24;

		/// <summary>Item ID for Bottle</summary>
		public const int ITEMID_BOTTLE = 0xF0E;

		/// <summary>Item ID for Jar</summary>
		public const int ITEMID_JAR = 0x10B4;

		/// <summary>Item ID for Book</summary>
		public const int ITEMID_BOOK = 0xFF4;

		/// <summary>Item ID for Blowpipe</summary>
		public const int ITEMID_BLOWPIPE = 0xE8A;

		/// <summary>Item ID for Monocle</summary>
		public const int ITEMID_MONOCLE = 0x2C84;

		/// <summary>Hue for Blowpipe</summary>
		public const int HUE_BLOWPIPE = 0x3B9;

		/// <summary>Sell price for Bottle</summary>
		public const int SELL_PRICE_BOTTLE = 3;

		/// <summary>Sell price for Jar</summary>
		public const int SELL_PRICE_JAR = 3;

		/// <summary>Sell price for GlassblowingBook</summary>
		public const int SELL_PRICE_GLASSBLOWING_BOOK = 5000;

		/// <summary>Sell price for SandMiningBook</summary>
		public const int SELL_PRICE_SAND_MINING_BOOK = 5000;

		/// <summary>Sell price for Blowpipe</summary>
		public const int SELL_PRICE_BLOWPIPE = 10;

		/// <summary>Sell price for Monocle</summary>
		public const int SELL_PRICE_MONOCLE = 12;

		#endregion

		#region Miller Constants

		/// <summary>Price for SackFlour</summary>
		public const int PRICE_SACK_FLOUR = 6;

		/// <summary>Price for WheatSheaf</summary>
		public const int PRICE_WHEAT_SHEAF = 6;

		/// <summary>Item ID for SackFlour</summary>
		public const int ITEMID_SACK_FLOUR = 0x1039;

		/// <summary>Item ID for WheatSheaf</summary>
		public const int ITEMID_WHEAT_SHEAF = 0xF36;

		/// <summary>Sell price for SackFlour</summary>
		public const int SELL_PRICE_SACK_FLOUR = 3;

		/// <summary>Sell price for WheatSheaf</summary>
		public const int SELL_PRICE_WHEAT_SHEAF = 2;

		#endregion

		#region Waiter Constants

		/// <summary>Price for BeverageBottle (Ale/Wine/Liquor)</summary>
		public const int PRICE_BEVERAGE_BOTTLE = 70;

		/// <summary>Price for Jug (Cider)</summary>
		public const int PRICE_JUG_CIDER = 130;

		/// <summary>Price for Pitcher (Milk)</summary>
		public const int PRICE_PITCHER_MILK = 180;

		/// <summary>Price for Pitcher (Ale/Cider/Liquor/Wine)</summary>
		public const int PRICE_PITCHER_BEVERAGE = 110;

		/// <summary>Price for Pitcher (Water)</summary>
		public const int PRICE_PITCHER_WATER = 90;

		/// <summary>Price for Pitcher (empty)</summary>
		public const int PRICE_PITCHER_EMPTY = 3;

		/// <summary>Price for BreadLoaf</summary>
		public const int PRICE_BREAD_LOAF_WAITER = 60;

		/// <summary>Price for CheeseWheel</summary>
		public const int PRICE_CHEESE_WHEEL = 410;

		/// <summary>Price for CookedBird</summary>
		public const int PRICE_COOKED_BIRD = 170;

		/// <summary>Price for LambLeg</summary>
		public const int PRICE_LAMB_LEG = 80;

		/// <summary>Price for WoodenBowlOfCarrots</summary>
		public const int PRICE_WOODEN_BOWL_CARROTS = 30;

		/// <summary>Price for WoodenBowlOfCorn</summary>
		public const int PRICE_WOODEN_BOWL_CORN = 30;

		/// <summary>Price for WoodenBowlOfLettuce</summary>
		public const int PRICE_WOODEN_BOWL_LETTUCE = 30;

		/// <summary>Price for WoodenBowlOfPeas</summary>
		public const int PRICE_WOODEN_BOWL_PEAS = 30;

		/// <summary>Price for EmptyPewterBowl</summary>
		public const int PRICE_EMPTY_PEWTER_BOWL = 5;

		/// <summary>Price for PewterBowlOfCorn</summary>
		public const int PRICE_PEWTER_BOWL_CORN = 30;

		/// <summary>Price for PewterBowlOfLettuce</summary>
		public const int PRICE_PEWTER_BOWL_LETTUCE = 30;

		/// <summary>Price for PewterBowlOfPeas</summary>
		public const int PRICE_PEWTER_BOWL_PEAS = 30;

		/// <summary>Price for PewterBowlOfFoodPotatos</summary>
		public const int PRICE_PEWTER_BOWL_POTATOS = 30;

		/// <summary>Price for WoodenBowlOfStew</summary>
		public const int PRICE_WOODEN_BOWL_STEW = 30;

		/// <summary>Price for WoodenBowlOfTomatoSoup</summary>
		public const int PRICE_WOODEN_BOWL_TOMATO_SOUP = 30;

	/// <summary>Price for ApplePie (Barkeeper)</summary>
	public const int PRICE_APPLE_PIE_BARKEEPER = 100;

		/// <summary>Item ID for BeverageBottle (Ale)</summary>
		public const int ITEMID_BEVERAGE_BOTTLE_ALE = 0x99F;

		/// <summary>Item ID for BeverageBottle (Wine)</summary>
		public const int ITEMID_BEVERAGE_BOTTLE_WINE = 0x9C7;

		/// <summary>Item ID for BeverageBottle (Liquor)</summary>
		public const int ITEMID_BEVERAGE_BOTTLE_LIQUOR = 0x99B;

		/// <summary>Item ID for Jug (Cider)</summary>
		public const int ITEMID_JUG_CIDER = 0x9C8;

		/// <summary>Item ID for Pitcher (Milk)</summary>
		public const int ITEMID_PITCHER_MILK = 0x9F0;

		/// <summary>Item ID for Pitcher (Ale)</summary>
		public const int ITEMID_PITCHER_ALE = 0x1F95;

		/// <summary>Item ID for Pitcher (Cider)</summary>
		public const int ITEMID_PITCHER_CIDER = 0x1F97;

		/// <summary>Item ID for Pitcher (Liquor)</summary>
		public const int ITEMID_PITCHER_LIQUOR = 0x1F99;

		/// <summary>Item ID for Pitcher (Wine)</summary>
		public const int ITEMID_PITCHER_WINE = 0x1F9B;

		/// <summary>Item ID for Pitcher (Water)</summary>
		public const int ITEMID_PITCHER_WATER = 0x1F9D;

		/// <summary>Item ID for CheeseWheel</summary>
		public const int ITEMID_CHEESE_WHEEL = 0x97E;

		/// <summary>Item ID for CookedBird</summary>
		public const int ITEMID_COOKED_BIRD = 0x9B7;

		/// <summary>Item ID for LambLeg</summary>
		public const int ITEMID_LAMB_LEG = 0x160A;

		/// <summary>Item ID for WoodenBowlOfCarrots</summary>
		public const int ITEMID_WOODEN_BOWL_CARROTS = 0x15F9;

		/// <summary>Item ID for WoodenBowlOfCorn</summary>
		public const int ITEMID_WOODEN_BOWL_CORN = 0x15FA;

		/// <summary>Item ID for WoodenBowlOfLettuce</summary>
		public const int ITEMID_WOODEN_BOWL_LETTUCE = 0x15FB;

		/// <summary>Item ID for WoodenBowlOfPeas</summary>
		public const int ITEMID_WOODEN_BOWL_PEAS = 0x15FC;

		/// <summary>Item ID for EmptyPewterBowl</summary>
		public const int ITEMID_EMPTY_PEWTER_BOWL = 0x15FD;

		/// <summary>Item ID for PewterBowlOfCorn</summary>
		public const int ITEMID_PEWTER_BOWL_CORN = 0x15FE;

		/// <summary>Item ID for PewterBowlOfLettuce</summary>
		public const int ITEMID_PEWTER_BOWL_LETTUCE = 0x15FF;

		/// <summary>Item ID for PewterBowlOfPeas</summary>
		public const int ITEMID_PEWTER_BOWL_PEAS = 0x1600;

		/// <summary>Item ID for PewterBowlOfFoodPotatos</summary>
		public const int ITEMID_PEWTER_BOWL_POTATOS = 0x1601;

		/// <summary>Item ID for WoodenBowlOfStew</summary>
		public const int ITEMID_WOODEN_BOWL_STEW = 0x1604;

		/// <summary>Item ID for WoodenBowlOfTomatoSoup</summary>
		public const int ITEMID_WOODEN_BOWL_TOMATO_SOUP = 0x1606;

	/// <summary>Item ID for ApplePie</summary>
	public const int ITEMID_APPLE_PIE = 0x1041;

	/// <summary>Quantity range for Pitcher (empty)</summary>
	public const int QTY_PITCHER_EMPTY_MIN = 50;
	public const int QTY_PITCHER_EMPTY_MAX = 100;

	#endregion

	#region Barkeeper Constants


	/// <summary>Price for tarotpoker</summary>
	public const int PRICE_TAROT_POKER = 115;

	/// <summary>Price for Chessboard</summary>
	public const int PRICE_CHESSBOARD = 211;

	/// <summary>Price for CheckerBoard</summary>
	public const int PRICE_CHECKER_BOARD = 211;

	/// <summary>Price for MahjongGame</summary>
	public const int PRICE_MAHJONG_GAME = 6000;

	/// <summary>Price for Backgammon</summary>
	public const int PRICE_BACKGAMMON = 200;

	/// <summary>Price for Dices</summary>
	public const int PRICE_DICES = 20;

	/// <summary>Price for Waterskin</summary>
	public const int PRICE_WATERSKIN = 50;

	/// <summary>Price for HenchmanFighterItem</summary>
	public const int PRICE_HENCHMAN_FIGHTER = 5000;

	/// <summary>Price for HenchmanArcherItem</summary>
	public const int PRICE_HENCHMAN_ARCHER = 6000;

	/// <summary>Price for HenchmanWizardItem</summary>
	public const int PRICE_HENCHMAN_WIZARD = 7000;

	/// <summary>Price for ContractOfEmployment</summary>
	public const int PRICE_CONTRACT_EMPLOYMENT = 1252;

	/// <summary>Price for BarkeepContract</summary>
	public const int PRICE_BARKEEP_CONTRACT = 1252;

	/// <summary>Price for VendorRentalContract</summary>
	public const int PRICE_VENDOR_RENTAL_CONTRACT = 1252;

	/// <summary>Item ID for tarotpoker</summary>
	public const int ITEMID_TAROT_POKER = 0x12AB;

	/// <summary>Item ID for Chessboard</summary>
	public const int ITEMID_CHESSBOARD = 0xFA6;

	/// <summary>Item ID for CheckerBoard</summary>
	public const int ITEMID_CHECKER_BOARD = 0xFA6;

	/// <summary>Item ID for MahjongGame</summary>
	public const int ITEMID_MAHJONG_GAME = 0xFAA;

	/// <summary>Item ID for Backgammon</summary>
	public const int ITEMID_BACKGAMMON = 0xE1C;

	/// <summary>Item ID for Dices</summary>
	public const int ITEMID_DICES = 0xFA7;

	/// <summary>Item ID for Waterskin</summary>
	public const int ITEMID_WATERSKIN = 0xA21;

	/// <summary>Item ID for HenchmanFighterItem</summary>
	public const int ITEMID_HENCHMAN_FIGHTER = 0x1419;

	/// <summary>Item ID for HenchmanArcherItem</summary>
	public const int ITEMID_HENCHMAN_ARCHER = 0xF50;

	/// <summary>Item ID for HenchmanWizardItem</summary>
	public const int ITEMID_HENCHMAN_WIZARD = 0xE30;

	/// <summary>Item ID for ContractOfEmployment</summary>
	public const int ITEMID_CONTRACT_EMPLOYMENT = 0x14F0;

	/// <summary>Item ID for BarkeepContract</summary>
	public const int ITEMID_BARKEEP_CONTRACT = 0x14F0;

	/// <summary>Item ID for VendorRentalContract</summary>
	public const int ITEMID_VENDOR_RENTAL_CONTRACT = 0x14F0;

	/// <summary>Hue for Henchman items</summary>
	public const int HUE_HENCHMAN = 0xB96;

	/// <summary>Hue for VendorRentalContract</summary>
	public const int HUE_VENDOR_RENTAL_CONTRACT = 0x672;

	/// <summary>Quantity range for Barkeeper items (min)</summary>
	public const int QTY_BARKEEPER_MIN = 1;

	/// <summary>Quantity range for Barkeeper items (max)</summary>
	public const int QTY_BARKEEPER_MAX = 100;

	/// <summary>Sell price for WoodenBowlOfCarrots</summary>
	public const int SELL_PRICE_WOODEN_BOWL_CARROTS = 5;

	/// <summary>Sell price for WoodenBowlOfCorn</summary>
	public const int SELL_PRICE_WOODEN_BOWL_CORN = 5;

	/// <summary>Sell price for WoodenBowlOfLettuce</summary>
	public const int SELL_PRICE_WOODEN_BOWL_LETTUCE = 5;

	/// <summary>Sell price for WoodenBowlOfPeas</summary>
	public const int SELL_PRICE_WOODEN_BOWL_PEAS = 5;

	/// <summary>Sell price for EmptyPewterBowl</summary>
	public const int SELL_PRICE_EMPTY_PEWTER_BOWL = 5;

	/// <summary>Sell price for PewterBowlOfCorn</summary>
	public const int SELL_PRICE_PEWTER_BOWL_CORN = 5;

	/// <summary>Sell price for PewterBowlOfLettuce</summary>
	public const int SELL_PRICE_PEWTER_BOWL_LETTUCE = 5;

	/// <summary>Sell price for PewterBowlOfPeas</summary>
	public const int SELL_PRICE_PEWTER_BOWL_PEAS = 5;

	/// <summary>Sell price for PewterBowlOfFoodPotatos</summary>
	public const int SELL_PRICE_PEWTER_BOWL_POTATOS = 5;

	/// <summary>Sell price for WoodenBowlOfStew</summary>
	public const int SELL_PRICE_WOODEN_BOWL_STEW = 5;

	/// <summary>Sell price for WoodenBowlOfTomatoSoup</summary>
	public const int SELL_PRICE_WOODEN_BOWL_TOMATO_SOUP = 5;

	/// <summary>Sell price for BeverageBottle</summary>
	public const int SELL_PRICE_BEVERAGE_BOTTLE = 15;

	/// <summary>Sell price for Jug</summary>
	public const int SELL_PRICE_JUG = 15;

	/// <summary>Sell price for Pitcher</summary>
	public const int SELL_PRICE_PITCHER = 1;

	/// <summary>Sell price for GlassMug</summary>
	public const int SELL_PRICE_GLASS_MUG = 5;

	/// <summary>Sell price for CheeseWheel</summary>
	public const int SELL_PRICE_CHEESE_WHEEL = 50;

	/// <summary>Sell price for Ribs</summary>
	public const int SELL_PRICE_RIBS = 15;

	/// <summary>Sell price for Peach</summary>
	public const int SELL_PRICE_PEACH = 5;

	/// <summary>Sell price for Pear</summary>
	public const int SELL_PRICE_PEAR = 5;

	/// <summary>Sell price for Grapes</summary>
	public const int SELL_PRICE_GRAPES = 5;

	/// <summary>Sell price for Apple</summary>
	public const int SELL_PRICE_APPLE = 5;

	/// <summary>Sell price for Banana</summary>
	public const int SELL_PRICE_BANANA = 5;

	/// <summary>Sell price for Candle</summary>
	public const int SELL_PRICE_CANDLE = 3;

	/// <summary>Sell price for Chessboard</summary>
	public const int SELL_PRICE_CHESSBOARD = 1;

	/// <summary>Sell price for CheckerBoard</summary>
	public const int SELL_PRICE_CHECKER_BOARD = 1;

	/// <summary>Sell price for tarotpoker</summary>
	public const int SELL_PRICE_TAROT_POKER = 2;

	/// <summary>Sell price for MahjongGame</summary>
	public const int SELL_PRICE_MAHJONG_GAME = 3;

	/// <summary>Sell price for Backgammon</summary>
	public const int SELL_PRICE_BACKGAMMON = 1;

	/// <summary>Sell price for Dices</summary>
	public const int SELL_PRICE_DICES = 1;

	/// <summary>Sell price for ContractOfEmployment</summary>
	public const int SELL_PRICE_CONTRACT_EMPLOYMENT = 626;

	/// <summary>Sell price for Waterskin</summary>
	public const int SELL_PRICE_WATERSKIN = 2;

	/// <summary>Sell price range for RomulanAle (min)</summary>
	public const int SELL_PRICE_ROMULAN_ALE_MIN = 200;

	/// <summary>Sell price range for RomulanAle (max)</summary>
	public const int SELL_PRICE_ROMULAN_ALE_MAX = 1000;

	#endregion

		#region Miner Constants

		/// <summary>Price for Bag (Miner)</summary>
		public const int PRICE_BAG_MINER = 6;

		/// <summary>Price for Candle (Miner)</summary>
		public const int PRICE_CANDLE_MINER = 6;

		/// <summary>Price for Torch (Miner)</summary>
		public const int PRICE_TORCH_MINER = 8;

		/// <summary>Price for Lantern (Miner)</summary>
		public const int PRICE_LANTERN_MINER = 2;

		/// <summary>Price for Pickaxe</summary>
		public const int PRICE_PICKAXE = 25;

		/// <summary>Price for Shovel</summary>
		public const int PRICE_SHOVEL = 12;

		/// <summary>Price for OreShovel</summary>
		public const int PRICE_ORE_SHOVEL = 10;

		/// <summary>Item ID for Candle</summary>
		public const int ITEMID_CANDLE = 0xA28;

		/// <summary>Item ID for Pickaxe</summary>
		public const int ITEMID_PICKAXE = 0xE86;

		/// <summary>Item ID for Shovel</summary>
		public const int ITEMID_SHOVEL = 0xF39;

		/// <summary>Item ID for OreShovel</summary>
		public const int ITEMID_ORE_SHOVEL = 0xF39;

		/// <summary>Hue for OreShovel</summary>
		public const int HUE_ORE_SHOVEL = 0x96D;

		/// <summary>Sell price for Pickaxe</summary>
		public const int SELL_PRICE_PICKAXE = 12;

		/// <summary>Sell price for Shovel</summary>
		public const int SELL_PRICE_SHOVEL = 6;

		/// <summary>Sell price for OreShovel</summary>
		public const int SELL_PRICE_ORE_SHOVEL = 5;

		/// <summary>Sell price for Lantern</summary>
		public const int SELL_PRICE_LANTERN = 1;

		/// <summary>Sell price for Torch</summary>
		public const int SELL_PRICE_TORCH = 3;

		/// <summary>Sell price for Bag</summary>
		public const int SELL_PRICE_BAG = 3;


		#endregion

		#region Monk Constants

		/// <summary>Price for MonkRobe</summary>
		public const int PRICE_MONK_ROBE = 136;

		/// <summary>Item ID for MonkRobe</summary>
		public const int ITEMID_MONK_ROBE = 0x204E;

		/// <summary>Hue for MonkRobe</summary>
		public const int HUE_MONK_ROBE = 0x21E;

		#endregion

		#region LeatherWorker Constants

		/// <summary>Price for Hides (LeatherWorker)</summary>
		public const int PRICE_HIDES_LEATHERWORKER = 4;

		/// <summary>Price for Leather (LeatherWorker)</summary>
		public const int PRICE_LEATHER_LEATHERWORKER = 4;

		/// <summary>Price for Waterskin (LeatherWorker)</summary>
		public const int PRICE_WATERSKIN_LEATHERWORKER = 5;

		/// <summary>Price for DemonSkin</summary>
		public const int PRICE_DEMON_SKIN = 1235;

		/// <summary>Price for DragonSkin</summary>
		public const int PRICE_DRAGON_SKIN = 1235;

		/// <summary>Price for NightmareSkin</summary>
		public const int PRICE_NIGHTMARE_SKIN = 1228;

		/// <summary>Price for SerpentSkin</summary>
		public const int PRICE_SERPENT_SKIN = 1214;

		/// <summary>Price for TrollSkin</summary>
		public const int PRICE_TROLL_SKIN = 1221;

		/// <summary>Price for UnicornSkin</summary>
		public const int PRICE_UNICORN_SKIN = 1228;

		/// <summary>Item ID for Hides</summary>
		public const int ITEMID_HIDES = 0x1078;

		/// <summary>Item ID for Leather</summary>
		public const int ITEMID_LEATHER = 0x1081;

		/// <summary>Quantity range for very rare skins</summary>
		public const int QTY_VERY_RARE_SKIN_MIN = 1;
		public const int QTY_VERY_RARE_SKIN_MAX = 10;

		/// <summary>Sell price for ThighBoots</summary>
		public const int SELL_PRICE_THIGH_BOOTS = 28;

		/// <summary>Sell price for MagicBoots</summary>
		public const int SELL_PRICE_MAGIC_BOOTS = 25;

		/// <summary>Sell price for MagicBelt</summary>
		public const int SELL_PRICE_MAGIC_BELT = 100;

		/// <summary>Sell price for MagicSash</summary>
		public const int SELL_PRICE_MAGIC_SASH = 100;

		/// <summary>Sell price for ThrowingGloves</summary>
		public const int SELL_PRICE_THROWING_GLOVES = 10;

		/// <summary>Sell price for PugilistGlove</summary>
		public const int SELL_PRICE_PUGILIST_GLOVE = 10;

		/// <summary>Sell price for PugilistGloves</summary>
		public const int SELL_PRICE_PUGILIST_GLOVES = 10;

		/// <summary>Sell price for UnicornSkin</summary>
		public const int SELL_PRICE_UNICORN_SKIN = 30;

		/// <summary>Sell price for DemonSkin</summary>
		public const int SELL_PRICE_DEMON_SKIN = 40;

		/// <summary>Sell price for DragonSkin</summary>
		public const int SELL_PRICE_DRAGON_SKIN = 50;

		/// <summary>Sell price for NightmareSkin</summary>
		public const int SELL_PRICE_NIGHTMARE_SKIN = 30;

		/// <summary>Sell price for SerpentSkin</summary>
		public const int SELL_PRICE_SERPENT_SKIN = 10;

		/// <summary>Sell price for TrollSkin</summary>
		public const int SELL_PRICE_TROLL_SKIN = 20;

		/// <summary>Sell price for Hides</summary>
		public const int SELL_PRICE_HIDES = 2;

		/// <summary>Sell price for SpinedHides</summary>
		public const int SELL_PRICE_SPINED_HIDES = 3;

		/// <summary>Sell price for HornedHides</summary>
		public const int SELL_PRICE_HORNED_HIDES = 3;

		/// <summary>Sell price for BarbedHides</summary>
		public const int SELL_PRICE_BARBED_HIDES = 4;

		/// <summary>Sell price for Leather</summary>
		public const int SELL_PRICE_LEATHER = 3;

		#endregion

		#region Mapmaker Constants

		/// <summary>Price for BlankMap</summary>
		public const int PRICE_BLANK_MAP = 5;

		/// <summary>Price for MapmakersPen</summary>
		public const int PRICE_MAPMAKERS_PEN = 8;

		/// <summary>Price for BlankScroll (Mapmaker)</summary>
		public const int PRICE_BLANK_SCROLL_MAPMAKER = 12;

		/// <summary>Price range for MasterSkeletonsKey</summary>
		public const int PRICE_MASTER_SKELETONS_KEY_MIN = 750;
		public const int PRICE_MASTER_SKELETONS_KEY_MAX = 1000;

		/// <summary>Quantity for MasterSkeletonsKey</summary>
		public const int QTY_MASTER_SKELETONS_KEY = 25;

		/// <summary>Quantity range for BlankScroll (Mapmaker)</summary>
		public const int QTY_BLANK_SCROLL_MAPMAKER_MIN = 50;
		public const int QTY_BLANK_SCROLL_MAPMAKER_MAX = 200;

		/// <summary>Item ID for BlankMap</summary>
		public const int ITEMID_BLANK_MAP = 0x14EC;

		/// <summary>Item ID for MapmakersPen</summary>
		public const int ITEMID_MAPMAKERS_PEN = 0x2052;

		/// <summary>Item ID for BlankScroll (Mapmaker)</summary>
		public const int ITEMID_BLANK_SCROLL_MAPMAKER = 0xEF3;

		/// <summary>Item ID for MasterSkeletonsKey</summary>
		public const int ITEMID_MASTER_SKELETONS_KEY = 0x410B;

		/// <summary>Sell price for BlankScroll (Mapmaker)</summary>
		public const int SELL_PRICE_BLANK_SCROLL_MAPMAKER = 6;

		/// <summary>Sell price for MapmakersPen</summary>
		public const int SELL_PRICE_MAPMAKERS_PEN = 4;

		/// <summary>Sell price for BlankMap</summary>
		public const int SELL_PRICE_BLANK_MAP = 2;

		/// <summary>Sell price for CityMap</summary>
		public const int SELL_PRICE_CITY_MAP = 3;

		/// <summary>Sell price for LocalMap</summary>
		public const int SELL_PRICE_LOCAL_MAP = 3;

		/// <summary>Sell price for WorldMap</summary>
		public const int SELL_PRICE_WORLD_MAP = 3;

		/// <summary>Sell price for PresetMapEntry</summary>
		public const int SELL_PRICE_PRESET_MAP_ENTRY = 3;

		/// <summary>Sell price range for WorldMapLodor</summary>
		public const int SELL_PRICE_WORLD_MAP_LODOR_MIN = 10;
		public const int SELL_PRICE_WORLD_MAP_LODOR_MAX = 150;

		/// <summary>Sell price range for WorldMapSosaria</summary>
		public const int SELL_PRICE_WORLD_MAP_SOSARIA_MIN = 10;
		public const int SELL_PRICE_WORLD_MAP_SOSARIA_MAX = 150;

		/// <summary>Sell price range for WorldMapBottle</summary>
		public const int SELL_PRICE_WORLD_MAP_BOTTLE_MIN = 10;
		public const int SELL_PRICE_WORLD_MAP_BOTTLE_MAX = 150;

		/// <summary>Sell price range for WorldMapSerpent</summary>
		public const int SELL_PRICE_WORLD_MAP_SERPENT_MIN = 10;
		public const int SELL_PRICE_WORLD_MAP_SERPENT_MAX = 150;

		/// <summary>Sell price range for WorldMapUmber</summary>
		public const int SELL_PRICE_WORLD_MAP_UMBER_MIN = 10;
		public const int SELL_PRICE_WORLD_MAP_UMBER_MAX = 150;

		/// <summary>Sell price range for WorldMapAmbrosia</summary>
		public const int SELL_PRICE_WORLD_MAP_AMBROSIA_MIN = 10;
		public const int SELL_PRICE_WORLD_MAP_AMBROSIA_MAX = 150;

		/// <summary>Sell price range for WorldMapIslesOfDread</summary>
		public const int SELL_PRICE_WORLD_MAP_ISLES_OF_DREAD_MIN = 10;
		public const int SELL_PRICE_WORLD_MAP_ISLES_OF_DREAD_MAX = 150;

		/// <summary>Sell price range for WorldMapSavage</summary>
		public const int SELL_PRICE_WORLD_MAP_SAVAGE_MIN = 10;
		public const int SELL_PRICE_WORLD_MAP_SAVAGE_MAX = 150;

		/// <summary>Sell price range for WorldMapUnderworld</summary>
		public const int SELL_PRICE_WORLD_MAP_UNDERWORLD_MIN = 20;
		public const int SELL_PRICE_WORLD_MAP_UNDERWORLD_MAX = 300;

		/// <summary>Sell price range for AlternateRealityMap</summary>
		public const int SELL_PRICE_ALTERNATE_REALITY_MAP_MIN = 500;
		public const int SELL_PRICE_ALTERNATE_REALITY_MAP_MAX = 1000;

		#endregion

		#region Rancher Constants

		/// <summary>Price for PackHorse</summary>
		public const int PRICE_PACK_HORSE = 631;

		/// <summary>Item ID for PackHorse</summary>
		public const int ITEMID_PACK_HORSE = 291;

		#endregion

		#region Ranger Constants

		/// <summary>Price for Cat</summary>
		public const int PRICE_CAT = 138;
		/// <summary>Item ID for Cat</summary>
		public const int ITEMID_CAT = 201;

		/// <summary>Price for Dog</summary>
		public const int PRICE_DOG = 181;
		/// <summary>Item ID for Dog</summary>
		public const int ITEMID_DOG = 217;

		/// <summary>Price for PackLlama</summary>
		public const int PRICE_PACK_LLAMA = 491;
		/// <summary>Item ID for PackLlama</summary>
		public const int ITEMID_PACK_LLAMA = 292;

		/// <summary>Price for PackMule</summary>
		public const int PRICE_PACK_MULE = 10000;
		/// <summary>Item ID for PackMule</summary>
		public const int ITEMID_PACK_MULE = 291;

		/// <summary>Price for Crossbow</summary>
		public const int PRICE_CROSSBOW = 55;
		/// <summary>Item ID for Crossbow</summary>
		public const int ITEMID_CROSSBOW = 0xF50;

		/// <summary>Price for HeavyCrossbow</summary>
		public const int PRICE_HEAVY_CROSSBOW = 55;
		/// <summary>Item ID for HeavyCrossbow</summary>
		public const int ITEMID_HEAVY_CROSSBOW = 0x13FD;

		/// <summary>Price for RepeatingCrossbow</summary>
		public const int PRICE_REPEATING_CROSSBOW = 46;
		/// <summary>Item ID for RepeatingCrossbow</summary>
		public const int ITEMID_REPEATING_CROSSBOW = 0x26C3;

		/// <summary>Price for CompositeBow</summary>
		public const int PRICE_COMPOSITE_BOW = 45;
		/// <summary>Item ID for CompositeBow</summary>
		public const int ITEMID_COMPOSITE_BOW = 0x26C2;

		/// <summary>Price for Bow</summary>
		public const int PRICE_BOW = 40;
		/// <summary>Item ID for Bow</summary>
		public const int ITEMID_BOW = 0x13B2;

		/// <summary>Price for Feather</summary>
		public const int PRICE_FEATHER = 2;
		/// <summary>Item ID for Feather</summary>
		public const int ITEMID_FEATHER = 0x4CCD;

		/// <summary>Price for Shaft</summary>
		public const int PRICE_SHAFT = 3;
		/// <summary>Item ID for Shaft</summary>
		public const int ITEMID_SHAFT = 0x1BD4;

		/// <summary>Price for ArcherQuiver</summary>
		public const int PRICE_ARCHER_QUIVER = 32;
		/// <summary>Item ID for ArcherQuiver</summary>
		public const int ITEMID_ARCHER_QUIVER = 0x2B02;

		/// <summary>Price for RangerArms</summary>
		public const int PRICE_RANGER_ARMS = 87;
		/// <summary>Item ID for RangerArms</summary>
		public const int ITEMID_RANGER_ARMS = 0x13DC;

		/// <summary>Price for RangerChest</summary>
		public const int PRICE_RANGER_CHEST = 128;
		/// <summary>Item ID for RangerChest</summary>
		public const int ITEMID_RANGER_CHEST = 0x13DB;

		/// <summary>Price for RangerGloves</summary>
		public const int PRICE_RANGER_GLOVES = 79;
		/// <summary>Item ID for RangerGloves</summary>
		public const int ITEMID_RANGER_GLOVES = 0x13D5;

		/// <summary>Price for RangerGorget</summary>
		public const int PRICE_RANGER_GORGET = 73;
		/// <summary>Item ID for RangerGorget</summary>
		public const int ITEMID_RANGER_GORGET = 0x13D6;

		/// <summary>Price for RangerLegs</summary>
		public const int PRICE_RANGER_LEGS = 103;
		/// <summary>Item ID for RangerLegs</summary>
		public const int ITEMID_RANGER_LEGS = 0x13DA;

		/// <summary>Hue for Ranger armor</summary>
		public const int HUE_RANGER_ARMOR = 0x59C;

		/// <summary>Price for SmallTent</summary>
		public const int PRICE_SMALL_TENT = 200;
		/// <summary>Item ID for SmallTent</summary>
		public const int ITEMID_SMALL_TENT = 0x1914;

		/// <summary>Price for CampersTent</summary>
		public const int PRICE_CAMPERS_TENT = 500;
		/// <summary>Item ID for CampersTent</summary>
		public const int ITEMID_CAMPERS_TENT = 0x0A59;

		/// <summary>Price for MyTentEastAddonDeed</summary>
		public const int PRICE_MY_TENT_EAST_ADDON_DEED = 1000;
		/// <summary>Item ID for MyTentEastAddonDeed</summary>
		public const int ITEMID_MY_TENT_EAST_ADDON_DEED = 0xA58;

		/// <summary>Price for MyTentSouthAddonDeed</summary>
		public const int PRICE_MY_TENT_SOUTH_ADDON_DEED = 1000;
		/// <summary>Item ID for MyTentSouthAddonDeed</summary>
		public const int ITEMID_MY_TENT_SOUTH_ADDON_DEED = 0xA59;

		/// <summary>Price for TrapKit</summary>
		public const int PRICE_TRAP_KIT = 420;
		/// <summary>Item ID for TrapKit</summary>
		public const int ITEMID_TRAP_KIT = 0x1EBB;

		/// <summary>Quantity range for Bandage (min)</summary>
		public const int QTY_BANDAGE_MIN = 10;
		/// <summary>Quantity range for Bandage (max)</summary>
		public const int QTY_BANDAGE_MAX = 60;

		/// <summary>Quantity range for ammunition (min)</summary>
		public const int QTY_AMMUNITION_MIN = 30;
		/// <summary>Quantity range for ammunition (max)</summary>
		public const int QTY_AMMUNITION_MAX = 60;

		/// <summary>Quantity range for tents (min)</summary>
		public const int QTY_TENT_MIN = 1;
		/// <summary>Quantity range for tents (max)</summary>
		public const int QTY_TENT_MAX = 5;

		// Sell prices for Ranger items
		public const int SELL_PRICE_MY_TENT_EAST_ADDON_DEED = 200;
		public const int SELL_PRICE_MY_TENT_SOUTH_ADDON_DEED = 200;
		public const int SELL_PRICE_SMALL_TENT = 50;
		public const int SELL_PRICE_CAMPERS_TENT = 100;
		public const int SELL_PRICE_CROSSBOW = 27;
		public const int SELL_PRICE_HEAVY_CROSSBOW = 28;
		public const int SELL_PRICE_REPEATING_CROSSBOW = 23;
		public const int SELL_PRICE_COMPOSITE_BOW = 22;
		public const int SELL_PRICE_BOW = 20;
		public const int SELL_PRICE_FEATHER = 1;
		public const int SELL_PRICE_SHAFT = 1;
		public const int SELL_PRICE_ARCHER_QUIVER = 16;
		public const int SELL_PRICE_RANGER_ARMS = 43;
		public const int SELL_PRICE_RANGER_CHEST = 64;
		public const int SELL_PRICE_RANGER_GLOVES = 40;
		public const int SELL_PRICE_RANGER_LEGS = 51;
		public const int SELL_PRICE_RANGER_GORGET = 36;
		public const int SELL_PRICE_TRAP_KIT = 210;

		#endregion

		#region Tinker Constants

		/// <summary>Price for Clock</summary>
		public const int PRICE_CLOCK = 22;
		/// <summary>Item ID for Clock</summary>
		public const int ITEMID_CLOCK = 0x104B;

		/// <summary>Price for ClockParts</summary>
		public const int PRICE_CLOCK_PARTS = 3;
		/// <summary>Item ID for ClockParts</summary>
		public const int ITEMID_CLOCK_PARTS = 0x104F;

		/// <summary>Price for AxleGears</summary>
		public const int PRICE_AXLE_GEARS = 3;
		/// <summary>Item ID for AxleGears</summary>
		public const int ITEMID_AXLE_GEARS = 0x1051;

		/// <summary>Price for Gears</summary>
		public const int PRICE_GEARS = 2;
		/// <summary>Item ID for Gears</summary>
		public const int ITEMID_GEARS = 0x1053;

		/// <summary>Price for Hinge</summary>
		public const int PRICE_HINGE = 2;
		/// <summary>Item ID for Hinge</summary>
		public const int ITEMID_HINGE = 0x1055;

		/// <summary>Price for Sextant</summary>
		public const int PRICE_SEXTANT = 13;
		/// <summary>Item ID for Sextant</summary>
		public const int ITEMID_SEXTANT = 0x1057;

		/// <summary>Price for SextantParts</summary>
		public const int PRICE_SEXTANT_PARTS = 5;
		/// <summary>Item ID for SextantParts</summary>
		public const int ITEMID_SEXTANT_PARTS = 0x1059;

		/// <summary>Price for Springs</summary>
		public const int PRICE_SPRINGS = 3;
		/// <summary>Item ID for Springs</summary>
		public const int ITEMID_SPRINGS = 0x105D;

		public const int ITEMID_KEY_1 = 0x100F;
		public const int ITEMID_KEY_2 = 0x1010;
		public const int ITEMID_KEY_3 = 0x1013;

		/// <summary>Price for KeyRing</summary>
		public const int PRICE_KEY_RING = 8;
		/// <summary>Item ID for KeyRing</summary>
		public const int ITEMID_KEY_RING = 0x1010;

		/// <summary>Price for TinkersTools</summary>
		public const int PRICE_TINKERS_TOOLS = 750;
		/// <summary>Quantity range for TinkersTools (min)</summary>
		public const int QTY_TINKERS_TOOLS_MIN = 1;
		/// <summary>Quantity range for TinkersTools (max)</summary>
		public const int QTY_TINKERS_TOOLS_MAX = 10;
		/// <summary>Item ID for TinkersTools</summary>
		public const int ITEMID_TINKERS_TOOLS = 0x1EBC;

		/// <summary>Price for IronIngot</summary>
		public const int PRICE_IRON_INGOT = 5;
		/// <summary>Item ID for IronIngot</summary>
		public const int ITEMID_IRON_INGOT = 0x1BF2;

		/// <summary>Price for SewingKit (Tinker)</summary>
		public const int PRICE_SEWING_KIT_TINKER = 3;
		/// <summary>Price for ButcherKnife</summary>
		public const int PRICE_BUTCHER_KNIFE = 13;
		/// <summary>Item ID for ButcherKnife</summary>
		public const int ITEMID_BUTCHER_KNIFE = 0x13F6;

		/// <summary>Price for Tongs</summary>
		public const int PRICE_TONGS = 13;
		/// <summary>Item ID for Tongs</summary>
		public const int ITEMID_TONGS = 0xFBB;

		/// <summary>Price for SmithHammer</summary>
		public const int PRICE_SMITH_HAMMER = 23;
		/// <summary>Item ID for SmithHammer</summary>
		public const int ITEMID_SMITH_HAMMER = 0x0FB4;
		/// <summary>Price for ThrowingWeapon</summary>
		public const int PRICE_THROWING_WEAPON = 2;
		/// <summary>Quantity range for ThrowingWeapon (min)</summary>
		public const int QTY_THROWING_WEAPON_MIN = 20;
		/// <summary>Quantity range for ThrowingWeapon (max)</summary>
		public const int QTY_THROWING_WEAPON_MAX = 120;
		/// <summary>Item ID for ThrowingWeapon</summary>
		public const int ITEMID_THROWING_WEAPON = 0x52B2;

		/// <summary>Price for WallTorch</summary>
		public const int PRICE_WALL_TORCH = 50;
		/// <summary>Quantity range for WallTorch (min)</summary>
		public const int QTY_WALL_TORCH_MIN = 5;
		/// <summary>Quantity range for WallTorch (max)</summary>
		public const int QTY_WALL_TORCH_MAX = 20;
		/// <summary>Item ID for WallTorch</summary>
		public const int ITEMID_WALL_TORCH = 0xA07;

		/// <summary>Price for ColoredWallTorch</summary>
		public const int PRICE_COLORED_WALL_TORCH = 100;
		/// <summary>Item ID for ColoredWallTorch</summary>
		public const int ITEMID_COLORED_WALL_TORCH = 0x3D89;

		/// <summary>Price for light_dragon_brazier</summary>
		public const int PRICE_LIGHT_DRAGON_BRAZIER = 750;
		/// <summary>Item ID for light_dragon_brazier</summary>
		public const int ITEMID_LIGHT_DRAGON_BRAZIER = 0x194E;

		/// <summary>Price range for PianoAddonDeed (min)</summary>
		public const int PRICE_PIANO_ADDON_DEED_MIN = 100000;
		/// <summary>Price range for PianoAddonDeed (max)</summary>
		public const int PRICE_PIANO_ADDON_DEED_MAX = 200000;
		/// <summary>Item ID for PianoAddonDeed</summary>
		public const int ITEMID_PIANO_ADDON_DEED = 0x14F0;

		/// <summary>Price for PoorMiningHarvester</summary>
		public const int PRICE_POOR_MINING_HARVESTER = 15000;
		/// <summary>Price for StandardMiningHarvester</summary>
		public const int PRICE_STANDARD_MINING_HARVESTER = 75000;
		/// <summary>Price for GoodMiningHarvester</summary>
		public const int PRICE_GOOD_MINING_HARVESTER = 225000;
		/// <summary>Item ID for MiningHarvester</summary>
		public const int ITEMID_MINING_HARVESTER = 0x5484;

		/// <summary>Price for PoorLumberHarvester</summary>
		public const int PRICE_POOR_LUMBER_HARVESTER = 15000;
		/// <summary>Price for StandardLumberHarvester</summary>
		public const int PRICE_STANDARD_LUMBER_HARVESTER = 75000;
		/// <summary>Item ID for LumberHarvester</summary>
		public const int ITEMID_LUMBER_HARVESTER = 0x5486;

		/// <summary>Price for PoorHideHarvester</summary>
		public const int PRICE_POOR_HIDE_HARVESTER = 15000;
		/// <summary>Price for StandardHideHarvester</summary>
		public const int PRICE_STANDARD_HIDE_HARVESTER = 75000;
		/// <summary>Item ID for HideHarvester</summary>
		public const int ITEMID_HIDE_HARVESTER = 0x5487;

		/// <summary>Price for HarvesterRepairKit</summary>
		public const int PRICE_HARVESTER_REPAIR_KIT = 7500;
		/// <summary>Item ID for HarvesterRepairKit</summary>
		public const int ITEMID_HARVESTER_REPAIR_KIT = 0x4C2C;

		/// <summary>Price for CiderBarrel</summary>
		public const int PRICE_CIDER_BARREL = 20000;
		/// <summary>Price for AleBarrel</summary>
		public const int PRICE_ALE_BARREL = 20000;
		/// <summary>Price for LiquorBarrel</summary>
		public const int PRICE_LIQUOR_BARREL = 20000;
		/// <summary>Price for CheesePress</summary>
		public const int PRICE_CHEESE_PRESS = 20000;
		/// <summary>Quantity range for barrels (min)</summary>
		public const int QTY_BARREL_MIN = 1;
		/// <summary>Quantity range for barrels (max)</summary>
		public const int QTY_BARREL_MAX = 4;
		/// <summary>Item ID for barrels</summary>
		public const int ITEMID_BARREL = 0x3DB9;

		/// <summary>Price for DeviceKit</summary>
		public const int PRICE_DEVICE_KIT = 1000;
		/// <summary>Quantity range for DeviceKit (min)</summary>
		public const int QTY_DEVICE_KIT_MIN = 1;
		/// <summary>Quantity range for DeviceKit (max)</summary>
		public const int QTY_DEVICE_KIT_MAX = 2;
		/// <summary>Item ID for DeviceKit</summary>
		public const int ITEMID_DEVICE_KIT = 0x4F86;

		/// <summary>Price for GogglesofScience</summary>
		public const int PRICE_GOGGLES_OF_SCIENCE = 1000;
		/// <summary>Item ID for GogglesofScience</summary>
		public const int ITEMID_GOGGLES_OF_SCIENCE = 0x3172;

		// Sell prices for Tinker items
		public const int SELL_PRICE_SEWING_KIT = 1;
		public const int SELL_PRICE_SCISSORS = 6;
		public const int SELL_PRICE_TONGS = 7;
		public const int SELL_PRICE_KEY = 1;
		public const int SELL_PRICE_CLOCK = 11;
		public const int SELL_PRICE_CLOCK_PARTS = 1;
		public const int SELL_PRICE_AXLE_GEARS = 1;
		public const int SELL_PRICE_GEARS = 1;
		public const int SELL_PRICE_HINGE = 1;
		public const int SELL_PRICE_SEXTANT = 6;
		public const int SELL_PRICE_SEXTANT_PARTS = 2;
		public const int SELL_PRICE_AXLE = 1;
		public const int SELL_PRICE_SPRINGS = 1;
		public const int SELL_PRICE_LOCKPICK = 6;
		public const int SELL_PRICE_SKELETONS_KEY = 10;
		public const int SELL_PRICE_TINKER_TOOLS = 5;
		public const int SELL_PRICE_BOARD = 1;
		public const int SELL_PRICE_LOG = 1;
		public const int SELL_PRICE_HAMMER = 3;
		public const int SELL_PRICE_SMITH_HAMMER = 11;
		public const int SELL_PRICE_BUTCHER_KNIFE = 6;
		public const int SELL_PRICE_THROWING_WEAPON = 1;

		#endregion

		#region Animal Trainer Constants

		/// <summary>Price for Rabbit</summary>
		public const int PRICE_RABBIT = 106;
		/// <summary>Item ID for Rabbit</summary>
		public const int ITEMID_RABBIT = 205;

		/// <summary>Price for Eagle</summary>
		public const int PRICE_EAGLE = 402;
		/// <summary>Item ID for Eagle</summary>
		public const int ITEMID_EAGLE = 5;

		/// <summary>Price for BrownBear</summary>
		public const int PRICE_BROWN_BEAR = 855;
		/// <summary>Item ID for BrownBear</summary>
		public const int ITEMID_BROWN_BEAR = 167;

		/// <summary>Price for GrizzlyBearRiding</summary>
		public const int PRICE_GRIZZLY_BEAR_RIDING = 1767;
		/// <summary>Item ID for GrizzlyBearRiding</summary>
		public const int ITEMID_GRIZZLY_BEAR_RIDING = 212;

		/// <summary>Price for Panther</summary>
		public const int PRICE_PANTHER = 1271;
		/// <summary>Item ID for Panther</summary>
		public const int ITEMID_PANTHER = 214;

		/// <summary>Price for TimberWolf</summary>
		public const int PRICE_TIMBER_WOLF = 768;
		/// <summary>Item ID for TimberWolf</summary>
		public const int ITEMID_TIMBER_WOLF = 225;

		/// <summary>Price for Rat</summary>
		public const int PRICE_RAT = 107;
		/// <summary>Item ID for Rat</summary>
		public const int ITEMID_RAT = 238;

		/// <summary>Price for HitchingPost</summary>
		public const int PRICE_HITCHING_POST = 20000;
		/// <summary>Item ID for HitchingPost</summary>
		public const int ITEMID_HITCHING_POST = 0x14E7;

		/// <summary>Price for TamingBODBook</summary>
		public const int PRICE_TAMING_BOD_BOOK = 10000;
		/// <summary>Item ID for TamingBODBook</summary>
		public const int ITEMID_TAMING_BOD_BOOK = 0x2259;

		/// <summary>Price for PetDyeTub</summary>
		public const int PRICE_PET_DYE_TUB = 1250000;
		/// <summary>Item ID for PetDyeTub</summary>
		public const int ITEMID_PET_DYE_TUB = 0x0012;

		/// <summary>Price for PetTrainer</summary>
		public const int PRICE_PET_TRAINER = 1500;
		/// <summary>Quantity for PetTrainer</summary>
		public const int QTY_PET_TRAINER = 4;
		/// <summary>Item ID for PetTrainer</summary>
		public const int ITEMID_PET_TRAINER = 0x166E;

		/// <summary>Quantity for animals</summary>
		public const int QTY_ANIMALS = 10;

		// Sell prices for Animal Trainer items
		public const int SELL_PRICE_HITCHING_POST = 2500;
		public const int SELL_PRICE_ALIEN_EGG_MIN = 500;
		public const int SELL_PRICE_ALIEN_EGG_MAX = 1000;
		public const int SELL_PRICE_DRAGON_EGG_MIN = 500;
		public const int SELL_PRICE_DRAGON_EGG_MAX = 1000;

		#endregion

		#region Mixologist Constants

		/// <summary>Minimum sell price for elixirs and mixtures</summary>
		public const int SELL_PRICE_ELIXIR_MIXTURE_MIN = 14;
		/// <summary>Maximum sell price for elixirs and mixtures</summary>
		public const int SELL_PRICE_ELIXIR_MIXTURE_MAX = 35;

		#endregion

		#region Healer Constants

		/// <summary>Price for LesserHealPotion</summary>
		public const int PRICE_LESSER_HEAL_POTION = 100;
		/// <summary>Item ID for LesserHealPotion</summary>
		public const int ITEMID_LESSER_HEAL_POTION = 0x25FD;

		/// <summary>Price for Ginseng</summary>
		public const int PRICE_GINSENG = 3;
		/// <summary>Item ID for Ginseng</summary>
		public const int ITEMID_GINSENG = 0xF85;

		/// <summary>Price for Garlic</summary>
		public const int PRICE_GARLIC = 3;
		/// <summary>Item ID for Garlic</summary>
		public const int ITEMID_GARLIC = 0xF84;

		/// <summary>Price for RefreshPotion</summary>
		public const int PRICE_REFRESH_POTION = 100;
		/// <summary>Item ID for RefreshPotion</summary>
		public const int ITEMID_REFRESH_POTION = 0xF0B;

		/// <summary>Price for GraveShovel</summary>
		public const int PRICE_GRAVE_SHOVEL = 12;
		/// <summary>Item ID for GraveShovel</summary>
		public const int ITEMID_GRAVE_SHOVEL = 0xF39;
		/// <summary>Hue for GraveShovel</summary>
		public const int HUE_GRAVE_SHOVEL = 0x966;

		/// <summary>Price for SurgeonsKnife</summary>
		public const int PRICE_SURGEONS_KNIFE = 14;
		/// <summary>Item ID for SurgeonsKnife</summary>
		public const int ITEMID_SURGEONS_KNIFE = 0xEC4;
		/// <summary>Hue for SurgeonsKnife</summary>
		public const int HUE_SURGEONS_KNIFE = 0x1B0;

		/// <summary>Price for HealingDragonJar</summary>
		public const int PRICE_HEALING_DRAGON_JAR = 2500;
		/// <summary>Item ID for HealingDragonJar</summary>
		public const int ITEMID_HEALING_DRAGON_JAR = 0xF39;
		/// <summary>Hue for HealingDragonJar</summary>
		public const int HUE_HEALING_DRAGON_JAR = 0x966;

		// Sell prices for Healer items
		public const int SELL_PRICE_LESSER_HEAL_POTION = 7;
		public const int SELL_PRICE_REFRESH_POTION = 7;
		public const int SELL_PRICE_GARLIC = 2;
		public const int SELL_PRICE_GINSENG = 2;
		public const int SELL_PRICE_SURGEONS_KNIFE = 7;
		public const int SELL_PRICE_FIRST_AID_KIT_MIN = 100;
		public const int SELL_PRICE_FIRST_AID_KIT_MAX = 250;

		#endregion

		#region Herbalist Constants

		/// <summary>Price for Ginseng</summary>
		public const int PRICE_GINSENG_HERBALIST = 3;
		/// <summary>Item ID for Ginseng</summary>
		public const int ITEMID_GINSENG_HERBALIST = 0xF85;

		/// <summary>Price for Garlic</summary>
		public const int PRICE_GARLIC_HERBALIST = 3;
		/// <summary>Item ID for Garlic</summary>
		public const int ITEMID_GARLIC_HERBALIST = 0xF84;

		/// <summary>Price for MandrakeRoot</summary>
		public const int PRICE_MANDRAKE_ROOT = 3;
		/// <summary>Item ID for MandrakeRoot</summary>
		public const int ITEMID_MANDRAKE_ROOT = 0xF86;

		/// <summary>Price for Nightshade</summary>
		public const int PRICE_NIGHTSHADE = 3;
		/// <summary>Item ID for Nightshade</summary>
		public const int ITEMID_NIGHTSHADE = 0xF88;

		/// <summary>Price for Bloodmoss</summary>
		public const int PRICE_BLOODMOSS = 5;
		/// <summary>Item ID for Bloodmoss</summary>
		public const int ITEMID_BLOODMOSS = 0xF7B;

		public const int ITEMID_MORTAR_PESTLE = 0x4CE9;

		/// <summary>Price for GardenTool</summary>
		public const int PRICE_GARDEN_TOOL = 12;
		/// <summary>Item ID for GardenTool</summary>
		public const int ITEMID_GARDEN_TOOL = 0xDFD;
		/// <summary>Hue for GardenTool</summary>
		public const int HUE_GARDEN_TOOL = 0x84F;

		/// <summary>Price for HerbalistCauldron</summary>
		public const int PRICE_HERBALIST_CAULDRON = 247;
		/// <summary>Item ID for HerbalistCauldron</summary>
		public const int ITEMID_HERBALIST_CAULDRON = 0x2676;

		public const int ITEMID_MIXING_SPOON = 0x1E27;
		/// <summary>Price for CBookDruidicHerbalism</summary>
		public const int PRICE_CBOOK_DRUIDIC_HERBALISM = 50;
		/// <summary>Item ID for CBookDruidicHerbalism</summary>
		public const int ITEMID_CBOOK_DRUIDIC_HERBALISM = 0x2D50;

		public const int ITEMID_ALCHEMY_TUB = 0x126A;

		/// <summary>Price range for HangingPlant (min)</summary>
		public const int PRICE_HANGING_PLANT_MIN = 5000;
		/// <summary>Price range for HangingPlant (max)</summary>
		public const int PRICE_HANGING_PLANT_MAX = 10000;

		/// <summary>Item ID for HangingPlantA</summary>
		public const int ITEMID_HANGING_PLANT_A = 0x113F;
		/// <summary>Item ID for HangingPlantB</summary>
		public const int ITEMID_HANGING_PLANT_B = 0x1151;
		/// <summary>Item ID for HangingPlantC</summary>
		public const int ITEMID_HANGING_PLANT_C = 0x1164;

		// Sell prices for Herbalist items
		public const int SELL_PRICE_BLOODMOSS = 3;
		public const int SELL_PRICE_MANDRAKE_ROOT = 2;
		public const int SELL_PRICE_GARLIC_HERBALIST = 2;
		public const int SELL_PRICE_GINSENG_HERBALIST = 2;
		public const int SELL_PRICE_NIGHTSHADE = 2;
		public const int SELL_PRICE_JAR_HERBALIST = 3;
		public const int SELL_PRICE_GARDEN_TOOL = 6;
		public const int SELL_PRICE_HERBALIST_CAULDRON = 123;

		// Sell price ranges for Herbalist items
		public const int SELL_PRICE_PLANT_HERBALISM_MIN = 2;
		public const int SELL_PRICE_PLANT_HERBALISM_MAX = 6;
		public const int SELL_PRICE_HOME_PLANTS_MIN = 100;
		public const int SELL_PRICE_HOME_PLANTS_MAX = 300;
		public const int SELL_PRICE_SPECIAL_SEAWEED_MIN = 15;
		public const int SELL_PRICE_SPECIAL_SEAWEED_MAX = 35;
		public const int SELL_PRICE_HANGING_PLANT_MIN = 10;
		public const int SELL_PRICE_HANGING_PLANT_MAX = 100;
		public const int SELL_PRICE_ALCHEMY_TUB_MIN = 200;
		public const int SELL_PRICE_ALCHEMY_TUB_MAX = 500;

		#endregion

		#region Veterinarian Constants

		/// <summary>Price for Bandage</summary>
		public const int PRICE_BANDAGE_VETERINARIAN = 2;
		/// <summary>Item ID for Bandage</summary>
		public const int ITEMID_BANDAGE_VETERINARIAN = 0xE21;

		/// <summary>Price for LesserHealPotion</summary>
		public const int PRICE_LESSER_HEAL_POTION_VETERINARIAN = 100;
		/// <summary>Item ID for LesserHealPotion</summary>
		public const int ITEMID_LESSER_HEAL_POTION_VETERINARIAN = 0x25FD;

		/// <summary>Price for Ginseng</summary>
		public const int PRICE_GINSENG_VETERINARIAN = 3;
		/// <summary>Item ID for Ginseng</summary>
		public const int ITEMID_GINSENG_VETERINARIAN = 0xF85;

		/// <summary>Price for Garlic</summary>
		public const int PRICE_GARLIC_VETERINARIAN = 3;
		/// <summary>Item ID for Garlic</summary>
		public const int ITEMID_GARLIC_VETERINARIAN = 0xF84;

		/// <summary>Price for RefreshPotion</summary>
		public const int PRICE_REFRESH_POTION_VETERINARIAN = 100;
		/// <summary>Item ID for RefreshPotion</summary>
		public const int ITEMID_REFRESH_POTION_VETERINARIAN = 0xF0B;

		/// <summary>Quantity range for Bandage (min)</summary>
		public const int QTY_BANDAGE_VETERINARIAN_MIN = 10;
		/// <summary>Quantity range for Bandage (max)</summary>
		public const int QTY_BANDAGE_VETERINARIAN_MAX = 150;

		/// <summary>Quantity range for HitchingPost (min)</summary>
		public const int QTY_HITCHING_POST_MIN = 1;
		/// <summary>Quantity range for HitchingPost (max)</summary>
		public const int QTY_HITCHING_POST_MAX = 3;

		// Sell prices for Veterinarian items
		public const int SELL_PRICE_LESSER_HEAL_POTION_VETERINARIAN = 7;
		public const int SELL_PRICE_REFRESH_POTION_VETERINARIAN = 7;
		public const int SELL_PRICE_GARLIC_VETERINARIAN = 2;
		public const int SELL_PRICE_GINSENG_VETERINARIAN = 2;
		public const int SELL_PRICE_FIRST_AID_KIT_VETERINARIAN_MIN = 100;
		public const int SELL_PRICE_FIRST_AID_KIT_VETERINARIAN_MAX = 250;

		// Sell price ranges for Veterinarian items

		#endregion

		#region Holy Mage Constants

		/// <summary>Price for Spellbook</summary>
		public const int PRICE_SPELLBOOK_HOLY_MAGE = 18;
		/// <summary>Item ID for Spellbook</summary>
		public const int ITEMID_SPELLBOOK_HOLY_MAGE = 0xEFA;

		/// <summary>Price for ScribesPen</summary>
		public const int PRICE_SCRIBES_PEN = 8;
		/// <summary>Item ID for ScribesPen</summary>
		public const int ITEMID_SCRIBES_PEN = 0x2051;

		public const int PRICE_BLANK_SCROLL_HOLY_MAGE = 5;
		public const int PRICE_RECALL_RUNE_HOLY_MAGE = 100;
		/// <summary>Price for BlackPearl</summary>
		public const int PRICE_BLACK_PEARL = 5;
		/// <summary>Item ID for BlackPearl</summary>
		public const int ITEMID_BLACK_PEARL = 0x266F;

		/// <summary>Price for Bloodmoss</summary>
		public const int PRICE_BLOODMOSS_HOLY_MAGE = 5;
		/// <summary>Item ID for Bloodmoss</summary>
		public const int ITEMID_BLOODMOSS_HOLY_MAGE = 0xF7B;

		/// <summary>Price for Garlic</summary>
		public const int PRICE_GARLIC_HOLY_MAGE = 3;
		/// <summary>Item ID for Garlic</summary>
		public const int ITEMID_GARLIC_HOLY_MAGE = 0xF84;

		/// <summary>Price for Ginseng</summary>
		public const int PRICE_GINSENG_HOLY_MAGE = 3;
		/// <summary>Item ID for Ginseng</summary>
		public const int ITEMID_GINSENG_HOLY_MAGE = 0xF85;

		/// <summary>Price for MandrakeRoot</summary>
		public const int PRICE_MANDRAKE_ROOT_HOLY_MAGE = 3;
		/// <summary>Item ID for MandrakeRoot</summary>
		public const int ITEMID_MANDRAKE_ROOT_HOLY_MAGE = 0xF86;

		/// <summary>Price for Nightshade</summary>
		public const int PRICE_NIGHTSHADE_HOLY_MAGE = 3;
		/// <summary>Item ID for Nightshade</summary>
		public const int ITEMID_NIGHTSHADE_HOLY_MAGE = 0xF88;

		/// <summary>Price for SpidersSilk</summary>
		public const int PRICE_SPIDERS_SILK = 3;
		/// <summary>Item ID for SpidersSilk</summary>
		public const int ITEMID_SPIDERS_SILK = 0xF8D;

		/// <summary>Price for SulfurousAsh</summary>
		public const int PRICE_SULFUROUS_ASH = 3;
		/// <summary>Item ID for SulfurousAsh</summary>
		public const int ITEMID_SULFUROUS_ASH = 0xF8C;

		/// <summary>Price for reagents_magic_jar1</summary>
		public const int PRICE_REAGENTS_MAGIC_JAR1 = 2000;
		/// <summary>Item ID for reagents_magic_jar1</summary>
		public const int ITEMID_REAGENTS_MAGIC_JAR1 = 0x1007;

		/// <summary>Price for WizardStaff</summary>
		public const int PRICE_WIZARD_STAFF = 40;
		/// <summary>Item ID for WizardStaff</summary>
		public const int ITEMID_WIZARD_STAFF = 0x0908;
		/// <summary>Hue for WizardStaff</summary>
		public const int HUE_WIZARD_STAFF = 0xB3A;

		/// <summary>Price for WizardStick</summary>
		public const int PRICE_WIZARD_STICK = 38;
		/// <summary>Item ID for WizardStick</summary>
		public const int ITEMID_WIZARD_STICK = 0xDF2;
		/// <summary>Hue for WizardStick</summary>
		public const int HUE_WIZARD_STICK = 0xB3A;

		/// <summary>Price for MageEye</summary>
		public const int PRICE_MAGE_EYE = 2;
		/// <summary>Item ID for MageEye</summary>
		public const int ITEMID_MAGE_EYE = 0xF19;
		/// <summary>Hue for MageEye</summary>
		public const int HUE_MAGE_EYE = 0xB78;

		public const int ITEMID_SCROLL_BASE = 0x1F2E;

		// Sell prices for Holy Mage items
		public const int SELL_PRICE_MAGIC_TALISMAN_MIN = 50;
		public const int SELL_PRICE_MAGIC_TALISMAN_MAX = 100;
		public const int SELL_PRICE_BLACK_PEARL_HOLY_MAGE = 3;
		public const int SELL_PRICE_BLOODMOSS_HOLY_MAGE = 3;
		public const int SELL_PRICE_MANDRAKE_ROOT_HOLY_MAGE = 2;
		public const int SELL_PRICE_GARLIC_HOLY_MAGE = 2;
		public const int SELL_PRICE_GINSENG_HOLY_MAGE = 2;
		public const int SELL_PRICE_NIGHTSHADE_HOLY_MAGE = 2;
		public const int SELL_PRICE_SPIDERS_SILK = 2;
		public const int SELL_PRICE_SPELLBOOK_HOLY_MAGE = 9;
		public const int SELL_PRICE_MYSTICAL_PEARL = 250;
		public const int SELL_PRICE_WIZARD_STAFF = 20;
		public const int SELL_PRICE_WIZARD_STICK = 19;
		public const int SELL_PRICE_MAGE_EYE = 1;

		// Sell price calculation for regular scrolls
		public const int SELL_PRICE_SCROLL_BASE = 2;
		public const int SELL_PRICE_SCROLL_MULTIPLIER = 5;

		// Sell price ranges for magic staves
		public const int SELL_PRICE_MAGIC_STAFF_1ST_MIN = 10;
		public const int SELL_PRICE_MAGIC_STAFF_1ST_MAX = 20;
		public const int SELL_PRICE_MAGIC_STAFF_2ND_MIN = 20;
		public const int SELL_PRICE_MAGIC_STAFF_2ND_MAX = 40;
		public const int SELL_PRICE_MAGIC_STAFF_3RD_MIN = 30;
		public const int SELL_PRICE_MAGIC_STAFF_3RD_MAX = 60;
		public const int SELL_PRICE_MAGIC_STAFF_4TH_MIN = 40;
		public const int SELL_PRICE_MAGIC_STAFF_4TH_MAX = 80;
		public const int SELL_PRICE_MAGIC_STAFF_5TH_MIN = 50;
		public const int SELL_PRICE_MAGIC_STAFF_5TH_MAX = 100;
		public const int SELL_PRICE_MAGIC_STAFF_6TH_MIN = 60;
		public const int SELL_PRICE_MAGIC_STAFF_6TH_MAX = 120;
		public const int SELL_PRICE_MAGIC_STAFF_7TH_MIN = 70;
		public const int SELL_PRICE_MAGIC_STAFF_7TH_MAX = 140;
		public const int SELL_PRICE_MAGIC_STAFF_8TH_MIN = 80;
		public const int SELL_PRICE_MAGIC_STAFF_8TH_MAX = 160;

		// Sell price ranges for special items
		public const int SELL_PRICE_TOME_OF_WANDS_MIN = 100;
		public const int SELL_PRICE_TOME_OF_WANDS_MAX = 400;
		public const int SELL_PRICE_MY_SPELLBOOK_MIN = 250;
		public const int SELL_PRICE_MY_SPELLBOOK_MAX = 1000;

		#endregion

		#region House Deed Constants

		// SBHouseDeed is currently empty - no items sold or bought

		#endregion

		#region Inn Keeper Constants

		// Beverage prices
		public const int PRICE_BEVERAGE_BOTTLE_ALE = 70;
		public const int PRICE_BEVERAGE_BOTTLE_WINE = 70;
		public const int PRICE_BEVERAGE_BOTTLE_LIQUOR = 70;
		public const int PRICE_PITCHER_ALE = 110;
		public const int PRICE_PITCHER_CIDER = 110;
		public const int PRICE_PITCHER_LIQUOR = 110;
		public const int PRICE_PITCHER_WINE = 110;

		// Beverage item IDs

		// Food prices
		public const int PRICE_CHICKEN_LEG = 90;
		public const int PRICE_RIBS = 90;
		public const int PRICE_APPLE_PIE = 270;

		// Food item IDs
		public const int ITEMID_BREAD_LOAF = 0x103B;
		public const int ITEMID_CHICKEN_LEG = 0x1608;
		public const int ITEMID_RIBS = 0x9F2;

		// Bowl food prices

		// Bowl food item IDs

		// Fruit prices
		public const int PRICE_PEACH = 30;
		public const int PRICE_PEAR = 30;
		public const int PRICE_GRAPES = 30;
		public const int PRICE_APPLE = 30;
		public const int PRICE_BANANA = 20;

		// Fruit item IDs
		public const int ITEMID_PEACH = 0x9D2;
		public const int ITEMID_PEAR = 0x994;
		public const int ITEMID_GRAPES = 0x9D1;
		public const int ITEMID_APPLE = 0x9D0;
		public const int ITEMID_BANANA = 0x171F;

		// Other items prices
		public const int PRICE_CANDLE = 6;
		public const int PRICE_CONTRACT_OF_EMPLOYMENT = 1252;

		// Other items item IDs
		public const int ITEMID_CONTRACT_OF_EMPLOYMENT = 0x14F0;

		// Other items hues
		public const int HUE_HENCHMAN_FIGHTER = 0xB96;
		public const int HUE_HENCHMAN_ARCHER = 0xB96;
		public const int HUE_HENCHMAN_WIZARD = 0xB96;

		// Sell prices for Inn Keeper items
		public const int SELL_PRICE_CONTRACT_OF_EMPLOYMENT = 626;

		#endregion

		#region Necro Mage Constants

		/// <summary>Price for BatWing</summary>
		public const int PRICE_BAT_WING_NECRO = 3;
		/// <summary>Item ID for BatWing</summary>
		public const int ITEMID_BAT_WING = 0xF78;

		/// <summary>Price for DaemonBlood</summary>
		public const int PRICE_DAEMON_BLOOD = 6;
		/// <summary>Item ID for DaemonBlood</summary>
		public const int ITEMID_DAEMON_BLOOD = 0xF7D;

		public const int ITEMID_PIG_IRON = 0xF8A;

		/// <summary>Price for NoxCrystal</summary>
		public const int PRICE_NOX_CRYSTAL = 6;
		/// <summary>Item ID for NoxCrystal</summary>
		public const int ITEMID_NOX_CRYSTAL = 0xF8E;

		/// <summary>Price for GraveDust</summary>
		public const int PRICE_GRAVE_DUST = 3;
		/// <summary>Item ID for GraveDust</summary>
		public const int ITEMID_GRAVE_DUST = 0xF8F;

		/// <summary>Price for BloodOathScroll</summary>
		public const int PRICE_BLOOD_OATH_SCROLL = 25;
		/// <summary>Item ID for BloodOathScroll</summary>
		public const int ITEMID_BLOOD_OATH_SCROLL = 0x2263;

		/// <summary>Price for CorpseSkinScroll</summary>
		public const int PRICE_CORPSE_SKIN_SCROLL = 28;
		/// <summary>Item ID for CorpseSkinScroll</summary>
		public const int ITEMID_CORPSE_SKIN_SCROLL = 0x2263;

		/// <summary>Price for CurseWeaponScroll</summary>
		public const int PRICE_CURSE_WEAPON_SCROLL = 12;
		/// <summary>Item ID for CurseWeaponScroll</summary>
		public const int ITEMID_CURSE_WEAPON_SCROLL = 0x2263;

		/// <summary>Price for PolishBoneBrush</summary>
		public const int PRICE_POLISH_BONE_BRUSH = 12;
		/// <summary>Item ID for PolishBoneBrush</summary>
		public const int ITEMID_POLISH_BONE_BRUSH = 0x1371;

		public const int ITEMID_MIXING_CAULDRON = 0x269C;

		/// <summary>Price for MixingSpoon</summary>
		public const int PRICE_MIXING_SPOON_NECRO = 34;
		/// <summary>Item ID for MixingSpoon</summary>
		public const int ITEMID_MIXING_SPOON_NECRO = 0x1E27;
		/// <summary>Hue for MixingSpoon</summary>
		public const int HUE_MIXING_SPOON_NECRO = 0x979;

		/// <summary>Price for CBookNecroticAlchemy</summary>
		public const int PRICE_CBOOK_NECROTIC_ALCHEMY = 50;
		/// <summary>Item ID for CBookNecroticAlchemy</summary>
		public const int ITEMID_CBOOK_NECROTIC_ALCHEMY = 0x2253;
		/// <summary>Hue for CBookNecroticAlchemy</summary>
		public const int HUE_CBOOK_NECROTIC_ALCHEMY = 0x4AA;

		/// <summary>Quantity range for reagents (min)</summary>
		public const int QTY_REAGENTS_NECRO_MIN = 20;

		// Sell prices for Necro Mage items
		public const int SELL_PRICE_BAT_WING = 1;
		public const int SELL_PRICE_DAEMON_BLOOD = 3;
		public const int SELL_PRICE_PIG_IRON = 2;
		public const int SELL_PRICE_NOX_CRYSTAL = 3;
		public const int SELL_PRICE_GRAVE_DUST = 1;
		public const int SELL_PRICE_EXORCISM_SCROLL = 1;
		public const int SELL_PRICE_ANIMATE_DEAD_SCROLL = 26;
		public const int SELL_PRICE_BLOOD_OATH_SCROLL = 26;
		public const int SELL_PRICE_CORPSE_SKIN_SCROLL = 26;
		public const int SELL_PRICE_CURSE_WEAPON_SCROLL = 26;
		public const int SELL_PRICE_EVIL_OMEN_SCROLL = 26;
		public const int SELL_PRICE_PAIN_SPIKE_SCROLL = 26;
		public const int SELL_PRICE_SUMMON_FAMILIAR_SCROLL = 26;
		public const int SELL_PRICE_HORRIFIC_BEAST_SCROLL = 27;
		public const int SELL_PRICE_MIND_ROT_SCROLL = 39;
		public const int SELL_PRICE_POISON_STRIKE_SCROLL = 39;
		public const int SELL_PRICE_WRAITH_FORM_SCROLL = 51;
		public const int SELL_PRICE_LICH_FORM_SCROLL = 64;
		public const int SELL_PRICE_STRANGLE_SCROLL = 64;
		public const int SELL_PRICE_WITHER_SCROLL = 64;
		public const int SELL_PRICE_VAMPIRIC_EMBRACE_SCROLL = 101;
		public const int SELL_PRICE_VENGEFUL_SPIRIT_SCROLL = 114;
		public const int SELL_PRICE_POLISH_BONE_BRUSH = 6;
		public const int SELL_PRICE_POLISHED_SKULL = 3;
		public const int SELL_PRICE_POLISHED_BONE = 3;
		public const int SELL_PRICE_MIXING_CAULDRON_NECRO = 123;
		public const int SELL_PRICE_MIXING_SPOON_NECRO = 17;
		public const int SELL_PRICE_SURGEONS_KNIFE_NECRO = 7;
		public const int SELL_PRICE_WOODEN_COFFIN = 25;
		public const int SELL_PRICE_WOODEN_CASKET = 25;
		public const int SELL_PRICE_STONE_COFFIN = 45;
		public const int SELL_PRICE_STONE_CASKET = 45;
		public const int SELL_PRICE_DRACOLICH_SKULL_MIN = 500;
		public const int SELL_PRICE_DRACOLICH_SKULL_MAX = 1000;
		public const int SELL_PRICE_WIZARD_STAFF_NECRO = 20;
		public const int SELL_PRICE_WIZARD_STICK_NECRO = 19;
		public const int SELL_PRICE_MAGE_EYE_NECRO = 1;

		// Sell price ranges for body parts and corpses
		public const int SELL_PRICE_CORPSE_SAILOR_MIN = 50;
		public const int SELL_PRICE_CORPSE_SAILOR_MAX = 300;
		public const int SELL_PRICE_CORPSE_CHEST_MIN = 50;
		public const int SELL_PRICE_CORPSE_CHEST_MAX = 300;
		public const int SELL_PRICE_BURIED_BODY_MIN = 50;
		public const int SELL_PRICE_BURIED_BODY_MAX = 300;
		public const int SELL_PRICE_BONE_CONTAINER_MIN = 50;
		public const int SELL_PRICE_BONE_CONTAINER_MAX = 300;
		public const int SELL_PRICE_LEFT_LEG_MIN = 5;
		public const int SELL_PRICE_LEFT_LEG_MAX = 10;
		public const int SELL_PRICE_RIGHT_LEG_MIN = 5;
		public const int SELL_PRICE_RIGHT_LEG_MAX = 10;
		public const int SELL_PRICE_TASTY_HEART_MIN = 10;
		public const int SELL_PRICE_TASTY_HEART_MAX = 20;
		public const int SELL_PRICE_BODY_PART_MIN = 30;
		public const int SELL_PRICE_BODY_PART_MAX = 90;
		public const int SELL_PRICE_HEAD_MIN = 10;
		public const int SELL_PRICE_HEAD_MAX = 20;
		public const int SELL_PRICE_LEFT_ARM_MIN = 5;
		public const int SELL_PRICE_LEFT_ARM_MAX = 10;
		public const int SELL_PRICE_RIGHT_ARM_MIN = 5;
		public const int SELL_PRICE_RIGHT_ARM_MAX = 10;
		public const int SELL_PRICE_TORSO_MIN = 5;
		public const int SELL_PRICE_TORSO_MAX = 10;
		public const int SELL_PRICE_BONE_MIN = 5;
		public const int SELL_PRICE_BONE_MAX = 10;
		public const int SELL_PRICE_RIB_CAGE_MIN = 5;
		public const int SELL_PRICE_RIB_CAGE_MAX = 10;
		public const int SELL_PRICE_BONE_PILE_MIN = 5;
		public const int SELL_PRICE_BONE_PILE_MAX = 10;
		public const int SELL_PRICE_BONES_MIN = 5;
		public const int SELL_PRICE_BONES_MAX = 10;
		public const int SELL_PRICE_GRAVE_CHEST_MIN = 100;
		public const int SELL_PRICE_GRAVE_CHEST_MAX = 500;
		public const int SELL_PRICE_ALCHEMY_TUB_NECRO_MIN = 200;
		public const int SELL_PRICE_ALCHEMY_TUB_NECRO_MAX = 500;
		public const int SELL_PRICE_SKULL_MINOTAUR_MIN = 50;
		public const int SELL_PRICE_SKULL_MINOTAUR_MAX = 150;
		public const int SELL_PRICE_SKULL_WYRM_MIN = 200;
		public const int SELL_PRICE_SKULL_WYRM_MAX = 400;
		public const int SELL_PRICE_SKULL_GREAT_DRAGON_MIN = 300;
		public const int SELL_PRICE_SKULL_GREAT_DRAGON_MAX = 600;
		public const int SELL_PRICE_SKULL_DRAGON_MIN = 100;
		public const int SELL_PRICE_SKULL_DRAGON_MAX = 300;
		public const int SELL_PRICE_SKULL_DEMON_MIN = 100;
		public const int SELL_PRICE_SKULL_DEMON_MAX = 300;
		public const int SELL_PRICE_SKULL_GIANT_MIN = 100;
		public const int SELL_PRICE_SKULL_GIANT_MAX = 300;

		#endregion

		#region Necromancer Constants

		/// <summary>Price for NecromancerSpellbook</summary>
		public const int PRICE_NECROMANCER_SPELLBOOK = 115;
		/// <summary>Item ID for NecromancerSpellbook</summary>
		public const int ITEMID_NECROMANCER_SPELLBOOK = 0x2253;

		/// <summary>Price for NecroSkinPotion</summary>
		public const int PRICE_NECRO_SKIN_POTION = 1000;
		/// <summary>Item ID for NecroSkinPotion</summary>
		public const int ITEMID_NECRO_SKIN_POTION = 0x1006;

		/// <summary>Price for BookofDead</summary>
		public const int PRICE_BOOK_OF_DEAD = 25000;
		/// <summary>Item ID for BookofDead</summary>
		public const int ITEMID_BOOK_OF_DEAD = 0x1C11;
		/// <summary>Hue for BookofDead</summary>
		public const int HUE_BOOK_OF_DEAD = 2500;

		/// <summary>Price for DarkHeart</summary>
		public const int PRICE_DARK_HEART = 500;
		/// <summary>Item ID for DarkHeart</summary>
		public const int ITEMID_DARK_HEART = 0xF91;
		/// <summary>Hue for DarkHeart</summary>
		public const int HUE_DARK_HEART = 0x386;

		/// <summary>Price for NecroHorse</summary>
		public const int PRICE_NECRO_HORSE = 500000;
		/// <summary>Item ID for NecroHorse</summary>
		public const int ITEMID_NECRO_HORSE = 0x2617;
		/// <summary>Hue for NecroHorse</summary>
		public const int HUE_NECRO_HORSE = 0xB97;

		/// <summary>Price for DaemonMount</summary>
		public const int PRICE_DAEMON_MOUNT = 350000;
		/// <summary>Item ID for DaemonMount</summary>
		public const int ITEMID_DAEMON_MOUNT = 11669;
		/// <summary>Hue for DaemonMount</summary>
		public const int HUE_DAEMON_MOUNT = 0x4AA;

		/// <summary>Price for BagOfNecroReagents</summary>
		public const int PRICE_BAG_OF_NECRO_REAGENTS = 500;
		/// <summary>Item ID for BagOfNecroReagents</summary>
		public const int ITEMID_BAG_OF_NECRO_REAGENTS = 0xE76;

		/// <summary>Price for BoneGrinder</summary>
		public const int PRICE_BONE_GRINDER = 25000;
		/// <summary>Item ID for BoneGrinder</summary>
		public const int ITEMID_BONE_GRINDER = 0x3DB9;

		public const int QTY_BAG_OF_NECRO_REAGENTS = 10;
		/// <summary>Quantity range for BoneGrinder (min)</summary>
		public const int QTY_BONE_GRINDER_MIN = 1;
		/// <summary>Quantity range for BoneGrinder (max)</summary>
		public const int QTY_BONE_GRINDER_MAX = 4;

		// Sell prices for Necromancer items
		public const int SELL_PRICE_NECROMANCER_SPELLBOOK = 55;
		public const int SELL_PRICE_DEATH_KNIGHT_SPELLBOOK_MIN = 100;
		public const int SELL_PRICE_DEATH_KNIGHT_SPELLBOOK_MAX = 300;
		public const int SELL_PRICE_BONE_CONTAINER_NECRO = 250;
		public const int SELL_PRICE_MIXING_CAULDRON_NECROMANCER = 123;
		public const int SELL_PRICE_MIXING_SPOON_NECROMANCER = 17;
		public const int SELL_PRICE_SURGEONS_KNIFE_NECROMANCER = 7;
		public const int SELL_PRICE_WOODEN_COFFIN_NECROMANCER = 205;
		public const int SELL_PRICE_WOODEN_CASKET_NECROMANCER = 250;
		public const int SELL_PRICE_STONE_COFFIN_NECROMANCER = 450;
		public const int SELL_PRICE_STONE_CASKET_NECROMANCER = 450;
		public const int SELL_PRICE_DEMON_PRISON_MIN = 500;
		public const int SELL_PRICE_DEMON_PRISON_MAX = 1000;
		public const int SELL_PRICE_DRACOLICH_SKULL_NECROMANCER_MIN = 500;
		public const int SELL_PRICE_DRACOLICH_SKULL_NECROMANCER_MAX = 1000;
		public const int SELL_PRICE_WIZARD_STAFF_NECROMANCER = 20;
		public const int SELL_PRICE_WIZARD_STICK_NECROMANCER = 19;
		public const int SELL_PRICE_MAGE_EYE_NECROMANCER = 1;
		public const int SELL_PRICE_MY_NECROMANCER_SPELLBOOK_MIN = 250;
		public const int SELL_PRICE_MY_NECROMANCER_SPELLBOOK_MAX = 1000;

		// Sell price ranges for body parts and corpses (shared with NecroMage)
		public const int SELL_PRICE_CORPSE_SAILOR_NECROMANCER_MIN = 50;
		public const int SELL_PRICE_CORPSE_SAILOR_NECROMANCER_MAX = 300;
		public const int SELL_PRICE_CORPSE_CHEST_NECROMANCER_MIN = 50;
		public const int SELL_PRICE_CORPSE_CHEST_NECROMANCER_MAX = 300;
		public const int SELL_PRICE_BODY_PART_NECROMANCER_MIN = 30;
		public const int SELL_PRICE_BODY_PART_NECROMANCER_MAX = 90;
		public const int SELL_PRICE_BURIED_BODY_NECROMANCER_MIN = 50;
		public const int SELL_PRICE_BURIED_BODY_NECROMANCER_MAX = 300;
		public const int SELL_PRICE_BONE_CONTAINER_NECROMANCER_MIN = 50;
		public const int SELL_PRICE_BONE_CONTAINER_NECROMANCER_MAX = 300;
		public const int SELL_PRICE_LEFT_LEG_NECROMANCER_MIN = 5;
		public const int SELL_PRICE_LEFT_LEG_NECROMANCER_MAX = 10;
		public const int SELL_PRICE_RIGHT_LEG_NECROMANCER_MIN = 5;
		public const int SELL_PRICE_RIGHT_LEG_NECROMANCER_MAX = 10;
		public const int SELL_PRICE_TASTY_HEART_NECROMANCER_MIN = 10;
		public const int SELL_PRICE_TASTY_HEART_NECROMANCER_MAX = 20;
		public const int SELL_PRICE_HEAD_NECROMANCER_MIN = 10;
		public const int SELL_PRICE_HEAD_NECROMANCER_MAX = 20;
		public const int SELL_PRICE_LEFT_ARM_NECROMANCER_MIN = 5;
		public const int SELL_PRICE_LEFT_ARM_NECROMANCER_MAX = 10;
		public const int SELL_PRICE_RIGHT_ARM_NECROMANCER_MIN = 5;
		public const int SELL_PRICE_RIGHT_ARM_NECROMANCER_MAX = 10;
		public const int SELL_PRICE_TORSO_NECROMANCER_MIN = 5;
		public const int SELL_PRICE_TORSO_NECROMANCER_MAX = 10;
		public const int SELL_PRICE_BONE_NECROMANCER_MIN = 5;
		public const int SELL_PRICE_BONE_NECROMANCER_MAX = 10;
		public const int SELL_PRICE_RIB_CAGE_NECROMANCER_MIN = 5;
		public const int SELL_PRICE_RIB_CAGE_NECROMANCER_MAX = 10;
		public const int SELL_PRICE_BONE_PILE_NECROMANCER_MIN = 5;
		public const int SELL_PRICE_BONE_PILE_NECROMANCER_MAX = 10;
		public const int SELL_PRICE_BONES_NECROMANCER_MIN = 5;
		public const int SELL_PRICE_BONES_NECROMANCER_MAX = 10;
		public const int SELL_PRICE_GRAVE_CHEST_NECROMANCER_MIN = 1000;
		public const int SELL_PRICE_GRAVE_CHEST_NECROMANCER_MAX = 5000;
		public const int SELL_PRICE_ALCHEMY_TUB_NECROMANCER_MIN = 200;
		public const int SELL_PRICE_ALCHEMY_TUB_NECROMANCER_MAX = 500;
		public const int SELL_PRICE_SKULL_DRAGON_NECROMANCER_MIN = 100;
		public const int SELL_PRICE_SKULL_DRAGON_NECROMANCER_MAX = 300;
		public const int SELL_PRICE_SKULL_DEMON_NECROMANCER_MIN = 100;
		public const int SELL_PRICE_SKULL_DEMON_NECROMANCER_MAX = 300;
		public const int SELL_PRICE_SKULL_GIANT_NECROMANCER_MIN = 100;
		public const int SELL_PRICE_SKULL_GIANT_NECROMANCER_MAX = 300;

		#endregion

		#region Mage Constants

		/// <summary>Price for Spellbook</summary>
		public const int PRICE_SPELLBOOK = 66;
		/// <summary>Price for WizardStick</summary>
		public const int PRICE_WIZARD_STICK_MAGE = 118;
		/// <summary>Item ID for WizardStick</summary>
		public const int ITEMID_WIZARD_STICK_MAGE = 0xDF2;
		/// <summary>Hue for WizardStick</summary>
		public const int HUE_WIZARD_STICK_MAGE = 0xB3A;
		/// <summary>Price for BagOfReagents</summary>
		public const int PRICE_BAG_OF_REAGENTS = 1088;
		/// <summary>Item ID for BagOfReagents</summary>
		public const int ITEMID_BAG_OF_REAGENTS = 0xE76;

		/// <summary>Price for RecallRune</summary>
		public const int PRICE_RECALL_RUNE_MAGE = 8;
		/// <summary>Item ID for RecallRune</summary>
		public const int ITEMID_RECALL_RUNE_MAGE = 0x1F14;

		/// <summary>Price for BlackPearl</summary>
		public const int PRICE_BLACK_PEARL_MAGE = 6;
		/// <summary>Price for Bloodmoss</summary>
		public const int PRICE_BLOODMOSS_MAGE = 5;
		/// <summary>Price for Garlic</summary>
		public const int PRICE_GARLIC_MAGE = 3;
		/// <summary>Price for Ginseng</summary>
		public const int PRICE_GINSENG_MAGE = 4;
		/// <summary>Price for MandrakeRoot</summary>
		public const int PRICE_MANDRAKE_ROOT_MAGE = 4;
		/// <summary>Price for Nightshade</summary>
		public const int PRICE_NIGHTSHADE_MAGE = 3;
		/// <summary>Price for SpidersSilk</summary>
		public const int PRICE_SPIDERS_SILK_MAGE = 3;
		/// <summary>Price for SulfurousAsh</summary>
		public const int PRICE_SULFUROUS_ASH_MAGE = 4;
		/// <summary>Price for MageEye</summary>
		public const int PRICE_MAGE_EYE_MAGE = 5;
		/// <summary>Item ID for MageEye</summary>
		public const int ITEMID_MAGE_EYE_MAGE = 0xF19;
		/// <summary>Hue for MageEye</summary>
		public const int HUE_MAGE_EYE_MAGE = 0xB78;

		public const int PRICE_SCROLL_MULTIPLIER = 10;
		/// <summary>Number of circles of scrolls to sell</summary>
		public const int SCROLL_CIRCLES = 3;

		/// <summary>Quantity for Spellbook</summary>
		public const int QTY_SPELLBOOK = 8;
		/// <summary>Quantity range for WizardStick (min)</summary>
		public const int QTY_WIZARD_STICK_MIN = 1;
		/// <summary>Quantity range for WizardStick (max)</summary>
		public const int QTY_WIZARD_STICK_MAX = 5;
		/// <summary>Quantity range for WitchHat (min)</summary>
		public const int QTY_WITCH_HAT_MIN = 1;
		/// <summary>Quantity range for WitchHat (max)</summary>
		public const int QTY_WITCH_HAT_MAX = 10;
		/// <summary>Quantity range for MagicWizardsHat (min)</summary>
		public const int QTY_MAGIC_WIZARDS_HAT_MIN = 2;
		/// <summary>Quantity range for MagicWizardsHat (max)</summary>
		public const int QTY_MAGIC_WIZARDS_HAT_MAX = 10;
		/// <summary>Quantity for BagOfReagents</summary>
		public const int QTY_BAG_OF_REAGENTS = 10;
		/// <summary>Quantity for RecallRune</summary>
		public const int QTY_RECALL_RUNE_MAGE = 50;
		/// <summary>Quantity for reagents</summary>
		public const int QTY_REAGENTS_MAGE = 50;
		/// <summary>Quantity range for MageEye (min)</summary>
		public const int QTY_MAGE_EYE_MIN = 10;
		/// <summary>Quantity range for MageEye (max)</summary>
		public const int QTY_MAGE_EYE_MAX = 50;
		/// <summary>Quantity range for scrolls (min)</summary>
		public const int QTY_SCROLL_MIN = 1;
		/// <summary>Quantity range for scrolls (max)</summary>
		public const int QTY_SCROLL_MAX = 12;

		public const int ITEMID_SCROLL_INDEX6 = 0x1F2D;
		/// <summary>Item ID offset for scrolls after index 6</summary>
		public const int ITEMID_SCROLL_OFFSET = 1;

		// Sell prices for Mage items
		public const int SELL_PRICE_WIZARD_STICK_MAGE = 49;
		public const int SELL_PRICE_WIZARDS_HAT = 50;
		public const int SELL_PRICE_WITCH_HAT_MAGE = 49;
		public const int SELL_PRICE_BLACK_PEARL_MAGE = 5;
		public const int SELL_PRICE_BLOODMOSS_MAGE = 4;
		public const int SELL_PRICE_MANDRAKE_ROOT_MAGE = 3;
		public const int SELL_PRICE_GARLIC_MAGE = 2;
		public const int SELL_PRICE_GINSENG_MAGE = 3;
		public const int SELL_PRICE_NIGHTSHADE_MAGE = 2;
		public const int SELL_PRICE_SPIDERS_SILK_MAGE = 2;
		public const int SELL_PRICE_SULFUROUS_ASH_MAGE = 3;
		public const int SELL_PRICE_RECALL_RUNE_MAGE_SELL = 7;
		public const int SELL_PRICE_SPELLBOOK_MAGE = 60;
		public const int SELL_PRICE_MAGE_EYE_MAGE_SELL = 3;
		public const int SELL_PRICE_SCROLL_PER_LEVEL = 1;

		#endregion

		#region Mage Guild Constants

		/// <summary>Price for NecromancerSpellbook in guild</summary>
		public const int PRICE_NECROMANCER_SPELLBOOK_GUILD = 115;
		/// <summary>Item ID for NecromancerSpellbook in guild</summary>
		public const int ITEMID_NECROMANCER_SPELLBOOK_GUILD = 0x2253;

		/// <summary>Price for ScribesPen in guild</summary>
		public const int PRICE_SCRIBES_PEN_GUILD = 8;
		/// <summary>Item ID for ScribesPen in guild</summary>
		public const int ITEMID_SCRIBES_PEN_GUILD = 0x2051;

		/// <summary>Price for BlankScroll in guild</summary>
		public const int PRICE_BLANK_SCROLL_GUILD = 5;
		/// <summary>Item ID for BlankScroll in guild</summary>
		public const int ITEMID_BLANK_SCROLL_GUILD = 0x0E34;

		/// <summary>Price for BatWing in guild</summary>
		public const int PRICE_BAT_WING_GUILD = 3;
		/// <summary>Item ID for BatWing in guild</summary>
		public const int ITEMID_BAT_WING_GUILD = 0xF78;

		/// <summary>Price for DaemonBlood in guild</summary>
		public const int PRICE_DAEMON_BLOOD_GUILD = 6;
		/// <summary>Item ID for DaemonBlood in guild</summary>
		public const int ITEMID_DAEMON_BLOOD_GUILD = 0xF7D;

		/// <summary>Price for PigIron in guild</summary>
		public const int PRICE_PIG_IRON_GUILD = 5;
		/// <summary>Item ID for PigIron in guild</summary>
		public const int ITEMID_PIG_IRON_GUILD = 0xF8A;

		/// <summary>Price for NoxCrystal in guild</summary>
		public const int PRICE_NOX_CRYSTAL_GUILD = 6;
		/// <summary>Item ID for NoxCrystal in guild</summary>
		public const int ITEMID_NOX_CRYSTAL_GUILD = 0xF8E;

		/// <summary>Price for GraveDust in guild</summary>
		public const int PRICE_GRAVE_DUST_GUILD = 3;
		/// <summary>Item ID for GraveDust in guild</summary>
		public const int ITEMID_GRAVE_DUST_GUILD = 0xF8F;

		/// <summary>Price for BloodOathScroll in guild</summary>
		public const int PRICE_BLOOD_OATH_SCROLL_GUILD = 25;
		/// <summary>Item ID for BloodOathScroll in guild</summary>
		public const int ITEMID_BLOOD_OATH_SCROLL_GUILD = 0x2263;

		/// <summary>Price for CorpseSkinScroll in guild</summary>
		public const int PRICE_CORPSE_SKIN_SCROLL_GUILD = 28;
		/// <summary>Item ID for CorpseSkinScroll in guild</summary>
		public const int ITEMID_CORPSE_SKIN_SCROLL_GUILD = 0x2263;

		/// <summary>Price for CurseWeaponScroll in guild</summary>
		public const int PRICE_CURSE_WEAPON_SCROLL_GUILD = 12;
		/// <summary>Item ID for CurseWeaponScroll in guild</summary>
		public const int ITEMID_CURSE_WEAPON_SCROLL_GUILD = 0x2263;

		/// <summary>Price for ElectrumFlask in guild</summary>
		public const int PRICE_ELECTRUM_FLASK_GUILD = 2500;
		/// <summary>Item ID for ElectrumFlask in guild</summary>
		public const int ITEMID_ELECTRUM_FLASK_GUILD = 0x282E;

		/// <summary>Price for WizardStaff in guild</summary>
		public const int PRICE_WIZARD_STAFF_GUILD = 40;
		/// <summary>Item ID for WizardStaff in guild</summary>
		public const int ITEMID_WIZARD_STAFF_GUILD = 0x0908;
		/// <summary>Hue for WizardStaff in guild</summary>
		public const int HUE_WIZARD_STAFF_GUILD = 0xB3A;

		/// <summary>Price for WizardStick in guild</summary>
		public const int PRICE_WIZARD_STICK_GUILD = 38;
		/// <summary>Item ID for WizardStick in guild</summary>
		public const int ITEMID_WIZARD_STICK_GUILD = 0xDF2;
		/// <summary>Hue for WizardStick in guild</summary>
		public const int HUE_WIZARD_STICK_GUILD = 0xB3A;

		/// <summary>Price for MageEye in guild</summary>
		public const int PRICE_MAGE_EYE_GUILD = 2;
		/// <summary>Item ID for MageEye in guild</summary>
		public const int ITEMID_MAGE_EYE_GUILD = 0xF19;
		/// <summary>Hue for MageEye in guild</summary>
		public const int HUE_MAGE_EYE_GUILD = 0xB78;

		/// <summary>Price for LinkedGateBag in guild</summary>
		public const int PRICE_LINKED_GATE_BAG_GUILD = 300000;
		/// <summary>Item ID for LinkedGateBag in guild</summary>
		public const int ITEMID_LINKED_GATE_BAG_GUILD = 0xE76;
		/// <summary>Hue for LinkedGateBag in guild</summary>
		public const int HUE_LINKED_GATE_BAG_GUILD = 0xABE;

		/// <summary>Quantity for reagents in guild</summary>
		public const int QTY_REAGENTS_GUILD = 100;
		/// <summary>Quantity for scrolls in guild</summary>
		public const int QTY_SCROLLS_GUILD = 100;
		/// <summary>Quantity for flasks in guild</summary>
		public const int QTY_FLASKS_GUILD_MIN = 1;
		/// <summary>Quantity for flasks in guild</summary>
		public const int QTY_FLASKS_GUILD_MAX = 5;
		/// <summary>Quantity for staves in guild</summary>
		public const int QTY_STAVES_GUILD_MIN = 1;
		/// <summary>Quantity for staves in guild</summary>
		public const int QTY_STAVES_GUILD_MAX = 5;
		/// <summary>Quantity for mage eye in guild</summary>
		public const int QTY_MAGE_EYE_GUILD_MIN = 10;
		/// <summary>Quantity for mage eye in guild</summary>
		public const int QTY_MAGE_EYE_GUILD_MAX = 150;
		/// <summary>Quantity for gate bag in guild</summary>
		public const int QTY_GATE_BAG_GUILD = 1;

		// Sell prices for Mage Guild items
		public const int SELL_PRICE_WIZARD_STAFF_GUILD = 20;
		public const int SELL_PRICE_WIZARD_STICK_GUILD_SELL = 19;
		public const int SELL_PRICE_MAGE_EYE_GUILD_SELL = 1;
		public const int SELL_PRICE_MAGIC_TALISMAN_GUILD_MIN = 50;
		public const int SELL_PRICE_MAGIC_TALISMAN_GUILD_MAX = 100;
		public const int SELL_PRICE_BAT_WING_GUILD = 1;
		public const int SELL_PRICE_DAEMON_BLOOD_GUILD = 3;
		public const int SELL_PRICE_PIG_IRON_GUILD = 2;
		public const int SELL_PRICE_NOX_CRYSTAL_GUILD = 3;
		public const int SELL_PRICE_GRAVE_DUST_GUILD = 1;
		public const int SELL_PRICE_NECROMANCER_SPELLBOOK_GUILD = 55;
		public const int SELL_PRICE_MYSTICAL_PEARL_GUILD = 250;

		#endregion

		#region Necro Guild Constants

		/// <summary>Price for NecromancerSpellbook in necro guild</summary>
		public const int PRICE_NECROMANCER_SPELLBOOK_NECRO_GUILD = 115;
		/// <summary>Item ID for NecromancerSpellbook in necro guild</summary>
		public const int ITEMID_NECROMANCER_SPELLBOOK_NECRO_GUILD = 0x2253;

		/// <summary>Price for BatWing in necro guild</summary>
		public const int PRICE_BAT_WING_NECRO_GUILD = 3;
		/// <summary>Item ID for BatWing in necro guild</summary>
		public const int ITEMID_BAT_WING_NECRO_GUILD = 0xF78;

		/// <summary>Price for DaemonBlood in necro guild</summary>
		public const int PRICE_DAEMON_BLOOD_NECRO_GUILD = 6;
		/// <summary>Item ID for DaemonBlood in necro guild</summary>
		public const int ITEMID_DAEMON_BLOOD_NECRO_GUILD = 0xF7D;

		/// <summary>Price for PigIron in necro guild</summary>
		public const int PRICE_PIG_IRON_NECRO_GUILD = 5;
		/// <summary>Item ID for PigIron in necro guild</summary>
		public const int ITEMID_PIG_IRON_NECRO_GUILD = 0xF8A;

		/// <summary>Price for NoxCrystal in necro guild</summary>
		public const int PRICE_NOX_CRYSTAL_NECRO_GUILD = 6;
		/// <summary>Item ID for NoxCrystal in necro guild</summary>
		public const int ITEMID_NOX_CRYSTAL_NECRO_GUILD = 0xF8E;

		/// <summary>Price for GraveDust in necro guild</summary>
		public const int PRICE_GRAVE_DUST_NECRO_GUILD = 3;
		/// <summary>Item ID for GraveDust in necro guild</summary>
		public const int ITEMID_GRAVE_DUST_NECRO_GUILD = 0xF8F;

		/// <summary>Price for BloodOathScroll in necro guild</summary>
		public const int PRICE_BLOOD_OATH_SCROLL_NECRO_GUILD = 25;
		/// <summary>Item ID for BloodOathScroll in necro guild</summary>
		public const int ITEMID_BLOOD_OATH_SCROLL_NECRO_GUILD = 0x2263;

		/// <summary>Price for CorpseSkinScroll in necro guild</summary>
		public const int PRICE_CORPSE_SKIN_SCROLL_NECRO_GUILD = 28;
		/// <summary>Item ID for CorpseSkinScroll in necro guild</summary>
		public const int ITEMID_CORPSE_SKIN_SCROLL_NECRO_GUILD = 0x2263;

		/// <summary>Price for CurseWeaponScroll in necro guild</summary>
		public const int PRICE_CURSE_WEAPON_SCROLL_NECRO_GUILD = 12;
		/// <summary>Item ID for CurseWeaponScroll in necro guild</summary>
		public const int ITEMID_CURSE_WEAPON_SCROLL_NECRO_GUILD = 0x2263;

		/// <summary>Quantity for reagents in necro guild</summary>
		public const int QTY_REAGENTS_NECRO_GUILD_MIN = 10;
		/// <summary>Quantity for reagents in necro guild</summary>
		public const int QTY_REAGENTS_NECRO_GUILD_MAX = 100;

		// Sell prices for Necro Guild items
		public const int SELL_PRICE_BAT_WING_NECRO_GUILD = 1;
		public const int SELL_PRICE_DAEMON_BLOOD_NECRO_GUILD = 3;
		public const int SELL_PRICE_PIG_IRON_NECRO_GUILD = 2;
		public const int SELL_PRICE_NOX_CRYSTAL_NECRO_GUILD = 3;
		public const int SELL_PRICE_GRAVE_DUST_NECRO_GUILD = 1;
		public const int SELL_PRICE_EXORCISM_SCROLL_NECRO_GUILD = 1;
		public const int SELL_PRICE_ANIMATE_DEAD_SCROLL_NECRO_GUILD = 26;
		public const int SELL_PRICE_BLOOD_OATH_SCROLL_NECRO_GUILD = 26;
		public const int SELL_PRICE_CORPSE_SKIN_SCROLL_NECRO_GUILD = 26;
		public const int SELL_PRICE_CURSE_WEAPON_SCROLL_NECRO_GUILD = 26;
		public const int SELL_PRICE_EVIL_OMEN_SCROLL_NECRO_GUILD = 26;
		public const int SELL_PRICE_PAIN_SPIKE_SCROLL_NECRO_GUILD = 26;
		public const int SELL_PRICE_SUMMON_FAMILIAR_SCROLL_NECRO_GUILD = 26;
		public const int SELL_PRICE_HORRIFIC_BEAST_SCROLL_NECRO_GUILD = 27;
		public const int SELL_PRICE_MIND_ROT_SCROLL_NECRO_GUILD = 39;
		public const int SELL_PRICE_POISON_STRIKE_SCROLL_NECRO_GUILD = 39;
		public const int SELL_PRICE_WRAITH_FORM_SCROLL_NECRO_GUILD = 51;
		public const int SELL_PRICE_LICH_FORM_SCROLL_NECRO_GUILD = 64;
		public const int SELL_PRICE_STRANGLE_SCROLL_NECRO_GUILD = 64;
		public const int SELL_PRICE_WITHER_SCROLL_NECRO_GUILD = 64;
		public const int SELL_PRICE_VAMPIRIC_EMBRACE_SCROLL_NECRO_GUILD = 101;
		public const int SELL_PRICE_VENGEFUL_SPIRIT_SCROLL_NECRO_GUILD = 114;

		#endregion

		#region Scribe Constants

		/// <summary>Price for ScribesPen</summary>
		public const int PRICE_SCRIBES_PEN_SCRIBE = 16;
		/// <summary>Price for BlankScroll</summary>
		public const int PRICE_BLANK_SCROLL_SCRIBE = 5;
		/// <summary>Item ID for BlankScroll</summary>
		public const int ITEMID_BLANK_SCROLL_SCRIBE = 0x0E34;

		/// <summary>Price for BrownBook</summary>
		public const int PRICE_BROWN_BOOK = 100;
		/// <summary>Item ID for BrownBook</summary>
		public const int ITEMID_BROWN_BOOK = 0xFEF;

		/// <summary>Price for TanBook</summary>
		public const int PRICE_TAN_BOOK = 100;
		/// <summary>Item ID for TanBook</summary>
		public const int ITEMID_TAN_BOOK = 0xFF0;

		/// <summary>Price for BlueBook</summary>
		public const int PRICE_BLUE_BOOK = 100;
		/// <summary>Item ID for BlueBook</summary>
		public const int ITEMID_BLUE_BOOK = 0xFF2;

		/// <summary>Price for Runebook</summary>
		public const int PRICE_RUNEBOOK = 3500;
		/// <summary>Item ID for Runebook</summary>
		public const int ITEMID_RUNEBOOK = 0x0F3D;

		/// <summary>Price for Mailbox</summary>
		public const int PRICE_MAILBOX = 158;
		/// <summary>Item ID for Mailbox</summary>
		public const int ITEMID_MAILBOX = 0x4142;

		/// <summary>Quantity range for books (min)</summary>
		public const int QTY_BOOKS_MIN = 1;
		/// <summary>Quantity range for books (max)</summary>
		public const int QTY_BOOKS_MAX = 100;
		/// <summary>Quantity range for monocle (min)</summary>
		public const int QTY_MONOCLE_MIN = 1;
		/// <summary>Quantity range for monocle (max)</summary>
		public const int QTY_MONOCLE_MAX = 25;
		/// <summary>Quantity range for runebook (min)</summary>
		public const int QTY_RUNEBOOK_MIN = 1;
		/// <summary>Quantity range for runebook (max)</summary>
		public const int QTY_RUNEBOOK_MAX = 3;
		/// <summary>Quantity range for mailbox (min)</summary>
		public const int QTY_MAILBOX_MIN = 1;
		/// <summary>Quantity range for mailbox (max)</summary>
		public const int QTY_MAILBOX_MAX = 5;

		// Sell prices for Scribe items
		public const int SELL_PRICE_SCRIBES_PEN = 4;
		public const int SELL_PRICE_BROWN_BOOK = 7;
		public const int SELL_PRICE_TAN_BOOK = 7;
		public const int SELL_PRICE_BLUE_BOOK = 7;
		public const int SELL_PRICE_BLANK_SCROLL_SCRIBE_SELL = 3;
		public const int SELL_PRICE_JOKE_BOOK_MIN = 750;
		public const int SELL_PRICE_JOKE_BOOK_MAX = 1500;
		public const int SELL_PRICE_DYNAMIC_BOOK_MIN = 10;
		public const int SELL_PRICE_DYNAMIC_BOOK_MAX = 150;
		public const int SELL_PRICE_DATA_PAD_MIN = 5;
		public const int SELL_PRICE_DATA_PAD_MAX = 150;
		public const int SELL_PRICE_NECROMANCER_SPELLBOOK_SCRIBE = 55;
		public const int SELL_PRICE_BOOK_OF_BUSHIDO = 70;
		public const int SELL_PRICE_BOOK_OF_NINJITSU = 70;
		public const int SELL_PRICE_MYSTIC_SPELLBOOK = 70;
		public const int SELL_PRICE_DEATH_KNIGHT_SPELLBOOK_SCRIBE_MIN = 100;
		public const int SELL_PRICE_DEATH_KNIGHT_SPELLBOOK_SCRIBE_MAX = 300;
		public const int SELL_PRICE_RUNEBOOK_SCRIBE_MIN = 100;
		public const int SELL_PRICE_RUNEBOOK_SCRIBE_MAX = 350;
		public const int SELL_PRICE_BOOK_OF_CHIVALRY = 70;
		public const int SELL_PRICE_HOLY_MAN_SPELLBOOK_SCRIBE_MIN = 50;
		public const int SELL_PRICE_HOLY_MAN_SPELLBOOK_SCRIBE_MAX = 200;

		#endregion

		#region Elf Rares Constants

		/// <summary>Price range for elf rare items (min)</summary>
		public const int PRICE_ELF_RARE_ITEM_MIN = 50000;
		/// <summary>Price range for elf rare items (max)</summary>
		public const int PRICE_ELF_RARE_ITEM_MAX = 150000;
		/// <summary>Quantity for elf rare items</summary>
		public const int QTY_ELF_RARE_ITEM = 1;

		// Item IDs for Elf Rare Items
		public const int ITEMID_FUTON = 0x295C;
		public const int ITEMID_ARTIFACT_VASE = 0x0B48;
		public const int ITEMID_ARTIFACT_LARGE_VASE = 0x0B47;
		public const int ITEMID_BROKEN_BOOKCASE_DEED = 0x14F0;
		public const int ITEMID_BROKEN_BED_DEED = 0x14F0;
		public const int ITEMID_BROKEN_ARMOIRE_DEED = 0x14F0;
		public const int ITEMID_STANDING_BROKEN_CHAIR_DEED = 0x14F0;
		public const int ITEMID_BROKEN_VANITY_DEED = 0x14F0;
		public const int ITEMID_BROKEN_FALLEN_CHAIR_DEED = 0x14F0;
		public const int ITEMID_BROKEN_COVERED_CHAIR_DEED = 0x14F0;
		public const int ITEMID_BROKEN_CHEST_OF_DRAWERS_DEED = 0x14F0;
		public const int ITEMID_TAPESTRY_OF_SOSARIA = 0x234E;
		public const int ITEMID_ROSE_OF_TRINSIC = 0x234C;
		public const int ITEMID_HEARTH_OF_HOME_FIRE_DEED = 0x14F0;
		public const int ITEMID_VANITY_DEED = 0x14F0;
		public const int ITEMID_BLUE_DECORATIVE_RUG_DEED = 0x14F0;
		public const int ITEMID_BLUE_FANCY_RUG_DEED = 0x14F0;
		public const int ITEMID_BLUE_PLAIN_RUG_DEED = 0x14F0;
		public const int ITEMID_BOILING_CAULDRON_DEED = 0x14F0;
		public const int ITEMID_CINNAMON_FANCY_RUG_DEED = 0x14F0;
		public const int ITEMID_CURTAINS_DEED = 0x14F0;
		public const int ITEMID_FOUNTAIN_DEED = 0x14F0;
		public const int ITEMID_GOLDEN_DECORATIVE_RUG_DEED = 0x14F0;
		public const int ITEMID_HANGING_AXES_DEED = 0x14F0;
		public const int ITEMID_HANGING_SWORDS_DEED = 0x14F0;
		public const int ITEMID_HOUSE_LADDER_DEED = 0x14F0;
		public const int ITEMID_LARGE_FISHING_NET_DEED = 0x14F0;
		public const int ITEMID_PINK_FANCY_RUG_DEED = 0x14F0;
		public const int ITEMID_RED_PLAIN_RUG_DEED = 0x14F0;
		public const int ITEMID_SCARECROW_DEED = 0x14F0;
		public const int ITEMID_SMALL_FISHING_NET_DEED = 0x14F0;
		public const int ITEMID_TABLE_WITH_BLUE_CLOTH_DEED = 0x14F0;
		public const int ITEMID_TABLE_WITH_ORANGE_CLOTH_DEED = 0x14F0;
		public const int ITEMID_TABLE_WITH_PURPLE_CLOTH_DEED = 0x14F0;
		public const int ITEMID_TABLE_WITH_RED_CLOTH_DEED = 0x14F0;
		public const int ITEMID_UNMADE_BED_DEED = 0x14F0;
		public const int ITEMID_WALL_BANNER_DEED = 0x14F0;
		public const int ITEMID_TREE_STUMP_DEED = 0x14F0;
		public const int ITEMID_DECORATIVE_SHIELD_DEED = 0x14F0;
		public const int ITEMID_MINING_CART_DEED = 0x14F0;
		public const int ITEMID_POTTED_CACTUS_DEED = 0x14F0;
		public const int ITEMID_STONE_ANKH_DEED = 0x14F0;
		public const int ITEMID_BANNER_DEED = 0x14F0;
		public const int ITEMID_TUB = 0xE83;
		public const int ITEMID_WATER_BARREL = 0xE77;
		public const int ITEMID_CLOSED_BARREL = 0x0FAE;
		public const int ITEMID_BUCKET = 0x14E0;
		public const int ITEMID_DECO_TRAY = 0x992;
		public const int ITEMID_DECO_TRAY2 = 0x991;
		public const int ITEMID_DECO_BOTTLES_OF_LIQUOR = 0x99E;
		public const int ITEMID_CHECKERS = 0xE1A;
		public const int ITEMID_CHESSMEN3 = 0xE14;
		public const int ITEMID_CHESSMEN2 = 0xE12;
		public const int ITEMID_CHESSMEN = 0xE13;
		public const int ITEMID_CHECKERS2 = 0xE1B;
		public const int ITEMID_DECO_HAY2 = 0xF34;
		public const int ITEMID_DECO_BRIDLE2 = 0x1375;
		public const int ITEMID_DECO_BRIDLE = 0x1374;

		#endregion

		#region Carpenter Constants

		/// <summary>Price for Hatchet</summary>
		public const int PRICE_HATCHET = 25;

		/// <summary>Price for LumberAxe</summary>
		public const int PRICE_LUMBER_AXE = 27;

		/// <summary>Price for Nails</summary>
		public const int PRICE_NAILS = 3;

		/// <summary>Price for Axle</summary>
		public const int PRICE_AXLE = 2;

		/// <summary>Price for Board</summary>
		public const int PRICE_BOARD = 3;

		/// <summary>Price for DrawKnife</summary>
		public const int PRICE_DRAW_KNIFE = 10;

		/// <summary>Price for Froe</summary>
		public const int PRICE_FROE = 10;

		/// <summary>Price for Scorp</summary>
		public const int PRICE_SCORP = 10;

		/// <summary>Price for Inshave</summary>
		public const int PRICE_INSHAVE = 10;

		/// <summary>Price for DovetailSaw</summary>
		public const int PRICE_DOVETAIL_SAW = 12;

		/// <summary>Price for Saw</summary>
		public const int PRICE_SAW = 100;

		/// <summary>Price for Hammer</summary>
		public const int PRICE_HAMMER = 17;

		/// <summary>Price for MouldingPlane</summary>
		public const int PRICE_MOULDING_PLANE = 11;

		/// <summary>Price for SmoothingPlane</summary>
		public const int PRICE_SMOOTHING_PLANE = 10;

		/// <summary>Price for JointingPlane</summary>
		public const int PRICE_JOINTING_PLANE = 11;

		/// <summary>Price for WoodworkingTools</summary>
		public const int PRICE_WOODWORKING_TOOLS = 10;

		/// <summary>Price for SawMillEastAddonDeed</summary>
		public const int PRICE_SAWMILL_ADDON_DEED = 500;

		/// <summary>Price for SawMillSouthAddonDeed</summary>
		public const int PRICE_SAWMILL_SOUTH_ADDON_DEED = 500;

		/// <summary>Price range for rare items (crates, shelves, armoires) - min</summary>
		public const int PRICE_RARE_ITEM_MIN = 200;

		/// <summary>Price range for rare items (crates, shelves, armoires) - max</summary>
		public const int PRICE_RARE_ITEM_MAX = 400;

		/// <summary>Quantity range for rare items - min</summary>
		public const int QTY_RARE_ITEM_MIN = 1;

		/// <summary>Quantity range for rare items - max</summary>
		public const int QTY_RARE_ITEM_MAX = 5;

		/// <summary>Quantity range for WoodworkingTools - min</summary>
		public const int QTY_WOODWORKING_TOOLS_MIN = 10;

		/// <summary>Quantity range for WoodworkingTools - max</summary>
		public const int QTY_WOODWORKING_TOOLS_MAX = 30;

		/// <summary>Item ID for Hatchet</summary>
		public const int ITEMID_HATCHET = 0xF44;

		/// <summary>Item ID for LumberAxe</summary>
		public const int ITEMID_LUMBER_AXE = 0xF43;

		/// <summary>Hue for LumberAxe</summary>
		public const int HUE_LUMBER_AXE = 0x96D;

		/// <summary>Item ID for Nails</summary>
		public const int ITEMID_NAILS = 0x102E;

		/// <summary>Item ID for Axle</summary>
		public const int ITEMID_AXLE = 0x105B;

		/// <summary>Item ID for Board</summary>
		public const int ITEMID_BOARD = 0x1BD7;

		/// <summary>Item ID for DrawKnife</summary>
		public const int ITEMID_DRAW_KNIFE = 0x10E4;

		/// <summary>Item ID for Froe</summary>
		public const int ITEMID_FROE = 0x10E5;

		/// <summary>Item ID for Scorp</summary>
		public const int ITEMID_SCORP = 0x10E7;

		/// <summary>Item ID for Inshave</summary>
		public const int ITEMID_INSHAVE = 0x10E6;

		/// <summary>Item ID for DovetailSaw</summary>
		public const int ITEMID_DOVETAIL_SAW = 0x1028;

		/// <summary>Item ID for Saw</summary>
		public const int ITEMID_SAW = 0x1034;

		/// <summary>Item ID for Hammer</summary>
		public const int ITEMID_HAMMER = 0x102A;

		/// <summary>Item ID for MouldingPlane</summary>
		public const int ITEMID_MOULDING_PLANE = 0x102C;

		/// <summary>Item ID for SmoothingPlane</summary>
		public const int ITEMID_SMOOTHING_PLANE = 0x1032;

		/// <summary>Item ID for JointingPlane</summary>
		public const int ITEMID_JOINTING_PLANE = 0x1030;

		/// <summary>Item ID for WoodworkingTools</summary>
		public const int ITEMID_WOODWORKING_TOOLS = 0x4F52;

		/// <summary>Item ID for SawMillAddonDeed</summary>
		public const int ITEMID_SAWMILL_ADDON_DEED = 0x14F0;

		/// <summary>Sell price for Hatchet</summary>
		public const int SELL_PRICE_HATCHET = 13;

		/// <summary>Sell price for LumberAxe</summary>
		public const int SELL_PRICE_LUMBER_AXE = 14;

		/// <summary>Sell price for WoodenBox</summary>
		public const int SELL_PRICE_WOODEN_BOX = 7;

		/// <summary>Sell price for SmallCrate</summary>
		public const int SELL_PRICE_SMALL_CRATE = 5;

		/// <summary>Sell price for MediumCrate</summary>
		public const int SELL_PRICE_MEDIUM_CRATE = 6;

		/// <summary>Sell price for LargeCrate</summary>
		public const int SELL_PRICE_LARGE_CRATE = 7;

		/// <summary>Sell price for WoodenChest</summary>
		public const int SELL_PRICE_WOODEN_CHEST = 100;

		/// <summary>Sell price for LargeTable</summary>
		public const int SELL_PRICE_LARGE_TABLE = 10;

		/// <summary>Sell price for Nightstand</summary>
		public const int SELL_PRICE_NIGHTSTAND = 7;

		/// <summary>Sell price for YewWoodTable</summary>
		public const int SELL_PRICE_YEW_WOOD_TABLE = 10;

		/// <summary>Sell price for Throne</summary>
		public const int SELL_PRICE_THRONE = 24;

		/// <summary>Sell price for WoodenThrone</summary>
		public const int SELL_PRICE_WOODEN_THRONE = 6;

		/// <summary>Sell price for Stool</summary>
		public const int SELL_PRICE_STOOL = 6;

		/// <summary>Sell price for FootStool</summary>
		public const int SELL_PRICE_FOOT_STOOL = 6;

		/// <summary>Sell price for FancyWoodenChairCushion</summary>
		public const int SELL_PRICE_FANCY_WOODEN_CHAIR_CUSHION = 12;

		/// <summary>Sell price for WoodenChairCushion</summary>
		public const int SELL_PRICE_WOODEN_CHAIR_CUSHION = 10;

		/// <summary>Sell price for WoodenChair</summary>
		public const int SELL_PRICE_WOODEN_CHAIR = 8;

		/// <summary>Sell price for BambooChair</summary>
		public const int SELL_PRICE_BAMBOO_CHAIR = 6;

		/// <summary>Sell price for WoodenBench</summary>
		public const int SELL_PRICE_WOODEN_BENCH = 6;

		/// <summary>Sell price for Saw</summary>
		public const int SELL_PRICE_SAW = 9;

		/// <summary>Sell price for DovetailSaw</summary>
		public const int SELL_PRICE_DOVETAIL_SAW = 6;

		/// <summary>Sell price for MouldingPlane</summary>
		public const int SELL_PRICE_MOULDING_PLANE = 5;

		/// <summary>Sell price for Nails</summary>
		public const int SELL_PRICE_NAILS = 1;

		/// <summary>Sell price for JointingPlane</summary>
		public const int SELL_PRICE_JOINTING_PLANE = 5;

		/// <summary>Sell price for WoodworkingTools</summary>
		public const int SELL_PRICE_WOODWORKING_TOOLS = 5;

		/// <summary>Sell price for Scorp</summary>
		public const int SELL_PRICE_SCORP = 6;

		/// <summary>Sell price for SmoothingPlane</summary>
		public const int SELL_PRICE_SMOOTHING_PLANE = 6;

		/// <summary>Sell price for DrawKnife</summary>
		public const int SELL_PRICE_DRAW_KNIFE = 6;

		/// <summary>Sell price for Froe</summary>
		public const int SELL_PRICE_FROE = 6;

		/// <summary>Sell price for Hammer (Carpenter)</summary>
		public const int SELL_PRICE_HAMMER_CARPENTER = 14;
		/// <summary>Sell price for Inshave</summary>
		public const int SELL_PRICE_INSHAVE = 6;

		#endregion

		#region Treasure Pile Addon Deed Constants

		/// <summary>Sell price for TreasurePile02AddonDeed</summary>
		public const int SELL_PRICE_TREASURE_PILE_02_ADDON_DEED = 200;
		/// <summary>Sell price for TreasurePile01AddonDeed</summary>
		public const int SELL_PRICE_TREASURE_PILE_01_ADDON_DEED = 225;
		/// <summary>Sell price for TreasurePile03AddonDeed</summary>
		public const int SELL_PRICE_TREASURE_PILE_03_ADDON_DEED = 250;
		/// <summary>Sell price for TreasurePile04AddonDeed</summary>
		public const int SELL_PRICE_TREASURE_PILE_04_ADDON_DEED = 300;
		/// <summary>Sell price for TreasurePile05AddonDeed</summary>
		public const int SELL_PRICE_TREASURE_PILE_05_ADDON_DEED = 350;
		/// <summary>Sell price for TreasurePileAddonDeed</summary>
		public const int SELL_PRICE_TREASURE_PILE_ADDON_DEED = 625;
		/// <summary>Sell price for TreasurePile2AddonDeed</summary>
		public const int SELL_PRICE_TREASURE_PILE_2_ADDON_DEED = 650;
		/// <summary>Sell price for TreasurePile3AddonDeed</summary>
		public const int SELL_PRICE_TREASURE_PILE_3_ADDON_DEED = 800;

		/// <summary>Buy price for TreasurePile02AddonDeed (20% markup)</summary>
		public const int PRICE_TREASURE_PILE_02_ADDON_DEED = 240;
		/// <summary>Buy price for TreasurePile01AddonDeed (20% markup)</summary>
		public const int PRICE_TREASURE_PILE_01_ADDON_DEED = 270;
		/// <summary>Buy price for TreasurePile03AddonDeed (20% markup)</summary>
		public const int PRICE_TREASURE_PILE_03_ADDON_DEED = 300;
		/// <summary>Buy price for TreasurePile04AddonDeed (20% markup)</summary>
		public const int PRICE_TREASURE_PILE_04_ADDON_DEED = 360;
		/// <summary>Buy price for TreasurePile05AddonDeed (20% markup)</summary>
		public const int PRICE_TREASURE_PILE_05_ADDON_DEED = 420;
		/// <summary>Buy price for TreasurePileAddonDeed (20% markup)</summary>
		public const int PRICE_TREASURE_PILE_ADDON_DEED = 750;
		/// <summary>Buy price for TreasurePile2AddonDeed (20% markup)</summary>
		public const int PRICE_TREASURE_PILE_2_ADDON_DEED = 780;
		/// <summary>Buy price for TreasurePile3AddonDeed (20% markup)</summary>
		public const int PRICE_TREASURE_PILE_3_ADDON_DEED = 960;

		/// <summary>Item ID for TreasurePileAddonDeed</summary>
		public const int ITEMID_TREASURE_PILE_ADDON_DEED = 0x0E41;

		#endregion
	}
}


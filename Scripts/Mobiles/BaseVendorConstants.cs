namespace Server.Mobiles
{
	/// <summary>
	/// Centralized constants for BaseVendor calculations and mechanics.
	/// Extracted from BaseVendor.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class BaseVendorConstants
	{
		#region Vendor Limits

		/// <summary>Maximum number of items that can be sold at once</summary>
		public const int MAX_SELL_ITEMS = 1000;

		/// <summary>Maximum number of items in buy list</summary>
		public const int MAX_BUY_LIST_ITEMS = 250;

		/// <summary>Maximum items before client errors (commented limit)</summary>
		public const int MAX_ITEMS_CLIENT_LIMIT = 255;

		#endregion

		#region Time Constants

		/// <summary>Bulk order delay in minutes</summary>
		public const int BULK_ORDER_DELAY_MINUTES = 10;

		/// <summary>Restock delay in hours</summary>
		public const int RESTOCK_DELAY_HOURS = 2;

		/// <summary>Spam timer delay in minutes</summary>
		public const int SPAM_TIMER_MINUTES = 10;

		/// <summary>Inventory decay time in hours</summary>
		public const double INVENTORY_DECAY_HOURS = 2.0;

		/// <summary>BOD turn-in delay in seconds</summary>
		public const double BOD_TURNIN_DELAY_SECONDS = 10.0;

		/// <summary>LookForReward delay in seconds</summary>
		public const int LOOK_FOR_REWARD_DELAY_SECONDS = 2;

		#endregion

		#region Range Constants

		/// <summary>Talk range for vendor greetings</summary>
		public const int TALK_RANGE = 3;

		/// <summary>Player sight range for vendor behavior</summary>
		public const int PLAYER_SIGHT_RANGE = 6;

		#endregion

		#region Gold Constants

		/// <summary>Initial gold minimum amount</summary>
		public const int INITIAL_GOLD_MIN = 30;

		/// <summary>Initial gold maximum amount</summary>
		public const int INITIAL_GOLD_MAX = 120;

		/// <summary>Percentage multiplier for gold calculations (0.01 = 1%)</summary>
		public const double PERCENTAGE_MULTIPLIER = 0.01;

		/// <summary>Bank threshold for using bank instead of backpack</summary>
		public const int BANK_THRESHOLD_GOLD = 2000;

		/// <summary>Bank check threshold (use check instead of gold)</summary>
		public const int BANK_CHECK_THRESHOLD = 10000;

		/// <summary>Bank deposit threshold (deposit instead of backpack)</summary>
		public const int BANK_DEPOSIT_THRESHOLD = 2500;

		/// <summary>Gold stack limit</summary>
		public const int GOLD_STACK_LIMIT = 30000;

		/// <summary>Midland gold divisor</summary>
		public const int MIDLAND_GOLD_DIVISOR = 2;

		/// <summary>Gold per credit minimum</summary>
		public const int GOLD_PER_CREDIT_MIN = 4;

		/// <summary>Gold per credit maximum</summary>
		public const int GOLD_PER_CREDIT_MAX = 7;

		#endregion

		#region Stat Ranges

		/// <summary>Strength minimum</summary>
		public const int STR_MIN = 50;

		/// <summary>Strength maximum</summary>
		public const int STR_MAX = 200;

		/// <summary>Dexterity minimum</summary>
		public const int DEX_MIN = 30;

		/// <summary>Dexterity maximum</summary>
		public const int DEX_MAX = 150;

		/// <summary>Intelligence minimum</summary>
		public const int INT_MIN = 50;

		/// <summary>Intelligence maximum</summary>
		public const int INT_MAX = 180;

		/// <summary>Hits minimum</summary>
		public const int HITS_MIN = 100;

		/// <summary>Hits maximum</summary>
		public const int HITS_MAX = 300;

		/// <summary>Damage minimum</summary>
		public const int DAMAGE_MIN = 5;

		/// <summary>Damage maximum</summary>
		public const int DAMAGE_MAX = 35;

		/// <summary>Resistance minimum</summary>
		public const int RESISTANCE_MIN = 30;

		/// <summary>Resistance maximum</summary>
		public const int RESISTANCE_MAX = 70;

		/// <summary>Physical damage type percentage</summary>
		public const int DAMAGE_TYPE_PHYSICAL_PERCENT = 100;

		#endregion

		#region Skill Ranges

		/// <summary>Skill minimum value</summary>
		public const double SKILL_MIN = 50.0;

		/// <summary>Skill maximum value</summary>
		public const double SKILL_MAX = 100.0;

		/// <summary>Detect Hidden skill minimum</summary>
		public const double DETECT_HIDDEN_MIN = 10.0;

		/// <summary>Min Tame Skill for purchased creatures</summary>
		public const double MIN_TAME_SKILL = 29.1;

		#endregion

		#region Initialization Constants

		/// <summary>Initial stats values</summary>
		public const int INIT_STATS_STR = 100;
		public const int INIT_STATS_DEX = 100;
		public const int INIT_STATS_INT = 25;

		/// <summary>Random threshold for special effects (10% chance)</summary>
		public const double RANDOM_THRESHOLD = 0.1;

		/// <summary>Random threshold for credit variability (33% chance)</summary>
		public const double CREDIT_VAR_LOW = 0.33;

		/// <summary>Random threshold for credit variability (80% chance)</summary>
		public const double CREDIT_VAR_HIGH = 0.80;

		/// <summary>Credit multiplier low</summary>
		public const double CREDIT_MULT_LOW = 0.85;

		/// <summary>Credit multiplier high</summary>
		public const double CREDIT_MULT_HIGH = 1.15;

		#endregion

		#region Price Calculation Constants

		/// <summary>Default price scalar</summary>
		public const int DEFAULT_PRICE_SCALAR = 100;

		/// <summary>Guild barter skill cap</summary>
		public const int GUILD_BARTER_CAP = 100;

		/// <summary>Begging karma threshold</summary>
		public const int BEGGING_KARMA_THRESHOLD = -2459;

		/// <summary>Begging charisma bonus</summary>
		public const int BEGGING_CHARISMA_BONUS = 40;

		/// <summary>Begging words random range minimum</summary>
		public const int BEGGING_WORDS_MIN = 0;

		/// <summary>Begging words random range maximum</summary>
		public const int BEGGING_WORDS_MAX = 5;

		#endregion

		#region Price Modifiers (AdjustPrices)

		/// <summary>Curse threshold level 1</summary>
		public const int CURSE_THRESHOLD_1 = 10000;

		/// <summary>Curse threshold level 2</summary>
		public const int CURSE_THRESHOLD_2 = 20000;

		/// <summary>Curse threshold level 3</summary>
		public const int CURSE_THRESHOLD_3 = 30000;

		/// <summary>Curse threshold level 4</summary>
		public const int CURSE_THRESHOLD_4 = 40000;

		/// <summary>Curse threshold level 5</summary>
		public const int CURSE_THRESHOLD_5 = 50000;

		/// <summary>Curse threshold level 6</summary>
		public const int CURSE_THRESHOLD_6 = 60000;

		/// <summary>Curse threshold level 7</summary>
		public const int CURSE_THRESHOLD_7 = 70000;

		/// <summary>Curse threshold level 8</summary>
		public const int CURSE_THRESHOLD_8 = 80000;

		/// <summary>Curse threshold level 9</summary>
		public const int CURSE_THRESHOLD_9 = 90000;

		/// <summary>Curse threshold level 10 (maximum)</summary>
		public const int CURSE_THRESHOLD_MAX = 100000;

		/// <summary>Price modifier for curse level 1 (55%)</summary>
		public const double PRICE_MOD_1 = 0.55;

		/// <summary>Price modifier for curse level 2 (60%)</summary>
		public const double PRICE_MOD_2 = 0.60;

		/// <summary>Price modifier for curse level 3 (65%)</summary>
		public const double PRICE_MOD_3 = 0.65;

		/// <summary>Price modifier for curse level 4 (70%)</summary>
		public const double PRICE_MOD_4 = 0.70;

		/// <summary>Price modifier for curse level 5 (75%)</summary>
		public const double PRICE_MOD_5 = 0.75;

		/// <summary>Price modifier for curse level 6 (80%)</summary>
		public const double PRICE_MOD_6 = 0.80;

		/// <summary>Price modifier for curse level 7 (85%)</summary>
		public const double PRICE_MOD_7 = 0.85;

		/// <summary>Price modifier for curse level 8 (90%)</summary>
		public const double PRICE_MOD_8 = 0.90;

		/// <summary>Price modifier for curse level 9 (95%)</summary>
		public const double PRICE_MOD_9 = 0.95;

		/// <summary>Price modifier for curse level 10 (100%)</summary>
		public const double PRICE_MOD_10 = 1.00;

		/// <summary>Amount modifier for curse level 1 (145%)</summary>
		public const double AMOUNT_MOD_1 = 1.45;

		/// <summary>Amount modifier for curse level 2 (135%)</summary>
		public const double AMOUNT_MOD_2 = 1.35;

		/// <summary>Amount modifier for curse level 3 (125%)</summary>
		public const double AMOUNT_MOD_3 = 1.25;

		/// <summary>Amount modifier for curse level 4 (115%)</summary>
		public const double AMOUNT_MOD_4 = 1.15;

		/// <summary>Amount modifier for curse level 5 (105%)</summary>
		public const double AMOUNT_MOD_5 = 1.05;

		/// <summary>Amount modifier for curse level 6 (95%)</summary>
		public const double AMOUNT_MOD_6 = 0.95;

		/// <summary>Amount modifier for curse level 7 (85%)</summary>
		public const double AMOUNT_MOD_7 = 0.85;

		/// <summary>Amount modifier for curse level 8 (75%)</summary>
		public const double AMOUNT_MOD_8 = 0.75;

		/// <summary>Amount modifier for curse level 9 (65%)</summary>
		public const double AMOUNT_MOD_9 = 0.65;

		/// <summary>Amount modifier for curse level 10 (55%)</summary>
		public const double AMOUNT_MOD_10 = 0.55;

		/// <summary>Minimum price modifier (prevents prices below 1)</summary>
		public const double MIN_PRICE_MOD = 1.0;

		#endregion

		#region Body and Hue Constants

		/// <summary>Body ID for male characters</summary>
		public const int BODY_MALE = 0x190;

		/// <summary>Body ID for female characters</summary>
		public const int BODY_FEMALE = 0x191;

		/// <summary>Body ID for waiter male</summary>
		public const int BODY_WAITER_MALE = 400;

		/// <summary>Body ID for waiter female</summary>
		public const int BODY_WAITER_FEMALE = 401;

		/// <summary>Body ID for ruins male</summary>
		public const int BODY_RUINS_MALE = 605;

		/// <summary>Body ID for ruins female</summary>
		public const int BODY_RUINS_FEMALE = 606;

		/// <summary>Name hue for invulnerable vendors (non-AOS)</summary>
		public const int INVULNERABLE_NAME_HUE = 0x35;

		/// <summary>Hue range minimum for ruins</summary>
		public const int HUE_RUINS_MIN = 496;

		/// <summary>Hue range maximum for ruins</summary>
		public const int HUE_RUINS_MAX = 510;

		/// <summary>Hue for Temple of Praetoria</summary>
		public const int HUE_TEMPLE_PRAETORIA = 1489;

		#endregion

		#region Karma Constants

		/// <summary>Karma for vendors in The Pit</summary>
		public const int KARMA_PIT = -500;

		/// <summary>Karma for vendors in Temple of Praetoria</summary>
		public const int KARMA_TEMPLE = -1;

		/// <summary>Karma award for killing vendor</summary>
		public const int KARMA_AWARD_VENDOR_KILL = 300;

		#endregion

		#region Kill Constants

		/// <summary>Kills minimum for vendors in The Pit</summary>
		public const int KILLS_PIT_MIN = 5;

		/// <summary>Kills maximum for vendors in The Pit</summary>
		public const int KILLS_PIT_MAX = 10;

		#endregion

		#region Coordinate Constants

		/// <summary>Darkmoor X coordinate maximum</summary>
		public const int DARKMOOR_X_MAX = 1007;

		/// <summary>Darkmoor Y coordinate maximum</summary>
		public const int DARKMOOR_Y_MAX = 1280;

		#endregion

		#region Sound Constants

		/// <summary>Sound ID for buying items</summary>
		public const int SOUND_BUY = 0x32;

		/// <summary>Sound ID for relic transactions</summary>
		public const int SOUND_RELIC = 0x3D;

		/// <summary>Sound ID for selling items (gold dropping)</summary>
		public const int SOUND_SELL = 0x0037;

		/// <summary>Sound ID for magic carpet alteration</summary>
		public const int SOUND_CARPET = 0x248;

		#endregion

		#region Message Hue Constants

		/// <summary>Default message hue</summary>
		public const int MESSAGE_HUE_DEFAULT = 1153;

		/// <summary>Message hue for death messages</summary>
		public const int MESSAGE_HUE_DEATH = 0;

		#endregion

		#region Serial Constants

		/// <summary>Null serial value</summary>
		public const int NULL_SERIAL = 0x7FC0FFEE;

		#endregion

		#region Skill Requirements

		/// <summary>Default skill requirement</summary>
		public const double SKILL_REQUIREMENT_DEFAULT = 50.0;

		/// <summary>Skill requirement for herbalist (multiple skills)</summary>
		public const double SKILL_REQUIREMENT_HERBALIST = 40.0;

		/// <summary>Thief skill requirement</summary>
		public const int THIEF_SKILL_REQUIREMENT = 10;

		#endregion

		#region Currency Exchange Rates

		/// <summary>Exchange rate for silver to gold</summary>
		public const int EXCHANGE_RATE_SILVER = 5;

		/// <summary>Exchange rate for copper to gold</summary>
		public const int EXCHANGE_RATE_COPPER = 10;

		/// <summary>Currency multiplier for Xormite</summary>
		public const int CURRENCY_MULT_XORMITE = 3;

		/// <summary>Currency multiplier for Crystals</summary>
		public const int CURRENCY_MULT_CRYSTALS = 5;

		/// <summary>Currency multiplier for Jewels</summary>
		public const int CURRENCY_MULT_JEWELS = 2;

		/// <summary>Currency multiplier for Gemstones</summary>
		public const int CURRENCY_MULT_GEMSTONES = 2;

		#endregion

		#region Special Item Values

		/// <summary>Map purchase cost</summary>
		public const int MAP_COST = 1000;

		/// <summary>Steal box base value</summary>
		public const int STEAL_BOX_BASE_VALUE = 500;

		/// <summary>Bottle value minimum</summary>
		public const int BOTTLE_MIN = 2;

		/// <summary>Bottle value maximum</summary>
		public const int BOTTLE_MAX = 10;

		/// <summary>Skill divisor for forensics/anatomy</summary>
		public const int SKILL_DIVISOR = 3;

		#endregion

		#region Tomb Raid Values

		/// <summary>Tomb raid value for common artifacts</summary>
		public const int TOMB_RAID_COMMON = 5000;

		/// <summary>Tomb raid value for uncommon artifacts</summary>
		public const int TOMB_RAID_UNCOMMON = 6000;

		/// <summary>Tomb raid value for rare artifacts</summary>
		public const int TOMB_RAID_RARE = 7000;

		/// <summary>Tomb raid value for default artifacts</summary>
		public const int TOMB_RAID_DEFAULT = 8000;

		/// <summary>Tomb raid value for very rare artifacts</summary>
		public const int TOMB_RAID_VERY_RARE = 9000;

		/// <summary>Tomb raid value for extremely rare artifacts</summary>
		public const int TOMB_RAID_EXTREMELY_RARE = 10000;

		/// <summary>Tomb raid value for legendary artifacts</summary>
		public const int TOMB_RAID_LEGENDARY = 11000;

		/// <summary>Tomb raid value for unique artifacts</summary>
		public const int TOMB_RAID_UNIQUE = 12000;

		/// <summary>Fame divisor for tomb raids</summary>
		public const int TOMB_RAID_FAME_DIVISOR = 25;

		#endregion

		#region BOD Credit System

		/// <summary>Base credit value for calculations</summary>
		public const int BASE_CREDIT = 20;

		/// <summary>Credit multiplier tier 1</summary>
		public const int CREDIT_MULT_TIER_1 = 4;

		/// <summary>Credit multiplier tier 2</summary>
		public const int CREDIT_MULT_TIER_2 = 10;

		/// <summary>Credit multiplier tier 3</summary>
		public const int CREDIT_MULT_TIER_3 = 20;

		/// <summary>Credit multiplier tier 4</summary>
		public const int CREDIT_MULT_TIER_4 = 34;

		/// <summary>Credit multiplier tier 5</summary>
		public const int CREDIT_MULT_TIER_5 = 70;

		/// <summary>Credit multiplier tier 6</summary>
		public const int CREDIT_MULT_TIER_6 = 92;

		/// <summary>Credit multiplier tier 7</summary>
		public const int CREDIT_MULT_TIER_7 = 118;

		/// <summary>Credit multiplier tier 8</summary>
		public const int CREDIT_MULT_TIER_8 = 148;

		/// <summary>Credit multiplier tier 9</summary>
		public const int CREDIT_MULT_TIER_9 = 172;

		/// <summary>Credit multiplier tier 10</summary>
		public const int CREDIT_MULT_TIER_10 = 200;

		/// <summary>Credit multiplier tier 11</summary>
		public const int CREDIT_MULT_TIER_11 = 230;

		/// <summary>Credit multiplier tier 12</summary>
		public const int CREDIT_MULT_TIER_12 = 262;

		/// <summary>Credit multiplier tier 13</summary>
		public const int CREDIT_MULT_TIER_13 = 308;

		/// <summary>Credit multiplier tier 14</summary>
		public const int CREDIT_MULT_TIER_14 = 358;

		/// <summary>Credit multiplier tier 15</summary>
		public const int CREDIT_MULT_TIER_15 = 412;

		/// <summary>Credit multiplier tier 16</summary>
		public const int CREDIT_MULT_TIER_16 = 470;

		/// <summary>Diminishing returns cap</summary>
		public const int DIMINISHING_RETURNS_CAP = 12000;

		/// <summary>Fame divisor for credits</summary>
		public const int FAME_DIVISOR = 50;

		#endregion

		#region Runic Tool Constants

		/// <summary>Runic hammer base charges</summary>
		public const int RUNIC_CHARGES_BASE = 55;

		/// <summary>Runic sewing kit base charges</summary>
		public const int RUNIC_CHARGES_SEWING = 60;

		/// <summary>Runic charge reduction per tier</summary>
		public const int RUNIC_CHARGE_REDUCTION = 5;

		/// <summary>Runic sewing charge reduction per tier</summary>
		public const int RUNIC_SEWING_CHARGE_REDUCTION = 15;

		#endregion

		#region Power Scroll Constants

		/// <summary>Power scroll base skill</summary>
		public const int POWER_SCROLL_BASE = 100;

		/// <summary>Power scroll bonus tier 1</summary>
		public const int POWER_SCROLL_BONUS_1 = 5;

		/// <summary>Power scroll bonus tier 2</summary>
		public const int POWER_SCROLL_BONUS_2 = 10;

		/// <summary>Power scroll bonus tier 3</summary>
		public const int POWER_SCROLL_BONUS_3 = 15;

		/// <summary>Power scroll bonus tier 4</summary>
		public const int POWER_SCROLL_BONUS_4 = 20;

		/// <summary>Power scroll bonus tier 5</summary>
		public const int POWER_SCROLL_BONUS_5 = 25;

		/// <summary>Power scroll odds tier 1 (97%)</summary>
		public const double POWER_SCROLL_ODDS_1 = 0.97;

		/// <summary>Power scroll odds tier 2 (90%)</summary>
		public const double POWER_SCROLL_ODDS_2 = 0.90;

		/// <summary>Power scroll odds tier 3 (75%)</summary>
		public const double POWER_SCROLL_ODDS_3 = 0.75;

		/// <summary>Power scroll odds tier 4 (20%)</summary>
		public const double POWER_SCROLL_ODDS_4 = 0.20;

		#endregion

		#region Random Selection Constants

		/// <summary>Random selection for shirt types</summary>
		public const int RANDOM_SHIRT_COUNT = 3;

		/// <summary>Random selection for female pants</summary>
		public const int RANDOM_FEMALE_PANTS_COUNT = 6;

		/// <summary>Random selection for male pants</summary>
		public const int RANDOM_MALE_PANTS_COUNT = 2;

		/// <summary>Random selection for hue types</summary>
		public const int RANDOM_HUE_COUNT = 5;

		/// <summary>Random selection for combat messages</summary>
		public const int RANDOM_COMBAT_MESSAGE_COUNT = 4;

		/// <summary>Random selection for henchman messages</summary>
		public const int RANDOM_HENCHMAN_MESSAGE_COUNT = 8;

		/// <summary>Random selection for reward messages</summary>
		public const int RANDOM_REWARD_MESSAGE_COUNT = 10;

		/// <summary>Random selection for look for reward messages</summary>
		public const int RANDOM_LOOK_REWARD_COUNT = 5;

		#endregion

		#region Bright Hue Constants

		/// <summary>Bright hue special 1</summary>
		public const int BRIGHT_HUE_SPECIAL_1 = 0x62;

		/// <summary>Bright hue special 2</summary>
		public const int BRIGHT_HUE_SPECIAL_2 = 0x71;

		/// <summary>Bright hue list count</summary>
		public const int BRIGHT_HUE_LIST_COUNT = 10;

		#endregion

		#region Facial Hair Constants

		/// <summary>Facial hair option count</summary>
		public const int FACIAL_HAIR_OPTION_COUNT = 8;

		#endregion

		#region Time Conversion Constants

		/// <summary>Seconds to hours conversion offset</summary>
		public const int SECONDS_TO_HOURS_OFFSET = 3599;

		/// <summary>Seconds to minutes conversion offset</summary>
		public const int SECONDS_TO_MINUTES_OFFSET = 59;

		/// <summary>Seconds per hour</summary>
		public const int SECONDS_PER_HOUR = 3600;

		/// <summary>Seconds per minute</summary>
		public const int SECONDS_PER_MINUTE = 60;

		#endregion

		#region Context Menu Constants

		/// <summary>Context menu ID for bulk order info</summary>
		public const int CONTEXT_MENU_BULK_ORDER = 6152;

		/// <summary>Context menu ID for vendor buy</summary>
		public const int CONTEXT_MENU_BUY = 6103;

		/// <summary>Context menu ID for vendor sell</summary>
		public const int CONTEXT_MENU_SELL = 6104;

		/// <summary>Context menu ID for setup shoppe</summary>
		public const int CONTEXT_MENU_SETUP_SHOPPE = 6164;

		/// <summary>Context menu range for setup shoppe</summary>
		public const int CONTEXT_MENU_RANGE_SETUP = 3;

		/// <summary>Context menu range for buy/sell</summary>
		public const int CONTEXT_MENU_RANGE_BUY_SELL = 8;

		#endregion

		#region Serialization

		/// <summary>Current serialization version</summary>
		public const int SERIALIZATION_VERSION = 1;

		/// <summary>Default amount for deserialized items</summary>
		public const int DESERIALIZE_DEFAULT_AMOUNT = 20;

		#endregion
	}
}


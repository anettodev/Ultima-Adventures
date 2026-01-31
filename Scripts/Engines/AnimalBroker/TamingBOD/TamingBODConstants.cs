namespace Server.Items
{
	/// <summary>
	/// Centralized constants for Taming BOD calculations and mechanics.
	/// Extracted from TamingBOD.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class TamingBODConstants
	{
		#region Item Constants

		/// <summary>Normal item ID</summary>
		public const int ITEM_ID_NORMAL = 0x14EF;

		/// <summary>Flipped item ID</summary>
		public const int ITEM_ID_FLIPPED = 0x14F0;

		/// <summary>Item weight</summary>
		public const int ITEM_WEIGHT = 1;

		#endregion

		#region Contract Tier System

		/// <summary>Tier 1: Generic contracts (min 5, max 15)</summary>
		public const int TIER_1_MIN = 5;
		public const int TIER_1_MAX = 15;
		public const int TIER_1_COINS_PER_CREATURE = 35;

		/// <summary>Tier 2: Normal wildlife/domestic animals (min 6, max 18)</summary>
		public const int TIER_2_MIN = 6;
		public const int TIER_2_MAX = 18;
		public const int TIER_2_COINS_MIN = 50;
		public const int TIER_2_COINS_MAX = 120;

		/// <summary>Tier 3: Common riding/mounts (min 5, max 12)</summary>
		public const int TIER_3_MIN = 5;
		public const int TIER_3_MAX = 12;
		public const int TIER_3_COINS_MIN = 150;
		public const int TIER_3_COINS_MAX = 400;

		/// <summary>Tier 4: Dangerous creatures (min 5, max 12)</summary>
		public const int TIER_4_MIN = 5;
		public const int TIER_4_MAX = 12;
		public const int TIER_4_COINS_MIN = 300;
		public const int TIER_4_COINS_MAX = 800;

		/// <summary>Tier 5: Dragons and Drakes (min 5, max 8)</summary>
		public const int TIER_5_MIN = 5;
		public const int TIER_5_MAX = 8;
		public const int TIER_5_COINS_MIN = 700;
		public const int TIER_5_COINS_MAX = 1200;

		#endregion

		#region Contract Generation (Legacy - Deprecated)

		/// <summary>Ultra rare contract threshold (2% chance) - DEPRECATED: Use tier system</summary>
		public const double RARITY_THRESHOLD_ULTRA_RARE = 0.98;

		/// <summary>Very rare contract threshold (4% chance) - DEPRECATED: Use tier system</summary>
		public const double RARITY_THRESHOLD_VERY_RARE = 0.94;

		/// <summary>Rare contract threshold (24% chance) - DEPRECATED: Use tier system</summary>
		public const double RARITY_THRESHOLD_RARE = 0.70;

		/// <summary>Ultra rare amount minimum - DEPRECATED: Use tier system</summary>
		public const int ULTRA_RARE_MIN = 20;

		/// <summary>Ultra rare amount maximum - DEPRECATED: Use tier system</summary>
		public const int ULTRA_RARE_MAX = 30;

		/// <summary>Very rare amount minimum - DEPRECATED: Use tier system</summary>
		public const int VERY_RARE_MIN = 10;

		/// <summary>Very rare amount maximum - DEPRECATED: Use tier system</summary>
		public const int VERY_RARE_MAX = 20;

		/// <summary>Rare amount minimum - DEPRECATED: Use tier system</summary>
		public const int RARE_MIN = 5;

		/// <summary>Rare amount maximum - DEPRECATED: Use tier system</summary>
		public const int RARE_MAX = 10;

		/// <summary>Common amount minimum - DEPRECATED: Use tier system</summary>
		public const int COMMON_MIN = 1;

		/// <summary>Common amount maximum - DEPRECATED: Use tier system</summary>
		public const int COMMON_MAX = 5;

		#endregion

		#region Reward Tier Thresholds

		/// <summary>Reward tier 1 threshold</summary>
		public const int REWARD_THRESHOLD_TIER_1 = 2500;

		/// <summary>Reward tier 2 threshold</summary>
		public const int REWARD_THRESHOLD_TIER_2 = 5000;

		/// <summary>Reward tier 3 threshold</summary>
		public const int REWARD_THRESHOLD_TIER_3 = 10000;

		/// <summary>Reward tier 4 threshold</summary>
		public const int REWARD_THRESHOLD_TIER_4 = 30000;

		/// <summary>Reward tier 5 threshold</summary>
		public const int REWARD_THRESHOLD_TIER_5 = 60000;

		/// <summary>Reward tier 6 threshold</summary>
		public const int REWARD_THRESHOLD_TIER_6 = 100000;

		/// <summary>Reward tier 7 threshold</summary>
		public const int REWARD_THRESHOLD_TIER_7 = 150000;

		#endregion

		#region Reward Ranges

		/// <summary>Tier 1 reward minimum</summary>
		public const int TIER_1_REWARD_MIN = 0;

		/// <summary>Tier 1 reward maximum</summary>
		public const int TIER_1_REWARD_MAX = 60;

		/// <summary>Tier 2 reward minimum</summary>
		public const int TIER_2_REWARD_MIN = 50;

		/// <summary>Tier 2 reward maximum</summary>
		public const int TIER_2_REWARD_MAX = 250;

		/// <summary>Tier 3 reward minimum</summary>
		public const int TIER_3_REWARD_MIN = 100;

		/// <summary>Tier 3 reward maximum</summary>
		public const int TIER_3_REWARD_MAX = 350;

		/// <summary>Tier 4 reward minimum</summary>
		public const int TIER_4_REWARD_MIN = 200;

		/// <summary>Tier 4 reward maximum</summary>
		public const int TIER_4_REWARD_MAX = 400;

		/// <summary>Tier 5 reward minimum</summary>
		public const int TIER_5_REWARD_MIN = 300;

		/// <summary>Tier 5 reward maximum</summary>
		public const int TIER_5_REWARD_MAX = 500;

		/// <summary>Tier 6 reward minimum</summary>
		public const int TIER_6_REWARD_MIN = 400;

		/// <summary>Tier 6 reward maximum</summary>
		public const int TIER_6_REWARD_MAX = 510;

		/// <summary>Tier 7 reward minimum</summary>
		public const int TIER_7_REWARD_MIN = 450;

		/// <summary>Tier 7 reward maximum</summary>
		public const int TIER_7_REWARD_MAX = 525;

		/// <summary>Tier 8 reward minimum</summary>
		public const int TIER_8_REWARD_MIN = 490;

		/// <summary>Tier 8 reward maximum</summary>
		public const int TIER_8_REWARD_MAX = 600;

		#endregion

		#region Reward Item Thresholds

		/// <summary>No reward threshold</summary>
		public const int REWARD_THRESHOLD_NOTHING = 100;

		/// <summary>Easy reward threshold</summary>
		public const int REWARD_THRESHOLD_EASY = 250;

		/// <summary>Medium reward threshold</summary>
		public const int REWARD_THRESHOLD_MEDIUM = 375;

		/// <summary>Rare reward threshold</summary>
		public const int REWARD_THRESHOLD_RARE = 470;

		/// <summary>Impossible tier 1 threshold</summary>
		public const int REWARD_THRESHOLD_IMPOSSIBLE_1 = 500;

		/// <summary>Impossible tier 2 threshold</summary>
		public const int REWARD_THRESHOLD_IMPOSSIBLE_2 = 600;

		#endregion

		#region Switch Random Values

		/// <summary>Easy reward switch random value</summary>
		public const int SWITCH_EASY_RANDOM = 150;

		/// <summary>Medium reward switch random value</summary>
		public const int SWITCH_MEDIUM_RANDOM = 140;

		/// <summary>Rare reward switch random value</summary>
		public const int SWITCH_RARE_RANDOM = 150;

		/// <summary>Impossible tier 1 switch random value</summary>
		public const int SWITCH_IMPOSSIBLE_1_RANDOM = 280;

		/// <summary>Impossible tier 2 switch random value</summary>
		public const int SWITCH_IMPOSSIBLE_2_RANDOM = 62;

		#endregion

		#region Item Creation

		/// <summary>Powder of translocation quantity</summary>
		public const int POWDER_QUANTITY = 10;

		/// <summary>Power scroll skill level 105</summary>
		public const int POWER_SCROLL_105 = 105;

		/// <summary>Power scroll skill level 110</summary>
		public const int POWER_SCROLL_110 = 110;

		/// <summary>Power scroll skill level 115</summary>
		public const int POWER_SCROLL_115 = 115;

		/// <summary>Power scroll skill level 120</summary>
		public const int POWER_SCROLL_120 = 120;

		#endregion

		#region Serialization

		/// <summary>Current serialization version</summary>
		public const int SERIALIZATION_VERSION = 1;

		#endregion
	}
}


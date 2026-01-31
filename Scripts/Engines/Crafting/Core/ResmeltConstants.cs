namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for Resmelt system calculations and mechanics.
	/// Extracted from Resmelt.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class ResmeltConstants
	{
		#region Message Numbers

		/// <summary>Message: "You must be near a forge and and anvil to repair items."</summary>
		public const int MSG_MUST_BE_NEAR_FORGE_AND_ANVIL = 1044267;

		/// <summary>Message: "You must be near an anvil"</summary>
		public const int MSG_MUST_BE_NEAR_ANVIL = 1044266;

		/// <summary>Message: "You must be near a forge."</summary>
		public const int MSG_MUST_BE_NEAR_FORGE = 1044265;

		/// <summary>Message: "You have no idea how to work this metal."</summary>
		public const int MSG_NO_SKILL = 1044269;

		/// <summary>Message: "You melt the item down into ingots."</summary>
		public const int MSG_SMELT_SUCCESS = 1044270;

		/// <summary>Message: "You can't melt that down into ingots."</summary>
		public const int MSG_CANNOT_SMELT = 1044272;

		/// <summary>Message: "Target an item to recycle."</summary>
		public const int MSG_TARGET_ITEM_TO_RECYCLE = 1044273;

		/// <summary>Message: "You melt the item down into ingots." (store bought)</summary>
		public const int MSG_SMELT_SUCCESS_STORE_BOUGHT = 500418;

		#endregion

		#region Resource Difficulty Values

		/// <summary>Mining skill required for DullCopper</summary>
		public const double DIFFICULTY_DULL_COPPER = 65.0;

		/// <summary>Mining skill required for Copper</summary>
		public const double DIFFICULTY_COPPER = 70.0;

		/// <summary>Mining skill required for Bronze</summary>
		public const double DIFFICULTY_BRONZE = 75.0;

		/// <summary>Mining skill required for ShadowIron</summary>
		public const double DIFFICULTY_SHADOW_IRON = 80.0;

		/// <summary>Mining skill required for Platinum</summary>
		public const double DIFFICULTY_PLATINUM = 85.0;

		/// <summary>Mining skill required for Gold</summary>
		public const double DIFFICULTY_GOLD = 85.0;

		/// <summary>Mining skill required for Agapite</summary>
		public const double DIFFICULTY_AGAPITE = 90.0;

		/// <summary>Mining skill required for Verite</summary>
		public const double DIFFICULTY_VERITE = 95.0;

		/// <summary>Mining skill required for Valorite</summary>
		public const double DIFFICULTY_VALORITE = 95.0;

		/// <summary>Mining skill required for Titanium</summary>
		public const double DIFFICULTY_TITANIUM = 100.0;

		/// <summary>Mining skill required for Rosenium</summary>
		public const double DIFFICULTY_ROSENIUM = 100.0;

		/// <summary>Mining skill required for Nepturite</summary>
		public const double DIFFICULTY_NEPTURITE = 105.0;

		/// <summary>Mining skill required for Obsidian</summary>
		public const double DIFFICULTY_OBSIDIAN = 105.0;

		/// <summary>Mining skill required for Steel</summary>
		public const double DIFFICULTY_STEEL = 110.0;

		/// <summary>Mining skill required for Brass</summary>
		public const double DIFFICULTY_BRASS = 110.0;

		/// <summary>Mining skill required for Mithril</summary>
		public const double DIFFICULTY_MITHRIL = 115.0;

		/// <summary>Mining skill required for Xormite</summary>
		public const double DIFFICULTY_XORMITE = 115.0;

		/// <summary>Mining skill required for Dwarven</summary>
		public const double DIFFICULTY_DWARVEN = 120.0;

		#endregion

		#region Resource Amount Constants

		/// <summary>Minimum resource amount required to resmelt</summary>
		public const int MIN_RESOURCE_AMOUNT = 2;

		/// <summary>Amount multiplier for player-constructed items (50% of original)</summary>
		public const int PLAYER_CONSTRUCTED_DIVISOR = 2;

		/// <summary>Amount for store-bought items</summary>
		public const int STORE_BOUGHT_AMOUNT = 1;

		#endregion

		#region Sound IDs

		/// <summary>First sound effect played when resmelting</summary>
		public const int SOUND_SMELT_1 = 0x2A;

		/// <summary>Second sound effect played when resmelting</summary>
		public const int SOUND_SMELT_2 = 0x240;

		#endregion
	}
}


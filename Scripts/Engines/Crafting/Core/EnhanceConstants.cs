namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for Enhance system calculations and mechanics.
	/// Extracted from Enhance.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class EnhanceConstants
	{
		#region Base Chance Constants

		/// <summary>Base success chance for enhancement (20%)</summary>
		public const int BASE_CHANCE = 20;

		/// <summary>Minimum skill level (100) that affects base chance</summary>
		public const int SKILL_THRESHOLD = 100;

		/// <summary>Skill level (90) used in base chance calculation</summary>
		public const int SKILL_BASE = 90;

		/// <summary>Divisor for converting skill to chance modifier</summary>
		public const int SKILL_DIVISOR = 10;

		#endregion

		#region Failure Chance Constants

		/// <summary>Failure chance threshold (10% chance of failure)</summary>
		public const int FAILURE_CHANCE = 10;

		/// <summary>Random roll maximum (100)</summary>
		public const int RANDOM_MAX = 100;

		#endregion

		#region Attribute Calculation Divisors

		/// <summary>Divisor for durability bonus calculation</summary>
		public const int DURABILITY_DIVISOR = 40;

		/// <summary>Base luck bonus added to chance</summary>
		public const int LUCK_BASE_BONUS = 10;

		/// <summary>Divisor for luck bonus calculation</summary>
		public const int LUCK_DIVISOR = 2;

		/// <summary>Divisor for lower requirements bonus calculation</summary>
		public const int LREQ_DIVISOR = 4;

		/// <summary>Divisor for damage increase bonus calculation</summary>
		public const int DAMAGE_INC_DIVISOR = 4;

		#endregion

		#region Message Numbers

		/// <summary>Message: "Target an item to enhance with the properties of your selected material."</summary>
		public const int MSG_TARGET_ITEM = 1061004;

		/// <summary>Message: "You must select a special material in order to enhance an item with its properties."</summary>
		public const int MSG_SELECT_SPECIAL_MATERIAL = 1061010;

		/// <summary>Message: "The item must be in your backpack to enhance it."</summary>
		public const int MSG_NOT_IN_BACKPACK = 1061005;

		/// <summary>Message: "This item is already enhanced with the properties of a special material."</summary>
		public const int MSG_ALREADY_ENHANCED = 1061012;

		/// <summary>Message: "You cannot enhance this type of item with the properties of the selected special material."</summary>
		public const int MSG_BAD_ITEM = 1061011;

		/// <summary>Message: "You attempt to enhance the item, but fail catastrophically. The item is lost."</summary>
		public const int MSG_BROKEN = 1061080;

		/// <summary>Message: "You attempt to enhance the item, but fail. Some material is lost in the process."</summary>
		public const int MSG_FAILURE = 1061082;

		/// <summary>Message: "You enhance the item with the properties of the special material."</summary>
		public const int MSG_SUCCESS = 1061008;

		/// <summary>Message: "You don't have the required skills to attempt this item."</summary>
		public const int MSG_NO_SKILL = 1044153;

		#endregion

		#region Skill Calculation Constants

		/// <summary>Divisor for converting skill fixed value to integer</summary>
		public const int SKILL_FIXED_DIVISOR = 10;

		#endregion
	}
}


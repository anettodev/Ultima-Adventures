namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for Repair system calculations and mechanics.
	/// Extracted from Repair.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class RepairConstants
	{
		#region Message Numbers

		/// <summary>Message: "Target an item to repair."</summary>
		public const int MSG_TARGET_ITEM = 1044276;

		/// <summary>Message: "You must be near a forge and and anvil to repair items."</summary>
		public const int MSG_MUST_BE_NEAR_FORGE_AND_ANVIL = 1044282;

		/// <summary>Message: "That item cannot be repaired."</summary>
		public const int MSG_CANNOT_REPAIR = 1044277;

		/// <summary>Message: "You cannot repair that item with this type of repair contract."</summary>
		public const int MSG_CANNOT_REPAIR_WITH_DEED = 1061136;

		/// <summary>Message: "The item must be in your backpack to repair it."</summary>
		public const int MSG_MUST_BE_IN_BACKPACK = 1044275;

		/// <summary>Message: "That item is in full repair"</summary>
		public const int MSG_FULL_REPAIR = 1044281;

		/// <summary>Message: "That item has been repaired many times, and will break if repairs are attempted again."</summary>
		public const int MSG_TOO_MANY_REPAIRS = 1044278;

		/// <summary>Message: "You repair the item."</summary>
		public const int MSG_REPAIR_SUCCESS = 1044279;

		/// <summary>Message: "You fail to repair the item."</summary>
		public const int MSG_REPAIR_FAILURE = 1044280;

		/// <summary>Message: "You fail to repair the item. [And the contract is destroyed]"</summary>
		public const int MSG_REPAIR_FAILURE_DEED = 1061137;

		/// <summary>Message: "You can't repair that."</summary>
		public const int MSG_CANNOT_REPAIR_GENERIC = 500426;

		/// <summary>Message: "That is already in full repair."</summary>
		public const int MSG_ALREADY_FULL_REPAIR = 500423;

		/// <summary>Message: "You don't have the required skills to attempt this item."</summary>
		public const int MSG_NO_SKILL = 1044153;

		/// <summary>Message: "You must wait before trying again."</summary>
		public const int MSG_MUST_WAIT = 501789;

		/// <summary>Message: "You do not have sufficient metal to make that."</summary>
		public const int MSG_INSUFFICIENT_METAL = 1044037;

		/// <summary>Message: "You create the item and put it in your backpack."</summary>
		public const int MSG_CREATE_REPAIR_DEED = 500442;

		/// <summary>Message: "You must be at least apprentice level to create a repair service contract."</summary>
		public const int MSG_APPRENTICE_REQUIRED = 1047005;

		#endregion

		#region Weaken Calculation Constants

		/// <summary>Base weaken chance percentage (40%)</summary>
		public const int WEAKEN_BASE_CHANCE = 40;

		/// <summary>Divisor for skill in weaken calculation</summary>
		public const int WEAKEN_SKILL_DIVISOR = 10;

		/// <summary>Random roll maximum for weaken check</summary>
		public const int WEAKEN_RANDOM_MAX = 100;

		#endregion

		#region Skill Level Thresholds

		/// <summary>High skill threshold (90.0) - weakens by 1</summary>
		public const double SKILL_HIGH = 90.0;

		/// <summary>Medium skill threshold (70.0) - weakens by 2</summary>
		public const double SKILL_MEDIUM = 70.0;

		/// <summary>Low skill - weakens by 3</summary>
		public const int WEAKEN_HIGH_SKILL = 1;
		public const int WEAKEN_MEDIUM_SKILL = 2;
		public const int WEAKEN_LOW_SKILL = 3;

		/// <summary>Minimum skill to create repair deed (50.0)</summary>
		public const double SKILL_MIN_FOR_DEED = 50.0;

		/// <summary>Minimum skill to repair golem (60.0)</summary>
		public const double SKILL_MIN_FOR_GOLEM = 60.0;

		#endregion

		#region Repair Difficulty Constants

		/// <summary>Multiplier for repair difficulty calculation</summary>
		public const int DIFFICULTY_MULTIPLIER = 1250;

		/// <summary>Subtractor for repair difficulty calculation</summary>
		public const int DIFFICULTY_SUBTRACTOR = 250;

		/// <summary>Difficulty scalar (0.1)</summary>
		public const double DIFFICULTY_SCALAR = 0.1;

		/// <summary>Skill variance for repair difficulty check</summary>
		public const double DIFFICULTY_VARIANCE = 25.0;

		#endregion

		#region Golem Repair Constants

		/// <summary>Maximum damage percentage for golem repair (30%)</summary>
		public const double GOLEM_DAMAGE_PERCENT = 0.3;

		/// <summary>Base damage added to golem repair</summary>
		public const int GOLEM_BASE_DAMAGE = 30;

		/// <summary>Divisor for failed skill check (damage halved)</summary>
		public const int GOLEM_FAILURE_DIVISOR = 2;

		/// <summary>Ingot consumption calculation divisor</summary>
		public const int GOLEM_INGOT_DIVISOR = 5;

		/// <summary>Ingot consumption calculation offset</summary>
		public const int GOLEM_INGOT_OFFSET = 4;

		/// <summary>Hits restored per ingot consumed</summary>
		public const int GOLEM_HITS_PER_INGOT = 5;

		/// <summary>Golem repair cooldown in seconds</summary>
		public const double GOLEM_COOLDOWN_SECONDS = 12.0;

		/// <summary>Minimum skill check for golem repair</summary>
		public const double GOLEM_SKILL_MIN = 0.0;

		/// <summary>Maximum skill check for golem repair</summary>
		public const double GOLEM_SKILL_MAX = 100.0;

		#endregion

		#region Target Range

		/// <summary>Target range for repair targeting</summary>
		public const int TARGET_RANGE = 2;

		#endregion

		#region Craft System Check

		/// <summary>Message number for forge/anvil requirement check</summary>
		public const int MSG_FORGE_ANVIL_CHECK = 1044267;

		#endregion
	}
}


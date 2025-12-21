namespace Server.Mobiles
{
	/// <summary>
	/// Centralized constants for Thief NPC calculations and mechanics.
	/// Extracted from Thief.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class ThiefConstants
	{
		#region NPC Title

		/// <summary>Title for Thief NPC</summary>
		public const string TITLE_THIEF = "o Ladino";

		#endregion

		#region Costs

		/// <summary>Base cost for unlocking a container</summary>
		public const int UNLOCK_COST_BASE = 1000;

		/// <summary>Multiplier for begging skill discount calculation</summary>
		public const double BEGGING_DISCOUNT_MULTIPLIER = 0.005;

		/// <summary>Minimum unlock cost (cannot be less than 1)</summary>
		public const int MIN_UNLOCK_COST = 1;

		#endregion

		#region Training System

		/// <summary>Region name where training is available</summary>
		public const string REGION_NAME_BASEMENT = "the Basement";

		/// <summary>Range required for training (in tiles)</summary>
		public const int TRAINING_RANGE = 1;

		/// <summary>Success chance threshold for training attempt (80%)</summary>
		public const double TRAINING_SUCCESS_CHANCE = 0.80;

		/// <summary>Minimum skill check value for training</summary>
		public const int TRAINING_SKILL_MIN = 0;

		/// <summary>Maximum skill check value for training</summary>
		public const int TRAINING_SKILL_MAX = 125;

		#endregion

		#region Ranges

		/// <summary>Target range for unlock service</summary>
		public const int TARGET_RANGE_UNLOCK = 12;

		#endregion

		#region Context Menu IDs

		/// <summary>Context menu ID for speech gump</summary>
		public const int CONTEXT_MENU_SPEECH_ID = 6146;

		/// <summary>Context menu range for speech gump</summary>
		public const int CONTEXT_MENU_SPEECH_RANGE = 3;

		/// <summary>Context menu ID for repair/unlock service</summary>
		public const int CONTEXT_MENU_REPAIR_ID = 6120;

		/// <summary>Context menu range for repair/unlock service</summary>
		public const int CONTEXT_MENU_REPAIR_RANGE = 12;

		#endregion

		#region Outfit Selection

		/// <summary>Minimum outfit selection index</summary>
		public const int OUTFIT_SELECTION_MIN = 0;

		/// <summary>Maximum outfit selection index</summary>
		public const int OUTFIT_SELECTION_MAX = 4;

		#endregion

		#region Sounds

		/// <summary>Sound ID played when unlocking a container</summary>
		public const int SOUND_UNLOCK = 0x241;

		#endregion

		#region Speech System

		/// <summary>Title for speech gump</summary>
		public const string SPEECH_GUMP_TITLE = "The Art Of Thievery";

		/// <summary>NPC type identifier for speech system</summary>
		public const string SPEECH_NPC_TYPE = "Thief";

		#endregion

		#region Skill Ranges

		/// <summary>Fencing skill minimum value</summary>
		public const double SKILL_FENCING_MIN = 55.0;

		/// <summary>Fencing skill maximum value</summary>
		public const double SKILL_FENCING_MAX = 78.0;

		/// <summary>DetectHidden skill minimum value</summary>
		public const double SKILL_DETECT_HIDDEN_MIN = 65.0;

		/// <summary>DetectHidden skill maximum value</summary>
		public const double SKILL_DETECT_HIDDEN_MAX = 88.0;

		/// <summary>Hiding skill minimum value</summary>
		public const double SKILL_HIDING_MIN = 45.0;

		/// <summary>Hiding skill maximum value</summary>
		public const double SKILL_HIDING_MAX = 68.0;

		/// <summary>RemoveTrap skill minimum value</summary>
		public const double SKILL_REMOVE_TRAP_MIN = 65.0;

		/// <summary>RemoveTrap skill maximum value</summary>
		public const double SKILL_REMOVE_TRAP_MAX = 88.0;

		/// <summary>Lockpicking skill minimum value</summary>
		public const double SKILL_LOCKPICKING_MIN = 60.0;

		/// <summary>Lockpicking skill maximum value</summary>
		public const double SKILL_LOCKPICKING_MAX = 83.0;

		/// <summary>Snooping skill minimum value</summary>
		public const double SKILL_SNOOPING_MIN = 65.0;

		/// <summary>Snooping skill maximum value</summary>
		public const double SKILL_SNOOPING_MAX = 88.0;

		/// <summary>Stealing skill minimum value</summary>
		public const double SKILL_STEALING_MIN = 65.0;

		/// <summary>Stealing skill maximum value</summary>
		public const double SKILL_STEALING_MAX = 88.0;

		/// <summary>Stealth skill minimum value</summary>
		public const double SKILL_STEALTH_MIN = 65.0;

		/// <summary>Stealth skill maximum value</summary>
		public const double SKILL_STEALTH_MAX = 88.0;

		#endregion
	}
}


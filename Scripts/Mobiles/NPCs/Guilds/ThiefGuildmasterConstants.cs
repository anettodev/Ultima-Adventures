namespace Server.Mobiles
{
	/// <summary>
	/// Centralized constants for ThiefGuildmaster NPC calculations and mechanics.
	/// Extracted from ThiefGuildmaster.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class ThiefGuildmasterConstants
	{
		#region NPC Title

		/// <summary>Title for ThiefGuildmaster NPC</summary>
		public const string TITLE_THIEF = "o Ladino";

		#endregion

		#region Skill Ranges

		/// <summary>DetectHidden skill minimum value</summary>
		public const double SKILL_DETECT_HIDDEN_MIN = 75.0;

		/// <summary>DetectHidden skill maximum value</summary>
		public const double SKILL_DETECT_HIDDEN_MAX = 98.0;

		/// <summary>Hiding skill minimum value</summary>
		public const double SKILL_HIDING_MIN = 65.0;

		/// <summary>Hiding skill maximum value</summary>
		public const double SKILL_HIDING_MAX = 88.0;

		/// <summary>Lockpicking skill minimum value</summary>
		public const double SKILL_LOCKPICKING_MIN = 85.0;

		/// <summary>Lockpicking skill maximum value</summary>
		public const double SKILL_LOCKPICKING_MAX = 100.0;

		/// <summary>Snooping skill minimum value</summary>
		public const double SKILL_SNOOPING_MIN = 90.0;

		/// <summary>Snooping skill maximum value</summary>
		public const double SKILL_SNOOPING_MAX = 100.0;

		/// <summary>Stealing skill minimum value</summary>
		public const double SKILL_STEALING_MIN = 90.0;

		/// <summary>Stealing skill maximum value</summary>
		public const double SKILL_STEALING_MAX = 100.0;

		/// <summary>Fencing skill minimum value</summary>
		public const double SKILL_FENCING_MIN = 75.0;

		/// <summary>Fencing skill maximum value</summary>
		public const double SKILL_FENCING_MAX = 98.0;

		/// <summary>Stealth skill minimum value</summary>
		public const double SKILL_STEALTH_MIN = 85.0;

		/// <summary>Stealth skill maximum value</summary>
		public const double SKILL_STEALTH_MAX = 100.0;

		/// <summary>RemoveTrap skill minimum value</summary>
		public const double SKILL_REMOVE_TRAP_MIN = 85.0;

		/// <summary>RemoveTrap skill maximum value</summary>
		public const double SKILL_REMOVE_TRAP_MAX = 100.0;

		#endregion

		#region Context Menu IDs

		/// <summary>Context menu ID for job entry</summary>
		public const int CONTEXT_MENU_JOB_ID = 2078;

		/// <summary>Context menu range for job entry</summary>
		public const int CONTEXT_MENU_JOB_RANGE = 3;

		/// <summary>Context menu ID for tithe entry</summary>
		public const int CONTEXT_MENU_TITHE_ID = 6198;

		/// <summary>Context menu range for tithe entry</summary>
		public const int CONTEXT_MENU_TITHE_RANGE = 4;

		#endregion

		#region Sounds

		/// <summary>Sound ID played when receiving a job note</summary>
		public const int SOUND_NOTE_RECEIVED = 0x249;

		#endregion

		#region Fame

		/// <summary>Minimum fame penalty for tithe service</summary>
		public const int TITHE_FAME_PENALTY_MIN = 200;

		/// <summary>Maximum fame penalty for tithe service</summary>
		public const int TITHE_FAME_PENALTY_MAX = 400;

		#endregion

		#region Localized Messages

		/// <summary>Localized message ID for welcome message</summary>
		public const int LOCALIZED_MSG_WELCOME = 1008053;

		#endregion

		#region Outfit Selection

		/// <summary>Minimum outfit selection index</summary>
		public const int OUTFIT_SELECTION_MIN = 0;

		/// <summary>Maximum outfit selection index</summary>
		public const int OUTFIT_SELECTION_MAX = 4;

		#endregion
	}
}


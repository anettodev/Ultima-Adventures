namespace Server.Mobiles
{
	/// <summary>
	/// Centralized constants for Healer NPC calculations and mechanics.
	/// Extracted from Healer.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class HealerConstants
	{
		#region NPC Title

		/// <summary>Title for Healer NPC</summary>
		public const string TITLE_HEALER = "the healer";

		#endregion

		#region Skill Ranges

		/// <summary>Minimum skill value for healer skills</summary>
		public const double SKILL_MIN = 80.0;

		/// <summary>Maximum skill value for healer skills</summary>
		public const double SKILL_MAX = 100.0;

		#endregion

		#region Context Menu IDs

		/// <summary>Context menu ID for speech gump</summary>
		public const int CONTEXT_MENU_SPEECH_ID = 6146;

		/// <summary>Context menu range for speech gump</summary>
		public const int CONTEXT_MENU_SPEECH_RANGE = 3;

		#endregion

		#region NameHue Values

		/// <summary>NameHue value for classic mode (non-AOS)</summary>
		public const int NAME_HUE_CLASSIC = 0x35;

		/// <summary>NameHue reset value for AOS mode</summary>
		public const int NAME_HUE_RESET = -1;

		#endregion

		#region Speech System

		/// <summary>NPC type identifier for speech system</summary>
		public const string SPEECH_NPC_TYPE = "Healer";

		#endregion

		#region Serialization

		/// <summary>Current serialization version</summary>
		public const int SERIALIZATION_VERSION = 0;

		#endregion
	}
}


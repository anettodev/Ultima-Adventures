namespace Server.Mobiles
{
	/// <summary>
	/// Centralized constants for TownHerald and DungeonGuide NPC calculations and mechanics.
	/// Extracted from TownHerald.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class TownHeraldConstants
	{
		#region Ranges

		/// <summary>Range at which herald talks to players</summary>
		public const int TALK_RANGE = 10;

		/// <summary>Range at which herald responds to speech</summary>
		public const int SPEECH_RANGE = 4;

		#endregion

		#region Time Delays

		/// <summary>Minimum delay between dungeon guide messages (seconds)</summary>
		public const int TALK_DELAY_MIN = 20;

		/// <summary>Maximum delay between dungeon guide messages (seconds)</summary>
		public const int TALK_DELAY_MAX = 45;

		/// <summary>Minimum delay between town herald messages (seconds)</summary>
		public const int HERALD_TALK_DELAY_MIN = 15;

		/// <summary>Maximum delay between town herald messages (seconds)</summary>
		public const int HERALD_TALK_DELAY_MAX = 30;

		/// <summary>Days to paralyze dungeon guide (10 years)</summary>
		public const int PARALYZE_DAYS = 3650;

		#endregion

		#region Probabilities

		/// <summary>Probability threshold for carrier message (0.70 = 70%)</summary>
		public const double CARRIER_MESSAGE_CHANCE = 0.70;

		#endregion

		#region Stats

		/// <summary>Base strength stat for town herald</summary>
		public const int STAT_STR = 100;

		/// <summary>Base dexterity stat for town herald</summary>
		public const int STAT_DEX = 100;

		/// <summary>Base intelligence stat for town herald</summary>
		public const int STAT_INT = 25;

		#endregion

		#region Appearance

		/// <summary>Default name hue for town herald</summary>
		public const int NAME_HUE_DEFAULT = -1;

		#endregion

		#region UI

		/// <summary>Context menu ID for town herald entry</summary>
		public const int CONTEXT_MENU_ID = 6146;

		/// <summary>Context menu range for town herald entry</summary>
		public const int CONTEXT_MENU_RANGE = 3;

		/// <summary>Default gump page number</summary>
		public const int GUMP_PAGE = 1;

		#endregion
	}
}


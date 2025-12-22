namespace Server.Misc
{
	/// <summary>
	/// Centralized constants for logging system calculations and mechanics.
	/// Extracted from LoggingFunctions.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class LoggingConstants
	{
		#region File Management

		/// <summary>Maximum number of lines to keep in log files before trimming</summary>
		public const int FILE_TRIM_LIMIT = 250;

		/// <summary>Base directory for log files</summary>
		public const string INFO_DIRECTORY = "Info";

		/// <summary>Directory for article files</summary>
		public const string ARTICLES_DIRECTORY = "Info/Articles";

		#endregion

		#region Random Counts

		/// <summary>Number of log types for random selection in LogShout</summary>
		public const int LOG_TYPE_COUNT = 7;

		/// <summary>Number of greeting variants for town crier</summary>
		public const int GREETING_VARIANT_COUNT = 4;

		/// <summary>Number of "no news" message variants</summary>
		public const int NO_NEWS_MESSAGE_COUNT = 4;

		/// <summary>Number of verb replacement variants</summary>
		public const int VERB_REPLACEMENT_COUNT = 4;

		/// <summary>Number of log types for random selection in LogSpeak</summary>
		public const int LOG_SPEAK_TYPE_COUNT = 6;

		/// <summary>Number of verb variants for LogSpeak</summary>
		public const int LOG_SPEAK_VERB_COUNT = 4;

		/// <summary>Number of trap verb variants</summary>
		public const int TRAP_VERB_COUNT = 7;

		/// <summary>Number of loot verb variants</summary>
		public const int LOOT_VERB_COUNT = 7;

		/// <summary>Number of boat loot verb variants</summary>
		public const int BOAT_LOOT_VERB_COUNT = 5;

		/// <summary>Number of corpse loot verb variants</summary>
		public const int CORPSE_LOOT_VERB_COUNT = 5;

		/// <summary>Number of slaying verb variants</summary>
		public const int SLAYING_VERB_COUNT = 4;

		/// <summary>Number of quest item verb variants</summary>
		public const int QUEST_ITEM_VERB_COUNT = 4;

		/// <summary>Number of quest body verb variants</summary>
		public const int QUEST_BODY_VERB_COUNT = 4;

		/// <summary>Number of quest body noun variants</summary>
		public const int QUEST_BODY_NOUN_COUNT = 4;

		/// <summary>Number of quest chest verb variants</summary>
		public const int QUEST_CHEST_VERB_COUNT = 4;

		/// <summary>Number of quest chest adjective variants</summary>
		public const int QUEST_CHEST_ADJECTIVE_COUNT = 4;

		/// <summary>Number of quest map verb variants</summary>
		public const int QUEST_MAP_VERB_COUNT = 4;

		/// <summary>Number of quest sea verb variants</summary>
		public const int QUEST_SEA_VERB_COUNT = 4;

		/// <summary>Number of quest kill verb variants</summary>
		public const int QUEST_KILL_VERB_COUNT = 4;

		/// <summary>Number of quest kill sea verb variants</summary>
		public const int QUEST_KILL_SEA_VERB_COUNT = 4;

		/// <summary>Number of quest kill assassin verb variants</summary>
		public const int QUEST_KILL_ASSASSIN_VERB_COUNT = 4;

		#endregion

		#region Probabilities

		/// <summary>Probability of logging loot events (25%)</summary>
		public const double LOOT_LOG_PROBABILITY = 0.25;

		#endregion

		#region HTML/Formatting

		/// <summary>HTML break tag</summary>
		public const string HTML_BREAK_TAG = "<br>";

		/// <summary>Color code for log entries display</summary>
		public const string COLOR_LOG_ENTRIES = "#FFC000";

		/// <summary>HTML basefont start tag with color</summary>
		public const string HTML_BASE_FONT_START = "<basefont color=#FFC000><big>";

		/// <summary>HTML basefont end tag</summary>
		public const string HTML_BASE_FONT_END = "</big></basefont>";

		#endregion

		#region Log Type Strings

		/// <summary>Log type identifier for adventures</summary>
		public const string LOG_TYPE_ADVENTURES = "Logging Adventures";

		/// <summary>Log type identifier for quests</summary>
		public const string LOG_TYPE_QUESTS = "Logging Quests";

		/// <summary>Log type identifier for battles</summary>
		public const string LOG_TYPE_BATTLES = "Logging Battles";

		/// <summary>Log type identifier for deaths</summary>
		public const string LOG_TYPE_DEATHS = "Logging Deaths";

		/// <summary>Log type identifier for murderers</summary>
		public const string LOG_TYPE_MURDERERS = "Logging Murderers";

		/// <summary>Log type identifier for journies</summary>
		public const string LOG_TYPE_JOURNIES = "Logging Journies";

		/// <summary>Log type identifier for misc events</summary>
		public const string LOG_TYPE_MISC = "Logging Misc";

		/// <summary>Log type identifier for server events</summary>
		public const string LOG_TYPE_SERVER = "Logging Server";

		#endregion

		#region File Paths

		/// <summary>File path for adventures log</summary>
		public const string FILE_PATH_ADVENTURES = "Info/adventures.txt";

		/// <summary>File path for quests log</summary>
		public const string FILE_PATH_QUESTS = "Info/quests.txt";

		/// <summary>File path for battles log</summary>
		public const string FILE_PATH_BATTLES = "Info/battles.txt";

		/// <summary>File path for deaths log</summary>
		public const string FILE_PATH_DEATHS = "Info/deaths.txt";

		/// <summary>File path for murderers log</summary>
		public const string FILE_PATH_MURDERERS = "Info/murderers.txt";

		/// <summary>File path for journies log</summary>
		public const string FILE_PATH_JOURNIES = "Info/journies.txt";

		/// <summary>File path for misc log</summary>
		public const string FILE_PATH_MISC = "Info/misc.txt";

		/// <summary>File path for server log</summary>
		public const string FILE_PATH_SERVER = "Info/server.txt";

		#endregion
	}
}


using Server.Mobiles;
using Server.Misc;

namespace Server.Misc.Helpers
{
	/// <summary>
	/// Helper class for building event strings and retrieving player titles.
	/// Extracted from LoggingFunctions.cs to eliminate code duplication and improve maintainability.
	/// </summary>
	public static class LoggingStringHelper
	{
		#region Public Methods

		/// <summary>
		/// Gets the player's title string, using custom title if available, otherwise skill title.
		/// </summary>
		/// <param name="mobile">The mobile to get the title for</param>
		/// <returns>The title string (e.g., "the Grandmaster Swordsman" or custom title)</returns>
		public static string GetPlayerTitle(Mobile mobile)
		{
			if (mobile == null)
				return "";

			if (mobile.Title != null && mobile.Title != "")
				return mobile.Title;

			return "the " + GetPlayerInfo.GetSkillTitle(mobile);
		}

		/// <summary>
		/// Builds a standard event string in the format: "Name Title Action#Date"
		/// </summary>
		/// <param name="mobile">The mobile performing the action</param>
		/// <param name="action">The action string</param>
		/// <returns>The formatted event string with date appended</returns>
		public static string BuildEventString(Mobile mobile, string action)
		{
			if (mobile == null || string.IsNullOrEmpty(action))
				return "";

			string title = GetPlayerTitle(mobile);
			string dateString = GetPlayerInfo.GetTodaysDate();

			return mobile.Name + " " + title + " " + action + "#" + dateString;
		}

		/// <summary>
		/// Builds an event string with a custom title parameter.
		/// </summary>
		/// <param name="mobile">The mobile performing the action</param>
		/// <param name="title">The title to use (overrides GetPlayerTitle)</param>
		/// <param name="action">The action string</param>
		/// <returns>The formatted event string with date appended</returns>
		public static string BuildEventStringWithTitle(Mobile mobile, string title, string action)
		{
			if (mobile == null || string.IsNullOrEmpty(action))
				return "";

			string dateString = GetPlayerInfo.GetTodaysDate();

			return mobile.Name + " " + title + " " + action + "#" + dateString;
		}

		/// <summary>
		/// Gets the default "no entries" message for a log type.
		/// </summary>
		/// <param name="logType">The log type identifier</param>
		/// <param name="playerName">The player's name for formatting</param>
		/// <returns>The formatted "no entries" message</returns>
		public static string GetNoEntriesMessage(string logType, string playerName)
		{
			if (string.IsNullOrEmpty(logType) || string.IsNullOrEmpty(playerName))
				return "";

			if (logType == LoggingConstants.LOG_TYPE_MURDERERS)
				return string.Format(LoggingStringConstants.MSG_NO_MURDERERS_FORMAT, playerName);
			else if (logType == LoggingConstants.LOG_TYPE_BATTLES)
				return string.Format(LoggingStringConstants.MSG_NO_BATTLES_FORMAT, playerName);
			else if (logType == LoggingConstants.LOG_TYPE_ADVENTURES)
				return string.Format(LoggingStringConstants.MSG_NO_ADVENTURES_FORMAT, playerName);
			else if (logType == LoggingConstants.LOG_TYPE_QUESTS)
				return string.Format(LoggingStringConstants.MSG_NO_QUESTS_FORMAT, playerName);
			else if (logType == LoggingConstants.LOG_TYPE_DEATHS)
				return string.Format(LoggingStringConstants.MSG_NO_DEATHS_FORMAT, playerName);
			else if (logType == LoggingConstants.LOG_TYPE_JOURNIES)
				return string.Format(LoggingStringConstants.MSG_NO_JOURNIES_FORMAT, playerName);
			else if (logType == LoggingConstants.LOG_TYPE_MISC)
				return string.Format(LoggingStringConstants.MSG_NO_MISC_FORMAT, playerName);
			else
				return string.Format(LoggingStringConstants.MSG_NOTHING_NEW_FORMAT, playerName);
		}

		/// <summary>
		/// Extracts the mobile name from a string that may contain brackets (e.g., "Name [Guild]").
		/// </summary>
		/// <param name="fullName">The full name string that may contain brackets</param>
		/// <returns>The name without brackets</returns>
		public static string ExtractNameWithoutBrackets(string fullName)
		{
			if (string.IsNullOrEmpty(fullName))
				return "";

			string[] parts = fullName.Split('[');
			return parts[0].TrimEnd();
		}

		#endregion
	}
}


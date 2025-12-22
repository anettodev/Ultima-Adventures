using System.Collections.Generic;
using Server.Misc;

namespace Server.Misc.Helpers
{
	/// <summary>
	/// Helper class for retrieving log file paths based on log type.
	/// Extracted from LoggingFunctions.cs to eliminate code duplication and improve maintainability.
	/// </summary>
	public static class LoggingPathHelper
	{
		#region File Path Dictionary

		/// <summary>
		/// Dictionary mapping log type strings to their file paths.
		/// </summary>
		private static readonly Dictionary<string, string> LogFilePaths = new Dictionary<string, string>
		{
			{ LoggingConstants.LOG_TYPE_ADVENTURES, LoggingConstants.FILE_PATH_ADVENTURES },
			{ LoggingConstants.LOG_TYPE_QUESTS, LoggingConstants.FILE_PATH_QUESTS },
			{ LoggingConstants.LOG_TYPE_BATTLES, LoggingConstants.FILE_PATH_BATTLES },
			{ LoggingConstants.LOG_TYPE_DEATHS, LoggingConstants.FILE_PATH_DEATHS },
			{ LoggingConstants.LOG_TYPE_MURDERERS, LoggingConstants.FILE_PATH_MURDERERS },
			{ LoggingConstants.LOG_TYPE_JOURNIES, LoggingConstants.FILE_PATH_JOURNIES },
			{ LoggingConstants.LOG_TYPE_MISC, LoggingConstants.FILE_PATH_MISC },
			{ LoggingConstants.LOG_TYPE_SERVER, LoggingConstants.FILE_PATH_SERVER }
		};

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the file path for a given log type.
		/// </summary>
		/// <param name="logType">The log type identifier (e.g., "Logging Adventures")</param>
		/// <returns>The file path for the log type, or default to adventures.txt if not found</returns>
		public static string GetFilePath(string logType)
		{
			if (string.IsNullOrEmpty(logType))
				return LoggingConstants.FILE_PATH_ADVENTURES;

			string filePath;
			if (LogFilePaths.TryGetValue(logType, out filePath))
				return filePath;

			// Default to adventures if log type not found
			return LoggingConstants.FILE_PATH_ADVENTURES;
		}

		#endregion
	}
}


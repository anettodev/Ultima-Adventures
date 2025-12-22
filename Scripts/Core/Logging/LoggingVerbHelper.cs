using Server.Misc;

namespace Server.Misc
{
	/// <summary>
	/// Helper class for verb replacement logic used in LogShout and LogSpeak methods.
	/// Extracted from LoggingFunctions.cs to eliminate code duplication and improve maintainability.
	/// </summary>
	public static class LoggingVerbHelper
	{
		#region Verb Replacement Arrays

		/// <summary>
		/// Array of verb replacements for "entered" in LogShout
		/// </summary>
		private static readonly string[] LogShoutEnteredVerbs = new string[]
		{
			LoggingStringConstants.VERB_ENTERED_0,
			LoggingStringConstants.VERB_ENTERED_1,
			LoggingStringConstants.VERB_ENTERED_2,
			LoggingStringConstants.VERB_ENTERED_3
		};

		/// <summary>
		/// Array of verb replacements for "left" in LogShout
		/// </summary>
		private static readonly string[] LogShoutLeftVerbs = new string[]
		{
			LoggingStringConstants.VERB_LEFT_0,
			LoggingStringConstants.VERB_LEFT_1,
			LoggingStringConstants.VERB_LEFT_2,
			LoggingStringConstants.VERB_LEFT_3
		};

		/// <summary>
		/// Array of verb replacements for "entered" in LogSpeak
		/// </summary>
		private static readonly string[] LogSpeakEnteredVerbs = new string[]
		{
			LoggingStringConstants.VERB_SPEAK_ENTERED_0,
			LoggingStringConstants.VERB_SPEAK_ENTERED_1,
			LoggingStringConstants.VERB_SPEAK_ENTERED_2,
			LoggingStringConstants.VERB_SPEAK_ENTERED_3
		};

		/// <summary>
		/// Array of verb replacements for "left" in LogSpeak
		/// </summary>
		private static readonly string[] LogSpeakLeftVerbs = new string[]
		{
			LoggingStringConstants.VERB_SPEAK_LEFT_0,
			LoggingStringConstants.VERB_SPEAK_LEFT_1,
			LoggingStringConstants.VERB_SPEAK_LEFT_2,
			LoggingStringConstants.VERB_SPEAK_LEFT_3
		};

		/// <summary>
		/// Array of verb replacements for "slain" in LogSpeak
		/// </summary>
		private static readonly string[] LogSpeakSlainVerbs = new string[]
		{
			LoggingStringConstants.VERB_SPEAK_SLAIN_0,
			LoggingStringConstants.VERB_SPEAK_SLAIN_1,
			LoggingStringConstants.VERB_SPEAK_SLAIN_2,
			LoggingStringConstants.VERB_SPEAK_SLAIN_3
		};

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets random verb replacements for LogShout method.
		/// </summary>
		/// <param name="enteredVerb">Output parameter for the "entered" replacement</param>
		/// <param name="leftVerb">Output parameter for the "left" replacement</param>
		public static void GetLogShoutVerbs(out string enteredVerb, out string leftVerb)
		{
			int index = Utility.Random(LoggingConstants.VERB_REPLACEMENT_COUNT);
			enteredVerb = LogShoutEnteredVerbs[index];
			leftVerb = LogShoutLeftVerbs[index];
		}

		/// <summary>
		/// Gets random verb replacements for LogSpeak method.
		/// </summary>
		/// <param name="enteredVerb">Output parameter for the "entered" replacement</param>
		/// <param name="leftVerb">Output parameter for the "left" replacement</param>
		/// <param name="slainVerb">Output parameter for the "slain" replacement</param>
		public static void GetLogSpeakVerbs(out string enteredVerb, out string leftVerb, out string slainVerb)
		{
			int index = Utility.Random(LoggingConstants.LOG_SPEAK_VERB_COUNT);
			enteredVerb = LogSpeakEnteredVerbs[index];
			leftVerb = LogSpeakLeftVerbs[index];
			slainVerb = LogSpeakSlainVerbs[index];
		}

		/// <summary>
		/// Applies verb replacements to a message string for LogShout.
		/// </summary>
		/// <param name="message">The message to process</param>
		/// <returns>The message with verb replacements applied</returns>
		public static string ApplyLogShoutReplacements(string message)
		{
			if (string.IsNullOrEmpty(message))
				return message;

			string enteredVerb;
			string leftVerb;
			GetLogShoutVerbs(out enteredVerb, out leftVerb);

			if (message.Contains(LoggingStringConstants.ENGLISH_ENTERED))
			{
				message = message.Replace(LoggingStringConstants.ENGLISH_ENTERED, " " + enteredVerb + " ");
			}

			if (message.Contains(LoggingStringConstants.ENGLISH_LEFT))
			{
				message = message.Replace(LoggingStringConstants.ENGLISH_LEFT, " " + leftVerb + " ");
			}

			return message;
		}

		/// <summary>
		/// Applies verb replacements to a message string for LogSpeak.
		/// </summary>
		/// <param name="message">The message to process</param>
		/// <returns>The message with verb replacements applied</returns>
		public static string ApplyLogSpeakReplacements(string message)
		{
			if (string.IsNullOrEmpty(message))
				return message;

			string enteredVerb;
			string leftVerb;
			string slainVerb;
			GetLogSpeakVerbs(out enteredVerb, out leftVerb, out slainVerb);

			if (message.Contains(LoggingStringConstants.ENGLISH_HAD_BEEN))
			{
				message = message.Replace(LoggingStringConstants.ENGLISH_HAD_BEEN, " sendo ");
			}

			if (message.Contains(LoggingStringConstants.ENGLISH_HAD_SLAIN))
			{
				message = message.Replace(LoggingStringConstants.ENGLISH_HAD_SLAIN, " " + slainVerb + " ");
			}

			if (message.Contains(LoggingStringConstants.ENGLISH_HAD_KILLED))
			{
				message = message.Replace(LoggingStringConstants.ENGLISH_HAD_KILLED, " acidentalmente matando ");
			}

			if (message.Contains(LoggingStringConstants.ENGLISH_FATAL_MISTAKE))
			{
				message = message.Replace(LoggingStringConstants.ENGLISH_FATAL_MISTAKE, " cometendo um erro fatal ");
			}

			if (message.Contains(LoggingStringConstants.ENGLISH_ENTERED))
			{
				message = message.Replace(LoggingStringConstants.ENGLISH_ENTERED, " " + enteredVerb + " ");
			}

			if (message.Contains(LoggingStringConstants.ENGLISH_LEFT))
			{
				message = message.Replace(LoggingStringConstants.ENGLISH_LEFT, " " + leftVerb + " ");
			}

			return message;
		}

		#endregion
	}
}


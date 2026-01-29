using System;
using System.Text;
using Server.Misc;

namespace Server.Misc.Helpers
{
	/// <summary>
	/// Helper class for building JSON responses in StatusAPI.
	/// Extracted from StatusAPI.cs to improve performance and reduce complexity.
	/// Uses pre-allocated StringBuilder to minimize memory allocations.
	/// </summary>
	public static class StatusAPIJSONBuilder
	{
		/// <summary>
		/// Thread-local StringBuilder to avoid allocations in hot path
		/// </summary>
		[ThreadStatic]
		private static StringBuilder jsonBuilder;

		/// <summary>
		/// Thread-local StringBuilder for JSON escaping operations
		/// </summary>
		[ThreadStatic]
		private static StringBuilder escapeBuilder;

		/// <summary>
		/// Gets or creates a StringBuilder instance for the current thread
		/// </summary>
		private static StringBuilder GetBuilder()
		{
			if (jsonBuilder == null)
			{
				jsonBuilder = new StringBuilder(StatusAPIConstants.JSON_BUILDER_CAPACITY);
			}
			else
			{
				jsonBuilder.Clear();
			}
			return jsonBuilder;
		}

		/// <summary>
		/// Builds the status JSON response with server information
		/// </summary>
		/// <param name="playersOnline">Current number of players online</param>
		/// <param name="uptime">Server uptime TimeSpan</param>
		/// <returns>JSON string with server status</returns>
		public static string BuildStatusJSON(int playersOnline, TimeSpan uptime)
		{
			StringBuilder sb = GetBuilder();

			sb.Append("{");
			sb.Append("\"isOnline\":true,");
			AppendJSONNumber(sb, "playersOnline", playersOnline, true);
			AppendJSONNumber(sb, "maxPlayers", StatusAPIConstants.MAX_PLAYERS, true);
			AppendUptimeObject(sb, uptime, true);

			// Server statistics
			AppendJSONNumber(sb, "activeAccounts", StatusAPIServerStats.GetActiveAccountCount(), true);
			AppendJSONNumber(sb, "bannedAccounts", StatusAPIServerStats.GetBannedAccountCount(), true);
			AppendJSONNumber(sb, "firewalled", StatusAPIServerStats.GetFirewalledCount(), true);
			AppendJSONNumber(sb, "mobiles", StatusAPIServerStats.GetMobileCount(), true);
			AppendJSONNumber(sb, "items", StatusAPIServerStats.GetItemCount(), true);

			// System information
			AppendJSONNumber(sb, "memoryMB", StatusAPIServerStats.GetMemoryUsageMB(), true);
			AppendJSONNumber(sb, "cpuPercent", StatusAPIServerStats.GetCpuUsagePercent(), true);
			AppendJSONProperty(sb, "framework", StatusAPIServerStats.GetFrameworkVersion(), true);
			AppendJSONProperty(sb, "operatingSystem", StatusAPIServerStats.GetOperatingSystem(), true);

			AppendJSONProperty(sb, "timestamp", DateTime.UtcNow.ToString(StatusAPIConstants.TIMESTAMP_FORMAT), false);
			sb.Append("}");

			return sb.ToString();
		}

		/// <summary>
		/// Builds an error JSON response
		/// </summary>
		/// <param name="message">Error message</param>
		/// <param name="statusCode">HTTP status code</param>
		/// <returns>JSON string with error information</returns>
		public static string BuildErrorJSON(string message, int statusCode)
		{
			StringBuilder sb = GetBuilder();
			
			sb.Append("{");
			AppendJSONProperty(sb, "error", message, true);
			AppendJSONNumber(sb, "statusCode", statusCode, false);
			sb.Append("}");

			return sb.ToString();
		}

		/// <summary>
		/// Appends a JSON property (string value) to the StringBuilder
		/// </summary>
		private static void AppendJSONProperty(StringBuilder sb, string key, string value, bool addComma)
		{
			sb.AppendFormat("\"{0}\":\"{1}\"", key, EscapeJSON(value));
			if (addComma)
			{
				sb.Append(",");
			}
		}

		/// <summary>
		/// Appends a JSON property (number value) to the StringBuilder
		/// </summary>
		private static void AppendJSONNumber(StringBuilder sb, string key, int value, bool addComma)
		{
			sb.AppendFormat("\"{0}\":{1}", key, value);
			if (addComma)
			{
				sb.Append(",");
			}
		}

		/// <summary>
		/// Appends a JSON property (long value) to the StringBuilder
		/// </summary>
		private static void AppendJSONNumber(StringBuilder sb, string key, long value, bool addComma)
		{
			sb.AppendFormat("\"{0}\":{1}", key, value);
			if (addComma)
			{
				sb.Append(",");
			}
		}

		/// <summary>
		/// Appends a JSON property (double value) to the StringBuilder
		/// </summary>
		private static void AppendJSONNumber(StringBuilder sb, string key, double value, bool addComma)
		{
			sb.AppendFormat("\"{0}\":{1}", key, value);
			if (addComma)
			{
				sb.Append(",");
			}
		}

		/// <summary>
		/// Appends the uptime object to the JSON
		/// </summary>
		private static void AppendUptimeObject(StringBuilder sb, TimeSpan uptime, bool addComma)
		{
			sb.Append("\"uptime\":{");
			AppendJSONNumber(sb, "days", uptime.Days, true);
			AppendJSONNumber(sb, "hours", uptime.Hours, true);
			AppendJSONNumber(sb, "minutes", uptime.Minutes, true);
			AppendJSONNumber(sb, "seconds", uptime.Seconds, true);
			AppendJSONNumber(sb, "totalSeconds", (long)uptime.TotalSeconds, false);
			sb.Append("}");
			if (addComma)
			{
				sb.Append(",");
			}
		}

		/// <summary>
		/// Escapes special characters in JSON strings using single-pass algorithm
		/// Optimized for performance - single pass through string using thread-local StringBuilder
		/// </summary>
		/// <param name="text">Text to escape</param>
		/// <returns>Escaped JSON string</returns>
		private static string EscapeJSON(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}

			// Use thread-local StringBuilder to avoid allocations
			if (escapeBuilder == null)
			{
				escapeBuilder = new StringBuilder(256);
			}
			else
			{
				escapeBuilder.Clear();
			}

			// Ensure capacity
			if (escapeBuilder.Capacity < text.Length * 2)
			{
				escapeBuilder.Capacity = text.Length * 2;
			}

			// Single-pass escape - more efficient than multiple Replace() calls
			foreach (char c in text)
			{
				switch (c)
				{
					case '\\':
						escapeBuilder.Append("\\\\");
						break;
					case '"':
						escapeBuilder.Append("\\\"");
						break;
					case '\n':
						escapeBuilder.Append("\\n");
						break;
					case '\r':
						escapeBuilder.Append("\\r");
						break;
					case '\t':
						escapeBuilder.Append("\\t");
						break;
					default:
						escapeBuilder.Append(c);
						break;
				}
			}
			return escapeBuilder.ToString();
		}
	}
}

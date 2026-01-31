using System;

namespace Server.Misc
{
	/// <summary>
	/// Helper class for routing HTTP requests in StatusAPI.
	/// Extracted from StatusAPI.cs to reduce complexity and improve maintainability.
	/// </summary>
	public static class StatusAPIRequestRouter
	{
		/// <summary>
		/// Checks if the HTTP method is valid (GET or OPTIONS)
		/// </summary>
		/// <param name="method">HTTP method string</param>
		/// <returns>True if method is GET or OPTIONS</returns>
		public static bool IsValidMethod(string method)
		{
			return method == "GET" || method == "OPTIONS";
		}

		/// <summary>
		/// Checks if the request is an OPTIONS preflight request
		/// </summary>
		/// <param name="method">HTTP method string</param>
		/// <returns>True if method is OPTIONS</returns>
		public static bool IsOptionsRequest(string method)
		{
			return method == "OPTIONS";
		}

		/// <summary>
		/// Checks if the path matches the status endpoint
		/// </summary>
		/// <param name="path">Request path</param>
		/// <returns>True if path is /status or /</returns>
		public static bool IsStatusEndpoint(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}

			return string.Equals(path, StatusAPIConstants.ENDPOINT_STATUS, StringComparison.OrdinalIgnoreCase)
				|| string.Equals(path, StatusAPIConstants.ENDPOINT_ROOT, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Checks if the path matches the ping endpoint
		/// </summary>
		/// <param name="path">Request path</param>
		/// <returns>True if path is /ping</returns>
		public static bool IsPingEndpoint(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}

			return string.Equals(path, StatusAPIConstants.ENDPOINT_PING, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Gets the normalized path from request URL
		/// </summary>
		/// <param name="absolutePath">Absolute path from request URL</param>
		/// <returns>Normalized path or "/" if null/empty</returns>
		public static string GetNormalizedPath(string absolutePath)
		{
			if (string.IsNullOrEmpty(absolutePath))
			{
				return StatusAPIConstants.ENDPOINT_ROOT;
			}

			return absolutePath;
		}
	}
}

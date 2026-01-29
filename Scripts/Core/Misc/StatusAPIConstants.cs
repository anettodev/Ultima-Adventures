namespace Server.Misc
{
	/// <summary>
	/// Centralized constants for StatusAPI HTTP server configuration.
	/// Extracted from StatusAPI.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class StatusAPIConstants
	{
		#region Network Configuration

		/// <summary>HTTP listener port number</summary>
		public const int PORT = 8080;

		/// <summary>Status endpoint path</summary>
		public const string ENDPOINT_STATUS = "/status";

		/// <summary>Ping endpoint path for latency measurement</summary>
		public const string ENDPOINT_PING = "/ping";

		/// <summary>Root endpoint path</summary>
		public const string ENDPOINT_ROOT = "/";

		/// <summary>HTTP prefix format for Mono compatibility (use + instead of *)</summary>
		public const string PREFIX_FORMAT = "http://+:{0}{1}/";

		#endregion

		#region Server Configuration

		/// <summary>Maximum number of players the server supports</summary>
		public const int MAX_PLAYERS = 50;

		#endregion

		#region HTTP Configuration

		/// <summary>JSON content type header</summary>
		public const string CONTENT_TYPE_JSON = "application/json; charset=utf-8";

		/// <summary>Plain text content type header</summary>
		public const string CONTENT_TYPE_TEXT = "text/plain; charset=utf-8";

		/// <summary>CORS allow origin header value (allows all origins)</summary>
		public const string CORS_ALLOW_ORIGIN = "*";

		/// <summary>CORS allow methods header value</summary>
		public const string CORS_ALLOW_METHODS = "GET, OPTIONS";

		/// <summary>CORS allow headers header value</summary>
		public const string CORS_ALLOW_HEADERS = "Content-Type";

		#endregion

		#region HTTP Status Codes

		/// <summary>HTTP 200 OK status code</summary>
		public const int STATUS_CODE_OK = 200;

		/// <summary>HTTP 404 Not Found status code</summary>
		public const int STATUS_CODE_NOT_FOUND = 404;

		/// <summary>HTTP 405 Method Not Allowed status code</summary>
		public const int STATUS_CODE_METHOD_NOT_ALLOWED = 405;

		/// <summary>HTTP 500 Internal Server Error status code</summary>
		public const int STATUS_CODE_INTERNAL_SERVER_ERROR = 500;

		#endregion

		#region Performance Configuration

		/// <summary>Delay in milliseconds when retrying after an error in listener loop</summary>
		public const int ERROR_RETRY_DELAY_MS = 1000;

		/// <summary>Initial capacity for JSON StringBuilder (reduces allocations)</summary>
		public const int JSON_BUILDER_CAPACITY = 512;

		/// <summary>Timestamp format string for ISO 8601 format</summary>
		public const string TIMESTAMP_FORMAT = "yyyy-MM-ddTHH:mm:ssZ";

		#endregion
	}
}

namespace Server.Misc
{
	/// <summary>
	/// Centralized string constants for StatusAPI messages and configuration.
	/// Extracted from StatusAPI.cs to improve maintainability and enable localization.
	/// </summary>
	public static class StatusAPIStringConstants
	{
		#region Logging Prefixes

		/// <summary>Log prefix for StatusAPI messages</summary>
		public const string LOG_PREFIX = "[Status API]";

		#endregion

		#region HTTP Responses

		/// <summary>Ping endpoint response text</summary>
		public const string PING_RESPONSE = "pong";

		#endregion

		#region Error Messages

		/// <summary>Error message when JSON generation fails</summary>
		public const string ERROR_GENERATE_STATUS = "Failed to generate status";

		/// <summary>HTTP 405 Method Not Allowed error message</summary>
		public const string ERROR_METHOD_NOT_ALLOWED = "Method Not Allowed";

		/// <summary>HTTP 404 Not Found error message</summary>
		public const string ERROR_NOT_FOUND = "Not Found";

		/// <summary>HTTP 500 Internal Server Error message</summary>
		public const string ERROR_INTERNAL_SERVER = "Internal Server Error";

		#endregion

		#region Log Messages

		/// <summary>Log message when already initialized</summary>
		public const string LOG_ALREADY_INITIALIZED = "Already initialized, ignoring duplicate call";

		/// <summary>Log message when initializing listener</summary>
		public const string LOG_INITIALIZING = "Initializing HTTP listener on port {0}...";

		/// <summary>Log message when starting listener</summary>
		public const string LOG_STARTING = "Attempting to start listener...";

		/// <summary>Log message when listener started successfully</summary>
		public const string LOG_STARTED = "HTTP listener started successfully!";

		/// <summary>Log message showing listener address</summary>
		public const string LOG_LISTENING_ON = "Listening on: http://localhost:{0}{1}";

		/// <summary>Log message for CORS enabled</summary>
		public const string LOG_CORS_ENABLED = "CORS enabled for all origins";

		/// <summary>Log message when listener thread created</summary>
		public const string LOG_THREAD_CREATED = "Listener thread created and started";

		/// <summary>Log message when shutting down</summary>
		public const string LOG_SHUTTING_DOWN = "Shutting down HTTP listener...";

		/// <summary>Log message when stopped</summary>
		public const string LOG_STOPPED = "HTTP listener stopped";

		/// <summary>Log message when listener thread starts</summary>
		public const string LOG_THREAD_STARTED = "=== Listener thread STARTED ===";

		/// <summary>Log message when listener thread stops</summary>
		public const string LOG_THREAD_STOPPED = "=== Listener thread STOPPED ===";

		#endregion

		#region Error Log Messages

		/// <summary>Error log prefix</summary>
		public const string LOG_ERROR = "ERROR";

		/// <summary>Error message when listener fails to start</summary>
		public const string LOG_ERROR_START_FAILED = "Failed to start HTTP listener: {0}";

		/// <summary>Error message for port/permissions</summary>
		public const string LOG_ERROR_PORT_PERMISSIONS = "Make sure port {0} is not in use and you have administrator privileges";

		/// <summary>Error message during shutdown</summary>
		public const string LOG_ERROR_SHUTDOWN = "ERROR during shutdown: {0}";

		/// <summary>Error message in listener loop</summary>
		public const string LOG_ERROR_LISTENER_LOOP = "ERROR in listener loop: {0}";

		/// <summary>Error message when handling request</summary>
		public const string LOG_ERROR_HANDLING_REQUEST = "ERROR handling request: {0}";

		/// <summary>Error message when sending error response fails</summary>
		public const string LOG_ERROR_SENDING_ERROR = "ERROR sending error response: {0}";

		/// <summary>Error message when generating JSON fails</summary>
		public const string LOG_ERROR_GENERATING_JSON = "ERROR generating JSON: {0}";

		#endregion
	}
}

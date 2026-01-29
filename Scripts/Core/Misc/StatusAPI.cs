using System;
using System.Net;
using System.Text;
using System.Threading;
using Server;
using Server.Network;

namespace Server.Misc
{
	/// <summary>
	/// HTTP Status API Server - Provides real-time server status via HTTP endpoint
	/// Endpoint: GET http://YOUR_SERVER_IP:8080/status
	/// Optimized for performance with reduced logging in hot path and efficient JSON generation
	/// </summary>
	public class StatusAPI
	{
		#region Fields

		private static HttpListener listener;
		private static Thread listenerThread;
		private static volatile bool isRunning = false;
		private static volatile bool hasInitialized = false;
		private static DateTime serverStartTime;

		#endregion

		#region Initialization

		/// <summary>
		/// Initialize and start the Status API server
		/// Called automatically when the server starts
		/// </summary>
		public static void Initialize()
		{
			// Prevent double initialization from ANY source
			if (hasInitialized)
			{
				LogMessage(StatusAPIStringConstants.LOG_ALREADY_INITIALIZED);
				return;
			}

			hasInitialized = true;
			serverStartTime = DateTime.UtcNow;

			try
			{
				LogMessage(string.Format(StatusAPIStringConstants.LOG_INITIALIZING, StatusAPIConstants.PORT));

				listener = new HttpListener();

				// IMPORTANT: Use + instead of * for Mono compatibility
				listener.Prefixes.Add(string.Format(StatusAPIConstants.PREFIX_FORMAT, StatusAPIConstants.PORT, StatusAPIConstants.ENDPOINT_STATUS));
				listener.Prefixes.Add(string.Format(StatusAPIConstants.PREFIX_FORMAT, StatusAPIConstants.PORT, StatusAPIConstants.ENDPOINT_ROOT));
				listener.Prefixes.Add(string.Format(StatusAPIConstants.PREFIX_FORMAT, StatusAPIConstants.PORT, StatusAPIConstants.ENDPOINT_PING));

				LogMessage(StatusAPIStringConstants.LOG_STARTING);
				listener.Start();
				isRunning = true;

				LogMessage(StatusAPIStringConstants.LOG_STARTED);
				LogMessage(string.Format(StatusAPIStringConstants.LOG_LISTENING_ON, StatusAPIConstants.PORT, StatusAPIConstants.ENDPOINT_STATUS));
				LogMessage(StatusAPIStringConstants.LOG_CORS_ENABLED);

				listenerThread = new Thread(new ThreadStart(ListenForRequests));
				listenerThread.IsBackground = true;
				listenerThread.Name = "Status API Listener";
				listenerThread.Start();

				LogMessage(StatusAPIStringConstants.LOG_THREAD_CREATED);

				// Initialize CPU monitor
				Helpers.StatusAPICpuMonitor.Initialize();
			}
			catch (Exception ex)
			{
				LogError(string.Format(StatusAPIStringConstants.LOG_ERROR_START_FAILED, ex.Message));
				LogError(string.Format(StatusAPIStringConstants.LOG_ERROR_PORT_PERMISSIONS, StatusAPIConstants.PORT));
				if (ex.StackTrace != null)
				{
					LogError("Stack trace: " + ex.StackTrace);
				}
			}
		}

		#endregion

		#region Shutdown

		/// <summary>
		/// Stop the Status API server
		/// Called when the server shuts down
		/// </summary>
		public static void Shutdown()
		{
			try
			{
				if (isRunning)
				{
					LogMessage(StatusAPIStringConstants.LOG_SHUTTING_DOWN);
					isRunning = false;

					if (listener != null && listener.IsListening)
					{
						listener.Stop();
						listener.Close();
					}

					// Shutdown CPU monitor
					Helpers.StatusAPICpuMonitor.Shutdown();

					LogMessage(StatusAPIStringConstants.LOG_STOPPED);
				}
			}
			catch (Exception ex)
			{
				LogError(string.Format(StatusAPIStringConstants.LOG_ERROR_SHUTDOWN, ex.Message));
			}
		}

		#endregion

		#region Request Listener

		/// <summary>
		/// Main listener loop - processes incoming HTTP requests
		/// Optimized: Removed excessive logging from hot path
		/// </summary>
		private static void ListenForRequests()
		{
			LogMessage(StatusAPIStringConstants.LOG_THREAD_STARTED);

			while (isRunning)
			{
				try
				{
					// Wait for incoming request (blocking call)
					HttpListenerContext context = listener.GetContext();

					// Process request on thread pool to avoid blocking
					ThreadPool.QueueUserWorkItem((_) => HandleRequest(context));
				}
				catch (HttpListenerException ex)
				{
					// Listener was stopped, exit gracefully
					LogError(string.Format("HttpListenerException: {0} (Error code: {1})", ex.Message, ex.ErrorCode));
					break;
				}
				catch (InvalidOperationException ex)
				{
					LogError(string.Format("InvalidOperationException: {0}", ex.Message));
					break;
				}
				catch (Exception ex)
				{
					LogError(string.Format(StatusAPIStringConstants.LOG_ERROR_LISTENER_LOOP, ex.Message));
					LogError(string.Format("Exception type: {0}", ex.GetType().Name));
					if (ex.StackTrace != null)
					{
						LogError("Stack trace: " + ex.StackTrace);
					}
					Thread.Sleep(StatusAPIConstants.ERROR_RETRY_DELAY_MS); // Prevent tight loop on errors
				}
			}

			LogMessage(StatusAPIStringConstants.LOG_THREAD_STOPPED);
		}

		#endregion

		#region Request Handling

		/// <summary>
		/// Handle individual HTTP request
		/// Optimized: Uses helper classes to reduce complexity
		/// </summary>
		private static void HandleRequest(HttpListenerContext context)
		{
			HttpListenerRequest request = context.Request;
			HttpListenerResponse response = context.Response;

			try
			{
				// Add CORS headers (allow all origins for public API)
				AddCORSHeaders(response);

				// Handle OPTIONS preflight request (CORS)
				if (Helpers.StatusAPIRequestRouter.IsOptionsRequest(request.HttpMethod))
				{
					response.StatusCode = StatusAPIConstants.STATUS_CODE_OK;
					response.Close();
					return;
				}

				// Only accept GET requests
				if (!Helpers.StatusAPIRequestRouter.IsValidMethod(request.HttpMethod))
				{
					SendErrorResponse(response, StatusAPIConstants.STATUS_CODE_METHOD_NOT_ALLOWED, StatusAPIStringConstants.ERROR_METHOD_NOT_ALLOWED);
					return;
				}

				// Get normalized path
				string path = Helpers.StatusAPIRequestRouter.GetNormalizedPath(request.Url?.AbsolutePath);

				// Handle ping endpoint
				if (Helpers.StatusAPIRequestRouter.IsPingEndpoint(path))
				{
					SendPingResponse(response);
					return;
				}

				// Handle status endpoint
				if (Helpers.StatusAPIRequestRouter.IsStatusEndpoint(path))
				{
					SendStatusResponse(response);
					return;
				}

				// Invalid path
				SendErrorResponse(response, StatusAPIConstants.STATUS_CODE_NOT_FOUND, StatusAPIStringConstants.ERROR_NOT_FOUND);
			}
			catch (Exception ex)
			{
				LogError(string.Format(StatusAPIStringConstants.LOG_ERROR_HANDLING_REQUEST, ex.Message));
				if (ex.StackTrace != null)
				{
					LogError("Stack trace: " + ex.StackTrace);
				}
				try
				{
					SendErrorResponse(response, StatusAPIConstants.STATUS_CODE_INTERNAL_SERVER_ERROR, StatusAPIStringConstants.ERROR_INTERNAL_SERVER);
				}
				catch (Exception ex2)
				{
					LogError(string.Format(StatusAPIStringConstants.LOG_ERROR_SENDING_ERROR, ex2.Message));
				}
			}
		}

		/// <summary>
		/// Adds CORS headers to the response
		/// </summary>
		private static void AddCORSHeaders(HttpListenerResponse response)
		{
			response.AddHeader("Access-Control-Allow-Origin", StatusAPIConstants.CORS_ALLOW_ORIGIN);
			response.AddHeader("Access-Control-Allow-Methods", StatusAPIConstants.CORS_ALLOW_METHODS);
			response.AddHeader("Access-Control-Allow-Headers", StatusAPIConstants.CORS_ALLOW_HEADERS);
		}

		/// <summary>
		/// Sends ping response (lightweight endpoint for latency measurement)
		/// </summary>
		private static void SendPingResponse(HttpListenerResponse response)
		{
			response.StatusCode = StatusAPIConstants.STATUS_CODE_OK;
			response.ContentType = StatusAPIConstants.CONTENT_TYPE_TEXT;
			
			byte[] buffer = Encoding.UTF8.GetBytes(StatusAPIStringConstants.PING_RESPONSE);
			response.ContentLength64 = buffer.Length;
			response.OutputStream.Write(buffer, 0, buffer.Length);
			response.OutputStream.Close();
		}

		/// <summary>
		/// Generates and sends status JSON response
		/// </summary>
		private static void SendStatusResponse(HttpListenerResponse response)
		{
			try
			{
				// Get current player count
				int playersOnline = NetState.Instances.Count;

				// Calculate uptime
				TimeSpan uptime = DateTime.UtcNow - serverStartTime;

				// Generate JSON using optimized builder
				string jsonResponse = Helpers.StatusAPIJSONBuilder.BuildStatusJSON(playersOnline, uptime);
				SendJSONResponse(response, jsonResponse);
			}
			catch (Exception ex)
			{
				LogError(string.Format(StatusAPIStringConstants.LOG_ERROR_GENERATING_JSON, ex.Message));
				// Return minimal valid JSON on error
				string errorJSON = Helpers.StatusAPIJSONBuilder.BuildErrorJSON(StatusAPIStringConstants.ERROR_GENERATE_STATUS, StatusAPIConstants.STATUS_CODE_INTERNAL_SERVER_ERROR);
				SendJSONResponse(response, errorJSON);
			}
		}

		#endregion

		#region Response Helpers

		/// <summary>
		/// Send JSON response
		/// </summary>
		private static void SendJSONResponse(HttpListenerResponse response, string json)
		{
			response.ContentType = StatusAPIConstants.CONTENT_TYPE_JSON;
			response.StatusCode = StatusAPIConstants.STATUS_CODE_OK;

			byte[] buffer = Encoding.UTF8.GetBytes(json);
			response.ContentLength64 = buffer.Length;
			response.OutputStream.Write(buffer, 0, buffer.Length);
			response.OutputStream.Close();
		}

		/// <summary>
		/// Send error response
		/// </summary>
		private static void SendErrorResponse(HttpListenerResponse response, int statusCode, string message)
		{
			response.StatusCode = statusCode;
			response.ContentType = StatusAPIConstants.CONTENT_TYPE_JSON;

			string json = Helpers.StatusAPIJSONBuilder.BuildErrorJSON(message, statusCode);
			byte[] buffer = Encoding.UTF8.GetBytes(json);
			response.ContentLength64 = buffer.Length;
			response.OutputStream.Write(buffer, 0, buffer.Length);
			response.OutputStream.Close();
		}

		#endregion

		#region Logging Helpers

		/// <summary>
		/// Logs a standard message with prefix
		/// </summary>
		private static void LogMessage(string message)
		{
			Console.WriteLine("{0} {1}", StatusAPIStringConstants.LOG_PREFIX, message);
		}

		/// <summary>
		/// Logs an error message with prefix
		/// </summary>
		private static void LogError(string message)
		{
			Console.WriteLine("{0} {1}: {2}", StatusAPIStringConstants.LOG_PREFIX, StatusAPIStringConstants.LOG_ERROR, message);
		}

		#endregion
	}
}

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Server;
using Server.Network;

namespace Server.Misc
{
    /// <summary>
    /// HTTP Status API Server - FIXED VERSION FOR MONO/LINUX
    /// Provides real-time server status via HTTP endpoint on port 8080
    /// Endpoint: GET http://YOUR_SERVER_IP:8080/status
    /// </summary>
    public class StatusAPI
    {
        private static HttpListener listener;
        private static Thread listenerThread;
        private static bool isRunning = false;
        private static bool hasInitialized = false;
        private static DateTime serverStartTime;

        // Configuration
        private const int PORT = 8080;
        private const string ENDPOINT = "/status";
        private const int MAX_PLAYERS = 100; // Update this to your server's max players

        /// <summary>
        /// Initialize and start the Status API server
        /// Called automatically when the server starts
        /// </summary>
        public static void Initialize()
        {
            // Prevent double initialization from ANY source
            if (hasInitialized)
            {
                Console.WriteLine("[Status API] Already initialized, ignoring duplicate call");
                return;
            }

            hasInitialized = true;
            serverStartTime = DateTime.UtcNow;

            try
            {
                Console.WriteLine("[Status API] Initializing HTTP listener on port {0}...", PORT);

                listener = new HttpListener();

                // IMPORTANT: Use + instead of * for Mono compatibility
                listener.Prefixes.Add($"http://+:{PORT}{ENDPOINT}/");
                listener.Prefixes.Add($"http://+:{PORT}/"); // Root endpoint

                Console.WriteLine("[Status API] Attempting to start listener...");
                listener.Start();
                isRunning = true;

                Console.WriteLine("[Status API] HTTP listener started successfully!");
                Console.WriteLine("[Status API] Listening on: http://localhost:{0}{1}", PORT, ENDPOINT);
                Console.WriteLine("[Status API] CORS enabled for all origins");
                Console.WriteLine("[Status API] IsListening: {0}", listener.IsListening);

                listenerThread = new Thread(new ThreadStart(ListenForRequests));
                listenerThread.IsBackground = true;
                listenerThread.Name = "Status API Listener";
                listenerThread.Start();

                Console.WriteLine("[Status API] Listener thread created and started");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Status API] ERROR: Failed to start HTTP listener: {0}", ex.Message);
                Console.WriteLine("[Status API] Stack trace: {0}", ex.StackTrace);
                Console.WriteLine("[Status API] Make sure port {0} is not in use and you have administrator privileges", PORT);
            }
        }

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
                    Console.WriteLine("[Status API] Shutting down HTTP listener...");
                    isRunning = false;

                    if (listener != null && listener.IsListening)
                    {
                        listener.Stop();
                        listener.Close();
                    }

                    Console.WriteLine("[Status API] HTTP listener stopped");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Status API] ERROR during shutdown: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Main listener loop - processes incoming HTTP requests
        /// </summary>
        private static void ListenForRequests()
        {
            Console.WriteLine("[Status API] === Listener thread STARTED ===");
            Console.WriteLine("[Status API] Thread ID: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("[Status API] isRunning: {0}", isRunning);
            Console.WriteLine("[Status API] listener != null: {0}", listener != null);

            if (listener != null)
            {
                Console.WriteLine("[Status API] listener.IsListening: {0}", listener.IsListening);
            }

            int loopCount = 0;

            while (isRunning)
            {
                try
                {
                    loopCount++;
                    Console.WriteLine("[Status API] Loop iteration #{0}", loopCount);

                    // REMOVED problematic IsListening check for Mono compatibility
                    // The IsListening property can cause issues on Mono/Linux
                    // if (!listener.IsListening)
                    // {
                    //     Console.WriteLine("[Status API] Listener is not listening, exiting...");
                    //     break;
                    // }

                    Console.WriteLine("[Status API] Waiting for request (blocking)...");

                    // Wait for incoming request (blocking call)
                    HttpListenerContext context = listener.GetContext();

                    Console.WriteLine("[Status API] Request received from {0}",
                        context.Request.RemoteEndPoint?.Address?.ToString() ?? "unknown");

                    // Process request on thread pool to avoid blocking
                    ThreadPool.QueueUserWorkItem((_) => HandleRequest(context));
                }
                catch (HttpListenerException ex)
                {
                    Console.WriteLine("[Status API] HttpListenerException: {0}", ex.Message);
                    Console.WriteLine("[Status API] Error code: {0}", ex.ErrorCode);
                    // Listener was stopped, exit gracefully
                    break;
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("[Status API] InvalidOperationException: {0}", ex.Message);
                    Console.WriteLine("[Status API] This usually means Start() wasn't called or failed");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Status API] ERROR in listener loop: {0}", ex.Message);
                    Console.WriteLine("[Status API] Exception type: {0}", ex.GetType().Name);
                    Console.WriteLine("[Status API] Stack trace: {0}", ex.StackTrace);
                    Thread.Sleep(1000); // Prevent tight loop on errors
                }
            }

            Console.WriteLine("[Status API] === Listener thread STOPPED ===");
            Console.WriteLine("[Status API] Reason: isRunning={0}, loopCount={1}", isRunning, loopCount);
        }

        /// <summary>
        /// Handle individual HTTP request
        /// </summary>
        private static void HandleRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            try
            {
                // Log incoming request
                string clientIP = request.RemoteEndPoint?.Address?.ToString() ?? "unknown";
                Console.WriteLine("[Status API] === Handling Request ===");
                Console.WriteLine("[Status API] From: {0}", clientIP);
                Console.WriteLine("[Status API] Method: {0}", request.HttpMethod);
                Console.WriteLine("[Status API] Path: {0}", request.Url?.AbsolutePath);

                // Add CORS headers (allow all origins for public API)
                response.AddHeader("Access-Control-Allow-Origin", "*");
                response.AddHeader("Access-Control-Allow-Methods", "GET, OPTIONS");
                response.AddHeader("Access-Control-Allow-Headers", "Content-Type");

                // Handle OPTIONS preflight request (CORS)
                if (request.HttpMethod == "OPTIONS")
                {
                    Console.WriteLine("[Status API] OPTIONS request - sending 200");
                    response.StatusCode = 200;
                    response.Close();
                    return;
                }

                // Only accept GET requests
                if (request.HttpMethod != "GET")
                {
                    Console.WriteLine("[Status API] Invalid method, sending 405");
                    SendErrorResponse(response, 405, "Method Not Allowed");
                    return;
                }

                // Check endpoint
                string path = request.Url?.AbsolutePath?.ToLower() ?? "/";
                if (path != ENDPOINT.ToLower() && path != "/")
                {
                    Console.WriteLine("[Status API] Invalid path, sending 404");
                    SendErrorResponse(response, 404, "Not Found");
                    return;
                }

                // Generate and send status JSON
                Console.WriteLine("[Status API] Generating status JSON...");
                string jsonResponse = GenerateStatusJSON();
                Console.WriteLine("[Status API] JSON generated, sending response...");
                SendJSONResponse(response, jsonResponse);

                Console.WriteLine("[Status API] Response sent successfully to {0}", clientIP);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Status API] ERROR handling request: {0}", ex.Message);
                Console.WriteLine("[Status API] Stack trace: {0}", ex.StackTrace);
                try
                {
                    SendErrorResponse(response, 500, "Internal Server Error");
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("[Status API] ERROR sending error response: {0}", ex2.Message);
                }
            }
        }

        /// <summary>
        /// Generate JSON status response with real server data
        /// </summary>
        private static string GenerateStatusJSON()
        {
            try
            {
                // Get current player count
                int playersOnline = NetState.Instances.Count;

                // Calculate uptime
                TimeSpan uptime = DateTime.UtcNow - serverStartTime;

                // Get server name (update this to your shard name)
                string serverName = "Nimeria Shard";

                // Get server version (update as needed)
                string serverVersion = "1.0.0";

                // Get expansion (update based on your server config)
                string expansion = "HighSeas";

                // Build JSON manually (or use JSON serializer if available)
                StringBuilder json = new StringBuilder();
                json.Append("{");
                json.AppendFormat("\"serverName\":\"{0}\",", EscapeJSON(serverName));
                json.Append("\"isOnline\":true,");
                json.AppendFormat("\"playersOnline\":{0},", playersOnline);
                json.AppendFormat("\"maxPlayers\":{0},", MAX_PLAYERS);
                json.Append("\"uptime\":{");
                json.AppendFormat("\"days\":{0},", uptime.Days);
                json.AppendFormat("\"hours\":{0},", uptime.Hours);
                json.AppendFormat("\"minutes\":{0},", uptime.Minutes);
                json.AppendFormat("\"seconds\":{0},", uptime.Seconds);
                json.AppendFormat("\"totalSeconds\":{0}", (long)uptime.TotalSeconds);
                json.Append("},");
                json.AppendFormat("\"serverVersion\":\"{0}\",", EscapeJSON(serverVersion));
                json.AppendFormat("\"expansion\":\"{0}\",", EscapeJSON(expansion));
                json.AppendFormat("\"timestamp\":\"{0}\"", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                json.Append("}");

                return json.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Status API] ERROR generating JSON: {0}", ex.Message);
                // Return minimal valid JSON on error
                return "{\"serverName\":\"Error\",\"isOnline\":false,\"error\":\"Failed to generate status\"}";
            }
        }

        /// <summary>
        /// Escape special characters for JSON
        /// </summary>
        private static string EscapeJSON(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            return text
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r")
                .Replace("\t", "\\t");
        }

        /// <summary>
        /// Send JSON response
        /// </summary>
        private static void SendJSONResponse(HttpListenerResponse response, string json)
        {
            response.ContentType = "application/json; charset=utf-8";
            response.StatusCode = 200;

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
            response.ContentType = "application/json; charset=utf-8";

            string json = $"{{\"error\":\"{EscapeJSON(message)}\",\"statusCode\":{statusCode}}}";
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }
    }
}

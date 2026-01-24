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
    /// HTTP Status API Server
    /// Provides real-time server status via HTTP endpoint on port 8080
    /// Endpoint: GET http://YOUR_SERVER_IP:8080/status
    /// </summary>
    public class StatusAPI
    {
        private static HttpListener listener;
        private static Thread listenerThread;
        private static bool isRunning = false;
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
            serverStartTime = DateTime.UtcNow;

            try
            {
                Console.WriteLine("[Status API] Initializing HTTP listener on port {0}...", PORT);

                listener = new HttpListener();
                listener.Prefixes.Add($"http://*:{PORT}{ENDPOINT}/");
                listener.Prefixes.Add($"http://*:{PORT}/"); // Root endpoint

                listener.Start();
                isRunning = true;

                listenerThread = new Thread(new ThreadStart(ListenForRequests));
                listenerThread.IsBackground = true;
                listenerThread.Start();

                Console.WriteLine("[Status API] HTTP listener started successfully!");
                Console.WriteLine("[Status API] Listening on: http://localhost:{0}{1}", PORT, ENDPOINT);
                Console.WriteLine("[Status API] CORS enabled for all origins");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Status API] ERROR: Failed to start HTTP listener: {0}", ex.Message);
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
                    listener?.Stop();
                    listener?.Close();
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
            Console.WriteLine("[Status API] Listener thread started");

            while (isRunning)
            {
                try
                {
                    if (!listener.IsListening)
                        break;

                    // Wait for incoming request (blocking call)
                    HttpListenerContext context = listener.GetContext();

                    // Process request on thread pool to avoid blocking
                    ThreadPool.QueueUserWorkItem((_) => HandleRequest(context));
                }
                catch (HttpListenerException)
                {
                    // Listener was stopped, exit gracefully
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Status API] ERROR in listener loop: {0}", ex.Message);
                    Thread.Sleep(1000); // Prevent tight loop on errors
                }
            }

            Console.WriteLine("[Status API] Listener thread stopped");
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
                Console.WriteLine("[Status API] Request from {0}: {1} {2}",
                    clientIP, request.HttpMethod, request.Url?.AbsolutePath);

                // Add CORS headers (allow all origins for public API)
                response.AddHeader("Access-Control-Allow-Origin", "*");
                response.AddHeader("Access-Control-Allow-Methods", "GET, OPTIONS");
                response.AddHeader("Access-Control-Allow-Headers", "Content-Type");

                // Handle OPTIONS preflight request (CORS)
                if (request.HttpMethod == "OPTIONS")
                {
                    response.StatusCode = 200;
                    response.Close();
                    return;
                }

                // Only accept GET requests
                if (request.HttpMethod != "GET")
                {
                    SendErrorResponse(response, 405, "Method Not Allowed");
                    return;
                }

                // Check endpoint
                string path = request.Url?.AbsolutePath?.ToLower() ?? "/";
                if (path != ENDPOINT.ToLower() && path != "/")
                {
                    SendErrorResponse(response, 404, "Not Found");
                    return;
                }

                // Generate and send status JSON
                string jsonResponse = GenerateStatusJSON();
                SendJSONResponse(response, jsonResponse);

                Console.WriteLine("[Status API] Response sent successfully to {0}", clientIP);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Status API] ERROR handling request: {0}", ex.Message);
                try
                {
                    SendErrorResponse(response, 500, "Internal Server Error");
                }
                catch
                {
                    // Response already closed or other error
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

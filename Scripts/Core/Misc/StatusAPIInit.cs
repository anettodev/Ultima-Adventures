using System;
using Server;

namespace Server.Misc
{
    /// <summary>
    /// Initialization script for Status API
    /// This ensures the HTTP listener starts when the server boots
    /// </summary>
    public class StatusAPIInit
    {
        /// <summary>
        /// Called automatically when server starts
        /// Priority: Low (runs after core systems are ready)
        /// </summary>
        public static void Initialize()
        {
            // Start the Status API HTTP listener
            StatusAPI.Initialize();

            // Register shutdown handler
            EventSink.Shutdown += new ShutdownEventHandler(OnShutdown);

            Console.WriteLine("[Status API Init] Initialization complete");
        }

        /// <summary>
        /// Called when server shuts down
        /// </summary>
        private static void OnShutdown(ShutdownEventArgs e)
        {
            StatusAPI.Shutdown();
        }
    }
}

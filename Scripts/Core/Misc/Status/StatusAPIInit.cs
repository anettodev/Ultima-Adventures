using System;
using Server;

namespace Server.Misc
{
    public class StatusAPIInit
    {
        private static bool hasInitialized = false;

        public static void Initialize()
        {
            // Prevent double initialization
            if (hasInitialized)
            {
                Console.WriteLine("[Status API Init] Already initialized, skipping duplicate call");
                return;
            }

            hasInitialized = true;
            Console.WriteLine("[Status API Init] First initialization - starting API...");

            // Start the Status API HTTP listener
            StatusAPI.Initialize();

            // Register shutdown handler
            EventSink.Shutdown += new ShutdownEventHandler(OnShutdown);

            Console.WriteLine("[Status API Init] Initialization complete");
        }

        private static void OnShutdown(ShutdownEventArgs e)
        {
            StatusAPI.Shutdown();
        }
    }
}
using System;
using System.Diagnostics;
using Server;

namespace Server.Misc
{
	/// <summary>
	/// CPU usage monitor for StatusAPI
	/// Samples CPU usage in background and caches the percentage for quick access
	/// </summary>
	public static class StatusAPICpuMonitor
	{
		private static double cachedCpuPercent = 0.0;
		private static DateTime lastSampleTime;
		private static TimeSpan lastTotalProcessorTime;
		private static Timer samplingTimer;
		private static bool isInitialized = false;
		private static readonly object lockObject = new object();

		/// <summary>
		/// Sample interval in seconds
		/// </summary>
		private const int SAMPLE_INTERVAL_SECONDS = 10;

		/// <summary>
		/// Initialize the CPU monitor
		/// Call this once during server startup
		/// </summary>
		public static void Initialize()
		{
			lock (lockObject)
			{
				if (isInitialized)
					return;

				try
				{
					// Take initial sample
					lastSampleTime = DateTime.UtcNow;
					lastTotalProcessorTime = Core.Process.TotalProcessorTime;

					// Start background sampling timer
					samplingTimer = Timer.DelayCall(
						TimeSpan.FromSeconds(SAMPLE_INTERVAL_SECONDS),
						TimeSpan.FromSeconds(SAMPLE_INTERVAL_SECONDS),
						SampleCpuUsage
					);

					isInitialized = true;
					Console.WriteLine("[Status API] CPU monitor initialized");
				}
				catch (Exception ex)
				{
					Console.WriteLine("[Status API] Failed to initialize CPU monitor: {0}", ex.Message);
				}
			}
		}

		/// <summary>
		/// Shutdown the CPU monitor
		/// </summary>
		public static void Shutdown()
		{
			lock (lockObject)
			{
				if (samplingTimer != null)
				{
					samplingTimer.Stop();
					samplingTimer = null;
				}
				isInitialized = false;
			}
		}

		/// <summary>
		/// Sample CPU usage (called by timer)
		/// </summary>
		private static void SampleCpuUsage()
		{
			try
			{
				DateTime currentTime = DateTime.UtcNow;
				TimeSpan currentTotalProcessorTime = Core.Process.TotalProcessorTime;

				// Calculate time deltas
				double realTimeDelta = (currentTime - lastSampleTime).TotalMilliseconds;
				double cpuTimeDelta = (currentTotalProcessorTime - lastTotalProcessorTime).TotalMilliseconds;

				// Calculate CPU percentage
				// Formula: (CPU time used / real time elapsed) / number of processors * 100
				if (realTimeDelta > 0)
				{
					double cpuPercent = (cpuTimeDelta / realTimeDelta / Core.ProcessorCount) * 100.0;

					// Clamp between 0 and 100
					cpuPercent = Math.Max(0, Math.Min(100, cpuPercent));

					lock (lockObject)
					{
						cachedCpuPercent = Math.Round(cpuPercent, 2);
					}
				}

				// Update for next sample
				lastSampleTime = currentTime;
				lastTotalProcessorTime = currentTotalProcessorTime;
			}
			catch (Exception ex)
			{
				Console.WriteLine("[Status API] Error sampling CPU: {0}", ex.Message);
			}
		}

		/// <summary>
		/// Get the current CPU usage percentage (cached value)
		/// </summary>
		/// <returns>CPU usage percentage (0-100)</returns>
		public static double GetCpuUsagePercent()
		{
			lock (lockObject)
			{
				return cachedCpuPercent;
			}
		}
	}
}

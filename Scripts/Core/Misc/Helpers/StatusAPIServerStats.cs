using System;
using Server.Accounting;

namespace Server.Misc.Helpers
{
	/// <summary>
	/// Helper class for gathering server statistics for StatusAPI
	/// Provides efficient access to server metrics without expensive operations
	/// </summary>
	public static class StatusAPIServerStats
	{
		/// <summary>
		/// Gets the count of active (non-banned) accounts
		/// </summary>
		public static int GetActiveAccountCount()
		{
			int active = 0;
			foreach (Account acct in Accounts.GetAccounts())
			{
				if (!acct.Banned)
					++active;
			}
			return active;
		}

		/// <summary>
		/// Gets the count of banned accounts
		/// </summary>
		public static int GetBannedAccountCount()
		{
			int banned = 0;
			foreach (Account acct in Accounts.GetAccounts())
			{
				if (acct.Banned)
					++banned;
			}
			return banned;
		}

		/// <summary>
		/// Gets the count of firewalled IPs
		/// </summary>
		public static int GetFirewalledCount()
		{
			return Firewall.List.Count;
		}

		/// <summary>
		/// Gets the total number of mobiles in the world
		/// </summary>
		public static int GetMobileCount()
		{
			return World.Mobiles.Count;
		}

		/// <summary>
		/// Gets the total number of items in the world
		/// </summary>
		public static int GetItemCount()
		{
			return World.Items.Count;
		}

		/// <summary>
		/// Gets the current memory usage in megabytes
		/// </summary>
		public static double GetMemoryUsageMB()
		{
			long bytes = GC.GetTotalMemory(false);
			return Math.Round(bytes / 1024.0 / 1024.0, 2);
		}

		/// <summary>
		/// Gets the .NET framework version string
		/// </summary>
		public static string GetFrameworkVersion()
		{
			return Environment.Version.ToString();
		}

		/// <summary>
		/// Gets the operating system string
		/// </summary>
		public static string GetOperatingSystem()
		{
			return Environment.OSVersion.ToString();
		}

		/// <summary>
		/// Gets the current CPU usage percentage
		/// </summary>
		public static double GetCpuUsagePercent()
		{
			return StatusAPICpuMonitor.GetCpuUsagePercent();
		}
	}
}

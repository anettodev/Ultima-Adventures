using System;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Items.Helpers
{
	/// <summary>
	/// Centralized delay/cooldown management for throwable potions.
	/// Shared by: Frostbite, Conflagration, Confusion Blast, and other throwable potions.
	/// Extracted from individual potion classes to eliminate code duplication (DRY principle).
	/// </summary>
	public static class ThrowablePotionDelayHelper
	{
		#region Static Data

		/// <summary>
		/// Tracks the last time each mobile threw a throwable potion for cooldown enforcement
		/// Thread-safe dictionary for per-mobile cooldown tracking
		/// </summary>
		private static Dictionary<Mobile, DateTime> m_LastThrowTime = new Dictionary<Mobile, DateTime>();

		/// <summary>
		/// Tracks the last time each mobile activated a throwable potion (1s anti-macro cooldown)
		/// </summary>
		private static Dictionary<Mobile, DateTime> m_LastActivationTime = new Dictionary<Mobile, DateTime>();

		#endregion

		#region Cooldown Management

		/// <summary>
		/// Checks if a mobile is currently on cooldown for throwing potions
		/// </summary>
		/// <param name="m">The mobile to check</param>
		/// <param name="baseCooldownSeconds">Base cooldown in seconds</param>
		/// <param name="applyChemistBonus">Whether to apply Chemist profession bonus</param>
		/// <param name="remainingSeconds">Output: seconds remaining on cooldown</param>
		/// <returns>True if on cooldown, false if can throw</returns>
		public static bool IsOnCooldown( Mobile m, double baseCooldownSeconds, bool applyChemistBonus, out int remainingSeconds )
		{
			DateTime lastThrow;

			lock ( m_LastThrowTime )
			{
				if ( m_LastThrowTime.TryGetValue( m, out lastThrow ) )
				{
					double scalar = applyChemistBonus ? CalculateChemistScalar( m ) : 1.0;
					double cooldown = baseCooldownSeconds / scalar;
					TimeSpan elapsed = DateTime.UtcNow - lastThrow;

					if ( elapsed.TotalSeconds < cooldown )
					{
						remainingSeconds = (int)Math.Ceiling( cooldown - elapsed.TotalSeconds );
						return true;
					}
				}
			}

			remainingSeconds = 0;
			return false;
		}

		/// <summary>
		/// Sets the cooldown timestamp for a mobile after throwing a potion
		/// </summary>
		/// <param name="m">The mobile that threw a potion</param>
		public static void SetCooldown( Mobile m )
		{
			lock ( m_LastThrowTime )
			{
				m_LastThrowTime[m] = DateTime.UtcNow;
			}
		}

		/// <summary>
		/// Checks if a mobile is on the 1-second activation cooldown (anti-macro)
		/// </summary>
		/// <param name="m">The mobile to check</param>
		/// <param name="remainingSeconds">Output: seconds remaining</param>
		/// <returns>True if on cooldown, false if can activate</returns>
		public static bool IsOnActivationCooldown( Mobile m, out double remainingSeconds )
		{
			DateTime lastActivation;

			lock ( m_LastActivationTime )
			{
				if ( m_LastActivationTime.TryGetValue( m, out lastActivation ) )
				{
					TimeSpan elapsed = DateTime.UtcNow - lastActivation;

					if ( elapsed.TotalSeconds < 1.0 )
					{
						remainingSeconds = 1.0 - elapsed.TotalSeconds;
						return true;
					}
				}
			}

			remainingSeconds = 0;
			return false;
		}

		/// <summary>
		/// Sets the activation cooldown timestamp
		/// </summary>
		/// <param name="m">The mobile that activated a potion</param>
		public static void SetActivationCooldown( Mobile m )
		{
			lock ( m_LastActivationTime )
			{
				m_LastActivationTime[m] = DateTime.UtcNow;
			}
		}

		/// <summary>
		/// Removes a mobile from cooldown tracking (cleanup)
		/// </summary>
		/// <param name="m">The mobile to remove</param>
		public static void RemoveCooldown( Mobile m )
		{
			lock ( m_LastThrowTime )
			{
				m_LastThrowTime.Remove( m );
			}

			lock ( m_LastActivationTime )
			{
				m_LastActivationTime.Remove( m );
			}
		}

		/// <summary>
		/// Cleans up old cooldown entries to prevent memory leaks
		/// Should be called periodically (e.g., every 5 minutes)
		/// </summary>
		public static void CleanupCooldowns()
		{
			lock ( m_LastThrowTime )
			{
				List<Mobile> toRemove = new List<Mobile>();

				foreach ( KeyValuePair<Mobile, DateTime> entry in m_LastThrowTime )
				{
					// Remove entries older than 2 minutes (way past any cooldown)
					if ( DateTime.UtcNow - entry.Value > TimeSpan.FromMinutes( 2 ) )
					{
						toRemove.Add( entry.Key );
					}
				}

				foreach ( Mobile m in toRemove )
				{
					m_LastThrowTime.Remove( m );
				}
			}

			lock ( m_LastActivationTime )
			{
				List<Mobile> toRemove = new List<Mobile>();

				foreach ( KeyValuePair<Mobile, DateTime> entry in m_LastActivationTime )
				{
					// Remove entries older than 10 seconds
					if ( DateTime.UtcNow - entry.Value > TimeSpan.FromSeconds( 10 ) )
					{
						toRemove.Add( entry.Key );
					}
				}

				foreach ( Mobile m in toRemove )
				{
					m_LastActivationTime.Remove( m );
				}
			}
		}

		#endregion

		#region Calculation Helpers

		/// <summary>
		/// Calculates the Chemist profession scalar for cooldown reduction
		/// Chemists get reduced cooldown based on their Enhance Potions value
		/// </summary>
		/// <param name="m">The mobile to calculate for</param>
		/// <returns>Scalar value (1.0 = no bonus, >1.0 = reduced cooldown)</returns>
		private static double CalculateChemistScalar( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)m;

				if ( pm.Alchemist() )
				{
					int enhancePotions = BasePotion.EnhancePotions( m );
					return 1.0 + (0.02 * enhancePotions);
				}
			}

			return 1.0;
		}

		#endregion
	}
}


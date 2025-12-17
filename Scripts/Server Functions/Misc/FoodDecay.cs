using System;
using Server.Network;
using Server;
using Server.Mobiles;

namespace Server.Misc
{
	/// <summary>
	/// Timer that handles hunger and thirst decay for players and controlled creatures.
	/// Decay occurs at random intervals between 30-60 minutes.
	/// </summary>
	public class FoodDecayTimer : Timer
	{
		#region Constructor and Initialization

		/// <summary>
		/// Initializes the food decay timer system
		/// </summary>
		public static void Initialize()
		{
			new FoodDecayTimer().Start();
		}

		/// <summary>
		/// Creates a new food decay timer with random interval between 30-60 minutes
		/// </summary>
		public FoodDecayTimer() : base( TimeSpan.FromMinutes( FoodDecayConstants.DECAY_INTERVAL_MIN_MINUTES ), TimeSpan.FromMinutes( FoodDecayConstants.DECAY_INTERVAL_MAX_MINUTES ) )
		{
			Priority = TimerPriority.OneMinute;
		}

		#endregion

		#region Timer Callbacks

		/// <summary>
		/// Called when the timer ticks, processes food decay for all connected players
		/// </summary>
		protected override void OnTick()
		{
			FoodDecay();
		}

		/// <summary>
		/// Processes hunger and thirst decay for all connected players
		/// </summary>
		public static void FoodDecay()
		{
			foreach ( NetState state in NetState.Instances )
			{
				HungerDecay( state.Mobile );
				ThirstDecay( state.Mobile );
			}
		}

		#endregion

		#region Hunger Decay Logic

		/// <summary>
		/// Processes hunger decay for a mobile
		/// </summary>
		/// <param name="m">The mobile to process hunger decay for</param>
		public static void HungerDecay( Mobile m )
		{
			if ( m == null )
				return;

			if ( IsValidPlayerForDecay( m ) )
			{
				ProcessPlayerHungerDecay( m );
			}
			else if ( m is BaseCreature )
			{
				ApplyCreatureHungerDecay( m );
			}
		}

		/// <summary>
		/// Processes hunger decay for a player mobile
		/// </summary>
		/// <param name="m">The player mobile to process</param>
		private static void ProcessPlayerHungerDecay( Mobile m )
		{
			// Camping skill protection: If skill >= 50, hunger decay is prevented
			if ( !HasCampingProtection( m ) )
			{
				if ( m.Hunger >= 1 )
				{
					ApplyHungerDecay( m );
					ClampHunger( m );
					SendHungerWarning( m );
				}
				else
				{
					ApplyStarvationEffects( m );
				}
			}
		}

		/// <summary>
		/// Applies hunger decay based on movement state and THC
		/// </summary>
		/// <param name="m">The mobile to apply decay to</param>
		private static void ApplyHungerDecay( Mobile m )
		{
			int decayAmount = CalculateHungerDecay( m );
			m.Hunger -= decayAmount;

			// Apply THC (Toxic Hunger Consumption) bonus decay if applicable
			if ( m is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)m;
				int thcDecay = CalculateTHCHungerDecay( pm );
				if ( thcDecay > 0 )
				{
					m.Hunger -= thcDecay;
				}
			}
		}

		/// <summary>
		/// Calculates hunger decay amount based on movement state
		/// </summary>
		/// <param name="m">The mobile to calculate decay for</param>
		/// <returns>The amount of hunger to decay</returns>
		private static int CalculateHungerDecay( Mobile m )
		{
			if ( IsRunning( m ) )
			{
				return Utility.RandomMinMax( FoodDecayConstants.HUNGER_DECAY_RUNNING_MIN, FoodDecayConstants.HUNGER_DECAY_RUNNING_MAX );
			}
			else
			{
				return FoodDecayConstants.HUNGER_DECAY_WALKING;
			}
		}

		/// <summary>
		/// Calculates additional hunger decay from THC (Toxic Hunger Consumption)
		/// </summary>
		/// <param name="pm">The player mobile to calculate THC decay for</param>
		/// <returns>The amount of additional hunger decay, or 0 if not applicable</returns>
		private static int CalculateTHCHungerDecay( PlayerMobile pm )
		{
			if ( pm.THC > 0 && ( Utility.RandomDouble() < ( pm.THC / (double)FoodDecayConstants.THC_DIVISOR ) ) )
			{
				return Utility.RandomMinMax( FoodDecayConstants.THC_HUNGER_DECAY_MIN, FoodDecayConstants.THC_HUNGER_DECAY_MAX );
			}
			return 0;
		}

		/// <summary>
		/// Clamps hunger value to ensure it doesn't go below 0
		/// </summary>
		/// <param name="m">The mobile to clamp hunger for</param>
		private static void ClampHunger( Mobile m )
		{
			if ( m.Hunger < 0 )
				m.Hunger = 0;
		}

		/// <summary>
		/// Sends warning messages to the player if hunger is low
		/// </summary>
		/// <param name="m">The mobile to send warnings to</param>
		private static void SendHungerWarning( Mobile m )
		{
			if ( m.Hunger < FoodDecayConstants.WARNING_THRESHOLD_LOW )
			{
				m.SendMessage( FoodDecayStringConstants.MSG_HUNGER_EXTREME );
				m.LocalOverheadMessage( MessageType.Emote, FoodDecayConstants.EMOTE_MESSAGE_COLOR, true, FoodDecayStringConstants.EMOTE_HUNGER_EXTREME );
			}
			else if ( m.Hunger < FoodDecayConstants.WARNING_THRESHOLD_MEDIUM )
			{
				m.SendMessage( FoodDecayStringConstants.MSG_HUNGER_VERY );
				m.LocalOverheadMessage( MessageType.Emote, FoodDecayConstants.EMOTE_MESSAGE_COLOR, true, FoodDecayStringConstants.EMOTE_HUNGER_VERY );
			}
		}

		/// <summary>
		/// Applies starvation effects when hunger reaches 0
		/// </summary>
		/// <param name="m">The mobile to apply effects to</param>
		private static void ApplyStarvationEffects( Mobile m )
		{
			if ( m.Hits > FoodDecayConstants.STARVATION_HITS_LOSS )
				m.Hits -= FoodDecayConstants.STARVATION_HITS_LOSS;
			if ( m.Stam > FoodDecayConstants.STARVATION_STAM_LOSS )
				m.Stam -= FoodDecayConstants.STARVATION_STAM_LOSS;
			if ( m.Mana > FoodDecayConstants.STARVATION_MANA_LOSS )
				m.Mana -= FoodDecayConstants.STARVATION_MANA_LOSS;

			m.SendMessage( FoodDecayStringConstants.MSG_HUNGER_STARVING );
			m.LocalOverheadMessage( MessageType.Emote, FoodDecayConstants.EMOTE_MESSAGE_COLOR, true, FoodDecayStringConstants.EMOTE_HUNGER_STARVING );
		}

		/// <summary>
		/// Applies hunger decay for controlled creatures
		/// </summary>
		/// <param name="m">The creature to apply decay to</param>
		private static void ApplyCreatureHungerDecay( Mobile m )
		{
			BaseCreature bc = m as BaseCreature;
			if ( bc != null && bc.Controlled && m.Hunger >= 1 )
			{
				m.Hunger -= FoodDecayConstants.CREATURE_DECAY_AMOUNT;
			}
		}

		#endregion

		#region Thirst Decay Logic

		/// <summary>
		/// Processes thirst decay for a mobile
		/// </summary>
		/// <param name="m">The mobile to process thirst decay for</param>
		public static void ThirstDecay( Mobile m )
		{
			if ( m == null )
				return;

			if ( IsValidPlayerForDecay( m ) )
			{
				ProcessPlayerThirstDecay( m );
			}
			else if ( m is BaseCreature )
			{
				ApplyCreatureThirstDecay( m );
			}
		}

		/// <summary>
		/// Processes thirst decay for a player mobile
		/// </summary>
		/// <param name="m">The player mobile to process</param>
		private static void ProcessPlayerThirstDecay( Mobile m )
		{
			// Camping skill protection: If skill < 50, thirst decay occurs (if >= 50, decay is prevented)
			if ( !HasCampingProtection( m ) )
			{
				if ( m.Thirst >= 1 )
				{
					ApplyThirstDecay( m );
					ClampThirst( m );
					SendThirstWarning( m );
				}
				else
				{
					ApplyDehydrationEffects( m );
				}
			}
		}

		/// <summary>
		/// Applies thirst decay based on movement state
		/// </summary>
		/// <param name="m">The mobile to apply decay to</param>
		private static void ApplyThirstDecay( Mobile m )
		{
			int decayAmount = CalculateThirstDecay( m );
			m.Thirst -= decayAmount;
		}

		/// <summary>
		/// Calculates thirst decay amount based on movement state
		/// </summary>
		/// <param name="m">The mobile to calculate decay for</param>
		/// <returns>The amount of thirst to decay</returns>
		private static int CalculateThirstDecay( Mobile m )
		{
			if ( IsRunning( m ) )
			{
				return Utility.RandomMinMax( FoodDecayConstants.THIRST_DECAY_RUNNING_MIN, FoodDecayConstants.THIRST_DECAY_RUNNING_MAX );
			}
			else if ( IsWalking( m ) )
			{
				return Utility.RandomMinMax( FoodDecayConstants.THIRST_DECAY_WALKING_MIN, FoodDecayConstants.THIRST_DECAY_WALKING_MAX );
			}
			else
			{
				return FoodDecayConstants.THIRST_DECAY_STANDING;
			}
		}

		/// <summary>
		/// Clamps thirst value to ensure it doesn't go below 0
		/// </summary>
		/// <param name="m">The mobile to clamp thirst for</param>
		private static void ClampThirst( Mobile m )
		{
			if ( m.Thirst < 0 )
				m.Thirst = 0;
		}

		/// <summary>
		/// Sends warning messages to the player if thirst is low
		/// </summary>
		/// <param name="m">The mobile to send warnings to</param>
		private static void SendThirstWarning( Mobile m )
		{
			if ( m.Thirst < FoodDecayConstants.WARNING_THRESHOLD_LOW )
			{
				m.SendMessage( FoodDecayStringConstants.MSG_THIRST_EXTREME );
				m.LocalOverheadMessage( MessageType.Emote, FoodDecayConstants.EMOTE_MESSAGE_COLOR, true, FoodDecayStringConstants.EMOTE_THIRST_EXTREME );
			}
			else if ( m.Thirst < FoodDecayConstants.WARNING_THRESHOLD_MEDIUM )
			{
				m.SendMessage( FoodDecayStringConstants.MSG_THIRST_GETTING );
				m.LocalOverheadMessage( MessageType.Emote, FoodDecayConstants.EMOTE_MESSAGE_COLOR, true, FoodDecayStringConstants.EMOTE_THIRST_GETTING );
			}
		}

		/// <summary>
		/// Applies dehydration effects when thirst reaches 0
		/// </summary>
		/// <param name="m">The mobile to apply effects to</param>
		private static void ApplyDehydrationEffects( Mobile m )
		{
			if ( m.Hits > FoodDecayConstants.DEHYDRATION_HITS_LOSS )
				m.Hits -= FoodDecayConstants.DEHYDRATION_HITS_LOSS;
			if ( m.Stam > FoodDecayConstants.DEHYDRATION_STAM_LOSS )
				m.Stam -= FoodDecayConstants.DEHYDRATION_STAM_LOSS;
			if ( m.Mana > FoodDecayConstants.DEHYDRATION_MANA_LOSS )
				m.Mana -= FoodDecayConstants.DEHYDRATION_MANA_LOSS;

			m.SendMessage( FoodDecayStringConstants.MSG_THIRST_EXHAUSTED );
			m.LocalOverheadMessage( MessageType.Emote, FoodDecayConstants.EMOTE_MESSAGE_COLOR, true, FoodDecayStringConstants.EMOTE_THIRST_EXHAUSTED );
		}

		/// <summary>
		/// Applies thirst decay for controlled creatures
		/// </summary>
		/// <param name="m">The creature to apply decay to</param>
		private static void ApplyCreatureThirstDecay( Mobile m )
		{
			BaseCreature bc = m as BaseCreature;
			if ( bc != null && bc.Controlled && m.Thirst >= 1 )
			{
				m.Thirst -= FoodDecayConstants.CREATURE_DECAY_AMOUNT;
			}
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Validates if a mobile is a valid player for decay processing
		/// </summary>
		/// <param name="m">The mobile to validate</param>
		/// <returns>True if the mobile is a valid player for decay, false otherwise</returns>
		private static bool IsValidPlayerForDecay( Mobile m )
		{
			return m is PlayerMobile 
				&& !Server.Commands.AFK.m_AFK.Contains( m.Serial.Value ) 
				&& m.AccessLevel == AccessLevel.Player 
				&& m.Alive;
		}

		/// <summary>
		/// Checks if the player has camping skill protection (skill >= 50)
		/// </summary>
		/// <param name="m">The mobile to check</param>
		/// <returns>True if the player has camping protection, false otherwise</returns>
		private static bool HasCampingProtection( Mobile m )
		{
			return m.Skills[SkillName.Camping].Value >= FoodDecayConstants.CAMPING_PROTECTION_THRESHOLD;
		}

		/// <summary>
		/// Checks if the mobile is currently running
		/// </summary>
		/// <param name="m">The mobile to check</param>
		/// <returns>True if the mobile is running, false otherwise</returns>
		private static bool IsRunning( Mobile m )
		{
			return ( m.Direction & Direction.Running ) != 0;
		}

		/// <summary>
		/// Checks if the mobile is currently walking (moving but not running)
		/// </summary>
		/// <param name="m">The mobile to check</param>
		/// <returns>True if the mobile is walking, false otherwise</returns>
		private static bool IsWalking( Mobile m )
		{
			return m.Direction != 0 && !IsRunning( m );
		}

		#endregion
	}
}

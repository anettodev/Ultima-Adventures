using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

using Server.Spells;
using Server.Misc;

namespace Server.SkillHandlers
{
	public class AnimalTaming
	{
		#region Fields

		private static Dictionary<BaseCreature, Mobile> m_BeingTamed = new Dictionary<BaseCreature, Mobile>();

		private static bool m_DisableMessage;

		#endregion

		#region Properties

		public static bool DisableMessage
		{
			get{ return m_DisableMessage; }
			set{ m_DisableMessage = value; }
		}

		#endregion

		#region Initialization

		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.AnimalTaming].Callback = new SkillUseCallback( OnUse );
		}

		#endregion

		#region Core Logic

		public static TimeSpan OnUse( Mobile m )
		{
			m.RevealingAction();

			m.Target = new InternalTarget(m);
			m.RevealingAction();

			if ( !m_DisableMessage )
				m.SendLocalizedMessage( 502789 ); // Tame which animal?

			return TimeSpan.FromHours( AnimalTamingConstants.COOLDOWN_HOURS );
		}

		public static bool CheckMastery( Mobile tamer, BaseCreature creature )
		{
			BaseCreature familiar = (BaseCreature)Spells.Necromancy.SummonFamiliarSpell.Table[tamer];

			if ( familiar != null && !familiar.Deleted && familiar is DarkWolfFamiliar )
			{
				if ( creature is DireWolf || creature is GreyWolf || creature is TimberWolf || creature is WhiteWolf || creature is MysticalFox )
					return true;
			}

			return false;
		}

		#endregion

		#region Validation Methods

		public static bool MustBeSubdued( BaseCreature bc )
		{
            if (bc.Owners.Count > 0) { return false; } //Checks to see if the animal has been tamed before
			return bc.SubdueBeforeTame && (bc.Hits > (bc.HitsMax / AnimalTamingConstants.SUBDUE_THRESHOLD_DIVISOR));
		}

		#endregion

		#region Scaling Methods

		public static void ScaleStats( BaseCreature bc, double scalar )
		{
			if ( bc.RawStr > 0 )
				bc.RawStr = (int)Math.Max( AnimalTamingConstants.MIN_STAT_VALUE, bc.RawStr * scalar );

			if ( bc.RawDex > 0 )
				bc.RawDex = (int)Math.Max( AnimalTamingConstants.MIN_STAT_VALUE, bc.RawDex * scalar );

			if ( bc.RawInt > 0 )
				bc.RawInt = (int)Math.Max( AnimalTamingConstants.MIN_STAT_VALUE, bc.RawInt * scalar );

			if ( bc.HitsMaxSeed > 0 )
			{
				bc.HitsMaxSeed = (int)Math.Max( AnimalTamingConstants.MIN_STAT_VALUE, bc.HitsMaxSeed * scalar );
				bc.Hits = bc.Hits;
				}

			if ( bc.StamMaxSeed > 0 )
			{
				bc.StamMaxSeed = (int)Math.Max( AnimalTamingConstants.MIN_STAT_VALUE, bc.StamMaxSeed * scalar );
				bc.Stam = bc.Stam;
			}
		}

		public static void ScaleSkills( BaseCreature bc, double scalar )
		{
			ScaleSkills( bc, scalar, scalar );
		}

		public static void ScaleSkills( BaseCreature bc, double scalar, double capScalar )
		{
			for ( int i = 0; i < bc.Skills.Length; ++i )
			{
				bc.Skills[i].Base *= scalar;

				bc.Skills[i].Cap = Math.Max( AnimalTamingConstants.MIN_SKILL_CAP, bc.Skills[i].Cap * capScalar );

				if ( bc.Skills[i].Base > bc.Skills[i].Cap )
				{
					bc.Skills[i].Cap = bc.Skills[i].Base;
				}
			}
		}

		#endregion

		#region Calculation Helpers

		/// <summary>
		/// Calculates the taming range based on AnimalTaming skill
		/// Base: 4 tiles, +1 per 10 points after 80, max 10 tiles
		/// </summary>
		private static int GetTamingRange(Mobile tamer)
		{
			double tamingSkill = tamer.Skills[SkillName.AnimalTaming].Value;
			
			if (tamingSkill < AnimalTamingConstants.RANGE_SKILL_THRESHOLD)
				return AnimalTamingConstants.BASE_TAMING_RANGE;
			
			// After threshold: +1 tile per skill points divisor
			int bonusTiles = (int)((tamingSkill - AnimalTamingConstants.RANGE_SKILL_THRESHOLD) / AnimalTamingConstants.RANGE_BONUS_DIVISOR);
			int range = AnimalTamingConstants.BASE_TAMING_RANGE + bonusTiles;
			
			// Cap at maximum range
			return Math.Min(range, AnimalTamingConstants.MAX_TAMING_RANGE);
		}

		/// <summary>
		/// Determines which tier a creature belongs to based on its MinTameSkill
		/// </summary>
		/// <param name="minTameSkill">The creature's minimum taming skill requirement</param>
		/// <returns>Tier number (1-7)</returns>
		private static int GetCreatureTier(double minTameSkill)
		{
			if (minTameSkill <= AnimalTamingConstants.TIER_1_MAX)
				return 1;
			else if (minTameSkill <= AnimalTamingConstants.TIER_2_MAX)
				return 2;
			else if (minTameSkill <= AnimalTamingConstants.TIER_3_MAX)
				return 3;
			else if (minTameSkill <= AnimalTamingConstants.TIER_4_MAX)
				return 4;
			else if (minTameSkill <= AnimalTamingConstants.TIER_5_MAX)
				return 5;
			else if (minTameSkill <= AnimalTamingConstants.TIER_6_MAX)
				return 6;
			else
				return 7;
		}

		/// <summary>
		/// Determines which tier a player's skill falls into
		/// </summary>
		/// <param name="playerSkill">The player's Animal Taming skill value</param>
		/// <returns>Tier number (1-7)</returns>
		private static int GetPlayerTier(double playerSkill)
		{
			if (playerSkill <= AnimalTamingConstants.TIER_1_MAX)
				return 1;
			else if (playerSkill <= AnimalTamingConstants.TIER_2_MAX)
				return 2;
			else if (playerSkill <= AnimalTamingConstants.TIER_3_MAX)
				return 3;
			else if (playerSkill <= AnimalTamingConstants.TIER_4_MAX)
				return 4;
			else if (playerSkill <= AnimalTamingConstants.TIER_5_MAX)
				return 5;
			else if (playerSkill <= AnimalTamingConstants.TIER_6_MAX)
				return 6;
			else
				return 7;
		}

		/// <summary>
		/// Calculates skill gain reduction factor based on tier difference (cumulative/multiplicative)
		/// Returns reduction factor (0.0 = no reduction, 1.0 = 100% reduction/no gain)
		/// Example: Tier 3 -> Tier 1 = 40% reduction, then 40% of remaining = 36% remaining (64% total reduction)
		/// </summary>
		/// <param name="playerSkill">The player's Animal Taming skill</param>
		/// <param name="creatureMinSkill">The creature's MinTameSkill</param>
		/// <returns>Reduction factor (0.0 to 1.0) and tier difference</returns>
		private static double GetTierReductionFactor(double playerSkill, double creatureMinSkill, out int tierDifference)
		{
			int playerTier = GetPlayerTier(playerSkill);
			int creatureTier = GetCreatureTier(creatureMinSkill);
			
			tierDifference = playerTier - creatureTier;
			
			// If creature tier >= player tier, no reduction
			if (tierDifference <= 0)
				return 0.0;
			
			// Calculate cumulative reduction: 40% reduction per tier (multiplicative)
			// Each tier below reduces by 40%, so remaining is 60% per tier
			// Tier 3 -> Tier 2: 60% remaining (40% reduction)
			// Tier 3 -> Tier 1: 60% * 60% = 36% remaining (64% reduction)
			double remainingFactor = 1.0 - AnimalTamingConstants.TIER_REDUCTION_PER_LEVEL; // 0.6 (60% remaining)
			double cumulativeRemaining = Math.Pow(remainingFactor, tierDifference);
			double reduction = 1.0 - cumulativeRemaining;
			
			// Cap at 100% (no gain if too many tiers below)
			return Math.Min(reduction, 1.0);
		}

		#endregion

		#region Nested Classes

		private class InternalTarget : Target
		{
			private bool m_SetSkillTime = true;
			private Mobile m_From;

			public InternalTarget(Mobile from) : base(GetTamingRange(from), false, TargetFlags.None)
			{
				m_From = from;
			}

			protected override void OnTargetFinish( Mobile from )
			{
				if ( m_SetSkillTime )
					from.NextSkillTime = Core.TickCount;
			}

			public virtual void ResetPacify( object obj )
			{
				if( obj is BaseCreature )
				{
					((BaseCreature)obj).BardPacified = true;
				}
			}

			/// <summary>
			/// Validates if the creature can be tamed
			/// </summary>
			/// <param name="from">The tamer</param>
			/// <param name="creature">The creature to tame</param>
			/// <returns>True if valid, false otherwise</returns>
			private bool ValidateCanTame(Mobile from, BaseCreature creature)
			{
				if ( !creature.Tamable )
				{
					creature.PrivateOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, 1049655, from.NetState ); // That creature cannot be tamed.
					return false;
				}

				if ( creature.Controlled )
				{
					creature.PrivateOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, 502804, from.NetState ); // That animal looks tame already.
					return false;
				}

				if ( from.Followers + creature.ControlSlots > from.FollowersMax )
				{
					from.SendLocalizedMessage( 1049611 ); // You have too many followers to tame that creature.
					return false;
				}

				if ( creature.Owners.Count >= BaseCreature.MaxOwners && !creature.Owners.Contains( from ) )
				{
					creature.PrivateOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, 1005615, from.NetState ); // This animal has had too many owners and is too upset for you to tame.
					return false;
				}

				if ( MustBeSubdued( creature ) )
				{
					creature.PrivateOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, 1054025, from.NetState ); // You must subdue this creature before you can tame it!
					return false;
				}

				return true;
			}

			/// <summary>
			/// Validates if the tamer has sufficient skill to attempt taming
			/// </summary>
			/// <param name="from">The tamer</param>
			/// <param name="creature">The creature to tame</param>
			/// <returns>True if has sufficient skill, false otherwise</returns>
			private bool ValidateHasSkill(Mobile from, BaseCreature creature)
			{
				return CheckMastery( from, creature ) || from.Skills[SkillName.AnimalTaming].Value >= creature.MinTameSkill;
			}

			/// <summary>
			/// Displays difficulty message based on skill difference
			/// </summary>
			/// <param name="from">The tamer</param>
			/// <param name="creature">The creature to tame</param>
			private void DisplayDifficultyMessage(Mobile from, BaseCreature creature)
			{
				double diff = creature.MinTameSkill - from.Skills[SkillName.AnimalTaming].Value;
				
				if (diff <= AnimalTamingConstants.DIFF_THRESHOLD_ALMOST)
					from.PublicOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, false, AnimalTamingStringConstants.MSG_DIFF_ALMOST );
				else if (diff <= AnimalTamingConstants.DIFF_THRESHOLD_CLOSE)
					from.PublicOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, false, AnimalTamingStringConstants.MSG_DIFF_CLOSE );
				else if (diff <= AnimalTamingConstants.DIFF_THRESHOLD_EFFORT)
					from.PublicOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, false, AnimalTamingStringConstants.MSG_DIFF_EFFORT );
				else if (diff <= AnimalTamingConstants.DIFF_THRESHOLD_LONG)
					from.PublicOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, false, AnimalTamingStringConstants.MSG_DIFF_LONG );
				else if (diff > AnimalTamingConstants.DIFF_THRESHOLD_DIFFICULT)
					from.PublicOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, false, AnimalTamingStringConstants.MSG_DIFF_DIFFICULT );
			}

			/// <summary>
			/// Handles creature anger when taming attempt fails
			/// </summary>
			/// <param name="from">The tamer</param>
			/// <param name="creature">The creature that became angry</param>
			private void HandleAnger(Mobile from, BaseCreature creature)
			{
				creature.PrivateOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, 502805, from.NetState ); // You seem to anger the beast!
				creature.PlaySound( creature.GetAngerSound() );
				creature.Direction = creature.GetDirectionTo( from );

				if( creature.BardPacified && Utility.RandomDouble() > AnimalTamingConstants.BARD_PACIFY_CHANCE)
				{
					Timer.DelayCall( TimeSpan.FromSeconds( AnimalTamingConstants.RESET_PACIFY_DELAY_SECONDS ), new TimerStateCallback( ResetPacify ), creature );
				}
				else
				{
					creature.BardEndTime = DateTime.UtcNow;
				}

				creature.BardPacified = false;
				creature.Move( creature.Direction );

				if ( from is PlayerMobile )
					creature.Combatant = from;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				from.RevealingAction();

				if (from.Blessed)
				{
					from.PublicOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, false, AnimalTamingStringConstants.MSG_BLESSED_STATE );
					return;
				}
				if ( targeted is Mobile )
				{
					if ( targeted is BaseCreature )
					{
						BaseCreature creature = (BaseCreature)targeted;

						// Validate creature can be tamed
						if ( !ValidateCanTame( from, creature ) )
							return;

						// Validate tamer has sufficient skill
						if ( !ValidateHasSkill( from, creature ) )
						{
							creature.PrivateOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, 502806, from.NetState ); // You have no chance of taming this creature.
							DisplayDifficultyMessage( from, creature );
							return;
						}

						// Check if already being tamed
						if ( m_BeingTamed.ContainsKey( creature ) )
						{
							creature.PrivateOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, 502802, from.NetState ); // Someone else is already taming this.
							return;
						}

						// Check for anger
						double angerodds = creature.MinTameSkill / (from.Skills[SkillName.AnimalTaming].Value * AnimalTamingConstants.ANGER_SKILL_MULTIPLIER);

						if ( creature.CanAngerOnTame && angerodds >= Utility.RandomDouble() && from.AccessLevel == AccessLevel.Player )
						{
							HandleAnger( from, creature );
							return;
						}

						// Start taming process
						m_BeingTamed[creature] = from;

						from.LocalOverheadMessage( MessageType.Emote, AnimalTamingConstants.MSG_COLOR_EMOTE, 1010597 ); // You start to tame the creature.
						from.NonlocalOverheadMessage( MessageType.Emote, AnimalTamingConstants.MSG_COLOR_EMOTE, 1010598 ); // *begins taming a creature.*

						new InternalTimer( from, creature, GetTamingAttemptCount(from) ).Start();

						m_SetSkillTime = false;
					}
					else
					{
						((Mobile)targeted).PrivateOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, 502469, from.NetState ); // That being cannot be tamed.
					}
				}
				else
				{
					from.SendLocalizedMessage( 502801 ); // You can't tame that!
				}
			}

			/// <summary>
			/// Calculates the taming timer interval
			/// Always 5 seconds
			/// </summary>
			private static TimeSpan GetTamingTimerInterval(Mobile tamer)
			{
				return TimeSpan.FromSeconds(AnimalTamingConstants.TIMER_INTERVAL_SECONDS);
			}

		/// <summary>
		/// Attempt range data structure for lookup table
		/// </summary>
		private struct AttemptRange
		{
			public double SkillThreshold;
			public int MinAttempts;
			public int MaxAttempts;
			public bool IsFixed;

			public AttemptRange(double threshold, int minAttempts, int maxAttempts, bool isFixed)
			{
				SkillThreshold = threshold;
				MinAttempts = minAttempts;
				MaxAttempts = maxAttempts;
				IsFixed = isFixed;
			}
		}

		/// <summary>
		/// Lookup table for taming attempt ranges based on skill level
		/// Ordered from highest to lowest skill threshold
		/// </summary>
		private static readonly AttemptRange[] s_AttemptRanges = new AttemptRange[]
		{
			new AttemptRange(AnimalTamingConstants.SKILL_THRESHOLD_120, AnimalTamingConstants.ATTEMPTS_120_PLUS, AnimalTamingConstants.ATTEMPTS_120_PLUS, true),
			new AttemptRange(AnimalTamingConstants.SKILL_THRESHOLD_110, AnimalTamingConstants.ATTEMPTS_MIN_110, AnimalTamingConstants.ATTEMPTS_MAX_110, false),
			new AttemptRange(AnimalTamingConstants.SKILL_THRESHOLD_100, AnimalTamingConstants.ATTEMPTS_MIN_100, AnimalTamingConstants.ATTEMPTS_MAX_100, false),
			new AttemptRange(AnimalTamingConstants.SKILL_THRESHOLD_90, AnimalTamingConstants.ATTEMPTS_MIN_90, AnimalTamingConstants.ATTEMPTS_MAX_90, false),
			new AttemptRange(AnimalTamingConstants.SKILL_THRESHOLD_80, AnimalTamingConstants.ATTEMPTS_MIN_80, AnimalTamingConstants.ATTEMPTS_MAX_80, false),
			new AttemptRange(0.0, AnimalTamingConstants.ATTEMPTS_MIN_BELOW_80, AnimalTamingConstants.ATTEMPTS_MAX_BELOW_80, false)
		};

		/// <summary>
		/// Calculates the number of taming attempts based on skill level
		/// Returns random value within skill-based range
		/// Minimum 1 attempt (5 seconds), maximum 6 attempts (30 seconds)
		/// </summary>
		private static int GetTamingAttemptCount(Mobile tamer)
		{
			double tamingSkill = tamer.Skills[SkillName.AnimalTaming].Value;

			// Look up the appropriate attempt range based on skill level
			for (int i = 0; i < s_AttemptRanges.Length; i++)
			{
				AttemptRange range = s_AttemptRanges[i];

				if (tamingSkill >= range.SkillThreshold)
				{
					if (range.IsFixed)
						return range.MinAttempts;
					else
						return Utility.RandomMinMax(range.MinAttempts, range.MaxAttempts);
				}
			}

			// Fallback to lowest range (should never reach here)
			return Utility.RandomMinMax(AnimalTamingConstants.ATTEMPTS_MIN_BELOW_80, AnimalTamingConstants.ATTEMPTS_MAX_BELOW_80);
		}

			private class InternalTimer : Timer
			{
				private Mobile m_Tamer;
				private BaseCreature m_Creature;
				private int m_MaxCount;
				private int m_Count;
				private bool m_Paralyzed;
				private DateTime m_StartTime;
				private int m_TamingRange;

				public InternalTimer( Mobile tamer, BaseCreature creature, int count ) : base( GetTamingTimerInterval(tamer), GetTamingTimerInterval(tamer), count )
				{
					m_Tamer = tamer;
					m_Creature = creature;
					m_MaxCount = count;
					m_Paralyzed = creature.Paralyzed;
					m_StartTime = DateTime.UtcNow;
					m_TamingRange = GetTamingRange(tamer);
					Priority = AnimalTamingConstants.TIMER_PRIORITY;
				}

				/// <summary>
				/// Stops the taming attempt and displays an error message
				/// </summary>
				/// <param name="messageId">Localized message ID to display</param>
				private void StopTamingAttempt(int messageId)
				{
					m_BeingTamed.Remove( m_Creature );
					m_Tamer.NextSkillTime = Core.TickCount;
					m_Creature.PrivateOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, messageId, m_Tamer.NetState );
					Stop();
				}

				/// <summary>
				/// Checks for skill gain in AnimalTaming and AnimalLore
				/// Applies tier-based reduction for lower tier creatures
				/// Shows success message when gaining skill from same tier or higher creatures
				/// </summary>
				/// <param name="minSkill">Minimum skill value for skill check</param>
				/// <param name="alreadyOwned">Whether the creature is already owned by tamer</param>
				private void CheckSkillGain(double minSkill, bool alreadyOwned)
				{
					if ( !alreadyOwned ) // Passively check animal lore for gain // Final added taming too!
					{
						double playerSkill = m_Tamer.Skills[SkillName.AnimalTaming].Value;
						
						// Calculate tier reduction factor
						int tierDifference;
						double reductionFactor = GetTierReductionFactor(playerSkill, minSkill, out tierDifference);
						
						// If 100% reduction, no skill gain
						if (reductionFactor >= 1.0)
						{
							return;
						}
						
						// Show feedback if taming lower tier creature
						if (reductionFactor > 0.0)
						{
							int reductionPercentage = (int)(reductionFactor * 100);
							m_Tamer.SendMessage(AnimalTamingConstants.MSG_COLOR_WARNING, string.Format(AnimalTamingStringConstants.MSG_TIER_REDUCED_GAIN_FORMAT, reductionPercentage));
						}
						
						// Calculate skill check range with reduction applied
						// Reduction makes the range narrower (more challenging), reducing gain chance
						double rangeReduction = reductionFactor;
						double effectiveRange = AnimalTamingConstants.SKILL_CHECK_RANGE * (1.0 - rangeReduction);
						
						double minCheck = minSkill - effectiveRange;
						double maxCheck = minSkill + effectiveRange;
						
						// Ensure minCheck doesn't go below 0
						if (minCheck < 0.0)
						{
							double adjustment = 0.0 - minCheck;
							minCheck = 0.0;
							maxCheck += adjustment;
						}
						
						// Track skill before check for success message (only for same tier or higher)
						double skillBefore = 0.0;
						bool shouldShowSuccessMessage = (tierDifference <= 0);
						
						if (shouldShowSuccessMessage)
						{
							skillBefore = m_Tamer.Skills[SkillName.AnimalTaming].Base;
						}
						
						switch ( Utility.Random( AnimalTamingConstants.SKILL_CHECK_RANDOM ) )
						{
							case 0: m_Tamer.CheckTargetSkill( SkillName.AnimalTaming, m_Creature, minCheck, maxCheck ); break;
							case 1: break;
						}
						m_Tamer.CheckTargetSkill( SkillName.AnimalLore, m_Creature, minCheck, maxCheck ); //+++
						
						// Show success message if skill increased (same tier or higher only)
						if (shouldShowSuccessMessage)
						{
							double skillAfter = m_Tamer.Skills[SkillName.AnimalTaming].Base;
							if (skillAfter > skillBefore)
							{
								// Calculate percentage gained (based on skill cap of 100.0)
								double skillGained = skillAfter - skillBefore;
								double skillCap = m_Tamer.Skills[SkillName.AnimalTaming].Cap;
								double percentageGained = (skillGained / skillCap) * 100.0;
								
								// Format to 1 decimal place
								string percentageText = percentageGained.ToString("F1");
								m_Tamer.SendMessage(AnimalTamingConstants.MSG_COLOR_SUCCESS, string.Format(AnimalTamingStringConstants.MSG_TIER_SKILL_GAIN_FORMAT, percentageText));
							}
						}
					}
				}

				/// <summary>
				/// Generates and displays generic taming speech (localized messages)
				/// </summary>
				private void GenerateGenericSpeech()
				{
					switch ( Utility.Random( AnimalTamingConstants.SPEECH_TYPE_COUNT ) )
					{
						case 0: m_Tamer.PublicOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, Utility.Random( 502790, 4 ) ); break;
						case 1: m_Tamer.PublicOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, Utility.Random( 1005608, 6 ) ); break;
						case 2: m_Tamer.PublicOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, Utility.Random( 1010593, 4 ) ); break;
					}
				}

				/// <summary>
				/// Generates evil karma speech based on random selection
				/// </summary>
				/// <returns>Evil karma speech string</returns>
				private string GenerateEvilSpeech()
				{
					switch ( Utility.Random( AnimalTamingConstants.SPEECH_COUNT ) )
					{
						case 0: return AnimalTamingStringConstants.SPEECH_EVIL_0;
						case 1: return AnimalTamingStringConstants.SPEECH_EVIL_1;
						case 2: return AnimalTamingStringConstants.SPEECH_EVIL_2;
						case 3: return AnimalTamingStringConstants.SPEECH_EVIL_3;
						case 4: return AnimalTamingStringConstants.SPEECH_EVIL_4;
						case 5: return AnimalTamingStringConstants.SPEECH_EVIL_5;
						case 6: return AnimalTamingStringConstants.SPEECH_EVIL_6;
						case 7: return AnimalTamingStringConstants.SPEECH_EVIL_7;
						case 8: return AnimalTamingStringConstants.SPEECH_EVIL_8;
						case 9: return AnimalTamingStringConstants.SPEECH_EVIL_9;
						case 10: return AnimalTamingStringConstants.SPEECH_EVIL_10;
						case 11: return string.Format(AnimalTamingStringConstants.SPEECH_EVIL_11_FORMAT, m_Tamer.Name);
						case 12: return string.Format(AnimalTamingStringConstants.SPEECH_EVIL_12_FORMAT, m_Creature.GetType().Name);
						default: return AnimalTamingStringConstants.SPEECH_EVIL_0;
					}
				}

				/// <summary>
				/// Generates good karma speech based on random selection
				/// </summary>
				/// <returns>Good karma speech string</returns>
				private string GenerateGoodSpeech()
				{
					switch ( Utility.Random( AnimalTamingConstants.SPEECH_COUNT ) )
					{
						case 0: return AnimalTamingStringConstants.SPEECH_GOOD_0;
						case 1: return AnimalTamingStringConstants.SPEECH_GOOD_1;
						case 2: return AnimalTamingStringConstants.SPEECH_GOOD_2;
						case 3: return AnimalTamingStringConstants.SPEECH_GOOD_3;
						case 4: return AnimalTamingStringConstants.SPEECH_GOOD_4;
						case 5: return AnimalTamingStringConstants.SPEECH_GOOD_5;
						case 6: return AnimalTamingStringConstants.SPEECH_GOOD_6;
						case 7: return AnimalTamingStringConstants.SPEECH_GOOD_7;
						case 8: return AnimalTamingStringConstants.SPEECH_GOOD_8;
						case 9: return AnimalTamingStringConstants.SPEECH_GOOD_9;
						case 10: return AnimalTamingStringConstants.SPEECH_GOOD_10;
						case 11: return AnimalTamingStringConstants.SPEECH_GOOD_11;
						case 12: return string.Format(AnimalTamingStringConstants.SPEECH_GOOD_12_FORMAT, m_Tamer.Name);
						default: return AnimalTamingStringConstants.SPEECH_GOOD_0;
					}
				}

				/// <summary>
				/// Garbles speech when tamer is drunk
				/// </summary>
				/// <param name="speech">Original speech to garble</param>
				/// <returns>Garbled speech string</returns>
				private string GarbleDrunkSpeech(string speech)
				{
					string[] said = speech.Split(' ');
					string garbled = "";

					for( int i = 0; i < said.Length; i++ )
					{
						if (Utility.RandomDouble() > AnimalTamingConstants.DRUNK_SPEECH_CHANCE)
						{
							switch (Utility.Random(AnimalTamingConstants.DRUNK_SPEECH_COUNT))
							{
								case 0: garbled += AnimalTamingStringConstants.DRUNK_CODE_0 + " "; break;
								case 1: garbled += AnimalTamingStringConstants.DRUNK_CODE_1 + " "; break;
								case 2: garbled += AnimalTamingStringConstants.DRUNK_CODE_2 + " "; break;
								case 3: garbled += AnimalTamingStringConstants.DRUNK_CODE_3 + " "; break;
								case 4: garbled += AnimalTamingStringConstants.DRUNK_CODE_4 + " "; break;
								case 5: garbled += AnimalTamingStringConstants.DRUNK_CODE_5 + " "; break;
								case 6: garbled += AnimalTamingStringConstants.DRUNK_CODE_6 + " "; break;
							}
						}
						else
							garbled += said[Utility.Random(said.Length)] + " ";
					}

					return garbled;
				}

				/// <summary>
				/// Generates and displays karma-based taming speech
				/// </summary>
				private void GenerateKarmaSpeech()
				{
					string speech = "";

					if (m_Tamer.Karma < AnimalTamingConstants.KARMA_THRESHOLD)
						speech = GenerateEvilSpeech();
					else
						speech = GenerateGoodSpeech();

					// Apply drunk garbling if applicable
					if (m_Tamer is PlayerMobile && ((PlayerMobile)m_Tamer).BAC > 0 && Utility.RandomDouble() < ((double)((PlayerMobile)m_Tamer).BAC / AnimalTamingConstants.BAC_DIVISOR))
						speech = GarbleDrunkSpeech(speech);

					m_Tamer.PublicOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, false, speech );
				}

				protected override void OnTick()
				{
					m_Count++;

					DamageEntry de = m_Creature.FindMostRecentDamageEntry( false );
					bool alreadyOwned = m_Creature.Owners.Contains( m_Tamer );

					// Use dynamic range based on tamer's skill
					if ( !m_Tamer.InRange( m_Creature, m_TamingRange ) )
					{
						StopTamingAttempt( 502795 ); // You are too far away to continue taming.
					}
					else if ( !m_Tamer.CheckAlive() )
					{
						StopTamingAttempt( 502796 ); // You are dead, and cannot continue taming.
					}
					else if ( !m_Tamer.CanSee( m_Creature ) || !m_Tamer.InLOS( m_Creature ) || !CanPath() )
					{
						StopTamingAttempt( 1049654 ); // You do not have a clear path to the animal you are taming, and must cease your attempt.
					}
					else if ( !m_Creature.Tamable )
					{
						StopTamingAttempt( 1049655 ); // That creature cannot be tamed.
					}
					else if ( m_Creature.Controlled )
					{
						StopTamingAttempt( 502804 ); // That animal looks tame already.
					}
					else if ( m_Creature.Owners.Count >= BaseCreature.MaxOwners && !m_Creature.Owners.Contains( m_Tamer ) )
					{
						StopTamingAttempt( 1005615 ); // This animal has had too many owners and is too upset for you to tame.
					}
					else if ( MustBeSubdued( m_Creature ) )
					{
						StopTamingAttempt( 1054025 ); // You must subdue this creature before you can tame it!
					}
					else if ( de != null && de.LastDamage > m_StartTime )
					{
						StopTamingAttempt( 502794 ); // The animal is too angry to continue taming.
					}
					else if ( m_Count < m_MaxCount )
					{
						m_Tamer.RevealingAction();
		
						if (Utility.RandomDouble() < AnimalTamingConstants.SPEECH_CHANCE)	
						{
							GenerateGenericSpeech();
						}
						else 
						{
							GenerateKarmaSpeech();
						}

						CheckSkillGain( m_Creature.MinTameSkill, alreadyOwned );

						if ( m_Creature.Paralyzed )
							m_Paralyzed = true;
					}
					else
					{
						m_Tamer.RevealingAction();
						m_Tamer.NextSkillTime = Core.TickCount;
						m_BeingTamed.Remove( m_Creature );

						double minSkill = m_Creature.MinTameSkill + (m_Creature.Owners.Count * AnimalTamingConstants.OWNER_PENALTY_PER_OWNER);

						if ( minSkill > AnimalTamingConstants.MASTERY_SKILL_THRESHOLD && CheckMastery( m_Tamer, m_Creature ) )
							minSkill = AnimalTamingConstants.MASTERY_SKILL_OVERRIDE;

						if ( m_Creature.Paralyzed )
							m_Paralyzed = true;

						CheckSkillGain( minSkill, alreadyOwned );


						if ( CheckMastery( m_Tamer, m_Creature ) || alreadyOwned || m_Tamer.CheckTargetSkill( SkillName.AnimalTaming, m_Creature, minSkill - AnimalTamingConstants.SKILL_CHECK_RANGE, minSkill + AnimalTamingConstants.SKILL_CHECK_RANGE ) )
						{
							if ( m_Creature.Owners.Count == AnimalTamingConstants.FIRST_OWNER_COUNT ) // First tame
							{
								if ( m_Paralyzed )
									ScaleSkills( m_Creature, AnimalTamingConstants.PARALYZED_SKILL_SCALAR );
								else
									ScaleSkills( m_Creature, AnimalTamingConstants.NORMAL_SKILL_SCALAR );

								if ( m_Creature.StatLossAfterTame )
									ScaleStats( m_Creature, AnimalTamingConstants.STAT_LOSS_SCALAR );
							}

							if ( alreadyOwned )
							{
								m_Tamer.SendLocalizedMessage( 502797 ); // That wasn't even challenging.
							}
							else
							{
								m_Creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 502799, m_Tamer.NetState ); // It seems to accept you as master.
								m_Creature.Owners.Add( m_Tamer );
							}

							if (m_Creature.Title != null)
							{
								if ( m_Creature.Title.Contains("*Enraged*") || m_Creature.Title.Contains("*Righteous*") )
								{
									m_Creature.RawStr /= AnimalTamingConstants.ENRAGED_STAT_DIVISOR;
									m_Creature.RawDex /= AnimalTamingConstants.ENRAGED_STAT_DIVISOR;
									m_Creature.RawInt /= AnimalTamingConstants.ENRAGED_STAT_DIVISOR;
									m_Creature.Hue = AnimalTamingConstants.ENRAGED_HUE;
									m_Creature.HitsMaxSeed /= AnimalTamingConstants.ENRAGED_STAT_DIVISOR;
									m_Creature.Hits = m_Creature.HitsMaxSeed;
									m_Creature.AIFullSpeedActive = false;
								}
							}

							if (m_Creature.Map != null)
							{
								int Heat = MyServerSettings.GetDifficultyLevel( m_Creature.Location, m_Creature.Map );
								if (Heat > 0 )
									Server.Mobiles.BaseCreature.BeefDown(m_Creature, Heat); //final beefdown to adjust for beefup in dungeons
							}
							
							m_Creature.SetControlMaster( m_Tamer );

							m_Creature.RangeHome = AnimalTamingConstants.DEFAULT_RANGE_HOME;
							m_Creature.Home = AnimalTamingConstants.DEFAULT_HOME;

							m_Creature.IsBonded = false;

						}
						else
						{
							m_Creature.PrivateOverheadMessage( MessageType.Regular, AnimalTamingConstants.MSG_COLOR_ERROR, 502798, m_Tamer.NetState ); // You fail to tame the creature.
						}
					}
				}

				private bool CanPath()
				{
					IPoint3D p = m_Tamer as IPoint3D;

					if ( p == null )
						return false;

					if( m_Creature.InRange( new Point3D( p ), 1 ) )
						return true;

					MovementPath path = new MovementPath( m_Creature, new Point3D( p ) );
					return path.Success;
				}
			}
		}
		#endregion
	}
}

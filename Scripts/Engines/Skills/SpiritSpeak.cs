using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Network;
using Server.Mobiles;

namespace Server.SkillHandlers
{
	class SpiritSpeak
	{
		#region Static Data

		/// <summary>Tracks active particle effect timers for each mobile</summary>
		private static System.Collections.Generic.Dictionary<Mobile, GhostHearingParticleTimer> m_ParticleTimers = new System.Collections.Generic.Dictionary<Mobile, GhostHearingParticleTimer>();

		#endregion

		#region Initialization

		public static void Initialize()
		{
			SkillInfo.Table[32].Callback = new SkillUseCallback( OnUse );
		}

		#endregion

		#region Skill Handler

		public static TimeSpan OnUse( Mobile m )
		{
			if ( Core.AOS )
			{
				Spell spell = new SpiritSpeakSpell( m );

				spell.Cast();

				if ( spell.IsCasting )
					return TimeSpan.FromSeconds( SpiritSpeakConstants.CAST_DELAY_SECONDS );

				return TimeSpan.Zero;
			}

			m.RevealingAction();

			double skillValue = m.Skills[SkillName.SpiritSpeak].Value;

			// Check minimum skill requirement
			if ( skillValue < SpiritSpeakConstants.MIN_SKILL_TO_HEAR_GHOSTS )
			{
				m.SendLocalizedMessage( 502443 ); // You fail to contact the netherworld.
				m.CanHearGhosts = false;

				// Stop and remove particle timer if it exists
				if ( m_ParticleTimers.ContainsKey( m ) )
				{
					GhostHearingParticleTimer particleTimer = m_ParticleTimers[m];
					if ( particleTimer != null )
					{
						particleTimer.Stop();
					}
					m_ParticleTimers.Remove( m );
				}

				// Still check for skill gain even if below minimum requirement
				m.CheckSkill( SkillName.SpiritSpeak, SpiritSpeakConstants.SKILL_GAIN_MIN, SpiritSpeakConstants.SKILL_GAIN_MAX );

				return TimeSpan.FromSeconds( SpiritSpeakConstants.SKILL_COOLDOWN_SECONDS );
			}

			// Check for skill gain (regardless of success/failure)
			m.CheckSkill( SkillName.SpiritSpeak, SpiritSpeakConstants.SKILL_GAIN_MIN, SpiritSpeakConstants.SKILL_GAIN_MAX );

			// Calculate success chance: 30% at 50.0, +10% per 10 skill points
			double successChance = SpiritSpeakConstants.BASE_SUCCESS_CHANCE;
			if ( skillValue >= 120.0 )
			{
				successChance = 1.0; // 100% at 120.0+
			}
			else if ( skillValue >= 110.0 )
			{
				successChance = 0.90; // 90% at 110.0-119.9
			}
			else
			{
				double skillBonus = skillValue - SpiritSpeakConstants.MIN_SKILL_TO_HEAR_GHOSTS;
				successChance += (skillBonus / 10.0) * SpiritSpeakConstants.SUCCESS_CHANCE_PER_10_SKILL;
				if ( successChance > 1.0 )
					successChance = 1.0;
			}

			// Check success
			bool success = Utility.RandomDouble() <= successChance;

			if ( success )
			{
				if ( !m.CanHearGhosts )
				{
					// Calculate duration: 10s at 50.0, 60s at 100.0+ (linear)
					double durationSeconds = SpiritSpeakConstants.MIN_DURATION_SECONDS;
					if ( skillValue >= 100.0 )
					{
						durationSeconds = SpiritSpeakConstants.MAX_DURATION_SECONDS;
					}
					else
					{
						double skillRange = skillValue - SpiritSpeakConstants.MIN_SKILL_TO_HEAR_GHOSTS;
						double durationRange = SpiritSpeakConstants.MAX_DURATION_SECONDS - SpiritSpeakConstants.MIN_DURATION_SECONDS;
						double skillRangeMax = 100.0 - SpiritSpeakConstants.MIN_SKILL_TO_HEAR_GHOSTS;
						durationSeconds = SpiritSpeakConstants.MIN_DURATION_SECONDS + (skillRange / skillRangeMax) * durationRange;
					}

					SpiritSpeakTimer timer = new SpiritSpeakTimer( m, TimeSpan.FromSeconds( durationSeconds ) );
					timer.Start();
					m.CanHearGhosts = true;

					// Start particle effect timer
					GhostHearingParticleTimer particleTimer = new GhostHearingParticleTimer( m );
					particleTimer.Start();
					m_ParticleTimers[m] = particleTimer;

					// Send success message with duration
					m.SendMessage( string.Format( SpiritSpeakStringConstants.MSG_CAN_HEAR_GHOSTS, (int)durationSeconds ) );
				}

				m.PlaySound( SpiritSpeakConstants.SPIRIT_SPEAK_SOUND );
				m.SendLocalizedMessage( 502444 ); // You contact the netherworld.
			}
			else
			{
				m.SendLocalizedMessage( 502443 ); // You fail to contact the netherworld.
				m.CanHearGhosts = false;

				// Stop and remove particle timer if it exists
				if ( m_ParticleTimers.ContainsKey( m ) )
				{
					GhostHearingParticleTimer particleTimer = m_ParticleTimers[m];
					if ( particleTimer != null )
					{
						particleTimer.Stop();
					}
					m_ParticleTimers.Remove( m );
				}
			}

			return TimeSpan.FromSeconds( SpiritSpeakConstants.SKILL_COOLDOWN_SECONDS );
		}

		#endregion

		#region Timer

		private class SpiritSpeakTimer : Timer
		{
			private Mobile m_Owner;

			public SpiritSpeakTimer( Mobile m, TimeSpan duration ) : base( duration )
			{
				m_Owner = m;
				Priority = TimerPriority.FiveSeconds;
			}

			protected override void OnTick()
			{
				if ( m_Owner != null && !m_Owner.Deleted )
				{
					m_Owner.CanHearGhosts = false;
					m_Owner.SendLocalizedMessage( 502445 ); // You feel your contact with the netherworld fading.
				}

				// Stop and remove particle timer if it exists
				if ( m_ParticleTimers.ContainsKey( m_Owner ) )
				{
					GhostHearingParticleTimer particleTimer = m_ParticleTimers[m_Owner];
					if ( particleTimer != null )
					{
						particleTimer.Stop();
					}
					m_ParticleTimers.Remove( m_Owner );
				}
			}
		}

		/// <summary>
		/// Timer that plays light blue particle effects every 3 seconds while CanHearGhosts is active
		/// </summary>
		private class GhostHearingParticleTimer : Timer
		{
			private Mobile m_Owner;

			public GhostHearingParticleTimer( Mobile m ) : base( TimeSpan.FromSeconds( SpiritSpeakConstants.PARTICLE_EFFECT_INTERVAL_SECONDS ), TimeSpan.FromSeconds( SpiritSpeakConstants.PARTICLE_EFFECT_INTERVAL_SECONDS ) )
			{
				m_Owner = m;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				if ( m_Owner == null || m_Owner.Deleted || !m_Owner.CanHearGhosts )
				{
					Stop();
					// Remove from dictionary when stopped
					if ( m_ParticleTimers.ContainsKey( m_Owner ) )
					{
						m_ParticleTimers.Remove( m_Owner );
					}
					return;
				}

				// Play light blue particle effect (same as NightSight sense mode)
				m_Owner.FixedParticles( SpiritSpeakConstants.PARTICLE_EFFECT_ID, 1, 15, 9964, SpiritSpeakConstants.PARTICLE_EFFECT_HUE, 0, EffectLayer.Waist );
			}
		}

		#endregion

		#region Spell

		public class SpiritSpeakSpell : Spell
		{
			private static SpellInfo m_Info = new SpellInfo( SpiritSpeakStringConstants.SPELL_NAME, "", 269 );

			public override bool BlockedByHorrificBeast{ get{ return false; } }

			public SpiritSpeakSpell( Mobile caster ) : base( caster, null, m_Info )
			{
			}

			#region Spell Properties

			public override bool ClearHandsOnCast{ get{ return false; } }
			public override double CastDelayFastScalar { get { return 0; } }
			public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( SpiritSpeakConstants.CAST_DELAY_BASE_SECONDS ); } }
			public override bool CheckNextSpellTime{ get{ return false; } }

			public override int GetMana()
			{
				return 0; // Mana cost disabled
			}

			public override bool ConsumeReagents()
			{
				return true;
			}

			public override bool CheckFizzle()
			{
				return true;
			}

			#endregion

			#region Spell Events

			public override void OnCasterHurt()
			{
				if ( IsCasting )
					Disturb( DisturbType.Hurt, false, true );
			}

			public override void OnDisturb( DisturbType type, bool message )
			{
				Caster.NextSkillTime = Core.TickCount;

				base.OnDisturb( type, message );
			}

			public override bool CheckDisturb( DisturbType type, bool checkFirst, bool resistable )
			{
				if ( type == DisturbType.EquipRequest || type == DisturbType.UseRequest )
					return false;

				return true;
			}

			#endregion

			#region Core Logic

			public override void OnCast()
			{
				Corpse toChannel = null;
				CorpseItem toDestroy = null;

				FindNearbyCorpse( out toChannel, out toDestroy );

				// No mana cost validation needed (disabled)
				// if ( !ValidateCasterState( mana ) )
				// {
				// 	FinishSequence();
				// 	return;
				// }

				Caster.CheckSkill( SkillName.SpiritSpeak, SpiritSpeakConstants.SKILL_GAIN_MIN, SpiritSpeakConstants.SKILL_GAIN_MAX );

				double skillValue = Caster.Skills[SkillName.SpiritSpeak].Value;

				// Check if we can activate CanHearGhosts (requires 50.0+ skill)
				bool canActivateGhostHearing = skillValue >= SpiritSpeakConstants.MIN_SKILL_TO_HEAR_GHOSTS;

				// Calculate success chance for CanHearGhosts: 30% at 50.0, +10% per 10 skill points
				bool ghostHearingSuccess = false;
				if ( canActivateGhostHearing )
				{
					double successChance = SpiritSpeakConstants.BASE_SUCCESS_CHANCE;
					if ( skillValue >= 120.0 )
					{
						successChance = 1.0; // 100% at 120.0+
					}
					else if ( skillValue >= 110.0 )
					{
						successChance = 0.90; // 90% at 110.0-119.9
					}
					else
					{
						double skillBonus = skillValue - SpiritSpeakConstants.MIN_SKILL_TO_HEAR_GHOSTS;
						successChance += (skillBonus / 10.0) * SpiritSpeakConstants.SUCCESS_CHANCE_PER_10_SKILL;
						if ( successChance > 1.0 )
							successChance = 1.0;
					}

					ghostHearingSuccess = Utility.RandomDouble() <= successChance;
				}

				// Check success for corpse/bone interaction (visual only, no healing/balance)
				bool corpseInteractionSuccess = CheckSuccess();

				// Process CanHearGhosts activation if successful
				if ( ghostHearingSuccess && !Caster.CanHearGhosts )
				{
					// Calculate duration: 10s at 50.0, 60s at 100.0+ (linear)
					double durationSeconds = SpiritSpeakConstants.MIN_DURATION_SECONDS;
					if ( skillValue >= 100.0 )
					{
						durationSeconds = SpiritSpeakConstants.MAX_DURATION_SECONDS;
					}
					else
					{
						double skillRange = skillValue - SpiritSpeakConstants.MIN_SKILL_TO_HEAR_GHOSTS;
						double durationRange = SpiritSpeakConstants.MAX_DURATION_SECONDS - SpiritSpeakConstants.MIN_DURATION_SECONDS;
						double skillRangeMax = 100.0 - SpiritSpeakConstants.MIN_SKILL_TO_HEAR_GHOSTS;
						durationSeconds = SpiritSpeakConstants.MIN_DURATION_SECONDS + (skillRange / skillRangeMax) * durationRange;
					}

					SpiritSpeakTimer timer = new SpiritSpeakTimer( Caster, TimeSpan.FromSeconds( durationSeconds ) );
					timer.Start();
					Caster.CanHearGhosts = true;

					// Start particle effect timer
					GhostHearingParticleTimer particleTimer = new GhostHearingParticleTimer( Caster );
					particleTimer.Start();
					m_ParticleTimers[Caster] = particleTimer;

					// Send success message with duration
					Caster.SendMessage( string.Format( SpiritSpeakStringConstants.MSG_CAN_HEAR_GHOSTS, (int)durationSeconds ) );
				}

				// Process corpse/bone interaction (visual only, no healing/balance/mana cost)
				if ( corpseInteractionSuccess )
				{
					ProcessCorpseTargetVisualOnly( toChannel, toDestroy );
					ApplyVisualEffects();
				}
				else
				{
					Caster.SendLocalizedMessage( 502443 ); // You fail your attempt at contacting the netherworld.
				}

				FinishSequence();
			}

			#endregion

			#region Helper Methods

			private void FindNearbyCorpse( out Corpse toChannel, out CorpseItem toDestroy )
			{
				toChannel = null;
				toDestroy = null;

				foreach ( Item item in Caster.GetItemsInRange( SpiritSpeakConstants.CORPSE_SEARCH_RANGE ) )
				{
					Corpse corpse = item as Corpse;
					if ( corpse != null && !corpse.Channeled && !corpse.Animated && Caster.Karma < 0 )
					{
						toChannel = corpse;
						return;
					}

					CorpseItem bones = item as CorpseItem;
					if ( bones != null )
					{
						toDestroy = bones;
						return;
					}
				}
			}

			private void GetHealingParameters( Corpse toChannel, CorpseItem toDestroy, out int min, out int max, out int mana, out string message )
			{
				min = CalculateMinHealing();
				max = CalculateMaxHealing( min );

				if ( toChannel != null )
				{
					mana = 0;
					message = SpiritSpeakStringConstants.MSG_OFFER_TO_EVIL;
				}
				else if ( toDestroy != null )
				{
					mana = 0;
					message = SpiritSpeakStringConstants.MSG_BONES_COMBUST;
				}
				else
				{
					mana = SpiritSpeakConstants.BASE_MANA_COST;
					message = SpiritSpeakStringConstants.MSG_CHANNEL_ENERGY;
				}
			}

			private int CalculateMinHealing()
			{
				int baseValue = 1
					+ (int)(Caster.Skills[SkillName.SpiritSpeak].Value * SpiritSpeakConstants.SPIRIT_SPEAK_SKILL_MULTIPLIER)
					+ (int)(Caster.Skills[SkillName.Wrestling].Value * SpiritSpeakConstants.WRESTLING_SKILL_MULTIPLIER);

				return Server.Misc.MyServerSettings.PlayerLevelMod( baseValue, Caster );
			}

			private int CalculateMaxHealing( int min )
			{
				int bonus = Server.Misc.MyServerSettings.PlayerLevelMod( SpiritSpeakConstants.DAMAGE_BONUS_ADDITION, Caster );
				return Server.Misc.MyServerSettings.PlayerLevelMod( min + bonus, Caster );
			}

			private bool ValidateCasterState( int manaCost )
			{
				if ( Caster.Mana < manaCost )
				{
					Caster.SendLocalizedMessage( 1061285 ); // You lack the mana required to use this skill.
					return false;
				}

				if ( Caster.Poisoned )
				{
					Caster.SendMessage( SpiritSpeakStringConstants.MSG_POISONED );
					return false;
				}

				if ( Caster.Hunger < 1 )
				{
					Caster.SendMessage( SpiritSpeakStringConstants.MSG_STARVING );
					return false;
				}

				if ( Caster.Thirst < 1 )
				{
					Caster.SendMessage( SpiritSpeakStringConstants.MSG_THIRSTY );
					return false;
				}

				return true;
			}

			private bool CheckSuccess()
			{
				return Utility.RandomDouble() <= (Caster.Skills[SkillName.SpiritSpeak].Value / SpiritSpeakConstants.SUCCESS_CHANCE_DIVISOR);
			}

			private void ProcessCorpseTarget( Corpse toChannel, CorpseItem toDestroy )
			{
				if ( toChannel != null )
				{
					toChannel.Channeled = true;
					toChannel.Hue = SpiritSpeakConstants.CHANNELED_CORPSE_HUE;
					ApplyBalanceEffect(
						SpiritSpeakConstants.SOULBOUND_BALANCE_CORPSE,
						SpiritSpeakConstants.NON_SOULBOUND_BALANCE_CORPSE );
				}
				else if ( toDestroy != null )
				{
					toDestroy.Delete();
					ApplyBalanceEffectRandom(
						SpiritSpeakConstants.SOULBOUND_BALANCE_BONES_MIN,
						SpiritSpeakConstants.SOULBOUND_BALANCE_BONES_MAX,
						SpiritSpeakConstants.NON_SOULBOUND_BALANCE_BONES_MIN,
						SpiritSpeakConstants.NON_SOULBOUND_BALANCE_BONES_MAX );
				}
			}

			/// <summary>
			/// Processes corpse/bone interaction for visual effects only (no balance/healing/mana cost)
			/// </summary>
			private void ProcessCorpseTargetVisualOnly( Corpse toChannel, CorpseItem toDestroy )
			{
				if ( toChannel != null )
				{
					toChannel.Channeled = true;
					toChannel.Hue = SpiritSpeakConstants.CHANNELED_CORPSE_HUE;
					// Balance system disabled - no ApplyBalanceEffect call
				}
				else if ( toDestroy != null )
				{
					toDestroy.Delete();
					// Balance system disabled - no ApplyBalanceEffectRandom call
				}
			}

			private void ApplyBalanceEffect( int soulboundAmount, int normalAmount )
			{
				PlayerMobile pm = Caster as PlayerMobile;
				if ( pm == null )
					return;

				int amount = pm.SoulBound ? soulboundAmount : normalAmount;
				AetherGlobe.BalanceLevel += amount;
				pm.BalanceEffect += amount;
			}

			private void ApplyBalanceEffectRandom( int soulboundMin, int soulboundMax, int normalMin, int normalMax )
			{
				PlayerMobile pm = Caster as PlayerMobile;
				if ( pm == null )
					return;

				int amount;
				if ( pm.SoulBound )
					amount = Utility.RandomMinMax( soulboundMin, soulboundMax );
				else
					amount = Utility.RandomMinMax( normalMin, normalMax );

				AetherGlobe.BalanceLevel += amount;
				pm.BalanceEffect += amount;
			}

			private void ApplyHealing( int min, int max, int manaCost, string message )
			{
				Caster.Mana -= manaCost;
				Caster.SendMessage( message );

				if ( min > max )
					min = max;

				Caster.Hits += Utility.RandomMinMax( min, max );
				Caster.Stam += Utility.RandomMinMax( min, max );

				if ( Utility.RandomDouble() < SpiritSpeakConstants.MANA_RESTORE_CHANCE )
					Caster.Mana += Utility.RandomMinMax( SpiritSpeakConstants.MANA_RESTORE_MIN, SpiritSpeakConstants.MANA_RESTORE_MAX );
			}

			private void ApplyVisualEffects()
			{
				if ( Caster.Karma < 0 )
				{
					int min = CalculateMinHealing();
					Misc.Titles.AwardKarma( Caster, -min, true );
					Caster.FixedParticles( 0x3400, 1, 15, 9501, 2100, 4, EffectLayer.Waist );
				}
				else
				{
					Caster.FixedParticles( 0x375A, 1, 15, 9501, 2100, 4, EffectLayer.Waist );
				}
			}

			#endregion
		}

		#endregion
	}
}

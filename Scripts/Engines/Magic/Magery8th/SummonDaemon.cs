using System;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Eighth
{
	/// <summary>
	/// Summon Daemon - 8th Circle Magery Spell
	/// Summons a Daemon creature to fight for the caster.
	/// Requires: Magery >= 100 AND Inscription >= 100 for Greater Daemon chance (20% base + EvalInt * 0.2%)
	/// Includes special particle effects on summon.
	/// LIMIT: Only 1 daemon per caster (new summon dismisses old one)
	/// </summary>
	public class SummonDaemonSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Summon Daemon", "Kal Vas Xen Corp",
				269,
				9050,
				false,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }

		#region Single Daemon Tracking

		/// <summary>
		/// Tracks the currently active daemon for each caster
		/// Only one daemon allowed per caster at a time
		/// </summary>
		private static Dictionary<Mobile, BaseCreature> m_ActiveDaemons = new Dictionary<Mobile, BaseCreature>();

		/// <summary>
		/// Registers a newly summoned daemon and dismisses any existing one
		/// </summary>
		private static void RegisterDaemon(Mobile caster, BaseCreature daemon)
		{
			// Dismiss old daemon if exists
			DismissExistingDaemon(caster);

			// Register new daemon
			m_ActiveDaemons[caster] = daemon;
		}

		/// <summary>
		/// Dismisses the caster's existing daemon if present
		/// Also cleans up if daemon expired naturally (was already deleted)
		/// </summary>
		private static void DismissExistingDaemon(Mobile caster)
		{
			BaseCreature existingDaemon;
			if (m_ActiveDaemons.TryGetValue(caster, out existingDaemon))
			{
				if (existingDaemon != null && !existingDaemon.Deleted)
				{
					existingDaemon.Delete();
					caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.INFO_PREVIOUS_SUMMON_DISMISSED);
				}
				// Always remove from dictionary (even if already deleted/expired)
				m_ActiveDaemons.Remove(caster);
			}
		}

		/// <summary>
		/// Unregisters a daemon when it's deleted/expires
		/// Called from cleanup or when daemon dies naturally
		/// </summary>
		public static void UnregisterDaemon(Mobile caster, BaseCreature daemon)
		{
			BaseCreature registeredDaemon;
			if (m_ActiveDaemons.TryGetValue(caster, out registeredDaemon))
			{
				if (registeredDaemon == daemon)
				{
					m_ActiveDaemons.Remove(caster);
				}
			}
		}

		#endregion

		#region Constants

		/// <summary>Multiplier for duration calculation based on Magery skill</summary>
		private const double DURATION_MULTIPLIER = 0.1;

		/// <summary>Skill threshold required for Greater Daemon summon chance (Magery and Inscription)</summary>
		private const double SKILL_THRESHOLD_FOR_GREATER = 100.0;

		/// <summary>Base chance percentage for Greater Daemon (when requirements are met)</summary>
		private const double GREATER_CHANCE_BASE = 20.0;

		/// <summary>EvalInt bonus multiplier per skill point (0.2% per point)</summary>
		private const double EVALINT_BONUS_PER_POINT = 0.2;

		/// <summary>Effect ID for summon visual</summary>
		private const int SUMMON_EFFECT_ID = 0x216;

		/// <summary>Particle effect ID for daemon summon</summary>
		private const int PARTICLE_EFFECT_ID = 0x3728;

		/// <summary>Number of particles for summon effect</summary>
		private const int PARTICLE_COUNT = 8;

		/// <summary>Particle speed for summon effect</summary>
		private const int PARTICLE_SPEED = 20;

		/// <summary>Particle duration for summon effect</summary>
		private const int PARTICLE_DURATION = 5042;

		/// <summary>Particle hue layer offset</summary>
		private const int PARTICLE_HUE_LAYER = 0;

		#endregion

		public SummonDaemonSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if (!base.CheckCast(caster))
				return false;

			if (Caster.Followers >= Caster.FollowersMax)
			{
				DoFizzle();
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TOO_MANY_FOLLOWERS);
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if (CheckSequence())
			{
				TimeSpan duration = CalculateSummonDuration();
				SendDurationMessage(duration);

				BaseCreature daemon;
				if (ShouldSummonGreaterDaemon())
				{
					daemon = new SummonedDaemonGreater();
				}
				else
				{
					daemon = new SummonedDaemon();
				}

				SpellHelper.Summon(daemon, Caster, SUMMON_EFFECT_ID, duration, false, false);
				RegisterDaemon(Caster, daemon);
				ApplySummonParticles(daemon);
			}

			FinishSequence();
		}

		#region Helper Methods

		/// <summary>
		/// Calculates the duration for the summoned creature
		/// Formula: (Magery.Fixed * DURATION_MULTIPLIER) * NMSUtils.getBonusIncriptBenefit(Caster)
		/// </summary>
		private TimeSpan CalculateSummonDuration()
		{
			return TimeSpan.FromSeconds((Caster.Skills[SkillName.Magery].Fixed * DURATION_MULTIPLIER) * NMSUtils.getBonusIncriptBenefit(Caster));
		}

		/// <summary>
		/// Determines if a Greater Daemon should be summoned
		/// Requires: Magery >= 100 AND Inscription >= 100
		/// Base chance: 20% + (EvalInt * 0.2%) per EvalInt point
		/// </summary>
		private bool ShouldSummonGreaterDaemon()
		{
			// Check requirements: Magery >= 100 AND Inscription >= 100
			if (Caster.Skills[SkillName.Magery].Value < SKILL_THRESHOLD_FOR_GREATER ||
			    Caster.Skills[SkillName.Inscribe].Value < SKILL_THRESHOLD_FOR_GREATER)
			{
				return false;
			}

			// Calculate chance: Base 20% + (EvalInt * 0.2%)
			double evalIntValue = Caster.Skills[SkillName.EvalInt].Value;
			double chance = GREATER_CHANCE_BASE + (evalIntValue * EVALINT_BONUS_PER_POINT);

			// Roll random 0-100 and check if it's below the chance percentage
			int roll = Utility.RandomMinMax(0, 100);
			return roll < chance;
		}

		/// <summary>
		/// Sends the duration message to the caster
		/// </summary>
		private void SendDurationMessage(TimeSpan duration)
		{
			Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, string.Format(Spell.SpellMessages.INFO_SUMMON_DURATION_FORMAT, duration.TotalSeconds));
		}

		/// <summary>
		/// Applies particle effects to the summoned daemon
		/// </summary>
		private void ApplySummonParticles(BaseCreature daemon)
		{
			daemon.FixedParticles(PARTICLE_EFFECT_ID, PARTICLE_COUNT, PARTICLE_SPEED, PARTICLE_DURATION, 
			                     Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0), PARTICLE_HUE_LAYER, EffectLayer.Head);
		}

		#endregion
	}
}

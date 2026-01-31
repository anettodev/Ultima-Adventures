using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Items;
using Server.Spells;

namespace Server.Spells.Fifth
{
	/// <summary>
	/// Magic Reflection - 5th Circle Defensive Spell
	/// Creates a temporary shield that reflects the FIRST hostile spell back to its caster
	/// Shield duration: 15-90 seconds (based on Magery + Inscription)
	/// Shield power: Magery + Inscription (for power comparison in dual-shield scenarios)
	/// Cooldown: 60 seconds after shield is consumed/expired
	/// </summary>
	public class MagicReflectSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Magic Reflection", "In Jux Sanct",
				242,
				9012,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }

		#region Constants

		// Duration constants
		/// <summary>Minimum shield duration (seconds)</summary>
		private const double MIN_DURATION = 15.0;

		/// <summary>Maximum shield duration (seconds)</summary>
		private const double MAX_DURATION = 90.0;

		/// <summary>Maximum total skills for duration calculation</summary>
		private const double MAX_SKILL_TOTAL = 240.0; // 120 Magery + 120 Inscription

		/// <summary>Duration range (MAX - MIN)</summary>
		private const double DURATION_RANGE = 75.0; // 90 - 15

		// Cooldown constants
		/// <summary>Cooldown after shield is dismissed (seconds)</summary>
		private const double COOLDOWN_SECONDS = 60.0;

		// Visual and audio effect constants
		// Shield activation
		private const int ACTIVATE_SOUND = 0x1ED;
		private const int ACTIVATE_PARTICLE = 0x375A;
		private const int ACTIVATE_PARTICLE_SPEED = 10;
		private const int ACTIVATE_PARTICLE_DURATION = 15;
		private const int ACTIVATE_PARTICLE_HUE = 5037;

		// Shield expiration (natural timeout)
		private const int EXPIRE_SOUND = 0x1EA;
		private const int EXPIRE_PARTICLE = 0x376A;
		private const int EXPIRE_PARTICLE_SPEED = 9;
		private const int EXPIRE_PARTICLE_DURATION = 32;
		private const int EXPIRE_PARTICLE_HUE = 5008;

		// Shield reflection (success)
		private const int REFLECT_SOUND_1 = 0x0FC;
		private const int REFLECT_SOUND_2 = 0x1F7;
		private const int REFLECT_EFFECT = 0x37B9;
		private const int REFLECT_PARTICLE = 0x374A;
		private const int REFLECT_PARTICLE_SPEED = 10;
		private const int REFLECT_PARTICLE_DURATION = 30;
		private const int REFLECT_PARTICLE_HUE = 5013;

		// Shield break (overpowered)
		private const int BREAK_SOUND = 0x1F1;
		private const int BREAK_PARTICLE = 0x3709;
		private const int BREAK_PARTICLE_SPEED = 10;
		private const int BREAK_PARTICLE_DURATION = 20;
		private const int BREAK_PARTICLE_HUE = 5009;

		// Arcane Interference (multi-target spell shield interaction)
		private const int INTERFERENCE_PARTICLE = 0x3728;
		private const int INTERFERENCE_PARTICLE_SPEED = 10;
		private const int INTERFERENCE_PARTICLE_DURATION = 20;
		private const int INTERFERENCE_PARTICLE_HUE = 5042;
		private const int INTERFERENCE_SOUND_ALERT = 0x1ED;
		private const int INTERFERENCE_SOUND_DISPEL = 0x653;
		private const double INTERFERENCE_SOUND_DELAY = 0.3;

		// Reflection delay (1-second delay before spell bounces back)
		private const double REFLECTION_DELAY = 1.0;

		#endregion

		#region Data Structures

		/// <summary>
		/// Stores active shield data for each mobile
		/// </summary>
		public class MagicReflectData
		{
			public int ShieldPower { get; set; }
			public Mobile Owner { get; set; }
			public DateTime CastTime { get; set; }
			public Timer DurationTimer { get; set; }
		}

		/// <summary>
		/// Stores reflection context for delayed reflection
		/// Used to apply reflected spell after 1-second delay
		/// </summary>
		public class ReflectionContext
		{
			public Mobile OriginalCaster { get; set; }
			public Mobile Reflector { get; set; }
			public int SpellCircle { get; set; }
			public bool WasReflected { get; set; }
		}

	// Global tracking dictionaries
	private static Dictionary<Mobile, MagicReflectData> m_ActiveShields = new Dictionary<Mobile, MagicReflectData>();
	private static Dictionary<Mobile, DateTime> m_LastShieldBreak = new Dictionary<Mobile, DateTime>();
	private static Dictionary<Mobile, ReflectionContext> m_ReflectionContexts = new Dictionary<Mobile, ReflectionContext>();
	private static object m_ReflectionLock = new object();

	#endregion

		public MagicReflectSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		/// <summary>
		/// Validates if caster can cast Magic Reflection
		/// Checks: existing shield, cooldown
		/// </summary>
		public override bool CheckCast(Mobile caster)
		{
			// Check if already has active shield
			if (m_ActiveShields.ContainsKey(Caster))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_MAGIC_REFLECT_ALREADY_ACTIVE);
				return false;
			}

			// Check cooldown (60 seconds after last shield dismissal)
			DateTime lastBreak;
			if (m_LastShieldBreak.TryGetValue(Caster, out lastBreak))
			{
				TimeSpan elapsed = DateTime.UtcNow - lastBreak;
				if (elapsed.TotalSeconds < COOLDOWN_SECONDS)
				{
					double remaining = COOLDOWN_SECONDS - elapsed.TotalSeconds;
					Caster.SendMessage(Spell.MSG_COLOR_ERROR,
						String.Format(Spell.SpellMessages.MAGIC_REFLECT_COOLDOWN_FORMAT, remaining));
					return false;
				}
			}

			return true;
		}

		public override void OnCast()
		{
			if (m_ActiveShields.ContainsKey(Caster))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_MAGIC_REFLECT_ALREADY_ACTIVE);
			}
			else if (CheckSequence())
			{
				CreateShield(Caster);
			}

			FinishSequence();
		}

		#region Shield Management

		/// <summary>
		/// Creates a new reflection shield for the caster
		/// Calculates power and duration based on skills
		/// </summary>
		private static void CreateShield(Mobile caster)
		{
			int power = CalculateShieldPower(caster);
			TimeSpan duration = CalculateShieldDuration(caster);

			MagicReflectData data = new MagicReflectData
			{
				ShieldPower = power,
				Owner = caster,
				CastTime = DateTime.UtcNow,
				DurationTimer = new ShieldDurationTimer(caster, duration)
			};

			m_ActiveShields[caster] = data;
			data.DurationTimer.Start();

			// Visual and audio feedback
			PlayActivationEffects(caster);

			// Inform caster
			caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.MAGIC_REFLECT_ACTIVATED);
			caster.SendMessage(Spell.MSG_COLOR_SYSTEM,
				String.Format(Spell.SpellMessages.MAGIC_REFLECT_DURATION_FORMAT, duration.TotalSeconds));
		}

		/// <summary>
		/// Removes an active shield from a mobile
		/// Starts cooldown timer
		/// </summary>
		/// <param name="owner">Mobile to remove shield from</param>
		/// <param name="playEffects">Whether to play expiration effects</param>
		public static void RemoveShield(Mobile owner, bool playEffects)
		{
			MagicReflectData data;
			if (!m_ActiveShields.TryGetValue(owner, out data))
				return;

			// Stop duration timer
			if (data.DurationTimer != null)
			{
				data.DurationTimer.Stop();
				data.DurationTimer = null;
			}

			// Remove from tracking
			m_ActiveShields.Remove(owner);

			// Play effects if requested
			if (playEffects)
			{
				PlayExpirationEffects(owner);
			}

			// Start cooldown
			m_LastShieldBreak[owner] = DateTime.UtcNow;
		}

		/// <summary>
		/// Attempts to consume a shield (first-come-first-served for multiple simultaneous spells)
		/// Thread-safe operation
		/// </summary>
		/// <param name="target">Mobile whose shield to consume</param>
		/// <returns>Shield data if consumed, null if no shield or already consumed</returns>
		public static MagicReflectData TryConsumeShield(Mobile target)
		{
			lock (m_ReflectionLock)
			{
				MagicReflectData data;
				if (m_ActiveShields.TryGetValue(target, out data))
				{
					// Shield exists and not yet consumed
					return data;
				}

				return null; // No shield or already consumed
			}
		}

		/// <summary>
		/// Checks if a mobile has an active reflection shield
		/// </summary>
		public static bool HasActiveShield(Mobile mobile)
		{
			return m_ActiveShields.ContainsKey(mobile);
		}

		/// <summary>
		/// Gets shield power for a mobile
		/// </summary>
		public static int GetShieldPower(Mobile mobile)
		{
			MagicReflectData data;
			if (m_ActiveShields.TryGetValue(mobile, out data))
				return data.ShieldPower;

			return 0;
		}

		#endregion

		#region Calculations

		/// <summary>
		/// Calculates shield power based on Magery and Inscription
		/// Power is used for shield vs shield comparisons
		/// Formula: Magery + Inscription
		/// </summary>
		private static int CalculateShieldPower(Mobile caster)
		{
			double magery = caster.Skills.Magery.Value;
			double inscribe = caster.Skills.Inscribe.Value;

			return (int)(magery + inscribe);
		}

		/// <summary>
		/// Calculates shield duration with linear progression
		/// Min: 15s (0 total skills) → Max: 90s (240 total skills)
		/// Formula: 15 + (totalSkills / 240) * 75
		/// </summary>
		private static TimeSpan CalculateShieldDuration(Mobile caster)
		{
			double magery = caster.Skills.Magery.Value;
			double inscribe = caster.Skills.Inscribe.Value;
			double totalSkills = magery + inscribe;

			// Linear progression: 15s to 90s
			double seconds = MIN_DURATION + ((totalSkills / MAX_SKILL_TOTAL) * DURATION_RANGE);

			// Hard cap at maximum
			if (seconds > MAX_DURATION)
				seconds = MAX_DURATION;

			return TimeSpan.FromSeconds(seconds);
		}

		#endregion

		#region Visual and Audio Effects

		/// <summary>
		/// Plays visual and audio effects when shield is activated
		/// </summary>
		private static void PlayActivationEffects(Mobile target)
		{
			target.PlaySound(ACTIVATE_SOUND);
			target.FixedParticles(
				ACTIVATE_PARTICLE,
				ACTIVATE_PARTICLE_SPEED,
				ACTIVATE_PARTICLE_DURATION,
				ACTIVATE_PARTICLE_HUE,
				Server.Items.CharacterDatabase.GetMySpellHue(target, 0),
				0,
				EffectLayer.Waist
			);
		}

		/// <summary>
		/// Plays visual and audio effects when shield expires naturally
		/// </summary>
		private static void PlayExpirationEffects(Mobile target)
		{
			target.PlaySound(EXPIRE_SOUND);
			target.FixedParticles(
				EXPIRE_PARTICLE,
				EXPIRE_PARTICLE_SPEED,
				EXPIRE_PARTICLE_DURATION,
				EXPIRE_PARTICLE_HUE,
				Server.Items.CharacterDatabase.GetMySpellHue(target, 0),
				0,
				EffectLayer.Waist
			);

			target.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.MAGIC_REFLECT_EXPIRED);
		}

		/// <summary>
		/// Plays visual and audio effects when spell is reflected
		/// </summary>
		public static void PlayReflectionEffects(Mobile caster, Mobile target)
		{
			// Target effects (reflector)
			target.FixedEffect(REFLECT_EFFECT, 10, 5);
			target.FixedParticles(
				REFLECT_PARTICLE,
				REFLECT_PARTICLE_SPEED,
				REFLECT_PARTICLE_DURATION,
				REFLECT_PARTICLE_HUE,
				Server.Items.CharacterDatabase.GetMySpellHue(target, 0),
				2,
				EffectLayer.Waist
			);
			target.PlaySound(REFLECT_SOUND_1);
			target.PlaySound(REFLECT_SOUND_2);

			target.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.MAGIC_REFLECT_SUCCESS_TARGET);

			// Caster notification (spell reflected back)
			caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.MAGIC_REFLECT_SUCCESS_CASTER);
		}

		/// <summary>
		/// Plays visual and audio effects when shield is broken/overpowered
		/// </summary>
		public static void PlayBreakEffects(Mobile target)
		{
			target.PlaySound(BREAK_SOUND);
			target.FixedParticles(
				BREAK_PARTICLE,
				BREAK_PARTICLE_SPEED,
				BREAK_PARTICLE_DURATION,
				BREAK_PARTICLE_HUE,
				Server.Items.CharacterDatabase.GetMySpellHue(target, 0),
				0,
				EffectLayer.Waist
			);

		target.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.MAGIC_REFLECT_OVERPOWERED_TARGET);
	}

		/// <summary>
		/// Dispels shield due to arcane interference (multi-target spell interaction)
		/// All shields involved are destroyed, no damage is dealt, no reflection occurs
		/// </summary>
		/// <param name="mobile">The mobile whose shield is being dispelled</param>
		/// <param name="multipleShields">True if multiple shields were involved in the interference</param>
		public static void DispelArcaneInterference(Mobile mobile, bool multipleShields)
		{
			if (!HasActiveShield(mobile))
				return;

			// Remove shield (no cooldown since it's arcane interference, not consumption)
			RemoveShield(mobile, false);

			// Arcane interference visual effects (chaotic dispel)
			mobile.FixedParticles(
				INTERFERENCE_PARTICLE,
				INTERFERENCE_PARTICLE_SPEED,
				INTERFERENCE_PARTICLE_DURATION,
				INTERFERENCE_PARTICLE_HUE,
				Server.Items.CharacterDatabase.GetMySpellHue(mobile, 0),
				0,
				EffectLayer.Waist
			);

			// Two-stage audio feedback (alert + dispel)
			mobile.PlaySound(INTERFERENCE_SOUND_ALERT);
			Timer.DelayCall(
				TimeSpan.FromSeconds(INTERFERENCE_SOUND_DELAY),
				delegate { mobile.PlaySound(INTERFERENCE_SOUND_DISPEL); }
			);

			// Send appropriate message based on context
			if (multipleShields)
			{
				// Multiple shields involved (caster + targets)
				mobile.SendMessage(Spell.MSG_COLOR_SYSTEM,
					Spell.SpellMessages.MAGIC_REFLECT_ARCANE_INTERFERENCE_CASTER);
			}
			else
			{
				// Only this mobile's shield was affected
				mobile.SendMessage(Spell.MSG_COLOR_WARNING,
					Spell.SpellMessages.MAGIC_REFLECT_ARCANE_INTERFERENCE_TARGET);
			}
		}

	#endregion

	#region Timers

		/// <summary>
		/// Timer that expires the shield after its duration
		/// </summary>
		private class ShieldDurationTimer : Timer
		{
			private Mobile m_Owner;

		public ShieldDurationTimer(Mobile owner, TimeSpan duration) : base(duration)
		{
			m_Owner = owner;
			Priority = TimerPriority.OneSecond;
		}

			protected override void OnTick()
			{
				// Shield expired naturally - play effects
				RemoveShield(m_Owner, true);
			}
		}

		#endregion

		#region Utility Methods

		/// <summary>
		/// Validates if a mobile is a valid target for reflected spell damage
		/// Checks: alive, not deleted, valid map, not logged out
		/// Used to prevent reflected damage to dead/invalid targets
		/// </summary>
		/// <param name="target">Mobile to validate</param>
		/// <param name="reflector">Mobile who reflected the spell (for notifications)</param>
		/// <returns>True if target is valid for damage</returns>
		public static bool IsValidReflectionTarget(Mobile target, Mobile reflector)
		{
			// Dead or deleted
			if (target == null || target.Deleted || !target.Alive)
			{
				if (reflector != null && reflector.Alive && !reflector.Deleted)
				{
					reflector.SendMessage(Spell.MSG_COLOR_SYSTEM,
						Spell.SpellMessages.MAGIC_REFLECT_TARGET_DEAD);
				}
				return false;
			}

			// Invalid map or logged out
			if (target.Map == null || target.Map == Map.Internal)
			{
				if (reflector != null && reflector.Alive && !reflector.Deleted)
				{
					reflector.SendMessage(Spell.MSG_COLOR_SYSTEM,
						Spell.SpellMessages.MAGIC_REFLECT_TARGET_UNAVAILABLE);
				}
				return false;
			}

		// Note: We explicitly do NOT check range, LOS, or safe zones
		// Reflected spells always hit their target regardless of position

		return true;
	}

	/// <summary>
	/// Creates a reflection context for delayed spell bounce-back
	/// Stores the original caster and starts 1-second timer
	/// </summary>
	public static void CreateDelayedReflection(Mobile originalCaster, Mobile reflector, int spellCircle)
	{
		// Store reflection context
		ReflectionContext context = new ReflectionContext
		{
			OriginalCaster = originalCaster,
			Reflector = reflector,
			SpellCircle = spellCircle,
			WasReflected = true
		};

		m_ReflectionContexts[reflector] = context;

		// Start 1-second delayed reflection timer
		Timer.DelayCall(TimeSpan.FromSeconds(REFLECTION_DELAY), 
			delegate { OnDelayedReflection(context); });
	}

	/// <summary>
	/// Called after 1-second delay to apply reflected spell damage
	/// Validates target is still alive and applies appropriate effects
	/// </summary>
	private static void OnDelayedReflection(ReflectionContext context)
	{
		// Clean up context
		if (m_ReflectionContexts.ContainsKey(context.Reflector))
			m_ReflectionContexts.Remove(context.Reflector);

		// Validate target is still valid
		if (!IsValidReflectionTarget(context.OriginalCaster, context.Reflector))
			return;

		// Visual effect showing spell bouncing back
		context.OriginalCaster.FixedParticles(
			REFLECT_PARTICLE,
			10,
			15,
			5038,
			Server.Items.CharacterDatabase.GetMySpellHue(context.OriginalCaster, 0),
			2,
			EffectLayer.Head
		);
		context.OriginalCaster.PlaySound(0x213);

		// Note: The actual spell damage is handled by the normal spell flow
		// because NMSCheckReflect already swapped caster/target
		// This timer just adds the delay and visual feedback
	}

	/// <summary>
	/// Checks if a mobile is currently in delayed reflection state
	/// </summary>
	public static bool HasPendingReflection(Mobile mobile)
	{
		return m_ReflectionContexts.ContainsKey(mobile);
	}

	/// <summary>
	/// Gets the reflection context for a mobile if one exists
	/// </summary>
	public static ReflectionContext GetReflectionContext(Mobile mobile)
	{
		ReflectionContext context;
		if (m_ReflectionContexts.TryGetValue(mobile, out context))
			return context;

		return null;
	}

	#endregion
	}
}

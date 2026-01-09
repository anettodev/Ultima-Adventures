using System;
using Server.Mobiles;
using Server.Targeting;
using Server.Network;
using Server.Spells.Chivalry;
using Server.Spells;

namespace Server.Spells.Fifth
{
	/// <summary>
	/// Paralyze - 5th Circle Control Spell
	/// Freezes target in place, preventing movement and actions
	/// Duration based on caster's EvalInt, can be resisted for half duration
	/// </summary>
	public class ParalyzeSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Paralyze", "An Ex Por",
				218,
				9012,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }

		#region Constants

		// Duration calculation constants
		/// <summary>Base duration in seconds (5 seconds)</summary>
		private const double BASE_DURATION = 5.0;

		/// <summary>EvalInt contribution multiplier (10% of EvalInt skill added to duration)</summary>
		private const double EVAL_MULTIPLIER = 0.1;

		/// <summary>Resistance duration multiplier (50% duration when resisted)</summary>
		private const double RESISTANCE_MULTIPLIER = 0.5;

		// Visual and audio effect constants
		/// <summary>Resistance effect ID (magical barrier visual)</summary>
		private const int RESIST_EFFECT_ID = 0x37B9;

		/// <summary>Resistance effect speed</summary>
		private const int RESIST_EFFECT_SPEED = 10;

		/// <summary>Resistance effect duration</summary>
		private const int RESIST_EFFECT_DURATION = 5;

		/// <summary>Resistance particle effect ID</summary>
		private const int RESIST_PARTICLE_ID = 0x374A;

		/// <summary>Resistance particle speed</summary>
		private const int RESIST_PARTICLE_SPEED = 10;

		/// <summary>Resistance particle duration</summary>
		private const int RESIST_PARTICLE_DURATION = 30;

		/// <summary>Resistance particle effect hue</summary>
		private const int RESIST_PARTICLE_HUE = 5013;

		/// <summary>Paralyze sound effect ID</summary>
		private const int PARALYZE_SOUND_ID = 0x204;

		/// <summary>Paralyze visual effect ID</summary>
		private const int PARALYZE_EFFECT_ID = 0x376A;

		/// <summary>Paralyze effect speed</summary>
		private const int PARALYZE_EFFECT_SPEED = 6;

		/// <summary>Paralyze effect render mode</summary>
		private const int PARALYZE_EFFECT_RENDER = 1;

		#endregion

		public ParalyzeSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
		}

		/// <summary>
		/// Attempts to paralyze the target mobile
		/// Checks for existing paralysis, casting state, and resistance
		/// </summary>
		/// <param name="m">Target mobile to paralyze</param>
		public void Target(Mobile m)
		{
			if (!Caster.CanSee(m))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if (Core.AOS && IsAlreadyParalyzed(m))
			{
				// Target is already frozen or casting (except Paladin spells)
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_ALREADY_FROZEN);
			}
			else if (CheckHSequence(m))
			{
				SpellHelper.Turn(Caster, m);

				// Check for magic reflection
				SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);

				// Calculate duration: ((10% of EvalInt) + 5) seconds [- 50% if resisted]
				double duration = CalculateDuration();

				// Check if target resists the spell
				if (CheckResisted(m))
				{
					duration = ApplyResistance(m, duration);
				}

				// Apply paralysis
				m.Paralyze(TimeSpan.FromSeconds(duration));

				// Play visual and audio effects
				PlayParalyzeEffects(m);

				HarmfulSpell(m);
			}

			FinishSequence();
		}

		/// <summary>
		/// Checks if target is already paralyzed or in a state that prevents paralysis
		/// </summary>
		/// <param name="m">Mobile to check</param>
		/// <returns>True if already paralyzed or immune</returns>
		private bool IsAlreadyParalyzed(Mobile m)
		{
			// Already frozen or paralyzed
			if (m.Frozen || m.Paralyzed)
				return true;

			// Currently casting (except Paladin spells which don't prevent paralysis)
			if (m.Spell != null && m.Spell.IsCasting && !(m.Spell is PaladinSpell))
				return true;

			return false;
		}

		/// <summary>
		/// Calculates base paralyze duration based on caster's EvalInt
		/// </summary>
		/// <returns>Duration in seconds</returns>
		private double CalculateDuration()
		{
			return BASE_DURATION + (Caster.Skills[SkillName.EvalInt].Value * EVAL_MULTIPLIER);
		}

		/// <summary>
		/// Applies resistance effects, reducing duration and showing visual feedback
		/// </summary>
		/// <param name="target">Target that resisted</param>
		/// <param name="duration">Original duration</param>
		/// <returns>Reduced duration (50%)</returns>
		private double ApplyResistance(Mobile target, double duration)
		{
			// Reduce duration by half
			double reducedDuration = duration * RESISTANCE_MULTIPLIER;

			// Notify victim
			target.SendMessage(Spell.MSG_COLOR_SYSTEM, 
				String.Format(Spell.SpellMessages.PARALYZE_RESIST_VICTIM, (int)reducedDuration));

			// Visual feedback - resistance effects
			target.FixedEffect(RESIST_EFFECT_ID, RESIST_EFFECT_SPEED, RESIST_EFFECT_DURATION);
			target.FixedParticles(
				RESIST_PARTICLE_ID, 
				RESIST_PARTICLE_SPEED, 
				RESIST_PARTICLE_DURATION, 
				RESIST_PARTICLE_HUE, 
				Server.Items.CharacterDatabase.GetMySpellHue(target, 0), 
				2, 
				EffectLayer.Waist
			);

			// Notify attacker
			Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, 
				String.Format(Spell.SpellMessages.PARALYZE_RESIST_ATTACKER, (int)reducedDuration));

			return reducedDuration;
		}

		/// <summary>
		/// Plays the visual and audio effects for successful paralysis
		/// </summary>
		/// <param name="target">Paralyzed target</param>
		private void PlayParalyzeEffects(Mobile target)
		{
			// Audio feedback
			target.PlaySound(PARALYZE_SOUND_ID);

			// Visual effect
			target.FixedEffect(
				PARALYZE_EFFECT_ID, 
				PARALYZE_EFFECT_SPEED, 
				PARALYZE_EFFECT_RENDER, 
				Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0), 
				0
			);
		}

		public class InternalTarget : Target
		{
			private ParalyzeSpell m_Owner;

			public InternalTarget(ParalyzeSpell owner) : base(SpellConstants.GetSpellRange(), false, TargetFlags.Harmful)
			{
				m_Owner = owner;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (o is Mobile)
					m_Owner.Target((Mobile)o);
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}

using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;

namespace Server.Spells.Fifth
{
	/// <summary>
	/// Mind Blast - 5th Circle Attack Spell
	/// Deals direct mental damage based on the INT difference between caster and target
	/// Unique mechanic: damage can backfire if target has higher INT
	/// Not affected by slayer spellbooks
	/// </summary>
	public class MindBlastSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Mind Blast", "Por Corp Wis",
				218,
				Core.AOS ? 9002 : 9032,
				Reagent.BlackPearl,
				Reagent.MandrakeRoot,
				Reagent.Nightshade,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }

		#region Constants

		// Damage calculation constants
		/// <summary>Minimum damage dealt regardless of INT difference</summary>
		private const int MIN_DAMAGE = 3;

	/// <summary>Maximum damage cap</summary>
	private const int MAX_DAMAGE = 35;

		/// <summary>Random damage variance (0-3 subtracted from final damage)</summary>
		private const int RANDOM_DAMAGE_RANGE = 3;

		/// <summary>Resistance damage divisor (50% damage when resisted)</summary>
		private const double RESISTANCE_DIVISOR = 2.0;

		/// <summary>Additional damage variance in AOS mode (+0 to +4)</summary>
		private const int AOS_DAMAGE_VARIANCE = 4;

		// Effect constants - Caster (attacker) particle
		/// <summary>Particle effect ID for both caster and target</summary>
		private const int PARTICLE_EFFECT_ID = 0x374A;

		/// <summary>Particle effect speed</summary>
		private const int PARTICLE_SPEED = 10;

		/// <summary>Particle effect duration</summary>
		private const int PARTICLE_DURATION = 15;

		/// <summary>Particle effect hue for caster (lighter)</summary>
		private const int CASTER_PARTICLE_HUE = 2038;

		/// <summary>Particle effect hue for target (darker)</summary>
		private const int TARGET_PARTICLE_HUE = 5038;

		/// <summary>Spell hue offset for custom coloring</summary>
		private const int SPELL_HUE_OFFSET = 1181;

		/// <summary>Particle effect layer value</summary>
		private const int PARTICLE_LAYER = 2;

		// Audio constants
		/// <summary>Sound effect ID for mind blast impact</summary>
		private const int MIND_BLAST_SOUND = 0x213;

		// Timing constants
		/// <summary>Delay before applying AOS damage (seconds)</summary>
		private const double AOS_DAMAGE_DELAY = 1.0;

		#endregion

		public MindBlastSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
			if (Core.AOS)
				m_Info.LeftHandEffect = m_Info.RightHandEffect = 9002;
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
		}

		public override bool DelayedDamage { get { return !Core.AOS; } }

		/// <summary>
		/// Callback for AOS delayed damage application
		/// Applies damage with variance and visual effects
		/// </summary>
		/// <param name="state">State object containing caster, target, defender, and damage</param>
		private void AosDelay_Callback(object state)
		{
			object[] states = (object[])state;
			Mobile caster = (Mobile)states[0];
			Mobile target = (Mobile)states[1];
			Mobile defender = (Mobile)states[2];
			int damage = (int)states[3];

			if (caster.HarmfulCheck(defender))
			{
				// Apply damage with random variance
				SpellHelper.Damage(this, target, Utility.RandomMinMax(damage, damage + AOS_DAMAGE_VARIANCE), 0, 0, 100, 0, 0);

				// Visual and audio feedback
				target.FixedParticles(
					PARTICLE_EFFECT_ID, 
					PARTICLE_SPEED, 
					PARTICLE_DURATION, 
					TARGET_PARTICLE_HUE, 
					Server.Items.CharacterDatabase.GetMySpellHue(caster, SPELL_HUE_OFFSET), 
					PARTICLE_LAYER, 
					EffectLayer.Head
				);
				target.PlaySound(MIND_BLAST_SOUND);
			}
		}

		/// <summary>
		/// Calculates mind blast damage based on INT difference between caster and target
		/// Algorithm: abs(fromInt - targetInt) with MIN_DAMAGE floor and MAX_DAMAGE cap
		/// Damage scales with GetDamageScalar and includes random variance
		/// </summary>
		/// <param name="from">Caster mobile</param>
		/// <param name="target">Target mobile</param>
		/// <returns>Final calculated damage</returns>
		private double CalculateDamage(Mobile from, Mobile target)
		{
			int fromStat = from.Int;
			int targetStat = target.Int;
			int damage = 0;

			// Calculate base damage from INT difference
			// Note: If target has higher INT, they "reflect" the mental attack back
			if (targetStat < fromStat)
				damage = ((fromStat - targetStat) > MIN_DAMAGE) ? (fromStat - targetStat) : MIN_DAMAGE;
			else
				damage = ((targetStat - fromStat) > MIN_DAMAGE) ? (targetStat - fromStat) : MIN_DAMAGE;

			// Apply damage scalar and random variance
			double finalDamage = (GetDamageScalar(target) * damage) - Utility.RandomMinMax(0, RANDOM_DAMAGE_RANGE);

			// Apply damage cap
			if (finalDamage > MAX_DAMAGE)
				finalDamage = MAX_DAMAGE;

			// Check for resistance (50% damage)
			if (CheckResisted(target))
			{
				finalDamage /= RESISTANCE_DIVISOR;
				target.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.MIND_BLAST_RESIST);
			}

			// Play visual effects on both caster and target
			PlayMindBlastEffects(from, target);

			return finalDamage;
		}

		/// <summary>
		/// Plays the visual and audio effects for mind blast on both caster and target
		/// </summary>
		/// <param name="caster">Spell caster</param>
		/// <param name="target">Spell target</param>
		private void PlayMindBlastEffects(Mobile caster, Mobile target)
		{
			// Caster particle effect (lighter hue - sending)
			caster.FixedParticles(
				PARTICLE_EFFECT_ID, 
				PARTICLE_SPEED, 
				PARTICLE_DURATION, 
				CASTER_PARTICLE_HUE, 
				Server.Items.CharacterDatabase.GetMySpellHue(Caster, SPELL_HUE_OFFSET), 
				PARTICLE_LAYER, 
				EffectLayer.Head
			);

			// Target particle effect (darker hue - receiving)
			target.FixedParticles(
				PARTICLE_EFFECT_ID, 
				PARTICLE_SPEED, 
				PARTICLE_DURATION, 
				TARGET_PARTICLE_HUE, 
				Server.Items.CharacterDatabase.GetMySpellHue(Caster, SPELL_HUE_OFFSET), 
				PARTICLE_LAYER, 
				EffectLayer.Head
			);

			// Audio feedback
			target.PlaySound(MIND_BLAST_SOUND);
		}

		/// <summary>
		/// Attempts to mind blast the target
		/// Handles both AOS (delayed damage) and legacy (instant damage) systems
		/// </summary>
		/// <param name="m">Target mobile</param>
		public void Target(Mobile m)
		{
			if (!Caster.CanSee(m))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if (Core.AOS)
			{
				if (Caster.CanBeHarmful(m) && CheckSequence())
				{
					Mobile from = Caster;
					Mobile target = m;

					SpellHelper.Turn(from, target);

					// Check for magic reflection
					SpellHelper.NMSCheckReflect((int)this.Circle, ref from, ref target);

					// Calculate damage
					int damage = (int)CalculateDamage(from, target);

					// Apply damage after delay (AOS system)
					Timer.DelayCall(
						TimeSpan.FromSeconds(AOS_DAMAGE_DELAY),
						new TimerStateCallback(AosDelay_Callback),
						new object[] { Caster, target, m, damage }
					);
				}
			}
			else if (CheckHSequence(m))
			{
				// Legacy system - instant damage
				Mobile from = Caster;
				Mobile target = m;

				SpellHelper.Turn(from, target);

				// Check for magic reflection
				SpellHelper.NMSCheckReflect((int)this.Circle, ref from, ref target);

				// Calculate and apply damage immediately
				double damage = CalculateDamage(from, target);

				SpellHelper.Damage(this, target, damage, 0, 0, 100, 0, 0);
			}

			FinishSequence();
		}

		/// <summary>
		/// Mind Blast is not affected by slayer spellbooks
		/// This maintains balance since damage is based purely on INT difference
		/// </summary>
		/// <param name="target">Target mobile</param>
		/// <returns>Always 1.0 (no slayer bonus)</returns>
		public override double GetSlayerDamageScalar(Mobile target)
		{
			return 1.0; // This spell isn't affected by slayer spellbooks
		}

		private class InternalTarget : Target
		{
			private MindBlastSpell m_Owner;

			public InternalTarget(MindBlastSpell owner) : base(SpellConstants.GetSpellRange(), false, TargetFlags.Harmful)
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

using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Spells.Third
{
	/// <summary>
	/// Poison - 3rd Circle Attack Spell
	/// Applies poison to a target based on caster skills
	/// </summary>
	public class PoisonSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Poison", "In Nox",
				203,
				9051,
				Reagent.Nightshade
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		#region Constants
		// Poison Level Skill Requirements
		/// <summary>Minimum combined Magery + Poisoning skill for Level 4 (Lethal) poison</summary>
		private const int POISON_LEVEL_4_MIN_SKILLS = 240;
		/// <summary>Minimum Magery skill required for Level 3 (Deadly) poison</summary>
		private const int POISON_LEVEL_3_MAGERY_MIN = 120;
		/// <summary>Minimum Poisoning skill required for Level 3 (Deadly) poison</summary>
		private const int POISON_LEVEL_3_POISONING_MIN = 100;
		/// <summary>Minimum Magery skill required for Level 2 (Greater) poison</summary>
		private const int POISON_LEVEL_2_MAGERY_MIN = 100;
		/// <summary>Minimum Poisoning skill required for Level 2 (Greater) poison (alternative to EvalInt)</summary>
		private const int POISON_LEVEL_2_POISONING_MIN = 80;
		/// <summary>Minimum EvalInt skill required for Level 2 (Greater) poison (alternative to Poisoning)</summary>
		private const int POISON_LEVEL_2_EVAL_INT_MIN = 100;
		/// <summary>Minimum Magery skill required for Level 1 (Regular) poison</summary>
		private const int POISON_LEVEL_1_MAGERY_MIN = 80;
		/// <summary>Minimum Poisoning skill required for Level 1 (Regular) poison (alternative to EvalInt)</summary>
		private const int POISON_LEVEL_1_POISONING_MIN = 60;
		/// <summary>Minimum EvalInt skill required for Level 1 (Regular) poison (alternative to Poisoning)</summary>
		private const int POISON_LEVEL_1_EVAL_INT_MIN = 80;

		// Effect Constants
		/// <summary>Particle effect ID for poison spell visual effect</summary>
		private const int EFFECT_ID = 0x374A;
		/// <summary>Speed of particle effect animation</summary>
		private const int EFFECT_SPEED = 10;
		/// <summary>Render mode for particle effect</summary>
		private const int EFFECT_RENDER = 15;
		/// <summary>Duration of particle effect in milliseconds</summary>
		private const int EFFECT_DURATION = 5021;
		/// <summary>Sound ID played when poison spell is cast</summary>
		private const int SOUND_ID = 0x205;
		/// <summary>Sound ID played when Deadly or Lethal poison (Level 3-4) is successfully applied (audible to nearby players)</summary>
		private const int SOUND_HIGH_LEVEL_POISON = 0x1F5;
		/// <summary>Default hue for spell effects (0 = use character's custom hue)</summary>
		private const int DEFAULT_HUE = 0;

		// Target Constants
		/// <summary>Maximum targeting range for Mondain's Legacy client</summary>
		private const int TARGET_RANGE_ML = 10;
		/// <summary>Maximum targeting range for Legacy client</summary>
		private const int TARGET_RANGE_LEGACY = 12;
		#endregion

		public PoisonSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target(Mobile target)
		{
			if (!Caster.CanSee(target))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
				FinishSequence();
				return;
			}

			if (CheckHSequence(target))
			{
				Mobile source = Caster;
				SpellHelper.Turn(source, target);
				SpellHelper.NMSCheckReflect((int)this.Circle, ref source, ref target);

				PrepareTargetForPoison(target);

				if (CheckResisted(target))
				{
					target.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_RESIST_POISON);
				}
				else
				{
					int poisonLevel = CalculatePoisonLevel();
					Poison poison = Poison.GetPoison(poisonLevel);
					ApplyPoisonResult result = target.ApplyPoison(Caster, poison);
					
					// Play sound feedback for high-level poison (Deadly/Lethal) - audible to nearby players
					if (result == ApplyPoisonResult.Poisoned && poisonLevel >= 3)
					{
						Effects.PlaySound(target.Location, target.Map, SOUND_HIGH_LEVEL_POISON);
					}
					// Edge cases (immunity, higher poison active) handled silently - no feedback needed
				}

				PlayEffects(target);
				HarmfulSpell(target);
			}

			FinishSequence();
		}

		/// <summary>
		/// Prepares target for poison application
		/// </summary>
		/// <param name="target">The target mobile</param>
		private void PrepareTargetForPoison(Mobile target)
		{
			if (target.Spell != null)
				target.Spell.OnCasterHurt();

			target.Paralyzed = false;
		}

		/// <summary>
		/// Calculates poison level based on caster skills
		/// </summary>
		/// <returns>Poison level (0-4)</returns>
		private int CalculatePoisonLevel()
		{
			// Cache skill values for performance and clarity
			int magery = (int)Caster.Skills[SkillName.Magery].Value;
			int poisoning = (int)Caster.Skills[SkillName.Poisoning].Value;
			int evalInt = (int)Caster.Skills[SkillName.EvalInt].Value;
			int total = magery + poisoning;

			// Level 4: Highest poison level
			if (total >= POISON_LEVEL_4_MIN_SKILLS)
			{
				return 4;
			}
			// Level 3: Deadly poison
			else if (magery >= POISON_LEVEL_3_MAGERY_MIN && poisoning >= POISON_LEVEL_3_POISONING_MIN)
			{
				return 3;
			}
			// Level 2: Greater poison
			else if (magery >= POISON_LEVEL_2_MAGERY_MIN &&
					(poisoning >= POISON_LEVEL_2_POISONING_MIN || evalInt >= POISON_LEVEL_2_EVAL_INT_MIN))
			{
				return 2;
			}
			// Level 1: Regular poison
			else if (magery >= POISON_LEVEL_1_MAGERY_MIN &&
					(poisoning >= POISON_LEVEL_1_POISONING_MIN || evalInt >= POISON_LEVEL_1_EVAL_INT_MIN))
			{
				return 1;
			}
			// Level 0: Lesser poison (default)
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Plays visual and sound effects for the spell
		/// </summary>
		/// <param name="target">The target mobile</param>
		private void PlayEffects(Mobile target)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE);
			target.FixedParticles(EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_DURATION, hue, 0, EffectLayer.Waist);
			target.PlaySound(SOUND_ID);
		}

		private class InternalTarget : Target
		{
			private PoisonSpell m_Owner;

			public InternalTarget(PoisonSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.Harmful)
			{
				m_Owner = owner;
			}

		protected override void OnTarget(Mobile from, object o)
		{
			if (o is Mobile)
			{
				m_Owner.Target((Mobile)o);
			}
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
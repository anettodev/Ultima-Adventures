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
		private const int POISON_LEVEL_4_MIN_SKILLS = 240; // Magery + Poisoning >= 240
		private const int POISON_LEVEL_3_MAGERY_MIN = 120;
		private const int POISON_LEVEL_3_POISONING_MIN = 100;
		private const int POISON_LEVEL_2_MAGERY_MIN = 100;
		private const int POISON_LEVEL_2_POISONING_MIN = 80;
		private const int POISON_LEVEL_2_EVAL_INT_MIN = 100;
		private const int POISON_LEVEL_1_MAGERY_MIN = 80;
		private const int POISON_LEVEL_1_POISONING_MIN = 60;
		private const int POISON_LEVEL_1_EVAL_INT_MIN = 80;

		// Effect Constants
		private const int EFFECT_ID = 0x374A;
		private const int EFFECT_SPEED = 10;
		private const int EFFECT_RENDER = 15;
		private const int EFFECT_DURATION = 5021;
		private const int SOUND_ID = 0x205;
		private const int DEFAULT_HUE = 0;

		// Target Constants
		private const int TARGET_RANGE_ML = 10;
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
			}
			else if (CheckHSequence(target))
			{
				SpellHelper.Turn(Caster, target);
				SpellHelper.CheckReflect((int)this.Circle, Caster, ref target);

				PrepareTargetForPoison(target);

				if (CheckResisted(target))
				{
					target.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_RESIST_POISON);
				}
				else
				{
					int poisonLevel = CalculatePoisonLevel(target);
					target.ApplyPoison(Caster, Poison.GetPoison(poisonLevel));
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
		/// <param name="target">The target mobile (unused in calculation)</param>
		/// <returns>Poison level (0-4)</returns>
		private int CalculatePoisonLevel(Mobile target)
		{
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
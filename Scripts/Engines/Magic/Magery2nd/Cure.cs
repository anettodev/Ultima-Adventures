using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Second
{
	/// <summary>
	/// Cure - 2nd Circle Beneficial Spell
	/// Cures poison from the target based on caster's skill
	/// </summary>
	public class CureSpell : MagerySpell
	{
		#region Constants
		private const int DEADLY_POISON_LEVEL = 4;
		private const int KARMA_AWARD = 10;

		// Effect IDs
		private const int EFFECT_ID_FAIL = 0x374A;
		private const int EFFECT_ID_SUCCESS = 0x373A;
		private const int EFFECT_SPEED = 10;
		private const int EFFECT_RENDER = 15;
		private const int EFFECT_DURATION_FAIL = 5028;
		private const int EFFECT_DURATION_MORTAL = 5021;
		private const int EFFECT_DURATION_SUCCESS = 5012;

		// Sound IDs
		private const int SOUND_FAIL = 342;
		private const int SOUND_MORTAL_WOUND = 343;
		private const int SOUND_SUCCESS = 0x1E0;

		// Message Colors
		private const int MSG_COLOR_FAIL = 33;
		private const int MSG_COLOR_SUCCESS = 2253;

		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;
		#endregion

		private static SpellInfo m_Info = new SpellInfo(
				"Cure", "An Nox",
				212,
				9061,
				Reagent.Garlic,
				Reagent.Ginseng
			);

		public override SpellCircle Circle { get { return SpellCircle.Second; } }

		public CureSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
		}

		public void Target(Mobile target)
		{
			if (!Caster.CanSee(target))
			{
				Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if (CheckBSequence(target))
			{
				SpellHelper.Turn(Caster, target);

				Poison poison = target.Poison;

				if (poison != null)
				{
					// Check if can cure this poison (lethal poison cannot be cured)
					if (!SpellCureCalculator.CanCurePoison(poison) || IsTargetMortallyPoisoned(target))
					{
						HandleMortalPoison(target);
					}
					else
					{
						int cureChance = SpellCureCalculator.CalculateCureChance(Caster, poison);
						
						if (SpellCureCalculator.CheckCureSuccess(cureChance, poison))
						{
							HandleCureSuccess(target);
						}
						else
						{
							HandleCureFailed(target);
						}
					}
				}
			}

			FinishSequence();
		}

		/// <summary>
		/// Checks if target is mortally poisoned (Level 4+) or under Mortal Strike
		/// </summary>
		private bool IsTargetMortallyPoisoned(Mobile target)
		{
			return (target.Poisoned && target.Poison.Level >= DEADLY_POISON_LEVEL) 
			       || Server.Items.MortalStrike.IsWounded(target);
		}


		/// <summary>
		/// Handles mortal poison case (cannot cure with this spell)
		/// </summary>
		private void HandleMortalPoison(Mobile target)
		{
			target.LocalOverheadMessage(MessageType.Emote, MSG_COLOR_FAIL, true, "* Ouch! *");
			target.PlaySound(SOUND_MORTAL_WOUND);
			PlayEffectsMortalWound(target);

			string message = (Caster == target) 
				? SpellMessages.CURE_MORTAL_POISON_SELF 
				: SpellMessages.CURE_MORTAL_POISON_OTHER;

			Caster.SendMessage(MSG_COLOR_FAIL, message);
		}

		/// <summary>
		/// Handles failed cure attempt
		/// </summary>
		private void HandleCureFailed(Mobile target)
		{
			target.PlaySound(SOUND_FAIL);
			target.SendMessage(MSG_COLOR_FAIL, SpellMessages.CURE_FAILED);
			PlayEffectsFailed(target);
		}

		/// <summary>
		/// Handles successful cure
		/// </summary>
		private void HandleCureSuccess(Mobile target)
		{
			target.CurePoison(Caster);
			target.PlaySound(SOUND_SUCCESS);

			Misc.Titles.AwardKarma(Caster, KARMA_AWARD, true);

			string message = (Caster == target) 
				? SpellMessages.CURE_SUCCESS_SELF 
				: SpellMessages.CURE_SUCCESS_OTHER;

			Caster.SendMessage(MSG_COLOR_SUCCESS, message);
			PlayEffectsSuccess(target);
		}

		/// <summary>
		/// Plays effects for mortal wound case
		/// </summary>
		private void PlayEffectsMortalWound(Mobile target)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0);
			target.FixedParticles(EFFECT_ID_FAIL, EFFECT_SPEED, EFFECT_RENDER, EFFECT_DURATION_MORTAL, hue, 0, EffectLayer.Waist);
		}

		/// <summary>
		/// Plays effects for failed cure
		/// </summary>
		private void PlayEffectsFailed(Mobile target)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0);
			target.FixedParticles(EFFECT_ID_FAIL, EFFECT_SPEED, EFFECT_RENDER, EFFECT_DURATION_FAIL, hue, 0, EffectLayer.Waist);
		}

		/// <summary>
		/// Plays effects for successful cure
		/// </summary>
		private void PlayEffectsSuccess(Mobile target)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0);
			target.FixedParticles(EFFECT_ID_SUCCESS, EFFECT_SPEED, EFFECT_RENDER, EFFECT_DURATION_SUCCESS, hue, 0, EffectLayer.Waist);
		}

		public class InternalTarget : Target
		{
			private CureSpell m_Owner;

			public InternalTarget(CureSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.Beneficial)
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

using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells.Fourth;

namespace Server.Spells.Third
{
	/// <summary>
	/// Bless - 3rd Circle Beneficial Buff Spell
	/// Increases target's Strength, Dexterity, and Intelligence temporarily
	/// </summary>
	public class BlessSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Bless", "Rel Sanct",
				203,
				9061,
				Reagent.Garlic,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		#region Constants
		// Effect Constants
		private const int EFFECT_ID = 0x373A;
		private const int EFFECT_SPEED = 10;
		private const int EFFECT_RENDER = 15;
		private const int EFFECT_DURATION = 5018;
		private const int SOUND_ID = 0x1EA;
		private const int DEFAULT_HUE = 0;

		// Sorcerer Bonus Constants
		private const int SORCERER_DURATION_BONUS_SECONDS = 10;
		private const double SORCERER_SKILL_DIVISOR = 180.0; // (Magery + EvalInt) / 180

		// Target Constants
		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;

		// Curse Removal
		private const double CURSE_REMOVAL_CHANCE = 0.5; // 50%
		#endregion

		public BlessSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
			else if (!IsValidBlessTarget(target))
			{
				// Error messages handled in IsValidBlessTarget
			}
			else if (CheckBSequence(target))
			{
				SpellHelper.Turn(Caster, target);

				TryRemoveCurse(target);
				ApplyStatBonuses(target);
				ApplyCreatureBlessing(target);

				int percentage = CalculateBuffPercentage(target);
				TimeSpan duration = CalculateDuration(target);

			PlayEffects(target);
			BuffInfo.AddBuff(target, new BuffInfo(BuffIcon.Bless, 1075847, 1075848, duration, target, String.Format("{0}\t{0}\t{0}", percentage)));
			}

			FinishSequence();
		}

		/// <summary>
		/// Validates if target can receive the bless effect
		/// </summary>
		/// <param name="target">The target mobile</param>
		/// <returns>True if target is valid for blessing</returns>
	private bool IsValidBlessTarget(Mobile target)
	{
		if (target is BaseCreature)
		{
			BaseCreature creature = (BaseCreature)target;
			if (creature.IsBlessed)
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_ALREADY_BLESSED);
				return false;
			}
		}

			return true;
		}

		/// <summary>
		/// Attempts to remove Curse effect from target (50% chance)
		/// </summary>
		/// <param name="target">The target mobile</param>
		private void TryRemoveCurse(Mobile target)
		{
			if (target is PlayerMobile && CurseSpell.UnderEffect(target))
			{
				if (Utility.RandomDouble() < CURSE_REMOVAL_CHANCE)
				{
					CurseSpell.RemoveEffect(target);
					target.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.SUCCESS_CURSE_REMOVED);
					target.UpdateResistances();
				}
				else
				{
					target.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_CURSE_REMOVE_FAILED);
				}
			}
		}

		/// <summary>
		/// Applies stat bonuses to all three stats (Str, Dex, Int)
		/// Follows the Strength/Agility/Cunning pattern
		/// </summary>
		/// <param name="target">The target mobile</param>
		private void ApplyStatBonuses(Mobile target)
		{
			SpellHelper.AddStatBonus(Caster, target, StatType.Str);
			SpellHelper.AddStatBonus(Caster, target, StatType.Dex);
			SpellHelper.AddStatBonus(Caster, target, StatType.Int);
		}

		/// <summary>
		/// Applies permanent blessing to BaseCreature
		/// </summary>
		/// <param name="target">The target mobile</param>
	private void ApplyCreatureBlessing(Mobile target)
	{
		if (target is BaseCreature)
		{
			BaseCreature creature = (BaseCreature)target;
			creature.IsBlessed = true;
		}
		}

		/// <summary>
		/// Calculates buff percentage with sorcerer bonus
		/// </summary>
		/// <param name="target">The target mobile</param>
		/// <returns>Buff percentage value</returns>
		private int CalculateBuffPercentage(Mobile target)
		{
			int percentage = (int)(SpellHelper.GetOffsetScalar(Caster, target, false) * 100);

		// Apply sorcerer bonus
		if (Caster is PlayerMobile)
		{
			PlayerMobile playerMobile = (PlayerMobile)Caster;
			if (playerMobile.Sorcerer())
			{
				double bonus = (Caster.Skills[SkillName.Magery].Value + Caster.Skills[SkillName.EvalInt].Value) / SORCERER_SKILL_DIVISOR;
				percentage = (int)((double)percentage * (1.0 + bonus));
			}
		}

			return percentage;
		}

		/// <summary>
		/// Calculates buff duration with sorcerer bonus
		/// </summary>
		/// <param name="target">The target mobile</param>
		/// <returns>Buff duration</returns>
		private TimeSpan CalculateDuration(Mobile target)
		{
			TimeSpan duration = SpellHelper.NMSGetDuration(Caster, target, true);

		// Apply sorcerer bonus
		if (Caster is PlayerMobile)
		{
			PlayerMobile playerMobile = (PlayerMobile)Caster;
			if (playerMobile.Sorcerer())
			{
				duration += TimeSpan.FromSeconds(SORCERER_DURATION_BONUS_SECONDS);
			}
		}

			return duration;
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

		public class InternalTarget : Target
		{
			private BlessSpell m_Owner;

			public InternalTarget(BlessSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.Beneficial)
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

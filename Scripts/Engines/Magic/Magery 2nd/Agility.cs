using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Second
{
	/// <summary>
	/// Agility - 2nd Circle Buff Spell
	/// Increases target's Dexterity temporarily
	/// </summary>
	public class AgilitySpell : MagerySpell
	{
		#region Constants
		private const int EFFECT_ID = 0x375A;
		private const int EFFECT_SPEED = 10;
		private const int EFFECT_RENDER = 15;
		private const int EFFECT_DURATION = 5010;
		private const int SOUND_ID = 0x1e7;
		private const int DEFAULT_HUE = 0;

		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;
		#endregion

		private static SpellInfo m_Info = new SpellInfo(
				"Agility", "Ex Uus",
				212,
				9061,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.Second; } }

		public AgilitySpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
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
				int percentage = CalculateBuffPercentage(target);
				TimeSpan duration = SpellHelper.NMSGetDuration(Caster, target, true);

				SpellHelper.Turn(Caster, target);
				SpellHelper.AddStatBonus(Caster, target, StatType.Dex);

				PlayEffects(target);
				BuffInfo.AddBuff(target, new BuffInfo(BuffIcon.Agility, 1075841, duration, target, percentage.ToString()));
			}

			FinishSequence();
		}

		/// <summary>
		/// Calculates buff percentage based on caster/target relationship
		/// </summary>
		private int CalculateBuffPercentage(Mobile target)
		{
			return (int)(SpellHelper.GetOffsetScalar(Caster, target, false) * 100);
		}

		/// <summary>
		/// Plays visual and sound effects for the spell
		/// </summary>
		private void PlayEffects(Mobile target)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE);
			target.FixedParticles(EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_DURATION, hue, 0, EffectLayer.Waist);
			target.PlaySound(SOUND_ID);
		}

		public class InternalTarget : Target
		{
			private AgilitySpell m_Owner;

			public InternalTarget(AgilitySpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.Beneficial)
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

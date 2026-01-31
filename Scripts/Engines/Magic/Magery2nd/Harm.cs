using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Spells.Second
{
	/// <summary>
	/// Harm - 2nd Circle Attack Spell
	/// Deals cold damage to the target at close range
	/// </summary>
	public class HarmSpell : MagerySpell
	{
		#region Constants
		private const int DAMAGE_BONUS = 4;
		private const int DAMAGE_DICE = 1;
		private const int DAMAGE_SIDES = 4;

		private const int EFFECT_ID = 0x374A;
		private const int EFFECT_SPEED = 10;
		private const int EFFECT_DURATION = 30;
		private const int EFFECT_RENDER = 5013;
		private const int EFFECT_LAYER_OFFSET = 2;
		private const int SOUND_ID = 0x0FC;
		private const int DEFAULT_HUE = 0;

		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;
		#endregion

		private static SpellInfo m_Info = new SpellInfo(
				"Harm", "An Mani",
				212,
				Core.AOS ? 9001 : 9041,
				Reagent.Nightshade,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Second; } }

		public HarmSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
		}

		public override bool DelayedDamage { get { return false; } }

		public override double GetSlayerDamageScalar(Mobile target)
		{
			return 1.0; // This spell isn't affected by slayer spellbooks
		}

		public void Target(Mobile target)
		{
			if (!Caster.CanSee(target))
			{
				Caster.SendMessage(MSG_COLOR_ERROR, SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if (CheckHSequence(target))
			{
				SpellHelper.Turn(Caster, target);
				SpellHelper.CheckReflect((int)Circle, Caster, ref target);

				double damage = CalculateDamage(target);
				PlayEffects(target);

				// Apply damage (0% physical, 0% fire, 100% cold, 0% poison, 0% energy)
				SpellHelper.Damage(this, target, damage, 0, 0, 100, 0, 0);
			}

			FinishSequence();
		}

		/// <summary>
		/// Calculates spell damage
		/// </summary>
		private double CalculateDamage(Mobile target)
		{
			return GetNMSDamage(DAMAGE_BONUS, DAMAGE_DICE, DAMAGE_SIDES, target);
		}

		/// <summary>
		/// Plays visual and sound effects for the spell
		/// </summary>
		private void PlayEffects(Mobile target)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE);
			target.FixedParticles(EFFECT_ID, EFFECT_SPEED, EFFECT_DURATION, EFFECT_RENDER, hue, EFFECT_LAYER_OFFSET, EffectLayer.Waist);
			target.PlaySound(SOUND_ID);
		}

		private class InternalTarget : Target
		{
			private HarmSpell m_Owner;

			public InternalTarget(HarmSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.Harmful)
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

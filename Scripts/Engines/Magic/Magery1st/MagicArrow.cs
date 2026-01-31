using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.First
{
	/// <summary>
	/// Magic Arrow - 1st Circle Attack Spell
	/// Fires a magical projectile at the target
	/// </summary>
	public class MagicArrowSpell : MagerySpell
	{
		#region Constants
		private const int DAMAGE_BONUS = 2;
		private const int DAMAGE_DICE = 1;
		private const int DAMAGE_SIDES = 3;
		private const int DAMAGE_CAP = 8;
		
		private const int EFFECT_ID = 0x36E4;
		private const int EFFECT_SPEED = 5;
		private const int EFFECT_DURATION = 3600;
		private const int SOUND_ID = 0x1E5;
		
		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;
		#endregion

		private static SpellInfo m_Info = new SpellInfo(
				"Magic Arrow", "In Por Ylem",
				212,
				9041,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.First; } }

		public MagicArrowSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
		{
		}

		public override bool DelayedDamageStacking { get { return !Core.AOS; } }
		public override bool DelayedDamage { get { return true; } }

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
			else if (CheckHSequence(target))
			{
				Mobile source = Caster;

				SpellHelper.Turn(source, target);
				SpellHelper.NMSCheckReflect((int)Circle, ref source, ref target);

				double damage = CalculateDamage(target);

				// Visual and sound effects
				PlayEffects(source, target);

				// Apply damage (0% physical, 100% fire, 0% cold, 0% poison, 0% energy)
				SpellHelper.Damage(this, target, damage, 0, 100, 0, 0, 0);
			}

			FinishSequence();
		}

		/// <summary>
		/// Calculates spell damage with cap
		/// </summary>
		private double CalculateDamage(Mobile target)
		{
			double damage = GetNMSDamage(DAMAGE_BONUS, DAMAGE_DICE, DAMAGE_SIDES, target);
			
			// Apply damage cap for balance
			if (damage >= DAMAGE_CAP)
			{
				damage = DAMAGE_CAP;
			}

			return damage;
		}

		/// <summary>
		/// Plays visual and sound effects for the spell
		/// </summary>
		private void PlayEffects(Mobile source, Mobile target)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0);
			source.MovingParticles(target, EFFECT_ID, EFFECT_SPEED, 0, false, false, hue, 0, EFFECT_DURATION, 0, 0, 0);
			source.PlaySound(SOUND_ID);
		}

		private class InternalTarget : Target
		{
			private MagicArrowSpell m_Owner;

			public InternalTarget(MagicArrowSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.Harmful)
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

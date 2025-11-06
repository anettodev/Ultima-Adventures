using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Third
{
	/// <summary>
	/// Fireball - 3rd Circle Attack Spell
	/// Deals fire damage to a target
	/// </summary>
	public class FireballSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Fireball", "Vas Flam",
				203,
				9041,
				Reagent.BlackPearl
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		#region Constants
		// Damage Constants
		private const int DAMAGE_BONUS = 4;
		private const int DAMAGE_DICE = 1;
		private const int DAMAGE_SIDES = 6;

		// Effect Constants
		private const int EFFECT_ID = 0x36D4;
		private const int EFFECT_SPEED = 7;
		private const int EFFECT_DURATION = 9502;
		private const int EFFECT_ITEM_ID = 4019;
		private const int EFFECT_LAYER = 0x160;
		private const int SOUND_ID_AOS = 0x15E;
		private const int SOUND_ID_LEGACY = 0x44B;
		private const int DEFAULT_HUE = 0;

		// Target Constants
		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;
		#endregion

		public FireballSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( Mobile target )
		{
			if ( !Caster.CanSee( target ) )
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if ( CheckHSequence( target ) )
			{
				Mobile source = Caster;

				SpellHelper.Turn( source, target );
				SpellHelper.NMSCheckReflect( (int)this.Circle, ref source, ref target );

				double damage = CalculateDamage(target);
				PlayEffects(source, target);

				// Apply damage (0% physical, 100% fire, 0% cold, 0% poison, 0% energy)
				SpellHelper.Damage( this, target, damage, 0, 100, 0, 0, 0 );
			}

			FinishSequence();
		}

		/// <summary>
		/// Calculates spell damage using NMS system
		/// </summary>
		/// <param name="target">The target mobile</param>
		/// <returns>Calculated damage amount</returns>
		private double CalculateDamage(Mobile target)
		{
			return GetNMSDamage(DAMAGE_BONUS, DAMAGE_DICE, DAMAGE_SIDES, target);
		}

		/// <summary>
		/// Plays visual and sound effects for the spell
		/// </summary>
		/// <param name="source">The source mobile</param>
		/// <param name="target">The target mobile</param>
		private void PlayEffects(Mobile source, Mobile target)
		{
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE);
			source.MovingParticles(target, EFFECT_ID, EFFECT_SPEED, 0, false, true, hue, 0, EFFECT_DURATION, EFFECT_ITEM_ID, EFFECT_LAYER, 0);
			source.PlaySound(Core.AOS ? SOUND_ID_AOS : SOUND_ID_LEGACY);
		}

		private class InternalTarget : Target
		{
			private FireballSpell m_Owner;

			public InternalTarget(FireballSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.Harmful)
			{
				m_Owner = owner;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (o is Mobile mobile)
				{
					m_Owner.Target(mobile);
				}
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}

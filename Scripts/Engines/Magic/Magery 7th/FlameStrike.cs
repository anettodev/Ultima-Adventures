using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Spells.Seventh
{
	/// <summary>
	/// Flame Strike - 7th Circle Magery Spell
	/// Single-target fire damage spell
	/// </summary>
	public class FlameStrikeSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Flame Strike", "Kal Vas Flam",
				245,
				9042,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Seventh; } }

		#region Constants

		/// <summary>Base damage bonus (reduced by 20% for balance)</summary>
		private const int BASE_DAMAGE_BONUS = 32;

		/// <summary>Damage dice count</summary>
		private const int DAMAGE_DICE_COUNT = 1;

		/// <summary>Damage dice sides</summary>
		private const int DAMAGE_DICE_SIDES = 6;

		/// <summary>Particle effect ID</summary>
		private const int PARTICLE_EFFECT_ID = 0x3709;

		/// <summary>Particle count</summary>
		private const int PARTICLE_COUNT = 10;

		/// <summary>Particle speed</summary>
		private const int PARTICLE_SPEED = 30;

		/// <summary>Particle duration</summary>
		private const int PARTICLE_DURATION = 5052;

		/// <summary>Sound effect ID</summary>
		private const int SOUND_EFFECT = 0x208;

		#endregion

		public FlameStrikeSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE );
			}
			else if ( CheckHSequence( m ) )
			{
				Mobile source = Caster;
				SpellHelper.Turn( Caster, m );

				SpellHelper.NMSCheckReflect( (int)this.Circle, ref source, ref m );

				double damage;

				int nBenefit = 0;
				damage = GetNMSDamage( BASE_DAMAGE_BONUS, DAMAGE_DICE_COUNT, DAMAGE_DICE_SIDES, m ) + nBenefit;

				m.FixedParticles( PARTICLE_EFFECT_ID, PARTICLE_COUNT, PARTICLE_SPEED, PARTICLE_DURATION, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.LeftFoot );
				m.PlaySound( SOUND_EFFECT );

				SpellHelper.Damage( this, m, damage, 0, 100, 0, 0, 0 );
				if ( Scroll is SoulShard )
				{
					((SoulShard)Scroll).SuccessfulCast = true;
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private FlameStrikeSpell m_Owner;

			public InternalTarget( FlameStrikeSpell owner ) : base( SpellConstants.GetSpellRange(), false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}

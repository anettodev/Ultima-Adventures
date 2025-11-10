using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;

namespace Server.Spells.Sixth
{
	/// <summary>
	/// Explosion - 6th Circle Magery Spell
	/// Delayed area damage spell that explodes after a short delay
	/// </summary>
	public class ExplosionSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Explosion", "Vas Ort Flam",
				230,
				9041,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

		#region Constants

		/// <summary>Delay before explosion in AOS mode (seconds)</summary>
		private const double DELAY_AOS_SECONDS = 3.3;

		/// <summary>Delay before explosion in Legacy mode (seconds)</summary>
		private const double DELAY_LEGACY_SECONDS = 2.8;

		/// <summary>Base damage bonus for NMS damage calculation</summary>
		private const int DAMAGE_BONUS = 25;

		/// <summary>Number of dice for NMS damage calculation</summary>
		private const int DAMAGE_DICE = 1;

		/// <summary>Sides per die for NMS damage calculation</summary>
		private const int DAMAGE_SIDES = 5;

		/// <summary>Legacy mode damage minimum</summary>
		private const int DAMAGE_LEGACY_MIN = 23;

		/// <summary>Legacy mode damage maximum</summary>
		private const int DAMAGE_LEGACY_MAX = 44;

		/// <summary>Resistance damage multiplier (AOS)</summary>
		private const double RESIST_MULTIPLIER_AOS = 0.5;

		/// <summary>Resistance damage multiplier (Legacy)</summary>
		private const double RESIST_MULTIPLIER_LEGACY = 0.75;

		/// <summary>Particle effect ID (head particles)</summary>
		private const int PARTICLE_EFFECT_HEAD = 0x36BD;

		/// <summary>Particle effect count</summary>
		private const int PARTICLE_COUNT = 20;

		/// <summary>Particle effect speed</summary>
		private const int PARTICLE_SPEED = 10;

		/// <summary>Particle effect duration</summary>
		private const int PARTICLE_DURATION = 5044;

		/// <summary>Location effect ID</summary>
		private const int LOCATION_EFFECT_ID = 0x3822;

		/// <summary>Location effect duration</summary>
		private const int LOCATION_EFFECT_DURATION = 60;

		/// <summary>Sound effect ID</summary>
		private const int SOUND_EFFECT = 0x307;

		#endregion

		public ExplosionSpell( Mobile caster, Item scroll )
			: base( caster, scroll, m_Info )
		{
		}

		public override bool DelayedDamageStacking { get { return !Core.AOS; } }

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage { get { return false; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
                Caster.SendMessage(55, "O alvo n�o pode ser visto.");
            }
			else if ( Caster.CanBeHarmful( m ) && CheckSequence() )
			{
				Mobile attacker = Caster, defender = m;

				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int) this.Circle, Caster, ref m );

				InternalTimer t = new InternalTimer( this, attacker, defender, m );
				t.Start();
			}

			FinishSequence();
		}

		#region Internal Classes

		private class InternalTimer : Timer
		{
			private MagerySpell m_Spell;
			private Mobile m_Target;
			private Mobile m_Attacker, m_Defender;

			public InternalTimer( MagerySpell spell, Mobile attacker, Mobile defender, Mobile target )
				: base( TimeSpan.FromSeconds( Core.AOS ? DELAY_AOS_SECONDS : DELAY_LEGACY_SECONDS ) )
			{
				m_Spell = spell;
				m_Attacker = attacker;
				m_Defender = defender;
				m_Target = target;

				if ( m_Spell != null )
					m_Spell.StartDelayedDamageContext( attacker, this );

				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				if ( m_Attacker.HarmfulCheck( m_Defender ) )
				{
					double damage;

					int nBenefit = 0;

					if ( Core.AOS )
					{
						damage = m_Spell.GetNMSDamage( DAMAGE_BONUS, DAMAGE_DICE, DAMAGE_SIDES, m_Defender ) + nBenefit;
                        if (m_Spell.CheckResisted(m_Target))
                        {
							damage *= RESIST_MULTIPLIER_AOS;
                            m_Target.SendMessage(55, "Sua aura m�gica lhe ajudou a resistir metade do dano desse feiti�o.");
                            m_Attacker.SendMessage(55, "O oponente resistiu metade do dano desse feiti�o.");
                        }
                    }
					else
					{
						damage = Utility.Random( DAMAGE_LEGACY_MIN, DAMAGE_LEGACY_MAX - DAMAGE_LEGACY_MIN + 1 ) + nBenefit;

						if ( m_Spell.CheckResisted( m_Target ) )
						{
							damage *= RESIST_MULTIPLIER_LEGACY;
							m_Target.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
						}

						damage *= m_Spell.GetDamageScalar( m_Target );
					}

					if ( Utility.RandomBool() )
					{
						m_Target.FixedParticles( PARTICLE_EFFECT_HEAD, PARTICLE_COUNT, PARTICLE_SPEED, PARTICLE_DURATION, Server.Items.CharacterDatabase.GetMySpellHue( m_Attacker, 0 ), 0, EffectLayer.Head );
					}
					else
					{
						Effects.SendLocationEffect( m_Target.Location, m_Target.Map, LOCATION_EFFECT_ID, LOCATION_EFFECT_DURATION, PARTICLE_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( m_Attacker, 0 ), 0 );
					}
					m_Target.PlaySound( SOUND_EFFECT );

					SpellHelper.Damage( m_Spell, m_Target, damage, 0, 100, 0, 0, 0 );

					if ( m_Spell != null )
						m_Spell.RemoveDelayedDamageContext( m_Attacker );
				}
			}
		}

		private class InternalTarget : Target
		{
			private ExplosionSpell m_Owner;

			public InternalTarget( ExplosionSpell owner )
				: base( SpellConstants.GetSpellRange(), false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile) o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

		#endregion
	}
}
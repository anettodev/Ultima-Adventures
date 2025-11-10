using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Spells.Fourth
{
	public class LightningSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Lightning", "Por Ort Grav",
				239,
				9021,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Fourth; } }

		public LightningSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return false; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
                Caster.SendMessage(55, "O alvo n�o pode ser visto.");
            }
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

				double damage;

				// Use circle-based damage params for consistent progression
				int bonus, dice, sides;
				SpellDamageCalculator.GetCircleDamageParams(SpellCircle.Fourth, out bonus, out dice, out sides);
				
				damage = GetNMSDamage(bonus, dice, sides, m);
				
				// Apply minimum damage floor based on EvalInt
				int baseMinDamage = bonus; // Minimum is the bonus value
				int minDamage = SpellDamageCalculator.GetMinimumDamageFloor(Caster, baseMinDamage);
				damage = Math.Max(damage, minDamage);

                if ( Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ) > 0 )
				{
					Point3D blast = new Point3D( ( m.X ), ( m.Y ), m.Z+10 );
					Effects.SendLocationEffect( blast, m.Map, 0x2A4E, 30, 10, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 );
					m.PlaySound( 0x029 );
				}
				else
				{
					m.BoltEffect( 0 );
				}

				SpellHelper.Damage( this, m, damage, 0, 0, 0, 0, 100 );
/*				if (Scroll is SoulShard) {
					((SoulShard)Scroll).SuccessfulCast = true;
				}*/
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private LightningSpell m_Owner;

			public InternalTarget( LightningSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}

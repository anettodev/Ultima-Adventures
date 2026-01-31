using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Spells.Sixth
{
	/// <summary>
	/// Energy Bolt - 6th Circle Magery Spell
	/// Instant energy damage spell
	/// </summary>
	public class EnergyBoltSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Energy Bolt", "Corp Por",
				230,
				9022,
				Reagent.BlackPearl,
				Reagent.Nightshade
			);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

		#region Constants

		/// <summary>Base damage bonus for NMS damage calculation</summary>
		private const int DAMAGE_BONUS = 23;

		/// <summary>Number of dice for NMS damage calculation</summary>
		private const int DAMAGE_DICE = 1;

		/// <summary>Sides per die for NMS damage calculation</summary>
		private const int DAMAGE_SIDES = 4;

		/// <summary>Particle effect ID</summary>
		private const int PARTICLE_EFFECT_ID = 0x3818;

		/// <summary>Particle effect count</summary>
		private const int PARTICLE_COUNT = 7;

		/// <summary>Particle effect speed</summary>
		private const int PARTICLE_SPEED = 0;

		/// <summary>Particle effect item ID 1</summary>
		private const int PARTICLE_ITEM_ID_1 = 3043;

		/// <summary>Particle effect item ID 2</summary>
		private const int PARTICLE_ITEM_ID_2 = 4043;

		/// <summary>Particle effect item ID 3</summary>
		private const int PARTICLE_ITEM_ID_3 = 0x211;

		/// <summary>Sound effect ID</summary>
		private const int SOUND_EFFECT = 0x20A;

		#endregion

		public EnergyBoltSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

				damage = GetNMSDamage( DAMAGE_BONUS, DAMAGE_DICE, DAMAGE_SIDES, m ) + nBenefit;

				// Do the effects
				source.MovingParticles( m, PARTICLE_EFFECT_ID, PARTICLE_COUNT, PARTICLE_SPEED, false, true, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, PARTICLE_ITEM_ID_1, PARTICLE_ITEM_ID_2, PARTICLE_ITEM_ID_3, 0 );
				source.PlaySound( SOUND_EFFECT );

				// Deal the damage
				SpellHelper.Damage( this, m, damage, 0, 0, 0, 0, 100 );
				if ( Scroll is SoulShard )
				{
					((SoulShard)Scroll).SuccessfulCast = true;
				}
			}

			FinishSequence();
		}

		#region Internal Classes

		private class InternalTarget : Target
		{
			private EnergyBoltSpell m_Owner;

			public InternalTarget( EnergyBoltSpell owner ) : base( SpellConstants.GetSpellRange(), false, TargetFlags.Harmful )
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

		#endregion
	}
}

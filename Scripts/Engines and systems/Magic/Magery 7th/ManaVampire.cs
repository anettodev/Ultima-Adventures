using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;

namespace Server.Spells.Seventh
{
	/// <summary>
	/// Mana Vampire - 7th Circle Magery Spell
	/// Drains mana from target and transfers it to the caster
	/// </summary>
	public class ManaVampireSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Mana Vampire", "Ort Sanct",
				221,
				9032,
				Reagent.BlackPearl,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Seventh; } }

		#region Constants

		/// <summary>Mana drain multiplier based on Magery skill</summary>
		private const double MANA_DRAIN_MULTIPLIER = 0.20;

		/// <summary>Resist percent for this spell</summary>
		private const double RESIST_PERCENT = 98.0;

		/// <summary>Particle effect ID for target</summary>
		private const int PARTICLE_EFFECT_TARGET = 0x374A;

		/// <summary>Particle count for target</summary>
		private const int PARTICLE_COUNT_TARGET = 1;

		/// <summary>Particle speed for target</summary>
		private const int PARTICLE_SPEED_TARGET = 15;

		/// <summary>Particle duration for target</summary>
		private const int PARTICLE_DURATION_TARGET = 5054;

		/// <summary>Particle effect ID for caster</summary>
		private const int PARTICLE_EFFECT_CASTER = 0x0000;

		/// <summary>Particle count for caster</summary>
		private const int PARTICLE_COUNT_CASTER = 10;

		/// <summary>Particle speed for caster</summary>
		private const int PARTICLE_SPEED_CASTER = 5;

		/// <summary>Particle duration for caster</summary>
		private const int PARTICLE_DURATION_CASTER = 2054;

		/// <summary>Sound effect ID</summary>
		private const int SOUND_EFFECT = 0x1F9;

		/// <summary>Spell hue index</summary>
		private const int SPELL_HUE_INDEX = 23;

		#endregion

		public ManaVampireSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE );
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

				if ( m.Spell != null )
					m.Spell.OnCasterHurt();

				m.Paralyzed = false;

				int toDrain = 0;
				toDrain = (int)((Caster.Skills[SkillName.Magery].Value * MANA_DRAIN_MULTIPLIER) * NMSUtils.getDamageEvalBenefit(Caster));

				if ( toDrain < 0 )
					toDrain = 0;
				else if ( toDrain > m.Mana )
					toDrain = m.Mana;

				if ( toDrain > (Caster.ManaMax - Caster.Mana) )
					toDrain = (Caster.ManaMax - Caster.Mana) - 1;

				m.Mana -= toDrain;
				Caster.Mana += Server.Misc.MyServerSettings.PlayerLevelMod( toDrain, Caster );
				Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, string.Format( Spell.SpellMessages.INFO_MANA_DRAINED_FORMAT, toDrain ) );
				m.SendMessage( Spell.MSG_COLOR_WARNING, Spell.SpellMessages.INFO_MANA_LOST );
				m.FixedParticles( PARTICLE_EFFECT_TARGET, PARTICLE_COUNT_TARGET, PARTICLE_SPEED_TARGET, PARTICLE_DURATION_TARGET, Server.Items.CharacterDatabase.GetMySpellHue( Caster, SPELL_HUE_INDEX ), 7, EffectLayer.Head );
				m.PlaySound( SOUND_EFFECT );

				Caster.FixedParticles( PARTICLE_EFFECT_CASTER, PARTICLE_COUNT_CASTER, PARTICLE_SPEED_CASTER, PARTICLE_DURATION_CASTER, Server.Items.CharacterDatabase.GetMySpellHue( Caster, SPELL_HUE_INDEX ), 7, EffectLayer.Head );

				HarmfulSpell( m );
			}

			FinishSequence();
		}

		public override double GetResistPercent( Mobile target )
		{
			return RESIST_PERCENT;
		}

		private class InternalTarget : Target
		{
			private ManaVampireSpell m_Owner;

			public InternalTarget( ManaVampireSpell owner ) : base( SpellConstants.GetSpellRange(), false, TargetFlags.Harmful )
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

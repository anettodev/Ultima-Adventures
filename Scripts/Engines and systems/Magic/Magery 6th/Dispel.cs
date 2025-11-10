using System;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;

namespace Server.Spells.Sixth
{
	/// <summary>
	/// Dispel - 6th Circle Magery Spell
	/// Dispels summoned creatures with chance based on skills
	/// </summary>
	public class DispelSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Dispel", "An Ort",
				218,
				9002,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

		#region Constants

		/// <summary>Chaos momentum minimum</summary>
		private const int CHAOS_MOMENTUM_MIN = 0;

		/// <summary>Chaos momentum maximum</summary>
		private const int CHAOS_MOMENTUM_MAX = 10;

		/// <summary>Low health threshold (percentage)</summary>
		private const double LOW_HEALTH_THRESHOLD = 0.2;

		/// <summary>Health divisor for dispel calculation</summary>
		private const int HEALTH_DIVISOR = 10;

		/// <summary>Special wizard creature control slots identifier</summary>
		private const int WIZARD_CREATURE_CONTROL_SLOTS = 666;

		/// <summary>Particle effect ID for successful dispel</summary>
		private const int DISPEL_PARTICLE_EFFECT_ID = 0x3728;

		/// <summary>Particle effect count</summary>
		private const int DISPEL_PARTICLE_COUNT = 8;

		/// <summary>Particle effect speed</summary>
		private const int DISPEL_PARTICLE_SPEED = 20;

		/// <summary>Particle effect duration</summary>
		private const int DISPEL_PARTICLE_DURATION = 5042;

		/// <summary>Sound effect ID for successful dispel</summary>
		private const int DISPEL_SOUND_EFFECT = 0x201;

		/// <summary>Effect ID for failed dispel</summary>
		private const int FAILED_DISPEL_EFFECT_ID = 0x3779;

		/// <summary>Failed dispel effect count</summary>
		private const int FAILED_DISPEL_EFFECT_COUNT = 10;

		/// <summary>Failed dispel effect duration</summary>
		private const int FAILED_DISPEL_EFFECT_DURATION = 20;

		#endregion

		public DispelSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		#region Helper Methods

		/// <summary>
		/// Attempts to dispel a creature
		/// </summary>
		private void AttemptDispel( Mobile from, BaseCreature bc )
		{
			int caosMomentum = Utility.RandomMinMax( CHAOS_MOMENTUM_MIN, CHAOS_MOMENTUM_MAX );
			double percentageLimit = NMSUtils.getSummonDispelPercentage( bc, caosMomentum );
			double dispelChance = NMSUtils.getDispelChance( from, bc, caosMomentum );

			if ( dispelChance >= percentageLimit )
			{
				// Successful dispel
				PerformSuccessfulDispel( from, bc );
			}
			else
			{
				// Failed dispel - check for low health bonus
				if ( IsLowHealth( bc ) && CheckLowHealthDispel( from, bc, dispelChance ) )
				{
					PerformSuccessfulDispel( from, bc );
				}
				else
				{
					PerformFailedDispel( from, bc );
				}
			}
		}

		/// <summary>
		/// Checks if creature is at low health
		/// </summary>
		private bool IsLowHealth( BaseCreature bc )
		{
			return bc.Hits < bc.HitsMax * LOW_HEALTH_THRESHOLD;
		}

		/// <summary>
		/// Checks if low health dispel succeeds
		/// </summary>
		private bool CheckLowHealthDispel( Mobile from, BaseCreature bc, double dispelChance )
		{
			return dispelChance >= (bc.Hits / HEALTH_DIVISOR);
		}

		/// <summary>
		/// Performs successful dispel effects and deletes creature
		/// </summary>
		private void PerformSuccessfulDispel( Mobile from, BaseCreature bc )
		{
			Effects.SendLocationParticles( EffectItem.Create( bc.Location, bc.Map, EffectItem.DefaultDuration ), DISPEL_PARTICLE_EFFECT_ID, DISPEL_PARTICLE_COUNT, DISPEL_PARTICLE_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( from, 0 ), 0, DISPEL_PARTICLE_DURATION, 0 );
			Effects.PlaySound( bc, bc.Map, DISPEL_SOUND_EFFECT );

			// Check if it was a low health dispel
			if ( IsLowHealth( bc ) )
			{
				from.SendMessage( Spell.MSG_COLOR_WARNING, bc.Name + " já estava sem forças e foi dissipado!" );
			}

			bc.Delete();
		}

		/// <summary>
		/// Performs failed dispel effects
		/// </summary>
		private void PerformFailedDispel( Mobile from, BaseCreature bc )
		{
			from.DoHarmful( bc );
			bc.FixedEffect( FAILED_DISPEL_EFFECT_ID, FAILED_DISPEL_EFFECT_COUNT, FAILED_DISPEL_EFFECT_DURATION, Server.Items.CharacterDatabase.GetMySpellHue( from, 0 ), 0 );
			from.SendMessage( Spell.MSG_COLOR_WARNING, "Você conseguiu irritar " + bc.Name );
		}

		/// <summary>
		/// Validates if target can be dispelled
		/// </summary>
		private bool CanDispelTarget( BaseCreature bc, object target )
		{
			if ( bc == null || !bc.IsDispellable )
				return false;

			if ( bc is BaseVendor )
				return false;

			if ( target is PlayerMobile )
				return false;

			return true;
		}

		/// <summary>
		/// Checks if target is a special wizard creature
		/// </summary>
		private bool IsWizardCreature( BaseCreature bc )
		{
			return bc.ControlSlots == WIZARD_CREATURE_CONTROL_SLOTS;
		}

		#endregion

		#region Internal Classes

		public class InternalTarget : Target
		{
			private DispelSpell m_Owner;

			public InternalTarget( DispelSpell owner ) : base( SpellConstants.GetSpellRange(), false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile && o is BaseCreature )
				{
					Mobile m = (Mobile)o;
					BaseCreature bc = m as BaseCreature;

					if ( !from.CanSee( m ) )
					{
						from.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE );
						m_Owner.FinishSequence();
						return;
					}

					if ( !m_Owner.CanDispelTarget( bc, o ) )
					{
						from.SendMessage( Spell.MSG_COLOR_ERROR, "Não é possível dissipar " + m.Name );
						m_Owner.FinishSequence();
						return;
					}

					// Check if it's dispellable, wizard creature, or passes sequence check
					if ( bc.IsDispellable || m_Owner.IsWizardCreature( bc ) || m_Owner.CheckHSequence( m ) )
					{
						SpellHelper.Turn( from, m );
						m_Owner.AttemptDispel( from, bc );
					}
					else
					{
						from.SendMessage( Spell.MSG_COLOR_ERROR, "Algo de estranho aconteceu neste feitiço." );
					}
				}

				m_Owner.FinishSequence();
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

		#endregion
	}
}

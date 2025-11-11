using System;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;

namespace Server.Spells.Seventh
{
	/// <summary>
	/// Mass Dispel - 7th Circle Magery Spell
	/// Dispels multiple summoned creatures and field spells in area
	/// </summary>
	public class MassDispelSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Mass Dispel", "Vas An Ort",
				263,
				9002,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.BlackPearl,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Seventh; } }

		#region Constants

		/// <summary>Mobile dispel range in tiles</summary>
		private const int MOBILE_DISPEL_RANGE = 8;

		/// <summary>Field dispel range in tiles</summary>
		private const int FIELD_DISPEL_RANGE = 4;

		/// <summary>Chaos momentum minimum</summary>
		private const int CHAOS_MOMENTUM_MIN = 0;

		/// <summary>Chaos momentum maximum</summary>
		private const int CHAOS_MOMENTUM_MAX = 10;

		/// <summary>Low health threshold (20%)</summary>
		private const double LOW_HEALTH_THRESHOLD = 0.2;

		/// <summary>Health divisor for low health dispel calculation</summary>
		private const int HEALTH_DIVISOR = 10;

		/// <summary>Control slots value for special creatures</summary>
		private const int SPECIAL_CONTROL_SLOTS = 666;

		/// <summary>Particle effect ID for fields</summary>
		private const int PARTICLE_EFFECT_FIELD = 0x376A;

		/// <summary>Particle count for fields</summary>
		private const int PARTICLE_COUNT_FIELD = 9;

		/// <summary>Particle speed for fields</summary>
		private const int PARTICLE_SPEED_FIELD = 20;

		/// <summary>Particle duration for fields</summary>
		private const int PARTICLE_DURATION_FIELD = 5042;

		/// <summary>Particle effect ID for creatures</summary>
		private const int PARTICLE_EFFECT_CREATURE = 0x3728;

		/// <summary>Particle count for creatures</summary>
		private const int PARTICLE_COUNT_CREATURE = 8;

		/// <summary>Particle speed for creatures</summary>
		private const int PARTICLE_SPEED_CREATURE = 20;

		/// <summary>Particle duration for creatures</summary>
		private const int PARTICLE_DURATION_CREATURE = 5042;

		/// <summary>Fixed effect ID for anger</summary>
		private const int FIXED_EFFECT_ANGER = 0x3779;

		/// <summary>Fixed effect duration</summary>
		private const int FIXED_EFFECT_DURATION = 10;

		/// <summary>Fixed effect speed</summary>
		private const int FIXED_EFFECT_SPEED = 20;

		/// <summary>Sound effect ID</summary>
		private const int SOUND_EFFECT = 0x201;

		#endregion

		public MassDispelSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE );
			}
			else if ( CheckSequence() )
			{
				int caosMomentum = Utility.RandomMinMax( CHAOS_MOMENTUM_MIN, CHAOS_MOMENTUM_MAX );
				SpellHelper.Turn( Caster, p );

				SpellHelper.GetSurfaceTop( ref p );

				List<Mobile> targets = new List<Mobile>();
				List<Item> fields = new List<Item>();

				Map map = Caster.Map;
				// Mapping all targets and fields
				if ( map != null )
				{
					IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), MOBILE_DISPEL_RANGE );

					foreach ( Mobile m in eable )
					{
						if ( m is BaseCreature )
						{
							BaseCreature mn = m as BaseCreature;
							if ( mn.ControlSlots == SPECIAL_CONTROL_SLOTS )
								targets.Add( m );
						}

						if ( m is BaseCreature && (m as BaseCreature).IsDispellable && Caster.CanBeHarmful( m, false ) )
							targets.Add( m );
					}

					eable.Free();

					eable = map.GetItemsInRange( new Point3D( p ), FIELD_DISPEL_RANGE );
					foreach ( Item i in eable )
						if ( i.GetType().IsDefined( typeof(DispellableFieldAttribute), false ) )
							fields.Add( i );
					eable.Free();
				}

				// Dispelling all fields in range
				ProcessFieldDispel( fields );

				// Dispelling all targets in range
				ProcessCreatureDispel( targets, caosMomentum );
			}

			FinishSequence();
		}

		#region Helper Methods

		/// <summary>
		/// Processes dispelling of all field spells in range
		/// </summary>
		private void ProcessFieldDispel( List<Item> fields )
		{
			foreach ( Item targ in fields )
			{
				if ( targ == null )
					continue;

				Effects.SendLocationParticles( EffectItem.Create( targ.Location, targ.Map, EffectItem.DefaultDuration ), PARTICLE_EFFECT_FIELD, PARTICLE_COUNT_FIELD, PARTICLE_SPEED_FIELD, PARTICLE_DURATION_FIELD );
				Effects.PlaySound( targ.GetWorldLocation(), targ.Map, SOUND_EFFECT );
				targ.Delete();
			}
		}

		/// <summary>
		/// Processes dispelling of all creatures in range
		/// </summary>
		private void ProcessCreatureDispel( List<Mobile> targets, int chaosMomentum )
		{
			foreach ( Mobile m in targets )
			{
				BaseCreature bc = m as BaseCreature;
				if ( bc == null )
					continue;

				TryDispelCreature( bc, chaosMomentum );
			}
		}

		/// <summary>
		/// Attempts to dispel a single creature
		/// </summary>
		private void TryDispelCreature( BaseCreature bc, int chaosMomentum )
		{
			double percentageLimit = NMSUtils.getSummonDispelPercentage( bc, chaosMomentum );
			double dispelChance = NMSUtils.getDispelChance( Caster, bc, chaosMomentum );

			if ( bc == null || !bc.IsDispellable || (bc is BaseVendor) || bc is PlayerMobile )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, string.Format( Spell.SpellMessages.ERROR_CANNOT_DISPEL_FORMAT, bc.Name ) );
			}
			else
			{
				if ( dispelChance >= percentageLimit )
				{
					DispelCreature( bc );
					Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, string.Format( Spell.SpellMessages.INFO_CREATURE_DISPELED_FORMAT, bc.Name ) );
				}
				else
				{
					if ( bc.Hits < bc.HitsMax * LOW_HEALTH_THRESHOLD )
					{
						if ( dispelChance >= (bc.Hits / HEALTH_DIVISOR) )
						{
							DispelCreature( bc );
							Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, string.Format( Spell.SpellMessages.INFO_CREATURE_DISPELED_LOW_HEALTH_FORMAT, bc.Name ) );
						}
					}
					else
					{
						AngerCreature( bc );
						Caster.SendMessage( Spell.MSG_COLOR_WARNING, string.Format( Spell.SpellMessages.INFO_CREATURE_ANGRY_FORMAT, bc.Name ) );
					}
				}
			}
		}

		/// <summary>
		/// Dispels a creature (removes it from the game)
		/// </summary>
		private void DispelCreature( BaseCreature bc )
		{
			Effects.SendLocationParticles( EffectItem.Create( bc.Location, bc.Map, EffectItem.DefaultDuration ), PARTICLE_EFFECT_CREATURE, PARTICLE_COUNT_CREATURE, PARTICLE_SPEED_CREATURE, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, PARTICLE_DURATION_CREATURE, 0 );
			Effects.PlaySound( bc, bc.Map, SOUND_EFFECT );
			bc.Delete();
		}

		/// <summary>
		/// Angers a creature (makes it hostile but doesn't dispel it)
		/// </summary>
		private void AngerCreature( BaseCreature bc )
		{
			Caster.DoHarmful( bc );
			bc.FixedEffect( FIXED_EFFECT_ANGER, FIXED_EFFECT_DURATION, FIXED_EFFECT_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 );
		}

		#endregion

		private class InternalTarget : Target
		{
			private MassDispelSpell m_Owner;

			public InternalTarget( MassDispelSpell owner ) : base( SpellConstants.GetSpellRange(), true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
					m_Owner.Target( p );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}

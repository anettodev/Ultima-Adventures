using System;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;

namespace Server.Spells.Seventh
{
	/// <summary>
	/// Meteor Swarm - 7th Circle Magery Spell
	/// Area-of-effect fire damage spell affecting multiple targets
	/// </summary>
	public class MeteorSwarmSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Meteor Swarm", "Flam Kal Des Ylem",
				233,
				9042,
				false,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Seventh; } }

		#region Constants

		/// <summary>Area of effect range in tiles</summary>
		private const int AOE_RANGE = 5;

		/// <summary>Base damage bonus</summary>
		private const int BASE_DAMAGE_BONUS = 38;

		/// <summary>Damage dice count</summary>
		private const int DAMAGE_DICE_COUNT = 2;

		/// <summary>Damage dice sides</summary>
		private const int DAMAGE_DICE_SIDES = 5;

		/// <summary>Minimum damage floor</summary>
		private const int MIN_DAMAGE_FLOOR = 12;

		/// <summary>Resist damage multiplier</summary>
		private const double RESIST_DAMAGE_MULTIPLIER = 0.5;

		/// <summary>Sound effect ID for cast</summary>
		private const int SOUND_EFFECT_CAST = 0x160;

		/// <summary>Sound effect ID for hit</summary>
		private const int SOUND_EFFECT_HIT = 0x15F;

		/// <summary>Particle effect ID option 1</summary>
		private const int PARTICLE_EFFECT_ID_1 = 0x33E5;

		/// <summary>Particle effect ID option 2</summary>
		private const int PARTICLE_EFFECT_ID_2 = 0x33F5;

		/// <summary>Particle effect duration</summary>
		private const int PARTICLE_EFFECT_DURATION = 85;

		/// <summary>Particle effect speed</summary>
		private const int PARTICLE_EFFECT_SPEED = 10;

		#endregion

		public MeteorSwarmSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE );
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				SpellHelper.Turn( Caster, p );

				if ( p is Item )
					p = ((Item)p).GetWorldLocation();

				List<Mobile> targets = new List<Mobile>();

				Map map = Caster.Map;

				bool playerVsPlayer = false;

				if ( map != null )
				{
					IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), AOE_RANGE );

					foreach ( Mobile m in eable )
					{
						if ( Caster.Region == m.Region && Caster != m && Caster.CanBeHarmful( m, true ) )
						{
							targets.Add( m );
							if ( m.Player )
								playerVsPlayer = true;
						}
					}

					eable.Free();
				}

				double damage;

				int nBenefit = 0;
				if ( Caster is PlayerMobile )
				{
					// Future benefit calculation can be added here
				}
				damage = GetNMSDamage( BASE_DAMAGE_BONUS, DAMAGE_DICE_COUNT, DAMAGE_DICE_SIDES, Caster.Player && playerVsPlayer ) + nBenefit;

				if ( targets.Count > 0 )
				{
					Effects.PlaySound( p, Caster.Map, SOUND_EFFECT_CAST );

					if ( targets.Count > 1 )
						damage /= targets.Count;

					if ( damage < MIN_DAMAGE_FLOOR )
						damage = MIN_DAMAGE_FLOOR;

					double toDeal;
					for ( int i = 0; i < targets.Count; ++i )
					{
						Mobile m = targets[i];

						toDeal = damage;

						if ( CheckResisted( m ) )
						{
							toDeal *= RESIST_DAMAGE_MULTIPLIER;
							m.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESIST_HALF_DAMAGE_VICTIM );
						}

						if ( m is PlayerMobile && m.FindItemOnLayer( Layer.Ring ) != null && m.FindItemOnLayer( Layer.Ring ) is OneRing )
						{
							m.SendMessage( Spell.MSG_COLOR_WARNING, Spell.SpellMessages.ONE_RING_PROTECTION_REVEAL );
						}
						else
						{
							m.RevealingAction();
						}

						Caster.DoHarmful( m );
						SpellHelper.Damage( this, m, toDeal, 0, 100, 0, 0, 0 );

						Point3D blast1 = new Point3D( m.X, m.Y, m.Z );
						Point3D blast2 = new Point3D( m.X - 1, m.Y, m.Z );
						Point3D blast3 = new Point3D( m.X + 1, m.Y, m.Z );
						Point3D blast4 = new Point3D( m.X, m.Y - 1, m.Z );
						Point3D blast5 = new Point3D( m.X, m.Y + 1, m.Z );

						Effects.SendLocationEffect( blast1, m.Map, Utility.RandomList( PARTICLE_EFFECT_ID_1, PARTICLE_EFFECT_ID_2 ), PARTICLE_EFFECT_DURATION, PARTICLE_EFFECT_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 );
						Effects.SendLocationEffect( blast2, m.Map, Utility.RandomList( PARTICLE_EFFECT_ID_1, PARTICLE_EFFECT_ID_2 ), PARTICLE_EFFECT_DURATION, PARTICLE_EFFECT_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 );
						Effects.SendLocationEffect( blast3, m.Map, Utility.RandomList( PARTICLE_EFFECT_ID_1, PARTICLE_EFFECT_ID_2 ), PARTICLE_EFFECT_DURATION, PARTICLE_EFFECT_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 );
						Effects.SendLocationEffect( blast4, m.Map, Utility.RandomList( PARTICLE_EFFECT_ID_1, PARTICLE_EFFECT_ID_2 ), PARTICLE_EFFECT_DURATION, PARTICLE_EFFECT_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 );
						Effects.SendLocationEffect( blast5, m.Map, Utility.RandomList( PARTICLE_EFFECT_ID_1, PARTICLE_EFFECT_ID_2 ), PARTICLE_EFFECT_DURATION, PARTICLE_EFFECT_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 );
						Effects.PlaySound( m.Location, m.Map, SOUND_EFFECT_HIT );
					}
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private MeteorSwarmSpell m_Owner;

			public InternalTarget( MeteorSwarmSpell owner ) : base( SpellConstants.GetSpellRange(), true, TargetFlags.None )
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

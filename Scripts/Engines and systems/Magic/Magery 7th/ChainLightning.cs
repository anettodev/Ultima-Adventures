using System;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Regions;
using Server.Mobiles;
using Server.Spells;

namespace Server.Spells.Seventh
{
	/// <summary>
	/// Chain Lightning - 7th Circle Magery Spell
	/// Area-of-effect lightning damage spell
	/// </summary>
	public class ChainLightningSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Chain Lightning", "Vas Ort Grav",
				209,
				9022,
				false,
				Reagent.BlackPearl,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
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

		/// <summary>One Ring damage multiplier</summary>
		private const double ONE_RING_DAMAGE_MULTIPLIER = 0.5;

		/// <summary>Particle effect ID (custom hue)</summary>
		private const int PARTICLE_EFFECT_ID = 0x2A4E;

		/// <summary>Particle effect duration</summary>
		private const int PARTICLE_EFFECT_DURATION = 30;

		/// <summary>Particle effect speed</summary>
		private const int PARTICLE_EFFECT_SPEED = 10;

		/// <summary>Particle Z offset</summary>
		private const int PARTICLE_Z_OFFSET = 10;

		/// <summary>Sound effect ID</summary>
		private const int SOUND_EFFECT = 0x029;

		#endregion

		public ChainLightningSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
						if ( Caster.Region == m.Region && Caster != m )
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
					if ( targets.Count > 1 )
						damage /= targets.Count;

					if ( damage < MIN_DAMAGE_FLOOR )
						damage = MIN_DAMAGE_FLOOR;

					for ( int i = 0; i < targets.Count; ++i )
					{
						Mobile m = targets[i];

						Region house = m.Region;

						double toDeal = damage;

						if ( CheckResisted( m ) )
						{
							toDeal *= RESIST_DAMAGE_MULTIPLIER;
							m.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESIST_HALF_DAMAGE_VICTIM );
						}

						if ( !(house is Regions.HouseRegion) )
						{
							if ( m is PlayerMobile && m.FindItemOnLayer( Layer.Ring ) != null && m.FindItemOnLayer( Layer.Ring ) is OneRing )
							{
								m.SendMessage( Spell.MSG_COLOR_WARNING, Spell.SpellMessages.ONE_RING_PROTECTION_REVEAL );
								toDeal *= ONE_RING_DAMAGE_MULTIPLIER;
							}
							else
							{
								m.RevealingAction();
							}

							Caster.DoHarmful( m );
							SpellHelper.Damage( this, m, toDeal, 0, 0, 0, 0, 100 );

							if ( Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ) > 0 )
							{
								Point3D blast = new Point3D( m.X, m.Y, m.Z + PARTICLE_Z_OFFSET );
								Effects.SendLocationEffect( blast, m.Map, PARTICLE_EFFECT_ID, PARTICLE_EFFECT_DURATION, PARTICLE_EFFECT_SPEED, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 );
								m.PlaySound( SOUND_EFFECT );
							}
							else
							{
								m.BoltEffect( 0 );
							}
						}
					}
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private ChainLightningSpell m_Owner;

			public InternalTarget( ChainLightningSpell owner ) : base( SpellConstants.GetSpellRange(), true, TargetFlags.None )
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

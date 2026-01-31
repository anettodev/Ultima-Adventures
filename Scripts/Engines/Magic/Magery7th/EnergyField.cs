using System;
using System.Collections.Generic;
using Server.Targeting;
using Server.Network;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Spells;

namespace Server.Spells.Seventh
{
	/// <summary>
	/// Energy Field - 7th Circle Magery Spell
	/// Creates a blocking energy field that prevents movement through it
	/// </summary>
	public class EnergyFieldSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Energy Field", "In Sanct Grav",
				221,
				9022,
				false,
				Reagent.BlackPearl,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Seventh; } }

		#region Constants

		/// <summary>Field spread range (minimum)</summary>
		private const int FIELD_SPREAD_MIN = -2;

		/// <summary>Field spread range (maximum)</summary>
		private const int FIELD_SPREAD_MAX = 2;

		/// <summary>Field adjustment range for placement</summary>
		private const int FIELD_ADJUST_RANGE = 12;

		/// <summary>Item ID for east-west oriented field</summary>
		private const int ITEM_ID_EAST_WEST = 0x3946;

		/// <summary>Item ID for north-south oriented field</summary>
		private const int ITEM_ID_NORTH_SOUTH = 0x3956;

		/// <summary>Particle effect ID</summary>
		private const int PARTICLE_EFFECT_ID = 0x376A;

		/// <summary>Particle count</summary>
		private const int PARTICLE_COUNT = 9;

		/// <summary>Particle speed</summary>
		private const int PARTICLE_SPEED = 10;

		/// <summary>Particle duration</summary>
		private const int PARTICLE_DURATION = 5051;

		/// <summary>Sound effect ID</summary>
		private const int SOUND_EFFECT = 0x20B;

		/// <summary>Default deserialization duration fallback (seconds)</summary>
		private const double DESERIALIZE_DURATION_FALLBACK = 5.0;

		#endregion

		#region Easter Egg Constants

		/// <summary>Base chance per Magery skill point (0.2%)</summary>
		private const double EASTER_EGG_BASE_CHANCE = 0.002;

		/// <summary>EvalInt bonus per 10 points (1%)</summary>
		private const double EASTER_EGG_EVALINT_BONUS = 0.01;

		/// <summary>EvalInt points per bonus increment</summary>
		private const int EASTER_EGG_EVALINT_DIVISOR = 10;

		/// <summary>Purple hue for special energy field</summary>
		private const int PURPLE_HUE = 0x10;

		/// <summary>Proximity damage range (1 tile)</summary>
		private const int PROXIMITY_DAMAGE_RANGE = 1;

		/// <summary>Proximity check interval (seconds)</summary>
		private const double PROXIMITY_CHECK_INTERVAL = 3.0;

		#endregion

		public EnergyFieldSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				SpellHelper.Turn( Caster, p );

				SpellHelper.GetSurfaceTop( ref p );

				// Use centralized field orientation calculation (includes self-targeting fix)
				bool eastToWest = FieldSpellHelper.GetFieldOrientation( Caster, p );

				Effects.PlaySound( p, Caster.Map, SOUND_EFFECT );

				int itemID = eastToWest ? ITEM_ID_EAST_WEST : ITEM_ID_NORTH_SOUTH;

				// Use FieldSpellHelper for consistent duration calculation
				// This automatically sends the duration message to the caster
				TimeSpan duration = FieldSpellHelper.GetFieldDuration( Caster );

				// Calculate easter egg chance for special damaging field
				double mageryChance = Caster.Skills[SkillName.Magery].Value * EASTER_EGG_BASE_CHANCE;
				double evalIntBonus = (int)(Caster.Skills[SkillName.EvalInt].Value / EASTER_EGG_EVALINT_DIVISOR) * EASTER_EGG_EVALINT_BONUS;
				double totalChance = mageryChance + evalIntBonus;
				bool isSpecialField = Utility.RandomDouble() < totalChance;

				// Send special message if easter egg triggered
				if ( isSpecialField )
				{
					Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, "Uma aura arcanica envolve seu campo de energia! A forca pulsante agora causa dano aos inimigos que se aproximarem." );
				}

				// Create field items in a line (-2 to +2 range)
				for ( int i = FIELD_SPREAD_MIN; i <= FIELD_SPREAD_MAX; ++i )
				{
					Point3D loc = new Point3D( eastToWest ? p.X + i : p.X, eastToWest ? p.Y : p.Y + i, p.Z );
					bool canFit = SpellHelper.AdjustField( ref loc, Caster.Map, FIELD_ADJUST_RANGE, false );

					if ( !canFit )
						continue;

					Item item;
					if ( isSpecialField )
					{
						item = new SpecialInternalItem( loc, Caster.Map, duration, itemID, Caster );
					}
					else
					{
						item = new InternalItem( loc, Caster.Map, duration, itemID, Caster );
					}
					item.ProcessDelta();

					int effectHue = isSpecialField ? PURPLE_HUE : Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 );
					Effects.SendLocationParticles( EffectItem.Create( loc, Caster.Map, EffectItem.DefaultDuration ), PARTICLE_EFFECT_ID, PARTICLE_COUNT, PARTICLE_SPEED, effectHue, 0, PARTICLE_DURATION, 0 );
				}
			}

			FinishSequence();
		}

		[DispellableField]
		private class InternalItem : Item
		{
			private Timer m_Timer;
			private Mobile m_Caster;

			public override bool BlocksFit{ get{ return true; } }

			public InternalItem( Point3D loc, Map map, TimeSpan duration, int itemID, Mobile caster ) : base( itemID )
			{
				Visible = false;
				Movable = false;
				Light = LightType.Circle300;

				MoveToWorld( loc, map );

				m_Caster = caster;

				if ( caster.InLOS( this ) )
					Visible = true;
				else
					Delete();

				if ( Deleted )
					return;

				m_Timer = new InternalTimer( this, duration );
				m_Timer.Start();
			}

			public InternalItem( Serial serial ) : base( serial )
			{
				m_Timer = new InternalTimer( this, TimeSpan.FromSeconds( DESERIALIZE_DURATION_FALLBACK ) );
				m_Timer.Start();
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 0 ); // version
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}

			public override bool OnMoveOver( Mobile m )
			{
				int noto = Notoriety.Compute( m_Caster, m );
				if ( m_Caster == m || noto == Notoriety.Ally || m.AccessLevel > AccessLevel.Player )
					return true;

				return false;
			}

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				if ( m_Timer != null )
					m_Timer.Stop();
			}

			private class InternalTimer : Timer
			{
				private InternalItem m_Item;

				public InternalTimer( InternalItem item, TimeSpan duration ) : base( duration )
				{
					Priority = TimerPriority.OneSecond;
					m_Item = item;
				}

				protected override void OnTick()
				{
					m_Item.Delete();
				}
			}
		}

		/// <summary>
		/// Special energy field that deals damage to enemies within 1 tile
		/// Easter egg feature with purple hue
		/// </summary>
		[DispellableField]
		private class SpecialInternalItem : Item
		{
			private Timer m_Timer;
			private Timer m_ProximityTimer;
			private Mobile m_Caster;
			private Dictionary<Mobile, DateTime> m_DamagedMobiles;

			public override bool BlocksFit{ get{ return true; } }

			public SpecialInternalItem( Point3D loc, Map map, TimeSpan duration, int itemID, Mobile caster ) : base( itemID )
			{
				Visible = false;
				Movable = false;
				Light = LightType.Circle300;
				Hue = PURPLE_HUE;

				MoveToWorld( loc, map );

				m_Caster = caster;
				m_DamagedMobiles = new Dictionary<Mobile, DateTime>();

				if ( caster.InLOS( this ) )
					Visible = true;
				else
					Delete();

				if ( Deleted )
					return;

				m_Timer = new InternalTimer( this, duration );
				m_Timer.Start();

				m_ProximityTimer = new ProximityDamageTimer( this );
				m_ProximityTimer.Start();
			}

			public SpecialInternalItem( Serial serial ) : base( serial )
			{
				m_Timer = new InternalTimer( this, TimeSpan.FromSeconds( DESERIALIZE_DURATION_FALLBACK ) );
				m_Timer.Start();
				m_DamagedMobiles = new Dictionary<Mobile, DateTime>();
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 0 ); // version
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
				m_DamagedMobiles = new Dictionary<Mobile, DateTime>();
			}

			public override bool OnMoveOver( Mobile m )
			{
				// Check if enemy and apply damage
				CheckAndDamageMobile( m );

				int noto = Notoriety.Compute( m_Caster, m );
				if ( m_Caster == m || noto == Notoriety.Ally || m.AccessLevel > AccessLevel.Player )
					return true;

				return false;
			}

			/// <summary>
			/// Checks if a spell is resisted by the target (using 4th circle calculation)
			/// </summary>
			private bool CheckResistedForCircle( Mobile target, SpellCircle circle )
			{
				// Calculate resist percent (same as Lightning spell - 4th circle)
				double firstPercent = target.Skills[SkillName.MagicResist].Value / 5.0;
				int rInt;
				if ( target.Skills[SkillName.MagicResist].Value >= 100.0 )
				{
					rInt = Utility.RandomMinMax( 1, 5 );
				}
				else
				{
					rInt = Utility.RandomMinMax( 0, 3 );
				}
				double secondPercent = firstPercent + rInt - (int)circle;
				double resistPercent = ( secondPercent > 0 ? secondPercent : 1 ) / 100.0;

				if ( resistPercent <= 0.0 )
					return false;

				if ( resistPercent >= 1.0 )
					return true;

				// Check skill gain
				int maxSkill = ( 1 + (int)circle ) * 10;
				maxSkill += ( 1 + ( (int)circle / 6 ) ) * 25;

				if ( target.Skills[SkillName.MagicResist].Value < maxSkill )
				{
					target.CheckSkill( SkillName.MagicResist, 0.0, target.Skills[SkillName.MagicResist].Cap );
				}

				return ( resistPercent >= Utility.RandomDouble() );
			}

			/// <summary>
			/// Checks if mobile is an enemy and applies damage if within range
			/// </summary>
			private void CheckAndDamageMobile( Mobile m )
			{
				if ( m == null || m.Deleted || m_Caster == null || m_Caster.Deleted )
					return;

				if ( m == m_Caster )
					return;

				int noto = Notoriety.Compute( m_Caster, m );
				if ( noto == Notoriety.Ally || m.AccessLevel > AccessLevel.Player )
					return;

				// Check if within 1 tile range
				if ( !Utility.InRange( m.Location, this.Location, PROXIMITY_DAMAGE_RANGE ) )
					return;

				// Prevent spam damage (only damage once per second per mobile)
				DateTime lastDamage;
				if ( m_DamagedMobiles.TryGetValue( m, out lastDamage ) )
				{
					if ( DateTime.Now - lastDamage < TimeSpan.FromSeconds( 1.0 ) )
						return;
				}

				// Apply damage using Lightning spell damage calculation (4th circle)
				int bonus, dice, sides;
				SpellDamageCalculator.GetCircleDamageParams( SpellCircle.Fourth, out bonus, out dice, out sides );

				// Calculate base damage (same as Lightning spell)
				int realDamage = Utility.Dice( dice, sides, bonus );
				double evalBenefit = NMSUtils.getDamageEvalBenefit( m_Caster );
				double damage = Math.Floor( realDamage * evalBenefit );

				// Apply minimum damage floor based on EvalInt
				int baseMinDamage = bonus;
				int minDamage = SpellDamageCalculator.GetMinimumDamageFloor( m_Caster, baseMinDamage );
				damage = Math.Max( damage, minDamage );

				// Reduce damage by half for field (balance adjustment)
				damage *= 0.5;

				// Check resistance (using 4th circle resistance calculation like Lightning spell)
				bool resisted = CheckResistedForCircle( m, SpellCircle.Fourth );
				if ( resisted )
				{
					damage *= 0.5;
					m.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.RESIST_HALF_DAMAGE_VICTIM );
				}

				// Apply damage (100% energy damage like Lightning)
				// Use TimeSpan.Zero overload to avoid null spell reference
				SpellHelper.Damage( TimeSpan.Zero, m, m_Caster, damage, 0, 0, 0, 0, 100 );

				// Visual and sound effects
				Point3D blast = new Point3D( m.X, m.Y, m.Z + 10 );
				Effects.SendLocationEffect( blast, m.Map, 0x2A4E, 30, 10, PURPLE_HUE, 0 );
				m.PlaySound( 0x029 );

				// Track damage time
				m_DamagedMobiles[m] = DateTime.Now;
			}

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				if ( m_Timer != null )
					m_Timer.Stop();

				if ( m_ProximityTimer != null )
					m_ProximityTimer.Stop();
			}

			private class InternalTimer : Timer
			{
				private SpecialInternalItem m_Item;

				public InternalTimer( SpecialInternalItem item, TimeSpan duration ) : base( duration )
				{
					Priority = TimerPriority.OneSecond;
					m_Item = item;
				}

				protected override void OnTick()
				{
					m_Item.Delete();
				}
			}

			private class ProximityDamageTimer : Timer
			{
				private SpecialInternalItem m_Item;

				public ProximityDamageTimer( SpecialInternalItem item ) : base( TimeSpan.FromSeconds( PROXIMITY_CHECK_INTERVAL ), TimeSpan.FromSeconds( PROXIMITY_CHECK_INTERVAL ) )
				{
					Priority = TimerPriority.OneSecond;
					m_Item = item;
				}

				protected override void OnTick()
				{
					if ( m_Item == null || m_Item.Deleted || m_Item.Map == null )
					{
						Stop();
						return;
					}

					// Check all mobiles within 1 tile range
					IPooledEnumerable eable = m_Item.Map.GetMobilesInRange( m_Item.Location, PROXIMITY_DAMAGE_RANGE );
					foreach ( Mobile m in eable )
					{
						m_Item.CheckAndDamageMobile( m );
					}
					eable.Free();
				}
			}
		}

		public class InternalTarget : Target
		{
			private EnergyFieldSpell m_Owner;

			public InternalTarget( EnergyFieldSpell owner ) : base( SpellConstants.GetSpellRange(), true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IPoint3D )
					m_Owner.Target( (IPoint3D)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}

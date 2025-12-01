// Created by FinalTwist and Gadget2013 
// Refactored to match modern explosion potion mechanics
// Trap grenade that explodes when placed in a player's backpack.
// Will not blow up when placed inside a bag or pouch (safe storage for thieves).
// Great for thieves and stealthers to plant on victims!

using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Spells;

namespace Server.Items
{
	/// <summary>
	/// Trap grenade that auto-activates when placed in a player's backpack
	/// Explodes with area damage after countdown
	/// Safe to carry in bags/pouches for storage
	/// </summary>
	public class PackGrenade : BaseExplosionPotion
	{
		#region Constants

		/// <summary>Minimum damage value</summary>
		private const int MIN_DAMAGE = 20;

		/// <summary>Maximum damage value</summary>
		private const int MAX_DAMAGE = 90;

		/// <summary>Countdown duration before detonation (ticks)</summary>
		private const int COUNTDOWN_TICKS = 4;

		/// <summary>Initial countdown delay (seconds)</summary>
		private const double COUNTDOWN_INITIAL_DELAY = 1.0;

		/// <summary>Countdown tick interval (seconds)</summary>
		private const double COUNTDOWN_TICK_INTERVAL = 1.25;

		/// <summary>Total countdown ticks (ML version)</summary>
		private const int ML_COUNTDOWN_TICKS = 5;

		/// <summary>Inactive hue (safe/unactivated)</summary>
		private const int INACTIVE_HUE = 2110;

		/// <summary>Active hue (armed/countdown started)</summary>
		private const int ACTIVE_HUE = 2160;

		/// <summary>Message color for countdown (red)</summary>
		private const int COUNTDOWN_COLOR = 0x22;

		/// <summary>Message color for activation warning (red)</summary>
		private const int WARNING_COLOR = 0x22;

		/// <summary>Explosion effect ID</summary>
		private const int EXPLOSION_EFFECT_ID = 0x36BD;

		/// <summary>Explosion sound ID</summary>
		private const int EXPLOSION_SOUND_ID = 0x307;

		/// <summary>Activation sound ID</summary>
		private const int ACTIVATION_SOUND_ID = 0x5A;

		#endregion

		#region Properties

		/// <summary>Minimum damage for pack grenade (higher than regular explosion potions)</summary>
		public override int MinDamage { get { return MIN_DAMAGE; } }

		/// <summary>Maximum damage for pack grenade (higher than regular explosion potions)</summary>
		public override int MaxDamage { get { return MAX_DAMAGE; } }

		#endregion

		#region Fields

		private Timer m_exploTimer;
		private bool m_Activated = false;
		private Mobile m_Planter; // Who planted this grenade (for damage attribution)

		#endregion

		#region Constructors

		[Constructable]
		public PackGrenade() : base( PotionEffect.Explosion )
		{
			Hue = INACTIVE_HUE;
			Name = "granada de mochila"; // PT-BR name
		}

		public PackGrenade( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Activation Logic

		/// <summary>
		/// Triggered when grenade is added to a container
		/// Activates countdown if placed directly in player's backpack
		/// Safe if placed in bags/pouches (for storage)
		/// </summary>
		public override void OnAdded( IEntity parent )
		{
			base.OnAdded( parent );

			// Check if placed in a player's inventory
			if ( this.RootParentEntity is Mobile )
			{
				// Safe if inside a bag or pouch (allows thieves to carry them safely)
				if ( this.Parent is Bag || this.Parent is Pouch )
					return;

				// ACTIVATE! Placed directly in player's backpack
				Mobile victim = (Mobile)this.RootParentEntity;

				// Store who planted this (for damage attribution)
				if ( m_Planter == null && victim.LastKiller != null )
					m_Planter = victim.LastKiller;

				// Start countdown timer
				if ( m_exploTimer == null && !m_Activated )
				{
					if ( Core.ML )
						m_exploTimer = Timer.DelayCall( TimeSpan.FromSeconds( COUNTDOWN_INITIAL_DELAY ), TimeSpan.FromSeconds( COUNTDOWN_TICK_INTERVAL ), ML_COUNTDOWN_TICKS, new TimerStateCallback( Detonate_OnTick ), new object[] { victim, COUNTDOWN_TICKS } );
					else
						m_exploTimer = Timer.DelayCall( TimeSpan.FromSeconds( COUNTDOWN_INITIAL_DELAY ), TimeSpan.FromSeconds( COUNTDOWN_TICK_INTERVAL ), ML_COUNTDOWN_TICKS, new TimerStateCallback( Detonate_OnTick ), new object[] { victim, COUNTDOWN_TICKS } );

					// Visual cues
					this.Hue = ACTIVE_HUE; // Change to active red color
					m_Activated = true;

					// Alert victim (PT-BR message)
					if ( victim is PlayerMobile )
					{
						victim.PrivateOverheadMessage( MessageType.Regular, WARNING_COLOR, false, "Algo queima em sua mochila!", victim.NetState );
					}

					// Play activation sound
					victim.PlaySound( ACTIVATION_SOUND_ID ); // Fizzle sound
				}
			}
		}

		/// <summary>
		/// Countdown timer tick - displays countdown and triggers explosion when reaching zero
		/// </summary>
		private void Detonate_OnTick( object state )
		{
			if ( Deleted )
				return;

			object[] states = (object[])state;
			Mobile victim = (Mobile)states[0];
			int timer = (int)states[1];

			object parent = FindParent( victim );

			// Check if still in player's inventory
			if ( parent != null && parent is Mobile )
			{
				if ( timer == 0 )
				{
					// COUNTDOWN REACHED ZERO - EXPLODE!
					PackExplode( victim );
					m_exploTimer = null;
				}
				else
				{
					// Display countdown number
					Mobile mobile = (Mobile)parent;
					mobile.PublicOverheadMessage( MessageType.Regular, COUNTDOWN_COLOR, false, timer.ToString() );

					// Visual countdown effect
					Effects.SendLocationEffect( mobile.Location, mobile.Map, EXPLOSION_EFFECT_ID, 15, 10, COUNTDOWN_COLOR, 0 );

					states[1] = timer - 1;
				}
			}
			else if ( m_Activated )
			{
				// Grenade was moved out of backpack (dropped, moved to bag, etc.)
				// Explode immediately at current location!
				Point3D loc = this.GetWorldLocation();
				Map map = this.Map;

				if ( map != null && map != Map.Internal )
				{
					// Use parent class Explode method for consistent area damage
					Explode( m_Planter ?? victim, true, loc, map );
				}
				else
				{
					// Fallback - just delete
					Effects.PlaySound( loc, map, EXPLOSION_SOUND_ID );
					Effects.SendLocationParticles( this, EXPLOSION_EFFECT_ID, 20, 10, 5044 );
					this.Delete();
				}

				if ( m_exploTimer != null )
				{
					m_exploTimer.Stop();
					m_exploTimer = null;
				}
			}
		}

		/// <summary>
		/// Pack explosion - detonates at victim's location with area damage
		/// Uses parent class Explode() method for consistency
		/// </summary>
		private void PackExplode( Mobile victim )
		{
			if ( victim == null || victim.Deleted || victim.Map == null )
			{
				this.Delete();
				return;
			}

			Point3D loc = victim.Location;
			Map map = victim.Map;

			// Use parent class Explode() method for consistent mechanics:
			// - Area damage (2 tile radius + Chemist bonus)
			// - Alchemy bonus
			// - Troubadour bonus
			// - Multiple target damage reduction
			// - Visible damage numbers via SpellHelper.Damage()
			// - Proper visual/sound effects
			Explode( m_Planter ?? victim, true, loc, map );

			// Cleanup
			if ( m_exploTimer != null )
			{
				m_exploTimer.Stop();
				m_exploTimer = null;
			}
		}

		/// <summary>
		/// Cleanup when grenade is removed from world
		/// </summary>
		public override void OnRemoved( IEntity parent )
		{
			base.OnRemoved( parent );

			// Stop timer if removed
			if ( m_exploTimer != null && m_Activated )
			{
				// If activated and removed, explode at removal location
				if ( parent is Mobile )
				{
					Mobile victim = (Mobile)parent;
					Point3D loc = victim.Location;
					Map map = victim.Map;

					if ( map != null && map != Map.Internal )
					{
						Timer.DelayCall( TimeSpan.FromSeconds( 0.5 ), delegate
						{
							if ( !Deleted )
								Explode( m_Planter ?? victim, true, loc, map );
						} );
					}
				}

				m_exploTimer.Stop();
				m_exploTimer = null;
			}
		}

		/// <summary>
		/// Cleanup on deletion
		/// </summary>
		public override void OnDelete()
		{
			base.OnDelete();

			if ( m_exploTimer != null )
			{
				m_exploTimer.Stop();
				m_exploTimer = null;
			}
		}

		#endregion

		#region Serialization

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version 1

			// Version 1
			writer.Write( m_Activated );
			writer.Write( m_Planter );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Activated = reader.ReadBool();
					m_Planter = reader.ReadMobile();
					break;
				}
				case 0:
				{
					// Old version - no stored data
					break;
				}
			}

			// Don't restart timer on server restart - just delete activated grenades
			if ( m_Activated )
			{
				Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), delegate
				{
					if ( !Deleted )
					{
						// Explode at current location
						Mobile carrier = this.RootParent as Mobile;
						if ( carrier != null )
						{
							Explode( m_Planter ?? carrier, true, carrier.Location, carrier.Map );
						}
						else
						{
							this.Delete();
						}
					}
				} );
			}
		}

		#endregion
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Spells;
using Server.Mobiles;
using Server.Items.Helpers;

namespace Server.Items
{
	/// <summary>
	/// Generic base class for all throwable area-effect potions
	/// Provides countdown mechanics, throw targeting, and area effect framework
	/// Derived classes implement specific effects (damage, paralyze, etc.)
	/// </summary>
	public abstract class BaseThrowablePotion : BasePotion
	{
		#region Constants

		/// <summary>Should explosion trigger chain reactions with nearby potions?</summary>
		protected virtual bool AllowChainReaction { get { return false; } }

		/// <summary>Should potion explode on impact (true) or continue countdown (false)?</summary>
		protected virtual bool InstantDetonation { get { return false; } }

		/// <summary>Is the target location relative for mobiles?</summary>
		protected virtual bool UseRelativeLocation { get { return false; } }

		/// <summary>Blast radius in tiles</summary>
		protected virtual int EffectRadius { get { return 2; } }

		/// <summary>Countdown duration before detonation (in ticks)</summary>
		protected virtual int CountdownTicks { get { return 3; } }

		/// <summary>Initial countdown delay (seconds)</summary>
		protected virtual double CountdownInitialDelay { get { return 1.0; } }

		/// <summary>Countdown tick interval (seconds)</summary>
		protected virtual double CountdownTickInterval { get { return 1.25; } }

		/// <summary>ML countdown tick count (longer fuse)</summary>
		protected virtual int MLCountdownTicks { get { return 5; } }

		/// <summary>Message color for countdown display</summary>
		protected virtual int CountdownMessageColor { get { return 0x22; } } // Red

		/// <summary>Throw cooldown duration to prevent macro exploits (seconds)</summary>
		protected virtual double ThrowCooldownSeconds { get { return 1.0; } }

		/// <summary>Base cooldown between throws (seconds) - can be reduced by Chemist profession</summary>
		protected virtual double BaseCooldownSeconds { get { return 0.0; } } // No base cooldown by default

		#endregion

		#region Fields

		private Timer m_Timer;
		private ArrayList m_Users;

		#endregion

		#region Abstract Properties

		/// <summary>Gets the item ID for the flying potion during throw animation</summary>
		protected abstract int FlyingPotionItemID { get; }

		/// <summary>Gets the flight speed for the throw animation</summary>
		protected virtual int FlyingPotionSpeed { get { return 7; } }

		/// <summary>Gets the throw range in tiles</summary>
		protected virtual int ThrowRange { get { return 12; } }

		/// <summary>Gets the name of this potion type for messages</summary>
		protected abstract string PotionTypeName { get; }

		#endregion

		#region Properties

		/// <summary>Throwable potions don't require a free hand to use</summary>
		public override bool RequireFreeHand { get { return false; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new throwable potion
		/// </summary>
		/// <param name="itemID">The item ID for this potion</param>
		/// <param name="effect">The potion effect type</param>
		public BaseThrowablePotion( int itemID, PotionEffect effect ) : base( itemID, effect )
		{
			m_Users = new ArrayList();
			
			// Throwable potions cannot stack (prevents all from activating when one is thrown)
			this.Stackable = false;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public BaseThrowablePotion( Serial serial ) : base( serial )
		{
			m_Users = new ArrayList();
		}

		#endregion

		#region Serialization

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

		#endregion

		#region Pickup Prevention

		/// <summary>
		/// Prevents picking up a potion that has an active countdown
		/// </summary>
		public override bool OnDragLift( Mobile from )
		{
			if ( m_Timer != null && m_Timer.Running )
			{
				from.SendMessage( 0x22, "Você não pode pegar uma poção com contagem regressiva ativa!" ); // Red
				return false;
			}

			return base.OnDragLift( from );
		}

		#endregion

		#region Core Drink/Throw Logic

		/// <summary>
		/// Activates the throwable potion for targeting
		/// </summary>
		public override void Drink( Mobile from )
		{
			// Validate throw conditions
			if ( !ValidateThrowConditions( from ) )
				return;

			// Check activation cooldown (1 second anti-macro)
			double remainingActivation;
			if ( ThrowablePotionDelayHelper.IsOnActivationCooldown( from, out remainingActivation ) )
			{
				from.SendMessage( CountdownMessageColor, 
					string.Format( "Você deve esperar um pouco antes de jogar outra {0}. ({1:F1}s)", PotionTypeName, remainingActivation ) );
				return;
			}

			// Check main cooldown (if applicable)
			if ( BaseCooldownSeconds > 0 )
			{
				int remainingSeconds;
				if ( ThrowablePotionDelayHelper.IsOnCooldown( from, BaseCooldownSeconds, true, out remainingSeconds ) )
				{
					from.SendMessage( CountdownMessageColor, 
						string.Format( "Você deve esperar {0} segundos antes de usar outra {1}.", remainingSeconds, PotionTypeName ) );
					return;
				}
			}

			// Check if already targeting this potion
			ThrowTarget targ = from.Target as ThrowTarget;
			if ( targ != null && targ.Potion == this )
				return;

			from.RevealingAction();

			// Check for invisibility potion effect (drinking/throwing any potion reveals player)
			if ( !(this is BaseInvisibilityPotion) )
			{
				BaseInvisibilityPotion.CheckRevealOnAction( from, "jogou uma poção" );
			}

		if ( !m_Users.Contains( from ) )
			m_Users.Add( from );

		from.Target = new ThrowTarget( this );

		// Set activation cooldown
		ThrowablePotionDelayHelper.SetActivationCooldown( from );
	}

		/// <summary>
		/// Validates if mobile can throw the potion
		/// </summary>
		protected virtual bool ValidateThrowConditions( Mobile from )
		{
			if ( Core.AOS && (from.Paralyzed || from.Blessed || from.Frozen || (from.Spell != null && from.Spell.IsCasting)) )
			{
				from.SendMessage( "Você não pode fazer isso ainda." );
				return false;
			}

			if ( !from.Region.AllowHarmful( from, from ) )
			{
				from.SendMessage( "Isso não parece uma boa ideia." );
				return false;
			}

			return true;
		}

		/// <summary>
		/// Finds the parent container or mobile holding this potion
		/// </summary>
		public virtual object FindParent( Mobile from )
		{
			Mobile m = this.HeldBy;

			if ( m != null && m.Holding == this )
				return m;

			object obj = this.RootParent;

			if ( obj != null )
				return obj;

			if ( Map == Map.Internal )
				return from;

			return this;
		}

		#endregion

		#region Countdown and Timer Logic

		/// <summary>
		/// Starts the countdown timer for detonation
		/// </summary>
		/// <param name="from">The mobile who activated the potion</param>
		public virtual void StartCountdown( Mobile from )
		{
			if ( m_Timer != null )
			{
				m_Timer.Stop();
				m_Timer = null;
			}

			int ticks = Core.ML ? MLCountdownTicks : CountdownTicks;
			object[] state = new object[] { from, ticks };

			// Make potion immovable during countdown (prevents pickup exploits)
			this.Movable = false;

			// Timer needs to run (ticks + 1) times to display countdown AND detonate
			// Example: ticks = 3 → runs 4 times (3, 2, 1, 0=DETONATE)
			m_Timer = Timer.DelayCall( 
				TimeSpan.FromSeconds( CountdownInitialDelay ), 
				TimeSpan.FromSeconds( CountdownTickInterval ), 
				ticks + 1, 
				new TimerStateCallback( Detonate_OnTick ), 
				state 
			);
		}

		/// <summary>
		/// Countdown timer tick - displays countdown and triggers detonation when reaching zero
		/// </summary>
		private void Detonate_OnTick( object state )
		{
			if ( Deleted )
				return;

			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			int timer = (int)states[1];

			// Get current location (works whether held, in container, or on ground)
			Point3D loc;
			Map map;

			if ( this.Map != Map.Internal )
			{
				loc = this.GetWorldLocation();
				map = this.Map;
			}
			else
			{
				// Find parent if on Internal map
				object parent = FindParent( from );

				if ( parent is Item )
				{
					Item item = (Item)parent;
					loc = item.GetWorldLocation();
					map = item.Map;
				}
				else if ( parent is Mobile )
				{
					Mobile m = (Mobile)parent;
					loc = m.Location;
					map = m.Map;
				}
				else
				{
					return;
				}
			}

			// Countdown reached zero - DETONATE!
			if ( timer == 0 )
			{
				Detonate( from, true, loc, map );
				m_Timer = null;
			}
			else
			{
				// Display countdown at potion's current location
				if ( this.Map != Map.Internal )
				{
					// Visual effect at location
					OnCountdownTick( loc, map, timer );
					
					// Display countdown number
					this.PublicOverheadMessage( MessageType.Regular, CountdownMessageColor, false, timer.ToString() );
				}

				states[1] = timer - 1;
			}
		}

		/// <summary>
		/// Called on each countdown tick - override for custom visual effects
		/// </summary>
		/// <param name="loc">Current location</param>
		/// <param name="map">Current map</param>
		/// <param name="timer">Remaining ticks</param>
		protected virtual void OnCountdownTick( Point3D loc, Map map, int timer )
		{
			// Default: No visual effect (override in derived classes)
		}

		/// <summary>
		/// Handles repositioning the potion after being thrown
		/// </summary>
		private void Reposition_OnTick( object state )
		{
			if ( Deleted )
				return;

			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			IPoint3D p = (IPoint3D)states[1];
			Map map = (Map)states[2];

			Point3D loc = new Point3D( p );

			if ( InstantDetonation )
			{
				// Instant detonation on impact
				Detonate( from, true, loc, map );
			}
		else
		{
			// Grenade-style: Move to location (countdown already running)
			MoveToWorld( loc, map );
			
			// Play landing sound
			OnLanding( loc, map );

			// Countdown is already running (started when target was selected)
			// Explosion will occur at this location when countdown reaches zero
		}
		}

		/// <summary>
		/// Called when potion lands after throw - override for custom sound/effects
		/// </summary>
		protected virtual void OnLanding( Point3D loc, Map map )
		{
			Effects.PlaySound( loc, map, 0x11D ); // Thud sound
		}

		#endregion

		#region Abstract Area Effect Methods

		/// <summary>
		/// Performs the area effect at the specified location
		/// Derived classes implement specific effects (damage, paralyze, ice field, etc.)
		/// </summary>
		/// <param name="from">The mobile who threw the potion</param>
		/// <param name="direct">Whether this is a direct throw (affects bonuses)</param>
		/// <param name="loc">The effect location</param>
		/// <param name="map">The map where effect occurs</param>
		protected abstract void ApplyAreaEffect( Mobile from, bool direct, Point3D loc, Map map );

		/// <summary>
		/// Plays visual and sound effects for the detonation
		/// </summary>
		/// <param name="loc">Effect location</param>
		/// <param name="map">Effect map</param>
		protected abstract void PlayDetonationEffects( Point3D loc, Map map );

		#endregion

		#region Detonation Logic

		/// <summary>
		/// Detonates the potion at the specified location
		/// </summary>
		/// <param name="from">The mobile who threw the potion</param>
		/// <param name="direct">Whether this is a direct throw</param>
		/// <param name="loc">The detonation location</param>
		/// <param name="map">The map where detonation occurs</param>
		public virtual void Detonate( Mobile from, bool direct, Point3D loc, Map map )
		{
			if ( Deleted )
				return;

			Consume();

			// Cancel all active throw targets
			for ( int i = 0; m_Users != null && i < m_Users.Count; ++i )
			{
				Mobile m = (Mobile)m_Users[i];
				ThrowTarget targ = m.Target as ThrowTarget;

				if ( targ != null && targ.Potion == this )
					Target.Cancel( m );
			}

			if ( map == null )
				return;

			// Play detonation effects
			PlayDetonationEffects( loc, map );

			// Apply area effect (damage, paralyze, ice field, etc.)
			ApplyAreaEffect( from, direct, loc, map );
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Gets all mobiles in the effect radius
		/// </summary>
		/// <param name="loc">Center location</param>
		/// <param name="map">Map</param>
		/// <param name="from">Thrower (for Chemist bonus calculation)</param>
		/// <returns>List of mobiles in range</returns>
		protected List<Mobile> GetMobilesInEffectRadius( Point3D loc, Map map, Mobile from )
		{
			// Calculate radius bonus for Chemists
			int bonusRadius = 0;
			if ( from is PlayerMobile && ((PlayerMobile)from).Alchemist() )
			{
				double scalarx = 1.0 + (0.02 * EnhancePotions( from ));
				bonusRadius = Convert.ToInt32( scalarx );
			}

			List<Mobile> mobiles = new List<Mobile>();
			IPooledEnumerable eable = map.GetMobilesInRange( loc, EffectRadius + bonusRadius );

			foreach ( Mobile m in eable )
			{
				mobiles.Add( m );
			}

			eable.Free();
			return mobiles;
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Target handler for throwing potions
		/// </summary>
		private class ThrowTarget : Target
		{
			private BaseThrowablePotion m_Potion;

			public BaseThrowablePotion Potion
			{
				get { return m_Potion; }
			}

			public ThrowTarget( BaseThrowablePotion potion ) : base( potion.ThrowRange, true, TargetFlags.None )
			{
				m_Potion = potion;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Potion.Deleted || m_Potion.Map == Map.Internal )
					return;

				IPoint3D p = targeted as IPoint3D;

				if ( p == null || from.Map == null )
					return;

				// Set cooldown after successful throw
				if ( m_Potion.BaseCooldownSeconds > 0 )
				{
					ThrowablePotionDelayHelper.SetCooldown( from );
				}

				SpellHelper.GetSurfaceTop( ref p );

				from.RevealingAction();

				IEntity to;

				if ( p is Mobile )
					to = (Mobile)p;
				else
					to = new Entity( Serial.Zero, new Point3D( p ), from.Map );

			// Send flying potion animation
			Effects.SendMovingEffect( from, to, m_Potion.FlyingPotionItemID, m_Potion.FlyingPotionSpeed, 0, false, false, m_Potion.Hue, 0 );

			// START COUNTDOWN when target is selected (pulled pin behavior)
			// Potion will begin countdown and fly to target location
			m_Potion.StartCountdown( from );

			// Delay reposition (move potion to target after flight time)
			// Countdown runs during flight, explosion happens at target location
			double flightTime = 1.0; // seconds
			Timer.DelayCall( TimeSpan.FromSeconds( flightTime ), new TimerStateCallback( m_Potion.Reposition_OnTick ), new object[] { from, new Point3D( p ), from.Map } );
		}

		protected override void OnTargetCancel( Mobile from, TargetCancelType cancelType )
		{
			// Predictable behavior: Cancel = Safe (pin not pulled)
			// Player cancelled targeting - nothing happens, potion remains in inventory
			// No countdown, no explosion - perfectly safe to cancel
		}
		}

		#endregion
	}
}


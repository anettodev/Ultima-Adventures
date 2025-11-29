using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Base class for all explosion potions.
	/// Provides area damage with throwable targeting.
	/// </summary>
	public abstract class BaseExplosionPotion : BasePotion
	{
	#region Constants

	/// <summary>Should explosion potions trigger chain explosions with nearby potions?</summary>
	private static bool LeveledExplosion = false;

	/// <summary>Should explosion potions explode on impact (true) or continue countdown (false)?</summary>
	/// <remarks>Set to false for grenade-style behavior where countdown continues after throw</remarks>
	private static bool InstantExplosion = false;

	/// <summary>Is the explosion target location relative for mobiles?</summary>
	private static bool RelativeLocation = false;

	/// <summary>Blast radius in tiles</summary>
	private const int ExplosionRange = 2;

	/// <summary>Countdown duration before detonation (in ticks)</summary>
	private const int COUNTDOWN_TICKS = 3;

	/// <summary>Initial countdown delay (seconds)</summary>
	private const double COUNTDOWN_INITIAL_DELAY = 1.0;

	/// <summary>Countdown tick interval (seconds)</summary>
	private const double COUNTDOWN_TICK_INTERVAL = 1.25;

	/// <summary>ML countdown tick count (longer fuse)</summary>
	private const int ML_COUNTDOWN_TICKS = 5;

	/// <summary>Message color for countdown display (red)</summary>
	private const int COUNTDOWN_MESSAGE_COLOR = 0x22;

	/// <summary>Throw cooldown duration to prevent macro exploits (seconds)</summary>
	private const double THROW_COOLDOWN_SECONDS = 1.0;

	/// <summary>Message color for cooldown warnings (red)</summary>
	private const int MSG_COLOR_COOLDOWN = 0x22;

	#endregion

	#region Static Data

	/// <summary>
	/// Tracks the last time each mobile threw an explosion potion for cooldown enforcement
	/// Prevents macro exploits of rapid-fire potion throwing
	/// </summary>
	private static System.Collections.Generic.Dictionary<Mobile, DateTime> m_LastThrowTime = new System.Collections.Generic.Dictionary<Mobile, DateTime>();

	#endregion

		#region Fields

		private Timer m_Timer;
		private ArrayList m_Users;

		#endregion

		#region Abstract Properties

		/// <summary>Gets the minimum damage for this explosion</summary>
		public abstract int MinDamage { get; }

		/// <summary>Gets the maximum damage for this explosion</summary>
		public abstract int MaxDamage { get; }

		#endregion

		#region Properties

		/// <summary>Explosion potions don't require a free hand to use</summary>
		public override bool RequireFreeHand{ get{ return false; } }

		#endregion


		#region Constructors

		/// <summary>
		/// Initializes a new instance of BaseExplosionPotion
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		public BaseExplosionPotion( PotionEffect effect ) : base( 0xF0D, effect )
		{
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public BaseExplosionPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the explosion potion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the explosion potion
		/// </summary>
		/// <param name="reader">Generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds custom properties to the object property list
		/// </summary>
		/// <param name="list">The object property list</param>
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			// Display potion type in custom cyan color (#8be4fc) with brackets
			string potionName = PotionMetadata.GetKegName( this.PotionEffect );
			if ( potionName != null )
			{
				list.Add( 1070722, string.Format( "<BASEFONT COLOR=#8be4fc>[{0}]", potionName ) ); // Custom cyan color #8be4fc
			}
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Finds the parent container or mobile holding this potion
		/// </summary>
		/// <param name="from">The mobile to default to if no parent found</param>
		/// <returns>The parent object (Mobile or Item)</returns>
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

	/// <summary>
	/// Checks if a mobile is currently on cooldown for throwing explosion potions
	/// </summary>
	/// <param name="from">The mobile to check</param>
	/// <returns>True if on cooldown, false if can throw</returns>
	private static bool IsOnThrowCooldown( Mobile from )
	{
		DateTime lastThrow;

		lock ( m_LastThrowTime )
		{
			if ( m_LastThrowTime.TryGetValue( from, out lastThrow ) )
			{
				TimeSpan elapsed = DateTime.UtcNow - lastThrow;
				return elapsed.TotalSeconds < THROW_COOLDOWN_SECONDS;
			}
		}

		return false;
	}

	/// <summary>
	/// Gets the remaining throw cooldown time for a mobile
	/// </summary>
	/// <param name="from">The mobile to check</param>
	/// <returns>Remaining cooldown duration</returns>
	private static TimeSpan GetRemainingThrowCooldown( Mobile from )
	{
		DateTime lastThrow;

		lock ( m_LastThrowTime )
		{
			if ( m_LastThrowTime.TryGetValue( from, out lastThrow ) )
			{
				TimeSpan elapsed = DateTime.UtcNow - lastThrow;
				double remaining = THROW_COOLDOWN_SECONDS - elapsed.TotalSeconds;

				if ( remaining > 0 )
					return TimeSpan.FromSeconds( remaining );
			}
		}

		return TimeSpan.Zero;
	}

	/// <summary>
	/// Sets the throw cooldown timestamp for a mobile after throwing an explosion potion
	/// </summary>
	/// <param name="from">The mobile that threw a potion</param>
	private static void SetThrowCooldown( Mobile from )
	{
		lock ( m_LastThrowTime )
		{
			m_LastThrowTime[from] = DateTime.UtcNow;
		}
	}

	/// <summary>
	/// Cleans up old throw cooldown entries to prevent memory leaks
	/// Should be called periodically
	/// </summary>
	public static void CleanupThrowCooldowns()
	{
		lock ( m_LastThrowTime )
		{
			System.Collections.Generic.List<Mobile> toRemove = new System.Collections.Generic.List<Mobile>();

			foreach ( System.Collections.Generic.KeyValuePair<Mobile, DateTime> entry in m_LastThrowTime )
			{
				// Remove entries older than 5 seconds (way past cooldown)
				if ( DateTime.UtcNow - entry.Value > TimeSpan.FromSeconds( 5 ) )
				{
					toRemove.Add( entry.Key );
				}
			}

			foreach ( Mobile m in toRemove )
			{
				m_LastThrowTime.Remove( m );
			}
		}
	}

	#endregion

		#region Core Logic

		/// <summary>
		/// Handles drinking (activating) the explosion potion
		/// </summary>
		/// <param name="from">The mobile activating the potion</param>
		public override void Drink( Mobile from )
		{
		// Check if mobile can act
		if ( Core.AOS && (from.Paralyzed || from.Blessed || from.Frozen || (from.Spell != null && from.Spell.IsCasting)) )
		{
			from.SendMessage( "Você não pode fazer isso ainda." );
			return;
		}
		else if ( !from.Region.AllowHarmful( from, from ) )
		{
			from.SendMessage( "Isso não parece uma boa ideia." ); 
			return;
		}

		// Check throw cooldown (1 second between explosion potion throws to prevent macro exploits)
		if ( IsOnThrowCooldown( from ) )
		{
			TimeSpan remaining = GetRemainingThrowCooldown( from );
			from.SendMessage( MSG_COLOR_COOLDOWN, 
				string.Format( "Você deve esperar um pouco antes de jogar outra poção explosiva. ({0:F1}s)", remaining.TotalSeconds ) );
			return;
		}

		ThrowTarget targ = from.Target as ThrowTarget;
			this.Stackable = false; // Scavenged explosion potions won't stack with those ones in backpack, and still will explode.

			if ( targ != null && targ.Potion == this )
				return;

			from.RevealingAction();

			// Track users who activated this potion
			if ( m_Users == null )
				m_Users = new ArrayList();

			if ( !m_Users.Contains( from ) )
				m_Users.Add( from );

		from.Target = new ThrowTarget( this );

		// Start detonation timer (grenade-style countdown)
		if ( m_Timer == null )
		{
			from.SendLocalizedMessage( 500236 ); // You should throw it now!

			// Countdown starts at 3 and continues even after throw
			if( Core.ML )
				m_Timer = Timer.DelayCall( TimeSpan.FromSeconds( COUNTDOWN_INITIAL_DELAY ), TimeSpan.FromSeconds( COUNTDOWN_TICK_INTERVAL ), ML_COUNTDOWN_TICKS, new TimerStateCallback( Detonate_OnTick ), new object[]{ from, COUNTDOWN_TICKS } );
			else
				m_Timer = Timer.DelayCall( TimeSpan.FromSeconds( 0.75 ), TimeSpan.FromSeconds( 1.0 ), 4, new TimerStateCallback( Detonate_OnTick ), new object[]{ from, COUNTDOWN_TICKS } );
		}
	}

	/// <summary>
	/// Handles the detonation timer tick (countdown before explosion)
	/// Countdown continues even after potion is thrown (grenade-style)
	/// </summary>
	/// <param name="state">Timer state containing mobile and countdown value</param>
	private void Detonate_OnTick( object state )
	{
		if ( Deleted )
			return;

		object[] states = (object[])state;
		Mobile from = (Mobile)states[0];
		int timer = (int)states[1];

		// Check if potion still exists and get its current location
		Point3D loc;
		Map map;

		// Potion is on the ground (thrown)
		if ( this.Parent == null && this.Map != Map.Internal )
		{
			loc = this.GetWorldLocation();
			map = this.Map;
		}
		// Potion is in a container or held by mobile
		else
		{
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

		// Countdown reached zero - EXPLODE!
		if ( timer == 0 )
		{
			Explode( from, true, loc, map );
			m_Timer = null;
		}
		else
		{
			// Display countdown at potion's current location
			// This works whether potion is held, in container, or on ground
			if ( this.Map != Map.Internal )
			{
				// Display countdown number at the potion's location
				Effects.SendLocationEffect( loc, map, 0x36BD, 20, 10, COUNTDOWN_MESSAGE_COLOR, 0 );
				this.PublicOverheadMessage( MessageType.Regular, COUNTDOWN_MESSAGE_COLOR, false, timer.ToString() );
			}

			states[1] = timer - 1;
		}
	}

	/// <summary>
	/// Handles repositioning the potion after being thrown
	/// Moves potion to target location - countdown continues until it reaches zero
	/// </summary>
	/// <param name="state">Timer state containing mobile, target point, and map</param>
	private void Reposition_OnTick( object state )
	{
		if ( Deleted )
			return;

		object[] states = (object[])state;
		Mobile from = (Mobile)states[0];
		IPoint3D p = (IPoint3D)states[1];
		Map map = (Map)states[2];

		Point3D loc = new Point3D( p );

		if ( InstantExplosion )
		{
			// Instant explosion on impact (classic behavior)
			Explode( from, true, loc, map );
		}
		else
		{
			// Grenade-style: Move to location and continue countdown
			MoveToWorld( loc, map );
			
			// Play landing sound
			Effects.PlaySound( loc, map, 0x11D ); // Thud sound
		}
	}

	/// <summary>
	/// Performs the explosion at the specified location
	/// Damage is always visible to all affected players (like area damage spells)
	/// </summary>
	/// <param name="from">The mobile who threw the potion</param>
	/// <param name="direct">Whether this is a direct throw (affects alchemy bonus)</param>
	/// <param name="loc">The explosion location</param>
	/// <param name="map">The map where explosion occurs</param>
	public void Explode( Mobile from, bool direct, Point3D loc, Map map )
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

			Effects.PlaySound(loc, map, 0x307);
			Effects.SendLocationEffect(loc, map, 0x3822, 20, 10, 0, 0);
			
			int alchemyBonus = 0;

			if ( direct )
				alchemyBonus = (int)(from.Skills.Alchemy.Value / (Core.AOS ? 5 : 10));

			IPooledEnumerable eable;

			// Explosion Potions have a wider range based on EnhancePotions for Chemists
			int scalar = 0;
			double scalarx = 0;
			if (from is PlayerMobile && ((PlayerMobile)from).Alchemist())
			{
				scalarx = 1.0 + (0.02 * EnhancePotions(from));
				scalar = Convert.ToInt32(scalarx);
			}

			if (LeveledExplosion)
			{
				eable = map.GetObjectsInRange(loc, ExplosionRange + scalar);
			}
			else
			{
				eable = map.GetMobilesInRange(loc, ExplosionRange + scalar);
			}
			
			ArrayList toExplode = new ArrayList();
			int toDamage = 0;

			foreach ( object o in eable )
			{
				if ( o is Mobile )
				{
					toExplode.Add( o );
					++toDamage;
				}
			}

			eable.Free();

			int min = Scale( from, MinDamage );
			int max = Scale( from, MaxDamage );

			// Apply damage to all targets in range
			for ( int i = 0; i < toExplode.Count; ++i )
			{
				object o = toExplode[i];

				if ( o is Mobile )
				{
					Mobile m = (Mobile)o;

					if ( from != null )
						from.DoHarmful( m );

					int damage = Utility.RandomMinMax( min, max );
					
					damage += alchemyBonus;

					// Troubadour bonus against discorded targets
					if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() && SkillHandlers.Discordance.IsDiscorded(m))
						damage += (int)((double)damage * 0.5);

					// Reduce damage if multiple targets
					if ( Core.AOS && toDamage > 2 )
						damage /= toDamage - 1;

					// Apply damage with visible damage numbers (like area spells)
					// Damage type: 0% physical, 100% fire, 0% cold, 0% poison, 0% energy
					SpellHelper.Damage( TimeSpan.Zero, m, from, damage, 0, 100, 0, 0, 0 );

					// Visual feedback for victim (explosion hit effect)
					m.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
					m.PlaySound( 0x207 );
				}
				else if ( o is BaseExplosionPotion )
				{
					// Chain explosion with nearby potions
					BaseExplosionPotion pot = (BaseExplosionPotion)o;
					pot.Explode( from, false, pot.GetWorldLocation(), pot.Map );
				}
			}
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Target handler for throwing explosion potions
		/// </summary>
		private class ThrowTarget : Target
		{
			private BaseExplosionPotion m_Potion;

			/// <summary>Gets the potion being thrown</summary>
			public BaseExplosionPotion Potion
			{
				get{ return m_Potion; }
			}

			/// <summary>
			/// Initializes a new instance of ThrowTarget
			/// </summary>
			/// <param name="potion">The explosion potion to throw</param>
			public ThrowTarget( BaseExplosionPotion potion ) : base( 12, true, TargetFlags.None )
			{
				m_Potion = potion;
			}

			/// <summary>
			/// Handles the target selection for throwing the potion
			/// </summary>
			/// <param name="from">The mobile throwing the potion</param>
			/// <param name="targeted">The targeted location</param>
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Potion.Deleted || m_Potion.Map == Map.Internal )
					return;

				IPoint3D p = targeted as IPoint3D;

				if ( p == null )
					return;

				Map map = from.Map;

				if ( map == null )
					return;

				SpellHelper.GetSurfaceTop( ref p );

				from.RevealingAction();

				IEntity to;

				to = new Entity( Serial.Zero, new Point3D( p ), map );

				// Handle mobile targeting
				if( p is Mobile )
				{
					if( !RelativeLocation ) // explosion location = current mob location. 
						p = ((Mobile)p).Location;
					else
						to = (Mobile)p;
				}

			// Send flying potion animation
			Effects.SendMovingEffect( from, to, m_Potion.ItemID, 7, 0, false, false, m_Potion.Hue, 0 );

			// Handle stacked potions
			if( m_Potion.Amount > 1 )
			{
				Mobile.LiftItemDupe( m_Potion, 1 );
			}

			m_Potion.Internalize();
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( m_Potion.Reposition_OnTick ), new object[]{ from, p, map } );

			// Set throw cooldown to prevent macro exploits
			SetThrowCooldown( from );
		}
	}

		#endregion
	}
}
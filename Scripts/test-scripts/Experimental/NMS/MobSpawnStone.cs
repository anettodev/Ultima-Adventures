using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	/// <summary>
	/// A stone that spawns mobs/creatures at a selected location.
	/// First double-click activates and prompts for location selection.
	/// Spawns 5 mobs per batch (2 easy, 2 medium, 1 hard).
	/// </summary>
	public class MobSpawnStone : Item
	{
		#region Constants

		/// <summary>Item ID for the mob spawn stone appearance.</summary>
		private const int ITEM_ID = 0xED4;

		/// <summary>Red hue color for the mob spawn stone.</summary>
		private const int STONE_HUE = 0x0026; // Red

		/// <summary>Serialization version number.</summary>
		private const int SERIALIZATION_VERSION = 0;

		/// <summary>Maximum distance from stone for spawn location (8 tiles).</summary>
		private const int MAX_SPAWN_DISTANCE = 8;

		/// <summary>Number of mobs spawned per batch.</summary>
		private const int MOBS_PER_BATCH = 5;
		
		/// <summary>Maximum mobs that can be spawned per minute.</summary>
		private const int MAX_MOBS_PER_MINUTE = 5;

		/// <summary>Mob expiration time (10 minutes).</summary>
		private static readonly TimeSpan MOB_EXPIRATION_TIME = TimeSpan.FromMinutes( 10.0 );

		/// <summary>Cooldown period (1 minute).</summary>
		private static readonly TimeSpan COOLDOWN_PERIOD = TimeSpan.FromMinutes( 1.0 );

		#endregion

		#region Properties

		private bool m_Activated;
		private Point3D m_SpawnLocation;
		private Map m_SpawnMap;
		private List<Mobile> m_SpawnedMobs;
		private DateTime m_LastSpawnTime;
		private int m_MobsSpawnedThisMinute;
		private Timer m_ExpirationTimer;

		/// <summary>
		/// Gets or sets whether the stone has been activated (location selected).
		/// </summary>
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Activated
		{
			get { return m_Activated; }
			set { m_Activated = value; InvalidateProperties(); }
		}

		/// <summary>
		/// Gets or sets the spawn location for mobs.
		/// </summary>
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D SpawnLocation
		{
			get { return m_SpawnLocation; }
			set { m_SpawnLocation = value; InvalidateProperties(); }
		}

		/// <summary>
		/// Gets or sets the map for spawning mobs.
		/// </summary>
		[CommandProperty( AccessLevel.GameMaster )]
		public Map SpawnMap
		{
			get { return m_SpawnMap; }
			set { m_SpawnMap = value; InvalidateProperties(); }
		}

		/// <summary>
		/// Gets the default name of the mob spawn stone.
		/// </summary>
		public override string DefaultName
		{
			get
			{
				return "Mob Spawn Stone";
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MobSpawnStone class.
		/// </summary>
		[Constructable]
		public MobSpawnStone()
			: base( ITEM_ID )
		{
			Movable = false;
			Hue = STONE_HUE;
			m_Activated = false;
			m_SpawnLocation = Point3D.Zero;
			m_SpawnMap = null;
			m_SpawnedMobs = new List<Mobile>();
			m_LastSpawnTime = DateTime.MinValue;
			m_MobsSpawnedThisMinute = 0;
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		public MobSpawnStone( Serial serial )
			: base( serial )
		{
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Handles double-click interaction.
		/// First click: Activates and prompts for location selection.
		/// Subsequent clicks: Spawns mobs at the selected location.
		/// </summary>
		public override void OnDoubleClick( Mobile from )
		{
			if ( from == null || from.Deleted )
				return;

			if ( !from.InRange( GetWorldLocation(), 2 ) )
			{
				from.SendLocalizedMessage( 500446 ); // That is too far away.
				return;
			}

			// First double-click: Activate and select location
			if ( !m_Activated )
			{
				from.SendMessage( "Select a location within {0} tiles of this stone where mobs should spawn.", MAX_SPAWN_DISTANCE );
				from.Target = new SpawnLocationTarget( this );
			}
			else
			{
				// Subsequent clicks: Spawn mobs
				TrySpawnMobs( from );
			}
		}

		/// <summary>
		/// Attempts to spawn mobs at the selected location.
		/// </summary>
		private void TrySpawnMobs( Mobile from )
		{
			// Check cooldown and reset counter if needed
			DateTime now = DateTime.UtcNow;
			if ( m_LastSpawnTime != DateTime.MinValue )
			{
				TimeSpan timeSinceLastSpawn = now - m_LastSpawnTime;
				// Reset counter if a full minute has passed
				if ( timeSinceLastSpawn >= TimeSpan.FromMinutes( 1.0 ) )
				{
					m_MobsSpawnedThisMinute = 0;
				}
			}

			// Check if we've reached the limit
			if ( m_MobsSpawnedThisMinute >= MAX_MOBS_PER_MINUTE )
			{
				TimeSpan timeUntilReset = COOLDOWN_PERIOD - ( now - m_LastSpawnTime );
				from.SendMessage( "You can only spawn {0} mobs per minute. Please wait {1} seconds.", MAX_MOBS_PER_MINUTE, (int)timeUntilReset.TotalSeconds );
				return;
			}

			// Remove previous batch
			RemovePreviousBatch();

			// Spawn mobs
			SpawnMobs( from );
		}

		/// <summary>
		/// Spawns mobs at the selected location.
		/// Spawns 2 easy, 2 medium, and 1 hard mob for balanced difficulty.
		/// </summary>
		private void SpawnMobs( Mobile from )
		{
			if ( m_SpawnMap == null || m_SpawnLocation == Point3D.Zero )
			{
				from.SendMessage( "The spawn location is not set. Please activate the stone first." );
				return;
			}

			// Mob types organized by difficulty
			Type[] easyMobs = new Type[]
			{
				typeof( Skeleton ),
				typeof( Zombie )
			};

			Type[] mediumMobs = new Type[]
			{
				typeof( Ratman ),
				typeof( Orc ),
				typeof( Harpy )
			};

			Type[] hardMobs = new Type[]
			{
				typeof( OphidianMage ),
				typeof( Lich ),
				typeof( Troll ),
				typeof( TerathanWarrior ),
				typeof( Gargoyle )
			};

			// Spawn balanced mobs: 2 easy, 2 medium, 1 hard
			int mobsSpawned = 0;

			// Spawn 2 easy mobs
			for ( int i = 0; i < 2; i++ )
			{
				Type mobType = easyMobs[Utility.Random( easyMobs.Length )];
				if ( SpawnSingleMob( mobType ) )
					mobsSpawned++;
			}

			// Spawn 2 medium mobs
			for ( int i = 0; i < 2; i++ )
			{
				Type mobType = mediumMobs[Utility.Random( mediumMobs.Length )];
				if ( SpawnSingleMob( mobType ) )
					mobsSpawned++;
			}

			// Spawn 1 hard mob
			Type hardMobType = hardMobs[Utility.Random( hardMobs.Length )];
			if ( SpawnSingleMob( hardMobType ) )
				mobsSpawned++;

			// 50% chance to spawn an extra hard mob
			if ( Utility.RandomBool() )
			{
				Type extraHardMobType = hardMobs[Utility.Random( hardMobs.Length )];
				if ( SpawnSingleMob( extraHardMobType ) )
					mobsSpawned++;
			}

			m_MobsSpawnedThisMinute += mobsSpawned;
			m_LastSpawnTime = DateTime.UtcNow;

			int hardMobCount = mobsSpawned - 4; // Total mobs minus 2 easy and 2 medium
			string mobCountMessage = hardMobCount == 1 
				? "2 easy, 2 medium, 1 hard" 
				: String.Format( "2 easy, 2 medium, {0} hard", hardMobCount );
			
			from.SendMessage( "Spawned {0} mob(s) at the selected location ({1}). They will expire in {2} minutes.", mobsSpawned, mobCountMessage, (int)MOB_EXPIRATION_TIME.TotalMinutes );
		}

		/// <summary>
		/// Spawns a single mob of the specified type.
		/// </summary>
		/// <param name="mobType">The type of mob to spawn.</param>
		/// <returns>True if the mob was successfully spawned, false otherwise.</returns>
		private bool SpawnSingleMob( Type mobType )
		{
			try
			{
				Mobile mob = Activator.CreateInstance( mobType ) as Mobile;
				if ( mob != null && mob is BaseCreature )
				{
					BaseCreature creature = (BaseCreature)mob;

					// Find valid spawn location near the target point
					Point3D spawnLoc = FindValidSpawnLocation( m_SpawnLocation, m_SpawnMap );

					creature.MoveToWorld( spawnLoc, m_SpawnMap );
					m_SpawnedMobs.Add( creature );

					// Start expiration timer for this mob
					StartExpirationTimer( creature );
					return true;
				}
			}
			catch
			{
				// Failed to create mob
			}
			return false;
		}

		/// <summary>
		/// Finds a valid spawn location near the target point.
		/// </summary>
		private Point3D FindValidSpawnLocation( Point3D center, Map map )
		{
			if ( map == null )
				return center;

			// Try to find a valid spawn location within 2 tiles of center
			for ( int i = 0; i < 10; i++ )
			{
				int x = center.X + Utility.RandomMinMax( -2, 2 );
				int y = center.Y + Utility.RandomMinMax( -2, 2 );
				int z = map.GetAverageZ( x, y );

				Point3D loc = new Point3D( x, y, z );

				if ( map.CanSpawnMobile( x, y, z ) )
				{
					return loc;
				}
			}

			// If no valid location found, return center
			return center;
		}

		/// <summary>
		/// Removes all mobs from the previous batch.
		/// </summary>
		private void RemovePreviousBatch()
		{
			foreach ( Mobile mob in m_SpawnedMobs )
			{
				if ( mob != null && !mob.Deleted )
				{
					mob.Delete();
				}
			}

			m_SpawnedMobs.Clear();

			// Stop expiration timer if running
			if ( m_ExpirationTimer != null )
			{
				m_ExpirationTimer.Stop();
				m_ExpirationTimer = null;
			}
		}

		/// <summary>
		/// Starts the expiration timer for a mob.
		/// </summary>
		private void StartExpirationTimer( BaseCreature mob )
		{
			Timer.DelayCall( MOB_EXPIRATION_TIME, new TimerCallback( delegate()
			{
				if ( mob != null && !mob.Deleted )
				{
					mob.Delete();
					if ( m_SpawnedMobs.Contains( mob ) )
					{
						m_SpawnedMobs.Remove( mob );
					}
				}
			} ) );
		}

		/// <summary>
		/// Sets the spawn location after targeting.
		/// </summary>
		public void SetSpawnLocation( Point3D location, Map map )
		{
			m_SpawnLocation = location;
			m_SpawnMap = map;
			m_Activated = true;
			InvalidateProperties();
		}

		#endregion

		#region Targeting

		/// <summary>
		/// Target for selecting spawn location.
		/// </summary>
		private class SpawnLocationTarget : Target
		{
			private MobSpawnStone m_Stone;

			public SpawnLocationTarget( MobSpawnStone stone )
				: base( -1, true, TargetFlags.None )
			{
				m_Stone = stone;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				IPoint3D p = targeted as IPoint3D;

				if ( p == null )
				{
					from.SendMessage( "Invalid target. Please select a location." );
					return;
				}

				Point3D targetLoc;
				if ( p is Item )
				{
					targetLoc = ((Item)p).GetWorldLocation();
				}
				else if ( p is Mobile )
				{
					targetLoc = ((Mobile)p).Location;
				}
				else
				{
					targetLoc = new Point3D( p );
				}

				// Check distance from stone
				Point3D stoneLoc = m_Stone.GetWorldLocation();
				if ( !Utility.InRange( targetLoc, stoneLoc, MAX_SPAWN_DISTANCE ) )
				{
					int distance = (int)Math.Sqrt( Math.Pow( targetLoc.X - stoneLoc.X, 2 ) + Math.Pow( targetLoc.Y - stoneLoc.Y, 2 ) );
					from.SendMessage( "The spawn location must be within {0} tiles of the stone. Selected location is {1} tiles away.", MAX_SPAWN_DISTANCE, distance );
					return;
				}

				// Set spawn location
				m_Stone.SetSpawnLocation( targetLoc, from.Map );
				from.SendMessage( "Spawn location set! You can now double-click the stone to spawn mobs." );
			}

			protected override void OnTargetCancel( Mobile from, TargetCancelType cancelType )
			{
				from.SendMessage( "Location selection cancelled." );
			}
		}

		#endregion

		#region Serialization

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) SERIALIZATION_VERSION );
			writer.Write( (bool) m_Activated );
			writer.Write( (Point3D) m_SpawnLocation );
			writer.Write( (Map) m_SpawnMap );
			writer.Write( (int) m_SpawnedMobs.Count );
			foreach ( Mobile mob in m_SpawnedMobs )
			{
				writer.Write( (Mobile) mob );
			}
			writer.Write( (DateTime) m_LastSpawnTime );
			writer.Write( (int) m_MobsSpawnedThisMinute );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			if ( version >= 0 )
			{
				m_Activated = reader.ReadBool();
				m_SpawnLocation = reader.ReadPoint3D();
				m_SpawnMap = reader.ReadMap();
				int count = reader.ReadInt();
				m_SpawnedMobs = new List<Mobile>();
				for ( int i = 0; i < count; i++ )
				{
					Mobile mob = reader.ReadMobile();
					if ( mob != null && !mob.Deleted )
					{
						m_SpawnedMobs.Add( mob );
					}
				}
				m_LastSpawnTime = reader.ReadDateTime();
				m_MobsSpawnedThisMinute = reader.ReadInt();

				// Restore hue
				Hue = STONE_HUE;
			}
		}

		#endregion
	}
}


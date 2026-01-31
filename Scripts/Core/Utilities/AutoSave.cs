using System;
using System.IO;
using Server;
using Server.Commands;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Misc
{
	public class AutoSave : Timer
	{
		private static TimeSpan m_Delay = TimeSpan.FromMinutes( MyServerSettings.ServerSaveMinutes() );
		private static TimeSpan m_Warning = TimeSpan.Zero;

		public static void Initialize()
		{
			new AutoSave().Start();

			DecayTimer decayTimer = new DecayTimer();
			decayTimer.Start();
			Console.WriteLine( "DecayTimer: Started - will run every {0} minutes", DecayTimer.GetInterval().TotalMinutes );

			CommandSystem.Register( "SetSaves", AccessLevel.Administrator, new CommandEventHandler( SetSaves_OnCommand ) );
		}

		private static bool m_SavesEnabled = true;

		public static bool SavesEnabled
		{
			get{ return m_SavesEnabled; }
			set{ m_SavesEnabled = value; }
		}

		[Usage( "SetSaves <true | false>" )]
		[Description( "Enables or disables automatic shard saving." )]
		public static void SetSaves_OnCommand( CommandEventArgs e )
		{
			if ( e.Length == 1 )
			{
				m_SavesEnabled = e.GetBoolean( 0 );
				e.Mobile.SendMessage( "Saves have been {0}.", m_SavesEnabled ? "enabled" : "disabled" );
			}
			else
			{
				e.Mobile.SendMessage( "Format: SetSaves <true | false>" );
			}
		}

		public AutoSave() : base( m_Delay - m_Warning, m_Delay )
		{
			Priority = TimerPriority.OneMinute;
		}

		protected override void OnTick()
		{
			if ( !m_SavesEnabled || AutoRestart.Restarting )
				return;

			if ( m_Warning == TimeSpan.Zero )
			{
				// CLEAN UP ANY GRAVE DISTURBED MONSTERS OR TREASURE MAP MONSTERS
				ArrayList monsters = new ArrayList();
				foreach ( Mobile m in World.Mobiles.Values )
				{
					if ( m is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)m;
						if ( ( bc.ControlSlots == 666 ) && ( m.Combatant == null ) )
							monsters.Add( bc );
					}
				}
				for ( int i = 0; i < monsters.Count; ++i )
				{
					Mobile ridof = ( Mobile )monsters[ i ];
					Effects.SendLocationParticles( EffectItem.Create( ridof.Location, ridof.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
					Effects.PlaySound( ridof, ridof.Map, 0x201 );
					ridof.Delete();
				}

				Save( true );
			}
			else
			{
				int s = (int)m_Warning.TotalSeconds;
				int m = s / 60;
				s %= 60;

				if ( m > 0 && s > 0 )
					World.Broadcast( 0x35, true, "O mundo será salvo em {0} minuto{1} e {2} segundo{3}.", m, m != 1 ? "s" : "", s, s != 1 ? "s" : "" );
				else if ( m > 0 )
					World.Broadcast( 0x35, true, "O mundo será salvo em {0} minuto{1}.", m, m != 1 ? "s" : "" );
				else
					World.Broadcast( 0x35, true, "O mundo será salvo em {0} segundo{1}.", s, s != 1 ? "s" : "" );

				Timer.DelayCall( m_Warning, new TimerCallback( Save ) );
			}
		}

		public static void Save()
		{
			AutoSave.Save( false );
		}

		public static void Save( bool permitBackgroundWrite )
		{
			if ( AutoRestart.Restarting )
				return;

			World.WaitForWriteCompletion();

			try{ Backup(); }
			catch ( Exception e ) { Console.WriteLine("WARNING: Automatic backup FAILED: {0}", e); }

			World.Save( true, permitBackgroundWrite );
		}

		private static string[] m_Backups = new string[]
			{
				"Sixth Backup",
				"Fifth Backup",
				"Fourth Backup",
				"Third Backup",
				"Second Backup",
				"Most Recent"
			};

		private static void Backup()
		{
			if ( m_Backups.Length == 0 )
				return;

			string root = Path.Combine( Core.BaseDirectory, "Backups/Automatic" );

			if ( !Directory.Exists( root ) )
				Directory.CreateDirectory( root );

			string[] existing = Directory.GetDirectories( root );

			for ( int i = 0; i < m_Backups.Length; ++i )
			{
				DirectoryInfo dir = Match( existing, m_Backups[i] );

				if ( dir == null )
					continue;

				if ( i > 0 )
				{
					string timeStamp = FindTimeStamp( dir.Name );

					if ( timeStamp != null )
					{
						try{ dir.MoveTo( FormatDirectory( root, m_Backups[i - 1], timeStamp ) ); }
						catch{}
					}
				}
				else
				{
					try{ dir.Delete( true ); }
					catch{}
				}
			}

			string saves = Path.Combine( Core.BaseDirectory, "Saves" );

			if ( Directory.Exists( saves ) )
				Directory.Move( saves, FormatDirectory( root, m_Backups[m_Backups.Length - 1], GetTimeStamp() ) );
		}

		private static DirectoryInfo Match( string[] paths, string match )
		{
			for ( int i = 0; i < paths.Length; ++i )
			{
				DirectoryInfo info = new DirectoryInfo( paths[i] );

				if ( info.Name.StartsWith( match ) )
					return info;
			}

			return null;
		}

		private static string FormatDirectory( string root, string name, string timeStamp )
		{
			return Path.Combine( root, String.Format( "{0} ({1})", name, timeStamp ) );
		}

		private static string FindTimeStamp( string input )
		{
			int start = input.IndexOf( '(' );

			if ( start >= 0 )
			{
				int end = input.IndexOf( ')', ++start );

				if ( end >= start )
					return input.Substring( start, end-start );
			}

			return null;
		}

		private static string GetTimeStamp()
		{
			DateTime now = DateTime.UtcNow;

			return String.Format( "{0}-{1}-{2} {3}-{4:D2}-{5:D2}",
					now.Day,
					now.Month,
					now.Year,
					now.Hour,
					now.Minute,
					now.Second
				);
		}
	}

	// Independent decay timer - runs every 5 minutes to remove expired items
	// This improves world save performance by reducing item count before saves
	public class DecayTimer : Timer
	{
		private static TimeSpan m_Interval = TimeSpan.FromMinutes( 5.0 );
		private static int m_RunCount = 0;

		public static TimeSpan GetInterval() { return m_Interval; }

		public DecayTimer() : base( m_Interval, m_Interval )
		{
			Priority = TimerPriority.FiveSeconds;
		}

		protected override void OnTick()
		{
			m_RunCount++;
			Console.WriteLine( "DecayTimer: OnTick() called (run #{0})", m_RunCount );

			try
			{
				ProcessItemDecay();
			}
			catch ( Exception ex )
			{
				Console.WriteLine( "DecayTimer: ERROR - {0}", ex.Message );
				Console.WriteLine( ex.StackTrace );
			}
		}

		private static void ProcessItemDecay()
		{
			Console.WriteLine( "DecayTimer: Processing item decay..." );

			DateTime now = DateTime.UtcNow;
			List<Item> toDecay = new List<Item>();
			int totalItems = World.Items.Count;
			int decayableItems = 0;

			// Find all items that should decay
			foreach ( Item item in World.Items.Values )
			{
				if ( item.Decays )
				{
					decayableItems++;

					if ( item.Parent == null && item.Map != Map.Internal &&
					     (item.LastMoved + item.DecayTime) <= now )
					{
						toDecay.Add( item );
					}
				}
			}

			Console.WriteLine( "DecayTimer: Total items: {0}, Decayable: {1}, Expired: {2}",
				totalItems, decayableItems, toDecay.Count );

			// Delete expired items
			int decayedCount = 0;
			foreach ( Item item in toDecay )
			{
				if ( item.OnDecay() )
				{
					item.Delete();
					decayedCount++;
				}
			}

			if ( decayedCount > 0 )
			{
				Console.WriteLine( "DecayTimer: Successfully removed {0} expired item{1}",
					decayedCount, decayedCount != 1 ? "s" : "" );
			}
			else
			{
				Console.WriteLine( "DecayTimer: No items removed (all items still valid)" );
			}
		}
	}
}
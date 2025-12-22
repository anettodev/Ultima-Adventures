using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using System.IO;

namespace Server.Misc
{
	/// <summary>
	/// Centralized logging system for game events.
	/// Handles logging of battles, deaths, quests, adventures, and misc events.
	/// Provides methods for town crier announcements and event tracking.
	/// </summary>
    class LoggingFunctions
    {
		#region Utility Methods

		/// <summary>
		/// Determines if logging events is enabled.
		/// </summary>
		/// <returns>True if logging is enabled, false otherwise</returns>
		public static bool LoggingEvents()
		{
			return true; // SET TO TRUE TO ENABLE LOG SYSTEM FOR GAME EVENTS AND TOWN CRIERS
		}

		/// <summary>
		/// Creates a file if it does not exist.
		/// </summary>
		/// <param name="sPath">The file path to create</param>
		public static void CreateFile(string sPath)
		{
			StreamWriter w = null; 
			try
			{
				using (w = File.AppendText( sPath ) ){}
			}
			catch(Exception)
			{
			}
			finally
			{
				if (w != null)
					w.Dispose();
			}
		}

		/// <summary>
		/// Updates a log file by prepending a new header and trimming old entries.
		/// </summary>
		/// <param name="filename">The file to update</param>
		/// <param name="header">The new header line to prepend</param>
		public static void UpdateFile(string filename, string header)
		{
			int nLine = 0;
			int nTrim = LoggingConstants.FILE_TRIM_LIMIT;
			string tempfile = Path.GetTempFileName();
			StreamWriter writer = null;
			StreamReader reader = null;
			using (writer = new StreamWriter(tempfile))
			using (reader = new StreamReader(filename))
			{
				writer.WriteLine(header);
				while (!reader.EndOfStream)
				{
					nLine = nLine + 1;
					if ( nLine < nTrim )
					{
						writer.WriteLine(reader.ReadLine());
					}
					else
					{
						reader.ReadLine();
					}
				}
			}

			if (writer != null)
				writer.Dispose();

			if (reader != null)
				reader.Dispose();

			File.Copy(tempfile, filename, true);
			File.Delete(tempfile);
		}

		/// <summary>
		/// Deletes a log file.
		/// </summary>
		/// <param name="filename">The file to delete</param>
		public static void DeleteFile(string filename)
		{
			try
			{
				File.Delete(filename);
			}
			catch(Exception)
			{
			}
		}

		/// <summary>
		/// Gets the total number of lines in a file.
		/// </summary>
		/// <param name="filePath">The file path to count lines in</param>
		/// <returns>The number of lines in the file</returns>
		public static int TotalLines(string filePath)
		{
			int i = 0;
			using (StreamReader r = new StreamReader(filePath)){ while (r.ReadLine() != null) { i++; } }
			return i;
		}

		/// <summary>
		/// Gets a timestamp string for logging.
		/// </summary>
		/// <param name="value">The DateTime to format</param>
		/// <returns>Formatted timestamp string</returns>
		public static String GetTimestamp(DateTime value)
		{
			return value.ToString("yyyy-MM-dd HH:mm:ss");
		}

		/// <summary>
		/// Logs a server event (currently disabled).
		/// </summary>
		/// <param name="sText">The text to log</param>
		/// <returns>Always returns null</returns>
		public static string LogServer ( string sText )
		{
			//String timeStamp = GetTimestamp(DateTime.UtcNow);
			//string sEvent = sText + "#" + timeStamp;
			//LoggingFunctions.LogEvent( sEvent, "Logging Server" );

			return null;
		}

		#endregion

		#region Core Logging Methods

		/// <summary>
		/// Logs an event to the appropriate log file.
		/// </summary>
		/// <param name="sEvent">The event string to log (format: "Message#Date")</param>
		/// <param name="sLog">The log type identifier</param>
		/// <returns>Always returns null</returns>
		public static string LogEvent( string sEvent, string sLog )
		{
			if ( LoggingFunctions.LoggingEvents() == true )
			{
				if ( sLog != LoggingConstants.LOG_TYPE_SERVER )
				{
					LoggingFunctions.LogServer( "Start - " + sLog );
				}
				
				if ( !Directory.Exists( LoggingConstants.INFO_DIRECTORY ) )
					Directory.CreateDirectory( LoggingConstants.INFO_DIRECTORY );

				string sPath = Server.Misc.Helpers.LoggingPathHelper.GetFilePath( sLog );
				
				
				CreateFile( sPath );

				/// PREPEND THE FILE WITH THE EVENT ///
				try
				{
					UpdateFile(sPath, sEvent);
				}
				catch(Exception)
				{
				}
				
				if ( sLog != LoggingConstants.LOG_TYPE_SERVER )
				{
					LoggingFunctions.LogServer( "Done - " + sLog );
				}
			}
			return null;
		}

		/// <summary>
		/// Reads log entries from a log file and formats them for display.
		/// </summary>
		/// <param name="sLog">The log type identifier</param>
		/// <param name="m">The mobile requesting the log</param>
		/// <returns>HTML formatted log entries</returns>
		public static string LogRead( string sLog, Mobile m )
		{
			if ( !Directory.Exists( LoggingConstants.INFO_DIRECTORY ) )
				Directory.CreateDirectory( LoggingConstants.INFO_DIRECTORY );

			string sPath = Server.Misc.Helpers.LoggingPathHelper.GetFilePath( sLog );

			string sBreak = "";

			if ( sLog == LoggingConstants.LOG_TYPE_MURDERERS){ sBreak = LoggingConstants.HTML_BREAK_TAG; }
			string sLogEntries = LoggingConstants.HTML_BASE_FONT_START;

			CreateFile( sPath );

			string eachLine = "";
			int nLine = 0;
			int nBlank = 1;
			StreamReader reader = null;

			try
			{
				using (reader = new StreamReader( sPath ))
				{
					while (!reader.EndOfStream)
					{
						eachLine = reader.ReadLine();
						string[] eachWord = eachLine.Split('#');
						nLine = 1;
						foreach (string eachWords in eachWord)
						{
							if ( nLine == 1 ){ nLine = 2; sLogEntries = sLogEntries + eachWords + "." + LoggingConstants.HTML_BREAK_TAG + sBreak; nBlank = 0; }
							else { nLine = 1; sLogEntries = sLogEntries + " - " + eachWords + LoggingConstants.HTML_BREAK_TAG + LoggingConstants.HTML_BREAK_TAG; }
						}
					}
				}
			}
			catch(Exception)
			{
				sLogEntries = sLogEntries + string.Format(LoggingStringConstants.MSG_BUSY_FORMAT, m.Name);
			}
			finally
			{
				if (reader != null)
					reader.Dispose();
			}

			if ( nBlank == 1 )
			{
				sLogEntries = sLogEntries + Server.Misc.Helpers.LoggingStringHelper.GetNoEntriesMessage( sLog, m.Name );
			}

			sLogEntries = sLogEntries + LoggingConstants.HTML_BASE_FONT_END;
			if ( sLogEntries.Contains(" .") ){ sLogEntries = sLogEntries.Replace(" .", "."); }
			if ( sLogEntries.Contains("..") ){ sLogEntries = sLogEntries.Replace("..", "."); }

			return sLogEntries;
		}

		/// <summary>
		/// Reads an article from the articles directory.
		/// </summary>
		/// <param name="article">The article number (0-10)</param>
		/// <param name="section">The section to read (1=title, 2=date, 3=message)</param>
		/// <returns>The requested article section</returns>
		public static string LogArticles( int article, int section )
		{
			if ( !Directory.Exists( LoggingConstants.ARTICLES_DIRECTORY ) )
				Directory.CreateDirectory( LoggingConstants.ARTICLES_DIRECTORY );

			if ( article > 10 ){ article = 0; }
			else if ( article > 0 ){}
			else { article = 0; }

			string text = article.ToString();

			string path = LoggingConstants.ARTICLES_DIRECTORY + "/" + text + ".txt";

			string part = "";

			string title = "";
			string date = "";
			string message = "";

			CreateFile( path );

			StreamReader reader = null;

			int line = 0;

			try
			{
				using (reader = new StreamReader( path ))
				{
					while (!reader.EndOfStream)
					{
						if ( line == 0 ){ title = reader.ReadLine(); }
						else if ( line == 1 ){ date = reader.ReadLine(); }
						else { message = reader.ReadLine(); }

						line++;
					}
				}
			}
			catch(Exception)
			{
			}
			finally
			{
				if (reader != null)
					reader.Dispose();
			}

			if ( section == 1 ){ part = title; }
			else if ( section == 2 ){ part = date; }
			else if ( section == 3 ){ part = message; }


			if ( part.Contains(" .") ){ part = part.Replace(" .", "."); }

			return part;
		}


		public static string LogShout()
		{
			LoggingFunctions.LogServer( "Start - Town Crier" );

			if ( !Directory.Exists( LoggingConstants.INFO_DIRECTORY ) )
				Directory.CreateDirectory( LoggingConstants.INFO_DIRECTORY );

			string sLog = LoggingConstants.LOG_TYPE_ADVENTURES;
			switch ( Utility.Random( LoggingConstants.LOG_TYPE_COUNT ))
			{
				case 0: sLog = LoggingConstants.LOG_TYPE_DEATHS; break;
				case 1: sLog = LoggingConstants.LOG_TYPE_QUESTS; break;
				case 2: sLog = LoggingConstants.LOG_TYPE_BATTLES; break;
				case 3: sLog = LoggingConstants.LOG_TYPE_JOURNIES; break;
				case 4: sLog = LoggingConstants.LOG_TYPE_MURDERERS; break;
				case 5: sLog = LoggingConstants.LOG_TYPE_ADVENTURES; break;
				case 6: sLog = LoggingConstants.LOG_TYPE_MISC; break;
			};

			string sPath = Server.Misc.Helpers.LoggingPathHelper.GetFilePath( sLog );

			CreateFile( sPath );

			int lineCount = 1;
			string sGreet = LoggingStringConstants.MSG_GREET_0;
				switch ( Utility.Random( LoggingConstants.GREETING_VARIANT_COUNT ))
				{
					case 0: sGreet = LoggingStringConstants.MSG_GREET_0; break;
					case 1: sGreet = LoggingStringConstants.MSG_GREET_1; break;
					case 2: sGreet = LoggingStringConstants.MSG_GREET_2; break;
					case 3: sGreet = LoggingStringConstants.MSG_GREET_3; break;
				};

			string myShout = "";
			if ( sLog == LoggingConstants.LOG_TYPE_MURDERERS ){ myShout = LoggingStringConstants.MSG_NO_MURDERS; }
			else
			{
				switch ( Utility.Random( LoggingConstants.NO_NEWS_MESSAGE_COUNT ))
				{
					case 0: myShout = LoggingStringConstants.MSG_NO_NEWS_0; break;
					case 1: myShout = LoggingStringConstants.MSG_NO_NEWS_1; break;
					case 2: myShout = LoggingStringConstants.MSG_NO_NEWS_2; break;
					case 3: myShout = LoggingStringConstants.MSG_NO_NEWS_3; break;
				};
			}

			try
			{
				lineCount = TotalLines( sPath );
			}
			catch(Exception)
			{
			}

			lineCount = Utility.RandomMinMax( 1, lineCount );
			string readLine = "";
			StreamReader reader = null;
			int nWhichLine = 0;
			int nLine = 1;
			try
			{
				using (reader = new StreamReader( sPath ))
				{
					string line;

					while ((line = reader.ReadLine()) != null)
					{
						nWhichLine = nWhichLine + 1;
						if ( nWhichLine == lineCount )
						{
							readLine = line;
							string[] shoutOut = readLine.Split('#');
							foreach (string shoutOuts in shoutOut)
							{
								if ( nLine == 1 ){ nLine = 2; readLine = shoutOuts; }
							}
						}
					}
					if ( readLine != "" ){ myShout = readLine; }
				}
			}
			catch(Exception)
			{
			}
			finally
			{
				if (reader != null)
					reader.Dispose();
			}

			myShout = sGreet + " " + myShout + "!";
			if ( myShout.Contains(" !") ){ myShout = myShout.Replace(" !", "!"); }
			myShout = Server.Misc.Helpers.LoggingVerbHelper.ApplyLogShoutReplacements( myShout );

			LoggingFunctions.LogServer( "Done - Town Crier" );
						
			return myShout;
		}

		/// <summary>
		/// Generates tavern chatter by reading a random event from log files.
		/// </summary>
		/// <returns>A formatted chatter string with verb replacements</returns>
		public static string LogSpeak()
		{
			LoggingFunctions.LogServer( "Start - Tavern Chatter" );

			if ( !Directory.Exists( LoggingConstants.INFO_DIRECTORY ) )
				Directory.CreateDirectory( LoggingConstants.INFO_DIRECTORY );


			string sLog = LoggingConstants.LOG_TYPE_MURDERERS;
			switch ( Utility.Random( LoggingConstants.LOG_SPEAK_TYPE_COUNT ))
			{
				case 0: sLog = LoggingConstants.LOG_TYPE_DEATHS; break;
				case 1: sLog = LoggingConstants.LOG_TYPE_BATTLES; break;
				case 2: sLog = LoggingConstants.LOG_TYPE_JOURNIES; break;
				case 3: sLog = LoggingConstants.LOG_TYPE_BATTLES; break;
				case 4: sLog = LoggingConstants.LOG_TYPE_JOURNIES; break;
			};


			string sPath = Server.Misc.Helpers.LoggingPathHelper.GetFilePath( sLog );

			CreateFile( sPath );

			int lineCount = 1;

			string mySpeaking = LoggingStringConstants.MSG_DEFAULT_NOTHING_INTEREST;

			try
			{
				lineCount = TotalLines( sPath );
			}
			catch(Exception)
			{
			}

			lineCount = Utility.RandomMinMax( 1, lineCount );
			string readLine = "";
			StreamReader reader = null;
			int nWhichLine = 0;
			int nLine = 1;
			try
			{
				using (reader = new StreamReader( sPath ))
				{
					string line;

					while ((line = reader.ReadLine()) != null)
					{
						nWhichLine = nWhichLine + 1;
						if ( nWhichLine == lineCount )
						{
							readLine = line;
							string[] shoutOut = readLine.Split('#');
							foreach (string shoutOuts in shoutOut)
							{
								if ( nLine == 1 ){ nLine = 2; readLine = shoutOuts; }
							}
						}
					}
					if ( readLine != "" ){ mySpeaking = readLine; }
				}
			}
			catch(Exception)
			{
			}
			finally
			{
				if (reader != null)
					reader.Dispose();
			}

			mySpeaking = Server.Misc.Helpers.LoggingVerbHelper.ApplyLogSpeakReplacements( mySpeaking );

			LoggingFunctions.LogServer( "Done - Tavern Chatter" );
						
			return mySpeaking;
		}

		/// <summary>
		/// Generates quest-related chatter by reading from quest log file.
		/// </summary>
		/// <returns>A quest-related message string</returns>
		public static string LogSpeakQuest()
		{
			if ( !Directory.Exists( LoggingConstants.INFO_DIRECTORY ) )
				Directory.CreateDirectory( LoggingConstants.INFO_DIRECTORY );

			string sLog = LoggingConstants.LOG_TYPE_QUESTS;
			string sPath = Server.Misc.Helpers.LoggingPathHelper.GetFilePath( sLog );

			CreateFile( sPath );

			int lineCount = 1;

			string mySpeaking = LoggingStringConstants.MSG_DEFAULT_ADVENTURERS_TAVERNS;

			try
			{
				lineCount = TotalLines( sPath );
			}
			catch(Exception)
			{
			}

			lineCount = Utility.RandomMinMax( 1, lineCount );
			string readLine = "";
			StreamReader reader = null;
			int nWhichLine = 0;
			int nLine = 1;
			try
			{
				using (reader = new StreamReader( sPath ))
				{
					string line;

					while ((line = reader.ReadLine()) != null)
					{
						nWhichLine = nWhichLine + 1;
						if ( nWhichLine == lineCount )
						{
							readLine = line;
							string[] shoutOut = readLine.Split('#');
							foreach (string shoutOuts in shoutOut)
							{
								if ( nLine == 1 ){ nLine = 2; readLine = shoutOuts; }
							}
						}
					}
					if ( readLine != "" ){ mySpeaking = readLine; }
				}
			}
			catch(Exception)
			{
			}
			finally
			{
				if (reader != null)
					reader.Dispose();
			}

			return mySpeaking;
		}

		#endregion

		#region Announcement Methods

		/// <summary>
		/// Logs when a player enters or leaves a region.
		/// </summary>
		/// <param name="m">The mobile entering/leaving the region</param>
		/// <param name="sRegion">The region name</param>
		/// <param name="sDirection">"enter" or "leave"</param>
		/// <returns>Always returns null</returns>
		public static string LogRegions( Mobile m, string sRegion, string sDirection )
		{
			if ( m is PlayerMobile )
			{
				int nDifficulty = MyServerSettings.GetDifficultyLevel( m.Location, m.Map );
				string sDifficulty = "";

				if ( nDifficulty == -1 ){ sDifficulty = " * (Easy)"; }
				else if ( nDifficulty == 0 ){ sDifficulty = " * (Normal)"; }
				else if ( nDifficulty == 1 ){ sDifficulty = " * (Difficult)"; }
				else if ( nDifficulty == 2 ){ sDifficulty = " * (Challenging)"; }
				else if ( nDifficulty == 3 ){ sDifficulty = " * (Hard)"; }
				else if ( nDifficulty == 4 ){ sDifficulty = " * (Deadly)"; }

				if ( sDirection == "enter" )
				{
					m.SendMessage(55, "Voc� entrou em " + sRegion + sDifficulty + "."); 
					//((PlayerMobile)m).lastdeeds = " entered " + sRegion + sDifficulty + "."; 
				}
				else { m.SendMessage(55, "Voc� saiu do(a) " + sRegion + "."); }
			}

			if ( ( m is PlayerMobile ) && ( m.AccessLevel < AccessLevel.GameMaster ) )
			{
				if ( !m.Alive && m.QuestArrow == null ){ GhostHelper.OnGhostWalking( m ); }
				string sDateString = GetPlayerInfo.GetTodaysDate();
				string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
				if ( m.Title != null ){ sTitle = m.Title; }

				PlayerMobile pm = (PlayerMobile)m;
				////if (pm.PublicMyRunUO == true)
				//{
					string sEvent;

					if ( sDirection == "enter" ){ sEvent = m.Name + " " + sTitle + " " + LoggingStringConstants.ACTION_ENTERED_REALM + " " + sRegion + "#" + sDateString; LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_JOURNIES ); }
					// else { sEvent = m.Name + " " + sTitle + " left " + sRegion + "#" + sDateString; LoggingFunctions.LogEvent( sEvent, "Logging Journies" ); }
				//}
			}
			return null;
		}

		/// <summary>
		/// Logs when a player slays a mobile in battle.
		/// </summary>
		/// <param name="m">The player who did the slaying</param>
		/// <param name="mob">The mobile that was slain</param>
		/// <returns>Always returns null</returns>
		public static string LogBattles( Mobile m, Mobile mob )
		{
			if (m == null || mob == null || mob.Blessed )
				return null;

			string sDateString = GetPlayerInfo.GetTodaysDate();

			if ( m is PlayerMobile && mob != null )
			{
				string sTitle = Server.Misc.Helpers.LoggingStringHelper.GetPlayerTitle( m );

				PlayerMobile pm = (PlayerMobile)m;

				string sKiller = Server.Misc.Helpers.LoggingStringHelper.ExtractNameWithoutBrackets( mob.Name );

				//if ( pm.PublicMyRunUO == true )
				//{
					string Killed = sKiller;
						if ( mob.Title != "" && mob.Title != null ){ Killed = Killed + " " + mob.Title; }
					string sEvent = m.Name + " " + sTitle + " " + LoggingStringConstants.ACTION_HAD_SLAIN + " " + Killed + "#" + sDateString;
					((PlayerMobile)m).lastdeeds = "killed " + Killed;
					LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_BATTLES );
				//}
				/*else
				{
					string privateEnemy = "an opponent";
					switch ( Utility.Random( 6 ) )
					{
						case 0: privateEnemy = "an opponent"; break;
						case 1: privateEnemy = "an enemy"; break;
						case 2: privateEnemy = "another"; break;
						case 3: privateEnemy = "an adversary"; break;
						case 4: privateEnemy = "a foe"; break;
						case 5: privateEnemy = "a rival"; break;
					}
					string sEvent = m.Name + " " + sTitle + " had slain " + privateEnemy + "#" + sDateString;
					LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_BATTLES );
				}*/
			}
			return null;
		}

		/// <summary>
		/// Logs when a player triggers a trap.
		/// </summary>
		/// <param name="m">The player who triggered the trap</param>
		/// <param name="sTrap">The trap name/description</param>
		/// <returns>Always returns null</returns>
		public static string LogTraps( Mobile m, string sTrap )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			string sTrip = LoggingStringConstants.TRAP_ACTION_TRIGGERED;
			switch( Utility.Random( LoggingConstants.TRAP_VERB_COUNT ) )
			{
				case 0: sTrip = LoggingStringConstants.TRAP_ACTION_TRIGGERED;	break;
				case 1: sTrip = LoggingStringConstants.TRAP_ACTION_SET_OFF;	break;
				case 2: sTrip = LoggingStringConstants.TRAP_ACTION_WALKED_INTO;	break;
				case 3: sTrip = LoggingStringConstants.TRAP_ACTION_STUMBLED_INTO;	break;
				case 4: sTrip = LoggingStringConstants.TRAP_ACTION_STRUCK_WITH;	break;
				case 5: sTrip = LoggingStringConstants.TRAP_ACTION_AFFECTED_WITH;	break;
				case 6: sTrip = LoggingStringConstants.TRAP_ACTION_RAN_INTO;	break;
			}

			PlayerMobile pm = (PlayerMobile)m;
			////if (pm.PublicMyRunUO == true)
			//{
				string sEvent = m.Name + " " + sTitle + " " + sTrip + " " + sTrap + "#" + sDateString;
				((PlayerMobile)m).lastdeeds =  sTrip + " " + sTrap;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_ADVENTURES );
			//}

			return null;
		}

		/// <summary>
		/// Logs when a player is affected by a void trap.
		/// </summary>
		/// <param name="m">The player affected</param>
		/// <param name="sTrap">The void trap description</param>
		/// <returns>Always returns null</returns>
		public static string LogVoid( Mobile m, string sTrap )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			PlayerMobile pm = (PlayerMobile)m;
			////if (pm.PublicMyRunUO == true)
			//{
				string sEvent = m.Name + " " + sTitle + " " + sTrap + ", teleporting them far away#" + sDateString;
				((PlayerMobile)m).lastdeeds =  sTrap + ", teleporting them far away";
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_ADVENTURES );
			//}

			return null;
		}

		/// <summary>
		/// Logs when a player searches through loot containers.
		/// </summary>
		/// <param name="m">The player searching</param>
		/// <param name="sBox">The container name</param>
		/// <param name="sType">The container type ("boat", "corpse", or other)</param>
		/// <returns>Always returns null</returns>
		public static string LogLoot( Mobile m, string sBox, string sType )
		{
			if (Utility.RandomDouble() < LoggingConstants.LOOT_LOG_PROBABILITY) // Final - too many of these were being generated
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

				string sLoot = LoggingStringConstants.LOOT_ACTION_SEARCHED;
				switch( Utility.Random( LoggingConstants.LOOT_VERB_COUNT ) )
				{
					case 0: sLoot = LoggingStringConstants.LOOT_ACTION_SEARCHED;	break;
					case 1: sLoot = LoggingStringConstants.LOOT_ACTION_FOUND;	break;
					case 2: sLoot = LoggingStringConstants.LOOT_ACTION_DISCOVERED;	break;
					case 3: sLoot = LoggingStringConstants.LOOT_ACTION_LOOKED;	break;
					case 4: sLoot = LoggingStringConstants.LOOT_ACTION_STUMBLED;	break;
					case 5: sLoot = LoggingStringConstants.LOOT_ACTION_DUG;	break;
					case 6: sLoot = LoggingStringConstants.LOOT_ACTION_OPENED;	break;
				}
				if ( sType == "boat" )
				{
					switch( Utility.Random( LoggingConstants.BOAT_LOOT_VERB_COUNT ) )
					{
						case 0: sLoot = LoggingStringConstants.LOOT_ACTION_SEARCHED;	break;
						case 1: sLoot = LoggingStringConstants.LOOT_ACTION_FOUND;	break;
						case 2: sLoot = LoggingStringConstants.LOOT_ACTION_DISCOVERED;	break;
						case 3: sLoot = LoggingStringConstants.LOOT_ACTION_LOOKED;	break;
						case 4: sLoot = LoggingStringConstants.LOOT_ACTION_SAILED;	break;
					}
					if ( sBox.Contains("Abandoned") || sBox.Contains("Adrift") ){ sLoot = sLoot + "n"; }
				}
				else if ( sType == "corpse" )
				{
					switch( Utility.Random( LoggingConstants.CORPSE_LOOT_VERB_COUNT ) )
					{
						case 0: sLoot = LoggingStringConstants.LOOT_ACTION_SEARCHED;	break;
						case 1: sLoot = LoggingStringConstants.LOOT_ACTION_FOUND;	break;
						case 2: sLoot = LoggingStringConstants.LOOT_ACTION_DISCOVERED;	break;
						case 3: sLoot = LoggingStringConstants.LOOT_ACTION_LOOKED;	break;
						case 4: sLoot = LoggingStringConstants.LOOT_ACTION_SAILED;	break;
					}
					if ( sBox.Contains("Abandoned") || sBox.Contains("Adrift") ){ sLoot = sLoot + "n"; }
				}

				PlayerMobile pm = (PlayerMobile)m;
				//if (pm.PublicMyRunUO == true)
				{
					string sEvent = m.Name + " " + sTitle + " " + sLoot + " " + sBox + "#" + sDateString;
					((PlayerMobile)m).lastdeeds =  " " + sLoot + " " + sBox;
					LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_ADVENTURES );
				}
			}
			return null;
		}

		/// <summary>
		/// Logs when a player makes a fatal mistake (killed by kill tile).
		/// </summary>
		/// <param name="m">The player who made the mistake</param>
		/// <param name="sTrap">The kill tile description</param>
		/// <returns>Always returns null</returns>
		public static string LogKillTile( Mobile m, string sTrap )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = m.Name + " " + sTitle + " " + string.Format(LoggingStringConstants.ACTION_FATAL_MISTAKE_FORMAT, sTrap) + "#" + sDateString;
				((PlayerMobile)m).lastdeeds =  " made a fatal mistake from " + sTrap;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_JOURNIES );
			}

			return null;
		}

		/// <summary>
		/// Logs when a player logs in or out of the realm.
		/// </summary>
		/// <param name="m">The player logging in/out</param>
		/// <param name="sAccess">"login" or "logout"</param>
		/// <returns>Always returns null</returns>
		public static string LogAccess( Mobile m, string sAccess )
		{
			PlayerMobile pm = (PlayerMobile)m;
			string sTitle = Server.Misc.Helpers.LoggingStringHelper.GetPlayerTitle( m );

            if ( m.AccessLevel < AccessLevel.GameMaster )
            {
				string sEvent;
				if ( sAccess == "login" )
				{
					sEvent = Server.Misc.Helpers.LoggingStringHelper.BuildEventStringWithTitle( m, sTitle, LoggingStringConstants.ACTION_HAD_ENTERED_REALM );
					World.Broadcast(0x35, true, LoggingStringConstants.MSG_BROADCAST_ENTERED_REALM_FORMAT, m.Name, sTitle);
				}
				else
				{
					sEvent = Server.Misc.Helpers.LoggingStringHelper.BuildEventStringWithTitle( m, sTitle, LoggingStringConstants.ACTION_HAD_LEFT_REALM );
					World.Broadcast(0x35, true, LoggingStringConstants.MSG_BROADCAST_LEFT_REALM_FORMAT, m.Name, sTitle);
				}


				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_ADVENTURES );
            }

			return null;
		}

		/// <summary>
		/// Logs when a player dies.
		/// </summary>
		/// <param name="m">The player who died</param>
		/// <param name="mob">The mobile that killed them (or null)</param>
		/// <returns>Always returns null</returns>
		public static string LogDeaths( Mobile m, Mobile mob )
		{
			if ( m != null && m is PlayerMobile && mob != null )
			{
				PlayerMobile pm = (PlayerMobile)m;
				string sDateString = GetPlayerInfo.GetTodaysDate();
				string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
				if ( m.Title != null ){ sTitle = m.Title; }

				string sKiller = Server.Misc.Helpers.LoggingStringHelper.ExtractNameWithoutBrackets( mob.Name );

				///////// PLAYER DIED SO DO SINGLE FILES //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				if ( m.AccessLevel < AccessLevel.GameMaster )
				{
					string sEvent = "";

					//if ( pm.PublicMyRunUO == true )
					//{
						if ( ( mob == m ) && ( mob != null ) )
						{
							sEvent = m.Name + " " + sTitle + " " + LoggingStringConstants.ACTION_HAD_KILLED_SELVES + "#" + sDateString;
						}
						else if ( ( mob != null ) && ( mob is PlayerMobile ) )
						{
							string kTitle = " the " + GetPlayerInfo.GetSkillTitle( mob );
							if ( mob.Title != null ){ kTitle = " " + mob.Title; }
							sEvent = m.Name + " " + sTitle + " " + LoggingStringConstants.ACTION_HAD_BEEN_KILLED_BY + " " + sKiller + kTitle + "#" + sDateString;
						}
						else if ( mob != null )
						{
							string kTitle = "";
							if ( mob.Title != null ){ kTitle = " " + mob.Title; }
							sEvent = m.Name + " " + sTitle + " " + LoggingStringConstants.ACTION_HAD_BEEN_KILLED_BY + " " + sKiller + kTitle + "#" + sDateString;
						}
						else
						{
							sEvent = m.Name + " " + sTitle + " " + LoggingStringConstants.ACTION_HAD_BEEN_KILLED + "#" + sDateString;
						}
					/*}
					else
					{
						string privateEnemy = "an opponent";
						switch ( Utility.Random( 6 ) )
						{
							case 0: privateEnemy = "an opponent"; break;
							case 1: privateEnemy = "an enemy"; break;
							case 2: privateEnemy = "another"; break;
							case 3: privateEnemy = "an adversary"; break;
							case 4: privateEnemy = "a foe"; break;
							case 5: privateEnemy = "a rival"; break;
						}

						if ( ( mob == m ) && ( mob != null ) )
						{
							sEvent = m.Name + " " + sTitle + " had killed themselves#" + sDateString;
						}
						else if ( ( mob != null ) && ( mob is PlayerMobile ) )
						{
							string kTitle = "the " + GetPlayerInfo.GetSkillTitle( mob );
							if ( mob.Title != null ){ kTitle = mob.Title; }
							sEvent = m.Name + " " + sTitle + " had been killed by " + sKiller + " " + kTitle + "#" + sDateString;
						}
						else if ( mob != null )
						{
							sEvent = m.Name + " " + sTitle + " had been killed by " + privateEnemy + "#" + sDateString;
						}
						else
						{
							sEvent = m.Name + " " + sTitle + " had been killed#" + sDateString;
						}
					}*/
					LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_DEATHS );
				}
			}
			return null;
		}

		/// <summary>
		/// Logs when a player becomes a murderer (has kills).
		/// </summary>
		/// <param name="m">The player who became a murderer</param>
		/// <param name="nKills">The number of kills (unused, uses m.Kills instead)</param>
		/// <returns>Always returns null</returns>
		public static string LogKillers( Mobile m, int nKills )
		{
			string sEvent = "";
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			if ( m.Kills > 1){ sEvent = m.Name + " " + sTitle + " is wanted for the murder of " + m.Kills + " people."; }
			else if ( m.Kills > 0){ sEvent = m.Name + " " + sTitle + " is wanted for murder."; }

			LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_MURDERERS );

			return null;
		}

		#endregion

		#region Misc Logging Methods

		/// <summary>
		/// Logs when a player purchases lottery tickets.
		/// </summary>
		/// <param name="m">The player purchasing tickets</param>
		/// <param name="purchase">The amount of gold spent</param>
		/// <returns>Always returns null</returns>
		public static string LogLottery( Mobile m, int purchase )
		{
			PlayerMobile pm = (PlayerMobile)m;
			////if (pm.PublicMyRunUO == true)
			//{
				string action = string.Format(LoggingStringConstants.ACTION_SPENT_LOTTERY_FORMAT, purchase);
				string sEvent = Server.Misc.Helpers.LoggingStringHelper.BuildEventString( m, action );
				((PlayerMobile)m).lastdeeds =  " spent " + purchase + " gold on Lottery tickets!";
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_MISC );
			//}

			return null;
		}

		public static string LogInvestments( Mobile m, int goldearned, bool gain )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			PlayerMobile pm = (PlayerMobile)m;
			////if (pm.PublicMyRunUO == true)
			//{
				string sEvent = "";
				if (gain)
				{
					sEvent = m.Name + " " + sTitle + " " + string.Format(LoggingStringConstants.ACTION_EARNED_INVESTMENTS_FORMAT, goldearned) + "#" + sDateString;
					((PlayerMobile)m).lastdeeds =  " earned " + goldearned + " gold on their investments!";
				}
				else
				{
					sEvent = m.Name + " " + sTitle + " " + string.Format(LoggingStringConstants.ACTION_LOST_INVESTMENTS_FORMAT, goldearned) + "#" + sDateString;
					((PlayerMobile)m).lastdeeds =  " lost " + goldearned + " gold on their investments!";
				}
					
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_MISC );
			//}

			return null;
		}

		public static string LogWin( Mobile m, int win )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			PlayerMobile pm = (PlayerMobile)m;
			////if (pm.PublicMyRunUO == true)
			//{
				string sEvent = m.Name + " " + sTitle + " " + string.Format(LoggingStringConstants.ACTION_WON_LOTTERY_FORMAT, win) + "#" + sDateString;
				((PlayerMobile)m).lastdeeds =  " won " + win + " gold in the Lottery!";
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_MISC );
			//}

			return null;
		}
		
		public static string LogLose( Mobile m, int pot )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			PlayerMobile pm = (PlayerMobile)m;
			////if (pm.PublicMyRunUO == true)
			//{
				string sEvent = m.Name + " " + sTitle + " " + string.Format(LoggingStringConstants.ACTION_DID_NOT_WIN_LOTTERY_FORMAT, pot) + "#" + sDateString;
				((PlayerMobile)m).lastdeeds =  " did not win " + pot + " gold that was up for grabs at the Lottery";
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_MISC );
			//}

			return null;
		}
		
		public static string LogWench( Mobile m, int price, bool stolen)
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			PlayerMobile pm = (PlayerMobile)m;
			////if (pm.PublicMyRunUO == true)
			//{
				if (stolen)
				{
				string sEvent = m.Name + " " + sTitle + " " + LoggingStringConstants.ACTION_FELL_IN_LOVE_WENCH + "#" + sDateString;
				((PlayerMobile)m).lastdeeds =  " fell in love with the Wench and gave her all their gold!";
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_MISC );
				}
				else
				{
				string sEvent = m.Name + " " + sTitle + " " + string.Format(LoggingStringConstants.ACTION_PAID_WENCH_FORMAT, price) + "#" + sDateString;
				((PlayerMobile)m).lastdeeds =  " paid " + price + " for the services of a wench.";
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_MISC );
				}
			//}

			return null;
		}

		public static string LogPetSale( Mobile m, Mobile pet, int price)
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			string petname = pet.Name;
			if ( m.Title != null ){ sTitle = m.Title; }

			PlayerMobile pm = (PlayerMobile)m;
			////if (pm.PublicMyRunUO == true)
			//{
				string sEvent = m.Name + " " + sTitle + " " + string.Format(LoggingStringConstants.ACTION_SOLD_PET_FORMAT, petname, price) + "#" + sDateString;
				((PlayerMobile)m).lastdeeds =  " sold " + petname + " for " + price + " gold to the animal broker.";
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_MISC );
			//}

			return null;
		}

		public static string LogBreed( Mobile m, Mobile pet, uint level)
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			PlayerMobile pm = (PlayerMobile)m;
			////if (pm.PublicMyRunUO == true)
			//{
				string sEvent = m.Name + " " + sTitle + " " + string.Format(LoggingStringConstants.ACTION_BRED_PET_FORMAT, pet, level) + "#" + sDateString;
				((PlayerMobile)m).lastdeeds =  " bred a " + pet + " with a maximum level of " + level + " !";
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_MISC );
			//}

			return null;
		}

		public static string LogZombie( Mobile m )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			PlayerMobile pm = (PlayerMobile)m;
			////if (pm.PublicMyRunUO == true)
			//{
				string sEvent = m.Name + " " + sTitle + " " + LoggingStringConstants.ACTION_DIED_ROSE_ZOMBIE + "#" + sDateString;
				((PlayerMobile)m).lastdeeds =  " died and rose again as a deadly infected Zombie!!";
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_MISC );
			//}

			return null;
		}
		
		public static string LogStudy( Mobile m, SkillName skill, double amount )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			PlayerMobile pm = (PlayerMobile)m;
			////if (pm.PublicMyRunUO == true)
			//{
				string sEvent = m.Name + " " + sTitle + " " + string.Format(LoggingStringConstants.ACTION_STUDIED_SKILL_FORMAT, skill, String.Format("{0:0.0}", amount)) + "#" + sDateString;
				((PlayerMobile)m).lastdeeds =  " studied " + skill + " assiduously and raised the skill " + String.Format("{0:0.0}", amount) + " points!";
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_MISC );
			//}

			return null;
		}
			
		public static string LogCarve( Mobile m, string name )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			AetherGlobe.BalanceLevel += Utility.RandomMinMax(1, 5);
			
			PlayerMobile pm = (PlayerMobile)m;
			////if (pm.PublicMyRunUO == true)
			//{
				string sEvent = string.Format(LoggingStringConstants.ACTION_BUTCHERED_FORMAT, m.Name + " " + sTitle, name) + "#" + sDateString;
				((PlayerMobile)m).lastdeeds = " butchered " + name + ".  It was messy.";
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_MISC );
			//}

			return null;
		}
		
		public static string LogGM( Mobile m, Skill skill )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = m.Name + " " + sTitle + " " + string.Format(LoggingStringConstants.ACTION_GRANDMASTER_FORMAT, skill.Name) + "#" + sDateString;
				((PlayerMobile)m).lastdeeds =  " became a grandmaster in " + skill.Name;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_MISC );
			}

			return null;
		}

		#endregion

		#region Quest Logging Methods

		public static string LogSlayingLord( Mobile m, string creature )
		{
			if (Utility.RandomDouble() < LoggingConstants.LOOT_LOG_PROBABILITY) // Final - too many of these were being generated
			{
				string sDateString = GetPlayerInfo.GetTodaysDate();
				string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
				if ( m.Title != null ){ sTitle = m.Title; }

				string sLoot = LoggingStringConstants.LOOT_ACTION_SEARCHED;
				switch( Utility.Random( LoggingConstants.LOOT_VERB_COUNT ) )
				{
					case 0: sLoot = LoggingStringConstants.LOOT_ACTION_SEARCHED;	break;
					case 1: sLoot = LoggingStringConstants.LOOT_ACTION_FOUND;	break;
					case 2: sLoot = LoggingStringConstants.LOOT_ACTION_DISCOVERED;	break;
					case 3: sLoot = LoggingStringConstants.LOOT_ACTION_LOOKED;	break;
					case 4: sLoot = LoggingStringConstants.LOOT_ACTION_STUMBLED;	break;
					case 5: sLoot = LoggingStringConstants.LOOT_ACTION_DUG;	break;
					case 6: sLoot = LoggingStringConstants.LOOT_ACTION_OPENED;	break;
				}
				if ( sType == "boat" )
				{
					switch( Utility.Random( LoggingConstants.BOAT_LOOT_VERB_COUNT ) )
					{
						case 0: sLoot = LoggingStringConstants.LOOT_ACTION_SEARCHED;	break;
						case 1: sLoot = LoggingStringConstants.LOOT_ACTION_FOUND;	break;
						case 2: sLoot = LoggingStringConstants.LOOT_ACTION_DISCOVERED;	break;
						case 3: sLoot = LoggingStringConstants.LOOT_ACTION_LOOKED;	break;
						case 4: sLoot = LoggingStringConstants.LOOT_ACTION_SAILED;	break;
					}
					if ( sBox.Contains("Abandoned") || sBox.Contains("Adrift") ){ sLoot = sLoot + "n"; }
				}
				else if ( sType == "corpse" )
				{
					switch( Utility.Random( LoggingConstants.CORPSE_LOOT_VERB_COUNT ) )
					{
						case 0: sLoot = LoggingStringConstants.LOOT_ACTION_SEARCHED;	break;
						case 1: sLoot = LoggingStringConstants.LOOT_ACTION_FOUND;	break;
						case 2: sLoot = LoggingStringConstants.LOOT_ACTION_DISCOVERED;	break;
						case 3: sLoot = LoggingStringConstants.LOOT_ACTION_LOOKED;	break;
						case 4: sLoot = LoggingStringConstants.LOOT_ACTION_SAILED;	break;
					}
					if ( sBox.Contains("Abandoned") || sBox.Contains("Adrift") ){ sLoot = sLoot + "n"; }
				}

				PlayerMobile pm = (PlayerMobile)m;
				//if (pm.PublicMyRunUO == true)
				{
					string sEvent = m.Name + " " + sTitle + " " + sLoot + " " + sBox + "#" + sDateString;
					((PlayerMobile)m).lastdeeds =  " " + sLoot + " " + sBox;
					LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_ADVENTURES );
				}
			}
			return null;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static string LogSlayingLord( Mobile m, string creature )
		{
			if ( m != null )
			{
				if ( m is BaseCreature )
					m = ((BaseCreature)m).GetMaster();

				if ( m is PlayerMobile )
				{
					string sDateString = GetPlayerInfo.GetTodaysDate();
					string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
					if ( m.Title != null ){ sTitle = m.Title; }

					string verb = LoggingStringConstants.SLAYING_ACTION_DESTROYED;
					switch( Utility.Random( LoggingConstants.SLAYING_VERB_COUNT ) )
					{
						case 0: verb = LoggingStringConstants.SLAYING_ACTION_DEFEATED;		break;
						case 1: verb = LoggingStringConstants.SLAYING_ACTION_SLAIN;		break;
						case 2: verb = LoggingStringConstants.SLAYING_ACTION_DESTROYED;	break;
						case 3: verb = LoggingStringConstants.SLAYING_ACTION_VANQUISHED;	break;
					}

					PlayerMobile pm = (PlayerMobile)m;
					//if (pm.PublicMyRunUO == true)
					{
						string sEvent = m.Name + " " + sTitle + " " + verb + " " + creature + "#" + sDateString;
						((PlayerMobile)m).lastdeeds =  " " + verb + " " + creature;
						LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
					}
				}
			}

			return null;
		}
		// --------------------------------------------------------------------------------------------
		public static string LogCreatedArtifact( Mobile m, string sArty )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = string.Format(LoggingStringConstants.QUEST_ACTION_GODS_CREATED_ARTIFACT_FORMAT, sArty) + "#" + sDateString;
				((PlayerMobile)m).lastdeeds =  " was granted a legendary artefact called " + sArty;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
			}

			return null;
		}
		// --------------------------------------------------------------------------------------------
		public static string LogRuneOfVirtue( Mobile m, string side )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			string sText = LoggingStringConstants.QUEST_ACTION_CLEANSED_RUNES;
				if ( side == "evil" ){ sText = LoggingStringConstants.QUEST_ACTION_CORRUPTED_RUNES; }

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = m.Name + " " + sTitle + " " + sText + "#" + sDateString;
				((PlayerMobile)m).lastdeeds = " " + sText;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
			}

			return null;
		}
		// --------------------------------------------------------------------------------------------
		public static string LogCreatedSyth( Mobile m, string sArty )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = string.Format(LoggingStringConstants.QUEST_ACTION_SYTH_CONSTRUCTED_FORMAT, sArty) + "#" + sDateString;
				((PlayerMobile)m).lastdeeds = " constructed a weapon called " + sArty;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
			}

			return null;
		}
		
			// --------------------------------------------------------------------------------------------
		public static string LogCreatedJedi( Mobile m, string sArty )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = string.Format(LoggingStringConstants.QUEST_ACTION_JEDI_CONSTRUCTED_FORMAT, sArty) + "#" + sDateString;
				((PlayerMobile)m).lastdeeds = " constructed a weapon called " + sArty;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
			}

			return null;
		}
		// --------------------------------------------------------------------------------------------
		
		// --------------------------------------------------------------------------------------------
		public static string LogGenericQuest( Mobile m, string sText )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = m.Name + " " + sTitle + " " + sText + "#" + sDateString;
				((PlayerMobile)m).lastdeeds = " " + sText;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
			}

			return null;
		}
		// --------------------------------------------------------------------------------------------
		public static string LogFoundItemQuest( Mobile m, string sBox )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			string sLoot = LoggingStringConstants.QUEST_ACTION_DISCOVERED_THE;
			switch( Utility.Random( LoggingConstants.QUEST_ITEM_VERB_COUNT ) )
			{
				case 0: sLoot = LoggingStringConstants.QUEST_ACTION_FOUND_THE;		break;
				case 1: sLoot = LoggingStringConstants.QUEST_ACTION_RECOVERED_THE;	break;
				case 2: sLoot = LoggingStringConstants.QUEST_ACTION_UNEARTHED_THE;	break;
				case 3: sLoot = LoggingStringConstants.QUEST_ACTION_DISCOVERED_THE;	break;
			}

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = m.Name + " " + sTitle + " " + sLoot + " " + sBox + "#" + sDateString;
				((PlayerMobile)m).lastdeeds = " " + sLoot + " " + sBox;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
			}

			return null;
		}
		// --------------------------------------------------------------------------------------------
		public static string LogQuestItem( Mobile m, string sBox )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			string sLoot = LoggingStringConstants.QUEST_ACTION_DISCOVERED_THE;
			switch( Utility.Random( LoggingConstants.QUEST_ITEM_VERB_COUNT ) )
			{
				case 0: sLoot = LoggingStringConstants.QUEST_ACTION_FOUND_THE;		break;
				case 1: sLoot = LoggingStringConstants.QUEST_ACTION_RECOVERED_THE;	break;
				case 2: sLoot = LoggingStringConstants.QUEST_ACTION_UNEARTHED_THE;	break;
				case 3: sLoot = LoggingStringConstants.QUEST_ACTION_DISCOVERED_THE;	break;
			}

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = m.Name + " " + sTitle + " " + sLoot + " " + sBox + "#" + sDateString;
				((PlayerMobile)m).lastdeeds = " " + sLoot + " " + sBox;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
			}

			return null;
		}
		// --------------------------------------------------------------------------------------------
		public static string LogQuestBody( Mobile m, string sBox )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			string sLoot = LoggingStringConstants.QUEST_ACTION_FOUND_THE;
			switch( Utility.Random( LoggingConstants.QUEST_BODY_VERB_COUNT ) )
			{
				case 0: sLoot = LoggingStringConstants.QUEST_ACTION_FOUND_THE;		break;
				case 1: sLoot = LoggingStringConstants.QUEST_ACTION_RECOVERED_THE;	break;
				case 2: sLoot = LoggingStringConstants.QUEST_ACTION_UNEARTHED_THE;	break;
				case 3: sLoot = LoggingStringConstants.QUEST_ACTION_UNEARTHED_THE;	break; // "has dug up" uses unearthed
			}

			string sBone = LoggingStringConstants.QUEST_BODY_BONES;
			switch( Utility.Random( LoggingConstants.QUEST_BODY_NOUN_COUNT ) )
			{
				case 0: sBone = LoggingStringConstants.QUEST_BODY_BONES;		break;
				case 1: sBone = LoggingStringConstants.QUEST_BODY_BODY;			break;
				case 2: sBone = LoggingStringConstants.QUEST_BODY_REMAINS;		break;
				case 3: sBone = LoggingStringConstants.QUEST_BODY_CORPSE;		break;
			}

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = m.Name + " " + sTitle + " " + sLoot + " " + sBone + " of " + sBox + "#" + sDateString;
				((PlayerMobile)m).lastdeeds = " " + sLoot + " " + sBone + " of " + sBox;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
			}

			return null;
		}
		// --------------------------------------------------------------------------------------------
		public static string LogQuestChest( Mobile m, string sBox )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			string sLoot = LoggingStringConstants.QUEST_ACTION_FOUND_THE;
			switch( Utility.Random( LoggingConstants.QUEST_CHEST_VERB_COUNT ) )
			{
				case 0: sLoot = LoggingStringConstants.QUEST_ACTION_FOUND_THE;		break;
				case 1: sLoot = LoggingStringConstants.QUEST_ACTION_RECOVERED_THE;	break;
				case 2: sLoot = LoggingStringConstants.QUEST_ACTION_UNEARTHED_THE;	break;
				case 3: sLoot = LoggingStringConstants.QUEST_ACTION_UNEARTHED_THE;	break; // "has dug up" uses unearthed
			}

			string sChest = LoggingStringConstants.QUEST_CHEST_HIDDEN;
			switch( Utility.Random( LoggingConstants.QUEST_CHEST_ADJECTIVE_COUNT ) )
			{
				case 0: sChest = LoggingStringConstants.QUEST_CHEST_HIDDEN;		break;
				case 1: sChest = LoggingStringConstants.QUEST_CHEST_LOST;		break;
				case 2: sChest = LoggingStringConstants.QUEST_CHEST_MISSING;		break;
				case 3: sChest = LoggingStringConstants.QUEST_CHEST_SECRET;		break;
			}

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = m.Name + " " + sTitle + " " + sLoot + " " + sChest + " chest of " + sBox + "#" + sDateString;
				((PlayerMobile)m).lastdeeds = " " + sLoot + " " + sChest + " chest of " + sBox;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
			}

			return null;
		}
		// --------------------------------------------------------------------------------------------
		public static string LogQuestMap( Mobile m, int sLevel, string chest )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			string sLoot = LoggingStringConstants.QUEST_ACTION_FOUND_THE;
			switch( Utility.Random( LoggingConstants.QUEST_MAP_VERB_COUNT ) )
			{
				case 0: sLoot = LoggingStringConstants.QUEST_ACTION_FOUND_THE;		break;
				case 1: sLoot = LoggingStringConstants.QUEST_ACTION_RECOVERED_THE;	break;
				case 2: sLoot = LoggingStringConstants.QUEST_ACTION_UNEARTHED_THE;	break;
				case 3: sLoot = LoggingStringConstants.QUEST_ACTION_UNEARTHED_THE;	break; // "has dug up" uses unearthed
			}

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = m.Name + " " + sTitle + " " + sLoot + " " + chest + "#" + sDateString;
				((PlayerMobile)m).lastdeeds = " " + sLoot + " " + chest;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
			}

			return null;
		}
		// --------------------------------------------------------------------------------------------
		public static string LogQuestSea( Mobile m, int sLevel, string sShip )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			string sLoot = LoggingStringConstants.QUEST_ACTION_FISHED_UP;
			switch( Utility.Random( LoggingConstants.QUEST_SEA_VERB_COUNT ) )
			{
				case 0: sLoot = LoggingStringConstants.QUEST_ACTION_SURFACED;		break;
				case 1: sLoot = LoggingStringConstants.QUEST_ACTION_SALVAGED;		break;
				case 2: sLoot = LoggingStringConstants.QUEST_ACTION_BROUGHT_UP;	break;
				case 3: sLoot = LoggingStringConstants.QUEST_ACTION_FISHED_UP;	break;
			}

			string sChest = LoggingStringConstants.QUEST_SEA_CHEST_GREAT;
			switch( sLevel )
			{
				case 0: sChest = LoggingStringConstants.QUEST_SEA_CHEST_MEAGER;		break;
				case 1: sChest = LoggingStringConstants.QUEST_SEA_CHEST_SIMPLE;		break;
				case 2: sChest = LoggingStringConstants.QUEST_SEA_CHEST_GOOD;			break;
				case 3: sChest = LoggingStringConstants.QUEST_SEA_CHEST_GREAT;		break;
				case 4: sChest = LoggingStringConstants.QUEST_SEA_CHEST_EXCELLENT;	break;
				case 5: sChest = LoggingStringConstants.QUEST_SEA_CHEST_SUPERB;		break;
			}

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = m.Name + " " + sTitle + " " + sLoot + " " + sChest + " from " + sShip + "#" + sDateString;
				((PlayerMobile)m).lastdeeds = " " + sLoot + " " + sChest + " from " + sShip;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
			}

			return null;
		}
		// --------------------------------------------------------------------------------------------
		public static string LogQuestKill( Mobile m, string sBox, Mobile t )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }

			string sLoot = "";
			string sWho = "";
			
			if ( sBox == "bounty" )
			{
				sWho = "";
				switch( Utility.Random( LoggingConstants.QUEST_KILL_VERB_COUNT ) )
				{
					case 0: sLoot = LoggingStringConstants.QUEST_ACTION_FULFILLED_BOUNTY;	break;
					case 1: sLoot = LoggingStringConstants.QUEST_ACTION_CLAIMED_BOUNTY;		break;
					case 2: sLoot = LoggingStringConstants.QUEST_ACTION_SERVED_BOUNTY;		break;
					case 3: sLoot = LoggingStringConstants.QUEST_ACTION_COMPLETED_BOUNTY;	break;
				}
			}
			if ( sBox == "sea" )
			{
				sWho = " " + LoggingStringConstants.QUEST_LOCATION_HIGH_SEAS;
				switch( Utility.Random( LoggingConstants.QUEST_KILL_SEA_VERB_COUNT ) )
				{
					case 0: sLoot = LoggingStringConstants.QUEST_ACTION_FULFILLED_BOUNTY;	break;
					case 1: sLoot = LoggingStringConstants.QUEST_ACTION_CLAIMED_BOUNTY;		break;
					case 2: sLoot = LoggingStringConstants.QUEST_ACTION_SERVED_BOUNTY;		break;
					case 3: sLoot = LoggingStringConstants.QUEST_ACTION_COMPLETED_BOUNTY;	break;
				}
			}
			if ( sBox == "assassin" )
			{
				sWho = " " + LoggingStringConstants.QUEST_LOCATION_FOR_GUILD;
				switch( Utility.Random( LoggingConstants.QUEST_KILL_ASSASSIN_VERB_COUNT ) )
				{
					case 0: sLoot = LoggingStringConstants.QUEST_ACTION_ASSASSINATED;		break;
					case 1: sLoot = LoggingStringConstants.QUEST_ACTION_DISPATCHED;		break;
					case 2: sLoot = LoggingStringConstants.QUEST_ACTION_DEALT_WITH;		break;
					case 3: sLoot = LoggingStringConstants.QUEST_ACTION_ELIMINATED;		break;
				}
			}
			
			sLoot = sLoot + " " + t.Name + " " + t.Title;

			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = m.Name + " " + sTitle + " " + sLoot + sWho + "#" + sDateString;
				((PlayerMobile)m).lastdeeds = " " + sLoot + sWho;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
			}

			return null;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static string LogGeneric( Mobile m, string sText )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }


			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = m.Name + " " + sTitle + " " + sText + "#" + sDateString;
				((PlayerMobile)m).lastdeeds = " " + sText;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_QUESTS );
			}


			return null;
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////////////


		public static string LogStandard( Mobile m, string sText )
		{
			string sDateString = GetPlayerInfo.GetTodaysDate();
			string sTitle = "the " + GetPlayerInfo.GetSkillTitle( m );
			if ( m.Title != null ){ sTitle = m.Title; }


			PlayerMobile pm = (PlayerMobile)m;
			//if (pm.PublicMyRunUO == true)
			{
				string sEvent = m.Name + " " + sTitle + " " + sText + "#" + sDateString;
				//((PlayerMobile)m).lastdeeds = " " + sText;
				LoggingFunctions.LogEvent( sEvent, LoggingConstants.LOG_TYPE_ADVENTURES );
			}

			return null;
		}

		public static string LogClear( string sLog )
		{
			string sPath = Server.Misc.Helpers.LoggingPathHelper.GetFilePath( sLog );

			DeleteFile( sPath );

			return null;
		}

		#endregion
	}
}

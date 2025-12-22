using System;
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Mobiles.Helpers;
using Server.Regions;
using Server.Commands;
using System.Text;

namespace Server.Mobiles
{
	/// <summary>
	/// DungeonGuide NPC that provides contextual guidance based on region type.
	/// Inherits from TownHerald and specializes in dungeon and region-specific messages.
	/// </summary>
	public class DungeonGuide : TownHerald
	{
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the DungeonGuide class.
		/// Sets up the guide with appropriate title and paralyzes for long-term placement.
		/// </summary>
		[Constructable]
		public DungeonGuide() : base()
		{
			Title = TownHeraldStringConstants.TITLE_DUNGEON_GUIDE;
			Name = NameList.RandomName( "male" );

			Blessed = true;
			Paralyze( TimeSpan.FromDays( TownHeraldConstants.PARALYZE_DAYS ) ); // Paralyzed for 10 years
		}

		#endregion

		#region Override Methods

		/// <summary>
		/// Called when a mobile moves near the dungeon guide.
		/// Provides contextual guidance based on the current region type.
		/// </summary>
		/// <param name="m">The mobile that moved</param>
		/// <param name="oldLocation">The previous location of the mobile</param>
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, TownHeraldConstants.TALK_RANGE ) && m is PlayerMobile )
			{
				// Turn to face the approaching player
				this.Direction = this.GetDirectionTo( m );

				if ( DateTime.UtcNow >= NextTalk )
				{
					Region reg = Region.Find( this.Location, this.Map );

					// Provide contextual guidance based on region type
					string message = null;

					if ( reg is DungeonRegion )
					{
						message = RegionMessageHelper.GetDungeonMessage(reg.Name);
					}
					else
					{
						message = RegionMessageHelper.GetRegionMessage(reg);
					}

					if ( message != null )
					{
						Say( message );
					}
					else
					{
						// Fall back to regular TownHerald behavior
						base.OnMovement( m, oldLocation );
					}

				NextTalk = DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( TownHeraldConstants.TALK_DELAY_MIN, TownHeraldConstants.TALK_DELAY_MAX ) );
			}
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">The serialization reader</param>
		public DungeonGuide( Serial serial ) : base( serial )
		{
		}

		/// <summary>
		/// Serializes the dungeon guide data
		/// </summary>
		/// <param name="writer">The writer to serialize to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the dungeon guide data
		/// </summary>
		/// <param name="reader">The reader to deserialize from</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion
	}

	/// <summary>
	/// TownHerald NPC that announces events, infections, and general news to players.
	/// Provides contextual information based on current game state and region.
	/// </summary>
	public class TownHerald : BasePerson
	{
		#region Fields

		/// <summary>Next time the herald can talk</summary>
		private DateTime m_NextTalk;

		#endregion

		#region Properties

		/// <summary>Gets or sets the next time the herald can talk</summary>
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }

		/// <summary>Gets whether to display title in single click</summary>
		public override bool ClickTitle{ get{ return false; } }

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the TownHerald class.
		/// Sets up appearance, stats, and equipment.
		/// </summary>
		[Constructable]
		public TownHerald() : base( )
		{
			NameHue = TownHeraldConstants.NAME_HUE_DEFAULT;

			InitStats( TownHeraldConstants.STAT_STR, TownHeraldConstants.STAT_DEX, TownHeraldConstants.STAT_INT );

			Title = TownHeraldStringConstants.TITLE_TOWN_CRIER;
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();

			AddItem( new FancyShirt( Utility.RandomBlueHue() ) );

			Item skirt;

			switch ( Utility.Random( 2 ) )
			{
				case 0: skirt = new Skirt(); break;
				default: case 1: skirt = new Kilt(); break;
			}

			skirt.Hue = Utility.RandomGreenHue();

			AddItem( skirt );

			AddItem( new FeatheredHat( Utility.RandomGreenHue() ) );

			Item boots;

			switch ( Utility.Random( 2 ) )
			{
				case 0: boots = new Boots(); break;
				default: case 1: boots = new ThighBoots(); break;
			}

			AddItem( boots );
			AddItem( new LightCitizen( true ) );

			Utility.AssignRandomHair( this );
		}

		#endregion

		#region Override Methods

		/// <summary>
		/// Called when a mobile moves near the town herald.
		/// Announces infections, events, or general status based on game state.
		/// </summary>
		/// <param name="m">The mobile that moved</param>
		/// <param name="oldLocation">The previous location of the mobile</param>
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, TownHeraldConstants.TALK_RANGE ) && m is PlayerMobile )
			{
				// Turn to face the approaching player
				this.Direction = this.GetDirectionTo( m );

				if ( DateTime.UtcNow >= m_NextTalk )
				{
					Region reg = Region.Find( this.Location, this.Map );

					if (AdventuresFunctions.InfectedRegions.Count > 0)
					{
						if (AetherGlobe.carrier != null && Utility.RandomDouble() > TownHeraldConstants.CARRIER_MESSAGE_CHANCE)
							Say(string.Format(TownHeraldStringConstants.MSG_CARRIER_SPREADING_FORMAT, AetherGlobe.carrier));
						else
							Say(TownHeraldStringConstants.MSG_INFECTED_SEEN);
					}
					else if ( LoggingFunctions.LoggingEvents() == true )
					{
						string sEvents = LoggingFunctions.LogShout();
						Say( sEvents );
					}
					else
					{
						Say( TownHeraldStringConstants.MSG_ALL_WELL );
					}
					m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( TownHeraldConstants.HERALD_TALK_DELAY_MIN, TownHeraldConstants.HERALD_TALK_DELAY_MAX ) ));
				}
			}
		}

		/// <summary>
		/// Determines if the herald handles speech from the specified mobile.
		/// </summary>
		/// <param name="from">The mobile speaking</param>
		/// <returns>Always returns true</returns>
		public override bool HandlesOnSpeech( Mobile from ) 
		{ 
			return true; 
		} 

		/// <summary>
		/// Called when a mobile speaks near the herald.
		/// Responds to queries about infections.
		/// </summary>
		/// <param name="e">The speech event arguments</param>
		public override void OnSpeech( SpeechEventArgs e ) 
		{
			if( e.Mobile.InRange( this, TownHeraldConstants.SPEECH_RANGE ))
			{
				if ( Insensitive.Contains( e.Speech, "infected") )  
				{
					TalkInfection();
				}
			}
		
		} 

		#endregion

		#region Helper Methods

		/// <summary>
		/// Talks about current infection status in the lands.
		/// Provides information about infected regions and carriers.
		/// </summary>
		public void TalkInfection()
		{
			StringBuilder sb = new StringBuilder();

			if (AdventuresFunctions.InfectedRegions == null)
                sb.Append(TownHeraldStringConstants.MSG_INFECTED_CONTAINED);

			AdventuresFunctions.CheckInfection();

			if (AdventuresFunctions.InfectedRegions.Count > 0 )
			{
				sb.Append(TownHeraldStringConstants.MSG_INFECTED_SPOTTED);

				for ( int i = 0; i < AdventuresFunctions.InfectedRegions.Count; i++ ) // load static regions
				{			
					String r = (String)AdventuresFunctions.InfectedRegions[i];

					if ( r != null || r != "" || r != " " )
						sb.Append( r + TownHeraldStringConstants.MSG_LOCATION_SEPARATOR );
					else
						sb.Append( TownHeraldStringConstants.MSG_UNKNOWN_LOCATION );
				}

				sb.Append(TownHeraldStringConstants.MSG_HELP_NEEDED);
			}
			else
				sb.Append(TownHeraldStringConstants.MSG_INFECTED_CONTAINED);

			Say( sb.ToString() );
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">The serialization reader</param>
        public TownHerald(Serial serial) : base(serial)
		{
		}

		/// <summary>
		/// Serializes the town herald data
		/// </summary>
		/// <param name="writer">The writer to serialize to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); 
		}

		/// <summary>
		/// Deserializes the town herald data
		/// </summary>
		/// <param name="reader">The reader to deserialize from</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion

		#region Context Menu

		/// <summary>
		/// Gets context menu entries for the town herald.
		/// </summary>
		/// <param name="from">The mobile viewing the menu</param>
		/// <param name="list">The list to add entries to</param>
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			//list.Add( new TownHeraldEntry( from, this ) ); 
		} 

		#endregion

		#region Nested Classes

		/// <summary>
		/// Context menu entry for interacting with the town herald.
		/// </summary>
		public class TownHeraldEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public TownHeraldEntry( Mobile from, Mobile giver ) : base( TownHeraldConstants.CONTEXT_MENU_ID, TownHeraldConstants.CONTEXT_MENU_RANGE )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( LoggingFunctions.LoggingEvents() == true )
					{
						if ( ! mobile.HasGump( typeof( LoggingGumpCrier ) ) )
						{
							mobile.SendGump(new LoggingGumpCrier( mobile, TownHeraldConstants.GUMP_PAGE ));
						}
					}
					else
					{
						m_Giver.Say(string.Format(TownHeraldStringConstants.MSG_GOOD_DAY_FORMAT, m_Mobile.Name));
					}
				}
            }
        }

		#endregion
	}  
}
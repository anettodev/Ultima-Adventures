using System;
using Server.Mobiles;
using Server.Network;
using Server.Spells;

namespace Server.Items
{
	public class MoonObeliskTeleporter : Item
	{
		private string m_Substring;
		private int m_Keyword;
		private int m_Range;
		private Point3D m_PointDest;
		private Map m_MapDest;
		private bool m_Active, m_Creatures, m_CombatCheck;
		private bool m_SourceEffect;
		private bool m_DestEffect;
		private int m_SoundID;
		private TimeSpan m_Delay;

		[CommandProperty( AccessLevel.GameMaster )]
		public string Substring
		{
			get{ return m_Substring; }
			set{ m_Substring = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Keyword
		{
			get{ return m_Keyword; }
			set{ m_Keyword = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Range
		{
			get{ return m_Range; }
			set{ m_Range = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active
		{
			get { return m_Active; }
			set { m_Active = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D PointDest
		{
			get { return m_PointDest; }
			set { m_PointDest = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map MapDest
		{
			get { return m_MapDest; }
			set { m_MapDest = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Creatures
		{
			get { return m_Creatures; }
			set { m_Creatures = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CombatCheck
		{
			get { return m_CombatCheck; }
			set { m_CombatCheck = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool SourceEffect
		{
			get{ return m_SourceEffect; }
			set{ m_SourceEffect = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool DestEffect
		{
			get{ return m_DestEffect; }
			set{ m_DestEffect = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int SoundID
		{
			get{ return m_SoundID; }
			set{ m_SoundID = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan Delay
		{
			get{ return m_Delay; }
			set{ m_Delay = value; InvalidateProperties(); }
		}

		public override int LabelNumber{ get{ return 1016474; } } // an MoonObelisk

		[Constructable]
		public MoonObeliskTeleporter() : base(0x115F)
		{
			Movable = false;
			m_Keyword = -1;
			m_Substring = null;
			m_Range = 2;
			m_Active = true;
			m_PointDest = new Point3D(0, 0, 0);
			m_MapDest = null;
			m_Creatures = false;
			m_CombatCheck = false;
			m_SourceEffect = false;
			m_DestEffect = false;
			m_SoundID = 0;
			m_Delay = TimeSpan.Zero;
		}

		public override bool HandlesOnSpeech{ get{ return true; } }

		public override void OnSpeech( SpeechEventArgs e )
		{
			if ( !e.Handled && m_Active )
			{
				Mobile m = e.Mobile;

				if ( !m_Creatures && !m.Player )
					return;

				if ( !m.InRange( GetWorldLocation(), m_Range ) )
					return;

				if ( m_CombatCheck && Server.Spells.SpellHelper.CheckCombat( m ) )
				{
					m.SendLocalizedMessage( 1005564, "", 0x22 ); // Wouldst thou flee during the heat of battle??
					return;
				}

				bool isMatch = false;

				if ( m_Keyword >= 0 && e.HasKeyword( m_Keyword ) )
					isMatch = true;
				else if ( m_Substring != null && e.Speech.ToLower().IndexOf( m_Substring.ToLower() ) >= 0 )
					isMatch = true;

				if ( !isMatch )
					return;

				e.Handled = true;
				StartTeleport( m );
			}
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( m is PlayerMobile && ((PlayerMobile)m).SkillsCap != 10000)
			{
				if ( Utility.InRange( Location, m.Location, 6 ) && !Utility.InRange( Location, oldLocation, 6 ) )
				{
					m.MoveToWorld(oldLocation, m.Map);
					m.SendMessage("A strange moon on the obelisk glows and stops you from moving.");
				}
			}
		}

		public void StartTeleport( Mobile m )
		{
			if ( m_Delay == TimeSpan.Zero )
				DoTeleport( m );
			else
				Timer.DelayCall( m_Delay, new TimerStateCallback( DoTeleport_Callback ), m );
		}

		private void DoTeleport_Callback( object state )
		{
			DoTeleport( (Mobile) state );
		}

		public void DoTeleport( Mobile m )
		{
			Map map = m_MapDest;

			if ( map == null || map == Map.Internal )
				map = m.Map;

			Point3D p = m_PointDest;

			if ( p == Point3D.Zero )
				p = m.Location;

			if (p.X == 5641 && map == Map.Felucca && m is PlayerMobile)
			{
				if ( !CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) )
				{
					p = new Point3D(198,2299,12);
					map = Map.Trammel;
				}
			}

			Server.Mobiles.BaseCreature.TeleportPets( m, p, map );

			bool sendEffect = ( !m.Hidden || m.AccessLevel == AccessLevel.Player );

			if ( m_SourceEffect && sendEffect )
				Effects.SendLocationEffect( m.Location, m.Map, 0x3728, 10, 10 );

			m.MoveToWorld( p, map );

			if ( m_DestEffect && sendEffect )
				Effects.SendLocationEffect( m.Location, m.Map, 0x3728, 10, 10 );

			if ( m_SoundID > 0 && sendEffect )
				Effects.PlaySound( m.Location, m.Map, m_SoundID );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060661, "Range\t{0}", m_Range );

			if ( m_Keyword >= 0 )
				list.Add( 1060662, "Keyword\t{0}", m_Keyword );

			if ( m_Substring != null )
				list.Add( 1060663, "Substring\t{0}", m_Substring );

			if ( m_PointDest != Point3D.Zero )
				list.Add( "Destination\t{0}", m_PointDest );

			if ( m_MapDest != null )
				list.Add( "Map\t{0}", m_MapDest );

			if ( m_Active )
				list.Add( 1060742 ); // active
			else
				list.Add( 1060743 ); // inactive

			if ( m_Creatures )
				list.Add( 1060658 ); // creatures included
			else
				list.Add( 1060659 ); // creatures excluded
		}

		public MoonObeliskTeleporter(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0); // version

			writer.Write( m_Substring );
			writer.Write( m_Keyword );
			writer.Write( m_Range );
			writer.Write( m_Active );
			writer.Write( m_PointDest );
			writer.Write( m_MapDest );
			writer.Write( m_Creatures );
			writer.Write( m_CombatCheck );
			writer.Write( m_SourceEffect );
			writer.Write( m_DestEffect );
			writer.Write( m_SoundID );
			writer.Write( m_Delay );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Substring = reader.ReadString();
					m_Keyword = reader.ReadInt();
					m_Range = reader.ReadInt();
					m_Active = reader.ReadBool();
					m_PointDest = reader.ReadPoint3D();
					m_MapDest = reader.ReadMap();
					m_Creatures = reader.ReadBool();
					m_CombatCheck = reader.ReadBool();
					m_SourceEffect = reader.ReadBool();
					m_DestEffect = reader.ReadBool();
					m_SoundID = reader.ReadInt();
					m_Delay = reader.ReadTimeSpan();

					break;
				}
			}
		}
	}
}

using System;
using Server;
using Server.Mobiles;

namespace Server.Gumps
{
	public class ConfirmReleaseGump : Gump
	{
		private Mobile m_From;
		private BaseCreature m_Pet;

		public ConfirmReleaseGump( Mobile from, BaseCreature pet ) : base( 25, 25 )
		{
			m_From = from;
			m_Pet = pet;

			m_From.CloseGump( typeof( ConfirmReleaseGump ) );

			AddPage(0);
			AddImage(0, 0, 154);
			AddImage(181, 0, 154);
			AddImage(2, 2, 163);
			AddImage(183, 2, 163);
			AddImage(7, 8, 145);
			AddImage(282, 9, 146);
			AddImage(202, 9, 156);
			AddImage(167, 7, 156);
			AddImage(184, 10, 156);
			AddButton(100, 200, 4005, 4005, 2, GumpButtonType.Reply, 0);
			AddButton(308, 200, 4020, 4020, 1, GumpButtonType.Reply, 0);
			AddHtml( 97, 152, 322, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Tem certeza que deseja libertar este animal?</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 137, 200, 58, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Sim</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 346, 200, 58, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Não</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddImage(278, 20, 156);
			AddImage(296, 21, 156);
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			if ( info.ButtonID == 2 )
			{
				if ( !m_Pet.Deleted && m_Pet.Controlled && m_From == m_Pet.ControlMaster && m_From.CheckAlive() /*&& m_Pet.CheckControlChance( m_From )*/ )
				{
					// Prevent releasing mounted followers
					if ( m_Pet is IMount && ((IMount)m_Pet).Rider != null )
					{
						m_From.PublicOverheadMessage( Server.Network.MessageType.Emote, 0x3B2, false, "*Isso não é uma boa ideia.*" );
						return;
					}

					// Allow release regardless of distance (for remote release via [pets] command)
					// The pet must still be controlled by the player and not deleted
					m_Pet.ControlTarget = null;
					m_Pet.ControlOrder = OrderType.Release;
				}
			}
		}
	}
}
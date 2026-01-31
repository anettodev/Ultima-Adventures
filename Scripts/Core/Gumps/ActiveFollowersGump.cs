using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Gumps
{
	/// <summary>
	/// Gump that displays a list of active followers with release buttons
	/// </summary>
	public class ActiveFollowersGump : Gump
	{
		private Mobile m_From;
		private List<BaseCreature> m_ActiveFollowers;

		// Gump Layout Constants (matching AnimalTrainer gump style)
		private const int GUMP_IMAGE_BACKGROUND = 155;
		private const int GUMP_IMAGE_BORDER_TL = 129;
		private const int GUMP_IMAGE_BORDER_TR = 129;
		private const int GUMP_IMAGE_BORDER_BL = 129;
		private const int GUMP_IMAGE_BORDER_BR = 129;
		private const int GUMP_IMAGE_HEADER = 133;
		private const int GUMP_IMAGE_DIVIDER = 132;
		private const int GUMP_IMAGE_BUTTON_BG = 134;

		// Gump Text Layout
		private const int GUMP_HEADER_X = 174;
		private const int GUMP_HEADER_Y = 68;
		private const int GUMP_HEADER_WIDTH = 300;
		private const int GUMP_HEADER_HEIGHT = 20;
		private const int GUMP_LIST_START_Y = 95;
		private const int GUMP_LIST_ITEM_HEIGHT = 35;
		private const int GUMP_PET_NAME_X = 145;
		private const int GUMP_PET_NAME_WIDTH = 425;
		private const int GUMP_BUTTON_X = 105;
		private const int GUMP_BUTTON_TYPE = 4005;

		public ActiveFollowersGump( Mobile from, List<BaseCreature> activeFollowers ) : base( 25, 25 )
		{
			m_From = from;
			m_ActiveFollowers = activeFollowers;

			from.CloseGump( typeof( ActiveFollowersGump ) );

			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			this.Resizable = false;

			AddPage(0);
			AddImage(0, 0, GUMP_IMAGE_BACKGROUND);
			AddImage(300, 0, GUMP_IMAGE_BACKGROUND);
			AddImage(0, 300, GUMP_IMAGE_BACKGROUND);
			AddImage(300, 300, GUMP_IMAGE_BACKGROUND);
			AddImage(2, 2, GUMP_IMAGE_BORDER_TL);
			AddImage(298, 2, GUMP_IMAGE_BORDER_TR);
			AddImage(2, 298, GUMP_IMAGE_BORDER_BL);
			AddImage(298, 298, GUMP_IMAGE_BORDER_BR);
			AddImage(7, 8, GUMP_IMAGE_HEADER);
			AddImage(218, 47, GUMP_IMAGE_DIVIDER);
			AddImage(380, 8, GUMP_IMAGE_BUTTON_BG);

			AddHtml( GUMP_HEADER_X, GUMP_HEADER_Y, GUMP_HEADER_WIDTH, GUMP_HEADER_HEIGHT, @"<BODY><BASEFONT Color=#FBFBFB><BIG>SEGUIDORES ATIVOS</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			// Add total followers label
			int totalFollowers = activeFollowers.Count;
			string totalLabel = string.Format( "<BODY><BASEFONT Color=#00FFFF><SMALL>Total de {0} seguidor{1}</SMALL></BASEFONT></BODY>", 
				totalFollowers, 
				totalFollowers != 1 ? "es" : "" );
			AddHtml( GUMP_PET_NAME_X - 50, GUMP_HEADER_Y + 30, GUMP_PET_NAME_WIDTH + 50, 20, totalLabel, (bool)false, (bool)false);

			int y = GUMP_LIST_START_Y + 10;

			if ( activeFollowers.Count == 0 )
			{
				AddHtml( GUMP_PET_NAME_X, y, GUMP_PET_NAME_WIDTH, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Você não tem seguidores ativos.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
			else
			{
				for ( int i = 0; i < activeFollowers.Count; ++i )
				{
					BaseCreature pet = activeFollowers[i];
					if ( pet == null || pet.Deleted )
						continue;

					y += GUMP_LIST_ITEM_HEIGHT;

					string petInfo = GetPetInfo( pet );
					AddHtml( GUMP_PET_NAME_X, y, GUMP_PET_NAME_WIDTH, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + petInfo + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(GUMP_BUTTON_X, y, GUMP_BUTTON_TYPE, GUMP_BUTTON_TYPE, (i + 1), GumpButtonType.Reply, 0);
				}
			}
		}

		/// <summary>
		/// Gets formatted information about a pet
		/// </summary>
		private string GetPetInfo( BaseCreature pet )
		{
			string petName = pet.Name ?? "Sem nome";
			string status = "";

			if ( pet.IsHitchStabled )
				status = " (Estabulado)";
			else if ( pet is IMount && ((IMount)pet).Rider != null )
				status = " (Montado)";
			else if ( pet.Controlled )
				status = " (Controlado)";
			else if ( pet.Summoned )
				status = " (Invocado)";

			return string.Format( "{0}{1}", petName, status );
		}

		/// <summary>
		/// Handles gump button responses
		/// </summary>
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( info.ButtonID > 0 && info.ButtonID <= m_ActiveFollowers.Count )
			{
				int petIndex = info.ButtonID - 1;
				BaseCreature selectedPet = m_ActiveFollowers[petIndex];

				if ( selectedPet != null && !selectedPet.Deleted && selectedPet.ControlMaster == m_From )
				{
					// Show release confirmation gump
					m_From.SendGump( new ConfirmReleaseGump( m_From, selectedPet ) );
				}
			}
		}
	}
}


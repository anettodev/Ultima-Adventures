using System;
using Server;
using Server.Gumps;
using Server.Items;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Gump that prompts the player whether to place a maker's mark on a crafted item
	/// </summary>
	public class QueryMakersMarkGump : Gump
	{
		#region Constants

		/// <summary>Gump X position</summary>
		private const int GUMP_X = 100;

		/// <summary>Gump Y position</summary>
		private const int GUMP_Y = 200;

		/// <summary>Gump width</summary>
		private const int GUMP_WIDTH = 220;

		/// <summary>Gump height</summary>
		private const int GUMP_HEIGHT = 170;

		/// <summary>Inner background X offset</summary>
		private const int INNER_X = 10;

		/// <summary>Inner background Y offset</summary>
		private const int INNER_Y = 10;

		/// <summary>Inner background width</summary>
		private const int INNER_WIDTH = 200;

		/// <summary>Inner background height</summary>
		private const int INNER_HEIGHT = 150;

		/// <summary>Outer background gump ID</summary>
		private const int BACKGROUND_OUTER = 5054;

		/// <summary>Inner background gump ID</summary>
		private const int BACKGROUND_INNER = 3000;

		/// <summary>Button ID for Continue (mark item)</summary>
		private const int BUTTON_CONTINUE = 1;

		/// <summary>Button ID for Cancel</summary>
		private const int BUTTON_CANCEL = 0;

		/// <summary>Button gump ID (normal state)</summary>
		private const int BUTTON_NORMAL = 4005;

		/// <summary>Button gump ID (pressed state)</summary>
		private const int BUTTON_PRESSED = 4007;

		/// <summary>Localized message: "Do you wish to place your maker's mark on this item?"</summary>
		private const int MSG_QUERY_MARK = 1018317;

		/// <summary>Localized message: "CONTINUE"</summary>
		private const int MSG_CONTINUE = 1011011;

		/// <summary>Localized message: "CANCEL"</summary>
		private const int MSG_CANCEL = 1011012;

		/// <summary>Localized message: "You mark the item."</summary>
		private const int MSG_MARKED = 501808;

		/// <summary>Localized message: "Cancelled mark."</summary>
		private const int MSG_CANCELLED = 501809;

		#endregion

		#region Fields

		private int m_Quality;
		private Mobile m_From;
		private CraftItem m_CraftItem;
		private CraftSystem m_CraftSystem;
		private Type m_TypeRes;
		private BaseTool m_Tool;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the QueryMakersMarkGump class
		/// </summary>
		/// <param name="quality">The quality level of the crafted item</param>
		/// <param name="from">The mobile who crafted the item</param>
		/// <param name="craftItem">The craft item that was created</param>
		/// <param name="craftSystem">The crafting system used</param>
		/// <param name="typeRes">The resource type used</param>
		/// <param name="tool">The crafting tool used</param>
		public QueryMakersMarkGump( int quality, Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool ) : base( GUMP_X, GUMP_Y )
		{
			from.CloseGump( typeof( QueryMakersMarkGump ) );

			m_Quality = quality;
			m_From = from;
			m_CraftItem = craftItem;
			m_CraftSystem = craftSystem;
			m_TypeRes = typeRes;
			m_Tool = tool;

			AddPage( 0 );

			AddBackground( 0, 0, GUMP_WIDTH, GUMP_HEIGHT, BACKGROUND_OUTER );
			AddBackground( INNER_X, INNER_Y, INNER_WIDTH, INNER_HEIGHT, BACKGROUND_INNER );

			AddHtmlLocalized( 20, 20, 180, 80, MSG_QUERY_MARK, false, false );

			AddHtmlLocalized( 55, 100, 140, 25, MSG_CONTINUE, false, false );
			AddButton( 20, 100, BUTTON_NORMAL, BUTTON_PRESSED, BUTTON_CONTINUE, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 55, 125, 140, 25, MSG_CANCEL, false, false );
			AddButton( 20, 125, BUTTON_NORMAL, BUTTON_PRESSED, BUTTON_CANCEL, GumpButtonType.Reply, 0 );
		}

		#endregion

		#region Methods

		/// <summary>
		/// Handles the gump response
		/// </summary>
		/// <param name="sender">The network state that sent the response</param>
		/// <param name="info">The relay information containing button selections</param>
		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			bool makersMark = ( info.ButtonID == BUTTON_CONTINUE );

			if ( makersMark )
				m_From.SendLocalizedMessage( MSG_MARKED );
			else
				m_From.SendLocalizedMessage( MSG_CANCELLED );

			m_CraftItem.CompleteCraft( m_Quality, makersMark, m_From, m_CraftSystem, m_TypeRes, m_Tool, null );
		}

		#endregion
	}
}
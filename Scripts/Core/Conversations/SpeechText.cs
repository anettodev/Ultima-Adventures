using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Regions;

namespace Server.Misc
{
  class SpeechFunctions
  {
		/// <summary>
		/// Gets conversation text for the specified conversation type.
		/// This method now uses the ConversationRegistry for all conversation lookups.
		/// </summary>
		/// <param name="sMyName">The speaker's name</param>
		/// <param name="sYourName">The listener's name</param>
		/// <param name="sConversation">The conversation type identifier</param>
		/// <returns>The formatted conversation text</returns>
		public static string SpeechText( string sMyName, string sYourName, string sConversation )
		{
			return ConversationRegistry.GetConversationText(sConversation, sMyName, sYourName);
		}
	}
}

/* TO ADD CONVERSATIONS TO NPCS, INCLUDE THE LINES OF CODE BELOW...

using Server.Misc;
using Server.ContextMenus;
using Server.Gumps;

		///////////////////////////////////////////////////////////////////////////
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		} 

		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "Camping Safely", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Ranger" ) ));
					}
				}
      }
    }
		///////////////////////////////////////////////////////////////////////////

*/

using System;
using Server.Network;
using Server;
using Server.Mobiles;

namespace Server.Misc
{
	// Create the timer that monitors the current state of hunger
	public class HitsDecayTimer : Timer
	{
		public static void Initialize()
		{
			new HitsDecayTimer().Start();
		}
		// Timer for starvation effects when hunger = 0
		public HitsDecayTimer() : base( TimeSpan.FromSeconds( 4 ), TimeSpan.FromSeconds( 4 ) )
		{
			Priority = TimerPriority.OneSecond;
		}
		
		protected override void OnTick()
		{
			HitsDecay();
		}
		// Check the NetState and call the decaying function
		public static void HitsDecay()
		{
			foreach ( NetState state in NetState.Instances )
			{
				HitsDecaying( state.Mobile );
			}
		}

		// Check hunger level - when hunger = 0, apply starvation effects
		public static void HitsDecaying( Mobile m )
		{
			if ( m is PlayerMobile && m != null && m.Hunger == 0 && m.Alive )
			{
				// Hunger = 0: -2 Hits, -2 Mana, -2 Stam per 4 seconds
				if ( m.Hits > 2 )
					m.Hits -= 2;
				if ( m.Mana > 2 )
					m.Mana -= 2;
				if ( m.Stam > 2 )
					m.Stam -= 2;

				m.SendMessage( "You are starving to death!" );
				m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "I am starving to death!");
			}
		}
	}
}

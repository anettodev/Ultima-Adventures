using System;
using Server;
using System.Collections.Generic;
using Server.Commands;
using Server.Mobiles;
using Server.SkillHandlers;
using Server.Misc;

namespace Server.Items
{
    class ClassicPoison
    {
        public static void Initialize()
        {
            // Admin/Owner only command to toggle global classic poisoning mode
            CommandSystem.Register("poisons", AccessLevel.Administrator, new CommandEventHandler(OnToggleClassicPoisoning));
        }

        [Usage("poisons")]
        [Description("Toggles global classic poisoning mode for all players. Admin/Owner only.")]
        private static void OnToggleClassicPoisoning(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
			int currentMode = MyServerSettings.ClassicPoisoningMode();

			if ( currentMode == 1 )
			{
				// Switching to modern mode (global)
				MyServerSettings.SetClassicPoisoningMode( 0 );
				m.SendMessage( 38, 
					PoisoningMessages.MSG_MODERN_MODE_ENABLED + " (Modo global aplicado a todos os jogadores)" );
			}
			else
			{
				// Switching to classic mode (global)
				MyServerSettings.SetClassicPoisoningMode( 1 );
				m.SendMessage( 68, 
					PoisoningMessages.MSG_CLASSIC_MODE_ENABLED + " (Modo global aplicado a todos os jogadores)" );
			}
        }
    }
}

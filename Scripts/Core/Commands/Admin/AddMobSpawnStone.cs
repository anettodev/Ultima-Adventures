using System;
using Server;
using Server.Commands;
using Server.Items;
using Server.Mobiles;

namespace Server.Commands
{
	public class AddMobSpawnStone
	{
		public static void Initialize()
		{
			CommandSystem.Register( "AddMobSpawnStone", AccessLevel.GameMaster, new CommandEventHandler( AddMobSpawnStone_OnCommand ) );
		}

		[Usage( "AddMobSpawnStone" )]
		[Description( "Creates a Mob Spawn Stone that can spawn mobs at a selected location." )]
		private static void AddMobSpawnStone_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			MobSpawnStone stone = new MobSpawnStone();
			stone.MoveToWorld( from.Location, from.Map );
			from.SendMessage( "A Mob Spawn Stone has been created. Double-click it to activate and select a spawn location." );
		}
	}
}


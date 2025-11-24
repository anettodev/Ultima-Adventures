using System;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Commands
{
    public class ItemSpawner
    {
        public static void Initialize()
        {
            CommandSystem.Register("ItemSpawner", AccessLevel.GameMaster, new CommandEventHandler(ItemSpawner_OnCommand));
            CommandSystem.Register("itemspawner", AccessLevel.GameMaster, new CommandEventHandler(ItemSpawner_OnCommand));
            CommandSystem.Register("UnpersistItem", AccessLevel.GameMaster, new CommandEventHandler(UnpersistItem_OnCommand));
            CommandSystem.Register("unpersistitem", AccessLevel.GameMaster, new CommandEventHandler(UnpersistItem_OnCommand));
        }

        [Usage("ItemSpawner")]
        [Description("Opens the ItemSpawner gump for easy item placement.")]
        private static void ItemSpawner_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            from.SendGump(new ItemSpawnerGump(from, ItemCategory.Decorations, 0, 1));
        }

        [Usage("UnpersistItem")]
        [Description("Target an item to remove it from the decorate.cfg persistence file.")]
        private static void UnpersistItem_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            from.SendMessage("Target the item you want to remove from persistence.");
            from.Target = new UnpersistItemTarget();
        }
    }

    public enum ItemCategory
    {
        None,
        Decorations,
        Construction,
        Resources,
        Doors
    }
}

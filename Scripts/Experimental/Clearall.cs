using System;
using System.IO;
using Server;
using System.Text;
using System.Collections;
using System.Net;
using Server.Mobiles;
using Server.Network;
using Server.Commands;
using Server.Gumps;

namespace Server.Commands
{
    public class ClearallConfirmGump : BaseConfirmGump
    {
        public override int TitleNumber { get { return 1075083; } } // Warning!
        public ClearallConfirmGump() : base()
        {
            // Replace the default label with our custom warning
            AddHtml(25, 55, 300, 200, "<center><basefont color=#FFFFFF><br><br>This action CANNOT be undone!<br><br>All decorations, spawns, and world items will be permanently deleted.</basefont></center>", false, false);
        }

        public override void Confirm(Mobile from)
        {
            if (from.AccessLevel < AccessLevel.Administrator)
            {
                from.SendMessage("You do not have permission to use this command.");
                return;
            }

            DateTime time = DateTime.Now;
            int countItems = 0;
            int countMobs = 0;
            ArrayList itemsdel = new ArrayList();

            // Count items to be deleted
            foreach (Item itemdel in World.Items.Values)
            {
                if (itemdel.Parent == null)
                {
                    itemsdel.Add(itemdel);
                    countItems += 1;
                }
            }

            // Count mobiles to be deleted
            foreach (Mobile mobdel in World.Mobiles.Values)
            {
                if (!mobdel.Player)
                {
                    itemsdel.Add(mobdel);
                    countMobs += 1;
                }
            }

            // Perform the deletion
            foreach (object itemdel2 in itemsdel)
            {
                if (itemdel2 is Item) ((Item)itemdel2).Delete();
                if (itemdel2 is Mobile) ((Mobile)itemdel2).Delete();
            }

            double totalTime = ((DateTime.Now - time).TotalSeconds);
            from.SendMessage("World cleared successfully! {0} items removed. {1} mobs removed. Took {2} seconds!", countItems, countMobs, totalTime);
        }

        public override void Refuse(Mobile from)
        {
            from.SendMessage("World clear cancelled.");
        }
    }

    public class WorldItemWipeCommand
    {
        public static void Initialize()
        {
            Register("Clearall", AccessLevel.Administrator, new CommandEventHandler(Clearall_OnCommand));
        }

        public static void Register(string command, AccessLevel access, CommandEventHandler handler)
        {
            CommandSystem.Register(command, access, handler);
        }

        [Usage("ClearAll")]
        [Description("Clear all facets.")]
        public static void Clearall_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            if (from.AccessLevel < AccessLevel.Administrator)
            {
                from.SendMessage("You do not have permission to use this command.");
                return;
            }

            from.SendGump(new ClearallConfirmGump());
        }
    }
}
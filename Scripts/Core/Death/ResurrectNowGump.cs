using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

namespace Server 
{ 
    /// <summary>
    /// Initializes auto-resurrection system event handlers
    /// DISABLED: Auto-resurrection gump system is disabled - players will only receive resurrection gumps from healers when near them
    /// </summary>
    public class AutoRessurection
    { 
        /// <summary>
        /// Registers event handlers for player death and login to show auto-resurrect gump
        /// DISABLED: Event handlers are commented out to disable the auto-resurrection gump system
        /// </summary>
        public static void Initialize()
        {
            // COMMENTED OUT: Auto-resurrection gump system is disabled
            // Players will only receive resurrection gumps from healers (BaseHealer.OnMovement) when they are near healers
            // EventSink.PlayerDeath += new PlayerDeathEventHandler(e => ResurrectNowGump.TryShowAutoResurrectGump(e.Mobile as PlayerMobile));
            // EventSink.Login += new LoginEventHandler(e => ResurrectNowGump.TryShowAutoResurrectGump(e.Mobile as PlayerMobile));
        }
    }
}

namespace Server.Gumps
{
    /// <summary>
    /// Gump displayed to players for instant resurrection with penalties
    /// </summary>
    public class ResurrectNowGump : Gump
    {
        #region Nested Types

        /// <summary>
        /// Button types for the gump
        /// </summary>
        private enum ButtonType
        {
            Close = 0,
            Accept = 1,
            Cancel = 2,
            CancelAndSuppress = 3
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of ResurrectNowGump
        /// </summary>
        /// <param name="from">The mobile to show the gump to</param>
        public ResurrectNowGump(Mobile from) : base(ResurrectionGumpConstants.GUMP_POS_X_NOW, ResurrectionGumpConstants.GUMP_POS_Y_NOW)
        {
            if (!(from is PlayerMobile) || from.Alive)
                return;

            PlayerMobile player = (PlayerMobile)from;

            // Calculate penalty
            double penalty = ResurrectionGumpHelper.CalculatePenaltyForResurrectNow(from.Karma);

            // Get costs and gold
            int healCost = GetPlayerInfo.GetResurrectCost(from);
            int bankGold = Banker.GetBalance(from);

            // Build message
            string sText = ResurrectionGumpHelper.BuildResurrectNowMessage(player, healCost, bankGold, penalty);

            // Get random grave message
            string sGrave = ResurrectionGumpHelper.GetRandomGraveMessageNow();

            // Setup gump properties
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);

            // Add gump layout (images and items)
            AddResurrectionGumpLayout();

            // Format bank gold
            string formattedBankGold;
            bool useKNotation;
            bool useKKNotation;
            ResurrectionGumpHelper.FormatBankGold(bankGold, out formattedBankGold, out useKNotation, out useKKNotation);

            // Add buttons and labels
            int y = 365;
            AddButton(ResurrectionGumpConstants.FIRST_COLUMN_X, y, 4023, 4024, (int)ButtonType.Accept, GumpButtonType.Reply, 0);
            AddHtml(ResurrectionGumpConstants.FIRST_COLUMN_X + ResurrectionGumpConstants.BUTTON_LABEL_OFFSET, y + 2, 477, 22, 
                @"<BODY><BASEFONT Color=#5eff00><BIG> " + ResurrectionGumpStringConstants.BUTTON_RESURRECT_ME + " </BIG></BASEFONT></BODY>", false, false);

            AddButton(ResurrectionGumpConstants.SECOND_COLUMN_X, y, 4017, 4018, (int)ButtonType.Cancel, GumpButtonType.Reply, 0);
            AddHtml(ResurrectionGumpConstants.SECOND_COLUMN_X + ResurrectionGumpConstants.BUTTON_LABEL_OFFSET, y + 2, 477, 22, 
                @"<BODY><BASEFONT Color=#fff700><BIG> " + ResurrectionGumpStringConstants.BUTTON_MAYBE_LATER + " </BIG></BASEFONT></BODY>", false, false);

            y += 30;
            AddButton(ResurrectionGumpConstants.SECOND_COLUMN_X, y, 4020, 4021, (int)ButtonType.CancelAndSuppress, GumpButtonType.Reply, 0);
            AddHtml(ResurrectionGumpConstants.SECOND_COLUMN_X + ResurrectionGumpConstants.BUTTON_LABEL_OFFSET, y + 2, 477, 22, 
                @"<BODY><BASEFONT Color=#FF0000><BIG> " + ResurrectionGumpStringConstants.BUTTON_NO_AND_STOP_ASKING + " </BIG></BASEFONT></BODY>", false, false);

            // Add tribute and bank gold labels
            AddHtml(ResurrectionGumpConstants.FIRST_COLUMN_X, 291, 190, 22, 
                @"<BODY><BASEFONT Color=#FCFF00><BIG>" + ResurrectionGumpStringConstants.LABEL_TRIBUTE_VALUE + "</BIG></BASEFONT></BODY>", false, false);
            AddHtml(ResurrectionGumpConstants.SECOND_COLUMN_X, 291, 196, 22, 
                @"<BODY><BASEFONT Color=#ffffff><BIG>" + string.Format(ResurrectionGumpStringConstants.FORMAT_GOLD_AMOUNT, healCost) + "</BIG></BASEFONT></BODY>", false, false);

            AddHtml(ResurrectionGumpConstants.FIRST_COLUMN_X, 315, 190, 22, 
                @"<BODY><BASEFONT Color=#FCFF00><BIG>" + ResurrectionGumpStringConstants.LABEL_BANK_GOLD + "</BIG></BASEFONT></BODY>", false, false);
            
            if (useKNotation)
            {
                AddHtml(ResurrectionGumpConstants.SECOND_COLUMN_X, 315, 196, 22, 
                    @"<BODY><BASEFONT Color=#ffffff><BIG>" + string.Format(ResurrectionGumpStringConstants.FORMAT_GOLD_K, formattedBankGold) + "</BIG></BASEFONT></BODY>", false, false);
            }
            else if (useKKNotation)
            {
                AddHtml(ResurrectionGumpConstants.SECOND_COLUMN_X, 315, 196, 22, 
                    @"<BODY><BASEFONT Color=#ffffff><BIG>" + string.Format(ResurrectionGumpStringConstants.FORMAT_GOLD_KK, formattedBankGold) + "</BIG></BASEFONT></BODY>", false, false);
            }
            else
            {
                AddHtml(ResurrectionGumpConstants.SECOND_COLUMN_X, 315, 156, 22, 
                    @"<BODY><BASEFONT Color=#ffffff><BIG>" + string.Format(ResurrectionGumpStringConstants.FORMAT_GOLD_AMOUNT, bankGold) + "</BIG></BASEFONT></BODY>", false, false);
            }

            // Add grave message and main text
            AddHtml(267, 95, 306, 22, 
                @"<BODY><BASEFONT Color=#fff700><BIG><CENTER>" + sGrave + "</CENTER></BIG></BASEFONT></BODY>", false, false);
            AddHtml(ResurrectionGumpConstants.FIRST_COLUMN_X, 155, 477, 130, 
                @"<BODY><BASEFONT Color=#ffffff><BIG>" + sText + "</BIG></BASEFONT></BODY>", false, false);
        }

        #endregion

        #region Gump Layout

        /// <summary>
        /// Adds the standard resurrection gump layout (images and items)
        /// </summary>
        private void AddResurrectionGumpLayout()
        {
            AddImage(0, 0, 154);
            AddImage(300, 201, 154);
            AddImage(0, 201, 154);
            AddImage(300, 0, 154);
            AddImage(298, 199, 129);
            AddImage(2, 199, 129);
            AddImage(298, 2, 129);
            AddImage(2, 2, 129);
            AddImage(7, 6, 145);
            AddImage(8, 257, 142);
            AddImage(253, 285, 144);
            AddImage(171, 47, 132);
            AddImage(379, 8, 134);
            AddImage(167, 7, 156);
            AddImage(209, 11, 156);
            AddImage(189, 10, 156);
            AddImage(170, 44, 159);

            AddItem(173, 64, 4455);
            AddItem(186, 85, 3810);
            AddItem(209, 102, 3808);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles gump response
        /// </summary>
        /// <param name="state">Network state</param>
        /// <param name="info">Relay information</param>
        public override void OnResponse(NetState state, RelayInfo info)
        {
            PlayerMobile from = state.Mobile as PlayerMobile;
            if (from == null)
                return;

            from.CloseGump(typeof(ResurrectNowGump));

            ButtonType button = (ButtonType)info.ButtonID;

            switch (button)
            {
                case ButtonType.Accept:
                    if (from.Alive)
                        return;

                    // NOTE: ResurrectNowGump is disabled - this code is not executed
                    // Delay checking removed - ResurrectionDelayTimer handles delays in ResurrectCostGump

                    from.Resurrect();
                    Server.Misc.Death.Penalty(from, true, false); // This will increment debits
                    bool hasDebits = (from.ResurrectionDebits > 0);
                    ResurrectionGumpHelper.ApplyResurrectionEffects(from, hasDebits);
                    from.LastAutoRes = DateTime.UtcNow;
                    break;

                case ButtonType.Cancel:
                case ButtonType.Close:
                case ButtonType.CancelAndSuppress:
                default:
                    from.SendMessage(ResurrectionGumpConstants.MSG_COLOR_WARNING, ResurrectionGumpStringConstants.MSG_REMAIN_SPIRITUAL_REALM);
                    if (button == ButtonType.CancelAndSuppress)
                        return;

                    TryShowAutoResurrectGump(from);
                    break;
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Attempts to show the auto-resurrect gump to a player after a delay
        /// </summary>
        /// <param name="mobile">The player mobile</param>
        public static void TryShowAutoResurrectGump(PlayerMobile mobile)
        {
            if (mobile == null || mobile.SoulBound || mobile.Alive)
                return;

            // Young/Iniciante players are teleported to healer location and should only get healer gumps when near healers
            // They should not receive the auto-resurrection gump
            if (mobile.Young)
                return;

            Timer.DelayCall(TimeSpan.FromSeconds(ResurrectionGumpConstants.AUTO_RESURRECT_DELAY_SECONDS), (m) =>
            {
                if (m == null || m.SoulBound || m.Alive)
                    return;

                // Double-check Young status in case it changed during the delay
                if (m.Young)
                    return;

                m.CloseGump(typeof(ResurrectNowGump));
                m.SendGump(new ResurrectNowGump(m));

            }, mobile);
        }

        #endregion
    }
}

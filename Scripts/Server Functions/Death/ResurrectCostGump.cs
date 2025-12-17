using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Mobiles;

namespace Server.Gumps
{
    /// <summary>
    /// Gump displayed to players for resurrection with cost options
    /// </summary>
    public class ResurrectCostGump : Gump
    {
        #region Fields

        private int m_Price;
        private int m_Healer;
        private int m_Bank;
        private int m_ResurrectType;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of ResurrectCostGump
        /// </summary>
        /// <param name="owner">The mobile to show the gump to</param>
        /// <param name="healer">The healer type</param>
        public ResurrectCostGump(Mobile owner, int healer) : base(ResurrectionGumpConstants.GUMP_POS_X_COST, ResurrectionGumpConstants.GUMP_POS_Y_COST)
        {
            if (!(owner is PlayerMobile))
                return;

            PlayerMobile player = (PlayerMobile)owner;

            // Handle SoulBound players
            if (player.SoulBound)
            {
                player.ResetPlayer(owner);
                owner.CloseGump(typeof(ResurrectCostGump));
                return;
            }

            m_Healer = healer;
            m_Price = GetPlayerInfo.GetResurrectCost(owner);
            m_Bank = Banker.GetBalance(owner);
            m_ResurrectType = ResurrectionGumpConstants.RESURRECT_TYPE_NONE;

            // Calculate penalty
            double penalty = ResurrectionGumpHelper.CalculatePenaltyForResurrectCost(owner.Karma);

            // Build message
            string sText = ResurrectionGumpHelper.BuildResurrectCostMessage(player, healer, m_Price, m_Bank, penalty, out m_ResurrectType);

            // Get random grave message
            string sGrave = ResurrectionGumpHelper.GetRandomGraveMessageCost();

            // Setup gump properties
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            // Format bank gold
            string formattedBankGold;
            bool useKNotation;
            bool useKKNotation;
            ResurrectionGumpHelper.FormatBankGold(m_Bank, out formattedBankGold, out useKNotation, out useKKNotation);

            AddPage(0);

            // Add gump layout (images)
            AddResurrectionGumpLayout();

            // Add buttons
            AddButton(101, 365, 4023, 4023, 1, GumpButtonType.Reply, 0);
            AddHtml(101 + ResurrectionGumpConstants.BUTTON_LABEL_OFFSET, 365 + 2, 477, 22, 
                @"<BODY><BASEFONT Color=#5eff00><BIG> " + ResurrectionGumpStringConstants.BUTTON_RESURRECT_ME + " </BIG></BASEFONT></BODY>", false, false);
            AddButton(307, 365, 4020, 4020, 2, GumpButtonType.Reply, 0);
            AddHtml(307 + ResurrectionGumpConstants.BUTTON_LABEL_OFFSET, 365 + 2, 477, 22, 
                @"<BODY><BASEFONT Color=#FF0000><BIG> " + ResurrectionGumpStringConstants.BUTTON_NO + " </BIG></BASEFONT></BODY>", false, false);

            // Add tribute and bank gold labels (always show, except for shrine)
            if (m_Healer != ResurrectionGumpConstants.HEALER_TYPE_SHRINE)
            {
                AddHtml(101, 291, 190, 22, 
                    @"<BODY><BASEFONT Color=#FCFF00><BIG>" + ResurrectionGumpStringConstants.LABEL_TRIBUTE + "</BIG></BASEFONT></BODY>", false, false);
                AddHtml(307, 291, 196, 22, 
                    @"<BODY><BASEFONT Color=#ffffff><BIG>" + string.Format(ResurrectionGumpStringConstants.FORMAT_GOLD_AMOUNT, m_Price) + "</BIG></BASEFONT></BODY>", false, false);

                AddHtml(101, 325, 190, 22, 
                    @"<BODY><BASEFONT Color=#FCFF00><BIG>" + ResurrectionGumpStringConstants.LABEL_BANK_GOLD + "</BIG></BASEFONT></BODY>", false, false);
                
                if (useKNotation)
                {
                    AddHtml(307, 325, 196, 22, 
                        @"<BODY><BASEFONT Color=#ffffff><BIG>" + string.Format(ResurrectionGumpStringConstants.FORMAT_GOLD_K, formattedBankGold) + "</BIG></BASEFONT></BODY>", false, false);
                }
                else if (useKKNotation)
                {
                    AddHtml(307, 325, 196, 22, 
                        @"<BODY><BASEFONT Color=#ffffff><BIG>" + string.Format(ResurrectionGumpStringConstants.FORMAT_GOLD_KK, formattedBankGold) + "</BIG></BASEFONT></BODY>", false, false);
                }
                else
                {
                    AddHtml(307, 325, 156, 22, 
                        @"<BODY><BASEFONT Color=#ffffff><BIG>" + string.Format(ResurrectionGumpStringConstants.FORMAT_GOLD_AMOUNT, m_Bank) + "</BIG></BASEFONT></BODY>", false, false);
                }
            }

            // Add grave message and main text
            AddHtml(177, 90, 400, 22, 
                @"<BODY><BASEFONT Color=#fff700><BIG><CENTER>" + sGrave + "</CENTER></BIG></BASEFONT></BODY>", false, false);
            AddHtml(100, 155, 477, 123, 
                @"<BODY><BASEFONT Color=#ffffff><BIG>" + sText + "</BIG></BASEFONT></BODY>", false, false);
        }

        #endregion

        #region Gump Layout

        /// <summary>
        /// Adds the standard resurrection gump layout (images)
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
            Mobile from = state.Mobile;

            from.CloseGump(typeof(ResurrectCostGump));

            // Handle cancellation (No button or closed via X button)
            if (info.ButtonID == 0 || info.ButtonID == 2)
            {
                if (info.ButtonID == 2) // No button
                {
                    from.SendMessage(ResurrectionGumpConstants.MSG_COLOR_WARNING, ResurrectionGumpStringConstants.MSG_REMAIN_SPIRITUAL_REALM);
                }
                
                // Record cancellation time to prevent immediate re-showing (applies to both No button and X button close)
                Server.Mobiles.BaseHealer.RecordGumpCancellation(from);
                
                return;
            }

            if (info.ButtonID == 1) // Yes/Resurrect button
            {
                // Check if can fit at location
                if (from.Map == null || !from.Map.CanFit(from.Location, ResurrectionGumpConstants.MAP_FIT_CHECK_SIZE, false, false))
                {
                    from.SendMessage(ResurrectionGumpConstants.MSG_COLOR_WARNING, ResurrectionGumpStringConstants.MSG_CANNOT_RESURRECT_HERE);
                    return;
                }

                PlayerMobile player = from as PlayerMobile;
                bool hasEnoughGold = m_Bank >= m_Price;

                // IMPORTANT: If player has enough gold for current resurrection, stop any active delay timer
                // and ensure immediate resurrection with no delay and no new debit
                if (player != null && hasEnoughGold && ResurrectionGumpHelper.HasActiveResurrectionDelay(player))
                {
                    ResurrectionGumpHelper.StopResurrectionDelay(player);
                }

                // Case 1: Player does NOT have enough money for resurrection (less than 300gp)
                if (!hasEnoughGold)
                {
                    // Player chose to resurrect without payment
                    if (m_ResurrectType == ResurrectionGumpConstants.RESURRECT_TYPE_NO_GOLD || m_ResurrectType == ResurrectionGumpConstants.RESURRECT_TYPE_SHRINE)
                    {
                        // Check if player already has an active delay timer
                        if (player != null && ResurrectionGumpHelper.HasActiveResurrectionDelay(player))
                        {
                            // Timer is already running - don't start a new one
                            return;
                        }

                        // ALWAYS apply delay timer when player has no money and chooses to resurrect without payment
                        // Delay is based on NEW debit count (current debits + 1) since Penalty will add a new debit
                        if (player != null)
                        {
                            // Calculate delay based on NEW debit count (current + 1)
                            int newDebitCount = player.ResurrectionDebits + 1;
                            int requiredDelay = ResurrectionGumpHelper.CalculateResurrectionDelay(newDebitCount);
                            
                            // Start the delay timer system (particles, freeze, periodic checks)
                            ResurrectionGumpHelper.StartResurrectionDelay(player, requiredDelay);
                            
                            // Return - player will be resurrected when timer expires
                            // The timer will call Penalty and Resurrect when delay completes
                            return;
                        }
                    }
                    else
                    {
                        // Player doesn't have enough gold and can't resurrect without payment
                        from.SendMessage(ResurrectionGumpConstants.MSG_COLOR_WARNING, ResurrectionGumpStringConstants.MSG_REMAIN_SPIRITUAL_REALM);
                        return;
                    }
                }
                // Case 2: Player HAS enough money (at least 300gp for current resurrection)
                // IMPORTANT: Immediate resurrection, NO delay, NO new debit, old debits remain if not paid
                else
                {
                    // Ensure no delay timer is active (safety check)
                    if (player != null && ResurrectionGumpHelper.HasActiveResurrectionDelay(player))
                    {
                        ResurrectionGumpHelper.StopResurrectionDelay(player);
                    }

                    // Player chose to resurrect with payment
                    if (m_ResurrectType == ResurrectionGumpConstants.RESURRECT_TYPE_WITH_GOLD)
                    {
                        if (player != null && AetherGlobe.EvilChamp != player && AetherGlobe.GoodChamp != player)
                        {
                            // Calculate total cost: (debts * 300) + (new ress 300)
                            int totalDebt = ResurrectionGumpHelper.CalculateDebtGold(player.ResurrectionDebits);
                            int totalCost = m_Price + totalDebt;
                            int bankBalance = Banker.GetBalance(from);
                            
                            // Payment logic: Pay resurrection + as many full debts as possible (partial debt payment allowed)
                            int actualPayment = 0;
                            int debtsPaid = 0;
                            
                            if (bankBalance >= totalCost)
                            {
                                // Can pay all debts + resurrection
                                actualPayment = totalCost;
                                debtsPaid = player.ResurrectionDebits;
                                player.ResurrectionDebits = 0;
                            }
                            else if (bankBalance >= m_Price)
                            {
                                // Pay resurrection cost first, then pay as many full debts as possible
                                int remainingAfterRes = bankBalance - m_Price;
                                
                                // Calculate how many full debts can be paid (each debt = 300gp)
                                debtsPaid = remainingAfterRes / ResurrectionGumpConstants.DEBT_GOLD_PER_DEBIT;
                                
                                // Total payment = resurrection + debts paid
                                actualPayment = m_Price + (debtsPaid * ResurrectionGumpConstants.DEBT_GOLD_PER_DEBIT);
                                
                                // Update remaining debits
                                player.ResurrectionDebits = Math.Max(0, player.ResurrectionDebits - debtsPaid);
                                
                            }
                            else
                            {
                                // Not enough even for resurrection (shouldn't happen since hasEnoughGold is true, but safety check)
                                from.SendMessage(ResurrectionGumpConstants.MSG_COLOR_WARNING, 
                                    "Você não tem dinheiro suficiente no banco.");
                                return;
                            }

                            // Withdraw payment
                            Banker.Withdraw(from, actualPayment);
                            from.SendLocalizedMessage(1060398, actualPayment.ToString()); // ~1_AMOUNT~ gold has been withdrawn from your bank box.
                            from.SendLocalizedMessage(1060022, Banker.GetBalance(from).ToString()); // You have ~1_AMOUNT~ gold in cash remaining in your bank box.

                            if (debtsPaid > 0)
                            {
                                from.SendMessage(ResurrectionGumpConstants.MSG_COLOR_WARNING, 
                                    string.Format(ResurrectionGumpStringConstants.MSG_DEBTS_PAID_FORMAT, debtsPaid * ResurrectionGumpConstants.DEBT_GOLD_PER_DEBIT));
                            }

                            if (player.ResurrectionDebits > 0)
                            {
                                from.SendMessage(ResurrectionGumpConstants.MSG_COLOR_WARNING, 
                                    string.Format(ResurrectionGumpStringConstants.MSG_DEBTS_REMAINING_FORMAT, 
                                        player.ResurrectionDebits, ResurrectionGumpHelper.CalculateDebtGold(player.ResurrectionDebits)));
                            }

                            // Healer says ludic message
                            from.PublicOverheadMessage(Server.Network.MessageType.Regular, 0x3B2, false, 
                                ResurrectionGumpStringConstants.MSG_COMEBACK_FROM_DEAD);
                        }

                        // NO new debit when paying - Penalty called with false
                        Server.Misc.Death.Penalty(from, false);
                    }
                    else if (m_ResurrectType == ResurrectionGumpConstants.RESURRECT_TYPE_SHRINE)
                    {
                        // Shrine resurrection (free)
                        Server.Misc.Death.Penalty(from, false, true);
                    }
                    else
                    {
                        // Free resurrection (price = 0)
                        Server.Misc.Death.Penalty(from, false);
                    }

                    // IMMEDIATE resurrection - no delay, no timer
                    from.Resurrect();
                    bool hasDebits = (player != null && player.ResurrectionDebits > 0);
                    ResurrectionGumpHelper.ApplyResurrectionEffects(from, hasDebits);
                }

                // Clear criminal flag if set
                if (from.Criminal)
                {
                    from.Criminal = false;
                }
            }
        }

        #endregion
    }
}

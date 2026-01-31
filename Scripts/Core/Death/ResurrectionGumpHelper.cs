using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Misc
{
    /// <summary>
    /// Helper class for resurrection gump calculations and utilities.
    /// Extracted from ResurrectNowGump.cs and ResurrectCostGump.cs to improve code organization and reusability.
    /// </summary>
    public static class ResurrectionGumpHelper
    {
        #region Static Data

        /// <summary>
        /// Tracks active resurrection delay timers for players
        /// </summary>
        private static Dictionary<Mobile, ResurrectionDelayTimer> m_ActiveDelayTimers = new Dictionary<Mobile, ResurrectionDelayTimer>();

        #endregion
        #region Penalty Calculation

        /// <summary>
        /// Calculates penalty for ResurrectNowGump based on karma and balance level
        /// </summary>
        /// <param name="karma">Player's karma value</param>
        /// <returns>Penalty value (0.0 to 0.999)</returns>
        public static double CalculatePenaltyForResurrectNow(int karma)
        {
            double penalty;

            if (karma >= 0)
            {
                penalty = ((DeathPenaltyConstants.PERCENTAGE_BASE_DOUBLE - 
                    ((double)AetherGlobe.BalanceLevel / DeathPenaltyConstants.BALANCE_LEVEL_MAX) * 
                    ((double)karma / DeathPenaltyConstants.KARMA_DIVISOR))) / 
                    DeathPenaltyConstants.PERCENTAGE_BASE_DOUBLE;
            }
            else
            {
                penalty = ((DeathPenaltyConstants.PERCENTAGE_BASE_DOUBLE - 
                    ((double)(DeathPenaltyConstants.BALANCE_LEVEL_MAX - AetherGlobe.BalanceLevel) / DeathPenaltyConstants.BALANCE_LEVEL_MAX) * 
                    ((double)Math.Abs(karma) / DeathPenaltyConstants.KARMA_DIVISOR))) / 
                    DeathPenaltyConstants.PERCENTAGE_BASE_DOUBLE;
            }

            if (penalty >= ResurrectionGumpConstants.PENALTY_CAP_RESURRECT_NOW)
            {
                penalty = ResurrectionGumpConstants.PENALTY_CAP_RESURRECT_NOW;
            }

            return penalty;
        }

        /// <summary>
        /// Calculates penalty for ResurrectCostGump based on karma and balance level
        /// </summary>
        /// <param name="karma">Player's karma value (will be adjusted if below minimum)</param>
        /// <returns>Penalty value (0.1 to 0.66)</returns>
        public static double CalculatePenaltyForResurrectCost(int karma)
        {
            int adjustedKarma = Math.Abs(karma);
            if (adjustedKarma < ResurrectionGumpConstants.MIN_KARMA_FOR_COST_GUMP)
            {
                adjustedKarma = ResurrectionGumpConstants.MIN_KARMA_FOR_COST_GUMP;
            }

            double penalty;

            if (karma >= 0)
            {
                penalty = (((double)AetherGlobe.BalanceLevel / DeathPenaltyConstants.BALANCE_LEVEL_MAX) * 
                    ((double)adjustedKarma / DeathPenaltyConstants.KARMA_DIVISOR)) / 
                    DeathPenaltyConstants.BALANCE_CALCULATION_DIVISOR;
            }
            else
            {
                penalty = (((double)(DeathPenaltyConstants.BALANCE_LEVEL_MAX - AetherGlobe.BalanceLevel) / DeathPenaltyConstants.BALANCE_LEVEL_MAX) * 
                    ((double)adjustedKarma / DeathPenaltyConstants.KARMA_DIVISOR)) / 
                    DeathPenaltyConstants.BALANCE_CALCULATION_DIVISOR;
            }

            if (penalty < ResurrectionGumpConstants.MIN_PENALTY_CAP_COST_GUMP)
            {
                penalty = ResurrectionGumpConstants.MIN_PENALTY_CAP_COST_GUMP;
            }

            return penalty;
        }

        #endregion

        #region Stat Calculations

        /// <summary>
        /// Gets the total of all stats (Str + Dex + Int)
        /// </summary>
        /// <param name="from">The mobile to calculate stats for</param>
        /// <returns>Total stat value</returns>
        public static int GetTotalStats(Mobile from)
        {
            return from.RawStr + from.RawDex + from.RawInt;
        }

        /// <summary>
        /// Checks if player has weak stats (total < 125)
        /// </summary>
        /// <param name="from">The mobile to check</param>
        /// <returns>True if total stats < 125</returns>
        public static bool IsWeakPlayer(Mobile from)
        {
            return GetTotalStats(from) < DeathPenaltyConstants.TOTAL_STATS_EXEMPTION_THRESHOLD;
        }

        #endregion

        #region Bank Gold Formatting

        /// <summary>
        /// Formats bank gold amount with k/kk notation if appropriate
        /// </summary>
        /// <param name="bankGold">The bank gold amount</param>
        /// <param name="formattedAmount">Output parameter: formatted amount as string</param>
        /// <param name="useKNotation">Output parameter: true if using 'k' notation</param>
        /// <param name="useKKNotation">Output parameter: true if using 'kk' notation</param>
        public static void FormatBankGold(int bankGold, out string formattedAmount, out bool useKNotation, out bool useKKNotation)
        {
            double bankGoldDouble = bankGold;
            useKNotation = false;
            useKKNotation = false;

            if (bankGoldDouble > ResurrectionGumpConstants.K_NOTATION_THRESHOLD)
            {
                if (bankGoldDouble > ResurrectionGumpConstants.KK_NOTATION_THRESHOLD)
                {
                    useKKNotation = true;
                    bankGoldDouble /= ResurrectionGumpConstants.KK_DIVISOR;
                }
                else
                {
                    useKNotation = true;
                    bankGoldDouble /= ResurrectionGumpConstants.K_DIVISOR;
                }

                bankGoldDouble = Math.Round(bankGoldDouble, ResurrectionGumpConstants.ROUNDING_DECIMALS);
            }

            formattedAmount = bankGoldDouble.ToString();
        }

        #endregion

        #region Resurrection Effects

        /// <summary>
        /// Applies standard resurrection effects (sound, visual effect, stats, hidden)
        /// </summary>
        /// <param name="from">The mobile being resurrected</param>
        /// <param name="hasDebits">True if player has resurrection debits (reduces stat restoration)</param>
        public static void ApplyResurrectionEffects(Mobile from, bool hasDebits = false)
        {
            from.PlaySound(ResurrectionGumpConstants.SOUND_RESURRECTION);
            from.FixedEffect(ResurrectionGumpConstants.EFFECT_RESURRECTION, 
                ResurrectionGumpConstants.EFFECT_SPEED, 
                ResurrectionGumpConstants.EFFECT_DURATION);

            // If player has debits, restore minimum stats (1 hit, 1 stam, 1 mana)
            // Otherwise restore full stats
            if (hasDebits)
            {
                from.Hits = Math.Max(1, from.HitsMax / 10); // 10% of max or 1, whichever is higher
                from.Stam = Math.Max(1, from.StamMax / 10);
                from.Mana = Math.Max(1, from.ManaMax / 10);
            }
            else
            {
                from.Hits = from.HitsMax;
                from.Stam = from.StamMax;
                from.Mana = from.ManaMax;
            }

            from.Hidden = true;
        }

        #endregion

        #region Grave Messages

        /// <summary>
        /// Gets a random grave message for ResurrectNowGump
        /// </summary>
        /// <returns>Random grave message string</returns>
        public static string GetRandomGraveMessageNow()
        {
            switch (Utility.RandomMinMax(ResurrectionGumpConstants.GRAVE_MESSAGE_MIN, ResurrectionGumpConstants.GRAVE_MESSAGE_MAX))
            {
                case 0:
                    return ResurrectionGumpStringConstants.GRAVE_MSG_NOW_1;
                case 1:
                    return ResurrectionGumpStringConstants.GRAVE_MSG_NOW_2;
                case 2:
                    return ResurrectionGumpStringConstants.GRAVE_MSG_NOW_3;
                case 3:
                    return ResurrectionGumpStringConstants.GRAVE_MSG_NOW_4;
                default:
                    return ResurrectionGumpStringConstants.GRAVE_MSG_NOW_1;
            }
        }

        /// <summary>
        /// Gets a random grave message for ResurrectCostGump
        /// </summary>
        /// <returns>Random grave message string</returns>
        public static string GetRandomGraveMessageCost()
        {
            switch (Utility.RandomMinMax(ResurrectionGumpConstants.GRAVE_MESSAGE_MIN, ResurrectionGumpConstants.GRAVE_MESSAGE_MAX))
            {
                case 0:
                    return ResurrectionGumpStringConstants.GRAVE_MSG_COST_1;
                case 1:
                    return ResurrectionGumpStringConstants.GRAVE_MSG_COST_2;
                case 2:
                    return ResurrectionGumpStringConstants.GRAVE_MSG_COST_3;
                case 3:
                    return ResurrectionGumpStringConstants.GRAVE_MSG_COST_4;
                default:
                    return ResurrectionGumpStringConstants.GRAVE_MSG_COST_1;
            }
        }

        #endregion

        #region Healer Information

        /// <summary>
        /// Gets the healer name based on healer type
        /// </summary>
        /// <param name="healerType">The healer type constant</param>
        /// <returns>Healer name string</returns>
        public static string GetHealerName(int healerType)
        {
            switch (healerType)
            {
                case ResurrectionGumpConstants.HEALER_TYPE_SHRINE:
                    return "santuário";
                case ResurrectionGumpConstants.HEALER_TYPE_AZRAEL:
                    return "Azrael";
                case ResurrectionGumpConstants.HEALER_TYPE_REAPER:
                    return "Reaper";
                case ResurrectionGumpConstants.HEALER_TYPE_GODDESS_SEA:
                    return "Anfitrite";
                default:
                    return "curandeiro";
            }
        }

        #endregion

        #region Debit System

        /// <summary>
        /// Calculates the resurrection delay in seconds based on debit count
        /// </summary>
        /// <param name="debitCount">Current debit count (will be incremented, so use current + 1)</param>
        /// <returns>Delay in seconds (capped at max)</returns>
        public static int CalculateResurrectionDelay(int debitCount)
        {
            int delay = debitCount * ResurrectionGumpConstants.DEBIT_DELAY_BASE_SECONDS;
            if (delay > ResurrectionGumpConstants.DEBIT_DELAY_MAX_SECONDS)
            {
                delay = ResurrectionGumpConstants.DEBIT_DELAY_MAX_SECONDS;
            }
            return delay;
        }

        /// <summary>
        /// Calculates the total debt gold amount based on debit count
        /// </summary>
        /// <param name="debitCount">Current debit count</param>
        /// <returns>Total debt in gold pieces</returns>
        public static int CalculateDebtGold(int debitCount)
        {
            return debitCount * ResurrectionGumpConstants.DEBT_GOLD_PER_DEBIT;
        }

        /// <summary>
        /// Checks if player can be resurrected now (delay has passed)
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="lastResurrectionTime">Time of last resurrection</param>
        /// <returns>True if delay has passed, false if still waiting</returns>
        public static bool CanResurrectNow(PlayerMobile player, DateTime lastResurrectionTime)
        {
            if (player.ResurrectionDebits == 0)
                return true;

            int delaySeconds = CalculateResurrectionDelay(player.ResurrectionDebits);
            TimeSpan timeSinceLastRes = DateTime.UtcNow - lastResurrectionTime;
            return timeSinceLastRes.TotalSeconds >= delaySeconds;
        }

        /// <summary>
        /// Gets the remaining delay time in seconds before resurrection is allowed
        /// Note: Timer starts when player chooses to resurrect. Delay is based on CURRENT debits.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="lastResurrectionTime">Time when player chose to resurrect (timer start time)</param>
        /// <returns>Remaining seconds until resurrection is allowed (0 if ready)</returns>
        public static int GetRemainingDelaySeconds(PlayerMobile player, DateTime lastResurrectionTime)
        {
            if (player.ResurrectionDebits == 0)
                return 0;

            // If lastResurrectionTime is MinValue, timer hasn't started yet
            // This shouldn't happen if called correctly, but return 0 to allow resurrection
            if (lastResurrectionTime == DateTime.MinValue)
            {
                return 0;
            }

            // Calculate delay based on CURRENT debits (before incrementing)
            int delaySeconds = CalculateResurrectionDelay(player.ResurrectionDebits);
            TimeSpan timeSinceTimerStarted = DateTime.UtcNow - lastResurrectionTime;
            int remaining = delaySeconds - (int)timeSinceTimerStarted.TotalSeconds;
            return remaining > 0 ? remaining : 0;
        }

        #endregion

        #region Message Building

        /// <summary>
        /// Builds the resurrection message for ResurrectNowGump
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="healCost">The resurrection cost</param>
        /// <param name="bankGold">The bank gold amount</param>
        /// <param name="penalty">The calculated penalty (unused now, kept for compatibility)</param>
        /// <returns>Complete message string</returns>
        public static string BuildResurrectNowMessage(PlayerMobile player, int healCost, int bankGold, double penalty)
        {
            bool isWeak = IsWeakPlayer(player);
            bool isAvatar = player.Avatar;
            bool hasEnoughGold = bankGold >= healCost;

            string c2;

            string f1;
            string f2 = ResurrectionGumpStringConstants.MSG_ENOUGH_GOLD_SUGGESTION;
            string f3 = ResurrectionGumpStringConstants.MSG_CANNOT_PAY_TRIBUTE;

            if (isWeak)
            {
                // Weak player (< 125 stats)
                if (!isAvatar)
                {
                    c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_NORMAL_WEAK.ToString();
                }
                else
                {
                    c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_BASE.ToString();
                }

                f1 = string.Format(ResurrectionGumpStringConstants.MSG_BEG_GODS_FORMAT, c2);

                if (hasEnoughGold)
                {
                    return f1 + "\n" + f2;
                }
                else
                {
                    // Calculate delay and debt for next resurrection (current debits + 1)
                    int nextDebitCount = player.ResurrectionDebits + 1;
                    int delaySeconds = CalculateResurrectionDelay(nextDebitCount);
                    int debtGold = CalculateDebtGold(nextDebitCount);
                    string debtMessage = string.Format(ResurrectionGumpStringConstants.MSG_RESURRECTION_DEBT_FORMAT, delaySeconds, nextDebitCount, debtGold);
                    return f1 + " " + f3 + "\n" + debtMessage;
                }
            }
            else
            {
                // Strong player (>= 125 stats)
                if (!isAvatar)
                {
                    c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_NORMAL_STRONG.ToString();
                }
                else
                {
                    c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_BASE.ToString();
                }

                f1 = string.Format(ResurrectionGumpStringConstants.MSG_BEG_GODS_FORMAT, c2);

                if (hasEnoughGold)
                {
                    return f1 + "\n" + f2;
                }
                else
                {
                    // Calculate delay and debt for next resurrection (current debits + 1)
                    int nextDebitCount = player.ResurrectionDebits + 1;
                    int delaySeconds = CalculateResurrectionDelay(nextDebitCount);
                    int debtGold = CalculateDebtGold(nextDebitCount);
                    string debtMessage = string.Format(ResurrectionGumpStringConstants.MSG_RESURRECTION_DEBT_FORMAT, delaySeconds, nextDebitCount, debtGold);
                    return f1 + " " + f3 + "\n" + debtMessage;
                }
            }
        }

        /// <summary>
        /// Builds the resurrection message for ResurrectCostGump
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="healerType">The healer type</param>
        /// <param name="price">The resurrection price</param>
        /// <param name="bankGold">The bank gold amount</param>
        /// <param name="penalty">The calculated penalty</param>
        /// <param name="resurrectType">Output parameter: resurrection type (1=no gold, 2=with gold, 3=shrine)</param>
        /// <returns>Complete message string</returns>
        public static string BuildResurrectCostMessage(PlayerMobile player, int healerType, int price, int bankGold, double penalty, out int resurrectType)
        {
            resurrectType = ResurrectionGumpConstants.RESURRECT_TYPE_NONE;
            bool isWeak = IsWeakPlayer(player);
            bool isAvatar = player.Avatar;
            bool hasEnoughGold = bankGold >= price;

            string c2;
            string loss = ""; // Initialize to empty string (loss messages are currently disabled)

            // Build base loss message fragment (fame/karma only, no skill/stat penalties)
            // NOTE: Loss messages are currently disabled - loss is set to empty string
            /*
            if (isAvatar)
            {
                c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_BASE.ToString();
                loss = " " + string.Format(ResurrectionGumpStringConstants.MSG_FAME_KARMA_LOSS_FORMAT, c2);
            }
            else
            {
                c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_NORMAL_BASE.ToString();
                loss = " " + string.Format(ResurrectionGumpStringConstants.MSG_FAME_KARMA_LOSS_FORMAT, c2);
            }
            */
            // Handle case when price is 0 (free resurrection)
            if (price == 0)
            {
                string baseMessage;
                
                if (healerType < ResurrectionGumpConstants.HEALER_TYPE_SHRINE)
                {
                    baseMessage = ResurrectionGumpStringConstants.MSG_HEALER_BRING_BACK;
                }
                else
                {
                    switch (healerType)
                    {
                        case ResurrectionGumpConstants.HEALER_TYPE_AZRAEL:
                            baseMessage = ResurrectionGumpStringConstants.MSG_AZRAEL_BRING_BACK;
                            break;
                        case ResurrectionGumpConstants.HEALER_TYPE_REAPER:
                            baseMessage = ResurrectionGumpStringConstants.MSG_REAPER_BRING_BACK;
                            break;
                        case ResurrectionGumpConstants.HEALER_TYPE_GODDESS_SEA:
                            baseMessage = ResurrectionGumpStringConstants.MSG_GODDESS_SEA_BRING_BACK;
                            break;
                        default:
                            baseMessage = ResurrectionGumpStringConstants.MSG_GODS_BRING_BACK;
                            break;
                    }
                }
                
                // Add Young player message if applicable
                if (player.Young)
                {
                    return baseMessage + ResurrectionGumpStringConstants.MSG_YOUNG_FREE_RESURRECTION;
                }
                
                return baseMessage;
            }

            // Handle case when player doesn't have enough gold
            if (!hasEnoughGold)
            {
                if (!isAvatar)
                {
                    if (isWeak)
                    {
                        c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_NORMAL_NO_GOLD_WEAK.ToString();
                    }
                    else
                    {
                        c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_NORMAL_NO_GOLD_STRONG.ToString();
                    }
                }
                else
                {
                    c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_BASE.ToString();
                }

                string f1 =  "";//string.Format(ResurrectionGumpStringConstants.MSG_FAME_KARMA_LOSS_FORMAT, c2);
                string f3 = ResurrectionGumpStringConstants.MSG_NOT_ENOUGH_GOLD_FOR_HEALER;
                
                // Calculate delay and debt for next resurrection (current debits + 1)
                int nextDebitCount = player.ResurrectionDebits + 1;
                int delaySeconds = CalculateResurrectionDelay(nextDebitCount);
                int debtGold = CalculateDebtGold(nextDebitCount);
                string debtMessage = string.Format(ResurrectionGumpStringConstants.MSG_RESURRECTION_DEBT_COST_FORMAT, delaySeconds, nextDebitCount, debtGold);

                if (healerType < ResurrectionGumpConstants.HEALER_TYPE_SHRINE)
                {
                    // Regular healer
                    resurrectType = ResurrectionGumpConstants.RESURRECT_TYPE_NO_GOLD;
                    return f3 + " " + f1 + "\n" + debtMessage;
                }
                else if (healerType == ResurrectionGumpConstants.HEALER_TYPE_SHRINE)
                {
                    // Shrine
                    if (!isAvatar)
                    {
                        if (isWeak)
                        {
                            c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_NORMAL_NO_GOLD_WEAK.ToString();
                        }
                        else
                        {
                            c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_NORMAL_NO_GOLD_STRONG.ToString();
                        }
                    }
                    else
                    {
                        c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_AVATAR_SHRINE.ToString();
                    }

                    f1 = "";//string.Format(ResurrectionGumpStringConstants.MSG_FAME_KARMA_LOSS_FORMAT, c2);
                    string f4 = ResurrectionGumpStringConstants.MSG_SHRINE_NO_GOLD_REQUIRED;

                    resurrectType = ResurrectionGumpConstants.RESURRECT_TYPE_SHRINE;
                    return f4 + " " + f1 + "\n" + debtMessage;
                }
                else
                {
                    // Special healers (Azrael, Reaper, Goddess of Sea) - no gold
                    f1 = "";//string.Format(ResurrectionGumpStringConstants.MSG_FAME_KARMA_LOSS_FORMAT, c2);
                    resurrectType = ResurrectionGumpConstants.RESURRECT_TYPE_NO_GOLD;
                    return f3 + " " + f1 + "\n" + debtMessage;
                }
            }

            // Handle case when player has enough gold
            if (!isAvatar)
            {
                if (isWeak)
                {
                    c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_NORMAL_BASE.ToString();
                }
                else
                {
                    c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_NORMAL_NO_GOLD_WEAK.ToString();
                }
            }

            // Calculate debit information for players with enough gold
            string debitInfo = "";
            string remainingDebtsObs = "";
            if (player.ResurrectionDebits > 0)
            {
                int totalDebt = CalculateDebtGold(player.ResurrectionDebits);
                int totalCost = price + totalDebt;
                
                int debitsToPay = 0;
                string paymentType = "";
                
                if (bankGold >= totalCost)
                {
                    // Can pay all debits
                    debitsToPay = player.ResurrectionDebits;
                    paymentType = ResurrectionGumpStringConstants.MSG_DEBTS_FULL;
                }
                else if (bankGold >= price)
                {
                    // Can pay resurrection + some debits
                    int remainingAfterRes = bankGold - price;
                    debitsToPay = remainingAfterRes / ResurrectionGumpConstants.DEBT_GOLD_PER_DEBIT;
                    paymentType = ResurrectionGumpStringConstants.MSG_DEBTS_PARTIAL;
                    
                    // Calculate remaining debits after partial payment
                    int remainingDebits = player.ResurrectionDebits - debitsToPay;
                    if (remainingDebits > 0)
                    {
                        int remainingDebtGold = CalculateDebtGold(remainingDebits);
                        remainingDebtsObs = string.Format(ResurrectionGumpStringConstants.MSG_REMAINING_DEBTS_OBS_FORMAT,
                            remainingDebits,
                            remainingDebtGold);
                    }
                }
                
                if (debitsToPay > 0)
                {
                    debitInfo = string.Format(ResurrectionGumpStringConstants.MSG_DEBTS_INFO_FORMAT,
                        player.ResurrectionDebits,
                        totalDebt,
                        debitsToPay,
                        paymentType);
                }
            }

            if (healerType < ResurrectionGumpConstants.HEALER_TYPE_SHRINE)
            {
                // Regular healer with gold
                resurrectType = ResurrectionGumpConstants.RESURRECT_TYPE_WITH_GOLD;
                return ResurrectionGumpStringConstants.MSG_ENOUGH_GOLD_FOR_HEALER + loss + debitInfo + remainingDebtsObs;
            }
            else if (healerType == ResurrectionGumpConstants.HEALER_TYPE_SHRINE)
            {
                // Shrine with gold
                if (!isAvatar)
                {
                    if (isWeak)
                    {
                        c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_NORMAL_NO_GOLD_WEAK.ToString();
                    }
                    else
                    {
                        c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_NORMAL_NO_GOLD_STRONG.ToString();
                    }
                }
                else
                {
                    c2 = ResurrectionGumpConstants.FAME_KARMA_LOSS_BASE.ToString();
                }

                string f1 = "";//string.Format(ResurrectionGumpStringConstants.MSG_FAME_KARMA_LOSS_FORMAT, c2);
                string f4 = ResurrectionGumpStringConstants.MSG_SHRINE_NO_GOLD_REQUIRED;

                resurrectType = ResurrectionGumpConstants.RESURRECT_TYPE_SHRINE;
                return f4 + " " + f1 + debitInfo + remainingDebtsObs;
            }
            else
            {
                // Special healers with gold
                string specialMessage = "";
                switch (healerType)
                {
                    case ResurrectionGumpConstants.HEALER_TYPE_AZRAEL:
                        specialMessage = ResurrectionGumpStringConstants.MSG_AZRAEL_ENOUGH_GOLD;
                        break;
                    case ResurrectionGumpConstants.HEALER_TYPE_REAPER:
                        specialMessage = ResurrectionGumpStringConstants.MSG_REAPER_ENOUGH_GOLD;
                        break;
                    case ResurrectionGumpConstants.HEALER_TYPE_GODDESS_SEA:
                        specialMessage = ResurrectionGumpStringConstants.MSG_GODDESS_SEA_ENOUGH_GOLD;
                        break;
                }

                resurrectType = ResurrectionGumpConstants.RESURRECT_TYPE_WITH_GOLD;
                return specialMessage + loss + debitInfo + remainingDebtsObs;
            }
        }

        #endregion

        #region Resurrection Delay Timer System

        /// <summary>
        /// Starts the resurrection delay timer for a player
        /// </summary>
        /// <param name="player">The player mobile</param>
        /// <param name="delaySeconds">Total delay in seconds</param>
        public static void StartResurrectionDelay(PlayerMobile player, int delaySeconds)
        {
            if (player == null || player.Deleted || player.Alive)
                return;

            // Stop any existing timer
            StopResurrectionDelay(player);

            // Create and start new timer
            ResurrectionDelayTimer timer = new ResurrectionDelayTimer(player, delaySeconds);
            timer.Start();
            m_ActiveDelayTimers[player] = timer;

            // Freeze player (paralyze)
            player.Paralyzed = true;

            // Show initial particle effect
            player.FixedParticles(
                ResurrectionGumpConstants.RESURRECTION_DELAY_PARTICLE_EFFECT,
                1,
                30,
                9964,
                ResurrectionGumpConstants.RESURRECTION_DELAY_PARTICLE_HUE,
                3,
                EffectLayer.Head
            );

            // Show initial message
            player.SendMessage(
                ResurrectionGumpConstants.MSG_COLOR_WARNING,
                string.Format(ResurrectionGumpStringConstants.MSG_RESURRECTION_DELAY_START_FORMAT, delaySeconds)
            );
        }

        /// <summary>
        /// Stops the resurrection delay timer for a player
        /// </summary>
        /// <param name="player">The player mobile</param>
        public static void StopResurrectionDelay(PlayerMobile player)
        {
            if (player == null)
                return;

            if (m_ActiveDelayTimers.ContainsKey(player))
            {
                ResurrectionDelayTimer timer = m_ActiveDelayTimers[player];
                if (timer != null && timer.Running)
                {
                    timer.Stop();
                }
                m_ActiveDelayTimers.Remove(player);
            }

            // Unfreeze player
            if (player.Paralyzed)
            {
                player.Paralyzed = false;
            }
        }

        /// <summary>
        /// Checks if a player has an active resurrection delay timer
        /// </summary>
        /// <param name="player">The player mobile</param>
        /// <returns>True if timer is active</returns>
        public static bool HasActiveResurrectionDelay(PlayerMobile player)
        {
            if (player == null)
                return false;

            return m_ActiveDelayTimers.ContainsKey(player) && 
                   m_ActiveDelayTimers[player] != null && 
                   m_ActiveDelayTimers[player].Running;
        }

        /// <summary>
        /// Timer class for resurrection delay with particle effects and periodic checks
        /// </summary>
        private class ResurrectionDelayTimer : Timer
        {
            private PlayerMobile m_Player;
            private DateTime m_EndTime;
            private DateTime m_NextCheck;

            public ResurrectionDelayTimer(PlayerMobile player, int delaySeconds)
                : base(TimeSpan.FromSeconds(ResurrectionGumpConstants.RESURRECTION_DELAY_CHECK_INTERVAL_SECONDS),
                        TimeSpan.FromSeconds(ResurrectionGumpConstants.RESURRECTION_DELAY_CHECK_INTERVAL_SECONDS))
            {
                m_Player = player;
                m_EndTime = DateTime.UtcNow + TimeSpan.FromSeconds(delaySeconds);
                m_NextCheck = DateTime.UtcNow + TimeSpan.FromSeconds(ResurrectionGumpConstants.RESURRECTION_DELAY_CHECK_INTERVAL_SECONDS);
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if (m_Player == null || m_Player.Deleted || m_Player.Alive)
                {
                    // Player is gone or already alive - stop timer
                    if (m_Player != null)
                    {
                        StopResurrectionDelay(m_Player);
                    }
                    Stop();
                    return;
                }

                // Check if delay has expired
                if (DateTime.UtcNow >= m_EndTime)
                {
                    // Delay expired - complete resurrection
                    CompleteResurrection();
                    Stop();
                    return;
                }

                // Show periodic particle effect
                m_Player.FixedParticles(
                    ResurrectionGumpConstants.RESURRECTION_DELAY_PARTICLE_EFFECT,
                    1,
                    15,
                    9964,
                    ResurrectionGumpConstants.RESURRECTION_DELAY_PARTICLE_HUE,
                    0,
                    EffectLayer.Waist
                );

                // Show remaining time message every check interval
                if (DateTime.UtcNow >= m_NextCheck)
                {
                    int remainingSeconds = (int)(m_EndTime - DateTime.UtcNow).TotalSeconds;
                    if (remainingSeconds > 0)
                    {
                        m_Player.SendMessage(
                            ResurrectionGumpConstants.MSG_COLOR_WARNING,
                            string.Format(ResurrectionGumpStringConstants.MSG_RESURRECTION_DELAY_REMAINING_FORMAT, remainingSeconds)
                        );
                    }
                    m_NextCheck = DateTime.UtcNow + TimeSpan.FromSeconds(ResurrectionGumpConstants.RESURRECTION_DELAY_CHECK_INTERVAL_SECONDS);
                }
            }

            private void CompleteResurrection()
            {
                if (m_Player == null || m_Player.Deleted || m_Player.Alive)
                    return;

                // Show completion message
                m_Player.SendMessage(
                    ResurrectionGumpConstants.MSG_COLOR_WARNING,
                    ResurrectionGumpStringConstants.MSG_RESURRECTION_DELAY_COMPLETE
                );

                // Stop timer tracking
                StopResurrectionDelay(m_Player);

                // Apply resurrection
                Server.Misc.Death.Penalty(m_Player, true); // This will increment debits
                m_Player.Resurrect();
                
                bool hasDebits = (m_Player.ResurrectionDebits > 0);
                ApplyResurrectionEffects(m_Player, hasDebits);

                // Clear criminal flag if set
                if (m_Player.Criminal)
                {
                    m_Player.Criminal = false;
                }
            }
        }

        #endregion
    }
}



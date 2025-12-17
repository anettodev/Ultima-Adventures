using Server;
using Server.Items;
using Server.Mobiles;
using System;

namespace Server.Misc
{
    /// <summary>
    /// Handles death penalties for players on resurrection.
    /// Applies fame/karma loss based on player type and resurrection method.
    /// Stat and skill loss are currently disabled.
    /// </summary>
    public static class Death
    {
        #region Public Methods

        /// <summary>
        /// Applies death penalty to player (without ankh flag)
        /// </summary>
        /// <param name="from">The mobile to apply penalty to</param>
        /// <param name="allPenalty">True if player insta-ressed or doesn't have gold</param>
        public static void Penalty(Mobile from, bool allPenalty)
        {
            Penalty(from, allPenalty, false);
        }

        /// <summary>
        /// Applies death penalty to player with full options
        /// </summary>
        /// <param name="from">The mobile to apply penalty to</param>
        /// <param name="allPenalty">True if player insta-ressed or doesn't have gold</param>
        /// <param name="ankh">True if resurrected via ankh (less/no gold paid)</param>
        public static void Penalty(Mobile from, bool allPenalty, bool ankh)
        {
            // Validate and cast player
            PlayerMobile player = ValidatePlayer(from);
            if (player == null)
                return;

            // Skip penalty for SoulBound or Midland players
            if (ShouldSkipPenalty(player))
                return;

            // Calculate penalty rates
            double karmaLossRate = CalculateKarmaLossRate(player, allPenalty, ankh);
            double balanceEffect = CalculateBalanceEffect(player, allPenalty);

            // Apply fame and karma loss
            ApplyFameLoss(player, karmaLossRate);
            ApplyKarmaLoss(player, karmaLossRate);

            // Increment resurrection debits if player didn't pay (allPenalty = true)
            if (allPenalty)
            {
                player.ResurrectionDebits++;
            }

            // Check for exemptions and send appropriate messages
            ExemptionType exemptionType;
            if (IsExemptFromPenalty(player, out exemptionType))
            {
                SendExemptionMessage(player, exemptionType, balanceEffect);
                return;
            }

            // Send penalty message
            player.SendMessage(DeathPenaltyConstants.MSG_COLOR_WARNING, DeathPenaltyStringConstants.MSG_RESURRECTION_WEAKER);
        }

        #endregion

        #region Validation Methods

        /// <summary>
        /// Validates that the mobile is a PlayerMobile
        /// </summary>
        /// <param name="from">The mobile to validate</param>
        /// <returns>PlayerMobile instance or null if invalid</returns>
        private static PlayerMobile ValidatePlayer(Mobile from)
        {
            if (from is PlayerMobile)
            {
                return (PlayerMobile)from;
            }
            return null;
        }

        /// <summary>
        /// Checks if penalty should be skipped (SoulBound or Midland)
        /// </summary>
        /// <param name="player">The player to check</param>
        /// <returns>True if penalty should be skipped</returns>
        private static bool ShouldSkipPenalty(PlayerMobile player)
        {
            if (player.SoulBound)
            {
                player.ResetPlayer(player);
                return true;
            }

            if (AdventuresFunctions.IsInMidland(player))
                return true;

            return false;
        }

        /// <summary>
        /// Checks if player is exempt from penalties
        /// </summary>
        /// <param name="player">The player to check</param>
        /// <param name="exemptionType">Output parameter indicating exemption type</param>
        /// <returns>True if player is exempt</returns>
        private static bool IsExemptFromPenalty(PlayerMobile player, out ExemptionType exemptionType)
        {
            exemptionType = ExemptionType.None;

            // Check if player is a champion
            if (AetherGlobe.EvilChamp == player || AetherGlobe.GoodChamp == player)
            {
                if (player.Avatar)
                {
                    exemptionType = ExemptionType.Champ;
                    return true;
                }
            }

            // Check if player is weak (total stats < 125)
            int totalStats = GetTotalStats(player);
            if (totalStats < DeathPenaltyConstants.TOTAL_STATS_EXEMPTION_THRESHOLD)
            {
                if (player.Avatar && (AetherGlobe.EvilChamp != player && AetherGlobe.GoodChamp != player))
                {
                    exemptionType = ExemptionType.Weak;
                    return true;
                }
            }

            // Check if player is non-avatar
            if (!player.Avatar)
            {
                exemptionType = ExemptionType.NonAvatar;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the total of all stats (Str + Dex + Int)
        /// </summary>
        /// <param name="from">The mobile to calculate stats for</param>
        /// <returns>Total stat value</returns>
        private static int GetTotalStats(Mobile from)
        {
            return from.RawStr + from.RawDex + from.RawInt;
        }

        #endregion

        #region Calculation Methods

        /// <summary>
        /// Calculates the karma/fame loss rate based on player type and resurrection method
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="allPenalty">True if insta-ress or no gold</param>
        /// <param name="ankh">True if resurrected via ankh</param>
        /// <returns>Loss rate multiplier (0.05 to 0.80)</returns>
        private static double CalculateKarmaLossRate(PlayerMobile player, bool allPenalty, bool ankh)
        {
            double lossRate = DeathPenaltyConstants.BASE_KARMA_LOSS_RATE;

            // Apply normal player multiplier
            if (!player.Avatar)
            {
                lossRate *= DeathPenaltyConstants.NORMAL_PLAYER_MULTIPLIER;
            }

            // Apply allPenalty multiplier
            if (allPenalty)
            {
                if (!player.Avatar)
                {
                    lossRate *= DeathPenaltyConstants.NORMAL_PLAYER_ALL_PENALTY_MULTIPLIER;
                }
                else
                {
                    lossRate *= DeathPenaltyConstants.AVATAR_ALL_PENALTY_MULTIPLIER;
                }
            }
            else if (!player.Avatar)
            {
                // Normal players get double penalty even without allPenalty
                lossRate *= DeathPenaltyConstants.NORMAL_PLAYER_MULTIPLIER;
            }

            // Apply weak player reduction (half loss if total stats < 125)
            int totalStats = GetTotalStats(player);
            if (totalStats < DeathPenaltyConstants.TOTAL_STATS_EXEMPTION_THRESHOLD)
            {
                lossRate /= DeathPenaltyConstants.NORMAL_PLAYER_MULTIPLIER;
            }

            // Apply ankh multiplier
            if (ankh)
            {
                lossRate *= DeathPenaltyConstants.ANKH_MULTIPLIER;
            }

            // Apply minimum cap
            if (lossRate < DeathPenaltyConstants.MIN_KARMA_LOSS_RATE)
            {
                lossRate = DeathPenaltyConstants.MIN_KARMA_LOSS_RATE;
            }

            return lossRate;
        }

        /// <summary>
        /// Calculates the balance effect rate (currently unused but kept for future reference)
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="allPenalty">True if insta-ress or no gold</param>
        /// <returns>Balance effect rate</returns>
        private static double CalculateBalanceEffect(PlayerMobile player, bool allPenalty)
        {
            double balanceEffect = DeathPenaltyConstants.BASE_BALANCE_EFFECT;

            if (allPenalty)
            {
                balanceEffect *= DeathPenaltyConstants.BALANCE_EFFECT_MULTIPLIER;
            }

            return balanceEffect;
        }

        /// <summary>
        /// Applies fame loss to the player
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="lossRate">The loss rate multiplier</param>
        private static void ApplyFameLoss(PlayerMobile player, double lossRate)
        {
            if (player.Fame > 0)
            {
                int amount = (int)((double)player.Fame * lossRate);
                if (player.Fame - amount < 0)
                {
                    amount = player.Fame;
                }
                if (player.Fame < 1)
                {
                    player.Fame = 0;
                }
                Misc.Titles.AwardFame(player, -amount, true);
            }
        }

        /// <summary>
        /// Applies karma loss to the player
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="lossRate">The loss rate multiplier</param>
        private static void ApplyKarmaLoss(PlayerMobile player, double lossRate)
        {
            int amount = (int)((double)player.Karma * lossRate);
            
            // Handle positive karma
            if (player.Karma > 0 && (player.Karma - amount) < 0)
            {
                amount = player.Karma;
            }
            // Handle negative karma
            else if (player.Karma < 0 && (player.Karma - amount) > 0)
            {
                amount = player.Karma;
            }
            
            // Zero out karma if it's very close to zero
            if (player.Karma - 1 == 0 || player.Karma + 1 == 0)
            {
                player.Karma = 0;
            }
            
            Misc.Titles.AwardKarma(player, -amount, true);
        }

        #endregion

        #region Message Methods

        /// <summary>
        /// Sends appropriate exemption message based on exemption type
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="exemptionType">The type of exemption</param>
        /// <param name="balanceEffect">The balance effect rate (for champ players)</param>
        private static void SendExemptionMessage(PlayerMobile player, ExemptionType exemptionType, double balanceEffect)
        {
            switch (exemptionType)
            {
                case ExemptionType.Weak:
                    player.SendMessage(DeathPenaltyStringConstants.MSG_WEAK_PLAYER_EXEMPTION);
                    break;

                case ExemptionType.Champ:
                    player.SendMessage(DeathPenaltyStringConstants.MSG_CHAMP_EXEMPTION);
                    double lost = (double)player.BalanceEffect * balanceEffect;
                    player.BalanceEffect -= (int)lost;
                    player.SendMessage(string.Format(DeathPenaltyStringConstants.MSG_BALANCE_EFFECT_REDUCED_FORMAT, (int)lost));
                    break;

                case ExemptionType.NonAvatar:
                    player.SendMessage(DeathPenaltyConstants.MSG_COLOR_WARNING, DeathPenaltyStringConstants.MSG_NON_AVATAR_EXEMPTION);
                    break;
            }
        }

        #endregion

        #region Nested Types

        /// <summary>
        /// Types of exemptions from death penalties
        /// </summary>
        private enum ExemptionType
        {
            None,
            Weak,        // Total stats < 125
            Champ,       // EvilChamp or GoodChamp
            NonAvatar    // Non-avatar player
        }

        #endregion
    }
}

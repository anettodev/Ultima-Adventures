using Server;

namespace Server.Mobiles
{
    /// <summary>
    /// Helper class for handling Midland currency operations.
    /// Extracted from Banker.cs to eliminate code duplication between Withdraw and Deposit methods.
    /// Handles all race-based currency logic for Midland region players.
    /// </summary>
    public static class MidlandCurrencyHandler
    {
        /// <summary>
        /// Attempts to withdraw the specified amount from a Midland player's account
        /// </summary>
        /// <param name="player">The PlayerMobile to withdraw from</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <returns>True if withdrawal was successful, false otherwise</returns>
        public static bool WithdrawFromAccount(PlayerMobile player, int amount)
        {
            switch (player.midrace)
            {
                case BankerConstants.MIDLAND_RACE_HUMAN:
                    if (player.midhumanacc >= amount)
                    {
                        player.midhumanacc -= amount;
                        return true;
                    }
                    break;

                case BankerConstants.MIDLAND_RACE_GARGOYLE:
                    if (player.midgargoyleacc >= amount)
                    {
                        player.midgargoyleacc -= amount;
                        return true;
                    }
                    break;

                case BankerConstants.MIDLAND_RACE_LIZARD:
                    if (player.midlizardacc >= amount)
                    {
                        player.midlizardacc -= amount;
                        return true;
                    }
                    break;

                case BankerConstants.MIDLAND_RACE_PIRATE:
                    if (player.midpirateacc >= amount)
                    {
                        player.midpirateacc -= amount;
                        return true;
                    }
                    break;

                case BankerConstants.MIDLAND_RACE_ORC:
                    if (player.midorcacc >= amount)
                    {
                        player.midorcacc -= amount;
                        return true;
                    }
                    break;
            }

            return false;
        }

        /// <summary>
        /// Deposits the specified amount into a Midland player's account
        /// </summary>
        /// <param name="player">The PlayerMobile to deposit to</param>
        /// <param name="amount">The amount to deposit</param>
        /// <returns>True if deposit was successful, false otherwise</returns>
        public static bool DepositToAccount(PlayerMobile player, int amount)
        {
            switch (player.midrace)
            {
                case BankerConstants.MIDLAND_RACE_HUMAN:
                    player.midhumanacc += amount;
                    return true;

                case BankerConstants.MIDLAND_RACE_GARGOYLE:
                    player.midgargoyleacc += amount;
                    return true;

                case BankerConstants.MIDLAND_RACE_LIZARD:
                    player.midlizardacc += amount;
                    return true;

                case BankerConstants.MIDLAND_RACE_PIRATE:
                    player.midpirateacc += amount;
                    return true;

                case BankerConstants.MIDLAND_RACE_ORC:
                    player.midorcacc += amount;
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the currency name for a Midland race
        /// </summary>
        /// <param name="raceId">The race ID</param>
        /// <returns>The currency name, or empty string if invalid race</returns>
        public static string GetCurrencyName(int raceId)
        {
            switch (raceId)
            {
                case BankerConstants.MIDLAND_RACE_HUMAN: return "Sovereign";
                case BankerConstants.MIDLAND_RACE_GARGOYLE: return "Drachma";
                case BankerConstants.MIDLAND_RACE_LIZARD: return "Sslit";
                case BankerConstants.MIDLAND_RACE_PIRATE: return "Dubloon";
                case BankerConstants.MIDLAND_RACE_ORC: return "Skaal";
                default: return string.Empty;
            }
        }
    }
}

namespace Server.Mobiles
{
    /// <summary>
    /// Centralized constants for MidlandBanker NPC calculations and mechanics.
    /// Extracted from MidlandBanker.cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class MidlandBankerConstants
    {
        #region Speech Range Constants

        /// <summary>Range for deposit speech handling</summary>
        public const int DEPOSIT_SPEECH_RANGE = 6;

        /// <summary>Range for general speech handling</summary>
        public const int GENERAL_SPEECH_RANGE = 12;

        #endregion

        #region Currency Type Constants

        /// <summary>Human currency type ID</summary>
        public const int CURRENCY_HUMAN = 1;

        /// <summary>Gargoyle currency type ID</summary>
        public const int CURRENCY_GARGOYLE = 2;

        /// <summary>Lizard currency type ID</summary>
        public const int CURRENCY_LIZARD = 3;

        /// <summary>Pirate currency type ID</summary>
        public const int CURRENCY_PIRATE = 4;

        /// <summary>Orc currency type ID</summary>
        public const int CURRENCY_ORC = 5;

        #endregion

        #region Withdrawal Limits

        /// <summary>Maximum withdrawal amount in non-ML</summary>
        public const int MAX_WITHDRAWAL_NON_ML = 5000;

        /// <summary>Maximum withdrawal amount in ML</summary>
        public const int MAX_WITHDRAWAL_ML = 60000;

        #endregion

        #region Check Limits

        /// <summary>Minimum amount for bank check creation</summary>
        public const int MIN_CHECK_AMOUNT = 5000;

        /// <summary>Maximum amount for single bank check</summary>
        public const int MAX_CHECK_AMOUNT = 1000000;

        #endregion
    }
}

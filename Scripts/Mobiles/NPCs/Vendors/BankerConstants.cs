namespace Server.Mobiles
{
    /// <summary>
    /// Centralized constants for Banker NPC calculations and mechanics.
    /// Extracted from Banker.cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class BankerConstants
    {
        #region Karma Constants

        /// <summary>Minimum karma value for banker randomization</summary>
        public const int KARMA_MIN = 13;

        /// <summary>Maximum karma value for banker randomization</summary>
        public const int KARMA_MAX = -45;

        #endregion

        #region Speech Range Constants

        /// <summary>Range for speech handling</summary>
        public const int SPEECH_RANGE = 12;

        #endregion

        #region Gold/Bank Check Thresholds

        /// <summary>Minimum amount for bank check creation</summary>
        public const int MIN_CHECK_AMOUNT = 5000;

        /// <summary>Maximum amount for single bank check</summary>
        public const int MAX_CHECK_AMOUNT = 1000000;

        /// <summary>Maximum bank check amount allowed</summary>
        public const int MAX_TOTAL_CHECK_AMOUNT = 10000000;

        #endregion

        #region Withdrawal Limits

        /// <summary>Maximum withdrawal amount in non-ML</summary>
        public const int MAX_WITHDRAWAL_NON_ML = 5000;

        /// <summary>Maximum withdrawal amount in ML</summary>
        public const int MAX_WITHDRAWAL_ML = 60000;

        #endregion

        #region Midland Currency Thresholds

        /// <summary>Minimum deposit amount for Midland fee</summary>
        public const int MIDLAND_FEE_THRESHOLD = 10;

        /// <summary>Midland banking fee percentage (1%)</summary>
        public const double MIDLAND_FEE_PERCENTAGE = 0.01;

        #endregion

        #region Midland Race IDs

        /// <summary>Human race ID in Midland</summary>
        public const int MIDLAND_RACE_HUMAN = 1;

        /// <summary>Gargoyle race ID in Midland</summary>
        public const int MIDLAND_RACE_GARGOYLE = 2;

        /// <summary>Lizard race ID in Midland</summary>
        public const int MIDLAND_RACE_LIZARD = 3;

        /// <summary>Pirate race ID in Midland</summary>
        public const int MIDLAND_RACE_PIRATE = 4;

        /// <summary>Orc race ID in Midland</summary>
        public const int MIDLAND_RACE_ORC = 5;

        #endregion
    }
}

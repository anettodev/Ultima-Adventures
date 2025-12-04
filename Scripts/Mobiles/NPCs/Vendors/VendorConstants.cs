namespace Server.Mobiles
{
    /// <summary>
    /// Centralized constants for vendor NPCs.
    /// Extracted from various vendor files to improve maintainability and reduce code duplication.
    /// </summary>
    public static class VendorConstants
    {
        #region Karma Constants

        /// <summary>Minimum karma value for vendor randomization</summary>
        public const int KARMA_MIN = 13;

        /// <summary>Maximum karma value for vendor randomization</summary>
        public const int KARMA_MAX = -45;

        #endregion

        #region Order/Chaos Balance System Control

        /// <summary>
        /// Enable/disable Order/Chaos balance influence on vendor pricing.
        /// When false: Prices are not affected by GetGoldCutRate() or VendorCurse.
        /// When true: Prices are affected by world balance (Order/Chaos system).
        /// Set to false for simplified pricing system.
        /// </summary>
        public const bool ENABLE_ORDER_CHAOS_PRICING = false;

        #endregion
    }
}

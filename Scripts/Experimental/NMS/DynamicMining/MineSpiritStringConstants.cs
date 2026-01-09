namespace Server.Mobiles
{
    /// <summary>
    /// Centralized string constants for MineSpirit messages.
    /// Extracted from MineSpirit.cs to improve maintainability and enable localization.
    /// </summary>
    public static class MineSpiritStringConstants
    {
        #region Harvest Messages (Portuguese)

        /// <summary>Message when finding iron ore (fallback resource)</summary>
        public const string MSG_FOUND_IRON_ORE_FALLBACK = "Você encontrou alguns minérios de ferro.";

        /// <summary>Message format when finding custom ore type</summary>
        public const string MSG_FOUND_ORE_FORMAT = "Você encontrou alguns minérios de {0} e colocou em sua mochila.";

        #endregion
    }
}


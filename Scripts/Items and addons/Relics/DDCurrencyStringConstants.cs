using System;

namespace Server.Items
{
    /// <summary>
    /// Localized string constants for DD currency/treasure items (Portuguese-Brazilian).
    /// Extracted from DDRelicMoney.cs to improve maintainability and enable localization.
    /// </summary>
    public static class DDCurrencyStringConstants
    {
        #region Item Names (Portuguese-Brazilian)

        /// <summary>Name for copper coins in Portuguese</summary>
        public const string NAME_COPPER_COINS = "moeda(s) de cobre";

        /// <summary>Name for silver coins in Portuguese</summary>
        public const string NAME_SILVER_COINS = "moeda(s) de prata";

        /// <summary>Name for jewels in Portuguese</summary>
        public const string NAME_JEWELS = "joia(s)";

        /// <summary>Name for xormite coins in Portuguese</summary>
        public const string NAME_XORMITE_COINS = "moeda(s) de xormita";

        /// <summary>Name for gemstones in Portuguese</summary>
        public const string NAME_GEMSTONES = "pedra(s) preciosa(s)";

        /// <summary>Name for gold nuggets in Portuguese</summary>
        public const string NAME_GOLD_NUGGETS = "pepita(s) de ouro";

        #endregion

        #region Conversion Messages (Portuguese-Brazilian)

        /// <summary>Message when currency is converted to gold (format string with amount)</summary>
        public const string MSG_CONVERSION_SUCCESS = "Ouro foi depositado em sua conta: {0} moeda(s) de ouro.";

        #endregion
    }
}

using System;

namespace Server.Items
{
    /// <summary>
    /// Centralized constants for DD currency/treasure items.
    /// Extracted from DDRelicMoney.cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class DDCurrencyConstants
    {
        #region Conversion Rates

        /// <summary>Copper coins conversion rate (10 copper = 1 gold)</summary>
        public const int COPPER_CONVERSION_RATE = 10;

        /// <summary>Silver coins conversion rate (5 silver = 1 gold)</summary>
        public const int SILVER_CONVERSION_RATE = 5;

        /// <summary>Jewels conversion rate (1 jewel = 2 gold)</summary>
        public const int JEWEL_CONVERSION_RATE = 2;

        /// <summary>Xormite coins conversion rate (1 xormite = 3 gold)</summary>
        public const int XORMITE_CONVERSION_RATE = 3;

        /// <summary>Gemstones conversion rate (1 gemstone = 2 gold)</summary>
        public const int GEMSTONE_CONVERSION_RATE = 2;

        /// <summary>Gold nuggets conversion rate (1 nugget = 1 gold)</summary>
        public const int GOLD_NUGGET_CONVERSION_RATE = 1;

        #endregion

        #region Item Properties

        /// <summary>Item ID for coin-type items</summary>
        public const int COIN_ITEM_ID = 0xEF0;

        /// <summary>Item ID for gemstones</summary>
        public const int GEMSTONE_ITEM_ID = 0xE99;

        /// <summary>Item ID for gold nuggets</summary>
        public const int GOLD_NUGGET_ITEM_ID = 0x1BC8;

        /// <summary>Hue for copper coins</summary>
        public const int COPPER_HUE = 0x83E;

        /// <summary>Hue for silver coins</summary>
        public const int SILVER_HUE = 0x9C4;

        /// <summary>Hue for xormite coins</summary>
        public const int XORMITE_HUE = 0xB96;

        /// <summary>Base weight for currency items</summary>
        public const double BASE_WEIGHT = 0.02;

        /// <summary>Weight divisor for ML clients</summary>
        public const int ML_WEIGHT_DIVISOR = 3;

        #endregion

        #region Audio

        /// <summary>Drop sound for small stacks (1 item)</summary>
        public const int DROP_SOUND_SMALL = 0x2E4;

        /// <summary>Drop sound for medium stacks (2-5 items)</summary>
        public const int DROP_SOUND_MEDIUM = 0x2E5;

        /// <summary>Drop sound for large stacks (6+ items)</summary>
        public const int DROP_SOUND_LARGE = 0x2E6;

        #endregion

        #region Messages

        /// <summary>Error message when item is not in bank box</summary>
        public const int ERROR_NOT_IN_BANK_MESSAGE = 1047026;

        #endregion
    }
}

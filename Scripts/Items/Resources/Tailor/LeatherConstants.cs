namespace Server.Items
{
    /// <summary>
    /// Centralized constants for leather and hides items.
    /// Extracted from Leathers.cs and Hides.cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class LeatherConstants
    {
        #region Item IDs

        /// <summary>Item ID for leather items</summary>
        public const int ITEM_ID_LEATHER = 0x1081;

        /// <summary>Item ID for leather flipable variant</summary>
        public const int ITEM_ID_LEATHER_FLIPABLE = 0x1082;

        /// <summary>Item ID for hides items</summary>
        public const int ITEM_ID_HIDES = 0x1079;

        /// <summary>Item ID for hides flipable variant</summary>
        public const int ITEM_ID_HIDES_FLIPABLE = 0x1078;

        #endregion

        #region Weights

        /// <summary>Weight of leather items (in stones)</summary>
        public const double WEIGHT_LEATHER = 1.0;

        /// <summary>Weight of hides items (in stones)</summary>
        public const double WEIGHT_HIDES = 5.0;

        #endregion

        #region Defaults

        /// <summary>Default amount when creating items</summary>
        public const int DEFAULT_AMOUNT = 1;

        /// <summary>Amount of leather created when cutting hides</summary>
        public const int SCISSOR_AMOUNT = 1;

        /// <summary>Range check for scissor operations (in tiles)</summary>
        public const int SCISSOR_RANGE = 3;

        #endregion

        #region Serialization

        /// <summary>Current serialization version</summary>
        public const int SERIALIZATION_VERSION_CURRENT = 1;

        /// <summary>Legacy serialization version (for backward compatibility)</summary>
        public const int SERIALIZATION_VERSION_LEGACY = 0;

        #endregion

        #region Label Numbers (Cliloc)

        /// <summary>Label number for multiple items format</summary>
        public const int LABEL_MULTIPLE_ITEMS_FORMAT = 1050039;

        /// <summary>Label number for cut leather</summary>
        public const int LABEL_CUT_LEATHER = 1024199;

        /// <summary>Label number for pile of hides</summary>
        public const int LABEL_PILE_OF_HIDES = 1024216;

        /// <summary>Label number for property display format</summary>
        public const int LABEL_PROPERTY_DISPLAY = 1053099;

        /// <summary>Base label number for Spined leather</summary>
        public const int LABEL_SPINED_LEATHER_BASE = 1049684;

        /// <summary>Base label number for Spined hides</summary>
        public const int LABEL_SPINED_HIDES_BASE = 1049687;

        /// <summary>Label number for Dinosaur leather</summary>
        public const int LABEL_DINOSAUR_LEATHER = 1036113;

        /// <summary>Label number for Dinosaur hides</summary>
        public const int LABEL_DINOSAUR_HIDES = 1036112;

        /// <summary>Label number for Regular leather</summary>
        public const int LABEL_REGULAR_LEATHER = 1047022;

        /// <summary>Label number for Regular hides</summary>
        public const int LABEL_REGULAR_HIDES = 1047023;

        #endregion
    }
}


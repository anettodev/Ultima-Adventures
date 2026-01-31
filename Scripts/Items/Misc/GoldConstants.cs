namespace Server.Items
{
    /// <summary>
    /// Centralized constants for Gold item properties and behavior.
    /// Extracted from Gold.cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class GoldConstants
    {
        #region Item Properties

        /// <summary>Item ID for gold appearance</summary>
        public const int ITEM_ID = 0xEED;

        /// <summary>Light type for gold items</summary>
        public const LightType LIGHT_TYPE = LightType.Circle150;

        #endregion

        #region Weight Constants

        /// <summary>Weight per gold piece in legacy mode (0.02)</summary>
        public const double WEIGHT_LEGACY = 0.02;

        /// <summary>Divisor for ML weight calculation (3)</summary>
        public const int WEIGHT_ML_DIVISOR = 3;

        /// <summary>Weight per gold piece in ML mode (0.02 / 3)</summary>
        public const double WEIGHT_ML = WEIGHT_LEGACY / WEIGHT_ML_DIVISOR;

        #endregion

        #region Amount Limits

        /// <summary>Minimum valid gold amount</summary>
        public const int MIN_AMOUNT = 0;

        /// <summary>Maximum valid gold amount per stack (UO gold stack limit)</summary>
        public const int MAX_AMOUNT = 60000;

        /// <summary>Default amount when invalid amount is provided</summary>
        public const int DEFAULT_AMOUNT = 1;

        #endregion

        #region Drop Sound Constants

        /// <summary>Sound ID for dropping single gold piece (1 or less)</summary>
        public const int DROP_SOUND_SINGLE = 0x2E4;

        /// <summary>Sound ID for dropping small gold stack (2-5 pieces)</summary>
        public const int DROP_SOUND_SMALL = 0x2E5;

        /// <summary>Sound ID for dropping large gold stack (6+ pieces)</summary>
        public const int DROP_SOUND_LARGE = 0x2E6;

        #endregion

        #region Amount Thresholds

        /// <summary>Threshold for single gold piece sound (1 or less)</summary>
        public const int AMOUNT_THRESHOLD_SINGLE = 1;

        /// <summary>Threshold for small gold stack sound (2-5 pieces)</summary>
        public const int AMOUNT_THRESHOLD_SMALL = 5;

        #endregion

        #region Serialization

        /// <summary>Current serialization version</summary>
        public const int SERIALIZATION_VERSION = 0;

        #endregion
    }
}


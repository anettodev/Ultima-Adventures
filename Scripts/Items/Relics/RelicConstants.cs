namespace Server.Items
{
    /// <summary>
    /// Centralized constants for DDRelic items.
    /// Extracted from multiple DDRelic files to improve maintainability and reduce code duplication.
    /// </summary>
    public static class RelicConstants
    {
        #region Random Ranges

        /// <summary>Minimum value for quality descriptor selection</summary>
        public const int RANDOM_QUALITY_MIN = 0;

        /// <summary>Maximum value for quality descriptor selection</summary>
        public const int RANDOM_QUALITY_MAX = 18;

        /// <summary>Minimum value for decorative term selection</summary>
        public const int RANDOM_DECORATIVE_MIN = 0;

        /// <summary>Maximum value for decorative term selection (standard)</summary>
        public const int RANDOM_DECORATIVE_MAX = 3;

        /// <summary>Maximum value for decorative term selection (with ceremonial)</summary>
        public const int RANDOM_DECORATIVE_MAX_WITH_CEREMONIAL = 5;

        #endregion

        #region Weight Constants

        /// <summary>Very light weight (1.0)</summary>
        public const double WEIGHT_VERY_LIGHT = 1.0;

        /// <summary>Light weight (5.0)</summary>
        public const int WEIGHT_LIGHT = 5;

        /// <summary>Medium weight (10.0)</summary>
        public const int WEIGHT_MEDIUM = 10;

        /// <summary>Heavy weight (20.0)</summary>
        public const int WEIGHT_HEAVY = 20;

        /// <summary>Very heavy weight (30.0)</summary>
        public const int WEIGHT_VERY_HEAVY_30 = 30;

        /// <summary>Very heavy weight (40.0)</summary>
        public const int WEIGHT_VERY_HEAVY_40 = 40;

        /// <summary>Very heavy weight (60.0)</summary>
        public const int WEIGHT_VERY_HEAVY = 60;

        #endregion

        #region Vase Constants

        /// <summary>Vase selection random maximum</summary>
        public const int RANDOM_VASE_MAX = 8;

        /// <summary>Vase special weight (for large vases)</summary>
        public const int VASE_SPECIAL_WEIGHT = 40;

        /// <summary>Vase special value minimum</summary>
        public const int VASE_SPECIAL_VALUE_MIN = 20;

        /// <summary>Vase special value maximum</summary>
        public const int VASE_SPECIAL_VALUE_MAX = 150;

        #endregion

        #region Cloth Constants

        /// <summary>Cloth type selection random maximum</summary>
        public const int RANDOM_CLOTH_TYPE_MAX = 5;

        /// <summary>Cloth weight</summary>
        public const int WEIGHT_CLOTH = 30;

        #endregion

        #region Jewel Constants

        /// <summary>Jewel type selection random maximum</summary>
        public const int RANDOM_JEWEL_TYPE_MAX = 8;

        #endregion

        #region Fur Constants

        /// <summary>Fur type selection random maximum</summary>
        public const int RANDOM_FUR_TYPE_MAX = 38;

        /// <summary>Fur weight</summary>
        public const int WEIGHT_FUR = 40;

        #endregion

        #region Serialization

        /// <summary>Current serialization version</summary>
        public const int SERIALIZATION_VERSION = 0;

        #endregion

        #region Message Colors

        /// <summary>Error message color (red)</summary>
        public const int MSG_COLOR_ERROR = 55;

        #endregion
    }
}


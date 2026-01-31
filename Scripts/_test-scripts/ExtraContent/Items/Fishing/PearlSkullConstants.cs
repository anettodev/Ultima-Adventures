namespace Server.Items
{
    /// <summary>
    /// Centralized constants for PearlSkull mechanics.
    /// Extracted from PearlSkull.cs to improve maintainability.
    /// </summary>
    public static class PearlSkullConstants
    {
        #region Item IDs

        /// <summary>Base skull item ID</summary>
        public const int ITEM_ID_BASE = 0x1AE0;

        /// <summary>Skull item ID variant 1</summary>
        public const int ITEM_ID_SKULL_1 = 0x1AE1;

        /// <summary>Skull item ID variant 2</summary>
        public const int ITEM_ID_SKULL_2 = 0x1AE2;

        /// <summary>Skull item ID variant 3</summary>
        public const int ITEM_ID_SKULL_3 = 0x1AE3;

        /// <summary>Skull item ID variant 4</summary>
        public const int ITEM_ID_SKULL_4 = 0x1AE4;

        #endregion

        #region Random Ranges

        /// <summary>Minimum value for liquid descriptor selection</summary>
        public const int RANDOM_LIQUID_MIN = 0;

        /// <summary>Maximum value for liquid descriptor selection</summary>
        public const int RANDOM_LIQUID_MAX = 6;

        #endregion

        #region Weight

        /// <summary>Weight of pearl skull</summary>
        public const double WEIGHT_SKULL = 1.0;

        #endregion
    }
}


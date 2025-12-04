namespace Server.Items
{
    /// <summary>
    /// Centralized constants for TreasurePileAddon calculations and mechanics.
    /// Extracted from TreasurePileAddon.cs to improve maintainability and reduce code duplication.
    /// Supports 8 different treasure pile variations in a single generic system.
    /// </summary>
    public static class TreasurePileAddonConstants
    {
        #region Deed Constants
        /// <summary>ItemID for the treasure pile addon deed</summary>
        public const int DEED_ITEM_ID = 0x0E41;
        /// <summary>Weight of the treasure pile addon deed</summary>
        public const double DEED_WEIGHT = 50.0;
        #endregion

        #region Serialization Constants
        /// <summary>Current serialization version</summary>
        public const int SERIALIZATION_VERSION = 0;
        #endregion

        #region Variation Definitions
        /// <summary>Available treasure pile addon variations</summary>
        public enum TreasurePileVariation
        {
            /// <summary>Original 26-component treasure pile (main design)</summary>
            Original = 0,
            /// <summary>10-component compact treasure pile</summary>
            Compact01 = 1,
            /// <summary>27-component extended treasure pile</summary>
            Extended02 = 2,
            /// <summary>9-component minimal treasure pile</summary>
            Minimal03 = 3,
            /// <summary>33-component large treasure pile</summary>
            Large04 = 4,
            /// <summary>11-component standard treasure pile</summary>
            Standard05 = 5,
            /// <summary>13-component medium treasure pile</summary>
            Medium06 = 6,
            /// <summary>15-component balanced treasure pile</summary>
            Balanced07 = 7
        }

        /// <summary>Component count for each variation</summary>
        public static readonly int[] COMPONENT_COUNTS = {
            26, // Original
            10, // Compact01
            27, // Extended02
            9,  // Minimal03
            33, // Large04
            11, // Standard05
            13, // Medium06
            15  // Balanced07
        };
        #endregion

        #region Component Definitions
        /// <summary>
        /// Addon component definitions: {ItemID, X, Y, Z} for each component.
        /// Contains data for all 8 treasure pile variations in a unified array.
        /// Each variation's components are stored sequentially.
        /// </summary>
        public static readonly int[] COMPONENT_DATA = {
            // Original (26 components)
            6981, 3, 0, 1, 6988, -2, 1, 1, 6989, -2, 0, 1,    // 1-3
            6995, 1, 2, 1, 7003, 1, 3, 1, 7000, -1, 2, 1,    // 4-6
            7005, -1, 3, 1, 7001, 0, 1, 1, 6984, 2, 0, 1,    // 7-9
            6980, -1, 1, 1, 6999, -2, 2, 1, 7015, 0, 0, 1,    // 10-12
            6979, 0, 3, 1, 7002, 1, 1, 1, 6993, -1, 0, 1,    // 13-15
            6998, 0, 2, 1, 7017, 1, 0, 1, 7018, 1, -1, 1,    // 16-18
            7004, 2, 1, 3, 7013, 0, -1, 1, 7012, -2, 3, 1,    // 19-21
            7019, 1, -2, 1, 7011, -2, 3, 1, 7014, 0, -2, 1,    // 22-24
            6992, 1, -2, 1, 6996, 0, -2, 0,                      // 25-26

            // Compact01 (10 components) - TreasurePile01Addon
            6975, 1, 1, 0, 6976, 0, 1, 0, 6977, -1, 1, 0,     // 27-29
            6979, 0, 0, 0, 6978, -1, 0, 0, 6980, 1, 0, 0,     // 30-32
            6982, -1, -1, 0, 6983, 0, -1, 0, 6984, 1, -1, 0,   // 33-35

            // Extended02 (27 components) - TreasurePile2Addon
            6981, 3, -2, 0, 6988, -2, -1, 0, 6989, -2, -2, 0,  // 36-38
            6995, 1, 0, 0, 6996, 0, 2, 0, 6998, -1, 2, 0,     // 39-41
            7003, 1, 1, 0, 7000, -1, 0, 0, 7005, -1, 1, 0,    // 42-44
            7001, 0, -1, 0, 6984, 2, -2, 0, 6980, -1, -1, 0,  // 45-47
            6999, -2, 0, 0, 7015, 0, -2, 0, 6979, 0, 1, 0,    // 48-50
            7002, 1, -1, 0, 6993, -1, -2, 0, 6998, 0, 0, 0,   // 51-53
            7017, 1, -1, 0, 7010, -1, 3, 0, 7004, 2, -1, 2,   // 54-56
            7011, -2, 3, 0, 6992, -2, 2, 0, 7012, -2, 1, 0,   // 57-59
            7011, -2, 1, 0, 6977, -3, 3, 0, 6996, -2, 3, 0,    // 60-62

            // Minimal03 (9 components) - TreasurePile02Addon
            6986, 0, 1, 0, 6987, -1, 1, 0, 6988, -1, 0, 0,     // 63-65
            6991, 1, 0, 0, 6990, 0, 0, 0, 6993, 0, -1, 0,     // 66-68
            6989, -1, -1, 0, 6992, 1, -1, 0,                      // 69-70

            // Large04 (33 components) - TreasurePile3Addon
            6981, 3, -1, 1, 6988, -2, 0, 1, 6989, -2, -1, 1,   // 71-73
            6995, 1, 1, 1, 6996, 0, 3, 1, 6998, -1, 3, 1,     // 74-76
            7003, 1, 2, 1, 7000, -1, 1, 1, 7005, -1, 2, 1,    // 77-79
            7001, 0, 0, 1, 6984, 2, -1, 1, 6980, -1, 0, 1,    // 80-82
            6999, -2, 1, 1, 7015, 0, -1, 1, 6979, 0, 2, 1,    // 83-85
            7002, 1, 0, 1, 6993, -1, -1, 1, 6998, 0, 1, 1,    // 86-88
            7017, 1, -1, 1, 7010, -1, 4, 1, 7018, 1, -2, 1,   // 89-91
            7004, 2, 0, 3, 7013, 0, -2, 1, 7011, -2, 4, 1,    // 92-94
            6992, -2, 3, 1, 7012, -2, 2, 1, 7019, 1, -3, 1,   // 95-97
            7011, -2, 2, 1, 7014, 0, -3, 1, 6992, 1, -3, 1,   // 98-100
            6977, -3, 4, 1, 6996, -2, 4, 1, 6996, 0, -3, 0,    // 101-103

            // Standard05 (11 components) - TreasurePile03Addon
            6995, 0, 1, 0, 6997, -1, 1, 0, 6998, -1, 0, 0,     // 104-106
            6999, -1, -1, 0, 7000, 0, -1, 0, 7001, 0, 0, 0,    // 107-109
            7002, 1, -1, 0, 7003, 1, 0, 0, 6996, 1, 1, 0,      // 110-112

            // Medium06 (13 components) - TreasurePile04Addon
            7019, 2, -1, 0, 7018, 1, -1, 0, 7017, 0, -1, 0,     // 113-115
            7016, -1, -1, 0, 7015, -1, 0, 0, 7014, -2, 0, 0,    // 116-118
            7011, -1, 1, 0, 7010, 0, 1, 0, 7009, 0, 0, 0,      // 119-121
            7008, 1, 0, 0, 7007, 2, 0, 0,                          // 122-123

            // Balanced07 (15 components) - TreasurePile05Addon
            7017, 0, -1, 0, 7016, -1, -1, 0, 7015, -1, 0, 0,    // 124-126
            7014, -2, 0, 0, 7013, -2, -1, 0, 7012, -2, 1, 0,    // 127-129
            7011, -1, 1, 0, 7010, 0, 1, 0, 7009, 0, 0, 0,      // 130-132
            7018, 1, -1, 0, 7019, 2, -1, 0, 7008, 1, 0, 0,      // 133-135
            7007, 2, 0, 0                                           // 136
        };

        /// <summary>
        /// Gets the starting index in COMPONENT_DATA for a specific variation
        /// </summary>
        /// <param name="variation">The treasure pile variation</param>
        /// <returns>Starting index in the component data array</returns>
        public static int GetVariationStartIndex(TreasurePileVariation variation)
        {
            int index = 0;
            for (int i = 0; i < (int)variation; i++)
            {
                index += COMPONENT_COUNTS[i] * 4; // 4 values per component (ItemID, X, Y, Z)
            }
            return index;
        }

        /// <summary>
        /// Gets the display number for a given TreasurePileVariation (1-based, smallest first).
        /// </summary>
        /// <param name="variation">The TreasurePileVariation to get the number for.</param>
        /// <returns>The display number (1-8) where 1 is smallest, 8 is largest.</returns>
        public static int GetVariationNumber(TreasurePileVariation variation)
        {
            switch (variation)
            {
                case TreasurePileVariation.Minimal03: return 1; // 9 components (smallest)
                case TreasurePileVariation.Compact01: return 2; // 10 components
                case TreasurePileVariation.Standard05: return 3; // 11 components
                case TreasurePileVariation.Medium06: return 4; // 13 components
                case TreasurePileVariation.Balanced07: return 5; // 15 components
                case TreasurePileVariation.Original: return 6; // 26 components
                case TreasurePileVariation.Extended02: return 7; // 27 components
                case TreasurePileVariation.Large04: return 8; // 33 components (largest)
                default: return 0;
            }
        }

        /// <summary>
        /// Gets the type label for a given TreasurePileVariation.
        /// </summary>
        /// <param name="variation">The TreasurePileVariation to get the type label for.</param>
        /// <returns>The type label in Portuguese.</returns>
        public static string GetVariationTypeLabel(TreasurePileVariation variation)
        {
            switch (variation)
            {
                case TreasurePileVariation.Minimal03: return TreasurePileAddonStringConstants.TYPE_MINIMAL;
                case TreasurePileVariation.Compact01: return TreasurePileAddonStringConstants.TYPE_COMPACT;
                case TreasurePileVariation.Standard05: return TreasurePileAddonStringConstants.TYPE_STANDARD;
                case TreasurePileVariation.Medium06: return TreasurePileAddonStringConstants.TYPE_MEDIUM;
                case TreasurePileVariation.Balanced07: return TreasurePileAddonStringConstants.TYPE_BALANCED;
                case TreasurePileVariation.Original: return TreasurePileAddonStringConstants.TYPE_ORIGINAL;
                case TreasurePileVariation.Extended02: return TreasurePileAddonStringConstants.TYPE_EXTENDED;
                case TreasurePileVariation.Large04: return TreasurePileAddonStringConstants.TYPE_LARGE;
                default: return "[Desconhecido]";
            }
        }
        #endregion
    }
}

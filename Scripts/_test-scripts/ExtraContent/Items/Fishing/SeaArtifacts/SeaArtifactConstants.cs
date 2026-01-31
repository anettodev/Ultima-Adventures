namespace Server.Items
{
    /// <summary>
    /// Centralized constants for Sea Artifact items.
    /// Extracted from LightHouse.cs and SeaShells.cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class SeaArtifactConstants
    {
        #region Lighthouse Component Constants

        /// <summary>Lighthouse component ID for light source</summary>
        public const int LIGHTHOUSE_COMPONENT_LIGHT_SOURCE = 6864;

        /// <summary>Light source type for lighthouse</summary>
        public const int LIGHTHOUSE_LIGHT_SOURCE_TYPE = 2;

        /// <summary>Default amount for components</summary>
        public const int COMPONENT_AMOUNT_DEFAULT = 1;

        /// <summary>Default Z offset for components</summary>
        public const int COMPONENT_Z_OFFSET_DEFAULT = 0;

        /// <summary>Default hue for components</summary>
        public const int COMPONENT_HUE_DEFAULT = 0;

        #endregion

        #region Lighthouse Deed Constants

        /// <summary>Lighthouse deed item ID</summary>
        public const int LIGHTHOUSE_DEED_ITEM_ID = 0xA18;

        /// <summary>Cliloc ID for "To Be Built In A Home"</summary>
        public const int CLILOC_BUILD_IN_HOME = 1070722;

        #endregion

        #region Sea Shell Item IDs

        /// <summary>Base sea shell item ID</summary>
        public const int SEA_SHELL_ITEM_ID_BASE = 0xFC4;

        /// <summary>Sea shell item ID variant 1</summary>
        public const int SEA_SHELL_ITEM_ID_1 = 0xFC5;

        /// <summary>Sea shell item ID variant 2</summary>
        public const int SEA_SHELL_ITEM_ID_2 = 0xFC6;

        /// <summary>Sea shell item ID variant 3</summary>
        public const int SEA_SHELL_ITEM_ID_3 = 0xFC7;

        /// <summary>Sea shell item ID variant 4</summary>
        public const int SEA_SHELL_ITEM_ID_4 = 0xFC8;

        /// <summary>Sea shell item ID variant 5</summary>
        public const int SEA_SHELL_ITEM_ID_5 = 0xFC9;

        /// <summary>Sea shell item ID variant 6</summary>
        public const int SEA_SHELL_ITEM_ID_6 = 0xFCA;

        /// <summary>Sea shell item ID variant 7</summary>
        public const int SEA_SHELL_ITEM_ID_7 = 0xFCB;

        /// <summary>Sea shell item ID variant 8</summary>
        public const int SEA_SHELL_ITEM_ID_8 = 0xFCC;

        #endregion

        #region Sea Shell Constants

        /// <summary>Default weight for sea shells</summary>
        public const int SEA_SHELL_DEFAULT_WEIGHT = 1;

        /// <summary>Minimum random value for sea shell selection</summary>
        public const int SEA_SHELL_RANDOM_MIN = 0;

        /// <summary>Maximum random value for sea shell selection</summary>
        public const int SEA_SHELL_RANDOM_MAX = 8;

        #endregion

        #region Array Constants

        /// <summary>Array dimension size for component arrays (item, x, y, z)</summary>
        public const int ARRAY_DIMENSION_SIZE = 4;

        #endregion

        #region Serialization

        /// <summary>Current serialization version</summary>
        public const int SERIALIZATION_VERSION = 0;

        #endregion
    }
}


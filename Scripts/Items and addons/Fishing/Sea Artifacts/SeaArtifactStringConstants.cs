namespace Server.Items
{
    /// <summary>
    /// Centralized string constants for Sea Artifact items.
    /// All strings are in Brazilian Portuguese (PT-BR).
    /// Extracted from LightHouse.cs and SeaShells.cs to improve maintainability and enable localization.
    /// </summary>
    public static class SeaArtifactStringConstants
    {
        #region Lighthouse Names

        /// <summary>Lighthouse item name</summary>
        public const string NAME_LIGHTHOUSE = "farol";

        #endregion

        #region Lighthouse Properties

        /// <summary>Property text: "To Be Built In A Home"</summary>
        public const string PROP_BUILD_IN_HOME = "Para Ser Construído Em Uma Casa";

        #endregion

        #region Sea Shell Names

        /// <summary>Sea shell name (singular)</summary>
        public const string NAME_SEA_SHELL_SINGULAR = "concha do mar";

        /// <summary>Sea shell name (plural)</summary>
        public const string NAME_SEA_SHELL_PLURAL = "conchas do mar";

        #endregion
    }
}


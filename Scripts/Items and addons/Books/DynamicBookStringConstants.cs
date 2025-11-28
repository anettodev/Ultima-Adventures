namespace Server.Items
{
    /// <summary>
    /// Centralized string constants for DynamicBook messages and labels.
    /// All strings are in Brazilian Portuguese (PT-BR).
    /// Extracted from DynamicBook.cs to improve maintainability and enable localization.
    /// </summary>
    public static class DynamicBookStringConstants
    {
        #region Property Messages (PT-BR)

        /// <summary>Property label: "Escrito por "</summary>
        public const string PROP_WRITTEN_BY = "Escrito por ";

        #endregion

        #region HTML Formatting

        /// <summary>HTML separator: " by "</summary>
        public const string HTML_BY_SEPARATOR = " by ";

        /// <summary>HTML separator with line break: "<br>by "</summary>
        public const string HTML_BR_BY_SEPARATOR = "<br>by ";

        #endregion

        #region Color Constants

        /// <summary>Cyan color for author name</summary>
        public const string COLOR_CYAN = "#8be4fc";

        /// <summary>Jedi title color</summary>
        public const string COLOR_JEDI_TITLE = "#308EB3";

        /// <summary>Syth title color</summary>
        public const string COLOR_SYTH_TITLE = "#FF0000";

        /// <summary>Green text color</summary>
        public const string COLOR_TEXT_GREEN = "#00FF06";

        /// <summary>Dark text color</summary>
        public const string COLOR_TEXT_DARK = "#111111";

        #endregion
    }
}


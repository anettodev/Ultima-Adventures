namespace Server.Items
{
    /// <summary>
    /// Centralized constants for Sextant mechanics.
    /// Extracted from Sextant.cs to improve maintainability.
    /// </summary>
    public static class SextantConstants
    {
        #region Item IDs

        /// <summary>Sextant item ID</summary>
        public const int ITEM_ID_SEXTANT = 0x1058;

        #endregion

        #region Weight

        /// <summary>Weight of sextant</summary>
        public const double WEIGHT_SEXTANT = 2.0;

        #endregion

        #region Coordinate Constants

        /// <summary>Initial coordinate value</summary>
        public const int COORD_INIT_VALUE = 0;

        /// <summary>Minutes per degree for coordinate conversion</summary>
        public const int MINUTES_PER_DEGREE = 60;

        /// <summary>Full circle in degrees (double)</summary>
        public const double DEGREES_FULL_CIRCLE = 360.0;

        /// <summary>Full circle in degrees (int)</summary>
        public const int DEGREES_FULL_CIRCLE_INT = 360;

        /// <summary>Half circle in degrees</summary>
        public const double DEGREES_HALF_CIRCLE = 180.0;

        /// <summary>Divisor for coordinate center calculation</summary>
        public const int COORD_DIVISOR = 2;

        /// <summary>Invalid start coordinate marker</summary>
        public const int START_COORD_INVALID = -1;

        #endregion

        #region Map Boundaries - Moon of Luna

        /// <summary>Moon of Luna X minimum</summary>
        public const int MAP_LUNA_X_MIN = 5801;

        /// <summary>Moon of Luna Y minimum</summary>
        public const int MAP_LUNA_Y_MIN = 2716;

        /// <summary>Moon of Luna X maximum</summary>
        public const int MAP_LUNA_X_MAX = 6125;

        /// <summary>Moon of Luna Y maximum</summary>
        public const int MAP_LUNA_Y_MAX = 3034;

        /// <summary>Moon of Luna width</summary>
        public const int MAP_LUNA_WIDTH = 324;

        /// <summary>Moon of Luna height</summary>
        public const int MAP_LUNA_HEIGHT = 411;

        #endregion

        #region Map Boundaries - Land of Sosaria

        /// <summary>Land of Sosaria X minimum</summary>
        public const int MAP_SOSARIA_X_MIN = 0;

        /// <summary>Land of Sosaria Y minimum</summary>
        public const int MAP_SOSARIA_Y_MIN = 0;

        /// <summary>Land of Sosaria X maximum</summary>
        public const int MAP_SOSARIA_X_MAX = 5120;

        /// <summary>Land of Sosaria Y maximum</summary>
        public const int MAP_SOSARIA_Y_MAX = 3127;

        /// <summary>Land of Sosaria width</summary>
        public const int MAP_SOSARIA_WIDTH = 5120;

        /// <summary>Land of Sosaria height</summary>
        public const int MAP_SOSARIA_HEIGHT = 3127;

        #endregion

        #region Map Boundaries - Land of Lodoria

        /// <summary>Land of Lodoria X minimum</summary>
        public const int MAP_LODORIA_X_MIN = 0;

        /// <summary>Land of Lodoria Y minimum</summary>
        public const int MAP_LODORIA_Y_MIN = 0;

        /// <summary>Land of Lodoria X maximum</summary>
        public const int MAP_LODORIA_X_MAX = 5120;

        /// <summary>Land of Lodoria Y maximum</summary>
        public const int MAP_LODORIA_Y_MAX = 4095;

        /// <summary>Land of Lodoria width</summary>
        public const int MAP_LODORIA_WIDTH = 5120;

        /// <summary>Land of Lodoria height</summary>
        public const int MAP_LODORIA_HEIGHT = 4095;

        #endregion

        #region Map Boundaries - Serpent Island

        /// <summary>Serpent Island X minimum</summary>
        public const int MAP_SERPENT_X_MIN = 0;

        /// <summary>Serpent Island Y minimum</summary>
        public const int MAP_SERPENT_Y_MIN = 0;

        /// <summary>Serpent Island X maximum</summary>
        public const int MAP_SERPENT_X_MAX = 1870;

        /// <summary>Serpent Island Y maximum</summary>
        public const int MAP_SERPENT_Y_MAX = 2047;

        /// <summary>Serpent Island width</summary>
        public const int MAP_SERPENT_WIDTH = 1870;

        /// <summary>Serpent Island height</summary>
        public const int MAP_SERPENT_HEIGHT = 2047;

        #endregion

        #region Map Boundaries - Isles of Dread

        /// <summary>Isles of Dread X minimum</summary>
        public const int MAP_DREAD_X_MIN = 0;

        /// <summary>Isles of Dread Y minimum</summary>
        public const int MAP_DREAD_Y_MIN = 0;

        /// <summary>Isles of Dread X maximum</summary>
        public const int MAP_DREAD_X_MAX = 1447;

        /// <summary>Isles of Dread Y maximum</summary>
        public const int MAP_DREAD_Y_MAX = 1447;

        /// <summary>Isles of Dread width</summary>
        public const int MAP_DREAD_WIDTH = 1447;

        /// <summary>Isles of Dread height</summary>
        public const int MAP_DREAD_HEIGHT = 1447;

        #endregion

        #region Map Boundaries - Savaged Empire

        /// <summary>Savaged Empire X minimum</summary>
        public const int MAP_SAVAGED_X_MIN = 136;

        /// <summary>Savaged Empire Y minimum</summary>
        public const int MAP_SAVAGED_Y_MIN = 8;

        /// <summary>Savaged Empire X maximum</summary>
        public const int MAP_SAVAGED_X_MAX = 1160;

        /// <summary>Savaged Empire Y maximum</summary>
        public const int MAP_SAVAGED_Y_MAX = 1792;

        /// <summary>Savaged Empire width</summary>
        public const int MAP_SAVAGED_WIDTH = 1024;

        /// <summary>Savaged Empire height</summary>
        public const int MAP_SAVAGED_HEIGHT = 1784;

        #endregion

        #region Map Boundaries - Land of Ambrosia

        /// <summary>Land of Ambrosia X minimum</summary>
        public const int MAP_AMBROSIA_X_MIN = 5122;

        /// <summary>Land of Ambrosia Y minimum</summary>
        public const int MAP_AMBROSIA_Y_MIN = 3036;

        /// <summary>Land of Ambrosia X maximum</summary>
        public const int MAP_AMBROSIA_X_MAX = 6126;

        /// <summary>Land of Ambrosia Y maximum</summary>
        public const int MAP_AMBROSIA_Y_MAX = 4095;

        /// <summary>Land of Ambrosia width</summary>
        public const int MAP_AMBROSIA_WIDTH = 1004;

        /// <summary>Land of Ambrosia height</summary>
        public const int MAP_AMBROSIA_HEIGHT = 1059;

        #endregion

        #region Map Boundaries - Island of Umber Veil

        /// <summary>Island of Umber Veil X minimum</summary>
        public const int MAP_UMBER_X_MIN = 699;

        /// <summary>Island of Umber Veil Y minimum</summary>
        public const int MAP_UMBER_Y_MIN = 3129;

        /// <summary>Island of Umber Veil X maximum</summary>
        public const int MAP_UMBER_X_MAX = 2272;

        /// <summary>Island of Umber Veil Y maximum</summary>
        public const int MAP_UMBER_Y_MAX = 4095;

        /// <summary>Island of Umber Veil width</summary>
        public const int MAP_UMBER_WIDTH = 1573;

        /// <summary>Island of Umber Veil height</summary>
        public const int MAP_UMBER_HEIGHT = 966;

        #endregion

        #region Map Boundaries - Ilha de Kuldar

        /// <summary>Ilha de Kuldar X minimum</summary>
        public const int MAP_KULDAR_X_MIN = 6127;

        /// <summary>Ilha de Kuldar Y minimum</summary>
        public const int MAP_KULDAR_Y_MIN = 828;

        /// <summary>Ilha de Kuldar X maximum</summary>
        public const int MAP_KULDAR_X_MAX = 7168;

        /// <summary>Ilha de Kuldar Y maximum</summary>
        public const int MAP_KULDAR_Y_MAX = 2743;

        /// <summary>Ilha de Kuldar width</summary>
        public const int MAP_KULDAR_WIDTH = 1041;

        /// <summary>Ilha de Kuldar height</summary>
        public const int MAP_KULDAR_HEIGHT = 1915;

        #endregion

        #region Map Boundaries - Underworld

        /// <summary>Underworld X minimum</summary>
        public const int MAP_UNDERWORLD_X_MIN = 0;

        /// <summary>Underworld Y minimum</summary>
        public const int MAP_UNDERWORLD_Y_MIN = 0;

        /// <summary>Underworld X maximum</summary>
        public const int MAP_UNDERWORLD_X_MAX = 1581;

        /// <summary>Underworld Y maximum</summary>
        public const int MAP_UNDERWORLD_Y_MAX = 1599;

        /// <summary>Underworld width</summary>
        public const int MAP_UNDERWORLD_WIDTH = 1581;

        /// <summary>Underworld height</summary>
        public const int MAP_UNDERWORLD_HEIGHT = 1599;

        #endregion
    }
}


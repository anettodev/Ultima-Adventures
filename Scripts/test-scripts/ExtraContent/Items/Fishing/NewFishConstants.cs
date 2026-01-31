namespace Server.Items
{
    /// <summary>
    /// Centralized constants for NewFish mechanics.
    /// Extracted from NewFish.cs to improve maintainability.
    /// </summary>
    public static class NewFishConstants
    {
        #region Item IDs

        /// <summary>Base fish item ID</summary>
        public const int ITEM_ID_BASE = 0x09CC;

        /// <summary>Marlin item ID variant 1</summary>
        public const int ITEM_ID_MARLIN_1 = 0x22A8;

        /// <summary>Marlin item ID variant 2</summary>
        public const int ITEM_ID_MARLIN_2 = 0x22AA;

        /// <summary>Shark item ID</summary>
        public const int ITEM_ID_SHARK = 0x22AD;

        /// <summary>Seahorse item ID</summary>
        public const int ITEM_ID_SEAHORSE = 0x52DE;

        /// <summary>Stingray item ID variant 1</summary>
        public const int ITEM_ID_STINGRAY_1 = 0x52DF;

        /// <summary>Stingray item ID variant 2</summary>
        public const int ITEM_ID_STINGRAY_2 = 0x52E0;

        /// <summary>Squid item ID</summary>
        public const int ITEM_ID_SQUID = 0x52E1;

        /// <summary>Octopus item ID</summary>
        public const int ITEM_ID_OCTOPUS = 0x52C9;

        /// <summary>Crab item ID</summary>
        public const int ITEM_ID_CRAB = 0x531F;

        #endregion

        #region Gold Value Ranges

        /// <summary>Minimum gold value for standard fish</summary>
        public const int GOLD_VALUE_MIN_STANDARD = 5;

        /// <summary>Maximum gold value for standard fish</summary>
        public const int GOLD_VALUE_MAX_STANDARD = 25;

        /// <summary>Minimum gold value for marlin</summary>
        public const int GOLD_VALUE_MIN_MARLIN = 45;

        /// <summary>Maximum gold value for marlin</summary>
        public const int GOLD_VALUE_MAX_MARLIN = 100;

        /// <summary>Minimum gold value for shark</summary>
        public const int GOLD_VALUE_MIN_SHARK = 25;

        /// <summary>Maximum gold value for shark</summary>
        public const int GOLD_VALUE_MAX_SHARK = 85;

        /// <summary>Minimum gold value for seahorse</summary>
        public const int GOLD_VALUE_MIN_SEAHORSE = 20;

        /// <summary>Maximum gold value for seahorse</summary>
        public const int GOLD_VALUE_MAX_SEAHORSE = 60;

        /// <summary>Minimum gold value for stingray</summary>
        public const int GOLD_VALUE_MIN_STINGRAY = 35;

        /// <summary>Maximum gold value for stingray</summary>
        public const int GOLD_VALUE_MAX_STINGRAY = 95;

        /// <summary>Minimum gold value for squid</summary>
        public const int GOLD_VALUE_MIN_SQUID = 13;

        /// <summary>Maximum gold value for squid</summary>
        public const int GOLD_VALUE_MAX_SQUID = 55;

        /// <summary>Minimum gold value for octopus</summary>
        public const int GOLD_VALUE_MIN_OCTOPUS = 15;

        /// <summary>Maximum gold value for octopus</summary>
        public const int GOLD_VALUE_MAX_OCTOPUS = 55;

        /// <summary>Minimum gold value for crab</summary>
        public const int GOLD_VALUE_MIN_CRAB = 10;

        /// <summary>Maximum gold value for crab</summary>
        public const int GOLD_VALUE_MAX_CRAB = 50;

        #endregion

        #region Weight Constants

        /// <summary>Standard fish weight</summary>
        public const double WEIGHT_STANDARD = 1.0;

        /// <summary>Heavy fish weight (marlin, shark)</summary>
        public const double WEIGHT_HEAVY = 10.0;

        /// <summary>Medium fish weight (stingray)</summary>
        public const double WEIGHT_MEDIUM = 5.0;

        #endregion

        #region Random Ranges

        /// <summary>Minimum value for number generation</summary>
        public const int RANDOM_NUMBER_MIN = 3;

        /// <summary>Maximum value for number generation</summary>
        public const int RANDOM_NUMBER_MAX = 12;

        /// <summary>Number of color types</summary>
        public const int COLOR_TYPE_COUNT = 11;

        /// <summary>Number of material types</summary>
        public const int MATERIAL_TYPE_COUNT = 15;

        /// <summary>Maximum hue color index</summary>
        public const int HUE_COLOR_MAX = 12;

        #endregion

        #region Carving

        /// <summary>Number of fish steaks from carving</summary>
        public const int CARVE_STEAK_COUNT = 4;

        #endregion

        #region Hue Values

        /// <summary>Emerald hue values</summary>
        public static readonly int[] HUE_EMERALD = new int[] { 0xB83, 0xB93, 0xB94, 0xB95, 0xB96 };

        /// <summary>Fire hue values</summary>
        public static readonly int[] HUE_FIRE = new int[] { 0x4E7, 0x4E8, 0x4E9, 0x4EA, 0x4EB, 0x4EC };

        /// <summary>Cold water hue values</summary>
        public static readonly int[] HUE_COLDWATER = new int[] { 0x551, 0x552, 0x553, 0x554, 0x555, 0x556 };

        /// <summary>Poison hue values</summary>
        public static readonly int[] HUE_POISON = new int[] { 0x557, 0x558, 0x559, 0x55A, 0x55B, 0x55C };

        #endregion
    }
}


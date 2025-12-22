namespace Server.Items
{
    /// <summary>
    /// Centralized constants for poison potion mechanics.
    /// Extracted from BasePoisonPotion.cs to improve maintainability.
    /// All numeric values previously hardcoded are now defined here.
    /// </summary>
    public static class PoisonPotionConstants
    {
        #region Item Constants

        /// <summary>Item ID for poison potions (base)</summary>
        public const int ITEM_ID = 0xF0A;

        /// <summary>Item ID for Lesser poison potion</summary>
        public const int ITEM_ID_LESSER_POISON = 0x2600;

        /// <summary>Item ID for Greater poison potion</summary>
        public const int ITEM_ID_GREATER_POISON = 0x2601;

        /// <summary>Item ID for Deadly poison potion</summary>
        public const int ITEM_ID_DEADLY_POISON = 0x2669;

        /// <summary>Item ID for Lethal poison potion</summary>
        public const int ITEM_ID_LETHAL_POISON = 0x266A;

        /// <summary>Item ID for VenomSack</summary>
        public const int ITEM_ID_VENOM_SACK = 0x23A;

        /// <summary>Hue color for poison splatter effects</summary>
        public const int SPLATTER_HUE = 0x4F8;

        /// <summary>Glow parameter for splatter effects</summary>
        public const int SPLATTER_GLOW = 0;

        /// <summary>Serialization version number</summary>
        public const int SERIALIZATION_VERSION = 0;

        #endregion

        #region Skill Level Constants

        /// <summary>Default skill level for DoPoison method (Lesser poison)</summary>
        public const int SKILL_LEVEL_DEFAULT_DO_POISON = 1;

        /// <summary>Default skill level for Drink method</summary>
        public const int SKILL_LEVEL_DEFAULT_DRINK = 50;

        /// <summary>Skill level required for Regular poison potion</summary>
        public const int SKILL_LEVEL_REGULAR = 60;

        /// <summary>Skill level required for Greater poison potion</summary>
        public const int SKILL_LEVEL_GREATER = 70;

        /// <summary>Skill level required for Deadly poison potion</summary>
        public const int SKILL_LEVEL_DEADLY = 80;

        /// <summary>Skill level required for Lethal poison potion</summary>
        public const int SKILL_LEVEL_LETHAL = 90;

        /// <summary>Skill check range (±25 from base skill level)</summary>
        public const double SKILL_CHECK_RANGE = 25.0;

        #endregion

        #region Skill Check Range Constants

        /// <summary>Minimum skill check for Lesser poison</summary>
        public const int SKILL_CHECK_MIN_LESSER = 0;

        /// <summary>Maximum skill check for Regular poison</summary>
        public const int SKILL_CHECK_MAX_REGULAR = 50;

        /// <summary>Maximum skill check for Greater poison</summary>
        public const int SKILL_CHECK_MAX_GREATER = 75;

        /// <summary>Maximum skill check for Deadly poison</summary>
        public const int SKILL_CHECK_MAX_DEADLY = 95;

        /// <summary>Maximum skill check for Lethal poison</summary>
        public const int SKILL_CHECK_MAX_LETHAL = 115;

        #endregion

        #region Probability Constants

        /// <summary>Skill gain chance for Regular poison potion (40%)</summary>
        public const double SKILL_GAIN_CHANCE_REGULAR = 0.40;

        /// <summary>Skill gain chance for Greater poison potion (20%)</summary>
        public const double SKILL_GAIN_CHANCE_GREATER = 0.20;

        /// <summary>Skill gain chance for Deadly poison potion (10%)</summary>
        public const double SKILL_GAIN_CHANCE_DEADLY = 0.10;

        /// <summary>Skill gain chance for Lethal poison potion (5%)</summary>
        public const double SKILL_GAIN_CHANCE_LETHAL = 0.05;

        /// <summary>Self-poison chance when throwing poison potion (50%)</summary>
        public const double SELF_POISON_CHANCE = 0.50;

        #endregion

        #region Range Constants

        /// <summary>Target range for throwing poison potions (tiles)</summary>
        public const int TARGET_RANGE = 6;

        /// <summary>Maximum throw distance for poison potions (tiles)</summary>
        public const int MAX_THROW_DISTANCE = 4;

        #endregion

        #region Karma Constants

        /// <summary>Karma penalty for throwing poison potion</summary>
        public const int KARMA_PENALTY_THROW = -40;

        #endregion

        #region Potion Skill Level Constants

        /// <summary>Minimum poisoning skill for Lesser poison potion</summary>
        public const double SKILL_MIN_LESSER = 0.0;

        /// <summary>Maximum poisoning skill for Lesser poison potion</summary>
        public const double SKILL_MAX_LESSER = 40.0;

        /// <summary>Minimum poisoning skill for Regular poison potion</summary>
        public const double SKILL_MIN_REGULAR = 20.0;

        /// <summary>Maximum poisoning skill for Regular poison potion</summary>
        public const double SKILL_MAX_REGULAR = 60.0;

        /// <summary>Minimum poisoning skill for Greater poison potion</summary>
        public const double SKILL_MIN_GREATER = 40.0;

        /// <summary>Maximum poisoning skill for Greater poison potion</summary>
        public const double SKILL_MAX_GREATER = 80.0;

        /// <summary>Minimum poisoning skill for Deadly poison potion</summary>
        public const double SKILL_MIN_DEADLY = 60.0;

        /// <summary>Maximum poisoning skill for Deadly poison potion</summary>
        public const double SKILL_MAX_DEADLY = 100.0;

        /// <summary>Minimum poisoning skill for Lethal poison potion</summary>
        public const double SKILL_MIN_LETHAL = 80.0;

        /// <summary>Maximum poisoning skill for Lethal poison potion</summary>
        public const double SKILL_MAX_LETHAL = 120.0;

        #endregion

        #region VenomSack Constants

        /// <summary>Skill requirement for Lesser venom sack</summary>
        public const int SKILL_LESSER_VENOM = -5;

        /// <summary>Skill requirement for Regular venom sack</summary>
        public const int SKILL_REGULAR_VENOM = 15;

        /// <summary>Skill requirement for Greater venom sack</summary>
        public const int SKILL_GREATER_VENOM = 35;

        /// <summary>Skill requirement for Deadly venom sack</summary>
        public const int SKILL_DEADLY_VENOM = 55;

        /// <summary>Skill requirement for Lethal venom sack</summary>
        public const int SKILL_LETHAL_VENOM = 75;

        /// <summary>Maximum skill check value for VenomSack</summary>
        public const int SKILL_MAX_CHECK = 125;

        /// <summary>Minimum poison chance value</summary>
        public const int POISON_CHANCE_MIN = 0;

        /// <summary>Maximum poison chance value</summary>
        public const int POISON_CHANCE_MAX = 10;

        /// <summary>Poison chance threshold</summary>
        public const int POISON_CHANCE_THRESHOLD = 6;

        /// <summary>Success sound ID for VenomSack</summary>
        public const int SOUND_ID_VENOM_SUCCESS = 0x240;

        /// <summary>Failure sound ID for VenomSack</summary>
        public const int SOUND_ID_VENOM_FAILURE = 0x62D;

        /// <summary>Weight for VenomSack</summary>
        public const double WEIGHT_VENOM_SACK = 1.0;

        /// <summary>Default hue for VenomSack</summary>
        public const int HUE_DEFAULT = 0;

        #endregion
    }
}


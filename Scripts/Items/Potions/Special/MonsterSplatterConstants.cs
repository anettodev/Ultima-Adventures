namespace Server.Items
{
    /// <summary>
    /// Centralized constants for MonsterSplatter mechanics.
    /// Extracted from MonsterSplatter.cs to improve maintainability.
    /// All numeric values previously hardcoded are now defined here.
    /// </summary>
    public static class MonsterSplatterConstants
    {
        #region Item ID Constants

        /// <summary>Base splatter item ID</summary>
        public const int ITEM_ID_BASE = 0x122A;

        /// <summary>Alternative splatter item ID 1</summary>
        public const int ITEM_ID_ALT_1 = 0x122B;

        /// <summary>Alternative splatter item ID 2</summary>
        public const int ITEM_ID_ALT_2 = 0x122D;

        /// <summary>Alternative splatter item ID 3</summary>
        public const int ITEM_ID_ALT_3 = 0x122E;

        /// <summary>Alternative splatter item ID 4</summary>
        public const int ITEM_ID_ALT_4 = 0x263B;

        /// <summary>Alternative splatter item ID 5</summary>
        public const int ITEM_ID_ALT_5 = 0x263C;

        /// <summary>Alternative splatter item ID 6</summary>
        public const int ITEM_ID_ALT_6 = 0x263D;

        /// <summary>Alternative splatter item ID 7</summary>
        public const int ITEM_ID_ALT_7 = 0x263E;

        /// <summary>Alternative splatter item ID 8</summary>
        public const int ITEM_ID_ALT_8 = 0x263F;

        /// <summary>Alternative splatter item ID 9</summary>
        public const int ITEM_ID_ALT_9 = 0x2640;

        #endregion

        #region Damage Constants

        /// <summary>Standard minimum damage</summary>
        public const int DAMAGE_MIN_STANDARD = 24;

        /// <summary>Standard maximum damage</summary>
        public const int DAMAGE_MAX_STANDARD = 48;

        /// <summary>High minimum damage</summary>
        public const int DAMAGE_MIN_HIGH = 40;

        /// <summary>High maximum damage</summary>
        public const int DAMAGE_MAX_HIGH = 60;

        /// <summary>Low minimum damage</summary>
        public const int DAMAGE_MIN_LOW = 20;

        /// <summary>Low maximum damage</summary>
        public const int DAMAGE_MAX_LOW = 40;

        #endregion

        #region Skill Calculation Constants

        /// <summary>Skill divisor for Poisoning skill</summary>
        public const int SKILL_DIVISOR_POISONING = 50;

        /// <summary>Skill divisor for TasteID and Alchemy skills</summary>
        public const int SKILL_DIVISOR_TASTE_ALCHEMY = 33;

        /// <summary>Skill divisor for EnhancePotions calculation</summary>
        public const int SKILL_DIVISOR_ENHANCE_POTIONS = 5;

        /// <summary>Minimum poison chance value</summary>
        public const int POISON_CHANCE_MIN = 1;

        /// <summary>Maximum poison chance value</summary>
        public const int POISON_CHANCE_MAX = 8;

        #endregion

        #region Effect ID Constants

        /// <summary>Fire effect ID</summary>
        public const int EFFECT_ID_FIRE = 0x3709;

        /// <summary>Blood/slime particle effect ID</summary>
        public const int EFFECT_ID_BLOOD_SLIME = 0x36B0;

        /// <summary>Ice effect ID</summary>
        public const int EFFECT_ID_ICE = 0x1A84;

        /// <summary>Ice effect hue</summary>
        public const int EFFECT_ID_ICE_HUE = 0x9C1;

        /// <summary>Rot effect ID</summary>
        public const int EFFECT_ID_ROT = 0x3400;

        /// <summary>Pain particle effect ID</summary>
        public const int EFFECT_ID_PAIN = 0x37C4;

        /// <summary>Air walk particle effect ID</summary>
        public const int EFFECT_ID_AIR_WALK = 0x2007;

        /// <summary>Liquid goo effect ID option 1</summary>
        public const int EFFECT_ID_LIQUID_GOO_1 = 0x3967;

        /// <summary>Liquid goo effect ID option 2</summary>
        public const int EFFECT_ID_LIQUID_GOO_2 = 0x3979;

        #endregion

        #region Sound ID Constants

        /// <summary>Poison sound ID</summary>
        public const int SOUND_ID_POISON = 0x4D1;

        /// <summary>Fire sound ID</summary>
        public const int SOUND_ID_FIRE = 0x225;

        /// <summary>Default blood sound ID</summary>
        public const int SOUND_ID_BLOOD_DEFAULT = 0x229;

        /// <summary>Acid sound ID</summary>
        public const int SOUND_ID_ACID = 0x231;

        /// <summary>Male player sound ID</summary>
        public const int SOUND_ID_MALE_PLAYER = 0x43F;

        /// <summary>Female player sound ID</summary>
        public const int SOUND_ID_FEMALE_PLAYER = 0x32D;

        /// <summary>Splatter creation sound ID</summary>
        public const int SOUND_ID_SPLATTER_CREATE = 0x026;

        /// <summary>Air walk sound ID</summary>
        public const int SOUND_ID_AIR_WALK = 0x014;

        /// <summary>Liquid fire sound ID</summary>
        public const int SOUND_ID_LIQUID_FIRE = 0x208;

        /// <summary>Liquid goo sound ID</summary>
        public const int SOUND_ID_LIQUID_GOO = 0x5C3;

        /// <summary>Liquid ice sound ID</summary>
        public const int SOUND_ID_LIQUID_ICE = 0x10B;

        /// <summary>Liquid rot sound ID</summary>
        public const int SOUND_ID_LIQUID_ROT = 0x108;

        /// <summary>Liquid pain sound ID</summary>
        public const int SOUND_ID_LIQUID_PAIN = 0x210;

        #endregion

        #region Hue/Color Constants

        /// <summary>Fire effect hue</summary>
        public const int HUE_FIRE_EFFECT = 5052;

        /// <summary>Blood effect hue</summary>
        public const int HUE_BLOOD_EFFECT = 0x25;

        /// <summary>Default particle hue</summary>
        public const int HUE_PARTICLE_DEFAULT = 63;

        #endregion

        #region Particle Effect Constants

        /// <summary>Particle count for blood/slime effects</summary>
        public const int PARTICLE_COUNT_BLOOD = 1;

        /// <summary>Particle speed for blood/slime effects</summary>
        public const int PARTICLE_SPEED_BLOOD = 14;

        /// <summary>Particle duration for blood/slime effects</summary>
        public const int PARTICLE_DURATION_BLOOD = 7;

        /// <summary>Particle effect ID for blood/slime</summary>
        public const int PARTICLE_EFFECT_BLOOD = 9915;

        /// <summary>Particle count for fire effects</summary>
        public const int PARTICLE_COUNT_FIRE = 10;

        /// <summary>Particle speed for fire effects</summary>
        public const int PARTICLE_SPEED_FIRE = 30;

        /// <summary>Particle count for air walk</summary>
        public const int PARTICLE_COUNT_AIR_WALK = 9;

        /// <summary>Particle speed for air walk</summary>
        public const int PARTICLE_SPEED_AIR_WALK = 32;

        /// <summary>Particle effect ID for air walk</summary>
        public const int PARTICLE_EFFECT_AIR_WALK = 5022;

        /// <summary>Particle count for pain effect</summary>
        public const int PARTICLE_COUNT_PAIN = 1;

        /// <summary>Particle speed for pain effect</summary>
        public const int PARTICLE_SPEED_PAIN = 8;

        /// <summary>Particle effect ID for pain (layer 1)</summary>
        public const int PARTICLE_EFFECT_PAIN_1 = 9916;

        /// <summary>Particle effect ID for pain (layer 2)</summary>
        public const int PARTICLE_EFFECT_PAIN_2 = 9502;

        /// <summary>Particle duration for pain (layer 1)</summary>
        public const int PARTICLE_DURATION_PAIN_1 = 39;

        /// <summary>Particle duration for pain (layer 2)</summary>
        public const int PARTICLE_DURATION_PAIN_2 = 39;

        /// <summary>Particle layer for pain (layer 1)</summary>
        public const int PARTICLE_LAYER_PAIN_1 = 3;

        /// <summary>Particle layer for pain (layer 2)</summary>
        public const int PARTICLE_LAYER_PAIN_2 = 4;

        #endregion

        #region Range/Threshold Constants

        /// <summary>Item search range for TooMuchSplatter check</summary>
        public const int SEARCH_RANGE = 10;

        /// <summary>Maximum splatter threshold before blocking creation</summary>
        public const int MAX_SPLATTER_THRESHOLD = 16;

        /// <summary>Timer duration in seconds</summary>
        public const double TIMER_DURATION_SECONDS = 30.0;

        /// <summary>Normal splatter weight</summary>
        public const double WEIGHT_NORMAL = 1.0;

        /// <summary>Glowing splatter weight</summary>
        public const double WEIGHT_GLOWING = 2.0;

        #endregion

        #region Body Type Constants

        /// <summary>Male body type</summary>
        public const int BODY_TYPE_MALE = 0x190;

        /// <summary>Female body type</summary>
        public const int BODY_TYPE_FEMALE = 0x191;

        #endregion

        #region Liquid Effect Constants

        /// <summary>Liquid effect duration</summary>
        public const int LIQUID_EFFECT_DURATION = 30;

        /// <summary>Liquid effect speed</summary>
        public const int LIQUID_EFFECT_SPEED = 10;

        /// <summary>Rot effect duration</summary>
        public const int ROT_EFFECT_DURATION = 60;

        #endregion

        #region Air Walk Constants

        /// <summary>Air walk X offset</summary>
        public const int AIR_WALK_OFFSET_X = 1;

        /// <summary>Air walk Y offset</summary>
        public const int AIR_WALK_OFFSET_Y = 1;

        /// <summary>Air walk Z offset</summary>
        public const int AIR_WALK_OFFSET_Z = 5;

        #endregion

        #region Serialization Constants

        /// <summary>Serialization version number</summary>
        public const int SERIALIZATION_VERSION = 0;

        #endregion
    }
}


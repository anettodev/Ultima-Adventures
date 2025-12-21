namespace Server.Misc
{
    /// <summary>
    /// Centralized constants for player information calculations and mechanics.
    /// Extracted from GetPlayerInfo.cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class PlayerInfoConstants
    {
        #region Character Skill Constants
        
        /// <summary>Special skill ID for Titan of Ether title</summary>
        public const int TITAN_OF_ETHER_SKILL_ID = 55;
        
        /// <summary>Minimum skill value required for any title</summary>
        public const double MIN_SKILL_FOR_TITLE = 0.1;
        
        /// <summary>Skill threshold for Aspiring title level</summary>
        public const double ASPIRING_SKILL_THRESHOLD = 29.1;
        
        #endregion
        
        #region Skill Level Thresholds
        
        /// <summary>Skill level threshold for level 1</summary>
        public const double SKILL_LEVEL_40 = 40.0;
        
        /// <summary>Skill level threshold for level 2</summary>
        public const double SKILL_LEVEL_50 = 50.0;
        
        /// <summary>Skill level threshold for level 3</summary>
        public const double SKILL_LEVEL_60 = 60.0;
        
        /// <summary>Skill level threshold for level 4</summary>
        public const double SKILL_LEVEL_70 = 70.0;
        
        /// <summary>Skill level threshold for level 5</summary>
        public const double SKILL_LEVEL_80 = 80.0;
        
        /// <summary>Skill level threshold for level 6</summary>
        public const double SKILL_LEVEL_90 = 90.0;
        
        /// <summary>Skill level threshold for level 7</summary>
        public const double SKILL_LEVEL_100 = 100.0;
        
        /// <summary>Skill level threshold for level 8</summary>
        public const double SKILL_LEVEL_110 = 110.0;
        
        /// <summary>Skill level threshold for level 9</summary>
        public const double SKILL_LEVEL_120 = 120.0;
        
        #endregion
        
        #region Specific Skill Requirements
        
        /// <summary>Minimum skill for Necromancer/Chivalry/EvalInt/Swords evil checks</summary>
        public const double MIN_SKILL_FOR_EVIL_CHECKS = 50.0;
        
        /// <summary>Minimum skill for Forensics undertaker check</summary>
        public const double MIN_SKILL_FOR_UNDERTAKER = 80.0;
        
        /// <summary>Minimum skill for Archmage (Magery + Necromancy)</summary>
        public const double MIN_SKILL_FOR_ARCHMAGE = 100.0;
        
        /// <summary>Minimum skill for Fishing Captain</summary>
        public const double MIN_SKILL_FOR_CAPTAIN = 100.0;
        
        /// <summary>Minimum skill for Monk Mystic transformation</summary>
        public const double MIN_SKILL_FOR_MYSTIC = 50.0;
        
        /// <summary>Minimum skill for Priest (Healing + SpiritSpeak)</summary>
        public const double MIN_SKILL_FOR_PRIEST = 50.0;
        
        /// <summary>Minimum skill for Jester check</summary>
        public const double MIN_SKILL_FOR_JESTER = 10.0;
        
        #endregion
        
        #region Karma Thresholds
        
        /// <summary>Neutral karma value</summary>
        public const int KARMA_NEUTRAL = 0;
        
        /// <summary>Karma threshold for Slaver title</summary>
        public const int KARMA_SLAVER = -2500;
        
        /// <summary>Karma threshold for Death Knight/Syth</summary>
        public const int KARMA_DEATH_KNIGHT = -5000;
        
        /// <summary>Karma threshold for Priest title</summary>
        public const int KARMA_PRIEST = 2500;
        
        /// <summary>Karma threshold for Jedi Padawan</summary>
        public const int KARMA_JEDI_PADAWAN = 500;
        
        /// <summary>Karma threshold for Jedi Consular</summary>
        public const int KARMA_JEDI_CONSULAR = 5000;
        
        /// <summary>Karma threshold for Jedi Master</summary>
        public const int KARMA_JEDI_MASTER = 7500;
        
        #endregion
        
        #region Jedi/Syth Skill Thresholds
        
        /// <summary>Minimum Chivalry skill for Jedi Padawan</summary>
        public const double JEDI_SKILL_PADAWAN = 10.0;
        
        /// <summary>Minimum Chivalry skill for Jedi Consular</summary>
        public const double JEDI_SKILL_CONSULAR = 100.0;
        
        /// <summary>Minimum Chivalry skill for Jedi Master</summary>
        public const double JEDI_SKILL_MASTER = 120.0;
        
        #endregion
        
        #region Player Level Calculation Constants
        
        /// <summary>Maximum fame value for level calculation</summary>
        public const int MAX_FAME_FOR_LEVEL = 15000;
        
        /// <summary>Maximum karma value for level calculation</summary>
        public const int MAX_KARMA_FOR_LEVEL = 15000;
        
        /// <summary>Maximum skills total for level calculation</summary>
        public const int MAX_SKILLS_FOR_LEVEL = 10000;
        
        /// <summary>Skills multiplier for level calculation</summary>
        public const double SKILLS_MULTIPLIER = 1.5;
        
        /// <summary>Stats multiplier for level calculation</summary>
        public const int STATS_MULTIPLIER = 60;
        
        /// <summary>Level calculation divisor</summary>
        public const int LEVEL_DIVISOR = 600;
        
        /// <summary>Level adjustment multiplier</summary>
        public const double LEVEL_ADJUSTMENT_MULTIPLIER = 1.12;
        
        /// <summary>Level adjustment subtractor</summary>
        public const int LEVEL_ADJUSTMENT_SUBTRACTOR = 10;
        
        /// <summary>Minimum player level</summary>
        public const int MIN_PLAYER_LEVEL = 1;
        
        /// <summary>Maximum player level</summary>
        public const int MAX_PLAYER_LEVEL = 100;
        
        #endregion
        
        #region Player Difficulty Thresholds
        
        /// <summary>Difficulty level 1 threshold</summary>
        public const int DIFFICULTY_LEVEL_1 = 25;
        
        /// <summary>Difficulty level 2 threshold</summary>
        public const int DIFFICULTY_LEVEL_2 = 50;
        
        /// <summary>Difficulty level 3 threshold</summary>
        public const int DIFFICULTY_LEVEL_3 = 75;
        
        /// <summary>Difficulty level 4 threshold</summary>
        public const int DIFFICULTY_LEVEL_4 = 95;
        
        #endregion
        
        #region Resurrection Cost Constants
        
        /// <summary>Fame/Karma/Skills divisor for resurrection cost</summary>
        public const int RESURRECT_DIVISOR = 3;
        
        /// <summary>Maximum fame for resurrection cost</summary>
        public const int MAX_FAME_FOR_RESURRECT = 5000;
        
        /// <summary>Maximum karma for resurrection cost</summary>
        public const int MAX_KARMA_FOR_RESURRECT = 5000;
        
        /// <summary>Maximum skills for resurrection cost</summary>
        public const int MAX_SKILLS_FOR_RESURRECT = 25000;
        
        /// <summary>Maximum stats for resurrection cost</summary>
        public const int MAX_STATS_FOR_RESURRECT = 250;
        
        /// <summary>Stats multiplier for resurrection cost</summary>
        public const int RESURRECT_STATS_MULTIPLIER = 50;
        
        /// <summary>Resurrection level divisor</summary>
        public const int RESURRECT_LEVEL_DIVISOR = 1000;
        
        /// <summary>Maximum resurrection level</summary>
        public const int MAX_RESURRECT_LEVEL = 150;
        
        /// <summary>Resurrection cost multiplier</summary>
        public const int RESURRECT_COST_MULTIPLIER = 100;
        
        /// <summary>Profession multiplier for resurrection cost</summary>
        public const double RESURRECT_PROFESSION_MULTIPLIER = 1.25;
        
        /// <summary>Non-Avatar multiplier for resurrection cost</summary>
        public const double RESURRECT_NON_AVATAR_MULTIPLIER = 2.0;
        
        /// <summary>Minimum resurrection cost</summary>
        public const int MIN_RESURRECT_COST = 300;
        
        #endregion
        
        #region Luck Calculation Constants
        
        /// <summary>Luck cap for Midland check</summary>
        public const int LUCK_CAP_MIDLAND = 8000;
        
        /// <summary>Minimum random luck value</summary>
        public const int LUCK_RANDOM_MIN = 1;
        
        /// <summary>Maximum random luck value</summary>
        public const int LUCK_RANDOM_MAX = 2000;
        
        /// <summary>Luck divisor</summary>
        public const double LUCK_DIVISOR = 0.5;
        
        /// <summary>Maximum balance value</summary>
        public const int BALANCE_MAX = 100000;
        
        /// <summary>Balance center point</summary>
        public const int BALANCE_CENTER = 50000;
        
        /// <summary>Balance divisor</summary>
        public const int BALANCE_DIVISOR = 200000;
        
        /// <summary>Clover percentage for LuckyPlayer and LuckyKiller</summary>
        public const double CLOVER_PERCENTAGE = 0.05;
        
        /// <summary>Clover percentage for LuckyPlayerArtifacts</summary>
        public const double CLOVER_PERCENTAGE_ARTIFACTS = 0.005;
        
        /// <summary>Clover random range</summary>
        public const int CLOVER_RANDOM_RANGE = 250;
        
        /// <summary>Well-rested chance</summary>
        public const double WELL_RESTED_CHANCE = 0.10;
        
        #endregion
        
        #region Other Constants
        
        /// <summary>Skills cap for space check</summary>
        public const int SKILLS_CAP_SPACE = 40000;
        
        /// <summary>Character Oriental/Evil true value</summary>
        public const int CHARACTER_FLAG_TRUE = 1;
        
        /// <summary>Minimum points for isJester, isMonk, isSyth, isJedi</summary>
        public const int MIN_POINTS_FOR_SPECIAL = 2;
        
        /// <summary>Female body ID</summary>
        public const int BODY_FEMALE = 0x191;
        
        #endregion
    }
}


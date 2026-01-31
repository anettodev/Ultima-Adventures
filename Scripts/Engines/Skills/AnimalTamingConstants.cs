using System;
using Server;

namespace Server.SkillHandlers
{
	/// <summary>
	/// Centralized constants for AnimalTaming skill calculations and mechanics.
	/// Extracted from AnimalTaming.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class AnimalTamingConstants
	{
		#region Timing Constants

		/// <summary>Cooldown period after using AnimalTaming skill</summary>
		public const double COOLDOWN_HOURS = 6.0;

		/// <summary>Timer interval between taming attempts (in seconds)</summary>
		public const double TIMER_INTERVAL_SECONDS = 5.0;

		/// <summary>Delay before resetting pacify status (in seconds)</summary>
		public const double RESET_PACIFY_DELAY_SECONDS = 2.0;

		#endregion

		#region Range Constants

		/// <summary>Base taming range in tiles (for skill &lt; 80)</summary>
		public const int BASE_TAMING_RANGE = 4;

		/// <summary>Maximum taming range in tiles</summary>
		public const int MAX_TAMING_RANGE = 10;

		/// <summary>Skill threshold for range bonus (after this, +1 tile per 10 points)</summary>
		public const double RANGE_SKILL_THRESHOLD = 80.0;

		/// <summary>Divisor for calculating range bonus (tiles per skill points)</summary>
		public const double RANGE_BONUS_DIVISOR = 10.0;

		#endregion

		#region Skill Check Constants

		/// <summary>Skill check range around MinTameSkill (±value)</summary>
		public const double SKILL_CHECK_RANGE = 10.0;

		/// <summary>Random chance for AnimalTaming skill check (1 in X)</summary>
		public const int SKILL_CHECK_RANDOM = 2;

		#endregion

		#region Skill Threshold Constants

		/// <summary>Skill threshold for 120+ attempts</summary>
		public const double SKILL_THRESHOLD_120 = 120.0;

		/// <summary>Skill threshold for 110+ attempts</summary>
		public const double SKILL_THRESHOLD_110 = 110.0;

		/// <summary>Skill threshold for 100+ attempts</summary>
		public const double SKILL_THRESHOLD_100 = 100.0;

		/// <summary>Skill threshold for 90+ attempts</summary>
		public const double SKILL_THRESHOLD_90 = 90.0;

		/// <summary>Skill threshold for 80+ attempts</summary>
		public const double SKILL_THRESHOLD_80 = 80.0;

		#endregion

		#region Attempt Count Constants

		/// <summary>Minimum attempts for skill &lt; 80</summary>
		public const int ATTEMPTS_MIN_BELOW_80 = 5;

		/// <summary>Maximum attempts for skill &lt; 80</summary>
		public const int ATTEMPTS_MAX_BELOW_80 = 6;

		/// <summary>Minimum attempts for skill 80-89</summary>
		public const int ATTEMPTS_MIN_80 = 4;

		/// <summary>Maximum attempts for skill 80-89</summary>
		public const int ATTEMPTS_MAX_80 = 5;

		/// <summary>Minimum attempts for skill 90-99</summary>
		public const int ATTEMPTS_MIN_90 = 3;

		/// <summary>Maximum attempts for skill 90-99</summary>
		public const int ATTEMPTS_MAX_90 = 4;

		/// <summary>Minimum attempts for skill 100-109</summary>
		public const int ATTEMPTS_MIN_100 = 2;

		/// <summary>Maximum attempts for skill 100-109</summary>
		public const int ATTEMPTS_MAX_100 = 3;

		/// <summary>Minimum attempts for skill 110-119</summary>
		public const int ATTEMPTS_MIN_110 = 1;

		/// <summary>Maximum attempts for skill 110-119</summary>
		public const int ATTEMPTS_MAX_110 = 2;

		/// <summary>Fixed attempts for skill 120+</summary>
		public const int ATTEMPTS_120_PLUS = 1;

		#endregion

		#region Anger System Constants

		/// <summary>Multiplier applied to player skill for anger calculation (makes taming easier)</summary>
		public const double ANGER_SKILL_MULTIPLIER = 1.12;

		/// <summary>Chance threshold for bard pacify reset (25% chance to reset immediately)</summary>
		public const double BARD_PACIFY_CHANCE = 0.25;

		#endregion

		#region Scaling Constants

		/// <summary>Minimum stat value after scaling</summary>
		public const int MIN_STAT_VALUE = 1;

		/// <summary>Minimum skill cap value</summary>
		public const double MIN_SKILL_CAP = 100.0;

		/// <summary>Skill scalar when creature is paralyzed during taming</summary>
		public const double PARALYZED_SKILL_SCALAR = 0.65;

		/// <summary>Skill scalar for normal taming (not paralyzed)</summary>
		public const double NORMAL_SKILL_SCALAR = 0.80;

		/// <summary>Stat scalar when creature has StatLossAfterTame property</summary>
		public const double STAT_LOSS_SCALAR = 0.50;

		#endregion

		#region Subdue Constants

		/// <summary>Divisor for calculating subdue threshold (HitsMax / value)</summary>
		public const int SUBDUE_THRESHOLD_DIVISOR = 10;

		#endregion

		#region Re-taming Constants

		/// <summary>Skill penalty per previous owner when re-taming</summary>
		public const double OWNER_PENALTY_PER_OWNER = 2.0;

		/// <summary>Skill threshold for mastery check override</summary>
		public const double MASTERY_SKILL_THRESHOLD = 24.9;

		/// <summary>Skill override value when mastery check passes</summary>
		public const double MASTERY_SKILL_OVERRIDE = 0.0;

		/// <summary>Owner count for first-time taming</summary>
		public const int FIRST_OWNER_COUNT = 0;

		#endregion

		#region Difficulty Message Constants

		/// <summary>Skill difference threshold for "almost grasp" message</summary>
		public const double DIFF_THRESHOLD_ALMOST = 0.5;

		/// <summary>Skill difference threshold for "close to able" message</summary>
		public const double DIFF_THRESHOLD_CLOSE = 2.5;

		/// <summary>Skill difference threshold for "more effort" message</summary>
		public const double DIFF_THRESHOLD_EFFORT = 5.0;

		/// <summary>Skill difference threshold for "long ways" message</summary>
		public const double DIFF_THRESHOLD_LONG = 10.0;

		/// <summary>Skill difference threshold for "too difficult" message</summary>
		public const double DIFF_THRESHOLD_DIFFICULT = 15.0;

		#endregion

		#region Speech Generation Constants

		/// <summary>Chance to use standard speech vs custom speech (85%)</summary>
		public const double SPEECH_CHANCE = 0.85;

		/// <summary>Number of speech types for standard messages</summary>
		public const int SPEECH_TYPE_COUNT = 3;

		/// <summary>Karma threshold for evil speech (negative karma)</summary>
		public const int KARMA_THRESHOLD = 0;

		/// <summary>Number of speech options per karma type</summary>
		public const int SPEECH_COUNT = 13;

		/// <summary>BAC divisor for drunk speech calculation</summary>
		public const int BAC_DIVISOR = 200;

		/// <summary>Chance threshold for drunk speech word replacement (85%)</summary>
		public const double DRUNK_SPEECH_CHANCE = 0.85;

		/// <summary>Number of drunk speech code options</summary>
		public const int DRUNK_SPEECH_COUNT = 7;

		#endregion

		#region Enraged/Righteous Constants

		/// <summary>Stat divisor for enraged/righteous creatures</summary>
		public const int ENRAGED_STAT_DIVISOR = 2;

		/// <summary>Hue value for enraged/righteous creatures (default)</summary>
		public const int ENRAGED_HUE = -1;

		#endregion

		#region Default Values

		/// <summary>Default home coordinates (0, 0, 0)</summary>
		public static readonly Point3D DEFAULT_HOME = new Point3D(0, 0, 0);

		/// <summary>Default range home value (unlimited)</summary>
		public const int DEFAULT_RANGE_HOME = -1;

		#endregion

		#region Message Colors

		/// <summary>Error message color</summary>
		public const int MSG_COLOR_ERROR = 0x3B2;

		/// <summary>Warning message color (yellow)</summary>
		public const int MSG_COLOR_WARNING = 33;

		/// <summary>Success message color (cyan)</summary>
		public const int MSG_COLOR_SUCCESS = 0x3FF;

		/// <summary>Emote message color</summary>
		public const int MSG_COLOR_EMOTE = 0x59;

		#endregion

		#region Timer Priority

		/// <summary>Timer priority for taming attempts</summary>
		public const TimerPriority TIMER_PRIORITY = TimerPriority.TwoFiftyMS;

		#endregion

		#region Tier System Constants

		/// <summary>Tier 1: Creatures requiring 0.0-20.0 skill</summary>
		public const double TIER_1_MAX = 20.0;

		/// <summary>Tier 2: Creatures requiring 20.1-40.0 skill</summary>
		public const double TIER_2_MIN = 20.1;
		public const double TIER_2_MAX = 40.0;

		/// <summary>Tier 3: Creatures requiring 40.1-60.0 skill</summary>
		public const double TIER_3_MIN = 40.1;
		public const double TIER_3_MAX = 60.0;

		/// <summary>Tier 4: Creatures requiring 60.1-80.0 skill</summary>
		public const double TIER_4_MIN = 60.1;
		public const double TIER_4_MAX = 80.0;

		/// <summary>Tier 5: Creatures requiring 80.1-100.0 skill</summary>
		public const double TIER_5_MIN = 80.1;
		public const double TIER_5_MAX = 100.0;

		/// <summary>Tier 6: Creatures requiring 100.1-110.0 skill</summary>
		public const double TIER_6_MIN = 100.1;
		public const double TIER_6_MAX = 110.0;

		/// <summary>Tier 7: Creatures requiring 110.1+ skill</summary>
		public const double TIER_7_MIN = 110.1;

		/// <summary>Skill gain reduction per tier difference when taming lower tier creatures (40% = 0.4)</summary>
		public const double TIER_REDUCTION_PER_LEVEL = 0.4;

		#endregion
	}
}


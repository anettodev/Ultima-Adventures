using System;
using Server.Mobiles;

namespace Server.Misc
{
	/// <summary>
	/// Centralized constants for SkillCheck calculations and mechanics.
	/// Extracted from SkillCheck.cs to improve maintainability and enable easy balance tuning.
	/// </summary>
	public static class SkillCheckConstants
	{
		#region Skill Gain Base Values

		/// <summary>Default gainer divisor for skill gain calculations</summary>
		public const double DEFAULT_GAINER = 2.0;

		/// <summary>Minimum gain chance threshold (prevents zero gain)</summary>
		public const double MIN_GAIN_CHANCE = 0.01;

		/// <summary>Success bonus multiplier for skill gain</summary>
		public const double SUCCESS_BONUS = 0.5;

		/// <summary>AOS failure bonus (no bonus on failure)</summary>
		public const double AOS_FAILURE_BONUS = 0.0;

		/// <summary>Legacy failure bonus (small bonus even on failure)</summary>
		public const double LEGACY_FAILURE_BONUS = 0.2;

		#endregion

		#region Guild Skill Bonuses

		/// <summary>Minimum guild gainer multiplier</summary>
		public const double GUILD_GAINER_MIN = 1.55;

		/// <summary>Maximum guild gainer multiplier</summary>
		public const double GUILD_GAINER_MAX = 1.80;

		/// <summary>Number of guild gainer options (0-5 = 6 options)</summary>
		public const int GUILD_GAINER_OPTIONS = 6;

		#endregion

		#region Avatar System

		/// <summary>Skill gain multiplier for Avatar players</summary>
		public const double AVATAR_GAIN_MULTIPLIER = 1.5;

		/// <summary>Skill gain multiplier for Normal players</summary>
		public const double NORMAL_GAIN_MULTIPLIER = 1.0;

		#endregion

		#region Hunger System

		/// <summary>Hunger threshold for starving state (penalty)</summary>
		public const int HUNGER_STARVING = 3;

		/// <summary>Hunger threshold for hungry state (penalty)</summary>
		public const int HUNGER_HUNGRY = 10;

		/// <summary>Hunger threshold for normal state (bonus)</summary>
		public const int HUNGER_NORMAL = 20;

		/// <summary>Hunger threshold for well-fed state (bonus)</summary>
		public const int HUNGER_WELL_FED = 25;

		/// <summary>Hunger threshold for very well-fed state (bonus)</summary>
		public const int HUNGER_VERY_WELL_FED = 30;

		/// <summary>Hunger threshold for overfed state (maximum bonus)</summary>
		public const int HUNGER_OVERFED = 40;

		/// <summary>Skill gain penalty multiplier when starving</summary>
		public const double STARVING_PENALTY = 1.5;

		/// <summary>Skill gain penalty multiplier when hungry</summary>
		public const double HUNGRY_PENALTY = 1.25;

		/// <summary>Skill gain bonus multiplier when normal hunger</summary>
		public const double NORMAL_HUNGER_BONUS = 1.10;

		/// <summary>Skill gain bonus multiplier when well-fed</summary>
		public const double WELL_FED_BONUS = 1.25;

		/// <summary>Skill gain bonus multiplier when very well-fed</summary>
		public const double VERY_WELL_FED_BONUS = 1.50;

		/// <summary>Skill gain bonus multiplier when overfed</summary>
		public const double OVERFED_BONUS = 2.00;

		#endregion

		#region Creature Bonuses

		/// <summary>Skill gain multiplier for controlled creatures</summary>
		public const double CONTROLLED_CREATURE_BONUS = 1.45;

		#endregion

		#region Region Bonuses

		/// <summary>Skill gain bonus multiplier for Church of Justice region</summary>
		public const double CHURCH_OF_JUSTICE_BONUS = 1.25;

		/// <summary>Skill gain bonus multiplier for Basement region (SoulBound only)</summary>
		public const double BASEMENT_SOULBOUND_BONUS = 4.0;

		/// <summary>Divisor for dungeon difficulty bonus calculation</summary>
		public const double DIFFICULTY_DIVISOR = 5.0;

		/// <summary>Divisor for passive skill dungeon bonus</summary>
		public const double PASSIVE_SKILL_DIVISOR = 3.0;

		#endregion

		#region Skill Gain Ranges - Midland

		/// <summary>Skill threshold for tier 1 (0-25) in Midland</summary>
		public const double MIDLAND_THRESHOLD_1 = 25.0;

		/// <summary>Skill threshold for tier 2 (25.1-55) in Midland</summary>
		public const double MIDLAND_THRESHOLD_2 = 55.0;

		/// <summary>Skill threshold for tier 3 (55.1-70) in Midland</summary>
		public const double MIDLAND_THRESHOLD_3 = 70.0;

		/// <summary>Skill threshold for tier 4 (70.1-80) in Midland</summary>
		public const double MIDLAND_THRESHOLD_4 = 80.0;

		/// <summary>Skill threshold for tier 5 (80.1-85) in Midland</summary>
		public const double MIDLAND_THRESHOLD_5 = 85.0;

		/// <summary>Skill threshold for tier 6 (85.1-90) in Midland</summary>
		public const double MIDLAND_THRESHOLD_6 = 90.0;

		/// <summary>Skill threshold for tier 7 (90.1-95) in Midland</summary>
		public const double MIDLAND_THRESHOLD_7 = 95.0;

		/// <summary>Skill threshold for tier 8 (95.1-100) in Midland</summary>
		public const double MIDLAND_THRESHOLD_8 = 100.0;

		/// <summary>Skill threshold for tier 9 (100.1-105) in Midland</summary>
		public const double MIDLAND_THRESHOLD_9 = 105.0;

		/// <summary>Skill threshold for tier 10 (105.1-110) in Midland</summary>
		public const double MIDLAND_THRESHOLD_10 = 110.0;

		/// <summary>Skill threshold for tier 11 (110.1+) in Midland</summary>
		public const double MIDLAND_THRESHOLD_11 = 110.1;

		/// <summary>Midland tier 1 gain range minimum</summary>
		public const int MIDLAND_GAIN_TIER_1_MIN = 4;

		/// <summary>Midland tier 1 gain range maximum</summary>
		public const int MIDLAND_GAIN_TIER_1_MAX = 6;

		/// <summary>Midland tier 2 gain range minimum</summary>
		public const int MIDLAND_GAIN_TIER_2_MIN = 3;

		/// <summary>Midland tier 2 gain range maximum</summary>
		public const int MIDLAND_GAIN_TIER_2_MAX = 4;

		/// <summary>Midland tier 3 gain range minimum</summary>
		public const int MIDLAND_GAIN_TIER_3_MIN = 1;

		/// <summary>Midland tier 3 gain range maximum</summary>
		public const int MIDLAND_GAIN_TIER_3_MAX = 3;

		/// <summary>Midland tier 4 gain (fixed)</summary>
		public const int MIDLAND_GAIN_TIER_4 = 1;

		/// <summary>Midland tier 5 harder roll maximum</summary>
		public const int MIDLAND_HARDER_TIER_5_MAX = 2;

		/// <summary>Midland tier 6 harder roll maximum</summary>
		public const int MIDLAND_HARDER_TIER_6_MAX = 3;

		/// <summary>Midland tier 7 harder roll maximum</summary>
		public const int MIDLAND_HARDER_TIER_7_MAX = 4;

		/// <summary>Midland tier 8 harder roll maximum</summary>
		public const int MIDLAND_HARDER_TIER_8_MAX = 5;

		/// <summary>Midland tier 9 harder roll maximum</summary>
		public const int MIDLAND_HARDER_TIER_9_MAX = 6;

		/// <summary>Midland tier 10 harder roll maximum</summary>
		public const int MIDLAND_HARDER_TIER_10_MAX = 7;

		/// <summary>Midland tier 11 harder roll maximum</summary>
		public const int MIDLAND_HARDER_TIER_11_MAX = 8;

		/// <summary>Midland Focus/Meditation harder roll maximum</summary>
		public const int MIDLAND_FOCUS_MEDITATION_HARDER_MAX = 2;

		#endregion

		#region Skill Gain Ranges - Normal

		/// <summary>Skill threshold for tier 1 (0-25) in Normal regions</summary>
		public const double NORMAL_THRESHOLD_1 = 25.0;

		/// <summary>Skill threshold for tier 2 (25.1-55) in Normal regions</summary>
		public const double NORMAL_THRESHOLD_2 = 55.0;

		/// <summary>Skill threshold for tier 3 (55.1-70) in Normal regions</summary>
		public const double NORMAL_THRESHOLD_3 = 70.0;

		/// <summary>Skill threshold for tier 4 (70.1-80) in Normal regions</summary>
		public const double NORMAL_THRESHOLD_4 = 80.0;

		/// <summary>Skill threshold for tier 5 (80.1-85) in Normal regions</summary>
		public const double NORMAL_THRESHOLD_5 = 85.0;

		/// <summary>Skill threshold for tier 6 (85.1-90) in Normal regions</summary>
		public const double NORMAL_THRESHOLD_6 = 90.0;

		/// <summary>Skill threshold for tier 7 (90.1-95) in Normal regions</summary>
		public const double NORMAL_THRESHOLD_7 = 95.0;

		/// <summary>Skill threshold for tier 8 (95.1-100) in Normal regions</summary>
		public const double NORMAL_THRESHOLD_8 = 100.0;

		/// <summary>Skill threshold for tier 9 (100.1-105) in Normal regions</summary>
		public const double NORMAL_THRESHOLD_9 = 105.0;

		/// <summary>Skill threshold for tier 10 (105.1-110) in Normal regions</summary>
		public const double NORMAL_THRESHOLD_10 = 110.0;

		/// <summary>Skill threshold for tier 11 (110.1+) in Normal regions</summary>
		public const double NORMAL_THRESHOLD_11 = 110.1;

		/// <summary>Normal tier 1 gain range minimum</summary>
		public const int NORMAL_GAIN_TIER_1_MIN = 3;

		/// <summary>Normal tier 1 gain range maximum</summary>
		public const int NORMAL_GAIN_TIER_1_MAX = 5;

		/// <summary>Normal tier 2 gain range minimum</summary>
		public const int NORMAL_GAIN_TIER_2_MIN = 2;

		/// <summary>Normal tier 2 gain range maximum</summary>
		public const int NORMAL_GAIN_TIER_2_MAX = 3;

		/// <summary>Normal tier 3 gain range minimum</summary>
		public const int NORMAL_GAIN_TIER_3_MIN = 1;

		/// <summary>Normal tier 3 gain range maximum</summary>
		public const int NORMAL_GAIN_TIER_3_MAX = 2;

		/// <summary>Normal tier 4 gain (fixed)</summary>
		public const int NORMAL_GAIN_TIER_4 = 1;

		/// <summary>Normal tier 5 harder roll maximum</summary>
		public const int NORMAL_HARDER_TIER_5_MAX = 1;

		/// <summary>Normal tier 6 harder roll maximum</summary>
		public const int NORMAL_HARDER_TIER_6_MAX = 2;

		/// <summary>Normal tier 7 harder roll maximum</summary>
		public const int NORMAL_HARDER_TIER_7_MAX = 3;

		/// <summary>Normal tier 8 harder roll maximum</summary>
		public const int NORMAL_HARDER_TIER_8_MAX = 4;

		/// <summary>Normal tier 9 harder roll maximum</summary>
		public const int NORMAL_HARDER_TIER_9_MAX = 5;

		/// <summary>Normal tier 10 harder roll maximum</summary>
		public const int NORMAL_HARDER_TIER_10_MAX = 6;

		/// <summary>Normal tier 11 harder roll maximum</summary>
		public const int NORMAL_HARDER_TIER_11_MAX = 7;

		/// <summary>Normal Focus/Meditation harder roll maximum</summary>
		public const int NORMAL_FOCUS_MEDITATION_HARDER_MAX = 2;

		#endregion

		#region Stat Gain

		/// <summary>Divisor for stat gain chance calculation</summary>
		public const double STAT_GAIN_DIVISOR = 33.3;

		/// <summary>Default stat gain delay in minutes</summary>
		public const double DEFAULT_STAT_GAIN_DELAY_MINUTES = 15.0;

		/// <summary>Pet stat gain delay in minutes</summary>
		public const double PET_STAT_GAIN_DELAY_MINUTES = 5.0;

		/// <summary>High stat cap threshold</summary>
		public const int HIGH_STAT_CAP_THRESHOLD = 275;

		/// <summary>Maximum stat value for high stat cap</summary>
		public const int HIGH_STAT_CAP_MAX = 175;

		/// <summary>Maximum stat value for normal stat cap</summary>
		public const int NORMAL_STAT_CAP_MAX = 150;

		/// <summary>Fast gain threshold</summary>
		public const int FAST_GAIN_THRESHOLD = 2;

		/// <summary>Fast gain delay divisor</summary>
		public const int FAST_GAIN_DELAY_DIVISOR = 2;

		#endregion

		#region Special Skill Handling

		/// <summary>Skill gain bonus multiplier for Animal Lore when used on wild creatures (1.5 = 50% bonus)</summary>
		public const double ANIMAL_LORE_WILD_BONUS = 1.5;

		/// <summary>Fishing skill threshold requiring boat</summary>
		public const double FISHING_BOAT_REQUIREMENT = 60.0;

		/// <summary>THC skill boost chance multiplier</summary>
		public const double THC_BOOST_MULTIPLIER = 0.08;

		/// <summary>THC skill boost divisor</summary>
		public const int THC_BOOST_DIVISOR = 60;

		/// <summary>Minimum skill base for guaranteed gain</summary>
		public const double MIN_SKILL_BASE_FOR_GAIN = 10.0;

		/// <summary>Scroll of Alacrity gain multiplier minimum</summary>
		public const int SCROLL_ALACRITY_GAIN_MIN = 2;

		/// <summary>Scroll of Alacrity gain multiplier maximum</summary>
		public const int SCROLL_ALACRITY_GAIN_MAX = 4;

		/// <summary>Focus/Meditation refresh chance denominator</summary>
		public const int FOCUS_MEDITATION_REFRESH_CHANCE = 20;

		/// <summary>Skill milestone threshold (100.0 skill)</summary>
		public const int SKILL_MILESTONE_THRESHOLD = 1000;

		#endregion

		#region Church of Justice Region

		/// <summary>Church of Justice region X coordinate minimum</summary>
		public const int CHURCH_OF_JUSTICE_X_MIN = 2586;

		/// <summary>Church of Justice region X coordinate maximum</summary>
		public const int CHURCH_OF_JUSTICE_X_MAX = 2766;

		/// <summary>Church of Justice region Y coordinate minimum</summary>
		public const int CHURCH_OF_JUSTICE_Y_MIN = 1402;

		/// <summary>Church of Justice region Y coordinate maximum</summary>
		public const int CHURCH_OF_JUSTICE_Y_MAX = 1595;

		/// <summary>Church of Justice guild name</summary>
		public const string CHURCH_OF_JUSTICE_GUILD_NAME = "the church of justice";

		#endregion
	}
}


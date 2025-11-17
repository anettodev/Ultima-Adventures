using System;

namespace Server.Items
{
	/// <summary>
	/// Centralized constants for bandage healing mechanics.
	/// Extracted from Bandage.cs to improve maintainability.
	/// </summary>
	public static class BandageConstants
	{
		#region Range Constants

		/// <summary>Default bandage usage range in tiles</summary>
		public const int RANGE_DEFAULT = 2;

		/// <summary>Extended range when FriendsAvoidHeels is enabled</summary>
		public const int RANGE_FRIENDS_AVOID_HEELS = 5;

		/// <summary>Range required for pet owner to resurrect pet</summary>
		public const int RANGE_PET_OWNER_RESURRECT = 3;

		/// <summary>Range required for pet friend to resurrect pet</summary>
		public const int RANGE_PET_FRIEND_RESURRECT = 3;

		#endregion

		#region Skill Requirements

		/// <summary>Minimum Healing skill required to resurrect</summary>
		public const double SKILL_RESURRECT_MIN_HEALING = 80.0;

		/// <summary>Minimum Anatomy skill required to resurrect</summary>
		public const double SKILL_RESURRECT_MIN_ANATOMY = 80.0;

		/// <summary>Minimum Healing skill required to cure poison</summary>
		public const double SKILL_CURE_MIN_HEALING = 60.0;

		/// <summary>Minimum Anatomy skill required to cure poison</summary>
		public const double SKILL_CURE_MIN_ANATOMY = 60.0;

		/// <summary>Minimum skill required to resurrect henchmen</summary>
		public const double SKILL_HENCHMAN_RESURRECT_MIN = 80.0;

		#endregion

		#region Healing Calculations

		/// <summary>Base value for resurrection chance calculation</summary>
		public const double HEALING_RESURRECT_BASE = 68.0;

		/// <summary>Divisor for resurrection chance calculation</summary>
		public const double HEALING_RESURRECT_DIVISOR = 50.0;

		/// <summary>Base cure chance for Lesser poison (level 0)</summary>
		public const double POISON_CURE_BASE_LESSER = 0.50;

		/// <summary>Base cure chance for Regular poison (level 1)</summary>
		public const double POISON_CURE_BASE_REGULAR = 0.40;

		/// <summary>Base cure chance for Greater poison (level 2)</summary>
		public const double POISON_CURE_BASE_GREATER = 0.30;

		/// <summary>Base cure chance for Deadly poison (level 3)</summary>
		public const double POISON_CURE_BASE_DEADLY = 0.12;

		/// <summary>Base cure chance for Lethal poison (level 4)</summary>
		public const double POISON_CURE_BASE_LETHAL = 0.03;

		/// <summary>Bonus to cure chance per 10 points of Healing skill</summary>
		public const double POISON_CURE_SKILL_BONUS_PER_10 = 0.01;

		/// <summary>Base value for hit point healing chance calculation</summary>
		public const double HEALING_HP_BASE = 10.0;

		/// <summary>Divisor for hit point healing chance calculation</summary>
		public const double HEALING_HP_DIVISOR = 100.0;

		/// <summary>Reduction to healing success chance (10% penalty)</summary>
		public const double HEALING_CHANCE_REDUCTION = 0.10;

		/// <summary>Penalty per slip for success chance</summary>
		public const double SLIP_PENALTY_PER_SLIP = 0.02;

		/// <summary>Penalty per slip for healing amount (AOS)</summary>
		public const double SLIP_HEALING_PENALTY_AOS = 0.35;

		/// <summary>Penalty per slip for healing amount (Classic)</summary>
		public const int SLIP_HEALING_PENALTY_CLASSIC = 4;

		/// <summary>Penalty per poison level for cure chance</summary>
		public const double POISON_LEVEL_PENALTY = 0.1;

		/// <summary>Minimum healing amount possible</summary>
		public const int HEALING_MINIMUM_AMOUNT = 1;

		/// <summary>Base minimum healing amount</summary>
		public const double HEALING_MIN_BASE = 25.0;

		/// <summary>Base maximum healing amount</summary>
		public const double HEALING_MAX_BASE = 100.0;

		/// <summary>Divisor for skill-based healing calculations</summary>
		public const int HEALING_CALC_DIVISOR = 2;

		/// <summary>Divisor for creature healing bonus</summary>
		public const int CREATURE_HEALING_DIVISOR = 100;

		/// <summary>Maximum healing amount cap in hit points</summary>
		public const int HEALING_AMOUNT_CAP = 100;

		/// <summary>Healing amount reduction multiplier (half healing)</summary>
		public const double HEALING_AMOUNT_REDUCTION = 0.5;

		#endregion

		#region Skill Gain Requirements

		/// <summary>Skill level where poison cure gains begin</summary>
		public const double SKILL_GAIN_POISON_CURE_MIN = 60.1;

		/// <summary>Skill level where poison cure gains end (before Lethal)</summary>
		public const double SKILL_GAIN_POISON_CURE_MAX = 90.0;

		/// <summary>Skill level where resurrection gains begin</summary>
		public const double SKILL_GAIN_RESURRECTION_MIN = 80.1;

		/// <summary>Skill level where resurrection gains end</summary>
		public const double SKILL_GAIN_RESURRECTION_MAX = 110.0;

		/// <summary>Skill level where Lethal poison cure gains begin</summary>
		public const double SKILL_GAIN_LETHAL_POISON_MIN = 110.1;

		/// <summary>Skill level where all training caps</summary>
		public const double SKILL_GAIN_LETHAL_POISON_MAX = 120.0;

		#endregion

		#region Veterinary Skill Gains

		/// <summary>Maximum ratio for veterinary bonus skill gains</summary>
		public const int VET_MAX_RATIO = 10;

		/// <summary>Minimum ratio required for veterinary bonus skill gains</summary>
		public const int VET_MIN_RATIO_FOR_BONUS = 2;

		/// <summary>Divisor for veterinary ratio calculation</summary>
		public const int VET_RATIO_DIVISOR = 2;

		/// <summary>Decrement value for veterinary ratio loop</summary>
		public const int VET_RATIO_DECREMENT = 1;

		#endregion

		#region Timing Constants

		/// <summary>Base bandage application time in seconds</summary>
		public const int BANDAGE_BASE_SECONDS = 4;

		/// <summary>Dexterity cap for bandage speed calculations</summary>
		public const int DEX_CAP_FOR_BANDAGE_SPEED = 300;

		/// <summary>Base dexterity value for Midland calculations</summary>
		public const int MIDLAND_DEX_BASE = 150;

		/// <summary>Divisor for Midland dexterity calculations</summary>
		public const int MIDLAND_DEX_DIVISOR = 25;

		/// <summary>Divisor for Soulbound dexterity calculations</summary>
		public const int SOULBOUND_DEX_DIVISOR = 150;

		/// <summary>Milliseconds per second conversion factor</summary>
		public const int MILLISECONDS_PER_SECOND = 1000;

		/// <summary>Bandage speed multiplier (30% slower = 1.3x time)</summary>
		public const double BANDAGE_SPEED_MULTIPLIER = 1.3;

		/// <summary>Minimum dexseconds threshold for veterinary healing</summary>
		public const int VET_DEXSECONDS_THRESHOLD = 2;

		/// <summary>Additional seconds for veterinary healing when above threshold</summary>
		public const int VET_ADDITIONAL_SECONDS = 1;

		/// <summary>Default veterinary healing time in seconds</summary>
		public const int VET_DEFAULT_SECONDS = 3;

		/// <summary>DEX threshold for AOS healing calculation mode</summary>
		public const int AOS_DEX_THRESHOLD = 204;

		/// <summary>Base seconds for AOS healing calculation</summary>
		public const double AOS_BASE_SECONDS = 3.2;

		/// <summary>Divisor for sin calculation in AOS healing</summary>
		public const int AOS_SIN_DIVISOR = 130;

		/// <summary>Multiplier for sin calculation in AOS healing</summary>
		public const double AOS_SIN_MULTIPLIER = 2.5;

		/// <summary>Minimum seconds for high DEX AOS healing</summary>
		public const double AOS_MIN_SECONDS = 0.7;

		/// <summary>Classic healing time for DEX >= 100</summary>
		public const double CLASSIC_FAST_SECONDS = 3.0;

		/// <summary>Classic healing time for DEX >= 40</summary>
		public const double CLASSIC_MEDIUM_SECONDS = 4.0;

		/// <summary>DEX threshold for fast classic healing</summary>
		public const int CLASSIC_FAST_DEX = 100;

		/// <summary>DEX threshold for medium classic healing</summary>
		public const int CLASSIC_MEDIUM_DEX = 40;

		#endregion

		#region Player State Requirements

		/// <summary>Minimum hunger level required to receive healing</summary>
		public const int HUNGER_MIN_FOR_HEALING = 5;

		#endregion

		#region Map and Resurrection Constants

		/// <summary>Height check for resurrection location validity</summary>
		public const int RESURRECT_FIT_CHECK_HEIGHT = 16;

		/// <summary>Height check for resurrection location fit test</summary>
		public const int RESURRECT_LOCATION_FIT_HEIGHT = 16;

		/// <summary>Skill loss per pet resurrection</summary>
		public const double PET_SKILL_LOSS_PER_RESURRECT = 0.1;

		#endregion

		#region Display and Effects

		/// <summary>Default bandage hue</summary>
		public const int HUE_BANDAGE_DEFAULT = 0;

		/// <summary>Overhead message color</summary>
		public const int MESSAGE_COLOR_OVERHEAD = 1150;

		/// <summary>Sound effect for resurrection</summary>
		public const int SOUND_RESURRECT = 0x214;

		/// <summary>Visual effect for resurrection</summary>
		public const int EFFECT_RESURRECT = 0x376A;

		/// <summary>Resurrection effect speed</summary>
		public const int EFFECT_RESURRECT_SPEED = 10;

		/// <summary>Resurrection effect duration</summary>
		public const int EFFECT_RESURRECT_DURATION = 16;

		/// <summary>Sound effect for successful bandage application</summary>
		public const int SOUND_BANDAGE_SUCCESS = 0x57;

		#endregion

		#region Cliloc Message IDs

		/// <summary>"Your fingers slip!"</summary>
		public const int CLILOC_FINGERS_SLIP = 500961;

		/// <summary>"You were unable to finish your work before you died."</summary>
		public const int CLILOC_DIED_BEFORE_FINISH = 500962;

		/// <summary>"You did not stay close enough to heal your target."</summary>
		public const int CLILOC_NOT_CLOSE_ENOUGH = 500963;

		/// <summary>"You are able to resurrect your patient."</summary>
		public const int CLILOC_RESURRECT_SUCCESS = 500965;

		/// <summary>"You are unable to resurrect your patient."</summary>
		public const int CLILOC_RESURRECT_FAILURE = 500966;

		/// <summary>"You heal what little damage your patient had."</summary>
		public const int CLILOC_HEAL_LITTLE_DAMAGE = 500967;

		/// <summary>"You apply the bandages, but they barely help."</summary>
		public const int CLILOC_BANDAGES_BARELY_HELP = 500968;

		/// <summary>"You finish applying the bandages."</summary>
		public const int CLILOC_FINISH_BANDAGES = 500969;

		/// <summary>"Bandages can not be used on that."</summary>
		public const int CLILOC_CANNOT_USE_ON_THAT = 500970;

		/// <summary>"You cannot heal that."</summary>
		public const int CLILOC_CANNOT_HEAL = 500951;

		/// <summary>"That being is not damaged!"</summary>
		public const int CLILOC_NOT_DAMAGED = 500955;

		/// <summary>"Who will you use the bandages on?"</summary>
		public const int CLILOC_WHO_TO_BANDAGE = 500948;

		/// <summary>"You are too far away to do that."</summary>
		public const int CLILOC_TOO_FAR_AWAY = 500295;

		/// <summary>"You begin applying the bandages."</summary>
		public const int CLILOC_BEGIN_BANDAGES = 500956;

		/// <summary>"Target can not be resurrected at that location."</summary>
		public const int CLILOC_CANNOT_RESURRECT_LOCATION = 501042;

		/// <summary>"Thou can not be resurrected there!"</summary>
		public const int CLILOC_CANNOT_RESURRECT_THERE = 502391;

		/// <summary>"The veil of death in this area is too strong..."</summary>
		public const int CLILOC_VEIL_OF_DEATH_TOO_STRONG = 1010395;

		/// <summary>"You have cured the target of all poisons."</summary>
		public const int CLILOC_CURED_TARGET = 1010058;

		/// <summary>"You have been cured of all poisons."</summary>
		public const int CLILOC_BEEN_CURED = 1010059;

		/// <summary>"You have failed to cure your target!"</summary>
		public const int CLILOC_CURE_FAILED = 1010060;

		/// <summary>"You bind the wound and stop the bleeding"</summary>
		public const int CLILOC_STOP_BLEEDING = 1060088;

		/// <summary>"The bleeding wounds have healed..."</summary>
		public const int CLILOC_BLEEDING_HEALED = 1060167;

		/// <summary>"The wound is too severe to heal."</summary>
		public const int CLILOC_MORTAL_WOUND_SELF = 1005000;

		/// <summary>"You apply the bandages, but the wound is too severe."</summary>
		public const int CLILOC_MORTAL_WOUND_OTHER = 1010398;

		/// <summary>"You are able to resurrect the creature."</summary>
		public const int CLILOC_RESURRECT_CREATURE_SUCCESS = 503255;

		/// <summary>"You fail to resurrect the creature."</summary>
		public const int CLILOC_RESURRECT_CREATURE_FAILURE = 503256;

		/// <summary>"The pet's owner must be nearby to attempt resurrection."</summary>
		public const int CLILOC_OWNER_MUST_BE_NEARBY = 1049670;

		/// <summary>"Attempting to heal you."</summary>
		public const int CLILOC_ATTEMPTING_HEAL = 1008078;

		#endregion
	}
}

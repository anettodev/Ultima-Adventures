using System;

namespace Server.Misc
{
	/// <summary>
	/// Centralized constants for food decay system calculations and mechanics.
	/// Extracted from FoodDecay.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class FoodDecayConstants
	{
		#region Timer Settings

		/// <summary>Minimum decay interval in minutes</summary>
		public const int DECAY_INTERVAL_MIN_MINUTES = 30;

		/// <summary>Maximum decay interval in minutes</summary>
		public const int DECAY_INTERVAL_MAX_MINUTES = 60;

		#endregion

		#region Camping Skill Protection

		/// <summary>Camping skill threshold required to prevent hunger/thirst decay (50.0 skill points)</summary>
		public const double CAMPING_PROTECTION_THRESHOLD = 50.0;

		#endregion

		#region Hunger Decay Values

		/// <summary>Hunger decay when walking (per interval)</summary>
		public const int HUNGER_DECAY_WALKING = 1;

		/// <summary>Minimum hunger decay when running (per interval)</summary>
		public const int HUNGER_DECAY_RUNNING_MIN = 1;

		/// <summary>Maximum hunger decay when running (per interval)</summary>
		public const int HUNGER_DECAY_RUNNING_MAX = 3;

		#endregion

		#region THC (Toxic Hunger Consumption) System

		/// <summary>Divisor for THC percentage calculation</summary>
		public const int THC_DIVISOR = 100;

		/// <summary>Minimum additional hunger decay from THC (per interval)</summary>
		public const int THC_HUNGER_DECAY_MIN = 1;

		/// <summary>Maximum additional hunger decay from THC (per interval)</summary>
		public const int THC_HUNGER_DECAY_MAX = 2;

		#endregion

		#region Thirst Decay Values

		/// <summary>Thirst decay when standing still (per interval)</summary>
		public const int THIRST_DECAY_STANDING = 1;

		/// <summary>Minimum thirst decay when walking (per interval)</summary>
		public const int THIRST_DECAY_WALKING_MIN = 1;

		/// <summary>Maximum thirst decay when walking (per interval)</summary>
		public const int THIRST_DECAY_WALKING_MAX = 2;

		/// <summary>Minimum thirst decay when running (per interval)</summary>
		public const int THIRST_DECAY_RUNNING_MIN = 2;

		/// <summary>Maximum thirst decay when running (per interval)</summary>
		public const int THIRST_DECAY_RUNNING_MAX = 3;

		#endregion

		#region Warning Thresholds

		/// <summary>Low warning threshold (extremely hungry/thirsty)</summary>
		public const int WARNING_THRESHOLD_LOW = 5;

		/// <summary>Medium warning threshold (getting hungry/thirsty)</summary>
		public const int WARNING_THRESHOLD_MEDIUM = 10;

		#endregion

		#region Starvation/Dehydration Effects

		/// <summary>Hits loss per interval when starving (hunger = 0)</summary>
		public const int STARVATION_HITS_LOSS = 3;

		/// <summary>Stamina loss per interval when starving (hunger = 0)</summary>
		public const int STARVATION_STAM_LOSS = 3;

		/// <summary>Mana loss per interval when starving (hunger = 0)</summary>
		public const int STARVATION_MANA_LOSS = 3;

		/// <summary>Hits loss per interval when dehydrated (thirst = 0)</summary>
		public const int DEHYDRATION_HITS_LOSS = 3;

		/// <summary>Stamina loss per interval when dehydrated (thirst = 0)</summary>
		public const int DEHYDRATION_STAM_LOSS = 3;

		/// <summary>Mana loss per interval when dehydrated (thirst = 0)</summary>
		public const int DEHYDRATION_MANA_LOSS = 3;

		#endregion

		#region Creature Decay

		/// <summary>Hunger/thirst decay amount for controlled creatures (per interval)</summary>
		public const int CREATURE_DECAY_AMOUNT = 1;

		#endregion

		#region Message Colors

		/// <summary>Color for emote messages (0xB1F)</summary>
		public const int EMOTE_MESSAGE_COLOR = 0xB1F;

		#endregion
	}
}


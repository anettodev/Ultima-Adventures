using System;

namespace Server.SkillHandlers
{
	/// <summary>
	/// Centralized constants for Spirit Speak skill calculations.
	/// Extracted from SpiritSpeak.cs to improve maintainability.
	/// </summary>
	public static class SpiritSpeakConstants
	{
		#region Timing

		/// <summary>Delay when spell is casting (seconds)</summary>
		public const double CAST_DELAY_SECONDS = 5.0;

		/// <summary>Base cast delay (seconds)</summary>
		public const double CAST_DELAY_BASE_SECONDS = 1.0;

		/// <summary>Cooldown between skill uses (seconds)</summary>
		public const double SKILL_COOLDOWN_SECONDS = 1.5;

		/// <summary>Minimum duration for hear ghosts effect (seconds)</summary>
		public const int MIN_DURATION_SECONDS = 10;

		/// <summary>Maximum duration for hear ghosts effect (seconds)</summary>
		public const int MAX_DURATION_SECONDS = 60;

		/// <summary>Default timer duration (minutes)</summary>
		public const double TIMER_DEFAULT_MINUTES = 2.0;

		#endregion

		#region Skill Checks

		/// <summary>Minimum value for skill check</summary>
		public const int SKILL_CHECK_MIN = 0;

		/// <summary>Maximum value for skill check</summary>
		public const int SKILL_CHECK_MAX = 100;

		/// <summary>Minimum value for skill gain check</summary>
		public const double SKILL_GAIN_MIN = 0.0;

		/// <summary>Maximum value for skill gain check</summary>
		public const double SKILL_GAIN_MAX = 120.0;

		/// <summary>Minimum skill required to hear ghosts</summary>
		public const double MIN_SKILL_TO_HEAR_GHOSTS = 50.0;

		/// <summary>Base success chance at minimum skill (30% at 50.0)</summary>
		public const double BASE_SUCCESS_CHANCE = 0.30;

		/// <summary>Success chance increase per 10 skill points above minimum</summary>
		public const double SUCCESS_CHANCE_PER_10_SKILL = 0.10;

		/// <summary>Divisor for success chance calculation</summary>
		public const double SUCCESS_CHANCE_DIVISOR = 100.0;

		#endregion

		#region Duration Calculation

		/// <summary>Divisor for duration calculation based on skill</summary>
		public const int DURATION_SKILL_DIVISOR = 50;

		/// <summary>Multiplier for duration calculation</summary>
		public const int DURATION_MULTIPLIER = 90;

		#endregion

		#region Combat Calculation

		/// <summary>Multiplier for Spirit Speak skill contribution</summary>
		public const double SPIRIT_SPEAK_SKILL_MULTIPLIER = 0.25;

		/// <summary>Multiplier for Wrestling skill contribution</summary>
		public const double WRESTLING_SKILL_MULTIPLIER = 0.15;

		/// <summary>Bonus addition for max damage calculation</summary>
		public const int DAMAGE_BONUS_ADDITION = 4;

		#endregion

		#region Balance Effects

		/// <summary>Balance effect when channeling corpse (soulbound)</summary>
		public const int SOULBOUND_BALANCE_CORPSE = 2;

		/// <summary>Balance effect when channeling corpse (non-soulbound)</summary>
		public const int NON_SOULBOUND_BALANCE_CORPSE = 1;

		/// <summary>Min balance effect when destroying bones (soulbound)</summary>
		public const int SOULBOUND_BALANCE_BONES_MIN = 5;

		/// <summary>Max balance effect when destroying bones (soulbound)</summary>
		public const int SOULBOUND_BALANCE_BONES_MAX = 8;

		/// <summary>Min balance effect when destroying bones (non-soulbound)</summary>
		public const int NON_SOULBOUND_BALANCE_BONES_MIN = 3;

		/// <summary>Max balance effect when destroying bones (non-soulbound)</summary>
		public const int NON_SOULBOUND_BALANCE_BONES_MAX = 5;

		#endregion

		#region Resources

		/// <summary>Base mana cost for self-healing</summary>
		public const int BASE_MANA_COST = 20;

		/// <summary>Chance to restore mana on success</summary>
		public const double MANA_RESTORE_CHANCE = 0.33;

		/// <summary>Minimum mana restored</summary>
		public const int MANA_RESTORE_MIN = 1;

		/// <summary>Maximum mana restored</summary>
		public const int MANA_RESTORE_MAX = 10;

		#endregion

		#region Range and Visual

		/// <summary>Range to search for corpses</summary>
		public const int CORPSE_SEARCH_RANGE = 3;

		/// <summary>Hue applied to channeled corpses</summary>
		public const int CHANNELED_CORPSE_HUE = 0x835;

		/// <summary>Sound played on successful spirit speak</summary>
		public const int SPIRIT_SPEAK_SOUND = 0x24A;

		/// <summary>Particle effect ID for ghost hearing (light blue)</summary>
		public const int PARTICLE_EFFECT_ID = 0x375A;

		/// <summary>Particle effect hue for ghost hearing (light blue)</summary>
		public const int PARTICLE_EFFECT_HUE = 1153;

		/// <summary>Interval between particle effects (seconds)</summary>
		public const double PARTICLE_EFFECT_INTERVAL_SECONDS = 3.0;

		#endregion
	}
}

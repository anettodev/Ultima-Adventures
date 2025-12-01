using System;

namespace Server.Items
{
	/// <summary>
	/// Centralized constants for Frostbite Potion calculations and mechanics.
	/// Extracted from BaseFrostbitePotion.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class FrostbiteConstants
	{
		#region Damage Constants

		/// <summary>Minimum damage for regular frostbite potion</summary>
		public const int REGULAR_MIN_DAMAGE = 10;

		/// <summary>Maximum damage for regular frostbite potion</summary>
		public const int REGULAR_MAX_DAMAGE = 20;

		/// <summary>Minimum damage for greater frostbite potion</summary>
		public const int GREATER_MIN_DAMAGE = 20;

		/// <summary>Maximum damage for greater frostbite potion</summary>
		public const int GREATER_MAX_DAMAGE = 30;

		#endregion

	#region Effect Constants

	/// <summary>Paralyze chance per tick (50%)</summary>
	public const double PARALYZE_CHANCE = 0.50;

	/// <summary>Explosion sound effect ID</summary>
	public const int EXPLOSION_SOUND_ID = 0x20C;

	/// <summary>Ice damage sound effect ID (for ice patches)</summary>
	public const int ICE_DAMAGE_SOUND_ID = 0x108;

	/// <summary>Flying potion item ID (during throw)</summary>
	public const int FLYING_POTION_ITEM_ID = 0xF0D;

	/// <summary>Ice patch item ID (ground hazard)</summary>
	public const int ICE_PATCH_ITEM_ID = 0x3400;

	/// <summary>Ice patch hue (cyan/blue)</summary>
	public const int ICE_PATCH_HUE = 0xB78;

	/// <summary>Paralyze sound effect ID (same as ParalyzeField - for explosion paralyze)</summary>
	public const int PARALYZE_SOUND_ID = 0x204;

	/// <summary>Paralyze visual effect ID (same as ParalyzeField - for explosion paralyze)</summary>
	public const int PARALYZE_EFFECT_ID = 0x376A;

	/// <summary>Paralyze effect count (same as ParalyzeField)</summary>
	public const int PARALYZE_EFFECT_COUNT = 10;

	/// <summary>Paralyze effect duration (same as ParalyzeField)</summary>
	public const int PARALYZE_EFFECT_DURATION = 16;

	/// <summary>Ice mist effect ID (for resist visual)</summary>
	public const int ICE_MIST_EFFECT_ID = 0x376A;

	/// <summary>Ice mist effect speed</summary>
	public const int ICE_MIST_EFFECT_SPEED = 10;

	/// <summary>Ice mist effect duration</summary>
	public const int ICE_MIST_EFFECT_DURATION = 15;

	/// <summary>Ice mist effect number</summary>
	public const int ICE_MIST_EFFECT_NUMBER = 5013;

	/// <summary>Ice crack sound (for resist)</summary>
	public const int ICE_CRACK_SOUND_ID = 0x28;

	/// <summary>Ice hit particle effect ID (for ice patch cold damage)</summary>
	public const int ICE_HIT_EFFECT_ID = 0x374A;

	/// <summary>Ice hit particle count</summary>
	public const int ICE_HIT_EFFECT_COUNT = 10;

	/// <summary>Ice hit particle speed</summary>
	public const int ICE_HIT_EFFECT_SPEED = 15;

	/// <summary>Ice hit particle effect number</summary>
	public const int ICE_HIT_EFFECT_NUMBER = 5013;

	#endregion

		#region Timing Constants

		/// <summary>Base cooldown between frostbite potion throws (seconds)</summary>
		public const double BASE_COOLDOWN_SECONDS = 30.0;

		/// <summary>Flight time for thrown potion (seconds)</summary>
		public const double THROW_FLIGHT_TIME = 1.5;

	/// <summary>Duration ice patches remain on ground (seconds)</summary>
	public const double ICE_PATCH_DURATION_SECONDS = 10.0;

	/// <summary>Timer tick interval for paralyze attempts (seconds) - rolls every 2 seconds</summary>
	public const double TIMER_TICK_INTERVAL = 2.0;

		/// <summary>Throw cooldown to prevent macro exploits (seconds)</summary>
		public const double THROW_COOLDOWN_SECONDS = 1.0;

		#endregion

		#region Range Constants

		/// <summary>Maximum throw range (tiles)</summary>
		public const int THROW_RANGE = 12;

		/// <summary>Ice field explosion radius (tiles)</summary>
		public const int EXPLOSION_RADIUS = 2;

		/// <summary>Range to check for mobiles in ice patch (0 = exact tile)</summary>
		public const int ICE_DAMAGE_RANGE = 0;

		/// <summary>Z-axis height check for mobile detection</summary>
		public const int Z_AXIS_HEIGHT_CHECK = 16;

		/// <summary>Z-axis height check for item fitting</summary>
		public const int ITEM_HEIGHT_CHECK = 12;

		#endregion

		#region Alchemy Calculation Constants

		/// <summary>Primary divisor for alchemy skill bonus calculation</summary>
		public const int ALCHEMY_DIVISOR_PRIMARY = 125;

		/// <summary>Secondary divisor for alchemy skill bonus calculation</summary>
		public const int ALCHEMY_DIVISOR_SECONDARY = 250;

		#endregion

		#region Chemist Bonus Constants

		/// <summary>Base scalar for cooldown calculation</summary>
		public const double CHEMIST_SCALAR_BASE = 1.0;

		/// <summary>Scalar bonus per enhance potion point for Chemists</summary>
		public const double CHEMIST_SCALAR_BONUS_PER_ENHANCE = 0.02;

		#endregion

		#region Animation Constants

		/// <summary>Flying potion animation speed</summary>
		public const int POTION_FLIGHT_SPEED = 7;

		#endregion

		#region Message Color Constants

		/// <summary>Message color for errors and cooldowns (red)</summary>
		public const int MSG_COLOR_ERROR = 0x22;

		/// <summary>Message color for feedback (cyan)</summary>
		public const int MSG_COLOR_FEEDBACK = 0x59;

		#endregion
	}
}


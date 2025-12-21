namespace Server.Mobiles
{
	/// <summary>
	/// Centralized constants for TownGuards NPC calculations and mechanics.
	/// Extracted from TownGuards.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class TownGuardsConstants
	{
		#region Stats

		/// <summary>Minimum strength value</summary>
		public const int STAT_STR_MIN = 200;

		/// <summary>Maximum strength value</summary>
		public const int STAT_STR_MAX = 300;

		/// <summary>Minimum dexterity value</summary>
		public const int STAT_DEX_MIN = 200;

		/// <summary>Maximum dexterity value</summary>
		public const int STAT_DEX_MAX = 300;

		/// <summary>Minimum intelligence value</summary>
		public const int STAT_INT_MIN = 200;

		/// <summary>Maximum intelligence value</summary>
		public const int STAT_INT_MAX = 300;

		/// <summary>Minimum hits value</summary>
		public const int HITS_MIN = 500;

		/// <summary>Maximum hits value</summary>
		public const int HITS_MAX = 5000;

		/// <summary>Minimum damage value</summary>
		public const int DAMAGE_MIN = 200;

		/// <summary>Maximum damage value</summary>
		public const int DAMAGE_MAX = 500;

		/// <summary>Virtual armor value</summary>
		public const int VIRTUAL_ARMOR = 3000;

		#endregion

		#region Skills

		/// <summary>Skill value for all guard skills</summary>
		public const double SKILL_VALUE = 120.0;

		#endregion

		#region Appearance

		/// <summary>Name hue for guards</summary>
		public const int NAME_HUE = 1154;

		/// <summary>Message color for guard communications</summary>
		public const int MESSAGE_COLOR = 1153;

		#endregion

		#region Weapon Configuration

		/// <summary>Weapon maximum hit points</summary>
		public const int WEAPON_MAX_HIT_POINTS = 1000;

		/// <summary>Weapon hit points</summary>
		public const int WEAPON_HIT_POINTS = 1000;

		/// <summary>Weapon minimum damage</summary>
		public const int WEAPON_MIN_DAMAGE = 100;

		/// <summary>Weapon maximum damage</summary>
		public const int WEAPON_MAX_DAMAGE = 500;

		#endregion

		#region Equipment Item IDs

		/// <summary>Helm item ID - Standard plate helm</summary>
		public const int HELM_TYPE_STANDARD = 0x140E;

		/// <summary>Helm item ID - Alternative plate helm</summary>
		public const int HELM_TYPE_ALTERNATIVE = 0x1412;

		/// <summary>Helm item ID - Ornate helm</summary>
		public const int HELM_TYPE_ORNATE = 0x2645;

		/// <summary>Helm item ID - Gargoyle helm</summary>
		public const int HELM_TYPE_GARGOYLE = 0x2FBB;

		/// <summary>Shield item ID - Type 1</summary>
		public const int SHIELD_TYPE_1 = 0x1B72;

		/// <summary>Shield item ID - Type 2</summary>
		public const int SHIELD_TYPE_2 = 0x1B74;

		/// <summary>Shield item ID - Type 3</summary>
		public const int SHIELD_TYPE_3 = 0x1B76;

		/// <summary>Shield item ID - Type 4</summary>
		public const int SHIELD_TYPE_4 = 0x1B7A;

		/// <summary>Shield item ID - Type 5</summary>
		public const int SHIELD_TYPE_5 = 0x1B7B;

		/// <summary>Shield item ID - Type 6</summary>
		public const int SHIELD_TYPE_6 = 0x1BC3;

		/// <summary>Shield item ID - Type 7</summary>
		public const int SHIELD_TYPE_7 = 0x1BC4;

		#endregion

		#region Effects

		/// <summary>Particle effect ID for teleportation</summary>
		public const int EFFECT_PARTICLE_ID = 0x3728;

		/// <summary>Sound effect ID for teleportation</summary>
		public const int EFFECT_SOUND_ID = 0x201;

		/// <summary>Particle count for teleportation effect</summary>
		public const int EFFECT_PARTICLE_COUNT = 8;

		/// <summary>Particle speed for teleportation effect</summary>
		public const int EFFECT_PARTICLE_SPEED = 20;

		/// <summary>Particle hue for teleportation effect</summary>
		public const int EFFECT_PARTICLE_HUE = 5042;

		/// <summary>Sound effect ID for bounty reward</summary>
		public const int SOUND_BOUNTY_REWARD = 0x2E6;

		/// <summary>Particle effect ID for death prevention</summary>
		public const int EFFECT_DEATH_PARTICLE_ID = 0x376A;

		/// <summary>Particle count for death prevention effect</summary>
		public const int EFFECT_DEATH_PARTICLE_COUNT = 9;

		/// <summary>Particle speed for death prevention effect</summary>
		public const int EFFECT_DEATH_PARTICLE_SPEED = 32;

		/// <summary>Particle hue for death prevention effect</summary>
		public const int EFFECT_DEATH_PARTICLE_HUE = 5030;

		/// <summary>Sound effect ID for death prevention</summary>
		public const int SOUND_DEATH_PREVENTION = 0x202;

		#endregion

		#region Timer and Range

		/// <summary>Timer delay in seconds for walk away combat check</summary>
		public const int TIMER_DELAY_SECONDS = 5;

		/// <summary>Home range offset for guard teleportation</summary>
		public const int HOME_RANGE_OFFSET = 15;

		#endregion

		#region Bounty Rewards

		/// <summary>Fame divisor for pirate bounty rewards</summary>
		public const int PIRATE_BOUNTY_FAME_DIVISOR = 5;

		/// <summary>Karma multiplier for pirate bounty (negative)</summary>
		public const int PIRATE_BOUNTY_KARMA_MULTIPLIER = -1;

		/// <summary>Minimum karma reward for Thief head</summary>
		public const int BOUNTY_THIEF_KARMA_MIN = 40;

		/// <summary>Maximum karma reward for Thief head</summary>
		public const int BOUNTY_THIEF_KARMA_MAX = 60;

		/// <summary>Minimum gold reward for Thief head</summary>
		public const int BOUNTY_THIEF_GOLD_MIN = 80;

		/// <summary>Maximum gold reward for Thief head</summary>
		public const int BOUNTY_THIEF_GOLD_MAX = 120;

		/// <summary>Minimum karma reward for Bandit head</summary>
		public const int BOUNTY_BANDIT_KARMA_MIN = 20;

		/// <summary>Maximum karma reward for Bandit head</summary>
		public const int BOUNTY_BANDIT_KARMA_MAX = 30;

		/// <summary>Minimum gold reward for Bandit head</summary>
		public const int BOUNTY_BANDIT_GOLD_MIN = 30;

		/// <summary>Maximum gold reward for Bandit head</summary>
		public const int BOUNTY_BANDIT_GOLD_MAX = 40;

		/// <summary>Minimum karma reward for Brigand head</summary>
		public const int BOUNTY_BRIGAND_KARMA_MIN = 30;

		/// <summary>Maximum karma reward for Brigand head</summary>
		public const int BOUNTY_BRIGAND_KARMA_MAX = 40;

		/// <summary>Minimum gold reward for Brigand head</summary>
		public const int BOUNTY_BRIGAND_GOLD_MIN = 50;

		/// <summary>Maximum gold reward for Brigand head</summary>
		public const int BOUNTY_BRIGAND_GOLD_MAX = 80;

		/// <summary>Minimum karma reward for Pirate head</summary>
		public const int BOUNTY_PIRATE_KARMA_MIN = 90;

		/// <summary>Maximum karma reward for Pirate head</summary>
		public const int BOUNTY_PIRATE_KARMA_MAX = 110;

		/// <summary>Minimum gold reward for Pirate head</summary>
		public const int BOUNTY_PIRATE_GOLD_MIN = 120;

		/// <summary>Maximum gold reward for Pirate head</summary>
		public const int BOUNTY_PIRATE_GOLD_MAX = 160;

		/// <summary>Minimum karma reward for Assassin head</summary>
		public const int BOUNTY_ASSASSIN_KARMA_MIN = 60;

		/// <summary>Maximum karma reward for Assassin head</summary>
		public const int BOUNTY_ASSASSIN_KARMA_MAX = 80;

		/// <summary>Minimum gold reward for Assassin head</summary>
		public const int BOUNTY_ASSASSIN_GOLD_MIN = 100;

		/// <summary>Maximum gold reward for Assassin head</summary>
		public const int BOUNTY_ASSASSIN_GOLD_MAX = 140;

		#endregion

		#region Random Message Ranges

		/// <summary>Minimum index for reward message selection</summary>
		public const int REWARD_MESSAGE_MIN = 0;

		/// <summary>Maximum index for reward message selection</summary>
		public const int REWARD_MESSAGE_MAX = 3;

		/// <summary>Minimum index for dialog message selection</summary>
		public const int DIALOG_MESSAGE_MIN = 0;

		/// <summary>Maximum index for dialog message selection</summary>
		public const int DIALOG_MESSAGE_MAX = 4;

		/// <summary>Maximum index for combat message selection</summary>
		public const int COMBAT_MESSAGE_MAX = 8;

		#endregion

		#region Context Menu

		/// <summary>Context menu entry ID for speech gump</summary>
		public const int CONTEXT_MENU_ENTRY_ID = 6146;

		/// <summary>Context menu entry range</summary>
		public const int CONTEXT_MENU_RANGE = 3;

		#endregion
	}
}


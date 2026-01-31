namespace Server.Items
{
	/// <summary>
	/// Centralized constants for Pet Trainer calculations and mechanics.
	/// Extracted from PetTrainer.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class PetTrainerConstants
	{
		#region Item Constants

		/// <summary>Item ID for Pet Trainer</summary>
		public const int ITEM_ID = 0x166E;

		/// <summary>Target range for selecting pets</summary>
		public const int TARGET_RANGE = 1;

		/// <summary>Hue values for random selection</summary>
		public static readonly int[] HUE_VALUES = new int[] { 0, 1281, 1359 };

		#endregion

		#region Skill Constants

		/// <summary>Animal Taming skill threshold for high-skill training</summary>
		public const double ANIMAL_TAMING_THRESHOLD = 100.0;

		/// <summary>List of trainable skills</summary>
		public static readonly SkillName[] TRAINABLE_SKILLS = new SkillName[]
		{
			SkillName.Wrestling,
			SkillName.EvalInt,
			SkillName.Magery,
			SkillName.Meditation,
			SkillName.MagicResist,
			SkillName.Tactics,
			SkillName.Poisoning,
			SkillName.Anatomy
		};

		#endregion

		#region Training Constants

		/// <summary>Number of cases in training switch (19 total: 8 skills gain, 8 skills loss, 3 no effect)</summary>
		public const int SWITCH_CASE_COUNT = 19;

		/// <summary>Skill gain amount for low Animal Taming skill (< 100)</summary>
		public const double SKILL_GAIN_LOW = 0.5;

		/// <summary>Skill gain amount for high Animal Taming skill (>= 100)</summary>
		public const double SKILL_GAIN_HIGH = 1.0;

		/// <summary>Skill loss amount for low Animal Taming skill (< 100)</summary>
		public const double SKILL_LOSS_LOW = 1.0;

		/// <summary>Skill loss amount for high Animal Taming skill (>= 100)</summary>
		public const double SKILL_LOSS_HIGH = 0.5;

		/// <summary>Number of skills that can be trained (gain cases)</summary>
		public const int SKILL_GAIN_COUNT = 8;

		/// <summary>Number of skills that can lose points (loss cases)</summary>
		public const int SKILL_LOSS_COUNT = 8;

		/// <summary>Number of no-effect cases (sheepishly)</summary>
		public const int NO_EFFECT_COUNT = 3;

		#endregion

		#region Combat Chance Constants

		/// <summary>Combat chance denominator for low Animal Taming skill (< 100) - 1 in 10 chance</summary>
		public const int COMBAT_CHANCE_DENOMINATOR_LOW = 10;

		/// <summary>Combat chance denominator for high Animal Taming skill (>= 100) - 1 in 20 chance</summary>
		public const int COMBAT_CHANCE_DENOMINATOR_HIGH = 20;

		#endregion

		#region Break Chance Constants

		/// <summary>Chance that the tool breaks when used (10%)</summary>
		public const double BREAK_CHANCE = 0.1;

		#endregion
	}
}


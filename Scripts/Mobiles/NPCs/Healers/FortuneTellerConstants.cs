namespace Server.Mobiles
{
	/// <summary>
	/// Centralized constants for FortuneTeller NPC calculations and mechanics.
	/// Extracted from FortuneTeller.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class FortuneTellerConstants
	{
		#region NPC Title

		/// <summary>Title for FortuneTeller NPC</summary>
		public const string TITLE_FORTUNE_TELLER = "the fortune teller";

		#endregion

		#region Skill Ranges

		/// <summary>EvalInt skill minimum value</summary>
		public const double SKILL_EVAL_INT_MIN = 65.0;

		/// <summary>EvalInt skill maximum value</summary>
		public const double SKILL_EVAL_INT_MAX = 88.0;

		/// <summary>Forensics skill minimum value</summary>
		public const double SKILL_FORENSICS_MIN = 75.0;

		/// <summary>Forensics skill maximum value</summary>
		public const double SKILL_FORENSICS_MAX = 98.0;

		/// <summary>Magery skill minimum value</summary>
		public const double SKILL_MAGERY_MIN = 64.0;

		/// <summary>Magery skill maximum value</summary>
		public const double SKILL_MAGERY_MAX = 100.0;

		/// <summary>Meditation skill minimum value</summary>
		public const double SKILL_MEDITATION_MIN = 60.0;

		/// <summary>Meditation skill maximum value</summary>
		public const double SKILL_MEDITATION_MAX = 83.0;

		/// <summary>MagicResist skill minimum value</summary>
		public const double SKILL_MAGIC_RESIST_MIN = 65.0;

		/// <summary>MagicResist skill maximum value</summary>
		public const double SKILL_MAGIC_RESIST_MAX = 88.0;

		/// <summary>Wrestling skill minimum value</summary>
		public const double SKILL_WRESTLING_MIN = 60.0;

		/// <summary>Wrestling skill maximum value</summary>
		public const double SKILL_WRESTLING_MAX = 80.0;

		/// <summary>Necromancy skill minimum value</summary>
		public const double SKILL_NECROMANCY_MIN = 64.0;

		/// <summary>Necromancy skill maximum value</summary>
		public const double SKILL_NECROMANCY_MAX = 100.0;

		#endregion

		#region Outfit Selection

		/// <summary>Number of outfit headgear options</summary>
		public const int OUTFIT_SELECTION_COUNT = 3;

		#endregion

		#region Serialization

		/// <summary>Current serialization version</summary>
		public const int SERIALIZATION_VERSION = 0;

		#endregion
	}
}


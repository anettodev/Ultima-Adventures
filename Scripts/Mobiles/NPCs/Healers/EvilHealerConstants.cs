namespace Server.Mobiles
{
	/// <summary>
	/// Centralized constants for EvilHealer NPC calculations and mechanics.
	/// Extracted from EvilHealer.cs to improve maintainability and reduce code duplication.
	/// </summary>
	public static class EvilHealerConstants
	{
		#region NPC Title

		/// <summary>Title for EvilHealer NPC</summary>
		public const string TITLE_EVIL_HEALER = "the mortician";

		#endregion

		#region Karma

		/// <summary>Karma value for evil healer</summary>
		public const int KARMA_EVIL = -1;

		#endregion

		#region Robe Color

		/// <summary>Robe color for evil healer (dark red)</summary>
		public const int ROBE_COLOR_EVIL = 0x497;

		#endregion
	}
}


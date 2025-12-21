namespace Server.Mobiles
{
	/// <summary>
	/// Centralized string constants for ThiefGuildmaster NPC messages and labels.
	/// Extracted from ThiefGuildmaster.cs to improve maintainability and enable localization.
	/// </summary>
	public static class ThiefGuildmasterStringConstants
	{
		#region Job System Messages (Portuguese)

		/// <summary>Message format when player already has a job from a person</summary>
		public const string MSG_JOB_EXISTS_FORMAT = "Hmmm...você já tem um trabalho de {0}. Aqui está uma cópia se você perdeu.";

		/// <summary>Message when giving a new job to the player</summary>
		public const string MSG_JOB_NEW = "Aqui está algo que acho que você pode lidar.";

		#endregion

		#region Tithe System Messages (Portuguese)

		/// <summary>Message when player is not flagged and doesn't need help</summary>
		public const string MSG_NOT_FLAGGED = "Você permanece nas sombras, não precisa da nossa ajuda.";

		/// <summary>Message when tithe service succeeds</summary>
		public const string MSG_TITHE_SUCCESS = "O ladrão cuidará para apagar qualquer evidência de seus erros.";

		/// <summary>Message when player has already used tithe service today</summary>
		public const string MSG_TITHE_LIMIT_REACHED = "Parece que não podemos ajudá-lo mais hoje.";

		#endregion
	}
}


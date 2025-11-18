namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized string constants for CraftSystem-related messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from CraftSystem.cs to improve maintainability and enable easier localization.
	/// </summary>
	public static class CraftSystemStringConstants
	{
		#region User Messages (Portuguese)

		/// <summary>Message when player moves and stops crafting items</summary>
		public const string MSG_PLAYER_MOVED_STOPPED_CRAFTING = "Você se moveu e parou de criar o(s) item(s).";

		/// <summary>Message when player stops crafting items</summary>
		public const string MSG_STOPPED_CRAFTING = "Você parou de criar item(s).";

		#endregion
	}
}


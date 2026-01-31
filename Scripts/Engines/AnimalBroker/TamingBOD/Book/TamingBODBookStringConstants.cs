namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for Taming BOD Book messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from TamingBODBook.cs to improve maintainability and enable localization.
	/// </summary>
	public static class TamingBODBookStringConstants
	{
		#region Item Properties

		/// <summary>Item name: "Livro de Contratos de Domesticação"</summary>
		public const string ITEM_NAME = "Livro de Contratos";

		/// <summary>Contract type label in cyan color: "[Adestramento]"</summary>
		public const string CONTRACT_TYPE = "<BASEFONT COLOR=#00FFFF>[Adestramento]</BASEFONT>";

		#endregion

		#region User Messages (Portuguese)

		/// <summary>Message when dropped item is not a valid contract</summary>
		public const string MSG_INVALID_CONTRACT = "Isso não é um contrato válido.";

		#endregion
	}
}


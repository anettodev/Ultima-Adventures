namespace Server.Gumps
{
	/// <summary>
	/// Centralized string constants for Taming BOD Book Gump messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from TamingBODBookGump.cs to improve maintainability and enable localization.
	/// </summary>
	public static class TamingBODBookGumpStringConstants
	{
		#region Gump Labels

		/// <summary>Gump title: "LIVRO DE DOMESTICAÇÃO DE MONSTROS"</summary>
		public const string GUMP_TITLE = "LIVRO DE CONTRATOS";

		/// <summary>Column header: "Contrato"</summary>
		public const string LABEL_CONTRACT = "Contrato";

		/// <summary>Column header: "Domesticados"</summary>
		public const string LABEL_TAMED = "Domesticados";

		/// <summary>Column header: "Para Domesticar"</summary>
		public const string LABEL_TO_TAME = "Para Domesticar";

		/// <summary>Column header: "Recompensa"</summary>
		public const string LABEL_REWARD = "Recompensa";

		/// <summary>Contract name format for generic contracts: "{0} Criaturas"</summary>
		public const string CONTRACT_NAME_FORMAT_GENERIC = "{0} Criaturas";

		/// <summary>Contract name format for specific creature contracts: "{0} {1}"</summary>
		public const string CONTRACT_NAME_FORMAT_SPECIFIC = "{0} {1}";

		#endregion

		#region User Messages

		/// <summary>Message when cannot add another pet: "Você não pode adicionar outro animal a este contrato."</summary>
		public const string MSG_CANNOT_ADD_PET = "Você não pode adicionar outro animal a este contrato.";

		/// <summary>Message prompting to choose tamable: "Escolha o animal domável para adicionar."</summary>
		public const string MSG_CHOOSE_TAMABLE = "Escolha o animal domável para adicionar.";

		/// <summary>Message when creature was summoned: "Esta criatura foi invocada."</summary>
		public const string MSG_CREATURE_SUMMONED = "Esta criatura foi invocada.";

		/// <summary>Message when creature is too big: "Esta criatura é grande demais para caber neste pequeno contrato."</summary>
		public const string MSG_CREATURE_TOO_BIG = "Esta criatura é grande demais para caber neste pequeno contrato.";

		/// <summary>Message when targeting humans: "Isso não funciona em humanos."</summary>
		public const string MSG_WONT_WORK_ON_HUMANS = "Isso não funciona em humanos.";

		/// <summary>Message when pet won't work: "Este animal não funcionará."</summary>
		public const string MSG_PET_WONT_WORK = "Este animal não funcionará.";

		/// <summary>Message when target is not tamable: "Isso não é um animal domável."</summary>
		public const string MSG_NOT_TAMABLE_PET = "Isso não é um animal domável.";

		#endregion
	}
}


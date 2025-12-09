namespace Server.Gumps
{
	/// <summary>
	/// Centralized string constants for Taming BOD Gump messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from TamingBODGump.cs to improve maintainability and enable localization.
	/// </summary>
	public static class TamingBODGumpStringConstants
	{
		#region Gump Labels

		/// <summary>Contract label format for generic contracts: "Contrato Para: {0} criaturas"</summary>
		public const string LABEL_CONTRACT_FORMAT = "Contrato Para: {0} criaturas";

		/// <summary>Contract label format for specific creature types: "Contrato Para: {0} {1}"</summary>
		public const string LABEL_CONTRACT_FORMAT_SPECIFIC = "Contrato Para: {0} {1}";

		/// <summary>Quantity label format: "Quantidade Domesticada: {0}"</summary>
		public const string LABEL_QUANTITY_FORMAT = "Quantidade Domesticada: {0}";

		/// <summary>Reward label format: "Recompensa: {0}"</summary>
		public const string LABEL_REWARD_FORMAT = "Recompensa: {0}";

		/// <summary>Add creature button label: "Adicionar criatura"</summary>
		public const string BUTTON_ADD_CREATURE = "Adicionar";

		/// <summary>Reward button label: "Recompensa"</summary>
		public const string BUTTON_REWARD = "Recompensa";

		#endregion

		#region User Messages

		/// <summary>Message prompting to choose creature: "Escolha a criatura domada para adicionar."</summary>
		public const string MSG_CHOOSE_TAMED_CREATURE = "Escolha a criatura domada para adicionar.";

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

		/// <summary>Message format when wrong creature type: "Este contrato requer {0}."</summary>
		public const string MSG_WRONG_CREATURE_TYPE = "Este contrato requer {0}.";

		/// <summary>Message when wrong creature type (generic): "Este animal não corresponde ao tipo requerido pelo contrato."</summary>
		public const string MSG_WRONG_CREATURE_TYPE_GENERIC = "Este animal não corresponde ao tipo requerido pelo contrato.";

		#endregion
	}
}


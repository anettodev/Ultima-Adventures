namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for Taming BOD messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from TamingBOD.cs to improve maintainability and enable localization.
	/// </summary>
	public static class TamingBODStringConstants
	{
		#region Item Names

		/// <summary>Item name format for generic contracts: "Contrato: {0} Criaturas"</summary>
		public const string NAME_FORMAT_GENERIC = "Contrato: {0} Criaturas";

		/// <summary>Item name format for specific creature contracts: "Contrato: {0} {1}"</summary>
		public const string NAME_FORMAT_SPECIFIC = "Contrato: {0} {1}";

		#endregion

		#region Property Labels

		/// <summary>Property label format with yellow color: "<BASEFONT COLOR=#FFFF00>Este contrato vale atualmente {0} de ouro.</BASEFONT>"</summary>
		public const string PROPERTY_WORTH_FORMAT = "<BASEFONT COLOR=#FFFF00>Vale atualmente {0} moedas de ouro.</BASEFONT>";

		/// <summary>Property label: "Adicione mais criaturas para aumentar o pagamento."</summary>
		public const string PROPERTY_ADD_MORE = "Adicione mais criaturas para aumentar o pagamento.";

		/// <summary>Property label for generic contracts: "<BASEFONT COLOR=#00FFFF>[Genérico]</BASEFONT>"</summary>
		public const string PROPERTY_GENERIC = "<BASEFONT COLOR=#00FFFF>[Genérico]</BASEFONT>";

		/// <summary>Property label format for creature type: "<BASEFONT COLOR=#00FFFF>[{0}]</BASEFONT>"</summary>
		public const string PROPERTY_CREATURE_TYPE_FORMAT = "<BASEFONT COLOR=#00FFFF>[{0}]</BASEFONT>";

		/// <summary>Property label for completed contracts: "<BASEFONT COLOR=#00FF00>[Completo]</BASEFONT>"</summary>
		public const string PROPERTY_COMPLETE = "<BASEFONT COLOR=#00FF00>[Completo]</BASEFONT>";

		#endregion

		#region User Messages

		/// <summary>Message when special drop received: "Você recebeu um item especial!"</summary>
		public const string MSG_SPECIAL_DROP = "Você recebeu um item especial!";

		/// <summary>Message when reward placed: "Sua recompensa foi colocada na sua bolsa."</summary>
		public const string MSG_REWARD_PLACED = "Sua recompensa foi colocada na sua bolsa.";

		/// <summary>Message when deed has error: "Há algo errado com este contrato."</summary>
		public const string MSG_DEED_ERROR = "Há algo errado com este contrato.";

		#endregion
	}
}


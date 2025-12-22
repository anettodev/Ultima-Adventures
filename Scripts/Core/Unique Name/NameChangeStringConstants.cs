namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for name change system messages and labels.
	/// Extracted from CensusRecords.cs, ChangeName.cs, NameAlterGump.cs, and NameChangeGump.cs
	/// to improve maintainability and enable localization.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// </summary>
	public static class NameChangeStringConstants
	{
		#region Error Messages (Portuguese)

		/// <summary>Message when name is unacceptable or already taken</summary>
		public const string MSG_NAME_UNACCEPTABLE = "Esse nome é inaceitável ou já está em uso.";

		/// <summary>Message format when name is successfully changed</summary>
		public const string MSG_NAME_CHANGED_FORMAT = "Seu nome agora é {0}.";

		/// <summary>Message when player doesn't have enough gold for Census Records name change</summary>
		public const string MSG_INSUFFICIENT_GOLD = "Você não tem ouro suficiente!";

		/// <summary>Message when name entry is required</summary>
		public const string MSG_NAME_REQUIRED = "Você deve inserir um nome.";

		/// <summary>Message when name entry is optional (NameAlterGump)</summary>
		public const string MSG_NAME_OPTIONAL = "Você pode inserir um nome.";

		#endregion

		#region Item Names (Portuguese)

		/// <summary>Name for Name Change Contract item</summary>
		public const string ITEM_NAME_CHANGE_CONTRACT = "Contrato de Mudança de Nome";

		/// <summary>Name for Census Records item</summary>
		public const string ITEM_NAME_CENSUS_RECORDS = "Nimeria's Census Records";

		#endregion

		#region Gump Text - Census Records (Portuguese)

		/// <summary>Legal census records instruction text</summary>
		public const string GUMP_TEXT_CENSUS_LEGAL = "Estes são os registros do censo des muitas terras e reinos, e os sábios compilaram uma lista de nomes de seus cidadãos. Seu nome também está nesta lista. Se você quiser mudar seu nome, pode fazê-lo neste livro.";

		/// <summary>Forged census records instruction text</summary>
		public const string GUMP_TEXT_CENSUS_FORGED = "Estes são os registros falsificados do censo das muitas terras, e a guilda dos ladrões compilou uma lista de nomes de seus cidadãos. Seu nome também está nesta lista. Se você quiser mudar seu nome, pode fazê-lo neste livro.";

		/// <summary>Additional instruction text for Census Records (about gold cost and character limit)</summary>
		public const string GUMP_TEXT_CENSUS_ADDITIONAL = " Então, se você tem uma ideia para um novo nome apropriado para fantasia, e está disposto a gastar 2.000 moedas de ouro, então delete o texto abaixo e digite novamente. Um novo nome não pode ter mais de 16 caracteres.";

		#endregion

		#region Gump Text - Name Alter (Portuguese)

		/// <summary>NameAlterGump instruction text</summary>
		public const string GUMP_TEXT_NAME_ALTER = "Uma das principais regras deste jogo é ter um nome único e apropriado para seu personagem. Se você acha que seu nome é apropriado, então digite seu nome novamente aqui. Caso contrário, delete o texto abaixo e insira um novo nome para si mesmo que não tenha mais de 16 caracteres.";

		#endregion

		#region Gump Text - Name Change (Portuguese)

		/// <summary>NameChangeGump instruction text</summary>
		public const string GUMP_TEXT_NAME_CHANGE = "O nome que você escolheu está atualmente em uso e não está mais disponível. Você deve escolher um nome diferente antes de poder continuar. Então delete o texto abaixo e insira um novo nome apropriado para fantasia.";

		/// <summary>Label text for new name input</summary>
		public const string LABEL_NEW_NAME = "Novo Nome:";

		#endregion

		#region Placeholder Text (Portuguese)

		/// <summary>Placeholder text for name input field</summary>
		public const string PLACEHOLDER_TYPE_HERE = "Digite aqui...";

		#endregion

		#region HTML Formatting

		/// <summary>HTML body and font formatting for gump text</summary>
		public const string HTML_FORMAT = "<BODY><BASEFONT Color=#FBFBFB><BIG>{0}</BIG></BASEFONT></BODY>";

		#endregion
	}
}


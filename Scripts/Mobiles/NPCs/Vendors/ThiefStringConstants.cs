namespace Server.Mobiles
{
	/// <summary>
	/// Centralized string constants for Thief NPC messages and labels.
	/// Extracted from Thief.cs to improve maintainability and enable localization.
	/// </summary>
	public static class ThiefStringConstants
	{
		#region Property Labels (Portuguese)

		/// <summary>Instruction text shown in property list when in training region</summary>
		public const string PROPERTY_TRAIN_INSTRUCTION = "Clique duas vezes em mim para treinar roubo.";

		#endregion

		#region Training Messages (Portuguese)

		/// <summary>Message format when training attempt succeeds</summary>
		public const string MSG_TRAINING_SUCCESS_FORMAT = "Você poderia ter pegado um item de {0}";

		/// <summary>Message when trainer catches the player during training</summary>
		public const string MSG_TRAINING_CAUGHT = "Não foi bem assim, eu te peguei.";

		/// <summary>Message when training attempt fails</summary>
		public const string MSG_TRAINING_FAIL = "Você falhou na tentativa.";

		#endregion

		#region Unlock Service Messages (Portuguese)

		/// <summary>Message format when player is begging and requesting unlock service</summary>
		public const string MSG_UNLOCK_BEGGING_FORMAT = "Já que você está implorando, ainda quer que eu destranque uma caixa? Custará apenas {0}.";

		/// <summary>Message format when player requests unlock service normally</summary>
		public const string MSG_UNLOCK_NORMAL_FORMAT = "Se quiser que eu destranque uma caixa, custará {0} moedas de ouro.";

		/// <summary>Message when trying to unlock a cursed item (BookBox)</summary>
		public const string MSG_CANNOT_UNLOCK_CURSED = "Não posso ajudar com um item tão amaldiçoado.";

		/// <summary>Message when unlock service succeeds</summary>
		public const string MSG_UNLOCK_SUCCESS = "Agora está destrancada.";

		/// <summary>Message format when payment is made</summary>
		public const string MSG_PAYMENT_FORMAT = "Você paga {0} moedas de ouro.";

		/// <summary>Message format showing unlock cost</summary>
		public const string MSG_UNLOCK_COST_FORMAT = "Custaria {0} moedas de ouro para destrancar isso.";

		/// <summary>Message when player doesn't have enough gold</summary>
		public const string MSG_INSUFFICIENT_GOLD = "Você não tem ouro suficiente.";

		/// <summary>Message when target doesn't need unlock service</summary>
		public const string MSG_NO_SERVICE_NEEDED = "Isso não precisa dos meus serviços.";

		#endregion

		#region Speech System (Portuguese)

		/// <summary>Title for speech gump (PT-BR translation)</summary>
		public const string SPEECH_GUMP_TITLE = "A Arte do Roubo";

		#endregion
	}
}


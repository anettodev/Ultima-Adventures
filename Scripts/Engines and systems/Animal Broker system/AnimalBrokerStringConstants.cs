namespace Server.Mobiles
{
	/// <summary>
	/// Centralized string constants for Animal Broker (AnimalTrainerLord) messages and labels.
	/// Extracted from AnimalBroker.cs to improve maintainability and enable localization.
	/// </summary>
	public static class AnimalBrokerStringConstants
	{
		#region Vendor Title

		/// <summary>Title for AnimalTrainerLord vendor</summary>
		public const string VENDOR_TITLE = "o corretor de animais";

		#endregion

		#region Greeting Messages (Portuguese)

		/// <summary>Greeting message 1: Pet collector announcement</summary>
		public const string GREETING_PET_COLLECTOR = "Colecionador de animais aqui, apenas me diga que quer vender!";

		/// <summary>Greeting message 2: Buying rare animals</summary>
		public const string GREETING_BUYING_RARE = "Comprando animais raros de todos os tipos, simplesmente me diga que quer vender um";

		/// <summary>Greeting message 3: Selling tamed pets</summary>
		public const string GREETING_SELL_PETS = "Venda-me seus animais domados, pagarei bem!";

		/// <summary>Greeting message 4: Price estimate offer</summary>
		public const string GREETING_APPRAISE_OFFER = "Se quiser uma estimativa de preço de um animal, apenas me diga para avaliar um";

		/// <summary>Greeting message 5: Appraisal service</summary>
		public const string GREETING_APPRAISE_SERVICE = "Posso avaliar um animal para você, apenas me peça.";

		/// <summary>Greeting message 6: Contract help needed</summary>
		public const string GREETING_CONTRACT_HELP = "Preciso de ajuda com uns contratos... ";

		#endregion

		#region Pet Sale Messages (Portuguese)

		/// <summary>Message when asking which beast to appraise</summary>
		public const string MSG_APPRAISE_PROMPT = "Qual animal você gostaria de avaliar?";

		/// <summary>Message when asking which beast to sell</summary>
		public const string MSG_SELL_PROMPT = "Qual animal você está vendendo?";

		/// <summary>Message format for appraising a pet (price)</summary>
		public const string MSG_APPRAISE_FORMAT = "Posso pagar {0} por este animal.";

		#endregion

		#region Price-Based Messages (Portuguese)

		/// <summary>Message format for low-value pets (400 or less)</summary>
		public const string MSG_PRICE_LOW_FORMAT = "Tenho muitos {0} então vou te dar {1} de ouro";

		/// <summary>Message format for medium-value pets (401-1000)</summary>
		public const string MSG_PRICE_MEDIUM_FORMAT = "Obrigado {0}, vou adicionar este {1} à minha coleção! Aqui está {2} pelos seus serviços";

		/// <summary>Message format for high-value pets (1001-5000)</summary>
		public const string MSG_PRICE_HIGH_FORMAT = "Um achado raro!!! Obrigado por {0} vale {1} para o comprador certo..";

		/// <summary>Message format for very high-value pets (5001-10001)</summary>
		public const string MSG_PRICE_VERY_HIGH_FORMAT = "Que espécime incrível! Vou te pagar {0} por ele! ";

		/// <summary>Message format for ultra high-value pets (40001+)</summary>
		public const string MSG_PRICE_ULTRA_HIGH_FORMAT = "Vou pagar {0}! Sempre quis um destes!!! ";

		#endregion

		#region Contract Messages (Portuguese)

		/// <summary>Message when contract is not available yet</summary>
		public const string MSG_CONTRACT_NOT_AVAILABLE = "Desculpe, ainda não tenho um contrato disponível para você.";

		#endregion

		#region Pet Sale Validation Messages (Portuguese)

		/// <summary>Message when user declines to sell the pet</summary>
		public const string MSG_SALE_DECLINED = "Que pena! Se mudar de ideia, estarei aqui.";

		/// <summary>Message when pet ownership validation fails</summary>
		public const string MSG_APPRAISE_NOT_OWNER = "Você não é o dono desse animal de estimação!";

		#endregion
	}
}


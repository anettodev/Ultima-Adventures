namespace Server.Mobiles
{
	/// <summary>
	/// Centralized string constants for Midland vendor messages and labels.
	/// All strings in Portuguese-Brazilian (PT-BR).
	/// Extracted from MidlandVendor.cs to improve maintainability and enable localization.
	/// </summary>
	public static class MidlandVendorStringConstants
	{
		#region Combat and Guard Messages (Portuguese)
		
		/// <summary>Guard call message 1</summary>
		public const string MSG_GUARDS_1 = "Guarda! Guarda!";
		
		/// <summary>Guard call message 2</summary>
		public const string MSG_GUARDS_2 = "Vendedor Comprar Banco Guarda!";
		
		/// <summary>Guard call message 3</summary>
		public const string MSG_GUARDS_3 = "Onde estão vocês Guardas???";
		
		/// <summary>Guard call message 4</summary>
		public const string MSG_GUARDS_4 = "Para mim!!! Guarda!";
		
		#endregion
		
		#region Banker Messages (Portuguese)
		
		/// <summary>Banker description message</summary>
		public const string MSG_BANKER_DESCRIPTION = "Sou um banqueiro... Fornecemos contas e mantemos seu dinheiro seguro.";
		
		/// <summary>Banker fee message</summary>
		public const string MSG_BANKER_FEE = "Bem, o banco cobra uma pequena taxa, mas é mínima.";
		
		/// <summary>Banker cannot buy message</summary>
		public const string MSG_BANKER_CANNOT_BUY = "Você não pode comprar um banqueiro!";
		
		/// <summary>Banker not interested in selling</summary>
		public const string MSG_BANKER_NOT_INTERESTED = "A menos que você esteja vendendo investimentos sem risco, não estamos interessados.";
		
		/// <summary>We don't serve your kind message</summary>
		public const string MSG_DONT_SERVE_YOUR_KIND = "Não servimos sua espécie aqui.";
		
		/// <summary>Deposit instructions message</summary>
		public const string MSG_DEPOSIT_INSTRUCTIONS = "Para depositar, simplesmente me diga que deseja depositar e o valor ou me dê o ouro. Você pode confiar em mim.";
		
		/// <summary>Deposit success message format: "Você depositou {0} em sua conta."</summary>
		public const string MSG_DEPOSIT_SUCCESS_FORMAT = "Você depositou {0} em sua conta.";
		
		/// <summary>Deposit insufficient funds message</summary>
		public const string MSG_DEPOSIT_INSUFFICIENT = "Você não parece ter o suficiente para depositar esse valor.";
		
		/// <summary>Withdraw success message format: "Você sacou {0} de sua conta."</summary>
		public const string MSG_WITHDRAW_SUCCESS_FORMAT = "Você sacou {0} de sua conta.";
		
		/// <summary>Balance message format: "Seu saldo atual conosco é {0}"</summary>
		public const string MSG_BALANCE_FORMAT = "Seu saldo atual conosco é {0}";
		
		/// <summary>No account message</summary>
		public const string MSG_NO_ACCOUNT = "Você não parece ter uma conta conosco.";
		
		#endregion
		
		#region Inventory and Price Messages (Portuguese)
		
		/// <summary>Inventory list header</summary>
		public const string MSG_INVENTORY_HEADER = "Claro, vamos ver o que tenho à venda...";
		
		/// <summary>Inventory item format: "{0} {1} a {2} {3}."</summary>
		public const string MSG_INVENTORY_ITEM_FORMAT = "{0} {1} a {2} {3}.";
		
		/// <summary>Service fee message</summary>
		public const string MSG_SERVICE_FEE = "Cobro uma pequena taxa de 1% pelos meus serviços.";
		
		/// <summary>Empty stock message</summary>
		public const string MSG_EMPTY_STOCK = "Desculpe, Senhor, meus estoques estão vazios... talvez você gostaria de vender para mim?";
		
		/// <summary>Deal in items message format: "Eu lido com {0} {1} {2} {3}"</summary>
		public const string MSG_DEAL_IN_ITEMS_FORMAT = "Eu lido com {0} {1} {2} {3}";
		
		/// <summary>Ask for prices message</summary>
		public const string MSG_ASK_FOR_PRICES = "Você pode me perguntar sobre meus preços";
		
		/// <summary>Price list header</summary>
		public const string MSG_PRICE_HEADER = "Meus preços? Claro...";
		
		/// <summary>Buy price format: "Eu compro {0} a {1} {2}."</summary>
		public const string MSG_BUY_PRICE_FORMAT = "Eu compro {0} a {1} {2}.";
		
		#endregion
		
		#region Buy/Sell Transaction Messages (Portuguese)
		
		/// <summary>Insufficient inventory message format: "Não tenho tantos {0} em mãos no momento, Senhor."</summary>
		public const string MSG_INSUFFICIENT_INVENTORY_FORMAT = "Não tenho tantos {0} em mãos no momento, Senhor.";
		
		/// <summary>Buy amount request format: "Quantos {0} você gostaria de comprar?"</summary>
		public const string MSG_BUY_AMOUNT_REQUEST_FORMAT = "Quantos {0} você gostaria de comprar?";
		
		/// <summary>Sell amount request format: "Quantos {0} você gostaria de vender, senhor?"</summary>
		public const string MSG_SELL_AMOUNT_REQUEST_FORMAT = "Quantos {0} você gostaria de vender, senhor?";
		
		/// <summary>One customer at a time format: "Desculpe {0}, só posso lidar com um cliente por vez."</summary>
		public const string MSG_ONE_CUSTOMER_FORMAT = "Desculpe {0}, só posso lidar com um cliente por vez.";
		
		/// <summary>Not enough stock format: "Desculpe, Senhor, não tenho {0} suficientes para vender tantos."</summary>
		public const string MSG_NOT_ENOUGH_STOCK_FORMAT = "Desculpe, Senhor, não tenho {0} suficientes para vender tantos.";
		
		/// <summary>Sale problem error message</summary>
		public const string MSG_SALE_PROBLEM = "houve um problema com a venda, avise um administrador.";
		
		/// <summary>Insufficient funds message</summary>
		public const string MSG_INSUFFICIENT_FUNDS = "Você tem esse valor em sua mochila ou em sua conta no banco local?";
		
		/// <summary>Thank you for business message</summary>
		public const string MSG_THANK_YOU = "Obrigado pelo seu negócio, Senhor.";
		
		/// <summary>Zero total message</summary>
		public const string MSG_ZERO_TOTAL = "O total foi 0 ouro, então nenhum dinheiro foi dado.";
		
		/// <summary>Pleasure doing business message</summary>
		public const string MSG_PLEASURE_BUSINESS = "Prazer em fazer negócios, Senhor!";
		
		/// <summary>Don't trade in that message</summary>
		public const string MSG_DONT_TRADE = "Perdão, Senhor, não negocio isso.";
		
		/// <summary>Don't think so message</summary>
		public const string MSG_DONT_THINK_SO = "Não acho que sim.";
		
		/// <summary>Purchase confirmation format: "Obrigado, Senhor, comprei {0} {1} por um total de {2} {3}."</summary>
		public const string MSG_PURCHASE_CONFIRMATION_FORMAT = "Obrigado, Senhor, comprei {0} {1} por um total de {2} {3}.";
		
		/// <summary>Bank deposit notification</summary>
		public const string MSG_BANK_DEPOSIT_NOTIFICATION = "Isso foi mais de 5.000, então enviei os fundos para sua conta bancária.";
		
		/// <summary>Player doesn't have items format: "Senhor, você não parece ter {0} {1} com você."</summary>
		public const string MSG_PLAYER_DOESNT_HAVE_FORMAT = "Senhor, você não parece ter {0} {1} com você.";
		
		#endregion
		
		#region Greeting Messages (Portuguese)
		
		/// <summary>Greeting message 1</summary>
		public const string MSG_GREETING_1 = "Apenas os melhores produtos aqui!";
		
		/// <summary>Greeting message 2</summary>
		public const string MSG_GREETING_2 = "Aventureiro, você tem algum produto para vender? Pergunte-me o que eu compro.";
		
		/// <summary>Greeting message 3</summary>
		public const string MSG_GREETING_3 = "Você pode comprar de mim, apenas me pergunte o que tenho.";
		
		/// <summary>Greeting message 4</summary>
		public const string MSG_GREETING_4 = "Você parece estar com vontade de gastar, senhor! Pergunte-me o que tenho à venda!";
		
		/// <summary>Greeting message 5</summary>
		public const string MSG_GREETING_5 = "Compro de aventureiros e vendo para outros aventureiros - que vida!";
		
		/// <summary>Greeting message 6</summary>
		public const string MSG_GREETING_6 = "Saudações, Senhor - Como posso ajudar?";
		
		#endregion
		
		#region Death Messages (Portuguese)
		
		/// <summary>Death message - Help</summary>
		public const string MSG_DEATH_HELP = "Socorro!";
		
		/// <summary>Death message - Guards</summary>
		public const string MSG_DEATH_GUARDS = "Guarda!";
		
		/// <summary>Death message - No hiding place</summary>
		public const string MSG_DEATH_NO_HIDING = "Não haverá lugar para você se esconder!";
		
		/// <summary>Death message - No</summary>
		public const string MSG_DEATH_NO = "Nãoooo!";
		
		/// <summary>Death message - Vile rogue</summary>
		public const string MSG_DEATH_VILE_ROGUE = "Vil patife!";
		
		/// <summary>Death message - Aarrgh</summary>
		public const string MSG_DEATH_AARRGH = "Aarrgh!";
		
		#endregion
		
		#region OneTime Messages (Portuguese)
		
		/// <summary>Goes back to business message</summary>
		public const string MSG_BACK_TO_BUSINESS = "*volta para seus negócios*";
		
		#endregion
	}
}


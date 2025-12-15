namespace Server.Gumps
{
	/// <summary>
	/// Centralized string constants for Young/Iniciante Gump messages and labels.
	/// Extracted from YoungGumps.cs to improve maintainability and enable localization.
	/// All strings are in Portuguese-Brazilian (PT-BR).
	/// </summary>
	public static class YoungGumpStringConstants
	{
		#region YoungDungeonWarning Messages

		/// <summary>Warning message about monsters attacking in dungeons</summary>
		public const string MSG_DUNGEON_WARNING = "Aviso: Monstros podem te atacar aqui embaixo nas masmorras!";

		/// <summary>OK button label</summary>
		public const string BUTTON_OKAY = "OK";

		#endregion

		#region YoungDeathNotice Messages

		/// <summary>Title: You have died (with red color)</summary>
		public const string MSG_DEATH_TITLE = "<BASEFONT COLOR=\"#FF0000\">Você morreu.</BASEFONT>";

		/// <summary>Message 1: As a ghost you cannot interact with the world (with white color)</summary>
		public const string MSG_DEATH_1 = "<BASEFONT COLOR=\"#FFFFFF\">Como um fantasma você não pode interagir com o mundo. Você não pode tocar itens nem usá-los.</BASEFONT>";

		/// <summary>Message 2: You can pass through doors (with white color)</summary>
		public const string MSG_DEATH_2 = "<BASEFONT COLOR=\"#FFFFFF\">Você pode passar por portas como se elas não existissem. No entanto, você não pode passar por paredes.</BASEFONT>";

		/// <summary>Message 3: Items will be in your backpack (with white color)</summary>
		public const string MSG_DEATH_3 = "<BASEFONT COLOR=\"#FFFFFF\">Como você é um jogador iniciante, quaisquer itens que você tinha em sua pessoa no momento da morte estarão em sua mochila após a ressurreição.</BASEFONT>";

		/// <summary>Message 4: To be resurrected find a healer (with white color)</summary>
		public const string MSG_DEATH_4 = "<BASEFONT COLOR=\"#FFFFFF\">Para ser ressuscitado você deve encontrar um curandeiro na cidade ou vagando no deserto. Alguns jogadores poderosos também podem ser capazes de ressuscitá-lo.</BASEFONT>";

		/// <summary>Message 5: While in young status you will be transported (with white color)</summary>
		public const string MSG_DEATH_5 = "<BASEFONT COLOR=\"#FFFFFF\">Enquanto você ainda estiver com status de Iniciante, será transportado para o curandeiro mais próximo.</BASEFONT>";

		/// <summary>Message 6: To rejoin the world of the living (with white color)</summary>
		public const string MSG_DEATH_6 = "<BASEFONT COLOR=\"#FFFFFF\">Para retornar ao mundo dos vivos, simplesmente caminhe perto de um dos curandeiros NPC, e eles o ressuscitarão desde que você não esteja marcado como criminoso.</BASEFONT>";

		#endregion

		#region RenounceYoungGump Messages

		/// <summary>Title: Renouncing 'Young Player' Status</summary>
		public const string MSG_RENOUNCE_TITLE = "<CENTER>Renunciando ao Status de 'Jogador Iniciante'</CENTER>";

		/// <summary>Body text explaining renouncement consequences</summary>
		public const string MSG_RENOUNCE_BODY = 
			"Como um jogador 'Iniciante', você está atualmente sob um sistema de proteção que impede " +
			"que você seja atacado por outros jogadores e certos monstros.<BR><BR>" +
			"Se você escolher renunciar ao seu status de jogador 'Iniciante', você perderá esta proteção. " +
			"Você se tornará vulnerável a outros jogadores, e muitos monstros que apenas te encaravam " +
			"ameaçadoramente antes agora vão te atacar à vista!<BR><BR>" +
			"Selecione OK agora se você deseja renunciar ao seu status de jogador 'Iniciante', caso contrário " +
			"pressione CANCELAR.";

		/// <summary>Message when player renounces status</summary>
		public const string MSG_RENOUNCED_STATUS = "Você escolheu renunciar ao seu status de jogador 'Iniciante'.";

		/// <summary>Message when player cancels renouncement</summary>
		public const string MSG_NOT_RENOUNCED = "Você escolheu não renunciar ao seu status de jogador 'Iniciante'.";

		/// <summary>Cancel button label</summary>
		public const string BUTTON_CANCEL = "CANCELAR";

		#endregion

		#region YoungStatusLostGump Messages

		/// <summary>Title: You are no longer an Iniciante (with yellow color)</summary>
		public const string TITLE_STATUS_LOST = "<CENTER><BIG><BIG><BASEFONT COLOR=\"#FFFF00\">Você não é mais um Iniciante!</BASEFONT></BIG></BIG></CENTER>";

		/// <summary>Greeting message</summary>
		public const string BODY_GREETING = "<CENTER><BIG>Parabéns, Aventureiro!</BIG></CENTER><BR><BR>";

		/// <summary>Introduction message</summary>
		public const string BODY_INTRO = "O status de <BASEFONT COLOR=\"#00FFFF\">Iniciante</BASEFONT> não se aplica mais a você.<BR><BR>";

		/// <summary>Question header</summary>
		public const string BODY_QUESTION = "<BASEFONT COLOR=\"#FFFF00\">O que isso significa?</BASEFONT><BR>";

		/// <summary>Explanation message</summary>
		public const string BODY_EXPLANATION = "A partir de agora, você enfrentará os desafios reais!<BR><BR>";

		/// <summary>Bullet point 1: Monsters will attack</summary>
		public const string BODY_BULLET_1 = "• Monstros que antes apenas te encaravam agora vão te atacar!<BR>";

		/// <summary>Bullet point 2: Players can attack</summary>
		public const string BODY_BULLET_2 = "• Outros jogadores podem te atacar em certas situações.<BR>";

		/// <summary>Bullet point 3: Death consequences</summary>
		public const string BODY_BULLET_3 = "• As consequências da morte serão mais severas.<BR>";

		/// <summary>Bullet point 4: More opportunities</summary>
		public const string BODY_BULLET_4 = "• Mas você também terá acesso a mais oportunidades!<BR><BR>";

		/// <summary>Closing message</summary>
		public const string BODY_CLOSING = "<I>A verdadeira aventura começa agora! Boa sorte!</I>";

		/// <summary>OK button label</summary>
		public const string BUTTON_OK_LABEL = "<CENTER>OK</CENTER>";

		#endregion

		#region YoungLoginInfoGump Messages

		/// <summary>Title: Status de Iniciante (with yellow color)</summary>
		public const string MSG_LOGIN_INFO_TITLE = "<CENTER><BIG><BASEFONT COLOR=\"#00FFFF\">Status de Iniciante</BASEFONT></BIG></CENTER>";

		/// <summary>Complete body text explaining Young status</summary>
		public const string MSG_LOGIN_INFO_BODY = 
			"<BASEFONT COLOR=\"#FFFFFF\">" +
			"<CENTER><BIG>Bem-vindo, Aventureiro!</BIG></CENTER><BR><BR>" +
			"Você possui o Status de <BASEFONT COLOR=\"#00FFFF\">Iniciante</BASEFONT>, que oferece proteção especial durante seus primeiros passos no mundo.<BR>" +
			"<BR><BASEFONT COLOR=\"#FFFF00\">Benefícios de Iniciante:</BASEFONT><BR>" +
			"• Proteção contra a maioria dos monstros em áreas protegidas<BR>" +
			"• Ao morrer, você será transportado para um curandeiro<BR>" +
			"• Seus itens serão preservados em sua mochila após a morte<BR>" +
			"• Resurreição gratuita pelos curandeiros<BR><BR>" +
			"<BASEFONT COLOR=\"#FFFF00\">Como o Status de Iniciante pode ser removido:</BASEFONT><BR>" +
			"• Expiração por tempo: Após 8 dias desde a criação do personagem<BR>" +
			"• Alcançando habilidades: Quando seus pontos de habilidade totais chegarem a 250.0 OU qualquer habilidade atingir 100.0 pontos<BR>" +
			"• Renúncia manual: Você pode renunciar voluntariamente dizendo <BASEFONT COLOR=\"#00FFFF\"> \"Eu renuncio ser iniciante\"</BASEFONT><BR><BR>" +
			"<I>Quando você perder o status de Iniciante, enfrentará os desafios reais do mundo!</I><BR><BR>" +
			"</BASEFONT>";

		#endregion
	}
}


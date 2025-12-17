namespace Server.Mobiles
{
	/// <summary>
	/// Centralized dialogue strings for PlayDirectorNewChar character creation sequence.
	/// All strings are in PT-BR (Portuguese - Brazil) for consistency with the server.
	/// </summary>
	public static class PlayDirectorStringConstants
	{
		#region Pledge Dialogue

		/// <summary>Initial pledge question</summary>
		public const string MSG_PLEDGE_QUESTION = "Você se compromete a manter este mundo limpo e respeitar outros aventureiros?";

		/// <summary>Positive response to pledge</summary>
		public const string MSG_PLEDGE_ACCEPTED = "Muito bem! Bem-vindo, amigo.";

		/// <summary>Negative response to pledge</summary>
		public const string MSG_PLEDGE_DECLINED = "Ok, sua escolha. Até logo!";

		#endregion

		#region Avatar Dialogue

		/// <summary>Avatar explanation - Balance</summary>
		public const string MSG_AVATAR_BALANCE = "Um Equilíbrio Poderoso afeta todas as coisas neste mundo. As ações dos avatares afetam se ele se move em direção ao mal ou ao bem.";

		/// <summary>Avatar explanation - Effects</summary>
		public const string MSG_AVATAR_EFFECTS = "Avatares podem escolher se comprometer com qualquer lado do Equilíbrio e afetar um grande número de coisas, como recompensas de ouro, dificuldade de monstros e preços de lojas.";

		/// <summary>Avatar explanation - Benefits and costs</summary>
		public const string MSG_AVATAR_BENEFITS_COSTS = "Isso vem com benefícios como mais atributos e ganho de habilidade mais rápido, mas também a um custo: a morte carregará uma penalidade muito real.";

		/// <summary>Avatar question</summary>
		public const string MSG_AVATAR_QUESTION = "Você deseja se tornar um avatar do equilíbrio? Esteja avisado que isso pode ser uma experiência mais difícil.";

		/// <summary>Positive response to avatar</summary>
		public const string MSG_AVATAR_ACCEPTED = "Boa escolha.";

		/// <summary>Negative response to avatar</summary>
		public const string MSG_AVATAR_DECLINED = "Tudo bem... você prefere uma experiência mais fácil.";

		#endregion

		#region SoulBound Dialogue

		/// <summary>SoulBound explanation - Introduction</summary>
		public const string MSG_SOULBOUND_INTRO = "Em seguida, você pode escolher vincular sua alma ao próprio tecido deste mundo.";

		/// <summary>SoulBound explanation - Death</summary>
		public const string MSG_SOULBOUND_DEATH = "Fazer isso significa que a morte será permanente e você retornará como uma nova pessoa toda vez que morrer.";

		/// <summary>SoulBound explanation - Rewards</summary>
		public const string MSG_SOULBOUND_REWARDS = "Aventurar-se dessa forma pode ser muito difícil, mas muito recompensador - diz-se que SoulBound pode vincular propriedades de itens em seus próprios seres.";

		/// <summary>SoulBound question</summary>
		public const string MSG_SOULBOUND_QUESTION = "Você deseja ser um SoulBound? *Isso não é recomendado para novos aventureiros*";

		/// <summary>Positive response to SoulBound</summary>
		public const string MSG_SOULBOUND_ACCEPTED = "Boa sorte!";

		/// <summary>Negative response to SoulBound</summary>
		public const string MSG_SOULBOUND_DECLINED = "Justo!";

		#endregion

		#region Completion Dialogue

		/// <summary>Completion farewell</summary>
		public const string MSG_COMPLETION_FAREWELL = "Vejo você por aí!";

		/// <summary>Completion teleport spell</summary>
		public const string MSG_COMPLETION_TELEPORT = "An Vam Trav";

		#endregion

		#region Play Sequence Dialogue

		/// <summary>Actor1 - Sleep spell exploit failed</summary>
		public const string MSG_PLAY_ACTOR1_EXPLOIT_FAILED = "Droga, minha nova exploração de feitiço de sono falhou novamente.";

		/// <summary>Actor1 - Need ingredient</summary>
		public const string MSG_PLAY_ACTOR1_NEED_INGREDIENT = "Vou precisar que você me traga mais daquele ingrediente especial.";

		/// <summary>Actor2 - But M'Lord</summary>
		public const string MSG_PLAY_ACTOR2_BUT_MYLORD = "Mas, Meu Senhor";

		/// <summary>Actor1 - No buts</summary>
		public const string MSG_PLAY_ACTOR1_NO_BUTS = "Sem mas, seu preguiçoso.";

		/// <summary>Actor1 - Threat</summary>
		public const string MSG_PLAY_ACTOR1_THREAT = "Vá, ou vou colocar meus animais paragons em você novamente.";

		/// <summary>Actor2 - Yes M'Lord</summary>
		public const string MSG_PLAY_ACTOR2_YES_MYLORD = "Sim, Meu Senhor";

		/// <summary>Actor1 - Grumbles</summary>
		public const string MSG_PLAY_ACTOR1_GRUMBLES = "*Resmunga*";

		/// <summary>Actor1 - Defences breached</summary>
		public const string MSG_PLAY_ACTOR1_DEFENCES_BREACHED = "Malditos Cracas! As defesas foram violadas!";

		/// <summary>Actor3 - Stop exploits</summary>
		public const string MSG_PLAY_ACTOR3_STOP_EXPLOITS = "Vou impedir que você use mais explorações!";

		/// <summary>Actor3 - Vas Flam spell</summary>
		public const string MSG_PLAY_ACTOR3_VAS_FLAM = "Vas Flam";

		/// <summary>Actor1 - We shall meet again</summary>
		public const string MSG_PLAY_ACTOR1_MEET_AGAIN = "Nos encontraremos novamente, Final!";

		/// <summary>Actor3 - New adventurer</summary>
		public const string MSG_PLAY_ACTOR3_NEW_ADVENTURER = "Bem, bem... o que temos aqui... um novo Aventureiro!";

		/// <summary>Actor3 - Stuck in jail</summary>
		public const string MSG_PLAY_ACTOR3_STUCK_JAIL = "Preso na prisão, estamos?";

		/// <summary>Actor3 - Let you out</summary>
		public const string MSG_PLAY_ACTOR3_LET_OUT = "Bem... acho que posso deixá-lo sair.";

		/// <summary>Actor3 - Need to know</summary>
		public const string MSG_PLAY_ACTOR3_NEED_TO_KNOW = "Mas preciso saber algumas coisas primeiro.";

		/// <summary>Actor3 - World created by Djeryv</summary>
		public const string MSG_PLAY_ACTOR3_WORLD_CREATED = "Este mundo foi criado por um ser chamado Djeryv há muito tempo.";

		/// <summary>Actor3 - World changed</summary>
		public const string MSG_PLAY_ACTOR3_WORLD_CHANGED = "Mas mudou, se transformou... para melhor ou pior.";

		/// <summary>Actor3 - Forces battle</summary>
		public const string MSG_PLAY_ACTOR3_FORCES_BATTLE = "Forças do Bem e do Mal batalham pela dominância, e o mundo está atormentado por seres horríveis de poder.";

		/// <summary>Actor3 - If I let you out</summary>
		public const string MSG_PLAY_ACTOR3_IF_LET_OUT = "Se eu deixá-lo sair, preciso saber algumas coisas...";

		/// <summary>Actor2 - Burps</summary>
		public const string MSG_PLAY_ACTOR2_BURPS = "*arrotos*";

		/// <summary>Actor1 - Yes Yes</summary>
		public const string MSG_PLAY_ACTOR1_YES_YES = "Sim... Sim....";

		/// <summary>Actor2 - Prisoner woke up</summary>
		public const string MSG_PLAY_ACTOR2_PRISONER_WOKE = "Meu Senhor, o Prisioneiro acordou.";

		/// <summary>Actor1 - What's this</summary>
		public const string MSG_PLAY_ACTOR1_WHATS_THIS = "O que é isso?";

		/// <summary>Actor3 - Found you</summary>
		public const string MSG_PLAY_ACTOR3_FOUND_YOU = "AHA! Encontrei você!";

		/// <summary>Actor1 - Lord of Exploits</summary>
		public const string MSG_PLAY_ACTOR1_LORD_EXPLOITS = "Senhor das Explorações, me leve!";

		/// <summary>Actor3 - Peter Grimm</summary>
		public const string MSG_PLAY_ACTOR3_PETER_GRIMM = "Aquele Peter Grimm... Sempre tentando encontrar novas explorações para ouro ilimitado.";

		#endregion

		#region Prompt Messages

		/// <summary>Prompt - Hello?</summary>
		public const string MSG_PROMPT_HELLO = "Olá?";

		/// <summary>Prompt - What will it be</summary>
		public const string MSG_PROMPT_WHAT_WILL_IT_BE = "Então, qual será, sim ou não?";

		/// <summary>Prompt - Tell me answer</summary>
		public const string MSG_PROMPT_TELL_ANSWER = "Você pode simplesmente me dizer sua resposta.";

		/// <summary>Prompt - Want me to repeat</summary>
		public const string MSG_PROMPT_WANT_REPEAT = "Você queria que eu repetisse? Apenas diga repetir.";

		/// <summary>Prompt - What'll it be friend</summary>
		public const string MSG_PROMPT_WHATLL_IT_BE = "Qual será, amigo?";

		/// <summary>Prompt - Tough decision</summary>
		public const string MSG_PROMPT_TOUGH_DECISION = "Eu sei... decisão difícil e tudo mais.";

		/// <summary>Prompt - Ask to repeat</summary>
		public const string MSG_PROMPT_ASK_REPEAT = "Se você quiser que eu repita o que escrevi, apenas me peça para repetir :)";

		#endregion

		#region Response Keywords

		/// <summary>Positive response keywords (comma-separated for parsing)</summary>
		public const string POSITIVE_RESPONSES = "eu me comprometo,eu faço,sim,claro,ok,okay";

		/// <summary>Negative response keywords (comma-separated for parsing)</summary>
		public const string NEGATIVE_RESPONSES = "não,nope,nunca";

		#endregion
	}
}


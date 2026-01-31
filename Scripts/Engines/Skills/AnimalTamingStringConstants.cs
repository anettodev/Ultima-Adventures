namespace Server.SkillHandlers
{
	/// <summary>
	/// Centralized string constants for AnimalTaming-related messages and labels.
	/// Extracted from AnimalTaming.cs to improve maintainability and enable localization.
	/// All strings are in Portuguese-Brazilian (PT-BR).
	/// </summary>
	public static class AnimalTamingStringConstants
	{
		#region Error Messages (Portuguese)

		/// <summary>Message when player is blessed and cannot tame</summary>
		public const string MSG_BLESSED_STATE = "Você não pode fazer isso neste estado.";

		#endregion

		#region Difficulty Messages (Portuguese)

		/// <summary>Message when skill difference is very small (almost able)</summary>
		public const string MSG_DIFF_ALMOST = "quase consigo entender os hábitos desta criatura";

		/// <summary>Message when skill difference is small (close to able)</summary>
		public const string MSG_DIFF_CLOSE = "estou perto de conseguir domar isso";

		/// <summary>Message when skill difference is moderate (needs more effort)</summary>
		public const string MSG_DIFF_EFFORT = "acho que vou conseguir domar isso com mais esforço";

		/// <summary>Message when skill difference is large (long way to go)</summary>
		public const string MSG_DIFF_LONG = "tenho um longo caminho para percorrer antes de conseguir domar isso";

		/// <summary>Message when skill difference is very large (too difficult)</summary>
		public const string MSG_DIFF_DIFFICULT = "esta criatura é muito difícil para mim agora";

		#endregion

		#region Tier System Messages (Portuguese)

		/// <summary>Message when taming lower tier creature - skill gain will be reduced (format: includes reduction percentage)</summary>
		public const string MSG_TIER_REDUCED_GAIN_FORMAT = "Esta criatura é muito fácil de domar. Você ganhará menos experiência.";

		/// <summary>Message when skill gain occurs from taming same tier or higher (format: includes skill gain percentage)</summary>
		public const string MSG_TIER_SKILL_GAIN_FORMAT = "Você ganhou experiência em Domar Animais!";

		#endregion

		#region Animal Lore Messages (Portuguese)

		/// <summary>Message when using Animal Lore on own pet - already know this animal</summary>
		public const string MSG_ANIMAL_LORE_OWN_PET = "Você já conhece este animal como a palma da sua mão!";

		/// <summary>Message when using Animal Lore on vendor-bought pet - no skill gain</summary>
		public const string MSG_ANIMAL_LORE_VENDOR_BOUGHT = "Este animal foi comprado de um vendedor - você não aprende nada novo aqui.";

		/// <summary>Message when Animal Lore skill gain occurs (format: includes skill gain percentage)</summary>
		public const string MSG_ANIMAL_LORE_SKILL_GAIN_FORMAT = "Seu conhecimento sobre animais aumentou!";

		/// <summary>Message when player doesn't know enough about the animal to use Animal Lore</summary>
		public const string MSG_ANIMAL_LORE_INSUFFICIENT_KNOWLEDGE = "Você não conhece este animal o suficiente para entender seus segredos...";

		/// <summary>Message when player successfully learns about the animal (even without skill gain)</summary>
		public const string MSG_ANIMAL_LORE_LEARNED = "Você aprendeu algo novo sobre este animal!";

		#endregion

		#region Evil Karma Speech (Portuguese)

		/// <summary>Evil karma speech option 0</summary>
		public const string SPEECH_EVIL_0 = "Venha aqui, seu vira-lata.";

		/// <summary>Evil karma speech option 1</summary>
		public const string SPEECH_EVIL_1 = "Vamos, se doma logo.";

		/// <summary>Evil karma speech option 2</summary>
		public const string SPEECH_EVIL_2 = "Considere-se sortudo por eu não apertar TAB...";

		/// <summary>Evil karma speech option 3</summary>
		public const string SPEECH_EVIL_3 = "Eu só vou te renomear e te matar.";

		/// <summary>Evil karma speech option 4</summary>
		public const string SPEECH_EVIL_4 = "Aposto que o corretor pagará bem por você.";

		/// <summary>Evil karma speech option 5</summary>
		public const string SPEECH_EVIL_5 = "Venha... mate meus inimigos e me deixe rico.";

		/// <summary>Evil karma speech option 6</summary>
		public const string SPEECH_EVIL_6 = "Sou muito preguiçoso para matar inimigos sozinho, preciso que você faça isso por mim, entende?";

		/// <summary>Evil karma speech option 7</summary>
		public const string SPEECH_EVIL_7 = "Eu só estava brincando quando pedi para você viajar comigo.";

		/// <summary>Evil karma speech option 8</summary>
		public const string SPEECH_EVIL_8 = "Se você não me der ganho, vai pagar.";

		/// <summary>Evil karma speech option 9</summary>
		public const string SPEECH_EVIL_9 = "Se não se domar agora, vou te deixar com fome por me fazer esperar.";

		/// <summary>Evil karma speech option 10</summary>
		public const string SPEECH_EVIL_10 = "Você pode valer mais em couros e carne.";

		/// <summary>Evil karma speech option 11 (format: includes tamer name)</summary>
		public const string SPEECH_EVIL_11_FORMAT = "Meu nome é {0} e eu sou seu dono.";

		/// <summary>Evil karma speech option 12 (format: includes creature type)</summary>
		public const string SPEECH_EVIL_12_FORMAT = "Você é o exemplo mais fraco de {0} que já vi.";

		#endregion

		#region Good Karma Speech (Portuguese)

		/// <summary>Good karma speech option 0</summary>
		public const string SPEECH_GOOD_0 = "Pode ir com calma! Quero que você confie em mim.";

		/// <summary>Good karma speech option 1</summary>
		public const string SPEECH_GOOD_1 = "Vou encontrar uma boa casa para você morar.";

		/// <summary>Good karma speech option 2</summary>
		public const string SPEECH_GOOD_2 = "Que criatura linda você é!";

		/// <summary>Good karma speech option 3</summary>
		public const string SPEECH_GOOD_3 = "Você pode fazer aquele barulho fofo de novo?";

		/// <summary>Good karma speech option 4</summary>
		public const string SPEECH_GOOD_4 = "Vou cuidar de você e crescer com você.";

		/// <summary>Good karma speech option 5</summary>
		public const string SPEECH_GOOD_5 = "Juntos vamos explorar lugares maravilhosos!";

		/// <summary>Good karma speech option 6</summary>
		public const string SPEECH_GOOD_6 = "Você e eu, sentados em uma árvore... espera, o quê?";

		/// <summary>Good karma speech option 7</summary>
		public const string SPEECH_GOOD_7 = "Aposto que seu pelo é macio... posso tocar?";

		/// <summary>Good karma speech option 8</summary>
		public const string SPEECH_GOOD_8 = "Considere vir comigo, amigo.";

		/// <summary>Good karma speech option 9</summary>
		public const string SPEECH_GOOD_9 = "De todas as criaturas ao meu redor, escolhi você, meu amigo.";

		/// <summary>Good karma speech option 10</summary>
		public const string SPEECH_GOOD_10 = "Vou treinar você para se tornar uma criatura melhor.";

		/// <summary>Good karma speech option 11</summary>
		public const string SPEECH_GOOD_11 = "Oooh que fofo, você acabou de lamber suas partes íntimas.";

		/// <summary>Good karma speech option 12 (format: includes tamer name)</summary>
		public const string SPEECH_GOOD_12_FORMAT = "Olá amigo, eu sou {0} e acho você muito fofo.";

		#endregion

		#region Drunk Speech Codes (Intentional Acronyms)

		/// <summary>Drunk speech code 0: "Sticks Stones May Break Bones"</summary>
		public const string DRUNK_CODE_0 = "ssmbaaa";

		/// <summary>Drunk speech code 1: "But words can't hurt me"</summary>
		public const string DRUNK_CODE_1 = "bwchmkvrsk";

		/// <summary>Drunk speech code 2: "Unless my giant angry tames"</summary>
		public const string DRUNK_CODE_2 = "umgatenhoo";

		/// <summary>Drunk speech code 3: "Mistake my commands for insults"</summary>
		public const string DRUNK_CODE_3 = "mmcfishh";

		/// <summary>Drunk speech code 4: "turn and eat my gear"</summary>
		public const string DRUNK_CODE_4 = "taemojunt";

		/// <summary>Drunk speech code 5: "and quickly kill me before"</summary>
		public const string DRUNK_CODE_5 = "aqkmbad";

		/// <summary>Drunk speech code 6: "I stop their mangy hides"</summary>
		public const string DRUNK_CODE_6 = "istmharia";

		#endregion
	}
}


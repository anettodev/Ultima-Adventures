namespace Server.Misc
{
	/// <summary>
	/// Centralized string constants for logging system messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from LoggingFunctions.cs to improve maintainability and enable localization.
	/// </summary>
	public static class LoggingStringConstants
	{
		#region Greeting Messages (Portuguese)

		/// <summary>Greeting for town crier announcements - variant 0</summary>
		public const string MSG_GREET_0 = "Ouçam, ouçam!";

		/// <summary>Greeting for town crier announcements - variant 1</summary>
		public const string MSG_GREET_1 = "Todos escutem!";

		/// <summary>Greeting for town crier announcements - variant 2</summary>
		public const string MSG_GREET_2 = "Todos saudem e ouçam minhas palavras!";

		/// <summary>Greeting for town crier announcements - variant 3</summary>
		public const string MSG_GREET_3 = "Sua atenção, por favor!";

		#endregion

		#region Default "No News" Messages (Portuguese)

		/// <summary>Default message when no murders</summary>
		public const string MSG_NO_MURDERS = "A justiça foi servida. Nenhum assassino vagueia pelas terras";

		/// <summary>Default message when no events - variant 0</summary>
		public const string MSG_NO_NEWS_0 = "Nada de interesse ocorreu nas terras";

		/// <summary>Default message when no events - variant 1</summary>
		public const string MSG_NO_NEWS_1 = "As coisas têm estado calmas ultimamente";

		/// <summary>Default message when no events - variant 2</summary>
		public const string MSG_NO_NEWS_2 = "As palavras faladas por aí não trazem notícias";

		/// <summary>Default message when no events - variant 3</summary>
		public const string MSG_NO_NEWS_3 = "Ainda não ouvi falar de eventos recentes";

		#endregion

		#region Verb Replacements for LogShout (Portuguese)

		/// <summary>Verb replacement for "entered" - variant 0</summary>
		public const string VERB_ENTERED_0 = "foi visto em";

		/// <summary>Verb replacement for "entered" - variant 1</summary>
		public const string VERB_ENTERED_1 = "foi avistado em";

		/// <summary>Verb replacement for "entered" - variant 2</summary>
		public const string VERB_ENTERED_2 = "era conhecido por estar em";

		/// <summary>Verb replacement for "entered" - variant 3</summary>
		public const string VERB_ENTERED_3 = "era rumorejado estar em";

		/// <summary>Verb replacement for "left" - variant 0</summary>
		public const string VERB_LEFT_0 = "foi visto saindo";

		/// <summary>Verb replacement for "left" - variant 1</summary>
		public const string VERB_LEFT_1 = "foi avistado saindo";

		/// <summary>Verb replacement for "left" - variant 2</summary>
		public const string VERB_LEFT_2 = "foi visto perto";

		/// <summary>Verb replacement for "left" - variant 3</summary>
		public const string VERB_LEFT_3 = "foi avistado por";

		#endregion

		#region Verb Replacements for LogSpeak (Portuguese)

		/// <summary>Verb replacement for LogSpeak "entered" - variant 0</summary>
		public const string VERB_SPEAK_ENTERED_0 = "sendo visto em";

		/// <summary>Verb replacement for LogSpeak "entered" - variant 1</summary>
		public const string VERB_SPEAK_ENTERED_1 = "sendo avistado em";

		/// <summary>Verb replacement for LogSpeak "entered" - variant 2</summary>
		public const string VERB_SPEAK_ENTERED_2 = "sendo visto em";

		/// <summary>Verb replacement for LogSpeak "entered" - variant 3</summary>
		public const string VERB_SPEAK_ENTERED_3 = "sendo avistado em";

		/// <summary>Verb replacement for LogSpeak "left" - variant 0</summary>
		public const string VERB_SPEAK_LEFT_0 = "sendo visto saindo";

		/// <summary>Verb replacement for LogSpeak "left" - variant 1</summary>
		public const string VERB_SPEAK_LEFT_1 = "sendo avistado saindo";

		/// <summary>Verb replacement for LogSpeak "left" - variant 2</summary>
		public const string VERB_SPEAK_LEFT_2 = "sendo visto perto";

		/// <summary>Verb replacement for LogSpeak "left" - variant 3</summary>
		public const string VERB_SPEAK_LEFT_3 = "sendo avistado por";

		/// <summary>Verb replacement for LogSpeak "slain" - variant 0</summary>
		public const string VERB_SPEAK_SLAIN_0 = "matando";

		/// <summary>Verb replacement for LogSpeak "slain" - variant 1</summary>
		public const string VERB_SPEAK_SLAIN_1 = "derrotando";

		/// <summary>Verb replacement for LogSpeak "slain" - variant 2</summary>
		public const string VERB_SPEAK_SLAIN_2 = "vencendo";

		/// <summary>Verb replacement for LogSpeak "slain" - variant 3</summary>
		public const string VERB_SPEAK_SLAIN_3 = "derrotando";

		#endregion

		#region LogRead Messages (Portuguese)

		/// <summary>Message when busy</summary>
		public const string MSG_BUSY_FORMAT = "Desculpe, {0}. Estou ocupado no momento.";

		/// <summary>Message when no murderers</summary>
		public const string MSG_NO_MURDERERS_FORMAT = "Tenho o prazer de dizer {0}, que ninguém está procurado por assassinato.";

		/// <summary>Message when no battles</summary>
		public const string MSG_NO_BATTLES_FORMAT = "Desculpe, {0}. Não tenho novas histórias de bravura para contar.";

		/// <summary>Message when no adventures</summary>
		public const string MSG_NO_ADVENTURES_FORMAT = "Desculpe, {0}. Não tenho novos boatos para contar.";

		/// <summary>Message when no quests</summary>
		public const string MSG_NO_QUESTS_FORMAT = "Desculpe, {0}. Não tenho novas histórias de feitos para contar.";

		/// <summary>Message when no deaths</summary>
		public const string MSG_NO_DEATHS_FORMAT = "Tenho o prazer de dizer {0}, que todos os cidadãos de Sosaria estão vivos e bem.";

		/// <summary>Message when no journies</summary>
		public const string MSG_NO_JOURNIES_FORMAT = "Desculpe, {0}. Não tenho novas histórias de exploração para contar.";

		/// <summary>Message when no misc</summary>
		public const string MSG_NO_MISC_FORMAT = "Desculpe, {0}. Não tenho nada para você, seu FodDoodle";

		/// <summary>Message when nothing new</summary>
		public const string MSG_NOTHING_NEW_FORMAT = "Desculpe, {0}. Não tenho nada novo para contar sobre tais coisas.";

		#endregion

		#region Event Action Strings (Portuguese)

		/// <summary>Action: entered the realm</summary>
		public const string ACTION_ENTERED_REALM = "entrou no reino";

		/// <summary>Action: left the realm</summary>
		public const string ACTION_LEFT_REALM = "saiu do reino";

		/// <summary>Action: had entered the realm</summary>
		public const string ACTION_HAD_ENTERED_REALM = "entrou no reino";

		/// <summary>Action: had left the realm</summary>
		public const string ACTION_HAD_LEFT_REALM = "saiu do reino";

		/// <summary>Action: had slain</summary>
		public const string ACTION_HAD_SLAIN = "derrotou";

		/// <summary>Action: had killed themselves</summary>
		public const string ACTION_HAD_KILLED_SELVES = "se matou";

		/// <summary>Action: had been killed by</summary>
		public const string ACTION_HAD_BEEN_KILLED_BY = "foi morto por";

		/// <summary>Action: had been killed</summary>
		public const string ACTION_HAD_BEEN_KILLED = "foi morto";

		/// <summary>Action: killed (for lastdeeds)</summary>
		public const string ACTION_KILLED = "matou";

		/// <summary>Murder message: is wanted for the murder of X people</summary>
		public const string MURDER_WANTED_MULTIPLE_FORMAT = "está procurado pelo assassinato de {0} pessoas.";

		/// <summary>Murder message: is wanted for murder</summary>
		public const string MURDER_WANTED_SINGLE = "está procurado por assassinato.";

		#endregion

		#region Trap Action Strings (Portuguese)

		/// <summary>Trap action: had triggered</summary>
		public const string TRAP_ACTION_TRIGGERED = "ativou";

		/// <summary>Trap action: had set off</summary>
		public const string TRAP_ACTION_SET_OFF = "disparou";

		/// <summary>Trap action: had walked into</summary>
		public const string TRAP_ACTION_WALKED_INTO = "pisou em";

		/// <summary>Trap action: had stumbled into</summary>
		public const string TRAP_ACTION_STUMBLED_INTO = "tropeçou em";

		/// <summary>Trap action: had been struck with</summary>
		public const string TRAP_ACTION_STRUCK_WITH = "foi atingido por";

		/// <summary>Trap action: had been affected with</summary>
		public const string TRAP_ACTION_AFFECTED_WITH = "foi afetado por";

		/// <summary>Trap action: had ran into</summary>
		public const string TRAP_ACTION_RAN_INTO = "correu para";

		#endregion

		#region Misc Event Strings (Portuguese)

		/// <summary>Action: spent gold on lottery tickets</summary>
		public const string ACTION_SPENT_LOTTERY_FORMAT = "gastou {0} de ouro em bilhetes de loteria!";

		/// <summary>Action: earned gold on investments</summary>
		public const string ACTION_EARNED_INVESTMENTS_FORMAT = "ganhou {0} de ouro em seus investimentos!";

		/// <summary>Action: lost gold on investments</summary>
		public const string ACTION_LOST_INVESTMENTS_FORMAT = "perdeu {0} de ouro em seus investimentos!";

		/// <summary>Action: won gold in lottery</summary>
		public const string ACTION_WON_LOTTERY_FORMAT = "ganhou {0} de ouro na Loteria!";

		/// <summary>Action: did not win lottery</summary>
		public const string ACTION_DID_NOT_WIN_LOTTERY_FORMAT = "não ganhou {0} de ouro que estava disponível na Loteria";

		/// <summary>Action: fell in love with wench</summary>
		public const string ACTION_FELL_IN_LOVE_WENCH = "se apaixonou pela Garota e deu todo seu ouro a ela!";

		/// <summary>Action: paid for wench services</summary>
		public const string ACTION_PAID_WENCH_FORMAT = "pagou {0} pelos serviços de uma garota.";

		/// <summary>Action: sold pet to broker</summary>
		public const string ACTION_SOLD_PET_FORMAT = "vendeu {0} por {1} de ouro para o corretor de animais.";

		/// <summary>Action: bred pet</summary>
		public const string ACTION_BRED_PET_FORMAT = "criou um {0} com nível máximo de {1} !";

		/// <summary>Action: died and rose as zombie</summary>
		public const string ACTION_DIED_ROSE_ZOMBIE = "morreu e ressuscitou como um Zumbi mortalmente infectado!!";

		/// <summary>Action: studied skill</summary>
		public const string ACTION_STUDIED_SKILL_FORMAT = "estudou {0} assiduamente e aumentou a habilidade {1} pontos!";

		/// <summary>Action: butchered</summary>
		public const string ACTION_BUTCHERED_FORMAT = "Em um ato incontrolável de pura sede de sangue, {0} massacrou {1}";

		/// <summary>Action: became grandmaster</summary>
		public const string ACTION_GRANDMASTER_FORMAT = "agora é um Grão-Mestre em {0}";

		/// <summary>Action: made fatal mistake</summary>
		public const string ACTION_FATAL_MISTAKE_FORMAT = "cometeu um erro fatal de {0}";

		#endregion

		#region Loot Action Strings (Portuguese)

		/// <summary>Loot action: had searched through a</summary>
		public const string LOOT_ACTION_SEARCHED = "procurou em um";

		/// <summary>Loot action: had found a</summary>
		public const string LOOT_ACTION_FOUND = "encontrou um";

		/// <summary>Loot action: had discovered a</summary>
		public const string LOOT_ACTION_DISCOVERED = "descobriu um";

		/// <summary>Loot action: had looked through a</summary>
		public const string LOOT_ACTION_LOOKED = "olhou em um";

		/// <summary>Loot action: had stumbled upon a</summary>
		public const string LOOT_ACTION_STUMBLED = "tropeçou em um";

		/// <summary>Loot action: had dug through a</summary>
		public const string LOOT_ACTION_DUG = "cavou em um";

		/// <summary>Loot action: had opened a</summary>
		public const string LOOT_ACTION_OPENED = "abriu um";

		/// <summary>Loot action: had sailed upon a</summary>
		public const string LOOT_ACTION_SAILED = "navegou em um";

		#endregion

		#region Slaying Action Strings (Portuguese)

		/// <summary>Slaying action: has defeated</summary>
		public const string SLAYING_ACTION_DEFEATED = "derrotou";

		/// <summary>Slaying action: has slain</summary>
		public const string SLAYING_ACTION_SLAIN = "matou";

		/// <summary>Slaying action: has destroyed</summary>
		public const string SLAYING_ACTION_DESTROYED = "destruiu";

		/// <summary>Slaying action: has vanquished</summary>
		public const string SLAYING_ACTION_VANQUISHED = "venceu";

		#endregion

		#region Quest Action Strings (Portuguese)

		/// <summary>Quest action: gods created artifact</summary>
		public const string QUEST_ACTION_GODS_CREATED_ARTIFACT_FORMAT = "Os deuses criaram um artefato lendário chamado {0}";

		/// <summary>Quest action: has cleansed runes</summary>
		public const string QUEST_ACTION_CLEANSED_RUNES = "purificou as Runas para a Câmara da Virtude.";

		/// <summary>Quest action: has corrupted runes</summary>
		public const string QUEST_ACTION_CORRUPTED_RUNES = "corrompeu as Runas da Virtude.";

		/// <summary>Quest action: Syth constructed weapon</summary>
		public const string QUEST_ACTION_SYTH_CONSTRUCTED_FORMAT = "Um Syth construiu uma arma chamada {0}";

		/// <summary>Quest action: Jedi constructed weapon</summary>
		public const string QUEST_ACTION_JEDI_CONSTRUCTED_FORMAT = "Um Jedi construiu uma arma chamada {0}";

		/// <summary>Quest action: has discovered the</summary>
		public const string QUEST_ACTION_DISCOVERED_THE = "descobriu o";

		/// <summary>Quest action: has found the</summary>
		public const string QUEST_ACTION_FOUND_THE = "encontrou o";

		/// <summary>Quest action: has recovered the</summary>
		public const string QUEST_ACTION_RECOVERED_THE = "recuperou o";

		/// <summary>Quest action: has unearthed the</summary>
		public const string QUEST_ACTION_UNEARTHED_THE = "desenterrou o";

		/// <summary>Quest action: has fished up</summary>
		public const string QUEST_ACTION_FISHED_UP = "pescou";

		/// <summary>Quest action: has surfaced</summary>
		public const string QUEST_ACTION_SURFACED = "emergiu";

		/// <summary>Quest action: has salvaged</summary>
		public const string QUEST_ACTION_SALVAGED = "recuperou";

		/// <summary>Quest action: has brought up</summary>
		public const string QUEST_ACTION_BROUGHT_UP = "trouxe à tona";

		/// <summary>Quest action: has fullfilled bounty</summary>
		public const string QUEST_ACTION_FULFILLED_BOUNTY = "cumpriu uma recompensa por";

		/// <summary>Quest action: has claimed bounty</summary>
		public const string QUEST_ACTION_CLAIMED_BOUNTY = "reivindicou uma recompensa por";

		/// <summary>Quest action: has served bounty</summary>
		public const string QUEST_ACTION_SERVED_BOUNTY = "serviu uma recompensa por";

		/// <summary>Quest action: has completed bounty</summary>
		public const string QUEST_ACTION_COMPLETED_BOUNTY = "completou uma recompensa por";

		/// <summary>Quest action: has assassinated</summary>
		public const string QUEST_ACTION_ASSASSINATED = "assassinou";

		/// <summary>Quest action: has dispatched</summary>
		public const string QUEST_ACTION_DISPATCHED = "eliminou";

		/// <summary>Quest action: has dealt with</summary>
		public const string QUEST_ACTION_DEALT_WITH = "lidou com";

		/// <summary>Quest action: has eliminated</summary>
		public const string QUEST_ACTION_ELIMINATED = "eliminou";

		/// <summary>Quest location: on the high seas</summary>
		public const string QUEST_LOCATION_HIGH_SEAS = "nos mares altos";

		/// <summary>Quest location: for the guild</summary>
		public const string QUEST_LOCATION_FOR_GUILD = "para a guilda";

		#endregion

		#region Quest Body Strings (Portuguese)

		/// <summary>Quest body: the bones</summary>
		public const string QUEST_BODY_BONES = "os ossos";

		/// <summary>Quest body: the body</summary>
		public const string QUEST_BODY_BODY = "o corpo";

		/// <summary>Quest body: the remains</summary>
		public const string QUEST_BODY_REMAINS = "os restos";

		/// <summary>Quest body: the corpse</summary>
		public const string QUEST_BODY_CORPSE = "o cadáver";

		#endregion

		#region Quest Chest Strings (Portuguese)

		/// <summary>Quest chest: the hidden</summary>
		public const string QUEST_CHEST_HIDDEN = "o escondido";

		/// <summary>Quest chest: the lost</summary>
		public const string QUEST_CHEST_LOST = "o perdido";

		/// <summary>Quest chest: the missing</summary>
		public const string QUEST_CHEST_MISSING = "o desaparecido";

		/// <summary>Quest chest: the secret</summary>
		public const string QUEST_CHEST_SECRET = "o secreto";

		#endregion

		#region Quest Sea Chest Strings (Portuguese)

		/// <summary>Quest sea chest: meager</summary>
		public const string QUEST_SEA_CHEST_MEAGER = "um baú submerso medíocre";

		/// <summary>Quest sea chest: simple</summary>
		public const string QUEST_SEA_CHEST_SIMPLE = "um baú submerso simples";

		/// <summary>Quest sea chest: good</summary>
		public const string QUEST_SEA_CHEST_GOOD = "um bom baú submerso";

		/// <summary>Quest sea chest: great</summary>
		public const string QUEST_SEA_CHEST_GREAT = "um grande baú submerso";

		/// <summary>Quest sea chest: excellent</summary>
		public const string QUEST_SEA_CHEST_EXCELLENT = "um excelente baú submerso";

		/// <summary>Quest sea chest: superb</summary>
		public const string QUEST_SEA_CHEST_SUPERB = "um baú submerso soberbo";

		#endregion

		#region Default Messages (Portuguese)

		/// <summary>Default message: adventurers in taverns</summary>
		public const string MSG_DEFAULT_ADVENTURERS_TAVERNS = "Aventureiros parecem estar todos sentados em tavernas";

		/// <summary>Default message: nothing of interest</summary>
		public const string MSG_DEFAULT_NOTHING_INTEREST = "nada de interesse ocorrendo nas terras";

		#endregion

		#region Broadcast Messages (Portuguese)

		/// <summary>Broadcast message: entered realm</summary>
		public const string MSG_BROADCAST_ENTERED_REALM_FORMAT = "{0} {1} entrou no reino";

		/// <summary>Broadcast message: left realm</summary>
		public const string MSG_BROADCAST_LEFT_REALM_FORMAT = "{0} {1} saiu do reino";

		#endregion

		#region Region Messages (Portuguese)

		/// <summary>Region message: entered region</summary>
		public const string MSG_REGION_ENTERED_FORMAT = "Você entrou em {0}{1}.";

		/// <summary>Region message: left region</summary>
		public const string MSG_REGION_LEFT_FORMAT = "Você saiu do(a) {0}.";

		#endregion

		#region English Strings for File Replacement (Keep for backward compatibility)

		/// <summary>English string: entered (for file replacement)</summary>
		public const string ENGLISH_ENTERED = " entered ";

		/// <summary>English string: left (for file replacement)</summary>
		public const string ENGLISH_LEFT = " left ";

		/// <summary>English string: had been (for file replacement)</summary>
		public const string ENGLISH_HAD_BEEN = " had been ";

		/// <summary>English string: had slain (for file replacement)</summary>
		public const string ENGLISH_HAD_SLAIN = " had slain ";

		/// <summary>English string: had killed (for file replacement)</summary>
		public const string ENGLISH_HAD_KILLED = " had killed ";

		/// <summary>English string: made a fatal mistake (for file replacement)</summary>
		public const string ENGLISH_FATAL_MISTAKE = " made a fatal mistake ";

		#endregion
	}
}


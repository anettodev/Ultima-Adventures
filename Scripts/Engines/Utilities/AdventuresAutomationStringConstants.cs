namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for AdventuresAutomation-related messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from AdventuresAutomation.cs to improve maintainability and enable easier localization.
	/// </summary>
	public static class AdventuresAutomationStringConstants
	{
		#region Action Strings (Internal)

		/// <summary>Action string identifier for fishing</summary>
		public const string ACTION_STRING_FISHING = "fishing";

		/// <summary>Action string identifier for mining</summary>
		public const string ACTION_STRING_MINING = "mining";

		/// <summary>Action string identifier for lumberjacking</summary>
		public const string ACTION_STRING_LUMBERJACKING = "lumberjacking";

		/// <summary>Action string identifier for skinning</summary>
		public const string ACTION_STRING_SKINNING = "skinning";

		/// <summary>Action string identifier for milling</summary>
		public const string ACTION_STRING_MILLING = "milling";

		/// <summary>Action string identifier for dough making</summary>
		public const string ACTION_STRING_DOUGH = "dough";

		/// <summary>Action string identifier for bread making</summary>
		public const string ACTION_STRING_BREAD = "bread";

		#endregion

		#region Command Strings

		/// <summary>Command to list available automation actions</summary>
		public const string COMMAND_LIST_ACTIONS = "auto-listar";

		/// <summary>Command to start fishing automation</summary>
		public const string COMMAND_FISHING = "auto-pescar";

		/// <summary>Command to start mining automation</summary>
		public const string COMMAND_MINING = "auto-minerar";

		/// <summary>Command to start lumberjacking automation</summary>
		public const string COMMAND_LUMBERJACKING = "auto-lenhar";

		/// <summary>Command to start skinning automation</summary>
		public const string COMMAND_SKINNING = "auto-esfolar";

		/// <summary>Command to start milling automation</summary>
		public const string COMMAND_MILLING = "auto-moer";

		/// <summary>Command to start dough making automation</summary>
		public const string COMMAND_DOUGH = "auto-massa";

		/// <summary>Command to start bread making automation</summary>
		public const string COMMAND_BREAD = "auto-panificar";

		#endregion

		#region Error Messages (Portuguese)

		/// <summary>Message when player is already automated</summary>
		public const string MSG_ALREADY_AUTOMATED = "Você já está realizando uma tarefa automática. Se quiser para, digite: '.parar' ou '.stop'.";

		/// <summary>Message when player has no backpack</summary>
		public const string MSG_NO_BACKPACK = "Você não tem uma mochila por algum motivo.";

		/// <summary>Message when player is dead</summary>
		public const string MSG_PLAYER_DEAD = "Você está morto e não pode fazer isso.";

		/// <summary>Message when player is in invalid location</summary>
		public const string MSG_INVALID_LOCATION = "Você se encontra em um local inválido!";

		/// <summary>Message when fishing pole is required</summary>
		public const string MSG_NEED_FISHING_POLE = "Você precisa segurar uma vara de pescar para pescar.";

		/// <summary>Message when mining tool is required</summary>
		public const string MSG_NEED_MINING_TOOL = "Você precisa equipar uma picareta ou possuir uma pá para minerar.";

		/// <summary>Message when axe is required for lumberjacking</summary>
		public const string MSG_NEED_AXE = "Você precisa segurar um machado para cortar árvores.";

		/// <summary>Message when skinning tools are required</summary>
		public const string MSG_NEED_SKINNING_TOOLS = "Você precisa de uma faca e uma tesoura em sua mochila para fazer isso.";

		/// <summary>Message when wheat is required for milling</summary>
		public const string MSG_NEED_WHEAT = "Você precisa de trigo na mochila para moer farinha!";

		/// <summary>Message when flour mill is required</summary>
		public const string MSG_NEED_FLOUR_MILL = "Você precisa estar próximo de um moinho de farinha para moer trigo.";

		/// <summary>Message when water source is required for dough</summary>
		public const string MSG_NEED_WATER_FOR_DOUGH = "Você precisa de uma fonte de água próxima para fazer massa.";

		/// <summary>Message when oven is required for bread</summary>
		public const string MSG_NEED_OVEN = "Você precisa de um forno próximo para fazer pão.";

		/// <summary>Message when craft tool is required</summary>
		public const string MSG_NEED_CRAFT_TOOL_FORMAT = "Você precisa da ferramenta adequada para criar {0}.";

		/// <summary>Message when no resources found nearby</summary>
		public const string MSG_NO_RESOURCES_FOUND = "Não foi possível encontrar recursos próximos.";

		/// <summary>Message when automation stops (player says)</summary>
		public const string MSG_FINISHED_HERE = "*Acho que acabei por aqui.*";

		/// <summary>Message when automation action stops</summary>
		public const string MSG_AUTOMATION_STOPPED = "* Ação automática parada! *";

		/// <summary>Message when player is overweight and drops items</summary>
		public const string MSG_OVERWEIGHT_DROP_ITEMS = "Você está muito pesado e deixa alguns itens caírem no chão.";

		/// <summary>Message when player is overweight (with period)</summary>
		public const string MSG_OVERWEIGHT_PERIOD = "Você está muito acima do peso máximo suportado!.";

		/// <summary>Message when player is overweight</summary>
		public const string MSG_OVERWEIGHT = "Você está muito acima do peso máximo suportado!";

		/// <summary>Message when player is too hungry or thirsty</summary>
		public const string MSG_TOO_HUNGRY_OR_THIRSTY = "Estou com muita fome ou sede para fazer isso agora.";

		/// <summary>Message when player moves and stops action</summary>
		public const string MSG_MOVED_STOPPED_ACTION = "Você se moveu e parou de fazer a ação.";

		/// <summary>Message when player forgets what they were doing</summary>
		public const string MSG_FORGOT_ACTION = "esqueci o que estava fazendo...";

		/// <summary>Message when tool has problems</summary>
		public const string MSG_TOOL_PROBLEM = "Há um problema com minha ferramenta.";

		/// <summary>Message when out of tools</summary>
		public const string MSG_OUT_OF_TOOLS = "*Aff! Estou sem ferramentas.*";

		/// <summary>Message when tool cannot be used and no replacement found</summary>
		public const string MSG_TOOL_BROKEN_NO_REPLACEMENT = "Sua ferramenta não pode ser mais usada e não há outra em sua mochila.";

		/// <summary>Message when skill is too low</summary>
		public const string MSG_SKILL_TOO_LOW = "Ainda não sei como fazer isso.";

		/// <summary>Message when crafting succeeds</summary>
		public const string MSG_CRAFT_SUCCESS_FORMAT = "Eba! Fiz uma comida ({0}).";

		/// <summary>Message when crafting fails</summary>
		public const string MSG_CRAFT_FAILED = "Você falhou e desperdiçou alguns recursos.";

		/// <summary>Message when not enough resources for crafting</summary>
		public const string MSG_NOT_ENOUGH_RESOURCES_FORMAT = "Hmm..tem algo faltando para criar isto. ({0})";

		#endregion

		#region Info Messages (Portuguese)

		/// <summary>Message listing available automation actions</summary>
		public const string MSG_AVAILABLE_ACTIONS = "Posso fazer as seguintes ações automáticas:";

		/// <summary>Message listing harvest actions</summary>
		public const string MSG_HARVEST_ACTIONS = "Auto-Pescar, Auto-Minerar, Auto-Lenhar";

		/// <summary>Message listing crafting actions</summary>
		public const string MSG_CRAFTING_ACTIONS = "Auto-Moer, Auto-Esfolar, Auto-Massa, Auto-Panificar";

		/// <summary>Sound/expression when overweight</summary>
		public const string MSG_OVERWEIGHT_EXPRESSION = "*argh!*";

		#endregion

		#region Item Names (Portuguese)

		/// <summary>Portuguese name for bread</summary>
		public const string ITEM_NAME_BREAD = "pão";

		/// <summary>Portuguese name for dough</summary>
		public const string ITEM_NAME_DOUGH = "massa";

		#endregion
	}
}


namespace Server.Mobiles
{
	/// <summary>
	/// Centralized string constants for TownGuards NPC messages and labels.
	/// Extracted from TownGuards.cs to improve maintainability and enable localization.
	/// All strings are in PT-BR (Portuguese - Brazil) for consistency with the server.
	/// </summary>
	public static class TownGuardsStringConstants
	{
		#region NPC Title

		/// <summary>Title for TownGuards NPC in Portuguese</summary>
		public const string TITLE_GUARD = "o guarda";

		#endregion

		#region Bounty Reward Messages

		/// <summary>Reward message format 1</summary>
		public const string BOUNTY_REWARD_1_FORMAT = "Aqui está a sua recompensa de {0} moedas de ouro.";

		/// <summary>Reward message format 2</summary>
		public const string BOUNTY_REWARD_2_FORMAT = "Tome o seu pagamento de {0} moedas de ouro.";

		/// <summary>Reward message format 3</summary>
		public const string BOUNTY_REWARD_3_FORMAT = "Sua recompensa é de {0} moedas de ouro.";

		/// <summary>Reward message format 4</summary>
		public const string BOUNTY_REWARD_4_FORMAT = "Este procurado tinha uma recompensa de {0} moedas de ouro.";

		#endregion

		#region Bounty Dialog Messages

		/// <summary>Dialog message format 1</summary>
		public const string BOUNTY_DIALOG_1_FORMAT = "Ora ora! Estavamos a muito tempo atrás desse aí. {0}";

		/// <summary>Dialog message format 2</summary>
		public const string BOUNTY_DIALOG_2_FORMAT = "Que satisfação hein aspira?! {0}";

		/// <summary>Dialog message format 3</summary>
		public const string BOUNTY_DIALOG_3_FORMAT = "Hmm..eu nunca achei que pegariam esse criminoso. {0}";

		/// <summary>Dialog message format 4</summary>
		public const string BOUNTY_DIALOG_4_FORMAT = "Os mares agora estão mais seguros. {0}";

		/// <summary>Dialog message format 5</summary>
		public const string BOUNTY_DIALOG_5_FORMAT = "Onde você achou esse traste!? {0}";

		#endregion

		#region DragDrop Messages

		/// <summary>Message when enemy tries to drop item</summary>
		public const string MSG_ENEMY_CANNOT_DROP = "Você não deveria estar carregando isso com você!";

		/// <summary>Message when unknown head type is dropped</summary>
		public const string MSG_UNKNOWN_HEAD_TYPE = "Irei assumir que ele lhe fez algum mal. Vou fazer vista grossa dessa vez!";

		#endregion

		#region Combat Messages

		/// <summary>Combat message 1 - Stop in the name of law</summary>
		public const string COMBAT_STOP_LAW = "AUTO! Pare em nome da lei!";

		/// <summary>Combat message 2 - Show justice</summary>
		public const string COMBAT_SHOW_JUSTICE = "Eu irei lhe mostrar a justiça!";

		/// <summary>Combat message 3 format - History ends</summary>
		public const string COMBAT_HISTORY_ENDS_FORMAT = "{0}!! Sua história acaba aqui e agora!";

		/// <summary>Combat message 4 format - We were after you</summary>
		public const string COMBAT_AFTER_YOU_FORMAT = "Estavamos atrás de você {0}!";

		/// <summary>Combat message 5 format - Soldiers alert</summary>
		public const string COMBAT_SOLDIERS_ALERT_FORMAT = "Soldados! {0} está aqui!";

		/// <summary>Combat message 6 format - Trained to hunt</summary>
		public const string COMBAT_TRAINED_HUNT_FORMAT = "Somos treinados para caçar criminosos como você {0}!";

		/// <summary>Combat message 7 format - Give up</summary>
		public const string COMBAT_GIVE_UP_FORMAT = "Desista! Irei acabar com você {0}!";

		/// <summary>Combat message 8 format - Sentence is death</summary>
		public const string COMBAT_SENTENCE_DEATH_FORMAT = "{0}! Sua sentença será a morte!";

		#endregion

		#region Timer Messages

		/// <summary>Message when guard returns to post</summary>
		public const string MSG_RETURNING_TO_POST = "Estou retornando para o meu posto!";

		#endregion

		#region Death Messages

		/// <summary>Message when guard prevents death</summary>
		public const string MSG_DEATH_PREVENTION = "A aura da irmandade militar irá me proteger!";

		#endregion

		#region Context Menu Messages

		/// <summary>Title for military conduct code gump</summary>
		public const string GUMP_TITLE_MILITARY_CONDUCT = "Código de Conduta Militar";

		/// <summary>Message when giving guard note to citizen</summary>
		public const string MSG_CITIZEN_ALERT = "Cidadão, fique atento!";

		#endregion

		#region Equipment Names

		/// <summary>Name for helm equipment</summary>
		public const string EQUIPMENT_NAME_HELM = "helm";

		/// <summary>Name for shield equipment</summary>
		public const string EQUIPMENT_NAME_SHIELD = "shield";

		#endregion
	}
}


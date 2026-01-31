namespace Server.SkillHandlers
{
	/// <summary>
	/// Centralized string constants for thievery skills (Stealing and Snooping) messages and labels.
	/// Extracted from Stealing.cs and Snooping.cs to improve maintainability and enable localization.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// </summary>
	public static class ThieveryStringConstants
	{
		#region Snooping Messages (Portuguese)

		/// <summary>Message when target is guarding possessions</summary>
		public const string MSG_TARGET_GUARDING = "O alvo está guardando seus pertences.";

		/// <summary>Message when cannot snoop in current state</summary>
		public const string MSG_CANNOT_SNOOP_STATE = "Você não pode bisbilhotar neste estado.";

		/// <summary>Message format when someone notices snooping attempt</summary>
		public const string MSG_NOTICE_SNOOPING_FORMAT = "Você percebe {0} tentando bisbilhotar os pertences de {1}.";

		/// <summary>Message when failed to peek into container</summary>
		public const string MSG_FAILED_PEEK = "Você falhou ao bisbilhotar o recipiente.";

		#endregion

		#region Stealing Error Messages (Portuguese)

		/// <summary>Message when cannot steal in current state</summary>
		public const string MSG_CANNOT_STEAL_STATE = "Você não pode roubar neste estado.";

		/// <summary>Message when unable to perform action in current state</summary>
		public const string MSG_UNABLE_STATE = "Você não pode fazer isso neste estado.";

		/// <summary>Message when trying to steal from dead/grave items</summary>
		public const string MSG_LEAVE_DEAD = "É melhor deixar os mortos em paz.";

		/// <summary>Message when trying to steal broken golem</summary>
		public const string MSG_NO_USE_BROKEN_GOLEM = "Você não tem uso para essa coisa de golem quebrada.";

		/// <summary>Message when wielding weapon while trying to steal</summary>
		public const string MSG_CANNOT_WIELD_WEAPON = "Você não pode estar empunhando uma arma ao tentar roubar algo.";

		/// <summary>Message when must be in thieves guild to steal from players</summary>
		public const string MSG_MUST_BE_IN_GUILD = "Você deve estar na guilda dos ladrões para roubar de outros jogadores.";

		/// <summary>Message when action wouldn't be right</summary>
		public const string MSG_WOULDNT_BE_RIGHT = "Isso não seria certo.";

		/// <summary>Message when action wouldn't be right for good alignment</summary>
		public const string MSG_WOULDNT_BE_RIGHT_GOOD = "Isso não seria certo para alguém comprometido a fazer o bem.";

		/// <summary>Message when no gold in coffer</summary>
		public const string MSG_NO_GOLD_IN_COFFER = "Parece não haver ouro no cofre.";

		/// <summary>Message format when successfully stealing gold from coffer</summary>
		public const string MSG_STOLE_GOLD_FROM_COFFER_FORMAT = "Você retira {0} moedas de ouro do cofre.";

		/// <summary>Message when fingers slip during coffer theft</summary>
		public const string MSG_FINGERS_SLIP = "Seus dedos escorregam, fazendo com que você seja notado!";

		/// <summary>Message when guards spot thief</summary>
		public const string MSG_STOP_THIEF = "Pare! Ladrão!";

		/// <summary>Message when cannot steal from shopkeepers</summary>
		public const string MSG_CANNOT_STEAL_SHOPKEEPERS = "Você não pode roubar de lojistas.";

		/// <summary>Message when cannot steal from vendors</summary>
		public const string MSG_CANNOT_STEAL_VENDORS = "Você não pode roubar de vendedores.";

		/// <summary>Message when target cannot be seen</summary>
		public const string MSG_TARGET_NOT_VISIBLE = "O alvo não pode ser visto.";

		/// <summary>Message when backpack is full</summary>
		public const string MSG_BACKPACK_FULL = "Sua mochila não pode carregar mais nada.";

		/// <summary>Message when cannot steal item</summary>
		public const string MSG_CANNOT_STEAL_THAT = "Você não pode roubar isso!";

		/// <summary>Message when must be next to item to steal</summary>
		public const string MSG_MUST_BE_NEXT_TO_ITEM = "Você deve estar ao lado de um item para roubá-lo.";

		/// <summary>Message when not skilled enough for artifact theft</summary>
		public const string MSG_NOT_SKILLED_ENOUGH = "Você não é habilidoso o suficiente para tentar roubar este item.";

		/// <summary>Message when cannot steal equipped items</summary>
		public const string MSG_CANNOT_STEAL_EQUIPPED = "Você não pode roubar itens que estão equipados.";

		/// <summary>Message when trying to steal from self</summary>
		public const string MSG_CATCH_SELF = "Você se pega em flagrante.";

		/// <summary>Message when item is too heavy to steal</summary>
		public const string MSG_TOO_HEAVY = "Isso é pesado demais para roubar.";

		/// <summary>Message when fumble steal attempt</summary>
		public const string MSG_FUMBLE_ATTEMPT = "Você falha na tentativa.";

		/// <summary>Message when successfully steal item</summary>
		public const string MSG_SUCCESSFULLY_STOLE = "Você rouba o item com sucesso.";

		/// <summary>Message when fail to steal item</summary>
		public const string MSG_FAILED_TO_STEAL = "Você falha ao roubar o item.";

		/// <summary>Message format when got item</summary>
		public const string MSG_GOT_ITEM_FORMAT = "Você conseguiu {0}!";

		/// <summary>Message when got something</summary>
		public const string MSG_GOT_SOMETHING = "Você conseguiu algo!";

		/// <summary>Message when fingers not quick enough</summary>
		public const string MSG_FINGERS_NOT_QUICK = "Seus dedos não são rápidos o suficiente.";

		/// <summary>Message when entity is guarding possessions</summary>
		public const string MSG_ENTITY_GUARDING = "Esta entidade está guardando seus pertences de perto... ";

		/// <summary>Message when cannot find anything to take</summary>
		public const string MSG_CANNOT_FIND_ANYTHING = "Você não consegue encontrar nada para tirar deste alvo.";

		/// <summary>Message when don't think can steal from that</summary>
		public const string MSG_DONT_THINK_CAN_STEAL = "Você não acha que poderia roubar isso.";

		/// <summary>Message format when notice someone trying to steal</summary>
		public const string MSG_NOTICE_STEALING_FORMAT = "Você percebe {0} tentando roubar de {1}.";

		/// <summary>Message when asking which item to steal</summary>
		public const string MSG_WHICH_ITEM_TO_STEAL = "Qual item você quer roubar?";

		/// <summary>Message when dump contents while stealing chest</summary>
		public const string MSG_DUMP_CONTENTS = "Você despeja todo o conteúdo ao roubar o item.";

		/// <summary>Message when not quick enough to steal chest</summary>
		public const string MSG_NOT_QUICK_ENOUGH = "Você não foi rápido o suficiente para roubá-lo.";

		/// <summary>Message when not strong enough</summary>
		public const string MSG_NOT_STRONG_ENOUGH = "Você simplesmente não é tão forte.";

		/// <summary>Message when steal quest item but not your quest</summary>
		public const string MSG_STOLE_WRONG_QUEST_ITEM = "Você rouba o item mencionado na nota, mas após perceber que a missão não foi dada a você, você joga o item fora.";

		/// <summary>Message format when found quest item</summary>
		public const string MSG_FOUND_QUEST_ITEM_FORMAT = "Você encontrou {0}.";

		/// <summary>Message when exploiting game mechanics</summary>
		public const string MSG_EXPLOITING_FORBIDDEN = "Explorar mecânicas do jogo é proibido!";

		#endregion

		#region Stealing Success Messages (Portuguese)

		/// <summary>Message format when successfully steal gold from coffer</summary>
		public const string MSG_STOLE_GOLD_FORMAT = "Você roubou {0} moedas de ouro.";

		#endregion
	}
}


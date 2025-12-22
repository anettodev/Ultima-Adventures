namespace Server.Custom
{
	/// <summary>
	/// Centralized string constants for Hitching Post messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from HitchingPost.cs to improve maintainability and enable localization.
	/// </summary>
	public static class HitchingPostStringConstants
	{
		#region Item Properties

		/// <summary>Item name: "Poste de Amarração"</summary>
		public const string ITEM_NAME = "Poste de Amarração";

		#endregion

		#region User Messages (Portuguese)

		/// <summary>Message when pet is released from post</summary>
		public const string MSG_PET_RELEASED = "Seu animal foi solto.";

		/// <summary>Message format when pet remains due to too many followers: "{0} permaneceu no poste porque você tem seguidores demais."</summary>
		public const string MSG_PET_REMAINED_FORMAT = "{0} permaneceu no poste porque você tem seguidores demais.";

		/// <summary>Message when post is already busy</summary>
		public const string MSG_POST_BUSY = "Desculpe! Este poste de amarração já está ocupado! Tente encontrar outro.";

		/// <summary>Message prompting player to choose a pet to hitch</summary>
		public const string MSG_WHAT_PET_HITCH = "Qual animal você deseja amarrar?";

		/// <summary>Message when player is too far away (currently commented out)</summary>
		public const string MSG_TOO_FAR_AWAY = "Você está muito longe!";

		/// <summary>Message when trying to hitch a GolemPorter</summary>
		public const string MSG_CANT_HITCH_GOLEM = "Você não consegue ver uma maneira de amarrar isso, tente o controlador.";

		/// <summary>Message when trying to hitch a Henchman or Squire</summary>
		public const string MSG_CANT_HITCH_HENCHMAN = "Você não consegue ver uma maneira de amarrar isso, tente algum equipamento de bondage.";

		/// <summary>Message when trying to hitch a BaseFamiliar</summary>
		public const string MSG_CANT_HITCH_FAMILIAR = "Apenas dispense o familiar se ele estiver te incomodando.";

		/// <summary>Message when pet is too far from post</summary>
		public const string MSG_MUST_BE_NEAR = "Você deve estar perto do poste de amarração para estabular o animal.";

		/// <summary>Message when trying to hitch a dead pet</summary>
		public const string MSG_CREATURE_MUST_BE_ALIVE = "A criatura deve estar viva!";

		/// <summary>Message when pet is already stabled</summary>
		public const string MSG_PET_ALREADY_STABLED = "Seu animal já está estabulado";

		/// <summary>Message when pet is successfully stabled</summary>
		public const string MSG_PET_STABLED_SUCCESS = "Seu animal foi estabulado. Você pode usar o poste de amarração novamente quando precisar reivindicar seu animal.";

		/// <summary>Message when trying to stable someone else's pet</summary>
		public const string MSG_CAN_STABLE_ONLY_OWN = "Você só pode estabular seus próprios animais!";

		/// <summary>Message when pet is not controlled</summary>
		public const string MSG_MUST_BE_CONTROLLING = "Você deve ser quem está controlando o animal!";

		#endregion
	}
}


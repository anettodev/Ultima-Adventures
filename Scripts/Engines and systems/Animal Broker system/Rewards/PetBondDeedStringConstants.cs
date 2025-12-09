namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for Pet Bond Deed messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from PetBondDeed.cs to improve maintainability and enable localization.
	/// </summary>
	public static class PetBondDeedStringConstants
	{
		#region Item Properties

		/// <summary>Item name: "Escritura de Vínculo de Animal"</summary>
		public const string ITEM_NAME = "Escritura de Vínculo de Animal";

		#endregion

		#region User Messages (Portuguese)

		/// <summary>Message when deed is not in backpack</summary>
		public const string MSG_NOT_IN_BACKPACK = "Isso precisa estar na sua mochila, bobinho.";

		/// <summary>Message prompting player to target an animal</summary>
		public const string MSG_TARGET_PROMPT = "Qual animal você deseja vincular?";

		/// <summary>Message when target is not a tamable animal</summary>
		public const string MSG_NOT_TAMABLE = "Este animal não pode ser domado!";

		/// <summary>Message when target is not the player's pet</summary>
		public const string MSG_NOT_YOUR_PET = "Este não é seu animal de estimação!";

		/// <summary>Message when pet is dead</summary>
		public const string MSG_PET_DEAD = "Este animal está morto...";

		/// <summary>Message when pet is summoned</summary>
		public const string MSG_PET_SUMMONED = "Este animal foi invocado";

		/// <summary>Message when trying to bond with a humanoid</summary>
		public const string MSG_HUMANOID_ERROR = "Você quer vincular um humanóide?? Hmm... tente um quarto.";

		/// <summary>Ludic messages when pet is already bonded (randomly selected)</summary>
		public static readonly string[] MSG_ALREADY_BONDED_LUDIC = new string[]
		{
			"*O animal olha para você com confusão* Ele já está vinculado a você, amigo!",
			"*Você tenta usar a escritura, mas percebe que o animal já está vinculado*",
			"*O animal já está vinculado a você!*",
			"*A escritura brilha por um momento, mas nada acontece* O animal já está vinculado a você!",
			"*Você observa o animal e percebe que já existe um vínculo forte*"
		};

		/// <summary>Message when player's skills are too low</summary>
		public const string MSG_SKILL_TOO_LOW = "Sua habilidade é muito baixa para controlar este animal!";

		/// <summary>Message format when bonding succeeds: "{0} agora está vinculado a você!"</summary>
		public const string MSG_BOND_SUCCESS_FORMAT = "{0} agora está vinculado a você!";

		/// <summary>Message when bonding fails due to error</summary>
		public const string MSG_BOND_ERROR = "Houve um problema ao vincular este animal..";

		/// <summary>Message when target is not an animal</summary>
		public const string MSG_TARGET_NOT_ANIMAL = "Você pode vincular somente animais";

		#endregion
	}
}


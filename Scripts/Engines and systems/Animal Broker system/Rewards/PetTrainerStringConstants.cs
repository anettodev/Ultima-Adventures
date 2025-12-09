namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for Pet Trainer messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from PetTrainer.cs to improve maintainability and enable localization.
	/// </summary>
	public static class PetTrainerStringConstants
	{
		#region Item Properties

		/// <summary>Item name: "Chicote de treino animal"</summary>
		public const string ITEM_NAME = "Chicote de treino animal";

		#endregion

		#region User Messages (Portuguese)

		/// <summary>Message when pet is dead</summary>
		public const string MSG_PET_MUST_BE_ALIVE = "O animal deve estar vivo!!!";

		/// <summary>Message when target is not the player's pet</summary>
		public const string MSG_NOT_YOUR_PET = "Este não é seu animal de estimação!";

		/// <summary>Message when pet is not bonded</summary>
		public const string MSG_MUST_BE_BONDED = "O animal deve estar vinculado a você!!!";

		/// <summary>Message when pet is at max skill level</summary>
		public const string MSG_MAX_SKILL_LEVEL = "O animal está no nível máximo de habilidades";

		/// <summary>Message when pet gains skill</summary>
		public const string MSG_PET_BECOMES_STRONGER = "Seu animal fica mais forte!!!";

		/// <summary>Message when pet gets angry</summary>
		public const string MSG_ANGER_THE_BEAST = "Você irrita a fera";

		/// <summary>Message when pet looks sheepishly (no effect)</summary>
		public const string MSG_PET_LOOKS_SHEEPISHLY = "Seu animal apenas olha para você com timidez...";

		/// <summary>Message when pet really gets angry (combat)</summary>
		public const string MSG_REALLY_ANGER_BEAST = "Você realmente irrita a fera!!!";

		/// <summary>Message when target is invalid</summary>
		public const string MSG_INVALID_TARGET = "Isso não é um alvo válido.";

		/// <summary>Message when tool breaks</summary>
		public const string MSG_TOOL_BROKE = "Infelizmente, você quebrou a ferramenta...";

		/// <summary>Message prompting player to choose a pet</summary>
		public const string MSG_CHOOSE_PET = "Escolha o animal que deseja treinar!!";

		#endregion
	}
}


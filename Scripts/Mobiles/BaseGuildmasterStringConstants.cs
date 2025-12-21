namespace Server.Mobiles
{
	/// <summary>
	/// Centralized string constants for BaseGuildmaster NPC messages and labels.
	/// Extracted from BaseGuildmaster.cs to improve maintainability and enable localization.
	/// </summary>
	public static class BaseGuildmasterStringConstants
	{
		#region Welcome Messages (Portuguese)

		/// <summary>Welcome message when player joins the guild</summary>
		public const string MSG_WELCOME_TO_GUILD = "Bem-vindo à confraria! Você achará benéfico para seus futuros empreendimentos.";

		#endregion

		#region Join Guild Messages (Portuguese)

		/// <summary>Message when player is blessed and cannot join</summary>
		public const string MSG_BLESSED_CANNOT_JOIN = "Fale comigo quando esse efeito estranho tiver passado.";

		/// <summary>Message when player profession prevents joining</summary>
		public const string MSG_PROFESSION_CANNOT_JOIN = "Não acho que poderíamos deixar alguém como você entrar.";

		/// <summary>Message when player karma is too high for Thieves Guild</summary>
		public const string MSG_KARMA_TOO_HIGH_THIEVES = "Ouvi dizer que você gosta de andar com o povo bom... você não se encaixaria aqui.";

		#endregion

		#region Ring Replacement Messages (Portuguese)

		/// <summary>Message when giving replacement guild ring</summary>
		public const string MSG_REPLACEMENT_RING = "Aqui está seu anel de reposição.";

		#endregion
	}
}


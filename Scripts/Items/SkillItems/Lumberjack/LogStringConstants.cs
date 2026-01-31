namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for log-related messages and labels.
	/// All strings are in Brazilian Portuguese (PT-BR).
	/// Extracted from Log.cs to improve maintainability and enable localization.
	/// </summary>
	public static class LogStringConstants
	{
		#region Item Names (Portuguese)

		/// <summary>Item name: "Tora(s)"</summary>
		public const string ITEM_NAME_LOG = "Tora(s)";

		/// <summary>Sawmill name for validation: "serraria"</summary>
		public const string SAWMILL_NAME = "serraria";

		#endregion

		#region User Messages (Portuguese)

		/// <summary>Message when selecting sawmill: "Selecione a serraria na qual deseja cortar as toras."</summary>
		public const string MSG_SELECT_SAWMILL = "Selecione a serraria na qual deseja cortar as toras.";

		/// <summary>Message when logs are too far: "As toras estão muito longe."</summary>
		public const string MSG_LOGS_TOO_FAR = "As toras estão muito longe.";

		/// <summary>Message when skill is insufficient: "Você não tem ideia de como cortar e trabalhar esse tipo de madeira!"</summary>
		public const string MSG_INSUFFICIENT_SKILL = "Você não tem ideia de como cortar e trabalhar esse tipo de madeira!";

		/// <summary>Message when wood amount is insufficient: "Não há madeira suficiente nesta pilha para fazer uma tábua."</summary>
		public const string MSG_INSUFFICIENT_WOOD = "Não há madeira suficiente nesta pilha para fazer uma tábua.";

		/// <summary>Message when conversion succeeds: "Você corta as toras e coloca algumas tábuas na mochila."</summary>
		public const string MSG_CONVERSION_SUCCESS = "Você corta as toras e coloca algumas tábuas na mochila.";

		/// <summary>Message when conversion fails completely: "Você tenta cortar as toras, mas estraga toda a madeira."</summary>
		public const string MSG_CONVERSION_FAILURE_ALL = "Você tenta cortar as toras, mas estraga toda a madeira.";

		/// <summary>Message when conversion fails partially: "Você tenta cortar as toras, mas estraga um pouco da madeira."</summary>
		public const string MSG_CONVERSION_FAILURE_PARTIAL = "Você tenta cortar as toras, mas estraga um pouco da madeira.";

		/// <summary>Message when target is not a sawmill: "Isso não é uma serraria"</summary>
		public const string MSG_NOT_SAWMILL = "Isso não é uma serraria";

		/// <summary>Message format for conversion bonus: "Você recebeu um bônus de {0}% na conversão!"</summary>
		public const string MSG_CONVERSION_BONUS_FORMAT = "Você recebeu um bônus de +{0}% em tábuas!";

		/// <summary>Emote for bonus conversion: "* woohoo *"</summary>
		public const string EMOTE_BONUS_CONVERSION = "* woohoo *";

		#endregion

		#region Color Constants

		/// <summary>Cyan color for resource type display in tooltips</summary>
		public const string COLOR_CYAN = "#8be4fc";

		#endregion
	}
}


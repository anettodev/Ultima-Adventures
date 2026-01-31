namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for TreasurePileAddon-related messages and labels.
	/// Extracted from TreasurePileAddon.cs to improve maintainability and enable localization.
	/// All messages are in Portuguese-Brazilian (PT-BR) as per codebase standards.
	/// </summary>
	public static class TreasurePileAddonStringConstants
	{
		#region Item Names (Portuguese)
		/// <summary>Base name for treasure pile addon deeds</summary>
		public const string DEED_BASE_NAME = "Bau do Tesouro";

		/// <summary>Name suffix format for treasure pile addon deeds</summary>
		public const string DEED_NAME_FORMAT = "Bau do Tesouro - #{0}";
		#endregion

		#region Type Labels (Portuguese)
		/// <summary>Type label for Minimal variation</summary>
		public const string TYPE_MINIMAL = "[Menor]";
		/// <summary>Type label for Compact variation</summary>
		public const string TYPE_COMPACT = "[Compacto]";
		/// <summary>Type label for Standard variation</summary>
		public const string TYPE_STANDARD = "[Padrão]";
		/// <summary>Type label for Medium variation</summary>
		public const string TYPE_MEDIUM = "[Médio]";
		/// <summary>Type label for Balanced variation</summary>
		public const string TYPE_BALANCED = "[Equilibrado]";
		/// <summary>Type label for Original variation</summary>
		public const string TYPE_ORIGINAL = "[Original]";
		/// <summary>Type label for Extended variation</summary>
		public const string TYPE_EXTENDED = "[Estendido]";
		/// <summary>Type label for Large variation</summary>
		public const string TYPE_LARGE = "[Grande]";
		#endregion
	}
}

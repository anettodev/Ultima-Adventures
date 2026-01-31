using System;

namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for CrystallineJar-related messages and labels.
	/// Extracted from CrystallineJar.cs to improve maintainability and enable localization.
	/// All strings translated to Portuguese-Brazilian.
	/// </summary>
	public static class CrystallineJarStringConstants
	{
		#region Item Names

		/// <summary>Name for empty crystalline flask</summary>
		public const string NAME_EMPTY_FLASK = "frasco cristalino";

		/// <summary>Name prefix when flask contains substance</summary>
		public const string NAME_PREFIX_FLASK = "frasco de ";

		/// <summary>Name for flask containing holy water</summary>
		public const string NAME_HOLY_WATER_FLASK = "frasco de água benta";

		#endregion

		#region Property Labels

		/// <summary>Property label describing flask's purpose</summary>
		public const string LABEL_HOLDS_SUBSTANCES = "Contém Substâncias Estranhas";

		#endregion

		#region User Prompts

		/// <summary>Prompt asking what to scoop into flask</summary>
		public const string MSG_SCOOP_PROMPT = "O que você deseja coletar no frasco?";

		/// <summary>Prompt asking where to dump flask contents</summary>
		public const string MSG_DUMP_PROMPT = "Onde você deseja despejar o conteúdo?";

		#endregion

		#region Error Messages

		/// <summary>Message when action doesn't feel right</summary>
		public const string MSG_BAD_IDEA = "Isso não parece uma boa ideia.";

		/// <summary>Message when too much liquid is already on ground</summary>
		public const string MSG_TOO_MUCH_LIQUID = "Já há muito líquido no chão.";

		/// <summary>Message when target is out of range</summary>
		public const string MSG_TOO_FAR = "Isso está muito longe.";

		/// <summary>Message when player cannot perform action yet</summary>
		public const string MSG_CANNOT_DO_YET = "Você não pode fazer isso ainda.";

		/// <summary>Message when substance is too diluted to collect</summary>
		public const string MSG_TOO_DILUTED = "Isso está muito diluído para coletar.";

		/// <summary>Message when flask is meant for different substances</summary>
		public const string MSG_WRONG_SUBSTANCE = "Este frasco é para outras substâncias.";

		#endregion
	}
}


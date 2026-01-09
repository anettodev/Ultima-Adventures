namespace Server.Misc
{
	/// <summary>
	/// Centralized string constants for SkillCheck-related messages and labels.
	/// Extracted from SkillCheck.cs to improve maintainability and enable localization.
	/// All strings are in Portuguese-Brazilian (PT-BR).
	/// </summary>
	public static class SkillCheckStringConstants
	{
		#region User Messages (Portuguese)

		/// <summary>Message when fishing skill requires boat after reaching 60</summary>
		public const string MSG_FISHING_REQUIRES_BOAT = "Você só ficará melhor em pescar se fizer isso de um barco.";

		/// <summary>Message when THC provides skill boost</summary>
		public const string MSG_THC_SKILL_BOOST = "Sua habilidade e foco parecem um pouco... maiores.";

		#endregion

		#region Debug Messages (Portuguese)

		/// <summary>Debug message when Animal Lore skill gain fails, showing success percentage and reason</summary>
		public const string MSG_DEBUG_ANIMAL_LORE_FAILED_FORMAT = "[DEBUG] Animal Lore: Falhou ao ganhar habilidade. Chance: {0:F2}% | AntiMacro: {1} | Lock: {2} | Cap: {3} | TotalCap: {4} ({5:F1}/{6:F1})";

		/// <summary>Debug message when Animal Taming skill gain fails, showing success percentage and reason</summary>
		public const string MSG_DEBUG_ANIMAL_TAMING_FAILED_FORMAT = "[DEBUG] Domar Animais: Falhou ao ganhar habilidade. Chance: {0:F2}% | AntiMacro: {1} | Lock: {2} | Cap: {3} | TotalCap: {4} ({5:F1}/{6:F1})";

		/// <summary>Debug message when Animal Lore skill gain succeeds, showing success percentage and amount gained</summary>
		public const string MSG_DEBUG_ANIMAL_LORE_SUCCESS_FORMAT = "[DEBUG] Animal Lore: Ganhou habilidade! Chance: {0:F2}% | Ganho: +{1:F1}";

		/// <summary>Debug message when Animal Taming skill gain succeeds, showing success percentage and amount gained</summary>
		public const string MSG_DEBUG_ANIMAL_TAMING_SUCCESS_FORMAT = "[DEBUG] Domar Animais: Ganhou habilidade! Chance: {0:F2}% | Ganho: +{1:F1}";

		#endregion
	}
}


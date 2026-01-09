namespace Server.Misc
{
    /// <summary>
    /// Centralized string constants for death penalty-related messages.
    /// Extracted from DeathPenalty.cs to improve maintainability and enable localization.
    /// All messages are in Portuguese (PT-BR).
    /// </summary>
    public static class DeathPenaltyStringConstants
    {
        #region Exemption Messages (Portuguese)

        /// <summary>Message when weak player would have lost skills but is exempt</summary>
        public const string MSG_WEAK_PLAYER_EXEMPTION = "Você teria sido punido se não fosse tão fraco ou um Iniciante.";

        /// <summary>Message when champion player is exempt from penalties</summary>
        public const string MSG_CHAMP_EXEMPTION = "Sua influência na força supera os perigos da morte!";

        /// <summary>Message format for balance effect reduction (parameter: amount lost)</summary>
        public const string MSG_BALANCE_EFFECT_REDUCED_FORMAT = "Mas sua influência na força foi reduzida por {0} da sua morte prematura.";

        /// <summary>Message when non-avatar player is exempt from penalties</summary>
        public const string MSG_NON_AVATAR_EXEMPTION = "Você evita qualquer penalidade por sua morte, pois suas ações não influenciam o equilíbrio da força.";

        #endregion

        #region Penalty Messages (Portuguese)

        /// <summary>Message when penalty is applied (fame/karma loss)</summary>
        public const string MSG_RESURRECTION_WEAKER = "Seu corpo revive pouco mais fraco.";

        /// <summary>Message for allPenalty case - Currently unused but kept for reference</summary>
        public const string MSG_RESURRECTION_MUCH_WEAKER = "Seu corpo revive muito mais fraco.";

        #endregion
    }
}


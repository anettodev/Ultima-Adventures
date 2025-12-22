namespace Server.Items
{
    /// <summary>
    /// Centralized string constants for poison potion messages.
    /// All strings are in Brazilian Portuguese (PT-BR).
    /// Extracted from BasePoisonPotion.cs to improve maintainability and enable easier localization.
    /// </summary>
    public static class PoisonPotionStringConstants
    {
        #region User Messages (Portuguese)

        /// <summary>Message when region doesn't allow harmful actions</summary>
        public const string MSG_REGION_NOT_ALLOWED = "Isso não parece uma boa ideia.";

        /// <summary>Message when there's too much splatter on the ground</summary>
        public const string MSG_TOO_MUCH_SPLATTER = "Já há muito líquido no chão.";

        /// <summary>Message prompting player to select dump location</summary>
        public const string MSG_SELECT_DUMP_LOCATION = "Onde você quer despejar o veneno?";

        /// <summary>Message when target is too far away</summary>
        public const string MSG_TARGET_TOO_FAR = "Isso está muito longe.";

        /// <summary>Message when player cannot perform action</summary>
        public const string MSG_CANNOT_ACT_YET = "Você não pode fazer isso ainda.";

        #endregion
    }
}


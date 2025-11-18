namespace Server.Items
{
    /// <summary>
    /// Centralized string constants for poison potion and venom sack item names and messages.
    /// All strings are in Brazilian Portuguese (PT-BR).
    /// Extracted from poison potion classes to improve maintainability and enable easier localization.
    /// </summary>
    public static class PoisonPotionItemStringConstants
    {
        #region Potion Names (Portuguese)

        /// <summary>Lesser poison potion name</summary>
        public const string NAME_LESSER_POISON = "poção de veneno menor";

        /// <summary>Regular poison potion name</summary>
        public const string NAME_POISON_POTION = "poção de veneno";

        /// <summary>Greater poison potion name</summary>
        public const string NAME_GREATER_POISON = "poção de veneno maior";

        /// <summary>Deadly poison potion name</summary>
        public const string NAME_DEADLY_POISON = "poção de veneno mortal";

        /// <summary>Lethal poison potion name</summary>
        public const string NAME_LETHAL_POISON = "poção de veneno letal";

        #endregion

        #region Venom Sack Names (Portuguese)

        /// <summary>Lesser venom sack name</summary>
        public const string NAME_LESSER_VENOM = "saco de veneno menor";

        /// <summary>Regular venom sack name</summary>
        public const string NAME_VENOM_SACK = "saco de veneno";

        /// <summary>Greater venom sack name</summary>
        public const string NAME_GREATER_VENOM = "saco de veneno maior";

        /// <summary>Deadly venom sack name</summary>
        public const string NAME_DEADLY_VENOM = "saco de veneno mortal";

        /// <summary>Lethal venom sack name</summary>
        public const string NAME_LETHAL_VENOM = "saco de veneno letal";

        #endregion

        #region User Messages (Portuguese)

        /// <summary>Message when bottle is needed for VenomSack</summary>
        public const string MSG_NEED_BOTTLE = "Você precisa de uma garrafa vazia para drenar o veneno do saco.";

        /// <summary>Message when getting partial potion from VenomSack</summary>
        public const string MSG_GET_POTION_PARTIAL = "Você obtém uma garrafa de {0} de parte do saco.";

        /// <summary>Message when getting complete potion from VenomSack</summary>
        public const string MSG_GET_POTION_COMPLETE = "Você obtém uma garrafa de {0} do resto do saco.";

        /// <summary>Message when poisoned by VenomSack</summary>
        public const string MSG_POISONED = "Veneno!";

        /// <summary>Message when extraction fails from VenomSack</summary>
        public const string MSG_FAILED_EXTRACTION = "Você falha em obter qualquer veneno do saco.";

        #endregion

        #region Property Labels (Portuguese)

        /// <summary>Property description for VenomSack use</summary>
        public const string PROP_USE_DESCRIPTION = "Use Para Tentar Extrair Veneno";

        /// <summary>Property description for VenomSack bottle requirement</summary>
        public const string PROP_NEED_BOTTLE = "Precisa De Uma Garrafa Vazia";

        #endregion

        #region Internal Potion Names (English - for internal use)

        /// <summary>Internal name for lesser poison (used in messages)</summary>
        public const string INTERNAL_LESSER_POISON = "veneno menor";

        /// <summary>Internal name for regular poison (used in messages)</summary>
        public const string INTERNAL_POISON = "veneno";

        /// <summary>Internal name for greater poison (used in messages)</summary>
        public const string INTERNAL_GREATER_POISON = "veneno maior";

        /// <summary>Internal name for deadly poison (used in messages)</summary>
        public const string INTERNAL_DEADLY_POISON = "veneno mortal";

        /// <summary>Internal name for lethal poison (used in messages)</summary>
        public const string INTERNAL_LETHAL_POISON = "veneno letal";

        #endregion
    }
}


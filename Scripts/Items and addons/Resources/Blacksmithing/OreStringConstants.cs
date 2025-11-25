namespace Server.Items
{
    /// <summary>
    /// Centralized string constants for ore-related messages in PT-BR.
    /// Extracted from Ore.cs to improve maintainability and enable localization.
    /// </summary>
    public static class OreStringConstants
    {
        #region Color Constants

        /// <summary>Cyan color for resource type display in tooltip</summary>
        public const string COLOR_CYAN = "#8be4fc";

        #endregion

        #region User Messages (Portuguese)

        /// <summary>Message when player cannot smelt the ore (insufficient skill)</summary>
        public const string MSG_CANNOT_SMELT_ORE = "Você não sabe como derreter este minério.";

        /// <summary>Message when there is not enough ore to make an ingot</summary>
        public const string MSG_NOT_ENOUGH_ORE_FOR_INGOT = "Você não tem minério suficiente para fazer um lingote.";

        /// <summary>Message when ore is successfully smelted</summary>
        public const string MSG_SMELTED_ORE_SUCCESS = "Você fundiu o minério removendo as impurezas e colocou o metal na mochila.";

        #endregion
    }
}


namespace Server.Items
{
    /// <summary>
    /// Centralized string constants for harvest tool messages and labels.
    /// Extracted from BaseHarvestTool.cs and Shovel.cs to improve maintainability and enable localization.
    /// </summary>
    public static class HarvestToolStringConstants
    {
        #region User Messages (Portuguese)
        
        /// <summary>Error: "Você não pode usar isso enquanto executa uma ação automatizada."</summary>
        public const string MSG_CANNOT_USE_WHILE_AUTOMATED = "Você não pode usar isso enquanto executa uma ação automatizada.";
        
        /// <summary>Automation hint for mining: "Diga '.auto-minerar' para usar o sistema de automação."</summary>
        public const string MSG_AUTOMATION_HINT_MINING = "Diga '.auto-minerar' para usar o sistema de automação.";
        
        /// <summary>Automation hint for lumberjacking: "Diga '.auto-lenhar' para usar o sistema de automação."</summary>
        public const string MSG_AUTOMATION_HINT_LUMBERJACKING = "Diga '.auto-lenhar' para usar o sistema de automação.";
        
        /// <summary>Automation hint for fishing: "Diga '.auto-pescar' para usar o sistema de automação."</summary>
        public const string MSG_AUTOMATION_HINT_FISHING = "Diga '.auto-pescar' para usar o sistema de automação.";
        
        /// <summary>Ore shovel special property: "* Apenas extrai minério/pedra de ferro! *"</summary>
        public const string MSG_ORE_SHOVEL_SPECIAL = "* Apenas extrai minério/pedra de ferro! *";
        
        /// <summary>Lumber axe special property: "Este machado apenas corta madeira comum"</summary>
        public const string MSG_LUMBER_AXE_SPECIAL = "Este machado apenas corta madeira comum";
        
        /// <summary>Mystical tree sap special property: "Cola peças de madeira"</summary>
        public const string MSG_MYSTICAL_TREE_SAP_SPECIAL = "Cola peças de madeira";
        
        /// <summary>Reaper oil special property: "Suaviza Madeira para Moldagem"</summary>
        public const string MSG_REAPER_OIL_SPECIAL = "Suaviza Madeira para Moldagem";
        
        #endregion
        
        #region Property Labels (Portuguese)
        
        /// <summary>Quality label: "Excepcional"</summary>
        public const string LABEL_EXCEPTIONAL = "Excepcional";
        
        #endregion
        
        #region Item Names (Portuguese)
        
        /// <summary>Shovel name: "Pá"</summary>
        public const string ITEM_NAME_SHOVEL = "Pá";
        
        #endregion
        
        #region Color Constants
        
        /// <summary>Cyan color for crafter name and resource name</summary>
        public const string COLOR_CYAN = "#8be4fc";
        
        /// <summary>Yellow color for exceptional quality</summary>
        public const string COLOR_YELLOW = "#ffe066";
        
        /// <summary>Salmon color for special properties</summary>
        public const string COLOR_SALMON = "#FA8072";
        
        /// <summary>Orange color for special properties</summary>
        public const string COLOR_ORANGE = "#FFA500";
        
        #endregion
    }
}


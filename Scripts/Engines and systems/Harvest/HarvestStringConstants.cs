using System;

namespace Server.Engines.Harvest
{
    /// <summary>
    /// Centralized string constants for harvest system messages and labels.
    /// Extracted from Mining.cs, Lumberjacking.cs, and Fishing.cs to improve maintainability and enable localization.
    /// </summary>
    public static class HarvestStringConstants
    {
        #region Mining Messages (Portuguese)

        /// <summary>Message when finding iron ore</summary>
        public const string MSG_FOUND_IRON_ORE = "Você encontrou alguns minérios de ferro.";

        /// <summary>Message when finding dull copper ore</summary>
        public const string MSG_FOUND_DULL_COPPER_ORE = "Você encontrou alguns minérios de cobre rústico.";

        /// <summary>Message when finding copper ore</summary>
        public const string MSG_FOUND_COPPER_ORE = "Você encontrou alguns minérios de cobre.";

        /// <summary>Message when finding bronze ore</summary>
        public const string MSG_FOUND_BRONZE_ORE = "Você encontrou alguns minérios de bronze.";

        /// <summary>Message when finding shadow iron ore</summary>
        public const string MSG_FOUND_SHADOW_IRON_ORE = "Você encontrou alguns minérios de ferro negro.";

        /// <summary>Message when finding platinum ore</summary>
        public const string MSG_FOUND_PLATINUM_ORE = "Você encontrou alguns minérios de platina.";

        /// <summary>Message when finding gold ore</summary>
        public const string MSG_FOUND_GOLD_ORE = "Você encontrou alguns minérios de dourado.";

        /// <summary>Message when finding agapite ore</summary>
        public const string MSG_FOUND_AGAPITE_ORE = "Você encontrou alguns minérios de agapite.";

        /// <summary>Message when finding verite ore</summary>
        public const string MSG_FOUND_VERITE_ORE = "Você encontrou alguns minérios de verite.";

        /// <summary>Message when finding valorite ore</summary>
        public const string MSG_FOUND_VALORITE_ORE = "Você encontrou alguns minérios de valorite.";

        /// <summary>Message when finding titanium ore</summary>
        public const string MSG_FOUND_TITANIUM_ORE = "Você encontrou alguns minérios de titânio.";

        /// <summary>Message when finding rosenium ore</summary>
        public const string MSG_FOUND_ROSENIUM_ORE = "Você encontrou alguns minérios de rosênio.";

        /// <summary>Message when finding sand</summary>
        public const string MSG_FOUND_SAND = "Você encontrou areia suficiente para fazer vidro.";

        #endregion

        #region Lumberjacking Messages (Portuguese)

        /// <summary>Message when cutting regular logs</summary>
        public const string MSG_CUT_REGULAR_LOGS = "Você cortou algumas toras";

        /// <summary>Message when cutting ash logs</summary>
        public const string MSG_CUT_ASH_LOGS = "Você cortou algumas toras de Carvalho cinza";

        /// <summary>Message when cutting ebony logs</summary>
        public const string MSG_CUT_EBONY_LOGS = "Você cortou algumas toras de ébano";

        /// <summary>Message when cutting golden oak logs</summary>
        public const string MSG_CUT_GOLDEN_OAK_LOGS = "Você cortou algumas toras de Ipê-amarelo";

        /// <summary>Message when cutting cherry logs</summary>
        public const string MSG_CUT_CHERRY_LOGS = "Você cortou algumas toras de Cerejeira";

        /// <summary>Message when cutting rosewood logs</summary>
        public const string MSG_CUT_ROSEWOOD_LOGS = "Você cortou algumas toras de Pau-Brasil";

        /// <summary>Message when cutting elven logs</summary>
        public const string MSG_CUT_ELVEN_LOGS = "Você cortou algumas toras de madeira élfica";

        /// <summary>Message when cutting hickory logs</summary>
        public const string MSG_CUT_HICKORY_LOGS = "Você cortou algumas toras de Nogueira Branca";

        /// <summary>Message when finding mushrooms</summary>
        public const string MSG_FOUND_MUSHROOMS = "Eba! Você achou cogumelos.";

        /// <summary>Message when finding reaper oil</summary>
        public const string MSG_FOUND_REAPER_OIL = "Eba! Você achou óleo ceifador.";

        /// <summary>Message when finding mystical tree sap</summary>
        public const string MSG_FOUND_MYSTICAL_SAP = "Eba! Você achou seiva de árvore mística.";

        /// <summary>Message when finding oil wood</summary>
        public const string MSG_FOUND_OIL_WOOD = "Eba! Você achou óleo mutagênico!";

        #endregion

        #region Fishing Messages (Portuguese)

        /// <summary>Message when catching common fish</summary>
        public const string MSG_CAUGHT_COMMON_FISH = "Você pescou um peixe comum!";

        #endregion

        #region General Harvest Messages (Portuguese)

        /// <summary>Message when carefully extracting workable stone</summary>
        public const string MSG_EXTRACTED_STONE = "Você extrai cuidadosamente alguma pedra trabalhável da veia de minério!";

        /// <summary>Message when digging for stone mining</summary>
        public const string MSG_DIGGING_STONE = "Você cava em busca de pedra...";

        #endregion
    }
}

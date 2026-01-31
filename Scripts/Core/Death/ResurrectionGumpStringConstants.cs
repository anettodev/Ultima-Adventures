namespace Server.Misc
{
    /// <summary>
    /// Centralized string constants for resurrection gump-related messages and labels.
    /// Extracted from ResurrectNowGump.cs and ResurrectCostGump.cs to improve maintainability and enable localization.
    /// All messages are in Portuguese (PT-BR).
    /// </summary>
    public static class ResurrectionGumpStringConstants
    {
        #region Button Labels (Portuguese)

        /// <summary>Accept resurrection button label</summary>
        public const string BUTTON_RESURRECT_ME = "Ressuscite-me";

        /// <summary>Cancel button label for ResurrectNowGump</summary>
        public const string BUTTON_MAYBE_LATER = "Talvez mais tarde";

        /// <summary>Cancel and suppress button label</summary>
        public const string BUTTON_NO_AND_STOP_ASKING = "Não e Pare de perguntar";

        /// <summary>No button label for ResurrectCostGump</summary>
        public const string BUTTON_NO = "Não";

        #endregion

        #region Gump Labels (Portuguese)

        /// <summary>Label for tribute value</summary>
        public const string LABEL_TRIBUTE_VALUE = "Valor do Tributo";

        /// <summary>Label for tribute (shorter version)</summary>
        public const string LABEL_TRIBUTE = "Tributo";

        /// <summary>Label for bank gold</summary>
        public const string LABEL_BANK_GOLD = "Dinheiro no banco";

        /// <summary>Format string for gold amount: "{0} Moeda(s) de Ouro"</summary>
        public const string FORMAT_GOLD_AMOUNT = "{0} Moeda(s) de Ouro";

        /// <summary>Format string for gold with k notation: "{0}k Moeda(s) de Ouro"</summary>
        public const string FORMAT_GOLD_K = "{0}k Moeda(s) de Ouro";

        /// <summary>Format string for gold with kk notation: "{0}kk Moeda(s) de Ouro"</summary>
        public const string FORMAT_GOLD_KK = "{0}kk Moeda(s) de Ouro";

        #endregion

        #region ResurrectNowGump Messages (Portuguese)

        /// <summary>Message asking if player wants to beg gods for life back, with fame/karma loss percentage</summary>
        public const string MSG_BEG_GODS_FORMAT = "Você deseja implorar aos deuses por sua vida de volta? Se fizer isso, você sofrerá uma perda de {0}% em sua fama e karma.";

        /// <summary>Message suggesting player has enough gold to pay tribute instead</summary>
        public const string MSG_ENOUGH_GOLD_SUGGESTION = "Você tem ouro suficiente no banco para oferecer o tributo da ressurreição, então talvez você queira encontrar um santuário ou curandeiro em vez de sofrer essas penalidades.";

        /// <summary>Message indicating player cannot pay tribute due to lack of gold</summary>
        public const string MSG_CANNOT_PAY_TRIBUTE = "Você não pode pagar o tributo da ressurreição devido à falta de ouro no banco, então talvez você queira fazer isso.";

        /// <summary>Message about losing stats and skills percentage - DEPRECATED (no longer used)</summary>
        public const string MSG_LOSE_STATS_SKILLS_FORMAT = "Você também perderá {0}% de suas estatísticas e todas as habilidades.";

        /// <summary>Message about resurrection delay and debt count</summary>
        public const string MSG_RESURRECTION_DEBT_FORMAT = "Você terá que esperar {0} segundo(s) para ser ressuscitado e ficará com {1} débito(s) com os curandeiros ({2} moedas de ouro).";

        /// <summary>Message when player decides to remain in spiritual realm</summary>
        public const string MSG_REMAIN_SPIRITUAL_REALM = "Você decide permanecer no plano espiritual.";

        #endregion

        #region ResurrectCostGump Messages (Portuguese)

        /// <summary>Message when player has enough gold to pay healer tribute</summary>
        public const string MSG_ENOUGH_GOLD_FOR_HEALER = "Atualmente você tem ouro suficiente no banco para fazer uma oferenda ao curandeiro. Você deseja prestar homenagem ao curandeiro pela sua vida de volta?";

        /// <summary>Message about fame/karma loss percentage (with line break)</summary>
        public const string MSG_FAME_KARMA_LOSS_FORMAT = "<br/>Se fizer isso, você sofrerá uma perda de {0}% de perda de sua fama e karma.";

        /// <summary>Message about losing random skills percentage - DEPRECATED (no longer used)</summary>
        public const string MSG_LOSE_RANDOM_SKILLS_FORMAT = "Você também perderá {0}% de algumas habilidades aleatórias.";

        /// <summary>Message when player doesn't have enough gold for healer tribute</summary>
        public const string MSG_NOT_ENOUGH_GOLD_FOR_HEALER = "Atualmente você não tem ouro suficiente no banco para oferecer uma oferenda ao curandeiro. Você deseja implorar ao curador pela sua vida de volta agora, sem prestar homenagem?";

        /// <summary>Message about shrine not requiring gold</summary>
        public const string MSG_SHRINE_NO_GOLD_REQUIRED = "Felizmente para você, o santuário não precisa de ouro para trazê-lo de volta à vida. Você deseja implorar aos deuses por sua vida de volta agora?";

        /// <summary>Message about losing stats and random skills percentage - DEPRECATED (no longer used)</summary>
        public const string MSG_LOSE_STATS_RANDOM_SKILLS_FORMAT = "Você também perderá {0}% de suas estatísticas e habilidades aleatórias.";

        /// <summary>Message about resurrection delay and debt count for ResurrectCostGump</summary>
        public const string MSG_RESURRECTION_DEBT_COST_FORMAT = "Você terá que esperar {0} segundo(s) para ser ressuscitado e ficará com {1} débito(s) com os curandeiros ({2} moedas de ouro).";

        /// <summary>Message for Azrael when player has enough gold</summary>
        public const string MSG_AZRAEL_ENOUGH_GOLD = "Azrael ainda não está pronto para receber sua alma e atualmente você tem ouro suficiente no banco para fazer uma oferenda a ele. Você deseja prestar homenagem a Azrael pela sua vida de volta?";

        /// <summary>Message for Reaper when player has enough gold</summary>
        public const string MSG_REAPER_ENOUGH_GOLD = "Embora o Ceifador ficasse feliz em levar sua alma, ele acha que seu tempo chegou ao fim muito cedo. Atualmente você tem ouro suficiente no banco para fazer uma oferenda ao Reaper. Você deseja prestar-lhe uma homenagem pela sua vida de volta?";

        /// <summary>Message for Goddess of Sea when player has enough gold</summary>
        public const string MSG_GODDESS_SEA_ENOUGH_GOLD = "Atualmente você tem ouro suficiente no banco para fazer uma oferenda à deusa do mar. Você deseja homenagear Anfitrite pela sua vida de volta?";

        /// <summary>Simple message asking if player wants healer to bring them back</summary>
        public const string MSG_HEALER_BRING_BACK = "Você deseja que o curandeiro o traga de volta à vida?";

        /// <summary>Simple message asking if player wants gods to bring them back</summary>
        public const string MSG_GODS_BRING_BACK = "Você deseja que os deuses o devolvam à vida?";

        /// <summary>Simple message asking if player wants Azrael to bring them back</summary>
        public const string MSG_AZRAEL_BRING_BACK = "Você deseja que Azrael o traga de volta à vida?";

        /// <summary>Simple message asking if player wants Reaper to bring them back</summary>
        public const string MSG_REAPER_BRING_BACK = "Você deseja que o Reaper o traga de volta à vida?";

        /// <summary>Simple message asking if player wants Goddess of Sea to bring them back</summary>
        public const string MSG_GODDESS_SEA_BRING_BACK = "Você deseja que Anfitrite o traga de volta à vida?";

        /// <summary>Ludic message for Young players (Iniciante) indicating free resurrection</summary>
        public const string MSG_YOUNG_FREE_RESURRECTION = "<br/><br/>*Como você é um Iniciante, a ressurreição não tem custo! Os deuses protegem os novatos em suas primeiras aventuras.*";

        /// <summary>Message when player cannot be resurrected at location</summary>
        public const string MSG_CANNOT_RESURRECT_HERE = "Você não pode ser ressuscitado aqui!";

        /// <summary>Message when player must wait before resurrecting without payment</summary>
        public const string MSG_MUST_WAIT_FORMAT = "Você precisa esperar mais {0} segundo(s) antes de poder ser ressuscitado sem pagar.";

        /// <summary>Message when player pays off all debts</summary>
        public const string MSG_DEBTS_PAID_FORMAT = "Você pagou todos os seus débitos ({0} moedas de ouro).";

        /// <summary>Message when player still has debts remaining</summary>
        public const string MSG_DEBTS_REMAINING_FORMAT = "Você ainda possui {0} débito(s) com os curandeiros ({1} moedas de ouro).";

        /// <summary>Message showing total debits and how many will be paid (when player has money)</summary>
        public const string MSG_DEBTS_INFO_FORMAT = "<br/><br/>Você possui {0} débito(s) com os curandeiros ({1} moedas de ouro). Com seu dinheiro atual, você pagará {2} débito(s) {3}.";

        /// <summary>Text indicating partial payment of debits</summary>
        public const string MSG_DEBTS_PARTIAL = " ";//"(parcial)";

        /// <summary>Text indicating full payment of debits</summary>
        public const string MSG_DEBTS_FULL = " ";//completo)";

        /// <summary>Message indicating remaining debits when player can pay resurrection but not all debits</summary>
        public const string MSG_REMAINING_DEBTS_OBS_FORMAT = "<br/><br/>Obs: Ainda há débitos anteriores a serem pagos ({0} débito(s) por {1} gp)";

        /// <summary>Ludic message when healer brings player back from the land of the dead</summary>
        public const string MSG_COMEBACK_FROM_DEAD = "*Você retornou das terras dos mortos!*";

        /// <summary>Message when resurrection delay process starts (ludic PT-BR)</summary>
        public const string MSG_RESURRECTION_DELAY_START_FORMAT = "O processo de ressurreição começou! Você precisa esperar {0} segundo(s) enquanto as forças místicas trabalham para trazer você de volta à vida.";

        /// <summary>Message showing remaining seconds in resurrection delay (ludic PT-BR)</summary>
        public const string MSG_RESURRECTION_DELAY_REMAINING_FORMAT = "O processo de ressurreição continua... Ainda faltam {0} segundo(s) para você retornar à vida.";

        /// <summary>Message when resurrection delay completes (ludic PT-BR)</summary>
        public const string MSG_RESURRECTION_DELAY_COMPLETE = "As forças místicas completaram o processo! Você está sendo ressuscitado agora!";

        #endregion

        #region Grave Messages (Portuguese) - ResurrectNowGump

        /// <summary>Grave message option 1: "VOCÊ MORREU!"</summary>
        public const string GRAVE_MSG_NOW_1 = "VOCÊ MORREU!";

        /// <summary>Grave message option 2: "VOCÊ PERECEU!"</summary>
        public const string GRAVE_MSG_NOW_2 = "VOCÊ PERECEU!";

        /// <summary>Grave message option 3: "VOCÊ CONHECEU SEU FIM!"</summary>
        public const string GRAVE_MSG_NOW_3 = "VOCÊ CONHECEU SEU FIM!";

        /// <summary>Grave message option 4: "SUA VIDA ACABOU!"</summary>
        public const string GRAVE_MSG_NOW_4 = "SUA VIDA ACABOU!";

        #endregion

        #region Grave Messages (Portuguese) - ResurrectCostGump

        /// <summary>Grave message option 1: "SUA VIDA DE VOLTA"</summary>
        public const string GRAVE_MSG_COST_1 = "SUA VIDA DE VOLTA";

        /// <summary>Grave message option 2: "SUA RESSURREIÇÃO"</summary>
        public const string GRAVE_MSG_COST_2 = "SUA RESSURREIÇÃO";

        /// <summary>Grave message option 3: "VOLTAR À VIDA"</summary>
        public const string GRAVE_MSG_COST_3 = "VOLTAR À VIDA";

        /// <summary>Grave message option 4: "RETORNO DOS MORTOS"</summary>
        public const string GRAVE_MSG_COST_4 = "RETORNO DOS MORTOS";

        #endregion
    }
}


namespace Server.Mobiles
{
    /// <summary>
    /// Centralized string constants for Banker NPC messages and labels.
    /// Extracted from Banker.cs to improve maintainability and enable localization.
    /// All messages are in Portuguese-Brazilian (PT-BR) as per codebase standards.
    /// </summary>
    public static class BankerStringConstants
    {
        #region Deposit Messages

        /// <summary>Message when player asks about depositing</summary>
        public const string MSG_DEPOSIT_INFO = "Para depositar, simplesmente me dê o ouro. Você pode confiar em mim.";

        /// <summary>Message when gold is successfully deposited</summary>
        public const string MSG_DEPOSIT_SUCCESS = "Muito bem, depositei {0} moedas de ouro em sua conta.";

        /// <summary>Message when Midland fee is charged</summary>
        public const string MSG_MIDLAND_FEE = "Obrigado pelo seu patrocínio, {0} moeda(s) de ouro foram cobradas como taxa bancária.";

        #endregion

        #region Balance Messages

        /// <summary>Message showing current bank balance (format string)</summary>
        public const string MSG_BALANCE_FORMAT = "Seu saldo atual é {0} moeda(s) de ouro.";

        #endregion

        #region Midland Bank Explanations

        /// <summary>Midland bank explanation variant 1</summary>
        public const string MSG_MIDLAND_BANK_VARIANT1 = "Sim senhor, somos um banco, você pode depositar, sacar ou verificar o saldo de sua conta.";

        /// <summary>Midland bank explanation variant 2</summary>
        public const string MSG_MIDLAND_BANK_VARIANT2 = "Sim, você está correto, este é um banco! Você gostaria de depositar, sacar ou verificar o saldo de sua conta?";

        #endregion

        #region Withdrawal Messages

        /// <summary>Message when gold is successfully withdrawn</summary>
        public const string MSG_WITHDRAW_SUCCESS = "Você sacou o valor de {0} moedas de ouro da sua conta.";

        /// <summary>Message when withdrawal amount exceeds limit</summary>
        public const string MSG_WITHDRAW_TOO_MUCH = "Você não pode sacar tanto ouro de uma vez!";

        /// <summary>Message when backpack is full during withdrawal</summary>
        public const string MSG_BACKPACK_FULL = "Sua mochila não pode carregar mais nada.";

        #endregion

        #region Check Messages

        /// <summary>Message when check is successfully created (format string with amount)</summary>
        public const string MSG_CHECK_CREATED = "Coloquei um cheque no valor de {0} moedas em sua caixa bancária.";

        /// <summary>Message when check amount is too small</summary>
        public const string MSG_CHECK_TOO_SMALL = "Não podemos criar cheques para uma quantia tão pequena de ouro!";

        /// <summary>Message when check amount is too large</summary>
        public const string MSG_CHECK_TOO_LARGE = "Nossas políticas nos impedem de criar cheques com valor tão alto!";

        /// <summary>Message when bankbox is full and cannot hold the check</summary>
        public const string MSG_BANKBOX_FULL = "Não há espaço suficiente em sua caixa bancária para o cheque!";

        #endregion

        #region Error Messages

        /// <summary>Message when player doesn't have enough gold</summary>
        public const string MSG_INSUFFICIENT_GOLD = "Ah, você está tentando me enganar? Você não tem tanto ouro!";

        #endregion
    }
}

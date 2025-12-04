namespace Server.Mobiles
{
    /// <summary>
    /// Centralized string constants for MidlandBanker NPC messages and labels.
    /// Extracted from MidlandBanker.cs to improve maintainability and enable localization.
    /// All messages are in Portuguese-Brazilian (PT-BR) as per codebase standards.
    /// </summary>
    public static class MidlandBankerStringConstants
    {
        #region Error Messages

        /// <summary>Message when wrong race tries to access services</summary>
        public const string MSG_WRONG_RACE = "Nós não servimos o seu tipo aqui.";

        /// <summary>Alternative message for wrong race/currency type</summary>
        public const string MSG_WRONG_TYPE = "Nós não lidamos com o seu tipo aqui.";

        /// <summary>Alternative message for wrong race in drag-drop</summary>
        public const string MSG_WRONG_KIND = "Nós não lidamos com o seu tipo aqui.";

        /// <summary>Message when invalid item is dropped</summary>
        public const string MSG_NOT_MONEY = "Essa coisa não é dinheiro.";

        #endregion

        #region Deposit Messages

        /// <summary>Message explaining how to deposit</summary>
        public const string MSG_DEPOSIT_INFO = "Para depositar, simplesmente me diga que deseja depositar e a quantia ou me dê o ouro. Você pode confiar em mim.";

        /// <summary>Message when deposit is successful</summary>
        public const string MSG_DEPOSIT_SUCCESS = "Você depositou {0} em sua conta.";

        /// <summary>Message when insufficient funds for deposit</summary>
        public const string MSG_INSUFFICIENT_FUNDS = "Você não parece ter ouro suficiente para depositar essa quantia.";

        #endregion

        #region Withdrawal Messages

        /// <summary>Message when withdrawal is successful</summary>
        public const string MSG_WITHDRAW_SUCCESS = "Você sacou {0} de sua conta.";

        #endregion

        #region Balance Messages

        /// <summary>Message showing current balance</summary>
        public const string MSG_BALANCE_INFO = "Seu saldo atual conosco é {0}";

        /// <summary>Message when no account exists</summary>
        public const string MSG_NO_ACCOUNT = "Você não parece ter uma conta conosco.";

        #endregion

        #region Midland Bank Explanations

        /// <summary>Midland bank explanation variant 1</summary>
        public const string MSG_MIDLAND_BANK_VARIANT1 = "Sim senhor, somos um banco, você pode depositar, sacar ou verificar o saldo de sua conta.";

        /// <summary>Midland bank explanation variant 2</summary>
        public const string MSG_MIDLAND_BANK_VARIANT2 = "Sim, você está correto, este é um banco! Você gostaria de depositar, sacar ou verificar o saldo de sua conta?";

        #endregion
    }
}

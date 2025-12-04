using System;

namespace Server.Items
{
    /// <summary>
    /// Centralized constants for BankCheck item properties and behavior.
    /// Extracted from BankCheck.cs to improve maintainability and reduce code duplication.
    /// </summary>
    public static class BankCheckConstants
    {
        #region Item Properties

        /// <summary>Item ID for bank check appearance</summary>
        public const int ITEM_ID = 0x14F0;

        /// <summary>Hue/color value for bank checks</summary>
        public const int ITEM_HUE = 0x34;

        /// <summary>Weight of bank check items</summary>
        public const double ITEM_WEIGHT = 1.0;

        /// <summary>Maximum gold per stack (UO gold stack limit)</summary>
        public const int MAX_GOLD_STACK = 60000;

        #endregion

        #region Message Properties

        /// <summary>Hue for single-click messages</summary>
        public const int MESSAGE_HUE = 0x3B2;

        /// <summary>Label number for "A bank check"</summary>
        public const int LABEL_NUMBER = 1041361;

        /// <summary>Property list number for "value: ~1_val~"</summary>
        public const int PROPERTY_NUMBER = 1060738;

        /// <summary>Deposit success message: "Gold was deposited in your account:"</summary>
        public const int DEPOSIT_MESSAGE = 1042672;

        /// <summary>Error message: "That must be in your bank box to use it."</summary>
        public const int ERROR_MESSAGE = 1047026;

        /// <summary>Cyan color for value property</summary>
        public const string COLOR_CYAN = "#8be4fc";

        #endregion
    }
}

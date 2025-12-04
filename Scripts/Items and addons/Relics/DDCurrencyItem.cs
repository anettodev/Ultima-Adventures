using System;

namespace Server.Items
{
    /// <summary>
    /// Abstract base class for all DD currency/treasure items that can be converted to gold.
    /// Provides common functionality while allowing subclasses to define specific conversion rates and properties.
    /// </summary>
    public abstract class DDCurrencyItem : Item
    {
        #region Abstract Properties

        /// <summary>Gets the conversion rate for this currency type (how many items = 1 gold)</summary>
        protected abstract int ConversionRate { get; }

        /// <summary>Gets whether this currency type supports change calculation (copper/silver)</summary>
        protected abstract bool SupportsChange { get; }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the weight of the item, adjusted for ML clients
        /// </summary>
        public override double DefaultWeight
        {
            get { return Core.ML ? (DDCurrencyConstants.BASE_WEIGHT / DDCurrencyConstants.ML_WEIGHT_DIVISOR) : DDCurrencyConstants.BASE_WEIGHT; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Deserializes a currency item from save data
        /// </summary>
        public DDCurrencyItem(Serial serial) : base(serial)
        {
        }

        /// <summary>
        /// Creates a currency item with the specified amount and item ID
        /// </summary>
        protected DDCurrencyItem(int itemId, int amount) : base(itemId)
        {
            Name = GetItemName();
            Hue = GetItemHue();
            Stackable = true;
            Amount = amount;
            Light = LightType.Circle150;
        }

        /// <summary>
        /// Gets the item ID for this currency type (implemented by derived classes)
        /// </summary>
        protected abstract int GetItemId();

        /// <summary>
        /// Gets the item name for this currency type (implemented by derived classes)
        /// </summary>
        protected abstract string GetItemName();

        /// <summary>
        /// Gets the item hue for this currency type (implemented by derived classes)
        /// </summary>
        protected abstract int GetItemHue();

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes the currency item to save data
        /// </summary>
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        /// <summary>
        /// Deserializes the currency item from save data
        /// </summary>
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            // Reinitialize properties that aren't serialized
            Name = GetItemName();
            Hue = GetItemHue();
            Light = LightType.Circle150;
        }

        #endregion

        #region Audio

        /// <summary>
        /// Gets the drop sound based on stack size
        /// </summary>
        public override int GetDropSound()
        {
            if (Amount <= 1)
                return DDCurrencyConstants.DROP_SOUND_SMALL;
            else if (Amount <= 5)
                return DDCurrencyConstants.DROP_SOUND_MEDIUM;
            else
                return DDCurrencyConstants.DROP_SOUND_LARGE;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles amount changes (currently no special logic needed)
        /// </summary>
        protected override void OnAmountChange(int oldValue)
        {
            // Base implementation - subclasses can override if needed
        }

        /// <summary>
        /// Handles double-clicking the item to convert it to gold in the bank box
        /// </summary>
        public override void OnDoubleClick(Mobile from)
        {
            BankBox box = from.FindBankNoCreate();

            if (box != null && IsChildOf(box))
            {
                Delete();

                if (SupportsChange)
                {
                    // Complex conversion with change (copper/silver)
                    int goldAmount = (int)Math.Floor((decimal)(Amount / ConversionRate));
                    int changeAmount = Amount - (goldAmount * ConversionRate);

                    if ((goldAmount > 0) && (changeAmount > 0))
                    {
                        from.AddToBackpack(new Gold(goldAmount));
                        AddChangeToBackpack(from, changeAmount);
                    }
                    else if (goldAmount > 0)
                    {
                        from.AddToBackpack(new Gold(goldAmount));
                    }

                    if (changeAmount > 0)
                    {
                        AddChangeToBackpack(from, changeAmount);
                    }
                }
                else
                {
                    // Simple conversion without change (jewels, gemstones, etc.)
                    int goldAmount = Amount * ConversionRate;
                    from.AddToBackpack(new Gold(goldAmount));
                }

                // Success message - "Gold was deposited in your account: [amount]"
                int totalDeposited = SupportsChange ?
                    (int)Math.Floor((decimal)(Amount / ConversionRate)) :
                    Amount * ConversionRate;
                from.SendMessage(String.Format(DDCurrencyStringConstants.MSG_CONVERSION_SUCCESS, totalDeposited.ToString()));
            }
            else
            {
                // Error message
                from.SendLocalizedMessage(DDCurrencyConstants.ERROR_NOT_IN_BANK_MESSAGE);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Adds change back to the player's backpack (implemented by subclasses)
        /// </summary>
        protected abstract void AddChangeToBackpack(Mobile from, int amount);

        #endregion
    }
}

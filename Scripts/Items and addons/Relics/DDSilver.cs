using System;

namespace Server.Items
{
    /// <summary>
    /// Silver coins that can be converted to gold (5 silver = 1 gold, with change)
    /// </summary>
    public class DDSilver : DDCurrencyItem
    {
        #region Abstract Property Overrides

        /// <summary>Silver conversion rate (5 silver = 1 gold)</summary>
        protected override int ConversionRate { get { return DDCurrencyConstants.SILVER_CONVERSION_RATE; } }

        /// <summary>Item ID for silver coins</summary>
        protected override int GetItemId() { return DDCurrencyConstants.COIN_ITEM_ID; }

        /// <summary>Localized name for silver coins</summary>
        protected override string GetItemName() { return DDCurrencyStringConstants.NAME_SILVER_COINS; }

        /// <summary>Hue for silver coins</summary>
        protected override int GetItemHue() { return DDCurrencyConstants.SILVER_HUE; }

        /// <summary>Silver supports change calculation</summary>
        protected override bool SupportsChange { get { return true; } }

        #endregion

        #region Constructors

        public DDSilver(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public DDSilver() : this(1)
        {
        }

        [Constructable]
        public DDSilver(int amountFrom, int amountTo) : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructable]
        public DDSilver(int amount) : base(DDCurrencyConstants.COIN_ITEM_ID, amount)
        {
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes the silver coins to save data
        /// </summary>
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        /// <summary>
        /// Deserializes the silver coins from save data
        /// </summary>
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Adds silver change back to player's backpack
        /// </summary>
        protected override void AddChangeToBackpack(Mobile from, int amount)
        {
            from.AddToBackpack(new DDSilver(amount));
        }

        #endregion
    }
}

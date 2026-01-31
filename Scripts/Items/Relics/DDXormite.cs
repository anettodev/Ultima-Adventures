using System;

namespace Server.Items
{
    /// <summary>
    /// Xormite coins that can be converted to gold (1 xormite = 3 gold)
    /// </summary>
    public class DDXormite : DDCurrencyItem
    {
        #region Abstract Property Overrides

        /// <summary>Xormite conversion rate (1 xormite = 3 gold)</summary>
        protected override int ConversionRate { get { return DDCurrencyConstants.XORMITE_CONVERSION_RATE; } }

        /// <summary>Item ID for xormite coins</summary>
        protected override int GetItemId() { return DDCurrencyConstants.COIN_ITEM_ID; }

        /// <summary>Localized name for xormite coins</summary>
        protected override string GetItemName() { return DDCurrencyStringConstants.NAME_XORMITE_COINS; }

        /// <summary>Hue for xormite coins</summary>
        protected override int GetItemHue() { return DDCurrencyConstants.XORMITE_HUE; }

        /// <summary>Xormite does not support change calculation</summary>
        protected override bool SupportsChange { get { return false; } }

        #endregion

        #region Constructors

        public DDXormite(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public DDXormite() : this(1)
        {
        }

        [Constructable]
        public DDXormite(int amountFrom, int amountTo) : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructable]
        public DDXormite(int amount) : base(DDCurrencyConstants.COIN_ITEM_ID, amount)
        {
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes the xormite coins to save data
        /// </summary>
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        /// <summary>
        /// Deserializes the xormite coins from save data
        /// </summary>
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Xormite doesn't support change, so this method should never be called
        /// </summary>
        protected override void AddChangeToBackpack(Mobile from, int amount)
        {
            // Xormite doesn't have change - this should never be called
            // But providing implementation for completeness
        }

        #endregion
    }
}

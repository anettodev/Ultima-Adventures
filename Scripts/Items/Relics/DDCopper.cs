using System;

namespace Server.Items
{
    /// <summary>
    /// Copper coins that can be converted to gold (10 copper = 1 gold, with change)
    /// </summary>
    public class DDCopper : DDCurrencyItem
    {
        #region Abstract Property Overrides

        /// <summary>Copper conversion rate (10 copper = 1 gold)</summary>
        protected override int ConversionRate { get { return DDCurrencyConstants.COPPER_CONVERSION_RATE; } }

        /// <summary>Item ID for copper coins</summary>
        protected override int GetItemId() { return DDCurrencyConstants.COIN_ITEM_ID; }

        /// <summary>Localized name for copper coins</summary>
        protected override string GetItemName() { return DDCurrencyStringConstants.NAME_COPPER_COINS; }

        /// <summary>Hue for copper coins</summary>
        protected override int GetItemHue() { return DDCurrencyConstants.COPPER_HUE; }

        /// <summary>Copper supports change calculation</summary>
        protected override bool SupportsChange { get { return true; } }

        #endregion

        #region Constructors

        public DDCopper(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public DDCopper() : this(1)
        {
        }

        [Constructable]
        public DDCopper(int amountFrom, int amountTo) : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructable]
        public DDCopper(int amount) : base(DDCurrencyConstants.COIN_ITEM_ID, amount)
        {
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes the copper coins to save data
        /// </summary>
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        /// <summary>
        /// Deserializes the copper coins from save data
        /// </summary>
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Adds copper change back to player's backpack
        /// </summary>
        protected override void AddChangeToBackpack(Mobile from, int amount)
        {
            from.AddToBackpack(new DDCopper(amount));
        }

        #endregion
    }
}

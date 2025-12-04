using System;

namespace Server.Items
{
    /// <summary>
    /// Jewels that can be converted to gold (1 jewel = 2 gold)
    /// </summary>
    public class DDJewels : DDCurrencyItem
    {
        #region Abstract Property Overrides

        /// <summary>Jewel conversion rate (1 jewel = 2 gold)</summary>
        protected override int ConversionRate { get { return DDCurrencyConstants.JEWEL_CONVERSION_RATE; } }

        /// <summary>Item ID for jewels</summary>
        protected override int GetItemId() { return DDCurrencyConstants.COIN_ITEM_ID; }

        /// <summary>Localized name for jewels</summary>
        protected override string GetItemName() { return DDCurrencyStringConstants.NAME_JEWELS; }

        /// <summary>Jewels use default hue (no special hue)</summary>
        protected override int GetItemHue() { return 0; }

        /// <summary>Jewels do not support change calculation</summary>
        protected override bool SupportsChange { get { return false; } }

        #endregion

        #region Constructors

        public DDJewels(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public DDJewels() : this(1)
        {
        }

        [Constructable]
        public DDJewels(int amountFrom, int amountTo) : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructable]
        public DDJewels(int amount) : base(DDCurrencyConstants.COIN_ITEM_ID, amount)
        {
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes the jewels to save data
        /// </summary>
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        /// <summary>
        /// Deserializes the jewels from save data
        /// </summary>
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Jewels don't support change, so this method should never be called
        /// </summary>
        protected override void AddChangeToBackpack(Mobile from, int amount)
        {
            // Jewels don't have change - this should never be called
            // But providing implementation for completeness
        }

        #endregion
    }
}

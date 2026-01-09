using System;

namespace Server.Items
{
    /// <summary>
    /// Gemstones that can be converted to gold (1 gemstone = 2 gold)
    /// </summary>
    public class DDGemstones : DDCurrencyItem
    {
        #region Abstract Property Overrides

        /// <summary>Gemstone conversion rate (1 gemstone = 2 gold)</summary>
        protected override int ConversionRate { get { return DDCurrencyConstants.GEMSTONE_CONVERSION_RATE; } }

        /// <summary>Item ID for gemstones</summary>
        protected override int GetItemId() { return DDCurrencyConstants.GEMSTONE_ITEM_ID; }

        /// <summary>Localized name for gemstones</summary>
        protected override string GetItemName() { return DDCurrencyStringConstants.NAME_GEMSTONES; }

        /// <summary>Gemstones use default hue (no special hue)</summary>
        protected override int GetItemHue() { return 0; }

        /// <summary>Gemstones do not support change calculation</summary>
        protected override bool SupportsChange { get { return false; } }

        #endregion

        #region Constructors

        public DDGemstones(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public DDGemstones() : this(1)
        {
        }

        [Constructable]
        public DDGemstones(int amountFrom, int amountTo) : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructable]
        public DDGemstones(int amount) : base(DDCurrencyConstants.GEMSTONE_ITEM_ID, amount)
        {
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes the gemstones to save data
        /// </summary>
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        /// <summary>
        /// Deserializes the gemstones from save data
        /// </summary>
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gemstones don't support change, so this method should never be called
        /// </summary>
        protected override void AddChangeToBackpack(Mobile from, int amount)
        {
            // Gemstones don't have change - this should never be called
            // But providing implementation for completeness
        }

        #endregion
    }
}

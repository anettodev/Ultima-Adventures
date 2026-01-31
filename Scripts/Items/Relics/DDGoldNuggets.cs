using System;

namespace Server.Items
{
    /// <summary>
    /// Gold nuggets that can be converted to gold (1 nugget = 1 gold)
    /// </summary>
    public class DDGoldNuggets : DDCurrencyItem
    {
        #region Abstract Property Overrides

        /// <summary>Gold nugget conversion rate (1 nugget = 1 gold)</summary>
        protected override int ConversionRate { get { return DDCurrencyConstants.GOLD_NUGGET_CONVERSION_RATE; } }

        /// <summary>Item ID for gold nuggets</summary>
        protected override int GetItemId() { return DDCurrencyConstants.GOLD_NUGGET_ITEM_ID; }

        /// <summary>Localized name for gold nuggets</summary>
        protected override string GetItemName() { return DDCurrencyStringConstants.NAME_GOLD_NUGGETS; }

        /// <summary>Gold nuggets use default hue (no special hue)</summary>
        protected override int GetItemHue() { return 0; }

        /// <summary>Gold nuggets do not support change calculation</summary>
        protected override bool SupportsChange { get { return false; } }

        #endregion

        #region Constructors

        public DDGoldNuggets(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public DDGoldNuggets() : this(1)
        {
        }

        [Constructable]
        public DDGoldNuggets(int amountFrom, int amountTo) : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructable]
        public DDGoldNuggets(int amount) : base(DDCurrencyConstants.GOLD_NUGGET_ITEM_ID, amount)
        {
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes the gold nuggets to save data
        /// </summary>
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        /// <summary>
        /// Deserializes the gold nuggets from save data
        /// </summary>
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gold nuggets don't support change, so this method should never be called
        /// </summary>
        protected override void AddChangeToBackpack(Mobile from, int amount)
        {
            // Gold nuggets don't have change - this should never be called
            // But providing implementation for completeness
        }

        #endregion
    }
}

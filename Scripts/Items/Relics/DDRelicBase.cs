using System;
using Server;

namespace Server.Items
{
    /// <summary>
    /// Base class for all DDRelic items.
    /// Provides common functionality for gold value, serialization, and identification.
    /// </summary>
    public abstract class DDRelicBase : Item
    {
        #region Fields

        /// <summary>Gold value of the relic</summary>
        public int RelicGoldValue;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the relic's gold value
        /// </summary>
        [CommandProperty(AccessLevel.Owner)]
        public int Relic_Value
        {
            get { return RelicGoldValue; }
            set { RelicGoldValue = value; InvalidateProperties(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor for relics
        /// </summary>
        /// <param name="itemID">Base item ID</param>
        protected DDRelicBase(int itemID) : base(itemID)
        {
            if (RelicGoldValue < 1)
            {
                RelicGoldValue = Server.Misc.RelicItems.RelicValue();
            }
        }

        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public DDRelicBase(Serial serial) : base(serial)
        {
        }

        #endregion

        #region Core Logic

        /// <summary>
        /// Default double-click behavior for relics
        /// </summary>
        public override void OnDoubleClick(Mobile from)
        {
            from.SendMessage(RelicStringConstants.MSG_IDENTIFY_VALUE);
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes the relic
        /// </summary>
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(RelicConstants.SERIALIZATION_VERSION);
            writer.Write(RelicGoldValue);
        }

        /// <summary>
        /// Deserializes the relic
        /// </summary>
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            RelicGoldValue = reader.ReadInt();
        }

        #endregion
    }
}


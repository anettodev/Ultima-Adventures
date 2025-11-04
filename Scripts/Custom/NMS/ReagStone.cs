using System;

namespace Server.Items
{
    public class ReagStone : Item
    {
        [Constructable]
        public ReagStone()
            : base(0xED4)
        {
            this.Movable = false;
            this.Hue = 0x2D1;
        }

        public ReagStone(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName
        {
            get
            {
                return "a reagent stone";
            }
        }
        public override void OnDoubleClick(Mobile from)
        {
            BagOfReagents regBag = new BagOfReagents();

            if (!from.AddToBackpack(regBag))
                regBag.Delete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
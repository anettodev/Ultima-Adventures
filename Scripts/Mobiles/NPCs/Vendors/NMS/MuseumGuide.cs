using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Misc;
using Server.Mobiles;
using System.Collections;
using Server.Gumps;
using System.Text;
namespace Server.Mobiles
{
    public class MuseumGuide : BaseVendor
    {
        private List<SBInfo> m_SBInfos = new List<SBInfo>();
        protected override List<SBInfo> SBInfos { get { return m_SBInfos; } }

        public override NpcGuild NpcGuild { get { return NpcGuild.LibrariansGuild; } }

        // Disable training for Museum Guides
        public override bool CanTeach { get { return false; } }

        protected DateTime m_LastGreeting;
        protected static readonly TimeSpan GREETING_COOLDOWN = TimeSpan.FromSeconds(60);

        private static readonly string[] GREETINGS = new string[]
        {
            "Bem-vindo ao museu! Posso ajudá-lo a explorar nossas exposições.",
            "Olá, aventureiro! Venha conhecer as maravilhas que temos aqui.",
            "Interessado em artefatos? Fale comigo para saber mais sobre o museu."
        };

        [Constructable]
        public MuseumGuide() : base("O guia do Museu")
        {
            Job = JobFragment.scholar;
            Karma = Utility.RandomMinMax(15, -45);

            // Skills at maximum (120.0)
            SetSkill(SkillName.Inscribe, 120.0, 120.0);
            SetSkill(SkillName.MagicResist, 120.0, 120.0);
            SetSkill(SkillName.Wrestling, 120.0, 120.0);
            SetSkill(SkillName.ItemID, 120.0, 120.0);
            SetSkill(SkillName.ArmsLore, 120.0, 120.0);
            SetSkill(SkillName.Blacksmith, 120.0, 120.0);
            SetSkill(SkillName.Mining, 120.0, 120.0);

            SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
            Blessed = true;

            // Random gender
            Female = Utility.RandomBool();

            // Random name based on gender
            if (Female)
            {
                Body = 401;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 400;
                Name = NameList.RandomName("male");
            }

            // Random appearance
            Hue = Server.Misc.RandomThings.GetRandomSkinColor();
            Utility.AssignRandomHair(this);
            
            // Random beard for males only
            if (!Female)
            {
                Utility.AssignRandomFacialHair(this);
            }
            else
            {
                FacialHairItemID = 0;
            }
        }

        public MuseumGuide(Serial serial) : base(serial)
        {
        }

        public override void InitSBInfo()
        {
            if (NpcGuild == NpcGuild.MinersGuild) 
            { 
                m_SBInfos.Add(new NMS_SBMinerBSMuseumGuide()); 
            }
            else if (NpcGuild == NpcGuild.RangersGuild) 
            { 
                m_SBInfos.Add(new NMS_SBLumberMuseumGuide()); 
            }
            else if (NpcGuild == NpcGuild.TailorsGuild) 
            { 
                m_SBInfos.Add(new NMS_SBTailorLeatherMuseumGuide()); 
            }
            else 
            { 
                m_SBInfos.Add(new NMS_SBMuseumGuide()); 
            }
            //m_SBInfos.Add(new SBBuyArtifacts());
        }

        public override VendorShoeType ShoeType
        {
            get { return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; }
        }

        public override void InitOutfit()
        {
            base.InitOutfit();
            
            // Scholar-themed robe with appropriate colors
            int clothHue = Utility.RandomList(
                0x0,      // Default
                0x3B2,    // Brown
                0x47E,    // Gray
                0x59C,    // Blue
                0x835,    // Dark brown
                0x96C     // Dark blue
            );
            
            AddItem(new Server.Items.Robe(clothHue));
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            base.OnMovement(m, oldLocation);

            if (m is PlayerMobile && m.Alive && !m.Hidden && 
                m.InRange(this, 4) && DateTime.Now >= m_LastGreeting + GREETING_COOLDOWN)
            {
                Say(GREETINGS[Utility.Random(GREETINGS.Length)]);
                m_LastGreeting = DateTime.Now;
                this.Move(GetDirectionTo(m.Location));
            }
        }

        /*public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (dropped is Gold)
            {
                int halfDroppedCoins = dropped.Amount / 2;
                string sMessage = "";
                this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
            }

            return base.OnDragDrop(from, dropped);
        }*/

        ///////////////////////////////////////////////////////////////////////////
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new SpeechGumpEntry(from, this));
        }

        public class SpeechGumpEntry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;

            public SpeechGumpEntry(Mobile from, Mobile giver) : base(6146, 3)
            {
                m_Mobile = from;
                m_Giver = giver;
            }

            public override void OnClick()
            {
                if (!(m_Mobile is PlayerMobile))
                    return;

                PlayerMobile mobile = (PlayerMobile)m_Mobile;
                {
                    if (!mobile.HasGump(typeof(SpeechGump)))
                    {
                        mobile.SendGump(new SpeechGump("* AVISO IMPORTANTE *", SpeechFunctions.SpeechText(m_Giver.Name, m_Mobile.Name, "MuseumGuide")));

                    }
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////

        // Repair option removed - not fully implemented yet
        // Can be re-added when repair functionality is complete
        /*
        private class FixEntry : ContextMenuEntry
        {
            private MuseumGuide m_Sage;
            private Mobile m_From;

            public FixEntry(MuseumGuide Sage, Mobile from) : base(1044077, 1) // "Repair" cliloc
            {
                m_Sage = Sage;
                m_From = from;
            }

            public override void OnClick()
            {
                m_Sage.BeginRepair(m_From);
            }
        }
        */

        public override void AddCustomContextEntries(Mobile from, List<ContextMenuEntry> list)
        {
            // Repair option removed - not fully implemented yet
            // if (from.Alive && !from.Blessed)
            // {
            //     list.Add(new FixEntry(this, from));
            // }

            base.AddCustomContextEntries(from, list);
        }

        public void BeginRepair(Mobile from)
        {
            if (Deleted || !from.Alive)
                return;

            SayTo(from, "Selecione o item que deseja reparar.");
            from.Target = new RepairTarget(this);
        }

        private class RepairTarget : Target
        {
            private MuseumGuide m_Guide;

            public RepairTarget(MuseumGuide guide) : base(2, false, TargetFlags.None)
            {
                m_Guide = guide;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Item)
                {
                    Item item = (Item)targeted;
                    
                    if (!item.IsChildOf(from.Backpack))
                    {
                        m_Guide.SayTo(from, "O item deve estar em sua mochila para ser reparado.");
                        return;
                    }

                    // Simple repair message - can be expanded later
                    m_Guide.SayTo(from, "Desculpe, o serviço de reparo ainda não está disponível. Em breve poderemos ajudá-lo com isso!");
                }
                else
                {
                    m_Guide.SayTo(from, "Isso não pode ser reparado.");
                }
            }
        }

        
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
            
            writer.Write(m_LastGreeting);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            
            if (version >= 1)
            {
                m_LastGreeting = reader.ReadDateTime();
            }
            else
            {
                m_LastGreeting = DateTime.MinValue;
            }
        }
    }

    public class MinerBSMuseumGuide : MuseumGuide
    {
        private static readonly string[] MINER_GREETINGS = new string[]
        {
            "Bem-vindo ao museu de mineração e ferragens! Explore nossas exposições de minérios e ferramentas.",
            "Olá! Interessado em mineração ou ferragens? Também compro e vendo lingotes!",
            "Venha conhecer a arte da mineração e da forja! Fale comigo para saber mais sobre nossas exposições e lingotes."
        };

        [Constructable]
        public MinerBSMuseumGuide() : base()
        {
            Title = "o guia do Museu";
            
            // Mining/Smithing themed skills at max
            SetSkill(SkillName.Mining, 120.0, 120.0);
            SetSkill(SkillName.Blacksmith, 120.0, 120.0);
            SetSkill(SkillName.Tinkering, 120.0, 120.0);
            SetSkill(SkillName.ItemID, 120.0, 120.0);
        }

        public MinerBSMuseumGuide(Serial serial) : base(serial)
        {
        }

        public override NpcGuild NpcGuild { get { return NpcGuild.MinersGuild; } }

        public override void InitOutfit()
        {
            base.InitOutfit();
            
            // Remove robe, add work clothes
            Item robe = FindItemOnLayer(Layer.OuterTorso);
            if (robe != null) robe.Delete();
            
            // Remove base shirt if it exists (from base.InitOutfit)
            Item shirt = FindItemOnLayer(Layer.InnerTorso);
            if (shirt != null) shirt.Delete();
            
            // Work clothes (earth tones)
            int workHue = Utility.RandomList(0x3B2, 0x47E, 0x835, 0x96C);
            
            // Simple work shirt
            AddItem(new Shirt(workHue));
            AddItem(new LongPants(0x3B2));
            
            // Work boots
            AddItem(new Boots(0x835));
            
            // Work apron (using BodySash for waist layer)
            AddItem(new BodySash(workHue));
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            base.OnMovement(m, oldLocation);

            if (m is PlayerMobile && m.Alive && !m.Hidden && 
                m.InRange(this, 4) && DateTime.Now >= m_LastGreeting + GREETING_COOLDOWN)
            {
                Say(MINER_GREETINGS[Utility.Random(MINER_GREETINGS.Length)]);
                m_LastGreeting = DateTime.Now;
                this.Move(GetDirectionTo(m.Location));
            }
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

    public class LumberMuseumGuide : MuseumGuide
    {
        private static readonly string[] LUMBER_GREETINGS = new string[]
        {
            "Bem-vindo ao museu de madeiras! Conheça as diferentes árvores e seus usos.",
            "Olá! Interessado em carpintaria? Posso mostrar nossas exposições de madeiras.",
            "Venha explorar a arte da madeira! Fale comigo para saber mais sobre nossas coleções."
        };

        [Constructable]
        public LumberMuseumGuide() : base()
        {
            Title = "o guia do Museu";
            
            // Lumber/Carpentry themed skills at max
            SetSkill(SkillName.Lumberjacking, 120.0, 120.0);
            SetSkill(SkillName.Carpentry, 120.0, 120.0);
            SetSkill(SkillName.Fletching, 120.0, 120.0);
            SetSkill(SkillName.ItemID, 120.0, 120.0);
        }

        public LumberMuseumGuide(Serial serial) : base(serial)
        {
        }

        public override NpcGuild NpcGuild { get { return NpcGuild.RangersGuild; } }

        public override void InitOutfit()
        {
            base.InitOutfit();
            
            // Remove robe, add nature-themed clothes
            Item robe = FindItemOnLayer(Layer.OuterTorso);
            if (robe != null) robe.Delete();
            
            // Nature colors (greens, browns)
            int natureHue = Utility.RandomList(0x59C, 0x835, 0x1C9, 0x3B2);
            
            AddItem(new Shirt(natureHue));
            AddItem(new LongPants(0x835));
            AddItem(new Sandals(0x59C));
            
            // Optional: Add a simple cloak or sash
            AddItem(new Cloak(0x1C9));
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            base.OnMovement(m, oldLocation);

            if (m is PlayerMobile && m.Alive && !m.Hidden && 
                m.InRange(this, 4) && DateTime.Now >= m_LastGreeting + GREETING_COOLDOWN)
            {
                Say(LUMBER_GREETINGS[Utility.Random(LUMBER_GREETINGS.Length)]);
                m_LastGreeting = DateTime.Now;
                this.Move(GetDirectionTo(m.Location));
            }
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

    public class TailorLeatherMuseumGuide : MuseumGuide
    {
        private static readonly string[] TAILOR_GREETINGS = new string[]
        {
            "Bem-vindo ao museu de tecidos e couros! Explore nossas exposições de roupas e armaduras de couro.",
            "Olá! Interessado em costura ou couros? Posso mostrar nossas coleções de tecidos e peles.",
            "Venha conhecer a arte da costura e do couro! Fale comigo para saber mais sobre nossas exposições."
        };

        [Constructable]
        public TailorLeatherMuseumGuide() : base()
        {
            Title = "o guia do Museu";
            
            // Tailoring/Leather themed skills at max
            SetSkill(SkillName.Tailoring, 120.0, 120.0);
            SetSkill(SkillName.ItemID, 120.0, 120.0);
            SetSkill(SkillName.ArmsLore, 120.0, 120.0);
        }

        public TailorLeatherMuseumGuide(Serial serial) : base(serial)
        {
        }

        public override NpcGuild NpcGuild { get { return NpcGuild.TailorsGuild; } }

        public override void InitOutfit()
        {
            base.InitOutfit();
            
            // Remove robe, add tailor/leather themed clothes
            Item robe = FindItemOnLayer(Layer.OuterTorso);
            if (robe != null) robe.Delete();
            
            // Fabric/leather colors (soft tones, elegant colors)
            int fabricHue = Utility.RandomList(0x59C, 0x835, 0x1C9, 0x3B2, 0x47E, 0x96C);
            
            // Elegant tunic (tailor style)
            AddItem(new Shirt(fabricHue));
            AddItem(new LongPants(0x835)); // Brown pants (leather tone)
            
            // Simple shoes
            AddItem(new Sandals(0x0));
            
            // Optional: Add a sash or belt (tailor accessory)
            AddItem(new BodySash(0x59C));
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            base.OnMovement(m, oldLocation);

            if (m is PlayerMobile && m.Alive && !m.Hidden && 
                m.InRange(this, 4) && DateTime.Now >= m_LastGreeting + GREETING_COOLDOWN)
            {
                Say(TAILOR_GREETINGS[Utility.Random(TAILOR_GREETINGS.Length)]);
                m_LastGreeting = DateTime.Now;
                this.Move(GetDirectionTo(m.Location));
            }
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
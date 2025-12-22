using System;
using Server;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Spells;

namespace Server.Mobiles
{
    public enum GuardType
    {
        Pikeman,
        Swordsman,
        Archer,
        Melee,
        Cavalry,
        Wizard,
        Medic
    }

    public enum GuardPatent
    {
        Soldier=0,
        Sargent=1,
        lieutenant=2,
        Captain=3,
        Colonel=4,
        General=5
    }

    [CorpseName("corpo de mercenario")]
    public class Guard : BasePerson
    {
        private GuardType m_Type;
        private GuardPatent m_Patent;

        [CommandProperty(AccessLevel.GameMaster)]
        public GuardType Type { get { return m_Type; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public GuardPatent Patent { get { return m_Patent; } }

        [Constructable]
        public Guard(GuardType type, GuardPatent patent) : this(type, patent, AIType.AI_Melee)
        {

        }

        [Constructable]
        public Guard(GuardType type, GuardPatent patent, AIType ai)
            : base(ai, FightMode.Closest, 20, 1, 0.2, 0.4)
        {
            setGuardsAttributes(type, patent);

            SetStatsAndSkills(type, patent);

            SetDamage(7, 13);
            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 30, 45);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 50, 60);

            HairItemID = Utility.RandomList(0x203B, 0x203C, 0x203D, 0x2048);
            HairHue = FacialHairHue = Utility.RandomHairHue();

            Backpack pack = new Backpack();
            pack.AddItem(new Bandage(Utility.RandomMinMax(100, 200)));

            this.AddItem(pack);

            AddEquipment(type, patent);

            if (type == GuardType.Cavalry)
            {
                Horse horse = new Horse();
                horse.Body = 0xE4;
                horse.Controlled = true;
                horse.ControlMaster = this;
                horse.ControlOrder = OrderType.Come;
                horse.RawName = "Cavalo";
                horse.Hue = 0;
                horse.ItemID = 16033;
                horse.Rider = this;

                horse.RawStr += Utility.RandomMinMax(80, 100);
                horse.RawDex += Utility.RandomMinMax(90, 120);
                horse.RawInt += Utility.RandomMinMax(25, 40);

                horse.SetSkill(SkillName.Wrestling, horse.Skills.Wrestling.Value + Utility.RandomMinMax(50, 70));
                horse.SetSkill(SkillName.Tactics, horse.Skills.Tactics.Value + Utility.RandomMinMax(50, 70));
                horse.SetSkill(SkillName.MagicResist, horse.Skills.MagicResist.Value + Utility.RandomMinMax(35, 60));

            }
        }

        public Guard(Serial serial)
            : base(serial)
        {
        }

        #region Stats
        private void setGuardsAttributes(GuardType type, GuardPatent Patent)
        {
            // Gender
            if (0.50 >= Utility.RandomDouble())
            {
                Name = NameList.RandomName("female");
                Female = true;
                Body = 0x191;
            }
            else
            {
                Name = NameList.RandomName("male");
                Body = 0x190;
                FacialHairItemID = Utility.RandomList(0x203E, 0x203F, 0x2040, 0x2041, 0x204B, 0x204C, 0x204D);
            }

            // Set the Type AI Attrs
            m_Type = type;
            m_Patent = Patent;
            ChangeAIType(AIType.AI_Melee);
            if (m_Type == GuardType.Wizard)
            {
                ChangeAIType(AIType.AI_Mage);
            }
            else if (m_Type == GuardType.Archer)
            {
                ChangeAIType(AIType.AI_Archer);
            }
            else if (m_Type == GuardType.Medic)
            {
                ChangeAIType(AIType.AI_Healer);
            }

            Title = GetTitle(type);
            Hue = Utility.RandomSkinHue();
            Karma = Utility.RandomMinMax(2000, 10000);
        }

        private string GetTitle(GuardType type)
        {
            string title;

            switch (type)
            {
                default:
                case GuardType.Archer: title = "o Arqueiro"; break;
                case GuardType.Cavalry: title = "o Cavaleiro"; break;
                case GuardType.Pikeman: title = "o Hoplita"; break;
                case GuardType.Swordsman: title = "o Espadachim"; break;
                case GuardType.Wizard: title = "o Mago"; break;
                case GuardType.Medic: title = "o Curandeiro"; break;
            }

            return title;
        }

        private void SetStatsAndSkills(GuardType type, GuardPatent patent)
        {
            switch (patent) 
            {
                default:
                case GuardPatent.Soldier: 
                    {
                        SetStr(70, 80);
                        SetDex(70, 80);
                        SetInt(70, 80);

                        SetHits(100, 120);
                        
                        SetSkill(SkillName.Tactics, 70, 80);
                        SetSkill(SkillName.Healing, 70, 80);
                        SetSkill(SkillName.Anatomy, 70, 80);
                        SetSkill(SkillName.Wrestling, 70, 80);
                        SetSkill(SkillName.Swords, 70, 80);
                        SetSkill(SkillName.Fencing, 70, 80);
                        SetSkill(SkillName.Parry, 50, 70);
                        SetSkill(SkillName.Macing, 70, 80);
                        SetSkill(SkillName.MagicResist, 50, 70);

                        if (type == GuardType.Wizard)
                        {
                            SetMana(70, 90);
                            SetSkill(SkillName.Magery, 70, 80);
                            SetSkill(SkillName.EvalInt, 70, 80);
                            SetSkill(SkillName.Focus, 50, 70);
                            SetSkill(SkillName.Meditation, 70, 80);
                        }
                        else if (type == GuardType.Archer)
                        {
                            SetSkill(SkillName.Archery, 70, 80);
                            this.RangeFight = 6;
                        }
                    } break;
                case GuardPatent.Sargent:
                    {
                        SetStr(80, 90);
                        SetDex(80, 90);
                        SetInt(80, 90);

                        SetHits(120, 140);

                        SetSkill(SkillName.Tactics, 80, 90);
                        SetSkill(SkillName.Healing, 80, 90);
                        SetSkill(SkillName.Anatomy, 80, 90);
                        SetSkill(SkillName.Wrestling, 80, 90);
                        SetSkill(SkillName.Swords, 80, 90);
                        SetSkill(SkillName.Fencing, 80, 90);
                        SetSkill(SkillName.Parry, 70, 80);
                        SetSkill(SkillName.Macing, 80, 90);
                        SetSkill(SkillName.MagicResist, 65, 75);

                        if (type == GuardType.Wizard)
                        {
                            SetMana(80, 100);
                            SetSkill(SkillName.Magery, 80, 90);
                            SetSkill(SkillName.EvalInt, 80, 90);
                            SetSkill(SkillName.Focus, 60, 80);
                            SetSkill(SkillName.Meditation, 80, 90);
                        }
                        else if (type == GuardType.Archer)
                        {
                            SetSkill(SkillName.Archery, 80, 90);
                            this.RangeFight = 6;
                        }
                    } break;
                case GuardPatent.lieutenant:
                    {
                        SetStr(90, 100);
                        SetDex(90, 100);
                        SetInt(90, 100);

                        SetHits(150, 170);

                        SetSkill(SkillName.Tactics, 90, 100);
                        SetSkill(SkillName.Healing, 90, 100);
                        SetSkill(SkillName.Anatomy, 90, 100);
                        SetSkill(SkillName.Wrestling, 90, 100);
                        SetSkill(SkillName.Swords, 90, 100);
                        SetSkill(SkillName.Fencing, 90, 100);
                        SetSkill(SkillName.Parry, 80, 90);
                        SetSkill(SkillName.Macing, 90, 100);
                        SetSkill(SkillName.MagicResist, 75, 85);

                        if (type == GuardType.Wizard)
                        {
                            SetMana(90, 110);
                            SetSkill(SkillName.Magery, 80, 90);
                            SetSkill(SkillName.EvalInt, 80, 90);
                            SetSkill(SkillName.Focus, 60, 80);
                            SetSkill(SkillName.Meditation, 80, 90);
                        }
                        else if (type == GuardType.Archer)
                        {
                            SetSkill(SkillName.Archery, 80, 90);
                            this.RangeFight = 6;
                        }
                    }
                    break;
                case GuardPatent.Captain:
                    {
                        SetStr(100, 110);
                        SetDex(100, 110);
                        SetInt(100, 110);

                        SetHits(170, 190);

                        SetSkill(SkillName.Tactics, 95, 110);
                        SetSkill(SkillName.Healing, 95, 110);
                        SetSkill(SkillName.Anatomy, 95, 110);
                        SetSkill(SkillName.Wrestling, 95, 110);
                        SetSkill(SkillName.Swords, 95, 110);
                        SetSkill(SkillName.Fencing, 95, 110);
                        SetSkill(SkillName.Parry, 85, 100);
                        SetSkill(SkillName.Macing, 95, 110);
                        SetSkill(SkillName.MagicResist, 85, 95);

                        if (type == GuardType.Wizard)
                        {
                            SetMana(100, 120);
                            SetSkill(SkillName.Magery, 90, 100);
                            SetSkill(SkillName.EvalInt, 90, 100);
                            SetSkill(SkillName.Focus, 70, 90);
                            SetSkill(SkillName.Meditation, 90, 100);
                        }
                        else if (type == GuardType.Archer)
                        {
                            SetSkill(SkillName.Archery, 90, 100);
                            this.RangeFight = 6;
                        }
                    }
                    break;
                case GuardPatent.Colonel:
                    {
                        SetStr(110, 120);
                        SetDex(110, 120);
                        SetInt(110, 120);

                        SetHits(200, 250);

                        SetSkill(SkillName.Tactics, 110, 120);
                        SetSkill(SkillName.Healing, 110, 120);
                        SetSkill(SkillName.Anatomy, 110, 120);
                        SetSkill(SkillName.Wrestling, 110, 120);
                        SetSkill(SkillName.Swords, 110, 120);
                        SetSkill(SkillName.Fencing, 110, 120);
                        SetSkill(SkillName.Parry, 100, 110);
                        SetSkill(SkillName.Macing, 110, 120);
                        SetSkill(SkillName.MagicResist, 100, 110);

                        if (type == GuardType.Wizard)
                        {
                            SetMana(120, 150);
                            SetSkill(SkillName.Magery, 100, 110);
                            SetSkill(SkillName.EvalInt, 100, 110);
                            SetSkill(SkillName.Focus, 80, 100);
                            SetSkill(SkillName.Meditation, 100, 110);
                        }
                        else if (type == GuardType.Archer)
                        {
                            SetSkill(SkillName.Archery, 100, 110);
                            this.RangeFight = 6;
                        }
                    }
                    break;
                case GuardPatent.General:
                    {
                        SetStr(120, 120);
                        SetDex(120, 120);
                        SetInt(120, 120);

                        SetHits(300, 500);

                        SetSkill(SkillName.Tactics, 120);
                        SetSkill(SkillName.Healing, 120);
                        SetSkill(SkillName.Anatomy, 120);
                        SetSkill(SkillName.Wrestling, 120);
                        SetSkill(SkillName.Swords, 120);
                        SetSkill(SkillName.Fencing, 120);
                        SetSkill(SkillName.Parry, 120);
                        SetSkill(SkillName.Macing, 120);
                        SetSkill(SkillName.MagicResist, 120);

                        if (type == GuardType.Wizard)
                        {
                            SetMana(150, 200);
                            SetSkill(SkillName.Magery, 120);
                            SetSkill(SkillName.EvalInt, 120);
                            SetSkill(SkillName.Focus, 100);
                            SetSkill(SkillName.Meditation, 120);
                        }
                        else if (type == GuardType.Archer)
                        {
                            SetSkill(SkillName.Archery, 120);
                            this.RangeFight = 6;
                        }
                    }
                    break;
            }
        }

        private void AddEquipment(GuardType type, GuardPatent patent)
        {
            // color
            int cloathColor = 0; // soldier
            switch (patent)
            {
                default:
                case GuardPatent.Soldier:
                case GuardPatent.Sargent:
                    {
                        cloathColor = 0;
                    }
                    break;
                case GuardPatent.lieutenant:
                    {
                        cloathColor = 291;
                    }
                    break;
                case GuardPatent.Captain: 
                    {
                        cloathColor = 346;
                    } break;
                case GuardPatent.Colonel:
                case GuardPatent.General:
                    {
                        cloathColor = 2815;
                    }
                    break;
            }
            // equipments
            switch (type)
            {
                default:
                case GuardType.Archer:
                    {
                        LeatherLegs legs = new LeatherLegs();
                        StuddedChest chest = new StuddedChest();
                        LeatherGloves gloves = new LeatherGloves();
                        LeatherArms arms = new LeatherArms();
                        Bow bow = new Bow();
                        bow.Quality = WeaponQuality.Low;

                        AddToBackpack(new Arrow(100));

                        if (patent >= GuardPatent.lieutenant) 
                        {
                            legs.Resource = CraftResource.SpinedLeather;
                            chest.Resource = CraftResource.SpinedLeather;
                            gloves.Resource = CraftResource.SpinedLeather;
                            arms.Resource = CraftResource.SpinedLeather;

                            //bow.Resource = CraftResource.AshTree;
                            bow.Quality = WeaponQuality.Regular;
                        }
                        if (patent >= GuardPatent.Captain)
                        {
                            legs.Resource = CraftResource.HornedLeather;
                            chest.Resource = CraftResource.HornedLeather;
                            gloves.Resource = CraftResource.HornedLeather;
                            arms.Resource = CraftResource.HornedLeather;

                            bow.Resource = CraftResource.AshTree;
                            bow.Quality = WeaponQuality.Regular;

                            //bow.Speed += 3;
                            AddToBackpack(new Arrow(50));
                        }
                        if (patent >= GuardPatent.Colonel)
                        {
                            legs.Resource = CraftResource.BarbedLeather;
                            chest.Resource = CraftResource.BarbedLeather;
                            gloves.Resource = CraftResource.BarbedLeather;
                            arms.Resource = CraftResource.BarbedLeather;

                            bow.Resource = CraftResource.EbonyTree;
                            bow.Quality = WeaponQuality.Regular;
                            bow.Speed += 1;
                            AddToBackpack(new Arrow(100)); // + 100
                        }
                        AddItem(legs);
                        AddItem(chest);
                        AddItem(gloves);
                        AddItem(arms);
                        AddItem(bow);
                    } break;
                case GuardType.Cavalry:
                    {

                        PlateLegs legs = new PlateLegs();
                        RingmailChest chest = new RingmailChest();
                        FancyShirt shirt = new FancyShirt();
                        PlateGorget gorget = new PlateGorget();
                        RingmailGloves gloves = new RingmailGloves();

                        BaseWeapon weapon;

                        if (Utility.RandomBool())
                            weapon = new Halberd();
                        else
                            weapon = new Bardiche();

                        legs.Resource = CraftResource.Iron;
                        chest.Resource = CraftResource.Iron;
                        shirt.Resource = CraftResource.Iron;
                        gorget.Resource = CraftResource.Iron;
                        gloves.Resource = CraftResource.Iron;
                        weapon.Quality = WeaponQuality.Low;
                        weapon.Resource = CraftResource.Iron;

                        if (patent >= GuardPatent.lieutenant)
                        {
                            legs.Resource = CraftResource.DullCopper;
                            chest.Resource = CraftResource.DullCopper;
                            shirt.Resource = CraftResource.DullCopper;
                            gorget.Resource = CraftResource.DullCopper;
                            gloves.Resource = CraftResource.DullCopper;

                            weapon.Quality = WeaponQuality.Regular;
                            weapon.Resource = CraftResource.DullCopper;
                            weapon.Speed += 1;
                        }
                        if (patent >= GuardPatent.Captain)
                        {
                            legs.Resource = CraftResource.Copper;
                            chest.Resource = CraftResource.Copper;
                            shirt.Resource = CraftResource.Copper;
                            gorget.Resource = CraftResource.Copper;
                            gloves.Resource = CraftResource.Copper;

                            weapon.Quality = WeaponQuality.Regular;
                            weapon.Resource = CraftResource.Copper;
                            //weapon.Speed += 1;
                        }
                        if (patent >= GuardPatent.Colonel)
                        {
                            legs.Resource = CraftResource.Bronze;
                            chest.Resource = CraftResource.Bronze;
                            shirt.Resource = CraftResource.Bronze;
                            gorget.Resource = CraftResource.Bronze;
                            gloves.Resource = CraftResource.Bronze;

                            weapon.Quality = WeaponQuality.Exceptional;
                            weapon.Resource = CraftResource.Bronze;
                            //weapon.Speed += 1;
                        }
                        AddItem(legs);
                        AddItem(chest);
                        AddItem(shirt);
                        AddItem(gorget);
                        AddItem(gloves);
                        AddItem(weapon);
                    } break;
                case GuardType.Pikeman:
                    {
                        RingmailLegs legs = new RingmailLegs();
                        RingmailChest chest = new RingmailChest();
                        RingmailArms arms = new RingmailArms();
                        PlateGorget gorget = new PlateGorget();
                        RingmailGloves gloves = new RingmailGloves();

                        BaseArmor helm = new CloseHelm();
                        if (Utility.RandomBool())
                            helm = new NorseHelm();

                        Pike weapon = new Pike();

                        helm.Resource = CraftResource.Iron;
                        legs.Resource = CraftResource.Iron;
                        chest.Resource = CraftResource.Iron;
                        arms.Resource = CraftResource.Iron;
                        gorget.Resource = CraftResource.Iron;
                        gloves.Resource = CraftResource.Iron;
                        weapon.Quality = WeaponQuality.Low;
                        weapon.Resource = CraftResource.Iron;

                        if (patent >= GuardPatent.lieutenant)
                        {
                            helm.Resource = CraftResource.DullCopper;
                            legs.Resource = CraftResource.DullCopper;
                            chest.Resource = CraftResource.DullCopper;
                            arms.Resource = CraftResource.DullCopper;
                            gorget.Resource = CraftResource.DullCopper;
                            gloves.Resource = CraftResource.DullCopper;
                            weapon.Quality = WeaponQuality.Regular;
                            weapon.Resource = CraftResource.DullCopper;
                            //weapon.Speed += 3;
                        }
                        if (patent >= GuardPatent.Captain)
                        {
                            helm.Resource = CraftResource.Copper;
                            legs.Resource = CraftResource.Copper;
                            chest.Resource = CraftResource.Copper;
                            arms.Resource = CraftResource.Copper;
                            gorget.Resource = CraftResource.Copper;
                            gloves.Resource = CraftResource.Copper;
                            weapon.Quality = WeaponQuality.Regular;
                            weapon.Resource = CraftResource.Copper;
                            //weapon.Speed += 3;
                        }
                        if (patent >= GuardPatent.Colonel)
                        {
                            helm.Resource = CraftResource.Bronze;
                            legs.Resource = CraftResource.Bronze;
                            chest.Resource = CraftResource.Bronze;
                            arms.Resource = CraftResource.Bronze;
                            gorget.Resource = CraftResource.Bronze;
                            gloves.Resource = CraftResource.Bronze;
                            weapon.Quality = WeaponQuality.Exceptional;
                            weapon.Resource = CraftResource.Bronze;
                            //weapon.Speed += 5;
                        }

                        AddItem(helm);
                        AddItem(legs);
                        AddItem(chest);
                        AddItem(arms);
                        AddItem(gorget);
                        AddItem(gloves);
                        AddItem(weapon);
                    }
                    break;
                case GuardType.Swordsman:
                    {
                        ChainLegs legs = new ChainLegs();
                        ChainChest chest = new ChainChest();
                        RingmailArms arms = new RingmailArms();
                        PlateGorget gorget = new PlateGorget();
                        RingmailGloves gloves = new RingmailGloves();

                        BaseArmor helm = new CloseHelm();
                        switch (Utility.Random(3))
                        {
                            case 0: helm = new CloseHelm(); break;
                            case 1: helm = new NorseHelm(); break;
                            case 2: helm = new PlateHelm(); break;
                        }

                        BaseWeapon weapon;

                        switch (Utility.Random(4))
                        {
                            default:
                            case 0: weapon = new Broadsword(); break;
                            case 1: weapon = new Longsword(); break;
                            case 2: weapon = new Katana(); break;
                            case 3: weapon = new Axe(); break;
                        }

                        BaseShield shield = new HeaterShield();
                        if (Utility.RandomBool()) 
                        {
                            shield = new MetalKiteShield();
                        }

                        helm.Resource = CraftResource.Iron;
                        legs.Resource = CraftResource.Iron;
                        chest.Resource = CraftResource.Iron;
                        arms.Resource = CraftResource.Iron;
                        gorget.Resource = CraftResource.Iron;
                        gloves.Resource = CraftResource.Iron;
                        shield.Quality = ArmorQuality.Low;
                        shield.Resource = CraftResource.Iron;
                        weapon.Quality = WeaponQuality.Low;
                        weapon.Resource = CraftResource.Iron;
                        weapon.Layer = Layer.OneHanded;

                        if (patent >= GuardPatent.lieutenant)
                        {
                            helm.Resource = CraftResource.DullCopper;
                            legs.Resource = CraftResource.DullCopper;
                            chest.Resource = CraftResource.DullCopper;
                            arms.Resource = CraftResource.DullCopper;
                            gorget.Resource = CraftResource.DullCopper;
                            gloves.Resource = CraftResource.DullCopper;
                            shield.Quality = ArmorQuality.Regular;
                            shield.Resource = CraftResource.DullCopper;
                            weapon.Quality = WeaponQuality.Regular;
                            weapon.Resource = CraftResource.DullCopper;
                            //weapon.Speed += 3;
                        }
                        if (patent >= GuardPatent.Captain)
                        {
                            helm.Resource = CraftResource.Copper;
                            legs.Resource = CraftResource.Copper;
                            chest.Resource = CraftResource.Copper;
                            arms.Resource = CraftResource.Copper;
                            gorget.Resource = CraftResource.Copper;
                            gloves.Resource = CraftResource.Copper;
                            shield.Quality = ArmorQuality.Regular;
                            shield.Resource = CraftResource.Copper;
                            weapon.Quality = WeaponQuality.Regular;
                            weapon.Resource = CraftResource.Copper;
                            //weapon.Speed += 3;
                        }
                        if (patent >= GuardPatent.Colonel)
                        {
                            helm.Resource = CraftResource.Bronze;
                            legs.Resource = CraftResource.Bronze;
                            chest.Resource = CraftResource.Bronze;
                            arms.Resource = CraftResource.Bronze;
                            gorget.Resource = CraftResource.Bronze;
                            gloves.Resource = CraftResource.Bronze;
                            shield.Quality = ArmorQuality.Exceptional;
                            shield.Resource = CraftResource.Bronze;
                            weapon.Quality = WeaponQuality.Exceptional;
                            weapon.Resource = CraftResource.Bronze;
                            //weapon.Speed += 5;
                        }

                        AddItem(helm);
                        AddItem(legs);
                        AddItem(chest);
                        AddItem(arms);
                        AddItem(gorget);
                        AddItem(gloves);
                        AddItem(weapon);
                        AddItem(shield);
                    } 
                    break;
                case GuardType.Wizard:
                    {
                        AddItem(new WizardsHat(cloathColor));
                        AddItem(new Robe(Utility.RandomNondyedHue()));

                        BagOfReagents reags = new BagOfReagents();
                        AddItem(reags);

                        GnarledStaff staff = new GnarledStaff();
                        staff.Attributes.SpellChanneling = 1;
                        staff.Attributes.SpellDamage = Utility.RandomMinMax(2, 4);
                        staff.Quality = WeaponQuality.Low;

                        if (patent >= GuardPatent.lieutenant)
                        {
                            staff.Attributes.SpellDamage = Utility.RandomMinMax(3, 5);
                            staff.Resource = CraftResource.RegularWood;
                            staff.Quality = WeaponQuality.Regular;
                        }
                        if (patent >= GuardPatent.Captain)
                        {
                            staff.Attributes.SpellDamage = Utility.RandomMinMax(5, 7);
                            staff.Resource = CraftResource.AshTree;
                            staff.Quality = WeaponQuality.Regular;
                        }
                        if (patent >= GuardPatent.Colonel)
                        {
                            staff.Attributes.SpellDamage = Utility.RandomMinMax(7, 9);
                            staff.Resource = CraftResource.EbonyTree;
                            staff.Quality = WeaponQuality.Exceptional;
                        }
                        AddItem(staff);
                    }
                    break;
                case GuardType.Medic:
                    {
                        AddItem(new Bandana(cloathColor));
                        AddItem(new Robe(Utility.RandomNondyedHue()));
                    } 
                    break;
            }

            AddItem(new Boots());
            AddItem(new Cloak(cloathColor));
            AddItem(new BodySash(cloathColor));
        }
        #endregion

        public override bool BardImmune { get { return true; } }

        public override bool IsScaryToPets
        {
            get
            {
                return true;
            }
        }

        public override bool IsEnemy(Mobile m)
        {
            int noto = Server.Misc.NotorietyHandlers.MobileNotoriety(this, m);

            if (noto == Notoriety.Criminal || noto == Notoriety.Murderer || noto == Notoriety.Enemy)
                return true;

            return base.IsEnemy(m);
        }

        private DateTime _nextCallHelp;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.Administrator)]
        public TimeSpan NextCallout
        {
            get
            {
                TimeSpan time = _nextCallHelp - DateTime.UtcNow;

                if (time < TimeSpan.Zero)
                    time = TimeSpan.Zero;

                return time;
            }
            set
            {
                try { _nextCallHelp = DateTime.UtcNow + value; }
                catch { }
            }
        }

        public override void OnThink()
        {
            //Adjust to control speed of recognition
            if (Utility.RandomDouble() > 0.66)
            {
                if (!SpellHelper.CheckCombat(this))
                {
                  foreach (Mobile mobile in this.GetMobilesInRange(16))
                  {
                    int noto = Server.Misc.NotorietyHandlers.MobileNotoriety(this, mobile);
                    bool isEnemy = (noto == Notoriety.Criminal || noto == Notoriety.Murderer || noto == Notoriety.Enemy);     
                 
                        if (this.Combatant == null && isEnemy && this.CanSee(mobile))
                        {
                               this.DoHarmful(mobile);
                        }                    
                    }  
                }
            }

            if (NextCallout == TimeSpan.Zero)
            {
                int toRescue = 0;

                if(Hits < (HitsMax * 0.33) && Combatant != null)
                {
                    switch(Utility.RandomMinMax( 1, 5 ))
                    {
                        case 1: Say("Preciso de ajuda!!"); break;
                        case 2: Say("Eu não acho que vou conseguir!"); break;
                        case 3: Say("Alguém pode me ajudar?"); break;
                        case 4: Say("Não consigo sozinho!."); break;
                        case 5: Say("Acho que me lasquei!"); break;
                        default: break;
                    }

                    NextCallout = TimeSpan.FromSeconds(Utility.RandomMinMax( 20, 40));

                    foreach (Mobile m in this.GetMobilesInRange(16))
                    {
                        if(m is Guard && m.Hits > (m.HitsMax * 0.33) && m != this)
                        {
                            if (Combatant != null && Combatant != m.Combatant)
                            {
                                if (toRescue == 0)
                                {                                
                                    switch (Utility.RandomMinMax(1, 4))
                                    {
                                        case 1: m.Say("Estou a caminho!"); break;
                                        case 2: m.Say("Aguente firme {0} ! Estou com você!", Name); break;
                                        case 3: m.Say("Você consegue!"); break;
                                        case 4: m.Say("Segura ai {0} ! Estou chegando.", Name); break;
                                        default: break;
                                    }
                                }

                                m.Combatant = Combatant;
                                m.DoHarmful(Combatant);

                                toRescue++;

                                if (toRescue > 1)
                                    return;
                            }
                        }
                    }
                }
            }

            base.OnThink();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write((int)m_Type);
            writer.Write((int)m_Patent);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Type = (GuardType)reader.ReadInt();
            m_Patent = (GuardPatent)reader.ReadInt();
        }
    }
}

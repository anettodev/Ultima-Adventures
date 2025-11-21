using System;
using Server;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;
using System.Collections;
using Server.Multis;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.PartySystem;
using Server.Misc;

namespace Server.Engines.Harvest
{
    public partial class Fishing : HarvestSystem
    {
        #region Mutation Table

        /// <summary>
        /// Entry structure for fishing mutations
        /// </summary>
        private struct MutateEntry
        {
            public string m_group;
            public double m_MinSkill;
            public double m_MaxSkill;
            public double m_ReqSkill;
            public bool m_DeepWater;
            public Type[] m_Types;

            public MutateEntry(string group, double minSkill, double maxSkill, double reqSkill, bool deepWater, params Type[] types)
            {
                m_group = group;
                m_MinSkill = minSkill;
                m_MaxSkill = maxSkill;
                m_ReqSkill = reqSkill;
                m_DeepWater = deepWater;
                m_Types = types;
            }
        }

        private static MutateEntry[] m_MutateTable = new MutateEntry[]
        {
            new MutateEntry("grupo1", 70.0, 70.0, 75.0, false, typeof(RustyJunk), typeof(WetClothes)), //min 0,02% - max 10%
            new MutateEntry("grupo2", 75.0, 75.0, 80, false, typeof(FishingNet), typeof(SpecialSeaweed), typeof(BlackPearl)), //min 0,02% - max 9%
            new MutateEntry("grupo3", 80.0, 80.0, 85.0, true, typeof(PrizedFish), typeof(InvisibleFish), typeof(PoisonFish), typeof(BigFish)), //min 0,02% - max 8%
            new MutateEntry("grupo4", 85.0, 85.0, 90.0, true, typeof(WondrousFish), typeof(StaminaFish), typeof(HealFish), typeof(ManaFish)), //min 0,02% - max 7%
            new MutateEntry("grupo5", 90.0, 90.0, 95.0, true, typeof(CorpseSailor), typeof(TrulyRareFish), typeof(PeculiarFish)), //min 0,02% - max 6%
            new MutateEntry("grupo6", 95.0, 95.0, 100.0, true, typeof(NewFish), typeof(PearlSkull)), //min 0,02% - max 5%
            new MutateEntry("grupo7", 100.0, 100.0, 105.0, true, typeof(SunkenBag)), //min 0,02% - max 4%
            new MutateEntry("grupo8", 105.0, 105.0, 110.0, true, typeof(SpecialFishingNet), typeof(NeptunesFishingNet), typeof(FabledFishingNet)), //min 0,2% - max 3%
            new MutateEntry("grupo9", 110.0, 110.0, 120.0, true, typeof(MessageInABottle)) //min 0,02% - max 1%
        };

        /// <summary>
        /// Handles type mutation for fishing based on skill and location
        /// </summary>
        public override Type MutateType(Type type, Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource)
        {
            bool deepWater = false;
            PlayerMobile pm = (PlayerMobile)from;

            if (Server.Misc.Worlds.IsOnBoat(from) && !Server.Misc.Worlds.BoatToCloseToTown(from))
                deepWater = true;

            double skillBase = from.Skills[SkillName.Fishing].Base;
            double skillValue = from.Skills[SkillName.Fishing].Value;

            for (int i = 0; i < m_MutateTable.Length; ++i)
            {
                MutateEntry entry = m_MutateTable[i];

                if (!deepWater && entry.m_DeepWater)
                    continue;

                if (skillBase >= entry.m_ReqSkill)
                {
                    double chance = ((skillValue - entry.m_MinSkill) / (entry.m_MaxSkill - entry.m_MinSkill)) / 100;
                    double random = Utility.RandomDouble();

                    if (chance >= random)
                    {
                        pm.PlaySound(pm.Female ? 783 : 1054);
                        pm.Say("*woohoo!*");
                        from.SendMessage(55, "Que sorte! Você pescou algo inesperado e colocou em sua mochila!");
                        return entry.m_Types[Utility.Random(entry.m_Types.Length)];
                    }
                }
            }

            return type;
        }

        #endregion

        #region Resource Checking

        /// <summary>
        /// Checks for special resources like SOS messages
        /// </summary>
        public override bool CheckResources(Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, bool timed)
        {
            Container pack = from.Backpack;
            PlayerMobile pm = (PlayerMobile)from;
            if (pack != null)
            {
                List<SOS> messages = pack.FindItemsByType<SOS>();

                for (int i = 0; i < messages.Count; ++i)
                {
                    SOS sos = messages[i];

                    if (from.Map == sos.TargetMap && from.InRange(sos.TargetLocation, 60))
                    {
                        return true;
                    }
                }
            }

            return base.CheckResources(from, tool, def, map, loc, timed);
        }

        #endregion

        #region Item Construction

        /// <summary>
        /// Generates random AOS stats for magical items
        /// </summary>
        private static void GetRandomAOSStats(out int attributeCount, out int min, out int max, int level)
        {
            int rnd = Utility.Random(15);
            attributeCount = Utility.RandomMinMax(1, level);
            min = level * 3;
            max = level * 7;
        }

        /// <summary>
        /// Constructs special fishing items and treasure
        /// </summary>
        public override Item Construct(Type type, Mobile from)
        {
            if (type == typeof(MessageInABottle))
            {
                return new MessageInABottle(from.Map, 0, from.Location, from.X, from.Y);
            }

            Container pack = from.Backpack;

            if (pack != null)
            {
                List<SOS> messages = pack.FindItemsByType<SOS>();

                for (int i = 0; i < messages.Count; ++i)
                {
                    SOS sos = messages[i];

                    if (from.Map == sos.TargetMap && from.InRange(sos.TargetLocation, 60))
                    {
                        int nWave = Utility.Random(8);
                        int nGuild = 0;

                        PlayerMobile pm = (PlayerMobile)from;

                        if (pm.NpcGuild != NpcGuild.FishermensGuild)
                        {
                            nWave = Utility.Random(11);
                            nGuild = (int)(from.Skills[SkillName.Fishing].Value / 4);
                        }

                        int mLevel = (int)(from.Skills[SkillName.Fishing].Value / 10) + 1;

                        Item preLoot = null;
                        int nChance = (int)(from.Skills[SkillName.Fishing].Value / 4) + nGuild;

                        #region Treasure Generation
                        switch (nWave)
                        {
                            case 1: // Body parts
                                {
                                    if (nChance > Utility.Random(100))
                                        preLoot = new RustyJunk();
                                    else
                                    {
                                        int[] list = new int[]
                                            {
                                                    0x1CDD, 0x1CE5, // arm
                                                    0x1CE0, 0x1CE8, // torso
                                                    0x1CE1, 0x1CE9, // head
                                                    0x1CE2, 0x1CEC, // leg
                                                    0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3, 0x1AE4, // skulls
                                                    0x1B09, 0x1B0A, 0x1B0B, 0x1B0C, 0x1B0D, 0x1B0E, 0x1B0F, 0x1B10, // bone piles
                                                    0x1B15, 0x1B16 // pelvis bones
                                            };

                                        preLoot = new ShipwreckedItem(Utility.RandomList(list), sos.ShipName);
                                        preLoot.Hue = Utility.RandomList(0xB97, 0xB98, 0xB99, 0xB9A, 0xB88);
                                    }
                                    break;
                                }
                            case 2: // Paintings and portraits
                                {
                                    if (nChance > Utility.Random(100))
                                    {
                                        preLoot = new DDRelicPainting();
                                        preLoot.Hue = Utility.RandomList(0xB97, 0xB98, 0xB99, 0xB9A, 0xB88);
                                        preLoot.Name = preLoot.Name + " (coberto de lodo)";
                                    }
                                    else
                                        preLoot = new ShipwreckedItem(Utility.Random(0xE9F, 10), sos.ShipName);

                                    break;
                                }
                            case 3: // Misc
                                {
                                    if (nChance > Utility.Random(100))
                                    {
                                        switch (Utility.Random(4))
                                        {
                                            case 0: preLoot = new DDRelicArts(); break;
                                            case 1: preLoot = new DDRelicDrink(); break;
                                            case 2: preLoot = new DDRelicInstrument(); break;
                                            case 3: preLoot = new DDRelicJewels(); break;
                                        }
                                    }
                                    else
                                    {
                                        if (Utility.Random(20) == 1)
                                        {
                                            preLoot = new ShipwreckedItem(Utility.RandomList(0x12AD), sos.ShipName);
                                        }
                                        else
                                        {
                                            preLoot = new ShipwreckedItem(Utility.Random(0x13A4, 11), sos.ShipName);
                                            preLoot.Hue = RandomThings.GetRandomColor(0);
                                        }
                                    }
                                    break;
                                }
                            case 4: // Shells
                                {
                                    if (nChance > Utility.Random(100))
                                        preLoot = new NewFish();
                                    else
                                        preLoot = new ShipwreckedItem(Utility.Random(0xFC4, 9), sos.ShipName);
                                    break;
                                }
                            case 5: // Hats
                                {
                                    if (nChance > Utility.Random(100))
                                    {
                                        preLoot = new MagicHat();
                                        string sAdj = "mágico";
                                        switch (Utility.RandomMinMax(1, 7))
                                        {
                                            case 1: sAdj = "mágico "; break;
                                            case 2: sAdj = "mágico "; break;
                                            case 3: sAdj = "místico "; break;
                                            case 4: sAdj = "encantado "; break;
                                            case 5: sAdj = "misterioso "; break;
                                            case 6: sAdj = "mítico "; break;
                                            case 7: sAdj = "inusitado "; break;
                                        }
                                        if (Utility.RandomBool()) { preLoot.Name = sAdj + "barrete"; preLoot.ItemID = 5444; }
                                        else { preLoot.Name = sAdj + "chapéu de pirata"; preLoot.ItemID = 5915; }

                                        int attributeCount;
                                        int min, max;
                                        GetRandomAOSStats(out attributeCount, out min, out max, mLevel);
                                        BaseRunicTool.ApplyAttributesTo((BaseJewel)preLoot, attributeCount, min, max);
                                    }
                                    else
                                    {
                                        if (Utility.RandomBool())
                                        {
                                            preLoot = new SkullCap();
                                            if (Utility.Random(4) == 1)
                                            {
                                                preLoot.Hue = Utility.RandomList(0xB97, 0xB98, 0xB99, 0xB9A, 0xB88);
                                                preLoot.Name = "barrete ensopado";
                                            }
                                            else
                                            {
                                                preLoot.Hue = RandomThings.GetRandomColor(0);
                                                preLoot.Name = "barrete";
                                            }
                                        }
                                        else
                                        {
                                            preLoot = new TricorneHat();
                                            if (Utility.Random(4) == 1)
                                            {
                                                preLoot.Hue = Utility.RandomList(0xB97, 0xB98, 0xB99, 0xB9A, 0xB88);
                                                preLoot.Name = "tricórnio ensopado";
                                            }
                                            else
                                            {
                                                preLoot.Hue = RandomThings.GetRandomColor(0);
                                                preLoot.Name = "tricórnio";
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 6: // Armor
                                {
                                    if (nChance > Utility.Random(100))
                                    {
                                        switch (Utility.Random(6))
                                        {
                                            case 0: preLoot = new DDRelicArmor(); break;
                                            case 1: preLoot = new DDRelicCloth(); break;
                                            case 2: preLoot = new DDRelicFur(); break;
                                            case 3: preLoot = new DDRelicLeather(); break;
                                            case 4: preLoot = new DDRelicStudded(); break;
                                            case 5: preLoot = new DDRelicWood(); break;
                                        }
                                        int attributeCount;
                                        int min, max;
                                        GetRandomAOSStats(out attributeCount, out min, out max, mLevel);
                                        BaseRunicTool.ApplyAttributesTo((BaseArmor)preLoot, attributeCount, min, max);
                                    }
                                    else
                                        preLoot = new ShipwreckedItem(Utility.Random(0x13BB, 10), sos.ShipName);
                                    break;
                                }
                            case 7: // Weapons
                                {
                                    if (nChance > Utility.Random(100))
                                    {
                                        switch (Utility.Random(5))
                                        {
                                            case 0: preLoot = new DDRelicWeapon(); break;
                                            case 1: preLoot = new DDRelicAxe(); break;
                                            case 2: preLoot = new DDRelicBow(); break;
                                            case 3: preLoot = new DDRelicPole(); break;
                                            case 4: preLoot = new DDRelicSword(); break;
                                        }
                                        int attributeCount;
                                        int min, max;
                                        GetRandomAOSStats(out attributeCount, out min, out max, mLevel);
                                        BaseRunicTool.ApplyAttributesTo((BaseWeapon)preLoot, attributeCount, min, max);
                                    }
                                    else
                                        preLoot = new ShipwreckedItem(Utility.Random(0x13B4, 23), sos.ShipName);
                                    break;
                                }
                            default:
                                {
                                    preLoot = new ShipwreckedItem(Utility.Random(0x13A4, 11), sos.ShipName);
                                    preLoot.Hue = RandomThings.GetRandomColor(0);
                                    break;
                                }
                        }

                        if (preLoot != null)
                        {
                            sos.Delete();
                            return preLoot;
                        }
                        #endregion
                    }
                }
            }

            return base.Construct(type, from);
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Safely gets a map, defaulting to Trammel if the map is invalid
        /// </summary>
        private static Map SafeMap(Map map)
        {
            if (map == null || map == Map.Internal)
                return Map.Trammel;

            return map;
        }

        #endregion
    }
}

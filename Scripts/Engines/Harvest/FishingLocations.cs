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
        #region Location Checking Methods

        /// <summary>
        /// Checks if the mobile is near a huge shipwreck
        /// </summary>
        public static bool IsNearHugeShipWreck(Mobile from)
        {
            if (from.InRange(new Point3D(578, 1370, -5), 36) && from.Map == Map.Ilshenar)
                return true;
            else if (from.InRange(new Point3D(946, 821, -5), 36) && from.Map == Map.TerMur)
                return true;
            else if (from.InRange(new Point3D(969, 217, -5), 36) && from.Map == Map.TerMur)
                return true;
            else if (from.InRange(new Point3D(322, 661, -5), 36) && from.Map == Map.TerMur)
                return true;
            else if (from.InRange(new Point3D(760, 587, -5), 36) && from.Map == Map.Tokuno)
                return true;
            else if (from.InRange(new Point3D(200, 1056, -5), 36) && from.Map == Map.Tokuno)
                return true;
            else if (from.InRange(new Point3D(1232, 387, -5), 36) && from.Map == Map.Tokuno)
                return true;
            else if (from.InRange(new Point3D(528, 233, -5), 36) && from.Map == Map.Tokuno)
                return true;
            else if (from.InRange(new Point3D(504, 1931, -5), 36) && from.Map == Map.Malas)
                return true;
            else if (from.InRange(new Point3D(1472, 1776, -5), 36) && from.Map == Map.Malas)
                return true;
            else if (from.InRange(new Point3D(1560, 579, -5), 36) && from.Map == Map.Malas)
                return true;
            else if (from.InRange(new Point3D(1328, 144, -5), 36) && from.Map == Map.Malas)
                return true;
            else if (from.InRange(new Point3D(2312, 2299, -5), 36) && from.Map == Map.Felucca)
                return true;
            else if (from.InRange(new Point3D(2497, 3217, -5), 36) && from.Map == Map.Felucca)
                return true;
            else if (from.InRange(new Point3D(576, 3523, -5), 36) && from.Map == Map.Felucca)
                return true;
            else if (from.InRange(new Point3D(4352, 3768, -5), 36) && from.Map == Map.Felucca)
                return true;
            else if (from.InRange(new Point3D(4824, 1627, -5), 36) && from.Map == Map.Felucca)
                return true;
            else if (from.InRange(new Point3D(3208, 216, -5), 36) && from.Map == Map.Felucca)
                return true;
            else if (from.InRange(new Point3D(1112, 619, -5), 36) && from.Map == Map.Felucca)
                return true;
            else if (from.InRange(new Point3D(521, 2153, -5), 36) && from.Map == Map.Felucca)
                return true;
            else if (from.InRange(new Point3D(2920, 1643, -5), 36) && from.Map == Map.Felucca)
                return true;
            else if (from.InRange(new Point3D(320, 2288, -5), 36) && from.Map == Map.Trammel)
                return true;
            else if (from.InRange(new Point3D(3343, 1842, -5), 36) && from.Map == Map.Trammel)
                return true;
            else if (from.InRange(new Point3D(3214, 938, -5), 36) && from.Map == Map.Trammel)
                return true;
            else if (from.InRange(new Point3D(4520, 1128, -5), 36) && from.Map == Map.Trammel)
                return true;
            else if (from.InRange(new Point3D(4760, 2307, -5), 36) && from.Map == Map.Trammel)
                return true;
            else if (from.InRange(new Point3D(3551, 2952, -5), 36) && from.Map == Map.Trammel)
                return true;
            else if (from.InRange(new Point3D(1271, 2651, -5), 36) && from.Map == Map.Trammel)
                return true;
            else if (from.InRange(new Point3D(744, 1304, -5), 36) && from.Map == Map.Trammel)
                return true;
            else if (from.InRange(new Point3D(735, 555, -5), 36) && from.Map == Map.Trammel)
                return true;
            else if (from.InRange(new Point3D(1824, 440, -5), 36) && from.Map == Map.Trammel)
                return true;

            return false;
        }

        /// <summary>
        /// Checks if the mobile is near a space crash site
        /// </summary>
        public static bool IsNearSpaceCrash(Mobile from)
        {
            if (from.X >= 457 && from.X <= 494 && from.Y >= 1785 && from.Y <= 1821 && from.Map == Map.Trammel)
                return true;

            if (from.X >= 4430 && from.X <= 4501 && from.Y >= 589 && from.Y <= 661 && from.Map == Map.Trammel)
                return true;

            return false;
        }

        /// <summary>
        /// Checks if the mobile is near underwater ruins
        /// </summary>
        public static bool IsNearUnderwaterRuins(Mobile from)
        {
            if (from.X >= 4342 && from.X <= 4420 && from.Y >= 2766 && from.Y <= 2845 && from.Map == Map.Trammel)
                return true;

            if (from.X >= 175 && from.X <= 243 && from.Y >= 2316 && from.Y <= 2344 && from.Map == Map.Trammel)
                return true;

            if (from.X >= 3664 && from.X <= 3737 && from.Y >= 2522 && from.Y <= 2594 && from.Map == Map.Trammel)
                return true;

            if (from.X >= 1668 && from.X <= 1734 && from.Y >= 1309 && from.Y <= 1376 && from.Map == Map.Felucca)
                return true;

            if (from.X >= 1573 && from.X <= 1634 && from.Y >= 3261 && from.Y <= 3326 && from.Map == Map.Felucca)
                return true;

            return false;
        }

        #endregion

        #region Location-Based Fishing Methods

        /// <summary>
        /// Handles fishing up items from spaceship crash sites
        /// </summary>
        public static void FishUpFromSpaceship(Mobile from)
        {
            int nGuild = 0;

            PlayerMobile pc = (PlayerMobile)from;
            if (pc.NpcGuild != NpcGuild.FishermensGuild)
            {
                nGuild = (int)(from.Skills[SkillName.Fishing].Value / 4);
            }

            int nChance = (int)(from.Skills[SkillName.Fishing].Value / 4) + nGuild;

            if (nChance > Utility.Random(100))
            {
                Item preLoot = Server.Items.DungeonLoot.RandomSpaceCrash();
                from.AddToBackpack(preLoot);
                from.SendMessage("Você pesca algo dos destroços abaixo.");
            }
        }

        /// <summary>
        /// Handles fishing up items from underwater ruins
        /// </summary>
        public static void FishUpFromRuins(Mobile from)
        {
            int nGuild = 1;

            PlayerMobile pc = (PlayerMobile)from;
            if (pc.NpcGuild != NpcGuild.FishermensGuild)
            {
                nGuild = (int)(from.Skills[SkillName.Fishing].Value / 25);
            }

            Item preLoot = new RustyJunk();

            int goldBoost = (int)(from.Skills[SkillName.Fishing].Value * nGuild);
            switch (Utility.Random(18))
            {
                case 0: preLoot = new DDRelicLeather(); ((DDRelicLeather)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 1: preLoot = new DDRelicOrbs(); ((DDRelicOrbs)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 2: preLoot = new DDRelicPainting(); ((DDRelicPainting)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 3: preLoot = new DDRelicRugAddonDeed(); ((DDRelicRugAddonDeed)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 4: preLoot = new DDRelicScrolls(); ((DDRelicScrolls)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 5: preLoot = new DDRelicStatue(); ((DDRelicStatue)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 6: preLoot = new DDRelicVase(); ((DDRelicVase)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 7: preLoot = new DDRelicWeapon(); ((DDRelicWeapon)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 8: preLoot = new DDRelicArmor(); ((DDRelicArmor)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 9: preLoot = new DDRelicArts(); ((DDRelicArts)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 10: preLoot = new DDRelicBanner(); ((DDRelicBanner)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 11: preLoot = new DDRelicBearRugsAddonDeed(); ((DDRelicBearRugsAddonDeed)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 12: preLoot = new DDRelicBook(); ((DDRelicBook)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 13: preLoot = new DDRelicCloth(); ((DDRelicCloth)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 14: preLoot = new DDRelicFur(); ((DDRelicFur)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 15: preLoot = new DDRelicInstrument(); ((DDRelicInstrument)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 16: preLoot = new DDRelicJewels(); ((DDRelicJewels)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                case 17:
                    switch (Utility.Random(3))
                    {
                        case 0: preLoot = new DDRelicClock1(); ((DDRelicClock1)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                        case 1: preLoot = new DDRelicClock2(); ((DDRelicClock2)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                        case 2: preLoot = new DDRelicClock3(); ((DDRelicClock3)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                    }
                    break;
            }

            from.AddToBackpack(preLoot);
            from.SendMessage("Você pesca algo das ruínas abaixo.");
        }

        /// <summary>
        /// Handles fishing up items from major shipwrecks
        /// </summary>
        public static void FishUpFromMajorWreck(Mobile from)
        {
            string ship = Server.Misc.RandomThings.GetRandomShipName("", 0);

            int nWave = Utility.Random(7);
            int nGuild = 0;

            PlayerMobile pc = (PlayerMobile)from;
            if (pc.NpcGuild != NpcGuild.FishermensGuild)
            {
                nWave = Utility.Random(8);
                nGuild = (int)(from.Skills[SkillName.Fishing].Value / 4);
            }

            int mLevel = (int)(from.Skills[SkillName.Fishing].Value / 10) + 1;

            Item preLoot = new RustyJunk();
            int nChance = (int)(from.Skills[SkillName.Fishing].Value / 4) + nGuild;

            switch (nWave)
            {
                case 0: // Body parts
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

                            preLoot = new ShipwreckedItem(Utility.RandomList(list), ship);
                            preLoot.Hue = Utility.RandomList(0xB97, 0xB98, 0xB99, 0xB9A, 0xB88);
                        }
                        break;
                    }
                case 1: // Paintings and portraits
                    {
                        if (nChance > Utility.Random(100))
                        {
                            preLoot = new DDRelicPainting();
                            preLoot.Hue = Utility.RandomList(0xB97, 0xB98, 0xB99, 0xB9A, 0xB88);
                            preLoot.Name = preLoot.Name + " (coberto de lama)";
                        }
                        else
                            preLoot = new ShipwreckedItem(Utility.Random(0xE9F, 10), ship);

                        break;
                    }
                case 2: // Misc
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
                            preLoot = new ShipwreckedItem(Utility.Random(0x13A4, 11), ship);
                            preLoot.Hue = RandomThings.GetRandomColor(0);
                        }
                        break;
                    }
                case 3: // Shells
                    {
                        if (nChance > Utility.Random(100))
                            preLoot = new NewFish();
                        else
                            preLoot = new ShipwreckedItem(Utility.Random(0xFC4, 9), ship);
                        break;
                    }
                case 4: // Hats
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
                                preLoot.Name = "chapéu de pirata ensopado";
                            }
                            else
                            {
                                preLoot.Hue = RandomThings.GetRandomColor(0);
                                preLoot.Name = "chapéu de pirata";
                            }
                        }
                        break;
                    }
                case 5: // Sea Relic
                    {
                        int[] list = new int[]
                            {
                                0x1EB5, // unfinished barrel
                                0xA2A, // stool
                                0xC1F, // broken clock
                                0x1047, 0x1048, // globe
                                0x1EB1, 0x1EB2, 0x1EB3, 0x1EB4 // barrel staves
                            };

                        if (Utility.Random(list.Length + 1) == 0)
                            preLoot = new Candelabra();
                        else
                            preLoot = new ShipwreckedItem(Utility.RandomList(list), ship);

                        break;
                    }
                case 6: // Boots
                    {
                        preLoot = new ThighBoots(); preLoot.Name = "botas";
                        switch (Utility.Random(4))
                        {
                            case 1: preLoot = new Sandals(); preLoot.Name = "sandálias"; break;
                            case 2: preLoot = new Shoes(); preLoot.Name = "sapatos"; break;
                            case 3: preLoot = new Boots(); preLoot.Name = "botas"; break;
                        }
                        if (Utility.Random(2) == 1)
                        {
                            preLoot.Hue = Utility.RandomList(0xB97, 0xB98, 0xB99, 0xB9A, 0xB88);
                            preLoot.Name = "ensopado " + preLoot.Name;
                        }
                        break;
                    }
                case 7: // Random Relic
                    {
                        int goldBoost = (int)from.Skills[SkillName.Fishing].Value;
                        switch (Utility.Random(18))
                        {
                            case 0: preLoot = new DDRelicLeather(); ((DDRelicLeather)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 1: preLoot = new DDRelicOrbs(); ((DDRelicOrbs)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 2: preLoot = new DDRelicPainting(); ((DDRelicPainting)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 3: preLoot = new DDRelicRugAddonDeed(); ((DDRelicRugAddonDeed)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 4: preLoot = new DDRelicScrolls(); ((DDRelicScrolls)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 5: preLoot = new DDRelicStatue(); ((DDRelicStatue)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 6: preLoot = new DDRelicVase(); ((DDRelicVase)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 7: preLoot = new DDRelicWeapon(); ((DDRelicWeapon)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 8: preLoot = new DDRelicArmor(); ((DDRelicArmor)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 9: preLoot = new DDRelicArts(); ((DDRelicArts)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 10: preLoot = new DDRelicBanner(); ((DDRelicBanner)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 11: preLoot = new DDRelicBearRugsAddonDeed(); ((DDRelicBearRugsAddonDeed)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 12: preLoot = new DDRelicBook(); ((DDRelicBook)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 13: preLoot = new DDRelicCloth(); ((DDRelicCloth)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 14: preLoot = new DDRelicFur(); ((DDRelicFur)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 15: preLoot = new DDRelicInstrument(); ((DDRelicInstrument)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 16: preLoot = new DDRelicJewels(); ((DDRelicJewels)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                            case 17:
                                switch (Utility.Random(3))
                                {
                                    case 0: preLoot = new DDRelicClock1(); ((DDRelicClock1)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                                    case 1: preLoot = new DDRelicClock2(); ((DDRelicClock2)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                                    case 2: preLoot = new DDRelicClock3(); ((DDRelicClock3)preLoot).RelicGoldValue += goldBoost; preLoot.InvalidateProperties(); break;
                                }
                                break;
                        }
                        break;
                    }
            }

            from.AddToBackpack(preLoot);
            from.SendMessage("Você pesca algo dos destroços abaixo.");
        }

        #endregion
    }
}

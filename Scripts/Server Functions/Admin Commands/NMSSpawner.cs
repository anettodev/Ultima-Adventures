using System;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Commands
{
    public class NMSSpawner
    {
        public static void Initialize()
        {
            CommandSystem.Register("NMSSpawner", AccessLevel.GameMaster, new CommandEventHandler(NMSSpawner_OnCommand));
        }

        [Usage("NMSSpawner")]
        [Description("Opens a gump to select and place mobiles by category.")]
        private static void NMSSpawner_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            from.SendGump(new NMSSpawnerGump(from, NMSCategory.None, 0, 1));
        }
    }
    public enum NMSCategory
    {
        None,
        Monsters,
        Monsters2,
        BossSpecialEvent,
        Animals,
        VendorNPCs,
        DarkMoor,
        Others
    }
}

namespace Server.Gumps
{
    public class NMSSpawnerGump : Gump
    {
        private Mobile m_From;
        private NMSCategory m_Category;
        private int m_Page;
        private int m_Count;

        private const int LabelHue = 0x480;
        private const int FontColor = 0xFFFFFF;
        private const int LabelCyan = 0x00FFFF;
        private const int LabelRed = 0xFF0000;

        public NMSSpawnerGump(Mobile from) : this(from, NMSCategory.None, 0, 1)
        {
        }

        public NMSSpawnerGump(Mobile from, NMSCategory category, int page, int count) : base(40, 40)
        {
            m_From = from;
            m_Category = category;
            m_Page = page;
            m_Count = count;

            from.CloseGump(typeof(NMSSpawnerGump));

            AddPage(0);

            AddBackground(0, 0, 530, 467, 5054);
            AddImageTiled(10, 10, 510, 22, 2624);
            AddImageTiled(10, 292, 150, 75, 2624);
            AddImageTiled(165, 292, 355, 75, 2624);
            AddImageTiled(10, 37, 200, 250, 2624);
            AddImageTiled(215, 37, 305, 250, 2624);
            AddAlphaRegion(10, 10, 510, 447);

            AddHtml(10, 12, 510, 20, "<BASEFONT COLOR=#00FFFF>NMS SPAWNER - Mobile Selection</BASEFONT>", false, false);

            AddHtml(10, 37, 200, 22, "<BASEFONT COLOR=#FFFF00>CATEGORIES</BASEFONT>", false, false);
            AddHtml(215, 37, 305, 22, "<BASEFONT COLOR=#FFFF00>SELECTIONS</BASEFONT>", false, false);

            // Count toggle button (cycles 1-5)
            AddButton(420, 422, 4005, 4007, GetButtonID(3, 0), GumpButtonType.Reply, 0);
            AddHtml(455, 425, 100, 18, String.Format("<BASEFONT COLOR=#00FFFF>COUNT: {0}</BASEFONT>", m_Count), false, false);

            // Save button ([spawngen savebyhand])
            AddButton(15, 372, 4005, 4007, GetButtonID(3, 1), GumpButtonType.Reply, 0);
            AddHtml(50, 375, 120, 18, "<BASEFONT COLOR=#00FFFF>SAVE BY HAND</BASEFONT>", false, false);

            // BuildWorld button ([buildworld])
            AddButton(15, 402, 4005, 4007, GetButtonID(3, 2), GumpButtonType.Reply, 0);
            AddHtml(50, 402, 120, 18, "<BASEFONT COLOR=#0080FF>BUILD WORLD</BASEFONT>", false, false);

            // Editor button
            AddButton(160, 372, 4005, 4007, GetButtonID(3, 4), GumpButtonType.Reply, 0);
            AddHtml(205, 375, 80, 18, "<BASEFONT COLOR=#00FF80>EDITOR</BASEFONT>", false, false);

            // Clear all button
            AddButton(410, 372, 4005, 4007, GetButtonID(3, 3), GumpButtonType.Reply, 0);
            AddHtml(445, 375, 80, 18, "<BASEFONT COLOR=#FF8000>CLEAR ALL</BASEFONT>", false, false);

            AddButton(15, 422, 4017, 4019, 0, GumpButtonType.Reply, 0);
            AddHtml(50, 425, 150, 18, "<BASEFONT COLOR=#FF0000>EXIT</BASEFONT>", false, false);

            CreateCategoryList();

            if (m_Category != NMSCategory.None)
                CreateMobileList();
        }

        private void CreateCategoryList()
        {
            string[] categories = { "Monsters #1", "Monsters #2", "Boss/Special/Event", "Animals", "Vendor/NPCs", "DarkMoor", "Others" };
            NMSCategory[] categoryEnums = { NMSCategory.Monsters, NMSCategory.Monsters2, NMSCategory.BossSpecialEvent,
                                          NMSCategory.Animals, NMSCategory.VendorNPCs, NMSCategory.DarkMoor, NMSCategory.Others };

            for (int i = 0; i < categories.Length; i++)
            {
                AddButton(15, 67 + (i * 20), 4005, 4007, GetButtonID(0, i), GumpButtonType.Reply, 0);
                AddHtml(50, 70 + (i * 20), 150, 18, String.Format("<BASEFONT COLOR=#00FFFF>{0}</BASEFONT>", categories[i]), false, false);
            }
        }

        private void CreateMobileList()
        {
            string[] mobiles = GetMobilesForCategory(m_Category);
            int startIndex = m_Page * 10;
            int endIndex = Math.Min(startIndex + 10, mobiles.Length);

            // Add navigation buttons if needed
            if (m_Page > 0)
            {
                AddButton(470, 37, 4014, 4015, GetButtonID(2, 0), GumpButtonType.Reply, 0); // Previous page
            }

            if (endIndex < mobiles.Length)
            {
                AddButton(490, 37, 4005, 4007, GetButtonID(2, 1), GumpButtonType.Reply, 0); // Next page
            }

            for (int i = startIndex; i < endIndex; i++)
            {
                int displayIndex = i - startIndex;
                AddButton(220, 67 + (displayIndex * 20), 4005, 4007, GetButtonID(1, i), GumpButtonType.Reply, 0);
                AddHtml(255, 70 + (displayIndex * 20), 250, 18, String.Format("<BASEFONT COLOR=#FFFFFF>{0}</BASEFONT>", mobiles[i]), false, false);
            }

            // Show current page info
            AddHtml(10, 302, 150, 25, String.Format("<BASEFONT COLOR=#FFFF00>Page {0}</BASEFONT>", m_Page + 1), false, false);
        }

        private string[] GetMobilesForCategory(NMSCategory category)
        {
            switch (category)
            {
                case NMSCategory.Monsters:
                    return new string[] {
                        // Undead
                        "Spectre", "Wraith", "Shroud", "Spirit", "Zombie",
                        "Skeleton", "FrailSkeleton", "Ghoul", "Mummy", "Wight",
                        // Humanoids
                        "Orc", "Goblin", "Hobgoblin", "Bugbear", "Gnoll",
                        // Giants
                        "Troll", "Ogre", "Cyclops",
                        // Bugs
                        "GiantSpider", "Scorpion", "Mantis", "Tarantula",
                        // Reptiles
                        "Alligator", "GiantLizard", "Snake", "GiantToad",
                        // Slimy
                        "Slime", "GreenSlime", "GiantLeech",
                        // Plants
                        "Bogling", "BogThing", "Fungal",
                        // Lesser Elementals
                        "BloodSpawn", "WaterSpawn"
                    };

                case NMSCategory.Monsters2:
                    return new string[] {
                        // Undead
                        "BoneKnight", "BoneMagi", "BoneSlasher", "DeadKnight", "DeadWizard",
                        "DarkReaper", "GraveSeeker", "Lich", "SkeletalKnight", "SkeletalMage",
                        "SkeletalSamurai", "SkeletalWizard", "SoulReaper", "WailingBanshee", "Vampire",

                        // Humanoids
                        "OrcCaptain", "OrcishMage", "OrkMage", "OrkMonks",

                        // Giants
                        "Ettin", "EttinShaman", "ArcticEttin", "OgreLord", "OgreMagi",
                        "ArcticOgreLord", "HillGiant", "HillGiantShaman", "ShamanicCyclops",

                        // Dragons/Drakes
                        "Drake", "AbysmalDrake", "SeaDrake", "BabyDragon", "FireWyrmling",

                        // Elementals
                        "BloodElemental", "FireElemental", "IceElemental", "PoisonElemental", "LightningElemental",
                        "MagmaElemental", "LavaElemental", "ToxicElemental", "CinderElemental",

                        // Demons
                        "BloodDemon", "BoneDemon", "FireDemon", "ShadowDemon", "Fiend",
                        "Ifreet", "Afreet", "Succubus", "ShadowHound", "LesserDemon",

                        // Reptiles
                        "Basilisk", "GiantSerpent", "SwampGator", "Ridgeback", "SavageRidgeback",
                        "Serpentaur", "GiantAdder", "LavaSerpent", "IceSerpent", "Toraxen"
                    };

                case NMSCategory.BossSpecialEvent:
                    return new string[] {
                        // Supreme Dragons
                        "AncientWyrm", "ShadowWyrm", "WhiteWyrm", "Dragon", "ElderDragon",
                        "DragonKing", "VoidDragon", "RadiationDragon", "SkeletalDragon", "ZombieDragon",

                        // Primeval Dragons
                        "PrimevalDragon", "PrimevalFireDragon", "PrimevalRedDragon", "PrimevalBlackDragon",
                        "PrimevalAbysmalDragon", "PrimevalRoyalDragon", "PrimevalNightDragon", "PrimevalStygianDragon",

                        // Titanic Undead
                        "AncientLich", "DemiLich", "Dracolich", "TitanLich", "BloodLichMonarch",
                        "Dracula", "VampireLord", "VampirePrince",

                        // Supreme Demons
                        "Balron", "Archfiend", "Satan", "BloodDemigod", "Daemon",
                        "MutantDaemon", "Marilith",

                        // Colossal Giants
                        "ElderTitan", "StormGiant", "Titan", "TitanWarrior", "TitanKing",

                        // Supreme Elementals
                        "CrystalGoliath", "IceColossus", "ToxicElemental", "LavaElemental",

                        // Other Boss Creatures
                        "Leviathan", "Kraken", "SeaSerpent", "Jormungand", "Titanoboa",
                        "Tyraosaur", "Megalania", "Megalodon"
                    };

                case NMSCategory.Animals:
                    return new string[] {
                        // Farm Animals
                        "Sheep", "Cow", "Bull", "Goat", "Pig", "Chicken",
                        "Llama", "PackHorse", "PackLlama", "PackMule",

                        // Mounts
                        "Horse", "DesertOstard", "ForestOstard", "FrenziedOstard", "SnowOstard",
                        "RidableLlama", "GiantHawk", "GiantRaven", "Roc", "YoungRoc",

                        // Canines
                        "Dog", "TimberWolf", "GreyWolf", "DireWolf", "BlackWolf",
                        "WhiteWolf", "WinterWolf", "Jackal", "Fox",

                        // Felines
                        "Cat", "Cougar", "Panther", "Tiger", "WhiteTiger",
                        "Jaguar", "SnowLeopard", "SnowLion", "Bobcat", "CragCat",

                        // Bears
                        "BlackBear", "BrownBear", "GrizzlyBear", "PolarBear", "KodiakBear",
                        "DireBear", "GreatBear", "Panda", "Owlbear",

                        // Rodents & Small Creatures
                        "Rabbit", "JackRabbit", "WhiteRabbit", "Rat", "GiantRat",
                        "Squirrel", "Ferret", "Weasel", "Mouse", "Bat",
                        "GiantBat", "VampireBat", "AlbinoBat", "FireBat", "Stirge",

                        // Birds
                        "Eagle", "Hawk", "Bird", "Crane", "TropicalBird",
                        "DesertBird", "SwampBird", "AxeBeak", "GoldenHen",

                        // Large Animals & Boars
                        "Boar", "DireBoar", "Hind", "GreatHart", "Antelope",
                        "MountainGoat", "Zebra", "Elephant", "Mammoth",

                        // Aquatic
                        "Dolphin", "SeaHorse", "Walrus",

                        // Other
                        "Ape", "Gorilla", "Ramadon", "Tuskadon", "Grum"
                    };

                case NMSCategory.VendorNPCs:
                    return new string[] {
                        // Guards & Protectors
                        "TownGuard", "Guard", "CityGuard", "PaladinGuard", "WarriorGuard",
                        "ArcherGuard", "MageGuard", "RoyalGuard", "EliteGuard", "CaptainGuard",
                        "BlueGuard", "MercenaryGuard", "OrphanGuard",

                        // Healers & Religious
                        "Healer", "WanderingHealer", "EvilHealer", "FortuneTeller", "HolyMage",
                        "Monk", "KeeperOfChivalry", "Priest", "Druid", "DruidTree",

                        // Shopkeepers & Craftsmen
                        "Vendor", "Butcher", "Baker", "Tailor", "Blacksmith",
                        "Alchemist", "Mage", "InnKeeper", "Provisioner", "Carpenter",
                        "Bowyer", "Tinker", "Scribe", "Jeweler", "Armorer",
                        "Weaponsmith", "LeatherWorker", "Tanner", "Weaver", "Glassblower",
                        "StoneCrafter", "Shipwright", "Miller", "Miner", "IronWorker",
                        "Herbalist", "Veterinarian", "AnimalTrainer", "GypsyAnimalTrainer",

                        // Service Providers
                        "Banker", "GypsyBanker", "Minter", "RealEstateBroker", "Architect",
                        "Mapmaker", "Sage", "Necromancer", "NecroMage", "Enchanter",
                        "HairStylist", "CustomHairstylist", "Rancher", "Shepherd", "Farmer",
                        "Fisherman", "GypsyLady", "GypsyMaiden", "Vagabond", "Thief",
                        "Furtrader", "VarietyDealer", "EtherealDealer", "Witches", "Undertaker",

                        // Entertainment & Leisure
                        "Bard", "Jester", "Actor", "Artist", "Cook",
                        "Barkeeper", "TavernKeeper", "Waiter", "Chickenfarmer", "Beekeeper",

                        // Town Folk & Citizens
                        "Citizens", "Adventurersnew", "TavernPatrons", "TavernPatron",
                        "Warriors", "TradesmanSmith", "TradesmanMiner", "TradesmanLogger",
                        "TradesmanCook", "TradesmanLeather", "TradesmanAlchemist",

                        // Guild Masters
                        "BlacksmithGuildmaster", "TailorGuildmaster", "CarpenterGuildmaster",
                        "TinkerGuildmaster", "AlchemistGuildmaster", "MageGuildmaster",
                        "NecromancerGuildmaster", "HealerGuildmaster", "MerchantGuildmaster",
                        "MinerGuildmaster", "FisherGuildmaster", "RangerGuildmaster",
                        "ArcherGuildmaster", "WarriorGuildmaster", "ThiefGuildmaster",
                        "AssassinGuildmaster", "BardGuildmaster", "DruidGuildmaster",
                        "CulinaryGuildmaster", "LibrarianGuildmaster", "CartographersGuildmaster",

                        // Teachers & Trainers
                        "Teacher_Power", "Teacher_Wonderous", "Teacher_Exalted",
                        "Teacher_Legendary", "Teacher_Mythical", "TrainingSingle",
                        "TrainingBow", "TrainingFishing", "TrainingMagery",

                        // Special Characters
                        "Devon", "Garth", "Kylearan", "Roscoe", "Xardok",
                        "GodOfCourage", "MadGodPriest", "EpicCharacter", "DungeonGuide",

                        // Other Services
                        "Porter", "PackBeast", "Courier"
                    };

                case NMSCategory.DarkMoor:
                    return new string[] {
                        // Ents & Nature Spirits
                        "GaiaEnt", "AncientGaiaEnt",

                        // Titans
                        "TitanWarrior", "TitanKing", "TitanGod",

                        // Sphinx
                        "HolySphinx", "GodlySphinx", "Sphinxacolyte",

                        // Legendary Creatures
                        "SilverCrow", "RoyalKnight", "LoveAngel", "Leprechaun",

                        // Lizardmen Tribes
                        "Reptalar", "ReptalarChieftain", "ReptalarShaman",
                        "Haptah", "HaptahArcher", "HaptahMage",
                        "Meepter", "MeepterMage", "Slith", "SlithArcher", "Slither",

                        // Unique NPCs
                        "Changeling", "Meduso", "CaptainBloodyRum", "MotherGorilla",
                        "MommaBear", "CujoWolf", "WeightLifter", "SuperMob",

                        // Magical Creatures
                        "HarmlessBunny", "TheCuteFluffyBunny", "PurpleChicken",
                        "HealingDragon", "SoulOfIce", "AquaticMist",

                        // Special Mounts
                        "RainbowUnicorn", "RainbowKirin", "RainbowHiryu",
                        "RainbowCuSidhe", "RainbowChargerOfTheFallen",

                        // Elementals & Guardians
                        "GraniteElemental",

                        // Special Creatures
                        "Wildebeest", "Arcanist", "EyesSlave", "SpeakingSign"
                    };

                case NMSCategory.Others:
                    return new string[] {
                        // Special Characters
                        "Jester", "Bard", "Beggar", "Peasant", "Noble",
                        "Merchant", "Pirate", "Ninja", "Samurai", "Executioner",
                        "Child", "Bride", "Groom", "Preacher",

                        // Unique NPCs
                        "Wench", "LadyLuck",
                        
                        // Mining System
                        "MineSpirit"
                    };

                default:
                    return new string[0];
            }
        }

        private static int GetButtonID(int type, int index)
        {
            return 1 + type + (index * 7);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID == 0) // Exit
                return;

            int buttonID = info.ButtonID - 1;
            int type = buttonID % 7;
            int index = buttonID / 7;

            switch (type)
            {
                case 0: // Category selected
                    if (index >= 0 && index <= 6)
                    {
                        NMSCategory selectedCategory = (NMSCategory)(index + 1);
                        m_From.SendGump(new NMSSpawnerGump(m_From, selectedCategory, 0, m_Count));
                    }
                    break;

                case 1: // Mobile selected
                    string[] mobiles = GetMobilesForCategory(m_Category);
                    if (index >= 0 && index < mobiles.Length)
                    {
                        string mobileType = mobiles[index];
                        m_From.SendMessage(String.Format("Selected: {0}. Target location to spawn.", mobileType));
                        m_From.Target = new NMSSpawnerTarget(mobileType, m_Count);
                    }
                    break;

                case 2: // Page navigation
                    if (index == 0 && m_Page > 0) // Previous page
                    {
                        m_From.SendGump(new NMSSpawnerGump(m_From, m_Category, m_Page - 1, m_Count));
                    }
                    else if (index == 1) // Next page
                    {
                        string[] categoryMobiles = GetMobilesForCategory(m_Category);
                        int maxPage = (categoryMobiles.Length - 1) / 10;
                        if (m_Page < maxPage)
                        {
                            m_From.SendGump(new NMSSpawnerGump(m_From, m_Category, m_Page + 1, m_Count));
                        }
                    }
                    break;

                case 3: // Footer buttons
                    if (index == 0) // Count toggle (1-5)
                    {
                        int newCount = m_Count + 1;
                        if (newCount > 5) newCount = 1;
                        m_From.SendGump(new NMSSpawnerGump(m_From, m_Category, m_Page, newCount));
                    }
                    else if (index == 1) // Save by hand
                    {
                        // Show command in chat and execute
                        m_From.Say("[spawngen savebyhand");
                        Server.Commands.CommandSystem.Handle(m_From, "[spawngen savebyhand");

                        m_From.SendMessage("Saved hand-placed spawners to SpawnsByHand.map");
                        m_From.SendGump(new NMSSpawnerGump(m_From, m_Category, m_Page, m_Count));
                    }
                    else if (index == 2) // BuildWorld
                    {
                        // Show command in chat and execute
                        m_From.Say("[buildworld");
                        Server.Commands.CommandSystem.Handle(m_From, "[buildworld");

                        m_From.SendMessage("Build World command executed");
                        // Don't reopen gump as buildworld opens its own gump
                    }
                    else if (index == 3) // Clear all
                    {
                        // Show command in chat and execute
                        m_From.Say("[clearall");
                        Server.Commands.CommandSystem.Handle(m_From, "[clearall");

                        m_From.SendMessage("Clear all command executed");
                        // Don't reopen gump as world is being cleared
                    }
                    else if (index == 4) // Editor
                    {
                        // Show command in chat and execute
                        m_From.Say("[SpawnEditor");
                        Server.Commands.CommandSystem.Handle(m_From, "[SpawnEditor");

                        m_From.SendMessage("Opened Spawn Editor");
                        m_From.SendGump(new NMSSpawnerGump(m_From, m_Category, m_Page, m_Count));
                    }
                    break;
            }
        }
    }

    public class NMSSpawnerTarget : Target
    {
        private string m_MobileType;
        private int m_Count;

        public NMSSpawnerTarget(string mobileType, int count) : base(12, true, TargetFlags.None)
        {
            m_MobileType = mobileType;
            m_Count = count;
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            IPoint3D p = targeted as IPoint3D;

            if (p == null)
                return;

            if (targeted is Item)
            {
                p = ((Item)targeted).GetWorldTop();
            }
            else if (targeted is Mobile)
            {
                p = ((Mobile)targeted).Location;
            }

            Point3D location = new Point3D(p);
            Map map = from.Map;

            if (map == null || map == Map.Internal)
                return;

            // Special handling for MineSpirit - create directly as Mobile, not spawner
            if (m_MobileType == "MineSpirit")
            {
                try
                {
                    Type type = ScriptCompiler.FindTypeByName(m_MobileType);
                    if (type == null)
                    {
                        from.SendMessage(String.Format("Mobile type '{0}' not found.", m_MobileType));
                        return;
                    }

                    if (!(typeof(Mobile).IsAssignableFrom(type)))
                    {
                        from.SendMessage(String.Format("Type '{0}' is not a mobile.", m_MobileType));
                        return;
                    }

                    // Create MineSpirit directly as a Mobile
                    Mobile mineSpirit = Activator.CreateInstance(type) as Mobile;
                    if (mineSpirit != null)
                    {
                        // Configure MineSpirit properties
                        if (mineSpirit is MineSpirit)
                        {
                            MineSpirit spirit = mineSpirit as MineSpirit;
                            spirit.Hidden = true;
                            spirit.Frozen = true;
                            spirit.CantWalk = true;
                            spirit.Blessed = true;
                            // Default to IronOre - admin can configure via [props
                            spirit.OreType = typeof(IronOre);
                            spirit.ReqSkill = 50.0;
                            spirit.MinSkill = 50.0;
                            spirit.MaxSkill = 120.0;
                            spirit.Range = 3; // 3 tile radius
                        }

                        mineSpirit.MoveToWorld(location, map);
                        from.SendMessage(String.Format("MineSpirit created at {0}. Use [props to configure ore type and skill requirements.", location));
                        from.SendMessage("Note: MineSpirit must be configured with OreType (e.g., TitaniumOre) to work properly.");
                    }
                    else
                    {
                        from.SendMessage(String.Format("Failed to create {0}.", m_MobileType));
                    }
                }
                catch (Exception e)
                {
                    from.SendMessage(String.Format("Error creating MineSpirit: {0}", e.Message));
                }
                return;
            }

            // Create only a PremiumSpawner at the location for other mobiles
            try
            {
                // Verify the mobile type exists before creating spawner
                Type type = ScriptCompiler.FindTypeByName(m_MobileType);
                if (type == null)
                {
                    from.SendMessage(String.Format("Mobile type '{0}' not found.", m_MobileType));
                    return;
                }

                if (!(typeof(Mobile).IsAssignableFrom(type)))
                {
                    from.SendMessage(String.Format("Type '{0}' is not a mobile.", m_MobileType));
                    return;
                }

                // Create a PremiumSpawner at the location with specified count
                PremiumSpawner spawner = new PremiumSpawner(m_MobileType);
                spawner.Count = m_Count; // Set the spawn count
                spawner.MoveToWorld(location, map);

                from.SendMessage(String.Format("PremiumSpawner for {0} (Count: {1}) created at {2}. Mobile will spawn according to spawner settings.", m_MobileType, m_Count, location));
            }
            catch (Exception e)
            {
                from.SendMessage(String.Format("Error creating spawner for {0}: {1}", m_MobileType, e.Message));
            }
        }
    }
}

using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Targeting;
using Server.Items;

namespace Server.Gumps
{
    public class ItemSpawnerGump : Gump
    {
        private Mobile m_From;
        private ItemCategory m_Category;
        private int m_Page;
        private int m_Count;
        private bool m_Persistent;

        public ItemSpawnerGump(Mobile from, ItemCategory category, int page, int count)
            : this(from, category, page, count, false)
        {
        }

        public ItemSpawnerGump(Mobile from, ItemCategory category, int page, int count, bool persistent)
            : base(50, 50)
        {
            m_From = from;
            m_Category = category;
            m_Page = page;
            m_Count = count;
            m_Persistent = persistent;

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);

            // Background
            AddBackground(0, 0, 620, 450, 9270);
            AddAlphaRegion(10, 10, 600, 430);

            // Title
            AddHtml(10, 10, 300, 25, String.Format("<BASEFONT COLOR=#FFFFFF><CENTER>ItemSpawner - {0}</CENTER></BASEFONT>", m_Category.ToString()), false, false);

            // Category buttons
            CreateCategoryList();

            // Item list
            if (m_Category != ItemCategory.None)
                CreateItemList();

            // Footer controls
            CreateFooter();
        }

        private void CreateCategoryList()
        {
            string[] categories = { "Decorations", "Construction", "Resources", "Doors" };
            ItemCategory[] categoryEnums = { ItemCategory.Decorations, ItemCategory.Construction, ItemCategory.Resources, ItemCategory.Doors };

            for (int i = 0; i < categories.Length; i++)
            {
                AddButton(15, 67 + (i * 20), 4005, 4007, GetButtonID(0, i), GumpButtonType.Reply, 0);
                AddHtml(50, 70 + (i * 20), 150, 18, String.Format("<BASEFONT COLOR=#00FFFF>{0}</BASEFONT>", categories[i]), false, false);
            }
        }

        private void CreateItemList()
        {
            string[] items = GetItemsForCategory(m_Category);
            int startIndex = m_Page * 10;
            int endIndex = Math.Min(startIndex + 10, items.Length);

            // Add navigation buttons if needed
            if (m_Page > 0)
            {
                AddButton(470, 37, 4014, 4015, GetButtonID(2, 0), GumpButtonType.Reply, 0); // Previous page
            }

            if (endIndex < items.Length)
            {
                AddButton(495, 37, 4005, 4007, GetButtonID(2, 1), GumpButtonType.Reply, 0); // Next page
            }

            for (int i = startIndex; i < endIndex; i++)
            {
                int displayIndex = i - startIndex;
                AddButton(220, 67 + (displayIndex * 20), 4005, 4007, GetButtonID(1, i), GumpButtonType.Reply, 0);
                AddHtml(255, 70 + (displayIndex * 20), 250, 18, String.Format("<BASEFONT COLOR=#FFFFFF>{0}</BASEFONT>", items[i]), false, false);
            }

            // Show current page info
            AddHtml(10, 302, 150, 25, String.Format("<BASEFONT COLOR=#FFFF00>Page {0}</BASEFONT>", m_Page + 1), false, false);
        }

        private void CreateFooter()
        {
            // Count toggle button
            AddButton(220, 350, 4005, 4007, GetButtonID(3, 0), GumpButtonType.Reply, 0);
            AddHtml(255, 353, 100, 18, String.Format("<BASEFONT COLOR=#FFFF00>QTD: {0}</BASEFONT>", m_Count), false, false);

            // Persistent toggle button
            AddButton(350, 350, 4005, 4007, GetButtonID(3, 1), GumpButtonType.Reply, 0);
            AddHtml(385, 353, 100, 18, String.Format("<BASEFONT COLOR=#00FF00>PERSIST: {0}</BASEFONT>", m_Persistent ? "ON" : "OFF"), false, false);

            // Unpersist button
            AddButton(220, 380, 4005, 4007, GetButtonID(3, 2), GumpButtonType.Reply, 0);
            AddHtml(255, 383, 120, 18, "<BASEFONT COLOR=#FFA500>UNPERSIST ITEM</BASEFONT>", false, false);

            // Close button
            AddButton(15, 422, 4017, 4019, 0, GumpButtonType.Reply, 0);
            AddHtml(50, 425, 150, 18, "<BASEFONT COLOR=#FF0000>Close</BASEFONT>", false, false);
        }

        private string[] GetItemsForCategory(ItemCategory category)
        {
            switch (category)
            {
                case ItemCategory.Decorations:
                    return new string[] {
                        // Natural Decorations
                        "WaterTile", "SwampTile", "LavaTile", "DirtPatch", "Web",
                        "WallBlood", "TatteredAncientMummyWrapping", "Pier",

                        // Statues & Sculptures
                        "MonsterStatueDeed", "SkullPole", "EvilIdolSkull", "DemonSkull",

                        // Lighting & Fixtures
                        "Futon", "ArtifactVase", "ArtifactLargeVase",

                        // House Signs & Decor
                        "HouseSign",

                        // Oriental Decorations
                        "OrientalItems",

                        // Pillows & Comfort
                        "Pillows",

                        // Evil Decorations
                        "EvilItems",

                        // Teleportation Items
                        "Moongate", "StrangePortal", "TeleportMagicStaff", "TeleportScroll",

                        // Gwenno Grave
                        "GwennoGraveAddon"
                    };

                case ItemCategory.Construction:
                    return new string[] {
                        // Walls
                        "ThinBrickWall", "ThinStoneWall", "WhiteStoneWall", "ThickGrayStoneWall", "DarkWoodWall",

                        // Floors
                        "Floors",

                        // Furniture - Chairs
                        "Chairs", "Stools", "Thrones", "Benchs",

                        // Furniture - Tables
                        "Tables", "WritingTable",

                        // Decorative Construction
                        "DecorativeShield", "DecorativeWeapon", "TallBanner", "Tapestry",
                        "PaintingPortraits", "CathedralWindows",

                        // Misc Construction
                        "Vase", "Screens", "MusicStand", "Obelisk", "Easle",
                        "BarrelParts", "MeltedWax", "Vines", "Statues",

                        // Signs
                        "Sign", "LocalizedSign",

                        // Ankhs
                        "Ankhs",

                        // Ruined Items
                        "RuinedItemSingle"
                    };

                case ItemCategory.Resources:
                    return new string[] {
                        // Ingots & Metals
                        "Ingots", "ShinySilverIngot", "RareMetals",

                        // Ores
                        "Ore", "CaddelliteOre",

                        // Gems & Blocks
                        "BlockofAmethyst", "BlockofCaddellite", "BlockofEmerald", "BlockofGarnet",
                        "BlockofIce", "BlockofJade", "BlockofMarble", "BlockofOnyx", "BlockofQuartz",
                        "BlockofRuby", "BlockofSapphire", "BlockofSpinel", "BlockofStarRuby", "BlockofTopaz",

                        // Scales & Crystals
                        "Scales", "HardScales", "HardCrystals",

                        // Reagents - Common
                        "Ginseng", "MandrakeRoot", "Nightshade", "Garlic", "SulfurousAsh",
                        "Bloodmoss", "BlackPearl", "SpidersSilk",

                        // Reagents - Necromancy
                        "BatWing", "GraveDust", "DaemonBone", "PigIron", "DeadWood",

                        // Reagents - Alchemy
                        "BeetleShell", "Brimstone", "ButterflyWings", "EyeOfToad", "FairyEgg",
                        "GargoyleEar", "MoonCrystal", "PixieSkull", "RedLotus", "SeaSalt",
                        "SilverWidow", "SwampBerries",

                        // Reagent Bags
                        "BagOfReagents", "BagOfNecroReagents", "BagOfAllReagents", "BagOfAlchemicReagents",

                        // Tailor Resources
                        "BoltOfCloth", "UncutCloth", "Cloth", "Cotton", "Wool", "Flax", "Silk",
                        "Hides", "Leathers", "Bone", "PolishedBone", "PolishedSkull",

                        // Special Skins
                        "DemonSkin", "DragonSkin", "NightmareSkin", "SerpentSkin", "TrollSkin", "UnicornSkin",

                        // Unique Resources
                        "DemigodBlood", "DemonClaw", "DragonBlood", "DragonTooth", "EnchantedSeaweed",
                        "GhostlyDust", "GoldenFeathers", "GoldenSerpentVenom", "LichDust", "MysticalTreeSap",
                        "PegasusFeather", "PhoenixFeather", "ReaperOil", "SilverSerpentVenom", "UnicornHorn"
                    };

                case ItemCategory.Doors:
                    return new string[] {
                        // Basic Doors
                        "Doors", "HouseDoors", "CraftDoor", "BasementDoor", "BasementDoorway",

                        // Special Doors
                        "SecretDoors", "PickableDoor", "KeywordDoor", "DoorSwitch", "DoorOpener",
                        "DoorStuck", "DoorBounce", "DoorTimeLord", "DoorRavendark",

                        // Gates & Barriers
                        "Portcullis", "SkullGate", "GateMoon", "SerpentPillars",

                        // Teleporters
                        "Teleporter", "TeleportTile", "DoorTeleporter", "ArgentrockTeleporter",
                        "NewPlayerTeleport", "RandomExit",

                        // Magical Doors/Mirrors
                        "MagicMirror", "PublicDoor"
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
                    if (index >= 0 && index <= 3)
                    {
                        ItemCategory selectedCategory = (ItemCategory)(index + 1);
                        m_From.SendGump(new ItemSpawnerGump(m_From, selectedCategory, 0, m_Count, m_Persistent));
                    }
                    break;

                case 1: // Item selected
                    string[] items = GetItemsForCategory(m_Category);
                    if (index >= 0 && index < items.Length)
                    {
                        string itemType = items[index];
                        string persistMsg = m_Persistent ? " (will persist)" : "";
                        m_From.SendMessage(String.Format("Selected: {0}. Target location to spawn.{1}", itemType, persistMsg));
                        m_From.Target = new ItemSpawnerTarget(itemType, m_Count, m_Persistent);
                    }
                    break;

                case 2: // Page navigation
                    if (index == 0 && m_Page > 0) // Previous page
                    {
                        m_From.SendGump(new ItemSpawnerGump(m_From, m_Category, m_Page - 1, m_Count, m_Persistent));
                    }
                    else if (index == 1) // Next page
                    {
                        string[] categoryItems = GetItemsForCategory(m_Category);
                        int maxPage = (categoryItems.Length - 1) / 10;
                        if (m_Page < maxPage)
                        {
                            m_From.SendGump(new ItemSpawnerGump(m_From, m_Category, m_Page + 1, m_Count, m_Persistent));
                        }
                    }
                    break;

                case 3: // Footer buttons
                    if (index == 0) // Count toggle
                    {
                        int newCount = m_Count + 1;
                        if (newCount > 5) newCount = 1;
                        m_From.SendGump(new ItemSpawnerGump(m_From, m_Category, m_Page, newCount, m_Persistent));
                    }
                    else if (index == 1) // Persistent toggle
                    {
                        bool newPersistent = !m_Persistent;
                        m_From.SendGump(new ItemSpawnerGump(m_From, m_Category, m_Page, m_Count, newPersistent));
                    }
                    else if (index == 2) // Unpersist item
                    {
                        m_From.SendMessage("Target the item you want to remove from persistence.");
                        m_From.Target = new UnpersistItemTarget();
                        // Don't reopen gump - let the target handle it
                    }
                    break;
            }
        }
    }
}

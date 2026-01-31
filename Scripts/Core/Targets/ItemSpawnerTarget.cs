using System;
using System.IO;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Targeting
{
    public class ItemSpawnerTarget : Target
    {
        private string m_ItemType;
        private int m_Count;
        private bool m_Persistent;

        public ItemSpawnerTarget(string itemType, int count, bool persistent)
            : base(12, true, TargetFlags.None)
        {
            m_ItemType = itemType;
            m_Count = count;
            m_Persistent = persistent;
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            IPoint3D p = targeted as IPoint3D;

            if (p == null)
            {
                from.SendMessage("Invalid target.");
                return;
            }

            Point3D location = new Point3D(p);
            Map map = from.Map;

            if (map == null || map == Map.Internal)
            {
                from.SendMessage("Invalid location.");
                return;
            }

            // Check if targeting a container
            if (targeted is Container)
            {
                Container container = (Container)targeted;
                SpawnItemsInContainer(from, container, m_ItemType, m_Count);
            }
            else
            {
                // Spawn items on ground
                SpawnItemsOnGround(from, location, map, m_ItemType, m_Count);
            }

            from.SendGump(new Server.Gumps.ItemSpawnerGump(from, Server.Commands.ItemCategory.Decorations, 0, m_Count));
        }

        private void SpawnItemsInContainer(Mobile from, Container container, string itemType, int count)
        {
            Type itemTypeObj = ScriptCompiler.FindTypeByName(itemType);

            if (itemTypeObj == null)
            {
                from.SendMessage("Item type '{0}' not found.", itemType);
                return;
            }

            for (int i = 0; i < count; i++)
            {
                try
                {
                    Item item = (Item)Activator.CreateInstance(itemTypeObj);
                    if (item != null)
                    {
                        container.AddItem(item);
                        from.SendMessage("Spawned {0} in container.", itemType);
                    }
                }
                catch (Exception ex)
                {
                    from.SendMessage("Error spawning item: {0}", ex.Message);
                    break;
                }
            }
        }

        private void SpawnItemsOnGround(Mobile from, Point3D location, Map map, string itemType, int count)
        {
            Type itemTypeObj = ScriptCompiler.FindTypeByName(itemType);

            if (itemTypeObj == null)
            {
                from.SendMessage("Item type '{0}' not found.", itemType);
                return;
            }

            for (int i = 0; i < count; i++)
            {
                try
                {
                    Item item = (Item)Activator.CreateInstance(itemTypeObj);
                    if (item != null)
                    {
                        // Find a valid location near the target
                        Point3D spawnLoc = FindValidLocation(location, map);

                        item.MoveToWorld(spawnLoc, map);
                        from.SendMessage("Spawned {0} at {1}.", itemType, spawnLoc);

                        // Save to decorate.cfg if persistent
                        if (m_Persistent)
                        {
                            SaveToDecorateCfg(item, spawnLoc, map);
                            from.SendMessage("Item saved to decorate.cfg for persistence.");
                        }

                        // Offset next item slightly if spawning multiple
                        if (count > 1 && i < count - 1)
                        {
                            location = new Point3D(location.X + Utility.RandomMinMax(-1, 1), location.Y + Utility.RandomMinMax(-1, 1), location.Z);
                        }
                    }
                }
                catch (Exception ex)
                {
                    from.SendMessage("Error spawning item: {0}", ex.Message);
                    break;
                }
            }
        }

        private Point3D FindValidLocation(Point3D location, Map map)
        {
            // Try the exact location first
            if (map.CanFit(location.X, location.Y, location.Z, 20, false, false, true))
                return location;

            // Search in a small radius for a valid location
            for (int x = location.X - 2; x <= location.X + 2; x++)
            {
                for (int y = location.Y - 2; y <= location.Y + 2; y++)
                {
                    if (map.CanFit(x, y, location.Z, 20, false, false, true))
                        return new Point3D(x, y, location.Z);
                }
            }

            // If no valid ground location, try above ground
            for (int z = location.Z + 1; z <= location.Z + 5; z++)
            {
                if (map.CanFit(location.X, location.Y, z, 20, false, false, true))
                    return new Point3D(location.X, location.Y, z);
            }

            // Return original location as fallback
            return location;
        }

        private void SaveToDecorateCfg(Item item, Point3D location, Map map)
        {
            try
            {
                string decoratePath = Path.Combine(Core.BaseDirectory, "Data", "Decoration", "NMS", "Common", "decorate.cfg");

                // Format: ItemType ItemID (Properties)
                // X Y Z
                string itemName = item.GetType().Name;
                int itemID = item.ItemID;

                // Build properties string
                string properties = "";
                if (item.Hue != 0)
                    properties += String.Format("Hue={0}; ", item.Hue);
                if (!String.IsNullOrEmpty(item.Name))
                    properties += String.Format("Name={0}; ", item.Name.Replace(";", ""));

                // Remove trailing semicolon and space
                if (properties.EndsWith("; "))
                    properties = properties.Substring(0, properties.Length - 2);

                // Format the line
                string line = "";
                if (!String.IsNullOrEmpty(properties))
                    line = String.Format("{0} {1} ({2})", itemName, itemID, properties);
                else
                    line = String.Format("{0} {1}", itemName, itemID);

                string fullLine = String.Format("{0}\r\n{1} {2} {3}\r\n", line, location.X, location.Y, location.Z);

                // Append to file
                File.AppendAllText(decoratePath, fullLine);
            }
            catch (Exception ex)
            {
                // Log error but don't crash
                Console.WriteLine("Error saving item to decorate.cfg: {0}", ex.Message);
            }
        }

        protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
        {
            from.SendGump(new Server.Gumps.ItemSpawnerGump(from, Server.Commands.ItemCategory.Decorations, 0, m_Count, m_Persistent));
        }
    }

    public class UnpersistItemTarget : Target
    {
        public UnpersistItemTarget() : base(12, false, TargetFlags.None)
        {
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            if (!(targeted is Item))
            {
                from.SendMessage("You must target an item.");
                return;
            }

            Item item = (Item)targeted;

            try
            {
                string decoratePath = Path.Combine(Core.BaseDirectory, "Data", "Decoration", "NMS", "Common", "decorate.cfg");

                if (!File.Exists(decoratePath))
                {
                    from.SendMessage("Decorate.cfg file not found.");
                    return;
                }

                // Read all lines
                string[] lines = File.ReadAllLines(decoratePath);
                bool found = false;
                System.Collections.Generic.List<string> newLines = new System.Collections.Generic.List<string>();

                // Process lines in pairs (item definition + coordinates)
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();

                    if (String.IsNullOrEmpty(line) || line.StartsWith("#") || line.StartsWith(";"))
                    {
                        // Keep comments and empty lines
                        newLines.Add(lines[i]);
                        continue;
                    }

                    // Check if this is an item definition line
                    if (i + 1 < lines.Length && IsItemDefinition(line))
                    {
                        string coordLine = lines[i + 1].Trim();

                        // Parse coordinates
                        string[] coords = coordLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (coords.Length >= 3)
                        {
                            try
                            {
                                int x = Int32.Parse(coords[0]);
                                int y = Int32.Parse(coords[1]);
                                int z = Int32.Parse(coords[2]);

                                // Check if this matches our item
                                if (x == item.X && y == item.Y && z == item.Z)
                                {
                                    // Found matching item - skip both lines
                                    from.SendMessage("Removed {0} from decorate.cfg persistence.", item.GetType().Name);
                                    found = true;
                                    i++; // Skip coordinate line too
                                    continue;
                                }
                            }
                            catch
                            {
                                // Invalid coordinate format, keep the lines
                            }
                        }

                        // Keep both lines
                        newLines.Add(lines[i]);
                        newLines.Add(lines[i + 1]);
                        i++; // Skip coordinate line
                    }
                    else
                    {
                        // Keep single line
                        newLines.Add(lines[i]);
                    }
                }

                if (found)
                {
                    // Write back the file without the removed item
                    File.WriteAllLines(decoratePath, newLines.ToArray());
                    from.SendMessage("Item removed from persistence. The item will not respawn on server restart.");
                }
                else
                {
                    from.SendMessage("Item not found in decorate.cfg. It may not be persistent or was manually added.");
                }
            }
            catch (Exception ex)
            {
                from.SendMessage("Error removing item from persistence: {0}", ex.Message);
            }
        }

        private bool IsItemDefinition(string line)
        {
            // Check if line looks like an item definition (TypeName ItemID or TypeName ItemID (properties))
            string[] parts = line.Split(new char[] { ' ' }, 2);
            if (parts.Length < 2)
                return false;

            // First part should be a type name, second should start with digits or have parentheses
            return parts[0].Length > 0 && (Char.IsDigit(parts[1][0]) || parts[1].Contains("("));
        }

        protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
        {
            from.SendMessage("Unpersist item cancelled.");
        }
    }
}

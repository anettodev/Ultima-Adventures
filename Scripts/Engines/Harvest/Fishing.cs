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
    /// <summary>
    /// Main fishing harvest system class that coordinates fishing mechanics across multiple partial classes.
    /// This class serves as the entry point for fishing operations and delegates to specialized
    /// partial classes for different aspects of fishing (resources, locations, mechanics).
    ///
    /// File Structure:
    /// - FishingSystem.cs: Core system initialization and basic harvest methods
    /// - FishingResources.cs: Mutation tables, item construction, and treasure generation
    /// - FishingLocations.cs: Location-specific fishing methods and checks
    /// </summary>
    public partial class Fishing : HarvestSystem
    {
        // Implementation details are now distributed across partial classes:
        // - FishingSystem.cs: Core system initialization and basic methods
        // - FishingResources.cs: Mutation tables, item construction, and treasure generation
        // - FishingLocations.cs: Location-specific fishing methods and checks
    }
}

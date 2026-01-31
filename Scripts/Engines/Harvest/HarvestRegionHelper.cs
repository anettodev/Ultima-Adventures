using System;
using Server;
using Server.Misc;
using Server.Regions;

namespace Server.Engines.Harvest
{
    /// <summary>
    /// Regions that provide special harvest bonuses or conversions
    /// </summary>
    public enum HarvestBonusRegion
    {
        None,
        MinesOfMorinia,           // 3x ore multiplier
        NecromancerRegion,       // Ebony log bonus
        ShipwreckGrotto,         // Nepturite conversion
        BarnacledCavern,         // Nepturite conversion
        SavageSeaDocks,          // Nepturite conversion
        SerpentSailDocks,        // Nepturite conversion
        AnchorRockDocks,         // Nepturite conversion
        KrakenReefDocks,         // Nepturite conversion
        ForgottenLighthouse,     // Nepturite conversion
        IslesOfDread,            // Felucca amount
        SavagedEmpire,           // Coal bonus
        IslandOfUmberVeil        // Zinc bonus
    }

    /// <summary>
    /// Helper class for determining harvest bonus regions
    /// </summary>
    public static class HarvestRegionHelper
    {
        /// <summary>
        /// Determines the harvest bonus region for a given location
        /// </summary>
        /// <param name="from">The mobile performing the harvest</param>
        /// <param name="reg">The region at the harvest location</param>
        /// <param name="map">The map</param>
        /// <param name="loc">The location</param>
        /// <returns>The harvest bonus region type</returns>
        public static HarvestBonusRegion GetBonusRegion(Mobile from, Region reg, Map map, Point3D loc)
        {
            if (reg.IsPartOf("the Mines of Morinia"))
                return HarvestBonusRegion.MinesOfMorinia;
            
            if (reg.IsPartOf(typeof(NecromancerRegion)))
                return HarvestBonusRegion.NecromancerRegion;
            
            if (reg.IsPartOf("Shipwreck Grotto"))
                return HarvestBonusRegion.ShipwreckGrotto;
            
            if (reg.IsPartOf("Barnacled Cavern"))
                return HarvestBonusRegion.BarnacledCavern;
            
            if (reg.IsPartOf("Savage Sea Docks"))
                return HarvestBonusRegion.SavageSeaDocks;
            
            if (reg.IsPartOf("Serpent Sail Docks"))
                return HarvestBonusRegion.SerpentSailDocks;
            
            if (reg.IsPartOf("Anchor Rock Docks"))
                return HarvestBonusRegion.AnchorRockDocks;
            
            if (reg.IsPartOf("Kraken Reef Docks"))
                return HarvestBonusRegion.KrakenReefDocks;
            
            if (reg.IsPartOf("the Forgotten Lighthouse"))
                return HarvestBonusRegion.ForgottenLighthouse;
            
            string world = Worlds.GetMyWorld(map, loc, loc.X, loc.Y);
            
            if (world == "the Isles of Dread")
                return HarvestBonusRegion.IslesOfDread;
            
            if (world == "the Savaged Empire")
                return HarvestBonusRegion.SavagedEmpire;
            
            if (world == "the Island of Umber Veil")
                return HarvestBonusRegion.IslandOfUmberVeil;
            
            return HarvestBonusRegion.None;
        }

        /// <summary>
        /// Checks if region is a sea region (for nepturite conversion)
        /// </summary>
        /// <param name="region">The harvest bonus region</param>
        /// <returns>True if it's a sea region</returns>
        public static bool IsSeaRegion(HarvestBonusRegion region)
        {
            return region == HarvestBonusRegion.ShipwreckGrotto ||
                   region == HarvestBonusRegion.BarnacledCavern ||
                   region == HarvestBonusRegion.SavageSeaDocks ||
                   region == HarvestBonusRegion.SerpentSailDocks ||
                   region == HarvestBonusRegion.AnchorRockDocks ||
                   region == HarvestBonusRegion.KrakenReefDocks ||
                   region == HarvestBonusRegion.ForgottenLighthouse;
        }
    }
}


using System;
using Server.Items;

namespace Server.Engines.Harvest
{
    /// <summary>
    /// Defines resource availability ranges based on rarity.
    /// Rarer resources have lower min/max availability when they spawn.
    /// These ranges can be used to modify bank initialization or resource distribution.
    /// </summary>
    public static class HarvestResourceAvailability
    {
        #region Ore Availability Ranges
        
        /// <summary>Iron ore availability - Common (highest availability)</summary>
        public const int IRON_ORE_MIN = 8;
        public const int IRON_ORE_MAX = 25;
        
        /// <summary>Dull copper ore availability - Common</summary>
        public const int DULL_COPPER_ORE_MIN = 6;
        public const int DULL_COPPER_ORE_MAX = 20;
        
        /// <summary>Copper ore availability - Common</summary>
        public const int COPPER_ORE_MIN = 6;
        public const int COPPER_ORE_MAX = 20;
        
        /// <summary>Bronze ore availability - Common</summary>
        public const int BRONZE_ORE_MIN = 5;
        public const int BRONZE_ORE_MAX = 18;
        
        /// <summary>Shadow iron ore availability - Uncommon</summary>
        public const int SHADOW_IRON_ORE_MIN = 4;
        public const int SHADOW_IRON_ORE_MAX = 15;
        
        /// <summary>Platinum ore availability - Uncommon</summary>
        public const int PLATINUM_ORE_MIN = 3;
        public const int PLATINUM_ORE_MAX = 12;
        
        /// <summary>Gold ore availability - Uncommon</summary>
        public const int GOLD_ORE_MIN = 3;
        public const int GOLD_ORE_MAX = 12;
        
        /// <summary>Agapite ore availability - Rare</summary>
        public const int AGAPITE_ORE_MIN = 2;
        public const int AGAPITE_ORE_MAX = 10;
        
        /// <summary>Verite ore availability - Rare</summary>
        public const int VERITE_ORE_MIN = 2;
        public const int VERITE_ORE_MAX = 8;
        
        /// <summary>Valorite ore availability - Rare</summary>
        public const int VALORITE_ORE_MIN = 2;
        public const int VALORITE_ORE_MAX = 8;
        
        /// <summary>Titanium ore availability - Very Rare</summary>
        public const int TITANIUM_ORE_MIN = 1;
        public const int TITANIUM_ORE_MAX = 6;
        
        /// <summary>Rosenium ore availability - Very Rare</summary>
        public const int ROSENIUM_ORE_MIN = 1;
        public const int ROSENIUM_ORE_MAX = 6;
        
        /// <summary>Nepturite ore availability - Very Rare (special location)</summary>
        public const int NEPTURITE_ORE_MIN = 1;
        public const int NEPTURITE_ORE_MAX = 5;
        
        #endregion
        
        #region Granite Availability Ranges
        
        /// <summary>Granite availability - Common</summary>
        public const int GRANITE_MIN = 8;
        public const int GRANITE_MAX = 25;
        
        /// <summary>Dull copper granite availability - Common</summary>
        public const int DULL_COPPER_GRANITE_MIN = 6;
        public const int DULL_COPPER_GRANITE_MAX = 20;
        
        /// <summary>Copper granite availability - Common</summary>
        public const int COPPER_GRANITE_MIN = 6;
        public const int COPPER_GRANITE_MAX = 20;
        
        /// <summary>Bronze granite availability - Common</summary>
        public const int BRONZE_GRANITE_MIN = 5;
        public const int BRONZE_GRANITE_MAX = 18;
        
        /// <summary>Shadow iron granite availability - Uncommon</summary>
        public const int SHADOW_IRON_GRANITE_MIN = 4;
        public const int SHADOW_IRON_GRANITE_MAX = 15;
        
        /// <summary>Platinum granite availability - Uncommon</summary>
        public const int PLATINUM_GRANITE_MIN = 3;
        public const int PLATINUM_GRANITE_MAX = 12;
        
        /// <summary>Gold granite availability - Uncommon</summary>
        public const int GOLD_GRANITE_MIN = 3;
        public const int GOLD_GRANITE_MAX = 12;
        
        /// <summary>Agapite granite availability - Rare</summary>
        public const int AGAPITE_GRANITE_MIN = 2;
        public const int AGAPITE_GRANITE_MAX = 10;
        
        /// <summary>Verite granite availability - Rare</summary>
        public const int VERITE_GRANITE_MIN = 2;
        public const int VERITE_GRANITE_MAX = 8;
        
        /// <summary>Valorite granite availability - Rare</summary>
        public const int VALORITE_GRANITE_MIN = 2;
        public const int VALORITE_GRANITE_MAX = 8;
        
        /// <summary>Titanium granite availability - Very Rare</summary>
        public const int TITANIUM_GRANITE_MIN = 1;
        public const int TITANIUM_GRANITE_MAX = 6;
        
        /// <summary>Rosenium granite availability - Very Rare</summary>
        public const int ROSENIUM_GRANITE_MIN = 1;
        public const int ROSENIUM_GRANITE_MAX = 6;
        
        /// <summary>Nepturite granite availability - Very Rare (special location)</summary>
        public const int NEPTURITE_GRANITE_MIN = 1;
        public const int NEPTURITE_GRANITE_MAX = 5;
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Gets availability range for ore type
        /// </summary>
        /// <param name="oreType">The ore type</param>
        /// <param name="min">Output parameter for minimum availability</param>
        /// <param name="max">Output parameter for maximum availability</param>
        public static void GetOreAvailability(Type oreType, out int min, out int max)
        {
            min = IRON_ORE_MIN;
            max = IRON_ORE_MAX;
            
            if (oreType == typeof(IronOre))
            {
                min = IRON_ORE_MIN;
                max = IRON_ORE_MAX;
            }
            else if (oreType == typeof(DullCopperOre))
            {
                min = DULL_COPPER_ORE_MIN;
                max = DULL_COPPER_ORE_MAX;
            }
            else if (oreType == typeof(CopperOre))
            {
                min = COPPER_ORE_MIN;
                max = COPPER_ORE_MAX;
            }
            else if (oreType == typeof(BronzeOre))
            {
                min = BRONZE_ORE_MIN;
                max = BRONZE_ORE_MAX;
            }
            else if (oreType == typeof(ShadowIronOre))
            {
                min = SHADOW_IRON_ORE_MIN;
                max = SHADOW_IRON_ORE_MAX;
            }
            else if (oreType == typeof(PlatinumOre))
            {
                min = PLATINUM_ORE_MIN;
                max = PLATINUM_ORE_MAX;
            }
            else if (oreType == typeof(GoldOre))
            {
                min = GOLD_ORE_MIN;
                max = GOLD_ORE_MAX;
            }
            else if (oreType == typeof(AgapiteOre))
            {
                min = AGAPITE_ORE_MIN;
                max = AGAPITE_ORE_MAX;
            }
            else if (oreType == typeof(VeriteOre))
            {
                min = VERITE_ORE_MIN;
                max = VERITE_ORE_MAX;
            }
            else if (oreType == typeof(ValoriteOre))
            {
                min = VALORITE_ORE_MIN;
                max = VALORITE_ORE_MAX;
            }
            else if (oreType == typeof(TitaniumOre))
            {
                min = TITANIUM_ORE_MIN;
                max = TITANIUM_ORE_MAX;
            }
            else if (oreType == typeof(RoseniumOre))
            {
                min = ROSENIUM_ORE_MIN;
                max = ROSENIUM_ORE_MAX;
            }
            else if (oreType == typeof(NepturiteOre))
            {
                min = NEPTURITE_ORE_MIN;
                max = NEPTURITE_ORE_MAX;
            }
        }
        
        /// <summary>
        /// Gets availability range for granite type
        /// </summary>
        /// <param name="graniteType">The granite type</param>
        /// <param name="min">Output parameter for minimum availability</param>
        /// <param name="max">Output parameter for maximum availability</param>
        public static void GetGraniteAvailability(Type graniteType, out int min, out int max)
        {
            min = GRANITE_MIN;
            max = GRANITE_MAX;
            
            if (graniteType == typeof(Granite))
            {
                min = GRANITE_MIN;
                max = GRANITE_MAX;
            }
            else if (graniteType == typeof(DullCopperGranite))
            {
                min = DULL_COPPER_GRANITE_MIN;
                max = DULL_COPPER_GRANITE_MAX;
            }
            else if (graniteType == typeof(CopperGranite))
            {
                min = COPPER_GRANITE_MIN;
                max = COPPER_GRANITE_MAX;
            }
            else if (graniteType == typeof(BronzeGranite))
            {
                min = BRONZE_GRANITE_MIN;
                max = BRONZE_GRANITE_MAX;
            }
            else if (graniteType == typeof(ShadowIronGranite))
            {
                min = SHADOW_IRON_GRANITE_MIN;
                max = SHADOW_IRON_GRANITE_MAX;
            }
            else if (graniteType == typeof(PlatinumGranite))
            {
                min = PLATINUM_GRANITE_MIN;
                max = PLATINUM_GRANITE_MAX;
            }
            else if (graniteType == typeof(GoldGranite))
            {
                min = GOLD_GRANITE_MIN;
                max = GOLD_GRANITE_MAX;
            }
            else if (graniteType == typeof(AgapiteGranite))
            {
                min = AGAPITE_GRANITE_MIN;
                max = AGAPITE_GRANITE_MAX;
            }
            else if (graniteType == typeof(VeriteGranite))
            {
                min = VERITE_GRANITE_MIN;
                max = VERITE_GRANITE_MAX;
            }
            else if (graniteType == typeof(ValoriteGranite))
            {
                min = VALORITE_GRANITE_MIN;
                max = VALORITE_GRANITE_MAX;
            }
            else if (graniteType == typeof(TitaniumGranite))
            {
                min = TITANIUM_GRANITE_MIN;
                max = TITANIUM_GRANITE_MAX;
            }
            else if (graniteType == typeof(RoseniumGranite))
            {
                min = ROSENIUM_GRANITE_MIN;
                max = ROSENIUM_GRANITE_MAX;
            }
            else if (graniteType == typeof(NepturiteGranite))
            {
                min = NEPTURITE_GRANITE_MIN;
                max = NEPTURITE_GRANITE_MAX;
            }
        }
        
        /// <summary>
        /// Calculates average availability for a resource type
        /// </summary>
        /// <param name="resourceType">The resource type</param>
        /// <returns>Average availability (min + max) / 2</returns>
        public static int GetAverageAvailability(Type resourceType)
        {
            int min, max;
            
            if (typeof(BaseOre).IsAssignableFrom(resourceType))
            {
                GetOreAvailability(resourceType, out min, out max);
            }
            else if (typeof(BaseGranite).IsAssignableFrom(resourceType))
            {
                GetGraniteAvailability(resourceType, out min, out max);
            }
            else
            {
                return 0;
            }
            
            return (min + max) / 2;
        }
        
        #endregion
    }
}


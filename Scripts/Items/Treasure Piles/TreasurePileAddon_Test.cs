// Test file to verify all TreasurePileAddon variations work correctly
// This file can be used for testing the consolidated addon system
// Run this during testing phase to ensure all variations load properly

using System;
using Server;
using Server.Items;

namespace Server.Items
{
    /// <summary>
    /// Test class to verify TreasurePileAddon variations work correctly.
    /// This class is for testing purposes only and should be removed after validation.
    /// </summary>
    public class TreasurePileAddonTest
    {
        /// <summary>
        /// Tests all treasure pile addon variations to ensure they can be created
        /// </summary>
        public static void TestAllVariations()
        {
            try
            {
                // Test all 8 variations
                var original = new TreasurePileAddon(TreasurePileAddonConstants.TreasurePileVariation.Original);
                var compact01 = new TreasurePileAddon(TreasurePileAddonConstants.TreasurePileVariation.Compact01);
                var extended02 = new TreasurePileAddon(TreasurePileAddonConstants.TreasurePileVariation.Extended02);
                var minimal03 = new TreasurePileAddon(TreasurePileAddonConstants.TreasurePileVariation.Minimal03);
                var large04 = new TreasurePileAddon(TreasurePileAddonConstants.TreasurePileVariation.Large04);
                var standard05 = new TreasurePileAddon(TreasurePileAddonConstants.TreasurePileVariation.Standard05);
                var medium06 = new TreasurePileAddon(TreasurePileAddonConstants.TreasurePileVariation.Medium06);
                var balanced07 = new TreasurePileAddon(TreasurePileAddonConstants.TreasurePileVariation.Balanced07);

                // Test deed creation
                var originalDeed = new TreasurePileAddonDeed(TreasurePileAddonConstants.TreasurePileVariation.Original);
                var compact01Deed = new TreasurePileAddonDeed(TreasurePileAddonConstants.TreasurePileVariation.Compact01);

                // Verify component counts
                bool originalCount = original.Components.Count == TreasurePileAddonConstants.COMPONENT_COUNTS[0];
                bool compact01Count = compact01.Components.Count == TreasurePileAddonConstants.COMPONENT_COUNTS[1];
                bool extended02Count = extended02.Components.Count == TreasurePileAddonConstants.COMPONENT_COUNTS[2];
                bool minimal03Count = minimal03.Components.Count == TreasurePileAddonConstants.COMPONENT_COUNTS[3];
                bool large04Count = large04.Components.Count == TreasurePileAddonConstants.COMPONENT_COUNTS[4];
                bool standard05Count = standard05.Components.Count == TreasurePileAddonConstants.COMPONENT_COUNTS[5];
                bool medium06Count = medium06.Components.Count == TreasurePileAddonConstants.COMPONENT_COUNTS[6];
                bool balanced07Count = balanced07.Components.Count == TreasurePileAddonConstants.COMPONENT_COUNTS[7];

                // Clean up test objects
                original.Delete();
                compact01.Delete();
                extended02.Delete();
                minimal03.Delete();
                large04.Delete();
                standard05.Delete();
                medium06.Delete();
                balanced07.Delete();
                originalDeed.Delete();
                compact01Deed.Delete();

                Console.WriteLine("TreasurePileAddon Test Results:");
                Console.WriteLine(String.Format("Original (26): {0}", originalCount));
                Console.WriteLine(String.Format("Compact01 (10): {0}", compact01Count));
                Console.WriteLine(String.Format("Extended02 (27): {0}", extended02Count));
                Console.WriteLine(String.Format("Minimal03 (9): {0}", minimal03Count));
                Console.WriteLine(String.Format("Large04 (33): {0}", large04Count));
                Console.WriteLine(String.Format("Standard05 (11): {0}", standard05Count));
                Console.WriteLine(String.Format("Medium06 (13): {0}", medium06Count));
                Console.WriteLine(String.Format("Balanced07 (15): {0}", balanced07Count));

                bool allTestsPassed = originalCount && compact01Count && extended02Count && minimal03Count &&
                                    large04Count && standard05Count && medium06Count && balanced07Count;

                Console.WriteLine(String.Format("All tests passed: {0}", allTestsPassed));
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("TreasurePileAddon test failed: {0}", ex.Message));
            }
        }
    }
}

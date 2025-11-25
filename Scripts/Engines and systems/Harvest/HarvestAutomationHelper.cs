using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Engines.Harvest
{
    /// <summary>
    /// Helper class for harvest automation integration.
    /// Extracted from HarvestSystem.cs to reduce code duplication.
    /// </summary>
    public static class HarvestAutomationHelper
    {
        /// <summary>
        /// Checks if mobile is an automated player
        /// </summary>
        /// <param name="from">The mobile to check</param>
        /// <returns>True if the mobile is an automated player</returns>
        public static bool IsAutomatedPlayer(Mobile from)
        {
            PlayerMobile pm = from as PlayerMobile;
            return pm != null && pm.GetFlag(PlayerFlag.IsAutomated);
        }

        /// <summary>
        /// Gets the PlayerMobile if automated, null otherwise
        /// </summary>
        /// <param name="from">The mobile to check</param>
        /// <returns>PlayerMobile if automated, null otherwise</returns>
        public static PlayerMobile GetAutomatedPlayer(Mobile from)
        {
            PlayerMobile pm = from as PlayerMobile;
            if (pm != null && pm.GetFlag(PlayerFlag.IsAutomated))
            {
                return pm;
            }
            return null;
        }

        /// <summary>
        /// Stops automation for player
        /// </summary>
        /// <param name="pm">The player mobile</param>
        public static void StopAutomation(PlayerMobile pm)
        {
            if (pm != null && pm.GetFlag(PlayerFlag.IsAutomated))
            {
                AdventuresAutomation.StopAction(pm);
            }
        }

        /// <summary>
        /// Clears harvest target and retries automation
        /// </summary>
        /// <param name="pm">The player mobile</param>
        /// <param name="system">The harvest system</param>
        public static void ClearTargetAndRetry(PlayerMobile pm, HarvestSystem system)
        {
            if (pm != null && pm.GetFlag(PlayerFlag.IsAutomated))
            {
                AdventuresAutomation.ClearHarvestTarget(pm);
                int delay = AdventuresAutomation.GetDelayForSystem(system);
                AdventuresAutomation.SetNextAction(pm, delay);
            }
        }

        /// <summary>
        /// Handles automation retry with delay
        /// </summary>
        /// <param name="pm">The player mobile</param>
        /// <param name="system">The harvest system</param>
        public static void RetryAutomation(PlayerMobile pm, HarvestSystem system)
        {
            if (pm != null && pm.GetFlag(PlayerFlag.IsAutomated))
            {
                int delay = AdventuresAutomation.GetDelayForSystem(system);
                AdventuresAutomation.SetNextAction(pm, delay);
            }
        }
    }
}


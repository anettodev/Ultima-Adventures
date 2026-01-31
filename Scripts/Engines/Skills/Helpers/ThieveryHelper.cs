using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.SkillHandlers
{
	/// <summary>
	/// Helper class for thievery-related skills (Stealing and Snooping).
	/// Extracted from Stealing.cs and Snooping.cs to eliminate code duplication
	/// and improve code organization and reusability.
	/// </summary>
	public static class ThieveryHelper
	{
		#region Target List Management

		/// <summary>
		/// Checks if a target is in the guarded targets set, or adds it if requested.
		/// Uses HashSet for O(1) lookup performance.
		/// </summary>
		/// <param name="targets">The set of guarded targets</param>
		/// <param name="target">The mobile to check or add</param>
		/// <param name="addToGuardList">If true, adds target to set and returns false. If false, checks if target is guarded.</param>
		/// <returns>True if target is not guarded (can proceed), false if guarded or being added to set</returns>
		public static bool CheckTarget(ref HashSet<Mobile> targets, Mobile target, bool addToGuardList)
		{
			if (targets == null)
				targets = new HashSet<Mobile>();

			if (addToGuardList)
			{
				targets.Add(target);
				return false;
			}
			else
			{
				return !targets.Contains(target);
			}
		}

		/// <summary>
		/// Clears the guarded targets set.
		/// </summary>
		/// <param name="targets">The set of guarded targets to clear</param>
		public static void WipeTargetList(ref HashSet<Mobile> targets)
		{
			if (targets != null)
				targets.Clear();
		}

		#endregion

		#region State Validation

		/// <summary>
		/// Checks if a mobile is in a blessed state that prevents thievery actions.
		/// </summary>
		/// <param name="from">The mobile attempting the action</param>
		/// <param name="actionName">The name of the action (for error message)</param>
		/// <returns>True if action is allowed, false if blessed state prevents it</returns>
		public static bool CheckBlessedState(Mobile from, string actionName)
		{
			if (from == null)
				return false;

			if (from.Blessed)
			{
				from.SendMessage("Você não pode " + actionName + " neste estado.");
				return false;
			}

			return true;
		}

		/// <summary>
		/// Checks if a mobile is a Citizens NPC (which has special permissions).
		/// </summary>
		/// <param name="from">The mobile to check</param>
		/// <returns>True if mobile is a Citizens NPC</returns>
		public static bool IsCitizensNPC(Mobile from)
		{
			return from is Citizens;
		}

		#endregion

		#region Range Validation

		/// <summary>
		/// Checks if a mobile is within range of a location.
		/// </summary>
		/// <param name="from">The mobile to check</param>
		/// <param name="location">The target location</param>
		/// <param name="range">The maximum range</param>
		/// <returns>True if within range</returns>
		public static bool IsInRange(Mobile from, Point3D location, int range)
		{
			if (from == null)
				return false;

			return from.InRange(location, range);
		}

		#endregion
	}
}


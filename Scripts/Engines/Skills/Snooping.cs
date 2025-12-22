using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Regions;
using System.Collections;
using System.Collections.Generic;

namespace Server.SkillHandlers
{
	/// <summary>
	/// Handles the Snooping skill functionality for peeking into containers.
	/// </summary>
	public class Snooping
	{
		#region Fields

		/// <summary>Set of mobiles that are currently guarding their possessions. Uses HashSet for O(1) lookup performance.</summary>
		static HashSet<Mobile> snoopingtargets = new HashSet<Mobile>();

		#endregion

		#region Initialization

		/// <summary>
		/// Configures the snooping skill handler.
		/// </summary>
		public static void Configure()
		{
			Container.SnoopHandler = new ContainerSnoopHandler(Container_Snoop);
		}

		#endregion

		#region Target Management

		/// <summary>
		/// Checks if a target is currently guarding their possessions, or adds them to the guard list.
		/// </summary>
		/// <param name="target">The mobile to check or add</param>
		/// <param name="check">If true, adds target to guard list. If false, checks if target is guarded.</param>
		/// <returns>True if target is not guarded (can proceed), false if guarded or being added to list</returns>
		public static bool CheckSnoopingTarget(Mobile target, bool check)
		{
			return ThieveryHelper.CheckTarget(ref snoopingtargets, target, check);
		}

		/// <summary>
		/// Clears the list of guarded targets.
		/// </summary>
		public static void WipeSnoopingList()
		{
			ThieveryHelper.WipeTargetList(ref snoopingtargets);
		}

		#endregion

		#region Validation

		/// <summary>
		/// Checks if snooping is allowed from the given mobile to the target mobile.
		/// </summary>
		/// <param name="from">The mobile attempting to snoop</param>
		/// <param name="to">The target mobile</param>
		/// <returns>True if snooping is allowed, false otherwise</returns>
		public static bool CheckSnoopAllowed(Mobile from, Mobile to)
		{
			if (!CheckSnoopingTarget(to, false))
			{
				from.SendMessage(ThieveryStringConstants.MSG_TARGET_GUARDING);
				return false;
			}

			if (ThieveryHelper.IsCitizensNPC(from))
				return true;

			if (!ThieveryHelper.CheckBlessedState(from, "bisbilhotar"))
				return false;

			if (to.Player)
				return from.CanBeHarmful(to, false, true); // normal restrictions

			Map map = from.Map;
			if (map != null && (map.Rules & MapRules.HarmfulRestrictions) == 0)
				return true; // felucca you can snoop anybody

			BaseCreature cret = to as BaseCreature;

			if (to.Body.IsHuman && (cret == null || (!cret.AlwaysAttackable && !cret.AlwaysMurderer)))
				return false; // in town we cannot snoop blue human npcs

			return true;
		}

		/// <summary>
		/// Checks if the root parent is alive and valid for snooping.
		/// </summary>
		/// <param name="root">The root parent mobile</param>
		/// <returns>True if root is null or alive, false if dead</returns>
		private static bool IsRootAlive(Mobile root)
		{
			return root == null || root.Alive;
		}

		/// <summary>
		/// Checks if the container belongs to a Citizens NPC.
		/// </summary>
		/// <param name="cont">The container to check</param>
		/// <returns>True if container belongs to Citizens NPC</returns>
		private static bool IsCitizensContainer(Container cont)
		{
			return cont.ParentEntity is Citizens;
		}

		/// <summary>
		/// Checks if the snooper has sufficient access level to snoop the target.
		/// </summary>
		/// <param name="from">The mobile attempting to snoop</param>
		/// <param name="root">The root parent mobile</param>
		/// <returns>True if access level check passes, false otherwise</returns>
		private static bool CheckAccessLevel(Mobile from, Mobile root)
		{
			if (root != null && root.AccessLevel > AccessLevel.Player && from.AccessLevel == AccessLevel.Player)
			{
				from.SendLocalizedMessage(500209); // You can not peek into the container.
				return false;
			}
			return true;
		}

		/// <summary>
		/// Broadcasts a notice to nearby players when snooping is detected.
		/// </summary>
		/// <param name="from">The mobile attempting to snoop</param>
		/// <param name="root">The target mobile being snooped</param>
		private static void BroadcastSnoopingNotice(Mobile from, Mobile root)
		{
			Map map = from.Map;
			if (map == null)
				return;

			string message = string.Format(ThieveryStringConstants.MSG_NOTICE_SNOOPING_FORMAT, from.Name, root.Name);

			IPooledEnumerable eable = map.GetClientsInRange(from.Location, ThieveryConstants.MESSAGE_BROADCAST_RANGE);

			foreach (NetState ns in eable)
			{
				if (ns.Mobile != from)
					ns.Mobile.SendMessage(message);
			}

			eable.Free();
		}

		/// <summary>
		/// Sets the appropriate gump ID for the container based on the root parent.
		/// </summary>
		/// <param name="cont">The container to set gump ID for</param>
		/// <param name="root">The root parent mobile</param>
		private static void SetContainerGumpID(Container cont, Mobile root)
		{
			cont.GumpID = ThieveryConstants.GUMP_ID_DEFAULT;
			if (root is PlayerMobile)
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB(root);
				if (DB != null && DB.Hue == ThieveryConstants.CHARACTER_DB_HUE_SPECIAL)
				{
					cont.GumpID = ThieveryConstants.GUMP_ID_SPECIAL_CHARACTER;
				}
			}
		}

		/// <summary>
		/// Handles the failure case when snooping is detected or skill check fails.
		/// </summary>
		/// <param name="from">The mobile attempting to snoop</param>
		/// <param name="root">The root parent mobile</param>
		private static void HandleSnoopingFailure(Mobile from, Mobile root)
		{
			from.SendLocalizedMessage(500210); // You failed to peek into the container.

			if (Utility.RandomDouble() < ThieveryConstants.SNOOP_GUARDED_CHANCE)
				CheckSnoopingTarget(root, true);

			if (from.Skills[SkillName.Hiding].Value / ThieveryConstants.HIDING_SKILL_DIVISOR < Utility.Random((int)ThieveryConstants.SNOOPING_SKILL_CHECK_MAX))
				from.RevealingAction();
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles the container snooping action.
		/// </summary>
		/// <param name="cont">The container to snoop</param>
		/// <param name="from">The mobile attempting to snoop</param>
		public static void Container_Snoop(Container cont, Mobile from)
		{
			if (from.AccessLevel > AccessLevel.Player || ThieveryHelper.IsInRange(from, cont.GetWorldLocation(), ThieveryConstants.CONTAINER_RANGE))
			{
				Mobile root = cont.RootParent as Mobile;

				if (!IsRootAlive(root))
					return;

				if (IsCitizensContainer(cont))
				{
					cont.DisplayTo(from);
					return;
				}

				if (!CheckAccessLevel(from, root))
					return;

				if (root != null && from.AccessLevel == AccessLevel.Player && !CheckSnoopAllowed(from, root))
				{
					from.SendLocalizedMessage(1001018); // You cannot perform negative acts on your target.
					return;
				}

				if (root != null && from.AccessLevel == AccessLevel.Player && from.Skills[SkillName.Snooping].Value < Utility.Random((int)ThieveryConstants.SNOOPING_SKILL_CHECK_MAX))
				{
					BroadcastSnoopingNotice(from, root);
				}

				if (from.AccessLevel == AccessLevel.Player)
					Titles.AwardKarma(from, ThieveryConstants.SNOOPING_KARMA_PENALTY, true);

				if (from.AccessLevel > AccessLevel.Player || from.CheckTargetSkill(SkillName.Snooping, cont, Utility.RandomMinMax(ThieveryConstants.SNOOPING_SKILL_CHECK_RANGE_MIN, ThieveryConstants.SNOOPING_SKILL_CHECK_RANGE_MAX), ThieveryConstants.SNOOPING_SKILL_CHECK_MAX))
				{
					if (cont is TrapableContainer && ((TrapableContainer)cont).ExecuteTrap(from))
						return;

					SetContainerGumpID(cont, root);
					cont.DisplayTo(from);
				}
				else
				{
					HandleSnoopingFailure(from, root);
				}
			}
			else
			{
				from.SendLocalizedMessage(500446); // That is too far away.
			}
		}

		#endregion
	}
}
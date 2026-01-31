using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Multis;
using Server.Mobiles;

namespace Server.Engines.Harvest
{
	/// <summary>
	/// Target handler for harvest system actions (mining, lumberjacking, fishing).
	/// Handles target selection and routes to appropriate system-specific handlers.
	/// </summary>
	public class HarvestTarget : Target
	{
		#region Fields

		private Item m_Tool;
		private HarvestSystem m_System;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new harvest target handler
		/// </summary>
		/// <param name="tool">The tool being used for harvesting</param>
		/// <param name="system">The harvest system (Mining, Lumberjacking, Fishing)</param>
		public HarvestTarget( Item tool, HarvestSystem system ) : base( -1, true, TargetFlags.None )
		{
			m_Tool = tool;
			m_System = system;

			DisallowMultis = true;
		}

		#endregion

		#region Target Handling

		/// <summary>
		/// Called when a target is selected by the player
		/// Routes to system-specific handlers based on harvest system type
		/// </summary>
		/// <param name="from">The mobile performing the action</param>
		/// <param name="targeted">The target object</param>
		protected override void OnTarget( Mobile from, object targeted )
		{
			if (m_System is Lumberjacking)
			{
				HandleLumberjackingTarget(from, targeted);
				return;
			}
			
			if (m_System is Mining)
			{
				HandleMiningTarget(from, targeted);
				return;
			}
			
			// Default: Start harvesting for other systems
			m_System.StartHarvesting( from, m_Tool, targeted );
		}

		#endregion

		#region System-Specific Handlers

		/// <summary>
		/// Handles targeting for lumberjacking system
		/// </summary>
		/// <param name="from">The mobile performing the action</param>
		/// <param name="targeted">The target object</param>
		private void HandleLumberjackingTarget(Mobile from, object targeted)
		{
			if (targeted is TombStone)
			{
				from.SendMessage(HarvestConstants.MSG_COLOR_ERROR, HarvestStringConstants.MSG_TOMBSTONE_PROTECTED);
				return;
			}
			
			if (targeted is IChopable)
			{
				((IChopable)targeted).OnChop(from);
				return;
			}
			
			if (targeted is IAxe && m_Tool is BaseAxe)
			{
				HandleAxeTarget(from, (IAxe)targeted, (Item)targeted);
				return;
			}
			
			if (targeted is ICarvable)
			{
				((ICarvable)targeted).Carve(from, (Item)m_Tool);
				return;
			}
			
			if (FurnitureAttribute.Check(targeted as Item))
			{
				DestroyFurniture(from, (Item)targeted);
				return;
			}
			
			// Default: Start harvesting
			m_System.StartHarvesting(from, m_Tool, targeted);
		}

		/// <summary>
		/// Handles targeting for mining system
		/// </summary>
		/// <param name="from">The mobile performing the action</param>
		/// <param name="targeted">The target object</param>
		private void HandleMiningTarget(Mobile from, object targeted)
		{
			if (targeted is TreasureMap)
			{
				((TreasureMap)targeted).OnBeginDig(from);
				return;
			}
			
			// Default: Start harvesting
			m_System.StartHarvesting(from, m_Tool, targeted);
		}

		/// <summary>
		/// Handles axe target interaction
		/// </summary>
		/// <param name="from">The mobile performing the action</param>
		/// <param name="obj">The IAxe target</param>
		/// <param name="item">The item target</param>
		private void HandleAxeTarget(Mobile from, IAxe obj, Item item)
		{
			if (!item.IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1062334); // This item must be in your backpack to be used.
				return;
			}
			
			if (obj.Axe(from, (BaseAxe)m_Tool))
			{
				from.PlaySound(HarvestConstants.SOUND_AXE_ACTION);
			}
		}

		#endregion

		#region Furniture Destruction

		/// <summary>
		/// Destroys furniture items that can be chopped with axes
		/// </summary>
		/// <param name="from">The mobile performing the action</param>
		/// <param name="item">The furniture item to destroy</param>
		private void DestroyFurniture( Mobile from, Item item )
		{
			if ( !from.InRange( item.GetWorldLocation(), HarvestConstants.FURNITURE_DESTROY_RANGE ) )
			{
				from.SendLocalizedMessage( 500446 ); // That is too far away.
				return;
			}
			else if ( !item.IsChildOf( from.Backpack ) && !item.Movable )
			{
				from.SendLocalizedMessage( 500462 ); // You can't destroy that while it is here.
				return;
			}

			from.SendLocalizedMessage( 500461 ); // You destroy the item.
			Effects.PlaySound( item.GetWorldLocation(), item.Map, HarvestConstants.SOUND_FURNITURE_DESTROY );

			if ( item is Container )
			{
				if ( item is TrapableContainer )
					(item as TrapableContainer).ExecuteTrap( from );

				((Container)item).Destroy();
			}
			else
			{
				item.Delete();
			}
		}

		#endregion
	}
}
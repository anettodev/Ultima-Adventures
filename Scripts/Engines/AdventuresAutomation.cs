using System; 
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.Items; 
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Targeting;
using Server.OneTime;
using Server.SkillHandlers;
using Server.Engines.Harvest;
using Server.Engines.Craft;


namespace Server.Items
{
	/// <summary>
	/// Automation system for player actions (fishing, mining, lumberjacking, crafting, etc.)
	/// Provides automated harvesting and crafting capabilities with configurable delays.
	/// </summary>
	public class AdventuresAutomation : Item, IOneTime
	{
		#region Static Fields

		/// <summary>
		/// Cached singleton instance of AdventuresAutomation item
		/// Eliminates need to iterate World.Items.Values for performance
		/// </summary>
		private static AdventuresAutomation s_Instance;

		private static Dictionary<PlayerMobile, Item> PlayerTool;
		private static Dictionary<PlayerMobile, Point3D> PlayerLoc;
		private static Dictionary<PlayerMobile, int> TaskNextAction;
		public static Dictionary<PlayerMobile, object> TaskTarget;
		private static Dictionary<PlayerMobile, HarvestSystem> TaskSystem;
		private static Dictionary<PlayerMobile, string> TaskString;

		/// <summary>
		/// Reverse lookup: Groups players by timer value for O(1) timer matching
		/// Key: Timer value, Value: List of players with that timer value
		/// </summary>
		private static Dictionary<int, List<PlayerMobile>> TimerReverseLookup;

		#endregion

		#region Instance Caching

		/// <summary>
		/// Gets the singleton AdventuresAutomation instance
		/// </summary>
		public static AdventuresAutomation Instance
		{
			get { return s_Instance; }
		}

		/// <summary>
		/// Sets the singleton instance (called when item is added to world)
		/// </summary>
		private static void SetInstance(AdventuresAutomation instance)
		{
			s_Instance = instance;
		}

		/// <summary>
		/// Clears the singleton instance (called when item is deleted)
		/// </summary>
		private static void ClearInstance()
		{
			s_Instance = null;
		}

		#endregion

		#region Delay Fields

		// Delay constants moved to AdventuresAutomationConstants.cs
		public static int fishingdelay = AdventuresAutomationConstants.DELAY_FISHING_SECONDS;
		public static int miningdelay = AdventuresAutomationConstants.DELAY_MINING_SECONDS;
		public static int lumberjackingdelay = AdventuresAutomationConstants.DELAY_LUMBERJACKING_SECONDS;
		public static int skinningdelay = AdventuresAutomationConstants.DELAY_SKINNING_SECONDS;
		public static int millingdelay = AdventuresAutomationConstants.DELAY_MILLING_SECONDS;
		public static int craftingdelay = AdventuresAutomationConstants.DELAY_CRAFTING_SECONDS;

		#endregion

		#region Timer Fields

		public static int globaltasktimer;
		private static DateTime m_LastStaffNotification = DateTime.MinValue;

		#endregion

		#region Instance Fields

		private int m_OneTimeType;

		#endregion

		#region Properties


		/// <summary>
		/// Gets or sets the OneTime system type identifier
		/// </summary>
        public int OneTimeType
		{
			get{ return m_OneTimeType; }
			set{ m_OneTimeType = value; }
		}

		#endregion

		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of AdventuresAutomation
		/// </summary>
		[Constructable]
		public AdventuresAutomation() : base( AdventuresAutomationConstants.AUTOMATION_ITEM_ID )
		{
			Movable = false;
			Name = "Item de automa��o de aventuras";
			Visible = false;
			m_OneTimeType = AdventuresAutomationConstants.ONETIME_TYPE_VALUE;
			globaltasktimer = AdventuresAutomationConstants.TIMER_INITIAL_VALUE;
			
			CheckHashTables();
		}

		/// <summary>
		/// Initializes all Dictionary collections if they are null.
		/// Called during construction and deserialization.
		/// </summary>
		public static void CheckHashTables()
		{
			// Legacy method name kept for compatibility
			// Now initializes Dictionary collections instead of Hashtable
			if ( AdventuresAutomation.PlayerLoc == null)
				AdventuresAutomation.PlayerLoc = new Dictionary<PlayerMobile, Point3D>();
			if ( AdventuresAutomation.PlayerTool == null)
				AdventuresAutomation.PlayerTool = new Dictionary<PlayerMobile, Item>();
			if ( AdventuresAutomation.TaskNextAction == null)
				AdventuresAutomation.TaskNextAction = new Dictionary<PlayerMobile, int>();
			if ( AdventuresAutomation.TaskTarget == null)
				AdventuresAutomation.TaskTarget = new Dictionary<PlayerMobile, object>();
			if ( AdventuresAutomation.TaskSystem == null)
				AdventuresAutomation.TaskSystem = new Dictionary<PlayerMobile, HarvestSystem>();
			if ( AdventuresAutomation.TaskString == null)
				AdventuresAutomation.TaskString = new Dictionary<PlayerMobile, string>();
			if ( AdventuresAutomation.TimerReverseLookup == null)
				AdventuresAutomation.TimerReverseLookup = new Dictionary<int, List<PlayerMobile>>();
			
			// Ensure AdventuresAutomation item exists in world for OneTimeTick to work
			EnsureAutomationItemExists();
		}
		
		/// <summary>
		/// Ensures the AdventuresAutomation item exists in the world for OneTimeTick to be called
		/// </summary>
		/// <summary>
		/// Validates that the AdventuresAutomation item exists in the world
		/// If not found, sends a page/message to staff members
		/// Uses cached instance for O(1) performance, falls back to world search if cache is invalid
		/// </summary>
		private static void EnsureAutomationItemExists()
		{
			// Fast path: Check cached instance first
			if (s_Instance != null && !s_Instance.Deleted && s_Instance.Map != null && s_Instance.Map != Map.Internal)
			{
				return; // Instance is valid
			}

			// Cache is invalid - search world for the item (fallback for manually placed items)
			AdventuresAutomation foundItem = null;
			foreach (Item item in World.Items.Values)
			{
				if (item is AdventuresAutomation)
				{
					AdventuresAutomation automation = (AdventuresAutomation)item;
					if (!automation.Deleted && automation.Map != null && automation.Map != Map.Internal)
					{
						foundItem = automation;
						break;
					}
				}
			}

			// Update cache if item was found
			if (foundItem != null)
			{
				SetInstance(foundItem);
				return; // Item exists, cache updated
			}

			// Item truly doesn't exist - notify staff
			NotifyStaffMissingAutomationItem();
		}
		
		/// <summary>
		/// Notifies staff members that the AdventuresAutomation item is missing
		/// Prevents spam by only notifying once per minute
		/// </summary>
		private static void NotifyStaffMissingAutomationItem()
		{
			// Prevent spam - only notify once per minute
			if (DateTime.UtcNow - m_LastStaffNotification < TimeSpan.FromMinutes(1))
				return;
			
			m_LastStaffNotification = DateTime.UtcNow;
			
			string message = "[SYSTEM ALERT] AdventuresAutomation item is missing from the world! Automation system will not work. Please spawn an AdventuresAutomation item using: [add AdventuresAutomation";
			
			// Send message to all online staff members
			int staffNotified = 0;
			foreach (NetState ns in NetState.Instances)
			{
				Mobile m = ns.Mobile;
				
				if (m != null && m.AccessLevel >= AccessLevel.Counselor)
				{
					m.SendMessage(33, message); // Yellow color for warning
					staffNotified++;
				}
			}
			
			// Also log to console
			Console.WriteLine(message);
			if (staffNotified == 0)
			{
				Console.WriteLine("[WARNING] No staff members are online to receive the alert!");
			}
		}

		#endregion

		#region Item Lifecycle

		/// <summary>
		/// Called when the item is added to a parent (container or world)
		/// Ensures only one AdventuresAutomation item exists in the server
		/// </summary>
		/// <param name="parent">The parent container or location</param>
		public override void OnAdded(IEntity parent)
		{
			base.OnAdded(parent);

			// Only check for duplicates when added to world (not containers)
			if (parent == null && this.Map != null && this.Map != Map.Internal)
			{
				// Set this as the instance if none exists
				if (s_Instance == null || s_Instance.Deleted || s_Instance.Map == Map.Internal)
				{
					SetInstance(this);
				}
				else
				{
					// Another instance exists - check if this is a duplicate
					CheckForDuplicate();
				}
			}
		}

		/// <summary>
		/// Called after the item is spawned in the world
		/// Ensures only one AdventuresAutomation item exists in the server
		/// </summary>
		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();

			// Check for duplicates when spawned in world
			if (this.Map != null && this.Map != Map.Internal)
			{
				// Set this as the instance if none exists
				if (s_Instance == null || s_Instance.Deleted || s_Instance.Map == Map.Internal)
				{
					SetInstance(this);
				}
				else
				{
					// Another instance exists - check if this is a duplicate
					CheckForDuplicate();
				}
			}
		}

		/// <summary>
		/// Called when the item is deleted
		/// Clears the cached instance if this was the singleton
		/// </summary>
		public override void OnDelete()
		{
			// Clear cached instance if this was the singleton
			if (s_Instance == this)
			{
				ClearInstance();
			}

			base.OnDelete();
		}

		/// <summary>
		/// Checks if another AdventuresAutomation item exists and removes this instance if duplicate found
		/// Uses cached instance for O(1) performance instead of O(n) iteration
		/// </summary>
		private void CheckForDuplicate()
		{
			// Skip check if this item is already deleted
			if (this.Deleted)
				return;

			// Check cached instance instead of iterating all world items
			AdventuresAutomation existingItem = s_Instance;

			// If another instance exists and it's not this one, delete this one
			if (existingItem != null && existingItem != this && !existingItem.Deleted && existingItem.Map != null && existingItem.Map != Map.Internal)
			{
				// Try to find who created this (check recent command log or nearby mobiles)
				Mobile creator = null;
				
				// Check if item is in a mobile's inventory
				if (this.RootParent is Mobile)
					creator = (Mobile)this.RootParent;
				else
				{
					// Try to find nearby staff member who might have created it
					if (this.Map != null)
					{
						IPooledEnumerable<Mobile> mobiles = this.Map.GetMobilesInRange(this.Location, 10);
						foreach (Mobile m in mobiles)
						{
							if (m != null && m.AccessLevel >= AccessLevel.GameMaster)
							{
								creator = m;
								break;
							}
						}
						mobiles.Free();
					}
				}

				// Delete this duplicate instance
				this.Delete();

				// Notify creator if available
				if (creator != null)
				{
					creator.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, 
						"AdventuresAutomation item already exists in the world! This duplicate has been removed.");
					creator.SendMessage(AdventuresAutomationConstants.MSG_COLOR_WARNING, 
						"Only one AdventuresAutomation item can exist at a time. Use the existing one instead.");
				}
				else
				{
					// Log to console if creator not found
					Console.WriteLine("[WARNING] Duplicate AdventuresAutomation item was created and removed. Only one instance should exist.");
				}
			}
		}

		#endregion

		#region Dictionary Accessor Helpers

		/// <summary>
		/// Gets the task string for the specified player
		/// </summary>
		private static string GetTaskString(PlayerMobile pm)
		{
			string value;
			return TaskString.TryGetValue(pm, out value) ? value : "";
		}

		/// <summary>
		/// Gets the player tool for the specified player
		/// </summary>
		private static Item GetPlayerTool(PlayerMobile pm)
		{
			Item value;
			PlayerTool.TryGetValue(pm, out value);
			return value;
		}

		/// <summary>
		/// Gets the task target for the specified player
		/// </summary>
		private static object GetTaskTarget(PlayerMobile pm)
		{
			object value;
			TaskTarget.TryGetValue(pm, out value);
			return value;
		}

		/// <summary>
		/// Gets the task system for the specified player
		/// </summary>
		private static HarvestSystem GetTaskSystem(PlayerMobile pm)
		{
			HarvestSystem value;
			TaskSystem.TryGetValue(pm, out value);
			return value;
		}

		/// <summary>
		/// Gets the player location for the specified player
		/// </summary>
		private static Point3D GetPlayerLoc(PlayerMobile pm)
		{
			Point3D value;
			PlayerLoc.TryGetValue(pm, out value);
			return value;
		}

		/// <summary>
		/// Gets the next action timer for the specified player
		/// </summary>
		private static int GetTaskNextAction(PlayerMobile pm)
		{
			int value;
			TaskNextAction.TryGetValue(pm, out value);
			return value;
		}

		#endregion

		#region Action Handlers

		/// <summary>
		/// Handles fishing action initialization
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="tool">Output parameter for the tool found</param>
		/// <param name="delay">Output parameter for the delay</param>
		/// <returns>True if action was set up successfully, false otherwise</returns>
		private static bool HandleFishingAction(PlayerMobile pm, out Item tool, out int delay)
		{
			tool = null;
			delay = 0;

			//find tool
			tool = pm.FindItemOnLayer( Layer.OneHanded );
			if (tool == null || !(tool is FishingPole) )
			{
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_NEED_FISHING_POLE);
				return false;
			}
			
			//find target
			HarvestTarget(pm, AdventuresAutomationStringConstants.ACTION_STRING_FISHING, tool);

			delay = fishingdelay; // in seconds, for fishing

			TaskSystem[pm] = (HarvestSystem)(Fishing.System);
			TaskString[pm] = AdventuresAutomationStringConstants.ACTION_STRING_FISHING;
			return true;
		}

		/// <summary>
		/// Handles mining action initialization
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="tool">Output parameter for the tool found</param>
		/// <param name="delay">Output parameter for the delay</param>
		/// <returns>True if action was set up successfully, false otherwise</returns>
		private static bool HandleMiningAction(PlayerMobile pm, out Item tool, out int delay)
		{
			tool = null;
			delay = 0;

			// possible tools
			tool = pm.FindItemOnLayer( Layer.OneHanded );

			if (tool == null)
				tool = pm.Backpack.FindItemByType(typeof(Shovel));
			if (tool == null)
				tool = pm.Backpack.FindItemByType(typeof(OreShovel));
			if (tool == null)
				tool = pm.Backpack.FindItemByType(typeof(SturdyShovel));

            // find tool
            if (tool == null || !(BaseAxe.IsMiningTool(tool)) )
			{
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_NEED_MINING_TOOL);
				return false;
			}
            
			HarvestSystem miningSystem = GetMiningSystem(pm, (Item)tool);
			// Allow auto-mining with DynamicMining systems (MineSpirit locations)
			// Previously blocked to force manual mining, but now supports auto-mining

            delay = miningdelay; // in seconds
            HarvestTarget(pm, AdventuresAutomationStringConstants.ACTION_STRING_MINING, tool); //find target
            TaskSystem[pm] = miningSystem;
            TaskString[pm] = AdventuresAutomationStringConstants.ACTION_STRING_MINING;
			return true;
        }

		/// <summary>
		/// Handles lumberjacking action initialization
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="tool">Output parameter for the tool found</param>
		/// <param name="delay">Output parameter for the delay</param>
		/// <returns>True if action was set up successfully, false otherwise</returns>
		private static bool HandleLumberjackingAction(PlayerMobile pm, out Item tool, out int delay)
		{
			tool = null;
			delay = 0;

			//find tool
			tool = pm.FindItemOnLayer( Layer.OneHanded ); //some axes are one handed, some are two

			if (tool == null || (tool != null && !(tool is BaseAxe) ) || BaseAxe.IsMiningTool(tool)) 
				tool = pm.FindItemOnLayer( Layer.TwoHanded );

			if (tool == null || (tool != null && !(tool is BaseAxe) ) || BaseAxe.IsMiningTool(tool)) 
			{
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_NEED_AXE);
				return false;
			}
			
			//find target
			HarvestTarget(pm, AdventuresAutomationStringConstants.ACTION_STRING_LUMBERJACKING, tool);

			delay = lumberjackingdelay; // in seconds

			TaskSystem[pm] = (HarvestSystem)(Lumberjacking.System);
			TaskString[pm] = AdventuresAutomationStringConstants.ACTION_STRING_LUMBERJACKING;
			return true;
		}

		/// <summary>
		/// Handles skinning action initialization
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="delay">Output parameter for the delay</param>
		/// <returns>True if action was set up successfully, false otherwise</returns>
		private static bool HandleSkinningAction(PlayerMobile pm, out int delay)
		{
			delay = 0;

			//find tool
			Item tool = pm.Backpack.FindItemByType(typeof(SkinningKnife));
			Item tool2 = pm.Backpack.FindItemByType(typeof(Scissors));
			
			if ( (tool == null || tool2 == null) || (tool != null && !(tool is SkinningKnife) ) || (tool2 != null && !(tool2 is Scissors) ) )
			{
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_NEED_SKINNING_TOOLS);
				return false;
			}

			delay = skinningdelay; // in seconds

			TaskString[pm] = AdventuresAutomationStringConstants.ACTION_STRING_SKINNING;
			return true;
		}

		/// <summary>
		/// Handles milling action initialization
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="tool">Output parameter for the tool found</param>
		/// <param name="delay">Output parameter for the delay</param>
		/// <returns>True if action was set up successfully, false otherwise</returns>
		private static bool HandleMillingAction(PlayerMobile pm, out Item tool, out int delay)
		{
			tool = null;
			delay = 0;

			Item wheat = pm.Backpack.FindItemByType(typeof(WheatSheaf));
			if (wheat == null)
			{
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_NEED_WHEAT);
				return false;
			}
			
			IPooledEnumerable eable = pm.Map.GetItemsInRange( pm.Location, AdventuresAutomationConstants.MILL_SEARCH_RANGE );
			try
			{
				foreach ( Item item in eable )
				{
					if (tool == null && item is IFlourMill)
						tool = (Item)item;
				}
			}
			finally
			{
				eable.Free();
			}
			
			if (tool == null)
			{
				pm.SendMessage(AdventuresAutomationStringConstants.MSG_NEED_FLOUR_MILL);
				return false;
			}
			
			delay = millingdelay; // in seconds

			TaskString[pm] = AdventuresAutomationStringConstants.ACTION_STRING_MILLING;
			return true;
		}

		/// <summary>
		/// Handles dough making action initialization
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="tool">Output parameter for the tool found</param>
		/// <param name="delay">Output parameter for the delay</param>
		/// <returns>True if action was set up successfully, false otherwise</returns>
		private static bool HandleDoughAction(PlayerMobile pm, out Item tool, out int delay)
		{
			tool = null;
			delay = 0;

			if (!FindWaterSource(pm))
			{
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_NEED_WATER_FOR_DOUGH);
				return false;
			}

			tool = FindCraftTool(pm, AdventuresAutomationStringConstants.ACTION_STRING_DOUGH);
			if (tool == null)
				return false;

			delay = craftingdelay;
			TaskString[pm] = AdventuresAutomationStringConstants.ACTION_STRING_DOUGH;
			return true;
		}

		/// <summary>
		/// Handles bread making action initialization
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="tool">Output parameter for the tool found</param>
		/// <param name="delay">Output parameter for the delay</param>
		/// <returns>True if action was set up successfully, false otherwise</returns>
		private static bool HandleBreadAction(PlayerMobile pm, out Item tool, out int delay)
		{
			tool = null;
			delay = 0;

			if (!FindOven(pm))
			{
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_NEED_OVEN);
				return false;
			}

			tool = FindCraftTool(pm, AdventuresAutomationStringConstants.ACTION_STRING_BREAD);
			if (tool == null)
				return false;

			delay = craftingdelay;
			TaskString[pm] = AdventuresAutomationStringConstants.ACTION_STRING_BREAD;
			return true;
		}

		/// <summary>
		/// Finds a water source near the player
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <returns>True if water source found, false otherwise</returns>
		private static bool FindWaterSource(PlayerMobile pm)
		{
			Map mp = pm.Map;
			//find water
			IPooledEnumerable eable = mp.GetItemsInRange( pm.Location, AdventuresAutomationConstants.WATER_SEARCH_RANGE_ITEMS);
			bool water = false;

			try
			{
				foreach ( Item item in eable )
				{
					string iName = item.ItemData.Name.ToUpper();
					
					if( iName.IndexOf("WATER") != -1 ) 
						water = true;	

					else if (item is WaterVatEast || item is WaterVatSouth || item is WaterTile || item is WaterBarrel || item is WaterTroughEastAddon || item is WaterTroughSouthAddon)	
						water = true;		

					bool soaked;
					Server.Items.DrinkingFunctions.CheckWater( pm, AdventuresAutomationConstants.WATER_SEARCH_RANGE_ITEMS, out soaked );		

					if (soaked)
						water = true;																			 
				}
			}
			finally
			{
				eable.Free();
			}

			if (!water)//try water tiles
			{
				for ( int x = -AdventuresAutomationConstants.WATER_SEARCH_RANGE_TILES; !water && x <= AdventuresAutomationConstants.WATER_SEARCH_RANGE_TILES; ++x )
				{
					if ((pm.X + x) != pm.X)
					{
						for ( int y = -AdventuresAutomationConstants.WATER_SEARCH_RANGE_TILES; !water && y <= AdventuresAutomationConstants.WATER_SEARCH_RANGE_TILES; ++y )
						{
							// Use squared distance comparison to avoid expensive Math.Sqrt
							int squaredDist = x*x + y*y;
							double maxDist = AdventuresAutomationConstants.WATER_SEARCH_DISTANCE_MAX;
							double squaredMaxDist = maxDist * maxDist;

							if (squaredDist <= squaredMaxDist)
							{
								LandTile landTile = pm.Map.Tiles.GetLandTile( pm.X + x, pm.Y + y ); //mining and fishing relies on landtiles

								if ( Server.Misc.Worlds.IsWaterTile( landTile.ID, 0 )) 
									water = true;
							}
						}
					}
				}
			}

			return water;
		}

		/// <summary>
		/// Finds an oven near the player
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <returns>True if oven found, false otherwise</returns>
		private static bool FindOven(PlayerMobile pm)
		{
			Map mp = pm.Map;
			//find oven
			IPooledEnumerable eable = mp.GetItemsInRange( pm.Location, AdventuresAutomationConstants.WATER_SEARCH_RANGE_ITEMS);
			bool ovenFound = false;

			try
			{
				foreach ( Item item in eable )
				{
					string iName = item.ItemData.Name.ToUpper();
					
					if( iName.IndexOf("OVEN") != -1 ) 
						ovenFound = true;																							 
				}
			}
			finally
			{
				eable.Free();
			}

			return ovenFound;
		}

		#endregion

		#region Harvest System Integration

		/// <summary>
		/// Gets the appropriate mining system (DynamicMining for MineSpirit locations, or standard Mining)
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="tool">The mining tool</param>
		/// <returns>The harvest system to use</returns>
		public static HarvestSystem GetMiningSystem(PlayerMobile pm, Item tool)
		{
			// Null check for tool
			if (tool == null || tool.Deleted)
				return (HarvestSystem)(Mining.System);
				
			HarvestSystem DMining = (HarvestSystem)DynamicMining.GetSystem(tool);
            HarvestSystem miningSystem = (DMining != null) ? DMining : (HarvestSystem)(Mining.System);
			return miningSystem;
        }

		#region Validation Methods

		/// <summary>
		/// Validates if player can start automation
		/// </summary>
		/// <param name="pm">The player mobile to validate</param>
		/// <returns>True if player can start automation, false otherwise</returns>
		private static bool ValidatePlayerForAutomation(PlayerMobile pm)
		{
			if ( pm.GetFlag( PlayerFlag.IsAutomated ) )
			{
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_ALREADY_AUTOMATED);
				return false;
			}
			else if ( pm.Backpack == null || pm.Backpack.Deleted)
			{
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_NO_BACKPACK);
				return false;
			}
			else if ( !pm.Alive )
			{
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_PLAYER_DEAD);
				return false;
			}
			else if ( pm.Map == null )
			{
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_INVALID_LOCATION);
				return false;
			}
			return true;
		}

		/// <summary>
		/// Validates if tool is valid (not null and not deleted)
		/// </summary>
		/// <param name="tool">The tool to validate</param>
		/// <returns>True if tool is valid, false otherwise</returns>
		private static bool IsValidTool(Item tool)
		{
			return tool != null && !tool.Deleted;
		}

		#endregion

		#endregion

		#region Core Automation Logic

		/// <summary>
		/// Starts an automation task based on player speech command
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="speech">The speech command</param>
		public static void StartTask( PlayerMobile pm, string speech )
		{
			CheckHashTables();
			
			// Ensure automation item exists in world for OneTimeTick to work
			EnsureAutomationItemExists();
		
			if (!ValidatePlayerForAutomation(pm))
				return;

			int delay = 0;
			Item tool = null;

			// Optimized command parsing: Extract command suffix after ".auto-" prefix
			// This is faster than multiple Insensitive.Contains() calls
			string commandSuffix = null;
			if (Insensitive.StartsWith(speech, ".auto-"))
			{
				// Extract command suffix (everything after ".auto-")
				int prefixLength = 6; // ".auto-".Length
				if (speech.Length > prefixLength)
				{
					commandSuffix = speech.Substring(prefixLength).ToLower();
				}
			}
			else
			{
				// Not an automation command, return early
				return;
			}

			// Route to appropriate handler using switch statement for O(1) lookup
			bool commandHandled = false;
			switch (commandSuffix)
			{
				case "listar":
					pm.Say(AdventuresAutomationStringConstants.MSG_AVAILABLE_ACTIONS);
					pm.Say(AdventuresAutomationStringConstants.MSG_HARVEST_ACTIONS);
					pm.Say(AdventuresAutomationStringConstants.MSG_CRAFTING_ACTIONS);
					return; // Early return for list command

				case "pescar":
					commandHandled = HandleFishingAction(pm, out tool, out delay);
					break;

				case "minerar":
					commandHandled = HandleMiningAction(pm, out tool, out delay);
					break;

				case "lenhar":
					commandHandled = HandleLumberjackingAction(pm, out tool, out delay);
					break;

				case "esfolar":
					commandHandled = HandleSkinningAction(pm, out delay);
					break;

				case "moer":
					commandHandled = HandleMillingAction(pm, out tool, out delay);
					break;

				case "massa":
					commandHandled = HandleDoughAction(pm, out tool, out delay);
					break;

				case "panificar":
					commandHandled = HandleBreadAction(pm, out tool, out delay);
					break;

				default:
					// Unknown command - silently return (could add error message here if needed)
					return;
			}

			// If command handler returned false, action setup failed
			if (!commandHandled)
				return;

            // Setup auto-action
            PlayerTool[pm] = tool;
			// Don't set delay here - DoAction is called immediately, delay will be set after harvest completes

			if (!pm.GetFlag(PlayerFlag.IsAutomated))
				pm.SetFlag(PlayerFlag.IsAutomated, true);
			else {
                pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_ALREADY_AUTOMATED);
            }

			PlayerLoc[pm] = pm.Location;	
			DoAction(pm); 
		}

		#endregion

		#region Harvest and Craft Target Finding

		/// <summary>
		/// Finds a craft tool in the player's backpack for the specified craft type
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="make">The craft type identifier (dough, bread, etc.)</param>
		/// <returns>The craft tool if found, null otherwise</returns>
		public static Item FindCraftTool(PlayerMobile pm, string make)
		{
				//find tool
				Item tool = null;
				CraftSystem system = null;

				if (make == AdventuresAutomationStringConstants.ACTION_STRING_DOUGH || make == AdventuresAutomationStringConstants.ACTION_STRING_BREAD )
				{
					system = DefCooking.CraftSystem;
				}

				for ( int i = 0; tool == null && i < pm.Backpack.Items.Count; i++ )
				{
					
					Item thing = pm.Backpack.Items[ i ];
					if (thing is BaseTool && ((BaseTool)thing).CraftSystem == system)
						tool = (Item)thing;
				}
				if (tool == null)
				{
					string name = AdventuresAutomationStringConstants.ITEM_NAME_BREAD;
					if (make == AdventuresAutomationStringConstants.ACTION_STRING_DOUGH)
						name = AdventuresAutomationStringConstants.ITEM_NAME_DOUGH;
					
					pm.SendMessage(string.Format(AdventuresAutomationStringConstants.MSG_NEED_CRAFT_TOOL_FORMAT, name));
					AdventuresAutomation.StopAction(pm);
				}
				return tool;
		}

		/// <summary>
		/// Finds a harvest target for the specified action and tool
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="action">The action string (fishing, mining, lumberjacking)</param>
		/// <param name="tool">The harvest tool</param>
		public static void HarvestTarget(PlayerMobile pm, string action, Item tool)
		{
			if (!IsValidTool(tool))
			{
				// Tool invalid - stop automation
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_NO_RESOURCES_FOUND);
				pm.Say(AdventuresAutomationStringConstants.MSG_FINISHED_HERE);
				StopAction(pm);
				return;
			}

			int range = GetHarvestRange(action);
			Map mp = pm.Map;
			object target = null;
			int harvestx = 0;
			int harvesty = 0;
			int harvestz = 0;

			// Search land tiles first
			target = FindTargetInLandTiles(pm, action, tool, mp, range, ref harvestx, ref harvesty, ref harvestz);

			// If no target found in land tiles, search static tiles
			if (target == null)
			{
				target = FindTargetInStaticTiles(pm, action, tool, mp, range, ref harvestx, ref harvesty, ref harvestz);
			}

			if (harvestx != 0 && harvesty != 0 && target != null)
			{
				// Remove existing entry if present (Remove returns true if key existed)
				TaskTarget.Remove(pm);
				TaskTarget[pm] = target;
			}
			else
			{
				// No target found - stop automation with proper messages
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_NO_RESOURCES_FOUND);
				pm.Say(AdventuresAutomationStringConstants.MSG_FINISHED_HERE);
				StopAction(pm);
			}
		}

		/// <summary>
		/// Gets the harvest range for the specified action
		/// </summary>
		/// <param name="action">The action string</param>
		/// <returns>Harvest range in tiles</returns>
		private static int GetHarvestRange(string action)
		{
			if (action == AdventuresAutomationStringConstants.ACTION_STRING_MINING || action == AdventuresAutomationStringConstants.ACTION_STRING_LUMBERJACKING)
				return AdventuresAutomationConstants.HARVEST_RANGE_MINING_LUMBERJACKING;
			return AdventuresAutomationConstants.HARVEST_RANGE_DEFAULT;
		}

		/// <summary>
		/// Finds harvest target in land tiles
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="action">The action string</param>
		/// <param name="tool">The harvest tool</param>
		/// <param name="mp">The map</param>
		/// <param name="range">Search range</param>
		/// <param name="harvestx">Output parameter for X coordinate</param>
		/// <param name="harvesty">Output parameter for Y coordinate</param>
		/// <param name="harvestz">Output parameter for Z coordinate</param>
		/// <returns>Target object if found, null otherwise</returns>
		private static object FindTargetInLandTiles(PlayerMobile pm, string action, Item tool, Map mp, int range, ref int harvestx, ref int harvesty, ref int harvestz)
		{
			// Pre-calculate squared range to avoid repeated calculations
			int squaredRange = range * range;

			for ( int x = -range; x <= range; ++x )
			{
				if ((pm.X + x) != pm.X)
				{
					for ( int y = -range; y <= range; ++y )
					{
						// Use squared distance comparison to avoid expensive Math.Sqrt
						int squaredDist = x*x + y*y;

						if (squaredDist <= squaredRange && (harvestx == 0 || harvesty == 0))
						{
							LandTile landTile = mp.Tiles.GetLandTile( pm.X + x, pm.Y + y );

							if (action == AdventuresAutomationStringConstants.ACTION_STRING_FISHING)
							{
								object target = TryFindFishingTargetInLandTile(pm, tool, mp, landTile, pm.X + x, pm.Y + y);
								if (target != null)
								{
									harvestx = pm.X + x;
									harvesty = pm.Y + y;
									harvestz = landTile.Z;
									return target;
								}
							}
							else if (action == AdventuresAutomationStringConstants.ACTION_STRING_MINING)
							{
								object target = TryFindMiningTargetInLandTile(pm, tool, mp, landTile, pm.X + x, pm.Y + y);
								if (target != null)
								{
									harvestx = pm.X + x;
									harvesty = pm.Y + y;
									harvestz = landTile.Z;
									return target;
								}
							}
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Tries to find a fishing target in a land tile
		/// </summary>
		private static object TryFindFishingTargetInLandTile(PlayerMobile pm, Item tool, Map mp, LandTile landTile, int x, int y)
		{
			if (!IsValidTool(tool) || !Server.Misc.Worlds.IsWaterTile(landTile.ID, 1))
				return null;

			HarvestSystem system = (HarvestSystem)(Fishing.System);
			if (system == null)
				return null;

			Point3D loc = new Point3D(x, y, landTile.Z);
			HarvestDefinition def = system.GetDefinition(landTile.ID);
			if (def != null && pm.InLOS(loc) && system.CheckResources(pm, tool, def, mp, loc, false))
			{
				return new LandTarget(loc, mp);
			}
			return null;
		}

		/// <summary>
		/// Tries to find a mining target in a land tile
		/// </summary>
		private static object TryFindMiningTargetInLandTile(PlayerMobile pm, Item tool, Map mp, LandTile landTile, int x, int y)
		{
			if (!IsValidTool(tool) || !Server.Misc.Worlds.IsMiningTile(landTile.ID, 0))
				return null;

			HarvestSystem system = GetMiningSystem(pm, tool);
			if (system == null)
				return null;

			Point3D loc = new Point3D(x, y, landTile.Z);
			HarvestDefinition def = system.GetDefinition(landTile.ID);
			if (def != null && pm.InLOS(loc) && system.CheckResources(pm, tool, def, mp, loc, false))
			{
				return new LandTarget(loc, mp);
			}
			return null;
		}

		/// <summary>
		/// Finds harvest target in static tiles
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="action">The action string</param>
		/// <param name="tool">The harvest tool</param>
		/// <param name="mp">The map</param>
		/// <param name="range">Search range</param>
		/// <param name="harvestx">Output parameter for X coordinate</param>
		/// <param name="harvesty">Output parameter for Y coordinate</param>
		/// <param name="harvestz">Output parameter for Z coordinate</param>
		/// <returns>Target object if found, null otherwise</returns>
		private static object FindTargetInStaticTiles(PlayerMobile pm, string action, Item tool, Map mp, int range, ref int harvestx, ref int harvesty, ref int harvestz)
		{
			// Pre-calculate squared range to avoid repeated calculations
			int squaredRange = range * range;

			for ( int x = -range; x <= range; ++x )
			{
				if ((pm.X + x) != pm.X)
				{
					for ( int y = -range; y <= range; ++y )
					{
						// Use squared distance comparison to avoid expensive Math.Sqrt
						int squaredDist = x*x + y*y;

						if (squaredDist <= squaredRange && (harvestx == 0 || harvesty == 0))
						{
							StaticTile[] tiles = mp.Tiles.GetStaticTiles( pm.X + x, pm.Y + y, true );

							if (action == AdventuresAutomationStringConstants.ACTION_STRING_FISHING)
							{
								object target = TryFindFishingTargetInStaticTiles(pm, tool, mp, tiles, pm.X + x, pm.Y + y);
								if (target != null)
								{
									harvestx = pm.X + x;
									harvesty = pm.Y + y;
									harvestz = mp.GetAverageZ(harvestx, harvesty);
									return target;
								}
							}
							else if (action == AdventuresAutomationStringConstants.ACTION_STRING_LUMBERJACKING)
							{
								object target = TryFindLumberjackingTargetInStaticTiles(pm, tool, mp, tiles, pm.X + x, pm.Y + y);
								if (target != null)
								{
									harvestx = pm.X + x;
									harvesty = pm.Y + y;
									harvestz = mp.GetAverageZ(harvestx, harvesty);
									return target;
								}
							}
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Tries to find a fishing target in static tiles
		/// </summary>
		private static object TryFindFishingTargetInStaticTiles(PlayerMobile pm, Item tool, Map mp, StaticTile[] tiles, int x, int y)
		{
			if (!IsValidTool(tool))
				return null;

			HarvestSystem system = (HarvestSystem)(Fishing.System);
			if (system == null)
				return null;

			for ( int i = 0; i < tiles.Length; ++i )
			{
				StaticTile tile = tiles[i];

				if ( Server.Misc.Worlds.IsWaterTile( (tile.ID+AdventuresAutomationConstants.STATIC_TILE_ID_OFFSET), 1 ) )
				{
					Point3D loc = new Point3D(x, y, mp.GetAverageZ(x, y));
					HarvestDefinition def = system.GetDefinition( tile.ID + AdventuresAutomationConstants.STATIC_TILE_ID_OFFSET );

					if (def == null || loc == Point3D.Zero)
						continue;

					if (pm.InLOS(loc) && system.CheckResources( pm, tool, def, mp, loc, false ))
					{
						return new StaticTarget(new Point3D(x, y, mp.GetAverageZ(x, y)), tile.ID );
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Tries to find a lumberjacking target in static tiles
		/// </summary>
		private static object TryFindLumberjackingTargetInStaticTiles(PlayerMobile pm, Item tool, Map mp, StaticTile[] tiles, int x, int y)
		{
			if (!IsValidTool(tool))
				return null;

			HarvestSystem system = (HarvestSystem)(Lumberjacking.System);
			if (system == null)
				return null;

			for ( int i = 0; i < tiles.Length; ++i )
			{
				StaticTile tile = tiles[i];

				if ( Server.Misc.Worlds.IsTreeTile( (tile.ID + AdventuresAutomationConstants.STATIC_TILE_ID_OFFSET) ) )
				{
					Point3D loc = new Point3D(x, y, mp.GetAverageZ(x, y));
					HarvestDefinition def = system.GetDefinition( tile.ID + AdventuresAutomationConstants.STATIC_TILE_ID_OFFSET );

					if (def == null || loc == Point3D.Zero)
						continue;

					if (pm.InLOS(loc) && system.CheckResources( pm, tool, def, mp, loc, false ))
					{
						return new StaticTarget(new Point3D(x, y, mp.GetAverageZ(x, y)), tile.ID );
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Clears the harvest target for the specified player
		/// </summary>
		/// <param name="pm">The player mobile</param>
		public static void ClearHarvestTarget( PlayerMobile pm )
		{
			if (pm != null)
			{
				TaskTarget.Remove(pm);
			}
		}

		/// <summary>
		/// Stops automation for the specified player
		/// </summary>
		/// <param name="pm">The player mobile</param>
		public static void StopAction( PlayerMobile pm )
		{
			if (pm != null) 
			{
                pm.SetFlag(PlayerFlag.IsAutomated, false);

                // Remove entries (Remove returns true if key existed, but we don't need to check)
                // Also remove from reverse lookup if present
                int oldTimer;
                if (TaskNextAction.TryGetValue(pm, out oldTimer))
                {
                    RemovePlayerFromTimer(oldTimer, pm);
                }

                PlayerLoc.Remove(pm);
                PlayerTool.Remove(pm);
                TaskNextAction.Remove(pm);
                TaskTarget.Remove(pm);
                TaskSystem.Remove(pm);
                TaskString.Remove(pm);

                pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_NORMAL, AdventuresAutomationStringConstants.MSG_AUTOMATION_STOPPED);
            }
		}

		#endregion

		#region Utility Methods

		/// <summary>
		/// Copies properties from source item to destination item
		/// </summary>
		/// <param name="dest">Destination item</param>
		/// <param name="src">Source item</param>
        private static void CopyProperties(Item dest, Item src)
        {
            PropertyInfo[] props = src.GetType().GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                try
                {
                    if (props[i].CanRead && props[i].CanWrite)
                    {
                        props[i].SetValue(dest, props[i].GetValue(src, null), null);
                    }
                }
                catch
                {
                    // Property copy failed, continue with next property
                }
            }
        }

		/// <summary>
		/// Checks player hunger and thirst, automatically consumes food/water if needed
		/// </summary>
		/// <param name="pm">The player mobile</param>
        public static void CheckFood(PlayerMobile pm)
		{
			if (pm.Hunger < AdventuresAutomationConstants.FOOD_CHECK_THRESHOLD)
			{
				Food munch = (Food)pm.Backpack.FindItemByType(typeof(Food));
				if (munch != null)
					munch.Eat((Mobile)pm);
			}
			if (pm.Thirst < AdventuresAutomationConstants.THIRST_CHECK_THRESHOLD)
			{
				Waterskin munch = (Waterskin)pm.Backpack.FindItemByType(typeof(Waterskin));

				if (munch != null && !(munch.ItemID == AdventuresAutomationConstants.WATERSKIN_ITEMID_1 || munch.ItemID == AdventuresAutomationConstants.WATERSKIN_ITEMID_2 ))
					munch.OnDoubleClick((Mobile)pm);
			}

		}
		
		#region DoAction Helper Methods

		/// <summary>
		/// Checks if player is overweight and handles item dropping
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="action">The current action string</param>
		/// <returns>True if overweight was handled, false if not overweight</returns>
		private static bool CheckWeightOverload(PlayerMobile pm, string action)
		{
			if (pm.Backpack.TotalWeight >= (pm.MaxWeight * AdventuresAutomationConstants.WEIGHT_OVERLOAD_MULTIPLIER))
			{
                pm.PlaySound(pm.Female ? AdventuresAutomationConstants.SOUND_OVERWEIGHT_FEMALE : AdventuresAutomationConstants.SOUND_OVERWEIGHT_MALE);
                pm.Say(AdventuresAutomationStringConstants.MSG_OVERWEIGHT_EXPRESSION);

                Type[] fishingList = new Type[]
                {
					typeof(Fish), 
					typeof(BaseMagicFish), 
					typeof(FishSteak), 
					typeof(BigFish), 
					typeof(NewFish)
                };
                Type resource = typeof(Item);
				if (action == AdventuresAutomationStringConstants.ACTION_STRING_MINING) { resource = typeof(BaseOre); }
                else if (action == AdventuresAutomationStringConstants.ACTION_STRING_LUMBERJACKING) { resource = typeof(BaseLog); }
                else if (action == AdventuresAutomationStringConstants.ACTION_STRING_FISHING) { resource = fishingList[Utility.Random(fishingList.Length)];}
                else if (action == AdventuresAutomationStringConstants.ACTION_STRING_SKINNING) { resource = typeof(BaseLeather); }

				if (pm.Backpack.FindItemByType(resource) != null) 
				{
                    Container backpack = pm.Backpack;
                    Item obj = pm.Backpack.FindItemByType(resource);
					// Calc Loss
                    int loss = AdventuresAutomationConstants.WEIGHT_LOSS_MINIMUM;
                    if ((obj.Amount / AdventuresAutomationConstants.WEIGHT_LOSS_DIVISOR) > 1)
                        loss = Utility.RandomMinMax(AdventuresAutomationConstants.WEIGHT_LOSS_MINIMUM, (int)(obj.Amount / AdventuresAutomationConstants.WEIGHT_LOSS_DIVISOR));
					// subtract amount and move original item again to the backpack
					obj.Amount -= loss;

                    // create new item with loss amount
                    Type t = obj.GetType();
                    ConstructorInfo c = t.GetConstructor(Type.EmptyTypes);

					if (c != null)
					{
						try
						{
							object o = c.Invoke(null);

							if (o != null && o is Item)
							{
                                Item newItem = (Item)o;
                                CopyProperties(newItem, obj);
								obj.OnAfterDuped(newItem);
								newItem.Parent = null;
                                newItem.Amount = loss;
                                newItem.MoveToWorld(pm.Location, pm.Map);// drop the losses on the floor
                                newItem.InvalidateProperties();

                                pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_OVERWEIGHT_DROP_ITEMS);
							}
						}
						catch
						{
                            pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_OVERWEIGHT_PERIOD);
                            StopAction(pm);
                            return true;
						}
					}
                }
                else
				{
                    pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_OVERWEIGHT);
                    StopAction(pm);
                }
				return true;
			}
			return false;
		}

		/// <summary>
		/// Gets the delay for the specified harvest system
		/// </summary>
		/// <param name="system">The harvest system</param>
		/// <returns>Delay in seconds</returns>
		public static int GetDelayForSystem(HarvestSystem system)
		{
			if (system is Fishing)
				return AdventuresAutomationConstants.DELAY_FISHING_SECONDS;
			else if (system is Mining || system is DynamicMining)
				return AdventuresAutomationConstants.DELAY_MINING_SECONDS;
			else if (system is Lumberjacking)
				return AdventuresAutomationConstants.DELAY_LUMBERJACKING_SECONDS;
			else
				return AdventuresAutomationConstants.DELAY_CRAFTING_SECONDS;
		}

		/// <summary>
		/// Performs the automation action for the specified player
		/// </summary>
		/// <param name="pm">The player mobile</param>
		public static void DoAction(PlayerMobile pm)
		{
			if (pm == null)
				return;

			// Check if player is still flagged as automated
			if (!pm.GetFlag(PlayerFlag.IsAutomated))
			{
				// Player is no longer automated, clean up
				TaskNextAction.Remove(pm);
				return;
			}

			string action = GetTaskString(pm);
			Item tool = GetPlayerTool(pm);
			
			if (action == null || action == "")
			{
				// Task string missing but player is still automated - try to re-initialize from tool
				if (tool != null)
				{
					// Try to determine action from tool type
					if (tool is FishingPole)
						action = AdventuresAutomationStringConstants.ACTION_STRING_FISHING;
					else if (BaseAxe.IsMiningTool(tool))
						action = AdventuresAutomationStringConstants.ACTION_STRING_MINING;
					else if (tool is BaseAxe && !BaseAxe.IsMiningTool(tool))
						action = AdventuresAutomationStringConstants.ACTION_STRING_LUMBERJACKING;
					
					if (action != null && action != "")
					{
						TaskString[pm] = action;
						// Re-initialize system if needed (use TryGetValue to check existence)
						HarvestSystem existingSystem;
						if (!TaskSystem.TryGetValue(pm, out existingSystem))
						{
							if (action == AdventuresAutomationStringConstants.ACTION_STRING_FISHING)
								TaskSystem[pm] = (HarvestSystem)(Fishing.System);
							else if (action == AdventuresAutomationStringConstants.ACTION_STRING_MINING)
								TaskSystem[pm] = GetMiningSystem(pm, tool);
							else if (action == AdventuresAutomationStringConstants.ACTION_STRING_LUMBERJACKING)
								TaskSystem[pm] = (HarvestSystem)(Lumberjacking.System);
						}
					}
					else
					{
						pm.Say(AdventuresAutomationStringConstants.MSG_FORGOT_ACTION);
						StopAction(pm);
						return;
					}
				}
				else
				{
					pm.Say(AdventuresAutomationStringConstants.MSG_FORGOT_ACTION);
					StopAction(pm);
					return;
				}
			}

			// Check weight overload
			if (CheckWeightOverload(pm, action))
				return;

			// Check hunger and thirst
			if (pm.Hunger <= AdventuresAutomationConstants.FOOD_STOP_THRESHOLD || pm.Thirst <= AdventuresAutomationConstants.THIRST_STOP_THRESHOLD)
			{
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_TOO_HUNGRY_OR_THIRSTY);
				StopAction(pm);
			}
			CheckFood(pm);

			// Check if player moved
			Point3D loc = pm.Location;
			Point3D oldloc;
			if (PlayerLoc.TryGetValue(pm, out oldloc))
			{
				if (loc.X != oldloc.X || loc.Y != oldloc.Y)
				{
					pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_MOVED_STOPPED_ACTION);
					StopAction(pm);
					return;
				}
			}
			
			// Some actions don't need harvest system so handle them here
			if (action == AdventuresAutomationStringConstants.ACTION_STRING_SKINNING)
			{
				SkinSomething(pm);
				return;
			}
			else if (action == AdventuresAutomationStringConstants.ACTION_STRING_MILLING)
			{
				MillSomething(pm);
				return;
			}

			if (tool == null)
			{
				pm.Say(AdventuresAutomationStringConstants.MSG_TOOL_PROBLEM);
				return;
			}

			if (action == AdventuresAutomationStringConstants.ACTION_STRING_DOUGH)
			{
				if (tool == null || tool.Deleted)
				{
					pm.Say(AdventuresAutomationStringConstants.MSG_TOOL_PROBLEM);
					StopAction(pm);
					return;
				}
				MakeDough(pm, tool);
				return;
			}
			if (action == AdventuresAutomationStringConstants.ACTION_STRING_BREAD)
			{
				if (tool == null || tool.Deleted)
				{
					pm.Say(AdventuresAutomationStringConstants.MSG_TOOL_PROBLEM);
					StopAction(pm);
					return;
				}
				MakeBread(pm, tool);
				return;
			}

			// Find harvest target if needed (use TryGetValue to check existence)
			object existingTarget;
			if (!TaskTarget.TryGetValue(pm, out existingTarget))
			{
				HarvestTarget(pm, action, tool);
				
				// If HarvestTarget didn't find a target, it will have called StopAction
				// Check if automation was stopped
				if (!pm.GetFlag(PlayerFlag.IsAutomated))
					return;
			}

			HarvestSystem hs;
			object harvestTarget;
			if (TaskSystem.TryGetValue(pm, out hs) && TaskTarget.TryGetValue(pm, out harvestTarget)) 
			{
				if (harvestTarget != null && hs != null)
				{
					// Start harvesting - delay will be set after harvest completes in HarvestSystem.FinishHarvesting
					// All failure cases in StartHarvesting now handle delay setting for automation
					hs.StartHarvesting(pm, tool, harvestTarget);
				}
				else
				{
					// Invalid target or system - clear and retry with delay
					TaskTarget.Remove(pm);
					int delay = GetDelayForSystem(hs != null ? hs : GetTaskSystem(pm));
					if (delay == 0)
						delay = AdventuresAutomationConstants.DELAY_MINING_SECONDS;
					SetNextAction(pm, delay);
				}
			}
			else
			{
				// No target or system found - try to find target again with delay
				// This handles cases where HarvestTarget was called but didn't set a target
				int delay = AdventuresAutomationConstants.DELAY_MINING_SECONDS;
				if (hs != null)
					delay = GetDelayForSystem(hs);
				SetNextAction(pm, delay);
			}
		}

		#endregion

		#endregion

		#region Action Handlers

		/// <summary>
		/// Handles skinning automation action
		/// </summary>
		/// <param name="pm">The player mobile</param>
		public static void SkinSomething(PlayerMobile pm)
		{
			bool foundcorpse = false;
			bool foundhides = false;
			IPooledEnumerable eable = pm.Map.GetItemsInRange( pm.Location, AdventuresAutomationConstants.SKINNING_SEARCH_RANGE );
			try
			{

				foreach ( Item item in eable )
				{
					if (!foundcorpse && item is ICarvable && pm.InLOS(item) )
					{
						((ICarvable)item).Carve(pm, pm.Backpack.FindItemByType(typeof(SkinningKnife)));
						foundcorpse = true;

						pm.CheckSkill(SkillName.Forensics, AdventuresAutomationConstants.SKILL_CHECK_MIN, AdventuresAutomationConstants.SKILL_CHECK_MAX ); // you can gain skill doing this 
					}
				}
				if (!foundcorpse) // all corpses are carved, check for hides
				{
					foreach ( Item item in eable )
					{
						if (!foundhides && item is Corpse && ((Corpse)item).Carved)
						{
							BaseHides hide = (BaseHides)((Container)item).FindItemByType(typeof(BaseHides));
							
							if (hide != null)
							{
								if (BaseHides.CutHides((Mobile)pm, hide))
									foundhides = true;

								pm.CheckSkill(SkillName.Tailoring, AdventuresAutomationConstants.SKILL_CHECK_MIN, AdventuresAutomationConstants.SKILL_CHECK_MAX ); // you can gain skill doing this 
							}
						}
					}
				}
			}
			finally
			{
				eable.Free();
			}
			if (!foundcorpse && !foundhides)
			{
                pm.Say(AdventuresAutomationStringConstants.MSG_FINISHED_HERE);
                AdventuresAutomation.StopAction(pm);
			}
			else 
			{
				SetNextAction(pm, skinningdelay);
			}
				
		}

		/// <summary>
		/// Handles milling automation action
		/// </summary>
		/// <param name="pm">The player mobile</param>
		public static void MillSomething(PlayerMobile pm)
		{
			bool millwheat = false;
			
			IFlourMill mill = null; // get tool from dictionary
			Item toolItem;
			if (PlayerTool.TryGetValue(pm, out toolItem))
			{
				mill = toolItem as IFlourMill;
			}
			
			WheatSheaf wheat = (WheatSheaf)pm.Backpack.FindItemByType(typeof(WheatSheaf));
			if (mill != null && wheat != null)
			{
				
				int needs = mill.MaxFlour - mill.CurFlour;

				if ( needs > wheat.Amount )
					needs = wheat.Amount;

				mill.CurFlour += needs;
				wheat.Consume( needs );
				if (mill is FlourMillEastAddon)
					((FlourMillEastAddon)mill).StartWorking( (Mobile)pm );
				if (mill is FlourMillSouthAddon)
					((FlourMillSouthAddon)mill).StartWorking( (Mobile)pm );

				millwheat = true;

				pm.CheckSkill(SkillName.Cooking, AdventuresAutomationConstants.SKILL_CHECK_MIN, AdventuresAutomationConstants.SKILL_CHECK_MAX ); // you can gain skill doing this 
			}

			if (!millwheat)
			{
                pm.Say(AdventuresAutomationStringConstants.MSG_FINISHED_HERE);
                AdventuresAutomation.StopAction(pm);
			}
			else 
			{
				SetNextAction(pm, millingdelay);
			}
				
		}

		/// <summary>
		/// Handles dough making automation action
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="tool">The craft tool</param>
		public static void MakeDough(PlayerMobile pm, Item tool)
		{
			//will need to add this for any items that are automated sadly
			//setting recipe and resources and skills required here from defcooking I have no idea how to read defcooking for this
			Type resource1 = typeof( SackFlour ); 
			int r1amount = 1;
			Type resource2 = null;
			int r2amount = 0;
			Type resource3 = null;
			int r3amount = 0;
			Type resource4 = null;
			int r4amount = 0;

			int minskill = AdventuresAutomationConstants.DOUGH_MIN_SKILL;
			int maxskill = AdventuresAutomationConstants.DOUGH_MAX_SKILL;

			int delay = craftingdelay;

			Type tomake = typeof(Dough);
			SkillName skill = SkillName.Cooking;

			Make(pm, (BaseTool)tool, AdventuresAutomationStringConstants.ACTION_STRING_DOUGH, skill, tomake, delay, minskill, maxskill, resource1, r1amount, resource2, r2amount, resource3, r3amount, resource4, r4amount );
		}

		/// <summary>
		/// Handles bread making automation action
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="tool">The craft tool</param>
		public static void MakeBread(PlayerMobile pm, Item tool)
		{
			//will need to add this for any items that are automated sadly
			//setting recipe and resources and skills required here from defcooking I have no idea how to read defcooking for this
			Type resource1 = typeof( Dough ); 
			int r1amount = 1;
			Type resource2 = null;
			int r2amount = 0;
			Type resource3 = null;
			int r3amount = 0;
			Type resource4 = null;
			int r4amount = 0;

			int minskill = AdventuresAutomationConstants.BREAD_MIN_SKILL;
			int maxskill = AdventuresAutomationConstants.BREAD_MAX_SKILL;

			int delay = craftingdelay;

			Type tomake = typeof(BreadLoaf);
			SkillName skill = SkillName.Cooking;

			Make(pm, (BaseTool)tool, AdventuresAutomationStringConstants.ACTION_STRING_BREAD, skill, tomake, delay, minskill, maxskill, resource1, r1amount, resource2, r2amount, resource3, r3amount, resource4, r4amount );
		}

		/// <summary>
		/// Generic crafting method for automation
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="tool">The craft tool</param>
		/// <param name="thing">The item name being crafted</param>
		/// <param name="skill">The skill required</param>
		/// <param name="tomake">The type of item to create</param>
		/// <param name="delay">Delay in seconds</param>
		/// <param name="minskill">Minimum skill required</param>
		/// <param name="maxskill">Maximum skill for success</param>
		/// <param name="Resource1">First resource type</param>
		/// <param name="r1">First resource amount</param>
		/// <param name="Resource2">Second resource type</param>
		/// <param name="r2">Second resource amount</param>
		/// <param name="Resource3">Third resource type</param>
		/// <param name="r3">Third resource amount</param>
		/// <param name="Resource4">Fourth resource type</param>
		/// <param name="r4">Fourth resource amount</param>
		public static void Make(PlayerMobile pm, BaseTool tool, string thing, SkillName skill, Type tomake, int delay, int minskill, int maxskill, Type Resource1, int r1, Type Resource2, int r2, Type Resource3, int r3, Type Resource4, int r4  )
		{
			//crafting system is bullshit complicated.  bypassing it.
			// check tool - null check must come first
			if (tool == null || tool.Deleted || tool.UsesRemaining < 1)
			{
				tool = (BaseTool)FindCraftTool(pm, thing); // try to find a new tool
				if (tool == null)
				{
					pm.Say(AdventuresAutomationStringConstants.MSG_OUT_OF_TOOLS);
                    pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_TOOL_BROKEN_NO_REPLACEMENT);
                    AdventuresAutomation.StopAction(pm);
				}
			}

			if ((int)pm.Skills[SkillName.Cooking].Value < minskill)
			{
				pm.Say(AdventuresAutomationStringConstants.MSG_SKILL_TOO_LOW);
				AdventuresAutomation.StopAction(pm);
			}

			//check resources in pack
			Item res1 = null;
			if (Resource1 != null && r1 > 0)
			{
				res1 = pm.Backpack.FindItemByType(Resource1);

				if (res1 == null && (res1 is IHasQuantity && ((IHasQuantity)res1).Quantity < r1))
				{
					NotEnoughResource(pm, thing);
					return;
				}
				else if (res1 == null || (res1 != null && res1.Amount < r1) )
				{
					NotEnoughResource(pm, thing);
					return;
				}
			}
			Item res2 = null;
			if (Resource2 != null && r2 > 0)
			{
				res2 = pm.Backpack.FindItemByType(Resource2);
				if (res2 == null || (res2 != null && res2.Amount < r2) )
				{
					NotEnoughResource(pm, thing);
					return;
				}
			}
			Item res3 = null;
			if (Resource3 != null && r3 > 0)
			{
				res3 = pm.Backpack.FindItemByType(Resource3);
				if (res3 == null || (res3 != null && res3.Amount < r3) )
				{
					NotEnoughResource(pm, thing);
					return;
				}
			}
			Item res4 = null;
			if (Resource4 != null && r4 > 0)
			{
				res4 = pm.Backpack.FindItemByType(Resource4);
				if (res4 == null || (res4 != null && res1.Amount < r4) )
				{
					NotEnoughResource(pm, thing);
					return;
				}
			}

			//okay player has everything, take it away!
			if (res1 != null)
			{
				if (res1 is IHasQuantity)
				{
					if (((IHasQuantity)res1).Quantity > 1)
						((IHasQuantity)res1).Quantity -= 1;
					else 
						res1.Delete();
				}
				else if (res1.Amount > r1)
					res1.Amount -= r1;
				else if (res1.Amount == r1);
					res1.Delete();
			}
			if (res2 != null)
			{
				if (res2.Amount > r2)
					res2.Amount -= r2;
				else if (res1.Amount == r2);
					res2.Delete();
			}
			if (res3 != null)
			{
				if (res3.Amount > r3)
					res3.Amount -= r3;
				else if (res1.Amount == r3);
					res3.Delete();
			}
			if (res4 != null)
			{
				if (res4.Amount > r4)
					res4.Amount -= r4;
				else if (res1.Amount == r4);
					res4.Delete();
			}

			if (pm.CheckSkill(skill, minskill, maxskill ))
			{

				//give new item to player
				Item reward = (Item)Activator.CreateInstance(tomake);
				pm.Backpack.DropItem(reward);

				pm.SendMessage(string.Format(AdventuresAutomationStringConstants.MSG_CRAFT_SUCCESS_FORMAT, thing));
			}
			else
				pm.SendMessage(AdventuresAutomationConstants.MSG_COLOR_ERROR, AdventuresAutomationStringConstants.MSG_CRAFT_FAILED);
			

			((BaseTool)tool).UsesRemaining --;
			SetNextAction(pm, delay);
		}

		/// <summary>
		/// Handles not enough resources error
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="thing">The item name being crafted</param>
		public static void NotEnoughResource(PlayerMobile pm, string thing)
		{
			pm.Say(string.Format(AdventuresAutomationStringConstants.MSG_NOT_ENOUGH_RESOURCES_FORMAT, thing));
			AdventuresAutomation.StopAction(pm);
		}

		#endregion

		#region Timer System

		/// <summary>
		/// Sets the next action timer for the specified player
		/// Also updates reverse lookup for O(1) timer matching
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="delay">Delay in seconds</param>
		public static void SetNextAction(PlayerMobile pm, int delay)
		{
			if (pm == null || pm.Deleted)
				return;

			// Only set delay if player is still flagged as automated
			if (!pm.GetFlag(PlayerFlag.IsAutomated))
				return;

			// Ensure minimum delay of 1 second
			if (delay < 1)
				delay = 1;

			// Remove player from old timer in reverse lookup
			int oldTimer;
			if (TaskNextAction.TryGetValue(pm, out oldTimer))
			{
				RemovePlayerFromTimer(oldTimer, pm);
			}

			// Calculate next timer value (current timer + delay)
			int nextTimer = globaltasktimer + delay;
			
			// Handle wrap-around if exceeds max value
			// Timer goes: 0, 1, 2, ..., 29, 30, then wraps to 0
			// So if nextTimer > 30, wrap to (nextTimer - 30 - 1)
			if (nextTimer > AdventuresAutomationConstants.TIMER_MAX_VALUE)
			{
				nextTimer = nextTimer - AdventuresAutomationConstants.TIMER_MAX_VALUE - 1;
				// Ensure it's not negative
				if (nextTimer < 0)
					nextTimer = 0;
			}

			// Update forward lookup
			TaskNextAction[pm] = nextTimer;

			// Update reverse lookup for O(1) timer matching
			AddPlayerToTimer(nextTimer, pm);
		}

		/// <summary>
		/// Adds player to reverse lookup for specified timer value
		/// </summary>
		private static void AddPlayerToTimer(int timerValue, PlayerMobile pm)
		{
			List<PlayerMobile> players;
			if (!TimerReverseLookup.TryGetValue(timerValue, out players))
			{
				players = new List<PlayerMobile>();
				TimerReverseLookup[timerValue] = players;
			}
			
			// Only add if not already in list (safety check)
			if (!players.Contains(pm))
				players.Add(pm);
		}

		/// <summary>
		/// Removes player from reverse lookup for specified timer value
		/// </summary>
		private static void RemovePlayerFromTimer(int timerValue, PlayerMobile pm)
		{
			List<PlayerMobile> players;
			if (TimerReverseLookup.TryGetValue(timerValue, out players))
			{
				players.Remove(pm);
				
				// Clean up empty lists to prevent memory leak
				if (players.Count == 0)
					TimerReverseLookup.Remove(timerValue);
			}
		}

		/// <summary>
		/// Called by OneTime system to process automation timers
		/// Uses reverse lookup for O(1) timer matching instead of O(n) iteration
		/// </summary>
		public void OneTimeTick()
		{
			CheckHashTables();

			// Use reverse lookup for O(1) timer matching
			List<PlayerMobile> playersForTimer;
			if (TimerReverseLookup.TryGetValue(globaltasktimer, out playersForTimer))
			{
				// Process all players with matching timer
				// Iterate backwards to safely remove items during iteration
				for (int i = playersForTimer.Count - 1; i >= 0; i--)
				{
					PlayerMobile pm = playersForTimer[i];
					
					// Validate player before processing
					if (pm == null || pm.Deleted || !pm.GetFlag(PlayerFlag.IsAutomated))
					{
						// Remove invalid player from reverse lookup
						playersForTimer.RemoveAt(i);
						// Also remove from forward lookup
						TaskNextAction.Remove(pm);
						continue;
					}

					// Process the player
					DoAction(pm);
				}

				// Clean up empty list
				if (playersForTimer.Count == 0)
					TimerReverseLookup.Remove(globaltasktimer);
			}

			globaltasktimer ++;
			
			if (globaltasktimer > AdventuresAutomationConstants.TIMER_MAX_VALUE)
				globaltasktimer = AdventuresAutomationConstants.TIMER_INITIAL_VALUE;

		}

		#endregion

		#region Serialization

		/// <summary>
		/// Deserializes AdventuresAutomation instance
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public AdventuresAutomation(Serial serial) : base(serial)
		{
		}

		/// <summary>
		/// Serializes AdventuresAutomation instance
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            		writer.Write( (int) 0 ); // version
			
		}

		/// <summary>
		/// Deserializes AdventuresAutomation instance
		/// </summary>
		/// <param name="reader">Generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            		int version = reader.ReadInt();

			m_OneTimeType = AdventuresAutomationConstants.ONETIME_TYPE_VALUE;
			globaltasktimer = AdventuresAutomationConstants.TIMER_INITIAL_VALUE;
			
			CheckHashTables();
			
		}

		#endregion

		#region Utility Methods (Legacy)

		/// <summary>
		/// Removes item from specified layer (legacy method, may not be used)
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="layer">The layer to remove item from</param>
		public static void UndressItem(PlayerMobile pm, Layer layer)
		{
			Item item = pm.FindItemOnLayer( layer );

			if ( item != null && item.Movable )
				pm.PlaceInBackpack( item );
		}

		#endregion
		
	}
}
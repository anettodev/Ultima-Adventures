using System;
using System.Collections.Generic;
using Server.Regions;
using Server.Mobiles;

namespace Server.Mobiles.Helpers
{
	/// <summary>
	/// Helper class for retrieving region-specific messages for TownHerald and DungeonGuide NPCs.
	/// Extracted from TownHerald.cs to reduce complexity and improve maintainability.
	/// </summary>
	public static class RegionMessageHelper
	{
		#region Dungeon Message Dictionary

		/// <summary>
		/// Dictionary mapping dungeon name keywords to their messages.
		/// </summary>
		private static readonly Dictionary<string, string> DungeonMessages = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{ "deceit", TownHeraldStringConstants.MSG_DUNGEON_DECEIT },
			{ "wrong", TownHeraldStringConstants.MSG_DUNGEON_DECEIT },
			{ "despise", TownHeraldStringConstants.MSG_DUNGEON_DESPISE },
			{ "destard", TownHeraldStringConstants.MSG_DUNGEON_DESTARD },
			{ "shame", TownHeraldStringConstants.MSG_DUNGEON_SHAME },
			{ "hythloth", TownHeraldStringConstants.MSG_DUNGEON_HYTHLOTH },
			{ "covetous", TownHeraldStringConstants.MSG_DUNGEON_COVETOUS },
			{ "wind", TownHeraldStringConstants.MSG_DUNGEON_WIND },
			{ "fire", TownHeraldStringConstants.MSG_DUNGEON_FIRE },
			{ "ice", TownHeraldStringConstants.MSG_DUNGEON_ICE },
			{ "doom", TownHeraldStringConstants.MSG_DUNGEON_DOOM },
			{ "bedlam", TownHeraldStringConstants.MSG_DUNGEON_BEDLAM },
			{ "labyrinth", TownHeraldStringConstants.MSG_DUNGEON_LABYRINTH },
			{ "sanctuary", TownHeraldStringConstants.MSG_DUNGEON_SANCTUARY },
			{ "prison", TownHeraldStringConstants.MSG_DUNGEON_PRISON },
			{ "jail", TownHeraldStringConstants.MSG_DUNGEON_PRISON }
		};

		#endregion

		#region Region Type Message Dictionary

		/// <summary>
		/// Dictionary mapping region types to their messages.
		/// </summary>
		private static readonly Dictionary<Type, string> RegionTypeMessages = new Dictionary<Type, string>
		{
			{ typeof(CaveRegion), TownHeraldStringConstants.MSG_REGION_CAVE },
			{ typeof(DeadRegion), TownHeraldStringConstants.MSG_REGION_DEAD },
			{ typeof(GargoyleRegion), TownHeraldStringConstants.MSG_REGION_GARGOYLE },
			{ typeof(NecromancerRegion), TownHeraldStringConstants.MSG_REGION_NECROMANCER },
			{ typeof(PirateRegion), TownHeraldStringConstants.MSG_REGION_PIRATE },
			{ typeof(MazeRegion), TownHeraldStringConstants.MSG_REGION_MAZE }
		};

		#endregion

		#region Region Name Keyword Dictionary

		/// <summary>
		/// Dictionary mapping region name keywords to their messages.
		/// </summary>
		private static readonly Dictionary<string, string> RegionNameMessages = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{ "Pirate", TownHeraldStringConstants.MSG_REGION_PIRATE },
			{ "Maze", TownHeraldStringConstants.MSG_REGION_MAZE },
			{ "Abyss", TownHeraldStringConstants.MSG_REGION_ABYSS },
			{ "Ice", TownHeraldStringConstants.MSG_REGION_ICE },
			{ "Frozen", TownHeraldStringConstants.MSG_REGION_ICE },
			{ "Fire", TownHeraldStringConstants.MSG_REGION_FIRE },
			{ "Volcano", TownHeraldStringConstants.MSG_REGION_FIRE },
			{ "Swamp", TownHeraldStringConstants.MSG_REGION_SWAMP },
			{ "Bog", TownHeraldStringConstants.MSG_REGION_SWAMP },
			{ "Castle", TownHeraldStringConstants.MSG_REGION_CASTLE },
			{ "Keep", TownHeraldStringConstants.MSG_REGION_CASTLE },
			{ "Ruins", TownHeraldStringConstants.MSG_REGION_RUINS },
			{ "Tomb", TownHeraldStringConstants.MSG_REGION_TOMB },
			{ "Crypt", TownHeraldStringConstants.MSG_REGION_TOMB },
			{ "Mine", TownHeraldStringConstants.MSG_REGION_MINE }
		};

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the message for a dungeon region based on its name.
		/// </summary>
		/// <param name="regionName">The name of the dungeon region (will be converted to lowercase)</param>
		/// <returns>The message for the dungeon, or default message if not found</returns>
		public static string GetDungeonMessage(string regionName)
		{
			if (string.IsNullOrEmpty(regionName))
				return TownHeraldStringConstants.MSG_DUNGEON_DEFAULT;

			string lowerName = regionName.ToLower();

			foreach (KeyValuePair<string, string> entry in DungeonMessages)
			{
				if (lowerName.Contains(entry.Key))
					return entry.Value;
			}

			return TownHeraldStringConstants.MSG_DUNGEON_DEFAULT;
		}

		/// <summary>
		/// Gets the message for a region based on its type.
		/// </summary>
		/// <param name="region">The region to get a message for</param>
		/// <returns>The message for the region type, or null if not found</returns>
		public static string GetRegionTypeMessage(Region region)
		{
			if (region == null)
				return null;

			Type regionType = region.GetType();

			// Check exact type match first
			if (RegionTypeMessages.ContainsKey(regionType))
				return RegionTypeMessages[regionType];

			// Check base types
			foreach (KeyValuePair<Type, string> entry in RegionTypeMessages)
			{
				if (entry.Key.IsAssignableFrom(regionType))
					return entry.Value;
			}

			return null;
		}

		/// <summary>
		/// Gets the message for a region based on its name keywords.
		/// </summary>
		/// <param name="regionName">The name of the region</param>
		/// <returns>The message for the region name, or null if not found</returns>
		public static string GetRegionNameMessage(string regionName)
		{
			if (string.IsNullOrEmpty(regionName))
				return null;

			foreach (KeyValuePair<string, string> entry in RegionNameMessages)
			{
				if (regionName.Contains(entry.Key))
					return entry.Value;
			}

			return null;
		}

		/// <summary>
		/// Gets the message for a region, checking type first, then name.
		/// </summary>
		/// <param name="region">The region to get a message for</param>
		/// <returns>The message for the region, or null if not found</returns>
		public static string GetRegionMessage(Region region)
		{
			if (region == null)
				return null;

			// Check region type first
			string message = GetRegionTypeMessage(region);
			if (message != null)
				return message;

			// Check region name keywords
			message = GetRegionNameMessage(region.Name);
			if (message != null)
				return message;

			return null;
		}

		#endregion
	}
}


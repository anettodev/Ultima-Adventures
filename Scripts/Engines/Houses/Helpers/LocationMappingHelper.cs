using System;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server.Items.Helpers
{
	/// <summary>
	/// Helper class for mapping vendor locations to display names and teleport coordinates.
	/// Extracted from AdvertiserVendor.cs to reduce complexity and improve maintainability.
	/// </summary>
	public static class LocationMappingHelper
	{
		/// <summary>
		/// Represents coordinate bounds for a location region
		/// </summary>
		private struct LocationBounds
		{
			public int MinX;
			public int MinY;
			public int MaxX;
			public int MaxY;

			public LocationBounds(int minX, int minY, int maxX, int maxY)
			{
				MinX = minX;
				MinY = minY;
				MaxX = maxX;
				MaxY = maxY;
			}

			public bool Contains(int x, int y)
			{
				return x > MinX && y > MinY && x < MaxX && y < MaxY;
			}
		}

		/// <summary>
		/// Represents location mapping information
		/// </summary>
		private class LocationMapping
		{
			public Map Map;
			public LocationBounds Bounds;
			public string LocationName;
			public int TeleportX;
			public int TeleportY;
			public Map TeleportMap;
			public bool UseVendorCoordinates;

			public LocationMapping(Map map, LocationBounds bounds, string locationName, int teleportX, int teleportY, Map teleportMap, bool useVendorCoordinates = false)
			{
				Map = map;
				Bounds = bounds;
				LocationName = locationName;
				TeleportX = teleportX;
				TeleportY = teleportY;
				TeleportMap = teleportMap;
				UseVendorCoordinates = useVendorCoordinates;
			}
		}

		/// <summary>
		/// Location mappings ordered by specificity (most specific first)
		/// </summary>
		private static readonly List<LocationMapping> LocationMappings = new List<LocationMapping>
		{
			// Felucca specific locations
			new LocationMapping(Map.Felucca, new LocationBounds(5157, 1095, 5296, 1401), AdvertiserVendorStringConstants.LOCATION_RANGER_OUTPOST, 1241, 1888, Map.Felucca),
			new LocationMapping(Map.Felucca, new LocationBounds(6445, 3054, 7007, 3478), AdvertiserVendorStringConstants.LOCATION_RAVENDARK_WOODS, 466, 3801, Map.Felucca),

			// Trammel specific locations (ordered by specificity - most specific first)
			new LocationMapping(Map.Trammel, new LocationBounds(5218, 1036, 5414, 1304), AdvertiserVendorStringConstants.LOCATION_UMBRA_CAVE, 3370, 1553, Map.Trammel),
			new LocationMapping(Map.Trammel, new LocationBounds(6548, 3812, 6741, 4071), AdvertiserVendorStringConstants.LOCATION_SHIPWRECK_GROTTO, 318, 1397, Map.Trammel),
			new LocationMapping(Map.Trammel, new LocationBounds(5793, 2738, 6095, 3011), AdvertiserVendorStringConstants.LOCATION_MOON_OF_LUNA, 3696, 519, Map.Trammel),
			new LocationMapping(Map.Trammel, new LocationBounds(5125, 3038, 6124, 4093), AdvertiserVendorStringConstants.LOCATION_AMBROSIA, 0, 0, Map.Trammel, true),
			new LocationMapping(Map.Trammel, new LocationBounds(6127, 828, 7168, 2738), AdvertiserVendorStringConstants.LOCATION_BOTTLE_WORLD, 0, 0, Map.Trammel, true),
			new LocationMapping(Map.Trammel, new LocationBounds(699, 3129, 2272, 4095), AdvertiserVendorStringConstants.LOCATION_UMBER_VEIL, 0, 0, Map.Trammel, true),
			new LocationMapping(Map.Trammel, new LocationBounds(860, 3184, 2136, 4090), AdvertiserVendorStringConstants.LOCATION_UMBER_VEIL, 0, 0, Map.Trammel, true),
			new LocationMapping(Map.Trammel, new LocationBounds(5129, 3062, int.MaxValue, int.MaxValue), AdvertiserVendorStringConstants.LOCATION_AMBROSIA, 0, 0, Map.Trammel, true),

			// Malas specific locations (portal areas)
			new LocationMapping(Map.Malas, new LocationBounds(1949, 1393, 2061, 1486), AdvertiserVendorStringConstants.LOCATION_SOSARIA, 1863, 1129, Map.Trammel),
			new LocationMapping(Map.Malas, new LocationBounds(2150, 1401, 2270, 1513), AdvertiserVendorStringConstants.LOCATION_LODORIA, 1861, 2747, Map.Felucca),
			new LocationMapping(Map.Malas, new LocationBounds(2375, 1398, 2442, 1467), AdvertiserVendorStringConstants.LOCATION_LODORIA, 466, 3801, Map.Felucca),
			new LocationMapping(Map.Malas, new LocationBounds(2401, 1635, 2468, 1703), AdvertiserVendorStringConstants.LOCATION_SERPENT_ISLAND, 254, 670, Map.Malas),
			new LocationMapping(Map.Malas, new LocationBounds(2408, 1896, 2517, 2005), AdvertiserVendorStringConstants.LOCATION_SAVAGED_EMPIRE, 422, 398, Map.TerMur),
			new LocationMapping(Map.Malas, new LocationBounds(2181, 1889, 2275, 2003), AdvertiserVendorStringConstants.LOCATION_DREAD_ISLES, 251, 1249, Map.Tokuno),
			new LocationMapping(Map.Malas, new LocationBounds(1930, 1890, 2022, 1997), AdvertiserVendorStringConstants.LOCATION_SOSARIA, 3884, 2879, Map.Trammel),

			// Ilshenar specific locations (portal areas)
			new LocationMapping(Map.Ilshenar, new LocationBounds(1644, 35, 1818, 163), AdvertiserVendorStringConstants.LOCATION_LODORIA, 4299, 3318, Map.Felucca),
			new LocationMapping(Map.Ilshenar, new LocationBounds(1864, 32, 2041, 162), AdvertiserVendorStringConstants.LOCATION_SAVAGED_EMPIRE, 177, 961, Map.TerMur),
			new LocationMapping(Map.Ilshenar, new LocationBounds(2098, 27, 2272, 156), AdvertiserVendorStringConstants.LOCATION_SAVAGED_EMPIRE, 766, 1527, Map.TerMur),
			new LocationMapping(Map.Ilshenar, new LocationBounds(1647, 184, 1810, 305), AdvertiserVendorStringConstants.LOCATION_SERPENT_ISLAND, 1191, 1516, Map.Malas),
			new LocationMapping(Map.Ilshenar, new LocationBounds(1877, 187, 2033, 302), AdvertiserVendorStringConstants.LOCATION_UMBER_VEIL, 1944, 3377, Map.Trammel),
			new LocationMapping(Map.Ilshenar, new LocationBounds(2108, 190, 2269, 305), AdvertiserVendorStringConstants.LOCATION_SERPENT_ISLAND, 1544, 1785, Map.Malas),
			new LocationMapping(Map.Ilshenar, new LocationBounds(1656, 335, 1807, 443), AdvertiserVendorStringConstants.LOCATION_SOSARIA, 2059, 2406, Map.Trammel),
			new LocationMapping(Map.Ilshenar, new LocationBounds(1880, 338, 2031, 445), AdvertiserVendorStringConstants.LOCATION_LODORIA, 1558, 2861, Map.Felucca),
			new LocationMapping(Map.Ilshenar, new LocationBounds(2111, 335, 2266, 446), AdvertiserVendorStringConstants.LOCATION_DREAD_ISLES, 755, 1093, Map.Tokuno),
			new LocationMapping(Map.Ilshenar, new LocationBounds(1657, 496, 1807, 606), AdvertiserVendorStringConstants.LOCATION_SOSARIA, 2181, 1327, Map.Trammel),
			new LocationMapping(Map.Ilshenar, new LocationBounds(1879, 498, 2031, 605), AdvertiserVendorStringConstants.LOCATION_SAVAGED_EMPIRE, 752, 680, Map.TerMur),
			new LocationMapping(Map.Ilshenar, new LocationBounds(2115, 499, 2263, 605), AdvertiserVendorStringConstants.LOCATION_RAVENDARK_WOODS, 466, 3801, Map.Felucca),
			new LocationMapping(Map.Ilshenar, new LocationBounds(1657, 641, 1808, 748), AdvertiserVendorStringConstants.LOCATION_LODORIA, 2893, 2030, Map.Felucca),
			new LocationMapping(Map.Ilshenar, new LocationBounds(1883, 640, 2033, 745), AdvertiserVendorStringConstants.LOCATION_SAVAGED_EMPIRE, 1050, 93, Map.TerMur),
			new LocationMapping(Map.Ilshenar, new LocationBounds(2113, 641, 2266, 747), AdvertiserVendorStringConstants.LOCATION_DREAD_ISLES, 127, 85, Map.Tokuno),
			new LocationMapping(Map.Ilshenar, new LocationBounds(1657, 795, 1811, 898), AdvertiserVendorStringConstants.LOCATION_SERPENT_ISLAND, 145, 1434, Map.Malas),
			new LocationMapping(Map.Ilshenar, new LocationBounds(1883, 794, 2034, 902), AdvertiserVendorStringConstants.LOCATION_LODORIA, 2625, 823, Map.Felucca),
			new LocationMapping(Map.Ilshenar, new LocationBounds(2112, 794, 2267, 898), AdvertiserVendorStringConstants.LOCATION_DREAD_ISLES, 740, 182, Map.Tokuno),
			new LocationMapping(Map.Ilshenar, new LocationBounds(1659, 953, 1809, 1059), AdvertiserVendorStringConstants.LOCATION_AMBROSIA, 5390, 3280, Map.Trammel),
			new LocationMapping(Map.Ilshenar, new LocationBounds(1881, 954, 2034, 1059), AdvertiserVendorStringConstants.LOCATION_HEDGE_MAZE, 922, 1775, Map.TerMur),
			new LocationMapping(Map.Ilshenar, new LocationBounds(2113, 952, 2268, 1056), AdvertiserVendorStringConstants.LOCATION_SAVAGED_EMPIRE, 1036, 1162, Map.TerMur),
		};

		/// <summary>
		/// Gets location information for a vendor based on its position
		/// </summary>
		/// <param name="vendorX">Vendor X coordinate</param>
		/// <param name="vendorY">Vendor Y coordinate</param>
		/// <param name="vendorMap">Vendor map</param>
		/// <param name="locationName">Output: Location display name</param>
		/// <param name="teleportX">Output: Teleport X coordinate</param>
		/// <param name="teleportY">Output: Teleport Y coordinate</param>
		/// <param name="teleportMap">Output: Teleport map</param>
		public static void GetLocationInfo(int vendorX, int vendorY, Map vendorMap, out string locationName, out int teleportX, out int teleportY, out Map teleportMap)
		{
			// Check specific location mappings first
			foreach (LocationMapping mapping in LocationMappings)
			{
				if (mapping.Map == vendorMap && mapping.Bounds.Contains(vendorX, vendorY))
				{
					locationName = mapping.LocationName;
					if (mapping.UseVendorCoordinates)
					{
						teleportX = vendorX;
						teleportY = vendorY;
					}
					else
					{
						teleportX = mapping.TeleportX;
						teleportY = mapping.TeleportY;
					}
					teleportMap = mapping.TeleportMap;
					return;
				}
			}

			// Default mappings by map
			if (vendorMap == Map.Felucca)
			{
				locationName = AdvertiserVendorStringConstants.LOCATION_LODORIA;
				teleportX = vendorX;
				teleportY = vendorY;
				teleportMap = Map.Felucca;
			}
			else if (vendorMap == Map.Malas)
			{
				locationName = AdvertiserVendorStringConstants.LOCATION_SERPENT_ISLAND;
				teleportX = vendorX;
				teleportY = vendorY;
				teleportMap = Map.Malas;
			}
			else if (vendorMap == Map.Tokuno)
			{
				locationName = AdvertiserVendorStringConstants.LOCATION_DREAD_ISLES;
				teleportX = vendorX;
				teleportY = vendorY;
				teleportMap = Map.Tokuno;
			}
			else if (vendorMap == Map.TerMur && vendorY < 1800)
			{
				locationName = AdvertiserVendorStringConstants.LOCATION_SAVAGED_EMPIRE;
				teleportX = vendorX;
				teleportY = vendorY;
				teleportMap = Map.TerMur;
			}
			else
			{
				// Default: Sosaria (Trammel)
				locationName = AdvertiserVendorStringConstants.LOCATION_SOSARIA;
				teleportX = vendorX;
				teleportY = vendorY;
				teleportMap = Map.Trammel;
			}
		}
	}
}


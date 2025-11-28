using System;
using System.Collections.Generic;
using Server.Network;
using Server.Misc;
using Server.Regions;

namespace Server.Items
{
	/// <summary>
	/// Sextant - Navigation tool that displays current coordinates.
	/// Can be used to determine location in degrees, minutes, and direction.
	/// </summary>
	public class Sextant : Item
	{
		#region Map Boundary Structure

		/// <summary>
		/// Structure to hold map boundary information
		/// </summary>
		private struct MapBoundary
		{
			public int XMin;
			public int YMin;
			public int XMax;
			public int YMax;
			public int Width;
			public int Height;

			public MapBoundary( int xMin, int yMin, int xMax, int yMax, int width, int height )
			{
				XMin = xMin;
				YMin = yMin;
				XMax = xMax;
				YMax = yMax;
				Width = width;
				Height = height;
			}
		}

		/// <summary>
		/// Dictionary mapping world names to their boundaries
		/// </summary>
		private static readonly Dictionary<string, MapBoundary> WorldBoundaries = new Dictionary<string, MapBoundary>
		{
			{ "the Moon of Luna", new MapBoundary( SextantConstants.MAP_LUNA_X_MIN, SextantConstants.MAP_LUNA_Y_MIN, SextantConstants.MAP_LUNA_X_MAX, SextantConstants.MAP_LUNA_Y_MAX, SextantConstants.MAP_LUNA_WIDTH, SextantConstants.MAP_LUNA_HEIGHT ) },
			{ "the Land of Sosaria", new MapBoundary( SextantConstants.MAP_SOSARIA_X_MIN, SextantConstants.MAP_SOSARIA_Y_MIN, SextantConstants.MAP_SOSARIA_X_MAX, SextantConstants.MAP_SOSARIA_Y_MAX, SextantConstants.MAP_SOSARIA_WIDTH, SextantConstants.MAP_SOSARIA_HEIGHT ) },
			{ "the Land of Lodoria", new MapBoundary( SextantConstants.MAP_LODORIA_X_MIN, SextantConstants.MAP_LODORIA_Y_MIN, SextantConstants.MAP_LODORIA_X_MAX, SextantConstants.MAP_LODORIA_Y_MAX, SextantConstants.MAP_LODORIA_WIDTH, SextantConstants.MAP_LODORIA_HEIGHT ) },
			{ "the Serpent Island", new MapBoundary( SextantConstants.MAP_SERPENT_X_MIN, SextantConstants.MAP_SERPENT_Y_MIN, SextantConstants.MAP_SERPENT_X_MAX, SextantConstants.MAP_SERPENT_Y_MAX, SextantConstants.MAP_SERPENT_WIDTH, SextantConstants.MAP_SERPENT_HEIGHT ) },
			{ "the Isles of Dread", new MapBoundary( SextantConstants.MAP_DREAD_X_MIN, SextantConstants.MAP_DREAD_Y_MIN, SextantConstants.MAP_DREAD_X_MAX, SextantConstants.MAP_DREAD_Y_MAX, SextantConstants.MAP_DREAD_WIDTH, SextantConstants.MAP_DREAD_HEIGHT ) },
			{ "the Savaged Empire", new MapBoundary( SextantConstants.MAP_SAVAGED_X_MIN, SextantConstants.MAP_SAVAGED_Y_MIN, SextantConstants.MAP_SAVAGED_X_MAX, SextantConstants.MAP_SAVAGED_Y_MAX, SextantConstants.MAP_SAVAGED_WIDTH, SextantConstants.MAP_SAVAGED_HEIGHT ) },
			{ "the Land of Ambrosia", new MapBoundary( SextantConstants.MAP_AMBROSIA_X_MIN, SextantConstants.MAP_AMBROSIA_Y_MIN, SextantConstants.MAP_AMBROSIA_X_MAX, SextantConstants.MAP_AMBROSIA_Y_MAX, SextantConstants.MAP_AMBROSIA_WIDTH, SextantConstants.MAP_AMBROSIA_HEIGHT ) },
			{ "the Island of Umber Veil", new MapBoundary( SextantConstants.MAP_UMBER_X_MIN, SextantConstants.MAP_UMBER_Y_MIN, SextantConstants.MAP_UMBER_X_MAX, SextantConstants.MAP_UMBER_Y_MAX, SextantConstants.MAP_UMBER_WIDTH, SextantConstants.MAP_UMBER_HEIGHT ) },
			{ "ilha de Kuldar", new MapBoundary( SextantConstants.MAP_KULDAR_X_MIN, SextantConstants.MAP_KULDAR_Y_MIN, SextantConstants.MAP_KULDAR_X_MAX, SextantConstants.MAP_KULDAR_Y_MAX, SextantConstants.MAP_KULDAR_WIDTH, SextantConstants.MAP_KULDAR_HEIGHT ) },
			{ "the Underworld", new MapBoundary( SextantConstants.MAP_UNDERWORLD_X_MIN, SextantConstants.MAP_UNDERWORLD_Y_MIN, SextantConstants.MAP_UNDERWORLD_X_MAX, SextantConstants.MAP_UNDERWORLD_Y_MAX, SextantConstants.MAP_UNDERWORLD_WIDTH, SextantConstants.MAP_UNDERWORLD_HEIGHT ) }
		};

		#endregion

		#region Constructors

		[Constructable]
		public Sextant() : base( SextantConstants.ITEM_ID_SEXTANT )
		{
			Weight = SextantConstants.WEIGHT_SEXTANT;
		}

		public Sextant( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Core Methods

		/// <summary>
		/// Handles double-click to display current coordinates
		/// </summary>
		public override void OnDoubleClick( Mobile from )
		{
			string world = Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y );

			int xLong = SextantConstants.COORD_INIT_VALUE;
			int yLat = SextantConstants.COORD_INIT_VALUE;
			int xMins = SextantConstants.COORD_INIT_VALUE;
			int yMins = SextantConstants.COORD_INIT_VALUE;
			bool xEast = false;
			bool ySouth = false;

			if ( world == FishingStringConstants.WORLD_NAME_UNDERWORLD && !(this is MagicSextant) )
			{
				from.SendMessage( FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_NEED_MAGIC_SEXTANT );
			}
			else if ( Format( from.Location, from.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
			{
				string location = String.Format( FishingStringConstants.MSG_COORDINATE_FORMAT, 
					yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
				from.LocalOverheadMessage( MessageType.Regular, from.SpeechHue, false, location );
			}
			else if ( IsBlockedRegion( from, from.Map, from.Location ) )
			{
				// Error message handled in IsBlockedRegion
			}
			else
			{
				from.SendMessage( FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_SEXTANT_NOT_WORKING );
			}
		}

		/// <summary>
		/// Computes map details for coordinate calculation
		/// </summary>
		/// <param name="map">The map to compute details for</param>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		/// <param name="xCenter">Output: X center coordinate</param>
		/// <param name="yCenter">Output: Y center coordinate</param>
		/// <param name="xWidth">Output: Map width</param>
		/// <param name="yHeight">Output: Map height</param>
		/// <returns>True if map details were computed successfully</returns>
		public static bool ComputeMapDetails( Map map, int x, int y, out int xCenter, out int yCenter, out int xWidth, out int yHeight )
		{
			xWidth = 0;
			yHeight = 0;
			xCenter = 0;
			yCenter = 0;

			Point3D location = new Point3D( x, y, 0 );
			string world = Worlds.GetMyWorld( map, location, x, y );

			MapBoundary boundary;
			if ( WorldBoundaries.TryGetValue( world, out boundary ) )
			{
				// Special case for Moon of Luna - requires coordinate check
				if ( world == "the Moon of Luna" )
				{
					if ( x >= boundary.XMin && y >= boundary.YMin && x <= boundary.XMax && y <= boundary.YMax )
					{
						SetMapDetails( boundary, out xCenter, out yCenter, out xWidth, out yHeight );
						return true;
					}
				}
				// Standard boundary check for other worlds
				else if ( x >= boundary.XMin && y >= boundary.YMin && x <= boundary.XMax && y <= boundary.YMax )
				{
					SetMapDetails( boundary, out xCenter, out yCenter, out xWidth, out yHeight );
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Converts coordinates back to a Point3D location
		/// </summary>
		/// <param name="map">The map to convert coordinates for</param>
		/// <param name="xLong">Longitude in degrees</param>
		/// <param name="yLat">Latitude in degrees</param>
		/// <param name="xMins">Longitude minutes</param>
		/// <param name="yMins">Latitude minutes</param>
		/// <param name="xEast">True if east, false if west</param>
		/// <param name="ySouth">True if south, false if north</param>
		/// <returns>Point3D location or Point3D.Zero if invalid</returns>
		public static Point3D ReverseLookup( Map map, int xLong, int yLat, int xMins, int yMins, bool xEast, bool ySouth )
		{
			if ( map == null || map == Map.Internal )
				return Point3D.Zero;

			int xCenter, yCenter;
			int xWidth, yHeight;

			if ( !ComputeMapDetails( map, 0, 0, out xCenter, out yCenter, out xWidth, out yHeight ) )
				return Point3D.Zero;

			double absLong = ConvertMinutesToDegrees( xLong, xMins );
			double absLat = ConvertMinutesToDegrees( yLat, yMins );

			if ( !xEast )
				absLong = SextantConstants.DEGREES_FULL_CIRCLE - absLong;

			if ( !ySouth )
				absLat = SextantConstants.DEGREES_FULL_CIRCLE - absLat;

			int x = xCenter + (int)((absLong * xWidth) / SextantConstants.DEGREES_FULL_CIRCLE_INT);
			int y = yCenter + (int)((absLat * yHeight) / SextantConstants.DEGREES_FULL_CIRCLE_INT);

			// Handle coordinate wrapping
			if ( x < 0 )
				x += xWidth;
			else if ( x >= xWidth )
				x -= xWidth;

			if ( y < 0 )
				y += yHeight;
			else if ( y >= yHeight )
				y -= yHeight;

			int z = map.GetAverageZ( x, y );

			return new Point3D( x, y, z );
		}

		/// <summary>
		/// Formats a Point3D location into coordinate string
		/// </summary>
		/// <param name="p">Point3D location</param>
		/// <param name="map">The map</param>
		/// <param name="xLong">Output: Longitude in degrees</param>
		/// <param name="yLat">Output: Latitude in degrees</param>
		/// <param name="xMins">Output: Longitude minutes</param>
		/// <param name="yMins">Output: Latitude minutes</param>
		/// <param name="xEast">Output: True if east, false if west</param>
		/// <param name="ySouth">Output: True if south, false if north</param>
		/// <returns>True if formatting was successful</returns>
		public static bool Format( Point3D p, Map map, ref int xLong, ref int yLat, ref int xMins, ref int yMins, ref bool xEast, ref bool ySouth )
		{
			if ( map == null || map == Map.Internal )
				return false;

			int x = p.X;
			int y = p.Y;
			int xCenter, yCenter;
			int xWidth, yHeight;

			if ( !ComputeMapDetails( map, x, y, out xCenter, out yCenter, out xWidth, out yHeight ) )
				return false;

			double absLong = (double)((x - xCenter) * SextantConstants.DEGREES_FULL_CIRCLE_INT) / xWidth;
			double absLat = (double)((y - yCenter) * SextantConstants.DEGREES_FULL_CIRCLE_INT) / yHeight;

			// Normalize to -180 to 180 range
			if ( absLong > SextantConstants.DEGREES_HALF_CIRCLE )
				absLong = -SextantConstants.DEGREES_HALF_CIRCLE + (absLong % SextantConstants.DEGREES_HALF_CIRCLE);

			if ( absLat > SextantConstants.DEGREES_HALF_CIRCLE )
				absLat = -SextantConstants.DEGREES_HALF_CIRCLE + (absLat % SextantConstants.DEGREES_HALF_CIRCLE);

			bool east = ( absLong >= 0 );
			bool south = ( absLat >= 0 );

			if ( absLong < 0.0 )
				absLong = -absLong;

			if ( absLat < 0.0 )
				absLat = -absLat;

			xLong = (int)absLong;
			yLat = (int)absLat;

			xMins = ConvertDegreesToMinutes( absLong );
			yMins = ConvertDegreesToMinutes( absLat );

			xEast = east;
			ySouth = south;

			return true;
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Checks if the current region blocks sextant usage and sends appropriate error message
		/// </summary>
		/// <param name="from">The mobile using the sextant</param>
		/// <param name="map">The map</param>
		/// <param name="location">The location</param>
		/// <returns>True if region blocks sextant, false otherwise</returns>
		private bool IsBlockedRegion( Mobile from, Map map, Point3D location )
		{
			string regionName = Server.Misc.Worlds.GetRegionName( map, location );

			if ( regionName == FishingStringConstants.REGION_NAME_RAVENDARK_WOODS || 
				 regionName == FishingStringConstants.REGION_NAME_VILLAGE_RAVENDARK )
			{
				from.SendMessage( FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_REGION_RAVENDARK );
				return true;
			}
			else if ( regionName == FishingStringConstants.REGION_NAME_RANGER_OUTPOST )
			{
				from.SendMessage( FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_REGION_RANGER_OUTPOST );
				return true;
			}
			else if ( regionName == FishingStringConstants.REGION_NAME_DARK_DRUIDS )
			{
				from.SendMessage( FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_REGION_DARK_DRUIDS );
				return true;
			}

			return false;
		}

		/// <summary>
		/// Sets map details from boundary structure
		/// </summary>
		/// <param name="boundary">Map boundary structure</param>
		/// <param name="xCenter">Output: X center coordinate</param>
		/// <param name="yCenter">Output: Y center coordinate</param>
		/// <param name="xWidth">Output: Map width</param>
		/// <param name="yHeight">Output: Map height</param>
		private static void SetMapDetails( MapBoundary boundary, out int xCenter, out int yCenter, out int xWidth, out int yHeight )
		{
			xWidth = boundary.Width;
			yHeight = boundary.Height;
			xCenter = (int)(boundary.XMin + (boundary.Width / SextantConstants.COORD_DIVISOR));
			yCenter = (int)(boundary.YMin + (boundary.Height / SextantConstants.COORD_DIVISOR));
		}

		/// <summary>
		/// Converts degrees and minutes to total degrees
		/// </summary>
		/// <param name="degrees">Degrees component</param>
		/// <param name="minutes">Minutes component</param>
		/// <returns>Total degrees as double</returns>
		private static double ConvertMinutesToDegrees( int degrees, int minutes )
		{
			return degrees + ((double)minutes / SextantConstants.MINUTES_PER_DEGREE);
		}

		/// <summary>
		/// Converts degrees to minutes (fractional part)
		/// </summary>
		/// <param name="degrees">Degrees as double</param>
		/// <returns>Minutes component</returns>
		private static int ConvertDegreesToMinutes( double degrees )
		{
			return (int)((degrees % 1.0) * SextantConstants.MINUTES_PER_DEGREE);
		}

		#endregion

		#region Serialization

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion
	}
}

namespace Server.Items
{
	/// <summary>
	/// Centralized string constants for AdvertiserVendor messages and labels.
	/// Extracted from AdvertiserVendor.cs to improve maintainability and enable localization.
	/// All strings are in Portuguese-Brazilian (PT-BR).
	/// </summary>
	public static class AdvertiserVendorStringConstants
	{
		#region Item Properties

		/// <summary>AdvertiserVendor item name</summary>
		public const string ITEM_NAME = "Quadro de Anúncios - Comércio";

		/// <summary>Property label displayed on item</summary>
		public const string PROPERTY_LABEL = "Lista de Vendedores";

		#endregion

		#region Gump Column Headers

		/// <summary>Column header for shop name</summary>
		public const string LABEL_SHOP_NAME = "Nome da Loja";

		/// <summary>Column header for owner name</summary>
		public const string LABEL_OWNER = "Dono";

		/// <summary>Column header for location</summary>
		public const string LABEL_LOCATION = "Localização";

		#endregion

		#region User Messages

		/// <summary>Footer message format (format: {0} = server name, {1} = vendor count)</summary>
		public const string MSG_FOOTER_FORMAT = "{0}  -  Comerciantes                 Existem {1} comerciantes neste mundo.";

		/// <summary>Message displayed when no vendors exist in the world</summary>
		public const string MSG_NO_VENDORS = ".....::: Não há comerciantes neste mundo. :::.....";

		/// <summary>Coordinate format string (format: {0} = latitude degrees, {1} = latitude minutes, {2} = N/S, {3} = longitude degrees, {4} = longitude minutes, {5} = E/W)</summary>
		public const string MSG_COORDINATE_FORMAT = "{0}° {1}'{2}, {3}° {4}'{5}";

		#endregion

		#region Location Names

		/// <summary>Location name: Sosaria</summary>
		public const string LOCATION_SOSARIA = "Sosaria";

		/// <summary>Location name: Ranger Outpost</summary>
		public const string LOCATION_RANGER_OUTPOST = "Ranger Outpost";

		/// <summary>Location name: Ravendark Woods</summary>
		public const string LOCATION_RAVENDARK_WOODS = "Ravendark Woods";

		/// <summary>Location name: Lodoria</summary>
		public const string LOCATION_LODORIA = "Lodoria";

		/// <summary>Location name: Umbra Cave</summary>
		public const string LOCATION_UMBRA_CAVE = "Umbra Cave";

		/// <summary>Location name: Shipwreck Grotto</summary>
		public const string LOCATION_SHIPWRECK_GROTTO = "Shipwreck Grotto";

		/// <summary>Location name: Umber Veil</summary>
		public const string LOCATION_UMBER_VEIL = "Umber Veil";

		/// <summary>Location name: Ambrosia</summary>
		public const string LOCATION_AMBROSIA = "Ambrosia";

		/// <summary>Location name: Moon of Luna</summary>
		public const string LOCATION_MOON_OF_LUNA = "Moon of Luna";

		/// <summary>Location name: Serpent Island</summary>
		public const string LOCATION_SERPENT_ISLAND = "Serpent Island";

		/// <summary>Location name: Savaged Empire</summary>
		public const string LOCATION_SAVAGED_EMPIRE = "Savaged Empire";

		/// <summary>Location name: Dread Isles</summary>
		public const string LOCATION_DREAD_ISLES = "Dread Isles";

		/// <summary>Location name: Hedge Maze</summary>
		public const string LOCATION_HEDGE_MAZE = "Hedge Maze";

		/// <summary>Location name: Bottle World</summary>
		public const string LOCATION_BOTTLE_WORLD = "Bottle World";

		#endregion

		#region Console Messages

		/// <summary>Console message when no vendors found (debugging)</summary>
		public const string CONSOLE_NO_VENDORS = "No Vendors In Shard...";

		#endregion
	}
}


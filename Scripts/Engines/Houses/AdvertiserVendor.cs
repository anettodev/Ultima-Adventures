using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Items.Helpers;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	/// <summary>
	/// AdvertiserVendor - Item that displays a gump listing all player vendors in the world.
	/// Allows players to find and teleport to vendor locations.
	/// </summary>
	[Flipable(AdvertiserVendorConstants.ITEM_ID_1, AdvertiserVendorConstants.ITEM_ID_2)]
	public class AdvertiserVendor : Item
	{
		#region Constructors

		/// <summary>
		/// Creates a new AdvertiserVendor item
		/// </summary>
		[Constructable]
		public AdvertiserVendor() : base(AdvertiserVendorConstants.ITEM_ID_1)
		{
			Weight = AdvertiserVendorConstants.WEIGHT;
			Name = AdvertiserVendorStringConstants.ITEM_NAME;
			Hue = AdvertiserVendorConstants.ITEM_HUE;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public AdvertiserVendor(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the AdvertiserVendor
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		/// <summary>
		/// Deserializes the AdvertiserVendor
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Gets properties displayed on the item
		/// </summary>
		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			list.Add(AdvertiserVendorStringConstants.PROPERTY_LABEL);
		}

		/// <summary>
		/// Handles double-click to open vendor listing gump
		/// </summary>
		/// <param name="e">The mobile that double-clicked the item</param>
		public override void OnDoubleClick(Mobile e)
		{
			List<PlayerVendor> list = new List<PlayerVendor>();

			foreach (Mobile mob in World.Mobiles.Values)
			{
				if (mob is PlayerVendor)
				{
					PlayerVendor pv = (PlayerVendor)mob;
					list.Add(pv);
				}
			}
			e.SendGump(new FindPlayerVendorsGump(e, list, AdvertiserVendorConstants.FIRST_PAGE));
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Gump that displays a paginated list of player vendors
		/// </summary>
		public class FindPlayerVendorsGump : Gump
		{
			#region Fields

			private List<PlayerVendor> m_List;
			private int m_Page;
			private Mobile m_From;

			#endregion

			#region Constructors

			/// <summary>
			/// Creates a new FindPlayerVendorsGump
			/// </summary>
			/// <param name="from">The mobile viewing the gump</param>
			/// <param name="list">List of player vendors to display</param>
			/// <param name="page">Current page number</param>
			public FindPlayerVendorsGump(Mobile from, List<PlayerVendor> list, int page) : base(AdvertiserVendorConstants.GUMP_X, AdvertiserVendorConstants.GUMP_Y)
			{
				from.CloseGump(typeof(FindPlayerVendorsGump));
				m_Page = page;
				m_From = from;
				m_List = list ?? new List<PlayerVendor>();

				AddPage(0);
				AddBackground(0, 0, AdvertiserVendorConstants.GUMP_WIDTH, AdvertiserVendorConstants.GUMP_HEIGHT, AdvertiserVendorConstants.GRAPHIC_GUMP_BACKGROUND);
				AddBlackAlpha(AdvertiserVendorConstants.ALPHA_REGION_X, AdvertiserVendorConstants.ALPHA_REGION_Y, AdvertiserVendorConstants.ALPHA_REGION_WIDTH, AdvertiserVendorConstants.ALPHA_REGION_HEIGHT);

				if (m_List == null || m_List.Count == 0)
				{
					AddLabel(AdvertiserVendorConstants.LABEL_EMPTY_X, AdvertiserVendorConstants.LABEL_EMPTY_Y, AdvertiserVendorConstants.HUE_LABEL_TEXT, AdvertiserVendorStringConstants.MSG_NO_VENDORS);
					return;
				}

				int vendorCount = m_List.Count;
				int pageCount = CalculatePageCount(vendorCount);

				// Column headers
				AddLabelCropped(AdvertiserVendorConstants.LABEL_SHOP_NAME_X, AdvertiserVendorConstants.LABEL_SHOP_NAME_Y, AdvertiserVendorConstants.LABEL_SHOP_NAME_WIDTH, AdvertiserVendorConstants.LABEL_SHOP_NAME_HEIGHT, AdvertiserVendorConstants.HUE_LABEL_TEXT, AdvertiserVendorStringConstants.LABEL_SHOP_NAME);
				AddLabelCropped(AdvertiserVendorConstants.LABEL_OWNER_X, AdvertiserVendorConstants.LABEL_OWNER_Y, AdvertiserVendorConstants.LABEL_OWNER_WIDTH, AdvertiserVendorConstants.LABEL_OWNER_HEIGHT, AdvertiserVendorConstants.HUE_LABEL_TEXT, AdvertiserVendorStringConstants.LABEL_OWNER);
				AddLabelCropped(AdvertiserVendorConstants.LABEL_LOCATION_X, AdvertiserVendorConstants.LABEL_LOCATION_Y, AdvertiserVendorConstants.LABEL_LOCATION_WIDTH, AdvertiserVendorConstants.LABEL_LOCATION_HEIGHT, AdvertiserVendorConstants.HUE_LABEL_TEXT, AdvertiserVendorStringConstants.LABEL_LOCATION);

				// Footer message
				AddLabel(AdvertiserVendorConstants.LABEL_FOOTER_X, AdvertiserVendorConstants.LABEL_FOOTER_Y, AdvertiserVendorConstants.HUE_FOOTER_TEXT, String.Format(AdvertiserVendorStringConstants.MSG_FOOTER_FORMAT, Server.Misc.ServerList.ServerName, vendorCount));

				// Previous page button
				if (page > AdvertiserVendorConstants.FIRST_PAGE)
					AddButton(AdvertiserVendorConstants.BUTTON_PREV_X, AdvertiserVendorConstants.BUTTON_PREV_Y, AdvertiserVendorConstants.GRAPHIC_BUTTON_PREV_ACTIVE, AdvertiserVendorConstants.GRAPHIC_BUTTON_PREV_PRESSED, AdvertiserVendorConstants.BUTTON_ID_PREV, GumpButtonType.Reply, 0);
				else
					AddImage(AdvertiserVendorConstants.BUTTON_PREV_X, AdvertiserVendorConstants.BUTTON_PREV_Y, AdvertiserVendorConstants.GRAPHIC_BUTTON_PREV_DISABLED);

				// Next page button
				if (pageCount > page)
					AddButton(AdvertiserVendorConstants.BUTTON_NEXT_X, AdvertiserVendorConstants.BUTTON_NEXT_Y, AdvertiserVendorConstants.GRAPHIC_BUTTON_NEXT_ACTIVE, AdvertiserVendorConstants.GRAPHIC_BUTTON_NEXT_PRESSED, AdvertiserVendorConstants.BUTTON_ID_NEXT, GumpButtonType.Reply, 0);
				else
					AddImage(AdvertiserVendorConstants.BUTTON_NEXT_X, AdvertiserVendorConstants.BUTTON_NEXT_Y, AdvertiserVendorConstants.GRAPHIC_BUTTON_NEXT_DISABLED);

				// Add vendor details
				int startIndex = (page - 1) * AdvertiserVendorConstants.VENDORS_PER_PAGE;
				int endIndex = Math.Min(startIndex + AdvertiserVendorConstants.VENDORS_PER_PAGE, vendorCount);

				for (int i = startIndex; i < endIndex; ++i)
				{
					AddDetails(i);
				}
			}

			#endregion

			#region Helper Methods

			/// <summary>
			/// Calculates the total number of pages needed
			/// </summary>
			/// <param name="itemCount">Total number of items</param>
			/// <returns>Number of pages</returns>
			private int CalculatePageCount(int itemCount)
			{
				if (itemCount % AdvertiserVendorConstants.VENDORS_PER_PAGE == 0)
				{
					return itemCount / AdvertiserVendorConstants.VENDORS_PER_PAGE;
				}
				else
				{
					return (itemCount / AdvertiserVendorConstants.VENDORS_PER_PAGE) + 1;
				}
			}

			/// <summary>
			/// Adds a black alpha region to the gump
			/// </summary>
			private void AddBlackAlpha(int x, int y, int width, int height)
			{
				AddImageTiled(x, y, width, height, AdvertiserVendorConstants.GRAPHIC_BLACK_ALPHA);
				AddAlphaRegion(x, y, width, height);
			}

			/// <summary>
			/// Adds vendor details to the gump for the specified index
			/// </summary>
			/// <param name="index">Index of the vendor in the list</param>
			private void AddDetails(int index)
			{
				if (index < 0 || index >= m_List.Count)
				{
					return;
				}

				PlayerVendor pv = m_List[index];
				if (pv == null || pv.Deleted)
				{
					return;
				}

				try
				{
					// Get location information using helper
					string locationName;
					int teleportX;
					int teleportY;
					Map teleportMap;
					LocationMappingHelper.GetLocationInfo(pv.X, pv.Y, pv.Map, out locationName, out teleportX, out teleportY, out teleportMap);

					// Calculate row position
					int row = index % AdvertiserVendorConstants.VENDORS_PER_PAGE;
					int rowY = AdvertiserVendorConstants.ROW_START_Y + (row * AdvertiserVendorConstants.ROW_HEIGHT);

					// Format location coordinates
					int xLong = 0, yLat = 0;
					int xMins = 0, yMins = 0;
					bool xEast = false, ySouth = false;

					Point3D spot = new Point3D(teleportX, teleportY, AdvertiserVendorConstants.DEFAULT_Z);
					string locationString = pv.Location.ToString();

					if (Sextant.Format(spot, teleportMap, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth))
					{
						locationString = String.Format(AdvertiserVendorStringConstants.MSG_COORDINATE_FORMAT, yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W");
					}

					// Add labels
					AddLabel(AdvertiserVendorConstants.ROW_START_X_SHOP, rowY, AdvertiserVendorConstants.HUE_LABEL_TEXT, pv.ShopName);
					AddLabel(AdvertiserVendorConstants.ROW_START_X_OWNER, rowY, AdvertiserVendorConstants.HUE_LABEL_TEXT, pv.Owner.Name);
					AddLabel(AdvertiserVendorConstants.ROW_START_X_LOCATION, rowY, AdvertiserVendorConstants.HUE_LABEL_TEXT, String.Format("{0} {1}", locationString, locationName));
				}
				catch (Exception ex)
				{
					// Log error but don't crash the gump
					Console.WriteLine("Error adding vendor details: {0}", ex.Message);
				}
			}

			#endregion

			#region Event Handlers

			/// <summary>
			/// Handles gump button responses
			/// </summary>
			public override void OnResponse(NetState state, RelayInfo info)
			{
				Mobile from = state.Mobile;
				if (from == null)
				{
					return;
				}

				int buttonID = info.ButtonID;

				// Next page
				if (buttonID == AdvertiserVendorConstants.BUTTON_ID_NEXT)
				{
					m_Page++;
					from.CloseGump(typeof(FindPlayerVendorsGump));
					from.SendGump(new FindPlayerVendorsGump(from, m_List, m_Page));
				}
				// Previous page
				else if (buttonID == AdvertiserVendorConstants.BUTTON_ID_PREV)
				{
					m_Page--;
					if (m_Page < AdvertiserVendorConstants.FIRST_PAGE)
					{
						m_Page = AdvertiserVendorConstants.FIRST_PAGE;
					}
					from.CloseGump(typeof(FindPlayerVendorsGump));
					from.SendGump(new FindPlayerVendorsGump(from, m_List, m_Page));
				}
				// Vendor selection (teleport)
				else if (buttonID >= AdvertiserVendorConstants.BUTTON_ID_OFFSET)
				{
					int index = buttonID - AdvertiserVendorConstants.BUTTON_ID_OFFSET;
					if (index >= 0 && index < m_List.Count)
					{
						PlayerVendor pv = m_List[index];
						if (pv != null && !pv.Deleted)
						{
							Point3D dest = pv.Location;
							from.MoveToWorld(dest, pv.Map);
							from.SendGump(new FindPlayerVendorsGump(from, m_List, m_Page));
						}
					}
				}
			}

			#endregion
		}

		#endregion
	}
}

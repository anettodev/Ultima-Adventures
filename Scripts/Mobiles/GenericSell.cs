using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles.Vendors;

namespace Server.Mobiles
{
	public class GenericSellInfo : IShopSellInfo
	{
		private Dictionary<Type, int> m_Table = new Dictionary<Type, int>();
		private Type[] m_Types;

		public GenericSellInfo()
		{
		}

		public void Add( Type type, int price )
		{
			m_Table[type] = GetRandSellPriceFor( price );
			m_Types = null;
		}

		public static int GetRandSellPriceFor( int itemPrice )
		{
			// Use centralized price manager for random price calculation
			return VendorPriceManager.GetRandomSellPrice(itemPrice);
		}

		public int GetSellPriceFor( Item item, int barter )
		{
			// Get base price from table
			int basePrice = 0;
			m_Table.TryGetValue( item.GetType(), out basePrice );

			// Handle special beverage pricing
			if ( item is BaseBeverage )
			{
				int price1 = basePrice, price2 = basePrice;

				if ( item is Pitcher )
				{ price1 = 3; price2 = 5; }
				else if ( item is BeverageBottle )
				{ price1 = 3; price2 = 3; }
				else if ( item is Jug )
				{ price1 = 6; price2 = 6; }

				BaseBeverage bev = (BaseBeverage)item;

				if ( bev.IsEmpty || bev.Content == BeverageType.Milk )
					basePrice = price1;
				else
					basePrice = price2;
			}

			// Return base price - VendorPriceManager will apply all modifiers
			// Note: BaseVendor will call VendorPriceManager.CalculateSellPrice with this base price
			return basePrice;
		}

		public int GetBuyPriceFor( Item item )
		{
			// Use centralized price manager for buy-back price calculation
			int sellPrice = GetSellPriceFor(item, 0);
			return VendorPriceManager.CalculateBuyBackPrice(sellPrice);
		}

		public Type[] Types
		{
			get
			{
				if ( m_Types == null )
				{
					m_Types = new Type[m_Table.Keys.Count];
					m_Table.Keys.CopyTo( m_Types, 0 );
				}

				return m_Types;
			}
		}

		public string GetNameFor( Item item )
		{
			if ( item.Name != null )
				return item.Name;
			else
				return item.LabelNumber.ToString();
		}

		public bool IsSellable( Item item )
		{
			return IsInList( item.GetType() );
		}
	 
		public bool IsResellable( Item item )
		{
			return false;
			//return IsInList( item.GetType() );
		}

		public bool IsInList( Type type )
		{
			return m_Table.ContainsKey( type );
		}
	}
}

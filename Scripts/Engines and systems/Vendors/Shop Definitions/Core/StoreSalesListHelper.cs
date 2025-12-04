using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Misc;

namespace Server.Mobiles.Vendors.ShopDefinitions
{
	/// <summary>
	/// Helper class for creating vendor shop definitions.
	/// Reduces code duplication in StoreSalesList classes.
	/// </summary>
	public static class StoreSalesListHelper
	{
		#region Buy Item Helpers

		/// <summary>
		/// Adds a buy item with fixed quantity.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The price</param>
		/// <param name="quantity">The fixed quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue (default 0)</param>
		public static void AddBuyItem(
			List<GenericBuyInfo> list,
			Type type,
			int price,
			int quantity,
			int itemID,
			int hue = StoreSalesListConstants.HUE_DEFAULT)
		{
			list.Add(new GenericBuyInfo(type, price, quantity, itemID, hue));
		}

		/// <summary>
		/// Adds a buy item with random quantity range.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The price</param>
		/// <param name="minQty">Minimum quantity</param>
		/// <param name="maxQty">Maximum quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue (default 0)</param>
		public static void AddBuyItemRandom(
			List<GenericBuyInfo> list,
			Type type,
			int price,
			int minQty,
			int maxQty,
			int itemID,
			int hue = StoreSalesListConstants.HUE_DEFAULT)
		{
			list.Add(new GenericBuyInfo(type, price, Utility.Random(minQty, maxQty), itemID, hue));
		}

		/// <summary>
		/// Adds a buy item with random quantity range using RandomMinMax.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The price</param>
		/// <param name="minQty">Minimum quantity</param>
		/// <param name="maxQty">Maximum quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue (default 0)</param>
		public static void AddBuyItemRandomMinMax(
			List<GenericBuyInfo> list,
			Type type,
			int price,
			int minQty,
			int maxQty,
			int itemID,
			int hue = StoreSalesListConstants.HUE_DEFAULT)
		{
			list.Add(new GenericBuyInfo(type, price, Utility.RandomMinMax(minQty, maxQty), itemID, hue));
		}

		/// <summary>
		/// Adds a buy item with chance check.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The price</param>
		/// <param name="quantity">The fixed quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue (default 0)</param>
		public static void AddBuyItemWithChance(
			List<GenericBuyInfo> list,
			Type type,
			int price,
			int quantity,
			int itemID,
			int hue = StoreSalesListConstants.HUE_DEFAULT)
		{
			if (MyServerSettings.SellChance())
			{
				list.Add(new GenericBuyInfo(type, price, quantity, itemID, hue));
			}
		}

		/// <summary>
		/// Adds a buy item with chance check and random quantity.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The price</param>
		/// <param name="minQty">Minimum quantity</param>
		/// <param name="maxQty">Maximum quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue (default 0)</param>
		public static void AddBuyItemWithChanceRandom(
			List<GenericBuyInfo> list,
			Type type,
			int price,
			int minQty,
			int maxQty,
			int itemID,
			int hue = StoreSalesListConstants.HUE_DEFAULT)
		{
			if (MyServerSettings.SellChance())
			{
				list.Add(new GenericBuyInfo(type, price, Utility.Random(minQty, maxQty), itemID, hue));
			}
		}

		/// <summary>
		/// Adds a buy item with chance check and RandomMinMax quantity.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The price</param>
		/// <param name="minQty">Minimum quantity</param>
		/// <param name="maxQty">Maximum quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue (default 0)</param>
		public static void AddBuyItemWithChanceRandomMinMax(
			List<GenericBuyInfo> list,
			Type type,
			int price,
			int minQty,
			int maxQty,
			int itemID,
			int hue = StoreSalesListConstants.HUE_DEFAULT)
		{
			if (MyServerSettings.SellChance())
			{
				list.Add(new GenericBuyInfo(type, price, Utility.RandomMinMax(minQty, maxQty), itemID, hue));
			}
		}

		/// <summary>
		/// Adds a very rare buy item with chance check.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="minPrice">Minimum price</param>
		/// <param name="maxPrice">Maximum price</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue (default 0)</param>
		public static void AddVeryRareItem(
			List<GenericBuyInfo> list,
			Type type,
			int minPrice,
			int maxPrice,
			int itemID,
			int hue = StoreSalesListConstants.HUE_DEFAULT)
		{
			if (MyServerSettings.SellVeryRareChance())
			{
				list.Add(new GenericBuyInfo(type, Utility.Random(minPrice, maxPrice), StoreSalesListConstants.QTY_SINGLE, itemID, hue));
			}
		}

		/// <summary>
		/// Adds a very rare buy item with chance check, random price, and random quantity.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="minPrice">Minimum price</param>
		/// <param name="maxPrice">Maximum price</param>
		/// <param name="minQty">Minimum quantity</param>
		/// <param name="maxQty">Maximum quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue (default 0)</param>
		public static void AddVeryRareItemRandom(
			List<GenericBuyInfo> list,
			Type type,
			int minPrice,
			int maxPrice,
			int minQty,
			int maxQty,
			int itemID,
			int hue = StoreSalesListConstants.HUE_DEFAULT)
		{
			if (MyServerSettings.SellVeryRareChance())
			{
				list.Add(new GenericBuyInfo(type, Utility.Random(minPrice, maxPrice), Utility.Random(minQty, maxQty), itemID, hue));
			}
		}

		/// <summary>
		/// Adds a rare buy item with chance check.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The price</param>
		/// <param name="quantity">The fixed quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue (default 0)</param>
		public static void AddRareItem(
			List<GenericBuyInfo> list,
			Type type,
			int price,
			int quantity,
			int itemID,
			int hue = StoreSalesListConstants.HUE_DEFAULT)
		{
			if (MyServerSettings.SellRareChance())
			{
				list.Add(new GenericBuyInfo(type, price, quantity, itemID, hue));
			}
		}

		/// <summary>
		/// Adds a rare buy item with chance check and random quantity.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The price</param>
		/// <param name="minQty">Minimum quantity</param>
		/// <param name="maxQty">Maximum quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue (default 0)</param>
		public static void AddRareItemRandom(
			List<GenericBuyInfo> list,
			Type type,
			int price,
			int minQty,
			int maxQty,
			int itemID,
			int hue = StoreSalesListConstants.HUE_DEFAULT)
		{
			if (MyServerSettings.SellRareChance())
			{
				list.Add(new GenericBuyInfo(type, price, Utility.Random(minQty, maxQty), itemID, hue));
			}
		}

		/// <summary>
		/// Adds a buy item with string name.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="name">The item name</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The price</param>
		/// <param name="quantity">The fixed quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue (default 0)</param>
		public static void AddBuyItemWithName(
			List<GenericBuyInfo> list,
			string name,
			Type type,
			int price,
			int quantity,
			int itemID,
			int hue = StoreSalesListConstants.HUE_DEFAULT)
		{
			list.Add(new GenericBuyInfo(name, type, price, quantity, itemID, hue));
		}

		/// <summary>
		/// Adds a buy item with string name and random quantity.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="name">The item name</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The price</param>
		/// <param name="minQty">Minimum quantity</param>
		/// <param name="maxQty">Maximum quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue (default 0)</param>
		public static void AddBuyItemWithNameRandom(
			List<GenericBuyInfo> list,
			string name,
			Type type,
			int price,
			int minQty,
			int maxQty,
			int itemID,
			int hue = StoreSalesListConstants.HUE_DEFAULT)
		{
			list.Add(new GenericBuyInfo(name, type, price, Utility.Random(minQty, maxQty), itemID, hue));
		}

		/// <summary>
		/// Adds a buy item with string name, chance check, and random quantity.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="name">The item name</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The price</param>
		/// <param name="minQty">Minimum quantity</param>
		/// <param name="maxQty">Maximum quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue (default 0)</param>
		public static void AddBuyItemWithNameWithChanceRandom(
			List<GenericBuyInfo> list,
			string name,
			Type type,
			int price,
			int minQty,
			int maxQty,
			int itemID,
			int hue = StoreSalesListConstants.HUE_DEFAULT)
		{
			if (MyServerSettings.SellChance())
			{
				list.Add(new GenericBuyInfo(name, type, price, Utility.Random(minQty, maxQty), itemID, hue));
			}
		}

		/// <summary>
		/// Adds a buy item with arguments.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The price</param>
		/// <param name="minQty">Minimum quantity</param>
		/// <param name="maxQty">Maximum quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue</param>
		/// <param name="args">The constructor arguments</param>
		public static void AddBuyItemWithArgs(
			List<GenericBuyInfo> list,
			Type type,
			int price,
			int minQty,
			int maxQty,
			int itemID,
			int hue,
			object[] args)
		{
			list.Add(new GenericBuyInfo(type, price, Utility.RandomMinMax(minQty, maxQty), itemID, hue, args));
		}

		/// <summary>
		/// Adds a buy item with chance check, arguments, and RandomMinMax quantity.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The price</param>
		/// <param name="minQty">Minimum quantity</param>
		/// <param name="maxQty">Maximum quantity</param>
		/// <param name="itemID">The item ID</param>
		/// <param name="hue">The hue</param>
		/// <param name="args">The constructor arguments</param>
		public static void AddBuyItemWithChanceArgs(
			List<GenericBuyInfo> list,
			Type type,
			int price,
			int minQty,
			int maxQty,
			int itemID,
			int hue,
			object[] args)
		{
			if (MyServerSettings.SellChance())
			{
				list.Add(new GenericBuyInfo(type, price, Utility.RandomMinMax(minQty, maxQty), itemID, hue, args));
			}
		}

		#endregion

		#region Sell Item Helpers

		/// <summary>
		/// Adds a sell item.
		/// </summary>
		/// <param name="sellInfo">The sell info to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The sell price</param>
		public static void AddSellItem(GenericSellInfo sellInfo, Type type, int price)
		{
			sellInfo.Add(type, price);
		}

		/// <summary>
		/// Adds a sell item with chance check.
		/// </summary>
		/// <param name="sellInfo">The sell info to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The sell price</param>
		public static void AddSellItemWithChance(GenericSellInfo sellInfo, Type type, int price)
		{
			if (MyServerSettings.BuyChance())
			{
				sellInfo.Add(type, price);
			}
		}

		/// <summary>
		/// Adds a sell item with random price.
		/// </summary>
		/// <param name="sellInfo">The sell info to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="minPrice">Minimum price</param>
		/// <param name="maxPrice">Maximum price</param>
		public static void AddSellItemRandom(GenericSellInfo sellInfo, Type type, int minPrice, int maxPrice)
		{
			sellInfo.Add(type, Utility.Random(minPrice, maxPrice));
		}

		/// <summary>
		/// Adds a sell item with chance check and random price.
		/// </summary>
		/// <param name="sellInfo">The sell info to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="minPrice">Minimum price</param>
		/// <param name="maxPrice">Maximum price</param>
		public static void AddSellItemWithChanceRandom(GenericSellInfo sellInfo, Type type, int minPrice, int maxPrice)
		{
			if (MyServerSettings.BuyChance())
			{
				sellInfo.Add(type, Utility.Random(minPrice, maxPrice));
			}
		}

		/// <summary>
		/// Adds a sell item with rare chance check.
		/// </summary>
		/// <param name="sellInfo">The sell info to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="price">The sell price</param>
		public static void AddSellItemWithRareChance(GenericSellInfo sellInfo, Type type, int price)
		{
			if (MyServerSettings.BuyRareChance())
			{
				sellInfo.Add(type, price);
			}
		}

		/// <summary>
		/// Adds a sell item with rare chance check and random price.
		/// </summary>
		/// <param name="sellInfo">The sell info to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="minPrice">Minimum price</param>
		/// <param name="maxPrice">Maximum price</param>
		public static void AddSellRareItemWithChanceRandom(GenericSellInfo sellInfo, Type type, int minPrice, int maxPrice)
		{
			if (MyServerSettings.BuyRareChance())
			{
				sellInfo.Add(type, Utility.Random(minPrice, maxPrice));
			}
		}

		/// <summary>
		/// Adds a beverage buy item with random quantity.
		/// </summary>
		/// <param name="list">The list to add to</param>
		/// <param name="type">The item type</param>
		/// <param name="beverageType">The beverage type</param>
		/// <param name="price">The price</param>
		/// <param name="minQty">Minimum quantity</param>
		/// <param name="maxQty">Maximum quantity</param>
		/// <param name="itemID">The item ID</param>
		public static void AddBeverageBuyItem(
			List<GenericBuyInfo> list,
			Type type,
			BeverageType beverageType,
			int price,
			int minQty,
			int maxQty,
			int itemID)
		{
			list.Add(new GenericBuyInfo(type, price, Utility.Random(minQty, maxQty), itemID, StoreSalesListConstants.HUE_DEFAULT, new object[] { beverageType }));
		}

		#endregion
	}
}


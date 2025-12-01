using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	/// <summary>
	/// Helper class for Midland currency operations.
	/// Eliminates duplication in GetMoney() and GiveMoney() methods.
	/// Supports all 5 Midland races with their respective currencies.
	/// </summary>
	public static class MidlandMoneyHelper
	{
		#region Currency Type Definitions
		
		/// <summary>
		/// Enumeration of Midland currency types.
		/// </summary>
		public enum MidlandCurrencyType
		{
			None = 0,
			Sovereign = 1,  // Human
			Drachma = 2,    // Gargoyle
			Sslit = 3,      // Lizard
			Dubloon = 4,    // Pirate
			Skaal = 5       // Orc
		}
		
		#endregion
		
		#region Currency Type Mapping
		
		/// <summary>
		/// Dictionary mapping currency type to item type.
		/// </summary>
		private static readonly Dictionary<MidlandCurrencyType, Type> s_CurrencyTypes;
		
		/// <summary>
		/// Dictionary mapping currency type to display name.
		/// </summary>
		private static readonly Dictionary<MidlandCurrencyType, string> s_CurrencyNames;
		
		static MidlandMoneyHelper()
		{
			s_CurrencyTypes = new Dictionary<MidlandCurrencyType, Type>
			{
				{ MidlandCurrencyType.Sovereign, typeof(Sovereign) },
				{ MidlandCurrencyType.Drachma, typeof(Drachma) },
				{ MidlandCurrencyType.Sslit, typeof(Sslit) },
				{ MidlandCurrencyType.Dubloon, typeof(Dubloon) },
				{ MidlandCurrencyType.Skaal, typeof(Skaal) }
			};
			
			s_CurrencyNames = new Dictionary<MidlandCurrencyType, string>
			{
				{ MidlandCurrencyType.Sovereign, "Sovereign" },
				{ MidlandCurrencyType.Drachma, "Drachma" },
				{ MidlandCurrencyType.Sslit, "Sslits" },
				{ MidlandCurrencyType.Dubloon, "Dubloons" },
				{ MidlandCurrencyType.Skaal, "Skaals" }
			};
		}
		
		#endregion
		
		#region Currency Type Conversion
		
		/// <summary>
		/// Converts integer money type to MidlandCurrencyType enum.
		/// </summary>
		/// <param name="moneyType">Integer money type (1-5)</param>
		/// <returns>MidlandCurrencyType enum value</returns>
		public static MidlandCurrencyType GetCurrencyType(int moneyType)
		{
			if (moneyType >= 1 && moneyType <= 5)
				return (MidlandCurrencyType)moneyType;
			
			return MidlandCurrencyType.None;
		}
		
		/// <summary>
		/// Gets the currency name for a given money type.
		/// </summary>
		/// <param name="moneyType">Integer money type (1-5)</param>
		/// <returns>Currency name string</returns>
		public static string GetCurrencyName(int moneyType)
		{
			MidlandCurrencyType currencyType = GetCurrencyType(moneyType);
			string name;
			if (s_CurrencyNames.TryGetValue(currencyType, out name))
				return name;
			
			return "";
		}
		
		#endregion
		
		#region Money Operations
		
		/// <summary>
		/// Gets money from a player (from backpack or bank account).
		/// </summary>
		/// <param name="from">The player to get money from</param>
		/// <param name="amount">The amount to get</param>
		/// <param name="moneyType">The currency type (1-5)</param>
		/// <param name="vendorRace">The vendor's race (must match moneyType)</param>
		/// <returns>True if money was successfully retrieved</returns>
		public static bool GetMoney(PlayerMobile from, int amount, int moneyType, int vendorRace)
		{
			if (from == null || amount <= 0)
				return false;
			
			if (moneyType != vendorRace)
			{
				// Race mismatch - handled by caller
				return false;
			}
			
			MidlandCurrencyType currencyType = GetCurrencyType(moneyType);
			Type currencyItemType;
			
			if (!s_CurrencyTypes.TryGetValue(currencyType, out currencyItemType))
				return false;
			
			// Try to get from backpack first
			Item money = from.Backpack.FindItemByType(currencyItemType);
			if (money != null && money.Amount >= amount)
			{
				if (money.Amount == amount)
					money.Delete();
				else
					money.Amount -= amount;
				return true;
			}
			
			// Try to get from bank account
			return DeductFromAccount(from, amount, moneyType);
		}
		
		/// <summary>
		/// Gives money to a player (to backpack or bank account).
		/// </summary>
		/// <param name="to">The player to give money to</param>
		/// <param name="amount">The amount to give</param>
		/// <param name="moneyType">The currency type (1-5)</param>
		/// <param name="vendorRace">The vendor's race (must match moneyType)</param>
		/// <returns>True if money was successfully given</returns>
		public static bool GiveMoney(PlayerMobile to, int amount, int moneyType, int vendorRace)
		{
			if (to == null || amount < 0)
				return false;
			
			if (moneyType != vendorRace)
			{
				// Race mismatch - handled by caller
				return false;
			}
			
			MidlandCurrencyType currencyType = GetCurrencyType(moneyType);
			Type currencyItemType;
			
			if (!s_CurrencyTypes.TryGetValue(currencyType, out currencyItemType))
				return false;
			
			// If amount is below threshold, give physical currency
			if (amount < MidlandVendorConstants.BANK_DEPOSIT_THRESHOLD)
			{
				to.AddToBackpack((Item)Activator.CreateInstance(currencyItemType, amount));
				return true;
			}
			
			// Otherwise, deposit to bank account
			return AddToAccount(to, amount, moneyType);
		}
		
		#endregion
		
		#region Account Operations
		
		/// <summary>
		/// Gets the account balance for a player based on race.
		/// </summary>
		/// <param name="from">The player</param>
		/// <param name="moneyType">The currency type (1-5)</param>
		/// <param name="vendorRace">The vendor's race</param>
		/// <returns>Account balance, or 0 if race mismatch or no account</returns>
		public static int GetAccountBalance(PlayerMobile from, int moneyType, int vendorRace)
		{
			if (from == null || moneyType != vendorRace)
				return 0;
			
			switch (moneyType)
			{
				case MidlandVendorConstants.RACE_HUMAN:
					return from.midhumanacc;
				case MidlandVendorConstants.RACE_GARGOYLE:
					return from.midgargoyleacc;
				case MidlandVendorConstants.RACE_LIZARD:
					return from.midlizardacc;
				case MidlandVendorConstants.RACE_PIRATE:
					return from.midpirateacc;
				case MidlandVendorConstants.RACE_ORC:
					return from.midorcacc;
				default:
					return 0;
			}
		}
		
		/// <summary>
		/// Deducts money from a player's bank account.
		/// </summary>
		/// <param name="from">The player</param>
		/// <param name="amount">The amount to deduct</param>
		/// <param name="moneyType">The currency type (1-5)</param>
		/// <returns>True if deduction was successful</returns>
		private static bool DeductFromAccount(PlayerMobile from, int amount, int moneyType)
		{
			switch (moneyType)
			{
				case MidlandVendorConstants.RACE_HUMAN:
					if (from.midhumanacc >= amount)
					{
						from.midhumanacc -= amount;
						return true;
					}
					break;
				case MidlandVendorConstants.RACE_GARGOYLE:
					if (from.midgargoyleacc >= amount)
					{
						from.midgargoyleacc -= amount;
						return true;
					}
					break;
				case MidlandVendorConstants.RACE_LIZARD:
					if (from.midlizardacc >= amount)
					{
						from.midlizardacc -= amount;
						return true;
					}
					break;
				case MidlandVendorConstants.RACE_PIRATE:
					if (from.midpirateacc >= amount)
					{
						from.midpirateacc -= amount;
						return true;
					}
					break;
				case MidlandVendorConstants.RACE_ORC:
					if (from.midorcacc >= amount)
					{
						from.midorcacc -= amount;
						return true;
					}
					break;
			}
			
			return false;
		}
		
		/// <summary>
		/// Adds money to a player's bank account.
		/// </summary>
		/// <param name="to">The player</param>
		/// <param name="amount">The amount to add</param>
		/// <param name="moneyType">The currency type (1-5)</param>
		/// <returns>True if addition was successful</returns>
		private static bool AddToAccount(PlayerMobile to, int amount, int moneyType)
		{
			switch (moneyType)
			{
				case MidlandVendorConstants.RACE_HUMAN:
					to.midhumanacc += amount;
					return true;
				case MidlandVendorConstants.RACE_GARGOYLE:
					to.midgargoyleacc += amount;
					return true;
				case MidlandVendorConstants.RACE_LIZARD:
					to.midlizardacc += amount;
					return true;
				case MidlandVendorConstants.RACE_PIRATE:
					to.midpirateacc += amount;
					return true;
				case MidlandVendorConstants.RACE_ORC:
					to.midorcacc += amount;
					return true;
			}
			
			return false;
		}
		
		#endregion
	}
}


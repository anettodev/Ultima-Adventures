using System;
using Server;
using Server.Misc;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;

namespace Server.Items
{
	/// <summary>
	/// Helper class for name change system validation and logic.
	/// Extracted from CensusRecords.cs, NameAlterGump.cs, and NameChangeGump.cs
	/// to improve code organization and reusability.
	/// </summary>
	public static class NameChangeHelper
	{
		/// <summary>
		/// Extracts and trims text from a gump text entry.
		/// </summary>
		/// <param name="info">RelayInfo from gump response</param>
		/// <param name="id">Text entry ID</param>
		/// <returns>Trimmed text string, or null if entry doesn't exist</returns>
		public static string GetString(RelayInfo info, int id)
		{
			TextRelay t = info.GetTextEntry(id);
			return (t == null ? null : t.Text.Trim());
		}

		/// <summary>
		/// Validates a character name using NameVerification.
		/// </summary>
		/// <param name="name">Name to validate</param>
		/// <returns>True if name is valid, false otherwise</returns>
		public static bool ValidateName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}

			return NameVerification.Validate(
				name,
				NameChangeConstants.NAME_MIN_LENGTH,
				NameChangeConstants.NAME_MAX_LENGTH,
				true,
				false,
				true,
				NameChangeConstants.NAME_VERIFICATION_PARAM,
				NameVerification.SpaceOnly
			);
		}

		/// <summary>
		/// Checks if name is duplicate and applies the name change if valid.
		/// </summary>
		/// <param name="from">Mobile requesting name change</param>
		/// <param name="name">New name to apply</param>
		/// <returns>True if name change was successful, false otherwise</returns>
		public static bool ApplyNameChange(Mobile from, string name)
		{
			if (from == null || string.IsNullOrEmpty(name))
			{
				return false;
			}

			if (!CharacterCreation.CheckDupe(from, name))
			{
				return false;
			}

			from.SendMessage(NameChangeConstants.MSG_COLOR_INFO, NameChangeStringConstants.MSG_NAME_CHANGED_FORMAT, name);
			from.Name = name;
			from.CantWalk = false;
			return true;
		}

		/// <summary>
		/// Checks if player has enough gold and consumes it for Census Records name change.
		/// </summary>
		/// <param name="from">Mobile requesting name change</param>
		/// <returns>True if gold was consumed successfully, false otherwise</returns>
		public static bool CheckAndConsumeGold(Mobile from)
		{
			if (from == null)
			{
				return false;
			}

			Container pack = from.Backpack;
			if (pack == null)
			{
				return false;
			}

			return pack.ConsumeTotal(typeof(Gold), NameChangeConstants.CENSUS_NAME_CHANGE_COST);
		}

		/// <summary>
		/// Validates name and checks for duplicates, then applies name change if valid.
		/// Sends appropriate error messages if validation fails.
		/// </summary>
		/// <param name="from">Mobile requesting name change</param>
		/// <param name="name">Name to validate and apply</param>
		/// <param name="requireGold">If true, requires gold payment (Census Records)</param>
		/// <returns>True if name change was successful, false otherwise</returns>
		public static bool ValidateAndApplyNameChange(Mobile from, string name, bool requireGold)
		{
			if (from == null)
			{
				return false;
			}

			// Check if name is placeholder text
			if (name == NameChangeStringConstants.PLACEHOLDER_TYPE_HERE)
			{
				return false;
			}

			// Validate name format
			if (!ValidateName(name))
			{
				from.SendMessage(NameChangeConstants.MSG_COLOR_INFO, NameChangeStringConstants.MSG_NAME_UNACCEPTABLE);
				return false;
			}

			// Check gold requirement if needed
			if (requireGold)
			{
				if (!CheckAndConsumeGold(from))
				{
					from.SendMessage(NameChangeConstants.MSG_COLOR_INFO, NameChangeStringConstants.MSG_INSUFFICIENT_GOLD);
					return false;
				}
			}

			// Apply name change - if it fails, CheckDupe failed (name already taken)
			if (!ApplyNameChange(from, name))
			{
				from.SendMessage(NameChangeConstants.MSG_COLOR_INFO, NameChangeStringConstants.MSG_NAME_UNACCEPTABLE);
				return false;
			}

			return true;
		}
	}
}


using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Gumps
{
	/// <summary>
	/// Gump for displaying and managing Taming BOD Book entries.
	/// Allows players to add pets to contracts and remove completed deeds.
	/// </summary>
	public class TamingBODBookGump : Gump
	{
		#region Fields

		private Mobile m_From;
		private TamingBODBook m_Book;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TamingBODBookGump class.
		/// </summary>
		/// <param name="from">The player viewing the gump</param>
		/// <param name="book">The Taming BOD Book to display</param>
		public TamingBODBookGump(Mobile from, TamingBODBook book) : base(TamingBODBookGumpConstants.GUMP_PAGE_ID, TamingBODBookGumpConstants.GUMP_PAGE_ID)
		{
			from.CloseGump(typeof(TamingBODBookGump));

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			m_From = from;
			m_Book = book;

			AddPage(TamingBODBookGumpConstants.GUMP_PAGE_ID);
			AddGumpElements();
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles gump button responses.
		/// </summary>
		/// <param name="state">The network state</param>
		/// <param name="info">The relay information</param>
		public override void OnResponse(NetState state, RelayInfo info)
		{
			if (info.ButtonID > 0)
			{
				if (info.ButtonID >= TamingBODBookGumpConstants.BUTTON_ADD_BASE_ID)
				{
					HandleAddPetButton(info.ButtonID);
				}
				else if (info.ButtonID >= TamingBODBookGumpConstants.BUTTON_REMOVE_BASE_ID)
				{
					HandleRemoveDeedButton(info.ButtonID);
				}

				m_From.SendGump(new TamingBODBookGump(m_From, m_Book));
			}
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Adds all gump elements (background, title, headers, entries).
		/// </summary>
		private void AddGumpElements()
		{
			int gumpHeight = TamingBODBookGumpConstants.GUMP_HEADER_HEIGHT + (m_Book.Entries.Count * TamingBODBookGumpConstants.ENTRY_ROW_HEIGHT);

			AddBackground(
				TamingBODBookGumpConstants.GUMP_X_OFFSET,
				TamingBODBookGumpConstants.GUMP_Y_OFFSET,
				TamingBODBookGumpConstants.GUMP_WIDTH,
				gumpHeight,
				TamingBODBookGumpConstants.GUMP_BACKGROUND_ID);

			AddTitle();
			AddHeaderRow();

			for (int i = 0; i < m_Book.Entries.Count; ++i)
			{
				AddEntryRow(i, m_Book.Entries[i]);
			}
		}

		/// <summary>
		/// Adds the title region and label.
		/// </summary>
		private void AddTitle()
		{
			AddAlphaRegion(
				TamingBODBookGumpConstants.TITLE_REGION_X,
				TamingBODBookGumpConstants.TITLE_REGION_Y,
				TamingBODBookGumpConstants.TITLE_REGION_WIDTH,
				TamingBODBookGumpConstants.TITLE_REGION_HEIGHT);

			// Center the title in the title region
			string title = TamingBODBookGumpStringConstants.GUMP_TITLE;
			int titleWidth = title.Length * 6; // Approximate character width
			int centeredX = TamingBODBookGumpConstants.TITLE_REGION_X + (TamingBODBookGumpConstants.TITLE_REGION_WIDTH / 2) - (titleWidth / 2);

			AddLabel(
				centeredX,
				TamingBODBookGumpConstants.TITLE_LABEL_Y,
				TamingBODBookGumpConstants.LABEL_COLOR,
				title);
		}

		/// <summary>
		/// Adds the header row with column labels.
		/// </summary>
		private void AddHeaderRow()
		{
			AddAlphaRegion(TamingBODBookGumpConstants.COLUMN_1_X, TamingBODBookGumpConstants.HEADER_ROW_Y, TamingBODBookGumpConstants.COLUMN_1_WIDTH, TamingBODBookGumpConstants.ROW_HEIGHT);
			AddAlphaRegion(TamingBODBookGumpConstants.COLUMN_2_X, TamingBODBookGumpConstants.HEADER_ROW_Y, TamingBODBookGumpConstants.COLUMN_2_WIDTH, TamingBODBookGumpConstants.ROW_HEIGHT);
			AddAlphaRegion(TamingBODBookGumpConstants.COLUMN_3_X, TamingBODBookGumpConstants.HEADER_ROW_Y, TamingBODBookGumpConstants.COLUMN_3_WIDTH, TamingBODBookGumpConstants.ROW_HEIGHT);
			AddAlphaRegion(TamingBODBookGumpConstants.COLUMN_4_X, TamingBODBookGumpConstants.HEADER_ROW_Y, TamingBODBookGumpConstants.COLUMN_4_WIDTH, TamingBODBookGumpConstants.ROW_HEIGHT);

			// Center labels in their columns
			string contractLabel = TamingBODBookGumpStringConstants.LABEL_CONTRACT;
			string tamedLabel = TamingBODBookGumpStringConstants.LABEL_TAMED;
			string toTameLabel = TamingBODBookGumpStringConstants.LABEL_TO_TAME;
			string rewardLabel = TamingBODBookGumpStringConstants.LABEL_REWARD;

			int label1X = TamingBODBookGumpConstants.COLUMN_1_X + (TamingBODBookGumpConstants.COLUMN_1_WIDTH / 2) - (contractLabel.Length * 3);
			int label2X = TamingBODBookGumpConstants.COLUMN_2_X + (TamingBODBookGumpConstants.COLUMN_2_WIDTH / 2) - (tamedLabel.Length * 3);
			int label3X = TamingBODBookGumpConstants.COLUMN_3_X + (TamingBODBookGumpConstants.COLUMN_3_WIDTH / 2) - (toTameLabel.Length * 3);
			int label4X = TamingBODBookGumpConstants.COLUMN_4_X + (TamingBODBookGumpConstants.COLUMN_4_WIDTH / 2) - (rewardLabel.Length * 3);

			AddLabel(label1X, TamingBODBookGumpConstants.HEADER_ROW_Y - 1, TamingBODBookGumpConstants.LABEL_COLOR, contractLabel);
			AddLabel(label2X, TamingBODBookGumpConstants.HEADER_ROW_Y - 1, TamingBODBookGumpConstants.LABEL_COLOR, tamedLabel);
			AddLabel(label3X, TamingBODBookGumpConstants.HEADER_ROW_Y - 1, TamingBODBookGumpConstants.LABEL_COLOR, toTameLabel);
			AddLabel(label4X, TamingBODBookGumpConstants.HEADER_ROW_Y - 1, TamingBODBookGumpConstants.LABEL_COLOR, rewardLabel);
		}

		/// <summary>
		/// Adds a single entry row with data and buttons.
		/// </summary>
		/// <param name="index">The entry index</param>
		/// <param name="entry">The entry to display</param>
		private void AddEntryRow(int index, TamingBODEntry entry)
		{
			int rowY = TamingBODBookGumpConstants.FIRST_ENTRY_ROW_Y + (index * TamingBODBookGumpConstants.ENTRY_ROW_HEIGHT);
			int labelY = TamingBODBookGumpConstants.ENTRY_LABEL_Y_OFFSET + (index * TamingBODBookGumpConstants.ENTRY_ROW_HEIGHT);
			int buttonY = TamingBODBookGumpConstants.BUTTON_Y_OFFSET + (index * TamingBODBookGumpConstants.ENTRY_ROW_HEIGHT);

			AddAlphaRegion(TamingBODBookGumpConstants.COLUMN_1_X, rowY, TamingBODBookGumpConstants.COLUMN_1_WIDTH, TamingBODBookGumpConstants.ROW_HEIGHT);
			AddAlphaRegion(TamingBODBookGumpConstants.COLUMN_2_X, rowY, TamingBODBookGumpConstants.COLUMN_2_WIDTH, TamingBODBookGumpConstants.ROW_HEIGHT);
			AddAlphaRegion(TamingBODBookGumpConstants.COLUMN_3_X, rowY, TamingBODBookGumpConstants.COLUMN_3_WIDTH, TamingBODBookGumpConstants.ROW_HEIGHT);
			AddAlphaRegion(TamingBODBookGumpConstants.COLUMN_4_X, rowY, TamingBODBookGumpConstants.COLUMN_4_WIDTH, TamingBODBookGumpConstants.ROW_HEIGHT);

			// Center content in columns
			// Build contract name based on tier and creature type
			string contractName;
			if (entry.Tier == 1 || entry.CreatureType == null)
			{
				// Generic contract
				contractName = string.Format(TamingBODBookGumpStringConstants.CONTRACT_NAME_FORMAT_GENERIC, entry.AmountToTame);
			}
			else
			{
				// Specific creature type contract
				string creatureTypeName = TamingBODCreatureTypes.GetCreatureTypeName(entry.CreatureType, entry.Tier);
				if (creatureTypeName != null)
				{
					contractName = string.Format(TamingBODBookGumpStringConstants.CONTRACT_NAME_FORMAT_SPECIFIC, entry.AmountToTame, creatureTypeName);
				}
				else
				{
					// Fallback to generic if name not found
					contractName = string.Format(TamingBODBookGumpStringConstants.CONTRACT_NAME_FORMAT_GENERIC, entry.AmountToTame);
				}
			}

			string amountTamed = entry.AmountTamed.ToString();
			string amountToTame = entry.AmountToTame.ToString();
			string reward = entry.Reward.ToString();

			int label1X = TamingBODBookGumpConstants.COLUMN_1_X + (TamingBODBookGumpConstants.COLUMN_1_WIDTH / 2) - (contractName.Length * 3);
			int label2X = TamingBODBookGumpConstants.COLUMN_2_X + (TamingBODBookGumpConstants.COLUMN_2_WIDTH / 2) - (amountTamed.Length * 3);
			int label3X = TamingBODBookGumpConstants.COLUMN_3_X + (TamingBODBookGumpConstants.COLUMN_3_WIDTH / 2) - (amountToTame.Length * 3);
			int label4X = TamingBODBookGumpConstants.COLUMN_4_X + (TamingBODBookGumpConstants.COLUMN_4_WIDTH / 2) - (reward.Length * 3);

			// Use green color if contract is complete, otherwise white
			bool isComplete = entry.AmountTamed >= entry.AmountToTame;
			int labelColor = isComplete ? TamingBODBookGumpConstants.LABEL_GREEN_COLOR : TamingBODBookGumpConstants.LABEL_WHITE_COLOR;

			AddLabel(label1X, labelY, labelColor, contractName);
			AddLabel(label2X, labelY, labelColor, amountTamed);
			AddLabel(label3X, labelY, labelColor, amountToTame);
			AddLabel(label4X, labelY, labelColor, reward);

			AddButton(
				TamingBODBookGumpConstants.BUTTON_ADD_X,
				buttonY,
				TamingBODBookGumpConstants.BUTTON_ADD_NORMAL_ID,
				TamingBODBookGumpConstants.BUTTON_ADD_PRESSED_ID,
				TamingBODBookGumpConstants.BUTTON_ADD_BASE_ID + index,
				GumpButtonType.Reply,
				0);

			AddButton(
				TamingBODBookGumpConstants.BUTTON_REMOVE_X,
				buttonY,
				TamingBODBookGumpConstants.BUTTON_REMOVE_NORMAL_ID,
				TamingBODBookGumpConstants.BUTTON_REMOVE_PRESSED_ID,
				TamingBODBookGumpConstants.BUTTON_REMOVE_BASE_ID + index,
				GumpButtonType.Reply,
				0);
		}

		/// <summary>
		/// Gets the entry index from a button ID.
		/// </summary>
		/// <param name="buttonID">The button ID</param>
		/// <returns>The entry index, or -1 if invalid</returns>
		private int GetEntryIndex(int buttonID)
		{
			return buttonID % TamingBODBookGumpConstants.BUTTON_REMOVE_BASE_ID;
		}

		/// <summary>
		/// Gets an entry from a button ID, with bounds checking.
		/// </summary>
		/// <param name="buttonID">The button ID</param>
		/// <returns>The entry, or null if invalid</returns>
		private TamingBODEntry GetEntryFromButtonID(int buttonID)
		{
			int index = GetEntryIndex(buttonID);

			if (index < 0 || index >= m_Book.Entries.Count)
			{
				return null;
			}

			return m_Book.Entries[index];
		}

		/// <summary>
		/// Handles the "Add Pet" button click.
		/// </summary>
		/// <param name="buttonID">The button ID</param>
		private void HandleAddPetButton(int buttonID)
		{
			TamingBODEntry entry = GetEntryFromButtonID(buttonID);

			if (entry == null)
			{
				return;
			}

			// Check if contract is already complete (can't add more pets)
			if (entry.AmountTamed >= entry.AmountToTame)
			{
				m_From.SendMessage(TamingBODBookGumpStringConstants.MSG_CANNOT_ADD_PET);
				return;
			}

			m_From.SendMessage(TamingBODBookGumpStringConstants.MSG_CHOOSE_TAMABLE);
			m_From.Target = new TameCreatureBookTarget(m_Book, GetEntryIndex(buttonID));
		}

		/// <summary>
		/// Handles the "Remove Deed" button click.
		/// </summary>
		/// <param name="buttonID">The button ID</param>
		private void HandleRemoveDeedButton(int buttonID)
		{
			TamingBODEntry entry = GetEntryFromButtonID(buttonID);

			if (entry == null)
			{
				return;
			}

			int index = GetEntryIndex(buttonID);

			if (index < 0 || index >= m_Book.Entries.Count)
			{
				return;
			}

			// Preserve tier and creature type when restoring contract from book
			TamingBOD tamingBOD = new TamingBOD(entry.AmountTamed, entry.AmountToTame, entry.Reward, entry.Tier, entry.CreatureType);
			m_From.AddToBackpack(tamingBOD);
			m_Book.Entries.RemoveAt(index);
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Target class for selecting a pet to add to a contract.
		/// </summary>
		private class TameCreatureBookTarget : Target
		{
			#region Fields

			private TamingBODEntry m_Entry;
			private TamingBODBook m_Book;

			#endregion

			#region Constructors

			/// <summary>
			/// Initializes a new instance of the TameCreatureBookTarget class.
			/// </summary>
			/// <param name="book">The Taming BOD Book</param>
			/// <param name="entryIndex">The entry index</param>
			public TameCreatureBookTarget(TamingBODBook book, int entryIndex) : base(TamingBODBookGumpConstants.TARGET_RANGE_UNLIMITED, true, TargetFlags.None)
			{
				if (entryIndex >= 0 && entryIndex < book.Entries.Count)
				{
					m_Entry = book.Entries[entryIndex];
				}

				m_Book = book;
			}

			#endregion

			#region Core Logic

			/// <summary>
			/// Handles the target selection.
			/// </summary>
			/// <param name="from">The player who selected the target</param>
			/// <param name="o">The object that was targeted</param>
			protected override void OnTarget(Mobile from, object o)
			{
				if (m_Entry == null)
				{
					return;
				}

				BaseCreature pet = o as BaseCreature;
				if (pet == null)
				{
					from.SendMessage(TamingBODBookGumpStringConstants.MSG_NOT_TAMABLE_PET);
					return;
				}

				string errorMessage = ValidatePetForContract(pet, from);
				if (errorMessage != null)
				{
					if (errorMessage.Length > 0)
					{
						from.SendMessage(errorMessage);
					}
					return;
				}

				if (pet.Tamable)
				{
					ProcessPetForContract(pet, from);
					}
				else
				{
					from.SendMessage(TamingBODBookGumpStringConstants.MSG_PET_WONT_WORK);
				}
			}

			#endregion

			#region Helper Methods

			/// <summary>
			/// Validates if a pet can be added to the contract.
			/// Uses PetValidationHelper for consistency with TamingBODGump.
			/// </summary>
			/// <param name="pet">The pet to validate</param>
			/// <param name="from">The player attempting to add the pet</param>
			/// <returns>Error message if validation fails, null if valid</returns>
			private string ValidatePetForContract(BaseCreature pet, Mobile from)
			{
				// Use PetValidationHelper with tier and creature type checking
				return PetValidationHelper.ValidatePetForContract(
					pet,
					from,
					TamingBODBookGumpConstants.PET_COMBAT_RANGE,
					m_Entry.Tier,
					m_Entry.CreatureType);
			}

			/// <summary>
			/// Processes a pet for the contract (adds reward, increments count, deletes pet).
			/// Uses tier-based reward system for consistency with TamingBODGump.
			/// </summary>
			/// <param name="pet">The pet to process</param>
			/// <param name="from">The player who owns the pet</param>
			private void ProcessPetForContract(BaseCreature pet, Mobile from)
			{
				// Use tier-based reward system
				int reward = PetValidationHelper.CalculateTierBasedReward(m_Entry.Tier);
				m_Entry.Reward += reward;
				m_Entry.AmountTamed += 1;

				// Use PetValidationHelper for consistent pet processing
				PetValidationHelper.ProcessPetForContract(pet);

				from.CloseGump(typeof(TamingBODBookGump));
				from.SendGump(new TamingBODBookGump(from, m_Book));
			}

			#endregion
		}

		#endregion
	}
}

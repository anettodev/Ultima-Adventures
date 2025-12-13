using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Gumps
{
	/// <summary>
	/// Gump for displaying and managing Taming BOD contracts.
	/// Allows players to add pets to contracts and claim rewards.
	/// </summary>
	public class TamingBODGump : Gump
	{
		#region Fields

		private TamingBOD m_Parent;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TamingBODGump class.
		/// </summary>
		/// <param name="from">The player viewing the gump</param>
		/// <param name="parentBOD">The Taming BOD to display</param>
		public TamingBODGump(Mobile from, TamingBOD parentBOD) : base(TamingBODGumpConstants.GUMP_PAGE_ID, TamingBODGumpConstants.GUMP_PAGE_ID)
		{
			if (from != null)
			{
				from.CloseGump(typeof(TamingBODGump));
			}

			if (parentBOD != null)
			{
				m_Parent = parentBOD;

				Closable = true;
				Disposable = true;
				Dragable = true;
				Resizable = false;

				AddPage(TamingBODGumpConstants.GUMP_PAGE_ID);
				AddGumpElements();
			}
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
			Mobile from = state.Mobile;

			if (from != null && m_Parent != null)
			{
				if (info.ButtonID == TamingBODGumpConstants.BUTTON_ADD_ID)
				{
					from.SendMessage(TamingBODGumpStringConstants.MSG_CHOOSE_TAMED_CREATURE);
					from.Target = new TamingBODTarget(m_Parent);
				}
				else if (info.ButtonID == TamingBODGumpConstants.BUTTON_REWARD_ID)
				{
					if (TamingBOD.PayRewardTo(from, m_Parent))
					{
						m_Parent.Delete();
					}
				}
			}
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Adds all gump elements (background, labels, buttons).
		/// </summary>
		private void AddGumpElements()
		{
			AddBackground(
				TamingBODGumpConstants.GUMP_X,
				TamingBODGumpConstants.GUMP_Y,
				TamingBODGumpConstants.GUMP_WIDTH,
				TamingBODGumpConstants.GUMP_HEIGHT,
				TamingBODGumpConstants.GUMP_BACKGROUND_ID);

			AddLabels();

			if (m_Parent.AmountTamed < m_Parent.AmountToTame)
			{
				AddAddCreatureButton();
			}
			else
			{
				AddRewardButton();
			}
		}

		/// <summary>
		/// Adds all labels to the gump.
		/// </summary>
		private void AddLabels()
		{
			// Build contract label based on tier and creature type
			string contractLabel;
			if (m_Parent.Tier == 1 || m_Parent.CreatureType == null)
			{
				// Generic contract
				contractLabel = string.Format(TamingBODGumpStringConstants.LABEL_CONTRACT_FORMAT, m_Parent.AmountToTame);
			}
			else
			{
				// Specific creature type contract
				string creatureTypeName = TamingBODCreatureTypes.GetCreatureTypeName(m_Parent.CreatureType, m_Parent.Tier);
				if (creatureTypeName != null)
				{
					contractLabel = string.Format(TamingBODGumpStringConstants.LABEL_CONTRACT_FORMAT_SPECIFIC, m_Parent.AmountToTame, creatureTypeName);
				}
				else
				{
					// Fallback to generic if name not found
					contractLabel = string.Format(TamingBODGumpStringConstants.LABEL_CONTRACT_FORMAT, m_Parent.AmountToTame);
				}
			}

			AddLabel(
				TamingBODGumpConstants.LABEL_X,
				TamingBODGumpConstants.LABEL_CONTRACT_Y,
				TamingBODGumpConstants.LABEL_COLOR,
				contractLabel);

			AddLabel(
				TamingBODGumpConstants.LABEL_X,
				TamingBODGumpConstants.LABEL_QUANTITY_Y,
				TamingBODGumpConstants.LABEL_COLOR,
				string.Format(TamingBODGumpStringConstants.LABEL_QUANTITY_FORMAT, m_Parent.AmountTamed));

			AddLabel(
				TamingBODGumpConstants.LABEL_X,
				TamingBODGumpConstants.LABEL_REWARD_Y,
				TamingBODGumpConstants.LABEL_COLOR,
				string.Format(TamingBODGumpStringConstants.LABEL_REWARD_FORMAT, m_Parent.Reward));
		}

		/// <summary>
		/// Adds the "Add creature" button.
		/// </summary>
		private void AddAddCreatureButton()
		{
			AddButton(
				TamingBODGumpConstants.BUTTON_X,
				TamingBODGumpConstants.BUTTON_Y,
				TamingBODGumpConstants.BUTTON_NORMAL_ID ,
				TamingBODGumpConstants.BUTTON_NORMAL_ID,
				TamingBODGumpConstants.BUTTON_ADD_ID,
				GumpButtonType.Reply,
				0);

			AddLabel(
				TamingBODGumpConstants.BUTTON_LABEL_X,
				TamingBODGumpConstants.BUTTON_LABEL_Y_REWARD,
				1153,
				TamingBODGumpStringConstants.BUTTON_ADD_CREATURE);
		}

		/// <summary>
		/// Adds the "Reward" button.
		/// </summary>
		private void AddRewardButton()
		{
			AddButton(
				TamingBODGumpConstants.BUTTON_X,
				TamingBODGumpConstants.BUTTON_Y,
				TamingBODGumpConstants.BUTTON_REWARD_PRESSED_ID,
				TamingBODGumpConstants.BUTTON_REWARD_PRESSED_ID,
				TamingBODGumpConstants.BUTTON_REWARD_ID,
				GumpButtonType.Reply,
				0);

			AddLabel(
				TamingBODGumpConstants.BUTTON_LABEL_X_REWARD,
				TamingBODGumpConstants.BUTTON_LABEL_Y_REWARD,
				TamingBODGumpConstants.LABEL_WHITE_COLOR,
				TamingBODGumpStringConstants.BUTTON_REWARD);
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Target class for selecting a pet to add to a contract.
		/// </summary>
		private class TamingBODTarget : Target
		{
			#region Fields

			private TamingBOD m_Parent;

			#endregion

			#region Constructors

			/// <summary>
			/// Initializes a new instance of the TamingBODTarget class.
			/// </summary>
			/// <param name="parentBOD">The Taming BOD</param>
			public TamingBODTarget(TamingBOD parentBOD) : base(TamingBODGumpConstants.TARGET_RANGE_UNLIMITED, true, TargetFlags.None)
			{
				m_Parent = parentBOD;
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
				if (m_Parent == null || from == null || o == null)
				{
					return;
				}

				BaseCreature pet = o as BaseCreature;
				if (pet == null)
				{
					from.SendMessage(TamingBODGumpStringConstants.MSG_NOT_TAMABLE_PET);
					return;
				}

				string errorMessage = PetValidationHelper.ValidatePetForContract(pet, from, TamingBODGumpConstants.PET_COMBAT_RANGE, m_Parent.Tier, m_Parent.CreatureType);
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
					from.SendMessage(TamingBODGumpStringConstants.MSG_PET_WONT_WORK);
				}
			}

			#endregion

			#region Helper Methods

			/// <summary>
			/// <summary>
			/// Processes a pet for the contract (adds reward, increments count, deletes pet).
			/// Uses dynamic reward calculation based on pet value with 15-35% bonus.
			/// </summary>
			/// <param name="pet">The pet to process</param>
			/// <param name="from">The player who owns the pet</param>
			private void ProcessPetForContract(BaseCreature pet, Mobile from)
			{
				// Use dynamic reward system (pet value + 15-35% bonus)
				int reward = PetValidationHelper.CalculateDynamicContractReward(pet, from);
				m_Parent.Reward += reward;
				m_Parent.AmountTamed += 1;
				m_Parent.InvalidateProperties();

				PetValidationHelper.ProcessPetForContract(pet);
			}

			#endregion
		}

		#endregion
	}
}

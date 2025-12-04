using System;
using System.Collections.Generic;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Gumps;

namespace Server.Mobiles
{
	/// <summary>
	/// Banker NPC that handles standard banking operations.
	/// Provides deposit, withdrawal, and balance checking services.
	/// In Midland regions, handles race-specific currencies.
	/// Does not provide skill training (CanTeach = false).
	/// </summary>
	public class Banker : BaseVendor
	{
		#region Fields
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		#endregion

		#region Properties
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }
		public override NpcGuild NpcGuild{ get{ return NpcGuild.MerchantsGuild; } }

		/// <summary>
		/// Banker NPCs cannot teach skills
		/// </summary>
		public override bool CanTeach { get { return false; } }
		#endregion

		#region Constructors
		[Constructable]
		public Banker() : base( "o Banqueiro" )
		{
			Job = JobFragment.banker;
			Karma = Utility.RandomMinMax(BankerConstants.KARMA_MIN, BankerConstants.KARMA_MAX);
		}
		#endregion

		#region Vendor Setup
		/// <summary>
		/// Initializes the banker's shop buy/sell information.
		/// Bankers use SBBanker for their vendor functionality.
		/// </summary>
		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBBanker() );
		}
		#endregion

		#region Banking Operations
		/// <summary>
		/// Gets the total balance (gold + bank checks) in a player's bank box
		/// </summary>
		/// <param name="from">The mobile whose balance to check</param>
		/// <returns>The total balance in gold pieces</returns>
		public static int GetBalance( Mobile from )
		{
			Item[] gold, checks;

			return GetBalance( from, out gold, out checks );
		}

		/// <summary>
		/// Gets the total balance and returns arrays of gold/check items
		/// </summary>
		/// <param name="from">The mobile whose balance to check</param>
		/// <param name="gold">Output array of gold items in bank</param>
		/// <param name="checks">Output array of bank check items in bank</param>
		/// <returns>The total balance in gold pieces</returns>
		public static int GetBalance( Mobile from, out Item[] gold, out Item[] checks )
		{
			int balance = 0;

			Container bank = from.FindBankNoCreate();

			if ( bank != null )
			{
				gold = bank.FindItemsByType( typeof( Gold ) );
				checks = bank.FindItemsByType( typeof( BankCheck ) );

				for ( int i = 0; i < gold.Length; ++i )
					balance += gold[i].Amount;

				for ( int i = 0; i < checks.Length; ++i )
					balance += ((BankCheck)checks[i]).Worth;
			}
			else
			{
				gold = checks = new Item[0];
			}

			return balance;
		}

		/// <summary>
		/// Withdraws the specified amount from the player's account.
		/// Handles both standard bank box withdrawals and Midland currency withdrawals.
		/// </summary>
		/// <param name="from">The mobile making the withdrawal</param>
		/// <param name="amount">The amount to withdraw</param>
		/// <returns>True if withdrawal was successful, false otherwise</returns>
		public static bool Withdraw( Mobile from, int amount )
		{
			if (!AdventuresFunctions.IsInMidland((object)from))
			{
				// Standard withdrawal from bank box
				return WithdrawFromBankBox(from, amount);
			}
			else if (AdventuresFunctions.IsInMidland((object)from) && from is PlayerMobile && ((PlayerMobile)from).midrace != 0)
			{
				// Midland currency withdrawal from player account
				return MidlandCurrencyHandler.WithdrawFromAccount((PlayerMobile)from, amount);
			}

			return false;
		}

		/// <summary>
		/// Withdraws gold and bank checks from a standard bank box.
		/// Prioritizes withdrawing from gold piles first, then bank checks.
		/// </summary>
		/// <param name="from">The mobile making the withdrawal</param>
		/// <param name="amount">The amount to withdraw</param>
		/// <returns>True if withdrawal was successful, false if insufficient funds</returns>
		private static bool WithdrawFromBankBox(Mobile from, int amount)
		{
			Item[] gold, checks;
			int balance = GetBalance(from, out gold, out checks);

			if (balance < amount)
				return false;

			// Withdraw from gold first
			for (int i = 0; amount > 0 && i < gold.Length; ++i)
			{
				if (gold[i].Amount <= amount)
				{
					amount -= gold[i].Amount;
					gold[i].Delete();
				}
				else
				{
					gold[i].Amount -= amount;
					amount = 0;
				}
			}

			// Then withdraw from checks
			for (int i = 0; amount > 0 && i < checks.Length; ++i)
			{
				BankCheck check = (BankCheck)checks[i];

				if (check.Worth <= amount)
				{
					amount -= check.Worth;
					check.Delete();
				}
				else
				{
					check.Worth -= amount;
					amount = 0;
				}
			}

			return true;
		}

		/// <summary>
		/// Deposits the specified amount into the player's account.
		/// Handles both standard bank box deposits and Midland currency deposits.
		/// </summary>
		/// <param name="from">The mobile making the deposit</param>
		/// <param name="amount">The amount to deposit</param>
		/// <returns>True if deposit was successful, false otherwise</returns>
		public static bool Deposit( Mobile from, int amount )
		{
			if (AdventuresFunctions.IsInMidland((object)from) && from is PlayerMobile && ((PlayerMobile)from).midrace != 0)
			{
				// Midland currency deposit to player account
				return MidlandCurrencyHandler.DepositToAccount((PlayerMobile)from, amount);
			}
			else
			{
				// Standard deposit to bank box
				return DepositToBankBox(from, amount);
			}
		}

		/// <summary>
		/// Deposits gold and bank checks into a standard bank box.
		/// Creates appropriate gold piles and bank checks based on amount thresholds.
		/// </summary>
		/// <param name="from">The mobile making the deposit</param>
		/// <param name="amount">The amount to deposit</param>
		/// <returns>True if deposit was successful, false if bank box is full</returns>
		private static bool DepositToBankBox(Mobile from, int amount)
		{
			BankBox box = from.FindBankNoCreate();
			if (box == null)
				return false;

			List<Item> items = new List<Item>();

			while (amount > 0)
			{
				Item item;
				if (amount < BankerConstants.MIN_CHECK_AMOUNT)
				{
					item = new Gold(amount);
					amount = 0;
				}
				else if (amount <= BankerConstants.MAX_CHECK_AMOUNT)
				{
					item = new BankCheck(amount);
					amount = 0;
				}
				else
				{
					item = new BankCheck(BankerConstants.MAX_CHECK_AMOUNT);
					amount -= BankerConstants.MAX_CHECK_AMOUNT;
				}

				if (box.TryDropItem(from, item, false))
				{
					items.Add(item);
				}
				else
				{
					item.Delete();
					foreach (Item curItem in items)
					{
						curItem.Delete();
					}
					return false;
				}
			}

			return true;
		}
		#endregion

		#region Utility Methods
		/// <summary>
		/// Deposits as much as possible of the specified amount into the bank box.
		/// Stops when bank box becomes full or amount is depleted.
		/// </summary>
		/// <param name="from">The mobile making the deposit</param>
		/// <param name="amount">The maximum amount to attempt to deposit</param>
		/// <returns>The actual amount that was deposited</returns>
		public static int DepositUpTo( Mobile from, int amount )
		{
			BankBox box = from.FindBankNoCreate();
			if ( box == null )
				return 0;

			int amountLeft = amount;
			while ( amountLeft > 0 )
			{
				Item item;
				int amountGiven;

				if ( amountLeft < BankerConstants.MIN_CHECK_AMOUNT )
				{
					item = new Gold( amountLeft );
					amountGiven = amountLeft;
				}
				else if ( amountLeft <= BankerConstants.MAX_CHECK_AMOUNT )
				{
					item = new BankCheck( amountLeft );
					amountGiven = amountLeft;
				}
				else
				{
					item = new BankCheck( BankerConstants.MAX_CHECK_AMOUNT );
					amountGiven = BankerConstants.MAX_CHECK_AMOUNT;
				}

				if ( box.TryDropItem( from, item, false ) )
				{
					amountLeft -= amountGiven;
				}
				else
				{
					item.Delete();
					break;
				}
			}

			return amount - amountLeft;
		}

		/// <summary>
		/// Deposits gold and bank checks directly into any container.
		/// Used for depositing into non-bank containers or direct container manipulation.
		/// </summary>
		/// <param name="cont">The container to deposit into</param>
		/// <param name="amount">The amount to deposit</param>
		public static void Deposit( Container cont, int amount )
		{
			while ( amount > 0 )
			{
				Item item;

				if ( amount < BankerConstants.MIN_CHECK_AMOUNT )
				{
					item = new Gold( amount );
					amount = 0;
				}
				else if ( amount <= BankerConstants.MAX_CHECK_AMOUNT )
				{
					item = new BankCheck( amount );
					amount = 0;
				}
				else
				{
					item = new BankCheck( BankerConstants.MAX_CHECK_AMOUNT );
					amount -= BankerConstants.MAX_CHECK_AMOUNT;
				}

				cont.DropItem( item );
			}
		}
		#endregion

		#region Serialization
		public Banker( Serial serial ) : base( serial )
		{
		}

		/// <summary>
		/// Serializes the banker data for saving.
		/// Currently uses version 0 with no additional data.
		/// </summary>
		/// <param name="writer">The generic writer to serialize to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the banker data when loading.
		/// Handles version compatibility for future updates.
		/// </summary>
		/// <param name="reader">The generic reader to deserialize from</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
		#endregion

		#region Player Interaction
		/// <summary>
		/// Determines if this banker should handle speech from the specified mobile.
		/// Bankers respond to banking-related keywords within a certain range.
		/// </summary>
		/// <param name="from">The mobile that spoke</param>
		/// <returns>True if speech should be handled, false otherwise</returns>
		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, BankerConstants.SPEECH_RANGE ) )
				return true;

			return base.HandlesOnSpeech( from );
		}

		/// <summary>
		/// Handles speech events for banking-related keywords.
		/// Processes commands like "withdraw", "balance", "bank", and "check".
		/// </summary>
		/// <param name="e">The speech event arguments</param>
		public override void OnSpeech( SpeechEventArgs e )
		{
			if (  Insensitive.Contains( e.Speech, "deposit" ) )
			{
				this.Say(BankerStringConstants.MSG_DEPOSIT_INFO);
			}

			// Support for Portuguese "cheque" command
			if ( !e.Handled && e.Mobile.InRange( this.Location, 12 ) && Insensitive.Contains( e.Speech, "cheque" ) )
			{
				e.Handled = true;
				ProcessCheckCommand( e.Mobile, e.Speech );
			}

			if ( !e.Handled && e.Mobile.InRange( this.Location, 12 ) )
			{
				for ( int i = 0; i < e.Keywords.Length; ++i )
				{
					int keyword = e.Keywords[i];

					switch ( keyword )
					{
						case 0x0000: // *withdraw*
						{
							e.Handled = true;

							string[] split = e.Speech.Split( ' ' );

							if ( split.Length >= 2 )
							{
								int amount;

								Container pack = e.Mobile.Backpack;

								if ( !int.TryParse( split[1], out amount ) )
									break;

								if ( (!Core.ML && amount > BankerConstants.MAX_WITHDRAWAL_NON_ML) || (Core.ML && amount > BankerConstants.MAX_WITHDRAWAL_ML) )
								{
									this.Say(BankerStringConstants.MSG_WITHDRAW_TOO_MUCH);
								}
								else if (pack == null || pack.Deleted || !(pack.TotalWeight < pack.MaxWeight) || !(pack.TotalItems < pack.MaxItems))
								{
									this.Say(BankerStringConstants.MSG_BACKPACK_FULL);
								}
								else if (amount > 0)
								{
									BankBox box = e.Mobile.FindBankNoCreate();

									if (box == null || !box.ConsumeTotal(typeof(Gold), amount))
									{
										this.Say(BankerStringConstants.MSG_INSUFFICIENT_GOLD);
									}
									else
									{
									pack.DropItem(new Gold(amount));

									Server.Gumps.WealthBar.RefreshWealthBar( e.Mobile );

									this.Say(BankerStringConstants.MSG_WITHDRAW_SUCCESS, amount.ToString());
									}
								}
							}

							break;
						}
						case 0x0001: // *balance*
						{
							e.Handled = true;

							BankBox box = e.Mobile.FindBankNoCreate();

							if ( box != null )
								this.Say(String.Format(BankerStringConstants.MSG_BALANCE_FORMAT, box.TotalGold.ToString()));
							else
								this.Say(String.Format(BankerStringConstants.MSG_BALANCE_FORMAT, "0"));

							break;
						}
						case 0x0002: // *bank*
						{
							e.Handled = true;
							if (AdventuresFunctions.IsInMidland((object)this))
							{
								if (Utility.RandomBool())
									this.Say(BankerStringConstants.MSG_MIDLAND_BANK_VARIANT1);
								else
									this.Say(BankerStringConstants.MSG_MIDLAND_BANK_VARIANT2);
							}
							else
							{
								BankBox box = e.Mobile.BankBox;
								if (box != null)
								{
									box.Open();
								}
							}

							break;
						}
						case 0x0003: // *check*
						{
							e.Handled = true;
							ProcessCheckCommand( e.Mobile, e.Speech );
							break;
						}
					}
				}
			} 
			base.OnSpeech( e );
		}

		/// <summary>
		/// Processes the check creation command from speech.
		/// Handles both English "check" and Portuguese "cheque" commands.
		/// </summary>
		/// <param name="from">The mobile requesting the check</param>
		/// <param name="speech">The speech text containing the command and amount</param>
		private void ProcessCheckCommand( Mobile from, string speech )
		{
			string[] split = speech.Split( ' ' );

			if ( split.Length >= 2 )
			{
				int amount;

				if ( !int.TryParse( split[1], out amount ) )
					return;

				if ( amount < BankerConstants.MIN_CHECK_AMOUNT )
				{
					this.Say(BankerStringConstants.MSG_CHECK_TOO_SMALL);
				}
				else if ( amount > BankerConstants.MAX_TOTAL_CHECK_AMOUNT )
				{
					this.Say(BankerStringConstants.MSG_CHECK_TOO_LARGE);
				}
				else
				{
					BankCheck check = new BankCheck( amount );

					BankBox box = from.BankBox;

					if ( !box.TryDropItem( from, check, false ) )
					{
						this.Say(BankerStringConstants.MSG_BANKBOX_FULL);
						check.Delete();
					}
					else if ( !box.ConsumeTotal( typeof( Gold ), amount ) )
					{
						this.Say(BankerStringConstants.MSG_INSUFFICIENT_GOLD);
						check.Delete();
					}
					else
					{
						this.Say(String.Format(BankerStringConstants.MSG_CHECK_CREATED, amount.ToString()));
					}
					Server.Gumps.WealthBar.RefreshWealthBar( from );
				}
			}
		}


		/// <summary>
		/// Handles gold deposits via drag and drop.
		/// Accepts gold items and deposits them into the player's account.
		/// Charges a fee for deposits in Midland regions.
		/// </summary>
		/// <param name="from">The mobile depositing the gold</param>
		/// <param name="dropped">The gold item being deposited</param>
		/// <returns>True if the deposit was accepted, false otherwise</returns>
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if (!from.Hidden && dropped is Gold  && from is PlayerMobile)
			{
				BankBox box = from.FindBankNoCreate();

				if ( box != null )
				{
					this.Say(String.Format(BankerStringConstants.MSG_DEPOSIT_SUCCESS, dropped.Amount));
					if (AdventuresFunctions.IsInMidland((object)this))
					{
						int fee = 0;
						if (dropped.Amount > BankerConstants.MIDLAND_FEE_THRESHOLD)
							fee = (int)((double)dropped.Amount * BankerConstants.MIDLAND_FEE_PERCENTAGE);
						this.Say(String.Format(BankerStringConstants.MSG_MIDLAND_FEE, fee));
						dropped.Amount -= fee;
					}

					Deposit( box, dropped.Amount );
					dropped.Delete();

				}
			}
			return base.OnDragDrop(from, dropped);
		}

		/// <summary>
		/// Adds custom context menu entries for banking operations.
		/// Adds the "Open Bank" option for eligible players.
		/// </summary>
		/// <param name="from">The mobile viewing the context menu</param>
		/// <param name="list">The list of context menu entries to add to</param>
		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive && from.Kills < 1 && from.Criminal == false )
				list.Add( new OpenBankEntry( from, this ) );

			base.AddCustomContextEntries( from, list );
		}

		///////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Adds context menu entries for this banker.
	/// Includes the speech gump option for banking conversations.
	/// </summary>
	/// <param name="from">The mobile viewing the context menu</param>
	/// <param name="list">The list of context menu entries to add to</param>
	public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
	{
		base.GetContextMenuEntries( from, list );
		list.Add( new SpeechGumpEntry( from, this ) );
	}

	/// <summary>
	/// Context menu entry that opens a speech gump for banker conversations.
	/// Allows players to interact with the banker through a graphical interface.
	/// </summary>
	public class SpeechGumpEntry : ContextMenuEntry
	{
		private Mobile m_Mobile;
		private Mobile m_Giver;

		/// <summary>
		/// Creates a new speech gump entry for banker conversations
		/// </summary>
		/// <param name="from">The player who clicked the menu</param>
		/// <param name="giver">The banker NPC</param>
		public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
		{
			m_Mobile = from;
			m_Giver = giver;
		}

		/// <summary>
		/// Called when the player clicks this context menu entry.
		/// Opens a speech gump for banker conversation if the player is valid.
		/// </summary>
		public override void OnClick()
		{
			if( !( m_Mobile is PlayerMobile ) )
				return;

			PlayerMobile mobile = (PlayerMobile) m_Mobile;
			{
				if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
				{
					mobile.SendGump(new SpeechGump( "Câmbio, Investimentos e Moedas de troca", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Banker" ) ));
				}
			}
		}
	}
		#endregion
}
}

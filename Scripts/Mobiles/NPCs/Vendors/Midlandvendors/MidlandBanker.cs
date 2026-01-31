using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Gumps;
using Server.OneTime;

namespace Server.Mobiles
{

	/// <summary>
	/// MidlandBanker NPC that handles race-specific banking operations in the Midland region.
	/// Each banker serves only players of their matching race, handling currency deposits and withdrawals
	/// for race-specific currencies: Sovereign, Drachma, Sslit, Dubloon, and Skaal.
	/// </summary>
	public class MidlandBanker : MidlandVendor
	{
		#region Fields
		private int m_moneytype;
		#endregion

		#region Properties
		[CommandProperty( AccessLevel.GameMaster )]
        public int moneytype
        {
            get{ return m_moneytype; }
            set{ m_moneytype = value; }
        }
		#endregion

		#region Constructors
		[Constructable]
		public MidlandBanker() : base(  )
		{
			Job = JobFragment.banker;
			Title = "o Banqueiro";
		}
		#endregion

		#region Banking Operations
		/// <summary>
		/// Gets the balance for a Midland player in this banker's currency
		/// </summary>
		/// <param name="from">The mobile requesting balance information</param>
		/// <returns>The account balance, or 0 if invalid</returns>
		public int GetBalance( Mobile from )
		{
			if (!CanAccessServices(from))
				return 0;

			PlayerMobile pm = (PlayerMobile)from;

			// Check if player and banker are same race
			if (pm.midrace != this.midrace)
			{
				this.Say(MidlandBankerStringConstants.MSG_WRONG_RACE);
				return 0;
			}

			// Return balance from appropriate account
			return GetRaceBalance(pm, this.midrace);
		}

		/// <summary>
		/// Checks if a mobile can access this Midland banker's services
		/// </summary>
		/// <param name="from">The mobile to check</param>
		/// <returns>True if services are accessible, false otherwise</returns>
		private bool CanAccessServices(Mobile from)
		{
			return AdventuresFunctions.IsInMidland((object)this) &&
				   AdventuresFunctions.IsInMidland((object)from) &&
				   from is PlayerMobile &&
				   ((PlayerMobile)from).midrace != 0;
		}

		/// <summary>
		/// Gets the balance from the appropriate race account
		/// </summary>
		/// <param name="player">The player mobile</param>
		/// <param name="raceId">The race ID to get balance for</param>
		/// <returns>The account balance</returns>
		private int GetRaceBalance(PlayerMobile player, int raceId)
		{
			switch (raceId)
			{
				case MidlandBankerConstants.CURRENCY_HUMAN: return player.midhumanacc;
				case MidlandBankerConstants.CURRENCY_GARGOYLE: return player.midgargoyleacc;
				case MidlandBankerConstants.CURRENCY_LIZARD: return player.midlizardacc;
				case MidlandBankerConstants.CURRENCY_PIRATE: return player.midpirateacc;
				case MidlandBankerConstants.CURRENCY_ORC: return player.midorcacc;
				default: return 0;
			}
		}


		/// <summary>
		/// Deposits currency into a Midland player's account
		/// </summary>
		/// <param name="from">The mobile making the deposit</param>
		/// <param name="amount">The amount to deposit</param>
		/// <returns>True if deposit was successful, false otherwise</returns>
		public bool Deposit( Mobile from, int amount)
		{
			if (!CanAccessServices(from))
				return false;

			PlayerMobile pm = (PlayerMobile)from;

			// Check if currency type matches banker's race
			if (m_moneytype != this.midrace)
			{
				this.Say(MidlandBankerStringConstants.MSG_WRONG_TYPE);
				return false;
			}

			// Find and consume the currency from backpack
			Type currencyType = GetCurrencyType(this.midrace);
			if (currencyType == null)
				return false;

			Item money = pm.Backpack.FindItemByType(currencyType);
			if (money == null || money.Amount < amount)
				return false;

			// Consume the currency
			if (money.Amount == amount)
				money.Delete();
			else
				money.Amount -= amount;

			// Add to appropriate account
			AddToAccount(pm, this.midrace, amount);
			return true;
		}

		/// <summary>
		/// Gets the currency item type for a race
		/// </summary>
		/// <param name="raceId">The race ID</param>
		/// <returns>The currency Type, or null if invalid</returns>
		private Type GetCurrencyType(int raceId)
		{
			switch (raceId)
			{
				case MidlandBankerConstants.CURRENCY_HUMAN: return typeof(Sovereign);
				case MidlandBankerConstants.CURRENCY_GARGOYLE: return typeof(Drachma);
				case MidlandBankerConstants.CURRENCY_LIZARD: return typeof(Sslit);
				case MidlandBankerConstants.CURRENCY_PIRATE: return typeof(Dubloon);
				case MidlandBankerConstants.CURRENCY_ORC: return typeof(Skaal);
				default: return null;
			}
		}

		/// <summary>
		/// Adds amount to the appropriate race account
		/// </summary>
		/// <param name="player">The player mobile</param>
		/// <param name="raceId">The race ID</param>
		/// <param name="amount">The amount to add</param>
		private void AddToAccount(PlayerMobile player, int raceId, int amount)
		{
			switch (raceId)
			{
				case MidlandBankerConstants.CURRENCY_HUMAN: player.midhumanacc += amount; break;
				case MidlandBankerConstants.CURRENCY_GARGOYLE: player.midgargoyleacc += amount; break;
				case MidlandBankerConstants.CURRENCY_LIZARD: player.midlizardacc += amount; break;
				case MidlandBankerConstants.CURRENCY_PIRATE: player.midpirateacc += amount; break;
				case MidlandBankerConstants.CURRENCY_ORC: player.midorcacc += amount; break;
			}
		}


		/// <summary>
		/// Withdraws currency from a Midland player's account
		/// </summary>
		/// <param name="from">The mobile making the withdrawal</param>
		/// <param name="amount">The amount to withdraw</param>
		/// <returns>True if withdrawal was successful, false otherwise</returns>
		public bool Withdraw( Mobile from, int amount )
		{
			int balance = GetBalance(from);

			if (balance == 0 || !(from is PlayerMobile))
				return false;

			if (balance < amount)
				return false;

			PlayerMobile pm = (PlayerMobile)from;

			// Check if player and banker are same race
			if (pm.midrace != this.midrace)
				return false;

			// Deduct from account and give currency
			DeductFromAccount(pm, this.midrace, amount);
			pm.AddToBackpack(CreateCurrencyItem(this.midrace, amount));

			return true;
		}

		/// <summary>
		/// Deducts amount from the appropriate race account
		/// </summary>
		/// <param name="player">The player mobile</param>
		/// <param name="raceId">The race ID</param>
		/// <param name="amount">The amount to deduct</param>
		private void DeductFromAccount(PlayerMobile player, int raceId, int amount)
		{
			switch (raceId)
			{
				case MidlandBankerConstants.CURRENCY_HUMAN: player.midhumanacc -= amount; break;
				case MidlandBankerConstants.CURRENCY_GARGOYLE: player.midgargoyleacc -= amount; break;
				case MidlandBankerConstants.CURRENCY_LIZARD: player.midlizardacc -= amount; break;
				case MidlandBankerConstants.CURRENCY_PIRATE: player.midpirateacc -= amount; break;
				case MidlandBankerConstants.CURRENCY_ORC: player.midorcacc -= amount; break;
			}
		}

		/// <summary>
		/// Creates a currency item of the specified type and amount
		/// </summary>
		/// <param name="raceId">The race ID (determines currency type)</param>
		/// <param name="amount">The amount of currency</param>
		/// <returns>The created currency item</returns>
		private Item CreateCurrencyItem(int raceId, int amount)
		{
			switch (raceId)
			{
				case MidlandBankerConstants.CURRENCY_HUMAN: return new Sovereign(amount);
				case MidlandBankerConstants.CURRENCY_GARGOYLE: return new Drachma(amount);
				case MidlandBankerConstants.CURRENCY_LIZARD: return new Sslit(amount);
				case MidlandBankerConstants.CURRENCY_PIRATE: return new Dubloon(amount);
				case MidlandBankerConstants.CURRENCY_ORC: return new Skaal(amount);
				default: return null;
			}
		}
		#endregion

		#region Player Interaction
		/// <summary>
		/// Determines if this banker should handle speech from the specified mobile.
		/// Midland bankers respond to banking-related keywords within range.
		/// </summary>
		/// <param name="from">The mobile that spoke</param>
		/// <returns>True if speech should be handled, false otherwise</returns>
		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, MidlandBankerConstants.GENERAL_SPEECH_RANGE ) )
				return true;

			return base.HandlesOnSpeech( from );
		}

		/// <summary>
		/// Handles speech events for Midland banking operations.
		/// Processes commands like "withdraw", "balance", "bank", and "check".
		/// </summary>
		/// <param name="e">The speech event arguments</param>
		public override void OnSpeech( SpeechEventArgs e )
		{
			if (  Insensitive.Contains( e.Speech, "deposit" ) && e.Mobile.InRange( this.Location, MidlandBankerConstants.DEPOSIT_SPEECH_RANGE )  )
			{
				string[] split = e.Speech.Split( ' ' );

				if ( split.Length >= 2 )
				{
					int amount;

					if ( !int.TryParse( split[1], out amount ) )
						this.Say(MidlandBankerStringConstants.MSG_DEPOSIT_INFO);

					if ( amount > 0 && Deposit(e.Mobile, amount) )
						this.Say(String.Format(MidlandBankerStringConstants.MSG_DEPOSIT_SUCCESS, amount));
					else
						this.Say(MidlandBankerStringConstants.MSG_INSUFFICIENT_FUNDS);
				}
				else
					this.Say(MidlandBankerStringConstants.MSG_DEPOSIT_INFO);
					
			}
			if ( !e.Handled && e.Mobile.InRange( this.Location, 6 ) )
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

								if ( (!Core.ML && amount > MidlandBankerConstants.MAX_WITHDRAWAL_NON_ML) || (Core.ML && amount > MidlandBankerConstants.MAX_WITHDRAWAL_ML) )
								{
									this.Say( 500381 ); // Thou canst not withdraw so much at one time!
								}
								else if (pack == null || pack.Deleted || !(pack.TotalWeight < pack.MaxWeight) || !(pack.TotalItems < pack.MaxItems))
								{
									this.Say(1048147); // Your backpack can't hold anything else.
								}
								else if (amount > 0)
								{

									if (!Withdraw( e.Mobile, amount))
									{
										this.Say(500384); // Ah, art thou trying to fool me? Thou hast not so much gold!
									}
									else
									{
										this.Say(String.Format(MidlandBankerStringConstants.MSG_WITHDRAW_SUCCESS, amount));
									}
								}
							}

							break;
						}
						case 0x0001: // *balance*
						{
							e.Handled = true;

							int amt = GetBalance(e.Mobile);

							if ( amt > 0 )
								this.Say( String.Format(MidlandBankerStringConstants.MSG_BALANCE_INFO, amt) );
							else
								this.Say( MidlandBankerStringConstants.MSG_NO_ACCOUNT ); 

							break;
						}
						case 0x0002: // *bank*
						{
							e.Handled = true;
							if (AdventuresFunctions.IsInMidland((object)this))
							{
								if (Utility.RandomBool())
									this.Say(MidlandBankerStringConstants.MSG_MIDLAND_BANK_VARIANT1);
								else
									this.Say(MidlandBankerStringConstants.MSG_MIDLAND_BANK_VARIANT2);
							}

							break;
						}
						/* no checks yet
						case 0x0003: // *check*
						{
							e.Handled = true;

							string[] split = e.Speech.Split( ' ' );

							if ( split.Length >= 2 )
							{
								int amount;

                                if ( !int.TryParse( split[1], out amount ) )
                                    break;

								if ( amount < MidlandBankerConstants.MIN_CHECK_AMOUNT )
								{
									this.Say( 1010006 ); // We cannot create checks for such a paltry amount of gold!
								}
								else if ( amount > MidlandBankerConstants.MAX_CHECK_AMOUNT )
								{
									this.Say( 1010007 ); // Our policies prevent us from creating checks worth that much!
								}
								else
								{
									BankCheck check = new BankCheck( amount );

									BankBox box = e.Mobile.BankBox;

									if ( !box.TryDropItem( e.Mobile, check, false ) )
									{
										this.Say( 500386 ); // There's not enough room in your bankbox for the check!
										check.Delete();
									}
									else if ( !box.ConsumeTotal( typeof( Gold ), amount ) )
									{
										this.Say( 500384 ); // Ah, art thou trying to fool me? Thou hast not so much gold!
										check.Delete();
									}
									else
									{
										this.Say( 1042673, AffixType.Append, amount.ToString(), "" ); // Into your bank box I have placed a check in the amount of:
									}
									Server.Gumps.WealthBar.RefreshWealthBar( e.Mobile );
								}
							}

							break;
						}
						*/
					}
				}
			} 
			base.OnSpeech( e );
		}


		/// <summary>
		/// Handles race-specific currency deposits via drag and drop.
		/// Accepts only currency items that match the banker's race.
		/// </summary>
		/// <param name="from">The mobile depositing the currency</param>
		/// <param name="dropped">The currency item being deposited</param>
		/// <returns>True if the deposit was accepted, false otherwise</returns>
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if (!from.Hidden && from is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)from;
				if (dropped is Sovereign && this.midrace == 1 && pm.midrace == 1)
				{
					pm.midhumanacc += dropped.Amount;
					dropped.Delete();
					return true;
				}
				else if (dropped is Drachma && this.midrace == 2 && pm.midrace == 2)
				{
					pm.midgargoyleacc += dropped.Amount;
					dropped.Delete();
					return true;
				}
				else if (dropped is Sslit && this.midrace == 3 && pm.midrace == 3)
				{
					pm.midlizardacc += dropped.Amount;
					dropped.Delete();
					return true;
				}
				else if (dropped is Dubloon && this.midrace == 4 && pm.midrace == 4)
				{
					pm.midpirateacc += dropped.Amount;
					dropped.Delete();
					return true;
				}
				else if (dropped is Skaal && this.midrace == 5 && pm.midrace == 5)
				{
					pm.midorcacc += dropped.Amount;
					dropped.Delete();
					return true;
				}
				else if (this.midrace != pm.midrace)
				{
					this.Say(MidlandBankerStringConstants.MSG_WRONG_KIND);
					return false;
				}
				else
				{
					this.Say(MidlandBankerStringConstants.MSG_NOT_MONEY);
					return false;
				}
			}

			return false;

		}
		#endregion

		#region Serialization
		public MidlandBanker( Serial serial ) : base( serial )
		{
		}

		/// <summary>
		/// Serializes the Midland banker data for saving.
		/// Currently uses version 0 with no additional data.
		/// </summary>
		/// <param name="writer">The generic writer to serialize to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the Midland banker data when loading.
		/// Handles version compatibility for future updates.
		/// </summary>
		/// <param name="reader">The generic reader to deserialize from</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			good1 = typeof(TinkerTools);
		}
		#endregion

	}
}
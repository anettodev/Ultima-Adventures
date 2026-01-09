using System;
using System.Globalization;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	/// <summary>
	/// Represents a bank check that can be cashed for gold in a player's bank box.
	/// Handles large gold amounts by creating multiple 60,000 gold stacks (UO limit).
	/// If the bank box becomes full during deposit, remaining gold is returned as a new check.
	/// </summary>
	public class BankCheck : Item
	{
		#region Fields

		private int m_Worth;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the gold value of this bank check
		/// </summary>
		[CommandProperty( AccessLevel.GameMaster )]
		public int Worth
		{
			get{ return m_Worth; }
			set{ m_Worth = value; InvalidateProperties(); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Deserializes a bank check from save data
		/// </summary>
		/// <param name="serial">Serial number of the item</param>
		public BankCheck( Serial serial ) : base( serial )
		{
		}

		/// <summary>
		/// Constructs a new bank check with the specified gold value
		/// </summary>
		/// <param name="worth">Gold value of the check</param>
		[Constructable]
		public BankCheck( int worth ) : base( BankCheckConstants.ITEM_ID )
		{
			Weight = BankCheckConstants.ITEM_WEIGHT;
			Hue = BankCheckConstants.ITEM_HUE;
			Name = "Cheque Bancário";

			m_Worth = worth;
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the bank check to save data
		/// </summary>
		/// <param name="writer">Writer to save data to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Worth );
		}

		/// <summary>
		/// Deserializes the bank check from save data
		/// </summary>
		/// <param name="reader">Reader to load data from</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Worth = reader.ReadInt();
					break;
				}
			}
		}

		#endregion

		#region Item Properties

		/// <summary>
		/// Whether to display the loot type (blessed) in the properties
		/// </summary>
		public override bool DisplayLootType{ get{ return Core.AOS; } }

		/// <summary>
		/// Label number for the item name ("A bank check")
		/// </summary>
		public override int LabelNumber{ get{ return BankCheckConstants.LABEL_NUMBER; } }

		/// <summary>
		/// Adds the gold value property to the item's property list
		/// </summary>
		/// <param name="list">Property list to add to</param>
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			// Valor: {Amount} in CYAN color
			list.Add( 1053099, ItemNameHue.UnifiedItemProps.SetColor( "Valor: " + FormatWorth(), BankCheckConstants.COLOR_CYAN ) );
		}

		/// <summary>
		/// Handles single-clicking the bank check to show its value
		/// </summary>
		/// <param name="from">Mobile single-clicking the item</param>
		public override void OnSingleClick( Mobile from )
		{
			from.Send( new MessageLocalizedAffix( Serial, ItemID, MessageType.Label, BankCheckConstants.MESSAGE_HUE, 3, BankCheckConstants.LABEL_NUMBER, "", AffixType.Append, String.Concat( " ", m_Worth.ToString() ), "" ) ); // A bank check:
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-clicking the bank check to cash it for gold.
		/// Deposits gold into player's bank box, handling large amounts by creating multiple stacks.
		/// If bank box is full, remaining amount is returned as a new bank check.
		/// </summary>
		/// <param name="from">Mobile double-clicking the check</param>
		public override void OnDoubleClick( Mobile from )
		{
			BankBox box = from.FindBankNoCreate();

			if ( box != null && IsChildOf( box ) )
			{
				Delete();

				int deposited = 0;
				int remaining = m_Worth;

				// Deposit gold in 60,000 stacks (UO gold stack limit) until remaining < 60,000
				while ( remaining >= BankCheckConstants.MAX_GOLD_STACK )
				{
					if ( !TryDepositGold( from, box, BankCheckConstants.MAX_GOLD_STACK, ref deposited, ref remaining ) )
						break; // Bank box full, remaining check created by helper
				}

				// Deposit any remaining gold (less than 60,000)
				if ( remaining > 0 )
				{
					TryDepositGold( from, box, remaining, ref deposited, ref remaining );
				}

				// Gold was deposited in your account:
				from.SendLocalizedMessage( BankCheckConstants.DEPOSIT_MESSAGE, true, " " + deposited.ToString() );
			}
			else
			{
				from.SendLocalizedMessage( BankCheckConstants.ERROR_MESSAGE ); // That must be in your bank box to use it.
			}
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Formats the worth value for display, handling ML formatting
		/// </summary>
		/// <returns>Formatted worth string</returns>
		private string FormatWorth()
		{
			if ( Core.ML )
				return m_Worth.ToString( "N0", CultureInfo.GetCultureInfo( "en-US" ) );
			else
				return m_Worth.ToString();
		}

		/// <summary>
		/// Attempts to deposit gold into the bank box, handling failures gracefully
		/// </summary>
		/// <param name="from">Mobile depositing the gold</param>
		/// <param name="box">Bank box to deposit into</param>
		/// <param name="amount">Amount of gold to deposit</param>
		/// <param name="deposited">Reference to total deposited amount (updated on success)</param>
		/// <param name="remaining">Reference to remaining amount (set to 0 on failure)</param>
		/// <returns>True if deposit succeeded, false if bank box was full</returns>
		private bool TryDepositGold( Mobile from, BankBox box, int amount, ref int deposited, ref int remaining )
		{
			Gold gold = new Gold( amount );

			if ( box.TryDropItem( from, gold, false ) )
			{
				deposited += amount;
				remaining -= amount;
				return true;
			}
			else
			{
				gold.Delete();
				from.AddToBackpack( new BankCheck( remaining ) );
				remaining = 0;
				return false;
			}
		}

		#endregion
	}
}
using System; 
using System.Collections; 
using System.Collections.Generic;
using Server; 
using Server.Misc;
using Server.Gumps; 
using Server.Items; 
using Server.Network; 
using Server.Targeting; 
using Server.ContextMenus; 


namespace Server.Mobiles 
{ 
	/// <summary>
	/// Animal Broker (AnimalTrainerLord) - Specialized vendor that buys pets from players,
	/// appraises pet values, and provides TamingBOD contracts.
	/// </summary>
	public class AnimalTrainerLord : BaseVendor
	{ 
		#region Fields

		private bool m_AppraiseMode = false;
		private List<SBInfo> m_SBInfos = new List<SBInfo>();

		#endregion

		#region Properties

		/// <summary>
		/// Gets the list of shop buy/sell information for this vendor.
		/// </summary>
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the AnimalTrainerLord class.
		/// </summary>
		[Constructable]
		public AnimalTrainerLord() : base( AnimalBrokerStringConstants.VENDOR_TITLE )
		{ 
			InitStats( AnimalBrokerConstants.STAT_STR, AnimalBrokerConstants.STAT_DEX, AnimalBrokerConstants.STAT_INT ); 
			Name = this.Female ? NameList.RandomName( "female" ) : NameList.RandomName( "male" );
			Title = AnimalBrokerStringConstants.VENDOR_TITLE;

			// Body and Hue will be set in InitBody() override
			Hue = Utility.RandomSkinHue(); 
			
			// Items will be added in InitOutfit() override
			
			HairItemID = AnimalBrokerConstants.HAIR_ITEM_ID;
			HairHue = AnimalBrokerConstants.HAIR_HUE;
			
			// Override AI type to maintain original behavior (BaseVendor uses AI_Vendor by default)
			AI = AIType.AI_Thief;
			FightMode = FightMode.None;
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes the vendor's buy/sell information.
		/// </summary>
		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBAnimalBroker() );
		}
		
		/// <summary>
		/// Initializes the vendor's body appearance.
		/// </summary>
		public override void InitBody()
		{
			// Override to preserve original body setup
			base.InitBody();
			Body = AnimalBrokerConstants.BODY_TYPE;
			Hue = Utility.RandomSkinHue();
		}
		
		/// <summary>
		/// Initializes the vendor's outfit and equipment.
		/// </summary>
		public override void InitOutfit()
		{
			// Override to preserve original outfit
			base.InitOutfit();
			
			// Remove items added by base.InitOutfit() and add custom ones
			RemoveItemFromLayer( Layer.InnerTorso );
			RemoveItemFromLayer( Layer.Pants );
			RemoveItemFromLayer( Layer.Shoes );
			
			AddItem( new Boots( Utility.RandomBirdHue() ) );
			AddItem( new ShepherdsCrook() );
			AddItem( new Cloak( Utility.RandomBirdHue() ) );
			AddItem( new FancyShirt( Utility.RandomBirdHue() ) );
			AddItem( new Kilt( Utility.RandomBirdHue() ) );
			AddItem( new BodySash( Utility.RandomBirdHue() ) );
		}

		#endregion

		#region Vendor Overrides

		/// <summary>
		/// Gets context menu entries for this vendor.
		/// </summary>
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			list.Add( new AppraiseEntry( this, from ) );
			list.Add( new TamingBODDealerEntry( from, this ) );
			
		}

		/// <summary>
		/// Handles movement-based greeting messages when players approach.
		/// </summary>
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, AnimalBrokerConstants.MOVEMENT_GREETING_RANGE ) && !InRange( oldLocation, AnimalBrokerConstants.MOVEMENT_GREETING_RANGE ) )
			{
				if ( m is PlayerMobile && !m.Hidden ) 
				{
					int messageIndex = Utility.Random( AnimalBrokerConstants.GREETING_MESSAGE_COUNT );
					string message = GetGreetingMessage( messageIndex );
					if ( message != null )
					{
						Say( message );
					}
				}
			}
		}

		/// <summary>
		/// Indicates this vendor handles speech commands.
		/// </summary>
		public override bool HandlesOnSpeech( Mobile from ) 
		{ 
			return true; 
		} 

		/// <summary>
		/// Handles speech commands for pet sales, appraisals, and contracts.
		/// Available commands:
		/// - "sell" / "vender" : Sell a pet
		/// - "avaliar" / "appraise" : Appraise a pet's value
		/// - "contrato" / "contract" : Request a TamingBOD contract
		/// </summary>
		public override void OnSpeech( SpeechEventArgs e )
		{
			if ( e.Mobile.InRange( this, AnimalBrokerConstants.SPEECH_RANGE ) && !e.Handled )
			{
				if ( HandleSellCommand( e ) || HandleAppraiseCommand( e ) || HandleContractCommand( e ) )
					return;
			}
			
			base.OnSpeech( e ); 
		}

		/// <summary>
		/// Handles the sell speech command.
		/// </summary>
		/// <param name="e">Speech event arguments</param>
		/// <returns>True if command was handled, false otherwise</returns>
		private bool HandleSellCommand( SpeechEventArgs e )
		{
			if ( !Insensitive.Contains( e.Speech, "sell" ) && !Insensitive.Contains( e.Speech, "vender" ) )
				return false;

			e.Handled = true;
			BeginPetSale( e.Mobile, false );
			return true;
		}

		/// <summary>
		/// Handles the appraise speech command (matches AnimalTrainer.cs commands).
		/// </summary>
		/// <param name="e">Speech event arguments</param>
		/// <returns>True if command was handled, false otherwise</returns>
		private bool HandleAppraiseCommand( SpeechEventArgs e )
		{
			if ( !Insensitive.Contains( e.Speech, "avaliar" ) && !Insensitive.Contains( e.Speech, "appraise" ) )
				return false;

			e.Handled = true;
			BeginPetSale( e.Mobile, true );
			return true;
		}

		/// <summary>
		/// Handles the contract speech command ("Contrato").
		/// </summary>
		/// <param name="e">Speech event arguments</param>
		/// <returns>True if command was handled, false otherwise</returns>
		private bool HandleContractCommand( SpeechEventArgs e )
		{
			if ( !Insensitive.Contains( e.Speech, "contrato" ) && !Insensitive.Contains( e.Speech, "contract" ) )
				return false;

			e.Handled = true;
			
			if ( !( e.Mobile is PlayerMobile ) )
				return true;

			PlayerMobile mobile = (PlayerMobile) e.Mobile;

			if ( TamingBODDealerEntry.CanGetContract( mobile ) )
			{
				if ( !mobile.HasGump( typeof( TamingBODDealerGump ) ) )
				{
					mobile.SendGump( new TamingBODDealerGump( mobile ) );
					// Contract is created in gump's OnResponse when player clicks the button
				}
			}
			else
			{
				Say( AnimalBrokerStringConstants.MSG_CONTRACT_NOT_AVAILABLE );
			}

			return true;
		}


		#endregion

		#region Pet Sale System

		/// <summary>
		/// Begins the pet sale or appraisal process.
		/// </summary>
		/// <param name="from">The mobile initiating the sale/appraisal</param>
		/// <param name="appraise">True for appraisal mode, false for sale mode</param>
		public void BeginPetSale( Mobile from, bool appraise ) 
		{ 
			if ( Deleted || !from.CheckAlive() )
				return; 

			m_AppraiseMode = appraise;

			if ( appraise )
				SayTo( from, AnimalBrokerStringConstants.MSG_APPRAISE_PROMPT ); 
			else
				SayTo( from, AnimalBrokerStringConstants.MSG_SELL_PROMPT ); 

			from.Target = new PetSaleTarget( this ); 
		} 

		/// <summary>
		/// Completes the pet sale or appraisal process.
		/// </summary>
		/// <param name="from">The mobile selling/appraising the pet</param>
		/// <param name="pet">The pet being sold/appraised</param>
		public void EndPetSale( Mobile from, BaseCreature pet ) 
		{ 
			if ( Deleted || !from.CheckAlive() )
				return;    
		
			// Validate pet for sale
			string validationError = ValidatePetForSale( from, pet );
			if ( validationError != null )
			{
				SayTo( from, validationError );
				return;
			}

			// Calculate pet value
			double oldValue = pet.MinTameSkill;
			int petPrice = ValuatePet( pet, this );

			// Handle appraisal mode - show gump with price (allows user to sell if desired)
			if ( m_AppraiseMode )
			{
				// Reset the value to what it was (appraise doesn't modify the pet)
				pet.MinTameSkill = oldValue;
				
				// Show confirmation gump with price (matches AnimalTrainer.cs behavior)
				from.SendGump( new SellPetConfirmationGump( this, from, pet, petPrice ) );
				return;
			}

			// Complete the sale
			LoggingFunctions.LogPetSale( from, pet, petPrice );
			SellPetForGold( from, pet, petPrice );
			Titles.AwardFame( from, ( pet.Fame / AnimalBrokerConstants.FAME_DIVISOR ), true );

			// Display price-based message
			string saleMessage = GetPriceBasedMessage( pet, petPrice, from.Name );
			Say( saleMessage );
		}

		/// <summary>
		/// Sells the pet and gives gold/bankcheck to the player.
		/// </summary>
		/// <param name="from">The mobile receiving payment</param>
		/// <param name="pet">The pet being sold</param>
		/// <param name="goldAmount">The amount of gold to pay</param>
		private void SellPetForGold( Mobile from, BaseCreature pet, int goldAmount )
		{
			// Create gold or bankcheck based on amount
			Item payment = null;
			if ( goldAmount < AnimalBrokerConstants.GOLD_TO_BANKCHECK_THRESHOLD )
				payment = new Gold( goldAmount );
			else 
				payment = new BankCheck( goldAmount );
			
			// Remove pet control and delete
			pet.ControlTarget = null; 
			pet.ControlOrder = OrderType.None; 
			pet.Internalize(); 
			pet.SetControlMaster( null ); 
			pet.SummonMaster = null;
			pet.Delete();

			// Give payment to player
			Container backpack = from.Backpack;
			if ( backpack == null || !backpack.TryDropItem( from, payment, false ) ) 
			{ 
				payment.MoveToWorld( from.Location, from.Map );
			}		
		}

		/// <summary>
		/// Calculates the value of a pet based on its taming difficulty and level.
		/// </summary>
		/// <param name="pet">The pet to value</param>
		/// <param name="broker">The broker calculating the value</param>
		/// <returns>The calculated price in gold</returns>
		public static int ValuatePet( BaseCreature pet, Mobile broker )
		{
			// Refresh pet values if pet was trained etc
			pet.DynamicFameKarma();
			pet.DynamicTaming( false );

			// Get base value with division by zero protection
			double baseValue = GetSafeTameSkill( pet );

			// Adjust for easier tames (non-angering pets worth less)
			if ( !pet.CanAngerOnTame )
				baseValue /= AnimalBrokerConstants.EASY_TAME_VALUE_DIVISOR;

			// Calculate factorial for price calculation
			double factorial = CalculateFactorial( baseValue, pet.MinTameSkill );

			// Calculate base price using iterative step method
			double finalPrice = CalculateBasePrice( baseValue, factorial );

			// Apply gold cut rate (server balance level)
			finalPrice *= ( (double)Misc.MyServerSettings.GetGoldCutRate( broker, null ) / AnimalBrokerConstants.GOLD_CUT_RATE_DIVISOR );

			// Apply level-based price adjustment
			if ( pet.Level > 1 )
				finalPrice = ApplyLevelPriceBonus( finalPrice, (int)pet.Level );

			// Convert to integer and enforce minimum
			int petPrice = Convert.ToInt32( finalPrice );
			if ( petPrice <= AnimalBrokerConstants.MIN_PET_PRICE )
				petPrice = AnimalBrokerConstants.MIN_PET_PRICE;

			return petPrice;
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Removes an item from the specified layer if it exists.
		/// </summary>
		/// <param name="layer">The layer to check</param>
		private void RemoveItemFromLayer( Layer layer )
		{
			Item item = FindItemOnLayer( layer );
			if ( item != null )
				item.Delete();
		}

		/// <summary>
		/// Gets a greeting message by index.
		/// </summary>
		/// <param name="index">The message index (0-5)</param>
		/// <returns>The greeting message, or null if index is out of range</returns>
		private string GetGreetingMessage( int index )
		{
			switch ( index )
			{
				case 0: return AnimalBrokerStringConstants.GREETING_PET_COLLECTOR;
				case 1: return AnimalBrokerStringConstants.GREETING_BUYING_RARE;
				case 2: return AnimalBrokerStringConstants.GREETING_SELL_PETS;
				case 3: return AnimalBrokerStringConstants.GREETING_APPRAISE_OFFER;
				case 4: return AnimalBrokerStringConstants.GREETING_APPRAISE_SERVICE;
				case 5: return AnimalBrokerStringConstants.GREETING_CONTRACT_HELP;
				default: return null;
			}
		}

		/// <summary>
		/// Validates if a pet can be sold/appraised.
		/// </summary>
		/// <param name="from">The mobile attempting to sell</param>
		/// <param name="pet">The pet to validate</param>
		/// <returns>Error message if validation fails, null if valid</returns>
		private string ValidatePetForSale( Mobile from, BaseCreature pet )
		{
			if ( !pet.Controlled || pet.ControlMaster != from )
				return "1042562"; // You do not own that pet!
			
			if ( pet.IsDeadPet )
				return "1049668"; // Living pets only, please.
			
			if ( pet.Summoned )
				return "502673"; // I can not PetSale summoned creatures.
			
			if ( pet.Body.IsHuman )
				return "502672"; // HA HA HA! Sorry, I am not an inn.
			
			if ( ( pet is PackLlama || pet is PackHorse || pet is Beetle ) && ( pet.Backpack != null && pet.Backpack.Items.Count > 0 ) )
				return "1042563"; // You need to unload your pet.
			
			if ( pet.Combatant != null && pet.InRange( pet.Combatant, AnimalBrokerConstants.PET_COMBAT_CHECK_RANGE ) && pet.Map == pet.Combatant.Map )
				return "1042564"; // I'm sorry. Your pet seems to be busy.

			return null; // Valid
		}

		/// <summary>
		/// Gets a price-based message for the pet sale.
		/// </summary>
		/// <param name="pet">The pet being sold</param>
		/// <param name="price">The price being paid</param>
		/// <param name="playerName">The name of the player selling</param>
		/// <returns>The formatted message</returns>
		private string GetPriceBasedMessage( BaseCreature pet, int price, string playerName )
		{
			if ( price <= AnimalBrokerConstants.PRICE_THRESHOLD_LOW )
				return string.Format( AnimalBrokerStringConstants.MSG_PRICE_LOW_FORMAT, pet.Name, price );
			
			if ( price <= AnimalBrokerConstants.PRICE_THRESHOLD_MEDIUM )
				return string.Format( AnimalBrokerStringConstants.MSG_PRICE_MEDIUM_FORMAT, playerName, pet.Name, price );
			
			if ( price <= AnimalBrokerConstants.PRICE_THRESHOLD_HIGH )
				return string.Format( AnimalBrokerStringConstants.MSG_PRICE_HIGH_FORMAT, pet.Name, price );
			
			if ( price <= AnimalBrokerConstants.PRICE_THRESHOLD_VERY_HIGH )
				return string.Format( AnimalBrokerStringConstants.MSG_PRICE_VERY_HIGH_FORMAT, price );
			
			if ( price >= AnimalBrokerConstants.PRICE_THRESHOLD_ULTRA_HIGH )
				return string.Format( AnimalBrokerStringConstants.MSG_PRICE_ULTRA_HIGH_FORMAT, price );

			// Fallback (should not reach here)
			return string.Format( AnimalBrokerStringConstants.MSG_PRICE_MEDIUM_FORMAT, playerName, pet.Name, price );
		}

		/// <summary>
		/// Gets a safe tame skill value (prevents division by zero).
		/// </summary>
		/// <param name="pet">The pet to check</param>
		/// <returns>Safe tame skill value (max 124)</returns>
		private static double GetSafeTameSkill( BaseCreature pet )
		{
			double baseValue = pet.MinTameSkill;
			if ( baseValue >= AnimalBrokerConstants.MAX_TAME_SKILL )
			{
				pet.MinTameSkill = AnimalBrokerConstants.SAFE_MAX_TAME_SKILL; // Divide by zero check
				baseValue = AnimalBrokerConstants.SAFE_MAX_TAME_SKILL;
			}
			return baseValue;
		}

		/// <summary>
		/// Calculates the factorial value for price calculation.
		/// </summary>
		/// <param name="baseValue">The base tame skill value</param>
		/// <param name="minTameSkill">The pet's MinTameSkill</param>
		/// <returns>The calculated factorial</returns>
		private static double CalculateFactorial( double baseValue, double minTameSkill )
		{
			return 1.0 / ( ( AnimalBrokerConstants.BASE_CALC_VALUE - baseValue ) / ( minTameSkill * AnimalBrokerConstants.FACTORIAL_MULTIPLIER ) );
		}

		/// <summary>
		/// Calculates the base price using iterative step method.
		/// </summary>
		/// <param name="baseValue">The base tame skill value</param>
		/// <param name="factorial">The calculated factorial</param>
		/// <returns>The calculated base price</returns>
		private static double CalculateBasePrice( double baseValue, double factorial )
		{
			double final = 0;
			double step = AnimalBrokerConstants.CALCULATION_STEP;
			double workingValue = baseValue;

			if ( workingValue < step )
			{
				final = workingValue * factorial;
			}
			else 
			{	
				while ( workingValue > 0 )
				{
					if ( workingValue > step )
					{
						workingValue -= step;
						final += step * factorial;
					}
					else
					{
						final += workingValue * factorial;
						workingValue = 0;
					}
				}
			}

			return final;
		}

		/// <summary>
		/// Applies level-based price adjustment.
		/// </summary>
		/// <param name="price">The current price</param>
		/// <param name="level">The pet's level</param>
		/// <returns>The adjusted price</returns>
		private static double ApplyLevelPriceBonus( double price, int level )
		{
			// Level-based price increase: higher level = higher price
			// Formula: price / ((level^0.25) / level)
			return price / ( ( Math.Pow( level, AnimalBrokerConstants.LEVEL_PRICE_EXPONENT ) ) / ( (double)level ) );
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		public AnimalTrainerLord( Serial serial ) : base( serial ) 
		{ 
		} 

		/// <summary>
		/// Serializes the AnimalTrainerLord.
		/// </summary>
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version 
		} 

		/// <summary>
		/// Deserializes the AnimalTrainerLord.
		/// </summary>
		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader );
			int version = reader.ReadInt(); 
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Target class for selecting pets to sell or appraise.
		/// </summary>
		private class PetSaleTarget : Target 
		{ 
			private AnimalTrainerLord m_Trainer; 

			/// <summary>
			/// Initializes a new instance of the PetSaleTarget class.
			/// </summary>
			/// <param name="trainer">The AnimalTrainerLord handling the sale</param>
			public PetSaleTarget( AnimalTrainerLord trainer ) : base( AnimalBrokerConstants.PET_SALE_TARGET_RANGE, false, TargetFlags.None ) 
			{ 
				m_Trainer = trainer; 
			} 

			/// <summary>
			/// Handles the target selection.
			/// </summary>
			protected override void OnTarget( Mobile from, object targeted ) 
			{ 
				if ( targeted is BaseCreature )
					m_Trainer.EndPetSale( from, (BaseCreature)targeted ); 
				else if ( targeted == from )
					m_Trainer.SayTo( from, 502672 ); // HA HA HA! Sorry, I am not an inn. 
			} 
		}

		#endregion

		#region Gumps

		/// <summary>
		/// Gump for confirming pet sale/appraisal (matches AnimalTrainer.cs behavior).
		/// </summary>
		private class SellPetConfirmationGump : Gump
		{
			private AnimalTrainerLord m_Broker;
			private Mobile m_From;
			private BaseCreature m_Pet;
			private int m_Price;

			/// <summary>
			/// Initializes a new instance of the SellPetConfirmationGump class.
			/// </summary>
			/// <param name="broker">The AnimalTrainerLord handling the sale</param>
			/// <param name="from">The player selling/appraising the pet</param>
			/// <param name="pet">The pet being sold/appraised</param>
			/// <param name="price">The calculated price</param>
			public SellPetConfirmationGump( AnimalTrainerLord broker, Mobile from, BaseCreature pet, int price ) : base( 200, 200 )
			{
				m_Broker = broker;
				m_From = from;
				m_Pet = pet;
				m_Price = price;

				from.CloseGump( typeof( SellPetConfirmationGump ) );

				this.Closable = true;
				this.Disposable = true;
				this.Dragable = true;
				this.Resizable = false;

				AddPage( 0 );
				AddBackground( 0, 0, 300, 200, 5054 );
				AddImageTiled( 10, 10, 280, 20, 2624 );
				AddImageTiled( 10, 40, 280, 140, 2624 );
				AddAlphaRegion( 10, 10, 280, 170 );

				AddHtml( 20, 15, 260, 20, @"<CENTER><BASEFONT COLOR=#00FFFF><B>Vender Animal</B></BASEFONT></CENTER>", false, false );

				string petName = m_Pet != null && !m_Pet.Deleted ? m_Pet.Name : "Animal";
				string message = string.Format( "Eu posso pagar <BASEFONT COLOR=#FFFF00>{0} moedas de ouro</BASEFONT> por este {1}.<BR><BR>Deseja vender?", m_Price, petName );
				AddHtml( 20, 45, 260, 100, message, true, true );

				AddButton( 50, 155, 4005, 4007, 1, GumpButtonType.Reply, 0 ); // YES
				AddHtml( 85, 157, 50, 20, @"<CENTER><BASEFONT COLOR=#00FF00><B>SIM</B></BASEFONT></CENTER>", false, false );

				AddButton( 200, 155, 4005, 4007, 0, GumpButtonType.Reply, 0 ); // NO
				AddHtml( 235, 157, 50, 20, @"<CENTER><BASEFONT COLOR=#FF0000><B>NÃO</B></BASEFONT></CENTER>", false, false );
			}

			/// <summary>
			/// Handles the gump response.
			/// </summary>
			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( m_Broker == null || m_Broker.Deleted || m_From == null || m_From.Deleted )
					return;

				if ( info.ButtonID == 1 ) // YES - Sell the pet
				{
					if ( m_Pet != null && !m_Pet.Deleted && m_Pet.ControlMaster == m_From && m_Pet.Controlled )
					{
						// Complete the sale directly (price already calculated, no need to recalculate)
						LoggingFunctions.LogPetSale( m_From, m_Pet, m_Price );
						m_Broker.SellPetForGold( m_From, m_Pet, m_Price );
						Titles.AwardFame( m_From, ( m_Pet.Fame / AnimalBrokerConstants.FAME_DIVISOR ), true );

						// Display price-based message
						string saleMessage = m_Broker.GetPriceBasedMessage( m_Pet, m_Price, m_From.Name );
						m_Broker.Say( saleMessage );
					}
					else
					{
						m_Broker.SayTo( m_From, AnimalBrokerStringConstants.MSG_APPRAISE_NOT_OWNER );
					}
				}
				else // NO - Decline
				{
					m_Broker.SayTo( m_From, AnimalBrokerStringConstants.MSG_SALE_DECLINED );
				}
			}
		}

		#endregion

		#region Context Menu Entries

		/// <summary>
		/// Context menu entry for appraising pets.
		/// </summary>
		private class AppraiseEntry : ContextMenuEntry
		{
			private AnimalTrainerLord m_Broker;
			private Mobile m_From;

			/// <summary>
			/// Initializes a new instance of the AppraiseEntry class.
			/// </summary>
			/// <param name="broker">The AnimalTrainerLord handling the appraisal</param>
			/// <param name="from">The mobile requesting the appraisal</param>
			public AppraiseEntry( AnimalTrainerLord broker, Mobile from ) : base( AnimalBrokerConstants.CONTEXT_MENU_APPRAISE_ID, AnimalBrokerConstants.CONTEXT_MENU_RANGE )
			{
				m_Broker = broker;
				m_From = from;
			}

			/// <summary>
			/// Handles the context menu click.
			/// </summary>
			public override void OnClick()
			{
				m_Broker.BeginPetSale( m_From, true );
			}
		}

		#endregion
	} 
   
	/// <summary>
	/// Context menu entry for requesting TamingBOD contracts from AnimalTrainerLord.
	/// </summary>
	public class TamingBODDealerEntry : ContextMenuEntry
	{
		#region Fields

		/// <summary>
		/// Delay between contract requests (shared with speech command).
		/// </summary>
		public static TimeSpan Delay = TimeSpan.FromHours( AnimalBrokerConstants.CONTRACT_DELAY_HOURS );
		
		/// <summary>
		/// Dictionary tracking last contract request time per player (shared with speech command).
		/// </summary>
		public static Dictionary<PlayerMobile, DateTime> LastUsers = new Dictionary<PlayerMobile, DateTime>();
		
		private Mobile m_Mobile;
		private Mobile m_Giver;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TamingBODDealerEntry class.
		/// </summary>
		/// <param name="from">The mobile requesting the contract</param>
		/// <param name="giver">The AnimalTrainerLord providing the contract</param>
		public TamingBODDealerEntry( Mobile from, Mobile giver ) : base( AnimalBrokerConstants.CONTEXT_MENU_ID, AnimalBrokerConstants.CONTEXT_MENU_RANGE )
		{
			m_Mobile = from;
			m_Giver = giver;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Handles the context menu click.
		/// </summary>
		public override void OnClick()
		{
			if ( !( m_Mobile is PlayerMobile ) )
				return;
			
			PlayerMobile mobile = (PlayerMobile) m_Mobile;

			if ( CanGetContract( mobile ) )
			{
				if ( !mobile.HasGump( typeof( TamingBODDealerGump ) ) )
				{
					mobile.SendGump( new TamingBODDealerGump( mobile ) );
					// Contract is created in gump's OnResponse when player clicks the button
				}
			}
			else
			{
				m_Giver.Say( AnimalBrokerStringConstants.MSG_CONTRACT_NOT_AVAILABLE );
			}
		}
		
		/// <summary>
		/// Checks if the player can get a contract (cooldown check).
		/// </summary>
		/// <param name="asker">The player requesting the contract</param>
		/// <returns>True if contract can be given, false if on cooldown</returns>
		public static bool CanGetContract( PlayerMobile asker )
		{
			if ( asker.AccessLevel > AccessLevel.Player )
				return true;
			
			if ( !LastUsers.ContainsKey( asker ) )
			{
				LastUsers.Add( asker, DateTime.UtcNow );
				return true;
			}
			else
			{
				if ( DateTime.UtcNow - LastUsers[asker] < Delay )
				{
					return false;
				}
				else
				{
					LastUsers[asker] = DateTime.UtcNow;
					return true;
				}
			}
		}

		#endregion
	}
} 

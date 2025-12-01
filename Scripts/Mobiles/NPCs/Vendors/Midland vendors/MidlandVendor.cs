using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Gumps;
using Server.OneTime;
using Server.Regions;
using System.Text.RegularExpressions;

namespace Server.Mobiles
{
	/// <summary>
	/// Custom vendor system for Midland region.
	/// Uses speech-based interaction and dynamic inventory-based pricing.
	/// Does not inherit from BaseVendor - uses custom pricing system.
	/// </summary>
	[CorpseName( "an vendor corpse" )]
	public class MidlandVendor : BaseConvo, IOneTime
	{
		#region Fields
		
		private DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }

		private int m_OneTimeType;
        public int OneTimeType
        {
            get{ return m_OneTimeType; }
            set{ m_OneTimeType = value; }
        }

		private int m_good1inventory;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good1inventory
        {
            get{ return m_good1inventory; }
            set{ m_good1inventory = value; }
        }

		private Type m_good1;
		[CommandProperty( AccessLevel.GameMaster )]
        public Type good1
        {
            get{ return m_good1; }
            set{ m_good1 = value; }
        }

		private string m_good1name;
		[CommandProperty( AccessLevel.GameMaster )]
        public string good1name
        {
            get{ return m_good1name; }
            set{ m_good1name = value; }
        }
		private int m_good1price;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good1price
        {
            get{ return m_good1price; }
            set{ m_good1price = value; }
        }

		private double m_good1adjust;
		[CommandProperty( AccessLevel.GameMaster )]
        public double good1adjust
        {
            get{ return m_good1adjust; }
            set{ m_good1adjust = value; }
        }

		private int m_good2inventory;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good2inventory
        {
            get{ return m_good2inventory; }
            set{ m_good2inventory = value; }
        }
		private Type m_good2;
		[CommandProperty( AccessLevel.GameMaster )]
        public Type good2
        {
            get{ return m_good2; }
            set{ m_good2 = value; }
        }	

		private string m_good2name;
		[CommandProperty( AccessLevel.GameMaster )]
        public string good2name
        {
            get{ return m_good2name; }
            set{ m_good2name = value; }
        }
		private int m_good2price;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good2price
        {
            get{ return m_good2price; }
            set{ m_good2price = value; }
        }

		private double m_good2adjust;
		[CommandProperty( AccessLevel.GameMaster )]
        public double good2adjust
        {
            get{ return m_good2adjust; }
            set{ m_good2adjust = value; }
        }

		private int m_good3inventory;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good3inventory
        {
            get{ return m_good3inventory; }
            set{ m_good3inventory = value; }
        }
		private Type m_good3;
		[CommandProperty( AccessLevel.GameMaster )]
        public Type good3
        {
            get{ return m_good3; }
            set{ m_good3 = value; }
        }

		private string m_good3name;
		[CommandProperty( AccessLevel.GameMaster )]
        public string good3name
        {
            get{ return m_good3name; }
            set{ m_good3name = value; }
        }	
		private int m_good3price;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good3price
        {
            get{ return m_good3price; }
            set{ m_good3price = value; }
        }

		private double m_good3adjust;
		[CommandProperty( AccessLevel.GameMaster )]
        public double good3adjust
        {
            get{ return m_good3adjust; }
            set{ m_good3adjust = value; }
        }

		private int m_good4inventory;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good4inventory
        {
            get{ return m_good4inventory; }
            set{ m_good4inventory = value; }
        }
		private Type m_good4;
		[CommandProperty( AccessLevel.GameMaster )]
        public Type good4
        {
            get{ return m_good4; }
            set{ m_good4 = value; }
        }

		private string m_good4name;
		[CommandProperty( AccessLevel.GameMaster )]
        public string good4name
        {
            get{ return m_good4name; }
            set{ m_good4name = value; }
        }
		private int m_good4price;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good4price
        {
            get{ return m_good4price; }
            set{ m_good4price = value; }
        }

		private double m_good4adjust;
		[CommandProperty( AccessLevel.GameMaster )]
        public double good4adjust
        {
            get{ return m_good4adjust; }
            set{ m_good4adjust = value; }
        }

		private int m_moneytype;
		[CommandProperty( AccessLevel.GameMaster )]
        public int moneytype
        {
            get{ return m_moneytype; }
            set{ m_moneytype = value; }
        }

		private bool buying;
		private string buyingwhat;
		private Mobile buyer;

		private int buyingamount;

		private bool selling;
		private string sellingwhat;
		private Mobile seller;

		private int sellingamount;

		private int saletick;
		
		#endregion
		
		#region Properties
		
		// + OmniAI support +
		protected override BaseAI ForcedAI
		{
			get
			{
			return new OmniAI(this);
			}
		}
		// - OmniAI support -
		
		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public virtual bool IsInvulnerable { get { return false; } }
		public override bool Unprovokable { get { return true; } }
		public override bool Uncalmable{ get{ return true; } }
		
		#endregion
		
		#region Constructors


		[Constructable]
		public MidlandVendor() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			m_OneTimeType = 3;

			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			

			if ( Female = Utility.RandomBool() ) 
			{ 
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
			}
			else 
			{ 
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
			}


			Karma = Utility.RandomMinMax( MidlandVendorConstants.KARMA_MIN, MidlandVendorConstants.KARMA_MAX );

			SetStr( MidlandVendorConstants.STAT_MIN, MidlandVendorConstants.STAT_MAX );
			SetDex( MidlandVendorConstants.STAT_MIN, MidlandVendorConstants.STAT_MAX );
			SetInt( MidlandVendorConstants.STAT_MIN, MidlandVendorConstants.STAT_MAX );

			SetHits( MidlandVendorConstants.HITS_MIN, MidlandVendorConstants.HITS_MAX );
			SetDamage( MidlandVendorConstants.DAMAGE_MIN, MidlandVendorConstants.DAMAGE_MAX );

			VirtualArmor = MidlandVendorConstants.VIRTUAL_ARMOR;

			SetDamageType( ResistanceType.Physical, MidlandVendorConstants.DAMAGE_PHYSICAL_PERCENT );
			SetDamageType( ResistanceType.Cold, MidlandVendorConstants.DAMAGE_COLD_PERCENT );
			SetDamageType( ResistanceType.Energy, MidlandVendorConstants.DAMAGE_ENERGY_PERCENT );

			SetResistance( ResistanceType.Physical, MidlandVendorConstants.RESISTANCE_PHYSICAL_MIN, MidlandVendorConstants.RESISTANCE_PHYSICAL_MAX );
			SetResistance( ResistanceType.Fire, MidlandVendorConstants.RESISTANCE_FIRE_MIN, MidlandVendorConstants.RESISTANCE_FIRE_MAX );
			SetResistance( ResistanceType.Cold, MidlandVendorConstants.RESISTANCE_COLD_MIN, MidlandVendorConstants.RESISTANCE_COLD_MAX );
			SetResistance( ResistanceType.Poison, MidlandVendorConstants.RESISTANCE_POISON_MIN, MidlandVendorConstants.RESISTANCE_POISON_MAX );
			SetResistance( ResistanceType.Energy, MidlandVendorConstants.RESISTANCE_ENERGY_MIN, MidlandVendorConstants.RESISTANCE_ENERGY_MAX );

			SetSkill( SkillName.EvalInt, (int)MidlandVendorConstants.SKILL_MIN, (int)MidlandVendorConstants.SKILL_MAX );
			SetSkill( SkillName.Magery, (int)MidlandVendorConstants.SKILL_MIN, (int)MidlandVendorConstants.SKILL_MAX );
			SetSkill( SkillName.Meditation, (int)MidlandVendorConstants.SKILL_MIN, (int)MidlandVendorConstants.SKILL_MAX );
			SetSkill( SkillName.Poisoning, (int)MidlandVendorConstants.SKILL_MIN, (int)MidlandVendorConstants.SKILL_MAX );
			SetSkill( SkillName.MagicResist, (int)MidlandVendorConstants.SKILL_MIN, (int)MidlandVendorConstants.SKILL_MAX );
			SetSkill( SkillName.Tactics, (int)MidlandVendorConstants.SKILL_MIN, (int)MidlandVendorConstants.SKILL_MAX );
			SetSkill( SkillName.Wrestling, (int)MidlandVendorConstants.SKILL_MIN, (int)MidlandVendorConstants.SKILL_MAX );
			SetSkill( SkillName.Macing, (int)MidlandVendorConstants.SKILL_MIN, (int)MidlandVendorConstants.SKILL_MAX );

			OmniAI.SetRandomSkillSet(this, MidlandVendorConstants.OMNIAI_SKILL_MIN, MidlandVendorConstants.OMNIAI_SKILL_MAX);

			buying = false;
			buyingwhat = "";
			buyingamount = 0;
			saletick = 0;
			selling = false;
			sellingwhat = "";
			sellingamount = 0;

			good1price = 0;
			good1name = "";
			good1inventory = 0;

			good2price = 0;
			good2name = "";
			good2inventory = 0;

			good3price = 0;
			good3name = "";
			good3inventory = 0;

			good4price = 0;
			good4name = "";
			good4inventory = 0;

			AdjustPrice();
			CantWalk = true;

		}
		
		#endregion
		
		#region Combat Methods
		
		/// <summary>
		/// Called when vendor performs a melee attack.
		/// </summary>
		public override void OnGaveMeleeAttack( Mobile defender )
		{
			switch ( Utility.Random( 4 ))  
			{
				case 0: Say(MidlandVendorStringConstants.MSG_GUARDS_1); break;
				case 1: Say(MidlandVendorStringConstants.MSG_GUARDS_2); break;
				case 2: Say(MidlandVendorStringConstants.MSG_GUARDS_3); break;
				case 3: Say(MidlandVendorStringConstants.MSG_GUARDS_4); break;
			};
		}

		public override bool IsEnemy( Mobile m )
		{
			if (m.Criminal)
				return true;

			if ( IntelligentAction.GetMidlandEnemies( m, this, true ) == false )
				return false;

			return true;
		}

		#endregion
		
		#region Speech Handling
		
		/// <summary>
		/// Determines if vendor handles speech from the given mobile.
		/// </summary>
		public override bool HandlesOnSpeech( Mobile from ) 
		{ 
			return (from != null && from.Player && from.Alive && (int)GetDistanceToSqrt( from ) < MidlandVendorConstants.SPEECH_RANGE && from != ControlMaster); 
		} 

		public override void OnSpeech( SpeechEventArgs e ) 
		{

			base.OnSpeech(e);

			if (e.Mobile == null)
				return;

			string speech = e.Speech;

			Mobile from = (Mobile)e.Mobile;

			if (from is PlayerMobile && IntelligentAction.RaceCheck( this, from) )
				return;

			this.Direction = this.GetDirectionTo( e.Mobile.Location );

			string mn = GetCurrency();

			if( from.InRange( this, MidlandVendorConstants.INTERACTION_RANGE ))
			{
				if (  Insensitive.Contains( speech, "inventory" ) || Insensitive.Contains( speech, "list" ) || Insensitive.Contains( speech, "you have" ) || Insensitive.Contains( speech, "stock" ) || Insensitive.Contains( speech, "for sale" ))
				{
					if (this is MidlandBanker)
					{
						Say(MidlandVendorStringConstants.MSG_BANKER_DESCRIPTION);
						return;
					}
				
					AdjustPrice();

					Say(MidlandVendorStringConstants.MSG_INVENTORY_HEADER);
					bool g = false;
					string nm = GetCurrency();

					if (good1inventory > 0 && good1name != null)
					{
						Say(string.Format(MidlandVendorStringConstants.MSG_INVENTORY_ITEM_FORMAT, good1inventory.ToString(), good1name, good1price, nm));
						g = true;
					}
					if (good2inventory > 0 && good2name != null)
					{
						Say(string.Format(MidlandVendorStringConstants.MSG_INVENTORY_ITEM_FORMAT, good2inventory.ToString(), good2name, good2price, nm));
						g = true;
					}
					if (good3inventory > 0 && good3name != null)
					{
						Say(string.Format(MidlandVendorStringConstants.MSG_INVENTORY_ITEM_FORMAT, good3inventory.ToString(), good3name, good3price, nm));
						g = true;
					}
					if (good4inventory > 0 && good4name != null)
					{
						Say(string.Format(MidlandVendorStringConstants.MSG_INVENTORY_ITEM_FORMAT, good4inventory.ToString(), good4name, good4price, nm));
						g = true;
					}

					if (g)
						Say(MidlandVendorStringConstants.MSG_SERVICE_FEE);

					if (!g)
					{
						Say(MidlandVendorStringConstants.MSG_EMPTY_STOCK);
						Say(string.Format(MidlandVendorStringConstants.MSG_DEAL_IN_ITEMS_FORMAT, good1name ?? "", good2name ?? "", good3name ?? "", good4name ?? ""));
						Say(MidlandVendorStringConstants.MSG_ASK_FOR_PRICES);
					}
						
					return;

				}
				if (  Insensitive.Contains( speech, "price" ) || Insensitive.Contains( speech, "prices" ) || Insensitive.Contains( speech, "cost" ))
				{

					if (this is MidlandBanker)
					{
						Say(MidlandVendorStringConstants.MSG_BANKER_FEE);
						return;
					}

					AdjustPrice();
					string nm = GetCurrency();

					Say(MidlandVendorStringConstants.MSG_PRICE_HEADER);
					if (good1name != null && good1name != "")
					{
						Say(string.Format(MidlandVendorStringConstants.MSG_BUY_PRICE_FORMAT, good1name, good1price, nm));
					}
					if (good2name != null && good2name != "")
					{
						Say(string.Format(MidlandVendorStringConstants.MSG_BUY_PRICE_FORMAT, good2name, good2price, nm));
					}
					if (good3name != null && good3name != "")
					{
						Say(string.Format(MidlandVendorStringConstants.MSG_BUY_PRICE_FORMAT, good3name, good3price, nm));
					}
					if (good4name != null && good4name != "")
					{
						Say(string.Format(MidlandVendorStringConstants.MSG_BUY_PRICE_FORMAT, good4name, good4price, nm));
					}
						
					return;
				}
				//text parser

				if (  Insensitive.Contains( speech, "buy" ) && !buying)
				{

					if (this is MidlandBanker)
					{
						Say(MidlandVendorStringConstants.MSG_BANKER_CANNOT_BUY);
						return;
					}

					buyingwhat = ""; //reset from previous orders
					buyingamount = 0;

					if (Insensitive.Contains( speech, good1name ))
						buyingwhat = good1name;
					else if (Insensitive.Contains( speech, good2name ))
						buyingwhat = good2name;
					else if (Insensitive.Contains( speech, good3name ))
						buyingwhat = good3name;
					else if (Insensitive.Contains( speech, good4name ))
						buyingwhat = good4name;

					if (buyingwhat != "" )
					{
						buying = true;
						buyer = e.Mobile;
					}
				}
				else if (  Insensitive.Contains( speech, "sell" ) && !selling)
				{

					if (this is MidlandBanker)
					{
						Say(MidlandVendorStringConstants.MSG_BANKER_NOT_INTERESTED);
						return;
					}

					sellingwhat = ""; //reset from previous orders
					sellingamount = 0;

					if (Insensitive.Contains( speech, good1name ))
						sellingwhat = good1name;
					else if (Insensitive.Contains( speech, good2name ))
						sellingwhat = good2name;
					else if (Insensitive.Contains( speech, good3name ))
						sellingwhat = good3name;
					else if (Insensitive.Contains( speech, good4name ))
						sellingwhat = good4name;

					if (sellingwhat != "" )
					{
						selling = true;
						seller = e.Mobile;
					}
				}


				if ( (buying && from == buyer) || (selling && from == seller))
				{
					
					String number = Regex.Match(e.Speech, @"\d+").Value;

					int amount = 0;
					if (number != null)
						int.TryParse(number, out amount);
					if (amount >= 1)
					{
							if (buying && !CheckInventory(buyingwhat, amount)) //checks amount of item on hand
							{
								Say(string.Format(MidlandVendorStringConstants.MSG_INSUFFICIENT_INVENTORY_FORMAT, buyingwhat));
							}
							else if (buying)
								ProcessBuy(buyer, buyingwhat, amount);
							else if (selling)
							{
								ProcessSell(seller, sellingwhat, amount);
							}
					}
					else if (buying)//amount not received
							Say(string.Format(MidlandVendorStringConstants.MSG_BUY_AMOUNT_REQUEST_FORMAT, buyingwhat));
					else if (selling)
							Say(string.Format(MidlandVendorStringConstants.MSG_SELL_AMOUNT_REQUEST_FORMAT, sellingwhat));

				}
				else if ((selling && from != seller) || (buying && from != buyer))
				{
					Say(string.Format(MidlandVendorStringConstants.MSG_ONE_CUSTOMER_FORMAT, from.Name));
				}

			} 
		} 

		public bool CheckInventory(string what, int amount)
		{
			bool instock = false;
			if (what == good1name && amount <= good1inventory)
				instock = true;
			if (what == good2name && amount <= good2inventory)
				instock = true;
			if (what == good3name && amount <= good3inventory)
				instock = true;
			if (what == good4name && amount <= good4inventory)
				instock = true;

			if (instock)
				return true;
			
			Say(string.Format(MidlandVendorStringConstants.MSG_NOT_ENOUGH_STOCK_FORMAT, what));
			return false;
		}
		
		#endregion
		
		#region Transaction Processing

		/// <summary>
		/// Processes a buy transaction - player buys from vendor.
		/// </summary>
		/// <param name="buyer">The player buying</param>
		/// <param name="what">The item name being bought</param>
		/// <param name="amount">The quantity being bought</param>
		public void ProcessBuy(Mobile buyer, string what, int amount)
		{
			AdjustPrice();

			Item ii = null;
			Type a = null;
			// give the goods
			if (what == good1name)
			{
				a = good1;
				ii = (Item)Activator.CreateInstance(a);
			}
			if (what == good2name)
			{
				a = good2;
				ii = (Item)Activator.CreateInstance(a);
			}
			if (what == good3name)
			{
				a = good3;
				ii = (Item)Activator.CreateInstance(a);
			}
			if (what == good4name)
			{
				a = good4;
				ii = (Item)Activator.CreateInstance(a);
			}


			if (a == null )
			{
				buyer.SendMessage(MidlandVendorStringConstants.MSG_SALE_PROBLEM);
				this.buying = false;
				this.buyingamount = 0;
				this.buyingwhat = "";
				this.buyer = null;
				return;
			}

			int price = CalculatePrice(what, amount, false);
			price = (int)((double)price * MidlandVendorConstants.BUY_PRICE_MULTIPLIER);

			if (!GetMoney(buyer, price))
			{
				AdjustInventory(what, amount, true);
				Say(MidlandVendorStringConstants.MSG_INSUFFICIENT_FUNDS);
				return;
			}

			if (amount > 0 && buyer is PlayerMobile)
				((PlayerMobile)buyer).AdjustReputation(price / MidlandVendorConstants.REPUTATION_DIVISOR, ((BaseCreature)this).midrace, true);

			buyer.Backpack.DropItem(ii);
			amount -=1;
			if ( !(ii is Container) && amount > 1 && ii.StackWith( buyer, (Item)Activator.CreateInstance(a), false ))
			{
				amount -= 1;
				ii.Amount += amount;
			}
			else 
			{
				for (int i = 0; i < amount; ++i)
				{
					buyer.Backpack.DropItem((Item)Activator.CreateInstance(a));
				}
			}
			Say(MidlandVendorStringConstants.MSG_THANK_YOU);			

			this.buying = false;
			this.buyingamount = 0;
			this.buyingwhat = "";
			this.buyer = null;
			
		}

		/// <summary>
		/// Processes a sell transaction - player sells to vendor.
		/// </summary>
		/// <param name="buyer">The player selling (parameter name kept for compatibility)</param>
		/// <param name="what">The item name being sold</param>
		/// <param name="amount">The quantity being sold</param>
		public void ProcessSell(Mobile buyer, string what, int amount)
		{
 			
			AdjustPrice();

			Type ii = null;
			// give the goods
			if (what == good1name)
			{
				ii =  good1;
			}
			if (what == good2name)
			{
				ii =  good2;
			}
			if (what == good3name)
			{
				ii = good3;
			}
			if (what == good4name)
			{
				ii =  good4;
			} 

			// check player has enough
			bool check = false;
			int aamount = amount;

			List<Server.Item> listy = buyer.Backpack.Items;

			for ( int i = 0; i < listy.Count; ++i )
			{
				Item item = listy[i];
				if (item.GetType() == ii)
				{
					if (item.Amount >= aamount)
						check = true;
					else 
					{
						aamount -= 1;
						if (aamount == 0)
						{
							check = true;
						}
					}
				}
			}
			if (!check)
			{
				Say(string.Format(MidlandVendorStringConstants.MSG_PLAYER_DOESNT_HAVE_FORMAT, amount, what));
			}
			else
			{
				aamount = amount;
				int price = CalculatePrice(what, amount, true);
				price = (int)((double)price * MidlandVendorConstants.SELL_PRICE_MULTIPLIER);

				if (!GiveMoney(buyer, price))
					return;
					
				if (price == 0)
					buyer.SendMessage(MidlandVendorStringConstants.MSG_ZERO_TOTAL);


			if (price > 0 && buyer is PlayerMobile)
				((PlayerMobile)buyer).AdjustReputation(price / MidlandVendorConstants.REPUTATION_DIVISOR, ((BaseCreature)this).midrace, true);
				
				for ( int i = 0; i < listy.Count; ++i )
				{
					Item item = listy[i];
					if (item.GetType() == ii)
					{
						if (item.Amount > aamount)
						{
							item.Amount -= aamount;
						}
						else if (item.Amount <= aamount)
						{
							aamount -= item.Amount;
							item.Delete();
						}
						else 
						{
							aamount -= 1;
							item.Delete();
							if (aamount == 0)
							{
								break;
							}
						}
					}
				}
				Say(MidlandVendorStringConstants.MSG_PLEASURE_BUSINESS);

				this.selling = false;
				this.sellingamount = 0;
				this.sellingwhat = "";
				this.seller = null;

			}

		}
		
		#endregion
		
		#region Money Operations
		
		/// <summary>
		/// Gives money to a player (to backpack or bank account).
		/// Uses MidlandMoneyHelper for currency operations.
		/// </summary>
		/// <param name="from">The player to give money to</param>
		/// <param name="amount">The amount to give</param>
		/// <returns>True if money was successfully given</returns>
		public bool GiveMoney( Mobile from, int amount)
		{
			if (!AdventuresFunctions.IsInMidland((object)this) || !AdventuresFunctions.IsInMidland((object)from) || !(from is PlayerMobile) || ((PlayerMobile)from).midrace == 0 )
				return false;

			if (amount < 0 )
				return false;

			PlayerMobile pm = (PlayerMobile)from;
			
			if (m_moneytype != this.midrace)
			{
				this.Say(MidlandVendorStringConstants.MSG_DONT_SERVE_YOUR_KIND);
				return false;
			}

			return MidlandMoneyHelper.GiveMoney(pm, amount, m_moneytype, this.midrace);
		}

		/// <summary>
		/// Gets money from a player (from backpack or bank account).
		/// Uses MidlandMoneyHelper for currency operations.
		/// </summary>
		/// <param name="from">The player to get money from</param>
		/// <param name="amount">The amount to get</param>
		/// <returns>True if money was successfully retrieved</returns>
		public bool GetMoney( Mobile from, int amount)
		{
			if (!AdventuresFunctions.IsInMidland((object)this) || !AdventuresFunctions.IsInMidland((object)from) || !(from is PlayerMobile) || ((PlayerMobile)from).midrace == 0 )
				return false;

			PlayerMobile pm = (PlayerMobile)from;
			
			if (m_moneytype != this.midrace)
			{
				this.Say(MidlandVendorStringConstants.MSG_DONT_SERVE_YOUR_KIND);
				return false;
			}

			return MidlandMoneyHelper.GetMoney(pm, amount, m_moneytype, this.midrace);
		}
		
		#endregion
		
		#region Price Calculation

		public int CalculatePrice (string what, int amount, bool increase)
		{
			int price = 0;
			int total = 0;

			if (what == good1name)
			{
				price = good1price;
			}
			if (what == good2name)
			{
				price = good2price;
			}
			if (what == good3name)
			{
				price = good3price;
			}
			if (what == good4name)
			{
				price = good4price;
			}			

			if (amount > MidlandVendorConstants.BULK_AMOUNT_THRESHOLD)
			{
				int a = amount;
				
				while ( a != 0 )
				{
					if (a >= MidlandVendorConstants.BULK_CHUNK_SIZE)
					{
						total += (MidlandVendorConstants.BULK_CHUNK_SIZE * price);
						a -= MidlandVendorConstants.BULK_CHUNK_SIZE;
						AdjustInventory(what, MidlandVendorConstants.BULK_CHUNK_SIZE, increase);
					}
					else
					{
						total += (a * price);
						a = 0;
						AdjustInventory(what, a, increase);						
					}
				}
			}
			else
				total = amount * price;

			return total;
		}

		public void AdjustInventory( string what, int amount, bool increase)
		{
			if (what == good1name)
			{
				if (increase)
					good1inventory += amount;
				else
					good1inventory -= amount;
			}
			if (what == good2name)
			{
				if (increase)
					good2inventory += amount;
				else
					good2inventory -= amount;
			}
			if (what == good3name)
			{
				if (increase)
					good3inventory += amount;
				else
					good3inventory -= amount;
			}
			if (what == good4name)
			{
				if (increase)
					good4inventory += amount;
				else
					good4inventory -= amount;
			}	
			AdjustPrice();		
		}

		/// <summary>
		/// Adjusts prices based on current inventory levels.
		/// Uses dynamic pricing formula: price decreases as inventory increases.
		/// </summary>
		public void AdjustPrice()
		{
			if (good1inventory > 0)
				good1price = MidlandVendorConstants.MIN_PRICE + ((int)(MidlandVendorConstants.PRICE_BASE_MULTIPLIER * good1adjust) - (int)(((MidlandVendorConstants.PRICE_BASE_MULTIPLIER * good1adjust) / (MidlandVendorConstants.PRICE_INVENTORY_DIVISOR / good1adjust)) * (double)good1inventory));
			else
				good1price = (int)(MidlandVendorConstants.PRICE_BASE_MULTIPLIER * good1adjust);

			if (good2inventory > 0)
				good2price = MidlandVendorConstants.MIN_PRICE + ((int)(MidlandVendorConstants.PRICE_BASE_MULTIPLIER * good2adjust) - (int)(((MidlandVendorConstants.PRICE_BASE_MULTIPLIER * good2adjust) / (MidlandVendorConstants.PRICE_INVENTORY_DIVISOR / good2adjust)) * (double)good2inventory));
			else
				good2price = (int)(MidlandVendorConstants.PRICE_BASE_MULTIPLIER * good2adjust);

			if (good3inventory > 0)
				good3price = MidlandVendorConstants.MIN_PRICE + ((int)(MidlandVendorConstants.PRICE_BASE_MULTIPLIER * good3adjust) - (int)(((MidlandVendorConstants.PRICE_BASE_MULTIPLIER * good3adjust) / (MidlandVendorConstants.PRICE_INVENTORY_DIVISOR / good3adjust)) * (double)good3inventory));
			else
				good3price = (int)(MidlandVendorConstants.PRICE_BASE_MULTIPLIER * good3adjust);

			if (good4inventory > 0)
				good4price = MidlandVendorConstants.MIN_PRICE + ((int)(MidlandVendorConstants.PRICE_BASE_MULTIPLIER * good4adjust) - (int)(((MidlandVendorConstants.PRICE_BASE_MULTIPLIER * good4adjust) / (MidlandVendorConstants.PRICE_INVENTORY_DIVISOR / good4adjust)) * (double)good4inventory));
			else
				good4price = (int)(MidlandVendorConstants.PRICE_BASE_MULTIPLIER * good4adjust);
		}

		/// <summary>
		/// Gets the currency name for the vendor's money type.
		/// </summary>
		/// <returns>Currency name string</returns>
		public string GetCurrency()
		{
			return MidlandMoneyHelper.GetCurrencyName(m_moneytype);
		}
		
		/// <summary>
		/// Gets a random greeting message based on vendor race.
		/// </summary>
		/// <returns>Random greeting string</returns>
		private string GetRandomGreeting()
		{
			switch ( Utility.Random( 6 ))
			{
				case 0: return MidlandVendorStringConstants.MSG_GREETING_1;
				case 1: return MidlandVendorStringConstants.MSG_GREETING_2;
				case 2: return MidlandVendorStringConstants.MSG_GREETING_3;
				case 3: return MidlandVendorStringConstants.MSG_GREETING_4;
				case 4: return MidlandVendorStringConstants.MSG_GREETING_5;
				case 5: return MidlandVendorStringConstants.MSG_GREETING_6;
				default: return MidlandVendorStringConstants.MSG_GREETING_6;
			}
		}
		
		#endregion
		
		#region Event Handlers

		/// <summary>
		/// Called when a mobile moves near the vendor.
		/// </summary>
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			base.OnMovement(m, oldLocation);

			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextTalk && InRange( m, MidlandVendorConstants.MOVEMENT_RANGE ) && InLOS( m ) )
				{ 
					Say(GetRandomGreeting());
					m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( MidlandVendorConstants.TALK_DELAY_SECONDS ));
				}
			}
			base.OnMovement(m, oldLocation );
			
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if (dropped == null || from == null)
				return false;

			bool sale = false;
			string what = "";

			if (dropped.GetType() == good1)
			{
				sale = true;
				what = good1name;
			}
			if (dropped.GetType() == good2)
			{
				sale = true;
				what = good2name;
			}
			if (dropped.GetType() == good3)
			{
				sale = true;
				what = good3name;
			}
			if (dropped.GetType() == good4)
			{
				sale = true;
				what = good4name;
			}

			if (!sale )
			{
				Say(MidlandVendorStringConstants.MSG_DONT_TRADE);
				return false;
			}
			else
			{
				int money = CalculatePrice(what, dropped.Amount, true);

				if (!GiveMoney(from, money))
				{
					Say(MidlandVendorStringConstants.MSG_DONT_THINK_SO);
					return false;
				}
				Say(string.Format(MidlandVendorStringConstants.MSG_PURCHASE_CONFIRMATION_FORMAT, dropped.Amount, what, money, GetCurrency()));
				if (money > MidlandVendorConstants.BANK_DEPOSIT_THRESHOLD)
					Say(MidlandVendorStringConstants.MSG_BANK_DEPOSIT_NOTIFICATION);

				dropped.Delete();
				return true;
			}

			return base.OnDragDrop( from, dropped );
		}

		/// <summary>
		/// Called periodically by OneTime system.
		/// Handles sale timeout and inventory decay.
		/// </summary>
		public void OneTimeTick()
        {
			if (saletick == 0 && (buying || selling))
			{
				saletick = MidlandVendorConstants.SALE_TICK_TIMEOUT;
			}
			else if (saletick == 1)
			{
				if (selling)
				{		
					this.selling = false;
					this.sellingamount = 0;
					this.sellingwhat = "";
					this.seller = null;
					this.PublicOverheadMessage(MessageType.Regular, 0x3B2, false, MidlandVendorStringConstants.MSG_BACK_TO_BUSINESS);
				}
				if (buying)
				{
					this.buying = false;
					this.buyingamount = 0;
					this.buyingwhat = "";
					this.buyer = null;
					this.PublicOverheadMessage(MessageType.Regular, 0x3B2, false, MidlandVendorStringConstants.MSG_BACK_TO_BUSINESS);
				}
			}
			saletick -=1;

			if (Utility.RandomMinMax(MidlandVendorConstants.INVENTORY_DECAY_CHANCE_MIN, MidlandVendorConstants.INVENTORY_DECAY_CHANCE_MAX) == MidlandVendorConstants.INVENTORY_DECAY_TRIGGER)
			{
				if (Utility.RandomBool() && good1inventory > MidlandVendorConstants.INVENTORY_DECAY_MIN_STOCK)
					good1inventory -= Utility.RandomMinMax(MidlandVendorConstants.INVENTORY_DECAY_MIN, MidlandVendorConstants.INVENTORY_DECAY_MAX);
				if (Utility.RandomBool() && good2inventory > MidlandVendorConstants.INVENTORY_DECAY_MIN_STOCK)
					good2inventory -= Utility.RandomMinMax(MidlandVendorConstants.INVENTORY_DECAY_MIN, MidlandVendorConstants.INVENTORY_DECAY_MAX);
				if (Utility.RandomBool() && good3inventory > MidlandVendorConstants.INVENTORY_DECAY_MIN_STOCK)
					good3inventory -= Utility.RandomMinMax(MidlandVendorConstants.INVENTORY_DECAY_MIN, MidlandVendorConstants.INVENTORY_DECAY_MAX);
				if (Utility.RandomBool() && good4inventory > MidlandVendorConstants.INVENTORY_DECAY_MIN_STOCK)
					good4inventory -= Utility.RandomMinMax(MidlandVendorConstants.INVENTORY_DECAY_MIN, MidlandVendorConstants.INVENTORY_DECAY_MAX);
			}

		}
		
		#endregion
		
		#region Helper Methods

		public Type FindType(string what)
		{
			if (what == m_good1name)
			{
				return m_good1;
			}
			if (what == m_good2name)
			{
				return m_good2;
			}
			if (what == m_good3name)
			{
				return m_good3;
			}
			if (what == m_good4name)
			{
				return m_good4;
			} 
			return null;
		}

		public void Dress()
		{
			if (((BaseCreature)this).midrace == 1)
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0: AddItem( new FancyShirt( RandomThings.GetRandomColor(0) ) ); break;
					case 1: AddItem( new Doublet( RandomThings.GetRandomColor(0) ) ); break;
					case 2: AddItem( new Shirt( RandomThings.GetRandomColor(0) ) ); break;
				}

				switch ( ShoeType )
				{
					case VendorShoeType.Shoes: AddItem( new Shoes( GetShoeHue() ) ); break;
					case VendorShoeType.Boots: AddItem( new Boots( GetShoeHue() ) ); break;
					case VendorShoeType.Sandals: AddItem( new Sandals( GetShoeHue() ) ); break;
					case VendorShoeType.ThighBoots: AddItem( new ThighBoots( GetShoeHue() ) ); break;
				}

				int hairHue =  RandomThings.GetRandomHairColor();

				Utility.AssignRandomHair( this, hairHue );
				Utility.AssignRandomFacialHair( this, hairHue );

				if ( Female )
				{
					switch ( Utility.Random( 6 ) )
					{
						case 0: AddItem( new ShortPants( RandomThings.GetRandomColor(0) ) ); break;
						case 1:
						case 2: AddItem( new Kilt( RandomThings.GetRandomColor(0) ) ); break;
						case 3:
						case 4:
						case 5: AddItem( new Skirt( RandomThings.GetRandomColor(0) ) ); break;
					}
				}
				else
				{
					FacialHairItemID = Utility.RandomList( 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
					FacialHairHue = hairHue;

					switch ( Utility.Random( 2 ) )
					{
						case 0: AddItem( new LongPants( RandomThings.GetRandomColor(0) ) ); break;
						case 1: AddItem( new ShortPants( RandomThings.GetRandomColor(0) ) ); break;
					}
				}


			}
			if (((BaseCreature)this).midrace == 4)
			{
			    AddItem( new ElvenBoots( 0x83A ) );
            	Item armor = new LeatherChest(); armor.Hue = 0x83A; AddItem( armor );
                Item robe = new PirateRobe(); robe.Hue = 0x455; AddItem(robe);
                AddItem( new FancyShirt( 0 ) );	

				switch ( Utility.Random( 2 ))
				{
					case 0: AddItem( new LongPants ( 0xBB4 ) ); break;
					case 1: AddItem( new ShortPants ( 0xBB4 ) ); break;
				}	
			}

            if (((BaseCreature)this).midrace == 5)
            {
                Item boots = new FurBoots();
                boots.Name = "Ugg Boots";
                boots.Hue = 357;
                AddItem(boots);

                Item cape = new FurCape();
                cape.Name = "Pelt Cape";
                cape.Hue = 351;
                AddItem(cape);

                Item skirt = new StuddedSkirt();
                skirt.Name = "Reed Skirt";
                skirt.Hue = 357;
                AddItem(skirt);

                Item gloves = new StuddedGloves();
                gloves.Hue = 357;
                AddItem(gloves);

                Item fetish = new SilverNecklace();
                fetish.Name = "Orkish Fetish";
                fetish.Hue = 357;
                AddItem(fetish);

                Item arms = new DragonArms();
                arms.Name = "Reed Armlets";
                arms.Hue = 357;
                AddItem(arms);

                Item malechest = new StuddedDo();
                malechest.Name = "Reed Chest";
                malechest.Hue = 357;

                Item femalechest = new FemaleStuddedChest();
                femalechest.Name = "Reed Chest";
                femalechest.Hue = 351;

                if (Female)
                {

                    AddItem(femalechest);
                }
                else
                {
                    AddItem(malechest);
                }

            }


			//PackGold( money1, money2 ); need to add respective curency for thieves/murderers
		}

		/// <summary>
		/// Gets a random shoe hue with a small chance of being neutral.
		/// </summary>
		/// <returns>Shoe hue value</returns>
		public virtual int GetShoeHue()
		{
			if ( MidlandVendorConstants.SHOE_HUE_CHANCE > Utility.RandomDouble() )
				return 0;

			return Utility.RandomNeutralHue();
		}

		public virtual VendorShoeType ShoeType
		{
			get { return VendorShoeType.Shoes; }
		}

		public override bool OnBeforeDeath()
		{
			Mobile killer = this.LastKiller;

			if (killer is BaseCreature)
			{
				BaseCreature bc_killer = (BaseCreature)killer;
				if(bc_killer.Summoned)
				{
					if(bc_killer.SummonMaster != null)
						killer = bc_killer.SummonMaster;
				}
				else if(bc_killer.Controlled)
				{
					if(bc_killer.ControlMaster != null)
						killer=bc_killer.ControlMaster;
				}
				else if(bc_killer.BardProvoked)
				{
					if(bc_killer.BardMaster != null)
						killer=bc_killer.BardMaster;
				}
			}

			if ( killer is PlayerMobile )
			{
				killer.Criminal = true;
				killer.Kills = killer.Kills + 1;
				((PlayerMobile)killer).AdjustReputation( this );
			}

			string bSay = MidlandVendorStringConstants.MSG_DEATH_HELP;
			if (((BaseCreature)this).midrace == MidlandVendorConstants.RACE_HUMAN)
			{
				switch ( Utility.Random( 5 ))		   
				{
					case 0: bSay = MidlandVendorStringConstants.MSG_DEATH_GUARDS; break;
					case 1: bSay = MidlandVendorStringConstants.MSG_DEATH_NO_HIDING; break;
					case 2: bSay = MidlandVendorStringConstants.MSG_DEATH_NO; break;
					case 3: bSay = MidlandVendorStringConstants.MSG_DEATH_VILE_ROGUE; break;
					case 4: bSay = MidlandVendorStringConstants.MSG_DEATH_AARRGH; break;
				};
			}

			this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( bSay ) );

			if ( !base.OnBeforeDeath() )
				return false;

			return true;
		}

		/// <summary>
		/// Called after vendor spawns.
		/// Assigns race and dresses the vendor.
		/// </summary>
		public override void OnAfterSpawn()
		{
			IntelligentAction.AssignMidlandRace( (BaseCreature)this );
			IntelligentAction.MidlandRace((BaseCreature)this);
			Dress();
			base.OnAfterSpawn();	
		}
		
		#endregion
		
		#region Serialization

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		public MidlandVendor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version

			writer.Write( m_good1inventory);
			writer.Write( m_good1name);
			writer.Write( m_good1adjust);

			writer.Write( m_good2inventory);
			writer.Write( m_good2name);
			writer.Write( m_good2adjust);

			writer.Write( m_good3inventory);
			writer.Write( m_good3name);
			writer.Write( m_good3adjust);

			writer.Write( m_good4inventory);
			writer.Write( m_good4name);
			writer.Write( m_good4adjust);

			writer.Write(m_moneytype);


		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_good1inventory = reader.ReadInt();
			m_good1name = reader.ReadString();
			m_good1adjust = reader.ReadDouble();
		
			m_good2inventory = reader.ReadInt();
			m_good2name = reader.ReadString();
			m_good2adjust = reader.ReadDouble();

			m_good3inventory = reader.ReadInt();
			m_good3name = reader.ReadString();
			m_good3adjust = reader.ReadDouble();

			m_good4inventory = reader.ReadInt();
			m_good4name = reader.ReadString();
			m_good4adjust = reader.ReadDouble();

			m_moneytype = reader.ReadInt();

			m_OneTimeType = 3;

			AdjustPrice();
		}

		#endregion
	}
} 

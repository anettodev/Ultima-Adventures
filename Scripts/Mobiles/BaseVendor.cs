using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Mobiles;
using Server.Misc;
using Server.Engines.BulkOrders;
using Server.Regions;
using Server.Custom;
using Server.Multis;
using Server.Mobiles.Vendors;

namespace Server.Mobiles
{
	public enum VendorShoeType
	{
		None,
		Shoes,
		Boots,
		Sandals,
		ThighBoots
	}

	public abstract class BaseVendor : BaseConvo, IVendor
	{
		#region Make Vendors Talk
        private static bool m_Talked;
        string[] VendorSay = new string[] 
		{ 
			BaseVendorStringConstants.GREETING_1,
            BaseVendorStringConstants.GREETING_2,
            BaseVendorStringConstants.GREETING_3,
            BaseVendorStringConstants.GREETING_4,
            BaseVendorStringConstants.GREETING_5,
			BaseVendorStringConstants.GREETING_6,
			BaseVendorStringConstants.GREETING_7,
			BaseVendorStringConstants.GREETING_8,
			BaseVendorStringConstants.GREETING_9,
			BaseVendorStringConstants.GREETING_10,
			BaseVendorStringConstants.GREETING_11,
			BaseVendorStringConstants.GREETING_12,
			BaseVendorStringConstants.GREETING_13,
			BaseVendorStringConstants.GREETING_14,
			BaseVendorStringConstants.GREETING_15,
			BaseVendorStringConstants.GREETING_16
		};

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m_Talked == false && !(Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == BaseVendorStringConstants.REGION_DOOM_GAUNTLET) && !(Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == BaseVendorStringConstants.REGION_DOOM)  )
            {
                if (m.InRange(this, BaseVendorConstants.TALK_RANGE) && m is PlayerMobile && !m.Hidden && m.Alive)
                {
                    m_Talked = true;

					if (this is Carpenter && Map == Map.Trammel)
					{
						string sRegion = Worlds.GetMyRegion( Map, Location );
						if (sRegion == BaseVendorStringConstants.REGION_CITY_OF_BRITAIN)
						{
							if (Utility.RandomBool())
								Say(BaseVendorStringConstants.CARPENTER_1);
							else
								Say(BaseVendorStringConstants.CARPENTER_2);
						}
					}
					else
                    	SayRandom(VendorSay, this);
                    this.Move(GetDirectionTo(m.Location));
                    SpamTimer t = new SpamTimer();
                    t.Start();
                }
            }
        }

        private class SpamTimer : Timer
        {
            public SpamTimer()
                : base(TimeSpan.FromMinutes(BaseVendorConstants.SPAM_TIMER_MINUTES))
            {
                Priority = TimerPriority.OneMinute;
            }

            protected override void OnTick()
            {
                m_Talked = false;
            }
        }

        private static void SayRandom(string[] say, Mobile m)
        {
            m.Say(say[Utility.Random(say.Length)]);
        }
		
        #endregion

		#region Fields and Properties

		protected abstract List<SBInfo> SBInfos { get; }

		private ArrayList m_ArmorBuyInfo = new ArrayList();
		private ArrayList m_ArmorSellInfo = new ArrayList();

		private DateTime m_LastRestock;

		private bool m_pricesadjusted;

		private bool m_sellingpriceadjusted;

		public override bool CanTeach { get { return true; } }
		public override bool BardImmune { get { return false; } }

		public override bool PlayerRangeSensitive { get { return true; } }

		public virtual bool IsActiveVendor { get { return true; } }
		public virtual bool IsActiveBuyer { get { return IsActiveVendor; } }
		public virtual bool IsActiveSeller { get { return IsActiveVendor; } }

		public virtual NpcGuild NpcGuild { get { return NpcGuild.None; } }

		public virtual bool IsInvulnerable { get { return true; } }
		public override bool Unprovokable { get { return false; } }
		public override bool Uncalmable{ get{ return false; } }

		public override bool ShowFameTitle { get { return false; } }

		#endregion

		#region Bulk Order System

		protected override void GetConvoFragments(ArrayList list)
		{
			list.Add( (int)JobFragment.shopkeep );
			base.GetConvoFragments (list);
		}

		public virtual bool IsValidBulkOrder( Item item )
		{
			return false;
		}

		public virtual Item CreateBulkOrder( Mobile from, bool fromContextMenu )
		{
			return null;
		}

		public virtual bool SupportsBulkOrders( Mobile from )
		{
			return false;
		}

		public virtual TimeSpan GetNextBulkOrder( Mobile from )
		{
			return TimeSpan.FromMinutes(BaseVendorConstants.BULK_ORDER_DELAY_MINUTES);
		}

		public virtual void OnSuccessfulBulkOrderReceive( Mobile from )
		{
		}

		#endregion

		#region AI and Behavior

		public override void OnThink()
		{
			base.OnThink();

			if ( PlayersInSight() )
				this.CantWalk = true;
			else
				this.CantWalk = false;
		}

		private bool PlayersInSight()
		{
			int players = 0;
			bool enemies = false;

			foreach ( Mobile player in GetMobilesInRange( BaseVendorConstants.PLAYER_SIGHT_RANGE ) )
			{
				if (player != null)
				{
					if ( player is PlayerMobile )
					{
						if ( CanSee( player ) && InLOS( player ) ){ ++players; }
					}

					if ( IsEnemy( player ) )
					{
						enemies = true;
						
						if (player.Criminal && !player.Hidden && player is PlayerMobile)
						{
							this.Combatant = player;
							return false;
						}
					}
				}
			}

			if ( players > 0 && !enemies )
				return true;

			return false;
		}

		#region Faction
		public virtual int GetPriceScalar()
		{
			return BaseVendorConstants.DEFAULT_PRICE_SCALAR;
		}

		public void UpdateBuyInfo()
		{
			int priceScalar = GetPriceScalar();

			IBuyItemInfo[] buyinfo = (IBuyItemInfo[])m_ArmorBuyInfo.ToArray( typeof( IBuyItemInfo ) );

			if ( buyinfo != null )
			{
				foreach ( IBuyItemInfo info in buyinfo )
					info.PriceScalar = priceScalar;
			}
		}
		#endregion

		private class BulkOrderInfoEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private BaseVendor m_Vendor;

			public BulkOrderInfoEntry( Mobile from, BaseVendor vendor )
				: base( BaseVendorConstants.CONTEXT_MENU_BULK_ORDER )
			{
				m_From = from;
				m_Vendor = vendor;
			}

			public override void OnClick()
			{
				if ( m_Vendor.SupportsBulkOrders( m_From ) )
				{
					TimeSpan ts = m_Vendor.GetNextBulkOrder( m_From );

					int totalSeconds = (int)ts.TotalSeconds;
					int totalHours = ( totalSeconds + BaseVendorConstants.SECONDS_TO_HOURS_OFFSET ) / BaseVendorConstants.SECONDS_PER_HOUR;
					int totalMinutes = ( totalSeconds + BaseVendorConstants.SECONDS_TO_MINUTES_OFFSET ) / BaseVendorConstants.SECONDS_PER_MINUTE;

					if ( ( ( Core.SE ) ? totalMinutes == 0 : totalHours == 0 ) )
					{
						m_From.SendLocalizedMessage( 1049038 ); // You can get an order now.

						if ( Core.AOS )
						{
							Item bulkOrder = m_Vendor.CreateBulkOrder( m_From, true );

							if ( bulkOrder is LargeBOD )
								m_From.SendGump( new LargeBODAcceptGump( m_From, (LargeBOD)bulkOrder ) );
							else if ( bulkOrder is SmallBOD )
								m_From.SendGump( new SmallBODAcceptGump( m_From, (SmallBOD)bulkOrder ) );
						}
					}
					else
					{
						int oldSpeechHue = m_Vendor.SpeechHue;
						m_Vendor.SpeechHue = Server.Misc.RandomThings.GetSpeechHue();

							m_Vendor.SayTo( m_From, 1072058, totalMinutes.ToString() ); // An offer may be available in about ~1_minutes~ minutes.

						m_Vendor.SpeechHue = oldSpeechHue;
					}
				}
			}
		}

		#endregion

		#region Constructors

		public BaseVendor( string title ) : base( AIType.AI_Vendor, FightMode.Closest, 15, 1, 0.1, 0.2 )
		{
			LoadSBInfo();

			this.Title = title;

			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();

			InitBody();
			InitOutfit();

			Container pack;
			pack = new Backpack();
			pack.Layer = Layer.ShopBuy;
			pack.Movable = false;
			pack.Visible = false;
			AddItem( pack );

			pack = new Backpack();
			pack.Layer = Layer.ShopResale;
			pack.Movable = false;
			pack.Visible = false;
			AddItem( pack );

			m_LastRestock = DateTime.UtcNow;
			LoadSBInfo();

				SetStr(BaseVendorConstants.STR_MIN, BaseVendorConstants.STR_MAX);
				SetDex(BaseVendorConstants.DEX_MIN, BaseVendorConstants.DEX_MAX);
				SetInt(BaseVendorConstants.INT_MIN, BaseVendorConstants.INT_MAX);

				SetHits(BaseVendorConstants.HITS_MIN, BaseVendorConstants.HITS_MAX);

				SetDamage(BaseVendorConstants.DAMAGE_MIN, BaseVendorConstants.DAMAGE_MAX);

				SetDamageType(ResistanceType.Physical, BaseVendorConstants.DAMAGE_TYPE_PHYSICAL_PERCENT);

				SetResistance(ResistanceType.Physical, BaseVendorConstants.RESISTANCE_MIN, BaseVendorConstants.RESISTANCE_MAX);
				SetResistance(ResistanceType.Fire, BaseVendorConstants.RESISTANCE_MIN, BaseVendorConstants.RESISTANCE_MAX);
				SetResistance(ResistanceType.Cold, BaseVendorConstants.RESISTANCE_MIN, BaseVendorConstants.RESISTANCE_MAX);
				SetResistance(ResistanceType.Poison, BaseVendorConstants.RESISTANCE_MIN, BaseVendorConstants.RESISTANCE_MAX);
				SetResistance(ResistanceType.Energy, BaseVendorConstants.RESISTANCE_MIN, BaseVendorConstants.RESISTANCE_MAX);

				SetSkill(SkillName.Swords, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.Fencing, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.Macing, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.Tactics, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);				
				SetSkill(SkillName.Tactics, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.MagicResist, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.Tactics, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.Parry, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.Anatomy, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.Healing, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.Magery, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.EvalInt, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.DetectHidden, BaseVendorConstants.DETECT_HIDDEN_MIN, BaseVendorConstants.SKILL_MAX);

				m_pricesadjusted = false;
				m_sellingpriceadjusted = false;
		}

		public BaseVendor( Serial serial ): base( serial )
		{
		}

		#endregion

		#region Static Helper Methods

		public static int BeggingPose( Mobile from )
		{
			int beggar = 0;
			if ( from is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );

				if ( DB.CharacterBegging > 0 )
				{
					beggar = (int)from.Skills[SkillName.Begging].Value;
				}
			}
			return beggar;
		}
		
		public static int BeggingKarma( Mobile from )
		{
			int charisma = 0;
			if ( from.Karma > BaseVendorConstants.BEGGING_KARMA_THRESHOLD ){ charisma = BaseVendorConstants.BEGGING_CHARISMA_BONUS; }
			return charisma;
		}

		public static string BeggingWords()
		{
			string sSpeak = BaseVendorStringConstants.BEGGING_1;
			switch( Utility.RandomMinMax( BaseVendorConstants.BEGGING_WORDS_MIN, BaseVendorConstants.BEGGING_WORDS_MAX ) )
			{
				case 0: sSpeak = BaseVendorStringConstants.BEGGING_1; break;
				case 1: sSpeak = BaseVendorStringConstants.BEGGING_2; break;
				case 2: sSpeak = BaseVendorStringConstants.BEGGING_3; break;
				case 3: sSpeak = BaseVendorStringConstants.BEGGING_4; break;
				case 4: sSpeak = BaseVendorStringConstants.BEGGING_5; break;
				case 5: sSpeak = BaseVendorStringConstants.BEGGING_6; break;
			}
			return sSpeak;
		}

		#endregion

		#region Properties

		public DateTime LastRestock
		{
			get
			{
				return m_LastRestock;
			}
			set
			{
				m_LastRestock = value;
			}
		}

		public virtual TimeSpan RestockDelay
		{
			get
			{
				return TimeSpan.FromHours( BaseVendorConstants.RESTOCK_DELAY_HOURS );
			}
		}

		public virtual TimeSpan RestockDelayFull
		{
			get
			{
				return TimeSpan.FromHours( BaseVendorConstants.RESTOCK_DELAY_HOURS );
			}
		}

		public Container BuyPack
		{
			get
			{
				Container pack = FindItemOnLayer( Layer.ShopBuy ) as Container;

				if ( pack == null )
				{
					pack = new Backpack();
					pack.Layer = Layer.ShopBuy;
					pack.Visible = false;
					AddItem( pack );
				}

				return pack;
			}
		}

		public abstract void InitSBInfo();

		protected void LoadSBInfo()
		{
			m_LastRestock = DateTime.UtcNow;

			for ( int i = 0; i < m_ArmorBuyInfo.Count; ++i )
			{
				GenericBuyInfo buy = m_ArmorBuyInfo[i] as GenericBuyInfo;

				if ( buy != null )
					buy.DeleteDisplayEntity();
			}

			SBInfos.Clear();

			InitSBInfo();

			m_ArmorBuyInfo.Clear();
			m_ArmorSellInfo.Clear();

			for ( int i = 0; i < SBInfos.Count; i++ )
			{
				SBInfo sbInfo = (SBInfo)SBInfos[i];
				m_ArmorBuyInfo.AddRange( sbInfo.BuyInfo ); //+++ Can change price here
				m_ArmorSellInfo.Add( sbInfo.SellInfo );
			}
		}

		public virtual bool GetGender()
		{
			return Utility.RandomBool();
		}

		public virtual void InitBody()
		{
			InitStats( BaseVendorConstants.INIT_STATS_STR, BaseVendorConstants.INIT_STATS_DEX, BaseVendorConstants.INIT_STATS_INT );

			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();

			if ( IsInvulnerable && !Core.AOS )
				NameHue = BaseVendorConstants.INVULNERABLE_NAME_HUE;

			if ( this is Roscoe || this is Garth )
			{
				Body = BaseVendorConstants.BODY_MALE;
				Name = NameList.RandomName( "male" );
			}
			else if ( this is KungFu )
			{
				if ( Female = GetGender() )
				{
					Body = BaseVendorConstants.BODY_FEMALE;
					Name = NameList.RandomName( "tokuno female" );
				}
				else
				{
					Body = BaseVendorConstants.BODY_MALE;
					Name = NameList.RandomName( "tokuno male" );
				}
			}
			else if ( Female = GetGender() )
			{
				Body = BaseVendorConstants.BODY_FEMALE;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = BaseVendorConstants.BODY_MALE;
				Name = NameList.RandomName( "male" );
			}
		}

		public virtual int GetRandomHue()
		{
			switch ( Utility.Random( BaseVendorConstants.RANDOM_HUE_COUNT ) )
			{
				default:
				case 0: return Utility.RandomBlueHue();
				case 1: return Utility.RandomGreenHue();
				case 2: return Utility.RandomRedHue();
				case 3: return Utility.RandomYellowHue();
				case 4: return Utility.RandomNeutralHue();
			}
		}

		public virtual int GetShoeHue()
		{
			if ( BaseVendorConstants.RANDOM_THRESHOLD > Utility.RandomDouble() )
				return 0;

			return Utility.RandomNeutralHue();
		}

		public virtual VendorShoeType ShoeType
		{
			get { return VendorShoeType.Shoes; }
		}

		public virtual int RandomBrightHue()
		{
			if ( BaseVendorConstants.RANDOM_THRESHOLD > Utility.RandomDouble() )
				return Utility.RandomList( BaseVendorConstants.BRIGHT_HUE_SPECIAL_1, BaseVendorConstants.BRIGHT_HUE_SPECIAL_2 );

			return Utility.RandomList( 0x03, 0x0D, 0x13, 0x1C, 0x21, 0x30, 0x37, 0x3A, 0x44, 0x59 );
		}

		public override void OnAfterSpawn()
		{
			if (this.Home.X == 0 && this.Home.Y == 0 && this.Home.Z == 0)
			    this.Home = this.Location;

			Region reg = Region.Find( this.Location, this.Map );

			if ( !Server.Items.EssenceBase.ColorCitizen( this ) )
			{
				Server.Misc.MorphingTime.CheckMorph( this );
			}

			Server.Mobiles.PremiumSpawner.SpreadOut( this );

			if (AdventuresFunctions.IsInMidland((object)this))
			{
				if ( reg.Name == BaseVendorStringConstants.REGION_MIDKEMIA  )
				{
					((BaseCreature)this).midrace = 1;
				}
				if ( reg.Name == BaseVendorStringConstants.REGION_CALYPSO  )
				{
					((BaseCreature)this).midrace = 4;	
				}	
				IntelligentAction.MidlandRace(this);			
			}

			if ( ( reg.Name == BaseVendorStringConstants.REGION_DUNGEON_ROOM || reg.Name == BaseVendorStringConstants.REGION_CAMPING_TENT ) && this is Provisioner )
			{
				this.Title = BaseVendorStringConstants.TITLE_MERCHANT;
			}
			else if ( reg.Name == BaseVendorStringConstants.REGION_FORGOTTEN_LIGHTHOUSE || reg.Name == BaseVendorStringConstants.REGION_SAVAGE_SEA_DOCKS || reg.Name == BaseVendorStringConstants.REGION_SERPENT_SAIL_DOCKS || reg.Name == BaseVendorStringConstants.REGION_ANCHOR_ROCK_DOCKS || reg.Name == BaseVendorStringConstants.REGION_KRAKEN_REEF_DOCKS || reg.Name == BaseVendorStringConstants.REGION_THE_PORT )
			{
				if ( this is Provisioner && reg.Name != BaseVendorStringConstants.REGION_THE_PORT ){ this.Title = BaseVendorStringConstants.TITLE_DOCK_WORKER; if ( Utility.RandomBool() ){ this.Title = BaseVendorStringConstants.TITLE_MERCHANT; } }
				else if ( this is Fisherman && reg.Name != BaseVendorStringConstants.REGION_THE_PORT ){ this.Title = BaseVendorStringConstants.TITLE_SAILOR; }
				else if ( this is Carpenter && reg.Name != BaseVendorStringConstants.REGION_THE_PORT ){ this.Title = BaseVendorStringConstants.TITLE_COOPER; }
				else if ( this is Waiter ){ this.Title = BaseVendorStringConstants.TITLE_CABIN_BOY; if ( this.Female ){ this.Title = BaseVendorStringConstants.TITLE_SERVING_WENCH; } }
				else if ( this is Weaponsmith && reg.Name != BaseVendorStringConstants.REGION_THE_PORT ){ this.Title = BaseVendorStringConstants.TITLE_MASTER_AT_ARMS; }
				else if ( this is Ranger )
				{
					this.Title = BaseVendorStringConstants.TITLE_HARPOONER;
					if ( this.FindItemOnLayer( Layer.OneHanded ) != null ) { this.FindItemOnLayer( Layer.OneHanded ).Delete(); }
					if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ) { this.FindItemOnLayer( Layer.TwoHanded ).Delete(); }
					if ( this.FindItemOnLayer( Layer.Helm ) != null ) { this.FindItemOnLayer( Layer.Helm ).Delete(); }
					if ( this.FindItemOnLayer( Layer.Helm ) != null ) { this.FindItemOnLayer( Layer.Helm ).Delete(); }
					if ( Utility.RandomBool() ){ this.AddItem( new SkullCap( Utility.RandomDyedHue() ) ); }
					this.AddItem( new Harpoon() );
				}
				else if ( this is Shipwright && reg.Name != BaseVendorStringConstants.REGION_THE_PORT ){ this.Title = BaseVendorStringConstants.TITLE_BOATSWAIN; }

				if ( !(this is Shipwright && reg.Name == BaseVendorStringConstants.REGION_THE_PORT ) )
				{
					if ( !(this is Jester) && !(this is Druid) && !(this is VarietyDealer) )
					{
						if ( this.FindItemOnLayer( Layer.Helm ) != null ) { this.FindItemOnLayer( Layer.Helm ).Delete(); }
						if ( Utility.RandomBool() ){ this.AddItem( new SkullCap() ); }
						else { this.AddItem( new Bandana() ); }
						MorphingTime.SailorMyClothes( this );
					}
				}
			}
			else if ( reg.Name == BaseVendorStringConstants.REGION_THIEVES_GUILD && this is Provisioner )
			{
				this.Title = BaseVendorStringConstants.TITLE_FENCE;
			}
			else if ( reg.Name == BaseVendorStringConstants.REGION_SHIP_LOWER_DECK && !(this is Jester) )
			{
				if ( this is Provisioner  ){ this.Title = BaseVendorStringConstants.TITLE_QUARTERMASTER; }
				else if ( this is Waiter ){ this.Title = BaseVendorStringConstants.TITLE_CABIN_BOY; if ( this.Female ){ this.Title = BaseVendorStringConstants.TITLE_SERVING_WENCH; } }
				if ( this.FindItemOnLayer( Layer.Helm ) != null ) { this.FindItemOnLayer( Layer.Helm ).Delete(); }
				if ( Utility.RandomBool() ){ this.AddItem( new SkullCap() ); }
				else { this.AddItem( new Bandana() ); }
				MorphingTime.SailorMyClothes( this );
			}
			else if ( reg.Name == BaseVendorStringConstants.REGION_WIZARDS_GUILD && this is Waiter && this.Body == BaseVendorConstants.BODY_WAITER_MALE )
			{
				this.Title = BaseVendorStringConstants.TITLE_BUTLER;
			}
			else if ( reg.Name == BaseVendorStringConstants.REGION_WIZARDS_GUILD && this is Waiter && this.Body == BaseVendorConstants.BODY_WAITER_FEMALE )
			{
				this.Title = BaseVendorStringConstants.TITLE_MAID;
			}
			else if ( reg.Name == BaseVendorStringConstants.REGION_THE_PIT )
			{
				this.Kills = Utility.RandomMinMax(BaseVendorConstants.KILLS_PIT_MIN, BaseVendorConstants.KILLS_PIT_MAX);
				this.Karma = BaseVendorConstants.KARMA_PIT;
			}		
			else if ( reg.Name == BaseVendorStringConstants.REGION_RUINS_OF_TENEBRAE)
			{ 
				this.Hue = Utility.RandomMinMax( BaseVendorConstants.HUE_RUINS_MIN, BaseVendorConstants.HUE_RUINS_MAX );
				if (this.Female)
					this.Body = BaseVendorConstants.BODY_RUINS_FEMALE;
				else
					this.Body = BaseVendorConstants.BODY_RUINS_MALE;
				
				if ( this.FindItemOnLayer( Layer.Shirt ) != null ) { this.FindItemOnLayer( Layer.Shirt ).Delete(); }
				if ( this.FindItemOnLayer( Layer.InnerTorso ) != null ) { this.FindItemOnLayer( Layer.InnerTorso ).Delete(); }
			}
			else if ( reg.Name == BaseVendorStringConstants.REGION_TEMPLE_OF_PRAETORIA)
			{ 
				this.Hue = BaseVendorConstants.HUE_TEMPLE_PRAETORIA ;
				if (this.Female)
					this.Body = BaseVendorConstants.BODY_RUINS_FEMALE;
				else
					this.Body = BaseVendorConstants.BODY_RUINS_MALE;
				this.Karma = BaseVendorConstants.KARMA_TEMPLE;
				
				if ( this.FindItemOnLayer( Layer.Shirt ) != null ) { this.FindItemOnLayer( Layer.Shirt ).Delete(); }
				if ( this.FindItemOnLayer( Layer.InnerTorso ) != null ) { this.FindItemOnLayer( Layer.InnerTorso ).Delete(); }
			}
			
			base.OnAfterSpawn();
		}

		#endregion

		#region Map and Region Handling

		protected override void OnMapChange( Map oldMap )
		{
			base.OnMapChange( oldMap );

			if ( !Server.Items.EssenceBase.ColorCitizen( this ) )
			{
				Server.Misc.MorphingTime.CheckMorph( this );
			}

			LoadSBInfo();
		}

		public virtual int GetHairHue()
		{
			return Utility.RandomHairHue();
		}

		public virtual void InitOutfit()
		{
			switch ( Utility.Random( BaseVendorConstants.RANDOM_SHIRT_COUNT ) )
			{
				case 0: AddItem( new FancyShirt( GetRandomHue() ) ); break;
				case 1: AddItem( new Doublet( GetRandomHue() ) ); break;
				case 2: AddItem( new Shirt( GetRandomHue() ) ); break;
			}

			switch ( ShoeType )
			{
				case VendorShoeType.Shoes: AddItem( new Shoes( GetShoeHue() ) ); break;
				case VendorShoeType.Boots: AddItem( new Boots( GetShoeHue() ) ); break;
				case VendorShoeType.Sandals: AddItem( new Sandals( GetShoeHue() ) ); break;
				case VendorShoeType.ThighBoots: AddItem( new ThighBoots( GetShoeHue() ) ); break;
			}

			int hairHue = GetHairHue();

			Utility.AssignRandomHair( this, hairHue );
			Utility.AssignRandomFacialHair( this, hairHue );

			if ( Female )
			{
				switch ( Utility.Random( BaseVendorConstants.RANDOM_FEMALE_PANTS_COUNT ) )
				{
					case 0: AddItem( new ShortPants( GetRandomHue() ) ); break;
					case 1:
					case 2: AddItem( new Kilt( GetRandomHue() ) ); break;
					case 3:
					case 4:
					case 5: AddItem( new Skirt( GetRandomHue() ) ); break;
				}
			}
			else
			{
				FacialHairItemID = Utility.RandomList( 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
				FacialHairHue = hairHue;

				switch ( Utility.Random( BaseVendorConstants.RANDOM_MALE_PANTS_COUNT ) )
				{
					case 0: AddItem( new LongPants( GetRandomHue() ) ); break;
					case 1: AddItem( new ShortPants( GetRandomHue() ) ); break;
				}
			}

				VendorGoldHelper.PackGoldForVendor(this);
		}

		public virtual void Restock()
		{
			m_LastRestock = DateTime.UtcNow;

			LoadSBInfo();

			Container cont = this.Backpack;
			if ( cont != null )
			{
				Gold m_Gold = (Gold)this.Backpack.FindItemByType( typeof( Gold ) );
				int m_Amount = this.Backpack.GetAmount( typeof( Gold ) );
				cont.ConsumeTotal( typeof( Gold ), m_Amount );

					VendorGoldHelper.PackGoldForVendor(this);
			}
		}

		#endregion

		#region Combat and Death

		public override bool OnBeforeDeath()
		{
			Server.Misc.MorphingTime.TurnToSomethingOnDeath( this );

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
				Region reg = Region.Find( this.Location, this.Map );
				if (reg.Name == BaseVendorStringConstants.REGION_THE_PIT) // no one gets kills from vendors in the pit
				{
				}
				else if ( this.Map == Map.Ilshenar && this.X <= BaseVendorConstants.DARKMOOR_X_MAX && this.Y <= BaseVendorConstants.DARKMOOR_Y_MAX) // darkmoor
				{
					if (this.Karma < 0 ) // evil killing evil vendors in darkmoor
					{
						killer.Criminal = true;
						killer.Kills = killer.Kills + 1;
						Titles.AwardKarma( killer, BaseVendorConstants.KARMA_AWARD_VENDOR_KILL, true );
						Server.Items.DisguiseTimers.RemoveDisguise( killer );
					}
					
				}
				else
				{
					killer.Criminal = true;
					killer.Kills = killer.Kills + 1;
					Titles.AwardKarma( killer, -(BaseVendorConstants.KARMA_AWARD_VENDOR_KILL), true );
					Server.Items.DisguiseTimers.RemoveDisguise( killer );
				}


			}

			if ( !base.OnBeforeDeath() )
				return false;

			string bSay = BaseVendorStringConstants.DEATH_CRY;
			this.PublicOverheadMessage( MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEATH, false, string.Format ( bSay ) ); 

			return true;
		}

		#endregion

		#region Enemy Detection

		public override bool IsEnemy( Mobile m )
		{
			if (m.Criminal);
				return true;

			if ( IntelligentAction.GetMyEnemies( m, this, true ) == false )
				return false;

			if ( m.Region != this.Region && !(m is PlayerMobile) )
				return false;

		}

		#endregion

		#region Combat Messages

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			switch ( Utility.Random( BaseVendorConstants.RANDOM_COMBAT_MESSAGE_COUNT ))		   
			{
				case 0: Say(BaseVendorStringConstants.COMBAT_1); break;
				case 1: Say(string.Format(BaseVendorStringConstants.COMBAT_2_FORMAT, defender.Name)); break;
				case 2: Say(string.Format(BaseVendorStringConstants.COMBAT_3_FORMAT, defender.Name)); break;
				case 3: Say(string.Format(BaseVendorStringConstants.COMBAT_4_FORMAT, defender.Name)); break;
			};
		}

		#endregion

		#region Price Calculation

		private static TimeSpan InventoryDecayTime = TimeSpan.FromHours( BaseVendorConstants.INVENTORY_DECAY_HOURS );

		public int AdjustPrices( int money, bool good)
		{

			int curse = AetherGlobe.VendorCurse;

			if (!good) // bad vendor, invert measurement
				curse = BaseVendorConstants.CURSE_THRESHOLD_MAX - curse;

			double pricemod = BaseVendorConstants.MIN_PRICE_MOD;
			
			if (curse < BaseVendorConstants.CURSE_THRESHOLD_1)
			{
				pricemod = ( BaseVendorConstants.PRICE_MOD_1 * (double)money );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_2)
			{
				pricemod = ( BaseVendorConstants.PRICE_MOD_2 * (double)money );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_3)
			{
				pricemod = ( BaseVendorConstants.PRICE_MOD_3 * (double)money );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_4)
			{
				pricemod = ( BaseVendorConstants.PRICE_MOD_4 * (double)money );
			}		
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_5)
			{
				pricemod = ( BaseVendorConstants.PRICE_MOD_5 * (double)money );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_6)
			{
				pricemod = ( BaseVendorConstants.PRICE_MOD_6 * (double)money );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_7)
			{
				pricemod = ( BaseVendorConstants.PRICE_MOD_7 * (double)money );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_8)
			{
				pricemod = ( BaseVendorConstants.PRICE_MOD_8 * (double)money );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_9)
			{
				pricemod = ( BaseVendorConstants.PRICE_MOD_9 * (double)money );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_MAX)
			{
				pricemod = ( BaseVendorConstants.PRICE_MOD_10 * (double)money );
			}				
				
			if ( pricemod < BaseVendorConstants.MIN_PRICE_MOD )
				pricemod = BaseVendorConstants.MIN_PRICE_MOD;

			return Convert.ToInt32(pricemod);
			
		}

		public static int AdjustAmount( int amount, bool good)
		{

			int curse = AetherGlobe.VendorCurse;

			if (!good) // bad vendor, invert
				curse = BaseVendorConstants.CURSE_THRESHOLD_MAX - curse;

			double pricemod = BaseVendorConstants.MIN_PRICE_MOD;
			
			if (curse < BaseVendorConstants.CURSE_THRESHOLD_1)
			{
				pricemod = ( BaseVendorConstants.AMOUNT_MOD_1 * (double)amount );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_2)
			{
				pricemod = ( BaseVendorConstants.AMOUNT_MOD_2 * (double)amount );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_3)
			{
				pricemod = ( BaseVendorConstants.AMOUNT_MOD_3 * (double)amount );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_4)
			{
				pricemod = ( BaseVendorConstants.AMOUNT_MOD_4 * (double)amount );
			}		
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_5)
			{
				pricemod = ( BaseVendorConstants.AMOUNT_MOD_5 * (double)amount );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_6)
			{
				pricemod = ( BaseVendorConstants.AMOUNT_MOD_6 * (double)amount );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_7)
			{
				pricemod = ( BaseVendorConstants.AMOUNT_MOD_7 * (double)amount );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_8)
			{
				pricemod = ( BaseVendorConstants.AMOUNT_MOD_8 * (double)amount );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_9)
			{
				pricemod = ( BaseVendorConstants.AMOUNT_MOD_9 * (double)amount );
			}	
			else if (curse < BaseVendorConstants.CURSE_THRESHOLD_MAX)
			{
				pricemod = ( BaseVendorConstants.AMOUNT_MOD_10 * (double)amount );
			}				
				
			if ( pricemod < BaseVendorConstants.MIN_PRICE_MOD )
				pricemod = BaseVendorConstants.MIN_PRICE_MOD;

			return Convert.ToInt32(pricemod);
		}

		#endregion

		#region Vendor Buy/Sell

		public virtual void VendorBuy( Mobile from )
		{
			if ( !IsActiveSeller )
				return;

			if ( !from.CheckAlive() )
				return;

			if ( !CheckVendorAccess( from ) )
			{
				this.Say(BaseVendorStringConstants.ERROR_NO_BUSINESS);
				return;
			}

			if (AdventuresFunctions.IsInMidland((object)this))
			{
				this.Say(BaseVendorStringConstants.ERROR_MIDLAND_NO_TRADE);
				return;
			}	

			if (	DateTime.UtcNow - m_LastRestock > RestockDelay || 
					( from.Region.IsPartOf( typeof( PublicRegion ) ) && DateTime.UtcNow - m_LastRestock > RestockDelayFull ) || 
					( this is BaseGuildmaster && DateTime.UtcNow - m_LastRestock > RestockDelayFull ) 
			)
			
			Restock();

			UpdateBuyInfo();

			int count = 0;
			List<BuyItemState> list; //+++
			IBuyItemInfo[] buyInfo = this.GetBuyInfo();
			IShopSellInfo[] sellInfo = this.GetSellInfo();

            list = new List<BuyItemState>( buyInfo.Length );
			Container cont = this.BuyPack;

			List<ObjectPropertyList> opls = null;

			for ( int idx = 0; idx < buyInfo.Length; idx++ )
			{
				IBuyItemInfo buyItem = (IBuyItemInfo)buyInfo[idx];

				if ( buyItem.Amount <= 0 || list.Count >= BaseVendorConstants.MAX_BUY_LIST_ITEMS )
					continue;

				// NOTE: Only GBI supported; if you use another implementation of IBuyItemInfo, this will crash
				GenericBuyInfo gbi = (GenericBuyInfo)buyItem;
				IEntity disp = gbi.GetDisplayEntity();

				int money = buyItem.Price;
				int amount = buyItem.Amount;

				if (!m_pricesadjusted)
				{
					// Use centralized price manager for buy prices
					money = VendorPriceManager.CalculateBuyPrice(money, this, from, null);
					buyItem.Price = money;

					// Use centralized price manager for stock amounts (if Order/Chaos enabled)
					if (VendorConstants.ENABLE_ORDER_CHAOS_PRICING)
					{
						bool isGoodVendor = this.Karma > 0 || (this.Karma == 0 && from != null && from.Karma >= 0);
						amount = VendorPriceManager.CalculateStockAmount(amount, isGoodVendor);
					}
					
					buyItem.Amount = amount;

					m_pricesadjusted = true;
				}

				list.Add( new BuyItemState( buyItem.Name, cont.Serial, disp == null ? (Serial)BaseVendorConstants.NULL_SERIAL : disp.Serial, buyItem.Price, buyItem.Amount, buyItem.ItemID, buyItem.Hue ) );
				count++;

				if ( opls == null ) {
					opls = new List<ObjectPropertyList>();
				}

				if ( disp is Item ) {
					opls.Add( ( ( Item ) disp ).PropertyList );
				} else if ( disp is Mobile ) {
					opls.Add( ( ( Mobile ) disp ).PropertyList );
				}
			}

			List<Item> playerItems = cont.Items;

			for ( int i = playerItems.Count - 1; i >= 0; --i )
			{
				if ( i >= playerItems.Count )
					continue;

				Item item = playerItems[i];

				if ( ( item.LastMoved + InventoryDecayTime ) <= DateTime.UtcNow )
					item.Delete();
			}

			for ( int i = 0; i < playerItems.Count; ++i )
			{
				Item item = playerItems[i];

				int price = 0;
				string name = null;

				foreach ( IShopSellInfo ssi in sellInfo )
				{
					if ( ssi.IsSellable( item ) )
					{
						price = ssi.GetBuyPriceFor( item );
						name = ssi.GetNameFor( item );
						break;
					}
				}

				if ( name != null && list.Count < BaseVendorConstants.MAX_BUY_LIST_ITEMS )
				{
					list.Add( new BuyItemState( name, cont.Serial, item.Serial, price, item.Amount, item.ItemID, item.Hue ) );
					count++;

					if ( opls == null ) {
						opls = new List<ObjectPropertyList>();
					}

					opls.Add( item.PropertyList );
				}
			}


			if ( list.Count > 0 )
			{
				list.Sort( new BuyItemStateComparer() );

				SendPacksTo( from );

				NetState ns = from.NetState;

				if ( ns == null )
					return;

				if ( ns.ContainerGridLines )
					from.Send( new VendorBuyContent6017( list ) );
				else
					from.Send( new VendorBuyContent( list ) );

				from.Send( new VendorBuyList( this, list ) );

				if ( ns.HighSeas )
					from.Send( new DisplayBuyListHS( this ) );
				else
					from.Send( new DisplayBuyList( this ) );

				from.Send( new MobileStatusExtended( from ) );//make sure their gold amount is sent

				if ( opls != null ) {
					for ( int i = 0; i < opls.Count; ++i ) {
						from.Send( opls[i] );
					}
				}

				SayTo( from, 500186 ); // Greetings.  Have a look around.
			}
		}

		#endregion

		#region Pack Management

		public virtual void SendPacksTo( Mobile from )
		{
			Item pack = FindItemOnLayer( Layer.ShopBuy );

			if ( pack == null )
			{
				pack = new Backpack();
				pack.Layer = Layer.ShopBuy;
				pack.Movable = false;
				pack.Visible = false;
				AddItem( pack );
			}

			from.Send( new EquipUpdate( pack ) );

			pack = FindItemOnLayer( Layer.ShopSell );

			if ( pack != null )
				from.Send( new EquipUpdate( pack ) );

			pack = FindItemOnLayer( Layer.ShopResale );

			if ( pack == null )
			{
				pack = new Backpack();
				pack.Layer = Layer.ShopResale;
				pack.Movable = false;
				pack.Visible = false;
				AddItem( pack );
			}

			from.Send( new EquipUpdate( pack ) );
		}

		public virtual void VendorSell( Mobile from )
		{
			if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING
			{
				from.Say( BeggingWords() );
			}

			if ( !IsActiveBuyer )
				return;

			if ( !from.CheckAlive() )
				return;

			if ( !CheckVendorAccess( from ) )
			{
				this.Say( BaseVendorStringConstants.ERROR_NO_BUSINESS );
				return;
			}

			if (AdventuresFunctions.IsInMidland((object)this))
			{
				this.Say( BaseVendorStringConstants.ERROR_MIDLAND_NO_TRADE );
				return;
			}				

			Container pack = from.Backpack;

			if ( pack != null )
			{
				IShopSellInfo[] info = GetSellInfo();

				Dictionary<Item, SellItemState> table = new Dictionary<Item, SellItemState>();

				foreach ( IShopSellInfo ssi in info )
				{
					Item[] items = pack.FindItemsByType( ssi.Types );

					foreach ( Item item in items )
					{
						LockableContainer parentcon = item.ParentEntity as LockableContainer;

						if ( item is Container && ( (Container)item ).Items.Count != 0 )
							continue;

						if ( parentcon != null && parentcon.Locked == true )
							continue;

						if ( item.IsStandardLoot() && item.Movable && ssi.IsSellable( item ) )
						{
							int GuildMember;
							int barter = VendorBarterHelper.CalculateBarterSkill(this, from, out GuildMember);

							int money = ssi.GetSellPriceFor( item, barter );

							if (!m_sellingpriceadjusted)
							{
								// Use centralized price manager for sell prices
								money = VendorPriceManager.CalculateSellPrice(money, item, this, from, barter);

								m_sellingpriceadjusted = true;
							}

							table[item] = new SellItemState( item, money, ssi.GetNameFor( item ) ); // +++ getsellprice needs to be changed
						}
					}
				}

				if ( table.Count > 0 )
				{
					SendPacksTo( from );

					from.Send(new VendorSellList(this, table.Values));
				}
				else
				{
                    string sSay = BaseVendorStringConstants.ERROR_NO_INTEREST;
                    this.PublicOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sSay);
				}
			}
		}

		#endregion

		#region Item Drop Handling

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( from.Blessed )
			{
				string sSay = BaseVendorStringConstants.ERROR_BLESSED;
				this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sSay, from.NetState);
				return false;
			}
			else if ( IntelligentAction.GetMyEnemies( from, this, false ) == true )
			{
				string sSay = BaseVendorStringConstants.ERROR_ENEMY;
				this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sSay, from.NetState);
				return false;
			}
			else if (from is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)from;

				int RelicValue = 0;

				if ( Server.Misc.RelicItems.RelicValue( dropped, this ) > 0 )
				{
					RelicValue = Server.Misc.RelicItems.RelicValue( dropped, this );
				}
				else if ( dropped is GolemManual && ( this is Tinker || this is TinkerGuildmaster ) )
				{
					Server.Items.GolemManual.ProcessGolemBook( from, this, dropped );
				}
				else if ( dropped is OrbOfTheAbyss && ( this is Tinker || this is TinkerGuildmaster ) )
				{
					Server.Items.OrbOfTheAbyss.ChangeOrb( from, this, dropped );
				}
				else if ( dropped is RobotSchematics && ( this is Tinker || this is TinkerGuildmaster ) )
				{
					Server.Items.RobotSchematics.ProcessRobotBook( from, this, dropped );
				}
				else if ( dropped is AlienEgg && ( this is AnimalTrainer || this is Veterinarian ) )
				{
					Server.Items.AlienEgg.ProcessAlienEgg( from, this, dropped );
				}
                else if (dropped is EarthyEgg && (this is AnimalTrainer || this is Veterinarian))
                {
                    Server.Items.EarthyEgg.ProcessEarthyEgg(from, this, dropped);
                }
                else if (dropped is CorruptedEgg && (this is AnimalTrainer || this is Veterinarian))
                {
                    Server.Items.CorruptedEgg.ProcessCorruptedEgg(from, this, dropped);
                }
                else if (dropped is FeyEgg && (this is AnimalTrainer || this is Veterinarian))
                {
                    Server.Items.FeyEgg.ProcessFeyEgg(from, this, dropped);
                }
                else if (dropped is PrehistoricEgg && (this is AnimalTrainer || this is Veterinarian))
                {
                    Server.Items.PrehistoricEgg.ProcessPrehistoricEgg(from, this, dropped);
                }
                else if (dropped is ReptilianEgg && (this is AnimalTrainer || this is Veterinarian))
                {
                    Server.Items.ReptilianEgg.ProcessReptilianEgg(from, this, dropped);
                }
                else if ( dropped is DragonEgg && ( this is AnimalTrainer || this is Veterinarian ) )
				{
					Server.Items.DragonEgg.ProcessDragonEgg( from, this, dropped );
				}
				else if ( dropped is DracolichSkull && ( this is NecromancerGuildmaster ) )
				{
					Server.Items.DracolichSkull.ProcessDracolichSkull( from, this, dropped );
				}
				else if ( dropped is DemonPrison && ( this is NecromancerGuildmaster || this is MageGuildmaster || this is Mage || this is NecroMage || this is Necromancer || this is Witches ) )
				{
					Server.Items.DemonPrison.ProcessDemonPrison( from, this, dropped );
				}
				else if (this is Waiter && dropped is CorpseItem)
				{
					string corpsename = dropped.Name;
					if (Insensitive.Contains(corpsename, BaseVendorStringConstants.ITEM_TYPE_COFFEE) || Insensitive.Contains(corpsename, BaseVendorStringConstants.ITEM_TYPE_ESPRESSO) || Insensitive.Contains(corpsename, BaseVendorStringConstants.ITEM_TYPE_COFFEE) || Insensitive.Contains(corpsename, BaseVendorStringConstants.ITEM_TYPE_AMERICANO) || Insensitive.Contains(corpsename, BaseVendorStringConstants.ITEM_TYPE_CAPPUCINO))
					{
						dropped.Delete();
						Coffee cof = new Coffee();
						from.Backpack.DropItem(cof);
						this.Say(BaseVendorStringConstants.COFFEE_ACCEPTANCE);
						return true;
					}	
				}

				if ( RelicValue > 0 )
				{
					int GuildMember = 0;

					int gBonus = (int)Math.Round((( from.Skills[SkillName.ItemID].Value * RelicValue ) / 100), 0);

					if ( this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild ){ gBonus = gBonus + (int)Math.Round((( 100.00 * RelicValue ) / 100), 0); GuildMember = 1; } // FOR GUILD MEMBERS

			if ( BeggingPose(from) > 0 && GuildMember == 0 )
			{
				Titles.AwardKarma( from, -BeggingKarma( from ), true );
				from.Say( BeggingWords() );
				gBonus = (int)Math.Round((( from.Skills[SkillName.Begging].Value * RelicValue ) / 100), 0);
			}
					gBonus = gBonus + RelicValue;
					from.SendSound( BaseVendorConstants.SOUND_RELIC );
					from.AddToBackpack ( new Gold( gBonus ) );
					string sMessage = "";
					switch ( Utility.RandomMinMax( 0, BaseVendorConstants.RANDOM_REWARD_MESSAGE_COUNT - 1 ) )
					{
						case 0:	sMessage = string.Format(BaseVendorStringConstants.RELIC_REWARD_1_FORMAT, gBonus.ToString());		break;
						case 1:	sMessage = string.Format(BaseVendorStringConstants.RELIC_REWARD_2_FORMAT, gBonus.ToString());		break;
						case 2:	sMessage = string.Format(BaseVendorStringConstants.RELIC_REWARD_3_FORMAT, gBonus.ToString());		break;
						case 3:	sMessage = string.Format(BaseVendorStringConstants.RELIC_REWARD_4_FORMAT, gBonus.ToString());		break;
						case 4:	sMessage = string.Format(BaseVendorStringConstants.RELIC_REWARD_5_FORMAT, gBonus.ToString());		break;
						case 5:	sMessage = string.Format(BaseVendorStringConstants.RELIC_REWARD_6_FORMAT, gBonus.ToString());		break;
						case 6:	sMessage = string.Format(BaseVendorStringConstants.RELIC_REWARD_7_FORMAT, gBonus.ToString());		break;
						case 7:	sMessage = string.Format(BaseVendorStringConstants.RELIC_REWARD_8_FORMAT, gBonus.ToString());		break;
						case 8:	sMessage = string.Format(BaseVendorStringConstants.RELIC_REWARD_9_FORMAT, gBonus.ToString());		break;
						case 9:	sMessage = string.Format(BaseVendorStringConstants.RELIC_REWARD_10_FORMAT, gBonus.ToString());		break;
					}
					this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
					dropped.Delete();
					return true;
				}
				else if ( dropped is Cargo )
				{
					Server.Items.Cargo.GiveCargo( (Cargo)dropped, this, from );
				}
				else if ( dropped is Museums && this is VarietyDealer )
				{
					Server.Items.Museums.GiveAntique( (Museums)dropped, this, from );
				}
				else if ( Server.Multis.BaseBoat.isRolledCarpet( dropped ) && ( this is Tailor || this is TailorGuildmaster ) )
				{
					Item carpet = null;

					if ( dropped is MagicDockedCarpetA || dropped is MagicCarpetADeed ){ carpet = new MagicCarpetBDeed(); }
					else if ( dropped is MagicDockedCarpetB || dropped is MagicCarpetBDeed ){ carpet = new MagicCarpetCDeed(); }
					else if ( dropped is MagicDockedCarpetC || dropped is MagicCarpetCDeed ){ carpet = new MagicCarpetDDeed(); }
					else if ( dropped is MagicDockedCarpetD || dropped is MagicCarpetDDeed ){ carpet = new MagicCarpetEDeed(); }
					else if ( dropped is MagicDockedCarpetE || dropped is MagicCarpetEDeed ){ carpet = new MagicCarpetFDeed(); }
					else if ( dropped is MagicDockedCarpetF || dropped is MagicCarpetFDeed ){ carpet = new MagicCarpetGDeed(); }
					else if ( dropped is MagicDockedCarpetG || dropped is MagicCarpetGDeed ){ carpet = new MagicCarpetHDeed(); }
					else if ( dropped is MagicDockedCarpetH || dropped is MagicCarpetHDeed ){ carpet = new MagicCarpetIDeed(); }
					else if ( dropped is MagicDockedCarpetI || dropped is MagicCarpetIDeed ){ carpet = new MagicCarpetADeed(); }

					dropped.Delete();
					carpet.Hue = dropped.Hue;
					from.AddToBackpack( carpet );
					SayTo(from, BaseVendorStringConstants.CARPET_ALTERED);
					Effects.PlaySound(from.Location, from.Map, BaseVendorConstants.SOUND_CARPET);
				}
				else if ( dropped is Gold && this is Mapmaker )
				{
					if ( dropped.Amount == BaseVendorConstants.MAP_COST )
					{
						if ( from.Map == Map.Trammel && from.X>5124 && from.Y>3041 && from.X<6147 && from.Y<4092 )
							from.AddToBackpack ( new WorldMapAmbrosia() );
						else if ( from.Map == Map.Trammel && from.X>859 && from.Y>3181 && from.X<2133 && from.Y<4092 )
							from.AddToBackpack ( new WorldMapUmber() );
						else if ( from.Map == Map.Malas && from.X<1870 )
							from.AddToBackpack ( new WorldMapSerpent() );
						else if ( from.Map == Map.Tokuno )
							from.AddToBackpack ( new WorldMapIslesOfDread() );
						else if ( from.Map == Map.TerMur && from.X>132 && from.Y>4 && from.X<1165 && from.Y<1798 )
							from.AddToBackpack ( new WorldMapSavage() );
						else if ( from.Map == Map.Trammel && from.X<6125 && from.Y<824 && from.X<7175 && from.Y<2746 )
							from.AddToBackpack ( new WorldMapBottle() );
						else if ( from.Map == Map.Felucca && from.X<5420 && from.Y<4096)
							from.AddToBackpack ( new WorldMapLodor() );
						else
							from.AddToBackpack ( new WorldMapSosaria() );

						string sMessage = BaseVendorStringConstants.MAP_PURCHASE;
						this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
						dropped.Delete();
						return true;
					}
					return false;
				}
				else if (	( dropped is DDCopper || dropped is DDSilver ) && 
							( this is Minter || this is Banker )	)
				{
					int nRate = BaseVendorConstants.EXCHANGE_RATE_SILVER;
					string sCoin = BaseVendorStringConstants.CURRENCY_TYPE_SILVER;
					if ( dropped is DDCopper ){ nRate = BaseVendorConstants.EXCHANGE_RATE_COPPER; sCoin = BaseVendorStringConstants.CURRENCY_TYPE_COPPER;}

					int nCoins = dropped.Amount;
					int nGold = (int)Math.Floor((decimal)(dropped.Amount / nRate));
					int nChange = dropped.Amount - ( nGold * nRate );

					string sMessage = BaseVendorStringConstants.CURRENCY_ERROR_NOT_ENOUGH;

					if ( ( nGold > 0 ) && ( nChange > 0 ) )
					{
						sMessage = string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_WITH_CHANGE_FORMAT, nGold.ToString(), nChange.ToString(), sCoin);
						from.AddToBackpack ( new Gold( nGold ) );
					}
					else if ( nGold > 0 )
					{
						sMessage = string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_FORMAT, nGold.ToString());
						from.AddToBackpack ( new Gold( nGold ) );
					}

					this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);

					if ( ( nChange > 0 ) && ( dropped is DDCopper ) ){ from.AddToBackpack ( new DDCopper( nChange ) ); }
					else if ( ( nChange > 0 ) && ( dropped is DDSilver ) ){ from.AddToBackpack ( new DDSilver( nChange ) ); }

					dropped.Delete();
					return true;
				}
				else if ( ( dropped is DDXormite ) && ( this is Minter || this is Banker ) )
				{
					int nGold = dropped.Amount * BaseVendorConstants.CURRENCY_MULT_XORMITE;

					string sMessage = string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_FORMAT, nGold.ToString());
					from.AddToBackpack ( new Gold( nGold ) );

					this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);

					dropped.Delete();
					return true;
				}
				else if ( ( dropped is Crystals ) && ( this is Minter || this is Banker ) )
				{
					int nGold = dropped.Amount * BaseVendorConstants.CURRENCY_MULT_CRYSTALS;

					string sMessage = string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_FORMAT, nGold.ToString());
					from.AddToBackpack ( new Gold( nGold ) );

					this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);

					dropped.Delete();
					return true;
				}
				else if ( ( dropped is DDJewels ) && ( this is Minter || this is Banker ) )
				{
					int nGold = dropped.Amount * BaseVendorConstants.CURRENCY_MULT_JEWELS;

					string sMessage = string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_FORMAT, nGold.ToString());
					from.AddToBackpack ( new Gold( nGold ) );

					this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);

					dropped.Delete();
					return true;
				}
				else if ( ( dropped is DDGemstones ) && ( this is Minter || this is Banker ) )
				{
					int nGold = dropped.Amount * BaseVendorConstants.CURRENCY_MULT_GEMSTONES;

					string sMessage = string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_FORMAT, nGold.ToString());
					from.AddToBackpack ( new Gold( nGold ) );

					this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);

					dropped.Delete();
					return true;
				}
				else if ( ( dropped is DDGoldNuggets ) && ( this is Minter || this is Banker ) )
				{
					int nGold = dropped.Amount;

					string sMessage = string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_FORMAT, nGold.ToString());
					from.AddToBackpack ( new Gold( nGold ) );

					this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);

					dropped.Delete();
					return true;
				}
				else if ( dropped is BottleOfParts && ( this is Alchemist || this is Witches ) )
				{
					int GuildMember = 0;

					int iForensics = (int)from.Skills[SkillName.Forensics].Value / BaseVendorConstants.SKILL_DIVISOR;
					int iAnatomy = (int)from.Skills[SkillName.Anatomy].Value / BaseVendorConstants.SKILL_DIVISOR;
					int nBottle = Utility.RandomMinMax( BaseVendorConstants.BOTTLE_MIN, BaseVendorConstants.BOTTLE_MAX ) + Utility.RandomMinMax( 0, iForensics ) + Utility.RandomMinMax( 0, iAnatomy );

					int gBonus = (int)Math.Round((( from.Skills[SkillName.ItemID].Value * nBottle ) / 100), 0);

					if ( this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild ){ gBonus = gBonus + (int)Math.Round((( 100.0 * nBottle ) / 100), 0); GuildMember = 1; } // FOR GUILD MEMBERS

					if ( BeggingPose(from) > 0 && GuildMember == 0 ) // LET US SEE IF THEY ARE BEGGING
					{
						Titles.AwardKarma( from, -BeggingKarma( from ), true );
						from.Say( BeggingWords() );
						gBonus = (int)Math.Round((( from.Skills[SkillName.Begging].Value * nBottle ) / 100), 0);
					}
					gBonus = (gBonus + nBottle) * dropped.Amount;
					from.AddToBackpack ( new Gold( gBonus ) );
					string sMessage = "";
					switch ( Utility.RandomMinMax( 0, BaseVendorConstants.RANDOM_REWARD_MESSAGE_COUNT - 1 ) )
					{
						case 0:	sMessage = string.Format(BaseVendorStringConstants.BOTTLE_REWARD_1_FORMAT, gBonus.ToString());						break;
						case 1:	sMessage = string.Format(BaseVendorStringConstants.BOTTLE_REWARD_2_FORMAT, gBonus.ToString());										break;
						case 2:	sMessage = string.Format(BaseVendorStringConstants.BOTTLE_REWARD_3_FORMAT, gBonus.ToString());								break;
						case 3:	sMessage = string.Format(BaseVendorStringConstants.BOTTLE_REWARD_4_FORMAT, gBonus.ToString());	break;
						case 4:	sMessage = string.Format(BaseVendorStringConstants.BOTTLE_REWARD_5_FORMAT, gBonus.ToString());					break;
						case 5:	sMessage = string.Format(BaseVendorStringConstants.BOTTLE_REWARD_6_FORMAT, gBonus.ToString());				break;
						case 6:	sMessage = string.Format(BaseVendorStringConstants.BOTTLE_REWARD_7_FORMAT, gBonus.ToString());				break;
						case 7:	sMessage = string.Format(BaseVendorStringConstants.BOTTLE_REWARD_8_FORMAT, gBonus.ToString());			break;
						case 8:	sMessage = string.Format(BaseVendorStringConstants.BOTTLE_REWARD_9_FORMAT, gBonus.ToString());					break;
						case 9:	sMessage = string.Format(BaseVendorStringConstants.BOTTLE_REWARD_10_FORMAT, gBonus.ToString());	break;
					}
					this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
					dropped.Delete();
					return true;
				}
				else if ( this is ThiefGuildmaster && pm.NpcGuild == NpcGuild.ThievesGuild && ( // TOMB RAIDING
					dropped is RockArtifact || 
					dropped is SkullCandleArtifact || 
					dropped is BottleArtifact || 
					dropped is DamagedBooksArtifact || 
					dropped is StretchedHideArtifact || 
					dropped is BrazierArtifact || 
					dropped is LampPostArtifact || 
					dropped is BooksNorthArtifact || 
					dropped is BooksWestArtifact || 
					dropped is BooksFaceDownArtifact || 
					dropped is StuddedLeggingsArtifact || 
					dropped is EggCaseArtifact || 
					dropped is SkinnedGoatArtifact || 
					dropped is GruesomeStandardArtifact || 
					dropped is BloodyWaterArtifact || 
					dropped is TarotCardsArtifact || 
					dropped is BackpackArtifact || 
					dropped is StuddedTunicArtifact || 
					dropped is CocoonArtifact || 
					dropped is SkinnedDeerArtifact || 
					dropped is SaddleArtifact || 
					dropped is LeatherTunicArtifact || 
					dropped is RuinedPaintingArtifact ) )
				{
					int TombRaid = BaseVendorConstants.TOMB_RAID_DEFAULT;

					if ( dropped is RockArtifact || dropped is SkullCandleArtifact || dropped is BottleArtifact || dropped is DamagedBooksArtifact ){ TombRaid = BaseVendorConstants.TOMB_RAID_COMMON; }
					else if ( dropped is StretchedHideArtifact || dropped is BrazierArtifact ){ TombRaid = BaseVendorConstants.TOMB_RAID_UNCOMMON; }
					else if ( dropped is LampPostArtifact || dropped is BooksNorthArtifact || dropped is BooksWestArtifact || dropped is BooksFaceDownArtifact ){ TombRaid = BaseVendorConstants.TOMB_RAID_RARE; }
					else if ( dropped is StuddedTunicArtifact || dropped is CocoonArtifact ){ TombRaid = BaseVendorConstants.TOMB_RAID_VERY_RARE; }
					else if ( dropped is SkinnedDeerArtifact ){ TombRaid = BaseVendorConstants.TOMB_RAID_EXTREMELY_RARE; }
					else if ( dropped is SaddleArtifact || dropped is LeatherTunicArtifact ){ TombRaid = BaseVendorConstants.TOMB_RAID_LEGENDARY; }
					else if ( dropped is RuinedPaintingArtifact ){ TombRaid = BaseVendorConstants.TOMB_RAID_UNIQUE; }

					from.AddToBackpack ( new Gold( TombRaid ) );
					string sMessage = "";
					switch ( Utility.RandomMinMax( 0, BaseVendorConstants.RANDOM_REWARD_MESSAGE_COUNT - 1 ) )
					{
						case 0:	sMessage = string.Format(BaseVendorStringConstants.TOMB_RAID_1_FORMAT, TombRaid.ToString());						break;
						case 1:	sMessage = string.Format(BaseVendorStringConstants.TOMB_RAID_2_FORMAT, TombRaid.ToString());										break;
						case 2:	sMessage = string.Format(BaseVendorStringConstants.TOMB_RAID_3_FORMAT, TombRaid.ToString());				break;
						case 3:	sMessage = string.Format(BaseVendorStringConstants.TOMB_RAID_4_FORMAT, TombRaid.ToString());	break;
						case 4:	sMessage = string.Format(BaseVendorStringConstants.TOMB_RAID_5_FORMAT, TombRaid.ToString());					break;
						case 5:	sMessage = string.Format(BaseVendorStringConstants.TOMB_RAID_6_FORMAT, TombRaid.ToString());				break;
						case 6:	sMessage = string.Format(BaseVendorStringConstants.TOMB_RAID_7_FORMAT, TombRaid.ToString());				break;
						case 7:	sMessage = string.Format(BaseVendorStringConstants.TOMB_RAID_8_FORMAT, TombRaid.ToString());		break;
						case 8:	sMessage = string.Format(BaseVendorStringConstants.TOMB_RAID_9_FORMAT, TombRaid.ToString());				break;
						case 9:	sMessage = string.Format(BaseVendorStringConstants.TOMB_RAID_10_FORMAT, TombRaid.ToString());							break;
					}
					this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);

					Titles.AwardFame( from, TombRaid/BaseVendorConstants.TOMB_RAID_FAME_DIVISOR, true );

					dropped.Delete();
					return true;
				}
				else if ( dropped is Item && this is Thief )
				{
					int GuildMember = 0;

					int iAmThief = (int)from.Skills[SkillName.Stealing].Value;

					if ( iAmThief < BaseVendorConstants.THIEF_SKILL_REQUIREMENT ){ this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, BaseVendorStringConstants.THIEF_REJECTION, from.NetState); }
					else if ( dropped is StealBox || dropped is StealMetalBox || dropped is StealBag )
					{
						int gBonus = (int)Math.Round((( from.Skills[SkillName.ItemID].Value * BaseVendorConstants.STEAL_BOX_BASE_VALUE ) / 100), 0);
						if ( this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild ){ gBonus = gBonus + (int)Math.Round((( 100.00 * BaseVendorConstants.STEAL_BOX_BASE_VALUE ) / 100), 0); GuildMember = 1; } // FOR GUILD MEMBERS
						if ( BeggingPose(from) > 0 && GuildMember == 0 ) // LET US SEE IF THEY ARE BEGGING
						{
							Titles.AwardKarma( from, -BeggingKarma( from ), true );
							from.Say( BeggingWords() );
							gBonus = (int)Math.Round((( from.Skills[SkillName.Begging].Value * BaseVendorConstants.STEAL_BOX_BASE_VALUE ) / 100), 0);
						}
						gBonus = gBonus + BaseVendorConstants.STEAL_BOX_BASE_VALUE;
						from.AddToBackpack ( new Gold( gBonus ) );
						string sMessage = "";
						switch ( Utility.RandomMinMax( 0, BaseVendorConstants.RANDOM_REWARD_MESSAGE_COUNT - 1 ) )
						{
							case 0:	sMessage = string.Format(BaseVendorStringConstants.THIEF_1_FORMAT, gBonus.ToString());						break;
							case 1:	sMessage = string.Format(BaseVendorStringConstants.THIEF_2_FORMAT, gBonus.ToString());										break;
							case 2:	sMessage = string.Format(BaseVendorStringConstants.THIEF_3_FORMAT, gBonus.ToString());					break;
							case 3:	sMessage = string.Format(BaseVendorStringConstants.THIEF_4_FORMAT, gBonus.ToString());	break;
							case 4:	sMessage = string.Format(BaseVendorStringConstants.THIEF_5_FORMAT, gBonus.ToString());					break;
							case 5:	sMessage = string.Format(BaseVendorStringConstants.THIEF_6_FORMAT, gBonus.ToString());				break;
							case 6:	sMessage = string.Format(BaseVendorStringConstants.THIEF_7_FORMAT, gBonus.ToString());					break;
							case 7:	sMessage = string.Format(BaseVendorStringConstants.THIEF_8_FORMAT, gBonus.ToString());		break;
							case 8:	sMessage = string.Format(BaseVendorStringConstants.THIEF_9_FORMAT, gBonus.ToString());						break;
							case 9:	sMessage = string.Format(BaseVendorStringConstants.THIEF_10_FORMAT, gBonus.ToString());							break;
						}
						this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
						dropped.Delete();
						return true;
					}
					else if ( dropped is StolenChest )
					{
						StolenChest sRipoff = (StolenChest)dropped;
						int vRipoff = sRipoff.ContainerValue;
						int gBonus = (int)Math.Round((( from.Skills[SkillName.ItemID].Value * vRipoff ) / 100), 0);
						if ( this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild ){ gBonus = gBonus + (int)Math.Round((( 100.00 * vRipoff ) / 100), 0); GuildMember = 1; } // FOR GUILD MEMBERS

						if ( BeggingPose(from) > 0 && GuildMember == 0 ) // LET US SEE IF THEY ARE BEGGING
						{
							Titles.AwardKarma( from, -BeggingKarma( from ), true );
							from.Say( BeggingWords() );
							gBonus = (int)Math.Round((( from.Skills[SkillName.Begging].Value * vRipoff ) / 100), 0);
						}
						gBonus = gBonus + vRipoff;
						from.AddToBackpack ( new Gold( gBonus ) );
						string sMessage = "";
						switch ( Utility.RandomMinMax( 0, BaseVendorConstants.RANDOM_REWARD_MESSAGE_COUNT - 1 ) )
						{
							case 0:	sMessage = string.Format(BaseVendorStringConstants.THIEF_1_FORMAT, gBonus.ToString());						break;
							case 1:	sMessage = string.Format(BaseVendorStringConstants.THIEF_2_FORMAT, gBonus.ToString());										break;
							case 2:	sMessage = string.Format(BaseVendorStringConstants.THIEF_3_FORMAT, gBonus.ToString());					break;
							case 3:	sMessage = string.Format(BaseVendorStringConstants.THIEF_4_FORMAT, gBonus.ToString());	break;
							case 4:	sMessage = string.Format(BaseVendorStringConstants.THIEF_5_FORMAT, gBonus.ToString());					break;
							case 5:	sMessage = string.Format(BaseVendorStringConstants.THIEF_6_FORMAT, gBonus.ToString());				break;
							case 6:	sMessage = string.Format(BaseVendorStringConstants.THIEF_7_FORMAT, gBonus.ToString());					break;
							case 7:	sMessage = string.Format(BaseVendorStringConstants.THIEF_8_FORMAT, gBonus.ToString());		break;
							case 8:	sMessage = string.Format(BaseVendorStringConstants.THIEF_9_FORMAT, gBonus.ToString());						break;
							case 9:	sMessage = string.Format(BaseVendorStringConstants.THIEF_10_FORMAT, gBonus.ToString());							break;
						}
						this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
						dropped.Delete();
						return true;
					}
				}
				else if ( ( dropped is HenchmanFighterItem || dropped is HenchmanArcherItem || dropped is HenchmanWizardItem ) && ( this is InnKeeper || this is TavernKeeper || this is Barkeeper ) )
				{
					int fairTrade = 1;
					string sMessage = "";
					switch ( Utility.RandomMinMax( 0, BaseVendorConstants.RANDOM_HENCHMAN_MESSAGE_COUNT - 1 ) )
					{
						case 0:	sMessage = BaseVendorStringConstants.HENCHMAN_1; break;
						case 1:	sMessage = BaseVendorStringConstants.HENCHMAN_2; break;
						case 2:	sMessage = BaseVendorStringConstants.HENCHMAN_3; break;
						case 3:	sMessage = BaseVendorStringConstants.HENCHMAN_4; break;
						case 4:	sMessage = BaseVendorStringConstants.HENCHMAN_5; break;
						case 5:	sMessage = BaseVendorStringConstants.HENCHMAN_6; break;
						case 6:	sMessage = BaseVendorStringConstants.HENCHMAN_7; break;
						case 7:	sMessage = BaseVendorStringConstants.HENCHMAN_8;		break;
					}
					if ( dropped is HenchmanFighterItem )
					{
						HenchmanFighterItem myFollower = (HenchmanFighterItem)dropped;
						if ( myFollower.HenchDead > 0 ){ fairTrade = 0; } else
						{
							HenchmanFighterItem newFollower = new HenchmanFighterItem();
							newFollower.HenchTimer = myFollower.HenchTimer;
							newFollower.HenchBandages = myFollower.HenchBandages;
							from.AddToBackpack ( newFollower );
						}
					}
					else if ( dropped is HenchmanWizardItem )
					{
						HenchmanWizardItem myFollower = (HenchmanWizardItem)dropped;
						if ( myFollower.HenchDead > 0 ){ fairTrade = 0; } else
						{
							HenchmanWizardItem newFollower = new HenchmanWizardItem();
							newFollower.HenchTimer = myFollower.HenchTimer;
							newFollower.HenchBandages = myFollower.HenchBandages;
							from.AddToBackpack ( newFollower );
						}
					}
					else if ( dropped is HenchmanArcherItem )
					{
						HenchmanArcherItem myFollower = (HenchmanArcherItem)dropped;
						if ( myFollower.HenchDead > 0 ){ fairTrade = 0; } else
						{
							HenchmanArcherItem newFollower = new HenchmanArcherItem();
							newFollower.HenchTimer = myFollower.HenchTimer;
							newFollower.HenchBandages = myFollower.HenchBandages;
							from.AddToBackpack ( newFollower );
						}
					}
					if ( fairTrade == 1 )
					{
						this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
						dropped.Delete();
						return true;
					}
					else { this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, BaseVendorStringConstants.HENCHMAN_REJECTION, from.NetState); }
				}
				else if ( dropped is BookBox && ( this is Mage || this is KeeperOfChivalry || this is Witches || this is Necromancer || this is MageGuildmaster || this is HolyMage || this is NecroMage ) )
				{
					Container pack = (Container)dropped;
						List<Item> items = new List<Item>();
						foreach (Item item in pack.Items)
						{
							items.Add(item);
						}
						foreach (Item item in items)
						{
							from.AddToBackpack ( item );
						}
					this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, BaseVendorStringConstants.CURSE_REMOVED_BOOKS, from.NetState);
					dropped.Delete();
					return true;
				}
				else if ( dropped is CurseItem && ( this is Mage || this is KeeperOfChivalry || this is Witches || this is Necromancer || this is MageGuildmaster || this is HolyMage || this is NecroMage ) )
				{
					Container pack = (Container)dropped;
						List<Item> items = new List<Item>();
						foreach (Item item in pack.Items)
						{
							items.Add(item);
						}
						foreach (Item item in items)
						{
							from.AddToBackpack ( item );
						}
					string curseName = dropped.Name;
						if ( curseName == ""){ curseName = BaseVendorStringConstants.DEFAULT_ITEM_NAME; }
					this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, string.Format(BaseVendorStringConstants.CURSE_REMOVED_FORMAT, curseName), from.NetState);
					dropped.Delete();
					return true;
				}
				else if ( ( dropped is SewageItem || dropped is SlimeItem ) && ( this is InnKeeper || this is TavernKeeper || this is Barkeeper || this is Waiter ) )
				{
					Container pack = (Container)dropped;
						List<Item> items = new List<Item>();
						foreach (Item item in pack.Items)
						{
							items.Add(item);
						}
						foreach (Item item in items)
						{
							from.AddToBackpack ( item );
						}
					this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, BaseVendorStringConstants.ITEM_CLEANED, from.NetState);
					dropped.Delete();
					return true;
				}
				else if ( dropped is WeededItem && ( this is Alchemist || this is Herbalist ) )
				{
					Container pack = (Container)dropped;
						List<Item> items = new List<Item>();
						foreach (Item item in pack.Items)
						{
							items.Add(item);
						}
						foreach (Item item in items)
						{
							from.AddToBackpack ( item );
						}
					this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, BaseVendorStringConstants.WEEDS_REMOVED, from.NetState);
					dropped.Delete();
					return true;
				}


				if ( dropped is SmallBOD || dropped is LargeBOD )
				{
					if( Core.ML )
					{
						if( ((PlayerMobile)from).NextBODTurnInTime > DateTime.UtcNow )
						{
							SayTo( from, 1079976 );	//
							return false;
						}
					}

					if ( !IsValidBulkOrder( dropped ) || !SupportsBulkOrders( from ) )
					{
						SayTo( from, 1045130 ); // That order is for some other shopkeeper.
						return false;
					}
					else if ( ( dropped is SmallBOD && !( (SmallBOD)dropped ).Complete ) || ( dropped is LargeBOD && !( (LargeBOD)dropped ).Complete ) )
					{
						SayTo( from, 1045131 ); // You have not completed the order yet.
						return false;
					}
					
					int creds = 0;

					if ((dropped is LargeSmithBOD || dropped is SmallSmithBOD) && from is PlayerMobile)
					{
						if (dropped is LargeSmithBOD)
							creds = SmithRewardCalculator.Instance.ComputePoints( (LargeBOD)dropped );
						else 
							creds = SmithRewardCalculator.Instance.ComputePoints( (SmallBOD)dropped )/3;

						((PlayerMobile)from).BlacksmithBOD += creds; 
					}
					else if ((dropped is LargeTailorBOD || dropped is SmallTailorBOD) && from is PlayerMobile)
					{
						if (dropped is LargeTailorBOD)
							creds = TailorRewardCalculator.Instance.ComputePoints( (LargeBOD)dropped );		
						else
							creds = TailorRewardCalculator.Instance.ComputePoints( (SmallBOD)dropped ) /3;
							
						((PlayerMobile)from).TailorBOD += creds; 
					}
					
					from.SendSound( BaseVendorConstants.SOUND_RELIC );

					SayTo( from, string.Format(BaseVendorStringConstants.BOD_CREDITS_EARNED_FORMAT, creds.ToString()) );
					SayTo( from, BaseVendorStringConstants.BOD_CREDITS_INFO );

					OnSuccessfulBulkOrderReceive( from );

					((PlayerMobile)from).NextBODTurnInTime = DateTime.Now + TimeSpan.FromSeconds( BaseVendorConstants.BOD_TURNIN_DELAY_SECONDS );

					dropped.Delete();
					return true;
				}
			}

			return base.OnDragDrop( from, dropped );
		}

		#endregion

		#region Buy/Sell Item Processing

		private GenericBuyInfo LookupDisplayObject( object obj )
		{
			IBuyItemInfo[] buyInfo = this.GetBuyInfo();

			for ( int i = 0; i < buyInfo.Length; ++i ) {
				GenericBuyInfo gbi = (GenericBuyInfo)buyInfo[i];

				if ( gbi.GetDisplayEntity() == obj )
					return gbi;
			}

			return null;
		}

		#endregion

		#region Credit System

		public void ClaimCredits(Mobile from, int amount)
		{
			if (!from.Player || from.Backpack == null)
				return;

			if ( ( this is Tailor || this is TailorGuildmaster) && ((PlayerMobile)from).TailorBOD <= 0)
				return;
				
			if ( ( this is Blacksmith || this is BlacksmithGuildmaster) && ((PlayerMobile)from).BlacksmithBOD <= 0)
				return;
		
			this.Say(BaseVendorStringConstants.CREDITS_THANK_YOU);
			this.Say(BaseVendorStringConstants.CREDITS_LOOKING);
			
			Timer.DelayCall( TimeSpan.FromSeconds( BaseVendorConstants.LOOK_FOR_REWARD_DELAY_SECONDS ), new TimerStateCallback ( LookForReward ), new object[]{ from, amount }  );

		}
		
		public void LookForReward( object state )
		{
			if (this.Deleted || this == null)
				return;

			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			int amount = (int)states[1];
			
			if (!(from is PlayerMobile) || from == null)
				return;
				
			switch (Utility.Random(BaseVendorConstants.RANDOM_LOOK_REWARD_COUNT))
			{
				case 0: Emote(BaseVendorStringConstants.LOOK_REWARD_1); break;
				case 1: Emote(BaseVendorStringConstants.LOOK_REWARD_2); break;
				case 2: Emote(BaseVendorStringConstants.LOOK_REWARD_3); break;
				case 3: Emote(BaseVendorStringConstants.LOOK_REWARD_4); break;
				case 4: Emote(BaseVendorStringConstants.LOOK_REWARD_5); break;
			}
			
			if (Utility.RandomDouble() > BaseVendorConstants.CREDIT_VAR_HIGH)
				GiveReward(from, amount);
			else 
				Timer.DelayCall( TimeSpan.FromSeconds( BaseVendorConstants.LOOK_FOR_REWARD_DELAY_SECONDS ), new TimerStateCallback ( LookForReward ), new object[]{ from, amount } );
		}
				
		
		private void GiveReward(Mobile from, int amount)
		{ 
			int credittype = 1; // blacksmith
			if (this is Tailor || this is TailorGuildmaster)
				credittype = 2;
			
			int credits = 0;
			if (amount >= 1)
   			{
	  			if (credittype == 1)
      				{
	  				if (amount > ((PlayerMobile)from).BlacksmithBOD)
       						return;
	     
	  				credits = amount;
      					((PlayerMobile)from).BlacksmithBOD -= credits;
	   			}
	   			else if (credittype == 2)
       				{
	   				if (amount > ((PlayerMobile)from).TailorBOD)
       						return;

      					credits = amount;
       					((PlayerMobile)from).TailorBOD -= credits;
	    			}
	    		}
      
			else if (credittype == 1)
   			{
				credits = ((PlayerMobile)from).BlacksmithBOD;
    				((PlayerMobile)from).BlacksmithBOD -= credits;
			}
			else //tailor
   			{
				credits = ((PlayerMobile)from).TailorBOD;
    				((PlayerMobile)from).TailorBOD -= credits;
    			}

			Titles.AwardFame( from, (credits/BaseVendorConstants.FAME_DIVISOR), true );
			
			this.Say(BaseVendorStringConstants.CREDITS_REWARD_GIVEN);
			from.SendMessage(string.Format(BaseVendorStringConstants.CREDITS_REDEEMED_FORMAT, credits.ToString()));
		
			Item reward = null;
			int gold = 0;
			
			if (Utility.RandomDouble() < BaseVendorConstants.CREDIT_VAR_LOW)
				credits = (int)((double)credits * BaseVendorConstants.CREDIT_MULT_LOW);
			else if (Utility.RandomDouble() > BaseVendorConstants.CREDIT_VAR_HIGH)
				credits = (int)((double)credits * BaseVendorConstants.CREDIT_MULT_HIGH);
			
			AdventuresFunctions.DiminishingReturns( credits, BaseVendorConstants.DIMINISHING_RETURNS_CAP );
			
			if (credittype == 1)//smith
			{
				int basecred = BaseVendorConstants.BASE_CREDIT; // variable which makes editing all reward values much easier for balancing
				
				if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_1))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new SturdyShovel(); break;
						case 1: reward = new SturdyPickaxe(); break;
						case 2: reward = new LeatherGlovesOfMining( 1 ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_2))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new GargoylesPickaxe(); break;
						case 1: reward = new ProspectorsTool(); break;
						case 2: reward = new StuddedGlovesOfMining( 3 ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_3))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new ColoredAnvil(); break;
						case 1: reward = new ProspectorsTool(); break;
						case 2: reward = new RingmailGlovesOfMining( 5 ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_4))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new RunicHammer( CraftResource.Iron + 1, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (1*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
						case 1: reward = new ProspectorsTool(); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 2, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (2*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_5))//+18
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new RunicHammer( CraftResource.Iron + 2, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (2*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
						case 1: reward = new ColoredAnvil(); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 3, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (3*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_6)) //+22
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new RunicHammer( CraftResource.Iron + 2, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (2*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
						case 1: reward = new ColoredAnvil(); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 3, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (3*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_7)) //+26
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new RunicHammer( CraftResource.Iron + 3, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (3*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
						case 1: reward = new ColoredAnvil(); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 4, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (4*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_8)) //+30
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new RunicHammer( CraftResource.Iron + 4, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (4*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
						case 1: reward = new DwarvenForge(); break;
						case 2: reward = new EnhancementDeed(); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_9)) //+34
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new AncientSmithyHammer( 10 ); break;
						case 1: reward = new DwarvenForge(); break;
						case 2: reward = new EnhancementDeed(); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_10)) //+38
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new AncientSmithyHammer( 15 ); break;
						case 1: reward = new DwarvenForge(); break;
						case 2: reward = new EnhancementDeed(); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_11)) //+38
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new AncientSmithyHammer( 15 ); break;
						case 1: reward = new ElvenForgeDeed(); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 5, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (5*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_12)) //+42
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new AncientSmithyHammer( 20 ); break;
						case 1: reward = new ElvenForgeDeed(); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 6, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (6*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_13)) //+46
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new AncientSmithyHammer( 20 ); break;
						case 1: reward = new PowerScroll( SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_1 ); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 7, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (7*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_14)) //+50
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new AncientSmithyHammer( 25 ); break;
						case 1: reward = new PowerScroll( SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_1 ); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 8, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (8*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_15)) //+54
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new OneHandedDeed(); break;
						case 1: reward = new PowerScroll( SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_2 ); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 8, ( BaseVendorConstants.RUNIC_CHARGES_BASE - (8*BaseVendorConstants.RUNIC_CHARGE_REDUCTION) ) ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_16)) //+58
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new OneHandedDeed(); break;
						case 1: reward = new PowerScroll( SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_2 ); break;
						case 2: reward = new ItemBlessDeed(); break;
					}
				}
				else if (credits > (basecred * BaseVendorConstants.CREDIT_MULT_TIER_16)) //only powerscrolls from here
				{
					double odds = Utility.RandomDouble();
					if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_1)
						reward = new PowerScroll( SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_5 );
					else if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_2)
						reward = new PowerScroll( SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_4 );
					else if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_3)
						reward = new PowerScroll( SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_3 );
					else if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_4)
						reward = new PowerScroll( SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_2 );
					else 
						reward = new PowerScroll( SkillName.Blacksmith, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_1 );
				}
			
			}
			else if (credittype == 2)//tailor
			{
				int basecred = BaseVendorConstants.BASE_CREDIT; // variable which makes editing all reward values much easier for balancing
				
				if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_1))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new SmallStretchedHideEastDeed(); break;
						case 1: reward = new SmallStretchedHideSouthDeed(); break;
						case 2: reward = new TallBannerEast(); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_2))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new MediumStretchedHideEastDeed(); break;
						case 1: reward = new MediumStretchedHideSouthDeed(); break;
						case 2: reward = new TallBannerNorth(); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_3))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new LightFlowerTapestryEastDeed(); break;
						case 1: reward = new LightFlowerTapestrySouthDeed(); break;
						case 2: reward = new SalvageBag(); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_4))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new BrownBearRugEastDeed(); break;
						case 1: reward = new BrownBearRugSouthDeed(); break;
						case 2: reward = new SalvageBag(); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_5))//+18
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new PolarBearRugEastDeed(); break;
						case 1: reward = new PolarBearRugSouthDeed(); break;
						case 2: reward = new SalvageBag(); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_6)) //+22
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new DarkFlowerTapestryEastDeed(); break;
						case 1: reward = new DarkFlowerTapestrySouthDeed(); break;
						case 2: reward = new RunicSewingKit( CraftResource.RegularLeather + 1, BaseVendorConstants.RUNIC_CHARGES_SEWING - (1*BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION) ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_7)) //+26
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new MagicScissors(); break;
						case 1: reward = new TallBannerEast(); break;
						case 2: reward = new GuildedTallBannerEast(); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_8)) //+30
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new GuildedTallBannerNorth(); break;
						case 1: reward = new TallBannerNorth(); break;
						case 2: reward = new MagicScissors(); break;
					}
				}
				else if (credits <= (basecred * 182)) //+34 (Note: 182 not in constants, keeping as-is for now)
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new EnhancementDeed(); break;
						case 1: reward = new GuildedTallBannerEast(); break;
						case 2: reward = new EnhancementDeed(); break;
					}
				}
				else if (credits <= (basecred * 220)) //+38 (Note: 220 not in constants, keeping as-is for now)
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new RunicSewingKit( CraftResource.RegularLeather + 2, BaseVendorConstants.RUNIC_CHARGES_SEWING - (2*BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION) ); break;
						case 1: reward = new GuildedTallBannerNorth(); break;
						case 2: reward = new EnhancementDeed(); break;
					}
				}
				else if (credits <= (basecred * 220)) //+38 (duplicate - keeping as-is)
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new EnhancementDeed(); break;
						case 1: reward = new RunicSewingKit( CraftResource.RegularLeather + 2, BaseVendorConstants.RUNIC_CHARGES_SEWING - (2*BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION) ); break;
						case 2: reward = new CathedralWindow1(); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_12)) //+42
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new CathedralWindow3(); break;
						case 1: reward = new RunicSewingKit( CraftResource.RegularLeather + 2, BaseVendorConstants.RUNIC_CHARGES_SEWING - (2*BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION) ); break;
						case 2: reward = new CathedralWindow2(); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_13)) //+46
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new CathedralWindow4(); break;
						case 1: reward = new RunicSewingKit( CraftResource.RegularLeather + 2, BaseVendorConstants.RUNIC_CHARGES_SEWING - (2*BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION) ); break;
						case 2: reward = new CathedralWindow5(); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_14)) //+50
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new LegsOfMusicalPanache(); break;
						case 1: reward = new RunicSewingKit( CraftResource.RegularLeather + 3, BaseVendorConstants.RUNIC_CHARGES_SEWING - (3*BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION) ); break;
						case 2: reward = new SkirtOfPower(); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_15)) //+54
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new ClothingBlessDeed(); break;
						case 1: reward = new PowerScroll( SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_1 ); break;
						case 2: reward = new RunicSewingKit( CraftResource.RegularLeather + 3, BaseVendorConstants.RUNIC_CHARGES_SEWING - (3*BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION) ); break;
					}
				}
				else if (credits <= (basecred * BaseVendorConstants.CREDIT_MULT_TIER_16)) //+58
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new ItemBlessDeed(); break;
						case 1: reward = new PowerScroll( SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_1 ); break;
						case 2: reward = new RunicSewingKit( CraftResource.RegularLeather + 3, BaseVendorConstants.RUNIC_CHARGES_SEWING - (3*BaseVendorConstants.RUNIC_SEWING_CHARGE_REDUCTION) );; break;
					}
				}
				else if (credits > (basecred * BaseVendorConstants.CREDIT_MULT_TIER_16)) //only powerscrolls from here
				{
					double odds = Utility.RandomDouble();
					if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_1)
						reward = new PowerScroll( SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_5 );
					else if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_2)
						reward = new PowerScroll( SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_4 );
					else if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_3)
						reward = new PowerScroll( SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_3 );
					else if (odds >= BaseVendorConstants.POWER_SCROLL_ODDS_4)
						reward = new PowerScroll( SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_2 );
					else 
						reward = new PowerScroll( SkillName.Tailoring, BaseVendorConstants.POWER_SCROLL_BASE + BaseVendorConstants.POWER_SCROLL_BONUS_1 );
				}
			}
						
			if ( reward != null )
			{
				from.AddToBackpack( reward );
			}
				
			gold = credits * Utility.RandomMinMax(BaseVendorConstants.GOLD_PER_CREDIT_MIN, BaseVendorConstants.GOLD_PER_CREDIT_MAX);
					
			if (AdventuresFunctions.IsInMidland((object)this))
				gold /= BaseVendorConstants.MIDLAND_GOLD_DIVISOR;

			if ( gold > BaseVendorConstants.BANK_DEPOSIT_THRESHOLD )
				Banker.Deposit(from, gold );
			else if ( gold > 0 )
				from.AddToBackpack( new Gold( gold ) );	
				
		}

        private void ProcessSinglePurchase( BuyItemResponse buy, IBuyItemInfo bii, List<BuyItemResponse> validBuy, ref int controlSlots, ref bool fullPurchase, ref int totalCost )
		{
			int amount = buy.Amount;

			if ( amount > bii.Amount )
				amount = bii.Amount;

			if ( amount <= 0 )
				return;

			int slots = bii.ControlSlots * amount;

			if ( controlSlots >= slots )
			{
				controlSlots -= slots;
			}
			else
			{
				fullPurchase = false;
				return;
			}

			totalCost += bii.Price * amount;
			validBuy.Add( buy );
		}

		private void ProcessValidPurchase( int amount, IBuyItemInfo bii, Mobile buyer, Container cont )
		{
			if ( amount > bii.Amount )
				amount = bii.Amount;

			if ( amount < 1 )
				return;

			bii.Amount -= amount;

			IEntity o = bii.GetEntity();

			if ( o is Item )
			{
				Item item = (Item)o;

				if ( item.Stackable )
				{
					item.Amount = amount;

					if ( cont == null || !cont.TryDropItem( buyer, item, false ) )
						item.MoveToWorld( buyer.Location, buyer.Map );
				}
				else
				{
					item.Amount = 1;

					if ( cont == null || !cont.TryDropItem( buyer, item, false ) )
						item.MoveToWorld( buyer.Location, buyer.Map );

					for ( int i = 1; i < amount; i++ )
					{
						item = bii.GetEntity() as Item;

						if ( item != null )
						{
							item.Amount = 1;

							if ( cont == null || !cont.TryDropItem( buyer, item, false ) )
								item.MoveToWorld( buyer.Location, buyer.Map );
						}
					}
				}
			}
			else if ( o is Mobile )
			{
				Mobile m = (Mobile)o;

				m.Direction = (Direction)Utility.Random( 8 );
				m.MoveToWorld( buyer.Location, buyer.Map );
				m.PlaySound( m.GetIdleSound() );

				if ( m is BaseCreature )
				{
					( (BaseCreature)m ).SetControlMaster( buyer );
					( (BaseCreature)m ).Tamable = true;
					( (BaseCreature)m ).MinTameSkill = 29.1;
				}

				for ( int i = 1; i < amount; ++i )
				{
					m = bii.GetEntity() as Mobile;

					if ( m != null )
					{
						m.Direction = (Direction)Utility.Random( 8 );
						m.MoveToWorld( buyer.Location, buyer.Map );

						if ( m is BaseCreature )
						{
							( (BaseCreature)m ).SetControlMaster( buyer );
							( (BaseCreature)m ).Tamable = true;
							( (BaseCreature)m ).MinTameSkill = 29.1;
						}
					}
				}
			}
		}

        public virtual bool OnBuyItems( Mobile buyer, List<BuyItemResponse> list )
		{
			if ( !IsActiveSeller )
				return false;

			if ( !buyer.CheckAlive() )
				return false;

			if ( !CheckVendorAccess( buyer ) )
			{
				this.Say(BaseVendorStringConstants.ERROR_NO_BUSINESS);
				return false;
			}

			UpdateBuyInfo();

			IBuyItemInfo[] buyInfo = this.GetBuyInfo();
			IShopSellInfo[] info = GetSellInfo();
			int totalCost = 0;
            List<BuyItemResponse> validBuy = new List<BuyItemResponse>( list.Count );
			Container cont;
			bool bought = false;
			bool fromBank = false;
			bool fullPurchase = true;
			int controlSlots = buyer.FollowersMax - buyer.Followers;

			foreach ( BuyItemResponse buy in list )
			{
				Serial ser = buy.Serial;
				int amount = buy.Amount;

				if ( ser.IsItem )
				{
					Item item = World.FindItem( ser );

					if ( item == null )
						continue;

					GenericBuyInfo gbi = LookupDisplayObject( item );

					if ( gbi != null )
					{
						ProcessSinglePurchase( buy, gbi, validBuy, ref controlSlots, ref fullPurchase, ref totalCost );
					}
					else if ( item != this.BuyPack && item.IsChildOf( this.BuyPack ) )
					{
						if ( amount > item.Amount )
							amount = item.Amount;

						if ( amount <= 0 )
							continue;

						foreach ( IShopSellInfo ssi in info )
						{
							if ( ssi.IsSellable( item ) )
							{
								if ( ssi.IsResellable( item ) )
								{
									totalCost += ssi.GetBuyPriceFor( item ) * amount;
									validBuy.Add( buy );
									break;
								}
							}
						}
					}
				}
				else if ( ser.IsMobile )
				{
					Mobile mob = World.FindMobile( ser );

					if ( mob == null )
						continue;

					GenericBuyInfo gbi = LookupDisplayObject( mob );

					if ( gbi != null )
						ProcessSinglePurchase( buy, gbi, validBuy, ref controlSlots, ref fullPurchase, ref totalCost );
				}
			}//foreach

			if ( fullPurchase && validBuy.Count == 0 )
				SayTo( buyer, 500190 ); // Thou hast bought nothing!
			else if ( validBuy.Count == 0 )
				SayTo( buyer, 500187 ); // Your order cannot be fulfilled, please try again.

			if ( validBuy.Count == 0 )
				return false;

			bought = ( buyer.AccessLevel >= AccessLevel.GameMaster );

			cont = buyer.Backpack;
			if ( !bought && cont != null )
			{
				if ( cont.ConsumeTotal( typeof( Gold ), totalCost ) )
					bought = true;
				else if ( totalCost < BaseVendorConstants.BANK_THRESHOLD_GOLD )
					SayTo( buyer, 500192 );//Begging thy pardon, but thou casnt afford that.
			}

			if ( !bought && totalCost >= BaseVendorConstants.BANK_THRESHOLD_GOLD )
			{
				cont = buyer.FindBankNoCreate();
				if ( cont != null && cont.ConsumeTotal( typeof( Gold ), totalCost ) )
				{
					bought = true;
					fromBank = true;
				}
				else
				{
					SayTo( buyer, 500191 ); //Begging thy pardon, but thy bank account lacks these funds.
				}
			}

			if ( !bought )
				return false;
			else
				buyer.PlaySound( BaseVendorConstants.SOUND_BUY );

			cont = buyer.Backpack;
			if ( cont == null )
				cont = buyer.BankBox;

			foreach ( BuyItemResponse buy in validBuy )
			{
				Serial ser = buy.Serial;
				int amount = buy.Amount;

				if ( amount < 1 )
					continue;

				if ( ser.IsItem )
				{
					Item item = World.FindItem( ser );

					if ( item == null )
						continue;

					GenericBuyInfo gbi = LookupDisplayObject( item );

					if ( gbi != null )
					{
						ProcessValidPurchase( amount, gbi, buyer, cont );
					}
					else
					{
						if ( amount > item.Amount )
							amount = item.Amount;

						foreach ( IShopSellInfo ssi in info )
						{
							if ( ssi.IsSellable( item ) )
							{
								if ( ssi.IsResellable( item ) )
								{
									Item buyItem;
									if ( amount >= item.Amount )
									{
										buyItem = item;
									}
									else
									{
										buyItem = Mobile.LiftItemDupe( item, item.Amount - amount );

										if ( buyItem == null )
											buyItem = item;
									}

									if ( cont == null || !cont.TryDropItem( buyer, buyItem, false ) )
										buyItem.MoveToWorld( buyer.Location, buyer.Map );

									break;
								}
							}
						}
					}
				}
				else if ( ser.IsMobile )
				{
					Mobile mob = World.FindMobile( ser );

					if ( mob == null )
						continue;

					GenericBuyInfo gbi = LookupDisplayObject( mob );

					if ( gbi != null )
						ProcessValidPurchase( amount, gbi, buyer, cont );
				}
			}//foreach

			if ( fullPurchase )
			{
				if ( buyer.AccessLevel >= AccessLevel.GameMaster )
					SayTo( buyer, true, BaseVendorStringConstants.PURCHASE_GM_FULL );
				else if ( fromBank )
					SayTo( buyer, true, BaseVendorStringConstants.PURCHASE_BANK_FORMAT, totalCost );
				else
					SayTo( buyer, true, BaseVendorStringConstants.PURCHASE_FORMAT, totalCost );
			}
			else
			{
				if ( buyer.AccessLevel >= AccessLevel.GameMaster )
					SayTo( buyer, true, BaseVendorStringConstants.PURCHASE_GM_PARTIAL );
				else if ( fromBank )
					SayTo( buyer, true, BaseVendorStringConstants.PURCHASE_BANK_PARTIAL_FORMAT, totalCost );
				else
					SayTo( buyer, true, BaseVendorStringConstants.PURCHASE_PARTIAL_FORMAT, totalCost );
			}

			return true;
		}

		#endregion

		#region Access Control

		public virtual bool CheckVendorAccess( Mobile from )
		{
			PlayerMobile pm = (PlayerMobile)from;

			if ( this is BaseGuildmaster && this.NpcGuild != pm.NpcGuild )
				return false;

			return true;
		}

		#endregion

		#region Buy/Sell Item Processing (continued)

		public virtual bool OnSellItems( Mobile seller, List<SellItemResponse> list )
		{
			if ( !IsActiveBuyer )
				return false;

			if ( !seller.CheckAlive() )
				return false;

			if ( !CheckVendorAccess( seller ) )
			{
				string sSay = BaseVendorStringConstants.ERROR_NO_BUSINESS;
				this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sSay, seller.NetState);
				return false;
			}

			seller.PlaySound( BaseVendorConstants.SOUND_BUY );

			IShopSellInfo[] info = GetSellInfo();
			IBuyItemInfo[] buyInfo = this.GetBuyInfo();
			int GiveGold = 0;
			int Sold = 0;
			Container cont;

			foreach ( SellItemResponse resp in list )
			{
				if ( resp.Item.RootParent != seller || resp.Amount <= 0 || !resp.Item.IsStandardLoot() || !resp.Item.Movable || ( resp.Item is Container && ( (Container)resp.Item ).Items.Count != 0 ) )
					continue;

				foreach ( IShopSellInfo ssi in info )
				{
					if ( ssi.IsSellable( resp.Item ) )
					{
						Sold++;
						break;
					}
				}
			}

			if ( Sold > BaseVendorConstants.MAX_SELL_ITEMS )
			{
				SayTo( seller, true, BaseVendorStringConstants.SELL_LIMIT_FORMAT, BaseVendorConstants.MAX_SELL_ITEMS );
				return false;
			}
			else if ( Sold == 0 )
			{
				return true;
			}

			foreach ( SellItemResponse resp in list )
			{
				if ( resp.Item.RootParent != seller || resp.Amount <= 0 || !resp.Item.IsStandardLoot() || !resp.Item.Movable || ( resp.Item is Container && ( (Container)resp.Item ).Items.Count != 0 ) )
					continue;

				if ( BeggingPose(seller) > 0 ) // LET US SEE IF THEY ARE BEGGING
				{
					Titles.AwardKarma( seller, -BeggingKarma( seller ), true );
				}

				PlayerMobile pm = (PlayerMobile)seller;

				foreach ( IShopSellInfo ssi in info )
				{
					if ( ssi.IsSellable( resp.Item ) )
					{
						int amount = resp.Amount;

						if ( amount > resp.Item.Amount )
							amount = resp.Item.Amount;

						if ( ssi.IsResellable( resp.Item ) )
						{
							bool found = false;

							foreach ( IBuyItemInfo bii in buyInfo )
							{
								if ( bii.Restock( resp.Item, amount ) )
								{
									resp.Item.Consume( amount );
									found = true;

									break;
								}
							}

							if ( !found )
							{
								cont = this.BuyPack;

								if ( amount < resp.Item.Amount )
								{
									Item item = Mobile.LiftItemDupe( resp.Item, resp.Item.Amount - amount );

									if ( item != null )
									{
										item.SetLastMoved();
										cont.DropItem( item );
									}
									else
									{
										resp.Item.SetLastMoved();
										cont.DropItem( item );
									}
								}
								else
								{
									resp.Item.SetLastMoved();
									cont.DropItem( resp.Item );
								}
							}
						}
						else
						{
							if ( amount < resp.Item.Amount )
								resp.Item.Amount -= amount;
							else
								resp.Item.Delete();
						}

						int GuildMember;
						int barter = VendorBarterHelper.CalculateBarterSkill(this, seller, out GuildMember);
						
						if (BeggingPose(seller) > 0 && GuildMember == 0)
						{
							seller.CheckSkill(SkillName.Begging, 0, BaseVendorConstants.GUILD_BARTER_CAP);
						}
						
						int money = ssi.GetSellPriceFor( resp.Item, barter );

						if (!m_sellingpriceadjusted)
						{
							money = VendorPriceManager.CalculateSellPrice(money, resp.Item, this, seller, barter);
							m_sellingpriceadjusted = true;
						}

						GiveGold += money * amount;
						break;
					}
				}
			}

			if ( GiveGold > 0 )
			{
				if ( GiveGold > BaseVendorConstants.BANK_CHECK_THRESHOLD )
					seller.AddToBackpack( new BankCheck( GiveGold ) );
				else
					seller.AddToBackpack( new Gold( GiveGold ) );

				seller.PlaySound( BaseVendorConstants.SOUND_SELL );//Gold dropping sound

				if ( SupportsBulkOrders( seller ) )
				{
					Item bulkOrder = CreateBulkOrder( seller, false );

					if ( bulkOrder is LargeBOD )
						seller.SendGump( new LargeBODAcceptGump( seller, (LargeBOD)bulkOrder ) );
					else if ( bulkOrder is SmallBOD )
						seller.SendGump( new SmallBODAcceptGump( seller, (SmallBOD)bulkOrder ) );
				}
			}

			return true;
		}

		#endregion

		#region Properties and Display

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			string sTitle = GetPlayerInfo.GetNPCGuild( this );
			if ( sTitle != "" ){ list.Add( Utility.FixHtml( sTitle ) ); }

			if (this is Blacksmith || this is BlacksmithGuildmaster || this is Tailor || this is TailorGuildmaster)
			{
				list.Add(BaseVendorStringConstants.PROPERTY_BULK_ORDERS);
				list.Add(BaseVendorStringConstants.PROPERTY_REDEEM_CREDITS);
			}

		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)BaseVendorConstants.SERIALIZATION_VERSION ); // version

			List<SBInfo> sbInfos = this.SBInfos;

			for ( int i = 0; sbInfos != null && i < sbInfos.Count; ++i )
			{
				SBInfo sbInfo = sbInfos[i];
				List<GenericBuyInfo> buyInfo = sbInfo.BuyInfo;

				for ( int j = 0; buyInfo != null && j < buyInfo.Count; ++j )
				{
					GenericBuyInfo gbi = (GenericBuyInfo)buyInfo[j];

					int maxAmount = gbi.MaxAmount;
					int doubled = 0;

					if ( doubled > 0 )
					{
						writer.WriteEncodedInt( 1 + ( ( j * sbInfos.Count ) + i ) );
						writer.WriteEncodedInt( doubled );
					}
				}
			}
			writer.WriteEncodedInt( 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			LoadSBInfo();

			List<SBInfo> sbInfos = this.SBInfos;

			switch ( version )
			{
				case 1:
					{
						int index;

						while ( ( index = reader.ReadEncodedInt() ) > 0 )
						{
							int doubled = reader.ReadEncodedInt();

							if ( sbInfos != null )
							{
								index -= 1;
								int sbInfoIndex = index % sbInfos.Count;
								int buyInfoIndex = index / sbInfos.Count;

								if ( sbInfoIndex >= 0 && sbInfoIndex < sbInfos.Count )
								{
									SBInfo sbInfo = sbInfos[sbInfoIndex];
									List<GenericBuyInfo> buyInfo = sbInfo.BuyInfo;

									if ( buyInfo != null && buyInfoIndex >= 0 && buyInfoIndex < buyInfo.Count )
									{
										GenericBuyInfo gbi = (GenericBuyInfo)buyInfo[buyInfoIndex];

										int amount = BaseVendorConstants.DESERIALIZE_DEFAULT_AMOUNT;

										gbi.Amount = gbi.MaxAmount = amount;
									}
								}
							}
						}

						break;
					}
			}

			if ( IsParagon )
				IsParagon = false;

			if ( Core.AOS && NameHue == 0x35 )
				NameHue = -1;
		}

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive && IsActiveVendor )
			{
				if ( SupportsBulkOrders( from ) )
					list.Add( new BulkOrderInfoEntry( from, this ) );
				
				if ( MyServerSettings.buysellcontext() )
				{
					if ( IsActiveSeller )
						list.Add( new VendorBuyEntry( from, this ) );

					if ( IsActiveBuyer )
						list.Add( new VendorSellEntry( from, this ) );
				}
			}

			if (
				( from.Skills[SkillName.Forensics].Value >= 50 && ( this is NecroMage || this is Witches || this is Necromancer || this is NecromancerGuildmaster ) ) || 
				( from.Skills[SkillName.Alchemy].Value >= 40 && from.Skills[SkillName.Cooking].Value >= 40 && from.Skills[SkillName.AnimalLore].Value >= 40 && ( this is Herbalist || this is DruidTree || this is Druid || this is DruidGuildmaster ) ) || 
				( from.Skills[SkillName.Alchemy].Value >= 50 && ( this is Alchemist || this is AlchemistGuildmaster ) ) || 
				( from.Skills[SkillName.Blacksmith].Value >= 50 && ( this is Blacksmith || this is BlacksmithGuildmaster ) ) || 
				( from.Skills[SkillName.Fletching].Value >= 50 && ( this is Bowyer || this is ArcherGuildmaster ) ) || 
				( from.Skills[SkillName.Carpentry].Value >= 50 && ( this is Carpenter || this is CarpenterGuildmaster ) ) || 
				( from.Skills[SkillName.Cartography].Value >= 50 && ( this is Mapmaker || this is CartographersGuildmaster ) ) || 
				( from.Skills[SkillName.Cooking].Value >= 50 && ( this is Cook || this is Baker || this is CulinaryGuildmaster ) ) || 
				( from.Skills[SkillName.Inscribe].Value >= 50 && ( this is Scribe || this is Sage || this is LibrarianGuildmaster ) ) || 
				( from.Skills[SkillName.Tailoring].Value >= 50 && ( this is Weaver || this is Tailor || this is LeatherWorker || this is TailorGuildmaster ) ) || 
				( from.Skills[SkillName.Tinkering].Value >= 50 && ( this is Tinker || this is TinkerGuildmaster ) ) 
			)
			{
				list.Add( new SetupShoppeEntry( from, this ) );
			}

			base.AddCustomContextEntries( from, list );
		}

		#endregion

		#region Shop Info Management

		public virtual IShopSellInfo[] GetSellInfo()
		{
			return (IShopSellInfo[])m_ArmorSellInfo.ToArray( typeof( IShopSellInfo ) );
		}

		public virtual IBuyItemInfo[] GetBuyInfo()
		{
			return (IBuyItemInfo[])m_ArmorBuyInfo.ToArray( typeof( IBuyItemInfo ) );
		}

		#endregion

		#region Damage and Combat

		public override bool CanBeDamaged()
		{
			return !IsInvulnerable;
		}

		#endregion
	}
}

namespace Server.ContextMenus
{
	public class SetupShoppeEntry : ContextMenuEntry
	{
		private BaseVendor m_Vendor;
		private Mobile m_From;
		
		public SetupShoppeEntry( Mobile from, BaseVendor vendor ) : base( BaseVendorConstants.CONTEXT_MENU_SETUP_SHOPPE, BaseVendorConstants.CONTEXT_MENU_RANGE_SETUP )
		{
			m_Vendor = vendor;
			m_From = from;
			Enabled = vendor.CheckVendorAccess( from );
		}

		public override void OnClick()
		{
			if ( !m_From.HasGump( typeof( Server.Items.ExplainShopped ) ) )
			{
				m_From.SendGump( new Server.Items.ExplainShopped( m_From, m_Vendor ) );
			}
		}
	}

	public class VendorBuyEntry : ContextMenuEntry
	{
		private BaseVendor m_Vendor;

		public VendorBuyEntry( Mobile from, BaseVendor vendor ) : base( BaseVendorConstants.CONTEXT_MENU_BUY, BaseVendorConstants.CONTEXT_MENU_RANGE_BUY_SELL )
		{
			m_Vendor = vendor;
			Enabled = vendor.CheckVendorAccess( from );
		}

		public override void OnClick()
		{
			m_Vendor.VendorBuy( this.Owner.From );
		}
	}

	public class VendorSellEntry : ContextMenuEntry
	{
		private BaseVendor m_Vendor;

		public VendorSellEntry( Mobile from, BaseVendor vendor ) : base( BaseVendorConstants.CONTEXT_MENU_SELL, BaseVendorConstants.CONTEXT_MENU_RANGE_BUY_SELL )
		{
			m_Vendor = vendor;
			Enabled = vendor.CheckVendorAccess( from );
		}

		public override void OnClick()
		{
			m_Vendor.VendorSell( this.Owner.From );
		}
	}
}

namespace Server
{
	public interface IShopSellInfo
	{
		//get display name for an item
		string GetNameFor( Item item );

		//get price for an item which the player is selling
		int GetSellPriceFor( Item item, int barter );

		//get price for an item which the player is buying
		int GetBuyPriceFor( Item item );

		//can we sell this item to this vendor?
		bool IsSellable( Item item );

		//What do we sell?
		Type[] Types { get; }

		//does the vendor resell this item?
		bool IsResellable( Item item );
	}

	public interface IBuyItemInfo
	{
		//get a new instance of an object (we just bought it)
		IEntity GetEntity();

		int ControlSlots { get; }

		int PriceScalar { get; set; }

		//display price of the item
		int Price { get; set; }

		//display name of the item
		string Name { get; }

		//display hue
		int Hue { get; }

		//display id
		int ItemID { get; }

		//amount in stock
		int Amount { get; set; }

		//max amount in stock
		int MaxAmount { get; }

		//Attempt to Restock with item, (return true if Restock sucessful)
		bool Restock( Item item, int amount );

		//called when its time for the whole shop to Restock
		void OnRestock();
	}
}

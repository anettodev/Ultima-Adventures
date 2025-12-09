using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
using Server.Gumps;

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

		// Caching fields for performance optimization
		private bool? m_isInMidland;
		private bool m_midlandCacheValid;

		public override bool CanTeach { get { return false; } }
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

				//SetSkill(SkillName.Swords, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				//SetSkill(SkillName.Fencing, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				//SetSkill(SkillName.Macing, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				//SetSkill(SkillName.EvalInt, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				//SetSkill(SkillName.Healing, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				//SetSkill(SkillName.Parry, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);

				SetSkill(SkillName.Anatomy, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.Camping, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.DetectHidden, BaseVendorConstants.DETECT_HIDDEN_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.Magery, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.MagicResist, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.Tactics, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);
				SetSkill(SkillName.Wrestling, BaseVendorConstants.SKILL_MIN, BaseVendorConstants.SKILL_MAX);

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

			if (IsInMidlandCached())
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
			// Invalidate caches when moving
			InvalidateMidlandCache();

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

			if (IsInMidlandCached())
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

				SayTo( from, BaseVendorConstants.UO_MSG_GREETINGS ); // Greetings.  Have a look around.
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

			if (IsInMidlandCached())
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

		/// <summary>
		/// Handles item drops on the vendor, processing various special items and transactions
		/// </summary>
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			// Check for training gold deposits first
			if (dropped is Gold && m_TrainingDeposits.ContainsKey(from.Serial))
			{
				return HandleTrainingGoldDeposit(from, (Gold)dropped);
			}

			if (!ValidateInteraction(from))
			{
				return false;
			}

			PlayerMobile pm;
			if (ValidatePlayerMobile(from, out pm))
			{

				// Check for tinker items first
				if (HandleTinkerItems(from, dropped)) return true;

				// Check for relic items
				if (HandleRelicItems(from, pm, dropped)) return true;

				// Check for cargo and museum items
				if (HandleCargoAndMuseumItems(from, dropped)) return true;

				// Check for magic carpets
				if (HandleMagicCarpets(from, dropped)) return true;

				// Check for map purchases
				if (HandleMapPurchases(from, dropped)) return true;

				// Check for currency exchange
				if (HandleCurrencyExchange(from, dropped)) return true;

				// Check for bottle of parts
				if (HandleBottleOfParts(from, pm, dropped)) return true;

				// Check for tomb raid artifacts
				if (HandleTombRaidArtifacts(from, pm, dropped)) return true;

				// Check for thief items
				if (HandleThiefItems(from, pm, dropped)) return true;

				// Check for henchman items
				if (HandleHenchmanItems(from, dropped)) return true;

				// Check for cursed items
				if (HandleCursedItems(from, dropped)) return true;

				// Check for dirty items
				if (HandleDirtyItems(from, dropped)) return true;

				// Check for weeded items
				if (HandleWeededItems(from, dropped)) return true;

				// Check for bulk order items
				if (HandleBulkOrderItems(from, dropped)) return true;
			}

			return base.OnDragDrop( from, dropped );
		}

		#endregion

		#region Validation Methods

		/// <summary>
		/// Validates if a mobile can interact with this vendor
		/// </summary>
		/// <param name="from">The mobile attempting to interact</param>
		/// <returns>True if the mobile can interact, false otherwise</returns>
		private bool ValidateInteraction(Mobile from)
		{
			if (from.Blessed)
			{
				string sSay = BaseVendorStringConstants.ERROR_BLESSED;
				this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sSay, from.NetState);
				return false;
			}

			if (IntelligentAction.GetMyEnemies(from, this, false) == true)
			{
				string sSay = BaseVendorStringConstants.ERROR_ENEMY;
				this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sSay, from.NetState);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Validates if the mobile is a player mobile
		/// </summary>
		/// <param name="from">The mobile to validate</param>
		/// <param name="pm">Output parameter for the PlayerMobile if valid</param>
		/// <returns>True if the mobile is a valid PlayerMobile, false otherwise</returns>
		private bool ValidatePlayerMobile(Mobile from, out PlayerMobile pm)
		{
			pm = from as PlayerMobile;
			return pm != null;
		}

		#endregion

		#region Caching Methods

		/// <summary>
		/// Gets cached Midland status for this vendor
		/// </summary>
		/// <returns>True if the vendor is in Midland region</returns>
		private bool IsInMidlandCached()
		{
			if (!m_midlandCacheValid)
			{
				m_isInMidland = AdventuresFunctions.IsInMidland((object)this);
				m_midlandCacheValid = true;
			}
			return m_isInMidland.Value;
		}

		/// <summary>
		/// Invalidates the Midland cache (call when vendor moves regions)
		/// </summary>
		private void InvalidateMidlandCache()
		{
			m_midlandCacheValid = false;
			m_isInMidland = null;
		}

		/// <summary>
		/// Gets a PlayerMobile reference with proper casting
		/// </summary>
		/// <param name="from">The mobile to cast</param>
		/// <returns>PlayerMobile instance or null if not a player</returns>
		private PlayerMobile GetPlayerMobile(Mobile from)
		{
			return from as PlayerMobile;
		}

		#endregion

		#region Item Drop Handlers

		/// <summary>
		/// Handles tinker-related items (GolemManual, OrbOfTheAbyss, RobotSchematics)
		/// </summary>
		private bool HandleTinkerItems(Mobile from, Item dropped)
		{
			if (dropped is GolemManual && (this is Tinker || this is TinkerGuildmaster))
			{
				Server.Items.GolemManual.ProcessGolemBook(from, this, dropped);
				return true;
			}
			else if (dropped is OrbOfTheAbyss && (this is Tinker || this is TinkerGuildmaster))
			{
				Server.Items.OrbOfTheAbyss.ChangeOrb(from, this, dropped);
				return true;
			}
			else if (dropped is RobotSchematics && (this is Tinker || this is TinkerGuildmaster))
			{
				Server.Items.RobotSchematics.ProcessRobotBook(from, this, dropped);
				return true;
			}
			else if (dropped is AlienEgg && (this is AnimalTrainer || this is Veterinarian))
			{
				Server.Items.AlienEgg.ProcessAlienEgg(from, this, dropped);
				return true;
			}
			else if (dropped is EarthyEgg && (this is AnimalTrainer || this is Veterinarian))
			{
				Server.Items.EarthyEgg.ProcessEarthyEgg(from, this, dropped);
				return true;
			}
			else if (dropped is CorruptedEgg && (this is AnimalTrainer || this is Veterinarian))
			{
				Server.Items.CorruptedEgg.ProcessCorruptedEgg(from, this, dropped);
				return true;
			}
			else if (dropped is FeyEgg && (this is AnimalTrainer || this is Veterinarian))
			{
				Server.Items.FeyEgg.ProcessFeyEgg(from, this, dropped);
				return true;
			}
			else if (dropped is PrehistoricEgg && (this is AnimalTrainer || this is Veterinarian))
			{
				Server.Items.PrehistoricEgg.ProcessPrehistoricEgg(from, this, dropped);
				return true;
			}
			else if (dropped is ReptilianEgg && (this is AnimalTrainer || this is Veterinarian))
			{
				Server.Items.ReptilianEgg.ProcessReptilianEgg(from, this, dropped);
				return true;
			}
			else if (dropped is DragonEgg && (this is AnimalTrainer || this is Veterinarian))
			{
				Server.Items.DragonEgg.ProcessDragonEgg(from, this, dropped);
				return true;
			}
			else if (dropped is DracolichSkull && (this is NecromancerGuildmaster))
			{
				Server.Items.DracolichSkull.ProcessDracolichSkull(from, this, dropped);
				return true;
			}
			else if (dropped is DemonPrison && (this is NecromancerGuildmaster || this is MageGuildmaster || this is Mage || this is NecroMage || this is Necromancer || this is Witches))
			{
				Server.Items.DemonPrison.ProcessDemonPrison(from, this, dropped);
				return true;
			}
			else if (this is Waiter && dropped is CorpseItem)
			{
				return HandleCoffeeCorpse(from, dropped);
			}

			return false;
		}

		/// <summary>
		/// Handles coffee corpse processing for waiters
		/// </summary>
		private bool HandleCoffeeCorpse(Mobile from, Item dropped)
		{
			string corpsename = dropped.Name;
			if (Insensitive.Contains(corpsename, BaseVendorStringConstants.ITEM_TYPE_COFFEE) ||
				Insensitive.Contains(corpsename, BaseVendorStringConstants.ITEM_TYPE_ESPRESSO) ||
				Insensitive.Contains(corpsename, BaseVendorStringConstants.ITEM_TYPE_AMERICANO) ||
				Insensitive.Contains(corpsename, BaseVendorStringConstants.ITEM_TYPE_CAPPUCINO))
			{
				dropped.Delete();
				Coffee cof = new Coffee();
				from.Backpack.DropItem(cof);
				this.Say(BaseVendorStringConstants.COFFEE_ACCEPTANCE);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Handles relic item processing with rewards
		/// </summary>
		private bool HandleRelicItems(Mobile from, PlayerMobile pm, Item dropped)
		{
			int relicValue = Server.Misc.RelicItems.RelicValue(dropped, this);
			if (relicValue <= 0) return false;

			int guildMember = 0;
			int gBonus = (int)Math.Round(((from.Skills[SkillName.ItemID].Value * relicValue) / 100), 0);

			if (this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild)
			{
				gBonus = gBonus + (int)Math.Round(((100.00 * relicValue) / 100), 0);
				guildMember = 1;
			}

			if (BeggingPose(from) > 0 && guildMember == 0)
			{
				Titles.AwardKarma(from, -BeggingKarma(from), true);
				from.Say(BeggingWords());
				gBonus = (int)Math.Round(((from.Skills[SkillName.Begging].Value * relicValue) / 100), 0);
			}

			gBonus = gBonus + relicValue;
			from.SendSound(BaseVendorConstants.SOUND_RELIC);
			from.AddToBackpack(new Gold(gBonus));

			string sMessage = GetRandomRelicRewardMessage(gBonus);
			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Gets a random relic reward message
		/// </summary>
		private string GetRandomRelicRewardMessage(int goldAmount)
		{
			switch (Utility.RandomMinMax(0, BaseVendorConstants.RANDOM_REWARD_MESSAGE_COUNT - 1))
			{
				case 0: return string.Format(BaseVendorStringConstants.RELIC_REWARD_1_FORMAT, goldAmount.ToString());
				case 1: return string.Format(BaseVendorStringConstants.RELIC_REWARD_2_FORMAT, goldAmount.ToString());
				case 2: return string.Format(BaseVendorStringConstants.RELIC_REWARD_3_FORMAT, goldAmount.ToString());
				case 3: return string.Format(BaseVendorStringConstants.RELIC_REWARD_4_FORMAT, goldAmount.ToString());
				case 4: return string.Format(BaseVendorStringConstants.RELIC_REWARD_5_FORMAT, goldAmount.ToString());
				case 5: return string.Format(BaseVendorStringConstants.RELIC_REWARD_6_FORMAT, goldAmount.ToString());
				case 6: return string.Format(BaseVendorStringConstants.RELIC_REWARD_7_FORMAT, goldAmount.ToString());
				case 7: return string.Format(BaseVendorStringConstants.RELIC_REWARD_8_FORMAT, goldAmount.ToString());
				case 8: return string.Format(BaseVendorStringConstants.RELIC_REWARD_9_FORMAT, goldAmount.ToString());
				case 9: return string.Format(BaseVendorStringConstants.RELIC_REWARD_10_FORMAT, goldAmount.ToString());
				default: return string.Format(BaseVendorStringConstants.RELIC_REWARD_1_FORMAT, goldAmount.ToString());
			}
		}

		/// <summary>
		/// Handles cargo and museum item processing
		/// </summary>
		private bool HandleCargoAndMuseumItems(Mobile from, Item dropped)
		{
			if (dropped is Cargo)
			{
				Server.Items.Cargo.GiveCargo((Cargo)dropped, this, from);
				return true;
			}
			else if (dropped is Museums && this is VarietyDealer)
			{
				Server.Items.Museums.GiveAntique((Museums)dropped, this, from);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Handles magic carpet upgrades for tailors
		/// </summary>
		private bool HandleMagicCarpets(Mobile from, Item dropped)
		{
			if (!Server.Multis.BaseBoat.isRolledCarpet(dropped) || !(this is Tailor || this is TailorGuildmaster))
				return false;

			Item carpet = null;

			if (dropped is MagicDockedCarpetA || dropped is MagicCarpetADeed) { carpet = new MagicCarpetBDeed(); }
			else if (dropped is MagicDockedCarpetB || dropped is MagicCarpetBDeed) { carpet = new MagicCarpetCDeed(); }
			else if (dropped is MagicDockedCarpetC || dropped is MagicCarpetCDeed) { carpet = new MagicCarpetDDeed(); }
			else if (dropped is MagicDockedCarpetD || dropped is MagicCarpetDDeed) { carpet = new MagicCarpetEDeed(); }
			else if (dropped is MagicDockedCarpetE || dropped is MagicCarpetEDeed) { carpet = new MagicCarpetFDeed(); }
			else if (dropped is MagicDockedCarpetF || dropped is MagicCarpetFDeed) { carpet = new MagicCarpetGDeed(); }
			else if (dropped is MagicDockedCarpetG || dropped is MagicCarpetGDeed) { carpet = new MagicCarpetHDeed(); }
			else if (dropped is MagicDockedCarpetH || dropped is MagicCarpetHDeed) { carpet = new MagicCarpetIDeed(); }
			else if (dropped is MagicDockedCarpetI || dropped is MagicCarpetIDeed) { carpet = new MagicCarpetADeed(); }

			if (carpet != null)
			{
				dropped.Delete();
				carpet.Hue = dropped.Hue;
				from.AddToBackpack(carpet);
				SayTo(from, BaseVendorStringConstants.CARPET_ALTERED);
				Effects.PlaySound(from.Location, from.Map, BaseVendorConstants.SOUND_CARPET);
				return true;
			}

			return false;
		}

		/// <summary>
		/// Handles map purchases from mapmakers
		/// </summary>
		private bool HandleMapPurchases(Mobile from, Item dropped)
		{
			if (!(dropped is Gold) || !(this is Mapmaker) || dropped.Amount != BaseVendorConstants.MAP_COST)
				return false;

			Item map = GetMapForLocation(from);
			from.AddToBackpack(map);

			string sMessage = BaseVendorStringConstants.MAP_PURCHASE;
			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Gets the appropriate map item based on player location
		/// </summary>
		private Item GetMapForLocation(Mobile from)
		{
			if (from.Map == Map.Trammel && from.X > 5124 && from.Y > 3041 && from.X < 6147 && from.Y < 4092)
				return new WorldMapAmbrosia();
			else if (from.Map == Map.Trammel && from.X > 859 && from.Y > 3181 && from.X < 2133 && from.Y < 4092)
				return new WorldMapUmber();
			else if (from.Map == Map.Malas && from.X < 1870)
				return new WorldMapSerpent();
			else if (from.Map == Map.Tokuno)
				return new WorldMapIslesOfDread();
			else if (from.Map == Map.TerMur && from.X > 132 && from.Y > 4 && from.X < 1165 && from.Y < 1798)
				return new WorldMapSavage();
			else if (from.Map == Map.Trammel && from.X < 6125 && from.Y < 824 && from.X < 7175 && from.Y < 2746)
				return new WorldMapBottle();
			else if (from.Map == Map.Felucca && from.X < 5420 && from.Y < 4096)
				return new WorldMapLodor();
			else
				return new WorldMapSosaria();
		}

		/// <summary>
		/// Handles currency exchange for bankers and minters
		/// </summary>
		private bool HandleCurrencyExchange(Mobile from, Item dropped)
		{
			if (!(this is Minter || this is Banker)) return false;

			if (dropped is DDCopper || dropped is DDSilver)
				return HandleCopperSilverExchange(from, dropped);
			else if (dropped is DDXormite)
				return HandleXormiteExchange(from, dropped);
			else if (dropped is Crystals)
				return HandleCrystalsExchange(from, dropped);
			else if (dropped is DDJewels)
				return HandleJewelsExchange(from, dropped);
			else if (dropped is DDGemstones)
				return HandleGemstonesExchange(from, dropped);
			else if (dropped is DDGoldNuggets)
				return HandleGoldNuggetsExchange(from, dropped);

			return false;
		}

		/// <summary>
		/// Handles copper and silver coin exchange
		/// </summary>
		private bool HandleCopperSilverExchange(Mobile from, Item dropped)
		{
			int nRate = (dropped is DDCopper) ? BaseVendorConstants.EXCHANGE_RATE_COPPER : BaseVendorConstants.EXCHANGE_RATE_SILVER;
			string sCoin = (dropped is DDCopper) ? BaseVendorStringConstants.CURRENCY_TYPE_COPPER : BaseVendorStringConstants.CURRENCY_TYPE_SILVER;

			int nCoins = dropped.Amount;
			int nGold = (int)Math.Floor((decimal)(dropped.Amount / nRate));
			int nChange = dropped.Amount - (nGold * nRate);

			string sMessage = (nGold > 0 && nChange > 0)
				? string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_WITH_CHANGE_FORMAT, nGold.ToString(), nChange.ToString(), sCoin)
				: (nGold > 0)
					? string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_FORMAT, nGold.ToString())
					: BaseVendorStringConstants.CURRENCY_ERROR_NOT_ENOUGH;

			if (nGold > 0)
				from.AddToBackpack(new Gold(nGold));

			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);

			if (nChange > 0)
			{
				if (dropped is DDCopper)
					from.AddToBackpack(new DDCopper(nChange));
				else if (dropped is DDSilver)
					from.AddToBackpack(new DDSilver(nChange));
			}

			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Handles xormite currency exchange
		/// </summary>
		private bool HandleXormiteExchange(Mobile from, Item dropped)
		{
			int nGold = dropped.Amount * BaseVendorConstants.CURRENCY_MULT_XORMITE;
			string sMessage = string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_FORMAT, nGold.ToString());
			from.AddToBackpack(new Gold(nGold));
			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Handles crystals currency exchange
		/// </summary>
		private bool HandleCrystalsExchange(Mobile from, Item dropped)
		{
			int nGold = dropped.Amount * BaseVendorConstants.CURRENCY_MULT_CRYSTALS;
			string sMessage = string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_FORMAT, nGold.ToString());
			from.AddToBackpack(new Gold(nGold));
			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Handles jewels currency exchange
		/// </summary>
		private bool HandleJewelsExchange(Mobile from, Item dropped)
		{
			int nGold = dropped.Amount * BaseVendorConstants.CURRENCY_MULT_JEWELS;
			string sMessage = string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_FORMAT, nGold.ToString());
			from.AddToBackpack(new Gold(nGold));
			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Handles gemstones currency exchange
		/// </summary>
		private bool HandleGemstonesExchange(Mobile from, Item dropped)
		{
			int nGold = dropped.Amount * BaseVendorConstants.CURRENCY_MULT_GEMSTONES;
			string sMessage = string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_FORMAT, nGold.ToString());
			from.AddToBackpack(new Gold(nGold));
			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Handles gold nuggets currency exchange
		/// </summary>
		private bool HandleGoldNuggetsExchange(Mobile from, Item dropped)
		{
			int nGold = dropped.Amount;
			string sMessage = string.Format(BaseVendorStringConstants.CURRENCY_EXCHANGE_FORMAT, nGold.ToString());
			from.AddToBackpack(new Gold(nGold));
			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Handles bottle of parts processing for alchemists
		/// </summary>
		private bool HandleBottleOfParts(Mobile from, PlayerMobile pm, Item dropped)
		{
			if (!(dropped is BottleOfParts) || !(this is Alchemist || this is Witches))
				return false;

			int guildMember = 0;
			int iForensics = (int)from.Skills[SkillName.Forensics].Value / BaseVendorConstants.SKILL_DIVISOR;
			int iAnatomy = (int)from.Skills[SkillName.Anatomy].Value / BaseVendorConstants.SKILL_DIVISOR;
			int nBottle = Utility.RandomMinMax(BaseVendorConstants.BOTTLE_MIN, BaseVendorConstants.BOTTLE_MAX) + Utility.RandomMinMax(0, iForensics) + Utility.RandomMinMax(0, iAnatomy);

			int gBonus = (int)Math.Round(((from.Skills[SkillName.ItemID].Value * nBottle) / 100), 0);

			if (this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild)
			{
				gBonus = gBonus + (int)Math.Round(((100.0 * nBottle) / 100), 0);
				guildMember = 1;
			}

			if (BeggingPose(from) > 0 && guildMember == 0)
			{
				Titles.AwardKarma(from, -BeggingKarma(from), true);
				from.Say(BeggingWords());
				gBonus = (int)Math.Round(((from.Skills[SkillName.Begging].Value * nBottle) / 100), 0);
			}

			gBonus = (gBonus + nBottle) * dropped.Amount;
			from.AddToBackpack(new Gold(gBonus));

			string sMessage = GetRandomBottleRewardMessage(gBonus);
			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Gets a random bottle reward message
		/// </summary>
		private string GetRandomBottleRewardMessage(int goldAmount)
		{
			switch (Utility.RandomMinMax(0, BaseVendorConstants.RANDOM_REWARD_MESSAGE_COUNT - 1))
			{
				case 0: return string.Format(BaseVendorStringConstants.BOTTLE_REWARD_1_FORMAT, goldAmount.ToString());
				case 1: return string.Format(BaseVendorStringConstants.BOTTLE_REWARD_2_FORMAT, goldAmount.ToString());
				case 2: return string.Format(BaseVendorStringConstants.BOTTLE_REWARD_3_FORMAT, goldAmount.ToString());
				case 3: return string.Format(BaseVendorStringConstants.BOTTLE_REWARD_4_FORMAT, goldAmount.ToString());
				case 4: return string.Format(BaseVendorStringConstants.BOTTLE_REWARD_5_FORMAT, goldAmount.ToString());
				case 5: return string.Format(BaseVendorStringConstants.BOTTLE_REWARD_6_FORMAT, goldAmount.ToString());
				case 6: return string.Format(BaseVendorStringConstants.BOTTLE_REWARD_7_FORMAT, goldAmount.ToString());
				case 7: return string.Format(BaseVendorStringConstants.BOTTLE_REWARD_8_FORMAT, goldAmount.ToString());
				case 8: return string.Format(BaseVendorStringConstants.BOTTLE_REWARD_9_FORMAT, goldAmount.ToString());
				case 9: return string.Format(BaseVendorStringConstants.BOTTLE_REWARD_10_FORMAT, goldAmount.ToString());
				default: return string.Format(BaseVendorStringConstants.BOTTLE_REWARD_1_FORMAT, goldAmount.ToString());
			}
		}

		/// <summary>
		/// Handles tomb raid artifacts for thief guildmasters
		/// </summary>
		private bool HandleTombRaidArtifacts(Mobile from, PlayerMobile pm, Item dropped)
		{
			if (!(this is ThiefGuildmaster) || pm.NpcGuild != NpcGuild.ThievesGuild)
				return false;

			if (!IsTombRaidArtifact(dropped))
				return false;

			int tombRaid = GetTombRaidValue(dropped);
			from.AddToBackpack(new Gold(tombRaid));

			string sMessage = GetRandomTombRaidMessage(tombRaid);
			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);

			Titles.AwardFame(from, tombRaid / BaseVendorConstants.TOMB_RAID_FAME_DIVISOR, true);
			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Checks if an item is a tomb raid artifact
		/// </summary>
		private bool IsTombRaidArtifact(Item dropped)
		{
			return dropped is RockArtifact || dropped is SkullCandleArtifact || dropped is BottleArtifact ||
				   dropped is DamagedBooksArtifact || dropped is StretchedHideArtifact || dropped is BrazierArtifact ||
				   dropped is LampPostArtifact || dropped is BooksNorthArtifact || dropped is BooksWestArtifact ||
				   dropped is BooksFaceDownArtifact || dropped is StuddedLeggingsArtifact || dropped is EggCaseArtifact ||
				   dropped is SkinnedGoatArtifact || dropped is GruesomeStandardArtifact || dropped is BloodyWaterArtifact ||
				   dropped is TarotCardsArtifact || dropped is BackpackArtifact || dropped is StuddedTunicArtifact ||
				   dropped is CocoonArtifact || dropped is SkinnedDeerArtifact || dropped is SaddleArtifact ||
				   dropped is LeatherTunicArtifact || dropped is RuinedPaintingArtifact;
		}

		/// <summary>
		/// Gets the tomb raid value for an artifact
		/// </summary>
		private int GetTombRaidValue(Item dropped)
		{
			if (dropped is RockArtifact || dropped is SkullCandleArtifact || dropped is BottleArtifact || dropped is DamagedBooksArtifact)
				return BaseVendorConstants.TOMB_RAID_COMMON;
			else if (dropped is StretchedHideArtifact || dropped is BrazierArtifact)
				return BaseVendorConstants.TOMB_RAID_UNCOMMON;
			else if (dropped is LampPostArtifact || dropped is BooksNorthArtifact || dropped is BooksWestArtifact || dropped is BooksFaceDownArtifact)
				return BaseVendorConstants.TOMB_RAID_RARE;
			else if (dropped is StuddedTunicArtifact || dropped is CocoonArtifact)
				return BaseVendorConstants.TOMB_RAID_VERY_RARE;
			else if (dropped is SkinnedDeerArtifact)
				return BaseVendorConstants.TOMB_RAID_EXTREMELY_RARE;
			else if (dropped is SaddleArtifact || dropped is LeatherTunicArtifact)
				return BaseVendorConstants.TOMB_RAID_LEGENDARY;
			else if (dropped is RuinedPaintingArtifact)
				return BaseVendorConstants.TOMB_RAID_UNIQUE;
			else
				return BaseVendorConstants.TOMB_RAID_DEFAULT;
		}

		/// <summary>
		/// Gets a random tomb raid message
		/// </summary>
		private string GetRandomTombRaidMessage(int goldAmount)
		{
			switch (Utility.RandomMinMax(0, BaseVendorConstants.RANDOM_REWARD_MESSAGE_COUNT - 1))
			{
				case 0: return string.Format(BaseVendorStringConstants.TOMB_RAID_1_FORMAT, goldAmount.ToString());
				case 1: return string.Format(BaseVendorStringConstants.TOMB_RAID_2_FORMAT, goldAmount.ToString());
				case 2: return string.Format(BaseVendorStringConstants.TOMB_RAID_3_FORMAT, goldAmount.ToString());
				case 3: return string.Format(BaseVendorStringConstants.TOMB_RAID_4_FORMAT, goldAmount.ToString());
				case 4: return string.Format(BaseVendorStringConstants.TOMB_RAID_5_FORMAT, goldAmount.ToString());
				case 5: return string.Format(BaseVendorStringConstants.TOMB_RAID_6_FORMAT, goldAmount.ToString());
				case 6: return string.Format(BaseVendorStringConstants.TOMB_RAID_7_FORMAT, goldAmount.ToString());
				case 7: return string.Format(BaseVendorStringConstants.TOMB_RAID_8_FORMAT, goldAmount.ToString());
				case 8: return string.Format(BaseVendorStringConstants.TOMB_RAID_9_FORMAT, goldAmount.ToString());
				case 9: return string.Format(BaseVendorStringConstants.TOMB_RAID_10_FORMAT, goldAmount.ToString());
				default: return string.Format(BaseVendorStringConstants.TOMB_RAID_1_FORMAT, goldAmount.ToString());
			}
		}

		/// <summary>
		/// Handles thief items (steal boxes and chests)
		/// </summary>
		private bool HandleThiefItems(Mobile from, PlayerMobile pm, Item dropped)
		{
			if (!(this is Thief)) return false;

			int iAmThief = (int)from.Skills[SkillName.Stealing].Value;
			if (iAmThief < BaseVendorConstants.THIEF_SKILL_REQUIREMENT)
			{
				this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, BaseVendorStringConstants.THIEF_REJECTION, from.NetState);
				return false;
			}

			if (dropped is StealBox || dropped is StealMetalBox || dropped is StealBag)
				return HandleStealBox(from, pm, dropped);
			else if (dropped is StolenChest)
				return HandleStolenChest(from, pm, dropped);

			return false;
		}

		/// <summary>
		/// Handles steal box processing
		/// </summary>
		private bool HandleStealBox(Mobile from, PlayerMobile pm, Item dropped)
		{
			int guildMember = 0;
			int gBonus = (int)Math.Round(((from.Skills[SkillName.ItemID].Value * BaseVendorConstants.STEAL_BOX_BASE_VALUE) / 100), 0);

			if (this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild)
			{
				gBonus = gBonus + (int)Math.Round(((100.00 * BaseVendorConstants.STEAL_BOX_BASE_VALUE) / 100), 0);
				guildMember = 1;
			}

			if (BeggingPose(from) > 0 && guildMember == 0)
			{
				Titles.AwardKarma(from, -BeggingKarma(from), true);
				from.Say(BeggingWords());
				gBonus = (int)Math.Round(((from.Skills[SkillName.Begging].Value * BaseVendorConstants.STEAL_BOX_BASE_VALUE) / 100), 0);
			}

			gBonus = gBonus + BaseVendorConstants.STEAL_BOX_BASE_VALUE;
			from.AddToBackpack(new Gold(gBonus));

			string sMessage = GetRandomThiefMessage(gBonus);
			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Handles stolen chest processing
		/// </summary>
		private bool HandleStolenChest(Mobile from, PlayerMobile pm, Item dropped)
		{
			StolenChest sRipoff = (StolenChest)dropped;
			int vRipoff = sRipoff.ContainerValue;
			int guildMember = 0;

			int gBonus = (int)Math.Round(((from.Skills[SkillName.ItemID].Value * vRipoff) / 100), 0);

			if (this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild)
			{
				gBonus = gBonus + (int)Math.Round(((100.00 * vRipoff) / 100), 0);
				guildMember = 1;
			}

			if (BeggingPose(from) > 0 && guildMember == 0)
			{
				Titles.AwardKarma(from, -BeggingKarma(from), true);
				from.Say(BeggingWords());
				gBonus = (int)Math.Round(((from.Skills[SkillName.Begging].Value * vRipoff) / 100), 0);
			}

			gBonus = gBonus + vRipoff;
			from.AddToBackpack(new Gold(gBonus));

			string sMessage = GetRandomThiefMessage(gBonus);
			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Gets a random thief message
		/// </summary>
		private string GetRandomThiefMessage(int goldAmount)
		{
			switch (Utility.RandomMinMax(0, BaseVendorConstants.RANDOM_REWARD_MESSAGE_COUNT - 1))
			{
				case 0: return string.Format(BaseVendorStringConstants.THIEF_1_FORMAT, goldAmount.ToString());
				case 1: return string.Format(BaseVendorStringConstants.THIEF_2_FORMAT, goldAmount.ToString());
				case 2: return string.Format(BaseVendorStringConstants.THIEF_3_FORMAT, goldAmount.ToString());
				case 3: return string.Format(BaseVendorStringConstants.THIEF_4_FORMAT, goldAmount.ToString());
				case 4: return string.Format(BaseVendorStringConstants.THIEF_5_FORMAT, goldAmount.ToString());
				case 5: return string.Format(BaseVendorStringConstants.THIEF_6_FORMAT, goldAmount.ToString());
				case 6: return string.Format(BaseVendorStringConstants.THIEF_7_FORMAT, goldAmount.ToString());
				case 7: return string.Format(BaseVendorStringConstants.THIEF_8_FORMAT, goldAmount.ToString());
				case 8: return string.Format(BaseVendorStringConstants.THIEF_9_FORMAT, goldAmount.ToString());
				case 9: return string.Format(BaseVendorStringConstants.THIEF_10_FORMAT, goldAmount.ToString());
				default: return string.Format(BaseVendorStringConstants.THIEF_1_FORMAT, goldAmount.ToString());
			}
		}

		/// <summary>
		/// Handles henchman item processing for innkeepers
		/// </summary>
		private bool HandleHenchmanItems(Mobile from, Item dropped)
		{
			if (!(this is InnKeeper || this is TavernKeeper || this is Barkeeper)) return false;
			if (!(dropped is HenchmanFighterItem || dropped is HenchmanArcherItem || dropped is HenchmanWizardItem)) return false;

			int fairTrade = 1;
			string sMessage = GetRandomHenchmanMessage();

			if (dropped is HenchmanFighterItem)
			{
				HenchmanFighterItem myFollower = (HenchmanFighterItem)dropped;
				if (myFollower.HenchDead > 0)
				{
					fairTrade = 0;
				}
				else
				{
					HenchmanFighterItem newFollower = new HenchmanFighterItem();
					newFollower.HenchTimer = myFollower.HenchTimer;
					newFollower.HenchBandages = myFollower.HenchBandages;
					from.AddToBackpack(newFollower);
				}
			}
			else if (dropped is HenchmanWizardItem)
			{
				HenchmanWizardItem myFollower = (HenchmanWizardItem)dropped;
				if (myFollower.HenchDead > 0)
				{
					fairTrade = 0;
				}
				else
				{
					HenchmanWizardItem newFollower = new HenchmanWizardItem();
					newFollower.HenchTimer = myFollower.HenchTimer;
					newFollower.HenchBandages = myFollower.HenchBandages;
					from.AddToBackpack(newFollower);
				}
			}
			else if (dropped is HenchmanArcherItem)
			{
				HenchmanArcherItem myFollower = (HenchmanArcherItem)dropped;
				if (myFollower.HenchDead > 0)
				{
					fairTrade = 0;
				}
				else
				{
					HenchmanArcherItem newFollower = new HenchmanArcherItem();
					newFollower.HenchTimer = myFollower.HenchTimer;
					newFollower.HenchBandages = myFollower.HenchBandages;
					from.AddToBackpack(newFollower);
				}
			}

			if (fairTrade == 1)
			{
				this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, sMessage, from.NetState);
				dropped.Delete();
				return true;
			}
			else
			{
				this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, BaseVendorStringConstants.HENCHMAN_REJECTION, from.NetState);
				return false;
			}
		}

		/// <summary>
		/// Gets a random henchman message
		/// </summary>
		private string GetRandomHenchmanMessage()
		{
			switch (Utility.RandomMinMax(0, BaseVendorConstants.RANDOM_HENCHMAN_MESSAGE_COUNT - 1))
			{
				case 0: return BaseVendorStringConstants.HENCHMAN_1;
				case 1: return BaseVendorStringConstants.HENCHMAN_2;
				case 2: return BaseVendorStringConstants.HENCHMAN_3;
				case 3: return BaseVendorStringConstants.HENCHMAN_4;
				case 4: return BaseVendorStringConstants.HENCHMAN_5;
				case 5: return BaseVendorStringConstants.HENCHMAN_6;
				case 6: return BaseVendorStringConstants.HENCHMAN_7;
				case 7: return BaseVendorStringConstants.HENCHMAN_8;
				default: return BaseVendorStringConstants.HENCHMAN_1;
			}
		}

		/// <summary>
		/// Handles cursed item processing for mages
		/// </summary>
		private bool HandleCursedItems(Mobile from, Item dropped)
		{
			if (dropped is BookBox && (this is Mage || this is KeeperOfChivalry || this is Witches || this is Necromancer || this is MageGuildmaster || this is HolyMage || this is NecroMage))
			{
				ExtractContainerContents(from, dropped);
				this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, BaseVendorStringConstants.CURSE_REMOVED_BOOKS, from.NetState);
				dropped.Delete();
				return true;
			}
			else if (dropped is CurseItem && (this is Mage || this is KeeperOfChivalry || this is Witches || this is Necromancer || this is MageGuildmaster || this is HolyMage || this is NecroMage))
			{
				ExtractContainerContents(from, dropped);
				string curseName = dropped.Name ?? BaseVendorStringConstants.DEFAULT_ITEM_NAME;
				string message = string.Format(BaseVendorStringConstants.CURSE_REMOVED_FORMAT, curseName);
				this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, message, from.NetState);
				dropped.Delete();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Handles dirty item processing for innkeepers
		/// </summary>
		private bool HandleDirtyItems(Mobile from, Item dropped)
		{
			if (!(this is InnKeeper || this is TavernKeeper || this is Barkeeper || this is Waiter)) return false;
			if (!(dropped is SewageItem || dropped is SlimeItem)) return false;

			ExtractContainerContents(from, dropped);
			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, BaseVendorStringConstants.ITEM_CLEANED, from.NetState);
			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Handles weeded item processing for alchemists
		/// </summary>
		private bool HandleWeededItems(Mobile from, Item dropped)
		{
			if (!(dropped is WeededItem) || !(this is Alchemist || this is Herbalist)) return false;

			ExtractContainerContents(from, dropped);
			this.PrivateOverheadMessage(MessageType.Regular, BaseVendorConstants.MESSAGE_HUE_DEFAULT, false, BaseVendorStringConstants.WEEDS_REMOVED, from.NetState);
			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Handles bulk order processing
		/// </summary>
		private bool HandleBulkOrderItems(Mobile from, Item dropped)
		{
			if (!(dropped is SmallBOD || dropped is LargeBOD)) return false;

			PlayerMobile pm = GetPlayerMobile(from);
			if (Core.ML && pm != null && pm.NextBODTurnInTime > DateTime.UtcNow)
			{
				SayTo(from, 1079976);
				return false;
			}

			if (!IsValidBulkOrder(dropped) || !SupportsBulkOrders(from))
			{
				SayTo(from, BaseVendorStringConstants.UO_MSG_WRONG_SHOPKEEPER);
				return false;
			}

			if ((dropped is SmallBOD && !((SmallBOD)dropped).Complete) || (dropped is LargeBOD && !((LargeBOD)dropped).Complete))
			{
				SayTo(from, BaseVendorStringConstants.UO_MSG_ORDER_NOT_COMPLETE);
				return false;
			}

			int creds = 0;

			if ((dropped is LargeSmithBOD || dropped is SmallSmithBOD) && from is PlayerMobile)
			{
				if (dropped is LargeSmithBOD)
					creds = SmithRewardCalculator.Instance.ComputePoints((LargeBOD)dropped);
				else
					creds = SmithRewardCalculator.Instance.ComputePoints((SmallBOD)dropped) / 3;

				if (pm != null) pm.BlacksmithBOD += creds;
			}
			else if ((dropped is LargeTailorBOD || dropped is SmallTailorBOD) && from is PlayerMobile)
			{
				if (dropped is LargeTailorBOD)
					creds = TailorRewardCalculator.Instance.ComputePoints((LargeBOD)dropped);
				else
					creds = TailorRewardCalculator.Instance.ComputePoints((SmallBOD)dropped) / 3;

				if (pm != null) pm.TailorBOD += creds;
			}

			from.SendSound(BaseVendorConstants.SOUND_RELIC);
			SayTo(from, string.Format(BaseVendorStringConstants.BOD_CREDITS_EARNED_FORMAT, creds.ToString()));
			SayTo(from, BaseVendorStringConstants.BOD_CREDITS_INFO);

			OnSuccessfulBulkOrderReceive(from);
			if (pm != null) pm.NextBODTurnInTime = DateTime.Now + TimeSpan.FromSeconds(BaseVendorConstants.BOD_TURNIN_DELAY_SECONDS);

			dropped.Delete();
			return true;
		}

		/// <summary>
		/// Extracts all items from a container and gives them to the player
		/// </summary>
		private void ExtractContainerContents(Mobile from, Item container)
		{
			if (!(container is Container)) return;

			List<Item> items = new List<Item>();
			foreach (Item item in ((Container)container).Items)
			{
				items.Add(item);
			}
			foreach (Item item in items)
			{
				from.AddToBackpack(item);
			}
		}

		#endregion

		#region Buy/Sell Item Processing (continued)

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
				
		
		/// <summary>
		/// Processes bulk order credit redemption and gives rewards
		/// </summary>
		/// <param name="from">The player redeeming credits</param>
		/// <param name="amount">The amount of credits to redeem</param>
		private void GiveReward(Mobile from, int amount)
		{
			int credittype = (this is Tailor || this is TailorGuildmaster) ? 2 : 1; // 1 = blacksmith, 2 = tailor

			int credits = ProcessCreditRedemption(from, amount, credittype);
			if (credits == 0) return;

			Titles.AwardFame(from, (credits / BaseVendorConstants.FAME_DIVISOR), true);

			this.Say(BaseVendorStringConstants.CREDITS_REWARD_GIVEN);
			from.SendMessage(string.Format(BaseVendorStringConstants.CREDITS_REDEEMED_FORMAT, credits.ToString()));

			Item reward = BulkOrderRewardCalculator.CalculateReward(credits, credittype);

			if (reward != null)
				from.AddToBackpack(reward);

			GiveGoldReward(from, credits);
		}

		/// <summary>
		/// Processes the credit redemption from player BOD totals
		/// </summary>
		/// <param name="from">The player redeeming credits</param>
		/// <param name="amount">The amount requested (0 = redeem all)</param>
		/// <param name="creditType">1 = blacksmith, 2 = tailor</param>
		/// <returns>The number of credits actually redeemed</returns>
		private int ProcessCreditRedemption(Mobile from, int amount, int creditType)
		{
			if (amount >= 1)
			{
				if (creditType == 1) // Blacksmith
				{
					if (amount > ((PlayerMobile)from).BlacksmithBOD)
						return 0; // Not enough credits

					((PlayerMobile)from).BlacksmithBOD -= amount;
					return amount;
				}
				else if (creditType == 2) // Tailor
				{
					if (amount > ((PlayerMobile)from).TailorBOD)
						return 0; // Not enough credits

					((PlayerMobile)from).TailorBOD -= amount;
					return amount;
				}
			}
			else // Redeem all available credits
			{
				if (creditType == 1) // Blacksmith
				{
					int availableCredits = ((PlayerMobile)from).BlacksmithBOD;
					((PlayerMobile)from).BlacksmithBOD = 0;
					return availableCredits;
				}
				else // Tailor
				{
					int availableCredits = ((PlayerMobile)from).TailorBOD;
					((PlayerMobile)from).TailorBOD = 0;
					return availableCredits;
				}
			}

			return 0;
		}

		/// <summary>
		/// Calculates the appropriate bulk order reward based on credit type and amount
		/// </summary>
		/// <param name="creditType">1 = blacksmith, 2 = tailor</param>
		/// <param name="credits">The number of credits to calculate reward for</param>
		/// <returns>The reward item, or null if no reward</returns>
		private Item CalculateBulkOrderReward(int creditType, int credits)
		{
			// Apply credit variability modifiers
			if (Utility.RandomDouble() < BaseVendorConstants.CREDIT_VAR_LOW)
				credits = (int)((double)credits * BaseVendorConstants.CREDIT_MULT_LOW);
			else if (Utility.RandomDouble() > BaseVendorConstants.CREDIT_VAR_HIGH)
				credits = (int)((double)credits * BaseVendorConstants.CREDIT_MULT_HIGH);

			// Apply diminishing returns
			AdventuresFunctions.DiminishingReturns(credits, BaseVendorConstants.DIMINISHING_RETURNS_CAP);

			// Get reward based on credit type
			if (creditType == 1) // Blacksmith
				return BulkOrderRewardCalculator.CalculateBlacksmithReward(credits);
			else if (creditType == 2) // Tailor
				return BulkOrderRewardCalculator.CalculateTailorReward(credits);

			return null;
		}

		/// <summary>
		/// Gives gold reward based on remaining credits
		/// </summary>
		/// <param name="from">The player receiving gold</param>
		/// <param name="credits">The number of credits to convert to gold</param>
		private void GiveGoldReward(Mobile from, int credits)
		{
			int gold = credits * Utility.RandomMinMax(BaseVendorConstants.GOLD_PER_CREDIT_MIN, BaseVendorConstants.GOLD_PER_CREDIT_MAX);

			if (IsInMidlandCached())
				gold /= BaseVendorConstants.MIDLAND_GOLD_DIVISOR;

			if (gold > BaseVendorConstants.BANK_DEPOSIT_THRESHOLD)
				Banker.Deposit(from, gold);
			else if (gold > 0)
				from.AddToBackpack(new Gold(gold));
		}

		#endregion

		#region Access Control

		/// <summary>
		/// Checks if a mobile has access to interact with this vendor
		/// </summary>
		/// <param name="from">The mobile attempting to access the vendor</param>
		/// <returns>True if the mobile has access, false otherwise</returns>
		public virtual bool CheckVendorAccess( Mobile from )
		{
			PlayerMobile pm = (PlayerMobile)from;

			if ( this is BaseGuildmaster && this.NpcGuild != pm.NpcGuild )
				return false;

			return true;
		}

		#endregion

		#region Buy/Sell Item Processing (continued)

		/// <summary>
		/// Processes a list of items being sold to the vendor
		/// </summary>
		/// <param name="seller">The mobile selling the items</param>
		/// <param name="list">List of sell responses containing items and prices</param>
		/// <returns>True if the sale was processed successfully, false otherwise</returns>
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

		/// <summary>
		/// Processes a single valid purchase from the vendor (validation phase)
		/// </summary>
		/// <param name="buy">The buy response containing purchase details</param>
		/// <param name="gbi">The buy info for the item</param>
		/// <param name="buyer">The mobile buying the item</param>
		/// <param name="totalCost">Reference to total cost accumulator</param>
		/// <param name="controlSlots">Reference to control slots available</param>
		/// <param name="bought">Reference to bought flag</param>
		/// <param name="validBuy">Reference to valid buy list</param>
		private void ProcessValidPurchase( BuyItemResponse buy, GenericBuyInfo gbi, Mobile buyer, ref int totalCost, ref int controlSlots, ref bool bought, ref List<BuyItemResponse> validBuy )
		{
			int amount = buy.Amount;
			if ( gbi.Amount <= 0 || amount <= 0 )
				return;

			int amountToBuy = amount;
			if ( amountToBuy > gbi.Amount )
				amountToBuy = gbi.Amount;

			if ( controlSlots >= gbi.ControlSlots * amountToBuy )
			{
				controlSlots -= gbi.ControlSlots * amountToBuy;
			}
			else
			{
				amountToBuy = controlSlots / gbi.ControlSlots;
				if ( amountToBuy < 1 )
					return;
				controlSlots -= gbi.ControlSlots * amountToBuy;
			}

			totalCost += gbi.Price * amountToBuy;
			bought = true;
			validBuy.Add( buy );
		}

		/// <summary>
		/// Processes a single valid purchase from the vendor (execution phase - creates items and gives to player)
		/// </summary>
		/// <param name="amount">The amount to purchase</param>
		/// <param name="bii">The buy info for the item</param>
		/// <param name="buyer">The mobile buying the item</param>
		/// <param name="cont">The container to place items in</param>
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

		/// <summary>
		/// Handles the "comandos" speech command to list available commands
		/// </summary>
		protected virtual bool HandleComandosCommand(SpeechEventArgs e)
		{
			if (!Insensitive.Contains(e.Speech, "comandos") && !Insensitive.Contains(e.Speech, "commands"))
				return false;

			e.Handled = true;

			StringBuilder commands = new StringBuilder();
			commands.Append("Comandos aceitos são:\n\n");

			// Base vendor commands
			if (IsActiveSeller)
				commands.Append("- Comprar (através do menu de contexto)\n");

			if (IsActiveBuyer)
				commands.Append("- Vender (através do menu de contexto)\n");

			// Item interactions
			if (this is Tinker || this is TinkerGuildmaster)
			{
				commands.Append("- Manual do Golem (soltar item)\n");
				commands.Append("- Orbe do Abismo (soltar item)\n");
				commands.Append("- Esquemas do Robô (soltar item)\n");
			}

			if (this is AnimalTrainer || this is Veterinarian)
			{
				commands.Append("- Ovos Alienígena/Earthy/Corrupted/Fey/Prehistoric/Reptilian/Dragon (soltar item)\n");
			}

			if (this is NecromancerGuildmaster)
			{
				commands.Append("- Crânio Dracolich (soltar item)\n");
				commands.Append("- Prisão Demoníaca (soltar item)\n");
			}

			if (this is Waiter)
			{
				commands.Append("- Item de Café (soltar item)\n");
			}

			if (this is VarietyDealer)
			{
				commands.Append("- Museu (soltar item)\n");
			}

			if (this is Tailor || this is TailorGuildmaster)
			{
				commands.Append("- Tapete Mágico (soltar item)\n");
			}

			if (this is Mapmaker)
			{
				commands.Append("- Ouro (100 moedas) para mapa personalizado\n");
			}

			if (this is Minter || this is Banker)
			{
				commands.Append("- Moedas de Cobre/Prata/Xormite/Cristais/Jóias/Gemas/Nuggets de Ouro (soltar item)\n");
			}

			if (this is Alchemist || this is Witches)
			{
				commands.Append("- Garrafa de Peças (soltar item)\n");
			}

			if (this is ThiefGuildmaster && e.Mobile is PlayerMobile && ((PlayerMobile)e.Mobile).NpcGuild == NpcGuild.ThievesGuild)
			{
				commands.Append("- Artefatos da Tumba (soltar item)\n");
			}

			if (this is InnKeeper || this is TavernKeeper || this is Barkeeper || this is Waiter)
			{
				commands.Append("- Itens Sujos/Espuma (soltar item)\n");
			}

			if (this is Alchemist || this is Herbalist)
			{
				commands.Append("- Item Encapotado (soltar item)\n");
			}

			// Bulk orders
			if (SupportsBulkOrders(e.Mobile))
			{
				commands.Append("- Ordens em Massa (através do menu de contexto)\n");
			}

			// Relic items (most vendors accept these)
			commands.Append("- Itens Relíquia (soltar item)\n");

			// Hired help
			if (this is InnKeeper || this is TavernKeeper || this is Barkeeper)
			{
				commands.Append("- Item Ajudante Lutador/Arqueiro/Mago (soltar item)\n");
			}

			// Cursed items
			if (this is Mage || this is KeeperOfChivalry || this is Witches || this is Necromancer || this is MageGuildmaster || this is HolyMage || this is NecroMage)
			{
				commands.Append("- Caixa de Livros/Item Amaldiçoado (soltar item)\n");
			}

			e.Mobile.SendMessage(0x5A, commands.ToString().TrimEnd());
			return true;
		}

		public override void OnSpeech(SpeechEventArgs e)
		{
			if (!e.Handled && e.Mobile.InRange(this, 3))
			{
				if (HandleComandosCommand(e))
					return;

				if (HandleTrainingCommand(e))
					return;
			}

			base.OnSpeech(e);
		}

		/// <summary>
		/// Handles training speech commands
		/// </summary>
		private bool HandleTrainingCommand(SpeechEventArgs e)
		{
			if (!Insensitive.Contains(e.Speech, "treinar") && !Insensitive.Contains(e.Speech, "train"))
				return false;

			e.Handled = true;
			BeginTraining(e.Mobile);
			return true;
		}

		/// <summary>
		/// Processes a list of items being bought from the vendor
		/// </summary>
		/// <param name="buyer">The mobile buying the items</param>
		/// <param name="list">List of buy responses containing items and prices</param>
		/// <returns>True if the purchase was processed successfully, false otherwise</returns>
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

				if ( ser.IsItem && amount > 0 )
				{
					Item item = World.FindItem( ser );

					if ( item == null )
						continue;

					GenericBuyInfo gbi = LookupDisplayObject( item );

					if ( gbi != null )
					{
						ProcessValidPurchase( buy, gbi, buyer, ref totalCost, ref controlSlots, ref bought, ref validBuy );
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
						ProcessValidPurchase( buy, gbi, buyer, ref totalCost, ref controlSlots, ref bought, ref validBuy );
				}
			}

			if ( fullPurchase && validBuy.Count == 0 )
				return false;

			bought = ( buyer.AccessLevel >= AccessLevel.GameMaster );

			// Always try backpack first
			cont = buyer.Backpack;
			if ( !bought && cont != null )
			{
				if ( cont.ConsumeTotal( typeof( Gold ), totalCost ) )
						bought = true;
			}

			// If backpack failed and totalCost > 100000, try bank
			if ( !bought && totalCost > 100000 )
			{
				cont = buyer.FindBankNoCreate();
				if ( cont != null && cont.ConsumeTotal( typeof( Gold ), totalCost ) )
				{
					bought = true;
					fromBank = true;
				}
				else
				{
					SayTo( buyer, 500191 ); // Begging thy pardon, but thy bank account lacks these funds.
				}
			}
			else if ( !bought && totalCost <= 100000 )
			{
				// If totalCost <= 100000 and backpack lacks funds, show error and don't try bank
				SayTo( buyer, 500192 ); // Begging thy pardon, but thou canst afford that.
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
					else if ( item != this.BuyPack && item.IsChildOf( this.BuyPack ) )
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
			}

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

		#region Properties and Display

		/// <summary>
		/// Adds vendor-specific properties to the object's property list
		/// </summary>
		/// <param name="list">The property list to add properties to</param>
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

		/// <summary>
		/// Serializes the vendor's state to save game data
		/// </summary>
		/// <param name="writer">The writer to serialize data to</param>
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

		/// <summary>
		/// Deserializes the vendor's state from saved game data
		/// </summary>
		/// <param name="reader">The reader to deserialize data from</param>
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

				// Add training context menu entry if vendor has trainable skills
				if ( GetTrainableSkills().Count > 0 )
					list.Add( new TrainingEntry( from, this ) );
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

		#region Generic Training System

		// Training System Limits
		private const double TRAINING_SKILL_CAP = 70.0; // Maximum trainable skill level (any vendor, any skill)
		private const double BEGINNER_TIER_MAX = 35.0; // End of beginner tier (0-35.0)
		private const double ADVANCED_TIER_MIN = 35.0; // Start of advanced tier (35.0-70.0)

		// Pricing Tiers (gold per 0.1 skill points)
		private const int BEGINNER_COST_PER_0_1 = 10; // 10 gold per 0.1 points (0-35.0 range)
		private const int ADVANCED_COST_PER_0_1 = 100; // 100 gold per 0.1 points (35.0-70.0 range)

		// Daily Limits
		private const double ADVANCED_DAILY_LIMIT = 5.0; // Max 5 points per day for advanced tier
		private const double TRAINING_INCREMENT = 0.1; // Skill increase per session

		// Training System Data Structures
		private static Dictionary<Serial, TrainingDepositState> m_TrainingDeposits = new Dictionary<Serial, TrainingDepositState>();

		/// <summary>
		/// Tracks active training deposit sessions
		/// </summary>
		private class TrainingDepositState
		{
			public SkillName Skill { get; set; }
			public string SkillName { get; set; }
			public TrainingTier Tier { get; set; }
			public DateTime DepositStartTime { get; set; }
			public List<Tuple<SkillName, double>> ReducedSkills { get; set; }

			public TrainingDepositState(SkillName skill, string skillName, TrainingTier tier)
			{
				Skill = skill;
				SkillName = skillName;
				Tier = tier;
				DepositStartTime = DateTime.UtcNow;
				ReducedSkills = new List<Tuple<SkillName, double>>();
			}
		}

		/// <summary>
		/// Training tier enumeration
		/// </summary>
		private enum TrainingTier
		{
			Beginner, // 0-35.0
			Advanced  // 35.0-70.0
		}

		/// <summary>
		/// Training calculation result
		/// </summary>
		private class TrainingCalculation
		{
			public double MaxTrainablePoints { get; set; }
			public int TotalCost { get; set; }
			public double RemainingDailyPoints { get; set; }
			public bool CanTrain { get; set; }
			public string Message { get; set; }
		}

		/// <summary>
		/// Training validation result
		/// </summary>
		public class TrainingValidationResult
		{
			public bool CanTrain { get; set; }
			public string Message { get; set; }
			public bool IsHardCapMessage { get; set; }

			public TrainingValidationResult(bool canTrain, string message, bool isHardCapMessage = false)
			{
				CanTrain = canTrain;
				Message = message;
				IsHardCapMessage = isHardCapMessage;
			}
		}

		/// <summary>
		/// Virtual method to check if vendor can train a specific skill
		/// Default implementation: vendor must have at least 1 point in the skill
		/// </summary>
		public virtual bool CanVendorTrainSkill(SkillName skill)
		{
			return this.Skills[skill].Value > 0;
		}

		/// <summary>
		/// Virtual method to check if player skill is below vendor's skill level
		/// </summary>
		public virtual bool IsPlayerBelowVendorSkill(Mobile from, SkillName skill)
		{
			return from.Skills[skill].Base < this.Skills[skill].Value;
		}

		/// <summary>
		/// Comprehensive validation for training eligibility
		/// </summary>
		public virtual TrainingValidationResult ValidateTrainingEligibility(Mobile from, SkillName skill)
		{
			// Check if vendor has the skill
			if (!CanVendorTrainSkill(skill))
			{
				return new TrainingValidationResult(false, "Eu não tenho conhecimento suficiente nesta habilidade.");
			}

			// Check if player skill is below vendor skill level
			if (!IsPlayerBelowVendorSkill(from, skill))
			{
				return new TrainingValidationResult(false, "Você possui mais conhecimento nesta habilidade do que eu posso te ensinar. Encontre outro treinador mais experiente!");
			}

			// Check training cap (70.0)
			if (from.Skills[skill].Base >= TRAINING_SKILL_CAP)
			{
				return new TrainingValidationResult(false, "Você já atingiu o máximo ensinável por treinadores.", true);
			}

			return new TrainingValidationResult(true, "");
		}

		/// <summary>
		/// Gets all skills that this vendor can train
		/// </summary>
		public virtual List<SkillName> GetTrainableSkills()
		{
			List<SkillName> trainableSkills = new List<SkillName>();

			// Check all possible skills
			foreach (SkillName skill in Enum.GetValues(typeof(SkillName)))
			{
				if (CanVendorTrainSkill(skill))
				{
					trainableSkills.Add(skill);
				}
			}

			return trainableSkills;
		}

		/// <summary>
		/// Gets the training tier for a player's skill level
		/// </summary>
		private TrainingTier GetTrainingTier(Mobile from, SkillName skill)
		{
			double currentSkill = from.Skills[skill].Base;
			return currentSkill < BEGINNER_TIER_MAX ? TrainingTier.Beginner : TrainingTier.Advanced;
		}

		/// <summary>
		/// Gets total skill points across all skills for a player
		/// Skills.Total is in fixed point (×10), so divide by 10.0
		/// </summary>
		private double GetTotalSkillPoints(Mobile from)
		{
			return from.Skills.Total / 10.0;
		}

		/// <summary>
		/// Finds all skills that can be reduced (Lock == Down) to make room for training
		/// </summary>
		/// <param name="from">The player mobile</param>
		/// <param name="excludeSkill">Skill to exclude from reduction (the one being trained)</param>
		/// <returns>List of reducible skills, sorted by highest first</returns>
		private List<Skill> GetReducibleSkills(Mobile from, SkillName excludeSkill)
		{
			List<Skill> reducibleSkills = new List<Skill>();

			foreach (Skill skill in from.Skills)
			{
				if (skill != null &&
					skill.SkillName != excludeSkill &&
					skill.Lock == SkillLock.Down &&
					skill.Base > 0)
				{
					reducibleSkills.Add(skill);
				}
			}

			// Sort by highest first (reduce from highest skills first)
			reducibleSkills.Sort((a, b) => b.Base.CompareTo(a.Base));

			return reducibleSkills;
		}

		/// <summary>
		/// Reduces skills with Lock == Down to make room for training
		/// Reduces from highest skills first, continues until enough points are freed
		/// </summary>
		/// <param name="from">The player mobile</param>
		/// <param name="pointsNeeded">Points needed to free up</param>
		/// <param name="trainingSkill">Skill being trained (to exclude from reduction)</param>
		/// <returns>List of skills reduced with amounts reduced</returns>
		private List<Tuple<SkillName, double>> ReduceSkillsToMakeRoom(Mobile from, double pointsNeeded, SkillName trainingSkill)
		{
			List<Tuple<SkillName, double>> reducedSkills = new List<Tuple<SkillName, double>>();
			double pointsFreed = 0.0;

			List<Skill> reducibleSkills = GetReducibleSkills(from, trainingSkill);

			foreach (Skill skill in reducibleSkills)
			{
				if (pointsFreed >= pointsNeeded)
					break;

				double remainingNeeded = pointsNeeded - pointsFreed;
				double availableToReduce = skill.Base;

				// Reduce in 0.1 increments
				int incrementsToReduce = (int)Math.Min(remainingNeeded * 10, availableToReduce * 10);
				double actualReduction = incrementsToReduce * 0.1;

				if (actualReduction > 0)
				{
					skill.Base -= actualReduction;
					pointsFreed += actualReduction;
					reducedSkills.Add(new Tuple<SkillName, double>(skill.SkillName, actualReduction));
				}
			}

			return reducedSkills;
		}

		/// <summary>
		/// Formats skill name for display (English)
		/// </summary>
		private string GetSkillDisplayName(SkillName skill)
		{
			// Return the original English skill name, formatted nicely
			string skillString = skill.ToString();

			// Convert PascalCase to Title Case (e.g., "AnimalTaming" -> "Animal Taming")
			if (skillString.Length > 0)
			{
				StringBuilder result = new StringBuilder();
				result.Append(skillString[0]);

				for (int i = 1; i < skillString.Length; i++)
				{
					if (char.IsUpper(skillString[i]) && i > 0)
					{
						result.Append(' ');
					}
					result.Append(skillString[i]);
				}

				return result.ToString();
			}

			return skillString;
		}

		/// <summary>
		/// Virtual method for vendors to initiate training
		/// </summary>
		public virtual void BeginTraining(Mobile from)
		{
			List<SkillName> trainableSkills = GetTrainableSkills();

			if (trainableSkills.Count == 0)
			{
				SayTo(from, "Desculpe, mas eu não tenho conhecimento suficiente para treinar nenhuma habilidade.");
				return;
			}

			// Default implementation - send training gump
			from.SendGump(new TrainingGump(this, from));
		}

		/// <summary>
		/// Virtual method to start training a specific skill
		/// </summary>
		public virtual void TrainSkill(Mobile from, SkillName skill, string skillName)
		{
			if (!from.CheckAlive())
				return;

			if (!(from is PlayerMobile))
				return;

			// Validate training eligibility
			var validation = ValidateTrainingEligibility(from, skill);
			if (!validation.CanTrain)
			{
				SayTo(from, validation.Message);
				return;
			}

			// Set player in deposit mode
			TrainingTier tier = GetTrainingTier(from, skill);
			m_TrainingDeposits[from.Serial] = new TrainingDepositState(skill, skillName, tier);

			// Show deposit instructions
			ShowDepositInstructions(from, skillName, tier);
		}

		/// <summary>
		/// Shows deposit instructions to player
		/// </summary>
		private void ShowDepositInstructions(Mobile from, string skillName, TrainingTier tier)
		{
			string message;

			if (tier == TrainingTier.Beginner)
			{
				message = string.Format(
					"Para treinar {0}: 10 moedas de ouro por cada 0,1 pontos de habilidade. " +
					"Me entregue a quantidade de ouro necessária.",
					skillName);
			}
			else
			{
				message = string.Format(
					"Para treinar {0}: 100 moedas de ouro por cada 0,1 pontos de habilidade. " +
					"Me entregue a quantidade de ouro necessária.",
					skillName);
			}

			// Overhead message (visible to everyone nearby)
			this.PublicOverheadMessage(MessageType.Regular, 0x3B2, false, message);

			// Also send private message to player
			from.SendMessage(0x3B2, message);
		}

		/// <summary>
		/// Handles gold deposits for training
		/// </summary>
		private bool HandleTrainingGoldDeposit(Mobile from, Gold gold)
		{
			if (!m_TrainingDeposits.ContainsKey(from.Serial))
			{
				// Not in training mode, return gold
				gold.MoveToWorld(from.Location, from.Map);
				return false;
			}

			TrainingDepositState depositState = m_TrainingDeposits[from.Serial];

			// Check minimum gold requirement
			int minimumGold = depositState.Tier == TrainingTier.Beginner ? BEGINNER_COST_PER_0_1 : ADVANCED_COST_PER_0_1;
			if (gold.Amount < minimumGold)
			{
				// Return gold to player's backpack
				from.AddToBackpack(gold);

				// Ludic message
				string ludicMessage;
				if (depositState.Tier == TrainingTier.Beginner)
				{
					ludicMessage = string.Format(
						"*risos* {0}, você precisa de pelo menos {1} moedas de ouro para treinar {2}! " +
						"Com {3} moedas não consigo ensinar nem 0,1 pontos! Aqui está seu ouro de volta.",
						from.Name, minimumGold, depositState.SkillName, gold.Amount);
				}
				else
				{
					ludicMessage = string.Format(
						"*risos* {0}, para treinar {1} no nível avançado você precisa de pelo menos {2} moedas de ouro! " +
						"Com apenas {3} moedas não consigo fazer nada! Aqui está seu ouro de volta.",
						from.Name, depositState.SkillName, minimumGold, gold.Amount);
				}

				this.PublicOverheadMessage(MessageType.Regular, 0x3B2, false, ludicMessage);
				from.SendMessage(0x3B2, ludicMessage);

				// Clear deposit state
				m_TrainingDeposits.Remove(from.Serial);
				return false;
			}

			// Calculate how many full 0.1 point increments can be purchased
			int costPerIncrement = depositState.Tier == TrainingTier.Beginner ? BEGINNER_COST_PER_0_1 : ADVANCED_COST_PER_0_1;
			int incrementsPurchasable = gold.Amount / costPerIncrement;
			
			// Calculate exact points and gold cost
			double pointsToTrain = incrementsPurchasable * 0.1;
			int goldCost = incrementsPurchasable * costPerIncrement;
			int change = gold.Amount - goldCost;

			// Validate against server skill cap (total skill points across all skills)
			double currentTotalSkills = GetTotalSkillPoints(from);
			int serverSkillCap = MyServerSettings.skillcap();
			double newTotalSkills = currentTotalSkills + pointsToTrain;

			if (currentTotalSkills >= serverSkillCap)
			{
				// Scenario A: Already at server skill cap
				this.PublicOverheadMessage(MessageType.Regular, 0x3B2, false,
					string.Format("Você já atingiu o limite máximo de habilidades (skill cap:{0} pontos). Não posso treinar mais! Aqui está seu ouro de volta.",
					serverSkillCap));
				from.AddToBackpack(gold);
				m_TrainingDeposits.Remove(from.Serial);
				return false;
			}
			else if (newTotalSkills > serverSkillCap)
			{
				// Scenario B: Would exceed server skill cap - try to reduce skills with Lock == Down
				double pointsNeeded = newTotalSkills - serverSkillCap;

				// Check if player has skills set to Down
				List<Skill> reducibleSkills = GetReducibleSkills(from, depositState.Skill);

				if (reducibleSkills.Count == 0)
				{
					// No skills set to Down - inform player
					this.PublicOverheadMessage(MessageType.Regular, 0x3B2, false,
						string.Format("Você atingiu o limite de habilidades ({0} pontos). " +
						"Para treinar mais, você precisa definir algumas habilidades como 'Down' (↓) no painel de habilidades. " +
						"Aqui está seu ouro de volta.", serverSkillCap));
					from.AddToBackpack(gold);
					m_TrainingDeposits.Remove(from.Serial);
					return false;
				}

				// Try to reduce skills to make room
				List<Tuple<SkillName, double>> reducedSkills = ReduceSkillsToMakeRoom(from, pointsNeeded, depositState.Skill);
				double pointsFreed = 0.0;
				foreach (var reduction in reducedSkills)
				{
					pointsFreed += reduction.Item2;
				}

				// Recalculate total after reduction
				double newTotalAfterReduction = GetTotalSkillPoints(from);
				double maxTrainableAfterReduction = serverSkillCap - newTotalAfterReduction;

				if (maxTrainableAfterReduction < 0.1)
				{
					// Still too close even after reduction
					this.PublicOverheadMessage(MessageType.Regular, 0x3B2, false,
						string.Format("Mesmo reduzindo habilidades, você ainda está muito próximo do limite ({0} pontos). " +
						"Aqui está seu ouro de volta.", serverSkillCap));
					from.AddToBackpack(gold);
					m_TrainingDeposits.Remove(from.Serial);
					return false;
				}

				// Check if we can train full amount or need to adjust
				if (pointsFreed >= pointsNeeded)
				{
					// Enough room freed - can train full amount
					// pointsToTrain, goldCost, change remain as calculated
					// Store reduction info for message later
					m_TrainingDeposits[from.Serial].ReducedSkills = reducedSkills;
				}
				else
				{
					// Not enough room freed - recalculate for partial training
					int maxIncrements = (int)(maxTrainableAfterReduction * 10); // Convert to increments
					pointsToTrain = maxIncrements * 0.1;
					goldCost = maxIncrements * costPerIncrement;
					change = gold.Amount - goldCost;
					// Store reduction info for message later
					m_TrainingDeposits[from.Serial].ReducedSkills = reducedSkills;
				}
			}
			// Scenario C: Within server skill cap - continue with normal training

			// Validate against training limits
			PlayerMobile pm = GetPlayerMobile(from);
			if (pm == null)
			{
				this.PublicOverheadMessage(MessageType.Regular, 0x3B2, false, "Erro: dados de treinamento não encontrados.");
				from.AddToBackpack(gold);
				m_TrainingDeposits.Remove(from.Serial);
				return false;
			}
			var maxTraining = CalculateMaxTrainingParameters(from, depositState.Skill, pm);

			if (pointsToTrain > maxTraining.MaxTrainablePoints)
			{
				double maxPointsPossible = maxTraining.MaxTrainablePoints;
				int maxGoldPossible = CalculateMaxGoldForPoints(maxPointsPossible, depositState.Tier);

				SayTo(from, string.Format(
					"Você depositou ouro demais. Com {0} moedas treinaria {1:F1} pontos, " +
					"mas só pode treinar {2:F1} pontos (máximo {3} moedas).",
					gold.Amount, pointsToTrain, maxPointsPossible, maxGoldPossible));

				// Return gold to player
				from.AddToBackpack(gold);
				return false;
			}

			// Check if player can train at least 0.1 points
			if (pointsToTrain < 0.1)
			{
				// This should have been caught by minimum check, but just in case
				from.AddToBackpack(gold);
				return false;
			}

			// Get the skill object
			Skill skill = from.Skills[depositState.Skill];
			if (skill == null)
			{
				from.AddToBackpack(gold);
				return false;
			}

			// Check if skill is locked (cannot be raised)
			if (skill.Lock == SkillLock.Locked)
			{
				this.PublicOverheadMessage(MessageType.Regular, 0x3B2, false,
					string.Format("*risos* {0}, a habilidade {1} está bloqueada para o treinamento! Aqui está seu ouro de volta.",
					from.Name, depositState.SkillName));
				from.AddToBackpack(gold);
				m_TrainingDeposits.Remove(from.Serial);
				return false;
			}

			// Check if skill is already at or above training cap
			double currentSkillBase = skill.Base;
			if (currentSkillBase >= TRAINING_SKILL_CAP)
			{
				this.PublicOverheadMessage(MessageType.Regular, 0x3B2, false,
					string.Format("Você já atingiu o máximo de conhecimento (skill cap: {0:F1} pontos). Talvez você precise esquecer alguma(s) habilidade(s)!",
					TRAINING_SKILL_CAP));
				from.AddToBackpack(gold);
				m_TrainingDeposits.Remove(from.Serial);
				return false;
			}

			// Check if training would exceed cap - adjust points to train
			double newSkillBase = currentSkillBase + pointsToTrain;
			if (newSkillBase > TRAINING_SKILL_CAP)
			{
				// Calculate how many points can actually be trained
				double maxPointsPossible = TRAINING_SKILL_CAP - currentSkillBase;
				
				if (maxPointsPossible < 0.1)
				{
					// Too close to cap, can't train even 0.1 points
					this.PublicOverheadMessage(MessageType.Regular, 0x3B2, false,
						string.Format("O copo está muito próximo de transbordar (total de skill: {0:F1} pontos)! Talvez eu consiga te ensinar menos! Aqui está seu ouro de volta.",
						TRAINING_SKILL_CAP));
					from.AddToBackpack(gold);
					m_TrainingDeposits.Remove(from.Serial);
					return false;
				}

				// Recalculate increments and cost for the adjusted amount
				int adjustedIncrements = (int)(maxPointsPossible * 10); // Convert to increments
				pointsToTrain = adjustedIncrements * 0.1;
				goldCost = adjustedIncrements * costPerIncrement;
				change = gold.Amount - goldCost;
			}

			// Apply training
			skill.Base += pointsToTrain;

			// Ensure we don't exceed cap (safety check)
			if (skill.Base > TRAINING_SKILL_CAP)
				skill.Base = TRAINING_SKILL_CAP;

			// Record training points for daily limits (ONLY for advanced tier)
			if (depositState.Tier == TrainingTier.Advanced)
			{
				RecordTrainingPoints(from, depositState.Skill, pointsToTrain);
			}

			// Store original amount and reduced skills info for message before deleting
			int originalAmount = gold.Amount;
			List<Tuple<SkillName, double>> skillsReduced = depositState.ReducedSkills;

			// Clear deposit state
			m_TrainingDeposits.Remove(from.Serial);

			// Delete the gold that was used
			gold.Delete();

			// Build success message with reduction info if applicable
			string successMessage;
			if (skillsReduced != null && skillsReduced.Count > 0)
			{
				// Build reduction details
				List<string> reductionDetails = new List<string>();
				foreach (var reduction in skillsReduced)
				{
					string skillName = GetSkillDisplayName(reduction.Item1);
					reductionDetails.Add(string.Format("{0} ({1:F1} pts)", skillName, reduction.Item2));
				}
				string reductionText = string.Join(", ", reductionDetails);

				if (change > 0)
				{
					successMessage = string.Format(
						"Você depositou {0} ouro, reduzi {1} para fazer espaço de treino, treinei {2:F1} pontos de {3} e devolvi {4} moedas de troco!",
						originalAmount, reductionText, pointsToTrain, depositState.SkillName, change);
				}
				else
				{
					successMessage = string.Format(
						"Você depositou {0} ouro, reduzi {1} para fazer espaço de treino e treinei {2:F1} pontos de {3}!",
						originalAmount, reductionText, pointsToTrain, depositState.SkillName);
				}
			}
			else
			{
				// No skills reduced
				if (change > 0)
				{
					successMessage = string.Format(
						"Você depositou {0} ouro, treinei {1:F1} pontos de {2} e devolvi {3} moedas de troco!",
						originalAmount, pointsToTrain, depositState.SkillName, change);
				}
				else
				{
					successMessage = string.Format(
						"Você depositou {0} ouro e treinou {1:F1} pontos de {2}!",
						originalAmount, pointsToTrain, depositState.SkillName);
				}
			}

			SayTo(from, successMessage);

			return true;
		}

		/// <summary>
		/// Calculates skill points from gold amount based on tier
		/// </summary>
		private double CalculatePointsFromGold(int goldAmount, TrainingTier tier)
		{
			if (tier == TrainingTier.Beginner)
			{
				return goldAmount / 100.0; // 100 gold = 1.0 points (10 gold per 0.1 points)
			}
			else
			{
				return goldAmount / 1000.0; // 1000 gold = 1.0 points (100 gold per 0.1 points)
			}
		}

		/// <summary>
		/// Calculates maximum gold needed for points based on tier
		/// </summary>
		private int CalculateMaxGoldForPoints(double points, TrainingTier tier)
		{
			if (tier == TrainingTier.Beginner)
			{
				return (int)(points * 100); // 1.0 points = 100 gold (10 gold per 0.1 points)
			}
			else
			{
				return (int)(points * 1000); // 1.0 points = 1000 gold (100 gold per 0.1 points)
			}
		}

		/// <summary>
		/// Calculates maximum training parameters for a player and skill
		/// </summary>
		private TrainingCalculation CalculateMaxTrainingParameters(Mobile from, SkillName skill, PlayerMobile pm)
		{
			var result = new TrainingCalculation();
			double currentSkill = from.Skills[skill].Base;

			if (currentSkill < BEGINNER_TIER_MAX)
			{
				// Beginner Tier (0-35.0): Can train up to 35.0 total, no daily limit
				double pointsToMax = BEGINNER_TIER_MAX - currentSkill;
				result.MaxTrainablePoints = pointsToMax;
				result.TotalCost = (int)(pointsToMax * 100); // 100 gold per 1.0 point (10 gold per 0.1 points)
				result.RemainingDailyPoints = -1; // No daily limit for beginner tier
				result.CanTrain = pointsToMax > 0;
			}
			else
			{
				// Advanced Tier (35.0+): Daily limit of 5.0 points
				double remainingDailyPoints = GetRemainingDailyPoints(from, skill, pm);
				double pointsToCap = TRAINING_SKILL_CAP - currentSkill;
				result.MaxTrainablePoints = Math.Min(remainingDailyPoints, pointsToCap);
				result.TotalCost = (int)(result.MaxTrainablePoints * 1000); // 1000 gold per 1.0 point (100 gold per 0.1 points)
				result.RemainingDailyPoints = remainingDailyPoints;
				result.CanTrain = result.MaxTrainablePoints > 0;
			}

			return result;
		}

		/// <summary>
		/// Records training points for daily limit tracking
		/// </summary>
		private void RecordTrainingPoints(Mobile from, SkillName skill, double pointsTrained)
		{
			// Only record daily points for advanced tier
			TrainingTier tier = GetTrainingTier(from, skill);
			if (tier != TrainingTier.Advanced)
				return;

			PlayerMobile pm = GetPlayerMobile(from);
			if (pm == null)
				return;

			pm.LastTrainingDate = DateTime.Today;

			if (!pm.DailyPointsTrained.ContainsKey(skill))
				pm.DailyPointsTrained[skill] = 0;

			pm.DailyPointsTrained[skill] += pointsTrained;
		}

		/// <summary>
		/// Training gump for selecting skills to train
		/// </summary>
		private class TrainingGump : Gump
		{
			private BaseVendor m_Vendor;
			private Mobile m_From;
			private List<SkillName> m_TrainableSkills;
			private int m_CurrentPage;
			private int m_TotalPages;
			private const int SKILLS_PER_PAGE = 8; // Number of skills to display per page
			private const int START_Y = 115; // Starting Y position for skills
			private const int SKILL_HEIGHT = 35; // Height per skill entry

			public TrainingGump(BaseVendor vendor, Mobile from, int page = 0)
				: base(25, 25)
			{
				m_Vendor = vendor;
				m_From = from;
				m_TrainableSkills = vendor.GetTrainableSkills();

				// Gump setup
				this.Closable = true;
				this.Disposable = true;
				this.Dragable = true;
				this.Resizable = false;

				// Calculate pagination
				m_TotalPages = (m_TrainableSkills.Count + SKILLS_PER_PAGE - 1) / SKILLS_PER_PAGE;
				if (m_TotalPages == 0)
					m_TotalPages = 1;

				// Ensure page is within valid range (0-based internally, displayed as 1-based)
				if (page < 0)
					page = 0;
				if (page >= m_TotalPages)
					page = m_TotalPages - 1;

				m_CurrentPage = page;

				// Create only the current page (simpler and more reliable)
				AddPage(0);

				// Background images
				AddImage(0, 0, 155); // Background - top left
				AddImage(300, 0, 155); // Background - top center (fills gap)
				AddImage(350, 0, 155); // Background - top right
				AddImage(0, 300, 155); // Background - bottom left
				AddImage(300, 300, 155); // Background - bottom center (fills gap)
				AddImage(350, 300, 155); // Background - bottom right
				// Add dark overlay to center vertical area (300-350px)
				AddImageTiled(300, 0, 50, 600, 2624); // Dark background tile for center area
				AddImage(2, 2, 129); // Borders
				AddImage(348, 2, 129);
				AddImage(2, 298, 129);
				AddImage(348, 298, 129);
				AddImage(7, 8, 133); // Header
				AddImage(218, 47, 132); // Divider
				AddImage(430, 8, 134); // Button background

				AddHtml(174, 68, 350, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>HABILIDADES DE TREINAMENTO</BIG></BASEFONT></BODY>", false, false);
				AddHtml(174, 90, 400, 40, @"<BODY><BASEFONT Color=#00FFFF><SMALL>* Limites para treinamento serão renovados todos os dias.</SMALL></BASEFONT></BODY>", false, false);

				// Calculate which skills to show on current page
				int startIndex = m_CurrentPage * SKILLS_PER_PAGE;
				int endIndex = Math.Min(startIndex + SKILLS_PER_PAGE, m_TrainableSkills.Count);

				// Display skills for current page
				int y = START_Y;

				for (int i = startIndex; i < endIndex; i++)
				{
					SkillName skill = m_TrainableSkills[i];

					y += SKILL_HEIGHT;

					string skillDisplayName = vendor.GetSkillDisplayName(skill);
					string trainingInfo = vendor.GetSkillTrainingInfo(from, skill);

					AddHtml(145, y, 490, 20,
						string.Format(@"<BODY><BASEFONT Color=#FCFF00><BIG>{0} {1}</BIG></BASEFONT></BODY>",
							skillDisplayName, trainingInfo), false, false);
					// Use skill index + 1 as button ID (1-based, avoids conflict with page navigation)
					AddButton(105, y, 4005, 4005, i + 1, GumpButtonType.Reply, 0);
				}

				// Add pagination buttons if needed
				// Button IDs: 10000 = Previous, 20000 = Next
				if (m_TotalPages > 1)
				{
					// Previous button - only show if NOT on first page (page 0)
					if (m_CurrentPage > 0)
					{
						AddButton(200, 450, 4014, 4016, 10000, GumpButtonType.Reply, 0);
						AddHtml(235, 452, 100, 20, @"<BODY><BASEFONT Color=#FFFFFF>Anterior</BASEFONT></BODY>", false, false);
					}

					// Page indicator (display as 1-based for user: page 0 = "Página 1")
					AddHtml(300, 452, 100, 20, string.Format(@"<BODY><BASEFONT Color=#FFFFFF>Página {0}/{1}</BASEFONT></BODY>", m_CurrentPage + 1, m_TotalPages), false, false);

					// Next button - only show if NOT on last page
					if (m_CurrentPage < m_TotalPages - 1)
					{
						AddButton(400, 450, 4005, 4007, 20000, GumpButtonType.Reply, 0);
						AddHtml(435, 452, 100, 20, @"<BODY><BASEFONT Color=#FFFFFF>Próxima</BASEFONT></BODY>", false, false);
					}
				}
			}

			public override void OnResponse(NetState sender, RelayInfo info)
			{
				Mobile from = sender.Mobile;
				int buttonID = info.ButtonID;
				
				// Handle page navigation
				if (buttonID == 10000)
				{
					// Previous button clicked - go to previous page
					int previousPage = m_CurrentPage - 1;
					if (previousPage >= 0)
					{
						from.SendGump(new TrainingGump(m_Vendor, m_From, previousPage));
					}
					return;
				}
				else if (buttonID == 20000)
				{
					// Next button clicked - go to next page
					int nextPage = m_CurrentPage + 1;
					if (nextPage < m_TotalPages)
					{
						from.SendGump(new TrainingGump(m_Vendor, m_From, nextPage));
					}
					return;
				}
				
				// Handle skill selection
				// Button IDs are skill index + 1 (1-based, range 1 to m_TrainableSkills.Count)
				if (buttonID > 0 && buttonID <= m_TrainableSkills.Count)
				{
					int skillIndex = buttonID - 1;

					if (skillIndex >= 0 && skillIndex < m_TrainableSkills.Count)
					{
						SkillName selectedSkill = m_TrainableSkills[skillIndex];
						string skillName = m_Vendor.GetSkillDisplayName(selectedSkill);

						// Start training for selected skill
						m_Vendor.TrainSkill(m_From, selectedSkill, skillName);
					}
				}
			}
		}

		/// <summary>
		/// Gets training information for display in gump
		/// </summary>
		private string GetSkillTrainingInfo(Mobile from, SkillName skill)
		{
			double currentSkill = from.Skills[skill].Base;

			if (currentSkill >= TRAINING_SKILL_CAP)
			{
				return "<BASEFONT Color=#FFFFFF>(Limite máx. - Não há como treinar aqui.)</BASEFONT>";
			}

			TrainingTier tier = GetTrainingTier(from, skill);

			if (tier == TrainingTier.Beginner)
			{
				// Beginner tier: can train up to 35.0 total
				// If skill is very low (less than 0.05), treat as 0 for display purposes
				double effectiveCurrentSkill = currentSkill < 0.05 ? 0.0 : currentSkill;
				double maxPoints = BEGINNER_TIER_MAX - effectiveCurrentSkill;
				int maxGold = (int)(maxPoints * 100); // 100 gold per 1.0 point (10 gold per 0.1 points)
				return string.Format("({0:F1} pts por {1} moedas de ouro) <BASEFONT Color=#00FFFF>- Iniciante</BASEFONT>", maxPoints, maxGold);
			}
			else
			{
				// Advanced tier: daily limit of 5.0 points
				PlayerMobile pm = GetPlayerMobile(from);
				if (pm == null)
					return "<BASEFONT Color=#FFFFFF>(Limite máx. - Não há como treinar aqui.)</BASEFONT>";

				double remainingPoints = GetRemainingDailyPoints(from, skill, pm);
				if (remainingPoints <= 0)
				{
					return "<BASEFONT Color=#FF0000>(Limite diário atingido - volte amanhã!)</BASEFONT>";
				}

				int maxGold = (int)(remainingPoints * 1000); // 1000 gold per 1.0 point (100 gold per 0.1 points)
				return string.Format("(até {0:F1} pts por {1} moedas de ouro) <BASEFONT Color=#00FFFF>- Avançado</BASEFONT>", remainingPoints, maxGold);
			}
		}

		/// <summary>
		/// Gets remaining daily points for advanced tier training
		/// </summary>
		private double GetRemainingDailyPoints(Mobile from, SkillName skill, PlayerMobile pm)
		{
			// Only apply daily limits for advanced tier
			TrainingTier tier = GetTrainingTier(from, skill);
			if (tier != TrainingTier.Advanced)
				return double.MaxValue; // Unlimited for beginner tier

			if (pm == null)
				return 0;

			// Reset daily counter only if it's a new day (midnight reset)
			// Only reset if LastTrainingDate is not MinValue (has been set before) AND date has changed
			if (pm.LastTrainingDate != DateTime.MinValue && pm.LastTrainingDate.Date < DateTime.Today)
			{
				pm.LastTrainingDate = DateTime.Today;
				pm.DailyPointsTrained.Clear();
			}

			double pointsTrainedToday;
			if (!pm.DailyPointsTrained.TryGetValue(skill, out pointsTrainedToday))
			{
				pointsTrainedToday = 0;
			}

			double remaining = ADVANCED_DAILY_LIMIT - pointsTrainedToday;
			return Math.Max(0, remaining); // Never return negative
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

	public class TrainingEntry : ContextMenuEntry
	{
		private BaseVendor m_Vendor;

		public TrainingEntry( Mobile from, BaseVendor vendor ) : base( 3006058, BaseVendorConstants.CONTEXT_MENU_RANGE_TRAINING )
		{
			m_Vendor = vendor;
			Enabled = vendor.CheckVendorAccess( from );
		}

		public override void OnClick()
		{
			m_Vendor.BeginTraining( this.Owner.From );
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

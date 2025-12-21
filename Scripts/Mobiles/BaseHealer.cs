using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using Server.Gumps;
using Server.ContextMenus;
using Server.Targeting;
using Server.Regions;

namespace Server.Mobiles
{
	public abstract class BaseHealer : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.HealersGuild; } }

		public override bool IsActiveVendor{ get{ return false; } }
		public override bool IsInvulnerable{ get{ return false; } }

		public override void InitSBInfo()
		{
		}

		public BaseHealer() : base( null )
		{
			if ( !IsInvulnerable )
			{
				AI = AIType.AI_Mage;
				ActiveSpeed = 0.2;
				PassiveSpeed = 0.8;
				RangePerception = BaseCreature.DefaultRangePerception;
				FightMode = FightMode.Aggressor;
			}

			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();

			SetStr( 304, 400 );
			SetDex( 102, 150 );
			SetInt( 204, 300 );

			SetDamage( 10, 23 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 75.0, 97.5 );
			SetSkill( SkillName.EvalInt, 82.0, 100.0 );
			SetSkill( SkillName.Healing, 75.0, 97.5 );
			SetSkill( SkillName.Magery, 82.0, 100.0 );
			SetSkill( SkillName.MagicResist, 82.0, 100.0 );
			SetSkill( SkillName.Tactics, 82.0, 100.0 );
			SetSkill( SkillName.Forensics, 82.0, 100.0 );

			Fame = 1000;
			Karma = 10000;

			PackItem( new Bandage( Utility.RandomMinMax( 5, 10 ) ) );
			PackItem( new HealPotion() );
			PackItem( new CurePotion() );
		}

		public override VendorShoeType ShoeType{ get{ return VendorShoeType.Sandals; } }

		public override bool ClickTitle{ get{ return false; } } // Do not display title in OnSingleClick

		public virtual bool HealsYoungPlayers{ get{ return true; } }

		public virtual bool CheckResurrect( Mobile m )
		{
			Region reg = Region.Find( m.Location, m.Map );

			if ( this is WanderingHealer || this is EvilHealer )
			{
				return true;
			}
			else if ( ( reg.IsPartOf( typeof( PublicRegion ) ) || reg.IsPartOf( typeof( NecromancerRegion ) ) || reg.IsPartOf( "the Undercity of Umbra" ) ) && ( m.Karma < 0 || m.Kills > 0 || m.Criminal ) )
			{
				return true;
			}
			else if ( this is WanderingHealer && m.Criminal )
			{
				Say( 501222 ); // Thou art a criminal.  I shall not resurrect thee.
				return false;
			}
			else if ( this is WanderingHealer && (m.Kills > 0 || m.Karma < 0) ) // WIZARD CHANGED
			{
				Say( 501223 ); // Thou'rt not a decent and good person. I shall not resurrect thee.
				return false;
			}
			else if (this is EvilHealer && m.Karma > 0)
			{
				this.Say(BaseHealerStringConstants.MSG_EVIL_HEALER_REFUSE);
				return false;
			}

			return true;
		}

		private DateTime m_NextResurrect;
		private static TimeSpan ResurrectDelay = TimeSpan.FromSeconds( 2.0 );

		// Track when players cancel the resurrection gump to prevent immediate re-showing
		private static Dictionary<Mobile, DateTime> m_GumpCancellationTimes = new Dictionary<Mobile, DateTime>();
		private static TimeSpan GumpCancellationDelay = TimeSpan.FromSeconds( 3.0 );

		/// <summary>
		/// Records when a player cancels the resurrection gump
		/// </summary>
		public static void RecordGumpCancellation( Mobile m )
		{
			if ( m != null )
			{
				m_GumpCancellationTimes[m] = DateTime.UtcNow;
			}
		}

		/// <summary>
		/// Checks if enough time has passed since the player last cancelled the gump
		/// </summary>
		public static bool CanShowResurrectionGump( Mobile m )
		{
			if ( m == null )
				return false;

			if ( !m_GumpCancellationTimes.ContainsKey( m ) )
				return true; // Never cancelled, can show

			DateTime cancellationTime = m_GumpCancellationTimes[m];
			TimeSpan timeSinceCancellation = DateTime.UtcNow - cancellationTime;

			// Remove old entries to prevent memory leak
			if ( timeSinceCancellation > TimeSpan.FromMinutes( 5.0 ) )
			{
				m_GumpCancellationTimes.Remove( m );
				return true;
			}

			// Can show if 3 seconds have passed
			return timeSinceCancellation >= GumpCancellationDelay;
		}

		public virtual void OfferResurrection( Mobile m )
		{
			// Check if player recently cancelled the gump
			if ( !CanShowResurrectionGump( m ) )
				return;

			Direction = GetDirectionTo( m );

			m.PlaySound( 0x214 );
			m.FixedEffect( 0x376A, 10, 16 );

			m.CloseGump( typeof( ResurrectCostGump ) );
			m.SendGump( new ResurrectCostGump( m, 1 ) );
		}

		public virtual void OfferHeal( PlayerMobile m )
		{
			Direction = GetDirectionTo( m );

			// COMMENTED OUT: CheckYoungHealTime is disabled - healers now always offer healing
			// if ( m.CheckYoungHealTime() )
			// {
				Say( 501229 ); // You look like you need some healing my child.

				m.PlaySound( 0x1F2 );
				m.FixedEffect( 0x376A, 9, 32 );

				m.Hits = m.HitsMax;
			// }
			// else
			// {
			//	Say( 501228 ); // I can do no more for you at this time.
			// }
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( !m.Frozen && (m is PlayerMobile || m is Squire) && DateTime.UtcNow >= m_NextResurrect && InRange( m, 6 ) && this.InLOS( m ) )
			{
				if ( !m.Alive )
				{
					m_NextResurrect = DateTime.UtcNow + ResurrectDelay;

					if ( m.Map == null || !m.Map.CanFit( m.Location, 16, false, false ) )
					{
						if (m is PlayerMobile)
							m.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!
					}
					else if ( CheckResurrect( m ) )
					{
						if (m is Squire)
							((BaseCreature)m).ResurrectPet();
						else if (m is PlayerMobile && !((PlayerMobile)m).SbResTimer  )
							OfferResurrection( m );
					}
				}
				else if ( m.Hits < m.HitsMax && m is PlayerMobile )
				{
					OfferHeal( (PlayerMobile) m );
				}
			}
		}

		private class FixEntry : ContextMenuEntry
		{
			private BaseHealer m_BaseHealer;
			private Mobile m_From;

			public FixEntry( BaseHealer BaseHealer, Mobile from ) : base( 6120, 12 )
			{
				m_BaseHealer = BaseHealer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_BaseHealer.BeginHealing( m_From );
			}
		}

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive && !from.Blessed )
			{
				list.Add( new FixEntry( this, from ) );
			}

			base.AddCustomContextEntries( from, list );
		}

        public void BeginHealing(Mobile from)
        {
            if ( Deleted || !from.Alive )
                return;

			SayTo(from, BaseHealerStringConstants.MSG_OFFER_HENCHMAN_RESURRECTION);

            from.Target = new HealingTarget(this);
        }

        private class HealingTarget : Target
        {
            private BaseHealer m_BaseHealer;

            public HealingTarget(BaseHealer mage) : base(12, false, TargetFlags.None)
            {
                m_BaseHealer = mage;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
				int nCost = 0;
                if (targeted is HenchmanFighterItem && from.Backpack != null)
                {
					Item hench = targeted as Item;
					HenchmanFighterItem thing = (HenchmanFighterItem)hench;

					nCost = thing.HenchDead;
					if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost );
					}

                    Container pack = from.Backpack;
                    int toConsume = nCost;

                    if ( nCost < 1 )
                    {
                        m_BaseHealer.SayTo( from, BaseHealerStringConstants.MSG_HENCHMAN_NOT_DEAD );
                    }
                    else if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						thing.Name = BaseHealerStringConstants.HENCHMAN_NAME_FIGHTER;
						thing.HenchDead = 0;
						thing.InvalidateProperties();
                        from.SendMessage(String.Format(BaseHealerStringConstants.MSG_PAY_GOLD_FORMAT, toConsume));
						from.PlaySound( 0x214 );
						m_BaseHealer.SayTo(from, BaseHealerStringConstants.MSG_HENCHMAN_RESURRECTED);
                    }
                    else
                    {
                        m_BaseHealer.SayTo(from, BaseHealerStringConstants.MSG_RESURRECTION_COST_FORMAT, toConsume);
                        from.SendMessage(BaseHealerStringConstants.MSG_NOT_ENOUGH_GOLD);
                    }
                }
                else if (targeted is HenchmanWizardItem && from.Backpack != null)
                {
					Item hench = targeted as Item;
					HenchmanWizardItem thing = (HenchmanWizardItem)hench;

					nCost = thing.HenchDead;
					if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost );
					}

                    Container pack = from.Backpack;
                    int toConsume = nCost;

                    if ( nCost < 1 )
                    {
                        m_BaseHealer.SayTo( from, BaseHealerStringConstants.MSG_HENCHMAN_NOT_DEAD );
                    }
                    else if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						thing.Name = BaseHealerStringConstants.HENCHMAN_NAME_WIZARD;
						thing.HenchDead = 0;
						thing.InvalidateProperties();
                        from.SendMessage(String.Format(BaseHealerStringConstants.MSG_PAY_GOLD_FORMAT, toConsume));
						from.PlaySound( 0x214 );
						m_BaseHealer.SayTo(from, BaseHealerStringConstants.MSG_HENCHMAN_RESURRECTED);
                    }
                    else
                    {
                        m_BaseHealer.SayTo(from, BaseHealerStringConstants.MSG_RESURRECTION_COST_FORMAT, toConsume);
                        from.SendMessage(BaseHealerStringConstants.MSG_NOT_ENOUGH_GOLD);
                    }
                }
                else if (targeted is HenchmanArcherItem && from.Backpack != null)
                {
					Item hench = targeted as Item;
					HenchmanArcherItem thing = (HenchmanArcherItem)hench;

					nCost = thing.HenchDead;
					if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost );
					}

                    Container pack = from.Backpack;
                    int toConsume = nCost;

                    if ( nCost < 1 )
                    {
                        m_BaseHealer.SayTo( from, BaseHealerStringConstants.MSG_HENCHMAN_NOT_DEAD );
                    }
                    else if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						thing.Name = BaseHealerStringConstants.HENCHMAN_NAME_ARCHER;
						thing.HenchDead = 0;
						thing.InvalidateProperties();
                        from.SendMessage(String.Format(BaseHealerStringConstants.MSG_PAY_GOLD_FORMAT, toConsume));
						from.PlaySound( 0x214 );
						m_BaseHealer.SayTo(from, BaseHealerStringConstants.MSG_HENCHMAN_RESURRECTED);
                    }
                    else
                    {
                        m_BaseHealer.SayTo(from, BaseHealerStringConstants.MSG_RESURRECTION_COST_FORMAT, toConsume);
                        from.SendMessage(BaseHealerStringConstants.MSG_NOT_ENOUGH_GOLD);
                    }
                }
                else if (targeted is HenchmanMonsterItem && from.Backpack != null)
                {
					Item hench = targeted as Item;
					HenchmanMonsterItem thing = (HenchmanMonsterItem)hench;

					nCost = thing.HenchDead;
					if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost );
					}

                    Container pack = from.Backpack;
                    int toConsume = nCost;

                    if ( nCost < 1 )
                    {
                        m_BaseHealer.SayTo( from, BaseHealerStringConstants.MSG_HENCHMAN_NOT_DEAD );
                    }
                    else if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						thing.Name = BaseHealerStringConstants.HENCHMAN_NAME_CREATURE;
						thing.HenchDead = 0;
						thing.InvalidateProperties();
                        from.SendMessage(String.Format(BaseHealerStringConstants.MSG_PAY_GOLD_FORMAT, toConsume));
						from.PlaySound( 0x214 );
						m_BaseHealer.SayTo(from, BaseHealerStringConstants.MSG_HENCHMAN_RESURRECTED);
                    }
                    else
                    {
                        m_BaseHealer.SayTo(from, BaseHealerStringConstants.MSG_RESURRECTION_COST_FORMAT, toConsume);
                        from.SendMessage(BaseHealerStringConstants.MSG_NOT_ENOUGH_GOLD);
                    }
                }
				else
				{
					m_BaseHealer.SayTo(from, BaseHealerStringConstants.MSG_DOES_NOT_NEED_SERVICES);
				}
            }
        }

		public BaseHealer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			if ( !IsInvulnerable )
			{
				AI = AIType.AI_Mage;
				ActiveSpeed = 0.2;
				PassiveSpeed = 0.8;
				RangePerception = BaseCreature.DefaultRangePerception;
				FightMode = FightMode.Aggressor;
			}
		}
	}
}
using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles 
{ 
	public class TownGuards : BasePerson
    {

        private static bool m_Talked;

        [Constructable] 
		public TownGuards() : base() 
		{
			Title = "o guarda";
			NameHue = 1154;
			SetStr( 200, 300 );
			SetDex( 200, 300 );
			SetInt( 200, 300 );
			SetHits( 500,5000 );
			SetDamage( 200, 500 );
			VirtualArmor = 3000;

			SetSkill( SkillName.Anatomy, 120.0 );
			SetSkill( SkillName.MagicResist, 120.0);
			SetSkill( SkillName.Parry, 120.0);
            SetSkill(SkillName.Fencing, 120.0);
            SetSkill(SkillName.Macing, 120.0);
            SetSkill( SkillName.DetectHidden, 120.0);
			SetSkill( SkillName.Wrestling, 120.0);
			SetSkill( SkillName.Swords, 120.0);
			SetSkill( SkillName.Tactics, 120.0);
		}

		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool Unprovokable { get { return true; } }
		public override bool Uncalmable{ get{ return true; } }

		private string getBountyDialog(Mobile from, Item dropped, int gold) 
		{
            string sMessage = "";
			string sReward = "";

            switch (Utility.RandomMinMax(0, 3))
            {
                case 0: sReward = "Aqui está a sua recompensa de " + gold.ToString() + " moedas de ouro."; break;
                case 1: sReward = "Tome o seu pagamento de " + gold.ToString() + " moedas de ouro."; break;
                case 2: sReward = "Sua recompensa é de " + gold.ToString() + " moedas de ouro."; break;
                case 3: sReward = "Este procurado tinha uma recompensa de " + gold.ToString() + " moedas de ouro."; break;
            }

            switch (Utility.RandomMinMax(0, 4))
            {
                case 0: sMessage = "Ora ora! Estavamos a muito tempo atrás desse aí. " + sReward; break;
                case 1: sMessage = "Que satisfação hein aspira?! " + sReward; break;
                case 2: sMessage = "Hmm..eu nunca achei que pegariam esse criminoso. " + sReward; break;
                case 3: sMessage = "Os mares agora estão mais seguros. " + sReward; break;
                case 4: sMessage = "Onde você achou esse traste!? " + sReward; break;
            }

            return sMessage;
        }

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if (IntelligentAction.GetMyEnemies(from, this, false) == true)
			{
				string sSay = "Você não deveria estar carregando isso com você!";
				this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sSay, from.NetState);
            }
			else 
			{
				string sMessage = "";
                int karma = 0;
                int gold = 0;
				int fame = 0;

				if (dropped is PirateBounty)
				{
					PirateBounty bounty = (PirateBounty)dropped;
					fame = (int)(bounty.BountyValue / 5);
					karma = -1 * fame;
					gold = bounty.BountyValue;
				}
				else if (dropped is Head && !from.Blessed)
				{
					Head head = (Head)dropped;

					if (head.m_Job == "Thief")
					{
						karma = Utility.RandomMinMax(40, 60);
						gold = Utility.RandomMinMax(80, 120);
					}
					else if (head.m_Job == "Bandit")
					{
						karma = Utility.RandomMinMax(20, 30);
						gold = Utility.RandomMinMax(30, 40);
					}
					else if (head.m_Job == "Brigand")
					{
						karma = Utility.RandomMinMax(30, 40);
						gold = Utility.RandomMinMax(50, 80);
					}
					else if (head.m_Job == "Pirate")
					{
						karma = Utility.RandomMinMax(90, 110);
						gold = Utility.RandomMinMax(120, 160);
					}
					else if (head.m_Job == "Assassin")
					{
						karma = Utility.RandomMinMax(60, 80);
						gold = Utility.RandomMinMax(100, 140);
					}
					else
					{
						sMessage = "Irei assumir que ele lhe fez algum mal. Vou fazer vista grossa dessa vez!";
						this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
						return base.OnDragDrop(from, dropped);
					}
				}

                sMessage = getBountyDialog(from, dropped, gold);

                Titles.AwardKarma(from, karma, true);
                Titles.AwardFame(from, fame, true);

                from.SendSound(0x2E6);
                from.AddToBackpack(new Gold(gold));

                this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
                dropped.Delete();
                return true;
            }
            return base.OnDragDrop(from, dropped);
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            WalkAwayCombatTimer t = new WalkAwayCombatTimer(this);
            t.Start();

        }

        private class WalkAwayCombatTimer : Timer
        {
            private static TownGuards m_from;

            public WalkAwayCombatTimer(TownGuards from) : base(TimeSpan.FromSeconds(5))
            {
				m_Talked = true;
                m_from = from;
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if ((int)m_from.GetDistanceToSqrt(m_from.Home) > (m_from.RangeHome + 15))
                {
                    string sMessage = "Estou retornando para o meu posto!";
                    m_from.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, m_from.NetState);

                    m_from.Location = m_from.Home;
                    Effects.SendLocationParticles(EffectItem.Create(m_from.Location, m_from.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                    Effects.PlaySound(m_from, m_from.Map, 0x201);
                }
                m_Talked = false;
            }
        }

        private int getCityColor() 
		{
			return 0;
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();

			Region reg = Region.Find( this.Location, this.Map );

			string World = Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y );

			int clothColor = 0;
			int shieldType = 0;
			int helmType = 0;
			int cloakColor = 0;

			Item weapon = new VikingSword(); weapon.Delete();

			if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Village of Whisper" )
			{
				clothColor = 0x96D;		shieldType = 0x1B72;	helmType = 0x140E;		cloakColor = 0x972;		weapon = new Longsword();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Town of Glacial Hills" )
			{
				clothColor = 0x482;		shieldType = 0x1B74;	helmType = 0x1412;		cloakColor = 0x542;		weapon = new Kryss();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Village of Springvale" )
			{
				clothColor = 0x595;		shieldType = 0;			helmType = 0x140E;		cloakColor = 0x593;		weapon = new Pike();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the City of Elidor" )
			{
				clothColor = 0x665;		shieldType = 0x1B7B;	helmType = 0x1412;		cloakColor = 0x664;		weapon = new Katana();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Village of Islegem" )
			{
				clothColor = 0x7D1;		shieldType = 0;			helmType = 0x140E;		cloakColor = 0x7D6;		weapon = new Spear();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "Greensky Village" )
			{
				clothColor = 0x7D7;		shieldType = 0;			helmType = 0x1412;		cloakColor = 0x7DA;		weapon = new Bardiche();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Port of Dusk" )
			{
				clothColor = 0x601;		shieldType = 0x1B76;	helmType = 0x140E;		cloakColor = 0x600;		weapon = new Cutlass();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Port of Starguide" )
			{
				clothColor = 0x751;		shieldType = 0;			helmType = 0x1412;		cloakColor = 0x758;		weapon = new BladedStaff();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Village of Portshine" )
			{
				clothColor = 0x847;		shieldType = 0x1B7A;	helmType = 0x140E;		cloakColor = 0x851;		weapon = new Mace();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Ranger Outpost" )
			{
				clothColor = 0x598;		shieldType = 0;			helmType = 0x140E;		cloakColor = 0x83F;		weapon = new Spear();
			}
			else if ( World == "the Land of Lodoria" ) // ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the City of Lodoria" || Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Castle of Knowledge" || Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Lodoria City Park" || Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Village of Lodoria" || Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Lodoria Cemetery" )
			{
				clothColor = 0x6E4;		shieldType = 0x1BC4;	helmType = 0x1412;		cloakColor = 0x6E7;		weapon = new Scimitar();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Lunar City of Dawn" )
			{
				clothColor = 0x9C4;		shieldType = 0x1B76;	helmType = 0x140E;		cloakColor = 0x9C4;		weapon = new DiamondMace();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "The Town of Devil Guard" || Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "The Farmland of Devil Guard" )
			{
				clothColor = 0x430;		shieldType = 0;			helmType = 0x140E;		cloakColor = 0;			weapon = new LargeBattleAxe();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Town of Moon" )
			{
				clothColor = 0x8AF;		shieldType = 0x1B72;	helmType = 0x1412;		cloakColor = 0x972;		weapon = new Longsword();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Village of Grey" )
			{
				clothColor = 0;			shieldType = 0;			helmType = 0x140E;		cloakColor = 0x763;		weapon = new Halberd();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the City of Montor" )
			{
				clothColor = 0x96F;		shieldType = 0x1B74;	helmType = 0x1412;		cloakColor = 0x529;		weapon = new Broadsword();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Village of Fawn" )
			{
				clothColor = 0x59D;		shieldType = 0;			helmType = 0x140E;		cloakColor = 0x59C;		weapon = new DoubleAxe();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Village of Yew" )
			{
				clothColor = 0x83C;		shieldType = 0;			helmType = 0x1412;		cloakColor = 0x850;		weapon = new Spear();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "Iceclad Fisherman's Village" || Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Town of Mountain Crest" || Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "Glacial Coast Village" )
			{
				clothColor = 0x482;		shieldType = 0;			helmType = 0x140E;		cloakColor = 0x47E;		weapon = new Bardiche();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Undercity of Umbra" )
			{
				clothColor = 0x964;		shieldType = 0x1BC3;	helmType = 0x140E;		cloakColor = 0x966;		weapon = new BoneHarvester();
			}
			else if ( World == "the Island of Umber Veil" )
			{
				clothColor = 0xA5D;		shieldType = 0;			helmType = 0x140E;		cloakColor = 0x96D;		weapon = new Halberd();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "ilha de Kuldar" || Server.Misc.Worlds.GetRegionName(this.Map, this.Location) == "cidade de Kuldara")
			{
				clothColor = 0xB3B;		shieldType = 0x1BC3;	helmType = 0x140E;		cloakColor = 0x845;		weapon = new Maul();
			}
			else if ( World == "the Isles of Dread" )
			{
				clothColor = 0x978;		shieldType = 0;			helmType = 0x2645;		cloakColor = 0x973;		weapon = new OrnateAxe();
			}
			else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Village of Barako" )
			{
				clothColor = 0x515;		shieldType = 0x1B72;	helmType = 0x2645;		cloakColor = 0x58D;		weapon = new WarMace();
			}
			else if ( World == "the Savaged Empire" ) // ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Village of Kurak" )
			{
				clothColor = 0x515;		shieldType = 0;			helmType = 0x140E;		cloakColor = 0x59D;		weapon = new Spear();
			}
			else if ( World == "the Serpent Island" ) // ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the City of Furnace" )
			{
				clothColor = 0x515;		shieldType = 0;			helmType = 0x2FBB;		cloakColor = 0;			weapon = new Halberd();
			}
			else // if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the City of Britain" || Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Britain Castle Grounds" || Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "Lord British Castle" || Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Britain Dungeons" )
			{
				clothColor = 0x966;		shieldType = 0x1BC4;	helmType = 0x140E;		cloakColor = 2900;		weapon = new VikingSword();
			}

			weapon.Movable = false;
			((BaseWeapon)weapon).MaxHitPoints = 1000;
			((BaseWeapon)weapon).HitPoints = 1000;
			((BaseWeapon)weapon).MinDamage = 100;
			((BaseWeapon)weapon).MaxDamage = 500;
			AddItem( weapon );

			AddItem( new PlateChest() );
			if ( World == "the Serpent Island" ){ AddItem( new RingmailArms() ); } else { AddItem( new PlateArms() ); } // FOR GARGOYLES
			AddItem( new PlateLegs() );
			AddItem( new PlateGorget() );
			AddItem( new PlateGloves() );
			AddItem( new Boots( ) );

			if ( helmType > 0 )
			{
				PlateHelm helm = new PlateHelm();
					helm.ItemID = helmType;
					helm.Name = "helm";
					AddItem( helm );
			}
			if ( shieldType > 0 )
			{
				ChaosShield shield = new ChaosShield();
					shield.ItemID = shieldType;
					shield.Name = "shield";
					AddItem( shield );
			}

			MorphingTime.ColorMyClothes( this, clothColor );

			if ( cloakColor > 0 )
			{
				Cloak cloak = new Cloak();
					cloak.Hue = cloakColor;
					AddItem( cloak );
			}

			Server.Misc.MorphingTime.CheckMorph( this );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			if (m_Talked == false)
			{
                switch (Utility.Random(8))
                {
                    case 0: Say("AUTO! Pare em nome da lei!"); break;
                    case 1: Say("Eu irei lhe mostrar a justiça!"); break;
                    case 2: Say("" + defender.Name + "!! Sua história acaba aqui e agora!"); break;
                    case 3: Say("Estavamos atrás de você " + defender.Name + "!"); break;
                    case 4: Say("Soldados! " + defender.Name + " está aqui!"); break;
                    case 5: Say("Somos treinados para caçar criminosos como você " + defender.Name + "!"); break;
                    case 6: Say("Desista! Irei acabar com você " + defender.Name + "!"); break;
                    case 7: Say("" + defender.Name + "! Sua sentença será a morte!"); break;
                };
            }
		}

		public override bool IsEnemy( Mobile m )
		{
			if ( IntelligentAction.GetMyEnemies( m, this, true ) == false )
				return false;

			if ( m.Region != this.Region && !(m is PlayerMobile) )
				return false;

			m.Criminal = true;
			Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
			Effects.PlaySound( this, this.Map, 0x201 );
			this.Location = m.Location;
			this.Combatant = m;
			this.Warmode = true;
			Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
			Effects.PlaySound( this, this.Map, 0x201 );

			return true;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		}

		
        public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "Código de Conduta Militar", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Guard" ) ));
					}
				}

				ArrayList wanted = new ArrayList();
				int w = 0;
				foreach ( Item item in World.Items.Values )
				{
					if ( item is CharacterDatabase )
					{
						CharacterDatabase DB = (CharacterDatabase)item;

						if ( DB.CharacterWanted != null && DB.CharacterWanted != "" )
						{
							wanted.Add( item );
							w++;
						}
					}
				}
				int wChoice = Utility.RandomMinMax( 1, w );
				int c = 0;
				for ( int i = 0; i < wanted.Count; ++i )
				{
					c++;
					if ( c == wChoice )
					{
						CharacterDatabase DB = ( CharacterDatabase )wanted[ i ];
						GuardNote note = new GuardNote();
						note.ScrollText = DB.CharacterWanted;
						m_Mobile.AddToBackpack( note );
						m_Giver.Say("Cidadão, fique atento!");
					}
				}
            }
        }

		public override bool OnBeforeDeath()
		{
			Say("A aura da irmandade militar irá me proteger!");
			this.Hits = this.HitsMax;
			this.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
			this.PlaySound( 0x202 );
			return false;
		}

		public TownGuards( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	} 
}   
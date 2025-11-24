using System;
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Regions;
using Server.Commands;
using System.Text;

namespace Server.Mobiles
{
	public class DungeonGuide : TownHerald
	{
		[Constructable]
		public DungeonGuide() : base()
		{
			Title = "o guia das masmorras";
			Name = NameList.RandomName( "male" );

			Blessed = true;
			Paralyze( TimeSpan.FromDays( 365 * 10 ) ); // Paralyzed for 10 years
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, 10 ) && m is PlayerMobile )
			{
				// Turn to face the approaching player
				this.Direction = this.GetDirectionTo( m );

				if ( DateTime.UtcNow >= NextTalk )
				{
					Region reg = Region.Find( this.Location, this.Map );

					// Provide contextual guidance based on region type
					if ( reg is DungeonRegion )
					{
						string dungeonMessage = GetDungeonMessage(reg);
						Say( dungeonMessage );
					}
				else if ( reg is CaveRegion )
				{
					Say( "Cuidado com desmoronamentos e criaturas venenosas nestas profundezas! Fique alerta e observe onde pisa." );
				}
				else if ( reg is DeadRegion )
				{
					Say( "Os mortos caminham por estas terras! Use magia sagrada e procure solo consagrado para sua segurança." );
				}
				else if ( reg is GargoyleRegion )
				{
					Say( "Gargulas habitam aqui! Sua pele semelhante a pedra resiste a armas normais. Magia e armas abençoadas são sua melhor defesa." );
				}
				else if ( reg is NecromancerRegion )
				{
					Say( "Necromantes praticam suas artes sombrias nas proximidades! Cuidado com lacaios mortos-vivos e magia amaldiçoada." );
				}
				else if ( reg.Name.Contains( "Pirate" ) || reg is PirateRegion )
				{
					Say( "Piratas navegam estas águas! Proteja sua carga e cuidado com grupos de abordagem." );
				}
				else if ( reg.Name.Contains( "Maze" ) || reg is MazeRegion )
				{
					Say( "Perdido no labirinto? Acompanhe seu caminho e cuidado com ilusões que podem enganar você!" );
				}
				else if ( reg.Name.Contains( "Abyss" ) )
				{
					Say( "Bem-vindo ao Abismo! As profundezas mais profundas guardam grandes perigos e maiores tesouros. Prepare-se!" );
				}
				else if ( reg.Name.Contains( "Ice" ) || reg.Name.Contains( "Frozen" ) )
				{
					Say( "O frio morde profundamente aqui! Vista-se com roupas quentes e cuidado com queimaduras de frio." );
				}
				else if ( reg.Name.Contains( "Fire" ) || reg.Name.Contains( "Volcano" ) )
				{
					Say( "Fogo e lava cercam você! Armaduras resistentes ao calor e poções de proteção contra fogo são essenciais aqui." );
				}
				else if ( reg.Name.Contains( "Swamp" ) || reg.Name.Contains( "Bog" ) )
				{
					Say( "Estas águas turvas escondem muitos perigos! Cuidado com plantas venenosas e criaturas que atacam por baixo." );
				}
				else if ( reg.Name.Contains( "Castle" ) || reg.Name.Contains( "Keep" ) )
				{
					Say( "Castelos antigos guardam segredos esquecidos! Esteja preparado para armadilhas, enigmas e poderosos guardiões." );
				}
				else if ( reg.Name.Contains( "Ruins" ) )
				{
					Say( "Estas ruínas em ruínas são instáveis! Cuidado com detritos caindo e maldições antigas." );
				}
				else if ( reg.Name.Contains( "Tomb" ) || reg.Name.Contains( "Crypt" ) )
				{
					Say( "Tumbas guardam espíritos inquietos! Magia sagrada e armas abençoadas lhe servirão bem aqui." );
				}
				else if ( reg.Name.Contains( "Mine" ) )
				{
					Say( "Túneis de mineração podem desmoronar! Traga picaretas, lanternas e esteja preparado para se desenterrar se necessário." );
				}
				else
				{
					// Fall back to regular TownHerald behavior
					base.OnMovement( m, oldLocation );
				}

				NextTalk = DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( 20, 45 ) );
			}
		}
		}

		private string GetDungeonMessage( Region reg )
		{
			string regionName = reg.Name.ToLower();

			if ( regionName.Contains( "deceit" ) || regionName.Contains( "wrong" ) )
				return "Bem-vindo a Deceit! As ilusões aqui podem enganar até o aventureiro mais sábio. Não confie em nada que vê!";
			else if ( regionName.Contains( "despise" ) )
				return "Despise aguarda! As piscinas ácidas aqui podem derreter armadura e carne. Pule com cuidado e traga resistência a ácido!";
			else if ( regionName.Contains( "destard" ) )
				return "As chamas de Destard queimam forte! Poções de resistência ao fogo e roupas frescas são seus melhores amigos aqui.";
			else if ( regionName.Contains( "shame" ) )
				return "Shame guarda muitos segredos! Procure portas escondidas e baús armadilhados, mas cuidado com os guardiões.";
			else if ( regionName.Contains( "hythloth" ) )
				return "Hythloth, o submundo! A masmorra mais profunda guarda os segredos mais sombrios. Traga muitos suprimentos!";
			else if ( regionName.Contains( "covetous" ) )
				return "Covetous está cheio de armadilhas e truques! Teste cada passo e verifique falsos pisos.";
			else if ( regionName.Contains( "wind" ) )
				return "A masmorra do vento! Correntes fortes podem jogá-lo para fora de penhascos. Mantenha-se no chão e mova-se com cuidado.";
			else if ( regionName.Contains( "fire" ) )
				return "Masmorra de fogo! Tudo queima aqui. Traga resistência ao fogo e extintores.";
			else if ( regionName.Contains( "ice" ) )
				return "Masmorra de gelo! O frio é mortal. Roupas quentes e armas baseadas em fogo ajudarão.";
			else if ( regionName.Contains( "doom" ) )
				return "Luva de Doom! Apenas os mais bravos entram aqui. Os artefatos dentro são lendários, mas os perigos também!";
			else if ( regionName.Contains( "bedlam" ) )
				return "O enigma de Bedlam! Resolva os enigmas para prosseguir, mas respostas erradas trazem consequências mortais.";
			else if ( regionName.Contains( "labyrinth" ) )
				return "O Labirinto! Este labirinto de loucura testará sua sanidade. Traga um mapa ou se perca para sempre.";
			else if ( regionName.Contains( "sanctuary" ) )
				return "Um santuário nas profundezas! Descanse aqui com segurança, mas cuidado - a segurança frequentemente atrai perigo.";
			else if ( regionName.Contains( "prison" ) || regionName.Contains( "jail" ) )
				return "Complexo prisional! Cuidado com presos que podem ter escapado, e tenha cuidado com patrulhas de guardas.";
			else
				return "Bem-vindo a esta masmorra! Fique alerta, trabalhe em conjunto e lembre-se - a discrição é a melhor parte da bravura!";
		}

		public DungeonGuide( Serial serial ) : base( serial )
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

	public class TownHerald : BasePerson
	{
		private DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }

		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public TownHerald() : base( )
		{
			NameHue = -1;

			InitStats( 100, 100, 25 );

			Title = "the town crier";
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();

			AddItem( new FancyShirt( Utility.RandomBlueHue() ) );

			Item skirt;

			switch ( Utility.Random( 2 ) )
			{
				case 0: skirt = new Skirt(); break;
				default: case 1: skirt = new Kilt(); break;
			}

			skirt.Hue = Utility.RandomGreenHue();

			AddItem( skirt );

			AddItem( new FeatheredHat( Utility.RandomGreenHue() ) );

			Item boots;

			switch ( Utility.Random( 2 ) )
			{
				case 0: boots = new Boots(); break;
				default: case 1: boots = new ThighBoots(); break;
			}

			AddItem( boots );
			AddItem( new LightCitizen( true ) );

			Utility.AssignRandomHair( this );
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, 10 ) && m is PlayerMobile )
			{
				// Turn to face the approaching player
				this.Direction = this.GetDirectionTo( m );

				if ( DateTime.UtcNow >= m_NextTalk )
				{
					Region reg = Region.Find( this.Location, this.Map );

					if (AdventuresFunctions.InfectedRegions.Count > 0)
					{
						if (AetherGlobe.carrier != null && Utility.RandomDouble() > 0.70)
							Say("Ive heard word of a being named " + AetherGlobe.carrier + " spreading a mysterious infection in the lands!");
						else
							Say("Infected have been seen in the lands! Help is needed!");
					}
					else if ( LoggingFunctions.LoggingEvents() == true )
					{
						string sEvents = LoggingFunctions.LogShout();
						Say( sEvents );
					}
					else
					{
						Say( "All is well in the land!" );
					}
					m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 30 ) ));
				}
			}
		}

		public override bool HandlesOnSpeech( Mobile from ) 
		{ 
			return true; 
		} 

		public override void OnSpeech( SpeechEventArgs e ) 
		{
			if( e.Mobile.InRange( this, 4 ))
			{
				if ( Insensitive.Contains( e.Speech, "infected") )  
				{
					TalkInfection();
				}
			}
		
		} 

		public void TalkInfection()
		{
			StringBuilder sb = new StringBuilder();

			if (AdventuresFunctions.InfectedRegions == null)
                sb.Append("Thank the balance! The infected are contained.");

			AdventuresFunctions.CheckInfection();

			if (AdventuresFunctions.InfectedRegions.Count > 0 )
			{
				sb.Append("Hear Ye! Infected have been spotted in ");

				for ( int i = 0; i < AdventuresFunctions.InfectedRegions.Count; i++ ) // load static regions
				{			
					String r = (String)AdventuresFunctions.InfectedRegions[i];

					if ( r != null || r != "" || r != " " )
						sb.Append( r + ", and " );
					else
						sb.Append( "an unknown location, and ");
				}

				sb.Append("help is urgently needed!");
			}
			else
				sb.Append("Thank the balance! The infected are contained.");

			Say( sb.ToString() );
		}

        public TownHerald(Serial serial) : base(serial)
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			//list.Add( new TownHeraldEntry( from, this ) ); 
		} 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
		
		public class TownHeraldEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public TownHeraldEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( LoggingFunctions.LoggingEvents() == true )
					{
						if ( ! mobile.HasGump( typeof( LoggingGumpCrier ) ) )
						{
							mobile.SendGump(new LoggingGumpCrier( mobile, 1 ));
						}
					}
					else
					{
						m_Giver.Say("Good day to you, " + m_Mobile.Name + ".");
					}
				}
            }
        }
	}  
}
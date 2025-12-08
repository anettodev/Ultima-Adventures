using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Misc;
using Server.Regions;
using Server.Spells.Syth;
using Server.Spells.Mystic;
using System.Collections;
using Server.Spells.Jedi;

namespace Server.Mobiles
{
	/// <summary>
	/// Animal Trainer NPC that provides pet stabling, claiming, and management services.
	/// Simple progressive stable limit system: 2-8 animals based on Animal Taming + Animal Lore skills (capped at 120 each).
	/// Handles gold-based transactions for pet care and provides speech-based commands for interaction.
	/// </summary>
	public class AnimalTrainer : BaseVendor
	{
		#region Constants

		// Costs
		private const int STABLE_COST_PER_PET = 250;
		private const int FIND_FOLLOWERS_COST = 1000;
		private const int TICKET_COST = 250;

		// Ranges
		private const int SPEECH_RANGE = 4;
		private const int CLAIM_RANGE = 14;
		private const int COMBATANT_RANGE = 12;
		private const int CONTEXT_MENU_RANGE = 12;

		// Skill Thresholds (Animal Taming + Animal Lore sum)
		private const double SKILL_SUM_LEVEL_7 = 240.0; // Max stabled: 8 (CAP - 120+120 skills)
		private const double SKILL_SUM_LEVEL_6 = 200.0; // Max stabled: 7
		private const double SKILL_SUM_LEVEL_5 = 160.0; // Max stabled: 6
		private const double SKILL_SUM_LEVEL_4 = 120.0; // Max stabled: 5
		private const double SKILL_SUM_LEVEL_3 = 80.0;  // Max stabled: 4
		private const double SKILL_SUM_LEVEL_2 = 40.0;  // Max stabled: 3
		private const double SKILL_SUM_LEVEL_1 = 0.0;   // Max stabled: 2
		// Simplified system - no skill bonuses to keep it capped at 8

		// Veterinary Skill Bonuses
		// Simplified system - no veterinary bonuses

		// Capacity Limits
		private const int MAX_STABLED_LEVEL_7 = 8;  // CAP
		private const int MAX_STABLED_LEVEL_6 = 7;
		private const int MAX_STABLED_LEVEL_5 = 6;
		private const int MAX_STABLED_LEVEL_4 = 5;
		private const int MAX_STABLED_LEVEL_3 = 4;
		private const int MAX_STABLED_LEVEL_2 = 3;
		private const int MAX_STABLED_LEVEL_1 = 2;

		// Speech Keywords
		private const int SPEECH_KEYWORD_STABLE = 0x0008; // *estabulo*
		private const int SPEECH_KEYWORD_CLAIM = 0x0009; // *retirar*


		// Gump Layout Constants
		private const int GUMP_X = 25;
		private const int GUMP_Y = 25;
		private const int GUMP_IMAGE_BACKGROUND = 155;
		private const int GUMP_IMAGE_BORDER_TL = 129;
		private const int GUMP_IMAGE_BORDER_TR = 129;
		private const int GUMP_IMAGE_BORDER_BL = 129;
		private const int GUMP_IMAGE_BORDER_BR = 129;
		private const int GUMP_IMAGE_HEADER = 133;
		private const int GUMP_IMAGE_DIVIDER = 132;
		private const int GUMP_IMAGE_BUTTON_BG = 134;
		private const int GUMP_IMAGE_FOOTER = 140;
		private const int GUMP_IMAGE_DECORATION = 139;
		private const int GUMP_IMAGE_BOTTOM = 147;

		// Gump Text Layout
		private const int GUMP_HEADER_X = 174;
		private const int GUMP_HEADER_Y = 68;
		private const int GUMP_HEADER_WIDTH = 300;
		private const int GUMP_HEADER_HEIGHT = 20;
		private const int GUMP_LIST_START_Y = 95;
		private const int GUMP_LIST_ITEM_HEIGHT = 35;
		private const int GUMP_PET_NAME_X = 145;
		private const int GUMP_PET_NAME_WIDTH = 425;
		private const int GUMP_BUTTON_X = 105;
		private const int GUMP_BUTTON_Y_OFFSET = 0;

		// Karma Range
		private const int KARMA_MIN = 13;
		private const int KARMA_MAX = -45;

		// Skills (Base Values)
		private const double SKILL_ANIMAL_LORE_MIN = 64.0;
		private const double SKILL_ANIMAL_LORE_MAX = 100.0;
		private const double SKILL_ANIMAL_TAMING_MIN = 90.0;
		private const double SKILL_ANIMAL_TAMING_MAX = 100.0;
		private const double SKILL_VETERINARY_MIN = 65.0;
		private const double SKILL_VETERINARY_MAX = 88.0;

		// Boots of Hermes Weight Threshold
		private const double BOOTS_HERMES_WEIGHT_THRESHOLD = 5.0;

		// Button IDs
		private const int GUMP_BUTTON_TYPE = 4005;


		#endregion

		#region PT-BR Strings

		// Property Values
		private const string NPC_TITLE = "o treinador de animais";
		private const string GUMP_TITLE_ANIMAL_COMPANIONS = "Companheiros Animais";
		private const string SPEECH_CATEGORY_PETS = "Animais de Estimação";
		private const string PET_LANGUAGE_MOUNT = "montaria";

		// User Messages
		private const string MSG_MOUNT_MOVED_TO_PACK = "Sua montaria foi movida para sua mochila.";
		private const string MSG_MOUNT_SAFELY_WAITING = "Sua montaria está esperando seguramente em outro lugar.";
		private const string MSG_ONLY_WORK_FOR_GOLD = "Eu só trabalho por ouro, Senhor.";
		private const string MSG_NEED_GOLD_INSTRUCTION = "Certifique-se de ter 1.000 moedas de ouro em sua mochila.";

		// NPC Dialogue
		private const string DIALOGUE_SIGH = "*suspiro*";
		private const string DIALOGUE_FIND_PETS = "Claro, deixe-me aventurar-me em terras perigosas, arriscando minha vida porque você é muito preguiçoso para fazer isso sozinho!";

		// World Regions (Keep English - Game World Specific)
		private const string REGION_SERPENT_ISLAND = "the Serpent Island";
		private const string REGION_LAND_OF_LODORIA = "the Land of Lodoria";
		private const string REGION_ISLES_OF_DREAD = "the Isles of Dread";
		private const string REGION_SAVAGED_EMPIRE = "the Savaged Empire";
		private const string REGION_HIDDEN_VALLEY = "the Hidden Valley";
		private const string REGION_CORRUPT_PASS = "the Corrupt Pass";
		private const string REGION_HEDGE_MAZE = "the Hedge Maze";
		private const string REGION_ALTAR_DRAGON_KING = "the Altar of the Dragon King";
		private const string REGION_ALTAR_BLOOD_GOD = "the Altar of the Blood God";
		private const string REGION_PORT = "the Port";
		private const string REGION_WATERFALL_CAVERN = "Waterfall Cavern";

		// Gump HTML Content
		private const string GUMP_HTML_HEADER = @"<BODY><BASEFONT Color=#FBFBFB><BIG>ANIMAIS NO ESTABULO</BIG></BASEFONT></BODY>";
		private const string GUMP_HTML_PET_NAME_START = @"<BODY><BASEFONT Color=#FCFF00><BIG>";
		private const string GUMP_HTML_PET_NAME_END = @"</BIG></BASEFONT></BODY>";

		// Pet Stabling Error Messages (PT-BR)
		private const string MSG_STABLE_ERROR_HUMAN = "HA HA HA! Desculpe, eu não sou uma pousada.";
		private const string MSG_STABLE_ERROR_NOT_CONTROLLED = "Você não pode estabular isso!";
		private const string MSG_STABLE_ERROR_WRONG_OWNER = "Você não é o dono desse animal de estimação!";
		private const string MSG_STABLE_ERROR_DEAD_PET = "Apenas animais vivos, por favor.";
		private const string MSG_STABLE_ERROR_SUMMONED = "Eu não posso estabular criaturas invocadas.";
		private const string MSG_STABLE_ERROR_HAS_ITEMS = "Você precisa descarregar seu animal.";
		private const string MSG_STABLE_ERROR_IN_COMBAT = "Desculpe, seu animal parece estar ocupado.";
		private const string MSG_STABLE_ERROR_TOO_MANY = "Você tem animais demais nos estábulos!";

		// Additional Messages (PT-BR)
		private const string MSG_PET_REMAINED_STABLED = "~1_NAME~ ficou nos estábulos porque você tem seguidores demais.";
		private const string MSG_HERE_YOU_GO = "Aqui está... e tenha um bom dia!";
		private const string MSG_NO_ANIMALS_STABLED = "Mas eu não tenho animais estabulados comigo no momento!";
		private const string MSG_PET_STABLED_SUCCESS = "Seu animal foi estabulado. Você pode recuperá-lo dizendo 'retirar' para mim. Em uma semana real, eu vou vendê-lo se não for retirado!";
		private const string MSG_NO_FUNDS_BACKPACK = "Mas você não tem o ouro na sua mochila!";
		private const string MSG_NO_GOLD_BACKPACK = "Você não tem ouro suficiente na sua mochila.";
		private const string MSG_TOO_FAR_AWAY = "Isso está muito longe.";

		// Ticket Messages (PT-BR)
		private const string MSG_TICKET_WHICH_ANIMAL = "Qual animal você gostaria de criar um ticket?";
		private const string MSG_TICKET_NO_GOLD = "Você não tem ouro suficiente na sua mochila! (250 moedas necessárias)";
		private const string MSG_TICKET_NOT_TAMABLE = "Apenas animais domados podem ser convertidos em ticket!";
		private const string MSG_TICKET_SUMMONED = "Criaturas invocadas não podem ser convertidas em ticket!";
		private const string MSG_TICKET_NOT_OWNER = "Você não é o dono desse animal!";
		private const string MSG_TICKET_NOT_ALIVE = "Apenas animais vivos podem ser convertidos em ticket!";
		private const string MSG_TICKET_SUCCESS = "Ticket criado com sucesso! O ticket expira em 15 dias.";
		private const string MSG_TICKET_EXPIRED = "Este ticket expirou e não pode mais ser usado.";


		#endregion

		#region Helper Methods

		/// <summary>
		/// Validates if player has enough gold for a service
		/// </summary>
		private static bool HasEnoughGold(Mobile from, int amount)
		{
			return from.Backpack != null && from.Backpack.GetAmount(typeof(Gold)) >= amount;
		}

		private static bool HasEnoughGoldAllowBank(Mobile from, int amount)
		{
			Container bank = from.FindBankNoCreate();
			return (from.Backpack != null && from.Backpack.GetAmount(typeof(Gold)) >= amount) ||
				   (bank != null && bank.GetAmount(typeof(Gold)) >= amount);
		}

		/// <summary>
		/// Consumes gold from player's backpack or bank
		/// </summary>
		private static bool ConsumeGold(Mobile from, int amount)
		{
			return from.Backpack != null && from.Backpack.ConsumeTotal(typeof(Gold), amount);
		}

		private static bool ConsumeGoldAllowBank(Mobile from, int amount)
		{
			Container bank = from.FindBankNoCreate();
			return (from.Backpack != null && from.Backpack.ConsumeTotal(typeof(Gold), amount)) ||
				   (bank != null && bank.ConsumeTotal(typeof(Gold), amount));
		}

		/// <summary>
		/// Formats the remaining time until pet expires from stable (7 days total)
		/// Returns formatted string with color coding (red if < 24h remaining)
		/// Note: Expired pets are removed from the list before this is called
		/// </summary>
		private static string FormatStableCountdown(BaseCreature pet)
		{
			if (!pet.IsStabled || pet.StabledDate == DateTime.MinValue)
				return "";

			TimeSpan timeStabled = DateTime.UtcNow - pet.StabledDate;
			TimeSpan timeRemaining = TimeSpan.FromDays(7.0) - timeStabled;

			// This shouldn't happen since expired pets are removed in BeginClaimList
			if (timeRemaining <= TimeSpan.Zero)
				return @"<BASEFONT COLOR=#FF0000>Expirado</BASEFONT>";

			bool isUrgent = timeRemaining.TotalHours < 24;

			int days = (int)timeRemaining.TotalDays;
			int hours = timeRemaining.Hours;
			int minutes = timeRemaining.Minutes;

			string timeString = "";
			if (days > 0)
				timeString += string.Format("{0}d ", days);
			if (hours > 0 || days > 0)
				timeString += string.Format("{0}h ", hours);
			timeString += string.Format("{0}m", minutes);

			if (isUrgent)
				return string.Format(@"<BASEFONT COLOR=#FF0000>(expira em {0})</BASEFONT>", timeString);
			else
				return string.Format(@"<BASEFONT COLOR=#FFFFFF>(expira em {0})</BASEFONT>", timeString);
		}

		/// <summary>
		/// Validates pet ownership and state for operations
		/// </summary>
		private static bool ValidatePetForOperation(Mobile from, BaseCreature pet)
		{
			if (pet == null || pet.Deleted)
				return false;
			if (from.Map != pet.Map || !from.InRange(pet, CLAIM_RANGE))
				return false;
			if (!from.Stabled.Contains(pet))
				return false;
			return from.CheckAlive();
		}

		#endregion

		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.DruidsGuild; } }

		/// <summary>
		/// Constructs a new AnimalTrainer NPC with default skills and properties
		/// </summary>
		[Constructable]
		public AnimalTrainer() : base( NPC_TITLE )
		{
			Job = JobFragment.animal;
			Karma = Utility.RandomMinMax( KARMA_MIN, KARMA_MAX );
			SetSkill( SkillName.AnimalLore, SKILL_ANIMAL_LORE_MIN, SKILL_ANIMAL_LORE_MAX );
			SetSkill( SkillName.AnimalTaming, SKILL_ANIMAL_TAMING_MIN, SKILL_ANIMAL_TAMING_MAX );
			SetSkill( SkillName.Veterinary, SKILL_VETERINARY_MIN, SKILL_VETERINARY_MAX );
		}

		/// <summary>
		/// Initializes the vendor's buy/sell information based on the current world region
		/// </summary>
		public override void InitSBInfo()
		{
			if ( Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == REGION_SERPENT_ISLAND )
			{
				m_SBInfos.Add( new SBGargoyleAnimalTrainer() );
			}
			else if ( Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == REGION_LAND_OF_LODORIA )
			{
				m_SBInfos.Add( new SBElfAnimalTrainer() );
			}
			else if ( Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == REGION_ISLES_OF_DREAD )
			{
				m_SBInfos.Add( new SBBarbarianAnimalTrainer() );
			}
			else if ( Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == REGION_SAVAGED_EMPIRE )
			{
				m_SBInfos.Add( new SBOrkAnimalTrainer() );
			}
			else
			{
				m_SBInfos.Add( new SBHumanAnimalTrainer() );
			}
			m_SBInfos.Add( new SBAnimalTrainer() ); 
			m_SBInfos.Add( new SBBuyArtifacts() ); 
		}

		public override VendorShoeType ShoeType
		{
			get{ return Female ? VendorShoeType.ThighBoots : VendorShoeType.Boots; }
		}

		public override int GetShoeHue()
		{
			return 0;
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( Utility.RandomBool() ? (Item)new QuarterStaff() : (Item)new ShepherdsCrook() );
		}


		#region Gumps

		private class ClaimListGump : Gump
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;
			private List<BaseCreature> m_List;

			public ClaimListGump( AnimalTrainer trainer, Mobile from, List<BaseCreature> list ) : base( GUMP_X, GUMP_Y )
			{
				m_Trainer = trainer;
				m_From = from;
				m_List = list;

				from.CloseGump( typeof( ClaimListGump ) );

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, GUMP_IMAGE_BACKGROUND);
				AddImage(300, 0, GUMP_IMAGE_BACKGROUND);
				AddImage(0, 300, GUMP_IMAGE_BACKGROUND);
				AddImage(300, 300, GUMP_IMAGE_BACKGROUND);
				AddImage(2, 2, GUMP_IMAGE_BORDER_TL);
				AddImage(298, 2, GUMP_IMAGE_BORDER_TR);
				AddImage(2, 298, GUMP_IMAGE_BORDER_BL);
				AddImage(298, 298, GUMP_IMAGE_BORDER_BR);
				AddImage(7, 8, GUMP_IMAGE_HEADER);
				AddImage(218, 47, GUMP_IMAGE_DIVIDER);
				AddImage(380, 8, GUMP_IMAGE_BUTTON_BG);
				AddImage(164, 551, GUMP_IMAGE_FOOTER);
				AddImage(8, 517, GUMP_IMAGE_DECORATION);
				AddImage(269, 342, GUMP_IMAGE_BOTTOM);
				AddHtml( GUMP_HEADER_X, GUMP_HEADER_Y, GUMP_HEADER_WIDTH, GUMP_HEADER_HEIGHT, GUMP_HTML_HEADER, (bool)false, (bool)false);

				// Add pet count label (current/max)
				int currentPets = m_From.Stabled.Count;
				int maxPets = GetMaxStabled(m_From);
				string petCountLabel = string.Format("<BODY><BASEFONT Color=#00FFFF><SMALL>TOTAL:{0}/{1}</SMALL></BASEFONT></BODY>", currentPets, maxPets);
				AddHtml( GUMP_HEADER_X, GUMP_HEADER_Y + 25, GUMP_HEADER_WIDTH, 20, petCountLabel, (bool)false, (bool)false);

				int y = 95;

				for ( int i = 0; i < list.Count; ++i )
				{
					BaseCreature pet = list[i];

					if ( pet == null || pet.Deleted )
						continue;

					y = y + 35;

					string petDisplay = pet.Name + " " + FormatStableCountdown(pet);
					AddHtml( GUMP_PET_NAME_X, y, GUMP_PET_NAME_WIDTH, 20, GUMP_HTML_PET_NAME_START + petDisplay + GUMP_HTML_PET_NAME_END, (bool)false, (bool)false);
					AddButton(GUMP_BUTTON_X, y, GUMP_BUTTON_TYPE, GUMP_BUTTON_TYPE, (i + 1), GumpButtonType.Reply, 0);
				}
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				int index = info.ButtonID - 1;

				if ( index >= 0 && index < m_List.Count )
					m_Trainer.EndClaimList( m_From, m_List[index] );
			}
		}




		private class LostPetsGump : Gump
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;
			private List<BaseCreature> m_LostPets;

			public LostPetsGump( AnimalTrainer trainer, Mobile from, List<BaseCreature> lostPets ) : base( 25, 25 )
			{
				m_Trainer = trainer;
				m_From = from;
				m_LostPets = lostPets;

				from.CloseGump( typeof( LostPetsGump ) );

				this.Closable = true;
				this.Disposable = true;
				this.Dragable = true;
				this.Resizable = false;

				AddPage(0);
				AddImage(0, 0, GUMP_IMAGE_BACKGROUND);
				AddImage(300, 0, GUMP_IMAGE_BACKGROUND);
				AddImage(0, 300, GUMP_IMAGE_BACKGROUND);
				AddImage(300, 300, GUMP_IMAGE_BACKGROUND);
				AddImage(2, 2, GUMP_IMAGE_BORDER_TL);
				AddImage(298, 2, GUMP_IMAGE_BORDER_TR);
				AddImage(2, 298, GUMP_IMAGE_BORDER_BL);
				AddImage(298, 298, GUMP_IMAGE_BORDER_BR);
				AddImage(7, 8, GUMP_IMAGE_HEADER);
				AddImage(218, 47, GUMP_IMAGE_DIVIDER);
				AddImage(380, 8, GUMP_IMAGE_BUTTON_BG);

				AddHtml( GUMP_HEADER_X, GUMP_HEADER_Y, GUMP_HEADER_WIDTH, GUMP_HEADER_HEIGHT, @"<BODY><BASEFONT Color=#FBFBFB><BIG>ANIMAIS PERDIDOS</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				// Add cost information
				AddHtml( GUMP_PET_NAME_X - 50, GUMP_HEADER_Y + 30, GUMP_PET_NAME_WIDTH + 50, 20, @"<BODY><BASEFONT Color=#00FFFF><SMALL>Custo: 1.000 moedas de ouro por animal</SMALL></BASEFONT></BODY>", (bool)false, (bool)false);

				int y = GUMP_LIST_START_Y + 10;

				if ( lostPets.Count == 0 )
				{
					AddHtml( GUMP_PET_NAME_X, y, GUMP_PET_NAME_WIDTH, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Nenhum animal perdido encontrado.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else
				{
					for ( int i = 0; i < lostPets.Count; ++i )
					{
						BaseCreature pet = lostPets[i];
					if ( pet == null || pet.Deleted )
						continue;

						y += GUMP_LIST_ITEM_HEIGHT;

						string petInfo = string.Format( "{0} (Distância: {1})",
							pet.Name,
							GetDistanceText( from, pet ) );

						AddHtml( GUMP_PET_NAME_X, y, GUMP_PET_NAME_WIDTH, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + petInfo + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(GUMP_BUTTON_X, y, GUMP_BUTTON_TYPE, GUMP_BUTTON_TYPE, (i + 1), GumpButtonType.Reply, 0);
					}
				}
			}

			private string GetDistanceText( Mobile from, BaseCreature pet )
			{
				if ( from.Map != pet.Map )
					return "Outro plano astral ou reino";

				int distance = (int)from.GetDistanceToSqrt( pet.Location );
				if ( distance < 20 )
					return "Muito Perto";
				else if ( distance < 80 )
					return "Próximo";
				else if ( distance < 150 )
					return "Longe";
				else
					return "Muito longe";
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( info.ButtonID > 0 && info.ButtonID <= m_LostPets.Count )
				{
					int petIndex = info.ButtonID - 1;
					BaseCreature selectedPet = m_LostPets[petIndex];

					if ( selectedPet != null && !selectedPet.Deleted )
					{
						m_Trainer.FetchLostPet( m_From, selectedPet );
					}
				}
			}
		}

		#endregion

		#region Static Utility Methods

		/// <summary>
		/// Calculates the maximum number of pets a player can have stabled based on their skills
		/// </summary>
		/// <param name="from">The player to calculate stable limit for</param>
		/// <returns>The maximum number of pets that can be stabled (2-8 based on skills, hard cap at 8)</returns>
		public static int GetMaxStabled( Mobile from )
		{
			double sklsum = CalculateSkillSum( from );
			int max = CalculateBaseStableLimit( sklsum );
			// Simplified: no bonuses to keep it simple and capped at 8
			return Math.Min(max, MAX_STABLED_LEVEL_7);
		}

		/// <summary>
		/// Calculates the sum of relevant skills (Animal Taming + Animal Lore) for stable limits
		/// </summary>
		private static double CalculateSkillSum( Mobile from )
		{
			return from.Skills[SkillName.AnimalTaming].Value +
				   from.Skills[SkillName.AnimalLore].Value;
		}

		/// <summary>
		/// Calculates base stable limit based on skill sum (2-8 animals, every 40 skill points)
		/// Max skill cap is 120 per skill, so max sum is 240 for 8 animals
		/// </summary>
		private static int CalculateBaseStableLimit( double sklsum )
		{
			if ( sklsum >= SKILL_SUM_LEVEL_7 )
				return MAX_STABLED_LEVEL_7;  // 8 animals (CAP)
			if ( sklsum >= SKILL_SUM_LEVEL_6 )
				return MAX_STABLED_LEVEL_6;  // 7 animals
			if ( sklsum >= SKILL_SUM_LEVEL_5 )
				return MAX_STABLED_LEVEL_5;  // 6 animals
			if ( sklsum >= SKILL_SUM_LEVEL_4 )
				return MAX_STABLED_LEVEL_4;  // 5 animals
			if ( sklsum >= SKILL_SUM_LEVEL_3 )
				return MAX_STABLED_LEVEL_3;  // 4 animals
			if ( sklsum >= SKILL_SUM_LEVEL_2 )
				return MAX_STABLED_LEVEL_2;  // 3 animals
			return MAX_STABLED_LEVEL_1;      // 2 animals (minimum)
		}


		private class StableTarget : Target
		{
			private AnimalTrainer m_Trainer;

			public StableTarget( AnimalTrainer trainer ) : base( CONTEXT_MENU_RANGE, false, TargetFlags.None )
			{
				m_Trainer = trainer;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is BaseCreature )
					m_Trainer.EndStable( from, (BaseCreature)targeted );
				else if ( targeted == from )
					m_Trainer.SayTo( from, MSG_STABLE_ERROR_HUMAN );
				else
					m_Trainer.SayTo( from, MSG_STABLE_ERROR_NOT_CONTROLLED );
			}
		}

		private class TicketTarget : Target
		{
			private AnimalTrainer m_Trainer;

			public TicketTarget( AnimalTrainer trainer ) : base( CONTEXT_MENU_RANGE, false, TargetFlags.None )
			{
				m_Trainer = trainer;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is BaseCreature )
					m_Trainer.EndCreateTicket( from, (BaseCreature)targeted );
				else if ( targeted == from )
					m_Trainer.SayTo( from, MSG_STABLE_ERROR_HUMAN );
				else
					m_Trainer.SayTo( from, MSG_TICKET_NOT_TAMABLE );
			}
		}
		
		private void CloseClaimList( Mobile from )
		{
			from.CloseGump( typeof( ClaimListGump ) );
		}

		public static void DismountPlayer( Mobile m )
		{
			CleanClaimList( m );

			if ( m.Mount is EtherealMount )
			{
				IMount mount = m.Mount;
				EtherealMount ethy = (EtherealMount)mount;
				Server.Mobiles.EtherealMount.Dismount( m );
				m.SendMessage( MSG_MOUNT_MOVED_TO_PACK );
			}
			else if ( m.Mount is BaseMount )
			{
				BaseCreature pet = (BaseCreature)(m.Mount);
				Server.Mobiles.BaseMount.Dismount( m );

				pet.ControlTarget = null;
				//pet.ControlOrder = OrderType.Stay;
				pet.Internalize();
				pet.SetControlMaster( null );
				pet.SummonMaster = null;
				pet.IsStabled = true;
				//pet.Loyalty = BaseCreature.MaxLoyalty;
				pet.Language = PET_LANGUAGE_MOUNT;

				m.Stabled.Add( pet );

				m.SendMessage( MSG_MOUNT_SAFELY_WAITING );
			}
		}

		public static bool IsBeingFast( Mobile from )
		{
			if ( from.Mounted )
				return true;

			Item shoes = from.FindItemOnLayer( Layer.Shoes );
			if ( shoes is BootsofHermes && shoes.Weight < BOOTS_HERMES_WEIGHT_THRESHOLD )
				return true;

			if ( from is PlayerMobile)
			{
				if ( Spells.Syth.SythSpeed.UnderEffect( (PlayerMobile)from ) )
					return true;

				if ( Spells.Mystic.WindRunner.UnderEffect( (PlayerMobile)from ) )
					return true;
				
				if ( Spells.Jedi.Celerity.UnderEffect( (PlayerMobile)from ) )
					return true;

				if ( ((PlayerMobile)from).sbmaster && ((PlayerMobile)from).sbmasterspeed)
					return true;

				Server.Spells.Ninjitsu.AnimalFormContext context = Server.Spells.Ninjitsu.AnimalForm.GetContext(from);
				if (context != null && context.SpeedBoost)
					return true;
				
			}

			return false;
		}

		public static void CleanClaimList( Mobile from )
		{
			for ( int i = from.Stabled.Count - 1; i >= 0; --i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
				}
				else
				{
					pet.Language = null;
				}
			}
		}

		public static void GetLastMounted( Mobile from )
		{
			bool hasMount = false;
			BaseCreature lastMount = null;

			// First pass: find the last mount and clean up deleted pets
			for ( int i = from.Stabled.Count - 1; i >= 0; --i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
					continue;
				}

				if ( pet.Language == PET_LANGUAGE_MOUNT && pet is BaseMount )
				{
					lastMount = pet;
					break; // Found the last mount, no need to continue
				}
			}

			// Second pass: try to claim the last mount if conditions are met
			if ( lastMount != null && CanGetLastMounted( from, lastMount ) )
			{
				lastMount.SetControlMaster( from );
				lastMount.ControlTarget = from;
				lastMount.MoveToWorld( from.Location, from.Map );
				lastMount.IsStabled = false;
				//lastMount.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully Happy

				from.Stabled.Remove( lastMount );
				lastMount.Language = null;
				Server.Mobiles.BaseMount.Ride( (BaseMount)lastMount, from );
					hasMount = true;
				}
			else if ( lastMount != null )
				{
				// Reset language if mount exists but couldn't be claimed
				lastMount.Language = null;
			}

			Server.Mobiles.AnimalTrainer.CleanClaimList( from );

			if ( !hasMount )
			{
				ArrayList ethy = new ArrayList();
				foreach ( Item item in World.Items.Values )
				{
					if ( item is EtherealMount )
					{
						if ( ((EtherealMount)item).Owner == from )
						{
							((EtherealMount)item).Rider = from;
							((EtherealMount)item).Owner = from;
						}
					}
				}
			}
		}

		public static bool CanGetLastMounted( Mobile from, BaseCreature pet )
		{
			return ((from.Followers + pet.ControlSlots) <= from.FollowersMax);
		}

		public static bool IsNoMountRegion( Region reg )
		{

			if ( reg.IsPartOf( REGION_HIDDEN_VALLEY ) )
				return true;

			if ( reg.IsPartOf( REGION_CORRUPT_PASS ) )
				return true;

			if ( reg.IsPartOf( REGION_HEDGE_MAZE ) )
				return true;

			// if ( reg.IsPartOf( "o Santuário Druida" ) )
			//	return true;

			if ( reg.IsPartOf( REGION_ALTAR_DRAGON_KING ) )
				return true;

			if ( reg.IsPartOf( REGION_ALTAR_BLOOD_GOD ) )
				return true;

			if ( !reg.IsPartOf( REGION_PORT ) && !reg.IsPartOf( REGION_WATERFALL_CAVERN ) && ( reg is ProtectedRegion || reg is CaveRegion || reg is BardDungeonRegion || reg is MoonCore || reg is UmbraRegion || reg is DungeonRegion || reg is PublicRegion ) )

					return true;

			if ( reg is ProtectedRegion || reg is CaveRegion || reg is BardDungeonRegion || reg is MoonCore || reg is UmbraRegion || reg is DungeonRegion || reg is PublicRegion )
					return true;

			return false;
		}

		public static bool AllowMagicSpeed( Mobile m, Region reg )
		{
			if ( reg.IsPartOf( REGION_PORT ) || reg is ProtectedRegion || reg is PublicRegion )
					return true;

			return false;
		}

		#endregion

		#region Core Functionality

		/// <summary>
		/// Displays a gump showing all stabled pets for the player to select from
		/// </summary>
		/// <param name="from">The player requesting the claim list</param>
		public void BeginClaimList( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			List<BaseCreature> list = new List<BaseCreature>();

			for ( int i = 0; i < from.Stabled.Count; ++i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					from.Stabled.RemoveAt( i );
					--i;
					continue;
				}

				// Check if pet has expired (7 days)
				if ( pet.IsStabled && pet.StabledDate != DateTime.MinValue )
				{
					TimeSpan timeStabled = DateTime.UtcNow - pet.StabledDate;
					if ( timeStabled >= TimeSpan.FromDays( 7.0 ) )
					{
						// Pet has expired - remove from stable list and delete from world
						from.Stabled.RemoveAt( i );
						pet.Delete();
						--i;
						continue;
					}
				}

				list.Add( pet );
			}

			if ( list.Count > 0 )
				from.SendGump( new ClaimListGump( this, from, list ) );
			else
				SayTo( from, MSG_NO_ANIMALS_STABLED );
		}

		/// <summary>
		/// Processes the claiming of a specific pet from the stables
		/// </summary>
		/// <param name="from">The player claiming the pet</param>
		/// <param name="pet">The pet being claimed</param>
		public void EndClaimList( Mobile from, BaseCreature pet )
		{
			if ( pet == null || pet.Deleted || from.Map != this.Map || !from.Stabled.Contains( pet ) || !from.CheckAlive() )
				return;
			
			if ( !from.InRange( this, CLAIM_RANGE ) )
			{
				from.SendMessage( MSG_TOO_FAR_AWAY );
				return;
			}

			if ( CanClaim( from, pet ) )
			{
				DoClaim( from, pet );

				from.Stabled.Remove( pet );
			}
			else
			{
				SayTo( from, string.Format(MSG_PET_REMAINED_STABLED, pet.Name) );
			}
		}

		/// <summary>
		/// Initiates the pet stabling process by prompting the player to select a pet
		/// </summary>
		/// <param name="from">The player wanting to stable a pet</param>
		public void BeginStable( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			if ( !HasEnoughGold( from, STABLE_COST_PER_PET ) )
			{
				SayTo( from, MSG_NO_GOLD_BACKPACK );
			}
			else
			{
				SayTo( from, "Eu cobro 250 moedas de ouro por animal para uma semana de estábulo. Qual animal você gostaria de guardar aqui?" );

				from.Target = new StableTarget( this );
			}
		}

		public void EndStable( Mobile from, BaseCreature pet )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			string errorMessage = ValidatePetForStabling( from, pet );
			if ( errorMessage != null )
			{
				SayTo( from, errorMessage );
				return;
			}

			if ( !ConsumeGold( from, STABLE_COST_PER_PET ) )
			{
				SayTo( from, MSG_NO_FUNDS_BACKPACK );
				return;
			}

			StabilizePet( from, pet );
			SayTo( from, MSG_PET_STABLED_SUCCESS );
		}




		/// <summary>
		/// Validates if a pet can be stabled
		/// </summary>
		/// <returns>Error message if validation fails, null if valid</returns>
		private string ValidatePetForStabling( Mobile from, BaseCreature pet )
		{
			if ( pet.Body.IsHuman )
				return MSG_STABLE_ERROR_HUMAN;

			if ( !pet.Controlled )
				return MSG_STABLE_ERROR_NOT_CONTROLLED;

			if ( pet.ControlMaster != from )
				return MSG_STABLE_ERROR_WRONG_OWNER;

			if ( pet.IsDeadPet )
				return MSG_STABLE_ERROR_DEAD_PET;

			if ( pet.Summoned )
				return MSG_STABLE_ERROR_SUMMONED;

			if ( (pet is PackLlama || pet is PackHorse || pet is Beetle) && (pet.Backpack != null && pet.Backpack.Items.Count > 0) )
				return MSG_STABLE_ERROR_HAS_ITEMS;

			if ( pet.Combatant != null && pet.InRange( pet.Combatant, COMBATANT_RANGE ) && pet.Map == pet.Combatant.Map )
				return MSG_STABLE_ERROR_IN_COMBAT;

			if ( from.Stabled.Count >= GetMaxStabled( from ) )
				return MSG_STABLE_ERROR_TOO_MANY;

			return null;
		}

		/// <summary>
		/// Performs the actual pet stabling process
		/// </summary>
		private void StabilizePet( Mobile from, BaseCreature pet )
				{
					pet.Language = null;
					pet.ControlTarget = null;
					pet.ControlOrder = OrderType.Stay;
					pet.Internalize();

					pet.SetControlMaster( null );
					pet.SummonMaster = null;
					pet.IsStabled = true;

					if ( Core.SE )	
						pet.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully happy

					from.Stabled.Add( pet );
		}

		public void Claim( Mobile from )
		{
			Claim( from, null );
		}

		public void Claim( Mobile from, string petName )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			bool claimed = false;
			int stabled = 0;
			
			bool claimByName = ( petName != null );

			for ( int i = 0; i < from.Stabled.Count; ++i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
					--i;
					continue;
				}

				++stabled;

				if ( claimByName && !Insensitive.Equals( pet.Name, petName ) )
					continue;

				if ( CanClaim( from, pet ) )
				{
					DoClaim( from, pet );

					from.Stabled.RemoveAt( i );
					--i;

					claimed = true;
				}
				else
				{
					SayTo( from, string.Format(MSG_PET_REMAINED_STABLED, pet.Name) );
				}
			}

			if ( claimed )
				SayTo( from, MSG_HERE_YOU_GO );
			else if ( stabled == 0 )
				SayTo( from, MSG_NO_ANIMALS_STABLED );
			else if ( claimByName )
				BeginClaimList( from );
		}

		public bool CanClaim( Mobile from, BaseCreature pet )
		{
			return ((from.Followers + pet.ControlSlots) <= from.FollowersMax);
		}

		private void DoClaim( Mobile from, BaseCreature pet )
		{
			pet.SetControlMaster( from );

			if ( pet.Summoned )
				pet.SummonMaster = from;

			pet.Language = null;
			pet.ControlTarget = from;
			pet.ControlOrder = OrderType.Follow;

			pet.MoveToWorld( from.Location, from.Map );

			pet.IsStabled = false;

			pet.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully Happy
		}

		#endregion

		#region Speech & Interaction

		public override bool HandlesOnSpeech( Mobile from )
		{
			return true;
		}

		/// <summary>
		/// Handles speech commands for AnimalTrainer interactions
		/// Available commands (PT-BR):
		/// - "estabulo" : Stable pets
		/// - "retirar" : Claim pets from stable
		/// - "buscar" : Find lost pets
		/// - "treinar" : Train skills
		/// </summary>
		public class SpeechGumpEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;

			public SpeechGumpEntry( Mobile from, Mobile giver )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;

				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( GUMP_TITLE_ANIMAL_COMPANIONS, SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, SPEECH_CATEGORY_PETS ) ));
					}
				}
            }
        }

		/// <summary>
		/// Context menu entry for stabling pets
		/// </summary>
		public class StableEntry : ContextMenuEntry
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;

			public StableEntry( AnimalTrainer trainer, Mobile from ) : base( 6126, 12 )
			{
				m_Trainer = trainer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Trainer.CloseClaimList( m_From );
				m_Trainer.BeginStable( m_From );
			}
		}

		/// <summary>
		/// Context menu entry for claiming specific pets (shows gump)
		/// </summary>
		public class ClaimSpecificEntry : ContextMenuEntry
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;

			public ClaimSpecificEntry( AnimalTrainer trainer, Mobile from ) : base( 6127, 12 )
			{
				m_Trainer = trainer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Trainer.CloseClaimList( m_From );
				m_Trainer.BeginClaimList( m_From );
			}
		}

		/// <summary>
		/// Context menu entry for claiming all pets at once
		/// </summary>
		public class ClaimAllEntry : ContextMenuEntry
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;

			public ClaimAllEntry( AnimalTrainer trainer, Mobile from ) : base( 6508, 12 )
			{
				m_Trainer = trainer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Trainer.CloseClaimList( m_From );
				m_Trainer.Claim( m_From );
			}
		}

		/// <summary>
		/// Context menu entry for creating a pet ticket
		/// </summary>
		public class TicketEntry : ContextMenuEntry
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;

			public TicketEntry( AnimalTrainer trainer, Mobile from ) : base( 3006097, 12 )
			{
				m_Trainer = trainer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Trainer.CloseClaimList( m_From );
				m_Trainer.BeginCreateTicket( m_From );
			}
		}






		public class SpeechGumpEntryAnimals : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;
            
            public SpeechGumpEntryAnimals( Mobile from, Mobile giver ) : base( 6146, 3 )
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
                        mobile.SendGump(new SpeechGump( "Centro de treinamento de animais!", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Pets" ) ));
                    }
                }
            }
        }

        public class RidingGumpEntry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;
            
            public RidingGumpEntry( Mobile from, Mobile giver ) : base( 6098, 3 )
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
                    if ( ! mobile.HasGump( typeof( Server.Mobiles.Veterinarian.RidingGump ) ) )
                    {
                        mobile.SendGump(new Server.Mobiles.Veterinarian.RidingGump());
                    }
                }
            }
        }

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive )
			{
				base.GetContextMenuEntries( from, list ); 
				// Add stable-related entries
				list.Add( new StableEntry( this, from ) );

				if ( from.Stabled.Count > 0 )
				{
					list.Add( new ClaimSpecificEntry( this, from ) );
					if ( from.Stabled.Count > 1 ) // Only show "Claim All" if more than 1 pet
						list.Add( new ClaimAllEntry( this, from ) );
				}

				list.Add( new TicketEntry( this, from ) );
				list.Add( new SpeechGumpEntryAnimals( from, this ) ); 
            	//list.Add( new RidingGumpEntry( from, this ) );

			}
		}

		/// <summary>
		/// Handles speech commands for AnimalTrainer interactions
		/// Available commands:
		/// - "estabulo" / "stable" : Stable pets
		/// - "retirar" : Show claim list gump (select specific pet)
		/// - "retirar todos" / "claim all" : Claim all pets from stable
		/// - "retirar [pet name]" / "claim [pet name]" : Claim specific pet by name
		/// - "buscar" / "procurar" / "find" / "fetch" : Find lost pets
		/// - "treinar" / "train" : Train skills
		/// </summary>
		public override void OnSpeech( SpeechEventArgs e )
		{
			if( e.Mobile.InRange( this, SPEECH_RANGE ) && !e.Handled )
			{
				if ( HandleStableCommand( e ) || HandleClaimCommand( e ) || HandleFindCommand( e ) || HandleTrainCommand( e ) || HandleTicketCommand( e ) )
					return;
				}

			base.OnSpeech( e );
		}

		/// <summary>
		/// Handles the stable speech command
		/// </summary>
		private bool HandleStableCommand( SpeechEventArgs e )
		{
			if ( !Insensitive.Contains( e.Speech, "estabulo" ) && !Insensitive.Contains( e.Speech, "guardar" ) && !Insensitive.Contains( e.Speech, "stable" ) )
				return false;

					e.Handled = true;
			CloseClaimList( e.Mobile );
			BeginStable( e.Mobile );
			return true;
		}

		/// <summary>
		/// Handles the claim speech command
		/// </summary>
		private bool HandleClaimCommand( SpeechEventArgs e )
		{
			if ( !Insensitive.Contains( e.Speech, "retirar" ) && !Insensitive.Contains( e.Speech, "claim" ) )
				return false;

			e.Handled = true;
			CloseClaimList( e.Mobile );

			// Check for "retirar todos" or "claim all" - claim all pets
			if ( Insensitive.Contains( e.Speech, "retirar todos" ) || Insensitive.Contains( e.Speech, "claim all" ) )
			{
				Claim( e.Mobile );
			}
			// Check for specific pet name after "retirar" or "claim"
			else if ( Insensitive.Contains( e.Speech, "retirar " ) || Insensitive.Contains( e.Speech, "claim " ) )
			{
				int index = e.Speech.IndexOf( ' ' );
				if ( index != -1 )
					Claim( e.Mobile, e.Speech.Substring( index ).Trim() );
				else
					BeginClaimList( e.Mobile ); // Show gump if no specific name
			}
			// Just "retirar" or "claim" - show claim list gump
			else
			{
				BeginClaimList( e.Mobile );
			}

			return true;
		}

		/// <summary>
		/// Handles the find/fetch speech command
		/// </summary>
		private bool HandleFindCommand( SpeechEventArgs e )
		{
			if ( !Insensitive.Contains( e.Speech, "buscar" ) && !Insensitive.Contains( e.Speech, "procurar" ) &&
				 !Insensitive.Contains( e.Speech, "find" ) && !Insensitive.Contains( e.Speech, "fetch" ) )
				return false;
						
						Mobile from = e.Mobile;

			// Show gump with lost pets (payment happens when selecting a pet)
			List<BaseCreature> lostPets = GetLostPets( from );
			if ( lostPets.Count > 0 )
			{
				from.SendGump( new LostPetsGump( this, from, lostPets ) );
						}
						else
						{
				SayTo( from, "Você não tem animais perdidos para buscar." );
				from.SendMessage( "Certifique-se de que seus animais não estão estabulados ou montados." );
			}

			return true;
		}

		/// <summary>
		/// Handles the train speech command
		/// </summary>
		private bool HandleTrainCommand( SpeechEventArgs e )
		{
			if ( !Insensitive.Contains( e.Speech, "treinar" ) && !Insensitive.Contains( e.Speech, "train" ) )
				return false;

			e.Handled = true;
			BeginTraining( e.Mobile );
			return true;
		}

		/// <summary>
		/// Handles the ticket speech command
		/// </summary>
		private bool HandleTicketCommand( SpeechEventArgs e )
		{
			if ( !Insensitive.Contains( e.Speech, "ticket" ) )
				return false;

			e.Handled = true;
			BeginCreateTicket( e.Mobile );
			return true;
		}

		/// <summary>
		/// Handles the "comandos" speech command for AnimalTrainer specific commands
		/// </summary>
		protected override bool HandleComandosCommand(SpeechEventArgs e)
		{
			if (!Insensitive.Contains(e.Speech, "comandos") && !Insensitive.Contains(e.Speech, "commands"))
				return false;

			e.Handled = true;

			StringBuilder commands = new StringBuilder();
			commands.Append("Comandos aceitos são:\n\n");

			// First call base to get general vendor commands
			base.HandleComandosCommand(e);

			// Add AnimalTrainer specific commands
			commands.Append("\nComandos específicos do Treinador de Animais:\n");
			commands.Append("- \"Estabulo\" / \"Guardar\" / \"Stable\" : Estabula animais\n");
			commands.Append("- \"Retirar\" : Mostra lista de animais estabulados\n");
			commands.Append("- \"Retirar Todos\" / \"Claim All\" : Retira todos os animais\n");
			commands.Append("- \"Retirar [nome]\" : Retira animal específico por nome\n");
			commands.Append("- \"Buscar\" / \"Procurar\" / \"Find\" / \"Fetch\" : Busca animais perdidos\n");
			commands.Append("- \"Treinar\" / \"Train\" : Treina habilidades de animais\n");
			commands.Append("- \"Ticket\" : Cria um ticket de um animal domado (250 moedas de ouro, expira em 15 dias)\n");

			e.Mobile.SendMessage(0x5A, commands.ToString().TrimEnd());
			return true;
		}

		/// <summary>
		/// Gets a list of the player's lost pets (not stabled, not controlled, not mounted, and far away)
		/// </summary>
		private List<BaseCreature> GetLostPets( Mobile from )
		{
			List<BaseCreature> lostPets = new List<BaseCreature>();

			// Search through all mobiles in the world
			foreach ( Mobile m in World.Mobiles.Values )
			{
				BaseCreature creature = m as BaseCreature;
				if ( creature != null && creature.ControlMaster == from )
				{
					// Check if pet is not stabled, not controlled by player, not mounted, and far away
					if ( !creature.IsStabled &&
						 //creature.Controlled == false &&
						 creature != from.Mount &&
						 creature.Map != Map.Internal &&
						 creature.GetDistanceToSqrt( from.Location ) > 10.0 )
					{
						lostPets.Add( creature );
					}
				}
			}

			return lostPets;
		}

		/// <summary>
		/// Teleports a lost pet to the player's location (consumes gold)
		/// </summary>
		private void FetchLostPet( Mobile from, BaseCreature pet )
		{
			if ( pet == null || pet.Deleted || pet.ControlMaster != from )
				return;

			// Consume gold for the service
			if ( !ConsumeGold( from, FIND_FOLLOWERS_COST ) )
			{
				from.SendMessage( MSG_ONLY_WORK_FOR_GOLD );
				from.SendMessage( MSG_NEED_GOLD_INSTRUCTION );
				return;
			}

			// Teleport the pet to the player's location
			pet.MoveToWorld( from.Location, from.Map );

			// Restore control
			pet.Controlled = true;
			pet.ControlMaster = from;
			pet.ControlOrder = OrderType.Follow;

			// Set loyalty
			BaseCreature bc = pet as BaseCreature;
			if ( bc != null )
			{
				bc.Loyalty = BaseCreature.MaxLoyalty;
			}

			from.SendMessage( string.Format( "Seu animal {0} foi encontrado e trazido até você.", pet.Name ) );
		}

		/// <summary>
		/// Initiates the ticket creation process by prompting the player to select a pet
		/// </summary>
		/// <param name="from">The player wanting to create a ticket</param>
		public void BeginCreateTicket( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			if ( !HasEnoughGold( from, TICKET_COST ) )
			{
				SayTo( from, MSG_TICKET_NO_GOLD );
			}
			else
			{
				SayTo( from, MSG_TICKET_WHICH_ANIMAL );
				from.Target = new TicketTarget( this );
			}
		}

		/// <summary>
		/// Processes the creation of a ticket from a specific pet
		/// </summary>
		/// <param name="from">The player creating the ticket</param>
		/// <param name="pet">The pet being converted to a ticket</param>
		public void EndCreateTicket( Mobile from, BaseCreature pet )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			string errorMessage = ValidatePetForTicket( from, pet );
			if ( errorMessage != null )
			{
				SayTo( from, errorMessage );
				return;
			}

			if ( !ConsumeGold( from, TICKET_COST ) )
			{
				SayTo( from, MSG_NO_FUNDS_BACKPACK );
				return;
			}

			// Create the ticket
			Server.Items.PetTicket ticket = new Server.Items.PetTicket( pet );
			
			// Add to player's backpack
			if ( from.Backpack != null )
			{
				from.Backpack.DropItem( ticket );
			}
			else
			{
				ticket.MoveToWorld( from.Location, from.Map );
			}

			// Delete the original creature
			pet.Delete();

			SayTo( from, MSG_TICKET_SUCCESS );
		}

		/// <summary>
		/// Validates if a pet can be converted to a ticket
		/// </summary>
		/// <returns>Error message if validation fails, null if valid</returns>
		private string ValidatePetForTicket( Mobile from, BaseCreature pet )
		{
			if ( pet == null || pet.Deleted )
				return MSG_TICKET_NOT_TAMABLE;

			if ( pet.Body.IsHuman )
				return MSG_STABLE_ERROR_HUMAN;

			if ( !pet.Controlled )
				return MSG_TICKET_NOT_TAMABLE;

			if ( pet.ControlMaster != from )
				return MSG_TICKET_NOT_OWNER;

			if ( pet.IsDeadPet || !pet.Alive )
				return MSG_TICKET_NOT_ALIVE;

			if ( pet.Summoned )
				return MSG_TICKET_SUMMONED;

			return null;
		}

		public AnimalTrainer( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

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

		#endregion
	}
}


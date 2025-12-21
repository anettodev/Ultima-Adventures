using System;
using Server;
using System.Collections;
using Server.Misc;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Items;

namespace Server.Misc
{
    class GetPlayerInfo
    {
		#region Public Skill Title Methods

		public static string GetSkillTitle( Mobile m )
		{
			bool isOriental = Server.Misc.GetPlayerInfo.OrientalPlay( m );
			bool isEvil = Server.Misc.GetPlayerInfo.EvilPlay( m );
			int isBarbaric = Server.Misc.GetPlayerInfo.BarbaricPlay( m );

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( DB.CharacterSkill == PlayerInfoConstants.TITAN_OF_ETHER_SKILL_ID )
			{
				return PlayerInfoStringConstants.TITLE_TITAN_OF_ETHER;
			}
			else if ( m.SkillsTotal > 0 )
			{
				Skill highest = GetShowingSkill( m, DB );

				if ( highest != null )
				{
					if ( highest.Value < PlayerInfoConstants.MIN_SKILL_FOR_TITLE )
					{
						return PlayerInfoStringConstants.TITLE_VILLAGE_IDIOT;
					}
					else
					{
						string skillLevel = null;
						if ( highest.Value < PlayerInfoConstants.ASPIRING_SKILL_THRESHOLD ){ skillLevel = PlayerInfoStringConstants.LEVEL_ASPIRING; }
						else { skillLevel = GetSkillLevel( highest ); }

						string skillTitle = highest.Info.Title;

						/* DEFAULT TITLES FOR SKILLS...
						Alchemy				Alchemist
						Anatomy				Biologist
						Animal Lore			Naturalist
						Animal Taming		Tamer
						Archery				Archer
						Arms Lore			Weapon Master
						Begging				Beggar
						Blacksmithy			Blacksmith
						Bowcraft/Fletching	Bowyer
						Bushido				Samurai
						Camping				Explorer
						Carpentry			Carpenter
						Cartography			Cartographer
						Chivalry			Paladin
						Cooking				Chef
						Detecting Hidden	Scout
						Discordance			Demoralizer
						Evaluating Int		Scholar
						Fencing				Fencer
						Fishing				Fisherman
						Focus				Driven
						Forensic Eval		Detective
						Healing				Healer
						Herding				Shepherd
						Hiding				Shade
						Imbuing				Artificer
						Inscription			Scribe
						Item ID				Merchant
						Lockpicking			Infiltrator
						Lumberjacking		Lumberjack
						Mace Fighting		Armsman
						Magery				Mage
						Meditation			Stoic
						Mining				Miner
						Musicianship		Bard
						Mysticism			Mystic
						Necromancy			Necromancer
						Ninjitsu			Ninja
						Parrying			Duelist
						Peacemaking			Pacifier
						Poisoning			Assassin
						Provocation			Rouser
						Remove Trap			Trap Specialist
						Resisting Spells	Warder
						Snooping			Spy
						Spellweaving		Arcanist
						Spirit Speak		Medium
						Stealing			Pickpocket
						Stealth				Rogue
						Swordsmanship		Swordsman
						Tactics				Tactician
						Tailoring			Tailor
						Taste ID			Praegustator
						Throwing			Bladeweaver
						Tinkering			Tinker
						Tracking			Ranger
						Veterinary			Veterinarian
						Wrestling			Wrestler
						*/

						// Apply title transformations in order of priority
						skillTitle = ApplyBarbaricTransformations( skillTitle, m, isBarbaric );
						skillTitle = ApplyArchmageTransformation( skillTitle, m, isOriental );
						skillTitle = ApplyMonkTransformation( skillTitle, m );
						skillTitle = ApplySythJediScholarTransformation( skillTitle, m );
						skillTitle = ApplyJediPaladinTransformation( skillTitle, m );
					skillTitle = ApplySythPaladinTransformation( skillTitle, m );
					skillTitle = ApplyOrientalTransformations( skillTitle, m, isOriental );
					skillTitle = ApplyDefaultTransformations( skillTitle, m, isEvil );
					skillTitle = ApplyPostTransformations( skillTitle, m, isBarbaric, isOriental ); 
					
					if ( skillTitle.Contains("Alchemist") || 
						skillTitle.Contains("Naturalist") || 
						skillTitle.Contains("Tamer") || 
						skillTitle.Contains("Archer") || 
						skillTitle.Contains("Explorer") || 
						skillTitle.Contains("Paladin") || 
						skillTitle.Contains("Fencer") || 
						skillTitle.Contains("Healer") || 
						skillTitle.Contains("Shepherd") || 
						skillTitle.Contains("Armsman") || 
						skillTitle.Contains("Mage") || 
						skillTitle.Contains("Bard") || 
						skillTitle.Contains("Necromancer") || 
						skillTitle.Contains("Fishing") || 
						skillTitle.Contains("Ranger") || 
						skillTitle.Contains("Duelist") || 
						skillTitle.Contains("Swordsman") || 
						skillTitle.Contains("Weapon Master") || 
						skillTitle.Contains("Tactician") || 
						skillTitle.Contains("Veterinarian") )
						{
							if ( skillTitle.Contains("Alchemist") )
							{
								skillTitle = skillTitle.Replace("Alchemist", PlayerInfoStringConstants.TITLE_BARBARIC_ALCHEMIST);
							}
							else if ( skillTitle.Contains("Naturalist") )
							{
								skillTitle = skillTitle.Replace("Naturalist", PlayerInfoStringConstants.TITLE_BARBARIC_BEASTMASTER);
							}
							else if ( skillTitle.Contains("Tamer") )
							{
								skillTitle = skillTitle.Replace("Tamer", PlayerInfoStringConstants.TITLE_BARBARIC_TAMER);
							}
							else if ( skillTitle.Contains("Shepherd") )
							{
								skillTitle = skillTitle.Replace("Shepherd", PlayerInfoStringConstants.TITLE_BARBARIC_SHEPHERD);
							}
							else if ( skillTitle.Contains("Fishing") )
							{
								if ( m.Skills[SkillName.Fishing].Value >= PlayerInfoConstants.MIN_SKILL_FOR_CAPTAIN )
								{
									skillTitle = skillTitle.Replace("Fishing", PlayerInfoStringConstants.TITLE_ATLANTEAN);
								}
								else
								{
									skillTitle = skillTitle.Replace("Fishing", PlayerInfoStringConstants.TITLE_FISHERMAN);
								}
							}
							else if ( skillTitle.Contains("Veterinarian") )
							{
								skillTitle = skillTitle.Replace("Veterinarian", PlayerInfoStringConstants.TITLE_VETERINARIAN);
							}
							else if ( skillTitle.Contains("Explorer") )
							{
								skillTitle = skillTitle.Replace("Explorer", PlayerInfoStringConstants.TITLE_EXPLORER);
							}
							else if ( skillTitle.Contains("Paladin") )
							{
								if ( m.Karma < PlayerInfoConstants.KARMA_NEUTRAL )
								{
									skillTitle = skillTitle.Replace("Paladin", PlayerInfoStringConstants.TITLE_DEATH_KNIGHT);
								}
								else
								{
									skillTitle = skillTitle.Replace("Paladin", PlayerInfoStringConstants.TITLE_PALADIN);
								}
							}
							else if ( skillTitle.Contains("Tactician") )
							{
								skillTitle = skillTitle.Replace("Tactician", PlayerInfoStringConstants.TITLE_GENERAL);
							}
							else if ( skillTitle.Contains("Duelist") )
							{
								skillTitle = skillTitle.Replace("Duelist", PlayerInfoStringConstants.TITLE_DUELIST);
							}
							else if ( skillTitle.Contains("Necromancer") )
							{
								skillTitle = skillTitle.Replace("Necromancer", PlayerInfoStringConstants.TITLE_OCCULTIST);
							}
							else if ( skillTitle.Contains("Bard") )
							{
								skillTitle = skillTitle.Replace("Bard", PlayerInfoStringConstants.TITLE_BARD);
							}
							else if ( skillTitle.Contains("Mage") )
							{
								skillTitle = skillTitle.Replace("Mage", PlayerInfoStringConstants.TITLE_ARCANIST);
							}
							else if ( skillTitle.Contains("Healer") )
							{
								if ( m.Female )
								{
									skillTitle = skillTitle.Replace("Healer", PlayerInfoStringConstants.TITLE_HEALER_FEMALE);
								}
								else
								{
									skillTitle = skillTitle.Replace("Healer", PlayerInfoStringConstants.TITLE_HEALER_MALE);
								}
							}
							else if ( skillTitle.Contains("Archer") && isBarbaric > 1 )
							{
								skillTitle = skillTitle.Replace("Archer", PlayerInfoStringConstants.TITLE_ARCHER_AMAZON);
							}
							else if ( skillTitle.Contains("Fencer") && isBarbaric > 1 )
							{
								skillTitle = skillTitle.Replace("Fencer", PlayerInfoStringConstants.TITLE_FENCER_AMAZON);
							}
							else if ( skillTitle.Contains("Armsman") && isBarbaric > 1 )
							{
								skillTitle = skillTitle.Replace("Armsman", PlayerInfoStringConstants.TITLE_ARMSMAN_AMAZON);
							}
							else if ( skillTitle.Contains("Swordsman") && isBarbaric > 1 )
							{
								skillTitle = skillTitle.Replace("Swordsman", PlayerInfoStringConstants.TITLE_SWORDSMAN_AMAZON);
							}
							else if ( skillTitle.Contains("Archer") )
							{
								skillTitle = skillTitle.Replace("Archer", PlayerInfoStringConstants.TITLE_BARBARIC_ARCHER);
							}
							else if ( skillTitle.Contains("Fencer") )
							{
								skillTitle = skillTitle.Replace("Fencer", PlayerInfoStringConstants.TITLE_BARBARIC_FENCER);
							}
							else if ( skillTitle.Contains("Armsman") )
							{
								skillTitle = skillTitle.Replace("Armsman", PlayerInfoStringConstants.TITLE_BARBARIC_ARMSMAN);
							}
							else if ( skillTitle.Contains("Swordsman") )
							{
								skillTitle = skillTitle.Replace("Swordsman", PlayerInfoStringConstants.TITLE_BARBARIC_SWORDSMAN);
							}
							else if ( skillTitle.Contains("Ranger") )
							{
								skillTitle = skillTitle.Replace("Ranger", PlayerInfoStringConstants.TITLE_RANGER);
							}
							else if ( skillTitle.Contains("Weapon Master") )
							{
								skillTitle = skillTitle.Replace("Weapon Master", PlayerInfoStringConstants.TITLE_GLADIATOR);
							}
						}
						else if ( !isOriental && skillTitle.Contains("Mage") && m.Skills[SkillName.Magery].Base >= PlayerInfoConstants.MIN_SKILL_FOR_ARCHMAGE && m.Skills[SkillName.Necromancy].Base >= PlayerInfoConstants.MIN_SKILL_FOR_ARCHMAGE )
						{
							skillTitle = skillTitle.Replace("Mage", PlayerInfoStringConstants.TITLE_ARCHMAGE);
						}
						else if ( !isOriental && skillTitle.Contains("Necromancer") && m.Skills[SkillName.Magery].Base >= PlayerInfoConstants.MIN_SKILL_FOR_ARCHMAGE && m.Skills[SkillName.Necromancy].Base >= PlayerInfoConstants.MIN_SKILL_FOR_ARCHMAGE )
						{
							skillTitle = skillTitle.Replace("Necromancer", PlayerInfoStringConstants.TITLE_ARCHMAGE);
						}

						else if ( ( skillTitle.Contains("Wrestler") ) && isMonk(m) )
						{
							skillTitle = skillTitle.Replace("Wrestler", PlayerInfoStringConstants.TITLE_MONK);
							if ( m.Skills[SkillName.Magery].Base >= PlayerInfoConstants.MIN_SKILL_FOR_MYSTIC || m.Skills[SkillName.Necromancy].Base >= PlayerInfoConstants.MIN_SKILL_FOR_MYSTIC )
							{
								skillTitle = skillTitle.Replace(PlayerInfoStringConstants.TITLE_MONK, PlayerInfoStringConstants.TITLE_MYSTIC);
							}
						}
						else if ( ( skillTitle.Contains("Scholar") ) && isSyth(m,false) )
						{
							skillTitle = skillTitle.Replace("Scholar", "Syth");
						}
						else if ( ( skillTitle.Contains("Scholar") ) && isJedi(m,false) )
						{
							skillTitle = skillTitle.Replace("Scholar", "Jedi");
						}
						
					// Jedi titles - ordered from highest to lowest
						else if ( ( skillTitle.Contains("Paladin") ) && isJedi(m,false) ) 
						
			{
				string jedi = PlayerInfoStringConstants.TITLE_JEDI_YOUNGLING;
				
				// Check conditions from highest to lowest
				if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
				{
					jedi = PlayerInfoStringConstants.TITLE_GRAND_MASTER;
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
				{
					jedi = PlayerInfoStringConstants.TITLE_MASTER_OF_ORDER;
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
				{
					jedi = PlayerInfoStringConstants.TITLE_JEDI_COUNCIL_MEMBER;
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
				{
					jedi = PlayerInfoStringConstants.TITLE_JEDI_MASTER;
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_CONSULAR || m.Karma >= PlayerInfoConstants.KARMA_JEDI_CONSULAR )
				{
					jedi = PlayerInfoStringConstants.TITLE_JEDI_CONSULAR;
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_PADAWAN || m.Karma >= PlayerInfoConstants.KARMA_JEDI_PADAWAN )
				{
					jedi = PlayerInfoStringConstants.TITLE_JEDI_GUARDIAN;
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_PADAWAN || m.Karma >= PlayerInfoConstants.KARMA_JEDI_PADAWAN )
				{
					jedi = PlayerInfoStringConstants.TITLE_JEDI_SENTINEL;
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_PADAWAN || m.Karma >= PlayerInfoConstants.KARMA_JEDI_PADAWAN )
				{
					jedi = PlayerInfoStringConstants.TITLE_JEDI_PADAWAN;
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.KARMA_NEUTRAL || m.Karma >= PlayerInfoConstants.KARMA_NEUTRAL )
				{
					jedi = PlayerInfoStringConstants.TITLE_JEDI_SERVICE_CORPS;
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.KARMA_NEUTRAL || m.Karma >= PlayerInfoConstants.KARMA_NEUTRAL )
				{
					jedi = PlayerInfoStringConstants.TITLE_JEDI_KNIGHT;
				}
				else if ( m.Skills[SkillName.Chivalry].Base < PlayerInfoConstants.KARMA_NEUTRAL || m.Karma < PlayerInfoConstants.KARMA_NEUTRAL )
				{
					jedi = PlayerInfoStringConstants.TITLE_JEDI_TROLL;
				}
				
				skillTitle = skillTitle.Replace("Paladin", jedi);
			}
			
			// Syth titles - ordered from highest to lowest (work in progress)
						else if ( ( skillTitle.Contains("Paladin") ) && isSyth(m,false) ) 
						
			{
				string syth = PlayerInfoStringConstants.TITLE_SYTH_HOPEFUL;
				
				// Check conditions from highest to lowest
				if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
				{
					syth = PlayerInfoStringConstants.TITLE_GRAND_MASTER;
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
				{
					syth = PlayerInfoStringConstants.TITLE_MASTER_OF_ORDER;
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
				{
					syth = PlayerInfoStringConstants.TITLE_SYTH_COUNCIL_MEMBER;
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
				{
					syth = PlayerInfoStringConstants.TITLE_SYTH_MASTER;
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_CONSULAR || m.Karma >= PlayerInfoConstants.KARMA_JEDI_CONSULAR )
				{
					syth = PlayerInfoStringConstants.TITLE_SYTH_HOPEFUL; // Placeholder - needs proper title
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_PADAWAN || m.Karma >= PlayerInfoConstants.KARMA_JEDI_PADAWAN )
				{
					syth = PlayerInfoStringConstants.TITLE_SYTH_HOPEFUL; // Placeholder - needs proper title
				}
				else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.KARMA_NEUTRAL || m.Karma >= PlayerInfoConstants.KARMA_NEUTRAL )
				{
					syth = PlayerInfoStringConstants.TITLE_SYTH_HOPEFUL; // Placeholder - needs proper title
				}
				else if ( m.Skills[SkillName.Chivalry].Base < PlayerInfoConstants.KARMA_NEUTRAL || m.Karma < PlayerInfoConstants.KARMA_NEUTRAL )
				{
					syth = PlayerInfoStringConstants.TITLE_SYTH_TROLL;
				}
				
				skillTitle = skillTitle.Replace("Paladin", syth);
			}
			
			
			
			
			
			
						
						else if ( skillTitle.Contains("Naturalist") )
						{ 
							if ( m.Karma < 0 ){ skillTitle = skillTitle.Replace("Naturalist", PlayerInfoStringConstants.TITLE_INDUSTRIALIST); }
						}
						else if ( skillTitle.Contains("Beggar") && isJester(m) ){ skillTitle = skillTitle.Replace("Beggar", PlayerInfoStringConstants.TITLE_JESTER); }
						else if ( skillTitle.Contains("Scholar") && isJester(m) ){ skillTitle = skillTitle.Replace("Scholar", PlayerInfoStringConstants.TITLE_CLOWN); }
						else if ( skillTitle.Contains("Samurai") && m.Karma < 0 ){ skillTitle = skillTitle.Replace("Samurai", PlayerInfoStringConstants.TITLE_RONIN); }
						else if ( skillTitle.Contains("Ninja") && m.Karma < 0 ){ skillTitle = skillTitle.Replace("Ninja", PlayerInfoStringConstants.TITLE_YAKUZA); }
						else if ( skillTitle.Contains("Mage") && isOriental == true ){ skillTitle = skillTitle.Replace("Mage", PlayerInfoStringConstants.TITLE_ORIENTAL_MAGE); }
						else if ( skillTitle.Contains("Swordsman") && isOriental == true ){ skillTitle = skillTitle.Replace("Swordsman", PlayerInfoStringConstants.TITLE_ORIENTAL_SWORDSMAN); }
						else if ( skillTitle.Contains("Healer") && isOriental == true ){ skillTitle = skillTitle.Replace("Healer", PlayerInfoStringConstants.TITLE_ORIENTAL_HEALER); }
						else if ( skillTitle.Contains("Necromancer") && isOriental == true ){ skillTitle = skillTitle.Replace("Necromancer", PlayerInfoStringConstants.TITLE_ORIENTAL_NECROMANCER); }
						else if ( skillTitle.Contains("Alchemist") && isOriental == true ){ skillTitle = skillTitle.Replace("Alchemist", PlayerInfoStringConstants.TITLE_ORIENTAL_ALCHEMIST); }
						else if ( skillTitle.Contains("Medium") && isOriental == true ){ skillTitle = skillTitle.Replace("Medium", PlayerInfoStringConstants.TITLE_ORIENTAL_MEDIUM); }
						else if ( skillTitle.Contains("Archer") && isOriental == true ){ skillTitle = skillTitle.Replace("Archer", PlayerInfoStringConstants.TITLE_ORIENTAL_ARCHER); }
						else if ( skillTitle.Contains("Fencer") && isOriental == true ){ skillTitle = skillTitle.Replace("Fencer", PlayerInfoStringConstants.TITLE_ORIENTAL_FENCER); }
						else if ( skillTitle.Contains("Armsman") && isOriental == true ){ skillTitle = skillTitle.Replace("Armsman", PlayerInfoStringConstants.TITLE_ORIENTAL_ARMSMAN); }
						else if ( skillTitle.Contains("Tactician") && isOriental == true ){ skillTitle = skillTitle.Replace("Tactician", PlayerInfoStringConstants.TITLE_ORIENTAL_TACTICIAN); }
						else if ( skillTitle.Contains("Paladin") && isOriental == true ){ skillTitle = skillTitle.Replace("Paladin", PlayerInfoStringConstants.TITLE_ORIENTAL_PALADIN); }

						else if ( ( skillTitle.Contains("Healer") || skillTitle.Contains("Medium") ) && m.Karma >= PlayerInfoConstants.KARMA_PRIEST && m.Skills[SkillName.Healing].Base >= PlayerInfoConstants.MIN_SKILL_FOR_PRIEST && m.Skills[SkillName.SpiritSpeak].Base >= PlayerInfoConstants.MIN_SKILL_FOR_PRIEST )
						{
							skillTitle = skillTitle.Replace("Medium", PlayerInfoStringConstants.TITLE_PRIEST);
							skillTitle = skillTitle.Replace("Healer", PlayerInfoStringConstants.TITLE_PRIEST);

							if ( isOriental == true ){ skillTitle = skillTitle.Replace("Priest", PlayerInfoStringConstants.TITLE_BUDDHIST); }
						}

						else if ( skillTitle.Contains("Wrestler") && isOriental == true ){ skillTitle = skillTitle.Replace("Wrestler", PlayerInfoStringConstants.TITLE_ORIENTAL_WRESTLER); }
						else if ( skillTitle.Contains("Detective") ){ skillTitle = skillTitle.Replace("Detective", PlayerInfoStringConstants.TITLE_UNDERTAKER); }
						else if ( skillTitle.Contains("Shade") ){ skillTitle = skillTitle.Replace("Shade", PlayerInfoStringConstants.TITLE_STEALTH); }
						else if ( skillTitle.Contains("Rogue") ){ skillTitle = skillTitle.Replace("Rogue", PlayerInfoStringConstants.TITLE_ROGUE); }
						//else if ( skillTitle.Contains("Infiltrator") ){ skillTitle = skillTitle.Replace("Infiltrator", "Lockpicker"); }
						else if ( skillTitle.Contains("Mage") && isEvil == true && m.Body == PlayerInfoConstants.BODY_FEMALE ){ skillTitle = skillTitle.Replace("Mage", PlayerInfoStringConstants.TITLE_ENCHANTRESS); }
						else if ( skillTitle.Contains("Mage") && isEvil == true ){ skillTitle = skillTitle.Replace("Mage", PlayerInfoStringConstants.TITLE_WARLOCK); }
						else if ( skillTitle.Contains("Mage") ){ skillTitle = skillTitle.Replace("Mage", PlayerInfoStringConstants.TITLE_WIZARD); }
						else if ( skillTitle.Contains("Paladin") ){ skillTitle = skillTitle.Replace("Paladin", PlayerInfoStringConstants.TITLE_KNIGHT); }
						else if ( skillTitle.Contains("Tamer") )
						{ 
							if ( m.Karma < PlayerInfoConstants.KARMA_SLAVER ){ skillTitle = skillTitle.Replace("Tamer", PlayerInfoStringConstants.TITLE_SLAVER); }
							else { skillTitle = skillTitle.Replace("Tamer", PlayerInfoStringConstants.TITLE_BEASTMASTER); }
						}
						else if ( skillTitle.Contains("Pickpocket") ){ skillTitle = skillTitle.Replace("Pickpocket", PlayerInfoStringConstants.TITLE_THIEF); }
						else if ( skillTitle.Contains("Fisherman") ){ skillTitle = skillTitle.Replace("Fisherman", PlayerInfoStringConstants.TITLE_SAILOR); }
						else if ( skillTitle.Contains("Stoic") ){ skillTitle = skillTitle.Replace("Stoic", PlayerInfoStringConstants.TITLE_MEDITATOR); }
						//else if ( skillTitle.Contains("Armsman") ){ skillTitle = skillTitle.Replace("Armsman", "Mace Fighter"); }
						else if ( skillTitle.Contains("Wrestler") ){ skillTitle = skillTitle.Replace("Wrestler", PlayerInfoStringConstants.TITLE_BRAWLER); }
						//else if ( skillTitle.Contains("Praegustator") ){ skillTitle = skillTitle.Replace("Praegustator", "Food Taster"); }
						else if ( skillTitle.Contains("Warder") ){ skillTitle = skillTitle.Replace("Warder", PlayerInfoStringConstants.TITLE_SPELL_WARDER); }

						if ( isBarbaric == 0 )
						{
							if ( skillTitle.Contains("Wizard") && m.Body == PlayerInfoConstants.BODY_FEMALE ){ skillTitle = skillTitle.Replace("Wizard", PlayerInfoStringConstants.TITLE_SORCERESS); }
							if ( skillTitle.Contains("Necromancer") && m.Body == PlayerInfoConstants.BODY_FEMALE ){ skillTitle = skillTitle.Replace("Necromancer", PlayerInfoStringConstants.TITLE_WITCH); }
							if ( skillTitle.Contains("Knight") && m.Karma < PlayerInfoConstants.KARMA_NEUTRAL ){ skillTitle = skillTitle.Replace("Knight", PlayerInfoStringConstants.TITLE_DEATH_KNIGHT_POST); }
							if ( skillTitle.Contains("Healer") && m.Karma < PlayerInfoConstants.KARMA_NEUTRAL ){ skillTitle = skillTitle.Replace("Healer", PlayerInfoStringConstants.TITLE_MORTICIAN); }
						}

						if ( skillTitle.Contains("Sailor") && m.Karma < PlayerInfoConstants.KARMA_NEUTRAL ){ skillTitle = skillTitle.Replace("Sailor", PlayerInfoStringConstants.TITLE_PIRATE); }
						if ( skillTitle.Contains("Sailor") && m.Skills[SkillName.Fishing].Value >= PlayerInfoConstants.MIN_SKILL_FOR_CAPTAIN ){ skillTitle = skillTitle.Replace("Sailor", PlayerInfoStringConstants.TITLE_SHIP_CAPTAIN); }
						if ( skillTitle.Contains("Pirate") && m.Skills[SkillName.Fishing].Value >= PlayerInfoConstants.MIN_SKILL_FOR_CAPTAIN ){ skillTitle = skillTitle.Replace("Pirate", PlayerInfoStringConstants.TITLE_PIRATE_CAPTAIN); }

						return String.Concat( skillLevel, " ", skillTitle );
					}
				}
			}

			return PlayerInfoStringConstants.TITLE_VILLAGE_IDIOT;
		}

		#endregion

		#region Private Skill Title Helpers

		private static Skill GetHighestSkill( Mobile m )
		{
			int points = 0;

			Spellbook book = Spellbook.FindMystic( m );
			if ( book is MysticSpellbook )
			{
				MysticSpellbook tome = (MysticSpellbook)book;
				if ( tome.owner == m )
				{
					points++;
				}
			}

			if ( Server.Spells.Mystic.MysticSpell.MonkNotIllegal( m ) ){ points++; }

			if ( points > 1 )
				return true;

			return false;
		}

		public static bool isFromSpace( Mobile m )
		{
			if ( m.Skills.Cap >= PlayerInfoConstants.SKILLS_CAP_SPACE )
				return true;

			return false;
		}

		public static bool isSyth ( Mobile m, bool checkSword )
		{
			int points = 0;

			Spellbook book = Spellbook.FindSyth( m );
			if ( book is SythSpellbook )
			{
				SythSpellbook tome = (SythSpellbook)book;
				if ( tome.owner == m )
				{
					points++;
				}
			}

			if ( Server.Spells.Syth.SythSpell.SythNotIllegal( m, checkSword ) ){ points++; }

			if ( points > 1 )
				return true;

			return false;
		}

		public static bool isJedi ( Mobile m, bool checkSword )
		{
			int points = 0;

			Spellbook book = Spellbook.FindJedi( m );
			if ( book is JediSpellbook )
			{
				JediSpellbook tome = (JediSpellbook)book;
				if ( tome.owner == m )
				{
					points++;
				}
			}

			if ( Server.Spells.Jedi.JediSpell.JediNotIllegal( m, checkSword ) ){ points++; }

			if ( points > 1 )
				return true;

			return false;
		}
		

		
		public static bool isJester ( Mobile from )
		{
			int points = 0;

			if ( from is PlayerMobile && from != null )
			{
				foreach( Item i in from.Backpack.FindItemsByType( typeof( BagOfTricks ), true ) )
				{
					if ( i != null ){ points = 1; }
				}

				if ( from.Skills[SkillName.Begging].Value > PlayerInfoConstants.MIN_SKILL_FOR_JESTER || from.Skills[SkillName.EvalInt].Value > PlayerInfoConstants.MIN_SKILL_FOR_JESTER )
				{
					points++;
				}

				if ( from.FindItemOnLayer( Layer.OuterTorso ) != null )
				{
					Item robe = from.FindItemOnLayer( Layer.OuterTorso );
					if ( robe.ItemID == 0x1f9f || robe.ItemID == 0x1fa0 || robe.ItemID == 0x4C16 || robe.ItemID == 0x4C17 || robe.ItemID == 0x2B6B || robe.ItemID == 0x3162 )
						points++;
				}
				if ( from.FindItemOnLayer( Layer.MiddleTorso ) != null )
				{
					Item shirt = from.FindItemOnLayer( Layer.MiddleTorso );
					if ( shirt.ItemID == 0x1f9f || shirt.ItemID == 0x1fa0 || shirt.ItemID == 0x4C16 || shirt.ItemID == 0x4C17 || shirt.ItemID == 0x2B6B || shirt.ItemID == 0x3162 )
						points++;
				}
				if ( from.FindItemOnLayer( Layer.Helm ) != null )
				{
					Item hat = from.FindItemOnLayer( Layer.Helm );
					if ( hat.ItemID == 0x171C || hat.ItemID == 0x4C15 )
						points++;
				}
				if ( from.FindItemOnLayer( Layer.Shoes ) != null )
				{
					Item feet = from.FindItemOnLayer( Layer.Shoes );
					if ( feet.ItemID == 0x4C27 )
						points++;
				}
			}

			if ( points > PlayerInfoConstants.MIN_POINTS_FOR_SPECIAL )
				return true;

			return false;
		}

		#endregion

		private static Skill GetHighestSkill( Mobile m )
		{
			Skills skills = m.Skills;

			if ( !Core.AOS )
				return skills.Highest;

			Skill highest = m.Skills[SkillName.Wrestling];

			for ( int i = 0; i < m.Skills.Length; ++i )
			{
				Skill check = m.Skills[i];

				if ( highest == null || check.Value > highest.Value )
					highest = check;
				else if ( highest != null && highest.Lock != SkillLock.Up && check.Lock == SkillLock.Up && check.Value == highest.Value )
					highest = check;
			}

			return highest;
		}

		private static string[,] m_Levels = new string[,]
			{
				{ "Neophyte",		"Neophyte",		"Neophyte"		},
				{ "Novice",			"Novice",		"Novice"		},
				{ "Apprentice",		"Apprentice",	"Apprentice"	},
				{ "Journeyman",		"Journeyman",	"Journeyman"	},
				{ "Expert",			"Expert",		"Expert"		},
				{ "Adept",			"Adept",		"Adept"			},
				{ "Master",			"Master",		"Master"		},
				{ "Grandmaster",	"Grandmaster",	"Grandmaster"	},
				{ "Elder",			"Tatsujin",		"Shinobi"		},
				{ "Legendary",		"Kengo",		"Ka-ge"			}
			};

		private static string GetSkillLevel( Skill skill )
		{
			return m_Levels[GetTableIndex( skill ), GetTableType( skill )];
		}

		private static int GetTableType( Skill skill )
		{
			switch ( skill.SkillName )
			{
				default: return 0;
				case SkillName.Bushido: return 1;
				case SkillName.Ninjitsu: return 2;
			}
		}

		private static int GetTableIndex( Skill skill )
		{
			int fp = 0;

			if ( skill.Value >= PlayerInfoConstants.SKILL_LEVEL_120 ){ fp = 9; }
			else if ( skill.Value >= PlayerInfoConstants.SKILL_LEVEL_110 ){ fp = 8; }
			else if ( skill.Value >= PlayerInfoConstants.SKILL_LEVEL_100 ){ fp = 7; }
			else if ( skill.Value >= PlayerInfoConstants.SKILL_LEVEL_90 ){ fp = 6; }
			else if ( skill.Value >= PlayerInfoConstants.SKILL_LEVEL_80 ){ fp = 5; }
			else if ( skill.Value >= PlayerInfoConstants.SKILL_LEVEL_70 ){ fp = 4; }
			else if ( skill.Value >= PlayerInfoConstants.SKILL_LEVEL_60 ){ fp = 3; }
			else if ( skill.Value >= PlayerInfoConstants.SKILL_LEVEL_50 ){ fp = 2; }
			else if ( skill.Value >= PlayerInfoConstants.SKILL_LEVEL_40 ){ fp = 1; }
			else { fp = 0; }

			return fp;
		}

		private static Skill GetShowingSkill( Mobile m, CharacterDatabase DB )
		{
			Skill skill = GetHighestSkill( m );

			if ( DB != null )
			{
				int NskillShow = DB.CharacterSkill;

				if ( NskillShow > 0 )
				{
					SkillName skillName;
					if ( SkillIndexMap.TryGetValue( NskillShow, out skillName ) )
					{
						skill = m.Skills[skillName];
					}
					else
					{
						skill = GetHighestSkill( m );
					}
				}
			}

			return skill;
		}

		#region Title Transformation Methods

		/// <summary>
		/// Applies barbaric title transformations based on player's barbaric level.
		/// </summary>
		/// <param name="skillTitle">The current skill title</param>
		/// <param name="m">The mobile</param>
		/// <param name="isBarbaric">Barbaric level (0 = none, 1 = barbaric, 2 = amazon)</param>
		/// <returns>Transformed title</returns>
		private static string ApplyBarbaricTransformations( string skillTitle, Mobile m, int isBarbaric )
		{
			if ( isBarbaric <= 0 )
				return skillTitle;

			// Check if title is eligible for barbaric transformation
			if ( !( skillTitle.Contains("Alchemist") || 
					skillTitle.Contains("Naturalist") || 
					skillTitle.Contains("Tamer") || 
					skillTitle.Contains("Archer") || 
					skillTitle.Contains("Explorer") || 
					skillTitle.Contains("Paladin") || 
					skillTitle.Contains("Fencer") || 
					skillTitle.Contains("Healer") || 
					skillTitle.Contains("Shepherd") || 
					skillTitle.Contains("Armsman") || 
					skillTitle.Contains("Mage") || 
					skillTitle.Contains("Bard") || 
					skillTitle.Contains("Necromancer") || 
					skillTitle.Contains("Fishing") || 
					skillTitle.Contains("Ranger") || 
					skillTitle.Contains("Duelist") || 
					skillTitle.Contains("Swordsman") || 
					skillTitle.Contains("Weapon Master") || 
					skillTitle.Contains("Tactician") || 
					skillTitle.Contains("Veterinarian") ) )
			{
				return skillTitle;
			}

			// Apply transformations
			if ( skillTitle.Contains("Alchemist") )
			{
				skillTitle = skillTitle.Replace("Alchemist", PlayerInfoStringConstants.TITLE_BARBARIC_ALCHEMIST);
			}
			else if ( skillTitle.Contains("Naturalist") )
			{
				skillTitle = skillTitle.Replace("Naturalist", PlayerInfoStringConstants.TITLE_BARBARIC_BEASTMASTER);
			}
			else if ( skillTitle.Contains("Tamer") )
			{
				skillTitle = skillTitle.Replace("Tamer", PlayerInfoStringConstants.TITLE_BARBARIC_TAMER);
			}
			else if ( skillTitle.Contains("Shepherd") )
			{
				skillTitle = skillTitle.Replace("Shepherd", PlayerInfoStringConstants.TITLE_BARBARIC_SHEPHERD);
			}
			else if ( skillTitle.Contains("Fishing") )
			{
				if ( m.Skills[SkillName.Fishing].Value >= PlayerInfoConstants.MIN_SKILL_FOR_CAPTAIN )
				{
					skillTitle = skillTitle.Replace("Fishing", PlayerInfoStringConstants.TITLE_ATLANTEAN);
				}
				else
				{
					skillTitle = skillTitle.Replace("Fishing", PlayerInfoStringConstants.TITLE_FISHERMAN);
				}
			}
			else if ( skillTitle.Contains("Veterinarian") )
			{
				skillTitle = skillTitle.Replace("Veterinarian", PlayerInfoStringConstants.TITLE_VETERINARIAN);
			}
			else if ( skillTitle.Contains("Explorer") )
			{
				skillTitle = skillTitle.Replace("Explorer", PlayerInfoStringConstants.TITLE_EXPLORER);
			}
			else if ( skillTitle.Contains("Paladin") )
			{
				if ( m.Karma < PlayerInfoConstants.KARMA_NEUTRAL )
				{
					skillTitle = skillTitle.Replace("Paladin", PlayerInfoStringConstants.TITLE_DEATH_KNIGHT);
				}
				// else if ( isBarbaric > 1 )
				// {
				// 	skillTitle = skillTitle.Replace("Paladin", "Valkyrie");
				// }
				else
				{
					skillTitle = skillTitle.Replace("Paladin", PlayerInfoStringConstants.TITLE_PALADIN);
				}
			}
			else if ( skillTitle.Contains("Tactician") )
			{
				skillTitle = skillTitle.Replace("Tactician", PlayerInfoStringConstants.TITLE_GENERAL);
			}
			else if ( skillTitle.Contains("Duelist") )
			{
				skillTitle = skillTitle.Replace("Duelist", PlayerInfoStringConstants.TITLE_DUELIST);
			}
			else if ( skillTitle.Contains("Necromancer") )
			{
				skillTitle = skillTitle.Replace("Necromancer", PlayerInfoStringConstants.TITLE_OCCULTIST);
			}
			else if ( skillTitle.Contains("Bard") )
			{
				skillTitle = skillTitle.Replace("Bard", PlayerInfoStringConstants.TITLE_BARD);
			}
			else if ( skillTitle.Contains("Mage") )
			{
				skillTitle = skillTitle.Replace("Mage", PlayerInfoStringConstants.TITLE_ARCANIST);
			}
			else if ( skillTitle.Contains("Healer") )
			{
				if ( m.Female )
				{
					skillTitle = skillTitle.Replace("Healer", PlayerInfoStringConstants.TITLE_HEALER_FEMALE);
				}
				else
				{
					skillTitle = skillTitle.Replace("Healer", PlayerInfoStringConstants.TITLE_HEALER_MALE);
				}
			}
			else if ( skillTitle.Contains("Archer") && isBarbaric > 1 )
			{
				skillTitle = skillTitle.Replace("Archer", PlayerInfoStringConstants.TITLE_ARCHER_AMAZON);
			}
			else if ( skillTitle.Contains("Fencer") && isBarbaric > 1 )
			{
				skillTitle = skillTitle.Replace("Fencer", PlayerInfoStringConstants.TITLE_FENCER_AMAZON);
			}
			else if ( skillTitle.Contains("Armsman") && isBarbaric > 1 )
			{
				skillTitle = skillTitle.Replace("Armsman", PlayerInfoStringConstants.TITLE_ARMSMAN_AMAZON);
			}
			else if ( skillTitle.Contains("Swordsman") && isBarbaric > 1 )
			{
				skillTitle = skillTitle.Replace("Swordsman", PlayerInfoStringConstants.TITLE_SWORDSMAN_AMAZON);
			}
			else if ( skillTitle.Contains("Archer") )
			{
				skillTitle = skillTitle.Replace("Archer", PlayerInfoStringConstants.TITLE_BARBARIC_ARCHER);
			}
			else if ( skillTitle.Contains("Fencer") )
			{
				skillTitle = skillTitle.Replace("Fencer", PlayerInfoStringConstants.TITLE_BARBARIC_FENCER);
			}
			else if ( skillTitle.Contains("Armsman") )
			{
				skillTitle = skillTitle.Replace("Armsman", PlayerInfoStringConstants.TITLE_BARBARIC_ARMSMAN);
			}
			else if ( skillTitle.Contains("Swordsman") )
			{
				skillTitle = skillTitle.Replace("Swordsman", PlayerInfoStringConstants.TITLE_BARBARIC_SWORDSMAN);
			}
			else if ( skillTitle.Contains("Ranger") )
			{
				skillTitle = skillTitle.Replace("Ranger", PlayerInfoStringConstants.TITLE_RANGER);
			}
			else if ( skillTitle.Contains("Weapon Master") )
			{
				skillTitle = skillTitle.Replace("Weapon Master", PlayerInfoStringConstants.TITLE_GLADIATOR);
			}

			return skillTitle;
		}

		/// <summary>
		/// Applies archmage transformation for high-level mages/necromancers.
		/// </summary>
		/// <param name="skillTitle">The current skill title</param>
		/// <param name="m">The mobile</param>
		/// <param name="isOriental">Whether player is oriental</param>
		/// <returns>Transformed title</returns>
		private static string ApplyArchmageTransformation( string skillTitle, Mobile m, bool isOriental )
		{
			if ( isOriental )
				return skillTitle;

			if ( skillTitle.Contains("Mage") && m.Skills[SkillName.Magery].Base >= PlayerInfoConstants.MIN_SKILL_FOR_ARCHMAGE && m.Skills[SkillName.Necromancy].Base >= PlayerInfoConstants.MIN_SKILL_FOR_ARCHMAGE )
			{
				return skillTitle.Replace("Mage", PlayerInfoStringConstants.TITLE_ARCHMAGE);
			}
			else if ( skillTitle.Contains("Necromancer") && m.Skills[SkillName.Magery].Base >= PlayerInfoConstants.MIN_SKILL_FOR_ARCHMAGE && m.Skills[SkillName.Necromancy].Base >= PlayerInfoConstants.MIN_SKILL_FOR_ARCHMAGE )
			{
				return skillTitle.Replace("Necromancer", PlayerInfoStringConstants.TITLE_ARCHMAGE);
			}

			return skillTitle;
		}

		/// <summary>
		/// Applies monk/mystic transformation for wrestlers with mystic abilities.
		/// </summary>
		/// <param name="skillTitle">The current skill title</param>
		/// <param name="m">The mobile</param>
		/// <returns>Transformed title</returns>
		private static string ApplyMonkTransformation( string skillTitle, Mobile m )
		{
			if ( skillTitle.Contains("Wrestler") && isMonk(m) )
			{
				skillTitle = skillTitle.Replace("Wrestler", PlayerInfoStringConstants.TITLE_MONK);
				if ( m.Skills[SkillName.Magery].Base >= PlayerInfoConstants.MIN_SKILL_FOR_MYSTIC || m.Skills[SkillName.Necromancy].Base >= PlayerInfoConstants.MIN_SKILL_FOR_MYSTIC )
				{
					skillTitle = skillTitle.Replace(PlayerInfoStringConstants.TITLE_MONK, PlayerInfoStringConstants.TITLE_MYSTIC);
				}
			}

			return skillTitle;
		}

		/// <summary>
		/// Applies Syth/Jedi scholar transformation.
		/// </summary>
		/// <param name="skillTitle">The current skill title</param>
		/// <param name="m">The mobile</param>
		/// <returns>Transformed title</returns>
		private static string ApplySythJediScholarTransformation( string skillTitle, Mobile m )
		{
			if ( skillTitle.Contains("Scholar") && isSyth(m, false) )
			{
				return skillTitle.Replace("Scholar", "Syth");
			}
			else if ( skillTitle.Contains("Scholar") && isJedi(m, false) )
			{
				return skillTitle.Replace("Scholar", "Jedi");
			}

			return skillTitle;
		}

		/// <summary>
		/// Applies Jedi Paladin title transformation based on skill and karma.
		/// </summary>
		/// <param name="skillTitle">The current skill title</param>
		/// <param name="m">The mobile</param>
		/// <returns>Transformed title</returns>
		private static string ApplyJediPaladinTransformation( string skillTitle, Mobile m )
		{
			if ( !skillTitle.Contains("Paladin") || !isJedi(m, false) )
				return skillTitle;

			string jedi = PlayerInfoStringConstants.TITLE_JEDI_YOUNGLING;

			// Check conditions from highest to lowest
			if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
			{
				jedi = PlayerInfoStringConstants.TITLE_GRAND_MASTER;
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
			{
				jedi = PlayerInfoStringConstants.TITLE_MASTER_OF_ORDER;
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
			{
				jedi = PlayerInfoStringConstants.TITLE_JEDI_COUNCIL_MEMBER;
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
			{
				jedi = PlayerInfoStringConstants.TITLE_JEDI_MASTER;
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_CONSULAR || m.Karma >= PlayerInfoConstants.KARMA_JEDI_CONSULAR )
			{
				jedi = PlayerInfoStringConstants.TITLE_JEDI_CONSULAR;
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_PADAWAN || m.Karma >= PlayerInfoConstants.KARMA_JEDI_PADAWAN )
			{
				jedi = PlayerInfoStringConstants.TITLE_JEDI_GUARDIAN;
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_PADAWAN || m.Karma >= PlayerInfoConstants.KARMA_JEDI_PADAWAN )
			{
				jedi = PlayerInfoStringConstants.TITLE_JEDI_SENTINEL;
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_PADAWAN || m.Karma >= PlayerInfoConstants.KARMA_JEDI_PADAWAN )
			{
				jedi = PlayerInfoStringConstants.TITLE_JEDI_PADAWAN;
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.KARMA_NEUTRAL || m.Karma >= PlayerInfoConstants.KARMA_NEUTRAL )
			{
				jedi = PlayerInfoStringConstants.TITLE_JEDI_SERVICE_CORPS;
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.KARMA_NEUTRAL || m.Karma >= PlayerInfoConstants.KARMA_NEUTRAL )
			{
				jedi = PlayerInfoStringConstants.TITLE_JEDI_KNIGHT;
			}
			else if ( m.Skills[SkillName.Chivalry].Base < PlayerInfoConstants.KARMA_NEUTRAL || m.Karma < PlayerInfoConstants.KARMA_NEUTRAL )
			{
				jedi = PlayerInfoStringConstants.TITLE_JEDI_TROLL;
			}

			return skillTitle.Replace("Paladin", jedi);
		}

		/// <summary>
		/// Applies Syth Paladin title transformation based on skill and karma.
		/// </summary>
		/// <param name="skillTitle">The current skill title</param>
		/// <param name="m">The mobile</param>
		/// <returns>Transformed title</returns>
		private static string ApplySythPaladinTransformation( string skillTitle, Mobile m )
		{
			if ( !skillTitle.Contains("Paladin") || !isSyth(m, false) )
				return skillTitle;

			string syth = PlayerInfoStringConstants.TITLE_SYTH_HOPEFUL;

			// Check conditions from highest to lowest
			if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
			{
				syth = PlayerInfoStringConstants.TITLE_GRAND_MASTER;
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
			{
				syth = PlayerInfoStringConstants.TITLE_MASTER_OF_ORDER;
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
			{
				syth = PlayerInfoStringConstants.TITLE_SYTH_COUNCIL_MEMBER;
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_MASTER || m.Karma >= PlayerInfoConstants.KARMA_JEDI_MASTER )
			{
				syth = PlayerInfoStringConstants.TITLE_SYTH_MASTER;
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_CONSULAR || m.Karma >= PlayerInfoConstants.KARMA_JEDI_CONSULAR )
			{
				syth = PlayerInfoStringConstants.TITLE_SYTH_HOPEFUL; // Placeholder - needs proper title
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.JEDI_SKILL_PADAWAN || m.Karma >= PlayerInfoConstants.KARMA_JEDI_PADAWAN )
			{
				syth = PlayerInfoStringConstants.TITLE_SYTH_HOPEFUL; // Placeholder - needs proper title
			}
			else if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.KARMA_NEUTRAL || m.Karma >= PlayerInfoConstants.KARMA_NEUTRAL )
			{
				syth = PlayerInfoStringConstants.TITLE_SYTH_HOPEFUL; // Placeholder - needs proper title
			}
			else if ( m.Skills[SkillName.Chivalry].Base < PlayerInfoConstants.KARMA_NEUTRAL || m.Karma < PlayerInfoConstants.KARMA_NEUTRAL )
			{
				syth = PlayerInfoStringConstants.TITLE_SYTH_TROLL;
			}

			return skillTitle.Replace("Paladin", syth);
		}

		/// <summary>
		/// Applies oriental title transformations.
		/// </summary>
		/// <param name="skillTitle">The current skill title</param>
		/// <param name="m">The mobile</param>
		/// <param name="isOriental">Whether player is oriental</param>
		/// <returns>Transformed title</returns>
		private static string ApplyOrientalTransformations( string skillTitle, Mobile m, bool isOriental )
		{
			if ( !isOriental )
				return skillTitle;

			if ( skillTitle.Contains("Mage") )
			{
				skillTitle = skillTitle.Replace("Mage", PlayerInfoStringConstants.TITLE_ORIENTAL_MAGE);
			}
			else if ( skillTitle.Contains("Swordsman") )
			{
				skillTitle = skillTitle.Replace("Swordsman", PlayerInfoStringConstants.TITLE_ORIENTAL_SWORDSMAN);
			}
			else if ( skillTitle.Contains("Healer") )
			{
				skillTitle = skillTitle.Replace("Healer", PlayerInfoStringConstants.TITLE_ORIENTAL_HEALER);
			}
			else if ( skillTitle.Contains("Necromancer") )
			{
				skillTitle = skillTitle.Replace("Necromancer", PlayerInfoStringConstants.TITLE_ORIENTAL_NECROMANCER);
			}
			else if ( skillTitle.Contains("Alchemist") )
			{
				skillTitle = skillTitle.Replace("Alchemist", PlayerInfoStringConstants.TITLE_ORIENTAL_ALCHEMIST);
			}
			else if ( skillTitle.Contains("Medium") )
			{
				skillTitle = skillTitle.Replace("Medium", PlayerInfoStringConstants.TITLE_ORIENTAL_MEDIUM);
			}
			else if ( skillTitle.Contains("Archer") )
			{
				skillTitle = skillTitle.Replace("Archer", PlayerInfoStringConstants.TITLE_ORIENTAL_ARCHER);
			}
			else if ( skillTitle.Contains("Fencer") )
			{
				skillTitle = skillTitle.Replace("Fencer", PlayerInfoStringConstants.TITLE_ORIENTAL_FENCER);
			}
			else if ( skillTitle.Contains("Armsman") )
			{
				skillTitle = skillTitle.Replace("Armsman", PlayerInfoStringConstants.TITLE_ORIENTAL_ARMSMAN);
			}
			else if ( skillTitle.Contains("Tactician") )
			{
				skillTitle = skillTitle.Replace("Tactician", PlayerInfoStringConstants.TITLE_ORIENTAL_TACTICIAN);
			}
			else if ( skillTitle.Contains("Paladin") )
			{
				skillTitle = skillTitle.Replace("Paladin", PlayerInfoStringConstants.TITLE_ORIENTAL_PALADIN);
			}
			else if ( skillTitle.Contains("Wrestler") )
			{
				skillTitle = skillTitle.Replace("Wrestler", PlayerInfoStringConstants.TITLE_ORIENTAL_WRESTLER);
			}

			return skillTitle;
		}

		/// <summary>
		/// Applies default title transformations (non-barbaric, non-oriental).
		/// </summary>
		/// <param name="skillTitle">The current skill title</param>
		/// <param name="m">The mobile</param>
		/// <param name="isEvil">Whether player is evil</param>
		/// <returns>Transformed title</returns>
		private static string ApplyDefaultTransformations( string skillTitle, Mobile m, bool isEvil )
		{
			if ( skillTitle.Contains("Naturalist") && m.Karma < PlayerInfoConstants.KARMA_NEUTRAL )
			{
				skillTitle = skillTitle.Replace("Naturalist", PlayerInfoStringConstants.TITLE_INDUSTRIALIST);
			}
			else if ( skillTitle.Contains("Beggar") && isJester(m) )
			{
				skillTitle = skillTitle.Replace("Beggar", PlayerInfoStringConstants.TITLE_JESTER);
			}
			else if ( skillTitle.Contains("Scholar") && isJester(m) )
			{
				skillTitle = skillTitle.Replace("Scholar", PlayerInfoStringConstants.TITLE_CLOWN);
			}
			else if ( skillTitle.Contains("Samurai") && m.Karma < PlayerInfoConstants.KARMA_NEUTRAL )
			{
				skillTitle = skillTitle.Replace("Samurai", PlayerInfoStringConstants.TITLE_RONIN);
			}
			else if ( skillTitle.Contains("Ninja") && m.Karma < PlayerInfoConstants.KARMA_NEUTRAL )
			{
				skillTitle = skillTitle.Replace("Ninja", PlayerInfoStringConstants.TITLE_YAKUZA);
			}
			else if ( ( skillTitle.Contains("Healer") || skillTitle.Contains("Medium") ) && m.Karma >= PlayerInfoConstants.KARMA_PRIEST && m.Skills[SkillName.Healing].Base >= PlayerInfoConstants.MIN_SKILL_FOR_PRIEST && m.Skills[SkillName.SpiritSpeak].Base >= PlayerInfoConstants.MIN_SKILL_FOR_PRIEST )
			{
				skillTitle = skillTitle.Replace("Medium", PlayerInfoStringConstants.TITLE_PRIEST);
				skillTitle = skillTitle.Replace("Healer", PlayerInfoStringConstants.TITLE_PRIEST);
			}
			else if ( skillTitle.Contains("Detective") )
			{
				skillTitle = skillTitle.Replace("Detective", PlayerInfoStringConstants.TITLE_UNDERTAKER);
			}
			else if ( skillTitle.Contains("Shade") )
			{
				skillTitle = skillTitle.Replace("Shade", PlayerInfoStringConstants.TITLE_STEALTH);
			}
			else if ( skillTitle.Contains("Rogue") )
			{
				skillTitle = skillTitle.Replace("Rogue", PlayerInfoStringConstants.TITLE_ROGUE);
			}
			else if ( skillTitle.Contains("Mage") && isEvil && m.Body == PlayerInfoConstants.BODY_FEMALE )
			{
				skillTitle = skillTitle.Replace("Mage", PlayerInfoStringConstants.TITLE_ENCHANTRESS);
			}
			else if ( skillTitle.Contains("Mage") && isEvil )
			{
				skillTitle = skillTitle.Replace("Mage", PlayerInfoStringConstants.TITLE_WARLOCK);
			}
			else if ( skillTitle.Contains("Mage") )
			{
				skillTitle = skillTitle.Replace("Mage", PlayerInfoStringConstants.TITLE_WIZARD);
			}
			else if ( skillTitle.Contains("Paladin") )
			{
				skillTitle = skillTitle.Replace("Paladin", PlayerInfoStringConstants.TITLE_KNIGHT);
			}
			else if ( skillTitle.Contains("Tamer") )
			{
				if ( m.Karma < PlayerInfoConstants.KARMA_SLAVER )
				{
					skillTitle = skillTitle.Replace("Tamer", PlayerInfoStringConstants.TITLE_SLAVER);
				}
				else
				{
					skillTitle = skillTitle.Replace("Tamer", PlayerInfoStringConstants.TITLE_BEASTMASTER);
				}
			}
			else if ( skillTitle.Contains("Pickpocket") )
			{
				skillTitle = skillTitle.Replace("Pickpocket", PlayerInfoStringConstants.TITLE_THIEF);
			}
			else if ( skillTitle.Contains("Fisherman") )
			{
				skillTitle = skillTitle.Replace("Fisherman", PlayerInfoStringConstants.TITLE_SAILOR);
			}
			else if ( skillTitle.Contains("Stoic") )
			{
				skillTitle = skillTitle.Replace("Stoic", PlayerInfoStringConstants.TITLE_MEDITATOR);
			}
			else if ( skillTitle.Contains("Wrestler") )
			{
				skillTitle = skillTitle.Replace("Wrestler", PlayerInfoStringConstants.TITLE_BRAWLER);
			}
			else if ( skillTitle.Contains("Warder") )
			{
				skillTitle = skillTitle.Replace("Warder", PlayerInfoStringConstants.TITLE_SPELL_WARDER);
			}

			return skillTitle;
		}

		/// <summary>
		/// Applies post-transformation adjustments (gender, karma-based, etc.).
		/// </summary>
		/// <param name="skillTitle">The current skill title</param>
		/// <param name="m">The mobile</param>
		/// <param name="isBarbaric">Barbaric level</param>
		/// <param name="isOriental">Whether player is oriental</param>
		/// <returns>Transformed title</returns>
		private static string ApplyPostTransformations( string skillTitle, Mobile m, int isBarbaric, bool isOriental )
		{
			// Priest to Buddhist for oriental players
			if ( skillTitle.Contains("Priest") && isOriental )
			{
				skillTitle = skillTitle.Replace("Priest", PlayerInfoStringConstants.TITLE_BUDDHIST);
			}

			// Non-barbaric gender and karma transformations
			if ( isBarbaric == 0 )
			{
				if ( skillTitle.Contains("Wizard") && m.Body == PlayerInfoConstants.BODY_FEMALE )
				{
					skillTitle = skillTitle.Replace("Wizard", PlayerInfoStringConstants.TITLE_SORCERESS);
				}
				if ( skillTitle.Contains("Necromancer") && m.Body == PlayerInfoConstants.BODY_FEMALE )
				{
					skillTitle = skillTitle.Replace("Necromancer", PlayerInfoStringConstants.TITLE_WITCH);
				}
				if ( skillTitle.Contains("Knight") && m.Karma < PlayerInfoConstants.KARMA_NEUTRAL )
				{
					skillTitle = skillTitle.Replace("Knight", PlayerInfoStringConstants.TITLE_DEATH_KNIGHT_POST);
				}
				if ( skillTitle.Contains("Healer") && m.Karma < PlayerInfoConstants.KARMA_NEUTRAL )
				{
					skillTitle = skillTitle.Replace("Healer", PlayerInfoStringConstants.TITLE_MORTICIAN);
				}
			}

			// Sailor/Pirate transformations
			if ( skillTitle.Contains("Sailor") && m.Karma < PlayerInfoConstants.KARMA_NEUTRAL )
			{
				skillTitle = skillTitle.Replace("Sailor", PlayerInfoStringConstants.TITLE_PIRATE);
			}
			if ( skillTitle.Contains("Sailor") && m.Skills[SkillName.Fishing].Value >= PlayerInfoConstants.MIN_SKILL_FOR_CAPTAIN )
			{
				skillTitle = skillTitle.Replace("Sailor", PlayerInfoStringConstants.TITLE_SHIP_CAPTAIN);
			}
			if ( skillTitle.Contains("Pirate") && m.Skills[SkillName.Fishing].Value >= PlayerInfoConstants.MIN_SKILL_FOR_CAPTAIN )
			{
				skillTitle = skillTitle.Replace("Pirate", PlayerInfoStringConstants.TITLE_PIRATE_CAPTAIN);
			}

			// Gender suffix transformation for PT-BR
			if ( m.Female && isBarbaric == 0 )
			{
				// Handle common PT-BR masculine endings (check more specific patterns first)
				if ( skillTitle.EndsWith( "eiro" ) && !skillTitle.EndsWith( "eira" ) )
				{
					// -eiro → -eira (e.g., "Marinheiro" → "Marinheira", "Cavaleiro" → "Cavaleira")
					skillTitle = skillTitle.Substring( 0, skillTitle.Length - 4 ) + "eira";
				}
				else if ( skillTitle.EndsWith( "dor" ) && !skillTitle.EndsWith( "dora" ) )
				{
					// -dor → -dora (e.g., "Domador" → "Domadora", "Meditador" → "Meditadora")
					skillTitle = skillTitle.Substring( 0, skillTitle.Length - 3 ) + "dora";
				}
				else if ( skillTitle.EndsWith( "Brigão" ) )
				{
					// Special case: "Brigão" → "Brigona"
					skillTitle = skillTitle.Replace( "Brigão", "Brigona" );
				}
				else if ( skillTitle.EndsWith( "rão" ) && !skillTitle.EndsWith( "ra" ) )
				{
					// -rão → -ra (e.g., "Ladrão" → "Ladra")
					skillTitle = skillTitle.Substring( 0, skillTitle.Length - 3 ) + "ra";
				}
				else if ( skillTitle.EndsWith( "ão" ) && !skillTitle.EndsWith( "ã" ) && !skillTitle.EndsWith( "ra" ) )
				{
					// -ão → -ã (e.g., "Capitão" → "Capitã")
					skillTitle = skillTitle.Substring( 0, skillTitle.Length - 2 ) + "ã";
				}
				else if ( skillTitle.EndsWith( "o" ) && !skillTitle.EndsWith( "a" ) && !skillTitle.EndsWith( "ista" ) && !skillTitle.EndsWith( "eiro" ) && !skillTitle.EndsWith( "dor" ) && !skillTitle.EndsWith( "ão" ) && !skillTitle.EndsWith( "rão" ) )
				{
					// -o → -a (e.g., "Mago" → "Maga")
					// Skip if already feminine, ends with -ista (gender-neutral), or matches other patterns above
					skillTitle = skillTitle.Substring( 0, skillTitle.Length - 1 ) + "a";
				}
			}

			return skillTitle;
		}

		#endregion

		#region Player Type Detection Methods

		public static bool isMonk ( Mobile m )
		{
			string GuildTitle = "";

			if ( m is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)m;

				if ( pm.Profession == PlayerInfoConstants.CHARACTER_FLAG_TRUE )
				{
					GuildTitle = PlayerInfoStringConstants.GUILD_NPC_FUGITIVE;
				}
				else if ( pm.NpcGuild != NpcGuild.None )
				{
					GuildNPCTitles.TryGetValue( pm.NpcGuild, out GuildTitle );
				}
			}
			else if ( m is BaseVendor )
			{
				BaseVendor pm = (BaseVendor)m;

				if ( pm.NpcGuild != NpcGuild.None )
				{
					GuildVendorTitles.TryGetValue( pm.NpcGuild, out GuildTitle );
				}
			}
			return GuildTitle;
		}

		public static string GetStatusGuild( Mobile m )
		{
			string GuildTitle = "";

			if ( m is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)m;

				if ( pm.Profession == PlayerInfoConstants.CHARACTER_FLAG_TRUE )
				{
					GuildTitle = PlayerInfoStringConstants.GUILD_STATUS_FUGITIVE;
				}
				else if ( pm.NpcGuild != NpcGuild.None )
				{
					GuildStatusTitles.TryGetValue( pm.NpcGuild, out GuildTitle );
				}
			}
			return GuildTitle;
		}

		#endregion

		#region Player Statistics Methods

		public static int GetPlayerLevel( Mobile m )
		{
			int fame = m.Fame;
				if ( fame > PlayerInfoConstants.MAX_FAME_FOR_LEVEL ){ fame = PlayerInfoConstants.MAX_FAME_FOR_LEVEL; }

			int karma = m.Karma;
				if ( karma < PlayerInfoConstants.KARMA_NEUTRAL ){ karma = m.Karma * -1; }
				if ( karma > PlayerInfoConstants.MAX_KARMA_FOR_LEVEL ){ karma = PlayerInfoConstants.MAX_KARMA_FOR_LEVEL; }

			int skills = m.Skills.Total;
				if ( skills > PlayerInfoConstants.MAX_SKILLS_FOR_LEVEL ){ skills = PlayerInfoConstants.MAX_SKILLS_FOR_LEVEL; }
				skills = (int)( PlayerInfoConstants.SKILLS_MULTIPLIER * skills );

			int stats = m.RawStr + m.RawDex + m.RawInt;
				stats = PlayerInfoConstants.STATS_MULTIPLIER * stats;

			int level = (int)( ( fame + karma + skills + stats ) / PlayerInfoConstants.LEVEL_DIVISOR );
				level = (int)( ( level - PlayerInfoConstants.LEVEL_ADJUSTMENT_SUBTRACTOR ) * PlayerInfoConstants.LEVEL_ADJUSTMENT_MULTIPLIER );

			if ( level < PlayerInfoConstants.MIN_PLAYER_LEVEL ){ level = PlayerInfoConstants.MIN_PLAYER_LEVEL; }
			if ( level > PlayerInfoConstants.MAX_PLAYER_LEVEL ){ level = PlayerInfoConstants.MAX_PLAYER_LEVEL; }

			return level;
		}

		public static int GetPlayerDifficulty( Mobile m )
		{
			int difficulty = 0;
			int level = GetPlayerLevel( m );

			if ( level >= PlayerInfoConstants.DIFFICULTY_LEVEL_4 ){ difficulty = 4; }
			else if ( level >= PlayerInfoConstants.DIFFICULTY_LEVEL_3 ){ difficulty = 3; }
			else if ( level >= PlayerInfoConstants.DIFFICULTY_LEVEL_2 ){ difficulty = 2; }
			else if ( level >= PlayerInfoConstants.DIFFICULTY_LEVEL_1 ){ difficulty = 1; }

			return difficulty;
		}

	public static int GetResurrectCost( Mobile m )
	{
		// Young players get free resurrection
		if (m is PlayerMobile && ((PlayerMobile)m).Young)
		{
			return 0;
		}

		int fame = (int)((double)m.Fame / PlayerInfoConstants.RESURRECT_DIVISOR);
			if ( fame > PlayerInfoConstants.MAX_FAME_FOR_RESURRECT ){ fame = PlayerInfoConstants.MAX_FAME_FOR_RESURRECT; }
		int karma = (int)((double)Math.Abs(m.Karma) / PlayerInfoConstants.RESURRECT_DIVISOR);
			if ( karma > PlayerInfoConstants.MAX_KARMA_FOR_RESURRECT ){ karma = PlayerInfoConstants.MAX_KARMA_FOR_RESURRECT; }

		int skills = (int)((double)m.Skills.Total / PlayerInfoConstants.RESURRECT_DIVISOR);
			if ( skills > PlayerInfoConstants.MAX_SKILLS_FOR_RESURRECT ){ skills = PlayerInfoConstants.MAX_SKILLS_FOR_RESURRECT; }		

		int stats = m.RawStr + m.RawDex + m.RawInt;
			if ( stats > PlayerInfoConstants.MAX_STATS_FOR_RESURRECT ){ stats = PlayerInfoConstants.MAX_STATS_FOR_RESURRECT; }
			stats = PlayerInfoConstants.RESURRECT_STATS_MULTIPLIER * stats;					

		int level = (int)( ( fame + karma + skills + stats ) / PlayerInfoConstants.RESURRECT_LEVEL_DIVISOR );
			level = (int)( ( level - PlayerInfoConstants.LEVEL_ADJUSTMENT_SUBTRACTOR ) * PlayerInfoConstants.LEVEL_ADJUSTMENT_MULTIPLIER );

		if ( level < PlayerInfoConstants.MIN_PLAYER_LEVEL ){ level = PlayerInfoConstants.MIN_PLAYER_LEVEL; }
		if ( level > PlayerInfoConstants.MAX_RESURRECT_LEVEL ){ level = PlayerInfoConstants.MAX_RESURRECT_LEVEL; }

		level = ( level * PlayerInfoConstants.RESURRECT_COST_MULTIPLIER );

		if (m is PlayerMobile)
		{
			if (((PlayerMobile)m).Profession == PlayerInfoConstants.CHARACTER_FLAG_TRUE ){ level = (int)(level * PlayerInfoConstants.RESURRECT_PROFESSION_MULTIPLIER); }
			if ( !((PlayerMobile)m).Avatar) {level = (int)(level * PlayerInfoConstants.RESURRECT_NON_AVATAR_MULTIPLIER);}
		}

		// Minimum cost is 300 gold (matching debt system)
		if (level < PlayerInfoConstants.MIN_RESURRECT_COST)
		{
			level = PlayerInfoConstants.MIN_RESURRECT_COST;
		}

		return level;
	}

		#endregion

		#region Utility Methods

		public static string GetTodaysDate()
		{
			string sYear = DateTime.UtcNow.Year.ToString();
			string sMonth = DateTime.UtcNow.Month.ToString();
				string sMonthName = "Janeiro";
				if ( sMonth == "2" ){ sMonthName = "Fevereiro"; }
				else if ( sMonth == "3" ){ sMonthName = "Março"; }
				else if ( sMonth == "4" ){ sMonthName = "Abril"; }
				else if ( sMonth == "5" ){ sMonthName = "Maio"; }
				else if ( sMonth == "6" ){ sMonthName = "Junho"; }
				else if ( sMonth == "7" ){ sMonthName = "Julho"; }
				else if ( sMonth == "8" ){ sMonthName = "Agosto"; }
				else if ( sMonth == "9" ){ sMonthName = "Setembro"; }
				else if ( sMonth == "10" ){ sMonthName = "Outubro"; }
				else if ( sMonth == "11" ){ sMonthName = "Novembro"; }
				else if ( sMonth == "12" ){ sMonthName = "Dezembro"; }
			string sDay = DateTime.UtcNow.Day.ToString();
			string sHour = DateTime.UtcNow.Hour.ToString();
			string sMinute = DateTime.UtcNow.Minute.ToString();
			string sSecond = DateTime.UtcNow.Second.ToString();

			if ( sHour.Length == 1 ){ sHour = "0" + sHour; }
			if ( sMinute.Length == 1 ){ sMinute = "0" + sMinute; }
			if ( sSecond.Length == 1 ){ sSecond = "0" + sSecond; }

			string sDateString = sMonthName + " " + sDay + ", " + sYear + " às " + sHour + ":" + sMinute;

			return sDateString;
		}

		#endregion

		#region Luck Calculation Methods

		public static bool LuckyPlayer( int luck, Mobile from )
		{
			int realluck = CalculateRealLuck( luck, from, false );

			if ( realluck <= 0 )
				return false;

			int clover = (int)((double)realluck * PlayerInfoConstants.CLOVER_PERCENTAGE);

			if ( clover >= Utility.RandomMinMax( PlayerInfoConstants.LUCK_RANDOM_MIN, PlayerInfoConstants.CLOVER_RANDOM_RANGE ) )
				return true;

			if ( from is PlayerMobile )
				((PlayerMobile)from).CheckRest();
			
			if ( from is PlayerMobile && ((PlayerMobile)from).GetFlag(PlayerFlag.WellRested) && Utility.RandomDouble() < PlayerInfoConstants.WELL_RESTED_CHANCE ) 
				return true;
				
			return false;
		}

		public static bool LuckyKiller( int luck, Mobile from )
		{
			int realluck = CalculateRealLuck( luck, from, true );

			if ( realluck <= 0 )
				return false;

			int clover = (int)((double)realluck * PlayerInfoConstants.CLOVER_PERCENTAGE);

			if ( clover >= Utility.RandomMinMax( PlayerInfoConstants.LUCK_RANDOM_MIN, PlayerInfoConstants.CLOVER_RANDOM_RANGE ) )
				return true;

			return false;
		}

		public static bool EvilPlayer( Mobile m )
		{
			if ( m is BaseCreature )
				m = ((BaseCreature)m).GetMaster();

			if ( m is PlayerMobile )
			{
				if ( m.AccessLevel > AccessLevel.Player )
					return true;

				if ( m.Skills[SkillName.Necromancy].Base >= PlayerInfoConstants.MIN_SKILL_FOR_EVIL_CHECKS && m.Karma < PlayerInfoConstants.KARMA_NEUTRAL ) // NECROMANCERS
					return true;

				if ( m.Skills[SkillName.Forensics].Base >= PlayerInfoConstants.MIN_SKILL_FOR_UNDERTAKER && m.Karma < PlayerInfoConstants.KARMA_NEUTRAL ) // UNDERTAKERS
					return true;

				if ( m.Skills[SkillName.Chivalry].Base >= PlayerInfoConstants.MIN_SKILL_FOR_EVIL_CHECKS && m.Karma <= PlayerInfoConstants.KARMA_DEATH_KNIGHT ) // DEATH KNIGHTS
					return true;

				if ( m.Skills[SkillName.EvalInt].Base >= PlayerInfoConstants.MIN_SKILL_FOR_EVIL_CHECKS && m.Skills[SkillName.Swords].Base >= PlayerInfoConstants.MIN_SKILL_FOR_EVIL_CHECKS && m.Karma <= PlayerInfoConstants.KARMA_DEATH_KNIGHT && Server.Misc.GetPlayerInfo.isSyth(m,false) ) // SYTH
					return true;

				if (((PlayerMobile)m).BalanceStatus <0 ) // pledged to evil
					return true;
			}

			return false;
		}
		
		

		public static int LuckyPlayerArtifacts( int luck, Mobile from)
		{
			int realluck = CalculateRealLuck( luck, from, true );

			if ( realluck <= 0 )
				return 0;

			int clover = (int)((double)realluck * PlayerInfoConstants.CLOVER_PERCENTAGE_ARTIFACTS);

			return clover;
		}

		#endregion

		public static bool OrientalPlay( Mobile m )
		{
			if (m == null)
				return false;

			if ( m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				if ( DB.CharacterOriental == PlayerInfoConstants.CHARACTER_FLAG_TRUE )
					return true;
			}
			else if ( m is BaseCreature )
			{
				Mobile killer = ResolveCreatureMaster( m.LastKiller );

				if ( killer != null && killer is PlayerMobile )
				{
					CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( killer );

					if ( DB.CharacterOriental == PlayerInfoConstants.CHARACTER_FLAG_TRUE )
						return true;
				}
				else
				{
					Mobile hitter = ResolveCreatureMaster( m.FindMostRecentDamager(true) );

					if ( hitter != null && hitter is PlayerMobile )
					{
						CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( hitter );

						if ( DB.CharacterOriental == PlayerInfoConstants.CHARACTER_FLAG_TRUE )
							return true;
					}
				}
			}

			return false;
		}

		public static int BarbaricPlay( Mobile m )
		{
			if ( m != null && m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				if ( DB.CharacterBarbaric > 0 )
					return DB.CharacterBarbaric;
			}

			return 0;
		}

		public static bool EvilPlay( Mobile m )
		{
			if ( m != null && m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				if ( DB.CharacterEvil == PlayerInfoConstants.CHARACTER_FLAG_TRUE )
					return true;
			}
			else if ( m != null && m is BaseCreature )
			{
				Mobile killer = m.LastKiller;
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

				if ( killer != null && killer is PlayerMobile )
				{
					CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( killer );

					if ( DB.CharacterEvil == PlayerInfoConstants.CHARACTER_FLAG_TRUE )
						return true;
				}
				else
				{
					Mobile hitter = m.FindMostRecentDamager(true);
					if (hitter is BaseCreature)
					{
						BaseCreature bc_killer = (BaseCreature)hitter;
						if(bc_killer.Summoned)
						{
							if(bc_killer.SummonMaster != null)
								hitter = bc_killer.SummonMaster;
						}
						else if(bc_killer.Controlled)
						{
							if(bc_killer.ControlMaster != null)
								hitter=bc_killer.ControlMaster;
						}
						else if(bc_killer.BardProvoked)
						{
							if(bc_killer.BardMaster != null)
								hitter=bc_killer.BardMaster;
						}
					}

					if ( hitter != null && hitter is PlayerMobile )
					{
						CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( hitter );

						if ( DB.CharacterEvil == 1 )
							return true;
					}
				}
			}

			return false;
		}

		public static int GetWealth( Mobile from, int pack )
		{
			int wealth = 0;

			Container bank = from.FindBankNoCreate();
				if ( pack > 0 ){ bank = from.Backpack; }

			if ( bank != null )
			{
				Item[] gold = bank.FindItemsByType( typeof( Gold ) );
				Item[] checks = bank.FindItemsByType( typeof( BankCheck ) );
				Item[] silver = bank.FindItemsByType( typeof( DDSilver ) );
				Item[] copper = bank.FindItemsByType( typeof( DDCopper ) );
				Item[] xormite = bank.FindItemsByType( typeof( DDXormite ) );
				Item[] jewels = bank.FindItemsByType( typeof( DDJewels ) );
				Item[] crystals = bank.FindItemsByType( typeof( Crystals ) );
				Item[] gems = bank.FindItemsByType( typeof( DDGemstones ) );
				Item[] nuggets = bank.FindItemsByType( typeof( DDGoldNuggets ) );

				for ( int i = 0; i < gold.Length; ++i )
					wealth += gold[i].Amount;

				for ( int i = 0; i < checks.Length; ++i )
					wealth += ((BankCheck)checks[i]).Worth;

				for ( int i = 0; i < silver.Length; ++i )
					wealth += (int)Math.Floor((decimal)(silver[i].Amount / 5));

				for ( int i = 0; i < copper.Length; ++i )
					wealth += (int)Math.Floor((decimal)(copper[i].Amount / 10));

				for ( int i = 0; i < xormite.Length; ++i )
					wealth += (xormite[i].Amount)*3;

				for ( int i = 0; i < crystals.Length; ++i )
					wealth += (crystals[i].Amount)*5;

				for ( int i = 0; i < jewels.Length; ++i )
					wealth += (jewels[i].Amount)*2;

				for ( int i = 0; i < gems.Length; ++i )
					wealth += (gems[i].Amount)*2;

				for ( int i = 0; i < nuggets.Length; ++i )
					wealth += (nuggets[i].Amount);
			}

			return wealth;
		}

		#region Private Helper Methods

		/// <summary>
		/// Resolves the master of a creature (SummonMaster, ControlMaster, or BardMaster).
		/// Used to find the actual player controlling a creature.
		/// </summary>
		/// <param name="creature">The creature to resolve the master for</param>
		/// <returns>The master mobile, or the original creature if no master found</returns>
		private static Mobile ResolveCreatureMaster( Mobile creature )
		{
			if ( creature is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)creature;
				if ( bc.Summoned && bc.SummonMaster != null )
					return bc.SummonMaster;
				if ( bc.Controlled && bc.ControlMaster != null )
					return bc.ControlMaster;
				if ( bc.BardProvoked && bc.BardMaster != null )
					return bc.BardMaster;
			}
			return creature;
		}

		/// <summary>
		/// Calculates the balance tweak multiplier for luck calculations.
		/// </summary>
		/// <param name="pm">The player mobile</param>
		/// <param name="requireAvatar">Whether avatar status is required</param>
		/// <returns>Balance tweak multiplier (1.0 if no balance effect)</returns>
		private static double CalculateBalanceTweak( PlayerMobile pm, bool requireAvatar )
		{
			if ( pm == null || ( requireAvatar && !pm.Avatar ) )
				return 1.0;

			double balance = (double)AetherGlobe.BalanceLevel;
			if ( pm.Karma < PlayerInfoConstants.KARMA_NEUTRAL )
				balance = PlayerInfoConstants.BALANCE_MAX - balance;

			return 1.0 + ( ( balance - PlayerInfoConstants.BALANCE_CENTER ) / PlayerInfoConstants.BALANCE_DIVISOR );
		}

		/// <summary>
		/// Calculates real luck value with balance tweak applied.
		/// </summary>
		/// <param name="luck">Base luck value</param>
		/// <param name="from">The mobile</param>
		/// <param name="requireAvatar">Whether avatar status is required for balance effect</param>
		/// <returns>Calculated real luck value</returns>
		private static int CalculateRealLuck( int luck, Mobile from, bool requireAvatar )
		{
			if ( AdventuresFunctions.IsInMidland( (object)from ) || luck > PlayerInfoConstants.LUCK_CAP_MIDLAND )
				luck = 0;

			int realluck = Utility.RandomMinMax( PlayerInfoConstants.LUCK_RANDOM_MIN, PlayerInfoConstants.LUCK_RANDOM_MAX ) + (int)( (double)luck * PlayerInfoConstants.LUCK_DIVISOR );

			if ( from is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)from;
				double balancetweak = CalculateBalanceTweak( pm, requireAvatar );
				realluck = (int)( (double)realluck * balancetweak );
			}

			if ( realluck <= 0 )
				return 0;

			if ( realluck > MyServerSettings.LuckCap() )
				realluck = MyServerSettings.LuckCap();

			return realluck;
		}

		#endregion

		#region Private Static Dictionaries

		/// <summary>
		/// Dictionary mapping skill index numbers to SkillName enum values.
		/// Used to replace 54 if-else statements in GetShowingSkill.
		/// </summary>
		private static readonly System.Collections.Generic.Dictionary<int, SkillName> SkillIndexMap = new System.Collections.Generic.Dictionary<int, SkillName>
		{
			{ 1, SkillName.Alchemy },
			{ 2, SkillName.Anatomy },
			{ 3, SkillName.AnimalLore },
			{ 4, SkillName.AnimalTaming },
			{ 5, SkillName.Archery },
			{ 6, SkillName.ArmsLore },
			{ 7, SkillName.Begging },
			{ 8, SkillName.Blacksmith },
			{ 9, SkillName.Bushido },
			{ 10, SkillName.Camping },
			{ 11, SkillName.Carpentry },
			{ 12, SkillName.Cartography },
			{ 13, SkillName.Chivalry },
			{ 14, SkillName.Cooking },
			{ 15, SkillName.DetectHidden },
			{ 16, SkillName.Discordance },
			{ 17, SkillName.EvalInt },
			{ 18, SkillName.Fencing },
			{ 19, SkillName.Fishing },
			{ 20, SkillName.Fletching },
			{ 21, SkillName.Focus },
			{ 22, SkillName.Forensics },
			{ 23, SkillName.Healing },
			{ 24, SkillName.Herding },
			{ 25, SkillName.Hiding },
			{ 26, SkillName.Inscribe },
			{ 27, SkillName.ItemID },
			{ 28, SkillName.Lockpicking },
			{ 29, SkillName.Lumberjacking },
			{ 30, SkillName.Macing },
			{ 31, SkillName.Magery },
			{ 32, SkillName.MagicResist },
			{ 33, SkillName.Meditation },
			{ 34, SkillName.Mining },
			{ 35, SkillName.Musicianship },
			{ 36, SkillName.Necromancy },
			{ 37, SkillName.Ninjitsu },
			{ 38, SkillName.Parry },
			{ 39, SkillName.Peacemaking },
			{ 40, SkillName.Poisoning },
			{ 41, SkillName.Provocation },
			{ 42, SkillName.RemoveTrap },
			{ 43, SkillName.Snooping },
			{ 44, SkillName.SpiritSpeak },
			{ 45, SkillName.Stealing },
			{ 46, SkillName.Stealth },
			{ 47, SkillName.Swords },
			{ 48, SkillName.Tactics },
			{ 49, SkillName.Tailoring },
			{ 50, SkillName.TasteID },
			{ 51, SkillName.Tinkering },
			{ 52, SkillName.Tracking },
			{ 53, SkillName.Veterinary },
			{ 54, SkillName.Wrestling }
		};

		/// <summary>
		/// Dictionary mapping NpcGuild enum values to NPC format guild titles (for PlayerMobile).
		/// </summary>
		private static readonly System.Collections.Generic.Dictionary<NpcGuild, string> GuildNPCTitles = new System.Collections.Generic.Dictionary<NpcGuild, string>
		{
			{ NpcGuild.MagesGuild, PlayerInfoStringConstants.GUILD_NPC_WIZARDS },
			{ NpcGuild.WarriorsGuild, PlayerInfoStringConstants.GUILD_NPC_WARRIORS },
			{ NpcGuild.ThievesGuild, PlayerInfoStringConstants.GUILD_NPC_THIEVES },
			{ NpcGuild.RangersGuild, PlayerInfoStringConstants.GUILD_NPC_RANGERS },
			{ NpcGuild.HealersGuild, PlayerInfoStringConstants.GUILD_NPC_HEALERS },
			{ NpcGuild.MinersGuild, PlayerInfoStringConstants.GUILD_NPC_MINERS },
			{ NpcGuild.MerchantsGuild, PlayerInfoStringConstants.GUILD_NPC_MERCHANTS },
			{ NpcGuild.TinkersGuild, PlayerInfoStringConstants.GUILD_NPC_TINKERS },
			{ NpcGuild.TailorsGuild, PlayerInfoStringConstants.GUILD_NPC_TAILORS },
			{ NpcGuild.FishermensGuild, PlayerInfoStringConstants.GUILD_NPC_MARINERS },
			{ NpcGuild.BardsGuild, PlayerInfoStringConstants.GUILD_NPC_BARDS },
			{ NpcGuild.BlacksmithsGuild, PlayerInfoStringConstants.GUILD_NPC_BLACKSMITHS },
			{ NpcGuild.NecromancersGuild, PlayerInfoStringConstants.GUILD_NPC_BLACK_MAGIC },
			{ NpcGuild.AlchemistsGuild, PlayerInfoStringConstants.GUILD_NPC_ALCHEMISTS },
			{ NpcGuild.DruidsGuild, PlayerInfoStringConstants.GUILD_NPC_DRUIDS },
			{ NpcGuild.ArchersGuild, PlayerInfoStringConstants.GUILD_NPC_ARCHERS },
			{ NpcGuild.CarpentersGuild, PlayerInfoStringConstants.GUILD_NPC_CARPENTERS },
			{ NpcGuild.CartographersGuild, PlayerInfoStringConstants.GUILD_NPC_CARTOGRAPHERS },
			{ NpcGuild.LibrariansGuild, PlayerInfoStringConstants.GUILD_NPC_LIBRARIANS },
			{ NpcGuild.CulinariansGuild, PlayerInfoStringConstants.GUILD_NPC_CULINARY },
			{ NpcGuild.AssassinsGuild, PlayerInfoStringConstants.GUILD_NPC_ASSASSINS }
		};

		/// <summary>
		/// Dictionary mapping NpcGuild enum values to vendor format guild titles (for BaseVendor).
		/// </summary>
		private static readonly System.Collections.Generic.Dictionary<NpcGuild, string> GuildVendorTitles = new System.Collections.Generic.Dictionary<NpcGuild, string>
		{
			{ NpcGuild.MagesGuild, PlayerInfoStringConstants.GUILD_VENDOR_WIZARDS },
			{ NpcGuild.WarriorsGuild, PlayerInfoStringConstants.GUILD_VENDOR_WARRIORS },
			{ NpcGuild.ThievesGuild, PlayerInfoStringConstants.GUILD_VENDOR_THIEVES },
			{ NpcGuild.RangersGuild, PlayerInfoStringConstants.GUILD_VENDOR_RANGERS },
			{ NpcGuild.HealersGuild, PlayerInfoStringConstants.GUILD_VENDOR_HEALERS },
			{ NpcGuild.MinersGuild, PlayerInfoStringConstants.GUILD_VENDOR_MINERS },
			{ NpcGuild.MerchantsGuild, PlayerInfoStringConstants.GUILD_VENDOR_MERCHANTS },
			{ NpcGuild.TinkersGuild, PlayerInfoStringConstants.GUILD_VENDOR_TINKERS },
			{ NpcGuild.TailorsGuild, PlayerInfoStringConstants.GUILD_VENDOR_TAILORS },
			{ NpcGuild.FishermensGuild, PlayerInfoStringConstants.GUILD_VENDOR_MARINERS },
			{ NpcGuild.BardsGuild, PlayerInfoStringConstants.GUILD_VENDOR_BARDS },
			{ NpcGuild.BlacksmithsGuild, PlayerInfoStringConstants.GUILD_VENDOR_BLACKSMITHS },
			{ NpcGuild.NecromancersGuild, PlayerInfoStringConstants.GUILD_VENDOR_BLACK_MAGIC },
			{ NpcGuild.AlchemistsGuild, PlayerInfoStringConstants.GUILD_VENDOR_ALCHEMISTS },
			{ NpcGuild.DruidsGuild, PlayerInfoStringConstants.GUILD_VENDOR_DRUIDS },
			{ NpcGuild.ArchersGuild, PlayerInfoStringConstants.GUILD_VENDOR_ARCHERS },
			{ NpcGuild.CarpentersGuild, PlayerInfoStringConstants.GUILD_VENDOR_CARPENTERS },
			{ NpcGuild.CartographersGuild, PlayerInfoStringConstants.GUILD_VENDOR_CARTOGRAPHERS },
			{ NpcGuild.LibrariansGuild, PlayerInfoStringConstants.GUILD_VENDOR_LIBRARIANS },
			{ NpcGuild.CulinariansGuild, PlayerInfoStringConstants.GUILD_VENDOR_CULINARY },
			{ NpcGuild.AssassinsGuild, PlayerInfoStringConstants.GUILD_VENDOR_ASSASSINS }
		};

		/// <summary>
		/// Dictionary mapping NpcGuild enum values to status format guild titles (for GetStatusGuild).
		/// </summary>
		private static readonly System.Collections.Generic.Dictionary<NpcGuild, string> GuildStatusTitles = new System.Collections.Generic.Dictionary<NpcGuild, string>
		{
			{ NpcGuild.MagesGuild, PlayerInfoStringConstants.GUILD_STATUS_WIZARDS },
			{ NpcGuild.WarriorsGuild, PlayerInfoStringConstants.GUILD_STATUS_WARRIORS },
			{ NpcGuild.ThievesGuild, PlayerInfoStringConstants.GUILD_STATUS_THIEVES },
			{ NpcGuild.RangersGuild, PlayerInfoStringConstants.GUILD_STATUS_RANGERS },
			{ NpcGuild.HealersGuild, PlayerInfoStringConstants.GUILD_STATUS_HEALERS },
			{ NpcGuild.MinersGuild, PlayerInfoStringConstants.GUILD_STATUS_MINERS },
			{ NpcGuild.MerchantsGuild, PlayerInfoStringConstants.GUILD_STATUS_MERCHANTS },
			{ NpcGuild.TinkersGuild, PlayerInfoStringConstants.GUILD_STATUS_TINKERS },
			{ NpcGuild.TailorsGuild, PlayerInfoStringConstants.GUILD_STATUS_TAILORS },
			{ NpcGuild.FishermensGuild, PlayerInfoStringConstants.GUILD_STATUS_MARINERS },
			{ NpcGuild.BardsGuild, PlayerInfoStringConstants.GUILD_STATUS_BARDS },
			{ NpcGuild.BlacksmithsGuild, PlayerInfoStringConstants.GUILD_STATUS_BLACKSMITHS },
			{ NpcGuild.NecromancersGuild, PlayerInfoStringConstants.GUILD_STATUS_BLACK_MAGIC },
			{ NpcGuild.AlchemistsGuild, PlayerInfoStringConstants.GUILD_STATUS_ALCHEMISTS },
			{ NpcGuild.DruidsGuild, PlayerInfoStringConstants.GUILD_STATUS_DRUIDS },
			{ NpcGuild.ArchersGuild, PlayerInfoStringConstants.GUILD_STATUS_ARCHERS },
			{ NpcGuild.CarpentersGuild, PlayerInfoStringConstants.GUILD_STATUS_CARPENTERS },
			{ NpcGuild.CartographersGuild, PlayerInfoStringConstants.GUILD_STATUS_CARTOGRAPHERS },
			{ NpcGuild.LibrariansGuild, PlayerInfoStringConstants.GUILD_STATUS_LIBRARIANS },
			{ NpcGuild.CulinariansGuild, PlayerInfoStringConstants.GUILD_STATUS_CULINARY },
			{ NpcGuild.AssassinsGuild, PlayerInfoStringConstants.GUILD_STATUS_ASSASSINS }
		};

		#endregion
	}
}

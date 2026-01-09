using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Regions;

using Server.Items;

namespace Server.Misc
{
	public class SkillCheck
	{
		#region Guild Skills Lookup

		/// <summary>
		/// Dictionary mapping NPC guilds to their associated skills for faster lookup.
		/// Replaces the massive if-else chain in IsGuildSkill, reducing complexity from ~25 to 1.
		/// </summary>
		private static readonly Dictionary<NpcGuild, HashSet<SkillName>> s_GuildSkills = new Dictionary<NpcGuild, HashSet<SkillName>>();

		/// <summary>
		/// Initializes the guild skills lookup dictionary.
		/// </summary>
		private static void InitializeGuildSkills()
		{
			if (s_GuildSkills.Count > 0)
				return; // Already initialized

			s_GuildSkills[NpcGuild.MagesGuild] = new HashSet<SkillName>
			{
				SkillName.EvalInt,
				SkillName.Magery,
				SkillName.Meditation,
				SkillName.Inscribe,
				SkillName.Alchemy
			};

			s_GuildSkills[NpcGuild.WarriorsGuild] = new HashSet<SkillName>
			{
				SkillName.Fencing,
				SkillName.Macing,
				SkillName.Parry,
				SkillName.Swords,
				SkillName.Tactics,
				SkillName.Healing,
				SkillName.Anatomy,
				SkillName.Chivalry
			};

			s_GuildSkills[NpcGuild.ThievesGuild] = new HashSet<SkillName>
			{
				SkillName.Hiding,
				SkillName.Lockpicking,
				SkillName.Snooping,
				SkillName.Stealing,
				SkillName.Stealth,
				SkillName.DetectHidden,
				SkillName.Ninjitsu
			};

			s_GuildSkills[NpcGuild.BardsGuild] = new HashSet<SkillName>
			{
				SkillName.Discordance,
				SkillName.Musicianship,
				SkillName.Peacemaking,
				SkillName.Provocation
			};

			s_GuildSkills[NpcGuild.BlacksmithsGuild] = new HashSet<SkillName>
			{
				SkillName.Blacksmith,
				SkillName.ArmsLore,
				SkillName.Mining,
				SkillName.Tinkering
			};

			s_GuildSkills[NpcGuild.NecromancersGuild] = new HashSet<SkillName>
			{
				SkillName.Forensics,
				SkillName.Necromancy,
				SkillName.SpiritSpeak,
				SkillName.Alchemy,
				SkillName.Inscribe
			};

			s_GuildSkills[NpcGuild.DruidsGuild] = new HashSet<SkillName>
			{
				SkillName.AnimalLore,
				SkillName.AnimalTaming,
				SkillName.Herding,
				SkillName.Veterinary,
				SkillName.Cooking,
				SkillName.Camping,
				SkillName.Tracking
			};

			s_GuildSkills[NpcGuild.CartographersGuild] = new HashSet<SkillName>
			{
				SkillName.Cartography,
				SkillName.RemoveTrap,
				SkillName.Lockpicking,
				SkillName.Fishing
			};

			s_GuildSkills[NpcGuild.AssassinsGuild] = new HashSet<SkillName>
			{
				SkillName.Fencing,
				SkillName.Hiding,
				SkillName.Poisoning,
				SkillName.Stealth,
				SkillName.Archery,
				SkillName.Alchemy,
				SkillName.Bushido
			};

			s_GuildSkills[NpcGuild.MerchantsGuild] = new HashSet<SkillName>
			{
				SkillName.ItemID,
				SkillName.ArmsLore,
				SkillName.TasteID
			};

			s_GuildSkills[NpcGuild.TailorsGuild] = new HashSet<SkillName>
			{
				SkillName.Tailoring,
				SkillName.Tinkering
			};

			s_GuildSkills[NpcGuild.CarpentersGuild] = new HashSet<SkillName>
			{
				SkillName.Carpentry,
				SkillName.Lumberjacking,
				SkillName.Fletching,
				SkillName.Tinkering
			};

			s_GuildSkills[NpcGuild.CulinariansGuild] = new HashSet<SkillName>
			{
				SkillName.Cooking,
				SkillName.TasteID,
				SkillName.Tinkering
			};

			s_GuildSkills[NpcGuild.TinkersGuild] = new HashSet<SkillName>
			{
				SkillName.Tinkering,
				SkillName.Fletching,
				SkillName.Carpentry,
				SkillName.Tailoring
			};

			s_GuildSkills[NpcGuild.ArchersGuild] = new HashSet<SkillName>
			{
				SkillName.Archery,
				SkillName.Fletching,
				SkillName.Tactics,
				SkillName.Chivalry
			};

			s_GuildSkills[NpcGuild.AlchemistsGuild] = new HashSet<SkillName>
			{
				SkillName.Alchemy,
				SkillName.Cooking,
				SkillName.TasteID
			};

			s_GuildSkills[NpcGuild.RangersGuild] = new HashSet<SkillName>
			{
				SkillName.Camping,
				SkillName.Tracking
			};

			s_GuildSkills[NpcGuild.LibrariansGuild] = new HashSet<SkillName>
			{
				SkillName.ItemID,
				SkillName.Inscribe
			};

			s_GuildSkills[NpcGuild.FishermensGuild] = new HashSet<SkillName>
			{
				SkillName.Fishing
			};

			s_GuildSkills[NpcGuild.HealersGuild] = new HashSet<SkillName>
			{
				SkillName.Anatomy,
				SkillName.Healing,
				SkillName.Veterinary
			};

			s_GuildSkills[NpcGuild.MinersGuild] = new HashSet<SkillName>
			{
				SkillName.Mining,
				SkillName.ArmsLore,
				SkillName.Blacksmith,
				SkillName.Tinkering
			};
		}

		#endregion

		private static readonly bool AntiMacroCode = MyServerSettings.NoMacroing();		// Change this to false to disable anti-macro code
		public static TimeSpan AntiMacroExpire = TimeSpan.FromMinutes( 5.0 ); 			// How long do we remember targets/locations?
		public const int Allowance = 3;													// How many times may we use the same location/target for gain
		private const int LocationSize = 5; 											// The size of each location, make this smaller so players don't have to move as far
		private static bool[] UseAntiMacro = new bool[]
		{
			// true if this skill uses the anti-macro code, false if it does not
			false,// Alchemy = 0,
			false,// Anatomy = 1,
			false,// AnimalLore = 2,
			false,// ItemID = 3,
			false,// ArmsLore = 4,
			false,// Parry = 5,
			false,// Begging = 6,
			false,// Blacksmith = 7,
			false,// Fletching = 8,
			false,// Peacemaking = 9,
			false,// Camping = 10,
			false,// Carpentry = 11,
			false,// Cartography = 12,
			false,// Cooking = 13,
			false,// DetectHidden = 14,
			false,// Discordance = 15,
			false,// EvalInt = 16,
			false,// Healing = 17,
			false,// Fishing = 18,
			false,// Forensics = 19,
			false,// Herding = 20,
			true,// Hiding = 21,
			false,// Provocation = 22,
			false,// Inscribe = 23,
			false,// Lockpicking = 24,
			false,// Magery = 25,
			false,// MagicResist = 26,
			false,// Tactics = 27,
			false,// Snooping = 28,
			true,// Musicianship = 29,
			false,// Poisoning = 30,
			false,// Archery = 31,
			true,// SpiritSpeak = 32,
			false,// Stealing = 33,
			false,// Tailoring = 34,
			false,// AnimalTaming = 35,
			false,// TasteID = 36,
			false,// Tinkering = 37,
			false,// Tracking = 38,
			false,// Veterinary = 39,
			true,// Swords = 40,
			true,// Macing = 41,
			true,// Fencing = 42,
			true,// Wrestling = 43,
			false,// Lumberjacking = 44,
			false,// Mining = 45,
			false,// Meditation = 46,
			false,// Stealth = 47,
			false,// RemoveTrap = 48,
			false,// Necromancy = 49,
			true,// Focus = 50,
			false,// Chivalry = 51
			false,// Bushido = 52
			false,//Ninjitsu = 53
			false // Spellweaving
		};

		public static void Initialize()
		{
			InitializeGuildSkills();

			Mobile.SkillCheckLocationHandler = new SkillCheckLocationHandler( Mobile_SkillCheckLocation );
			Mobile.SkillCheckDirectLocationHandler = new SkillCheckDirectLocationHandler( Mobile_SkillCheckDirectLocation );

			Mobile.SkillCheckTargetHandler = new SkillCheckTargetHandler( Mobile_SkillCheckTarget );
			Mobile.SkillCheckDirectTargetHandler = new SkillCheckDirectTargetHandler( Mobile_SkillCheckDirectTarget );
		}

	/// <summary>
	/// Calculates skill check chance based on skill value and min/max skill ranges.
	/// Returns a value between 0.0 and 1.0 representing the chance of success.
	/// </summary>
	/// <param name="skillValue">Current skill value</param>
	/// <param name="minSkill">Minimum skill required</param>
	/// <param name="maxSkill">Maximum skill for the check</param>
	/// <param name="chance">Output: calculated chance (0.0-1.0)</param>
	/// <returns>True if skill check is valid (not too difficult or too easy), false otherwise</returns>
	private static bool CalculateSkillChance( double skillValue, double minSkill, double maxSkill, out double chance )
	{
		chance = 0.0;

		if ( skillValue < minSkill )
			return false; // Too difficult
		else if ( skillValue >= maxSkill )
		{
			chance = 1.0; // No challenge
			return true;
		}

		// Linear interpolation: chance increases from 0.0 to 1.0 as skill increases from minSkill to maxSkill
		chance = (skillValue - minSkill) / (maxSkill - minSkill);
		return true;
		}

		public static bool Mobile_SkillCheckLocation( Mobile from, SkillName skillName, double minSkill, double maxSkill )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

		double chance;
		if ( !CalculateSkillChance( skill.Value, minSkill, maxSkill, out chance ) )
				return false; // Too difficult

			Point2D loc = new Point2D( from.Location.X / LocationSize, from.Location.Y / LocationSize );
			return CheckSkill( from, skill, loc, chance );
		}

	/// <summary>
	/// Validates a direct skill chance value.
	/// </summary>
	/// <param name="chance">The chance value to validate</param>
	/// <returns>True if chance is valid (0.0-1.0), false if too difficult</returns>
	private static bool ValidateSkillChance( double chance )
	{
		if ( chance < 0.0 )
			return false; // Too difficult
		else if ( chance >= 1.0 )
			return true; // No challenge (will be handled by CheckSkill)
		
		return true;
	}

		public static bool Mobile_SkillCheckDirectLocation( Mobile from, SkillName skillName, double chance )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

		if ( !ValidateSkillChance( chance ) )
				return false; // Too difficult

			Point2D loc = new Point2D( from.Location.X / LocationSize, from.Location.Y / LocationSize );
			return CheckSkill( from, skill, loc, chance );
		}

	#region Skill Gain Calculation Helpers

	/// <summary>
	/// Calculates the gainer divisor for skill gain calculations.
	/// Guild members get a bonus gainer (lower divisor = faster gain).
	/// </summary>
	/// <param name="from">The mobile performing the skill check</param>
	/// <param name="skillName">The skill being checked</param>
	/// <returns>The gainer divisor to use</returns>
	private static double CalculateGainer( Mobile from, SkillName skillName )
	{
		double gainer = SkillCheckConstants.DEFAULT_GAINER;

		if ( from is PlayerMobile && IsGuildSkill( from, skillName ) )
		{
			// Guild members gain skills faster (lower gainer = faster gain)
			int randomGainer = Utility.RandomMinMax( 0, SkillCheckConstants.GUILD_GAINER_OPTIONS - 1 );
			gainer = SkillCheckConstants.GUILD_GAINER_MAX - (randomGainer * 0.05);
		}

		return gainer;
	}

	/// <summary>
	/// Calculates the base gain chance (gc) before modifiers.
	/// </summary>
	/// <param name="skill">The skill being checked</param>
	/// <param name="chance">The skill check chance (0.0-1.0)</param>
	/// <param name="gainer">The gainer divisor</param>
	/// <param name="success">Whether the skill check succeeded</param>
	/// <returns>The base gain chance</returns>
	private static double CalculateBaseGainChance( Skill skill, double chance, double gainer, bool success )
	{
		// Base calculation: how much room for improvement (cap - base) / cap
		double gc = (double)(skill.Cap - skill.Base) / skill.Cap;
		gc /= gainer;

		// Add bonus/penalty based on success/failure
		double failureBonus = Core.AOS ? SkillCheckConstants.AOS_FAILURE_BONUS : SkillCheckConstants.LEGACY_FAILURE_BONUS;
		gc += ( 1.0 - chance ) * ( success ? SkillCheckConstants.SUCCESS_BONUS : failureBonus );
		gc /= gainer;

		// Apply skill-specific gain factor
		gc *= skill.Info.GainFactor;

		return gc;
	}

	/// <summary>
	/// Applies skill-specific bonuses to gain chance.
	/// </summary>
	/// <param name="gc">Current gain chance</param>
	/// <param name="skillName">The skill being checked</param>
	/// <param name="amObj">The target object</param>
	/// <returns>Modified gain chance</returns>
	private static double ApplySkillSpecificBonuses( double gc, SkillName skillName, object amObj )
	{
		// Apply Animal Lore wild creature bonus (for learning from untamed/wild animals)
		if ( skillName == SkillName.AnimalLore && amObj is BaseCreature )
		{
			BaseCreature creature = (BaseCreature)amObj;
			// Bonus applies to wild/untamed creatures that are not vendor-bought
			if ( !creature.Controlled && !creature.IsVendorBought )
			{
				gc *= SkillCheckConstants.ANIMAL_LORE_WILD_BONUS;
			}
		}

		return gc;
	}

	/// <summary>
	/// Applies player-specific modifiers to gain chance (Avatar, Hunger, SoulBound, FastGain).
	/// </summary>
	/// <param name="gc">Current gain chance</param>
	/// <param name="from">The player mobile</param>
	/// <returns>Modified gain chance</returns>
	private static double ApplyPlayerModifiers( double gc, PlayerMobile from )
	{
		// Avatar vs Normal player multiplier
		if ( from.Avatar )
			gc *= SkillCheckConstants.AVATAR_GAIN_MULTIPLIER;
		else 
			gc *= SkillCheckConstants.NORMAL_GAIN_MULTIPLIER;

		// Hunger system modifiers
		if ( from.Hunger <= SkillCheckConstants.HUNGER_STARVING )
			gc /= SkillCheckConstants.STARVING_PENALTY;
		else if ( from.Hunger <= SkillCheckConstants.HUNGER_HUNGRY )
			gc /= SkillCheckConstants.HUNGRY_PENALTY;
		else if ( from.Hunger <= SkillCheckConstants.HUNGER_NORMAL )
			gc *= SkillCheckConstants.NORMAL_HUNGER_BONUS;
		else if ( from.Hunger <= SkillCheckConstants.HUNGER_WELL_FED )
			gc *= SkillCheckConstants.WELL_FED_BONUS;
		else if ( from.Hunger <= SkillCheckConstants.HUNGER_VERY_WELL_FED )
			gc *= SkillCheckConstants.VERY_WELL_FED_BONUS;
		else if ( from.Hunger <= SkillCheckConstants.HUNGER_OVERFED )
			gc *= SkillCheckConstants.OVERFED_BONUS;

		// SoulBound phylactery modifier
		double gcModifier = 1.00;
		if ( from.SoulBound )
		{
			Phylactery phylactery = from.FindPhylactery();
			if ( phylactery != null )
			{
				gcModifier += phylactery.CalculateSkillGainModifier();
			}
		}
		gc *= gcModifier;

		// FastGain modifier (if > 1)
		if ( from.FastGain > 1 )
			gc *= from.FastGain;

		return gc;
	}

	/// <summary>
	/// Applies region-specific bonuses to gain chance (Church of Justice, Basement).
	/// </summary>
	/// <param name="gc">Current gain chance</param>
	/// <param name="from">The player mobile</param>
	/// <returns>Modified gain chance</returns>
	private static double ApplyRegionBonuses( double gc, PlayerMobile from )
	{
		// Church of Justice bonus (Trammel, specific coordinates, specific guild)
		if ( from.Map != null && from.Map == Map.Trammel 
			&& from.X >= SkillCheckConstants.CHURCH_OF_JUSTICE_X_MIN 
			&& from.X <= SkillCheckConstants.CHURCH_OF_JUSTICE_X_MAX 
			&& from.Y >= SkillCheckConstants.CHURCH_OF_JUSTICE_Y_MIN 
			&& from.Y <= SkillCheckConstants.CHURCH_OF_JUSTICE_Y_MAX 
			&& from.Guild != null 
			&& !from.Guild.Disbanded 
			&& Insensitive.Contains( from.Guild.Name, SkillCheckConstants.CHURCH_OF_JUSTICE_GUILD_NAME ) )
		{
			gc *= SkillCheckConstants.CHURCH_OF_JUSTICE_BONUS;
		}

		// Basement SoulBound bonus
		if ( from.SoulBound && Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) == "the Basement" )
			gc *= SkillCheckConstants.BASEMENT_SOULBOUND_BONUS;

		return gc;
	}

	/// <summary>
	/// Calculates dungeon region bonus based on skill type and difficulty level.
	/// </summary>
	/// <param name="skillName">The skill being checked</param>
	/// <param name="difficultyLevel">The difficulty level of the dungeon</param>
	/// <returns>The bonus multiplier to apply</returns>
	private static double CalculateDungeonBonus( SkillName skillName, double difficultyLevel )
	{
		double bonus = difficultyLevel / SkillCheckConstants.DIFFICULTY_DIVISOR;

		// Passive skills get reduced bonus
		if ( skillName == SkillName.DetectHidden ||
			skillName == SkillName.Meditation ||
			skillName == SkillName.TasteID ||
			skillName == SkillName.ItemID ||
			skillName == SkillName.Anatomy ||
			skillName == SkillName.Lockpicking ||
			skillName == SkillName.RemoveTrap ||
			skillName == SkillName.Tactics ||
			skillName == SkillName.Musicianship ||
			skillName == SkillName.AnimalLore ||
			skillName == SkillName.Mining ||
			skillName == SkillName.Tracking ||
			skillName == SkillName.Parry ||
			skillName == SkillName.EvalInt ||
			skillName == SkillName.Hiding )
		{
			bonus = 1 + (bonus / SkillCheckConstants.PASSIVE_SKILL_DIVISOR);
		}
		// Active combat/magic skills get full bonus
		else if ( skillName == SkillName.Swords ||
			skillName == SkillName.Fencing ||
			skillName == SkillName.Macing ||
			skillName == SkillName.Magery ||
			skillName == SkillName.Necromancy ||
			skillName == SkillName.Provocation ||
			skillName == SkillName.Discordance ||
			skillName == SkillName.Peacemaking ||
			skillName == SkillName.Chivalry ||
			skillName == SkillName.Stealing ||
			skillName == SkillName.Stealth ||
			skillName == SkillName.Ninjitsu ||
			skillName == SkillName.Bushido ||
			skillName == SkillName.Archery )
		{
			bonus = 1 + bonus;
		}
		// Other skills get no bonus
		else
		{
			bonus = 1;
		}

		return bonus;
	}

	#endregion

	public static bool CheckSkill( Mobile from, Skill skill, object amObj, double chance )
	{
		SkillName skillName = skill.SkillName;

		if ( from.Skills.Cap == 0 )
			return false;

		// Calculate gainer (guild bonus)
		double gainer = CalculateGainer( from, skillName );

		// Determine success
			bool success = ( chance >= Utility.RandomDouble() );

		// Calculate base gain chance
		double gc = CalculateBaseGainChance( skill, chance, gainer, success );

		// Apply skill-specific bonuses
		gc = ApplySkillSpecificBonuses( gc, skillName, amObj );

		// Ensure minimum gain chance
		if ( gc < SkillCheckConstants.MIN_GAIN_CHANCE )
			gc = SkillCheckConstants.MIN_GAIN_CHANCE;

		// Controlled creature bonus
			if ( from is BaseCreature && ((BaseCreature)from).Controlled )
			gc *= SkillCheckConstants.CONTROLLED_CREATURE_BONUS;

		// Apply player modifiers
		if ( from is PlayerMobile )
		{
			PlayerMobile pm = (PlayerMobile)from;
			gc = ApplyPlayerModifiers( gc, pm );
			gc = ApplyRegionBonuses( gc, pm );

			// Dungeon region bonus
			Region reg = Region.Find( from.Location, from.Map );
			if ( reg.IsPartOf( typeof( BardDungeonRegion ) ) || reg.IsPartOf( typeof( DungeonRegion ) ) )
			{
				double difficultyLevel = Misc.MyServerSettings.GetDifficultyLevel( from.Location, from.Map );
				double bonus = CalculateDungeonBonus( skillName, difficultyLevel );
				if ( bonus >= 1 )
					gc *= bonus;
			}
		}

			// Store random value for skill gain check
			double randomValue = Utility.RandomDouble();
			bool allowGainResult = AllowGain( from, skill, amObj );
			
			// Store skill value before gain for debug messages
			// double skillBefore = skill.Base; // DEBUG: Commented out

			if ( from.Alive && ( ( gc >= randomValue && allowGainResult ) || skill.Base < SkillCheckConstants.MIN_SKILL_BASE_FOR_GAIN ) )
			{
				// CAN ONLY GAIN FISHING SKILL ON A BOAT AFTER REACHING 60
				if ( Worlds.IsOnBoat( from ) == false && skill.SkillName == SkillName.Fishing && from.Skills[SkillName.Fishing].Base >= SkillCheckConstants.FISHING_BOAT_REQUIREMENT )
				{
					from.SendMessage(SkillCheckStringConstants.MSG_FISHING_REQUIRES_BOAT);
				}
				else
				{
					Gain( from, skill );
					// bool skillGained = true; // DEBUG: Commented out
				}
			}
			
			/* DEBUG: Commented out - Debug messages for Animal Lore and Animal Taming when skill gain succeeds
			if ( from is PlayerMobile && from.Alive && success && skillGained && ( skillName == SkillName.AnimalLore || skillName == SkillName.AnimalTaming ) )
			{
				double skillAfter = skill.Base;
				double actualGain = skillAfter - skillBefore;
				double successPercentage = gc * 100.0;
				
				string message;
				if ( skillName == SkillName.AnimalLore )
				{
					message = string.Format( SkillCheckStringConstants.MSG_DEBUG_ANIMAL_LORE_SUCCESS_FORMAT, successPercentage, actualGain );
				}
				else
				{
					message = string.Format( SkillCheckStringConstants.MSG_DEBUG_ANIMAL_TAMING_SUCCESS_FORMAT, successPercentage, actualGain );
				}
				
				from.SendMessage( 0x3FF, message );
			}
			*/

			/* DEBUG: Commented out - Debug messages for Animal Lore and Animal Taming when skill gain fails
			// Show when: skill check succeeded, but gain failed due to low chance or anti-macro
			if ( from is PlayerMobile && from.Alive && success && !skillGained && skill.Base >= SkillCheckConstants.MIN_SKILL_BASE_FOR_GAIN )
			{
				if ( skillName == SkillName.AnimalLore || skillName == SkillName.AnimalTaming )
				{
					double successPercentage = gc * 100.0;
					string antiMacroStatus = allowGainResult ? "OK" : "BLOQUEADO";
					string lockStatus = skill.Lock == SkillLock.Up ? "OK" : skill.Lock.ToString();
					string capStatus = skill.Base < skill.Cap ? "OK" : "ATINGIDO";
					
					// Check if total skill cap would be exceeded (assuming toGain = 1)
					Skills skills = from.Skills;
					string totalCapStatus = "OK";
					int currentTotal = skills.Total;
					int skillCap = skills.Cap;
					if ( from.Player && (currentTotal + 1) > skillCap )
					{
						totalCapStatus = "EXCEDIDO";
				}
					
					// Convert to display format (divide by 10 to show as skill points)
					double currentTotalDisplay = (double)currentTotal / 10.0;
					double skillCapDisplay = (double)skillCap / 10.0;
					
					string message;
					if ( skillName == SkillName.AnimalLore )
					{
						message = string.Format( SkillCheckStringConstants.MSG_DEBUG_ANIMAL_LORE_FAILED_FORMAT, 
							successPercentage, antiMacroStatus, lockStatus, capStatus, totalCapStatus, currentTotalDisplay, skillCapDisplay );
					}
					else
					{
						message = string.Format( SkillCheckStringConstants.MSG_DEBUG_ANIMAL_TAMING_FAILED_FORMAT, 
							successPercentage, antiMacroStatus, lockStatus, capStatus, totalCapStatus, currentTotalDisplay, skillCapDisplay );
					}
					
					from.SendMessage( 0x3B2, message );
				}
			}
			*/

			if (!success && from.Player && ((PlayerMobile)from).THC > 0)
			{
				PlayerMobile pm = (PlayerMobile)from;
				if (Utility.RandomDouble() < (SkillCheckConstants.THC_BOOST_MULTIPLIER * ((double)pm.THC / SkillCheckConstants.THC_BOOST_DIVISOR)))
				{
					success = true;
					from.SendMessage(SkillCheckStringConstants.MSG_THC_SKILL_BOOST);
				}
			}

			return success;
		}

		public static bool Mobile_SkillCheckTarget( Mobile from, SkillName skillName, object target, double minSkill, double maxSkill )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

		double chance;
		if ( !CalculateSkillChance( skill.Value, minSkill, maxSkill, out chance ) )
				return false; // Too difficult

			return CheckSkill( from, skill, target, chance );
		}

		public static bool Mobile_SkillCheckDirectTarget( Mobile from, SkillName skillName, object target, double chance )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

		if ( !ValidateSkillChance( chance ) )
				return false; // Too difficult

			return CheckSkill( from, skill, target, chance );
		}

		/// <summary>
		/// Checks if a skill is associated with the player's NPC guild.
		/// Uses dictionary lookup for O(1) performance instead of O(n) if-else chain.
		/// </summary>
		/// <param name="from">The mobile to check</param>
		/// <param name="skillName">The skill to check</param>
		/// <returns>True if the skill is a guild skill for the player's guild</returns>
		public static bool IsGuildSkill( Mobile from, SkillName skillName )
		{
			if (!(from is PlayerMobile))
				return false;

			PlayerMobile pm = (PlayerMobile)from;

			if (pm.NpcGuild == NpcGuild.None)
				return false;

			HashSet<SkillName> guildSkills;
			if (s_GuildSkills.TryGetValue(pm.NpcGuild, out guildSkills))
			{
				return guildSkills.Contains(skillName);
			}

			return false;
		}




		private static bool AllowGain( Mobile from, Skill skill, object obj )
		{

			if ( AntiMacroCode && from is PlayerMobile && UseAntiMacro[skill.Info.SkillID] )
				return ((PlayerMobile)from).AntiMacroCheck( skill, obj );
			else
				return true;
		}

		public enum Stat { Str, Dex, Int }

		public static void Gain( Mobile from, Skill skill )
		{
			if ( from.Region.IsPartOf( typeof( Regions.Jail ) ) )
				return;

			if ( from is BaseCreature && ((BaseCreature)from).IsDeadPet )
				return;

			if ( skill.SkillName == SkillName.Focus && from is BaseCreature )
				return;

			if ( skill.Base < skill.Cap && skill.Lock == SkillLock.Up )
			{
				int toGain = 1;
				int harder = 1;


				if (AdventuresFunctions.IsInMidland((object)from))
				{
					// Midland Region skill gain rates
					if ( skill.Base <= 25.0 )
						toGain = Utility.RandomMinMax(3, 4); // 0.3-0.4 skill points

					else if ( skill.Base <= 55.0 )
						toGain = Utility.RandomMinMax(2, 3); // 0.2-0.3 skill points

					else if ( skill.Base <= 90.0 )
						toGain = 2; // 0.2 skill points (fixed)

					else if ( skill.Base <= 100.0 )
						toGain = 1; // 0.1 skill points (fixed)

					else
					{
						// Skill > 100.0: Gain becomes harder (chance-based)
						if ( skill.Base <= 105.0 )
						{
							harder = Utility.RandomMinMax( 0, 1 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
						else if ( skill.Base <= 110.0 )
						{
							harder = Utility.RandomMinMax( 0, 2 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
						else if ( skill.Base >= 110.1 )
						{
							harder = Utility.RandomMinMax( 0, 3 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}	
						}	

					if ( toGain > 0 && (skill.SkillName == SkillName.Focus || skill.SkillName == SkillName.Meditation))
						{
							harder = Utility.RandomMinMax( 0, 2 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
				}
				else
				{
					// Normal Region skill gain rates
					if ( skill.Base <= 25.0 )
						toGain = Utility.RandomMinMax(2, 3); // 0.2-0.3 skill points

					else if ( skill.Base <= 55.0 )
						toGain = 2; // 0.2 skill points (fixed)

					else if ( skill.Base <= 90.0 )
						toGain = 1; // 0.1 skill points (fixed)

					else
					{
						// Skill > 90.0: Gain becomes harder (chance-based)
						if ( skill.Base <= 95.0 )
						{
							harder = Utility.RandomMinMax( 0, 1 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
						else if ( skill.Base <= 100.0 )
						{
							harder = Utility.RandomMinMax( 0, 2 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
						else if ( skill.Base <= 105 )
						{
							harder = Utility.RandomMinMax( 0, 3 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
						else if ( skill.Base <= 110 )
						{
							harder = Utility.RandomMinMax( 0, 4 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
						
						else if ( skill.Base >= 110.1 )
						{
							harder = Utility.RandomMinMax( 0, 5 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}	
						}	

					if ( toGain > 0 && (skill.SkillName == SkillName.Focus || skill.SkillName == SkillName.Meditation))
						{
							harder = Utility.RandomMinMax( 0, 2 );
							if ( harder == 1 )
								toGain = 1;
							else 
								toGain = 0;
						}
				}				


				Skills skills = from.Skills;

				if ( from.Player && ( skills.Total / skills.Cap ) >= Utility.RandomDouble() )//( skills.Total >= skills.Cap )
				{
					for ( int i = 0; i < skills.Length; ++i )
					{
						Skill toLower = skills[i];

						if ( toLower != skill && toLower.Lock == SkillLock.Down && toLower.BaseFixedPoint >= toGain )
						{
							toLower.BaseFixedPoint -= toGain;
							break;
						}
					}
				}

				#region Scroll of Alacrity
				PlayerMobile pm = from as PlayerMobile;

				if ( from is PlayerMobile )
					if (pm != null && skill.SkillName == pm.AcceleratedSkill && pm.AcceleratedStart > DateTime.UtcNow)
					toGain *= Utility.RandomMinMax(SkillCheckConstants.SCROLL_ALACRITY_GAIN_MIN, SkillCheckConstants.SCROLL_ALACRITY_GAIN_MAX);
					#endregion

				if (from is PlayerMobile && skill.BaseFixedPoint < SkillCheckConstants.SKILL_MILESTONE_THRESHOLD && (toGain + skill.BaseFixedPoint >= SkillCheckConstants.SKILL_MILESTONE_THRESHOLD))
					LoggingFunctions.LogGM( from, skill );

				if ( !from.Player || (skills.Total + toGain) <= skills.Cap )
				{
					skill.BaseFixedPoint += toGain;

					if ( skill.SkillName == SkillName.Focus && Utility.RandomMinMax( 1, SkillCheckConstants.FOCUS_MEDITATION_REFRESH_CHANCE ) == 1 )
						{ Server.Gumps.SkillListingGump.RefreshSkillList( from ); }

					else if ( skill.SkillName == SkillName.Meditation && Utility.RandomMinMax( 1, SkillCheckConstants.FOCUS_MEDITATION_REFRESH_CHANCE ) == 1 )
						{ Server.Gumps.SkillListingGump.RefreshSkillList( from ); }

					else
						{ Server.Gumps.SkillListingGump.RefreshSkillList( from ); }
				}
			}

			if ( skill.Lock == SkillLock.Up )
			{
				SkillInfo info = skill.Info;

				if ( from.StrLock == StatLockType.Up && (info.StrGain / SkillCheckConstants.STAT_GAIN_DIVISOR) > Utility.RandomDouble() )
					GainStat( from, Stat.Str );
				else if ( from.DexLock == StatLockType.Up && (info.DexGain / SkillCheckConstants.STAT_GAIN_DIVISOR) > Utility.RandomDouble() )
					GainStat( from, Stat.Dex );
				else if ( from.IntLock == StatLockType.Up && (info.IntGain / SkillCheckConstants.STAT_GAIN_DIVISOR) > Utility.RandomDouble() )
					GainStat( from, Stat.Int );
			}
		}

		public static bool CanLower( Mobile from, Stat stat )
		{
			switch ( stat )
			{
				case Stat.Str: return ( from.StrLock == StatLockType.Down && from.RawStr > 10 );
				case Stat.Dex: return ( from.DexLock == StatLockType.Down && from.RawDex > 10 );
				case Stat.Int: return ( from.IntLock == StatLockType.Down && from.RawInt > 10 );
			}

			return false;
		}

		public static bool CanRaise( Mobile from, Stat stat )
		{
			if ( !(from is BaseCreature && ((BaseCreature)from).Controlled) )
			{
				if ( from.RawStatTotal >= from.StatCap )
					return false;
			}

			if ( from.StatCap > SkillCheckConstants.HIGH_STAT_CAP_THRESHOLD )
			{
				switch ( stat )
				{
					case Stat.Str: return ( from.StrLock == StatLockType.Up && from.RawStr < SkillCheckConstants.HIGH_STAT_CAP_MAX );
					case Stat.Dex: return ( from.DexLock == StatLockType.Up && from.RawDex < SkillCheckConstants.HIGH_STAT_CAP_MAX );
					case Stat.Int: return ( from.IntLock == StatLockType.Up && from.RawInt < SkillCheckConstants.HIGH_STAT_CAP_MAX );
				}
			}
			else
			{
				switch ( stat )
				{
					case Stat.Str: return ( from.StrLock == StatLockType.Up && from.RawStr < SkillCheckConstants.NORMAL_STAT_CAP_MAX );
					case Stat.Dex: return ( from.DexLock == StatLockType.Up && from.RawDex < SkillCheckConstants.NORMAL_STAT_CAP_MAX );
					case Stat.Int: return ( from.IntLock == StatLockType.Up && from.RawInt < SkillCheckConstants.NORMAL_STAT_CAP_MAX );
				}
			}

			return false;
		}

		public static void IncreaseStat( Mobile from, Stat stat, bool atrophy )
		{
			atrophy = atrophy || (from.RawStatTotal >= from.StatCap);

			switch ( stat )
			{
				case Stat.Str:
				{
					if ( atrophy )
					{
						if ( CanLower( from, Stat.Dex ) && (from.RawDex < from.RawInt || !CanLower( from, Stat.Int )) )
							--from.RawDex;
						else if ( CanLower( from, Stat.Int ) )
							--from.RawInt;
					}

					if ( CanRaise( from, Stat.Str ) )
						++from.RawStr;

					break;
				}
				case Stat.Dex:
				{
					if ( atrophy )
					{
						if ( CanLower( from, Stat.Str ) && (from.RawStr < from.RawInt || !CanLower( from, Stat.Int )) )
							--from.RawStr;
						else if ( CanLower( from, Stat.Int ) )
							--from.RawInt;
					}

					if ( CanRaise( from, Stat.Dex ) )
						++from.RawDex;

					break;
				}
				case Stat.Int:
				{
					if ( atrophy )
					{
						if ( CanLower( from, Stat.Str ) && (from.RawStr < from.RawDex || !CanLower( from, Stat.Dex )) )
							--from.RawStr;
						else if ( CanLower( from, Stat.Dex ) )
							--from.RawDex;
					}

					if ( CanRaise( from, Stat.Int ) )
						++from.RawInt;

					break;
				}
			}
		}

		private static double m_DefaultStatGainDelay = SkillCheckConstants.DEFAULT_STAT_GAIN_DELAY_MINUTES;
		private static TimeSpan m_StatGainDelay = TimeSpan.FromMinutes(m_DefaultStatGainDelay);
		private static TimeSpan m_PetStatGainDelay = TimeSpan.FromMinutes( SkillCheckConstants.PET_STAT_GAIN_DELAY_MINUTES );

		public static void GainStat( Mobile from, Stat stat )
		{
			if (from is PlayerMobile)
			{
				if ( ((PlayerMobile)from).SoulBound) {
					Phylactery phylactery = ((PlayerMobile)from).FindPhylactery();
					if (phylactery != null) {
						m_StatGainDelay = TimeSpan.FromMinutes(phylactery.CalculateStatGainModifier(m_DefaultStatGainDelay));
					}
				}
				if ( ((PlayerMobile)from).FastGain > SkillCheckConstants.FAST_GAIN_THRESHOLD)
					m_StatGainDelay = TimeSpan.FromMinutes( ( m_DefaultStatGainDelay / SkillCheckConstants.FAST_GAIN_DELAY_DIVISOR ));
			}
		
			switch( stat )
			{
				case Stat.Str:
				{
					if ( from is BaseCreature && ((BaseCreature)from).Controlled ) {
						if ( (from.LastStrGain + m_PetStatGainDelay) >= DateTime.UtcNow )
							return;
					}
					else if( (from.LastStrGain + m_StatGainDelay) >= DateTime.UtcNow )
						return;

					from.LastStrGain = DateTime.UtcNow;
					break;
				}
				case Stat.Dex:
				{
					if ( from is BaseCreature && ((BaseCreature)from).Controlled ) {
						if ( (from.LastDexGain + m_PetStatGainDelay) >= DateTime.UtcNow )
							return;
					}
					else if( (from.LastDexGain + m_StatGainDelay) >= DateTime.UtcNow )
						return;

					from.LastDexGain = DateTime.UtcNow;
					break;
				}
				case Stat.Int:
				{
					if ( from is BaseCreature && ((BaseCreature)from).Controlled ) {
						if ( (from.LastIntGain + m_PetStatGainDelay) >= DateTime.UtcNow )
							return;
					}

					else if( (from.LastIntGain + m_StatGainDelay) >= DateTime.UtcNow )
						return;

					from.LastIntGain = DateTime.UtcNow;
					break;
				}
			}

			bool atrophy = ( (from.RawStatTotal / (double)from.StatCap) >= Utility.RandomDouble() );

			IncreaseStat( from, stat, atrophy );
		}
	}
}

using System;
using System.Collections.Generic;

namespace Server.Items
{
	/// <summary>
	/// Metadata registry for potion types, providing centralized access to
	/// potion factory methods, display names, and visual properties.
	/// Replaces three 55+ case switch statements from PotionKeg.cs.
	/// </summary>
	public static class PotionMetadata
	{
		#region Potion Data Structure
		
		/// <summary>
		/// Metadata for a single potion type
		/// </summary>
		public class PotionInfo
		{
			/// <summary>Factory method to create potion instance</summary>
			public Func<BasePotion> CreatePotion { get; set; }
			
			/// <summary>Display name for keg (Portuguese-Brazilian)</summary>
			public string KegName { get; set; }
			
			/// <summary>Hue/color for keg when containing this potion</summary>
			public int KegHue { get; set; }
			
			/// <summary>Whether this potion uses jars instead of bottles</summary>
			public bool UsesJar { get; set; }
		}
		
		#endregion
		
		#region Potion Registry
		
		/// <summary>
		/// Central registry mapping PotionEffect to metadata
		/// </summary>
		private static readonly Dictionary<PotionEffect, PotionInfo> Registry = 
			new Dictionary<PotionEffect, PotionInfo>
		{
			// Basic Potions
			{ PotionEffect.Nightsight, new PotionInfo 
				{ 
					CreatePotion = () => new NightSightPotion(), 
					KegName = "visão noturna", 
					KegHue = 1109,
					UsesJar = false
				} 
			},
			
			// Cure Potions
			{ PotionEffect.CureLesser, new PotionInfo 
				{ 
					CreatePotion = () => new LesserCurePotion(), 
					KegName = "cura menor", 
					KegHue = 46,
					UsesJar = false
				} 
			},
			{ PotionEffect.Cure, new PotionInfo 
				{ 
					CreatePotion = () => new CurePotion(), 
					KegName = "cura", 
					KegHue = 46,
					UsesJar = false
				} 
			},
			{ PotionEffect.CureGreater, new PotionInfo 
				{ 
					CreatePotion = () => new GreaterCurePotion(), 
					KegName = "cura maior", 
					KegHue = 46,
					UsesJar = false
				} 
			},
			
			// Agility Potions
			{ PotionEffect.Agility, new PotionInfo 
				{ 
					CreatePotion = () => new AgilityPotion(), 
					KegName = "agilidade", 
					KegHue = 396,
					UsesJar = false
				} 
			},
			{ PotionEffect.AgilityGreater, new PotionInfo 
				{ 
					CreatePotion = () => new GreaterAgilityPotion(), 
					KegName = "agilidade maior", 
					KegHue = 396,
					UsesJar = false
				} 
			},
			
			// Strength Potions
			{ PotionEffect.Strength, new PotionInfo 
				{ 
					CreatePotion = () => new StrengthPotion(), 
					KegName = "força", 
					KegHue = 1001,
					UsesJar = false
				} 
			},
			{ PotionEffect.StrengthGreater, new PotionInfo 
				{ 
					CreatePotion = () => new GreaterStrengthPotion(), 
					KegName = "força maior", 
					KegHue = 1001,
					UsesJar = false
				} 
			},
			
			// Poison Potions
			{ PotionEffect.PoisonLesser, new PotionInfo 
				{ 
					CreatePotion = () => new LesserPoisonPotion(), 
					KegName = "veneno menor", 
					KegHue = 73,
					UsesJar = false
				} 
			},
			{ PotionEffect.Poison, new PotionInfo 
				{ 
					CreatePotion = () => new PoisonPotion(), 
					KegName = "veneno", 
					KegHue = 73,
					UsesJar = false
				} 
			},
			{ PotionEffect.PoisonGreater, new PotionInfo 
				{ 
					CreatePotion = () => new GreaterPoisonPotion(), 
					KegName = "veneno maior", 
					KegHue = 73,
					UsesJar = false
				} 
			},
			{ PotionEffect.PoisonDeadly, new PotionInfo 
				{ 
					CreatePotion = () => new DeadlyPoisonPotion(), 
					KegName = "veneno mortal", 
					KegHue = 73,
					UsesJar = false
				} 
			},
			{ PotionEffect.PoisonLethal, new PotionInfo 
				{ 
					CreatePotion = () => new LethalPoisonPotion(), 
					KegName = "veneno letal", 
					KegHue = 73,
					UsesJar = false
				} 
			},
			
			// Refresh Potions
			{ PotionEffect.Refresh, new PotionInfo 
				{ 
					CreatePotion = () => new RefreshPotion(), 
					KegName = "vigor", 
					KegHue = 0xEE,
					UsesJar = false
				} 
			},
			{ PotionEffect.RefreshTotal, new PotionInfo 
				{ 
					CreatePotion = () => new TotalRefreshPotion(), 
					KegName = "vigor total", 
					KegHue = 0xEE,
					UsesJar = false
				} 
			},
			
			// Heal Potions
			{ PotionEffect.HealLesser, new PotionInfo 
				{ 
					CreatePotion = () => new LesserHealPotion(), 
					KegName = "vida menor", 
					KegHue = 53,
					UsesJar = false
				} 
			},
			{ PotionEffect.Heal, new PotionInfo 
				{ 
					CreatePotion = () => new HealPotion(), 
					KegName = "vida", 
					KegHue = 53,
					UsesJar = false
				} 
			},
			{ PotionEffect.HealGreater, new PotionInfo 
				{ 
					CreatePotion = () => new GreaterHealPotion(), 
					KegName = "vida maior", 
					KegHue = 53,
					UsesJar = false
				} 
			},
			
			// Explosion Potions
			{ PotionEffect.ExplosionLesser, new PotionInfo 
				{ 
					CreatePotion = () => new LesserExplosionPotion(), 
					KegName = "explosão menor", 
					KegHue = 0x204,
					UsesJar = false
				} 
			},
			{ PotionEffect.Explosion, new PotionInfo 
				{ 
					CreatePotion = () => new ExplosionPotion(), 
					KegName = "explosão", 
					KegHue = 0x204,
					UsesJar = false
				} 
			},
			{ PotionEffect.ExplosionGreater, new PotionInfo 
				{ 
					CreatePotion = () => new GreaterExplosionPotion(), 
					KegName = "explosão maior", 
					KegHue = 0x204,
					UsesJar = false
				} 
			},
			
			// Conflagration Potions
			{ PotionEffect.Conflagration, new PotionInfo 
				{ 
					CreatePotion = () => new ConflagrationPotion(), 
					KegName = "conflagração", 
					KegHue = 0xAD8,
					UsesJar = false
				} 
			},
			{ PotionEffect.ConflagrationGreater, new PotionInfo 
				{ 
					CreatePotion = () => new GreaterConflagrationPotion(), 
					KegName = "conflagração maior", 
					KegHue = 0xAD8,
					UsesJar = false
				} 
			},
			
		// Confusion Blast Potions
		{ PotionEffect.ConfusionBlast, new PotionInfo 
			{ 
				CreatePotion = () => new ConfusionBlastPotion(), 
				KegName = "confusão", 
				KegHue = 0x54F, // 1359 - Cyan/turquoise color
				UsesJar = false
			} 
		},
		{ PotionEffect.ConfusionBlastGreater, new PotionInfo 
			{ 
				CreatePotion = () => new GreaterConfusionBlastPotion(), 
				KegName = "confusão maior", 
				KegHue = 0x54F, // 1359 - Cyan/turquoise color
				UsesJar = false
			} 
		},
			
			// Invisibility Potions
			{ PotionEffect.InvisibilityLesser, new PotionInfo 
				{ 
					CreatePotion = () => new LesserInvisibilityPotion(), 
					KegName = "invisibilidade menor", 
					KegHue = 0x4FE,
					UsesJar = false
				} 
			},
			{ PotionEffect.Invisibility, new PotionInfo 
				{ 
					CreatePotion = () => new InvisibilityPotion(), 
					KegName = "invisibilidade", 
					KegHue = 0x4FE,
					UsesJar = false
				} 
			},
			{ PotionEffect.InvisibilityGreater, new PotionInfo 
				{ 
					CreatePotion = () => new GreaterInvisibilityPotion(), 
					KegName = "invisibilidade maior", 
					KegHue = 0x4FE,
					UsesJar = false
				} 
			},
			
			// Rejuvenate Potions
			{ PotionEffect.RejuvenateLesser, new PotionInfo 
				{ 
					CreatePotion = () => new LesserRejuvenatePotion(), 
					KegName = "rejuvenescimento menor", 
					KegHue = 0x48E,
					UsesJar = false
				} 
			},
			{ PotionEffect.Rejuvenate, new PotionInfo 
				{ 
					CreatePotion = () => new RejuvenatePotion(), 
					KegName = "rejuvenescimento", 
					KegHue = 0x48E,
					UsesJar = false
				} 
			},
			{ PotionEffect.RejuvenateGreater, new PotionInfo 
				{ 
					CreatePotion = () => new GreaterRejuvenatePotion(), 
					KegName = "rejuvenescimento maior", 
					KegHue = 0x48E,
					UsesJar = false
				} 
			},
			
			// Mana Potions
			{ PotionEffect.ManaLesser, new PotionInfo 
				{ 
					CreatePotion = () => new LesserManaPotion(), 
					KegName = "mana menor", 
					KegHue = 0x54,
					UsesJar = false
				} 
			},
			{ PotionEffect.Mana, new PotionInfo 
				{ 
					CreatePotion = () => new ManaPotion(), 
					KegName = "mana", 
					KegHue = 0x54,
					UsesJar = false
				} 
			},
			{ PotionEffect.ManaGreater, new PotionInfo 
				{ 
					CreatePotion = () => new GreaterManaPotion(), 
					KegName = "mana maior", 
					KegHue = 0x54,
					UsesJar = false
				} 
			},
			
			// Special Potions
			{ PotionEffect.Invulnerability, new PotionInfo 
				{ 
					CreatePotion = () => new InvulnerabilityPotion(), 
					KegName = "invulnerabilidade", 
					KegHue = 0x496,
					UsesJar = false
				} 
			},
			
			// Elixirs - Skill Potions
			{ PotionEffect.ElixirAlchemy, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirAlchemy(), 
					KegName = "elixir de alquimia", 
					KegHue = 0x493,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirAnatomy, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirAnatomy(), 
					KegName = "elixir de anatomia", 
					KegHue = 0x492,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirAnimalLore, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirAnimalLore(), 
					KegName = "elixir de conhecimento animal", 
					KegHue = 0x491,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirAnimalTaming, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirAnimalTaming(), 
					KegName = "elixir de domar animais", 
					KegHue = 0x490,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirArchery, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirArchery(), 
					KegName = "elixir de arco e flecha", 
					KegHue = 0x48F,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirArmsLore, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirArmsLore(), 
					KegName = "elixir de conhecimento de armas", 
					KegHue = 0x48E,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirBegging, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirBegging(), 
					KegName = "elixir de mendicância", 
					KegHue = 0x48D,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirBlacksmith, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirBlacksmith(), 
					KegName = "elixir de ferraria", 
					KegHue = 0x48C,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirCamping, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirCamping(), 
					KegName = "elixir de acampamento", 
					KegHue = 0x482,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirCarpentry, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirCarpentry(), 
					KegName = "elixir de carpintaria", 
					KegHue = 0x47E,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirCartography, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirCartography(), 
					KegName = "elixir de cartografia", 
					KegHue = 0x40,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirCooking, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirCooking(), 
					KegName = "elixir de culinária", 
					KegHue = 0x46,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirDetectHidden, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirDetectHidden(), 
					KegName = "elixir de detecção", 
					KegHue = 0x50,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirDiscordance, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirDiscordance(), 
					KegName = "elixir de discordância", 
					KegHue = 0x55,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirEvalInt, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirEvalInt(), 
					KegName = "elixir de avaliação de inteligência", 
					KegHue = 0x5A,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirFencing, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirFencing(), 
					KegName = "elixir de esgrima", 
					KegHue = 0x5E,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirFishing, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirFishing(), 
					KegName = "elixir de pesca", 
					KegHue = 0x64,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirFletching, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirFletching(), 
					KegName = "elixir de fazer flechas", 
					KegHue = 0x69,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirFocus, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirFocus(), 
					KegName = "elixir de foco", 
					KegHue = 0x6E,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirForensics, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirForensics(), 
					KegName = "elixir de perícia forense", 
					KegHue = 0x74,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirHealing, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirHealing(), 
					KegName = "elixir do curandeiro", 
					KegHue = 0x78,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirHerding, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirHerding(), 
					KegName = "elixir de pastoreio", 
					KegHue = 0xB95,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirHiding, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirHiding(), 
					KegName = "elixir de esconder-se", 
					KegHue = 0x967,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirInscribe, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirInscribe(), 
					KegName = "elixir de inscrição", 
					KegHue = 0x970,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirItemID, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirItemID(), 
					KegName = "elixir de identificação de itens", 
					KegHue = 0x976,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirLockpicking, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirLockpicking(), 
					KegName = "elixir de arrombamento", 
					KegHue = 0x97B,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirLumberjacking, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirLumberjacking(), 
					KegName = "elixir de lenhador", 
					KegHue = 0x89C,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirMacing, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirMacing(), 
					KegName = "elixir de combate com maça", 
					KegHue = 0x8A1,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirMagicResist, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirMagicResist(), 
					KegName = "elixir de resistência mágica", 
					KegHue = 0x8A8,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirMeditation, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirMeditation(), 
					KegName = "elixir de meditação", 
					KegHue = 0x8AD,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirMining, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirMining(), 
					KegName = "elixir de mineração", 
					KegHue = 0x846,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirMusicianship, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirMusicianship(), 
					KegName = "elixir de musicalidade", 
					KegHue = 0x84C,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirParry, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirParry(), 
					KegName = "elixir de aparar", 
					KegHue = 0x852,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirPeacemaking, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirPeacemaking(), 
					KegName = "elixir de pacificação", 
					KegHue = 0x6DE,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirPoisoning, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirPoisoning(), 
					KegName = "elixir de envenenar", 
					KegHue = 0x9C4,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirProvocation, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirProvocation(), 
					KegName = "elixir de provocação", 
					KegHue = 0x6EE,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirRemoveTrap, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirRemoveTrap(), 
					KegName = "elixir de desarmar armadilhas", 
					KegHue = 0x5B1,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirSnooping, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirSnooping(), 
					KegName = "elixir de bisbilhotar", 
					KegHue = 0x5B2,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirSpiritSpeak, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirSpiritSpeak(), 
					KegName = "elixir de falar com espíritos", 
					KegHue = 0x5B3,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirStealing, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirStealing(), 
					KegName = "elixir de roubo", 
					KegHue = 0x5B4,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirStealth, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirStealth(), 
					KegName = "elixir de furtividade", 
					KegHue = 0x5B5,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirSwords, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirSwords(), 
					KegName = "elixir de combate com espadas", 
					KegHue = 0x5B6,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirTactics, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirTactics(), 
					KegName = "elixir de táticas", 
					KegHue = 0x5B7,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirTailoring, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirTailoring(), 
					KegName = "elixir de alfaiataria", 
					KegHue = 0x550,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirTasteID, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirTasteID(), 
					KegName = "elixir de identificação de sabor", 
					KegHue = 0x556,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirTinkering, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirTinkering(), 
					KegName = "elixir de engenharia", 
					KegHue = 0x55C,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirTracking, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirTracking(), 
					KegName = "elixir de rastreamento", 
					KegHue = 0x560,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirVeterinary, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirVeterinary(), 
					KegName = "elixir veterinário", 
					KegHue = 0x495,
					UsesJar = false
				} 
			},
			{ PotionEffect.ElixirWrestling, new PotionInfo 
				{ 
					CreatePotion = () => new ElixirWrestling(), 
					KegName = "elixir de luta livre", 
					KegHue = 0x494,
					UsesJar = false
				} 
			},
			
			// Mixtures (Use Jars)
			{ PotionEffect.MixtureSlime, new PotionInfo 
				{ 
					CreatePotion = () => new MixtureSlime(), 
					KegName = "mistura viscosa", 
					KegHue = 0x8AB,
					UsesJar = true
				} 
			},
			{ PotionEffect.MixtureIceSlime, new PotionInfo 
				{ 
					CreatePotion = () => new MixtureIceSlime(), 
					KegName = "mistura viscosa de gelo", 
					KegHue = 0x480,
					UsesJar = true
				} 
			},
			{ PotionEffect.MixtureFireSlime, new PotionInfo 
				{ 
					CreatePotion = () => new MixtureFireSlime(), 
					KegName = "mistura viscosa de fogo", 
					KegHue = 0x4EC,
					UsesJar = true
				} 
			},
			{ PotionEffect.MixtureDiseasedSlime, new PotionInfo 
				{ 
					CreatePotion = () => new MixtureDiseasedSlime(), 
					KegName = "mistura viscosa doente", 
					KegHue = 0x7D6,
					UsesJar = true
				} 
			},
			{ PotionEffect.MixtureRadiatedSlime, new PotionInfo 
				{ 
					CreatePotion = () => new MixtureRadiatedSlime(), 
					KegName = "mistura viscosa irradiada", 
					KegHue = 0xB96,
					UsesJar = true
				} 
			},
			
			// Liquids (Use Jars)
			{ PotionEffect.LiquidFire, new PotionInfo 
				{ 
					CreatePotion = () => new LiquidFire(), 
					KegName = "fogo líquido", 
					KegHue = 0x489,
					UsesJar = true
				} 
			},
			{ PotionEffect.LiquidGoo, new PotionInfo 
				{ 
					CreatePotion = () => new LiquidGoo(), 
					KegName = "gosma líquida", 
					KegHue = 0x490,
					UsesJar = true
				} 
			},
			{ PotionEffect.LiquidIce, new PotionInfo 
				{ 
					CreatePotion = () => new LiquidIce(), 
					KegName = "gelo líquido", 
					KegHue = 0x482,
					UsesJar = true
				} 
			},
			{ PotionEffect.LiquidRot, new PotionInfo 
				{ 
					CreatePotion = () => new LiquidRot(), 
					KegName = "podridão líquida", 
					KegHue = 0xB97,
					UsesJar = true
				} 
			},
			{ PotionEffect.LiquidPain, new PotionInfo 
				{ 
					CreatePotion = () => new LiquidPain(), 
					KegName = "dor líquida", 
					KegHue = 0x835,
					UsesJar = true
				} 
			},
			
			// Special Potions
			{ PotionEffect.Resurrect, new PotionInfo 
				{ 
					CreatePotion = () => new ResurrectPotion(), 
					KegName = "ressurreição", 
					KegHue = 0xB06,
					UsesJar = false
				} 
			},
			{ PotionEffect.SuperPotion, new PotionInfo 
				{ 
					CreatePotion = () => new SuperPotion(), 
					KegName = "superior", 
					KegHue = 0xBA4,
					UsesJar = false
				} 
			},
			{ PotionEffect.Repair, new PotionInfo 
				{ 
					CreatePotion = () => new RepairPotion(), 
					KegName = "reparo", 
					KegHue = 0xB7A,
					UsesJar = false
				} 
			},
			{ PotionEffect.Durability, new PotionInfo 
				{ 
					CreatePotion = () => new DurabilityPotion(), 
					KegName = "durabilidade", 
					KegHue = 0xB7D,
					UsesJar = false
				} 
			},
			{ PotionEffect.HairOil, new PotionInfo 
				{ 
					CreatePotion = () => new HairOilPotion(), 
					KegName = "estilo de cabelo", 
					KegHue = 0xB07,
					UsesJar = false
				} 
			},
			{ PotionEffect.HairDye, new PotionInfo 
				{ 
					CreatePotion = () => new HairDyePotion(), 
					KegName = "tintura de cabelo", 
					KegHue = 0xB04,
					UsesJar = false
				} 
			},
			
			// Frostbite Potions
			{ PotionEffect.Frostbite, new PotionInfo 
				{ 
					CreatePotion = () => new FrostbitePotion(), 
					KegName = "congelamento", 
					KegHue = 0xAF3,
					UsesJar = false
				} 
			},
			{ PotionEffect.FrostbiteGreater, new PotionInfo 
				{ 
					CreatePotion = () => new GreaterFrostbitePotion(), 
					KegName = "congelamento maior", 
					KegHue = 0xAF3,
					UsesJar = false
				} 
			}
		};
		
		#endregion
		
		#region Public API
		
		/// <summary>
		/// Gets potion metadata for specified effect
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		/// <returns>Potion metadata, or null if not found</returns>
		public static PotionInfo GetMetadata(PotionEffect effect)
		{
			PotionInfo info;
			return Registry.TryGetValue(effect, out info) ? info : null;
		}
		
		/// <summary>
		/// Creates a potion instance of the specified type
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		/// <returns>New potion instance, or null if type not found</returns>
		public static BasePotion CreatePotion(PotionEffect effect)
		{
			PotionInfo info = GetMetadata(effect);
			return info != null ? info.CreatePotion() : null;
		}
		
		/// <summary>
		/// Gets the keg name for a potion type
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		/// <returns>Keg name string (Portuguese-Brazilian)</returns>
		public static string GetKegName(PotionEffect effect)
		{
			PotionInfo info = GetMetadata(effect);
			return info != null ? info.KegName : null;
		}
		
		/// <summary>
		/// Gets the keg hue/color for a potion type
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		/// <returns>Hue value, or 0 if not found</returns>
		public static int GetKegHue(PotionEffect effect)
		{
			PotionInfo info = GetMetadata(effect);
			return info != null ? info.KegHue : 0;
		}
		
		/// <summary>
		/// Checks if potion type uses jars instead of bottles
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		/// <returns>True if uses jars, false otherwise</returns>
		public static bool UsesJar(PotionEffect effect)
		{
			PotionInfo info = GetMetadata(effect);
			return info != null && info.UsesJar;
		}
		
		#endregion
	}
}


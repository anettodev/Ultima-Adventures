using System;
using System.Collections.Generic;

namespace Server.Items
{
	/// <summary>
	/// Sci-fi themed names and properties for potions when converted to futuristic items.
	/// Used by BasePotion.MakePillBottle method to transform fantasy potions into sci-fi equivalents.
	/// This enables alternate theme gameplay where potions appear as pills, serums, or futuristic chemicals.
	/// </summary>
	public static class PotionSciFiNames
	{
		/// <summary>
		/// Represents a sci-fi theme configuration for a potion type
		/// </summary>
		public class SciFiTheme
		{
			/// <summary>Name when rendered as pills (bottle container)</summary>
			public string PillName { get; set; }

			/// <summary>Name when rendered as injectable serum (syringe container)</summary>
			public string SyringeName { get; set; }

			/// <summary>Special name for unique variants (fire/cold/liquid types)</summary>
			public string SpecialName { get; set; }

			/// <summary>Item ID override for special variants</summary>
			public int? SpecialItemID { get; set; }

			/// <summary>Hue override for special variants</summary>
			public int? SpecialHue { get; set; }

			/// <summary>Type of special variant (fire, cold, liquid)</summary>
			public SpecialType? Special { get; set; }
		}

		/// <summary>
		/// Types of special variants for themed potions
		/// </summary>
		public enum SpecialType
		{
			Fire,
			Cold,
			Liquid
		}

		#region Theme Registry

		/// <summary>
		/// Registry of all sci-fi themes mapped to potion effects
		/// </summary>
		private static readonly Dictionary<PotionEffect, SciFiTheme> Themes = 
			new Dictionary<PotionEffect, SciFiTheme>
		{
			// Vision and Perception
			{ PotionEffect.Nightsight, new SciFiTheme 
				{ PillName = "cornea dilation pills", SyringeName = "cornea dilation serum" } },

			// Antidotes and Cures
			{ PotionEffect.CureLesser, new SciFiTheme 
				{ PillName = "weak antidote pills", SyringeName = "weak antidote serum" } },
			{ PotionEffect.Cure, new SciFiTheme 
				{ PillName = "antidote pills", SyringeName = "antidote serum" } },
			{ PotionEffect.CureGreater, new SciFiTheme 
				{ PillName = "powerful antidote pills", SyringeName = "powerful antidote serum" } },

			// Agility and Speed Enhancers
			{ PotionEffect.Agility, new SciFiTheme 
				{ PillName = "amphetamine pills", SyringeName = "amphetamine serum" } },
			{ PotionEffect.AgilityGreater, new SciFiTheme 
				{ PillName = "powerful amphetamine pills", SyringeName = "powerful amphetamine serum" } },

			// Strength Enhancers
			{ PotionEffect.Strength, new SciFiTheme 
				{ PillName = "steroid pills", SyringeName = "steroid serum" } },
			{ PotionEffect.StrengthGreater, new SciFiTheme 
				{ PillName = "powerful steroid pills", SyringeName = "powerful steroid serum" } },

			// Poisons and Toxins
			{ PotionEffect.PoisonLesser, new SciFiTheme 
				{ PillName = "weak cyanide pills", SyringeName = "weak cyanide serum" } },
			{ PotionEffect.Poison, new SciFiTheme 
				{ PillName = "cyanide pills", SyringeName = "cyanide serum" } },
			{ PotionEffect.PoisonGreater, new SciFiTheme 
				{ PillName = "powerful cyanide pills", SyringeName = "powerful cyanide serum" } },
			{ PotionEffect.PoisonDeadly, new SciFiTheme 
				{ PillName = "deadly cyanide pills", SyringeName = "deadly cyanide serum" } },
			{ PotionEffect.PoisonLethal, new SciFiTheme 
				{ PillName = "lethal cyanide pills", SyringeName = "lethal cyanide serum" } },

			// Stamina and Energy
			{ PotionEffect.Refresh, new SciFiTheme 
				{ PillName = "caffeine pills", SyringeName = "thiamin serum" } },
			{ PotionEffect.RefreshTotal, new SciFiTheme 
				{ PillName = "powerful caffeine pills", SyringeName = "powerful thiamin serum" } },

			// Healing and Recovery
			{ PotionEffect.HealLesser, new SciFiTheme 
				{ PillName = "weak aspirin pills", SyringeName = "weak ketamine serum" } },
			{ PotionEffect.Heal, new SciFiTheme 
				{ PillName = "aspirin pills", SyringeName = "ketamine serum" } },
			{ PotionEffect.HealGreater, new SciFiTheme 
				{ PillName = "powerful aspirin pills", SyringeName = "powerfule ketamine serum" } }, // Note: typo "powerfule" matches original

			// Invisibility and Stealth
			{ PotionEffect.InvisibilityLesser, new SciFiTheme 
				{ PillName = "weak camouflage pills", SyringeName = "weak camouflage serum" } },
			{ PotionEffect.Invisibility, new SciFiTheme 
				{ PillName = "camouflage pills", SyringeName = "camouflage serum" } },
			{ PotionEffect.InvisibilityGreater, new SciFiTheme 
				{ PillName = "powerful camouflage pills", SyringeName = "powerful camouflage serum" } },

			// Rejuvenation and Enhancement
			{ PotionEffect.RejuvenateLesser, new SciFiTheme 
				{ PillName = "weak super soldier pills", SyringeName = "weak super soldier serum" } },
			{ PotionEffect.Rejuvenate, new SciFiTheme 
				{ PillName = "super soldier pills", SyringeName = "super soldier serum" } },
			{ PotionEffect.RejuvenateGreater, new SciFiTheme 
				{ PillName = "powerful super soldier pills", SyringeName = "powerful super soldier serum" } },

			// Mana and Psychoactive
			{ PotionEffect.ManaLesser, new SciFiTheme 
				{ PillName = "weak psychoactive pills", SyringeName = "weak psychoactive serum" } },
			{ PotionEffect.Mana, new SciFiTheme 
				{ PillName = "psychoactive pills", SyringeName = "psychoactive serum" } },
			{ PotionEffect.ManaGreater, new SciFiTheme 
				{ PillName = "powerful psychoactive pills", SyringeName = "powerfule psychoactive serum" } }, // Note: typo "powerfule" matches original

			// Defense and Protection
			{ PotionEffect.Invulnerability, new SciFiTheme 
				{ PillName = "phencyclidine pills", SyringeName = "phencyclidine dilation serum" } },

			// Fire-Based (Fuel Canisters)
			{ PotionEffect.Conflagration, new SciFiTheme 
				{ SpecialName = "gasoline", Special = SpecialType.Fire } },
			{ PotionEffect.ConflagrationGreater, new SciFiTheme 
				{ SpecialName = "diesel fuel", Special = SpecialType.Fire } },

			// Cold-Based (Fire Extinguishers)
			{ PotionEffect.Frostbite, new SciFiTheme 
				{ SpecialName = "fire extinguisher", Special = SpecialType.Cold, SpecialHue = PotionConstants.HUE_DEFAULT } },
			{ PotionEffect.FrostbiteGreater, new SciFiTheme 
				{ SpecialName = "halon extinguisher", Special = SpecialType.Cold, SpecialHue = PotionConstants.HUE_HALON } },

			// Explosives (Liquid Bottles)
			{ PotionEffect.ExplosionLesser, new SciFiTheme 
				{ SpecialName = "weak nitroglycerin", Special = SpecialType.Liquid } },
			{ PotionEffect.Explosion, new SciFiTheme 
				{ SpecialName = "nitroglycerin", Special = SpecialType.Liquid } },
			{ PotionEffect.ExplosionGreater, new SciFiTheme 
				{ SpecialName = "strong nitroglycerin", Special = SpecialType.Liquid } }
		};

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the sci-fi theme for a specific potion effect
		/// </summary>
		/// <param name="effect">The potion effect to get theme for</param>
		/// <returns>The sci-fi theme, or null if no theme exists for this effect</returns>
		public static SciFiTheme GetTheme(PotionEffect effect)
		{
			SciFiTheme theme;
			return Themes.TryGetValue(effect, out theme) ? theme : null;
		}

		/// <summary>
		/// Checks if a potion effect has a sci-fi theme defined
		/// </summary>
		/// <param name="effect">The potion effect to check</param>
		/// <returns>True if theme exists, false otherwise</returns>
		public static bool HasTheme(PotionEffect effect)
		{
			return Themes.ContainsKey(effect);
		}

		#endregion
	}
}


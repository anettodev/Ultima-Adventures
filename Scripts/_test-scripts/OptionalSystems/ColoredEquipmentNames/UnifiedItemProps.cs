using System;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace ItemNameHue
{
    public class UnifiedItemProps
    {
		/// <summary>
		/// Determines item name color based on property count and artifact rarity.
		/// Centralized system for all item types.
		/// </summary>
		public static string GetArmorItemValue(Item item)
		{
			int rarityValue = 0, rarityProps = 0;
			ItemTier.GetItemTier(item, out rarityValue, out rarityProps);

			// Artifact rarity system (enabled)
			bool useartifactrarity = true;
			
			if (useartifactrarity)
			{
				int artifactRarityValue = GetArtifactRarityValue(item);
				if (artifactRarityValue >= 1)
				{
					// Artifact rarity takes precedence
					if (artifactRarityValue >= 4)
						return "<BASEFONT COLOR=#FFFF00>"; // Yellow for ArtifactRarity <= 30
					else if (artifactRarityValue >= 3)
						return "<BASEFONT COLOR=#00FFFF>"; // Cyan for ArtifactRarity <= 20
					else if (artifactRarityValue >= 2)
						return "<BASEFONT COLOR=#00FF00>"; // Green for ArtifactRarity <= 10
					else if (artifactRarityValue >= 1)
						return "<BASEFONT COLOR=#FFFFFF>"; // White for ArtifactRarity <= 1
				}
			}

			// Property-based color system (new scheme)
			if (rarityProps >= 7)
				return "<BASEFONT COLOR=#DDA0DD>"; // Light purple for 7+ properties
			else if (rarityProps >= 4)
				return "<BASEFONT COLOR=#FFB6C1>"; // Light pink for 4-6 properties
			else if (rarityProps >= 1)
				return "<BASEFONT COLOR=#ADD8E6>"; // Light blue for 1-3 properties

			return "<BASEFONT COLOR=#D6D6D6>"; // Gray for 0 properties
		}

		/// <summary>
		/// Gets artifact rarity value for the item (0-4 scale).
		/// Lower ArtifactRarity values = higher rarity (1 is most rare).
		/// </summary>
		private static int GetArtifactRarityValue(Item item)
		{
			int artifactRarity = 0;

			if (item is BaseWeapon)
			{
				BaseWeapon bw = item as BaseWeapon;
				artifactRarity = bw.ArtifactRarity;
			}
			else if (item is BaseArmor)
			{
				BaseArmor ba = item as BaseArmor;
				artifactRarity = ba.ArtifactRarity;
			}
			else if (item is BaseClothing)
			{
				BaseClothing bc = item as BaseClothing;
				artifactRarity = bc.ArtifactRarity;
			}

			// Convert ArtifactRarity to 0-4 scale (lower rarity = higher value)
			if (artifactRarity < 1)
				return 0;
			else if (artifactRarity <= 1)
				return 1;
			else if (artifactRarity <= 10)
				return 2;
			else if (artifactRarity <= 20)
				return 3;
			else if (artifactRarity <= 30)
				return 4;
			else
				return 0; // ArtifactRarity > 30 is not considered artifact
		}

		public static string RarityNameMod(Item item, string orig)
		{
			return (string)(GetArmorItemValue(item) + orig + "<BASEFONT COLOR=#FFFFFF>");
		}

        public static string SetColor(string text, string color)
        {
            return String.Format("<BASEFONT COLOR={0}>{1}</BASEFONT>", color, text);
        }
    }
}

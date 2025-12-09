using System;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Centralized creature type definitions for Taming BOD contracts.
	/// Defines which creature types belong to each tier and their PT-BR names.
	/// </summary>
	public static class TamingBODCreatureTypes
	{
		#region Tier 2: Normal Wildlife/Domestic Animals

		/// <summary>
		/// Tier 2 creature types: Normal wildlife and domestic animals.
		/// </summary>
		public static readonly Type[] Tier2CreatureTypes = new Type[]
		{
			typeof(Rat),
			typeof(GiantRat),
			typeof(Cat),
			typeof(Dog),
			typeof(Cow),
			typeof(Bull),
			typeof(Hind),
			typeof(GreatHart),
			typeof(Rabbit),
			typeof(JackRabbit),
			typeof(Bird),
			typeof(Eagle),
			typeof(Chicken),
			typeof(Pig),
			typeof(Boar),
			typeof(Goat),
			typeof(MountainGoat),
			typeof(Sheep),
			typeof(Squirrel)
		};

		/// <summary>
		/// PT-BR names for Tier 2 creature types.
		/// </summary>
		public static readonly Dictionary<Type, string> Tier2CreatureNames = new Dictionary<Type, string>
		{
			{ typeof(Rat), "Ratos" },
			{ typeof(GiantRat), "Ratos Gigantes" },
			{ typeof(Cat), "Gatos" },
			{ typeof(Dog), "Cachorros" },
			{ typeof(Cow), "Vacas" },
			{ typeof(Bull), "Touros" },
			{ typeof(Hind), "Corças" },
			{ typeof(GreatHart), "Cervos Grandes" },
			{ typeof(Rabbit), "Coelhos" },
			{ typeof(JackRabbit), "Lebres" },
			{ typeof(Bird), "Pássaros" },
			{ typeof(Eagle), "Águias" },
			{ typeof(Chicken), "Galinhas" },
			{ typeof(Pig), "Porcos" },
			{ typeof(Boar), "Javalis" },
			{ typeof(Goat), "Cabras" },
			{ typeof(MountainGoat), "Cabras da Montanha" },
			{ typeof(Sheep), "Ovelhas" },
			{ typeof(Squirrel), "Esquilos" }
		};

		#endregion

		#region Tier 3: Common Riding/Mounts

		/// <summary>
		/// Tier 3 creature types: Common riding animals and mounts.
		/// </summary>
		public static readonly Type[] Tier3CreatureTypes = new Type[]
		{
			typeof(Horse),
			typeof(Llama),
			typeof(DesertOstard),
			typeof(ForestOstard),
			typeof(SnowOstard)
		};

		/// <summary>
		/// PT-BR names for Tier 3 creature types.
		/// </summary>
		public static readonly Dictionary<Type, string> Tier3CreatureNames = new Dictionary<Type, string>
		{
			{ typeof(Horse), "Cavalos" },
			{ typeof(Llama), "Lhamas" },
			{ typeof(DesertOstard), "Ostards do Deserto" },
			{ typeof(ForestOstard), "Ostards da Floresta" },
			{ typeof(SnowOstard), "Ostards da Neve" }
		};

		#endregion

		#region Tier 4: Dangerous Creatures

		/// <summary>
		/// Tier 4 creature types: Dangerous creatures like FrenziedOstard and Bears.
		/// </summary>
		public static readonly Type[] Tier4CreatureTypes = new Type[]
		{
			typeof(FrenziedOstard),
			typeof(BlackBear),
			typeof(BrownBear),
			typeof(GrizzlyBear),
			typeof(PolarBear)
		};

		/// <summary>
		/// PT-BR names for Tier 4 creature types.
		/// </summary>
		public static readonly Dictionary<Type, string> Tier4CreatureNames = new Dictionary<Type, string>
		{
			{ typeof(FrenziedOstard), "Ostards do Gelo" },
			{ typeof(BlackBear), "Ursos Negros" },
			{ typeof(BrownBear), "Ursos Marrons" },
			{ typeof(GrizzlyBear), "Ursos Pardos" },
			{ typeof(PolarBear), "Ursos Polares" }
		};

		#endregion

		#region Tier 5: Dragons and Drakes

		/// <summary>
		/// Tier 5 creature types: Dragons and Drakes.
		/// </summary>
		public static readonly Type[] Tier5CreatureTypes = new Type[]
		{
			//typeof(Dragons),
			typeof(WhiteWyrm),
			typeof(SerpentineDragon),
			typeof(AncientWyrm),
			typeof(ShadowWyrm),
			typeof(SkeletalDragon),
			typeof(Drake),
			typeof(Wyvern)
		};

		/// <summary>
		/// PT-BR names for Tier 5 creature types.
		/// </summary>
		public static readonly Dictionary<Type, string> Tier5CreatureNames = new Dictionary<Type, string>
		{
			//{ typeof(Dragons), "Dragões" },
			{ typeof(WhiteWyrm), "Vermes Brancos" },
			{ typeof(SerpentineDragon), "Dragões Serpentinos" },
			{ typeof(AncientWyrm), "Vermes Ancestrais" },
			{ typeof(ShadowWyrm), "Vermes das Sombras" },
			{ typeof(SkeletalDragon), "Dragões Esqueléticos" },
			{ typeof(Drake), "Dracos" },
			{ typeof(Wyvern), "Wyverns" }
		};

		#endregion

		#region Helper Methods

		/// <summary>
		/// Gets the creature type name in PT-BR for a given type and tier.
		/// </summary>
		/// <param name="creatureType">The creature type</param>
		/// <param name="tier">The contract tier</param>
		/// <returns>The PT-BR name, or null if not found</returns>
		public static string GetCreatureTypeName(Type creatureType, int tier)
		{
			if (creatureType == null)
				return null;

			switch (tier)
			{
				case 2:
					if (Tier2CreatureNames.ContainsKey(creatureType))
						return Tier2CreatureNames[creatureType];
					break;
				case 3:
					if (Tier3CreatureNames.ContainsKey(creatureType))
						return Tier3CreatureNames[creatureType];
					break;
				case 4:
					if (Tier4CreatureNames.ContainsKey(creatureType))
						return Tier4CreatureNames[creatureType];
					break;
				case 5:
					if (Tier5CreatureNames.ContainsKey(creatureType))
						return Tier5CreatureNames[creatureType];
					break;
			}

			return null;
		}

		/// <summary>
		/// Gets a random creature type for a given tier.
		/// </summary>
		/// <param name="tier">The contract tier (2-5)</param>
		/// <returns>A random creature type, or null for tier 1 (generic)</returns>
		public static Type GetRandomCreatureType(int tier)
		{
			switch (tier)
			{
				case 2:
					if (Tier2CreatureTypes.Length > 0)
						return Tier2CreatureTypes[Utility.Random(Tier2CreatureTypes.Length)];
					break;
				case 3:
					if (Tier3CreatureTypes.Length > 0)
						return Tier3CreatureTypes[Utility.Random(Tier3CreatureTypes.Length)];
					break;
				case 4:
					if (Tier4CreatureTypes.Length > 0)
						return Tier4CreatureTypes[Utility.Random(Tier4CreatureTypes.Length)];
					break;
				case 5:
					if (Tier5CreatureTypes.Length > 0)
						return Tier5CreatureTypes[Utility.Random(Tier5CreatureTypes.Length)];
					break;
			}

			return null; // Tier 1 is generic
		}

		/// <summary>
		/// Checks if a creature type is valid for a given tier.
		/// </summary>
		/// <param name="creatureType">The creature type to check</param>
		/// <param name="tier">The contract tier</param>
		/// <returns>True if valid, false otherwise</returns>
		public static bool IsValidCreatureType(Type creatureType, int tier)
		{
			if (creatureType == null)
				return tier == 1; // Generic contracts allow any type

			switch (tier)
			{
				case 1:
					return true; // Generic accepts any
				case 2:
					return Array.IndexOf(Tier2CreatureTypes, creatureType) >= 0;
				case 3:
					return Array.IndexOf(Tier3CreatureTypes, creatureType) >= 0;
				case 4:
					return Array.IndexOf(Tier4CreatureTypes, creatureType) >= 0;
				case 5:
					return Array.IndexOf(Tier5CreatureTypes, creatureType) >= 0;
				default:
					return false;
			}
		}

		#endregion
	}
}


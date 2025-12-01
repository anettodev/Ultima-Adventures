using System;
using System.Collections.Generic;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Metadata for a single reagent requirement
	/// </summary>
	public class ReagentInfo
	{
		public string Name { get; set; }
		public int Amount { get; set; }

		public ReagentInfo(string name, int amount)
		{
			Name = name;
			Amount = amount;
		}
	}

	/// <summary>
	/// Metadata for a single alchemy recipe
	/// </summary>
	public class AlchemyRecipeInfo
	{
		public int RecipeID { get; set; }
		public int Category { get; set; }
		public string Name { get; set; }
		public string CategoryName { get; set; }
		public double SkillMin { get; set; }
		public double SkillMax { get; set; }
		public int Hue { get; set; }
		public List<ReagentInfo> Reagents { get; set; }

		public AlchemyRecipeInfo()
		{
			Reagents = new List<ReagentInfo>();
		}
	}

	/// <summary>
	/// Central registry for all alchemy recipe metadata.
	/// Provides recipe lookup by ID, category, and full recipe list.
	/// </summary>
	public static class AlchemyRecipeData
	{
		private static List<AlchemyRecipeInfo> m_Recipes;
		private static Dictionary<int, List<AlchemyRecipeInfo>> m_RecipesByCategory;
		private static Dictionary<int, AlchemyRecipeInfo> m_RecipesByID;

		/// <summary>
		/// Gets all alchemy recipes
		/// </summary>
		public static List<AlchemyRecipeInfo> GetAllRecipes()
		{
			if (m_Recipes == null)
				InitializeRecipes();
			return m_Recipes;
		}

		/// <summary>
		/// Gets all recipes in a specific category
		/// </summary>
		public static List<AlchemyRecipeInfo> GetRecipesByCategory(int category)
		{
			if (m_RecipesByCategory == null)
				InitializeRecipes();

			if (m_RecipesByCategory.ContainsKey(category))
				return m_RecipesByCategory[category];

			return new List<AlchemyRecipeInfo>();
		}

		/// <summary>
		/// Gets a specific recipe by ID
		/// </summary>
		public static AlchemyRecipeInfo GetRecipeByID(int recipeID)
		{
			if (m_RecipesByID == null)
				InitializeRecipes();

			if (m_RecipesByID.ContainsKey(recipeID))
				return m_RecipesByID[recipeID];

			return null;
		}

		/// <summary>
		/// Gets a random recipe from a specific category
		/// </summary>
		public static AlchemyRecipeInfo GetRandomRecipeByCategory(int category)
		{
			List<AlchemyRecipeInfo> recipes = GetRecipesByCategory(category);
			if (recipes != null && recipes.Count > 0)
			{
				return recipes[Utility.Random(recipes.Count)];
			}
			return null;
		}

		/// <summary>
		/// Gets the category name for a category index
		/// </summary>
		public static string GetCategoryName(int category)
		{
			switch (category)
			{
				case AlchemyRecipeConstants.CATEGORY_BASIC: return AlchemyRecipeStringConstants.CATEGORY_BASIC;
				case AlchemyRecipeConstants.CATEGORY_ADVANCED: return AlchemyRecipeStringConstants.CATEGORY_ADVANCED;
				case AlchemyRecipeConstants.CATEGORY_SPECIAL: return AlchemyRecipeStringConstants.CATEGORY_SPECIAL;
				case AlchemyRecipeConstants.CATEGORY_COSMETIC: return AlchemyRecipeStringConstants.CATEGORY_COSMETIC;
				default: return "Desconhecido";
			}
		}

		/// <summary>
		/// Initializes all alchemy recipes organized by category
		/// </summary>
		private static void InitializeRecipes()
		{
			m_Recipes = new List<AlchemyRecipeInfo>();
			m_RecipesByCategory = new Dictionary<int, List<AlchemyRecipeInfo>>();
			m_RecipesByID = new Dictionary<int, AlchemyRecipeInfo>();

			// ========== CATEGORY 0: POÇÕES BÁSICAS ==========
			// Basic Potions: All Lesser and regular potions
			AddRecipe(500, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Agilidade", 35.0, 65.0, 396,
				R("Bloodmoss", 1), R("Garrafa Vazia", 1));

			AddRecipe(501, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Visão Noturna", 5.0, 30.0, 1109,
				R("Spider's Silk", 1), R("Garrafa Vazia", 1));

			AddRecipe(502, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Cura Menor", 10.0, 45.0, 46,
				R("Garlic", 2), R("Garrafa Vazia", 1));

			AddRecipe(503, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Vida Menor", 15.0, 45.0, 53,
				R("Ginseng", 2), R("Garrafa Vazia", 1));

			AddRecipe(504, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Explosão Menor", 15.0, 55.0, 0x204,
				R("Sulfurous Ash", 3), R("Garrafa Vazia", 1));

			AddRecipe(505, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Vigor", 10.0, 45.0, 0xEE,
				R("Black Pearl", 2), R("Garrafa Vazia", 1));

			AddRecipe(506, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Força", 25.0, 65.0, 1001,
				R("Mandrake Root", 2), R("Garrafa Vazia", 1));

			AddRecipe(507, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Cura", 30.0, 75.0, 46,
				R("Garlic", 3), R("Garrafa Vazia", 1));

			AddRecipe(509, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Vida", 35.0, 70.0, 53,
				R("Ginseng", 4), R("Garrafa Vazia", 1));

			AddRecipe(512, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Explosão", 40.0, 85.0, 0x204,
				R("Sulfurous Ash", 5), R("Garrafa Vazia", 1));

			AddRecipe(514, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Veneno Menor", 15.0, 50.0, 73,
				R("Nightshade", 1), R("Garrafa Vazia", 1));

			AddRecipe(515, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Veneno", 25.0, 70.0, 73,
				R("Nightshade", 2), R("Garrafa Vazia", 1));

			AddRecipe(521, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Mana Menor", 10.0, 45.0, 0x54,
				R("Black Pearl", 2), R("Garrafa Vazia", 1));

			AddRecipe(522, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Mana", 40.0, 80.0, 0x54,
				R("Black Pearl", 4), R("Garrafa Vazia", 1));

			AddRecipe(524, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Invisibilidade Menor", 20.0, 55.0, 0x4FE,
				R("Bloodmoss", 2), R("Garrafa Vazia", 1));

			AddRecipe(525, AlchemyRecipeConstants.CATEGORY_BASIC, "Poção de Invisibilidade", 50.0, 85.0, 0x4FE,
				R("Bloodmoss", 4), R("Garrafa Vazia", 1));

			// ========== CATEGORY 1: POÇÕES FORTES E AVANÇADAS ==========
			// Strong and Advanced Potions: All Greater potions
			AddRecipe(508, AlchemyRecipeConstants.CATEGORY_ADVANCED, "Poção de Cura Maior", 60.0, 90.0, 46,
				R("Garlic", 6), R("Garrafa Vazia", 1));

			AddRecipe(510, AlchemyRecipeConstants.CATEGORY_ADVANCED, "Poção de Vida Maior", 55.0, 95.0, 53,
				R("Ginseng", 7), R("Garrafa Vazia", 1));

			AddRecipe(511, AlchemyRecipeConstants.CATEGORY_ADVANCED, "Poção de Vigor Total", 45.0, 80.0, 0xEE,
				R("Black Pearl", 5), R("Garrafa Vazia", 1));

			AddRecipe(513, AlchemyRecipeConstants.CATEGORY_ADVANCED, "Poção de Explosão Maior", 65.0, 95.0, 0x204,
				R("Sulfurous Ash", 10), R("Garrafa Vazia", 1));

			AddRecipe(516, AlchemyRecipeConstants.CATEGORY_ADVANCED, "Poção de Veneno Maior", 40.0, 80.0, 73,
				R("Nightshade", 4), R("Nox Crystal", 1), R("Garrafa Vazia", 1));

			AddRecipe(517, AlchemyRecipeConstants.CATEGORY_ADVANCED, "Poção de Veneno Mortal", 65.0, 95.0, 73,
				R("Nightshade", 8), R("Nox Crystal", 2), R("Garrafa Vazia", 1));

			AddRecipe(518, AlchemyRecipeConstants.CATEGORY_ADVANCED, "Poção de Veneno Letal", 80.0, 110.0, 73,
				R("Nightshade", 12), R("Nox Crystal", 3), R("Garrafa Vazia", 1));

			AddRecipe(519, AlchemyRecipeConstants.CATEGORY_ADVANCED, "Poção de Agilidade Maior", 55.0, 85.0, 396,
				R("Bloodmoss", 3), R("Garrafa Vazia", 1));

			AddRecipe(520, AlchemyRecipeConstants.CATEGORY_ADVANCED, "Poção de Força Maior", 45.0, 90.0, 1001,
				R("Mandrake Root", 5), R("Garrafa Vazia", 1));

			AddRecipe(523, AlchemyRecipeConstants.CATEGORY_ADVANCED, "Poção de Mana Maior", 70.0, 100.0, 0x54,
				R("Black Pearl", 6), R("Garrafa Vazia", 1));

			AddRecipe(526, AlchemyRecipeConstants.CATEGORY_ADVANCED, "Poção de Invisibilidade Maior", 75.0, 105.0, 0x4FE,
				R("Bloodmoss", 6), R("Garrafa Vazia", 1));

			// ========== CATEGORY 2: POÇÕES ESPECIAIS ==========
			// Special Potions: Frostbite, Confusion, Conflagration
			AddRecipe(527, AlchemyRecipeConstants.CATEGORY_SPECIAL, "Poção de Congelamento", 70.0, 100.0, 0xAF3,
				R("Spider's Silk", 3), R("Ginseng", 2), R("Garrafa Vazia", 1));

			AddRecipe(528, AlchemyRecipeConstants.CATEGORY_SPECIAL, "Poção de Congelamento Maior", 90.0, 120.0, 0xAF3,
				R("Spider's Silk", 5), R("Ginseng", 4), R("Garrafa Vazia", 1));

			AddRecipe(529, AlchemyRecipeConstants.CATEGORY_SPECIAL, "Poção de Conflagração", 55.0, 90.0, 0xAD8,
				R("Sulfurous Ash", 6), R("Garrafa Vazia", 1));

			AddRecipe(530, AlchemyRecipeConstants.CATEGORY_SPECIAL, "Poção de Conflagração Maior", 75.0, 105.0, 0xAD8,
				R("Sulfurous Ash", 10), R("Garrafa Vazia", 1));

			AddRecipe(531, AlchemyRecipeConstants.CATEGORY_SPECIAL, "Poção de Confusão", 75.0, 105.0, 0x54F,
				R("Nightshade", 3), R("Black Pearl", 2), R("Garrafa Vazia", 1));

			AddRecipe(532, AlchemyRecipeConstants.CATEGORY_SPECIAL, "Poção de Confusão Maior", 95.0, 120.0, 0x54F,
				R("Nightshade", 5), R("Black Pearl", 4), R("Garrafa Vazia", 1));

			// ========== CATEGORY 3: COSMÉTICOS ==========
			// Cosmetic: Hair cut and tint
			AddRecipe(533, AlchemyRecipeConstants.CATEGORY_COSMETIC, "Poção de Corte de Cabelo", 80.0, 110.0, 0xB07,
				R("Pixie Skull", 2), R("Garrafa Vazia", 1));

			AddRecipe(534, AlchemyRecipeConstants.CATEGORY_COSMETIC, "Tinta de Cabelo", 80.0, 110.0, 0xB04,
				R("Fairy Egg", 3), R("Garrafa Vazia", 1));
		}

		/// <summary>
		/// Helper method to create a ReagentInfo
		/// </summary>
		private static ReagentInfo R(string name, int amount)
		{
			return new ReagentInfo(name, amount);
		}

		/// <summary>
		/// Adds a recipe to all internal collections
		/// </summary>
		private static void AddRecipe(int id, int category, string name, double skillMin, double skillMax, int hue, params ReagentInfo[] reagents)
		{
			AlchemyRecipeInfo recipe = new AlchemyRecipeInfo
			{
				RecipeID = id,
				Category = category,
				Name = name,
				CategoryName = GetCategoryName(category),
				SkillMin = skillMin,
				SkillMax = skillMax,
				Hue = hue,
				Reagents = new List<ReagentInfo>(reagents)
			};

			// Add to main list
			m_Recipes.Add(recipe);

			// Add to category dictionary
			if (!m_RecipesByCategory.ContainsKey(category))
				m_RecipesByCategory[category] = new List<AlchemyRecipeInfo>();
			m_RecipesByCategory[category].Add(recipe);

			// Add to ID dictionary
			m_RecipesByID[id] = recipe;
		}
	}
}


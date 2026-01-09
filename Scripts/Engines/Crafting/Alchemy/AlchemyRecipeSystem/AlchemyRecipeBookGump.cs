using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Gumps;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Main gump for Alchemy Recipe Book.
	/// Shows categories on left sidebar and learned recipes on right panel.
	/// Only displays learned recipes and categories with learned recipes.
	/// </summary>
	public class AlchemyRecipeBookGump : Gump
	{
		private PlayerMobile m_Player;
		private AlchemyRecipeBook m_Book;
		private int m_SelectedCategory;
		private int m_Page;

		public AlchemyRecipeBookGump(PlayerMobile pm, AlchemyRecipeBook book, int selectedCategory)
			: this(pm, book, selectedCategory, 0)
		{
		}

		public AlchemyRecipeBookGump(PlayerMobile pm, AlchemyRecipeBook book, int selectedCategory, int page)
			: base(AlchemyRecipeConstants.GUMP_X, AlchemyRecipeConstants.GUMP_Y)
		{
			m_Player = pm;
			m_Book = book;
			m_SelectedCategory = selectedCategory;
			m_Page = page;

			BuildGump();
		}

		private void BuildGump()
		{
			// Gump properties
			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			// Background - Book image
			AddPage(0);
			AddImage(0, 0, AlchemyRecipeConstants.GUMP_BACKGROUND_ID);

			// Title (moved 20px down, 30px left)
			AddHtml(-50, 30, AlchemyRecipeConstants.GUMP_WIDTH, 25,
				String.Format("<CENTER><BASEFONT COLOR={0}><BIG>{1}</BIG></BASEFONT></CENTER>",
					AlchemyRecipeStringConstants.COLOR_TITLE,
					AlchemyRecipeStringConstants.GUMP_TITLE),
				false, false);

			// Recipe counter (KNOW/TOTAL format)
			int totalLearned = GetTotalLearnedCount();
			int totalRecipes = AlchemyRecipeData.GetAllRecipes().Count;
			AddHtml(-50, 50, AlchemyRecipeConstants.GUMP_WIDTH, 20,
				String.Format("<CENTER><BASEFONT COLOR=#555555>{1} {2}/{3}</BASEFONT></CENTER>",
					AlchemyRecipeStringConstants.COLOR_INFO,
					AlchemyRecipeStringConstants.LABEL_RECIPES_KNOWN,
					totalLearned,
					totalRecipes),
				false, false);

			// Draw panels
			DrawCategoryPanel();
			DrawRecipePanel();
		}

		/// <summary>
		/// Draws the left sidebar with categories
		/// </summary>
		private void DrawCategoryPanel()
		{
			int x = AlchemyRecipeConstants.CATEGORY_PANEL_X;
			int y = AlchemyRecipeConstants.CATEGORY_PANEL_Y;

			// Header (black color)
			AddHtml(x, y, 200, 20,
				String.Format("<BASEFONT COLOR=#000000><B>{0}</B></BASEFONT>",
					AlchemyRecipeStringConstants.LABEL_CATEGORIES),
				false, false);

			y += 25;

			// Iterate through all categories (show all, even with 0 recipes)
			for (int i = 0; i < AlchemyRecipeConstants.CATEGORY_COUNT; i++)
			{
				int learnedInCategory = GetLearnedCountInCategory(i);
				bool selected = (i == m_SelectedCategory);

				// Selection indicator button (moved down)
				AddButton(x, y + 3,
					selected ? AlchemyRecipeConstants.IMAGE_CATEGORY_SELECTED : AlchemyRecipeConstants.IMAGE_CATEGORY_UNSELECTED,
					selected ? AlchemyRecipeConstants.IMAGE_CATEGORY_SELECTED : AlchemyRecipeConstants.IMAGE_CATEGORY_UNSELECTED,
					AlchemyRecipeConstants.BUTTON_CATEGORY_BASE + i,
					GumpButtonType.Reply, 0);

				// Category name with count (always show, even if 0)
				// All categories use black color
				string categoryName = AlchemyRecipeData.GetCategoryName(i);

				AddHtml(x + 22, y, 280, 20,
					String.Format("<BASEFONT COLOR=#000000>{0} ({1})</BASEFONT>",
						categoryName,
						learnedInCategory),
					false, false);

				y += AlchemyRecipeConstants.CATEGORY_SPACING;
			}
		}

		/// <summary>
		/// Draws the right panel with recipes in selected category
		/// </summary>
		private void DrawRecipePanel()
		{
			int x = AlchemyRecipeConstants.RECIPE_PANEL_X;
			int y = AlchemyRecipeConstants.RECIPE_PANEL_Y;

			// Header (black color)
			AddHtml(x, y, 220, 20,
				String.Format("<BASEFONT COLOR=#000000><B>{0}</B></BASEFONT>",
					AlchemyRecipeStringConstants.LABEL_RECIPES),
				false, false);

			y += 25;

			// Get learned recipes in selected category
			List<AlchemyRecipeInfo> recipes = GetLearnedRecipesInCategory(m_SelectedCategory);

			if (recipes.Count == 0)
			{
				AddHtml(x, y, 220, 20,
					String.Format("<BASEFONT COLOR={0}>{1}</BASEFONT>",
						AlchemyRecipeStringConstants.COLOR_DISABLED,
						AlchemyRecipeStringConstants.MSG_NO_RECIPES_IN_CATEGORY),
					false, false);
				return;
			}

			// Calculate pagination
			int totalPages = (recipes.Count + AlchemyRecipeConstants.RECIPES_PER_PAGE - 1) / AlchemyRecipeConstants.RECIPES_PER_PAGE;
			int startIndex = m_Page * AlchemyRecipeConstants.RECIPES_PER_PAGE;
			int endIndex = Math.Min(startIndex + AlchemyRecipeConstants.RECIPES_PER_PAGE, recipes.Count);

			// Draw navigation buttons in top right (only if more than 1 page)
			if (totalPages > 1)
			{
				int navX = x + 200; // Right side of recipe panel (moved +10px)
				int navY = AlchemyRecipeConstants.RECIPE_PANEL_Y; // Top of recipe panel

				// Previous button (left side)
				if (m_Page > 0)
				{
					AddButton(navX, navY, AlchemyRecipeConstants.IMAGE_PREV_BUTTON, AlchemyRecipeConstants.IMAGE_PREV_BUTTON_PRESSED,
						AlchemyRecipeConstants.BUTTON_PREV, GumpButtonType.Reply, 0);
				}
				else
				{
					// Disabled state (grayed out)
					AddImage(navX, navY, AlchemyRecipeConstants.IMAGE_PREV_BUTTON, 0x3E9);
				}

				// Next button (right side)
				if (m_Page < totalPages - 1)
				{
					AddButton(navX + 30, navY, AlchemyRecipeConstants.IMAGE_NEXT_BUTTON, AlchemyRecipeConstants.IMAGE_NEXT_BUTTON_PRESSED,
						AlchemyRecipeConstants.BUTTON_NEXT, GumpButtonType.Reply, 0);
				}
				else
				{
					// Disabled state (grayed out)
					AddImage(navX + 30, navY, AlchemyRecipeConstants.IMAGE_NEXT_BUTTON, 0x3E9);
				}
			}

			// Show recipes for current page
			for (int i = startIndex; i < endIndex; i++)
			{
				AlchemyRecipeInfo recipe = recipes[i];

				// Potion icon
				AddItem(x, y - 3, AlchemyRecipeConstants.POTION_ICON_ITEM_ID, recipe.Hue);

				// Clickable button to view details
				AddButton(x + 30, y + 1,
					AlchemyRecipeConstants.IMAGE_RECIPE_BUTTON,
					AlchemyRecipeConstants.IMAGE_RECIPE_BUTTON_PRESSED,
					AlchemyRecipeConstants.BUTTON_RECIPE_BASE + recipe.RecipeID,
					GumpButtonType.Reply, 0);

				// Recipe name
				AddHtml(x + 55, y, 200, 20,
					String.Format("<BASEFONT COLOR={0}>{1}</BASEFONT>",
						AlchemyRecipeStringConstants.COLOR_RECIPE_NAME,
						recipe.Name),
					false, false);

				y += AlchemyRecipeConstants.RECIPE_SPACING;
			}
		}

		/// <summary>
		/// Gets total count of learned recipes across all categories
		/// </summary>
		private int GetTotalLearnedCount()
		{
			int count = 0;
			foreach (AlchemyRecipeInfo recipe in AlchemyRecipeData.GetAllRecipes())
			{
				if (m_Player.HasRecipe(recipe.RecipeID))
					count++;
			}
			return count;
		}

		/// <summary>
		/// Gets count of learned recipes in a specific category
		/// </summary>
		private int GetLearnedCountInCategory(int category)
		{
			int count = 0;
			foreach (AlchemyRecipeInfo recipe in AlchemyRecipeData.GetRecipesByCategory(category))
			{
				if (m_Player.HasRecipe(recipe.RecipeID))
					count++;
			}
			return count;
		}

		/// <summary>
		/// Gets list of learned recipes in a specific category
		/// </summary>
		private List<AlchemyRecipeInfo> GetLearnedRecipesInCategory(int category)
		{
			List<AlchemyRecipeInfo> learned = new List<AlchemyRecipeInfo>();

			foreach (AlchemyRecipeInfo recipe in AlchemyRecipeData.GetRecipesByCategory(category))
			{
				if (m_Player.HasRecipe(recipe.RecipeID))
					learned.Add(recipe);
			}

			return learned;
		}

		/// <summary>
		/// Gets the unique color for a category
		/// </summary>
		private string GetCategoryColor(int category)
		{
			switch (category)
			{
				case AlchemyRecipeConstants.CATEGORY_BASIC:
					return AlchemyRecipeStringConstants.COLOR_CATEGORY_BASIC;
				case AlchemyRecipeConstants.CATEGORY_ADVANCED:
					return AlchemyRecipeStringConstants.COLOR_CATEGORY_ADVANCED;
				case AlchemyRecipeConstants.CATEGORY_SPECIAL:
					return AlchemyRecipeStringConstants.COLOR_CATEGORY_SPECIAL;
				case AlchemyRecipeConstants.CATEGORY_COSMETIC:
					return AlchemyRecipeStringConstants.COLOR_CATEGORY_COSMETIC;
				default:
					return AlchemyRecipeStringConstants.COLOR_CATEGORY_UNSELECTED;
			}
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			if (m_Player == null || m_Book == null || m_Book.Deleted)
				return;

			if (info.ButtonID == 0) // Close
				return;

			// Pagination buttons
			if (info.ButtonID == AlchemyRecipeConstants.BUTTON_NEXT)
			{
				m_Player.SendGump(new AlchemyRecipeBookGump(m_Player, m_Book, m_SelectedCategory, m_Page + 1));
				return;
			}
			else if (info.ButtonID == AlchemyRecipeConstants.BUTTON_PREV)
			{
				m_Player.SendGump(new AlchemyRecipeBookGump(m_Player, m_Book, m_SelectedCategory, m_Page - 1));
				return;
			}

			// Category selection (100-107)
			if (info.ButtonID >= AlchemyRecipeConstants.BUTTON_CATEGORY_BASE &&
				info.ButtonID < AlchemyRecipeConstants.BUTTON_CATEGORY_BASE + AlchemyRecipeConstants.CATEGORY_COUNT)
			{
				int newCategory = info.ButtonID - AlchemyRecipeConstants.BUTTON_CATEGORY_BASE;
				m_Player.SendGump(new AlchemyRecipeBookGump(m_Player, m_Book, newCategory, 0)); // Reset to page 0 when changing category
			}
			// Recipe clicked (200+)
			else if (info.ButtonID >= AlchemyRecipeConstants.BUTTON_RECIPE_BASE)
			{
				int recipeID = info.ButtonID - AlchemyRecipeConstants.BUTTON_RECIPE_BASE;
				AlchemyRecipeInfo recipe = AlchemyRecipeData.GetRecipeByID(recipeID);

				if (recipe != null && m_Player.HasRecipe(recipeID))
				{
					m_Player.CloseGump(typeof(AlchemyRecipeDetailGump));
					m_Player.SendGump(new AlchemyRecipeDetailGump(m_Player, m_Book, recipe, m_SelectedCategory, m_Page));
				}
			}
		}
	}
}


using System;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Gumps;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Detail gump for individual alchemy recipe.
	/// Shows recipe name, skill requirements, reagents, and category.
	/// Includes back button to return to main recipe list.
	/// </summary>
	public class AlchemyRecipeDetailGump : Gump
	{
		private PlayerMobile m_Player;
		private AlchemyRecipeBook m_Book;
		private AlchemyRecipeInfo m_Recipe;
		private int m_ReturnCategory;
		private int m_ReturnPage;

		public AlchemyRecipeDetailGump(PlayerMobile pm, AlchemyRecipeBook book, AlchemyRecipeInfo recipe, int returnCategory)
			: this(pm, book, recipe, returnCategory, 0)
		{
		}

		public AlchemyRecipeDetailGump(PlayerMobile pm, AlchemyRecipeBook book, AlchemyRecipeInfo recipe, int returnCategory, int returnPage)
			: base(AlchemyRecipeConstants.GUMP_X, AlchemyRecipeConstants.GUMP_Y)
		{
			m_Player = pm;
			m_Book = book;
			m_Recipe = recipe;
			m_ReturnCategory = returnCategory;
			m_ReturnPage = returnPage;

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

			// Title (centered)
			AddHtml(-60, 30, AlchemyRecipeConstants.GUMP_WIDTH, 25,
				String.Format("<CENTER><BASEFONT COLOR={0}><BIG>{1}</BIG></BASEFONT></CENTER>",
					AlchemyRecipeStringConstants.COLOR_TITLE,
					AlchemyRecipeStringConstants.GUMP_TITLE),
				false, false);

			// Large potion icon (moved 60px left)
			AddItem(AlchemyRecipeConstants.GUMP_WIDTH / 2 - 80, 70, AlchemyRecipeConstants.POTION_ICON_ITEM_ID, m_Recipe.Hue);

			// Recipe name (centered)
			AddHtml(-60, 130, AlchemyRecipeConstants.GUMP_WIDTH, 25,
				String.Format("<CENTER><BASEFONT COLOR={0}><BIG>{1}</BIG></BASEFONT></CENTER>",
					AlchemyRecipeStringConstants.COLOR_RECIPE_NAME,
					m_Recipe.Name),
				false, false);

			int y = 170;
			int leftMargin = 100; // Moved 20px left (was 120)

			// Skill requirement (dark grey, almost black)
			AddHtml(leftMargin, y, 220, 20,
				String.Format("<BASEFONT COLOR=#111111><B>{0}</B></BASEFONT>",
					AlchemyRecipeStringConstants.LABEL_SKILL_REQUIRED),
				false, false);

			y += 22;

			AddHtml(leftMargin, y, 220, 20,
				String.Format("<BASEFONT COLOR=#000000>Alchemy: {1:F1} - {2:F1}</BASEFONT>",
					AlchemyRecipeStringConstants.COLOR_INFO,
					m_Recipe.SkillMin,
					m_Recipe.SkillMax),
				false, false);

			y += 30;

			// Reagents (black color)
			AddHtml(leftMargin, y, 220, 20,
				String.Format("<BASEFONT COLOR=#111111><B>{0}</B></BASEFONT>",
					AlchemyRecipeStringConstants.LABEL_REAGENTS_REQUIRED),
				false, false);

			y += 22;

			foreach (ReagentInfo reagent in m_Recipe.Reagents)
			{
				AddHtml(leftMargin + 20, y, 200, 20,
					String.Format("<BASEFONT COLOR=#000000>• {1}x {2}</BASEFONT>",
						AlchemyRecipeStringConstants.COLOR_INFO,
						reagent.Amount,
						reagent.Name),
					false, false);
				y += 20;
			}

			y += 15;

			// Category (black color)
			AddHtml(leftMargin, y, 280, 20,
				String.Format("<BASEFONT COLOR=#000000><B>{0}</B> {1}</BASEFONT>",
					AlchemyRecipeStringConstants.LABEL_CATEGORY,
					m_Recipe.CategoryName),
				false, false);

			// Back button (same Y as potion icon, black color)
			AddButton(AlchemyRecipeConstants.GUMP_WIDTH / 2 - 180, 70, 0x15E3, 0x15E7, AlchemyRecipeConstants.BUTTON_BACK, GumpButtonType.Reply, 0);
			AddHtml(AlchemyRecipeConstants.GUMP_WIDTH / 2 - 150, 70, 100, 20,
				String.Format("<BASEFONT COLOR=#000000>{0}</BASEFONT>",
					AlchemyRecipeStringConstants.LABEL_BACK),
				false, false);
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			if (m_Player == null || m_Book == null || m_Book.Deleted)
				return;

			if (info.ButtonID == AlchemyRecipeConstants.BUTTON_BACK)
			{
				// Return to main recipe list (preserve page)
				m_Player.CloseGump(typeof(AlchemyRecipeBookGump));
				m_Player.SendGump(new AlchemyRecipeBookGump(m_Player, m_Book, m_ReturnCategory, m_ReturnPage));
			}
		}
	}
}


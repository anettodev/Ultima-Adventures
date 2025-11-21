using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for Cooking crafting system.
	/// Extracted from DefCooking.cs to improve maintainability.
	/// </summary>
	public static class CookingConstants
	{
		#region Gump and Menu IDs

		/// <summary>Gump title: "COOKING MENU"</summary>
		public const int GUMP_TITLE_COOKING_MENU = 1044003;

		#endregion

		#region Crafting System Parameters

		/// <summary>Minimum chance multiplier for crafting success</summary>
		public const int MIN_CHANCE_MULTIPLIER = 1;

		/// <summary>Maximum chance multiplier for crafting success</summary>
		public const int MAX_CHANCE_MULTIPLIER = 1;

		/// <summary>Delay multiplier for crafting actions</summary>
		public const double DELAY_MULTIPLIER = 1.25;

		/// <summary>Minimum success chance at minimum skill (0%)</summary>
		public const double MIN_SUCCESS_CHANCE = 0.0;

		#endregion

		#region Skill Requirements

		// Ingredients
		public const double SACK_FLOUR_MIN_SKILL = 0.0;
		public const double SACK_FLOUR_MAX_SKILL = 40.0;
		public const double DOUGH_MIN_SKILL = 20.0;
		public const double DOUGH_MAX_SKILL = 50.0;
		public const double SWEET_DOUGH_MIN_SKILL = 30.0;
		public const double SWEET_DOUGH_MAX_SKILL = 60.0;
		public const double CAKE_MIX_MIN_SKILL = 40.0;
		public const double CAKE_MIX_MAX_SKILL = 70.0;
		public const double COOKIE_MIX_MIN_SKILL = 40.0;
		public const double COOKIE_MIX_MAX_SKILL = 70.0;

		// Preparations
		public const double FRUIT_BASKET_MIN_SKILL = 40.0;
		public const double FRUIT_BASKET_MAX_SKILL = 85.0;
		public const double UNBAKED_QUICHE_MIN_SKILL = 30.0;
		public const double UNBAKED_QUICHE_MAX_SKILL = 65.0;
		public const double UNBAKED_MEAT_PIE_MIN_SKILL = 30.0;
		public const double UNBAKED_MEAT_PIE_MAX_SKILL = 60.0;
		public const double UNCOOKED_SAUSAGE_PIZZA_MIN_SKILL = 40.0;
		public const double UNCOOKED_SAUSAGE_PIZZA_MAX_SKILL = 60.0;
		public const double UNCOOKED_CHEESE_PIZZA_MIN_SKILL = 30.0;
		public const double UNCOOKED_CHEESE_PIZZA_MAX_SKILL = 65.0;
		public const double UNBAKED_FRUIT_PIE_MIN_SKILL = 40.0;
		public const double UNBAKED_FRUIT_PIE_MAX_SKILL = 85.0;
		public const double UNBAKED_PEACH_COBBLER_MIN_SKILL = 40.0;
		public const double UNBAKED_PEACH_COBBLER_MAX_SKILL = 85.0;
		public const double UNBAKED_APPLE_PIE_MIN_SKILL = 40.0;
		public const double UNBAKED_APPLE_PIE_MAX_SKILL = 85.0;
		public const double UNBAKED_PUMPKIN_PIE_MIN_SKILL = 40.0;
		public const double UNBAKED_PUMPKIN_PIE_MAX_SKILL = 85.0;
		public const double GREEN_TEA_MIN_SKILL = 70.0;
		public const double GREEN_TEA_MAX_SKILL = 100.0;
		public const double WASABI_CLUMPS_MIN_SKILL = 70.0;
		public const double WASABI_CLUMPS_MAX_SKILL = 100.0;
		public const double SUSHI_ROLLS_MIN_SKILL = 50.0;
		public const double SUSHI_ROLLS_MAX_SKILL = 90.0;
		public const double SUSHI_PLATTER_MIN_SKILL = 50.0;
		public const double SUSHI_PLATTER_MAX_SKILL = 90.0;
		public const double TRIBAL_PAINT_MIN_SKILL = 55.0;
		public const double TRIBAL_PAINT_MAX_SKILL = 95.0;
		public const double TRIBAL_PAINT_MIN_SKILL_PRE_ML = 70.0;
		public const double TRIBAL_PAINT_MAX_SKILL_PRE_ML = 90.0;
		public const double EGG_BOMB_MIN_SKILL = 75.0;
		public const double EGG_BOMB_MAX_SKILL = 100.0;

		// Baking
		public const double BREAD_LOAF_MIN_SKILL = 30.0;
		public const double BREAD_LOAF_MAX_SKILL = 80.0;
		public const double CHEESE_BREAD_MIN_SKILL = 40.0;
		public const double CHEESE_BREAD_MAX_SKILL = 90.0;
		public const double COOKIES_MIN_SKILL = 70.0;
		public const double COOKIES_MAX_SKILL = 100.0;
		public const double CAKE_MIN_SKILL = 80.0;
		public const double CAKE_MAX_SKILL = 120.0;
		public const double MUFFINS_MIN_SKILL = 60.0;
		public const double MUFFINS_MAX_SKILL = 110.0;
		public const double QUICHE_MIN_SKILL = 60.0;
		public const double QUICHE_MAX_SKILL = 100.0;
		public const double MEAT_PIE_MIN_SKILL = 60.0;
		public const double MEAT_PIE_MAX_SKILL = 110.0;
		public const double SAUSAGE_PIZZA_MIN_SKILL = 80.0;
		public const double SAUSAGE_PIZZA_MAX_SKILL = 115.0;
		public const double CHEESE_PIZZA_MIN_SKILL = 70.0;
		public const double CHEESE_PIZZA_MAX_SKILL = 100.0;
		public const double FRUIT_PIE_MIN_SKILL = 80.0;
		public const double FRUIT_PIE_MAX_SKILL = 100.0;
		public const double PEACH_COBBLER_MIN_SKILL = 80.0;
		public const double PEACH_COBBLER_MAX_SKILL = 100.0;
		public const double APPLE_PIE_MIN_SKILL = 70.0;
		public const double APPLE_PIE_MAX_SKILL = 100.0;
		public const double PUMPKIN_PIE_MIN_SKILL = 60.0;
		public const double PUMPKIN_PIE_MAX_SKILL = 100.0;
		public const double MISO_SOUP_MIN_SKILL = 40.0;
		public const double MISO_SOUP_MAX_SKILL = 70.0;

		// Barbecue
		public const double COOKED_BIRD_MIN_SKILL = 20.0;
		public const double COOKED_BIRD_MAX_SKILL = 70.0;
		public const double CHICKEN_LEG_MIN_SKILL = 20.0;
		public const double CHICKEN_LEG_MAX_SKILL = 70.0;
		public const double FISH_STEAK_MIN_SKILL = 10.0;
		public const double FISH_STEAK_MAX_SKILL = 70.0;
		public const double FRIED_EGGS_MIN_SKILL = 10.0;
		public const double FRIED_EGGS_MAX_SKILL = 70.0;
		public const double LAMB_LEG_MIN_SKILL = 30.0;
		public const double LAMB_LEG_MAX_SKILL = 70.0;
		public const double RIBS_MIN_SKILL = 40.0;
		public const double RIBS_MAX_SKILL = 80.0;

		// Rations
		public const double RATION_MIN_SKILL = 70.0;
		public const double RATION_MAX_SKILL = 100.0;

		#endregion

		#region Resource Quantities

		public const int WHEAT_SHEAF_QUANTITY = 5;
		public const int PEAR_QUANTITY = 2;
		public const int PEACH_QUANTITY = 2;
		public const int APPLE_QUANTITY = 2;
		public const int PUMPKIN_QUANTITY = 2;
		public const int TOMATO_QUANTITY = 2;
		public const int RAW_FISH_STEAK_SUSHI_ROLLS = 6;
		public const int RAW_FISH_STEAK_SUSHI_PLATTER = 10;
		public const int WOODEN_BOWL_OF_PEAS_QUANTITY = 3;
		public const int TRIBAL_BERRY_QUANTITY = 3;
		public const int SACK_FLOUR_EGG_BOMB = 3;
		public const int BEESWAX_RUNEBOOK = 8;
		public const int BLANK_SCROLL_RUNEBOOK = 16;

		#endregion
	}
}

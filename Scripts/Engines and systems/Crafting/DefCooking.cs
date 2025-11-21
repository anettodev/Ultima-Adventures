using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefCooking : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Cooking;	}
		}

		public override int GumpTitleNumber
		{
			get { return CookingConstants.GUMP_TITLE_COOKING_MENU; } // <CENTER>COOKING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefCooking();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return CookingConstants.MIN_SUCCESS_CHANCE; // 0%
		}

		private DefCooking() : base( CookingConstants.MIN_CHANCE_MULTIPLIER, CookingConstants.MAX_CHANCE_MULTIPLIER, CookingConstants.DELAY_MULTIPLIER )// base( 1, 1, 1.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				if ( quality == 0 )
					return 502785; // You were barely able to make this item.  It's quality is below average.
				else if ( makersMark && quality == 2 )
					return 1044156; // You create an exceptional quality item and affix your maker's mark.
				else if ( quality == 2 )
					return 1044155; // You create an exceptional quality item.
				else
					return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			/* Begin Ingredients */
			index = AddCraft( typeof( SackFlour ), 1044495, 1024153, CookingConstants.SACK_FLOUR_MIN_SKILL, CookingConstants.SACK_FLOUR_MAX_SKILL, typeof( WheatSheaf ), 1044489, CookingConstants.WHEAT_SHEAF_QUANTITY, 1044490 );
			SetNeedMill( index, true );

			index = AddCraft( typeof( Dough ), 1044495, 1024157, CookingConstants.DOUGH_MIN_SKILL, CookingConstants.DOUGH_MAX_SKILL, typeof( SackFlour ), 1044468, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );

			index = AddCraft( typeof( SweetDough ), 1044495, 1041340, CookingConstants.SWEET_DOUGH_MIN_SKILL, CookingConstants.SWEET_DOUGH_MAX_SKILL, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( JarHoney ), 1044472, 1, 1044253 );

			index = AddCraft( typeof( CakeMix ), 1044495, 1041002, CookingConstants.CAKE_MIX_MIN_SKILL, CookingConstants.CAKE_MIX_MAX_SKILL, typeof( SackFlour ), 1044468, 1, 1044253 );
			AddRes( index, typeof( SweetDough ), 1044475, 1, 1044253 );

			index = AddCraft( typeof( CookieMix ), 1044495, 1024159, CookingConstants.COOKIE_MIX_MIN_SKILL, CookingConstants.COOKIE_MIX_MAX_SKILL, typeof( JarHoney ), 1044472, 1, 1044253 );
			AddRes( index, typeof( SweetDough ), 1044475, 1, 1044253 );
            /* End Ingredients */

            /* Begin Preparations */
            index = AddCraft(typeof(FruitBasket), 1044496, CookingStringConstants.ITEM_FRUIT_BASKET, CookingConstants.FRUIT_BASKET_MIN_SKILL, CookingConstants.FRUIT_BASKET_MAX_SKILL, typeof(Basket), CookingStringConstants.RESOURCE_BASKET, 1, 1044253);
            AddRes(index, typeof(Pear), 1044481, CookingConstants.PEAR_QUANTITY, 1044253);
            AddRes(index, typeof(Peach), 1044480, CookingConstants.PEACH_QUANTITY, 1044253);
            AddRes(index, typeof(Apple), 1044479, CookingConstants.APPLE_QUANTITY, 1044253);

            index = AddCraft( typeof( UnbakedQuiche ), 1044496, 1041339, CookingConstants.UNBAKED_QUICHE_MIN_SKILL, CookingConstants.UNBAKED_QUICHE_MAX_SKILL, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Eggs ), 1044477, 1, 1044253 );
			AddRes( index, typeof( Carrot ), CookingStringConstants.RESOURCE_CARROT, 1, 1044253 );

			// TODO: This must also support chicken and lamb legs
			index = AddCraft( typeof( UnbakedMeatPie ), 1044496, 1041338, CookingConstants.UNBAKED_MEAT_PIE_MIN_SKILL, CookingConstants.UNBAKED_MEAT_PIE_MAX_SKILL, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( RawRibs ), 1044482, 1, 1044253 );
			AddRes( index, typeof( FoodPotato ), CookingStringConstants.RESOURCE_POTATO, 1, 1044253 );

			index = AddCraft( typeof( UncookedSausagePizza ), 1044496, 1041337, CookingConstants.UNCOOKED_SAUSAGE_PIZZA_MIN_SKILL, CookingConstants.UNCOOKED_SAUSAGE_PIZZA_MAX_SKILL, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Sausage ), 1044483, 1, 1044253 );
			AddRes( index, typeof( Tomato ), CookingStringConstants.RESOURCE_TOMATO, CookingConstants.TOMATO_QUANTITY, 1044253 );
			AddRes( index, typeof( CheeseWheel ), 1044486, 1, 1044253 );

			index = AddCraft( typeof( UncookedCheesePizza ), 1044496, 1041341, CookingConstants.UNCOOKED_CHEESE_PIZZA_MIN_SKILL, CookingConstants.UNCOOKED_CHEESE_PIZZA_MAX_SKILL, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( CheeseWheel ), 1044486, 1, 1044253 );
			AddRes( index, typeof( Tomato ), CookingStringConstants.RESOURCE_TOMATO, CookingConstants.TOMATO_QUANTITY, 1044253 );

			index = AddCraft( typeof( UnbakedFruitPie ), 1044496, 1041334, CookingConstants.UNBAKED_FRUIT_PIE_MIN_SKILL, CookingConstants.UNBAKED_FRUIT_PIE_MAX_SKILL, typeof( SweetDough ), 1044475, 1, 1044253 );
			AddRes( index, typeof( Pear ), 1044481, CookingConstants.PEAR_QUANTITY, 1044253 );

			index = AddCraft( typeof( UnbakedPeachCobbler ), 1044496, 1041335, CookingConstants.UNBAKED_PEACH_COBBLER_MIN_SKILL, CookingConstants.UNBAKED_PEACH_COBBLER_MAX_SKILL, typeof( SweetDough ), 1044475, 1, 1044253 );
			AddRes( index, typeof( Peach ), 1044480, CookingConstants.PEACH_QUANTITY, 1044253 );

			index = AddCraft( typeof( UnbakedApplePie ), 1044496, 1041336, CookingConstants.UNBAKED_APPLE_PIE_MIN_SKILL, CookingConstants.UNBAKED_APPLE_PIE_MAX_SKILL, typeof( SweetDough ), 1044475, 1, 1044253 );
			AddRes( index, typeof( Apple ), 1044479, CookingConstants.APPLE_QUANTITY, 1044253 );

			index = AddCraft( typeof( UnbakedPumpkinPie ), 1044496, 1041342, CookingConstants.UNBAKED_PUMPKIN_PIE_MIN_SKILL, CookingConstants.UNBAKED_PUMPKIN_PIE_MAX_SKILL, typeof( SweetDough ), 1044475, 1, 1044253 );
			AddRes( index, typeof( Pumpkin ), 1044484, CookingConstants.PUMPKIN_QUANTITY, 1044253 );

			index = AddCraft( typeof( GreenTea ), 1044496, 1030315, CookingConstants.GREEN_TEA_MIN_SKILL, CookingConstants.GREEN_TEA_MAX_SKILL, typeof( GreenTeaBasket ), 1030316, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( WasabiClumps ), 1044496, 1029451, CookingConstants.WASABI_CLUMPS_MIN_SKILL, CookingConstants.WASABI_CLUMPS_MAX_SKILL, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( WoodenBowlOfPeas ), 1025633, CookingConstants.WOODEN_BOWL_OF_PEAS_QUANTITY, 1044253 );

			index = AddCraft( typeof( SushiRolls ), 1044496, 1030303, CookingConstants.SUSHI_ROLLS_MIN_SKILL, CookingConstants.SUSHI_ROLLS_MAX_SKILL, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( RawFishSteak ), 1044476, CookingConstants.RAW_FISH_STEAK_SUSHI_ROLLS, 1044253 );

			index = AddCraft( typeof( SushiPlatter ), 1044496, 1030305, CookingConstants.SUSHI_PLATTER_MIN_SKILL, CookingConstants.SUSHI_PLATTER_MAX_SKILL, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( RawFishSteak ), 1044476, CookingConstants.RAW_FISH_STEAK_SUSHI_PLATTER, 1044253 );

			index = AddCraft( typeof( TribalPaint ), 1044496, 1040000, Core.ML? CookingConstants.TRIBAL_PAINT_MIN_SKILL : CookingConstants.TRIBAL_PAINT_MIN_SKILL_PRE_ML, Core.ML? CookingConstants.TRIBAL_PAINT_MAX_SKILL : CookingConstants.TRIBAL_PAINT_MAX_SKILL_PRE_ML, typeof( SackFlour ), 1044468, 1, 1044253 );
			AddRes( index, typeof( TribalBerry ), 1046460, CookingConstants.TRIBAL_BERRY_QUANTITY, 1044253 );

			index = AddCraft( typeof( EggBomb ), 1044496, 1030249, CookingConstants.EGG_BOMB_MIN_SKILL, CookingConstants.EGG_BOMB_MAX_SKILL, typeof( Eggs ), 1044477, 1, 1044253 );
			AddRes( index, typeof( SackFlour ), 1044468, CookingConstants.SACK_FLOUR_EGG_BOMB, 1044253 );
			/* End Preparations */

			/* Begin Baking */
			index = AddCraft( typeof( BreadLoaf ), 1044497, 1024156, CookingConstants.BREAD_LOAF_MIN_SKILL, CookingConstants.BREAD_LOAF_MAX_SKILL, typeof( Dough ), 1044469, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CheeseBread ), 1044497, CookingStringConstants.ITEM_CHEESE_BREAD, CookingConstants.CHEESE_BREAD_MIN_SKILL, CookingConstants.CHEESE_BREAD_MAX_SKILL, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( CheeseWheel ), 1044486, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Cookies ), 1044497, 1025643, CookingConstants.COOKIES_MIN_SKILL, CookingConstants.COOKIES_MAX_SKILL, typeof( CookieMix ), 1044474, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Cake ), 1044497, 1022537, CookingConstants.CAKE_MIN_SKILL, CookingConstants.CAKE_MAX_SKILL, typeof( CakeMix ), 1044471, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Muffins ), 1044497, 1022539, CookingConstants.MUFFINS_MIN_SKILL, CookingConstants.MUFFINS_MAX_SKILL, typeof( SweetDough ), 1044475, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Quiche ), 1044497, 1041345, CookingConstants.QUICHE_MIN_SKILL, CookingConstants.QUICHE_MAX_SKILL, typeof( UnbakedQuiche ), 1044518, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( MeatPie ), 1044497, 1041347, CookingConstants.MEAT_PIE_MIN_SKILL, CookingConstants.MEAT_PIE_MAX_SKILL, typeof( UnbakedMeatPie ), 1044519, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( SausagePizza ), 1044497, 1044517, CookingConstants.SAUSAGE_PIZZA_MIN_SKILL, CookingConstants.SAUSAGE_PIZZA_MAX_SKILL, typeof( UncookedSausagePizza ), 1044520, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CheesePizza ), 1044497, 1044516, CookingConstants.CHEESE_PIZZA_MIN_SKILL, CookingConstants.CHEESE_PIZZA_MAX_SKILL, typeof( UncookedCheesePizza ), 1044521, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( FruitPie ), 1044497, 1041346, CookingConstants.FRUIT_PIE_MIN_SKILL, CookingConstants.FRUIT_PIE_MAX_SKILL, typeof( UnbakedFruitPie ), 1044522, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PeachCobbler ), 1044497, 1041344, CookingConstants.PEACH_COBBLER_MIN_SKILL, CookingConstants.PEACH_COBBLER_MAX_SKILL, typeof( UnbakedPeachCobbler ), 1044523, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ApplePie ), 1044497, 1041343, CookingConstants.APPLE_PIE_MIN_SKILL, CookingConstants.APPLE_PIE_MAX_SKILL, typeof( UnbakedApplePie ), 1044524, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PumpkinPie ), 1044497, 1041348, CookingConstants.PUMPKIN_PIE_MIN_SKILL, CookingConstants.PUMPKIN_PIE_MAX_SKILL, typeof( UnbakedPumpkinPie ), 1046461, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( MisoSoup ), 1044497, 1030317, CookingConstants.MISO_SOUP_MIN_SKILL, CookingConstants.MISO_SOUP_MAX_SKILL, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( WhiteMisoSoup ), 1044497, 1030318, CookingConstants.MISO_SOUP_MIN_SKILL, CookingConstants.MISO_SOUP_MAX_SKILL, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( RedMisoSoup ), 1044497, 1030319, CookingConstants.MISO_SOUP_MIN_SKILL, CookingConstants.MISO_SOUP_MAX_SKILL, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( AwaseMisoSoup ), 1044497, 1030320, CookingConstants.MISO_SOUP_MIN_SKILL, CookingConstants.MISO_SOUP_MAX_SKILL, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );
			/* End Baking */

			/* Begin Barbecue */
			index = AddCraft( typeof( CookedBird ), 1044498, 1022487, CookingConstants.COOKED_BIRD_MIN_SKILL, CookingConstants.COOKED_BIRD_MAX_SKILL, typeof( RawBird ), 1044470, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( ChickenLeg ), 1044498, 1025640, CookingConstants.CHICKEN_LEG_MIN_SKILL, CookingConstants.CHICKEN_LEG_MAX_SKILL, typeof( RawChickenLeg ), 1044473, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( FishSteak ), 1044498, 1022427, CookingConstants.FISH_STEAK_MIN_SKILL, CookingConstants.FISH_STEAK_MAX_SKILL, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( FriedEggs ), 1044498, 1022486, CookingConstants.FRIED_EGGS_MIN_SKILL, CookingConstants.FRIED_EGGS_MAX_SKILL, typeof( Eggs ), 1044477, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( LambLeg ), 1044498, 1025642, CookingConstants.LAMB_LEG_MIN_SKILL, CookingConstants.LAMB_LEG_MAX_SKILL, typeof( RawLambLeg ), 1044478, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( Ribs ), 1044498, 1022546, CookingConstants.RIBS_MIN_SKILL, CookingConstants.RIBS_MAX_SKILL, typeof( RawRibs ), 1044485, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );
			/* End Barbecue */

			/* Rations By Krystofer */
			index = AddCraft( typeof(FoodSmallRation), CookingStringConstants.CATEGORY_RATIONS, CookingStringConstants.ITEM_RATION_FISH, CookingConstants.RATION_MIN_SKILL, CookingConstants.RATION_MAX_SKILL, typeof( RawFishSteak ),1044476, 1, 1044253);
			AddRes(index, typeof( BreadLoaf ), 1024156, 1, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);

			index = AddCraft( typeof(FoodSmallRation), CookingStringConstants.CATEGORY_RATIONS, CookingStringConstants.ITEM_RATION_LAMB, CookingConstants.RATION_MIN_SKILL, CookingConstants.RATION_MAX_SKILL, typeof( RawLambLeg ), 1044478, 1, 1044253 );
			AddRes(index, typeof( BreadLoaf ), 1024156, 1, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);

			index = AddCraft( typeof(FoodSmallRation), CookingStringConstants.CATEGORY_RATIONS, CookingStringConstants.ITEM_RATION_PORK, CookingConstants.RATION_MIN_SKILL, CookingConstants.RATION_MAX_SKILL, typeof( RawRibs ), 1044485, 1, 1044253 );
			AddRes(index, typeof( BreadLoaf ), 1024156, 1, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);

			index = AddCraft( typeof(FoodSmallRation), CookingStringConstants.CATEGORY_RATIONS, CookingStringConstants.ITEM_RATION_CHICKEN, CookingConstants.RATION_MIN_SKILL, CookingConstants.RATION_MAX_SKILL, typeof( RawChickenLeg ), 1044473, 1, 1044253 );
			AddRes(index, typeof( BreadLoaf ), 1024156, 1, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);

			index = AddCraft( typeof(FoodSmallRation), CookingStringConstants.CATEGORY_RATIONS, CookingStringConstants.ITEM_RATION_BIRD, CookingConstants.RATION_MIN_SKILL, CookingConstants.RATION_MAX_SKILL, typeof( RawBird ), 1044470, 1, 1044253 );
			AddRes(index, typeof( BreadLoaf ), 1024156, 1, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);

/*			index = AddCraft( typeof(FoodLargeRation),"Rations", "large ration", 95.0, 120.0, typeof( Cookies ),"cookies", 2, 1044253);
			AddRes(index, typeof( BreadLoaf ), 1024156, 2, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);

			index = AddCraft( typeof(FoodLargeRation),"Rations", "large ration", 90.0, 120.0, typeof( Quiche ), "quiche", 2, 1044253 );
			AddRes(index, typeof( BreadLoaf ), 1024156, 2, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);

			index = AddCraft( typeof(FoodLargeRation),"Rations", "large ration", 90.0, 120.0, typeof( MeatPie ), "meat pie", 2, 1044253 );
			AddRes(index, typeof( BreadLoaf ), 1024156, 2, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);

			index = AddCraft( typeof(FoodLargeRation),"Rations", "large ration", 90.0, 120.0, typeof( SausagePizza ), "saussage pizza", 2, 1044253 );
			AddRes(index, typeof( BreadLoaf ), 1024156, 2, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);

			index = AddCraft( typeof(FoodLargeRation),"Rations", "large ration", 90.0, 120.0, typeof( CheesePizza ), "cheese pizza", 2, 1044253 );
			AddRes(index, typeof( BreadLoaf ), 1024156, 2, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);*/

			/* End Rations */



/*
			AddCraft( typeof( CarvedPumpkin ), "Halloween", "jack-o-lantern", 80.0, 110.0, typeof( PumpkinLarge ), "Large Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin2 ), "Halloween", "jack-o-lantern", 80.0, 110.0, typeof( PumpkinLarge ), "Large Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin16 ), "Halloween", "jack-o-lantern", 80.0, 110.0, typeof( PumpkinLarge ), "Large Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin17 ), "Halloween", "jack-o-lantern", 80.0, 110.0, typeof( PumpkinLarge ), "Large Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin18 ), "Halloween", "jack-o-lantern", 80.0, 110.0, typeof( PumpkinLarge ), "Large Pumpkin", 1, 1042081 );
			index = AddCraft( typeof( CarvedPumpkin19 ), "Halloween", "jack-o-lantern", 80.0, 110.0, typeof( PumpkinLarge ), "Large Pumpkin", 1, 1042081 );
				AddRes( index, typeof( SkullGiant ), "Giant Skull", 1, 1042081 );

			AddCraft( typeof( CarvedPumpkin3 ), "Halloween", "jack-o-lantern", 90.0, 120.0, typeof( PumpkinTall ), "Tall Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin4 ), "Halloween", "jack-o-lantern", 90.0, 120.0, typeof( PumpkinTall ), "Tall Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin5 ), "Halloween", "jack-o-lantern", 90.0, 120.0, typeof( PumpkinTall ), "Tall Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin6 ), "Halloween", "jack-o-lantern", 90.0, 120.0, typeof( PumpkinTall ), "Tall Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin7 ), "Halloween", "jack-o-lantern", 90.0, 120.0, typeof( PumpkinTall ), "Tall Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin8 ), "Halloween", "jack-o-lantern", 90.0, 120.0, typeof( PumpkinTall ), "Tall Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin9 ), "Halloween", "jack-o-lantern", 90.0, 120.0, typeof( PumpkinTall ), "Tall Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin10 ), "Halloween", "jack-o-lantern", 90.0, 120.0, typeof( PumpkinTall ), "Tall Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin11 ), "Halloween", "jack-o-lantern", 90.0, 120.0, typeof( PumpkinTall ), "Tall Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin12 ), "Halloween", "jack-o-lantern", 90.0, 120.0, typeof( PumpkinTall ), "Tall Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin13 ), "Halloween", "jack-o-lantern", 90.0, 120.0, typeof( PumpkinTall ), "Tall Pumpkin", 1, 1042081 );

			AddCraft( typeof( CarvedPumpkin14 ), "Halloween", "jack-o-lantern", 95.0, 120.0, typeof( PumpkinGreen ), "Green Pumpkin", 1, 1042081 );
			AddCraft( typeof( CarvedPumpkin15 ), "Halloween", "jack-o-lantern", 95.0, 120.0, typeof( PumpkinGreen ), "Green Pumpkin", 1, 1042081 );

			AddCraft( typeof( CarvedPumpkin20 ), "Halloween", "jack-o-lantern", 99.0, 125.0, typeof( PumpkinGiant ), "Giant Pumpkin", 1, 1042081 );*/
		}
	}
}

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
			get { return 1044003; } // <CENTER>COOKING MENU</CENTER>
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
			return 0.0; // 0%
		}

		private DefCooking() : base( 1, 1, 1.25 )// base( 1, 1, 1.5 )
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
			index = AddCraft( typeof( SackFlour ), 1044495, 1024153, 0.0, 40.0, typeof( WheatSheaf ), 1044489, 5, 1044490 );
			SetNeedMill( index, true );

			index = AddCraft( typeof( Dough ), 1044495, 1024157, 20.0, 50.0, typeof( SackFlour ), 1044468, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );

			index = AddCraft( typeof( SweetDough ), 1044495, 1041340, 30.0, 60.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( JarHoney ), 1044472, 1, 1044253 );

			index = AddCraft( typeof( CakeMix ), 1044495, 1041002, 40.0, 70.0, typeof( SackFlour ), 1044468, 1, 1044253 );
			AddRes( index, typeof( SweetDough ), 1044475, 1, 1044253 );

			index = AddCraft( typeof( CookieMix ), 1044495, 1024159, 40.0, 70.0, typeof( JarHoney ), 1044472, 1, 1044253 );
			AddRes( index, typeof( SweetDough ), 1044475, 1, 1044253 );
            /* End Ingredients */

            /* Begin Preparations */
            index = AddCraft(typeof(FruitBasket), 1044496, "Cesta de Frutas", 40.0, 85.0, typeof(Basket), "Basket", 1, 1044253);
            AddRes(index, typeof(Pear), 1044481, 2, 1044253);
            AddRes(index, typeof(Peach), 1044480, 2, 1044253);
            AddRes(index, typeof(Apple), 1044479, 2, 1044253);

            index = AddCraft( typeof( UnbakedQuiche ), 1044496, 1041339, 30.0, 65.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Eggs ), 1044477, 1, 1044253 );
			AddRes( index, typeof( Carrot ), "Cenoura", 1, 1044253 );

			// TODO: This must also support chicken and lamb legs
			index = AddCraft( typeof( UnbakedMeatPie ), 1044496, 1041338, 30.0, 60.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( RawRibs ), 1044482, 1, 1044253 );
			AddRes( index, typeof( FoodPotato ), "Batata", 1, 1044253 );

			index = AddCraft( typeof( UncookedSausagePizza ), 1044496, 1041337, 40.0, 60.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Sausage ), 1044483, 1, 1044253 );
			AddRes( index, typeof( Tomato ), "Tomate", 2, 1044253 );
			AddRes( index, typeof( CheeseWheel ), 1044486, 1, 1044253 );

			index = AddCraft( typeof( UncookedCheesePizza ), 1044496, 1041341, 30.0, 65.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( CheeseWheel ), 1044486, 1, 1044253 );
			AddRes( index, typeof( Tomato ), "Tomate", 2, 1044253 );

			index = AddCraft( typeof( UnbakedFruitPie ), 1044496, 1041334, 40.0, 85.0, typeof( SweetDough ), 1044475, 1, 1044253 );
			AddRes( index, typeof( Pear ), 1044481, 2, 1044253 );

			index = AddCraft( typeof( UnbakedPeachCobbler ), 1044496, 1041335, 40.0, 85.0, typeof( SweetDough ), 1044475, 1, 1044253 );
			AddRes( index, typeof( Peach ), 1044480, 2, 1044253 );

			index = AddCraft( typeof( UnbakedApplePie ), 1044496, 1041336, 40.0, 85.0, typeof( SweetDough ), 1044475, 1, 1044253 );
			AddRes( index, typeof( Apple ), 1044479, 2, 1044253 );

			index = AddCraft( typeof( UnbakedPumpkinPie ), 1044496, 1041342, 40.0, 85.0, typeof( SweetDough ), 1044475, 1, 1044253 );
			AddRes( index, typeof( Pumpkin ), 1044484, 2, 1044253 );

			index = AddCraft( typeof( GreenTea ), 1044496, 1030315, 70.0, 100.0, typeof( GreenTeaBasket ), 1030316, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( WasabiClumps ), 1044496, 1029451, 70.0, 100.0, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( WoodenBowlOfPeas ), 1025633, 3, 1044253 );

			index = AddCraft( typeof( SushiRolls ), 1044496, 1030303, 50.0, 90.0, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( RawFishSteak ), 1044476, 6, 1044253 );

			index = AddCraft( typeof( SushiPlatter ), 1044496, 1030305, 50.0, 90.0, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( RawFishSteak ), 1044476, 10, 1044253 );

			index = AddCraft( typeof( TribalPaint ), 1044496, 1040000, Core.ML? 55.0 : 70.0, Core.ML? 95.0 : 90.0, typeof( SackFlour ), 1044468, 1, 1044253 );
			AddRes( index, typeof( TribalBerry ), 1046460, 3, 1044253 );

			index = AddCraft( typeof( EggBomb ), 1044496, 1030249, 75.0, 100.0, typeof( Eggs ), 1044477, 1, 1044253 );
			AddRes( index, typeof( SackFlour ), 1044468, 3, 1044253 );
			/* End Preparations */

			/* Begin Baking */
			index = AddCraft( typeof( BreadLoaf ), 1044497, 1024156, 30.0, 80.0, typeof( Dough ), 1044469, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CheeseBread ), 1044497, "cheese bread", 40.0, 90.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( CheeseWheel ), 1044486, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Cookies ), 1044497, 1025643, 70.0, 100.0, typeof( CookieMix ), 1044474, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Cake ), 1044497, 1022537, 80.0, 120.0, typeof( CakeMix ), 1044471, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Muffins ), 1044497, 1022539, 60.0, 110.0, typeof( SweetDough ), 1044475, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Quiche ), 1044497, 1041345, 60.0, 100.0, typeof( UnbakedQuiche ), 1044518, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( MeatPie ), 1044497, 1041347, 60.0, 110.0, typeof( UnbakedMeatPie ), 1044519, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( SausagePizza ), 1044497, 1044517, 80.0, 115.0, typeof( UncookedSausagePizza ), 1044520, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CheesePizza ), 1044497, 1044516, 70.0, 100.0, typeof( UncookedCheesePizza ), 1044521, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( FruitPie ), 1044497, 1041346, 80.0, 100.0, typeof( UnbakedFruitPie ), 1044522, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PeachCobbler ), 1044497, 1041344, 80.0, 100.0, typeof( UnbakedPeachCobbler ), 1044523, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ApplePie ), 1044497, 1041343, 70.0, 100.0, typeof( UnbakedApplePie ), 1044524, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PumpkinPie ), 1044497, 1041348, 60.0, 100.0, typeof( UnbakedPumpkinPie ), 1046461, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( MisoSoup ), 1044497, 1030317, 40.0, 70.0, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( WhiteMisoSoup ), 1044497, 1030318, 40.0, 70.0, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( RedMisoSoup ), 1044497, 1030319, 40.0, 70.0, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( AwaseMisoSoup ), 1044497, 1030320, 40.0, 70.0, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );
			/* End Baking */

			/* Begin Barbecue */
			index = AddCraft( typeof( CookedBird ), 1044498, 1022487, 20.0, 70.0, typeof( RawBird ), 1044470, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( ChickenLeg ), 1044498, 1025640, 20.0, 70.0, typeof( RawChickenLeg ), 1044473, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( FishSteak ), 1044498, 1022427, 10.0, 70.0, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( FriedEggs ), 1044498, 1022486, 10.0, 70.0, typeof( Eggs ), 1044477, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( LambLeg ), 1044498, 1025642, 30.0, 70.0, typeof( RawLambLeg ), 1044478, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( Ribs ), 1044498, 1022546, 40.0, 80.0, typeof( RawRibs ), 1044485, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );
			/* End Barbecue */

			/* Rations By Krystofer */
			index = AddCraft( typeof(FoodSmallRation), "Ra��o", "Ra��o de Peixe", 70.0, 100.0, typeof( RawFishSteak ),1044476, 1, 1044253);
			AddRes(index, typeof( BreadLoaf ), 1024156, 1, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);

			index = AddCraft( typeof(FoodSmallRation), "Ra��o", "Ra��o de Carneiro", 70.0, 100.0, typeof( RawLambLeg ), 1044478, 1, 1044253 );
			AddRes(index, typeof( BreadLoaf ), 1024156, 1, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);

			index = AddCraft( typeof(FoodSmallRation), "Ra��o", "Ra��o de Porco", 70.0, 100.0, typeof( RawRibs ), 1044485, 1, 1044253 );
			AddRes(index, typeof( BreadLoaf ), 1024156, 1, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);

			index = AddCraft( typeof(FoodSmallRation), "Ra��o", "Ra��o de Galinha", 70.0, 100.0, typeof( RawChickenLeg ), 1044473, 1, 1044253 );
			AddRes(index, typeof( BreadLoaf ), 1024156, 1, 1024156);
			AddRes(index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true);

			index = AddCraft( typeof(FoodSmallRation), "Ra��o", "Ra��o de P�ssaro", 70.0, 100.0, typeof( RawBird ), 1044470, 1, 1044253 );
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

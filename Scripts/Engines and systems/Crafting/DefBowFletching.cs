using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefBowFletching : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Fletching; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044006; } // <CENTER>BOWCRAFT AND FLETCHING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefBowFletching();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefBowFletching() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 )
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
			from.PlaySound( 0x55 );
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

		public override CraftECA ECA{ get{ return CraftECA.FiftyPercentChanceMinusTenPercent; } }

		public override void InitCraftList()
		{
			int index = -1;

			// Materials
			AddCraft( typeof( Kindling ), 1044457, "gravetos", 0.0, 30.0, typeof( Log ), 1015101, 1, 1044351 );

			index = AddCraft( typeof( Kindling ), 1044457, "lote de gravetos", 15.0, 40.0, typeof( Log ), 1015101, 1, 1044351 );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( Shaft ), 1044457, 1027124, 0.0, 30.0, typeof( Log ), 1015101, 1, 1044351 );
			//SetUseAllRes( index, true );

			// Ammunition
			index = AddCraft( typeof( Arrow ), 1044565, 1023903, 30.0, 60.0, typeof( Shaft ), 1044560, 1, 1044561 );
			AddRes( index, typeof( Feather ), 1044562, 1, 1044563 );
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( Bolt ), 1044565, 1027163, 50.0, 70.0, typeof( Shaft ), 1044560, 1, 1044561 );
			AddRes( index, typeof( Feather ), 1044562, 1, 1044563 );
            AddRes(index, typeof(IronIngot), 1044562, 1, 1044563);
            //SetUseAllRes( index, true );

            if ( Core.SE )
			{
				index = AddCraft( typeof( FukiyaDarts ), 1044565, 1030246, 60.0, 80.0, typeof( Log ), 1015101, 1, 1044351 );
				//SetUseAllRes( index, true );
                index = AddCraft(typeof(ThrowingWeapon), 1044565, 1044117, 70.0, 90.0, typeof(IronIngot), 1074904, 1, 1044037);
                //SetUseAllRes(index, true);
            }

			// Weapons
			AddCraft( typeof( Bow ), 1044566, 1025042, 40.0, 75.0, typeof( Board ), 1015101, 8, 1044351 );
			AddCraft( typeof( Crossbow ), 1044566, 1023919, 60.0, 90.0, typeof( Board ), 1015101, 12, 1044351 );
			AddCraft( typeof( HeavyCrossbow ), 1044566, 1025117, 80.0, 110.0, typeof( Board ), 1015101, 15, 1044351);

			if ( Core.AOS )
			{
				AddCraft( typeof( CompositeBow ), 1044566, 1029922, 70.0, 100.0, typeof( Board ), 1015101, 11, 1044351 );
				AddCraft( typeof( RepeatingCrossbow ), 1044566, 1029923, 90.0, 110.0, typeof( Board ), 1015101, 14, 1044351 );
			}

			if( Core.SE )
			{
				index = AddCraft( typeof( Yumi ), 1044566, 1030224, 90.0, 120.0, typeof( Board ), 1015101, 13, 1044351 );
				 
			}

			AddCraft( typeof( MagicalShortbow ), 1044566, "Arco curto da floresta", 55.0, 85.0, typeof( Board ), 1015101, 9, 1044351 );
			AddCraft( typeof( ElvenCompositeLongbow ), 1044566, "Arco longo da floresta", 70.0, 95.0, typeof( Board ), 1015101, 16, 1044351 );

			Repair = true;
			MarkOption = true;
			CanEnhance = Core.AOS;

			SetSubRes( typeof( Board ), 1072643 );

            // Add every material you want the player to be able to choose from
            // This will override the overridable material	TODO: Verify the required skill amount
            AddSubRes(typeof(Board), 1072643, 00.0, 1015101, 1072652);
            AddSubRes(typeof(AshBoard), 1095379, 60.0, 1015101, 1072652);
            AddSubRes(typeof(EbonyBoard), 1095381, 70.0, 1015101, 1072652);
            AddSubRes(typeof(ElvenBoard), 1095535, 80.0, 1015101, 1072652);
            AddSubRes(typeof(GoldenOakBoard), 1095382, 85.0, 1015101, 1072652);
            AddSubRes(typeof(CherryBoard), 1095380, 90.0, 1015101, 1072652);
            AddSubRes(typeof(RosewoodBoard), 1095387, 95.0, 1015101, 1072652);
            AddSubRes(typeof(HickoryBoard), 1095383, 100.0, 1015101, 1072652);
        }
	}
}
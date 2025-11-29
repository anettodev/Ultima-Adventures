using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	public class CraftGumpItem : Gump
	{
		private Mobile m_From;
		private CraftSystem m_CraftSystem;
		private CraftItem m_CraftItem;
		private BaseTool m_Tool;

		private const int LabelHue = 0x480; // 0x384
		private const int RedLabelHue = 0x20;

		private const int LabelColor = 0x7FFF;
		private const int RedLabelColor = 0x6400;

		private const int GreyLabelColor = 0x3DEF;
		private const int LabelYellow = 0xfcd219; // Hex color for HTML
		private const int LabelYellowHue = 0x35; // Yellow hue for AddHtmlLocalized/AddLabel
        private const int LabelPurple = 0xfcd219;
        private const int LabelBlue = 0x7BB; // Blue hue for AddHtmlLocalized/AddLabel
        private const int LabelBlueHex = 0x0077BB; // Blue hex color for HTML
        private const int LabelGreen = 0x7CA; // Green hue for AddHtmlLocalized/AddLabel
        private const int LabelCyan = 0x00FFFF; // Hex color for HTML
        private const int LabelCyanHue = 0x5A; // Cyan hue for AddHtmlLocalized/AddLabel

        private int m_OtherCount;

		public CraftGumpItem( Mobile from, CraftSystem craftSystem, CraftItem craftItem, BaseTool tool ) : base( 40, 40 )
		{
			m_From = from;
			m_CraftSystem = craftSystem;
			m_CraftItem = craftItem;
			m_Tool = tool;

			from.CloseGump( typeof( CraftGump ) );
			from.CloseGump( typeof( CraftGumpItem ) );

			AddPage( 0 );
			AddBackground( 0, 0, 530, 417, 5054 );
			AddImageTiled( 10, 10, 510, 22, 2624 );
			AddImageTiled( 10, 37, 150, 148, 2624 );
			AddImageTiled( 165, 37, 355, 90, 2624 );
			AddImageTiled( 10, 190, 155, 22, 2624 );
			AddImageTiled( 10, 217, 150, 53, 2624 );
			AddImageTiled( 165, 132, 355, 80, 2624 );
			AddImageTiled( 10, 275, 155, 22, 2624 );
			AddImageTiled( 10, 302, 150, 53, 2624 );
			AddImageTiled( 165, 217, 355, 80, 2624 );
			AddImageTiled( 10, 360, 155, 22, 2624 );
			AddImageTiled( 165, 302, 355, 80, 2624 );
			AddImageTiled( 10, 387, 510, 22, 2624 );
			AddAlphaRegion( 10, 10, 510, 399 );

			string itemHtml = String.Format( "<BASEFONT COLOR=#{0:X6}>Item</BASEFONT>", LabelYellow );
			AddHtml( 170, 40, 150, 20, itemHtml, false, false ); // ITEM
			string skillsHtml = String.Format( "<BASEFONT COLOR=#{0:X6}><CENTER>Habilidades</CENTER></BASEFONT>", LabelYellow );
			AddHtml( 10, 192, 150, 22, skillsHtml, false, false ); // <CENTER>SKILLS</CENTER>
			string materialsHtml = String.Format( "<BASEFONT COLOR=#{0:X6}><CENTER>Materiais</CENTER></BASEFONT>", LabelYellow );
			AddHtml( 10, 277, 150, 22, materialsHtml, false, false ); // <CENTER>MATERIALS</CENTER>
			string otherHtml = String.Format( "<BASEFONT COLOR=#{0:X6}><CENTER>Outros</CENTER></BASEFONT>", LabelYellow );
			AddHtml( 10, 362, 150, 22, otherHtml, false, false ); // <CENTER>OTHER</CENTER>

			if ( craftSystem.GumpTitleNumber > 0 )
				AddHtmlLocalized( 10, 12, 510, 20, craftSystem.GumpTitleNumber, LabelPurple, false, false );
			else
				AddHtml( 10, 12, 510, 20, craftSystem.GumpTitleString, false, false );

			AddButton( 15, 387, 4014, 4016, 0, GumpButtonType.Reply, 0 );
			string backHtml = String.Format( "<BASEFONT COLOR=#FFFFFF>Voltar</BASEFONT>" );
			AddHtml( 50, 390, 150, 18, backHtml, false, false ); // BACK

			bool needsRecipe = ( craftItem.Recipe != null && from is PlayerMobile && !((PlayerMobile)from).HasRecipe( craftItem.Recipe ) );

			if( needsRecipe )
			{
				AddButton( 270, 387, 4005, 4007, 0, GumpButtonType.Page, 0 );
				string makeNowHtml = String.Format( "<BASEFONT COLOR=#{0:X6}>Fazer Agora</BASEFONT>", LabelCyan );
				AddHtml( 305, 390, 150, 18, makeNowHtml, false, false ); // MAKE NOW
			}
			else
			{
				AddButton( 270, 387, 4005, 4007, 1, GumpButtonType.Reply, 0 );
				string makeNowHtml = String.Format( "<BASEFONT COLOR=#{0:X6}>Fazer Agora</BASEFONT>", LabelCyan );
				AddHtml( 305, 390, 150, 18, makeNowHtml, false, false ); // MAKE NOW
			}

			if ( craftItem.NameNumber > 0 )
				AddHtmlLocalized( 330, 40, 180, 18, craftItem.NameNumber, LabelColor, false, false );
			else
				AddLabel( 330, 40, LabelCyanHue, craftItem.NameString );

			if ( craftItem.UseAllRes )
				AddHtmlLocalized( 170, 302 + (m_OtherCount++ * 20), 310, 18, 1048176, RedLabelColor, false, false ); // Makes as many as possible at once

			DrawItem();
			DrawSkill();
			DrawResource();

			/*
			if( craftItem.RequiresSE )
				AddHtmlLocalized( 170, 302 + (m_OtherCount++ * 20), 310, 18, 1063363, LabelColor, false, false ); //* Requires the "Samurai Empire" expansion
			 * */

			if( craftItem.RequiredExpansion != Expansion.None )
			{
				bool supportsEx = (from.NetState != null && from.NetState.SupportsExpansion( craftItem.RequiredExpansion ));
				TextDefinition.AddHtmlText( this, 170, 302 + (m_OtherCount++ * 20), 310, 18, RequiredExpansionMessage( craftItem.RequiredExpansion ), false, false, supportsEx ? LabelColor : RedLabelColor, supportsEx ? LabelHue : RedLabelHue );
			}

		if( needsRecipe )
			TextDefinition.AddHtmlText( this, 170, 302 + (m_OtherCount++ * 20), 310, 18, CraftGumpStringConstants.NOTICE_RECIPE_NOT_LEARNED, false, false, RedLabelColor, RedLabelHue );

		}

		private TextDefinition RequiredExpansionMessage( Expansion expansion )
		{
			switch( expansion )
			{
				case Expansion.SE:
					return 1063363; // * Requires the "Samurai Empire" expansion
				case Expansion.ML:
					return 1072651; // * Requires the "Mondain's Legacy" expansion
				default:
					return String.Format( "* Requires the \"{0}\" expansion", ExpansionInfo.GetInfo( expansion ).Name );
			}
		}

		private bool m_ShowExceptionalChance;

		public void DrawItem()
		{
			Type type = m_CraftItem.ItemType;

			AddItem( 20, 50, CraftItem.ItemIDOf( type ) );

			if ( type != typeof(BaseMagicStaff) )
			{
				m_ShowExceptionalChance = false;
			}
			else if ( m_CraftItem.IsMarkable( type ) )
			{
				AddHtmlLocalized( 170, 302 + (m_OtherCount++ * 20), 310, 18, 1044059, LabelBlue, false, false ); // This item may hold its maker's mark
				m_ShowExceptionalChance = true;
			}
		}

		public void DrawSkill()
		{
			for ( int i = 0; i < m_CraftItem.Skills.Count; i++ )
			{
				CraftSkill skill = m_CraftItem.Skills.GetAt( i );
				double minSkill = skill.MinSkill, maxSkill = skill.MaxSkill;

				if ( minSkill < 0 )
					minSkill = 0;

				AddHtmlLocalized( 170, 132 + (i * 20), 200, 18, 1044060 + (int)skill.SkillToMake, LabelGreen, false, false );
				AddLabel( 430, 132 + (i * 20), LabelBlue, String.Format( "{0:F1}", minSkill ) );
			}

			CraftSubResCol res = ( m_CraftItem.UseSubRes2 ? m_CraftSystem.CraftSubRes2 : m_CraftSystem.CraftSubRes );
			int resIndex = -1;

			CraftContext context = m_CraftSystem.GetContext( m_From );

			if ( context != null )
				resIndex = ( m_CraftItem.UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex );

			bool allRequiredSkills = true;
			double chance = m_CraftItem.GetSuccessChance( m_From, resIndex > -1 ? res.GetAt( resIndex ).ItemType : null, m_CraftSystem, false, ref allRequiredSkills );
			double excepChance = m_CraftItem.GetExceptionalChance( m_CraftSystem, chance, m_From );

			if ( chance < 0.0 )
				chance = 0.0;
			else if ( chance > 1.0 )
				chance = 1.0;

			string successChanceHtml = String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", LabelCyan, CraftGumpStringConstants.LABEL_SUCCESS_CHANCE );
			AddHtml( 170, 80, 250, 18, successChanceHtml, false, false ); // Success Chance:
			
			// Determine color based on success chance percentage
			double successPercent = chance * 100;
			int percentColor;
			if ( successPercent > 70.0 )
				percentColor = LabelGreen; // 0x7CA - Green hue
			else if ( successPercent > 35.0 )
				percentColor = LabelBlue; // 0x7BB - Blue hue
			else
				percentColor = RedLabelHue; // 0x20 - Red hue (correct for AddLabel)
			
			AddLabel( 430, 80, percentColor, String.Format( "{0:F1}%", successPercent ) );

			if ( m_ShowExceptionalChance )
			{
				if( excepChance < 0.0 )
					excepChance = 0.0;
				else if( excepChance > 1.0 )
					excepChance = 1.0;

				AddHtmlLocalized( 170, 100, 250, 18, 1044058, 32767, false, false ); // Exceptional Chance:
				AddLabel( 430, 100, LabelHue, String.Format( "{0:F1}%", excepChance * 100 ) );
			}
		}

		private static Type typeofBlankScroll = typeof( BlankScroll );
        private static Type typeofRunebook = typeof(Runebook);
        private static Type typeofSpellScroll = typeof( SpellScroll );

		public void DrawResource()
		{
			bool retainedColor = false;

			CraftContext context = m_CraftSystem.GetContext( m_From );

			CraftSubResCol res = ( m_CraftItem.UseSubRes2 ? m_CraftSystem.CraftSubRes2 : m_CraftSystem.CraftSubRes );
			int resIndex = -1;

			if ( context != null )
				resIndex = ( m_CraftItem.UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex );

			bool cropScroll = ( m_CraftItem.Resources.Count > 1 )
				&& m_CraftItem.Resources.GetAt( m_CraftItem.Resources.Count - 1 ).ItemType == typeofBlankScroll
				&& typeofSpellScroll.IsAssignableFrom( m_CraftItem.ItemType );

            //bool cropRunebook = m_CraftItem.Resources.GetAt(m_CraftItem.Resources.Count - 1).ItemType == typeofRunebook;

            for ( int i = 0; i < m_CraftItem.Resources.Count - (cropScroll ? 1 : 0) && i < 4; i++ )
			{
				Type type;
				string nameString;
				int nameNumber;

				CraftRes craftResource = m_CraftItem.Resources.GetAt( i );

				type = craftResource.ItemType;
				nameString = craftResource.NameString;
				nameNumber = craftResource.NameNumber;
				
				// Resource Mutation
				if ( type == res.ResType && resIndex > -1 )
				{
					CraftSubRes subResource = res.GetAt( resIndex );

					type = subResource.ItemType;

					nameString = subResource.NameString;
					nameNumber = subResource.GenericNameNumber;

					if ( nameNumber <= 0 )
						nameNumber = subResource.NameNumber;
				}
				// ******************

				if ( !retainedColor && m_CraftItem.RetainsColorFrom( m_CraftSystem, type ) )
				{
					retainedColor = true;
					string retainsColorHtml = String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", LabelCyan, CraftGumpStringConstants.MESSAGE_RETAINS_COLOR );
					AddHtml( 170, 302 + (m_OtherCount++ * 20), 310, 18, retainsColorHtml, false, false ); // * The item retains the color of this material
					AddLabel( 500, 219 + (i * 20), LabelBlue, "*" );
				}

				if ( nameNumber > 0 )
					AddHtmlLocalized( 170, 219 + (i * 20), 310, 18, nameNumber, LabelGreen, false, false );
				else
					AddLabel( 170, 219 + (i * 20), LabelGreen, nameString );

				AddLabel( 430, 219 + (i * 20), LabelHue, craftResource.Amount.ToString() );
			}
/*
			if ( m_CraftItem.NameNumber == 1041267 ) // runebook // WIZARD REMOVED
			{
				AddHtmlLocalized( 170, 219 + (m_CraftItem.Resources.Count * 20), 310, 18, 1044447, LabelColor, false, false );
				AddLabel( 430, 219 + (m_CraftItem.Resources.Count * 20), LabelHue, "1" );
			}
*/
			if ( cropScroll )
				AddHtmlLocalized( 170, 302 + (m_OtherCount++ * 20), 360, 18, 1044379, LabelColor, false, false ); // Inscribing scrolls also requires a blank scroll and mana.

			if (m_CraftItem.ItemType == typeofRunebook)
                AddHtmlLocalized(170, 302 + (m_OtherCount++ * 20), 360, 18, 1109025, LabelColor, false, false);
        }

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			// Back Button
			if ( info.ButtonID == 0 )
			{
				CraftGump craftGump = new CraftGump( m_From, m_CraftSystem, m_Tool, null );
				m_From.SendGump( craftGump );
			}
			else // Make Button
			{
                PlayerMobile pm = m_From as PlayerMobile;
                if (CraftSystem.PlayerLoc == null)
                {
                    CraftSystem.PlayerLoc = new Dictionary<Mobile, Point3D>();
                }
                if (!CraftSystem.PlayerLoc.ContainsKey(pm))
                    CraftSystem.PlayerLoc[pm] = pm.Location;

                int num = m_CraftSystem.CanCraft( m_From, m_Tool, m_CraftItem.ItemType );

				if ( num > 0 )
				{
					m_From.SendGump( new CraftGump( m_From, m_CraftSystem, m_Tool, num ) );
				}
				else
				{
					Type type = null;

					CraftContext context = m_CraftSystem.GetContext( m_From );

					if ( context != null )
					{
						CraftSubResCol res = ( m_CraftItem.UseSubRes2 ? m_CraftSystem.CraftSubRes2 : m_CraftSystem.CraftSubRes );
						int resIndex = ( m_CraftItem.UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex );

						if ( resIndex > -1 )
							type = res.GetAt( resIndex ).ItemType;
					}

					m_CraftSystem.CreateItem( m_From, m_CraftItem.ItemType, type, m_Tool, m_CraftItem );
				}
			}
		}
	}
}
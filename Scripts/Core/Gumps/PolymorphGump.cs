using System;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Targets;
using Server.Spells;
using Server.Spells.Seventh;

namespace Server.Gumps
{
	public class PolymorphEntry
	{
		// 0 < 40: Chicken, Dog, Slime (3 forms)
		public static readonly PolymorphEntry Chicken =		new PolymorphEntry( 8401, 0xD0, 1015236, 15, 10, 0 );
		public static readonly PolymorphEntry Dog =			new PolymorphEntry( 8405, 0xD9, 1015237, 17, 10, 0 );
		public static readonly PolymorphEntry Slime =		new PolymorphEntry( 8424, 0x33, 1015246, 5, 10, 0 );

		// 40 < 50: Eagle, Wolf, Hind (3 forms)
		public static readonly PolymorphEntry Eagle =		new PolymorphEntry( 0x211D, 0x5, 1015236, 15, 10, 40 );
		public static readonly PolymorphEntry Wolf =		new PolymorphEntry( 8426, 0xE1, 1015238, 18, 10, 40 );
		public static readonly PolymorphEntry Hind =		new PolymorphEntry( 0x20D4, 0xED, 1015236, 15, 10, 40 );

		// 50 < 60: Giant Snake, Panther, Gorilla, BlackBear, GrizzlyBear, Orc (6 forms)
		public static readonly PolymorphEntry GiantSnake =	new PolymorphEntry( 0x25BF, 0x15, 1015236, 15, 10, 50 );
		public static readonly PolymorphEntry Panther =		new PolymorphEntry( 8473, 0xD6, 1015239, 20, 14, 50 );
		public static readonly PolymorphEntry Gorilla =		new PolymorphEntry( 8437, 0x1D, 1015240, 23, 10, 50 );
		public static readonly PolymorphEntry BlackBear =	new PolymorphEntry( 8399, 0xD3, 1015241, 22, 10, 50 );
		public static readonly PolymorphEntry GrizzlyBear =	new PolymorphEntry( 8411, 0xD4, 1015242, 22, 12, 50 );
		public static readonly PolymorphEntry Orc =			new PolymorphEntry( 8416, 0x11, 1015247, 29, 10, 50 );

		// 60 < 70: LizardMan, PolarBear, Ogre (3 forms)
		public static readonly PolymorphEntry LizardMan =	new PolymorphEntry( 8414, 0x21, 1015248, 26, 10, 60 );
		public static readonly PolymorphEntry PolarBear =	new PolymorphEntry( 8417, 0xD5, 1015243, 26, 10, 60 );
		public static readonly PolymorphEntry Ogre =		new PolymorphEntry( 8415, 0x01, 1015250, 24, 9, 60 );

		// 70 < 80: Troll, Gargoyle, Stirge (3 forms)
		public static readonly PolymorphEntry Troll =		new PolymorphEntry( 8425, 0x36, 1015251, 25, 9, 70 );
		public static readonly PolymorphEntry Gargoyle =	new PolymorphEntry( 8409, 0x04, 1015249, 22, 10, 70 );
		public static readonly PolymorphEntry Stirge =		new PolymorphEntry( 0x25A6, 0x6D, 1015236, 15, 10, 70 ); // Bat/Mongbat image

		// 80 < 90: Minotaur, Naga Guardian, DeathKnight (3 forms)
		public static readonly PolymorphEntry Minotaur =	new PolymorphEntry( 0x2D88, 0x4E, 1015236, 15, 10, 80 );
		public static readonly PolymorphEntry NagaGuardian = new PolymorphEntry( 0x2134, 0x42, 1015236, 15, 10, 80 );
		public static readonly PolymorphEntry DeathKnight =	new PolymorphEntry( 0x2D85, 0x41, 1015236, 15, 10, 80 ); // Giant Widow image

		// 90 - 100: Daemon, Giant Widow, Werebear (3 forms)
		public static readonly PolymorphEntry Daemon =		new PolymorphEntry( 8403, 0x09, 1015253, 25, 8, 90 );
		public static readonly PolymorphEntry GiantWidow =	new PolymorphEntry( 0x25C3, 0x8C, 1015236, 15, 10, 90 ); // Widow Spider image
		public static readonly PolymorphEntry Werewolf =	new PolymorphEntry( 8399, 0x22, 1015236, 15, 10, 90 ); // Black Bear image (Werebear)

		// 100 < 110: Cerberus, WolfMan (2 forms)
		// Cerberus: Body 0x8D (141 decimal) → 
		// WolfMan: Body 0x5E (94 decimal) → Uses same art ID as Wolf (8426 / 0x20EA) for better visual representation
		// public static readonly PolymorphEntry Cerberus =	new PolymorphEntry( 0x335B, 0x8D, 1015236, 15, 10, 100 ); // Commented out
		public static readonly PolymorphEntry WolfMan =		new PolymorphEntry( 0x2770, 0x5E, 1015236, 15, 10, 100 );

		// 110 <= 120: Dragon (1 form)
		public static readonly PolymorphEntry Dragon =		new PolymorphEntry( 0x20D6, 0x3B, 1015236, 15, 10, 110 );

		private int m_Art, m_Body, m_Num, m_X, m_Y, m_MinSkill;

		private PolymorphEntry( int Art, int Body, int LocNum, int X, int Y, int minSkill )
		{
			m_Art = Art;
			m_Body = Body;
			m_Num = LocNum;
			m_X = X;
			m_Y = Y;
			m_MinSkill = minSkill;
		}

		public int ArtID { get { return m_Art; } }
		public int BodyID { get { return m_Body; } }
		public int LocNumber{ get { return m_Num; } }
		public int X{ get{ return m_X; } }
		public int Y{ get{ return m_Y; } }
		public int MinSkill{ get{ return m_MinSkill; } }
	}


	public class PolymorphGump : Gump
	{
		private class PolymorphCategory
		{
			private int m_Num;
			private PolymorphEntry[] m_Entries;

			public PolymorphCategory( int num, params PolymorphEntry[] entries )
			{
				m_Num = num;
				m_Entries = entries;
			}

			public PolymorphEntry[] Entries{ get { return m_Entries; } }
			public int LocNumber{ get { return m_Num; } }
		}

		private static PolymorphCategory[] Categories = new PolymorphCategory[]
			{
			new PolymorphCategory( 1015235, // Animals
				PolymorphEntry.Chicken,
				PolymorphEntry.Dog,
				PolymorphEntry.Eagle,
				PolymorphEntry.Wolf,
				PolymorphEntry.Hind,
				PolymorphEntry.GiantSnake,
				PolymorphEntry.Panther,
				PolymorphEntry.Gorilla,
				PolymorphEntry.BlackBear,
				PolymorphEntry.GrizzlyBear,
				PolymorphEntry.PolarBear,
				PolymorphEntry.Stirge,
				PolymorphEntry.Werewolf,
				PolymorphEntry.WolfMan,
				// PolymorphEntry.Cerberus, // Commented out
				PolymorphEntry.Dragon ),

			new PolymorphCategory( 1015245, // Monsters
				PolymorphEntry.Slime,
				PolymorphEntry.Orc,
				PolymorphEntry.LizardMan,
				PolymorphEntry.Ogre,
				PolymorphEntry.Gargoyle,
				PolymorphEntry.Troll,
				PolymorphEntry.Minotaur,
				PolymorphEntry.NagaGuardian,
				PolymorphEntry.DeathKnight,
				PolymorphEntry.Daemon,
				PolymorphEntry.GiantWidow )
			};

		/// <summary>
		/// Filters polymorph entries based on caster's Magery skill
		/// Forms are cumulative: if caster has skill >= MinSkill, the form is available
		/// Example: Skill 50 shows forms from 0-40, 40-50, and 50-60 ranges
		/// Skill 120 shows ALL forms (all 23 forms)
		/// </summary>
		private static List<PolymorphEntry> GetAvailableEntries( Mobile caster, PolymorphEntry[] entries )
		{
			List<PolymorphEntry> available = new List<PolymorphEntry>();
			double magerySkill = caster.Skills[SkillName.Magery].Value;

			foreach ( PolymorphEntry entry in entries )
			{
				// Forms are cumulative: if skill >= MinSkill, the form is available
				// For MinSkill == 0, always available (skill >= 0 is always true)
				bool canUse = ( magerySkill >= entry.MinSkill );

				if ( canUse )
					available.Add( entry );
			}

			return available;
		}


		private Mobile m_Caster;
		private Item m_Scroll;

		public PolymorphGump( Mobile caster, Item scroll ) : base( 50, 50 )
		{
			m_Caster = caster;
			m_Scroll = scroll;

			int x,y;
			AddPage( 0 );
			AddBackground( 0, 0, 585, 393, 5054 );
			AddBackground( 195, 36, 387, 275, 3000 );
			AddHtmlLocalized( 0, 0, 510, 18, 1015234, false, false ); // <center>Polymorph Selection Menu</center>
			AddHtmlLocalized( 60, 355, 150, 18, 1011036, false, false ); // OKAY
			AddButton( 25, 355, 4005, 4007, 1, GumpButtonType.Reply, 1 );
			AddHtmlLocalized( 320, 355, 150, 18, 1011012, false, false ); // CANCEL
			AddButton( 285, 355, 4005, 4007, 0, GumpButtonType.Reply, 2 );

			y = 35;
			for ( int i=0;i<Categories.Length;i++ )
			{
				PolymorphCategory cat = (PolymorphCategory)Categories[i];
				AddHtmlLocalized( 5, y, 150, 25, cat.LocNumber, true, false );
				AddButton( 155, y, 4005, 4007, 0, GumpButtonType.Page, i+1 );
				y += 25;
			}

			for ( int i=0;i<Categories.Length;i++ )
			{
				PolymorphCategory cat = (PolymorphCategory)Categories[i];
				List<PolymorphEntry> availableEntries = GetAvailableEntries( m_Caster, cat.Entries );
				
				if ( availableEntries.Count == 0 )
					continue;

				AddPage( i+1 );

				for ( int c=0;c<availableEntries.Count;c++ )
				{
					PolymorphEntry entry = availableEntries[c];
					x = 198 + (c%3)*129;
					y = 38 + (c/3)*67;

					// Use custom names for forms without proper localization numbers
					if ( entry.BodyID == 0x5 )
						AddHtml( x, y, 100, 18, "<BASEFONT COLOR=#FFFFFF>Eagle</BASEFONT>", false, false );
					else if ( entry.BodyID == 0x15 )
						AddHtml( x, y, 100, 18, "<BASEFONT COLOR=#FFFFFF>Giant Snake</BASEFONT>", false, false );
					else if ( entry.BodyID == 0xED )
						AddHtml( x, y, 100, 18, "<BASEFONT COLOR=#FFFFFF>Hind</BASEFONT>", false, false );
					else if ( entry.BodyID == 0x6D )
						AddHtml( x, y, 100, 18, "<BASEFONT COLOR=#FFFFFF>Stirge</BASEFONT>", false, false );
					else if ( entry.BodyID == 0x4E )
						AddHtml( x, y, 100, 18, "<BASEFONT COLOR=#FFFFFF>Minotaur</BASEFONT>", false, false );
					else if ( entry.BodyID == 0x42 )
						AddHtml( x, y, 100, 18, "<BASEFONT COLOR=#FFFFFF>Naga Guardian</BASEFONT>", false, false );
					else if ( entry.BodyID == 0x41 )
						AddHtml( x, y, 100, 18, "<BASEFONT COLOR=#FFFFFF>Death Knight</BASEFONT>", false, false );
					else if ( entry.BodyID == 0x8C )
						AddHtml( x, y, 100, 18, "<BASEFONT COLOR=#FFFFFF>Giant Widow</BASEFONT>", false, false );
					else if ( entry.BodyID == 0x22 )
						AddHtml( x, y, 100, 18, "<BASEFONT COLOR=#FFFFFF>Werebear</BASEFONT>", false, false );
					else if ( entry.BodyID == 0x5E )
						AddHtml( x, y, 100, 18, "<BASEFONT COLOR=#FFFFFF>Wolf Man</BASEFONT>", false, false );
					// else if ( entry.BodyID == 0x8D ) // Cerberus - Commented out
					// 	AddHtml( x, y, 100, 18, "<BASEFONT COLOR=#FFFFFF>Cerberus</BASEFONT>", false, false );
					else if ( entry.BodyID == 0x3B )
						AddHtml( x, y, 100, 18, "<BASEFONT COLOR=#FFFFFF>Dragon</BASEFONT>", false, false );
					else
						AddHtmlLocalized( x, y, 100, 18, entry.LocNumber, false, false );
					
					AddItem( x+20, y+25, entry.ArtID );
					AddRadio( x, y+20, 210, 211, false, (c<<8) + i );
				}
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( info.ButtonID == 1 && info.Switches.Length > 0 )
			{
				int cnum = info.Switches[0];
				int cat = cnum%256;
				int ent = cnum>>8;

				if ( cat >= 0 && cat < Categories.Length )
				{
					List<PolymorphEntry> availableEntries = GetAvailableEntries( m_Caster, Categories[cat].Entries );
					if ( ent >= 0 && ent < availableEntries.Count )
					{
						Spell spell = new PolymorphSpell( m_Caster, m_Scroll, availableEntries[ent].BodyID );
						spell.Cast();
					}
				}
			}
		}
	}

	public class NewPolymorphGump : Gump
	{
		// Color scheme constants for skill-based visual differentiation (RGB 24-bit format for HTML)
		// Colors match UO hue codes where specified (hue codes converted to RGB)
		private const int COLOR_BEGINNER = 0xFFFF00;      // Yellow (0-40)
		private const int COLOR_INTERMEDIATE = 0x00FF00;  // Green (40-50)
		private const int COLOR_ADVANCED = 0x0000FF;     // Blue (50-60)
		private const int COLOR_EXPERT = 0xFF8000;       // Orange (60-70)
		private const int COLOR_MASTER = 0xFF0000;       // Red (70-80)
		private const int COLOR_GRANDMASTER = 0xFF69B4;  // Pink (80-90)
		private const int COLOR_LEGENDARY = 0xB8860B;    // Dark Goldenrod (90-100) - Prestigious bronze-gold
		private const int COLOR_EPIC = 0x87CEFA;         // Frozen Blue (hue 1384) (100-110)
		private const int COLOR_MYTHIC = 0x9370DB;       // Special Purple (hue 1796) (110-120)
		
		// Background colors for skill tiers (darker shades)
		private const int BG_COLOR_BEGINNER = 0x0A40;      // Dark green
		private const int BG_COLOR_INTERMEDIATE = 0x0A40;  // Dark blue
		private const int BG_COLOR_ADVANCED = 0x0A40;      // Dark yellow
		private const int BG_COLOR_EXPERT = 0x0A40;        // Dark orange
		private const int BG_COLOR_MASTER = 0x0A40;        // Dark red
		private const int BG_COLOR_GRANDMASTER = 0x0A40;  // Dark purple
		private const int BG_COLOR_LEGENDARY = 0x0A40;     // Dark gold
		private const int BG_COLOR_EPIC = 0x0A40;           // Dark silver
		private const int BG_COLOR_MYTHIC = 0x0A40;        // Dark white

		private static readonly PolymorphEntry[] m_Entries = new PolymorphEntry[]
			{
				PolymorphEntry.Chicken,
				PolymorphEntry.Dog,
				PolymorphEntry.Slime,
				PolymorphEntry.Eagle,
				PolymorphEntry.Wolf,
				PolymorphEntry.Hind,
				PolymorphEntry.GiantSnake,
				PolymorphEntry.Panther,
				PolymorphEntry.Gorilla,
				PolymorphEntry.BlackBear,
				PolymorphEntry.GrizzlyBear,
				PolymorphEntry.Orc,
				PolymorphEntry.PolarBear,
				PolymorphEntry.LizardMan,
				PolymorphEntry.Ogre,
				PolymorphEntry.Stirge,
				PolymorphEntry.Gargoyle,
				PolymorphEntry.Troll,
				PolymorphEntry.Minotaur,
				PolymorphEntry.NagaGuardian,
				PolymorphEntry.DeathKnight,
				PolymorphEntry.Werewolf,
				PolymorphEntry.GiantWidow,
				PolymorphEntry.Daemon,
				PolymorphEntry.WolfMan,
				// PolymorphEntry.Cerberus, // Commented out
				PolymorphEntry.Dragon
			};

		private Mobile m_Caster;
		private Item m_Scroll;

		/// <summary>
		/// Gets the text color based on the skill requirement tier
		/// </summary>
		private static int GetSkillTierColor( int minSkill )
		{
			if ( minSkill < 40 )
				return COLOR_BEGINNER;
			else if ( minSkill < 50 )
				return COLOR_INTERMEDIATE;
			else if ( minSkill < 60 )
				return COLOR_ADVANCED;
			else if ( minSkill < 70 )
				return COLOR_EXPERT;
			else if ( minSkill < 80 )
				return COLOR_MASTER;
			else if ( minSkill < 90 )
				return COLOR_GRANDMASTER;
			else if ( minSkill < 100 )
				return COLOR_LEGENDARY;
			else if ( minSkill < 110 )
				return COLOR_EPIC;
			else
				return COLOR_MYTHIC;
		}

		/// <summary>
		/// Gets the background color based on the skill requirement tier
		/// </summary>
		private static int GetSkillTierBackground( int minSkill )
		{
			if ( minSkill < 40 )
				return BG_COLOR_BEGINNER;
			else if ( minSkill < 50 )
				return BG_COLOR_INTERMEDIATE;
			else if ( minSkill < 60 )
				return BG_COLOR_ADVANCED;
			else if ( minSkill < 70 )
				return BG_COLOR_EXPERT;
			else if ( minSkill < 80 )
				return BG_COLOR_MASTER;
			else if ( minSkill < 90 )
				return BG_COLOR_GRANDMASTER;
			else if ( minSkill < 100 )
				return BG_COLOR_LEGENDARY;
			else if ( minSkill < 110 )
				return BG_COLOR_EPIC;
			else
				return BG_COLOR_MYTHIC;
		}

		/// <summary>
		/// Gets the skill tier name for display
		/// </summary>
		private static string GetSkillTierName( int minSkill )
		{
			if ( minSkill < 40 )
				return "Beginner";
			else if ( minSkill < 50 )
				return "Intermediate";
			else if ( minSkill < 60 )
				return "Advanced";
			else if ( minSkill < 70 )
				return "Expert";
			else if ( minSkill < 80 )
				return "Master";
			else if ( minSkill < 90 )
				return "Grandmaster";
			else if ( minSkill < 100 )
				return "Legendary";
			else if ( minSkill < 110 )
				return "Epic";
			else
				return "Mythic";
		}

		/// <summary>
		/// Converts RGB 24-bit color to UO 16-bit hue value for AddHtmlLocalized
		/// </summary>
		private static int ConvertRGBToUOHue( int rgbColor )
		{
			// Extract RGB components
			int r = ( (rgbColor >> 16) & 0xFF );
			int g = ( (rgbColor >> 8) & 0xFF );
			int b = ( rgbColor & 0xFF );

			// Convert to UO 16-bit hue format (5 bits per channel)
			// UO hue format: RRRRR GGGGG BBBBB (15 bits total, 16-bit value)
			int r5 = ( r >> 3 );
			int g5 = ( g >> 3 );
			int b5 = ( b >> 3 );

			// Combine into 16-bit hue value
			int uoHue = (r5 << 10) | (g5 << 5) | b5;

			// Ensure it's within valid UO hue range (0-0x7FFF)
			return uoHue & 0x7FFF;
		}

		/// <summary>
		/// Gets custom X offset for large images that may overflow the container
		/// Returns adjusted offset to ensure images fit within 220x60px container
		/// </summary>
		private static int GetImageOffsetX( PolymorphEntry entry )
		{
			// Cerberus (BodyID 0x8D) - large image needs smaller offset to fit
			// if ( entry.BodyID == 0x8D ) // Cerberus - Commented out
			// 	return 0; // Center the image (no offset)
			
			// Add other large images here if they overflow
			// Dragon (BodyID 0x3B) might also need adjustment if it overflows
			// if ( entry.BodyID == 0x3B ) // Dragon
			//     return 0;
			
			// Default offset for normal-sized images
			return entry.X;
		}

		/// <summary>
		/// Gets custom Y offset for large images that may overflow the container
		/// Returns adjusted offset to ensure images fit within 220x60px container
		/// </summary>
		private static int GetImageOffsetY( PolymorphEntry entry )
		{
			// Cerberus (BodyID 0x8D) - large image needs smaller offset to fit
			// if ( entry.BodyID == 0x8D ) // Cerberus - Commented out
			// 	return 0; // Center the image (no offset)
			
			// Add other large images here if they overflow
			// Dragon (BodyID 0x3B) might also need adjustment if it overflows
			// if ( entry.BodyID == 0x3B ) // Dragon
			//     return 0;
			
			// Default offset for normal-sized images
			return entry.Y;
		}


		/// <summary>
		/// Filters polymorph entries based on caster's Magery skill
		/// Forms are cumulative: if caster has skill >= MinSkill, the form is available
		/// Example: Skill 50 shows forms from 0-40, 40-50, and 50-60 ranges
		/// </summary>
		private static List<PolymorphEntry> GetAvailableEntries( Mobile caster, PolymorphEntry[] entries )
		{
			List<PolymorphEntry> available = new List<PolymorphEntry>();
			double magerySkill = caster.Skills[SkillName.Magery].Value;

			foreach ( PolymorphEntry entry in entries )
			{
				// Forms are cumulative: if skill >= MinSkill, the form is available
				// For MinSkill == 0, always available (skill >= 0 is always true)
				bool canUse = ( magerySkill >= entry.MinSkill );

				if ( canUse )
					available.Add( entry );
			}

			return available;
		}

		public NewPolymorphGump( Mobile caster, Item scroll ) : base( 0, 0 )
		{
			m_Caster = caster;
			m_Scroll = scroll;

			List<PolymorphEntry> availableEntries = GetAvailableEntries( m_Caster, m_Entries );

			AddPage( 0 );

			// Enhanced background with richer visual design (720x570 size - +10px height)
			AddBackground( 0, 0, 720, 570, 0x13BE );
			AddImageTiled( 10, 10, 700, 20, 0xA40 );
			AddImageTiled( 10, 40, 700, 490, 0xA40 );
			AddImageTiled( 10, 540, 700, 20, 0xA40 );
			AddAlphaRegion( 10, 10, 700, 550 );

			// Enhanced header with available forms indicator (improved margins)
			AddHtmlLocalized( 20, 15, 680, 20, 1015234, 0x7FFF, false, false ); // <center>Polymorph Selection Menu</center>
			AddHtml( 20, 38, 680, 20, String.Format( "<CENTER><BASEFONT COLOR=#00FF00>Available Forms: {0}/{1}</BASEFONT></CENTER>", availableEntries.Count, m_Entries.Length ), false, false );

			// Cancel button on top right (better margin from edge, aligned with header, -30px X axis)
			AddButton( 620, 15, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 655, 17, 60, 20, 1060051, 0x7FFF, false, false ); // CANCEL

			for ( int i = 0; i < availableEntries.Count; i++ )
			{
				PolymorphEntry entry = availableEntries[i];

				// 3 columns × 6 rows = 18 items per page (with 700x500 size)
				int page = i / 18 + 1;
				int pos = i % 18;

				if ( pos == 0 )
				{
					AddPage( page );

					// Back button on bottom left (better margin from edge, -10px Y axis)
					if ( page > 1 )
					{
						AddButton( 20, 535, 0xFAE, 0xFB0, 0, GumpButtonType.Page, page - 1 );
						AddHtmlLocalized( 55, 537, 60, 20, 1011393, 0x7FFF, false, false ); // Back
					}

					// Next button on bottom right (better margin from edge, +10px X axis, -10px Y axis)
					int totalPages = ( availableEntries.Count + 17 ) / 18; // Calculate total pages
					if ( page < totalPages )
					{
						AddButton( 630, 535, 0xFA5, 0xFA7, 0, GumpButtonType.Page, page + 1 );
						AddHtmlLocalized( 670, 537, 60, 20, 1043353, 0x7FFF, false, false ); // Next
					}
				}

				// Adjust layout for 720x570 gump (3 columns instead of 2)
				// Column spacing: 20px margin, 220px button width, 10px gap = 230px per column
				// Row spacing: 74px per row, starting at 70px from top (better margin from header)
				int x = ( pos % 3 == 0 ) ? 20 : ( pos % 3 == 1 ) ? 250 : 480;
				int y = ( pos / 3 ) * 74 + 70;

				// Get skill-based colors
				int textColor = GetSkillTierColor( entry.MinSkill );
				int bgColor = GetSkillTierBackground( entry.MinSkill );
				string tierName = GetSkillTierName( entry.MinSkill );

				// Enhanced button with skill-based background (220px width for 3 columns)
				AddImageTiled( x, y, 220, 60, bgColor );
				
				// Get custom offsets for large images (prevents overflow)
				int offsetX = GetImageOffsetX( entry );
				int offsetY = GetImageOffsetY( entry );
				AddImageTiledButton( x, y, 0x918, 0x919, i + 1, GumpButtonType.Reply, 0, entry.ArtID, 0x0, offsetX, offsetY );
				
				// Form name with skill-based color
				string formName = "";
				if ( entry.BodyID == 0x5 )
					formName = "Eagle";
				else if ( entry.BodyID == 0x15 )
					formName = "Giant Snake";
				else if ( entry.BodyID == 0xED )
					formName = "Hind";
				else if ( entry.BodyID == 0x6D )
					formName = "Stirge";
				else if ( entry.BodyID == 0x4E )
					formName = "Minotaur";
				else if ( entry.BodyID == 0x42 )
					formName = "Naga Guardian";
				else if ( entry.BodyID == 0x41 )
					formName = "Death Knight";
				else if ( entry.BodyID == 0x8C )
					formName = "Giant Widow";
				else if ( entry.BodyID == 0x22 )
					formName = "Werebear";
				// else if ( entry.BodyID == 0x8D ) // Cerberus - Commented out
				// 	formName = "Cerberus";
				else if ( entry.BodyID == 0x5E )
					formName = "Wolf Man";
				else if ( entry.BodyID == 0x3B )
					formName = "Dragon";
				else
					formName = ""; // Will use localized name

				// Special visual treatment for high-tier forms
				bool isSpecialForm = ( entry.BodyID == 0x3B || entry.BodyID == 0x22 || entry.BodyID == 0x5E || entry.BodyID == 0x8C ); // Removed 0x8D (Cerberus)
				string colorHex = String.Format( "{0:X6}", textColor );
				
				// Form name and level use the same color (colorHex)
				if ( !String.IsNullOrEmpty( formName ) )
				{
					if ( isSpecialForm )
						AddHtml( x + 84, y, 220, 20, String.Format( "<BASEFONT COLOR=#{0}><BIG>{1}</BIG></BASEFONT>", colorHex, formName ), false, false );
					else
						AddHtml( x + 84, y, 220, 20, String.Format( "<BASEFONT COLOR=#{0}>{1}</BASEFONT>", colorHex, formName ), false, false );
				}
				else
				{
					// For localized names, use AddHtmlLocalized with color parameter (converted from RGB to UO hue)
					// Convert RGB to approximate UO hue value for AddHtmlLocalized
					int uoHue = ConvertRGBToUOHue( textColor );
					AddHtmlLocalized( x + 84, y, 220, 20, entry.LocNumber, uoHue, false, false );
				}

				// Skill tier indicator (Level only, same color as form title)
				AddHtml( x + 84, y + 20, 220, 20, String.Format( "<BASEFONT COLOR=#{0}>({1})</BASEFONT>", colorHex, tierName ), false, false );
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int idx = info.ButtonID - 1;

			if ( idx < 0 )
				return;

			List<PolymorphEntry> availableEntries = GetAvailableEntries( m_Caster, m_Entries );

			if ( idx >= availableEntries.Count )
				return;

			Spell spell = new PolymorphSpell( m_Caster, m_Scroll, availableEntries[idx].BodyID );
			spell.Cast();
		}
	}
}
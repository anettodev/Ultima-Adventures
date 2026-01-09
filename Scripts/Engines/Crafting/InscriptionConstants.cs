using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Centralized constants for Inscription crafting system.
	/// Extracted from DefInscription.cs to improve maintainability.
	/// </summary>
	public static class InscriptionConstants
	{
		#region Gump and Menu IDs

		/// <summary>Gump title: "INSCRIPTION MENU"</summary>
		public const int GUMP_TITLE_INSCRIPTION_MENU = 1044009;

		#endregion

		#region Sound Effects

		/// <summary>Sound played when crafting inscriptions (writing sound)</summary>
		public const int SOUND_CRAFT_EFFECT = 0x249;

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

		/// <summary>Skill difference for necromancy scrolls (OSI standard)</summary>
		public const double NECRO_SKILL_DIFFERENCE = 1.0;

		#endregion

		#region Mana Costs by Circle

		/// <summary>Mana cost for 1st circle spells</summary>
		public const int MANA_CIRCLE_1 = 4;

		/// <summary>Mana cost for 2nd circle spells</summary>
		public const int MANA_CIRCLE_2 = 6;

		/// <summary>Mana cost for 3rd circle spells</summary>
		public const int MANA_CIRCLE_3 = 9;

		/// <summary>Mana cost for 4th circle spells</summary>
		public const int MANA_CIRCLE_4 = 11;

		/// <summary>Mana cost for 5th circle spells</summary>
		public const int MANA_CIRCLE_5 = 14;

		/// <summary>Mana cost for 6th circle spells</summary>
		public const int MANA_CIRCLE_6 = 20;

		/// <summary>Mana cost for 7th circle spells</summary>
		public const int MANA_CIRCLE_7 = 40;

		/// <summary>Mana cost for 8th circle spells</summary>
		public const int MANA_CIRCLE_8 = 50;

		#endregion

		#region Skill Requirements by Circle

		// Circle 1 (0)
		public const double CIRCLE_1_MIN_SKILL = 0.0;
		public const double CIRCLE_1_MAX_SKILL = 20.0;

		// Circle 2 (1)
		public const double CIRCLE_2_MIN_SKILL = 10.0;
		public const double CIRCLE_2_MAX_SKILL = 30.0;

		// Circle 3 (2)
		public const double CIRCLE_3_MIN_SKILL = 20.0;
		public const double CIRCLE_3_MAX_SKILL = 40.0;

		// Circle 4 (3)
		public const double CIRCLE_4_MIN_SKILL = 30.0;
		public const double CIRCLE_4_MAX_SKILL = 50.0;

		// Circle 5 (4)
		public const double CIRCLE_5_MIN_SKILL = 40.0;
		public const double CIRCLE_5_MAX_SKILL = 60.0;

		// Circle 6 (5)
		public const double CIRCLE_6_MIN_SKILL = 50.0;
		public const double CIRCLE_6_MAX_SKILL = 70.0;

		// Circle 7 (6)
		public const double CIRCLE_7_MIN_SKILL = 60.0;
		public const double CIRCLE_7_MAX_SKILL = 80.0;

		// Circle 8 (7)
		public const double CIRCLE_8_MIN_SKILL = 70.0;
		public const double CIRCLE_8_MAX_SKILL = 90.0;

		#endregion

		#region Necromancy Spell Mana Costs

		public const int NECRO_ANIMATE_DEAD_MANA = 23;
		public const int NECRO_BLOOD_OATH_MANA = 13;
		public const int NECRO_CORPSE_SKIN_MANA = 11;
		public const int NECRO_CURSE_WEAPON_MANA = 7;
		public const int NECRO_EVIL_OMEN_MANA = 11;
		public const int NECRO_HORRIFIC_BEAST_MANA = 11;
		public const int NECRO_LICH_FORM_MANA = 23;
		public const int NECRO_MIND_ROT_MANA = 17;
		public const int NECRO_PAIN_SPIKE_MANA = 5;
		public const int NECRO_POISON_STRIKE_MANA = 17;
		public const int NECRO_STRANGLE_MANA = 29;
		public const int NECRO_SUMMON_FAMILIAR_MANA = 17;
		public const int NECRO_VAMPIRIC_EMBRACE_MANA = 23;
		public const int NECRO_VENGEFUL_SPIRIT_MANA = 41;
		public const int NECRO_WITHER_MANA = 23;
		public const int NECRO_WRAITH_FORM_MANA = 17;
		public const int NECRO_EXORCISM_MANA = 40;

		#endregion

		#region Necromancy Spell Skill Requirements

		public const double NECRO_ANIMATE_DEAD_SKILL = 39.6;
		public const double NECRO_BLOOD_OATH_SKILL = 19.6;
		public const double NECRO_CORPSE_SKIN_SKILL = 19.6;
		public const double NECRO_CURSE_WEAPON_SKILL = 19.6;
		public const double NECRO_EVIL_OMEN_SKILL = 19.6;
		public const double NECRO_HORRIFIC_BEAST_SKILL = 39.6;
		public const double NECRO_LICH_FORM_SKILL = 69.6;
		public const double NECRO_MIND_ROT_SKILL = 29.6;
		public const double NECRO_PAIN_SPIKE_SKILL = 19.6;
		public const double NECRO_POISON_STRIKE_SKILL = 49.6;
		public const double NECRO_STRANGLE_SKILL = 64.6;
		public const double NECRO_SUMMON_FAMILIAR_SKILL = 29.6;
		public const double NECRO_VAMPIRIC_EMBRACE_SKILL = 98.6;
		public const double NECRO_VENGEFUL_SPIRIT_SKILL = 79.6;
		public const double NECRO_WITHER_SKILL = 59.6;
		public const double NECRO_WRAITH_FORM_SKILL = 79.6;
		public const double NECRO_EXORCISM_SKILL = 79.6;

		#endregion

		#region Non-Scroll Crafting

		// Blank Scrolls
		public const double BLANK_SCROLL_MIN_SKILL = 30.0;
		public const double BLANK_SCROLL_MAX_SKILL = 65.0;
		public const int BLANK_SCROLL_BARK_FRAGMENT_QUANTITY = 1;
		public const int BLANK_SCROLL_LEATHER_QUANTITY = 1;

		// Runebook
		public const double RUNEBOOK_MIN_SKILL = 60.0;
		public const double RUNEBOOK_MAX_SKILL = 90.0;
		public const int RUNEBOOK_BLANK_SCROLL_QUANTITY = 16;
		public const int RUNEBOOK_BEESWAX_QUANTITY = 8;
		public const int RUNEBOOK_RECALL_SCROLL_QUANTITY = 1;
		public const int RUNEBOOK_GATE_SCROLL_QUANTITY = 1;
		public const int RUNEBOOK_RUNE_QUANTITY = 1;
		public const int RUNEBOOK_MANA = 25;

		// Spellbook
		public const double SPELLBOOK_MIN_SKILL = 50.0;
		public const double SPELLBOOK_MAX_SKILL = 80.0;
		public const int SPELLBOOK_BLANK_SCROLL_QUANTITY = 12;
		public const int SPELLBOOK_BEESWAX_QUANTITY = 8;
		public const int SPELLBOOK_LEATHER_QUANTITY = 2;
		public const int SPELLBOOK_MANA = 10;

		// Bulk Order Book (commented out in original)
		public const double BULK_ORDER_BOOK_MIN_SKILL = 65.0;
		public const double BULK_ORDER_BOOK_MAX_SKILL = 115.0;
		public const int BULK_ORDER_BOOK_BLANK_SCROLL_QUANTITY = 10;
		public const int BULK_ORDER_BOOK_BEESWAX_QUANTITY = 5;

		// Necromancer Spellbook (commented out in original)
		public const double NECRO_SPELLBOOK_MIN_SKILL = 50.0;
		public const double NECRO_SPELLBOOK_MAX_SKILL = 126.0;
		public const int NECRO_SPELLBOOK_BLANK_SCROLL_QUANTITY = 10;
		public const int NECRO_SPELLBOOK_BEESWAX_QUANTITY = 5;

		// Song Book (commented out in original)
		public const double SONG_BOOK_MIN_SKILL = 50.0;
		public const double SONG_BOOK_MAX_SKILL = 126.0;
		public const int SONG_BOOK_BLANK_SCROLL_QUANTITY = 10;
		public const int SONG_BOOK_BEESWAX_QUANTITY = 5;

		// Bard Scrolls (commented out in original)
		public const double BARD_SCROLL_MIN_SKILL = 75.0;
		public const double BARD_SCROLL_MAX_SKILL = 95.0;
		public const double BARD_MUSICIANSHIP_MIN_SKILL = 95.0;
		public const double BARD_MUSICIANSHIP_MAX_SKILL = 120.0;
		public const int BARD_SCROLL_BLANK_SCROLL_QUANTITY = 1;
		public const int BARD_SCROLL_LUTE_QUANTITY = 1;

		// Expert Study Books (commented out in original)
		public const double EXPERT_STUDY_BOOK_MIN_SKILL = 65.0;
		public const double EXPERT_STUDY_BOOK_MAX_SKILL = 85.0;
		public const double EXPERT_STUDY_BOOK_SKILL_REQUIREMENT_MIN = 70.0;
		public const double EXPERT_STUDY_BOOK_SKILL_REQUIREMENT_MAX = 80.0;
		public const int EXPERT_STUDY_BOOK_BLANK_SCROLL_QUANTITY = 10;
		public const int EXPERT_STUDY_BOOK_BEESWAX_QUANTITY = 1;
		public const int EXPERT_STUDY_BOOK_LEATHER_QUANTITY = 2;

		#endregion

		#region Reagent Base Cliloc IDs

		/// <summary>Base cliloc ID for reagent names (1044353 + reagent enum)</summary>
		public const int REAGENT_NAME_BASE_CLILOC = 1044353;

		/// <summary>Base cliloc ID for "You don't have [reagent]" messages (1044361 + reagent enum)</summary>
		public const int REAGENT_LACK_BASE_CLILOC = 1044361;

		/// <summary>Base cliloc ID for spell scroll names (1044381 + spell index)</summary>
		public const int SPELL_SCROLL_NAME_BASE_CLILOC = 1044381;

		#endregion

		#region Category Cliloc IDs

		/// <summary>Circle menu cliloc for 1st and 2nd circle: "1st & 2nd Circle"</summary>
		public const int CIRCLE_MENU_1_AND_2 = 1044369;

		/// <summary>Circle menu cliloc for 3rd and 4th circle: "3rd & 4th Circle"</summary>
		public const int CIRCLE_MENU_3_AND_4 = 1044371;

		/// <summary>Circle menu cliloc for 5th and 6th circle: "5th & 6th Circle"</summary>
		public const int CIRCLE_MENU_5_AND_6 = 1044373;

		/// <summary>Circle menu cliloc for 7th and 8th circle: "7th & 8th Circle"</summary>
		public const int CIRCLE_MENU_7_AND_8 = 1044375;

		/// <summary>Cliloc 1044294: "Other Items"</summary>
		public const int CATEGORY_OTHER_ITEMS = 1044294;

		/// <summary>Cliloc 1061677: "Necromancy"</summary>
		public const int CATEGORY_NECROMANCY = 1061677;

		#endregion

		#region Item Cliloc IDs

		/// <summary>Cliloc 1044377: "Blank Scroll"</summary>
		public const int ITEM_BLANK_SCROLL = 1044377;

		/// <summary>Cliloc 1044378: "You don't have enough blank scrolls"</summary>
		public const int ITEM_BLANK_SCROLL_LACK = 1044378;

		/// <summary>Cliloc 1073477: "Bark Fragment"</summary>
		public const int ITEM_BARK_FRAGMENT = 1073477;

		/// <summary>Cliloc 1073478: "You don't have enough bark fragments"</summary>
		public const int ITEM_BARK_FRAGMENT_LACK = 1073478;

		/// <summary>Cliloc 1044462: "Leather"</summary>
		public const int ITEM_LEATHER = 1044462;

		/// <summary>Cliloc 1041267: "Runebook"</summary>
		public const int ITEM_RUNEBOOK = 1041267;

		/// <summary>Cliloc 1025154: "Beeswax"</summary>
		public const int ITEM_BEESWAX = 1025154;

		/// <summary>Cliloc 1044445: "Recall Scroll"</summary>
		public const int ITEM_RECALL_SCROLL = 1044445;

		/// <summary>Cliloc 1044446: "Gate Travel Scroll"</summary>
		public const int ITEM_GATE_TRAVEL_SCROLL = 1044446;

		/// <summary>Cliloc 1027958: "Recall Rune"</summary>
		public const int ITEM_RECALL_RUNE = 1027958;

		/// <summary>Cliloc 1028793: "Bulk Order Book"</summary>
		public const int ITEM_BULK_ORDER_BOOK = 1028793;

		/// <summary>Cliloc 1023834: "Spellbook"</summary>
		public const int ITEM_SPELLBOOK = 1023834;

		/// <summary>Cliloc 1028787: "Necromancer Spellbook"</summary>
		public const int ITEM_NECROMANCER_SPELLBOOK = 1028787;

		/// <summary>Cliloc 1095590: "Song Book"</summary>
		public const int ITEM_SONG_BOOK = 1095590;

		#endregion

		#region Necromancy Spell Base Cliloc

		/// <summary>Base cliloc for necromancy spell names (1060509 + spell index)</summary>
		public const int NECRO_SPELL_NAME_BASE_CLILOC = 1060509;

		/// <summary>Cliloc for generic necromancy reagent failure: "You don't have the required reagents"</summary>
		public const int NECRO_REAGENT_LACK = 501627;

		/// <summary>Base cliloc for item IDs less than 0x4000</summary>
		public const int ITEM_ID_BASE_CLILOC_LOW = 1020000;

		/// <summary>Base cliloc for item IDs 0x4000 and above</summary>
		public const int ITEM_ID_BASE_CLILOC_HIGH = 1078872;

		/// <summary>Threshold for determining which item ID cliloc base to use</summary>
		public const int ITEM_ID_CLILOC_THRESHOLD = 0x4000;

		#endregion
	}
}

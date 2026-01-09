namespace Server.Items
{
    /// <summary>
    /// Centralized constants for DynamicBook mechanics.
    /// Extracted from DynamicBook.cs to improve maintainability.
    /// </summary>
    public static class DynamicBookConstants
    {
        #region Item IDs

        /// <summary>Base book item ID</summary>
        public const int ITEM_ID_BASE = 0x1C11;

        /// <summary>Syth book item ID</summary>
        public const int ITEM_ID_SYTH_BOOK = 0x4CDF;

        /// <summary>Jedi book item ID</summary>
        public const int ITEM_ID_JEDI_BOOK = 0xFFDA;

        /// <summary>Necrotic alchemy book item ID</summary>
        public const int ITEM_ID_NECROTIC = 0x2253;

        #endregion

        #region Weight

        /// <summary>Weight of book</summary>
        public const double WEIGHT_BOOK = 1.0;

        #endregion

        #region Hue Values

        /// <summary>Base hue for random color generation</summary>
        public const int HUE_RANDOM_BASE = 0;

        /// <summary>Necrotic alchemy book hue</summary>
        public const int HUE_NECROTIC = 0x4AA;

        #endregion

        #region Gump Layout - Positions

        /// <summary>Gump X position for Jedi/Syth gumps</summary>
        public const int GUMP_X_POS_SMALL = 25;

        /// <summary>Gump Y position for Jedi/Syth gumps</summary>
        public const int GUMP_Y_POS_SMALL = 25;

        /// <summary>Gump X position for standard book gump</summary>
        public const int GUMP_X_POS_STANDARD = 100;

        /// <summary>Gump Y position for standard book gump</summary>
        public const int GUMP_Y_POS_STANDARD = 100;

        #endregion

        #region Gump Layout - Image IDs

        /// <summary>Background image ID for Jedi/Syth gumps</summary>
        public const int IMAGE_ID_BACKGROUND = 30521;

        /// <summary>Jedi top image ID</summary>
        public const int IMAGE_ID_JEDI_TOP = 11435;

        /// <summary>Jedi bottom image ID</summary>
        public const int IMAGE_ID_JEDI_BOTTOM = 11433;

        /// <summary>Syth top image ID</summary>
        public const int IMAGE_ID_SYTH_TOP = 11428;

        /// <summary>Syth bottom image ID</summary>
        public const int IMAGE_ID_SYTH_BOTTOM = 11426;

        /// <summary>Standard book image ID</summary>
        public const int IMAGE_ID_BOOK = 1261;

        /// <summary>Black background image ID</summary>
        public const int IMAGE_ID_BACKGROUND_BLACK = 1260;

        /// <summary>Picture border image ID</summary>
        public const int IMAGE_ID_PICTURE_BORDER = 1262;

        #endregion

        #region Gump Layout - HTML Positions

        /// <summary>HTML title X position</summary>
        public const int HTML_TITLE_X = 275;

        /// <summary>HTML title Y position</summary>
        public const int HTML_TITLE_Y = 45;

        /// <summary>HTML title width</summary>
        public const int HTML_TITLE_WIDTH = 445;

        /// <summary>HTML title height</summary>
        public const int HTML_TITLE_HEIGHT = 20;

        /// <summary>HTML text X position</summary>
        public const int HTML_TEXT_X = 275;

        /// <summary>HTML text Y position</summary>
        public const int HTML_TEXT_Y = 84;

        /// <summary>HTML text width</summary>
        public const int HTML_TEXT_WIDTH = 445;

        /// <summary>HTML text height</summary>
        public const int HTML_TEXT_HEIGHT = 521;

        /// <summary>HTML standard title X position</summary>
        public const int HTML_STANDARD_TITLE_X = 63;

        /// <summary>HTML standard title Y position</summary>
        public const int HTML_STANDARD_TITLE_Y = 29;

        /// <summary>HTML standard title width</summary>
        public const int HTML_STANDARD_TITLE_WIDTH = 246;

        /// <summary>HTML standard title height</summary>
        public const int HTML_STANDARD_TITLE_HEIGHT = 59;

        /// <summary>HTML standard content X position</summary>
        public const int HTML_STANDARD_CONTENT_X = 63;

        /// <summary>HTML standard content Y position</summary>
        public const int HTML_STANDARD_CONTENT_Y = 95;

        /// <summary>HTML standard content width</summary>
        public const int HTML_STANDARD_CONTENT_WIDTH = 246;

        /// <summary>HTML standard content height</summary>
        public const int HTML_STANDARD_CONTENT_HEIGHT = 272;

        /// <summary>Book cover image X position</summary>
        public const int IMAGE_COVER_X = 379;

        /// <summary>Book cover image Y position</summary>
        public const int IMAGE_COVER_Y = 67;

        #endregion

        #region Sound IDs

        /// <summary>Sound ID for closing Jedi/Syth gumps</summary>
        public const int SOUND_CLOSE_JEDI_SYTH = 0x54D;

        /// <summary>Sound ID for closing standard book gump</summary>
        public const int SOUND_CLOSE_STANDARD = 0x55;

        #endregion

        #region Random Ranges

        /// <summary>Minimum book cover number</summary>
        public const int COVER_MIN = 1;

        /// <summary>Maximum book cover number</summary>
        public const int COVER_MAX = 80;

        /// <summary>Minimum author type for random selection</summary>
        public const int AUTHOR_TYPE_MIN = 0;

        /// <summary>Maximum author type for random selection</summary>
        public const int AUTHOR_TYPE_MAX = 3;

        #endregion

        #region Book Cover Item IDs

        /// <summary>Cover 1: Man Fighting Skeleton</summary>
        public const int COVER_ITEM_ID_1 = 0x4F1;

        /// <summary>Cover 2: Dungeon Door</summary>
        public const int COVER_ITEM_ID_2 = 0x4F2;

        /// <summary>Cover 3: Castle</summary>
        public const int COVER_ITEM_ID_3 = 0x4F3;

        /// <summary>Cover 4: Old Man</summary>
        public const int COVER_ITEM_ID_4 = 0x4F4;

        /// <summary>Cover 5: Sword and Shield</summary>
        public const int COVER_ITEM_ID_5 = 0x4F5;

        /// <summary>Cover 6: Lion with Sword</summary>
        public const int COVER_ITEM_ID_6 = 0x4F6;

        /// <summary>Cover 7: Chalice</summary>
        public const int COVER_ITEM_ID_7 = 0x4F7;

        /// <summary>Cover 8: Two Women</summary>
        public const int COVER_ITEM_ID_8 = 0x4F8;

        /// <summary>Cover 9: Dragon</summary>
        public const int COVER_ITEM_ID_9 = 0x4F9;

        /// <summary>Cover 10: Dragon</summary>
        public const int COVER_ITEM_ID_10 = 0x4FA;

        /// <summary>Cover 11: Dragon</summary>
        public const int COVER_ITEM_ID_11 = 0x4FB;

        /// <summary>Cover 12: Wizard Hat</summary>
        public const int COVER_ITEM_ID_12 = 0x4FC;

        /// <summary>Cover 13: Skeleton Dancing</summary>
        public const int COVER_ITEM_ID_13 = 0x4FD;

        /// <summary>Cover 14: Skull Crown</summary>
        public const int COVER_ITEM_ID_14 = 0x4FE;

        /// <summary>Cover 15: Devil Pitchfork</summary>
        public const int COVER_ITEM_ID_15 = 0x4FF;

        /// <summary>Cover 16: Sun Symbol</summary>
        public const int COVER_ITEM_ID_16 = 0x500;

        /// <summary>Cover 17: Griffon</summary>
        public const int COVER_ITEM_ID_17 = 0x501;

        /// <summary>Cover 18: Unicorn</summary>
        public const int COVER_ITEM_ID_18 = 0x502;

        /// <summary>Cover 19: Mermaid</summary>
        public const int COVER_ITEM_ID_19 = 0x503;

        /// <summary>Cover 20: Merman</summary>
        public const int COVER_ITEM_ID_20 = 0x504;

        /// <summary>Cover 21: Crown</summary>
        public const int COVER_ITEM_ID_21 = 0x505;

        /// <summary>Cover 22: Demon</summary>
        public const int COVER_ITEM_ID_22 = 0x506;

        /// <summary>Cover 23: Hell</summary>
        public const int COVER_ITEM_ID_23 = 0x507;

        /// <summary>Cover 24: Arch Devil</summary>
        public const int COVER_ITEM_ID_24 = 0x514;

        /// <summary>Cover 25: Grim Reaper</summary>
        public const int COVER_ITEM_ID_25 = 0x515;

        /// <summary>Cover 26: Castle</summary>
        public const int COVER_ITEM_ID_26 = 0x516;

        /// <summary>Cover 27: Tombstone</summary>
        public const int COVER_ITEM_ID_27 = 0x517;

        /// <summary>Cover 28: Dragon Crest</summary>
        public const int COVER_ITEM_ID_28 = 0x518;

        /// <summary>Cover 29: Cross</summary>
        public const int COVER_ITEM_ID_29 = 0x519;

        /// <summary>Cover 30: Village</summary>
        public const int COVER_ITEM_ID_30 = 0x51A;

        /// <summary>Cover 31: Knight</summary>
        public const int COVER_ITEM_ID_31 = 0x51B;

        /// <summary>Cover 32: Alchemy</summary>
        public const int COVER_ITEM_ID_32 = 0x51C;

        /// <summary>Cover 33: Symbol Man Magic Dragon</summary>
        public const int COVER_ITEM_ID_33 = 0x51D;

        /// <summary>Cover 34: Throne</summary>
        public const int COVER_ITEM_ID_34 = 0x51E;

        /// <summary>Cover 35: Ship</summary>
        public const int COVER_ITEM_ID_35 = 0x51F;

        /// <summary>Cover 36: Ship with Fish</summary>
        public const int COVER_ITEM_ID_36 = 0x520;

        /// <summary>Cover 37: Bard</summary>
        public const int COVER_ITEM_ID_37 = 0x579;

        /// <summary>Cover 38: Thief</summary>
        public const int COVER_ITEM_ID_38 = 0x57A;

        /// <summary>Cover 39: Witches</summary>
        public const int COVER_ITEM_ID_39 = 0x57B;

        /// <summary>Cover 40: Ship</summary>
        public const int COVER_ITEM_ID_40 = 0x57C;

        /// <summary>Cover 41: Village Map</summary>
        public const int COVER_ITEM_ID_41 = 0x57D;

        /// <summary>Cover 42: World Map</summary>
        public const int COVER_ITEM_ID_42 = 0x57E;

        /// <summary>Cover 43: Dungeon Map</summary>
        public const int COVER_ITEM_ID_43 = 0x57F;

        /// <summary>Cover 44: Devil with 2 Servants</summary>
        public const int COVER_ITEM_ID_44 = 0x580;

        /// <summary>Cover 45: Druid</summary>
        public const int COVER_ITEM_ID_45 = 0x581;

        /// <summary>Cover 46: Star Magic Symbol</summary>
        public const int COVER_ITEM_ID_46 = 0x582;

        /// <summary>Cover 47: Giant</summary>
        public const int COVER_ITEM_ID_47 = 0x583;

        /// <summary>Cover 48: Harpy</summary>
        public const int COVER_ITEM_ID_48 = 0x584;

        /// <summary>Cover 49: Minotaur</summary>
        public const int COVER_ITEM_ID_49 = 0x585;

        /// <summary>Cover 50: Cloud Giant</summary>
        public const int COVER_ITEM_ID_50 = 0x586;

        /// <summary>Cover 51: Skeleton Warrior</summary>
        public const int COVER_ITEM_ID_51 = 0x960;

        /// <summary>Cover 52: Lich</summary>
        public const int COVER_ITEM_ID_52 = 0x961;

        /// <summary>Cover 53: Mind Flayer</summary>
        public const int COVER_ITEM_ID_53 = 0x962;

        /// <summary>Cover 54: Lizard</summary>
        public const int COVER_ITEM_ID_54 = 0x963;

        /// <summary>Cover 55: Mondain</summary>
        public const int COVER_ITEM_ID_55 = 0x521;

        /// <summary>Cover 56: Minax</summary>
        public const int COVER_ITEM_ID_56 = 0x522;

        /// <summary>Cover 57: Serpent Pillar</summary>
        public const int COVER_ITEM_ID_57 = 0x523;

        /// <summary>Cover 58: Gem of Immortality</summary>
        public const int COVER_ITEM_ID_58 = 0x524;

        /// <summary>Cover 59: Wizard Den</summary>
        public const int COVER_ITEM_ID_59 = 0x525;

        /// <summary>Cover 60: Guard</summary>
        public const int COVER_ITEM_ID_60 = 0x526;

        /// <summary>Cover 61: Shadowlords</summary>
        public const int COVER_ITEM_ID_61 = 0x527;

        /// <summary>Cover 62: Gargoyle</summary>
        public const int COVER_ITEM_ID_62 = 0x528;

        /// <summary>Cover 63: Moongate</summary>
        public const int COVER_ITEM_ID_63 = 0x529;

        /// <summary>Cover 64: Elf</summary>
        public const int COVER_ITEM_ID_64 = 0x52A;

        /// <summary>Cover 65: Shipwreck</summary>
        public const int COVER_ITEM_ID_65 = 0x52B;

        /// <summary>Cover 66: Black Demon</summary>
        public const int COVER_ITEM_ID_66 = 0x52C;

        /// <summary>Cover 67: Exodus</summary>
        public const int COVER_ITEM_ID_67 = 0x52D;

        /// <summary>Cover 68: Sea Serpent</summary>
        public const int COVER_ITEM_ID_68 = 0x52E;

        /// <summary>Cover 69: Hydra</summary>
        public const int COVER_ITEM_ID_69 = 0x530;

        /// <summary>Cover 70: Beholder</summary>
        public const int COVER_ITEM_ID_70 = 0x531;

        /// <summary>Cover 71: Flying Castle</summary>
        public const int COVER_ITEM_ID_71 = 0x532;

        /// <summary>Cover 72: Serpent</summary>
        public const int COVER_ITEM_ID_72 = 0x533;

        /// <summary>Cover 73: Ogre</summary>
        public const int COVER_ITEM_ID_73 = 0x534;

        /// <summary>Cover 74: Skeleton Graveyard</summary>
        public const int COVER_ITEM_ID_74 = 0x535;

        /// <summary>Cover 75: Shrine</summary>
        public const int COVER_ITEM_ID_75 = 0x536;

        /// <summary>Cover 76: Volcano</summary>
        public const int COVER_ITEM_ID_76 = 0x537;

        /// <summary>Cover 77: Castle</summary>
        public const int COVER_ITEM_ID_77 = 0x538;

        /// <summary>Cover 78: Dark Knight</summary>
        public const int COVER_ITEM_ID_78 = 0x539;

        /// <summary>Cover 79: Skull Ring</summary>
        public const int COVER_ITEM_ID_79 = 0x53A;

        /// <summary>Cover 80: Serpents of Balance</summary>
        public const int COVER_ITEM_ID_80 = 0x53B;

        #endregion
    }
}


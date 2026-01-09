namespace Server.Items
{
    /// <summary>
    /// Centralized string constants for DDRelic items.
    /// All strings are in Brazilian Portuguese (PT-BR).
    /// Extracted from multiple DDRelic files to improve maintainability and enable localization.
    /// </summary>
    public static class RelicStringConstants
    {
        #region Quality Descriptors (PT-BR)

        /// <summary>
        /// Array of quality descriptor strings used in relic naming.
        /// Index corresponds to random selection (0-18).
        /// </summary>
        public static readonly string[] QUALITY_DESCRIPTORS = new string[]
        {
            "um raro",
            "um bonito",
            "um belo",
            "um excelente",
            "um delicioso",
            "um elegante",
            "um requintado",
            "um fino",
            "um magnífico",
            "um adorável",
            "um majestoso",
            "um maravilhoso",
            "um esplêndido",
            "um maravilhoso",
            "um extraordinário",
            "estranho",
            "estranho",
            "um único",
            "incomum"
        };

        /// <summary>Maximum index for quality descriptor selection</summary>
        public const int QUALITY_DESCRIPTOR_MAX = 18;

        #endregion

        #region Decorative Terms (PT-BR)

        /// <summary>
        /// Array of decorative term strings used in relic naming.
        /// Includes empty string option for no decorative term.
        /// </summary>
        public static readonly string[] DECORATIVE_TERMS = new string[]
        {
            ", decorativo",
            ", ornamental",
            ", cerimonial",
            ""  // Empty string option
        };

        /// <summary>Maximum index for decorative term selection (3 options + empty)</summary>
        public const int DECORATIVE_TERM_MAX = 3;

        /// <summary>Maximum index for decorative term selection with ceremonial option</summary>
        public const int DECORATIVE_TERM_MAX_WITH_CEREMONIAL = 5;

        #endregion

        #region User Messages (PT-BR)

        /// <summary>Message when identifying relic value</summary>
        public const string MSG_IDENTIFY_VALUE = "Isto pode ser identificado para determinar seu valor.";

        /// <summary>Message when item must be in backpack to flip</summary>
        public const string MSG_MUST_BE_IN_PACK = "Isto deve estar em sua mochila para virar.";

        /// <summary>Message for book relic</summary>
        public const string MSG_BOOK_STORY = "Esta é apenas mais uma história, mas pode valer alguma coisa.";

        #endregion

        #region Item Type Names (PT-BR)

        /// <summary>Goblet item type</summary>
        public const string ITEM_TYPE_GOBLET = " cálice";

        /// <summary>Bowl item type</summary>
        public const string ITEM_TYPE_BOWL = " tigela";

        /// <summary>Scepter item type</summary>
        public const string ITEM_TYPE_SCEPTER = " cetro";

        /// <summary>Axe item type</summary>
        public const string ITEM_TYPE_AXE = " machado";

        /// <summary>Sword item type</summary>
        public const string ITEM_TYPE_SWORD = " espada";

        /// <summary>Dagger item type</summary>
        public const string ITEM_TYPE_DAGGER = " adaga";

        /// <summary>Trident item type</summary>
        public const string ITEM_TYPE_TRIDENT = " tridente";

        /// <summary>War hammer item type</summary>
        public const string ITEM_TYPE_WAR_HAMMER = " martelo de guerra";

        /// <summary>Scythe item type</summary>
        public const string ITEM_TYPE_SCYTHE = " foice";

        /// <summary>Pike item type</summary>
        public const string ITEM_TYPE_PIKE = " pique";

        /// <summary>Lance item type</summary>
        public const string ITEM_TYPE_LANCE = " lança";

        /// <summary>Swords (plural) item type</summary>
        public const string ITEM_TYPE_SWORDS = " espadas";

        /// <summary>Vase item type</summary>
        public const string ITEM_TYPE_VASE = " vaso";

        /// <summary>Alchemy flask item type</summary>
        public const string ITEM_TYPE_ALCHEMY_FLASK = "frasco de alquimia";

        /// <summary>Bundle of cloth item type</summary>
        public const string ITEM_TYPE_CLOTH_BUNDLE = " pacote de tecido";

        /// <summary>Necklace item type</summary>
        public const string ITEM_TYPE_NECKLACE = " colar";

        /// <summary>Amulet item type</summary>
        public const string ITEM_TYPE_AMULET = " amuleto";

        /// <summary>Medallion item type</summary>
        public const string ITEM_TYPE_MEDALLION = " medalhão";

        /// <summary>Ring item type</summary>
        public const string ITEM_TYPE_RING = " anel";

        /// <summary>Set of earrings item type</summary>
        public const string ITEM_TYPE_EARRINGS_SET = " conjunto de brincos";

        /// <summary>Pair of earrings item type</summary>
        public const string ITEM_TYPE_EARRINGS_PAIR = " par de brincos";

        /// <summary>Bundle of fur item type</summary>
        public const string ITEM_TYPE_FUR_BUNDLE = " pacote de pele";

        #endregion

        #region Cloth Type Names (PT-BR)

        /// <summary>Silk cloth type</summary>
        public const string CLOTH_TYPE_SILK = "seda ";

        /// <summary>Cotton cloth type</summary>
        public const string CLOTH_TYPE_COTTON = "algodão ";

        /// <summary>Hemp cloth type</summary>
        public const string CLOTH_TYPE_HEMP = "cânhamo ";

        /// <summary>Wool cloth type</summary>
        public const string CLOTH_TYPE_WOOL = "lã ";

        /// <summary>Empty cloth type (no prefix)</summary>
        public const string CLOTH_TYPE_NONE = "";

        #endregion

        #region Fur Type Names (PT-BR)

        /// <summary>Beaver fur type</summary>
        public const string FUR_TYPE_BEAVER = "castor";

        /// <summary>Ermine fur type</summary>
        public const string FUR_TYPE_ERMINE = "arminho";

        /// <summary>Fox fur type</summary>
        public const string FUR_TYPE_FOX = "raposa";

        /// <summary>Marten fur type</summary>
        public const string FUR_TYPE_MARTEN = "marta";

        /// <summary>Mink fur type</summary>
        public const string FUR_TYPE_MINK = "vison";

        /// <summary>Muskrat fur type</summary>
        public const string FUR_TYPE_MUSKRAT = "ratazana";

        /// <summary>Sable fur type</summary>
        public const string FUR_TYPE_SABLE = "zibelina";

        /// <summary>Bear fur type</summary>
        public const string FUR_TYPE_BEAR = "urso";

        /// <summary>Deer fur type</summary>
        public const string FUR_TYPE_DEER = "veado";

        /// <summary>Rabbit fur type</summary>
        public const string FUR_TYPE_RABBIT = "coelho";

        /// <summary>Yeti fur type</summary>
        public const string FUR_TYPE_YETI = "iéti";

        /// <summary>Dire bear fur type</summary>
        public const string FUR_TYPE_DIRE_BEAR = "urso terrível";

        /// <summary>Polar bear fur type</summary>
        public const string FUR_TYPE_POLAR_BEAR = "urso polar";

        /// <summary>Black wolf fur type</summary>
        public const string FUR_TYPE_BLACK_WOLF = "lobo negro";

        /// <summary>Badger fur type</summary>
        public const string FUR_TYPE_BADGER = "texugo";

        /// <summary>Mammoth fur type</summary>
        public const string FUR_TYPE_MAMMOTH = "mamute";

        /// <summary>Mastodon fur type</summary>
        public const string FUR_TYPE_MASTODON = "mastodonte";

        /// <summary>Buffalo fur type</summary>
        public const string FUR_TYPE_BUFFALO = "búfalo";

        /// <summary>Camel fur type</summary>
        public const string FUR_TYPE_CAMEL = "camelo";

        /// <summary>Cheetah fur type</summary>
        public const string FUR_TYPE_CHEETAH = "guepardo";

        /// <summary>Leopard fur type</summary>
        public const string FUR_TYPE_LEOPARD = "leopardo";

        /// <summary>Lion fur type</summary>
        public const string FUR_TYPE_LION = "leão";

        /// <summary>Panther fur type</summary>
        public const string FUR_TYPE_PANTHER = "pantera";

        /// <summary>Lynx fur type</summary>
        public const string FUR_TYPE_LYNX = "lince";

        /// <summary>Cougar fur type</summary>
        public const string FUR_TYPE_COUGAR = "puma";

        /// <summary>Sabretooth tiger fur type</summary>
        public const string FUR_TYPE_SABRETOOTH_TIGER = "tigre dente-de-sabre";

        /// <summary>Tiger fur type</summary>
        public const string FUR_TYPE_TIGER = "tigre";

        /// <summary>Goat fur type</summary>
        public const string FUR_TYPE_GOAT = "cabra";

        /// <summary>Griffin fur type</summary>
        public const string FUR_TYPE_GRIFFIN = "grifo";

        /// <summary>Hippogriff fur type</summary>
        public const string FUR_TYPE_HIPPOGRIFF = "hipogrifo";

        /// <summary>Hyena fur type</summary>
        public const string FUR_TYPE_HYENA = "hiena";

        /// <summary>Jackal fur type</summary>
        public const string FUR_TYPE_JACKAL = "chacal";

        /// <summary>Wolf fur type</summary>
        public const string FUR_TYPE_WOLF = "lobo";

        /// <summary>Otter fur type</summary>
        public const string FUR_TYPE_OTTER = "lontra";

        /// <summary>Kodiak bear fur type</summary>
        public const string FUR_TYPE_KODIAK_BEAR = "urso kodiak";

        /// <summary>Unicorn fur type</summary>
        public const string FUR_TYPE_UNICORN = "unicórnio";

        /// <summary>Pegasus fur type</summary>
        public const string FUR_TYPE_PEGASUS = "pégaso";

        /// <summary>Weasel fur type</summary>
        public const string FUR_TYPE_WEASEL = "doninha";

        /// <summary>Wolverine fur type</summary>
        public const string FUR_TYPE_WOLVERINE = "glutão";

        #endregion

        #region Special Hue Constants

        /// <summary>Hue for white/light colored furs (yeti, polar bear, etc.)</summary>
        public const int HUE_WHITE_FUR = 1150;

        /// <summary>Hue for black/dark colored furs (black wolf, panther, etc.)</summary>
        public const int HUE_BLACK_FUR = 1899;

        #endregion
    }
}


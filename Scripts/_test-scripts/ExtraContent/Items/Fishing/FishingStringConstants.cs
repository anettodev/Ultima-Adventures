namespace Server.Items
{
    /// <summary>
    /// Centralized constants for Fishing items translations and formatting.
    /// Provides PT-BR translations and consistent color formatting for all fishing-related items.
    /// </summary>
    public static class FishingStringConstants
    {
        #region Color Constants

        /// <summary>Cyan color for main properties</summary>
        public const string COLOR_CYAN = "#8be4fc";

        /// <summary>Yellow color for skill requirements and important info</summary>
        public const string COLOR_YELLOW = "#ffe066";

        /// <summary>Orange color for special properties</summary>
        public const string COLOR_ORANGE = "#ff8c42";

        /// <summary>Error message color (red)</summary>
        public const int COLOR_ERROR = 55;

        #endregion

        #region Property Messages (PT-BR)

        /// <summary>Use on high seas property</summary>
        public const string PROP_USE_HIGH_SEAS = "Use em alto mar";

        /// <summary>Scrap iron type property</summary>
        public const string PROP_SCRAP_IRON = "Ferro-velho";

        /// <summary>Squeeze out water to dry property</summary>
        public const string PROP_SQUEEZE_WATER = "Espremer água para secar";

        /// <summary>Exotic fish property</summary>
        public const string PROP_EXOTIC_FISH = "Um peixe exótico";

        /// <summary>Try to squeeze to extract fluid property</summary>
        public const string PROP_SQUEEZE_EXTRACT = "Tente espremer para extrair fluido";

        /// <summary>Needs empty bottle property</summary>
        public const string PROP_NEEDS_BOTTLE = "Precisa de uma garrafa vazia";

        /// <summary>Ancient scroll property</summary>
        public const string PROP_ANCIENT_SCROLL = "[Pergaminho Ancestral]";

        /// <summary>SOS instructions property</summary>
        public const string PROP_SOS_INSTRUCTIONS = "Na localização do naufrágio e use a vara de pescar.";

        /// <summary>Can recover wood boards property</summary>
        public const string PROP_CAN_RECOVER_WOOD = "* É possível recuperar algumas tábuas de madeira *";

        #endregion

        #region Skill Requirement Formatting

        /// <summary>
        /// Formats skill requirement text with yellow color for the skill value
        /// </summary>
        /// <param name="skillValue">The skill value (e.g., 70.0)</param>
        /// <param name="skillName">The skill name (e.g., "Fishing")</param>
        /// <returns>Formatted string with skill in yellow</returns>
        public static string FormatSkillRequirement(double skillValue, string skillName)
        {
            return "Requer " + ItemNameHue.UnifiedItemProps.SetColor(skillValue.ToString("F1") + " " + skillName, COLOR_YELLOW);
        }

        /// <summary>
        /// Formats skill requirement text with yellow color for the skill value
        /// </summary>
        /// <param name="skillValue">The skill value as string (e.g., "70.0")</param>
        /// <param name="skillName">The skill name (e.g., "Fishing")</param>
        /// <returns>Formatted string with skill in yellow</returns>
        public static string FormatSkillRequirement(string skillValue, string skillName)
        {
            return "Requer " + ItemNameHue.UnifiedItemProps.SetColor(skillValue + " " + skillName, COLOR_YELLOW);
        }

        #endregion

        #region Property Formatting Helpers

        /// <summary>
        /// Formats a property with cyan color
        /// </summary>
        /// <param name="text">The property text</param>
        /// <returns>Formatted string with cyan color</returns>
        public static string FormatProperty(string text)
        {
            return ItemNameHue.UnifiedItemProps.SetColor(text, COLOR_CYAN);
        }

        /// <summary>
        /// Formats a property with yellow color
        /// </summary>
        /// <param name="text">The property text</param>
        /// <returns>Formatted string with yellow color</returns>
        public static string FormatInfo(string text)
        {
            return ItemNameHue.UnifiedItemProps.SetColor(text, COLOR_YELLOW);
        }

        /// <summary>
        /// Formats a property with orange color
        /// </summary>
        /// <param name="text">The property text</param>
        /// <returns>Formatted string with orange color</returns>
        public static string FormatPropertyOrange(string text)
        {
            return ItemNameHue.UnifiedItemProps.SetColor(text, COLOR_ORANGE);
        }

        #endregion

        #region Error Messages (PT-BR)

        /// <summary>Net already in use error</summary>
        public const string ERROR_NET_IN_USE = "Alguém já está usando essa rede!";

        /// <summary>Not skilled enough error</summary>
        public const string ERROR_NOT_SKILLED = "Você não tem habilidade suficiente em pescar para usar esta rede.";

        /// <summary>Need to be on boat error</summary>
        public const string ERROR_NEED_BOAT = "Você precisará estar no seu barco para usar esta rede.";

        /// <summary>Need deeper waters error</summary>
        public const string ERROR_NEED_DEEPER_WATER = "Você precisará ir para águas mais profundas para usar esta rede.";

        /// <summary>Where to use net prompt</summary>
        public const string PROMPT_WHERE_USE_NET = "Onde você deseja usar a rede de pesca?";

        /// <summary>Must be in pack error</summary>
        public const string ERROR_MUST_BE_IN_PACK = "O item precisa estar em sua mochila para que você possa usar.";

        /// <summary>Too far from water error</summary>
        public const string ERROR_TOO_FAR_WATER = "Você precisa estar mais perto da água para pescar!";

        /// <summary>Plunge net into sea message</summary>
        public const string MSG_PLUNGE_NET = "Você mergulha a rede no mar";

        /// <summary>Only deep water error</summary>
        public const string ERROR_ONLY_DEEP_WATER = "Você só pode usar esta rede em águas profundas!";

        /// <summary>Need empty bottle error</summary>
        public const string ERROR_NEED_BOTTLE = "Você precisa de uma garrafa vazia para drenar o líquido das algas.";

        /// <summary>Squeeze liquid message</summary>
        public const string MSG_SQUEEZE_LIQUID = "Você espreme o líquido e coloca na garrafa.";

        /// <summary>Cannot extract liquid error</summary>
        public const string ERROR_CANNOT_EXTRACT = "Você não consegue obter nenhum líquido das algas.";

        /// <summary>Extract message from bottle</summary>
        public const string MSG_EXTRACT_MESSAGE = "Você retira a mensagem da garrafa e coloca em sua mochila";

        /// <summary>Must be in pack to read error</summary>
        public const string ERROR_MUST_BE_IN_PACK_READ = "O item precisa estar em sua mochila para que você possa ler";

        /// <summary>Identify item to discover value</summary>
        public const string MSG_IDENTIFY_VALUE = "Identifique o item para descobrir o valor.";

        /// <summary>Must be in pack to flip</summary>
        public const string MSG_MUST_BE_IN_PACK_FLIP = "Isso deve estar na sua mochila para virar.";

        /// <summary>Received gold payment message</summary>
        public const string MSG_RECEIVED_GOLD = "Você recebeu {0} moeda(s) de ouro.";

        /// <summary>You are paid gold message</summary>
        public const string MSG_PAID_GOLD = "Você recebeu {0} moeda(s) de ouro.";

        /// <summary>Select forge to smelt message</summary>
        public const string MSG_SELECT_FORGE = "Selecione a forja para fundir este item.";

        /// <summary>Must be in pack to use error</summary>
        public const string ERROR_MUST_BE_IN_PACK_USE = "Isso deve estar em sua mochila para usar.";

        /// <summary>No idea how to smelt error</summary>
        public const string ERROR_NO_IDEA_SMELT = "Você não tem ideia de como fundir este item!";

        /// <summary>Smelted into ingot message (singular)</summary>
        public const string MSG_SMELTED_INGOT = "Você fundiu o metal enferrujado em um lingote de ferro utilizável!";

        /// <summary>Smelted into ingots message (plural)</summary>
        public const string MSG_SMELTED_INGOTS = "Você fundiu o metal enferrujado em lingotes de ferro utilizáveis!";

        /// <summary>Failed to smelt error</summary>
        public const string ERROR_FAILED_SMELT = "Você falhou ao fundir o metal enferrujado em algo utilizável!";

        /// <summary>Squeeze out water message</summary>
        public const string MSG_SQUEEZE_WATER = "Você espreme a água.";

        /// <summary>Message when salvaging wood from boat</summary>
        public const string MSG_SALVAGE_WOOD = "Você recupera madeira utilizável do barco.";

        #endregion

        #region PearlSkull Messages (PT-BR)

        /// <summary>Skull name prefix</summary>
        public const string SKULL_NAME_PREFIX = "crânio ";

        /// <summary>Liquid descriptor: strange</summary>
        public const string SKULL_LIQUID_STRANGE = "estranho";

        /// <summary>Liquid descriptor: uncommon</summary>
        public const string SKULL_LIQUID_UNCOMMON = "incomum";

        /// <summary>Liquid descriptor: bizarre</summary>
        public const string SKULL_LIQUID_BIZARRE = "bizarro";

        /// <summary>Liquid descriptor: curious</summary>
        public const string SKULL_LIQUID_CURIOUS = "curioso";

        /// <summary>Liquid descriptor: peculiar</summary>
        public const string SKULL_LIQUID_PECULIAR = "peculiar";

        /// <summary>Liquid descriptor: abnormal</summary>
        public const string SKULL_LIQUID_ABNORMAL = "anormal";

        /// <summary>Message when finding pearl in skull</summary>
        public const string MSG_FOUND_PEARL = "Você abre a boca da caveira e encontra uma pérola mística.";

        #endregion

        #region NewFish Messages (PT-BR)

        /// <summary>Fish name: flying fish</summary>
        public const string FISH_NAME_FLYING = "peixe-voador";

        /// <summary>Fish name: anglerfish</summary>
        public const string FISH_NAME_ANGLER = "tamboril";

        /// <summary>Fish name: barb fish</summary>
        public const string FISH_NAME_BARB = "peixe barbo";

        /// <summary>Fish name: barracuda</summary>
        public const string FISH_NAME_BARRACUDA = "barracuda";

        /// <summary>Fish name: carp</summary>
        public const string FISH_NAME_CARP = "carpa";

        /// <summary>Fish name: catalufa</summary>
        public const string FISH_NAME_CATALUFA = "catalufa";

        /// <summary>Fish name: cod</summary>
        public const string FISH_NAME_COD = "bacalhau";

        /// <summary>Fish name: sardine</summary>
        public const string FISH_NAME_SARDINE = "sardinha";

        /// <summary>Fish name: tilapia</summary>
        public const string FISH_NAME_TILAPIA = "tilápia";

        /// <summary>Fish name: fly fish</summary>
        public const string FISH_NAME_FLY = "peixe-mosca";

        /// <summary>Fish name: flounder</summary>
        public const string FISH_NAME_FLOUNDER = "linguado";

        /// <summary>Fish name: grouper</summary>
        public const string FISH_NAME_GROUPER = "garoupa";

        /// <summary>Fish name: gulper</summary>
        public const string FISH_NAME_GULPER = "gulper";

        /// <summary>Fish name: gunnel</summary>
        public const string FISH_NAME_GUNNEL = "gunnel";

        /// <summary>Fish name: hake</summary>
        public const string FISH_NAME_HAKE = "peixe arinca";

        /// <summary>Fish name: whiting</summary>
        public const string FISH_NAME_WHITING = "pescada";

        /// <summary>Fish name: salmon</summary>
        public const string FISH_NAME_SALMON = "salmão";

        /// <summary>Fish name: shark</summary>
        public const string FISH_NAME_SHARK = "tubarão";

        /// <summary>Fish name: trout</summary>
        public const string FISH_NAME_TROUT = "truta";

        /// <summary>Fish name: tuna</summary>
        public const string FISH_NAME_TUNA = "atum";

        /// <summary>Fish name: marlin</summary>
        public const string FISH_NAME_MARLIN = "marlim";

        /// <summary>Fish name: seahorse</summary>
        public const string FISH_NAME_SEAHORSE = "cavalo-marinho";

        /// <summary>Fish name: stingray</summary>
        public const string FISH_NAME_STINGRAY = "arraia";

        /// <summary>Fish name: squid</summary>
        public const string FISH_NAME_SQUID = "lula";

        /// <summary>Fish name: octopus</summary>
        public const string FISH_NAME_OCTOPUS = "polvo";

        /// <summary>Fish name: crab</summary>
        public const string FISH_NAME_CRAB = "caranguejo";

        /// <summary>Fish type: eyed</summary>
        public const string FISH_TYPE_EYED = "eyed";

        /// <summary>Fish type: colors</summary>
        public const string FISH_TYPE_COLORS = "COLORS";

        /// <summary>Fish color suffix: red</summary>
        public const string FISH_COLOR_RED = " vermelho(a)";

        /// <summary>Fish color suffix: blue</summary>
        public const string FISH_COLOR_BLUE = " azul";

        /// <summary>Fish color suffix: green</summary>
        public const string FISH_COLOR_GREEN = " verde";

        /// <summary>Fish color suffix: yellow</summary>
        public const string FISH_COLOR_YELLOW = " amarelo(a)";

        /// <summary>Fish color suffix: orange</summary>
        public const string FISH_COLOR_ORANGE = " laranja";

        /// <summary>Fish color suffix: pink</summary>
        public const string FISH_COLOR_PINK = " rosa";

        /// <summary>Fish color suffix: emerald</summary>
        public const string FISH_COLOR_EMERALD = " esmeralda";

        /// <summary>Fish color suffix: fire</summary>
        public const string FISH_COLOR_FIRE = " de fogo";

        /// <summary>Fish color suffix: cold water</summary>
        public const string FISH_COLOR_COLDWATER = " de água fria ";

        /// <summary>Fish color suffix: poisonous</summary>
        public const string FISH_COLOR_POISONOUS = " venenoso(a)";

        /// <summary>Fish material suffix: copper</summary>
        public const string FISH_MATERIAL_COPPER = " de cobre";

        /// <summary>Fish material suffix: verite</summary>
        public const string FISH_MATERIAL_VERITE = "-verite ";

        /// <summary>Fish material suffix: valorite</summary>
        public const string FISH_MATERIAL_VALORITE = "-valorite ";

        /// <summary>Fish material suffix: agapite</summary>
        public const string FISH_MATERIAL_AGAPITE = "-agapite ";

        /// <summary>Fish material suffix: bronze</summary>
        public const string FISH_MATERIAL_BRONZE = " de bronze";

        /// <summary>Fish material suffix: coppery</summary>
        public const string FISH_MATERIAL_COPPERY = " acobreado(a) ";

        /// <summary>Fish material suffix: golden</summary>
        public const string FISH_MATERIAL_GOLDEN = " dourado(a) ";

        /// <summary>Fish material suffix: black</summary>
        public const string FISH_MATERIAL_BLACK = " negro(a) ";

        /// <summary>Fish material suffix: topaz</summary>
        public const string FISH_MATERIAL_TOPAZ = "-topázio ";

        /// <summary>Fish material suffix: amethyst</summary>
        public const string FISH_MATERIAL_AMETHYST = "-ametista ";

        /// <summary>Fish material suffix: marble</summary>
        public const string FISH_MATERIAL_MARBLE = "de mármore ";

        /// <summary>Fish material suffix: onyx</summary>
        public const string FISH_MATERIAL_ONYX = " de onyx ";

        /// <summary>Fish material suffix: ruby</summary>
        public const string FISH_MATERIAL_RUBY = "-rubi ";

        /// <summary>Fish material suffix: sapphire</summary>
        public const string FISH_MATERIAL_SAPPHIRE = "-safira ";

        /// <summary>Fish material suffix: silver</summary>
        public const string FISH_MATERIAL_SILVER = " de prata ";

        #endregion

        #region Sextant Messages (PT-BR)

        /// <summary>Error when need magic sextant in Underworld</summary>
        public const string ERROR_NEED_MAGIC_SEXTANT = "Você precisará de um sextante mágico para ver as estrelas através do teto da caverna!";

        /// <summary>Coordinate format string</summary>
        public const string MSG_COORDINATE_FORMAT = "{0}° {1}'{2}, {3}° {4}'{5}";

        /// <summary>Error when sextant blocked in Ravendark region</summary>
        public const string ERROR_REGION_RAVENDARK = "Você não pode usar um sextante porque o sol e as estrelas estão bloqueados pela escuridão maligna aqui!";

        /// <summary>Error when sextant blocked in Ranger Outpost</summary>
        public const string ERROR_REGION_RANGER_OUTPOST = "Você não pode usar um sextante porque as nuvens da montanha bloqueiam o céu!";

        /// <summary>Error when sextant blocked in Druid's Sanctuary</summary>
        public const string ERROR_REGION_DRUID_SANCTUARY = "A magia antiga deste santuário vela as estrelas com uma aura mística!";

        /// <summary>Error when sextant doesn't work</summary>
        public const string ERROR_SEXTANT_NOT_WORKING = "O sextante parece não funcionar aqui!";

        /// <summary>World name: Underworld</summary>
        public const string WORLD_NAME_UNDERWORLD = "the Underworld";

        /// <summary>Region name: Ravendark Woods</summary>
        public const string REGION_NAME_RAVENDARK_WOODS = "Ravendark Woods";

        /// <summary>Region name: Village of Ravendark</summary>
        public const string REGION_NAME_VILLAGE_RAVENDARK = "the Village of Ravendark";

        /// <summary>Region name: Ranger Outpost</summary>
        public const string REGION_NAME_RANGER_OUTPOST = "the Ranger Outpost";

        /// <summary>Region name: Druid's Sanctuary</summary>
        public const string REGION_NAME_DRUID_SANCTUARY = "o Santuário Druida";

        #endregion

        #region Value Formatting

        /// <summary>
        /// Formats gold value property
        /// </summary>
        /// <param name="value">The gold value</param>
        /// <returns>Formatted string with value in yellow</returns>
        public static string FormatGoldValue(int value)
        {
            return ItemNameHue.UnifiedItemProps.SetColor("valor: " + value + " moedas de ouro", COLOR_YELLOW);
        }

        #endregion
    }
}


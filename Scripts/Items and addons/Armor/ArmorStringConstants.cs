using System;

namespace Server.Items
{
    /// <summary>
    /// Centralized string constants for armor messages and labels.
    /// All strings are in Brazilian Portuguese (PT-BR).
    /// Extracted from BaseArmor.cs to improve maintainability and enable easier localization.
    /// </summary>
    public static class ArmorStringConstants
    {
        #region Color Constants

        /// <summary>Cyan color for general information (crafter, requirements, etc.)</summary>
        public const string COLOR_CYAN = "#8be4fc";

        /// <summary>Yellow color for quality indicators (exceptional, wear)</summary>
        public const string COLOR_YELLOW = "#ffe066";

        /// <summary>Purple color for rare/artifact properties</summary>
        public const string COLOR_PURPLE = "#d896ff";

        /// <summary>Pink color for magic/casting properties</summary>
        public const string COLOR_PINK = "#ff69b4";

        /// <summary>Blue color for mana properties</summary>
        public const string COLOR_BLUE = "#87ceeb";

        /// <summary>Red color for high values (danger/powerful)</summary>
        public const string COLOR_RED = "#ff6b6b";

        /// <summary>Orange color for medium values (warning/moderate)</summary>
        public const string COLOR_ORANGE = "#ffa500";

        /// <summary>Green color for positive effects/healing</summary>
        public const string COLOR_GREEN = "#90ee90";

        #endregion

        #region Equipment Messages

        /// <summary>Message when scissors require item in backpack</summary>
        public const string MSG_SCISSORS_BACKPACK = "Itens que deseja cortar devem estar na mochila.";

        /// <summary>Message when scissors cannot be used on item</summary>
        public const string MSG_SCISSORS_INVALID = "Tesouras não podem ser usadas nisso.";

        #endregion

        #region Race/Gender Restriction Messages

        /// <summary>Message when only elves can use an item</summary>
        public const string MSG_ELVES_ONLY = "Apenas Elfos podem usar isto.";

        /// <summary>Format string for race-specific restrictions</summary>
        public const string MSG_RACE_ONLY_FORMAT = "Apenas {0} podem usar isto.";

        /// <summary>Message when only females can wear an item</summary>
        public const string MSG_FEMALES_ONLY = "Apenas mulheres podem vestir isto.";

        /// <summary>Message when only males can wear an item</summary>
        public const string MSG_MALES_ONLY = "Apenas homens podem vestir isto.";

        /// <summary>Generic message when character cannot wear item</summary>
        public const string MSG_CANNOT_WEAR = "Você não pode vestir isto.";

        #endregion

        #region Stat Requirement Messages

        /// <summary>Message when character lacks sufficient dexterity</summary>
        public const string MSG_INSUFFICIENT_DEX = "Você não tem destreza suficiente.";

        /// <summary>Message when character lacks sufficient strength</summary>
        public const string MSG_INSUFFICIENT_STR = "Você não é forte o suficiente.";

        /// <summary>Message when character lacks sufficient intelligence</summary>
        public const string MSG_INSUFFICIENT_INT = "Você não é inteligente o suficiente.";

        #endregion

        #region Cowl/Hat Color Change Messages

        /// <summary>Hint message for changing cowl color</summary>
        public const string MSG_COWL_COLOR_HINT = "Você pode clicar duas vezes para mudar a cor.";

        /// <summary>Message when must be wearing item to change color</summary>
        public const string MSG_MUST_WEAR_TO_CHANGE = "Você deve estar usando isto para mudar a cor.";

        /// <summary>Prompt for selecting item to match color</summary>
        public const string MSG_MATCH_COLOR_PROMPT = "Qual item vestido você quer combinar a cor?";

        /// <summary>Message when can only match equipped items</summary>
        public const string MSG_MATCH_EQUIPPED_ONLY = "Você só pode combinar cores de certos itens equipados.";

        /// <summary>Success message after changing color</summary>
        public const string MSG_COLOR_CHANGED = "Você muda a cor para combinar com o item.";

        /// <summary>Message when selected item has no distinct color</summary>
        public const string MSG_ITEM_NEEDS_COLOR = "Itens selecionados devem ter cor distinta.";

        /// <summary>Message when can only match distinct colored items</summary>
        public const string MSG_MATCH_DISTINCT_COLORS = "Você só pode combinar cores de itens equipados com cores distintas.";

        #endregion

        #region Balance Challenge System Messages

        /// <summary>Challenge prompt for good-aligned characters</summary>
        public const string MSG_CHALLENGE_HONOR = "A honra de quem você desafiará?";

        /// <summary>Challenge prompt for evil-aligned characters</summary>
        public const string MSG_CHALLENGE_HUMILIATE = "Qual humano patético você humilhará?";

        /// <summary>Message when challenge is on cooldown</summary>
        public const string MSG_CHALLENGE_COOLDOWN = "Você ainda está tirando sua luva, tente novamente.";

        /// <summary>Message when not pledged to balance</summary>
        public const string MSG_CHALLENGE_NOT_PLEDGED = "Apenas devotos ao equilíbrio podem desafiar.";

        /// <summary>Message when target is too far</summary>
        public const string MSG_TARGET_TOO_FAR = "Ele está muito longe!";

        /// <summary>Message when targeting self</summary>
        public const string MSG_CHALLENGE_SELF = "Quão ridículo você se sente! Lembre-se de parar com isso.";

        /// <summary>Message when target is unworthy of challenge</summary>
        public const string MSG_TARGET_UNWORTHY = "Este não é digno de um desafio.";

        /// <summary>Message when lacking status to challenge champion</summary>
        public const string MSG_INSUFFICIENT_STATUS = "Você não tem status para desafiar um campeão.";

        /// <summary>Message when target is beneath challenger</summary>
        public const string MSG_TARGET_BENEATH = "Esta pessoa está abaixo de você.";

        /// <summary>Message when cannot challenge in current region</summary>
        public const string MSG_CHALLENGE_INVALID_REGION = "Não aqui...";

        /// <summary>Message when glove throw misses (attacker)</summary>
        public const string MSG_CHALLENGE_MISS_FROM = "Você joga uma luva no rosto do oponente, mas erra!";

        /// <summary>Message when glove throw misses (defender)</summary>
        public const string MSG_CHALLENGE_MISS_TO = "Uma luva passa raspando em seu rosto!";

        /// <summary>Message when glove connects (attacker)</summary>
        public const string MSG_CHALLENGE_HIT_FROM = "Sua luva acerta o rosto dele!";

        /// <summary>Message when glove connects (defender)</summary>
        public const string MSG_CHALLENGE_HIT_TO = "Uma luva acerta seu rosto!";

        /// <summary>Message for good-aligned victory</summary>
        public const string MSG_CHALLENGE_GOOD_WIN = "Você não acredita que tal ser vil pode andar ereto - você o desonra!";

        /// <summary>Message for evil-aligned victory</summary>
        public const string MSG_CHALLENGE_EVIL_WIN = "Este era fraco - você o humilha completamente.";

        /// <summary>Format string for being humiliated</summary>
        public const string MSG_CHALLENGE_HUMILIATED_FORMAT = "Você foi humilhado por {0}!";

        /// <summary>Message when dethroned as champion</summary>
        public const string MSG_CHAMPION_DETHRONED = "Você não é mais o campeão do Equilíbrio.";

        /// <summary>Format string for being found wanting</summary>
        public const string MSG_CHALLENGE_FOUND_WANTING_FORMAT = "Você foi achado em falta por {0}!";

        /// <summary>Format string for humiliating opponent</summary>
        public const string MSG_HUMILIATED_FORMAT = "Você foi humilhado por {0}!";

        /// <summary>Format string for opponent found wanting</summary>
        public const string MSG_FOUND_WANTING_FORMAT = "Você foi achado em falta por {0}!";

        /// <summary>Message when glove snags on finger</summary>
        public const string MSG_GLOVE_SNAGGED = "Sua luva enrosca num dedo, tente novamente.";

        /// <summary>Message when must pledge to balance before challenging</summary>
        public const string MSG_MUST_PLEDGE_BALANCE = "Você deve se devotar ao equilíbrio antes de desafiar.";

        /// <summary>Message when targeting same faction</summary>
        public const string MSG_SAME_SIDE = "Esta pessoa luta pelo mesmo lado que você!";

        #endregion

        #region Property Labels

        /// <summary>Label for exceptional quality</summary>
        public const string LABEL_EXCEPTIONAL = "Excepcional";

        /// <summary>Format string for crafted by message</summary>
        public const string LABEL_CRAFTED_BY_FORMAT = "criado por {0}";

        /// <summary>Label indicating item is considered armor</summary>
        public const string LABEL_CONSIDERED_ARMOR = "Este item é considerado como armadura";

        /// <summary>Format string for durability display</summary>
        public const string FORMAT_DURABILITY = "{0}\t{1}";

        /// <summary>Format string for wear and tear display</summary>
        public const string LABEL_WEAR = "Desgaste: {0}%";

        /// <summary>Label for armor rating</summary>
        public const string LABEL_ARMOR_RATING = "Classificação de Armadura {0}";

        /// <summary>Label for meditation allowed</summary>
        public const string LABEL_MEDITATION_ALLOWED = "Meditação: Permitida";

        /// <summary>Label for meditation hindered</summary>
        public const string LABEL_MEDITATION_HINDERED = "Meditação: Dificultada";

        /// <summary>Label for meditation severely hindered</summary>
        public const string LABEL_MEDITATION_SEVERELY_HINDERED = "Meditação: Severamente Dificultada";

        #endregion

        #region Magic Property Labels

        /// <summary>Format string for Faster Cast Recovery</summary>
        public const string LABEL_FASTER_CAST_RECOVERY = "Recuperação de Conjuração Mais Rápida {0}";

        /// <summary>Format string for Faster Casting</summary>
        public const string LABEL_FASTER_CASTING = "Conjuração Mais Rápida {0}";

        /// <summary>Format string for Lower Mana Cost</summary>
        public const string LABEL_LOWER_MANA_COST = "Custo de Mana Reduzido {0}%";

        /// <summary>Format string for Lower Reagent Cost</summary>
        public const string LABEL_LOWER_REAGENT_COST = "Custo de Reagente Reduzido {0}%";

        /// <summary>Label for Mage Armor</summary>
        public const string LABEL_MAGE_ARMOR = "Armadura de Mago";

        /// <summary>Label for Spell Channeling</summary>
        public const string LABEL_SPELL_CHANNELING = "Canalização de Magia";

        /// <summary>Format string for Mana Increase</summary>
        public const string LABEL_MANA_INCREASE = "Aumento de Mana {0}";

        /// <summary>Format string for Mana Regeneration</summary>
        public const string LABEL_MANA_REGENERATION = "Regeneração de Mana {0}";

        #endregion

        #region Combat Dodge Messages

        /// <summary>Message when you dodge an attack</summary>
        public const string MSG_YOU_DODGE = "Você se esquiva do ataque!";

        /// <summary>Message when opponent dodges your attack</summary>
        public const string MSG_OPPONENT_DODGES = "Seu oponente se esquiva!";

        /// <summary>Message when you dodge and counter</summary>
        public const string MSG_DODGE_AND_COUNTER = "Você se esquiva e contra-ataca!";

        #endregion

        #region Format Strings

        /// <summary>Format for artifact rarity display</summary>
        public const string FORMAT_ARTIFACT_RARITY = "raridade de artefato {0}";

        #endregion
    }
}

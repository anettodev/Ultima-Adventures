using System;

namespace Server.Items
{
    /// <summary>
    /// Centralized string constants for Guild Crafting system messages and labels.
    /// All strings are in Brazilian Portuguese (PT-BR).
    /// Extracted from multiple files to improve maintainability and support internationalization.
    /// </summary>
    public static class GuildCraftingStringConstants
    {
        #region Guild Tool Names

        public const string NAME_GUILD_HAMMER = "Martelo de Ferraria Extraordinário";
        public const string NAME_GUILD_CARPENTRY = "Ferramentas de Carpintaria Extraordinárias";
        public const string NAME_GUILD_TINKERING = "Ferramentas de Funilaria Extraordinárias";
        public const string NAME_GUILD_FLETCHING = "Ferramentas de Flecharia Extraordinárias";
        public const string NAME_GUILD_SEWING = "Kit de Costura Extraordinário";
        public const string NAME_GUILD_SCRIBE = "Caneta de Escriba Extraordinária";

        #endregion

        #region Guild Requirement Messages

        public const string MSG_GUILD_BLACKSMITHS_ONLY =
            "Apenas membros da Guilda de Ferreiros podem usar isto, ou aqueles com habilidades anciãs.";

        public const string MSG_GUILD_CARPENTERS_ONLY =
            "Apenas membros da Guilda de Carpinteiros podem usar isto!";

        public const string MSG_GUILD_TINKERS_ONLY =
            "Apenas membros da Guilda de Funileiros podem usar isto!";

        public const string MSG_GUILD_ARCHERS_ONLY =
            "Apenas membros da Guilda de Arqueiros podem usar isto!";

        public const string MSG_GUILD_TAILORS_ONLY =
            "Apenas membros da Guilda de Alfaiates podem usar isto!";

        public const string MSG_GUILD_LIBRARIANS_ONLY =
            "Apenas membros da Guilda de Bibliotecários podem usar isto, ou aqueles com habilidades anciãs.";

        #endregion

        #region Skill Requirement Messages

        public const string MSG_REQUIRES_MASTER_BLACKSMITH =
            "Apenas um mestre ferreiro pode usar isto!";

        public const string MSG_REQUIRES_MASTER_CARPENTER =
            "Apenas um mestre carpinteiro pode usar isto!";

        public const string MSG_REQUIRES_MASTER_TINKER =
            "Apenas um mestre funileiro pode usar isto!";

        public const string MSG_REQUIRES_MASTER_FLETCHER =
            "Apenas um mestre flecheiro pode usar isto!";

        public const string MSG_REQUIRES_MASTER_TAILOR =
            "Apenas um mestre alfaiate pode usar isto!";

        public const string MSG_REQUIRES_MASTER_SCRIBE =
            "Apenas um mestre escriba pode usar isto!";

        #endregion

        #region Location Requirement Messages

        public const string MSG_REQUIRES_SMITHING_LOCATION =
            "Você precisa estar perto de um mestre da guilda de ferraria, ou uma loja de ferraria que você possui, para usar isto!";

        public const string MSG_REQUIRES_CARPENTRY_LOCATION =
            "Você precisa estar perto de um mestre da guilda de carpintaria, ou uma loja de carpintaria que você possui, para usar isto!";

        public const string MSG_REQUIRES_TINKER_LOCATION =
            "Você precisa estar perto de um mestre da guilda de funilaria, ou uma loja de funilaria que você possui, para usar isto!";

        public const string MSG_REQUIRES_BOWYER_LOCATION =
            "Você precisa estar perto de um mestre da guilda de arco e flecha, ou uma loja de arco e flecha que você possui, para usar isto!";

        public const string MSG_REQUIRES_TAILOR_LOCATION =
            "Você precisa estar perto de um mestre da guilda de alfaiataria, ou uma loja de alfaiataria que você possui, para usar isto!";

        public const string MSG_REQUIRES_LIBRARIAN_LOCATION =
            "Você precisa estar perto de um mestre da guilda de bibliotecários, ou uma loja de inscrição que você possui, para usar isto!";

        #endregion

        #region Target Selection Messages

        public const string MSG_SELECT_METAL_EQUIPMENT =
            "Selecione o equipamento de metal que você gostaria de aprimorar...";

        public const string MSG_SELECT_WOODEN_EQUIPMENT =
            "Selecione o equipamento de madeira que você gostaria de aprimorar...";

        public const string MSG_SELECT_JEWELRY =
            "Selecione a joia que você gostaria de aprimorar...";

        public const string MSG_SELECT_RANGED_WEAPON =
            "Selecione o arco ou besta que você gostaria de aprimorar...";

        public const string MSG_SELECT_CLOTHING =
            "Selecione a roupa que você gostaria de aprimorar...";

        public const string MSG_SELECT_BOOK =
            "Selecione o livro que você gostaria de aprimorar...";

        #endregion

        #region Error Messages

        public const string MSG_CANNOT_ENHANCE_ITEM =
            "Você não pode aprimorar este item!";

        public const string MSG_CANNOT_ENHANCE_LEGENDARY =
            "Você não pode aprimorar artefatos lendários!";

        public const string MSG_MAGIC_BOOTS_BLOCKED =
            "A magia nestas botas impede que sejam aprimoradas.";

        public const string MSG_ITEM_NOT_ENHANCEABLE =
            "Isto não pode ser aprimorado.";

        public const string MSG_MAX_ENHANCEMENT_REACHED =
            "Esta peça de equipamento não pode ser aprimorada mais.";

        public const string MSG_ATTRIBUTE_MAXED =
            "Esta peça de equipamento não pode ser aprimorada com isso mais.";

        #endregion

        #region Gold Transaction Messages

        public const string MSG_ADMIN_GOLD_BYPASS_FORMAT =
            "{0} de ouro teriam sido retirados do seu banco se você não fosse um administrador.";

        public const string MSG_PURCHASE_FROM_BANK_FORMAT =
            "O total de sua compra é {0} de ouro, que foi retirado de sua conta bancária.";

        public const string MSG_PURCHASE_COST_FORMAT =
            "O total de sua compra é {0} de ouro.";

        #endregion

        #region UI Labels - Enhancement Gump

        public const string LABEL_TITLE = "Aprimoramento de Equipamento";
        public const string LABEL_ATTRIBUTES = "Atributos";
        public const string LABEL_GOLD = "Ouro";
        public const string LABEL_USE = "Usar";

        #endregion

        #region Speech Gump

        public const string SPEECH_GUMP_TITLE = "Aprimorando Itens";
        public const string SPEECH_TOPIC_ENHANCE = "Enhance";
        public const string SPEECH_TOPIC_ENHANCE_JEWELS = "EnhanceJewels";

        #endregion

        #region Attribute Descriptions (PT-BR)

        // Combat Attributes
        public const string ATTR_SPELL_CHANNELING = "Canalização de Feitiços";
        public const string ATTR_DEFENSE_CHANCE = "Aumento de Chance de Defesa";
        public const string ATTR_REFLECT_PHYSICAL = "Refletir Dano Físico";
        public const string ATTR_HIT_CHANCE = "Aumento de Chance de Acerto";
        public const string ATTR_LOWER_REQUIREMENTS = "Requisitos Menores";
        public const string ATTR_SELF_REPAIR = "Auto Reparação";
        public const string ATTR_MAGE_ARMOR = "Armadura de Mago";

        // Regeneration
        public const string ATTR_REGEN_HITS = "Regeneração de Pontos de Vida";
        public const string ATTR_REGEN_STAM = "Regeneração de Stamina";
        public const string ATTR_REGEN_MANA = "Regeneração de Mana";

        // Vision
        public const string ATTR_NIGHT_SIGHT = "Visão Noturna";

        // Stat Bonuses
        public const string ATTR_BONUS_HITS = "Aumento de Pontos de Vida";
        public const string ATTR_BONUS_STAM = "Aumento de Stamina";
        public const string ATTR_BONUS_MANA = "Aumento de Mana";
        public const string ATTR_BONUS_STR = "Bônus de Força";
        public const string ATTR_BONUS_DEX = "Bônus de Destreza";
        public const string ATTR_BONUS_INT = "Bônus de Inteligência";

        // Magic
        public const string ATTR_LOWER_MANA_COST = "Custo Menor de Mana";
        public const string ATTR_LOWER_REG_COST = "Custo Menor de Reagentes";
        public const string ATTR_ENHANCE_POTIONS = "Aprimorar Poções";
        public const string ATTR_FASTER_CASTING = "Conjuração Mais Rápida";
        public const string ATTR_FASTER_CAST_RECOVERY = "Recuperação de Conjuração Mais Rápida";
        public const string ATTR_SPELL_DAMAGE = "Aumento de Dano Mágico";

        // Misc
        public const string ATTR_LUCK = "Sorte";

        // Resistances - Armor
        public const string ATTR_PHYSICAL_RESIST = "Resistência Física";
        public const string ATTR_FIRE_RESIST = "Resistência ao Fogo";
        public const string ATTR_COLD_RESIST = "Resistência ao Frio";
        public const string ATTR_POISON_RESIST = "Resistência ao Veneno";
        public const string ATTR_ENERGY_RESIST = "Resistência à Energia";

        // Weapon - Area Effects
        public const string ATTR_HIT_PHYSICAL_AREA = "Acerto Área Física";
        public const string ATTR_HIT_FIRE_AREA = "Acerto Área de Fogo";
        public const string ATTR_HIT_COLD_AREA = "Acerto Área de Frio";
        public const string ATTR_HIT_POISON_AREA = "Acerto Área de Veneno";
        public const string ATTR_HIT_ENERGY_AREA = "Acerto Área de Energia";

        // Weapon - Spell Effects
        public const string ATTR_HIT_MAGIC_ARROW = "Acerto Flecha Mágica";
        public const string ATTR_HIT_HARM = "Acerto Malefício";
        public const string ATTR_HIT_FIREBALL = "Acerto Bola de Fogo";
        public const string ATTR_HIT_LIGHTNING = "Acerto Relâmpago";

        // Weapon - Special
        public const string ATTR_USE_BEST_SKILL = "Usar Melhor Habilidade de Arma";
        public const string ATTR_MAGE_WEAPON = "Arma de Mago";
        public const string ATTR_WEAPON_DAMAGE = "Aumento de Dano";
        public const string ATTR_WEAPON_SPEED = "Aumento de Velocidade de Ataque";

        // Weapon - On Hit Effects
        public const string ATTR_HIT_DISPEL = "Acerto Dissipar";
        public const string ATTR_HIT_LIFE_LEECH = "Acerto Drenar Vida";
        public const string ATTR_HIT_LOWER_ATTACK = "Acerto Diminuir Ataque";
        public const string ATTR_HIT_LOWER_DEFENSE = "Acerto Diminuir Defesa";
        public const string ATTR_HIT_MANA_LEECH = "Acerto Drenar Mana";
        public const string ATTR_HIT_STAMINA_LEECH = "Acerto Drenar Stamina";

        #endregion
    }
}

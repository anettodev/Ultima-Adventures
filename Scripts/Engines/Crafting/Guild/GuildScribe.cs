using System;
using Server;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
    /// <summary>
    /// Extraordinary Scribes Pen for enhancing spellbooks.
    /// Requires Librarians Guild membership (or elder skill 110+) and skill 90+.
    /// </summary>
    public class GuildScribe : BaseGuildTool
    {
        #region Properties

        protected override NpcGuild RequiredGuild { get { return NpcGuild.LibrariansGuild; } }
        protected override SkillName RequiredSkill { get { return SkillName.Inscribe; } }
        protected override double MinimumSkillRequired { get { return GuildCraftingConstants.MIN_SKILL_REQUIRED; } }
        protected override bool AllowElderSkillBypass { get { return true; } }
        protected override Type GuildmasterType { get { return typeof(LibrarianGuildmaster); } }
        protected override Type ShoppeType { get { return typeof(LibrarianShoppe); } }
        protected override string SelectionPrompt { get { return GuildCraftingStringConstants.MSG_SELECT_BOOK; } }
        protected override string GuildRequirementMessage { get { return GuildCraftingStringConstants.MSG_GUILD_LIBRARIANS_ONLY; } }
        protected override string SkillRequirementMessage { get { return GuildCraftingStringConstants.MSG_REQUIRES_MASTER_SCRIBE; } }
        protected override string LocationRequirementMessage { get { return GuildCraftingStringConstants.MSG_REQUIRES_LIBRARIAN_LOCATION; } }

        // Note: Scribe guild doesn't have a specific sound in the original - using default
        public override int EnhancementSoundEffect { get { return GuildCraftingConstants.SOUND_CARPENTRY; } }

        #endregion

        #region Constructor

        [Constructable]
        public GuildScribe()
            : base(GuildCraftingConstants.ITEMID_SCRIBE_PEN,
                   GuildCraftingConstants.TOOL_WEIGHT_VERY_LIGHT,
                   GuildCraftingConstants.EXTRAORDINARY_TOOL_HUE)
        {
            Name = GuildCraftingStringConstants.NAME_GUILD_SCRIBE;
        }

        public GuildScribe(Serial serial) : base(serial) { }

        #endregion

        #region Item Validation

        protected override bool ValidateItem(Mobile from, Item item)
        {
            // Accept regular spellbooks
            if (item is Spellbook && !(item is NecromancerSpellbook))
            {
                return true;
            }

            // Accept necromancer spellbooks only if they are metal
            if (item is NecromancerSpellbook)
            {
                return Server.Misc.MaterialInfo.IsAnyKindOfMetalItem(item);
            }

            return false;
        }

        #endregion

        #region Serialization

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)GuildCraftingConstants.SERIALIZATION_VERSION);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        #endregion
    }
}

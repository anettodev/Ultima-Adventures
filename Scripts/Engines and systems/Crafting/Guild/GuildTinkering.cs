using System;
using Server;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
    /// <summary>
    /// Extraordinary Tinkers Tools for enhancing jewelry.
    /// Requires Tinkers Guild membership and skill 90+.
    /// </summary>
    public class GuildTinkering : BaseGuildTool
    {
        #region Properties

        protected override NpcGuild RequiredGuild => NpcGuild.TinkersGuild;
        protected override SkillName RequiredSkill => SkillName.Tinkering;
        protected override double MinimumSkillRequired => GuildCraftingConstants.MIN_SKILL_REQUIRED;
        protected override bool AllowElderSkillBypass => false;
        protected override Type GuildmasterType => typeof(TinkerGuildmaster);
        protected override Type ShoppeType => typeof(TinkerShoppe);
        protected override string SelectionPrompt => GuildCraftingStringConstants.MSG_SELECT_JEWELRY;
        protected override string GuildRequirementMessage => GuildCraftingStringConstants.MSG_GUILD_TINKERS_ONLY;
        protected override string SkillRequirementMessage => GuildCraftingStringConstants.MSG_REQUIRES_MASTER_TINKER;
        protected override string LocationRequirementMessage => GuildCraftingStringConstants.MSG_REQUIRES_TINKER_LOCATION;
        public override int EnhancementSoundEffect => GuildCraftingConstants.SOUND_TINKERING;

        #endregion

        #region Constructor

        [Constructable]
        public GuildTinkering()
            : base(GuildCraftingConstants.ITEMID_TINKERING_TOOLS,
                   GuildCraftingConstants.TOOL_WEIGHT_HEAVY,
                   GuildCraftingConstants.EXTRAORDINARY_TOOL_HUE)
        {
            Name = GuildCraftingStringConstants.NAME_GUILD_TINKERING;
        }

        public GuildTinkering(Serial serial) : base(serial) { }

        #endregion

        #region Item Validation

        protected override string GetHelpTopic()
        {
            return GuildCraftingStringConstants.SPEECH_TOPIC_ENHANCE_JEWELS;
        }

        protected override bool ValidateItem(Mobile from, Item item)
        {
            // Accept jewelry, but exclude special magic items that are classified as BaseJewel
            if (item is BaseJewel &&
                !(MaterialInfo.IsMagicTorch(item)) &&
                !(MaterialInfo.IsMagicTalisman(item)) &&
                !(MaterialInfo.IsMagicCandle(item)) &&
                !(item is MagicRobe) &&
                !(item is MagicHat) &&
                !(item is MagicCloak) &&
                !(item is MagicBoots) &&
                !(MaterialInfo.IsMagicBelt(item)) &&
                !(MaterialInfo.IsMagicSash(item)))
            {
                return true;
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

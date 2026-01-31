using System;
using Server;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
    /// <summary>
    /// Extraordinary Fletching Tools for enhancing bows and crossbows.
    /// Requires Archers Guild membership and skill 90+.
    /// </summary>
    public class GuildFletching : BaseGuildTool
    {
        #region Properties

        protected override NpcGuild RequiredGuild { get { return NpcGuild.ArchersGuild; } }
        protected override SkillName RequiredSkill { get { return SkillName.Fletching; } }
        protected override double MinimumSkillRequired { get { return GuildCraftingConstants.MIN_SKILL_REQUIRED; } }
        protected override bool AllowElderSkillBypass { get { return false; } }
        protected override Type GuildmasterType { get { return typeof(ArcherGuildmaster); } }
        protected override Type ShoppeType { get { return typeof(BowyerShoppe); } }
        protected override string SelectionPrompt { get { return GuildCraftingStringConstants.MSG_SELECT_RANGED_WEAPON; } }
        protected override string GuildRequirementMessage { get { return GuildCraftingStringConstants.MSG_GUILD_ARCHERS_ONLY; } }
        protected override string SkillRequirementMessage { get { return GuildCraftingStringConstants.MSG_REQUIRES_MASTER_FLETCHER; } }
        protected override string LocationRequirementMessage { get { return GuildCraftingStringConstants.MSG_REQUIRES_BOWYER_LOCATION; } }
        public override int EnhancementSoundEffect { get { return GuildCraftingConstants.SOUND_FLETCHING; } }

        #endregion

        #region Constructor

        [Constructable]
        public GuildFletching()
            : base(GuildCraftingConstants.ITEMID_FLETCHING_TOOLS,
                   GuildCraftingConstants.TOOL_WEIGHT_HEAVY,
                   GuildCraftingConstants.EXTRAORDINARY_TOOL_HUE)
        {
            Name = GuildCraftingStringConstants.NAME_GUILD_FLETCHING;
        }

        public GuildFletching(Serial serial) : base(serial) { }

        #endregion

        #region Item Validation

        protected override bool ValidateItem(Mobile from, Item item)
        {
            // Only accept ranged weapons (bows and crossbows)
            if (item is BaseRanged)
            {
                return Server.Misc.MaterialInfo.IsAnyKindOfWoodItem(item);
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

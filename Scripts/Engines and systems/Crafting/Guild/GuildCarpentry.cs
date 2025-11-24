using System;
using Server;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
    /// <summary>
    /// Extraordinary Woodworking Tools for enhancing wooden weapons and armor.
    /// Requires Carpenters Guild membership and skill 90+.
    /// </summary>
    public class GuildCarpentry : BaseGuildTool
    {
        #region Properties

        protected override NpcGuild RequiredGuild { get { return NpcGuild.CarpentersGuild; } }
        protected override SkillName RequiredSkill { get { return SkillName.Carpentry; } }
        protected override double MinimumSkillRequired { get { return GuildCraftingConstants.MIN_SKILL_REQUIRED; } }
        protected override bool AllowElderSkillBypass { get { return false; } }
        protected override Type GuildmasterType { get { return typeof(CarpenterGuildmaster); } }
        protected override Type ShoppeType { get { return typeof(CarpentryShoppe); } }
        protected override string SelectionPrompt { get { return GuildCraftingStringConstants.MSG_SELECT_WOODEN_EQUIPMENT; } }
        protected override string GuildRequirementMessage { get { return GuildCraftingStringConstants.MSG_GUILD_CARPENTERS_ONLY; } }
        protected override string SkillRequirementMessage { get { return GuildCraftingStringConstants.MSG_REQUIRES_MASTER_CARPENTER; } }
        protected override string LocationRequirementMessage { get { return GuildCraftingStringConstants.MSG_REQUIRES_CARPENTRY_LOCATION; } }
        public override int EnhancementSoundEffect { get { return GuildCraftingConstants.SOUND_CARPENTRY; } }

        #endregion

        #region Constructor

        [Constructable]
        public GuildCarpentry()
            : base(GuildCraftingConstants.ITEMID_CARPENTRY_TOOLS,
                   GuildCraftingConstants.TOOL_WEIGHT_HEAVY,
                   GuildCraftingConstants.EXTRAORDINARY_TOOL_HUE)
        {
            Name = GuildCraftingStringConstants.NAME_GUILD_CARPENTRY;
        }

        public GuildCarpentry(Serial serial) : base(serial) { }

        #endregion

        #region Item Validation

        protected override bool ValidateItem(Mobile from, Item item)
        {
            // Accept wooden weapons (excluding ranged - handled by Fletching)
            if (item is BaseWeapon && !(item is BaseRanged))
            {
                return Server.Misc.MaterialInfo.IsAnyKindOfWoodItem(item);
            }
            // Accept wooden armor
            else if (item is BaseArmor)
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

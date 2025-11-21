using System;
using Server;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
    /// <summary>
    /// Extraordinary Smithing Hammer for enhancing metal weapons and armor.
    /// Requires Blacksmithing Guild membership (or elder skill 110+) and skill 90+.
    /// </summary>
    public class GuildHammer : BaseGuildTool
    {
        #region Properties

        protected override NpcGuild RequiredGuild => NpcGuild.BlacksmithsGuild;
        protected override SkillName RequiredSkill => SkillName.Blacksmith;
        protected override double MinimumSkillRequired => GuildCraftingConstants.MIN_SKILL_REQUIRED;
        protected override bool AllowElderSkillBypass => true;
        protected override Type GuildmasterType => typeof(BlacksmithGuildmaster);
        protected override Type ShoppeType => typeof(BlacksmithShoppe);
        protected override string SelectionPrompt => GuildCraftingStringConstants.MSG_SELECT_METAL_EQUIPMENT;
        protected override string GuildRequirementMessage => GuildCraftingStringConstants.MSG_GUILD_BLACKSMITHS_ONLY;
        protected override string SkillRequirementMessage => GuildCraftingStringConstants.MSG_REQUIRES_MASTER_BLACKSMITH;
        protected override string LocationRequirementMessage => GuildCraftingStringConstants.MSG_REQUIRES_SMITHING_LOCATION;
        public override int EnhancementSoundEffect => GuildCraftingConstants.SOUND_BLACKSMITHING;

        #endregion

        #region Constructor

        [Constructable]
        public GuildHammer()
            : base(GuildCraftingConstants.ITEMID_SMITHING_HAMMER,
                   GuildCraftingConstants.TOOL_WEIGHT_HEAVY,
                   GuildCraftingConstants.EXTRAORDINARY_TOOL_HUE)
        {
            Name = GuildCraftingStringConstants.NAME_GUILD_HAMMER;
        }

        public GuildHammer(Serial serial) : base(serial) { }

        #endregion

        #region Item Validation

        protected override bool ValidateItem(Mobile from, Item item)
        {
            if (item is BaseWeapon)
            {
                return Server.Misc.MaterialInfo.IsAnyKindOfMetalItem(item);
            }
            else if (item is BaseArmor)
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

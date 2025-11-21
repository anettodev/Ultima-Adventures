using System;
using Server;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
    /// <summary>
    /// Extraordinary Sewing Kit for enhancing clothing and fabric armor.
    /// Requires Tailors Guild membership and skill 90+.
    /// </summary>
    public class GuildSewing : BaseGuildTool
    {
        #region Properties

        protected override NpcGuild RequiredGuild => NpcGuild.TailorsGuild;
        protected override SkillName RequiredSkill => SkillName.Tailoring;
        protected override double MinimumSkillRequired => GuildCraftingConstants.MIN_SKILL_REQUIRED;
        protected override bool AllowElderSkillBypass => false;
        protected override Type GuildmasterType => typeof(TailorGuildmaster);
        protected override Type ShoppeType => typeof(TailorShoppe);
        protected override string SelectionPrompt => GuildCraftingStringConstants.MSG_SELECT_CLOTHING;
        protected override string GuildRequirementMessage => GuildCraftingStringConstants.MSG_GUILD_TAILORS_ONLY;
        protected override string SkillRequirementMessage => GuildCraftingStringConstants.MSG_REQUIRES_MASTER_TAILOR;
        protected override string LocationRequirementMessage => GuildCraftingStringConstants.MSG_REQUIRES_TAILOR_LOCATION;
        public override int EnhancementSoundEffect => GuildCraftingConstants.SOUND_TAILORING;

        #endregion

        #region Constructor

        [Constructable]
        public GuildSewing()
            : base(GuildCraftingConstants.ITEMID_SEWING_KIT_2,
                   GuildCraftingConstants.TOOL_WEIGHT_LIGHT,
                   GuildCraftingConstants.EXTRAORDINARY_TOOL_HUE)
        {
            Name = GuildCraftingStringConstants.NAME_GUILD_SEWING;
            // Randomly choose between the two sewing kit graphics
            if (Utility.RandomBool())
                ItemID = GuildCraftingConstants.ITEMID_SEWING_KIT_1;
        }

        public GuildSewing(Serial serial) : base(serial) { }

        #endregion

        #region Item Validation

        protected override bool ValidateItem(Mobile from, Item item)
        {
            // Special case: BootsofHermes cannot be enhanced
            if (item is BootsofHermes)
            {
                from.SendMessage(GuildCraftingStringConstants.MSG_MAGIC_BOOTS_BLOCKED);
                return false;
            }

            // Accept special magical clothing items classified as BaseJewel
            if (item is BaseJewel &&
                (item is MagicRobe || item is MagicHat || item is MagicCloak ||
                 item is MagicBoots || MaterialInfo.IsMagicBelt(item) || MaterialInfo.IsMagicSash(item)))
            {
                return true;
            }

            // Accept special weapon items (throwing gloves, pugilist gloves/mits)
            if (item is BaseWeapon &&
                (item is ThrowingGloves || item is PugilistGlove ||
                 item is PugilistGloves || item is PugilistMits))
            {
                return true;
            }

            // Accept cloth armor
            if (item is BaseArmor)
            {
                return Server.Misc.MaterialInfo.IsAnyKindOfClothItem(item);
            }

            // Accept regular clothing
            if (item is BaseClothing)
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

            // Ensure valid ItemID
            if (ItemID != GuildCraftingConstants.ITEMID_SEWING_KIT_1 &&
                ItemID != GuildCraftingConstants.ITEMID_SEWING_KIT_2)
            {
                ItemID = GuildCraftingConstants.ITEMID_SEWING_KIT_2;
            }
        }

        #endregion
    }
}

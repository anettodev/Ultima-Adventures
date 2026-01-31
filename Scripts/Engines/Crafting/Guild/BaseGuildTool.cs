using System;
using Server;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
    /// <summary>
    /// Base class for all extraordinary guild crafting tools.
    /// Provides common validation and enhancement logic for guild-specific item enhancement.
    /// Eliminates code duplication across 6 guild tool implementations.
    /// </summary>
    public abstract class BaseGuildTool : Item
    {
        #region Abstract Properties

        /// <summary>Required guild membership for using this tool</summary>
        protected abstract NpcGuild RequiredGuild { get; }

        /// <summary>Required skill for using this tool</summary>
        protected abstract SkillName RequiredSkill { get; }

        /// <summary>Minimum skill level required (usually 90.0, but Fletching uses 100.0)</summary>
        protected abstract double MinimumSkillRequired { get; }

        /// <summary>Whether elder skill (110+) bypasses guild requirement</summary>
        protected abstract bool AllowElderSkillBypass { get; }

        /// <summary>Type of guildmaster to check proximity for</summary>
        protected abstract Type GuildmasterType { get; }

        /// <summary>Type of shoppe to check ownership for</summary>
        protected abstract Type ShoppeType { get; }

        /// <summary>Message displayed when requesting target selection</summary>
        protected abstract string SelectionPrompt { get; }

        /// <summary>Message displayed if player is not guild member</summary>
        protected abstract string GuildRequirementMessage { get; }

        /// <summary>Message displayed if player skill is too low</summary>
        protected abstract string SkillRequirementMessage { get; }

        /// <summary>Message displayed if player is not near required location</summary>
        protected abstract string LocationRequirementMessage { get; }

        /// <summary>Sound effect played when enhancement succeeds</summary>
        public abstract int EnhancementSoundEffect { get; }

        /// <summary>Validates if the targeted item can be enhanced by this tool</summary>
        /// <param name="from">The mobile attempting to enhance</param>
        /// <param name="item">The item to validate</param>
        /// <returns>True if item can be enhanced, false otherwise</returns>
        protected abstract bool ValidateItem(Mobile from, Item item);

        #endregion

        #region Constructor

        protected BaseGuildTool(int itemID, double weight, int hue) : base(itemID)
        {
            Weight = weight;
            Hue = hue;
        }

        public BaseGuildTool(Serial serial) : base(serial) { }

        #endregion

        #region Validation Methods

        /// <summary>
        /// Validates that the player is in an acceptable location to use the guild tool.
        /// Checks for: guildmaster proximity, owned shoppe proximity, or Ter Mur guild district.
        /// </summary>
        protected virtual bool ValidateLocation(Mobile from)
        {
            // Check for guildmaster within range
            foreach (Mobile m in this.GetMobilesInRange(GuildCraftingConstants.GUILDMASTER_PROXIMITY_RANGE))
            {
                if (m.GetType() == GuildmasterType)
                    return true;
            }

            // Check for owned shoppe within range
            foreach (Item i in this.GetItemsInRange(GuildCraftingConstants.GUILDMASTER_PROXIMITY_RANGE))
            {
                if (i.GetType() == ShoppeType && !i.Movable)
                {
                    // Use reflection to get ShoppeOwner property
                    var shoppeOwnerProp = i.GetType().GetProperty("ShoppeOwner");
                    if (shoppeOwnerProp != null)
                    {
                        Mobile owner = shoppeOwnerProp.GetValue(i, null) as Mobile;
                        if (owner == from)
                            return true;
                    }
                }
            }

            // Check for Ter Mur guild district
            if (from.Map == Map.TerMur &&
                from.X > GuildCraftingConstants.TER_MUR_GUILD_MIN_X &&
                from.X < GuildCraftingConstants.TER_MUR_GUILD_MAX_X &&
                from.Y > GuildCraftingConstants.TER_MUR_GUILD_MIN_Y &&
                from.Y < GuildCraftingConstants.TER_MUR_GUILD_MAX_Y)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validates that the player has the required guild membership or elder skills.
        /// </summary>
        protected virtual bool ValidateGuildMembership(Mobile from)
        {
            if (!(from is PlayerMobile))
                return false;

            PlayerMobile pm = (PlayerMobile)from;

            // Elder skill check bypasses guild requirement (if allowed by this tool)
            if (AllowElderSkillBypass &&
                from.Skills[RequiredSkill].Value >= GuildCraftingConstants.ELDER_SKILL_BYPASS)
                return true;

            return pm.NpcGuild == RequiredGuild;
        }

        /// <summary>
        /// Validates that the player has the required skill level.
        /// </summary>
        protected virtual bool ValidateSkillLevel(Mobile from)
        {
            return from.Skills[RequiredSkill].Value >= MinimumSkillRequired;
        }

        /// <summary>
        /// Validates that the item is in the player's backpack.
        /// </summary>
        protected bool ValidateItemInPack(Mobile from, Item item)
        {
            if (item.RootParent != from)
            {
                from.SendLocalizedMessage(GuildCraftingConstants.MSG_MUST_BE_IN_PACK);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates that the item is not a legendary artifact (ILevelable).
        /// </summary>
        protected bool ValidateLegendaryArtifact(Mobile from, Item item)
        {
            if (item is ILevelable)
            {
                from.SendMessage(GuildCraftingStringConstants.MSG_CANNOT_ENHANCE_LEGENDARY);
                return false;
            }
            return true;
        }

        #endregion

        #region Public Methods

        public override void OnDoubleClick(Mobile from)
        {
            if (!(from is PlayerMobile))
                return;

            if (!ValidateGuildMembership(from))
            {
                from.SendMessage(GuildRequirementMessage);
                return;
            }

            if (!ValidateSkillLevel(from))
            {
                from.SendMessage(SkillRequirementMessage);
                return;
            }

            if (!ValidateLocation(from))
            {
                from.SendMessage(LocationRequirementMessage);
                return;
            }

            from.SendMessage(SelectionPrompt);
            from.BeginTarget(GuildCraftingConstants.TARGET_DISTANCE_UNLIMITED, false, TargetFlags.None, new TargetCallback(OnTarget));
        }

        #endregion

        #region Target Handling

        private void OnTarget(Mobile from, object obj)
        {
            if (!(obj is Item))
                return;

            Item item = (Item)obj;

            if (!ValidateItemInPack(from, item))
                return;

            if (!ValidateLegendaryArtifact(from, item))
                return;

            if (!ValidateItem(from, item))
            {
                from.SendMessage(GuildCraftingStringConstants.MSG_CANNOT_ENHANCE_ITEM);
                return;
            }

            GuildCraftingProcess process = new GuildCraftingProcess(from, item, this);
            process.BeginProcess();
        }

        #endregion

        #region Context Menu

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new SpeechGumpEntry(from, GetHelpTopic()));
        }

        /// <summary>
        /// Gets the help topic for the speech gump. Can be overridden by derived classes.
        /// </summary>
        protected virtual string GetHelpTopic()
        {
            return GuildCraftingStringConstants.SPEECH_TOPIC_ENHANCE;
        }

        public class SpeechGumpEntry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private string m_Topic;

            public SpeechGumpEntry(Mobile from, string topic)
                : base(GuildCraftingConstants.CONTEXT_MENU_ENTRY_ID,
                       GuildCraftingConstants.CONTEXT_MENU_PRIORITY)
            {
                m_Mobile = from;
                m_Topic = topic;
            }

            public override void OnClick()
            {
                if (!(m_Mobile is PlayerMobile))
                    return;

                PlayerMobile mobile = (PlayerMobile)m_Mobile;
                if (!mobile.HasGump(typeof(SpeechGump)))
                {
                    mobile.SendGump(new SpeechGump(
                        GuildCraftingStringConstants.SPEECH_GUMP_TITLE,
                        SpeechFunctions.SpeechText(m_Mobile.Name, m_Mobile.Name, m_Topic)
                    ));
                }
            }
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

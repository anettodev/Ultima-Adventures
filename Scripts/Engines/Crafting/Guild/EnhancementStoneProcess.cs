using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
    public class GuildCraftingProcess
    {
        private int BaseCost = GuildCraftingConstants.BASE_ENHANCEMENT_COST;
        private bool AttrCountAffectsCost = true;
        public int MaxAttrCount = GuildCraftingConstants.MAX_ATTRIBUTES_ALLOWED;

        public Mobile Owner = null;
        public Item ItemToUpgrade = null;
        public int CurrentAttributeCount = 0;
        private BaseGuildTool GuildTool = null;

        public GuildCraftingProcess(Mobile from, Item target)
        {
            Owner = from;
            ItemToUpgrade = target;
        }

        public GuildCraftingProcess(Mobile from, Item target, BaseGuildTool tool)
        {
            Owner = from;
            ItemToUpgrade = target;
            GuildTool = tool;
        }

        public void BeginProcess()
        {
            CurrentAttributeCount = 0;

            if (!(ItemToUpgrade is BaseShield || ItemToUpgrade is BaseClothing || ItemToUpgrade is BaseArmor || ItemToUpgrade is BaseWeapon || ItemToUpgrade is BaseJewel || ItemToUpgrade is Spellbook))
            {
                Owner.SendMessage(GuildCraftingStringConstants.MSG_ITEM_NOT_ENHANCEABLE);
            }
            else
            {
                int MaxedAttributes = 0;

                foreach (AttributeHandler handler in AttributeHandler.Definitions)
                {
                    int attr = handler.Upgrade(ItemToUpgrade, true);
                    
                    if (attr > 0)
                        CurrentAttributeCount++;

                    if (attr >= handler.MaxValue)
                        MaxedAttributes++;
                }

                if (CurrentAttributeCount > MaxAttrCount || MaxedAttributes >= MaxAttrCount )
                    Owner.SendMessage(GuildCraftingStringConstants.MSG_MAX_ENHANCEMENT_REACHED);
                else
                    Owner.SendGump(new EnhancementGump(this));
            }
        }

        public void BeginUpgrade(AttributeHandler handler)
        {
            if (GetCostToUpgrade(handler) < 1 )
			{
				Owner.SendMessage(GuildCraftingStringConstants.MSG_ATTRIBUTE_MAXED);
			}
            else if (SpendGold(GetCostToUpgrade(handler)))
            {
                handler.Upgrade(ItemToUpgrade, false);
                BeginProcess();
            }
        }

        private bool SpendGold(int amount)
        {
            bool bought = (Owner.AccessLevel >= AccessLevel.GameMaster);
            bool fromBank = false;

            Container cont = Owner.Backpack;
            if (!bought && cont != null)
            {
                if (cont.ConsumeTotal(typeof(Gold), amount))
                    bought = true;
                else
                {
                    cont = Owner.FindBankNoCreate();
                    if (cont != null && cont.ConsumeTotal(typeof(Gold), amount))
                    {
                        bought = true;
                        fromBank = true;
                    }
                    else
                    {
                        Owner.SendLocalizedMessage(GuildCraftingConstants.MSG_NEED_MORE_GOLD);
                    }
                }
            }

            if (bought)
            {
                if (Owner.AccessLevel >= AccessLevel.GameMaster)
                    Owner.SendMessage(GuildCraftingStringConstants.MSG_ADMIN_GOLD_BYPASS_FORMAT, amount);
                else if (fromBank)
                    Owner.SendMessage(GuildCraftingStringConstants.MSG_PURCHASE_FROM_BANK_FORMAT, amount);
                else
                    Owner.SendMessage(GuildCraftingStringConstants.MSG_PURCHASE_COST_FORMAT, amount);
            }

			// Play sound effect based on guild tool (if available) or fall back to guild type
			if (GuildTool != null)
			{
				Owner.PlaySound(GuildTool.EnhancementSoundEffect);
			}
			else
			{
				// Legacy fallback for old constructor calls
				PlayerMobile pc = (PlayerMobile)Owner;
				if ( pc.NpcGuild == NpcGuild.TailorsGuild ){ Owner.PlaySound( GuildCraftingConstants.SOUND_TAILORING ); }
				else if ( pc.NpcGuild == NpcGuild.CarpentersGuild ){ Owner.PlaySound( GuildCraftingConstants.SOUND_CARPENTRY ); }
				else if ( pc.NpcGuild == NpcGuild.ArchersGuild ){ Owner.PlaySound( GuildCraftingConstants.SOUND_FLETCHING ); }
				else if ( pc.NpcGuild == NpcGuild.TinkersGuild ){ Owner.PlaySound( GuildCraftingConstants.SOUND_TINKERING ); }
				else if ( pc.NpcGuild == NpcGuild.BlacksmithsGuild ){ Owner.PlaySound( GuildCraftingConstants.SOUND_BLACKSMITHING ); }
			}

            return bought;
        }

		public bool IsCraftedByEnhancer( Item item, Mobile from )
		{
			bool crafted = false;

			if ( item is BaseClothing ){ BaseClothing cloth = (BaseClothing)item; if ( cloth.Crafter == from ){ crafted = true; } }
			else if ( item is BaseArmor ){ BaseArmor armor = (BaseArmor)item; if ( armor.Crafter == from ){ crafted = true; } }
			else if ( item is BaseWeapon ){ BaseWeapon weapon = (BaseWeapon)item; if ( weapon.Crafter == from ){ crafted = true; } }

			return crafted;
		}

        public int GetCostToUpgrade(AttributeHandler handler)
        {
            int attrMultiplier = 1;
			int gold = BaseCost;
				if ( IsCraftedByEnhancer( ItemToUpgrade, Owner ) ){ gold = (int)( gold / GuildCraftingConstants.CRAFTER_DISCOUNT_DIVISOR ); }

            if (AttrCountAffectsCost)
            {
                foreach (AttributeHandler h in AttributeHandler.Definitions)
                    if (h.Name != handler.Name && h.Upgrade(ItemToUpgrade, true) > 0)
                        attrMultiplier++;
            }

            int cost = 0;

            int max = handler.MaxValue;
			int inc = handler.IncrementValue;
            int lvl = handler.Upgrade(ItemToUpgrade, true);

			if ( lvl < max )
			{
				cost = ((lvl+1)*handler.Cost)*gold;
			}

            cost = (int)(cost * attrMultiplier);

            return cost;
        }
    }
}
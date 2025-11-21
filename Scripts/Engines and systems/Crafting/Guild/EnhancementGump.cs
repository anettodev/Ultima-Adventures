using System;
using Server;
using Server.Items;

namespace Server.Gumps
{
    public class EnhancementGump : Gump
    {
        private GuildCraftingProcess Process;

        public EnhancementGump(GuildCraftingProcess process) : base(40, 40)
        {
            bool MoreAttributesAllowed = true;

            Process = process;

            if (Process.CurrentAttributeCount >= Process.MaxAttrCount)
                MoreAttributesAllowed = false;

            AddBackground(0, 0, GuildCraftingConstants.GUMP_WIDTH, GuildCraftingConstants.GUMP_HEIGHT, GuildCraftingConstants.GUMP_BACKGROUND_ID);
            AddImageTiled(GuildCraftingConstants.GUMP_PADDING_LEFT, GuildCraftingConstants.GUMP_PADDING_TOP, GuildCraftingConstants.GUMP_TITLE_WIDTH, GuildCraftingConstants.GUMP_TITLE_HEIGHT, GuildCraftingConstants.GUMP_TILE_ID);
            AddImageTiled(GuildCraftingConstants.GUMP_LEFT_COLUMN_X, GuildCraftingConstants.GUMP_COLUMN_Y, GuildCraftingConstants.GUMP_LEFT_COLUMN_WIDTH, GuildCraftingConstants.GUMP_COLUMN_HEIGHT, GuildCraftingConstants.GUMP_TILE_ID);
            AddImageTiled(GuildCraftingConstants.GUMP_RIGHT_COLUMN_X, GuildCraftingConstants.GUMP_COLUMN_Y, GuildCraftingConstants.GUMP_RIGHT_COLUMN_WIDTH, GuildCraftingConstants.GUMP_COLUMN_HEIGHT, GuildCraftingConstants.GUMP_TILE_ID);
            AddAlphaRegion(GuildCraftingConstants.GUMP_PADDING_LEFT, GuildCraftingConstants.GUMP_PADDING_TOP, GuildCraftingConstants.GUMP_TITLE_WIDTH, GuildCraftingConstants.GUMP_COLUMN_HEIGHT + GuildCraftingConstants.GUMP_COLUMN_Y - GuildCraftingConstants.GUMP_PADDING_TOP + GuildCraftingConstants.GUMP_PADDING_LEFT);

            AddLabel(GuildCraftingConstants.TITLE_LABEL_X, GuildCraftingConstants.TITLE_LABEL_Y, GuildCraftingConstants.GUMP_LABEL_COLOR, GuildCraftingStringConstants.LABEL_TITLE);

            AddLabel(GuildCraftingConstants.HEADER_ATTRIBUTES_X_LEFT, GuildCraftingConstants.HEADER_LABEL_Y, GuildCraftingConstants.GUMP_LABEL_COLOR, GuildCraftingStringConstants.LABEL_ATTRIBUTES);
            AddLabel(GuildCraftingConstants.HEADER_GOLD_X_LEFT, GuildCraftingConstants.HEADER_LABEL_Y, GuildCraftingConstants.GUMP_LABEL_COLOR, GuildCraftingStringConstants.LABEL_GOLD);
            AddLabel(GuildCraftingConstants.HEADER_USE_X_LEFT, GuildCraftingConstants.HEADER_LABEL_Y, GuildCraftingConstants.GUMP_LABEL_COLOR, GuildCraftingStringConstants.LABEL_USE);

            AddLabel(GuildCraftingConstants.HEADER_ATTRIBUTES_X_RIGHT, GuildCraftingConstants.HEADER_LABEL_Y, GuildCraftingConstants.GUMP_LABEL_COLOR, GuildCraftingStringConstants.LABEL_ATTRIBUTES);
            AddLabel(GuildCraftingConstants.HEADER_GOLD_X_RIGHT, GuildCraftingConstants.HEADER_LABEL_Y, GuildCraftingConstants.GUMP_LABEL_COLOR, GuildCraftingStringConstants.LABEL_GOLD);
            AddLabel(GuildCraftingConstants.HEADER_USE_X_RIGHT, GuildCraftingConstants.HEADER_LABEL_Y, GuildCraftingConstants.GUMP_LABEL_COLOR, GuildCraftingStringConstants.LABEL_USE);

            int column = 0;
            int row = 0;

            for ( int i = 0; i < AttributeHandler.Definitions.Count; i++)
            {
                AttributeHandler handler = AttributeHandler.Definitions[i];

                if (handler.IsUpgradable(Process.ItemToUpgrade))
                {
                    int currentValue = handler.Upgrade(Process.ItemToUpgrade, true);

                    if (currentValue > 0 || MoreAttributesAllowed)
                    {
                        if (row > GuildCraftingConstants.MAX_ROWS_PER_COLUMN)
                        {
                            row = 0;
                            column = 1;
                        }

                        AddLabel(GuildCraftingConstants.LABEL_X_BASE + (GuildCraftingConstants.COLUMN_OFFSET * column), GuildCraftingConstants.LABEL_Y_OFFSET + (GuildCraftingConstants.ROW_HEIGHT * row), GuildCraftingConstants.GUMP_LABEL_COLOR, handler.Description);
                        AddLabel(GuildCraftingConstants.GOLD_LABEL_X_BASE + (GuildCraftingConstants.COLUMN_OFFSET * column), GuildCraftingConstants.LABEL_Y_OFFSET + (GuildCraftingConstants.ROW_HEIGHT * row), GuildCraftingConstants.GUMP_LABEL_COLOR, Process.GetCostToUpgrade(handler).ToString());
                        AddButton(GuildCraftingConstants.BUTTON_X_BASE + (GuildCraftingConstants.COLUMN_OFFSET * column), GuildCraftingConstants.BUTTON_Y_OFFSET + (GuildCraftingConstants.ROW_HEIGHT * row), GuildCraftingConstants.BUTTON_GRAPHIC_NORMAL, GuildCraftingConstants.BUTTON_GRAPHIC_PRESSED, GuildCraftingConstants.BUTTON_ID_OFFSET + i, GumpButtonType.Reply, 0);

                        row++;
                    }
                }
            }
        }

        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
            if (info.ButtonID >= GuildCraftingConstants.BUTTON_ID_OFFSET)
            {
                AttributeHandler handler = AttributeHandler.Definitions[info.ButtonID - GuildCraftingConstants.BUTTON_ID_OFFSET];
                Process.BeginUpgrade(handler);
            }
        }
    }
}

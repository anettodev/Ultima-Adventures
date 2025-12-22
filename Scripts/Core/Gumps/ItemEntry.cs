using System;

namespace Server.Gumps
{
	public class ItemEntry
	{
		public int ButtonID { get; set; }
		public Type[] ItemTypes { get; set; }
		public int NameCliloc { get; set; }
		public int TooltipCliloc { get; set; }
		public int ImageID { get; set; }
		public int ImageHue { get; set; }
		public int ImageOffsetX { get; set; }
		public int ImageOffsetY { get; set; }

		public ItemEntry( int buttonID, Type itemType, int nameCliloc, int tooltipCliloc, int imageID, int imageHue, int imageOffsetX, int imageOffsetY )
		{
			ButtonID = buttonID;
			ItemTypes = new Type[] { itemType };
			NameCliloc = nameCliloc;
			TooltipCliloc = tooltipCliloc;
			ImageID = imageID;
			ImageHue = imageHue;
			ImageOffsetX = imageOffsetX;
			ImageOffsetY = imageOffsetY;
		}

		public ItemEntry( int buttonID, Type[] itemTypes, int nameCliloc, int tooltipCliloc, int imageID, int imageHue, int imageOffsetX, int imageOffsetY )
		{
			ButtonID = buttonID;
			ItemTypes = itemTypes;
			NameCliloc = nameCliloc;
			TooltipCliloc = tooltipCliloc;
			ImageID = imageID;
			ImageHue = imageHue;
			ImageOffsetX = imageOffsetX;
			ImageOffsetY = imageOffsetY;
		}
	}
}


using System;
using Server.Items;

namespace Server.Gumps
{
	public class HeritageItemEntry : ItemEntry
	{
		public HeritageItemEntry( int buttonID, Type itemType, int nameCliloc, int tooltipCliloc, int imageID, int imageHue, int imageOffsetX, int imageOffsetY )
			: base( buttonID, itemType, nameCliloc, tooltipCliloc, imageID, imageHue, imageOffsetX, imageOffsetY )
		{
		}

		public HeritageItemEntry( int buttonID, Type[] itemTypes, int nameCliloc, int tooltipCliloc, int imageID, int imageHue, int imageOffsetX, int imageOffsetY )
			: base( buttonID, itemTypes, nameCliloc, tooltipCliloc, imageID, imageHue, imageOffsetX, imageOffsetY )
		{
		}
	}
}


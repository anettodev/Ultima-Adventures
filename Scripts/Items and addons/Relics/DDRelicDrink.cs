using System;
using Server;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	/// <summary>
	/// Drink relic item that can be consumed to restore thirst and stamina.
	/// Supports bottles, barrels, and kegs with various liquid types.
	/// </summary>
	public class DDRelicDrink : DDRelicBase
	{
		#region Constants

		private const int BASE_ITEM_ID = 0x9C7;
		private const int DRINK_TYPE_MIN = 1;
		private const int DRINK_TYPE_MAX = 4;
		private const int RANDOM_LIQUID_MIN = 0;
		private const int RANDOM_LIQUID_MAX = 5;
		private const int WEIGHT_BOTTLE = 3;
		private const int WEIGHT_BOTTLE_CULTURE = 5;
		private const int WEIGHT_BARREL = 100;
		private const int WEIGHT_KEG = 50;
		private const int HUE_KEG = 0x96D;
		private const int THIRST_RESTORE = 5;
		private const int THIRST_MAX = 20;
		private const int STAMINA_DIVISOR = 5;

		#endregion

		#region Fields

		/// <summary>Item IDs for standard bottle variants</summary>
		private static readonly int[] BOTTLE_ITEM_IDS = new[]
		{
			0x9C7, 0x99B, 0x99F
		};

		/// <summary>Item IDs for culture bottle variants</summary>
		private static readonly int[] CULTURE_BOTTLE_ITEM_IDS = new[]
		{
			0x543F, 0x5440, 0x5441
		};

		/// <summary>Item IDs for keg variants</summary>
		private static readonly int[] KEG_ITEM_IDS = new[]
		{
			0x1940, 0x1AD6, 0x1AD7
		};

		/// <summary>Liquid type names</summary>
		private static readonly string[] LIQUID_TYPES = new[]
		{
			"rum", "grog", "brandy", "whiskey", "brandy"
		};

		/// <summary>Drinking sound IDs</summary>
		private static readonly int[] DRINK_SOUNDS = new[]
		{
			0x30, 0x2D6
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new drink relic with random type and liquid
		/// </summary>
		[Constructable]
		public DDRelicDrink() : base(BASE_ITEM_ID)
		{
			string containerType = " garrafa de ";

			int drinkType = Utility.RandomMinMax(DRINK_TYPE_MIN, DRINK_TYPE_MAX);

			if (drinkType == 1)
			{
				Weight = WEIGHT_BOTTLE;
				ItemID = Utility.RandomList(BOTTLE_ITEM_IDS);
				Hue = Server.Misc.RandomThings.GetRandomColor(0);
				containerType = " garrafa de ";
			}
			else if (drinkType == 2)
			{
				Weight = WEIGHT_BOTTLE_CULTURE;
				ItemID = Utility.RandomList(CULTURE_BOTTLE_ITEM_IDS);
				Hue = Server.Misc.RandomThings.GetRandomColor(0);
				containerType = " garrafa de " + NameList.RandomName("cultures") + " ";
			}
			else if (drinkType == 3)
			{
				Weight = WEIGHT_BARREL;
				ItemID = 0xFAE;
				containerType = " barril de ";
			}
			else
			{
				Weight = WEIGHT_KEG;
				ItemID = Utility.RandomList(KEG_ITEM_IDS);
				Hue = HUE_KEG;
				containerType = " barril de ";
			}

			string quality = RelicHelper.GetRandomQualityDescriptor();
			int liquidIndex = Utility.RandomMinMax(RANDOM_LIQUID_MIN, RANDOM_LIQUID_MAX);
			string liquid = LIQUID_TYPES[liquidIndex];

			Name = quality + containerType + liquid;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicDrink(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to consume drink and restore thirst/stamina
		/// </summary>
		public override void OnDoubleClick(Mobile from)
		{
			if (from.Thirst < THIRST_MAX)
			{
				// Restore stamina (WIZARD feature)
				int staminaRestore = from.StamMax / STAMINA_DIVISOR;
				if (from.Stam < from.StamMax)
				{
					from.Stam += staminaRestore;
				}

				from.Thirst += THIRST_RESTORE;

				// Send message based on current thirst value
				int currentThirst = from.Thirst;
				if (currentThirst < 5)
				{
					from.SendMessage("Você bebe a água mas ainda está extremamente sedento");
				}
				else if (currentThirst < 10)
				{
					from.SendMessage("Você bebe a água e sente menos sede");
				}
				else if (currentThirst < 15)
				{
					from.SendMessage("Você bebe a água e sente muito menos sede");
				}
				else
				{
					from.SendMessage("Você bebe a água e não está mais sedento");
				}

				Consume();
				from.PlaySound(Utility.RandomList(DRINK_SOUNDS));
			}
			else
			{
				from.SendMessage("Você está simplesmente muito saciado para beber mais");
				from.Thirst = THIRST_MAX;
			}
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the drink relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		/// <summary>
		/// Deserializes the drink relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}

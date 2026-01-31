using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// First type of light relic item (candelabra variant 1).
	/// Inherits from BaseLight and provides decorative lighting.
	/// </summary>
	public class DDRelicLight1 : BaseLight
	{
		#region Constants

		private const int LIT_ITEM_ID = 0x40BE;
		private const int UNLIT_ITEM_ID = 0x4039;
		private const double WEIGHT_VALUE = 20.0;

		#endregion

		#region Properties

		/// <summary>Gold value of the relic</summary>
		public int RelicGoldValue;

		/// <summary>Gets the lit ItemID</summary>
		public override int LitItemID { get { return LIT_ITEM_ID; } }

		/// <summary>Gets the unlit ItemID</summary>
		public override int UnlitItemID { get { return UNLIT_ITEM_ID; } }

		/// <summary>
		/// Gets or sets the relic's gold value
		/// </summary>
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value
		{
			get { return RelicGoldValue; }
			set { RelicGoldValue = value; InvalidateProperties(); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new light relic with random quality
		/// </summary>
		[Constructable]
		public DDRelicLight1() : base(UNLIT_ITEM_ID)
		{
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.Circle225;
			Weight = WEIGHT_VALUE;

			string quality = RelicHelper.GetRandomQualityDescriptor();
			string decorative = RelicHelper.GetRandomDecorativeTerm();
			Name = quality + decorative + " candelabra";
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicLight1(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to display identification message
		/// </summary>
		public override void OnDoubleClick(Mobile from)
		{
			from.SendMessage(RelicStringConstants.MSG_IDENTIFY_VALUE);
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the light relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(RelicConstants.SERIALIZATION_VERSION);
			writer.Write(RelicGoldValue);
		}

		/// <summary>
		/// Deserializes the light relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			RelicGoldValue = reader.ReadInt();
		}

		#endregion
	}

	/// <summary>
	/// Second type of light relic item (candelabra variant 2).
	/// Inherits from BaseLight and provides decorative lighting.
	/// </summary>
	public class DDRelicLight2 : BaseLight
	{
		#region Constants

		private const int LIT_ITEM_ID = 0xB1D;
		private const int UNLIT_ITEM_ID = 0xA27;
		private const double WEIGHT_VALUE = 20.0;

		#endregion

		#region Properties

		/// <summary>Gold value of the relic</summary>
		public int RelicGoldValue;

		/// <summary>Gets the lit ItemID</summary>
		public override int LitItemID { get { return LIT_ITEM_ID; } }

		/// <summary>Gets the unlit ItemID</summary>
		public override int UnlitItemID { get { return UNLIT_ITEM_ID; } }

		/// <summary>
		/// Gets or sets the relic's gold value
		/// </summary>
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value
		{
			get { return RelicGoldValue; }
			set { RelicGoldValue = value; InvalidateProperties(); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new light relic with random quality
		/// </summary>
		[Constructable]
		public DDRelicLight2() : base(UNLIT_ITEM_ID)
		{
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.Circle225;
			Weight = WEIGHT_VALUE;

			string quality = RelicHelper.GetRandomQualityDescriptor();
			string decorative = RelicHelper.GetRandomDecorativeTerm();
			Name = quality + decorative + " candelabra";
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicLight2(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to display identification message
		/// </summary>
		public override void OnDoubleClick(Mobile from)
		{
			from.SendMessage(RelicStringConstants.MSG_IDENTIFY_VALUE);
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the light relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(RelicConstants.SERIALIZATION_VERSION);
			writer.Write(RelicGoldValue);
		}

		/// <summary>
		/// Deserializes the light relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			RelicGoldValue = reader.ReadInt();
		}

		#endregion
	}

	/// <summary>
	/// Third type of light relic item (candelabra variant 3).
	/// Inherits from BaseLight and provides decorative lighting.
	/// </summary>
	public class DDRelicLight3 : BaseLight
	{
		#region Constants

		private const int LIT_ITEM_ID = 0xB26;
		private const int UNLIT_ITEM_ID = 0xA29;
		private const double WEIGHT_VALUE = 20.0;

		#endregion

		#region Properties

		/// <summary>Gold value of the relic</summary>
		public int RelicGoldValue;

		/// <summary>Gets the lit ItemID</summary>
		public override int LitItemID { get { return LIT_ITEM_ID; } }

		/// <summary>Gets the unlit ItemID</summary>
		public override int UnlitItemID { get { return UNLIT_ITEM_ID; } }

		/// <summary>
		/// Gets or sets the relic's gold value
		/// </summary>
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value
		{
			get { return RelicGoldValue; }
			set { RelicGoldValue = value; InvalidateProperties(); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new light relic with random quality
		/// </summary>
		[Constructable]
		public DDRelicLight3() : base(UNLIT_ITEM_ID)
		{
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.Circle225;
			Weight = WEIGHT_VALUE;

			string quality = RelicHelper.GetRandomQualityDescriptor();
			string decorative = RelicHelper.GetRandomDecorativeTerm();
			Name = quality + decorative + " candelabra";
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public DDRelicLight3(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to display identification message
		/// </summary>
		public override void OnDoubleClick(Mobile from)
		{
			from.SendMessage(RelicStringConstants.MSG_IDENTIFY_VALUE);
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the light relic
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(RelicConstants.SERIALIZATION_VERSION);
			writer.Write(RelicGoldValue);
		}

		/// <summary>
		/// Deserializes the light relic
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			RelicGoldValue = reader.ReadInt();
		}

		#endregion
	}
}

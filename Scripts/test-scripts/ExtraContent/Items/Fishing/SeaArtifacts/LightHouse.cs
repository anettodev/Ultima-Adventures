using System;
using Server;
using Server.Items;

namespace Server.Items
{
	/// <summary>
	/// Lighthouse addon that can be placed in player homes.
	/// Provides a decorative light source with multiple components.
	/// </summary>
	public class LightHouseAddon : BaseAddon
	{
		#region Constants

		private const int ARRAY_DIMENSION_SIZE = SeaArtifactConstants.ARRAY_DIMENSION_SIZE;
		private const int LIGHT_SOURCE_COMPONENT_ID = SeaArtifactConstants.LIGHTHOUSE_COMPONENT_LIGHT_SOURCE;
		private const int LIGHT_SOURCE_X_OFFSET = 2;
		private const int LIGHT_SOURCE_Y_OFFSET = 2;
		private const int LIGHT_SOURCE_Z_OFFSET = SeaArtifactConstants.COMPONENT_Z_OFFSET_DEFAULT;
		private const int LIGHT_SOURCE_HUE = SeaArtifactConstants.COMPONENT_HUE_DEFAULT;
		private const int LIGHT_SOURCE_TYPE = SeaArtifactConstants.LIGHTHOUSE_LIGHT_SOURCE_TYPE;
		private const int LIGHT_SOURCE_AMOUNT = SeaArtifactConstants.COMPONENT_AMOUNT_DEFAULT;

		#endregion

		#region Fields

		private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {6843, -3, -3, 0}, {6844, -3, -2, 0}, {6845, -3, -1, 0}// 1	2	3	
			, {6842, -2, -3, 0}, {6862, -2, -2, 0}, {6861, -2, -1, 0}// 4	5	6	
			, {6846, -2, 0, 0}, {6849, -2, 1, 0}, {6841, -1, -3, 0}// 7	8	9	
			, {6863, -1, -2, 0}, {6859, -1, -1, 0}, {6858, -1, 0, 0}// 10	11	12	
			, {6855, -1, 1, 0}, {6852, -1, 2, 0}, {6820, -1, 3, 0}// 13	14	15	
			, {6838, 0, -2, 0}, {6860, 0, -1, 0}, {6821, 0, 3, 0}// 16	17	18	
			, {6835, 1, -2, 0}, {6832, 1, -1, 0}, {6822, 1, 3, 0}// 19	20	21	
			, {6829, 2, -1, 0}, {6823, 2, 3, 0}, {6828, 3, -1, 0}// 22	24	25	
			, {6827, 3, 0, 0}, {6826, 3, 1, 0}, {6825, 3, 2, 0}// 26	27	28	
			, {6824, 3, 3, 0}// 29	
		};

		#endregion

		#region Properties

		/// <summary>
		/// Gets the deed for this addon
		/// </summary>
		public override BaseAddonDeed Deed
		{
			get
			{
				return new LightHouseAddonDeed();
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new lighthouse addon
		/// </summary>
		[Constructable]
		public LightHouseAddon()
		{
			for (int i = 0; i < m_AddOnSimpleComponents.Length / ARRAY_DIMENSION_SIZE; i++)
			{
				AddComponent(
					new AddonComponent(m_AddOnSimpleComponents[i, 0]),
					m_AddOnSimpleComponents[i, 1],
					m_AddOnSimpleComponents[i, 2],
					m_AddOnSimpleComponents[i, 3]
				);
			}

			AddComplexComponent(
				this,
				LIGHT_SOURCE_COMPONENT_ID,
				LIGHT_SOURCE_X_OFFSET,
				LIGHT_SOURCE_Y_OFFSET,
				LIGHT_SOURCE_Z_OFFSET,
				LIGHT_SOURCE_HUE,
				LIGHT_SOURCE_TYPE,
				SeaArtifactStringConstants.NAME_LIGHTHOUSE,
				LIGHT_SOURCE_AMOUNT
			);
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public LightHouseAddon(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Adds a complex component to the addon with default amount
		/// </summary>
		private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
		{
			AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, SeaArtifactConstants.COMPONENT_AMOUNT_DEFAULT);
		}

		/// <summary>
		/// Adds a complex component to the addon with full configuration
		/// </summary>
		/// <param name="addon">The addon to add the component to</param>
		/// <param name="item">The item ID of the component</param>
		/// <param name="xoffset">X coordinate offset</param>
		/// <param name="yoffset">Y coordinate offset</param>
		/// <param name="zoffset">Z coordinate offset</param>
		/// <param name="hue">Hue value (0 for default)</param>
		/// <param name="lightsource">Light source type (-1 for no light)</param>
		/// <param name="name">Component name (null for default)</param>
		/// <param name="amount">Component amount</param>
		private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
		{
			AddonComponent ac = new AddonComponent(item);
			
			if (!string.IsNullOrEmpty(name))
			{
				ac.Name = name;
			}
			
			if (hue != 0)
			{
				ac.Hue = hue;
			}
			
			if (amount > 1)
			{
				ac.Stackable = true;
				ac.Amount = amount;
			}
			
			if (lightsource != -1)
			{
				ac.Light = LightType.Circle300;
			}

			addon.AddComponent(ac, xoffset, yoffset, zoffset);
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the addon
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(SeaArtifactConstants.SERIALIZATION_VERSION);
		}

		/// <summary>
		/// Deserializes the addon
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}

	/// <summary>
	/// Deed for placing a lighthouse addon in a player home
	/// </summary>
	public class LightHouseAddonDeed : BaseAddonDeed
	{
		#region Constants

		private const int DEED_ITEM_ID = SeaArtifactConstants.LIGHTHOUSE_DEED_ITEM_ID;
		private const int CLILOC_BUILD_IN_HOME = SeaArtifactConstants.CLILOC_BUILD_IN_HOME;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the addon created by this deed
		/// </summary>
		public override BaseAddon Addon
		{
			get
			{
				return new LightHouseAddon();
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new lighthouse addon deed
		/// </summary>
		[Constructable]
		public LightHouseAddonDeed()
		{
			Name = SeaArtifactStringConstants.NAME_LIGHTHOUSE;
			ItemID = DEED_ITEM_ID;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public LightHouseAddonDeed(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds name properties to the property list
		/// </summary>
		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
			list.Add(CLILOC_BUILD_IN_HOME, SeaArtifactStringConstants.PROP_BUILD_IN_HOME);
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the deed
		/// </summary>
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(SeaArtifactConstants.SERIALIZATION_VERSION);
		}

		/// <summary>
		/// Deserializes the deed
		/// </summary>
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		#endregion
	}
}
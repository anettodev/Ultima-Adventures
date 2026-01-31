using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Abstract base class for custom crafting implementations
	/// </summary>
	public abstract class CustomCraft
	{
		#region Fields

		private Mobile m_From;
		private CraftItem m_CraftItem;
		private CraftSystem m_CraftSystem;
		private Type m_TypeRes;
		private BaseTool m_Tool;
		private int m_Quality;

		#endregion

		#region Properties

		/// <summary>
		/// The mobile performing the craft
		/// </summary>
		public Mobile From{ get{ return m_From; } }

		/// <summary>
		/// The craft item being created
		/// </summary>
		public CraftItem CraftItem{ get{ return m_CraftItem; } }

		/// <summary>
		/// The crafting system being used
		/// </summary>
		public CraftSystem CraftSystem{ get{ return m_CraftSystem; } }

		/// <summary>
		/// The resource type being used
		/// </summary>
		public Type TypeRes{ get{ return m_TypeRes; } }

		/// <summary>
		/// The crafting tool being used
		/// </summary>
		public BaseTool Tool{ get{ return m_Tool; } }

		/// <summary>
		/// The quality level of the crafted item (0=normal, 1=exceptional, 2=exceptional with mark)
		/// </summary>
		public int Quality{ get{ return m_Quality; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustomCraft class
		/// </summary>
		/// <param name="from">The mobile performing the craft</param>
		/// <param name="craftItem">The craft item being created</param>
		/// <param name="craftSystem">The crafting system being used</param>
		/// <param name="typeRes">The resource type being used</param>
		/// <param name="tool">The crafting tool being used</param>
		/// <param name="quality">The quality level of the crafted item</param>
		public CustomCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality )
		{
			m_From = from;
			m_CraftItem = craftItem;
			m_CraftSystem = craftSystem;
			m_TypeRes = typeRes;
			m_Tool = tool;
			m_Quality = quality;
		}

		#endregion

		#region Abstract Methods

		/// <summary>
		/// Called when the craft action ends (cleanup, effects, etc.)
		/// </summary>
		public abstract void EndCraftAction();

		/// <summary>
		/// Completes the craft and returns the created item
		/// </summary>
		/// <param name="message">The localized message number to display</param>
		/// <returns>The created item, or null if crafting failed</returns>
		public abstract Item CompleteCraft( out int message );

		#endregion
	}
}
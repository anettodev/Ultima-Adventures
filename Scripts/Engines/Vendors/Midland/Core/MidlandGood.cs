using System;

namespace Server.Mobiles
{
	/// <summary>
	/// Represents a single good/item that a Midland vendor trades.
	/// Replaces the good1-4 pattern to eliminate code duplication.
	/// </summary>
	public class MidlandGood
	{
		#region Fields
		
		private Type m_Type;
		private string m_Name;
		private int m_Price;
		private int m_Inventory;
		private double m_Adjust;
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets or sets the item type for this good.
		/// </summary>
		public Type Type
		{
			get { return m_Type; }
			set { m_Type = value; }
		}
		
		/// <summary>
		/// Gets or sets the display name for this good.
		/// </summary>
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}
		
		/// <summary>
		/// Gets or sets the current price for this good.
		/// </summary>
		public int Price
		{
			get { return m_Price; }
			set { m_Price = value; }
		}
		
		/// <summary>
		/// Gets or sets the current inventory amount for this good.
		/// </summary>
		public int Inventory
		{
			get { return m_Inventory; }
			set { m_Inventory = value; }
		}
		
		/// <summary>
		/// Gets or sets the price adjustment multiplier for this good.
		/// Used in dynamic price calculation based on inventory.
		/// </summary>
		public double Adjust
		{
			get { return m_Adjust; }
			set { m_Adjust = value; }
		}
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of MidlandGood with default values.
		/// </summary>
		public MidlandGood()
		{
			m_Type = null;
			m_Name = "";
			m_Price = 0;
			m_Inventory = 0;
			m_Adjust = 1.0;
		}
		
		/// <summary>
		/// Initializes a new instance of MidlandGood with specified values.
		/// </summary>
		/// <param name="type">The item type</param>
		/// <param name="name">The display name</param>
		/// <param name="adjust">The price adjustment multiplier</param>
		public MidlandGood(Type type, string name, double adjust)
		{
			m_Type = type;
			m_Name = name;
			m_Price = 0;
			m_Inventory = 0;
			m_Adjust = adjust;
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Calculates the price for this good based on current inventory.
		/// Uses the dynamic pricing formula: 1 + (100*adjust - ((100*adjust)/(5000/adjust)) * inventory)
		/// </summary>
		/// <returns>The calculated price</returns>
		public int CalculatePrice()
		{
			if (m_Inventory > 0)
			{
				return MidlandVendorConstants.MIN_PRICE + 
					((int)(MidlandVendorConstants.PRICE_BASE_MULTIPLIER * m_Adjust) - 
					 (int)(((MidlandVendorConstants.PRICE_BASE_MULTIPLIER * m_Adjust) / 
					        (MidlandVendorConstants.PRICE_INVENTORY_DIVISOR / m_Adjust)) * 
					       (double)m_Inventory));
			}
			else
			{
				return (int)(MidlandVendorConstants.PRICE_BASE_MULTIPLIER * m_Adjust);
			}
		}
		
		#endregion
	}
}


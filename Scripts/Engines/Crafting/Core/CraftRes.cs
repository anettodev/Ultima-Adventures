using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Represents a resource requirement for a craftable item
	/// </summary>
	public class CraftRes
	{
		#region Constants

		/// <summary>Default localized message when resources are missing: "You don't have the resources required to make that item."</summary>
		private const int DEFAULT_RESOURCE_MESSAGE = 502925;

		#endregion

		#region Fields

		private Type m_Type;
		private int m_Amount;

		private string m_MessageString;
		private int m_MessageNumber;

		private string m_NameString;
		private int m_NameNumber;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CraftRes class with basic type and amount
		/// </summary>
		/// <param name="type">The type of resource required</param>
		/// <param name="amount">The amount of resource required</param>
		public CraftRes( Type type, int amount )
		{
			m_Type = type;
			m_Amount = amount;
		}

		/// <summary>
		/// Initializes a new instance of the CraftRes class with name and message
		/// </summary>
		/// <param name="type">The type of resource required</param>
		/// <param name="name">The name of the resource (TextDefinition)</param>
		/// <param name="amount">The amount of resource required</param>
		/// <param name="message">The message to display if resource is missing (TextDefinition)</param>
		public CraftRes( Type type, TextDefinition name, int amount, TextDefinition message ): this ( type, amount )
		{
			m_NameNumber = name;
			m_MessageNumber = message;

			m_NameString = name;
			m_MessageString = message;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The type of item required as a resource
		/// </summary>
		public Type ItemType
		{
			get { return m_Type; }
		}

		/// <summary>
		/// The amount of resource required
		/// </summary>
		public int Amount
		{
			get { return m_Amount; }
		}

		/// <summary>
		/// The string name of the resource
		/// </summary>
		public string NameString
		{
			get { return m_NameString; }
		}

		/// <summary>
		/// The localized name number of the resource
		/// </summary>
		public int NameNumber
		{
			get { return m_NameNumber; }
		}

		/// <summary>
		/// The string message to display if resource is missing
		/// </summary>
		public string MessageString
		{
			get { return m_MessageString; }
		}

		/// <summary>
		/// The localized message number to display if resource is missing
		/// </summary>
		public int MessageNumber
		{
			get { return m_MessageNumber; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Sends an appropriate message to the mobile about missing resources
		/// </summary>
		/// <param name="from">The mobile to send the message to</param>
		public void SendMessage( Mobile from )
		{
			if ( m_MessageNumber > 0 )
				from.SendLocalizedMessage( m_MessageNumber );
			else if ( !String.IsNullOrEmpty( m_MessageString ) )
				from.SendMessage( m_MessageString );
			else
				from.SendLocalizedMessage( DEFAULT_RESOURCE_MESSAGE );
		}

		#endregion
	}
}

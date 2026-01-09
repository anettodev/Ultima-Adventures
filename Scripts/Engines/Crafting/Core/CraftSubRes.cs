using System;

namespace Server.Engines.Craft
{
	/// <summary>
	/// Represents a sub-resource type (e.g., different metal types for blacksmithy) with skill requirements
	/// </summary>
	public class CraftSubRes
	{
		#region Constants

		/// <summary>Default generic name number when not specified</summary>
		private const int NO_GENERIC_NAME = 0;

		#endregion

		#region Fields

		private Type m_Type;
		private double m_ReqSkill;
		private string m_NameString;
		private int m_NameNumber;
		private int m_GenericNameNumber;
		private object m_Message;

		#endregion

		#region Properties

		/// <summary>
		/// The type of item this sub-resource represents
		/// </summary>
		public Type ItemType
		{
			get { return m_Type; }
		}

		/// <summary>
		/// The string name of this sub-resource
		/// </summary>
		public string NameString
		{
			get { return m_NameString; }
		}

		/// <summary>
		/// The localized name number of this sub-resource
		/// </summary>
		public int NameNumber
		{
			get { return m_NameNumber; }
		}

		/// <summary>
		/// The generic name number for display purposes
		/// </summary>
		public int GenericNameNumber
		{
			get { return m_GenericNameNumber; }
		}

		/// <summary>
		/// The message to display if skill requirement is not met
		/// </summary>
		public object Message
		{
			get { return m_Message; }
		}

		/// <summary>
		/// The required skill level to use this sub-resource
		/// </summary>
		public double RequiredSkill
		{
			get { return m_ReqSkill; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CraftSubRes class without generic name
		/// </summary>
		/// <param name="type">The type of sub-resource</param>
		/// <param name="name">The name of the sub-resource (TextDefinition)</param>
		/// <param name="reqSkill">The required skill to use this sub-resource</param>
		/// <param name="message">The message to display if skill requirement is not met</param>
		public CraftSubRes( Type type, TextDefinition name, double reqSkill, object message ) : this( type, name, reqSkill, NO_GENERIC_NAME, message )
		{
		}

		/// <summary>
		/// Initializes a new instance of the CraftSubRes class with generic name
		/// </summary>
		/// <param name="type">The type of sub-resource</param>
		/// <param name="name">The name of the sub-resource (TextDefinition)</param>
		/// <param name="reqSkill">The required skill to use this sub-resource</param>
		/// <param name="genericNameNumber">The generic name number for display</param>
		/// <param name="message">The message to display if skill requirement is not met</param>
		public CraftSubRes( Type type, TextDefinition name, double reqSkill, int genericNameNumber, object message )
		{
			m_Type = type;
			m_NameNumber = name;
			m_NameString = name;
			m_ReqSkill = reqSkill;
			m_GenericNameNumber = genericNameNumber;
			m_Message = message;
		}

		#endregion
	}
}
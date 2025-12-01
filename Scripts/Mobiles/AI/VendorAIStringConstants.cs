namespace Server.Mobiles
{
	/// <summary>
	/// Centralized string constants for VendorAI debug messages and labels.
	/// All strings in Portuguese-Brazilian (PT-BR).
	/// Extracted from VendorAI.cs to improve maintainability and enable localization.
	/// </summary>
	public static class VendorAIStringConstants
	{
		#region Debug Messages (Portuguese)
		
		/// <summary>Debug message when vendor is fine</summary>
		public const string DEBUG_I_AM_FINE = "Estou bem";
		
		/// <summary>Debug message format when vendor is attacked: "{0} está me atacando"</summary>
		public const string DEBUG_ATTACKED_BY_FORMAT = "{0} está me atacando";
		
		/// <summary>Debug message format when customer talks to vendor: "{0} falou comigo"</summary>
		public const string DEBUG_TALKED_TO_FORMAT = "{0} falou comigo";
		
		/// <summary>Debug message when customer disappears</summary>
		public const string DEBUG_CUSTOMER_DISAPPEARED = "Meu cliente desapareceu";
		
		/// <summary>Debug message format when vendor is with customer: "Estou com {0}"</summary>
		public const string DEBUG_WITH_CUSTOMER_FORMAT = "Estou com {0}";
		
		/// <summary>Debug message format when customer is gone: "{0} se foi"</summary>
		public const string DEBUG_CUSTOMER_GONE_FORMAT = "{0} se foi";
		
		/// <summary>Debug message when combatant is gone</summary>
		public const string DEBUG_COMBATANT_GONE = "Meu oponente se foi..";
		
		/// <summary>Debug message format when vendor cannot find target: "Não consigo encontrar {0}"</summary>
		public const string DEBUG_CANNOT_FIND_FORMAT = "Não consigo encontrar {0}";
		
		/// <summary>Debug message format when vendor should be closer: "Devo estar mais perto de {0}"</summary>
		public const string DEBUG_SHOULD_BE_CLOSER_FORMAT = "Devo estar mais perto de {0}";
		
		/// <summary>Debug message when vendor is low on health</summary>
		public const string DEBUG_LOW_HEALTH = "Estou com pouca vida!";
		
		#endregion
	}
}


namespace Server.Mobiles
{
	/// <summary>
	/// Centralized constants for VendorAI behavior and speech keyword recognition.
	/// Extracted from VendorAI.cs to improve maintainability and reduce magic numbers.
	/// </summary>
	public static class VendorAIConstants
	{
		#region Speech Keywords
		
		/// <summary>Keyword ID for "vendor sell" speech command</summary>
		public const int KEYWORD_VENDOR_SELL = 0x14D;
		
		/// <summary>Keyword ID for "vendor buy" speech command</summary>
		public const int KEYWORD_VENDOR_BUY = 0x3C;
		
		/// <summary>Keyword ID for "sell" speech command</summary>
		public const int KEYWORD_SELL = 0x177;
		
		/// <summary>Keyword ID for "buy" speech command</summary>
		public const int KEYWORD_BUY = 0x171;
		
		#endregion
		
		#region Speech Range
		
		/// <summary>Maximum range for vendor to handle speech commands (tiles)</summary>
		public const int SPEECH_RANGE = 4;
		
		#endregion
		
		#region Health Thresholds
		
		/// <summary>Low health threshold (10% of max health) - triggers flee behavior</summary>
		public const double LOW_HEALTH_THRESHOLD = 0.1;
		
		#endregion
		
		#region Speech Message IDs
		
		/// <summary>Speech message ID when vendor is attacked (first option)</summary>
		public const int SPEECH_ATTACKED_1 = 1005305;
		
		/// <summary>Speech message ID when vendor is attacked (second option)</summary>
		public const int SPEECH_ATTACKED_2 = 501603;
		
		#endregion
	}
}


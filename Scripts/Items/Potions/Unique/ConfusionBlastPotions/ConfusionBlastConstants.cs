using System;

namespace Server.Items
{
	/// <summary>
	/// Centralized constants for Confusion Blast Potion mechanics.
	/// Extracted from BaseConfusionBlastPotion.cs to improve maintainability.
	/// </summary>
	public static class ConfusionBlastConstants
	{
		#region Blast Radius
		
		/// <summary>Blast radius for regular confusion blast potion (tiles)</summary>
		public const int REGULAR_BLAST_RADIUS = 5;
		
		/// <summary>Blast radius for greater confusion blast potion (tiles)</summary>
		public const int GREATER_BLAST_RADIUS = 7;
		
		#endregion
		
		#region Pacify Effect
		
		/// <summary>Duration creatures remain pacified (seconds)</summary>
		public const double PACIFY_DURATION_SECONDS = 5.0;
		
		#endregion
		
		#region Cooldown
		
		/// <summary>Cooldown between confusion blast uses (seconds)</summary>
		public const int COOLDOWN_SECONDS = 20;
		
		#endregion
		
		#region Throw Mechanics
		
		/// <summary>Maximum range for throwing confusion blast (tiles)</summary>
		public const int THROW_RANGE = 12;
		
		/// <summary>Flying potion item ID during throw animation</summary>
		public const int FLYING_POTION_ITEM_ID = 0xF0D;
		
		/// <summary>Flying potion effect speed</summary>
		public const int FLYING_EFFECT_SPEED = 7;
		
		#endregion
		
		#region Visual Effects
		
		/// <summary>Sound ID for explosion effect</summary>
		public const int EXPLOSION_SOUND_ID = 0x207;
		
		/// <summary>Visual effect ID for blast particles</summary>
		public const int BLAST_EFFECT_ID = 0x376A;
		
		/// <summary>Blast effect animation speed</summary>
		public const int BLAST_EFFECT_SPEED = 4;
		
		/// <summary>Blast effect duration</summary>
		public const int BLAST_EFFECT_DURATION = 9;
		
		/// <summary>Delay before second circle effect wave (seconds)</summary>
		public const double CIRCLE_EFFECT_DELAY = 0.3;
		
		/// <summary>First circle wave start angle (degrees)</summary>
		public const int CIRCLE_START_ANGLE_1 = 270;
		
		/// <summary>First circle wave end angle (degrees)</summary>
		public const int CIRCLE_END_ANGLE_1 = 90;
		
		/// <summary>Second circle wave start angle (degrees)</summary>
		public const int CIRCLE_START_ANGLE_2 = 90;
		
		/// <summary>Second circle wave end angle (degrees)</summary>
		public const int CIRCLE_END_ANGLE_2 = 270;
		
		#endregion
		
		#region Validation
		
		/// <summary>Height check for fit validation</summary>
		public const int FIT_CHECK_HEIGHT = 12;
		
		#endregion
		
	#region Property Display
	
	/// <summary>Color for confusion blast type bracket display (cyan - matches potion/keg color)</summary>
	public const int TYPE_BRACKET_COLOR = 0x8be4fc;
	
	/// <summary>Hue for confusion blast potion and keg (1359 - cyan/turquoise)</summary>
	public const int POTION_KEG_HUE = 0x54F; // 1359 decimal
	
	#endregion
	
	#region Serialization
	
	/// <summary>Current serialization version</summary>
	public const int SERIALIZATION_VERSION = 0;
	
	#endregion
	}
}


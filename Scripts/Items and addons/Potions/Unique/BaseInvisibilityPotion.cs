using System;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
/// <summary>
/// Base class for all invisibility potions (Lesser, Regular, Greater).
/// Provides temporary invisibility with different mechanics based on potion type.
/// 
/// INTEGRATION COMPLETE - ALL HOOKS IMPLEMENTED:
/// - Drinking potions: Hooked in BasePotion.OnDoubleClick() ✅
/// - Grabbing from kegs: Hooked in PotionKeg.OnDoubleClick() ✅
/// - Using skills: Hooked in Server.Skills.UseSkill() ✅
/// - Using tools/items: Hooked in Server.Mobile.Use() ✅
/// - Movement: Hooked in PlayerMobile.Move() via CheckRevealOnMove() ✅
/// - Hiding skill: Hooked in Hiding.OnUse() - reveals from potion, allows skill check ✅
/// - Step depletion: Hooked in PlayerMobile.Move() - prevents auto-refresh for potions ✅
/// 
/// REVEAL BEHAVIOR:
/// - Any action (except invisibility potions) reveals the player immediately
/// - When revealed, effect expires AND "cooldown" resets immediately
/// - Player can drink another invisibility potion right after being revealed
/// - No waiting period - cooldown only prevents stacking, not re-use after reveal
/// - HIDING SKILL: Using Hiding skill while potion is active reveals player from potion
///   -> But allows normal Hiding skill check to proceed
///   -> If skill check succeeds: player becomes hidden via skill-based hiding
///   -> If skill check fails: player remains revealed
///   -> This allows transitioning from potion invisibility to skill-based hiding
/// 
/// BALANCED REVEAL MECHANICS vs SKILL-BASED HIDING:
/// - Lesser Invisibility Potion: 100% reveal chance (easily detected)
/// - Regular Invisibility Potion: 70% reveal chance (moderate protection)
/// - Greater Invisibility Potion: 50% reveal chance (better protection, but still weaker than GM Hiding/Stealth)
/// - Applies to: Reveal spell, Detect Hidden skill (player), Detect Hidden (creatures/NPCs)
/// - Skill-based Hiding/Stealth: 10-80% reveal chance (depends on skill levels)
/// - Investing in Hiding/Stealth skills provides better stealth than even Greater potions
/// - Greater potions offer tactical advantage but cannot match dedicated stealth builds
/// 
/// STEALTH STEP TIERS (Greater Potion only - 50% chance on drink):
/// - Based on BASE Stealth skill (no skill mods applied)
/// - ARMOR RESTRICTION: AR < 42 (same as skill-based stealth) - heavy armor prevents stealth movement
/// - < 30 skill: 2 tiles max
/// - 30-49: 4 tiles
/// - 50-59: 6 tiles
/// - 60-69: 8 tiles
/// - 70-79: 10 tiles
/// - 80-89: 12 tiles
/// - 90-99: 14 tiles
/// - 100-109: 16 tiles
/// - 110-119: 18 tiles
/// - 120 Stealth + 120 Hiding: 21 tiles (elite bonus +3)
/// - Steps do NOT auto-refresh - FIXED count for entire duration (no Stealth.OnUse() called)
/// - When steps reach 0: Player is revealed immediately with message
/// - Rewards skill investment - higher skill = more mobility
/// - WARNING: When steps < 2, player receives warning: "Um movimento em falso e você será revelado!"
/// - DIFFERENCE FROM SKILL-BASED: Skill-based stealth auto-refreshes steps, potions do NOT
/// </summary>
	public abstract class BaseInvisibilityPotion : BasePotion
	{
	#region Abstract Properties

	/// <summary>Gets the duration of invisibility in seconds</summary>
	public abstract int DurationSeconds { get; }

	/// <summary>Gets whether this potion has a chance to allow movement while hidden (stealth)</summary>
	public abstract bool CanAttemptStealth { get; }

	/// <summary>Gets the chance (0.0 to 1.0) to successfully enable stealth on drink</summary>
	public abstract double StealthSuccessChance { get; }

	/// <summary>Gets the type identifier for action locking</summary>
	public abstract Type PotionType { get; }

	/// <summary>
	/// Gets the reveal chance (0-100) when detected by Reveal spell or Detect Hidden
	/// Lesser: 100% (easily detected)
	/// Regular: 70% (moderate protection)
	/// Greater: 50% (better protection)
	/// </summary>
	public abstract int RevealChance { get; }

	#endregion

	#region Constants

	/// <summary>Particle effect Z offset above player head</summary>
	private const int PARTICLE_Z_OFFSET = 16;

	/// <summary>Particle effect ID (shimmer effect)</summary>
	private const int PARTICLE_EFFECT_ID = 0x376A;

	/// <summary>Particle effect count</summary>
	private const int PARTICLE_COUNT = 10;

	/// <summary>Particle effect speed</summary>
	private const int PARTICLE_SPEED = 15;

	/// <summary>Particle effect duration</summary>
	private const int PARTICLE_DURATION = 5045;

	/// <summary>Sound effect when becoming invisible</summary>
	private const int SOUND_EFFECT = 0x3C4;

	/// <summary>Buff icon localized message ID</summary>
	private const int BUFF_MESSAGE_ID = 1075825;

	/// <summary>Maximum armor rating allowed for stealth movement (same as skill-based stealth)</summary>
	private const int MAX_ARMOR_RATING = 42;

	/// <summary>Low steps warning threshold</summary>
	private const int LOW_STEPS_WARNING_THRESHOLD = 2;

	/// <summary>Message color for warnings (yellow)</summary>
	private const int WARNING_COLOR = 0x35;

	// Stealth Step Tiers (based on BASE Stealth skill - NERFED from auto-100)
	// Default (< 30): 2 steps
	private const int DEFAULT_STEPS = 2;
	
	private const int TIER_1_SKILL = 30;   // >= 30: 4 tiles
	private const int TIER_1_STEPS = 4;
	private const int TIER_2_SKILL = 50;   // >= 50: 6 tiles
	private const int TIER_2_STEPS = 6;
	private const int TIER_3_SKILL = 60;   // >= 60: 8 tiles
	private const int TIER_3_STEPS = 8;
	private const int TIER_4_SKILL = 70;   // >= 70: 10 tiles
	private const int TIER_4_STEPS = 10;
	private const int TIER_5_SKILL = 80;   // >= 80: 12 tiles
	private const int TIER_5_STEPS = 12;
	private const int TIER_6_SKILL = 90;   // >= 90: 14 tiles
	private const int TIER_6_STEPS = 14;
	private const int TIER_7_SKILL = 100;  // >= 100: 16 tiles
	private const int TIER_7_STEPS = 16;
	private const int TIER_8_SKILL = 110;  // >= 110: 18 tiles
	private const int TIER_8_STEPS = 18;
	private const int TIER_9_SKILL = 120;  // >= 120: 18 tiles (or 21 with elite bonus)
	private const int TIER_9_STEPS = 18;
	private const int ELITE_STEPS = 21;    // 120 Hiding + 120 Stealth: 21 tiles (elite bonus +3)
	
	private const int ELITE_HIDING_THRESHOLD = 120;
	private const int ELITE_STEALTH_THRESHOLD = 120;

	#endregion

		#region Data Structures

		/// <summary>Thread-safe storage for active invisibility data per mobile</summary>
		private static Dictionary<Mobile, InvisibilityData> m_Table = new Dictionary<Mobile, InvisibilityData>();

	/// <summary>Stores invisibility effect data for a mobile</summary>
	private class InvisibilityData
	{
		public Timer Timer { get; set; }
		public Type PotionType { get; set; }
		public bool StealthEnabled { get; set; } // Rolled once on drink
		public int AllowedSteps { get; set; } // Calculated based on BASE skill
	}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new invisibility potion
		/// </summary>
		/// <param name="itemID">Item ID for the potion bottle</param>
		/// <param name="effect">Potion effect type</param>
		public BaseInvisibilityPotion( int itemID, PotionEffect effect ) : base( itemID, effect )
		{
			Hue = Server.Items.PotionKeg.GetPotionColor( this );
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public BaseInvisibilityPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion

		#region Property Display

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			// Display potion type in custom cyan color (#8be4fc) with brackets
			string potionName = PotionMetadata.GetKegName( this.PotionEffect );
			if ( potionName != null )
			{
				list.Add( 1070722, string.Format( "<BASEFONT COLOR=#8be4fc>[{0}]", potionName ) );
			}
		}

		#endregion

		#region Core Logic

	/// <summary>
	/// Handles drinking the invisibility potion
	/// </summary>
	/// <param name="from">The mobile drinking the potion</param>
	public override void Drink( Mobile from )
	{
		// Check if another invisibility potion is already active (prevents stacking)
		// NOTE: This check fails immediately after being revealed (effect removed from table)
		// Allowing the player to drink another potion right after reveal
		if ( HasActiveEffect( from ) )
		{
			from.SendMessage( 0x22, "Você não pode beber outra poção de invisibilidade ainda." ); // Red
			return;
		}

		// Apply invisibility effect and get stealth steps (if successful)
		int stealthSteps = ApplyInvisibility( from );

		// Play effects
		PlayDrinkEffect( from );
		PlayVisualEffects( from );

		// Ludic message about duration and stealth capability
		if ( CanAttemptStealth )
		{
			if ( stealthSteps > 0 )
			{
				from.SendMessage( 0x59, string.Format( "Você se torna invisível e consegue se mover furtivamente por até {0} passos! O efeito durará {1} segundos...", stealthSteps, DurationSeconds ) ); // Cyan
			}
			else
			{
				from.SendMessage( 0x59, string.Format( "Você se torna invisível, mas não conseguiu dominar o movimento furtivo. O efeito durará {0} segundos...", DurationSeconds ) ); // Cyan
			}
		}
		else
		{
			from.SendMessage( 0x59, string.Format( "Você se torna invisível! O efeito durará {0} segundos...", DurationSeconds ) ); // Cyan
		}

			// Consume potion
			this.Consume();
		}

		#endregion

		#region Helper Methods

	/// <summary>
	/// Checks if a mobile has any active invisibility potion effect
	/// </summary>
	public static bool HasActiveEffect( Mobile m )
	{
		lock ( m_Table )
		{
			return m_Table.ContainsKey( m );
		}
	}

	/// <summary>
	/// Gets the reveal chance for a mobile with an active invisibility potion effect
	/// Returns the reveal chance (0-100) based on potion type:
	/// Lesser: 100%, Regular: 70%, Greater: 50%
	/// Returns 0 if no active effect
	/// </summary>
	public static int GetRevealChance( Mobile m )
	{
		InvisibilityData data = null;

		lock ( m_Table )
		{
			if ( m_Table.TryGetValue( m, out data ) )
			{
				// Get the potion type from stored data
				Type potionType = data.PotionType;

				// Map potion type to reveal chance
				if ( potionType == typeof( LesserInvisibilityPotion ) )
					return 100; // Lesser: 100% reveal chance
				else if ( potionType == typeof( InvisibilityPotion ) )
					return 70;  // Regular: 70% reveal chance
				else if ( potionType == typeof( GreaterInvisibilityPotion ) )
					return 50;  // Greater: 50% reveal chance
			}
		}

		return 0; // No active effect
	}

	/// <summary>
	/// Calculates stealth steps based on BASE Stealth skill (and Hiding for elite bonus)
	/// Tiered system that rewards skill investment:
	/// < 30: 2 tiles, 30-49: 4 tiles, 50-59: 6 tiles, 60-69: 8 tiles, 70-79: 10 tiles,
	/// 80-89: 12 tiles, 90-99: 14 tiles, 100-109: 16 tiles, 110-119: 18 tiles,
	/// 120 Stealth + 120 Hiding: 21 tiles (elite bonus +3)
	/// </summary>
	/// <param name="from">The mobile to calculate steps for</param>
	/// <returns>Number of steps allowed for stealth movement</returns>
	private int CalculateStealthSteps( Mobile from )
	{
		// Get BASE skill values (before any mods/buffs)
		double stealthSkill = from.Skills[SkillName.Stealth].Base;
		double hidingSkill = from.Skills[SkillName.Hiding].Base;

		int steps = DEFAULT_STEPS; // Default: 2 steps for < 30 skill

		// Tiered system - rewards skill investment (check from highest to lowest)
		if ( stealthSkill >= TIER_9_SKILL ) // >= 120
		{
			steps = TIER_9_STEPS; // 18 tiles
			
			// Elite bonus: Both Hiding AND Stealth at 120+ = +3 bonus (21 total)
			if ( hidingSkill >= ELITE_HIDING_THRESHOLD && stealthSkill >= ELITE_STEALTH_THRESHOLD )
			{
				steps = ELITE_STEPS; // 21 tiles
			}
		}
		else if ( stealthSkill >= TIER_8_SKILL ) // >= 110
			steps = TIER_8_STEPS; // 18 tiles
		else if ( stealthSkill >= TIER_7_SKILL ) // >= 100
			steps = TIER_7_STEPS; // 16 tiles
		else if ( stealthSkill >= TIER_6_SKILL ) // >= 90
			steps = TIER_6_STEPS; // 14 tiles
		else if ( stealthSkill >= TIER_5_SKILL ) // >= 80
			steps = TIER_5_STEPS; // 12 tiles
		else if ( stealthSkill >= TIER_4_SKILL ) // >= 70
			steps = TIER_4_STEPS; // 10 tiles
		else if ( stealthSkill >= TIER_3_SKILL ) // >= 60
			steps = TIER_3_STEPS; // 8 tiles
		else if ( stealthSkill >= TIER_2_SKILL ) // >= 50
			steps = TIER_2_STEPS; // 6 tiles
		else if ( stealthSkill >= TIER_1_SKILL ) // >= 30
			steps = TIER_1_STEPS; // 4 tiles
		// else: < 30 = DEFAULT_STEPS (2 tiles)

		return steps;
	}

	/// <summary>
	/// Applies invisibility effect to the mobile
	/// </summary>
	/// <returns>Number of stealth steps allowed (0 if stealth roll failed or not Greater potion)</returns>
	private int ApplyInvisibility( Mobile from )
		{
			// Remove any existing invisibility effect
			RemoveEffect( from );

			// Make invisible
			from.Hidden = true;
			from.Combatant = null;
			from.Warmode = false;
			from.Delta( MobileDelta.Flags );

	// Roll stealth chance ONCE on drink (for Greater potion only)
	bool stealthEnabled = false;
	int allowedSteps = 0;

	if ( CanAttemptStealth )
	{
		// Check armor rating (same restriction as skill-based stealth)
		int armorRating = SkillHandlers.Stealth.GetArmorRating( from );

		if ( armorRating >= MAX_ARMOR_RATING )
		{
			// Too much armor - no stealth movement allowed
			from.SendMessage( 0x22, "Você está usando armadura muito pesada para se mover furtivamente!" ); // Red
			stealthEnabled = false;
		}
		else
		{
			// Roll the 50% chance ONCE when drinking
			stealthEnabled = ( Utility.RandomDouble() <= StealthSuccessChance );

			if ( stealthEnabled )
			{
				// Success! Calculate steps based on BASE Stealth skill (NERFED - no longer boosts to 100)
				allowedSteps = CalculateStealthSteps( from );
				from.AllowedStealthSteps = allowedSteps;

				// Mark player as stealthing (required for movement detection)
				PlayerMobile pm = from as PlayerMobile;
				if ( pm != null )
					pm.IsStealthing = true;
			}
		}
	}

			// Create timer
			TimeSpan duration = TimeSpan.FromSeconds( DurationSeconds );
			Timer timer = new InternalTimer( from, duration );

		// Store data
		InvisibilityData data = new InvisibilityData
		{
			Timer = timer,
			PotionType = PotionType,
			StealthEnabled = stealthEnabled,
			AllowedSteps = allowedSteps
		};

			lock ( m_Table )
			{
				m_Table[from] = data;
			}

		// Add buff icon
		BuffInfo.RemoveBuff( from, BuffIcon.HidingAndOrStealth );
		BuffInfo.AddBuff( from, new BuffInfo( BuffIcon.Invisibility, BUFF_MESSAGE_ID, duration, from ) );

		// Start timer
		timer.Start();

		// Register action lock (all potion types)
		from.BeginAction( PotionType );

		return allowedSteps; // Return step count (0 if stealth failed or not Greater potion)
	}

	/// <summary>
	/// Removes invisibility effect from a mobile
	/// IMPORTANT: This method resets the "cooldown" by:
	/// - Removing mobile from active effects table (allows HasActiveEffect to return false)
	/// - Ending the action lock (allows BeginAction to succeed)
	/// - Player can immediately drink another invisibility potion after being revealed
	/// </summary>
	/// <param name="from">The mobile to remove the effect from</param>
	public static void RemoveEffect( Mobile from )
	{
		InvisibilityData data = null;

		lock ( m_Table )
		{
			if ( m_Table.TryGetValue( from, out data ) )
			{
				m_Table.Remove( from ); // COOLDOWN RESET: Remove from active effects
			}
		}

		if ( data != null )
		{
			// Stop duration timer (effect expires immediately)
			if ( data.Timer != null )
			{
				data.Timer.Stop();
			}

			// Reset stealth state
			PlayerMobile pm = from as PlayerMobile;
			if ( pm != null )
			{
				pm.IsStealthing = false;
			}

			// Reset allowed stealth steps
			from.AllowedStealthSteps = 0;

			// COOLDOWN RESET: End action lock (allows drinking another potion immediately)
			from.EndAction( data.PotionType );

			// Reveal player (if still hidden)
			if ( from.Hidden && from.AccessLevel == AccessLevel.Player )
			{
				from.Hidden = false;
				from.Delta( MobileDelta.Flags );
				BuffInfo.RemoveBuff( from, BuffIcon.Invisibility );
			}
		}
	}

	/// <summary>
	/// Checks if mobile should be revealed on movement
	/// For Lesser/Regular: Always reveal on movement
	/// For Greater: Only reveal if stealth was NOT enabled on drink (rolled once)
	/// When revealed: Effect expires AND cooldown resets (can drink another potion immediately)
	/// Also sends warning when steps remaining < 2
	/// </summary>
	public static void CheckRevealOnMove( Mobile from )
	{
		InvisibilityData data = null;

		lock ( m_Table )
		{
			if ( m_Table.TryGetValue( from, out data ) )
			{
				// If stealth was NOT enabled (failed the initial 50% roll or Lesser/Regular potion)
				// then movement reveals the player
				if ( !data.StealthEnabled )
				{
					from.SendMessage( 0x22, "Você se moveu e foi revelado!" ); // Red
					RemoveEffect( from ); // Expires effect + resets cooldown
				}
				else
				{
					// If stealth WAS enabled (Greater potion + successful 50% roll)
					// Check if steps are running low and send warning
					if ( from.AllowedStealthSteps > 0 && from.AllowedStealthSteps < LOW_STEPS_WARNING_THRESHOLD )
					{
						from.SendMessage( WARNING_COLOR, "Um movimento em falso e você será revelado!" ); // Yellow warning
					}
					// Stealth skill determines movement distance (handled by core Stealth mechanics)
					// No reveal happens here - player can move based on their Stealth skill
				}
			}
		}
	}

	/// <summary>
	/// Checks if mobile should be revealed when performing an action
	/// Actions include: drinking potions, double-clicking items/kegs, using skills, using tools
	/// Always reveals regardless of potion type or stealth status
	/// When revealed: Effect expires AND cooldown resets (can drink another potion immediately)
	/// </summary>
	/// <param name="from">The mobile performing the action</param>
	/// <param name="actionDescription">Description of the action for the message (optional)</param>
	/// <returns>True if the player had an active invisibility effect that was removed</returns>
	public static bool CheckRevealOnAction( Mobile from, string actionDescription = null )
	{
		if ( !HasActiveEffect( from ) )
			return false;

		// Reveal player with appropriate message
		if ( !string.IsNullOrEmpty( actionDescription ) )
		{
			from.SendMessage( 0x22, string.Format( "Você {0} e foi revelado!", actionDescription ) ); // Red
		}
		else
		{
			from.SendMessage( 0x22, "Sua ação revelou você!" ); // Red
		}

		RemoveEffect( from ); // Expires effect + resets cooldown
		return true;
	}

		/// <summary>
		/// Plays visual effects (same as Invisibility spell)
		/// </summary>
		private void PlayVisualEffects( Mobile from )
		{
			Effects.SendLocationParticles( 
				EffectItem.Create( new Point3D( from.X, from.Y, from.Z + PARTICLE_Z_OFFSET ), from.Map, EffectItem.DefaultDuration ), 
				PARTICLE_EFFECT_ID, 
				PARTICLE_COUNT, 
				PARTICLE_SPEED, 
				Server.Items.CharacterDatabase.GetMySpellHue( from, 0 ), 
				0, 
				PARTICLE_DURATION, 
				0 
			);
			
			from.PlaySound( SOUND_EFFECT );
		}

		#endregion

		#region Internal Timer

		/// <summary>
		/// Timer that handles invisibility duration and expiration
		/// When timer expires: Effect ends AND cooldown resets (can drink another potion immediately)
		/// </summary>
		private class InternalTimer : Timer
		{
			private Mobile m_Mobile;

			public InternalTimer( Mobile mobile, TimeSpan duration ) : base( duration )
			{
				m_Mobile = mobile;
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				// Check if effect is still active
				bool shouldReveal = false;
				lock ( m_Table )
				{
					shouldReveal = m_Table.ContainsKey( m_Mobile );
				}

				if ( shouldReveal )
				{
					m_Mobile.SendMessage( 0x22, "O efeito de invisibilidade expirou!" ); // Red
					RemoveEffect( m_Mobile ); // Expires effect + resets cooldown
				}
			}
		}

		#endregion
	}
}


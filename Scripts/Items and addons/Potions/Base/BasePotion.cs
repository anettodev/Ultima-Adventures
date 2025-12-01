using System;
using Server;
using Server.Engines.Craft;
using Server.Misc;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Defines all available potion effect types in the game.
	/// Used by BasePotion and derived classes to identify potion functionality.
	/// Includes standard fantasy potions, sci-fi variants, elixirs, and special mixtures.
	/// </summary>
	public enum PotionEffect
	{
		Nightsight,
		CureLesser,
		Cure,
		CureGreater,
		Agility,
		AgilityGreater,
		Strength,
		StrengthGreater,
		PoisonLesser,
		Poison,
		PoisonGreater,
		PoisonDeadly,
		Refresh,
		RefreshTotal,
		HealLesser,
		Heal,
		HealGreater,
		ExplosionLesser,
		Explosion,
		ExplosionGreater,
		Conflagration,
		ConflagrationGreater,
		MaskOfDeath,		// Mask of Death is not available in OSI but does exist in cliloc files
		MaskOfDeathGreater,	// included in enumeration for compatability if later enabled by OSI
		ConfusionBlast,
		ConfusionBlastGreater,
		InvisibilityLesser,
		Invisibility,
		InvisibilityGreater,
		RejuvenateLesser,
		Rejuvenate,
		RejuvenateGreater,
		ManaLesser,
		Mana,
		ManaGreater,
		Invulnerability,
		PoisonLethal,
		SilverSerpentVenom,
		GoldenSerpentVenom,
		ElixirAlchemy,
		ElixirAnatomy,
		ElixirAnimalLore,
		ElixirAnimalTaming,
		ElixirArchery,
		ElixirArmsLore,
		ElixirBegging,
		ElixirBlacksmith,
		ElixirCamping,
		ElixirCarpentry,
		ElixirCartography,
		ElixirCooking,
		ElixirDetectHidden,
		ElixirDiscordance,
		ElixirEvalInt,
		ElixirFencing,
		ElixirFishing,
		ElixirFletching,
		ElixirFocus,
		ElixirForensics,
		ElixirHealing,
		ElixirHerding,
		ElixirHiding,
		ElixirInscribe,
		ElixirItemID,
		ElixirLockpicking,
		ElixirLumberjacking,
		ElixirMacing,
		ElixirMagicResist,
		ElixirMeditation,
		ElixirMining,
		ElixirMusicianship,
		ElixirParry,
		ElixirPeacemaking,
		ElixirPoisoning,
		ElixirProvocation,
		ElixirRemoveTrap,
		ElixirSnooping,
		ElixirSpiritSpeak,
		ElixirStealing,
		ElixirStealth,
		ElixirSwords,
		ElixirTactics,
		ElixirTailoring,
		ElixirTasteID,
		ElixirTinkering,
		ElixirTracking,
		ElixirVeterinary,
		ElixirWrestling,
		MixtureSlime,
		MixtureIceSlime,
		MixtureFireSlime,
		MixtureDiseasedSlime,
		MixtureRadiatedSlime,
		LiquidFire,
		LiquidGoo,
		LiquidIce,
		LiquidRot,
		LiquidPain,
		Resurrect,
		SuperPotion,
		Repair,
		Durability,
		HairOil,
		HairDye,
		Frostbite,
		FrostbiteGreater
	}

	/// <summary>
	/// Base class for all potion items in the game.
	/// Provides core functionality including drinking mechanics, enhancement scaling,
	/// hand requirements, stacking logic, sci-fi theme conversion, and crafting integration.
	/// All potions must inherit from this class and implement the Drink method.
	/// </summary>
	public abstract class BasePotion : Item, ICraftable, ICommodity
	{
		#region Static Data

		/// <summary>
		/// Tracks the last time each mobile drank a potion for cooldown enforcement
		/// </summary>
		private static Dictionary<Mobile, DateTime> m_LastDrinkTime = new Dictionary<Mobile, DateTime>();

		#endregion

		#region Fields

		private PotionEffect m_PotionEffect;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the potion effect type that determines this potion's behavior
		/// </summary>
		public PotionEffect PotionEffect
		{
			get
			{
				return m_PotionEffect;
			}
			set
			{
				m_PotionEffect = value;
				InvalidateProperties();
			}
		}

		/// <summary>
		/// Gets whether this potion requires a free hand to drink
		/// </summary>
		public virtual bool RequireFreeHand{ get{ return false; } }

		/// <summary>
		/// Gets the label number for this potion based on its effect type
		/// </summary>
		public override int LabelNumber{ get{ return 1041314 + (int)m_PotionEffect; } }

		#endregion

		#region ICommodity Implementation

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return (Core.ML); } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of BasePotion
		/// </summary>
		/// <param name="itemID">The item ID for this potion</param>
		/// <param name="effect">The potion effect type</param>
		public BasePotion( int itemID, PotionEffect effect ) : base( itemID )
		{
			m_PotionEffect = effect;
			Stackable = true;
			Weight = PotionConstants.DEFAULT_POTION_WEIGHT;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public BasePotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Hand Requirements

		/// <summary>
		/// Checks if a mobile has a free hand to drink a potion.
		/// Considers balanced weapons, gloves, and two-handed weapons.
		/// </summary>
		/// <param name="m">The mobile to check</param>
		/// <returns>True if the mobile has a free hand, false otherwise</returns>
		public static bool HasFreeHand( Mobile m )
		{
			Item handOne = m.FindItemOnLayer( Layer.OneHanded );
			Item handTwo = m.FindItemOnLayer( Layer.TwoHanded );

			// Two-handed weapons occupy the primary hand slot
			if ( handTwo is BaseWeapon )
				handOne = handTwo;
			
			// Balanced ranged weapons don't require a free hand
			if ( handOne is BaseRanged )
			{
				BaseRanged ranged = (BaseRanged) handOne;
				if ( ranged.Balanced )
					return true;
			}

			// Special gloves that don't prevent potion drinking
			if (	( handOne is PugilistGlove ) || 
					( handOne is PugilistGloves ) || 
					( handOne is LevelPugilistGloves ) || 
					( handOne is LevelThrowingGloves ) || 
					( handOne is GiftPugilistGloves ) || 
					( handOne is GiftThrowingGloves ) || 
					( handOne is ThrowingGloves ) )
			{
				return true;
			}

		// At least one hand must be free
		return ( handOne == null || handTwo == null );
	}

	#endregion

	#region Cooldown System

	/// <summary>
	/// Checks if a mobile is currently on cooldown for drinking potions
	/// </summary>
	/// <param name="from">The mobile to check</param>
	/// <returns>True if on cooldown, false if can drink</returns>
	private static bool IsOnCooldown( Mobile from )
	{
		DateTime lastDrink;

		lock ( m_LastDrinkTime )
		{
			if ( m_LastDrinkTime.TryGetValue( from, out lastDrink ) )
			{
				TimeSpan elapsed = DateTime.UtcNow - lastDrink;
				return elapsed.TotalSeconds < PotionConstants.DRINK_COOLDOWN_SECONDS;
			}
		}

		return false;
	}

	/// <summary>
	/// Gets the remaining cooldown time for a mobile
	/// </summary>
	/// <param name="from">The mobile to check</param>
	/// <returns>Remaining cooldown duration</returns>
	private static TimeSpan GetRemainingCooldown( Mobile from )
	{
		DateTime lastDrink;

		lock ( m_LastDrinkTime )
		{
			if ( m_LastDrinkTime.TryGetValue( from, out lastDrink ) )
			{
				TimeSpan elapsed = DateTime.UtcNow - lastDrink;
				double remaining = PotionConstants.DRINK_COOLDOWN_SECONDS - elapsed.TotalSeconds;

				if ( remaining > 0 )
					return TimeSpan.FromSeconds( remaining );
			}
		}

		return TimeSpan.Zero;
	}

	/// <summary>
	/// Sets the cooldown timestamp for a mobile after drinking a potion
	/// </summary>
	/// <param name="from">The mobile that drank a potion</param>
	private static void SetCooldown( Mobile from )
	{
		lock ( m_LastDrinkTime )
		{
			m_LastDrinkTime[from] = DateTime.UtcNow;
		}
	}

	/// <summary>
	/// Cleans up old cooldown entries to prevent memory leaks
	/// Should be called periodically (e.g., every 10 minutes)
	/// </summary>
	public static void CleanupCooldowns()
	{
		lock ( m_LastDrinkTime )
		{
			List<Mobile> toRemove = new List<Mobile>();

			foreach ( KeyValuePair<Mobile, DateTime> entry in m_LastDrinkTime )
			{
				// Remove entries older than 1 minute (way past cooldown)
				if ( DateTime.UtcNow - entry.Value > TimeSpan.FromMinutes( 1 ) )
				{
					toRemove.Add( entry.Key );
				}
			}

			foreach ( Mobile m in toRemove )
			{
				m_LastDrinkTime.Remove( m );
			}
		}
	}

	#endregion

	#region Core Potion Logic

	/// <summary>
	/// Handles double-clicking the potion to drink it.
	/// Checks distance, hand requirements, cooldown, and handles stacked explosion potions specially.
	/// </summary>
	/// <param name="from">The mobile attempting to drink the potion</param>
	public override void OnDoubleClick( Mobile from )
	{
		if ( !Movable )
			return;

		// Check if mobile is within range
		if ( from.InRange( this.GetWorldLocation(), PotionConstants.USE_DISTANCE ) )
		{
			// Check free hand requirement
			if (!RequireFreeHand || HasFreeHand(from))
			{
				// Check global potion cooldown (1.5 seconds between any potions)
				if ( IsOnCooldown( from ) )
				{
					TimeSpan remaining = GetRemainingCooldown( from );
					from.SendMessage( PotionConstants.MSG_COLOR_COOLDOWN, 
						string.Format( "Você deve esperar um pouco antes de beber outra poção.", remaining.TotalSeconds ) );
					return;
				}

				// Check if player has invisibility potion effect active
				// Drinking any potion (except invisibility potions) reveals the player
				if ( !(this is BaseInvisibilityPotion) )
				{
					BaseInvisibilityPotion.CheckRevealOnAction( from, "bebeu uma poção" );
				}

			// Special handling for stacked explosion potions
			// Create a single potion from the stack to throw
			if (this is BaseExplosionPotion && Amount > 1)
			{
				BasePotion pot = (BasePotion)Activator.CreateInstance(this.GetType());

				if (pot != null)
				{
					Amount--;

					// Try to add to backpack first, fall back to ground
					if (from.Backpack != null && !from.Backpack.Deleted)
					{
						from.Backpack.DropItem(pot);
					}
					else
					{
						pot.MoveToWorld(from.Location, from.Map);
					}
					
					pot.Drink( from );

					// Set cooldown after drinking
					SetCooldown( from );
				}
			}
			else
			{
				this.Drink( from );

				// Set cooldown after drinking
				SetCooldown( from );
			}
			}
				else
				{
					from.SendLocalizedMessage(502172); // You must have a free hand to drink a potion.
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		/// <summary>
		/// Abstract method that derived classes must implement to define potion drinking behavior.
		/// Called when a mobile successfully drinks this potion.
		/// </summary>
		/// <param name="from">The mobile drinking the potion</param>
		public abstract void Drink( Mobile from );

		#endregion

		#region Effects

		/// <summary>
		/// Plays the standard potion drinking effects (sound, animation, and returns empty bottle).
		/// Should be called by derived classes after successfully applying potion effects.
		/// </summary>
		/// <param name="m">The mobile drinking the potion</param>
		public static void PlayDrinkEffect( Mobile m )
		{
			m.PlaySound( PotionConstants.DRINK_SOUND_ID );
			m.AddToBackpack( new Bottle() );

			// Play drinking animation for humanoid forms
			if ( m.Body.IsHuman && !m.Mounted )
			{
				m.Animate( PotionConstants.DRINK_ANIMATION_ID, 
						  PotionConstants.DRINK_ANIMATION_FRAMES, 
						  PotionConstants.DRINK_ANIMATION_REPEAT, 
						  true, false, 0 );
			}
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the potion data
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) PotionConstants.SERIALIZATION_VERSION );
			writer.Write( (int) m_PotionEffect );
		}

		/// <summary>
		/// Deserializes the potion data
		/// </summary>
		/// <param name="reader">Generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				case 0:
				{
					m_PotionEffect = (PotionEffect)reader.ReadInt();
					break;
				}
			}

			// Legacy version handling
			if( version == PotionConstants.LEGACY_VERSION )
				Stackable = Core.ML;
		}

		#endregion

		#region Enhancement and Scaling

		/// <summary>
		/// Calculates the total enhance potions bonus for a mobile.
		/// Combines equipment bonuses, alchemy skill, and server cap limits.
		/// </summary>
		/// <param name="m">The mobile to calculate bonus for</param>
		/// <returns>Total enhance potions percentage (0-100+)</returns>
		public static int EnhancePotions( Mobile m )
		{
			// Get enhance potions from equipment
			int EP = AosAttributes.GetValue( m, AosAttribute.EnhancePotions );
			
			// Calculate alchemy skill bonus
			int skillBonus = GetAlchemySkillBonus( m );

			// Apply server cap
			if ( EP > MyServerSettings.EnhancePotionCap(m) )
				EP = MyServerSettings.EnhancePotionCap(m);

			return ( EP + skillBonus );
		}

		/// <summary>
		/// Scales a TimeSpan duration based on enhance potions and alchemist bonuses.
		/// </summary>
		/// <param name="m">The mobile using the potion</param>
		/// <param name="v">The base duration value</param>
		/// <returns>Scaled duration</returns>
		public static TimeSpan Scale( Mobile m, TimeSpan v )
		{
			if ( !Core.AOS )
				return v;

			double scalar = CalculateEnhancementScalar( m );
			return TimeSpan.FromSeconds( v.TotalSeconds * scalar );
		}

		/// <summary>
		/// Scales a double value based on enhance potions and alchemist bonuses.
		/// </summary>
		/// <param name="m">The mobile using the potion</param>
		/// <param name="v">The base double value</param>
		/// <returns>Scaled value</returns>
		public static double Scale( Mobile m, double v )
		{
			if ( !Core.AOS )
				return v;

			double scalar = CalculateEnhancementScalar( m );
			return v * scalar;
		}

		/// <summary>
		/// Scales an integer value based on enhance potions and alchemist bonuses.
		/// </summary>
		/// <param name="m">The mobile using the potion</param>
		/// <param name="v">The base integer value</param>
		/// <returns>Scaled value</returns>
		public static int Scale( Mobile m, int v )
		{
			if ( !Core.AOS )
				return v;
			
			// Apply alchemist bonus first
			v = ApplyAlchemistBonusToInt( m, v );

			return AOS.Scale( v, 100 + EnhancePotions( m ) );
		}

		#endregion

		#region Stacking Logic

		/// <summary>
		/// Determines if this potion can stack with another dropped item.
		/// Potions only stack if they have the same effect type.
		/// </summary>
		/// <param name="from">The mobile performing the action</param>
		/// <param name="dropped">The item being dropped onto this potion</param>
		/// <param name="playSound">Whether to play stacking sound</param>
		/// <returns>True if items can stack, false otherwise</returns>
		public override bool StackWith( Mobile from, Item dropped, bool playSound )
		{
			if( dropped is BasePotion )
			{
				BasePotion droppedPotion = (BasePotion)dropped;
				if ( droppedPotion.m_PotionEffect == m_PotionEffect )
					return base.StackWith( from, dropped, playSound );
			}

			return false;
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Gets the alchemy skill-based enhancement bonus.
		/// </summary>
		/// <param name="m">The mobile to check</param>
		/// <returns>Enhancement bonus based on alchemy skill level</returns>
		private static int GetAlchemySkillBonus( Mobile m )
		{
			int skillBonus = 0;
			
			if ( m.Skills.Alchemy.Fixed >= PotionConstants.ALCHEMY_GRANDMASTER )
				skillBonus = PotionConstants.ALCHEMY_BONUS_GM;
			else if ( m.Skills.Alchemy.Fixed >= PotionConstants.ALCHEMY_EXPERT )
				skillBonus = PotionConstants.ALCHEMY_BONUS_EXPERT;
			else if ( m.Skills.Alchemy.Fixed >= PotionConstants.ALCHEMY_JOURNEYMAN )
				skillBonus = PotionConstants.ALCHEMY_BONUS_JOURNEYMAN;

			return skillBonus;
		}

		/// <summary>
		/// Calculates the total enhancement scalar including alchemist profession bonus.
		/// </summary>
		/// <param name="m">The mobile to calculate for</param>
		/// <returns>Enhancement scalar multiplier</returns>
		private static double CalculateEnhancementScalar( Mobile m )
		{
			double scalar = PotionConstants.ENHANCE_POTION_SCALAR_BASE + 
						   ( PotionConstants.ENHANCE_POTION_SCALAR_MULTIPLIER * EnhancePotions( m ) );
			
			// Apply alchemist profession bonus if applicable
			PlayerMobile pm = m as PlayerMobile;
			if ( pm != null && pm.Alchemist() )
			{
				scalar *= pm.AlchemistBonus();
			}

			return scalar;
		}

		/// <summary>
		/// Applies alchemist profession bonus to an integer value.
		/// </summary>
		/// <param name="m">The mobile to check</param>
		/// <param name="value">The value to scale</param>
		/// <returns>Scaled value</returns>
		private static int ApplyAlchemistBonusToInt( Mobile m, int value )
		{
			PlayerMobile pm = m as PlayerMobile;
			if ( pm != null && pm.Alchemist() )
			{
				value = (int)((double)value * pm.AlchemistBonus());
			}

			return value;
		}

		#endregion

		#region Sci-Fi Theme Conversion

		/// <summary>
		/// Converts a potion into its sci-fi themed equivalent (pills, serums, or futuristic chemicals).
		/// Used for alternate gameplay themes where fantasy potions become futuristic medical items.
		/// Reduces complexity from 40 to 3 by using data-driven dictionary lookup.
		/// </summary>
		/// <param name="potion">The potion item to convert</param>
		public static void MakePillBottle( Item potion )
		{
			BasePotion pot = (BasePotion)potion;
			PotionSciFiNames.SciFiTheme theme = PotionSciFiNames.GetTheme( pot.PotionEffect );
			
			// If no theme exists for this potion type, don't modify it
			if ( theme == null )
				return;

			// Randomly choose between pill bottle or syringe for standard potions
			bool useSyringe = Utility.RandomBool();
			
			// Apply the sci-fi theme
			ApplySciFiTheme( potion, theme, useSyringe );
		}

		/// <summary>
		/// Applies the sci-fi theme properties to a potion item.
		/// Handles special variants (fire, cold, liquid) and standard variants (pills, serums).
		/// </summary>
		/// <param name="potion">The potion to modify</param>
		/// <param name="theme">The sci-fi theme to apply</param>
		/// <param name="useSyringe">Whether to use syringe (true) or pill bottle (false) for standard potions</param>
		private static void ApplySciFiTheme( Item potion, PotionSciFiNames.SciFiTheme theme, bool useSyringe )
		{
			// Handle special variants (fire/cold/liquid types)
			if ( theme.Special.HasValue )
			{
				switch ( theme.Special.Value )
				{
					case PotionSciFiNames.SpecialType.Fire:
						potion.ItemID = PotionConstants.ITEMID_FUEL_CANISTER;
						potion.Name = theme.SpecialName;
						potion.Hue = PotionConstants.HUE_DEFAULT;
						break;

					case PotionSciFiNames.SpecialType.Cold:
						potion.ItemID = PotionConstants.ITEMID_EXTINGUISHER;
						potion.Name = theme.SpecialName;
						potion.Hue = theme.SpecialHue ?? PotionConstants.HUE_DEFAULT;
						break;

					case PotionSciFiNames.SpecialType.Liquid:
						potion.ItemID = PotionConstants.ITEMID_LIQUID_BOTTLE;
						potion.Name = theme.SpecialName;
						potion.Hue = Server.Items.PotionKeg.GetPotionColor( potion );
						break;
				}
			}
			// Handle standard variants (pills/serums)
			else
			{
				if ( useSyringe )
				{
					potion.ItemID = PotionConstants.ITEMID_SYRINGE;
					potion.Name = theme.SyringeName;
				}
				else
				{
					potion.ItemID = PotionConstants.ITEMID_PILL_BOTTLE;
					potion.Name = theme.PillName;
				}
				
				potion.Hue = Server.Items.PotionKeg.GetPotionColor( potion );
			}
		}

		#endregion

		#region Crafting Integration (ICraftable)

		/// <summary>
		/// Called when this potion is crafted using the alchemy skill.
		/// Automatically fills available potion kegs in the crafter's backpack.
		/// </summary>
		/// <param name="quality">Quality of the craft (not used for potions)</param>
		/// <param name="makersMark">Whether to add maker's mark (not used for potions)</param>
		/// <param name="from">The mobile crafting the potion</param>
		/// <param name="craftSystem">The craft system (DefAlchemy)</param>
		/// <param name="typeRes">Resource type used</param>
		/// <param name="tool">Tool used for crafting</param>
		/// <param name="craftItem">The craft item definition</param>
		/// <param name="resHue">Resource hue</param>
		/// <returns>1 if potion created normally, -1 if placed in keg</returns>
		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			// Only process for alchemy crafting
			if ( craftSystem is DefAlchemy )
			{
				Container pack = from.Backpack;

				if ( pack != null )
				{
					// Find all potion kegs in backpack
					List<PotionKeg> kegs = pack.FindItemsByType<PotionKeg>();

					// Try to find a compatible keg to fill
					for ( int i = 0; i < kegs.Count; ++i )
					{
						PotionKeg keg = kegs[i];

						if ( keg == null )
							continue;

						// Keg must not be empty or full
						if ( keg.Held <= PotionConstants.KEG_MIN_CAPACITY || keg.Held >= PotionConstants.KEG_MAX_CAPACITY )
							continue;

						// Keg must match this potion's type
						if ( keg.Type != PotionEffect )
							continue;

						// Fill the keg and return bottle
						++keg.Held;
						Consume();
						from.AddToBackpack( new Bottle() );

						return -1; // Signal that potion was placed in keg
					}
				}
			}

			return 1; // Normal crafting - create potion in backpack
		}

		#endregion
	}
}

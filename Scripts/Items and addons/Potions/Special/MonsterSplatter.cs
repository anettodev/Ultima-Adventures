using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	/// <summary>
	/// Represents a monster splatter item that damages mobiles when stepped on.
	/// Supports 30+ different splatter types with unique damage and effect patterns.
	/// </summary>
	public class MonsterSplatter : Item
	{
		#region Fields

		/// <summary>The mobile that created this splatter</summary>
		public Mobile owner;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the owner of this splatter
		/// </summary>
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of MonsterSplatter
		/// </summary>
		/// <param name="source">The mobile that created this splatter</param>
		[Constructable]
		public MonsterSplatter( Mobile source ) : base( MonsterSplatterConstants.ITEM_ID_BASE )
		{
			Weight = MonsterSplatterConstants.WEIGHT_NORMAL;
			Movable = false;
			owner = source;
			Name = MonsterSplatterStringConstants.NAME_DEFAULT;
			ItemID = Utility.RandomList( 
				MonsterSplatterConstants.ITEM_ID_BASE, 
				MonsterSplatterConstants.ITEM_ID_BASE, 
				MonsterSplatterConstants.ITEM_ID_BASE, 
				MonsterSplatterConstants.ITEM_ID_ALT_1, 
				MonsterSplatterConstants.ITEM_ID_ALT_2, 
				MonsterSplatterConstants.ITEM_ID_ALT_3, 
				MonsterSplatterConstants.ITEM_ID_ALT_4, 
				MonsterSplatterConstants.ITEM_ID_ALT_5, 
				MonsterSplatterConstants.ITEM_ID_ALT_6, 
				MonsterSplatterConstants.ITEM_ID_ALT_7, 
				MonsterSplatterConstants.ITEM_ID_ALT_8, 
				MonsterSplatterConstants.ITEM_ID_ALT_9 
			);
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start(); 
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public MonsterSplatter(Serial serial) : base(serial)
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Calculates damage for a mobile, applying alchemy bonus for players
		/// </summary>
		/// <param name="m">The mobile to calculate damage for</param>
		/// <param name="min">Minimum damage</param>
		/// <param name="max">Maximum damage</param>
		/// <returns>Calculated damage amount</returns>
		public static int Hurt( Mobile m, int min, int max )
		{
			if ( m is PlayerMobile )
			{
				int alchemySkill = (int)(Server.Items.BasePotion.EnhancePotions( m ) / MonsterSplatterConstants.SKILL_DIVISOR_ENHANCE_POTIONS);
				return Utility.RandomMinMax( min, max ) + alchemySkill;
			}
			
			return Utility.RandomMinMax( min, max );
		}

		/// <summary>
		/// Handles when a mobile moves over this splatter
		/// </summary>
		/// <param name="m">The mobile that moved over</param>
		/// <returns>True if the move was allowed</returns>
		public override bool OnMoveOver( Mobile m )
		{
			if ( !CanHurtTarget( m ) )
				return true;

			HandleSplatterEffect( m );
			return true;
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Determines if the target can be hurt by this splatter
		/// </summary>
		/// <param name="m">The target mobile</param>
		/// <returns>True if target can be hurt</returns>
		private bool CanHurtTarget( Mobile m )
		{
			if ( m.Blessed )
				return false;

			if ( !m.Alive )
				return false;

			if ( owner is BaseCreature && m is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)m;
				if ( !bc.Controlled )
					return false;
			}

			return true;
		}

		/// <summary>
		/// Handles the splatter effect based on type
		/// </summary>
		/// <param name="m">The mobile that triggered the effect</param>
		private void HandleSplatterEffect( Mobile m )
		{
			// Handle air walk effect
			if ( m is PlayerMobile && Spells.Research.ResearchAirWalk.UnderEffect( m ) )
			{
				PlayAirWalkEffect( m );
				return;
			}

			// Handle poison potions
			if ( IsPoisonPotion( this.Name ) )
			{
				HandlePoisonPotionSplatter( m );
				return;
			}

			// Handle liquid types
			if ( IsLiquidType( this.Name ) )
			{
				HandleLiquidSplatter( m );
				return;
			}

			// Handle specific splatter types
			HandleSpecificSplatterType( m );
		}

		/// <summary>
		/// Handles specific splatter type effects
		/// </summary>
		/// <param name="m">The target mobile</param>
		private void HandleSpecificSplatterType( Mobile m )
		{
			string splatterName = this.Name;

			if ( splatterName == MonsterSplatterStringConstants.TYPE_HOT_MAGMA && !(m is MagmaElemental) )
			{
				ApplyFireSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 0, 100, 0, 0, 0 );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_QUICK_SILVER )
			{
				ApplySimpleSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 50, 0, 0, 0, 50, MonsterSplatterConstants.SOUND_ID_POISON );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_HOLY_WATER )
			{
				SlayerEntry SilverSlayer = SlayerGroup.GetEntryByName( SlayerName.Silver );
				SlayerEntry ExorcismSlayer = SlayerGroup.GetEntryByName( SlayerName.Exorcism );
				if ( SilverSlayer.Slays(m) || ExorcismSlayer.Slays(m) )
				{
					ApplyFireSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_HIGH, MonsterSplatterConstants.DAMAGE_MAX_HIGH, 20, 20, 20, 20, 20 );
				}
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_GLOWING_GOO && !(m is GlowBeetle) && !(m is GlowBeetleRiding) )
			{
				ApplyBloodSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 0, 0, 0, 50, 50 );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_SCORCHING_OOZE && !(m is Lavapede) )
			{
				ApplyBloodSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 0, 100, 0, 0, 0 );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_BLUE_SLIME && !(m is SlimeDevil) )
			{
				ApplySimpleSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 0, 0, 100, 0, 0, MonsterSplatterConstants.SOUND_ID_POISON );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_SWAMP_MUCK && !(m is SwampThing) )
			{
				ApplySimpleSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 50, 0, 0, 50, 0, MonsterSplatterConstants.SOUND_ID_POISON );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_POISONOUS_SLIME && !(m is AbyssCrawler) )
			{
				ApplySimpleSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 0, 0, 0, 100, 0, MonsterSplatterConstants.SOUND_ID_POISON );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_POISON_SPIT && !(m is Neptar) && !(m is NeptarWizard) )
			{
				ApplySimpleSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 0, 0, 0, 100, 0, MonsterSplatterConstants.SOUND_ID_POISON );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_POISON_SPITTLE && !(m is Lurker) )
			{
				ApplySimpleSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 0, 0, 0, 100, 0, MonsterSplatterConstants.SOUND_ID_POISON );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_FUNGAL_SLIME && !(m is Fungal) && !(m is FungalMage) && !(m is CreepingFungus) )
			{
				ApplySimpleSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 50, 0, 0, 50, 0, MonsterSplatterConstants.SOUND_ID_POISON );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_SPIDER_OOZE && !(m is ZombieSpider) )
			{
				ApplySimpleSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 50, 0, 0, 50, 0, MonsterSplatterConstants.SOUND_ID_POISON );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_ACIDIC_SLIME && !(m is ToxicElemental) )
			{
				ApplySimpleSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 50, 0, 0, 50, 0, MonsterSplatterConstants.SOUND_ID_ACID );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_ACIDIC_ICHOR && !(m is AntaurKing) && !(m is AntaurProgenitor) && !(m is AntaurSoldier) && !(m is AntaurWorker) )
			{
				ApplySimpleSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 50, 0, 0, 50, 0, MonsterSplatterConstants.SOUND_ID_ACID );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_THICK_BLOOD && !(m is BloodElemental) && !(m is BloodDemon) )
			{
				ApplyBloodSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 0, 0, 0, 100, 0 );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_INFECTED_BLOOD && !(m is Infected) )
			{
				ApplyBloodSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 0, 0, 0, 100, 0 );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_ALIEN_BLOOD && !(m is Xenomorph) && !(m is Xenomutant) )
			{
				ApplyBloodSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 20, 20, 20, 20, 20 );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_GREEN_BLOOD && !(m is ZombieGiant) )
			{
				ApplyBloodSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 20, 0, 0, 80, 0 );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_TOXIC_BLOOD && !(m is Mutant) )
			{
				ApplyBloodSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_STANDARD, MonsterSplatterConstants.DAMAGE_MAX_STANDARD, 0, 0, 0, 100, 0 );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_FREEZING_WATER && !(m is WaterElemental) && !(m is WaterWeird) && !(m is DeepWaterElemental) && !(m is Dagon) )
			{
				ApplySimpleSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_LOW, MonsterSplatterConstants.DAMAGE_MAX_LOW, 0, 0, 100, 0, 0, MonsterSplatterConstants.SOUND_ID_POISON );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_DEEP_WATER && !(m is WaterElemental) && !(m is WaterWeird) && !(m is DeepWaterElemental) && !(m is Dagon) )
			{
				ApplySimpleSplatterDamage( m, MonsterSplatterConstants.DAMAGE_MIN_HIGH, MonsterSplatterConstants.DAMAGE_MAX_HIGH, 0, 0, 100, 0, 0, MonsterSplatterConstants.SOUND_ID_POISON );
			}
		}

		/// <summary>
		/// Applies fire-based splatter damage with fire effects
		/// </summary>
		/// <param name="m">The target mobile</param>
		/// <param name="min">Minimum damage</param>
		/// <param name="max">Maximum damage</param>
		/// <param name="phys">Physical damage percent</param>
		/// <param name="fire">Fire damage percent</param>
		/// <param name="cold">Cold damage percent</param>
		/// <param name="pois">Poison damage percent</param>
		/// <param name="energy">Energy damage percent</param>
		private void ApplyFireSplatterDamage( Mobile m, int min, int max, int phys, int fire, int cold, int pois, int energy )
				{
					owner.DoHarmful( m );
			Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), MonsterSplatterConstants.EFFECT_ID_FIRE, MonsterSplatterConstants.PARTICLE_COUNT_FIRE, MonsterSplatterConstants.PARTICLE_SPEED_FIRE, MonsterSplatterConstants.HUE_FIRE_EFFECT );
			Effects.PlaySound( m.Location, m.Map, MonsterSplatterConstants.SOUND_ID_FIRE );
			AOS.Damage( true, m, owner, Hurt( owner, min, max ), phys, fire, cold, pois, energy );
		}

		/// <summary>
		/// Applies blood-based splatter damage with blood effects
		/// </summary>
		/// <param name="m">The target mobile</param>
		/// <param name="min">Minimum damage</param>
		/// <param name="max">Maximum damage</param>
		/// <param name="phys">Physical damage percent</param>
		/// <param name="fire">Fire damage percent</param>
		/// <param name="cold">Cold damage percent</param>
		/// <param name="pois">Poison damage percent</param>
		/// <param name="energy">Energy damage percent</param>
		private void ApplyBloodSplatterDamage( Mobile m, int min, int max, int phys, int fire, int cold, int pois, int energy )
				{
					owner.DoHarmful( m );
			Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), MonsterSplatterConstants.EFFECT_ID_BLOOD_SLIME, MonsterSplatterConstants.PARTICLE_COUNT_BLOOD, MonsterSplatterConstants.PARTICLE_SPEED_BLOOD, MonsterSplatterConstants.HUE_BLOOD_EFFECT, MonsterSplatterConstants.PARTICLE_DURATION_BLOOD, MonsterSplatterConstants.PARTICLE_EFFECT_BLOOD, 0 );
			int soundId = GetBodyTypeSound( m );
			Effects.PlaySound( m.Location, m.Map, soundId );
			AOS.Damage( true, m, owner, Hurt( owner, min, max ), phys, fire, cold, pois, energy );
		}

		/// <summary>
		/// Applies simple splatter damage with sound effect
		/// </summary>
		/// <param name="m">The target mobile</param>
		/// <param name="min">Minimum damage</param>
		/// <param name="max">Maximum damage</param>
		/// <param name="phys">Physical damage percent</param>
		/// <param name="fire">Fire damage percent</param>
		/// <param name="cold">Cold damage percent</param>
		/// <param name="pois">Poison damage percent</param>
		/// <param name="energy">Energy damage percent</param>
		/// <param name="soundId">Sound effect ID</param>
		private void ApplySimpleSplatterDamage( Mobile m, int min, int max, int phys, int fire, int cold, int pois, int energy, int soundId )
				{
					owner.DoHarmful( m );
			Effects.PlaySound( m.Location, m.Map, soundId );
			AOS.Damage( true, m, owner, Hurt( owner, min, max ), phys, fire, cold, pois, energy );
		}

		/// <summary>
		/// Gets the appropriate sound ID based on body type
		/// </summary>
		/// <param name="m">The mobile</param>
		/// <returns>Sound ID</returns>
		private static int GetBodyTypeSound( Mobile m )
		{
			if ( m.Body == MonsterSplatterConstants.BODY_TYPE_MALE && m is PlayerMobile )
				return MonsterSplatterConstants.SOUND_ID_MALE_PLAYER;
			if ( m.Body == MonsterSplatterConstants.BODY_TYPE_FEMALE && m is PlayerMobile )
				return MonsterSplatterConstants.SOUND_ID_FEMALE_PLAYER;
			return MonsterSplatterConstants.SOUND_ID_BLOOD_DEFAULT;
		}

		/// <summary>
		/// Plays air walk effect when player has air walk active
		/// </summary>
		/// <param name="m">The mobile with air walk</param>
		private static void PlayAirWalkEffect( Mobile m )
		{
			Point3D air = new Point3D( m.X + MonsterSplatterConstants.AIR_WALK_OFFSET_X, m.Y + MonsterSplatterConstants.AIR_WALK_OFFSET_Y, m.Z + MonsterSplatterConstants.AIR_WALK_OFFSET_Z );
			Effects.SendLocationParticles( EffectItem.Create( air, m.Map, EffectItem.DefaultDuration ), MonsterSplatterConstants.EFFECT_ID_AIR_WALK, MonsterSplatterConstants.PARTICLE_COUNT_AIR_WALK, MonsterSplatterConstants.PARTICLE_SPEED_AIR_WALK, Server.Items.CharacterDatabase.GetMySpellHue( m, 0 ), 0, MonsterSplatterConstants.PARTICLE_EFFECT_AIR_WALK, 0 );
			m.PlaySound( MonsterSplatterConstants.SOUND_ID_AIR_WALK );
		}

		/// <summary>
		/// Checks if the name is a poison potion type
		/// </summary>
		/// <param name="name">The splatter name</param>
		/// <returns>True if poison potion type</returns>
		private static bool IsPoisonPotion( string name )
		{
			return name == MonsterSplatterStringConstants.TYPE_LESSER_POISON ||
				   name == MonsterSplatterStringConstants.TYPE_POISON_POTION ||
				   name == MonsterSplatterStringConstants.TYPE_GREATER_POISON ||
				   name == MonsterSplatterStringConstants.TYPE_DEADLY_POISON ||
				   name == MonsterSplatterStringConstants.TYPE_LETHAL_POISON;
		}

		/// <summary>
		/// Handles poison potion splatter effects
		/// </summary>
		/// <param name="m">The target mobile</param>
		private void HandlePoisonPotionSplatter( Mobile m )
		{
			int pSkill = (int)(owner.Skills[SkillName.Poisoning].Value / MonsterSplatterConstants.SKILL_DIVISOR_POISONING);
			int tSkill = (int)(owner.Skills[SkillName.TasteID].Value / MonsterSplatterConstants.SKILL_DIVISOR_TASTE_ALCHEMY);
			int aSkill = (int)(owner.Skills[SkillName.Alchemy].Value / MonsterSplatterConstants.SKILL_DIVISOR_TASTE_ALCHEMY);

					int pMin = pSkill + tSkill + aSkill;
					int pMax = pMin * 2;
					Poison pois = Poison.Lesser;

			if ( this.Name == MonsterSplatterStringConstants.TYPE_POISON_POTION )
			{
				pMin = pMin + 2;
				pMax = pMax + 2;
				pois = Poison.Regular;
			}
			else if ( this.Name == MonsterSplatterStringConstants.TYPE_GREATER_POISON )
			{
				pMin = pMin + 3;
				pMax = pMax + 3;
				pois = Poison.Greater;
			}
			else if ( this.Name == MonsterSplatterStringConstants.TYPE_DEADLY_POISON )
			{
				pMin = pMin + 4;
				pMax = pMax + 4;
				pois = Poison.Deadly;
			}
			else if ( this.Name == MonsterSplatterStringConstants.TYPE_LETHAL_POISON )
			{
				pMin = pMin + 5;
				pMax = pMax + 5;
				pois = Poison.Lethal;
			}

			if ( pMin >= Utility.RandomMinMax( MonsterSplatterConstants.POISON_CHANCE_MIN, MonsterSplatterConstants.POISON_CHANCE_MAX ) )
					{
						m.ApplyPoison( owner, pois );
					}

					owner.DoHarmful( m );
			Effects.PlaySound( m.Location, m.Map, MonsterSplatterConstants.SOUND_ID_POISON );
			AOS.Damage( true, m, owner, Hurt( owner, pMin, pMax ), 0, 0, 0, 100, 0 );
		}

		/// <summary>
		/// Checks if the name is a liquid type
		/// </summary>
		/// <param name="name">The splatter name</param>
		/// <returns>True if liquid type</returns>
		private static bool IsLiquidType( string name )
		{
			return name == MonsterSplatterStringConstants.TYPE_LIQUID_FIRE ||
				   name == MonsterSplatterStringConstants.TYPE_LIQUID_GOO ||
				   name == MonsterSplatterStringConstants.TYPE_LIQUID_ICE ||
				   name == MonsterSplatterStringConstants.TYPE_LIQUID_ROT ||
				   name == MonsterSplatterStringConstants.TYPE_LIQUID_PAIN;
		}

		/// <summary>
		/// Handles liquid splatter effects
		/// </summary>
		/// <param name="m">The target mobile</param>
		private void HandleLiquidSplatter( Mobile m )
				{
					int liqMin = Server.Items.BaseLiquid.GetLiquidBonus( owner );
					int liqMax = liqMin * 2;

			string splatterName = this.Name;

			if ( splatterName == MonsterSplatterStringConstants.TYPE_LIQUID_FIRE )
			{
					owner.DoHarmful( m );
				Effects.SendLocationEffect( m.Location, m.Map, MonsterSplatterConstants.EFFECT_ID_FIRE, MonsterSplatterConstants.LIQUID_EFFECT_DURATION, MonsterSplatterConstants.LIQUID_EFFECT_SPEED );
				m.PlaySound( MonsterSplatterConstants.SOUND_ID_LIQUID_FIRE );
				AOS.Damage( true, m, owner, Hurt( owner, liqMin, liqMax ), 20, 80, 0, 0, 0 );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_LIQUID_GOO )
			{
					owner.DoHarmful( m );
				Effects.SendLocationEffect( m.Location, m.Map, Utility.RandomList( MonsterSplatterConstants.EFFECT_ID_LIQUID_GOO_1, MonsterSplatterConstants.EFFECT_ID_LIQUID_GOO_2 ), MonsterSplatterConstants.LIQUID_EFFECT_DURATION, MonsterSplatterConstants.LIQUID_EFFECT_SPEED );
				m.PlaySound( MonsterSplatterConstants.SOUND_ID_LIQUID_GOO );
				AOS.Damage( true, m, owner, Hurt( owner, liqMin, liqMax ), 20, 0, 0, 0, 80 );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_LIQUID_ICE )
			{
					owner.DoHarmful( m );
				Effects.SendLocationEffect( m.Location, m.Map, MonsterSplatterConstants.EFFECT_ID_ICE, MonsterSplatterConstants.LIQUID_EFFECT_DURATION, MonsterSplatterConstants.LIQUID_EFFECT_SPEED, MonsterSplatterConstants.EFFECT_ID_ICE_HUE, 0 );
				m.PlaySound( MonsterSplatterConstants.SOUND_ID_LIQUID_ICE );
				AOS.Damage( true, m, owner, Hurt( owner, liqMin, liqMax ), 20, 0, 80, 0, 0 );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_LIQUID_ROT )
			{
					owner.DoHarmful( m );
				Effects.SendLocationEffect( m.Location, m.Map, MonsterSplatterConstants.EFFECT_ID_ROT, MonsterSplatterConstants.ROT_EFFECT_DURATION );
				Effects.PlaySound( m.Location, m.Map, MonsterSplatterConstants.SOUND_ID_LIQUID_ROT );
				AOS.Damage( true, m, owner, Hurt( owner, liqMin, liqMax ), 20, 0, 0, 80, 0 );
			}
			else if ( splatterName == MonsterSplatterStringConstants.TYPE_LIQUID_PAIN )
			{
					owner.DoHarmful( m );
				m.FixedParticles( MonsterSplatterConstants.EFFECT_ID_PAIN, MonsterSplatterConstants.PARTICLE_COUNT_PAIN, MonsterSplatterConstants.PARTICLE_SPEED_PAIN, MonsterSplatterConstants.PARTICLE_EFFECT_PAIN_1, MonsterSplatterConstants.PARTICLE_DURATION_PAIN_1, MonsterSplatterConstants.PARTICLE_LAYER_PAIN_1, EffectLayer.Head );
				m.FixedParticles( MonsterSplatterConstants.EFFECT_ID_PAIN, MonsterSplatterConstants.PARTICLE_COUNT_PAIN, MonsterSplatterConstants.PARTICLE_SPEED_PAIN, MonsterSplatterConstants.PARTICLE_EFFECT_PAIN_2, MonsterSplatterConstants.PARTICLE_DURATION_PAIN_2, MonsterSplatterConstants.PARTICLE_LAYER_PAIN_2, EffectLayer.Head );
				m.PlaySound( MonsterSplatterConstants.SOUND_ID_LIQUID_PAIN );
				AOS.Damage( true, m, owner, Hurt( owner, liqMin, liqMax ), 80, 5, 5, 5, 5 );
			}
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Creates splatter items at multiple locations around a target point
		/// </summary>
		/// <param name="iX">Center X coordinate</param>
		/// <param name="iY">Center Y coordinate</param>
		/// <param name="iZ">Z coordinate</param>
		/// <param name="iMap">Map</param>
		/// <param name="iLoc">Location for sound effect</param>
		/// <param name="source">The mobile that created the splatter</param>
		/// <param name="description">Splatter name/description</param>
		/// <param name="color">Hue color</param>
		/// <param name="glow">Glow parameter (0 = no glow, >0 = create glow)</param>
		public static void AddSplatter( int iX, int iY, int iZ, Map iMap, Point3D iLoc, Mobile source, string description, int color, int glow )
		{
			Effects.PlaySound( iLoc, iMap, MonsterSplatterConstants.SOUND_ID_SPLATTER_CREATE );

			double weight = glow > 0 ? MonsterSplatterConstants.WEIGHT_GLOWING : MonsterSplatterConstants.WEIGHT_NORMAL;

			// Create splatters at all offset positions
			Point3D[] offsets = GetSplatterOffsets();
			foreach ( Point3D offset in offsets )
			{
				CreateSplatterAtLocation( source, description, color, weight, iX + offset.X, iY + offset.Y, iZ, iMap );
			}

			// Create glows if needed
			if ( glow > 0 )
			{
				foreach ( Point3D offset in offsets )
				{
					CreateGlowAtLocation( description, iX + offset.X, iY + offset.Y, iZ, iMap );
				}
			}
		}

		/// <summary>
		/// Gets the array of splatter position offsets
		/// </summary>
		/// <returns>Array of Point3D offsets</returns>
		private static Point3D[] GetSplatterOffsets()
		{
			return new Point3D[]
			{
				new Point3D( -2, -1, 0 ),
				new Point3D( -1, -1, 0 ),
				new Point3D( -1, 0, 0 ),
				new Point3D( -1, 1, 0 ),
				new Point3D( 0, 1, 0 ),
				new Point3D( 1, 1, 0 ),
				new Point3D( 1, 0, 0 ),
				new Point3D( 1, -1, 0 ),
				new Point3D( 0, -1, 0 ),
				new Point3D( 1, -2, 0 ),
				new Point3D( 2, -2, 0 ),
				new Point3D( -2, 1, 0 ),
				new Point3D( -2, 2, 0 ),
				new Point3D( 1, 2, 0 )
			};
		}

		/// <summary>
		/// Creates a splatter at the specified location
		/// </summary>
		/// <param name="source">The mobile that created the splatter</param>
		/// <param name="description">Splatter name/description</param>
		/// <param name="color">Hue color</param>
		/// <param name="weight">Splatter weight</param>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		/// <param name="z">Z coordinate</param>
		/// <param name="map">Map</param>
		private static void CreateSplatterAtLocation( Mobile source, string description, int color, double weight, int x, int y, int z, Map map )
		{
			MonsterSplatter splatter = new MonsterSplatter( source );
			splatter.Name = description;
			splatter.Hue = color;
			splatter.Weight = weight;
			splatter.MoveToWorld( new Point3D( x, y, z ), map );
		}

		/// <summary>
		/// Creates a glow at the specified location
		/// </summary>
		/// <param name="description">Glow name/description</param>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		/// <param name="z">Z coordinate</param>
		/// <param name="map">Map</param>
		private static void CreateGlowAtLocation( string description, int x, int y, int z, Map map )
		{
			StrangeGlow glow = new StrangeGlow();
			glow.Name = description;
			glow.MoveToWorld( new Point3D( x, y, z ), map );
		}

		/// <summary>
		/// Checks if there are too many splatters in the area
		/// </summary>
		/// <param name="from">The mobile to check around</param>
		/// <returns>True if too many splatters exist</returns>
		public static bool TooMuchSplatter( Mobile from )
		{
			int splatter = 0;

			foreach ( Item i in from.GetItemsInRange( MonsterSplatterConstants.SEARCH_RANGE ) )
			{
				if ( i is MonsterSplatter )
				{
					MonsterSplatter splat = (MonsterSplatter)i;
					if ( splat.owner != from )
						splatter++;
				}
			}

			return splatter > MonsterSplatterConstants.MAX_SPLATTER_THRESHOLD;
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the MonsterSplatter
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( MonsterSplatterConstants.SERIALIZATION_VERSION );
			writer.Write( (Mobile)owner );
		}

		/// <summary>
		/// Deserializes the MonsterSplatter
		/// </summary>
		/// <param name="reader">Generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			owner = reader.ReadMobile();
			this.Delete(); // none when the world starts 
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Timer that removes the splatter item after a duration
		/// </summary>
		public class ItemRemovalTimer : Timer 
		{ 
			private Item i_item; 

			/// <summary>
			/// Initializes a new instance of ItemRemovalTimer
			/// </summary>
			/// <param name="item">The item to remove</param>
			public ItemRemovalTimer( Item item ) : base( TimeSpan.FromSeconds( MonsterSplatterConstants.TIMER_DURATION_SECONDS ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
				i_item = item; 
			} 

			/// <summary>
			/// Called when the timer ticks
			/// </summary>
			protected override void OnTick() 
			{ 
				if ( ( i_item != null ) && ( !i_item.Deleted ) )
				{
					i_item.Delete();
				}
			} 
		} 

		#endregion
	}
}

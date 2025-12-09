using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	/// <summary>
	/// FrenziedOstard - A mount creature with two variants (regular and greater)
	/// Greater variant has 20% spawn chance and enhanced stats
	/// </summary>
	[CorpseName( FrenziedOstardStringConstants.CORPSE_NAME )]
	public class FrenziedOstard : BaseMount
	{
		#region Constructors

		/// <summary>
		/// Default constructor - creates a FrenziedOstard with default name
		/// </summary>
		[Constructable]
		public FrenziedOstard() : this( FrenziedOstardStringConstants.NAME )
		{
		}

		/// <summary>
		/// Constructor with custom name
		/// </summary>
		/// <param name="name">Name for the FrenziedOstard</param>
		[Constructable]
		public FrenziedOstard( string name ) : base( name, FrenziedOstardConstants.BODY_ID, FrenziedOstardConstants.ITEM_ID, 
			FrenziedOstardConstants.AI_TYPE, FrenziedOstardConstants.FIGHT_MODE, 
			FrenziedOstardConstants.RANGE_PERCEPTION, FrenziedOstardConstants.RANGE_FIGHT, 
			FrenziedOstardConstants.REGULAR_ACTIVE_SPEED, FrenziedOstardConstants.REGULAR_PASSIVE_SPEED )
		{
			bool isGreater = Utility.RandomDouble() > FrenziedOstardConstants.GREATER_VARIANT_CHANCE;
			
			SetCommonProperties();
			
			if (isGreater)
			{
				ConfigureGreaterVariant();
			}
			else
			{
				ConfigureRegularVariant();
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Amount of meat yielded when butchered
		/// </summary>
		public override int Meat{ get{ return 3; } }

		/// <summary>
		/// Amount of hides yielded when butchered
		/// </summary>
		public override int Hides{ get{ return 12; } }

		/// <summary>
		/// Type of hide yielded
		/// </summary>
		public override HideType HideType{ get{ return HideType.Horned; } }

		/// <summary>
		/// Favorite food types
		/// </summary>
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish | FoodType.Eggs | FoodType.FruitsAndVegies; } }

		/// <summary>
		/// Pack instinct type
		/// </summary>
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Ostard; } }

		#endregion

		#region Helper Methods

		/// <summary>
		/// Sets properties common to both variants
		/// </summary>
		private void SetCommonProperties()
		{
			Name = FrenziedOstardStringConstants.NAME;
			BaseSoundID = FrenziedOstardConstants.BASE_SOUND_ID;
			SetMana( FrenziedOstardConstants.MANA );
			SetDamageType( ResistanceType.Physical, FrenziedOstardConstants.DAMAGE_TYPE_PHYSICAL_PERCENT );
			SetResistances();
			SetSkills();
			Fame = FrenziedOstardConstants.FAME;
			Karma = FrenziedOstardConstants.KARMA;
			Tamable = true;
			ControlSlots = FrenziedOstardConstants.CONTROL_SLOTS;
		}

		/// <summary>
		/// Configures the greater variant with enhanced stats
		/// </summary>
		private void ConfigureGreaterVariant()
		{
			Hue = FrenziedOstardConstants.HUE_GREATER;
			ActiveSpeed = FrenziedOstardConstants.GREATER_ACTIVE_SPEED;
			PassiveSpeed = FrenziedOstardConstants.GREATER_PASSIVE_SPEED;
			
			SetStr( FrenziedOstardConstants.GREATER_STR_MIN, FrenziedOstardConstants.GREATER_STR_MAX );
			SetDex( FrenziedOstardConstants.GREATER_DEX_MIN, FrenziedOstardConstants.GREATER_DEX_MAX );
			SetInt( FrenziedOstardConstants.GREATER_INT_MIN, FrenziedOstardConstants.GREATER_INT_MAX );
			SetHits( FrenziedOstardConstants.GREATER_HITS_MIN, FrenziedOstardConstants.GREATER_HITS_MAX );
			SetDamage( FrenziedOstardConstants.GREATER_DAMAGE_MIN, FrenziedOstardConstants.GREATER_DAMAGE_MAX );
			SetResistance( ResistanceType.Physical, FrenziedOstardConstants.GREATER_PHYSICAL_RESIST_MIN, FrenziedOstardConstants.GREATER_PHYSICAL_RESIST_MAX );
			
			MinTameSkill = FrenziedOstardConstants.GREATER_MIN_TAME_SKILL;
		}

		/// <summary>
		/// Configures the regular variant with standard stats
		/// </summary>
		private void ConfigureRegularVariant()
		{
			Hue = FrenziedOstardConstants.HUE_REGULAR;
			
			SetStr( FrenziedOstardConstants.REGULAR_STR_MIN, FrenziedOstardConstants.REGULAR_STR_MAX );
			SetDex( FrenziedOstardConstants.REGULAR_DEX_MIN, FrenziedOstardConstants.REGULAR_DEX_MAX );
			SetInt( FrenziedOstardConstants.REGULAR_INT_MIN, FrenziedOstardConstants.REGULAR_INT_MAX );
			SetHits( FrenziedOstardConstants.REGULAR_HITS_MIN, FrenziedOstardConstants.REGULAR_HITS_MAX );
			SetDamage( FrenziedOstardConstants.REGULAR_DAMAGE_MIN, FrenziedOstardConstants.REGULAR_DAMAGE_MAX );
			SetResistance( ResistanceType.Physical, FrenziedOstardConstants.REGULAR_PHYSICAL_RESIST_MIN, FrenziedOstardConstants.REGULAR_PHYSICAL_RESIST_MAX );
			
			MinTameSkill = FrenziedOstardConstants.REGULAR_MIN_TAME_SKILL;
		}

		/// <summary>
		/// Sets resistance values common to both variants
		/// </summary>
		private void SetResistances()
		{
			SetResistance( ResistanceType.Fire, FrenziedOstardConstants.FIRE_RESIST_MIN, FrenziedOstardConstants.FIRE_RESIST_MAX );
			SetResistance( ResistanceType.Poison, FrenziedOstardConstants.POISON_RESIST_MIN, FrenziedOstardConstants.POISON_RESIST_MAX );
			SetResistance( ResistanceType.Energy, FrenziedOstardConstants.ENERGY_RESIST_MIN, FrenziedOstardConstants.ENERGY_RESIST_MAX );
		}

		/// <summary>
		/// Sets skill values common to both variants
		/// </summary>
		private void SetSkills()
		{
			SetSkill( SkillName.MagicResist, FrenziedOstardConstants.MAGIC_RESIST_SKILL_MIN, FrenziedOstardConstants.MAGIC_RESIST_SKILL_MAX );
			SetSkill( SkillName.Tactics, FrenziedOstardConstants.TACTICS_SKILL_MIN, FrenziedOstardConstants.TACTICS_SKILL_MAX );
			SetSkill( SkillName.Wrestling, FrenziedOstardConstants.WRESTLING_SKILL_MIN, FrenziedOstardConstants.WRESTLING_SKILL_MAX );
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public FrenziedOstard( Serial serial ) : base( serial )
		{
		}

		/// <summary>
		/// Serializes the FrenziedOstard
		/// </summary>
		/// <param name="writer">GenericWriter to write to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the FrenziedOstard
		/// </summary>
		/// <param name="reader">GenericReader to read from</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		#endregion
	}
}

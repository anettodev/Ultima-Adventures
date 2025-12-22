// coded by ßåяя¥ 2.0
using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Base class for all strength potions.
	/// Provides temporary strength bonus with visual effects.
	/// Normal: 3-5 points, 60 seconds | Greater: 7-8 points, 90 seconds
	/// </summary>
	public abstract class BaseStrengthPotion : BasePotion
	{
		#region Abstract Properties

		/// <summary>Gets the minimum strength bonus</summary>
		public abstract int MinStrength{ get; }

		/// <summary>Gets the maximum strength bonus</summary>
		public abstract int MaxStrength{ get; }

		/// <summary>Gets the duration of the strength buff</summary>
		public abstract TimeSpan Duration{ get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of BaseStrengthPotion
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		public BaseStrengthPotion( PotionEffect effect ) : base( 0xF09, effect )
		{
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public BaseStrengthPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the strength potion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the strength potion
		/// </summary>
		/// <param name="reader">Generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds custom properties to the object property list
		/// </summary>
		/// <param name="list">The object property list</param>
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			// Display potion type in custom cyan color (#8be4fc) with brackets
			string potionName = PotionMetadata.GetKegName( this.PotionEffect );
			if ( potionName != null )
			{
				list.Add( 1070722, string.Format( "<BASEFONT COLOR=#8be4fc>[{0}]", potionName ) ); // Custom cyan color #8be4fc
			}
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Applies strength buff to the specified mobile
		/// </summary>
		/// <param name="from">The mobile to buff</param>
		/// <returns>True if buff was applied, false if already under similar effect</returns>
		public bool DoStrength( Mobile from )
		{
			// Calculate random strength bonus within range
			int strengthBonus = Utility.RandomMinMax( MinStrength, MaxStrength );
			int scale = Scale(from, strengthBonus);
			
			if (Spells.SpellHelper.AddStatOffset(from, StatType.Str, scale, Duration))
			{
				from.FixedEffect( 0x375A, 10, 15 );
				from.PlaySound( 0x1E7 );
				
				BuffInfo.AddBuff(from, new BuffInfo(BuffIcon.Strength, 1075845, Duration, from, scale.ToString()));
				
				// Display strength bonus and duration (PT-BR)
				int durationSeconds = (int)Duration.TotalSeconds;
				from.SendMessage( 0x59, string.Format( "Você ganhou +{0} de força por {1} segundos.", scale, durationSeconds ) ); // 0x59 = cyan
				
				return true;
			}

			from.SendLocalizedMessage( 502173 ); // You are already under a similar effect.
			return false;
		}

		/// <summary>
		/// Handles drinking the strength potion
		/// </summary>
		/// <param name="from">The mobile drinking the potion</param>
		public override void Drink( Mobile from )
		{
			if ( DoStrength( from ) )
			{
				BasePotion.PlayDrinkEffect( from );
				this.Consume();
			}
		}

		#endregion
	}
}

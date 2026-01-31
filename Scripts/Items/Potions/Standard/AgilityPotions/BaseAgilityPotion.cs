// coded by ßåяя¥ 2.0
using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Base class for all agility potions.
	/// Provides temporary dexterity bonus with visual effects.
	/// Normal: 3-5 points, 60 seconds | Greater: 7-8 points, 90 seconds
	/// </summary>
	public abstract class BaseAgilityPotion : BasePotion
	{
		#region Abstract Properties

		/// <summary>Gets the minimum dexterity bonus</summary>
		public abstract int MinDexterity{ get; }

		/// <summary>Gets the maximum dexterity bonus</summary>
		public abstract int MaxDexterity{ get; }

		/// <summary>Gets the duration of the agility buff</summary>
		public abstract TimeSpan Duration{ get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of BaseAgilityPotion
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		public BaseAgilityPotion( PotionEffect effect ) : base( 0xF08, effect )
		{
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public BaseAgilityPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the agility potion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the agility potion
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
		/// Applies agility buff to the specified mobile
		/// </summary>
		/// <param name="from">The mobile to buff</param>
		/// <returns>True if buff was applied, false if already under similar effect</returns>
		public bool DoAgility( Mobile from )
		{
			// Calculate random dexterity bonus within range
			int dexterityBonus = Utility.RandomMinMax( MinDexterity, MaxDexterity );
			int scale = Scale(from, dexterityBonus);
			
			if (Spells.SpellHelper.AddStatOffset(from, StatType.Dex, scale, Duration))
			{
				from.FixedEffect( 0x375A, 10, 15 );
				from.PlaySound( 0x1E7 );
				
				BuffInfo.AddBuff(from, new BuffInfo(BuffIcon.Agility, 1075841, Duration, from, scale.ToString()));
				
				// Display dexterity bonus and duration (PT-BR)
				int durationSeconds = (int)Duration.TotalSeconds;
				from.SendMessage( 0x59, string.Format( "Você ganhou +{0} de destreza por {1} segundos.", scale, durationSeconds ) ); // 0x59 = cyan
				
				return true;
			}

			from.SendLocalizedMessage( 502173 ); // You are already under a similar effect.
			return false;
		}

		/// <summary>
		/// Handles drinking the agility potion
		/// </summary>
		/// <param name="from">The mobile drinking the potion</param>
		public override void Drink( Mobile from )
		{
			if ( DoAgility( from ) )
			{
				BasePotion.PlayDrinkEffect( from );
				this.Consume();
			}
		}

		#endregion
	}
}

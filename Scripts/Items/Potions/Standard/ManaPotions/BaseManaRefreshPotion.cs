using System;
using Server;
using Server.Network;

namespace Server.Items
{
	/// <summary>
	/// Base class for all mana refresh potions.
	/// Provides mana restoration with a delay between uses.
	/// </summary>
	public abstract class BaseManaRefreshPotion : BasePotion
	{
		#region Abstract Properties

		/// <summary>Gets the minimum mana restoration amount</summary>
		public abstract int MinMana { get; }

		/// <summary>Gets the maximum mana restoration amount</summary>
		public abstract int MaxMana { get; }

		/// <summary>Gets the delay in seconds before another mana potion can be used</summary>
		public abstract double Delay { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of BaseManaRefreshPotion
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		public BaseManaRefreshPotion( PotionEffect effect ) : base( 0x180F, effect )
		{
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public BaseManaRefreshPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the mana refresh potion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the mana refresh potion
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
		/// Applies mana restoration to the specified mobile
		/// </summary>
		/// <param name="from">The mobile to restore mana to</param>
		public void DoMana( Mobile from )
		{
			int min = Scale( from, Server.Misc.MyServerSettings.PlayerLevelMod( MinMana, from ) );
			int max = Scale( from, Server.Misc.MyServerSettings.PlayerLevelMod( MaxMana, from ) );

			int manaAmount = Utility.RandomMinMax( min, max );
			
			// Calculate actual mana restored (capped at max)
			int oldMana = from.Mana;
			from.Mana = from.Mana + manaAmount;
			int actualRestored = from.Mana - oldMana;
			
			// Display how much mana was recovered (PT-BR)
			from.SendMessage( 0x59, string.Format( "Você recuperou +{0} pontos de mana.", actualRestored ) ); // 0x59 = cyan color
		}

		/// <summary>
		/// Handles drinking the mana refresh potion
		/// </summary>
		/// <param name="from">The mobile drinking the potion</param>
		public override void Drink( Mobile from )
		{
			// Check if mana restoration is needed
			if ( from.Mana < from.ManaMax )
			{
				// Check if mana delay has expired
				if ( from.BeginAction( typeof( BaseManaRefreshPotion ) ) )
				{
					DoMana( from );
					BasePotion.PlayDrinkEffect( from );
					this.Consume();

					// Set delay timer before next mana potion can be used
					Timer.DelayCall( TimeSpan.FromSeconds( Delay ), new TimerStateCallback( ReleaseManaLock ), from );
				}
				else
				{
					from.LocalOverheadMessage( MessageType.Regular, 0x22, true, "Você deve esperar aproximadamente 5 segundos antes de usar outra poção de mana." );
				}
			}
			else
			{
				from.SendMessage( "Você decide não beber esta poção, pois já está com mana máxima." );
			}
		}

		/// <summary>
		/// Releases the mana lock allowing the mobile to use another mana potion
		/// </summary>
		/// <param name="state">The mobile state object</param>
		private static void ReleaseManaLock( object state )
		{
			Mobile mobile = state as Mobile;
			if ( mobile != null )
			{
				mobile.EndAction( typeof( BaseManaRefreshPotion ) );
			}
		}

		#endregion
	}
}

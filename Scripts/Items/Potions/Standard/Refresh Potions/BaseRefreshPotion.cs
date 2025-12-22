using System;
using Server;

namespace Server.Items
{
	/// <summary>
	/// Base class for all refresh potions.
	/// Provides stamina restoration.
	/// </summary>
	public abstract class BaseRefreshPotion : BasePotion
	{
	#region Abstract Properties

	/// <summary>Gets the refresh percentage (0.0 to 1.0)</summary>
	public abstract double Refresh{ get; }

	#endregion

	#region Constants

	/// <summary>Visual effect ID (same as Cunning spell)</summary>
	private const int EFFECT_ID = 0x375A;

	/// <summary>Visual effect speed</summary>
	private const int EFFECT_SPEED = 10;

	/// <summary>Visual effect render mode</summary>
	private const int EFFECT_RENDER = 15;

	/// <summary>Visual effect duration</summary>
	private const int EFFECT_DURATION = 5011;

	/// <summary>Sound effect ID (same as Cunning spell)</summary>
	private const int SOUND_ID = 0x1EB;

	/// <summary>Default hue for effects</summary>
	private const int DEFAULT_HUE = 0;

	/// <summary>Message color for feedback (cyan)</summary>
	private const int MESSAGE_COLOR = 0x59;

	#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of BaseRefreshPotion
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		public BaseRefreshPotion( PotionEffect effect ) : base( 0xF0B, effect )
		{
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public BaseRefreshPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the refresh potion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the refresh potion
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
	/// Handles drinking the refresh potion
	/// </summary>
	/// <param name="from">The mobile drinking the potion</param>
	public override void Drink( Mobile from )
	{
		if ( from.Stam < from.StamMax )
		{
			// Calculate stamina to restore
			int staminaAmount = Server.Misc.MyServerSettings.PlayerLevelMod( Scale( from, (int)(Refresh * from.StamMax) ), from );

			// Calculate actual stamina restored (capped at max)
			int oldStam = from.Stam;
			from.Stam += staminaAmount;
			int actualRestored = from.Stam - oldStam;

			// Play drinking effect
			BasePotion.PlayDrinkEffect( from );

			// Play visual and sound effects (same as Cunning spell)
			PlayRefreshEffects( from );

			// Display how much stamina was recovered (PT-BR)
			from.SendMessage( MESSAGE_COLOR, string.Format( "Você recuperou +{0} pontos de vigor.", actualRestored ) ); // Cyan color

			this.Consume();
		}
		else
		{
			from.SendMessage( MESSAGE_COLOR, "Você decide não beber esta poção, pois já está com vigor máximo." );
		}
	}

	/// <summary>
	/// Plays visual and sound effects for stamina refresh (same as Cunning spell)
	/// </summary>
	/// <param name="target">The mobile being refreshed</param>
	private void PlayRefreshEffects( Mobile target )
	{
		int hue = Server.Items.CharacterDatabase.GetMySpellHue( target, DEFAULT_HUE );
		target.FixedParticles( EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_DURATION, hue, 0, EffectLayer.Head );
		target.PlaySound( SOUND_ID );
	}

	#endregion
	}
}
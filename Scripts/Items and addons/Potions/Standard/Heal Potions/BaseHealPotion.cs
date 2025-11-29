using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{
	/// <summary>
	/// Base class for all healing potions (Lesser, Regular, Greater).
	/// Provides health restoration with a delay between uses.
	/// </summary>
	public abstract class BaseHealPotion : BasePotion
	{
	#region Abstract Properties

	/// <summary>Gets the minimum healing amount</summary>
	public abstract int MinHeal { get; }

	/// <summary>Gets the maximum healing amount</summary>
	public abstract int MaxHeal { get; }

	/// <summary>Gets the delay in seconds before another heal potion can be used</summary>
	public abstract double Delay { get; }

	#endregion

	#region Constants

	/// <summary>Visual effect ID (same as Heal spell)</summary>
	private const int EFFECT_ID = 0x376A;

	/// <summary>Visual effect speed</summary>
	private const int EFFECT_SPEED = 9;

	/// <summary>Visual effect render mode</summary>
	private const int EFFECT_RENDER = 32;

	/// <summary>Visual effect duration</summary>
	private const int EFFECT_DURATION = 5005;

	/// <summary>Sound effect ID (same as Heal spell)</summary>
	private const int SOUND_ID = 0x1F2;

	/// <summary>Message color for heal feedback (cyan)</summary>
	private const int MESSAGE_COLOR = 0x59;

	#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of BaseHealPotion
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		public BaseHealPotion( PotionEffect effect ) : base( 0xF0C, effect )
		{
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public BaseHealPotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the heal potion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the heal potion
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
	/// Applies healing to the specified mobile
	/// </summary>
	/// <param name="from">The mobile to heal</param>
	public void DoHeal( Mobile from )
	{
		int min = Scale( from, Server.Misc.MyServerSettings.PlayerLevelMod( MinHeal, from ) );
		int max = Scale( from, Server.Misc.MyServerSettings.PlayerLevelMod( MaxHeal, from ) );

		int healAmount = Utility.RandomMinMax( min, max );

		// Calculate actual HP restored (capped at max)
		int oldHits = from.Hits;
		from.Heal( healAmount );
		int actualHealed = from.Hits - oldHits;

		// Play visual and sound effects (same as Heal spell)
		PlayHealEffects( from );

		// Display how much HP was recovered (PT-BR)
		from.SendMessage( MESSAGE_COLOR, string.Format( "Você recuperou +{0} pontos de vida.", actualHealed ) ); // Cyan color
	}

	/// <summary>
	/// Plays visual and sound effects for healing (same as Heal spell)
	/// </summary>
	/// <param name="target">The mobile being healed</param>
	private void PlayHealEffects( Mobile target )
	{
		int hue = Server.Items.CharacterDatabase.GetMySpellHue( target, 0 );
		target.FixedParticles( EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, EFFECT_DURATION, hue, 0, EffectLayer.Waist );
		target.PlaySound( SOUND_ID );
	}

		/// <summary>
		/// Handles drinking the healing potion
		/// </summary>
		/// <param name="from">The mobile drinking the potion</param>
		public override void Drink( Mobile from )
		{
			// Check for ONE RING prevention
			if ( from is PlayerMobile && from.FindItemOnLayer( Layer.Ring ) != null && from.FindItemOnLayer( Layer.Ring ) is OneRing)
			{
				from.SendMessage( "O UM ANEL convence você a não fazer isso, e você o escuta..." );
				return;
			}

			// Check if healing is needed
			if ( from.Hits < from.HitsMax )
			{
				// Check if healing is prevented by poison or mortal strike
				if ( from.Poisoned || MortalStrike.IsWounded( from ) )
				{
					from.LocalOverheadMessage( MessageType.Regular, 0x22, 1005000 ); // You can not heal yourself in your current state.
				}
				else
				{
					// Check if heal delay has expired
					if ( from.BeginAction( typeof( BaseHealPotion ) ) )
					{
						DoHeal( from );
						BasePotion.PlayDrinkEffect( from );
						this.Consume();

						// Set delay timer before next heal potion can be used
						Timer.DelayCall( TimeSpan.FromSeconds( Delay ), new TimerStateCallback( ReleaseHealLock ), from );
					}
					else
					{
						from.LocalOverheadMessage( MessageType.Regular, 0x22, 500235 ); // You must wait 10 seconds before using another healing potion.
					}
				}
			}
			else
			{
				from.SendLocalizedMessage( 1049547 ); // You decide against drinking this potion, as you are already at full health.
			}
		}

		/// <summary>
		/// Releases the heal lock allowing the mobile to use another heal potion
		/// </summary>
		/// <param name="state">The mobile state object</param>
		private static void ReleaseHealLock( object state )
		{
			Mobile mobile = state as Mobile;
			if ( mobile != null )
			{
				mobile.EndAction( typeof( BaseHealPotion ) );
			}
		}

		#endregion
	}
}
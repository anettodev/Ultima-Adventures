using System;
using Server;
using Server.Spells;

namespace Server.Items
{
	/// <summary>
	/// Information about cure chance for a specific poison level
	/// </summary>
	public class CureLevelInfo
	{
		private Poison m_Poison;
		private double m_Chance;

		/// <summary>Gets the poison type this cure level affects</summary>
		public Poison Poison
		{
			get{ return m_Poison; }
		}

		/// <summary>Gets the chance to cure this poison level (0.0 to 1.0)</summary>
		public double Chance
		{
			get{ return m_Chance; }
		}

		/// <summary>
		/// Initializes a new instance of CureLevelInfo
		/// </summary>
		/// <param name="poison">The poison type</param>
		/// <param name="chance">The cure chance (0.0 to 1.0)</param>
		public CureLevelInfo( Poison poison, double chance )
		{
			m_Poison = poison;
			m_Chance = chance;
		}
	}

	/// <summary>
	/// Base class for all cure potions.
	/// Provides poison removal based on cure level and poison strength.
	/// </summary>
	public abstract class BaseCurePotion : BasePotion
	{
	#region Abstract Properties

	/// <summary>Gets the cure level information for this potion</summary>
	public abstract CureLevelInfo[] LevelInfo{ get; }

	#endregion

	#region Constants

	/// <summary>Visual effect ID for cure success</summary>
	private const int CURE_EFFECT_ID = 0x373A;

	/// <summary>Visual effect speed</summary>
	private const int EFFECT_SPEED = 10;

	/// <summary>Visual effect duration</summary>
	private const int EFFECT_DURATION = 15;

	/// <summary>Sound effect ID for cure</summary>
	private const int CURE_SOUND_ID = 0x1E0;

	/// <summary>Message color for success (green)</summary>
	private const int MESSAGE_COLOR_SUCCESS = 0x3F;

	/// <summary>Message color for failure (red)</summary>
	private const int MESSAGE_COLOR_FAILURE = 0x22;

	#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of BaseCurePotion
		/// </summary>
		/// <param name="effect">The potion effect type</param>
		public BaseCurePotion( PotionEffect effect ) : base( 0xF07, effect )
		{
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public BaseCurePotion( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the cure potion
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		/// <summary>
		/// Deserializes the cure potion
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
	/// Attempts to cure poison on the specified mobile
	/// </summary>
	/// <param name="from">The mobile to cure</param>
	public void DoCure( Mobile from )
	{
		bool cure = false;

		CureLevelInfo[] info = LevelInfo;

		// Check if this potion can cure the mobile's poison
		for ( int i = 0; i < info.Length; ++i )
		{
			CureLevelInfo li = info[i];

			if ( li.Poison == from.Poison && Scale( from, li.Chance ) > Utility.RandomDouble() )
			{
				cure = true;
				break;
			}
		}

		// Apply cure if successful
		if ( cure && from.CurePoison( from ) )
		{
			// Success message (PT-BR)
			from.SendMessage( MESSAGE_COLOR_SUCCESS, "Você se sente curado do veneno!" );

			// Visual and sound effects
			from.FixedEffect( CURE_EFFECT_ID, EFFECT_SPEED, EFFECT_DURATION );
			from.PlaySound( CURE_SOUND_ID );
		}
		else if ( !cure )
		{
			// Failure message with poison level info (PT-BR)
			string poisonName = GetPoisonLevelName( from.Poison );
			from.SendMessage( MESSAGE_COLOR_FAILURE, string.Format( "Esta poção não foi forte o suficiente para curar o {0}!", poisonName ) );
		}
	}

	/// <summary>
	/// Gets the Portuguese name for a poison level
	/// </summary>
	/// <param name="poison">The poison to get name for</param>
	/// <returns>PT-BR poison name</returns>
	private string GetPoisonLevelName( Poison poison )
	{
		if ( poison == null )
			return "o veneno";

		switch ( poison.Level )
		{
			case 0: return "veneno fraco";
			case 1: return "veneno menor";
			case 2: return "veneno";
			case 3: return "veneno maior";
			case 4: return "veneno mortal";
			case 5: return "veneno letal";
			default: return "veneno desconhecido";
		}
	}

	/// <summary>
	/// Handles drinking the cure potion
	/// </summary>
	/// <param name="from">The mobile drinking the potion</param>
	public override void Drink( Mobile from )
	{
		// Check if under vampiric embrace (garlic is deadly to vampires)
		if ( TransformationSpellHelper.UnderTransformation( from, typeof( Spells.Necromancy.VampiricEmbraceSpell ) ) )
		{
			from.SendMessage( MESSAGE_COLOR_FAILURE, "O alho na poção certamente mataria você!" );
		}
		else if ( from.Poisoned )
		{
			DoCure( from );

			BasePotion.PlayDrinkEffect( from );

			from.FixedParticles( CURE_EFFECT_ID, EFFECT_SPEED, EFFECT_DURATION, 5012, EffectLayer.Waist );
			from.PlaySound( CURE_SOUND_ID );

			this.Consume();
		}
		else
		{
			from.SendMessage( 0x59, "Você não está envenenado." ); // Cyan color
		}
	}

		#endregion
	}
}
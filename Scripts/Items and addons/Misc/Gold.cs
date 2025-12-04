using System;

namespace Server.Items
{
	/// <summary>
	/// Gold currency item - the standard monetary unit in Ultima Online.
	/// Gold pieces can be stacked up to the maximum amount limit.
	/// </summary>
	public class Gold : Item
	{
	#region Properties

	/// <summary>
	/// Gets the default name for gold items.
	/// </summary>
	public override string DefaultName
	{
		get { return GoldStringConstants.ITEM_NAME; }
	}

	/// <summary>
	/// Gets the default weight per gold piece.
	/// In ML (Mondain's Legacy) mode, gold is lighter (0.02 / 3).
	/// In legacy mode, gold weighs 0.02 per piece.
	/// </summary>
	public override double DefaultWeight
	{
		get { return ( Core.ML ? GoldConstants.WEIGHT_ML : GoldConstants.WEIGHT_LEGACY ); }
	}

	#endregion

		#region Constructors

		/// <summary>
		/// Creates a single gold piece.
		/// </summary>
		[Constructable]
		public Gold() : this( GoldConstants.DEFAULT_AMOUNT )
		{
		}

		/// <summary>
		/// Creates a random amount of gold between the specified range.
		/// </summary>
		/// <param name="amountFrom">Minimum amount of gold</param>
		/// <param name="amountTo">Maximum amount of gold</param>
		[Constructable]
		public Gold( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		/// <summary>
		/// Creates the specified amount of gold.
		/// Amounts outside valid range (0-60000) are clamped to default (1).
		/// </summary>
		/// <param name="amount">Amount of gold to create</param>
		[Constructable]
		public Gold( int amount ) : base( GoldConstants.ITEM_ID )
		{
			Stackable = true;

			if ( amount <= GoldConstants.MIN_AMOUNT || amount > GoldConstants.MAX_AMOUNT )
			{
				Amount = GoldConstants.DEFAULT_AMOUNT;
			}
			else
			{
				Amount = amount;
			}

			Light = GoldConstants.LIGHT_TYPE;
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		/// <param name="serial">Serialization reader</param>
		public Gold( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Gets the sound ID to play when gold is dropped.
		/// Different sounds are played based on the amount of gold.
		/// </summary>
		/// <returns>Sound ID based on gold amount</returns>
		public override int GetDropSound()
		{
			if ( Amount <= GoldConstants.AMOUNT_THRESHOLD_SINGLE )
			{
				return GoldConstants.DROP_SOUND_SINGLE;
			}
			else if ( Amount <= GoldConstants.AMOUNT_THRESHOLD_SMALL )
			{
				return GoldConstants.DROP_SOUND_SMALL;
			}
			else
			{
				return GoldConstants.DROP_SOUND_LARGE;
			}
		}

		/// <summary>
		/// Called when the amount of gold changes.
		/// Updates the total gold tracking for the container.
		/// </summary>
		/// <param name="oldValue">Previous amount of gold</param>
		protected override void OnAmountChange( int oldValue )
		{
			int newValue = Amount;

			UpdateTotal( this, TotalType.Gold, newValue - oldValue );
		}

		/// <summary>
		/// Gets the total value for the specified total type.
		/// For gold total type, returns the amount of gold in this stack.
		/// </summary>
		/// <param name="type">Type of total to calculate</param>
		/// <returns>Total value for the specified type</returns>
		public override int GetTotal( TotalType type )
		{
			int baseTotal = base.GetTotal( type );

			if ( type == TotalType.Gold )
			{
				baseTotal += Amount;
			}

			return baseTotal;
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the gold item.
		/// </summary>
		/// <param name="writer">Writer to serialize to</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( GoldConstants.SERIALIZATION_VERSION );
		}

		/// <summary>
		/// Deserializes the gold item.
		/// </summary>
		/// <param name="reader">Reader to deserialize from</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Light = GoldConstants.LIGHT_TYPE;
		}

		#endregion
	}
}
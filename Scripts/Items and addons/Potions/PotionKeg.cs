using System;
using Server;
using Server.Items;

namespace Server.Items
{
	/// <summary>
	/// A specially lined keg for storing up to 100 potions of the same type.
	/// Supports both bottle-based potions and jar-based mixtures.
	/// </summary>
	[FlipableAttribute( PotionKegConstants.KEG_ITEM_ID, PotionKegConstants.KEG_FLIPPED_ID )]
	public class PotionKeg : Item
	{
		#region Fields
		
		private PotionEffect m_Type;
		private int m_Held;

		#endregion
		
		#region Properties

		/// <summary>
		/// Number of potions currently held in the keg (0-100)
		/// </summary>
		[CommandProperty( AccessLevel.GameMaster )]
		public int Held
		{
			get
			{
				return m_Held;
			}
			set
			{
				if ( m_Held != value )
				{
					m_Held = value;
					UpdateWeight();
					InvalidateProperties();
				}
			}
		}

		/// <summary>
		/// Type of potion stored in the keg
		/// </summary>
		[CommandProperty( AccessLevel.GameMaster )]
		public PotionEffect Type
		{
			get
			{
				return m_Type;
			}
			set
			{
				m_Type = value;
				InvalidateProperties();
				
				// Automatically set hue and name based on potion type
				if ( m_Held > 0 )
				{
					this.Hue = PotionMetadata.GetKegHue( m_Type );
					SetColorKeg( this, this );
				}
				else
				{
					// Empty keg has default hue and name
					this.Hue = PotionKegConstants.EMPTY_KEG_HUE;
					this.Name = PotionKegStringConstants.KEG_NAME_EMPTY;
				}
			}
		}
		
		#endregion
		
		#region Constructors

		[Constructable]
		public PotionKeg() : base( PotionKegConstants.KEG_ITEM_ID )
		{
			UpdateWeight();
			SetColorKeg( this, this );
		}

		public PotionKeg( Serial serial ) : base( serial )
		{
		}
		
		#endregion
		
		#region Serialization

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
			writer.Write( (int) m_Type );
			writer.Write( (int) m_Held );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 1:
				case 0:
				{
					m_Type = (PotionEffect)reader.ReadInt();
					m_Held = reader.ReadInt();
					break;
				}
			}
			if ( version < 1 )
				Timer.DelayCall( TimeSpan.Zero, new TimerCallback( UpdateWeight ) );
		}
		
		#endregion
		
		#region Static Initialization

		public static void Initialize()
		{
			TileData.ItemTable[PotionKegConstants.KEG_ITEM_ID].Height = PotionKegConstants.KEG_TILE_HEIGHT;
		}
		
		#endregion
		
		#region Property Display

		public override int LabelNumber
		{ 
			get
			{ 
				if ( m_Held == 0 )
					return PotionKegStringConstants.MSG_KEG_DESCRIPTION;
				else if( m_Type >= PotionEffect.Conflagration )
					return 1072658 + (int) m_Type - (int) PotionEffect.Conflagration;
				else
					return ( 1041620 + (int)m_Type ); 
			} 
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			// Display potion type in custom cyan color (#8be4fc) with brackets if keg contains potions
			if ( m_Held > 0 )
			{
				string potionName = PotionMetadata.GetKegName( m_Type );
				if ( potionName != null )
				{
					list.Add( 1070722, string.Format( "<BASEFONT COLOR=#8be4fc>[{0}]", potionName ) ); // Custom cyan color #8be4fc
				}
			}

			// Add colored fill level message
			list.Add( 1070722, GetColoredFillLevelMessage() );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );
			from.SendMessage( GetColoredFillLevelMessage() );
		}
		
		#endregion
		
		#region User Interaction

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( GetWorldLocation(), PotionKegConstants.INTERACTION_RANGE ) )
			{
				if ( m_Held > 0 )
				{
					Container pack = from.Backpack;

					if ( pack != null && ( ( IsJarPotion( m_Type ) && pack.ConsumeTotal( typeof( Jar ), 1 ) ) || ( !IsJarPotion( m_Type ) && pack.ConsumeTotal( typeof( Bottle ), 1 ) ) ) )
					{
						from.SendMessage( PotionKegStringConstants.MSG_POUR_INTO_BOTTLE );

						BasePotion pot = FillBottle();

						if ( pack.TryDropItem( from, pot, false ) )
						{
					from.SendMessage( PotionKegStringConstants.MSG_PLACE_IN_BACKPACK );
					from.PlaySound( PotionKegConstants.POUR_SOUND_ID );

					// Check if player has invisibility potion effect active
					// Grabbing a potion from a keg (except invisibility potions) reveals the player
					if ( !(pot is BaseInvisibilityPotion) )
					{
						BaseInvisibilityPotion.CheckRevealOnAction( from, "pegou uma poção do barril" );
					}

							if ( --Held == 0 )
						from.SendMessage( PotionKegStringConstants.MSG_KEG_NOW_EMPTY );
					
					// Force client to refresh tooltip immediately
					InvalidateProperties();
					Delta( ItemDelta.Properties );
						}
						else
						{
							from.SendMessage( PotionKegStringConstants.MSG_NO_ROOM_FOR_BOTTLE );
							pot.Delete();
						}
					}
					else
					{
						// TODO: Target a bottle
					}
				}
				else
				{
					from.SendMessage( PotionKegStringConstants.MSG_KEG_EMPTY );
				}
				SetColorKeg( this, this );
			}
			else
			{
				from.SendMessage( PotionKegStringConstants.MSG_CANT_REACH );
			}
		}

		public override bool OnDragDrop( Mobile from, Item item )
		{
			if ( item is BasePotion )
			{
				BasePotion pot = (BasePotion)item;
				int toHold = Math.Min( PotionKegConstants.MAX_CAPACITY - m_Held, pot.Amount );

				if ( toHold <= 0 )
				{
					from.SendMessage( PotionKegStringConstants.MSG_KEG_FULL_CANNOT_ADD );
					return false;
				}
				else if ( m_Held == 0 )
				{
					if ( GiveBottle( from, toHold, item ) )
					{
					this.Hue = PotionKegConstants.EMPTY_KEG_HUE;
						m_Type = pot.PotionEffect;
						Held = toHold;

						SetColorKeg( item, this );

					from.PlaySound( PotionKegConstants.POUR_SOUND_ID );
					from.SendMessage( PotionKegStringConstants.MSG_EMPTY_BOTTLE_IN_PACK );

                        item.Consume( toHold );

						if( !item.Deleted )
							item.Bounce( from );

					// Force client to refresh tooltip immediately
					InvalidateProperties();
					Delta( ItemDelta.Properties );

						return true;
					}
					else
					{
						from.SendMessage( PotionKegStringConstants.MSG_NO_ROOM_FOR_EMPTY );
						return false;
					}
				}
				else if ( pot.PotionEffect != m_Type )
				{
					from.SendMessage( PotionKegStringConstants.MSG_CANNOT_MIX_TYPES );
					return false;
				}
				else
				{
					if ( GiveBottle( from, toHold, item ) )
					{
						Held += toHold;

					from.PlaySound( PotionKegConstants.POUR_SOUND_ID );
					from.SendMessage( PotionKegStringConstants.MSG_EMPTY_BOTTLE_IN_PACK );

						item.Consume( toHold );

						if( !item.Deleted )
							item.Bounce( from );

					// Force client to refresh tooltip immediately
					InvalidateProperties();
					Delta( ItemDelta.Properties );

						return true;
					}
					else
					{
						from.SendMessage( PotionKegStringConstants.MSG_NO_ROOM_FOR_EMPTY );
						return false;
					}
				}
			}
			else
			{
				from.SendMessage( PotionKegStringConstants.MSG_INVALID_ITEM_TYPE );
				return false;
			}
		}

		#endregion
		
		#region Helper Methods

		/// <summary>
		/// Updates the keg weight based on the number of potions held
		/// </summary>
		public virtual void UpdateWeight()
		{
			int held = Math.Max( 0, Math.Min( m_Held, PotionKegConstants.MAX_CAPACITY ) );
			this.Weight = PotionKegConstants.BASE_WEIGHT + ((held * PotionKegConstants.FULL_WEIGHT_ADDITION) / PotionKegConstants.MAX_CAPACITY);
			SetColorKeg( this, this );
		}

		/// <summary>
		/// Gets the colored fill level message based on keg capacity
		/// Colors: White (empty), Red (< 25%), Orange (25-49%), Yellow (50-74%), Green (75-100%)
		/// </summary>
		/// <returns>HTML formatted colored message in PT-BR</returns>
		private string GetColoredFillLevelMessage()
		{
			string message;
			string color;
			
			// Determine message based on fill level
			if ( m_Held <= 0 )
				message = PotionKegStringConstants.MSG_KEG_EMPTY;
			else if ( m_Held < PotionKegConstants.FILL_NEARLY_EMPTY )
				message = PotionKegStringConstants.MSG_KEG_NEARLY_EMPTY;
			else if ( m_Held < PotionKegConstants.FILL_NOT_VERY_FULL )
				message = PotionKegStringConstants.MSG_KEG_NOT_VERY_FULL;
			else if ( m_Held < PotionKegConstants.FILL_QUARTER_FULL )
				message = PotionKegStringConstants.MSG_KEG_QUARTER_FULL;
			else if ( m_Held < PotionKegConstants.FILL_THIRD_FULL )
				message = PotionKegStringConstants.MSG_KEG_THIRD_FULL;
			else if ( m_Held < PotionKegConstants.FILL_ALMOST_HALF )
				message = PotionKegStringConstants.MSG_KEG_ALMOST_HALF;
			else if ( m_Held < PotionKegConstants.FILL_HALF_FULL )
				message = PotionKegStringConstants.MSG_KEG_HALF_FULL;
			else if ( m_Held < PotionKegConstants.FILL_MORE_THAN_HALF )
				message = PotionKegStringConstants.MSG_KEG_MORE_THAN_HALF;
			else if ( m_Held < PotionKegConstants.FILL_THREE_QUARTERS )
				message = PotionKegStringConstants.MSG_KEG_THREE_QUARTERS;
			else if ( m_Held < PotionKegConstants.FILL_VERY_FULL )
				message = PotionKegStringConstants.MSG_KEG_VERY_FULL;
			else if ( m_Held < PotionKegConstants.FILL_ALMOST_TOP )
				message = PotionKegStringConstants.MSG_KEG_ALMOST_TOP;
			else
				message = PotionKegStringConstants.MSG_KEG_FULL;
			
			// Determine color based on fill percentage
			if ( m_Held <= 0 )
				color = "#FFFFFF"; // White - empty
			else if ( m_Held < 25 )
				color = "#FF0000"; // Red - less than 1/4
			else if ( m_Held < 50 )
				color = "#FFA500"; // Orange - 1/4 to 1/2
			else if ( m_Held < 75 )
				color = "#FFFF00"; // Yellow - 1/2 to 3/4
			else
				color = "#00FF00"; // Green - more than 3/4
			
			return string.Format( "<BASEFONT COLOR={0}>{1}", color, message );
		}

		/// <summary>
		/// Returns empty container (bottle or jar) to player's backpack
		/// </summary>
		/// <param name="m">Mobile to receive container</param>
		/// <param name="amount">Number of containers to return</param>
		/// <param name="potion">Potion item to determine container type</param>
		/// <returns>True if containers were successfully added, false if backpack full</returns>
		public bool GiveBottle( Mobile m, int amount, Item potion )
		{
			Container pack = m.Backpack;

			if ( potion is BaseMixture )
			{
				Jar jar = new Jar( amount );

				if ( pack == null || !pack.TryDropItem( m, jar, false ) )
				{
					jar.Delete();
					return false;
				}
			}
			else
			{
				Bottle bottle = new Bottle( amount );

				if ( pack == null || !pack.TryDropItem( m, bottle, false ) )
				{
					bottle.Delete();
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Creates a potion instance based on the keg's potion type
		/// </summary>
		/// <returns>New potion instance, or null if type not found</returns>
		public BasePotion FillBottle()
		{
			return PotionMetadata.CreatePotion( m_Type );
		}

		/// <summary>
		/// Sets the color and name of a keg based on its potion type
		/// </summary>
		/// <param name="potion">Potion being added (or keg itself if updating display)</param>
		/// <param name="keg">The keg to update</param>
		public static void SetColorKeg( Item potion, Item keg )
		{
			PotionKeg p = (PotionKeg)keg;

			if ( p.Held < 1 )
			{ 
				keg.Hue = PotionKegConstants.EMPTY_KEG_HUE;
				keg.Name = PotionKegStringConstants.KEG_NAME_EMPTY;
			}
			else
			{
				// Update type if adding new potion
				if ( potion != keg && potion is BasePotion )
					{
						BasePotion pot = (BasePotion)potion;
						p.Type = pot.PotionEffect;
					keg.Hue = PotionMetadata.GetKegHue( p.Type );
				}
				
				// Keg name is always "barril de poções" (potion type shown in properties)
				keg.Name = PotionKegStringConstants.KEG_NAME_FORMAT;
			}
		}

		/// <summary>
		/// Gets the hue/color for a potion keg based on potion type
		/// </summary>
		/// <param name="potion">Potion item to get color for</param>
		/// <returns>Hue value for the keg</returns>
		public static int GetPotionColor( Item potion )
		{
			if ( potion is BasePotion )
			{
				BasePotion pot = (BasePotion)potion;
				return PotionMetadata.GetKegHue( pot.PotionEffect );
			}
			
			return 0;
		}

		/// <summary>
		/// Checks if a potion type uses jars instead of bottles
		/// </summary>
		/// <param name="p">The potion effect type</param>
		/// <returns>True if uses jars, false if uses bottles</returns>
		public static bool IsJarPotion( PotionEffect p )
		{
			return PotionMetadata.UsesJar( p );
		}
		
		#endregion
	}
}
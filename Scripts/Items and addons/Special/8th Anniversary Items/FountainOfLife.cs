using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Multis;

namespace Server.Items
{
	/// <summary>
	/// Enhanced bandage with improved healing properties
	/// Created by placing regular bandages in a Fountain of Life
	/// </summary>
	public class EnhancedBandage : Bandage
	{
		#region Properties

		/// <summary>
		/// Gets the healing bonus provided by enhanced bandages
		/// </summary>
		public static int HealingBonus { get { return FountainOfLifeConstants.ENHANCED_BANDAGE_HEALING_BONUS; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of EnhancedBandage with amount 1
		/// </summary>
		[Constructable]
		public EnhancedBandage()
			: this( 1 )
		{
		}

		/// <summary>
		/// Initializes a new instance of EnhancedBandage with specified amount
		/// </summary>
		/// <param name="amount">Number of enhanced bandages in the stack</param>
		[Constructable]
		public EnhancedBandage( int amount )
			: base( amount )
		{
			Hue = BandageConstants.HUE_ENHANCED_BANDAGE;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public EnhancedBandage( Serial serial )
			: base( serial )
		{
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds enhanced bandage property to the property list
		/// </summary>
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1070722, ItemNameHue.UnifiedItemProps.SetColor( BandageStringConstants.MSG_ENHANCED_BANDAGE_PROPERTY, BandageStringConstants.ENHANCED_BANDAGE_COLOR_HEX ) );
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the enhanced bandage state
		/// </summary>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); //version
		}

		/// <summary>
		/// Deserializes the enhanced bandage state and ensures hue is maintained
		/// </summary>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
			
			// Ensure enhanced bandage maintains its special hue
			Hue = BandageConstants.HUE_ENHANCED_BANDAGE;
		}

		#endregion
	}

	[FlipableAttribute( FountainOfLifeConstants.FOUNTAIN_ITEM_ID, FountainOfLifeConstants.FOUNTAIN_FLIPABLE_ITEM_ID )]
	public class FountainOfLife : BaseAddonContainer
	{
		#region Constants

		/// <summary>Static readonly recharge time to avoid repeated allocations</summary>
		private static readonly TimeSpan s_RechargeTime = TimeSpan.FromDays( FountainOfLifeConstants.FOUNTAIN_RECHARGE_DAYS );

		#endregion

		#region Properties

		public override BaseAddonContainerDeed Deed
		{
			get { return new FountainOfLifeDeed( m_Charges ); }
		}

		/// <summary>
		/// Gets the recharge time for the fountain (1 day)
		/// </summary>
		public virtual TimeSpan RechargeTime { get { return s_RechargeTime; } }

		/// <summary>
		/// Gets the label number for "Fountain of Life"
		/// </summary>
		public override int LabelNumber { get { return FountainOfLifeConstants.CLILOC_FOUNTAIN_OF_LIFE; } }

		/// <summary>
		/// Gets the default gump ID for the fountain
		/// </summary>
		public override int DefaultGumpID { get { return FountainOfLifeConstants.FOUNTAIN_DEFAULT_GUMP_ID; } }

		/// <summary>
		/// Gets the default drop sound for the fountain
		/// </summary>
		public override int DefaultDropSound { get { return FountainOfLifeConstants.FOUNTAIN_DEFAULT_DROP_SOUND; } }

		/// <summary>
		/// Gets the default maximum items the fountain can hold
		/// </summary>
		public override int DefaultMaxItems { get { return FountainOfLifeConstants.FOUNTAIN_DEFAULT_MAX_ITEMS; } }

		#endregion

		#region Fields

		private int m_Charges;
		private Timer m_Timer;

		#endregion

		#region Properties (Charges)

		/// <summary>
		/// Gets or sets the number of charges remaining in the fountain
		/// </summary>
		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get { return m_Charges; }
			set { m_Charges = Math.Min( value, FountainOfLifeConstants.FOUNTAIN_MAX_CHARGES ); InvalidateProperties(); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of FountainOfLife with default charges
		/// </summary>
		[Constructable]
		public FountainOfLife()
			: this( FountainOfLifeConstants.FOUNTAIN_DEFAULT_CHARGES )
		{
		}

		/// <summary>
		/// Initializes a new instance of FountainOfLife with specified charges
		/// </summary>
		/// <param name="charges">Initial number of charges</param>
		[Constructable]
		public FountainOfLife( int charges )
			: base( FountainOfLifeConstants.FOUNTAIN_ITEM_ID )
		{
			m_Charges = charges;

			m_Timer = Timer.DelayCall( RechargeTime, RechargeTime, new TimerCallback( Recharge ) );
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public FountainOfLife( Serial serial )
			: base( serial )
		{
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Prevents the fountain from being lifted
		/// </summary>
		public override bool OnDragLift( Mobile from )
		{
			return false;
		}

		/// <summary>
		/// Handles item deletion and stops the recharge timer
		/// </summary>
		public override void OnDelete()
		{
			if( m_Timer != null )
				m_Timer.Stop();

			base.OnDelete();
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles bandage drop into the fountain
		/// </summary>
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			return TryProcessBandageDrop( from, dropped, () => base.OnDragDrop( from, dropped ) );
		}

		/// <summary>
		/// Handles bandage drop into specific location in the fountain
		/// </summary>
		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			return TryProcessBandageDrop( from, item, () => base.OnDragDropInto( from, item, p ) );
		}

		/// <summary>
		/// Shared logic for processing bandage drops (eliminates DRY violation)
		/// </summary>
		/// <param name="from">The mobile dropping the item</param>
		/// <param name="dropped">The item being dropped</param>
		/// <param name="baseDropAction">The base drop action to execute if item is valid</param>
		/// <returns>True if drop was successful, false otherwise</returns>
		private bool TryProcessBandageDrop( Mobile from, Item dropped, System.Func<bool> baseDropAction )
		{
			if( dropped is Bandage )
			{
				bool allow = baseDropAction();

				if( allow )
					Enhance();

				return allow;
			}
			else
			{
				from.SendLocalizedMessage( FountainOfLifeConstants.CLILOC_ONLY_BANDAGES );
				return false;
			}
		}

		/// <summary>
		/// Recharges the fountain to maximum charges and enhances any bandages inside
		/// </summary>
		public void Recharge()
		{
			m_Charges = FountainOfLifeConstants.FOUNTAIN_MAX_CHARGES;

			Enhance();
		}

		/// <summary>
		/// Enhances regular bandages in the fountain to enhanced bandages, consuming charges
		/// Processes bandages in reverse order to safely remove items during iteration
		/// </summary>
		public void Enhance()
		{
			for( int i = Items.Count - 1; i >= 0 && m_Charges > 0; i-- )
			{
				Bandage bandage = Items[ i ] as Bandage;

				if( bandage != null )
				{
					if( bandage.Amount > m_Charges )
					{
						// Partial conversion: consume remaining charges
						bandage.Amount -= m_Charges;
						DropItem( new EnhancedBandage( m_Charges ) );
						m_Charges = 0;
					}
					else
					{
						// Full conversion: convert entire stack
						DropItem( new EnhancedBandage( bandage.Amount ) );
						m_Charges -= bandage.Amount;
						bandage.Delete();
					}

					InvalidateProperties();
				}
			}
		}

		#endregion

		#region Property Display

		/// <summary>
		/// Adds charge information to the property list
		/// </summary>
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( FountainOfLifeConstants.CLILOC_CHARGES_REMAINING, m_Charges.ToString() );
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the fountain state including charges and timer
		/// </summary>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); //version

			writer.Write( m_Charges );
			writer.Write( (DateTime)m_Timer.Next );
		}

		/// <summary>
		/// Deserializes the fountain state and restarts the recharge timer
		/// </summary>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_Charges = reader.ReadInt();

			DateTime next = reader.ReadDateTime();

			if( next < DateTime.UtcNow )
				m_Timer = Timer.DelayCall( TimeSpan.Zero, RechargeTime, new TimerCallback( Recharge ) );
			else
				m_Timer = Timer.DelayCall( next - DateTime.UtcNow, RechargeTime, new TimerCallback( Recharge ) );
		}

		#endregion
	}

	/// <summary>
	/// Deed for placing a Fountain of Life addon
	/// </summary>
	public class FountainOfLifeDeed : BaseAddonContainerDeed
	{
		#region Properties

		/// <summary>
		/// Gets the label number for "Fountain of Life"
		/// </summary>
		public override int LabelNumber { get { return FountainOfLifeConstants.CLILOC_FOUNTAIN_OF_LIFE; } }

		/// <summary>
		/// Gets the addon created when this deed is used
		/// </summary>
		public override BaseAddonContainer Addon { get { return new FountainOfLife( m_Charges ); } }

		/// <summary>
		/// Gets or sets the hue of the deed (matches enhanced bandage hue)
		/// </summary>
		public override int Hue
		{
			get
			{
				int baseHue = base.Hue;
				return baseHue == 0 ? BandageConstants.HUE_ENHANCED_BANDAGE : baseHue;
			}
			set { base.Hue = value; }
		}

		#endregion

		#region Fields

		private int m_Charges;

		#endregion

		#region Properties (Charges)

		/// <summary>
		/// Gets or sets the number of charges the fountain will have when placed
		/// </summary>
		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get { return m_Charges; }
			set { m_Charges = Math.Min( value, FountainOfLifeConstants.FOUNTAIN_MAX_CHARGES ); InvalidateProperties(); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of FountainOfLifeDeed with default charges
		/// </summary>
		[Constructable]
		public FountainOfLifeDeed()
			: this( FountainOfLifeConstants.FOUNTAIN_DEFAULT_CHARGES )
		{
		}

		/// <summary>
		/// Initializes a new instance of FountainOfLifeDeed with specified charges
		/// </summary>
		/// <param name="charges">Initial number of charges for the fountain</param>
		[Constructable]
		public FountainOfLifeDeed( int charges )
			: base()
		{
			LootType = LootType.Blessed;
			m_Charges = charges;
			Hue = BandageConstants.HUE_ENHANCED_BANDAGE;
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		public FountainOfLifeDeed( Serial serial )
			: base( serial )
		{
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the deed state including charges
		/// </summary>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); //version

			writer.Write( m_Charges );
		}

		/// <summary>
		/// Deserializes the deed state including charges
		/// </summary>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_Charges = reader.ReadInt();
			
			// Ensure deed maintains its special hue
			Hue = BandageConstants.HUE_ENHANCED_BANDAGE;
		}

		#endregion
	}
}

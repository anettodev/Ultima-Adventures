using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Engines.Craft;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Base class for all log types that can be converted to boards via sawmill.
	/// Handles resource gating, skill-based conversion, and board generation.
	/// </summary>
	public abstract class BaseLog : Item, ICommodity
	{
		#region Fields

		private CraftResource m_Resource;

		#endregion

		#region Properties

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get { return m_Resource; }
			set { m_Resource = value; InvalidateProperties(); }
		}

		int ICommodity.DescriptionNumber { get { return CraftResources.IsStandard( m_Resource ) ? LabelNumber : 1075062 + ( (int)m_Resource - (int)CraftResource.RegularWood ); } }
		bool ICommodity.IsDeedable { get { return true; } }

		#endregion

		#region Constructors

		[Constructable]
		public BaseLog() : this( 1 )
		{
		}

		[Constructable]
		public BaseLog( int amount ) : this( CraftResource.RegularWood, amount )
		{
		}

		[Constructable]
		public BaseLog( CraftResource resource ) : this( resource, 1 )
		{
		}

		[Constructable]
		public BaseLog( CraftResource resource, int amount ) : base( LogConstants.ITEM_ID_LOG )
		{
			Stackable = true;
			Weight = LogConstants.WEIGHT_BASE;
			Amount = amount;
			Name = LogStringConstants.ITEM_NAME_LOG;
			m_Resource = resource;
			Hue = CraftResources.GetHue( resource );
		}

		#endregion

		#region Abstract Methods

		/// <summary>
		/// Gets the board type that this log converts to
		/// </summary>
		public abstract BaseWoodBoard GetLog();

		#endregion

		#region Property Display

		/// <summary>
		/// Gets the resource type display name for the tooltip properties.
		/// Maps CraftResource enum values to PT-BR display names from LogNameConstants.
		/// </summary>
		/// <returns>The resource type display name, or null if not found</returns>
		public virtual string GetResourceTypeDisplayName()
		{
			switch ( m_Resource )
			{
				case CraftResource.RegularWood: return LogNameConstants.REGULAR_WOOD_DISPLAY_NAME;
				case CraftResource.AshTree: return LogNameConstants.ASH_TREE_DISPLAY_NAME;
				case CraftResource.EbonyTree: return LogNameConstants.EBONY_TREE_DISPLAY_NAME;
				case CraftResource.ElvenTree: return LogNameConstants.ELVEN_TREE_DISPLAY_NAME;
				case CraftResource.GoldenOakTree: return LogNameConstants.GOLDEN_OAK_TREE_DISPLAY_NAME;
				case CraftResource.CherryTree: return LogNameConstants.CHERRY_TREE_DISPLAY_NAME;
				case CraftResource.RosewoodTree: return LogNameConstants.ROSEWOOD_TREE_DISPLAY_NAME;
				case CraftResource.HickoryTree: return LogNameConstants.HICKORY_TREE_DISPLAY_NAME;
				default: return null;
			}
		}

		/// <summary>
		/// Adds properties to the object property list (tooltip).
		/// Shows the resource type using custom PT-BR names (including RegularWood).
		/// </summary>
		/// <param name="list">The object property list to add to</param>
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			// Try to get custom PT-BR display name first (includes RegularWood)
			string customName = GetResourceTypeDisplayName();
			
			if ( customName != null )
			{
				list.Add( 1053099, ItemNameHue.UnifiedItemProps.SetColor( customName, LogStringConstants.COLOR_CYAN ) );
			}
			else if ( !CraftResources.IsStandard( m_Resource ) )
			{
				// Fallback to original system if custom name not found (only for non-standard resources)
				int num = CraftResources.GetLocalizationNumber( m_Resource );

				if ( num > 0 )
					list.Add( num );
				else
				{
					string resourceName = CraftResources.GetName( m_Resource );
					if ( !string.IsNullOrEmpty( resourceName ) )
						list.Add( resourceName );
				}
			}
		}

		#endregion

		#region Serialization

		public BaseLog( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int)m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
					{
						m_Resource = (CraftResource)reader.ReadInt();
						break;
					}
			}

			if ( version == 0 )
				m_Resource = CraftResource.RegularWood;

			ItemID = LogConstants.ITEM_ID_LOG;
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles double-click to initiate sawmill targeting
		/// </summary>
		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;
			
			if ( RootParent is BaseCreature )
			{
				from.SendLocalizedMessage( 500447 ); // That is not accessible
				return;
			}
			else if ( from.InRange( this.GetWorldLocation(), LogConstants.TARGET_RANGE ) )
			{
				from.SendMessage( LogStringConstants.MSG_SELECT_SAWMILL );
				from.Target = new InternalTarget( this );
			}
			else
			{
				from.SendMessage( LogStringConstants.MSG_LOGS_TOO_FAR );
			}
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Gets the conversion difficulty for the log's resource type
		/// </summary>
		/// <param name="resource">The CraftResource to get difficulty for</param>
		/// <returns>Difficulty value, or default if resource not found</returns>
		private static double GetDifficultyForResource( CraftResource resource )
		{
			double difficulty;
			if ( LogConstants.RESOURCE_DIFFICULTY.TryGetValue( resource, out difficulty ) )
			{
				return difficulty;
			}
			return LogConstants.DIFFICULTY_DEFAULT;
		}

		/// <summary>
		/// Calculates the conversion multiplier based on player's Lumberjacking skill
		/// </summary>
		/// <param name="skillValue">The player's Lumberjacking skill value</param>
		/// <returns>Conversion multiplier (1.0 to 1.5)</returns>
		private static double GetConversionMultiplier( double skillValue )
		{
			if ( skillValue < LogConstants.CONVERSION_SKILL_THRESHOLD )
			{
				return LogConstants.CONVERSION_MULTIPLIER_BASE;
			}
			else if ( skillValue >= 120.0 )
			{
				return LogConstants.CONVERSION_MULTIPLIER_120;
			}
			else if ( skillValue >= 110.0 )
			{
				return LogConstants.CONVERSION_MULTIPLIER_110;
			}
			else if ( skillValue >= 100.0 )
			{
				return LogConstants.CONVERSION_MULTIPLIER_100;
			}
			else if ( skillValue >= 90.0 )
			{
				return LogConstants.CONVERSION_MULTIPLIER_90;
			}
			else // skillValue >= 80.0
			{
				return LogConstants.CONVERSION_MULTIPLIER_80;
			}
		}

		/// <summary>
		/// Validates if the player has sufficient skill to attempt conversion
		/// </summary>
		/// <param name="from">The player attempting conversion</param>
		/// <param name="difficulty">The difficulty of the conversion</param>
		/// <returns>True if skill check passes, false otherwise</returns>
		private static bool ValidateSkillRequirement( Mobile from, double difficulty )
		{
			if ( difficulty > LogConstants.SKILL_THRESHOLD && difficulty > from.Skills[SkillName.Lumberjacking].Value )
			{
				from.SendMessage( LogConstants.MSG_COLOR_ERROR, LogStringConstants.MSG_INSUFFICIENT_SKILL );
				return false;
			}
			return true;
		}

		/// <summary>
		/// Converts log to board and adds to player's backpack
		/// Applies skill-based multiplier (1.0x to 1.5x) with ceiling rounding
		/// </summary>
		/// <param name="from">The player performing conversion</param>
		/// <param name="log">The log being converted</param>
		private static void ConvertLogToBoard( Mobile from, BaseLog log )
		{
			if ( log.Amount <= 0 )
			{
				from.SendMessage( LogConstants.MSG_COLOR_ERROR, LogStringConstants.MSG_INSUFFICIENT_WOOD );
				return;
			}

			int logAmount = log.Amount;
			BaseWoodBoard wood = log.GetLog();
			log.Delete();
			
			// Calculate skill-based multiplier
			double skillValue = from.Skills[SkillName.Lumberjacking].Value;
			double multiplier = GetConversionMultiplier( skillValue );
			
			// Apply multiplier with ceiling (round up)
			int boardAmount = (int)Math.Ceiling( logAmount * multiplier );
			wood.Amount = boardAmount;
			
			from.AddToBackpack( wood );
			from.PlaySound( LogConstants.SOUND_ID_SAWMILL );
			from.SendMessage( LogConstants.MSG_COLOR_ERROR, LogStringConstants.MSG_CONVERSION_SUCCESS );
			
			// Show bonus message and emote if multiplier bonus is applied
			if ( multiplier > LogConstants.CONVERSION_MULTIPLIER_BASE )
			{
				int bonusPercent = (int)( ( multiplier - LogConstants.CONVERSION_MULTIPLIER_BASE ) * 100.0 );
				string bonusMessage = string.Format( LogStringConstants.MSG_CONVERSION_BONUS_FORMAT, bonusPercent );
				
				// Play woohoo sound (gender-specific)
				from.PlaySound( from.Female ? LogConstants.SOUND_WOOHOO_FEMALE : LogConstants.SOUND_WOOHOO_MALE );
				
				// Send bonus message in cyan color
				from.SendMessage( LogConstants.MSG_COLOR_CYAN, bonusMessage );
				
				// Display emote
				from.Emote( LogStringConstants.EMOTE_BONUS_CONVERSION );
			}
		}

		/// <summary>
		/// Handles conversion failure, removing some or all of the log
		/// </summary>
		/// <param name="from">The player who failed conversion</param>
		/// <param name="log">The log being converted</param>
		private static void HandleConversionFailure( Mobile from, BaseLog log )
		{
			int amount = log.Amount;
			int lose = Utility.RandomMinMax( 1, amount );

			if ( amount < 2 || lose == amount )
			{
				log.Delete();
				from.SendMessage( LogConstants.MSG_COLOR_ERROR, LogStringConstants.MSG_CONVERSION_FAILURE_ALL );
			}
			else
			{
				log.Amount = amount - lose;
				from.SendMessage( LogConstants.MSG_COLOR_ERROR, LogStringConstants.MSG_CONVERSION_FAILURE_PARTIAL );
			}

			from.PlaySound( LogConstants.SOUND_ID_SAWMILL );
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Internal target class for sawmill selection
		/// </summary>
		private class InternalTarget : Target
		{
			private BaseLog m_Log;

			public InternalTarget( BaseLog log ) : base( LogConstants.TARGET_RANGE, false, TargetFlags.None )
			{
				m_Log = log;
			}

			/// <summary>
			/// Validates if the targeted object is a valid sawmill
			/// </summary>
			/// <param name="obj">The object being targeted</param>
			/// <returns>True if object is a valid sawmill, false otherwise</returns>
			private bool IsMill( object obj )
			{
				if ( obj is Item )
				{
					Item saw = (Item)obj;

					if ( saw.Name == LogStringConstants.SAWMILL_NAME && LogConstants.SAWMILL_ITEM_IDS.Contains( saw.ItemID ) )
					{
						return true;
					}
				}

				return false;
			}

			/// <summary>
			/// Handles sawmill targeting and log conversion
			/// </summary>
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Log.Deleted )
					return;

				if ( !from.InRange( m_Log.GetWorldLocation(), LogConstants.TARGET_RANGE ) )
				{
					from.SendMessage( LogStringConstants.MSG_LOGS_TOO_FAR );
					return;
				}

				if ( !IsMill( targeted ) )
				{
					from.SendMessage( LogConstants.MSG_COLOR_ERROR, LogStringConstants.MSG_NOT_SAWMILL );
					return;
				}

				// RESOURCE GATING: Check if this wood type is gated (unknown to players)
				if ( Server.Misc.ResourceGating.IsResourceGated( m_Log.Resource ) )
				{
					from.SendMessage( LogConstants.MSG_COLOR_ERROR, Server.Misc.ResourceGating.MSG_CANNOT_CONVERT_GATED_LOGS );
					return;
				}

				double difficulty = GetDifficultyForResource( m_Log.Resource );
				double minSkill = difficulty - LogConstants.SKILL_RANGE_OFFSET;
				double maxSkill = difficulty + LogConstants.SKILL_RANGE_OFFSET;

				if ( !ValidateSkillRequirement( from, difficulty ) )
				{
					return;
				}

				if ( from.CheckTargetSkill( SkillName.Lumberjacking, targeted, minSkill, maxSkill ) )
				{
					ConvertLogToBoard( from, m_Log );
				}
				else
				{
					HandleConversionFailure( from, m_Log );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				// Target sequence complete
			}
		}
		#endregion
	}

	#region Log Classes

	/// <summary>
	/// Regular wood log - converts to standard Board
	/// </summary>
	public class Log : BaseLog
	{
		[Constructable]
		public Log() : this( 1 )
		{
		}
		[Constructable]
		public Log( int amount ) : base( CraftResource.RegularWood, amount )
		{
		}
		public Log( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Weight = LogConstants.WEIGHT_STANDARD;
		}

		public override BaseWoodBoard GetLog()
		{
			return new Board();
		}
	}

	/// <summary>
	/// Ash tree log - converts to AshBoard
	/// </summary>
	public class AshLog : BaseLog
	{
		[Constructable]
		public AshLog() : this(1)
		{
		}
		[Constructable]
		public AshLog(int amount) : base(CraftResource.AshTree, amount)
		{
		}
		public AshLog(Serial serial) : base(serial)
		{
		}
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Weight = LogConstants.WEIGHT_STANDARD;
		}

		public override BaseWoodBoard GetLog()
		{
			return new AshBoard();
		}
	}

	/// <summary>
	/// Ebony tree log - converts to EbonyBoard
	/// </summary>
	public class EbonyLog : BaseLog
    {
        [Constructable]
        public EbonyLog() : this(1)
        {
        }
        [Constructable]
        public EbonyLog(int amount) : base(CraftResource.EbonyTree, amount)
        {
        }
        public EbonyLog(Serial serial) : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Weight = LogConstants.WEIGHT_MEDIUM;
		}

		public override BaseWoodBoard GetLog()
		{
			return new EbonyBoard();
		}
	}

	/// <summary>
	/// Elven tree log - converts to ElvenBoard
	/// </summary>
	public class ElvenLog : BaseLog
    {
        [Constructable]
        public ElvenLog() : this(1)
        {
        }
        [Constructable]
        public ElvenLog(int amount) : base(CraftResource.ElvenTree, amount)
        {
        }
        public ElvenLog(Serial serial) : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Weight = LogConstants.WEIGHT_STANDARD;
		}

		public override BaseWoodBoard GetLog()
		{
			return new ElvenBoard();
		}
	}

	/// <summary>
	/// Golden oak tree log - converts to GoldenOakBoard
	/// </summary>
	public class GoldenOakLog : BaseLog
	{
		[Constructable]
		public GoldenOakLog() : this( 1 )
		{
		}
		[Constructable]
		public GoldenOakLog( int amount ) : base( CraftResource.GoldenOakTree, amount )
		{
		}
		public GoldenOakLog( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Weight = LogConstants.WEIGHT_HEAVY;
		}

		public override BaseWoodBoard GetLog()
		{
			return new GoldenOakBoard();
		}
	}

	/// <summary>
	/// Cherry tree log - converts to CherryBoard
	/// </summary>
	public class CherryLog : BaseLog
    {
        [Constructable]
        public CherryLog() : this(1)
        {
        }
        [Constructable]
        public CherryLog(int amount) : base(CraftResource.CherryTree, amount)
        {
        }
        public CherryLog(Serial serial) : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Weight = LogConstants.WEIGHT_MEDIUM;
		}

		public override BaseWoodBoard GetLog()
		{
			return new CherryBoard();
		}
	}

	/// <summary>
	/// Rosewood tree log - converts to RosewoodBoard
	/// </summary>
	public class RosewoodLog : BaseLog
    {
        [Constructable]
        public RosewoodLog() : this(1)
        {
        }
        [Constructable]
        public RosewoodLog(int amount) : base(CraftResource.RosewoodTree, amount)
        {
        }
        public RosewoodLog(Serial serial) : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Weight = LogConstants.WEIGHT_HEAVY;
		}

		public override BaseWoodBoard GetLog()
		{
			return new RosewoodBoard();
		}
	}

	/// <summary>
	/// Hickory tree log - converts to HickoryBoard
	/// </summary>
	public class HickoryLog : BaseLog
    {
        [Constructable]
        public HickoryLog() : this(1)
        {
        }
        [Constructable]
        public HickoryLog(int amount) : base(CraftResource.HickoryTree, amount)
        {
        }
        public HickoryLog(Serial serial) : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Weight = LogConstants.WEIGHT_MEDIUM;
		}

		public override BaseWoodBoard GetLog()
		{
			return new HickoryBoard();
		}
	}

	#endregion

	/*
	 * FUTURE LOG CLASSES
	 * 
	 * The following log classes are preserved for future content releases.
	 * See LogFutureResources.cs for complete class definitions and implementation notes.
	 * 
	 * When these resources are enabled:
	 * 1. Uncomment the CraftResource enum values in ResourceInfo.cs
	 * 2. Add difficulty mappings to LogConstants.RESOURCE_DIFFICULTY
	 * 3. Uncomment the appropriate log class from LogFutureResources.cs
	 * 4. Create corresponding Board classes
	 * 
	 * Future log types:
	 * - PetrifiedLog (CraftResource.PetrifiedTree)
	 * - MahoganyLog (CraftResource.MahoganyTree)
	 * - OakLog (CraftResource.OakTree)
	 * - PineLog (CraftResource.PineTree)
	 * - WalnutLog (CraftResource.WalnutTree)
	 * - DriftwoodLog (CraftResource.DriftwoodTree)
	 * - GhostLog (CraftResource.GhostTree)
	 */

	/*public class PetrifiedLog : BaseLog
	{
		[Constructable]
		public PetrifiedLog() : this(1)
		{
		}
		[Constructable]
		public PetrifiedLog(int amount) : base(CraftResource.PetrifiedTree, amount)
		{
		}
		public PetrifiedLog(Serial serial) : base(serial)
		{
		}
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
		public override BaseWoodBoard GetLog()
		{
			return new PetrifiedBoard();
		}
	}*/

	/*public class MahoganyLog : BaseLog
	{
		[Constructable]
		public MahoganyLog() : this(1)
		{
		}
		[Constructable]
		public MahoganyLog(int amount) : base(CraftResource.MahoganyTree, amount)
		{
		}
		public MahoganyLog(Serial serial) : base(serial)
		{
		}
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
		public override BaseWoodBoard GetLog()
		{
			return new MahoganyBoard();
		}
	}*/

	/*public class OakLog : BaseLog
	{
		[Constructable]
		public OakLog() : this(1)
		{
		}
		[Constructable]
		public OakLog(int amount) : base(CraftResource.OakTree, amount)
		{
		}
		public OakLog(Serial serial) : base(serial)
		{
		}
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
		public override BaseWoodBoard GetLog()
		{
			return new OakBoard();
		}
	}*/

	/*public class PineLog : BaseLog
	{
		[Constructable]
		public PineLog() : this(1)
		{
		}
		[Constructable]
		public PineLog(int amount) : base(CraftResource.PineTree, amount)
		{
		}
		public PineLog(Serial serial) : base(serial)
		{
		}
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
		public override BaseWoodBoard GetLog()
		{
			return new PineBoard();
		}
	}*/

	/*public class WalnutLog : BaseLog
	{
		[Constructable]
		public WalnutLog() : this(1)
		{
		}
		[Constructable]
		public WalnutLog(int amount) : base(CraftResource.WalnutTree, amount)
		{
		}
		public WalnutLog(Serial serial) : base(serial)
		{
		}
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
		public override BaseWoodBoard GetLog()
		{
			return new WalnutBoard();
		}
	}*/

	/*public class DriftwoodLog : BaseLog
	{
		[Constructable]
		public DriftwoodLog() : this(1)
		{
		}
		[Constructable]
		public DriftwoodLog(int amount) : base(CraftResource.DriftwoodTree, amount)
		{
		}
		public DriftwoodLog(Serial serial) : base(serial)
		{
		}
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
		public override BaseWoodBoard GetLog()
		{
			return new DriftwoodBoard();
		}
	}*/

	/*public class GhostLog : BaseLog
	{
		[Constructable]
		public GhostLog() : this(1)
		{
		}
		[Constructable]
		public GhostLog(int amount) : base(CraftResource.GhostTree, amount)
		{
		}
		public GhostLog(Serial serial) : base(serial)
		{
		}
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
		public override BaseWoodBoard GetLog()
		{
			return new GhostBoard();
		}
	}*/

}

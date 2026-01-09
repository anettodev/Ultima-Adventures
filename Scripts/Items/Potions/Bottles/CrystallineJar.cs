using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
	/// <summary>
	/// Crystalline jar that can collect and throw monster splatter substances.
	/// Can scoop up MonsterSplatter and HolyWater, then throw contents at a location.
	/// Empty jars can be refilled multiple times.
	/// </summary>
	public class CrystallineJar : Item
	{
		#region Constructors

		/// <summary>
		/// Creates an empty crystalline jar.
		/// </summary>
		[Constructable]
		public CrystallineJar() : base( CrystallineJarConstants.ITEM_ID_CRYSTALLINE_JAR )
		{
			Weight = CrystallineJarConstants.WEIGHT_EMPTY;
			Hue = CrystallineJarConstants.HUE_EMPTY;
			Name = CrystallineJarStringConstants.NAME_EMPTY_FLASK;
		}

		/// <summary>
		/// Deserialization constructor.
		/// </summary>
		/// <param name="serial">Serial reader</param>
		public CrystallineJar( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Adds custom properties to the item property list.
		/// Shows "Holds Odd Substances" label for empty jars.
		/// </summary>
		/// <param name="list">Property list to add to</param>
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			if ( IsEmpty() )
			{
				list.Add( CrystallineJarConstants.CLILOC_HOLDS_SUBSTANCES, CrystallineJarStringConstants.LABEL_HOLDS_SUBSTANCES );
			}
		}

		/// <summary>
		/// Handles double-click interaction with the jar.
		/// Empty jars allow scooping substances, filled jars allow throwing.
		/// </summary>
		/// <param name="from">Mobile double-clicking the jar</param>
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
				return;
			}

			if ( IsEmpty() )
			{
				// Empty jar - allow scooping
				from.SendMessage( CrystallineJarStringConstants.MSG_SCOOP_PROMPT );
				from.Target = new ScoopTarget( this );
			}
			else
			{
				// Filled jar - allow throwing
				if ( !from.Region.AllowHarmful( from, from ) )
				{
					from.SendMessage( CrystallineJarStringConstants.MSG_BAD_IDEA );
					return;
				}

				if ( Server.Items.MonsterSplatter.TooMuchSplatter( from ) )
				{
					from.SendMessage( CrystallineJarStringConstants.MSG_TOO_MUCH_LIQUID );
					return;
				}

				from.SendMessage( CrystallineJarStringConstants.MSG_DUMP_PROMPT );
				
				ThrowTarget targ = from.Target as ThrowTarget;
				if ( targ != null && targ.Potion == this )
					return;

				from.RevealingAction();
				from.Target = new ThrowTarget( this );
			}
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Validates if mobile can perform jar actions (not paralyzed, frozen, casting, etc.).
		/// </summary>
		/// <param name="from">Mobile attempting action</param>
		/// <returns>True if mobile can act, false otherwise</returns>
		private static bool CanPerformAction( Mobile from )
		{
			return !from.Paralyzed && 
			       !from.Blessed && 
			       !from.Frozen && 
			       (from.Spell == null || !from.Spell.IsCasting);
		}

		/// <summary>
		/// Validates if target is within specified range.
		/// </summary>
		/// <param name="from">Mobile checking distance</param>
		/// <param name="target">Target location</param>
		/// <param name="maxDistance">Maximum allowed distance (squared)</param>
		/// <returns>True if in range, false otherwise</returns>
		private static bool IsInRange( Mobile from, Point3D target, int maxDistance )
		{
			return from.GetDistanceToSqrt( target ) <= maxDistance;
		}

		/// <summary>
		/// Creates flask name with collected substance.
		/// </summary>
		/// <param name="substanceName">Name of substance collected</param>
		/// <returns>Full flask name with substance</returns>
		private static string GetFlaskNameWithSubstance( string substanceName )
		{
			return CrystallineJarStringConstants.NAME_PREFIX_FLASK + substanceName;
		}

		/// <summary>
		/// Extracts substance name from flask name.
		/// </summary>
		/// <param name="flaskName">Full flask name</param>
		/// <returns>Substance name without flask prefix</returns>
		private static string ExtractSubstanceName( string flaskName )
		{
			if ( flaskName.Contains( CrystallineJarStringConstants.NAME_PREFIX_FLASK ) )
			{
				return flaskName.Replace( CrystallineJarStringConstants.NAME_PREFIX_FLASK, "" );
			}
			return flaskName;
		}

		/// <summary>
		/// Resets jar to empty crystalline flask state.
		/// </summary>
		private void ResetToEmpty()
		{
			Name = CrystallineJarStringConstants.NAME_EMPTY_FLASK;
			Hue = CrystallineJarConstants.HUE_EMPTY;
			Weight = CrystallineJarConstants.WEIGHT_EMPTY;
		}

		/// <summary>
		/// Checks if jar is currently empty.
		/// </summary>
		/// <returns>True if jar is empty, false otherwise</returns>
		private bool IsEmpty()
		{
			return Name == CrystallineJarStringConstants.NAME_EMPTY_FLASK;
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the crystalline jar to the world save.
		/// </summary>
		/// <param name="writer">Generic writer</param>
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( CrystallineJarConstants.SERIALIZATION_VERSION );
		}

		/// <summary>
		/// Deserializes the crystalline jar from the world save.
		/// Fixes ItemID and resets name if empty (Hue == 0).
		/// </summary>
		/// <param name="reader">Generic reader</param>
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			if ( Hue == CrystallineJarConstants.HUE_EMPTY )
			{
				Name = CrystallineJarStringConstants.NAME_EMPTY_FLASK;
			}
			
			ItemID = CrystallineJarConstants.ITEM_ID_CRYSTALLINE_JAR;
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Target handler for scooping substances into the jar.
		/// Allows collecting MonsterSplatter and HolyWater.
		/// </summary>
		private class ScoopTarget : Target
		{
			private CrystallineJar m_Jar;

			/// <summary>
			/// Creates a new scoop target handler.
			/// </summary>
			/// <param name="jar">Jar to scoop into</param>
			public ScoopTarget( CrystallineJar jar ) : base( CrystallineJarConstants.SCOOP_RANGE, false, TargetFlags.None )
			{
				m_Jar = jar;
			}

			/// <summary>
			/// Handles targeting for scooping substances.
			/// Validates range, action ability, and substance type.
			/// </summary>
			/// <param name="from">Mobile performing the scoop</param>
			/// <param name="targeted">Targeted object</param>
			protected override void OnTarget( Mobile from, object targeted )
			{
				Item targetItem = targeted as Item;

				if ( targetItem == null )
				{
					from.SendMessage( CrystallineJarStringConstants.MSG_WRONG_SUBSTANCE );
					return;
				}

				// Validate range
				if ( !CrystallineJar.IsInRange( from, targetItem.Location, CrystallineJarConstants.MAX_SCOOP_DISTANCE ) )
				{
					from.SendMessage( CrystallineJarStringConstants.MSG_TOO_FAR );
					return;
				}

				// Validate action ability
				if ( !CrystallineJar.CanPerformAction( from ) )
				{
					from.SendMessage( CrystallineJarStringConstants.MSG_CANNOT_DO_YET );
					return;
				}

				// Handle different substance types
				if ( targetItem is MonsterSplatter )
				{
					ScoopMonsterSplatter( from, (MonsterSplatter)targetItem );
				}
				else if ( targetItem is HolyWater )
				{
					ScoopHolyWater( from );
				}
				else
				{
					from.SendMessage( CrystallineJarStringConstants.MSG_WRONG_SUBSTANCE );
				}
			}

			/// <summary>
			/// Handles scooping monster splatter into jar.
			/// Rejects player-created splatter (too diluted).
			/// </summary>
			/// <param name="from">Mobile performing the scoop</param>
			/// <param name="splatter">Monster splatter to scoop</param>
			private void ScoopMonsterSplatter( Mobile from, MonsterSplatter splatter )
			{
				if ( splatter.owner is PlayerMobile )
				{
					from.SendMessage( CrystallineJarStringConstants.MSG_TOO_DILUTED );
					return;
				}

				from.RevealingAction();
				from.PlaySound( CrystallineJarConstants.SOUND_ID_SCOOP );

				m_Jar.Name = CrystallineJar.GetFlaskNameWithSubstance( splatter.Name );
				m_Jar.Hue = splatter.Hue;
				m_Jar.Weight = splatter.Weight;
			}

			/// <summary>
			/// Handles scooping holy water into jar.
			/// Sets fixed properties for holy water flask.
			/// </summary>
			/// <param name="from">Mobile performing the scoop</param>
			private void ScoopHolyWater( Mobile from )
			{
				from.RevealingAction();
				from.PlaySound( CrystallineJarConstants.SOUND_ID_SCOOP );

				m_Jar.Name = CrystallineJarStringConstants.NAME_HOLY_WATER_FLASK;
				m_Jar.Hue = CrystallineJarConstants.HUE_HOLY_WATER;
				m_Jar.Weight = CrystallineJarConstants.WEIGHT_HOLY_WATER;
			}
		}

		/// <summary>
		/// Target handler for throwing jar contents at a location.
		/// Creates MonsterSplatter at target location and resets jar to empty.
		/// </summary>
		private class ThrowTarget : Target
		{
			private CrystallineJar m_Potion;

			/// <summary>
			/// Gets the jar being thrown.
			/// </summary>
			public CrystallineJar Potion
			{
				get { return m_Potion; }
			}

			/// <summary>
			/// Creates a new throw target handler.
			/// </summary>
			/// <param name="potion">Jar to throw</param>
			public ThrowTarget( CrystallineJar potion ) : base( CrystallineJarConstants.THROW_RANGE, true, TargetFlags.None )
			{
				m_Potion = potion;
			}

			/// <summary>
			/// Handles targeting for throwing jar contents.
			/// Validates range, visibility, and action ability.
			/// Creates MonsterSplatter at target location.
			/// </summary>
			/// <param name="from">Mobile throwing the jar</param>
			/// <param name="targeted">Targeted location</param>
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Potion.Deleted || m_Potion.Map == Map.Internal )
					return;

				IPoint3D p = targeted as IPoint3D;
				Point3D targetLocation = new Point3D( p );

				if ( p == null || from.Map == null )
					return;

				SpellHelper.GetSurfaceTop( ref p );

				int throwResult = CrystallineJarConstants.THROW_SUCCESS;

				// Validate throw conditions
				if ( !ValidateThrow( from, targetLocation, ref throwResult ) )
				{
					return;
				}

				// Extract substance name and determine glow
				string substanceName = CrystallineJar.ExtractSubstanceName( m_Potion.Name );
				int glow = (m_Potion.Weight == CrystallineJarConstants.WEIGHT_GLOW_THRESHOLD) ? 1 : 0;

				// Create splatter at target location
				MonsterSplatter.AddSplatter( p.X, p.Y, p.Z, from.Map, targetLocation, from, substanceName, m_Potion.Hue, glow );

				// Reset jar to empty state
				if ( throwResult > CrystallineJarConstants.THROW_FAIL )
				{
					from.RevealingAction();
					m_Potion.ResetToEmpty();
				}
			}

			/// <summary>
			/// Validates all conditions for throwing jar contents.
			/// Checks range, visibility, and action ability.
			/// </summary>
			/// <param name="from">Mobile throwing the jar</param>
			/// <param name="target">Target location</param>
			/// <param name="throwResult">Result code (modified by reference)</param>
			/// <returns>True if throw is valid, false otherwise</returns>
			private bool ValidateThrow( Mobile from, Point3D target, ref int throwResult )
			{
				// Check range
				if ( !CrystallineJar.IsInRange( from, target, CrystallineJarConstants.MAX_THROW_DISTANCE ) )
				{
					throwResult = CrystallineJarConstants.THROW_FAIL;
					from.SendMessage( CrystallineJarStringConstants.MSG_TOO_FAR );
					return false;
				}

				// Check visibility
				if ( !from.CanSee( target ) )
				{
					throwResult = CrystallineJarConstants.THROW_FAIL;
					from.SendLocalizedMessage( 500237 ); // Target can not be seen.
					return false;
				}

				// Check action ability
				if ( !CrystallineJar.CanPerformAction( from ) )
				{
					throwResult = CrystallineJarConstants.THROW_FAIL;
					from.SendMessage( CrystallineJarStringConstants.MSG_CANNOT_DO_YET );
					return false;
				}

				return true;
			}
		}

		#endregion
	}
}

using System;
using Server.Targeting;
using Server.Network;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Regions;
using System.Collections.Generic;

namespace Server.Spells.Third
{
	/// <summary>
	/// Wall of Stone - 3rd Circle Field Spell
	/// Creates a temporary wall of stone that blocks movement
	/// </summary>
	public class WallOfStoneSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Wall of Stone", "In Sanct Ylem",
				227,
				9011,
				false,
				Reagent.Bloodmoss,
				Reagent.Garlic
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		#region Constants
		// Wall Geometry
		private const int WALL_LENGTH = 5; // -2 to +2 = 5 segments
		private const int WALL_MIN_OFFSET = -2;
		private const int WALL_MAX_OFFSET = 2;
		private const int ORIENTATION_MULTIPLIER = 44;

		// Z-Axis Detection
		private const int Z_DIFFERENCE_MAX = 18;
		private const int Z_DIFFERENCE_MIN = -10;

		// Duration Constants
		private const double BASE_DURATION_SECONDS = 5.0;
		private const double DURATION_BONUS_SECONDS = 10.0; // Version 0 fallback
		private const double INSCRIBE_DURATION_DIVISOR = 4.0;

		// Effect Constants
		private const int SOUND_ID = 0x1F6;
		private const int EFFECT_ID = 0x376A;
		private const int EFFECT_SPEED = 9;
		private const int EFFECT_RENDER = 10;
		private const int EFFECT_DURATION = 5025;
		private const int DEFAULT_HUE = 0;

		// Item Constants
		// Orientation-based wall IDs:
		// 87 (0x57) = East-West orientation wall
		// 88 (0x58) = North-South orientation wall
		private const int WALL_ITEM_ID_EAST_WEST = 0x58;  // 87 decimal
		private const int WALL_ITEM_ID_NORTH_SOUTH = 0x57; // 88 decimal

		// Target Constants
		private const int TARGET_RANGE_ML = 10;
		private const int TARGET_RANGE_LEGACY = 12;
		#endregion

		public WallOfStoneSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target(IPoint3D targetPoint)
		{
			if (!Caster.CanSee(targetPoint))
			{
				Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TARGET_NOT_VISIBLE);
			}
			else if (SpellHelper.CheckTown(targetPoint, Caster) && CheckSequence())
			{
				SpellHelper.Turn(Caster, targetPoint);
				SpellHelper.GetSurfaceTop(ref targetPoint);

				bool eastToWest = DetermineWallOrientation(targetPoint);
				PlaySound(targetPoint);

				CreateWallSegments(targetPoint, eastToWest);
			}

			FinishSequence();
		}

		/// <summary>
		/// Determines the orientation of the wall based on caster and target positions
		/// </summary>
		/// <param name="targetPoint">The target point for the wall</param>
		/// <returns>True if wall should be east-to-west, false for north-to-south</returns>
		/// 
		/// TODO: FUTURE REFACTOR - WALL ORIENTATION ISSUE
		/// ================================================
		/// Current Problem:
		/// - The orientation calculation is not working correctly
		/// - Wrong wall ItemID is being rendered (87 vs 88 are swapped or logic is inverted)
		/// - Walls appear in incorrect orientation relative to casting direction
		/// 
		/// Expected Behavior:
		/// - When casting East or West: Wall should be East-West (horizontal) - ItemID 87 (0x57)
		/// - When casting North or South: Wall should be North-South (vertical) - ItemID 88 (0x58)
		/// - Wall should be aligned parallel to the casting direction
		/// - The wall orientation should match the direction the caster is facing after SpellHelper.Turn()
		/// 
		/// Possible Solutions to Investigate:
		/// 1. Use caster's Direction property after SpellHelper.Turn() instead of position-based calculation
		/// 2. Invert the return values (swap true/false logic)
		/// 3. Swap the ItemID constants (currently swapped as workaround: 0x58 for EW, 0x57 for NS)
		/// 4. Check if wall should be parallel vs perpendicular to casting direction
		/// 5. Compare with working field spells (EnergyField, FireField) for correct orientation logic
		/// 6. Test with different casting angles (cardinal vs diagonal directions)
		/// 
		/// Current Workaround:
		/// - ItemID constants are swapped: WALL_ITEM_ID_EAST_WEST = 0x58, WALL_ITEM_ID_NORTH_SOUTH = 0x57
		/// - This is a temporary fix but the underlying orientation logic still needs correction
		/// - Correct mapping should be:
		///   * WALL_ITEM_ID_EAST_WEST = 0x57 (87) for East/West casting
		///   * WALL_ITEM_ID_NORTH_SOUTH = 0x58 (88) for North/South casting
		private bool DetermineWallOrientation(IPoint3D targetPoint)
		{
			int dx = Caster.Location.X - targetPoint.X;
			int dy = Caster.Location.Y - targetPoint.Y;
			int rx = (dx - dy) * ORIENTATION_MULTIPLIER;
			int ry = (dx + dy) * ORIENTATION_MULTIPLIER;

			if (rx >= 0 && ry >= 0)
			{
				return false; // North-to-South
			}
			else if (rx >= 0)
			{
				return true; // East-to-West
			}
			else if (ry >= 0)
			{
				return true; // East-to-West
			}
			else
			{
				return false; // North-to-South
			}
		}

		/// <summary>
		/// Plays the sound effect for wall creation
		/// </summary>
		/// <param name="targetPoint">Location to play sound</param>
		private void PlaySound(IPoint3D targetPoint)
		{
			Effects.PlaySound(targetPoint, Caster.Map, SOUND_ID);
		}

		/// <summary>
		/// Creates all wall segments for the field spell
		/// </summary>
		/// <param name="targetPoint">The center point of the wall</param>
		/// <param name="eastToWest">Wall orientation</param>
		private void CreateWallSegments(IPoint3D targetPoint, bool eastToWest)
		{
			for (int i = WALL_MIN_OFFSET; i <= WALL_MAX_OFFSET; ++i)
			{
				Point3D segmentLocation = new Point3D(
					eastToWest ? targetPoint.X + i : targetPoint.X,
					eastToWest ? targetPoint.Y : targetPoint.Y + i,
					targetPoint.Z
				);

				if (CanPlaceWallSegment(segmentLocation))
				{
					RemoveExistingWalls(segmentLocation);
					CreateWallSegment(segmentLocation, eastToWest);
				}
			}
		}

		/// <summary>
		/// Checks if a wall segment can be placed at the given location
		/// </summary>
		/// <param name="location">Location to check</param>
		/// <returns>True if segment can be placed</returns>
		private bool CanPlaceWallSegment(Point3D location)
		{
			IPooledEnumerable mobiles = Caster.Map.GetMobilesInRange(location, 0);

			foreach (Mobile mobile in mobiles)
			{
				if (mobile.AccessLevel != AccessLevel.Player || !mobile.Alive)
					continue;

				int zDifference = mobile.Location.Z - location.Z;
				if (zDifference < Z_DIFFERENCE_MAX && zDifference > Z_DIFFERENCE_MIN)
				{
					// Mobile is in the way, cannot place wall segment
					mobiles.Free();
					return false;
				}
			}

			mobiles.Free();
			return true;
		}

		/// <summary>
		/// Removes any existing wall items at the location
		/// </summary>
		/// <param name="location">Location to clear</param>
		private void RemoveExistingWalls(Point3D location)
		{
			List<Item> existingWalls = new List<Item>();

			foreach (Item item in Caster.Map.GetItemsInRange(location, 0))
			{
				if (item is InternalItem)
					existingWalls.Add(item);
			}

			for (int j = existingWalls.Count - 1; j >= 0; --j)
			{
				existingWalls[j].Delete();
			}
		}

		/// <summary>
		/// Creates a single wall segment at the specified location
		/// </summary>
		/// <param name="location">Location for the wall segment</param>
		/// <param name="eastToWest">Wall orientation - true for East-West, false for North-South</param>
		private void CreateWallSegment(Point3D location, bool eastToWest)
		{
			// Select ItemID based on orientation
			int itemID = eastToWest ? WALL_ITEM_ID_EAST_WEST : WALL_ITEM_ID_NORTH_SOUTH;
			Item wall = new InternalItem(location, Caster.Map, Caster, itemID);
			int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE);

			Effects.SendLocationParticles(wall, EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, hue, 0, EFFECT_DURATION, 0);
		}

		/// <summary>
		/// Calculates the duration of the wall based on caster's inscribe skill
		/// </summary>
		/// <param name="caster">The caster of the spell</param>
		/// <returns>Duration of the wall</returns>
		private static TimeSpan CalculateWallDuration(Mobile caster)
		{
			if (caster is PlayerMobile)
			{
				int inscribeBonus = (int)(caster.Skills[SkillName.Inscribe].Value / INSCRIBE_DURATION_DIVISOR);
				return TimeSpan.FromSeconds(BASE_DURATION_SECONDS + inscribeBonus);
			}

			return TimeSpan.FromSeconds(BASE_DURATION_SECONDS);
		}

		[DispellableField]
		public class InternalItem : Item
		{
			private Timer m_Timer;
			private DateTime m_End;
			private Mobile m_Caster;

			public override bool BlocksFit{ get{ return true; } }

			public InternalItem(Point3D loc, Map map, Mobile caster, int itemID) : base(itemID)
			{
				Visible = false;
				Movable = false;

				MoveToWorld(loc, map);

				m_Caster = caster;

				if (caster.InLOS(this))
					Visible = true;
				else
					Delete();

				if (Deleted)
					return;

				TimeSpan duration = CalculateWallDuration(caster);
				m_Timer = new InternalTimer(this, duration);
				m_Timer.Start();

				m_End = DateTime.Now + duration;
			}

			public InternalItem( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 1 ); // version

				writer.WriteDeltaTime( m_End );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();

				switch (version)
				{
					case 1:
					{
						m_End = reader.ReadDeltaTime();

						m_Timer = new InternalTimer(this, m_End - DateTime.UtcNow);
						m_Timer.Start();

						break;
					}
					case 0:
					{
						TimeSpan duration = TimeSpan.FromSeconds(DURATION_BONUS_SECONDS);

						m_Timer = new InternalTimer(this, duration);
						m_Timer.Start();

						m_End = DateTime.UtcNow + duration;

						break;
					}
				}
			}

			public override bool OnMoveOver( Mobile m )
			{
				int noto;

				if ( m is PlayerMobile )
				{
					noto = Notoriety.Compute( m_Caster, m );
					if ( noto == Notoriety.Enemy || noto == Notoriety.Ally )
						return false;
				}
				return base.OnMoveOver( m );
			}

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				if ( m_Timer != null )
					m_Timer.Stop();
			}

			private class InternalTimer : Timer
			{
				private InternalItem m_Item;

				public InternalTimer( InternalItem item, TimeSpan duration ) : base( duration )
				{
					Priority = TimerPriority.OneSecond;
					m_Item = item;
				}

				protected override void OnTick()
				{
					m_Item.Delete();
				}
			}
		}

		public class InternalTarget : Target
		{
			private WallOfStoneSpell m_Owner;

			public InternalTarget(WallOfStoneSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, true, TargetFlags.None)
			{
				m_Owner = owner;
			}

		protected override void OnTarget(Mobile from, object o)
		{
			if (o is IPoint3D)
			{
				m_Owner.Target((IPoint3D)o);
			}
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
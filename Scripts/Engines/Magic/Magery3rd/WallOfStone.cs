using System;
using Server.Targeting;
using Server.Network;
using Server.Misc;
using Server.Items;
using Server.Mobiles;

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
		private const int WALL_MIN_OFFSET = -2;
		private const int WALL_MAX_OFFSET = 2;
		private const int ORIENTATION_MULTIPLIER = 44;

		// Z-Axis Detection
		private const int Z_DIFFERENCE_MAX = 18;
		private const int Z_DIFFERENCE_MIN = -10;

		// Duration Constants
		private const double BASE_DURATION_SECONDS = 5.0;
		private const double DURATION_BONUS_SECONDS = 10.0;
		private const double INSCRIBE_DURATION_DIVISOR = 4.0;

		// Effect Constants
		private const int SOUND_ID = 0x1F6;
		private const int EFFECT_ID = 0x376A;
		private const int EFFECT_SPEED = 9;
		private const int EFFECT_RENDER = 10;
		private const int EFFECT_DURATION = 5025;
		private const int DEFAULT_HUE = 0;

	// Item Constants - Orientation-based wall IDs
	private const int WALL_ITEM_ID_EAST_WEST = 0x58;
	private const int WALL_ITEM_ID_NORTH_SOUTH = 0x57;

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
				FinishSequence();
				return;
			}

			if (!SpellHelper.CheckTown(targetPoint, Caster) || !CheckSequence())
			{
				FinishSequence();
				return;
			}

			if (Caster.Map == null)
			{
				FinishSequence();
				return;
			}

		SpellHelper.Turn(Caster, targetPoint);
		SpellHelper.GetSurfaceTop(ref targetPoint);

		bool eastToWest = DetermineWallOrientation(targetPoint);
		
		Caster.SendMessage("=== WALL OF STONE DEBUG ===");
		Caster.SendMessage("Caster: (" + Caster.X + ", " + Caster.Y + ") Facing: " + (Caster.Direction & Direction.Mask));
		Caster.SendMessage("Target: (" + targetPoint.X + ", " + targetPoint.Y + ")");
		Caster.SendMessage("Self-target: " + (Caster.X == targetPoint.X && Caster.Y == targetPoint.Y));
		Caster.SendMessage("Orientation: " + (eastToWest ? "EAST-WEST (horizontal)" : "NORTH-SOUTH (vertical)"));
		Caster.SendMessage("Will use ItemID: 0x" + (eastToWest ? WALL_ITEM_ID_EAST_WEST : WALL_ITEM_ID_NORTH_SOUTH).ToString("X"));
		
		PlaySound(targetPoint);
		CreateWallSegments(targetPoint, eastToWest);

		FinishSequence();
		}

	/// <summary>
	/// Determines the orientation of the wall based on caster and target positions
	/// When targeting self, uses caster's facing direction to create perpendicular wall
	/// </summary>
	/// <param name="targetPoint">The target point for the wall</param>
	/// <returns>True if wall should be east-to-west, false for north-to-south</returns>
	private bool DetermineWallOrientation(IPoint3D targetPoint)
	{
		int dx = Caster.Location.X - targetPoint.X;
		int dy = Caster.Location.Y - targetPoint.Y;
		
		// If targeting self (dx=0, dy=0), use caster's facing direction
		if (dx == 0 && dy == 0)
		{
			Direction facing = Caster.Direction & Direction.Mask;
			
			// If facing North/South (vertical), create East-West wall (horizontal)
			if (facing == Direction.North || facing == Direction.South)
				return true;  // East-West
			
			// If facing East/West (horizontal), create North-South wall (vertical)
			if (facing == Direction.East || facing == Direction.West)
				return false; // North-South
			
			// Diagonal directions: use geometric calculation
			// This will fall through to the original logic below
		}
		
		// Original geometric calculation for non-self targeting
		int rx = (dx - dy) * ORIENTATION_MULTIPLIER;
		int ry = (dx + dy) * ORIENTATION_MULTIPLIER;

		if (rx >= 0 && ry >= 0)
			return false;

		if (rx >= 0 || ry >= 0)
			return true;

		return false;
	}

		/// <summary>
		/// Plays the sound effect for wall creation
		/// </summary>
		/// <param name="targetPoint">Location to play sound</param>
		private void PlaySound(IPoint3D targetPoint)
		{
			if (Caster.Map != null)
				Effects.PlaySound(targetPoint, Caster.Map, SOUND_ID);
		}

	/// <summary>
	/// Creates all wall segments for the field spell
	/// </summary>
	/// <param name="targetPoint">The center point of the wall</param>
	/// <param name="eastToWest">Wall orientation</param>
	private void CreateWallSegments(IPoint3D targetPoint, bool eastToWest)
	{
		Caster.SendMessage("Creating " + (eastToWest ? "EAST-WEST" : "NORTH-SOUTH") + " segments:");
		if (eastToWest)
			Caster.SendMessage("  (varying X from " + (targetPoint.X - 2) + " to " + (targetPoint.X + 2) + ", fixed Y=" + targetPoint.Y + ")");
		else
			Caster.SendMessage("  (fixed X=" + targetPoint.X + ", varying Y from " + (targetPoint.Y - 2) + " to " + (targetPoint.Y + 2) + ")");
		
		for (int i = WALL_MIN_OFFSET; i <= WALL_MAX_OFFSET; ++i)
		{
			Point3D segmentLocation = new Point3D(
				eastToWest ? targetPoint.X + i : targetPoint.X,
				eastToWest ? targetPoint.Y : targetPoint.Y + i,
				targetPoint.Z
			);

			// Adjust Z-level without mobile blocking (AdjustField checks mobiles, which we handle separately)
			if (AdjustFieldWithoutMobileCheck(ref segmentLocation) && CanPlaceWallSegment(segmentLocation))
			{
				RemoveExistingWalls(segmentLocation);
				CreateWallSegment(segmentLocation, eastToWest);
				Caster.SendMessage("  [" + i + "] Created at (" + segmentLocation.X + ", " + segmentLocation.Y + ")");
			}
			else
			{
				Caster.SendMessage("  [" + i + "] SKIPPED at (" + segmentLocation.X + ", " + segmentLocation.Y + ")");
			}
		}
	}

	/// <summary>
	/// Adjusts the field location's Z-coordinate to fit the terrain
	/// Does NOT check for mobiles (we handle that separately in CanPlaceWallSegment)
	/// </summary>
	/// <param name="p">The point to adjust</param>
	/// <returns>True if a valid location was found</returns>
	private bool AdjustFieldWithoutMobileCheck(ref Point3D p)
	{
		if (Caster.Map == null)
			return false;

		// Try to find a valid Z-level within 10 tiles below
		for (int offset = 0; offset < 10; ++offset)
		{
			Point3D loc = new Point3D(p.X, p.Y, p.Z - offset);

			// Check if location can fit without mobile blocking (false = don't check mobiles)
			if (Caster.Map.CanFit(loc, 12, true, false))
			{
				p = loc;
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Checks if a wall segment can be placed at the given location
	/// Ignores all mobiles that belong to the caster (self, pets, summons)
	/// </summary>
	/// <param name="location">Location to check</param>
	/// <returns>True if segment can be placed</returns>
	private bool CanPlaceWallSegment(Point3D location)
	{
		if (Caster.Map == null)
			return false;

		IPooledEnumerable mobiles = Caster.Map.GetMobilesInRange(location, 0);

		try
		{
			foreach (Mobile mobile in mobiles)
			{
				// Always skip the caster
				if (mobile == Caster)
					continue;

				// Skip caster's pets and summons
				BaseCreature creature = mobile as BaseCreature;
				if (creature != null && creature.Controlled && creature.ControlMaster == Caster)
					continue;

				// Skip non-player mobiles (NPCs, animals, etc.) - only check for other players
				if (mobile.AccessLevel != AccessLevel.Player || !mobile.Alive)
					continue;

				// Only block if it's another player (not the caster)
				int zDifference = mobile.Location.Z - location.Z;
				if (zDifference < Z_DIFFERENCE_MAX && zDifference > Z_DIFFERENCE_MIN)
					return false;
			}
		}
		finally
		{
			mobiles.Free();
		}

		return true;
	}

		/// <summary>
		/// Removes any existing wall items at the location
		/// </summary>
		/// <param name="location">Location to clear</param>
		private void RemoveExistingWalls(Point3D location)
		{
			if (Caster.Map == null)
				return;

			IPooledEnumerable items = Caster.Map.GetItemsInRange(location, 0);

			try
			{
				foreach (Item item in items)
				{
					if (item is InternalItem)
						item.Delete();
				}
			}
			finally
			{
				items.Free();
			}
		}

		/// <summary>
		/// Creates a single wall segment at the specified location
		/// </summary>
		/// <param name="location">Location for the wall segment</param>
		/// <param name="eastToWest">Wall orientation - true for East-West, false for North-South</param>
	private void CreateWallSegment(Point3D location, bool eastToWest)
	{
		if (Caster.Map == null)
			return;

		int itemID = eastToWest ? WALL_ITEM_ID_EAST_WEST : WALL_ITEM_ID_NORTH_SOUTH;
		
		// TEST: Force alternate ItemID to see which graphic it produces
		// Comment out ONE of these lines to test:
		// itemID = 0x57;  // Force horizontal graphic?
		// itemID = 0x58;  // Force vertical graphic?
		
		Caster.SendMessage("    -> ItemID 0x" + itemID.ToString("X") + " (" + (eastToWest ? "0x58 EW" : "0x57 NS") + ")");
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
			Visible = true;  // Always visible - wall segments should appear regardless of LOS
			Movable = false;

			MoveToWorld(loc, map);

			m_Caster = caster;

				TimeSpan duration = CalculateWallDuration(caster);
				m_Timer = new InternalTimer(this, duration);
				m_Timer.Start();

				m_End = DateTime.UtcNow + duration;
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
					m_Owner.Target((IPoint3D)o);
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
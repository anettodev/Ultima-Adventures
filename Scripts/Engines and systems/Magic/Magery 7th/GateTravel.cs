using System;
using Server.Network;
using Server.Multis;
using Server.Items;
using Server.Targeting;
using Server.Misc;
using Server.Regions;
using Server.Mobiles;
using Server.Spells;
using Server.Gumps;

namespace Server.Spells.Seventh
{
	/// <summary>
	/// Gate Travel - 7th Circle Magery Spell
	/// Creates a two-way portal between current location and target location
	/// </summary>
	public class GateTravelSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Gate Travel", "Vas Rel Por",
				263,
				9032,
				Reagent.BlackPearl,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Seventh; } }

		#region Constants

		/// <summary>Gate duration in seconds</summary>
		private const double GATE_DURATION_SECONDS = 30.0;

		/// <summary>Gate existence check range</summary>
		private const int GATE_CHECK_RANGE = 0;

		/// <summary>Sound effect ID for gate creation</summary>
		private const int SOUND_EFFECT = 0x20E;

		/// <summary>Hue for safe (guarded) destinations - Blue (default)</summary>
		private const int HUE_SAFE_DESTINATION = 0;

		/// <summary>Hue for unsafe (unguarded) destinations - Red</summary>
		private const int HUE_UNSAFE_DESTINATION = 0x0021;

		/// <summary>Minimum Tracking skill required for Scrying Gate feature</summary>
		private const double SCRYING_GATE_MIN_TRACKING = 80.0;

		/// <summary>Base chance for Scrying Gate at minimum skill (80.0) - 30%</summary>
		private const double SCRYING_GATE_BASE_CHANCE = 0.30;

		/// <summary>Chance increase per 10 Tracking skill points above minimum - 5%</summary>
		private const double SCRYING_GATE_CHANCE_INCREASE = 0.05;

		/// <summary>Skill points required for each chance increase</summary>
		private const double SCRYING_GATE_SKILL_INTERVAL = 10.0;

		/// <summary>Maximum chance for Scrying Gate - 50%</summary>
		private const double SCRYING_GATE_MAX_CHANCE = 0.50;

		/// <summary>Scrying visual effect ID</summary>
		private const int SCRYING_EFFECT_ID = 0x373A;

		/// <summary>Scrying sound effect ID</summary>
		private const int SCRYING_SOUND_ID = 0x1F9;

		/// <summary>Color for safe destination messages - Blue</summary>
		private const int MSG_COLOR_SAFE = 0x0059;

		/// <summary>Color for unsafe destination messages - Red</summary>
		private const int MSG_COLOR_UNSAFE = 0x0021;

		/// <summary>Color for creature detection messages - Purple</summary>
		private const int MSG_COLOR_PURPLE = 0x10;

		/// <summary>Color for white/default messages</summary>
		private const int MSG_COLOR_WHITE = 0x3B2;

		/// <summary>Minimum Cartography skill required for Origin Gate feature</summary>
		private const double ORIGIN_GATE_MIN_CARTOGRAPHY = 80.0;

		/// <summary>Base chance for Origin Gate at minimum skill (80.0) - 10%</summary>
		private const double ORIGIN_GATE_BASE_CHANCE = 0.10;

		/// <summary>Chance increase per Cartography skill point above minimum - 0.5%</summary>
		private const double ORIGIN_GATE_CHANCE_PER_POINT = 0.005;

		/// <summary>Maximum chance for Origin Gate - 30%</summary>
		private const double ORIGIN_GATE_MAX_CHANCE = 0.30;

		/// <summary>Gate Marker item ID (rune-like appearance)</summary>
		private const int GATE_MARKER_ITEM_ID = 0x1F14;

		/// <summary>Gate Marker hue (light green)</summary>
		private const int GATE_MARKER_HUE = 0x0059;

		/// <summary>Gate Marker duration in hours</summary>
		private const int GATE_MARKER_DURATION_HOURS = 1;

		#endregion

		private RunebookEntry m_Entry;

		public GateTravelSpell( Mobile caster, Item scroll ) : this( caster, scroll, null )
		{
		}

		public GateTravelSpell( Mobile caster, Item scroll, RunebookEntry entry ) : base( caster, scroll, m_Info )
		{
			m_Entry = entry;
		}

		public override void OnCast()
		{
			if ( m_Entry == null )
				Caster.Target = new InternalTarget( this );
			else
				Effect( m_Entry.Location, m_Entry.Map, true );
		}

		public override bool CheckCast(Mobile caster)
		{
			return SpellHelper.CheckTravel( Caster, TravelCheckType.GateFrom );
		}

		private bool GateExistsAt(Map map, Point3D loc )
		{
			bool _gateFound = false;

			IPooledEnumerable eable = map.GetItemsInRange( loc, GATE_CHECK_RANGE );
			foreach ( Item item in eable )
			{
				if ( item is Moongate || item is PublicMoongate )
				{
					_gateFound = true;
					break;
				}
			}
			eable.Free();

			return _gateFound;
		}

		/// <summary>
		/// Calculates the chance for Scrying Gate based on Tracking skill
		/// </summary>
		/// <param name="trackingSkill">Current Tracking skill value</param>
		/// <returns>Chance value between 0.0 and SCRYING_GATE_MAX_CHANCE</returns>
		private double CalculateScryingGateChance( double trackingSkill )
		{
			if ( trackingSkill < SCRYING_GATE_MIN_TRACKING )
				return 0.0;

			// Base chance at minimum skill (80.0) = 30%
			double chance = SCRYING_GATE_BASE_CHANCE;

			// Calculate bonus: +5% per 10 skill points above 80.0
			double skillAboveMinimum = trackingSkill - SCRYING_GATE_MIN_TRACKING;
			double bonusIntervals = Math.Floor( skillAboveMinimum / SCRYING_GATE_SKILL_INTERVAL );
			double bonus = bonusIntervals * SCRYING_GATE_CHANCE_INCREASE;

			// Add bonus to base chance
			chance += bonus;

			// Cap at maximum chance (50%)
			if ( chance > SCRYING_GATE_MAX_CHANCE )
				chance = SCRYING_GATE_MAX_CHANCE;

			return chance;
		}

		/// <summary>
		/// Performs scrying to reveal destination information using Tracking skill
		/// </summary>
		/// <param name="destination">Destination location</param>
		/// <param name="map">Destination map</param>
		private void PerformScryingGate( Point3D destination, Map map )
		{
			if ( Caster == null || Caster.Deleted )
				return;

			double trackingSkill = Caster.Skills[SkillName.Tracking].Value;

			// Check if Tracking skill is high enough
			if ( trackingSkill < SCRYING_GATE_MIN_TRACKING )
				return;

			// Calculate chance based on skill level
			double scryingChance = CalculateScryingGateChance( trackingSkill );

			// Roll for scrying success
			if ( Utility.RandomDouble() >= scryingChance )
				return;

			// Visual and sound effects for scrying
			Effects.SendLocationEffect( Caster.Location, Caster.Map, SCRYING_EFFECT_ID, 30, 10, 0x1F4, 0 );
			Effects.PlaySound( Caster.Location, Caster.Map, SCRYING_SOUND_ID );

			// Get destination information
			string regionName = Worlds.GetRegionName( map, destination );
			bool isSafe = IsSafeDestination( map, destination );
			string safetyStatus = isSafe ? "[Protegido]" : "[Não Protegido]";

			// Count nearby mobiles (enemies and creatures)
			int nearbyMobiles = 0;
			if ( map != null && map != Map.Internal )
			{
				IPooledEnumerable eable = map.GetMobilesInRange( destination, 10 );
				foreach ( Mobile m in eable )
				{
					if ( m != null && !m.Deleted && m.Alive )
					{
						// Count hostile mobiles (not allies)
						if ( m is BaseCreature || ( m is PlayerMobile && Notoriety.Compute( Caster, m ) == Notoriety.Enemy ) )
						{
							nearbyMobiles++;
						}
					}
				}
				eable.Free();
			}

			// Send scrying message to caster
			Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, "Suas habilidades de rastreamento permitem que você veja através do portal..." );
			
			// Combined destination and safety status message with appropriate color
			int destinationColor = isSafe ? MSG_COLOR_SAFE : MSG_COLOR_UNSAFE;
			Caster.SendMessage( destinationColor, String.Format( "Destino: {0} {1}", regionName, safetyStatus ) );
			
			if ( nearbyMobiles > 0 )
			{
				string creatureText = nearbyMobiles == 1 ? "criatura" : "criaturas";
				Caster.SendMessage( MSG_COLOR_PURPLE, String.Format( "Você sente {0} {1} nas proximidades.", nearbyMobiles, creatureText ) );
			}
			else
			{
				Caster.SendMessage( MSG_COLOR_WHITE, "A área parece estar livre de ameaças imediatas." );
			}
		}

		/// <summary>
		/// Calculates the chance for Origin Gate based on Cartography skill
		/// </summary>
		/// <param name="cartographySkill">Current Cartography skill value</param>
		/// <returns>Chance value between 0.0 and ORIGIN_GATE_MAX_CHANCE</returns>
		private double CalculateOriginGateChance( double cartographySkill )
		{
			if ( cartographySkill < ORIGIN_GATE_MIN_CARTOGRAPHY )
				return 0.0;

			// TEMPORARY: 100% chance for validation/testing
			return 1.0;

			// Original calculation (commented for validation):
			// Base chance at minimum skill (80.0) = 10%
			// double chance = ORIGIN_GATE_BASE_CHANCE;
			// 
			// // Calculate bonus: +0.5% per skill point above 80.0
			// double skillAboveMinimum = cartographySkill - ORIGIN_GATE_MIN_CARTOGRAPHY;
			// double bonus = skillAboveMinimum * ORIGIN_GATE_CHANCE_PER_POINT;
			// 
			// // Add bonus to base chance
			// chance += bonus;
			// 
			// // Cap at maximum chance (30%)
			// if ( chance > ORIGIN_GATE_MAX_CHANCE )
			// 	chance = ORIGIN_GATE_MAX_CHANCE;
			// 
			// return chance;
		}

		/// <summary>
		/// Attempts to create an Origin Gate marker if Cartography skill is sufficient
		/// </summary>
		/// <param name="originLocation">Original caster location</param>
		/// <param name="originMap">Original caster map</param>
		private void TryCreateOriginGateMarker( Point3D originLocation, Map originMap )
		{
			if ( Caster == null || Caster.Deleted )
				return;

			double cartographySkill = Caster.Skills[SkillName.Cartography].Value;

			// Check if Cartography skill is high enough
			if ( cartographySkill < ORIGIN_GATE_MIN_CARTOGRAPHY )
			{
				Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, String.Format( "[DEBUG] Cartography skill {0:F1} < {1:F1}", cartographySkill, ORIGIN_GATE_MIN_CARTOGRAPHY ) );
				return;
			}

			// Validate that origin location allows travel/recall (marker must be usable)
			// Note: We check GateTo because the marker allows travel TO the origin location
			if ( !SpellHelper.CheckTravel( Caster, originMap, originLocation, TravelCheckType.GateTo ) )
			{
				Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, "[DEBUG] Origin doesn't allow GateTo" );
				return; // Origin doesn't allow travel - don't create marker
			}

			if ( Worlds.RegionAllowedTeleport( originMap, originLocation, originLocation.X, originLocation.Y ) == false )
			{
				Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, "[DEBUG] Origin doesn't allow teleport" );
				return; // Origin doesn't allow teleport - don't create marker
			}

			if ( Worlds.AllowEscape( Caster, originMap, originLocation, originLocation.X, originLocation.Y ) == false ||
				 Worlds.RegionAllowedRecall( originMap, originLocation, originLocation.X, originLocation.Y ) == false )
			{
				Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, "[DEBUG] Origin doesn't allow recall" );
				return; // Origin doesn't allow recall - don't create marker
			}

			// Note: CanSpawnMobile check removed - it's too restrictive
			// The marker allows travel TO the origin, not spawning there
			// If GateTo, Teleport, and Recall checks pass, the origin is valid for travel

			// Calculate chance based on skill level
			double originGateChance = CalculateOriginGateChance( cartographySkill );

			// Debug: Show calculated chance
			Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, String.Format( "[DEBUG] Origin Gate chance: {0:P2}", originGateChance ) );

			// Roll for Origin Gate success
			double roll = Utility.RandomDouble();
			Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, String.Format( "[DEBUG] Roll: {0:F4} >= {1:F4}? {2}", roll, originGateChance, roll >= originGateChance ) );
			if ( roll >= originGateChance )
			{
				Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, "[DEBUG] Origin Gate roll failed" );
				return;
			}

			// Create the gate marker
			GateMarker marker = new GateMarker( originLocation, originMap, Caster );
			
			// Add marker to caster's backpack instead of placing on ground
			if ( Caster.Backpack == null )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, "Você precisa de uma mochila para receber o marcador." );
				marker.Delete();
				return;
			}
			
			if ( !Caster.AddToBackpack( marker ) )
			{
				// If backpack is full, place on ground as fallback
				marker.MoveToWorld( Caster.Location, Caster.Map );
				Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, "Sua mochila está cheia. O marcador foi colocado no chão." );
			}

			// Send creative message to caster
			Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, "Sua maestria em cartografia permite ancorar a localização de origem do portal, criando um marcador permanente que outros podem usar para retornar..." );
		}

		/// <summary>
		/// Checks if a location is in a safe area protected by guards
		/// </summary>
		/// <param name="map">Map to check</param>
		/// <param name="loc">Location to check</param>
		/// <returns>True if location is in a safe/guarded region, false otherwise</returns>
		private bool IsSafeDestination( Map map, Point3D loc )
		{
			if ( map == null || map == Map.Internal )
				return false;

			Region reg = Region.Find( loc, map );
			if ( reg == null )
				return false;

			// Check if location is in a safe region type (protected by guards)
			// VillageRegion includes towns like Britain, Yew, etc.
			return reg.IsPartOf( typeof( SafeRegion ) ) ||
				   reg.IsPartOf( typeof( ProtectedRegion ) ) ||
				   reg.IsPartOf( typeof( PublicRegion ) ) ||
				   reg.IsPartOf( typeof( GuardedRegion ) ) ||
				   reg.IsPartOf( typeof( BardTownRegion ) ) ||
				   reg.IsPartOf( typeof( VillageRegion ) ) ||
				   reg.IsPartOf( typeof( TownRegion ) );
		}

		public void Effect( Point3D loc, Map map, bool checkMulti )
		{
			if ( !SpellHelper.CheckTravel( Caster, TravelCheckType.GateFrom ) || !SpellHelper.CheckTravel(Caster, map, loc, TravelCheckType.GateTo))
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TRAVEL_BLOCKED );
			}
			else if ( Worlds.AllowEscape( Caster, Caster.Map, Caster.Location, Caster.X, Caster.Y ) == false || 
				Worlds.RegionAllowedRecall(Caster.Map, Caster.Location, Caster.X, Caster.Y) == false)
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TRAVEL_LOCATION_BLOCKED );
			}
			else if ( Worlds.RegionAllowedTeleport( map, loc, loc.X, loc.Y ) == false )
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TRAVEL_DESTINATION_BLOCKED );
			}
			else if ( !map.CanSpawnMobile( loc.X, loc.Y, loc.Z ) || (checkMulti && SpellHelper.CheckMulti(loc, map)))
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TRAVEL_LOCATION_OCCUPIED );
			}
			else if ( Core.SE && ( GateExistsAt( map, loc ) || GateExistsAt( Caster.Map, Caster.Location ) ) ) // SE restricted stacking gates
			{
				Caster.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_GATE_ALREADY_EXISTS );
			}
			else if ( CheckSequence() )
			{
				// Store origin location before gates are created
				Point3D originLocation = Caster.Location;
				Map originMap = Caster.Map;

				Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.INFO_GATE_CREATED );

				// Perform scrying if Tracking skill is high enough (after gate creation message)
				PerformScryingGate( loc, map );

				// Attempt to create Origin Gate marker if Cartography skill is sufficient
				TryCreateOriginGateMarker( originLocation, originMap );

				// Determine gate hue based on destination safety
				// Blue (default) for safe destinations, Red for unsafe
				bool isDestinationSafe = IsSafeDestination( map, loc );
				int destinationHue = isDestinationSafe ? HUE_SAFE_DESTINATION : HUE_UNSAFE_DESTINATION;

				bool isOriginSafe = IsSafeDestination( Caster.Map, Caster.Location );
				int originHue = isOriginSafe ? HUE_SAFE_DESTINATION : HUE_UNSAFE_DESTINATION;

				Effects.PlaySound( Caster.Location, Caster.Map, SOUND_EFFECT );
				InternalItem firstGate = new InternalItem( loc, map );
				// First gate shows destination safety (where it leads to)
				firstGate.Hue = destinationHue;
				if ( AetherGlobe.EvilChamp == Caster || AetherGlobe.GoodChamp == Caster )
					firstGate.ChampGate = true;
				firstGate.MoveToWorld( Caster.Location, Caster.Map );

				if ( Worlds.RegionAllowedTeleport( Caster.Map, Caster.Location, Caster.X, Caster.Y ) == true )
				{
					Effects.PlaySound( loc, map, SOUND_EFFECT );
					InternalItem secondGate = new InternalItem( Caster.Location, Caster.Map );
					// Second gate shows origin safety (where it leads back to)
					secondGate.Hue = originHue;
					if ( AetherGlobe.EvilChamp == Caster || AetherGlobe.GoodChamp == Caster )
						secondGate.ChampGate = true;
					secondGate.MoveToWorld( loc, map );
				}
			}

			FinishSequence();
		}

		[DispellableField]
		private class InternalItem : Moongate
		{
			public override bool ShowFeluccaWarning{ get { return false; } }

			public InternalItem( Point3D target, Map map ) : base( target, map )
			{
				Map = map;

				Dispellable = true;

				InternalTimer t = new InternalTimer( this );
				t.Start();
			}

			/// <summary>
			/// Always show confirmation gump for Gate Travel to prevent accidental travel
			/// </summary>
			public override void BeginConfirmation( Mobile from )
			{
				if ( from.AccessLevel == AccessLevel.Player || !from.Hidden )
					from.Send( new PlaySound( 0x20E, from.Location ) );
				from.CloseGump( typeof( GateTravelConfirmGump ) );
				from.SendGump( new GateTravelConfirmGump( from, this ) );
			}

			public InternalItem( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				Delete();
			}

			private class InternalTimer : Timer
			{
				private Item m_Item;

				public InternalTimer( Item item ) : base( TimeSpan.FromSeconds( GATE_DURATION_SECONDS ) )
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

		private class InternalTarget : Target
		{
			private GateTravelSpell m_Owner;

			public InternalTarget( GateTravelSpell owner ) : base( SpellConstants.GetSpellRange(), false, TargetFlags.None )
			{
				m_Owner = owner;
				owner.Caster.SendMessage( Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.INFO_SELECT_VALID_DESTINATION );
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is RecallRune )
				{
					RecallRune rune = (RecallRune)o;

					if ( rune.Marked )
						m_Owner.Effect( rune.Target, rune.TargetMap, true );
					else
						from.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_RUNE_NOT_MARKED );
				}
				else if ( o is Runebook )
				{
					RunebookEntry e = ((Runebook)o).Default;

					if ( e != null )
						m_Owner.Effect( e.Location, e.Map, true );
					else
						from.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_DESTINATION_NOT_MARKED );
				}
				else if ( o is HouseRaffleDeed && ((HouseRaffleDeed)o).ValidLocation() )
				{
					HouseRaffleDeed deed = (HouseRaffleDeed)o;

					m_Owner.Effect( deed.PlotLocation, deed.PlotFacet, true );
				}
				else
				{
					from.Send( new MessageLocalized( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 501030, from.Name, "" ) ); // I can not gate travel from that object.
				}
			}
			
			protected override void OnNonlocalTarget( Mobile from, object o )
			{
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

		/// <summary>
		/// Simple confirmation gump for Gate Travel to prevent accidental travel
		/// </summary>
		private class GateTravelConfirmGump : Gump
		{
			private Mobile m_From;
			private Moongate m_Gate;

			public GateTravelConfirmGump( Mobile from, Moongate gate ) : base( 100, 0 )
			{
				m_From = from;
				m_Gate = gate;

				Closable = false;

				AddBackground( 0, 0, 330, 200, 0xA28 );

				// Moongate icon (colored blue) next to title
				AddImage( 70, 20, 0x1F70, 0x0044 ); // Magic circle/portal icon in blue
				AddHtmlLocalized( 100, 20, 200, 35, 1062051, false, false ); // Gate Warning

				AddHtmlLocalized( 50, 55, 250, 80, 1062049, true, true ); // Dost thou wish to step into the moongate? Continue to enter the gate, Cancel to stay here

				// OK button (plain text, icon can be colored if needed)
				AddButton( 45, 150, 0xFA5, 0xFA7, 1, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 80, 152, 110, 35, 1011036, false, false ); // OKAY

				// CANCEL button (plain text)
				AddButton( 200, 150, 0xFA5, 0xFA7, 0, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 235, 152, 110, 35, 1011012, false, false ); // CANCEL
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				if ( info.ButtonID == 1 )
				{
					m_Gate.EndConfirmation( m_From );
				}
			}
		}

		/// <summary>
		/// Gate Marker item created by Origin Gate feature
		/// Allows anyone to create a one-way gate back to the origin location
		/// </summary>
		private class GateMarker : Item
		{
			private Point3D m_OriginLocation;
			private Map m_OriginMap;
			private Mobile m_Caster;
			private Timer m_ExpireTimer;

			[Constructable]
			public GateMarker( Point3D originLocation, Map originMap, Mobile caster ) : base( GATE_MARKER_ITEM_ID )
			{
				m_OriginLocation = originLocation;
				m_OriginMap = originMap;
				m_Caster = caster;

				Hue = GATE_MARKER_HUE;
				Name = "Marcador de Portal";
				Weight = 1.0;
				Movable = true;
				LootType = LootType.Regular;

				// Start expiration timer (1 hour)
				m_ExpireTimer = new ExpireTimer( this, TimeSpan.FromHours( GATE_MARKER_DURATION_HOURS ) );
				m_ExpireTimer.Start();
			}

			public GateMarker( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 0 ); // version

				writer.Write( m_OriginLocation );
				writer.Write( m_OriginMap );
				writer.Write( m_Caster );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();

				m_OriginLocation = reader.ReadPoint3D();
				m_OriginMap = reader.ReadMap();
				m_Caster = reader.ReadMobile();

				// Restart expiration timer
				m_ExpireTimer = new ExpireTimer( this, TimeSpan.FromHours( GATE_MARKER_DURATION_HOURS ) );
				m_ExpireTimer.Start();
			}

			public override void OnDelete()
			{
				if ( m_ExpireTimer != null )
				{
					m_ExpireTimer.Stop();
					m_ExpireTimer = null;
				}

				base.OnDelete();
			}

			/// <summary>
			/// Gate markers are movable and cannot be locked down
			/// The Movable property is set to true in constructor to prevent locking down in houses
			/// </summary>
			public override void OnMovement( Mobile m, Point3D oldLocation )
			{
				// Ensure item remains movable (prevents locking down)
				if ( !Movable )
					Movable = true;

				base.OnMovement( m, oldLocation );
			}

			public override void GetProperties( ObjectPropertyList list )
			{
				base.GetProperties( list );

				if ( m_OriginMap != null && m_OriginMap != Map.Internal )
				{
					string regionName = Worlds.GetRegionName( m_OriginMap, m_OriginLocation );
					list.Add( String.Format( "Destino: {0}", regionName ) );
					list.Add( String.Format( "Coordenadas: {0}, {1}, {2}", m_OriginLocation.X, m_OriginLocation.Y, m_OriginLocation.Z ) );
				}
				else
				{
					list.Add( "Destino: Localização desconhecida" );
				}
			}

			public override void OnDoubleClick( Mobile from )
			{
				if ( from == null || from.Deleted )
					return;

				if ( !IsChildOf( from.Backpack ) && !from.InRange( this.GetWorldLocation(), 2 ) )
				{
					from.SendLocalizedMessage( 500446 ); // That is too far away.
					return;
				}

				if ( m_OriginMap == null || m_OriginMap == Map.Internal )
				{
					from.SendMessage( Spell.MSG_COLOR_ERROR, "O marcador perdeu sua conexão com o destino." );
					Delete();
					return;
				}

				// Check if travel is allowed from current location
				if ( !SpellHelper.CheckTravel( from, TravelCheckType.GateFrom ) )
				{
					from.SendMessage( Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TRAVEL_BLOCKED );
					return;
				}

				// Check if travel is allowed to origin location
				if ( !SpellHelper.CheckTravel( from, m_OriginMap, m_OriginLocation, TravelCheckType.GateTo ) )
				{
					from.SendMessage( Spell.MSG_COLOR_ERROR, "O destino do marcador não permite viagem." );
					Delete(); // Delete marker if destination is blocked
					return;
				}

				// Check if origin location allows teleport/recall
				if ( Worlds.RegionAllowedTeleport( m_OriginMap, m_OriginLocation, m_OriginLocation.X, m_OriginLocation.Y ) == false )
				{
					from.SendMessage( Spell.MSG_COLOR_ERROR, "O destino do marcador não permite teletransporte." );
					Delete(); // Delete marker if destination doesn't allow teleport
					return;
				}

				if ( Worlds.AllowEscape( from, m_OriginMap, m_OriginLocation, m_OriginLocation.X, m_OriginLocation.Y ) == false ||
					 Worlds.RegionAllowedRecall( m_OriginMap, m_OriginLocation, m_OriginLocation.X, m_OriginLocation.Y ) == false )
				{
					from.SendMessage( Spell.MSG_COLOR_ERROR, "O destino do marcador não permite recall." );
					Delete(); // Delete marker if destination doesn't allow recall
					return;
				}

				// Check if location is valid for spawning
				if ( !m_OriginMap.CanSpawnMobile( m_OriginLocation.X, m_OriginLocation.Y, m_OriginLocation.Z ) )
				{
					from.SendMessage( Spell.MSG_COLOR_ERROR, "O destino do marcador não é mais acessível." );
					Delete(); // Delete marker if location is invalid
					return;
				}

				// Create one-way gate to origin location
				from.SendMessage( Spell.MSG_COLOR_SYSTEM, "Você ativa o marcador e um portal se abre..." );

				// Determine gate hue based on origin safety
				bool isOriginSafe = IsSafeDestination( m_OriginMap, m_OriginLocation );
				int gateHue = isOriginSafe ? 0 : 0x0021; // HUE_SAFE_DESTINATION : HUE_UNSAFE_DESTINATION

				Effects.PlaySound( from.Location, from.Map, 0x20E ); // SOUND_EFFECT
				
				// Use InternalItem (custom Moongate) instead of standard Moongate to avoid zoom issues
				InternalItem gate = new InternalItem( m_OriginLocation, m_OriginMap );
				gate.Hue = gateHue;
				gate.MoveToWorld( from.Location, from.Map );

				// Delete marker after use (one-time use)
				Delete();
			}

			/// <summary>
			/// Helper method to check if destination is safe (reused from parent class logic)
			/// </summary>
			private bool IsSafeDestination( Map map, Point3D loc )
			{
				if ( map == null || map == Map.Internal )
					return false;

				Region reg = Region.Find( loc, map );
				if ( reg == null )
					return false;

				return reg.IsPartOf( typeof( SafeRegion ) ) ||
					   reg.IsPartOf( typeof( ProtectedRegion ) ) ||
					   reg.IsPartOf( typeof( PublicRegion ) ) ||
					   reg.IsPartOf( typeof( GuardedRegion ) ) ||
					   reg.IsPartOf( typeof( BardTownRegion ) ) ||
					   reg.IsPartOf( typeof( VillageRegion ) ) ||
					   reg.IsPartOf( typeof( TownRegion ) );
			}

			/// <summary>
			/// Timer to expire the marker after 1 hour
			/// </summary>
			private class ExpireTimer : Timer
			{
				private GateMarker m_Marker;

				public ExpireTimer( GateMarker marker, TimeSpan delay ) : base( delay )
				{
					m_Marker = marker;
					Priority = TimerPriority.OneMinute;
				}

				protected override void OnTick()
				{
					if ( m_Marker != null && !m_Marker.Deleted )
					{
						m_Marker.Delete();
					}
				}
			}
		}
	}
}

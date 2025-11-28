using System;
using Server;
using Server.Misc;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class FabledFishingNet : Item
	{
		private bool m_InUse;

		[Constructable]
		public FabledFishingNet() : base( 0x1EA5 )
		{
			Name = "Ancient Fishing Net";
			Weight = 5.0;
			ItemID = Utility.RandomList( 0x1EA5, 0x1EA6 );
			Hue = Utility.RandomList( m_Hues );
		}

		private static int[] m_Hues = new int[]
			{
				0xB8B,
				0xB8C,
				0xB8E,
				0xB8F,
				0xB90,
				0xB91,
				0xB92,
				0xB97,
				0xB98,
				0xB99,
				0xB9A
			};

		public FabledFishingNet( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, FishingStringConstants.FormatProperty(FishingStringConstants.PROP_USE_HIGH_SEAS));
			list.Add( 1049644, FishingStringConstants.FormatSkillRequirement(90.0, "Fishing"));
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
			writer.Write( m_InUse );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 1:
				{
					m_InUse = reader.ReadBool();

					if ( m_InUse )
						Delete();

					break;
				}
			}

			Stackable = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_InUse )
			{
				from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_NET_IN_USE);
			}
			else if ( from.Skills[SkillName.Fishing].Value < 90.0 )
			{
				from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_NOT_SKILLED);
			}
			else if ( Worlds.IsOnBoat( from ) == false )
			{
				from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_NEED_BOAT);
			}
			else if ( Worlds.BoatToCloseToTown( from ) == true )
			{
				from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_NEED_DEEPER_WATER);
			}
			else if ( IsChildOf( from.Backpack ) )
			{
				from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.PROMPT_WHERE_USE_NET);
				from.BeginTarget( -1, true, TargetFlags.None, new TargetCallback( OnTarget ) );
			}
			else
			{
				from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_MUST_BE_IN_PACK);
			}
		}

		public void OnTarget( Mobile from, object obj )
		{
			if ( Deleted || m_InUse )
				return;

			IPoint3D p3D = obj as IPoint3D;

			if ( p3D == null )
				return;

			Map map = from.Map;

			if ( map == null || map == Map.Internal )
				return;

			int x = p3D.X, y = p3D.Y;

			LandTile landTile = map.Tiles.GetLandTile( x, y );
			StaticTile[] tiles = map.Tiles.GetStaticTiles( x, y, true );

			bool hasWater = false;

			if ( landTile.Z == p3D.Z && Server.Misc.Worlds.IsWaterTile( landTile.ID, 0 ) )
				hasWater = true;

			for ( int i = 0; i < tiles.Length; ++i )
			{
				StaticTile tile = tiles[i];

				if ( tile.Z == p3D.Z && Server.Misc.Worlds.IsWaterTile( tile.ID, 0 ) )
					hasWater = true;
			}

			if ( !from.InRange( p3D, 6 ) )
			{
				from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_TOO_FAR_WATER);
			}
			else if ( hasWater )
			{
				Point3D p = new Point3D( x, y, map.GetAverageZ( x, y ) );

				this.ItemID = 0x0DCA;

				m_InUse = true;
				Movable = false;
				MoveToWorld( p, map );

				from.Animate( 12, 5, 1, true, false, 0 );

				Timer.DelayCall( TimeSpan.FromSeconds( 1.5 ), TimeSpan.FromSeconds( 1.0 ), 20, new TimerStateCallback( DoEffect ), new object[]{ p, 0, from } );

				from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.MSG_PLUNGE_NET);
			}
			else
			{
				from.SendMessage(FishingStringConstants.COLOR_ERROR, FishingStringConstants.ERROR_ONLY_DEEP_WATER);
			}
		}

		private void DoEffect( object state )
		{
			if ( Deleted )
				return;

			object[] states = (object[])state;

			Point3D p = (Point3D)states[0];
			int index = (int)states[1];
			Mobile from = (Mobile)states[2];

			states[1] = ++index;

			if ( index == 1 )
			{
				Effects.SendLocationEffect( p, Map, 0x352D, 16, 4 );
				Effects.PlaySound( p, Map, 0x364 );
			}
			else if ( index <= 10 || index == 20 )
			{
				for ( int i = 0; i < 3; ++i )
				{
					int x, y;

					switch ( Utility.Random( 8 ) )
					{
						default:
						case 0: x = -1; y = -1; break;
						case 1: x = -1; y =  0; break;
						case 2: x = -1; y = +1; break;
						case 3: x =  0; y = -1; break;
						case 4: x =  0; y = +1; break;
						case 5: x = +1; y = -1; break;
						case 6: x = +1; y =  0; break;
						case 7: x = +1; y = +1; break;
					}

					Effects.SendLocationEffect( new Point3D( p.X + x, p.Y + y, p.Z ), Map, 0x352D, 16, 4 );
				}

				Effects.PlaySound( p, Map, 0x364 );

				if ( index == 20 )
					FinishEffect( p, Map, from );
				else
					this.Z -= 1;
			}
		}

		protected void Spawn( Point3D p, Map map, BaseCreature spawn, int onBoat )
		{
			if ( map == null )
			{
				spawn.Delete();
				return;
			}

			int x = p.X, y = p.Y;

			if ( onBoat != 1 )
			{
				for ( int j = 0; j < 20; ++j )
				{
					int tx = p.X - 2 + Utility.Random( 5 );
					int ty = p.Y - 2 + Utility.Random( 5 );

					LandTile t = map.Tiles.GetLandTile( tx, ty );

					if ( t.Z == p.Z && Server.Misc.Worlds.IsWaterTile( t.ID, 0 ) && !Spells.SpellHelper.CheckMulti( new Point3D( tx, ty, p.Z ), map ) )
					{
						x = tx;
						y = ty;
						break;
					}
				}
			}
			spawn.MoveToWorld( new Point3D( x, y, p.Z ), map );
		}

		protected virtual void FinishEffect( Point3D p, Map map, Mobile from )
		{
			from.RevealingAction();
			Server.Engines.Harvest.Fishing.FishingSkill( from, 8 );

			int count = Utility.RandomMinMax( 1, 1 );
			int onBoat = 0;
			string monster = "";
			Point3D SpawnAt = p;
			switch ( Utility.Random( 8 ) )
			{
				case 0: monster = "WaterNaga"; break;
				case 1: monster = "SeaHagGreater"; SpawnAt = from.Location; onBoat = 1; break;
				case 2: monster = "SeaDragon"; break;
				case 3: monster = "SeaGiant"; break;
				case 4: monster = "DeepSeaSerpent"; break;
				case 5: monster = "DemonOfTheSea"; SpawnAt = from.Location; onBoat = 1; break;
				case 6: monster = "RottingSquid"; break;
				case 7: monster = "DeepWaterElemental"; break;
			}

			for ( int i = 0; map != null && i < count; ++i )
			{
				BaseCreature spawn = new WaterNaga();

				if ( monster == "WaterNaga" ){ spawn = new WaterNaga(); }
				else if ( monster == "SeaHagGreater" ){ spawn = new SeaHagGreater(); }
				else if ( monster == "SeaDragon" ){ spawn = new SeaDragon(); }
				else if ( monster == "SeaGiant" ){ spawn = new SeaGiant(); }
				else if ( monster == "DeepSeaSerpent" ){ spawn = new DeepSeaSerpent(); }
				else if ( monster == "DemonOfTheSea" ){ spawn = new DemonOfTheSea(); }
				else if ( monster == "RottingSquid" ){ spawn = new RottingSquid(); }
				else if ( monster == "DeepWaterElemental" ){ spawn = new DeepWaterElemental(); }

				Spawn( SpawnAt, map, spawn, onBoat );

				spawn.WhisperHue = 999; // SO TASK MANAGER DELETES THEM EVENTUALLY
				spawn.Combatant = from;
			}

			Delete();
		}
	}
}
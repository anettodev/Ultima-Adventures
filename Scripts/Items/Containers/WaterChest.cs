using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Multis;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.PartySystem;
using Server.Misc;

namespace Server.Items
{
	public class WaterChest : LockableContainer, IChopable
	{
		private Timer m_DecayTimer;
		private DateTime m_DeleteTime;
		private int m_SOSLevel;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime DeleteTime{ get{ return m_DeleteTime; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int SOSLevel{ get{ return m_SOSLevel; } set{ m_SOSLevel = value; InvalidateProperties(); } }

		[Constructable]
		public WaterChest() : base( 0x2299 )
		{
			Name = "Boat";
			ContainerFunctions.BuildContainer( this, 0, 0, 0, 10 );
			Movable = false;
			Weight = 100.0;
			LiftOverride = true;

			// Start decay timer - 1 hour
			m_DeleteTime = DateTime.UtcNow + TimeSpan.FromHours( 1.0 );
			m_DecayTimer = new DecayTimer( this, m_DeleteTime );
			m_DecayTimer.Start();
		}

		public override void Open( Mobile from )
		{
			if ( this.Weight > 50 )
			{
				// Calculate loot level based on SOS level or random (1-5)
				int lootLevel = m_SOSLevel > 0 ? m_SOSLevel : Utility.RandomList( 5, 4, 4, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1 );

				if ( GetPlayerInfo.LuckyPlayer( from.Luck, from ) )
				{
					lootLevel = lootLevel + Utility.RandomMinMax( 1, 2 );
					if ( lootLevel > 5 ){ lootLevel = 5; }
				}

				// Add body parts (50% chance)
				if ( Utility.RandomBool() )
				{
					int[] list = new int[]
						{
							0xECA, 0xECB, 0xECC, 0xECD, 0xECE, 0xECF, 0xED0,
							0xED1, 0xED2, 0x1B09, 0x1B0A, 0x1B0B, 0x1B0C,
							0x1B0D, 0x1B0E, 0x1B0F, 0x1B10,
						};

					Item bones = new BodyPart( Utility.RandomList( list ) );
					bones.Name = ContainerFunctions.GetOwner( "BodySailor" );
					this.DropItem( bones );
				}

				// Generate loot using NMSDungeonLoot
				FillWithNMSLoot( lootLevel, from );

				this.Weight = 5.0;
				LoggingFunctions.LogLoot( from, this.Name, "boat" );
			}

			base.Open( from );

			Server.Items.CharacterDatabase.LootContainer( from, this );
		}

		/// <summary>
		/// Calculates the quantity range for harvest resources based on SOS level
		/// </summary>
		/// <param name="level">Loot level (1-5)</param>
		/// <returns>Random amount within the level-appropriate range</returns>
		private int GetResourceAmount( int level )
		{
			if ( level < 1 ){ level = 1; }
			if ( level > 4 ){ level = 4; }

			switch ( level )
			{
				case 1:
					return Utility.RandomMinMax( 4, 20 );
				case 2:
					return Utility.RandomMinMax( 8, 25 );
				case 3:
					return Utility.RandomMinMax( 12, 36 );
				case 4:
				default:
					return Utility.RandomMinMax( 15, 42 );
			}
		}

		/// <summary>
		/// Fills the WaterChest with loot using NMSDungeonLoot methods
		/// </summary>
		/// <param name="level">Loot level (1-5)</param>
		/// <param name="from">The player opening the chest</param>
		private void FillWithNMSLoot( int level, Mobile from )
		{
			if ( level < 1 ){ level = 1; }
			if ( level > 5 ){ level = 5; }

			// Calculate number of items based on level (doubled)
			int minItems = level * 2;
			int maxItems = ( level + 2 ) * 2;
			int itemCount = Utility.RandomMinMax( minItems, maxItems );

			// Ensure at least 1 item is generated
			if ( itemCount < 1 ){ itemCount = 1; }

			// Calculate gold amount
			int goldAmount = ( level + 1 ) * Utility.RandomMinMax( 40, 160 );
			double goldCut = goldAmount * ( MyServerSettings.GetGoldCutRate( from, null ) * 0.01 );
			goldAmount = (int)goldCut;

			if ( goldAmount > 0 )
			{
				Item gold = new Gold( goldAmount );
				this.DropItem( gold );
			}

			// Always add one high-level scroll
			Item highLevelScroll = NMSDungeonLoot.RandomHighLevelScroll();
			if ( highLevelScroll != null )
			{
				this.DropItem( highLevelScroll );
			}

			// Always add one lore book
			Item loreBook = NMSDungeonLoot.RandomLoreBook();
			if ( loreBook != null )
			{
				this.DropItem( loreBook );
			}

			// Add alchemy recipe based on SOS level
			Item alchemyRecipe = null;
			
			if ( level == 1 )
			{
				// SOS level 1: Random recipe from Category 0 (Basic) OR Category 3 (Cosmetic)
				int category = Utility.RandomBool() ? 
					Server.Engines.Craft.AlchemyRecipeConstants.CATEGORY_BASIC : 
					Server.Engines.Craft.AlchemyRecipeConstants.CATEGORY_COSMETIC;
				alchemyRecipe = NMSDungeonLoot.RandomAlchemyRecipeByCategory( category );
			}
			else if ( level == 2 )
			{
				// SOS level 2: 50% chance Category 0 (Basic), 50% chance Category 1 (Advanced)
				int category = Utility.RandomBool() ? 
					Server.Engines.Craft.AlchemyRecipeConstants.CATEGORY_BASIC : 
					Server.Engines.Craft.AlchemyRecipeConstants.CATEGORY_ADVANCED;
				alchemyRecipe = NMSDungeonLoot.RandomAlchemyRecipeByCategory( category );
			}
			else if ( level == 3 )
			{
				// SOS level 3: Random recipe from Category 1 (Advanced)
				alchemyRecipe = NMSDungeonLoot.RandomAlchemyRecipeByCategory( 
					Server.Engines.Craft.AlchemyRecipeConstants.CATEGORY_ADVANCED );
			}
			else if ( level >= 4 )
			{
				// SOS level 4+: 50% chance Category 1 (Advanced), 50% chance Category 2 (Special)
				int category = Utility.RandomBool() ? 
					Server.Engines.Craft.AlchemyRecipeConstants.CATEGORY_ADVANCED : 
					Server.Engines.Craft.AlchemyRecipeConstants.CATEGORY_SPECIAL;
				alchemyRecipe = NMSDungeonLoot.RandomAlchemyRecipeByCategory( category );
			}
			
			if ( alchemyRecipe != null )
			{
				this.DropItem( alchemyRecipe );
			}

			

			// 50% chance for DDRelic
			if ( Utility.Random( 100 ) < 50 )
			{
				Item ddRelic = NMSDungeonLoot.RandomDDRelic();
				if ( ddRelic != null )
				{
					this.DropItem( ddRelic );
				}
			}

			// 30% chance for PotionKeg
			if ( Utility.Random( 100 ) < 30 )
			{
				Item potionKeg = NMSDungeonLoot.RandomPotionKeg();
				if ( potionKeg != null )
				{
					this.DropItem( potionKeg );
				}
			}
			else 
			{
				// 50% chance to drop Alchemy Recipe Book
				if ( Utility.Random( 100 ) < 50 )
				{
					Item alchemyBook = new AlchemyRecipeBook();
					if ( alchemyBook != null )
					{
						this.DropItem( alchemyBook );
					}
				}
			}

			// 50% chance to add one special NMS item (only for SOS level >= 4)
			if ( level >= 4 && Utility.Random( 100 ) < 50 )
			{
				Item specialNMSItem = NMSDungeonLoot.RandomSpecialNMSItem();
				if ( specialNMSItem != null )
				{
					this.DropItem( specialNMSItem );
				}
			}

			// Generate items
			int itemsGenerated = 0;
			int maxAttempts = itemCount * 3; // Allow retries if items fail to generate
			int attempts = 0;

			while ( itemsGenerated < itemCount && attempts < maxAttempts )
			{
				attempts++;
				Item loot = null;
				int roll = Utility.Random( 100 );

				// 40% chance for harvest resources
				if ( roll < 40 )
				{
					loot = NMSDungeonLoot.RandomHarvestResource();
					if ( loot != null && loot.Stackable )
					{
						loot.Amount = GetResourceAmount( level );
					}
				}
				// 25% chance for weapons
				else if ( roll < 65 )
				{
					loot = NMSDungeonLoot.RandomWeapon();
				}
				// 20% chance for armor
				else if ( roll < 85 )
				{
					loot = NMSDungeonLoot.RandomArmor();
				}
				// 15% chance for tools
				else
				{
					loot = NMSDungeonLoot.RandomTools();
					if ( loot is BaseTool )
					{
						BaseTool tool = (BaseTool)loot;
						if ( tool.UsesRemaining > 0 )
						{
							tool.UsesRemaining = Utility.RandomMinMax( 3, 20 );
						}
					}
					else if ( loot is BaseHarvestTool )
					{
						BaseHarvestTool tool = (BaseHarvestTool)loot;
						if ( tool.UsesRemaining > 0 )
						{
							tool.UsesRemaining = Utility.RandomMinMax( 3, 20 );
						}
					}
				}

				// If loot is null, try to generate a harvest resource as fallback
				if ( loot == null )
				{
					loot = NMSDungeonLoot.RandomHarvestResource();
					if ( loot != null && loot.Stackable )
					{
						loot.Amount = GetResourceAmount( level );
					}
				}

				if ( loot != null )
				{
					this.DropItem( loot );
					itemsGenerated++;
				}
			}
		}

		public virtual void OnChop( Mobile from )
		{
			// Stop decay timer since player is chopping it
			if ( m_DecayTimer != null )
			{
				m_DecayTimer.Stop();
				m_DecayTimer = null;
			}

			int fishSkill = (int)(from.Skills[SkillName.Fishing].Value/10);
				if ( fishSkill > 7 ){ fishSkill = 7; }
			int woodSkill = (int)(from.Skills[SkillName.Lumberjacking].Value/2);
				if ( woodSkill < 5 ){ woodSkill = 5; }

			switch ( Utility.Random( fishSkill ) )
			{
				case 0: from.AddToBackpack( new Board( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 1: from.AddToBackpack( new AshBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 2: from.AddToBackpack( new CherryBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 3: from.AddToBackpack( new EbonyBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 4: from.AddToBackpack( new GoldenOakBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 5: from.AddToBackpack( new HickoryBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 6: from.AddToBackpack( new RosewoodBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
			}

			from.PlaySound( 0x13E );
			from.PlaySound( 0x026 );
			Effects.SendLocationEffect( this.Location, this.Map, 0x352D, 16, 4 );
			from.SendMessage( FishingStringConstants.MSG_SALVAGE_WOOD );
			this.Delete();
		}

		public override bool DisplaysContent{ get{ return false; } }
		public override bool DisplayWeight{ get{ return false; } }

		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);

			// Display SOS level in cyan if this chest came from an SOS treasure
			if (m_SOSLevel > 0)
			{
				string sosLevelText = String.Format("SOS [Level {0}]", m_SOSLevel);
				list.Add(1070722, FishingStringConstants.FormatProperty(sosLevelText));
			}

			// Always display that wood can be recovered (in cyan)
			list.Add(1070722, FishingStringConstants.FormatProperty(FishingStringConstants.PROP_CAN_RECOVER_WOOD));
		}

        public override void OnAfterSpawn()
        {
			base.OnAfterSpawn();
			
			// Only auto-relocate if current location is not a valid water tile
			// This allows GMs to manually place chests at water locations using [Add command
			if ( this.Map != null )
			{
				LandTile tile = this.Map.Tiles.GetLandTile( this.X, this.Y );
				bool isWaterTile = Server.Misc.Worlds.IsWaterTile( tile.ID, 0 );
				
				// If not on water, relocate to a random sea location
				if ( !isWaterTile )
				{
					Point3D oldLocation = this.Location;
					string oldLocationStr = String.Format( "{0} ({1}, {2}, {3})", this.Map.Name, oldLocation.X, oldLocation.Y, oldLocation.Z );
					
					this.Location = Worlds.GetRandomLocation( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ), "sea" );
					
					Point3D newLocation = this.Location;
					string newLocationStr = String.Format( "{0} ({1}, {2}, {3})", this.Map.Name, newLocation.X, newLocation.Y, newLocation.Z );
					
					// Notify staff of relocation
					foreach ( Mobile staff in World.Mobiles.Values )
					{
						if ( staff != null && staff.AccessLevel >= AccessLevel.GameMaster && staff.NetState != null )
						{
							staff.SendMessage( 33, "[WaterChest] Relocated from {0} to {1}", oldLocationStr, newLocationStr );
						}
					}
				}
			}
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			if ( m_DecayTimer != null )
			{
				m_DecayTimer.Stop();
				m_DecayTimer = null;
			}
		}

		public WaterChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version

			writer.WriteDeltaTime( m_DeleteTime );
			writer.Write( (int) m_SOSLevel );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( version >= 1 )
			{
				m_DeleteTime = reader.ReadDeltaTime();
			}
			else
			{
				// For old items without decay time, set it to 1 hour from now
				m_DeleteTime = DateTime.UtcNow + TimeSpan.FromHours( 1.0 );
			}

			if ( version >= 2 )
			{
				m_SOSLevel = reader.ReadInt();
			}
			else
			{
				m_SOSLevel = 0;
			}

			// Restart decay timer if not already deleted
			if ( m_DeleteTime > DateTime.UtcNow )
			{
				TimeSpan delay = m_DeleteTime - DateTime.UtcNow;
				m_DecayTimer = new DecayTimer( this, m_DeleteTime );
				m_DecayTimer.Start();
			}
			else
			{
				// Delete immediately if decay time has passed
				Timer.DelayCall( TimeSpan.Zero, new TimerCallback( Delete ) );
			}
		}

		private class DecayTimer : Timer
		{
			private WaterChest m_Chest;

			public DecayTimer( WaterChest chest, DateTime deleteTime ) : base( deleteTime - DateTime.UtcNow )
			{
				m_Chest = chest;
				Priority = TimerPriority.OneMinute;
			}

			protected override void OnTick()
			{
				if ( m_Chest != null && !m_Chest.Deleted )
				{
					m_Chest.Delete();
				}
			}
		}
	}
}
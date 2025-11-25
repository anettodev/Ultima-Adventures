using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Gumps;
using Server.Mobiles;
using Server.Misc;
using Server.Regions;

namespace Server.Engines.Harvest
{
	/// <summary>
	/// Abstract base class for all harvest systems (Mining, Lumberjacking, Fishing).
	/// Provides core harvesting functionality including validation, resource processing, and automation integration.
	/// </summary>
	public abstract class HarvestSystem
	{
		#region Fields

		private List<HarvestDefinition> m_Definitions;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the list of harvest definitions for this system
		/// </summary>
		public List<HarvestDefinition> Definitions { get { return m_Definitions; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HarvestSystem class
		/// </summary>
		public HarvestSystem()
		{
			m_Definitions = new List<HarvestDefinition>();
		}

		#endregion

		#region Validation Methods

		/// <summary>
		/// Checks if the tool is valid and not worn out
		/// </summary>
		/// <param name="from">The mobile using the tool</param>
		/// <param name="tool">The tool to check</param>
		/// <returns>True if the tool is valid and usable</returns>
		public virtual bool CheckTool( Mobile from, Item tool )
		{
			bool wornOut = ( tool == null || tool.Deleted || (tool is IUsesRemaining && ((IUsesRemaining)tool).UsesRemaining <= 0) );

			if ( wornOut )
			{
                from.PlaySound(from.Female ? HarvestConstants.SOUND_TOOL_WORN_OUT_FEMALE : HarvestConstants.SOUND_TOOL_WORN_OUT_MALE);
                from.Say(HarvestStringConstants.EMOTE_TOOL_WORN_OUT);
                
                PlayerMobile pm = HarvestAutomationHelper.GetAutomatedPlayer(from);
                if (pm != null)
					HarvestAutomationHelper.StopAutomation(pm);
				
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool!
            }

			return !wornOut;
		}

		/// <summary>
		/// Checks if harvest can proceed with the given tool
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <returns>True if harvest can proceed</returns>
		public virtual bool CheckHarvest( Mobile from, Item tool )
		{
			return CheckTool( from, tool );
		}

		/// <summary>
		/// Checks if harvest can proceed with the given tool and target
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="toHarvest">The target to harvest</param>
		/// <returns>True if harvest can proceed</returns>
		public virtual bool CheckHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			return CheckTool( from, tool );
		}

		/// <summary>
		/// Checks if the mobile is within range of the harvest location
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="map">The map</param>
		/// <param name="loc">The harvest location</param>
		/// <param name="timed">Whether this is a timed check</param>
		/// <returns>True if within range</returns>
		public virtual bool CheckRange( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, bool timed )
		{
			bool inRange = ( from.Map == map && from.InRange( loc, def.MaxRange ) );

			if ( !inRange )
				def.SendMessageTo( from, timed ? def.TimedOutOfRangeMessage : def.OutOfRangeMessage );

			return inRange;
		}

		/// <summary>
		/// Checks if resources are available at the harvest location
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="map">The map</param>
		/// <param name="loc">The harvest location</param>
		/// <param name="timed">Whether this is a timed check</param>
		/// <returns>True if resources are available</returns>
		public virtual bool CheckResources( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, bool timed )
		{
			if (from == null || map == null || tool == null || loc == Point3D.Zero) 
			{
				from.SendMessage(HarvestConstants.MSG_COLOR_ERROR, HarvestStringConstants.MSG_ERROR_UNEXPECTED);
				if (map == null)
					from.SendMessage(HarvestConstants.MSG_COLOR_ERROR, HarvestStringConstants.MSG_ERROR_MAP_NOT_EXISTS);
				if (tool == null)
					from.SendMessage(HarvestConstants.MSG_COLOR_ERROR, HarvestStringConstants.MSG_ERROR_TOOL_NOT_EXISTS);
				if (loc == Point3D.Zero)
					from.SendMessage(HarvestConstants.MSG_COLOR_ERROR, HarvestStringConstants.MSG_ERROR_INVALID_LOCATION);
													
				return false;
			}

			HarvestBank bank = def.GetBank( map, loc.X, loc.Y );
			bool available = ( bank != null && bank.Current >= def.ConsumedPerHarvest );

			PlayerMobile pm = HarvestAutomationHelper.GetAutomatedPlayer(from);
			if ( !available && pm != null )
			{
				if ( AdventuresAutomation.TaskTarget.ContainsKey(pm)) // ran out of resources on the current target area
				{
					from.SendMessage(HarvestConstants.MSG_COLOR_ERROR, HarvestStringConstants.MSG_SEARCHING_NEW_LOCATION);

					AdventuresAutomation.TaskTarget.Remove(pm); // this means next doaction itll try another spot.
					return false;
				}
			}
			else if (!available)
				def.SendMessageTo( from, timed ? def.DoubleHarvestMessage : def.NoResourcesMessage );

			return available;
		}

		#endregion

		#region Core Harvest Methods

		/// <summary>
		/// Begins the harvesting process for the mobile
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <returns>True if harvesting began successfully</returns>
		public virtual bool BeginHarvesting( Mobile from, Item tool )
		{
			if ( !CheckHarvest( from, tool ) )
				return false;

			PlayerMobile pm = HarvestAutomationHelper.GetAutomatedPlayer(from);
			if (pm != null)
			{
				AdventuresAutomation.DoAction(pm);
				return true;
			}
			else
				from.Target = new HarvestTarget( tool, this );

			return true;
		}

		/// <summary>
		/// Completes the harvesting process and processes the harvested item
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="toHarvest">The target being harvested</param>
		/// <param name="locked">The lock object for concurrent harvest prevention</param>
		public virtual void FinishHarvesting( Mobile from, Item tool, HarvestDefinition def, object toHarvest, object locked )
		{
			// Performance optimization: Cache PlayerMobile cast once
			PlayerMobile pm = HarvestAutomationHelper.GetAutomatedPlayer(from);

			from.EndAction( locked );

			if ( !CheckHarvest( from, tool ) )
			{
				if (pm != null)
					HarvestAutomationHelper.StopAutomation(pm);

				return;
			}

			int tileID;
			Map map;
			Point3D loc;

			if ( !GetHarvestDetails( from, tool, toHarvest, out tileID, out map, out loc ) )
			{
				OnBadHarvestTarget( from, tool, toHarvest );
				return;
			}
			else if ( !def.Validate( tileID ) )
			{
				OnBadHarvestTarget( from, tool, toHarvest );
				return;
			}
			
			if ( !CheckRange( from, tool, def, map, loc, true ) )
				return;
			else if ( !CheckResources( from, tool, def, map, loc, true ) )
				return;
			else if ( !CheckHarvest( from, tool, def, toHarvest ) )
				return;

			if ( SpecialHarvest( from, tool, def, map, loc ) )
				return;

			HarvestBank bank = def.GetBank( map, loc.X, loc.Y );

			if ( bank == null )
				return;

			HarvestVein vein = bank.Vein;

			if ( vein != null )
				vein = MutateVein( from, tool, def, bank, toHarvest, vein );

			if ( vein == null )
				return;

			HarvestResource primary = vein.PrimaryResource;
			HarvestResource fallback = vein.FallbackResource;
			HarvestResource resource = MutateResource( from, tool, def, map, loc, vein, primary, fallback );

			double skillBase = from.Skills[def.Skill].Base;
			double skillValue = from.Skills[def.Skill].Value;

			Type type = null;

			if ( skillValue >= resource.ReqSkill && from.CheckSkill( def.Skill, resource.MinSkill, resource.MaxSkill ) )
			{
				type = GetResourceType( from, tool, def, map, loc, resource );

				if ( type != null )
					type = MutateType( type, from, tool, def, map, loc, resource );

				if ( type != null )
				{
					Item item = Construct( type, from );

					if ( item == null )
					{
						type = null;
					}
					else
					{
						if ( item.Stackable )
						{
							// Performance optimization: Cache region lookup
							Region reg = Region.Find( from.Location, from.Map );

							// Calculate item amount based on region and bonuses
							item.Amount = CalculateItemAmount(from, def, bank, item, reg, map, loc);

							// Detect special resources
							bool findSpecialOre;
							bool findSpecialGranite;
							bool findBlackLog;
							bool findToughLog;
							DetectSpecialResources(item, out findSpecialOre, out findSpecialGranite, out findBlackLog, out findToughLog);

							#region Commented Code - Future Reference
							/*
							 * DISABLED REGIONS - Kept for future reference
							 * 
							 * These regions were previously active but are currently disabled.
							 * They may be re-enabled in the future or used as reference for new regions.
							 */

							// Sea areas driftwood conversion - DISABLED
							/*if ( Worlds.IsExploringSeaAreas( from ) && item is BaseLog )
							{
								int driftWood = item.Amount;
								item.Delete();
								item = new DriftwoodLog( driftWood );
								from.SendMessage(55, "Você corta alguns troncos de madeira flutuante.");
							}*/

							// Underworld Xormite/Mithril conversion - DISABLED
							/*if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" &&
								findSpecialOre &&
								item is BaseOre &&
								from.Map == Map.TerMur )
							{
								int xormiteOre = item.Amount;
								item.Delete();
								item = new XormiteOre( xormiteOre );
								from.SendMessage(55, "Você encontrou minério xormite.");
							}
							else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" &&
								findSpecialOre &&
								item is BaseOre )
							{
								int mithrilOre = item.Amount;
								item.Delete();
								item = new MithrilOre( mithrilOre );
								from.SendMessage(55, "Você encontrou minério de mithril.");
							}
							else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Serpent Island" &&
								findSpecialOre &&
								item is BaseOre )
							{
								int obsidianOre = item.Amount;
								item.Delete();
								item = new ObsidianOre( obsidianOre );
								from.SendMessage(55, "Você encontrou minério de obsidiana.");
							}
							else if ( Worlds.IsExploringSeaAreas( from ) &&
								findSpecialOre &&
								item is BaseOre )
							{
								int nepturiteOre = item.Amount;
								item.Delete();
								item = new NepturiteOre( nepturiteOre );
								from.SendMessage(55, "Você encontrou minério de nepturite.");
							}
							else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" &&
								findSpecialGranite &&
								item is BaseGranite &&
								from.Map == Map.TerMur )
							{
								int xormiteGranite = item.Amount;
								item.Delete();
								item = new XormiteGranite( xormiteGranite );
								from.SendMessage(55, "Você encontrou granito de xormite.");
							}
							else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" &&
								findSpecialGranite &&
								item is BaseGranite )
							{
								int mithrilGranite = item.Amount;
								item.Delete();
								item = new MithrilGranite( mithrilGranite );
								from.SendMessage(55, "Você encontrou granito de mithril.");
							}
							else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Serpent Island" && findSpecialGranite && item is BaseGranite )
							{
								int obsidianGranite = item.Amount;
								item.Delete();
								item = new ObsidianGranite( obsidianGranite );
								from.SendMessage(55, "Você encontrou granito obsidiano.");
							}
							else if ( Worlds.IsExploringSeaAreas( from ) && findSpecialGranite && item is BaseGranite )
							{
								int nepturiteGranite = item.Amount;
								item.Delete();
								item = new NepturiteGranite( nepturiteGranite );
								from.SendMessage(55, "Você encontrou granito de nepturite.");
							}*/

							// Ghost log conversion - DISABLED
							/*else if ( reg.IsPartOf( typeof( NecromancerRegion ) ) && FindGhostLog && item is BaseLog )
							{
								int ghostLog = item.Amount;
								item.Delete();
								item = new GhostLog( ghostLog );
								from.SendMessage( "Você corta algumas toras de madeira fantasma.");
							}*/

							// Underworld Petrified log conversion - DISABLED
							/*else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" && findToughLog && item is BaseLog )
							{
								int toughLog = item.Amount;
								item.Delete();
								item = new PetrifiedLog( toughLog );
								from.SendMessage( "Você corta algumas toras petrificadas.");
							}*/

							// Shipwreck driftwood conversion - DISABLED
							/*else if ( ( reg.IsPartOf( "Shipwreck Grotto" ) || reg.IsPartOf( "Barnacled Cavern" ) ) && findToughLog && item is BaseLog )
							{
								int driftWood = item.Amount;
								item.Delete();
								item = new DriftwoodLog( driftWood );
								from.SendMessage( "Você corta algumas toras de madeira flutuante.");
							}*/

							// Ore found messages - DISABLED (currently handled by resource.SendSuccessTo)
							/*else if ( item is IronOre ){ from.SendMessage(55, "Você encontrou alguns minérios de ferro."); }
							else if ( item is DullCopperOre ){ from.SendMessage( "Você encontra alguns minérios de cobre rústico."); }
							else if ( item is ShadowIronOre ){ from.SendMessage( "Você encontra alguns minérios de ferro negro."); }
							else if ( item is CopperOre ){ from.SendMessage( "Você encontra alguns minérios de cobre."); }
							else if ( item is BronzeOre ){ from.SendMessage( "Você encontra alguns minérios de bronze."); }
							else if (item is PlatinumOre) { from.SendMessage("Você encontra alguns minérios de platina."); }
							else if ( item is GoldOre ){ from.SendMessage( "Você encontra alguns minérios de dourado."); }
							else if ( item is AgapiteOre ){ from.SendMessage( "Você encontra alguns minérios de agapite."); }
							else if ( item is VeriteOre ){ from.SendMessage( "Você encontra alguns minérios de verite."); }
							else if ( item is ValoriteOre ){ from.SendMessage( "Você encontra alguns minérios de valorite."); }
							else if (item is TitaniumOre) { from.SendMessage("Você encontra alguns minérios de titânio."); }
							else if (item is RoseniumOre) { from.SendMessage("Você encontra alguns minérios de rosênio."); }*/

							// Log found messages - DISABLED (currently handled by resource.SendSuccessTo)
							/*else if ( item is Log ){ from.SendMessage("Você corta algumas toras."); }
							else if ( item is AshLog ){ from.SendMessage( "Você corta algumas toras de Carvalho cinza."); }
							else if ( item is CherryLog ){ from.SendMessage( "Você corta algumas toras de Cerejeira."); }
							else if ( item is EbonyLog ){ from.SendMessage( "Você corta algumas toras de ébano."); }
							else if ( item is GoldenOakLog ){ from.SendMessage( "Você corta algumas toras de Ipê-amarelo."); }
							else if ( item is HickoryLog ){ from.SendMessage( "Você corta algumas toras de Nogueira Branca."); }
							else if ( item is MahoganyLog ){ from.SendMessage( "Você corta algumas toras de mogno."); }
							else if ( item is OakLog ){ from.SendMessage( "Você corta algumas toras de Carvalho."); }
							else if ( item is PineLog ){ from.SendMessage( "Você corta algumas toras de Pinheiro."); }
							else if ( item is RosewoodLog ){ from.SendMessage( "Você corta algumas toras de Pau-Brasil."); }
							else if ( item is WalnutLog ){ from.SendMessage( "Você corta algumas toras de Nogueira."); }
							else if ( item is ElvenLog ){ from.SendMessage( "Você corta algumas toras de madeira élfica."); }*/

							// Fishing special locations - DISABLED
							/*if ( tool is FishingPole && Server.Engines.Harvest.Fishing.IsNearHugeShipWreck( from ) && 
								from.Skills[SkillName.Fishing].Value >= Utility.RandomMinMax( 1, 250 ) )
							{
								Server.Engines.Harvest.Fishing.FishUpFromMajorWreck( from );
							}
							else if ( tool is FishingPole && Server.Engines.Harvest.Fishing.IsNearSpaceCrash( from ) && 
								from.Skills[SkillName.Fishing].Value >= Utility.RandomMinMax( 1, 250 ) )
							{
								Server.Engines.Harvest.Fishing.FishUpFromSpaceship( from );
							}
							else if ( tool is FishingPole && Server.Engines.Harvest.Fishing.IsNearUnderwaterRuins( from ) && 
								from.Skills[SkillName.Fishing].Value >= Utility.RandomMinMax( 1, 250 ) )
							{
								Server.Engines.Harvest.Fishing.FishUpFromRuins( from );
							}*/
							#endregion

							// Apply region bonuses and conversions
							item = ApplyRegionBonuses(from, item, reg, map, loc, findSpecialOre, findSpecialGranite, findBlackLog, skillValue, resource);

							// Send ore/granite found messages
							SendOreTypeFoundMessage(from, item);

							// Performance optimization: Cache world name lookup
							string worldName = Worlds.GetMyWorld(map, loc, loc.X, loc.Y);
							HandleWorldBonuses(from, map, loc, worldName);
						}
						else
						{
							// Handle special items (books, scrolls)
							HandleSpecialItems(from, item);
						}

						bank.Consume( item.Amount, from );

						if ( Give( from, item, def.PlaceAtFeetIfFull ) )
						{
							SendSuccessTo( from, item, resource );
						}
						else
						{
							SendPackFullTo( from, item, def, resource );
							item.Delete();
						}

						BonusHarvestResource bonus = def.GetBonusResource();

						if ( bonus != null && bonus.Type != null && skillBase >= bonus.ReqSkill )
						{
							Item bonusItem = Construct( bonus.Type, from );

							if ( Give( from, bonusItem, true ) )	//Bonuses always allow placing at feet, even if pack is full irregrdless of def
							{
								bonus.SendSuccessTo( from );
							}
							else
							{
								item.Delete();
							}
						}

						if ( tool is IUsesRemaining )
						{
							IUsesRemaining toolWithUses = (IUsesRemaining)tool;

							toolWithUses.ShowUsesRemaining = true;

							if ( toolWithUses.UsesRemaining > 0 )
								--toolWithUses.UsesRemaining;

							if ( toolWithUses.UsesRemaining < 1 )
							{
								tool.Delete();
								def.SendMessageTo( from, def.ToolBrokeMessage );
							}
						}
					}
				}
			}

            if (type == null) 
			{
                def.SendMessageTo(from, def.FailMessage);
                // Clear target for automation to find new spot
                if (pm != null)
                {
                    HarvestAutomationHelper.ClearTargetAndRetry(pm, this);
                }
                return;
            }
				

			OnHarvestFinished( from, tool, def, vein, bank, resource, toHarvest );
			
			// Clear target after successful harvest and continue automation with delay
			if (pm != null)
			{
				HarvestAutomationHelper.ClearTargetAndRetry(pm, this);
			}
		}

		/// <summary>
		/// Called during the harvesting process (between harvest actions)
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="toHarvest">The target being harvested</param>
		/// <param name="locked">The lock object for concurrent harvest prevention</param>
		/// <param name="last">Whether this is the last harvest action</param>
		/// <returns>True if harvesting should continue</returns>
		public virtual bool OnHarvesting( Mobile from, Item tool, HarvestDefinition def, object toHarvest, object locked, bool last )
		{
			PlayerMobile pm = HarvestAutomationHelper.GetAutomatedPlayer(from);

			if ( !CheckHarvest( from, tool ) )
			{
				if (pm != null)
					HarvestAutomationHelper.StopAutomation(pm);

				from.EndAction( locked );
				return false;
			}

			int tileID;
			Map map;
			Point3D loc;

			if ( !GetHarvestDetails( from, tool, toHarvest, out tileID, out map, out loc ) )
			{
				from.EndAction( locked );
				OnBadHarvestTarget( from, tool, toHarvest );

				if (pm != null)
					HarvestAutomationHelper.StopAutomation(pm);

				return false;
			}
			else if ( !def.Validate( tileID ) )
			{
				from.EndAction( locked );
				OnBadHarvestTarget( from, tool, toHarvest );

				if (pm != null)
					HarvestAutomationHelper.StopAutomation(pm);

				return false;
			}
			else if ( !CheckRange( from, tool, def, map, loc, true ) )
			{
				from.EndAction( locked );

				if (pm != null)
					HarvestAutomationHelper.StopAutomation(pm);

				return false;
			}
			else if ( !CheckResources( from, tool, def, map, loc, true ) )
			{
				from.EndAction( locked );
				
				// Clear target for automation to find new spot when resources depleted
				if (pm != null)
				{
					HarvestAutomationHelper.ClearTargetAndRetry(pm, this);
				}

				return false;
			}
			else if ( !CheckHarvest( from, tool, def, toHarvest ) )
			{
				from.EndAction( locked );

				if (pm != null)
					HarvestAutomationHelper.StopAutomation(pm);

				return false;
			}

			DoHarvestingEffect( from, tool, def, map, loc );

			new HarvestSoundTimer( from, tool, this, def, toHarvest, locked, last ).Start();

			return !last;
		}

		/// <summary>
		/// Starts the harvesting process
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="toHarvest">The target to harvest</param>
		public virtual void StartHarvesting( Mobile from, Item tool, object toHarvest )
		{
			PlayerMobile pm = HarvestAutomationHelper.GetAutomatedPlayer(from);

			if ( !CheckHarvest( from, tool ) )
			{
				// Tool check failed - stop automation
				if (pm != null)
				{
					HarvestAutomationHelper.StopAutomation(pm);
				}
				return;
			}

			int tileID;
			Map map;
			Point3D loc;

			if ( !GetHarvestDetails( from, tool, toHarvest, out tileID, out map, out loc ) )
			{
                if (pm != null)
					HarvestAutomationHelper.StopAutomation(pm);

				OnBadHarvestTarget( from, tool, toHarvest );

				return;
			}

			HarvestDefinition def = GetDefinition( tileID );

			if ( def == null )
			{
                OnBadHarvestTarget( from, tool, toHarvest );

				if (pm != null)
					HarvestAutomationHelper.StopAutomation(pm);

				return;
			}

			if ( !CheckRange( from, tool, def, map, loc, false ) )
			{				
				if (pm != null)
					HarvestAutomationHelper.StopAutomation(pm);

				return;
			}
			else if ( !CheckResources( from, tool, def, map, loc, false ) )
			{
				// Resources depleted - clear target and try to find new spot
				if (pm != null)
				{
					HarvestAutomationHelper.ClearTargetAndRetry(pm, this);
				}
				return;
			}
			else if ( !CheckHarvest( from, tool, def, toHarvest ) )
			{
                if (pm != null)
					HarvestAutomationHelper.StopAutomation(pm);

				return;
			}

			object toLock = GetLock( from, tool, def, toHarvest );

			if ( !from.BeginAction( toLock ) )
			{
				OnConcurrentHarvest( from, tool, def, toHarvest );
				
				// Concurrent harvest - set delay to retry instead of stopping
				if (pm != null)
				{
					HarvestAutomationHelper.RetryAutomation(pm, this);
				}

				return;
			
			}

			new HarvestTimer( from, tool, this, def, toHarvest, toLock ).Start();
			OnHarvestStarted( from, tool, def, toHarvest );
		}

		#endregion

		#region Item Processing Methods

		/// <summary>
		/// Constructs an item instance from the given type
		/// </summary>
		/// <param name="type">The type of item to construct</param>
		/// <param name="from">The mobile who will receive the item</param>
		/// <returns>The constructed item, or null if construction failed</returns>
		public virtual Item Construct( Type type, Mobile from )
		{
			try{ return Activator.CreateInstance( type ) as Item; }
			catch{ return null; }
		}

		/// <summary>
		/// Mutates the resource type based on region and other factors
		/// </summary>
		/// <param name="type">The original resource type</param>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="map">The map</param>
		/// <param name="loc">The harvest location</param>
		/// <param name="resource">The harvest resource</param>
		/// <returns>The mutated resource type</returns>
		public virtual Type MutateType( Type type, Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource )
		{
			return from.Region.GetResource( type );
		}

		/// <summary>
		/// Gets the resource type from the harvest resource
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="map">The map</param>
		/// <param name="loc">The harvest location</param>
		/// <param name="resource">The harvest resource</param>
		/// <returns>The resource type, or null if none available</returns>
		public virtual Type GetResourceType( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource )
		{
			if ( resource.Types.Length > 0 )
				return resource.Types[Utility.Random( resource.Types.Length )];

			return null;
		}

		/// <summary>
		/// Mutates the harvest resource based on skill and other factors
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="map">The map</param>
		/// <param name="loc">The harvest location</param>
		/// <param name="vein">The harvest vein</param>
		/// <param name="primary">The primary resource</param>
		/// <param name="fallback">The fallback resource</param>
		/// <returns>The selected resource (primary or fallback)</returns>
		public virtual HarvestResource MutateResource( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestVein vein, HarvestResource primary, HarvestResource fallback )
		{
			bool racialBonus = (def.RaceBonus && from.Race == Race.Elf );

			if( vein.ChanceToFallback > (Utility.RandomDouble() + (racialBonus ? HarvestConstants.ELF_VEIN_FALLBACK_BONUS : 0)) )
				return fallback;

			double skillValue = from.Skills[def.Skill].Value;

			if ( fallback != null && (skillValue < primary.ReqSkill || skillValue < primary.MinSkill) )
				return fallback;

			return primary;
		}

		/// <summary>
		/// Gives the harvested item to the mobile
		/// </summary>
		/// <param name="m">The mobile receiving the item</param>
		/// <param name="item">The item to give</param>
		/// <param name="placeAtFeet">Whether to place at feet if backpack is full</param>
		/// <returns>True if the item was successfully given</returns>
		public virtual bool Give( Mobile m, Item item, bool placeAtFeet )
		{
			if ( m.PlaceInBackpack( item ) )
				return true;

			if ( !placeAtFeet )
				return false;

			Map map = m.Map;

			if ( map == null )
				return false;

			List<Item> atFeet = new List<Item>();

			foreach ( Item obj in m.GetItemsInRange( 0 ) )
				atFeet.Add( obj );

			for ( int i = 0; i < atFeet.Count; ++i )
			{
				Item check = atFeet[i];

				if ( check.StackWith( m, item, false ) )
					return true;
			}

			item.MoveToWorld( m.Location, map );
			return true;
		}

		#endregion

		#region Effect Methods

		/// <summary>
		/// Plays the harvesting sound effect
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="toHarvest">The target being harvested</param>
		public virtual void DoHarvestingSound( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			if ( def.EffectSounds.Length > 0 )
				from.PlaySound( Utility.RandomList( def.EffectSounds ) );
		}

		/// <summary>
		/// Performs the harvesting visual effect
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="map">The map</param>
		/// <param name="loc">The harvest location</param>
		public virtual void DoHarvestingEffect( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc )
		{
			from.Direction = from.GetDirectionTo( loc );

			if ( !from.Mounted )
				from.Animate( Utility.RandomList( def.EffectActions ), HarvestConstants.ANIMATION_SPEED, HarvestConstants.ANIMATION_REPEAT, HarvestConstants.ANIMATION_FORWARD, HarvestConstants.ANIMATION_REPEAT_BACK, HarvestConstants.ANIMATION_REPEAT_DELAY );
		}

		/// <summary>
		/// Sends success message to the mobile
		/// </summary>
		/// <param name="from">The mobile who harvested</param>
		/// <param name="item">The item that was harvested</param>
		/// <param name="resource">The harvest resource</param>
		public virtual void SendSuccessTo( Mobile from, Item item, HarvestResource resource )
		{
			resource.SendSuccessTo( from );
		}

		/// <summary>
		/// Sends pack full message to the mobile
		/// </summary>
		/// <param name="from">The mobile whose pack is full</param>
		/// <param name="item">The item that couldn't be placed</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="resource">The harvest resource</param>
		public virtual void SendPackFullTo( Mobile from, Item item, HarvestDefinition def, HarvestResource resource )
        {
            item.OnDragDrop(from, item);
            def.SendMessageTo( from, def.PackFullMessage );
		}

		#endregion

		#region Utility Methods

		/// <summary>
		/// Gets the harvest definition for the given tile ID
		/// </summary>
		/// <param name="tileID">The tile ID to check</param>
		/// <returns>The harvest definition, or null if none found</returns>
		public virtual HarvestDefinition GetDefinition( int tileID )
		{
			HarvestDefinition def = null;

			for ( int i = 0; def == null && i < m_Definitions.Count; ++i )
			{
				HarvestDefinition check = m_Definitions[i];

				if (check.Validate(tileID)) {
                    def = check;
					break; // TODO: COOP3R check
                }
			}

			return def;
		}

		/// <summary>
		/// Gets harvest details from the target object
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="toHarvest">The target to harvest</param>
		/// <param name="tileID">Output: The tile ID</param>
		/// <param name="map">Output: The map</param>
		/// <param name="loc">Output: The location</param>
		/// <returns>True if harvest details were successfully retrieved</returns>
		public virtual bool GetHarvestDetails( Mobile from, Item tool, object toHarvest, out int tileID, out Map map, out Point3D loc )
		{
			if ( toHarvest is Static && !((Static)toHarvest).Movable )
			{
				Static obj = (Static)toHarvest;

				tileID = (obj.ItemID & HarvestConstants.TILE_ID_BITMASK) | HarvestConstants.STATIC_TILE_FLAG;
				map = obj.Map;
				loc = obj.GetWorldLocation();
			}
			else if ( toHarvest is StaticTarget )
			{
				StaticTarget obj = (StaticTarget)toHarvest;

				tileID = (obj.ItemID & HarvestConstants.TILE_ID_BITMASK) | HarvestConstants.STATIC_TILE_FLAG;
				map = from.Map;
				loc = obj.Location;
			}
			else if ( toHarvest is LandTarget )
			{
				LandTarget obj = (LandTarget)toHarvest;

				tileID = obj.TileID;
				map = from.Map;
				loc = obj.Location;
			}
			else
			{
				tileID = 0;
				map = null;
				loc = Point3D.Zero;
				return false;
			}

			return ( map != null && map != Map.Internal );
		}

		/// <summary>
		/// Gets the lock object for concurrent harvest prevention
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="toHarvest">The target to harvest</param>
		/// <returns>The lock object</returns>
		/// <remarks>
		/// Here we prevent multiple harvesting.
		/// 
		/// Some options:
		///  - 'return tool;' : This will allow the player to harvest more than once concurrently, but only if they use multiple tools. This seems to be as OSI.
		///  - 'return GetType();' : This will disallow multiple harvesting of the same type. That is, we couldn't mine more than once concurrently, but we could be both mining and lumberjacking.
		///  - 'return typeof( HarvestSystem );' : This will completely restrict concurrent harvesting.
		/// </remarks>
		public virtual object GetLock( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
		    return typeof( HarvestSystem );
		}

		#endregion

		#region Virtual Hook Methods

		/// <summary>
		/// Called when a bad harvest target is selected
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="toHarvest">The invalid target</param>
		public virtual void OnBadHarvestTarget( Mobile from, Item tool, object toHarvest )
		{
		}

		/// <summary>
		/// Called when a concurrent harvest attempt is made
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="toHarvest">The target being harvested</param>
		public virtual void OnConcurrentHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
		}

		/// <summary>
		/// Called when harvesting starts
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="toHarvest">The target being harvested</param>
		public virtual void OnHarvestStarted( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
		}

		/// <summary>
		/// Called when harvesting finishes successfully
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="vein">The harvest vein used</param>
		/// <param name="bank">The harvest bank</param>
		/// <param name="resource">The harvest resource obtained</param>
		/// <param name="harvested">The target that was harvested</param>
		public virtual void OnHarvestFinished( Mobile from, Item tool, HarvestDefinition def, HarvestVein vein, HarvestBank bank, HarvestResource resource, object harvested )
		{
		}

		/// <summary>
		/// Performs special harvest logic (override in derived classes)
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="map">The map</param>
		/// <param name="loc">The harvest location</param>
		/// <returns>True if special harvest handled the harvest (prevents normal processing)</returns>
		public virtual bool SpecialHarvest( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc )
		{
			return false;
		}

		/// <summary>
		/// Mutates the harvest vein (override in derived classes)
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="tool">The tool being used</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="bank">The harvest bank</param>
		/// <param name="toHarvest">The target being harvested</param>
		/// <param name="vein">The original vein</param>
		/// <returns>The mutated vein, or the original if no mutation</returns>
		public virtual HarvestVein MutateVein( Mobile from, Item tool, HarvestDefinition def, HarvestBank bank, object toHarvest, HarvestVein vein )
		{
			return vein;
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Sends a harvest target to the mobile from a delayed callback
		/// </summary>
		/// <param name="from">The mobile to send the target to</param>
		/// <param name="o">The object array containing [Item, HarvestSystem]</param>
		public static void SendHarvestTarget( Mobile from, object o )
        {
            if (!(o is object[]))
                return;
            object[] arglist = (object[])o;
 
            if (arglist.Length != 2)
                return;
 
            if (!(arglist[0] is Item))
                return;
 
            if (!(arglist[1] is HarvestSystem))
                return;
               
            from.Target = new HarvestTarget((Item)arglist[0], (HarvestSystem)arglist[1] );
        }

		#endregion

		#region Helper Methods

		/// <summary>
		/// Sends appropriate message when ore or granite is found
		/// </summary>
		/// <param name="from">The mobile who found the resource</param>
		/// <param name="item">The item that was found</param>
		private void SendOreTypeFoundMessage(Mobile from, Item item)
		{
			int messageColor = HarvestConstants.MSG_COLOR_SUCCESS;
			
			// Granite messages
			if (item is Granite)
			{
				from.SendMessage(messageColor, HarvestStringConstants.MSG_FOUND_GRANITE);
			}
			else if (item is DullCopperGranite)
			{
				from.SendMessage(messageColor, HarvestStringConstants.MSG_FOUND_DULL_COPPER_GRANITE);
			}
			else if (item is ShadowIronGranite)
			{
				from.SendMessage(messageColor, HarvestStringConstants.MSG_FOUND_SHADOW_IRON_GRANITE);
			}
			else if (item is CopperGranite)
			{
				from.SendMessage(messageColor, HarvestStringConstants.MSG_FOUND_COPPER_GRANITE);
			}
			else if (item is BronzeGranite)
			{
				from.SendMessage(messageColor, HarvestStringConstants.MSG_FOUND_BRONZE_GRANITE);
			}
			else if (item is PlatinumGranite)
			{
				from.SendMessage(messageColor, HarvestStringConstants.MSG_FOUND_PLATINUM_GRANITE);
			}
			else if (item is GoldGranite)
			{
				from.SendMessage(messageColor, HarvestStringConstants.MSG_FOUND_GOLD_GRANITE);
			}
			else if (item is AgapiteGranite)
			{
				from.SendMessage(messageColor, HarvestStringConstants.MSG_FOUND_AGAPITE_GRANITE);
			}
			else if (item is VeriteGranite)
			{
				from.SendMessage(messageColor, HarvestStringConstants.MSG_FOUND_VERITE_GRANITE);
			}
			else if (item is ValoriteGranite)
			{
				from.SendMessage(messageColor, HarvestStringConstants.MSG_FOUND_VALORITE_GRANITE);
			}
			else if (item is TitaniumGranite)
			{
				from.SendMessage(messageColor, HarvestStringConstants.MSG_FOUND_TITANIUM_GRANITE);
			}
			else if (item is RoseniumGranite)
			{
				from.SendMessage(messageColor, HarvestStringConstants.MSG_FOUND_ROSENIUM_GRANITE);
			}
			else if (item is NepturiteGranite)
			{
				from.SendMessage(messageColor, HarvestStringConstants.MSG_FOUND_NEPTURITE_GRANITE);
			}
		}

		/// <summary>
		/// Calculates the amount of items to harvest based on region and bonuses
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="def">The harvest definition</param>
		/// <param name="bank">The harvest bank</param>
		/// <param name="item">The item being harvested</param>
		/// <param name="reg">The region</param>
		/// <param name="map">The map</param>
		/// <param name="loc">The location</param>
		/// <returns>The calculated amount</returns>
		private int CalculateItemAmount(Mobile from, HarvestDefinition def, HarvestBank bank, Item item, Region reg, Map map, Point3D loc)
		{
			int amount = def.ConsumedPerHarvest;
			int feluccaAmount = def.ConsumedPerFeluccaHarvest;

			// Blank scroll special calculation
			if (item is BlankScroll)
			{
				amount = Utility.RandomMinMax(amount, (int)(amount + (from.Skills[SkillName.Inscribe].Value / HarvestConstants.INSCRIBE_SKILL_DIVISOR)));
				from.SendMessage(HarvestConstants.MSG_COLOR_ERROR, HarvestStringConstants.MSG_FOUND_BLANK_SCROLLS);
				from.PlaySound(from.Female ? HarvestConstants.SOUND_SUCCESS_FEMALE : HarvestConstants.SOUND_SUCCESS_MALE);
				from.Say(HarvestStringConstants.EMOTE_SUCCESS);
				return amount;
			}

			HarvestBonusRegion bonusRegion = HarvestRegionHelper.GetBonusRegion(from, reg, map, loc);

			// Isles of Dread - use felucca amount
			if (bonusRegion == HarvestBonusRegion.IslesOfDread && bank.Current >= feluccaAmount)
			{
				return feluccaAmount;
			}
			// Mines of Morinia - 3x multiplier for ore
			else if (bonusRegion == HarvestBonusRegion.MinesOfMorinia && item is BaseOre && 
				Utility.RandomMinMax(HarvestConstants.MINES_OF_MORINIA_CHANCE_MIN, HarvestConstants.MINES_OF_MORINIA_CHANCE_MAX) > 1)
			{
				return HarvestConstants.MINES_OF_MORINIA_MULTIPLIER * amount;
			}

			return amount;
		}

		/// <summary>
		/// Detects if special ore/granite/log resources were found
		/// </summary>
		/// <param name="item">The item found</param>
		/// <param name="findSpecialOre">Output: true if special ore found</param>
		/// <param name="findSpecialGranite">Output: true if special granite found</param>
		/// <param name="findBlackLog">Output: true if black log found</param>
		/// <param name="findToughLog">Output: true if tough log found</param>
		private void DetectSpecialResources(Item item, out bool findSpecialOre, out bool findSpecialGranite, out bool findBlackLog, out bool findToughLog)
		{
			findSpecialOre = false;
			findSpecialGranite = false;
			findBlackLog = false;
			findToughLog = false;

			if ((item is AgapiteOre || item is VeriteOre || item is ValoriteOre || item is TitaniumOre || item is RoseniumOre) &&
				Utility.RandomMinMax(HarvestConstants.SPECIAL_RESOURCE_CHANCE_MIN, HarvestConstants.SPECIAL_RESOURCE_CHANCE_MAX) == 1)
			{
				findSpecialOre = true;
			}

			if ((item is AgapiteGranite || item is VeriteGranite || item is ValoriteGranite || item is TitaniumGranite || item is RoseniumGranite) &&
				Utility.RandomMinMax(HarvestConstants.SPECIAL_RESOURCE_CHANCE_MIN, HarvestConstants.SPECIAL_RESOURCE_CHANCE_MAX) == 1)
			{
				findSpecialGranite = true;
			}

			if ((item is AshLog) || (item is EbonyLog))
			{
				findBlackLog = true;
			}

			if (!(item is Log))
			{
				findToughLog = true;
			}
		}

		/// <summary>
		/// Applies region-specific bonuses and conversions
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="item">The item reference (may be modified)</param>
		/// <param name="reg">The region</param>
		/// <param name="map">The map</param>
		/// <param name="loc">The location</param>
		/// <param name="findSpecialOre">Whether special ore was detected</param>
		/// <param name="findSpecialGranite">Whether special granite was detected</param>
		/// <param name="findBlackLog">Whether black log was detected</param>
		/// <param name="skillValue">The skill value</param>
		/// <param name="resource">The harvest resource</param>
		/// <returns>The modified item (may be different from input)</returns>
		private Item ApplyRegionBonuses(Mobile from, Item item, Region reg, Map map, Point3D loc, bool findSpecialOre, bool findSpecialGranite, bool findBlackLog, double skillValue, HarvestResource resource)
		{
			HarvestBonusRegion bonusRegion = HarvestRegionHelper.GetBonusRegion(from, reg, map, loc);

			// Necromancer region ebony bonus
			if (bonusRegion == HarvestBonusRegion.NecromancerRegion && findBlackLog && item is BaseLog)
			{
				if (skillValue >= resource.ReqSkill)
				{
					Container pack = from.Backpack;
					Item bonusItem = new EbonyLog(1);
					from.AddToBackpack(bonusItem);
					from.SendMessage(HarvestConstants.MSG_COLOR_SUCCESS, HarvestStringConstants.MSG_BONUS_EBONY_WOOD);
				}
			}
			// Sea regions nepturite conversion
			else if (HarvestRegionHelper.IsSeaRegion(bonusRegion))
			{
				if (findSpecialGranite && item is BaseGranite)
				{
					int nepturiteGranite = item.Amount;
					item.Delete();
					item = new NepturiteGranite(nepturiteGranite);
					from.SendMessage(HarvestConstants.MSG_COLOR_ERROR, HarvestStringConstants.MSG_FOUND_NEPTURITE_GRANITE);
				}
				else if (findSpecialOre && item is BaseOre)
				{
					int nepturiteOre = item.Amount;
					item.Delete();
					item = new NepturiteOre(nepturiteOre);
					from.SendMessage(HarvestConstants.MSG_COLOR_ERROR, HarvestStringConstants.MSG_FOUND_NEPTURITE_ORE);
				}
			}

			return item;
		}

		/// <summary>
		/// Handles world-specific bonuses (coal, zinc)
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="map">The map</param>
		/// <param name="loc">The location</param>
		/// <param name="worldName">The cached world name (performance optimization)</param>
		private void HandleWorldBonuses(Mobile from, Map map, Point3D loc, string worldName)
		{
			if (worldName == "the Savaged Empire" && from.Skills[SkillName.Mining].Value > Utility.RandomMinMax(HarvestConstants.COAL_ZINC_SKILL_CHECK_MIN, HarvestConstants.COAL_ZINC_SKILL_CHECK_MAX))
			{
				Container pack = from.Backpack;
				DugUpCoal coal = new DugUpCoal(Utility.RandomMinMax(HarvestConstants.COAL_ZINC_AMOUNT_MIN, HarvestConstants.COAL_ZINC_AMOUNT_MAX));
				from.AddToBackpack(coal);
				from.SendMessage(HarvestConstants.MSG_COLOR_ERROR, HarvestStringConstants.MSG_FOUND_COAL);
			}
			else if (worldName == "the Island of Umber Veil" && from.Skills[SkillName.Mining].Value > Utility.RandomMinMax(HarvestConstants.COAL_ZINC_SKILL_CHECK_MIN, HarvestConstants.COAL_ZINC_SKILL_CHECK_MAX))
			{
				Container pack = from.Backpack;
				DugUpZinc zinc = new DugUpZinc(Utility.RandomMinMax(HarvestConstants.COAL_ZINC_AMOUNT_MIN, HarvestConstants.COAL_ZINC_AMOUNT_MAX));
				from.AddToBackpack(zinc);
				from.SendMessage(HarvestConstants.MSG_COLOR_ERROR, HarvestStringConstants.MSG_FOUND_ZINC);
			}
		}

		/// <summary>
		/// Handles special items (books, scrolls)
		/// </summary>
		/// <param name="from">The mobile performing the harvest</param>
		/// <param name="item">The item found</param>
		private void HandleSpecialItems(Mobile from, Item item)
		{
			if (item is BlueBook || 
				item is LoreBook || 
				item is DDRelicBook || 
				item is MyNecromancerSpellbook || 
				item is MySpellbook || 
				item is MyNinjabook || 
				item is MySamuraibook || 
				item is MyPaladinbook || 
				item is MySongbook || 
				item is ArtifactManual)
			{
				from.SendMessage(HarvestConstants.MSG_COLOR_ERROR, HarvestStringConstants.MSG_FOUND_BOOK);
				from.PlaySound(from.Female ? HarvestConstants.SOUND_SUCCESS_FEMALE : HarvestConstants.SOUND_SUCCESS_MALE);
				from.Say(HarvestStringConstants.EMOTE_SUCCESS);
				
				if (item is DDRelicBook)
				{
					((DDRelicBook)item).RelicGoldValue = ((DDRelicBook)item).RelicGoldValue + Utility.RandomMinMax(1, (int)(from.Skills[SkillName.Inscribe].Value * HarvestConstants.INSCRIBE_SKILL_MULTIPLIER));
				}
				else if (item is BlueBook)
				{
					item.Name = HarvestStringConstants.ITEM_NAME_BOOK;
					item.Hue = RandomThings.GetRandomColor(0);
					item.ItemID = RandomThings.GetRandomBookItemID();
				}
			}
			else if (item is SomeRandomNote || 
				item is ScrollClue || 
				item is LibraryScroll1 || 
				item is LibraryScroll2 || 
				item is LibraryScroll3 || 
				item is LibraryScroll4 || 
				item is LibraryScroll5 || 
				item is LibraryScroll6 || 
				item is DDRelicScrolls)
			{
				from.SendMessage(HarvestConstants.MSG_COLOR_ERROR, HarvestStringConstants.MSG_FOUND_SCROLL);
				from.PlaySound(from.Female ? HarvestConstants.SOUND_SUCCESS_FEMALE : HarvestConstants.SOUND_SUCCESS_MALE);
				from.Say(HarvestStringConstants.EMOTE_SUCCESS);
				
				if (item is DDRelicScrolls)
				{
					((DDRelicScrolls)item).RelicGoldValue = ((DDRelicScrolls)item).RelicGoldValue + Utility.RandomMinMax(1, (int)(from.Skills[SkillName.Inscribe].Value * HarvestConstants.INSCRIBE_SKILL_MULTIPLIER));
				}
			}
		}

		#endregion
	}
}

namespace Server
{
	public interface IChopable
	{
		void OnChop( Mobile from );
	}

	[AttributeUsage( AttributeTargets.Class )]
	public class FurnitureAttribute : Attribute
	{
		public static bool Check( Item item )
		{
			return ( item != null && item.GetType().IsDefined( typeof( FurnitureAttribute ), false ) );
		}

		public FurnitureAttribute()
		{
		}
	}
}

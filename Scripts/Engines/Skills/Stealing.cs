using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using Server.Spells.Necromancy;
using Server.Spells;
using Server.Spells.Ninjitsu;
using Server.Misc;
using System.Reflection;
using System.Text;
using Server.Regions;
using Felladrin.Automations;

namespace Server.SkillHandlers
{
	/// <summary>
	/// Handles the Stealing skill functionality for taking items from containers and mobiles.
	/// </summary>
	public class Stealing
	{
		#region Fields

		/// <summary>Set of mobiles that are currently guarding their possessions. Uses HashSet for O(1) lookup performance.</summary>
		static HashSet<Mobile> stealingtargets = new HashSet<Mobile>();

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes the stealing skill handler.
		/// </summary>
		public static void Initialize()
		{
			SkillInfo.Table[ThieveryConstants.STEALING_SKILL_ID].Callback = new SkillUseCallback(OnUse);
		}

		/// <summary>Classic mode flag (currently disabled)</summary>
		public static readonly bool ClassicMode = false;

		/// <summary>Suspend on murder flag (currently disabled)</summary>
		public static readonly bool SuspendOnMurder = false;

		#endregion

		#region Validation

		/// <summary>
		/// Checks if a mobile is in the Thieves Guild.
		/// </summary>
		/// <param name="m">The mobile to check</param>
		/// <returns>True if mobile is in Thieves Guild</returns>
		public static bool IsInGuild(Mobile m)
		{
			return (m is PlayerMobile && ((PlayerMobile)m).NpcGuild == NpcGuild.ThievesGuild);
		}

		/// <summary>
		/// Checks if one mobile is innocent to another.
		/// </summary>
		/// <param name="from">The mobile to check from</param>
		/// <param name="to">The mobile to check to</param>
		/// <returns>True if from is innocent to to</returns>
		public static bool IsInnocentTo(Mobile from, Mobile to)
		{
			return (Notoriety.Compute(from, (Mobile)to) == Notoriety.Innocent);
		}

		#endregion

		#region Target Management

		/// <summary>
		/// Checks if a target is currently guarding their possessions, or adds them to the guard list.
		/// </summary>
		/// <param name="target">The mobile to check or add</param>
		/// <param name="check">If true, adds target to guard list. If false, checks if target is guarded.</param>
		/// <returns>True if target is not guarded (can proceed), false if guarded or being added to list</returns>
		public static bool CheckStealingTarget(Mobile target, bool check)
		{
			return ThieveryHelper.CheckTarget(ref stealingtargets, target, check);
		}

		/// <summary>
		/// Clears the list of guarded targets.
		/// </summary>
		public static void WipeStealingList()
		{
			ThieveryHelper.WipeTargetList(ref stealingtargets);
		}

		#endregion

		private class StealingTarget : Target
		{
			private Mobile m_Thief;

			public StealingTarget( Mobile thief ) : base ( 1, false, TargetFlags.None )
			{
				m_Thief = thief;

				AllowNonlocal = true;
			}

			private Item TryStealItem( Item toSteal, ref bool caught, Mobile target )
			{
				Item stolen = null;

				object root = toSteal.RootParent;

				bool isstealbase = false;
				if ( toSteal is AddonComponent || toSteal is StealBase || (toSteal is StealBox) || (root is StealBase) || (root is StealBox) || toSteal.Parent is StealBase || toSteal.Parent is StealBox)
					isstealbase = true;

				StealableArtifactsSpawner.StealableInstance si = null;
				if ( (toSteal.Parent == null || !toSteal.Movable) && !isstealbase)
					si = StealableArtifactsSpawner.GetStealableInstance( toSteal );

				/// WIZARD WANTS THEM TO BE ABLE TO STEAL THE DUNGEON CHESTS ///
				if ( toSteal is DungeonChest )
				{
					DungeonChest dBox = (DungeonChest)toSteal;

					if (!ThieveryHelper.CheckBlessedState(m_Thief, "roubar"))
					{
						// Error message sent by helper
					}
					else if (dBox.ItemID == ThieveryConstants.DUNGEON_CHEST_ID_1 || 
					         dBox.ItemID == ThieveryConstants.DUNGEON_CHEST_ID_2 || 
					         dBox.ItemID == ThieveryConstants.DUNGEON_CHEST_ID_3 || 
					         dBox.ItemID == ThieveryConstants.DUNGEON_CHEST_ID_4 || 
					         (dBox.ItemID >= ThieveryConstants.DUNGEON_CHEST_ID_RANGE_START && dBox.ItemID <= ThieveryConstants.DUNGEON_CHEST_ID_RANGE_END) || 
					         (dBox.ItemID >= ThieveryConstants.DUNGEON_CHEST_ID_RANGE_START_2 && dBox.ItemID <= ThieveryConstants.DUNGEON_CHEST_ID_RANGE_END_2))
					{
						m_Thief.SendMessage(ThieveryStringConstants.MSG_LEAVE_DEAD);
					}
					else if (dBox.ItemID == ThieveryConstants.BROKEN_GOLEM_ID_1 || dBox.ItemID == ThieveryConstants.BROKEN_GOLEM_ID_2)
					{
						m_Thief.SendMessage(ThieveryStringConstants.MSG_NO_USE_BROKEN_GOLEM);
					}
					else
					{
						if (m_Thief.CheckSkill(SkillName.Stealing, ThieveryConstants.SKILL_CHECK_MIN, ThieveryConstants.SKILL_CHECK_VERY_HIGH))
						{
							m_Thief.SendMessage( "Você despeja todo o conteúdo enquanto rouba o item." );
							StolenChest sBox = new StolenChest();
							int dValue = 0;

							dValue = (dBox.ContainerLevel + 1) * ThieveryConstants.DUNGEON_CHEST_VALUE_MULTIPLIER;
							sBox.ContainerID = dBox.ContainerID;
							sBox.ContainerGump = dBox.ContainerGump;
							sBox.ContainerHue = dBox.ContainerHue;
							sBox.ContainerFlip = dBox.ContainerFlip;
							sBox.ContainerWeight = dBox.ContainerWeight;
							sBox.ContainerName = dBox.ContainerName;

							sBox.ContainerValue = dValue;

							Item iBox = (Item)sBox;

							iBox.ItemID = sBox.ContainerID;
							iBox.Hue = sBox.ContainerHue;
							iBox.Weight = sBox.ContainerWeight;
							iBox.Name = sBox.ContainerName;

							Bag oBox = (Bag)iBox;

							oBox.GumpID = sBox.ContainerGump;

							m_Thief.AddToBackpack( oBox );

							Titles.AwardFame( m_Thief, dValue, true );

							LoggingFunctions.LogStandard( m_Thief, "roubou um " + iBox.Name + "" );
						}
						else
						{
							m_Thief.SendMessage(ThieveryStringConstants.MSG_NOT_QUICK_ENOUGH);
							m_Thief.RevealingAction(); // REVEALING ONLY WHEN FAILED
						}

						Item spawnBox = new DungeonChestSpawner( dBox.ContainerLevel, (double)(Utility.RandomMinMax( ThieveryConstants.RANDOM_RANGE_DUNGEON_CHEST_MIN, ThieveryConstants.RANDOM_RANGE_DUNGEON_CHEST_MAX )) );
						spawnBox.MoveToWorld (new Point3D(dBox.X, dBox.Y, dBox.Z), dBox.Map);

						toSteal.Delete();
					}
				}
				else if (toSteal is LandChest)
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_LEAVE_DEAD);
				}
				else if (!ThieveryHelper.CheckBlessedState(m_Thief, "fazer isso"))
				{
					// Error message sent by helper
				}
				else if (toSteal is SunkenShip)
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_NOT_STRONG_ENOUGH);
				}
				else if (!IsEmptyHanded(m_Thief))
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_CANNOT_WIELD_WEAPON);
				}
				else if (root is Mobile && ((Mobile)root).Player && IsInnocentTo(m_Thief, (Mobile)root) && !IsInGuild(m_Thief))
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_MUST_BE_IN_GUILD);
				}
				else if ( toSteal is Coffer )
				{

					if (m_Thief is PlayerMobile && ((PlayerMobile)m_Thief).BalanceStatus > 0)
						m_Thief.SendMessage(ThieveryStringConstants.MSG_WOULDNT_BE_RIGHT);

					Coffer coffer = (Coffer)toSteal;
					bool Pilfer = true;

					if ( m_Thief.Backpack.FindItemByType( typeof ( ThiefNote ) ) != null )
					{
						Item mail = m_Thief.Backpack.FindItemByType( typeof ( ThiefNote ) );
						ThiefNote envelope = (ThiefNote)mail;

						if ( envelope.NoteOwner == m_Thief )
						{
							if ( envelope.NoteItemArea == Server.Misc.Worlds.GetRegionName( m_Thief.Map, m_Thief.Location ) && envelope.NoteItemGot == 0 && envelope.NoteItemCategory == coffer.CofferType )
							{
								envelope.NoteItemGot = 1;
								m_Thief.LocalOverheadMessage(MessageType.Emote, ThieveryConstants.MESSAGE_COLOR_EMOTE, true, string.Format(ThieveryStringConstants.MSG_FOUND_QUEST_ITEM_FORMAT, envelope.NoteItem));
								m_Thief.SendSound( 0x3D );
								envelope.InvalidateProperties();
								Pilfer = false;
							}
						}
						else
							m_Thief.SendMessage(ThieveryStringConstants.MSG_STOLE_WRONG_QUEST_ITEM);
					}

					if ( Pilfer )
					{
						if (coffer.CofferGold < 1)
						{
							m_Thief.SendMessage(ThieveryStringConstants.MSG_NO_GOLD_IN_COFFER);
						}
						else if (m_Thief.CheckSkill(SkillName.Stealing, Utility.RandomMinMax(ThieveryConstants.STEALING_SKILL_CHECK_RANGE_LOW_MIN, ThieveryConstants.STEALING_SKILL_CHECK_RANGE_LOW_MAX), (int)ThieveryConstants.SNOOPING_SKILL_CHECK_MAX))
						{
							m_Thief.SendMessage(string.Format(ThieveryStringConstants.MSG_STOLE_GOLD_FROM_COFFER_FORMAT, coffer.CofferGold));
							m_Thief.SendSound( 0x2E6 );
							m_Thief.AddToBackpack ( new Gold( coffer.CofferGold ) );

							Titles.AwardFame(m_Thief, (int)((double)coffer.CofferGold / ThieveryConstants.COFFER_GOLD_FAME_DIVISOR), true);
							Titles.AwardKarma(m_Thief, -((int)((double)coffer.CofferGold / ThieveryConstants.COFFER_GOLD_KARMA_DIVISOR)), true);

							coffer.CofferRobbed = 1;
							coffer.CofferRobber = m_Thief.Name + " the " + Server.Misc.GetPlayerInfo.GetSkillTitle( m_Thief );
							coffer.CofferGold = 0;

							LoggingFunctions.LogStandard( m_Thief, "roubou " + coffer.CofferGold + " ouros de um " + coffer.CofferType + " em " + Server.Misc.Worlds.GetRegionName( m_Thief.Map, m_Thief.Location ) + "" );
						}
						else
						{
							m_Thief.SendMessage(ThieveryStringConstants.MSG_FINGERS_SLIP);
							m_Thief.RevealingAction(); // REVEALING ONLY WHEN FAILED +++
							m_Thief.CriminalAction(true);

							if (!m_Thief.CheckSkill(SkillName.Snooping, Utility.RandomMinMax(ThieveryConstants.STEALING_SKILL_CHECK_RANGE_MEDIUM_MIN, ThieveryConstants.STEALING_SKILL_CHECK_RANGE_MEDIUM_MAX), ThieveryConstants.SKILL_CHECK_VERY_HIGH))
							{
								List<Mobile> spotters = new List<Mobile>();
								foreach (Mobile m in m_Thief.GetMobilesInRange(ThieveryConstants.SPOTTER_DETECTION_RANGE))
								{
									//if ( m is BaseBlue && m.CanSee( m_Thief ) && m.InLOS( m_Thief ) )
									if ( m is BaseBlue || m is BaseVendor )
									{
										m_Thief.CriminalAction( false );
										((BaseCreature)m).FocusMob = m_Thief;
										m.Combatant = m_Thief;
										m.PublicOverheadMessage(MessageType.Regular, 0, false, ThieveryStringConstants.MSG_STOP_THIEF); 
										//((PlayerMobile)m_Thief).flagged = true;
									}

								}
							}
						}
					}
				}
				else if ( isstealbase )
				{
					if (!(toSteal is AddonComponent ) )
						return null;

					BaseAddon pedestal = (BaseAddon)((AddonComponent)toSteal).Addon;

					if (pedestal == null || !(pedestal is StealBase))
						return null;

					if ( m_Thief.Backpack.FindItemByType( typeof ( ThiefNote ) ) != null && m_Thief.CheckSkill( SkillName.Stealing, Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_THIEF_NOTE_MIN, ThieveryConstants.RANDOM_RANGE_THIEF_NOTE_MAX), ThieveryConstants.SKILL_CHECK_THIEF_NOTE ) )
					{
						Item mail = m_Thief.Backpack.FindItemByType( typeof ( ThiefNote ) );
						ThiefNote envelope = (ThiefNote)mail;

						if ( envelope.NoteOwner == m_Thief )
						{
							if ( envelope.NoteItemArea == Server.Misc.Worlds.GetRegionName( m_Thief.Map, m_Thief.Location ) && envelope.NoteItemGot == 0 )
							{
								envelope.NoteItemGot = 1;
								m_Thief.LocalOverheadMessage(MessageType.Emote, ThieveryConstants.MESSAGE_COLOR_EMOTE, true, string.Format(ThieveryStringConstants.MSG_FOUND_QUEST_ITEM_FORMAT, envelope.NoteItem));
								m_Thief.SendSound( 0x3D );
								m_Thief.CloseGump( typeof( Server.Items.ThiefNote.NoteGump ) );
								envelope.InvalidateProperties();
							}
						}
					}
					else if (m_Thief.Skills[SkillName.Stealing].Value < ThieveryConstants.SKILL_CHECK_HIGH)
						((StealBase)pedestal).DoDamage(m_Thief);
					else if (m_Thief.CheckSkill(SkillName.Stealing, Utility.RandomMinMax(ThieveryConstants.SKILL_CHECK_HIGH, ThieveryConstants.SKILL_CHECK_VERY_HIGH)))
					{
						double difficulty = (double)Misc.MyServerSettings.GetDifficultyLevel( m_Thief.Location, m_Thief.Map );

						if (difficulty == 0 ) // divide by zero check
							difficulty = 1;

						double chance = 1 / difficulty; //difficulty is 0-5, so harder dungeons will be harder to steal from
						if (Utility.RandomDouble() < chance)
							((StealBase)pedestal).SuccessGet( m_Thief );
						else
							((StealBase)pedestal).DoDamage( m_Thief );
					}
					else 
						((StealBase)pedestal).DoDamage( m_Thief );

				}
				else if (root is BaseVendor && ((BaseVendor)root).IsInvulnerable)
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_CANNOT_STEAL_SHOPKEEPERS);
				}
				else if (root is PlayerVendor || root is PlayerBarkeeper)
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_CANNOT_STEAL_VENDORS);
				}
				else if (!m_Thief.CanSee(toSteal) && root != null)
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_TARGET_NOT_VISIBLE);
				}
				else if (m_Thief.Backpack == null || !m_Thief.Backpack.CheckHold(m_Thief, toSteal, false, true))
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_BACKPACK_FULL);
				}
				else if (si == null && (toSteal.Parent == null && !toSteal.Movable) && !isstealbase)
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_CANNOT_STEAL_THAT);
				}
				else if ((toSteal.LootType == LootType.Newbied || toSteal.CheckBlessed(root)))
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_CANNOT_STEAL_THAT);
				}
				else if (Core.AOS && si == null && toSteal is Container && !isstealbase)
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_CANNOT_STEAL_THAT);
				}
				else if (!m_Thief.InRange(toSteal.GetWorldLocation(), ThieveryConstants.CONTAINER_RANGE) && toSteal.Parent != null)
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_MUST_BE_NEXT_TO_ITEM);
				}
				else if (si != null && m_Thief.Skills[SkillName.Stealing].Value < ThieveryConstants.STEALABLE_ARTIFACT_MIN_SKILL)
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_NOT_SKILLED_ENOUGH);
				}
				else if (toSteal.Parent is Mobile)
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_CANNOT_STEAL_EQUIPPED);
				}
				else if (root == m_Thief)
				{
					m_Thief.SendMessage(ThieveryStringConstants.MSG_CATCH_SELF);
				}
				else if ( root is Mobile && ((Mobile)root).AccessLevel > AccessLevel.Player )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else if ( root is Mobile && !m_Thief.CanBeHarmful( (Mobile)root ) )
				{
				}
				else if ( root is Corpse )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else
				{
					double w = toSteal.Weight + toSteal.TotalWeight;
					double w2 = 0;
					if (m_Thief is PlayerMobile)
						w2 = (w / ThieveryConstants.WEIGHT_DIVISOR) * (1 - ((PlayerMobile)m_Thief).Agility());

					if (m_Thief is BaseCreature)
						w2 = (w / ThieveryConstants.WEIGHT_DIVISOR) * (1 - ((BaseCreature)m_Thief).Agility());

					if (isstealbase)
					{

					}
					if (w > ThieveryConstants.MAX_STEALABLE_WEIGHT)
					{
						m_Thief.SendMessage(ThieveryStringConstants.MSG_TOO_HEAVY);
					}
					else if (AdventuresFunctions.IsInMidland((object)m_Thief) && (m_Thief is PlayerMobile || m_Thief is BaseCreature) && Utility.RandomDouble() < w2 )
					{
						if (m_Thief is PlayerMobile)
							m_Thief.SendMessage(ThieveryStringConstants.MSG_FUMBLE_ATTEMPT);

						caught = true;
					}
					else
					{
						if ( toSteal.Stackable && toSteal.Amount > 1 )
						{
							int maxAmount = (int)((m_Thief.Skills[SkillName.Stealing].Value / ThieveryConstants.WEIGHT_CALCULATION_DIVISOR) / toSteal.Weight);

							if ( maxAmount < 1 )
								maxAmount = 1;
							else if ( maxAmount > toSteal.Amount )
								maxAmount = toSteal.Amount;

							int amount = Utility.RandomMinMax( 1, maxAmount );

							if ( amount >= toSteal.Amount )
							{
								int pileWeight = (int)Math.Ceiling(toSteal.Weight * toSteal.Amount);
								pileWeight *= ThieveryConstants.WEIGHT_MULTIPLIER;

								if (m_Thief.CheckTargetSkill(SkillName.Stealing, toSteal, pileWeight - (int)ThieveryConstants.WEIGHT_SKILL_OFFSET_MIN, pileWeight + (int)ThieveryConstants.WEIGHT_SKILL_OFFSET_MAX))
									stolen = toSteal;
							}
							else
							{
								int pileWeight = (int)Math.Ceiling(toSteal.Weight * amount);
								pileWeight *= ThieveryConstants.WEIGHT_MULTIPLIER;

								if (m_Thief.CheckTargetSkill(SkillName.Stealing, toSteal, pileWeight - (int)ThieveryConstants.WEIGHT_SKILL_OFFSET_MIN, pileWeight + (int)ThieveryConstants.WEIGHT_SKILL_OFFSET_MAX))
								{
									stolen = Mobile.LiftItemDupe( toSteal, toSteal.Amount - amount );

									if ( stolen == null )
										stolen = toSteal;
								}
							}
						}
						else
						{
							int iw = (int)Math.Ceiling(w);
							iw *= ThieveryConstants.WEIGHT_MULTIPLIER;

							if (m_Thief.CheckTargetSkill(SkillName.Stealing, toSteal, iw - (int)ThieveryConstants.WEIGHT_SKILL_OFFSET_MIN, iw + (int)ThieveryConstants.WEIGHT_SKILL_OFFSET_MAX))
								stolen = toSteal;
						}

						if (stolen != null)
						{
							m_Thief.SendMessage(ThieveryStringConstants.MSG_SUCCESSFULLY_STOLE);

							if (target != null)
							{
								if (target.Karma > 0)
									Titles.AwardKarma(m_Thief, -(int)(target.Karma / ThieveryConstants.KARMA_DIVISOR), true);
								else
									Titles.AwardKarma(m_Thief, (int)(target.Karma / ThieveryConstants.KARMA_DIVISOR), true);
							}
							else
								Titles.AwardKarma(m_Thief, ThieveryConstants.STEALING_KARMA_PENALTY_BASE, true);
							
							if ( si != null )
							{
								toSteal.Movable = true;
								si.Item = null;
							}
						}
						else
						{
							m_Thief.SendMessage(ThieveryStringConstants.MSG_FAILED_TO_STEAL);
							m_Thief.RevealingAction(); // REVEALING ONLY WHEN FAILED
						}

						caught = (m_Thief.Skills[SkillName.Stealing].Value < Utility.Random(ThieveryConstants.STEALING_SKILL_CHECK_MAX));
					}
				}

				if (AdventuresFunctions.IsInMidland((object)m_Thief) && caught && m_Thief is PlayerMobile)
				{
					if (root is BaseCreature && ((BaseCreature)root).midrace > 0 )
						((PlayerMobile)m_Thief).AdjustReputation( Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_REPUTATION_MIN, ThieveryConstants.RANDOM_RANGE_REPUTATION_MAX), ((BaseCreature)root).midrace, false);
				}

				return stolen;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				//from.RevealingAction(); // NO REVEALING ON THIS SERVER

				Item stolen = null;
				object root = null;
				bool caught = false;

				if ( target is Item )
				{

					root = ((Item)target).RootParent;

					if (root is PlayerMobile)
						((PlayerMobile)from).flagged = true;

					stolen = TryStealItem( (Item)target, ref caught, null );
				} 
				else if ( target is Mobile )
				{

					Mobile mobs = target as Mobile;
					if (mobs.Region.IsPartOf( typeof( PublicRegion ) ))
						return;

					if (from.Region.IsPartOf(typeof(HouseRegion)) && !mobs.Region.IsPartOf(typeof(HouseRegion)))
					{
						from.SendMessage(ThieveryStringConstants.MSG_EXPLOITING_FORBIDDEN);
						if (from.Hits > ThieveryConstants.MIN_HITS_REQUIRED)
							from.Hits -= from.Hits -5;
						else
							from.Kill();
					}

					if (target is PlayerMobile)
					{
						((PlayerMobile)from).flagged = true;

					if (from is PlayerMobile && ((PlayerMobile)from).BalanceStatus > 0 && ((Mobile)target).Karma >= 0)
						from.SendMessage(ThieveryStringConstants.MSG_WOULDNT_BE_RIGHT_GOOD);
					}

					Container pack = ((Mobile)target).Backpack;
					
					double odds = (from.Skills[SkillName.Stealing].Value / ThieveryConstants.STEALING_SKILL_CHECK_MAX) * (1 - ((Math.Abs(((Mobile)target).Fame) / ThieveryConstants.FAME_ODDS_DIVISOR))) * (1 + (from.RawDex / ThieveryConstants.STEALING_SKILL_CHECK_MAX));

					if (odds > 1)
						odds = ThieveryConstants.PROBABILITY_NEAR_CERTAIN;
					if (odds < ThieveryConstants.PROBABILITY_LOW)
						odds = ThieveryConstants.PROBABILITY_LOW;
					
					if ( pack != null && pack.Items.Count > 0 ) 
					{
							int randomIndex = Utility.Random( pack.Items.Count );

							root = target;

							if (target is PlayerMobile)
								((PlayerMobile)from).flagged = true;
								
							stolen = TryStealItem( pack.Items[randomIndex], ref caught, null );
					
					}
					else if (target is BaseCreature && Utility.RandomDouble() < odds && !(((Mobile)target).Blessed) && ((Mobile)target).Fame != 0 && !(target is CloneCharacterOnLogout.CharacterClone) ) // player targetting a basecreature Final making it more fun to steal
					{

						if (((PlayerMobile)from).BalanceStatus > 0 && ((Mobile)target).Karma > ThieveryConstants.MIN_KARMA_THRESHOLD)
							from.SendMessage(ThieveryStringConstants.MSG_WOULDNT_BE_RIGHT_GOOD);

						if (Stealing.CheckStealingTarget( (Mobile)target, false )) // check only
						{

									Item rngitem = null;
									int reward = ((Mobile)target).Fame; 
									
									if (Math.Abs(((Mobile)target).Fame) <= ThieveryConstants.FAME_THRESHOLD_EASY)
										reward = Utility.RandomMinMax(ThieveryConstants.REWARD_EASY_MIN, ThieveryConstants.REWARD_EASY_MAX); // 100% easy
									else if (Math.Abs(((Mobile)target).Fame) <= ThieveryConstants.FAME_THRESHOLD_MEDIUM)
										reward = Utility.RandomMinMax(ThieveryConstants.REWARD_MEDIUM_MIN, ThieveryConstants.REWARD_MEDIUM_MAX); // 60% easy, 40% medium
									else if (Math.Abs(((Mobile)target).Fame) <= ThieveryConstants.FAME_THRESHOLD_HARD)
										reward = Utility.RandomMinMax(ThieveryConstants.REWARD_HARD_MIN, ThieveryConstants.REWARD_HARD_MAX); // 25% easy, 62.5% medium, 12.5% rare
									else if (Math.Abs(((Mobile)target).Fame) <= ThieveryConstants.FAME_THRESHOLD_VERY_HARD)
										reward = Utility.RandomMinMax(ThieveryConstants.REWARD_VERY_HARD_MIN, ThieveryConstants.REWARD_VERY_HARD_MAX); // 37.5 medium, 47% rare, 15% impossible
									else if (Math.Abs(((Mobile)target).Fame) <= ThieveryConstants.FAME_THRESHOLD_EXTREME)
										reward = Utility.RandomMinMax(ThieveryConstants.REWARD_EXTREME_MIN, ThieveryConstants.REWARD_EXTREME_MAX); // 70% rare, 30% impossible
									else if (Math.Abs(((Mobile)target).Fame) > ThieveryConstants.FAME_THRESHOLD_EXTREME)
										reward = Utility.RandomMinMax(ThieveryConstants.REWARD_IMPOSSIBLE_MIN, ThieveryConstants.REWARD_IMPOSSIBLE_MAX); // 40% rare, 60% impossible

									if (AdventuresFunctions.IsInMidland((object)target) && reward > ThieveryConstants.MIDLAND_REWARD_THRESHOLD)
										reward -= Utility.RandomMinMax(ThieveryConstants.MIDLAND_REWARD_REDUCTION_MIN, ThieveryConstants.MIDLAND_REWARD_REDUCTION_MAX);
									
									double luckavg = 0;
									
									if (reward <= ThieveryConstants.REWARD_EASY_MAX) // easy finds
									{
										switch (Utility.Random(ThieveryConstants.SWITCH_CASE_COUNT_EASY)) // 
										{
												case 0: rngitem = Loot.RandomArmorOrHatOrClothes(); break;
												case 1: rngitem = Loot.RandomNecromancyReagent(); rngitem.Amount = Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_REAGENT_MIN, ThieveryConstants.RANDOM_RANGE_REAGENT_MAX); break;
												case 2: rngitem = Loot.RandomReagent(); rngitem.Amount = Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_REAGENT_MIN, ThieveryConstants.RANDOM_RANGE_REAGENT_MAX); break;
												case 3: rngitem = Loot.RandomClothing(); break;
												case 4: rngitem = Loot.RandomGem(); rngitem.Amount = Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_REAGENT_MIN, ThieveryConstants.RANDOM_RANGE_REAGENT_MAX); break;
												case 5: rngitem = Loot.RandomPotion(); break;
												case 6: rngitem = new Arrow( Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_REAGENT_MIN, ThieveryConstants.RANDOM_RANGE_REAGENT_MAX) ); break;
												case 7: rngitem = new Arrow( Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_REAGENT_MIN, ThieveryConstants.RANDOM_RANGE_REAGENT_MAX) ); break;
												case 8: rngitem = new Gold(); rngitem.Amount = (int)((double)reward * Utility.RandomDouble()); break;
												case 9: rngitem = Loot.RandomWand(); break;
												case 10: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(1, 2) ); break;
												case 11: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(1, 2) ); break;
												case 12: rngitem = Loot.RandomInstrument(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(1, 2) ); break;
										} 
										
									}

									else if (reward <= 375) // medium finds
									{
										luckavg = (from.Luck + 400) / 2;

										switch (Utility.Random(ThieveryConstants.SWITCH_CASE_COUNT_MEDIUM)) // 
										{
												case 0: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(2, 4) ); break;
												case 1: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(2, 4) ); break;
												case 2: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(2, 4) ); break;
												case 3: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(2, 4) ); break;
												case 4: rngitem = Loot.RandomInstrument(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 4); break;
												case 5: rngitem = Loot.RandomQuiver(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 4); break;
												case 6: rngitem = Loot.RandomWand(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 4); break;
												case 7: rngitem = Loot.RandomJewelry(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 4); break;
												case 8: rngitem = new Gold(); rngitem.Amount = reward; break;
												case 9: rngitem = Loot.RandomMixerReagent(); rngitem.Amount = Utility.RandomMinMax(1, 20); break;
										}
											
									}

									else if (reward <= 470) // rare finds
									{
										luckavg = (from.Luck + 800) /2;
										
										switch ( Utility.Random( 20 ) ) // 7.5%
										{
												case 0: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(4, 8)); break;
												case 1: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(4, 8)); break;
												case 2: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(4, 8)); break;
												case 3: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(4, 8)); break;
												case 4: rngitem = Loot.RandomInstrument(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 9); break;
												case 5: rngitem = Loot.RandomQuiver(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 9); break;
												case 6: rngitem = Loot.RandomWand(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 9); break;
												case 7: rngitem = Loot.RandomJewelry(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 9); break;
												case 8: rngitem = new Gold(); rngitem.Amount = (int)((double)reward * (1+Utility.RandomDouble())); break;
												case 9: rngitem = Loot.RandomSecretReagent(); rngitem.Amount = Utility.RandomMinMax(1, 50); break;
												case 10: rngitem = Loot.RandomRelic(); break;
										}
									}
									

									else if (reward <= ThieveryConstants.REWARD_IMPOSSIBLE_MAX) // impossible finds
									{
										luckavg = (from.Luck + 1200) / 2;

										switch (Utility.Random(ThieveryConstants.SWITCH_CASE_COUNT_IMPOSSIBLE)) //
										{
												case 0: rngitem = Loot.RandomArty(); break;
												case 1: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate(from, (int)luckavg, rngitem, Utility.RandomMinMax(ThieveryConstants.MUTATION_LEVEL_VERY_HIGH_MIN, ThieveryConstants.MUTATION_LEVEL_VERY_HIGH_MAX)); break;
												case 2: rngitem = Loot.RandomInstrument(); Stealing.ItemMutate(from, (int)luckavg, rngitem, Utility.RandomMinMax(ThieveryConstants.MUTATION_LEVEL_VERY_HIGH_MIN, ThieveryConstants.MUTATION_LEVEL_VERY_HIGH_MAX)); break;
												case 3: rngitem = Loot.RandomQuiver(); Stealing.ItemMutate(from, (int)luckavg, rngitem, Utility.RandomMinMax(ThieveryConstants.MUTATION_LEVEL_VERY_HIGH_MIN, ThieveryConstants.MUTATION_LEVEL_VERY_HIGH_MAX)); break;
												case 4: rngitem = Loot.RandomWand(); Stealing.ItemMutate(from, (int)luckavg, rngitem, Utility.RandomMinMax(ThieveryConstants.MUTATION_LEVEL_VERY_HIGH_MIN, ThieveryConstants.MUTATION_LEVEL_VERY_HIGH_MAX)); break;
												case 5: rngitem = Loot.RandomJewelry(); Stealing.ItemMutate(from, (int)luckavg, rngitem, Utility.RandomMinMax(ThieveryConstants.MUTATION_LEVEL_VERY_HIGH_MIN, ThieveryConstants.MUTATION_LEVEL_VERY_HIGH_MAX)); break;
										}
									}
									
									
									if (rngitem != null)
									{
										rngitem.Movable = true;
										stolen = TryStealItem( rngitem, ref caught, (Mobile)target );
										if (stolen != null)
										{
											if (rngitem is Gold && AdventuresFunctions.IsInMidland((object)from))
											{
												if (target is BaseCreature && ((BaseCreature)target).midrace != 0)
												{
													BaseCreature ft = target as BaseCreature;
													Gold gd = rngitem as Gold;
													Item csh = null;

													if (ft.midrace == 1)
														 csh = new Sovereign( gd.Amount/10 );
													if (ft.midrace == 2)
														csh = new Drachma( gd.Amount/10 );
													if (ft.midrace == 3)
														csh = new Sslit( gd.Amount/10 ) ;
													if (ft.midrace == 4)
														csh = new Dubloon( gd.Amount/10 ) ;
													if (ft.midrace == 5)
														csh = new Skaal( gd.Amount/10 ) ;
													
													from.AddToBackpack(csh);
													stolen = csh;
													rngitem.Delete();
													
												}
												else 
													((Gold)rngitem).Amount /= 50;
											}
											if (rngitem != null)
												from.AddToBackpack( rngitem );
											
											StolenItem.Add( stolen, m_Thief, root as Mobile );
										
											Titles.AwardFame(from, (int)(Math.Abs(((Mobile)target).Fame) / ThieveryConstants.FAME_DIVISOR), true);

											if (stolen.Name != null)
												from.SendMessage(string.Format(ThieveryStringConstants.MSG_GOT_ITEM_FORMAT, stolen.Name));
											else
												from.SendMessage(ThieveryStringConstants.MSG_GOT_SOMETHING);
											stolen = null;
											
											LevelItemManager.CheckItems( from, (Mobile)target, true );
										}
											
										else if (from.AccessLevel == AccessLevel.Player) // for testing by gm's
										{
											Stealing.CheckStealingTarget( (Mobile)target, true); // user got caught?
											rngitem.Delete();
											stolen = null;
										}
									}
									else
										from.SendMessage(ThieveryStringConstants.MSG_FINGERS_NOT_QUICK);

						}
						else
							from.SendMessage(ThieveryStringConstants.MSG_ENTITY_GUARDING);
					}
					else
						from.SendMessage(ThieveryStringConstants.MSG_CANNOT_FIND_ANYTHING);

				}
				else
					from.SendMessage(ThieveryStringConstants.MSG_DONT_THINK_CAN_STEAL);

				if (stolen != null)
				{
					if (stolen.Name != null)
						from.SendMessage(string.Format(ThieveryStringConstants.MSG_GOT_ITEM_FORMAT, stolen.Name));
					else
						from.SendMessage(ThieveryStringConstants.MSG_GOT_SOMETHING);
					
					Container pack = from.Backpack;
					pack.DropItem(stolen);
					StolenItem.Add( stolen, m_Thief, root as Mobile );
				}

				if ( caught )
				{
					if ( root == null )
					{
						m_Thief.RevealingAction(); 
						m_Thief.CriminalAction( false );
					}
					else if ( root is Corpse && ((Corpse)root).IsCriminalAction( m_Thief ) )
					{
						m_Thief.RevealingAction(); // REVEALING ONLY WHEN FAILED +++
						m_Thief.CriminalAction( false );
					}
					else if ( root is Mobile )
					{
						Mobile mobRoot = (Mobile)root;

						bool isindarkmoor = false;

						if ( m_Thief.Map == Map.Ilshenar && m_Thief.X <= 1007 && m_Thief.Y <= 1280 )
							isindarkmoor = true;

						if ( 	mobRoot is PlayerMobile || 
								mobRoot is BaseVendor || 
								( mobRoot is BaseBlue && ((Mobile)m_Thief).Karma > 0 ) || 
								( isindarkmoor && ( mobRoot is Praetor || mobRoot is Honorae ) && ((Mobile)m_Thief).Karma < 0 ) ||
								( !isindarkmoor && ((Mobile)m_Thief).Karma < 0 && ((Mobile)root).Karma > 0) )
						{
							m_Thief.CriminalAction( true );
						}
						
						if (mobRoot is BaseCreature )
						{
							((BaseCreature)mobRoot).DoHarmful( m_Thief );
							if (mobRoot.Combatant == null)
								mobRoot.Combatant = m_Thief;
						}
						if (AdventuresFunctions.IsInMidland((object)mobRoot) && mobRoot is BaseCreature && ((BaseCreature)mobRoot).midrace != 0 && m_Thief is PlayerMobile)
							((PlayerMobile)m_Thief).AdjustReputation(Utility.RandomMinMax(20,100), ((BaseCreature)mobRoot).midrace, false);

						string message = string.Format(ThieveryStringConstants.MSG_NOTICE_STEALING_FORMAT, m_Thief.Name, mobRoot.Name);
						m_Thief.RevealingAction(); // REVEALING ONLY WHEN NOTICED
						Server.Items.DisguiseTimers.RemoveDisguise(m_Thief);
						foreach (NetState ns in m_Thief.GetClientsInRange(ThieveryConstants.MESSAGE_BROADCAST_RANGE))
						{
							if ( ns.Mobile != m_Thief )
								ns.Mobile.SendMessage( message );
						}
					}
				}
				else if ( root is Corpse && ((Corpse)root).IsCriminalAction( m_Thief ) )
				{
					m_Thief.CriminalAction( false );
				}

				if ( root is Mobile && ((Mobile)root).Player && m_Thief is PlayerMobile && IsInnocentTo( m_Thief, (Mobile)root ) && !IsInGuild( (Mobile)root ) )
				{
					PlayerMobile pm = (PlayerMobile)m_Thief;

					pm.PermaFlags.Add( (Mobile)root );
					pm.Delta( MobileDelta.Noto );
				}
			}
		}

		public static void ItemMutate( Mobile from, int luckChance, Item item, int level )
		{
				int attributeCount;
				int min, max;
				ContainerFunctions.GetRandomAOSStats( out attributeCount, out min, out max, level );
				attributeCount = (int)((double)attributeCount / 2);

				if ( item is BaseWeapon || item is BaseArmor || item is BaseJewel || item is BaseHat || item is BaseQuiver )
				{
					if ( item is BaseWeapon )
					{
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						Server.Misc.MorphingTime.ChangeMaterialType( item, from );
						BaseRunicTool.ApplyAttributesTo( (BaseWeapon)item, attributeCount, min, max );
					
						BaseWeapon idropped = (BaseWeapon)item;
						
						if (Utility.RandomDouble() > ThieveryConstants.PROBABILITY_HIGH)
						{
							switch (Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_DAMAGE_LEVEL_MIN, ThieveryConstants.RANDOM_RANGE_DAMAGE_LEVEL_MAX))
							{
								case 0: idropped.DamageLevel = WeaponDamageLevel.Ruin; break;
								case 1: idropped.DamageLevel = WeaponDamageLevel.Might; break;
								case 2: idropped.DamageLevel = WeaponDamageLevel.Force; break;
								case 3: idropped.DamageLevel = WeaponDamageLevel.Power; break;
								case 4: idropped.DamageLevel = WeaponDamageLevel.Vanq; break;
							}
						}
					if (Utility.RandomDouble() > ThieveryConstants.PROBABILITY_HIGH)
					{
						switch (Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_DURABILITY_LEVEL_MIN, ThieveryConstants.RANDOM_RANGE_DURABILITY_LEVEL_MAX))
							{
								case 0: idropped.DurabilityLevel = WeaponDurabilityLevel.Durable; break;
								case 1: idropped.DurabilityLevel = WeaponDurabilityLevel.Substantial; break;
								case 2: idropped.DurabilityLevel = WeaponDurabilityLevel.Massive; break;
								case 3: idropped.DurabilityLevel = WeaponDurabilityLevel.Fortified; break;
								case 4: idropped.DurabilityLevel = WeaponDurabilityLevel.Indestructible; break;
							}
						}
						if (Utility.RandomDouble() > ThieveryConstants.PROBABILITY_VERY_HIGH) { idropped.Quality = WeaponQuality.Exceptional; }
						
						Server.Misc.MorphingTime.MakeSpaceAceItem( item, from );
					
					}
					else if ( item is BaseArmor )
					{
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						Server.Misc.MorphingTime.ChangeMaterialType( item, from );
						BaseRunicTool.ApplyAttributesTo( (BaseArmor)item, attributeCount, min, max );
						
						BaseArmor idropped = (BaseArmor)item;

						if (Utility.RandomDouble() > ThieveryConstants.PROBABILITY_HIGH)
						{
							switch (Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_DAMAGE_LEVEL_MIN, ThieveryConstants.RANDOM_RANGE_DAMAGE_LEVEL_MAX))
							{
								case 0: idropped.ProtectionLevel = ArmorProtectionLevel.Defense; break;
								case 1: idropped.ProtectionLevel = ArmorProtectionLevel.Guarding; break;
								case 2: idropped.ProtectionLevel = ArmorProtectionLevel.Hardening; break;
								case 3: idropped.ProtectionLevel = ArmorProtectionLevel.Fortification; break;
								case 4: idropped.ProtectionLevel = ArmorProtectionLevel.Invulnerability; break;
							}
						}
						if (Utility.RandomDouble() > ThieveryConstants.PROBABILITY_HIGH)
						{
							switch (Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_DURABILITY_LEVEL_MIN, ThieveryConstants.RANDOM_RANGE_DURABILITY_LEVEL_MAX)) 
							{
								case 0: idropped.Durability = ArmorDurabilityLevel.Durable; break;
								case 1: idropped.Durability = ArmorDurabilityLevel.Substantial; break;
								case 2: idropped.Durability = ArmorDurabilityLevel.Massive; break;
								case 3: idropped.Durability = ArmorDurabilityLevel.Fortified; break;
								case 4: idropped.Durability = ArmorDurabilityLevel.Indestructible; break;
							}
						}
						if (Utility.RandomDouble() > ThieveryConstants.PROBABILITY_VERY_HIGH) { idropped.Quality = ArmorQuality.Exceptional; }
						
						
						Server.Misc.MorphingTime.MakeSpaceAceItem( item, from );
					}
					else if ( item is BaseJewel )
					{
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						BaseRunicTool.ApplyAttributesTo( (BaseJewel)item, attributeCount, min, max );
						Server.Misc.MorphingTime.MakeSpaceAceItem( item, from );
					}
					else if ( item is BaseQuiver )
					{
						BaseRunicTool.ApplyAttributesTo( (BaseQuiver)item, attributeCount, min, max );
						item.Name = LootPackEntry.MagicItemName( item, from, Region.Find( from.Location, from.Map ) );
					}
					else if ( item is BaseHat )
					{
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						BaseRunicTool.ApplyAttributesTo( (BaseHat)item, attributeCount, min, max );
						Server.Misc.MorphingTime.MakeSpaceAceItem( item, from );
					}
				}
				else if ( item is BaseInstrument )
				{
					if ( Server.Misc.Worlds.IsOnSpaceship( from.Location, from.Map ) )
					{
						string newName = "alienígena estranho";
						switch( Utility.RandomMinMax( 0, 6 ) )
						{
							case 0: newName = "estranho"; break;
							case 1: newName = "incomum"; break;
							case 2: newName = "bizarro"; break;
							case 3: newName = "curioso"; break;
							case 4: newName = "peculiar"; break;
							case 5: newName = "estranho"; break;
							case 6: newName = "esquisito"; break;
						}

						switch (Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_INSTRUMENT_VARIANT_MIN, ThieveryConstants.RANDOM_RANGE_INSTRUMENT_VARIANT_MAX))
						{
							case 1: item = new Pipes();		item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " flautas";		break;
							case 2: item = new Pipes();		item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " flauta de pã";	break;
							case 3: item = new Fiddle();	item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " violino";		break;
							case 4: item = new Fiddle();	item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " rabeca";		break;
						}

						BaseInstrument lute = (BaseInstrument)item;
							lute.Resource = CraftResource.None;

						item.Hue = Server.Misc.RandomThings.GetRandomColor(0);
					}
					else 
					{
						Server.Misc.MorphingTime.ChangeMaterialType( item, from );
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						item.Name = LootPackEntry.MagicItemName( item, from, Region.Find( from.Location, from.Map ) );
					}

					BaseRunicTool.ApplyAttributesTo( (BaseInstrument)item, attributeCount, min, max );
					
					if (Utility.RandomDouble() < ((double)max / 200))
						BaseRunicTool.ApplyAttributesTo( (BaseInstrument)item, attributeCount, min, max );

					SlayerName slayer = BaseRunicTool.GetRandomSlayer();

					BaseInstrument instr = (BaseInstrument)item;

					int cHue = 0;
					int cUse = 0;

					switch (instr.Resource)
					{
						case CraftResource.AshTree: cHue = MaterialInfo.GetMaterialColor("ash", "", 0); cUse = ThieveryConstants.MATERIAL_USE_ASH; break;
						case CraftResource.CherryTree: cHue = MaterialInfo.GetMaterialColor("cherry", "", 0); cUse = ThieveryConstants.MATERIAL_USE_CHERRY; break;
						case CraftResource.EbonyTree: cHue = MaterialInfo.GetMaterialColor("ebony", "", 0); cUse = ThieveryConstants.MATERIAL_USE_EBONY; break;
						case CraftResource.GoldenOakTree: cHue = MaterialInfo.GetMaterialColor("gold", "", 0); cUse = ThieveryConstants.MATERIAL_USE_GOLDEN_OAK; break;
						case CraftResource.HickoryTree: cHue = MaterialInfo.GetMaterialColor("hickory", "", 0); cUse = ThieveryConstants.MATERIAL_USE_HICKORY; break;
						/*case CraftResource.MahoganyTree: cHue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 ); cUse = 120; break;
						case CraftResource.DriftwoodTree: cHue = MaterialInfo.GetMaterialColor( "driftwood", "", 0 ); cUse = 120; break;
						case CraftResource.OakTree: cHue = MaterialInfo.GetMaterialColor( "oak", "", 0 ); cUse = 140; break;
						case CraftResource.PineTree: cHue = MaterialInfo.GetMaterialColor( "pine", "", 0 ); cUse = 160; break;*/
						case CraftResource.RosewoodTree: cHue = MaterialInfo.GetMaterialColor("rosewood", "", 0); cUse = ThieveryConstants.MATERIAL_USE_ROSEWOOD; break;
						/*case CraftResource.WalnutTree: cHue = MaterialInfo.GetMaterialColor( "walnute", "", 0 ); cUse = 200; break;*/
					}

					instr.UsesRemaining = instr.UsesRemaining + cUse;

					if ( !( Server.Misc.Worlds.IsOnSpaceship( from.Location, from.Map ) ) )
					{
						if ( cHue > 0 ){ item.Hue = cHue; }
						else if (Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_QUALITY_MIN, ThieveryConstants.RANDOM_RANGE_QUALITY_MAX) == 1) { item.Hue = RandomThings.GetRandomColor(0); }
					}

					instr.Quality = InstrumentQuality.Regular;
					if (Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_QUALITY_MIN, ThieveryConstants.RANDOM_RANGE_QUALITY_MAX) == 1) { instr.Quality = InstrumentQuality.Exceptional; }

					if (Utility.RandomMinMax(ThieveryConstants.RANDOM_RANGE_SLAYER_MIN, ThieveryConstants.RANDOM_RANGE_SLAYER_MAX) == 1) { instr.Slayer = slayer; }
				}
	
		}				


		public static bool IsEmptyHanded( Mobile from )
		{
			if ( from.FindItemOnLayer( Layer.OneHanded ) != null )
			{
				if ( from.FindItemOnLayer( Layer.OneHanded ) is BaseWeapon )
				{
					if ( 
						!( from.FindItemOnLayer( Layer.OneHanded ) is PugilistGlove ) && 
						!( from.FindItemOnLayer( Layer.OneHanded ) is PugilistGloves ) 
					)
					{
						return false;
					}
				}
			}
			if ( from.FindItemOnLayer( Layer.TwoHanded ) != null )
			{
				if ( from.FindItemOnLayer( Layer.TwoHanded ) is BaseWeapon )
				{
					if ( 
						!( from.FindItemOnLayer( Layer.TwoHanded ) is PugilistGlove ) && 
						!( from.FindItemOnLayer( Layer.TwoHanded ) is PugilistGloves ) 
					)
					{
						return false;
					}
				}
			}

			return true;
		}

		public static TimeSpan OnUse( Mobile m )
		{
			if ( !IsEmptyHanded( m ) )
			{
				m.SendMessage( "Você não pode estar empunhando uma arma ao tentar roubar algo." );
			}
			else
			{
				m.Target = new Stealing.StealingTarget( m );
				//m.RevealingAction(); // NO REVEALING ON THIS SERVER

				m.SendMessage(ThieveryStringConstants.MSG_WHICH_ITEM_TO_STEAL);
			}

			return TimeSpan.FromSeconds(ThieveryConstants.SKILL_USE_DELAY_SECONDS);
		}
	}

	/// <summary>
	/// Tracks stolen items and their expiration time.
	/// </summary>
	public class StolenItem
	{
		/// <summary>Time before a stolen item can be returned to its original owner</summary>
		public static readonly TimeSpan StealTime = TimeSpan.FromMinutes(ThieveryConstants.STOLEN_ITEM_EXPIRATION_MINUTES);

		private Item m_Stolen;
		private Mobile m_Thief;
		private Mobile m_Victim;
		private DateTime m_Expires;

		public Item Stolen{ get{ return m_Stolen; } }
		public Mobile Thief{ get{ return m_Thief; } }
		public Mobile Victim{ get{ return m_Victim; } }
		public DateTime Expires{ get{ return m_Expires; } }

		public bool IsExpired{ get{ return ( DateTime.UtcNow >= m_Expires ); } }

		public StolenItem( Item stolen, Mobile thief, Mobile victim )
		{
			m_Stolen = stolen;
			m_Thief = thief;
			m_Victim = victim;

			m_Expires = DateTime.UtcNow + StealTime;
		}

		private static Queue m_Queue = new Queue();

		public static void Add( Item item, Mobile thief, Mobile victim )
		{
			Clean();

			m_Queue.Enqueue( new StolenItem( item, thief, victim ) );
		}

		public static bool IsStolen( Item item )
		{
			Mobile victim = null;

			return IsStolen( item, ref victim );
		}

		public static bool IsStolen( Item item, ref Mobile victim )
		{
			Clean();

			foreach ( StolenItem si in m_Queue )
			{
				if ( si.m_Stolen == item && !si.IsExpired )
				{
					victim = si.m_Victim;
					return true;
				}
			}

			return false;
		}

		public static void ReturnOnDeath( Mobile killed, Container corpse )
		{
			Clean();

			foreach ( StolenItem si in m_Queue )
			{
				if ( si.m_Stolen.RootParent == corpse && si.m_Victim != null && !si.IsExpired )
				{
					if ( si.m_Victim.AddToBackpack( si.m_Stolen ) )
						si.m_Victim.SendLocalizedMessage( 1010464 ); // the item that was stolen is returned to you.
					else
						si.m_Victim.SendLocalizedMessage( 1010463 ); // the item that was stolen from you falls to the ground.

					si.m_Expires = DateTime.UtcNow; // such a hack
				}
			}
		}

		public static void Clean()
		{
			while ( m_Queue.Count > 0 )
			{
				StolenItem si = (StolenItem) m_Queue.Peek();

				if ( si.IsExpired )
					m_Queue.Dequeue();
				else
					break;
			}
		}
	}
}

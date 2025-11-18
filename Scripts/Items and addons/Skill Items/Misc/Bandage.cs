using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Misc;

namespace Server.Items
{
	/// <summary>
	/// Bandage item used for healing and resurrection through the Healing/Veterinary skills.
	/// </summary>
	public class Bandage : Item/*, IDyable*/
	{
		#region Static Configuration

		public static int Range = ( Server.Misc.MyServerSettings.FriendsAvoidHeels() ? BandageConstants.RANGE_FRIENDS_AVOID_HEELS : BandageConstants.RANGE_DEFAULT );

		#endregion

		#region Properties

		public override int Hue{ get { return BandageConstants.HUE_BANDAGE_DEFAULT; } }

		public override double DefaultWeight
		{
			get { return 0.3; }
		}

		#endregion

		#region Constructors

		[Constructable]
		public Bandage() : this( 1 )
		{
		}

		[Constructable]
		public Bandage( int amount ) : base( 0xE21 )
		{
			Stackable = true;
			Amount = amount;
			Hue = BandageConstants.HUE_BANDAGE_DEFAULT;
		}

		public Bandage( Serial serial ) : base( serial )
		{
		}

		#endregion

		#region Serialization

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Hue = BandageConstants.HUE_BANDAGE_DEFAULT;
		}

		#endregion

		#region Command Methods

		/// <summary>
		/// Bandages self - called by the [bandself command.
		/// </summary>
		/// <param name="from">The mobile bandaging themselves</param>
		/// <param name="m_Bandage">The bandage item to consume</param>
		public static void BandSelfCommandCall( Mobile from, Item m_Bandage )
		{
			if ( BandageHelpers.IsWearingOneRing(from) )
			{
				from.SendMessage( BandageStringConstants.MSG_ONERING_PREVENTS_ACTION );
				return;
			}

			if ( BandageHelpers.IsBlessedAndCannotUseItems(from) )
			{
				from.SendMessage( BandageStringConstants.MSG_CANNOT_USE_BLESSED );
				return;
			}

			from.RevealingAction();

			if ( BandageContext.BeginHeal( from, from, m_Bandage ) != null )
				m_Bandage.Consume();
				Server.Gumps.QuickBar.RefreshQuickBar( from );
		}

		/// <summary>
		/// Bandages another mobile - called by the [bandother command.
		/// </summary>
		/// <param name="from">The mobile performing the bandage action</param>
		/// <param name="m_Bandage">The bandage item to consume</param>
		public static void BandOtherCommandCall( Mobile from, Item m_Bandage )
		{
			if ( BandageHelpers.IsWearingOneRing(from) )
			{
				from.SendMessage( BandageStringConstants.MSG_ONERING_PREVENTS_ACTION );
				return;
			}
			if ( BandageHelpers.IsBlessedAndCannotUseItems(from) )
			{
				from.SendMessage( BandageStringConstants.MSG_CANNOT_USE_BLESSED );
				return;
			}

			from.RevealingAction();
			Bandage band = (Bandage)m_Bandage;
			from.SendLocalizedMessage( BandageConstants.CLILOC_WHO_TO_BANDAGE ); // Who will you use the bandages on?
			from.Target = new InternalTarget( band );
		}

		#endregion

		#region Core Logic

		/// <summary>
		/// Handles bandage double-click to initiate healing on a target.
		/// </summary>
		/// <param name="from">The mobile using the bandage</param>
		public override void OnDoubleClick( Mobile from )
		{
			if ( BandageHelpers.IsWearingOneRing(from) )
			{
				from.SendMessage( BandageStringConstants.MSG_ONERING_PREVENTS_ACTION );
				return;
			}
			if ( BandageHelpers.IsBlessedAndCannotUseItems(from) )
			{
				from.SendMessage( BandageStringConstants.MSG_CANNOT_USE_BLESSED );
				return;
			}

			if ( from.InRange( GetWorldLocation(), Range ) )
			{
				from.RevealingAction();

				from.SendLocalizedMessage( BandageConstants.CLILOC_WHO_TO_BANDAGE ); // Who will you use the bandages on?

				from.Target = new InternalTarget( this );
			}
			else
			{
				from.SendLocalizedMessage( BandageConstants.CLILOC_TOO_FAR_AWAY ); // You are too far away to do that.
			}
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Targeting handler for bandage usage.
		/// </summary>
		private class InternalTarget : Target
		{
			private Bandage m_Bandage;

			public InternalTarget( Bandage bandage ) : base( Bandage.Range, false, TargetFlags.Beneficial )
			{
				m_Bandage = bandage;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Bandage.Deleted )
					return;

				if ( targeted is Mobile )
				{
					if ( from.InRange( m_Bandage.GetWorldLocation(), Bandage.Range ) )
					{
					if ( BandageContext.BeginHeal( from, (Mobile)targeted, m_Bandage ) != null )
					{
						m_Bandage.Consume();
							Server.Gumps.QuickBar.RefreshQuickBar( from );
						}
					}
					else
					{
						from.SendLocalizedMessage( BandageConstants.CLILOC_TOO_FAR_AWAY ); // You are too far away to do that.
					}
				}
				else if ( targeted is HenchmanFighterItem )
				{
					BandageHelpers.TryResurrectHenchman((HenchmanFighterItem)targeted, from, m_Bandage, BandageStringConstants.HENCHMAN_NAME_FIGHTER);
				}
				else if ( targeted is HenchmanWizardItem )
				{
					BandageHelpers.TryResurrectHenchman((HenchmanWizardItem)targeted, from, m_Bandage, BandageStringConstants.HENCHMAN_NAME_WIZARD);
				}
				else if ( targeted is HenchmanArcherItem )
				{
					BandageHelpers.TryResurrectHenchman((HenchmanArcherItem)targeted, from, m_Bandage, BandageStringConstants.HENCHMAN_NAME_ARCHER);
				}
				else if ( targeted is HenchmanMonsterItem )
				{
					BandageHelpers.TryResurrectHenchman((HenchmanMonsterItem)targeted, from, m_Bandage, BandageStringConstants.HENCHMAN_NAME_CREATURE);
				}
				else
				{
					from.SendLocalizedMessage( BandageConstants.CLILOC_CANNOT_USE_ON_THAT ); // Bandages can not be used on that.
				}
			}

			protected override void OnNonlocalTarget( Mobile from, object targeted )
			{
				base.OnNonlocalTarget( from, targeted );
			}
		}

		#endregion
	}

	/// <summary>
	/// Manages the state and timing of a bandage healing operation.
	/// </summary>
	public class BandageContext
	{
		#region Fields

		private Mobile m_Healer;
		private Mobile m_Patient;
		private int m_Slips;
		private Timer m_Timer;
		private DateTime m_StartTime;
		private TimeSpan m_HealDelay;
		private int m_HealerStartingHits;
		private int m_DamageEvents;
		private int m_LastKnownHits; // Track last known hits to avoid double-counting damage events
		private bool m_IsEnhancedBandage; // Track if enhanced bandage was used

		private static Dictionary<Mobile, BandageContext> m_Table = new Dictionary<Mobile, BandageContext>();

		#endregion

		#region Properties

		public Mobile Healer{ get{ return m_Healer; } }
		public Mobile Patient{ get{ return m_Patient; } }
		public int Slips{ get{ return m_Slips; } set{ m_Slips = value; } }
		public Timer Timer{ get{ return m_Timer; } }
		public int DamageEvents{ get{ return m_DamageEvents; } }

		#endregion

		#region Constructor

		public BandageContext( Mobile healer, Mobile patient, TimeSpan delay, bool isEnhancedBandage )
		{
			m_Healer = healer;
			m_Patient = patient;
			m_HealDelay = delay;
			m_StartTime = DateTime.UtcNow;
			m_HealerStartingHits = healer != null ? healer.Hits : 0;
			m_LastKnownHits = healer != null ? healer.Hits : 0;
			m_DamageEvents = 0;
			m_IsEnhancedBandage = isEnhancedBandage;

			m_Timer = new InternalTimer( this );
			m_Timer.Start();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Records a finger slip during bandaging, increasing penalty and displaying a message.
		/// Slips are now counted as damage events (not separate).
		/// </summary>
		/// <param name="damageAmount">The amount of damage that caused the slip (used to update tracking)</param>
		public void Slip( int damageAmount )
		{
			// Send PT-BR message in yellow color
			m_Healer.SendMessage( BandageConstants.SLIP_MESSAGE_HUE, BandageStringConstants.MSG_FINGERS_SLIP );
			m_Healer.LocalOverheadMessage( MessageType.Regular, BandageConstants.SLIP_MESSAGE_HUE, false, BandageStringConstants.MSG_FINGERS_SLIP );

			++m_Slips;
			// Slips are now counted as damage events (not separate)
			++m_DamageEvents;
			// Update last known hits to prevent double-counting in timer
			// Calculate expected hits after damage is applied (hits haven't been reduced yet when Slip() is called)
			int expectedHits = m_Healer.Hits - damageAmount;
			if ( expectedHits < 0 )
				expectedHits = 0;
			m_LastKnownHits = expectedHits;
		}

		/// <summary>
		/// Stops the current healing operation and cleans up the timer.
		/// </summary>
		public void StopHeal()
		{
			m_Table.Remove( m_Healer );

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;

			// Release action lock
			if ( m_Healer != null )
				m_Healer.EndAction( typeof( BandageContext ) );
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Gets the current BandageContext for a healer, if any.
		/// </summary>
		/// <param name="healer">The mobile to check</param>
		/// <returns>Active BandageContext or null</returns>
		public static BandageContext GetContext( Mobile healer )
		{
			BandageContext bc = null;
			m_Table.TryGetValue( healer, out bc );
			return bc;
		}

		/// <summary>
		/// Determines the primary healing skill based on the target type.
		/// </summary>
		/// <param name="m">The mobile being healed</param>
		/// <returns>Veterinary for animals/monsters, Healing for players</returns>
		public static SkillName GetPrimarySkill( Mobile m )
		{
			if ( !m.Player && (m.Body.IsMonster || m.Body.IsAnimal) )
				return SkillName.Veterinary;
			else
				return SkillName.Healing;
		}

		/// <summary>
		/// Determines the secondary healing skill based on the target type.
		/// </summary>
		/// <param name="m">The mobile being healed</param>
		/// <returns>AnimalLore for animals/monsters, Anatomy for players</returns>
		public static SkillName GetSecondarySkill( Mobile m )
		{
			if ( !m.Player && (m.Body.IsMonster || m.Body.IsAnimal) )
				return SkillName.AnimalLore;
			else
				return SkillName.Anatomy;
		}

		/// <summary>
		/// Initiates a bandage healing operation on a target.
		/// Validates the target, calculates timing, and creates a BandageContext.
		/// </summary>
		/// <param name="healer">The mobile applying the bandage</param>
		/// <param name="patient">The mobile being healed</param>
		/// <param name="bandage">Optional bandage item to check if it's enhanced</param>
		/// <returns>BandageContext if successful, null if healing cannot be performed</returns>
		public static BandageContext BeginHeal( Mobile healer, Mobile patient, Item bandage = null )
		{
			bool isDeadPet = ( patient is BaseCreature && ((BaseCreature)patient).IsDeadPet );

			if ( patient.Hunger < BandageConstants.HUNGER_MIN_FOR_HEALING && patient is PlayerMobile && patient.Alive )
			{
				healer.SendMessage( BandageStringConstants.MSG_CANNOT_HEAL_HUNGRY );
			}
			else if ( patient is Golem )
			{
				healer.SendLocalizedMessage( BandageConstants.CLILOC_CANNOT_USE_ON_THAT ); // Bandages cannot be used on that.
			}
			else if ( patient is BaseCreature && ((BaseCreature)patient).IsAnimatedDead )
			{
				healer.SendLocalizedMessage( BandageConstants.CLILOC_CANNOT_HEAL ); // You cannot heal that.
			}
			else if ( !patient.Poisoned && patient.Hits == patient.HitsMax && !BleedAttack.IsBleeding( patient ) && !isDeadPet )
			{
				healer.SendLocalizedMessage( BandageConstants.CLILOC_NOT_DAMAGED ); // That being is not damaged!
			}
			else if ( !patient.Alive && (patient.Map == null || !patient.Map.CanFit( patient.Location, BandageConstants.RESURRECT_LOCATION_FIT_HEIGHT, false, false )) )
			{
				healer.SendLocalizedMessage( BandageConstants.CLILOC_CANNOT_RESURRECT_LOCATION ); // Target cannot be resurrected at that location.
			}
			else if ( healer.CanBeBeneficial( patient, true, true ) )
			{
				healer.DoBeneficial( patient );

				bool onSelf = ( healer == patient );
				SkillName primarySkill = GetPrimarySkill( patient );

				// Check if bandage is enhanced
				bool isEnhanced = ( bandage != null && bandage is EnhancedBandage );

				// Calculate bandage delay using timing calculator (enhanced bandages are faster)
				double milliseconds = BandageTimingCalculator.CalculateBandageDelay( healer, patient, primarySkill, isEnhanced );

				BandageContext context = GetContext( healer );

				if ( context != null )
					context.StopHeal();

				// Block other actions while bandaging
				if ( !healer.BeginAction( typeof( BandageContext ) ) )
				{
					// If we can't begin action, something else is blocking - shouldn't happen but handle gracefully
					return null;
				}

				context = new BandageContext( healer, patient, TimeSpan.FromMilliseconds( milliseconds ), isEnhanced );

				m_Table[healer] = context;

				if ( !onSelf )
					patient.SendLocalizedMessage( BandageConstants.CLILOC_ATTEMPTING_HEAL, false, healer.Name ); //  : Attempting to heal you.


				BandageHelpers.SendMessageWithOverhead( healer, BandageConstants.CLILOC_BEGIN_BANDAGES ); // You begin applying the bandages.

				return context;
			}

			return null;
		}

		#endregion

		#region Healing Logic

		/// <summary>
		/// Completes the bandage healing operation after the timer expires.
		/// Handles resurrection, poison cure, bleeding, mortal wounds, and hit point restoration.
		/// </summary>
		public void EndHeal()
		{
			StopHeal();

			int healerNumber = -1, patientNumber = -1;
			bool playSound = true;

			// Track action type and success for skill gain system
			string actionType = "";
			bool wasSuccessful = false;
			int poisonLevel = 0;

			SkillName primarySkill = GetPrimarySkill( m_Patient );
			SkillName secondarySkill = GetSecondarySkill( m_Patient );

			BaseCreature petPatient = m_Patient as BaseCreature;

			if ( !m_Healer.Alive )
			{
				healerNumber = BandageConstants.CLILOC_DIED_BEFORE_FINISH; // You were unable to finish your work before you died.
				patientNumber = -1;
				playSound = false;
			}
			else if ( !m_Healer.InRange( m_Patient, Bandage.Range ) )
			{
				healerNumber = BandageConstants.CLILOC_NOT_CLOSE_ENOUGH; // You did not stay close enough to heal your target.
				patientNumber = -1;
				playSound = false;
			}
			else if ( !m_Patient.Alive || (petPatient != null && petPatient.IsDeadPet) )
			{
				actionType = "resurrect";

				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = ((healing - BandageConstants.HEALING_RESURRECT_BASE) / BandageConstants.HEALING_RESURRECT_DIVISOR) - (m_Slips * BandageConstants.SLIP_PENALTY_PER_SLIP);

				bool hasResurrectSkills = (healing >= BandageConstants.SKILL_RESURRECT_MIN_HEALING && anatomy >= BandageConstants.SKILL_RESURRECT_MIN_ANATOMY);

				if ( hasResurrectSkills && chance > Utility.RandomDouble() )
				{
					if ( m_Patient.Map == null || !m_Patient.Map.CanFit( m_Patient.Location, BandageConstants.RESURRECT_FIT_CHECK_HEIGHT, false, false ) )
					{
						healerNumber = BandageConstants.CLILOC_CANNOT_RESURRECT_LOCATION; // Target can not be resurrected at that location.
						patientNumber = BandageConstants.CLILOC_CANNOT_RESURRECT_THERE; // Thou can not be resurrected there!
					}
					else if ( m_Patient.Region != null && m_Patient.Region.IsPartOf( "Khaldun" ) )
					{
						healerNumber = BandageConstants.CLILOC_VEIL_OF_DEATH_TOO_STRONG; // The veil of death in this area is too strong and resists thy efforts to restore life.
						patientNumber = -1;
					}
					else
					{
						wasSuccessful = true; // Resurrection successful!

						healerNumber = BandageConstants.CLILOC_RESURRECT_SUCCESS; // You are able to resurrect your patient.
						patientNumber = -1;

						m_Patient.PlaySound( BandageConstants.SOUND_RESURRECT );
						m_Patient.FixedEffect( BandageConstants.EFFECT_RESURRECT, BandageConstants.EFFECT_RESURRECT_SPEED, BandageConstants.EFFECT_RESURRECT_DURATION );

						if ( petPatient != null && petPatient.IsDeadPet )
						{
							Mobile master = petPatient.ControlMaster;

							if( master != null && m_Healer == master )
							{
								petPatient.ResurrectPet();

								for ( int i = 0; i < petPatient.Skills.Length; ++i )
								{
									petPatient.Skills[i].Base -= BandageConstants.PET_SKILL_LOSS_PER_RESURRECT;
								}
							}
							else if ( master != null && master.InRange( petPatient, BandageConstants.RANGE_PET_OWNER_RESURRECT ) )
							{
								healerNumber = BandageConstants.CLILOC_RESURRECT_CREATURE_SUCCESS; // You are able to resurrect the creature.

								master.CloseGump( typeof( PetResurrectGump ) );
								master.SendGump( new PetResurrectGump( m_Healer, petPatient ) );
							}
							else
							{
								bool found = false;

								List<Mobile> friends = petPatient.Friends;

								for ( int i = 0; friends != null && i < friends.Count; ++i )
								{
									Mobile friend = friends[i];

									if ( friend.InRange( petPatient, BandageConstants.RANGE_PET_FRIEND_RESURRECT ) )
									{
										healerNumber = BandageConstants.CLILOC_RESURRECT_CREATURE_SUCCESS; // You are able to resurrect the creature.

										friend.CloseGump( typeof( PetResurrectGump ) );
										friend.SendGump( new PetResurrectGump( m_Healer, petPatient ) );

										found = true;
										break;
									}
								}

								if ( !found )
									healerNumber = BandageConstants.CLILOC_OWNER_MUST_BE_NEARBY; // The pet's owner must be nearby to attempt resurrection.
							}
						}
						else
						{
							m_Patient.CloseGump( typeof( ResurrectGump ) );
							m_Patient.SendGump( new ResurrectGump( m_Patient, m_Healer ) );
						}
					}
				}
				else
				{
					if ( petPatient != null && petPatient.IsDeadPet )
						healerNumber = BandageConstants.CLILOC_RESURRECT_CREATURE_FAILURE; // You fail to resurrect the creature.
					else
						healerNumber = BandageConstants.CLILOC_RESURRECT_FAILURE; // You are unable to resurrect your patient.

					patientNumber = -1;
				}
			}
			else if ( m_Patient.Poisoned )
			{
				poisonLevel = m_Patient.Poison.Level;
				actionType = (poisonLevel == 4) ? "cure_lethal" : "cure";

				m_Healer.SendLocalizedMessage( BandageConstants.CLILOC_FINISH_BANDAGES ); // You finish applying the bandages.
				m_Healer.LocalOverheadMessage( MessageType.Regular, BandageConstants.MESSAGE_COLOR_OVERHEAD, BandageConstants.CLILOC_FINISH_BANDAGES ); // WIZARD ADDED FOR OVERHEAD MESSAGE

				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;

				// New poison cure formula: BaseChance + (Healing/10 * 1%) + (Anatomy/10 * 1%) - (Slips * 2%)
				// Enhanced bandage adds 5% to cure chance for all poison levels
				double chance = BandageHelpers.CalculatePoisonCureChance(healing, anatomy, poisonLevel, m_Slips, m_IsEnhancedBandage);

				bool hasCureSkills = (healing >= BandageConstants.SKILL_CURE_MIN_HEALING && anatomy >= BandageConstants.SKILL_CURE_MIN_ANATOMY);

				if ( hasCureSkills && chance > Utility.RandomDouble() )
				{
					if ( m_Patient.CurePoison( m_Healer ) )
					{
						wasSuccessful = true; // Cure successful!

						// Apply enhanced bandage visual and sound effects (like Greater Heal spell)
						// Use green color for poison cure
						if ( m_IsEnhancedBandage )
						{
							m_Patient.FixedParticles( 0x376A, 9, 32, 5030, BandageConstants.ENHANCED_BANDAGE_PARTICLE_HUE, 0, EffectLayer.Waist );
							m_Patient.PlaySound( 0x202 );
						}

						healerNumber = (m_Healer == m_Patient) ? -1 : BandageConstants.CLILOC_CURED_TARGET; // You have cured the target of all poisons.
						patientNumber = BandageConstants.CLILOC_BEEN_CURED; // You have been cured of all poisons.
					}
					else
					{
						healerNumber = -1;
						patientNumber = -1;
					}
				}
				else
				{
					healerNumber = BandageConstants.CLILOC_CURE_FAILED; // You have failed to cure your target!
					patientNumber = -1;
				}
			}
			else if ( BleedAttack.IsBleeding( m_Patient ) )
			{
				healerNumber = BandageConstants.CLILOC_STOP_BLEEDING; // You bind the wound and stop the bleeding
				patientNumber = BandageConstants.CLILOC_BLEEDING_HEALED; // The bleeding wounds have healed, you are no longer bleeding!

				BleedAttack.EndBleed( m_Patient, false );
			}
			else if ( MortalStrike.IsWounded( m_Patient ) )
			{
				healerNumber = ( m_Healer == m_Patient ? BandageConstants.CLILOC_MORTAL_WOUND_SELF : BandageConstants.CLILOC_MORTAL_WOUND_OTHER );
				patientNumber = -1;
				playSound = false;
			}
			else if ( m_Patient.Hits == m_Patient.HitsMax )
			{
				healerNumber = BandageConstants.CLILOC_HEAL_LITTLE_DAMAGE; // You heal what little damage your patient had.
				patientNumber = -1;
			}
			else
			{
				actionType = "heal";
				patientNumber = -1;

				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = ((healing + BandageConstants.HEALING_HP_BASE) / BandageConstants.HEALING_HP_DIVISOR)
					- BandageConstants.HEALING_CHANCE_REDUCTION  // -10% success chance
					- (m_Slips * BandageConstants.SLIP_PENALTY_PER_SLIP);

				if ( chance > Utility.RandomDouble() )
				{
					wasSuccessful = true; // HP healing successful!

					healerNumber = BandageConstants.CLILOC_FINISH_BANDAGES; // You finish applying the bandages.

					double min, max;

					min = (anatomy / BandageConstants.HEALING_CALC_DIVISOR) + (healing / BandageConstants.HEALING_CALC_DIVISOR) + BandageConstants.HEALING_MIN_BASE;
					max = (anatomy / BandageConstants.HEALING_CALC_DIVISOR) + (healing / BandageConstants.HEALING_CALC_DIVISOR) + BandageConstants.HEALING_MAX_BASE;

					double toHeal = min + (Utility.RandomDouble() * (max - min));

					if ( m_Patient.Body.IsMonster || m_Patient.Body.IsAnimal )
						toHeal += m_Patient.HitsMax / BandageConstants.CREATURE_HEALING_DIVISOR;

					if ( Core.AOS )
						toHeal -= toHeal * m_Slips * BandageConstants.SLIP_HEALING_PENALTY_AOS; // TODO: Verify algorithm
					else
						toHeal -= m_Slips * BandageConstants.SLIP_HEALING_PENALTY_CLASSIC;

					// Reduce healing amount by base reduction
					toHeal *= BandageConstants.HEALING_AMOUNT_REDUCTION;

					// Apply concentration loss penalty (10% per damage event)
					// Each time healer takes damage, healing is reduced by 10%
					if ( m_DamageEvents > 0 )
					{
						double concentrationPenalty = 1.0 - (m_DamageEvents * BandageConstants.HEALING_DAMAGE_EVENT_REDUCTION);
						if ( concentrationPenalty < 0.0 )
							concentrationPenalty = 0.0; // Can't go below 0
						toHeal *= concentrationPenalty;
					}

					// Apply enhanced bandage bonus if used
					if ( m_IsEnhancedBandage )
					{
						toHeal += EnhancedBandage.HealingBonus;
					}

					// Cap healing at 100 HP maximum
					if ( toHeal > BandageConstants.HEALING_AMOUNT_CAP )
						toHeal = BandageConstants.HEALING_AMOUNT_CAP;

					if ( toHeal < BandageConstants.HEALING_MINIMUM_AMOUNT )
					{
						toHeal = BandageConstants.HEALING_MINIMUM_AMOUNT;
						healerNumber = BandageConstants.CLILOC_BANDAGES_BARELY_HELP; // You apply the bandages, but they barely help.
					}

					int finalHealAmount = (int)toHeal;
					m_Patient.Heal( finalHealAmount, m_Healer, false );

					// Apply enhanced bandage visual and sound effects (like Greater Heal spell)
					// Use green color for healing
					if ( m_IsEnhancedBandage )
					{
						m_Patient.FixedParticles( 0x376A, 9, 32, 5030, BandageConstants.ENHANCED_BANDAGE_PARTICLE_HUE, 0, EffectLayer.Waist );
						m_Patient.PlaySound( 0x202 );
					}

					// Send message to healer about healing result and concentration penalty (green color)
					if ( m_DamageEvents > 0 )
					{
						int penaltyPercentage = m_DamageEvents * (int)(BandageConstants.HEALING_DAMAGE_EVENT_REDUCTION * 100);
						m_Healer.SendMessage( BandageConstants.HEALING_RESULT_MESSAGE_HUE, BandageStringConstants.MSG_HEALING_RESULT_WITH_PENALTY, finalHealAmount, penaltyPercentage );
					}
					else
					{
						m_Healer.SendMessage( BandageConstants.HEALING_RESULT_MESSAGE_HUE, BandageStringConstants.MSG_HEALING_RESULT_NO_PENALTY, finalHealAmount );
					}
				}
				else
				{
					healerNumber = BandageConstants.CLILOC_BANDAGES_BARELY_HELP; // You apply the bandages, but they barely help.
					playSound = false;
				}
			}

			if ( healerNumber != -1 ){
				m_Healer.SendLocalizedMessage( healerNumber );
				m_Healer.LocalOverheadMessage( MessageType.Regular, BandageConstants.MESSAGE_COLOR_OVERHEAD, healerNumber );} // WIZARD ADDED FOR OVERHEAD MESSAGE

			if ( patientNumber != -1 ){
				m_Patient.SendLocalizedMessage( patientNumber );
				m_Healer.LocalOverheadMessage( MessageType.Regular, BandageConstants.MESSAGE_COLOR_OVERHEAD, patientNumber );} // WIZARD ADDED FOR OVERHEAD MESSAGE

			if ( playSound )
				m_Patient.PlaySound( BandageConstants.SOUND_BANDAGE_SUCCESS );

			// New skill gain system: only grant gains based on skill level and successful actions
			double healerSkill = m_Healer.Skills[primarySkill].Value;
			bool shouldGainSkills = BandageHelpers.ShouldGrantSkillGains(healerSkill, actionType, wasSuccessful, poisonLevel);

			if ( shouldGainSkills )
			{
				//Veterinary bonus: increase gains when healing your pet fighting stronger creatures
				if (primarySkill == SkillName.Veterinary && m_Patient is BaseCreature )
				{
					BaseCreature bc = m_Patient as BaseCreature;
					if (!bc.Summoned && bc.ControlMaster != null && bc.ControlMaster == m_Healer && bc.Combatant != null && bc.Combatant is BaseCreature)
					{
						BaseCreature fighting = bc.Combatant as BaseCreature;
						int ratio = (int)(fighting.HitsMax / ( bc.HitsMax * BandageConstants.VET_RATIO_DIVISOR));

						if (ratio > BandageConstants.VET_MAX_RATIO)
							ratio = BandageConstants.VET_MAX_RATIO;

						if (ratio >= BandageConstants.VET_MIN_RATIO_FOR_BONUS)
						{
							while (ratio > BandageConstants.VET_RATIO_DECREMENT )
							{
								if (Utility.RandomBool())
								{
									BandageHelpers.CheckHealingSkills( m_Healer, primarySkill, secondarySkill );
								}
								ratio -= BandageConstants.VET_RATIO_DECREMENT;
							}
						}
					}
				}

				BandageHelpers.CheckHealingSkills( m_Healer, primarySkill, secondarySkill );
			}
		}

		#endregion

		#region Nested Classes

		/// <summary>
		/// Timer that triggers the EndHeal method after the bandage delay expires.
		/// Also monitors distance between healer and patient, cancelling if they move too far apart.
		/// </summary>
		private class InternalTimer : Timer
		{
			private BandageContext m_Context;

			public InternalTimer( BandageContext context ) : base( TimeSpan.FromMilliseconds( 500 ), TimeSpan.FromMilliseconds( 500 ) )
			{
				m_Context = context;
				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				if ( m_Context == null || m_Context.m_Healer == null || m_Context.m_Patient == null )
				{
					Stop();
					return;
				}

				// Check if healer or patient is deleted
				if ( m_Context.m_Healer.Deleted || m_Context.m_Patient.Deleted )
				{
					m_Context.StopHeal();
					Stop();
					return;
				}

				// Check if healer is no longer alive
				if ( !m_Context.m_Healer.Alive )
				{
					m_Context.StopHeal();
					Stop();
					return;
				}

				// Check if healer became frozen/paralyzed (spell) - cancel healing
				if (  m_Context.m_Healer.Frozen || m_Context.m_Healer.Paralyzed )
				{
					m_Context.m_Healer.SendMessage( BandageStringConstants.MSG_HEALING_CANCELLED_PARALYZED );
					m_Context.StopHeal();
					Stop();
					return;
				}

				// Check distance - if target moved too far, cancel healing
				if ( !m_Context.m_Healer.InRange( m_Context.m_Patient, BandageConstants.HEALING_MAX_RANGE ) )
				{
					m_Context.m_Healer.SendLocalizedMessage( BandageConstants.CLILOC_TARGET_TOO_FAR_CANCELLED );
					m_Context.StopHeal();
					Stop();
					return;
				}

				// Check if healer took damage (concentration compromised)
				// Track damage events by monitoring hits reduction
				// Note: Slips already increment damage events, so we only count damage that didn't cause a slip
				if ( m_Context.m_Healer != null && m_Context.m_Healer.Hits < m_Context.m_LastKnownHits )
				{
					// Calculate how much damage was taken since last check
					int damageTaken = m_Context.m_LastKnownHits - m_Context.m_Healer.Hits;
					
					// Only count as damage event if damage didn't cause a slip (slips already increment damage events)
					// Damage below slip threshold still counts as a damage event
					if ( damageTaken > 0 )
					{
						// Increment damage events (slips already counted, this is for sub-threshold damage)
						m_Context.m_DamageEvents++;
						m_Context.m_LastKnownHits = m_Context.m_Healer.Hits; // Update baseline
					}
				}
				else if ( m_Context.m_Healer != null && m_Context.m_Healer.Hits > m_Context.m_LastKnownHits )
				{
					// If hits increased (healing), update baseline
					m_Context.m_LastKnownHits = m_Context.m_Healer.Hits;
				}

				// Check if healing delay has elapsed
				TimeSpan elapsed = DateTime.UtcNow - m_Context.m_StartTime;
				if ( elapsed >= m_Context.m_HealDelay )
				{
					m_Context.EndHeal();
					Stop();
					return;
				}
			}
		}

		#endregion
	}
}
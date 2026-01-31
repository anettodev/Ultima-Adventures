using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Sixth;
using Server.Spells.Eighth;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a vortex corpse" )]
	public class EnergyVortex : BaseCreature
	{
		public override bool DeleteCorpseOnDeath { get { return true; } }

		public override double DispelDifficulty { get { return 80.0; } }
		public override double DispelFocus { get { return 20.0; } }

		#region Caster Skills Storage

		/// <summary>
		/// Stores the caster's EvalInt skill for dispel resistance calculation
		/// </summary>
		private double m_CasterEvalInt = 0.0;

		/// <summary>
		/// Gets or sets the caster's EvalInt skill value
		/// </summary>
		public double CasterEvalInt
		{
			get { return m_CasterEvalInt; }
			set { m_CasterEvalInt = value; }
		}

		/// <summary>
		/// Stores the caster's Poisoning skill for variant selection
		/// </summary>
		private double m_CasterPoisoning = 0.0;

		/// <summary>
		/// Gets or sets the caster's Poisoning skill value
		/// When set, automatically initializes the vortex type based on skill
		/// </summary>
		public double CasterPoisoning
		{
			get { return m_CasterPoisoning; }
			set 
			{ 
				m_CasterPoisoning = value;
				// Initialize vortex type after skill is set
				InitializeVortexType();
			}
		}

		/// <summary>
		/// Minimum Poisoning skill required to create Poison Vortex
		/// </summary>
		private const double MIN_POISONING_SKILL = 80.0;

		#endregion

		#region Vortex Variant Type

		/// <summary>
		/// Enum for vortex variant types
		/// </summary>
		private enum VortexType
		{
			Energy,
			Ice,
			Fire,
			Poison
		}

		/// <summary>
		/// Stores the variant type of this vortex
		/// </summary>
		private VortexType m_VortexType = VortexType.Energy;

		/// <summary>
		/// Gets whether this is an Energy Vortex variant
		/// </summary>
		private bool IsEnergyVortex
		{
			get { return m_VortexType == VortexType.Energy; }
		}

		#endregion

		#region Constants

		/// <summary>Base dispel resistance chance (20%)</summary>
		private const double BASE_DISPEL_RESISTANCE = 0.20;

		/// <summary>Maximum dispel resistance chance (50%)</summary>
		private const double MAX_DISPEL_RESISTANCE = 0.50;

		/// <summary>EvalInt skill cap for maximum resistance</summary>
		private const double EVALINT_SKILL_CAP = 100.0;

		/// <summary>Minimum chance for death explosion (10%)</summary>
		private const double DEATH_EXPLOSION_CHANCE_MIN = 0.10;

		/// <summary>Maximum chance for death explosion (30%)</summary>
		private const double DEATH_EXPLOSION_CHANCE_MAX = 0.30;

		/// <summary>Area of effect range for death explosion (tiles)</summary>
		private const int DEATH_EXPLOSION_RANGE = 5;

		/// <summary>Base damage bonus for death explosion</summary>
		private const int DEATH_EXPLOSION_DAMAGE_BONUS = 28;

		/// <summary>Damage dice count for death explosion</summary>
		private const int DEATH_EXPLOSION_DICE_COUNT = 2;

		/// <summary>Damage dice sides for death explosion</summary>
		private const int DEATH_EXPLOSION_DICE_SIDES = 5;

		/// <summary>Minimum damage floor for death explosion</summary>
		private const int DEATH_EXPLOSION_MIN_DAMAGE = 12;

		/// <summary>Resist damage multiplier</summary>
		private const double DEATH_EXPLOSION_RESIST_MULTIPLIER = 0.5;

		/// <summary>Particle effect ID for explosion</summary>
		private const int DEATH_EXPLOSION_EFFECT_ID = 0x2A4E;

		/// <summary>Particle effect duration</summary>
		private const int DEATH_EXPLOSION_EFFECT_DURATION = 30;

		/// <summary>Particle effect speed</summary>
		private const int DEATH_EXPLOSION_EFFECT_SPEED = 10;

		/// <summary>Particle Z offset</summary>
		private const int DEATH_EXPLOSION_Z_OFFSET = 10;

		/// <summary>Sound effect ID for explosion</summary>
		private const int DEATH_EXPLOSION_SOUND = 0x029;

		/// <summary>Delay before casting Energy Bolt retaliation (seconds)</summary>
		private const int ENERGY_BOLT_CAST_DELAY_SECONDS = 1;

		#endregion

		public override double GetFightModeRanking( Mobile m, FightMode acqType, bool bPlayerOnly )
		{
			return ( m.Int + m.Skills[SkillName.Magery].Value ) / Math.Max( GetDistanceToSqrt( m ), 1.0 );
		}

		[Constructable]
		public EnergyVortex() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SetStr( 100 );
			SetDex( 120 );
			SetInt( 120 );

			SetHits( 100 );
			SetStam( 250 );
			SetMana( 80 );

			SetDamage( 12, 25 );

			SetDamageType( ResistanceType.Physical, 30 );
			SetResistance( ResistanceType.Physical, 60, 70 );

			// Vortex type will be initialized when CasterPoisoning is set
			// Default to Energy Vortex until skill is set
			m_VortexType = VortexType.Energy;
			Name = "Vórtice de Energia";
			Body = 13;
			Hue = 0x707;
			BaseSoundID = 655;
		}

		/// <summary>
		/// Initializes the vortex type based on caster's Poisoning skill
		/// Called automatically when CasterPoisoning is set
		/// </summary>
		private void InitializeVortexType()
		{
			// Determine available variants based on caster's Poisoning skill
			// Poison Vortex requires at least 80.0 Poisoning skill
			bool canCreatePoisonVortex = m_CasterPoisoning >= MIN_POISONING_SKILL;
			
			// Weighted random distribution:
			// If Poisoning skill >= 80: 10 variants (Energy 40%, Poison 40%, Ice 10%, Fire 10%)
			// If Poisoning skill < 80: 4 variants (Energy 50%, Ice 25%, Fire 25%)
			int maxVariant = canCreatePoisonVortex ? 10 : 4;
			int variant = Utility.Random( maxVariant );
			
			if (canCreatePoisonVortex)
			{
				// With Poisoning >= 80: Energy 40%, Poison 40%, Ice 10%, Fire 10%
				switch ( variant )
				{
					case 0:
					case 1:
					case 2:
					case 3:
						// Energy Vortex - 40% (4/10)
						m_VortexType = VortexType.Energy;
						Name = "Vórtice de Energia";
						Body = 13;
						Hue = 0x707; // Purple/energy hue
						BaseSoundID = 655;
						SetDamageType( ResistanceType.Energy, 100 );
						SetResistance( ResistanceType.Fire, 40, 50 );
						SetResistance( ResistanceType.Cold, 40, 50 );
						SetResistance( ResistanceType.Poison, 40, 50 );
						SetResistance( ResistanceType.Energy, 90, 100 );
						break;
					case 4:
						// Ice Vortex - 10% (1/10)
						m_VortexType = VortexType.Ice;
						Name = "Vórtice de Gelo";
						Body = 13;
						Hue = 0x568;
						BaseSoundID = 655;
						SetDamageType( ResistanceType.Cold, 100 );
						SetResistance( ResistanceType.Fire, 40, 50 );
						SetResistance( ResistanceType.Cold, 90, 100 );
						SetResistance( ResistanceType.Poison, 40, 50 );
						SetResistance( ResistanceType.Energy, 40, 50 );
						break;
					case 5:
						// Fire Vortex - 10% (1/10)
						m_VortexType = VortexType.Fire;
						Name = "Vórtice de Fogo";
						Body = 13;
						Hue = 0xB73;
						BaseSoundID = 838;
						SetDamageType( ResistanceType.Fire, 100 );
						SetResistance( ResistanceType.Fire, 90, 100 );
						SetResistance( ResistanceType.Cold, 40, 50 );
						SetResistance( ResistanceType.Poison, 40, 50 );
						SetResistance( ResistanceType.Energy, 40, 50 );
						break;
					case 6:
					case 7:
					case 8:
					case 9:
						// Poison Vortex - 40% (4/10)
						m_VortexType = VortexType.Poison;
						Name = "Vórtice de Veneno";
						Body = 13;
						Hue = 0x55B;
						BaseSoundID = 655;
						SetDamageType( ResistanceType.Poison, 100 );
						SetResistance( ResistanceType.Fire, 40, 50 );
						SetResistance( ResistanceType.Cold, 40, 50 );
						SetResistance( ResistanceType.Poison, 90, 100 );
						SetResistance( ResistanceType.Energy, 40, 50 );
						break;
				}
			}
			else
			{
				// Without Poisoning >= 80: Energy 50%, Ice 25%, Fire 25%
				switch ( variant )
				{
					case 0:
					case 1:
						// Energy Vortex - 50% (2/4)
						m_VortexType = VortexType.Energy;
						Name = "Vórtice de Energia";
						Body = 13;
						Hue = 0x707; // Purple/energy hue
						BaseSoundID = 655;
						SetDamageType( ResistanceType.Energy, 100 );
						SetResistance( ResistanceType.Fire, 40, 50 );
						SetResistance( ResistanceType.Cold, 40, 50 );
						SetResistance( ResistanceType.Poison, 40, 50 );
						SetResistance( ResistanceType.Energy, 90, 100 );
						break;
					case 2:
						// Ice Vortex - 25% (1/4)
						m_VortexType = VortexType.Ice;
						Name = "Vórtice de Gelo";
						Body = 13;
						Hue = 0x568;
						BaseSoundID = 655;
						SetDamageType( ResistanceType.Cold, 100 );
						SetResistance( ResistanceType.Fire, 40, 50 );
						SetResistance( ResistanceType.Cold, 90, 100 );
						SetResistance( ResistanceType.Poison, 40, 50 );
						SetResistance( ResistanceType.Energy, 40, 50 );
						break;
					case 3:
						// Fire Vortex - 25% (1/4)
						m_VortexType = VortexType.Fire;
						Name = "Vórtice de Fogo";
						Body = 13;
						Hue = 0xB73;
						BaseSoundID = 838;
						SetDamageType( ResistanceType.Fire, 100 );
						SetResistance( ResistanceType.Fire, 90, 100 );
						SetResistance( ResistanceType.Cold, 40, 50 );
						SetResistance( ResistanceType.Poison, 40, 50 );
						SetResistance( ResistanceType.Energy, 40, 50 );
						break;
				}
			}

			SetSkill( SkillName.EvalInt, 100.0 );
			SetSkill( SkillName.Magery, 100.0 );
			SetSkill( SkillName.MagicResist, 90.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 100.0 );
			SetSkill( SkillName.Anatomy, 80.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 40;
			ControlSlots = ( Core.SE ) ? 2 : 1;
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune { get { return Poison.Lethal; } }
		
		/// <summary>
		/// Poison Vortex can poison targets on hit
		/// Type varies based on caster's Poisoning skill
		/// </summary>
		public override Poison HitPoison
		{
			get
			{
				// Only Poison Vortex variant can poison
				if (m_VortexType == VortexType.Poison)
					return GetPoisonTypeBasedOnSkill();
				return null;
			}
		}
		
		/// <summary>
		/// Chance to apply poison on hit (for Poison Vortex)
		/// Poison is applied manually in OnGaveMeleeAttack, so return 0 to disable automatic application
		/// </summary>
		public override double HitPoisonChance
		{
			get
			{
				// Poison is applied manually in OnGaveMeleeAttack with same syntax as Fire/Ice
				return 0.0;
			}
		}
		
		/// <summary>
		/// Calculates poison type based on caster's Poisoning skill
		/// Base distribution: 10% Deadly, 20% Greater, 30% Regular, 40% Lesser
		/// Scales linearly with skill: decreases Lesser, increases others equally
		/// </summary>
		private Poison GetPoisonTypeBasedOnSkill()
		{
			// Base chances (at 0 skill)
			double baseDeadly = 0.10;   // 10%
			double baseGreater = 0.20;   // 20%
			double baseRegular = 0.30;   // 30%
			double baseLesser = 0.40;    // 40%
			
			// Skill scaling factor (0.0 to 1.0 based on skill 0-120)
			// At 120 skill, Lesser becomes 0% and others increase proportionally
			double skillFactor = Math.Min(m_CasterPoisoning / 120.0, 1.0);
			
			// Calculate adjusted chances
			// Lesser decreases from 40% to 0% as skill increases
			double lesserChance = baseLesser * (1.0 - skillFactor);
			
			// The reduction in Lesser is distributed equally among the other three
			double bonusPerType = (baseLesser - lesserChance) / 3.0;
			
			double deadlyChance = baseDeadly + bonusPerType;
			double greaterChance = baseGreater + bonusPerType;
			double regularChance = baseRegular + bonusPerType;
			
			// Random selection based on weighted chances
			double roll = Utility.RandomDouble();
			
			if (roll < deadlyChance)
				return Poison.Deadly;
			else if (roll < deadlyChance + greaterChance)
				return Poison.Greater;
			else if (roll < deadlyChance + greaterChance + regularChance)
				return Poison.Regular;
			else
				return Poison.Lesser;
		}

		public override int GetAngerSound()
		{
			return 0x15;
		}

		public override int GetAttackSound()
		{
			return 0x28;
		}

		/// <summary>
		/// Fire Vortex causes additional fire damage, Ice Vortex can paralyze targets, Poison Vortex can poison targets
		/// </summary>
		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if (defender == null || defender.Deleted || !defender.Alive)
				return;

			// Poison Vortex can poison targets
			if (m_VortexType == VortexType.Poison)
			{
				// Chance to poison (30%)
				if (Utility.RandomDouble() < 0.30)
				{
					Poison poison = GetPoisonTypeBasedOnSkill();
					if (poison != null)
					{
						defender.ApplyPoison(this, poison);
						
						// Visual and audio effects
						defender.PlaySound(0x62D);
						defender.FixedParticles(0x3728, 244, 25, 9941, 1266, 0, EffectLayer.Waist);
					}
				}
			}
			// Fire Vortex causes additional fire damage
			else if (m_VortexType == VortexType.Fire)
			{
				// Chance to cause additional fire damage (30%)
				if (Utility.RandomDouble() < 0.30)
				{
					// Calculate fire damage (based on vortex's base damage)
					int fireDamage = Utility.RandomMinMax(8, 23);
					
					// Apply fire damage
					this.DoHarmful(defender);
					AOS.Damage(defender, this, fireDamage, 0, 100, 0, 0, 0);
					
					// Fire effect
					defender.FixedParticles(0x3709, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot);
					defender.PlaySound(0x208);
					
					// Visual effect
					Effects.SendLocationParticles(EffectItem.Create(defender.Location, defender.Map, EffectItem.DefaultDuration), 0x3709, 10, 30, 5052);
				}
			}
			// Ice Vortex can paralyze targets
			else if (m_VortexType == VortexType.Ice)
			{
				// Check if target is already paralyzed
				if (defender.Paralyzed || defender.Frozen)
					return;

				// Chance to paralyze (30%)
				if (Utility.RandomDouble() < 0.30)
				{
					// Calculate paralysis duration
					double duration;
					
					if (m_CasterEvalInt > 0)
					{
						// Scale linearly from 2s (EvalInt 0) to 5s (EvalInt 100)
						double baseDuration = 2.0;
						double maxDuration = 5.0;
						duration = baseDuration + ((m_CasterEvalInt / 100.0) * (maxDuration - baseDuration));
					}
					else
					{
						// Random duration 1-3 seconds if no EvalInt stored
						duration = Utility.RandomMinMax(1, 3);
					}
					
					// Apply paralysis
					defender.Paralyze(TimeSpan.FromSeconds(duration));
					
					// Visual and audio effects
					defender.FixedEffect(0x376A, 9, 32);
					defender.PlaySound(0x204);
					
					// Ice particles effect
					defender.FixedParticles(0x374A, 10, 15, 5021, 1153, 0, EffectLayer.Waist);
				}
			}
		}

		/// <summary>
		/// Checks if dispel should be resisted based on caster's EvalInt
		/// </summary>
		public bool CheckDispelResistance(Mobile dispeller)
		{
			double resistanceChance = GetDispelResistanceChance();
			if (Utility.RandomDouble() < resistanceChance)
			{
				if (dispeller != null && !dispeller.Deleted)
				{
					dispeller.SendMessage(SpellConstants.MSG_COLOR_WARNING, Server.Spells.Spell.SpellMessages.VORTEX_RESISTED_DISPEL);
					
					// Retaliate against dispeller
					RetaliateAgainstDispeller(dispeller);
				}
				return true; // Resisted
			}
			return false; // Not resisted
		}

		/// <summary>
		/// Calculates dispel resistance chance based on caster's EvalInt
		/// Base 20% + linear increase with EvalInt, capped at 50%
		/// </summary>
		private double GetDispelResistanceChance()
		{
			if (m_CasterEvalInt <= 0)
				return BASE_DISPEL_RESISTANCE;
			
			// Linear scaling: 20% base + (EvalInt / 100) * 30% = 20% to 50%
			double resistance = BASE_DISPEL_RESISTANCE + 
			                   ((m_CasterEvalInt / EVALINT_SKILL_CAP) * 
			                    (MAX_DISPEL_RESISTANCE - BASE_DISPEL_RESISTANCE));
			return Math.Min(resistance, MAX_DISPEL_RESISTANCE);
		}

		/// <summary>
		/// Retaliates against dispeller by targeting them and casting Energy Bolt
		/// </summary>
		private void RetaliateAgainstDispeller(Mobile dispeller)
		{
			if (dispeller == null || dispeller.Deleted || this.Deleted)
				return;
			
			// Set combatant to dispeller
			this.Combatant = dispeller;
			
			// Make vortex aggressive toward dispeller
			Mobile actualTarget = dispeller;
			if (dispeller is BaseCreature)
			{
				BaseCreature bc = (BaseCreature)dispeller;
				if (bc.Controlled || bc.Summoned)
				{
					// Attack the dispeller's master if they have one
					if (bc.ControlMaster != null && bc.ControlMaster.Alive)
					{
						actualTarget = bc.ControlMaster;
						this.Combatant = actualTarget;
					}
				}
			}
			
			// Send creative retaliation messages
			if (actualTarget != null && !actualTarget.Deleted)
			{
				actualTarget.SendMessage(SpellConstants.MSG_COLOR_WARNING, Server.Spells.Spell.SpellMessages.VORTEX_RETALIATION_TARGET);
			}
			
			// Also notify the dispeller if they're different from the target
			if (dispeller != actualTarget && dispeller != null && !dispeller.Deleted)
			{
				dispeller.SendMessage(SpellConstants.MSG_COLOR_WARNING, Server.Spells.Spell.SpellMessages.VORTEX_RETALIATION_CASTER);
			}
			
			// Cast Energy Bolt on the dispeller after brief delay
			Timer.DelayCall(TimeSpan.FromSeconds(ENERGY_BOLT_CAST_DELAY_SECONDS), () =>
			{
				if (!this.Deleted && !dispeller.Deleted && this.InRange(dispeller, SpellConstants.GetSpellRange()))
				{
					CastEnergyBoltOnTarget(dispeller);
				}
			});
		}

		/// <summary>
		/// Casts Energy Bolt spell on the target
		/// </summary>
		private void CastEnergyBoltOnTarget(Mobile target)
		{
			if (target == null || target.Deleted || this.Deleted)
				return;
			
			// Check if target is in range (Energy Bolt range)
			if (!this.InRange(target, SpellConstants.GetSpellRange()))
				return;
			
			// Check if vortex has line of sight
			if (!this.InLOS(target))
				return;
			
			// Create and cast Energy Bolt spell
			try
			{
				EnergyBoltSpell energyBolt = new EnergyBoltSpell(this, null);
				energyBolt.Target(target);
			}
			catch
			{
				// Silently fail if spell casting fails
			}
		}

		/// <summary>
		/// Handles death explosion chance and effect
		/// Only triggers for Energy Vortex variant
		/// </summary>
		public override bool OnBeforeDeath()
		{
			// Only Energy Vortex can explode on death
			if (IsEnergyVortex)
			{
				// Check for death explosion chance (10-30%)
				double explosionChance = Utility.RandomDouble() * (DEATH_EXPLOSION_CHANCE_MAX - DEATH_EXPLOSION_CHANCE_MIN) + DEATH_EXPLOSION_CHANCE_MIN;
				
				if (Utility.RandomDouble() < explosionChance)
				{
					// Trigger death explosion
					TriggerDeathExplosion();
				}
			}
			
			return base.OnBeforeDeath();
		}

		/// <summary>
		/// Triggers the death explosion effect (energy dissipation)
		/// </summary>
		private void TriggerDeathExplosion()
		{
			if (this.Deleted || this.Map == null)
				return;
			
			// Get all targets in range
			List<Mobile> targets = new List<Mobile>();
			List<Mobile> allNearby = new List<Mobile>();
			bool playerVsPlayer = false;
			
			IPooledEnumerable eable = this.Map.GetMobilesInRange(this.Location, DEATH_EXPLOSION_RANGE);
			foreach (Mobile m in eable)
			{
				if (this.Region == m.Region && m != this && m.Alive)
				{
					allNearby.Add(m);
					targets.Add(m);
					if (m.Player)
						playerVsPlayer = true;
				}
			}
			eable.Free();
			
			// Send area-wide explosion message to everyone nearby
			foreach (Mobile m in allNearby)
			{
				if (m != null && !m.Deleted)
				{
					m.SendMessage(SpellConstants.MSG_COLOR_WARNING, Server.Spells.Spell.SpellMessages.VORTEX_DEATH_EXPLOSION_AREA);
				}
			}
			
			if (targets.Count == 0)
				return;
			
			// Calculate damage (similar to Chain Lightning)
			double damage = CalculateDeathExplosionDamage(playerVsPlayer);
			
			// Split damage across targets (like Chain Lightning)
			if (targets.Count > 1)
				damage /= targets.Count;
			
			if (damage < DEATH_EXPLOSION_MIN_DAMAGE)
				damage = DEATH_EXPLOSION_MIN_DAMAGE;
			
			// Apply damage to all targets
			foreach (Mobile m in targets)
			{
				double toDeal = damage;
				
				// Send victim message about being hit by explosion
				m.SendMessage(SpellConstants.MSG_COLOR_ERROR, Server.Spells.Spell.SpellMessages.VORTEX_DEATH_EXPLOSION_VICTIM);
				
				// Check resistance
				if (CheckResisted(m))
				{
					toDeal *= DEATH_EXPLOSION_RESIST_MULTIPLIER;
					m.SendMessage(SpellConstants.MSG_COLOR_ERROR, Server.Spells.Spell.SpellMessages.RESIST_HALF_DAMAGE_VICTIM);
				}
				
				// Check One Ring protection
				bool hasOneRing = false;
				if (m is PlayerMobile && m.FindItemOnLayer(Layer.Ring) != null && m.FindItemOnLayer(Layer.Ring) is OneRing)
				{
					hasOneRing = true;
					m.SendMessage(SpellConstants.MSG_COLOR_WARNING, Server.Spells.Spell.SpellMessages.ONE_RING_PROTECTION_REVEAL);
					toDeal *= 0.5; // One Ring reduces damage by 50%
				}
				
				// Reveal hidden targets (except One Ring protection)
				if (!hasOneRing)
				{
					m.RevealingAction();
				}
				
				// Apply damage
				this.DoHarmful(m);
				SpellHelper.Damage(TimeSpan.Zero, m, this, (int)toDeal, 100, 0, 0, 0, 0);
				
				// Visual and sound effects
				Point3D blast = new Point3D(m.X, m.Y, m.Z + DEATH_EXPLOSION_Z_OFFSET);
				Effects.SendLocationEffect(blast, m.Map, DEATH_EXPLOSION_EFFECT_ID, DEATH_EXPLOSION_EFFECT_DURATION, DEATH_EXPLOSION_EFFECT_SPEED, 0, 0);
				m.PlaySound(DEATH_EXPLOSION_SOUND);
			}
			
			// Main explosion effect at vortex location
			Effects.SendLocationEffect(this.Location, this.Map, DEATH_EXPLOSION_EFFECT_ID, DEATH_EXPLOSION_EFFECT_DURATION, DEATH_EXPLOSION_EFFECT_SPEED, 0, 0);
			this.PlaySound(DEATH_EXPLOSION_SOUND);
			
			// Send message to caster if they're nearby (within explosion range)
			if (this.SummonMaster != null && !this.SummonMaster.Deleted && this.InRange(this.SummonMaster, DEATH_EXPLOSION_RANGE + 2))
			{
				this.SummonMaster.SendMessage(SpellConstants.MSG_COLOR_SYSTEM, Server.Spells.Spell.SpellMessages.VORTEX_DEATH_EXPLOSION_CASTER);
			}
		}

		/// <summary>
		/// Calculates death explosion damage
		/// Uses caster's EvalInt if available, otherwise uses base damage
		/// </summary>
		private double CalculateDeathExplosionDamage(bool playerVsPlayer)
		{
			// Use stored caster EvalInt for damage calculation if available
			if (m_CasterEvalInt > 0)
			{
				// Calculate damage similar to NMS system but without requiring spell instance
				int baseDamage = Utility.Dice(DEATH_EXPLOSION_DICE_COUNT, DEATH_EXPLOSION_DICE_SIDES, DEATH_EXPLOSION_DAMAGE_BONUS);
				double evalBenefit = 1.0 + (m_CasterEvalInt / 100.0); // Simplified EvalInt benefit
				return Math.Floor(baseDamage * evalBenefit);
			}
			else
			{
				// Fallback to base damage if no caster EvalInt stored
				return Utility.Dice(DEATH_EXPLOSION_DICE_COUNT, DEATH_EXPLOSION_DICE_SIDES, DEATH_EXPLOSION_DAMAGE_BONUS);
			}
		}

		/// <summary>
		/// Checks if target resists spell effects
		/// </summary>
		private bool CheckResisted(Mobile m)
		{
			if (m == null)
				return false;
			
			// Simplified resistance check - can be enhanced if needed
			double chance = (m.Skills[SkillName.MagicResist].Value - 100.0) / 200.0;
			if (chance < 0.0)
				chance = 0.0;
			if (chance > 1.0)
				chance = 1.0;
			
			return Utility.RandomDouble() < chance;
		}

		public override void OnThink()
		{
			if ( Core.SE && Summoned )
			{
				ArrayList spirtsOrVortexes = new ArrayList();

				foreach ( Mobile m in GetMobilesInRange( 5 ) )
				{
					if ( m is SummonSnakes || m is BladeSpirits || m is GasCloud || m is DeathVortex || m is SummonedTreefellow || m is EnergyVortex || m is SummonDragon )
					{
						if ( ( (BaseCreature) m ).Summoned )
							spirtsOrVortexes.Add( m );
					}
				}

				while ( spirtsOrVortexes.Count > 6 )
				{
					int index = Utility.Random( spirtsOrVortexes.Count );
					Dispel( ( (Mobile) spirtsOrVortexes[index] ) );
					spirtsOrVortexes.RemoveAt( index );
				}
			}

			base.OnThink();
		}

		/// <summary>
		/// Cleanup tracking when vortex is deleted
		/// </summary>
		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			// Unregister from tracking when vortex is deleted
			if (this.SummonMaster != null)
			{
				EnergyVortexSpell.UnregisterVortex(this.SummonMaster, this);
			}
		}

		public EnergyVortex( Serial serial ): base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 3 ); // version
			writer.Write( (double) m_CasterEvalInt );
			writer.Write( (double) m_CasterPoisoning );
			writer.Write( (int) m_VortexType );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			if ( version >= 1 )
			{
				m_CasterEvalInt = reader.ReadDouble();
			}
			else
			{
				m_CasterEvalInt = 0.0;
			}
			
			if ( version >= 3 )
			{
				m_CasterPoisoning = reader.ReadDouble();
			}
			else
			{
				m_CasterPoisoning = 0.0;
			}
			
			if ( version >= 2 )
			{
				m_VortexType = (VortexType)reader.ReadInt();
			}
			else
			{
				// Legacy: determine type from name
				if (this.Name != null)
				{
					if (this.Name.Contains("Energia"))
						m_VortexType = VortexType.Energy;
					else if (this.Name.Contains("Gelo"))
						m_VortexType = VortexType.Ice;
					else if (this.Name.Contains("Fogo"))
						m_VortexType = VortexType.Fire;
					else if (this.Name.Contains("Veneno"))
						m_VortexType = VortexType.Poison;
					else
						m_VortexType = VortexType.Energy; // Default
				}
			}

			if ( BaseSoundID == 263 )
				BaseSoundID = 0;
		}
	}
}


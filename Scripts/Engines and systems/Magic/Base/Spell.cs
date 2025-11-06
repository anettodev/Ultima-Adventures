using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Misc;
using Server.Spells.Second;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;
using System.Collections.Generic;
using Server.Spells.Bushido;
using Server.Gumps;
using Server.Spells.HolyMan;
using Server.Spells.Song;
using Server.Spells.Mystic;
using Server.Spells.Syth;
using Server.Spells.Jester;
using Server.Spells.Research;
using Server.Spells.DeathKnight;
using Server.Spells.Chivalry;
using Server.SkillHandlers;

namespace Server.Spells
{
	public abstract class Spell : ISpell
	{
		#region Constants

		// Timing Constants
		private const double NEXT_SPELL_DELAY_SECONDS = 0.75;
		private const double ANIMATE_DELAY_SECONDS = 1.5;
		private const int DEFAULT_CAST_RECOVERY_BASE = 6;
		private const int CAST_RECOVERY_PER_SECOND = 4;

		// Damage Calculation Constants
		private const int BASE_DAMAGE_MULTIPLIER = 100;
		private const int INSCRIBE_DAMAGE_DIVISOR = 200;
		private const int INT_BONUS_DIVISOR = 10;
		private const int EVAL_SCALE_BASE = 30;
		private const int EVAL_SCALE_MULTIPLIER = 9;
		private const int EVAL_SCALE_DIVISOR = 100;

		// Skill Thresholds for Movement
		private const double MAGERY_SKILL_40 = 40.0;
		private const double MAGERY_SKILL_50 = 50.0;
		private const double MAGERY_SKILL_60 = 60.0;
		private const double MAGERY_SKILL_70 = 70.0;
		private const double MAGERY_SKILL_80 = 80.0;
		private const double MAGERY_SKILL_90 = 90.0;
		private const double MAGERY_SKILL_100 = 100.0;
		private const double MAGERY_SKILL_110 = 110.0;
		private const double MAGERY_SKILL_120 = 120.0;

		// Steps Allowed Constants
		private const int BASE_STEPS_ALLOWED = 2;
		private const int STEPS_PER_TIER = 2;
		private const int RUNNING_STEP_COST = 2;
		private const int WALKING_STEP_COST = 1;

		// Drunk Mantra Constants
		private const double DRUNK_MANTRA_CHANCE_THRESHOLD = 0.85;
		private const int BAC_DIVISOR = 200;

	// Message Colors
	public const int MSG_COLOR_SYSTEM = 95;
	public const int MSG_COLOR_ERROR = 55;
	public const int MSG_COLOR_WARNING = 33;
	public const int MSG_COLOR_HEAL = 68; // Light blue for healing

	#if DEBUG
	public const int MSG_COLOR_DEBUG_1 = 20;
	public const int MSG_COLOR_DEBUG_2 = 21;
	public const int MSG_COLOR_DEBUG_3 = 22;
	#endif

		// Midland Lucidity Thresholds
		private const double MIDLAND_LUCIDITY_THRESHOLD_LOW = 0.50;
		private const double MIDLAND_LUCIDITY_THRESHOLD_MED = 0.70;
		private const double MIDLAND_LUCIDITY_THRESHOLD_HIGH = 0.90;
		private const double MIDLAND_LUCIDITY_DAMAGE_MULTIPLIER = 1.25;

		// Time Constants
		private const double SPELL_HOLD_MAX_BASE = 6.0;
		private const double SPELL_HOLD_CIRCLE_FACTOR = 0.25;

		// Mana/Reagent Caps
		private const int MIN_MANA_SCALAR = 10; // 10% minimum
		private const int MANA_SCALAR_DIVISOR = 100;
		private const double LRC_MANA_INCREASE_DIVISOR = 200.0;
		private const double LMC_MANA_DECREASE_DIVISOR = 100.0;

		// Stat Mod Constants
		private const int INSCRIBE_MULTIPLIER = 1000;
		private const int PHYLACTERY_MULTIPLIER = 10;

		#endregion

	#region PT-BR User Messages

	/// <summary>
	/// Centralized PT-BR user messages for all spell-related communications
	/// </summary>
	public static class SpellMessages
	{
		#region Common Error Messages
		// General Errors
		public const string ERROR_CANNOT_CAST_IN_STATE = "Você não pode usar magia nesse estado.";
		public const string ERROR_TARGET_ALREADY_DEAD = "O alvo já está morto!";
		public const string ERROR_SOMETHING_PREVENTED_CAST = "Algo de estranho aconteceu e não permitiu usar o feitiço.";
		public const string ERROR_LOST_CONCENTRATION_FORMAT = "Você perdeu a concentração após ter segurado o feitiço por aproximadamente: {0} segundos";
		
		// Target Errors
		public const string ERROR_TARGET_NOT_VISIBLE = "O alvo não pode ser visto.";
		public const string ERROR_CANNOT_HEAL_DEAD = "Você não pode curar aquilo que já está morto.";
		public const string ERROR_CANNOT_HEAL_GOLEM = "* Não sei como curar isso *";
		public const string ERROR_TARGET_MORTALLY_POISONED_SELF = "Você sente o veneno penetrar mais em suas veias.";
		public const string ERROR_TARGET_MORTALLY_POISONED_OTHER = "O seu alvo está mortalmente envenenado e não poderá ser curado com esse feitiço!";
		#endregion

		#region Resist Messages
		// Resistance Messages
		public const string RESIST_SPELL_EFFECTS = "Você resiste aos efeitos da magia.";
		public const string RESIST_HALF_DAMAGE_VICTIM = "Sua aura mágica lhe ajudou a resistir metade do dano desse feitiço.";
		public const string RESIST_HALF_DAMAGE_ATTACKER = "O oponente resistiu metade do dano desse feitiço.";
		#endregion

		#region One Ring Messages
		// One Ring (Lord of the Rings Easter Egg)
		public const string ONE_RING_PREVENTED_SPELL = "O UM ANEL desfez o feitiço e te diz para não fazer isso... ";
		public const string ONE_RING_PROTECTION_REVEAL = "O UM ANEL te protegeu de ser revelado.";
		#endregion

	#region Informational Messages
	// General Info
	public const string INFO_SPELL_UNSTABLE_AFTER_STEPS_FORMAT = "Após {0} passos o seu feitiço ficará instável!";
	public const string INFO_HEAL_AMOUNT_FORMAT = "+{0}";
	#endregion

	#region Buff/Debuff Effect Messages
	// Reactive Armor / Buff Spells
	public const string ERROR_TARGET_ALREADY_UNDER_EFFECT = "O alvo já está sob efeito do feitiço.";
	public const string ERROR_TARGET_UNDER_SIMILAR_EFFECT = "O alvo já está sob um efeito semelhante.";
	public const string ERROR_SPELL_WILL_NOT_ADHERE = "O feitiço não adere em você neste momento.";
	public const string ERROR_SPELL_WILL_NOT_ADHERE_NOW = "O feitiço não adere em você.";
	#endregion

	#region Utility Spell Messages
	// CreateFood Spell
	public const string INFO_FOOD_CREATED_FORMAT = "Magicamente um pouco de {0} apareceu em sua mochila.";
	public const string INFO_WATER_FLASK_CREATED = "Uma garrafa de água surgiu magicamente!";
	#endregion

	#region 2nd Circle Spell Messages
	// Cure Spell
	public const string CURE_MORTAL_POISON_SELF = "Você está mortalmente envenenado e não poderá se curar com esse simples feitiço!";
	public const string CURE_MORTAL_POISON_OTHER = "O seu alvo está mortalmente envenenado e não poderá ser curado com esse simples feitiço!";
	public const string CURE_FAILED = "Você falhou em curar o veneno!";
	public const string CURE_SUCCESS_SELF = "Você curou o veneno!";
	public const string CURE_SUCCESS_OTHER = "Você curou o veneno do alvo!";

	// Protection Spell
	public const string PROTECTION_ALREADY_ACTIVE = "O alvo já está sob efeito do feitiço.";
	public const string PROTECTION_CANNOT_APPLY = "O feitiço não pode ser aplicado nesse momento.";

	// Magic Trap Spell
	public const string MAGIC_TRAP_TOO_MANY = "Existem muitas armadilhas mágicas na área!";
	public const string MAGIC_TRAP_BAD_LOCATION = "Não parece ser uma boa ideia fazer isso aqui.";
	public const string MAGIC_TRAP_INVALID_TARGET = "Você não pode criar uma armadilha aqui.";

	// Remove Trap Spell
	public const string REMOVE_TRAP_INSTRUCTION = "Selecione uma armadilha ou você mesmo para invocar um amuleto de proteção.";
	public const string REMOVE_TRAP_SUCCESS = "Todas as armadilhas aqui foram desativadas.";
	public const string REMOVE_TRAP_FAILED = "Essa armadilha parece complicada demais para ser desfeita por sua magia.";
	public const string REMOVE_TRAP_INVALID = "Este feitiço não tem efeito sobre isso!";
	public const string REMOVE_TRAP_WAND_CREATED = "Você invoca um orbe mágico em sua mochila.";
	#endregion

		#region Drunk Messages
		// Drunk Messages - Easter Eggs (Cheese references)
		public const string DRUNK_CHEESE_1 = "Llhc";  // Leo liked his cheese
		public const string DRUNK_CHEESE_2 = "Vwfob"; // Veryance was a fan of brie
		public const string DRUNK_CHEESE_3 = "Chtsk"; // Cheddar hit the spot for Krystopher
		public const string DRUNK_CHEESE_4 = "Fpfhh"; // Francis partook of fondue, hot heated
		public const string DRUNK_CHEESE_5 = "Cwptc"; // Coffee was partial to cheesewhizz
		public const string DRUNK_CHEESE_6 = "Jwagf"; // Jetson was a grand fromage
		#endregion

		#region Debug Messages
		#if DEBUG
		public const string DEBUG_REAL_DAMAGE_FORMAT = "realDamage-> {0}";
		public const string DEBUG_EVAL_BENEFIT_FORMAT = "getDamageEvalBenefit-> {0}";
		public const string DEBUG_FINAL_DAMAGE_FORMAT = "finalDamage-> {0}";
		#endif
		#endregion
	}

	// Legacy alias for backward compatibility within Spell.cs
	private static class UserMessages
	{
		public const string ERROR_CANNOT_CAST_IN_STATE = SpellMessages.ERROR_CANNOT_CAST_IN_STATE;
		public const string ERROR_TARGET_ALREADY_DEAD = SpellMessages.ERROR_TARGET_ALREADY_DEAD;
		public const string ERROR_SOMETHING_PREVENTED_CAST = SpellMessages.ERROR_SOMETHING_PREVENTED_CAST;
		public const string ERROR_LOST_CONCENTRATION_FORMAT = SpellMessages.ERROR_LOST_CONCENTRATION_FORMAT;
		public const string INFO_SPELL_UNSTABLE_AFTER_STEPS_FORMAT = SpellMessages.INFO_SPELL_UNSTABLE_AFTER_STEPS_FORMAT;
		public const string DRUNK_CHEESE_1 = SpellMessages.DRUNK_CHEESE_1;
		public const string DRUNK_CHEESE_2 = SpellMessages.DRUNK_CHEESE_2;
		public const string DRUNK_CHEESE_3 = SpellMessages.DRUNK_CHEESE_3;
		public const string DRUNK_CHEESE_4 = SpellMessages.DRUNK_CHEESE_4;
		public const string DRUNK_CHEESE_5 = SpellMessages.DRUNK_CHEESE_5;
		public const string DRUNK_CHEESE_6 = SpellMessages.DRUNK_CHEESE_6;
		
		#if DEBUG
		public const string DEBUG_REAL_DAMAGE_FORMAT = SpellMessages.DEBUG_REAL_DAMAGE_FORMAT;
		public const string DEBUG_EVAL_BENEFIT_FORMAT = SpellMessages.DEBUG_EVAL_BENEFIT_FORMAT;
		public const string DEBUG_FINAL_DAMAGE_FORMAT = SpellMessages.DEBUG_FINAL_DAMAGE_FORMAT;
		#endif
	}

	#endregion

		#region Consumable Scroll Types

		private static readonly HashSet<Type> SCROLLS_CONSUMABLE_WITH_JAR = new HashSet<Type>
		{
			// Necromancy Scrolls
			typeof(BloodPactScroll), typeof(GhostlyImagesScroll), typeof(GhostPhaseScroll),
			typeof(GraveyardGatewayScroll), typeof(HellsBrandScroll), typeof(HellsGateScroll),
			typeof(ManaLeechScroll), typeof(NecroCurePoisonScroll), typeof(NecroPoisonScroll),
			typeof(NecroUnlockScroll), typeof(PhantasmScroll), typeof(RetchedAirScroll),
			typeof(SpectreShadowScroll), typeof(UndeadEyesScroll), typeof(VampireGiftScroll),
			typeof(WallOfSpikesScroll),
			
			// Druid Potions
			typeof(ShieldOfEarthPotion), typeof(WoodlandProtectionPotion),
			typeof(ProtectiveFairyPotion), typeof(HerbalHealingPotion), typeof(GraspingRootsPotion),
			typeof(BlendWithForestPotion), typeof(SwarmOfInsectsPotion), typeof(VolcanicEruptionPotion),
			typeof(TreefellowPotion), typeof(StoneCirclePotion), typeof(DruidicRunePotion),
			typeof(LureStonePotion), typeof(NaturesPassagePotion), typeof(MushroomGatewayPotion),
			typeof(RestorativeSoilPotion), typeof(FireflyPotion)
		};

		#endregion

		#region Fields

		private Mobile m_Caster;
		private Item m_Scroll;
		private SpellInfo m_Info;
		private SpellState m_State;
		private DateTime m_StartCastTime;

		private static TimeSpan NextSpellDelay = TimeSpan.FromSeconds(NEXT_SPELL_DELAY_SECONDS);
		private static TimeSpan AnimateDelay = TimeSpan.FromSeconds(ANIMATE_DELAY_SECONDS);

		private CastTimer m_CastTimer;
		private AnimTimer m_AnimTimer;

		private static Dictionary<Type, DelayedDamageContextWrapper> m_ContextTable = new Dictionary<Type, DelayedDamageContextWrapper>();

		#endregion

		#region Properties

		public SpellState State { get { return m_State; } set { m_State = value; } }
		public Mobile Caster { get { return m_Caster; } }
		public SpellInfo Info { get { return m_Info; } }
		public string Name { get { return m_Info.Name; } }
		public string Mantra { get { return m_Info.Mantra; } }
		public Type[] Reagents { get { return m_Info.Reagents; } }
		public Item Scroll { get { return m_Scroll; } }
		public DateTime StartCastTime { get { return m_StartCastTime; } }

		public virtual SkillName CastSkill { get { return SkillName.Magery; } }
		public virtual SkillName DamageSkill { get { return SkillName.EvalInt; } }

		public virtual bool RevealOnCast { get { return true; } }
		public virtual bool ClearHandsOnCast { get { return true; } }
		public virtual bool ShowHandMovement { get { return true; } }

		public virtual bool DelayedDamage { get { return false; } }

		public virtual bool DelayedDamageStacking { get { return true; } }
		// In reality, it's ANY delayed Damage spell Post-AoS that can't stack, but only 
		// Explosion & Magic Arrow have enough delay and a short enough cast time to bring up 
		// the possibility of stacking them. Note that a MA & an Explosion will stack, but
		// of course, two MA's won't.

		public virtual bool BlockedByHorrificBeast { get { return true; } }
		public virtual bool BlockedByAnimalForm { get { return false; } }
		public virtual bool BlocksMovement { get { return true; } }

		public virtual bool CheckNextSpellTime { get { return true; } }

		public virtual bool IsCasting { get { return m_State == SpellState.Casting; } }

		public virtual int CastRecoveryBase { get { return DEFAULT_CAST_RECOVERY_BASE; } }
		public virtual int CastRecoveryFastScalar { get { return 1; } }
		public virtual int CastRecoveryPerSecond { get { return CAST_RECOVERY_PER_SECOND; } }
		public virtual int CastRecoveryMinimum { get { return 0; } }

		public abstract TimeSpan CastDelayBase { get; }

		public virtual double CastDelayFastScalar { get { return 1; } }
		public virtual double CastDelaySecondsPerTick { get { return 0.25; } }
		public virtual TimeSpan CastDelayMinimum { get { return TimeSpan.FromSeconds(0.25); } }

		#endregion

		#region Constructor

		public Spell(Mobile caster, Item scroll, SpellInfo info)
		{
			m_Caster = caster;
			m_Scroll = scroll;
			m_Info = info;
		}

		#endregion

		#region Delayed Damage Context

		private class DelayedDamageContextWrapper
		{
			private Dictionary<Mobile, Timer> m_Contexts = new Dictionary<Mobile, Timer>();

			public void Add(Mobile m, Timer t)
			{
				Timer oldTimer;
				if (m_Contexts.TryGetValue(m, out oldTimer))
				{
					oldTimer.Stop();
					m_Contexts.Remove(m);
				}

				m_Contexts.Add(m, t);
			}

			public void Remove(Mobile m)
			{
				m_Contexts.Remove(m);
			}
		}

		public void StartDelayedDamageContext(Mobile m, Timer t)
		{
			if (DelayedDamageStacking)
				return; // Sanity check

			DelayedDamageContextWrapper contexts;

			if (!m_ContextTable.TryGetValue(GetType(), out contexts))
			{
				contexts = new DelayedDamageContextWrapper();
				m_ContextTable.Add(GetType(), contexts);
			}

			contexts.Add(m, t);
		}

		public void RemoveDelayedDamageContext(Mobile m)
		{
			DelayedDamageContextWrapper contexts;

			if (!m_ContextTable.TryGetValue(GetType(), out contexts))
				return;

			contexts.Remove(m);
		}

		#endregion

		#region Damage Calculation - NMS System

		/// <summary>
		/// Calculates damage using the NMS (New Magic System) formula
		/// </summary>
		public virtual int GetNMSDamage(int bonus, int dice, int sides, Mobile singleTarget)
		{
			return GetNMSDamage(bonus, dice, sides, (Caster.Player && singleTarget.Player));
		}

		/// <summary>
		/// Calculates damage using the NMS (New Magic System) formula
		/// </summary>
		public virtual int GetNMSDamage(int bonus, int dice, int sides, bool playerVsPlayer)
		{
			int realDamage = Utility.Dice(dice, sides, bonus);
			double evalBenefit = NMSUtils.getDamageEvalBenefit(Caster);
			int finalDamage = (int)Math.Floor(realDamage * evalBenefit);

			#if DEBUG
			SendDebugDamageInfo(realDamage, evalBenefit, finalDamage);
			#endif

			return finalDamage;
		}

		#if DEBUG
		/// <summary>
		/// Sends debug damage calculation information to caster (DEBUG only)
		/// </summary>
		private void SendDebugDamageInfo(int realDamage, double evalBenefit, int finalDamage)
		{
			Caster.SendMessage(MSG_COLOR_DEBUG_1, string.Format(UserMessages.DEBUG_REAL_DAMAGE_FORMAT, realDamage));
			Caster.SendMessage(MSG_COLOR_DEBUG_2, string.Format(UserMessages.DEBUG_EVAL_BENEFIT_FORMAT, evalBenefit));
			Caster.SendMessage(MSG_COLOR_DEBUG_3, string.Format(UserMessages.DEBUG_FINAL_DAMAGE_FORMAT, finalDamage));
		}
		#endif

		#endregion

		#region Damage Calculation - AOS System

		/// <summary>
		/// Calculates damage using the AOS (Age of Shadows) formula with target
		/// </summary>
		public virtual int GetNewAosDamage(int bonus, int dice, int sides, Mobile singleTarget)
		{
			if (singleTarget != null)
			{
				return GetNewAosDamage(bonus, dice, sides, (Caster.Player && singleTarget.Player), GetDamageScalar(singleTarget));
			}
			else
			{
				return GetNewAosDamage(bonus, dice, sides, false);
			}
		}

		/// <summary>
		/// Calculates damage using the AOS (Age of Shadows) formula
		/// </summary>
		public virtual int GetNewAosDamage(int bonus, int dice, int sides, bool playerVsPlayer)
		{
			return GetNewAosDamage(bonus, dice, sides, playerVsPlayer, 1.0);
		}

		/// <summary>
		/// Calculates damage using the AOS (Age of Shadows) formula with all modifiers
		/// </summary>
		public virtual int GetNewAosDamage(int bonus, int dice, int sides, bool playerVsPlayer, double scalar)
		{
			int damage = Utility.Dice(dice, sides, bonus) * BASE_DAMAGE_MULTIPLIER;
			int damageBonus = CalculateTotalDamageBonus(playerVsPlayer);

			damage = AOS.Scale(damage, BASE_DAMAGE_MULTIPLIER + damageBonus);
			damage = ApplyEvalIntScaling(damage);
			damage = AOS.Scale(damage, (int)(scalar * BASE_DAMAGE_MULTIPLIER));

			return damage / BASE_DAMAGE_MULTIPLIER;
		}

		/// <summary>
		/// Calculates total damage bonus from Inscribe, Int, and SDI
		/// </summary>
		private int CalculateTotalDamageBonus(bool playerVsPlayer)
		{
			int inscribeBonus = CalculateInscribeBonus();
			int intBonus = CalculateIntBonus();
			int sdiBonus = CalculateSDIBonus(playerVsPlayer);

			int totalBonus = inscribeBonus + intBonus + sdiBonus;

			// Apply Midland modifications if applicable
			if (IsInMidland())
			{
				totalBonus = ApplyMidlandDamageModifications(totalBonus);
			}

			return totalBonus;
		}

		/// <summary>
		/// Calculates Inscription skill bonus for damage
		/// </summary>
		private int CalculateInscribeBonus()
		{
			int inscribeSkill = GetInscribeFixed(m_Caster);
			return (inscribeSkill + (INSCRIBE_MULTIPLIER * (inscribeSkill / INSCRIBE_MULTIPLIER))) / INSCRIBE_DAMAGE_DIVISOR;
		}

		/// <summary>
		/// Calculates Intelligence bonus for damage
		/// </summary>
		private int CalculateIntBonus()
		{
			return Caster.Int / INT_BONUS_DIVISOR;
		}

		/// <summary>
		/// Calculates Spell Damage Increase bonus with caps
		/// </summary>
		private int CalculateSDIBonus(bool playerVsPlayer)
		{
			int sdiBonus = AosAttributes.GetValue(m_Caster, AosAttribute.SpellDamage);
			int sdiCap = MyServerSettings.RealSpellDamageCap();

			if (IsInMidland())
			{
				return 0; // SDI disabled in Midland
			}

			if (sdiBonus > sdiCap)
			{
				sdiBonus = sdiCap;
			}

			// PvP specific cap
			if (playerVsPlayer && Server.Misc.MyServerSettings.SpellDamageIncreaseVsPlayers() > 0)
			{
				int pvpCap = Server.Misc.MyServerSettings.SpellDamageIncreaseVsPlayers();
				if (sdiBonus > pvpCap)
				{
					sdiBonus = pvpCap;
				}
			}

			return sdiBonus;
		}

	/// <summary>
	/// Applies Midland-specific damage modifications
	/// </summary>
	private int ApplyMidlandDamageModifications(int currentBonus)
	{
		if (!(m_Caster is PlayerMobile))
			return currentBonus;

		PlayerMobile playerMobile = (PlayerMobile)m_Caster;

		// Apply Lucidity multiplier
		int modifiedBonus = (int)((double)currentBonus * (playerMobile.Lucidity() * MIDLAND_LUCIDITY_DAMAGE_MULTIPLIER));

		// Add extra int emphasis in Midland
		modifiedBonus += CalculateIntBonus();

		return modifiedBonus;
	}

		/// <summary>
		/// Applies EvalInt scaling to damage
		/// </summary>
		private int ApplyEvalIntScaling(int damage)
		{
			int evalSkill = GetDamageFixed(m_Caster);
			int evalScale = EVAL_SCALE_BASE + ((EVAL_SCALE_MULTIPLIER * evalSkill) / EVAL_SCALE_DIVISOR);
			return AOS.Scale(damage, evalScale);
		}

		#endregion

		#region Mobile Benefit Calculation

		/// <summary>
		/// Calculates mobile benefit based on Magery or Phylactery power
		/// </summary>
		public virtual int CalculateMobileBenefit(Mobile mobile, double modifier, double phylacteryModifier)
		{
			int benefit = 0;
			if (mobile is PlayerMobile)
			{
				// Phylactery support commented out for now
				// Can be re-enabled when Phylactery system is active
			}
			benefit = (int)(mobile.Skills[SkillName.Magery].Value / modifier);
			return benefit;
		}

		#endregion

		#region Damage and Resist Scalars

		/// <summary>
		/// Gets the damage scalar for a target considering various modifiers
		/// </summary>
		public virtual double GetDamageScalar(Mobile target)
		{
			double scalar = 1.0;

			if (!Core.AOS) // Pre-AOS EvalInt mechanics
			{
				double casterEI = m_Caster.Skills[DamageSkill].Value;
				double targetRS = target.Skills[SkillName.MagicResist].Value;

				if (casterEI > targetRS)
					scalar = (1.0 + ((casterEI - targetRS) / 500.0));
				else
					scalar = (1.0 + ((casterEI - targetRS) / 200.0));

				// Magery damage bonus: -25% at 0 skill, +0% at 100 skill, +5% at 120 skill
				scalar += (m_Caster.Skills[CastSkill].Value - 100.0) / 400.0;

				if (!target.Player && !target.Body.IsHuman)
					scalar *= 2.0; // Double magery damage to monsters/animals if not AOS
			}

			// Creature alterations
			if (target is BaseCreature)
				((BaseCreature)target).AlterDamageScalarFrom(m_Caster, ref scalar);

			if (m_Caster is BaseCreature)
				((BaseCreature)m_Caster).AlterDamageScalarTo(target, ref scalar);

			// Slayer damage
			if (Core.SE)
				scalar *= GetSlayerDamageScalar(target);

			// Region modifications
			target.Region.SpellDamageScalar(m_Caster, target, ref scalar);

			// Evasion check
			if (Evasion.CheckSpellEvasion(target))
				scalar = 0;

			return scalar;
		}

		/// <summary>
		/// Gets slayer damage scalar from equipped spellbook
		/// </summary>
		public virtual double GetSlayerDamageScalar(Mobile defender)
		{
			Spellbook atkBook = Spellbook.FindEquippedSpellbook(m_Caster);
			double scalar = 1.0;

			if (atkBook != null)
			{
				SlayerEntry atkSlayer = SlayerGroup.GetEntryByName(atkBook.Slayer);
				SlayerEntry atkSlayer2 = SlayerGroup.GetEntryByName(atkBook.Slayer2);

				if (atkSlayer != null && atkSlayer.Slays(defender) || atkSlayer2 != null && atkSlayer2.Slays(defender))
				{
					defender.FixedEffect(0x37B9, 10, 5);
					scalar = 2.0;
				}

				TransformContext context = TransformationSpellHelper.GetContext(defender);

				if ((atkBook.Slayer == SlayerName.Silver || atkBook.Slayer2 == SlayerName.Silver) && context != null && context.Type != typeof(HorrificBeastSpell))
					scalar += .25; // Every necromancer transformation other than horrific beast takes an additional 25% damage

				if (scalar != 1.0)
					return scalar;
			}

			// Check defender's slayer
			ISlayer defISlayer = Spellbook.FindEquippedSpellbook(defender);

			if (defISlayer == null)
				defISlayer = defender.Weapon as ISlayer;

			if (defISlayer != null)
			{
				SlayerEntry defSlayer = SlayerGroup.GetEntryByName(defISlayer.Slayer);
				SlayerEntry defSlayer2 = SlayerGroup.GetEntryByName(defISlayer.Slayer2);

				if (defSlayer != null && defSlayer.Group.OppositionSuperSlays(m_Caster) || defSlayer2 != null && defSlayer2.Group.OppositionSuperSlays(m_Caster))
					scalar = 2.0;
			}

			return scalar;
		}

		#endregion

		#region Skill Access Methods

		public virtual double GetEvalSkill(Mobile m)
		{
			return m.Skills[SkillName.EvalInt].Value;
		}

		public virtual int GetEvalFixed(Mobile m)
		{
			return m.Skills[SkillName.EvalInt].Fixed;
		}

		public virtual double GetInscribeSkill(Mobile m)
		{
			return m.Skills[SkillName.Inscribe].Value;
		}

		public virtual int GetInscribeFixed(Mobile m)
		{
			return m.Skills[SkillName.Inscribe].Fixed;
		}

		public virtual int GetDamageFixed(Mobile m)
		{
			return m.Skills[DamageSkill].Fixed;
		}

		public virtual double GetDamageSkill(Mobile m)
		{
			return m.Skills[DamageSkill].Value;
		}

		public virtual double GetResistSkill(Mobile m)
		{
			return m.Skills[SkillName.MagicResist].Value;
		}

		#endregion

		#region Caster Event Handlers

		public void HarmfulSpell(Mobile m)
		{
			if (m is BaseCreature)
				((BaseCreature)m).OnHarmfulSpell(m_Caster);
		}

		public virtual void OnCasterHurt()
		{
			// Confirm: Monsters and pets cannot be disturbed
			if (!Caster.Player)
				return;

			if (IsCasting)
			{
				object o = ProtectionSpell.Registry[m_Caster];
				bool disturb = true;

				if (o != null)
				{
					double protectionValue = 0.0;

					// Support new ProtectionEntry system (tracks first hit vs subsequent hits)
					if (o is ProtectionSpell.ProtectionEntry)
					{
						protectionValue = ((ProtectionSpell.ProtectionEntry)o).GetProtectionValue();
					}
					// Backward compatibility with old double system
					else if (o is double)
					{
						protectionValue = (double)o;
					}

					if (protectionValue > Utility.RandomDouble() * 100.0)
						disturb = false;
				}

				if (disturb)
					Disturb(DisturbType.Hurt, false, true);
			}
		}

		public virtual void OnCasterKilled()
		{
			Disturb(DisturbType.Kill);
		}

		public virtual void OnConnectionChanged()
		{
			FinishSequence();
		}

	public virtual bool OnCasterMoving(Direction direction)
	{
		if (!(m_Caster is PlayerMobile))
			return true;

		PlayerMobile playerMobile = (PlayerMobile)m_Caster;
		bool isRunning = (direction & Direction.Running) != 0;

		if (playerMobile.StepsAllowedForCastingSpells >= 0)
		{
			return ProcessRemainingSteps(playerMobile, isRunning);
		}

		return ValidateSpellHoldTime();
	}

		public virtual bool OnCasterEquiping(Item item)
		{
			if (IsCasting)
				Disturb(DisturbType.EquipRequest);

			return true;
		}

		public virtual bool OnCasterUsingObject(object o)
		{
			if (m_State == SpellState.Sequencing)
				Disturb(DisturbType.UseRequest);

			return true;
		}

		public virtual bool OnCastInTown(Region r)
		{
			return m_Info.AllowTown;
		}

		#endregion

		#region Movement and Step Processing

		/// <summary>
		/// Processes and deducts remaining steps allowed during casting
		/// </summary>
		private bool ProcessRemainingSteps(PlayerMobile player, bool isRunning)
		{
			int stepCost = isRunning ? RUNNING_STEP_COST : WALKING_STEP_COST;
			player.StepsAllowedForCastingSpells -= stepCost;
			return true;
		}

	/// <summary>
	/// Validates if the spell has been held too long
	/// </summary>
	private bool ValidateSpellHoldTime()
	{
		if (!(this is MagerySpell))
			return true;

		MagerySpell magerySpell = (MagerySpell)this;
		TimeSpan castDelay = GetCastDelay();
		double maxHoldSeconds = CalculateMaxHoldSeconds(magerySpell.Circle, castDelay);
		double elapsedSeconds = (DateTime.UtcNow - m_StartCastTime).TotalSeconds;

		if (elapsedSeconds > maxHoldSeconds)
		{
			NotifySpellLostConcentration(elapsedSeconds);
			DoFizzle();
			Disturb(DisturbType.UseRequest);
			return false;
		}

		return true;
	}

		/// <summary>
		/// Calculates maximum seconds a spell can be held before losing concentration
		/// </summary>
		private double CalculateMaxHoldSeconds(SpellCircle circle, TimeSpan castDelay)
		{
			return SPELL_HOLD_MAX_BASE - ((SPELL_HOLD_CIRCLE_FACTOR * (int)circle) + castDelay.TotalSeconds);
		}

		/// <summary>
		/// Notifies caster they lost concentration
		/// </summary>
		private void NotifySpellLostConcentration(double elapsedSeconds)
		{
			string message = string.Format(UserMessages.ERROR_LOST_CONCENTRATION_FORMAT, Math.Truncate(elapsedSeconds));
			m_Caster.SendMessage(message);
		}

		#endregion

		#region Reagent Consumption

		public virtual bool ConsumeReagents()
		{
			if (m_Scroll != null || !m_Caster.Player)
				return true;

			if (AosAttributes.GetValue(m_Caster, AosAttribute.LowerRegCost) > Utility.Random(100))
				return true;

			Container pack = m_Caster.Backpack;

			if (pack == null)
				return false;

			if (pack.ConsumeTotal(m_Info.Reagents, m_Info.Amounts) == -1)
				return true;

			return false;
		}

		#endregion

		#region Spell Fizzle

		public virtual void DoFizzle()
		{
			m_Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 502632); // The spell fizzles.

			if (m_Caster.Player)
			{
				if (Core.AOS)
					m_Caster.FixedParticles(0x3735, 1, 30, 9503, EffectLayer.Waist);
				else
					m_Caster.FixedEffect(0x3735, 6, 30);

				m_Caster.PlaySound(0x5C);
			}
		}

		public virtual void DoHurtFizzle()
		{
			m_Caster.FixedEffect(0x3735, 6, 30);
			m_Caster.PlaySound(0x5C);
		}

		#endregion

		#region Spell Disturbance

		public void Disturb(DisturbType type)
		{
			if (!(m_Scroll is SoulShard))
			{
				Disturb(type, true, false);
			}
		}

		public virtual bool CheckDisturb(DisturbType type, bool firstCircle, bool resistable)
		{
			return true;
		}

		public void Disturb(DisturbType type, bool firstCircle, bool resistable)
		{
			if (!CheckDisturb(type, firstCircle, resistable))
				return;

			if (m_State == SpellState.Casting)
			{
				if (!firstCircle && !Core.AOS && this is MagerySpell && ((MagerySpell)this).Circle == SpellCircle.First)
					return;

				m_State = SpellState.None;
				m_Caster.Spell = null;

				OnDisturb(type, true);

				if (m_CastTimer != null)
					m_CastTimer.Stop();

				if (m_AnimTimer != null)
					m_AnimTimer.Stop();

				if (Core.AOS && m_Caster.Player && type == DisturbType.Hurt)
					DoHurtFizzle();

				m_Caster.NextSpellTime = Core.TickCount + (int)GetDisturbRecovery().TotalMilliseconds;
			}
			else if (m_State == SpellState.Sequencing)
			{
				if (!firstCircle && !Core.AOS && this is MagerySpell && ((MagerySpell)this).Circle == SpellCircle.First)
					return;

				m_State = SpellState.None;
				m_Caster.Spell = null;

				OnDisturb(type, false);

				Targeting.Target.Cancel(m_Caster);

				if (Core.AOS && m_Caster.Player && type == DisturbType.Hurt)
					DoHurtFizzle();
			}
		}

		public virtual void OnDisturb(DisturbType type, bool message)
		{
			if (message)
			{
				if (this is HolyManSpell)
				{
					m_Caster.SendMessage("Your concentration is disturbed, thus ruining thy prayer.");
				}
				else if (this is MysticSpell || this is JesterSpell)
				{
					m_Caster.SendMessage("Your concentration is disturbed, thus ruining thy attempt.");
				}
				else
				{
					m_Caster.SendLocalizedMessage(500641); // Your concentration is disturbed, thus ruining thy spell.
				}
			}
		}

		#endregion

		#region Cast Validation

		public virtual bool CheckCast(Mobile caster)
		{
		if (caster.Blessed)
		{
			caster.SendMessage(MSG_COLOR_ERROR, UserMessages.ERROR_CANNOT_CAST_IN_STATE);
			return false;
		}

		PlayerMobile playerMobile = caster as PlayerMobile;
		if (playerMobile != null)
		{
			ConfigureAllowedSteps(playerMobile);
		}

		return true;
		}

		/// <summary>
		/// Calculates the number of steps allowed based on caster's Magery skill
		/// </summary>
		private int CalculateAllowedStepsByMagery(double mageryValue)
		{
			if (mageryValue >= MAGERY_SKILL_120) return 20;
			if (mageryValue >= MAGERY_SKILL_110) return 18;
			if (mageryValue >= MAGERY_SKILL_100) return 16;
			if (mageryValue >= MAGERY_SKILL_90) return 14;
			if (mageryValue >= MAGERY_SKILL_80) return 12;
			if (mageryValue >= MAGERY_SKILL_70) return 10;
			if (mageryValue >= MAGERY_SKILL_60) return 8;
			if (mageryValue >= MAGERY_SKILL_50) return 6;
			if (mageryValue >= MAGERY_SKILL_40) return 4;
			return BASE_STEPS_ALLOWED;
		}

		/// <summary>
		/// Configures step tracking for PlayerMobile during casting
		/// </summary>
		private void ConfigureAllowedSteps(PlayerMobile player)
		{
			int maxSteps = CalculateAllowedStepsByMagery(player.Skills[SkillName.Magery].Value);
			player.StepsAllowedForCastingSpells = maxSteps;

			string message = string.Format(UserMessages.INFO_SPELL_UNSTABLE_AFTER_STEPS_FORMAT, maxSteps);
			player.SendMessage(MSG_COLOR_SYSTEM, message);
		}

		#endregion

		#region Mantra and Speech

		/// <summary>
		/// Attempts to say the spell mantra, handling drunk state
		/// </summary>
	public virtual bool SayMantra()
	{
		if (!string.IsNullOrEmpty(m_Info.Mantra) && m_Caster.Player)
		{
			PlayerMobile playerMobile = m_Caster as PlayerMobile;
			if (playerMobile != null && IsDrunk(playerMobile))
			{
				return TrySayDrunkMantra(playerMobile, m_Info.Mantra);
			}

			m_Caster.PublicOverheadMessage(MessageType.Spell, m_Caster.SpeechHue, true, m_Info.Mantra, false);

				if (this is DeathKnightSpell)
				{
					m_Caster.PlaySound(0x19E);
				}
			}
			else
			{
				return HandleNonStandardMantra();
			}

			return true;
		}

		/// <summary>
		/// Checks if player is drunk
		/// </summary>
		private bool IsDrunk(PlayerMobile player)
		{
			if (player.BAC <= 0)
				return false;

			double drunkChance = (double)player.BAC / BAC_DIVISOR;
			return Utility.RandomDouble() < drunkChance;
		}

		/// <summary>
		/// Attempts to say mantra while drunk (may garble words)
		/// </summary>
		private bool TrySayDrunkMantra(PlayerMobile player, string mantra)
		{
			string garbledMantra = GenerateGarbledMantra(mantra);
			player.PublicOverheadMessage(MessageType.Spell, player.SpeechHue, true, garbledMantra, false);
			return false; // Spell fails when drunk-garbled
		}

		/// <summary>
		/// Generates a garbled version of the mantra for drunk casters
		/// </summary>
		private string GenerateGarbledMantra(string mantra)
		{
			string[] words = mantra.Split(' ');
			System.Text.StringBuilder result = new System.Text.StringBuilder();

			for (int i = 0; i < words.Length; i++)
			{
				if (Utility.RandomDouble() > DRUNK_MANTRA_CHANCE_THRESHOLD)
				{
					result.Append(GetRandomDrunkWord());
				}
				else
				{
					result.Append(words[Utility.Random(words.Length)]);
				}

				if (i < words.Length - 1)
				{
					result.Append(" ");
				}
			}

			return result.ToString();
		}

		/// <summary>
		/// Returns a random drunk cheese word (easter egg)
		/// </summary>
		private string GetRandomDrunkWord()
		{
			switch (Utility.Random(6))
			{
				case 0: return UserMessages.DRUNK_CHEESE_1;
				case 1: return UserMessages.DRUNK_CHEESE_2;
				case 2: return UserMessages.DRUNK_CHEESE_3;
				case 3: return UserMessages.DRUNK_CHEESE_4;
				case 4: return UserMessages.DRUNK_CHEESE_5;
				case 5: return UserMessages.DRUNK_CHEESE_6;
				default: return UserMessages.DRUNK_CHEESE_1;
			}
		}

	/// <summary>
	/// Handles special mantras for non-standard spell types
	/// </summary>
	private bool HandleNonStandardMantra()
	{
		PaladinSpell paladinSpell = this as PaladinSpell;
		if (paladinSpell != null)
		{
			m_Caster.PublicOverheadMessage(MessageType.Regular, 0x3B2, paladinSpell.MantraNumber, "", false);
		}
		else if (this is SpiritSpeak.SpiritSpeakSpell)
		{
			HandleSpiritSpeakMantra();
		}

		return true;
	}

		/// <summary>
		/// Handles Spirit Speak specific mantra based on karma
		/// </summary>
		private void HandleSpiritSpeakMantra()
		{
			if (m_Caster.Karma < 0)
			{
				m_Caster.Say("Xtee Mee Glau");
				m_Caster.PlaySound(0x481);
			}
			else
			{
				m_Caster.Say("Anh Mi Sah Ko");
				m_Caster.PlaySound(0x24A);
			}
		}

		#endregion

		#region Cast Execution

		public bool Cast()
		{
			m_StartCastTime = DateTime.UtcNow;

			if (m_Caster.Spell is Spell && ((Spell)m_Caster.Spell).State == SpellState.Sequencing)
				((Spell)m_Caster.Spell).Disturb(DisturbType.NewCast);

			if (!ValidateCanCast())
				return false;

			if (m_Caster.Mana >= ScaleMana(GetMana()))
			{
				if (m_Caster.Spell == null && m_Caster.CheckSpellCast(this) && CheckCast(m_Caster) && m_Caster.Region.OnBeginSpellCast(m_Caster, this))
				{
					m_State = SpellState.Casting;
					m_Caster.Spell = this;

					if (RevealOnCast)
						m_Caster.RevealingAction();

					bool spoke = SayMantra();

					if (!spoke) // Character was drunk
					{
						m_State = SpellState.None;

						if (m_Caster.Spell == this)
							m_Caster.Spell = null;
						return false;
					}

					TimeSpan castDelay = GetCastDelay();

					if (ShowHandMovement && m_Caster.Body.IsHuman)
					{
						int count = (int)Math.Ceiling(castDelay.TotalSeconds / AnimateDelay.TotalSeconds);

						if (count != 0)
						{
							m_AnimTimer = new AnimTimer(this, count);
							m_AnimTimer.Start();
						}

						if (m_Info.LeftHandEffect > 0)
							Caster.FixedParticles(0, 10, 5, m_Info.LeftHandEffect, EffectLayer.LeftHand);

						if (m_Info.RightHandEffect > 0)
							Caster.FixedParticles(0, 10, 5, m_Info.RightHandEffect, EffectLayer.RightHand);
					}

					if ((ClearHandsOnCast) && (MagicCastingItem.CastNoSkill(m_Scroll) == false))
						m_Caster.ClearHands();

					if (Core.ML)
						WeaponAbility.ClearCurrentAbility(m_Caster);

					m_CastTimer = new CastTimer(this, castDelay);
					m_CastTimer.Start();

					OnBeginCast();

					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				m_Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502625); // Insufficient mana
			}

			return false;
		}

		/// <summary>
		/// Validates various conditions that prevent casting
		/// </summary>
		private bool ValidateCanCast()
		{
			if (m_Caster.Blessed)
			{
				m_Caster.SendMessage("You cannot do that while in this state.");
				return false;
			}
			
			if (!m_Caster.CheckAlive())
			{
				return false;
			}
			
			if (m_Caster.Spell != null && m_Caster.Spell.IsCasting)
			{
				m_Caster.SendLocalizedMessage(502642); // You are already casting a spell.
				return false;
			}
			
			if (BlockedByHorrificBeast && TransformationSpellHelper.UnderTransformation(m_Caster, typeof(HorrificBeastSpell)) || (BlockedByAnimalForm && AnimalForm.UnderTransformation(m_Caster)))
			{
				m_Caster.SendLocalizedMessage(1061091); // You cannot cast that spell in this form.
				return false;
			}
			
			if (m_Caster.Paralyzed || m_Caster.Frozen)
			{
				m_Caster.SendLocalizedMessage(502643); // You can not cast a spell while frozen.
				return false;
			}
			
			if (CheckNextSpellTime && Core.TickCount - m_Caster.NextSpellTime < 0)
			{
				m_Caster.SendLocalizedMessage(502644); // You have not yet recovered from casting a spell.
				return false;
			}
			
			if (m_Caster is PlayerMobile && ((PlayerMobile)m_Caster).PeacedUntil > DateTime.UtcNow)
			{
				m_Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
				return false;
			}

			return true;
		}

		public abstract void OnCast();

		public virtual void OnBeginCast()
		{
		}

		#endregion

		#region Cast Skills and Fizzle Check

		public virtual void GetCastSkills(out double min, out double max)
		{
			min = max = 0; // Intended but not required for overriding
		}

		public virtual bool CheckFizzle()
		{
			if (MagicCastingItem.CastNoSkill(m_Scroll) == true)
				return true;

			double minSkill, maxSkill;

			GetCastSkills(out minSkill, out maxSkill);

			if (DamageSkill != CastSkill)
				Caster.CheckSkill(DamageSkill, 0.0, Caster.Skills[DamageSkill].Cap);

			return Caster.CheckSkill(CastSkill, minSkill, maxSkill);
		}

		#endregion

		#region Mana Cost

		public abstract int GetMana();

		public virtual int ScaleMana(int mana)
		{
			double scalar = 1.0;

			if (!Necromancy.MindRotSpell.GetMindRotScalar(Caster, ref scalar))
				scalar = 1.0;

			// Lower Reagent Cost increases mana cost
			int lowerRegCost = AosAttributes.GetValue(m_Caster, AosAttribute.LowerRegCost);
			int lrcCap = MyServerSettings.LowerReagentCostCap();

			if (lowerRegCost > lrcCap)
				lowerRegCost = lrcCap;

			scalar += (double)lowerRegCost / LRC_MANA_INCREASE_DIVISOR; // 100% LRC = 50% more mana cost

			// Lower Mana Cost reduces mana cost
			int lowerManaCost = AosAttributes.GetValue(m_Caster, AosAttribute.LowerManaCost);
			int lmcCap = MyServerSettings.LowerManaCostCap();

			if (Caster is PlayerMobile && ((PlayerMobile)Caster).Sorcerer())
				lmcCap = 100;

			lowerManaCost = AdventuresFunctions.DiminishingReturns(lowerManaCost, lmcCap);

			if (lowerManaCost > lmcCap)
				lowerManaCost = lmcCap;

			scalar -= (double)lowerManaCost / LMC_MANA_DECREASE_DIVISOR;

			if (scalar <= 0.1)
				scalar = 0.1; // Minimum 10% mana cost

			// Soul Shard casts are free
			if (m_Scroll is SoulShard)
			{
				return 0;
			}

			return (int)(mana * scalar);
		}

		#endregion

		#region Cast Recovery and Delay

		public virtual TimeSpan GetDisturbRecovery()
		{
			if (Core.AOS)
				return TimeSpan.Zero;

			double delay = 1.0 - Math.Sqrt((DateTime.UtcNow - m_StartCastTime).TotalSeconds / GetCastDelay().TotalSeconds);

			if (delay < 0.2)
				delay = 0.2;

			return TimeSpan.FromSeconds(delay);
		}

		public virtual TimeSpan GetCastRecovery()
		{
			if (!Core.AOS)
				return NextSpellDelay;

			int fastCastRecovery = CalculateFastCastRecovery();
			int fcrDelay = -(CastRecoveryFastScalar * fastCastRecovery);
			int delay = CastRecoveryBase + fcrDelay;

			if (delay < CastRecoveryMinimum)
				delay = CastRecoveryMinimum;

			return TimeSpan.FromSeconds((double)delay / CastRecoveryPerSecond);
		}

		/// <summary>
		/// Calculates Fast Cast Recovery value with all modifiers
		/// </summary>
		private int CalculateFastCastRecovery()
		{
			int fcr = AosAttributes.GetValue(m_Caster, AosAttribute.CastRecovery);

			if (IsInMidland())
				fcr = 0;

			if (AnimalForm.UnderTransformation(m_Caster))
				fcr = 0;

			fcr += GetMidlandFCRBonus();

			int fcrCap = MyServerSettings.CastRecoveryCap();
			if (fcr > fcrCap)
				fcr = fcrCap;

			return fcr;
		}

		public virtual TimeSpan GetCastDelay()
		{
			int fastCast = CalculateFastCast();
			TimeSpan baseDelay = CastDelayBase;
			TimeSpan fcDelay = TimeSpan.FromSeconds(-(CastDelayFastScalar * fastCast * CastDelaySecondsPerTick));
			TimeSpan delay = baseDelay + fcDelay;

			if (delay < CastDelayMinimum)
				delay = CastDelayMinimum;

			return delay;
		}

		/// <summary>
		/// Calculates Fast Cast value with all modifiers
		/// </summary>
		private int CalculateFastCast()
		{
			// Faster casting cap of 2 (if not using the protection spell)
			// Faster casting cap of 0 (if using the protection spell)
			// Paladin spells are subject to a faster casting cap of 4
			// Paladins with magery of 70.0 or above are subject to a faster casting cap of 2
			int fcMax = MyServerSettings.CastSpeedCap();

			if (IsMageryNecromancyOrChivalryWithMagery())
				fcMax = 2;

			int fc = AosAttributes.GetValue(m_Caster, AosAttribute.CastSpeed);

			if (IsInMidland() || AnimalForm.UnderTransformation(m_Caster))
				fc = 0;

			if (fc > fcMax)
				fc = fcMax;

			if (ProtectionSpell.Registry.Contains(m_Caster))
				fc -= 2;

			fc += GetMidlandFCBonus();

			return fc;
		}

		/// <summary>
		/// Checks if cast skill qualifies for lower FC cap
		/// </summary>
		private bool IsMageryNecromancyOrChivalryWithMagery()
		{
			return CastSkill == SkillName.Magery ||
				   CastSkill == SkillName.Necromancy ||
				   (CastSkill == SkillName.Chivalry && m_Caster.Skills[SkillName.Magery].Value >= 70.0);
		}

		#endregion

		#region Midland Helpers

		/// <summary>
		/// Checks if caster is in Midland region
		/// </summary>
		private bool IsInMidland()
		{
			return AdventuresFunctions.IsInMidland((object)m_Caster);
		}

	/// <summary>
	/// Gets Fast Cast Recovery bonus for Midland based on Lucidity
	/// </summary>
	private int GetMidlandFCRBonus()
	{
		if (!IsInMidland() || !(m_Caster is PlayerMobile))
			return 0;

		PlayerMobile playerMobile = (PlayerMobile)m_Caster;
		double lucidity = playerMobile.Lucidity();
		int bonus = 0;

			if (lucidity > MIDLAND_LUCIDITY_THRESHOLD_LOW) bonus++;
			if (lucidity > MIDLAND_LUCIDITY_THRESHOLD_MED) bonus++;
			if (lucidity > MIDLAND_LUCIDITY_THRESHOLD_HIGH) bonus++;

			return bonus;
		}

	/// <summary>
	/// Gets Fast Cast bonus for Midland based on Lucidity
	/// </summary>
	private int GetMidlandFCBonus()
	{
		if (!IsInMidland() || !(m_Caster is PlayerMobile))
			return 0;

		PlayerMobile playerMobile = (PlayerMobile)m_Caster;
		double lucidity = playerMobile.Lucidity();
		int bonus = 0;

			if (lucidity > MIDLAND_LUCIDITY_THRESHOLD_MED) bonus++;
			if (lucidity > MIDLAND_LUCIDITY_THRESHOLD_HIGH) bonus++;

			return bonus;
		}

		#endregion

		#region Sequence Validation

		public virtual void FinishSequence()
		{
			m_State = SpellState.None;

			if (m_Caster.Spell == this)
				m_Caster.Spell = null;

			Server.Gumps.MReagentGump.XReagentGump(m_Caster);
		}

		public virtual int ComputeKarmaAward()
		{
			return 0;
		}

		public virtual bool CheckSequence()
		{
			int mana = ScaleMana(GetMana());

			if (m_Caster.Deleted || !m_Caster.Alive || m_Caster.Spell != this || m_State != SpellState.Sequencing)
			{
				DoFizzle();
			}
			else if (m_Scroll != null && !(m_Scroll is SoulShard) && !(m_Scroll is BaseMagicStaff) && !(m_Scroll is Runebook) && (m_Scroll.Amount <= 0 || m_Scroll.Deleted || m_Scroll.RootParent != m_Caster))
			{
				DoFizzle();
			}
			else if (!ConsumeReagents())
			{
				m_Caster.SendLocalizedMessage(502630); // More reagents are needed for this spell.
				DoFizzle();
			}
			else if (m_Caster.Mana < mana)
			{
				m_Caster.SendLocalizedMessage(502625); // Insufficient mana for this spell.
				DoFizzle();
			}
			else if (Core.AOS && (m_Caster.Frozen || m_Caster.Paralyzed))
			{
				m_Caster.SendLocalizedMessage(502646); // You cannot cast a spell while frozen.
				DoFizzle();
			}
			else if (m_Caster is PlayerMobile && ((PlayerMobile)m_Caster).PeacedUntil > DateTime.UtcNow)
			{
				m_Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
				DoFizzle();
			}
			else if (CheckFizzle())
			{
				m_Caster.Mana -= mana;

				ConsumeScrollWithJarReturn();

				if (MagicCastingItem.CastNoSkill(m_Scroll) == true) // DO NOT REMOVE WAND FROM HANDS
				{
				}
				else
				{
					if (ClearHandsOnCast)
						m_Caster.ClearHands();
				}

				int karma = ComputeKarmaAward();

				if (karma != 0)
					Misc.Titles.AwardKarma(Caster, karma, true);

				if (TransformationSpellHelper.UnderTransformation(m_Caster, typeof(VampiricEmbraceSpell)))
				{
					bool garlic = false;

					for (int i = 0; !garlic && i < m_Info.Reagents.Length; ++i)
						garlic = (m_Info.Reagents[i] == Reagent.Garlic);

					if (garlic)
					{
						m_Caster.SendLocalizedMessage(1061651); // The garlic burns you!
						AOS.Damage(m_Caster, Utility.RandomMinMax(17, 23), 100, 0, 0, 0, 0);
					}
				}

				return true;
			}
			else
			{
				DoFizzle();
			}

			return false;
		}

		/// <summary>
		/// Consumes scroll and handles jar return for specific scroll types
		/// </summary>
		private void ConsumeScrollWithJarReturn()
		{
			if (m_Scroll == null)
				return;

			Type scrollType = m_Scroll.GetType();

		if (SCROLLS_CONSUMABLE_WITH_JAR.Contains(scrollType))
		{
			m_Scroll.Consume();
			m_Caster.AddToBackpack(new Jar());
		}
		else if (m_Scroll is SpellScroll)
		{
			m_Scroll.Consume();
		}
		else
		{
			BaseMagicStaff magicStaff = m_Scroll as BaseMagicStaff;
			if (magicStaff != null)
			{
				magicStaff.ConsumeCharge(m_Caster);
			}
		}
		}

		#endregion

		#region Beneficial and Harmful Sequence Checks

		public bool CheckBSequence(Mobile target)
		{
			return CheckBSequence(target, false);
		}

		public bool CheckBSequence(Mobile target, bool allowDead)
		{
			if (!target.Alive && !allowDead)
			{
				m_Caster.SendLocalizedMessage(501857); // This spell won't work on that!
				return false;
			}
			else if (Caster.CanBeBeneficial(target, true, allowDead) && CheckSequence())
			{
				Caster.DoBeneficial(target);
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool CheckHSequence(Mobile target)
		{
			if (!target.Alive)
			{
				m_Caster.SendMessage(MSG_COLOR_SYSTEM, UserMessages.ERROR_TARGET_ALREADY_DEAD);
				return false;
			}
			else if (Caster.CanBeHarmful(target) && CheckSequence())
			{
				Caster.DoHarmful(target);
				return true;
			}
			else
			{
				m_Caster.SendMessage(MSG_COLOR_WARNING, UserMessages.ERROR_SOMETHING_PREVENTED_CAST);
				return false;
			}
		}

		#endregion

		#region Timers

		private class AnimTimer : Timer
		{
			private Spell m_Spell;

			public AnimTimer(Spell spell, int count) : base(TimeSpan.Zero, AnimateDelay, count)
			{
				m_Spell = spell;
				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				if (m_Spell.State != SpellState.Casting || m_Spell.m_Caster.Spell != m_Spell)
				{
					Stop();
					return;
				}

				if (!m_Spell.Caster.Mounted && m_Spell.Caster.Body.IsHuman && m_Spell.m_Info.Action >= 0)
					m_Spell.Caster.Animate(m_Spell.m_Info.Action, 7, 1, true, false, 0);

				if (!Running)
					m_Spell.m_AnimTimer = null;
			}
		}

		private class CastTimer : Timer
		{
			private Spell m_Spell;

			public CastTimer(Spell spell, TimeSpan castDelay) : base(castDelay)
			{
				m_Spell = spell;
				Priority = TimerPriority.TwentyFiveMS;
			}

			protected override void OnTick()
			{
				if (m_Spell.m_State == SpellState.Casting && m_Spell.m_Caster.Spell == m_Spell)
				{
					m_Spell.m_State = SpellState.Sequencing;
					m_Spell.m_CastTimer = null;
					m_Spell.m_Caster.OnSpellCast(m_Spell);
					m_Spell.m_Caster.Region.OnSpellCast(m_Spell.m_Caster, m_Spell);
					m_Spell.m_Caster.NextSpellTime = Core.TickCount + (int)m_Spell.GetCastRecovery().TotalMilliseconds;

					Target originalTarget = m_Spell.m_Caster.Target;

					m_Spell.OnCast();

					if (m_Spell.m_Caster.Player && m_Spell.m_Caster.Target != originalTarget && m_Spell.Caster.Target != null)
						m_Spell.m_Caster.Target.BeginTimeout(m_Spell.m_Caster, TimeSpan.FromSeconds(30.0));

					m_Spell.m_CastTimer = null;
				}
			}
		}

		#endregion
	}
}


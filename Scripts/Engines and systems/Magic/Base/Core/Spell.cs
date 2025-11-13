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

		// Message Colors (kept here for backward compatibility)
		public const int MSG_COLOR_SYSTEM = SpellConstants.MSG_COLOR_SYSTEM;
		public const int MSG_COLOR_ERROR = SpellConstants.MSG_COLOR_ERROR;
		public const int MSG_COLOR_WARNING = SpellConstants.MSG_COLOR_WARNING;
		public const int MSG_COLOR_HEAL = SpellConstants.MSG_COLOR_HEAL;

		#if DEBUG
		public const int MSG_COLOR_DEBUG_1 = SpellConstants.MSG_COLOR_DEBUG_1;
		public const int MSG_COLOR_DEBUG_2 = SpellConstants.MSG_COLOR_DEBUG_2;
		public const int MSG_COLOR_DEBUG_3 = SpellConstants.MSG_COLOR_DEBUG_3;
		#endif

		// Mana/Reagent Caps (kept here as they're spell-specific)
		private const int MIN_MANA_SCALAR = SpellConstants.MIN_MANA_SCALAR;
		private const int MANA_SCALAR_DIVISOR = SpellConstants.MANA_SCALAR_DIVISOR;
		private const double LRC_MANA_INCREASE_DIVISOR = SpellConstants.LRC_MANA_INCREASE_DIVISOR;
		private const double LMC_MANA_DECREASE_DIVISOR = SpellConstants.LMC_MANA_DECREASE_DIVISOR;

		// Stat Mod Constants
		private const int PHYLACTERY_MULTIPLIER = SpellConstants.PHYLACTERY_MULTIPLIER;

		// Drunk Mantra Constants
		private const double DRUNK_MANTRA_CHANCE_THRESHOLD = SpellConstants.DRUNK_MANTRA_CHANCE_THRESHOLD;
		private const int BAC_DIVISOR = SpellConstants.BAC_DIVISOR;

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
		public const string ERROR_CONCENTRATION_DISTURBED = "Sua concentração foi perturbada, arruinando o feitiço.";
		
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
	public const string REMOVE_TRAP_SUCCESS_WITH_PERCENT = "Todas as armadilhas aqui foram desativadas. (Chance de sucesso: {0}%)";
	public const string REMOVE_TRAP_FAILED = "Essa armadilha parece complicada demais para ser desfeita por sua magia.";
	public const string REMOVE_TRAP_INVALID = "Este feitiço não tem efeito sobre isso!";
	public const string REMOVE_TRAP_WAND_CREATED = "Você invoca um orbe mágico em sua mochila.";
	#endregion

	#region 3rd Circle Spell Messages
	// Bless Spell
	public const string ERROR_ALREADY_BLESSED = "O alvo já está abençoado.";
	public const string SUCCESS_CURSE_REMOVED = "A maldição foi removida!";
	public const string ERROR_CURSE_REMOVE_FAILED = "Você tenta remover a maldição, mas falha!";

	// Poison Spell
	public const string ERROR_RESIST_POISON = "Você se sente resistindo ao feitiço.";

	// Telekinesis Spell
	public const string ERROR_ITEM_NOT_MOVABLE = "Esse item não parece se mover.";
	public const string ERROR_TOO_MANY_ITEMS_STACKED = "Há muitos itens empilhados aqui para serem movidos.";
	public const string ERROR_ITEM_TOO_HEAVY = "Isso é muito pesado para mover.";
	public const string ERROR_CANNOT_MOVE_WORN_ITEMS = "Você não pode mover objetos que estão dentro de outros objetos ou sendo usados.";
	public const string SUCCESS_ITEM_MOVED_TO_BACKPACK = "Você move o objeto ao seu alcance e o coloca em sua mochila.";
	public const string ERROR_SPELL_WONT_WORK = "Este feitiço não funcionará nisso!";

	// Teleport Spell
	public const string ERROR_TOO_HEAVY_TELEPORT = "Você está muito pesado para se teletransportar!";
	public const string ERROR_LOCATION_BLOCKED = "Esse local está bloqueado com aura anti-magia.";
	public const string ERROR_CURSED_PREVENTS_TELEPORT = "Você falhou em se teletransportar para longe desta criatura!";

	// Recall Spell
	public const string ERROR_RUNE_NOT_MARKED = "Essa runa ainda não foi marcada.";
	public const string ERROR_TARGET_NOT_MARKED = "O alvo não está marcado.";
	public const string ERROR_CANNOT_RECALL_FROM_OBJECT = "Você não pode se teletransportar a partir desse objeto.";
	public const string ERROR_SPELL_DOES_NOT_WORK_HERE = "Esse feitiço parece não funcionar neste lugar.";
	public const string ERROR_DESTINATION_MAGICALLY_INACCESSIBLE = "O destino parece magicamente inacessível.";
	public const string ERROR_LOCATION_NOT_DISCOVERED = "Você não conhece esse local e não tem ideia de como mentalizá-lo!";
	public const string ERROR_LOCATION_BLOCKED_TELEPORT = "Esse local está bloqueado para o uso de teletransporte!";
	public const string ERROR_NO_CHARGES_LEFT = "Não há mais cargas nesse item!";

	// Unlock Spell
	public const string SUCCESS_UNLOCKED = "Você ouve a fechadura abrir.";
	public const string ERROR_LOCK_TOO_COMPLEX = "A fechadura é muito complexa para este feitiço.";
	public const string ERROR_NOTHING_TO_UNLOCK = "Você não tem o que destrancar.";
	public const string ERROR_UNLOCK_WRONG_SPELL = "Este feitiço é para destrancar baús, caixas, cofres e algumas portas.";
	public const string ERROR_CURSED_BOX_CANNOT_UNLOCK = "Este feitiço nunca será capaz de destrancar uma caixa amaldiçoada.";
	public const string ERROR_UNLOCK_NO_EFFECT = "Este feitiço não serve para destrancar isto.";
	public const string ERROR_ALREADY_UNLOCKED = "Isso não precisava ser destrancado.";
	public const string ERROR_CANNOT_UNLOCK_ITEM = "Você não pode destrancar isso!";
	public const string ERROR_CANNOT_USE_MAGIC_ON_SECURED = "Você não pode usar magia em um item seguro.";
	public const string ERROR_CHEST_NOT_LOCKED = "Este baú não parece estar trancado ou não possuir um nível de trava.";
	public const string ERROR_MAGIC_AURA_PREVENTS_UNLOCK = "Uma forte aura mágica neste baú impede o funcionamento do seu feitiço mas talvez um ladrão possa abri-lo.";
	public const string ERROR_LOCK_TOO_COMPLEX_FOR_SPELL = "Esta fechadura parece ser muito complexa para o seu feitiço.";
	#endregion

	#region 5th Circle Spell Messages
	// Summon Creature
	public const string ERROR_TOO_MANY_FOLLOWERS = "Você já possui muitos seguidores para usar essa magia.";
	public const string INFO_SUMMON_DURATION_FORMAT = "O seu feitiço terá a duração de aproximadamente {0}s.";
	public const string INFO_PREVIOUS_SUMMON_DISMISSED = "Sua invocação anterior foi dissipada.";

	// Paralyze
	public const string ERROR_TARGET_ALREADY_FROZEN = "O alvo já está congelado.";
	public const string PARALYZE_RESIST_VICTIM = "Sua aura mágica lhe ajudou a resistir ao feitiço pela metade. ({0}s)";
	public const string PARALYZE_RESIST_ATTACKER = "O oponente resistiu ao feitiço pela metade. ({0}s)";

	// Mind Blast
	public const string MIND_BLAST_RESIST = "Você resiste aos efeitos da magia.";

	// Magic Reflect - Reflection Shield System
	public const string ERROR_MAGIC_REFLECT_ALREADY_ACTIVE = "Esse feitiço já está fazendo efeito em você.";
	public const string ERROR_MAGIC_REFLECT_COOLDOWN = "Você precisa aguardar para usar novamente esse feitiço.";
	public const string MAGIC_REFLECT_ACTIVATED = "Seu escudo de reflexão está ativo.";
	public const string MAGIC_REFLECT_EXPIRED = "Seu escudo de reflexão se dissipou.";
	public const string MAGIC_REFLECT_COOLDOWN_FORMAT = "Você precisa aguardar {0:F0}s para usar esse feitiço novamente.";
	public const string MAGIC_REFLECT_DURATION_FORMAT = "Seu escudo de reflexão durará {0:F0} segundos.";
	
	// Reflection success
	public const string MAGIC_REFLECT_SUCCESS_CASTER = "Seu feitiço foi refletido de volta!";
	public const string MAGIC_REFLECT_SUCCESS_TARGET = "Seu escudo refletiu o feitiço!";
	
	// Power comparison - Target wins/tie (both shields consumed)
	public const string MAGIC_REFLECT_BOTH_BREAK_ATTACKER = "O escudo do alvo refletiu seu feitiço e destruiu ambos os escudos!";
	public const string MAGIC_REFLECT_BOTH_BREAK_TARGET = "Seu escudo refletiu o feitiço! (Ambos os escudos foram destruídos)";
	
	// Power comparison - Attacker wins (target shield broken)
	public const string MAGIC_REFLECT_OVERPOWERED_ATTACKER = "Seu escudo superior atravessou a reflexão do alvo!";
	public const string MAGIC_REFLECT_OVERPOWERED_TARGET = "Seu escudo foi destruído por um feitiço mais poderoso!";
	
	// Reflection failure - Target state
	public const string MAGIC_REFLECT_TARGET_DEAD = "O feitiço refletido se dissipou - o alvo não existe mais.";
	public const string MAGIC_REFLECT_TARGET_SAFE_ZONE = "O feitiço refletido se dissipou - o alvo está em área protegida.";
	public const string MAGIC_REFLECT_TARGET_UNAVAILABLE = "O feitiço refletido se dissipou - o alvo não está mais disponível.";
	
	// Arcane Interference - Multi-target spell shield interaction
	public const string MAGIC_REFLECT_ARCANE_INTERFERENCE_CASTER = "As energias arcanas se repelem! Todos os escudos foram dissipados.";
	public const string MAGIC_REFLECT_ARCANE_INTERFERENCE_TARGET = "Seu escudo foi dissipado pela interferência arcana do feitiço!";
	public const string MAGIC_REFLECT_ARCANE_INTERFERENCE_PROTECTED = "A interferência arcana protegeu você do dano!";

	// Incognito
	public const string ERROR_INCOGNITO_ALREADY_ACTIVE = "Este feitiço já atua sobre você!";
	public const string ERROR_INCOGNITO_BODY_PAINT = "Você não pode usar esse feitiço enquanto veste uma pintura corporal.";
	public const string ERROR_INCOGNITO_ALREADY_DISGUISED = "Você não pode usar esse feitiço quando já está disfarçado.";
	public const string ERROR_INCOGNITO_INVALID_TARGET = "Não é possível usar este feitiço neste alvo!";
	public const string INFO_INCOGNITO_SELECT_TARGET = "Quem você deseja usar como disfarce?";

	// Blade Spirits
	// (Uses ERROR_LOCATION_BLOCKED from general messages section)

	// Dispel Field
	public const string ERROR_CANNOT_DISPEL = "Isso não pode ser dissipado.";
	public const string ERROR_MAGIC_TOO_CHAOTIC = "Essa magia é muito caótica para o seu feitiço.";
	#endregion

	#region 7th Circle Spell Messages
	// Energy Field
	public const string INFO_FIELD_DURATION_FORMAT = "O seu feitiço funcionará por aproximadamente {0}.";

	// Gate Travel
	public const string ERROR_TRAVEL_BLOCKED = "Algo de estranho aconteceu e bloqueou o uso deste feitiço.";
	public const string ERROR_TRAVEL_LOCATION_BLOCKED = "Esse feitiço parece não funcionar neste lugar.";
	public const string ERROR_TRAVEL_DESTINATION_BLOCKED = "O destino parece magicamente inacessível.";
	public const string ERROR_TRAVEL_LOCATION_OCCUPIED = "Esse local está bloqueado para o uso de teletransporte!";
	public const string ERROR_GATE_ALREADY_EXISTS = "Já existe um portal neste local.";
	public const string INFO_GATE_CREATED = "Você abriu um portal mágico para outro lugar.";
	public const string INFO_SELECT_VALID_DESTINATION = "Selecione um destino válido.";
	public const string ERROR_DESTINATION_NOT_MARKED = "O destino não está marcado corretamente.";

	// Mana Vampire
	public const string INFO_MANA_DRAINED_FORMAT = "Você sugou {0} pontos de mana do oponente.";
	public const string INFO_MANA_LOST = "Você sente que perdeu uma parte de sua mana!";

	// Mass Dispel
	public const string ERROR_CANNOT_DISPEL_FORMAT = "Não é possível dissipar {0}";
	public const string INFO_CREATURE_DISPELED_FORMAT = "{0} foi dissipado!";
	public const string INFO_CREATURE_DISPELED_LOW_HEALTH_FORMAT = "{0} já estava sem forças e foi dissipado!";
	public const string INFO_CREATURE_ANGRY_FORMAT = "Você conseguiu irritar {0}";

	// Polymorph
	public const string INFO_POLYMORPH_TRANSFORM_START_FORMAT = "Você sente sua forma se transformando! A transmutação durará aproximadamente {0} segundos.";
	public const string INFO_POLYMORPH_TRANSFORM_END = "A transmutação se dissipa, e você retorna à sua aparência original.";
	#endregion

	#region 8th Circle Spell Messages
	// Resurrection
	public const string RESURRECTION_TARGET_NOT_VISIBLE = "O alvo não pode ser visto.";
	public const string RESURRECTION_CANNOT_USE_ON_SELF = "Este feitiço não funciona em você mesmo.";
	public const string RESURRECTION_MUST_BE_ALIVE = "Você deve estar vivo para usar este feitiço.";
	public const string RESURRECTION_TARGET_NOT_DEAD = "O alvo não está morto para ser ressuscitado.";
	public const string RESURRECTION_TARGET_TOO_FAR = "O alvo precisa estar mais próximo para ser ressuscitado.";
	public const string RESURRECTION_LOCATION_BLOCKED_CASTER = "O alvo não pode ser ressuscitado neste local.";
	public const string RESURRECTION_LOCATION_BLOCKED_TARGET = "Você não pode ser ressuscitado aqui!";
	public const string RESURRECTION_SOULBOUND_MESSAGE = "Essa pessoa escolheu levar uma vida difícil. (SoulBound)";
	public const string RESURRECTION_SOULORB_CREATED = "Você invoca um orbe mágico para proteger sua alma.";
	public const string RESURRECTION_SOULORB_DURATION = "O orbe durará {0} antes de se dissipar.";
	public const string RESURRECTION_PET_VETERINARY_REQUIRED = "Você precisa de pelo menos 80 pontos em Veterinária para ressuscitar pets.";
	public const string RESURRECTION_PET_SUCCESS = "O feitiço foi bem-sucedido! Aguardando aprovação do dono do pet para completar a ressurreição.";
	public const string RESURRECTION_PET_FAILED = "O feitiço falhou ao tentar ressuscitar o pet.";
	public const string RESURRECTION_HENCH_NOT_DEAD = "Eles não estão mortos.";
	public const string RESURRECTION_SPELL_FAILED = "Este feitiço não parece funcionar.";

	// Earthquake
	// (Uses RESIST_HALF_DAMAGE_VICTIM from Resist Messages section)

	// Energy Vortex / Elemental Summons
	public const string ERROR_LOCATION_BLOCKED_SUMMON = "O local está bloqueado para este feitiço.";
	public const string VORTEX_RESISTED_DISPEL = "O vortex de energia resistiu ao feitiço de dissipação!";
	public const string VORTEX_RETALIATION_CASTER = "O vortex de energia sentiu sua tentativa de dissipação e agora te mira com fúria arcana!";
	public const string VORTEX_RETALIATION_TARGET = "O vortex de energia detecta sua presença e concentra sua energia destrutiva em você!";
	public const string VORTEX_DEATH_EXPLOSION_CASTER = "O vortex de energia se desintegra em uma explosão cataclísmica de energia pura!";
	public const string VORTEX_DEATH_EXPLOSION_VICTIM = "Você é atingido pela onda de choque da explosão do vortex de energia!";
	public const string VORTEX_DEATH_EXPLOSION_AREA = "Uma explosão massiva de energia pura se espalha pela área!";
	// (Uses ERROR_TOO_MANY_FOLLOWERS and INFO_SUMMON_DURATION_FORMAT from 5th Circle section)
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

		private static TimeSpan NextSpellDelay = SpellConstants.NextSpellDelay;
		private static TimeSpan AnimateDelay = SpellConstants.AnimateDelay;

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

		public virtual int CastRecoveryBase { get { return SpellConstants.DEFAULT_CAST_RECOVERY_BASE; } }
		public virtual int CastRecoveryFastScalar { get { return 1; } }
		public virtual int CastRecoveryPerSecond { get { return SpellConstants.CAST_RECOVERY_PER_SECOND; } }
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
			return SpellDamageCalculator.GetNMSDamage(this, bonus, dice, sides, singleTarget);
		}

		/// <summary>
		/// Calculates damage using the NMS (New Magic System) formula
		/// </summary>
		public virtual int GetNMSDamage(int bonus, int dice, int sides, bool playerVsPlayer)
		{
			return SpellDamageCalculator.GetNMSDamage(this, bonus, dice, sides, playerVsPlayer);
		}

		#endregion

		#region Damage Calculation - AOS System

		/// <summary>
		/// Calculates damage using the AOS (Age of Shadows) formula with target
		/// </summary>
		public virtual int GetNewAosDamage(int bonus, int dice, int sides, Mobile singleTarget)
		{
			return SpellDamageCalculator.GetNewAosDamage(this, bonus, dice, sides, singleTarget);
		}

		/// <summary>
		/// Calculates damage using the AOS (Age of Shadows) formula
		/// </summary>
		public virtual int GetNewAosDamage(int bonus, int dice, int sides, bool playerVsPlayer)
		{
			return SpellDamageCalculator.GetNewAosDamage(this, bonus, dice, sides, playerVsPlayer);
		}

		/// <summary>
		/// Calculates damage using the AOS (Age of Shadows) formula with all modifiers
		/// </summary>
		public virtual int GetNewAosDamage(int bonus, int dice, int sides, bool playerVsPlayer, double scalar)
		{
			return SpellDamageCalculator.GetNewAosDamage(this, bonus, dice, sides, playerVsPlayer, scalar);
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
			return SpellDamageCalculator.GetDamageScalar(this, target);
		}

		/// <summary>
		/// Gets slayer damage scalar from equipped spellbook
		/// </summary>
		public virtual double GetSlayerDamageScalar(Mobile defender)
		{
			return SpellDamageCalculator.GetSlayerDamageScalar(this, defender);
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
			return SpellMovementHandler.ProcessRemainingSteps(playerMobile, isRunning);
		}

		return SpellMovementHandler.ValidateSpellHoldTime(this, m_StartCastTime);
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
		// Movement logic has been moved to SpellMovementHandler
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
		return SpellCastingValidator.CheckCast(this, caster);
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

				// Animate casting for all human-bodied casters (mounted or not)
				if (ShowHandMovement && m_Caster.Body.IsHuman)
				{
					int count = (int)Math.Ceiling(castDelay.TotalSeconds / AnimateDelay.TotalSeconds);

					if (count != 0)
					{
						m_AnimTimer = new AnimTimer(this, count);
						m_AnimTimer.Start();
					}

					// Hand effects (only visible when not mounted)
					if (!m_Caster.Mounted)
					{
						if (m_Info.LeftHandEffect > 0)
							Caster.FixedParticles(0, 10, 5, m_Info.LeftHandEffect, EffectLayer.LeftHand);

						if (m_Info.RightHandEffect > 0)
							Caster.FixedParticles(0, 10, 5, m_Info.RightHandEffect, EffectLayer.RightHand);
					}
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
			return SpellCastingValidator.ValidateCanCast(this);
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

			if (MidlandSpellModifier.IsInMidland(m_Caster))
			{
				fcr = 0;
			}

			if (AnimalForm.UnderTransformation(m_Caster))
			{
				fcr = 0;
			}

			fcr += MidlandSpellModifier.GetFastCastRecoveryBonus(m_Caster);

			int fcrCap = MyServerSettings.CastRecoveryCap();
			if (fcr > fcrCap)
			{
				fcr = fcrCap;
			}

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
			{
				fcMax = 2;
			}

			int fc = AosAttributes.GetValue(m_Caster, AosAttribute.CastSpeed);

			if (MidlandSpellModifier.IsInMidland(m_Caster) || AnimalForm.UnderTransformation(m_Caster))
			{
				fc = 0;
			}

			if (fc > fcMax)
			{
				fc = fcMax;
			}

			if (ProtectionSpell.Registry.Contains(m_Caster))
			{
				fc -= 2;
			}

			fc += MidlandSpellModifier.GetFastCastBonus(m_Caster);

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
		// Midland logic has been moved to MidlandSpellModifier
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

			if (m_Spell.Caster.Body.IsHuman)
			{
				if (!m_Spell.Caster.Mounted)
				{
					// Standard on-foot casting animation
					if (m_Spell.m_Info.Action >= 0)
						m_Spell.Caster.Animate(m_Spell.m_Info.Action, 7, 1, true, false, 0);
				}
				else
				{
					// Mounted casting animation (27 = mounted spell cast)
					m_Spell.Caster.Animate(27, 7, 1, true, false, 0);
				}
			}

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
						m_Spell.m_Caster.Target.BeginTimeout(m_Spell.m_Caster, TimeSpan.FromSeconds(SpellConstants.SPELL_TARGET_TIMEOUT_SECONDS));

					m_Spell.m_CastTimer = null;
				}
			}
		}

		#endregion
	}
}


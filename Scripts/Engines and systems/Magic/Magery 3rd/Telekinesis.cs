using System;
using System.Collections.Generic;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;

namespace Server.Spells.Third
{
    /// <summary>
    /// Telekinesis - 3rd Circle Utility Spell
    /// Allows manipulation of objects and containers at a distance
    /// </summary>
    public class TelekinesisSpell : MagerySpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Telekinesis", "Ort Por Ylem",
            203,
            9031,
            Reagent.Bloodmoss,
            Reagent.MandrakeRoot);

        public TelekinesisSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

    #region Constants
    // Weight Calculation
    private const int WEIGHT_DIVISOR = 20; // Caster.Int / 20

    // Effect Constants
    private const int EFFECT_ID = 0x376A;
    private const int EFFECT_SPEED = 9;
    private const int EFFECT_RENDER = 32;
    private const int EFFECT_DURATION = 5022;
    private const int SOUND_ID = 0x1F5;
    private const int DEFAULT_HUE = 0;

    // Target Constants
    private const int TARGET_RANGE_ML = 10;
    private const int TARGET_RANGE_LEGACY = 12;
    #endregion

    #region Mobile Grab Easter Egg Constants
    // Chance Calculation
    private const double MOBILE_GRAB_BASE_CHANCE_PER_MAGERY = 0.3;       // 0.3% per magery point
    private const double MOBILE_GRAB_BONUS_CHANCE_PER_INSCRIPTION = 0.15; // 0.15% per inscription point

    // Cooldown
    private const double MOBILE_GRAB_COOLDOWN_SECONDS = 10.0;            // 10 second cooldown

    // Mana Cost Multiplier
    private const double MOBILE_GRAB_MANA_MULTIPLIER = 2.0;              // 2x mana cost even on fail

    // Placement Distance
    private const int MOBILE_GRAB_PLACEMENT_DISTANCE = 1;                // Place 1 tile away from caster

    // Effect Constants
    private const int MOBILE_GRAB_PARTICLE_EFFECT_SUCCESS = 0x3709;      // Whirlwind effect on success
    private const int MOBILE_GRAB_PARTICLE_EFFECT_FAIL = 0x3735;         // Fizzle effect on fail
    private const int MOBILE_GRAB_SOUND_SUCCESS = 0x20F;                 // Teleport sound
    private const int MOBILE_GRAB_SOUND_FAIL = 0x5C;                     // Fizzle sound
    private const int MOBILE_GRAB_EFFECT_HUE = 1153;                     // Special hue for grab effect

    // Messages (PT-BR)
    private const string MSG_GRAB_SUCCESS_CASTER = "Você puxou {0} com telecinesia!";
    private const string MSG_GRAB_SUCCESS_TARGET = "Você foi puxado por {0} com telecinesia!";
    private const string MSG_GRAB_FAIL_CASTER = "Você não conseguiu puxar {0}.";
    private const string MSG_GRAB_FAIL_TARGET_PLAYER = "Você sentiu uma leve tontura. Como se algo estivesse lhe puxando.";
    private const string MSG_GRAB_COOLDOWN = "Você ainda está se recuperando do último uso de telecinesia avançada.";
    private const string MSG_GRAB_INVALID_TARGET = "Você não pode usar telecinesia avançada neste alvo.";
    private const string MSG_GRAB_CANNOT_TARGET_SELF = "Você não pode usar telecinesia em si mesmo.";
    #endregion

        #region Validation Result
        /// <summary>
        /// Represents the result of a validation check
        /// </summary>
        private class ValidationResult
        {
            public bool IsValid { get; private set; }
            public string ErrorMessage { get; private set; }
            public int ErrorColor { get; private set; }

            private ValidationResult(bool isValid, string errorMessage = null, int errorColor = 0)
            {
                IsValid = isValid;
                ErrorMessage = errorMessage;
                ErrorColor = errorColor;
            }

            public static ValidationResult Success()
            {
                return new ValidationResult(true);
            }

            public static ValidationResult Failure(string message, int color)
            {
                return new ValidationResult(false, message, color);
            }
        }
    #endregion

    #region Static Data
    /// <summary>Tracks cooldowns for mobile grab easter egg feature</summary>
    private static Dictionary<Mobile, DateTime> m_MobileGrabCooldowns = new Dictionary<Mobile, DateTime>();
    #endregion

    public override SpellCircle Circle
    {
        get
        {
            return SpellCircle.Third;
        }
    }

        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }

        /// <summary>
        /// Unified target handler that processes all target types efficiently
        /// </summary>
        /// <param name="target">The target object</param>
        public void Target(object target)
        {
            // Validate target exists and is in range
            if (!ValidateTarget(target))
            {
                FinishSequence();
                return;
            }

            // Check spell sequence (mana, reagents, fizzle, etc.)
            if (!CheckSequence())
            {
                FinishSequence();
                return;
            }

        // Turn caster toward target
        SpellHelper.Turn(Caster, target);

        // Easter Egg: Handle mobile grab attempts
        if (target is Mobile)
        {
            HandleMobileGrab((Mobile)target);
            FinishSequence();
            return;
        }

        // Process target based on type (optimized order: interface first, then specific types)
        if (target is ITelekinesisable)
            {
                HandleTelekinesisable((ITelekinesisable)target);
            }
            else if (target is Container)
            {
                HandleContainer((Container)target);
            }
            else if (target is Item)
            {
                HandleItem((Item)target);
            }
            else
            {
                Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.ERROR_SPELL_WONT_WORK);
            }

            FinishSequence();
        }

        /// <summary>
        /// Validates that target exists, is not deleted, and is within range
        /// </summary>
        private bool ValidateTarget(object target)
        {
            if (target == null)
                return false;

            // Check if target is an item and validate it
            if (target is Item)
            {
                Item item = (Item)target;
                if (item.Deleted)
                    return false;

                // Verify item is still in range (safety check - Target system also validates)
                int range = Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY;
                if (!Caster.InRange(item.GetWorldLocation(), range))
                {
                    Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_SPELL_WONT_WORK);
                    return false;
                }
            }
            // Check if target is a mobile and validate it
            else if (target is Mobile)
            {
                Mobile mobile = (Mobile)target;
                if (mobile.Deleted)
                    return false;

                int range = Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY;
                if (!Caster.InRange(mobile.Location, range))
                {
                    Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_SPELL_WONT_WORK);
                    return false;
                }
            }
            // For IPoint3D targets, check range
            else if (target is IPoint3D)
            {
                IPoint3D point = (IPoint3D)target;
                int range = Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY;
                if (!Caster.InRange(new Point3D(point), range))
                {
                    Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_SPELL_WONT_WORK);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Handles ITelekinesisable objects (dice, doors, trapped containers)
        /// </summary>
        private void HandleTelekinesisable(ITelekinesisable obj)
        {
            PlayEffects(obj);
            obj.OnTelekinesis(Caster);
        }

        /// <summary>
        /// Handles container targets
        /// </summary>
        private void HandleContainer(Container container)
        {
            ValidationResult validation = ValidateContainerAccess(container);
            
            if (!validation.IsValid)
            {
                if (!string.IsNullOrEmpty(validation.ErrorMessage))
                {
                    Caster.SendMessage(validation.ErrorColor, validation.ErrorMessage);
                }
                return;
            }

            PlayEffects(container);
            container.OnItemUsed(Caster, container);
        }

        /// <summary>
        /// Handles item grabbing
        /// </summary>
        private void HandleItem(Item item)
        {
            ValidationResult validation = ValidateItemGrab(item);
            
            if (!validation.IsValid)
            {
                Caster.SendMessage(validation.ErrorColor, validation.ErrorMessage);
                return;
            }

            PlayEffects(item);
            Caster.AddToBackpack(item);
            Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.SUCCESS_ITEM_MOVED_TO_BACKPACK);
        }

        /// <summary>
        /// Validates if caster can access the container
        /// </summary>
        private ValidationResult ValidateContainerAccess(Container container)
        {
            if (container == null || container.Deleted)
                return ValidationResult.Failure(Spell.SpellMessages.ERROR_SPELL_WONT_WORK, Spell.MSG_COLOR_ERROR);

            if (!container.IsAccessibleTo(Caster))
            {
                container.OnDoubleClickNotAccessible(Caster);
                return ValidationResult.Failure(null, 0); // Error handled by OnDoubleClickNotAccessible
            }

            if (!container.CheckItemUse(Caster, container))
                return ValidationResult.Failure(null, 0); // Error handled by CheckItemUse

            object root = container.RootParent;
            if (root != null && root is Mobile && root != Caster)
            {
                container.OnSnoop(Caster);
                return ValidationResult.Failure(null, 0); // Error handled by OnSnoop
            }

            Corpse corpse = container as Corpse;
            if (corpse != null && !corpse.CheckLoot(Caster, null))
                return ValidationResult.Failure(null, 0); // Error handled by CheckLoot

            if (!Caster.Region.OnDoubleClick(Caster, container))
                return ValidationResult.Failure(null, 0); // Error handled by region

            return ValidationResult.Success();
        }

        /// <summary>
        /// Validates if an item can be grabbed with telekinesis
        /// </summary>
        private ValidationResult ValidateItemGrab(Item item)
        {
            if (item == null || item.Deleted)
                return ValidationResult.Failure(Spell.SpellMessages.ERROR_SPELL_WONT_WORK, Spell.MSG_COLOR_ERROR);

            if (!item.Movable)
                return ValidationResult.Failure(Spell.SpellMessages.ERROR_ITEM_NOT_MOVABLE, Spell.MSG_COLOR_ERROR);

            if (item.Amount > 1)
                return ValidationResult.Failure(Spell.SpellMessages.ERROR_TOO_MANY_ITEMS_STACKED, Spell.MSG_COLOR_ERROR);

            int maxWeight = Caster.Int / WEIGHT_DIVISOR;
            if (item.Weight > maxWeight)
                return ValidationResult.Failure(Spell.SpellMessages.ERROR_ITEM_TOO_HEAVY, Spell.MSG_COLOR_ERROR);

            if (item.RootParentEntity != null)
                return ValidationResult.Failure(Spell.SpellMessages.ERROR_CANNOT_MOVE_WORN_ITEMS, Spell.MSG_COLOR_ERROR);

            return ValidationResult.Success();
        }

        #region Mobile Grab Easter Egg Methods

        /// <summary>
        /// Checks if the caster is on cooldown for mobile grab
        /// </summary>
        private bool CheckMobileGrabCooldown()
        {
            if (m_MobileGrabCooldowns.ContainsKey(Caster))
            {
                DateTime cooldownEnd = m_MobileGrabCooldowns[Caster];
                if (DateTime.UtcNow < cooldownEnd)
                {
                    Caster.SendMessage(Spell.MSG_COLOR_ERROR, MSG_GRAB_COOLDOWN);
                    return false;
                }
                
                // Remove expired cooldown
                m_MobileGrabCooldowns.Remove(Caster);
            }
            
            return true;
        }

        /// <summary>
        /// Sets the mobile grab cooldown for the caster
        /// </summary>
        private void SetMobileGrabCooldown()
        {
            m_MobileGrabCooldowns[Caster] = DateTime.UtcNow.AddSeconds(MOBILE_GRAB_COOLDOWN_SECONDS);
        }

        /// <summary>
        /// Calculates the success chance for mobile grab based on Magery and Inscription
        /// </summary>
        private double CalculateMobileGrabChance()
        {
            double mageryChance = Caster.Skills[SkillName.Magery].Value * MOBILE_GRAB_BASE_CHANCE_PER_MAGERY;
            double inscribeBonus = Caster.Skills[SkillName.Inscribe].Value * MOBILE_GRAB_BONUS_CHANCE_PER_INSCRIPTION;
            double totalChance = mageryChance + inscribeBonus;
            
            return Math.Min(totalChance, 100.0); // Cap at 100%
        }

        /// <summary>
        /// Finds a valid location 1 tile away from caster to place the grabbed mobile
        /// </summary>
        private Point3D FindValidPlacement()
        {
            // Try to find a valid location 1 tile away from caster
            Point3D casterLoc = Caster.Location;
            
            for (int xOffset = -MOBILE_GRAB_PLACEMENT_DISTANCE; xOffset <= MOBILE_GRAB_PLACEMENT_DISTANCE; xOffset++)
            {
                for (int yOffset = -MOBILE_GRAB_PLACEMENT_DISTANCE; yOffset <= MOBILE_GRAB_PLACEMENT_DISTANCE; yOffset++)
                {
                    // Skip center (caster's position) and positions further than 1 tile
                    if (xOffset == 0 && yOffset == 0)
                        continue;
                    if (Math.Abs(xOffset) + Math.Abs(yOffset) > MOBILE_GRAB_PLACEMENT_DISTANCE)
                        continue;
                    
                    Point3D testLoc = new Point3D(casterLoc.X + xOffset, casterLoc.Y + yOffset, casterLoc.Z);
                    
                    if (Caster.Map.CanSpawnMobile(testLoc))
                        return testLoc;
                }
            }
            
            // No valid location found, return caster location as fallback
            return casterLoc;
        }

        /// <summary>
        /// Validates that a mobile can be grabbed with telekinesis
        /// </summary>
        private ValidationResult ValidateMobileGrab(Mobile target)
        {
            // Check if target is deleted
            if (target == null || target.Deleted)
                return ValidationResult.Failure(MSG_GRAB_INVALID_TARGET, Spell.MSG_COLOR_ERROR);

            // Check if targeting self
            if (target == Caster)
                return ValidationResult.Failure(MSG_GRAB_CANNOT_TARGET_SELF, Spell.MSG_COLOR_ERROR);

            // Check if target is alive
            if (!target.Alive)
                return ValidationResult.Failure(MSG_GRAB_INVALID_TARGET, Spell.MSG_COLOR_ERROR);

            // Check if target is hidden
            if (target.Hidden)
                return ValidationResult.Failure(MSG_GRAB_INVALID_TARGET, Spell.MSG_COLOR_ERROR);

            // Check if target is blessed
            if (target.Blessed)
                return ValidationResult.Failure(MSG_GRAB_INVALID_TARGET, Spell.MSG_COLOR_ERROR);

            // Check AccessLevel (must be Player)
            if (target.AccessLevel != AccessLevel.Player)
                return ValidationResult.Failure(MSG_GRAB_INVALID_TARGET, Spell.MSG_COLOR_ERROR);

            // Check if in safe zone / anti-magic region
            if (!SpellHelper.CheckTravel(Caster, Caster.Map, Caster.Location, TravelCheckType.TeleportFrom))
                return ValidationResult.Failure(Spell.SpellMessages.ERROR_SPELL_WONT_WORK, Spell.MSG_COLOR_ERROR);

            if (!SpellHelper.CheckTravel(Caster, target.Map, target.Location, TravelCheckType.TeleportTo))
                return ValidationResult.Failure(Spell.SpellMessages.ERROR_SPELL_WONT_WORK, Spell.MSG_COLOR_ERROR);

            return ValidationResult.Success();
        }

        /// <summary>
        /// Plays success effects for mobile grab
        /// </summary>
        private void PlayGrabSuccessEffects(Mobile target)
        {
            // Whirlwind effect on target
            target.FixedParticles(MOBILE_GRAB_PARTICLE_EFFECT_SUCCESS, 10, 30, 5052, MOBILE_GRAB_EFFECT_HUE, 0, EffectLayer.Waist);
            target.PlaySound(MOBILE_GRAB_SOUND_SUCCESS);
            
            // Effect at destination
            Effects.SendLocationParticles(EffectItem.Create(target.Location, target.Map, EffectItem.DefaultDuration), 
                EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, MOBILE_GRAB_EFFECT_HUE, 0, EFFECT_DURATION, 0);
        }

        /// <summary>
        /// Plays failure effects for mobile grab
        /// </summary>
        private void PlayGrabFailEffects(Mobile target)
        {
            // Fizzle effect
            target.FixedParticles(MOBILE_GRAB_PARTICLE_EFFECT_FAIL, 1, 30, 9503, EffectLayer.Waist);
            target.PlaySound(MOBILE_GRAB_SOUND_FAIL);
        }

        /// <summary>
        /// Easter Egg: Attempts to grab and teleport a mobile to the caster
        /// Costs 2x mana even on failure, has a cooldown, and success based on Magery/Inscription
        /// </summary>
        private void HandleMobileGrab(Mobile target)
        {
            // Check cooldown first
            if (!CheckMobileGrabCooldown())
                return;

            // Validate target
            ValidationResult validation = ValidateMobileGrab(target);
            if (!validation.IsValid)
            {
                Caster.SendMessage(validation.ErrorColor, validation.ErrorMessage);
                return;
            }

            // Deduct 2x mana cost (even if it fails)
            int manaCost = ScaleMana(GetMana());
            int doubleCost = (int)(manaCost * MOBILE_GRAB_MANA_MULTIPLIER);
            Caster.Mana -= doubleCost;

            // Set cooldown
            SetMobileGrabCooldown();

            // Calculate success chance
            double successChance = CalculateMobileGrabChance();
            bool success = Utility.RandomDouble() * 100.0 < successChance;

            if (success)
            {
                // SUCCESS - Teleport target to near caster
                Point3D destination = FindValidPlacement();
                
                PlayGrabSuccessEffects(target);
                target.MoveToWorld(destination, Caster.Map);
                
                Caster.SendMessage(Spell.MSG_COLOR_HEAL, string.Format(MSG_GRAB_SUCCESS_CASTER, target.Name));
                target.SendMessage(Spell.MSG_COLOR_WARNING, string.Format(MSG_GRAB_SUCCESS_TARGET, Caster.Name));
            }
            else
            {
                // FAILURE - Play effects and send messages
                PlayGrabFailEffects(target);
                
                Caster.SendMessage(Spell.MSG_COLOR_ERROR, string.Format(MSG_GRAB_FAIL_CASTER, target.Name));
                
                // Only send dizzy message to players, not NPCs
                if (target.Player)
                {
                    target.SendMessage(Spell.MSG_COLOR_WARNING, MSG_GRAB_FAIL_TARGET_PLAYER);
                }
            }
        }

        #endregion

        /// <summary>
        /// Plays visual and sound effects for the spell at target location
        /// </summary>
        private void PlayEffects(IPoint3D target)
        {
            if (target == null)
                return;

            Point3D location = new Point3D(target);
            Map map = null;

            // Get map from target if it's an item or mobile
            if (target is Item)
            {
                Item item = (Item)target;
                map = item.Map;
            }
            else if (target is Mobile)
            {
                Mobile mobile = (Mobile)target;
                map = mobile.Map;
            }

            if (map == null || map == Map.Internal)
                return;

            int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE);
            Effects.SendLocationParticles(EffectItem.Create(location, map, EffectItem.DefaultDuration), EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, hue, 0, EFFECT_DURATION, 0);
            Effects.PlaySound(location, map, SOUND_ID);
        }

        public class InternalTarget : Target
        {
            private readonly TelekinesisSpell m_Owner;

            public InternalTarget(TelekinesisSpell owner) : base(Core.ML ? TARGET_RANGE_ML : TARGET_RANGE_LEGACY, false, TargetFlags.None)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                // Unified target handler processes all types
                m_Owner.Target(o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                // FinishSequence is now handled in unified Target method
            }
        }
    }
}

namespace Server
{
    public interface ITelekinesisable : IPoint3D
    {
        void OnTelekinesis(Mobile from);
    }
}
using System;
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
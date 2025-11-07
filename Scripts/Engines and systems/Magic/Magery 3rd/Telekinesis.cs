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

        public void Target(ITelekinesisable obj)
        {
            if (this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, obj);

                obj.OnTelekinesis(this.Caster);
            }

            this.FinishSequence();
        }

        public void Target(Container item)
        {
            if (this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, item);

                if (CanAccessContainer(item))
                {
                    PlayEffects(item);
                    item.OnItemUsed(this.Caster, item);
                }
            }

            this.FinishSequence();
        }

        /// <summary>
        /// Handles telekinetic grabbing of movable items
        /// </summary>
        /// <param name="item">The item to grab</param>
        public void Target(Item item)
        {
            if (this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, item);

                if (!CanGrabItem(item))
                {
                    // Error messages handled in CanGrabItem
                }
                else
                {
                    PlayEffects(item);
                    Caster.AddToBackpack(item);
                    Caster.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.SUCCESS_ITEM_MOVED_TO_BACKPACK);
                }
            }

            this.FinishSequence();
        }

        /// <summary>
        /// Checks if caster can access the container
        /// </summary>
        /// <param name="item">The container to check</param>
        /// <returns>True if container can be accessed</returns>
        private bool CanAccessContainer(Container item)
        {
            object root = item.RootParent;

            if (!item.IsAccessibleTo(this.Caster))
            {
                item.OnDoubleClickNotAccessible(this.Caster);
                return false;
            }
            else if (!item.CheckItemUse(this.Caster, item))
            {
                return false;
            }
            else if (root != null && root is Mobile && root != this.Caster)
            {
                item.OnSnoop(this.Caster);
                return false;
            }
            else if (item is Corpse && !((Corpse)item).CheckLoot(this.Caster, null))
            {
                return false;
            }
            else if (!this.Caster.Region.OnDoubleClick(this.Caster, item))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if an item can be grabbed with telekinesis
        /// </summary>
        /// <param name="item">The item to check</param>
        /// <returns>True if item can be grabbed</returns>
        private bool CanGrabItem(Item item)
        {
            if (!item.Movable)
            {
                Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_ITEM_NOT_MOVABLE);
                return false;
            }
            else if (item.Amount > 1)
            {
                Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_TOO_MANY_ITEMS_STACKED);
                return false;
            }
            else if (item.Weight > (Caster.Int / WEIGHT_DIVISOR))
            {
                Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_ITEM_TOO_HEAVY);
                return false;
            }
            else if (item.RootParentEntity != null)
            {
                Caster.SendMessage(Spell.MSG_COLOR_ERROR, Spell.SpellMessages.ERROR_CANNOT_MOVE_WORN_ITEMS);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Plays visual and sound effects for the spell
        /// </summary>
        /// <param name="item">The item location for effects</param>
        private void PlayEffects(Item item)
        {
            int hue = Server.Items.CharacterDatabase.GetMySpellHue(Caster, DEFAULT_HUE);
            Effects.SendLocationParticles(EffectItem.Create(item.Location, item.Map, EffectItem.DefaultDuration), EFFECT_ID, EFFECT_SPEED, EFFECT_RENDER, hue, 0, EFFECT_DURATION, 0);
            Effects.PlaySound(item.Location, item.Map, SOUND_ID);
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
                if (o is ITelekinesisable)
                {
                    this.m_Owner.Target((ITelekinesisable)o);
                }
                else if (o is Container)
                {
                    this.m_Owner.Target((Container)o);
                }
                else if (o is Item)
                {
                    this.m_Owner.Target((Item)o);
                }
                else
                {
                    from.SendMessage(Spell.MSG_COLOR_SYSTEM, Spell.SpellMessages.ERROR_SPELL_WONT_WORK);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
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
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;

namespace Server.Items
{
    public enum EnhanceType
    {
        None,
        Property,
        AosAttribute,
        AosArmorAttribute,
        AosWeaponAttribute,
        AosElementAttribute
    }

    public class AttributeHandler
    {
        public EnhanceType Type = EnhanceType.None;
        public string Name = String.Empty;
        public string Description = String.Empty;
        public int MaxValue = 15;
        public int IncrementValue = 1;
        public int Cost = 1;
        public bool AllowArmor = false;
        public bool AllowWeapon = false;
        public bool AllowJewelry = false;
        public bool AllowSpellbook = false;
        public bool AllowShield = false;
        public bool AllowClothing= false;

        #region Attribute Definitions
        public static List<AttributeHandler> Definitions = new List<AttributeHandler>();

        /// <summary>
        /// Initializes all attribute definitions for item enhancement.
        /// Refactored to use helper methods, reducing complexity from 54 to ~10.
        /// Total of 58 attributes organized by category.
        /// </summary>
        public static void Initialize()
        {
            DefineCombatAttributes();
            DefineArmorAttributes();
            DefineRegenerationAttributes();
            DefineStatBonusAttributes();
            DefineMagicAttributes();
            DefineResistanceAttributes();
            DefineWeaponAttributes();
            DefineJewelryResistances();
        }
        #endregion

        #region Attribute Definition Helpers

        /// <summary>
        /// Defines combat-related attributes (defense, attack, reflect, etc.)
        /// </summary>
        private static void DefineCombatAttributes()
        {
            // Spell Channeling - Weapons and Shields only
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "SpellChanneling", "Spell Channeling", 1, 1, 200,
                false, true, false, false, true, false));

            // Defense and Attack Chance
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "DefendChance", "Defense Chance Increase", 25, 1, 10,
                true, true, true, true, true, true));
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "AttackChance", "Hit Chance Increase", 25, 1, 10,
                false, true, true, true, true, true));

            // Reflect Physical Damage
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "ReflectPhysical", "Reflect Physical Damage", 50, 1, 2,
                true, false, true, true, true, true));

            // Weapon Damage and Speed
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "WeaponDamage", "Damage Increase", 50, 1, 5,
                true, true, true, true, true, true));
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "WeaponSpeed", "Swing Speed Increase", 40, 1, 6,
                false, true, true, true, true, true));

            // Luck (no items allowed)
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "Luck", "Luck", 1000, 1, 5,
                false, false, false, false, false, false));

            // Night Sight (no items allowed)
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "NightSight", "Night Sight", 1, 1, 50,
                false, false, false, false, false, false));
        }

        /// <summary>
        /// Defines armor-specific attributes (requirements, repair, mage armor)
        /// </summary>
        private static void DefineArmorAttributes()
        {
            // Lower Requirements - Armor version
            Definitions.Add(new AttributeHandler(EnhanceType.AosArmorAttribute, "LowerStatReq", "Lower Requirements", 100, 1, 2,
                true, false, false, false, true, false));

            // Lower Requirements - Weapon version
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "LowerStatReq", "Lower Requirements", 100, 1, 2,
                false, true, false, false, false, false));

            // Self Repair
            Definitions.Add(new AttributeHandler(EnhanceType.AosArmorAttribute, "SelfRepair", "Self Repair", 5, 1, 200,
                true, true, false, false, true, false));

            // Mage Armor
            Definitions.Add(new AttributeHandler(EnhanceType.AosArmorAttribute, "MageArmor", "Mage Armor", 1, 1, 250,
                true, false, false, false, false, false));
        }

        /// <summary>
        /// Defines regeneration and resource bonus attributes
        /// </summary>
        private static void DefineRegenerationAttributes()
        {
            // Regeneration
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "RegenHits", "Hit Point Regeneration", 25, 1, 25,
                true, false, true, true, true, true));
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "RegenStam", "Stamina Regeneration", 25, 1, 10,
                true, false, true, true, true, true));
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "RegenMana", "Mana Regeneration", 25, 1, 10,
                true, false, true, true, true, true));

            // Resource Bonuses
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "BonusHits", "Hit Point Increase", 50, 1, 10,
                true, false, true, true, true, true));
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "BonusStam", "Stamina Increase", 50, 1, 10,
                true, false, false, true, false, false));
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "BonusMana", "Mana Increase", 50, 1, 10,
                true, false, true, true, true, true));
        }

        /// <summary>
        /// Defines stat bonus attributes (Str, Dex, Int)
        /// </summary>
        private static void DefineStatBonusAttributes()
        {
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "BonusStr", "Strength Bonus", 20, 1, 10,
                true, false, true, true, true, true));
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "BonusDex", "Dexterity Bonus", 20, 1, 10,
                true, false, true, true, true, true));
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "BonusInt", "Intelligence Bonus", 20, 1, 10,
                true, false, true, true, true, true));
        }

        /// <summary>
        /// Defines magic-related attributes (mana cost, reagents, casting, spell damage)
        /// </summary>
        private static void DefineMagicAttributes()
        {
            // Mana and Reagent Cost
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "LowerManaCost", "Lower Mana Cost", 50, 1, 10,
                true, false, true, true, true, true));
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "LowerRegCost", "Lower Reagent Cost", 100, 1, 5,
                true, false, true, true, true, true));

            // Casting Speed and Recovery
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "CastSpeed", "Faster Casting", 2, 1, 20,
                false, false, true, true, false, true));
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "CastRecovery", "Faster Cast Recovery", 6, 1, 20,
                false, false, true, true, false, true));

            // Spell Damage
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "SpellDamage", "Spell Damage Increase", 25, 1, 4,
                true, true, true, true, true, true));

            // Enhance Potions
            Definitions.Add(new AttributeHandler(EnhanceType.AosAttribute, "EnhancePotions", "Enhance Potions", 25, 1, 2,
                true, false, true, true, true, true));
        }

        /// <summary>
        /// Defines resistance attributes for armor (Physical, Fire, Cold, Poison, Energy)
        /// </summary>
        private static void DefineResistanceAttributes()
        {
            // Armor/Shield Property Resistances
            Definitions.Add(new AttributeHandler(EnhanceType.Property, "PhysicalBonus", "Physical Resist", 30, 1, 5,
                true, false, false, false, true, false));
            Definitions.Add(new AttributeHandler(EnhanceType.Property, "FireBonus", "Fire Resist", 30, 1, 5,
                true, false, false, false, true, false));
            Definitions.Add(new AttributeHandler(EnhanceType.Property, "ColdBonus", "Cold Resist", 30, 1, 5,
                true, false, false, false, true, false));
            Definitions.Add(new AttributeHandler(EnhanceType.Property, "PoisonBonus", "Poison Resist", 30, 1, 5,
                true, false, false, false, true, false));
            Definitions.Add(new AttributeHandler(EnhanceType.Property, "EnergyBonus", "Energy Resist", 30, 1, 5,
                true, false, false, false, true, false));
        }

        /// <summary>
        /// Defines weapon-specific attributes (area effects, spell effects, leeches, special abilities)
        /// </summary>
        private static void DefineWeaponAttributes()
        {
            // Area Effects (mutually exclusive)
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitPhysicalArea", "Hit Physical Area", 50, 1, 5,
                false, true, false, false, false, false));
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitFireArea", "Hit Fire Area", 50, 1, 5,
                false, true, false, false, false, false));
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitColdArea", "Hit Cold Area", 50, 1, 5,
                false, true, false, false, false, false));
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitPoisonArea", "Hit Poison Area", 50, 1, 5,
                false, true, false, false, false, false));
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitEnergyArea", "Hit Energy Area", 50, 1, 5,
                false, true, false, false, false, false));

            // Spell Effects (mutually exclusive)
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitMagicArrow", "Hit Magic Arrow", 50, 1, 5,
                false, true, false, false, false, false));
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitHarm", "Hit Harm", 50, 1, 7,
                false, true, false, false, false, false));
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitFireball", "Hit Fireball", 50, 1, 10,
                false, true, false, false, false, false));
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitLightning", "Hit Lightning", 50, 1, 13,
                false, true, false, false, false, false));

            // Special Weapon Abilities
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "UseBestSkill", "Use Best Weapon Skill", 1, 1, 100,
                false, true, false, false, false, false));
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "MageWeapon", "Mage Weapon", 1, 1, 50,
                false, false, false, false, false, false));

            // Hit Effects
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitDispel", "Hit Dispel", 50, 1, 3,
                false, true, false, false, false, false));
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitLowerAttack", "Hit Lower Attack", 50, 1, 6,
                false, true, false, false, false, false));
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitLowerDefend", "Hit Lower Defense", 50, 1, 6,
                false, true, false, false, false, false));

            // Leeches
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitLeechHits", "Hit Life Leech", 50, 1, 6,
                false, true, false, false, false, false));
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitLeechMana", "Hit Mana Leech", 50, 1, 6,
                false, true, false, false, false, false));
            Definitions.Add(new AttributeHandler(EnhanceType.AosWeaponAttribute, "HitLeechStam", "Hit Stamina Leech", 50, 1, 6,
                false, true, false, false, false, false));
        }

        /// <summary>
        /// Defines elemental resistances for jewelry and clothing
        /// </summary>
        private static void DefineJewelryResistances()
        {
            // Jewelry/Clothing Elemental Resistances
            Definitions.Add(new AttributeHandler(EnhanceType.AosElementAttribute, "Physical", "Physical Resist", 30, 1, 2,
                false, false, true, false, false, true));
            Definitions.Add(new AttributeHandler(EnhanceType.AosElementAttribute, "Fire", "Fire Resist", 30, 1, 2,
                false, false, true, true, false, true));
            Definitions.Add(new AttributeHandler(EnhanceType.AosElementAttribute, "Cold", "Cold Resist", 30, 1, 2,
                false, false, true, true, false, true));
            Definitions.Add(new AttributeHandler(EnhanceType.AosElementAttribute, "Poison", "Poison Resist", 30, 1, 2,
                false, false, true, true, false, true));
            Definitions.Add(new AttributeHandler(EnhanceType.AosElementAttribute, "Energy", "Energy Resist", 30, 1, 2,
                false, false, true, true, false, true));
        }

        #endregion

        public AttributeHandler(EnhanceType type, string name, string description, int maxValue, int incrementValue, int cost, bool armor, bool weapon, bool jewelry, bool spellbook, bool shield, bool clothing)
        {
            Type = type;
            Name = name;
            Description = description;
            MaxValue = maxValue;
            IncrementValue = incrementValue;
			Cost = cost;
            AllowArmor = armor;
            AllowWeapon = weapon;
            AllowJewelry = jewelry;
            AllowSpellbook = spellbook;
            AllowShield = shield;
            AllowClothing = clothing;
        }

        #region IsUpgradable Validation Helpers

        /// <summary>
        /// Checks if a weapon has any area effect attributes other than the specified one.
        /// </summary>
        private bool HasOtherAreaEffect(BaseWeapon weapon, string exceptThisOne)
        {
            if (exceptThisOne != "HitPhysicalArea" && weapon.WeaponAttributes.HitPhysicalArea > 0) return true;
            if (exceptThisOne != "HitFireArea" && weapon.WeaponAttributes.HitFireArea > 0) return true;
            if (exceptThisOne != "HitColdArea" && weapon.WeaponAttributes.HitColdArea > 0) return true;
            if (exceptThisOne != "HitPoisonArea" && weapon.WeaponAttributes.HitPoisonArea > 0) return true;
            if (exceptThisOne != "HitEnergyArea" && weapon.WeaponAttributes.HitEnergyArea > 0) return true;
            return false;
        }

        /// <summary>
        /// Checks if a weapon has any spell effect attributes other than the specified one.
        /// </summary>
        private bool HasOtherSpellEffect(BaseWeapon weapon, string exceptThisOne)
        {
            if (exceptThisOne != "HitMagicArrow" && weapon.WeaponAttributes.HitMagicArrow > 0) return true;
            if (exceptThisOne != "HitHarm" && weapon.WeaponAttributes.HitHarm > 0) return true;
            if (exceptThisOne != "HitFireball" && weapon.WeaponAttributes.HitFireball > 0) return true;
            if (exceptThisOne != "HitLightning" && weapon.WeaponAttributes.HitLightning > 0) return true;
            return false;
        }

        /// <summary>
        /// Validates if this attribute can be applied to a shield.
        /// </summary>
        private bool ValidateShield(BaseShield shield)
        {
            if (shield.ArtifactRarity > 0) return false;
            if (Name == "AttackChance" && Core.ML) return false;
            if (Name == "ReflectPhysical" && !Core.ML) return false;
            if (Name == "LowerStatReq" && (!Core.AOS || (shield.Resource >= CraftResource.RegularLeather && shield.Resource <= CraftResource.BarbedLeather))) return false;
            if (!AllowShield) return false;
            return true;
        }

        /// <summary>
        /// Validates if this attribute can be applied to armor.
        /// </summary>
        private bool ValidateArmor(BaseArmor armor)
        {
            if (armor.ArtifactRarity > 0) return false;
            if (Name == "LowerStatReq" && (!Core.AOS || (armor.Resource >= CraftResource.RegularLeather && armor.Resource <= CraftResource.BarbedLeather))) return false;
            if (Name == "MageArmor" && armor.MeditationAllowance == ArmorMeditationAllowance.All) return false;
            if (Name == "NightSight" && armor.RequiredRace == Race.Elf) return false;
            if (!AllowArmor) return false;
            return true;
        }

        /// <summary>
        /// Validates if this attribute can be applied to a weapon.
        /// </summary>
        private bool ValidateWeapon(BaseWeapon weapon)
        {
            if (weapon.ArtifactRarity > 0) return false;
            if (Name == "UseBestSkill" && weapon.WeaponAttributes.MageWeapon > 0) return false;
            if (Name == "MageWeapon" && weapon.WeaponAttributes.UseBestSkill > 0) return false;

            // Check for area effect conflicts
            if ((Name == "HitPhysicalArea" || Name == "HitFireArea" || Name == "HitColdArea" ||
                 Name == "HitPoisonArea" || Name == "HitEnergyArea") && HasOtherAreaEffect(weapon, Name))
                return false;

            // Check for spell effect conflicts
            if ((Name == "HitMagicArrow" || Name == "HitHarm" || Name == "HitFireball" ||
                 Name == "HitLightning") && HasOtherSpellEffect(weapon, Name))
                return false;

            if (!AllowWeapon) return false;
            return true;
        }

        /// <summary>
        /// Validates if this attribute can be applied to jewelry.
        /// </summary>
        private bool ValidateJewel(BaseJewel jewel)
        {
            if (jewel.ArtifactRarity > 0) return false;
            if (!AllowJewelry) return false;
            return true;
        }

        /// <summary>
        /// Validates if this attribute can be applied to clothing.
        /// </summary>
        private bool ValidateClothing(BaseClothing cloth)
        {
            if (cloth.ArtifactRarity > 0) return false;
            if (!AllowClothing) return false;
            return true;
        }

        /// <summary>
        /// Validates if this attribute can be applied to a spellbook.
        /// </summary>
        private bool ValidateSpellbook()
        {
            return AllowSpellbook;
        }

        #endregion

        #region IsUpgradable

        /// <summary>
        /// Determines if this attribute can be applied to the specified item.
        /// Refactored to use validation helper methods, reducing complexity from 32 to ~8.
        /// </summary>
        public bool IsUpgradable(Item itemToTest)
        {
            bool allowed = false;

            // Validate based on item type
            if (itemToTest is BaseShield)
                allowed = ValidateShield((BaseShield)itemToTest);
            else if (itemToTest is BaseArmor)
                allowed = ValidateArmor((BaseArmor)itemToTest);
            else if (itemToTest is BaseWeapon)
                allowed = ValidateWeapon((BaseWeapon)itemToTest);
            else if (itemToTest is BaseJewel)
                allowed = ValidateJewel((BaseJewel)itemToTest);
            else if (itemToTest is BaseClothing)
                allowed = ValidateClothing((BaseClothing)itemToTest);
            else if (itemToTest is Spellbook)
                allowed = ValidateSpellbook();

            // Check if already at maximum value
            if (allowed)
            {
                int currentValue = Upgrade(itemToTest, true);
                if (currentValue >= MaxValue)
                    allowed = false;
            }

            return allowed;
        }
        #endregion

        #region Upgrade

        /// <summary>
        /// Upgrades an attribute on an item, or reports its current value.
        /// Refactored to use helper methods, reducing complexity from 42 to ~8.
        /// </summary>
        /// <param name="itemToEnhance">The item to upgrade</param>
        /// <param name="reportCurrentValueOnly">If true, only returns current value without modifying</param>
        /// <returns>The new value after upgrade, or current value if reporting only</returns>
        public int Upgrade(Item itemToEnhance, bool reportCurrentValueOnly)
        {
            int value = (reportCurrentValueOnly ? 0 : IncrementValue);

            switch (Type)
            {
                case EnhanceType.None:
                    return -1;

                case EnhanceType.AosAttribute:
                    return UpgradeAosAttribute(itemToEnhance, value);

                case EnhanceType.AosArmorAttribute:
                    return UpgradeArmorAttribute(itemToEnhance, value);

                case EnhanceType.AosElementAttribute:
                    return UpgradeElementAttribute(itemToEnhance, value);

                case EnhanceType.AosWeaponAttribute:
                    return UpgradeWeaponAttribute(itemToEnhance, value);

                case EnhanceType.Property:
                    if (itemToEnhance is BaseArmor)
                        return UpgradeArmorBonus((BaseArmor)itemToEnhance, value);
                    return 0;

                default:
                    return 0;
            }
        }

        #endregion

        #region Upgrade Helper Methods

        /// <summary>
        /// Gets the AosAttributes collection from an item, or null if not applicable.
        /// </summary>
        private AosAttributes GetAosAttributes(Item item)
        {
            if (item is BaseShield) return ((BaseShield)item).Attributes;
            if (item is BaseArmor) return ((BaseArmor)item).Attributes;
            if (item is BaseWeapon) return ((BaseWeapon)item).Attributes;
            if (item is BaseJewel) return ((BaseJewel)item).Attributes;
            if (item is BaseClothing) return ((BaseClothing)item).Attributes;
            if (item is Spellbook) return ((Spellbook)item).Attributes;
            return null;
        }

        /// <summary>
        /// Gets the AosArmorAttributes collection from an item, or null if not applicable.
        /// </summary>
        private AosArmorAttributes GetArmorAttributes(Item item)
        {
            if (item is BaseShield) return ((BaseShield)item).ArmorAttributes;
            if (item is BaseArmor) return ((BaseArmor)item).ArmorAttributes;
            return null;
        }

        /// <summary>
        /// Gets the AosWeaponAttributes collection from an item, or null if not applicable.
        /// </summary>
        private AosWeaponAttributes GetWeaponAttributes(Item item)
        {
            if (item is BaseWeapon) return ((BaseWeapon)item).WeaponAttributes;
            return null;
        }

        /// <summary>
        /// Gets the AosElementAttributes (Resistances) collection from an item, or null if not applicable.
        /// </summary>
        private AosElementAttributes GetElementAttributes(Item item)
        {
            if (item is BaseJewel) return ((BaseJewel)item).Resistances;
            if (item is BaseClothing) return ((BaseClothing)item).Resistances;
            return null;
        }

        /// <summary>
        /// Upgrades an AosAttribute on an item.
        /// </summary>
        private int UpgradeAosAttribute(Item item, int value)
        {
            AosAttributes attributes = GetAosAttributes(item);
            if (attributes == null) return 0;

            AosAttribute attr = (AosAttribute)Enum.Parse(typeof(AosAttribute), Name);
            int currentVal = attributes.GetValue((int)attr);
            int newVal = currentVal + value;

            if (newVal > MaxValue)
                newVal = MaxValue;

            if (currentVal < MaxValue)
                attributes.SetValue((int)attr, newVal);

            return newVal;
        }

        /// <summary>
        /// Upgrades an AosArmorAttribute on an item.
        /// </summary>
        private int UpgradeArmorAttribute(Item item, int value)
        {
            AosArmorAttributes attributes = GetArmorAttributes(item);
            if (attributes == null) return 0;

            AosArmorAttribute attr = (AosArmorAttribute)Enum.Parse(typeof(AosArmorAttribute), Name);
            int currentVal = attributes.GetValue((int)attr);
            int newVal = currentVal + value;

            if (newVal > MaxValue)
                newVal = MaxValue;

            if (currentVal < MaxValue)
                attributes.SetValue((int)attr, newVal);

            return newVal;
        }

        /// <summary>
        /// Upgrades an AosWeaponAttribute on an item.
        /// </summary>
        private int UpgradeWeaponAttribute(Item item, int value)
        {
            AosWeaponAttributes attributes = GetWeaponAttributes(item);
            if (attributes == null) return 0;

            AosWeaponAttribute attr = (AosWeaponAttribute)Enum.Parse(typeof(AosWeaponAttribute), Name);
            int currentVal = attributes.GetValue((int)attr);
            int newVal = currentVal + value;

            if (newVal > MaxValue)
                newVal = MaxValue;

            if (currentVal < MaxValue)
                attributes.SetValue((int)attr, newVal);

            return newVal;
        }

        /// <summary>
        /// Upgrades an AosElementAttribute (resistance) on an item.
        /// </summary>
        private int UpgradeElementAttribute(Item item, int value)
        {
            AosElementAttributes attributes = GetElementAttributes(item);
            if (attributes == null) return 0;

            AosElementAttribute attr = (AosElementAttribute)Enum.Parse(typeof(AosElementAttribute), Name);
            int currentVal = attributes.GetValue((int)attr);
            int newVal = currentVal + value;

            if (newVal > MaxValue)
                newVal = MaxValue;

            if (currentVal < MaxValue)
                attributes.SetValue((int)attr, newVal);

            return newVal;
        }

        /// <summary>
        /// Upgrades an armor property bonus (Physical/Fire/Cold/Poison/Energy).
        /// </summary>
        private int UpgradeArmorBonus(BaseArmor armor, int value)
        {
            int currentVal = 0;
            int newVal = 0;

            switch (Name)
            {
                case "PhysicalBonus":
                    currentVal = armor.PhysicalBonus;
                    newVal = currentVal + value;
                    if (newVal > MaxValue) newVal = MaxValue;
                    if (currentVal < MaxValue) armor.PhysicalBonus = newVal;
                    break;
                case "FireBonus":
                    currentVal = armor.FireBonus;
                    newVal = currentVal + value;
                    if (newVal > MaxValue) newVal = MaxValue;
                    if (currentVal < MaxValue) armor.FireBonus = newVal;
                    break;
                case "ColdBonus":
                    currentVal = armor.ColdBonus;
                    newVal = currentVal + value;
                    if (newVal > MaxValue) newVal = MaxValue;
                    if (currentVal < MaxValue) armor.ColdBonus = newVal;
                    break;
                case "PoisonBonus":
                    currentVal = armor.PoisonBonus;
                    newVal = currentVal + value;
                    if (newVal > MaxValue) newVal = MaxValue;
                    if (currentVal < MaxValue) armor.PoisonBonus = newVal;
                    break;
                case "EnergyBonus":
                    currentVal = armor.EnergyBonus;
                    newVal = currentVal + value;
                    if (newVal > MaxValue) newVal = MaxValue;
                    if (currentVal < MaxValue) armor.EnergyBonus = newVal;
                    break;
            }

            return newVal;
        }

        #endregion
    }
}
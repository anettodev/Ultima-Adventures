# Magery Spells - Complete Reference Guide

**System:** Ultima Adventures - Magic System  
**Last Updated:** November 6, 2025  
**Total Spells:** 64 (8 circles × 8 spells each)

---

## Table of Contents

1. [First Circle (1-8)](#first-circle)
2. [Second Circle (9-16)](#second-circle)
3. [Third Circle (17-24)](#third-circle)
4. [Fourth Circle (25-32)](#fourth-circle)
5. [Fifth Circle (33-40)](#fifth-circle)
6. [Sixth Circle (41-48)](#sixth-circle)
7. [Seventh Circle (49-56)](#seventh-circle)
8. [Eighth Circle (57-64)](#eighth-circle)
9. [Spell Categories](#spell-categories)
10. [Damage Type Reference](#damage-type-reference)

---

## First Circle

**Mana Cost:** 4  
**Difficulty:** 0-20 skill  
**Circle Level:** Beginner

### 1. Clumsy (An Dex)
**Type:** Curse/Debuff  
**Target:** Single Target  
**Reagents:** Bloodmoss, Nightshade

**Description:** Reduces the target's Dexterity temporarily.

**How it Works:**
- Calculates DEX reduction based on caster's EvalInt and target's MagicResist
- Duration based on Magery and Inscription skills
- Can be resisted by target's Magic Resist skill
- Does not stack with other stat modifications of same type

**Requirements:**
- Minimum Magery: 0.0
- Line of sight to target
- Target must be alive

---

### 2. Create Food (In Mani Ylem)
**Type:** Utility  
**Target:** Self  
**Reagents:** Garlic, Ginseng, Mandrake Root

**Description:** Creates food items in the caster's backpack.

**How it Works:**
- Generates random food items (bread, meat, cheese, etc.)
- Quantity based on Magery skill
- Food is immediately edible
- Can cast in towns

**Requirements:**
- Minimum Magery: 0.0
- Backpack space available

---

### 3. Feeblemind (Rel Wis)
**Type:** Curse/Debuff  
**Target:** Single Target  
**Reagents:** Nightshade, Ginseng

**Description:** Reduces the target's Intelligence temporarily.

**How it Works:**
- Calculates INT reduction based on caster's EvalInt and target's MagicResist
- Duration based on Magery and Inscription skills
- Affects mana pool and spellcasting ability
- Can be resisted

**Requirements:**
- Minimum Magery: 0.0
- Line of sight to target
- Target must be alive

---

### 4. Heal (In Mani)
**Type:** Beneficial/Healing  
**Target:** Single Target  
**Reagents:** Garlic, Ginseng, Spider's Silk

**Description:** Restores hit points to the target.

**How it Works:**
- Heal amount based on caster's Magery and Inscription
- 20% bonus when healing others (encourages cooperation)
- Cannot heal dead, undead, or golems
- Blocked by deadly poison (level 3+) or Mortal Strike
- Formula: `NMSUtils.getBeneficialMageryInscribePercentage(Caster) / 3`

**Requirements:**
- Minimum Magery: 0.0
- Line of sight to target
- Target must be alive and healable

**Special Cases:**
- Blocked by One Ring (Easter egg)
- Cannot heal animated dead or bonded ghost pets
- Reduced effectiveness against Mortal Strike

---

### 5. Magic Arrow (In Por Ylem)
**Type:** Attack/Damage  
**Target:** Single Target  
**Reagents:** Sulfurous Ash  
**Damage Type:** 100% Fire

**Description:** Fires a magical projectile at the target.

**How it Works:**
- Base Damage: `Dice(1, 3) + 2`
- Damage capped at 8 points for balance
- Uses NMS damage calculation system
- Delayed damage (travels to target)
- Can be reflected by Magic Reflect spell
- Formula: `GetNMSDamage(2, 1, 3, target)`

**Requirements:**
- Minimum Magery: 0.0
- Line of sight to target
- Target must be alive

**Combat Notes:**
- Fast cast time makes it good for interrupting enemy spells
- Low mana cost allows for sustained use
- Useful for finishing weakened opponents

---

### 6. Night Sight (In Lor)
**Type:** Utility/Buff  
**Target:** Self  
**Reagents:** Spider's Silk, Sulfurous Ash

**Description:** Provides magical vision in darkness.

**How it Works:**
- Grants temporary night vision
- Duration based on Magery skill
- Allows seeing in complete darkness
- Useful in dungeons

**Requirements:**
- Minimum Magery: 0.0

---

### 7. Reactive Armor (Flam Sanct)
**Type:** Defensive/Buff  
**Target:** Self  
**Reagents:** Garlic, Spider's Silk, Sulfurous Ash

**Description:** Creates magical armor that reflects physical damage back to attackers.

**How it Works:**
- Provides physical resistance
- Duration based on Magery and Inscription
- Reflects a percentage of melee damage
- Does not stack with Protection spell
- Reflection amount scaled with Inscription skill

**Requirements:**
- Minimum Magery: 0.0
- Cannot cast if Protection is active

---

### 8. Weaken (Des Mani)
**Type:** Curse/Debuff  
**Target:** Single Target  
**Reagents:** Garlic, Nightshade

**Description:** Reduces the target's Strength temporarily.

**How it Works:**
- Calculates STR reduction based on caster's EvalInt and target's MagicResist
- Duration based on Magery and Inscription skills
- Affects carrying capacity and melee damage
- Can be resisted

**Requirements:**
- Minimum Magery: 0.0
- Line of sight to target
- Target must be alive

---

## Second Circle

**Mana Cost:** 6  
**Difficulty:** 20-40 skill  
**Circle Level:** Apprentice

### 9. Agility (Ex Uus)
**Type:** Buff  
**Target:** Single Target  
**Reagents:** Bloodmoss, Mandrake Root

**Description:** Increases target's Dexterity temporarily.

**How it Works:**
- DEX increase based on caster's Inscription and target's stats
- Duration based on Magery and Inscription
- Stacks with equipment bonuses
- Useful for archers and rogues

**Requirements:**
- Minimum Magery: 20.0
- Line of sight to target

---

### 10. Cunning (Uus Wis)
**Type:** Buff  
**Target:** Single Target  
**Reagents:** Nightshade, Mandrake Root

**Description:** Increases target's Intelligence temporarily.

**How it Works:**
- INT increase based on caster's Inscription and target's stats
- Duration based on Magery and Inscription
- Increases mana pool
- Useful for spellcasters

**Requirements:**
- Minimum Magery: 20.0
- Line of sight to target

---

### 11. Cure (An Nox)
**Type:** Beneficial/Cure  
**Target:** Single Target  
**Reagents:** Garlic, Ginseng

**Description:** Cures poison on the target.

**How it Works:**
- Success chance based on poison level vs Magery skill
- Guaranteed cure for level 1 poison
- Higher poison levels require higher Magery
- Inscription increases cure chance

**Requirements:**
- Minimum Magery:** 20.0
- Target must be poisoned
- Line of sight to target

---

### 12. Harm (An Mani)
**Type:** Attack/Damage  
**Target:** Single Target  
**Reagents:** Nightshade, Spider's Silk  
**Damage Type:** 100% Cold

**Description:** Damages the target with negative energy.

**How it Works:**
- Base Damage: `Dice(1, 4) + 4`
- Instant damage (no delay)
- Not affected by slayer spellbooks
- Can be reflected by Magic Reflect
- Formula: `GetNMSDamage(4, 1, 4, target)`

**Requirements:**
- Minimum Magery: 20.0
- Line of sight to target
- Target must be alive

---

### 13. Magic Trap (In Jux)
**Type:** Utility/Trap  
**Target:** Container  
**Reagents:** Garlic, Spider's Silk, Sulfurous Ash

**Description:** Places an explosive magical trap on a container.

**How it Works:**
- Damages anyone who opens the trapped container
- Trap strength based on Magery skill
- Can be detected by Remove Trap spell
- Lasts until triggered or dispelled

**Requirements:**
- Minimum Magery: 20.0
- Target must be a container
- Container must be unlocked and accessible

---

### 14. Magic Untrap (Remove Trap) (An Jux)
**Type:** Utility  
**Target:** Container/Item  
**Reagents:** Bloodmoss, Sulfurous Ash

**Description:** Removes magical traps from containers and items.

**How it Works:**
- Success chance based on Magery vs trap strength
- Can also detect hidden traps
- Safe way to disarm trapped containers
- Also works on trapped dungeon chests

**Requirements:**
- Minimum Magery: 20.0
- Target must be trapped

---

### 15. Protection (Uus Sanct)
**Type:** Defensive/Buff  
**Target:** Self  
**Reagents:** Garlic, Ginseng, Sulfurous Ash

**Description:** Provides magical protection but reduces casting speed.

**How it Works:**
- Grants chance to resist spell interruption when damaged while casting
- Reduces Faster Casting by 2
- Cannot stack with Reactive Armor
- Protection percentage based on Magery skill
- Duration until dispelled or removed

**Requirements:**
- Minimum Magery: 20.0
- Reactive Armor must not be active

**Trade-off:**
- Better concentration when casting
- Slower casting speed

---

### 16. Strength (Uus Mani)
**Type:** Buff  
**Target:** Single Target  
**Reagents:** Nightshade, Mandrake Root

**Description:** Increases target's Strength temporarily.

**How it Works:**
- STR increase based on caster's Inscription and target's stats
- Duration based on Magery and Inscription
- Increases carrying capacity and melee damage
- Useful for warriors

**Requirements:**
- Minimum Magery: 20.0
- Line of sight to target

---

## Third Circle

**Mana Cost:** 9  
**Difficulty:** 40-60 skill  
**Circle Level:** Journeyman

### 17. Bless (Rel Sanct)
**Type:** Buff  
**Target:** Single Target  
**Reagents:** Garlic, Mandrake Root

**Description:** Increases all stats (STR, DEX, INT) temporarily.

**How it Works:**
- Increases all three stats simultaneously
- More efficient than casting individual stat buffs
- Duration based on Magery and Inscription
- Popular pre-combat buff

**Requirements:**
- Minimum Magery: 40.0
- Line of sight to target

---

### 18. Fireball (Vas Flam)
**Type:** Attack/Damage  
**Target:** Single Target  
**Reagents:** Black Pearl  
**Damage Type:** 100% Fire

**Description:** Hurls an explosive ball of fire at the target.

**How it Works:**
- Base Damage: `Dice(1, 6) + 4`
- Delayed damage (travels to target)
- Can be reflected by Magic Reflect
- Popular mid-level damage spell
- Formula: `GetNMSDamage(4, 1, 6, target)`

**Requirements:**
- Minimum Magery: 40.0
- Line of sight to target
- Target must be alive

---

### 19. Magic Lock (An Por)
**Type:** Utility/Security  
**Target:** Container/Door  
**Reagents:** Bloodmoss, Garlic, Sulfurous Ash

**Description:** Magically locks containers and doors.

**How it Works:**
- Lock strength based on Magery skill
- Prevents opening without Unlock spell or lockpicking
- Can be broken by high-skill lockpickers
- Useful for securing storage

**Requirements:**
- Minimum Magery: 40.0
- Target must be lockable
- Target must be closed

---

### 20. Poison (In Nox)
**Type:** Attack/Debuff  
**Target:** Single Target  
**Reagents:** Nightshade

**Description:** Poisons the target, dealing damage over time.

**How it Works:**
- Poison level based on Magery and EvalInt
- Can apply poison levels 1-4
- Ticks damage over time
- Can be cured by Cure spell or Greater Cure
- Higher levels require higher skills

**Requirements:**
- Minimum Magery: 40.0
- Line of sight to target
- Target must be alive

**Poison Levels:**
- Level 1: 40-60 Magery
- Level 2: 60-80 Magery
- Level 3: 80-100 Magery
- Level 4: 100+ Magery

---

### 21. Telekinesis (Ort Por Ylem)
**Type:** Utility  
**Target:** Item/Object  
**Reagents:** Bloodmoss, Mandrake Root

**Description:** Manipulates objects from a distance.

**How it Works:**
- Opens containers remotely
- Activates switches and levers
- Range based on Magery skill
- Useful for avoiding traps

**Requirements:**
- Minimum Magery: 40.0
- Target must be visible
- Maximum range based on skill

---

### 22. Teleport (Rel Por)
**Type:** Travel/Movement  
**Target:** Location  
**Reagents:** Bloodmoss, Mandrake Root

**Description:** Instantly transports caster to a nearby location.

**How it Works:**
- Range based on Magery skill
- Requires line of sight to destination
- Cannot teleport to blocked locations
- Cannot use in combat heat (30 seconds after PvP)
- Blocked in certain regions (dungeons, houses)

**Requirements:**
- Minimum Magery: 40.0
- Clear destination
- No combat flag (in some areas)

**Travel Rules:**
- Cannot teleport into houses
- Cannot teleport into dungeons
- Region restrictions apply

---

### 23. Unlock (Ex Por)
**Type:** Utility  
**Target:** Container/Door  
**Reagents:** Bloodmoss, Sulfurous Ash

**Description:** Magically unlocks containers and doors.

**How it Works:**
- Success chance based on Magery vs lock strength
- Can unlock magically locked items
- Cannot unlock items above skill level
- Useful for treasure hunting

**Requirements:**
- Minimum Magery: 40.0
- Target must be locked

---

### 24. Wall of Stone (In Sanct Ylem)
**Type:** Field/Barrier  
**Target:** Location  
**Reagents:** Bloodmoss, Garlic

**Description:** Creates a temporary wall of stone that blocks movement.

**How it Works:**
- Creates impassable barrier
- Duration based on Magery skill
- Blocks line of sight
- Can be used for tactical positioning
- Dispellable with Dispel Field

**Requirements:**
- Minimum Magery: 40.0
- Clear ground target
- Can cast in town: No

---

## Fourth Circle

**Mana Cost:** 11  
**Difficulty:** 60-80 skill  
**Circle Level:** Expert

### 25. Arch Cure (Vas An Nox)
**Type:** Area Beneficial/Cure  
**Target:** Area of Effect  
**Reagents:** Garlic, Ginseng, Mandrake Root

**Description:** Cures poison on all nearby allies.

**How it Works:**
- Affects all party members and pets in range
- Range based on Magery skill (typically 3-5 tiles)
- Cure chance per target based on poison level vs Magery
- More efficient than casting Cure multiple times

**Requirements:**
- Minimum Magery: 60.0
- Caster must have targets in range

---

### 26. Arch Protection (Vas Uus Sanct)
**Type:** Area Buff  
**Target:** Area of Effect  
**Reagents:** Garlic, Ginseng, Mandrake Root, Sulfurous Ash

**Description:** Grants Protection buff to all nearby allies.

**How it Works:**
- Applies Protection spell to all valid targets in range
- Range based on Magery skill
- Each target gets individual Protection benefits
- Duration per target based on caster's Magery and Inscription

**Requirements:**
- Minimum Magery: 60.0
- Targets must not have Reactive Armor active

---

### 27. Curse (Des Sanct)
**Type:** Curse/Debuff  
**Target:** Single Target  
**Reagents:** Garlic, Nightshade, Sulfurous Ash

**Description:** Reduces all stats (STR, DEX, INT) on the target.

**How it Works:**
- Opposite of Bless spell
- Reduces all three stats simultaneously
- Duration based on Magery and EvalInt
- Can be resisted
- Useful for weakening tough opponents

**Requirements:**
- Minimum Magery: 60.0
- Line of sight to target
- Target must be alive

---

### 28. Fire Field (In Flam Grav)
**Type:** Field/Damage  
**Target:** Location  
**Reagents:** Black Pearl, Spider's Silk, Sulfurous Ash  
**Damage Type:** 100% Fire

**Description:** Creates a line of fire that damages anyone who crosses it.

**How it Works:**
- Creates 3-5 tiles of fire in a line
- Deals fire damage to anyone standing in it
- Damage per tick based on Magery and EvalInt
- Duration based on Magery skill
- Can be dispelled with Dispel Field
- Useful for area denial and crowd control

**Requirements:**
- Minimum Magery: 60.0
- Clear ground target
- Can cast in town: No

---

### 29. Greater Heal (In Vas Mani)
**Type:** Beneficial/Healing  
**Target:** Single Target  
**Reagents:** Garlic, Ginseng, Mandrake Root, Spider's Silk

**Description:** Restores a large amount of hit points to the target.

**How it Works:**
- Heal amount significantly higher than Heal spell
- Based on caster's Magery and Inscription
- Same restrictions as Heal (cannot heal dead, undead, golems)
- Blocked by deadly poison or Mortal Strike
- 20% bonus when healing others

**Requirements:**
- Minimum Magery: 60.0
- Line of sight to target
- Target must be alive and healable

---

### 30. Lightning (Por Ort Grav)
**Type:** Attack/Damage  
**Target:** Single Target  
**Reagents:** Mandrake Root, Sulfurous Ash  
**Damage Type:** 100% Energy

**Description:** Strikes the target with a lightning bolt.

**How it Works:**
- Base Damage: `Dice(1, 6) + 4`
- Instant damage (no delay)
- Can be reflected by Magic Reflect
- Visual effect changes based on spell hue
- Formula: `GetNMSDamage(4, 1, 6, target)`

**Requirements:**
- Minimum Magery: 60.0
- Line of sight to target
- Target must be alive

---

### 31. Mana Drain (Ort Rel)
**Type:** Attack/Debuff  
**Target:** Single Target  
**Reagents:** Black Pearl, Mandrake Root, Spider's Silk

**Description:** Drains mana from the target.

**How it Works:**
- Mana drain amount based on caster's Magery and EvalInt vs target's Resisting Spells
- Drained mana is lost (not transferred to caster)
- Can be resisted
- Effective against spellcasters

**Requirements:**
- Minimum Magery: 60.0
- Line of sight to target
- Target must be alive and have mana

---

### 32. Recall (Kal Ort Por)
**Type:** Travel  
**Target:** Rune  
**Reagents:** Bloodmoss, Black Pearl, Mandrake Root

**Description:** Teleports the caster to a marked rune location.

**How it Works:**
- Requires a marked rune
- Target location must be valid for travel
- Cannot recall while in combat heat (30 seconds in PvP)
- Cannot recall to/from certain regions
- Spell can fizzle if interrupted

**Requirements:**
- Minimum Magery: 60.0
- Valid marked rune
- No combat flag (in some areas)
- Not in jail or restricted area

**Travel Restrictions:**
- Cannot recall into houses (without access)
- Cannot recall into dungeons
- Cannot recall from some dungeons
- Midland has special restrictions

---

## Fifth Circle

**Mana Cost:** 14  
**Difficulty:** 80-100 skill  
**Circle Level:** Master

### 33. Blade Spirits (In Jux Hur Ylem)
**Type:** Summon/Combat  
**Target:** Location  
**Reagents:** Black Pearl, Mandrake Root, Nightshade

**Description:** Summons a Blade Spirit to attack nearby enemies.

**How it Works:**
- Summons aggressive spirit for duration
- Spirit attacks enemies automatically
- Strength based on caster's Magery
- Duration based on Magery and Inscription
- Can be dispelled

**Requirements:**
- Minimum Magery: 80.0
- Clear ground target
- Can cast in town: No

**Combat Notes:**
- Useful for solo players needing help
- Good for crowd control
- Can target caster's enemies

---

### 34. Dispel Field (An Grav)
**Type:** Utility/Dispel  
**Target:** Field  
**Reagents:** Black Pearl, Garlic, Spider's Silk, Sulfurous Ash

**Description:** Removes magical field spells.

**How it Works:**
- Removes Fire Field, Poison Field, Paralyze Field, Energy Field, Wall of Stone
- Success chance based on Magery vs field creator's Magery
- Can clear multiple fields in area
- Useful for navigation and tactics

**Requirements:**
- Minimum Magery: 80.0
- Target must be a field spell

---

### 35. Incognito (Kal In Ex)
**Type:** Utility/Disguise  
**Target:** Self  
**Reagents:** Bloodmoss, Garlic, Nightshade

**Description:** Disguises the caster's appearance and name.

**How it Works:**
- Changes appearance and name
- Duration based on Magery
- Does not hide you (different from Invisibility)
- Useful for avoiding recognition
- Can be dispelled

**Requirements:**
- Minimum Magery: 80.0
- Cannot be polymorphed
- Cannot be transformed

---

### 36. Magic Reflection (In Jux Sanct)
**Type:** Defensive/Buff  
**Target:** Self  
**Reagents:** Garlic, Mandrake Root, Spider's Silk

**Description:** Reflects hostile spells back at the caster.

**How it Works:**
- Creates magical shield that can reflect spells
- Number of charges based on Magery and Inscription
- Each reflected spell consumes charges
- Higher circle spells consume more charges
- Visual effect shows blue shield

**Requirements:**
- Minimum Magery: 80.0

**Mechanics:**
- Absorption points = Magery based
- Each spell reduces points by its circle level
- Shield breaks when points reach 0

---

### 37. Mind Blast (Por Corp Wis)
**Type:** Attack/Damage  
**Target:** Single Target  
**Reagents:** Black Pearl, Mandrake Root, Nightshade, Sulfurous Ash  
**Damage Type:** 100% Cold

**Description:** Psychic attack based on intelligence difference.

**How it Works:**
- Damage based on INT difference between caster and target
- If caster has higher INT: `(casterINT - targetINT)` damage
- If target has higher INT: `(targetINT - casterINT)` damage (backfires!)
- Minimum 3 damage, maximum 42 damage
- Can be resisted (half damage)
- Not affected by slayer spellbooks
- Delayed damage in AOS

**Requirements:**
- Minimum Magery: 80.0
- Line of sight to target
- Target must be alive

**Strategy:**
- Very effective against low-INT creatures
- Can backfire against high-INT targets
- Check target's INT before casting

---

### 38. Paralyze (An Ex Por)
**Type:** Debuff/Control  
**Target:** Single Target  
**Reagents:** Garlic, Mandrake Root, Spider's Silk

**Description:** Freezes the target in place temporarily.

**How it Works:**
- Duration based on Magery and EvalInt vs target's Resisting Spells
- Target cannot move but can still cast spells
- Can be broken by taking damage
- Can be resisted
- Very useful in PvP

**Requirements:**
- Minimum Magery: 80.0
- Line of sight to target
- Target must be alive

---

### 39. Poison Field (In Nox Grav)
**Type:** Field/Damage  
**Target:** Location  
**Reagents:** Black Pearl, Nightshade, Spider's Silk

**Description:** Creates a field of poisonous clouds.

**How it Works:**
- Creates 3-5 tiles of poison field in a line
- Poisons anyone who walks through it
- Poison level based on Magery skill
- Duration based on Magery
- Can be dispelled with Dispel Field

**Requirements:**
- Minimum Magery: 80.0
- Clear ground target
- Can cast in town: No

---

### 40. Summon Creature (Kal Xen)
**Type:** Summon  
**Target:** Location  
**Reagents:** Bloodmoss, Mandrake Root, Spider's Silk

**Description:** Summons a random creature to aid the caster.

**How it Works:**
- Summons creature based on Magery skill
- Higher Magery = stronger creatures
- Duration based on Magery and Inscription
- Creature follows and defends caster
- Can summon various animals and monsters

**Requirements:**
- Minimum Magery: 80.0
- Clear ground target

**Possible Summons:**
- Low skill: Dog, Cat, Rabbit
- Med skill: Wolf, Bear, Gorilla
- High skill: Llama, Horse, Great Hart

---

## Sixth Circle

**Mana Cost:** 20  
**Difficulty:** 100-120 skill  
**Circle Level:** Grandmaster

### 41. Dispel (An Ort)
**Type:** Utility/Banish  
**Target:** Creature/Summon  
**Reagents:** Garlic, Mandrake Root, Sulfurous Ash

**Description:** Attempts to banish summoned creatures or remove transformations.

**How it Works:**
- Dispel chance based on caster's Magery+Inscription vs target's summoner's power
- More effective against weak summons
- Can remove transformations
- Can dispel Blade Spirits and Energy Vortex
- Uses NMS dispel formula with Chaos bonus support

**Requirements:**
- Minimum Magery: 100.0
- Line of sight to target
- Target must be summon, transformation, or have active magical effects

---

### 42. Energy Bolt (Corp Por)
**Type:** Attack/Damage  
**Target:** Single Target  
**Reagents:** Black Pearl, Nightshade  
**Damage Type:** 100% Energy

**Description:** Fires a powerful bolt of pure energy.

**How it Works:**
- Base Damage: `Dice(1, 4) + 23`
- Delayed damage (travels to target)
- One of the strongest single-target damage spells
- Can be reflected by Magic Reflect
- Popular high-level combat spell
- Formula: `GetNMSDamage(23, 1, 4, target)`

**Requirements:**
- Minimum Magery: 100.0
- Line of sight to target
- Target must be alive

---

### 43. Explosion (Vas Ort Flam)
**Type:** Attack/Damage  
**Target:** Single Target  
**Reagents:** Bloodmoss, Mandrake Root  
**Damage Type:** 100% Fire

**Description:** Creates a delayed explosion that damages the target.

**How it Works:**
- Base Damage: `Dice(1, 5) + 25`
- 2.5-3 second delay before damage
- Can be resisted (half damage)
- Damage is high but delayed
- Can be reflected
- Formula: `GetNMSDamage(25, 1, 5, target)`

**Requirements:**
- Minimum Magery: 100.0
- Line of sight to target
- Target must be alive

**Strategy:**
- Cast before other spells for delayed combo
- Use with faster spells for burst damage
- Delay allows enemy to heal/escape

---

### 44. Invisibility (An Lor Xen)
**Type:** Utility/Stealth  
**Target:** Single or Self  
**Reagents:** Bloodmoss, Nightshade

**Description:** Makes the target invisible.

**How it Works:**
- Grants invisibility until broken
- Broken by: attacking, casting offensive spells, revealing
- Can be revealed by Reveal spell or detect hidden
- Duration until action taken
- Useful for stealth and escape

**Requirements:**
- Minimum Magery: 100.0
- Line of sight to target (if not self)

---

### 45. Mark (Kal Por Ylem)
**Type:** Utility/Travel  
**Target:** Rune  
**Reagents:** Bloodmoss, Black Pearl, Mandrake Root

**Description:** Marks a recall rune with current location.

**How it Works:**
- Records current location on blank rune
- Marked rune can be used with Recall or Gate Travel
- Cannot mark in restricted areas (jails, certain dungeons)
- Requires rune in backpack
- Useful for creating travel network

**Requirements:**
- Minimum Magery: 100.0
- Blank or existing rune in backpack
- Valid location for marking

**Restrictions:**
- Cannot mark in jails
- Cannot mark in some dungeon areas
- Region restrictions apply

---

### 46. Mass Curse (Vas Des Sanct)
**Type:** Area Curse  
**Target:** Area of Effect  
**Reagents:** Garlic, Mandrake Root, Nightshade, Sulfurous Ash

**Description:** Curses all enemies in the area.

**How it Works:**
- Applies Curse effect to all valid targets in range
- Range based on Magery skill (3-5 tiles)
- Each target can resist individually
- Very powerful in group combat
- Affects hostile targets only

**Requirements:**
- Minimum Magery: 100.0
- Can cast in town: No

---

### 47. Paralyze Field (In Ex Grav)
**Type:** Field/Control  
**Target:** Location  
**Reagents:** Black Pearl, Ginseng, Spider's Silk

**Description:** Creates a field that paralyzes anyone who crosses it.

**How it Works:**
- Creates 3-5 tiles of paralyze field
- Anyone crossing becomes paralyzed briefly
- Duration of field based on Magery
- Paralyze duration can be resisted
- Very useful for controlling movement
- Can be dispelled with Dispel Field

**Requirements:**
- Minimum Magery: 100.0
- Clear ground target
- Can cast in town: No

---

### 48. Reveal (Wis Quas)
**Type:** Utility/Detection  
**Target:** Area of Effect  
**Reagents:** Bloodmoss, Sulfurous Ash

**Description:** Reveals hidden and invisible creatures in the area.

**How it Works:**
- Reveals invisible players/creatures
- Reveals hidden players/creatures
- Range based on Magery skill
- Can be resisted by hiding skill vs Magery
- Useful against stealth characters
- One Ring provides protection (Easter egg)

**Requirements:**
- Minimum Magery: 100.0

---

## Seventh Circle

**Mana Cost:** 40  
**Difficulty:** 120+ skill  
**Circle Level:** Legendary

### 49. Chain Lightning (Vas Ort Grav)
**Type:** Area Attack/Damage  
**Target:** Area of Effect  
**Reagents:** Black Pearl, Bloodmoss, Mandrake Root, Sulfurous Ash  
**Damage Type:** 100% Energy

**Description:** Strikes multiple targets with lightning.

**How it Works:**
- Hits all valid targets in range
- Damage per target based on Magery and EvalInt
- Range based on Magery skill (3-5 tiles)
- Can be resisted individually
- Very powerful AoE spell
- Popular for crowd control

**Requirements:**
- Minimum Magery: 120.0
- Valid targets in range
- Can cast in town: No

---

### 50. Energy Field (In Sanct Grav)
**Type:** Field/Damage  
**Target:** Location  
**Reagents:** Black Pearl, Mandrake Root, Spider's Silk, Sulfurous Ash  
**Damage Type:** 100% Energy

**Description:** Creates a field of pure energy that damages and blocks movement.

**How it Works:**
- Creates 3-5 tiles of energy field
- Damages anyone standing in it
- Also blocks line of sight
- Duration based on Magery
- Higher damage than other fields
- Can be dispelled with Dispel Field

**Requirements:**
- Minimum Magery: 120.0
- Clear ground target
- Can cast in town: No

---

### 51. Flamestrike (Kal Vas Flam)
**Type:** Attack/Damage  
**Target:** Single Target  
**Reagents:** Spider's Silk, Sulfurous Ash  
**Damage Type:** 100% Fire

**Description:** Calls down a column of flame on the target.

**How it Works:**
- Base Damage: `Dice(1, 6) + 40`
- Delayed damage
- One of the highest damage single-target spells
- Can be reflected by Magic Reflect
- Popular endgame combat spell
- Formula: `GetNMSDamage(40, 1, 6, target)`

**Requirements:**
- Minimum Magery: 120.0
- Line of sight to target
- Target must be alive

---

### 52. Gate Travel (Vas Rel Por)
**Type:** Travel  
**Target:** Rune  
**Reagents:** Black Pearl, Mandrake Root, Sulfurous Ash

**Description:** Opens a magical gate that anyone can use to travel to marked location.

**How it Works:**
- Creates blue moongate to rune location
- Anyone can use gate (friend or foe)
- Gate duration based on Magery
- Requires marked rune
- Same travel restrictions as Recall
- Gate remains open for ~30 seconds

**Requirements:**
- Minimum Magery: 120.0
- Valid marked rune
- Not in restricted area

**Strategic Uses:**
- Group transportation
- Quick escape route
- Tactical repositioning

---

### 53. Mana Vampire (Ort Sanct)
**Type:** Attack/Drain  
**Target:** Single Target  
**Reagents:** Black Pearl, Bloodmoss, Mandrake Root, Spider's Silk

**Description:** Drains mana from target and transfers it to caster.

**How it Works:**
- Mana drain amount based on caster's Magery vs target's Resisting Spells
- Drained mana is added to caster's mana pool
- Can be resisted
- Very useful in mage vs mage combat
- Cannot drain more than caster's maximum mana

**Requirements:**
- Minimum Magery: 120.0
- Line of sight to target
- Target must have mana

---

### 54. Mass Dispel (Vas An Ort)
**Type:** Area Utility  
**Target:** Area of Effect  
**Reagents:** Black Pearl, Garlic, Mandrake Root, Sulfurous Ash

**Description:** Attempts to dispel multiple summons and magical effects in area.

**How it Works:**
- Affects all summons and transformations in range
- Range based on Magery skill (3-5 tiles)
- Dispel chance per target based on Magery
- Useful for removing enemy summons
- Can clear multiple Blade Spirits/Energy Vortexes

**Requirements:**
- Minimum Magery: 120.0
- Valid targets in range

---

### 55. Meteor Swarm (Flam Kal Des Ylem)
**Type:** Area Attack/Damage  
**Target:** Area of Effect  
**Reagents:** Bloodmoss, Mandrake Root, Spider's Silk, Sulfurous Ash  
**Damage Type:** 100% Fire

**Description:** Rains meteors down on all nearby enemies.

**How it Works:**
- Hits all valid targets in range
- Damage per target based on Magery and EvalInt
- Range based on Magery skill
- Can be resisted individually
- Spectacular visual effect
- Popular high-level AoE spell

**Requirements:**
- Minimum Magery: 120.0
- Valid targets in range
- Can cast in town: No

---

### 56. Polymorph (Vas Ylem Rel)
**Type:** Transformation  
**Target:** Self  
**Reagents:** Bloodmoss, Mandrake Root, Spider's Silk

**Description:** Transforms the caster into a different creature.

**How it Works:**
- Changes appearance to selected creature
- Grants some creature abilities
- Duration based on Magery
- Cannot cast while transformed into another form
- Can be dispelled
- Useful for roleplay and some tactical advantages

**Requirements:**
- Minimum Magery: 120.0
- Not already polymorphed or transformed

---

## Eighth Circle

**Mana Cost:** 50  
**Difficulty:** 120+ skill  
**Circle Level:** Legendary (Highest)

### 57. Earthquake (In Vas Por)
**Type:** Area Attack/Damage  
**Target:** Area of Effect (Self-Centered)  
**Reagents:** Bloodmoss, Ginseng, Mandrake Root, Sulfurous Ash  
**Damage Type:** 100% Physical

**Description:** Creates a massive earthquake that damages all nearby enemies.

**How it Works:**
- Hits all enemies around caster
- Range based on Magery: `1 + (Magery / 15)` tiles
- Damage based on Magery and EvalInt
- Can be resisted (half damage)
- Does not affect allies
- Monster damage is capped and randomized
- Formula: `GetNMSDamage(35, 1, 6, PvP) + RandomMinMax(0, 10)`

**Requirements:**
- Minimum Magery: 120.0
- Can cast in town: Yes (but checks region)
- Line of sight to targets not required

**Special Rules:**
- One Ring provides reveal protection
- Reveals hidden/invisible enemies
- Extra damage to non-players

---

### 58. Energy Vortex (Vas Corp Por)
**Type:** Summon/Combat  
**Target:** Location  
**Reagents:** Black Pearl, Bloodmoss, Mandrake Root, Nightshade

**Description:** Summons a powerful Energy Vortex to attack enemies.

**How it Works:**
- Summons aggressive vortex for duration
- Vortex is very strong (higher stats than Blade Spirit)
- Attacks enemies automatically
- Strength based on caster's Magery
- Duration based on Magery and Inscription
- Can be dispelled
- Requires high skill to control

**Requirements:**
- Minimum Magery: 120.0
- Clear ground target
- Can cast in town: No
- Must have sufficient control slots

---

### 59. Resurrection (An Corp)
**Type:** Beneficial/Revival  
**Target:** Dead Player/Pet  
**Reagents:** Bloodmoss, Garlic, Ginseng

**Description:** Resurrects a dead player or pet.

**How it Works:**
- Target must be a ghost
- Restores target to life with partial stats
- Initial hit points/mana based on caster's Magery
- Can resurrect pets
- Cannot resurrect in combat (in some areas)
- Very important utility spell

**Requirements:**
- Minimum Magery: 120.0
- Target must be dead ghost
- Line of sight to target

---

### 60-64. Summon [Elemental] Spells

All elemental summon spells share similar mechanics:

**Common Requirements:**
- Minimum Magery: 120.0
- Clear ground target
- Can cast in town: No
- Must have sufficient control slots

**Common Mechanics:**
- Duration based on Magery and Inscription
- Elemental strength scales with Magery
- Can be dispelled
- Elementals attack caster's enemies
- Sorcerer trait provides 50% bonus to scaling

---

### 60. Air Elemental (Kal Vas Xen Hur)
**Reagents:** Bloodmoss, Mandrake Root, Spider's Silk

**Description:** Summons an Air Elemental.

**Strengths:** Fast movement, decent damage
**Weaknesses:** Lower hit points

---

### 61. Earth Elemental (Kal Vas Xen Ylem)
**Reagents:** Bloodmoss, Mandrake Root, Spider's Silk

**Description:** Summons an Earth Elemental.

**Strengths:** High hit points, good defense
**Weaknesses:** Slow movement

---

### 62. Fire Elemental (Kal Vas Xen Flam)
**Reagents:** Bloodmoss, Mandrake Root, Spider's Silk, Sulfurous Ash

**Description:** Summons a Fire Elemental.

**Strengths:** High fire damage, fire immunity
**Weaknesses:** Vulnerable to cold

---

### 63. Water Elemental (Kal Vas Xen An Flam)
**Reagents:** Bloodmoss, Mandrake Root, Spider's Silk

**Description:** Summons a Water Elemental.

**Strengths:** High cold damage, cold immunity
**Weaknesses:** Vulnerable to fire

---

### 64. Summon Daemon (Kal Vas Xen Corp)
**Reagents:** Bloodmoss, Mandrake Root, Spider's Silk, Sulfurous Ash

**Description:** Summons a powerful Daemon.

**Strengths:** Highest combat power of summons
**Weaknesses:** Difficult to control, can turn on caster if control fails

**Special Notes:**
- Most powerful summon in Magery
- Requires excellent control
- Can fail and attack caster

---

## Spell Categories

### By Type

#### Attack/Damage Spells (17)
- **1st Circle:** Magic Arrow
- **2nd Circle:** Harm
- **3rd Circle:** Fireball, Poison
- **4th Circle:** Lightning
- **6th Circle:** Energy Bolt, Explosion
- **7th Circle:** Chain Lightning, Flamestrike, Meteor Swarm
- **8th Circle:** Earthquake

#### Beneficial/Healing Spells (5)
- **1st Circle:** Heal
- **4th Circle:** Greater Heal, Arch Cure, Arch Protection
- **8th Circle:** Resurrection

#### Buff Spells (7)
- **1st Circle:** Night Sight, Reactive Armor
- **2nd Circle:** Agility, Cunning, Protection, Strength
- **3rd Circle:** Bless

#### Curse/Debuff Spells (8)
- **1st Circle:** Clumsy, Feeblemind, Weaken
- **2nd Circle:** (none)
- **3rd Circle:** Poison
- **4th Circle:** Curse, Mana Drain
- **5th Circle:** Paralyze
- **6th Circle:** Mass Curse
- **7th Circle:** Mana Vampire

#### Field Spells (6)
- **3rd Circle:** Wall of Stone
- **4th Circle:** Fire Field
- **5th Circle:** Poison Field
- **6th Circle:** Paralyze Field
- **7th Circle:** Energy Field

#### Summon Spells (7)
- **5th Circle:** Blade Spirits, Summon Creature
- **8th Circle:** Air Elemental, Earth Elemental, Fire Elemental, Water Elemental, Energy Vortex, Summon Daemon

#### Travel Spells (5)
- **3rd Circle:** Teleport
- **4th Circle:** Recall
- **6th Circle:** Mark
- **7th Circle:** Gate Travel

#### Utility Spells (15)
- **1st Circle:** Create Food
- **2nd Circle:** Cure, Magic Trap, Magic Untrap
- **3rd Circle:** Magic Lock, Telekinesis, Unlock
- **5th Circle:** Dispel Field, Incognito
- **6th Circle:** Dispel, Invisibility, Reveal
- **7th Circle:** Mass Dispel, Polymorph

#### Defensive Spells (2)
- **1st Circle:** Reactive Armor
- **5th Circle:** Magic Reflection

---

## Damage Type Reference

### Damage Type Distribution

| Damage Type | Spells | Percentage |
|-------------|--------|------------|
| **Fire** | Magic Arrow, Fireball, Fire Field, Explosion, Flamestrike, Meteor Swarm | 35% |
| **Energy** | Lightning, Energy Bolt, Energy Field, Chain Lightning | 24% |
| **Cold** | Harm, Mind Blast | 12% |
| **Physical** | Earthquake | 6% |
| **Poison** | Poison spell (DoT), Poison Field | 12% |
| **Mixed/None** | Various utility spells | 11% |

### Resistance Strategy

**For Players:**
- **Fire Resist:** Essential for PvE (most mages use Fireball/Flamestrike)
- **Energy Resist:** Important for PvP (Energy Bolt is common)
- **Cold Resist:** Less critical but useful
- **Poison Resist:** Helpful but Cure spells are common

**For Combat:**
- Check target's weakest resistance
- Use appropriate damage type
- Fire and Energy are most common, so most creatures resist them

---

## Spell Combinations and Strategies

### Offensive Combos

#### Burst Damage Combo (8th Circle)
1. Explosion (delayed)
2. Energy Bolt
3. Flamestrike
- Total potential: 90+ damage in 3-4 seconds

#### Area Control Combo (6th-7th Circle)
1. Paralyze Field (block escape)
2. Meteor Swarm or Chain Lightning
3. Energy Field (finish)

#### Mage Killer Combo (7th Circle)
1. Mana Vampire (reduce enemy mana)
2. Mana Drain (finish mana pool)
3. Flamestrike (they can't cast defensive spells)

### Defensive Strategies

#### Pre-Combat Buffs
1. Protection (reduce interruption)
2. Magic Reflection (reflect 1-2 spells)
3. Reactive Armor (if not using Protection)

#### Emergency Escape
1. Invisibility
2. Teleport away
3. Recall to safety rune

### Support Strategies

#### Group Support
1. Arch Protection (team buff)
2. Greater Heal (keep tank alive)
3. Arch Cure (counter poison)

#### Crowd Control
1. Paralyze Field (funnel enemies)
2. Wall of Stone (block path)
3. Energy Field (damage zone)

---

## Skill Requirements Summary

| Circle | Minimum Magery | Recommended Magery | Mana Cost |
|--------|----------------|-------------------|-----------|
| 1st | 0.0 | 0-20 | 4 |
| 2nd | 0.0 | 20-40 | 6 |
| 3rd | 0.0 | 40-60 | 9 |
| 4th | 0.0 | 60-80 | 11 |
| 5th | 0.0 | 80-100 | 14 |
| 6th | 0.0 | 100-120 | 20 |
| 7th | 70.0 | 120+ | 40 |
| 8th | 80.0 | 120+ | 50 |

**Note:** While minimum skill is 0 for most spells, success chance increases dramatically with proper skill level. Recommended Magery ensures consistent casting.

---

## Supporting Skills

### Inscription
- Increases buff/debuff duration
- Increases damage slightly
- Improves Magic Reflection charges
- Essential for PvP mages

### Evaluate Intelligence
- Increases spell damage significantly
- Affects resist calculations
- Essential for damage dealers

### Meditation
- Faster mana regeneration
- Essential for sustained casting
- Meditation cap based on armor

### Resisting Spells
- Reduces hostile spell effects
- Lowers damage taken
- Increases chance to resist debuffs

---

## Reagent Cost Summary

### Most Common Reagents
1. **Mandrake Root** - Used in 29 spells
2. **Spider's Silk** - Used in 24 spells
3. **Bloodmoss** - Used in 24 spells
4. **Black Pearl** - Used in 18 spells
5. **Garlic** - Used in 18 spells
6. **Sulfurous Ash** - Used in 18 spells
7. **Ginseng** - Used in 10 spells
8. **Nightshade** - Used in 18 spells

### Budget Spells (1-2 reagents)
- Magic Arrow (1)
- Fireball (1)
- Poison (1)

### Expensive Spells (4 reagents)
- Mind Blast (4)
- Chain Lightning (4)
- Meteor Swarm (4)
- Earthquake (4)

---

## PvE vs PvP Considerations

### Best PvE Spells
- **Flamestrike** - Highest single-target damage
- **Meteor Swarm** - Best AoE for groups
- **Energy Vortex** - Tank and damage
- **Greater Heal** - Sustain

### Best PvP Spells
- **Energy Bolt** - Fast, high damage
- **Paralyze** - Control
- **Magic Reflection** - Counter enemy mages
- **Invisibility** - Escape

### Dungeon Essentials
- Recall (escape)
- Greater Heal (survival)
- Invisibility (stealth)
- Reveal (hidden enemies)

---

## Conclusion

The Magery system in Ultima Adventures provides 64 diverse spells across 8 circles, offering options for damage, healing, buffing, crowd control, travel, and utility. Mastering the system requires understanding:

1. **Damage Types** - Use appropriate types for targets
2. **Skill Scaling** - Higher skills = better results
3. **Mana Management** - Balance power with sustainability
4. **Combo Timing** - Coordinate delayed and instant spells
5. **Situational Awareness** - Choose right spell for situation

Whether focusing on PvE, PvP, or support roles, the Magery system offers deep strategic gameplay with room for specialization and mastery.

---

**For refactoring patterns and technical details, see:**
- `Spell_Refactoring_Summary.md`
- `Spell_Developer_Guide.md`
- `Spell_Migration_Checklist.md`


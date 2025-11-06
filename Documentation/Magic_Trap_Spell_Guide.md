# Magic Trap Spell - Complete Guide

**Circle:** 2nd Circle Magery  
**Words of Power:** In Jux  
**Reagents:** Garlic, Spider's Silk, Sulfurous Ash

---

## 📋 Overview

**Magic Trap** is a defensive/offensive utility spell that creates magical traps. It has **two distinct uses**:

1. **Container Traps:** Trap chests, boxes, and containers
2. **Ground Traps:** Place visible magical runes on the ground

Both trap types deal **elemental damage** when triggered by enemies.

---

## 🎯 How It Works

### **Targeting Options:**

**Option 1: Target a Container (Chest, Box, etc.)**
- Adds a magical trap to the container
- Triggers when someone **opens** the container
- Invisible until triggered
- Deals 50-200 damage

**Option 2: Target the Ground**
- Creates a **visible magical rune** on the floor
- Triggers when someone **walks over** it
- Glows with elemental color
- Deals damage based on caster's Magery + EvalInt
- Auto-decays after **3 minutes (180 seconds)**
- Limit: **2 ground traps** within 10 tiles

---

## ⚡ Trap Power Formula

### **Container Traps:**
```csharp
BasePower = Magery / 3
TrapPower = (BasePower * EvalIntBonus) / 1 + 1
Damage = Random(50, 200) adjusted by target's resistance
```

### **Ground Traps:**
```csharp
BasePower = Magery / 3
TrapPower = (BasePower * EvalIntBonus) / 2 + 1
Damage = Random(TrapPower/2, TrapPower) adjusted by target's resistance
```

**Ground traps are 50% weaker than container traps!**

---

## 🔥 Trap Types (Ground Traps Only)

Ground traps randomly select one of **5 elemental types**:

| Type | Color | Effect | Damage Type | Resisted By |
|------|-------|--------|-------------|-------------|
| **Fire** | Red (0x489) | Flames | Fire Damage | Fire Resistance |
| **Energy** | Yellow (0x490) | Lightning | Energy Damage | Energy Resistance |
| **Poison** | Green (0x48F) | Gas Cloud | Poison + Damage | Poison Resistance |
| **Cold** | Blue (0x480) | Blizzard | Cold Damage | Cold Resistance |
| **Physical** | White (0x48E) | Explosion | Physical Damage | Physical Resistance |

**Note:** Poison traps also apply poison status (Lesser to Lethal based on target's poison resistance).

---

## 🎮 SIMULATION SCENARIOS

### **Scenario 1: Beginner Mage (Magery 50, EvalInt 50)**

**Container Trap:**
```
Power Calculation:
- BasePower = 50 / 3 = 16.6 → 16
- EvalIntBonus = ~1.0 (low skill)
- TrapPower = (16 * 1.0) / 1 + 1 = 17

Trap Execution:
- Damage Range: 50-200 (fixed for containers)
- Victim (50% Fire Resistance): 25-100 damage
- Victim (0% Fire Resistance): 50-200 damage
```

**Ground Trap:**
```
Power Calculation:
- BasePower = 50 / 3 = 16.6 → 16
- EvalIntBonus = ~1.0
- TrapPower = (16 * 1.0) / 2 + 1 = 9

Trap Execution:
- Damage Range: 4-9 raw damage
- Victim (50% Cold Resistance): 2-4 damage
- Victim (0% Cold Resistance): 4-9 damage
```

**Result:** Weak but usable at low levels. Ground traps barely scratch high-resistance targets.

---

### **Scenario 2: Competent Mage (Magery 80, EvalInt 80)**

**Container Trap:**
```
Power Calculation:
- BasePower = 80 / 3 = 26.6 → 26
- EvalIntBonus = ~1.3
- TrapPower = (26 * 1.3) / 1 + 1 = 34

Trap Execution:
- Damage Range: 50-200
- Victim (50% Energy Resistance): 25-100 damage
- Victim (70% Energy Resistance): 15-60 damage
```

**Ground Trap:**
```
Power Calculation:
- BasePower = 80 / 3 = 26.6 → 26
- EvalIntBonus = ~1.3
- TrapPower = (26 * 1.3) / 2 + 1 = 18

Trap Execution:
- Damage Range: 9-18 raw damage
- Victim (50% Poison Resistance): 4-9 damage + Poison
- Victim (0% Poison Resistance): 9-18 damage + Lethal Poison
```

**Result:** Moderate effectiveness. Container traps become dangerous. Ground traps still relatively weak.

---

### **Scenario 3: Expert Mage (Magery 100, EvalInt 100)**

**Container Trap:**
```
Power Calculation:
- BasePower = 100 / 3 = 33.3 → 33
- EvalIntBonus = ~1.5
- TrapPower = (33 * 1.5) / 1 + 1 = 50

Trap Execution:
- Damage Range: 50-200
- Victim (30% Physical Resistance): 35-140 damage
- Victim (70% Physical Resistance): 15-60 damage
```

**Ground Trap:**
```
Power Calculation:
- BasePower = 100 / 3 = 33.3 → 33
- EvalIntBonus = ~1.5
- TrapPower = (33 * 1.5) / 2 + 1 = 26

Trap Execution:
- Damage Range: 13-26 raw damage
- Victim (40% Fire Resistance): 7-15 damage
- Victim (0% Fire Resistance): 13-26 damage
```

**Result:** Solid damage. Container traps can severely hurt low-resistance characters. Ground traps become useful.

---

### **Scenario 4: Master Mage (Magery 120, EvalInt 120)**

**Container Trap:**
```
Power Calculation:
- BasePower = 120 / 3 = 40
- EvalIntBonus = ~1.8
- TrapPower = (40 * 1.8) / 1 + 1 = 73

Trap Execution:
- Damage Range: 50-200
- Victim (0% Resistance): 50-200 damage (LETHAL)
- Victim (50% Resistance): 25-100 damage (SEVERE)
- Victim (70% Resistance): 15-60 damage (MODERATE)
```

**Ground Trap:**
```
Power Calculation:
- BasePower = 120 / 3 = 40
- EvalIntBonus = ~1.8
- TrapPower = (40 * 1.8) / 2 + 1 = 37

Trap Execution:
- Damage Range: 18-37 raw damage
- Victim (0% Cold Resistance): 18-37 damage
- Victim (50% Cold Resistance): 9-18 damage
- Victim (70% Cold Resistance): 5-11 damage
```

**Result:** Very effective! Container traps are LETHAL to unprepared characters. Ground traps deal respectable damage.

---

## 🚫 Avoidance Mechanics

### **Ground Traps Can Be Avoided By:**

1. **Trap Avoidance Skills:**
   - Remove Trap skill
   - Detect Hidden skill
   - Physical avoidance mechanics

2. **EvalInt Skill Check:**
   - When walking over trap, chance to "outsmart" it
   - Success: "You got near a magical trap, but you were too intelligent to suffer the effects."
   - Roll: EvalInt skill check (0-125 difficulty)

3. **Immunity:**
   - Caster can walk over own traps safely
   - Players with "Blessed" status
   - Special items (certain gems/jewelry)
   - Air Walk spell effect

4. **Visual Detection:**
   - Ground traps are **VISIBLE** (glowing runes with light)
   - Smart players can avoid them by sight

### **Container Traps Can Be:**

1. **Detected:**
   - Detect Trap skill/spell
   - Trap Wand (from Remove Trap spell)

2. **Disarmed:**
   - Remove Trap spell/skill
   - High lockpicking skill

3. **Resisted:**
   - High elemental resistance reduces damage
   - Magic resistance can reduce trap effectiveness

---

## 💡 Why Use Magic Trap?

### **PvP Uses:**

#### **1. Area Denial**
```
Scenario: Enemy chasing you through dungeon
Action: Drop ground traps in corridors
Result: Enemy must stop, go around, or eat damage
Benefit: Escape time, tactical advantage
```

#### **2. Ambush Preparation**
```
Scenario: Setting up for group fight
Action: Place 2 ground traps at choke points
Result: Enemies walk into traps during retreat
Benefit: Free damage, disrupts enemy formation
```

#### **3. Container Protection**
```
Scenario: Storing valuables in house
Action: Trap all valuable chests
Result: Thieves take damage when looting
Benefit: Punishes would-be thieves, security
```

#### **4. Tactical Retreat**
```
Scenario: Losing 1v1 fight, need to heal
Action: Drop trap while running, hide behind corner
Result: Enemy must choose: eat trap or lose pursuit
Benefit: Creates safe healing window
```

---

### **PvE Uses:**

#### **1. Dungeon Preparation**
```
Scenario: Pulling strong monsters
Action: Place traps at pull location
Result: Monster walks through traps to you
Benefit: Free 20-40 damage before engagement
```

#### **2. Safe Looting**
```
Scenario: Found treasure chest in dungeon
Action: Detect trap, Remove trap, loot safely
Result: No damage taken
Benefit: Safety, preserves health/resources
```

#### **3. Escape Routes**
```
Scenario: Overwhelmed by monsters
Action: Drop trap, run through it
Result: Monsters trigger trap, take damage
Benefit: Slows pursuit, deals damage
```

#### **4. Boss Fight Prep**
```
Scenario: Preparing for boss encounter
Action: Place 2 traps at boss spawn location
Result: Boss walks through traps immediately
Benefit: Start fight with advantage
```

---

### **Economic/Utility Uses:**

#### **1. Container Security (Houses)**
```
Use: Trap chests containing valuables
Why: Punishes thieves/guild thieves
Damage: 50-200 per open attempt
Cost: Low (cheap reagents)
Benefit: Deterrent + revenge damage
```

#### **2. Training Magery**
```
Use: Cast repeatedly on containers
Why: Gains Magery skill points
Cost: 3 reagents per cast
Benefit: Cheap skill training method
```

#### **3. Dungeon Chest Protection**
```
Use: Trap dungeon loot chests for others
Why: Trolling/area control
Result: Slows enemy looting operations
Benefit: Tactical advantage in contested areas
```

---

## 📊 Comparison: Ground vs Container Traps

| Feature | Ground Traps | Container Traps |
|---------|--------------|-----------------|
| **Visibility** | Visible (glowing rune) | Invisible until opened |
| **Damage** | 50% power (weaker) | 100% power (stronger) |
| **Duration** | 3 minutes auto-decay | Permanent until triggered |
| **Limit** | Max 2 per 10 tiles | Unlimited |
| **Avoidance** | Walk around, EvalInt check | Detect trap, Remove trap |
| **Positioning** | Anywhere on ground | Only on containers |
| **Best For** | Area denial, chase disruption | Security, ambush damage |
| **Damage Range** | Skill-based (5-40 dmg) | Fixed (50-200 dmg) |
| **Trigger** | Walking over | Opening container |

---

## ⚔️ Tactical Considerations

### **When to Use Ground Traps:**

✅ **Good For:**
- Choke points (doorways, corridors)
- Retreat routes (slow pursuers)
- Visual deterrent (enemy sees trap, slows down)
- Cheap area denial (low reagent cost)

❌ **Bad For:**
- High damage (too weak compared to direct spells)
- Invisible traps (they're visible!)
- Long-term area control (3 min decay)
- Against smart players (easily avoided)

---

### **When to Use Container Traps:**

✅ **Good For:**
- House security (protect valuables)
- Ambush damage (hidden chest bait)
- Treasure protection (loot denial)
- Punishing thieves (revenge damage)

❌ **Bad For:**
- Active combat (can't trap mid-fight)
- Mobile defense (containers don't move)
- Quick deployment (requires container)

---

## 💰 Cost vs Benefit

### **Reagent Cost:**
- Garlic: ~5 gold
- Spider's Silk: ~8 gold
- Sulfurous Ash: ~5 gold
- **Total: ~18 gold per cast**

### **Value Propositions:**

**Ground Trap:**
- Cost: 18 gold
- Damage: 5-40 (skill dependent)
- Duration: 3 minutes
- **Value: LOW** (direct damage spells more efficient)
- **Use Case: Tactical utility, not raw damage**

**Container Trap:**
- Cost: 18 gold
- Damage: 50-200 (guaranteed high)
- Duration: Permanent
- **Value: HIGH** (cheap security, lethal damage)
- **Use Case: Best use of the spell**

---

## 🎯 Optimal Usage Strategies

### **PvP Strategy: Retreat Traps**
```
1. Engage enemy in combat
2. Start losing, need to heal
3. Drop ground trap while running
4. Turn corner, hide
5. Enemy choices:
   a) Eat trap damage (18-37 dmg)
   b) Stop to avoid trap (you gain distance)
   c) Go around (you gain time to heal)
6. Result: Safe healing window created
```

### **PvE Strategy: Pull Optimization**
```
1. Find strong monster
2. Place 2 traps between you and monster
3. Aggro monster with spell
4. Run backwards through trap line
5. Monster walks through both traps (36-74 dmg)
6. Start combat with HP advantage
7. Result: Easier kill, less healing needed
```

### **Security Strategy: Layered Defense**
```
1. Valuable chest in house
2. Cast Magic Trap on chest
3. Cast Magic Lock on chest (3rd circle)
4. Hide chest behind furniture
5. Thief must:
   a) Find hidden chest
   b) Pick lock (possibly triggers trap)
   c) Open chest (triggers trap if not removed)
6. Result: 50-200 damage to thief + detection
```

---

## 🚨 Important Limitations

### **Ground Traps:**

1. **Visible to All:**
   - Glows with elemental color
   - Has light radius (Circle300)
   - Smart enemies avoid easily

2. **Limited Quantity:**
   - Max 2 traps within 10 tiles
   - Can't spam trap entire area

3. **Auto-Decay:**
   - Disappears after 3 minutes
   - Not suitable for permanent area denial

4. **Weak Damage:**
   - Ground traps only 50% power
   - High-resistance targets barely hurt

5. **Criminal Action:**
   - Placing ground trap flags you criminal
   - Guards will attack in guard zones

---

### **Container Traps:**

1. **Requires Container:**
   - Can't use on just any item
   - Must be TrapableContainer type
   - Limited to chests, boxes, etc.

2. **Can Be Detected:**
   - Detect Trap spell/skill reveals it
   - Remove Trap spell/skill disarms it
   - Trap Wand warns of presence

3. **Fixed Damage:**
   - Always 50-200 regardless of skill
   - High-resistance targets take minimal damage

4. **One-Time Use:**
   - Trap consumed when triggered
   - Must recast to trap again

---

## 🆚 vs Other Spells

### **vs Blade Spirits (4th Circle):**
- **Magic Trap:** 18 gold, 5-40 damage, tactical utility
- **Blade Spirits:** 30+ gold, ~20-60 DPS, mobile attacker
- **Winner:** Blade Spirits for damage, Magic Trap for utility

### **vs Poison Field (5th Circle):**
- **Magic Trap:** 18 gold, instant damage, visible
- **Poison Field:** 40+ gold, damage over time, area
- **Winner:** Poison Field for area denial, Magic Trap for burst

### **vs Energy Bolt (6th Circle):**
- **Magic Trap:** 18 gold, 5-40 damage, delayed
- **Energy Bolt:** 50+ gold, 40-80 damage, instant
- **Winner:** Energy Bolt for reliable damage

### **Conclusion:**
Magic Trap is **NOT** for damage. It's for **utility and tactics**.

---

## 📚 Advanced Tips

### **Tip 1: Trap Layering**
```
Place 2 ground traps right next to each other at choke point
Enemy walks through both = double damage (36-74 total)
```

### **Tip 2: False Retreat**
```
Drop trap, pretend to run away, enemy chases
Actually loop back behind enemy while they trigger trap
Free backstab opportunity while they're taking damage
```

### **Tip 3: Container Bait**
```
Drop trapped chest with 100 gold on ground
Enemy sees "free loot", opens chest
Takes 50-200 damage for 100 gold
You profit from their greed
```

### **Tip 4: Boss Pre-Damage**
```
Know boss spawn location? Place 2 traps there
Boss spawns, immediately walks through traps
Start boss fight with 40-80 damage already dealt
```

### **Tip 5: EvalInt Check**
```
High EvalInt characters can pass through ground traps safely
Use this to create "mage only" paths in your house
Non-mages eat damage, mages pass freely
```

---

## ✅ Conclusion

### **Magic Trap is GOOD for:**
- ✅ Container security (best use case)
- ✅ Tactical disruption in PvP
- ✅ Area denial in narrow spaces
- ✅ Cheap utility (18 gold)
- ✅ Punishing thieves/griefers

### **Magic Trap is BAD for:**
- ❌ High direct damage (other spells better)
- ❌ Open area combat (easily avoided)
- ❌ Long-term area control (3 min decay)
- ❌ Vs high-resistance targets (weak damage)

---

## 📊 Final Ratings

| Category | Rating | Notes |
|----------|--------|-------|
| **PvP Usefulness** | ⭐⭐⭐☆☆ | Situational, best for retreats |
| **PvE Usefulness** | ⭐⭐☆☆☆ | Better spells exist |
| **Security Value** | ⭐⭐⭐⭐⭐ | Excellent for container protection |
| **Cost Efficiency** | ⭐⭐⭐⭐☆ | Cheap reagents, good value |
| **Skill Requirement** | ⭐⭐☆☆☆ | Low (2nd Circle, easy) |
| **Tactical Depth** | ⭐⭐⭐⭐☆ | Many creative uses |
| **Overall** | ⭐⭐⭐☆☆ | Niche but effective in its role |

---

**Best Used By:**
- Home owners (security)
- Thieves/scouts (ambush setups)
- Kiting mages (retreat utility)
- Dungeon runners (pull optimization)

**Rarely Used By:**
- Pure damage dealers
- Tank warriors
- Open-field PvPers

**Verdict:** A **utility spell** that excels in its niche (container security and tactical disruption) but isn't powerful enough to be mandatory. Fun and creative, but not a "meta" spell.


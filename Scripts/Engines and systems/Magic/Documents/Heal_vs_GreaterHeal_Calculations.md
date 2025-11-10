# Heal vs Greater Heal - Calculation Comparison & Simulation

## Calculation Formulas

### Step-by-Step Process

**Base Calculation:**
```
getBeneficialMageryInscribePercentage(Magery, Inscribe):
  maxPercent = Inscribe / 3 (min 1)
  influence = (maxPercent / 100) + 1
  points = (Magery * influence) / 3

Base Heal = points / 3
```

**Skill Bonuses:**
```
Inscription Bonus = Ceiling((Inscribe / 10) * 0.3)
Healing Bonus = (Healing / 10) * 1
```

**For Greater Heal:**
```
After Skill Bonuses: Multiply by 2.0
```

**Modifiers (Both Spells):**
```
Other Target Bonus: * 1.15 (if healing others)
Power Reduction: * 0.7 (30% reduction)
Random Variance: - (2-5% of current value)
Consecutive Cast Penalty: - 25% (if within 2 seconds)
Self-Heal Bonus: +5% (Ceiling) when healing yourself (you know your body better)
```

---

## Calculation Scenarios

### Scenario 1: Novice (Magery 50, Inscribe 50, Healing 50)

#### Heal Calculation

**Step 1: Base Calculation**
```
maxPercent = 50 / 3 = 16.67
influence = (16.67 / 100) + 1 = 1.1667
points = (50 * 1.1667) / 3 = 19.45
Base Heal = 19.45 / 3 = 6.48 → 6
```

**Step 2: Skill Bonuses**
```
Inscription Bonus = Ceiling((50 / 10) * 0.3) = Ceiling(1.5) = 2
Healing Bonus = (50 / 10) * 1 = 5
Subtotal = 6 + 2 + 5 = 13
```

**Step 3: Other Target Bonus (Healing Others)**
```
Subtotal = 13 * 1.15 = 14.95 → 14
```

**Step 4: Power Reduction**
```
14 * 0.7 = 9.8 → 9
```

**Step 5: Random Variance (2-5%)**
```
Min Variance: 9 - (9 * 0.05) = 9 - 0.45 = 8.55 → 8
Max Variance: 9 - (9 * 0.02) = 9 - 0.18 = 8.82 → 8
```

**Step 6: Self-Heal Bonus (5% when healing yourself)**
```
Self: 8 * 0.05 = 0.4 → Ceiling = 1, so 8 + 1 = 9
Others: No bonus (healing others)
```

**Final Heal Range:**
- **Self:** 8-9 HP (with self-heal bonus)
- **Others:** 7-8 HP
- **With Consecutive Cast:** 6-7 HP (others), 7 HP (self)

#### Greater Heal Calculation

**Step 1: Base Calculation (Same as Heal)**
```
Base Heal = 6
```

**Step 2: Skill Bonuses (Same as Heal)**
```
Subtotal = 6 + 2 + 5 = 13
```

**Step 3: 2.0x Multiplier**
```
13 * 2.0 = 26
```

**Step 4: Other Target Bonus (Healing Others)**
```
26 * 1.15 = 29.9 → 29
```

**Step 5: Power Reduction**
```
29 * 0.7 = 20.3 → 20
```

**Step 6: Random Variance (2-5%)**
```
Min Variance: 20 - (20 * 0.05) = 20 - 1.0 = 19
Max Variance: 20 - (20 * 0.02) = 20 - 0.4 = 19.6 → 19
```

**Step 7: Self-Heal Bonus (5% when healing yourself)**
```
Self: 19 * 0.05 = 0.95 → Ceiling = 1, so 19 + 1 = 20
Others: No bonus (healing others)
```

**Final Greater Heal Range:**
- **Self:** 20 HP
- **Others:** 19 HP
- **With Consecutive Cast:** 14-15 HP (others), 15 HP (self)

**Ratio:** Greater Heal is **~2.4x** more powerful than Heal at this skill level

---

### Scenario 2: Adept (Magery 80, Inscribe 80, Healing 80)

#### Heal Calculation

**Step 1: Base Calculation**
```
maxPercent = 80 / 3 = 26.67
influence = (26.67 / 100) + 1 = 1.2667
points = (80 * 1.2667) / 3 = 33.78
Base Heal = 33.78 / 3 = 11.26 → 11
```

**Step 2: Skill Bonuses**
```
Inscription Bonus = Ceiling((80 / 10) * 0.3) = Ceiling(2.4) = 3
Healing Bonus = (80 / 10) * 1 = 8
Subtotal = 11 + 3 + 8 = 22
```

**Step 3: Other Target Bonus**
```
22 * 1.15 = 25.3 → 25
```

**Step 4: Power Reduction**
```
25 * 0.7 = 17.5 → 17
```

**Step 5: Random Variance**
```
Min: 17 - (17 * 0.05) = 16.15 → 16
Max: 17 - (17 * 0.02) = 16.66 → 16
```

**Step 6: Self-Heal Bonus (5% when healing yourself)**
```
Self: 16 * 0.05 = 0.8 → Ceiling = 1, so 16 + 1 = 17
Others: No bonus (healing others)
```

**Final Heal Range:**
- **Self:** 16-17 HP (with self-heal bonus)
- **Others:** 15-16 HP
- **With Consecutive Cast:** 12 HP (others), 12-13 HP (self)

#### Greater Heal Calculation

**Step 1-2: Base + Skills**
```
Subtotal = 22
```

**Step 3: 2.0x Multiplier**
```
22 * 2.0 = 44
```

**Step 4: Other Target Bonus**
```
44 * 1.15 = 50.6 → 50
```

**Step 5: Power Reduction**
```
50 * 0.7 = 35
```

**Step 6: Random Variance**
```
Min: 35 - (35 * 0.05) = 33.25 → 33
Max: 35 - (35 * 0.02) = 34.3 → 34
```

**Step 7: Self-Heal Bonus (5% when healing yourself)**
```
Self: 33 * 0.05 = 1.65 → Ceiling = 2, so 33 + 2 = 35
Self: 34 * 0.05 = 1.7 → Ceiling = 2, so 34 + 2 = 36
Others: No bonus (healing others)
```

**Final Greater Heal Range:**
- **Self:** 35-36 HP
- **Others:** 33-34 HP
- **With Consecutive Cast:** 25 HP (others), 26-27 HP (self)

**Ratio:** Greater Heal is **~2.1x** more powerful than Heal

---

### Scenario 3: Expert (Magery 100, Inscribe 100, Healing 100)

#### Heal Calculation

**Step 1: Base Calculation**
```
maxPercent = 100 / 3 = 33.33
influence = (33.33 / 100) + 1 = 1.3333
points = (100 * 1.3333) / 3 = 44.44
Base Heal = 44.44 / 3 = 14.81 → 14
```

**Step 2: Skill Bonuses**
```
Inscription Bonus = Ceiling((100 / 10) * 0.3) = Ceiling(3.0) = 3
Healing Bonus = (100 / 10) * 1 = 10
Subtotal = 14 + 3 + 10 = 27
```

**Step 3: Other Target Bonus**
```
27 * 1.15 = 31.05 → 31
```

**Step 4: Power Reduction**
```
31 * 0.7 = 21.7 → 21
```

**Step 5: Random Variance**
```
Min: 21 - (21 * 0.05) = 19.95 → 19
Max: 21 - (21 * 0.02) = 20.58 → 20
```

**Step 6: Self-Heal Bonus (5% when healing yourself)**
```
Self: 19 * 0.05 = 0.95 → Ceiling = 1, so 19 + 1 = 20
Self: 20 * 0.05 = 1.0 → Ceiling = 1, so 20 + 1 = 21
Others: No bonus (healing others)
```

**Final Heal Range:**
- **Self:** 20-21 HP (with self-heal bonus)
- **Others:** 19-20 HP
- **With Consecutive Cast:** 14-15 HP (others), 15-16 HP (self)

#### Greater Heal Calculation

**Step 1-2: Base + Skills**
```
Subtotal = 27
```

**Step 3: 2.0x Multiplier**
```
27 * 2.0 = 54
```

**Step 4: Other Target Bonus**
```
54 * 1.15 = 62.1 → 62
```

**Step 5: Power Reduction**
```
62 * 0.7 = 43.4 → 43
```

**Step 6: Random Variance**
```
Min: 43 - (43 * 0.05) = 40.85 → 40
Max: 43 - (43 * 0.02) = 42.14 → 42
```

**Step 7: Self-Heal Bonus (5% when healing yourself)**
```
Self: 40 * 0.05 = 2.0 → Ceiling = 2, so 40 + 2 = 42
Self: 42 * 0.05 = 2.1 → Ceiling = 3, so 42 + 3 = 45
Others: No bonus (healing others)
```

**Final Greater Heal Range:**
- **Self:** 42-45 HP
- **Others:** 40-42 HP
- **With Consecutive Cast:** 30-32 HP (others), 32-34 HP (self)

**Ratio:** Greater Heal is **~2.1x** more powerful than Heal

---

### Scenario 4: Master (Magery 120, Inscribe 120, Healing 120)

#### Heal Calculation

**Step 1: Base Calculation**
```
maxPercent = 120 / 3 = 40
influence = (40 / 100) + 1 = 1.4
points = (120 * 1.4) / 3 = 56
Base Heal = 56 / 3 = 18.67 → 18
```

**Step 2: Skill Bonuses**
```
Inscription Bonus = Ceiling((120 / 10) * 0.3) = Ceiling(3.6) = 4
Healing Bonus = (120 / 10) * 1 = 12
Subtotal = 18 + 4 + 12 = 34
```

**Step 3: Other Target Bonus**
```
34 * 1.15 = 39.1 → 39
```

**Step 4: Power Reduction**
```
39 * 0.7 = 27.3 → 27
```

**Step 5: Random Variance**
```
Min: 27 - (27 * 0.05) = 25.65 → 25
Max: 27 - (27 * 0.02) = 26.46 → 26
```

**Step 6: Self-Heal Bonus (5% when healing yourself)**
```
Self: 25 * 0.05 = 1.25 → Ceiling = 2, so 25 + 2 = 27
Self: 26 * 0.05 = 1.3 → Ceiling = 2, so 26 + 2 = 28
Others: No bonus (healing others)
```

**Final Heal Range:**
- **Self:** 27-28 HP (with self-heal bonus)
- **Others:** 25-26 HP
- **With Consecutive Cast:** 19 HP (others), 20 HP (self)

#### Greater Heal Calculation

**Step 1-2: Base + Skills**
```
Subtotal = 34
```

**Step 3: 2.0x Multiplier**
```
34 * 2.0 = 68
```

**Step 4: Other Target Bonus**
```
68 * 1.15 = 78.2 → 78
```

**Step 5: Power Reduction**
```
78 * 0.7 = 54.6 → 54
```

**Step 6: Random Variance**
```
Min: 54 - (54 * 0.05) = 51.3 → 51
Max: 54 - (54 * 0.02) = 52.92 → 52
```

**Step 7: Self-Heal Bonus (5% when healing yourself)**
```
Self: 51 * 0.05 = 2.55 → Ceiling = 3, so 51 + 3 = 54
Self: 52 * 0.05 = 2.6 → Ceiling = 3, so 52 + 3 = 55
Others: No bonus (healing others)
```

**Final Greater Heal Range:**
- **Self:** 54-55 HP
- **Others:** 51-52 HP
- **With Consecutive Cast:** 38-39 HP (others), 40-41 HP (self)

**Ratio:** Greater Heal is **~2.0x** more powerful than Heal

---

## Summary Comparison Table

| Skill Level | Spell | Self Heal (Min-Max) | Others Heal (Min-Max) | Consecutive Cast (Self) | Consecutive Cast (Others) |
|-------------|-------|---------------------|----------------------|------------------------|---------------------------|
| **50/50/50** | Heal | 8-9 HP | 7-8 HP | 7 HP | 6-7 HP |
| | Greater Heal | 20 HP | 19 HP | 15 HP | 14-15 HP |
| | **Ratio** | **2.2x-2.5x** | **2.4x** | **2.1x** | **2.1x** |
| **80/80/80** | Heal | 16-17 HP | 15-16 HP | 12-13 HP | 12 HP |
| | Greater Heal | 35-36 HP | 33-34 HP | 26-27 HP | 25 HP |
| | **Ratio** | **2.1x** | **2.1x** | **2.1x** | **2.1x** |
| **100/100/100** | Heal | 20-21 HP | 19-20 HP | 15-16 HP | 14-15 HP |
| | Greater Heal | 42-45 HP | 40-42 HP | 32-34 HP | 30-32 HP |
| | **Ratio** | **2.2x** | **2.1x** | **2.0x** | **2.0x** |
| **120/120/120** | Heal | 27-28 HP | 25-26 HP | 20 HP | 19 HP |
| | Greater Heal | 54-55 HP | 51-52 HP | 40-41 HP | 38-39 HP |
| | **Ratio** | **2.0x** | **2.0x** | **2.0x** | **2.0x** |

---

## Key Observations

### Minimum and Maximum Values

**Heal (1st Circle):**
- **Minimum:** 7 HP (at 50/50/50 skills, others)
- **Maximum:** 28 HP (at 120/120/120 skills, self, no consecutive cast)

**Greater Heal (4th Circle):**
- **Minimum:** 19 HP (at 50/50/50 skills, others)
- **Maximum:** 55 HP (at 120/120/120 skills, self, no consecutive cast)

### Power Ratio Analysis

The power ratio converges to **~2.0x** at higher skill levels (120+), which matches the design goal. At lower skill levels, the ratio ranges from **2.0x to 2.4x** due to rounding effects and skill bonus scaling.

### Impact of Consecutive Cast Penalty

- **Heal:** Loses ~25% effectiveness (19-20 HP → 14-15 HP at 100 skills)
- **Greater Heal:** Loses ~25% effectiveness (40-42 HP → 30-32 HP at 100 skills)
- Both spells maintain the same penalty percentage, ensuring consistency

### Skill Bonus Impact

**At 100/100/100:**
- Inscription adds: +3 HP (both spells)
- Healing adds: +10 HP (both spells)
- Total skill bonus: +13 HP base, which becomes +26 HP for Greater Heal after 2.0x multiplier

**At 120/120/120:**
- Inscription adds: +4 HP (both spells)
- Healing adds: +12 HP (both spells)
- Total skill bonus: +16 HP base, which becomes +32 HP for Greater Heal after 2.0x multiplier

---

## Conclusion

Greater Heal successfully achieves the **~2.0x power ratio** target, with actual ratios ranging from **2.0x to 2.4x** depending on skill level. The variance is primarily due to:

1. **Rounding effects** at different calculation stages
2. **Skill bonus scaling** (Inscription uses Ceiling, creating non-linear scaling)
3. **Random variance** (2-5% reduction) creating small ranges

The implementation is **consistent and balanced**, with both spells using identical calculation logic except for the 2.0x multiplier applied to Greater Heal.


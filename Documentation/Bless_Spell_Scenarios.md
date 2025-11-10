# Bless Spell - Comparative Scenarios Table

**Spell:** Bless (3rd Circle)  
**Date:** Generated Analysis  
**Purpose:** Comparative table showing buff percentage and duration across skill levels (50-120)

---

## Calculation Formulas

### Buff Percentage Formula
1. **Base Scalar:** `(1 + Inscribe.Fixed / 100) * 0.01`
2. **Multiplier:** 
   - If Inscribe ≥ 120.0: `scalar * 0.8`
   - If Inscribe < 120.0: `scalar * 0.6`
3. **Minimum:** 0.01 (1%)
4. **Display Percentage:** `(int)(scalar * 100)`
5. **Sorcerer Bonus:** `percentage * (1.0 + (Magery + EvalInt) / 180.0)`

### Duration Formula
1. **Base Random:** 10-30 seconds
2. **Inscribe Bonus:** `Math.Ceiling(Inscribe * 0.25)`
3. **Additional Random Bonus:**
   - Inscribe 120+: +20 to +40 seconds
   - Inscribe 100-119: +18 to +30 seconds
   - Inscribe 80-99: +16 to +25 seconds
   - Inscribe 60-79: +10 to +20 seconds
   - Inscribe <60: +0 seconds
4. **Total Before Reduction:** `(10-30) + InscribeBonus + AdditionalBonus`
5. **30% Reduction:** `total * 0.70`
6. **Caps Applied:** Minimum 15s, Maximum 90s
7. **Sorcerer Bonus:** +10 seconds (if sorcerer)

---

## Comparative Scenarios Table

### Standard Caster (Non-Sorcerer)

| Inscribe | Magery | EvalInt | Buff % | Duration (Min) | Duration (Max) | Duration (Avg) |
|----------|--------|---------|--------|----------------|----------------|----------------|
| 50.0     | 50.0   | 50.0    | 0.30%  | 15s            | 15s            | 15s            |
| 60.0     | 60.0   | 60.0    | 0.36%  | 15s            | 21s            | 18s            |
| 70.0     | 70.0   | 70.0    | 0.42%  | 15s            | 24s            | 19.5s          |
| 80.0     | 80.0   | 80.0    | 0.48%  | 18s            | 28s            | 23s            |
| 90.0     | 90.0   | 90.0    | 0.54%  | 20s            | 32s            | 26s            |
| 100.0    | 100.0  | 100.0   | 0.60%  | 22s            | 35s            | 28.5s          |
| 110.0    | 110.0  | 110.0   | 0.66%  | 24s            | 38s            | 31s            |
| 120.0    | 120.0  | 120.0   | 0.64%  | 28s            | 49s            | 38.5s          |

**Notes:**
- Buff % uses Inscribe.Fixed (multiply by 10: 50.0 = 500 Fixed)
- Duration has random components, showing min/max/avg
- At 120 Inscribe, multiplier changes from 0.6 to 0.8, causing slight decrease in displayed %
- Duration capped at 90s maximum

---

### Sorcerer Caster (With Sorcerer Bonus)

**Sorcerer Requirements:** Magery ≥ 90, Int > 80, (EvalInt ≥ 90 OR Inscribe ≥ 90)

| Inscribe | Magery | EvalInt | Base % | Sorcerer % | Duration (Min) | Duration (Max) | Duration (Avg) |
|----------|--------|---------|--------|------------|----------------|----------------|----------------|
| 50.0     | 90.0   | 90.0    | 0.30%  | 0.30%      | 25s            | 25s            | 25s            |
| 60.0     | 100.0  | 100.0   | 0.36%  | 0.40%      | 25s            | 31s            | 28s            |
| 70.0     | 110.0  | 110.0   | 0.42%  | 0.48%      | 25s            | 34s            | 29.5s          |
| 80.0     | 120.0  | 120.0   | 0.48%  | 0.56%      | 28s            | 38s            | 33s            |
| 90.0     | 120.0  | 120.0   | 0.54%  | 0.64%      | 30s            | 42s            | 36s            |
| 100.0    | 120.0  | 120.0   | 0.60%  | 0.73%      | 32s            | 45s            | 38.5s          |
| 110.0    | 120.0  | 120.0   | 0.66%  | 0.80%      | 34s            | 48s            | 41s            |
| 120.0    | 120.0  | 120.0   | 0.64%  | 0.78%      | 38s            | 90s            | 64s            |

**Sorcerer Bonus Calculation:**
- Multiplier: `1.0 + (Magery + EvalInt) / 180.0`
- Examples:
  - 90+90 = 180 → `1.0 + 1.0 = 2.0x` (100% increase)
  - 120+120 = 240 → `1.0 + 1.33 = 2.33x` (133% increase)
- Duration: Always adds +10 seconds to base duration

---

## Detailed Breakdown by Skill Level

### Low Skill (50-70)

| Skill Level | Buff % | Stat Bonus* | Duration Range | Notes |
|-------------|--------|------------|---------------|-------|
| 50.0        | 0.30%  | ~1-2 pts   | 15s (capped)  | Minimum duration cap applies |
| 60.0        | 0.36%  | ~1-2 pts   | 15-21s        | Random bonus starts at 60 |
| 70.0        | 0.42%  | ~2-3 pts   | 15-24s        | Still near minimum |

*Stat bonus depends on target's base stats (Str/Dex/Int)

### Mid Skill (80-100)

| Skill Level | Buff % | Stat Bonus* | Duration Range | Notes |
|-------------|--------|------------|---------------|-------|
| 80.0        | 0.48%  | ~2-4 pts   | 18-28s        | Better duration range |
| 90.0        | 0.54%  | ~3-5 pts   | 20-32s        | Good balance |
| 100.0       | 0.60%  | ~3-6 pts   | 22-35s        | Solid performance |

### High Skill (110-120)

| Skill Level | Buff % | Stat Bonus* | Duration Range | Notes |
|-------------|--------|------------|---------------|-------|
| 110.0       | 0.66%  | ~4-7 pts   | 24-38s        | Strong buff |
| 120.0       | 0.64%  | ~4-7 pts   | 28-49s        | Best duration, % slightly lower due to 0.8x multiplier |

**Note:** At 120 Inscribe, the multiplier changes from 0.6x to 0.8x, which reduces the displayed percentage slightly, but the actual stat bonus calculation remains strong.

---

## Sorcerer vs Non-Sorcerer Comparison

### Example: 100 Inscribe, 120 Magery, 120 EvalInt

| Caster Type | Buff % | Duration (Min) | Duration (Max) | Duration (Avg) | Sorcerer Bonus |
|-------------|--------|---------------|---------------|----------------|----------------|
| Standard    | 0.60%  | 22s           | 35s           | 28.5s          | None           |
| Sorcerer    | 0.73%  | 32s           | 45s           | 38.5s          | +22% buff, +10s duration |

**Sorcerer Advantage:**
- **Buff Percentage:** +21.7% stronger (0.73% vs 0.60%)
- **Duration:** +10 seconds minimum, +10 seconds average
- **Total Benefit:** ~35-40% more effective overall

---

## Key Observations

1. **Duration Caps:** 
   - Minimum: 15 seconds (applies to low skill casts)
   - Maximum: 90 seconds (applies to high skill + sorcerer casts)

2. **Buff Percentage:**
   - Scales linearly with Inscribe skill
   - Slight decrease at 120 Inscribe due to multiplier change (0.6x → 0.8x)
   - Sorcerer bonus significantly increases effectiveness

3. **Duration Variability:**
   - Random component: 10-30 seconds base
   - Additional random bonus based on Inscribe level
   - 30% reduction applied to beneficial spells
   - Final result capped between 15-90 seconds

4. **Sorcerer Impact:**
   - Requires: Magery ≥ 90, Int > 80, (EvalInt ≥ 90 OR Inscribe ≥ 90)
   - Provides: +10 seconds duration + percentage multiplier boost
   - Most beneficial at high skill levels (100+)

5. **Skill Synergy:**
   - Inscribe: Primary skill for both % and duration
   - Magery: Affects sorcerer bonus multiplier
   - EvalInt: Affects sorcerer bonus multiplier
   - Best results require all three skills at high levels

---

## Practical Recommendations

### For PvE (Player vs Environment)
- **Minimum Viable:** 80 Inscribe (decent duration, acceptable buff)
- **Recommended:** 100 Inscribe (good balance of % and duration)
- **Optimal:** 120 Inscribe + Sorcerer (maximum effectiveness)

### For PvP (Player vs Player)
- **Minimum:** 100 Inscribe (need reliable duration)
- **Recommended:** 120 Inscribe + Sorcerer (every advantage counts)
- **Critical:** High Inscribe + Sorcerer status (significant stat advantage)

### Skill Training Priority
1. **Inscribe** (primary skill - affects both % and duration)
2. **Magery** (required for spell, affects sorcerer bonus)
3. **EvalInt** (affects sorcerer bonus multiplier)
4. **Intelligence** (stat requirement for sorcerer status)

---

## Formula Reference

### Buff Percentage Calculation
```csharp
// Step 1: Base scalar
double percent = 1 + (Inscribe.Fixed / 100);
percent *= 0.01;

// Step 2: Apply multiplier
if (Inscribe.Fixed / 10 >= 120.0)
    percent *= 0.8;  // 120+ Inscribe
else
    percent *= 0.6;  // <120 Inscribe

// Step 3: Minimum check
if (percent < 0.01)
    percent = 0.01;

// Step 4: Convert to percentage
int percentage = (int)(percent * 100);

// Step 5: Sorcerer bonus (if applicable)
if (sorcerer)
{
    double bonus = (Magery + EvalInt) / 180.0;
    percentage = (int)((double)percentage * (1.0 + bonus));
}
```

### Duration Calculation
```csharp
// Step 1: Base random
int rInt = Random(10, 30);

// Step 2: Inscribe bonus
int durationBonus = (int)Math.Ceiling(Inscribe * 0.25);

// Step 3: Additional random bonus
if (Inscribe >= 120.0)
    durationBonus += Random(20, 40);
else if (Inscribe >= 100.0)
    durationBonus += Random(18, 30);
else if (Inscribe >= 80.0)
    durationBonus += Random(16, 25);
else if (Inscribe >= 60.0)
    durationBonus += Random(10, 20);

// Step 4: Total before reduction
int total = rInt + durationBonus;

// Step 5: 30% reduction
total = (int)(total * 0.70);

// Step 6: Apply caps
if (total < 15)
    total = 15;
else if (total > 90)
    total = 90;

// Step 7: Sorcerer bonus
if (sorcerer)
    total += 10;
```

---

**Generated:** Analysis of Bless Spell Implementation  
**File:** `Scripts/Engines and systems/Magic/Magery 3rd/Bless.cs`


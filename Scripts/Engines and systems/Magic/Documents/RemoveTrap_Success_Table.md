# Remove Trap Spell - Success Percentage Comparison Table

## Formula

**Effective Skill Calculation:**
```
Base Skill = Magery / 3
Effective Skill = Base Skill - Random(0-1)
Success = Effective Skill >= TrapLevel
```

**Base Success Percentage:**
- If `(Magery/3) - 1 >= TrapLevel`: **100%** base success (guaranteed)
- If `Magery/3 > TrapLevel`: **((Magery/3) - TrapLevel) × 100%** (partial success)
- If `Magery/3 <= TrapLevel`: **((Magery/3) / TrapLevel) × 30%** (diminishing returns)
  - This handles both exact equality and when base skill is below trap level
  - Example: Magery 120 (Base 40) vs Trap Level 40 = (40/40) × 30% = **30%**
  - Example: Magery 120 (Base 40) vs Trap Level 50 = (40/50) × 30% = **24%**

**Remove Trap Skill Bonus:**
- Each 10 points of Remove Trap skill = **+1%** success chance
- Maximum bonus: **12%** (at 120 skill)
- Bonus is added to base success percentage

**Final Success Percentage:**
```
Final % = Base % + Remove Trap Bonus
Final % = Min(100%, Base % + Floor(RemoveTrapSkill / 10))
```

---

## Remove Trap Skill Bonus Table

| Remove Trap Skill | Bonus % | Notes |
|-------------------|---------|-------|
| 0-9 | 0% | No bonus |
| 10-19 | +1% | Beginner |
| 20-29 | +2% | Apprentice |
| 30-39 | +3% | Adept |
| 40-49 | +4% | Expert |
| 50-59 | +5% | Master |
| 60-69 | +6% | Grandmaster |
| 70-79 | +7% | Legendary |
| 80-89 | +8% | Mythical |
| 90-99 | +9% | Transcendent |
| 100-109 | +10% | Supreme |
| 110-119 | +11% | Ultimate |
| 120 | +12% | Maximum bonus |

---

## Success Percentage Table (Base Only - No Remove Trap Bonus)

*Note: Add Remove Trap skill bonus to these values (see table above)*

| Magery Skill | Base Skill (÷3) | Trap Level 0 | Trap Level 10 | Trap Level 20 | Trap Level 30 | Trap Level 40 | Trap Level 50 | Trap Level 60 | Trap Level 70 | Trap Level 80 | Trap Level 90 |
|--------------|-----------------|--------------|---------------|---------------|---------------|---------------|---------------|---------------|---------------|---------------|---------------|
| **0** | 0.0 | 0% | 0% | 0% | 0% | 0% | 0% | 0% | 0% | 0% | 0% |
| **30** | 10.0 | 100% | 0% | 0% | 0% | 0% | 0% | 0% | 0% | 0% | 0% |
| **50** | 16.7 | 100% | 67% | 0% | 0% | 0% | 0% | 0% | 0% | 0% | 0% |
| **60** | 20.0 | 100% | 100% | 0% | 0% | 0% | 0% | 0% | 0% | 0% | 0% |
| **70** | 23.3 | 100% | 100% | 33% | 0% | 0% | 0% | 0% | 0% | 0% | 0% |
| **80** | 26.7 | 100% | 100% | 67% | 0% | 0% | 0% | 0% | 0% | 0% | 0% |
| **90** | 30.0 | 100% | 100% | 100% | 0% | 0% | 0% | 0% | 0% | 0% | 0% |
| **100** | 33.3 | 100% | 100% | 100% | 100% | 25% | 20% | 17% | 14% | 12% | 11% |
| **110** | 36.7 | 100% | 100% | 100% | 100% | 28% | 22% | 18% | 16% | 14% | 12% |
| **120** | 40.0 | 100% | 100% | 100% | 100% | 30% | 24% | 20% | 17% | 15% | 13% |

---

## Detailed Breakdown by Magery Level

### **Magery 0-29 (Base Skill: 0-9.7)**
- **Trap Level 0-8**: 0-100% (variable)
- **Trap Level 9+**: 0% success

### **Magery 30-59 (Base Skill: 10-19.7)**
- **Trap Level 0-9**: 100% success
- **Trap Level 10-19**: 0-90% (variable)
- **Trap Level 20+**: 0% success

### **Magery 60-89 (Base Skill: 20-29.7)**
- **Trap Level 0-19**: 100% success
- **Trap Level 20-29**: 0-100% (variable)
- **Trap Level 30+**: 0% success

### **Magery 90-119 (Base Skill: 30-39.7)**
- **Trap Level 0-29**: 100% success (guaranteed for most)
- **Trap Level 30-39**: 0-100% (variable, depends on exact Magery)
- **Trap Level 40+**: 11-30% success (diminishing returns, scales with Magery)

### **Magery 120 (Base Skill: 40.0)**
- **Trap Level 0-39**: 100% success (guaranteed)
- **Trap Level 40**: 30% success (exactly at limit, using diminishing returns)
- **Trap Level 41-50**: 24-29% success (diminishing returns)
- **Trap Level 51-60**: 20-22% success
- **Trap Level 61-70**: 17-19% success
- **Trap Level 71-80**: 15-16% success
- **Trap Level 81-90**: 13-14% success

---

## Key Insights

1. **Minimum Magery Required:**
   - To have ANY chance (base): `Magery >= TrapLevel × 3`
   - For 100% base success: `Magery >= (TrapLevel + 1) × 3`
   - **With Remove Trap 120 bonus (+12%)**: Can achieve partial success even when base is 0%

2. **Maximum Removable Trap Level:**
   - With Magery 120 (base): Can remove up to Trap Level 39 (100% success)
   - **Trap Level 40**: **30% base success** (exactly at limit, using diminishing returns)
   - **With Remove Trap 120**: Trap Level 40 = 30% + 12% = **42% success**
   - **Trap Level 50**: **24% base success** (with diminishing returns formula)
   - **With Remove Trap 120**: Trap Level 50 = 24% + 12% = **36% success**
   - **All trap levels 40+** now have some chance of removal (diminishing returns)

3. **Partial Success Range:**
   - Occurs when: `(Magery/3) - 1 < TrapLevel <= (Magery/3)`
   - Base Success % = `((Magery/3) - TrapLevel) × 100%`
   - **Remove Trap bonus adds flat percentage on top**

4. **Random Variance Impact:**
   - The `-Random(0-1)` penalty creates a 1-point uncertainty
   - This means you need `Magery/3 >= TrapLevel + 1` for guaranteed base success
   - **Remove Trap bonus helps overcome this uncertainty**

5. **Remove Trap Skill Value:**
   - **12% bonus at 120 skill** can make the difference between 0% and 12% success
   - Especially valuable for high-level traps where base success is low
   - Can turn impossible (0%) into possible (1-12%) for traps 1-12 levels above your Magery limit

---

## Examples

### Example 1: Magery 50 vs Trap Level 10 (No Remove Trap Skill)
```
Base Skill = 50 / 3 = 16.7
Min Effective = 16.7 - 1 = 15.7
Max Effective = 16.7

Trap Level 10:
- Min (15.7) >= 10: Not guaranteed
- Base (16.7) >= 10: Yes
- Base Success % = (16.7 - 10) × 100% = 67%
- Remove Trap Bonus = 0% (no skill)
- Final Success % = 67% + 0% = 67%
```

### Example 1b: Magery 50 vs Trap Level 10 (With Remove Trap 50)
```
Base Success % = 67% (from above)
Remove Trap Bonus = Floor(50 / 10) = 5%
Final Success % = 67% + 5% = 72%
```

### Example 2: Magery 100 vs Trap Level 30 (No Remove Trap Skill)
```
Base Skill = 100 / 3 = 33.3
Min Effective = 33.3 - 1 = 32.3
Max Effective = 33.3

Trap Level 30:
- Min (32.3) >= 30: Yes, but not 100% (32.3 < 30+1)
- Base (33.3) >= 30: Yes
- Base Success % = (33.3 - 30) × 100% = 33%
- Remove Trap Bonus = 0% (no skill)
- Final Success % = 33% + 0% = 33%
```

### Example 2b: Magery 100 vs Trap Level 30 (With Remove Trap 120)
```
Base Success % = 33% (from above)
Remove Trap Bonus = Floor(120 / 10) = 12%
Final Success % = 33% + 12% = 45%
```

### Example 3: Magery 120 vs Trap Level 50 (No Remove Trap Skill)
```
Base Skill = 120 / 3 = 40.0
Trap Level 50:
- Base (40.0) < 50: Using diminishing returns formula
- Base Success % = (40.0 / 50) × 30% = 0.8 × 30% = 24%
- Remove Trap Bonus = 0% (no skill)
- Final Success % = 24% + 0% = 24%
```

### Example 3b: Magery 120 vs Trap Level 50 (With Remove Trap 120)
```
Base Success % = 24% (from above)
Remove Trap Bonus = Floor(120 / 10) = 12%
Final Success % = 24% + 12% = 36%
```

### Example 3c: Magery 120 vs Trap Level 40 (No Remove Trap Skill)
```
Base Skill = 120 / 3 = 40.0
Trap Level 40:
- Base (40.0) = 40: Using diminishing returns formula (exact equality)
- Base Success % = (40.0 / 40) × 30% = 1.0 × 30% = 30%
- Remove Trap Bonus = 0% (no skill)
- Final Success % = 30% + 0% = 30%
```

### Example 3d: Magery 120 vs Trap Level 40 (With Remove Trap 120)
```
Base Success % = 30% (from above)
Remove Trap Bonus = Floor(120 / 10) = 12%
Final Success % = 30% + 12% = 42%
```

### Example 4: Magery 80 vs Trap Level 25 (With Remove Trap 100)
```
Base Skill = 80 / 3 = 26.7
Trap Level 25:
- Base Success % = (26.7 - 25) × 100% = 17%
- Remove Trap Bonus = Floor(100 / 10) = 10%
- Final Success % = 17% + 10% = 27%
```

---

## Notes

- Trap levels are typically capped at 90 (see ContainerFunctions.cs)
- Most common trap levels in game: 0-50
- **NEW: Diminishing Returns Formula** allows removal of traps above your skill level
  - When Base Skill < TrapLevel: Success % = (Base Skill / TrapLevel) × 30%
  - This makes high-level traps (40+) accessible with reduced chance
  - Example: Magery 120 can attempt Trap Level 50 with 24% base chance
- The random variance of 0-1 means you can never be 100% certain unless you exceed the trap level by at least 1 point
- **Remove Trap skill bonus is always beneficial**, even when base success is 0%
- **Maximum possible success with Remove Trap 120**: Can attempt traps up to 12 levels above your Magery limit (with 1-12% chance)
- **Synergy**: Combining high Magery (120) with Remove Trap 120 gives the best results for high-level traps
- **Trap Level 50 is now achievable**: With Magery 120 + Remove Trap 120 = 24% + 12% = **36% success chance**


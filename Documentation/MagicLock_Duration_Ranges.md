# Magic Lock Spell - Duration Ranges and Success Rates

## Overview
This document details the duration ranges and success rates for all Magic Lock spell effects based on caster skill levels.

---

## 1. Container Locking (LockableContainer)

### Duration
**Permanent** - Containers locked with Magic Lock remain locked indefinitely until manually unlocked.

### Lock Level Calculation
- **Formula**: `LockLevel = min(Magery, 75)`
- **Range**: 0 to 75
- **MaxLockLevel**: 120 (set on container)
- **RequiredSkill**: Same as LockLevel

### Examples
- Magery 50 → LockLevel 50
- Magery 75 → LockLevel 75
- Magery 100 → LockLevel 75 (capped)
- Magery 120 → LockLevel 75 (capped)

---

## 2. Door Locking (BaseDoor - Dungeon Doors Only)

### Duration Calculation
**Formula**: `Duration = 10 + (Magery * 80) / 120` (linear progression, clamped between 10-90 seconds)

### Duration Range
| Magery Skill | Calculation | Duration (seconds) |
|--------------|-------------|-------------------|
| 0            | 10 + 0      | **10** (minimum)  |
| 20           | 10 + 13.33  | **23**            |
| 40           | 10 + 26.67  | **37**            |
| 60           | 10 + 40     | **50**            |
| 80           | 10 + 53.33  | **63**            |
| 100          | 10 + 66.67  | **77**            |
| 120+         | 10 + 80     | **90** (maximum)  |

### Notes
- Only works on dungeon doors (BardDungeonRegion or DungeonRegion)
- Door automatically unlocks after duration expires
- Timer uses `TimerPriority.OneSecond`
- Linear progression: Each skill point adds approximately 0.67 seconds

---

## 3. Trapped Souls (LockedCreature)

### Duration Calculation
**Formula**: `Duration = (10 + 3 * Magery) / 2` (minimum 10 seconds)

### Duration Range
| Magery Skill | Calculation | Duration (seconds) |
|--------------|-------------|-------------------|
| 0            | (10+0)/2 = 5 | 10 (clamped)      |
| 20           | (10+60)/2 = 35 | 35                |
| 40           | (10+120)/2 = 65 | 65                |
| 60           | (10+180)/2 = 95 | 95                |
| 80           | (10+240)/2 = 125 | 125               |
| 100          | (10+300)/2 = 155 | 155               |
| 120         | (10+360)/2 = 185 | 185               |

### Notes
- Duration is calculated when the trapped soul is released from the flask
- Uses the caster's Magery skill at the time of release
- Creature is automatically deleted when duration expires
- Minimum duration is 10 seconds regardless of skill level

---

## 4. Soul Capture Success Rate

### Success Rate Calculation
**Formula**: `Success% = 5% + (AverageSkill - 100) * 0.5%` (clamped between 5-15%)

Where `AverageSkill = (Magery + EvalInt) / 2`

### Success Rate Range
| Average Skill | Calculation | Success Rate |
|---------------|-------------|--------------|
| 100           | 5% + 0      | **5%**       |
| 105           | 5% + 2.5%   | **7.5%**     |
| 110           | 5% + 5%     | **10%**      |
| 115           | 5% + 7.5%   | **12.5%**    |
| 120+          | 5% + 10%    | **15%** (max)|

### Examples
- **Magery 100, EvalInt 100**: Average = 100 → **5% success**
- **Magery 110, EvalInt 110**: Average = 110 → **10% success**
- **Magery 120, EvalInt 120**: Average = 120 → **15% success**
- **Magery 120, EvalInt 100**: Average = 110 → **10% success**
- **Magery 100, EvalInt 120**: Average = 110 → **10% success**

### Requirements
- **Minimum Magery**: 100
- **Minimum EvalInt**: 100
- **Minimum Intelligence**: 100
- **Mana Cost**: 80
- **Required Item**: Empty Electrum Flask

---

## Summary Table

| Effect Type | Duration/Success | Skill Range | Result Range |
|-------------|------------------|-------------|--------------|
| **Container Lock** | Permanent | 0-120 Magery | LockLevel 0-75 |
| **Door Lock** | Temporary | 0-120 Magery | 10-90 seconds (linear) |
| **Trapped Soul** | Temporary | 0-120 Magery | 10-185 seconds |
| **Soul Capture** | Success Rate | 100-120 Avg | 5-15% chance |

---

## Code References

### Container Locking
- **File**: `MagicLock.cs`
- **Method**: `TryLockContainer()`
- **Lines**: 130-155

### Door Locking
- **File**: `MagicLock.cs`
- **Method**: `TryLockDoor()`, `InternalTimer`
- **Lines**: 192-210, 650-674

### Trapped Soul Duration
- **File**: `MagicLock.cs`
- **Class**: `LockedCreature`
- **Constructor**: Line 1086-1097

### Soul Capture Success Rate
- **File**: `MagicLock.cs`
- **Method**: `CalculateSoulCaptureSuccessRate()`
- **Lines**: 428-448


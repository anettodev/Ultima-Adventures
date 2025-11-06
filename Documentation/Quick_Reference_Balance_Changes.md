# Quick Reference: 2nd Circle Balance Changes

**Date:** November 6, 2025  
**Spells:** Strength, Agility, Cunning, Protection (2nd Circle)

---

## 🎯 What Changed?

### **STAT BUFFS (Strength/Agility/Cunning)**

#### **1. Stat Bonus: 30-50% Random Reduction**
```
OLD: Inscription 120 + STR 150 = +15 STR
NEW: Inscription 120 + STR 150 = +7-10 STR (50-70% of original)
```

#### **2. Duration: 30% Reduction + Caps**
```
OLD: 80-100 seconds
NEW: 56-60 seconds (capped at 60s max, 15s min)
```

#### **3. Anti-Stacking: Cannot Recast**
```
OLD: Could recast to extend or stack
NEW: "O alvo já está sob efeito deste feitiço."
```

---

### **PROTECTION SPELL**

#### **1. Resistance Penalties: Randomized**
```
OLD: All resistances -8 (total -40)
NEW: Each resistance -2 to -8 random (avg -25 total)
```

#### **2. Duration: 30% Reduction + 60s Cap**
```
OLD: 80-100 seconds
NEW: 39-60 seconds (capped at 60s max)
```

#### **3. Disruption Protection: First Hit → 50%**
```
OLD: 100% all hits during casting
NEW: 100% first hit, 50% subsequent hits
```

---

## 📊 Quick Lookup Tables

### **Stat Buffs (STR/AGI/CUN)**
| Inscribe | Base Stat | New Bonus Range | New Duration | 
|----------|-----------|-----------------|--------------|
| 50 | 80 | +1 | 15-31s |
| 80 | 100 | +2-3 | 35-52s |
| 100 | 120 | +3-4 | 45-59s |
| 120 | 150 | +7-10 | 56-60s |
| 120 | 200 | +10-14 | 56-60s |

### **Protection Spell**
| Inscribe | Resist Loss OLD | Resist Loss NEW | Duration OLD | Duration NEW |
|----------|-----------------|-----------------|--------------|--------------|
| 50 | -40 | ~-25 | 30-45s | 15-22s |
| 80 | -40 | ~-25 | 50-75s | 24-36s |
| 100 | -40 | ~-25 | 65-85s | 31-41s |
| 120 | -40 | ~-25 | 80-100s | 39-60s |

---

## 💡 Key Points

### **Stat Buffs:**
✅ **Nerf Range:** 60-70% reduction in total effectiveness  
✅ **Still Useful:** Not useless, just balanced  
✅ **No Stacking:** Must wait for expiration  
✅ **Curses Unaffected:** Weaken, Clumsy, Feeblemind unchanged  

### **Protection:**
✅ **Less Punishing:** -25 avg vs -40 resistance loss (37% better)  
✅ **Shorter Duration:** Max 60 seconds vs 100 seconds  
✅ **First Hit Safe:** Still 100% protection on first interrupt  
✅ **Riskier After:** Only 50% protection after first hit  

### **Reserved for Future:**
⏳ **Bless:** 3rd Circle reserved for separate balancing  

---

## 🔧 Technical Details

**Files Modified:** 
- `SpellHelper.cs` - Stat buff mechanics
- `Protection.cs` - Protection spell implementation  
- `Spell.cs` - Core disruption handling

**Methods Changed:** 8 methods total  
**New Classes:** 1 (ProtectionEntry)  
**Lines Added:** ~75 lines  
**Compilation:** ✅ Success, no errors  

---

## 📝 For Players

### **Stat Buffs - What You'll Notice:**
- Buffs give less stat points (about half)
- Buffs last less time (max 60 seconds)
- Can't recast until buff expires
- Message when trying to recast too soon

### **Protection - What You'll Notice:**
- Resistance penalty less severe (varies -10 to -40)
- Buff lasts less time (max 60 seconds)
- First interrupt always prevented
- Second+ interrupts only 50% prevented

---

## 🎮 Strategy Tips

### **Stat Buffs:**
- Time buffs before important fights
- Don't spam cast (wastes mana)
- Higher base stats = better buff value
- Inscribe 120 still worth having

### **Protection:**
- Still best for starting important casts
- Don't rely on it for multiple interrupts
- Resistance penalty now varies (luck factor)
- Recast more frequently (shorter duration)

---

## 📚 Full Documentation

- **Stat Buffs:** `Spell_Balance_Changes_2nd_Circle.md`
- **Protection:** `Protection_Spell_Balance_Changes.md`

---

## 🎯 Overall Impact

**Average Effectiveness Reduction:**
- Strength/Agility/Cunning: **~65% nerf**
- Protection Disruption: **~25% nerf** (after first hit)
- Protection Resistances: **~37% buff** (less penalty)

**Result:** All 2nd Circle buffs remain useful but no longer mandatory for all scenarios.


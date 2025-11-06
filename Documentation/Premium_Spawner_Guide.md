# Premium Spawner Guide
## Quick Start Guide to Spawning System

---

## What is Premium Spawner?

The Premium Spawner system is a powerful map file-based spawning solution that allows you to:
- Load thousands of spawns from text files
- Organize spawns by SpawnID
- Bulk save and remove spawns
- Create persistent spawn configurations

**Key Advantage**: Your spawns are saved in `.map` files, not just the world save. This means you can rebuild the world without losing spawns!

---

## Getting Started

### Essential Commands

```
[spawner          - Open main menu
[editor           - Spawn editor
[spawngen <file>  - Load spawns from file
[spawngen save    - Save all spawns
[add premiumspawner - Add spawner manually
```

### Your First Spawn

**Manual Method:**
```
1. [add premiumspawner Orc
   (Target ground where you want it)

2. [props
   (Target the spawner - purple sparkle)

3. Configure:
   - SpawnObject0: Orc
   - Count0: 5
   - MinDelay: 00:05:00
   - MaxDelay: 00:10:00
   - HomeRange: 15
   - WalkingRange: 30
   - Running: true

4. [spawngen savebyhand
```

**Result**: 5 Orcs that respawn every 5-10 minutes!

---

## Map File Basics

### File Format

Every spawner line in a map file looks like this:

```
*|Creature1|Creature2|Creature3|||X|Y|Z|Facet|MinTime|MaxTime|WalkRange|HomeRange|SpawnID|Count1|Count2|Count3|0|0|0
```

### Simple Example

```
## My First Spawn
*|Orc||||||1500|1600|0|2|5|10|20|15|1|5|0|0|0|0|0
```

This creates:
- 5 Orcs
- At location 1500, 1600 (Z=0)
- In Trammel (Facet 2)
- Respawn: 5-10 minutes
- Walk 20 tiles, spawn within 15

---

## Map Files

### Location

All map files go in: `Data/Monsters/`

### Creating a Map File

1. **Create file**: `Data/Monsters/myspawns.map`

2. **Add content**:
```
## My Custom Spawns
## SpawnID: 100

overrideid 100

*|Orc||||||1500|1600|0|2|5|10|20|15|100|5|0|0|0|0|0
*|Skeleton||||||1520|1620|0|2|5|10|20|15|100|3|0|0|0|0|0
```

3. **Load it**:
```
[spawngen myspawns.map
```

4. **Save it**:
```
[spawngen save
```

---

## Facet Numbers

```
0 = Felucca AND Trammel (both)
1 = Felucca only
2 = Trammel only
3 = Ilshenar
4 = Malas
5 = Tokuno
```

**Example**: Change facet `2` to `0` to spawn in both facets.

---

## Time Formats

```
5 or 5m  = 5 minutes (default)
30s      = 30 seconds
2h       = 2 hours
```

**Examples**:
```
*|Orc||||||1500|1600|0|2|5|10|20|15|1|5|0|0|0|0|0          → 5-10 minutes
*|Dragon||||||1500|1600|0|2|30s|60s|20|15|1|1|0|0|0|0|0    → 30-60 seconds
*|Boss||||||1500|1600|0|2|2h|4h|50|40|1|1|0|0|0|0|0        → 2-4 hours
```

---

## SpawnID System

### What is SpawnID?

SpawnID is a number that groups spawns together so you can:
- Load specific groups
- Remove specific groups
- Organize your spawns

### Recommended IDs

```
1      = Hand-placed spawners (default)
10-19  = Cities
20-29  = Dungeons
30-39  = Wilderness
50-99  = Events/temporary
100+   = Custom packs
```

### Using SpawnID

**In map file:**
```
overrideid 20    ← All spawners below get ID 20
```

**Remove by ID:**
```
[spawngen unload 20    ← Removes all spawners with ID 20
```

---

## Multiple Creatures

### Random Selection

Use `:` to separate creatures:

```
*|Orc:OrcBomber:OrcBrute||||||1500|1600|0|2|5|10|20|15|1|5|0|0|0|0|0
```

Spawns 5 creatures randomly from the list.

### Weighted Random

Repeat creatures for higher chance:

```
*|Orc:Orc:Orc:OrcBomber||||||1500|1600|0|2|5|10|20|15|1|5|0|0|0|0|0
```

Now spawns: 75% Orc, 25% OrcBomber

---

## Override Commands

Place at top of map file to change settings for all spawners below:

### Override SpawnID

```
overrideid 20
```

All spawners below get SpawnID 20.

### Override Facet

```
overridemap 0
```

All spawners below spawn in both facets.

### Override Time

```
overridemintime 10
overridemaxtime 20
```

All spawners below respawn in 10-20 minutes.

### Example

```
overrideid 20
overridemap 0
overridemintime 10
overridemaxtime 20

## Now all spawners below use these settings
*|Orc||||||1500|1600|0|2|5|10|20|15|1|5|0|0|0|0|0
*|Skeleton||||||1520|1620|0|2|5|10|20|15|1|3|0|0|0|0|0
```

Even though spawners say Facet 2, Time 5-10, ID 1, they use the override values!

---

## Essential Commands

### Loading Spawns

```
[spawngen spawns.map              - Load main spawn file
[spawngen myfile.map              - Load custom file
[SpawnUOML                        - Load ML expansion
```

### Saving Spawns

```
[spawngen save                    - Save ALL spawns
[spawngen savebyhand              - Save hand-placed (ID=1)
[spawngen save britain            - Save Britain region
[spawngen save 1400 1500 1600 1700 - Save coordinates
```

### Removing Spawns (CAREFUL!)

```
[spawngen unload 20               - Remove spawners with ID 20 (SAFE)
[spawngen cleanfacet              - Remove current facet
[spawngen remove                  - Remove ALL (DANGEROUS!)
```

**⚠ ALWAYS backup first!**
```
[spawngen save    ← Backup before removing!
```

---

## Spawn Editor

### Opening

```
[editor
```

### Using Editor

1. **Filter spawns**:
   - By facet
   - By region
   - By SpawnID
   - By creature name

2. **Actions**:
   - Go: Teleport to spawner
   - Props: Edit properties
   - Creatures: See what's spawned
   - Delete: Remove spawner

### Workflow

```
1. [editor
2. Filter by SpawnID: 20
3. Click spawner in list
4. Click "Go" to visit
5. Click "Props" to edit
6. Modify settings
7. Close
```

---

## Common Patterns

### Single Creature Spawn

```
*|Orc||||||1500|1600|0|2|5|10|20|15|1|5|0|0|0|0|0
```
5 Orcs

### Mixed Creatures

```
*|Orc:OrcBomber:OrcBrute||||||1500|1600|0|2|5|10|20|15|1|5|0|0|0|0|0
```
5 random orcs

### Multiple Types

```
*|Orc:OrcBrute|OrcishMage|||||1500|1600|0|2|5|10|30|20|1|5|2|0|0|0|0
```
5 melee orcs + 2 mages

### Boss

```
*|Dragon||||||5750|2150|40|1|6h|8h|100|80|777|1|0|0|0|0|0
```
1 Dragon, 6-8 hour respawn

### Guards

```
*|WarriorGuard||||||1450|1650|5|2|30|60|10|5|10|3|0|0|0|0|0
```
3 guards, 30-60 min respawn

---

## Quick Workflows

### Create New Area

```
1. [go <location>
2. [where    (note coordinates)
3. Create map file with those coords
4. [spawngen <file>.map
5. Test
```

### Replace Spawns

```
1. [spawngen save    (backup)
2. [spawngen unload <old ID>
3. [spawngen <newfile>.map
4. Test
```

### Add Boss

```
1. [go <boss location>
2. [where
3. Add line to map:
   *|BossName||||||X|Y|Z|F|6h|8h|100|80|999|1|0|0|0|0|0
4. [spawngen <file>.map
```

---

## Troubleshooting

### Spawner Not Working

```
1. [props on spawner
2. Check:
   - Running = true
   - Count > 0
   - Creature name correct
3. Toggle: Running false → true
```

### Can't Find Spawner

```
1. [editor
2. Filter by SpawnID or region
3. Click spawner
4. Click "Go"
```

### Spawns Not Loading

```
1. Check file is in Data/Monsters/
2. Check file format (starts with *)
3. Check creature names (case-sensitive)
4. Look at console for errors
```

---

## Best Practices

**DO**:
- ✅ Use SpawnIDs to organize
- ✅ Backup before removing (`[spawngen save`)
- ✅ Comment your map files
- ✅ Test spawns before deployment
- ✅ Use override commands efficiently

**DON'T**:
- ❌ Use SpawnID 0
- ❌ Mix targetable/non-targetable creatures
- ❌ Make WalkingRange huge (lags)
- ❌ Delete spawns without backup
- ❌ Forget to set Running = true

---

## Map File Template

```
## [Map Name]
## Version: 1.0
## SpawnID: [XX]
## Description: [What this contains]

overrideid [XX]

*|[Creature]||||||[X]|[Y]|[Z]|[Facet]|5|10|20|15|[XX]|[Count]|0|0|0|0|0
```

---

## Command Reference

| Command | Purpose |
|---------|---------|
| `[spawner` | Main menu |
| `[editor` | Spawn editor |
| `[spawngen <file>` | Load spawns |
| `[spawngen save` | Save all |
| `[spawngen savebyhand` | Save hand-placed |
| `[spawngen unload <ID>` | Remove by ID |
| `[spawngen cleanfacet` | Clear facet |
| `[add premiumspawner` | Add spawner |
| `[props` | Edit spawner |

---

## Quick Format Reference

```
*|List1|List2|List3|List4|List5|List6|X|Y|Z|Facet|MinTime|MaxTime|WalkRange|HomeRange|SpawnID|Count1|Count2|Count3|Count4|Count5|Count6
```

**Facets**: 0=Both, 1=Fel, 2=Tram, 3=Ilsh, 4=Malas, 5=Tokuno  
**Times**: 5m=minutes, 30s=seconds, 2h=hours  
**Creatures**: Use `:` for multiple (Orc:OrcBomber)  
**Overrides**: overrideid, overridemap, overridemintime, overridemaxtime

---

## Next Steps

**For Detailed Information**:
- See: `Premium_Spawner_System_Guide.md` (complete reference)
- See: `Quick_Reference_Spawners.md` (cheat sheet)

**For Map File Examples**:
- Check: `Data/Monsters/Spawns.map`
- Check: `Premium Spawner Tutorial (EN-rev).txt`

---

**Guide Version**: 1.0  
**For**: Ultima Adventures  
**Quick Start Focus**: Get spawning fast!

---

*Questions? See Premium_Spawner_System_Guide.md for complete documentation*


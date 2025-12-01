# Alchemy Recipe Book System

## Overview
Complete recipe system for alchemy where ALL potions now require learned recipes before crafting.

## System Components

### Created Files (7 total)
1. **AlchemyRecipeConstants.cs** - All constants (IDs, hues, gump positions)
2. **AlchemyRecipeStringConstants.cs** - PT-BR user-facing strings
3. **AlchemyRecipeData.cs** - Recipe metadata (34 potions across 8 categories)
4. **AlchemyRecipeScroll.cs** - Stackable scroll item to learn recipes
5. **AlchemyRecipeBook.cs** - Blessed book item (Necro style, hue 2003)
6. **AlchemyRecipeBookGump.cs** - Main UI with category sidebar
7. **AlchemyRecipeDetailGump.cs** - Individual recipe detail view

### Modified Files (1 total)
1. **DefAlchemy.cs** - Added recipe IDs (500-534) to all 34 potions

## Recipe Categories (Like Spellbook Circles)

| Category | Name | Potion Count | Recipe IDs |
|----------|------|--------------|------------|
| 0 | Básico | 7 | 500-506 |
| 1 | Cura e Recuperação | 5 | 507-511 |
| 2 | Combate | 4 | 512-515 |
| 3 | Venenos Avançados | 3 | 516-518 |
| 4 | Aprimoramento | 5 | 519-523 |
| 5 | Invisibilidade | 3 | 524-526 |
| 6 | Elementos | 4 | 527-530 |
| 7 | Especial | 4 | 531-534 |

**Total: 34 alchemy recipes** (500-534)

## Key Features

✅ **Blessed Book** - Never lost on death (ItemID 0x2253, Hue 2003)  
✅ **Stackable Scrolls** - Same recipe scrolls stack together  
✅ **Recipe Required** - Cannot craft without learning recipe first  
✅ **Category Organization** - Like spellbook circles  
✅ **Learned Only Display** - Book only shows learned recipes  
✅ **Skill Requirement** - Must have minimum alchemy skill to learn recipe  
✅ **Detail View** - Click recipe to see reagents and requirements  

## How to Use

### For Players

**Learning Recipes:**
1. Obtain Alchemy Recipe Scroll (drops from mobs, vendors, chests)
2. Double-click scroll
3. Must have minimum Alchemy skill for that recipe
4. Recipe learned and scroll consumed (1 from stack)

**Using Recipe Book:**
1. Double-click Tomo Alquímico
2. Select category from sidebar (only shows categories with learned recipes)
3. View recipes in selected category (only learned recipes shown)
4. Click recipe name to see details (skill, reagents, category)
5. Use alchemy craft system normally to craft learned recipes

**Crafting Potions:**
- Use mortar & pestle as normal
- Only learned recipes appear in craft menu
- Attempting unknown recipe shows error message

### For GMs/Admins

**Give Recipe Scroll:**
```
[add AlchemyRecipeScroll <recipeID>
```
Example: `[add AlchemyRecipeScroll 500` (Agility Potion)

**Give Recipe Book:**
```
[add AlchemyRecipeBook
```

**Learn All Recipes:**
```
[LearnAllRecipes
```
Then target player

**Give Specific Recipe to Player:**
Use GM console to run: `player.AcquireRecipe(recipeID)`

## Recipe ID Reference

### Category 0: Básico (Basic)
- 500: Agility Potion
- 501: Nightsight Potion  
- 502: Lesser Cure Potion
- 503: Lesser Heal Potion
- 504: Lesser Explosion Potion
- 505: Refresh Potion
- 506: Strength Potion

### Category 1: Cura e Recuperação (Healing)
- 507: Cure Potion
- 508: Greater Cure Potion
- 509: Heal Potion
- 510: Greater Heal Potion
- 511: Total Refresh Potion

### Category 2: Combate (Combat)
- 512: Explosion Potion
- 513: Greater Explosion Potion
- 514: Lesser Poison Potion
- 515: Poison Potion

### Category 3: Venenos Avançados (Advanced Poisons)
- 516: Greater Poison Potion
- 517: Deadly Poison Potion
- 518: Lethal Poison Potion

### Category 4: Aprimoramento (Enhancement)
- 519: Greater Agility Potion
- 520: Greater Strength Potion
- 521: Lesser Mana Potion
- 522: Mana Potion
- 523: Greater Mana Potion

### Category 5: Invisibilidade (Invisibility)
- 524: Lesser Invisibility Potion
- 525: Invisibility Potion
- 526: Greater Invisibility Potion

### Category 6: Elementos (Elemental)
- 527: Frostbite Potion
- 528: Greater Frostbite Potion
- 529: Conflagration Potion
- 530: Greater Conflagration Potion

### Category 7: Especial (Special)
- 531: Confusion Blast Potion
- 532: Greater Confusion Blast Potion
- 533: Hair Oil Potion
- 534: Hair Dye Potion

## Testing Checklist

- [ ] Book is blessed (doesn't drop on death)
- [ ] Scrolls are stackable (same recipe)
- [ ] Scrolls don't stack (different recipes)
- [ ] Cannot learn recipe without minimum skill
- [ ] Cannot learn recipe twice
- [ ] Book only shows learned recipes
- [ ] Categories only show if recipes learned
- [ ] Detail view shows correct information
- [ ] Cannot craft without recipe learned
- [ ] Can craft after learning recipe
- [ ] Recipe persists after server restart

## Loot Distribution TODO

To distribute recipe scrolls in the game world:

1. **Add to NPC Vendors** - Basic recipes (500-506, 507-511)
2. **Add to Dungeon Chests** - Common recipes (512-515, 519-523)
3. **Add to Boss Loot** - Rare recipes (516-518, 524-526)
4. **Add to Champion Spawns** - Very rare recipes (527-534)

## Technical Notes

- Recipe IDs stored in `PlayerMobile.m_AcquiredRecipes` dictionary
- Recipe validation happens in `CraftItem.AddRecipe()` method
- Book uses necromancer book ItemID (0x2253) with custom hue (2003)
- Gumps use existing recipe system infrastructure
- All strings are PT-BR for consistency with server

## Support

For issues or questions, reference:
- `AlchemyRecipeData.cs` - Recipe definitions
- `AlchemyRecipeConstants.cs` - All magic numbers
- `DefAlchemy.cs` - Craft integration


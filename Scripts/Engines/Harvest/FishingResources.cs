using System;
using Server;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;
using System.Collections;
using Server.Multis;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.PartySystem;
using Server.Misc;

namespace Server.Engines.Harvest
{
    public partial class Fishing : HarvestSystem
    {
        #region Mutation Table

        /// <summary>
        /// Entry structure for fishing mutations
        /// </summary>
        private struct MutateEntry
        {
            public string m_group;
            public double m_MinSkill;
            public double m_MaxSkill;
            public double m_ReqSkill;
            public bool m_DeepWater;
            public Type[] m_Types;

            public MutateEntry(string group, double minSkill, double maxSkill, double reqSkill, bool deepWater, params Type[] types)
            {
                m_group = group;
                m_MinSkill = minSkill;
                m_MaxSkill = maxSkill;
                m_ReqSkill = reqSkill;
                m_DeepWater = deepWater;
                m_Types = types;
            }
        }

        private static MutateEntry[] m_MutateTable = new MutateEntry[]
        {
            new MutateEntry("grupo1", 70.0, 70.0, 75.0, false, typeof(RustyJunk), typeof(WetClothes)), //min 0,02% - max 10%
            new MutateEntry("grupo2", 75.0, 75.0, 80, false, typeof(FishingNet), typeof(SpecialSeaweed), typeof(BlackPearl)), //min 0,02% - max 9%
            new MutateEntry("grupo3", 80.0, 80.0, 85.0, true, typeof(PrizedFish), typeof(InvisibleFish), typeof(PoisonFish), typeof(BigFish)), //min 0,02% - max 8%
            new MutateEntry("grupo4", 85.0, 85.0, 90.0, true, typeof(WondrousFish), typeof(StaminaFish), typeof(HealFish), typeof(ManaFish)), //min 0,02% - max 7%
            new MutateEntry("grupo5", 90.0, 90.0, 95.0, true, typeof(CorpseSailor), typeof(TrulyRareFish), typeof(PeculiarFish)), //min 0,02% - max 6%
            new MutateEntry("grupo6", 95.0, 95.0, 100.0, true, typeof(NewFish), typeof(PearlSkull)), //min 0,02% - max 5%
            new MutateEntry("grupo7", 100.0, 100.0, 105.0, true, typeof(SunkenBag)), //min 0,02% - max 4%
            new MutateEntry("grupo8", 105.0, 105.0, 110.0, true, typeof(SpecialFishingNet), typeof(NeptunesFishingNet), typeof(FabledFishingNet)), //min 0,2% - max 3%
            new MutateEntry("grupo9", 110.0, 110.0, 75.0, true, typeof(MessageInABottle)) // DEBUG: Lowered req skill to 75.0, max chance increased to 50%
        };

        /// <summary>
        /// Handles type mutation for fishing based on skill and location
        /// </summary>
        public override Type MutateType(Type type, Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource)
        {
            bool deepWater = false;
            PlayerMobile pm = (PlayerMobile)from;

            if (Server.Misc.Worlds.IsOnBoat(from) && !Server.Misc.Worlds.BoatToCloseToTown(from))
                deepWater = true;

            double skillBase = from.Skills[SkillName.Fishing].Base;
            double skillValue = from.Skills[SkillName.Fishing].Value;

            for (int i = 0; i < m_MutateTable.Length; ++i)
            {
                MutateEntry entry = m_MutateTable[i];

                if (!deepWater && entry.m_DeepWater)
                    continue;

                if (skillBase >= entry.m_ReqSkill)
                {
                    double chance;
                    
                    // Handle division by zero when minSkill == maxSkill
                    // Based on comments: min 0.2% - max varies by group (10%, 9%, 8%, etc.)
                    if (entry.m_MaxSkill == entry.m_MinSkill)
                    {
                        // Calculate chance based on skill above requirement
                        // Scale from 0.2% at reqSkill to max chance at 120 skill
                        // Use fixed 35-point skill range for proportional scaling (same as grupo 3)
                        // This ensures all groups scale at the same rate regardless of requirement skill
                        double skillAboveReq = Math.Max(0, skillValue - entry.m_ReqSkill);
                        double fixedSkillRange = 35.0; // Fixed range for proportional scaling across all groups
                        double effectiveMaxSkill = Math.Min(120.0, entry.m_ReqSkill + fixedSkillRange); // Cap at 120 skill
                        double effectiveSkillRange = effectiveMaxSkill - entry.m_ReqSkill;
                        
                        if (effectiveSkillRange > 0)
                        {
                            // Calculate progress: if skill is above effective max, use 1.0 (max chance)
                            double skillProgress = skillValue >= effectiveMaxSkill ? 1.0 : Math.Min(1.0, skillAboveReq / effectiveSkillRange);
                            // Max chances: grupo1=13%, grupo2=13%, grupo3=12%, grupo4=10%, grupo5=9%, grupo6=7%, grupo7=6%, grupo8=5%, grupo9=3%
                            double maxChance;
                            if (i == 0) // grupo1 - RustyJunk, WetClothes
                                maxChance = 0.13; // 13%
                            else if (i == 1) // grupo2 - FishingNet, SpecialSeaweed, BlackPearl
                                maxChance = 0.13; // 13%
                            else if (i == 2) // grupo3 - Magic Fish (PrizedFish, InvisibleFish, PoisonFish, BigFish)
                                maxChance = 0.12; // 12%
                            else if (i == 3) // grupo4 - Magic Fish (WondrousFish, StaminaFish, HealFish, ManaFish)
                                maxChance = 0.10; // 10%
                            else if (i == 4) // grupo5 - Magic Fish (CorpseSailor, TrulyRareFish, PeculiarFish)
                                maxChance = 0.09; // 9%
                            else if (i == 5) // grupo6 - NewFish, PearlSkull
                                maxChance = 0.07; // 7%
                            else if (i == 6) // grupo7 - SunkenBag
                                maxChance = 0.06; // 6%
                            else if (i == 7) // grupo8 - SpecialFishingNet, NeptunesFishingNet, FabledFishingNet
                                maxChance = 0.05; // 5%
                            else if (i == 8) // grupo9 - MessageInABottle
                                maxChance = 0.50; // DEBUG: 50% for testing (was 3%)
                            else
                                maxChance = (10.0 - i) / 100.0; // Fallback formula
                            chance = 0.002 + (skillProgress * (maxChance - 0.002));
                        }
                        else
                        {
                            // Fallback: use minimum chance
                            chance = 0.002;
                        }
                    }
                    else
                    {
                        // Normal scaling when skill range is valid
                        double skillRange = entry.m_MaxSkill - entry.m_MinSkill;
                        double skillProgress = Math.Max(0, Math.Min(1, (skillValue - entry.m_MinSkill) / skillRange));
                        
                        // Scale from 0.002 (0.2%) to appropriate max based on group
                        double maxChance;
                        if (i == 0) // grupo1 - RustyJunk, WetClothes
                            maxChance = 0.13; // 13%
                        else if (i == 1) // grupo2 - FishingNet, SpecialSeaweed, BlackPearl
                            maxChance = 0.13; // 13%
                        else if (i == 2) // grupo3 - Magic Fish (PrizedFish, InvisibleFish, PoisonFish, BigFish)
                            maxChance = 0.12; // 12%
                        else if (i == 3) // grupo4 - Magic Fish (WondrousFish, StaminaFish, HealFish, ManaFish)
                            maxChance = 0.10; // 10%
                        else if (i == 4) // grupo5 - Magic Fish (CorpseSailor, TrulyRareFish, PeculiarFish)
                            maxChance = 0.09; // 9%
                        else if (i == 5) // grupo6 - NewFish, PearlSkull
                            maxChance = 0.07; // 7%
                        else if (i == 6) // grupo7 - SunkenBag
                            maxChance = 0.06; // 6%
                        else if (i == 7) // grupo8 - SpecialFishingNet, NeptunesFishingNet, FabledFishingNet
                            maxChance = 0.05; // 5%
                        else if (i == 8) // grupo9 - MessageInABottle
                            maxChance = 0.50; // DEBUG: 50% for testing (was 3%)
                        else
                            maxChance = (10.0 - i) / 100.0; // Fallback formula
                        chance = 0.002 + (skillProgress * (maxChance - 0.002));
                    }
                    
                    double random = Utility.RandomDouble();

                    if (chance >= random)
                    {
                        pm.PlaySound(pm.Female ? 783 : 1054);
                        pm.Say("*woohoo!*");
                        from.SendMessage(55, "Que sorte! Você pescou algo inesperado e colocou em sua mochila!");
                        return entry.m_Types[Utility.Random(entry.m_Types.Length)];
                    }
                }
            }

            return type;
        }

        #endregion

        #region Resource Checking

        /// <summary>
        /// Checks for special resources like SOS messages
        /// </summary>
        public override bool CheckResources(Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, bool timed)
        {
            Container pack = from.Backpack;
            PlayerMobile pm = (PlayerMobile)from;
            if (pack != null)
            {
                List<SOS> messages = pack.FindItemsByType<SOS>();

                for (int i = 0; i < messages.Count; ++i)
                {
                    SOS sos = messages[i];

                    if (from.Map == sos.TargetMap && from.InRange(sos.TargetLocation, 60))
                    {
                        return true;
                    }
                }
            }

            return base.CheckResources(from, tool, def, map, loc, timed);
        }

        #endregion

        #region Item Construction


        /// <summary>
        /// Constructs special fishing items and treasure
        /// </summary>
        // Temporary storage for fishing location (exact tile player targeted)
        private static Dictionary<Mobile, Point3D> m_FishingLocations = new Dictionary<Mobile, Point3D>();

        /// <summary>
        /// Override FinishHarvesting to capture the exact fishing location
        /// </summary>
        public override void FinishHarvesting(Mobile from, Item tool, HarvestDefinition def, object toHarvest, object locked)
        {
            // Get the exact fishing location before base processing
            int tileID;
            Map map;
            Point3D loc;

            if (GetHarvestDetails(from, tool, toHarvest, out tileID, out map, out loc))
            {
                // Store the exact fishing location for this player
                m_FishingLocations[from] = loc;
            }

            // Call base to handle normal harvest flow
            base.FinishHarvesting(from, tool, def, toHarvest, locked);

            // Clean up the stored location after harvest completes
            if (m_FishingLocations.ContainsKey(from))
            {
                m_FishingLocations.Remove(from);
            }
        }

        public override Item Construct(Type type, Mobile from)
        {
            if (type == typeof(MessageInABottle))
            {
                return new MessageInABottle(from.Map, 0, from.Location, from.X, from.Y);
            }

            Container pack = from.Backpack;

            if (pack != null)
            {
                List<SOS> messages = pack.FindItemsByType<SOS>();

                for (int i = 0; i < messages.Count; ++i)
                {
                    SOS sos = messages[i];

                    if (from.Map == sos.TargetMap && from.InRange(sos.TargetLocation, 60))
                    {
                        // Get the exact fishing location (the tile the player targeted)
                        Point3D fishingLoc;
                        if (m_FishingLocations.ContainsKey(from))
                        {
                            fishingLoc = m_FishingLocations[from];
                        }
                        else
                        {
                            // Fallback: use player's location if not stored (shouldn't happen normally)
                            fishingLoc = from.Location;
                        }
                        
                        // Generate complete SOS treasure hunt
                        GenerateSOSTreasure(from, sos, pack, fishingLoc);
                        
                        // Delete the SOS after treasure is generated
                        sos.Delete();
                        
                        // Return a dummy item to indicate success (body parts are already in backpack from GenerateSOSTreasure)
                        // We return a simple item to satisfy the harvest system
                        return new RustyJunk();
                    }
                }
            }

            return base.Construct(type, from);
        }

        /// <summary>
        /// Generates complete SOS treasure hunt based on SOS level
        /// </summary>
        private void GenerateSOSTreasure(Mobile from, SOS sos, Container pack, Point3D fishingLocation)
        {
            PlayerMobile pm = (PlayerMobile)from;
            int nGuild = 0;

            if (pm.NpcGuild != NpcGuild.FishermensGuild)
            {
                nGuild = (int)(from.Skills[SkillName.Fishing].Value / 4);
            }

            int nChance = (int)(from.Skills[SkillName.Fishing].Value / 4) + nGuild;
            int sosLevel = sos.Level;

            // Step 1: Always start with body parts (tells player shipwreck was found)
            Item bodyParts = GenerateBodyParts(sos, from);
            if (bodyParts != null)
            {
                pack.DropItem(bodyParts);
                from.SendMessage(0x3B2, "*Você sente algo pesado na linha!* Você puxa e encontra restos do naufrágio do {0}!", sos.ShipName);
            }

            // Step 2: Generate waves based on SOS level
            int waveCount = sosLevel; // Level 1 = 1 wave, Level 2 = 2 waves, etc.

            // Determine available cases based on level
            int minCase = 2; // Paintings
            int maxCase = sosLevel >= 2 ? 7 : 5; // Level 1: cases 2-5, Level 2+: cases 2-7

            for (int wave = 1; wave <= waveCount; wave++)
            {
                int selectedCase = Utility.RandomMinMax(minCase, maxCase);
                Item waveLoot = GenerateTreasureWave(selectedCase, sos, from);

                if (waveLoot != null)
                {
                    pack.DropItem(waveLoot);
                }

                // Level 4 (Ancient): 10% chance for MessageInABottle in each wave
                if (sosLevel >= 4 && Utility.Random(10) == 0)
                {
                    int bottleLevel = Utility.RandomMinMax(1, 3); // Level 1-3, not 4
                    MessageInABottle bottle = new MessageInABottle(sos.TargetMap, bottleLevel, sos.TargetLocation, sos.TargetLocation.X, sos.TargetLocation.Y);
                    pack.DropItem(bottle);

                    // Ludic message in PT-BR
                    string[] ludicMessages = new string[]
                    {
                        "*O oceano sussurra segredos antigos!* Dentro dos destroços, você encontra uma garrafa com uma mensagem! Parece que este naufrágio foi parte de outra caça ao tesouro!",
                        "*Uma energia mística emana dos destroços!* Você descobre uma garrafa flutuando entre os restos! Alguém mais estava procurando este tesouro antes de você!",
                        "*O destino tece sua teia!* Entre os escombros, uma garrafa brilha misteriosamente! Este naufrágio guarda mais segredos do que você imaginava!",
                        "*Os espíritos do mar te guiam!* Você encontra uma garrafa antiga nos destroços! Parece que este navio estava envolvido em múltiplas aventuras!",
                        "*A sorte sorri para os corajosos!* Uma garrafa com mensagem aparece entre os restos! Este tesouro já foi procurado por outros aventureiros!"
                    };

                    string message = ludicMessages[Utility.Random(ludicMessages.Length)];
                    from.SendMessage(0x3F, message);
                }
            }

            // Step 3: Always end with WaterChest at the exact fishing location on the map
            WaterChest waterChest = new WaterChest();
            
            // Set the chest name to "Restos de {shipwreck_name}"
            if (!string.IsNullOrEmpty(sos.ShipName))
            {
                waterChest.Name = String.Format("Restos de {0}", sos.ShipName);
            }
            
            // Set the SOS level for property display
            waterChest.SOSLevel = sosLevel;
            
            Map chestMap = from.Map;
            
            // Use the exact fishing location (the tile the player targeted)
            Point3D chestLocation = fishingLocation;
            
            // Try to place at exact location first, if it can't fit, find nearby valid spot
            if (chestMap == null || !chestMap.CanFit(chestLocation.X, chestLocation.Y, chestLocation.Z, 20, false, false, true))
            {
                // Search nearby for a valid location (within 2 tiles)
                bool foundLocation = false;
                for (int offsetX = -2; offsetX <= 2 && !foundLocation; offsetX++)
                {
                    for (int offsetY = -2; offsetY <= 2 && !foundLocation; offsetY++)
                    {
                        Point3D testLoc = new Point3D(
                            fishingLocation.X + offsetX,
                            fishingLocation.Y + offsetY,
                            fishingLocation.Z
                        );
                        
                        if (chestMap != null && chestMap.CanFit(testLoc.X, testLoc.Y, testLoc.Z, 20, false, false, true))
                        {
                            chestLocation = testLoc;
                            foundLocation = true;
                        }
                    }
                }
            }
            
            // Spawn the chest at the exact fishing location (or nearest valid spot)
            waterChest.MoveToWorld(chestLocation, chestMap);

            // Step 4: Spawn monsters based on SOS level (appear together with WaterChest)
            SpawnSOSMonsters(from, sosLevel, fishingLocation, chestMap);

            // Ludic message for completing treasure hunt
            string[] completionMessages = new string[]
            {
                "*O tesouro está completo!* Você recuperou todos os itens do naufrágio do {0}! A caça ao tesouro foi um sucesso!",
                "*A aventura chega ao fim!* Todos os segredos do {0} foram descobertos! Você completou a caça ao tesouro com maestria!",
                "*A glória é sua!* O naufrágio do {0} não tem mais segredos para você! A caça ao tesouro está completa!",
                "*O oceano entrega seus tesouros!* Você recuperou tudo do {0}! A caça ao tesouro foi concluída com sucesso!",
                "*Os deuses do mar te abençoam!* Todos os itens do {0} foram encontrados! Você completou esta épica caça ao tesouro!"
            };

            string completionMessage = completionMessages[Utility.Random(completionMessages.Length)];
            from.SendMessage(0x3F, String.Format(completionMessage, sos.ShipName));
        }

        /// <summary>
        /// Generates body parts from shipwreck (always first item)
        /// </summary>
        private Item GenerateBodyParts(SOS sos, Mobile from)
        {
            PlayerMobile pm = (PlayerMobile)from;
            int nGuild = 0;

            if (pm.NpcGuild != NpcGuild.FishermensGuild)
            {
                nGuild = (int)(from.Skills[SkillName.Fishing].Value / 4);
            }

            int nChance = (int)(from.Skills[SkillName.Fishing].Value / 4) + nGuild;

            if (nChance > Utility.Random(100))
            {
                return new RustyJunk();
            }
            else
            {
                int[] list = new int[]
                {
                    0x1CDD, 0x1CE5, // arm
                    0x1CE0, 0x1CE8, // torso
                    0x1CE1, 0x1CE9, // head
                    0x1CE2, 0x1CEC, // leg
                    0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3, 0x1AE4, // skulls
                    0x1B09, 0x1B0A, 0x1B0B, 0x1B0C, 0x1B0D, 0x1B0E, 0x1B0F, 0x1B10, // bone piles
                    0x1B15, 0x1B16 // pelvis bones
                };

                Item bodyParts = new ShipwreckedItem(Utility.RandomList(list), sos.ShipName);
                bodyParts.Hue = Utility.RandomList(0xB97, 0xB98, 0xB99, 0xB9A, 0xB88);
                return bodyParts;
            }
        }

        /// <summary>
        /// Generates a single treasure wave item
        /// </summary>
        /// <summary>
        /// Generates a single treasure wave item (always ShipwreckedItem variants, no DDRelics)
        /// DDRelics are now handled exclusively in WaterChest loot generation
        /// </summary>
        private Item GenerateTreasureWave(int waveCase, SOS sos, Mobile from)
        {
            Item preLoot = null;

            switch (waveCase)
            {
                case 2: // Paintings and portraits
                    {
                        if (Utility.Random(2) == 1) 
                        {
                            preLoot = new ShipwreckedItem(Utility.Random(0xE9F, 10), sos.ShipName);
                        }
                        else
                        {
                            preLoot = new ShipwreckedItem(SeaArtifactConstants.LIGHTHOUSE_DEED_ITEM_ID, sos.ShipName);
                        }
                        break;
                    }
                case 3: // Misc
                    {
                        if (Utility.Random(10) == 0) // (10% chance)
                        {
                            if (Utility.Random(2) == 1) // 50% chance 
                            {
                                preLoot = new ScrapIronBarrel();
                            }
                            else
                            { // msg SOS vazia
                                preLoot = new ShipwreckedItem(Utility.RandomList(0x12AD), sos.ShipName);
                            }
                        }
                        else
                        {
                            preLoot = new ShipwreckedItem(Utility.Random(0x13A4, 11), sos.ShipName);
                            preLoot.Hue = RandomThings.GetRandomColor(0);
                        }
                        break;
                    }
                case 4: // Shells
                    {
                        preLoot = new ShipwreckedItem(Utility.Random(0xFC4, 9), sos.ShipName);
                        break;
                    }
                case 5: // Hats
                    {
                        if (Utility.RandomBool())
                        {
                            preLoot = new SkullCap();
                            if (Utility.Random(4) == 1)
                            {
                                preLoot.Hue = Utility.RandomList(0xB97, 0xB98, 0xB99, 0xB9A, 0xB88);
                                preLoot.Name = "barrete ensopado";
                            }
                            else
                            {
                                preLoot.Hue = RandomThings.GetRandomColor(0);
                                preLoot.Name = "barrete";
                            }
                        }
                        else
                        {
                            preLoot = new TricorneHat();
                            if (Utility.Random(4) == 1)
                            {
                                preLoot.Hue = Utility.RandomList(0xB97, 0xB98, 0xB99, 0xB9A, 0xB88);
                                preLoot.Name = "tricórnio ensopado";
                            }
                            else
                            {
                                preLoot.Hue = RandomThings.GetRandomColor(0);
                                preLoot.Name = "tricórnio";
                            }
                        }
                        break;
                    }
                case 6: // Armor
                    {
                        preLoot = new ShipwreckedItem(Utility.Random(0x13BB, 10), sos.ShipName);
                        break;
                    }
                case 7: // Weapons
                    {
                        preLoot = new ShipwreckedItem(Utility.Random(0x13B4, 23), sos.ShipName);
                        break;
                    }
                default:
                    {
                        preLoot = new ShipwreckedItem(Utility.Random(0x13A4, 11), sos.ShipName);
                        preLoot.Hue = RandomThings.GetRandomColor(0);
                        break;
                    }
            }

            return preLoot;
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Safely gets a map, defaulting to Trammel if the map is invalid
        /// </summary>
        private static Map SafeMap(Map map)
        {
            if (map == null || map == Map.Internal)
                return Map.Trammel;

            return map;
        }

        /// <summary>
        /// Finds a valid water location near the target location
        /// </summary>
        private Point3D FindValidWaterLocation(Map map, Point3D targetLocation, Point3D playerLocation)
        {
            // Try player's location first (they're fishing from there)
            if (map != null && map.CanFit(playerLocation.X, playerLocation.Y, playerLocation.Z, 20, false, false, true))
            {
                return playerLocation;
            }
            
            // Try target location
            if (map != null && map.CanFit(targetLocation.X, targetLocation.Y, targetLocation.Z, 20, false, false, true))
            {
                return targetLocation;
            }
            
            // Search nearby
            for (int offsetX = -3; offsetX <= 3; offsetX++)
            {
                for (int offsetY = -3; offsetY <= 3; offsetY++)
                {
                    Point3D testLoc = new Point3D(
                        targetLocation.X + offsetX,
                        targetLocation.Y + offsetY,
                        targetLocation.Z
                    );
                    
                    if (map != null && map.CanFit(testLoc.X, testLoc.Y, testLoc.Z, 20, false, false, true))
                    {
                        return testLoc;
                    }
                }
            }
            
            // Fallback to target location
            return targetLocation;
        }

        /// <summary>
        /// Spawns monsters based on SOS level when shipwreck is found
        /// Monsters appear together with the WaterChest
        /// </summary>
        private void SpawnSOSMonsters(Mobile from, int sosLevel, Point3D spawnLocation, Map map)
        {
            if (map == null || from == null)
                return;

            // Determine monster count and types based on SOS level
            int monsterCount = 0;
            Type[] monsterTypes = null;

            switch (sosLevel)
            {
                case 1: // Easy - Basic sea creatures
                    monsterCount = Utility.RandomMinMax(1, 2);
                    monsterTypes = new Type[]
                    {
                        typeof(Shark),
                        typeof(SeaSerpent),
                        typeof(WaterWeird)
                    };
                    break;

                case 2: // Medium - Stronger sea creatures
                    monsterCount = Utility.RandomMinMax(2, 3);
                    monsterTypes = new Type[]
                    {
                        typeof(GiantEel),
                        typeof(SeaSerpent),
                        typeof(WaterWeird),
                        typeof(SeaweedElemental),
                        typeof(GreatWhite)
                    };
                    break;

                case 3: // Hard - Dangerous sea creatures
                    monsterCount = Utility.RandomMinMax(2, 4);
                    monsterTypes = new Type[]
                    {
                        typeof(SeaDrake),
                        typeof(GiantSquid),
                        typeof(EyeOfTheDeep),
                        typeof(GreatWhite),
                        typeof(SteamElemental),
                        typeof(DeepSeaSerpent)
                    };
                    break;

                case 4: // Ancient - Very dangerous sea creatures
                    monsterCount = Utility.RandomMinMax(3, 5);
                    monsterTypes = new Type[]
                    {
                        typeof(Kraken),
                        typeof(Typhoon),
                        typeof(Megalodon),
                        typeof(Calamari),
                        typeof(EyeOfTheDeep),
                        typeof(SeaDrake),
                        typeof(GiantSquid)
                    };
                    break;

                default:
                    // Fallback for invalid levels
                    monsterCount = 1;
                    monsterTypes = new Type[] { typeof(Shark) };
                    break;
            }

            // Spawn monsters around the fishing location
            for (int i = 0; i < monsterCount; i++)
            {
                try
                {
                    // Select random monster type from the level's pool
                    Type monsterType = monsterTypes[Utility.Random(monsterTypes.Length)];
                    BaseCreature monster = Activator.CreateInstance(monsterType) as BaseCreature;

                    if (monster != null)
                    {
                        // Find a valid spawn location near the fishing spot (water tile)
                        Point3D monsterLocation = FindMonsterSpawnLocation(spawnLocation, map);

                        monster.OnBeforeSpawn(monsterLocation, map);
                        monster.MoveToWorld(monsterLocation, map);
                        monster.OnAfterSpawn();

                        // Set monster to attack the player
                        monster.Combatant = from;

                        // Mark for cleanup (so task manager can delete them eventually)
                        monster.WhisperHue = 999;
                    }
                }
                catch
                {
                    // Skip invalid monster types
                    continue;
                }
            }

            // Send warning message to player
            if (monsterCount > 0)
            {
                string[] warningMessages = new string[]
                {
                    "*Algo se agita nas profundezas!* Criaturas do mar emergem dos destroços!",
                    "*O oceano se revolta!* Monstros marinhos aparecem para proteger o tesouro!",
                    "*Perigo nas águas!* As criaturas do naufrágio despertam!",
                    "*As profundezas sussurram!* Guardiões do mar emergem dos destroços!",
                    "*O tesouro tem guardiões!* Monstros marinhos aparecem para defender o naufrágio!"
                };

                string warning = warningMessages[Utility.Random(warningMessages.Length)];
                from.SendMessage(0x3B2, warning);
            }
        }

        /// <summary>
        /// Finds a valid water location for spawning monsters near the fishing spot
        /// </summary>
        private Point3D FindMonsterSpawnLocation(Point3D center, Map map)
        {
            if (map == null)
                return center;

            // Try to find a water tile within 3-5 tiles of the center
            for (int radius = 3; radius <= 5; radius++)
            {
                for (int attempts = 0; attempts < 20; attempts++)
                {
                    int offsetX = center.X + Utility.RandomMinMax(-radius, radius);
                    int offsetY = center.Y + Utility.RandomMinMax(-radius, radius);
                    int z = center.Z;

                    Point3D testLoc = new Point3D(offsetX, offsetY, z);

                    // Check if it's a water tile and can fit a monster
                    LandTile tile = map.Tiles.GetLandTile(offsetX, offsetY);
                    if (Worlds.IsWaterTile(tile.ID, 0))
                    {
                        if (map.CanFit(offsetX, offsetY, z, 16, false, false, true))
                        {
                            return testLoc;
                        }
                    }
                }
            }

            // Fallback: return center location
            return center;
        }

        #endregion
    }
}

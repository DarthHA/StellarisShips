
using StellarisShips.System;
using StellarisShips.System.BaseType;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;

namespace StellarisShips.Static
{
    public static class LeaderHelper
    {
        public const int MaxCost = 3;
        public static List<string> GetLeaderModifier(this NPC npc)
        {
            List<string> result = new();
            //30%几率生成4-1，70几率生成3+0
            int currentCost = MaxCost;
            if (npc.type != NPCID.Princess && !IsPet(npc) && Main.rand.NextFloat() < 0.3f)
            {
                string negative = ModifierID.Negative[Main.rand.Next(ModifierID.Negative.Count)];
                result.Add(negative);
                currentCost += EverythingLibrary.Modifiers[negative].Cost;
            }
            switch (npc.type)
            {
                case NPCID.Guide:
                    result.Add(ModifierID.GuideInsight);
                    currentCost++;
                    break;
                case NPCID.Angler:
                    result.Add(ModifierID.DisgustingAngler);
                    break;
                case NPCID.Dryad:
                    result.Add(ModifierID.EvilCleaner);
                    currentCost--;
                    break;
                case NPCID.DD2Bartender:
                    result.Add(ModifierID.TavernKeeperInsight);
                    break;
                case 54:
                    result.Add(ModifierID.SkeletronCurse);
                    break;
                case NPCID.Pirate:
                    result.Add(ModifierID.HellHeart);
                    break;
                case NPCID.Cyborg:
                    result.Add(ModifierID.Cyborg);
                    break;
                case NPCID.Princess:
                    currentCost += 3;
                    result.Add(ModifierID.GreatPrincess);
                    break;
                case NPCID.Wizard:
                    result.Add(ModifierID.MagicShield);
                    break;
                case NPCID.Truffle:
                    result.Add(ModifierID.MushroomMind);
                    break;
                default:
                    if (IsPet(npc))
                    {
                        result.Add(ModifierID.Cute);
                        currentCost = 0;
                    }
                    if (IsSlime(npc))
                    {
                        currentCost--;
                        result.Add(ModifierID.Slimy);
                    }
                    break;
            }

            WeightedRandom<string> random = new();
            random.random.SetSeed(Environment.TickCount + npc.type);
            foreach (string trait in ModifierID.Common)
            {
                if (EverythingLibrary.Modifiers[trait].Cost == 2)
                {
                    random.Add(trait, 1);
                }
                else
                {
                    random.Add(trait, 1);
                }
            }
            foreach (string trait in ModifierID.Veteran)
            {
                if (EverythingLibrary.Modifiers[trait].Cost == 2)
                {
                    random.Add(trait, 1);
                }
                else
                {
                    random.Add(trait, 1);
                }
            }
            do
            {
                string r = random.Get();
                if (CanAddTrait(r, result) && currentCost >= EverythingLibrary.Modifiers[r].Cost)
                {
                    result.Add(r);
                    currentCost -= EverythingLibrary.Modifiers[r].Cost;
                }
            } while (currentCost > 0);

            for (int i = result.Count - 1; i >= 0; i--)
            {
                if (EverythingLibrary.Modifiers[result[i]].Negative)
                {
                    string tmp = result[i];
                    result.RemoveAt(i);
                    result.Add(tmp);
                }
            }

            return result;
        }

        public static bool CanAddTrait(string name,List<string> current)
        {
            if (current.Contains(name)) return false;
            List<string> conflict = new();
            foreach (string item in current)
            {
                conflict.AddRange(EverythingLibrary.Modifiers[item].ConflictModifiers);
            }
            if (conflict.Contains(name)) return false;
            return true;
        }

        public static bool IsSlime(NPC npc)
        {
            return NPCID.Sets.IsTownSlime[npc.type];
        }
        public static bool IsPet(NPC npc)
        {
            return NPCID.Sets.IsTownPet[npc.type] && !NPCID.Sets.IsTownSlime[npc.type];
        }
    }
}

using StellarisShips.Static;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace StellarisShips.System
{
    public class TraitSystem : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public List<string> Traits = new();

        public override void PostAI(NPC npc)
        {
            if (npc.CountAsTownNPC())
            {
                if (Traits.Count == 0)
                {
                    Traits = LeaderHelper.GetLeaderModifier(npc);
                }
            }
        }

        public override void SaveData(NPC npc, TagCompound tag)
        {
            if (npc.CountAsTownNPC())
            {
                tag.Add("Traits", Traits);
            }
        }

        public override void LoadData(NPC npc, TagCompound tag)
        {
            if (npc.CountAsTownNPC())
            {
                Traits = tag.Get<List<string>>("Traits");
            }
        }
    }
}

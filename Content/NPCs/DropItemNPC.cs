﻿using StellarisShips.Content.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using StellarisShips.System;
using Terraria.Localization;

namespace StellarisShips.Content.NPCs
{
    public class DropItemNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ConnecterItem>(), 1, 1, 1));
            }
            if (npc.boss && npc.lifeMax > 60000)
            {
                LeadingConditionRule rule =
                    new(new DropRubricator());
                rule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Rubricator>(), 20));
                npcLoot.Add(rule);
            }
        }

        public class DropRubricator : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) 
            {
                return !info.IsInSimulation && ProgressHelper.DiscoveredMR == 0 && NPC.downedMoonlord;
            }
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => Language.GetTextValue("Mods.StellarisShips.DropRule.Rubricator");
        }
    }
}

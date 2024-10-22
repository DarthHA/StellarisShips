using StellarisShips.Content.Buffs;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using StellarisShips.UI;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.System
{
    public class ShipControlPlayer : ModPlayer
    {
        public string CurrentShroudBuffs = "";

        public override void PreUpdateBuffs()
        {
            if (!Player.HasBuff(ModContent.BuffType<ShroudBuff>()))
            {
                CurrentShroudBuffs = "";
            }

            if (NPC.CountNPCS(ModContent.NPCType<ShipNPC>()) > 0)
            {
                Player.AddBuff(ModContent.BuffType<FollowBuff>(), 2);
                Player.AddBuff(ModContent.BuffType<PassiveBuff>(), 2);
                Player.AddBuff(ModContent.BuffType<CommanderEditorBuff>(), 2);
            }
            else
            {
                LeaderUI.Close();
            }
            int TimerMax = 0;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<ShipNPC>() && npc.GetShipNPC().shipAI == ShipAI.Missing)
                {
                    if (npc.GetShipNPC().MissingTimer > TimerMax) TimerMax = npc.GetShipNPC().MissingTimer;
                }
            }
            if (TimerMax > 0)
            {
                if (!Player.HasBuff(ModContent.BuffType<MissingBuff>()))
                {
                    SomeUtils.PlaySound(SoundPath.UI + "Notification");
                }
                Player.AddBuff(ModContent.BuffType<MissingBuff>(), TimerMax);
            }
            else
            {
                Player.ClearBuff(ModContent.BuffType<MissingBuff>());
            }

        }
    }


}



using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.Content.Buffs
{
    public class MissingBuff : ModBuff
    {

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            drawParams.DrawColor = Color.White;
            return true;
        }



        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            rare = ItemRarityID.Orange;
            int TimerMax = 0;
            int count = 0;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<ShipNPC>() && npc.GetShipNPC().shipAI == ShipAI.Missing)
                {
                    count++;
                    if (npc.GetShipNPC().MissingTimer > TimerMax) TimerMax = npc.GetShipNPC().MissingTimer;
                }
            }
            tip = string.Format(Language.GetTextValue("Mods.StellarisShips.BuffExtraDesc.MissingDesc"), count, GenTimeSpanFromSeconds(TimerMax / 60));
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] = time;
            return true;
        }

        public static string GenTimeSpanFromSeconds(int seconds)
        {
            TimeSpan interval = TimeSpan.FromSeconds(seconds);
            string timeInterval = interval.ToString();
            return timeInterval;
        }
    }
}


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using StellarisShips.System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Buffs
{
    public class BattleStatBuff : ModBuff
    {
        public override string Texture => "StellarisShips/Content/Buffs/BattleStatBuff";

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            drawParams.DrawColor = Color.White;
            return true;
        }


        public override bool RightClick(int buffIndex)
        {
            SomeUtils.PlaySound(SoundPath.UI + "Close");
            return true;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            rare = ItemRarityID.Cyan;
            tip = BattleStatSystem.StatText;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (BattleStatSystem.StatText == "")
            {
                player.ClearBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
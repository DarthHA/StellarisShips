

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using StellarisShips.System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.Content.Buffs
{
    public class ShroudBuff : ModBuff
    {
        public override string Texture => "StellarisShips/Content/Buffs/ShroudBuff";

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
            rare = ItemRarityID.Purple;
            if (Main.LocalPlayer.GetModPlayer<ShipControlPlayer>().CurrentShroudBuffs != "")
            {
                string name = Main.LocalPlayer.GetModPlayer<ShipControlPlayer>().CurrentShroudBuffs;
                buffName = Language.GetTextValue("Mods.StellarisShips.BuffExtraDesc." + name + "Name");
                int time = 0;
                if (Main.LocalPlayer.FindBuffIndex(Type) != -1)
                {
                    time = Main.LocalPlayer.buffTime[Main.LocalPlayer.FindBuffIndex(Type)] / 60;
                }
                tip = string.Format(Language.GetTextValue("Mods.StellarisShips.BuffExtraDesc.ShroudBuffDesc"), Language.GetTextValue("Mods.StellarisShips.BuffExtraDesc." + name + "Desc"), time);
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<ShipControlPlayer>().CurrentShroudBuffs == "")
            {
                player.ClearBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
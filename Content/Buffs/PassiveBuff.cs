

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Static;
using StellarisShips.System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.Content.Buffs
{
    public class PassiveBuff : ModBuff
    {
        public override string Texture => "StellarisShips/Content/Buffs/Passive1";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;

        }

        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            drawParams.DrawColor = Color.White;
            if (drawParams.MouseRectangle.Contains(new Point(Main.mouseX, Main.mouseY)))
            {
                drawParams.DrawColor = Color.Orange;
            }

            if (!ShapeSystem.Passive)
            {
                drawParams.Texture = ModContent.Request<Texture2D>("StellarisShips/Content/Buffs/Passive1", AssetRequestMode.ImmediateLoad).Value;
            }
            else
            {
                drawParams.Texture = ModContent.Request<Texture2D>("StellarisShips/Content/Buffs/Passive2", AssetRequestMode.ImmediateLoad).Value;
            }
            return true;
        }

        public override bool RightClick(int buffIndex)
        {
            ShapeSystem.Passive = !ShapeSystem.Passive;
            SomeUtils.PlaySound(SoundPath.UI + "Click");
            return false;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            if (!ShapeSystem.Passive)
            {
                rare = ItemRarityID.Red;
                buffName = Language.GetTextValue("Mods.StellarisShips.BuffExtraDesc.Passive1Name");
                tip = Language.GetTextValue("Mods.StellarisShips.BuffExtraDesc.Passive1Desc");
            }
            else
            {
                rare = ItemRarityID.Green;
                buffName = Language.GetTextValue("Mods.StellarisShips.BuffExtraDesc.Passive2Name");
                tip = Language.GetTextValue("Mods.StellarisShips.BuffExtraDesc.Passive2Desc");
            }
        }
    }
}
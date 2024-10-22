using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using StellarisShips.UI;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.Content.Buffs
{
    public class CommanderEditorBuff : ModBuff
    {
        public override string Texture => "StellarisShips/Content/Buffs/CommanderEditorBuff1";

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
            if (UIManager.LeaderUIVisible)
            {
                drawParams.Texture = ModContent.Request<Texture2D>("StellarisShips/Content/Buffs/CommanderEditorBuff2").Value;
            }

            return true;
        }

        public override bool RightClick(int buffIndex)
        {
            if (UIManager.LeaderUIVisible)
            {
                LeaderUI.Close();
            }
            else if (!UIManager.AnyUIVisible())
            {
                LeaderUI.Open();
            }

            SomeUtils.PlaySound(SoundPath.UI + "Click");
            return false;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            rare = ItemRarityID.Orange;
            if (!UIManager.LeaderUIVisible)
            {
                tip = Language.GetTextValue("Mods.StellarisShips.BuffExtraDesc.LeaderEditorDesc1");
            }
            else
            {
                tip = Language.GetTextValue("Mods.StellarisShips.BuffExtraDesc.LeaderEditorDesc2");
            }
        }
    }
}

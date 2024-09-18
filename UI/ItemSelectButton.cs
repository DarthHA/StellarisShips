using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Content.Items;
using StellarisShips.Static;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.UI
{
    public class ItemSelectButton  //300x60
    {
        public Vector2 Pos;
        public Vector2 Size = new(300, 60);
        public Texture2D IconTex = null;
        public string InternalText;
        public string ShowText;
        public string AppendText = "";

        public string DescText = "";

        private bool HasHovered = false;
        public ItemSelectButton()
        {

        }
        public ItemSelectButton(Vector2 pos, Texture2D iconTex, string internalText, string showText, string descText = "")
        {
            Pos = pos;
            IconTex = iconTex;
            InternalText = internalText;
            ShowText = showText;
            DescText = descText;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.Request<Texture2D>("StellarisShips/Images/UI/ItemSelectButton", AssetRequestMode.ImmediateLoad).Value;
            if (Hovered())
            {
                if (!HasHovered)
                {
                    SomeUtils.PlaySound(SoundPath.UI + "MouseOver");
                }
                HasHovered = true;
                tex = ModContent.Request<Texture2D>("StellarisShips/Images/UI/ItemSelectButton_Hover", AssetRequestMode.ImmediateLoad).Value;
            }
            else
            {
                HasHovered = false;
            }
            spriteBatch.Draw(tex, new Rectangle((int)Pos.X, (int)Pos.Y, (int)Size.X, (int)Size.Y), Color.White * 0.5f);
            if (IconTex != null)
            {
                EasyDraw.AnotherDraw(BlendState.NonPremultiplied);
                spriteBatch.Draw(IconTex, new Rectangle((int)Pos.X + 4, (int)Pos.Y + 4, (int)Size.Y - 8, (int)Size.Y - 8), Color.White);
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            }

            string IconText = AppendText;
            IconText = IconText.Replace("S", "[i:" + ModContent.ItemType<Slot_S>().ToString() + "]");
            IconText = IconText.Replace("M", "[i:" + ModContent.ItemType<Slot_M>().ToString() + "]");
            IconText = IconText.Replace("L", "[i:" + ModContent.ItemType<Slot_L>().ToString() + "]");
            IconText = IconText.Replace("X", "[i:" + ModContent.ItemType<Slot_X>().ToString() + "]");
            IconText = IconText.Replace("G", "[i:" + ModContent.ItemType<Slot_G>().ToString() + "]");
            IconText = IconText.Replace("H", "[i:" + ModContent.ItemType<Slot_H>().ToString() + "]");
            IconText = IconText.Replace("P", "[i:" + ModContent.ItemType<Slot_P>().ToString() + "]");
            IconText = IconText.Replace("T", "[i:" + ModContent.ItemType<Slot_T>().ToString() + "]");
            IconText = IconText.Replace("A", "[i:" + ModContent.ItemType<Slot_A>().ToString() + "]");
            IconText = IconText.Replace("B", "[i:" + ModContent.ItemType<Slot_S>().ToString() + "]");
            IconText = IconText.Replace("C", "[i:" + ModContent.ItemType<Slot_M>().ToString() + "]");
            IconText = IconText.Replace("D", "[i:" + ModContent.ItemType<Slot_L>().ToString() + "]");
            IconText = IconText.Replace("E", "");
            IconText = IconText.Replace("F", "");
            IconText = IconText.Replace("I", "");
            IconText = IconText.Replace("J", "");
            IconText = IconText.Replace("K", "");

            Utils.DrawBorderString(spriteBatch, ShowText + " " + IconText, new Vector2(Pos.X + 70, Pos.Y + Size.Y / 2), Color.White, 1, 0, 0.5f);
        }

        public bool Hovered()
        {
            Rectangle rectangle = new((int)Pos.X, (int)Pos.Y, (int)Size.X, (int)Size.Y);
            if (rectangle.Contains(new Point(Main.mouseX, Main.mouseY)))
            {
                return true;
            }
            return false;
        }

        public bool Clicked()
        {
            if (Hovered() && UIManager.LeftClicked)
            {
                return true;
            }
            return false;
        }
    }
}

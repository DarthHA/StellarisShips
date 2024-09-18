using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Static;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.UI
{
    public class YesButton
    {
        public Vector2 Pos;
        public Vector2 Size = new(120, 40);
        public string ShowText;
        public bool Enabled = true;

        public bool HasHovered = false;
        public YesButton(Vector2 pos, string showText)
        {
            Pos = pos;
            ShowText = showText;
        }

        public YesButton()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.Request<Texture2D>("StellarisShips/Images/UI/YesButton", AssetRequestMode.ImmediateLoad).Value;
            Color color = Color.White;
            if (!Enabled)
            {
                color = Color.DarkGray;
            }
            else if (Hovered())
            {
                if (!HasHovered)
                {
                    SomeUtils.PlaySound(SoundPath.UI + "MouseOver");
                }
                HasHovered = true;
                tex = ModContent.Request<Texture2D>("StellarisShips/Images/UI/YesButton_Hover", AssetRequestMode.ImmediateLoad).Value;
            }
            else
            {
                HasHovered = false;
            }

            spriteBatch.Draw(tex, new Rectangle((int)Pos.X, (int)Pos.Y, (int)Size.X, (int)Size.Y), color);
            Utils.DrawBorderString(spriteBatch, ShowText, new Vector2(Pos.X + Size.X / 2, Pos.Y + Size.Y / 2), color, 1, 0.5f, 0.5f);
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
            if (!Enabled) return false;
            if (Hovered() && UIManager.LeftClicked)
            {
                return true;
            }
            return false;
        }
    }
}

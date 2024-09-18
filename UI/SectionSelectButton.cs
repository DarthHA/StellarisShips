using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Static;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.UI
{
    public class SectionSelectButton  //180x40
    {
        public Vector2 Pos;
        public Vector2 Size = new(180, 40);
        public string InternalText;
        public string ShowText;
        public bool Selected = false;

        public bool HasHovered = false;
        public SectionSelectButton()
        {

        }
        public SectionSelectButton(Vector2 pos, string internalText, string showText)
        {
            Pos = pos;
            InternalText = internalText;
            ShowText = showText;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.Request<Texture2D>("StellarisShips/Images/UI/ItemSelectButton", AssetRequestMode.ImmediateLoad).Value;
            if (Hovered() || Selected)
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
            spriteBatch.Draw(tex, new Rectangle((int)Pos.X, (int)Pos.Y, (int)Size.X, (int)Size.Y), Color.White);
            Utils.DrawBorderString(spriteBatch, ShowText, new Vector2(Pos.X + Size.X / 2, Pos.Y + Size.Y / 2), Color.White, 1, 0.5f, 0.5f);
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

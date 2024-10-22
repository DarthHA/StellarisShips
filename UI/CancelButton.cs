

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.UI
{
    public class CancelButton
    {
        public Vector2 Center;
        public Vector2 Size = new(40, 40);

        private bool HasHovered = false;
        public CancelButton()
        {

        }
        public CancelButton(Vector2 center)
        {
            Center = center;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.Request<Texture2D>("StellarisShips/Images/UI/Cross").Value;
            float scale = 0.7f;
            if (Hovered())
            {
                scale = 0.8f;
                if (!HasHovered)
                {
                    HasHovered = true;
                    SomeUtils.PlaySound(SoundPath.UI + "MouseOver");
                }
            }
            else
            {
                HasHovered = false;
            }
            spriteBatch.Draw(tex, Center, null, Color.White, 0, tex.Size() / 2f, scale, SpriteEffects.None, 0);
        }

        public bool Hovered()
        {
            Rectangle rectangle = new((int)Center.X - (int)(Size.X / 2), (int)Center.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
            if (rectangle.Contains(new Point(Main.mouseX, Main.mouseY)))
            {
                return true;
            }
            return false;
        }

        public bool LeftClicked()
        {
            if (Hovered() && UIManager.LeftClicked)
            {
                return true;
            }
            return false;
        }

    }

}

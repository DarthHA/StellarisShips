using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Static;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.UI
{
    public class TalkButton  //300x60
    {
        public Vector2 Pos;
        public Vector2 Size = new(450, 35);
        public string InternalText = "";
        public string ShowText = "";
        public string DescText = "";

        public bool Enabled = true;

        private bool HasHovered = false;
        public TalkButton()
        {

        }
        public TalkButton(Vector2 pos, string internalText, string showText, string descText = "")
        {
            Pos = pos;
            InternalText = internalText;
            ShowText = showText;
            DescText = descText;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.Request<Texture2D>("StellarisShips/Images/UI/DialogButton", AssetRequestMode.ImmediateLoad).Value;
            Color buttonColor = Color.White;
            if (!Enabled)
            {
                buttonColor = Color.DarkGray;
            }
            else if (Hovered())
            {
                if (!HasHovered)
                {
                    SomeUtils.PlaySound(SoundPath.UI + "MouseOver");
                }
                HasHovered = true;
                tex = ModContent.Request<Texture2D>("StellarisShips/Images/UI/DialogButton_Hover", AssetRequestMode.ImmediateLoad).Value;
            }
            else
            {
                HasHovered = false;
            }
            spriteBatch.Draw(tex, new Rectangle((int)Pos.X, (int)Pos.Y, (int)Size.X, (int)Size.Y), buttonColor * 0.5f);
            string LocalizeText = Language.GetTextValue("Mods.StellarisShips.DialogOption." + ShowText);
            Utils.DrawBorderString(spriteBatch, LocalizeText, new Vector2(Pos.X + 10, Pos.Y + 5), buttonColor, 1, 0, 0f);
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

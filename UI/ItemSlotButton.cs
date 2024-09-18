using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Static;
using StellarisShips.System;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.UI
{
    public class ItemSlotButton //60x60
    {
        public Vector2 Pos;
        public Vector2 Size = new(60, 60);
        public string EquipType = "";
        public string ItemType = "";
        public bool Selected = false;

        public bool HasHovered = false;

        public ItemSlotButton()
        {

        }
        public ItemSlotButton(Vector2 pos, string equipType, string itemType)
        {
            Pos = pos;
            EquipType = equipType;
            ItemType = itemType;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.Request<Texture2D>("StellarisShips/Images/UI/ItemSlotButton", AssetRequestMode.ImmediateLoad).Value;
            if (Hovered() || Selected)
            {
                if (!HasHovered)
                {
                    SomeUtils.PlaySound(SoundPath.UI + "MouseOver");
                }
                HasHovered = true;
                tex = ModContent.Request<Texture2D>("StellarisShips/Images/UI/ItemSlotButton_Hover", AssetRequestMode.ImmediateLoad).Value;
            }
            else
            {
                HasHovered = false;
            }
            spriteBatch.Draw(tex, new Rectangle((int)Pos.X, (int)Pos.Y, (int)Size.X, (int)Size.Y), Color.White * 0.5f);
            if (ItemType != "")
            {
                Texture2D ItemTex = EverythingLibrary.Components[ItemType].GetIcon();
                EasyDraw.AnotherDraw(BlendState.NonPremultiplied);
                spriteBatch.Draw(ItemTex, new Rectangle((int)Pos.X + 4, (int)Pos.Y + 4, (int)Size.X - 8, (int)Size.Y - 8), Color.White);
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            }
            if (ShouldShowSlotType(EquipType))
            {
                Texture2D Outer = ModContent.Request<Texture2D>("StellarisShips/Images/UI/ItemSlotButton_" + EquipType, AssetRequestMode.ImmediateLoad).Value;
                spriteBatch.Draw(Outer, new Rectangle((int)Pos.X, (int)Pos.Y, (int)Size.X, (int)Size.Y), Color.White);
            }
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

        public bool RightClicked()
        {
            if (Hovered() && UIManager.RightClicked)
            {
                return true;
            }
            return false;
        }

        private static bool ShouldShowSlotType(string e)
        {
            switch (e)
            {
                case ComponentTypes.Weapon_S:
                case ComponentTypes.Weapon_M:
                case ComponentTypes.Weapon_L:
                case ComponentTypes.Weapon_X:
                case ComponentTypes.Weapon_P:
                case ComponentTypes.Weapon_G:
                case ComponentTypes.Weapon_H:
                case ComponentTypes.Defense_S:
                case ComponentTypes.Defense_M:
                case ComponentTypes.Defense_L:
                case ComponentTypes.Accessory:
                    return true;
                default:
                    break;
            }
            return false;
        }
    }
}

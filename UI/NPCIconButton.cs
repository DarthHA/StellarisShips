using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.Buffs;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using StellarisShips.System;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.UI
{
    public class NPCIconButton  //40x40
    {
        public Vector2 Center;
        public Vector2 Size = new(40, 40);
        public int NPCHeadType = -1;
        public string NPCName = "";
        public int NPCWhoAmI = -1;
        public string ShipName = "";
        public int ShipWhoAmI = -1;

        public bool Enabled = true;
        public bool Selected = false;

        private bool HasHovered = false;
        public NPCIconButton()
        {

        }
        public NPCIconButton(Vector2 center, int npcWMI, int shipWMI)
        {
            Center = center;
            NPCWhoAmI = npcWMI;
            if (npcWMI != -1)
            {
                NPCHeadType = TownNPCProfiles.GetHeadIndexSafe(Main.npc[npcWMI]);
                NPCName = Main.npc[npcWMI].GivenOrTypeName;
            }
            else
            {
                NPCHeadType = -1;
                NPCName = "";
            }

            ShipWhoAmI = shipWMI;
            if (shipWMI != -1)
            {
                ShipName = Main.npc[shipWMI].GetShipNPC().ShipName;
            }
            else
            {
                ShipName = "";
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texFrame = ModContent.Request<Texture2D>("StellarisShips/Images/UI/IconButton").Value;
            if ((Hovered() || Selected) && Enabled)
            {
                if (!HasHovered)
                {
                    HasHovered = true;
                    SomeUtils.PlaySound(SoundPath.UI + "MouseOver");
                }
                texFrame = ModContent.Request<Texture2D>("StellarisShips/Images/UI/IconButton_Hover").Value;
            }
            else
            {
                HasHovered = false;
            }
            spriteBatch.Draw(texFrame, Center, null, Color.White, 0, texFrame.Size() / 2f, 1, SpriteEffects.None, 0);
            if (NPCHeadType > -1)
            {
                Texture2D tex = TextureAssets.NpcHead[NPCHeadType].Value;
                float num10 = Math.Max(tex.Width, tex.Height);
                float scale = 1;
                if (num10 > 36) scale = 36 / num10;
                spriteBatch.Draw(tex, Center, null, Color.White, 0f, tex.Size() / 2f, scale, SpriteEffects.None, 0);
            }

            if (NPCWhoAmI != -1)
            {
                if (Main.npc[NPCWhoAmI].CountAsTownNPC())
                {
                    if (LeaderNPC.GetShip(Main.npc[NPCWhoAmI]) != -1)
                    {
                        Texture2D tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/UI/IconButton_Busy").Value;
                        spriteBatch.Draw(tex1, Center, null, Color.White, 0f, tex1.Size() / 2f, 1, SpriteEffects.None, 0);
                    }
                }
            }

            if (!Enabled)
            {
                Selected = false;
                Texture2D tex2 = ModContent.Request<Texture2D>("StellarisShips/Images/OtherIcons/Missing_MapIcon").Value;
                spriteBatch.Draw(tex2, Center, null, Color.White, 0, tex2.Size() / 2, 1, SpriteEffects.None, 0);
            }
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
            if (Hovered() && UIManager.LeftClicked && Enabled)
            {
                return true;
            }
            return false;
        }

        public bool RightClicked()
        {
            if (Hovered() && UIManager.RightClicked && Enabled)
            {
                return true;
            }
            return false;
        }

        public string GetDesc()
        {
            if (NPCWhoAmI != -1)
            {
                if (Main.npc[NPCWhoAmI].CountAsTownNPC())
                {
                    string result = "";
                    result += "[c/ffd700:" + Main.npc[NPCWhoAmI].FullName + "]";
                    if (LeaderNPC.GetShip(Main.npc[NPCWhoAmI]) != -1)
                    {
                        NPC ship = Main.npc[LeaderNPC.GetShip(Main.npc[NPCWhoAmI])];
                        result += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.UI.LeaderBusy"), "[c/87ceeb:" + ship.GetShipNPC().ShipName + "]");
                        if (!ship.ShipActive())
                        {
                            result += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.UI.LeaderMissing"), MissingBuff.GenTimeSpanFromSeconds(ship.GetShipNPC().MissingTimer / 60));
                        }
                        List<string> traits = Main.npc[NPCWhoAmI].GetGlobalNPC<TraitSystem>().Traits;
                        foreach(string t in traits)
                        {
                            string color = EverythingLibrary.Modifiers[t].Negative ?"[c/ff1111:" : "[c/adff2f:";
                            result += "\n" + color + EverythingLibrary.Modifiers[t].GetLocalizedName() + "]";
                            result += "\n" + EverythingLibrary.Modifiers[t].GetLocalizedDesc();
                        }
                        if(ship.ShipActive())
                        {
                            result += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.UI.LeaderCanResign"));
                        }
                    }
                    else
                    {
                        result += "\n" + Language.GetTextValue("Mods.StellarisShips.UI.LeaderSpare");
                        List<string> traits = Main.npc[NPCWhoAmI].GetGlobalNPC<TraitSystem>().Traits;
                        foreach (string t in traits)
                        {
                            string color = EverythingLibrary.Modifiers[t].Negative ? "[c/ff1111:" : "[c/adff2f:";
                            result += "\n" + color + EverythingLibrary.Modifiers[t].GetLocalizedName() + "]";
                            result += "\n" + EverythingLibrary.Modifiers[t].GetLocalizedDesc();
                        }
                        result += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.UI.LeaderCanAssign"));
                    }
                    return result;
                }
            }
            return Language.GetTextValue("Mods.StellarisShips.UI.LeaderNeeded");
        }
    }
}

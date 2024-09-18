using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace StellarisShips.UI
{
    public class ShroudUI : UIState
    {
        public static Vector2 StartPos => new(960 - PanelWidth / 2, 500 - PanelHeight / 2);
        public const int PanelWidth = 1200;
        public const int PanelHeight = 800;

        public static List<TalkButton> talkButtons = new();

        public static string TalkText = "";

        public static string Currentdialog = "";

        public static string HoverString = "";

        public static bool ShowInfo = true;

        public override void OnActivate()
        {
            AllClear();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Main.gameMenu || Main.LocalPlayer.IsDead() || !UIManager.ShroudVisible)
                return;
            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            //绘制基本框架
            Texture2D texPanel = ModContent.Request<Texture2D>("StellarisShips/Images/UI/ShroudBG", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw(texPanel, new Rectangle((int)StartPos.X, (int)StartPos.Y, PanelWidth, PanelHeight), Color.White * 0.95f);
            //绘制大头
            Texture2D texPortrait = ModContent.Request<Texture2D>("StellarisShips/Images/UI/Shroud", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw(texPortrait, new Rectangle((int)StartPos.X + 60, (int)StartPos.Y + 80, 1080, 380), Color.White);
            Utils.DrawBorderString(spriteBatch, Language.GetTextValue("Mods.StellarisShips.UI.IncomingTransmission"), new Vector2(StartPos.X + 40, StartPos.Y + 20), Color.White, 1.25f);
            Utils.DrawBorderString(spriteBatch, Language.GetTextValue("Mods.StellarisShips.UI.Shroud"), new Vector2(StartPos.X + 80, StartPos.Y + 480), Color.White, 1.25f);
            //绘制说话
            string talk = TalkText;
            talk = talk.Replace("[i:StellarisShips/MR_Icon]", "MR_");
            talk = SomeUtils.BreakLongString(talk, 55);
            talk = talk.Replace("MR_", "[i:StellarisShips/MR_Icon]");
            Utils.DrawBorderString(spriteBatch, talk, new Vector2(StartPos.X + 80, StartPos.Y + 520), Color.White);

            if (ShowInfo)
            {
                string info = "";
                Utils.DrawBorderString(spriteBatch, info, new Vector2(StartPos.X + 80, StartPos.Y + 750), Color.White);
            }
            foreach (TalkButton talkButton in talkButtons)
            {
                talkButton.Draw(spriteBatch);
            }

            if (HoverString != "")
            {
                DrawHoverDescs(spriteBatch);
            }

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Rectangle rectangle = new Rectangle((int)StartPos.X, (int)StartPos.Y, PanelWidth, PanelHeight);
            if (rectangle.Contains(new Point(Main.mouseX, Main.mouseY)))
            {
                Main.LocalPlayer.mouseInterface = true;
                Main.mouseText = true;
            }
            BaseDialog baseDialog = EverythingLibrary.Dialogs[Currentdialog];
            baseDialog.Update();

            for (int i = 0; i < talkButtons.Count; i++)
            {
                if (talkButtons[i].Hovered())
                {
                    HoverString = talkButtons[i].DescText;
                    break;
                }
            }
            HoverString = SomeUtils.BreakLongString(HoverString, 30);

            for (int i = 0; i < talkButtons.Count; i++)
            {
                if (talkButtons[i].Clicked())
                {
                    if (talkButtons[i].InternalText == "Exit")
                    {
                        SomeUtils.PlaySound(SoundPath.UI + "Close");
                    }
                    else
                    {
                        SomeUtils.PlaySound(SoundPath.UI + "Confirm");
                    }
                    baseDialog.ClickEvent(talkButtons[i].InternalText);
                    break;
                }
            }
        }

        public void DrawHoverDescs(SpriteBatch spriteBatch)
        {
            if (HoverString != "")
            {
                int line = 1;
                for (int i = 0; i < HoverString.Length; i++)
                {
                    if (HoverString[i] == '\n') line++;
                }
                Texture2D texDesc = ModContent.Request<Texture2D>("StellarisShips/Images/UI/DescPanel", AssetRequestMode.ImmediateLoad).Value;
                int TextWidth = TextHelper.GetTextWidth(HoverString) + 40;
                int XOffset = 0, YOffset = 0;
                if (Main.mouseX + 20 + TextWidth > Main.screenWidth)
                {
                    XOffset = Main.mouseX + 20 + TextWidth - Main.screenWidth;
                }
                if (Main.mouseY + 20 + 30 * line + 20 > Main.screenHeight)
                {
                    YOffset = Main.mouseY + 20 + 30 * line + 20 - Main.screenHeight;
                }
                spriteBatch.Draw(texDesc, new Rectangle(Main.mouseX + 20 - XOffset, Main.mouseY + 20 - YOffset, TextWidth, 30 * line + 20), Color.White);
                Utils.DrawBorderString(spriteBatch, HoverString, new Vector2(Main.mouseX + 40 - XOffset, Main.mouseY + 40 - YOffset), Color.White);
                Main.mouseText = true;
            }
        }

        public static void Start(string dialog)
        {
            AllClear();
            Currentdialog = dialog;
            BaseDialog baseDialog = EverythingLibrary.Dialogs[dialog];
            float baseX = 680; float baseY = 500; int unit = 35;
            talkButtons.Clear();
            for (int i = 0; i < baseDialog.ButtonNames.Count; i++)
            {
                talkButtons.Add(new TalkButton(new Vector2(StartPos.X + baseX, StartPos.Y + baseY), baseDialog.ButtonInternalStrs[i], baseDialog.ButtonNames[i]));
                baseY += unit;
            }
            baseDialog.SetUp();
        }

        public static void AllClear(bool All = false)
        {
            ShowInfo = true;
            Currentdialog = "";
            TalkText = "";
            talkButtons.Clear();
        }

    }
}

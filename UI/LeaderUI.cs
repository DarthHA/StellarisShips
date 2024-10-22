using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace StellarisShips.UI
{
    public class LeaderUI : UIState
    {
        public static Vector2 StartPos => new(650, 60);
        public const int PanelWidth = 420;
        public const int PanelHeight = 260;

        public static List<NPCIconButton> ShipList = new();
        public static List<NPCIconButton> NPCList = new();

        public static CancelButton cancelButton = new();

        public static string HoverString = "";

        public override void OnActivate()
        {
            AllClear();
        }

        public static void Open()
        {
            UIManager.LeaderUIVisible = true;
            AllClear();
            cancelButton = new(StartPos + new Vector2(PanelWidth - 25, 25));
        }

        public static void Close()
        {
            UIManager.LeaderUIVisible = false;
            AllClear();
            cancelButton = null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Main.gameMenu || Main.LocalPlayer.IsDead() || !UIManager.LeaderUIVisible)
                return;
            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            //绘制舰船NPC框
            foreach (NPCIconButton button in ShipList)
            {
                button.Draw(spriteBatch);
            }


            Texture2D TexPanel = ModContent.Request<Texture2D>("StellarisShips/Images/UI/LeaderUI").Value;
            spriteBatch.Draw(TexPanel, new Rectangle((int)StartPos.X, (int)StartPos.Y, PanelWidth, PanelHeight), Color.White);
            Vector2 DrawPos = StartPos + new Vector2(PanelWidth / 2, 10);
            Utils.DrawBorderString(spriteBatch, Language.GetTextValue("Mods.StellarisShips.UI.LeaderList"), DrawPos, Color.White, 1.25f, 0.5f);
            //绘制可选NPC列表
            foreach (NPCIconButton button in NPCList)
            {
                button.Draw(spriteBatch);
            }
            cancelButton.Draw(spriteBatch);

            DrawHoverDescs(spriteBatch);
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

            UpdateIconButton();

            UpdateHover();

            UpdateClicked();
        }


        public static void Start()
        {
            AllClear();
        }

        public static void AllClear(bool All = false)
        {
            NPCList.Clear();
            ShipList.Clear();
            HoverString = "";
        }


        private static void UpdateIconButton()
        {
            //更新舰船NPC框
            List<string> nameList1 = new();
            List<string> nameList2 = new();
            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive())
                {
                    if (ship.GetShipNPC().shipAI != ShipAI.Attack)
                    {
                        nameList1.Add(ship.GetShipNPC().ShipName);
                    }
                }
            }
            for (int i = ShipList.Count - 1; i >= 0; i--)
            {
                if (!nameList1.Contains(ShipList[i].ShipName))
                {
                    ShipList.RemoveAt(i);
                }
                else
                {
                    nameList2.Add(ShipList[i].ShipName);
                }
            }
            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive())
                {
                    if (ship.GetShipNPC().shipAI != ShipAI.Attack)
                    {
                        if (!nameList2.Contains(ship.GetShipNPC().ShipName))
                        {
                            ShipList.Add(new NPCIconButton(ship.Center - Main.screenPosition, ship.GetShipNPC().GetLeader(), ship.whoAmI));
                        }
                    }
                }
            }

            //更新状态
            foreach (NPCIconButton button in ShipList)
            {
                NPC ship = Main.npc[button.ShipWhoAmI];
                if (ship.ShipActive(true))
                {
                    Vector2 vector = Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
                    Vector2 vec2 = ship.Center - Main.LocalPlayer.velocity;
                    if (Main.LocalPlayer.gravDir != 1)
                    {
                        vec2.Y = Main.screenHeight - (vec2.Y - Main.screenPosition.Y) + Main.screenPosition.Y;
                    }
                    vec2 -= vector;
                    vec2 = new Vector2(vec2.X * Main.GameViewMatrix.Zoom.X, vec2.Y * Main.GameViewMatrix.Zoom.Y);
                    vec2 += vector;

                    button.Center = vec2 - Main.screenPosition;
                    int leaderwmi = ship.GetShipNPC().GetLeader();
                    if (leaderwmi != -1)
                    {
                        button.NPCHeadType = TownNPCProfiles.GetHeadIndexSafe(Main.npc[leaderwmi]);
                        button.NPCWhoAmI = leaderwmi;
                        button.NPCName = Main.npc[leaderwmi].GivenOrTypeName;
                    }
                    else
                    {
                        button.NPCHeadType = -1;
                        button.NPCWhoAmI = -1;
                        button.NPCName = "";
                    }
                }
            }

            //更新可选NPC列表
            List<string> nameList3 = new();
            List<string> nameList4 = new();
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.CountAsTownNPC())
                {
                    nameList3.Add(npc.GivenOrTypeName);
                }
            }
            for (int i = NPCList.Count - 1; i >= 0; i--)
            {
                if (!nameList3.Contains(NPCList[i].NPCName))
                {
                    NPCList.RemoveAt(i);
                }
                else
                {
                    nameList4.Add(NPCList[i].NPCName);
                }
            }
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.CountAsTownNPC())
                {
                    if (!nameList4.Contains(npc.GivenOrTypeName))
                    {
                        NPCList.Add(new NPCIconButton(Vector2.Zero, npc.whoAmI, -1));
                    }
                }
            }
            //重排
            int XSlot = 0, YSlot = 0;
            foreach (NPCIconButton button in NPCList)
            {
                button.Enabled = true;
                if (LeaderNPC.GetShip(Main.npc[button.NPCWhoAmI]) != -1)
                {
                    if (!Main.npc[LeaderNPC.GetShip(Main.npc[button.NPCWhoAmI])].ShipActive())
                    {
                        button.Enabled = false;
                    }
                }

                Vector2 Pos = StartPos + new Vector2(30 + XSlot * 40, 70 + YSlot * 40);
                button.Center = Pos;
                XSlot++;
                if (XSlot >= 10)
                {
                    YSlot++;
                    XSlot = 0;
                }
            }

            //更新光标状态
            foreach (NPCIconButton iconButton in NPCList)
            {
                if (iconButton.Hovered())
                {
                    Main.LocalPlayer.mouseInterface = true;
                    Main.mouseText = true;
                    break;
                }
            }
            foreach (NPCIconButton iconButton in ShipList)
            {
                if (iconButton.Hovered())
                {
                    Main.LocalPlayer.mouseInterface = true;
                    Main.mouseText = true;
                    break;
                }
            }
        }


        private static void UpdateClicked()
        {
            //关闭按钮最大
            if (cancelButton.LeftClicked())
            {
                SomeUtils.PlaySound(SoundPath.UI + "Close");
                Close();
                return;
            }

            //右键取消
            for (int i = 0; i < NPCList.Count; i++)
            {
                if (NPCList[i].RightClicked())
                {
                    SomeUtils.PlaySound(SoundPath.UI + "Close");
                    if (LeaderNPC.GetShip(Main.npc[NPCList[i].NPCWhoAmI]) != -1)
                    {
                        Main.npc[LeaderNPC.GetShip(Main.npc[NPCList[i].NPCWhoAmI])].GetShipNPC().ClearLeader();
                    }
                    break;
                }
            }

            //左键指派
            int npcwmi = -1;
            for (int i = 0; i < NPCList.Count; i++)
            {
                if (NPCList[i].LeftClicked())
                {
                    npcwmi = NPCList[i].NPCWhoAmI;
                    break;
                }
            }

            if (npcwmi != -1)
            {
                int shipwmi = -1;
                foreach (NPCIconButton button in ShipList)
                {
                    if (button.Selected)
                    {
                        shipwmi = button.ShipWhoAmI;
                        button.Selected = false;
                        break;
                    }
                }

                if (shipwmi != -1)
                {
                    //给船配指挥官
                    SomeUtils.PlaySound(SoundPath.UI + "Confirm");
                    Main.npc[shipwmi].GetShipNPC().AssignLeader(Main.npc[npcwmi]);
                    if (Main.LocalPlayer.talkNPC == npcwmi) Main.LocalPlayer.SetTalkNPC(-1);
                }
                else
                {
                    SomeUtils.PlaySound(SoundPath.UI + "Click");
                }
                return;
            }



            //左键选择
            for (int i = 0; i < ShipList.Count; i++)
            {
                if (ShipList[i].LeftClicked())
                {
                    SomeUtils.PlaySound(SoundPath.UI + "Click");
                    ShipList[i].Selected = !ShipList[i].Selected;
                    for (int j = 0; j < ShipList.Count; j++)
                    {
                        if (i != j) ShipList[j].Selected = false;
                    }
                    break;
                }
            }
            //右键取消
            for (int i = 0; i < ShipList.Count; i++)
            {
                if (ShipList[i].RightClicked())
                {
                    SomeUtils.PlaySound(SoundPath.UI + "Close");
                    ShipList[i].Selected = false;
                    Main.npc[ShipList[i].ShipWhoAmI].GetShipNPC().ClearLeader();
                    break;
                }
            }
        }

        private static void UpdateHover()
        {
            foreach (NPCIconButton button in NPCList)
            {
                if (button.Hovered())
                {
                    HoverString = button.GetDesc();
                    return;
                }
            }
            foreach (NPCIconButton button in ShipList)
            {
                if (button.Hovered())
                {
                    HoverString = button.GetDesc();
                    return;
                }
            }
            HoverString = "";
        }

        private static void DrawHoverDescs(SpriteBatch spriteBatch)
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
    }
}
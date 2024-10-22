using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Content.Components.Weapons;
using StellarisShips.Content.Items;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace StellarisShips.UI
{

    public class ShipDesignUI : UIState
    {
        //UI部分
        /// <summary>
        /// 左栏选取按钮
        /// </summary>
        public static List<ItemSelectButton> itemSelectButtons = new();
        /// <summary>
        /// 非核心部件按钮
        /// </summary>
        public static List<List<ItemSlotButton>> componentButtons = new();
        /// <summary>
        /// 核心部件按钮
        /// </summary>
        public static List<ItemSlotButton> coreComponentButtons = new();
        /// <summary>
        /// 船体选择按钮
        /// </summary>
        public static List<SectionSelectButton> sectionSelectButtons = new();
        /// <summary>
        /// 确认按钮
        /// </summary>
        public static YesButton yesButton;
        /// <summary>
        /// 取消按钮
        /// </summary>
        public static YesButton noButton;
        /// <summary>
        /// 清除设计按钮
        /// </summary>
        public static YesButton clearButton;

        //public static YesButton randomButton;

        /// <summary>
        /// 一个引用，用于判断哪个按钮被选中
        /// </summary>
        public static object SelectedOne;

        /// <summary>
        /// 一个引用，用于判断光标在哪个按钮上面
        /// </summary>
        public static object HoverOne;

        /// <summary>
        /// 多次选中可以挑选更低级的配件这个特性
        /// </summary>
        public static int ClickForLowerLevel = 0;


        public const int PanelWidth = 1300;
        public const int PanelHeight = 800;

        public static Vector2 StartPos => new(960 - PanelWidth / 2, 500 - PanelHeight / 2);

        /// <summary>
        /// 舰船类型
        /// </summary>
        public static string shipType = "";

        /// <summary>
        /// 舰船名称
        /// </summary>
        public static string graphName = "";

        public static string HoverString = "";

        public static string StatString = "";

        public override void OnActivate()
        {
            AllClear(true);
        }

        public static void StartForSelectShipType(bool Restart = true)
        {
            AllClear(true);
            if (Restart)
            {
                graphName = SomeUtils.GetRandomName();
            }
            //更新左栏
            int index = 0;
            foreach (string ship in EverythingLibrary.Ships.Keys)
            {
                if (EverythingLibrary.Ships[ship].CanUnlock())
                {
                    itemSelectButtons.Add(new ItemSelectButton(StartPos + new Vector2(20, 45 + index * 60), EverythingLibrary.Ships[ship].GetIcon(), ship, EverythingLibrary.Ships[ship].GetLocalizedName(), EverythingLibrary.Ships[ship].GetLocalizedDescription()));
                    index++;
                }
            }
            //itemSelectButtons.Sort((a, b) => { return EverythingLibrary.Ships[a.InternalText].Size.CompareTo(EverythingLibrary.Ships[b.InternalText].Size); });
        }

        public static void StartForNormal(ShipGraph graph)        //需要载入数据
        {
            AllClear(true);
            //在这里载入数据
            graphName = graph.GraphName;
            shipType = graph.ShipType;

            Vector2 OffSet = new Vector2((3 - EverythingLibrary.Ships[shipType].PartCount) * 90, 0);

            for (int i = 0; i < graph.Parts.Count; i++)
            {
                //绘制区段按钮
                sectionSelectButtons.Add(new SectionSelectButton(StartPos + new Vector2(360 + 180 * i, 70) + OffSet, graph.Parts[i].InternalName, EverythingLibrary.Sections[graph.Parts[i].InternalName].GetLocalizedName()));

                //绘制武器部件槽
                componentButtons.Add(new List<ItemSlotButton>());
                int j = 0;
                foreach (string slotType in EverythingLibrary.Sections[sectionSelectButtons[i].InternalText].WeaponSlot)
                {
                    Vector2 TopLeft = StartPos + new Vector2(360 + 180 * i, 130) + OffSet + PlaceItemSlotButton(j);
                    componentButtons[i].Add(new ItemSlotButton(TopLeft, slotType, graph.Parts[i].WeaponSlot[j]));
                    j++;
                }

                //绘制通用部件槽
                j = 0;
                foreach (string slotType in EverythingLibrary.Sections[sectionSelectButtons[i].InternalText].UtilitySlot)
                {
                    Vector2 TopLeft = StartPos + new Vector2(360 + 180 * i, 570) + OffSet + PlaceItemSlotButton(j);
                    componentButtons[i].Add(new ItemSlotButton(TopLeft, slotType, graph.Parts[i].UtilitySlot[j]));
                    j++;
                }
            }

            //绘制核心部件槽
            Vector2 TopLeft2 = StartPos + new Vector2(920, 210);
            coreComponentButtons.Add(new ItemSlotButton(TopLeft2, ComponentTypes.Reactor, graph.CoreComponent[0]));
            coreComponentButtons.Add(new ItemSlotButton(TopLeft2 + new Vector2(0, 60), ComponentTypes.FTLDrive, graph.CoreComponent[1]));
            coreComponentButtons.Add(new ItemSlotButton(TopLeft2 + new Vector2(0, 120), ComponentTypes.Thruster, graph.CoreComponent[2]));
            coreComponentButtons.Add(new ItemSlotButton(TopLeft2 + new Vector2(0, 180), ComponentTypes.Sensor, graph.CoreComponent[3]));
            coreComponentButtons.Add(new ItemSlotButton(TopLeft2 + new Vector2(0, 240), ComponentTypes.Computer, graph.CoreComponent[4]));
            if (EverythingLibrary.Ships[graph.ShipType].HasAura)
            {
                coreComponentButtons.Add(new ItemSlotButton(TopLeft2 + new Vector2(0, 300), ComponentTypes.Aura, graph.CoreComponent[5]));
            }

            StatString = CalcShipStat();
        }

        public void UpdateForSelectShipType()
        {
            foreach (ItemSelectButton itemSelectButton in itemSelectButtons)
            {
                if (itemSelectButton.Clicked())
                {
                    shipType = itemSelectButton.InternalText;
                    break;
                }
            }
            if (shipType != "")
            {
                SomeUtils.PlaySoundRandom(SoundPath.UI + "AddS", 4);
                //360,70,绘制区段按钮
                AllClear(false);
                Vector2 OffSet = new Vector2((3 - EverythingLibrary.Ships[shipType].PartCount) * 90, 0);
                for (int i = 0; i < EverythingLibrary.Ships[shipType].PartCount; i++)
                {
                    string firstuseSection = EverythingLibrary.Ships[shipType].CanUseSections[i][0];
                    sectionSelectButtons.Add(new SectionSelectButton(StartPos + new Vector2(360 + 180 * i, 70) + OffSet, firstuseSection, EverythingLibrary.Sections[firstuseSection].GetLocalizedName()));

                    //360,130,绘制武器部件按钮
                    componentButtons.Add(new List<ItemSlotButton>());

                    int j = 0;
                    foreach (string slotType in EverythingLibrary.Sections[sectionSelectButtons[i].InternalText].WeaponSlot)
                    {
                        Vector2 TopLeft = StartPos + new Vector2(360 + 180 * i, 130) + OffSet + PlaceItemSlotButton(j);
                        componentButtons[i].Add(new ItemSlotButton(TopLeft, slotType, ""));
                        j++;
                    }

                    //360,450,绘制通用部件按钮
                    j = 0;
                    foreach (string slotType in EverythingLibrary.Sections[sectionSelectButtons[i].InternalText].UtilitySlot)
                    {
                        Vector2 TopLeft = StartPos + new Vector2(360 + 180 * i, 570) + OffSet + PlaceItemSlotButton(j);
                        componentButtons[i].Add(new ItemSlotButton(TopLeft, slotType, ""));
                        j++;
                    }
                }

                //1120,120,绘制核心部件按钮
                Vector2 TopLeft2 = StartPos + new Vector2(920, 210);
                coreComponentButtons.Add(new ItemSlotButton(TopLeft2, ComponentTypes.Reactor, LibraryHelpers.FindMaxLevelComponent("", ComponentTypes.Reactor, true)));
                coreComponentButtons.Add(new ItemSlotButton(TopLeft2 + new Vector2(0, 60), ComponentTypes.FTLDrive, LibraryHelpers.FindMaxLevelComponent("", ComponentTypes.FTLDrive, true)));
                coreComponentButtons.Add(new ItemSlotButton(TopLeft2 + new Vector2(0, 120), ComponentTypes.Thruster, LibraryHelpers.FindMaxLevelComponent("", ComponentTypes.Thruster, true)));
                coreComponentButtons.Add(new ItemSlotButton(TopLeft2 + new Vector2(0, 180), ComponentTypes.Sensor, LibraryHelpers.FindMaxLevelComponent("", ComponentTypes.Sensor, true)));
                coreComponentButtons.Add(new ItemSlotButton(TopLeft2 + new Vector2(0, 240), ComponentTypes.Computer, LibraryHelpers.FindMaxLevelComponent(EverythingLibrary.Ships[shipType].InitialComputer, ComponentTypes.Computer, true)));
                if (EverythingLibrary.Ships[shipType].HasAura)
                {
                    coreComponentButtons.Add(new ItemSlotButton(TopLeft2 + new Vector2(0, 300), ComponentTypes.Aura, "O_NoAura_1"));
                }
                StatString = CalcShipStat();
            }
        }

        public void UpdateForNormal()
        {
            #region 点部件槽更新左栏
            //先判断输入端
            object Click1 = null;
            foreach (List<ItemSlotButton> itemSelectButtons in componentButtons)
            {
                foreach (ItemSlotButton button in itemSelectButtons)
                {
                    if (button.Clicked())
                    {
                        Click1 = button;
                        break;
                    }
                }
            }
            foreach (ItemSlotButton button in coreComponentButtons)
            {
                if (button.Clicked())
                {
                    Click1 = button;
                    break;
                }
            }
            foreach (SectionSelectButton button in sectionSelectButtons)
            {
                if (button.Clicked())
                {
                    Click1 = button;
                    break;
                }
            }

            //更新左栏
            if (Click1 != null)
            {
                SomeUtils.PlaySound(SoundPath.UI + "Click");
                if (SelectedOne is ItemSlotButton) (SelectedOne as ItemSlotButton).Selected = false;
                if (SelectedOne is SectionSelectButton) (SelectedOne as SectionSelectButton).Selected = false;
                SelectedOne = Click1;
                itemSelectButtons.Clear();
                ClickForLowerLevel = 0;
                if (Click1 is ItemSlotButton)
                {
                    (Click1 as ItemSlotButton).Selected = true;
                    List<BaseComponent> Samples = LibraryHelpers.FindMaxLevelComponent(true);

                    //如果是非核心部件或者是电脑部件就显示最高级，如果是核心部件就全部显示

                    foreach (ItemSlotButton button in coreComponentButtons)
                    {
                        if ((Click1 as ItemSlotButton) == button && (Click1 as ItemSlotButton).EquipType != ComponentTypes.Computer)
                        {
                            Samples.Clear();
                            foreach (BaseComponent component in EverythingLibrary.Components.Values)
                            {
                                if (component.CanUnlock())
                                {
                                    Samples.Add(component);
                                }
                            }
                            ClickForLowerLevel = 1;
                            break;
                        }
                    }


                    int index = 0;
                    foreach (BaseComponent component in Samples)
                    {
                        if (component.EquipType == (Click1 as ItemSlotButton).EquipType)
                        {
                            if ((Click1 as ItemSlotButton).EquipType != ComponentTypes.Computer)
                            {
                                itemSelectButtons.Add(new ItemSelectButton(StartPos + new Vector2(20, 45) + new Vector2(0, index * 60), component.GetIcon(), component.InternalName, component.GetLocalizedName(), component.GetLocalizedDescription()));
                                itemSelectButtons[index].AppendText = component.ExtraInfo;
                                index++;
                            }
                            else if (EverythingLibrary.Ships[shipType].CanUseComputer.Contains(component.TypeName))
                            {
                                itemSelectButtons.Add(new ItemSelectButton(StartPos + new Vector2(20, 45) + new Vector2(0, index * 60), component.GetIcon(), component.InternalName, component.GetLocalizedName(), component.GetLocalizedDescription()));
                                itemSelectButtons[index].AppendText = component.ExtraInfo;
                                index++;
                            }
                        }
                    }
                }
                else if (Click1 is SectionSelectButton)
                {
                    (Click1 as SectionSelectButton).Selected = true;
                    int SectionIndex = 0;
                    //首先我们要看看是第几个区段
                    for (int i = 0; i < sectionSelectButtons.Count; i++)
                    {
                        if (Click1 == sectionSelectButtons[i])
                        {
                            SectionIndex = i;
                            break;
                        }
                    }
                    int index = 0;
                    foreach (BaseSection section in EverythingLibrary.Sections.Values)
                    {
                        if (EverythingLibrary.Ships[shipType].CanUseSections[SectionIndex].Contains(section.InternalName))
                        {
                            itemSelectButtons.Add(new ItemSelectButton(StartPos + new Vector2(20, 45) + new Vector2(0, index * 60), section.GetIcon(), section.InternalName, section.GetLocalizedName(), ""));
                            itemSelectButtons[index].AppendText = section.ExtraInfo;
                            index++;
                        }
                    }
                }
                return;
            }
            #endregion

            #region 右键点部件槽去除部件
            object Click3 = null;
            bool ExtraClick3 = false;
            foreach (List<ItemSlotButton> itemSlotButtons in componentButtons)
            {
                foreach (ItemSlotButton button in itemSlotButtons)
                {
                    if (button.RightClicked())
                    {
                        Click3 = button;
                        ExtraClick3 = true;
                        break;
                    }
                }
            }

            Rectangle recUI = new((int)StartPos.X, (int)StartPos.Y, PanelWidth, PanelHeight);
            if (UIManager.RightClicked && recUI.Contains(new Point(Main.mouseX, Main.mouseY)))
            {
                ExtraClick3 = true;
            }

            if (ExtraClick3)
            {
                SomeUtils.PlaySound(SoundPath.UI + "Close");
                itemSelectButtons.Clear();
                if (SelectedOne is ItemSlotButton)
                {
                    (SelectedOne as ItemSlotButton).Selected = false;
                }
                else if (SelectedOne is SectionSelectButton)
                {
                    (SelectedOne as SectionSelectButton).Selected = false;
                }
                SelectedOne = null;
            }

            if (Click3 is ItemSlotButton)
            {
                (Click3 as ItemSlotButton).ItemType = "";
                StatString = CalcShipStat();
                return;
            }
            #endregion

            #region 点左栏更新部件槽
            if (SelectedOne == null) return;
            //然后判断输出
            int Click2 = -1;
            for (int i = 0; i < itemSelectButtons.Count; i++)
            {
                if (itemSelectButtons[i].Clicked())
                {
                    Click2 = i;
                    //更新Slot
                    break;
                }
            }

            if (Click2 != -1)
            {
                if (SelectedOne is ItemSlotButton)
                {
                    if (LibraryHelpers.IsWeapon((SelectedOne as ItemSlotButton).EquipType))
                    {
                        SomeUtils.PlaySoundRandom(SoundPath.UI + "AddW", 4);
                    }
                    else
                    {
                        SomeUtils.PlaySoundRandom(SoundPath.UI + "AddU", 4);
                    }

                    (SelectedOne as ItemSlotButton).ItemType = itemSelectButtons[Click2].InternalText;
                    StatString = CalcShipStat();
                    if (ClickForLowerLevel == 0)//更新更低级的
                    {
                        List<BaseComponent> groups = LibraryHelpers.FindTheSameTypeAndSlotComponent(EverythingLibrary.Components[itemSelectButtons[Click2].InternalText], true);

                        itemSelectButtons.Clear();

                        int index = 0;
                        foreach (BaseComponent component in groups)
                        {
                            itemSelectButtons.Add(new ItemSelectButton(StartPos + new Vector2(20, 45) + new Vector2(0, index * 60), component.GetIcon(), component.InternalName, component.GetLocalizedName(), component.GetLocalizedDescription()));
                            itemSelectButtons[index].AppendText = component.ExtraInfo;
                            index++;
                        }

                        ClickForLowerLevel = 1;
                    }
                }
                else if (SelectedOne is SectionSelectButton)
                {
                    SomeUtils.PlaySoundRandom(SoundPath.UI + "AddS", 4);
                    (SelectedOne as SectionSelectButton).Selected = false;
                    (SelectedOne as SectionSelectButton).ShowText = itemSelectButtons[Click2].ShowText;
                    (SelectedOne as SectionSelectButton).InternalText = itemSelectButtons[Click2].InternalText;
                    //切换区段应该清除对应区段的块
                    int SectionIndex = 0;
                    for (int i = 0; i < sectionSelectButtons.Count; i++)
                    {
                        if ((SelectedOne as SectionSelectButton) == sectionSelectButtons[i])
                        {
                            SectionIndex = i;
                            break;
                        }
                    }

                    //重置武器部件槽
                    componentButtons[SectionIndex].Clear();

                    Vector2 OffSet = new Vector2((3 - EverythingLibrary.Ships[shipType].PartCount) * 90, 0);

                    int j = 0;
                    foreach (string slotType in EverythingLibrary.Sections[sectionSelectButtons[SectionIndex].InternalText].WeaponSlot)
                    {
                        Vector2 TopLeft = StartPos + new Vector2(360 + 180 * SectionIndex, 130) + OffSet + PlaceItemSlotButton(j);
                        componentButtons[SectionIndex].Add(new ItemSlotButton(TopLeft, slotType, ""));
                        j++;
                    }
                    //重置通用部件槽
                    j = 0;
                    foreach (string slotType in EverythingLibrary.Sections[sectionSelectButtons[SectionIndex].InternalText].UtilitySlot)
                    {
                        Vector2 TopLeft = StartPos + new Vector2(360 + 180 * SectionIndex, 570) + OffSet + PlaceItemSlotButton(j);
                        componentButtons[SectionIndex].Add(new ItemSlotButton(TopLeft, slotType, ""));
                        j++;
                    }

                    //清空左栏
                    itemSelectButtons.Clear();

                    StatString = CalcShipStat();
                }
                return;
            }
            #endregion

        }

        public static void UpdateForHover()
        {
            HoverOne = null;
            foreach (List<ItemSlotButton> itemSelectButtons in componentButtons)
            {
                foreach (ItemSlotButton button in itemSelectButtons)
                {
                    if (button.Hovered())
                    {
                        HoverOne = button;
                        break;
                    }
                }
            }
            foreach (ItemSlotButton button in coreComponentButtons)
            {
                if (button.Hovered())
                {
                    HoverOne = button;
                    break;
                }
            }
            foreach (SectionSelectButton button in sectionSelectButtons)
            {
                if (button.Hovered())
                {
                    HoverOne = button;
                    break;
                }
            }

            for (int i = 0; i < itemSelectButtons.Count; i++)
            {
                if (itemSelectButtons[i].Hovered())
                {
                    HoverOne = itemSelectButtons[i];
                    break;
                }
            }
            string ShowStr = "";
            if (HoverOne is ItemSlotButton)
            {
                if ((HoverOne as ItemSlotButton).ItemType != "")
                {
                    ShowStr = EverythingLibrary.Components[(HoverOne as ItemSlotButton).ItemType].GetLocalizedDescription();
                }
            }
            else if (HoverOne is ItemSelectButton)
            {
                ShowStr = (HoverOne as ItemSelectButton).DescText;
            }
            else
            {
                if (StatString != "")
                {
                    Vector2 WordSize = Terraria.GameContent.FontAssets.MouseText.Value.MeasureString(StatString);
                    Rectangle WordRec = new((int)StartPos.X + 1050, (int)StartPos.Y + 150, 200, (int)WordSize.Y);
                    if (WordRec.Contains(new Point(Main.mouseX, Main.mouseY)))
                    {
                        Vector2 RelaPos = new Vector2(Main.mouseX, Main.mouseY) - (StartPos + new Vector2(1050, 150));
                        int line = (int)(RelaPos.Y / WordSize.Y * 13);
                        if (line >= 0 && line <= 12)
                        {
                            ShowStr = Language.GetTextValue("Mods.StellarisShips.UI.ShipStat" + line.ToString());
                        }
                    }
                }
            }
            HoverString = SomeUtils.BreakLongString(ShowStr, 40);
            HoverString = HoverString.Replace("MR_", "[i:StellarisShips/MR_Icon]");
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Main.gameMenu || Main.LocalPlayer.IsDead() || !UIManager.ShipDesignVisible)
                return;
            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            //绘制基本框架
            Texture2D texPanel = ModContent.Request<Texture2D>("StellarisShips/Images/UI/ShipDesignBG", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw(texPanel, new Rectangle((int)StartPos.X, (int)StartPos.Y, PanelWidth, PanelHeight), Color.White * 0.95f);
            Utils.DrawBorderString(spriteBatch, Language.GetTextValue("Mods.StellarisShips.UI.ShipDesign"), StartPos + new Vector2(600, 30), Color.White, 1.2f, 0.5f, 0.5f);

            DrawSlots(spriteBatch);

            DrawButtons(spriteBatch);

            DrawShips(spriteBatch);

            DrawShipStats(spriteBatch);

            DrawHoverDescs(spriteBatch);

        }

        public void DrawSlots(SpriteBatch spriteBatch)
        {
            foreach (List<ItemSlotButton> itemSlotButtons in componentButtons)
            {
                foreach (ItemSlotButton itemSlotButton in itemSlotButtons)
                {
                    itemSlotButton.Draw(spriteBatch);
                }
            }
            foreach (ItemSelectButton itemSelectButton in itemSelectButtons)
            {
                itemSelectButton.Draw(spriteBatch);
            }
            foreach (ItemSlotButton itemSlotButton in coreComponentButtons)
            {
                itemSlotButton.Draw(spriteBatch);
            }
            foreach (SectionSelectButton sectionSelectButton in sectionSelectButtons)
            {
                sectionSelectButton.Draw(spriteBatch);
            }
        }

        public void DrawButtons(SpriteBatch spriteBatch)
        {
            if (yesButton != null)
            {
                yesButton.Draw(spriteBatch);
            }
            if (noButton != null)
            {
                noButton.Draw(spriteBatch);
            }
            if (clearButton != null)
            {
                clearButton.Draw(spriteBatch);
            }
        }

        public void DrawShips(SpriteBatch spriteBatch)
        {
            if (shipType != "")
            {
                Texture2D texBlackPanel = ModContent.Request<Texture2D>("StellarisShips/Images/UI/ShipShowBG", AssetRequestMode.ImmediateLoad).Value;

                spriteBatch.Draw(texBlackPanel, new Rectangle((int)StartPos.X + 360, (int)StartPos.Y + 260, 540, 280), Color.White); //600,300

                List<string> sectionList = new();
                foreach (SectionSelectButton sectionSelectButton in sectionSelectButtons)
                {
                    sectionList.Add(sectionSelectButton.InternalText);
                }
                float GlobalOffset = EverythingLibrary.Ships[shipType].ShipDesignViewOffSet;
                float TailOffset = EverythingLibrary.Sections[sectionSelectButtons[sectionSelectButtons.Count - 1].InternalText].TailDrawOffSet;
                TailOffset += GlobalOffset - 100;
                Texture2D shipTex = LibraryHelpers.GetShipTexture(sectionList);
                EasyDraw.AnotherDraw(BlendState.NonPremultiplied);
                spriteBatch.Draw(shipTex, StartPos + new Vector2(900, 400) + new Vector2(TailOffset, 0), null, Color.White, 0, new Vector2(shipTex.Width, shipTex.Height / 2), 0.75f, SpriteEffects.None, 0);
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);

                //绘制武器
                for (int i = 0; i < componentButtons.Count; i++)
                {
                    for (int j = 0; j < componentButtons[i].Count; j++)
                    {
                        if (LibraryHelpers.IsWeapon(componentButtons[i][j].EquipType) && componentButtons[i][j].ItemType != "")
                        {
                            Texture2D texCannon = LibraryHelpers.GetGunTex(componentButtons[i][j].ItemType);
                            if (texCannon != null)
                            {
                                Vector2 weaponPos = EverythingLibrary.Sections[sectionSelectButtons[i].InternalText].WeaponPos[j];
                                weaponPos = new Vector2(-weaponPos.Y, weaponPos.X) + (StartPos + new Vector2(900, 400) + new Vector2(-100, 0));
                                weaponPos.X += GlobalOffset;
                                float scale = EverythingLibrary.Ships[shipType].WeaponScale;
                                spriteBatch.Draw(texCannon, weaponPos, null, Color.White, 0, texCannon.Size() / 2f, scale, SpriteEffects.None, 0);
                            }
                        }
                    }
                }

                //Vector2 componentRelativePos = new Vector2(Main.mouseX, Main.mouseY) - (StartPos + new Vector2(900, 400) + new Vector2(-100, 0));
                //Utils.DrawBorderString(spriteBatch, "武器相对坐标: " + new Vector2(-componentRelativePos.X, componentRelativePos.Y), StartPos + new Vector2(165, 660), Color.White, 1, 0.5f, 0.5f);

                Vector2? Pos = null;

                if (HoverOne is ItemSlotButton)
                {
                    for (int i = 0; i < componentButtons.Count; i++)
                    {
                        for (int j = 0; j < componentButtons[i].Count; j++)
                        {
                            if ((HoverOne as ItemSlotButton) == componentButtons[i][j] && LibraryHelpers.IsWeapon((HoverOne as ItemSlotButton).EquipType))
                            {
                                Pos = EverythingLibrary.Sections[sectionSelectButtons[i].InternalText].WeaponPos[j];
                                break;
                            }
                        }
                    }
                }
                if (Pos == null)
                {
                    if (SelectedOne is ItemSlotButton)
                    {
                        for (int i = 0; i < componentButtons.Count; i++)
                        {
                            for (int j = 0; j < componentButtons[i].Count; j++)
                            {
                                if ((SelectedOne as ItemSlotButton) == componentButtons[i][j] && LibraryHelpers.IsWeapon((SelectedOne as ItemSlotButton).EquipType))
                                {
                                    Pos = EverythingLibrary.Sections[sectionSelectButtons[i].InternalText].WeaponPos[j];
                                    break;
                                }
                            }
                        }
                    }
                }

                if (Pos.HasValue)
                {
                    Texture2D texPos = ModContent.Request<Texture2D>("StellarisShips/Images/UI/WeaponPos", AssetRequestMode.ImmediateLoad).Value;
                    Vector2 weaponPos = new Vector2(-Pos.Value.Y, Pos.Value.X) + (StartPos + new Vector2(900, 400) + new Vector2(-100, 0));
                    weaponPos.X += GlobalOffset;
                    spriteBatch.Draw(texPos, weaponPos, null, Color.White, 0, texPos.Size() / 2f, 2.5f, SpriteEffects.None, 0);
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

        public void DrawShipStats(SpriteBatch spriteBatch)
        {
            if (shipType != "")
            {
                //160,600
                string str = string.Format(Language.GetTextValue("Mods.StellarisShips.UI.GraphNameUI"), graphName, EverythingLibrary.Ships[shipType].GetLocalizedName());
                Utils.DrawBorderString(spriteBatch, str, StartPos + new Vector2(1150, 80), Color.White, 1f, 0.5f, 0f);
                if (StatString != "")
                {
                    Utils.DrawBorderString(spriteBatch, StatString, StartPos + new Vector2(1050, 150), Color.White, 1f, 0f, 0f);
                }

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
            if (shipType == "")
            {
                UpdateForSelectShipType();
            }
            else
            {
                UpdateForNormal();
            }

            UpdateYesButton();
            UpdateNoButton();
            UpdateClearButton();

            UpdateForHover();
        }

        public void UpdateYesButton()
        {
            if (yesButton == null)
            {
                yesButton = new(StartPos + new Vector2(1100, 600), Language.GetTextValue("Mods.StellarisShips.UI.YesButton"));
            }
            if (shipType == "")
            {
                yesButton.Enabled = false;
            }
            else
            {
                bool FullCore = true;
                foreach (ItemSlotButton itemSlotButton in coreComponentButtons)
                {
                    if (itemSlotButton.ItemType == "")
                    {
                        FullCore = false;
                        break;
                    }
                }

                if (FullCore && Main.LocalPlayer.HeldItem.type == ModContent.ItemType<GraphItem>())
                {
                    yesButton.Enabled = true;
                }
                else
                {
                    yesButton.Enabled = false;
                }
            }

            if (yesButton.Clicked())
            {
                SomeUtils.PlaySound(SoundPath.UI + "Confirm");
                //存储
                ShipGraph graph = new();

                graph.GraphName = graphName;
                graph.ShipType = shipType;
                graph.Value = EverythingLibrary.Ships[shipType].Value;
                foreach (ItemSlotButton itemSlotButton in coreComponentButtons)
                {
                    graph.CoreComponent.Add(itemSlotButton.ItemType);
                    if (itemSlotButton.ItemType != "")
                    {
                        graph.Value += EverythingLibrary.Components[itemSlotButton.ItemType].Value;
                        graph.MRValue += EverythingLibrary.Components[itemSlotButton.ItemType].MRValue;
                    }
                }
                for (int i = 0; i < componentButtons.Count; i++)
                {
                    graph.Parts.Add(new SectionForSave());
                    graph.Parts[i].InternalName = sectionSelectButtons[i].InternalText;
                    foreach (ItemSlotButton button in componentButtons[i])
                    {
                        if (LibraryHelpers.IsUtility(button.EquipType))
                        {
                            graph.Parts[i].UtilitySlot.Add(button.ItemType);
                        }
                        else
                        {
                            graph.Parts[i].WeaponSlot.Add(button.ItemType);
                        }

                        if (button.ItemType != "")
                        {
                            graph.Value += EverythingLibrary.Components[button.ItemType].Value;
                            graph.MRValue += EverythingLibrary.Components[button.ItemType].MRValue;
                        }
                    }
                }
                (Main.LocalPlayer.HeldItem.ModItem as GraphItem).graph = graph.Copy();
                (Main.LocalPlayer.HeldItem.ModItem as GraphItem).DataDesc = CalcShipStat();
                UIManager.ShipDesignVisible = false;
                AllClear(true);
            }
        }

        public void UpdateNoButton()
        {
            if (noButton == null)
            {
                noButton = new(StartPos + new Vector2(1100, 700), Language.GetTextValue("Mods.StellarisShips.UI.NoButton"));
            }
            if (noButton.Clicked())
            {
                SomeUtils.PlaySound(SoundPath.UI + "Close");
                UIManager.ShipDesignVisible = false;
                AllClear(true);
            }
        }

        public void UpdateClearButton()
        {
            if (clearButton == null)
            {
                clearButton = new(StartPos + new Vector2(1100, 650), Language.GetTextValue("Mods.StellarisShips.UI.ClearButton"));
            }
            clearButton.Enabled = shipType != "";
            if (clearButton.Clicked())
            {
                SomeUtils.PlaySound(SoundPath.UI + "Click");
                StartForSelectShipType(false);
            }
        }

        private static Vector2 PlaceItemSlotButton(int index)
        {
            int x = index % 3;
            int y = index / 3;
            return new Vector2(x * 60, y * 60);
        }


        public static void AllClear(bool ClearShipType = true)
        {
            itemSelectButtons.Clear();
            componentButtons.Clear();
            coreComponentButtons.Clear();
            sectionSelectButtons.Clear();
            SelectedOne = null;
            ClickForLowerLevel = 0;
            StatString = "";
            HoverString = "";
            if (ClearShipType)
            {
                shipType = "";
            }
        }


        private static string CalcShipStat()
        {
            if (shipType == "") return "";
            NPC dummyShip = new();
            dummyShip.SetDefaults(ModContent.NPCType<ShipNPC>());
            dummyShip.GetShipNPC().ShipGraph = new();
            dummyShip.GetShipNPC().ShipGraph.ShipType = shipType;
            float TotalDPS = 0;
            long Value = EverythingLibrary.Ships[shipType].Value;
            int ValueMR = 0;
            foreach (ItemSlotButton button in coreComponentButtons)           //计算核心部件舰船加成与费用
            {
                if (button.ItemType != "")
                {
                    EverythingLibrary.Components[button.ItemType].ApplyEquip(dummyShip);
                    Value += EverythingLibrary.Components[button.ItemType].Value;
                    ValueMR += EverythingLibrary.Components[button.ItemType].MRValue;
                }
            }
            foreach (List<ItemSlotButton> itemSlotButtons in componentButtons)    //计算常规部件舰船加成与费用
            {
                foreach (ItemSlotButton button in itemSlotButtons)
                {
                    if (button.ItemType != "")
                    {
                        EverythingLibrary.Components[button.ItemType].ApplyEquip(dummyShip);
                        Value += EverythingLibrary.Components[button.ItemType].Value;
                        ValueMR += EverythingLibrary.Components[button.ItemType].MRValue;
                    }
                }
            }

            //添加修正
            dummyShip.GetShipNPC().ResetShipStat(true);

            foreach (List<ItemSlotButton> itemSlotButtons in componentButtons)       //计算DPS
            {
                foreach (ItemSlotButton button in itemSlotButtons)
                {
                    if (button.ItemType != "" && LibraryHelpers.IsWeapon(button.EquipType))
                    {
                        if (button.EquipType == ComponentTypes.Weapon_H)
                        {
                            TotalDPS += (EverythingLibrary.Components[button.ItemType] as BaseWeaponComponent).DPS(dummyShip.GetShipNPC().StaticBuff) * (1 + dummyShip.GetShipNPC().ExtraStriker / 4f);
                        }
                        else
                        {
                            TotalDPS += (EverythingLibrary.Components[button.ItemType] as BaseWeaponComponent).DPS(dummyShip.GetShipNPC().StaticBuff);
                        }
                    }
                }
            }

            int CP = EverythingLibrary.Ships[shipType].CP;
            int Hull = dummyShip.lifeMax - dummyShip.GetShipNPC().MaxShield;
            int Defense = dummyShip.defense;
            int Shield = dummyShip.GetShipNPC().MaxShield;
            float ShieldRegen = (float)Math.Round(dummyShip.GetShipNPC().ShieldRegen);
            float HullRegen = (float)Math.Round(dummyShip.GetShipNPC().HullRegen);
            float Evasion = (float)Math.Round(dummyShip.GetShipNPC().Evasion, 1);
            float MaxSpeed = (float)Math.Round(dummyShip.GetShipNPC().MaxSpeed * 20);
            int DetectRange = dummyShip.GetShipNPC().DetectRange;
            float Aggro = dummyShip.GetShipNPC().Aggro;
            TotalDPS = (float)Math.Round(TotalDPS, 1);
            string profession = Language.GetTextValue("Mods.StellarisShips.ComputerType." + dummyShip.GetShipNPC().ComputerType);
            string result = string.Format(Language.GetTextValue("Mods.StellarisShips.UI.ShipStat"), CP, Hull, Defense, Shield, HullRegen, ShieldRegen, Evasion, MaxSpeed, DetectRange, Aggro, TotalDPS, profession, MoneyHelpers.ShowCoins(Value, ValueMR));
            return result;
        }

    }
}

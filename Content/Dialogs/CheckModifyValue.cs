using Microsoft.Xna.Framework;
using StellarisShips.Content.NPCs;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Dialogs
{
    public class CheckModifyValue : BaseDialog
    {
        public override string InternalName => "CheckModifyValue";

        public override List<string> ButtonNames => new()
        {
            "OK",
            "ReChoose",
            "Return",
            "Bye"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Yes",
            "Restart",
            "No",
            "Bye"
        };

        public override void SetUp()
        {
            ShipBuildUI.TalkText = GetDialogLocalize("NoShipModify");
        }

        public override void Update()
        {
            long values = 0;
            int count = 0;
            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive())
                {
                    ShipNPC shipNPC = ship.GetShipNPC();
                    if (shipNPC.ShipGraph.GraphName == ShipBuildUI.shipGraph.GraphName && shipNPC.ShipGraph.ShipType == ShipBuildUI.shipGraph.ShipType)
                    {
                        count++;//血没满的船按比例减少等效价格
                        long realValue = (long)(ship.life / (float)ship.lifeMax * shipNPC.ShipGraph.Value);
                        if (ShipBuildUI.shipGraph.Value - realValue > 0)
                            values += ShipBuildUI.shipGraph.Value - realValue;
                    }
                }
            }
            ShipBuildUI.Value = values;
            bool enabled = false;
            if (count > 0 && Main.LocalPlayer.CanAfford(ShipBuildUI.Value))
            {
                enabled = true;
            }
            ShipBuildUI.talkButtons[0].Enabled = enabled;

            if (count == 0)
            {
                ShipBuildUI.TalkText = GetDialogLocalize("NoShipModify");
            }
            else
            {
                ShipBuildUI.TalkText = string.Format(GetDialogLocalize("CheckModifyValue"),
                    string.Format(Language.GetTextValue("Mods.StellarisShips.UI.GraphNameUI"), ShipBuildUI.shipGraph.GraphName, EverythingLibrary.Ships[ShipBuildUI.shipGraph.ShipType].GetLocalizedName()),
                    count, MoneyHelpers.ShowCoins(ShipBuildUI.Value));
            }
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Yes":
                    Main.LocalPlayer.BuyItem(ShipBuildUI.Value);
                    ModifyShip();
                    SomeUtils.PlaySound(SoundPath.UI + "BuildShip");
                    ShipBuildUI.Start("ModifySuccess");
                    break;
                case "Restart":
                    ShipBuildUI.Start("SelectShipModify");
                    break;
                case "No":
                    ShipBuildUI.Start("NormalStart");
                    break;
                case "Bye":
                    ShipBuildUI.AllClear(true);
                    UIManager.ShipBuildVisible = false;
                    break;
            }

        }

        private void ModifyShip()
        {
            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive())
                {
                    ShipNPC shipNPC = ship.GetShipNPC();
                    if (shipNPC.ShipGraph.GraphName == ShipBuildUI.shipGraph.GraphName && shipNPC.ShipGraph.ShipType == ShipBuildUI.shipGraph.ShipType)
                    {
                        float scale = EverythingLibrary.Ships[ShipBuildUI.shipGraph.ShipType].Length / 70f;
                        FTLLight.Summon(ship.GetSource_FromAI(), ship.Center, scale);
                        ship.active = false;
                        Vector2 GivePos = ShapeSystem.TipPos + new Vector2(Main.rand.Next(2000) - 1000, -1000);
                        ShipNPC.BuildAShip(Main.LocalPlayer.GetSource_GiftOrReward(), GivePos, ShipBuildUI.shipGraph);

                        FTLLight.Summon(ship.GetSource_FromAI(), GivePos, scale);
                        SomeUtils.PlaySound(SoundPath.Other + "FTL", GivePos);
                    }
                }
            }
        }
    }
}

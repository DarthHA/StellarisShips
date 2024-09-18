using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Dialogs
{
    public class CheckRepairValue : BaseDialog
    {
        public override string InternalName => "CheckRepairValue";

        public override List<string> ButtonNames => new()
        {
            "OK",
            "Return",
            "Bye"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Yes",
            "No",
            "Bye"
        };

        public override void SetUp()
        {
            ShipBuildUI.TalkText = GetDialogLocalize("NoShipRepair");
        }

        public override void Update()
        {
            long values = 0;
            int count = 0;
            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive())
                {
                    if (ship.life < ship.lifeMax)
                    {
                        ShipNPC shipNPC = ship.GetShipNPC();
                        count++;
                        long realValue = (long)((1f - ship.life / (float)ship.lifeMax) * EverythingLibrary.Ships[shipNPC.ShipGraph.ShipType].Value);
                        values += realValue;
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
                ShipBuildUI.TalkText = GetDialogLocalize("NoShipRepair");
            }
            else
            {
                ShipBuildUI.TalkText = string.Format(GetDialogLocalize("CheckRepairValue"), count, MoneyHelpers.ShowCoins(ShipBuildUI.Value));
            }
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Yes":
                    RepairAllShip();
                    ShipBuildUI.Start("RepairSuccess");
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

        private static void RepairAllShip()
        {
            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive())
                {
                    ship.life = ship.lifeMax;
                    ship.GetShipNPC().CurrentShield = ship.GetShipNPC().MaxShield;

                }
            }
            Main.LocalPlayer.BuyItem(ShipBuildUI.Value);
            SomeUtils.PlaySound(SoundPath.UI + "Repair");
        }
    }
}

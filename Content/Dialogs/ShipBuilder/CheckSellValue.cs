using StellarisShips.Content.NPCs;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;

namespace StellarisShips.Content.Dialogs.ShipBuilder
{
    public class CheckSellValue : BaseDialog
    {
        public override string InternalName => "CheckSellValue";

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
            ShipBuildUI.TalkText = GetDialogLocalize("NoShipSell");
        }

        public override void Update()
        {
            long values = 0;
            int valuesMR = 0;
            int count = 0;
            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive())
                {
                    ShipNPC shipNPC = ship.GetShipNPC();
                    if (shipNPC.ShipGraph.GraphName == ShipBuildUI.shipGraph.GraphName && shipNPC.ShipGraph.ShipType == ShipBuildUI.shipGraph.ShipType)
                    {
                        count++;
                        values += EverythingLibrary.Ships[shipNPC.ShipGraph.ShipType].Value;
                        valuesMR += shipNPC.ShipGraph.MRValue;
                    }
                }
            }
            ShipBuildUI.Value = values;
            ShipBuildUI.MRValue = valuesMR;
            bool enabled = false;
            if (count > 0)
            {
                enabled = true;
            }
            ShipBuildUI.talkButtons[0].Enabled = enabled;

            if (count == 0)
            {
                ShipBuildUI.TalkText = GetDialogLocalize("NoShipSell");
            }
            else
            {
                ShipBuildUI.TalkText = string.Format(GetDialogLocalize("CheckSellValue"),
                    string.Format(Language.GetTextValue("Mods.StellarisShips.UI.GraphNameUI"), ShipBuildUI.shipGraph.GraphName, EverythingLibrary.Ships[ShipBuildUI.shipGraph.ShipType].GetLocalizedName())
                    , count, MoneyHelpers.ShowCoins(ShipBuildUI.Value, ShipBuildUI.MRValue));
            }
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Yes":
                    SellShip();
                    ShipBuildUI.Start("SellSuccess");
                    break;
                case "Restart":
                    ShipBuildUI.Start("SelectShipSell");
                    break;
                case "No":
                    ShipBuildUI.Start("NormalStart");
                    break;
                case "Bye":
                    ShipBuildUI.Close();
                    break;
            }

        }

        private static void SellShip()
        {
            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive())
                {
                    ShipNPC shipNPC = ship.GetShipNPC();
                    if (shipNPC.ShipGraph.GraphName == ShipBuildUI.shipGraph.GraphName && shipNPC.ShipGraph.ShipType == ShipBuildUI.shipGraph.ShipType)
                    {
                        float scale = EverythingLibrary.Ships[ShipBuildUI.shipGraph.ShipType].Length / 70f;
                        FTLLight2.Summon(ship.GetSource_FromAI(), ship.Center, scale);
                        SomeUtils.PlaySound(SoundPath.Other + "FTL", ship.Center);

                        ship.active = false;
                    }
                }
            }
            Main.LocalPlayer.GiveMoney(ShipBuildUI.Value);
            SoundEngine.PlaySound(SoundID.Coins);
        }

    }
}

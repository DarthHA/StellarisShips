

using StellarisShips.Content.Items;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.Dialogs
{
    public class SelectShipSell : BaseDialog
    {
        public override string InternalName => "SelectShipSell";

        public override List<string> ButtonNames => new()
        {
            "SellShip1",
            "SellShip2",
            "Return",
            "Bye"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Yes",
            "All",
            "No",
            "Bye"
        };

        public override void SetUp()
        {
            ShipBuildUI.TalkText = GetDialogLocalize("SelectShipSell");
        }

        public override void Update()
        {
            bool enabled1 = false; bool enabled2 = true;
            if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<GraphItem>())
            {
                if ((Main.LocalPlayer.HeldItem.ModItem as GraphItem).graph.ShipType != "")
                {
                    foreach (NPC ship in Main.ActiveNPCs)
                    {
                        if (ship.ShipActive())
                        {
                            if (ship.GetShipNPC().ShipGraph.GraphName == (Main.LocalPlayer.HeldItem.ModItem as GraphItem).graph.GraphName)
                            {
                                enabled1 = true;
                                break;
                            }
                        }
                    }
                }
            }
            bool AnyShip = false;
            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive())
                {
                    AnyShip = true;
                    break;
                }
            }
            if (!AnyShip)
            {
                enabled1 = false;
                enabled2 = false;
            }

            ShipBuildUI.talkButtons[0].Enabled = enabled1;
            ShipBuildUI.talkButtons[1].Enabled = enabled2;
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Yes":
                    ShipBuildUI.shipGraph = (Main.LocalPlayer.HeldItem.ModItem as GraphItem).graph.Copy();
                    ShipBuildUI.Value = ShipBuildUI.shipGraph.Value;
                    ShipBuildUI.MRValue = ShipBuildUI.shipGraph.MRValue;
                    ShipBuildUI.Start("CheckSellValue");
                    break;
                case "All":
                    ShipBuildUI.Start("CheckSellValue2");
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

    }
}
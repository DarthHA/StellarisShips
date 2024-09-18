using StellarisShips.Content.Items;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.Dialogs.ShipBuilder
{
    public class SelectShipBuild : BaseDialog
    {
        public override string InternalName => "SelectShipBuild";

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
            ShipBuildUI.TalkText = GetDialogLocalize("SelectShipBuild");
        }

        public override void Update()
        {
            bool enabled = false;
            if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<GraphItem>())
            {
                if ((Main.LocalPlayer.HeldItem.ModItem as GraphItem).graph.ShipType != "")
                {
                    enabled = true;
                }
            }
            ShipBuildUI.talkButtons[0].Enabled = enabled;
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Yes":
                    ShipBuildUI.shipGraph = (Main.LocalPlayer.HeldItem.ModItem as GraphItem).graph.Copy();
                    ShipBuildUI.Value = ShipBuildUI.shipGraph.Value;
                    ShipBuildUI.MRValue = ShipBuildUI.shipGraph.MRValue;
                    ShipBuildUI.Start("CheckBuildValue");
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
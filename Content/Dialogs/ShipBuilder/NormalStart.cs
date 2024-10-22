using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Dialogs.ShipBuilder
{
    public class NormalStart : BaseDialog
    {
        public override string InternalName => "NormalStart";

        public override List<string> ButtonNames => new()
        {
            "WantBuild",
            "WantGraph",
            "WantRepair",
            "WantModify",
            "WantSell",
            "Bye"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Build",
            "Graph",
            "Repair",
            "Modify",
            "Sell",
            "Bye"
        };

        public override void SetUp()
        {
            ShipBuildUI.TalkText = GetDialogLocalize("NormalChat" + Main.rand.Next(4).ToString());
        }

        public override void Update()
        {

        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Build":
                    ShipBuildUI.Start("SelectShipBuild");
                    break;
                case "Graph":
                    ShipBuildUI.Start("GetGraph");
                    break;
                case "Repair":
                    ShipBuildUI.Start("CheckRepairValue");
                    break;
                case "Modify":
                    ShipBuildUI.Start("SelectShipModify");
                    break;
                case "Sell":
                    ShipBuildUI.Start("SelectShipSell");
                    break;
                case "Bye":
                    ShipBuildUI.Close();
                    break;
            }

        }
    }
}
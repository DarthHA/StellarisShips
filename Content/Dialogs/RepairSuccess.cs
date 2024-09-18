using StellarisShips.Static;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;

namespace StellarisShips.Content.Dialogs
{
    public class RepairSuccess : BaseDialog
    {
        public override string InternalName => "RepairSuccess";

        public override List<string> ButtonNames => new()
        {
            "Return",
            "Bye"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "No",
            "Bye"
        };

        public override void SetUp()
        {
            ShipBuildUI.TalkText = string.Format(GetDialogLocalize("RepairSuccess"), MoneyHelpers.ShowCoins(ShipBuildUI.Value));
        }

        public override void Update()
        {
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
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

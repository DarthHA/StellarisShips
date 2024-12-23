﻿using StellarisShips.Static;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;

namespace StellarisShips.Content.Dialogs.ShipBuilder
{
    public class SellSuccess : BaseDialog
    {
        public override string InternalName => "SellSuccess";

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
            ShipBuildUI.TalkText = string.Format(GetDialogLocalize("SellSuccess"), MoneyHelpers.ShowCoins(ShipBuildUI.Value, ShipBuildUI.MRValue));
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

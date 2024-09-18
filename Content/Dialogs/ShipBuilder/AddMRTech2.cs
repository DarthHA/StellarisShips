using StellarisShips.System;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;

namespace StellarisShips.Content.Dialogs.ShipBuilder
{
    public class AddMRTech : BaseDialog
    {
        public override string InternalName => "AddMRTech2";

        public override List<string> ButtonNames => new()
        {
            "Nice",
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Nice",
        };

        public override void SetUp()
        {
            ShipBuildUI.TalkText = GetDialogLocalize("AddMRTech2");
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Nice":
                    ProgressHelper.DiscoveredMR++;
                    ShipBuildUI.Start("NormalStart");
                    break;
            }

        }
    }
}

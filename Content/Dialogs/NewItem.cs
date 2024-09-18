using StellarisShips.System;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;

namespace StellarisShips.Content.Dialogs
{
    public class NewItem : BaseDialog
    {
        public override string InternalName => "NewItem";

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
            if (ProgressHelper.GetCurrentProgress() == 2 || ProgressHelper.GetCurrentProgress() == 5 || ProgressHelper.GetCurrentProgress() == 8)
            {
                ShipBuildUI.TalkText = GetDialogLocalize("NewShip");
            }
            else if (ProgressHelper.GetCurrentProgress() == 4 || ProgressHelper.GetCurrentProgress() == 7)
            {
                ShipBuildUI.TalkText = GetDialogLocalize("NewCP");
            }
            else
            {
                ShipBuildUI.TalkText = GetDialogLocalize("NewTech");
            }
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Nice":
                    ShipBuildUI.Start("NormalStart");
                    break;
            }

        }
    }
}

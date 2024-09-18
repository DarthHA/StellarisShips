using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;

namespace StellarisShips.Content.Dialogs.Shroud
{
    public class ShroudFirstContract : BaseDialog
    {
        public override string InternalName => "ShroudFirstContract";

        public override List<string> ButtonNames => new()
        {
            "Exit"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Exit"
        };

        public override void SetUp()
        {
            ShroudUI.ShowInfo = false;
            ShroudUI.TalkText = GetDialogLocalize("ShroudFirstContract");
        }

        public override void Update()
        {
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Exit":
                    ShroudUI.AllClear(true);
                    UIManager.ShroudVisible = false;
                    break;
            }

        }
    }
}

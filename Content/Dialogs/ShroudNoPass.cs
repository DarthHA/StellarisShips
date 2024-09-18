using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;

namespace StellarisShips.Content.Dialogs
{
    public class ShroudNoPass : BaseDialog
    {
        public override string InternalName => "ShroudNoPass";

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
            ShroudUI.TalkText = "不让你进去";
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

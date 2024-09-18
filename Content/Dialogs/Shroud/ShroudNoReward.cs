using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Dialogs.Shroud
{
    public class ShroudNoReward : BaseDialog
    {
        public override string InternalName => "ShroudNoReward";

        public override List<string> ButtonNames => new()
        {
            "ExitShroud"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Exit"
        };

        public override void SetUp()
        {
            int num = Main.rand.Next(3) + 1;
            ShroudUI.TalkText = GetDialogLocalize("ShroudNoReward" + num.ToString());
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

using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;

namespace StellarisShips.Content.Dialogs.Shroud
{
    public class RewardPsiComputer1 : BaseDialog
    {
        public override string InternalName => "RewardPsiComputer1";

        public override List<string> ButtonNames => new()
        {
            "RewardPsiComputer"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Reward"
        };

        public override void SetUp()
        {
            ShroudUI.TalkText = GetDialogLocalize("RewardPsiComputer1");
        }

        public override void Update()
        {
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Reward":
                    ShroudUI.Start("RewardPsiComputer2");
                    break;
            }

        }
    }
}

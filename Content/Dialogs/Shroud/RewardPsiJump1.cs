using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;

namespace StellarisShips.Content.Dialogs.Shroud
{
    public class RewardPsiJump1 : BaseDialog
    {
        public override string InternalName => "RewardPsiJump1";

        public override List<string> ButtonNames => new()
        {
            "RewardPsiJump"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Reward"
        };

        public override void SetUp()
        {
            ShroudUI.TalkText = GetDialogLocalize("RewardPsiJump1");
        }

        public override void Update()
        {
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Reward":
                    ShroudUI.Start("RewardPsiJump2");
                    break;
            }

        }
    }
}

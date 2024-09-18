using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;

namespace StellarisShips.Content.Dialogs.Shroud
{
    public class RewardPsiShield1 : BaseDialog
    {
        public override string InternalName => "RewardPsiShield1";

        public override List<string> ButtonNames => new()
        {
            "RewardPsiShield"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Reward"
        };

        public override void SetUp()
        {
            ShroudUI.TalkText = GetDialogLocalize("RewardPsiShield1");
        }

        public override void Update()
        {
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Reward":
                    ShroudUI.Start("RewardPsiShield2");
                    break;
            }

        }
    }
}

using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;

namespace StellarisShips.Content.Dialogs.Shroud
{
    public class RewardTech1 : BaseDialog
    {
        public override string InternalName => "RewardTech1";

        public override List<string> ButtonNames => new()
        {
            "RewardTech"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Reward"
        };

        public override void SetUp()
        {
            ShroudUI.TalkText = GetDialogLocalize("RewardTech1");
        }

        public override void Update()
        {
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Reward":
                    ShroudUI.Start("RewardTech2");
                    break;
            }

        }
    }
}

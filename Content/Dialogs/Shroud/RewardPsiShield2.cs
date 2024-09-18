using StellarisShips.System;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;

namespace StellarisShips.Content.Dialogs.Shroud
{
    public class RewardPsiShield2 : BaseDialog
    {
        public override string InternalName => "RewardPsiShield2";

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
            ShroudUI.TalkText = GetDialogLocalize("RewardPsiShield2");
        }

        public override void Update()
        {
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Exit":
                    ProgressHelper.UnlockTech.Add("PsiShield");
                    ShroudUI.AllClear(true);
                    UIManager.ShroudVisible = false;
                    break;
            }

        }
    }
}

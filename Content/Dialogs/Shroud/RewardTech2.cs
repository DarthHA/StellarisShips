using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.Utilities;

namespace StellarisShips.Content.Dialogs.Shroud
{
    public class RewardTech2 : BaseDialog
    {
        public override string InternalName => "RewardTech2";

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
            List<string> targetList = LibraryHelpers.GetLockedTech();
            targetList.Remove("PsiShield");
            targetList.Remove("PsiJump");
            targetList.Remove("PsiComputer");
            ShroudUI.ExtraInfo = targetList[Main.rand.Next(targetList.Count)];
            int num = Main.rand.Next(3) + 1;
            ShroudUI.TalkText = string.Format(GetDialogLocalize("RewardTech2" + num.ToString()), Language.GetTextValue("Mods.StellarisShips.UnlockTech." + ShroudUI.ExtraInfo));
        }

        public override void Update()
        {
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Exit":
                    ProgressHelper.UnlockTech.Add(ShroudUI.ExtraInfo);
                    ShroudUI.Exit();
                    break;
            }

        }
    }
}

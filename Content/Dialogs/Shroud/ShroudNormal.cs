using StellarisShips.System;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.Utilities;

namespace StellarisShips.Content.Dialogs.Shroud
{
    public class ShroudNormal : BaseDialog
    {
        public override string InternalName => "ShroudNormal";

        public override List<string> ButtonNames => new()
        {
            "EnterShroud",
            "ExitShroud"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Enter",
            "Exit"
        };

        public override void SetUp()
        {
            int num = Main.rand.Next(19) + 1;
            ShroudUI.TalkText = GetDialogLocalize("ShroudNormal" + num.ToString());
        }

        public override void Update()
        {
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Enter":
                    if (AnyTechUnlock() && Main.rand.NextBool(5))           //20%几率出科技
                    {
                        WeightedRandom<string> weightedRandom = new();
                        if (!ProgressHelper.UnlockTech.Contains("PsiShield"))
                        {
                            weightedRandom.Add("RewardPsiShield1", 1);
                        }
                        if (!ProgressHelper.UnlockTech.Contains("PsiJump"))
                        {
                            weightedRandom.Add("RewardPsiJump1", 1);
                        }
                        if (!ProgressHelper.UnlockTech.Contains("PsiComputer"))
                        {
                            weightedRandom.Add("RewardPsiComputer1", 1);
                        }
                        ShroudUI.Start(weightedRandom.Get());
                    }
                    else
                    {
                        WeightedRandom<string> weightedRandom = new();
                        weightedRandom.Add("ShroudNoReward", 1);
                        weightedRandom.Add("RewardSelectRandom", 4);
                        ShroudUI.Start(weightedRandom.Get());
                    }
                    ProgressHelper.PsychoPower = 0;
                    break;
                case "Exit":
                    ShroudUI.AllClear(true);
                    UIManager.ShroudVisible = false;
                    break;
            }

        }
        internal bool AnyTechUnlock() => !ProgressHelper.UnlockTech.Contains("PsiShield") || !ProgressHelper.UnlockTech.Contains("PsiJump") || !ProgressHelper.UnlockTech.Contains("PsiComputer");
    }
}

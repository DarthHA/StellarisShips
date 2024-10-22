using StellarisShips.Static;
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
                        List<string> targetList = LibraryHelpers.GetLockedTech();
                        foreach(string tech in targetList)
                        {
                            switch (tech)
                            {
                                case "PsiShield":
                                    weightedRandom.Add("RewardPsiShield1", 1);
                                    break;
                                case "PsiJump":
                                    weightedRandom.Add("RewardPsiJump1", 1);
                                    break;
                                case "PsiComputer":
                                    weightedRandom.Add("RewardPsiComputer1", 1);
                                    break;
                                default:
                                    weightedRandom.Add("RewardTech1", 1);
                                    break;
                            }
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
                    ShroudUI.Exit();
                    break;
            }

        }
        internal static bool AnyTechUnlock() => ProgressHelper.UnlockTech.Count < EverythingLibrary.LockedTech.Count;
    }
}

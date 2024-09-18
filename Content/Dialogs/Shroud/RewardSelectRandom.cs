using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Dialogs.Shroud
{
    public class RewardSelectRandom : BaseDialog
    {
        public override string InternalName => "RewardSelectRandom";

        public override List<string> ButtonNames => new()
        {
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Item1",
            "Item2",
            "Item3",
        };

        public override void SetUp()
        {
            ShroudUI.TalkText = GetDialogLocalize("RewardSelectRandom");
            List<string> rewards = new() { "ASPDUp", "AtkUp", "EvasionUp", "RegenUp", "ShieldUp", "SpeedUp" };
            for(int i = 0; i < 3; i++)
            {
                string s = rewards[Main.rand.Next(rewards.Count)];
                ShroudUI.talkButtons[i].InternalText = "Reward" + s;
                ShroudUI.talkButtons[i].ShowText = "Boon" + s;
                rewards.Remove(s);   
            }
        }

        public override void Update()
        {
        }

        public override void ClickEvent(string internalStr)
        {
            ShroudUI.Start(internalStr);
        }
    }
}

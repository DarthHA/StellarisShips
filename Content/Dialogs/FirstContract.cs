using StellarisShips.Static;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace StellarisShips.Content.Dialogs
{
    public class FirstContract : BaseDialog
    {
        public override string InternalName => "FirstContract";
        const int GiveValue = 50000;

        public override List<string> ButtonNames => new()
        {
            "Hello",
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Yes",
        };

        public override void SetUp()
        {
            SoundEngine.PlaySound(SoundID.Coins);
            ShipBuildUI.ShowInfo = false;
            ShipBuildUI.TalkText = GetDialogLocalize("FirstContract" + Main.rand.Next(3).ToString());
            Main.LocalPlayer.GiveMoney(GiveValue);
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Yes":
                    ShipBuildUI.Start("NormalStart");
                    break;
            }

        }
    }
}

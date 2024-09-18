using StellarisShips.Content.Items;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.Dialogs
{
    public class AddMRTech1 : BaseDialog
    {
        public override string InternalName => "AddMRTech1";

        public override List<string> ButtonNames => new()
        {
            "Try",
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "OK",
        };

        public override void SetUp()
        {
            ShipBuildUI.TalkText = GetDialogLocalize("AddMRTech1");
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "OK":
                    Main.LocalPlayer.ConsumeItem(ModContent.ItemType<Rubricator>());
                    ProgressHelper.DiscoveredMR++;
                    ShipBuildUI.Start("NormalStart");
                    break;
            }

        }
    }
}

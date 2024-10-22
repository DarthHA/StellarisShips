using StellarisShips.Content.Items;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Dialogs.ShipBuilder
{
    public class GetGraph : BaseDialog
    {
        public override string InternalName => "GetGraph";
        const int GraphValue = 500;

        public override List<string> ButtonNames => new()
        {
            "BuyGraph",
            "Return",
            "Bye"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Yes",
            "No",
            "Bye"
        };

        public override void SetUp()
        {
            ShipBuildUI.TalkText = string.Format(GetDialogLocalize("SellGraph"), MoneyHelpers.ShowCoins(GraphValue));
        }

        public override void Update()
        {
            bool enabled = false;
            if (Main.LocalPlayer.CanAfford(GraphValue))
            {
                enabled = true;
            }
            ShipBuildUI.talkButtons[0].Enabled = enabled;
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Yes":
                    ShipBuildUI.TalkText = string.Format(GetDialogLocalize("SellGraphSuccess"), MoneyHelpers.ShowCoins(GraphValue));
                    SoundEngine.PlaySound(SoundID.Coins);
                    Main.LocalPlayer.BuyItem(GraphValue);
                    Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ModContent.ItemType<GraphItem>());
                    break;
                case "No":
                    ShipBuildUI.Start("NormalStart");
                    break;
                case "Bye":
                    ShipBuildUI.Close();
                    break;
            }

        }
    }
}

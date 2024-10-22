using Microsoft.Xna.Framework;
using StellarisShips.Content.NPCs;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.Content.Dialogs.ShipBuilder
{
    public class CheckBuildValue : BaseDialog
    {
        public override string InternalName => "CheckBuildValue";

        public override List<string> ButtonNames => new()
        {
            "OK",
            "ReChoose",
            "Return",
            "Bye"
        };

        public override List<string> ButtonInternalStrs => new()
        {
            "Yes",
            "Restart",
            "No",
            "Bye"
        };

        public override void SetUp()
        {
            ShipBuildUI.TalkText = string.Format(GetDialogLocalize("CheckBuildValue"),
                string.Format(Language.GetTextValue("Mods.StellarisShips.UI.GraphNameUI"), ShipBuildUI.shipGraph.GraphName, EverythingLibrary.Ships[ShipBuildUI.shipGraph.ShipType].GetLocalizedName()),
MoneyHelpers.ShowCoins(ShipBuildUI.Value, ShipBuildUI.MRValue), EverythingLibrary.Ships[ShipBuildUI.shipGraph.ShipType].CP);
        }

        public override void Update()
        {
            bool enabled = false;
            if (Main.LocalPlayer.CanAfford(ShipBuildUI.Value))
            {
                enabled = true;
            }
            if (EverythingLibrary.Ships[ShipBuildUI.shipGraph.ShipType].CP > ProgressHelper.GetMaxCommandPoint() - MoneyHelpers.GetCurrentCommandPoint())
            {
                enabled = false;
            }
            if (ShipBuildUI.shipGraph.MRValue > ProgressHelper.GetMaxMinorArtifact() - MoneyHelpers.GetCurrentMR())
            {
                enabled = false;
            }
            int count = 0;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<ShipNPC>())
                {
                    if (npc.GetShipNPC().ShipGraph.ShipType == ShipBuildUI.shipGraph.ShipType)
                    {
                        count++;
                    }
                }
            }
            if (count >= EverythingLibrary.Ships[ShipBuildUI.shipGraph.ShipType].MaxCount)
            {
                enabled = false;
            }
            if (ShipBuildUI.shipGraph.MRValue > ProgressHelper.GetMaxMinorArtifact() - MoneyHelpers.GetCurrentMR())
            {
                enabled = false;
            }
            ShipBuildUI.talkButtons[0].Enabled = enabled;
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Yes":
                    Vector2 GivePos = FleetSystem.TipPos + new Vector2(Main.rand.Next(2000) - 1000, -1000);
                    ShipNPC.BuildAShip(Main.LocalPlayer.GetSource_GiftOrReward(), GivePos, ShipBuildUI.shipGraph, SomeUtils.GetRandomName());

                    string name = string.Format(Language.GetTextValue("Mods.StellarisShips.UI.GraphNameUI"), ShipBuildUI.shipGraph.GraphName, EverythingLibrary.Ships[ShipBuildUI.shipGraph.ShipType].GetLocalizedName());
                    if (!BattleStatSystem.ShipInBattle.TryAdd(name, 1))
                    {
                        BattleStatSystem.ShipInBattle[name] += 1;
                    }

                    float scale = EverythingLibrary.Ships[ShipBuildUI.shipGraph.ShipType].Length / 70f;
                    FTLLight.Summon(Main.LocalPlayer.GetSource_ReleaseEntity(), GivePos, scale);
                    SomeUtils.PlaySound(SoundPath.Other + "FTL", GivePos);

                    Main.LocalPlayer.BuyItem(ShipBuildUI.Value);
                    ShipBuildUI.TalkText = string.Format(GetDialogLocalize("BuildSuccess"),
                        string.Format(Language.GetTextValue("Mods.StellarisShips.UI.GraphNameUI"), ShipBuildUI.shipGraph.GraphName, EverythingLibrary.Ships[ShipBuildUI.shipGraph.ShipType].GetLocalizedName()),
MoneyHelpers.ShowCoins(ShipBuildUI.Value, ShipBuildUI.MRValue));
                    SomeUtils.PlaySound(SoundPath.UI + "BuildShip");
                    break;
                case "Restart":
                    ShipBuildUI.Start("SelectShipBuild");
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

﻿using StellarisShips.Content.Buffs;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using StellarisShips.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.Dialogs.Shroud
{
    public class RewardAtkUp : BaseDialog
    {
        public override string InternalName => "RewardAtkUp";

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
            ShroudUI.TalkText = GetDialogLocalize("RewardAtkUp");
        }

        public override void Update()
        {
        }

        public override void ClickEvent(string internalStr)
        {
            switch (internalStr)
            {
                case "Exit":
                    Main.LocalPlayer.GetModPlayer<ShipControlPlayer>().CurrentShroudBuffs = ModifierID.ShroudAtkUp;
                    Main.LocalPlayer.AddBuff(ModContent.BuffType<ShroudBuff>(), 60 * 60 * 10);
                    ShroudUI.Close();
                    break;
            }

        }
    }
}
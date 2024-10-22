using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Core
{
    public class FTLDrive1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.FTLDrive;
        public override int Level => 1;
        public override string TypeName => "FTLDrive";
        public virtual int FTLCooldown => 60 * 60;
        public virtual float EscapeChance => 0f;
        public override long Value => 0 * 400;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().StaticBuff.AddBonusLevel(BonusID.FTLLevel, Level);
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.EscapeChance, EscapeChance);
            ship.GetShipNPC().StaticBuff.SetBonus(BonusID.FTLMaxCooldown, FTLCooldown);
        }

        public override void ModifyDesc(ref string desc)
        {
            if (Level > 1)
            {
                desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.FTL"), FTLCooldown / 60, Math.Round(EscapeChance * 100, 1), Level * 30);
            }
        }
    }

    public class FTLDrive2 : FTLDrive1
    {
        public override int Level => 2;
        public override int FTLCooldown => 60 * 60;
        public override float EscapeChance => 0.2f;
        public override long Value => 5 * 400;
        public override int Progress => 3;
    }

    public class FTLDrive3 : FTLDrive1
    {
        public override int Level => 3;
        public override int FTLCooldown => 45 * 60;
        public override float EscapeChance => 0.4f;
        public override long Value => 10 * 400;
        public override int Progress => 4;
    }

    public class FTLDrive4 : FTLDrive1
    {
        public override int Level => 4;
        public override int FTLCooldown => 30 * 60;
        public override float EscapeChance => 0.6f;
        public override long Value => 15 * 400;
        public override int Progress => 6;
    }

    public class FTLDrive5 : FTLDrive1
    {
        public override int Level => 5;
        public override int FTLCooldown => 10 * 60;
        public override float EscapeChance => 0.7f;
        public override long Value => 20 * 400;
        public override int Progress => 8;
    }

    public class FTLDrive6 : FTLDrive1
    {
        public override int Level => 6;
        public override int FTLCooldown => 5 * 60;
        public override float EscapeChance => 0.8f;
        public override long Value => 25 * 400;
        public override int Progress => 9;
        public override string SpecialUnLock => "PsiJump";
    }
}
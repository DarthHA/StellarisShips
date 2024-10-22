using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Shield
{
    public class ArchShieldS : BaseComponent
    {
        public override string EquipType => ComponentTypes.Defense_S;
        public override int Level => 1;
        public override string TypeName => "ArchShield";
        public override string ExtraInfo => "SML";
        public int Shield => 864;
        public float ShieldRegen => 18;
        public float ShieldDR = 0.1f;
        public override long Value => 35 * 400;
        public override int MRValue => 20;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.Shield, Shield);
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.ShieldRegen, ShieldRegen);
            ship.GetShipNPC().StaticBuff.AddBonusShieldDR(BonusID.ShieldDR, ShieldDR);
            ship.GetShipNPC().StaticBuff.AddBonusLevel(BonusID.ShieldDRLevel, 3);
        }
        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetOrRegister("Mods.StellarisShips.ExtraDesc.ShieldPlus2").Value, Shield, ShieldRegen, Math.Round(ShieldDR * 100f, 1));
        }
    }

    public class ArchShieldM : BaseComponent
    {
        public override string EquipType => ComponentTypes.Defense_M;
        public override int Level => 1;
        public override string TypeName => "ArchShield";
        public override string ExtraInfo => "SML";
        public int Shield => 2160;
        public float ShieldRegen => 27.9f;
        public float ShieldDR = 0.1f;
        public override long Value => 52 * 600;
        public override int MRValue => 40;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().MaxShield += Shield;
            ship.GetShipNPC().ShieldRegen += ShieldRegen;
            ship.GetShipNPC().ShieldDR = 1 - (1 - ship.GetShipNPC().ShieldDR) * (1 - ShieldDR);
            ship.GetShipNPC().ShieldDRLevel = Math.Max(ship.GetShipNPC().ShieldDRLevel, 3);
        }
        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetOrRegister("Mods.StellarisShips.ExtraDesc.ShieldPlus2").Value, Shield, ShieldRegen, Math.Round(ShieldDR * 100f, 1));
        }
    }

    public class ArchShieldL : BaseComponent
    {
        public override string EquipType => ComponentTypes.Defense_L;
        public override int Level => 1;
        public override string TypeName => "ArchShield";
        public override string ExtraInfo => "SML";
        public int Shield => 5184;
        public float ShieldRegen => 66.6f;
        public float ShieldDR = 0.1f;
        public override long Value => 68 * 800;
        public override int MRValue => 80;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().MaxShield += Shield;
            ship.GetShipNPC().ShieldRegen += ShieldRegen;
            ship.GetShipNPC().ShieldDR = 1 - (1 - ship.GetShipNPC().ShieldDR) * (1 - ShieldDR);
            ship.GetShipNPC().ShieldDRLevel = Math.Max(ship.GetShipNPC().ShieldDRLevel, 3);
        }
        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetOrRegister("Mods.StellarisShips.ExtraDesc.ShieldPlus2").Value, Shield, ShieldRegen, Math.Round(ShieldDR * 100f, 1));
        }
    }
}

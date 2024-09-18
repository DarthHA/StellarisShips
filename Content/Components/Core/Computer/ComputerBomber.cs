using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Core.Computer
{
    public class ComputerBomber1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Computer;
        public override int Level => 1;
        public override string TypeName => "ComputerBomber";

        public virtual float DamageS => 0.05f;
        public virtual float DamageG => 0.15f;
        public virtual float CritS => 5f;
        public virtual float CritG => 15f;
        public override long Value => 0 * 200;

        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().ComputerType = TypeName;
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.WeaponDamage_S, DamageS);
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.WeaponDamage_G, DamageG);
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.WeaponCrit_S, CritS);
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.WeaponCrit_G, CritG);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.ComputerBomber"),
                Math.Round(DamageS * 100f, 1), CritS, Math.Round(DamageG * 100f, 1), CritG);
        }
    }

    public class ComputerBomber2 : ComputerBomber1
    {
        public override int Level => 2;
        public override float DamageS => 0.1f;
        public override float DamageG => 0.2f;
        public override float CritS => 10f;
        public override float CritG => 20f;
        public override long Value => 5 * 200;
        public override int Progress => 3;
    }

    public class ComputerBomber3 : ComputerBomber1
    {
        public override int Level => 3;
        public override float DamageS => 0.15f;
        public override float DamageG => 0.25f;
        public override float CritS => 15f;
        public override float CritG => 25f;
        public override long Value => 10 * 200;
        public override int Progress => 5;
    }

    public class ComputerBomber4 : ComputerBomber1
    {
        public override int Level => 4;
        public override float DamageS => 0.2f;
        public override float DamageG => 0.3f;
        public override float CritS => 20f;
        public override float CritG => 30f;
        public override long Value => 20 * 200;
        public override int Progress => 7;
    }

    public class ComputerBomber5 : ComputerBomber1
    {
        public override int Level => 5;
        public override float DamageS => 0.25f;
        public override float DamageG => 0.4f;
        public override float CritS => 25f;
        public override float CritG => 40f;
        public override long Value => 40 * 200;
        public override int Progress => 9;
        public override string SpecialUnLock => "PsiComputer";
    }
}
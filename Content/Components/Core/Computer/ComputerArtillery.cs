using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Core.Computer
{
    public class ComputerArtillery1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Computer;
        public override int Level => 1;
        public override string TypeName => "ComputerArtillery";
        public virtual float ShootSpeedT => 0.15f;
        public virtual float ShootSpeedX => 0.15f;
        public virtual float ShootSpeedL => 0.1f;
        public virtual float CritT => 10f;
        public virtual float CritX => 10f;
        public virtual float CritL => 15f;
        public override long Value => 0 * 200;

        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().ComputerType = TypeName;
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.WeaponAttackCD_T, ShootSpeedT);
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.WeaponAttackCD_X, ShootSpeedX);
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.WeaponAttackCD_L, ShootSpeedL);
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.WeaponCrit_T, CritT);
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.WeaponCrit_X, CritX);
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.WeaponCrit_L, CritL);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.ComputerArtillery"),
                CritL, Math.Round(ShootSpeedL * 100f, 1), CritX, Math.Round(ShootSpeedX * 100f, 1), CritT, Math.Round(ShootSpeedT * 100f, 1));
        }

    }

    public class ComputerArtillery2 : ComputerArtillery1
    {
        public override int Level => 2;
        public override float ShootSpeedT => 0.20f;
        public override float ShootSpeedX => 0.20f;
        public override float ShootSpeedL => 0.15f;
        public override float CritT => 15f;
        public override float CritX => 15f;
        public override float CritL => 20f;
        public override long Value => 5 * 200;
        public override int Progress => 3;
    }

    public class ComputerArtillery3 : ComputerArtillery1
    {
        public override int Level => 3;
        public override float ShootSpeedT => 0.25f;
        public override float ShootSpeedX => 0.25f;
        public override float ShootSpeedL => 0.20f;
        public override float CritT => 20f;
        public override float CritX => 20f;
        public override float CritL => 25f;
        public override long Value => 10 * 200;
        public override int Progress => 5;
    }

    public class ComputerArtillery4 : ComputerArtillery1
    {
        public override int Level => 4;
        public override float ShootSpeedT => 0.3f;
        public override float ShootSpeedX => 0.3f;
        public override float ShootSpeedL => 0.25f;
        public override float CritT => 25f;
        public override float CritX => 25f;
        public override float CritL => 30f;
        public override long Value => 20 * 200;
        public override int Progress => 7;
    }

    public class ComputerArtillery5 : ComputerArtillery1
    {
        public override int Level => 5;
        public override float ShootSpeedT => 0.35f;
        public override float ShootSpeedX => 0.35f;
        public override float ShootSpeedL => 0.3f;
        public override float CritT => 30f;
        public override float CritX => 30f;
        public override float CritL => 35f;
        public override long Value => 40 * 200;
        public override int Progress => 9;
        public override string SpecialUnLock => "PsiComputer";
    }
}
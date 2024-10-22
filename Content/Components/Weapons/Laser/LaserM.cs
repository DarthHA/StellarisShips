using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Laser
{
    public class LaserM1 : BaseWeaponComponent            //DPS:15*2.4
    {
        public override string EquipType => ComponentTypes.Weapon_M;
        public override int Level => 1;
        public override string TypeName => "Laser";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 70;
        public override float Crit => 10;
        public override int MaxDamage => 54;
        public override int MinDamage => 18;
        public override float MaxRange => 300;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_M };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_M };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_M };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_M };

        public override long Value => 20 * 200;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "M";
        }
    }

    public class LaserM2 : LaserM1           //DPS:25*2.4
    {
        public override int Level => 2;
        public override int MaxDamage => 90;
        public override int MinDamage => 30;
        public override long Value => 26 * 300;
        public override int Progress => 3;
    }

    public class LaserM3 : LaserM1          //DPS:40*2.4
    {
        public override int Level => 3;
        public override int MaxDamage => 144;
        public override int MinDamage => 48;
        public override long Value => 30 * 500;
        public override int Progress => 4;
    }

    public class LaserM4 : LaserM1          //DPS:60*2.4
    {
        public override int Level => 4;
        public override int MaxDamage => 216;
        public override int MinDamage => 72;
        public override long Value => 34 * 600;
        public override int Progress => 6;
    }

    public class LaserM5 : LaserM1          //DPS:80*2.4
    {
        public override int Level => 5;
        public override int MaxDamage => 288;
        public override int MinDamage => 96;
        public override long Value => 44 * 800;
        public override int Progress => 8;

    }
}
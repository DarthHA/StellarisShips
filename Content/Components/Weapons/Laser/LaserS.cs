using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Laser
{

    public class LaserS1 : BaseWeaponComponent            //DPS:15
    {
        public override string EquipType => ComponentTypes.Weapon_S;
        public override int Level => 1;
        public override string TypeName => "Laser";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 60;
        public override float Crit => 20;
        public override int MaxDamage => 25;
        public override int MinDamage => 5;
        public override float MaxRange => 200;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_S };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_S };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_S };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_S };

        public override long Value => 10 * 200;
        public override int Progress => 1;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "S";
        }
    }

    public class LaserS2 : LaserS1           //DPS:25
    {
        public override int Level => 2;
        public override int MaxDamage => 40;
        public override int MinDamage => 10;
        public override long Value => 13 * 300;
        public override int Progress => 3;
    }

    public class LaserS3 : LaserS1          //DPS:40
    {
        public override int Level => 3;
        public override int MaxDamage => 60;
        public override int MinDamage => 20;
        public override long Value => 15 * 500;
        public override int Progress => 4;
    }

    public class LaserS4 : LaserS1           //DPS:60
    {
        public override int Level => 4;
        public override int MaxDamage => 90;
        public override int MinDamage => 30;
        public override long Value => 17 * 600;
        public override int Progress => 6;
    }

    public class LaserS5 : LaserS1          //DPS:80
    {
        public override int Level => 5;
        public override int MaxDamage => 120;
        public override int MinDamage => 40;
        public override long Value => 22 * 800;
        public override int Progress => 8;
    }
}
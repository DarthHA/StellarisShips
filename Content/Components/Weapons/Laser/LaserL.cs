using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Laser
{
    public class LaserL1 : BaseWeaponComponent            //DPS:15*6
    {
        public override string EquipType => ComponentTypes.Weapon_L;
        public override int Level => 1;
        public override string TypeName => "Laser";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 80;
        public override float Crit => 0;
        public override int MaxDamage => 135;
        public override int MinDamage => 45;
        public override float MaxRange => 400;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_L };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_L };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_L };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_L };

        public override long Value => 40 * 200;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "L";
        }
    }

    public class LaserL2 : LaserL1            //DPS:25*6
    {
        public override int Level => 2;
        public override int MaxDamage => 225;
        public override int MinDamage => 75;
        public override long Value => 52 * 300;
        public override int Progress => 3;
    }

    public class LaserL3 : LaserL1            //DPS:40*6
    {
        public override int Level => 3;
        public override int MaxDamage => 360;
        public override int MinDamage => 120;
        public override long Value => 60 * 500;
        public override int Progress => 4;
    }

    public class LaserL4 : LaserL1            //DPS:60*6
    {
        public override int Level => 4;
        public override int MaxDamage => 540;
        public override int MinDamage => 180;
        public override long Value => 68 * 600;
        public override int Progress => 6;
    }

    public class LaserL5 : LaserL1            //DPS:80*6
    {
        public override int Level => 5;
        public override int MaxDamage => 720;
        public override int MinDamage => 240;
        public override long Value => 88 * 800;
        public override int Progress => 8;
    }
}
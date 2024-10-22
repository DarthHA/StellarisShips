using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Plasma
{
    public class PlasmaL1 : BaseWeaponComponent          //DPS:80
    {
        public override string EquipType => ComponentTypes.Weapon_L;
        public override int Level => 1;
        public override string TypeName => "Plasma";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 154;            //30ртио
        public override float Crit => 10;
        public override int MaxDamage => 580;
        public override int MinDamage => 120;
        public override float MaxRange => 400;
        public override float MinRange => 225;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_L };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_L };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_L };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_L };

        public override long Value => 52 * 300;
        public override int Progress => 2;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "L";
        }
    }

    public class PlasmaL2 : PlasmaL1          //DPS:200
    {
        public override int Level => 2;
        public override int MaxDamage => 1120;
        public override int MinDamage => 260;
        public override long Value => 68 * 500;
        public override int Progress => 5;
    }

    public class PlasmaL3 : PlasmaL1          //DPS:280
    {
        public override int Level => 3;
        public override int MaxDamage => 2100;
        public override int MinDamage => 380;
        public override long Value => 88 * 800;
        public override int Progress => 7;
    }
}
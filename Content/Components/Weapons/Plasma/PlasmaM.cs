using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Plasma
{
    public class PlasmaM1 : BaseWeaponComponent          //DPS:40
    {
        public override string EquipType => ComponentTypes.Weapon_M;
        public override int Level => 1;
        public override string TypeName => "Plasma";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 137;            //30ртио
        public override float Crit => 20;
        public override int MaxDamage => 150;
        public override int MinDamage => 22;
        public override float MaxRange => 300;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_M };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_M };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_M };

        public override long Value => 26 * 300;
        public override int Progress => 2;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "M";
        }
    }

    public class PlasmaM2 : PlasmaM1          //DPS:100
    {
        public override int Level => 2;
        public override int MaxDamage => 378;
        public override int MinDamage => 55;
        public override long Value => 34 * 500;
        public override int Progress => 5;
    }

    public class PlasmaM3 : PlasmaM1          //DPS:140
    {
        public override int Level => 3;
        public override int MaxDamage => 730;
        public override int MinDamage => 107;
        public override long Value => 44 * 800;
        public override int Progress => 7;
    }
}
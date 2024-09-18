using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Plasma
{
    public class PlasmaS1 : BaseWeaponComponent          //DPS:20
    {
        public override string EquipType => ComponentTypes.Weapon_S;
        public override int Level => 1;
        public override string TypeName => "Plasma";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 120;            //30ртио
        public override float Crit => 30;
        public override int MaxDamage => 63;
        public override int MinDamage => 11;
        public override float MaxRange => 200;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_S };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_S };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_S };

        public override long Value => 13 * 300;
        public override int Progress => 2;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "S";
        }
    }

    public class PlasmaS2 : PlasmaS1          //DPS:50
    {
        public override int Level => 2;
        public override int MaxDamage => 158;
        public override int MinDamage => 32;
        public override long Value => 17 * 500;
        public override int Progress => 5;
    }

    public class PlasmaS3 : PlasmaS1          //DPS:70
    {
        public override int Level => 3;
        public override int MaxDamage => 310;
        public override int MinDamage => 50;
        public override long Value => 22 * 800;
        public override int Progress => 7;
    }
}
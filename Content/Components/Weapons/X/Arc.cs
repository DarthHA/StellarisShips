using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.X
{
    public class Arc1 : BaseWeaponComponent          //DPS:640
    {
        public override string EquipType => ComponentTypes.Weapon_X;
        public override int Level => 1;
        public override string TypeName => "Arc";
        public override string ExtraInfo => "X";

        public override int AttackCD => 220;            //80ртио
        public override float Crit => 0;
        public override int MaxDamage => 7999;
        public override int MinDamage => 1;
        public override float MaxRange => 750;
        public override float MinRange => 50;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_X };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_X };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_X };

        public override long Value => 176 * 1000;
        public override int Progress => 8;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "";
        }
    }

    public class Arc2 : Arc1          //DPS:800
    {
        public override int Level => 2;
        public override float Crit => 0;
        public override int MaxDamage => 9998;
        public override int MinDamage => 2;
        public override long Value => 229 * 1200;
        public override int Progress => 9;
    }
}
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.X
{
    public class Lance1 : BaseWeaponComponent          //DPS:640
    {
        public override string EquipType => ComponentTypes.Weapon_X;
        public override int Level => 1;
        public override string TypeName => "Lance";
        public override string ExtraInfo => "X";

        public override int AttackCD => 220;            //80ртио
        public override float Crit => 0;
        public override int MaxDamage => 6300;
        public override int MinDamage => 2050;
        public override float MaxRange => 750;
        public override float MinRange => 0;

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

    public class Lance2 : Lance1          //DPS:800
    {
        public override int Level => 2;
        public override float Crit => 0;
        public override int MaxDamage => 7300;
        public override int MinDamage => 3050;
        public override long Value => 229 * 1200;
        public override int Progress => 9;
    }
}
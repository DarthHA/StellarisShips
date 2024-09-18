using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.PDefense
{
    public class PDefense1 : BaseWeaponComponent          //DPS:15
    {
        public override string EquipType => ComponentTypes.Weapon_P;
        public override int Level => 1;
        public override string TypeName => "PDefense";
        public override string ExtraInfo => "P";

        public override int AttackCD => 15;          //10ртио
        public override float Crit => 20;
        public override int MaxDamage => 8;
        public override int MinDamage => 6;
        public override float MaxRange => 200;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_P };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_P };

        public override long Value => 8 * 200;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "P";
        }
    }

    public class PDefense2 : PDefense1          //DPS:40
    {
        public override int Level => 2;
        public override int MaxDamage => 12;
        public override int MinDamage => 8;
        public override long Value => 10 * 500;
        public override int Progress => 4;
    }

    public class PDefense3 : PDefense1          //DPS:80
    {
        public override int Level => 3;
        public override int MaxDamage => 24;
        public override int MinDamage => 16;
        public override long Value => 13 * 800;
        public override int Progress => 7;
    }
}
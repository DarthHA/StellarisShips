using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Ancient
{
    public class ArchPDefense : BaseWeaponComponent          //DPS:15
    {
        public override string EquipType => ComponentTypes.Weapon_P;
        public override int Level => 1;
        public override string TypeName => "ArchPDefense";
        public override string ExtraInfo => "P";

        public override int AttackCD => 15;          //10以上
        public override float Crit => 20;
        public override int MaxDamage => 36;
        public override int MinDamage => 26;
        public override float MaxRange => 250;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_P };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_P };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_P };

        public override long Value => 16 * 1000;
        public override int MRValue => 30;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "P";
        }
    }
}

using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Ancient
{
    public class ArchTitanBeam : BaseWeaponComponent          //DPS:640
    {
        public override string EquipType => ComponentTypes.Weapon_T;
        public override int Level => 1;
        public override string TypeName => "ArchTitanBeam";
        public override string ExtraInfo => "T";

        public override int AttackCD => 360;            //80以上
        public override float Crit => 0;
        public override int MaxDamage => 37500;
        public override int MinDamage => 22500;
        public override float MaxRange => 600;
        public override float MinRange => 0;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_T };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_T };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_T };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_T };

        public override long Value => 456 * 1500;
        public override int MRValue => 480;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "";
        }
    }
}
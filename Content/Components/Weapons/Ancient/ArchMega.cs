using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Ancient
{
    public class ArchMega : BaseWeaponComponent          //DPS:640
    {
        public override string EquipType => ComponentTypes.Weapon_X;
        public override int Level => 1;
        public override string TypeName => "ArchMega";
        public override string ExtraInfo => "X";

        public override int AttackCD => 240;            //80以上
        public override float Crit => 0;
        public override int MaxDamage => 10000;
        public override int MinDamage => 6400;
        public override float MaxRange => 750;
        public override float MinRange => 225;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_X };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_X };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_X };

        public override long Value => 300 * 1500;
        public override int MRValue => 240;

        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "";
        }
    }
}

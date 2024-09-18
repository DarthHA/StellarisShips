using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.T
{
    public class TitanBeam : BaseWeaponComponent          //DPS:640
    {
        public override string EquipType => ComponentTypes.Weapon_T;
        public override int Level => 1;
        public override string TypeName => "TitanBeam";
        public override string ExtraInfo => "T";

        public override int AttackCD => 360;            //80ртио
        public override float Crit => 0;
        public override int MaxDamage => 22500;
        public override int MinDamage => 15000;
        public override float MaxRange => 1250;
        public override float MinRange => 0;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_T };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_T };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_T };

        public override long Value => 456 * 1200;
        public override int Progress => 9;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "";
        }
    }
}
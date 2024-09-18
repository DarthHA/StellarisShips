using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Ancient
{
    public class ArchLaserS : BaseWeaponComponent
    {
        public override string EquipType => ComponentTypes.Weapon_S;
        public override int Level => 1;
        public override string TypeName => "ArchLaser";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 80;
        public override float Crit => 40;
        public override int MaxDamage => 211;
        public override int MinDamage => 70;
        public override float MaxRange => 200;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_S };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_S };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_S };

        public override long Value => 26 * 1000;
        public override int MRValue => 30;

        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "S";
        }
    }

    public class ArchLaserM : BaseWeaponComponent
    {
        public override string EquipType => ComponentTypes.Weapon_M;
        public override int Level => 1;
        public override string TypeName => "ArchLaser";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 80;
        public override float Crit => 30;
        public override int MaxDamage => 552;
        public override int MinDamage => 182;
        public override float MaxRange => 300;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_M };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_M };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_M };

        public override long Value => 53 * 1000;
        public override int MRValue => 60;

        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "M";
        }
    }

    public class ArchLaserL : BaseWeaponComponent
    {
        public override string EquipType => ComponentTypes.Weapon_L;
        public override int Level => 1;
        public override string TypeName => "ArchLaser";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 80;
        public override float Crit => 20;
        public override int MaxDamage => 1256;
        public override int MinDamage => 422;
        public override float MaxRange => 400;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_L };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_L };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_L };

        public override long Value => 105 * 1000;
        public override int MRValue => 120;

        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "L";
        }
    }
}

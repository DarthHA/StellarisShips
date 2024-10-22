using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Special
{
    public class ExDimensionalS : BaseWeaponComponent
    {
        public override string EquipType => ComponentTypes.Weapon_S;
        public override int Level => 1;
        public override string TypeName => "ExDimensional";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 60;
        public override float Crit => 20;
        public override int MaxDamage => 190;
        public override int MinDamage => 52;
        public override float MaxRange => 150;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_S };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_S };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_S };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_S };

        public override long Value => 23 * 1000;
        public override string SpecialUnLock => "ExDimensional";

        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "S";
        }
    }

    public class ExDimensionalM : ExDimensionalS
    {
        public override string EquipType => ComponentTypes.Weapon_M;

        public override int AttackCD => 60;
        public override float Crit => 10;
        public override int MaxDamage => 410;
        public override int MinDamage => 121;
        public override float MaxRange => 250;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_M };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_M };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_M };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_M };

        public override long Value => 46 * 1000;
        public override string SpecialUnLock => "ExDimensional";

        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "M";
        }
    }

    public class ExDimensionalL : ExDimensionalS
    {
        public override string EquipType => ComponentTypes.Weapon_L;

        public override int AttackCD => 60;
        public override float Crit => 0;
        public override int MaxDamage => 998;
        public override int MinDamage => 256;
        public override float MaxRange => 350;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_L };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_L };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_L };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_L };

        public override long Value => 82 * 1000;
        public override string SpecialUnLock => "ExDimensional";

        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "L";
        }
    }
}
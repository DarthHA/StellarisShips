using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Special
{
    public class NullVoidS : BaseWeaponComponent                  //标准：100DPS纯，50DPS防御，-1/Slot参数吗
    {
        public override string EquipType => ComponentTypes.Weapon_S;
        public override int Level => 1;
        public override string TypeName => "NullVoid";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 60;
        public override float Crit => 0;
        public override int MaxDamage => 96;
        public override int MinDamage => 32;
        public override float MaxRange => 300;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_S };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_S };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_S };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_S };

        public override long Value => 23 * 1000;
        public override string SpecialUnLock => "NullVoid";

        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "S";
        }
    }

    public class NullVoidM : NullVoidS
    {
        public override string EquipType => ComponentTypes.Weapon_M;

        public override int AttackCD => 70;
        public override float Crit => 0;
        public override int MaxDamage => 235;
        public override int MinDamage => 110;
        public override float MaxRange => 450;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_M };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_M };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_M };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_M };

        public override long Value => 46 * 1000;
        public override string SpecialUnLock => "NullVoid";

        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "M";
        }
    }

    public class NullVoidL : NullVoidS
    {
        public override string EquipType => ComponentTypes.Weapon_L;

        public override int AttackCD => 80;
        public override float Crit => 0;
        public override int MaxDamage => 536;
        public override int MinDamage => 180;
        public override float MaxRange => 600;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_L };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_L };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_L };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_L };

        public override long Value => 82 * 1000;
        public override string SpecialUnLock => "NullVoid";

        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "L";
        }
    }
}
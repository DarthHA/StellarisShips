using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.AutoCannon
{
    public class AutoCannonL1 : BaseWeaponComponent          //DPS:80
    {
        public override string EquipType => ComponentTypes.Weapon_L;
        public override int Level => 1;
        public override string TypeName => "AutoCannon";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 40;
        public override float Crit => 0;
        public override int MaxDamage => 72;
        public override int MinDamage => 63;
        public override float MaxRange => 200;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_L };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_L };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_L };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_L };

        public override long Value => 52 * 300;
        public override int Progress => 2;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "L";
        }
    }

    public class AutoCannon2 : AutoCannonL1          //DPS:200
    {
        public override int Level => 2;
        public override int MaxDamage => 190;
        public override int MinDamage => 176;
        public override long Value => 68 * 500;
        public override int Progress => 5;
    }

    public class AutoCannon3 : AutoCannonL1          //DPS:280
    {
        public override int Level => 3;
        public override int MaxDamage => 280;
        public override int MinDamage => 258;
        public override long Value => 88 * 800;
        public override int Progress => 7;
    }

    public class AutoCannon4 : AutoCannonL1          //DPS:360
    {
        public override int Level => 4;
        public override int MaxDamage => 356;
        public override int MinDamage => 328;
        public override long Value => 116 * 1000;
        public override int Progress => 9;
        public override string SpecialUnLock => "NanoAutoCannon";
    }

}
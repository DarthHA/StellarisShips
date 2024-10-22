using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.AutoCannon
{
    public class AutoCannonM1 : BaseWeaponComponent          //DPS:40
    {
        public override string EquipType => ComponentTypes.Weapon_M;
        public override int Level => 1;
        public override string TypeName => "AutoCannon";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 30;
        public override float Crit => 0;
        public override int MaxDamage => 32;
        public override int MinDamage => 27;
        public override float MaxRange => 175;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_M };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_M };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_M };

        public override long Value => 26 * 300;
        public override int Progress => 2;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "M";
        }
    }

    public class AutoCannonM2 : AutoCannonM1          //DPS:100
    {
        public override int Level => 2;
        public override int MaxDamage => 80;
        public override int MinDamage => 72;
        public override long Value => 34 * 500;
        public override int Progress => 5;
    }

    public class AutoCannonM3 : AutoCannonM1          //DPS:140
    {
        public override int Level => 3;
        public override int MaxDamage => 120;
        public override int MinDamage => 113;
        public override long Value => 44 * 800;
        public override int Progress => 7;
    }
    public class AutoCannonM4 : AutoCannonM1          //DPS:180
    {
        public override int Level => 4;
        public override int MaxDamage => 148;
        public override int MinDamage => 135;
        public override long Value => 58 * 1000;
        public override int Progress => 9;
        public override string SpecialUnLock => "NanoAutoCannon";
    }

}
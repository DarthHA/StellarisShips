using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.AutoCannon
{
    public class AutoCannonS1 : BaseWeaponComponent          //DPS:20
    {
        public override string EquipType => ComponentTypes.Weapon_S;
        public override int Level => 1;
        public override string TypeName => "AutoCannon";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 20;
        public override float Crit => 0;
        public override int MaxDamage => 12;
        public override int MinDamage => 11;
        public override float MaxRange => 150;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_S };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_S };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_S };

        public override long Value => 13 * 300;
        public override int Progress => 2;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "S";
        }
    }

    public class AutoCannonS2 : AutoCannonS1          //DPS:50
    {
        public override int Level => 2;
        public override int MaxDamage => 32;
        public override int MinDamage => 29;
        public override long Value => 17 * 500;
        public override int Progress => 5;
    }

    public class AutoCannonS3 : AutoCannonS1          //DPS:70
    {
        public override int Level => 3;
        public override int MaxDamage => 49;
        public override int MinDamage => 43;
        public override long Value => 22 * 800;
        public override int Progress => 7;
    }

    public class AutoCannonS4 : AutoCannonS1          //DPS:90
    {
        public override int Level => 4;
        public override int MaxDamage => 69;
        public override int MinDamage => 54;
        public override long Value => 29 * 1000;
        public override int Progress => 9;
        public override string SpecialUnLock => "NanoAutoCannon";
    }

}
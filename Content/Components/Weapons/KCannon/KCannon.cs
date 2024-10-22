using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.KCannon
{
    public class KCannon1 : BaseWeaponComponent          //DPS:200
    {
        public override string EquipType => ComponentTypes.Weapon_L;
        public override int Level => 1;
        public override string TypeName => "KCannon";
        public override string ExtraInfo => "L";

        public override int AttackCD => 240;            //30ртио
        public override float Crit => 20;
        public override int MaxDamage => 900;
        public override int MinDamage => 700;
        public override float MaxRange => 600;
        public override float MinRange => 225;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_L };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_L };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_L };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_L };

        public override long Value => 88 * 700;
        public override int Progress => 5;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "L";
        }
    }

    public class KCannon2 : KCannon1          //DPS:280
    {
        public override int Level => 2;
        public override int MaxDamage => 1440;
        public override int MinDamage => 1160;
        public override long Value => 114 * 900;
        public override int Progress => 7;
    }

}
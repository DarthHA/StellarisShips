using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Disruptor
{
    public class CloudLightning : BaseWeaponComponent          //DPS:382
    {
        public override string EquipType => ComponentTypes.Weapon_L;
        public override int Level => 1;
        public override string TypeName => "CloudLightning";
        public override string ExtraInfo => "L";

        public override int AttackCD => 170;
        public override float Crit => 0;
        public override int MaxDamage => 1900;
        public override int MinDamage => 1;
        public override float MaxRange => 300;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_L };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_L };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_L };

        public override long Value => 98 * 900;
        public override int Progress => 7;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "L";
        }
    }
}

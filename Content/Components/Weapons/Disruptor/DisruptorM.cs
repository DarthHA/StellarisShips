using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Disruptor
{
    public class DisruptorM1 : BaseWeaponComponent          //DPS:40
    {
        public override string EquipType => ComponentTypes.Weapon_M;
        public override int Level => 1;
        public override string TypeName => "Disruptor";
        public override string ExtraInfo => "SM";

        public override int AttackCD => 85;
        public override float Crit => 10;
        public override int MaxDamage => 130;
        public override int MinDamage => 1;
        public override float MaxRange => 200;

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

    public class DisruptorM2 : DisruptorM1          //DPS:100
    {
        public override int Level => 2;
        public override int MaxDamage => 300;
        public override int MinDamage => 1;
        public override long Value => 30 * 500;
        public override int Progress => 5;
    }

    public class DisruptorM3 : DisruptorM1          //DPS:140
    {
        public override int Level => 3;
        public override int MaxDamage => 460;
        public override int MinDamage => 1;
        public override long Value => 44 * 800;
        public override int Progress => 7;
    }
}
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Disruptor
{
    public class DisruptorS1 : BaseWeaponComponent          //DPS:20
    {
        public override string EquipType => ComponentTypes.Weapon_S;
        public override int Level => 1;
        public override string TypeName => "Disruptor";
        public override string ExtraInfo => "SM";

        public override int AttackCD => 60;
        public override float Crit => 20;
        public override int MaxDamage => 36;
        public override int MinDamage => 12;
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

    public class DisruptorS2 : DisruptorS1          //DPS:50
    {
        public override int Level => 2;
        public override int MaxDamage => 90;
        public override int MinDamage => 30;
        public override long Value => 15 * 500;
        public override int Progress => 5;
    }

    public class DisruptorS3 : DisruptorS1          //DPS:70
    {
        public override int Level => 3;
        public override int MaxDamage => 126;
        public override int MinDamage => 42;
        public override long Value => 22 * 800;
        public override int Progress => 7;
    }
}
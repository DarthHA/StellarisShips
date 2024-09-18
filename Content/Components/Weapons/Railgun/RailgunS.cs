using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Railgun
{
    public class RailgunS1 : BaseWeaponComponent            //DPS:15
    {
        public override string EquipType => ComponentTypes.Weapon_S;
        public override int Level => 1;
        public override string TypeName => "Railgun";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 90;            //30ртио
        public override float Crit => 30;
        public override int MaxDamage => 28;
        public override int MinDamage => 22;
        public override float MaxRange => 250;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_S };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_S };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_S };

        public override long Value => 10 * 200;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "S";
        }
    }

    public class RailgunS2 : RailgunS1           //DPS:25
    {
        public override int Level => 2;
        public override int MaxDamage => 44;
        public override int MinDamage => 38;
        public override long Value => 13 * 300;
        public override int Progress => 3;
    }

    public class RailgunS3 : RailgunS1          //DPS:40
    {
        public override int Level => 3;
        public override int MaxDamage => 77;
        public override int MinDamage => 56;
        public override long Value => 15 * 500;
        public override int Progress => 4;
    }

    public class RailgunS4 : RailgunS1           //DPS:60
    {
        public override int Level => 4;
        public override int MaxDamage => 110;
        public override int MinDamage => 88;
        public override long Value => 17 * 600;
        public override int Progress => 6;
    }

    public class RailgunS5 : RailgunS1          //DPS:80
    {
        public override int Level => 5;
        public override int MaxDamage => 154;
        public override int MinDamage => 110;
        public override long Value => 22 * 800;
        public override int Progress => 8;
    }
}
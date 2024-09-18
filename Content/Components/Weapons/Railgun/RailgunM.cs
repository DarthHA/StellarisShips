using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Railgun
{
    public class RailgunM1 : BaseWeaponComponent            //DPS:30
    {
        public override string EquipType => ComponentTypes.Weapon_M;
        public override int Level => 1;
        public override string TypeName => "Railgun";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 111;            //30ртио
        public override float Crit => 20;
        public override int MaxDamage => 66;
        public override int MinDamage => 58;
        public override float MaxRange => 375;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_M };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_M };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_M };

        public override long Value => 20 * 200;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "M";
        }
    }

    public class RailgunM2 : RailgunM1           //DPS:50
    {
        public override int Level => 2;
        public override int MaxDamage => 109;
        public override int MinDamage => 99;
        public override long Value => 26 * 300;
        public override int Progress => 3;
    }

    public class RailgunM3 : RailgunM1          //DPS:80
    {
        public override int Level => 3;
        public override int MaxDamage => 181;
        public override int MinDamage => 143;
        public override long Value => 30 * 500;
        public override int Progress => 4;
    }

    public class RailgunM4 : RailgunM1          //DPS:120
    {
        public override int Level => 4;
        public override int MaxDamage => 270;
        public override int MinDamage => 230;
        public override long Value => 34 * 600;
        public override int Progress => 6;
    }

    public class RailgunM5 : RailgunM1          //DPS:160
    {
        public override int Level => 5;
        public override int MaxDamage => 385;
        public override int MinDamage => 280;
        public override long Value => 44 * 800;
        public override int Progress => 8;
    }
}
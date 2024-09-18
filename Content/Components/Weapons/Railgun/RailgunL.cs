using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Railgun
{
    public class RailgunL1 : BaseWeaponComponent            //DPS:60
    {
        public override string EquipType => ComponentTypes.Weapon_L;
        public override int Level => 1;
        public override string TypeName => "Railgun";
        public override string ExtraInfo => "SML";

        public override int AttackCD => 131;            //30ртио
        public override float Crit => 10;
        public override int MaxDamage => 165;
        public override int MinDamage => 132;
        public override float MaxRange => 500;
        public override float MinRange => 225;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_L };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_L };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_L };

        public override long Value => 40 * 200;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "L";
        }
    }

    public class RailgunL2 : RailgunL1            //DPS:100
    {
        public override int Level => 2;
        public override int MaxDamage => 264;
        public override int MinDamage => 231;
        public override long Value => 52 * 300;
        public override int Progress => 3;
    }

    public class RailgunL3 : RailgunL1            //DPS:160
    {
        public override int Level => 3;
        public override int MaxDamage => 462;
        public override int MinDamage => 330;
        public override long Value => 60 * 500;
        public override int Progress => 4;
    }

    public class RailgunL4 : RailgunL1            //DPS:240
    {
        public override int Level => 4;
        public override int MaxDamage => 660;
        public override int MinDamage => 528;
        public override long Value => 68 * 600;
        public override int Progress => 6;
    }

    public class RailgunL5 : RailgunL1            //DPS:320
    {
        public override int Level => 5;
        public override int MaxDamage => 882;
        public override int MinDamage => 630;
        public override long Value => 88 * 800;
        public override int Progress => 8;
    }
}
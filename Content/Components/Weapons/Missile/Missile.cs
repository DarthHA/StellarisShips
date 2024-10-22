using StellarisShips.Content.WeaponUnits;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Missile
{
    public class Missile1 : BaseWeaponComponent
    {
        public override string EquipType => ComponentTypes.Weapon_S;          //DPS:15
        public override int Level => 1;
        public override string TypeName => "Missile";
        public override string ExtraInfo => "S";
        public override bool IsExplosive => true;

        public override int AttackCD => 180;            //40ртио
        public override float Crit => 20;
        public override int MaxDamage => 65;
        public override int MinDamage => 35;
        public override float MaxRange => 500;

        public virtual float DetectRange => 600;
        public virtual float HomingFactor => 0.05f;
        public virtual float MaxSpeed => 10;
        public virtual float ExplosionScale => 1.2f;
        public virtual int TimeLeft => 360;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_S };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_S };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_S };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_S };

        public override long Value => 10 * 200;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "SE";
            (weapon as MissileUnit).DetectRange = DetectRange;
            (weapon as MissileUnit).HomingFactor = HomingFactor;
            (weapon as MissileUnit).MaxSpeed = MaxSpeed;
            (weapon as MissileUnit).ExplosionScale = ExplosionScale;
            (weapon as MissileUnit).TimeLeft = TimeLeft;
        }
    }

    public class Missile2 : Missile1          //DPS:25
    {
        public override int Level => 2;

        public override int MaxDamage => 85;
        public override int MinDamage => 65;

        public override float DetectRange => 700;
        public override float HomingFactor => 0.05f;
        public override float MaxSpeed => 10f;
        public override float ExplosionScale => 1.3f;
        public override int TimeLeft => 360;
        public override long Value => 13 * 300;
        public override int Progress => 3;
    }

    public class Missile3 : Missile1          //DPS:40
    {
        public override int Level => 3;

        public override int MaxDamage => 130;
        public override int MinDamage => 110;

        public override float DetectRange => 800;
        public override float HomingFactor => 0.075f;
        public override float MaxSpeed => 10f;
        public override float ExplosionScale => 1.4f;
        public override int TimeLeft => 480;
        public override long Value => 15 * 500;
        public override int Progress => 4;
    }

    public class Missile4 : Missile1          //DPS:60
    {
        public override int Level => 4;

        public override int MaxDamage => 200;
        public override int MinDamage => 160;

        public override float DetectRange => 900;
        public override float HomingFactor => 0.075f;
        public override float MaxSpeed => 10f;
        public override float ExplosionScale => 1.5f;
        public override int TimeLeft => 480;
        public override long Value => 17 * 600;
        public override int Progress => 6;
    }

    public class Missile5 : Missile1          //DPS:80
    {
        public override int Level => 5;

        public override int MaxDamage => 280;
        public override int MinDamage => 200;

        public override float DetectRange => 1000;
        public override float HomingFactor => 0.1f;
        public override float MaxSpeed => 10f;
        public override float ExplosionScale => 1.6f;
        public override int TimeLeft => 600;
        public override long Value => 22 * 800;
        public override int Progress => 8;
    }

}
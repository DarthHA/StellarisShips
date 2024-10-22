using StellarisShips.Content.WeaponUnits;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Missile
{
    public class MissileS1 : BaseWeaponComponent          //DPS:80
    {
        public override string EquipType => ComponentTypes.Weapon_M;
        public override int Level => 1;
        public override string TypeName => "MissileS";
        public override string ExtraInfo => "M";
        public override bool IsExplosive => true;

        public override int AttackCD => 120;            //40ртио
        public override float Crit => 10;
        public override int MaxDamage => 310;
        public override int MinDamage => 250;
        public override float MaxRange => 600;

        public virtual float DetectRange => 800;
        public virtual float HomingFactor => 0.05f;
        public virtual float MaxSpeed => 10;
        public virtual float ExplosionScale => 1.2f;
        public virtual int TimeLeft => 360;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_M };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_M };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_M };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_M };

        public override long Value => 34 * 400;
        public override int Progress => 4;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "G";
            (weapon as MissileSUnit).DetectRange = DetectRange;
            (weapon as MissileSUnit).HomingFactor = HomingFactor;
            (weapon as MissileSUnit).MaxSpeed = MaxSpeed;
            (weapon as MissileSUnit).ExplosionScale = ExplosionScale;
            (weapon as MissileSUnit).TimeLeft = TimeLeft;
        }
    }

    public class MissileS2 : MissileS1          //DPS:120
    {
        public override int Level => 2;

        public override int MaxDamage => 470;
        public override int MinDamage => 320;

        public override float DetectRange => 1000;
        public override int TimeLeft => 480;
        public override long Value => 44 * 800;
        public override int Progress => 6;
    }
}
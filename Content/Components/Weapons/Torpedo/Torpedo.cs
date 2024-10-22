using StellarisShips.Content.WeaponUnits;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Torpedo
{
    public class Torpedo1 : BaseWeaponComponent          //DPS:40
    {
        public override string EquipType => ComponentTypes.Weapon_G;
        public override int Level => 1;
        public override string TypeName => "Torpedo";
        public override string ExtraInfo => "G";
        public override bool IsExplosive => true;

        public override int AttackCD => 360;            //40ртио
        public override float Crit => 20;
        public override int MaxDamage => 280;
        public override int MinDamage => 200;
        public override float MaxRange => 150;

        public virtual float DetectRange => 400;
        public virtual float HomingFactor => 0.03f;
        public virtual float MaxSpeed => 8;
        public virtual float ExplosionScale => 1.45f;
        public virtual int TimeLeft => 180;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_G };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_G };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_G };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_G };

        public override long Value => 34 * 300;
        public override int Progress => 2;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "G";
            (weapon as TorpedoUnit).DetectRange = DetectRange;
            (weapon as TorpedoUnit).HomingFactor = HomingFactor;
            (weapon as TorpedoUnit).MaxSpeed = MaxSpeed;
            (weapon as TorpedoUnit).ExplosionScale = ExplosionScale;
            (weapon as TorpedoUnit).TimeLeft = TimeLeft;
        }
    }

    public class Torpedo2 : Torpedo1          //DPS:100
    {
        public override int Level => 2;
        public override int MaxDamage => 720;
        public override int MinDamage => 480;

        public override float DetectRange => 500;
        public override float HomingFactor => 0.04f;
        public override float MaxSpeed => 8f;
        public override float ExplosionScale => 1.7f;
        public override int TimeLeft => 240;
        public override long Value => 38 * 500;
        public override int Progress => 5;
    }

    public class Torpedo3 : Torpedo1          //DPS:140
    {
        public override int Level => 3;

        public override int MaxDamage => 980;
        public override int MinDamage => 700;

        public override float DetectRange => 600;
        public override float HomingFactor => 0.05f;
        public override float MaxSpeed => 8f;
        public override float ExplosionScale => 1.75f;
        public override int TimeLeft => 360;
        public override long Value => 44 * 800;
        public override int Progress => 7;
    }
}
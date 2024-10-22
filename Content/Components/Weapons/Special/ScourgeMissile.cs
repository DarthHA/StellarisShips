using StellarisShips.Content.WeaponUnits;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Special
{
    public class ScourgeMissile : BaseWeaponComponent          //DPS:201
    {
        public override string EquipType => ComponentTypes.Weapon_G;
        public override int Level => 1;
        public override string TypeName => "ScourgeMissile";
        public override string ExtraInfo => "G";
        public override bool IsExplosive => true;

        public override int AttackCD => 120;            //40以上
        public override float Crit => 20;
        public override int MaxDamage => 365;
        public override int MinDamage => 305;
        public override float MaxRange => 450;

        public virtual float DetectRange => 800;
        public virtual float HomingFactor => 0.1f;
        public virtual float MaxSpeed => 10;
        public virtual float ExplosionScale => 2f;
        public virtual int TimeLeft => 360;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_G };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_G };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_G };

        public override long Value => 65 * 1000;
        public override int Progress => 9;
        public override string SpecialUnLock => "ScourgeMissile";

        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "G";
            (weapon as ScourgeMissileUnit).DetectRange = DetectRange;
            (weapon as ScourgeMissileUnit).HomingFactor = HomingFactor;
            (weapon as ScourgeMissileUnit).MaxSpeed = MaxSpeed;
            (weapon as ScourgeMissileUnit).ExplosionScale = ExplosionScale;
            (weapon as ScourgeMissileUnit).TimeLeft = TimeLeft;
        }
    }


}
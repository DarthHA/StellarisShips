using StellarisShips.Content.WeaponUnits;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Ancient
{
    public class ArchMissile : BaseWeaponComponent
    {
        public override string EquipType => ComponentTypes.Weapon_S;          //DPS:15
        public override int Level => 1;
        public override string TypeName => "ArchMissile";
        public override string ExtraInfo => "S";
        public override bool IsExplosive => true;

        public override int AttackCD => 180;            //40以上
        public override float Crit => 20;
        public override int MaxDamage => 420;
        public override int MinDamage => 360;
        public override float MaxRange => 500;

        public float DetectRange => 1000;
        public float HomingFactor => 0.1f;
        public float MaxSpeed => 15;
        public float ExplosionScale => 0.4f;
        public int TimeLeft => 360;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_S };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_S };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_S };

        public override long Value => 26 * 1000;
        public override int MRValue => 30;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "SE";
            (weapon as ArchMissileUnit).DetectRange = DetectRange;
            (weapon as ArchMissileUnit).HomingFactor = HomingFactor;
            (weapon as ArchMissileUnit).MaxSpeed = MaxSpeed;
            (weapon as ArchMissileUnit).ExplosionScale = ExplosionScale;
            (weapon as ArchMissileUnit).TimeLeft = TimeLeft;
        }
    }
}
